using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Data;
using DTO;
using System.ServiceModel;
using ExpressionEvaluator;
using Business.Common;
using System.Threading;

namespace Business
{
    public class BLWorkFlow : Base, IBase
    {
        #region Event
        public DTOResult WFLSetting_EventRead(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_Event.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOWFLEvent
                    {
                        ID = c.ID,
                        Code = c.Code,
                        EventName = c.EventName,
                        Expression = c.Expression,
                        IsApproved = c.IsApproved,
                        IsChoose = false,
                        IsSystem = c.IsSystem
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLEvent>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLEvent WFLSetting_EventGet(int id)
        {
            try
            {
                var result = new DTOWFLEvent { ID = -1, Code = "", EventName = "", Expression = "", IsApproved = true, };
                result.lstField = new List<DTOWFLEvent_Field>();
                result.lstUserMail = new List<DTOWFLEvent_ActionUser>();
                result.lstUserTMS = new List<DTOWFLEvent_ActionUser>();
                result.lstUserSMS = new List<DTOWFLEvent_ActionUser>();
                result.lstTemplate = new List<DTOWFLEvent_Template>();
                result.lstCustomer = new List<DTOWFLEvent_ActionCustomer>();
                result.MailTemplateContent = result.SMSTemplateContent = result.TMSTemplateContent = string.Empty;
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.WFL_Event.Where(c => c.ID == id).Select(c => new DTOWFLEvent
                        {
                            ID = c.ID,
                            Code = c.Code,
                            EventName = c.EventName,
                            Expression = c.Expression,
                            IsApproved = c.IsApproved,
                            IsChoose = false,
                            IsSystem = c.IsSystem,
                            IsExpr = false,
                        }).FirstOrDefault();

                        if (result.ID > 0)
                        {
                            var lstField = model.WFL_EventField.Where(c => c.EventID == result.ID).Select(c => new DTOWFLEvent_Field
                            {
                                ID = c.ID,
                                CompareValue = c.CompareValue,
                                FieldID = c.FieldID,
                                FieldCode = c.WFL_Field.ColumnName,
                                FieldName = c.WFL_Field.ColumnName,
                                OperatorCode = c.OperatorCode,
                                OperatorValue = c.OperatorValue,
                                TableCode = c.WFL_Field.TableName,
                                Type = c.WFL_Field.ColumnType,
                                IsModified = c.IsModified,
                                IsChoose = false
                            }).ToList();
                            result.lstField = lstField;

                            var lstUserMail = model.WFL_Action.Where(c => c.EventID == result.ID && c.UserID.HasValue && c.IsUse && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail).Select(c => new
                                {
                                    UserID = c.UserID,
                                    UserName = c.SYS_User.UserName,
                                    Email = c.SYS_User.Email,
                                    TelNo = c.SYS_User.TelNo,
                                    IsChoose = false
                                }).Distinct().ToList();
                            result.lstUserMail = lstUserMail.Select(c => new DTOWFLEvent_ActionUser { UserID = c.UserID, UserName = c.UserName, Email = c.Email, IsChoose = c.IsChoose }).ToList();

                            var lstUserTMS = model.WFL_Action.Where(c => c.EventID == result.ID && c.UserID.HasValue && c.IsUse && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS).Select(c => new
                            {
                                UserID = c.UserID,
                                UserName = c.SYS_User.UserName,
                                Email = c.SYS_User.Email,
                                TelNo = c.SYS_User.TelNo,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.lstUserTMS = lstUserTMS.Select(c => new DTOWFLEvent_ActionUser { UserID = c.UserID, UserName = c.UserName, Email = c.Email, IsChoose = c.IsChoose }).ToList();

                            var lstUserSMS = model.WFL_Action.Where(c => c.EventID == result.ID && c.UserID.HasValue && c.IsUse && c.TypeOfActionID == (int)WFLTypeOfAction.SMS).Select(c => new
                            {
                                UserID = c.UserID,
                                UserName = c.SYS_User.UserName,
                                Email = c.SYS_User.Email,
                                TelNo = c.SYS_User.TelNo,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.lstUserSMS = lstUserSMS.Select(c => new DTOWFLEvent_ActionUser { UserID = c.UserID, UserName = c.UserName, Email = c.Email, IsChoose = c.IsChoose }).ToList();

                            var lstCustomer = model.WFL_Action.Where(c => c.EventID == result.ID && c.CustomerID.HasValue).Select(c => new
                            {
                                CustomerID = c.CustomerID,
                                CustomerName = c.CUS_Customer.CustomerName,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.lstCustomer = lstCustomer.Select(c => new DTOWFLEvent_ActionCustomer { CustomerID = c.CustomerID, CustomerName = c.CustomerName, IsChoose = c.IsChoose }).ToList();

                            var lstTemplate = model.WFL_EventTemplate.Where(c => c.EventID == result.ID).Select(c => new DTOWFLEvent_Template
                            {
                                Template = c.WFL_Template.Template,
                                TemplateDetail = c.WFL_Template.TemplateDetail,
                                TemplateID = c.TemplateID,
                                TypeOfActionID = c.TypeOfActionID
                            }).ToList();

                            var mailTemp = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                            if (mailTemp != null)
                            {
                                result.MailTemplateID = mailTemp.TemplateID;
                                result.MailTemplateContent = mailTemp.Template;
                            }
                            var tmsTemp = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                            if (tmsTemp != null)
                            {
                                result.TMSTemplateID = tmsTemp.TemplateID;
                                result.TMSTemplateContent = tmsTemp.Template;
                            }
                            var smsTemp = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                            if (smsTemp != null)
                            {
                                result.SMSTemplateID = smsTemp.TemplateID;
                                result.SMSTemplateContent = smsTemp.Template;
                            }
                        }
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public int WFLSetting_EventSave(DTOWFLEvent item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    var obj = model.WFL_Event.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new WFL_Event();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        model.WFL_Event.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.EventName = item.EventName;
                    obj.IsApproved = item.IsApproved;
                    obj.Expression = item.Expression;

                    // Xóa data cũ
                    foreach (var detail in model.WFL_EventField.Where(c => c.EventID == item.ID))
                        model.WFL_EventField.Remove(detail);

                    // Lưu danh sách field
                    if (item.lstField != null)
                    {
                        foreach (var field in item.lstField)
                        {
                            var objField = new WFL_EventField();
                            objField.CreatedBy = Account.UserName;
                            objField.CreatedDate = DateTime.Now;
                            objField.FieldID = field.FieldID;
                            objField.OperatorCode = field.OperatorCode == null ? string.Empty : field.OperatorCode;
                            objField.OperatorValue = field.OperatorValue;
                            objField.CompareValue = field.CompareValue;
                            objField.IsModified = field.IsModified;
                            if (string.IsNullOrEmpty(field.CompareValue))
                                field.CompareValue = objField.CompareValue = "null";

                            obj.WFL_EventField.Add(objField);
                        }
                        if (item.IsExpr)
                            obj.Expression = WFLSetting_GenerateExpression(item.lstField);
                    }

                    // Lưu template mail
                    if (item.MailTemplateID > 0)
                    {
                        var objTemplate = model.WFL_EventTemplate.FirstOrDefault(c => c.EventID == obj.ID && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                        if (objTemplate == null)
                        {
                            objTemplate = new WFL_EventTemplate();
                            objTemplate.CreatedBy = Account.UserName;
                            objTemplate.CreatedDate = DateTime.Now;
                            objTemplate.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                            objTemplate.WFL_Event = obj;
                            model.WFL_EventTemplate.Add(objTemplate);
                        }
                        objTemplate.TemplateID = item.MailTemplateID;
                    }

                    // Lưu template tms
                    if (item.TMSTemplateID > 0)
                    {
                        var objTemplate = model.WFL_EventTemplate.FirstOrDefault(c => c.EventID == obj.ID && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                        if (objTemplate == null)
                        {
                            objTemplate = new WFL_EventTemplate();
                            objTemplate.CreatedBy = Account.UserName;
                            objTemplate.CreatedDate = DateTime.Now;
                            objTemplate.TypeOfActionID = (int)WFLTypeOfAction.MessageTMS;
                            objTemplate.WFL_Event = obj;
                            model.WFL_EventTemplate.Add(objTemplate);
                        }
                        objTemplate.TemplateID = item.TMSTemplateID;
                    }

                    // Lưu template sms
                    if (item.SMSTemplateID > 0)
                    {
                        var objTemplate = model.WFL_EventTemplate.FirstOrDefault(c => c.EventID == obj.ID && c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                        if (objTemplate == null)
                        {
                            objTemplate = new WFL_EventTemplate();
                            objTemplate.CreatedBy = Account.UserName;
                            objTemplate.CreatedDate = DateTime.Now;
                            objTemplate.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                            objTemplate.WFL_Event = obj;
                            model.WFL_EventTemplate.Add(objTemplate);
                        }
                        objTemplate.TemplateID = item.SMSTemplateID;
                    }

                    // Disable tất cả user hiện tại
                    foreach (var act in model.WFL_Action.Where(c => c.EventID == item.ID && c.UserID.HasValue))
                        act.IsUse = false;

                    #region Gửi mail
                    if (item.lstUserMail != null && item.lstUserMail.Count > 0)
                    {
                        foreach (var user in item.lstUserMail)
                        {
                            if (item.lstCustomer != null && item.lstCustomer.Count > 0)
                            {
                                foreach (var cus in item.lstCustomer)
                                {
                                    var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == cus.CustomerID && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                                    if (objAction == null)
                                    {
                                        objAction = new WFL_Action();
                                        objAction.CreatedBy = Account.UserName;
                                        objAction.CreatedDate = DateTime.Now;
                                        objAction.WFL_Event = obj;
                                        model.WFL_Action.Add(objAction);
                                    }
                                    objAction.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                                    objAction.UserID = user.UserID;
                                    objAction.CustomerID = cus.CustomerID;
                                    objAction.IsUse = true;
                                }
                            }
                            else
                            {
                                var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == null && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                                if (objAction == null)
                                {
                                    objAction = new WFL_Action();
                                    objAction.CreatedBy = Account.UserName;
                                    objAction.CreatedDate = DateTime.Now;
                                    objAction.WFL_Event = obj;
                                    model.WFL_Action.Add(objAction);
                                }
                                objAction.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                                objAction.UserID = user.UserID;
                                objAction.IsUse = true;
                            }
                        }
                    }
                    #endregion

                    #region Gửi thông báo TMS
                    if (item.lstUserTMS != null && item.lstUserTMS.Count > 0)
                    {
                        foreach (var user in item.lstUserTMS)
                        {
                            if (item.lstCustomer != null && item.lstCustomer.Count > 0)
                            {
                                foreach (var cus in item.lstCustomer)
                                {
                                    var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == cus.CustomerID && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                                    if (objAction == null)
                                    {
                                        objAction = new WFL_Action();
                                        objAction.CreatedBy = Account.UserName;
                                        objAction.CreatedDate = DateTime.Now;
                                        objAction.WFL_Event = obj;
                                        model.WFL_Action.Add(objAction);
                                    }
                                    objAction.TypeOfActionID = (int)WFLTypeOfAction.MessageTMS;
                                    objAction.UserID = user.UserID;
                                    objAction.CustomerID = cus.CustomerID;
                                    objAction.IsUse = true;
                                }
                            }
                            else
                            {
                                var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == null && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                                if (objAction == null)
                                {
                                    objAction = new WFL_Action();
                                    objAction.CreatedBy = Account.UserName;
                                    objAction.CreatedDate = DateTime.Now;
                                    objAction.WFL_Event = obj;
                                    model.WFL_Action.Add(objAction);
                                }
                                objAction.TypeOfActionID = (int)WFLTypeOfAction.MessageTMS;
                                objAction.UserID = user.UserID;
                                objAction.IsUse = true;
                            }
                        }
                    }
                    #endregion

                    #region Gửi SMS
                    if (item.lstUserSMS != null && item.lstUserSMS.Count > 0)
                    {
                        foreach (var user in item.lstUserSMS
                            )
                        {
                            if (item.lstCustomer != null && item.lstCustomer.Count > 0)
                            {
                                foreach (var cus in item.lstCustomer)
                                {
                                    var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == cus.CustomerID && c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                                    if (objAction == null)
                                    {
                                        objAction = new WFL_Action();
                                        objAction.CreatedBy = Account.UserName;
                                        objAction.CreatedDate = DateTime.Now;
                                        objAction.WFL_Event = obj;
                                        model.WFL_Action.Add(objAction);
                                    }
                                    objAction.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                                    objAction.UserID = user.UserID;
                                    objAction.CustomerID = cus.CustomerID;
                                    objAction.IsUse = true;
                                }
                            }
                            else
                            {
                                var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == null && c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                                if (objAction == null)
                                {
                                    objAction = new WFL_Action();
                                    objAction.CreatedBy = Account.UserName;
                                    objAction.CreatedDate = DateTime.Now;
                                    objAction.WFL_Event = obj;
                                    model.WFL_Action.Add(objAction);
                                }
                                objAction.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                                objAction.UserID = user.UserID;
                                objAction.IsUse = true;
                            }
                        }
                    }
                    #endregion

                    model.SaveChanges();

                    return obj.ID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_EventDelete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lstid.Count > 0)
                    {
                        foreach (var item in lstid)
                        {
                            var obj = model.WFL_Event.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                if (model.WFL_Message.Count(c => c.WFL_Action.EventID == item) > 0)
                                    throw FaultHelper.BusinessFault(null, null, "Event đã phát sinh, không thể xóa");

                                foreach (var detail in model.WFL_EventField.Where(c => c.EventID == item))
                                    model.WFL_EventField.Remove(detail);
                                foreach (var detail in model.WFL_Action.Where(c => c.EventID == item))
                                    model.WFL_Action.Remove(detail);
                                foreach (var detail in model.WFL_EventTemplate.Where(c => c.EventID == item))
                                    model.WFL_EventTemplate.Remove(detail);
                                model.WFL_Event.Remove(obj);
                            }
                        }
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLField> WFLSetting_EventTableRead()
        {
            try
            {
                List<DTOWFLField> result = new List<DTOWFLField>();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_Field.Where(c => c.IsApproved).Select(c => new
                    {
                        TableName = c.TableName,
                        TableDescription = c.TableDescription,
                    }).Distinct().ToList();
                    if (query.Count > 0)
                    {
                        result = query.Select(c => new DTOWFLField
                            {
                                TableName = c.TableName,
                                TableDescription = c.TableDescription
                            }).ToList();
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLField> WFLSetting_EventFieldRead()
        {
            try
            {
                List<DTOWFLField> result = new List<DTOWFLField>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_Field.Where(c => c.IsApproved).Select(c => new DTOWFLField
                    {
                        ID = c.ID,
                        TableName = c.TableName,
                        TableDescription = c.TableDescription,
                        ColumnType = c.ColumnType,
                        ColumnName = c.ColumnName,
                        ColumnDescription = c.ColumnDescription,
                        IsApproved = c.IsApproved
                    }).Distinct().ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLTemplate> WFLSetting_EventTemplateRead()
        {
            try
            {
                List<DTOWFLTemplate> result = new List<DTOWFLTemplate>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_Template.Select(c => new DTOWFLTemplate
                    {
                        ID = c.ID,
                        Code = c.Code,
                        Name = c.Name,
                        Template = c.Template,
                        TemplateDetail = c.TemplateDetail
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<CUSCustomer> WFLSetting_EventCustomerRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<CUSCustomer>();
                    var objUser = model.SYS_User.Where(c => c.ID == Account.UserID).Select(c => new { c.ListCustomerID }).FirstOrDefault();
                    if (objUser != null && objUser.ListCustomerID != null && objUser.ListCustomerID.Trim() != string.Empty)
                    {
                        var lstid = objUser.ListCustomerID.Split(',').Select(c => Convert.ToInt32(c)).ToList();
                        result = model.CUS_Customer.Where(c => !c.IsSystem && lstid.Contains(c.ID) && (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH)).Select(c => new CUSCustomer
                        {
                            ID = c.ID,
                            Code = c.Code,
                            CustomerName = c.CustomerName
                        }).ToList();
                    }
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSUser> WFLSetting_EventUserRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<SYSUser>();
                    result = model.SYS_User.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new SYSUser
                        {
                            ID = c.ID,
                            Code = c.Code,
                            UserName = c.UserName,
                            Email = c.Email,
                            TelNo = c.TelNo,
                        }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLTypeOfAction> WFLSetting_EventTypeOfActionRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<DTOWFLTypeOfAction>();
                    result = model.WFL_TypeOfAction.Select(c => new DTOWFLTypeOfAction
                    {
                        ID = c.ID,
                        Code = c.Code,
                        TypeName = c.TypeName,
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSVar> WFLSetting_EventStatusOfOrderRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<SYSVar>();
                    result = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfOrder).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSVar> WFLSetting_EventStatusOfPlanRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<SYSVar>();
                    result = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfPlan).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSVar> WFLSetting_EventStatusOfDITOMasterRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<SYSVar>();
                    result = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfDITOMaster).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSVar> WFLSetting_EventKPIReasonRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<SYSVar>();
                    result = model.KPI_Reason.Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ReasonName
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSVar> WFLSetting_EventStatusOfAssetTimeSheetRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<SYSVar>();
                    result = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfAssetTimeSheet).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSVar> WFLSetting_EventTypeOfAssetTimeSheetRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<SYSVar>();
                    result = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TypeOfAssetTimeSheet).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLEvent_SysVar WFLSettingEvent_SysVar()
        {
            try
            {
                DTOWFLEvent_SysVar reslut = new DTOWFLEvent_SysVar();
                using (var model = new DataEntities())
                {
                    reslut.ListDITOGroupProductStatus = new List<SYSVar>();
                    reslut.ListDITOGroupProductStatusPOD = new List<SYSVar>();
                    reslut.ListKPIReason = new List<SYSVar>();
                    reslut.ListStatusOfAssetTimeSheet = new List<SYSVar>();
                    reslut.ListStatusOfDITOMaster = new List<SYSVar>();
                    reslut.ListStatusOfCOTOMaster = new List<SYSVar>();
                    reslut.ListStatusOfOrder = new List<SYSVar>();
                    reslut.ListStatusOfPlan = new List<SYSVar>();
                    reslut.ListTypeOfAssetTimeSheet = new List<SYSVar>();
                    reslut.ListTypeOfPaymentDITOMaster = new List<SYSVar>();
                    reslut.ListTroubleCostStatus = new List<SYSVar>();
                    reslut.ListDITOLocationStatus = new List<SYSVar>();
                    reslut.ListCOTOLocationStatus = new List<SYSVar>();
                    reslut.ListDITOGroupProductStatus = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.DITOGroupProductStatus).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListDITOGroupProductStatusPOD = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.DITOGroupProductStatusPOD).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListKPIReason = model.KPI_Reason.Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ReasonName
                    }).ToList();

                    reslut.ListStatusOfAssetTimeSheet = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfAssetTimeSheet).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListStatusOfDITOMaster = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfDITOMaster).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListStatusOfCOTOMaster = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfCOTOMaster).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListStatusOfOrder = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfOrder).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListStatusOfPlan = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfPlan).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListTypeOfAssetTimeSheet = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TypeOfAssetTimeSheet).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListTypeOfPaymentDITOMaster = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TypeOfPaymentDITOMaster).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListTroubleCostStatus = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TroubleCostStatus).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListDITOLocationStatus = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.DITOLocationStatus).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListCOTOLocationStatus = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.COTOLocationStatus).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();
                }

                return reslut;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        private string WFLSetting_GenerateExpression(List<DTOWFLEvent_Field> lst)
        {
            lst = lst.OrderBy(c => c.OperatorCode).ToList();
            string strExpression = string.Empty;

            string[] ArrayOpName = { "Equal", "NotEqual", "Great", "Less", "GreaterOrEqual", "LesserOrEqual",
                                     "EqualField", "NotEqualField", "GreatField", "LessField", "GreaterOrEqualField",
                                     "LesserOrEqualField" };
            string[] ArrayOpVal = { "==", "!=", ">", "<", ">=", "<=" };
            string[] ArrayOpCodeName = { "Or", "And" };
            string[] ArrayOpCodeVal = { "||", "&&" };
            int plat = 0;
            strExpression = "(";
            foreach (var x in lst)
            {
                string OperatorCode = "";
                if (plat != 0)
                {
                    for (int i = 0; i < ArrayOpCodeName.Length; i++)
                    {
                        if (ArrayOpCodeName[i] == x.OperatorCode)
                        {
                            OperatorCode = ArrayOpCodeVal[i];
                            break;
                        }
                    }
                }
                string OperatorValue = "";
                for (int i = 0; i < ArrayOpName.Length; i++)
                {
                    if (ArrayOpName[i] == x.OperatorValue)
                    {

                        int t = 0;
                        if (i > 5)
                        {
                            t = i - 6;
                        }
                        else
                            t = i;
                        OperatorValue = ArrayOpVal[t];
                        break;
                    }
                }
                strExpression = strExpression + OperatorCode + x.FieldCode + OperatorValue + x.CompareValue;
                plat = 1;
            }
            strExpression = strExpression + ")";
            return strExpression;
        }

        private string WFLSetting_GenerateExpression(List<DTOWFL_WFEventField> lst)
        {
            lst = lst.OrderBy(c => c.OperatorCode).ToList();
            string strExpression = string.Empty;

            string[] ArrayOpName = { "Equal", "NotEqual", "Great", "Less", "GreaterOrEqual", "LesserOrEqual",
                                     "EqualField", "NotEqualField", "GreatField", "LessField", "GreaterOrEqualField",
                                     "LesserOrEqualField" };
            string[] ArrayOpVal = { "==", "!=", ">", "<", ">=", "<=" };
            string[] ArrayOpCodeName = { "Or", "And" };
            string[] ArrayOpCodeVal = { "||", "&&" };
            int plat = 0;
            strExpression = "(";
            foreach (var x in lst)
            {
                string OperatorCode = "";
                if (plat != 0)
                {
                    for (int i = 0; i < ArrayOpCodeName.Length; i++)
                    {
                        if (ArrayOpCodeName[i] == x.OperatorCode)
                        {
                            OperatorCode = ArrayOpCodeVal[i];
                            break;
                        }
                    }
                }
                string OperatorValue = "";
                for (int i = 0; i < ArrayOpName.Length; i++)
                {
                    if (ArrayOpName[i] == x.OperatorValue)
                    {

                        int t = 0;
                        if (i > 5)
                        {
                            t = i - 6;
                        }
                        else
                            t = i;
                        OperatorValue = ArrayOpVal[t];
                        break;
                    }
                }
                strExpression = strExpression + OperatorCode + x.FieldCode + OperatorValue + x.CompareValue;
                plat = 1;
            }
            strExpression = strExpression + ")";
            return strExpression;
        }
        #endregion

        #region Template
        public DTOResult WFLSetting_TemplateRead(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_Template.Select(c => new DTOWFLTemplate
                    {
                        ID = c.ID,
                        Code = c.Code,
                        Name = c.Name,
                        Template = c.Template,
                        TemplateDetail = c.TemplateDetail,
                        IsChoose = false
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLTemplate>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLTemplate WFLSetting_TemplateGet(int id)
        {
            try
            {
                var result = new DTOWFLTemplate { ID = -1, Code = "", Name = "", Template = "" };
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.WFL_Template.Where(c => c.ID == id).Select(c => new DTOWFLTemplate
                        {
                            ID = c.ID,
                            Code = c.Code,
                            Name = c.Name,
                            Template = c.Template,
                            TemplateDetail = c.TemplateDetail,
                            Subject = c.Subject,
                            FileID = c.FileID,
                            FileName = c.CAT_File.FileName,
                            IsChoose = false
                        }).FirstOrDefault();
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public int WFLSetting_TemplateSave(DTOWFLTemplate item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var obj = model.WFL_Template.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new WFL_Template();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.WFL_Template.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.Name = item.Name;
                    obj.Template = item.Template;
                    obj.TemplateDetail = item.TemplateDetail;
                    obj.Subject = item.Subject;
                    obj.FileID = item.FileID;
                    model.SaveChanges();

                    return obj.ID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_TemplateDelete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lstid.Count > 0)
                    {
                        foreach (var item in lstid)
                        {
                            var obj = model.WFL_Template.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                if (model.WFL_EventTemplate.Count(c => c.TemplateID == obj.ID) > 0)
                                    throw FaultHelper.BusinessFault(null, null, "Trường dữ liệu đang được sử dụng, ko thể xóa!");

                                model.WFL_Template.Remove(obj);
                            }
                        }
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region Field
        public DTOResult WFLSetting_FieldRead(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_Field.Select(c => new DTOWFLField
                    {
                        ID = c.ID,
                        ColumnDescription = c.ColumnDescription,
                        ColumnName = c.ColumnName,
                        ColumnType = c.ColumnType,
                        TableDescription = c.TableDescription,
                        TableName = c.TableName,
                        IsApproved = c.IsApproved,
                        IsChoose = false
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLField>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLField WFLSetting_FieldGet(int id)
        {
            try
            {
                var result = new DTOWFLField { ID = -1 };
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.WFL_Field.Where(c => c.ID == id).Select(c => new DTOWFLField
                        {
                            ID = c.ID,
                            ColumnDescription = c.ColumnDescription,
                            ColumnName = c.ColumnName,
                            ColumnType = c.ColumnType,
                            TableDescription = c.TableDescription,
                            TableName = c.TableName,
                            IsApproved = c.IsApproved,
                            IsChoose = false
                        }).FirstOrDefault();
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public int WFLSetting_FieldSave(DTOWFLField item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var obj = model.WFL_Field.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new WFL_Field();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.WFL_Field.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.TableName = item.TableName;
                    obj.TableDescription = item.TableDescription;
                    obj.ColumnName = item.ColumnName;
                    obj.ColumnDescription = item.ColumnDescription;
                    obj.ColumnType = item.ColumnType;
                    obj.IsApproved = item.IsApproved;
                    model.SaveChanges();

                    return obj.ID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_FieldDelete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lstid.Count > 0)
                    {
                        foreach (var item in lstid)
                        {
                            var obj = model.WFL_Field.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                if (model.WFL_EventField.Count(c => c.FieldID == obj.ID) > 0)
                                    throw FaultHelper.BusinessFault(null, null, "Trường dữ liệu đang được sử dụng, ko thể xóa!");
                                model.WFL_Field.Remove(obj);
                            }
                        }
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region Define
        public DTOResult WFLSetting_DefineRead(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_Define.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOWFLDefine
                    {
                        ID = c.ID,
                        Code = c.Code,
                        DefineName = c.DefineName,
                        IsChoose = false
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLDefine>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLDefine WFLSetting_DefineGet(int id)
        {
            try
            {
                var result = new DTOWFLDefine { ID = -1 };
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.WFL_Define.Where(c => c.ID == id).Select(c => new DTOWFLDefine
                        {
                            ID = c.ID,
                            Code = c.Code,
                            DefineName = c.DefineName,
                            IsChoose = false
                        }).FirstOrDefault();

                        //var step = model.WFL_DefineStep.Where(c => c.DefineID == id && c.WFL_Step.ParentID == null).FirstOrDefault();
                        //if (step != null)
                        //{
                        //    result.StepID = step.ID;
                        //}
                        //else
                        //{
                        //    var stepID = model.WFL_Step.Where(c => c.ParentID == null).Select(c => c.ID).FirstOrDefault();
                        //    if (stepID != null)
                        //    {
                        //        result.StepID = stepID;
                        //    }
                        //}
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public int WFLSetting_DefineSave(DTOWFLDefine item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (model.WFL_Define.Count(c => c.ID != item.ID && c.Code == item.Code) > 0)
                        throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");

                    var obj = model.WFL_Define.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new WFL_Define();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        model.WFL_Define.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.DefineName = item.DefineName;
                    model.SaveChanges();

                    return obj.ID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_DefineDelete(int id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    var objDefine = model.WFL_Define.Where(c => c.ID == id).FirstOrDefault();

                    if (objDefine == null)
                    {
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy define");
                    }

                    //Xóa WL Define Group
                    var listWFDefineGroup = model.WFL_DefineGroup.Where(c => c.DefineID == id).ToList();
                    if (listWFDefineGroup != null)
                        model.WFL_DefineGroup.RemoveRange(listWFDefineGroup);

                    //Xóa WL Define Step
                    var listWFDefineStep = model.WFL_DefineStep.Where(c => c.DefineID == id).ToList();
                    if (listWFDefineStep != null)
                        model.WFL_DefineStep.RemoveRange(listWFDefineStep);

                    //Xóa Define WL
                    var listWF = model.WFL_DefineWF.Where(c => c.DefineID == id).ToList();

                    if (listWF != null)
                    {
                        //Xóa Define WL Event
                        foreach (var item in listWF)
                        {
                            var lstEvent = model.WFL_DefineWFEvent.Where(c => c.DefineWFID == item.ID).ToList();
                            //Xóa Define WL ACtion
                            if (lstEvent != null)
                            {
                                foreach (var wfEvent in lstEvent)
                                {
                                    var lstAction = model.WFL_DefineWFAction.Where(c => c.DefineWFEventID == wfEvent.ID).ToList();
                                    if (lstAction != null)
                                        model.WFL_DefineWFAction.RemoveRange(lstAction);
                                }
                                model.WFL_DefineWFEvent.RemoveRange(lstEvent);
                            }
                        }
                        model.WFL_DefineWF.RemoveRange(listWF);
                    }
                    model.WFL_Define.Remove(objDefine);

                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLStep> WFLSetting_StepParentList(int defineID)
        {
            try
            {
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_DefineStep.Where(c => c.DefineID == defineID && c.WFL_Step.ParentID == null).Select(c => new DTOWFLStep
                    {
                        ID = c.WFL_Step.ID,
                        Code = c.WFL_Step.Code,
                        StepName = c.WFL_Step.StepName,
                        Step = c.WFL_Step.Step
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLStep> WFLSetting_StepChildList(int stepID)
        {
            try
            {
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_Step.Where(c => c.ParentID == stepID).Select(c => new DTOWFLStep
                    {
                        ID = c.ID,
                        Code = c.Code,
                        StepName = c.StepName,
                        Step = c.Step
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLDefineData WFLSetting_DefineData(int defineID, int stepID)
        {
            try
            {
                DTOWFLDefineData result = new DTOWFLDefineData();
                using (var model = new DataEntities())
                {
                    //Get WL select
                    var listWF = model.WFL_DefineWF.Where(c => c.DefineID == defineID).Select(c => c.WFL_WorkFlow).ToList();

                    result.DefineID = defineID;
                    result.StepParentID = stepID;
                    result.maxWFLInStep = 0;
                    List<DTOWFLStep> listStep = new List<DTOWFLStep>();

                    listStep = model.WFL_Step.Where(c => c.ParentID == stepID).Select(c => new DTOWFLStep
                    {
                        ID = c.ID,
                        Code = c.Code,
                        StepName = c.StepName,
                        Step = c.Step,
                        UseAllWorkFlow = c.UseAllWorkFlow,
                    }).ToList();

                    result.ListWLStep = new List<DTOWFLWorkFlowStep>();

                    if (listStep != null)
                    {
                        foreach (var item in listStep)
                        {
                            DTOWFLWorkFlowStep data = new DTOWFLWorkFlowStep();
                            data.StepID = item.ID;
                            data.StepName = item.StepName;
                            data.ListWorkFlow = model.WFL_WorkFlow.Where(c => c.StepID == item.ID && c.ParentID == null).Select(c => new DTOWFLWorkFlow
                                {
                                    ID = c.ID,
                                    Code = c.Code,
                                    WorkFlowName = c.WorkFlowName,
                                    StepID = c.StepID,
                                    UseAllWorkFlow = item.UseAllWorkFlow,
                                }).ToList();
                            if (data.ListWorkFlow != null)
                            {
                                foreach (var objWFL in data.ListWorkFlow)
                                {
                                    var objDFWFL = model.WFL_DefineWF.Where(c => c.DefineID == defineID && c.WorkFlowID == objWFL.ID).FirstOrDefault();
                                    if (objDFWFL != null)
                                    {
                                        objWFL.IsSelected = true;
                                    }
                                    else
                                    {
                                        objWFL.IsSelected = false;
                                    }

                                    var count = model.WFL_WorkFlowWFEvent.Count(c => c.WorkFlowID == objWFL.ID);
                                    if (count > 0)
                                    {
                                        objWFL.HasEvent = true;
                                    }
                                    else
                                    {
                                        objWFL.HasEvent = false;
                                    }
                                }
                            }
                            result.ListWLStep.Add(data);
                            if (data.ListWorkFlow.Count > result.maxWFLInStep)
                            {
                                result.maxWFLInStep = data.ListWorkFlow.Count;
                            }
                        }
                    }

                    result.ListWorkFlowLink = new List<DTOWFLWorkFlowLink>();
                    if (listStep != null)
                    {
                        foreach (var item in result.ListWLStep)
                        {
                            if (item.ListWorkFlow != null)
                            {
                                foreach (var wf in item.ListWorkFlow)
                                {
                                    var listWFLLink = model.WFL_WorkFlowLink.Where(c => c.WorkFlowFromID == wf.ID).Select(c => new DTOWFLWorkFlowLink
                                    {
                                        ID = c.ID,
                                        WorkFlowFromID = c.WorkFlowFromID,
                                        WorkFlowToID = c.WorkFlowToID,
                                    }).ToList();

                                    result.ListWorkFlowLink.AddRange(listWFLLink);
                                }
                            }
                        }
                    }

                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public int WFLSetting_StepSave(DTOWFLStep item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var obj = model.WFL_Step.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new WFL_Step();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.WFL_Step.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.StepName = item.StepName;
                    model.SaveChanges();

                    return obj.ID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_DefineDetailSave(DTOWFLDefineData item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var data in item.ListWorkFlow)
                    {
                        if (data.ID > 0)
                        {
                            var objWFL = model.WFL_DefineWF.FirstOrDefault(c => c.WorkFlowID == data.ID && c.DefineID == item.DefineID);
                            if (objWFL != null)
                            {
                                if (!data.IsSelected)
                                {
                                    //Xoa
                                    model.WFL_DefineWF.Remove(objWFL);
                                }
                            }
                            else
                            {
                                if (data.IsSelected)
                                {
                                    objWFL = new WFL_DefineWF();
                                    objWFL.CreatedBy = Account.UserName;
                                    objWFL.CreatedDate = DateTime.Now;
                                    objWFL.DefineID = item.DefineID;
                                    objWFL.WorkFlowID = data.ID;
                                    model.WFL_DefineWF.Add(objWFL);
                                }
                            }
                        }
                    }
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_DefineWFLSave(int defineID, DTOWFLWorkFlow item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (item.ID > 0)
                    {
                        var objDefine = model.WFL_Define.FirstOrDefault(c => c.ID == defineID);
                        if (objDefine != null)
                        {
                            var objDefWFL = model.WFL_DefineWF.FirstOrDefault(c => c.WorkFlowID == item.ID && c.DefineID == defineID);
                            if (objDefWFL != null)
                            {
                                if (item.IsSelected)
                                {
                                    //Xoa
                                    var lstDefineWFEvent = model.WFL_DefineWFEvent.Where(c => c.DefineWFID == objDefWFL.ID).ToList();
                                    if (lstDefineWFEvent != null)
                                    {
                                        foreach (var objEvent in lstDefineWFEvent)
                                        {
                                            var lstAction = model.WFL_DefineWFAction.Where(c => c.DefineWFEventID == objEvent.ID);
                                            if (lstAction != null)
                                                model.WFL_DefineWFAction.RemoveRange(lstAction);
                                        }
                                        model.WFL_DefineWFEvent.RemoveRange(lstDefineWFEvent);
                                    }
                                    model.WFL_DefineWF.Remove(objDefWFL);
                                }
                            }
                            else
                            {
                                if (!item.IsSelected)
                                {
                                    objDefWFL = new WFL_DefineWF();
                                    objDefWFL.CreatedBy = Account.UserName;
                                    objDefWFL.CreatedDate = DateTime.Now;
                                    objDefWFL.DefineID = defineID;
                                    objDefWFL.WorkFlowID = item.ID;
                                    model.WFL_DefineWF.Add(objDefWFL);

                                    var objWFL = model.WFL_WorkFlow.FirstOrDefault(c => c.ID == item.ID);
                                    var objStep = model.WFL_Step.Where(c => c.ID == objWFL.StepID).FirstOrDefault();
                                    if (objStep != null)
                                    {
                                        if (objStep.UseAllWorkFlow == false)
                                        {
                                            List<int> listWFL = model.WFL_WorkFlow.Where(c => c.StepID == objStep.ID).Select(c => c.ID).ToList();

                                            if (listWFL != null)
                                            {
                                                if (model.WFL_DefineWF.Count(c => c.DefineID == defineID && listWFL.Contains(c.WorkFlowID) && c.WorkFlowID != item.ID) > 0)
                                                    throw FaultHelper.BusinessFault(null, null, "Step chỉ được chọn 1 WorkFlow");
                                                //var listDef = model.WFL_DefineWF.Where(c => c.DefineID == defineID && listWFL.Contains(c.WorkFlowID) && c.WorkFlowID != item.ID).ToList();
                                                //foreach (var objDefineWF in listDef)
                                                //{
                                                //    var lstDefineWFEvent = model.WFL_DefineWFEvent.Where(c => c.DefineWFID == objDefineWF.ID).ToList();
                                                //    if (lstDefineWFEvent != null)
                                                //    {
                                                //        foreach (var objEvent in lstDefineWFEvent)
                                                //        {
                                                //            var lstAction = model.WFL_DefineWFAction.Where(c => c.DefineWFEventID == objEvent.ID);
                                                //            if (lstAction != null)
                                                //                model.WFL_DefineWFAction.RemoveRange(lstAction);
                                                //        }
                                                //        model.WFL_DefineWFEvent.RemoveRange(lstDefineWFEvent);
                                                //    }
                                                //    model.WFL_DefineWF.Remove(objDefineWF);
                                                //}
                                            }
                                        }
                                    }
                                }
                            }
                            model.SaveChanges();
                        }
                        else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy define");
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLSetting_StepDefineList(int defineID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_DefineStep.Where(c => c.DefineID == defineID && c.WFL_Step.ParentID == null).Select(c => new DTOWFLDefineStep
                    {
                        ID = c.ID,
                        StepID = c.WFL_Step.ID,
                        Code = c.WFL_Step.Code,
                        StepName = c.WFL_Step.StepName,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Data = query.Data as IEnumerable<DTOWFLDefineStep>;
                    result.Total = query.Total;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLSetting_StepDefineNotInList(int defineID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                List<int> listID = new List<int>();
                using (var model = new DataEntities())
                {
                    listID = model.WFL_DefineStep.Where(c => c.DefineID == defineID && c.WFL_Step.ParentID == null).Select(c => c.WFL_Step.ID).ToList();
                    var query = model.WFL_Step.Where(c => !listID.Contains(c.ID) && c.ParentID == null).Select(c => new DTOWFLStep
                    {
                        ID = c.ID,
                        Code = c.Code,
                        StepName = c.StepName,
                        Step = c.Step
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Data = query.Data as IEnumerable<DTOWFLStep>;
                    result.Total = query.Total;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_StepDefineNotInSave(int defineID, List<int> lstStepID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objDefine = model.WFL_Define.FirstOrDefault(c => c.ID == defineID);
                    if (objDefine != null)
                    {
                        foreach (var id in lstStepID)
                        {
                            var obj = model.WFL_DefineStep.FirstOrDefault(c => c.DefineID == defineID && c.StepID == id);
                            if (obj != null)
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            else
                            {
                                obj = new WFL_DefineStep();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.DefineID = defineID;
                                obj.StepID = id;
                                model.WFL_DefineStep.Add(obj);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy define");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_StepDefineDelete(int defineID, List<int> lstStepID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objDefine = model.WFL_Define.FirstOrDefault(c => c.ID == defineID);
                    if (objDefine != null)
                    {
                        foreach (var id in lstStepID)
                        {
                            var obj = model.WFL_DefineStep.FirstOrDefault(c => c.DefineID == defineID && c.ID == id);
                            if (obj != null)
                            {
                                model.WFL_DefineStep.Remove(obj);

                                var lstWFL = model.WFL_DefineWF.Where(c => c.DefineID == defineID && c.WorkFlowID == obj.StepID).ToList();
                                if (lstWFL != null)
                                    model.WFL_DefineWF.RemoveRange(lstWFL);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy define");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLSetting_DefineGroupList(int defineID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_DefineGroup.Where(c => c.DefineID == defineID).Select(c => new DTOWFLDefineGroup
                    {
                        ID = c.ID,
                        GroupID = c.SYS_Group.ID,
                        Code = c.SYS_Group.Code,
                        GroupName = c.SYS_Group.GroupName,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Data = query.Data as IEnumerable<DTOWFLDefineGroup>;
                    result.Total = query.Total;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLSetting_DefineGroupNotInList(int defineID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                List<int> listID = new List<int>();
                using (var model = new DataEntities())
                {
                    listID = model.WFL_DefineGroup.Where(c => c.DefineID == defineID).Select(c => c.SYS_Group.ID).ToList();
                    if (listID != null)
                    {
                        var query = model.SYS_Group.Where(c => !listID.Contains(c.ID)).Select(c => new DTOWFLDefineGroup
                        {
                            ID = c.ID,
                            Code = c.Code,
                            GroupName = c.GroupName,
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Data = query.Data as IEnumerable<DTOWFLDefineGroup>;
                        result.Total = query.Total;
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_DefineGroupNotInSave(int defineID, List<int> lstGroupID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objDefine = model.WFL_Define.FirstOrDefault(c => c.ID == defineID);
                    if (objDefine != null)
                    {
                        foreach (var id in lstGroupID)
                        {
                            var obj = model.WFL_DefineGroup.FirstOrDefault(c => c.DefineID == defineID && c.GroupID == id);
                            if (obj != null)
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            else
                            {
                                obj = new WFL_DefineGroup();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.DefineID = defineID;
                                obj.GroupID = id;
                                model.WFL_DefineGroup.Add(obj);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy define");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_DefineGroupDelete(int defineID, List<int> lstGroupID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objDefine = model.WFL_Define.FirstOrDefault(c => c.ID == defineID);
                    if (objDefine != null)
                    {
                        foreach (var id in lstGroupID)
                        {
                            var obj = model.WFL_DefineGroup.FirstOrDefault(c => c.DefineID == defineID && c.ID == id);
                            if (obj != null)
                            {
                                model.WFL_DefineGroup.Remove(obj);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy define");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        //Event
        public DTOResult WFLSetting_DefineEventList(int defineID, int workflowID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {

                    var objDefine = model.WFL_Define.FirstOrDefault(c => c.ID == defineID);
                    var objWFL = model.WFL_WorkFlow.FirstOrDefault(c => c.ID == workflowID);
                    if (objDefine == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy define");
                    if (objWFL == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy workflow");

                    var objDefineWF = model.WFL_DefineWF.FirstOrDefault(c => c.DefineID == defineID && c.WorkFlowID == workflowID);
                    if (objDefineWF == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy DefineWF");
                    int defineWFID = objDefineWF.ID;

                    var query = model.WFL_DefineWFEvent.Where(c => c.DefineWFID == defineWFID).Select(c => new DTOWFLDefineEvent
                    {
                        ID = c.ID,
                        EventID = c.WFEventID,
                        EventCode = c.WFL_WFEvent.Code,
                        EventName = c.WFL_WFEvent.EventName,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Data = query.Data as IEnumerable<DTOWFLDefineEvent>;
                    result.Total = query.Total;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLSetting_DefineEventNotInList(int defineID, int workflowID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var objDefine = model.WFL_Define.FirstOrDefault(c => c.ID == defineID);
                    var objWFL = model.WFL_WorkFlow.FirstOrDefault(c => c.ID == workflowID);
                    if (objDefine == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy define");
                    if (objWFL == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy workflow");

                    var objDefineWF = model.WFL_DefineWF.FirstOrDefault(c => c.DefineID == defineID && c.WorkFlowID == workflowID);
                    if (objDefineWF == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy DefineWF");
                    int defineWFID = objDefineWF.ID;

                    var lstExist = model.WFL_DefineWFEvent.Where(c => c.DefineWFID == defineWFID).Select(c => c.WFEventID).Distinct().ToList();
                    var query = model.WFL_WorkFlowWFEvent.Where(c => !lstExist.Contains(c.ID) && c.WorkFlowID == workflowID).Select(c => new DTOWFLDefineEvent
                        {
                            ID = c.ID,
                            EventID = c.WFEventID,
                            EventCode = c.WFL_WFEvent.Code,
                            EventName = c.WFL_WFEvent.EventName,
                        }).ToDataSourceResult(CreateRequest(request));

                    result.Data = query.Data as IEnumerable<DTOWFLDefineEvent>;
                    result.Total = query.Total;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_DefineEventNotInSave(int defineID, int workflowID, List<int> lstEventID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objDefine = model.WFL_Define.FirstOrDefault(c => c.ID == defineID);
                    var objWFL = model.WFL_WorkFlow.FirstOrDefault(c => c.ID == workflowID);
                    if (objDefine == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy define");
                    if (objWFL == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy workflow");

                    var objDefineWF = model.WFL_DefineWF.FirstOrDefault(c => c.DefineID == defineID && c.WorkFlowID == workflowID);
                    if (objDefineWF == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy DefineWF");
                    int defineWFID = objDefineWF.ID;

                    var objWF = model.WFL_DefineWF.FirstOrDefault(c => c.ID == defineWFID);
                    if (objWF != null)
                    {
                        foreach (var id in lstEventID)
                        {
                            var obj = model.WFL_DefineWFEvent.FirstOrDefault(c => c.DefineWFID == defineWFID && c.WFEventID == id);
                            if (obj != null)
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            else
                            {
                                obj = new WFL_DefineWFEvent();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.DefineWFID = defineWFID;
                                obj.WFEventID = id;
                                model.WFL_DefineWFEvent.Add(obj);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy define workflow");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLDefineEvent WFLSetting_DefineEventGet(int id)
        {
            try
            {
                var result = new DTOWFLDefineEvent { ID = -1, EventName = "", EventCode = "" };
                result.ListCustomer = new List<DTOWFLDefineEventCustomer>();
                result.ListUserMail = new List<DTOWFLDefineEventAction>();
                result.ListUserSMS = new List<DTOWFLDefineEventAction>();
                result.ListUserTMS = new List<DTOWFLDefineEventAction>();
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.WFL_DefineWFEvent.Where(c => c.ID == id).Select(c => new DTOWFLDefineEvent
                        {
                            ID = c.ID,
                            EventID = c.WFEventID,
                            EventCode = c.WFL_WFEvent.Code,
                            EventName = c.WFL_WFEvent.EventName,
                        }).FirstOrDefault();

                        if (result.ID > 0)
                        {
                            var lstUserMail = model.WFL_DefineWFAction.Where(c => c.DefineWFEventID == result.ID && c.UserID.HasValue && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail).Select(c => new
                            {
                                UserID = c.UserID.Value,
                                UserName = c.SYS_User.UserName,
                                Email = c.SYS_User.Email,
                                TelNo = c.SYS_User.TelNo,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.ListUserMail = lstUserMail.Select(c => new DTOWFLDefineEventAction { UserID = c.UserID, UserName = c.UserName, Email = c.Email, IsChoose = c.IsChoose }).ToList();

                            var lstUserTMS = model.WFL_DefineWFAction.Where(c => c.DefineWFEventID == result.ID && c.UserID.HasValue && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS).Select(c => new
                            {
                                UserID = c.UserID.Value,
                                UserName = c.SYS_User.UserName,
                                Email = c.SYS_User.Email,
                                TelNo = c.SYS_User.TelNo,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.ListUserTMS = lstUserTMS.Select(c => new DTOWFLDefineEventAction { UserID = c.UserID, UserName = c.UserName, Email = c.Email, IsChoose = c.IsChoose }).ToList();

                            var lstUserSMS = model.WFL_DefineWFAction.Where(c => c.DefineWFEventID == result.ID && c.UserID.HasValue && c.TypeOfActionID == (int)WFLTypeOfAction.SMS).Select(c => new
                            {
                                UserID = c.UserID.Value,
                                UserName = c.SYS_User.UserName,
                                Email = c.SYS_User.Email,
                                TelNo = c.SYS_User.TelNo,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.ListUserSMS = lstUserSMS.Select(c => new DTOWFLDefineEventAction { UserID = c.UserID, UserName = c.UserName, Email = c.Email, IsChoose = c.IsChoose }).ToList();

                            var lstCustomer = model.WFL_DefineWFAction.Where(c => c.DefineWFEventID == result.ID && c.CustomerID.HasValue).Select(c => new
                            {
                                CustomerID = c.CustomerID,
                                CustomerName = c.CUS_Customer.CustomerName,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.ListCustomer = lstCustomer.Select(c => new DTOWFLDefineEventCustomer { CustomerID = c.CustomerID.Value, CustomerName = c.CustomerName, IsChoose = c.IsChoose }).ToList();
                        }
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_DefineEventSave(DTOWFLDefineEvent item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    // Disable tất cả user hiện tại
                    foreach (var act in model.WFL_Action.Where(c => c.EventID == item.ID && c.UserID.HasValue))
                        act.IsUse = false;

                    #region Gửi mail
                    if (item.ListUserMail != null && item.ListUserMail.Count > 0)
                    {
                        foreach (var user in item.ListUserMail)
                        {
                            if (item.ListCustomer != null && item.ListCustomer.Count > 0)
                            {
                                foreach (var cus in item.ListCustomer)
                                {
                                    var objAction = model.WFL_DefineWFAction.FirstOrDefault(c => c.DefineWFEventID == item.ID && c.UserID == user.UserID && c.CustomerID == cus.CustomerID && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                                    if (objAction == null)
                                    {
                                        objAction = new WFL_DefineWFAction();
                                        objAction.CreatedBy = Account.UserName;
                                        objAction.CreatedDate = DateTime.Now;
                                        objAction.DefineWFEventID = item.ID;
                                        model.WFL_DefineWFAction.Add(objAction);
                                    }
                                    objAction.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                                    objAction.UserID = user.UserID;
                                    objAction.CustomerID = cus.CustomerID;
                                    objAction.IsUsed = true;
                                }
                            }
                            else
                            {
                                var objAction = model.WFL_DefineWFAction.FirstOrDefault(c => c.DefineWFEventID == item.ID && c.UserID == user.UserID && c.CustomerID == null && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                                if (objAction == null)
                                {
                                    objAction = new WFL_DefineWFAction();
                                    objAction.CreatedBy = Account.UserName;
                                    objAction.CreatedDate = DateTime.Now;
                                    objAction.DefineWFEventID = item.ID;
                                    model.WFL_DefineWFAction.Add(objAction);
                                }
                                objAction.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                                objAction.UserID = user.UserID;
                                objAction.IsUsed = true;
                            }
                        }
                    }
                    #endregion

                    #region Gửi thông báo TMS
                    if (item.ListUserTMS != null && item.ListUserTMS.Count > 0)
                    {
                        foreach (var user in item.ListUserTMS)
                        {
                            if (item.ListCustomer != null && item.ListCustomer.Count > 0)
                            {
                                foreach (var cus in item.ListCustomer)
                                {
                                    var objAction = model.WFL_DefineWFAction.FirstOrDefault(c => c.DefineWFEventID == item.ID && c.UserID == user.UserID && c.CustomerID == cus.CustomerID && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                                    if (objAction == null)
                                    {
                                        objAction = new WFL_DefineWFAction();
                                        objAction.CreatedBy = Account.UserName;
                                        objAction.CreatedDate = DateTime.Now;
                                        objAction.DefineWFEventID = item.ID;
                                        model.WFL_DefineWFAction.Add(objAction);
                                    }
                                    objAction.TypeOfActionID = (int)WFLTypeOfAction.MessageTMS;
                                    objAction.UserID = user.UserID;
                                    objAction.CustomerID = cus.CustomerID;
                                    objAction.IsUsed = true;
                                }
                            }
                            else
                            {
                                var objAction = model.WFL_DefineWFAction.FirstOrDefault(c => c.DefineWFEventID == item.ID && c.UserID == user.UserID && c.CustomerID == null && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                                if (objAction == null)
                                {
                                    objAction = new WFL_DefineWFAction();
                                    objAction.CreatedBy = Account.UserName;
                                    objAction.CreatedDate = DateTime.Now;
                                    objAction.DefineWFEventID = item.ID;
                                    model.WFL_DefineWFAction.Add(objAction);
                                }
                                objAction.TypeOfActionID = (int)WFLTypeOfAction.MessageTMS;
                                objAction.UserID = user.UserID;
                                objAction.IsUsed = true;
                            }
                        }
                    }
                    #endregion

                    #region Gửi SMS
                    if (item.ListUserSMS != null && item.ListUserSMS.Count > 0)
                    {
                        foreach (var user in item.ListUserSMS)
                        {
                            if (item.ListCustomer != null && item.ListCustomer.Count > 0)
                            {
                                foreach (var cus in item.ListCustomer)
                                {
                                    var objAction = model.WFL_DefineWFAction.FirstOrDefault(c => c.DefineWFEventID == item.ID && c.UserID == user.UserID && c.CustomerID == cus.CustomerID && c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                                    if (objAction == null)
                                    {
                                        objAction = new WFL_DefineWFAction();
                                        objAction.CreatedBy = Account.UserName;
                                        objAction.CreatedDate = DateTime.Now;
                                        objAction.DefineWFEventID = item.ID;
                                        model.WFL_DefineWFAction.Add(objAction);
                                    }
                                    objAction.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                                    objAction.UserID = user.UserID;
                                    objAction.CustomerID = cus.CustomerID;
                                    objAction.IsUsed = true;
                                }
                            }
                            else
                            {
                                var objAction = model.WFL_DefineWFAction.FirstOrDefault(c => c.DefineWFEventID == item.ID && c.UserID == user.UserID && c.CustomerID == null && c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                                if (objAction == null)
                                {
                                    objAction = new WFL_DefineWFAction();
                                    objAction.CreatedBy = Account.UserName;
                                    objAction.CreatedDate = DateTime.Now;
                                    objAction.DefineWFEventID = item.ID;
                                    model.WFL_DefineWFAction.Add(objAction);
                                }
                                objAction.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                                objAction.UserID = user.UserID;
                                objAction.IsUsed = true;
                            }
                        }
                    }
                    #endregion

                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_DefineEventDelete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lstid.Count > 0)
                    {
                        foreach (var item in lstid)
                        {
                            var obj = model.WFL_DefineWFEvent.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                //if (model.WFL_Message.Count(c => c.WFL_Action.EventID == item) > 0)
                                //    throw FaultHelper.BusinessFault(null, null, "Event đã phát sinh, không thể xóa");

                                foreach (var detail in model.WFL_DefineWFAction.Where(c => c.DefineWFEventID == item))
                                    model.WFL_DefineWFAction.Remove(detail);
                                model.WFL_DefineWFEvent.Remove(obj);
                            }
                        }
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSUser> WFLSetting_DefineEventUserRead(int defineID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var listID = model.WFL_DefineGroup.Where(c => c.DefineID == defineID).Select(c => c.SYS_Group.ID);

                    var result = new List<SYSUser>();
                    result = model.SYS_User.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.GroupID > 0 && listID.Contains(c.GroupID.Value))).Select(c => new SYSUser
                    {
                        ID = c.ID,
                        Code = c.Code,
                        UserName = c.UserName,
                        Email = c.Email,
                        TelNo = c.TelNo,
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region Function
        public DTOResult WFLSetting_FunctionList(string request, int workflowID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.ActionID == null).Select(c => new SYSFunction
                    {
                        ID = c.FunctionID,
                        ParentID = c.SYS_Function.ParentID,
                        Code = c.SYS_Function.Code,
                        FunctionName = c.SYS_Function.FunctionName,
                        Description = c.SYS_Function.Description,
                        Icon = c.SYS_Function.Icon,
                        IsAdmin = c.SYS_Function.IsAdmin,
                        IsApproved = c.SYS_Function.IsApproved,
                        IsCustomer = c.SYS_Function.IsCustomer,
                        SortOrder = c.SYS_Function.SortOrder,
                        Level = c.SYS_Function.Level
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<SYSFunction>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLSetting_FunctionNotInList(string request, int workflowID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var hasList = model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.ActionID == null).Select(c => new { c.FunctionID }).ToList();

                    var query = model.SYS_Function.Select(c => new SYSFunction
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        Code = c.Code,
                        FunctionName = c.FunctionName,
                        Description = c.Description,
                        Icon = c.Icon,
                        IsAdmin = c.IsAdmin,
                        IsApproved = c.IsApproved,
                        IsCustomer = c.IsCustomer,
                        SortOrder = c.SortOrder,
                        Level = c.Level
                    }).ToDataSourceResult(CreateRequest(request));
                    var lst = query.Data.Cast<SYSFunction>().ToList();

                    var lstCheck = lst.Where(c => c.Level == 5).ToList();
                    foreach (var item in lstCheck)
                    {
                        if (hasList.Where(c => c.FunctionID == item.ID).Count() > 0)
                            lst.Remove(item);
                    }
                    lstCheck = lst.Where(c => c.Level == 4).ToList();
                    foreach (var item in lstCheck)
                    {
                        if (hasList.Where(c => c.FunctionID == item.ID).Count() > 0 && lst.Where(c => c.ParentID == item.ID).Count() == 0)
                            lst.Remove(item);
                    }
                    lstCheck = lst.Where(c => c.Level == 3).ToList();
                    foreach (var item in lstCheck)
                    {
                        if (hasList.Where(c => c.FunctionID == item.ID).Count() > 0 && lst.Where(c => c.ParentID == item.ID).Count() == 0)
                            lst.Remove(item);
                    }
                    lstCheck = lst.Where(c => c.Level == 2).ToList();
                    foreach (var item in lstCheck)
                    {
                        if (hasList.Where(c => c.FunctionID == item.ID).Count() > 0 && lst.Where(c => c.ParentID == item.ID).Count() == 0)
                            lst.Remove(item);
                    }
                    lstCheck = lst.Where(c => c.Level == 1).ToList();
                    foreach (var item in lstCheck)
                    {
                        if (hasList.Where(c => c.FunctionID == item.ID).Count() > 0 && lst.Where(c => c.ParentID == item.ID).Count() == 0)
                            lst.Remove(item);
                    }

                    result.Total = lst.Count;
                    result.Data = lst.ToArray();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_FunctionNotInSave(int workflowID, List<SYSFunction> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;
                    foreach (var item in lst)
                    {
                        var obj = model.WFL_WorkFlowFunction.FirstOrDefault(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj == null)
                        {
                            obj = new WFL_WorkFlowFunction();
                            obj.WorkFlowID = workflowID;
                            obj.FunctionID = item.ID;
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;

                            model.WFL_WorkFlowFunction.Add(obj);
                        }
                    }
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_FunctionDelete(int workflowID, List<SYSFunction> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;
                    var flag = false;
                    foreach (var item in lst.Where(c => c.Level == 5))
                    {
                        var obj = model.WFL_WorkFlowFunction.FirstOrDefault(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj != null)
                        {
                            model.WFL_WorkFlowFunction.Remove(obj);
                            foreach (var detail in model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID > 0))
                                model.WFL_WorkFlowFunction.Remove(detail);
                            flag = true;
                        }
                    }
                    if (flag)
                        model.SaveChanges();

                    flag = false;
                    foreach (var item in lst.Where(c => c.Level == 4))
                    {
                        var obj = model.WFL_WorkFlowFunction.FirstOrDefault(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj != null && model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.SYS_Function.ParentID == item.ID && c.ActionID == null).Count() == 0)
                        {
                            model.WFL_WorkFlowFunction.Remove(obj);
                            foreach (var detail in model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID > 0))
                                model.WFL_WorkFlowFunction.Remove(detail);
                            flag = true;
                        }
                    }
                    if (flag)
                        model.SaveChanges();

                    flag = false;
                    foreach (var item in lst.Where(c => c.Level == 3))
                    {
                        var obj = model.WFL_WorkFlowFunction.FirstOrDefault(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj != null && model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.SYS_Function.ParentID == item.ID && c.ActionID == null).Count() == 0)
                        {
                            model.WFL_WorkFlowFunction.Remove(obj);
                            foreach (var detail in model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID > 0))
                                model.WFL_WorkFlowFunction.Remove(detail);
                            flag = true;
                        }
                    }
                    if (flag)
                        model.SaveChanges();

                    flag = false;
                    foreach (var item in lst.Where(c => c.Level == 2))
                    {
                        var obj = model.WFL_WorkFlowFunction.FirstOrDefault(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj != null && model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.SYS_Function.ParentID == item.ID && c.ActionID == null).Count() == 0)
                        {
                            model.WFL_WorkFlowFunction.Remove(obj);
                            foreach (var detail in model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID > 0))
                                model.WFL_WorkFlowFunction.Remove(detail);
                            flag = true;
                        }
                    }
                    if (flag)
                        model.SaveChanges();

                    flag = false;
                    foreach (var item in lst.Where(c => c.Level == 1))
                    {
                        var obj = model.WFL_WorkFlowFunction.FirstOrDefault(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID == null);
                        if (obj != null && model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.SYS_Function.ParentID == item.ID && c.ActionID == null).Count() == 0)
                        {
                            model.WFL_WorkFlowFunction.Remove(obj);
                            foreach (var detail in model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.FunctionID == item.ID && c.ActionID > 0))
                                model.WFL_WorkFlowFunction.Remove(detail);
                            flag = true;
                        }
                    }
                    if (flag)
                        model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOSYSAction> WFLSetting_ActionList(int workflowID, int functionID)
        {
            try
            {
                List<DTOSYSAction> result = new List<DTOSYSAction>();
                using (var model = new DataEntities())
                {
                    var objDefine = model.SYS_Function.FirstOrDefault(c => c.ID == functionID);
                    if (objDefine != null)
                    {
                        result = model.SYS_ActionInFunction.Where(c => c.FunctionID == functionID).Select(c => new DTOSYSAction()
                        {
                            ID = c.SYS_Action.ID,
                            Code = c.SYS_Action.Code,
                            ActionName = c.SYS_Action.ActionName,
                            FunctionID = c.FunctionID,
                        }).ToList();

                        var lstWFLFunction = model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.FunctionID == functionID && c.ActionID != null).Select(c => c.ActionID).ToList();
                        if (lstWFLFunction != null)
                        {
                            foreach (var item in result)
                            {
                                if (lstWFLFunction.Contains(item.ID))
                                {
                                    item.IsChoose = true;
                                }
                                else
                                {
                                    item.IsChoose = false;
                                }
                            }
                        }
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy function");
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLStep> WFLSetting_StepParentAllList()
        {
            try
            {
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_Step.Where(c => c.ParentID == null).Select(c => new DTOWFLStep
                    {
                        ID = c.ID,
                        Code = c.Code,
                        StepName = c.StepName,
                        Step = c.Step
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLDefineData WFLSetting_FunctionData(int stepID)
        {
            try
            {
                DTOWFLDefineData result = new DTOWFLDefineData();
                using (var model = new DataEntities())
                {
                    //Get WL select
                    result.StepParentID = stepID;
                    result.maxWFLInStep = 0;
                    List<DTOWFLStep> listStep = new List<DTOWFLStep>();

                    listStep = model.WFL_Step.Where(c => c.ParentID == stepID).Select(c => new DTOWFLStep
                    {
                        ID = c.ID,
                        Code = c.Code,
                        StepName = c.StepName,
                        Step = c.Step,
                        UseAllWorkFlow = c.UseAllWorkFlow,
                    }).ToList();

                    result.ListWLStep = new List<DTOWFLWorkFlowStep>();

                    if (listStep != null)
                    {
                        foreach (var item in listStep)
                        {
                            DTOWFLWorkFlowStep data = new DTOWFLWorkFlowStep();
                            data.StepID = item.ID;
                            data.StepName = item.StepName;
                            data.ListWorkFlow = model.WFL_WorkFlow.Where(c => c.StepID == item.ID && c.ParentID == null).Select(c => new DTOWFLWorkFlow
                            {
                                ID = c.ID,
                                Code = c.Code,
                                WorkFlowName = c.WorkFlowName,
                                StepID = c.StepID,
                                UseAllWorkFlow = item.UseAllWorkFlow,
                            }).ToList();

                            result.ListWLStep.Add(data);
                            if (data.ListWorkFlow.Count > result.maxWFLInStep)
                            {
                                result.maxWFLInStep = data.ListWorkFlow.Count;
                            }
                        }
                    }

                    //Link
                    result.ListWorkFlowLink = new List<DTOWFLWorkFlowLink>();
                    if (listStep != null)
                    {
                        foreach (var item in result.ListWLStep)
                        {
                            if (item.ListWorkFlow != null)
                            {
                                foreach (var wf in item.ListWorkFlow)
                                {
                                    var listWFLLink = model.WFL_WorkFlowLink.Where(c => c.WorkFlowFromID == wf.ID).Select(c => new DTOWFLWorkFlowLink
                                    {
                                        ID = c.ID,
                                        WorkFlowFromID = c.WorkFlowFromID,
                                        WorkFlowToID = c.WorkFlowToID,
                                    }).ToList();

                                    result.ListWorkFlowLink.AddRange(listWFLLink);
                                }
                            }
                        }
                    }

                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_ActionSave(int workflowID, int functionID, List<int> lstActID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objFunc = model.SYS_Function.FirstOrDefault(c => c.ID == functionID);
                    if (objFunc != null)
                    {
                        foreach (var id in lstActID)
                        {
                            var act = model.SYS_Action.FirstOrDefault(c => c.ID == id);
                            if (act != null)
                            {
                                var obj = model.WFL_WorkFlowFunction.FirstOrDefault(c => c.WorkFlowID == workflowID && c.FunctionID == functionID && c.ActionID == id);
                                if (obj == null)
                                {
                                    obj = new WFL_WorkFlowFunction();
                                    obj.CreatedBy = Account.UserName;
                                    obj.CreatedDate = DateTime.Now;
                                    obj.WorkFlowID = workflowID;
                                    obj.FunctionID = functionID;
                                    obj.ActionID = id;
                                    model.WFL_WorkFlowFunction.Add(obj);
                                }
                            }
                            else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy action");

                        }

                        //Delete
                        var lstActSelected = model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == workflowID && c.FunctionID == functionID && c.ActionID != null).Select(c => new { c.ActionID }).ToList();
                        foreach (var item in lstActSelected)
                        {
                            if (!lstActID.Contains(item.ActionID.GetValueOrDefault()))
                            {
                                var obj = model.WFL_WorkFlowFunction.FirstOrDefault(c => c.WorkFlowID == workflowID && c.FunctionID == functionID && c.ActionID == item.ActionID);
                                model.WFL_WorkFlowFunction.Remove(obj);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy function");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        // Danh sách event
        public DTOResult WFLSetting_WFEvent_List(string request, int workflowID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_WorkFlowWFEvent.Where(c => c.WorkFlowID == workflowID).Select(c => new DTOWFL_WFEvent
                    {
                        ID = c.ID,
                        EventID = c.WFEventID,
                        Code = c.WFL_WFEvent.Code,
                        EventName = c.WFL_WFEvent.EventName,
                        Expression = c.WFL_WFEvent.Expression,
                        SortOrder = c.WFL_WFEvent.SortOrder,
                        WorkFlowID = c.WorkFlowID,
                        IsApproved = c.WFL_WFEvent.IsApproved
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFL_WFEvent>;
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLSetting_WFEvent_NotInList(string request, int workflowID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var lstID = model.WFL_WorkFlowWFEvent.Where(c => c.WorkFlowID == workflowID).Select(c => c.WFEventID);

                    var query = model.WFL_WFEvent.Where(c => !lstID.Contains(c.ID)).Select(c => new DTOWFEvent
                    {
                        ID = c.ID,
                        Code = c.Code,
                        EventName = c.EventName,
                        Expression = c.Expression,
                        IsApproved = c.IsApproved,
                        IsChoose = false,
                        UseCustomer = c.UseCustomer,
                        SortOrder = c.SortOrder,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFEvent>;

                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_WFEvent_NotInSaveList(int workflowID, List<int> lstId)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objWorkflow = model.WFL_WorkFlow.FirstOrDefault(c => c.ID == workflowID);
                    if (objWorkflow != null)
                    {
                        foreach (var id in lstId)
                        {
                            var obj = model.WFL_WorkFlowWFEvent.FirstOrDefault(c => c.WorkFlowID == workflowID && c.WFEventID == id);
                            if (obj != null)
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            else
                            {
                                obj = new WFL_WorkFlowWFEvent();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.WFEventID = id;
                                obj.WorkFlowID = workflowID;
                                model.WFL_WorkFlowWFEvent.Add(obj);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy workflow");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_WFEvent_DeleteList(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lstid.Count > 0)
                    {
                        foreach (var item in lstid)
                        {
                            var obj = model.WFL_WorkFlowWFEvent.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                //Get define sử dụng workflow
                                var lstDefine = model.WFL_DefineWF.Where(c => c.WorkFlowID == obj.WorkFlowID).Select(c => c.ID);
                                List<int> lstWFEventID = new List<int>();
                                if (lstDefine != null && lstDefine.Count() > 0)
                                {
                                    foreach (var def in lstDefine)
                                    {
                                        //Get WFEvent của define 
                                        var lst = model.WFL_DefineWFEvent.Where(c => c.DefineWFID == def).Select(c => c.WFEventID).ToList();
                                        lstWFEventID.AddRange(lst);
                                    }
                                }

                                if (lstWFEventID != null && lstWFEventID.Count(c => c == obj.WFEventID) > 0)
                                    throw FaultHelper.BusinessFault(null, null, "Event đã được thiết lập, không thể xóa");

                                model.WFL_WorkFlowWFEvent.Remove(obj);
                            }
                        }
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        //public DTOWFL_WFEvent WFLSetting_WFEventGet(int id)
        //{
        //    try
        //    {
        //        var result = new DTOWFL_WFEvent { ID = -1, Code = "", EventName = "", Expression = "", IsApproved = true, };
        //        result.ListField = new List<DTOWFL_WFEventField>();
        //        using (var model = new DataEntities())
        //        {
        //            if (id > 0)
        //            {
        //                result = model.WFL_WFEvent.Where(c => c.ID == id).Select(c => new DTOWFL_WFEvent
        //                {
        //                    ID = c.ID,
        //                    Code = c.Code,
        //                    EventName = c.EventName,
        //                    Expression = c.Expression,
        //                    IsApproved = c.IsApproved,
        //                }).FirstOrDefault();

        //                if (result.ID > 0)
        //                {
        //                    result.ListField = model.WFL_WFEventField.Where(c => c.WFEventID == result.ID).Select(c => new DTOWFL_WFEventField
        //                    {
        //                        ID = c.ID,
        //                        CompareValue = c.CompareValue,
        //                        FieldID = c.FieldID,
        //                        FieldCode = c.WFL_Field.ColumnName,
        //                        FieldName = c.WFL_Field.ColumnName,
        //                        OperatorCode = c.OperatorCode,
        //                        OperatorValue = c.OperatorValue,
        //                        TableCode = c.WFL_Field.TableName,
        //                        Type = c.WFL_Field.ColumnType,
        //                        IsModified = c.IsModified,
        //                        IsChoose = false
        //                    }).ToList();

        //                    var lstTemplate = model.WFL_WFEventTemplate.Where(c => c.WFEventID == result.ID).Select(c => new DTOWFL_WFEventTemplate
        //                        {
        //                            ID = c.ID,
        //                            TemplateID = c.TemplateID,
        //                            TypeOfActionID = c.TypeOfActionID,
        //                            WFEventID = c.WFEventID
        //                        }).ToList();

        //                    if (lstTemplate.Count > 0)
        //                    {
        //                        var SendMail = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
        //                        if (SendMail != null)
        //                            result.SendMail_TemplateID = SendMail.TemplateID;
        //                        var SMS = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
        //                        if (SMS != null)
        //                            result.SMS_TemplateID = SMS.TemplateID;
        //                        var MessageTMS = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
        //                        if (MessageTMS != null)
        //                            result.MessageTMS_TemplateID = MessageTMS.TemplateID;
        //                    }
        //                }
        //            }
        //        }
        //        return result;
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.BusinessFault(ex);
        //    }
        //}

        //public int WFLSetting_WFEventSave(DTOWFL_WFEvent item, int workflowID)
        //{
        //    try
        //    {
        //        using (var model = new DataEntities())
        //        {
        //            model.EventAccount = Account; model.EventRunning = false;

        //            var obj = model.WFL_WFEvent.FirstOrDefault(c => c.ID == item.ID);
        //            if (obj == null)
        //            {
        //                obj = new WFL_WFEvent();
        //                obj.CreatedBy = Account.UserName;
        //                obj.CreatedDate = DateTime.Now;
        //                //obj.WorkFlowID = workflowID;
        //                model.WFL_WFEvent.Add(obj);
        //            }
        //            else
        //            {
        //                obj.ModifiedBy = Account.UserName;
        //                obj.ModifiedDate = DateTime.Now;
        //            }
        //            obj.Code = item.Code;
        //            obj.EventName = item.EventName;
        //            obj.IsApproved = item.IsApproved;
        //            obj.Expression = string.Empty;

        //            // Xóa data cũ
        //            foreach (var detail in model.WFL_WFEventField.Where(c => c.WFEventID == item.ID))
        //                model.WFL_WFEventField.Remove(detail);
        //            foreach (var detail in model.WFL_WFEventTemplate.Where(c => c.WFEventID == item.ID))
        //                model.WFL_WFEventTemplate.Remove(detail);

        //            // Lưu danh sách field
        //            if (item.ListField != null)
        //            {
        //                foreach (var field in item.ListField)
        //                {
        //                    var objField = new WFL_WFEventField();
        //                    objField.CreatedBy = Account.UserName;
        //                    objField.CreatedDate = DateTime.Now;
        //                    objField.FieldID = field.FieldID;
        //                    objField.OperatorCode = field.OperatorCode == null ? string.Empty : field.OperatorCode;
        //                    objField.OperatorValue = field.OperatorValue;
        //                    objField.CompareValue = field.CompareValue;
        //                    objField.IsModified = field.IsModified;
        //                    if (string.IsNullOrEmpty(field.CompareValue))
        //                        field.CompareValue = objField.CompareValue = "null";
        //                    obj.WFL_WFEventField.Add(objField);
        //                }

        //                obj.Expression = WFLSetting_GenerateExpression(item.ListField);
        //            }

        //            // Lưu template mail
        //            if (item.SendMail_TemplateID > 0)
        //            {
        //                var objTemplate = new WFL_WFEventTemplate();
        //                objTemplate.CreatedBy = Account.UserName;
        //                objTemplate.CreatedDate = DateTime.Now;
        //                objTemplate.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
        //                objTemplate.WFL_WFEvent = obj;
        //                objTemplate.TemplateID = item.SendMail_TemplateID;
        //                model.WFL_WFEventTemplate.Add(objTemplate);
        //            }

        //            // Lưu template tms
        //            if (item.MessageTMS_TemplateID > 0)
        //            {
        //                var objTemplate = new WFL_WFEventTemplate();
        //                objTemplate.CreatedBy = Account.UserName;
        //                objTemplate.CreatedDate = DateTime.Now;
        //                objTemplate.TypeOfActionID = (int)WFLTypeOfAction.MessageTMS;
        //                objTemplate.WFL_WFEvent = obj;
        //                objTemplate.TemplateID = item.MessageTMS_TemplateID;
        //                model.WFL_WFEventTemplate.Add(objTemplate);
        //            }

        //            // Lưu template sms
        //            if (item.SMS_TemplateID > 0)
        //            {
        //                var objTemplate = new WFL_WFEventTemplate();
        //                objTemplate.CreatedBy = Account.UserName;
        //                objTemplate.CreatedDate = DateTime.Now;
        //                objTemplate.TypeOfActionID = (int)WFLTypeOfAction.SMS;
        //                objTemplate.WFL_WFEvent = obj;
        //                objTemplate.TemplateID = item.SMS_TemplateID;
        //                model.WFL_WFEventTemplate.Add(objTemplate);
        //            }

        //            model.SaveChanges();

        //            return obj.ID;
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.BusinessFault(ex);
        //    }
        //}

        //public void WFLSetting_WFEventDelete(List<int> lstid)
        //{
        //    try
        //    {
        //        using (var model = new DataEntities())
        //        {
        //            if (lstid.Count > 0)
        //            {
        //                foreach (var item in lstid)
        //                {
        //                    var obj = model.WFL_WFEvent.FirstOrDefault(c => c.ID == item);
        //                    if (obj != null)
        //                    {
        //                        if (model.WFL_DefineWFEvent.Count(c => c.WFEventID == item) > 0)
        //                            throw FaultHelper.BusinessFault(null, null, "Event đã được thiết lập, không thể xóa");
        //                        else
        //                        {
        //                            foreach (var detail in model.WFL_WFEventField.Where(c => c.WFEventID == item))
        //                                model.WFL_WFEventField.Remove(detail);
        //                            foreach (var detail in model.WFL_WFEventTemplate.Where(c => c.WFEventID == item))
        //                                model.WFL_WFEventTemplate.Remove(detail);
        //                        }
        //                        model.WFL_WFEvent.Remove(obj);
        //                    }
        //                }
        //                model.SaveChanges();
        //            }
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.BusinessFault(ex);
        //    }
        //}

        // Danh sách flow con
        public DTOResult WFLSetting_ChildrenFlow(string request, int workflowID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_WorkFlow.Where(c => c.ParentID == workflowID).Select(c => new DTOWFLWorkFlow
                    {
                        ID = c.ID,
                        Code = c.Code,
                        WorkFlowName = c.WorkFlowName,
                        StepID = c.StepID,
                        UseAllWorkFlow = false
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLWorkFlow>;
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public int WFLSetting_ChildrenFlow_Save(DTOWFLWorkFlow item, int workflowID)
        {
            try
            {
                DTOError result = new DTOError();
                using (var model = new DataEntities())
                {
                    var objWorkflow = model.WFL_WorkFlow.FirstOrDefault(c => c.ID == workflowID);
                    if (objWorkflow == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy workflow");

                    model.EventAccount = Account; model.EventRunning = true;

                    var obj = model.WFL_WorkFlow.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new WFL_WorkFlow();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.ParentID = workflowID;
                        obj.StepID = objWorkflow.StepID;
                        model.WFL_WorkFlow.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.WorkFlowName = item.WorkFlowName;
                    model.SaveChanges();
                    return obj.ID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOWFLWorkFlow WFLSetting_ChildrenFlow_Get(int id)
        {
            try
            {
                DTOWFLWorkFlow result = new DTOWFLWorkFlow { ID = 0 };
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.WFL_WorkFlow.Where(c => c.ID == id).Select(c => new DTOWFLWorkFlow
                        {
                            ID = c.ID,
                            Code = c.Code,
                            WorkFlowName = c.WorkFlowName,
                            StepID = c.StepID,
                            UseAllWorkFlow = false
                        }).FirstOrDefault();
                    }
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void WFLSetting_ChildrenFlow_Delete(int id)
        {
            try
            {
                DTOError result = new DTOError();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;
                    var obj = model.WFL_WorkFlow.FirstOrDefault(c => c.ID == id && c.ParentID != null);
                    if (obj != null)
                    {
                        var lstFunc = model.WFL_WorkFlowFunction.Where(c => c.WorkFlowID == id);
                        if (lstFunc != null)
                        {
                            model.WFL_WorkFlowFunction.RemoveRange(lstFunc);
                        }
                        model.WFL_WorkFlow.Remove(obj);
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region Packing
        public DTOResult WFLPacket_Read(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_Packet.Where(c => c.WFL_PacketSetting.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOWFLPacket
                    {
                        ID = c.ID,
                        PacketDate = c.PacketDate,
                        SettingCode = c.WFL_PacketSetting.Code,
                        SettingName = c.WFL_PacketSetting.SettingName,
                        SettingType = c.WFL_PacketSetting.SYS_Var.ValueOfVar,
                        CustomerName = c.WFL_PacketSetting.CUSSettingID > 0 ? c.WFL_PacketSetting.CUS_Customer.CustomerName : string.Empty,
                        PacketProcess = c.SYS_Var.ValueOfVar,
                        PacketProcessID = c.PacketProcessID,
                        IsCheck = c.PacketProcessID == -(int)SYSVarType.PacketProcessOpen ? true : false,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLPacket>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public DTOWFLPacket WFLPacket_Get(long id)
        {
            try
            {
                DTOWFLPacket result = new DTOWFLPacket();

                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.WFL_Packet.Where(c => c.ID == id).Select(c => new DTOWFLPacket
                        {
                            ID = c.ID,
                            PacketDate = c.PacketDate,
                            PacketSettingID = c.PacketSettingID,
                            PacketProcessID = c.PacketProcessID,
                            PacketSettingTypeID = c.WFL_PacketSetting.PacketSettingTypeID,
                        }).FirstOrDefault();

                        if (result != null)
                        {
                            if (result.PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeOrderConfirmAll)
                            {
                                result.PacketSettingTypeID = 1;
                            }
                            else if (result.PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeOrderConfirmPlan)
                            {
                                result.PacketSettingTypeID = 2;
                            }
                            else if (result.PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeTOMasterChange)
                            {
                                result.PacketSettingTypeID = 3;
                            }
                            else if (result.PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeTOMasterOwner)
                            {
                                result.PacketSettingTypeID = 4;
                            }
                        }
                    }
                    else
                    {
                        result.ID = -1;
                        var objPacketSetting = model.WFL_PacketSetting.Where(c => c.SYSCustomerID == Account.SYSCustomerID).FirstOrDefault();
                        if (objPacketSetting != null)
                        {
                            result.PacketSettingID = objPacketSetting.ID;
                        }
                        else
                            result.PacketSettingID = -1;

                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public long WFLPacket_Save(DTOWFLPacket item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    var obj = model.WFL_Packet.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new WFL_Packet();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.WFL_Packet.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.PacketProcessID = item.PacketProcessID;
                    obj.PacketSettingID = item.PacketSettingID;
                    obj.PacketDate = item.PacketDate;

                    model.SaveChanges();

                    return obj.ID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLPacket_Delete(long id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var obj = model.WFL_Packet.FirstOrDefault(c => c.ID == id);
                    if (obj != null)
                    {
                        var lst = model.WFL_PacketDetail.Where(c => c.PacketID == id);
                        if (lst != null)
                        {
                            model.WFL_PacketDetail.RemoveRange(lst);
                        }
                        model.WFL_Packet.Remove(obj);
                    }
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLPacket_Send(List<long> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;
                    foreach (var item in lstid)
                    {
                        var obj = model.WFL_Packet.FirstOrDefault(c => c.ID == item && c.PacketProcessID == -(int)SYSVarType.PacketProcessOpen);
                        if (obj != null)
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                            obj.PacketProcessID = -(int)SYSVarType.PacketProcessClose;
                            obj.TimeSendOffer = DateTime.Now;
                        }
                    }
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLPacket_ORDGroupProductList(long packetID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    int iLCL = -(int)SYSVarType.TransportModeLCL;
                    int iFCL = -(int)SYSVarType.TransportModeFCL;

                    var query = model.WFL_PacketDetail.Where(c => c.PacketID == packetID && c.ORDGroupProductID > 0).Select(c => new DTOWFLPacket_Detail
                    {
                        ID = c.ID,
                        ORDGroupProductID = c.ORDGroupProductID,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                        CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        CustomerName = c.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                        ServiceOfOrderName = c.ORD_GroupProduct.ORD_Order.ServiceOfOrderID > 0 ? c.ORD_GroupProduct.ORD_Order.CAT_ServiceOfOrder.Name : "",
                        TransportModeName = c.ORD_GroupProduct.ORD_Order.CAT_TransportMode.Name,
                        TypeOfContractName = c.ORD_GroupProduct.ORD_Order.SYS_Var2.ValueOfVar,
                        ETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        ETA = c.ORD_GroupProduct.ORD_Order.ETA,

                        TotalTon = c.ORD_GroupProduct.ORD_Order.CAT_TransportMode.TransportModeID == iFCL ? (c.ORD_GroupProduct.ORD_Order.ORD_Container.Count > 0 ? c.ORD_GroupProduct.ORD_Order.ORD_Container.Sum(o => o.Ton) : 0) : (c.ORD_GroupProduct.ORD_Order.ORD_GroupProduct.Count > 0 ? c.ORD_GroupProduct.ORD_Order.ORD_GroupProduct.Sum(o => o.Ton) : 0),
                        TotalCBM = c.ORD_GroupProduct.ORD_Order.CAT_TransportMode.TransportModeID == iFCL ? 0 : (c.ORD_GroupProduct.ORD_Order.ORD_GroupProduct.Count > 0 ? c.ORD_GroupProduct.ORD_Order.ORD_GroupProduct.Sum(o => o.CBM) : 0),
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLPacket_Detail>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLPacket_ORDGroupProductNotInList(long packetID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    int iLCL = -(int)SYSVarType.TransportModeLCL;
                    int iFCL = -(int)SYSVarType.TransportModeFCL;

                    var listHasID = model.WFL_PacketDetail.Where(c => c.ORDGroupProductID > 0 && c.PacketID == packetID).Select(c => c.ORDGroupProductID);
                    var query = model.ORD_GroupProduct.Where(c => !listHasID.Contains(c.ID)).Select(c => new DTOWFLPacket_Detail
                    {
                        ID = c.ID,
                        OrderCode = c.ORD_Order.Code,
                        CustomerCode = c.ORD_Order.CUS_Customer.Code,
                        CustomerName = c.ORD_Order.CUS_Customer.CustomerName,
                        ServiceOfOrderName = c.ORD_Order.ServiceOfOrderID > 0 ? c.ORD_Order.CAT_ServiceOfOrder.Name : "",
                        TransportModeName = c.ORD_Order.CAT_TransportMode.Name,
                        TypeOfContractName = c.ORD_Order.SYS_Var2.ValueOfVar,
                        ETD = c.ORD_Order.ETD,
                        ETA = c.ORD_Order.ETA,
                        TotalTon = c.ORD_Order.CAT_TransportMode.TransportModeID == iFCL ? (c.ORD_Order.ORD_Container.Count > 0 ? c.ORD_Order.ORD_Container.Sum(o => o.Ton) : 0) : (c.ORD_Order.ORD_GroupProduct.Count > 0 ? c.ORD_Order.ORD_GroupProduct.Sum(o => o.Ton) : 0),
                        TotalCBM = c.ORD_Order.CAT_TransportMode.TransportModeID == iFCL ? 0 : (c.ORD_Order.ORD_GroupProduct.Count > 0 ? c.ORD_Order.ORD_GroupProduct.Sum(o => o.CBM) : 0),
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLPacket_Detail>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLPacket_ORDGroupProductSaveList(long packetID, List<int> lstId)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objPacket = model.WFL_Packet.FirstOrDefault(c => c.ID == packetID);
                    if (objPacket != null)
                    {
                        foreach (var id in lstId)
                        {
                            var obj = model.WFL_PacketDetail.FirstOrDefault(c => c.PacketID == packetID && c.ORDGroupProductID == id);
                            if (obj != null)
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            else
                            {
                                obj = new WFL_PacketDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PacketID = packetID;
                                obj.ORDGroupProductID = id;
                                model.WFL_PacketDetail.Add(obj);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy packet");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLPacket_DITOMasterList(long packetID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_PacketDetail.Where(c => c.PacketID == packetID && c.DITOMasterID > 0).Select(c => new DTOWFLPacket_Detail
                    {
                        ID = c.ID,
                        DITOMasterID = c.DITOMasterID,
                        MasterCode = c.OPS_DITOMaster.Code,
                        VehicleCode = c.OPS_DITOMaster.CAT_Vehicle.RegNo,
                        DriverName = c.OPS_DITOMaster.FLM_Driver.CAT_Driver.FirstName + " " + c.OPS_DITOMaster.FLM_Driver.CAT_Driver.LastName,
                        VendorName = c.OPS_DITOMaster.CUS_Customer.CustomerName,

                        ETD = c.OPS_DITOMaster.ETD,
                        ETA = c.OPS_DITOMaster.ETA,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLPacket_Detail>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLPacket_DITOMasterNotInList(long packetID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var listHasID = model.WFL_PacketDetail.Where(c => c.DITOMasterID > 0 && c.PacketID == packetID).Select(c => c.DITOMasterID);
                    var query = model.OPS_DITOMaster.Where(c => !listHasID.Contains(c.ID) && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterPlanning && c.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered && c.VehicleID > 0).Select(c => new DTOWFLPacket_Detail
                    {
                        ID = c.ID,
                        MasterCode = c.Code,
                        VehicleCode = c.CAT_Vehicle.RegNo,
                        DriverName = c.FLM_Driver.CAT_Driver.FirstName + " " + c.FLM_Driver.CAT_Driver.LastName,
                        VendorName = c.CUS_Customer.CustomerName,

                        ETD = c.ETD,
                        ETA = c.ETA,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLPacket_Detail>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLPacket_DITOMasterSaveList(long packetID, List<int> lstId)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objPacket = model.WFL_Packet.FirstOrDefault(c => c.ID == packetID);
                    if (objPacket != null)
                    {
                        foreach (var id in lstId)
                        {
                            var obj = model.WFL_PacketDetail.FirstOrDefault(c => c.PacketID == packetID && c.DITOMasterID == id);
                            if (obj != null)
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            else
                            {
                                obj = new WFL_PacketDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PacketID = packetID;
                                obj.DITOMasterID = id;
                                model.WFL_PacketDetail.Add(obj);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy packet");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOResult WFLPacket_COTOMasterList(long packetID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_PacketDetail.Where(c => c.PacketID == packetID && c.COTOMasterID > 0).Select(c => new DTOWFLPacket_Detail
                    {
                        ID = c.ID,
                        COTOMasterID = c.COTOMasterID,
                        MasterCode = c.OPS_COTOMaster.Code,
                        VehicleCode = c.OPS_COTOMaster.CAT_Vehicle.RegNo,
                        DriverName = c.OPS_COTOMaster.FLM_Driver.CAT_Driver.FirstName + " " + c.OPS_COTOMaster.FLM_Driver.CAT_Driver.LastName,
                        VendorName = c.OPS_COTOMaster.CUS_Customer.CustomerName,

                        ETD = c.ORD_GroupProduct.ORD_Order.ETD,
                        ETA = c.ORD_GroupProduct.ORD_Order.ETA,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLPacket_Detail>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public DTOResult WFLPacket_COTOMasterNotInList(long packetID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var listHasID = model.WFL_PacketDetail.Where(c => c.COTOMasterID > 0 && c.PacketID == packetID).Select(c => c.COTOMasterID);
                    var query = model.OPS_COTOMaster.Where(c => !listHasID.Contains(c.ID) && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterPHT && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterTendered && c.VehicleID > 0).Select(c => new DTOWFLPacket_Detail
                    {
                        ID = c.ID,
                        MasterCode = c.Code,
                        VehicleCode = c.CAT_Vehicle.RegNo,
                        DriverName = c.FLM_Driver.CAT_Driver.FirstName + " " + c.FLM_Driver.CAT_Driver.LastName,
                        VendorName = c.CUS_Customer.CustomerName,

                        ETD = c.ETD,
                        ETA = c.ETA,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLPacket_Detail>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLPacket_COTOMasterSaveList(long packetID, List<int> lstId)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objPacket = model.WFL_Packet.FirstOrDefault(c => c.ID == packetID);
                    if (objPacket != null)
                    {
                        foreach (var id in lstId)
                        {
                            var obj = model.WFL_PacketDetail.FirstOrDefault(c => c.PacketID == packetID && c.COTOMasterID == id);
                            if (obj != null)
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            else
                            {
                                obj = new WFL_PacketDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PacketID = packetID;
                                obj.COTOMasterID = id;
                                model.WFL_PacketDetail.Add(obj);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy packet");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }


        public void WFLPacket_DetailDelete(long packetID, List<long> lstId)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objPacket = model.WFL_Packet.FirstOrDefault(c => c.ID == packetID);
                    if (objPacket != null)
                    {
                        foreach (var id in lstId)
                        {
                            var obj = model.WFL_PacketDetail.FirstOrDefault(c => c.PacketID == packetID && c.ID == id);
                            if (obj != null)
                            {
                                model.WFL_PacketDetail.Remove(obj);
                            }
                        }

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy packet");
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #region Packet Setting
        public List<SYSVar> WFLPacket_SettingType()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                var obj = new SYSVar();
                obj.ID = -(int)SYSVarType.PacketSettingTypeOrderConfirmAll;
                obj.Code = "PacketSettingTypeOrderConfirmAll";
                obj.TypeOfVar = 1;
                obj.ValueOfVar = "Xác nhận đơn hàng";
                result.Add(obj);

                obj = new SYSVar();
                obj.ID = -(int)SYSVarType.PacketSettingTypeOrderConfirmPlan;
                obj.Code = "PacketSettingTypeOrderConfirmPlan";
                obj.TypeOfVar = 2;
                obj.ValueOfVar = "Xác nhận đơn hàng lập kế hoạch";
                result.Add(obj);

                obj = new SYSVar();
                obj.ID = -(int)SYSVarType.PacketSettingTypeTOMasterChange;
                obj.Code = "PacketSettingTypeTOMasterChange";
                obj.TypeOfVar = 3;
                obj.ValueOfVar = "Đổi chuyến vận chuyển";
                result.Add(obj);

                obj = new SYSVar();
                obj.ID = -(int)SYSVarType.PacketSettingTypeTOMasterOwner;
                obj.Code = "PacketSettingTypeTOMasterOwner";
                obj.TypeOfVar = 4;
                obj.ValueOfVar = "Xác nhận chuyến tài xế";
                result.Add(obj);

                obj = new SYSVar();
                obj.ID = -(int)SYSVarType.PacketSettingTypeEvent;
                obj.Code = "PacketSettingTypeTOMasterOwner";
                obj.TypeOfVar = 5;
                obj.ValueOfVar = "Theo sự kiện";
                result.Add(obj);

                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public List<DTOWFLPacket_Setting> WFLPacket_SettingAllList()
        {
            try
            {
                List<DTOWFLPacket_Setting> result = new List<DTOWFLPacket_Setting>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_PacketSetting.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOWFLPacket_Setting
                    {
                        ID = c.ID,
                        SettingName = c.SettingName,
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public DTOResult WFLPacket_SettingRead(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_PacketSetting.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOWFLPacket_Setting
                    {
                        ID = c.ID,
                        Code = c.Code,
                        SettingName = c.SettingName,
                        PacketSettingTypeID = c.PacketSettingTypeID,
                        PacketSettingType = c.SYS_Var.ValueOfVar,
                        IsApproved = c.IsApproved,
                        IsChoose = false,
                        CustomerID = c.CustomerID,
                        CustomerCode = c.CustomerID > 0 ? c.CUS_Customer.Code : "",
                        CustomerName = c.CustomerID > 0 ? c.CUS_Customer.CustomerName : "",
                        IsAutoCollect = c.IsAutoCollect,
                        IsAutoSend = c.IsAutoSend,
                        IsAutoSendEmpty = c.IsAutoSendEmpty,
                        TimeSend1 = c.TimeSend1,
                        TimeSend2 = c.TimeSend2,
                        IsDriverSMS = c.IsDriverSMS,
                        DriverSMSTemplateID = c.DriverSMSTemplateID,
                        IsDriverMail = c.IsDriverMail,
                        DriverMailTemplateID = c.DriverMailTemplateID,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFLPacket_Setting>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLPacket_Setting WFLPacket_SettingGet(int id)
        {
            try
            {
                var result = new DTOWFLPacket_Setting { ID = -1, Code = "", SettingName = "", IsApproved = true, CustomerID = -1, CUSSettingID = -1 };
                result.lstUserMail = new List<DTOWFLPacketSetting_ActionUser>();
                result.lstUserSMS = new List<DTOWFLPacketSetting_ActionUser>();
                result.lstTemplate = new List<DTOWFLPacketSetting_Template>();
                result.lstCustomer = new List<DTOWFLPacketSetting_ActionCustomer>();
                result.lstEvent = new List<DTOWFLEvent>();
                result.MailTemplateContent = result.SMSTemplateContent = string.Empty;
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.WFL_PacketSetting.Where(c => c.ID == id).Select(c => new DTOWFLPacket_Setting
                        {
                            ID = c.ID,
                            Code = c.Code,
                            SettingName = c.SettingName,
                            PacketSettingTypeID = c.PacketSettingTypeID,
                            PacketSettingType = c.SYS_Var.ValueOfVar,
                            IsApproved = c.IsApproved,
                            IsChoose = false,
                            CustomerID = c.CustomerID != null ? c.CustomerID : -1,
                            CUSSettingID = c.CUSSettingID != null ? c.CUSSettingID : -1,
                            IsAutoCollect = c.IsAutoCollect,
                            IsAutoSend = c.IsAutoSend,
                            IsAutoSendEmpty = c.IsAutoSendEmpty,
                            TimeSend1 = c.TimeSend1,
                            TimeSend2 = c.TimeSend2,
                            IsDriverSMS = c.IsDriverSMS,
                            DriverSMSTemplateID = c.DriverSMSTemplateID,
                            IsDriverMail = c.IsDriverMail,
                            DriverMailTemplateID = c.DriverMailTemplateID,
                        }).FirstOrDefault();

                        if (result.ID > 0)
                        {
                            var lstevent = model.WFL_Event.Where(c => c.PacketSettingID == result.ID).Select(c => new DTOWFLEvent
                            {
                                ID = c.ID,
                                Code = c.Code,
                                EventName = c.EventName,
                                Expression = c.Expression,
                                IsApproved = c.IsApproved,
                                IsChoose = false,
                                IsExpr = false,
                                IsSystem = c.IsSystem
                            }).Distinct().ToList();
                            result.lstEvent = lstevent;
                        }

                        if (result.ID > 0)
                        {

                            var lstUserMail = model.WFL_PacketSettingAction.Where(c => c.PacketSettingID == result.ID && c.IsUse && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail).Select(c => new
                            {
                                UserID = c.UserID,
                                UserName = c.SYS_User.UserName,
                                Email = c.SYS_User.Email,
                                TelNo = c.SYS_User.TelNo,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.lstUserMail = lstUserMail.Select(c => new DTOWFLPacketSetting_ActionUser { UserID = c.UserID, UserName = c.UserName, Email = c.Email, TelNo = c.TelNo, IsChoose = c.IsChoose }).ToList();

                            var lstUserSMS = model.WFL_PacketSettingAction.Where(c => c.PacketSettingID == result.ID && c.IsUse && c.TypeOfActionID == (int)WFLTypeOfAction.SMS).Select(c => new
                            {
                                UserID = c.UserID,
                                UserName = c.SYS_User.UserName,
                                Email = c.SYS_User.Email,
                                TelNo = c.SYS_User.TelNo,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.lstUserSMS = lstUserSMS.Select(c => new DTOWFLPacketSetting_ActionUser { UserID = c.UserID, UserName = c.UserName, Email = c.Email, TelNo = c.TelNo, IsChoose = c.IsChoose }).ToList();

                            var lstTemplate = model.WFL_PacketSettingTemplate.Where(c => c.PacketSettingID == result.ID).Select(c => new DTOWFLPacketSetting_Template
                            {
                                Template = c.WFL_Template.Template,
                                TemplateDetail = c.WFL_Template.TemplateDetail,
                                TemplateID = c.TemplateID,
                                TypeOfActionID = c.TypeOfActionID
                            }).ToList();

                            var mailTemp = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                            if (mailTemp != null)
                            {
                                result.MailTemplateID = mailTemp.TemplateID;
                                result.MailTemplateContent = mailTemp.Template;
                            }

                            var smsTemp = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                            if (smsTemp != null)
                            {
                                result.SMSTemplateID = smsTemp.TemplateID;
                                result.SMSTemplateContent = smsTemp.Template;
                            }
                        }
                    }
                    else
                    {
                        result.PacketSettingTypeID = -(int)SYSVarType.PacketSettingTypeOrderConfirmAll;
                        result.CustomerID = -1;
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLEvent WFLPacketSetting_EventGet(int id)
        {
            try
            {
                var result = new DTOWFLEvent { ID = -1, Code = "", EventName = "", Expression = "", IsApproved = true, PacketSettingID = null };
                result.lstField = new List<DTOWFLEvent_Field>();
                result.lstUserMail = new List<DTOWFLEvent_ActionUser>();
                result.lstUserTMS = new List<DTOWFLEvent_ActionUser>();
                result.lstUserSMS = new List<DTOWFLEvent_ActionUser>();
                result.lstTemplate = new List<DTOWFLEvent_Template>();
                result.lstCustomer = new List<DTOWFLEvent_ActionCustomer>();
                result.MailTemplateContent = result.SMSTemplateContent = result.TMSTemplateContent = string.Empty;
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.WFL_Event.Where(c => c.ID == id).Select(c => new DTOWFLEvent
                        {
                            ID = c.ID,
                            Code = c.Code,
                            EventName = c.EventName,
                            Expression = c.Expression,
                            IsApproved = c.IsApproved,
                            IsChoose = false,
                            IsSystem = c.IsSystem,
                            PacketSettingID = c.PacketSettingID,
                        }).FirstOrDefault();

                        if (result.ID > 0)
                        {
                            var lstField = model.WFL_EventField.Where(c => c.EventID == result.ID).Select(c => new DTOWFLEvent_Field
                            {
                                ID = c.ID,
                                CompareValue = c.CompareValue,
                                FieldID = c.FieldID,
                                FieldCode = c.WFL_Field.ColumnName,
                                FieldName = c.WFL_Field.ColumnName,
                                OperatorCode = c.OperatorCode,
                                OperatorValue = c.OperatorValue,
                                TableCode = c.WFL_Field.TableName,
                                Type = c.WFL_Field.ColumnType,
                                IsModified = c.IsModified,
                                IsChoose = false
                            }).ToList();
                            result.lstField = lstField;

                            var lstUserMail = model.WFL_Action.Where(c => c.EventID == result.ID && c.UserID.HasValue && c.IsUse && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail).Select(c => new
                            {
                                UserID = c.UserID,
                                UserName = c.SYS_User.UserName,
                                Email = c.SYS_User.Email,
                                TelNo = c.SYS_User.TelNo,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.lstUserMail = lstUserMail.Select(c => new DTOWFLEvent_ActionUser { UserID = c.UserID, UserName = c.UserName, Email = c.Email, IsChoose = c.IsChoose }).ToList();

                            var lstUserTMS = model.WFL_Action.Where(c => c.EventID == result.ID && c.UserID.HasValue && c.IsUse && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS).Select(c => new
                            {
                                UserID = c.UserID,
                                UserName = c.SYS_User.UserName,
                                Email = c.SYS_User.Email,
                                TelNo = c.SYS_User.TelNo,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.lstUserTMS = lstUserTMS.Select(c => new DTOWFLEvent_ActionUser { UserID = c.UserID, UserName = c.UserName, Email = c.Email, IsChoose = c.IsChoose }).ToList();

                            var lstUserSMS = model.WFL_Action.Where(c => c.EventID == result.ID && c.UserID.HasValue && c.IsUse && c.TypeOfActionID == (int)WFLTypeOfAction.SMS).Select(c => new
                            {
                                UserID = c.UserID,
                                UserName = c.SYS_User.UserName,
                                Email = c.SYS_User.Email,
                                TelNo = c.SYS_User.TelNo,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.lstUserSMS = lstUserSMS.Select(c => new DTOWFLEvent_ActionUser { UserID = c.UserID, UserName = c.UserName, Email = c.Email, IsChoose = c.IsChoose }).ToList();

                            var lstCustomer = model.WFL_Action.Where(c => c.EventID == result.ID && c.CustomerID.HasValue).Select(c => new
                            {
                                CustomerID = c.CustomerID,
                                CustomerName = c.CUS_Customer.CustomerName,
                                IsChoose = false
                            }).Distinct().ToList();
                            result.lstCustomer = lstCustomer.Select(c => new DTOWFLEvent_ActionCustomer { CustomerID = c.CustomerID, CustomerName = c.CustomerName, IsChoose = c.IsChoose }).ToList();

                            var lstTemplate = model.WFL_EventTemplate.Where(c => c.EventID == result.ID).Select(c => new DTOWFLEvent_Template
                            {
                                Template = c.WFL_Template.Template,
                                TemplateID = c.TemplateID,
                                TypeOfActionID = c.TypeOfActionID
                            }).ToList();

                            var mailTemp = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                            if (mailTemp != null)
                            {
                                result.MailTemplateID = mailTemp.TemplateID;
                                result.MailTemplateContent = mailTemp.Template;
                            }
                            var tmsTemp = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                            if (tmsTemp != null)
                            {
                                result.TMSTemplateID = tmsTemp.TemplateID;
                                result.TMSTemplateContent = tmsTemp.Template;
                            }
                            var smsTemp = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                            if (smsTemp != null)
                            {
                                result.SMSTemplateID = smsTemp.TemplateID;
                                result.SMSTemplateContent = smsTemp.Template;
                            }
                        }
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public int WFLPacketSetting_EventSave(DTOWFLEvent item, int packetSettingID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    var obj = model.WFL_Event.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new WFL_Event();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        model.WFL_Event.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.EventName = item.EventName;
                    obj.IsApproved = item.IsApproved;
                    obj.PacketSettingID = packetSettingID;
                    obj.Expression = item.Expression;

                    // Xóa data cũ
                    foreach (var detail in model.WFL_EventField.Where(c => c.EventID == item.ID))
                        model.WFL_EventField.Remove(detail);

                    // Lưu danh sách field
                    if (item.lstField != null)
                    {
                        foreach (var field in item.lstField)
                        {
                            var objField = new WFL_EventField();
                            objField.CreatedBy = Account.UserName;
                            objField.CreatedDate = DateTime.Now;
                            objField.FieldID = field.FieldID;
                            objField.OperatorCode = field.OperatorCode == null ? string.Empty : field.OperatorCode;
                            objField.OperatorValue = field.OperatorValue;
                            objField.CompareValue = field.CompareValue;
                            objField.IsModified = field.IsModified;
                            if (string.IsNullOrEmpty(field.CompareValue))
                                field.CompareValue = objField.CompareValue = "null";

                            obj.WFL_EventField.Add(objField);
                        }
                        if (item.IsExpr)
                            obj.Expression = WFLSetting_GenerateExpression(item.lstField);
                    }

                    // Lưu template mail
                    if (item.MailTemplateID > 0)
                    {
                        var objTemplate = model.WFL_EventTemplate.FirstOrDefault(c => c.EventID == obj.ID && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                        if (objTemplate == null)
                        {
                            objTemplate = new WFL_EventTemplate();
                            objTemplate.CreatedBy = Account.UserName;
                            objTemplate.CreatedDate = DateTime.Now;
                            objTemplate.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                            objTemplate.WFL_Event = obj;
                            model.WFL_EventTemplate.Add(objTemplate);
                        }
                        objTemplate.TemplateID = item.MailTemplateID;
                    }

                    // Lưu template tms
                    if (item.TMSTemplateID > 0)
                    {
                        var objTemplate = model.WFL_EventTemplate.FirstOrDefault(c => c.EventID == obj.ID && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                        if (objTemplate == null)
                        {
                            objTemplate = new WFL_EventTemplate();
                            objTemplate.CreatedBy = Account.UserName;
                            objTemplate.CreatedDate = DateTime.Now;
                            objTemplate.TypeOfActionID = (int)WFLTypeOfAction.MessageTMS;
                            objTemplate.WFL_Event = obj;
                            model.WFL_EventTemplate.Add(objTemplate);
                        }
                        objTemplate.TemplateID = item.TMSTemplateID;
                    }

                    // Lưu template sms
                    if (item.SMSTemplateID > 0)
                    {
                        var objTemplate = model.WFL_EventTemplate.FirstOrDefault(c => c.EventID == obj.ID && c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                        if (objTemplate == null)
                        {
                            objTemplate = new WFL_EventTemplate();
                            objTemplate.CreatedBy = Account.UserName;
                            objTemplate.CreatedDate = DateTime.Now;
                            objTemplate.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                            objTemplate.WFL_Event = obj;
                            model.WFL_EventTemplate.Add(objTemplate);
                        }
                        objTemplate.TemplateID = item.SMSTemplateID;
                    }

                    // Disable tất cả user hiện tại
                    foreach (var act in model.WFL_Action.Where(c => c.EventID == item.ID && c.UserID.HasValue))
                        act.IsUse = false;

                    #region Gửi mail
                    if (item.lstUserMail != null && item.lstUserMail.Count > 0)
                    {
                        foreach (var user in item.lstUserMail)
                        {
                            if (item.lstCustomer != null && item.lstCustomer.Count > 0)
                            {
                                foreach (var cus in item.lstCustomer)
                                {
                                    var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == cus.CustomerID && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                                    if (objAction == null)
                                    {
                                        objAction = new WFL_Action();
                                        objAction.CreatedBy = Account.UserName;
                                        objAction.CreatedDate = DateTime.Now;
                                        objAction.WFL_Event = obj;
                                        model.WFL_Action.Add(objAction);
                                    }
                                    objAction.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                                    objAction.UserID = user.UserID;
                                    objAction.CustomerID = cus.CustomerID;
                                    objAction.IsUse = true;
                                }
                            }
                            else
                            {
                                var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == null && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                                if (objAction == null)
                                {
                                    objAction = new WFL_Action();
                                    objAction.CreatedBy = Account.UserName;
                                    objAction.CreatedDate = DateTime.Now;
                                    objAction.WFL_Event = obj;
                                    model.WFL_Action.Add(objAction);
                                }
                                objAction.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                                objAction.UserID = user.UserID;
                                objAction.IsUse = true;
                            }
                        }
                    }
                    #endregion

                    #region Gửi thông báo TMS
                    if (item.lstUserTMS != null && item.lstUserTMS.Count > 0)
                    {
                        foreach (var user in item.lstUserTMS)
                        {
                            if (item.lstCustomer != null && item.lstCustomer.Count > 0)
                            {
                                foreach (var cus in item.lstCustomer)
                                {
                                    var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == cus.CustomerID && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                                    if (objAction == null)
                                    {
                                        objAction = new WFL_Action();
                                        objAction.CreatedBy = Account.UserName;
                                        objAction.CreatedDate = DateTime.Now;
                                        objAction.WFL_Event = obj;
                                        model.WFL_Action.Add(objAction);
                                    }
                                    objAction.TypeOfActionID = (int)WFLTypeOfAction.MessageTMS;
                                    objAction.UserID = user.UserID;
                                    objAction.CustomerID = cus.CustomerID;
                                    objAction.IsUse = true;
                                }
                            }
                            else
                            {
                                var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == null && c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                                if (objAction == null)
                                {
                                    objAction = new WFL_Action();
                                    objAction.CreatedBy = Account.UserName;
                                    objAction.CreatedDate = DateTime.Now;
                                    objAction.WFL_Event = obj;
                                    model.WFL_Action.Add(objAction);
                                }
                                objAction.TypeOfActionID = (int)WFLTypeOfAction.MessageTMS;
                                objAction.UserID = user.UserID;
                                objAction.IsUse = true;
                            }
                        }
                    }
                    #endregion

                    #region Gửi SMS
                    if (item.lstUserSMS != null && item.lstUserSMS.Count > 0)
                    {
                        foreach (var user in item.lstUserSMS)
                        {
                            if (item.lstCustomer != null && item.lstCustomer.Count > 0)
                            {
                                foreach (var cus in item.lstCustomer)
                                {
                                    var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == cus.CustomerID && c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                                    if (objAction == null)
                                    {
                                        objAction = new WFL_Action();
                                        objAction.CreatedBy = Account.UserName;
                                        objAction.CreatedDate = DateTime.Now;
                                        objAction.WFL_Event = obj;
                                        model.WFL_Action.Add(objAction);
                                    }
                                    objAction.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                                    objAction.UserID = user.UserID;
                                    objAction.CustomerID = cus.CustomerID;
                                    objAction.IsUse = true;
                                }
                            }
                            else
                            {
                                var objAction = model.WFL_Action.FirstOrDefault(c => c.EventID == item.ID && c.UserID == user.UserID && c.CustomerID == null && c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                                if (objAction == null)
                                {
                                    objAction = new WFL_Action();
                                    objAction.CreatedBy = Account.UserName;
                                    objAction.CreatedDate = DateTime.Now;
                                    objAction.WFL_Event = obj;
                                    model.WFL_Action.Add(objAction);
                                }
                                objAction.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                                objAction.UserID = user.UserID;
                                objAction.IsUse = true;
                            }
                        }
                    }
                    #endregion

                    model.SaveChanges();

                    return obj.ID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLTemplate> WFLPacket_SettingTemplateRead()
        {
            try
            {
                List<DTOWFLTemplate> result = new List<DTOWFLTemplate>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_Template.Select(c => new DTOWFLTemplate
                    {
                        ID = c.ID,
                        Code = c.Code,
                        Name = c.Name,
                        Template = c.Template,
                        TemplateDetail = c.TemplateDetail,
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<CUSCustomer> WFLPacket_SettingCustomerRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<CUSCustomer>();
                    var objUser = model.SYS_User.Where(c => c.ID == Account.UserID).Select(c => new { c.ListCustomerID }).FirstOrDefault();
                    if (objUser != null && objUser.ListCustomerID != null && objUser.ListCustomerID.Trim() != string.Empty)
                    {
                        var lstid = objUser.ListCustomerID.Split(',').Select(c => Convert.ToInt32(c)).ToList();
                        result = model.CUS_Customer.Where(c => !c.IsSystem && lstid.Contains(c.ID) && (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH)).Select(c => new CUSCustomer
                        {
                            ID = c.ID,
                            Code = c.Code,
                            CustomerName = c.CustomerName
                        }).ToList();
                    }
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<SYSUser> WFLPacket_SettingUserRead()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<SYSUser>();
                    result = model.SYS_User.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new SYSUser
                    {
                        ID = c.ID,
                        Code = c.Code,
                        UserName = c.UserName,
                        Email = c.Email,
                        TelNo = c.TelNo,
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        public int WFLPacketSetting_Save(DTOWFLPacket_Setting item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    var obj = model.WFL_PacketSetting.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new WFL_PacketSetting();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        model.WFL_PacketSetting.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.SettingName = item.SettingName;
                    obj.PacketSettingTypeID = item.PacketSettingTypeID;
                    obj.IsApproved = item.IsApproved;
                    obj.CustomerID = item.CustomerID > 0 ? item.CustomerID : null;
                    obj.IsAutoCollect = item.IsAutoCollect;
                    obj.IsAutoSend = item.IsAutoSend;
                    obj.IsAutoSendEmpty = item.IsAutoSendEmpty;
                    obj.TimeSend1 = item.TimeSend1;
                    obj.TimeSend2 = item.TimeSend2;
                    obj.IsDriverSMS = item.IsDriverSMS;
                    obj.DriverSMSTemplateID = item.DriverSMSTemplateID;
                    obj.IsDriverMail = item.IsDriverMail;
                    obj.DriverMailTemplateID = item.DriverMailTemplateID;
                    obj.CUSSettingID = item.CUSSettingID > 0 ? item.CUSSettingID : null;
                    // Lưu template mail
                    if (item.MailTemplateID > 0)
                    {
                        var objTemplate = model.WFL_PacketSettingTemplate.FirstOrDefault(c => c.PacketSettingID == obj.ID && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                        if (objTemplate == null)
                        {
                            objTemplate = new WFL_PacketSettingTemplate();
                            objTemplate.CreatedBy = Account.UserName;
                            objTemplate.CreatedDate = DateTime.Now;
                            objTemplate.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                            objTemplate.WFL_PacketSetting = obj;
                            model.WFL_PacketSettingTemplate.Add(objTemplate);
                        }
                        objTemplate.TemplateID = item.MailTemplateID;
                    }

                    // Lưu template sms
                    if (item.SMSTemplateID > 0)
                    {
                        var objTemplate = model.WFL_PacketSettingTemplate.FirstOrDefault(c => c.PacketSettingID == obj.ID && c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                        if (objTemplate == null)
                        {
                            objTemplate = new WFL_PacketSettingTemplate();
                            objTemplate.CreatedBy = Account.UserName;
                            objTemplate.CreatedDate = DateTime.Now;
                            objTemplate.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                            objTemplate.WFL_PacketSetting = obj;
                            model.WFL_PacketSettingTemplate.Add(objTemplate);
                        }
                        objTemplate.TemplateID = item.SMSTemplateID;
                    }

                    // Disable tất cả user hiện tại
                    foreach (var act in model.WFL_PacketSettingAction.Where(c => c.PacketSettingID == item.ID))
                        act.IsUse = false;

                    #region Gửi mail
                    if (item.lstUserMail != null && item.lstUserMail.Count > 0)
                    {
                        foreach (var user in item.lstUserMail)
                        {
                            var objAction = model.WFL_PacketSettingAction.FirstOrDefault(c => c.PacketSettingID == item.ID && c.UserID == user.UserID && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                            if (objAction == null)
                            {
                                objAction = new WFL_PacketSettingAction();
                                objAction.CreatedBy = Account.UserName;
                                objAction.CreatedDate = DateTime.Now;
                                objAction.WFL_PacketSetting = obj;
                                model.WFL_PacketSettingAction.Add(objAction);
                            }
                            objAction.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                            objAction.UserID = user.UserID;
                            objAction.IsUse = true;
                        }
                    }
                    #endregion

                    #region Gửi SMS
                    if (item.lstUserSMS != null && item.lstUserSMS.Count > 0)
                    {
                        foreach (var user in item.lstUserSMS)
                        {
                            var objAction = model.WFL_PacketSettingAction.FirstOrDefault(c => c.PacketSettingID == item.ID && c.UserID == user.UserID && c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                            if (objAction == null)
                            {
                                objAction = new WFL_PacketSettingAction();
                                objAction.CreatedBy = Account.UserName;
                                objAction.CreatedDate = DateTime.Now;
                                objAction.WFL_PacketSetting = obj;
                                model.WFL_PacketSettingAction.Add(objAction);
                            }
                            objAction.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                            objAction.UserID = user.UserID;
                            objAction.IsUse = true;
                        }
                    }
                    #endregion

                    model.SaveChanges();

                    return obj.ID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLPacket_SettingDelete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lstid.Count > 0)
                    {
                        foreach (var item in lstid)
                        {
                            var obj = model.WFL_PacketSetting.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                if (model.WFL_Packet.Count(c => c.PacketSettingID == item) > 0)
                                    throw FaultHelper.BusinessFault(null, null, "Packet đã phát sinh, không thể xóa");

                                foreach (var detail in model.WFL_PacketSettingAction.Where(c => c.PacketSettingID == item))
                                    model.WFL_PacketSettingAction.Remove(detail);
                                foreach (var detail in model.WFL_PacketSettingTemplate.Where(c => c.PacketSettingID == item))
                                    model.WFL_PacketSettingTemplate.Remove(detail);
                                model.WFL_PacketSetting.Remove(obj);
                            }
                        }
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<CUSSettingsReport> CUSSettingsPlan_AllList(int cusId)
        {
            try
            {
                string strKey = CUSSettingKey.Plan.ToString();
                var result = new List<CUSSettingsReport>();
                using (var model = new DataEntities())
                {
                    foreach (var itemSetting in model.CUS_Setting.Where(c => c.Key == strKey && c.SYSCustomerID == Account.SYSCustomerID).Select(c => new { c.ID, c.Key, c.ReferID, c.Name, c.Setting }))
                    {
                        if (!string.IsNullOrEmpty(itemSetting.Setting))
                        {
                            var item = Newtonsoft.Json.JsonConvert.DeserializeObject<CUSSettingsReport>(itemSetting.Setting);
                            item.ID = itemSetting.ID;
                            item.ReferID = itemSetting.ReferID;
                            item.Name = itemSetting.Name;
                            item.Key = itemSetting.Key;
                            if (cusId > 0 && item.ListCustomer != null)
                            {
                                foreach (var cus in item.ListCustomer)
                                {
                                    if (cus.CustomerID == cusId)
                                    {
                                        result.Add(item);
                                    }
                                }
                            }
                            else
                                result.Add(item);
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion
        #endregion

        #region WFEvent
        public DTOResult WFLSetting_WFEvent_Read(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_WFEvent.Select(c => new DTOWFEvent
                    {
                        ID = c.ID,
                        Code = c.Code,
                        EventName = c.EventName,
                        Expression = c.Expression,
                        IsApproved = c.IsApproved,
                        IsChoose = false,
                        UseCustomer = c.UseCustomer,
                        SortOrder = c.SortOrder,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOWFEvent>;
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFEvent WFLSetting_WFEvent_Get(int id)
        {
            try
            {
                var result = new DTOWFEvent { ID = -1, Code = "", EventName = "", Expression = "", IsApproved = true, };
                result.lstField = new List<DTOWFLEvent_Field>();

                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.WFL_WFEvent.Where(c => c.ID == id).Select(c => new DTOWFEvent
                        {
                            ID = c.ID,
                            Code = c.Code,
                            EventName = c.EventName,
                            Expression = c.Expression,
                            IsApproved = c.IsApproved,
                            IsChoose = false,
                            UseCustomer = c.UseCustomer,
                            SortOrder = c.SortOrder,
                            IsExpr = false,
                        }).FirstOrDefault();

                        if (result.ID > 0)
                        {
                            var lstField = model.WFL_WFEventField.Where(c => c.WFEventID == result.ID).Select(c => new DTOWFLEvent_Field
                            {
                                ID = c.ID,
                                CompareValue = c.CompareValue,
                                FieldID = c.FieldID,
                                FieldCode = c.WFL_Field.ColumnName,
                                FieldName = c.WFL_Field.ColumnName,
                                OperatorCode = c.OperatorCode,
                                OperatorValue = c.OperatorValue,
                                TableCode = c.WFL_Field.TableName,
                                Type = c.WFL_Field.ColumnType,
                                IsModified = c.IsModified,
                                IsChoose = false
                            }).ToList();
                            result.lstField = lstField;

                            var lstTemplate = model.WFL_WFEventTemplate.Where(c => c.WFEventID == result.ID).Select(c => new DTOWFL_WFEventTemplate
                            {
                                ID = c.ID,
                                TemplateID = c.TemplateID,
                                TypeOfActionID = c.TypeOfActionID,
                                WFEventID = c.WFEventID
                            }).ToList();

                            if (lstTemplate.Count > 0)
                            {
                                var SendMail = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.SendMail);
                                if (SendMail != null)
                                    result.SendMail_TemplateID = SendMail.TemplateID;
                                var SMS = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.SMS);
                                if (SMS != null)
                                    result.SMS_TemplateID = SMS.TemplateID;
                                var MessageTMS = lstTemplate.FirstOrDefault(c => c.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS);
                                if (MessageTMS != null)
                                    result.MessageTMS_TemplateID = MessageTMS.TemplateID;
                            }
                        }
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public int WFLSetting_WFEvent_Save(DTOWFEvent item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    if (model.WFL_WFEvent.Count(c => c.ID != item.ID && c.Code == item.Code) > 0)
                        throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");

                    var obj = model.WFL_WFEvent.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new WFL_WFEvent();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.WFL_WFEvent.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.EventName = item.EventName;
                    obj.IsApproved = item.IsApproved;
                    obj.Expression = item.Expression;
                    obj.UseCustomer = item.UseCustomer;
                    obj.SortOrder = item.SortOrder;

                    // Xóa data cũ
                    foreach (var detail in model.WFL_WFEventField.Where(c => c.WFEventID == item.ID))
                        model.WFL_WFEventField.Remove(detail);
                    foreach (var detail in model.WFL_WFEventTemplate.Where(c => c.WFEventID == item.ID))
                        model.WFL_WFEventTemplate.Remove(detail);

                    // Lưu danh sách field
                    if (item.lstField != null)
                    {
                        foreach (var field in item.lstField)
                        {
                            var objField = new WFL_WFEventField();
                            objField.CreatedBy = Account.UserName;
                            objField.CreatedDate = DateTime.Now;
                            objField.FieldID = field.FieldID;
                            objField.OperatorCode = field.OperatorCode == null ? string.Empty : field.OperatorCode;
                            objField.OperatorValue = field.OperatorValue;
                            objField.CompareValue = field.CompareValue;
                            objField.IsModified = field.IsModified;
                            if (string.IsNullOrEmpty(field.CompareValue))
                                field.CompareValue = objField.CompareValue = "null";

                            obj.WFL_WFEventField.Add(objField);
                        }
                        if (item.IsExpr)
                            obj.Expression = WFLSetting_GenerateExpression(item.lstField);
                    }

                    // Lưu template mail
                    if (item.SendMail_TemplateID > 0)
                    {
                        var objTemplate = new WFL_WFEventTemplate();
                        objTemplate.CreatedBy = Account.UserName;
                        objTemplate.CreatedDate = DateTime.Now;
                        objTemplate.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                        objTemplate.WFL_WFEvent = obj;
                        objTemplate.TemplateID = item.SendMail_TemplateID;
                        model.WFL_WFEventTemplate.Add(objTemplate);
                    }

                    // Lưu template tms
                    if (item.MessageTMS_TemplateID > 0)
                    {
                        var objTemplate = new WFL_WFEventTemplate();
                        objTemplate.CreatedBy = Account.UserName;
                        objTemplate.CreatedDate = DateTime.Now;
                        objTemplate.TypeOfActionID = (int)WFLTypeOfAction.MessageTMS;
                        objTemplate.WFL_WFEvent = obj;
                        objTemplate.TemplateID = item.MessageTMS_TemplateID;
                        model.WFL_WFEventTemplate.Add(objTemplate);
                    }

                    // Lưu template sms
                    if (item.SMS_TemplateID > 0)
                    {
                        var objTemplate = new WFL_WFEventTemplate();
                        objTemplate.CreatedBy = Account.UserName;
                        objTemplate.CreatedDate = DateTime.Now;
                        objTemplate.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                        objTemplate.WFL_WFEvent = obj;
                        objTemplate.TemplateID = item.SMS_TemplateID;
                        model.WFL_WFEventTemplate.Add(objTemplate);
                    }

                    model.SaveChanges();

                    return obj.ID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void WFLSetting_WFEvent_Delete(List<int> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lstid.Count > 0)
                    {
                        foreach (var item in lstid)
                        {
                            var obj = model.WFL_WFEvent.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                if (model.WFL_DefineWFEvent.Count(c => c.WFEventID == item) > 0)
                                    throw FaultHelper.BusinessFault(null, null, "Event đã được thiết lập, không thể xóa");
                                else
                                {
                                    foreach (var detail in model.WFL_WorkFlowWFEvent.Where(c => c.WFEventID == item))
                                        model.WFL_WorkFlowWFEvent.Remove(detail);
                                    foreach (var detail in model.WFL_WFEventField.Where(c => c.WFEventID == item))
                                        model.WFL_WFEventField.Remove(detail);
                                    foreach (var detail in model.WFL_WFEventTemplate.Where(c => c.WFEventID == item))
                                        model.WFL_WFEventTemplate.Remove(detail);
                                    model.WFL_WFEvent.Remove(obj);
                                }
                            }
                        }
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLField> WFLSetting_WFEvent_TableRead()
        {
            try
            {
                List<DTOWFLField> result = new List<DTOWFLField>();
                using (var model = new DataEntities())
                {
                    var query = model.WFL_Field.Where(c => c.IsApproved).Select(c => new
                    {
                        TableName = c.TableName,
                        TableDescription = c.TableDescription,
                    }).Distinct().ToList();
                    if (query.Count > 0)
                    {
                        result = query.Select(c => new DTOWFLField
                        {
                            TableName = c.TableName,
                            TableDescription = c.TableDescription
                        }).ToList();
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLField> WFLSetting_WFEvent_FieldRead()
        {
            try
            {
                List<DTOWFLField> result = new List<DTOWFLField>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_Field.Where(c => c.IsApproved).Select(c => new DTOWFLField
                    {
                        ID = c.ID,
                        TableName = c.TableName,
                        TableDescription = c.TableDescription,
                        ColumnType = c.ColumnType,
                        ColumnName = c.ColumnName,
                        ColumnDescription = c.ColumnDescription,
                        IsApproved = c.IsApproved
                    }).Distinct().ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOWFLEvent_SysVar WFLSetting_WFEvent_SysVar()
        {
            try
            {
                DTOWFLEvent_SysVar reslut = new DTOWFLEvent_SysVar();
                using (var model = new DataEntities())
                {
                    reslut.ListDITOGroupProductStatus = new List<SYSVar>();
                    reslut.ListDITOGroupProductStatusPOD = new List<SYSVar>();
                    reslut.ListKPIReason = new List<SYSVar>();
                    reslut.ListStatusOfAssetTimeSheet = new List<SYSVar>();
                    reslut.ListStatusOfDITOMaster = new List<SYSVar>();
                    reslut.ListStatusOfCOTOMaster = new List<SYSVar>();
                    reslut.ListStatusOfOrder = new List<SYSVar>();
                    reslut.ListStatusOfPlan = new List<SYSVar>();
                    reslut.ListTypeOfAssetTimeSheet = new List<SYSVar>();
                    reslut.ListTypeOfPaymentDITOMaster = new List<SYSVar>();
                    reslut.ListTroubleCostStatus = new List<SYSVar>();
                    reslut.ListDITOLocationStatus = new List<SYSVar>();
                    reslut.ListCOTOLocationStatus = new List<SYSVar>();
                    reslut.ListDITOGroupProductStatus = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.DITOGroupProductStatus).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListDITOGroupProductStatusPOD = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.DITOGroupProductStatusPOD).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListKPIReason = model.KPI_Reason.Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ReasonName
                    }).ToList();

                    reslut.ListStatusOfAssetTimeSheet = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfAssetTimeSheet).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListStatusOfDITOMaster = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfDITOMaster).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListStatusOfCOTOMaster = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfCOTOMaster).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListStatusOfOrder = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfOrder).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListStatusOfPlan = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.StatusOfPlan).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListTypeOfAssetTimeSheet = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TypeOfAssetTimeSheet).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListTypeOfPaymentDITOMaster = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TypeOfPaymentDITOMaster).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListTroubleCostStatus = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TroubleCostStatus).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListDITOLocationStatus = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.DITOLocationStatus).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();

                    reslut.ListCOTOLocationStatus = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.COTOLocationStatus).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ValueOfVar = c.ValueOfVar
                    }).ToList();
                }

                return reslut;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOWFLTemplate> WFLSetting_WFEvent_TemplateRead()
        {
            try
            {
                List<DTOWFLTemplate> result = new List<DTOWFLTemplate>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_Template.Select(c => new DTOWFLTemplate
                    {
                        ID = c.ID,
                        Code = c.Code,
                        Name = c.Name,
                        Template = c.Template,
                        TemplateDetail = c.TemplateDetail,
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion
    }
}

