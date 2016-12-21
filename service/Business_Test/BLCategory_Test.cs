using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using System.Collections;
using System.Collections.Generic;
using DTO;
using System.Diagnostics;
using Kendo.Mvc.UI;

namespace Business_Test
{
    [TestClass]
    public class BLCategory_Test : BaseTest
    {

        #region All Data
        [TestMethod]
        public void ALL_Country()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_District()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_District();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_Province()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_Province();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_Ward()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_Ward();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_SysVar()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_SysVar(SYSVarType.TypeOfActivity);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_Customer()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_Customer();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CustomerInUser()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CustomerInUser();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_Vendor()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_Vendor();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_VendorInUser()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_VendorInUser();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_GroupOfVehicle()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_GroupOfVehicle();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_Service()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_Service();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATPacking()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_CATPacking(SYSVarType.TypeOfPackingCO);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATPackingGOP()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CATPackingGOP();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_TroubleCostStatus()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_TroubleCostStatus();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATGroupOfRomooc()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CATGroupOfRomooc();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATGroupOfEquipment()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CATGroupOfEquipment();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void All_CATGroupOfPartner()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.All_CATGroupOfPartner();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATGroupOfMaterial()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CATGroupOfMaterial();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATGroupOfCost()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CATGroupOfCost();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_Material()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_Material();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATTypeOfPriceDIEx()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CATTypeOfPriceDIEx();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATGroupOfLocation()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CATGroupOfLocation();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_OPSTypeOfDITOGroupProductReturn()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_OPSTypeOfDITOGroupProductReturn();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATTypeOfDriverFee()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CATTypeOfDriverFee();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATDrivingLicence()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CATDrivingLicence();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_CATShift()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_CATShift();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ALL_FLMTypeOfScheduleFee()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_FLMTypeOfScheduleFee();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region CATCountry

        [TestMethod]
        public void CATCountry_CRUD()
        {
            string fileName = "CATCountry_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATCountry item = new CATCountry();
            item.Code = "Test01-VN";
            item.CountryName = "Việt Nam";

            using (var bl = new BLCategory())
            {
                
                // Test hàm List Quốc gia
                lstContent.Add("Test method: CATCountry_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATCountry_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

               
                // Test ham them mới Quốc gia
                lstContent.Add("Test method: CATCountry_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATCountry_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");


                // Hàm sữa Quốc gia
                CATCountry edit = new CATCountry();
                edit.ID = id;
                edit.Code = "Test02-VN";
                edit.CountryName = "Việt Nam 2";
                lstContent.Add("Test method: CATCountry_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                id = bl.CATCountry_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                
                // Hàm get Quốc gia
                lstContent.Add("Test method: CATCountry_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATCountry_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                // Hàm delete Quốc gia
                lstContent.Add("Test method: CATCountry_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATCountry_Delete(edit);
                edit = bl.CATCountry_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }
        #region phần bỏ
        //[TestMethod]
        //public void CATCountry_Edit()
        //{

        //    CATCountry item = new CATCountry();
        //    item.ID = _countryID;
        //    item.Code = "Test02-VN";
        //    item.CountryName = "Việt Nam 2";

        //    using (var bl = new BLCategory())
        //    {
        //        _countryID = bl.CATCountry_Save(item);
        //        Assert.IsTrue(item.ID > 0);
        //    }
        //}

        //[TestMethod]
        //public void CATCountry_Get()
        //{
        //    using (var bl = new BLCategory())
        //    {
        //        var item = bl.CATCountry_Get(_countryID);
        //        Assert.IsTrue(item != null);
        //    }
        //}

        //[TestMethod]
        //public void CATCountry_Delete()
        //{
        //    CATCountry obj = new CATCountry();
        //    obj.ID = _countryID;
        //    using (var bl = new BLCategory())
        //    {
        //        bl.CATCountry_Delete(obj);
        //        var item = bl.CATCountry_Get(_countryID);
        //        Assert.IsTrue(item == null);
        //    }
        //}
        //[TestMethod]
        //public void CATCountry_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATCountry_List(request);
        //    }
        //    //DateTime dtEnd = DateTime.Now;
        //    //var sub = dtEnd - dtStart;
        //    //Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        //[TestMethod]
        //public void CATCountry_Add()
        //{
        //    CATCountry item = new CATCountry();
        //    item.Code = "Test01-VN";
        //    item.CountryName = "Việt Nam";

        //    using (var bl = new BLCategory())
        //    {
        //        _countryID = bl.CATCountry_Save(item);
        //        Assert.IsTrue(item.ID > 0);
        //    }

        //}
        #endregion
        #endregion

        #region CATDistrict
        [TestMethod]
        public void CATDistrict_CRUD()
        {
            string fileName = "CATDistrict_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATDistrict item = new CATDistrict();
            item.ProvinceID = 1;
            item.CountryID = 1;
            item.Code = "Test_01";
            item.DistrictName = "Test_Huyen_01";

            using (var bl = new BLCategory())
            {
                lstContent.Add("Test method: CATDistrict_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                // Hàm list Quận/ Huyện
                var lst = bl.CATDistrict_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                lstContent.Add("Test method: CATDistrict_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                // Hàm thêm mới Quận/ Huyện
                var id = bl.CATDistrict_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa Quận/ Huyện
                CATDistrict edit = new CATDistrict();
                edit.ID = id;
                edit.ProvinceID = 1;
                edit.CountryID = 1;

                edit.Code = "Test_02";
                edit.DistrictName = "Test_Huyen_02";
                lstContent.Add("Test method: CATDistrict_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                id = bl.CATDistrict_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Hàm Get Quận/Huyện
                lstContent.Add("Test method: CATDistrict_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATDistrict_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                // Hàm delete Quận/ Huyện
                lstContent.Add("Test method: CATDistrict_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATDistrict_Delete(edit);
                edit = bl.CATDistrict_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

        }
        #region Phần bỏ
        //[TestMethod]
        //public void CATDistrict_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATDistrict_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATDistrict_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATDistrict_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATDistrict_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion
        #endregion

        #region CATProvince

        [TestMethod]

        public void CATProvince_CRUD()
        {
            string fileName = "CATProvince_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATProvince item = new CATProvince();
            item.CountryID = 1;
            item.Code = "Test_01";
            item.ProvinceName = "Test_01";

            using (var bl = new BLCategory())
            {
                // Hàm list Tỉnh/Thành
                lstContent.Add("Test method: CATProvince_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATProvince_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Hàm thêm mới Tỉnh/Thành
                lstContent.Add("Test method: CATProvince_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATProvince_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Hàm sữa Tỉnh/Thành
                CATProvince edit = new CATProvince();
                edit.CountryID = 1;
                edit.ID = id;
                edit.Code = "Test_02";
                edit.ProvinceName = "Test_02";
                lstContent.Add("Test method: CATProvince_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                id = bl.CATProvince_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Hàm Get Tỉnh/Thành
                lstContent.Add("Test method: CATProvince_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATProvince_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                //Hàm delete Tỉnh/Thành
                lstContent.Add("Test method: CATProvince_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATProvince_Delete(edit);
                edit = bl.CATProvince_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #region Phần bỏ
        //[TestMethod]
        //public void CATProvince_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATProvince_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATProvince_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATProvince_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //  var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATProvince_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion
        #endregion

        #region Sea port
        public CATPartner getDataSeaport()
        {
            CATPartner item = new CATPartner();
            item.ID = 0;
            item.Code = "TestVNDVU";
            item.Address = "Khu CN  Đình Vũ - Đông Hải 1 - Quận Hải An, Hải Phòng";
            item.TypeOfPartnerID = 0;
            item.PartnerName = "Cảng Đình Vũ - Hải Phòng";
            return item;
        }

        [TestMethod]
        public void SeaPort_CRUD()
        {
            int ID = 0;
            List<int> lstCustomer = new List<int> { 1, 2, 3 };
            CATPartner item = null;
            CATPartner item1 = null;

            using (var bl = new BLCategory())
            {
                //Ham List (SeaPortCustomer_List)
                var lst = bl.SeaPortCustomer_List(lstCustomer);

                //Ham add (SeaPort_Save)
                ID = bl.SeaPort_Save(getDataSeaport());
                Assert.IsTrue(ID > 0);

                //Ham get (SeaPort_Get)
                item = bl.SeaPort_Get(ID);
                Assert.IsTrue(item != null);

                //Ham edit (SeaPort_Save);
                item.Code = "Test";
                bl.SeaPort_Save(item);
                item1 = bl.SeaPort_Get(ID);
                Assert.IsTrue(item.Code == item1.Code);

                //Ham delete (SeaPort_Delete)
                bl.SeaPort_Delete(item1);
                item = bl.SeaPort_Get(ID);
                Assert.IsTrue(item == null);
            }
        }

        [TestMethod]
        public void SeaPortCustom_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.SeaPortCustom_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        //[TestMethod]
        //public void SeaPortCustomer_List()
        //{

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.SeaPortCustomer_List(lstCustomer);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void SeaPort_Get()
        //{
        //    int ID = 2011;
        //    CATPartner lst = null;
        //    using (var bl = new BLCategory())
        //    {
        //        lst = bl.SeaPort_Get(ID);
        //    }
        //    Assert.IsTrue(lst != null);
        //}

        //[TestMethod]
        //public void SeaPort_Save()
        //{
        //    int ID = 0;
        //    using (var bl = new BLCategory())
        //    {
        //        ID = bl.SeaPort_Save(item);
        //    }
        //    Assert.IsTrue(ID > 0);
        //}
        //[TestMethod]
        //public void SeaPort_Delete()
        //{
        //    int ID = 6586;
        //    CATPartner item = null;
        //    using (var bl = new BLCategory())
        //    {
        //        var data = bl.SeaPort_Get(ID);
        //        bl.SeaPort_Delete(data);
        //        item = bl.SeaPort_Get(ID);
        //    }
        //    Assert.IsTrue(item == null);
        //}

        [TestMethod]
        public void SeaPortCustom_Save()
        {
            DTOCATSeaPortCustom item = new DTOCATSeaPortCustom();
            item.Address = "Khu CN  Đình Vũ - Đông Hải 1 - Quận Hải An, Hải Phòng";
            item.Code = "VNDU";
            item.ID = 2011;
            item.PartnerName = "Cảng Đình Vũ - Hải Phòng";
            item.IsPartner = true;
            item.lstPartnerLocation = new List<DTOCATPartnerLocation>();
            var partner = new DTOCATPartnerLocation();
            partner.Code = "VNDU";
            partner.CustomerID = 4;
            item.lstPartnerLocation.Add(partner);

            int flag = 0;
            using (var bl = new BLCategory())
            {
                bl.SeaPortCustom_Save(item);
                List<int> lstCustomer = new List<int> { 4 };
                var Custom_List = bl.SeaPortCustomer_List(lstCustomer);
                List<DTOPartnerResult> lstPartner = Custom_List.lstPartner;
                foreach (var Item_lstPartner in lstPartner)
                {
                    if (Item_lstPartner.Code == partner.Code)
                    {
                        flag = 1;
                    }
                }
            }
            Assert.IsTrue(flag == 1);
        }

        #endregion

        #region Distributor
        [TestMethod]
        public void Customer_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Customer_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DistributorCustom_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.DistributorCustom_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        public DTOCATDistributor getDataDistributor()
        {
            DTOCATDistributor item = new DTOCATDistributor();
            item.Address = "1";
            item.Code = "TestBIGCHCM";
            item.PartnerName = "PartnerName";
            item.CountryID = 1;
            item.CountryName = "Việt Nam";
            item.DistrictID = 1;
            item.DistrictName = "Ba Đình";
            item.ProvinceID = 1;
            item.ProvinceName = "Hà Nội";
            return item;
        }

        [TestMethod]
        public void DistributorCustomer_CRUD()
        {
            List<int> lstCustomer = new List<int> { 1, 2, 3 };
            DTOCATDistributor item = null;
            DTOCATDistributor item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham List ()
                var lst = bl.DistributorCustomer_List(lstCustomer);

                //Ham Add ()
                ID = bl.Distributor_Save(getDataDistributor());
                Assert.IsTrue(ID > 0);

                //Ham Get ()
                item = bl.Distributor_Get(ID);
                Assert.IsTrue(item != null);

                //Ham Edit ()
                item.Code = "TEST";
                bl.Distributor_Save(item);
                item2 = bl.Distributor_Get(ID);
                Assert.IsTrue(item.Code == item2.Code);

                //Ham Delete ()
                bl.Distributor_Delete(item2);
                item = bl.Distributor_Get(ID);
                Assert.IsTrue(item == null);
            }
        }

        //[TestMethod]
        //public void DistributorCustomer_List()
        //{
        //    List<int> lstCustomer = new List<int> { 1, 2, 3 };

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.DistributorCustomer_List(lstCustomer);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void Distributor_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void Distributor_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //  var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        //[TestMethod]
        //public void Distributor_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        [TestMethod]
        public void DistributorCustom_Save()
        {
            DTOCATDistributorCustom DataInput = new DTOCATDistributorCustom();
            DataInput.Address = "1";
            DataInput.Code = "BIGHCM";
            DataInput.ID = 1981;
            DataInput.PartnerName = "BIGC HCM";
            DataInput.IsPartner = true;
            DataInput.lstPartnerLocation = new List<DTOCATPartnerLocation>();
            DTOCATPartnerLocation ItemPartner = new DTOCATPartnerLocation();
            ItemPartner.Code = "ANC";
            ItemPartner.CustomerID = 4;
            DataInput.lstPartnerLocation.Add(ItemPartner);

            List<int> lstCustomer = new List<int> { 4 };
            int flag = 0;
            using (var bl = new BLCategory())
            {
                bl.DistributorCustom_Save(DataInput);
                var lstPartner = bl.DistributorCustomer_List(lstCustomer).lstPartner;
                foreach (var item in lstPartner)
                {
                    if (item.Code == ItemPartner.Code)
                    {
                        flag = 1;
                    }
                }

            }
            Assert.IsTrue(flag == 1);
        }

        #endregion

        #region Carrier

        [TestMethod]
        public void CarrierCustom_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CarrierCustom_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CarrierCustom_Save()
        {
            //List<DTOCATCarrierCustom> lstInput = new List<DTOCATCarrierCustom>();
            //DTOCATCarrierCustom carrierCustom = new DTOCATCarrierCustom();
            //carrierCustom.Code = "METROQ2";
            //carrierCustom.ID = 2009;
            //carrierCustom.IsPartner = true;
            //carrierCustom.PartnerName = "HoanHao";
            //carrierCustom.lstPartnerLocation = new List<DTOCATPartnerLocation>();
            //DTOCATPartnerLocation ItemPartner = new DTOCATPartnerLocation();
            //ItemPartner.Code = "00700Test";
            //ItemPartner.CustomerID = 4;
            //carrierCustom.lstPartnerLocation.Add(ItemPartner);
            //lstInput.Add(carrierCustom);

            //int flag = 0;
            //List<int> lstCustomer = new List<int> { 4 };
            //using (var bl = new BLCategory())
            //{
            //    bl.CarrierCustom_Save(lstInput);
            //    var itemCarrierList = bl.CarrierCustomer_List(lstCustomer).lstPartner;
            //    foreach (var item in itemCarrierList)
            //    {
            //        if (item.Code == ItemPartner.Code)
            //        {
            //            flag = 1;
            //        }
            //    }
            //}
            //Assert.IsTrue(flag == 1);
        }

        public DTOCATCarrier getDataCarrier()
        {
            DTOCATCarrier item = new DTOCATCarrier();
            item.ID = 0;
            item.Address = "1";
            item.Code = "TestBIGCHCM";
            item.PartnerName = "PartnerName";
            item.CountryID = 1;
            item.CountryName = "Việt Nam";
            item.DistrictID = 1;
            item.DistrictName = "Ba Đình";
            item.ProvinceID = 1;
            item.ProvinceName = "Hà Nội";
            return item;
        }

        [TestMethod]
        public void CarrierCustomer_CRUD()
        {
            List<int> lstCustomer = new List<int> { 1, 2, 3 };
            int ID = 0;
            DTOCATCarrier item = null;
            DTOCATCarrier item1 = null;
            using (var bl = new BLCategory())
            {
                //Ham List ()
                var lst = bl.CarrierCustomer_List(lstCustomer);

                //Ham Add ()
                //ID = bl.Carrier_Save(getDataCarrier());
                Assert.IsTrue(ID > 0);

                //Ham Get ()
                item = bl.Carrier_Get(ID);
                Assert.IsTrue(item != null);

                //Ham Edit ()
                item.Code = "TestBIGCHCM123";
                //bl.Carrier_Save(item);
                item1 = bl.Carrier_Get(ID);
                Assert.IsTrue(item.Code == item1.Code);

                //Ham Delete ()
                bl.Carrier_Delete(item1);
                item = bl.Carrier_Get(ID);
                Assert.IsTrue(item == null);
                
            }

        }

        //[TestMethod]
        //public void CarrierCustomer_List()
        //{
        //    List<int> lstCustomer = new List<int> { 1, 2, 3 };

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CarrierCustomer_List(lstCustomer);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void Carrier_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void Carrier_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void Carrier_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region LocationInDistributor
        public DTOCATLocationInPartner getDataLocationInDistributor()
        {
            DTOCATLocationInPartner data = new DTOCATLocationInPartner();
            data.Location = "TestABC";
            data.LocationID = 0;
            data.PartnerCode = "TestABC";
            data.PartnerID = 0;
            return data;
        }

        [TestMethod]
        public void LocationInDistributor_CRUD()
        {
            DTOCATLocationInPartner item = null;
            DTOCATLocationInPartner item1 = null;
            int partnerID = 1981;
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            using (var bl = new BLCategory())
            { 
                //Ham List ()
                bl.LocationInDistributor_List(request,partnerID);

                //Ham Add ()
                item = bl.LocationInDistributor_Save(getDataLocationInDistributor(), partnerID);
                Assert.IsTrue(item != null);

                //Ham Get ()
                item1 = bl.LocationInDistributor_Get(item.ID);
                Assert.IsTrue(item1 != null);

                //Ham Edit ()
                item.Lat = 0;
                item1 = bl.LocationInDistributor_Save(item, partnerID);
                Assert.IsTrue(item.Lat == item1.Lat);

                //Ham Delete ()
                bl.LocationInDistributor_Delete(item1);
                item = null;
                item = bl.LocationInDistributor_Get(item1.ID);
                Assert.IsTrue(item == null);
            }

        }

        //[TestMethod]
        //public void LocationInDistributor_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
        //    int partnerID = 1;

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.LocationInDistributor_List(request, partnerID);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void LocationInDistributor_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void LocationInDistributor_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void LocationInDistributor_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        [TestMethod]
        public void LocationNotInDistributor_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int partnerID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.LocationNotInDistributor_List(request, partnerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationNotInDistributor_SaveList()
        {
            List<DTOCATLocationInPartner> lst = new List<DTOCATLocationInPartner>();
            DTOCATLocationInPartner item = new DTOCATLocationInPartner();
            item.Address = "2 Trần Hưng Đạo, P.Cầu Kho";
            item.CountryID = 1;
            item.CountryName = "Việt Nam";
            item.DistrictID = 30;
            item.DistrictName = "Quận 1";
            item.ID = 1046;
            item.Lat = 10.8042804;
            item.Lng = 106.7175784;
            item.Location = "BIGC Quận 1";
            item.LocationID = 1046;
            item.PartnerCode = "BIGC1";
            item.PartnerID = 0;
            item.ProvinceID = 2;
            item.ProvinceName = "Hồ Chí Minh";
            lst.Add(item);

            int partnerid = 1982; 
            using (var bl = new BLCategory())
            {
                bl.LocationNotInDistributor_SaveList(lst, partnerid);
            }
        }

        #endregion

        #region LocationInSeaport

        public DTOCATLocationInPartner getDataLocationInSeaport()
        {
            DTOCATLocationInPartner data = new DTOCATLocationInPartner();
            data.PartnerCode ="Test00001";
            data.PartnerID = 0;
            data.Location = "Test00001";
            data.LocationID = 0;
            data.Address = "Quận 9";
            data.CountryID = 1;
            data.CountryName = "Việt Nam";
            data.DistrictID = 1;
            data.DistrictName = "Ba Đình";
            data.ProvinceID = 1;
            data.ProvinceName = "Hà Nội";
            return data;
        }


        [TestMethod]
        public void LocationInSeaport_CRUD()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DTOCATLocationInPartner item = null;
            DTOCATLocationInPartner item1 = null;
            int partnerID = 1982;
            using (var bl = new BLCategory())
            {
                //Ham List ()
                var lst = bl.LocationInSeaport_List(request, partnerID);

                //Ham save ()
                item = bl.LocationInSeaport_Save(getDataLocationInSeaport(), partnerID);
                Assert.IsTrue(item != null);

                //Ham Get ()
                item1 = bl.LocationInSeaport_Get(item.ID);
                Assert.IsTrue(item1 != null);

                //Ham Edit ()
                item1.PartnerCode = "123000Test";
                item = bl.LocationInSeaport_Save(item1, partnerID);
                Assert.IsTrue(item1.PartnerCode == item.PartnerCode);

                //Ham Delete ()
                bl.LocationInStation_Delete(item);
                item1 = bl.LocationInStation_Get(item.ID);
                Assert.IsTrue(item1 == null);
            }
        }

        //[TestMethod]
        //public void LocationInSeaport_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
        //    int partnerID = 1;

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.LocationInSeaport_List(request, partnerID);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void LocationInSeaport_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void LocationInSeaport_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void LocationInSeaport_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        [TestMethod]
        public void LocationNotInSeaport_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int partnerID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.LocationNotInSeaport_List(request, partnerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationNotInSeaport_SaveList()
        {
            List<DTOCATLocationInPartner> lst= new List<DTOCATLocationInPartner>();
            DTOCATLocationInPartner item = new DTOCATLocationInPartner();
            item.Address = "Test1123";
            item.CountryID = 1;
            item.CountryName = "Việt Nam";
            item.DistrictID = 1;
            item.DistrictName = "Ba Đình";
            item.ProvinceID = 1;
            item.ProvinceName = "Hà Nội";
            item.ID = 1;
            lst.Add(item);
            int partnerid = 2011;
            using (var bl = new BLCategory())
            {
                bl.LocationNotInCarrier_SaveList(lst, partnerid);
            }
        }
        #endregion

        #region LocationInCarrier

        public DTOCATLocationInPartner getDataLocationInCarrier_CRUD()
        {
            DTOCATLocationInPartner data = new DTOCATLocationInPartner();
            
            return data;
        }

        [TestMethod]
        public void LocationInCarrier_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int partnerID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.LocationInCarrier_List(request, partnerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationInCarrier_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationInCarrier_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationNotInCarrier_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int partnerID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.LocationNotInCarrier_List(request, partnerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationNotInCarrier_SaveList()
        {
            List<DTOCATLocationInPartner> lst = new List<DTOCATLocationInPartner>();
            DTOCATLocationInPartner item = new DTOCATLocationInPartner();
            item.Address = "Test1123";
            item.CountryID = 1;
            item.CountryName = "Việt Nam";
            item.DistrictID = 1;
            item.DistrictName = "Ba Đình";
            item.ProvinceID = 1;
            item.ProvinceName = "Hà Nội";
            item.ID = 1;
            lst.Add(item);

            int partnerid = 2009;
            using (var bl = new BLCategory())
            {
                bl.LocationNotInCarrier_SaveList(lst, partnerid);
            }
        }
        #endregion

        #region CATStock
        [TestMethod]
        public void FLMStock_CURD()
        {
            string fileName = "FLMStock_CURD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            FLMStock item = new FLMStock();
            item.Code = "Test_ 01";
            item.StockName = "Test_01";

            using (var bl = new BLCategory())
            {
                //Hàm  list Kho
                lstContent.Add("Test method: FLMStock_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.FLMStock_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                 // Hàm thêm kho
                lstContent.Add("Test method: FLMStock_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.FLMStock_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa kho
                FLMStock edit = new FLMStock();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.StockName = "Test_02";
                lstContent.Add("Test method: FLMStock_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.FLMStock_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm get kho
                lstContent.Add("Test method: FLMStock_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.FLMStock_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                // Hàm xóa kho
                lstContent.Add("Test method: FLMStock_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.FLMStock_Delete(edit);
                edit = bl.FLMStock_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }
        #region Phần bỏ

        [TestMethod]
        public void FLMStock_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.FLMStock_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMStock_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMStock_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMStock_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATLocation
        [TestMethod]
        public void CATLocation_CURD()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATLocation item = new CATLocation();
            item.Code = "Test_01";
            item.Location = "Test_01";
            item.Address = "Test_01";
            item.CountryID = 1;
            item.ProvinceID = 1;
            item.DistrictID = 1;
            item.WardID = null;
            item.Lat = null;
            item.Lng = null;
            item.EconomicZone = "Test_01";
            item.GroupOfLocationID = 1;
            item.LoadTimeDI = null;
            item.UnLoadTimeDI = null;
            item.CreatedBy = "Test_01";
            item.CreatedDate = null;
            item.Note = null;
            item.Note1 = null;

            using (var bl = new BLCategory())
            {
                // Hàm list danh mục các điểm
                var lst = bl.Location_List(request);

                // Hàm thêm mới danh mục các điểm
                var id = bl.Location_Save(item);
                Assert.IsTrue(id > 0);

                // Hàm sữa danh mục các điểm
                CATLocation edit = new CATLocation();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.Location = "Test_02";
                edit.Address = "Test_02";
                edit.CountryID = 1;
                edit.ProvinceID = 1;
                edit.DistrictID = 1;
                edit.WardID = null;
                edit.Lat = null;
                edit.Lng = null;
                edit.EconomicZone = "Test_02";
                edit.GroupOfLocationID = 1;
                edit.LoadTimeDI = null;
                edit.UnLoadTimeDI = null;
                edit.CreatedBy = "Test_02";
                edit.CreatedDate = null;
                edit.Note = null;
                edit.Note1 = null;
                bl.Location_Save(edit);
                Assert.IsTrue(id > 0);

                // Hàm get danh mục các điểm
                var edit2 = bl.Location_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);

                // Hàm xóa danh mục các điểm
                bl.Location_Delete(edit);
                edit = bl.Location_Get(id);
                Assert.IsTrue(edit == null);

            }
        }

        #region Phần bỏ
        [TestMethod]
        public void Location_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Location_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Location_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Location_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Location_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        [TestMethod]
        public void ExcelLocation_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ExcelLocation_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelLocation_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region DrivingLicence

        [TestMethod]
        public void DrivingLicence_CRUD()
        {
            string fileName = "DrivingLicence_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATDrivingLicence item = new CATDrivingLicence();
            item.TypeOfVehicleID = 6;
            item.Code = "Test_01";
            item.VehicleWeight = 0;
            item.DrivingLicenceName = "Test_01";
            item.Description = "Test_01";
            using (var bl = new BLCategory())
            {
                // Hàm list Loại bằng lái
                lstContent.Add("Test method: DrivingLicence_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.DrivingLicence_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Hàm thêm Loại bằng lái
                lstContent.Add("Test method: DrivingLicence_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.DrivingLicence_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sửa loại bằng lái
                CATDrivingLicence edit = new CATDrivingLicence();
                edit.ID = id;
                edit.TypeOfVehicleID = 6;
                edit.VehicleWeight = 0;
                edit.Code = "Test_02";
                edit.DrivingLicenceName = "Test_02";
                edit.Description = "Test_02";
                lstContent.Add("Test method: DrivingLicence_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.DrivingLicence_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm Get loại Băng lái
                lstContent.Add("Test method: DrivingLicence_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.DrivingLicence_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                //Hàm delete loại bằng lái
                lstContent.Add("Test method: DrivingLicence_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.DrivingLicence_Delete(edit);
                edit = bl.DrivingLicence_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

        }

        #region Phần bỏ
        //[TestMethod]
        //public void DrivingLicence_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.DrivingLicence_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void DrivingLicence_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void DrivingLicence_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void DrivingLicence_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion
        #endregion

        #region Service
        [TestMethod]
        public void GroupOfService_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.GroupOfService_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Cost_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Cost_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CostRevenue_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CostRevenue_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Packing_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Packing_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region CategoryFactory
        [TestMethod]
        public void Customer_Vehicle()
        {
            int? cusID = null;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Customer_Vehicle(cusID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Customer_Romooc()
        {
            int? cusID = null;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Customer_Romooc(cusID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Container Packing

        [TestMethod]
        public void ContainerPacking_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ContainerPacking_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Thiết lập cung đường
        [TestMethod]
        public void Routing_CURD()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DTOCATRouting item = new DTOCATRouting();
            item.Code = "test_01";
            item.RoutingName = "Test_01";
            item.EDistance = null;
            item.EHours = null;
            item.IsAreaLast = false;
            item.ParentID = 586;
            item.LocationFromID = null;
            item.LocationToID = null;
            item.RoutingAreaToID = 10;
            item.RoutingAreaFromID = 40;
            item.IsUse = true;
            item.AreaFromName = "Test_01";
            item.AreaToName = "Test_01";
            item.IsLocation = false;
            item.IsArea = true;
            item.IsChangeAble = false;
            item.Note = null;

            using (var bl = new BLCategory())
            {
                // Hàm list thiết lập cung dường
                var lst = bl.RoutingAll_List();
                var lst1 = bl.Routing_List(request);

                // Hàm thêm phần thiết lập cung đường

                var id = bl.Routing_Save(item);
                Assert.IsTrue(id > 0);

                // Hàm sửa phần thiết lập cung đường
                DTOCATRouting edit = new DTOCATRouting();
                edit.ID = id;
                edit.Code = "test_02";
                edit.RoutingName = "Test_02";
                edit.EDistance = null;
                edit.EHours = null;
                edit.IsAreaLast = false;
                edit.ParentID = 586;
                edit.LocationFromID = null;
                edit.LocationToID = null;
                edit.RoutingAreaToID = 10;
                edit.RoutingAreaFromID = 40;
                edit.IsUse = true;
                edit.AreaFromName = "Test_02";
                edit.AreaToName = "Test_02";
                edit.IsLocation = false;
                edit.IsArea = true;
                edit.IsChangeAble = false;
                edit.Note = null;
                bl.Routing_Save(edit);
                Assert.IsTrue(id > 0);

                // Hàm get phần thiết lập cung đường
                var edit2 = bl.Routing_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);

                // Hàm xóa phần thiết lập cung đường
                bl.Routing_Delete(edit);
                edit = bl.Routing_Get(id);
                Assert.IsTrue(edit == null);

            }
        }
        #region Phần bỏ
        [TestMethod]
        public void RoutingAll_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.RoutingAll_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Routing_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Routing_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Routing_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Routing_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Routing_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        [TestMethod]
        public void Routing_UpdateLocationForAllRouting()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Routing_SaveAllCustomer()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingLocationNotIn_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int? LocationFrom = null;
            int? LocationTo = null;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.RoutingLocationNotIn_List(request, LocationFrom, LocationTo);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingAreaNotIn_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int? AreaFrom = null;
            int? AreaTo = null;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.RoutingAreaNotIn_List(request, AreaFrom, AreaTo);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingArea_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingArea_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingArea_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingAreaDetail_List()
        {
            int AreaID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.RoutingAreaDetail_List(AreaID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingAreaDetail_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingAreaDetail_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingAreaDetail_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingAreaDetail_Refresh()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingCost_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int routingID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.RoutingCost_List(request, routingID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingCost_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingCost_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RoutingCost_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelRoutingCost_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ExcelRoutingCost_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelRoutingCost_HeaderList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ExcelRoutingCost_HeaderList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelRoutingArea_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ExcelRoutingArea_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelCost_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ExcelCost_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelArea_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ExcelArea_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelRoutingCost_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelArea_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelProvince_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ExcelProvince_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelDistrict_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ExcelDistrict_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void AddressSearch_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.AddressSearch_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ExcelRouteAreaLocation_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ExcelRouteAreaLocation_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATRouting_ExcelData()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATRouting_ExcelData();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATRouting_AllCustomerList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATRouting_AllCustomerList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region CATGroupOfCost, CATCost

        [TestMethod]
        public void CATCost_GroupList()
        {
            int ID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATCost_GroupList(ID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region Import Hãng tàu, Cảng biển, Nhà phân phối

        [TestMethod]
        public void Distributor_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Distributor_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Carrier_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Carrier_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SeaPort_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.SeaPort_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Station_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Station_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DistributorLocation_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.DistributorLocation_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CarrierLocation_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CarrierLocation_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SeaPortLocation_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.SeaPortLocation_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void StationLocation_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.StationLocation_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void GroupOfPartner_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.GroupOfPartner_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Distributor_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Carrier_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SeaPort_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Station_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Distributor_Export()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Distributor_Export();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SeaPort_Export()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.SeaPort_Export();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Station_Export()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Station_Export();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Carrier_Export()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.Carrier_Export();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PartnerLocation_Distributor_Export()
        {
            List<int> lstCustomerID = new List<int> { 1, 2, 3 };
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.PartnerLocation_Distributor_Export(lstCustomerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PartnerLocation_Carrier_Export()
        {
            List<int> lstCustomerID = new List<int> { 1, 2, 3 };
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.PartnerLocation_Carrier_Export(lstCustomerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PartnerLocation_SeaPort_Export()
        {
            List<int> lstCustomerID = new List<int> { 1, 2, 3 };
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.PartnerLocation_SeaPort_Export(lstCustomerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void PartnerLocation_Station_Export()
        {
            List<int> lstCustomerID = new List<int> { 1, 2, 3 };
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.PartnerLocation_Station_Export(lstCustomerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DistributorLocation_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CarrierLocation_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SeaPortLocation_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void StationLocation_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region CATConstraint
        [TestMethod]
        public void CATConstraint_CURD()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DTOCATConstraint item = new DTOCATConstraint();
            item.Code = "Tes_01";
            item.ConstraintName = "Test_01";
            item.IsApproved = false;

            using (var bl = new BLCategory())
            {
                // Hàm lấy list của ràng buộc cung đường
                var lst = bl.CATConstraint_List(request);

                // Hàm thêm ràng buộc cung đường
                var id = bl.CATConstraint_Save(item);
                Assert.IsTrue(id > 0);

                // Hàm sửa ràng buộc cung đường
                DTOCATConstraint edit = new DTOCATConstraint();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.ConstraintName = "Test_02";
                edit.IsApproved = false;
                bl.CATConstraint_Save(edit);
                
                // Hàm get ràng buộc cung đường
                var edit2 = bl.CATConstraint_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);

                // Hàm xóa ràng buộc cung đường
                bl.CATConstraint_Delete(edit);
                edit = bl.CATConstraint_Get(id);
                Assert.IsTrue(edit == null);
            }

        }
        #region Phần bỏ
        [TestMethod]
        public void CATConstraint_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATConstraint_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        [TestMethod]
        public void CATConstraint_UpdateConstraint()
        {
            List<int> lstID = new List<int> { 1, 2, 3 };

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                bl.CATConstraint_UpdateConstraint(lstID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_Route_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int ID = 1;


            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATConstraint_Route_List(request, ID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_RouteNotIn_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_Route_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_RouteNotIn_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int ID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATConstraint_RouteNotIn_List(request, ID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_Location_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int ID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATConstraint_Location_List(request, ID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_LocationNotIn_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_Location_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_LocationNotIn_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int ID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATConstraint_LocationNotIn_List(request, ID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_OpenHour_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int ID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATConstraint_OpenHour_List(request, ID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_OpenHour_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_OpenHour_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_OpenHour_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        #region Phần bỏ
        [TestMethod]
        public void CATConstraint_Weight_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int ID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATConstraint_Weight_List(request, ID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_Weight_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_Weight_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATConstraint_Weight_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATCurrency

        [TestMethod]
        public void CATCurrency_CRUD()
        {
            string fileName = "CATCurrency_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATCurrency item = new CATCurrency();
            item.Code = "Test_01";
            item.CurrencyName = "Test_01";


            using (var bl = new BLCategory())
            {
                // Hàm list Tiền tệ
                lstContent.Add("Test method: CATCurrency_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATCurrency_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Hàm thêm mới Tiền tệ
                lstContent.Add("Test method: CATCurrency_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATCurrency_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa tiền tệ
                CATCurrency edit = new CATCurrency();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.CurrencyName = "Test_02";
                lstContent.Add("Test method: CATCurrency_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATCurrency_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Hàm Get Tiền tệ
                lstContent.Add("Test method: CATCurrency_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATCurrency_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                // Hàm delete Tiền tệ
                lstContent.Add("Test method: CATCurrency_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATCurrency_Delete(edit);
                edit = bl.CATCurrency_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                lstContent.Add("Test method: CATCurrency_AllList");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                List<CATCurrency> lst1 = bl.CATCurrency_AllList();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #region Phần bỏ
        [TestMethod]
        public void CATCurrency_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATCurrency_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATCurrency_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATCurrency_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATCurrency_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATCurrency_AllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATCurrency_AllList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATReason


        [TestMethod]
        public void CATReason_CRUD()
        {
            string fileName = "CATReason_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DTOCATReason item = new DTOCATReason();
            item.ReasonName = "Test_01";
            item.TypeOfReasonID = -(int)SYSVarType.TypeOfReasonTenderReject;

            using (var bl = new BLCategory())
            {
                //Hàm list Danh mục lý do
                lstContent.Add("Test method: CATReason_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATReason_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");


                // Hàm thêm Danh mục lý do
                lstContent.Add("Test method: CATReason_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATReason_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa Danh mục lý do
                DTOCATReason edit = new DTOCATReason();
                edit.ID = id;
                edit.ReasonName = "Test_02";
                edit.TypeOfReasonID = -(int)SYSVarType.TypeOfReasonTenderReject;
                lstContent.Add("Test method: CATReason_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATReason_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");


                //Hàm Get danh muc lý do
                lstContent.Add("Test method: CATReason_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATReason_Get(id);
                Assert.IsTrue(edit.ReasonName == edit2.ReasonName);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.ReasonName == edit2.ReasonName ? "success" : "fail"));
                lstContent.Add("");
                
                // Hàm xóa danh mục lý do
                lstContent.Add("Test method: CATReason_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATReason_Delete(edit);
                edit = bl.CATReason_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);

            }

        }

        #region Phần bỏ
        [TestMethod]
        public void CATReason_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATReason_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATReason_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATReason_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATReason_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATGroupOfVehicle

        [TestMethod]
        public void CATGroupOfVehicle_CRUD()
        {
            string fileName = "CATGroupOfVehicle_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);


            CATGroupOfVehicle item = new CATGroupOfVehicle();
            item.Code = "Test_01";
            item.GroupName = "Test_01";
            item.ParentID = null;
            item.Ton = 8;
            item.SortOrder = 50;

            using (var bl = new BLCategory())
            {
                //Hàm list loại xe
                lstContent.Add("Test method: CATGroupOfVehicle_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATGroupOfVehicle_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Hàm them loại xe
                lstContent.Add("Test method: CATGroupOfVehicle_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATGroupOfVehicle_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm chỉnh sữa loại xe
                CATGroupOfVehicle edit = new CATGroupOfVehicle();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.GroupName = "Test_02";
                edit.ParentID = null;
                edit.Ton = 1.5;
                edit.SortOrder = 50;
                lstContent.Add("Test method: CATGroupOfVehicle_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfVehicle_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm Get loại xe
                lstContent.Add("Test method: CATGroupOfVehicle_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATGroupOfVehicle_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                // Hàm xóa laoi5 xe
                lstContent.Add("Test method: CATGroupOfVehicle_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfVehicle_Delete(edit);
                edit = bl.CATGroupOfVehicle_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #region Phần bỏ
        [TestMethod]
        public void CATGroupOfVehicle_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATGroupOfVehicle_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfVehicle_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfVehicle_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfVehicle_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATGroupOfRomooc
        [TestMethod]
        public void CATGroupOfRomooc_CRUD()
        {
            string fileName = "CATCountry_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATGroupOfRomooc item = new CATGroupOfRomooc();
            //item.TypeName = "Test_01";
            item.ParentID = null;

            using (var bl = new BLCategory())
            {
                // Hàm Get Romooc
                lstContent.Add("Test method: CATGroupOfRomooc_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATGroupOfRomooc_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Ham Them Romooc
                lstContent.Add("Test method: CATGroupOfRomooc_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATGroupOfRomooc_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa Romooc
                CATGroupOfRomooc edit = new CATGroupOfRomooc();
                edit.ID = id;
                //edit.TypeName = "Test_02";
                edit.ParentID = null;
                lstContent.Add("Test method: CATGroupOfRomooc_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfRomooc_Save(edit);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm Get Romooc
                lstContent.Add("Test method: CATGroupOfRomooc_Get - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATGroupOfRomooc_Get(id);
               // Assert.IsTrue(edit.TypeName == edit2.TypeName);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                //lstContent.Add("Status: " + (edit.TypeName == edit2.TypeName ? "success" : "fail"));
                lstContent.Add("");

                // Hàm delete Romooc
                lstContent.Add("Test method: CATGroupOfRomooc_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfRomooc_Delete(edit);
                edit = bl.CATGroupOfRomooc_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #region phần bỏ
        [TestMethod]
        public void CATGroupOfRomooc_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATGroupOfRomooc_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfRomooc_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfRomooc_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfRomooc_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATGroupOfEquipment
        [TestMethod]
        public void CATGroupOfEquipment_CRUD()
        {
            string fileName = "CATGroupOfEquipment_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATGroupOfEquipment item = new CATGroupOfEquipment();
            item.GroupName = "Test_01";
            item.ParentID = null;

            using (var bl = new BLCategory())
            {
                //Hàm list Loại Romooc
                lstContent.Add("Test method: CATGroupOfEquipment_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATGroupOfEquipment_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Hàm thêm loại Romooc
                lstContent.Add("Test method: CATGroupOfEquipment_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATGroupOfEquipment_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa loại Romooc
                CATGroupOfEquipment edit = new CATGroupOfEquipment();
                edit.ID = id;
                edit.GroupName = "Test_02";
                edit.ParentID = null;
                lstContent.Add("Test method: CATGroupOfEquipment_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfEquipment_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm Get loại Romooc
                lstContent.Add("Test method: CATGroupOfEquipment_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATGroupOfEquipment_Get(id);
                Assert.IsTrue(edit.GroupName == edit2.GroupName);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.GroupName == edit2.GroupName ? "success" : "fail"));
                lstContent.Add(""); 

                // Hàm delete loại Romooc
                lstContent.Add("Test method: CATGroupOfEquipment_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfEquipment_Delete(edit);
                edit = bl.CATGroupOfEquipment_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #region Phần bỏ
        [TestMethod]
        public void CATGroupOfEquipment_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATGroupOfEquipment_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfEquipment_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfEquipment_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfEquipment_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATGroupOfMaterial

        [TestMethod]
        public void CATGroupOfMaterial_CRUD()
        {
            string fileName = "CATGroupOfMaterial_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATGroupOfMaterial item = new CATGroupOfMaterial();
            item.Code = "Test_01";
            item.GroupName = "Test_01";
            item.ParentID = null;
            item.IsFuel = false;

            using (var bl = new BLCategory())
            {
                // Hàm list Nhóm vật tư
                lstContent.Add("Test method: CATGroupOfMaterial_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATGroupOfMaterial_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");


                // Hàm thêm Nhóm vật tư
                lstContent.Add("Test method: CATGroupOfMaterial_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATGroupOfMaterial_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa nhóm vật tư
                CATGroupOfMaterial edit = new CATGroupOfMaterial();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.GroupName = "Test_02";
                edit.ParentID = null;
                edit.IsFuel = false;
                lstContent.Add("Test method: CATGroupOfMaterial_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfMaterial_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Hàm Get nhóm vật tư
                lstContent.Add("Test method: CATGroupOfMaterial_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATGroupOfMaterial_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                //Hàm Delete nhóm vật tư
                lstContent.Add("Test method: CATGroupOfMaterial_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfMaterial_Delete(edit);
                edit = bl.CATGroupOfMaterial_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #region Phần bỏ
        [TestMethod]
        public void CATGroupOfMaterial_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATGroupOfMaterial_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfMaterial_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfMaterial_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfMaterial_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATMaterial
        public FLMMaterial SetFLMMaterial()
        {
            FLMMaterial Material = new FLMMaterial();
            Material.ID = 0;
            Material.Code = "Diesel";
            Material.MaterialName = "Test001";
            Material.GroupOfMaterialID = 1;
            return Material;
        }

        [TestMethod]
        public void FLMMaterial_CRUD()
        {
            string fileName = "FLMMaterial_CRUD";
            List<string> lstContent = new List<string>();


            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            FLMMaterial item = null;
            FLMMaterial item2 = null;
            int FLMMaterialID = 0;

            using (var bl = new BLCategory())
            {
                //Ham list (FLMMaterial_List)
                lstContent.Add("Test method: FLMMaterial_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var list = bl.FLMMaterial_List(request);
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (FLMMaterial_Save)
                lstContent.Add("Test method:FLMMaterial_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                FLMMaterialID = 0;
                FLMMaterialID = bl.FLMMaterial_Save(SetFLMMaterial());
                Assert.IsTrue(FLMMaterialID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (FLMMaterialID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Ham get (FLMMaterial_Get)
                lstContent.Add("Test method:FLMMaterial_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.FLMMaterial_Get(FLMMaterialID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                //Ham edit (FLMMaterial_Save)
                lstContent.Add("Test method:FLMMaterial_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.MaterialName = "EditTest001";
                bl.FLMMaterial_Save(item);
                item2 = bl.FLMMaterial_Get(FLMMaterialID);
                Assert.IsTrue(item.MaterialName == item2.MaterialName);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.MaterialName == item2.MaterialName ? "success" : "fail"));
                lstContent.Add("");

                //Ham delete (FLMMaterial_Delete)
                lstContent.Add("Test method:FLMMaterial_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.FLMMaterial_Delete(item2);
                item = bl.FLMMaterial_Get(FLMMaterialID);
                Assert.IsTrue(item == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }


        //[TestMethod]
        //public void FLMMaterial_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.FLMMaterial_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void FLMMaterial_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //        var item = bl.FLMMaterial_Get(FLMMaterialID);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void FLMMaterial_Add()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    FLMMaterial item = null;
        //    using (var bl = new BLCategory())
        //    {
        //        FLMMaterialID = 0;
        //        FLMMaterialID = bl.FLMMaterial_Save(SetFLMMaterial());
        //        bl.FLMMaterial_Delete(SetFLMMaterial());
        //        item = bl.FLMMaterial_Get(FLMMaterialID);
        //    }
        //    //DateTime dtEnd = DateTime.Now;
        //    //var sub = dtEnd - dtStart;
        //    //Assert.IsTrue(sub.Seconds < MaxSec);
        //    Assert.IsTrue(item.ID < 0);
        //}

        //[TestMethod]
        //public void FLMMaterial_Edit()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    FLMMaterial item = null;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //        FLMMaterialID = 0;
        //        FLMMaterialID = bl.FLMMaterial_Save(SetFLMMaterial());
        //        item = SetFLMMaterial();
        //        item.MaterialName = "EditTest001";
        //        FLMMaterialID = bl.FLMMaterial_Save(item);
        //        item = bl.FLMMaterial_Get(FLMMaterialID);
        //        bl.FLMMaterial_Delete(item);
        //        item = bl.FLMMaterial_Get(FLMMaterialID);
        //    }
        //    //DateTime dtEnd = DateTime.Now;
        //    //var sub = dtEnd - dtStart;
        //    Assert.IsTrue(item.ID < 0);
        //}

        //[TestMethod]
        //public void FLMMaterial_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    FLMMaterial item = null;
        //    using (var bl = new BLCategory())
        //    {
        //        FLMMaterialID = 0;
        //        FLMMaterialID = bl.FLMMaterial_Save(SetFLMMaterial());
        //        item = bl.FLMMaterial_Get(FLMMaterialID);
        //        bl.FLMMaterial_Delete(item);
        //        item = bl.FLMMaterial_Get(FLMMaterialID);
        //    }
        //    //DateTime dtEnd = DateTime.Now;
        //    //var sub = dtEnd - dtStart;
        //    Assert.IsTrue(item.ID < 0);
        //}
        #endregion

        #region CATRomooc
        public CATRomooc getDataRomooc()
        {
            CATRomooc data = new CATRomooc();
            data.ID = 0;
            data.RegNo = "Test51R-1234";
            data.IsOwn = false;
            data.Lat = 10.79856;
            data.Lng = 106.72239;
            return data;
        }

        [TestMethod]
        public void CATRomooc_CRUD()
        {
            string fileName = "CATRomooc_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            CATRomooc item = null;
            CATRomooc item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham List
                lstContent.Add("Test method: CATRomooc_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATRomooc_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATRomooc_Save)
                lstContent.Add("Test method: CATRomooc_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = 0;
                ID = bl.CATRomooc_Save(getDataRomooc());
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Ham get (CATRomooc_Get)
                lstContent.Add("Test method: CATRomooc_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATRomooc_Get(ID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                //Ham edit (CATRomooc_Save)
                lstContent.Add("Test method: CATRomooc_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Lat = 10;
                bl.CATRomooc_Save(item);
                item2 = bl.CATRomooc_Get(ID);
                Assert.IsTrue(item.Lat == item2.Lat);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Lat == item2.Lat ? "success" : "fail"));
                lstContent.Add("");

                //Ham delete (CATRomooc_Delete)
                lstContent.Add("Test method: CATRomooc_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATRomooc_Delete(item2);
                item = bl.CATRomooc_Get(ID);
                Assert.IsTrue(item == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);

            }
        }

        //[TestMethod]
        //public void CATRomooc_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATRomooc_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATRomooc_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATRomooc_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATRomooc_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region CATVehicle
        public DTOCATVehicle getDataVehicle()
        {
            DTOCATVehicle data = new DTOCATVehicle();
            data.ID = 0;
            data.RegNo = "Test51R-1234";
            data.TypeOfVehicleID = 6;
            data.RegCapacity = 030;
            data.MaxCapacity = 020;
            data.MaxWeight = 40;
            return data;
        }

        [TestMethod]
        public void CATVehicle_CRUD()
        {
            string fileName = "CATVehicle_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DTOCATVehicle item = null;
            DTOCATVehicle item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (CATVehicle_List)
                lstContent.Add("Test method: CATVehicle_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATVehicle_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATVehicle_Save)
                lstContent.Add("Test method: CATVehicle_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = 0;
                ID = bl.CATVehicle_Save(getDataVehicle());
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Ham get (CATVehicle_Get)
                lstContent.Add("Test method: CATVehicle_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATVehicle_Get(ID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                //Ham edit (CATVehicle_Save)
                lstContent.Add("Test method: CATVehicle_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.RegCapacity = 140;
                bl.CATVehicle_Save(item);
                item2 = bl.CATVehicle_Get(ID);
                Assert.IsTrue(item.RegCapacity == item2.RegCapacity);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.RegCapacity == item2.RegCapacity ? "success" : "fail"));
                lstContent.Add("");

                //Ham delete ()
                lstContent.Add("Test method: CATVehicle_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATVehicle_Delete(item2);
                item = bl.CATVehicle_Get(ID);
                Assert.IsTrue(item == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);

            }
        }

        //[TestMethod]
        //public void CATVehicle_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATVehicle_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATVehicle_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATVehicle_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATVehicle_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region CATGroupOfService
        public CATGroupOfService getDataGroupOfService()
        {
            CATGroupOfService data = new CATGroupOfService();
            data.ID = 0;
            data.Code = "TestXNK";
            data.GroupOfService = "Dich vu xuat nhap";
            return data;
        }

        [TestMethod]
        public void CATGroupOfService_CRUD()
        {
            string fileName = "CATGroupOfService_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            CATGroupOfService item = null;
            CATGroupOfService item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (CATGroupOfService_List)
                lstContent.Add("Test method: CATGroupOfService_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATGroupOfService_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATGroupOfService_Save)
                lstContent.Add("Test method: CATGroupOfService_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = 0;
                ID = bl.CATGroupOfService_Save(getDataGroupOfService());
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Ham get (CATGroupOfService_Get)
                lstContent.Add("Test method: CATGroupOfService_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATGroupOfService_Get(ID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                //Ham edit (CATGroupOfService_Save)
                lstContent.Add("Test method: CATGroupOfService_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "XNKTest";
                bl.CATGroupOfService_Save(item);
                item2 = bl.CATGroupOfService_Get(ID);
                Assert.IsTrue(item.Code == item2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Code == item2.Code ? "success" : "fail"));
                lstContent.Add("");

                //Ham delete (CATGroupOfService_Delete)
                lstContent.Add("Test method: CATGroupOfService_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfService_Delete(item2);
                item = bl.CATGroupOfService_Get(ID);
                Assert.IsTrue(item == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        //[TestMethod]
        //public void CATGroupOfService_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATGroupOfService_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfService_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfService_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfService_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region CATService
        public CATService getDataService()
        {
            CATService data = new CATService();
            data.ID = 0;
            data.Code = "TestIM";
            data.ServiceName = "Lap chung tu nhap khau";
            data.ServiceTypeID = 132;
            data.ServiceTypeName = "Theo to khai";
            return data;
        }

        [TestMethod]
        public void CATService_CRUD()
        {
            string fileName = "CATService_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            CATService item = null;
            CATService item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (CATService_List)
                lstContent.Add("Test method: CATService_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATService_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATService_Save)
                lstContent.Add("Test method: CATService_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = 0;
                ID = bl.CATService_Save(getDataService());
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Ham get (CATService_Get)
                lstContent.Add("Test method: CATService_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATService_Get(ID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                //Ham edit (CATService_Save)
                lstContent.Add("Test method: CATService_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "IMTest";
                bl.CATService_Save(item);
                item2 = bl.CATService_Get(ID);
                Assert.IsTrue(item.Code == item2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Code == item2.Code ? "success" : "fail"));
                lstContent.Add("");

                //Ham delete (CATService_Delete)
                lstContent.Add("Test method: CATService_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATService_Delete(item2);
                item = bl.CATService_Get(ID);
                Assert.IsTrue(item == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        //[TestMethod]
        //public void CATService_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATService_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATService_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATService_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATService_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        #endregion

        #region CATGroupOfPartner
        public CATGroupOfPartner getDataGroupOfPartner()
        {
            CATGroupOfPartner data = new CATGroupOfPartner();
            data.ID = 0;
            data.Code = "TestNPP";
            data.GroupName = "Loai nha phan phoi";
            return data;
        }

        [TestMethod]
        public void CATGroupOfPartner_CRUD()
        {
            string fileName = "CATGroupOfPartner_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            CATGroupOfPartner item = null;
            CATGroupOfPartner item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (CATGroupOfPartner_List)
                lstContent.Add("Test method: CATGroupOfPartner_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATGroupOfPartner_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATGroupOfPartner_Save)
                lstContent.Add("Test method: CATGroupOfPartner_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = 0;
                ID = bl.CATGroupOfPartner_Save(getDataGroupOfPartner());
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Ham get (CATGroupOfPartner_Get)
                lstContent.Add("Test method: CATGroupOfPartner_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATGroupOfPartner_Get(ID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                //Ham edit (CATGroupOfPartner_Save)
                lstContent.Add("Test method: CATGroupOfPartner_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "NPPTest";
                bl.CATGroupOfPartner_Save(item);
                item2 = bl.CATGroupOfPartner_Get(ID);
                Assert.IsTrue(item.Code == item2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Code == item2.Code ? "success" : "fail"));
                lstContent.Add("");

                //Ham delete (CATGroupOfPartner_Delete)
                lstContent.Add("Test method: CATGroupOfPartner_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfPartner_Delete(item2);
                item = bl.CATGroupOfPartner_Get(ID);
                Assert.IsTrue(item == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        //[TestMethod]
        //public void CATGroupOfPartner_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATGroupOfPartner_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfPartner_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //  var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfPartner_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //  var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfPartner_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region CATShift
        [TestMethod]
        public void CATShift_CURD()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            CATShift item = new CATShift();
            item.Code = "Test_01";
            item.ShiftName = "Test_01";
            item.TimeFrom = DateTime.Now.AddHours(4);
            item.TimeTo = DateTime.Now.AddHours(9);
            item.Hour = 7;

            using (var bl = new BLCategory())
            {
                // Hàm list ca/kít
                var lst = bl.CATShift_List(request);

                // Hàm thêm ca/kít
                var id = bl.CATShift_Save(item);
                Assert.IsTrue(id > 0);

                // Hàm sửa ca/kít
                CATShift edit = new CATShift();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.ShiftName = "Test_02";
                edit.TimeFrom = DateTime.Now.AddHours(4);
                edit.TimeTo = DateTime.Now.AddHours(9);
                edit.Hour = 7;
                bl.CATShift_Save(edit);
                Assert.IsTrue(id > 0);

                // Hàm Get ca/kít
                var edit2 = bl.CATShift_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);

                // Hàm xóa ca/kít
                bl.CATShift_Delete(edit);
                edit = bl.CATShift_Get(id);
                Assert.IsTrue(edit == null);
            }

        }
        #region Phần bỏ
        [TestMethod]
        public void CATShift_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATShift_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATShift_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATShift_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATShift_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATGroupOfTrouble

        public CATGroupOfTrouble getDataGroupOfTrouble()
        {
            CATGroupOfTrouble data = new CATGroupOfTrouble();
            data.ID = 0;
            data.Code = "Test001";
            data.Name = "Thay vo xe";
            data.TypeOfGroupTroubleID = 38;
            data.TypeOfGroupTroubleName = "Phan phoi";
            return data;
        }
        [TestMethod]
        public void CATGroupOfTrouble_CRUD()
        {
            string fileName = "CATGroupOfTrouble_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            CATGroupOfTrouble item = null;
            CATGroupOfTrouble item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (CATGroupOfTrouble_List)
                lstContent.Add("Test method: CATGroupOfTrouble_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATGroupOfTrouble_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATGroupOfTrouble_Save)
                lstContent.Add("Test method: CATGroupOfTrouble_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = 0;
                ID = bl.CATGroupOfTrouble_Save(getDataGroupOfTrouble());
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Ham get (CATGroupOfTrouble_Get)
                lstContent.Add("Test method: CATGroupOfTrouble_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATGroupOfTrouble_Get(ID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null? "success" : "fail"));
                lstContent.Add("");

                //Ham edit (CATGroupOfTrouble_Save)
                lstContent.Add("Test method: CATGroupOfTrouble_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "01Test";
                bl.CATGroupOfTrouble_Save(item);
                item2 = bl.CATGroupOfTrouble_Get(ID);
                Assert.IsTrue(item.Code == item2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Code == item2.Code ? "success" : "fail"));
                lstContent.Add("");

                //Ham delete (CATGroupOfTrouble_Delete)
                lstContent.Add("Test method: CATGroupOfTrouble_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfTrouble_Delete(item2);
                item = bl.CATGroupOfTrouble_Get(ID);
                Assert.IsTrue(item == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");
                LogResult(fileName, lstContent);
            }
        }
        //[TestMethod]
        //public void CATGroupOfTrouble_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATGroupOfTrouble_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfTrouble_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfTrouble_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfTrouble_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //  var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region CATTypeBusiness
        public CATTypeBusiness getDataTypeBusiness()
        {
            CATTypeBusiness data = new CATTypeBusiness();
            data.ID = 0;
            data.Code = "TestVT";
            data.TypeBusinessName = "Van tai";
            return data;
        }

        [TestMethod]
        public void CATTypeBusiness_CRUD()
        {
            string fileName = "CATTypeBusiness_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            CATTypeBusiness item = null;
            CATTypeBusiness item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (CATTypeBusiness_List)
                lstContent.Add("Test method: CATDistrict_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATTypeBusiness_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATTypeBusiness_Save)
                lstContent.Add("Test method: CATTypeBusiness_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = 0;
                ID = bl.CATTypeBusiness_Save(getDataTypeBusiness());
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");


                //Ham get (CATTypeBusiness_Get)
                lstContent.Add("Test method: CATTypeBusiness_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATTypeBusiness_Get(ID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null? "success" : "fail"));
                lstContent.Add("");


                //Ham edit (CATTypeBusiness_Save)
                lstContent.Add("Test method: CATTypeBusiness_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "VTTest";
                bl.CATTypeBusiness_Save(item);
                item2 = bl.CATTypeBusiness_Get(ID);
                Assert.IsTrue(item.Code == item2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Code == item2.Code ? "success" : "fail"));
                lstContent.Add("");


                //Ham delete (CATTypeBusiness_Delete)
                lstContent.Add("Test method: CATTypeBusiness_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATTypeBusiness_Delete(item2);
                item = bl.CATTypeBusiness_Get(ID);
                Assert.IsTrue(item == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }
        //[TestMethod]
        //public void CATTypeBusiness_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATTypeBusiness_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATTypeBusiness_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATTypeBusiness_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATTypeBusiness_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //  var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region CATField
        [TestMethod]
        public void CATField_CURD()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATField item = new CATField();
            item.Code = "Test_01";
            item.FieldName = "Test_01";

            using (var bl = new BLCategory())
            {
                // Hàm list phần lãnh vực
                var lst = bl.CATField_List(request);

                // Hàm thêm lãnh vực
                var id = bl.CATField_Save(item);
                Assert.IsTrue(id > 0);

                // Hàm sửa lãnh vực
                CATField edit = new CATField();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.FieldName = "Tes_02";
                bl.CATField_Save(edit);
                Assert.IsTrue(id > 0);

                // Hàm Get lãnh vực
                var edit2 = bl.CATField_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);

                // Hàm xóa lành vực
                bl.CATField_Delete(edit);
                edit = bl.CATField_Get(id);
                Assert.IsTrue(edit == null);

            }
        }
        #region Phần bỏ
        [TestMethod]
        public void CATField_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATField_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATField_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATField_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATField_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATScale
        [TestMethod]
        public void CATScale_CURD()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATScale item = new CATScale();
            item.Code = "Test_01";
            item.ScaleName = "Test_01";

            using (var bl = new BLCategory())
            {
                // Hàm list danh sách Qui mô
                var lst = bl.CATScale_List(request);

                // Hàm thêm mới qui mô
                var id = bl.CATScale_Save(item);
                Assert.IsTrue(id > 0);

                // Hàm sửa qui mô
                CATScale edit = new CATScale();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.ScaleName = "Test_02";
                bl.CATScale_Save(edit);
                Assert.IsTrue(id > 0);

                // Hàm get qui mô
                var edit2 = bl.CATScale_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                
                // Hàm xóa qui mô
                bl.CATScale_Delete(edit);
                edit = bl.CATScale_Get(id);
                Assert.IsTrue(edit == null);
            }

        }
        #region
        [TestMethod]
        public void CATScale_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATScale_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATScale_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATScale_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATGroupOfCost
        public CATGroupOfCost getDataGroupOfCost()
        {
            CATGroupOfCost data = new CATGroupOfCost();
            data.ID = 0;
            data.Code = "Test003";
            data.GroupName = "Trạm thu phí";
            data.ParentID = 2;
            return data;
        }

        [TestMethod]
        public void CATGroupOfCost_CRUD()
        {
            string fileName = "CATGroupOfCost_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            CATGroupOfCost item = null;
            CATGroupOfCost item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (CATGroupOfCost_List)
                lstContent.Add("Test method: CATGroupOfCost_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATGroupOfCost_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATGroupOfCost_Save)
                lstContent.Add("Test method: CATGroupOfCost_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = 0;
                ID = bl.CATGroupOfCost_Save(getDataGroupOfCost());
                Assert.IsTrue(ID > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Ham get (CATGroupOfCost_Get)
                lstContent.Add("Test method: CATGroupOfCost_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATGroupOfCost_Get(ID);
                Assert.IsTrue(item != null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                //Ham edit (CATGroupOfCost_Save)
                lstContent.Add("Test method: CATGroupOfCost_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "Test0123";
                bl.CATGroupOfCost_Save(item);
                item2 = bl.CATGroupOfCost_Get(ID);
                Assert.IsTrue(item.Code == item2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Code == item2.Code ? "success" : "fail"));
                lstContent.Add("");

                //Ham delete (CATGroupOfCost_Delete)
                lstContent.Add("Test method: CATGroupOfCost_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfCost_Delete(item2);
                item = bl.CATGroupOfCost_Get(ID);
                Assert.IsTrue(item == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        //[TestMethod]
        //public void CATGroupOfCost_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATGroupOfCost_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfCost_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfCost_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATGroupOfCost_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region CATCost
        [TestMethod]
        public void CATGroupOfCost_GroupList()
        {
            int id = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATGroupOfCost_GroupList(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        [TestMethod]
        public void CATCost_CURD()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DTOCATCost item = new DTOCATCost();
            item.Code = "Test_01";
            item.GroupOfCostID = 1;
            item.CostName = "Test_01";
            item.AccountNo = null;
            item.TypeOfCostID = 185;
            item.IsSystemCost = false;
            using (var bl = new BLCategory())
            {
                // Hàm list phần chi phí
                var lst = bl.CATCost_List(request);
                
                //Hàm thêm chi phí
                var id = bl.CATCost_Save(item);
                Assert.IsTrue(id > 0);

                // Hàm sửa chi phí
                DTOCATCost edit = new DTOCATCost();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.GroupOfCostID = 1;
                edit.CostName = "Test_02";
                edit.AccountNo = null;
                edit.TypeOfCostID = 185;
                edit.IsSystemCost = false;
                bl.CATCost_Save(edit);
                Assert.IsTrue(id > 0);

                // Hàm get chi phí
                var edit2 = bl.CATCost_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);

                // Hàm xóa chi phí
                bl.CATCost_Delete(edit);
                edit = bl.CATCost_Get(id);
                Assert.IsTrue(edit == null);
            }
        }
        #region Phần bỏ

        [TestMethod]
        public void CATCost_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATCost_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATCost_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATCost_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATCost_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region SYSCustomer


        #region Phần bỏ
        [TestMethod]
        public void SYSCustomer_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.SYSCustomer_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SYSCustomer_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SYSCustomer_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        [TestMethod]
        public void SYSCustomer_Goal_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int brachID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.SYSCustomer_Goal_List(request, brachID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SYSCustomer_Goal_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SYSCustomer_Goal_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SYSCustomer_Goal_Reset()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SYSCustomer_GoalDetail_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            int goalID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.SYSCustomer_GoalDetail_List(request, goalID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SYSCustomer_GoalDetail_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region SYSSetting
        [TestMethod]
        public void SYSSetting_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SYSSetting_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }


        #endregion

        #region SYSMaterial
        [TestMethod]
        public void SYSMaterial_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void SYSMaterial_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region CATSYSCustomerTrouble
        [TestMethod]
        public void CATSYSCustomerTrouble_SysCustomer_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATSYSCustomerTrouble_SysCustomer_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATSYSCustomerTrouble_Trouble_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int sysCus = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATSYSCustomerTrouble_Trouble_List(request, sysCus);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATSYSCustomerTrouble_Trouble_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region CATPackingGOPTU

        [TestMethod]
        public void CATPackingGOPTU_CURD()
        {
            //DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            //string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            //CATPacking item = new CATPacking();
            //item.Code = "Test_01";
            //item.PackingName = "Test_01";

            //using (var bl = new BLCategory())
            //{
            //    // List danh sach CATPackingGOPTU
            //    var lst = bl.CATPackingGOPTU_List(request);

            //    // Hàm thêm 1 CATPackingGOPTU
            //    var id = bl.CATPackingGOPTU_Save(item);
            //    Assert.IsTrue(id > 0);

            //    // Hàm sửa 1 CATPackingGOPTU
            //    CATPacking edit = new CATPacking();
            //    edit.ID = id;
            //    edit.Code = "Test_02";
            //    edit.PackingName = "Test_02";
            //    bl.CATPackingGOPTU_Save(edit);
            //    Assert.IsTrue(id > 0);

            //    // Hàm get 1 CATPackingGOPTU
            //    var edit2 = bl.CATPackingGOPTU_Get(id);
            //    Assert.IsTrue(edit.Code == edit2.Code);

            //    // Hàm xóa 1 CATPackingGOPTU
            //    bl.CATPackingGOPTU_Delete(id);
            //    edit = bl.CATPackingGOPTU_Get(id);
            //    Assert.IsTrue(edit == null);
            //}
        }

        #region Phần bỏ
        [TestMethod]
        public void CATPackingGOPTU_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATPackingGOPTU_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATPackingGOPTU_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATPackingGOPTU_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATPackingGOPTU_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATTypeOfRouteProblem
        [TestMethod]
        public void CATTypeOfRouteProblem_CURD()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DTOCATTypeOfRouteProblem item = new DTOCATTypeOfRouteProblem();
            item.Code = "Test_01";
            item.TypeName = "Test_01";

            using (var bl = new BLCategory())
            {
                // Hàm lấy 1 List DTOCATTypeOfRouteProblem
                var lst = bl.CATTypeOfRouteProblem_List(request);

                // Hàm thêm 1 DTOCATTypeOfRouteProblem
                var id = bl.CATTypeOfRouteProblem_Save(item);
                Assert.IsTrue(id > 0);

                // Hàm sửa DTOCATTypeOfRouteProblem
                DTOCATTypeOfRouteProblem edit = new DTOCATTypeOfRouteProblem();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.TypeName = "Test_02";
                bl.CATTypeOfRouteProblem_Save(edit);
                Assert.IsTrue(id > 0);

                // Hàm Get DTOCATTypeOfRouteProblem

                var edit2 = bl.CATTypeOfRouteProblem_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);

                // Hàm xóa DTOCATTypeOfRouteProblem
                bl.CATTypeOfRouteProblem_Delete(edit);
                edit = bl.CATTypeOfRouteProblem_Get(id);
                Assert.IsTrue(edit == null);
            }
            
        }

        #region Phần bỏ
        [TestMethod]
        public void CATTypeOfRouteProblem_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATTypeOfRouteProblem_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATTypeOfRouteProblem_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATTypeOfRouteProblem_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATTypeOfRouteProblem_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATTypeOfPriceDIEx
        public DTOCATTypeOfPriceDIEx getDataTypeOfPriceDIEx()
        {
            DTOCATTypeOfPriceDIEx data = new DTOCATTypeOfPriceDIEx();
            data.ID = 0;
            data.Code = "TestTypeOfPriceExDropPoint";
            data.IsUse = false;
            data.TypeName = "Tha diem";
            return data;
        }

        [TestMethod]
        public void CATTypeOfPriceDIEx_CRUD()
        {
            string fileName = "CATTypeOfPriceDIEx_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DTOCATTypeOfPriceDIEx item = null;
            DTOCATTypeOfPriceDIEx item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (CATTypeOfPriceDIEx_List)
                lstContent.Add("Test method: CATTypeOfPriceDIEx_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATTypeOfPriceDIEx_List(request);
                   lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATTypeOfPriceDIEx_Save)
                lstContent.Add("Test method: CATTypeOfPriceDIEx_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = 0;
                ID = bl.CATTypeOfPriceDIEx_Save(getDataTypeOfPriceDIEx());
                Assert.IsTrue(ID > 0);
                   lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + ( ID > 0? "success" : "fail"));
                lstContent.Add("");

                //Ham get (CATTypeOfPriceDIEx_Get)
                lstContent.Add("Test method: CATTypeOfPriceDIEx_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATTypeOfPriceDIEx_Get(ID);
                Assert.IsTrue(item != null);
                   lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                //Ham edit (CATTypeOfPriceDIEx_Save)
                lstContent.Add("Test method: CATTypeOfPriceDIEx_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "TypeOfPriceExDropPointTest";
                bl.CATTypeOfPriceDIEx_Save(item);
                item2 = bl.CATTypeOfPriceDIEx_Get(ID);
                Assert.IsTrue(item.Code == item2.Code);
                   lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Code == item2.Code ? "success" : "fail"));
                lstContent.Add("");

                //Ham delete (CATTypeOfPriceDIEx_Delete)
                lstContent.Add("Test method: CATTypeOfPriceDIEx_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATTypeOfPriceDIEx_Delete(item2);
                item = bl.CATTypeOfPriceDIEx_Get(ID);
                Assert.IsTrue(item == null);
                   lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                   lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }
        //[TestMethod]
        //public void CATTypeOfPriceDIEx_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATTypeOfPriceDIEx_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATTypeOfPriceDIEx_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATTypeOfPriceDIEx_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATTypeOfPriceDIEx_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region CATGroupOfLocation

        [TestMethod]
        public void CATGroupOfLocation_CRUD()
        {
            string fileName = "CATGroupOfLocation_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DTOCATGroupOfLocation item = new DTOCATGroupOfLocation();
            item.Code = "Test_01";
            item.GroupName = "Test_01";
            item.GroupOfPartnerID = 1;

            using (var bl = new BLCategory())
            {
                // Hàm list Loại địa chỉ
                lstContent.Add("Test method: CATGroupOfLocation_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATGroupOfLocation_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Hàm thêm loại địa chỉ
                lstContent.Add("Test method: CATGroupOfLocation_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATGroupOfLocation_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa lọi địa chỉ
                DTOCATGroupOfLocation edit = new DTOCATGroupOfLocation();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.GroupName = "Test_02";
                edit.GroupOfPartnerID = 1;
                lstContent.Add("Test method: CATGroupOfLocation_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfLocation_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm Get loại địa chỉ
                lstContent.Add("Test method: CATGroupOfLocation_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATGroupOfLocation_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                // Hàm delete Loại địa chỉ
                lstContent.Add("Test method: CATGroupOfLocation_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATGroupOfLocation_Delete(edit);
                edit = bl.CATGroupOfLocation_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #region Phần bỏ
        [TestMethod]
        public void CATGroupOfLocation_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATGroupOfLocation_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfLocation_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfLocation_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //   var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATGroupOfLocation_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region OPSTypeOfDITOGroupProductReturn
        [TestMethod]
        public void OPSTypeOfDITOGroupProductReturn_CRUD()
        {
            string fileName = "OPSTypeOfDITOGroupProductReturn_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DTOOPSTypeOfDITOGroupProductReturn item = new DTOOPSTypeOfDITOGroupProductReturn();
            item.Code = "Test_01";
            item.TypeName = "Test_01";
            item.TypeOfDITOGroupProductReturnStatusID = -(int)SYSVarType.TypeOfDITOGroupProductReturnStatusEmpty;

            using (var bl = new BLCategory())
            {
                // Hàm list lý do trả về
                lstContent.Add("Test method: OPSTypeOfDITOGroupProductReturn_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.OPSTypeOfDITOGroupProductReturn_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                // Hàm thêm lý do trả về
                lstContent.Add("Test method: OPSTypeOfDITOGroupProductReturn_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.OPSTypeOfDITOGroupProductReturn_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa lý do trả về
                DTOOPSTypeOfDITOGroupProductReturn edit = new DTOOPSTypeOfDITOGroupProductReturn();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.TypeName = "Test_02";
                edit.TypeOfDITOGroupProductReturnStatusID = -(int)SYSVarType.TypeOfDITOGroupProductReturnStatusEmpty;
                lstContent.Add("Test method: OPSTypeOfDITOGroupProductReturn_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.OPSTypeOfDITOGroupProductReturn_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm Get lý do trả về
                lstContent.Add("Test method: OPSTypeOfDITOGroupProductReturn_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.OPSTypeOfDITOGroupProductReturn_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                // Hàm delete lý do trả về
                lstContent.Add("Test method: OPSTypeOfDITOGroupProductReturn_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.OPSTypeOfDITOGroupProductReturn_Delete(edit);
                edit = bl.OPSTypeOfDITOGroupProductReturn_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        #region Phần bỏ
        [TestMethod]
        public void OPSTypeOfDITOGroupProductReturn_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.OPSTypeOfDITOGroupProductReturn_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSTypeOfDITOGroupProductReturn_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSTypeOfDITOGroupProductReturn_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSTypeOfDITOGroupProductReturn_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CATTypeOfDriverFee
        [TestMethod]
        public void CATTypeOfDriverFee_CRUD()
        {
            string fileName = "CATTypeOfDriverFee_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DTOCATTypeOfDriverFee item = new DTOCATTypeOfDriverFee();
            item.Code = "Test_01";
            item.TypeName = "Test_01";

            using (var bl = new BLCategory())
            {
                // Hàm list Loại tài xế
                lstContent.Add("Test method: CATTypeOfDriverFee_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATTypeOfDriverFee_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");


                // Hàm thêm Loại tài xế
                lstContent.Add("Test method: CATTypeOfDriverFee_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATTypeOfDriverFee_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa Loại tài xế
                DTOCATTypeOfDriverFee edit = new DTOCATTypeOfDriverFee();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.TypeName = "Test_02";
                lstContent.Add("Test method: CATTypeOfDriverFee_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATTypeOfDriverFee_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm Get Loại tài xế
                lstContent.Add("Test method: CATTypeOfDriverFee_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATTypeOfDriverFee_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                // Hàm delete Loại tài xế
                lstContent.Add("Test method: CATTypeOfDriverFee_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATTypeOfDriverFee_Delete(edit);
                edit = bl.CATTypeOfDriverFee_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }
        #region Phần bỏ
        [TestMethod]
        public void CATTypeOfDriverFee_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATTypeOfDriverFee_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATTypeOfDriverFee_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATTypeOfDriverFee_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATTypeOfDriverFee_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region ORD_TypeOfDoc

        #region Phần bỏ
        [TestMethod]
        public void ORDTypeOfDoc_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ORDTypeOfDoc_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDTypeOfDoc_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDTypeOfDoc_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDTypeOfDoc_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #endregion

        #region FLM_TypeOfScheduleFee
        //[TestMethod]
        //public void FLMTypeOfScheduleFee_CRUD()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DTOFLMTypeOfScheduleFee item = new DTOFLMTypeOfScheduleFee();
        //    item.Code = "Test_01";
        //    item.TypeName = "Test_01";
        //    item.TypeOfScheduleFeeStatusID = 1;

        //    using (var bl = new BLCategory())
        //    {
        //        // Hàm list Loại phụ phí tháng
        //        var lst = bl.FLMTypeOfScheduleFee_List(request);

        //        // Hàm thêm Loại phụ phí tháng
        //        var id = bl.FLMTypeOfScheduleFee_Save(item);
        //        Assert.IsTrue(id > 0);

        //        // Hàm sữa Loại phụ phí tháng
        //        DTOFLMTypeOfScheduleFee edit = new DTOFLMTypeOfScheduleFee();
        //        edit.ID = id;
        //        edit.Code = "Test_02";
        //        edit.TypeName = "Test_02";
        //        edit.TypeOfScheduleFeeStatusID = 1;
        //        bl.FLMTypeOfScheduleFee_Save(edit);
        //        Assert.IsTrue(id > 0);

        //        // Hàm Get Loại phụ phí tháng
        //        var edit2 = bl.FLMTypeOfScheduleFee_Get(id);
        //        Assert.IsTrue(edit.Code == edit2.Code);

        //        // Hàm delete Loại phụ phí tháng
        //        bl.FLMTypeOfScheduleFee_Delete(edit);
        //        edit = bl.FLMTypeOfScheduleFee_Get(id);
        //        Assert.IsTrue(edit == null);
        //    }
        //}

        #region Phấn bỏ
        [TestMethod]
        public void FLMTypeOfScheduleFee_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.FLMTypeOfScheduleFee_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTypeOfScheduleFee_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTypeOfScheduleFee_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void FLMTypeOfScheduleFee_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
        #endregion

        #region CAT_LocationMatrix

        [TestMethod]
        public void CATLocationMatrix_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATLocationMatrix_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATLocationMatrix_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //   var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATLocationMatrix_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATLocationMatrix_Generate()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATLocationMatrix_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATLocationMatrix_GenerateByList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATLocationMatrix_GenerateLimit()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
       
        #endregion

        #region CATStation
        [TestMethod]
        public void CATStation_CRUD()
        {
            string fileName = "CATStation_CRUD";
            List<string> lstContent = new List<string>();

            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            CATPartner item = new CATPartner();
            item.Code = "Test_01";
            item.PartnerName = "Test_01";
            item.Address = "Test_01";
            item.CountryID = 1;
            item.ProvinceID = 1;
            item.DistrictID = 1;
            item.TelNo = "Test_01";
            item.Fax = "Test_01";
            item.Email = "Test_01";
            item.BillingName = "Test_01";
            item.BillingAddress = "Test_01";
            item.TaxCode = "Test_01";
            item.Note = "Test_01";
            using (var bl = new BLCategory())
            {
                // Hàm list Loại phí hàng tháng
                lstContent.Add("Test method: CATStation_Data");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                List<CATPartner> lst = bl.CATStation_Data();
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");


                // Hàm thêm Loại phí hàng tháng
                lstContent.Add("Test method: CATStation_Save - Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var id = bl.CATStation_Save(item);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                // Hàm sữa Loại phí hàng tháng
                CATPartner edit = new CATPartner();
                edit.ID = id;
                edit.Code = "Test_02";
                edit.PartnerName = "Test_02";
                edit.Address = "Test_02";
                edit.CountryID = 1;
                edit.ProvinceID = 1;
                edit.DistrictID = 1;
                edit.TelNo = "Test_02";
                edit.Fax = "Test_02";
                edit.Email = "Test_02";
                edit.BillingName = "Test_02";
                edit.BillingAddress = "Test_02";
                edit.TaxCode = "Test_02";
                edit.Note = "Test_02";
                lstContent.Add("Test method: CATStation_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATStation_Save(edit);
                Assert.IsTrue(id > 0);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (id > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Hàm Get Loại phí hàng tháng
                lstContent.Add("Test method: CATStation_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var edit2 = bl.CATStation_Get(id);
                Assert.IsTrue(edit.Code == edit2.Code);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit.Code == edit2.Code ? "success" : "fail"));
                lstContent.Add("");

                // Hàm xóa Loại phí hàng tháng
                lstContent.Add("Test method: CATStation_Save - Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATStation_Delete(edit);
                edit = bl.CATStation_Get(id);
                Assert.IsTrue(edit == null);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (edit == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }

        }

        #region Phần bỏ
        [TestMethod]
        public void CATStation_Data()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATStation_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATStation_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATStation_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        [TestMethod]
        public void LocationInStation_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int partnerID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.LocationInStation_List(request, partnerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationInStation_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationInStation_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationInStation_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationNotInStation_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int partnerID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.LocationNotInStation_List(request, partnerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void LocationNotInStation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        #endregion

        #region CATSupplier
        public FLMSupplier getDataFLMSupplier()
        {
            FLMSupplier data = new FLMSupplier();
            data.ID = 0;
            data.Code = "Test001";
            data.CountryID = 1;
            data.CountryName = "Việt Nam";
            data.DistrictID = 1;
            data.DistrictName = "Ba Đình";
            data.ProvinceID = 1;
            data.ProvinceName = "Hà Nội";
            data.SupplierName = "Test";
            return data;
        }

        [TestMethod]
        public void FLMSupplier_CRUD()
        {
            string fileName = "FLMSupplier_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            FLMSupplier item = null;
            FLMSupplier item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (FLMSupplier_List)
                lstContent.Add("Test method: FLMSupplier_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.FLMSupplier_List(request);
                     lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");


                //Ham add (FLMSupplier_Save)
                lstContent.Add("Test method: FLMSupplier_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.FLMSupplier_Save(getDataFLMSupplier());
                Assert.IsTrue(ID > 0);
                 lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");


                //Ham get (FLMSupplier_Get)
                lstContent.Add("Test method: FLMSupplier_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.FLMSupplier_Get(ID);
                Assert.IsTrue(item != null);
                 lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");


                //Ham edit (FLMSupplier_Save)
                lstContent.Add("Test method: FLMSupplier_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "Test02";
                bl.FLMSupplier_Save(item);
                item2 = bl.FLMSupplier_Get(ID);
                Assert.IsTrue(item.Code == item2.Code);
                 lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Code == item2.Code ? "success" : "fail"));
                lstContent.Add("");


                //Ham delete (FLMSupplier_Delete)
                lstContent.Add("Test method: FLMSupplier_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.FLMSupplier_Delete(item2);
                item = bl.FLMSupplier_Get(ID);
                Assert.IsTrue(item == null);
                 lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                 lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);

            }
        }
        //[TestMethod]
        //public void FLMSupplier_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.FLMSupplier_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void FLMSupplier_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void FLMSupplier_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void FLMSupplier_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region CATMaterialPrice
        public DTOFLMMaterialPrice getDataFLMMaterialPrice()
        {
            DTOFLMMaterialPrice data = new DTOFLMMaterialPrice();
            data.ID = 0;
            data.SupplierID = 4;
            data.IsFuel = false;
            data.MaterialID = 1;
            data.Price = 12;
            data.EffectDate = DateTime.Now;
            data.parentId = 0;
            return data;
        }

        [TestMethod]
        public void FLMMaterialPrice_CRUD()
        {
            string fileName = "FLMMaterialPrice_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int supplierID = 4;
            int ID = 0;
            DTOFLMMaterialPrice item = null;
            DTOFLMMaterialPrice item2 = null;
            using (var bl = new BLCategory())
            {
                //Ham List (FLMMaterialPrice_List)
                lstContent.Add("Test method: CATDistrict_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.FLMMaterialPrice_List(supplierID, request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (FLMMaterialPrice_Save)
                lstContent.Add("Test method: FLMMaterialPrice_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.FLMMaterialPrice_Save(getDataFLMMaterialPrice());
                Assert.IsTrue(ID > 0);
                 lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Ham get (FLMMaterialPrice_Get)
                lstContent.Add("Test method: FLMMaterialPrice_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.FLMMaterialPrice_Get(ID);
                Assert.IsTrue(item != null);
                 lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                //Ham edit (FLMMaterialPrice_Save)
                lstContent.Add("Test method: FLMMaterialPrice_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Price = 132;
                item.SupplierID = supplierID;
                bl.FLMMaterialPrice_Save(item);
                item2 = bl.FLMMaterialPrice_Get(ID);
                Assert.IsTrue(item.Price == item2.Price);
                 lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Price == item2.Price ? "success" : "fail"));
                lstContent.Add("");

                //Ham delete (FLMaterialPrice_Delete)
                lstContent.Add("Test method: FLMaterialPrice_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.FLMaterialPrice_Delete(item2);
                item = bl.FLMMaterialPrice_Get(ID);
                Assert.IsTrue(item == null);
                 lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                 lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        //[TestMethod]
        //public void FLMMaterialPrice_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
        //    int supplierID = 1;

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.FLMMaterialPrice_List(supplierID, request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void FLMMaterialPrice_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //  var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void FLMMaterialPrice_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void FLMaterialPrice_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}
        #endregion

        #region CATStationPrice

        public DTOCATStationPrice getDataCATStationPrice()
        {
            DTOCATStationPrice data = new DTOCATStationPrice();
            data.ID = 0;
            data.LocationAddress = "2 Trần Hưng Đạo, P.Cầu Kho";
            data.LocationCode = "BIGC1";
            data.LocationID = 1046;
            data.LocationName = "BIGC Quận 1";
            data.Note = "Note6";
            data.Price = 14000;
            data.PriceKM = 7000;
            data.Ton = 2;
            return data;
        }
        [TestMethod]
        public void CATStationPrice_CRUD()
        {
            string fileName = "CATStationPrice_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DTOCATStationPrice item = null;
            DTOCATStationPrice item2 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (CATStationPrice_List)
                lstContent.Add("Test method: CATStationPrice_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATStationPrice_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATStationPrice_Save)
                lstContent.Add("Test method: CATStationPrice_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATStationPrice_Save(getDataCATStationPrice());
                Assert.IsTrue(ID == 0);
                    lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                 lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham get(CATStationPrice_Get)
                lstContent.Add("Test method: CATStationPrice_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATStationPrice_Get(ID);
                    lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
               lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham edit(CATStationPrice_Save)
                lstContent.Add("Test method: CATStationPrice_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Ton = 12;
                bl.CATStationPrice_Save(item);
                item2 = bl.CATStationPrice_Get(ID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham delete (CATStationPrice_Delete)
                lstContent.Add("Test method: CATStationPrice_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATStationPrice_Delete(item2);
                item = bl.CATStationPrice_Get(ID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }
        //[TestMethod]
        //public void CATStationPrice_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATStationPrice_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATStationPrice_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATStationPrice_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATStationPrice_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //  var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        [TestMethod]
        public void CATStationPrice_LocationList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATStationPrice_LocationList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATStationPrice_AssetList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATStationPrice_AssetList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATStationPriceExport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATStationPriceExport();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }


        [TestMethod]
        public void CATStationPrice_Data()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATStationPrice_Data();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATStationPriceImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region CATStationMonth

        public DTOCATStationMonth getDataCATStationMonth()
        {
            DTOCATStationMonth data = new DTOCATStationMonth();
            data.ID = 0;
            data.LocationAddress = "2 Trần Hưng Đạo, P.Cầu Kho";
            data.LocationCode = "BIGC1";
            data.LocationID = 1046;
            data.LocationName = "BIGC Quận 1";
            data.Note = "Note6";
            data.Price = 14000;
            data.AssetID = 3;
            data.AssetNo = "61C-09204";
            data.DateFrom = DateTime.Now;
            data.DateTo = DateTime.Now.AddDays(2);
            return data;
        }

        [TestMethod]
        public void CATStationMonth_List()
        {
            string fileName = "CATStationMonth_List";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DTOCATStationMonth item = null;
            DTOCATStationMonth item1 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham list (CATStationMonth_List)
                lstContent.Add("Test method: CATStationMonth_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATStationMonth_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham add (CATStationMonth_Save)
                lstContent.Add("Test method: CATStationMonth_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATStationMonth_Save(getDataCATStationMonth());
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham get (CATStationMonth_Get)
                lstContent.Add("Test method: CATStationMonth_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATStationMonth_Get(ID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham edit (CATStationMonth_Save)
                lstContent.Add("Test method: CATStationMonth_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Price = 3000;
                bl.CATStationMonth_Save(item);
                item1 = bl.CATStationMonth_Get(ID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham delete (CATStationMonth_Delete)
                lstContent.Add("Test method: CATStationMonth_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATStationMonth_Delete(item1);
                item = bl.CATStationMonth_Get(ID);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }

        //[TestMethod]
        //public void CATStationMonth_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATStationMonth_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATStationMonth_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        // var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATStationMonth_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //   var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATStationMonth_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        //var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        [TestMethod]
        public void CATStationMonth_LocationList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATStationMonth_LocationList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATStationMonth_AssetList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATStationMonth_AssetList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATStationMonthExport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //   var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATStationMonth_Data()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATStationMonth_Data();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATStationMonthImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //    var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region CATLocationMatrixStation

        #region Phần bỏ
        [TestMethod]
        public void CATLocationMatrixStation_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATLocationMatrixStation_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATLocationMatrixStation_Get()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATLocationMatrixStation_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATLocationMatrixStation_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //   var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        [TestMethod]
        public void CATLocationMatrixStation_LocationMatrixList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATLocationMatrixStation_LocationMatrixList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATLocationMatrixStation_LocationList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATLocationMatrixStation_LocationList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion

        #region CATRoutingArea

        public CATRoutingArea getDataCATRoutingArea()
        {
            CATRoutingArea data = new CATRoutingArea();
            data.ID = 0;
            data.AreaName = "Song";
            data.Code = "STTT1";
            return data;
        }

        [TestMethod]
        public void CATRoutingArea_CRUD()
        {
            string fileName = "CATRoutingArea_CRUD";
            List<string> lstContent = new List<string>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            CATRoutingArea item = null;
            CATRoutingArea item1 = null;
            int ID = 0;
            using (var bl = new BLCategory())
            {
                //Ham List ()
                lstContent.Add("Test method: CATRoutingArea_List");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                var lst = bl.CATRoutingArea_List(request);
                lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + "success");
                lstContent.Add("");

                //Ham Add ()
                lstContent.Add("Test method: CATRoutingArea_Save-Add");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                ID = bl.CATRoutingArea_Save(getDataCATRoutingArea());
                Assert.IsTrue(ID > 0);
                  lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (ID > 0 ? "success" : "fail"));
                lstContent.Add("");

                //Ham Get ()
                lstContent.Add("Test method: CATRoutingArea_Get");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item = bl.CATRoutingArea_Get(ID);
                Assert.IsTrue(item != null);
                  lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item != null ? "success" : "fail"));
                lstContent.Add("");

                //Ham Edit ()
                lstContent.Add("Test method: CATRoutingArea_Save-Edit");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                item.Code = "1ST01";
                bl.CATRoutingArea_Save(item);
                item1 = bl.CATRoutingArea_Get(ID);
                Assert.IsTrue(item.Code == item1.Code);
                  lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                lstContent.Add("Status: " + (item.Code == item1.Code ? "success" : "fail"));
                lstContent.Add("");

                //Ham Delete ()
                lstContent.Add("Test method: CATRoutingArea_Delete");
                lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                bl.CATRoutingArea_Delete(item1);
                item = bl.CATRoutingArea_Get(ID);
                Assert.IsTrue(item == null);
                  lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                  lstContent.Add("Status: " + (item == null ? "success" : "fail"));
                lstContent.Add("");

                LogResult(fileName, lstContent);
            }
        }
        //[TestMethod]
        //public void CATRoutingArea_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATRoutingArea_List(request);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATRoutingArea_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATRoutingArea_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATRoutingArea_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATRoutingAreaDetail_List()
        //{
        //    DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
        //    string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
        //    int areaID = -1;

        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.CATRoutingAreaDetail_List(request, areaID);
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATRoutingAreaDetail_Get()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATRoutingAreaDetail_Save()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        //[TestMethod]
        //public void CATRoutingAreaDetail_Delete()
        //{
        //    DateTime dtStart = DateTime.Now;
        //    using (var bl = new BLCategory())
        //    {
        //        var lst = bl.ALL_Country();
        //    }
        //    DateTime dtEnd = DateTime.Now;
        //    var sub = dtEnd - dtStart;
        //    Assert.IsTrue(sub.Seconds < MaxSec);
        //}

        [TestMethod]
        public void CATRoutingArea_Location_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int routingAreaID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATRoutingArea_Location_List(request, routingAreaID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATRoutingArea_LocationNotIn_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            int routingAreaID = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                var lst = bl.CATRoutingArea_LocationNotIn_List(request, routingAreaID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATRoutingArea_LocationNotIn_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATRoutingArea_Location_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATRoutingAreaLocation_Refresh()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATRoutingAreaExcel_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATRoutingAreaExcel_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                // var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void CATRouteAreaLocationExcel_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCategory())
            {
                //  var lst = bl.ALL_Country();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
        #endregion
    }
}
