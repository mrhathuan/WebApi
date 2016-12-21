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
    public class BLFleetManage_Test : BaseTest
    {
        #region FLMAsset
        [TestMethod]
        public void FLMAsset_Location_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMAsset_Location_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMAsset_Location_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                // var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMAsset_Location_Get()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                //var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMAsset_Location_Delete()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                //var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMAsset_History_DepreciationList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int assetID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMAsset_History_DepreciationList(request, assetID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMAsset_History_OPSList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int assetID = 1;
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;
            bool isDI = true;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMAsset_History_OPSList(request, assetID, From, To, isDI);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMAsset_History_RepairList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int assetID = 1;
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMAsset_History_RepairList(request, assetID, From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Truck
        [TestMethod]
        public void Truck_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Truck_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Truck_Get()
        {

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Truck_Save()
        {

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Truck_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                //var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Truck_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                // var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Truck_Export()
        {
            //DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            //string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                //var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Tractor
        [TestMethod]
        public void Tractor_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Tractor_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Tractor_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                //  var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Tractor_Save()
        {
            //   DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            //   string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                // var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Tractor_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Romooc
        [TestMethod]
        public void RomoocAll_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.RomoocAll_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Vendor_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Vendor_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Romooc_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Romooc_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Romooc_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                //var lst = bl.Romooc_Get();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Romooc_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                //  var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Romooc_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                // var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Container (Vỏ con)

        [TestMethod]
        public void Container_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Container_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Container_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Container_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Container_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Equipment
        [TestMethod]
        public void Equipment_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Equipment_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Equipment_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Equipment_GetLocation()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Equipment_GetLocation();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Equipment_ListByVehicleID()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int vehicleID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Equipment_ListByVehicleID(request, vehicleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Equipment_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void EquipmentHistory_ListByID()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                //var lst = bl.EquipmentHistory_ListByID(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region FLM_ScheduleFeeDefault (phụ phí tháng trong chi tiết asset)

        [TestMethod]
        public void FLMScheduleFeeDefault_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int assetID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMScheduleFeeDefault_List(request, assetID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMScheduleFeeDefault_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                // var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMScheduleFeeDefault_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                //var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMScheduleFeeDefault_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                // var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion


        #region Operation
        [TestMethod]
        public void Operation_Vehicle()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Operation_Driver()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Operation_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Operation_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                // var lst = bl.LocationInSeaport_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Supplier
        [TestMethod]
        public void MaterialBySupplierID_List()
        {
            int suppID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.MaterialBySupplierID_List(suppID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void MaterialAll_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.MaterialAll_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Material in Vehicle (MaterialQuota)
        [TestMethod]
        public void MaterialQuota_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int assetID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.MaterialQuota_List(request, assetID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void MaterialQuota_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void MaterialQuota_Save()
        {

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void MaterialQuota_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void MaterialByVehicleID_List()
        {
            int assetID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.MaterialByVehicleID_List(assetID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Valriable Cost (Tạo mới) 
        [TestMethod]
        public void VehicleAutoComplete_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.VehicleAutoComplete_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Maintenance
        [TestMethod]
        public void FLMMaintenance_Data()
        {
            List<int> lstAsset = new List<int> { 1, 2, 3 };
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMMaintenance_Data(lstAsset, From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMMaintenance_VehicleTimeGet()
        {
            int timeID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMMaintenance_VehicleTimeGet(timeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMMaintenance_VehicleTimeSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMMaintenance_VehicleTimeDelete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMMaintenance_AssetList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMMaintenance_AssetList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMMaintenance_LocationList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMMaintenance_LocationList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region FLMTransferReceipt
        [TestMethod]
        public void FLMTransferReceipt_StockList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMTransferReceipt_StockList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_EQMByStock()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int stockID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMTransferReceipt_EQMByStock(request, stockID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_EQMGet()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_EQMData()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMTransferReceipt_EQMData();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_EQMSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_EQMImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMTransferReceipt_List(request, From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_GetEQMHistory()
        {
            int assetID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMTransferReceipt_GetEQMHistory(assetID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_GetEQMList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMTransferReceipt_GetEQMList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_GetEQMLocation()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMTransferReceipt_GetEQMLocation();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTransferReceipt_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion


        #region Thanh lý Xe/Thiết bị
        [TestMethod]
        public void FLMDisposal_Vehicle_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDisposal_Vehicle_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDisposal_EQMByVehicle_List()
        {
            int vehicleID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDisposal_EQMByVehicle_List(vehicleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDisposal_EQM_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDisposal_EQM_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDisposal_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDisposal_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDisposal_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDisposal_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Lịch sử cấp phát vật tư

        [TestMethod]
        public void FuelHistory_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int assetID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FuelHistory_List(request, assetID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region FLMDriver

        [TestMethod]
        public void FLMDriver_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriver_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_TransportHistory_Read()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;
            int driverID = 1;
            int typeTrans = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriver_TransportHistory_Read(request, From, To, driverID, typeTrans);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverExport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriverExport();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_Data()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriver_Data();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Truck_GetByDriver()
        {
            int driverID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Truck_GetByDriver(driverID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_DrivingLicence_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriver_DrivingLicence_List(request, driverID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_DrivingLicence_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_DrivingLicence_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_DrivingLicence_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_Schedule_Data()
        {
            int month = 1;
            int year = 2016;
            int driverID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriver_Schedule_Data(month, year, driverID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Common
        [TestMethod]
        public void FLMDriver_FLMScheduleFeeDefault_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriver_FLMScheduleFeeDefault_List(request, driverID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_FLMScheduleFeeDefault_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_FLMScheduleFeeDefault_Save()
        {

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_FLMScheduleFeeDefault_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_FLMDriverRole_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriver_FLMDriverRole_List(request, driverID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_FLMDriverRole_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_FLMDriverRole_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriver_FLMDriverRole_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMAsset_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMAsset_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void AllSupplier_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.AllSupplier_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Stock_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Stock_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMVehicle_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMVehicle_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }


        [TestMethod]
        public void FLMReceiptAll_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceiptAll_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverAll_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriverAll_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMRomooc_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMRomooc_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }


        [TestMethod]
        public void CATDrivingLicence_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.CATDrivingLicence_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATDepartment_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.CATDepartment_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMMaterial_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMMaterial_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfRomooc_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.CATGroupOfRomooc_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATRomooc_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.CATRomooc_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfEquipment_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.CATGroupOfEquipment_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Schedule - Chấm công tài xế
        //danh sach bang châm
        [TestMethod]
        public void FLMSchedule_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        //chi tiết 1 bảng
        [TestMethod]
        public void FLMSchedule_Detail_Data()
        {
            int scheduleID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_Detail_Data(scheduleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Detail_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Detail_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        //tai xe cua bảng 
        [TestMethod]
        public void FLMSchedule_Driver_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int scheduleID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_Driver_List(request, scheduleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Driver_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Driver_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Driver_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Driver_NotInList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int scheduleID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_Driver_NotInList(request, scheduleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Driver_NotInSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Driver_UpdateInfo()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }


        //loai ngay trong bang cham
        [TestMethod]
        public void FLMSchedule_Date_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int scheduleID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_Date_List(request, scheduleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Date_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_Date_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        //phi khác theo tài xe
        [TestMethod]
        public void FLMSchedule_DriverFee_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int scheduleID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_DriverFee_List(request, scheduleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_DriverFee_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                //var lst = bl.FLMSchedule_DriverFee_Get(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_DriverFee_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_DriverFee_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_DriverFee_DriverList()
        {
            int scheduleID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_DriverFee_DriverList(scheduleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        //phi thang theo xe
        [TestMethod]
        public void FLMSchedule_AssetFee_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int scheduleID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_AssetFee_List(request, scheduleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_AssetFee_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_AssetFee_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_AssetFee_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_AssetFee_AsestList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_AssetFee_AsestList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_ScheduleFee_Data()
        {
            int scheduleID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_ScheduleFee_Data(scheduleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_DriverFee_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_AssetFee_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        //chi phí theo lái/ phụ xe
        [TestMethod]
        public void FLMSchedule_AssistantFee_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int scheduleID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMSchedule_AssistantFee_List(request, scheduleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_AssistantFee_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_AssistantFee_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMSchedule_AssistantFee_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        //tinh luong
        [TestMethod]
        public void FLM_Schedule_Detail_CalculateFee()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLM_Schedule_Detail_RefreshFee()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Bảng lương tài xế
        [TestMethod]
        public void FLMDriverFee_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverFee_Export()
        {
            int contractID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriverFee_Export(contractID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverFee_Import_Data()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Excel
        [TestMethod]
        public void Tractor_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Tractor_Export()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Tractor_Export();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATVehicle_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.CATVehicle_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfVehicle_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.CATGroupOfVehicle_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void VehicleOwn_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.VehicleOwn_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Romooc_Export()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Romooc_Export();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Romooc_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Eqm_Export()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.Eqm_Export();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Eqm_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void EquipmentOwn_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.EquipmentOwn_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region FLMDriverTimeSheet

        [TestMethod]
        public void FLMDriverTimeSheet_VehicleList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriverTimeSheet_VehicleList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverTimeSheet_VehicleTimeList()
        {
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriverTimeSheet_VehicleTimeList(From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverTimeSheet_VehicleTimeGet()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverTimeSheet_DriverSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverTimeSheet_DriverDelete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverTimeSheet_DriverList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMDriverTimeSheet_DriverList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMDriverTimeSheet_ChangeType()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region work order(danh sách các phiều)
        [TestMethod]
        public void FLMReceipt_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_List(request, From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_ApprovedList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_UnApprovedList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        #region Receipt Fuel
        
        [TestMethod]
        public void FLMReceipt_FuelGet()
        {
            int receiptID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_FuelGet(receiptID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_FuelSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Receipt Tranfer
        [TestMethod]
        public void FLMReceipt_TranfersGet()
        {
            int receiptID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_TranfersGet(receiptID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_TranfersEQMByStock()
        {
            int stockID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_TranfersEQMByStock(stockID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_TranfersEQMByVehicle()
        {
            int vehicleID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_TranfersEQMByVehicle(vehicleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_TranfersSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Receipt Disposal
        [TestMethod]
        public void FLMReceipt_DisposalGet()
        {
            int receiptID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_DisposalGet(receiptID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_DisposalSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_DisposalEQMByVehicle()
        {
            int vehicleID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_DisposalEQMByVehicle(vehicleID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_DisposalEQMList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_DisposalEQMList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_DisposalVehicleList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_DisposalVehicleList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Receipt Repair
        [TestMethod]
        public void FLMReceipt_RepairLargeGet()
        {
            int receiptID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_RepairLargeGet(receiptID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_RepairLargeSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_RepairLargeListFixCost()
        {
            int receiptID = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_RepairLargeListFixCost(receiptID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_RepairLargeGenerateFixCost()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_RepairLargeSaveFixCost()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_RepairLargeDeleteFixCost()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_RepairChangeToLarge()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_RepairChangeToSmall()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #region Receipt import equipment

        [TestMethod]
        public void FLMReceipt_ImportEQM_Get()
        {
            int receiptID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_ImportEQM_Get(receiptID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_ImportEQM_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_ImportEQM_StockList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_ImportEQM_StockList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMReceipt_ImportEQM_VehicleList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMReceipt_ImportEQM_VehicleList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #endregion

        #region FLMSetting
        [TestMethod]
        public void FLMContract_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int contractID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_List(request, contractID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_Get(-1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Group Location
        [TestMethod]
        public void FLMContract_DriverFee_GroupLocation_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int driverFeeID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_GroupLocation_List(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_GroupLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_GroupLocation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_GroupLocation_GroupNotInList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_GroupLocation_GroupNotInList(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region  Location
        [TestMethod]
        public void FLMContract_DriverFee_Location_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = -1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_Location_List(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Location_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Location_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Location_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Location_LocationNotInSaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Location_LocationNotInList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_Location_LocationNotInList(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region  ruote
        [TestMethod]
        public void FLMContract_DriverFee_Route_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_Route_List(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Route_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Route_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Route_RouteNotInList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_Route_RouteNotInList(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region  parent route
        [TestMethod]
        public void FLMContract_DriverFee_ParentRoute_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_ParentRoute_List(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_ParentRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_ParentRoute_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_ParentRoute_RouteNotInList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_ParentRoute_RouteNotInList(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Customer
        [TestMethod]
        public void FLMContract_DriverFee_Customer_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_Customer_List(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Customer_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Customer_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }

            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_Customer_GroupNotInList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_Customer_GroupNotInList(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

#endregion
        
        #region price  GroupProduct
        [TestMethod]
        public void FLMContract_DriverFee_GroupProduct_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_GroupProduct_List(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_GroupProduct_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_GroupProduct_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_GroupProduct_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_GroupProduct_NotInList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int driverFeeID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMContract_DriverFee_GroupProduct_NotInList(request, driverFeeID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMContract_DriverFee_GroupProduct_NotInSaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion


        #region khấu hao mới(theo xe)
        [TestMethod]
        public void FLMFixedCost_ByAssetList()
        {
            int assetID = -1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMFixedCost_ByAssetList(assetID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMFixedCost_Generate()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMFixedCost_Save()
        {

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMFixedCost_ByAssetDelete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMFixedCost_ByAsset_CheckExpr()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region tính khấu hao
        [TestMethod]
        public void FLMFixedCost_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int month = 1;
            int year = 2016;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMFixedCost_List(request, month, year);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMFixedCost_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMFixedCost_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMFixedCost_ReceiptList()
        {
            int assetID = -1;
            int month = 1;
            int year = 2016;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMFixedCost_ReceiptList(assetID, month, year);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region FLM_VehiclePlan

        [TestMethod]
        public void FLMVehiclePlan_Data()
        {
            List<int> lstAssetID = new List<int> { 1, 2, 3 };
            DateTime From = DateTime.Now;
            DateTime To = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMVehiclePlan_Data(lstAssetID, From, To);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMVehiclePlan_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMVehiclePlan_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMVehiclePlan_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMVehiclePlan_VehicleList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMVehiclePlan_VehicleList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMVehiclePlan_DriverList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMVehiclePlan_DriverList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        #endregion

        #region FLMTypeDriverCost (Loại phí tài xế)
        [TestMethod]
        public void FLMTypeDriverCost_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {
                var lst = bl.FLMTypeDriverCost_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTypeDriverCost_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTypeDriverCost_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTypeDriverCost_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFleetManage())
            {

            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
    }
}
