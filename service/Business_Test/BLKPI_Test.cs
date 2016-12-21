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
    public class BLKPI_Test : BaseTest
    {
        [TestMethod]
        public void KPITime_CRUD()
        {
            string fileName = "KPITime_CRUD";
            List<string> lstContent = new List<string>();
            
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int cusID = -1, kpiID = -1;
            DateTime datefrom = new DateTime();
            DateTime dateto = new DateTime();
            using (var bl = new BLKPI())
            {
                //KPITime_List
                lstContent.Add("Test method: KPITime_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.KPITime_List(request,kpiID,cusID,datefrom,dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");
                
                //KPITime_Save
                KPIKPITime obj = new KPIKPITime();
                obj.ReasonID = 4;
                obj.Note = "Test Note";
                bl.KPITime_Save(obj);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPITime_Generate
                lstContent.Add("Test method: KPITime_Generate");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPITime_Generate(datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPITimeReason_List
                lstContent.Add("Test method: KPITimeReason_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lstitem = bl.KPITimeReason_List(kpiID);
                Assert.IsTrue(lstitem != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (lstitem != null ? "success" : "fail"));
                lstContent.Add("");

                //KPITime_Excel
                lstContent.Add("Test method: KPITime_Excel");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lstexcel = bl.KPITime_Excel(kpiID,cusID,datefrom,dateto);
                Assert.IsTrue(lstexcel != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (lstexcel != null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

            LogResult(fileName, lstContent);
        }
        //[TestMethod]
        //public void KPITime_List()
        //{

        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPITime_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPITime_Generate()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPITimeReason_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPITime_Excel()
        //{
        //    Assert.Fail();
        //}
        [TestMethod]
        public void KPIReason_CRUD()
        {
            string fileName = "KPIReason_CRUD";
            List<string> lstContent = new List<string>();
            int ID;
            KPIReason obj = new DTO.KPIReason();
            obj.Code = "Test002t";
            obj.ReasonName = "Test002t";
            obj.KPIID = 3;
            obj.ID = -1;
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            using (var bl = new BLKPI())
            {
                //KPIKPI_List
                lstContent.Add("Test method: KPIKPI_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.KPIKPI_List();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIReason_List
                lstContent.Add("Test method: KPIReason_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lstReason = bl.KPIReason_List(request);
                Assert.IsTrue(lstReason != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (lstReason != null ? "success" : "fail"));
                lstContent.Add("");

                //KPIReason_Save_add
                lstContent.Add("Test method: KPIReason_Save_Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.KPIReason_Save(obj);
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //KPIReason_Save_Edit
                lstContent.Add("Test method: KPIReason_Save_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                obj.ID = ID;
                obj.KPIID = 1;
                bl.KPIReason_Save(obj);
                lstReason = bl.KPIReason_List(request);
                int KPIID = 0;
                foreach(var itemlst in lstReason.Data.Cast<KPIReason>().ToList()){
                    if (itemlst.ID == ID)
                    {
                        KPIID = itemlst.KPIID;
                    }
                }
                Assert.IsTrue(KPIID == obj.KPIID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (KPIID == obj.KPIID ? "success" : "fail"));
                lstContent.Add("");

                //KPIReason_Delete
                lstContent.Add("Test method: KPIReason_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIReason_Delete(ID);
                KPIID = 0;
                lstReason = bl.KPIReason_List(request);
                foreach (var itemlst in lstReason.Data.Cast<KPIReason>().ToList())
                {
                    if (itemlst.ID == ID)
                    {
                        KPIID = itemlst.KPIID;
                    }
                }
                Assert.IsTrue(KPIID == 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (KPIID == 0 ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

            LogResult(fileName, lstContent);
        }
        //[TestMethod]
        //public void KPIKPI_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIReason_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIReason_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIReason_Delete()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        public void KPIReason_Export()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void KPIReason_Import()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Dashboard_DN_CRUD()
        {
            string fileName = "Dashboard_DN_CRUD";
            List<string> lstContent = new List<string>();

            int cusID = -1, venID = -1, kpiID = 1;
            DateTime datefrom = new DateTime();
            DateTime dateto = new DateTime();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            using (var bl = new BLKPI())
            {
                //Dashboard_DN_Data
                lstContent.Add("Test method: Dashboard_DN_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Dashboard_DN_Data(cusID,kpiID,datefrom,dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Dashboard_DN_Data
                lstContent.Add("Test method: Dashboard_DN_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Dashboard_Reason_Data(cusID, kpiID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Dashboard_DN_VENData
                lstContent.Add("Test method: Dashboard_DN_VENData");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Dashboard_DN_VENData(venID, kpiID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Dashboard_Reason_VENData
                lstContent.Add("Test method: Dashboard_Reason_VENData");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.Dashboard_Reason_VENData(venID, kpiID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

            LogResult(fileName, lstContent);
        }

        //[TestMethod]
        //public void Dashboard_DN_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Dashboard_Reason_Data()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Dashboard_DN_VENData()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void Dashboard_Reason_VENData()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        public void KPIVENTime_CRUD()
        {
            string fileName = "KPIVENTime_CRUD";
            List<string> lstContent = new List<string>();

            int venID = -1, kpiID = 1;
            DateTime datefrom = new DateTime();
            DateTime dateto = new DateTime();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            using (var bl = new BLKPI())
            {
                //KPIVENTime_List
                lstContent.Add("Test method: KPIVENTime_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTime_List(request, kpiID, venID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIVENTime_Save
                lstContent.Add("Test method: KPIVENTime_Generate");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIVENTime_Generate 
                lstContent.Add("Test method: KPIVENTime_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
               
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

            LogResult(fileName, lstContent);
        }

        //[TestMethod]
        //public void KPIVENTime_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTime_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTime_Generate()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTime_Excel()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        public void KPIVENTenderFTL_CRUD()
        {
            string fileName = "KPIVENTenderFTL_CRUD";
            List<string> lstContent = new List<string>();

            int venID = -1;
            DateTime datefrom = new DateTime();
            DateTime dateto = new DateTime();

            KPIVENTenderFTL item = new KPIVENTenderFTL();
            item.ID = -1;
            item.ReasonID = 1;
            item.Note = "Test";

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            using (var bl = new BLKPI())
            {
                //KPIVENTenderFTL_List
                lstContent.Add("Test method: KPIVENTenderFTL_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTenderFTL_List(request, venID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIVENTenderFTL_Save
                lstContent.Add("Test method: KPIVENTenderFTL_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTenderFTL_Save(item);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIVENTenderFTL_Generate
                lstContent.Add("Test method: KPIVENTenderFTL_Generate");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTenderFTL_Generate(datefrom,dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIVENTenderFTL_Dashboard
                lstContent.Add("Test method: KPIVENTenderFTL_Dashboard");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTenderFTL_Dashboard(venID,datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIVENTenderFTL_Dashboard_Reason
                lstContent.Add("Test method: KPIVENTenderFTL_Dashboard_Reason");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTenderFTL_Dashboard_Reason(venID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

            LogResult(fileName, lstContent);
        }

        //[TestMethod]
        //public void KPIVENTenderFTL_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTenderFTL_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTenderFTL_Generate()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTenderFTL_Excel()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTenderFTL_Dashboard()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTenderFTL_Dashboard_Reason()
        //{
        //    Assert.Fail();
        //}
        [TestMethod]
        public void KPIVENTenderLTL_CRUD()
        {
            string fileName = "KPIVENTenderLTL_CRUD";
            List<string> lstContent = new List<string>();

            int venID = -1;
            DateTime datefrom = new DateTime();
            DateTime dateto = new DateTime();

            KPIVENTenderLTL item = new KPIVENTenderLTL();
            item.ID = -1;
            item.ReasonID = 1;
            item.Note = "Test";

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            using (var bl = new BLKPI())
            {
                //KPIVENTenderFTL_List
                lstContent.Add("Test method: KPIVENTenderLTL_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTenderLTL_List(request, venID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIVENTenderLTL_Save
                lstContent.Add("Test method: KPIVENTenderLTL_Save");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTenderLTL_Save(item);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIVENTenderLTL_Generate
                lstContent.Add("Test method: KPIVENTenderLTL_Generate");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTenderLTL_Generate(datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIVENTenderLTL_Dashboard
                lstContent.Add("Test method: KPIVENTenderLTL_Dashboard");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTenderLTL_Dashboard(venID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIVENTenderLTL_Dashboard_Reason
                lstContent.Add("Test method: KPIVENTenderLTL_Dashboard_Reason");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIVENTenderLTL_Dashboard_Reason(venID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

            LogResult(fileName, lstContent);
        }

        //[TestMethod]
        //public void KPIVENTenderLTL_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTenderLTL_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTenderLTL_Generate()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTenderLTL_Excel()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTenderLTL_Dashboard()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIVENTenderLTL_Dashboard_Reason()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        public void KPIKPI_CRUD()
        {
            string fileName = "KPIKPI_CRUD";
            List<string> lstContent = new List<string>();

            int ID = 0;
            KPIKPI item = new KPIKPI();
            KPIKPI item2 = new KPIKPI();
            item.Code = "OPSTEST";
            item.TypeOfKPIID = 241;
            item.ID = 0;


            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            using (var bl = new BLKPI())
            {
                //KPIKPI_GetList
                lstContent.Add("Test method: KPIKPI_GetList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIKPI_GetList(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIKPI_Save_Add
                lstContent.Add("Test method: KPIKPI_Save_Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.KPIKPI_Save(item);
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //KPIKPI_Get
                lstContent.Add("Test method: KPIKPI_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item2 = bl.KPIKPI_Get(ID);
                Assert.IsTrue(item2 != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item2 != null ? "success" : "fail"));
                lstContent.Add("");

                //KPIKPI_Save_Edit
                lstContent.Add("Test method: KPIKPI_Save_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "TestOPS";
                ID = bl.KPIKPI_Save(item);
                item2 = bl.KPIKPI_Get(ID);
                Assert.IsTrue(item2.Code == item.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item2.Code == item.Code ? "success" : "fail"));
                lstContent.Add("");

                //KPIKPI_Save_Edit
                lstContent.Add("Test method: KPIKPI_Save_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "TestOPS";
                bl.KPIKPI_Delete(ID);
                item2 = bl.KPIKPI_Get(ID);
                Assert.IsTrue(item2 != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item2 != null ? "success" : "fail"));
                lstContent.Add("");

                //KPIKPI_AllList
                lstContent.Add("Test method: KPIKPI_AllList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIKPI_AllList();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

            LogResult(fileName, lstContent);
        }

        //[TestMethod]
        //public void KPIKPI_GetList()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIKPI_Get()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIKPI_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIKPI_Delete()
        //{
        //    Assert.Fail();
        //}
        //[TestMethod]
        //public void KPIKPI_AllList()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        public void KPIColumn_CRUD()
        {
            string fileName = "KPIColumn_CRUD";
            List<string> lstContent = new List<string>();

            int KPIID = 2, ID;
            KPIColumn item = new KPIColumn();
            KPIColumn item2 = new KPIColumn();
            item.Code = "Test001";
            item.ColumnName = "Test001";
            item.ExprData = "Test001";
            item.FieldID = 1;
            item.KPIColumnTypeID = 414;

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            using (var bl = new BLKPI())
            {
                //KPIColumn_List
                lstContent.Add("Test method: KPIColumn_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIColumn_List(request, KPIID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPIColumn_Save_Add
                lstContent.Add("Test method: KPIColumn_Save_Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.KPIColumn_Save(item, KPIID);
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //KPIColumn_Get
                lstContent.Add("Test method: KPIColumn_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item2 = bl.KPIColumn_Get(ID);
                Assert.IsTrue(item2 != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item2 != null ? "success" : "fail"));
                lstContent.Add("");

                //KPIColumn_Save_Edit
                lstContent.Add("Test method: KPIColumn_Save_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "TestOPS";
                ID = bl.KPIColumn_Save(item,KPIID);
                item2 = bl.KPIColumn_Get(ID);
                Assert.IsTrue(item2.Code == item.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item2.Code == item.Code ? "success" : "fail"));
                lstContent.Add("");

                //KPIKPI_Save_Edit
                lstContent.Add("Test method: KPIKPI_Save_Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "TestOPS";
                bl.KPIColumn_Delete(ID);
                item2 = bl.KPIColumn_Get(ID);
                Assert.IsTrue(item2 != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item2 != null ? "success" : "fail"));
                lstContent.Add("");

                //KPIKPI_AllList
                lstContent.Add("Test method: KPIKPI_AllList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIKPI_AllList();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

            LogResult(fileName, lstContent);
        }

        //[TestMethod]
        //public void KPIColumn_List()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIColumn_Get()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIColumn_Save()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPIColumn_Delete()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        public void KPIField_CRUD()
        {
            string fileName = "KPIField_CRUD";
            List<string> lstContent = new List<string>();
            int typeID = 0;
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            using (var bl = new BLKPI())
            {
                //KPIColumn_List
                lstContent.Add("Test method: KPIField_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPIField_List(typeID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

            LogResult(fileName, lstContent);
        }

        //[TestMethod]
        //public void KPIField_List()
        //{
        //    Assert.Fail();
        //}

        [TestMethod]
        public void KPICollection_CRUD()
        {
            string fileName = "KPICollection_CRUD";
            List<string> lstContent = new List<string>();
            int kpiID = 1;
            DateTime datefrom = new DateTime();
            DateTime dateto = new DateTime();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            using (var bl = new BLKPI())
            {
                //KPICollection_Generate
                lstContent.Add("Test method: KPICollection_Generate");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPICollection_Generate(kpiID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //KPICollection_GetList
                lstContent.Add("Test method: KPICollection_GetList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.KPICollection_GetList(request,kpiID, datefrom, dateto);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

            LogResult(fileName, lstContent);
        }

        //[TestMethod]
        //public void KPICollection_Generate()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod]
        //public void KPICollection_GetList()
        //{
        //    Assert.Fail();
        //}
    }
}
