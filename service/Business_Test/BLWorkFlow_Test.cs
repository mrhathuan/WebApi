using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Kendo.Mvc.UI;
using DTO;
namespace Business_Test
{
    [TestClass]
    public class BLWorkFlow_Test : BaseTest
    {
        #region WFLSetting

        [TestMethod]
        public void WFLSetting_CRUD()
        {
            string fileName = "WFLSetting_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DTOWFLDefine item = new DTOWFLDefine();
            DTOWFLDefine item1 = new DTOWFLDefine();

            item.ID = -1;
            item.Code = "Test001";
            item.DefineName = "Test001";
            item.StepID = 0;

            int IDWF = 0;
            DTOWFLWorkFlow itemwf = new DTOWFLWorkFlow();
            DTOWFLWorkFlow itemwf1 = new DTOWFLWorkFlow();
            itemwf.Code = "TestAbc";
            itemwf.HasEvent = false;
            itemwf.ID = 0;
            itemwf.IsSelected = false;
            itemwf.UseAllWorkFlow = false;
            itemwf.WorkFlowName = "Test001";

            DTOWFLField itemDF = new DTOWFLField();
            DTOWFLField itemDF1 = new DTOWFLField();
            int IDDF = 0;
            List<int> lstDIDF = new List<int>();
            itemDF.ColumnDescription = "IsKPI";
            itemDF.ColumnName = "IsKPI";
            itemDF.ColumnType = "bool";
            itemDF.ID = -1;
            itemDF.IsApproved = true;
            itemDF.TableDescription = "Test";
            itemDF.TableName = "Test001";

            DTOWFLTemplate itemDT = new DTOWFLTemplate();
            DTOWFLTemplate itemDT1 = new DTOWFLTemplate();
            int IDDT = 0;
            List<int> lstIDDT = new List<int>();
            itemDT.Code = "Test_SendMail";
            itemDT.ID = -1;
            itemDT.FileID = null;
            itemDT.Name = "Test_SendMail";
            itemDT.Subject = "Test_SendMail";
            itemDT.Template = "";

            int ID = 0;
            int stepID = 1;
            int workflowID = 1;
            int functionID = 1;
            using (var bl = new BLWorkFlow())
            {

                // Test WFLSetting_DefineRead
                lstContent.Add("Test method: WFLSetting_DefineRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.WFLSetting_DefineRead(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_DefineSave_Add
                lstContent.Add("Test method: WFLSetting_DefineSave_Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.WFLSetting_DefineSave(item);
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_DefineGet
                lstContent.Add("Test method: WFLSetting_DefineGet");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item1 = bl.WFLSetting_DefineGet(ID);
                Assert.IsTrue(item1 != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item1 != null ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_DefineSave_Edit
                item.Code = "Test002";
                lstContent.Add("Test method: WFLSetting_DefineSave_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.WFLSetting_DefineSave(item);
                item1 = bl.WFLSetting_DefineGet(ID);
                Assert.IsTrue(item1.Code == item.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item1.Code == item.Code ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_DefineDelete
                lstContent.Add("Test method: WFLSetting_DefineDelete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_DefineDelete(ID);
                item1 = bl.WFLSetting_DefineGet(ID);
                Assert.IsTrue(item1 == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item1 == null ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_EventTableRead
                lstContent.Add("Test method: WFLSetting_EventTableRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventTableRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_EventFieldRead
                lstContent.Add("Test method: WFLSetting_EventFieldRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventFieldRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_EventTemplateRead
                lstContent.Add("Test method: WFLSetting_EventTemplateRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventTemplateRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSettingEvent_SysVar
                lstContent.Add("Test method: WFLSettingEvent_SysVar");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSettingEvent_SysVar();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_FunctionList
                lstContent.Add("Test method: WFLSetting_FunctionList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_FunctionList(request, workflowID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_FunctionNotInList
                lstContent.Add("Test method: WFLSetting_FunctionNotInList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_FunctionNotInList(request, workflowID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_StepParentAllList
                lstContent.Add("Test method: WFLSetting_StepParentAllList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_StepParentAllList();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_FunctionData
                lstContent.Add("Test method: WFLSetting_FunctionData");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_FunctionData(stepID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_WFEvent_List
                lstContent.Add("Test method: WFLSetting_WFEvent_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_WFEvent_List(request,workflowID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_WFEvent_NotInList
                lstContent.Add("Test method: WFLSetting_WFEvent_NotInList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_WFEvent_NotInList(request, workflowID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_ChildrenFlow_Save_Add
                lstContent.Add("Test method: WFLSetting_ChildrenFlow_Save_Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                IDWF =  bl.WFLSetting_ChildrenFlow_Save(itemwf, workflowID);
                Assert.IsTrue(IDWF > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (IDWF > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_ChildrenFlow_Get
                lstContent.Add("Test method: WFLSetting_ChildrenFlow_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                itemwf1 = bl.WFLSetting_ChildrenFlow_Get(IDWF);
                Assert.IsTrue(itemwf1 != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemwf1 != null ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_ChildrenFlow_Save_Edit
                itemwf.Code = "Test";
                lstContent.Add("Test method: WFLSetting_ChildrenFlow_Save_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                IDWF = bl.WFLSetting_ChildrenFlow_Save(itemwf, workflowID);
                itemwf1 = bl.WFLSetting_ChildrenFlow_Get(IDWF);
                Assert.IsTrue(itemwf.Code == itemwf1.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemwf.Code == itemwf1.Code ? "success" : "fail"));
                lstContent.Add("");

                itemwf.Code = "Test";
                lstContent.Add("Test method: WFLSetting_ChildrenFlow_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_ChildrenFlow_Delete(IDWF);
                itemwf1 = bl.WFLSetting_ChildrenFlow_Get(IDWF);
                Assert.IsTrue(itemwf1 == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemwf1 == null ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_ActionList
                lstContent.Add("Test method: WFLSetting_ActionList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_ActionList(workflowID,functionID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_FieldRead
                lstContent.Add("Test method: WFLSetting_FieldRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_FieldRead(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_FieldSave_Add
                lstContent.Add("Test method: WFLSetting_FieldSave_Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                IDDF = bl.WFLSetting_FieldSave(itemDF);
                Assert.IsTrue(IDDF > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (IDDF > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_FieldGet
                lstContent.Add("Test method: WFLSetting_FieldGet");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                itemDF1 = bl.WFLSetting_FieldGet(IDDF);
                Assert.IsTrue(itemDF1 != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemDF1 != null ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_FieldSave_Edit
                itemDF.TableName = "Test";
                lstContent.Add("Test method: WFLSetting_FieldSave_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                IDDF = bl.WFLSetting_FieldSave(itemDF);
                itemDF1 = bl.WFLSetting_FieldGet(IDDF);
                Assert.IsTrue(itemDF.TableName == itemDF1.TableName);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemDF.TableName == itemDF1.TableName ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_FieldDelete
                lstDIDF.Add(IDDF);
                lstContent.Add("Test method: WFLSetting_FieldDelete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_FieldDelete(lstDIDF);
                itemDF1 = bl.WFLSetting_FieldGet(IDDF);
                Assert.IsTrue(itemDF1 == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemDF1 == null ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_TemplateRead
                lstContent.Add("Test method: WFLSetting_TemplateRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_TemplateRead(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_TemplateSave_Add
                lstContent.Add("Test method: WFLSetting_TemplateSave_Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                IDDT = bl.WFLSetting_TemplateSave(itemDT);
                Assert.IsTrue(IDDT > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (IDDT > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_TemplateGet
                lstContent.Add("Test method: WFLSetting_TemplateGet");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                itemDT1 = bl.WFLSetting_TemplateGet(IDDT);
                Assert.IsTrue(itemDT1 != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemDT1 != null ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_TemplateSave_Edit
                itemDT.Code = "ABC_SendMail";
                lstContent.Add("Test method: WFLSetting_TemplateSave_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                IDDT = bl.WFLSetting_TemplateSave(itemDT);
                itemDT1 = bl.WFLSetting_TemplateGet(IDDT);
                Assert.IsTrue(itemDT1.Code == itemDT.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemDT1 != null ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_TemplateDelete
                lstIDDT.Add(IDDT);
                lstContent.Add("Test method: WFLSetting_TemplateDelete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_TemplateDelete(lstIDDT);
                itemDT1 = bl.WFLSetting_TemplateGet(IDDT);
                Assert.IsTrue(itemDT1 == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemDT1 == null ? "success" : "fail"));
                lstContent.Add("");

                // Test WFLSetting_EventCustomerRead
                lstContent.Add("Test method: WFLSetting_EventCustomerRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventCustomerRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_EventUserRead
                lstContent.Add("Test method: WFLSetting_EventUserRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventUserRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_EventTypeOfActionRead
                lstContent.Add("Test method: WFLSetting_EventTypeOfActionRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventTypeOfActionRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_EventStatusOfOrderRead
                lstContent.Add("Test method: WFLSetting_EventStatusOfOrderRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventStatusOfOrderRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_EventStatusOfPlanRead
                lstContent.Add("Test method: WFLSetting_EventStatusOfPlanRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventStatusOfPlanRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_EventStatusOfDITOMasterRead
                lstContent.Add("Test method: WFLSetting_EventStatusOfDITOMasterRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventStatusOfDITOMasterRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_EventKPIReasonRead
                lstContent.Add("Test method: WFLSetting_EventKPIReasonRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventKPIReasonRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_EventStatusOfAssetTimeSheetRead
                lstContent.Add("Test method: WFLSetting_EventStatusOfAssetTimeSheetRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventStatusOfAssetTimeSheetRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test WFLSetting_EventTypeOfAssetTimeSheetRead
                lstContent.Add("Test method: WFLSetting_EventTypeOfAssetTimeSheetRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.WFLSetting_EventTypeOfAssetTimeSheetRead();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);     
            }
        }
        #endregion

        //[TestMethod]
        //public void WFLSetting_DefineRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineGet()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineSave()
        //{
        //    Assert.Fail();
        //}
        //[TestMethod]
        //public void WFLSetting_EventTableRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventFieldRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventTemplateRead()
        //{
        //    Assert.Fail();
        //} 
        //[TestMethod]
        //public void WFLSettingEvent_SysVar()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_FunctionList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_FunctionNotInList()
        //{
        //    Assert.Fail();
        //}
        //[TestMethod]
        //public void WFLSetting_StepParentAllList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_FunctionData()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_FieldRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_FieldGet()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_FieldSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_FieldDelete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_TemplateRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_TemplateGet()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_TemplateSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_TemplateDelete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_WFEventList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_WFEventGet()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_WFEventSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_WFEventDelete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventCustomerRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventUserRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventTypeOfActionRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventStatusOfOrderRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventStatusOfPlanRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventStatusOfDITOMasterRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventKPIReasonRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventStatusOfAssetTimeSheetRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventTypeOfAssetTimeSheetRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventGet()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_EventDelete()
        //{
        //    Assert.Fail();
        //}
       
        //[TestMethod]
        //public void WFLSetting_StepParentList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_StepChildList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineData()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_StepSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineDetailSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineWFLSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_StepDefineList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_StepDefineNotInList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_StepDefineNotInSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_StepDefineDelete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineGroupList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineGroupNotInList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineGroupNotInSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineGroupDelete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineEventList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineEventNotInList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineEventNotInSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineEventGet()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineEventSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_DefineEventDelete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_FunctionNotInSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void WFLSetting_FunctionDelete()
        //{
        //    Assert.Fail();
        //}

        ////[TestMethod]
        ////public void WFLSetting_ActionList()
        ////{
        ////    Assert.Fail();
        ////}

        //[TestMethod]
        //public void WFLSetting_ActionSave()
        //{
        //    Assert.Fail();
        //}
    }
}
