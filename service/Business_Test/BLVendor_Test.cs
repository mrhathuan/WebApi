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
    public class BLVendor_Test : BaseTest
    {
        public int customerId = 6;//VenABC
        #region Vendor
        [TestMethod]
        public void Vendor_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            using (var bl = new BLVendor())
            {
                bl.Account_Setting();
            }

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.Vendor_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Vendor_Get()
        {
            var id = 4; //Công ty bia
            DTOVendor item = new DTOVendor();
            using (var bl = new BLVendor())
            {
                item = bl.Vendor_Get(id);
            }
            Assert.IsTrue(item.ID > 0);
        }
        //constructor
        private DTOVendor VendorConstructor()
        {
            DTOVendor item = new DTOVendor();
            item.Code = "DTest_Vendor_Save_";
            item.CustomerName = "DTest_Vendor_Save_";
            item.ShortName = "DTest";

            return item;
        }

        [TestMethod]
        public void Vendor_Save()
        {
            DTOVendor item = VendorConstructor();
            using (var bl = new BLVendor())
            {
                item.ID = bl.Vendor_Save(item);
                bl.Vendor_Delete(item);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void Vendor_Delete()
        {
            DTOVendor item = VendorConstructor();
            int id = -1;

            using (var bl = new BLVendor())
            {
                item.ID = bl.Vendor_Save(item);
                bl.Vendor_Delete(item);
                id = item.ID;
                item = null;

                item = bl.Vendor_Get(id);
            }
            Assert.IsTrue((item == null && item.ID <= 0) && id > 0);
        }
        #endregion

        #region Company
        [TestMethod]
        public void Company_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 4; //Công ty bia

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.Company_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CompanyNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6; //Công ty bia

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.CompanyNotIn_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CompanyNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Company_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Truck
        [TestMethod]
        public void CATTruck_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.CATTruck_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Truck_Get()
        {
            DTOCUSVehicle item = new DTOCUSVehicle();
            var id = 2;

            using (var bl = new BLVendor())
            {
                item = bl.Truck_Get(id);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void Truck_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.Truck_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        private DTOCUSVehicle TruckConstructor()
        {
            DTOCUSVehicle item = new DTOCUSVehicle();
            item.RegNo = "Test-1111";
            return item;
        }
        [TestMethod]
        public void Truck_Save()
        {
            string fileName = "Truck_Save";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Truck_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOCUSVehicle item = TruckConstructor();
            int venID = 6;

            using (var bl = new BLVendor())
            {
                item.ID = bl.Truck_Save(item, venID);
                bl.Truck_Delete(item);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");
            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void Truck_Delete()
        {
            string fileName = "Truck_Delete";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Truck_Delete");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOCUSVehicle item = TruckConstructor();
            int venID = 6;
            var id = -1;

            using (var bl = new BLVendor())
            {
                item.ID = bl.Truck_Save(item, venID);
                bl.Truck_Delete(item);
                id = item.ID;
                item = null;

                item = bl.Truck_Get(id);
            }
            Assert.IsTrue((item == null || item.ID <= 0) && id > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (((item == null || item.ID <= 0) && id > 0) ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void Truck_NotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.Truck_NotInList(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Truck_NotInSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Truck_Export()
        {
            var id = 6;
            var type = "tractor";

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.Truck_Export(id, type);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Truck_Check()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Truck_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Tractor
        [TestMethod]
        public void CATTractor_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.CATTractor_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Tractor_Get()
        {
            var id = 2;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.Tractor_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Tractor_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.Tractor_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Tractor_Save()
        {
            DTOCUSVehicle item = new DTOCUSVehicle();
            int venID = 6;
            using (var bl = new BLVendor())
            {
                item.ID = bl.Tractor_Save(item, venID);
                bl.Tractor_Delete(item);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void Tractor_Delete()
        {
            DTOCUSVehicle item = new DTOCUSVehicle();
            int venID = 6;
            int id = -1;

            using (var bl = new BLVendor())
            {
                item.ID = bl.Tractor_Save(item, venID);
                bl.Tractor_Delete(item);
                id = item.ID;
                item = null;

                item = bl.Tractor_Get(id);
            }
            Assert.IsTrue((item == null || item.ID <= 0) && id > 0);
        }

        [TestMethod]
        public void Tractor_NotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.Tractor_NotInList(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Tractor_NotInSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Romooc
        [TestMethod]
        public void CATRomooc_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.CATRomooc_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Romooc_Get()
        {
            DTOCUSRomooc item = new DTOCUSRomooc();
            var id = 2;

            using (var bl = new BLVendor())
            {
                item = bl.Romooc_Get(id);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void Romooc_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.Romooc_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        private DTOCUSRomooc RomoocConstructor()
        {
            DTOCUSRomooc item = new DTOCUSRomooc();
            item.RegNo = "Test-1111";
            return item;
        }
        [TestMethod]
        public void Romooc_Save()
        {
            string fileName = "Romooc_Save";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Romooc_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOCUSRomooc item = RomoocConstructor();
            int venID = 6;

            using (var bl = new BLVendor())
            {
                item.ID = bl.Romooc_Save(item, venID);
                bl.Romooc_Delete(item);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void Romooc_Delete()
        {
            string fileName = "Romooc_Delete";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Romooc_Delete");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOCUSRomooc item = RomoocConstructor();
            int venID = 6;
            var id = -1;

            using (var bl = new BLVendor())
            {
                item.ID = bl.Romooc_Save(item, venID);
                bl.Romooc_Delete(item);
                id = item.ID;
                item = null;

                item = bl.Romooc_Get(id);
            }
            Assert.IsTrue((item == null || item.ID <= 0) && id > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (((item == null || item.ID <= 0) && id > 0) ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void Romooc_NotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.Romooc_NotInList(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Romooc_NotInSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Routing
        [TestMethod]
        public void Routing_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.Routing_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Routing_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void RoutingCusNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.RoutingCusNotIn_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void RoutingNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.RoutingNotIn_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void RoutingNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Routing_Update()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Driver
        [TestMethod]
        public void VENDriver_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENDriver_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENDriver_Get()
        {
            DTOVENDriver item = new DTOVENDriver();
            var id = 6;

            using (var bl = new BLVendor())
            {
                item = bl.VENDriver_Get(id);
            }
            Assert.IsTrue(item.ID > 0);
        }

        private DTOVENDriver DriverConstructor()
        {
            DTOVENDriver item = new DTOVENDriver();
            item.CardNumber = "99999999";
            item.FirstName = "FirstName";
            item.LastName = "LastName";
            return item;
        }
        [TestMethod]
        public void VENDriver_Save()
        {
            DTOVENDriver item = DriverConstructor();
            int venID = 6;

            using (var bl = new BLVendor())
            {
                item.ID = bl.VENDriver_Save(item, venID);
                bl.VENDriver_Delete(item);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void VENDriver_Delete()
        {
            DTOVENDriver item = DriverConstructor();
            int venID = 6;
            var id = -1;

            using (var bl = new BLVendor())
            {
                item.ID = bl.VENDriver_Save(item, venID);
                bl.VENDriver_Delete(item);
                id = item.ID;
                item = null;

                item = bl.VENDriver_Get(id);
            }
            Assert.IsTrue((item == null || item.ID <= 0) && id > 0);
        }

        [TestMethod]
        public void VENDriver_Data()
        {
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENDriver_Data(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENDriver_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENDriver_NotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENDriver_NotInList(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENDriver_NotInSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENDriver_ExportByVendor()
        {
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENDriver_ExportByVendor(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENDriver_DrivingLicence_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENDriver_DrivingLicence_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENDriver_DrivingLicence_Get()
        {
            DTOCATDriverLicence item = new DTOCATDriverLicence();
            var id = 6;

            using (var bl = new BLVendor())
            {
                item = bl.VENDriver_DrivingLicence_Get(id);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void VENDriver_DrivingLicence_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENDriver_DrivingLicence_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region GroupOfProduct
        [TestMethod]
        public void GroupOfProductAll_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.GroupOfProductAll_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void GroupOfProduct_List()
        {
            string request = string.Empty;
            var venId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.GroupOfProduct_List(request, venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        //constructor
        private DTOCUSGroupOfProduct GOPConstructor()
        {
            //constructor
            DTOCUSGroupOfProduct item = new DTOCUSGroupOfProduct();
            item.Code = "Code_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.GroupName = "GroupName_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.Level = 1;
            item.IsDefault = false;

            return item;
        }

        [TestMethod]
        public void GroupOfProduct_Save()
        {
            DTOCUSGroupOfProduct item = GOPConstructor();
            using (var bl = new BLVendor())
            {
                item.ID = bl.GroupOfProduct_Save(item, customerId);
                bl.GroupOfProduct_Delete(item);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void GroupOfProduct_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void GroupOfProduct_GetByCode()
        {
            var code = "Beer";
            var venId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.GroupOfProduct_GetByCode(code, venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void GroupOfProduct_ResetPrice()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void GroupOfProductMapping_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.GroupOfProductMapping_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void GroupOfProductMappingNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var venId = 6;
            var gopId = 2;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.GroupOfProductMappingNotIn_List(request, gopId, venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void GroupOfProductMapping_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void GroupOfProductMapping_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region VENContract

        #region Common
        [TestMethod]
        public void VENContract_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            using (var bl = new BLVendor())
            {
                bl.Account_Setting();
            }

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Get()
        {
            var id = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Data()
        {
            var id = 6;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_Data(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_ByCustomerList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var venId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_ByCustomerList(request, venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        [TestMethod]
        public void VENContract_Routing_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_Routing_List(contractId, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Routing_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Routing_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Routing_NotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_Routing_NotIn_List(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Routing_NotIn_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Routing_Insert()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Routing_Export()
        {
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_Routing_Export(contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Routing_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_RoutingByCus_List()
        {
            var venId = 4;
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                bl.VENContract_RoutingByCus_List(venId, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_KPI_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_KPI_Routing_List()
        {
            var contractId = 43;
            var routingId = 770;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_KPI_Routing_List(request, contractId, routingId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_KPI_Check_Expression()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_KPI_Check_Hit()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_KPI_Routing_Apply()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Routing_CATNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_Routing_CATNotIn_List(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Routing_CATNotIn_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Routing_ContractTermList()
        {
            var contractId = 8;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_Routing_ContractTermList(contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_Get()
        {
            var id = 72;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_NewRouting_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_LocationList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_NewRouting_LocationList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_AreaList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_NewRouting_AreaList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_AreaGet()
        {
            var id = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_NewRouting_AreaGet(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_AreaSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_AreaDelete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_AreaRefresh()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_AreaDetailList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var areaId = 12;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_NewRouting_AreaDetailList(request, areaId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_AreaDetailGet()
        {
            var id = 75;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_NewRouting_AreaDetailGet(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_AreaDetailSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_NewRouting_AreaDetailDelete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Price_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractTermId = 8;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_Price_List(request, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Price_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Price_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Price_Data()
        {
            var contractTermId = 8;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_Price_Data(contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Price_Get()
        {
            var id = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_Price_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Price_Copy()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Price_DeletePriceNormal()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Price_DeletePriceLevel()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupVehicle_GetData()
        {
            var id = 1314;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_GroupVehicle_GetData(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupVehicle_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupVehicle_ExcelData()
        {
            var id = 1314;
            var contractTermId = 1298;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_GroupVehicle_ExcelData(id, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupVehicle_ExcelImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupVehicle_GOVList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1314;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_GroupVehicle_GOVList(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupVehicle_GOVDelete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupVehicle_GOVNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1314;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_GroupVehicle_GOVNotInList(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupVehicle_GOVNotInSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceGVLevel_DetailData()
        {
            var id = 1353;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_PriceGVLevel_DetailData(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceGVLevel_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceGVLevel_ExcelData()
        {
            var id = 1353;
            var contractTermId = 1187;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_PriceGVLevel_ExcelData(id, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceGVLevel_ExcelImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupProduct_List()
        {
            var id = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_GroupProduct_List(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupProduct_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupProduct_Export()
        {
            var id = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_GroupProduct_Export(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_GroupProduct_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Loading_List()
        {
            var id = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_Loading_List(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Loading_Location_NotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Loading_Location_NotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Loading_Location_NotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Loading_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Loading_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Loading_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        //[TestMethod]
        //public void VENPrice_DI_Loading_Export()
        //{
        //    var priceId = 1356;

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLVendor())
        //    {
        //        var lst = bl.VENPrice_DI_Loading_Export(priceId);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < 1);
        //}

        [TestMethod]
        public void VENPrice_DI_Loading_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceEx_RoutingParentList()
        {
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceEx_RoutingParentList(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceEx_RoutingList()
        {
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceEx_RoutingList(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceEx_DisLocationList()
        {
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceEx_DisLocationList(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceEx_StockLocationList()
        {
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceEx_StockLocationList(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceEx_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceEx_List(id, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceEx_Detail()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceEx_Get()
        {
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceEx_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceEx_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceEx_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_CO_COPackingPrice_List()
        {
            var priceId = 1225;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_CO_COPackingPrice_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_CO_COPackingPrice_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_CO_COPackingPrice_Export()
        {
            var priceId = 1225;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_CO_COPackingPrice_Export(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_CO_COPackingPrice_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_CO_Service_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_CO_Service_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_CO_Service_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceLevel_DetailData()
        {
            var priceId = 2376;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_PriceLevel_DetailData(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceLevel_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceLevel_ExcelData()
        {
            var priceId = 2376;
            var contractTermId = 1166;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_PriceLevel_ExcelData(priceId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceLevel_ExcelImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Get()
        {
            var priceId = 2376;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQ_Get(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_GroupLocation_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQ_GroupLocation_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_GroupLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_GroupLocation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_GroupLocation_GroupNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_GroupProduct_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQ_GroupProduct_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_GroupProduct_Get()
        {
            var id = 6;
            var venId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_PriceMOQ_GroupProduct_Get(id, venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_GroupProduct_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_GroupProduct_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_GroupProduct_GOPList()
        {
            var venId = 1108;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQ_GroupProduct_GOPList(venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Location_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQ_Location_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Location_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Location_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Location_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Location_LocationNotInSaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Location_LocationNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;
            var venId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQ_Location_LocationNotInList(request, moqId, venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Route_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQ_Route_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Route_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Route_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_Route_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;
            var contractTermId = 1258;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQ_Route_RouteNotInList(request, moqId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_ParentRoute_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                bl.VENPrice_DI_PriceMOQ_ParentRoute_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_ParentRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_ParentRoute_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQ_ParentRoute_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;
            var contractTermId = 1258;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(request, moqId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Get()
        {
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_GroupLocation_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_GroupLocation_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_GroupLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_GroupLocation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_GroupLocation_GroupNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_GroupLocation_GroupNotInList(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_GroupProduct_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_GroupProduct_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_GroupProduct_Get()
        {
            var id = 1168;
            var venId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_Ex_GroupProduct_Get(id, venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_GroupProduct_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_GroupProduct_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_GroupProduct_GOPList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Location_List()
        {
            var venId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_GroupProduct_GOPList(venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Location_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Location_Get()
        {
            var id = 2;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_Ex_Location_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Location_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Location_LocationNotInSaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Location_LocationNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;
            var venId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_Location_LocationNotInList(request, id, venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Route_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_Route_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Route_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Route_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_Route_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;
            var contractTermId = 1349;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_Route_RouteNotInList(request, id, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_ParentRoute_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_ParentRoute_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_ParentRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_ParentRoute_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Ex_ParentRoute_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;
            var contractTermId = 1349;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_Ex_ParentRoute_RouteNotInList(request, id, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Load_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Load_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_Load_DeleteAllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadLocation_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_LoadLocation_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadLocation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadLocation_LocationNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_LoadLocation_LocationNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadLocation_LocationNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadRoute_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_LoadRoute_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadRoute_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadRoute_RouteNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_LoadRoute_RouteNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadRoute_RouteNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadPartner_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_LoadPartner_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadPartner_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadPartner_PartnerNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_LoadPartner_PartnerNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadPartner_PartnerNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_LoadPartner_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoad_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoad_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoad_DeleteAllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadLocation_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_UnLoadLocation_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadLocation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadLocation_LocationNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_UnLoadLocation_LocationNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadLocation_LocationNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadRoute_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_UnLoadRoute_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadRoute_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadRoute_RouteNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_UnLoadRoute_RouteNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadRoute_RouteNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadPartner_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_UnLoadPartner_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadPartner_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadPartner_PartnerNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_UnLoadPartner_PartnerNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadPartner_PartnerNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_UnLoadPartner_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 2376;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQLoad_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Get()
        {
            var id = 36;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_PriceMOQLoad_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_GroupLocation_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQLoad_GroupLocation_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_GroupLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_GroupLocation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_GroupProduct_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQLoad_GroupProduct_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_GroupProduct_Get()
        {
            var id = 6;
            var venId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_PriceMOQLoad_GroupProduct_Get(id, venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_GroupProduct_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_GroupProduct_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_GroupProduct_GOPList()
        {
            var venId = 1108;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQLoad_GroupProduct_GOPList(venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Location_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQLoad_Location_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Location_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Location_LocationNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;
            var venId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQLoad_Location_LocationNotInList(request, moqId, venId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Route_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQLoad_Route_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Route_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Route_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_Route_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;
            var contractTermId = 1258;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQLoad_Route_RouteNotInList(request, moqId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_ParentRoute_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                bl.VENPrice_DI_PriceMOQLoad_ParentRoute_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_ParentRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_ParentRoute_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;
            var contractTermId = 1258;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(request, moqId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQUnLoad_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1283;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENPrice_DI_PriceMOQUnLoad_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQUnLoad_Get()
        {
            var id = 20;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENPrice_DI_PriceMOQUnLoad_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQUnLoad_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQUnLoad_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENPrice_DI_PriceMOQUnLoad_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_GroupOfProduct_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 7;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_GroupOfProduct_List(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_GroupOfProduct_Get()
        {
            var id = 13;
            var contractId = 7;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_GroupOfProduct_Get(id, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_GroupOfProduct_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_GroupOfProduct_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_GroupOfProduct_Check()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_MaterialChange_Data()
        {
            var id = 1349;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_MaterialChange_Data(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_MaterialChange_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_ContractTerm_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_ContractTerm_List(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_ContractTerm_Get()
        {
            var id = 1349;
            var contractID = 6;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_ContractTerm_Get(id, contractID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_ContractTerm_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_ContractTerm_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_ContractTerm_Price_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractTermId = 1349;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_ContractTerm_Price_List(request, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_ContractTerm_Open()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_ContractTerm_Close()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENTerm_Change_RemoveWarning()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_TypeOfRunLevelSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_GOVList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_Setting_GOVList(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_GOVGet()
        {
            var id = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_Setting_GOVGet(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_GOVSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_GOVDeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_GOVNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_Setting_GOVNotInList(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_GOVNotInSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_LevelList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 68;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_Setting_LevelList(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_LevelGet()
        {
            var id = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var obj = bl.VENContract_Setting_LevelGet(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_LevelSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_LevelDeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void VENContract_Setting_Level_GOVList()
        {
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
                var lst = bl.VENContract_Setting_Level_GOVList(contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region PriceHistory
        [TestMethod]
        public void PriceHistory_CheckPrice()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PriceHistory_GetDataOneUser()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PriceHistory_GetDataMulUser()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PriceHistory_GetListVendor()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLVendor())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
    }
}
