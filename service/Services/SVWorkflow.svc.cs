using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Kendo.Mvc.Extensions;
using DTO;
using IServices;
using Business;

namespace Services
{
    public partial class SVWorkflow : SVBase, ISVWorkflow
    {
        public SVWorkflow()
        {

        }
        public string Connect()
        {
            try
            {
                return "";
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

        #region Event
        public DTOResult WFLSetting_EventRead(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventRead(request);
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

        public DTOWFLEvent WFLSetting_EventGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventGet(id);
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

        public int WFLSetting_EventSave(DTOWFLEvent item)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventSave(item);
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

        public void WFLSetting_EventDelete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_EventDelete(lstid);
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

        public List<DTOWFLField> WFLSetting_EventTableRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventTableRead();
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

        public List<DTOWFLField> WFLSetting_EventFieldRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventFieldRead();
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

        public List<DTOWFLTemplate> WFLSetting_EventTemplateRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventTemplateRead();
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

        public List<CUSCustomer> WFLSetting_EventCustomerRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventCustomerRead();
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

        public List<SYSUser> WFLSetting_EventUserRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventUserRead();
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

        public List<DTOWFLTypeOfAction> WFLSetting_EventTypeOfActionRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventTypeOfActionRead();
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

        public List<SYSVar> WFLSetting_EventStatusOfOrderRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventStatusOfOrderRead();
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

        public List<SYSVar> WFLSetting_EventStatusOfPlanRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventStatusOfPlanRead();
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

        public List<SYSVar> WFLSetting_EventStatusOfDITOMasterRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventStatusOfDITOMasterRead();
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

        public List<SYSVar> WFLSetting_EventKPIReasonRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventKPIReasonRead();
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

        public List<SYSVar> WFLSetting_EventStatusOfAssetTimeSheetRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventStatusOfAssetTimeSheetRead();
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

        public List<SYSVar> WFLSetting_EventTypeOfAssetTimeSheetRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_EventTypeOfAssetTimeSheetRead();
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

        public DTOWFLEvent_SysVar WFLSettingEvent_SysVar()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSettingEvent_SysVar();
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

        #region Template
        public DTOResult WFLSetting_TemplateRead(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_TemplateRead(request);
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

        public DTOWFLTemplate WFLSetting_TemplateGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_TemplateGet(id);
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

        public int WFLSetting_TemplateSave(DTOWFLTemplate item)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_TemplateSave(item);
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

        public void WFLSetting_TemplateDelete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_TemplateDelete(lstid);
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

        #region Field
        public DTOResult WFLSetting_FieldRead(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_FieldRead(request);
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

        public DTOWFLField WFLSetting_FieldGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_FieldGet(id);
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

        public int WFLSetting_FieldSave(DTOWFLField item)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_FieldSave(item);
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

        public void WFLSetting_FieldDelete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_FieldDelete(lstid);
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

        #region Define
        public DTOResult WFLSetting_DefineRead(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_DefineRead(request);
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

        public DTOWFLDefine WFLSetting_DefineGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_DefineGet(id);
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

        public int WFLSetting_DefineSave(DTOWFLDefine item)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_DefineSave(item);
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

        public void WFLSetting_DefineDelete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_DefineDelete(id);
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

        public List<DTOWFLStep> WFLSetting_StepParentList(int defineID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_StepParentList(defineID);
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

        public List<DTOWFLStep> WFLSetting_StepChildList(int stepID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_StepChildList(stepID);
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

        public DTOWFLDefineData WFLSetting_DefineData(int defineID, int stepID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_DefineData(defineID,stepID);
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

        public void WFLSetting_DefineDetailSave(DTOWFLDefineData item)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_DefineDetailSave(item);
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

        public void WFLSetting_DefineWFLSave(int defineID, DTOWFLWorkFlow item)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_DefineWFLSave(defineID,item);
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

        public DTOResult WFLSetting_StepDefineList(int defineID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_StepDefineList(defineID, request);
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

        public DTOResult WFLSetting_StepDefineNotInList(int defineID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_StepDefineNotInList(defineID, request);
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

        public void WFLSetting_StepDefineNotInSave(int defineID, List<int> lstStepID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_StepDefineNotInSave(defineID, lstStepID);
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

        public void WFLSetting_StepDefineDelete(int defineID, List<int> lstStepID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_StepDefineDelete(defineID, lstStepID);
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

        public DTOResult WFLSetting_DefineGroupList(int defineID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_DefineGroupList(defineID, request);
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

        public DTOResult WFLSetting_DefineGroupNotInList(int defineID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_DefineGroupNotInList(defineID, request);
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

        public void WFLSetting_DefineGroupNotInSave(int defineID, List<int> lstGroupID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_DefineGroupNotInSave(defineID, lstGroupID);
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

        public void WFLSetting_DefineGroupDelete(int defineID, List<int> lstGroupID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_DefineGroupDelete(defineID, lstGroupID);
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

        public DTOResult WFLSetting_DefineEventList(int defineID, int workflowID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_DefineEventList(defineID, workflowID, request);
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

        public DTOResult WFLSetting_DefineEventNotInList(int defineID, int workflowID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_DefineEventNotInList(defineID, workflowID, request);
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

        public void WFLSetting_DefineEventNotInSave(int defineID, int workflowID, List<int> lstEventID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_DefineEventNotInSave(defineID, workflowID, lstEventID);
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

        public DTOWFLDefineEvent WFLSetting_DefineEventGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_DefineEventGet(id);
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

        public void WFLSetting_DefineEventSave(DTOWFLDefineEvent item)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_DefineEventSave(item);
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

        public void WFLSetting_DefineEventDelete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_DefineEventDelete(lstid);
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

        public List<SYSUser> WFLSetting_DefineEventUserRead(int defineID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_DefineEventUserRead(defineID);
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

        #region Function

        public DTOResult WFLSetting_FunctionList(string request, int workflowID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_FunctionList(request, workflowID);
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

        public DTOResult WFLSetting_FunctionNotInList(string request, int workflowID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_FunctionNotInList(request, workflowID);
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

        public void WFLSetting_FunctionNotInSave(int workflowID, List<SYSFunction> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_FunctionNotInSave(workflowID, lst);
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

        public void WFLSetting_FunctionDelete(int workflowID, List<SYSFunction> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_FunctionDelete(workflowID, lst);
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

        public List<DTOSYSAction> WFLSetting_ActionList(int workflowID, int functionID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_ActionList(workflowID, functionID);
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

        public DTOWFLDefineData WFLSetting_FunctionData(int stepID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_FunctionData(stepID);
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

        public List<DTOWFLStep> WFLSetting_StepParentAllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_StepParentAllList();
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

        public void WFLSetting_ActionSave(int workflowID, int functionID, List<int> lstActID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_ActionSave(workflowID,functionID,lstActID);
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

        public DTOResult WFLSetting_WFEvent_List(string request, int workflowID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_WFEvent_List(request, workflowID);
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

        public DTOResult WFLSetting_WFEvent_NotInList(string request, int workflowID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_WFEvent_NotInList(request, workflowID);
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

        public void WFLSetting_WFEvent_NotInSaveList(int workflowID, List<int> lstId)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_WFEvent_NotInSaveList(workflowID, lstId);
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

        public void WFLSetting_WFEvent_DeleteList(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_WFEvent_DeleteList(lstid);
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

        //public DTOWFL_WFEvent WFLSetting_WFEventGet(int id)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLWorkFlow>())
        //        {
        //            return bl.WFLSetting_WFEventGet(id);
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}

        //public int WFLSetting_WFEventSave(DTOWFL_WFEvent item, int workflowID)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLWorkFlow>())
        //        {
        //            return bl.WFLSetting_WFEventSave(item, workflowID);
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}

        //public void WFLSetting_WFEventDelete(List<int> lstid)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLWorkFlow>())
        //        {
        //            bl.WFLSetting_WFEventDelete(lstid);
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}

        public DTOResult WFLSetting_ChildrenFlow(string request, int workflowID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_ChildrenFlow(request, workflowID);
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

        public int WFLSetting_ChildrenFlow_Save(DTOWFLWorkFlow item, int workflowID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_ChildrenFlow_Save(item, workflowID);
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
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_ChildrenFlow_Get(id);
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
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_ChildrenFlow_Delete(id);
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

        #region Packet
        public DTOResult WFLPacket_Read(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_Read(request);
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

        public DTOWFLPacket WFLPacket_Get(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_Get(id);
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

        public long WFLPacket_Save(DTOWFLPacket item)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_Save(item);
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

        public void WFLPacket_Delete(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLPacket_Delete(id);
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

        public void WFLPacket_Send(List<long> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLPacket_Send(lstid);
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

        public DTOResult WFLPacket_ORDGroupProductList(long packetID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_ORDGroupProductList(packetID, request);
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

        public DTOResult WFLPacket_ORDGroupProductNotInList(long packetID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_ORDGroupProductNotInList(packetID, request);
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

        public void WFLPacket_ORDGroupProductSaveList(long packetID, List<int> lstId)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLPacket_ORDGroupProductSaveList(packetID, lstId);
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

        public DTOResult WFLPacket_DITOMasterList(long packetID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_DITOMasterList(packetID, request);
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

        public DTOResult WFLPacket_DITOMasterNotInList(long packetID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_DITOMasterNotInList(packetID, request);
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

        public void WFLPacket_DITOMasterSaveList(long packetID, List<int> lstId)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLPacket_DITOMasterSaveList(packetID, lstId);
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

        public DTOResult WFLPacket_COTOMasterList(long packetID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_COTOMasterList(packetID, request);
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
        public DTOResult WFLPacket_COTOMasterNotInList(long packetID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_COTOMasterNotInList(packetID, request);
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

        public void WFLPacket_COTOMasterSaveList(long packetID, List<int> lstId)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLPacket_COTOMasterSaveList(packetID, lstId);
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

        public void WFLPacket_DetailDelete(long packetID, List<long> lstId)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLPacket_DetailDelete(packetID, lstId);
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

        #region PacketSetting
        public List<SYSVar> WFLPacket_SettingType()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_SettingType();
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
        public List<DTOWFLPacket_Setting> WFLPacket_SettingAllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_SettingAllList();
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
        public DTOResult WFLPacket_SettingRead(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_SettingRead(request);
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
        public DTOWFLPacket_Setting WFLPacket_SettingGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_SettingGet(id);
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

        public DTOWFLEvent WFLPacketSetting_EventGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacketSetting_EventGet(id);
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

        public int WFLPacketSetting_EventSave(DTOWFLEvent item, int packetSettingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacketSetting_EventSave(item, packetSettingID);
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

        public List<DTOWFLTemplate> WFLPacket_SettingTemplateRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_SettingTemplateRead();
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

        public List<CUSCustomer> WFLPacket_SettingCustomerRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_SettingCustomerRead();
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

        public List<SYSUser> WFLPacket_SettingUserRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLPacket_SettingUserRead();
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

        public void WFLPacketSetting_Save(DTOWFLPacket_Setting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLPacketSetting_Save(item);
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

        public void WFLPacket_SettingDelete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLPacket_SettingDelete(lstid);
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

        public List<CUSSettingsReport> CUSSettingsPlan_AllList(int cusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.CUSSettingsPlan_AllList(cusId);
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
        #endregion

        #region WFEvent
        public DTOResult WFLSetting_WFEvent_Read(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_WFEvent_Read(request);
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

        public DTOWFEvent WFLSetting_WFEvent_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_WFEvent_Get(id);
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

        public int WFLSetting_WFEvent_Save(DTOWFEvent item)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_WFEvent_Save(item);
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

        public void WFLSetting_WFEvent_Delete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    bl.WFLSetting_WFEvent_Delete(lstid);
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

        public List<DTOWFLField> WFLSetting_WFEvent_TableRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_WFEvent_TableRead();
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

        public List<DTOWFLField> WFLSetting_WFEvent_FieldRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_WFEvent_FieldRead();
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

        public DTOWFLEvent_SysVar WFLSetting_WFEvent_SysVar()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_WFEvent_SysVar();
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

        public List<DTOWFLTemplate> WFLSetting_WFEvent_TemplateRead()
        {
            try
            {
                using (var bl = CreateBusiness<BLWorkFlow>())
                {
                    return bl.WFLSetting_WFEvent_TemplateRead();
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
    }
}

