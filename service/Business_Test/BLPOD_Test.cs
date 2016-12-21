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
    public class BLPOD_Test : BaseTest
    {
        [TestMethod]
        public void PODCOInput_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;
            List<int> lstCus = new List<int> { 1, 2, 3 };

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODCOInput_List(request, From, To, lstCus);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODCOInput_Save()
        {

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDIInput_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;
            List<int> lstCus = new List<int> { 1, 2, 3 };

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODDIInput_List(request, From, To, lstCus,lstCus);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDIInput_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODBarcodeGroup_List()
        {
            string barcode = string.Empty;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODBarcodeGroup_List(barcode);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODBarcodeGroup_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDistributionDN_ExcelExport()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODDistributionDN_ExcelExport(request, From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDistributionDN_GetDataCheck()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODDistributionDN_GetDataCheck();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDistributionDN_ExcelSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDI_DN_ReportExcel()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODDI_DN_ReportExcel();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDIInput_MobiList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                //var lst = bl.PODDIInput_MobiList(From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODMONTime_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODMONTime_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODMONTime_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_List(request, From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_Data()
        {
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_Data(From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_DetailList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int ExtReturnID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_DetailList(request, ExtReturnID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_Approved()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_DetailSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_CustomerList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_CustomerList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_GOPByCus()
        {
            int customerID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_GOPByCus(customerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_ProductByGOP()
        {
            int gopID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_ProductByGOP(gopID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_VehicleList()
        {
            int vendorID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_VehicleList(vendorID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_DriverList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_DriverList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_VendorList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_VendorList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_DetailNotIn()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int masterID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_DetailNotIn(request, masterID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_DITOMasterList()
        {
            int Cus = -1;
            int ven = -1;
            int vehicleId = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_DITOMasterList(Cus, ven, vehicleId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_FindList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_FindList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_QuickList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_QuickList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_QuickSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODOPSExtReturn_QuickData()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODOPSExtReturn_QuickData();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDIInput_Quick_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;
            List<int> lstCus = new List<int> { 1, 2, 3 };

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODDIInput_Quick_List(request, From, To, lstCus);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDIInput_Quick_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDIInput_Quick_DNGet()
        {
            int DITOGroupProductID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODDIInput_Quick_DNGet(DITOGroupProductID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODDIInput_Quick_DNSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMDIInput_List(request, From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_Export()
        {
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMDIInput_Export(From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_Approved()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_GetDrivers()
        {
            int DITOMasterID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMDIInput_GetDrivers(DITOMasterID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_SaveDrivers()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_TroubleCostList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int DITOMasterID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMDIInput_TroubleCostList(request, DITOMasterID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_TroubleCostNotIn_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int DITOMasterID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMDIInput_TroubleCostNotIn_List(request, DITOMasterID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_TroubleCostNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_TroubleCost_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_TroubleCostSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_StationCostList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int DITOMasterID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMDIInput_StationCostList(request, DITOMasterID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_StationCostSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODGroupOfTrouble_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODGroupOfTrouble_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMDIInput_DriverList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMDIInput_DriverList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMCOInput_List(request, From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_Export()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMCOInput_Export(From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_Approved()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_GetDrivers()
        {
            int COTOMasterID = -1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMCOInput_GetDrivers(COTOMasterID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_SaveDrivers()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_TroubleCostList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int COTOMasterID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMCOInput_TroubleCostList(request, COTOMasterID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_TroubleCostNotIn_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int COTOMasterID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMCOInput_TroubleCostNotIn_List(request, COTOMasterID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_TroubleCostNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_TroubleCost_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_TroubleCostSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_StationCostList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int COTOMasterID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMCOInput_StationCostList(request, COTOMasterID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_StationCostSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PODFLMCOInput_DriverList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLPOD())
            {
                var lst = bl.PODFLMCOInput_DriverList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
    }
}
