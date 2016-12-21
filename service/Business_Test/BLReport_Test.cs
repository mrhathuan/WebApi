using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTO;
using Newtonsoft.Json;

namespace Business_Test
{
    [TestClass]
    public class BLReport_Test : BaseTest
    {
        #region Common
        [TestMethod]
        public void Customer_List()
        {
            string fileName = "Customer_List";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: Customer_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            using (var bl = new BLReport())
            {
                bl.Account_Setting();
            }

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.Customer_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void GroupOfProduct_List()
        {
            string fileName = "GroupOfProduct_List";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: GroupOfProduct_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.GroupOfProduct_List(lstCusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds>0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds  > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        private DTOCUSGroupOfProduct DTOCUSGroupOfProduct()
        {
            DTOCUSGroupOfProduct item = new DTOCUSGroupOfProduct();
            item.Code = "DTest_CUSGroupOfProduct_Save_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.CustomerName = "DTest_CCUSGroupOfProduct_Save_" + DateTime.Now.ToString("yyyyMMddHHmmss");

            return item;
        }

        [TestMethod]
        public void Vendor_List()
        {
            string fileName = "Vendor_List";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: Vendor_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            using (var bl = new BLReport())
            {
                bl.Account_Setting();
            }

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.Vendor_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void Vendor_Read()
        {
            string fileName = "Vendor_Read";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: Vendor_Read");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            using (var bl = new BLReport())
            {
                bl.Account_Setting();
            }

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.Vendor_Read();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void FLMDriver_List()
        {
            string fileName = "FLMDriver_List";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: FLMDriver_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.FLMDriver_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);

        }
        #endregion

        #region REPDIPivotOrder
        [TestMethod]
        public void REPDIPivotOrder_List()
        {
            string fileName = "REPDIPivotOrder_List";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPivotOrder_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            var lstCusId = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIPivotOrder_List(lstCusId, dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPivotOrder_Read()
        {
            string fileName = "REPDIPivotOrder_Read";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPivotOrder_Read");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            var lstCusId = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIPivotOrder_Read(lstCusId, dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPivotOrder_GetTemplate()
        {
            string fileName = "REPDIPivotOrder_GetTemplate";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPivotOrder_GetTemplate");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var functionId = 124;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIPivotOrder_GetTemplate(functionId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPivotOrder_SaveTemplate()
        {
            string fileName = "REPDIPivotOrder_SaveTemplate";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPivotOrder_SaveTemplate");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds  < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPivotOrder_DeleteTemplate()
        {
            string fileName = "REPDIPivotOrder_DeleteTemplate";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPivotOrder_DeleteTemplate");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region REPDIPivotPOD
        [TestMethod]
        public void REPDIPivotPOD_List()
        {
            string fileName = "REPDIPivotPOD_List";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPivotPOD_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIPivotPOD_List(lstCusId, dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region REPDISchedulePivot
        [TestMethod]
        public void REPDISchedulePivot_Data()
        {
            string fileName = "REPDISchedulePivot_Data";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDISchedulePivot_Data");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDISchedulePivot_Data(lstCusId, dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region REPDIOPSPlan
        [TestMethod]
        public void REPDIOPSPlan_DetailData()
        {
            string fileName = "REPDIOPSPlan_DetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIOPSPlan_DetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var statusId = 1;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIOPSPlan_DetailData(lstCusId, lstGOPId, dtfrom, dtto, statusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIOPSPlan_ColumnDetailData()
        {
            string fileName = "REPDIOPSPlan_ColumnDetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIOPSPlan_ColumnDetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var statusId = 1;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIOPSPlan_ColumnDetailData(lstCusId, lstGOPId, dtfrom, dtto, statusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");
        }

        [TestMethod]
        public void REPDIOPSPlan_ColumnDetailGroupStockData()
        {
            string fileName = "REPDIOPSPlan_ColumnDetailGroupStockData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIOPSPlan_ColumnDetailGroupStockData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var statusId = 1;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIOPSPlan_ColumnDetailGroupStockData(lstCusId, lstGOPId, dtfrom, dtto, statusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIOPSPlan_OrderData()
        {
            string fileName = "REPDIOPSPlan_OrderData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIOPSPlan_OrderData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var statusId = 1;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIOPSPlan_OrderData(lstCusId, lstGOPId, dtfrom, dtto, statusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIOPSPlan_ColumnOrderData()
        {
            string fileName = "REPDIOPSPlan_ColumnOrderData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIOPSPlan_ColumnOrderData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var statusId = 1;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIOPSPlan_ColumnOrderData(lstCusId, lstGOPId, dtfrom, dtto, statusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIOPSPlan_ColumnOrderGroupStockData()
        {
            string fileName = "REPDIOPSPlan_ColumnOrderGroupStockData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIOPSPlan_ColumnOrderGroupStockData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var statusId = 1;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIOPSPlan_ColumnOrderGroupStockData(lstCusId, lstGOPId, dtfrom, dtto, statusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region REPDIPL
        [TestMethod]
        public void REPDIPL_DetailData()
        {
            string fileName = "REPDIPL_DetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPL_DetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var lstStockID = new List<int>();
            var isComplete = true;
            bool? isReceived = null;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport()) 
            {
                //var lst = bl.REPDIPL_DetailData(lstCusId, lstGOPId, lstStockID, dtfrom, dtto, isComplete, isReceived, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPL_ColumnDetailData()
        {
            string fileName = "REPDIPL_ColumnDetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPL_ColumnDetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var lstStockID = new List<int>();
            var isComplete = true;
            bool? isReceived = null;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPDIPL_ColumnDetailData(lstCusId, lstGOPId, lstStockID, dtfrom, dtto, isComplete, isReceived, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPL_ColumnDetailGroupStockData()
        {
            string fileName = "REPDIPL_ColumnDetailGroupStockData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPL_ColumnDetailGroupStockData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var lstStockID = new List<int>();
            var isComplete = true;
            bool? isReceived = null;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPDIPL_ColumnDetailGroupStockData(lstCusId, lstGOPId, lstStockID, dtfrom, dtto, isComplete, isReceived, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPL_ColumnDetailMOQData()
        {
            string fileName = "REPDIPL_ColumnDetailMOQData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPL_ColumnDetailMOQData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var lstStockID = new List<int>();
            var isComplete = true;
            bool? isReceived = null;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPDIPL_ColumnDetailMOQData(lstCusId, lstGOPId, lstStockID, dtfrom, dtto, isComplete, isReceived, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);

            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPL_ColumnDetailGroupProductData()
        {
            string fileName = "REPDIPL_ColumnDetailGroupProductData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPL_ColumnDetailGroupProductData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var lstStockID = new List<int>();
            var isComplete = true;
            bool? isReceived = null;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPDIPL_ColumnDetailGroupProductData(lstCusId, lstGOPId, lstStockID, dtfrom, dtto, isComplete, isReceived, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPL_OrderData()
        {
            string fileName = "REPDIPL_OrderData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPL_OrderData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var lstStockID = new List<int>();
            var isComplete = true;
            bool? isReceived = null;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
               // var lst = bl.REPDIPL_OrderData(lstCusId, lstGOPId, lstStockID, dtfrom, dtto, isComplete, isReceived, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPL_ColumnOrderData()
        {
            string fileName = "REPDIPL_ColumnOrderData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPL_ColumnOrderData");
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var lstStockID = new List<int>();
            var isComplete = true;
            bool? isReceived = null;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPDIPL_ColumnOrderData(lstCusId, lstGOPId, lstStockID, dtfrom, dtto, isComplete, isReceived, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPL_ColumnOrderGroupStockData()
        {
            string fileName = "REPDIPL_ColumnOrderGroupStockData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPL_ColumnOrderGroupStockData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var lstStockID = new List<int>();
            var isComplete = true;
            bool? isReceived = null;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPDIPL_ColumnOrderGroupStockData(lstCusId, lstGOPId, lstStockID, dtfrom, dtto, isComplete, isReceived, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPL_ColumnOrderMOQData()
        {
            string fileName = "REPDIPL_ColumnOrderMOQData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPL_ColumnOrderMOQData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var lstStockID = new List<int>();
            var isComplete = true;
            bool? isReceived = null;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPDIPL_ColumnOrderMOQData(lstCusId, lstGOPId, lstStockID, dtfrom, dtto, isComplete, isReceived, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPL_ColumnOrderGroupProductData()
        {
            string fileName = "REPDIPL_ColumnOrderGroupProductData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPL_ColumnOrderGroupProductData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var lstStockID = new List<int>();
            var isComplete = true;
            bool? isReceived = null;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPDIPL_ColumnOrderGroupProductData(lstCusId, lstGOPId, lstStockID, dtfrom, dtto, isComplete, isComplete, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region REPDIPOD

        [TestMethod]
        public void REPDIPOD_DetailData()
        {
            string fileName = "REPDIPOD_DetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPOD_DetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var statusId = 1;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIPOD_DetailData(lstCusId, lstGOPId, dtfrom, dtto, statusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPDIPOD_ColumnDetailData()
        {
            string fileName = "REPDIPOD_ColumnDetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDIPOD_ColumnDetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var lstGOPId = new List<int>() { 1, 2, 3, 11, 43, 33 };
            var statusId = 1;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDIPOD_ColumnDetailData(lstCusId, lstGOPId, dtfrom, dtto, statusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region REPCOOPSPlan
        [TestMethod]
        public void REPCOOPSPlan_DetailData()
        {
            string fileName = "REPCOOPSPlan_DetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPCOOPSPlan_DetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var statusId = 1;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPCOOPSPlan_DetailData(lstCusId, dtfrom, dtto, statusId, null);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPCOOPSPlan_ColumnDetailData()
        {
            string fileName = "REPCOOPSPlan_ColumnDetailDataREPCOOPSPlan_ColumnDetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPCOOPSPlan_ColumnDetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var statusId = 1;
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPCOOPSPlan_ColumnDetailData(lstCusId, dtfrom, dtto, statusId, null);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region REPCOPL
        [TestMethod]
        public void REPCOPL_DetailData()
        {
            string fileName = "REPCOPL_DetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPCOPL_DetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var isComplete = true;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPCOPL_DetailData(lstCusId, dtfrom, dtto, isComplete, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPCOPL_ColumnDetailData()
        {
            string fileName = "REPCOPL_ColumnDetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPCOPL_ColumnDetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var isComplete = true;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPCOPL_ColumnDetailData(lstCusId, dtfrom, dtto, isComplete, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPCOPL_OrderData()
        {
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var isComplete = true;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPCOPL_OrderData(lstCusId, dtfrom, dtto, isComplete, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void REPCOPL_ColumnOrderData()
        {
            string fileName = "REPCOPL_ColumnOrderData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPCOPL_ColumnOrderData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var isComplete = true;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPCOPL_ColumnOrderData(lstCusId, dtfrom, dtto, isComplete, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region MAP
        [TestMethod]
        public void CartoDB_List()
        {
            string fileName = "CartoDB_List";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CartoDB_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstCusId = new List<int>() { 4, 5, 17, 18, 38 };
            var provinceID = 1;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.CartoDB_List(request, lstCusId, dtfrom, dtto, provinceID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CartoDB_Vehicle_List()
        {
            string fileName = "CartoDB_Vehicle_List";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CartoDB_Vehicle_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.CartoDB_Vehicle_List(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region Đội xe
        [TestMethod]
        public void REPOwner_DriverFee()
        {
            string fileName = "REPOwner_DriverFee";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_DriverFee");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            var scheduleID = 14;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_DriverFee(scheduleID, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_DriverFee_ColumnDetailData()
        {
            string fileName = "REPOwner_DriverFee_ColumnDetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_DriverFee_ColumnDetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var scheduleID = 14;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_DriverFee_ColumnDetailData(scheduleID, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0  ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_DriverSalary()
        {
            string fileName = "REPOwner_DriverSalary";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_DriverSalary");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstDriverId = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            var scheduleID = 14;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_DriverSalary(scheduleID, lstDriverId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_VehicleFee()
        {
            string fileName = "REPOwner_VehicleFee";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_VehicleFee");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPOwner_VehicleFee(dtfrom, dtto, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_VehicleFee_ColumnDetailData()
        {
            string fileName = "REPOwner_VehicleFee_ColumnDetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_VehicleFee_ColumnDetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                //var lst = bl.REPOwner_VehicleFee_ColumnDetailData(dtfrom, dtto, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_VehicleFee_Cost()
        {
            string fileName = "REPOwner_VehicleFee_Cost";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_VehicleFee_Cost");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_VehicleFee_Cost(dtfrom, dtto, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_VehicleFee_Cost_ColumnDetailData()
        {
            string fileName = "REPOwner_VehicleFee_Cost_ColumnDetailData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_VehicleFee_Cost_ColumnDetailData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_VehicleFee_Cost_ColumnDetailData(dtfrom, dtto, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_TotalSchedule()
        {
            string fileName = "REPOwner_TotalSchedule";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_TotalSchedule");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstVenId = new List<int?>() { 6, 7, 8 };
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_TotalSchedule(dtfrom, dtto, lstVenId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);

            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_Schedule()
        {
            string fileName = "REPOwner_Schedule";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_Schedule");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_Schedule();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + ( sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPFLMDriverRole_PlanData()
        {
            string fileName = "REPFLMDriverRole_PlanData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPFLMDriverRole_PlanData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPFLMDriverRole_PlanData(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPFLMDriverRole_ActualData()
        {
            string fileName = "REPFLMDriverRole_ActualData";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPFLMDriverRole_ActualData");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPFLMDriverRole_ActualData(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);

            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_Receipt()
        {
            string fileName = "REPOwner_Receipt";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_Receipt");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_Receipt(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_Receipt_Detail()
        {
            string fileName = "REPOwner_Receipt_Detail";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_Receipt_Detail");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_Receipt_Detail(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_Repair()
        {
            string fileName = "REPOwner_Repair";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_Repair");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_Repair(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_Equipment()
        {
            string fileName = "REPOwner_Equipment";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_Equipment");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_Equipment(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_DriverRole()
        {
            string fileName = "REPOwner_DriverRole";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_DriverRole");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_DriverRole(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_Maintenance_Detail()
        {
            string fileName = "REPOwner_Maintenance_Detail";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_Maintenance_Detail");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_Maintenance_Detail(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region CUSSettingsReport
        [TestMethod]
        public void CUSSettingsReport_List()
        {
            string fileName = "CUSSettingsReport_List";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CUSSettingsReport_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var referId = 28;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.CUSSettingsReport_List(referId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingsReport_Save()
        {
            string fileName = "CUSSettingsReport_Save";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CUSSettingsReport_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingsReport_Delete()
        {
            string fileName = "CUSSettingsReport_Delete";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CUSSettingsReport_Delete");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingReport_CustomerNotInList()
        {
            string fileName = "CUSSettingReport_CustomerNotInList";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CUSSettingReport_CustomerNotInList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingReport_CustomerNotInSave()
        {
            string fileName = "CUSSettingReport_CustomerNotInSave";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CUSSettingReport_CustomerNotInSave");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingReport_CustomerDeleteList()
        {
            string fileName = "CUSSettingReport_CustomerDeleteList";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CUSSettingReport_CustomerDeleteList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingReport_GroupOfProductNotInList()
        {
            string fileName = "CUSSettingReport_GroupOfProductNotInList";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CUSSettingReport_GroupOfProductNotInList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingReport_GroupOfProductNotInSave()
        {
            string fileName = "CUSSettingReport_GroupOfProductNotInSave";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CUSSettingReport_GroupOfProductNotInSave");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingReport_GroupOfProductDeleteList()
        {
            string fileName = "CUSSettingReport_GroupOfProductDeleteList";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CUSSettingReport_GroupOfProductDeleteList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region telerik reporting
        [TestMethod]
        public void REPDriverScheduleFee_Data()
        {
            string fileName = "REPDriverScheduleFee_Data";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPDriverScheduleFee_Data");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstDriverId = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            var month = 5;
            var year = 2016;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPDriverScheduleFee_Data(lstDriverId, month, year);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPWorkOrderFuel_Data()
        {
            string fileName = "REPWorkOrderFuel_Data";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPWorkOrderFuel_Data");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var lstReceiptID = new List<int>() { 1113, 1114, 1115, 1116, 1117, 1118 };

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPWorkOrderFuel_Data(lstReceiptID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);

            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region Báo cáo khấu hao
        [TestMethod]
        public void REPOwner_FixedCost_Detail()
        {
            string fileName = "REPOwner_FixedCost_Detail";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_FixedCost_Detail");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dt = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_FixedCost_Detail(dt);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPOwner_FixedCost_Vehicle()
        {
            string fileName = "REPOwner_FixedCost_Vehicle";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_FixedCost_Vehicle");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dt = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_FixedCost_Vehicle(dt);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region Báo cáo GPS
        [TestMethod]
        public void REPOwner_GPS_Detail()
        {
            string fileName = "REPOwner_GPS_Detail";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPOwner_GPS_Detail");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPOwner_GPS_Detail(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);

        }
        #endregion

        #region REPTotalPriceVendor
        [TestMethod]
        public void REPTotalPriceVendor_Data()
        {
            string fileName = "REPTotalPriceVendor_Data";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPTotalPriceVendor_Data");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            var lstVenId = new List<int>() { 6, 7, 65 };
            var cusId = 5;
            var transportModeID = 34;
            var typePrice = 0;
            var dtEffect = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPTotalPriceVendor_Data(cusId, lstVenId, transportModeID, typePrice, dtEffect);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void REPTotalPriceVendor_ListVendor()
        {
            string fileName = "REPTotalPriceVendor_ListVendor";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPTotalPriceVendor_ListVendor");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var cusId = 5;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPTotalPriceVendor_ListVendor(cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region Báo cáo tổng hợp
        [TestMethod]
        public void REPTotalPL_List()
        {
            string fileName = "REPTotalPL_List";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPTotalPL_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var typeOfView = 1;
            using (var bl = new BLReport())
            {
                bl.Account_Setting();
            }

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPTotalPL_List(dtfrom, dtto, typeOfView);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds < 1 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion

        #region Báo cáo tờ khai (REPCOInspection)
        [TestMethod]
        public void REPCOInspection_Detail()
        {
            string fileName = "REPCOInspection_Detail";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: REPCOInspection_Detail");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var lstCusId = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLReport())
            {
                var lst = bl.REPCOInspection_Detail(lstCusId, dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (sub.Seconds > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }
        #endregion
    }
}
