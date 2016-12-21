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
    public class BLOperation_Test : BaseTest
    {
        [TestMethod]
        public void Vendor_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.Vendor_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void TractorByVendorID_List()
        {
            int? vendorid = null;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.TractorByVendorID_List(vendorid);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RomoocByVendorID_List()
        {
            int? vendorid = null;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.RomoocByVendorID_List(vendorid);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void TruckByVendorID_List()
        {
            int? vendorid = null;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.TruckByVendorID_List(vendorid);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void TenderCustomer_List()
        { 
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.TenderCustomer_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void RejectReason_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.RejectReason_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Appointment_Route_ListVendor()
        { 
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.Appointment_Route_ListVendor();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Appointment_Route_ListDriver()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.Appointment_Route_ListDriver();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Appointment_Route_ListCustomer()
        { 
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.Appointment_Route_ListCustomer( );
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Appointment_Route_ActivityGet()
        {
            int id = 0;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.Appointment_Route_ActivityGet(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Appointment_Route_ActivitySave()
        {
            
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Appointment_Route_ActivityDel()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
              
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Appointment_Route_MaterialList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.Appointment_Route_MaterialList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Appointment_Route_MaterialDetail()
        {
            List<int> lstid = new List<int>();
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.Appointment_Route_MaterialDetail(lstid);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Appointment_Route_MaterialSave()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_Tender_Update()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_TenderVendor_Update()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void Appointment_Route_FLMPlaning()
        { 
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.Appointment_Route_FLMPlaning();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Cancel()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_OrderList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_OrderList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_OrderDetail()
        {
            List<DTODIAppointmentOrder> lstid = new List<DTODIAppointmentOrder>();
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_OrderDetail(lstid);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_OrderDiv()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
               
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_OrderGroup()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_OrderDivCustomGet()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_OrderDivCustomSave()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
               
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_OrderDNCodeChange()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleTimeList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleTimeList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleDetail()
        {
            List<int> lstid = new List<int>();
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleDetail(lstid);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleAdd()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleGet()
        {
            DTODIAppointmentOrder item = new DTODIAppointmentOrder();
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleGet(item);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleTimeGet()
        {
            
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleTimeGet(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleRemove()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleSave()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleMonitor()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
               
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleTenderApproved()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleTender()
        {
            List<int> lstMasterID = new List<int>();
            List<DTODIAppointmentRouteTender> lstTender = new List<DTODIAppointmentRouteTender>();
            double RateTime = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleTender(lstMasterID,lstTender,RateTime);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleTenderReject()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleRemoveMonitor()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleRemoveMonitor(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleTimelineChange()
        {
            DTOFLMAssetTimeSheet source=new DTOFLMAssetTimeSheet();
            DTODIAppointmentOrder target = new DTODIAppointmentOrder();
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleTimelineChange(source,target);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleListVehicle()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleListVehicle();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleListGroupVehicle()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleListGroupVehicle();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleAddRate()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleTOVENList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleTOVENList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleTOVEN()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleTOVEN(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleTOVENInDate()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleTOVENInDate(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleTOVENGet()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_VehicleTOVENGet(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_VehicleSend()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_WinVehicleSave()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_PODList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_PODList(DateTime.Now,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_PODExcelSave()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_PODDiv()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_QuickSearch()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_QuickSearch(DateTime.Now,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_QuickSearchGet()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_QuickSearchGet(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_QuickSearchApproved()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_QuickSearchUnApproved()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_HasDNList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_HasDNList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_HasDNOrderList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_HasDNOrderList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_HasDNOrderListDN()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_HasDNOrderListDN( );
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_HasDNListGroupID()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_HasDNListGroupID(new List<int>{1,2,3});
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_HasDNOrderListByGroupID()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_HasDNOrderListByGroupID(new List<int> { 1, 2, 3 });
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_HasDNSave()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
               
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_HasDNDelete()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_HasDNApproved()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_HasDNUnApproved()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_NoDNList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_NoDNList(request,new List<int>{1,2,3},DateTime.Now,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_NoDNOrderList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_NoDNOrderList(request, new List<int> { 1, 2, 3 }, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_NoDNSave()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_FTL_NoDNList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_FTL_NoDNList(request, new List<int> { 1, 2, 3 }, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_FTL_NoDNOrderList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_FTL_NoDNOrderList(request, new List<int> { 1, 2, 3 }, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_FTL_NoDNSave()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_FTL_NoDNCancel()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_FTL_NoDNSplit()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_RouteDN_OrderList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_RouteDN_OrderList(request,  DateTime.Now, DateTime.Now,new List<int> { 1, 2, 3 },1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_RouteDN_OrderDNCodeChange()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_RouteDN_Delete()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_RouteDN_Revert()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_SendToTender()
        {
           List<int> lst=new List<int>();
           List<DTODIAppointmentRouteTender> lstTender = new List<DTODIAppointmentRouteTender>();
            double RateTime=1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst1 = bl.DIAppointment_Route_SendToTender(lst,lstTender,RateTime);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_TenderReject()
        {
            List<int> lst = new List<int>();
            DTODIAppointmentRouteTenderReject item = new DTODIAppointmentRouteTenderReject();
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst1 = bl.DIAppointment_Route_TenderReject(lst,item);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_TenderApproved()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_TenderSave()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_TenderRequestList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_TenderRequestList(request, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_TenderAcceptList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_TenderAcceptList(request, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_TenderRejectList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_TenderRejectList(request, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_TenderRequestOrderList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_TenderRequestOrderList(request, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_TenderAcceptOrderList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_TenderAcceptOrderList(request, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_TenderRejectOrderList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_TenderRejectOrderList(request, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_SendMailToTender()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_MasterList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_MasterList(request,new List<int>{1,2},DateTime.Now,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_Master_OfferPL()
        {
           DTODIAppointmentRate master=new DTODIAppointmentRate();
           List<DTODIAppointmentOrder> lstOrder = new List<DTODIAppointmentOrder>();
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_Master_OfferPL(master,lstOrder);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_Master_ChangeMode()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_Master_CheckDriver()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_BookingConfirmation_Read()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_BookingConfirmation_Read(request,1,DateTime.Now,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_BookingConfirmation()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_BookingConfirmation(new List<int>{1,2},DateTime.Now,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_Route_ExcelConfirm()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_Route_ExcelConfirm(DateTime.Now,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.COAppointment_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_Cancel()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
               
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_Container_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.COAppointment_2View_Container_List(request,new List<int>{1},DateTime.Now,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_ContainerHasMaster_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.COAppointment_2View_ContainerHasMaster_List(request, new List<int> { 1 }, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_Master_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.COAppointment_2View_Master_List(request, new List<int> { 1 }, DateTime.Now, DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_Master_PL()
        {
DTOOPSCOTOMaster objMaster=new DTOOPSCOTOMaster();
List<DTOOPSCOTOContainer> dataContainer = new List<DTOOPSCOTOContainer>();
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.COAppointment_2View_Master_PL(objMaster,dataContainer);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_Master_Update()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_Master_Delete()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_Master_ToMON()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_Master_ToOPS()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_Master_ToVendor()
        {
           List<int> dataMaster=new List<int>();
            List<DTODIAppointmentRouteTender> dataRate=new List<DTODIAppointmentRouteTender>();
            double rTime = 1;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.COAppointment_2View_Master_ToVendor(dataMaster,dataRate,rTime);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_Master_ToVendor_Email()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_2View_Master_CheckDriver()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {  
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_Tender_Master_Reject()
        {
             List<int> dataMaster=new List<int>(); DTOOPSReaonReject item=new DTOOPSReaonReject();
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.COAppointment_Tender_Master_Reject(dataMaster,item);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void COAppointment_Tender_Master_Accept()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_Tendering_Vendor_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_Tendering_Vendor_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Packet_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Packet_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_PacketGroupProduct_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_PacketGroupProduct_List(request,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_PacketGroupProduct_NotIn_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_PacketGroupProduct_NotIn_List(request,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_PacketRate_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_PacketRate_List(request,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_PacketDetail_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_PacketDetail_List(request, 1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Location_List()
        {
            List<int> dataExist = new List<int>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Setting_Location_List(request, dataExist);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Routing_List()
        {
            List<int> dataExist = new List<int>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Setting_Routing_List(request, dataExist);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_GroupLocation_List()
        {
            List<int> dataExist = new List<int>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Setting_GroupLocation_List(request, dataExist);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Customer_List()
        {
            List<int> dataExist = new List<int>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Setting_Customer_List(request, dataExist);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Service_List()
        {
            List<int> dataExist = new List<int>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                //var lst = bl.OPS_DI_Tendering_Setting_Service_List(request, dataExist);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Vendor_List()
        {
            List<int> dataExist = new List<int>();
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Setting_Vendor_List(request, dataExist);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Packet_Order_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Setting_Packet_Order_List(request,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

       
        [TestMethod]
        public void OPS_DI_Tendering_Packet_Get()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Packet_Get(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Setting_List(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Get()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Setting_Get(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Packet_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Tendering_Setting_Packet_List(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Packet_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_PacketGroupProduct_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_PacketGroupProduct_Remove()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_PacketRate_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_PacketRate_Remove()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Packet_Send()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Packet_Delete()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Delete()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Setting_Packet_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
               
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Tendering_Packet_CreateViaSetting()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_Tendering_Rate_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_VEN_Tendering_Rate_List(request,true);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_Tendering_GroupProduct_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_VEN_Tendering_GroupProduct_List(request,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_Tendering_Rate_Reject()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_Tendering_Rate_Accept()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_Tendering_Rate_AcceptPart()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_2View_GroupProduct_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_VEN_2View_GroupProduct_List(request,1,true);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_2View_TOMaster_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_VEN_2View_TOMaster_List(request,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_2View_Get()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_VEN_2View_Get(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_2View_TOMaster_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_2View_TOMaster_Delete()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_Tendering_Packet_Confirm()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_VEN_Tendering_Packet_AutoCheck()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
               
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                //var lst = bl.OPS_DI_Import_Packet_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_Setting_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Import_Packet_Setting_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_TOMaster_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Import_Packet_TOMaster_List(request,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_GroupProduct_ByMaster_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Import_Packet_GroupProduct_ByMaster_List(request,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_GroupProduct_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Import_Packet_GroupProduct_List(request,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_Setting_Get()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Import_Packet_Setting_Get(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_SettingPlan()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Import_Packet_SettingPlan( );
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_Get()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Import_Packet_Get(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_Data()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPS_DI_Import_Packet_Data(1,new List<string>{"cccc"});
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_Import()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_2View_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_ToOPS()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_Delete()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_DI_Import_Packet_Reset()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

       
         
        [TestMethod]
        public void OPSMasterTendered_AutoSendMail()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            { 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_RouteBarcode_SOList()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.DIAppointment_RouteBarcode_SOList("aa");
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void DIAppointment_RouteBarcode_SOSave()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Order_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                //var lst = bl.OPSCO_MAP_Order_List(request,1,null,null);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Tractor_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSCO_MAP_Tractor_List(request,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Romooc_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSCO_MAP_Romooc_List(request,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_TOMaster_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSCO_MAP_TOMaster_List(request,false,false,null,null);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Location_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSCO_MAP_Location_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Vendor_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSCO_MAP_Vendor_List( );
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Driver_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSCO_MAP_Driver_List( );
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_DriverVendor_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSCO_MAP_DriverVendor_List(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Setting()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSCO_MAP_Setting();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_TripByVehicle_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSCO_MAP_TripByVehicle_List(DateTime.Now,1,1,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_CheckVehicleAvailable()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                //var lst = bl.OPSCO_MAP_CheckVehicleAvailable(1,1,1,DateTime.Now,DateTime.Now,10);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Schedule_Data()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSCO_MAP_Schedule_Data(new List<int>());
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_ToMON()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_ToOPS()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Cancel()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_UpdateAndToMON()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_ToVendor()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_Delete()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_COTOContainer_Split()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_MAP_COTOContainer_Split_Cancel()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSCO_CheckLocationRequired()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_GetLocationRequired()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPS_CheckingTime()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_Order_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                //var lst = bl.OPSDI_MAP_Order_List(request,1,null,null);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_Vehicle_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSDI_MAP_Vehicle_List(request,DateTime.Now);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_VehicleVendor_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSDI_MAP_VehicleVendor_List(request,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_TOMaster_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSDI_MAP_TOMaster_List(request,false,false,null,null);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_TripByID()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSDI_MAP_TripByID(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_TripByVehicle_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSDI_MAP_TripByVehicle_List(DateTime.Now,1,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_CheckVehicleAvailable()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSDI_MAP_CheckVehicleAvailable(1,1,DateTime.Now,DateTime.Now,1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_Schedule_Data()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            List<int> dataVehicle = new List<int>();
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSDI_MAP_Schedule_Data(dataVehicle);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_GroupByTrip_List()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                var lst = bl.OPSDI_MAP_GroupByTrip_List(1);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_Save()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_Map_Update()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_ToMON()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_ToOPS()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_Cancel()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_UpdateAndToMON()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_ToVendor()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_Delete()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
               
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_GroupProduct_Split()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_GroupProduct_Split_Cancel()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
               
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_CheckLocationRequired()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_FTL_Split()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }

        [TestMethod]
        public void OPSDI_MAP_Vehicle_New()
        {
            DataSourceRequest objRequest = new DataSourceRequest { Page = 1, PageSize = 100 };
            string request = Newtonsoft.Json.JsonConvert.SerializeObject(objRequest);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLOperation())
            {
                 
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < MaxSec);
        }
       
    }
}
