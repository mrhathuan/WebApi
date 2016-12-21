using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using DTO;
using CacheManager.Core;
using System.Web;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Presentation;
using Microsoft.SqlServer;
using System.IO;
using OfficeOpenXml;
using IServices;
using OfficeOpenXml.Style;
using System.Drawing;

namespace ClientWeb
{
    public class WFLController : BaseController
    {
        #region WFLSetting

        [HttpPost]
        public DTOResult WFLSetting_EventRead(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventRead(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLEvent WFLSetting_EventGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOWFLEvent);
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventGet(id);
                });
                
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int WFLSetting_EventSave(dynamic dynParam)
        {
            try
            {
                DTOWFLEvent item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFLEvent>(dynParam.item.ToString());
                var result = -1;
                
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventSave(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLSetting_EventDelete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_EventDelete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOWFLField> WFLSetting_EventTableRead()
        {
            try
            {
                List<DTOWFLField> result = new List<DTOWFLField>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventTableRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOWFLField> WFLSetting_EventFieldRead()
        {
            try
            {
                List<DTOWFLField> result = new List<DTOWFLField>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventFieldRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOWFLTemplate> WFLSetting_EventTemplateRead()
        {
            try
            {
                List<DTOWFLTemplate> result = new List<DTOWFLTemplate>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventTemplateRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSCustomer> WFLSetting_EventCustomerRead()
        {
            try
            {
                List<CUSCustomer> result = new List<CUSCustomer>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventCustomerRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSUser> WFLSetting_EventUserRead()
        {
            try
            {
                List<SYSUser> result = new List<SYSUser>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventUserRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOWFLTypeOfAction> WFLSetting_EventTypeOfActionRead()
        {
            try
            {
                List<DTOWFLTypeOfAction> result = new List<DTOWFLTypeOfAction>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventTypeOfActionRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSVar> WFLSetting_EventStatusOfOrderRead()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventStatusOfOrderRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSVar> WFLSetting_EventStatusOfPlanRead()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventStatusOfPlanRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSVar> WFLSetting_EventStatusOfDITOMasterRead()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventStatusOfDITOMasterRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSVar> WFLSetting_EventKPIReasonRead()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventKPIReasonRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSVar> WFLSetting_EventStatusOfAssetTimeSheetRead()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventStatusOfAssetTimeSheetRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSVar> WFLSetting_EventTypeOfAssetTimeSheetRead()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_EventTypeOfAssetTimeSheetRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLEvent_SysVar WFLSettingEvent_SysVar()
        {
            try
            {
                DTOWFLEvent_SysVar result = new DTOWFLEvent_SysVar();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSettingEvent_SysVar();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult WFLSetting_TemplateRead(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_TemplateRead(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLTemplate WFLSetting_TemplateGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOWFLTemplate);
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_TemplateGet(id);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int WFLSetting_TemplateSave(dynamic dynParam)
        {
            try
            {
                DTOWFLTemplate item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFLTemplate>(dynParam.item.ToString());
                var result = -1;

                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_TemplateSave(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLSetting_TemplateDelete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_TemplateDelete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult WFLSetting_FieldRead(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_FieldRead(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLField WFLSetting_FieldGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOWFLField);
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_FieldGet(id);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int WFLSetting_FieldSave(dynamic dynParam)
        {
            try
            {
                DTOWFLField item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFLField>(dynParam.item.ToString());
                var result = -1;

                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_FieldSave(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLSetting_FieldDelete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_FieldDelete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Define
        [HttpPost]
        public DTOResult WFLSetting_DefineRead(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_DefineRead(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLDefine WFLSetting_DefineGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOWFLDefine);
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_DefineGet(id);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int WFLSetting_DefineSave(dynamic dynParam)
        {
            try
            {
                DTOWFLDefine item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFLDefine>(dynParam.item.ToString());
                var result = -1;

                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_DefineSave(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLSetting_DefineDelete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_DefineDelete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOWFLStep> WFLSetting_StepParentList(dynamic dynParam)
        {
            try
            {
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                int defineID = (int)dynParam.defineID;
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_StepParentList(defineID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOWFLStep> WFLSetting_StepChildList(dynamic dynParam)
        {
            try
            {
                int stepID = (int)dynParam.stepID;
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_StepChildList(stepID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLDefineData WFLSetting_DefineData(dynamic dynParam)
        {
            try
            {
                int stepID = (int)dynParam.stepID;
                int defineID = (int)dynParam.defineID;
                DTOWFLDefineData result = new DTOWFLDefineData();
                if (stepID != null && defineID != null)
                {
                    ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                    {
                        result = sv.WFLSetting_DefineData(defineID, stepID);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLSetting_DefineDetailSave(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                int stepID = (int)dynParam.stepID;
                List<DTOWFLWorkFlow> lst = Newtonsoft.Json.JsonConvert.DeserializeObject <List<DTOWFLWorkFlow>>(dynParam.lst.ToString());
                DTOWFLDefineData data = new DTOWFLDefineData();
                data.DefineID = defineID;
                data.StepParentID = stepID;
                data.ListWorkFlow = lst;

                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_DefineDetailSave(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLSetting_DefineWFLSave(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                DTOWFLWorkFlow item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFLWorkFlow>(dynParam.item.ToString());

                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_DefineWFLSave(defineID,item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult WFLSetting_StepDefineList(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                string request = dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_StepDefineList(defineID,request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult WFLSetting_StepDefineNotInList(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                DTOResult result = new DTOResult();
                string request = dynParam.request.ToString();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_StepDefineNotInList(defineID,request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLSetting_StepDefineNotInSave(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_StepDefineNotInSave(defineID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLSetting_StepDefineDelete(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_StepDefineDelete(defineID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult WFLSetting_DefineGroupList(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                string request = dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_DefineGroupList(defineID,request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult WFLSetting_DefineGroupNotInList(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                string request = dynParam.request.ToString();
                DTOResult result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_DefineGroupNotInList(defineID,request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLSetting_DefineGroupNotInSave(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_DefineGroupNotInSave(defineID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLSetting_DefineGroupDelete(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_DefineGroupDelete(defineID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Event
        [HttpPost]
        public DTOResult WFLSetting_DefineEventList(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                int workflowID = (int)dynParam.workflowID;
                string request = dynParam.request.ToString();
                DTOResult result = new DTOResult();
                if (workflowID > 0)
                {
                    ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                    {
                        result = sv.WFLSetting_DefineEventList(defineID, workflowID, request);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult WFLSetting_DefineEventNotInList(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                int workflowID = (int)dynParam.workflowID;
                string request = dynParam.request.ToString();
                DTOResult result = new DTOResult();
                if (workflowID > 0)
                {
                    ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                    {
                        result = sv.WFLSetting_DefineEventNotInList(defineID, workflowID, request);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLSetting_DefineEventNotInSave(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                int workflowID = (int)dynParam.workflowID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_DefineEventNotInSave(defineID, workflowID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLSetting_DefineEventDelete(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_DefineEventDelete(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLDefineEvent WFLSetting_DefineEventGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOWFLDefineEvent result = new DTOWFLDefineEvent();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_DefineEventGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLSetting_DefineEventSave(dynamic dynParam)
        {
            try
            {
                DTOWFLDefineEvent item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFLDefineEvent>(dynParam.item.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_DefineEventSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSUser> WFLSetting_DefineEventUserRead(dynamic dynParam)
        {
            try
            {
                int defineID = (int)dynParam.defineID;
                List<SYSUser> result = new List<SYSUser>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_DefineEventUserRead(defineID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region Function
        [HttpPost]
        public DTOResult WFLSetting_FunctionList(dynamic dynParam)
        {
            try
            {
                string request = "";
                int workflowID = (int)dynParam.workflowID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_FunctionList(request, workflowID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult WFLSetting_FunctionNotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int workflowID = (int)dynParam.workflowID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_FunctionNotInList(request, workflowID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLSetting_FunctionNotInSave(dynamic dynParam)
        {
            try
            {
                int workflowID = (int)dynParam.workflowID;
                List<SYSFunction> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SYSFunction>>(dynParam.lst.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_FunctionNotInSave(workflowID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLSetting_FunctionDelete(dynamic dynParam)
        {
            try
            {
                int workflowID = (int)dynParam.workflowID;
                List<SYSFunction> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<SYSFunction>>(dynParam.lst.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_FunctionDelete(workflowID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOSYSAction> WFLSetting_ActionList(dynamic dynParam)
        {
            try
            {
                List<DTOSYSAction> result = new List<DTOSYSAction>();
                int functionID = (int)dynParam.functionID;
                int workflowID = (int)dynParam.workflowID;
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_ActionList(workflowID, functionID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOWFLDefineData WFLSetting_FunctionData(dynamic dynParam)
        {
            try
            {
                int stepID = (int)dynParam.stepID;
                DTOWFLDefineData result = new DTOWFLDefineData();
                if (stepID != null)
                {
                    ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                    {
                        result = sv.WFLSetting_FunctionData(stepID);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOWFLStep> WFLSetting_StepParentAllList(dynamic dynParam)
        {
            try
            {
                List<DTOWFLStep> result = new List<DTOWFLStep>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_StepParentAllList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLSetting_ActionSave(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int functionID = (int)dynParam.functionID;
                int workflowID = (int)dynParam.workflowID;
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_ActionSave(workflowID,functionID,lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region Event
        [HttpPost]
        public DTOResult WFLSetting_WFEventList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int workflowID = (int)dynParam.workflowID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_WFEvent_List(request, workflowID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult WFLSetting_WFEvent_NotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int workflowID = (int)dynParam.workflowID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_WFEvent_NotInList(request, workflowID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult WFLSetting_WFEvent_NotInSaveList(dynamic dynParam)
        {
            try
            {
                int workflowID = (int)dynParam.workflowID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                DTOResult result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_WFEvent_NotInSaveList(workflowID, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult WFLSetting_WFEvent_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                DTOResult result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_WFEvent_DeleteList(lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public DTOWFL_WFEvent WFLSetting_WFEventGet(dynamic dynParam)
        //{
        //    try
        //    {
        //        int id = (int)dynParam.id;
        //        DTOWFL_WFEvent result = new DTOWFL_WFEvent();
        //        ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
        //        {
        //            result = sv.WFLSetting_WFEventGet(id);
        //        });
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public int WFLSetting_WFEventSave(dynamic dynParam)
        //{
        //    try
        //    {
        //        DTOWFL_WFEvent item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFL_WFEvent>(dynParam.item.ToString());
        //        int workflowID = (int)dynParam.workflowID;
        //        int result = -1;
        //        ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
        //        {
        //            result = sv.WFLSetting_WFEventSave(item, workflowID);
        //        });
        //        return result;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public void WFLSetting_WFEventDelete(dynamic dynParam)
        //{
        //    try
        //    {
        //        List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
        //        ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
        //        {
        //            sv.WFLSetting_WFEventDelete(lst);
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
        #endregion

        [HttpPost]
        public DTOResult WFLSetting_ChildrenFlow(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int workflowID = (int)dynParam.workflowID;
                DTOResult result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_ChildrenFlow(request, workflowID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int WFLSetting_ChildrenFlow_Save(dynamic dynParam)
        {
            try
            {
                int workflowID = (int)dynParam.workflowID;
                DTOWFLWorkFlow item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFLWorkFlow>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_ChildrenFlow_Save(item, workflowID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLWorkFlow WFLSetting_ChildrenFlow_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOWFLWorkFlow result = new DTOWFLWorkFlow();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_ChildrenFlow_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLSetting_ChildrenFlow_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                DTOWFLWorkFlow result = new DTOWFLWorkFlow();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_ChildrenFlow_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Event
        [HttpPost]
        public DTOResult WFLSetting_WFEvent_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_WFEvent_Read(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFEvent WFLSetting_WFEvent_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = default(DTOWFEvent);
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_WFEvent_Get(id);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int WFLSetting_WFEvent_Save(dynamic dynParam)
        {
            try
            {
                DTOWFEvent item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFEvent>(dynParam.item.ToString());
                var result = -1;

                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_WFEvent_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLSetting_WFEvent_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lstid.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLSetting_WFEvent_Delete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOWFLField> WFLSetting_WFEvent_TableRead()
        {
            try
            {
                List<DTOWFLField> result = new List<DTOWFLField>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_WFEvent_TableRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOWFLField> WFLSetting_WFEvent_FieldRead()
        {
            try
            {
                List<DTOWFLField> result = new List<DTOWFLField>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_WFEvent_FieldRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLEvent_SysVar WFLSetting_WFEvent_SysVar()
        {
            try
            {
                DTOWFLEvent_SysVar result = new DTOWFLEvent_SysVar();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_WFEvent_SysVar();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOWFLTemplate> WFLSetting_WFEvent_TemplateRead()
        {
            try
            {
                List<DTOWFLTemplate> result = new List<DTOWFLTemplate>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLSetting_WFEvent_TemplateRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
        #endregion

        #region WFLParket
        [HttpPost]
        public DTOResult WFLPacket_Read(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacket_Read(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLPacket WFLPacket_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new DTOWFLPacket();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacket_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public long WFLPacket_Save(dynamic dynParam)
        {
            try
            {
                long res = 0;
                DTOWFLPacket item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFLPacket>(dynParam.item.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    res = sv.WFLPacket_Save(item);
                });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLPacket_Delete(dynamic dynParam)
        {
            try
            {
                long id = (int)dynParam.id;
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLPacket_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLPacket_Send(dynamic dynParam)
        {
            try
            {
                List<long> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<long>>(dynParam.lstid.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLPacket_Send(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult WFLPacket_ORDGroupProductList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                long packetID = (long)dynParam.packetID;
                var res = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    res = sv.WFLPacket_ORDGroupProductList(packetID,request);
                });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult WFLPacket_ORDGroupProductNotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                long packetID = (long)dynParam.packetID;
                var res = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    res = sv.WFLPacket_ORDGroupProductNotInList(packetID, request);
                });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLPacket_ORDGroupProductSaveList(dynamic dynParam)
        {
            try
            {
                long packetID = (long)dynParam.packetID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLPacket_ORDGroupProductSaveList(packetID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLPacket_DetailDelete(dynamic dynParam)
        {
            try
            {
                long packetID = (long)dynParam.packetID;
                List<long> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<long>>(dynParam.lst.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLPacket_DetailDelete(packetID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //xe tai
        [HttpPost]
        public DTOResult WFLPacket_DITOMasterList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                long packetID = (long)dynParam.packetID;
                var res = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    res = sv.WFLPacket_DITOMasterList(packetID, request);
                });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult WFLPacket_DITOMasterNotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                long packetID = (long)dynParam.packetID;
                var res = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    res = sv.WFLPacket_DITOMasterNotInList(packetID, request);
                });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLPacket_DITOMasterSaveList(dynamic dynParam)
        {
            try
            {
                long packetID = (long)dynParam.packetID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLPacket_DITOMasterSaveList(packetID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //xe Container
        [HttpPost]
        public DTOResult WFLPacket_COTOMasterList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                long packetID = (long)dynParam.packetID;
                var res = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    res = sv.WFLPacket_COTOMasterList(packetID, request);
                });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult WFLPacket_COTOMasterNotInList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                long packetID = (long)dynParam.packetID;
                var res = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    res = sv.WFLPacket_COTOMasterNotInList(packetID, request);
                });
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void WFLPacket_COTOMasterSaveList(dynamic dynParam)
        {
            try
            {
                long packetID = (long)dynParam.packetID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLPacket_COTOMasterSaveList(packetID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region WFLParket_Setting
        [HttpPost]
        public List<DTOWFLPacket_Setting> WFLPacket_SettingAllList()
        {
            try
            {
                var result = new List<DTOWFLPacket_Setting>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacket_SettingAllList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult WFLPacket_SettingRead(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacket_SettingRead(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLPacket_Setting WFLPacket_SettingGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new DTOWFLPacket_Setting();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacket_SettingGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOWFLEvent WFLPacketSetting_EventGet(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                var result = new DTOWFLEvent();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacketSetting_EventGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int WFLPacketSetting_EventSave(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.packetSettingID;
                DTOWFLEvent item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFLEvent>(dynParam.item.ToString());
                int result = 0;
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacketSetting_EventSave(item, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSVar> WFLPacket_SettingType(dynamic dynParam)
        {
            try
            {
                var result = new List<SYSVar>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacket_SettingType();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLPacketSetting_Save(dynamic dynParam)
        {
            try
            {
                DTOWFLPacket_Setting item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOWFLPacket_Setting>(dynParam.item.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLPacketSetting_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void WFLPacket_SettingDelete(dynamic dynParam)
        {
            try
            {
                List<int> lstid = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    sv.WFLPacket_SettingDelete(lstid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOWFLTemplate> WFLPacket_SettingTemplateRead(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOWFLTemplate>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacket_SettingTemplateRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<SYSUser> WFLPacket_SettingUserRead(dynamic dynParam)
        {
            try
            {
                var result = new List<SYSUser>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacket_SettingUserRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSCustomer> WFLPacket_SettingCustomerRead(dynamic dynParam)
        {
            try
            {
                var result = new List<CUSCustomer>();
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.WFLPacket_SettingCustomerRead();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSSettingsReport> CUSSettingsPlan_AllList(dynamic dynParam)
        {
            try
            {
                var result = new List<CUSSettingsReport>();
                int cusId = (int)dynParam.cusId;
                ServiceFactory.SVWorkflow((ISVWorkflow sv) =>
                {
                    result = sv.CUSSettingsPlan_AllList(cusId);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}