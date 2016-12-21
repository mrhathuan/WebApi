using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Data;
using DTO;
using ExpressionEvaluator;
using System.Reflection;

namespace Business
{
    public class MessageQueue<T>
    {
        private Queue<T> _msgQueue;
        private Action<T> _processMsg;
        private Task _runUpdateDynamicData;
        private CancellationTokenSource _tokenSource;
        private CancellationToken _ct;

        public MessageQueue(Action<T> processMsg)
        {
            _processMsg = processMsg;
            _msgQueue = new Queue<T>();
            _tokenSource = new CancellationTokenSource();
            _ct = _tokenSource.Token;
            _runUpdateDynamicData = new Task(ProcessMsgInQueue, _ct);
            _runUpdateDynamicData.Start();
        }

        private readonly object _token = new object();
        public void EnqueueAMessage(T msg)
        {
            lock (_token)
            {
                _msgQueue.Enqueue(msg);
                Monitor.Pulse(_token);
            }
        }

        private void ProcessMsgInQueue()
        {
            while (true)
            {
                T msg;
                lock (_token)
                {
                    if (_msgQueue.Count == 0) Monitor.Wait(_token);
                    if (_ct.IsCancellationRequested)
                        _ct.ThrowIfCancellationRequested();
                    msg = _msgQueue.Dequeue();
                }

                _processMsg(msg);
            }
        }

        public void TerminateQueue()
        {
            Monitor.PulseAll(_token);
            _tokenSource.Cancel();
        }
    }

    public static class HelperMessageQueue
    {
        private static MessageQueue<DTOEventSender> _eventQueue;
        public static MessageQueue<DTOEventSender> EventQueue
        {
            get
            {
                if (_eventQueue == null)
                    _eventQueue = new MessageQueue<DTOEventSender>(DataEntitiesObject_OnDataEntitiesChanged);
                return _eventQueue;
            }
        }

        private static void DataEntitiesObject_OnDataEntitiesChanged(DTOEventSender sender)
        {
            if (sender != null && sender.ListEvent != null && sender.Account != null)
            {
                System.Diagnostics.Debug.WriteLine("Workflow run");
                var Account = (AccountItem)sender.Account;
                try
                {
                    if (sender.ListEvent.Count > 0)
                    {
                        using (var model = new DataEntities())
                        {
                            model.EventAccount = Account; model.EventRunning = true;

                            var lstTable = model.WFL_EventField.Where(c => c.WFL_Event.PacketSettingID == null && c.WFL_Event.SYSCustomerID == Account.SYSCustomerID && (c.WFL_Event.IsApproved || c.WFL_Event.IsSystem)).Select(c => new
                            {
                                c.WFL_Field.TableName,
                                c.WFL_Field.ColumnName,
                                c.WFL_Event.Expression,
                                EventID = c.WFL_Event.ID,
                                c.WFL_Event.EventName,
                                c.WFL_Field.ColumnType,
                                c.IsModified,
                                c.WFL_Event.IsSystem
                            }).ToList();
                            var lstEventTemplate = model.WFL_EventTemplate.Where(c => c.WFL_Event.PacketSettingID == null && c.WFL_Event.SYSCustomerID == Account.SYSCustomerID && (c.WFL_Event.IsApproved || c.WFL_Event.IsSystem)).Select(c => new
                            {
                                c.EventID,
                                c.TypeOfActionID,
                                c.WFL_Template.Name,
                                c.WFL_Template.Template,
                                c.WFL_Template.TemplateDetail,
                                c.ID,
                            }).ToList();

                            if (Account.SYSCustomerID < 2)
                            {
                                lstTable = model.WFL_EventField.Where(c => c.WFL_Event.PacketSettingID == null && (c.WFL_Event.IsApproved || c.WFL_Event.IsSystem)).Select(c => new
                                {
                                    c.WFL_Field.TableName,
                                    c.WFL_Field.ColumnName,
                                    c.WFL_Event.Expression,
                                    EventID = c.WFL_Event.ID,
                                    c.WFL_Event.EventName,
                                    c.WFL_Field.ColumnType,
                                    c.IsModified,
                                    c.WFL_Event.IsSystem
                                }).ToList();

                                lstEventTemplate = model.WFL_EventTemplate.Where(c => c.WFL_Event.PacketSettingID == null && (c.WFL_Event.IsApproved || c.WFL_Event.IsSystem)).Select(c => new
                                {
                                    c.EventID,
                                    c.TypeOfActionID,
                                    c.WFL_Template.Name,
                                    c.WFL_Template.Template,
                                    c.WFL_Template.TemplateDetail,
                                    c.ID,
                                }).ToList();
                            }

                            var lstSYSEvent = new List<EventCommonWorkflow>();
                            var lstTableName = lstTable.Where(c => c.IsModified).Select(c => c.TableName).Distinct().ToList();
                            var lstChange = sender.ListEvent.Where(c => lstTableName.Contains(c.Table));
                            foreach (var item in lstChange)
                            {
                                var lstEventMatch = lstTable.Where(c => c.TableName == item.Table).GroupBy(c => new
                                {
                                    c.EventID,
                                    c.TableName,
                                    c.Expression,
                                    c.EventName,
                                }).ToList();
                                if (lstEventMatch.Count > 0)
                                {
                                    object obj = null;
                                    obj = GetObjectByTableName(model, item.Table, item.ID, obj, Account);
                                    if (obj != null)
                                    {
                                        // Danh sách event trigger
                                        foreach (var evt in lstEventMatch)
                                        {
                                            if (evt.Any(c => c.IsModified))
                                            {
                                                var lstTemplate = lstEventTemplate.Where(c => c.EventID == evt.Key.EventID).ToList();
                                                var objEvent = model.WFL_Event.FirstOrDefault(c => c.ID == evt.Key.EventID);
                                                // Chỉ xét obj có các field được modified như event khai báo
                                                var lstModifiedColumn = evt.Where(c => c.IsModified).Select(c => c.ColumnName).Distinct().ToList();
                                                int count = 0;
                                                foreach (var itemChange in item.lstProperty)
                                                {
                                                    if (lstModifiedColumn.Count(c => c == itemChange) > 0)
                                                        count++;
                                                }
                                                if (lstTemplate.Count > 0 && lstModifiedColumn.Count == count)
                                                {
                                                    // Ktra công thức event có matching
                                                    var registry = new TypeRegistry();
                                                    foreach (var property in evt)
                                                        registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                                                    var compiler = new CompiledExpression(evt.Key.Expression);
                                                    compiler.TypeRegistry = registry;
                                                    bool res = false;
                                                    try
                                                    {
                                                        res = (bool)compiler.Eval();
                                                    }
                                                    catch
                                                    {
                                                        res = false;
                                                    }

                                                    // Event matching => xử lý send mail || sms || message tms
                                                    if (res)
                                                    {
                                                        if (objEvent.IsSystem)
                                                        {
                                                            var id = Convert.ToInt32(obj.GetType().GetProperty("ID").GetValue(obj));
                                                            var lstProperty = evt.Where(c => c.IsModified && (c.ColumnType == "int" || c.ColumnType == "bool"));
                                                            foreach (var prop in lstProperty)
                                                            {
                                                                try
                                                                {
                                                                    switch (prop.ColumnType)
                                                                    {
                                                                        case "int":
                                                                            lstSYSEvent.Add(new EventCommonWorkflow { EventCode = objEvent.Code, ID = id, IntChange = (int)obj.GetType().GetProperty(prop.ColumnName).GetValue(obj), BoolChange = false });
                                                                            break;
                                                                        case "bool":
                                                                            lstSYSEvent.Add(new EventCommonWorkflow { EventCode = objEvent.Code, ID = id, IntChange = -1, BoolChange = (bool)obj.GetType().GetProperty(prop.ColumnName).GetValue(obj) });
                                                                            break;
                                                                    }
                                                                }
                                                                catch
                                                                {

                                                                }
                                                            }
                                                        }

                                                        List<int> lstCustomerID = new List<int>();
                                                        try
                                                        {
                                                            var objList = obj.GetType().GetProperty("ListCustomerID").GetValue(obj);
                                                            lstCustomerID = objList as List<int>;
                                                        }
                                                        catch { }

                                                        List<int> lstVendorID = new List<int>();
                                                        try
                                                        {
                                                            var objList = obj.GetType().GetProperty("ListVendorID").GetValue(obj);
                                                            lstVendorID = objList as List<int>;
                                                        }
                                                        catch { }

                                                        List<DTOMessageQueueHelper_COTOContainer> lstDetail = new List<DTOMessageQueueHelper_COTOContainer>();
                                                        try
                                                        {
                                                            var objList = obj.GetType().GetProperty("ListDetail").GetValue(obj);
                                                            lstDetail = objList as List<DTOMessageQueueHelper_COTOContainer>;
                                                        }
                                                        catch { }

                                                        var lstAction = model.WFL_Action.Where(c => c.EventID == evt.Key.EventID && c.UserID.HasValue && c.IsUse && c.SYS_User.IsApproved).Select(c=> new
                                                        {
                                                            c.UserID,
                                                            c.TypeOfActionID,
                                                            c.ID,
                                                            c.SYS_User.Email,
                                                            c.SYS_User.ListCustomerID
                                                        }).ToList();

                                                        foreach (var act in lstAction)
                                                        {
                                                            var ListCustomerID = new List<int>();
                                                            if (!string.IsNullOrEmpty(act.ListCustomerID))
                                                            {
                                                                try
                                                                {
                                                                    ListCustomerID = act.ListCustomerID.Split(',').Select(Int32.Parse).ToList();
                                                                }
                                                                catch { }
                                                            }
                                                            if (lstCustomerID.Any(c => ListCustomerID.Contains(c)) || lstVendorID.Any(c => ListCustomerID.Contains(c)))
                                                            {
                                                                // Nội dung message
                                                                string strContent = string.Empty;
                                                                string strDetail = string.Empty;

                                                                var template = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == act.TypeOfActionID);
                                                                if (template != null && !string.IsNullOrEmpty(template.Template))
                                                                    strContent = template.Template;
                                                                if (template != null && !string.IsNullOrEmpty(template.TemplateDetail))
                                                                    strDetail = template.TemplateDetail;

                                                                // Chi tiết
                                                                string strMessageDetail = string.Empty;
                                                                if (!string.IsNullOrEmpty(strDetail) && lstDetail != null && lstDetail.Count > 0)
                                                                {
                                                                    foreach (var objDetail in lstDetail)
                                                                    {
                                                                        // Nội dung chính
                                                                        strMessageDetail += MailHelper.StringHTML(strDetail, delegate(MailTemplate objMessage)
                                                                        {
                                                                            objMessage.HTML = objMessage.Token;
                                                                            try { objMessage.HTML = objDetail.GetType().GetProperty(objMessage.Token).GetValue(objDetail).ToString(); }
                                                                            catch { }
                                                                        });
                                                                    }
                                                                }

                                                                // Nội dung chính
                                                                string strMessage = MailHelper.StringHTML(strContent, delegate(MailTemplate objMessage)
                                                                {
                                                                    objMessage.HTML = objMessage.Token;
                                                                    try { objMessage.HTML = obj.GetType().GetProperty(objMessage.Token).GetValue(obj).ToString(); }
                                                                    catch { }
                                                                });

                                                                if (string.IsNullOrEmpty(strMessage))
                                                                {
                                                                    var actionName = "";
                                                                    if (act.TypeOfActionID == (int)WFLTypeOfAction.SendMail)
                                                                        actionName = WFLTypeOfAction.SendMail.ToString();
                                                                    if (act.TypeOfActionID == (int)WFLTypeOfAction.SMS)
                                                                        actionName = WFLTypeOfAction.SMS.ToString();
                                                                    if (act.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS)
                                                                        actionName = WFLTypeOfAction.MessageTMS.ToString();
                                                                    strMessage = "Event: " + objEvent.EventName + " hành động " + actionName + " không có template";
                                                                }
                                                                
                                                                // Cập nhật nội dung detail
                                                                if (strMessage.Contains("Details"))
                                                                    strMessage = strMessage.Replace("Details", strMessageDetail);

                                                                WFL_Message message = new WFL_Message();
                                                                message.CreatedBy = Account.UserName;
                                                                message.CreatedDate = DateTime.Now;
                                                                message.ActionID = act.ID;
                                                                message.Message = strMessage;
                                                                message.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageWait;
                                                                // Gửi mail
                                                                if (act.TypeOfActionID == (int)WFLTypeOfAction.SendMail)
                                                                {
                                                                    if (!string.IsNullOrEmpty(act.Email))
                                                                        model.WFL_Message.Add(message);
                                                                }
                                                                // Gửi SMS || Gửi thông báo TMS
                                                                if (act.TypeOfActionID == (int)WFLTypeOfAction.SMS || act.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS)
                                                                    model.WFL_Message.Add(message);
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    model.SaveChanges();
                                }
                            }

                            if (lstSYSEvent.Count > 0)
                                EventCommon.RunEventCommonWorkflowChanged(lstSYSEvent);
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }

        private static void DataEntitiesObject_OnDataEntitiesChanged_Validation(DTOEventSender sender)
        {
            if (sender != null && sender.ListEvent != null && sender.Account != null)
            {
                System.Diagnostics.Debug.WriteLine("Workflow run");
                var Account = (AccountItem)sender.Account;
                try
                {
                    if (sender.ListEvent.Count > 0)
                    {
                        using (var model = new DataEntities())
                        {
                            List<DTOWFLMessageQueue> lstMessage = new List<DTOWFLMessageQueue>();
                            model.EventAccount = Account; model.EventRunning = true;

                            var lstDefine = model.WFL_Define.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new
                            {
                                c.ID,
                                c.ListCustomerID
                            }).ToList();

                            List<int> lstDefineID = lstDefine.Select(c => c.ID).ToList();
                            var lstDefineWFID = model.WFL_DefineWF.Where(c => lstDefineID.Contains(c.DefineID)).Select(c => c.ID).Distinct().ToList();
                            var lstWFEventID = model.WFL_DefineWFEvent.Where(c => lstDefineWFID.Contains(c.DefineWFID)).Select(c => c.WFEventID).Distinct().ToList();

                            var lstTable = model.WFL_WFEventField.Where(c => lstWFEventID.Contains(c.WFEventID) && c.WFL_WFEvent.IsApproved).Select(c => new
                            {
                                c.WFL_Field.TableName,
                                c.WFL_Field.ColumnName,
                                c.WFL_WFEvent.Expression,
                                EventID = c.WFEventID,
                                c.WFL_WFEvent.EventName,
                                c.WFL_Field.ColumnType,
                                c.IsModified,
                            }).ToList();
                            var lstEventTemplate = model.WFL_WFEventTemplate.Where(c => lstWFEventID.Contains(c.WFEventID) && c.WFL_WFEvent.IsApproved).Select(c => new
                            {
                                EventID = c.WFEventID,
                                c.TypeOfActionID,
                                c.WFL_Template.Name,
                                c.WFL_Template.Template,
                                c.WFL_Template.TemplateDetail,
                                c.ID,
                            }).ToList();

                            if (Account.SYSCustomerID < 2)
                            {
                                lstTable = model.WFL_WFEventField.Where(c => c.WFL_WFEvent.IsApproved).Select(c => new
                                {
                                    c.WFL_Field.TableName,
                                    c.WFL_Field.ColumnName,
                                    c.WFL_WFEvent.Expression,
                                    EventID = c.WFL_WFEvent.ID,
                                    c.WFL_WFEvent.EventName,
                                    c.WFL_Field.ColumnType,
                                    c.IsModified,
                                }).ToList();
                                lstEventTemplate = model.WFL_WFEventTemplate.Where(c => c.WFL_WFEvent.IsApproved).Select(c => new
                                {
                                    EventID = c.WFEventID,
                                    c.TypeOfActionID,
                                    c.WFL_Template.Name,
                                    c.WFL_Template.Template,
                                    c.WFL_Template.TemplateDetail,
                                    c.ID,
                                }).ToList();

                                lstWFEventID = lstTable.Select(c => c.EventID).Distinct().ToList();
                            }

                            var lstEvent = model.WFL_WFEvent.Where(c => lstWFEventID.Contains(c.ID)).Select(c => new
                            {
                                ID = c.ID,
                                Code = c.Code,
                                EventName = c.EventName,
                                IsSystem = false,
                            }).ToList();

                            var lstTableName = lstTable.Where(c => c.IsModified).Select(c => c.TableName).Distinct().ToList();
                            var lstChange = sender.ListEvent.Where(c => lstTableName.Contains(c.Table));
                            foreach (var item in lstChange)
                            {
                                var lstEventMatch = lstTable.Where(c => c.TableName == item.Table).GroupBy(c => new
                                {
                                    c.EventID,
                                    c.TableName,
                                    c.Expression,
                                    c.EventName,
                                }).ToList();
                                if (lstEventMatch.Count > 0)
                                {
                                    object obj = null;
                                    obj = GetObjectByTableName(model, item.Table, item.ID, obj, Account);
                                    if (obj != null)
                                    {
                                        // Danh sách event trigger
                                        foreach (var evt in lstEventMatch)
                                        {
                                            if (evt.Any(c => c.IsModified))
                                            {
                                                var lstTemplate = lstEventTemplate.Where(c => c.EventID == evt.Key.EventID).ToList();
                                                var objEvent = lstEvent.FirstOrDefault(c => c.ID == evt.Key.EventID);
                                                // Chỉ xét obj có các field được modified như event khai báo
                                                var lstModifiedColumn = evt.Where(c => c.IsModified).Select(c => c.ColumnName).Distinct().ToList();
                                                int count = 0;
                                                foreach (var itemChange in item.lstProperty)
                                                {
                                                    if (lstModifiedColumn.Count(c => c == itemChange) > 0)
                                                        count++;
                                                }
                                                if (lstTemplate.Count > 0 && lstModifiedColumn.Count == count)
                                                {
                                                    // Ktra công thức event có matching
                                                    var registry = new TypeRegistry();
                                                    foreach (var property in evt)
                                                        registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                                                    var compiler = new CompiledExpression(evt.Key.Expression);
                                                    compiler.TypeRegistry = registry;
                                                    bool res = false;
                                                    try
                                                    {
                                                        res = (bool)compiler.Eval();
                                                    }
                                                    catch
                                                    {
                                                        res = false;
                                                    }

                                                    // Event matching => xử lý send mail || sms || message tms
                                                    if (res)
                                                    {
                                                        List<int> lstCustomerID = new List<int>();
                                                        try
                                                        {
                                                            var objList = obj.GetType().GetProperty("ListCustomerID").GetValue(obj);
                                                            lstCustomerID = objList as List<int>;
                                                        }
                                                        catch { }

                                                        List<int> lstVendorID = new List<int>();
                                                        try
                                                        {
                                                            var objList = obj.GetType().GetProperty("ListVendorID").GetValue(obj);
                                                            lstVendorID = objList as List<int>;
                                                        }
                                                        catch { }

                                                        List<object> lstDetail = new List<object>();
                                                        try
                                                        {
                                                            var objList = obj.GetType().GetProperty("ListDetail").GetValue(obj);
                                                            lstDetail = objList as List<object>;
                                                        }
                                                        catch { }

                                                        var lstAction = model.WFL_DefineWFAction.Where(c => c.WFL_DefineWFEvent.WFL_DefineWF.WFL_Define.SYSCustomerID == Account.SYSCustomerID && c.WFL_DefineWFEvent.WFEventID == evt.Key.EventID && c.UserID.HasValue && c.IsUsed && c.SYS_User.IsApproved).Select(c => new
                                                        {
                                                            EventID = c.WFL_DefineWFEvent.WFEventID,
                                                            ActionID = c.ID,
                                                            TypeOfActionID = c.TypeOfActionID,
                                                            UserID = c.UserID.Value,
                                                            Email = c.SYS_User.Email,
                                                            ListCustomerID = c.SYS_User.ListCustomerID,
                                                        }).ToList();

                                                        foreach (var act in lstAction)
                                                        {
                                                            var ListCustomerID = new List<int>();
                                                            if (!string.IsNullOrEmpty(act.ListCustomerID))
                                                            {
                                                                try
                                                                {
                                                                    ListCustomerID = act.ListCustomerID.Split(',').Select(Int32.Parse).ToList();
                                                                }
                                                                catch { }
                                                            }
                                                            if (lstCustomerID.Count == 0 || lstCustomerID.Any(c => ListCustomerID.Contains(c)) || lstVendorID.Any(c => ListCustomerID.Contains(c)))
                                                            {
                                                                // Chỉ thêm message khi event message cho user ko trùng
                                                                if (lstMessage.Count(c => c.EventID == act.EventID && c.UserID == act.UserID && c.TypeOfActionID == act.TypeOfActionID) == 0)
                                                                {
                                                                    lstMessage.Add(new DTOWFLMessageQueue { EventID = act.EventID, UserID = act.UserID, TypeOfActionID = act.TypeOfActionID });
                                                                    // Nội dung message
                                                                    string strContent = string.Empty;
                                                                    string strDetail = string.Empty;

                                                                    var template = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == act.TypeOfActionID);
                                                                    if (template != null && !string.IsNullOrEmpty(template.Template))
                                                                        strContent = template.Template;
                                                                    if (template != null && !string.IsNullOrEmpty(template.TemplateDetail))
                                                                        strDetail = template.TemplateDetail;

                                                                    // Chi tiết
                                                                    string strMessageDetail = string.Empty;
                                                                    if (!string.IsNullOrEmpty(strDetail) && lstDetail.Count > 0)
                                                                    {
                                                                        foreach (var objDetail in lstDetail)
                                                                        {
                                                                            // Nội dung chính
                                                                            strMessageDetail += MailHelper.StringHTML(strDetail, delegate(MailTemplate objMessage)
                                                                            {
                                                                                objMessage.HTML = objMessage.Token;
                                                                                try { objMessage.HTML = objDetail.GetType().GetProperty(objMessage.Token).GetValue(objDetail).ToString(); }
                                                                                catch { }
                                                                            });
                                                                        }
                                                                    }

                                                                    string strMessage = MailHelper.StringHTML(strContent, delegate(MailTemplate objMessage)
                                                                    {
                                                                        objMessage.HTML = objMessage.Token;
                                                                        try { objMessage.HTML = obj.GetType().GetProperty(objMessage.Token).GetValue(obj).ToString(); }
                                                                        catch { }
                                                                    });
                                                                    if (string.IsNullOrEmpty(strMessage))
                                                                    {
                                                                        var actionName = "";
                                                                        if (act.TypeOfActionID == (int)WFLTypeOfAction.SendMail)
                                                                            actionName = WFLTypeOfAction.SendMail.ToString();
                                                                        if (act.TypeOfActionID == (int)WFLTypeOfAction.SMS)
                                                                            actionName = WFLTypeOfAction.SMS.ToString();
                                                                        if (act.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS)
                                                                            actionName = WFLTypeOfAction.MessageTMS.ToString();
                                                                        strMessage = "Event: " + objEvent.EventName + " hành động " + actionName + " không có template";
                                                                    }

                                                                    // Cập nhật nội dung detail
                                                                    if (strMessage.Contains("[Details]"))
                                                                        strMessage = strMessage.Replace("[Details]", strMessageDetail);

                                                                    WFL_DefineWFMessage message = new WFL_DefineWFMessage();
                                                                    message.CreatedBy = Account.UserName;
                                                                    message.CreatedDate = DateTime.Now;
                                                                    message.DefineWFActionID = act.ActionID;
                                                                    message.Message = strMessage;
                                                                    message.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageWait;
                                                                    // Gửi mail
                                                                    if (act.TypeOfActionID == (int)WFLTypeOfAction.SendMail)
                                                                    {
                                                                        if (!string.IsNullOrEmpty(act.Email))
                                                                            model.WFL_DefineWFMessage.Add(message);
                                                                    }
                                                                    // Gửi SMS || Gửi thông báo TMS
                                                                    if (act.TypeOfActionID == (int)WFLTypeOfAction.SMS || act.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS)
                                                                        model.WFL_DefineWFMessage.Add(message);
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            model.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }


        private static object GetObjectByTableName(DataEntities model, string tableName, int id, object obj, AccountItem Account)
        {
            switch (tableName)
            {
                #region KPI_KPITime
                case "KPI_KPITime":
                    var objKPITime = model.KPI_KPITime.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_KPITime
                    {
                        ID = c.ID,
                        CustomerID = c.CustomerID,
                        OrderID = c.OrderID,
                        DITOMasterID = c.DITOMasterID,
                        COTOMasterID = c.COTOMasterID,
                        DateData = c.DateData,
                        OrderGroupProductID = c.OrderGroupProductID,
                        OrderContainerID = c.OrderContainerID,
                        DITOGroupProductID = c.DITOGroupProductID,
                        DateFromCome= c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        DateDN = c.DateDN,
                        DateInvoice = c.DateInvoice,
                        KPIDate = c.KPIDate,
                        KPIID = c.KPIID,
                        IsKPI = c.IsKPI,
                        Zone = c.Zone,
                        LeadTime = c.LeadTime,
                        ReasonID = c.ReasonID,
                        Note = c.Note,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        DateRequest = c.DateRequest,
                        CustomerName = c.CUS_Customer.CustomerName,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                        LocationFrom = c.ORD_GroupProduct.LocationFromID.HasValue ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Address : string.Empty,
                        LocationTo = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : string.Empty,
                        Ton = c.OPS_DITOGroupProduct.Ton,
                        CBM = c.OPS_DITOGroupProduct.CBM,
                        Quantity = c.OPS_DITOGroupProduct.Quantity,
                    }).FirstOrDefault();

                    if (objKPITime != null)
                    {
                        objKPITime.CreatedDateTemp = objKPITime.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objKPITime.ModifiedDateTemp = objKPITime.ModifiedDate.HasValue ? objKPITime.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objKPITime.DateFromComeTemp = objKPITime.DateFromCome.HasValue ? objKPITime.DateFromCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objKPITime.DateFromLoadStartTemp = objKPITime.DateFromLoadStart.HasValue ? objKPITime.DateFromLoadStart.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objKPITime.DateFromLoadEndTemp = objKPITime.DateFromLoadEnd.HasValue ? objKPITime.DateFromLoadEnd.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objKPITime.DateFromLeaveTemp = objKPITime.DateFromLeave.HasValue ? objKPITime.DateFromLeave.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objKPITime.DateToComeTemp = objKPITime.DateToCome.HasValue ? objKPITime.DateToCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objKPITime.DateToLoadStartTemp = objKPITime.DateToLoadStart.HasValue ? objKPITime.DateToLoadStart.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objKPITime.DateToLoadEndTemp = objKPITime.DateToLoadEnd.HasValue ? objKPITime.DateToLoadEnd.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objKPITime.DateToLeaveTemp = objKPITime.DateToLeave.HasValue ? objKPITime.DateToLeave.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objKPITime.ListCustomerID = new List<int>();
                        objKPITime.ListCustomerID.Add(objKPITime.CustomerID);
                        obj = objKPITime;
                    }

                    break;
                #endregion

                #region ORD_Order
                case "ORD_Order":
                    var objOrder = model.ORD_Order.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_Order
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        OrderRefID = c.OrderRefID,
                        SYSCustomerID = c.SYSCustomerID,
                        Code = c.Code,
                        CustomerID = c.CustomerID,
                        ServiceOfOrderID = c.ServiceOfOrderID,
                        TransportModeID = c.TransportModeID,
                        TypeOfContractID = c.TypeOfContractID,
                        ContractID = c.ContractID,
                        TypeOfOrderID = c.TypeOfOrderID,
                        StatusOfOrderID = c.StatusOfOrderID,
                        StatusOfPlanID = c.StatusOfPlanID,
                        RequestDate = c.RequestDate,
                        LocationFromID = c.LocationFromID,
                        ETD = c.ETD,
                        LocationToID = c.LocationToID,
                        ETA = c.ETA,
                        DateConfig = c.DateConfig,
                        CutOffTime = c.CutOffTime,
                        LoadingTime = c.LoadingTime,
                        PartnerID = c.PartnerID,
                        LocationDepotID = c.LocationDepotID,
                        LocationDepotReturnID = c.LocationDepotReturnID,
                        VesselNo = c.VesselNo,
                        VesselName = c.VesselName,
                        TripNo = c.TripNo,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        IsOPS = c.IsOPS,
                        IsClosed = c.IsClosed,
                        IsHot = c.IsHot,
                        BiddingNo = c.BiddingNo,
                        AllowCoLoad = c.AllowCoLoad,
                        LeadTime = c.LeadTime,
                        Note = c.Note,
                        ExternalCode = c.ExternalCode,
                        ExternalDate = c.ExternalDate,
                        UserDefine1 = c.UserDefine1,
                        UserDefine2 = c.UserDefine2,
                        UserDefine3 = c.UserDefine3,
                        UserDefine4 = c.UserDefine4,
                        UserDefine5 = c.UserDefine5,
                        UserDefine6 = c.UserDefine6,
                        UserDefine7 = c.UserDefine7,
                        UserDefine8 = c.UserDefine8,
                        UserDefine9 = c.UserDefine9,
                        DateShipCome = c.DateShipCome,
                        DateDocument = c.DateDocument,
                        DateInspect = c.DateInspect,
                        DateGetEmpty = c.DateGetEmpty,
                        DateReturnEmpty = c.DateReturnEmpty,
                        DateLoading = c.DateLoading,
                        DateUnloading = c.DateUnloading,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        ETARequest = c.ETARequest,
                        TextFrom = c.TextFrom,
                        TextTo = c.TextTo,
                        ETDEnd = c.ETDEnd,
                        ETAEnd = c.ETAEnd,
                        OrderCode = c.Code,
                        StatusOfOrderName = c.SYS_Var.ValueOfVar,
                        StatusOfPlanName = c.SYS_Var1.ValueOfVar,
                        TotalTon = model.OPS_DITOGroupProduct.Count(d => d.ORD_GroupProduct.OrderID == id) > 0 ? model.OPS_DITOGroupProduct.Where(d => d.ORD_GroupProduct.OrderID == id).Sum(d => d.Ton) : c.ORD_GroupProduct.Sum(d => d.Ton),
                        TotalCBM = model.OPS_DITOGroupProduct.Count(d => d.ORD_GroupProduct.OrderID == id) > 0 ? model.OPS_DITOGroupProduct.Where(d => d.ORD_GroupProduct.OrderID == id).Sum(d => d.CBM) : c.ORD_GroupProduct.Sum(d => d.CBM),
                    }).FirstOrDefault();
                    if (objOrder != null)
                    {
                        objOrder.CreatedDateTemp = objOrder.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objOrder.ModifiedDateTemp = objOrder.ModifiedDate.HasValue ? objOrder.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objOrder.ETDTemp = objOrder.ETD.HasValue ? objOrder.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objOrder.ETATemp = objOrder.ETA.HasValue ? objOrder.ETA.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objOrder.DateConfigTemp = objOrder.DateConfig.HasValue ? objOrder.DateConfig.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objOrder.ListCustomerID = new List<int>();
                        objOrder.ListCustomerID.Add(objOrder.CustomerID);
                        obj = objOrder;
                    }
                    break;
                #endregion

                #region OPS_DITOMaster
                case "OPS_DITOMaster":
                    var objDITOMaster = model.OPS_DITOMaster.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_DITOMaster
                    {
                        ID = c.ID,
                        SYSCustomerID = c.SYSCustomerID,
                        Code = c.Code,
                        VehicleID = c.VehicleID,
                        VendorOfVehicleID = c.VendorOfVehicleID,
                        DriverID1 = c.DriverID1,
                        DriverID2 = c.DriverID2,
                        ApprovedBy = c.ApprovedBy,
                        ApprovedDate = c.ApprovedDate,
                        StatusOfDITOMasterID = c.StatusOfDITOMasterID,
                        TypeOfDITOMasterID = c.TypeOfDITOMasterID,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        SortOrder = c.SortOrder,
                        ETD = c.ETD,
                        ETA = c.ETA,
                        IsRouteVendor = c.IsRouteVendor,
                        IsRouteCustomer = c.IsRouteCustomer,
                        IsLoading = c.IsLoading,
                        IsHot = c.IsHot,
                        RateTime = c.RateTime,
                        IsBidding = c.IsBidding,
                        Note = c.Note,
                        DriverName1 = c.DriverName1,
                        DriverName2 = c.DriverName2,
                        DriverTel1 = c.DriverTel1,
                        DriverTel2 = c.DriverTel2,
                        DriverCard1 = c.DriverCard1,
                        DriverCard2 = c.DriverCard2,
                        KM = c.KM,
                        TransportModeID = c.TransportModeID,
                        TypeOfOrderID = c.TypeOfOrderID,
                        ContractID = c.ContractID,
                        AllowCoLoad = c.AllowCoLoad,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        CATRoutingID = c.CATRoutingID,
                        CUSRoutingID = c.CUSRoutingID,
                        DateConfig = c.DateConfig,
                        KMStart = c.KMStart,
                        KMEnd = c.KMEnd,
                        Note1 = c.Note1,
                        Note2 = c.Note2,
                        TotalLocation = c.TotalLocation,
                        TypeOfPaymentDITOMasterID = c.TypeOfPaymentDITOMasterID,
                        PayVendorModified = c.PayVendorModified,
                        PayVendorNote = c.PayVendorNote,
                        PayUserModified = c.PayUserModified,
                        PayUserNote = c.PayUserNote,
                        StatusOfDITOMasterName = c.SYS_Var.ValueOfVar,
                        TOMasterCode = c.Code,
                        VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                    }).FirstOrDefault();
                    if (objDITOMaster != null)
                    {
                        objDITOMaster.CreatedDateTemp = objDITOMaster.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objDITOMaster.ModifiedDateTemp = objDITOMaster.ModifiedDate.HasValue ? objDITOMaster.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOMaster.ETDTemp = objDITOMaster.ETD.HasValue ? objDITOMaster.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOMaster.ETATemp = objDITOMaster.ETA.HasValue ? objDITOMaster.ETA.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOMaster.DateConfigTemp = objDITOMaster.DateConfig.HasValue ? objDITOMaster.DateConfig.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOMaster.ListCustomerID = new List<int>();
                        objDITOMaster.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == objDITOMaster.ID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                        obj = objDITOMaster;
                    }

                    break;
                #endregion

                #region FLM_AssetTimeSheet
                case "FLM_AssetTimeSheet":
                    var objAssetTimeSheet = model.FLM_AssetTimeSheet.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_AssetTimeSheet
                        {
                            ID = c.ID,
                            AssetID = c.AssetID,
                            StatusOfAssetTimeSheetID = c.StatusOfAssetTimeSheetID,
                            ReferID = c.ReferID,
                            DateFrom = c.DateFrom,
                            DateTo = c.DateTo,
                            Note = c.Note,
                            CreatedDate = c.CreatedDate,
                            CreatedBy = c.CreatedBy,
                            ModifiedDate = c.ModifiedDate,
                            ModifiedBy = c.ModifiedBy,
                            DateFromActual = c.DateFromActual,
                            DateToActual = c.DateToActual,
                            TypeOfAssetTimeSheetID = c.TypeOfAssetTimeSheetID,
                            StatusOfAssetTimeSheetName = c.SYS_Var.ValueOfVar,
                            TypeOfAssetTimeSheetName = c.SYS_Var1.ValueOfVar,
                            VehicleCode = c.FLM_Asset.CAT_Vehicle.RegNo,
                            DriverName = c.FLM_AssetTimeSheetDriver.Count > 0 ? c.FLM_AssetTimeSheetDriver.FirstOrDefault().FLM_Driver.CAT_Driver.LastName + " " + c.FLM_AssetTimeSheetDriver.FirstOrDefault().FLM_Driver.CAT_Driver.FirstName : "",
                            DITOMasterID = c.ReferID,
                            TOMasterCode = c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster && c.ReferID > 0 && model.OPS_DITOMaster.Count(d => d.ID == c.ReferID) > 0 ? model.OPS_DITOMaster.FirstOrDefault(d => d.ID == c.ReferID).Code : "",
                        }).FirstOrDefault();
                    if (objAssetTimeSheet != null)
                    {
                        objAssetTimeSheet.CreatedDateTemp = objAssetTimeSheet.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objAssetTimeSheet.ModifiedDateTemp = objAssetTimeSheet.ModifiedDate.HasValue ? objAssetTimeSheet.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objAssetTimeSheet.DateFromTemp = objAssetTimeSheet.DateFrom.HasValue ? objAssetTimeSheet.DateFrom.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objAssetTimeSheet.DateToTemp = objAssetTimeSheet.DateTo.HasValue ? objAssetTimeSheet.DateTo.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objAssetTimeSheet.DateFromActualTemp = objAssetTimeSheet.DateFromActual.HasValue ? objAssetTimeSheet.DateFromActual.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objAssetTimeSheet.DateToActualTemp = objAssetTimeSheet.DateToActual.HasValue ? objAssetTimeSheet.DateToActual.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objAssetTimeSheet.ListCustomerID = new List<int>();
                        objAssetTimeSheet.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == objAssetTimeSheet.ReferID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                        obj = objAssetTimeSheet;
                    }

                    break;
                #endregion

                #region OPS_DITOGroupProduct
                case "OPS_DITOGroupProduct":
                    var objDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_DITOGroupProduct
                    {
                        ID = c.ID,
                        DITOMasterID = c.DITOMasterID,
                        CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                        OrderGroupProductID = c.OrderGroupProductID,
                        LockedBy = c.LockedBy,
                        Ton = c.Ton,
                        CBM = c.CBM,
                        Quantity = c.Quantity,
                        TonTranfer = c.TonTranfer,
                        CBMTranfer = c.CBMTranfer,
                        QuantityTranfer = c.QuantityTranfer,
                        TonBBGN = c.TonBBGN,
                        CBMBBGN = c.CBMBBGN,
                        QuantityBBGN = c.QuantityBBGN,
                        QuantityLoading = c.QuantityLoading,
                        Note = c.Note == null ? string.Empty : c.Note,
                        IsInput = c.IsInput,
                        GroupSort = c.GroupSort,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        DNCode = c.DNCode == null ? string.Empty : c.DNCode,
                        DITOGroupProductStatusID = c.DITOGroupProductStatusID,
                        DateFromCome = c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        IsOrigin = c.IsOrigin,
                        Note1 = c.Note1 == null ? string.Empty : c.Note1,
                        Note2 = c.Note2 == null ? string.Empty : c.Note2,
                        DITOGroupProductStatusPODID = c.DITOGroupProductStatusPODID,
                        InvoiceNote = c.InvoiceNote == null ? string.Empty : c.InvoiceNote,
                        InvoiceBy = c.InvoiceBy == null ? string.Empty : c.InvoiceBy,
                        InvoiceDate = c.InvoiceDate,
                        DateDN = c.DateDN,
                        CUSRoutingID = c.CUSRoutingID,
                        StatusOfDITOMasterID = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.StatusOfDITOMasterID : -1,
                        TOMasterCode = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.Code : string.Empty,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                        SOCode = c.ORD_GroupProduct.SOCode == null ? string.Empty : c.ORD_GroupProduct.SOCode,
                        LocationFromCode = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Code : "",
                        LocationFromName = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Location : "",
                        LocationFromAddress = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Address : "",
                        LocationToCode = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Code : "",
                        LocationToName = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location : "",
                        LocationToAddress = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : "",
                    }).FirstOrDefault();
                    if (objDITOGroupProduct != null)
                    {
                        objDITOGroupProduct.CreatedDateTemp = objDITOGroupProduct.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objDITOGroupProduct.ModifiedDateTemp = objDITOGroupProduct.ModifiedDate.HasValue ? objDITOGroupProduct.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.DateFromComeTemp = objDITOGroupProduct.DateFromCome.HasValue ? objDITOGroupProduct.DateFromCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.DateFromLoadStartTemp = objDITOGroupProduct.DateFromLoadStart.HasValue ? objDITOGroupProduct.DateFromLoadStart.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.DateFromLoadEndTemp = objDITOGroupProduct.DateFromLoadEnd.HasValue ? objDITOGroupProduct.DateFromLoadEnd.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.DateFromLeaveTemp = objDITOGroupProduct.DateFromLeave.HasValue ? objDITOGroupProduct.DateFromLeave.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.DateToComeTemp = objDITOGroupProduct.DateToCome.HasValue ? objDITOGroupProduct.DateToCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.DateToLoadStartTemp = objDITOGroupProduct.DateToLoadStart.HasValue ? objDITOGroupProduct.DateToLoadStart.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.DateToLoadEndTemp = objDITOGroupProduct.DateToLoadEnd.HasValue ? objDITOGroupProduct.DateToLoadEnd.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.DateToLeaveTemp = objDITOGroupProduct.DateToLeave.HasValue ? objDITOGroupProduct.DateToLeave.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.InvoiceDateTemp = objDITOGroupProduct.InvoiceDate.HasValue ? objDITOGroupProduct.InvoiceDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.DateDNTemp = objDITOGroupProduct.DateDN.HasValue ? objDITOGroupProduct.DateDN.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOGroupProduct.ListCustomerID = new List<int>();
                        objDITOGroupProduct.ListCustomerID.Add(objDITOGroupProduct.CustomerID);
                        obj = objDITOGroupProduct;
                    }

                    break;
                #endregion

                #region OPS_DITORate
                case "OPS_DITORate":
                    var objDITORate = model.OPS_DITORate.Where(c => c.ID == id && c.VendorID.HasValue && c.VendorID != Account.SYSCustomerID).Select(c => new DTOMessageQueueHelper_DITORate
                    {
                        ID = c.ID,
                        DITOMasterID = c.DITOMasterID,
                        VendorID = c.VendorID,
                        SortOrder = c.SortOrder,
                        IsAccept = c.IsAccept,
                        IsSend = c.IsSend,
                        Debit = c.Debit,
                        ReasonID = c.ReasonID,
                        Reason = c.Reason,
                        IsManual = c.IsManual,
                        FirstRateTime = c.FirstRateTime,
                        LastRateTime = c.LastRateTime,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        StatusOfDITOMasterID = c.OPS_DITOMaster.StatusOfDITOMasterID,
                        TOMasterCode = c.OPS_DITOMaster.Code,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.ShortName
                    }).FirstOrDefault();
                    if (objDITORate != null)
                    {
                        objDITORate.CreatedDateTemp = objDITORate.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objDITORate.ModifiedDateTemp = objDITORate.ModifiedDate.HasValue ? objDITORate.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITORate.FirstRateTimeTemp = objDITORate.FirstRateTime.HasValue ? objDITORate.FirstRateTime.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITORate.LastRateTimeTemp = objDITORate.LastRateTime.HasValue ? objDITORate.LastRateTime.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITORate.ListCustomerID = new List<int>();
                        objDITORate.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == objDITORate.DITOMasterID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                        objDITORate.ListVendorID = new List<int>();
                        objDITORate.ListVendorID.Add(objDITORate.VendorID.Value);
                        objDITORate.ListDetail = new List<DTOMessageQueueHelper_DITOGroupProduct>();
                        objDITORate.ListDetail = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == objDITORate.DITOMasterID).Select(c => new DTOMessageQueueHelper_DITOGroupProduct
                            {
                                SOCode = c.ORD_GroupProduct.SOCode,
                                DNCode = c.DNCode,
                                Note = c.Note,
                                Note1 = c.Note1,
                                Note2 = c.Note2,
                                Ton = c.Ton,
                                CBM = c.CBM,
                                Quantity = c.Quantity,
                                TonTranfer = c.TonTranfer,
                                CBMTranfer = c.CBMTranfer,
                                QuantityTranfer = c.QuantityTranfer,
                                OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                                LocationFromCode = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Code : "",
                                LocationFromName = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Location : "",
                                LocationFromAddress = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Address : "",
                                LocationToCode = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Code : "",
                                LocationToName = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location : "",
                                LocationToAddress = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : "",
                            }).ToList();
                        obj = objDITORate;
                    }
                    break;
                #endregion

                #region OPS_COTORate
                case "OPS_COTORate":
                    var objCOTORate = model.OPS_COTORate.Where(c => c.ID == id && c.VendorID.HasValue && c.VendorID != Account.SYSCustomerID).Select(c => new DTOMessageQueueHelper_COTORate
                    {
                        ID = c.ID,
                        COTOMasterID = c.COTOMasterID,
                        VendorID = c.VendorID,
                        SortOrder = c.SortOrder,
                        IsAccept = c.IsAccept,
                        IsSend = c.IsSend,
                        Debit = c.Debit,
                        ReasonID = c.ReasonID,
                        Reason = c.Reason,
                        IsManual = c.IsManual,
                        FirstRateTime = c.FirstRateTime,
                        LastRateTime = c.LastRateTime,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        StatusOfCOTOMasterID = c.OPS_COTOMaster.StatusOfCOTOMasterID,
                        TOMasterCode = c.OPS_COTOMaster.Code,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.ShortName,
                        ETD = c.OPS_COTOMaster.ETD,
                        ETA = c.OPS_COTOMaster.ETA,
                        DriverTel = c.OPS_COTOMaster.DriverTel1,
                        Note = c.Note,
                    }).FirstOrDefault();
                    if (objCOTORate != null)
                    {
                        objCOTORate.CreatedDateTemp = objCOTORate.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objCOTORate.ModifiedDateTemp = objCOTORate.ModifiedDate.HasValue ? objCOTORate.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objCOTORate.FirstRateTimeTemp = objCOTORate.FirstRateTime.HasValue ? objCOTORate.FirstRateTime.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objCOTORate.LastRateTimeTemp = objCOTORate.LastRateTime.HasValue ? objCOTORate.LastRateTime.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objCOTORate.ETDTemp = objCOTORate.ETD.HasValue ? objCOTORate.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objCOTORate.ETATemp = objCOTORate.ETA.HasValue ? objCOTORate.ETA.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objCOTORate.ListCustomerID = new List<int>();
                        objCOTORate.ListCustomerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCOTORate.COTOMasterID).Select(c => c.OPS_Container.ORD_Container.ORD_Order.CustomerID).Distinct().ToList();
                        objCOTORate.ListVendorID = new List<int>();
                        objCOTORate.ListVendorID.Add(objCOTORate.VendorID.Value);
                        objCOTORate.ListDetail = new List<DTOMessageQueueHelper_COTOContainer>();
                        var lstDetail = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCOTORate.COTOMasterID && c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID > 0).OrderBy(c => c.SortOrder).Select(c => new 
                            {
                                OrderID = c.OPS_Container.ORD_Container.OrderID,
                                ORDContainerID = c.OPS_Container.ContainerID,
                                OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                                ContainerNo = c.OPS_Container.ContainerNo != null ? c.OPS_Container.ContainerNo : "",
                                SealNo1 = c.OPS_Container.SealNo1 != null ? c.OPS_Container.SealNo1 : "",
                                SealNo2 = c.OPS_Container.SealNo2 != null ? c.OPS_Container.SealNo2 : "",
                                UserDefine1 = c.OPS_Container.ORD_Container.ORD_Order.UserDefine1 != null ? c.OPS_Container.ORD_Container.ORD_Order.UserDefine1 : "",
                                UserDefine2 = c.OPS_Container.ORD_Container.ORD_Order.UserDefine2 != null ? c.OPS_Container.ORD_Container.ORD_Order.UserDefine2 : "",
                                UserDefine3 = c.OPS_Container.ORD_Container.ORD_Order.UserDefine3 != null ? c.OPS_Container.ORD_Container.ORD_Order.UserDefine3 : "",
                                LocationFromID = c.OPS_Container.ORD_Container.LocationFromID,
                                LocationFromCode = c.LocationFromID > 0 ? c.OPS_Container.ORD_Container.CUS_Location2.CAT_Location.Code : "",
                                LocationFromName = c.LocationFromID > 0 ? c.OPS_Container.ORD_Container.CUS_Location2.CAT_Location.Location : "",
                                LocationFromAddress = c.LocationFromID > 0 ? c.OPS_Container.ORD_Container.CUS_Location2.CAT_Location.Address : "",
                                LocationToCode = c.LocationFromID > 0 ? c.OPS_Container.ORD_Container.CUS_Location3.CAT_Location.Code : "",
                                LocationToName = c.LocationFromID > 0 ? c.OPS_Container.ORD_Container.CUS_Location3.CAT_Location.Location : "",
                                LocationToAddress = c.LocationFromID > 0 ? c.OPS_Container.ORD_Container.CUS_Location3.CAT_Location.Address : "",
                                ETA = c.OPS_Container.ORD_Container.ETA,
                                ETD = c.OPS_Container.ORD_Container.ETD,
                                TOMasterCode = objCOTORate.TOMasterCode,
                                VehicleCode = c.COTOMasterID > 0 && c.OPS_COTOMaster.VehicleID > 0 ? c.OPS_COTOMaster.CAT_Vehicle.RegNo : "",
                                DriverTel = objCOTORate.DriverTel != null ? objCOTORate.DriverTel : "",
                                CustomerCode = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                                CustomerName = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.CustomerName,
                                CustomerShortName = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.ShortName,
                                VesselNo = c.OPS_Container.ORD_Container.ORD_Order.VesselNo != null ? c.OPS_Container.ORD_Container.ORD_Order.VesselNo : "",
                                VesselName = c.OPS_Container.ORD_Container.ORD_Order.VesselName != null ? c.OPS_Container.ORD_Container.ORD_Order.VesselName : "",
                                TripNo = c.OPS_Container.ORD_Container.ORD_Order.TripNo!= null ? c.OPS_Container.ORD_Container.ORD_Order.TripNo : "",
                                PackingCode = c.OPS_Container.ORD_Container.CAT_Packing.Code,
                                ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID,
                                ServiceOfOrderName = c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.Name,
                                Ton = c.OPS_Container.ORD_Container.Ton,
                                LocationDepotCode = c.OPS_Container.LocationDepotID > 0 ? c.OPS_Container.CAT_Location.Code : c.OPS_Container.LocationDepotReturnID > 0 ? c.OPS_Container.CAT_Location1.Code : "",
                                LocationDepotName = c.OPS_Container.LocationDepotID > 0 ? c.OPS_Container.CAT_Location.Location : c.OPS_Container.LocationDepotReturnID > 0 ? c.OPS_Container.CAT_Location1.Location : "",
                                LocationDepotAddress = c.OPS_Container.LocationDepotID > 0 ? c.OPS_Container.CAT_Location.Address : c.OPS_Container.LocationDepotReturnID > 0 ? c.OPS_Container.CAT_Location1.Address : "",
                            }).Distinct().ToList();

                        foreach (var item in lstDetail)
                        {
                            DTOMessageQueueHelper_COTOContainer objDetail = new DTOMessageQueueHelper_COTOContainer
                            {
                                ORDContainerID = item.ORDContainerID,
                                OrderCode = item.OrderCode,
                                ContainerNo = item.ContainerNo,
                                SealNo1 = item.SealNo1,
                                SealNo2 = item.SealNo2,
                                UserDefine1 = item.UserDefine1,
                                UserDefine2 = item.UserDefine2,
                                UserDefine3 = item.UserDefine3,
                                TOMasterCode = item.TOMasterCode,
                                VehicleCode = item.VehicleCode,
                                DriverTel = item.DriverTel,
                                
                                CustomerCode = item.CustomerCode,
                                CustomerName = item.CustomerName,
                                CustomerShortName = item.CustomerShortName,
                                VesselNo = item.VesselNo,
                                VesselName = item.VesselName,
                                TripNo = item.TripNo,
                                PackingCode = item.PackingCode,
                                LocationFromCode = item.LocationFromCode,
                                LocationFromName = item.LocationFromName,
                                LocationFromAddress = item.LocationFromAddress,
                                LocationToCode = item.LocationToCode,
                                LocationToName = item.LocationToName,
                                LocationToAddress = item.LocationToAddress,
                                ETA = item.ETA,
                                ETD = item.ETD,
                                VendorCode = objCOTORate.VendorCode,
                                VendorName = objCOTORate.VendorName,
                                ServiceOfOrderName = item.ServiceOfOrderName,
                                LocationDepotCode = item.LocationDepotCode,
                                LocationDepotName = item.LocationDepotName,
                                LocationDepotAddress = item.LocationDepotAddress,
                                Ton = item.Ton,
                                IMLocationFromCode = string.Empty,
                                IMLocationFromName = string.Empty,
                                IMLocationFromAddress = string.Empty,
                                IMLocationToCode = string.Empty,
                                IMLocationToName = string.Empty,
                                IMLocationToAddress = string.Empty,
                                EXLocationFromCode = string.Empty,
                                EXLocationFromName = string.Empty,
                                EXLocationFromAddress = string.Empty,
                                EXLocationToCode = string.Empty,
                                EXLocationToName = string.Empty,
                                EXLocationToAddress = string.Empty,
                                IMETATemp = string.Empty,
                                IMETDTemp = string.Empty,
                                EXETATemp = string.Empty,
                                EXETDTemp = string.Empty
                            };
                            var lstGroup = model.ORD_GroupProduct.Where(c => c.OrderID == item.OrderID).Select(c => new
                            {
                                Code = c.GroupOfProductID > 0 ? c.CUS_GroupOfProduct.Code : "",
                                GroupName = c.GroupOfProductID > 0 ? c.CUS_GroupOfProduct.GroupName : "",
                                c.Description
                            }).ToList();
                            if (lstGroup.Count > 0)
                            {
                                objDetail.GroupOfProductCode = string.Join(", ", lstGroup.Select(c => c.Code).Distinct().ToList());
                                objDetail.GroupOfProductName = string.Join(", ", lstGroup.Select(c => c.GroupName).Distinct().ToList());
                            }
                            else
                            {
                                objDetail.GroupOfProductCode = string.Empty;
                                objDetail.GroupOfProductName = string.Empty;
                            }
                            if (item.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)
                            {
                                objDetail.IMETA = item.ETA;
                                objDetail.IMETD = item.ETD;
                                objDetail.IMLocationFromCode = item.LocationFromCode;
                                objDetail.IMLocationFromName = item.LocationFromName;
                                objDetail.IMLocationFromAddress = item.LocationFromAddress;
                                objDetail.IMLocationToCode = item.LocationToCode;
                                objDetail.IMLocationToName = item.LocationToName;
                                objDetail.IMLocationToAddress = item.LocationToAddress;
                                objDetail.IMETDTemp = item.ETD.HasValue ? item.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                                objDetail.IMETATemp = item.ETA.HasValue ? item.ETA.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            }
                            else
                            {
                                objDetail.EXETA = item.ETA;
                                objDetail.EXETD = item.ETD;
                                objDetail.EXLocationFromCode = item.LocationFromCode;
                                objDetail.EXLocationFromName = item.LocationFromName;
                                objDetail.EXLocationFromAddress = item.LocationFromAddress;
                                objDetail.EXLocationToCode = item.LocationToCode;
                                objDetail.EXLocationToName = item.LocationToName;
                                objDetail.EXLocationToAddress = item.LocationToAddress;
                                objDetail.EXETDTemp = item.ETD.HasValue ? item.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                                objDetail.EXETATemp = item.ETA.HasValue ? item.ETA.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            }
                            objDetail.ETDTemp = item.ETD.HasValue ? item.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            objDetail.ETATemp = item.ETA.HasValue ? item.ETA.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            objCOTORate.ListDetail.Add(objDetail);
                        }
                        obj = objCOTORate;
                    }
                    break;
                #endregion

                #region CAT_ContractTerm
                case "CAT_ContractTerm":
                    var objContractTerm = model.CAT_ContractTerm.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_ContractTerm
                    {
                        ID = c.ID,
                        ContractID = c.ContractID,
                        MaterialID = c.MaterialID,
                        PriceContract = c.PriceContract.HasValue ? c.PriceContract.Value : 0,
                        PriceCurrent = c.PriceCurrent.HasValue ? c.PriceCurrent.Value : 0,
                        Note = c.Note,
                        ExprInput = c.ExprInput,
                        IsWarning = c.IsWarning,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        MaterialCode = c.FLM_Material.Code,
                        ContractNo = c.CAT_Contract.ContractNo,
                        CustomerID = c.CAT_Contract.CustomerID.HasValue ? c.CAT_Contract.CustomerID.Value : -1,
                        CustomerName = c.CAT_Contract.CustomerID.HasValue ? c.CAT_Contract.CUS_Customer.CustomerName : string.Empty,
                    }).FirstOrDefault();

                    if (objContractTerm != null)
                    {
                        objContractTerm.CreatedDateTemp = objContractTerm.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objContractTerm.ModifiedDateTemp = objContractTerm.ModifiedDate.HasValue ? objContractTerm.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objContractTerm.PriceContractTemp = objContractTerm.PriceContract.ToString("0");
                        objContractTerm.PriceCurrentTemp = objContractTerm.PriceCurrent.ToString("0");
                        objContractTerm.ListCustomerID = new List<int>();
                        objContractTerm.ListCustomerID.Add(objContractTerm.CustomerID);
                        obj = objContractTerm;
                    }
                    break;
                #endregion

                #region OPS_RouteProblem
                case "OPS_RouteProblem":
                    var objRouteProblem = model.OPS_RouteProblem.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_RouteProblem
                    {
                        ID = c.ID,
                        RegNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                        LastName = c.DriverID > 0 ? c.FLM_Driver.CAT_Driver.LastName : "",
                        FirstName = c.DriverID > 0 ? c.FLM_Driver.CAT_Driver.FirstName : "",
                        TypeOfRouteProblemCode = c.OPS_TypeOfRouteProblem.Code,
                        TypeOfRouteProblemName = c.OPS_TypeOfRouteProblem.TypeName,
                        Lat = c.Lat,
                        Lng = c.Lng,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        DateStart = c.DateStart,
                        DateEnd = c.DateEnd
                    }).FirstOrDefault();

                    if (objRouteProblem != null)
                    {
                        objRouteProblem.CreatedDateTemp = objRouteProblem.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objRouteProblem.DateStartTemp = objRouteProblem.DateStart.ToString("dd/MM/yyyy HH:mm");
                        objRouteProblem.DateEndTemp = objRouteProblem.DateEnd.ToString("dd/MM/yyyy HH:mm");
                        objRouteProblem.ModifiedDateTemp = objRouteProblem.ModifiedDate.HasValue ? objRouteProblem.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objRouteProblem.ListCustomerID = new List<int>();
                        obj = objRouteProblem;
                    }
                    break;
                #endregion

                #region CAT_Trouble
                case "CAT_Trouble":
                    var objTrouble = model.CAT_Trouble.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_Trouble
                    {
                        ID = c.ID,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        Description = c.Description,
                        Cost = c.Cost,
                        TOMasterCode = c.DITOMasterID > 0 ? c.OPS_DITOMaster.Code : c.COTOMasterID > 0 ? c.OPS_COTOMaster.Code : "",
                        DITOMasterID = c.DITOMasterID,
                        COTOMasterID = c.COTOMasterID,
                        TroubleCostStatusID = c.TroubleCostStatusID,
                        TroubleName = c.CAT_GroupOfTrouble.Name,
                    }).FirstOrDefault();

                    if (objTrouble != null)
                    {
                        objTrouble.CostTemp = objTrouble.Cost.ToString("0");
                        objTrouble.CreatedDateTemp = objTrouble.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objTrouble.ModifiedDateTemp = objTrouble.ModifiedDate.HasValue ? objTrouble.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objTrouble.ListCustomerID = new List<int>();
                        if (objTrouble.DITOMasterID > 0)
                            objTrouble.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == objTrouble.DITOMasterID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                        obj = objTrouble;
                    }
                    break;
                #endregion

                #region CAT_Comment
                case "CAT_Comment":
                    var objComment = model.CAT_Comment.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_Comment
                    {
                        ID = c.ID,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate= c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        OrderCode = model.ORD_Order.Count(d => d.ID == c.ReferID) > 0 ? model.ORD_Order.FirstOrDefault(d => d.ID == c.ReferID).Code : "",
                        ReferID = c.ReferID,
                    }).FirstOrDefault();

                    if (objComment != null)
                    {
                        objComment.CreatedDateTemp = objComment.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objComment.ModifiedDateTemp = objComment.ModifiedDate.HasValue ? objComment.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objComment.ListCustomerID = new List<int>();
                        objComment.ListCustomerID.Add(objComment.ReferID);
                        obj = objComment;
                    }
                    break;
                #endregion

                #region OPS_DITOLocation
                case "OPS_DITOLocation":
                    var objDITOLocation = model.OPS_DITOLocation.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_DITOLocation
                    {
                        ID = c.ID,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        TOMasterCode = c.OPS_DITOMaster.Code,
                        VehicleCode = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                        DriverName = c.OPS_DITOMaster.DriverID1 > 0 ? c.OPS_DITOMaster.FLM_Driver.CAT_Driver.LastName + " " + c.OPS_DITOMaster.FLM_Driver.CAT_Driver.FirstName : "",
                        DateCome = c.DateCome,
                        DateLeave = c.DateLeave,
                        LocationAddress = c.CAT_Location.Address,
                        LocationName = c.CAT_Location.Location,
                        DITOLocationStatusID = c.DITOLocationStatusID,
                        DITOMasterID = c.DITOMasterID,
                        TypeOfTOLocationID = c.TypeOfTOLocationID,
                    }).FirstOrDefault();

                    if (objDITOLocation != null)
                    {
                        objDITOLocation.CreatedDateTemp = objDITOLocation.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objDITOLocation.ModifiedDateTemp = objDITOLocation.ModifiedDate.HasValue ? objDITOLocation.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOLocation.DateComeTemp = objDITOLocation.DateCome.HasValue ? objDITOLocation.DateCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOLocation.DateLeaveTemp = objDITOLocation.DateLeave.HasValue ? objDITOLocation.DateLeave.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOLocation.ListCustomerID = new List<int>();
                        if (objDITOLocation.DITOMasterID > 0)
                            objDITOLocation.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == objDITOLocation.DITOMasterID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                        obj = objDITOLocation;
                    }

                    break;
                #endregion

                #region OPS_DITOStation
                case "OPS_DITOStation":
                    var objDITOStation = model.OPS_DITOStation.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_DITOStation
                    {
                        ID = c.ID,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        TOMasterCode = c.OPS_DITOMaster.Code,
                        VehicleCode = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                        DriverName = c.OPS_DITOMaster.DriverID1 > 0 ? c.OPS_DITOMaster.FLM_Driver.CAT_Driver.LastName + " " + c.OPS_DITOMaster.FLM_Driver.CAT_Driver.FirstName : "",
                        DateCome = c.DateCome,
                        LocationAddress = c.CAT_Location.Address,
                        LocationName = c.CAT_Location.Location,
                        LocationID = c.LocationID,
                        DITOLocationID = c.DITOLocationID,
                        DITOMasterID = c.DITOMasterID,
                    }).FirstOrDefault();

                    if (objDITOStation != null)
                    {
                        objDITOStation.CreatedDateTemp = objDITOStation.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objDITOStation.ModifiedDateTemp = objDITOStation.ModifiedDate.HasValue ? objDITOStation.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOStation.DateComeTemp = objDITOStation.DateCome.HasValue ? objDITOStation.DateCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objDITOStation.ListCustomerID = new List<int>();
                        if (objDITOStation.DITOMasterID > 0)
                            objDITOStation.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == objDITOStation.DITOMasterID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                        obj = objDITOStation;
                    }
                    break;
                #endregion

                #region FLM_AssetWarning
                case "FLM_AssetWarning":
                    var objAssetWarning = model.FLM_AssetWarning.Where(c => c.ID == id).Select(c => new DTOMessageQueueHelper_AssetWarning
                    {
                        ID = c.ID,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        AssetID = c.AssetID,
                        AssetNo = c.FLM_Asset.Code != null ? c.FLM_Asset.Code : c.FLM_Asset.VehicleID > 0 ? c.FLM_Asset.CAT_Vehicle.RegNo : c.FLM_Asset.RomoocID > 0 ? c.FLM_Asset.CAT_Romooc.RegNo : c.FLM_Asset.ContainerID > 0 ? c.FLM_Asset.CAT_Container.ContainerNo : "",
                        TypeWarningID = c.TypeWarningID,
                        TypeWarningName = c.FLM_TypeWarning.WarningName,
                        DateData = c.DateData,
                        DateCompare = c.DateCompare,
                        NumberData = c.NumberData,
                        NumberCompare = c.NumberCompare,
                    }).FirstOrDefault();

                    if (objAssetWarning != null)
                    {
                        objAssetWarning.CreatedDateTemp = objAssetWarning.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                        objAssetWarning.ModifiedDateTemp = objAssetWarning.ModifiedDate.HasValue ? objAssetWarning.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objAssetWarning.DateCompareTemp = objAssetWarning.DateCompare.HasValue ? objAssetWarning.DateCompare.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        objAssetWarning.DateDataTemp = objAssetWarning.DateData.HasValue ? objAssetWarning.DateData.Value.ToString("dd/MM/yyyy HH:mm") : "";
                        obj = objAssetWarning;
                    }
                    break;
                #endregion
            }
            return obj;
        }

        private static Type GetTypeByTypeName(string typeName)
        {
            switch (typeName)
            {
                case "int": return typeof(int);
                case "string": return typeof(string);
                case "datetime": return typeof(DateTime);
                case "bool": return typeof(bool);
                case "double": return typeof(double);
                case "decimal": return typeof(decimal);
                default: return typeof(string);
            }
        }
    }
}
