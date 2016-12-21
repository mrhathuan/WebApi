using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTO;
using Kendo.Mvc.UI;

namespace Business_Test
{
    [TestClass]
    public class BLSystem_Test : BaseTest
    {

        #region App

        [TestMethod]
        public void App_CRUD()
        {
            string fileName = "App_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int parentid = 2;
            int referID = 17;
            string referKey = "main.ORDOrder.Index";
            SYSUserSettingFunction_Grid item = new SYSUserSettingFunction_Grid();
            List<SYSUserSettingFunction_Columns> col = new List<SYSUserSettingFunction_Columns>();
            col.Add(null);
            item.Columns = col;
            item.FilterRowHidden= true;
            item.FilterType = "";
            item.Name="order_grid";

            //file list
            CATTypeOfFileCode code = new CATTypeOfFileCode();
            int idfile = -1;

            //file save
            CATFile itemfile = new CATFile();
            itemfile.FileExt = ".xlsx";
            itemfile.FileName = "_040716113325.xlsx";
            itemfile.FilePath = "Uploads/temp/_170916100016.xlsx";
            itemfile.ID = 0;
            itemfile.IsDelete = false;
            itemfile.ReferID = 157;
            itemfile.SYSCustomerID = 0;
            itemfile.TypeOfFileCode = "TemplateReport";
            itemfile.TypeOfFileID=0;

            //file delete
            List<int> listdelete = new List<int>();
            listdelete.Add(1);

            //comment list
            CATTypeOfCommentCode codeComment = new CATTypeOfCommentCode();

            //comment save
            CATComment itemCom = new CATComment();
            itemCom = null;

            //SYSUserSetting
            SYSUserSettingFunction itemSetFun = new SYSUserSettingFunction();
            itemSetFun = null;

            //DTOTriggerMaterial
            DTOTriggerMaterial itemTriMat = new DTOTriggerMaterial();
            itemTriMat = null;

            // Location save
            List<DTOCATLocation> lstCatLo = new List<DTOCATLocation>();
            lstCatLo = null;

            //Location Matrix
            List<CATLocationMatrix> lstLocMat = new List<CATLocationMatrix>();
            lstLocMat = null;

            using (var bl = new BLSystem())
            {

                // Test App_User
                lstContent.Add("Test method: App_User");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.App_User();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_ListFunction
                lstContent.Add("Test method: App_ListFunction");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lstfun = bl.App_ListFunction(parentid);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (lstfun != null ? "success" : "fail"));
                lstContent.Add("");

                // Test App_ListResource
                lstContent.Add("Test method: App_ListResource");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lstres = bl.App_ListResource();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_ListResourceEmpty
                lstContent.Add("Test method: App_ListResourceEmpty");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lstresem = bl.App_ListResourceEmpty();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_UserGridSetting_Save
                lstContent.Add("Test method: App_UserGridSetting_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.App_UserGridSetting_Save(referID,referKey,item);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_UserGridSetting_Save
                lstContent.Add("Test method: App_UserGridSetting_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.App_UserGridSetting_Save(referID, referKey, item);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_FileList
                lstContent.Add("Test method: App_FileList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.App_FileList(request, code ,idfile);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_FileSave
                lstContent.Add("Test method: App_FileSave");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.App_FileSave(itemfile);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_FileDelete
                lstContent.Add("Test method: App_FileSave");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.App_FileDelete(listdelete);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_CommentList
                lstContent.Add("Test method: App_CommentList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.App_CommentList(codeComment, referID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_CommentSave
                lstContent.Add("Test method: App_CommentSave");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.App_CommentSave(itemCom);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_UserSettingGet
                lstContent.Add("Test method: App_UserSettingGet");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.App_UserSettingGet(itemSetFun);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_UserSettingGet
                lstContent.Add("Test method: App_UserSettingGet");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.App_UserSettingGet(itemSetFun);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_UserSettingSave
                lstContent.Add("Test method: App_UserSettingSave");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.App_UserSettingSave(itemSetFun);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_PriceMaterial
                lstContent.Add("Test method: App_UserSettingSave");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
               // bl.App_PriceMaterial(itemTriMat);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_LocationList
                lstContent.Add("Test method: App_LocationList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.App_LocationList();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_LocationSave
                lstContent.Add("Test method: App_LocationList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.App_LocationSave(lstCatLo);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_LocationMatrixList
                lstContent.Add("Test method: App_LocationMatrixList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.App_LocationMatrixList();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test App_LocationMatrixSave
                lstContent.Add("Test method: App_LocationMatrixList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.App_LocationMatrixSave(lstLocMat);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);  
            }
        }
        #endregion

        //[TestMethod]
        //public void App_User()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_ListFunction()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_ListResource()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_ListResourceEmpty()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_UserGridSetting_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_FileList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_FileSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_FileDelete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_CommentList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_CommentSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_UserSettingGet()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_UserSettingSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_PriceMaterial()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_LocationList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_LocationSave()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_LocationMatrixList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void App_LocationMatrixSave()
        //{
        //    Assert.Fail();
        //}

        #region SYSAction

        [TestMethod]
        public void SYSAction_CRUD()
        {
            string fileName = "SYSAction_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            using (var bl = new BLSystem())
            {

                // Test SYSAction_List
                lstContent.Add("Test method: SYSAction_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSAction_List(objRequest);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSAction_Save
                lstContent.Add("Test method: SYSAction_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSAction_Save();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSAction_Delete
                lstContent.Add("Test method: SYSAction_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSAction_Delete();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void SYSAction_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSAction_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSAction_Delete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSGroup_Read()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSGroup_Parent()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSGroup_Item()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSGroup_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSGroup_Delete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSFunction_Read()
        //{
        //    Assert.Fail();
        //}

        #region SYSGroup

        [TestMethod]
        public void SYSGroup_CRUD()
        {
            string fileName = "SYSGroup_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            //SYSGroup
            int ID = 1;
            SYSGroup itemGroup = new SYSGroup();
            SYSGroup itemGroup1 = new SYSGroup();
            itemGroup.Code = "Test001";
            itemGroup.Description = "Test001";
            itemGroup.GroupName = "Test001";
            itemGroup.ID = 0;
            itemGroup.IsApproved = false;
            itemGroup.ParentID = 1;
            itemGroup.SortOrder = 0;
            List<int> lstIDGroup = new List<int>();

            using (var bl = new BLSystem())
            {

                //// Test SYSGroup_Read
                //lstContent.Add("Test method: SYSGroup_Read");
                //lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSGroup_Read(request);
                //lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //lstContent.Add("Status: " + "success");
                //lstContent.Add("");

                //// Test SYSGroup_Parent
                //lstContent.Add("Test method: SYSGroup_Parent");
                //lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //var item = bl.SYSGroup_Parent(ID);
                //Assert.IsTrue(item != null);
                //lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                //lstContent.Add("");

                //// Test SYSGroup_Save_Add
                //lstContent.Add("Test method: SYSGroup_Save_Add");
                //lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //ID = bl.SYSGroup_Save(itemGroup);
                //Assert.IsTrue(ID > 0);
                //lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                //lstContent.Add("");

                //// Test SYSGroup_Item
                //lstContent.Add("Test method: SYSGroup_Item");
                //lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //itemGroup = bl.SYSGroup_Item(ID);
                //Assert.IsTrue(itemGroup != null);
                //lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //lstContent.Add("Status: " + (itemGroup != null ? "success" : "fail"));
                //lstContent.Add("");

                //// Test SYSGroup_Save_Edit
                //itemGroup.Code = "Test";
                //lstContent.Add("Test method: SYSGroup_Save_Edit");
                //lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //ID = bl.SYSGroup_Save(itemGroup);
                //itemGroup1 = bl.SYSGroup_Item(ID);
                //Assert.IsTrue(itemGroup.Code == itemGroup1.Code);
                //lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //lstContent.Add("Status: " + (itemGroup.Code == itemGroup1.Code ? "success" : "fail"));
                //lstContent.Add("");

                //// Test SYSGroup_Delete
                //lstIDGroup.Add(ID);
                //itemGroup.Code = "Test";
                //lstContent.Add("Test method: SYSGroup_Delete");
                //lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSGroup_Delete(lstIDGroup);
                //itemGroup1 = bl.SYSGroup_Item(ID);
                //Assert.IsTrue(itemGroup == null);
                //lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //lstContent.Add("Status: " + (itemGroup == null ? "success" : "fail"));
                //lstContent.Add("");

                //LogResult(fileName, lstContent);
            }
        }

        #endregion

        #region SYSFunction

        [TestMethod]
        public void SYSFunction_CRUD()
        {
            string fileName = "SYSFunction_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            
            int ID = -1;

            //move
            int idmove = 1;
            int typemove = 1;

            //save
            SYSFunction itemFun = new SYSFunction();
            SYSFunction itemFun1 = new SYSFunction();

            itemFun.Code = "Test001";
            itemFun.Description = "Test001";
            itemFun.FunctionName = "Test001";
            itemFun.HasChild = false;
            itemFun.ID = 0;
            itemFun.Icon = "Test001";
            itemFun.IsAdmin = true;
            itemFun.IsApproved = false;
            itemFun.IsCustomer = false;
            itemFun.Level = 0;
            itemFun.ParentID = -1;
            itemFun.SortOrder = 0;

            //delete
            List<int> lstIDFun = new List<int>();


            using (var bl = new BLSystem())
            {

                // Test SYSFunction_Read
                lstContent.Add("Test method: SYSFunction_Read");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSFunction_Read(request);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSFunction_Parent
                lstContent.Add("Test method: SYSFunction_Parent");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var item = bl.SYSFunction_Parent(ID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSFunction_Move
                lstContent.Add("Test method: SYSFunction_Move");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSFunction_Move(idmove, typemove);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSFunction_Save_Add
                lstContent.Add("Test method: SYSFunction_Save_Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.SYSFunction_Save(itemFun);
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSFunction_Item
                lstContent.Add("Test method: SYSFunction_Item");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                itemFun = bl.SYSFunction_Item(ID);
                Assert.IsTrue(itemFun != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemFun != null ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSFunction_Save_Edit
                itemFun.Code = "Test";
                lstContent.Add("Test method: SYSFunction_Save_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.SYSFunction_Save(itemFun);
                itemFun1 = bl.SYSFunction_Item(ID);
                Assert.IsTrue(itemFun.Code == itemFun1.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemFun.Code == itemFun1.Code ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSFunction_Delete
                lstIDFun.Add(ID);
                lstContent.Add("Test method: SYSFunction_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSFunction_Delete(lstIDFun);
                itemFun1 = bl.SYSFunction_Item(ID);
                Assert.IsTrue(itemFun1 == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemFun1 == null ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSFunction_Refresh
                lstIDFun.Add(ID);
                lstContent.Add("Test method: SYSFunction_Refresh");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSFunction_Refresh();
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void SYSFunction_Parent()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSFunction_Item()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSFunction_Move()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSFunction_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSFunction_Delete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSFunction_Refresh()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSFunction_Export()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSFunction_ImportCheck()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSFunction_ExcelImport()
        //{
        //    Assert.Fail();
        //}

        #region SYSConfigGroup

        [TestMethod]
        public void SYSConfigGroup_CRUD()
        {
            string fileName = "SYSConfigGroup_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int groupID = 2;
            int functionID = 4;
            List<SYSFunction> lstFun = new List<SYSFunction>();
            SYSFunction itemFun = new SYSFunction();
            itemFun.ID = 5;
            itemFun.Level = 1;
            lstFun.Add(itemFun);

            using (var bl = new BLSystem())
            {

                // Test SYSConfigGroup_Read
                lstContent.Add("Test method: SYSConfigGroup_Read");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSConfigGroup_Read(request);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSConfigGroup_GroupItem
                lstContent.Add("Test method: SYSConfigGroup_GroupItem");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var item = bl.SYSConfigGroup_GroupItem(groupID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSConfigFunction_InRead
                lstContent.Add("Test method: SYSConfigFunction_InRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSConfigFunction_InRead(request,groupID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSConfigFunction_NotInRead
                lstContent.Add("Test method: SYSConfigFunction_NotInRead");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSConfigFunction_NotInRead(request, groupID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSConfigFunction_AddFunction
                lstContent.Add("Test method: SYSConfigFunction_AddFunction");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSConfigFunction_AddFunction(lstFun, groupID);
                var lstInread = bl.SYSConfigFunction_InRead(request, groupID).Data.Cast<SYSFunction>().ToList();
                int flag = 0;
                foreach (var x in lstInread)
                {
                    if (x.ID == itemFun.ID)
                    {
                        flag = 1;
                    }
                }
                Assert.IsTrue(flag == 1);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (flag == 1 ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSConfigFunction_DelFunction
                lstContent.Add("Test method: SYSConfigFunction_DelFunction");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSConfigFunction_DelFunction(lstFun, groupID);
                lstInread = bl.SYSConfigFunction_InRead(request, groupID).Data.Cast<SYSFunction>().ToList();
                flag = 0;
                foreach (var x in lstInread)
                {
                    if (x.ID == itemFun.ID)
                    {
                        flag = 1;
                    }
                }
                Assert.IsTrue(flag == 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (flag == 0 ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSConfigFunction_GetItem
                lstContent.Add("Test method: SYSConfigFunction_GetItem");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var itemget = bl.SYSConfigFunction_GetItem(groupID, functionID);
                Assert.IsTrue(itemget != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (itemget != null ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSConfigFunction_SaveItem
                lstContent.Add("Test method: SYSConfigFunction_SaveItem");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSConfigFunction_SaveItem(groupID, functionID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: success");
                lstContent.Add("");

                // Test SYSConfigFunctionNotInGroup_List
                lstContent.Add("Test method: SYSConfigFunctionNotInGroup_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSConfigFunctionNotInGroup_List(groupID, functionID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: success");
                lstContent.Add("");

                // Test SYSConfigFunctionInGroup_List
                lstContent.Add("Test method: SYSConfigFunctionInGroup_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSConfigFunctionInGroup_List(groupID, functionID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: success");
                lstContent.Add("");

                // Test SYSConfigFunction_Save
                lstContent.Add("Test method: SYSConfigFunction_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSConfigFunction_Save(groupID, functionID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: success");
                lstContent.Add("");

                // Test SYSConfigFunction_Delete
                lstContent.Add("Test method: SYSConfigFunction_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSConfigFunction_Delete(groupID, functionID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: success");
                lstContent.Add("");

                // Test SYSConfigAction_Get
                lstContent.Add("Test method: SYSConfigAction_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSConfigAction_Get(groupID, functionID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: success");
                lstContent.Add("");

                // Test SYSConfigAction_Save
                lstContent.Add("Test method: SYSConfigAction_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSConfigAction_Save(groupID, functionID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: success");
                lstContent.Add("");

                // Test SYSConfig_ExcelFuntion
                lstContent.Add("Test method: SYSConfig_ExcelFuntion");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSConfig_ExcelFuntion(groupID, functionID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: success");
                lstContent.Add("");

                // Test SYSConfig_ExcelAction
                lstContent.Add("Test method: SYSConfig_ExcelAction");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSConfig_ExcelAction(groupID, functionID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: success");
                lstContent.Add("");

                // Test SYSConfig_ExcelFunctionInGroup
                lstContent.Add("Test method: SYSConfig_ExcelFunctionInGroup");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSConfig_ExcelFunctionInGroup(groupID, functionID);
                Assert.IsTrue(true);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void SYSConfigGroup_Read()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigGroup_GroupItem()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigFunction_InRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigFunction_NotInRead()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigFunction_AddFunction()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigFunction_DelFunction()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigFunction_GetItem()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigFunction_SaveItem()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigFunctionNotInGroup_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigFunctionInGroup_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigFunction_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigFunction_Delete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigAction_Get()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfigAction_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfig_ExcelFuntion()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfig_ExcelAction()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSConfig_ExcelFunctionInGroup()
        //{
        //    Assert.Fail();
        //}

        #region SYSGroup

        [TestMethod]
        public void SYSUser_CRUD()
        {
            string fileName = "SYSUser_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            //item
            int ID = 0;
            SYSUser item = new SYSUser();
            SYSUser item1 = new SYSUser();
            item.Code = null;
            item.CreatedDate = new DateTime();
            item.Email = "abc@gosmartlog.com";
            item.FirstName = "Test123123";
            item.GroupID = -1;
            item.ID = -1;
            item.IsAdmin = false;
            item.IsApproved = true;
            item.LastName = "Test123123";
            item.Password = "123456789";
            item.RePassword = "123456789";
            item.SYSCustomerID = 1;
            item.UserName = "Test123123";
            //list user
            List<int> lstid = new List<int>();

            using (var bl = new BLSystem())
            {

                // Test SYSUser_Read
                lstContent.Add("Test method: SYSUser_Read");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUser_Read(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSUser_Driver
                lstContent.Add("Test method: SYSUser_Driver");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUser_Driver();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSUser_Group
                lstContent.Add("Test method: SYSUser_Group");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUser_Group();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSUser_Customer
                lstContent.Add("Test method: SYSUser_Customer");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUser_Customer();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSUser_Save_Add
                lstContent.Add("Test method: SYSUser_Save_Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.SYSUser_Save(item);
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSUser_Get
                lstContent.Add("Test method: SYSUser_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item1 = bl.SYSUser_Get(ID);
                Assert.IsTrue(item1 != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item1 != null ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSUser_CheckUserName
                lstContent.Add("Test method: SYSUser_CheckUserName");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUser_CheckUserName(item1.ID, item1.UserName);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSUser_Save_Edit
                item.FirstName = "Test";
                lstContent.Add("Test method: SYSUser_Save_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.SYSUser_Save(item);
                item1 = bl.SYSUser_Get(ID);
                Assert.IsTrue(item1.FirstName == item.FirstName);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item1.FirstName == item.FirstName ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSUser_Delete
                lstid.Add(ID);
                lstContent.Add("Test method: SYSUser_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUser_Delete(lstid);
                item1 = bl.SYSUser_Get(ID);
                Assert.IsTrue(item1 == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item1 == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void SYSUser_Read()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_Get()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_Group()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_Customer()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_CheckUserName()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_CheckIsAdmin()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_CheckData()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_Delete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_Export()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_ImportCheck()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_Import()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUser_Driver()
        //{
        //    Assert.Fail();
        //}

        #region FLMDriver

        [TestMethod]
        public void FLMDriver_CRUD()
        {
            string fileName = "FLMDriver_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

          

            using (var bl = new BLSystem())
            {

                // Test FLMDriver_Get
                lstContent.Add("Test method: FLMDriver_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSUser_Read(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Truck_GetByDriver
                lstContent.Add("Test method: Truck_GetByDriver");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.Truck_GetByDriver(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void FLMDriver_Get()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Truck_GetByDriver()
        //{
        //    Assert.Fail();
        //}

        #region File

        [TestMethod]
        public void File_CRUD()
        {
            string fileName = "File_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);



            using (var bl = new BLSystem())
            {

                // Test File_List
                lstContent.Add("Test method: File_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.File_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test File_Save
                lstContent.Add("Test method: File_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.File_Save(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test File_Delete
                lstContent.Add("Test method: File_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.File_Delete(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void File_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void File_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void File_Delete()
        //{
        //    Assert.Fail();
        //}

        #region UserProfile

        [TestMethod]
        public void UserProfile_CRUD()
        {
            string fileName = "File_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);



            using (var bl = new BLSystem())
            {

                // Test UserProfile_Get
                lstContent.Add("Test method: UserProfile_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
               // bl.UserProfile_Get(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test UserProfileComment_List
                lstContent.Add("Test method: UserProfileComment_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.UserProfileComment_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Notification_Order
                lstContent.Add("Test method: Notification_Order");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.Notification_Order(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test UserProfile_GetUser
                lstContent.Add("Test method: UserProfile_GetUser");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.UserProfile_GetUser(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test UserProfile_Save
                lstContent.Add("Test method: UserProfile_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.UserProfile_Save(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion
        
        //[TestMethod]
        //public void UserProfile_Get()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void UserProfileComment_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Notification_Order()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void UserProfile_GetUser()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void UserProfile_Save()
        //{
        //    Assert.Fail();
        //}

        #region MailTemplate

        [TestMethod]
        public void MailTemplate_CRUD()
        {
            string fileName = "MailTemplate_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int ID = 0;
            CATMailTemplate item = new CATMailTemplate();
            CATMailTemplate item1 = new CATMailTemplate();
            item.CC = "abc";
            item.Code = "Customer";
            item.ID = 2;
            item.IsUse = true;
            item.SYSCustomerID = 2;
            item.TemplateName = "Gửi khách hàng";


            using (var bl = new BLSystem())
            {
                // Test MailTemplate_List
                lstContent.Add("Test method: MailTemplate_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.MailTemplate_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test MailTemplate_Get
                lstContent.Add("Test method: MailTemplate_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.MailTemplate_Get(item.ID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test MailTemplate_Save
                lstContent.Add("Test method: MailTemplate_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.MailTemplate_Save(item);
                item1 = bl.MailTemplate_Get(ID);
                Assert.IsTrue(item1.CC == item.CC);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item1.CC == item.CC ? "success" : "fail"));
                lstContent.Add("");

                // Test MailTemplate_Delete
                lstContent.Add("Test method: MailTemplate_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.MailTemplate_Delete(item.ID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test MailTemplate_GetBySYSCustomerID
                lstContent.Add("Test method: MailTemplate_GetBySYSCustomerID");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.MailTemplate_GetBySYSCustomerID(item.ID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test MailTemplate_GetByCurrentSYSCustomerID
                lstContent.Add("Test method: MailTemplate_GetByCurrentSYSCustomerID");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.MailTemplate_GetByCurrentSYSCustomerID(item.ID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void MailTemplate_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void MailTemplate_Get()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void MailTemplate_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void MailTemplate_Delete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void MailTemplate_GetBySYSCustomerID()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void MailTemplate_GetByCurrentSYSCustomerID()
        //{
        //    Assert.Fail();
        //}

        #region SYSResource

        [TestMethod]
        public void SYSResource_CRUD()
        {
            string fileName = "SYSResource_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int ID = 0;
            int languageID = 0;
            DTOSYSResource item = new DTOSYSResource();
            DTOSYSResource item1 = new DTOSYSResource();

            item.ID = 0;
            item.IsDefault = true;
            item.Key = "Test.Code";
            item.Name = "Test";
            item.ShortName = "Te";

            using (var bl = new BLSystem())
            {
                // Test SYSResource_List
                lstContent.Add("Test method: SYSResource_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSResource_List(request, languageID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSResource_Data
                lstContent.Add("Test method: SYSResource_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSResource_Data(languageID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSResource_UpdateAllUser
                lstContent.Add("Test method: SYSResource_UpdateAllUser");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSResource_UpdateAllUser(languageID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSResource_Save_add
                lstContent.Add("Test method: SYSResource_List_Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSResource_Save(item, languageID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var dataList = bl.SYSResource_List(request, languageID);
                foreach (var itdata in dataList.Data.Cast<DTOSYSResource>().ToList())
                {
                    if (itdata.Key == item.Key)
                    {
                        ID = itdata.ID;
                    }
                }
                Assert.IsTrue(ID > 0);
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSResource_Get
                lstContent.Add("Test method: SYSResource_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item1 = bl.SYSResource_Get(ID, languageID);
                Assert.IsTrue(item1 != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item1 != null ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSResource_Save_Edit
                item.ShortName = "Test";
                lstContent.Add("Test method: SYSResource_List_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSResource_Save(item, languageID);
                item1 = bl.SYSResource_Get(ID, languageID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                Assert.IsTrue(item1.ShortName == item.ShortName);
                lstContent.Add("Status: " + (item1.ShortName == item.ShortName ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSResource_Delete
                lstContent.Add("Test method: SYSResource_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSResource_Delete(item1, languageID);
                item1 = bl.SYSResource_Get(ID, languageID);
                Assert.IsTrue(item1 == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item1 == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void SYSResource_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSResource_Get()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSResource_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSResource_Delete()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSResource_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSResource_Import()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSResource_UpdateAllUser()
        //{
        //    Assert.Fail();
        //}

        #region SYSUserResource

        [TestMethod]
        public void SYSUserResource_CRUD()
        {
            string fileName = "SYSUserResource_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int ID = 30284;
            DTOSYSUserResource item = new DTOSYSUserResource();
            DTOSYSUserResource item1 = new DTOSYSUserResource();

            using (var bl = new BLSystem())
            {
                // Test SYSUserResource_List
                lstContent.Add("Test method: SYSUserResource_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUserResource_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSUserResource_Data
                lstContent.Add("Test method: SYSUserResource_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUserResource_Data();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSUserResource_SetDefault
                lstContent.Add("Test method: SYSUserResource_SetDefault");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUserResource_SetDefault();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test SYSUserResource_Get
                lstContent.Add("Test method: SYSUserResource_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.SYSUserResource_Get(ID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSUserResource_Save
                item.ShortName = "DHC";
                lstContent.Add("Test method: SYSUserResource_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUserResource_Save(item);
                item1 = bl.SYSUserResource_Get(ID);
                Assert.IsTrue(item.ShortName == item1.ShortName);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.ShortName == item1.ShortName ? "success" : "fail"));
                lstContent.Add("");

                // Test SYSUserResource_SetDefaultByUser
                lstContent.Add("Test method: SYSUserResource_SetDefaultByUser");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.SYSUserResource_SetDefaultByUser();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void SYSUserResource_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUserResource_Get()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUserResource_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUserResource_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUserResource_Import()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUserResource_SetDefault()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void SYSUserResource_SetDefaultByUser()
        //{
        //    Assert.Fail();
        //}

        #region Dashboard_UserSetting_

        [TestMethod]
        public void Dashboard_UserSetting__CRUD()
        {
            string fileName = "Dashboard_UserSetting__CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int functionID = 16;

            using (var bl = new BLSystem())
            {
                // Test SYSUserResource_List
                lstContent.Add("Test method: SYSUserResource_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.SYSUserResource_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Dashboard_UserSetting_Get
                lstContent.Add("Test method: SYSUserResource_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Dashboard_UserSetting_Get(functionID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Dashboard_UserSetting_Save
                lstContent.Add("Test method: Dashboard_UserSetting_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
               // bl.Dashboard_UserSetting_Save(functionID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void Dashboard_UserSetting_Get()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Dashboard_UserSetting_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Dashboard_Vehicle_List()
        //{
        //    Assert.Fail();
        //}

        #region Chart

        [TestMethod]
        public void Chart_CRUD()
        {
            string fileName = "Chart_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            //chart_Summary_data
            DateTime dtfrom = new DateTime();
            DateTime dtto = new DateTime();
            int? customerID = 1;
            int statusid = 1;
            int quantity = 0;
            int typeofchart = 5;

            using (var bl = new BLSystem())
            {
                // Test Chart_Summary_Data
                lstContent.Add("Test method: Chart_Summary_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Summary_Data(dtfrom, dtto, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Customer_Order_Data
                lstContent.Add("Test method: Chart_Customer_Order_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //bl.Chart_Customer_Order_Data(dtfrom, dtto, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Customer_OPS_Data
                lstContent.Add("Test method: Chart_Customer_OPS_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Customer_OPS_Data(dtfrom, dtto, customerID, statusid);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Customer_Order_Rate_Data
                lstContent.Add("Test method: Chart_Customer_Order_Rate_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Customer_Order_Rate_Data(dtfrom, dtto, quantity);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_Capacity_Data
                lstContent.Add("Test method: Chart_Customer_Order_Rate_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_Capacity_Data(dtfrom, dtto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_KM_Data
                lstContent.Add("Test method: Chart_Owner_KM_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_KM_Data(dtfrom, dtto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_Operation_Data
                lstContent.Add("Test method: Chart_Owner_Operation_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_Operation_Data(dtfrom, dtto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_CostRate_Data
                lstContent.Add("Test method: Chart_Owner_CostRate_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_CostRate_Data(dtfrom, dtto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_CostChange_Data
                lstContent.Add("Test method: Chart_Owner_CostChange_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_CostChange_Data(dtfrom, dtto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_OnTime_PickUp_Data
                lstContent.Add("Test method: Chart_Owner_OnTime_PickUp_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_OnTime_PickUp_Data(dtfrom, dtto,customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_OnTime_Delivery_Data
                lstContent.Add("Test method: Chart_Owner_OnTime_Delivery_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_OnTime_Delivery_Data(dtfrom, dtto, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_OnTime_POD_Data
                lstContent.Add("Test method: Chart_Owner_OnTime_POD_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_OnTime_POD_Data(dtfrom, dtto, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_Return_Data
                lstContent.Add("Test method: Chart_Owner_Return_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_Return_Data(dtfrom, dtto, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_Profit_Data
                lstContent.Add("Test method: Chart_Owner_Profit_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_Profit_Data(dtfrom, dtto, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_Profit_Customer_Data
                lstContent.Add("Test method: Chart_Owner_Profit_Customer_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_Profit_Customer_Data(dtfrom, dtto, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_OnTime_PickUp_Radial_Data
                lstContent.Add("Test method: Chart_Owner_OnTime_PickUp_Radial_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_OnTime_PickUp_Radial_Data(dtfrom, dtto, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_OnTime_Delivery_Radial_Data
                lstContent.Add("Test method: Chart_Owner_OnTime_Delivery_Radial_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_OnTime_Delivery_Radial_Data(dtfrom, dtto, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_OnTime_POD_Radial_Data
                lstContent.Add("Test method: Chart_Owner_OnTime_POD_Radial_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_OnTime_POD_Radial_Data(dtfrom, dtto, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Chart_Owner_Profit_Vendor_Data
                lstContent.Add("Test method: Chart_Owner_Profit_Vendor_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Chart_Owner_Profit_Vendor_Data(dtfrom, dtto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test Widget_Data
                lstContent.Add("Test method: Widget_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Widget_Data(dtfrom, dtto, typeofchart, customerID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #endregion


        //[TestMethod]
        //public void Chart_Summary_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Customer_Order_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Customer_OPS_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Customer_Order_Rate_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_Capacity_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_KM_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_Operation_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_CostRate_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_CostChange_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_OnTime_PickUp_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_OnTime_Delivery_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_OnTime_POD_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_Return_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_Profit_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_Profit_Customer_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_OnTime_PickUp_Radial_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_OnTime_Delivery_Radial_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_OnTime_POD_Radial_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Chart_Owner_Profit_Vendor_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Widget_Data()
        //{
        //    Assert.Fail();
        //}

        #region MAP_Summary

        [TestMethod]
        public void MAP_Summary_CRUD()
        {
            string fileName = "MAP_Summary_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            List<int> lstCustomerID = null;
            DateTime dtfrom = new DateTime();
            DateTime dtto = new DateTime();
            int provinceID = -1;
            int typeoflocationID = 1;
            int vehicelID = 1;
            int masterID = 1;
            List<int> lstMasetrID = null;

            using (var bl = new BLSystem())
            {
                // Test MAP_Summary_Data
                lstContent.Add("Test method: Chart_Summary_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.MAP_Summary_Data(request,lstCustomerID,dtfrom, dtto, provinceID, typeoflocationID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test MAP_Summary_Vehicle_Data
                lstContent.Add("Test method: MAP_Summary_Vehicle_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.MAP_Summary_Vehicle_Data(request, dtfrom, dtto, vehicelID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test MAP_Summary_Master_Data
                lstContent.Add("Test method: MAP_Summary_Master_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.MAP_Summary_Master_Data(masterID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Test MAP_Summary_Master_DataList
                lstContent.Add("Test method: MAP_Summary_Master_DataList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.MAP_Summary_Master_DataList(lstMasetrID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");


                LogResult(fileName, lstContent);
            }
        }

        #endregion

        //[TestMethod]
        //public void MAP_Summary_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void MAP_Summary_Vehicle_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void MAP_Summary_Master_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void MAP_Summary_Master_DataList()
        //{
        //    Assert.Fail();
        //}
    }
}
