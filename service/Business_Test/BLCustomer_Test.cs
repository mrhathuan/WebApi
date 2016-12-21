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
    public class BLCustomer_Test : BaseTest
    {
        public int customerId = 4;//Công ty bia

        #region Customer
        [TestMethod]
        public void Customer_List()
        {
            string fileName = "Customer_List";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Customer_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            using (var bl = new BLCustomer())
            {
                bl.Account_Setting();
            }

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.Customer_List(request);
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
        public void CustomerGetByID()
        {
            string fileName = "CustomerGetByID";
            List<string> lstContent = new List<string>();

            lstContent.Add("Test method: CustomerGetByID");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            CUSCustomer item = null;
            using (var bl = new BLCustomer())
            {
                item = bl.CustomerGetByID(customerId);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        //constructor
        private CUSCustomer CustomerConstructor()
        {
            CUSCustomer item = new CUSCustomer();
            item.Code = "DTest_Customer_Save_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.CustomerName = "DTest_Customer_Save_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.ShortName = "DTest";

            return item;
        }

        [TestMethod]
        public void Customer_Save()
        {
            string fileName = "Customer_Save";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Customer_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            //constructor
            CUSCustomer item = CustomerConstructor();

            using (var bl = new BLCustomer())
            {
                item.ID = bl.Customer_Save(item);
                bl.Customer_Delete(item);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void Customer_Delete()
        {
            string fileName = "Customer_Delete";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Customer_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            //constructor
            CUSCustomer item = CustomerConstructor();
            var id = -1;
            using (var bl = new BLCustomer())
            {
                item.ID = bl.Customer_Save(item);
                bl.Customer_Delete(item);
                id = item.ID;
                item = null;

                item = bl.CustomerGetByID(id);
            }

            Assert.IsTrue(item.ID == 0 && id > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + ((item.ID == 0 && id > 0) ? "success" : "fail"));
            lstContent.Add("");
            LogResult(fileName, lstContent);
        }
        #endregion

        #region Contact
        private CUSContact ContactConstructor()
        {
            CUSContact item = new CUSContact();
            item.FirstName = "Contact_Save_FN";
            item.LastName = "Contact_Save_LN";

            return item;
        }
        [TestMethod]
        public void Contact_Get()
        {
            string fileName = "Contact_Get";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Contact_Get");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var id = 2;
            CUSContact item = null;
            using (var bl = new BLCustomer())
            {
                item = bl.Contact_Get(id);
            }
            Assert.IsTrue(item != null);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item != null ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void Contact_List()
        {
            string fileName = "Contact_List";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Contact_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.Contact_List(request, customerId);
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
        public void Contact_Save()
        {
            string fileName = "Contact_Save";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Contact_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            //constructor
            CUSContact item = ContactConstructor();

            using (var bl = new BLCustomer())
            {
                item.ID = bl.Contact_Save(item, customerId);
                bl.Contact_Delete(item.ID);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);

            
        }

        [TestMethod]
        public void Contact_Delete()
        {
            string fileName = "Contact_Delete";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Contact_Delete");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            //constructor
            CUSContact item = ContactConstructor();
            var id = -1;
            using (var bl = new BLCustomer())
            {
                item.ID = bl.Contact_Save(item, customerId);
                bl.Contact_Delete(item.ID);
                id = item.ID;
                item = null;

                item = bl.Contact_Get(id);
            }
            Assert.IsTrue((item == null || item.ID > 0) && id > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (((item == null || item.ID > 0) && id > 0) ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);

        }
        #endregion

        #region GroupOfProduct
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
        public void GroupOfProduct_Get()
        {
            string fileName = "GroupOfProduct_Get";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: GroupOfProduct_Get");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var id = 1;
            DTOCUSGroupOfProduct item = null;
            using (var bl = new BLCustomer())
            {
                item = bl.GroupOfProduct_Get(id);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void GroupOfProductAll_List()
        {
            Kendo.Mvc.UI.DataSourceRequest request = new Kendo.Mvc.UI.DataSourceRequest();
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
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
            var cusId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.GroupOfProduct_List(request, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
           
        }

        [TestMethod]
        public void GroupOfProduct_Save()
        {
            string fileName = "GroupOfProduct_Save";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: GroupOfProduct_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOCUSGroupOfProduct item = GOPConstructor();
            using (var bl = new BLCustomer())
            {
                item.ID = bl.GroupOfProduct_Save(item, customerId);
                bl.GroupOfProduct_Delete(item);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void GroupOfProduct_Delete()
        {
            string fileName = "GroupOfProduct_Delete";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: GroupOfProduct_Delete");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            //constructor
            DTOCUSGroupOfProduct item = GOPConstructor();
            var id = -1;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.GroupOfProduct_Save(item, customerId);
                bl.GroupOfProduct_Delete(item);
                id = item.ID;
                item = null;

                item = bl.GroupOfProduct_Get(id);
            }
            Assert.IsTrue((item == null || item.ID == 0) && id > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (((item == null || item.ID == 0) && id > 0) ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);

        }

        [TestMethod]
        public void GroupOfProduct_GetByCode()
        {
            string fileName = "GroupOfProduct_GetByCode";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: GroupOfProduct_GetByCode");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var code = "Beer";
            var cusId = 4;
            CUSGroupOfProduct item = null;
            using (var bl = new BLCustomer())
            {
                item = bl.GroupOfProduct_GetByCode(code, cusId);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void GroupOfProduct_ResetPrice()
        {
            string fileName = "GroupOfProduct_ResetPrice";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: GroupOfProduct_ResetPrice");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                bl.GroupOfProduct_ResetPrice(customerId);
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

        #region Product
        private DTOCUSProduct ProductConstructor()
        {
            DTOCUSProduct item = new DTOCUSProduct();
            item.Code = "Code_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.ProductName = "ProductName_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.IsKg = false;
            item.IsDefault = false;

            return item;
        }

        [TestMethod]
        public void Product_Get()
        {
            string fileName = "Product_Get";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Product_Get");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var id = 1;
            DTOCUSProduct item = null;
            using (var bl = new BLCustomer())
            {
                item = bl.Product_Get(id);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void Product_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var gopId = 3;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.Product_List(request, gopId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Product_Save()
        {
            string fileName = "Product_Save";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Product_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            //constructor
            DTOCUSProduct item = ProductConstructor();
            var gopId = 1;
            using (var bl = new BLCustomer())
            {
                item.ID = bl.Product_Save(item, gopId);
                bl.Product_Delete(item);
            }

            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);

        }

        [TestMethod]
        public void Product_Delete()
        {
            string fileName = "Product_Delete";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Product_Delete");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            //constructor
            DTOCUSProduct item = ProductConstructor();
            var gopId = 1;
            var id = -1;
            using (var bl = new BLCustomer())
            {
                item.ID = bl.Product_Save(item, gopId);
                bl.Product_Delete(item);
                id = item.ID;
                item = null;

                item = bl.Product_Get(id);
            }

            Assert.IsTrue((item == null || item.ID == 0) && id > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (((item == null || item.ID == 0) && id > 0) ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);

        }

        [TestMethod]
        public void CUS_Product_Export()
        {
            var cusId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUS_Product_Export(cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUS_Product_Check()
        {
            var cusId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                bl.CUS_Product_Check(cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUS_Product_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Stock
        private DTOCUSStock StockConstructor()
        {
            DTOCUSStock item = new DTOCUSStock();
            item.Code = "Code_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.LocationName = "Name_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.CountryID = 1;
            item.ProvinceID = 1;
            item.DistrictID = 1;

            return item;
        }
        [TestMethod]
        public void Stock_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var cusId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.Stock_List(request, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void StockNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var cusId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.StockNotIn_List(request, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Stock_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Stock_Save()
        {
            string fileName = "Stock_Save";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Stock_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOCUSStock item = StockConstructor();

            using (var bl = new BLCustomer())
            {
                item = bl.Stock_Save(item, customerId);
                bl.Stock_Delete(item);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void Stock_GetByID()
        {
            string fileName = "Stock_GetByID";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Stock_GetByID");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var stockId = 98;
            var cusId = 4;
            DTOCUSStock item = new DTOCUSStock();
            using (var bl = new BLCustomer())
            {
                item = bl.Stock_GetByID(stockId, cusId);
            }
            Assert.IsTrue(item != null && item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + ((item != null && item.ID > 0) ? "success" : "fail"));
            lstContent.Add("");
            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void Stock_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Stock_Delete()
        {
            string fileName = "Stock_Delete";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: Stock_Delete");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOCUSStock item = StockConstructor();
            var id = -1;

            using (var bl = new BLCustomer())
            {
                item = bl.Stock_Save(item, customerId);
                bl.Stock_Delete(item);
                id = item.ID;
                item = null;

                item = bl.Stock_GetByID(id, customerId);
            }
            Assert.IsTrue((item == null || item.ID == 0) && id > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (((item == null || item.ID == 0) && id > 0) ? "success" : "fail"));
            lstContent.Add("");
            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void AddressSearch_List()
        {
            var id = 98;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.AddressSearch_List(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        #endregion

        #region routing area (kho+ đối tác)
        [TestMethod]
        public void Customer_Location_RoutingAreaList()
        {
            var locationId = 1046;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.Customer_Location_RoutingAreaList(locationId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Customer_Location_RoutingAreaNotInList()
        {
            var locationId = 1046;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.Customer_Location_RoutingAreaList(locationId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Customer_Location_RoutingAreaNotInSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Customer_Location_RoutingAreaNotInDeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region GroupOfProduct In Stock
        [TestMethod]
        public void GroupOfProductInStock_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var stockId = 98;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.GroupOfProductInStock_List(request, stockId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void GroupOfProductNotInStock_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var stockId = 98;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.GroupOfProductInStock_List(request, stockId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void GroupOfProductNotInStock_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void GroupOfProductInStock_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
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
            var cusId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.Routing_List(request, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Routing_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void RoutingNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var cusId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.RoutingNotIn_List(request, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void RoutingNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Routing_Reset()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region SYSSetting
        [TestMethod]
        public void CUSSettingInfo_Get()
        {
            string fileName = "CUSSettingInfo_Get";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSSettingInfo_Get");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var cusId = 4;
            DTOCUSSettingInfo item = new DTOCUSSettingInfo();
            using (var bl = new BLCustomer())
            {
                item = bl.CUSSettingInfo_Get(cusId);
            }
            Assert.IsTrue(item != null);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item != null ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingInfo_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSSettingInfo_LocationList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSSettingInfo_LocationList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Đối tác
        [TestMethod]
        public void Partner_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var typeofpartnerID = -(int)SYSVarType.TypeOfPartnerDistributor;
            var cusId = 19;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.Partner_List(request, cusId, typeofpartnerID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerNotIn_List()
        {
            //var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            //var cusId = 19;
            //bool isCarrier = true, isSeaport = false, isDistributor = false;
            //DateTime dtStart = DateTime.Now;
            //using (var bl = new BLCustomer())
            //{
            //    var lst = bl.PartnerNotIn_List(request, cusId, isCarrier, isSeaport, isDistributor);
            //}
            //DateTime dtEnd = DateTime.Now;
            //var sub = dtEnd - dtStart;
            //Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Partner_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Partner_GetByID()
        {
        //    var id = 1022;
        //    DTOCUSPartnerAllCustom item = null;
        //    using (var bl = new BLCustomer())
        //    {
        //        item = bl.Partner_GetByID(id);
        //    }
        //    Assert.IsTrue(item != null);
        }

        [TestMethod]
        public void Partner_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void Partner_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var partnerId = 1022;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                //var lst = bl.PartnerLocation_List(partnerId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_NotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var partnerId = 1022;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.PartnerLocation_NotInList(request, partnerId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_SaveNotinList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_GetByID()
        {
            //var id = 98;
            //DateTime dtStart = DateTime.Now;
            //using (var bl = new BLCustomer())
            //{
            //    var obj = bl.PartnerLocation_GetByID(id);
            //}
            //DateTime dtEnd = DateTime.Now;
            //var sub = dtEnd - dtStart;
            //Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_Export()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var cusId = 19;
            bool isCarrier = true, isSeaport = false, isDistributor = false;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.PartnerLocation_Export(cusId, isCarrier, isSeaport, isDistributor);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_Check()
        {
            var cusId = 19;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.PartnerLocation_Check(cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PartnerLocation_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region CUSSettingORD
        [TestMethod]
        public void CUSSettingOrder_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSSettingOrder_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSSettingOrder_Get()
        {
            string fileName = "CUSSettingOrder_Get";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSSettingOrder_Get");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var id = 79;
            DTOCUSSettingOrder item = null;
            using (var bl = new BLCustomer())
            {
                item = bl.CUSSettingOrder_Get(id);
            }
            Assert.IsTrue(item != null || item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + ((item != null || item.ID > 0) ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingOrder_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSSettingOrder_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSSettingORD_StockByCus_List()
        {
            var cusId = 22;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSSettingORD_StockByCus_List(cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region CUSSettingPlan
        [TestMethod]
        public void CUSSettingPlan_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSSettingPlan_Get()
        {
            string fileName = "CUSSettingPlan_Get";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSSettingPlan_Get");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var id = 152;
            DTOCUSSettingPlan item = null;
            using (var bl = new BLCustomer())
            {
                item = bl.CUSSettingPlan_Get(id);
            }
            Assert.IsTrue(item != null && item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + ((item != null && item.ID > 0) ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSSettingPlan_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSSettingPlan_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSSettingPlan_SettingOrderList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSSettingPlan_SettingOrderList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Cussetting order code
        [TestMethod]
        public void CUSSettingOrderCode_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSSettingOrderCode_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSSettingOrderCode_Get()
        {
            var id = 1194;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSSettingOrderCode_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSSettingOrderCode_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSSettingOrderCode_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region CUSContract
        #region Common
        [TestMethod]
        public void CUSContract_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            using (var bl = new BLCustomer())
            {
                bl.Account_Setting();
            }

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_List(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Get()
        {
            string fileName = "CUSContract_Get";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSContract_Get");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var id = 6;

            DTOCATContract item = null;
            using (var bl = new BLCustomer())
            {
                item = bl.CUSContract_Get(id);
            }
            Assert.IsTrue(item != null && item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + ((item != null && item.ID > 0) ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        private DTOCATContract ContractConstructor()
        {
            DTOCATContract item = new DTOCATContract();
            item.ContractNo = "ContractNo_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.DisplayName = "DisplayName_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.CustomerID = customerId;
            item.EffectDate = DateTime.Now;
            item.ExpiredDate = DateTime.Now.AddDays(7);
            item.TypeOfContractDateID = 74;

            return item;
        }

        [TestMethod]
        public void CUSContract_Save()
        {
            string fileName = "CUSContract_Save";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSContract_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOCATContract item = ContractConstructor();
            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSContract_Save(item);
                bl.CUSContract_Delete(item.ID);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSContract_Delete()
        {
            string fileName = "CUSContract_Delete";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSContract_Delete");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOCATContract item = ContractConstructor();
            var id = -1;
            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSContract_Save(item);
                bl.CUSContract_Delete(item.ID);
                id = item.ID;
                item = null;

                item = bl.CUSContract_Get(id);
            }
            Assert.IsTrue((item == null || item.ID == 0) && id > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (((item == null || item.ID == 0) && id > 0) ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSContract_Data()
        {
            string fileName = "CUSContract_Delete";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSContract_Delete");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            var id = 6;
            DTOCUSContract_Data item = null;

            using (var bl = new BLCustomer())
            {
                item = bl.CUSContract_Data(id);
            }
            Assert.IsTrue(item != null);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item != null  ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSContract_ByCustomerList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var cusId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_ByCustomerList(request, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_ByCompanyList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var cusId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_ByCompanyList(request, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region CODefault
        [TestMethod]
        public void CUSContract_CODefault_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 2;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_CODefault_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_CODefault_Update()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Routing
        [TestMethod]
        public void CUSContract_Routing_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_Routing_List(contractId, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Routing_Save()
        {
            string fileName = "CUSContract_Routing_Save";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSContract_Routing_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOCATContractRouting item = new DTOCATContractRouting();
            item.ID = 2654;
            item.ContractID = 6;
            item.Code = "BA-KH";
            item.CATCode = "BA-KH";
            item.ContractTermID = 1166;
            item.SortOrder = 2;
            item.RoutingID = 557;
            item.RoutingName = "Kho Bình An-Khánh Hòa";

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                bl.CUSContract_Routing_Save(item);
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
        public void CUSContract_Routing_LeadTime_Check()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Routing_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Routing_NotIn_Delete()
        {
            DTOCATRouting item = CATRoutingConstructor();
            var contractId = 6;
            var id = -1;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSContract_NewRouting_Save(item, contractId);
                bl.CUSContract_Routing_NotIn_Delete(item.ID, contractId);
                id = item.ID;
                item = null;

                item = bl.CUSContract_NewRouting_Get(id);
            }
            Assert.IsTrue((item == null || item.ID == 0) && id > 0);
        }

        [TestMethod]
        public void CUSContract_Routing_NotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_Routing_NotIn_List(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Routing_CATNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_Routing_CATNotIn_List(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Routing_CATNotIn_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Routing_Insert()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Routing_Export()
        {
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_Routing_Export(contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Routing_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_RoutingByCus_List()
        {
            var cusId = 4;
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                bl.CUSContract_RoutingByCus_List(cusId, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_KPI_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_KPI_Routing_List()
        {
            var contractId = 43;
            var routingId = 770;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_KPI_Routing_List(request, contractId, routingId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_KPI_Check_Expression()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_KPI_Check_Hit()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_KPI_Routing_Apply()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Routing_ContractTermList()
        {
            var contractId = 8;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_Routing_ContractTermList(contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        #region tạo mới cung duong trong hợp đồng
        [TestMethod]
        public void CUSContract_NewRouting_Get()
        {
            var id = 72;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSContract_NewRouting_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        private DTOCATRouting CATRoutingConstructor()
        {
            DTOCATRouting item = new DTOCATRouting();
            item.Code = "Code_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.RoutingName = "RoutingName_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.LocationFromID = 1046;
            item.LocationToID = 1051;

            return item;
        }
        [TestMethod]
        public void CUSContract_NewRouting_Save()
        {
            DTOCATRouting item = CATRoutingConstructor();
            var contractId = 6;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSContract_NewRouting_Save(item, contractId);
                bl.CUSContract_Routing_NotIn_Delete(item.ID, contractId);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void CUSContract_NewRouting_LocationList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_NewRouting_LocationList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_NewRouting_AreaList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_NewRouting_AreaList(request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_NewRouting_AreaGet()
        {
            var id = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSContract_NewRouting_AreaGet(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_NewRouting_AreaSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_NewRouting_AreaDelete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_NewRouting_AreaRefresh()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_NewRouting_AreaDetailList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var areaId = 12;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_NewRouting_AreaDetailList(request, areaId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_NewRouting_AreaDetailGet()
        {
            var id = 75;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSContract_NewRouting_AreaDetailGet(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_NewRouting_AreaDetailSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_NewRouting_AreaDetailDelete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #endregion

        #region Price

        #region Common
        [TestMethod]
        public void CUSContract_Price_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractTermId = 8;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_Price_List(request, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        private DTOPrice PriceConstructor()
        {
            DTOPrice item = new DTOPrice();
            item.Code = "Code_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.Name = "Name_" + DateTime.Now.ToString("yyyyMMddHHmmss");
            item.EffectDate = DateTime.Now;
            item.TypeOfOrderID = 20;
            return item;
        }

        [TestMethod]
        public void CUSContract_Price_Save()
        {
            string fileName = "CUSContract_Price_Save";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSContract_Price_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOPrice item = PriceConstructor();
            var termId = 1166;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSContract_Price_Save(item, termId);
                bl.CUSContract_Price_Delete(item.ID);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);

        }

        [TestMethod]
        public void CUSContract_Price_Copy()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Price_Delete()
        {
            string fileName = "CUSContract_Price_Delete";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSContract_Price_Delete");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOPrice item = PriceConstructor();
            var termId = 1166;
            var id = -1;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSContract_Price_Save(item, termId);
                bl.CUSContract_Price_Delete(item.ID);
                id = item.ID;
                item = null;

                item = bl.CUSContract_Price_Get(id);
            }
            Assert.IsTrue((item == null || item.ID <= 0) && id > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (((item == null || item.ID <= 0) && id > 0) ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void CUSContract_Price_Data()
        {
            var contractTermId = 8;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSContract_Price_Data(contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Price_Get()
        {
            string fileName = "CUSContract_Price_Get";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: CUSContract_Price_Get");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DTOPrice item = null;
            var id = 1;
            using (var bl = new BLCustomer())
            {
                item = bl.CUSContract_Price_Get(id);
            }
            Assert.IsTrue(item.ID > 0);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + (item.ID > 0 ? "success" : "fail"));
            lstContent.Add("");

            LogResult(fileName, lstContent);

        }

        [TestMethod]
        public void CUSContract_Price_DeletePriceNormal()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Price_DeletePriceLevel()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_PriceCO_Data()
        {
            var contractTermId = 2;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSContract_PriceCO_Data(contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        #endregion

        #region DI_GroupVehicle

        #region ftl normal
        [TestMethod]
        public void CUSPrice_DI_GroupVehicle_GetData()
        {
            var id = 1314;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_GroupVehicle_GetData(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_GroupVehicle_SaveList()
        {
            int priceID = 3393;
            int termId = 1166;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var data = bl.CUSContract_Price_Data(termId);

                List<DTOPriceGroupVehicle> dataPrice = new List<DTOPriceGroupVehicle>();
                foreach (var route in data.ListRouting)
                {
                    foreach (var gov in data.ListGroupOfVehicle)
                    {
                        DTOPriceGroupVehicle item = new DTOPriceGroupVehicle();
                        item.RouteID = route.ID;
                        item.GroupOfVehicleID = gov.ID;
                        item.Price = 1;
                        item.PriceMin = 1;
                        item.PriceMax = 1;

                        dataPrice.Add(item);
                    }
                }

                bl.CUSPrice_DI_GroupVehicle_SaveList(dataPrice, priceID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_GroupVehicle_ExcelData()
        {
            var id = 1314;
            var contractTermId = 1298;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_GroupVehicle_ExcelData(id, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_GroupVehicle_ExcelImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        #endregion
        #region ftl lv
        [TestMethod]
        public void CUSPrice_DI_PriceGVLevel_DetailData()
        {
            var id = 1353;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_PriceGVLevel_DetailData(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceGVLevel_Save()
        {
            int priceID = 3394;
            int termId = 25;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var data = bl.CUSContract_Price_Data(termId);

                List<DTOPriceGVLevelGroupVehicle> lst = new List<DTOPriceGVLevelGroupVehicle>();
                foreach (var route in data.ListRouting)
                {
                    foreach (var level in data.ListLevel)
                    {
                        var item = new DTOPriceGVLevelGroupVehicle();
                        item.ContractLevelID = level.ID;
                        item.RoutingID = route.ID;
                        item.Price = 1;
                        item.PriceMin = 1;
                        item.PriceMax = 1;

                        lst.Add(item);
                    }
                }

                bl.CUSPrice_DI_PriceGVLevel_Save(lst, priceID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceGVLevel_ExcelData()
        {
            var id = 1353;
            var contractTermId = 1187;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_PriceGVLevel_ExcelData(id, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceGVLevel_ExcelImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #endregion

        #region DI_GroupProduct
        [TestMethod]
        public void CUSPrice_DI_GroupProduct_Data()
        {
            var id = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_GroupProduct_Data(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_GroupProduct_SaveList()
        {
            int priceID = 3395;
            int termId = 1167;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var data = bl.CUSContract_Price_Data(termId);

                List<DTOPriceDIGroupOfProduct> lst = new List<DTOPriceDIGroupOfProduct>();
                foreach (var route in data.ListRouting)
                {
                    foreach (var gop in data.ListGroupOfProduct)
                    {
                        var item = new DTOPriceDIGroupOfProduct();

                        item.ContractRoutingID = route.ID;
                        item.GroupOfProductID = gop.ID;
                        item.Price = 1;
                        item.PriceMin = 1;
                        item.PriceMax = 1;

                        lst.Add(item);
                    }
                }

                bl.CUSPrice_DI_GroupProduct_SaveList(lst, priceID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_GroupProduct_Export()
        {
            var id = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_GroupProduct_Export(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_GroupProduct_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Loading Up

        #region common
        [TestMethod]
        public void CUSPrice_DI_Load_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Load_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Load_DeleteAllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Location
        [TestMethod]
        public void CUSPrice_DI_LoadLocation_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_LoadLocation_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadLocation_SaveList()
        {
            var priceID = 3396;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var loadingLst = bl.CUSPrice_DI_LoadLocation_List(priceID);

                foreach (var location in loadingLst)
                {
                    foreach (var price in location.ListPriceTruckLoadingDetail)
                    {
                        price.Price = 1;
                    }
                }

                bl.CUSPrice_DI_LoadLocation_SaveList(loadingLst);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadLocation_LocationNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_LoadLocation_LocationNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadLocation_LocationNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Route
        [TestMethod]
        public void CUSPrice_DI_LoadRoute_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_LoadRoute_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadRoute_SaveList()
        {
            var priceID = 3396;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var loadingLst = bl.CUSPrice_DI_LoadRoute_List(priceID);

                foreach (var item in loadingLst)
                {
                    foreach (var price in item.ListPriceTruckLoadingDetail)
                    {
                        price.Price = 1;
                    }
                }

                bl.CUSPrice_DI_LoadRoute_SaveList(loadingLst);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadRoute_RouteNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_LoadRoute_RouteNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadRoute_RouteNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Partner
        [TestMethod]
        public void CUSPrice_DI_LoadPartner_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_LoadPartner_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadPartner_SaveList()
        {
            var priceID = 3396;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var loadingLst = bl.CUSPrice_DI_LoadPartner_List(priceID);

                foreach (var item in loadingLst)
                {
                    foreach (var price in item.ListPriceTruckLoadingDetail)
                    {
                        price.Price = 1;
                    }
                }

                bl.CUSPrice_DI_LoadPartner_SaveList(loadingLst);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadPartner_PartnerNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_LoadPartner_PartnerNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadPartner_PartnerNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_LoadPartner_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #endregion

        #region Unload
        #region common
        [TestMethod]
        public void CUSPrice_DI_UnLoad_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoad_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoad_DeleteAllList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Location
        [TestMethod]
        public void CUSPrice_DI_UnLoadLocation_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_UnLoadLocation_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadLocation_SaveList()
        {
            var priceID = 3396;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var loadingLst = bl.CUSPrice_DI_UnLoadLocation_List(priceID);

                foreach (var item in loadingLst)
                {
                    foreach (var price in item.ListPriceTruckLoadingDetail)
                    {
                        price.Price = 1;
                    }
                }

                bl.CUSPrice_DI_UnLoadLocation_SaveList(loadingLst);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadLocation_LocationNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_UnLoadLocation_LocationNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadLocation_LocationNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Route
        [TestMethod]
        public void CUSPrice_DI_UnLoadRoute_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_UnLoadRoute_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadRoute_SaveList()
        {
            var priceID = 3396;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var loadingLst = bl.CUSPrice_DI_UnLoadRoute_List(priceID);

                foreach (var item in loadingLst)
                {
                    foreach (var price in item.ListPriceTruckLoadingDetail)
                    {
                        price.Price = 1;
                    }
                }

                bl.CUSPrice_DI_UnLoadRoute_SaveList(loadingLst);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadRoute_RouteNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_UnLoadRoute_RouteNotIn_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadRoute_RouteNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #region Partner
        [TestMethod]
        public void CUSPrice_DI_UnLoadPartner_List()
        {
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_UnLoadPartner_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadPartner_SaveList()
        {
            var priceID = 3396;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var loadingLst = bl.CUSPrice_DI_UnLoadPartner_List(priceID);

                foreach (var item in loadingLst)
                {
                    foreach (var price in item.ListPriceTruckLoadingDetail)
                    {
                        price.Price = 1;
                    }
                }

                bl.CUSPrice_DI_UnLoadPartner_SaveList(loadingLst);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadPartner_PartnerNotIn_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_UnLoadPartner_PartnerNotIn_List(request,priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadPartner_PartnerNotIn_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_UnLoadPartner_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #endregion

        #region price ex new
        #region info
        [TestMethod]
        public void CUSPrice_DI_Ex_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1356;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_Get()
        {
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        private DTOPriceDIEx Price_DI_ExConstructor()
        {
            DTOPriceDIEx item = new DTOPriceDIEx();
            item.TypeOfPriceDIExID = 1;
            item.DIExSumID = 318;
            item.Note = "TestName";

            return item;
        }
        [TestMethod]
        public void CUSPrice_DI_Ex_Save()
        {
            int priceID = 3396;
            DTOPriceDIEx item = Price_DI_ExConstructor();

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSPrice_DI_Ex_Save(item, priceID);
                bl.CUSPrice_DI_Ex_Delete(item.ID);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_Delete()
        {
            int priceID = 3396;
            DTOPriceDIEx item = Price_DI_ExConstructor();
            var id = -1;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSPrice_DI_Ex_Save(item, priceID);
                bl.CUSPrice_DI_Ex_Delete(item.ID);
                id = item.ID;
                item = null;

                item = bl.CUSPrice_DI_Ex_Get(id);
            }

            Assert.IsTrue((item == null || item.ID <= 0) && id > 0);
        }
        #endregion

        #region price di ex GroupLocation
        [TestMethod]
        public void CUSPrice_DI_Ex_GroupLocation_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_GroupLocation_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_GroupLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_GroupLocation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_GroupLocation_GroupNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_GroupLocation_GroupNotInList(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price di ex GroupProduct
        [TestMethod]
        public void CUSPrice_DI_Ex_GroupProduct_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_GroupProduct_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_GroupProduct_Get()
        {
            var id = 1168;
            var cusId = 4;
            DTOPriceDIExGroupProduct item = new DTOPriceDIExGroupProduct();

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                item = bl.CUSPrice_DI_Ex_GroupProduct_Get(id, cusId);
            }

            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_GroupProduct_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_GroupProduct_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_GroupProduct_GOPList()
        {
            var cusId = 4;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_GroupProduct_GOPList(cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price di ex Location
        [TestMethod]
        public void CUSPrice_DI_Ex_Location_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_Location_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_Location_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_Location_Get()
        {
            var id = 12;
            DTOPriceDIExLocation item = new DTOPriceDIExLocation();

            using (var bl = new BLCustomer())
            {
                item = bl.CUSPrice_DI_Ex_Location_Get(id);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_Location_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_Location_LocationNotInSaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_Location_LocationNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;
            var cusId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_Location_LocationNotInList(request, id, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price di ex ruote
        [TestMethod]
        public void CUSPrice_DI_Ex_Route_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_Route_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_Route_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_Route_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_Route_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;
            var contractTermId = 1349;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_Route_RouteNotInList(request, id, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price di ex parent route
        [TestMethod]
        public void CUSPrice_DI_Ex_ParentRoute_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_ParentRoute_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_ParentRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_ParentRoute_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_Ex_ParentRoute_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 1174;
            var contractTermId = 1349;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_Ex_ParentRoute_RouteNotInList(request, id, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #endregion

        #region MOQ new

        #region info
        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1378;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Get()
        {
            var moqId = 1030;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_PriceMOQ_Get(moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        private DTOCATPriceDIMOQ Price_DI_PriceMOQConstructor()
        {
            DTOCATPriceDIMOQ item = new DTOCATPriceDIMOQ();
            item.MOQName = "Test_Price_DI_PriceMOQ";
            item.DIMOQSumID = 306;
            item.TypeOfPriceDIExID = 1;
            return item;
        }
        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Save()
        {
            int priceID = 3396;
            DTOCATPriceDIMOQ item = Price_DI_PriceMOQConstructor();

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSPrice_DI_PriceMOQ_Save(item, priceID);
                bl.CUSPrice_DI_PriceMOQ_Delete(item.ID);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Delete()
        {
            int priceID = 3396;
            DTOCATPriceDIMOQ item = Price_DI_PriceMOQConstructor();
            var id = -1;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSPrice_DI_PriceMOQ_Save(item, priceID);
                bl.CUSPrice_DI_PriceMOQ_Delete(item.ID);
                id = item.ID;
                item = null;

                item = bl.CUSPrice_DI_PriceMOQ_Get(id);
            }
            Assert.IsTrue((item == null || item.ID <= 0) && id > 0);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Group Location
        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_GroupLocation_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 23;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_GroupLocation_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_GroupLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_GroupLocation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_GroupLocation_GroupNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 23;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price moq GroupProduct
        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_GroupProduct_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 23;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_GroupProduct_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_GroupProduct_Get()
        {
            var id = 14;
            var cusId = 76;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_PriceMOQ_GroupProduct_Get(id, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_GroupProduct_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_GroupProduct_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_GroupProduct_GOPList()
        {
            var cusId = 76;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_GroupProduct_GOPList(cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price moq Location
        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Location_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var id = 23;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_Location_List(request, id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Location_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Location_Get()
        {
            var id = 56;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_PriceMOQ_Location_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Location_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Location_LocationNotInSaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Location_LocationNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;
            var cusId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_Location_LocationNotInList(request, moqId, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price di moq ruote
        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Route_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_Route_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Route_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Route_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_Route_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;
            var contractTermId = 1264;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_Route_RouteNotInList(request, moqId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price di moq parent route
        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_ParentRoute_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_ParentRoute_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_ParentRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_ParentRoute_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQ_ParentRoute_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;
            var contractTermId = 1264;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(request, moqId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #endregion

        #region CO_Packing
        [TestMethod]
        public void CUSPrice_CO_COPackingPrice_List()
        {
            var priceId = 1225;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_CO_COPackingPrice_List(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_COPackingPrice_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_COPackingPrice_Export()
        {
            var priceId = 1225;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_CO_COPackingPrice_Export(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_COPackingPrice_Import()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region CO_Service
        [TestMethod]
        public void CUSPrice_CO_Service_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1225;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_CO_Service_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_ServicePacking_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1225;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_CO_ServicePacking_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_Service_Get()
        {
            var id = 35;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_CO_Service_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_ServicePacking_Get()
        {
            var id = 35;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_CO_ServicePacking_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        private DTOCATPriceCOService Price_CO_ServiceConstructor()
        {
            DTOCATPriceCOService item = new DTOCATPriceCOService();
            item.ServiceID = 1;
            item.Price = 10000;
            item.CurrencyID = 1;
            return item;
        }
        [TestMethod]
        public void CUSPrice_CO_Service_Save()
        {
            int priceID = 1223;
            DTOCATPriceCOService item = Price_CO_ServiceConstructor();

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSPrice_CO_Service_Save(item, priceID);
                bl.CUSPrice_CO_Service_Delete(item.ID);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void CUSPrice_CO_Service_Delete()
        {
            int priceID = 1223;
            DTOCATPriceCOService item = Price_CO_ServiceConstructor();
            var id = -1;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSPrice_CO_Service_Save(item, priceID);
                bl.CUSPrice_CO_Service_Delete(item.ID);
                id = item.ID;
                item = null;

                item = bl.CUSPrice_CO_ServicePacking_Get(id);
            }
            Assert.IsTrue((item == null || item.ID <= 0) && id > 0);
        }

        [TestMethod]
        public void CUSPrice_CO_CATService_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_CO_CATService_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_CATServicePacking_List()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_CO_CATServicePacking_List();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region ltl level
        [TestMethod]
        public void CUSPrice_DI_PriceLevel_DetailData()
        {
            var priceId = 2376;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_PriceLevel_DetailData(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceLevel_Save()
        {
            int priceID = 3397;
            int termID = 14;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var data = bl.CUSContract_Price_Data(termID);

                List<DTOPriceDILevelGroupProduct> lst = new List<DTOPriceDILevelGroupProduct>();
                foreach (var route in data.ListRouting)
                {
                    foreach (var level in data.ListLevel)
                    {
                        foreach (var gop in data.ListGroupOfProduct)
                        {
                            var item = new DTOPriceDILevelGroupProduct();
                            item.RoutingID = route.ID;
                            item.LevelID = level.ID;
                            item.GroupProductID = gop.ID;
                            item.Price = 1;

                            lst.Add(item);
                        }
                    }
                }

                bl.CUSPrice_DI_PriceLevel_Save(lst, priceID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceLevel_ExcelData()
        {
            var priceId = 2376;
            var contractTermId = 1166;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_PriceLevel_ExcelData(priceId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceLevel_ExcelImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region MOQ Load & Unload

        #region Load

        #region info
        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 2376;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQLoad_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Get()
        {
            var id = 36;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_PriceMOQLoad_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        private DTOPriceDIMOQLoad Price_DI_PriceMOQLoadConstructor()
        {
            DTOPriceDIMOQLoad item = new DTOPriceDIMOQLoad();
            item.MOQName = "TestMOQName";
            item.DIMOQLoadSumID = 316;
            item.TypeOfPriceDIExID = 1;
            return item;
        }
        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Save()
        {
            DTOPriceDIMOQLoad item = Price_DI_PriceMOQLoadConstructor();
            int priceID = 3396;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSPrice_DI_PriceMOQLoad_Save(item, priceID);
                bl.CUSPrice_DI_PriceMOQLoad_Delete(item.ID);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Delete()
        {
            DTOPriceDIMOQLoad item = Price_DI_PriceMOQLoadConstructor();
            int priceID = 3396;
            var id = -1;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSPrice_DI_PriceMOQLoad_Save(item, priceID);
                bl.CUSPrice_DI_PriceMOQLoad_Delete(item.ID);
                id = item.ID;
                item = null;

                item = bl.CUSPrice_DI_PriceMOQLoad_Get(id);
            }
            Assert.IsTrue((item == null || item.ID <= 0) && id > 0);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Group Location
        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_GroupLocation_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQLoad_GroupLocation_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_GroupLocation_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_GroupLocation_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price moq load GroupProduct
        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_GroupProduct_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 22;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQLoad_GroupProduct_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_GroupProduct_Get()
        {
            var id = 6;
            var cusId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_PriceMOQLoad_GroupProduct_Get(id, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_GroupProduct_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_GroupProduct_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_GroupProduct_GOPList()
        {
            var cusId = 1108;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQLoad_GroupProduct_GOPList(cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price moq load Location
        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Location_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQLoad_Location_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Location_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Location_LocationNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;
            var cusId = 4;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQLoad_Location_LocationNotInList(request, moqId, cusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price di moq ruote
        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Route_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQLoad_Route_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Route_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Route_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_Route_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;
            var contractTermId = 1258;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQLoad_Route_RouteNotInList(request, moqId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region price di moq parent route
        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_ParentRoute_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                bl.CUSPrice_DI_PriceMOQLoad_ParentRoute_List(request, moqId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_ParentRoute_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_ParentRoute_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var moqId = 6;
            var contractTermId = 1258;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(request, moqId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #endregion

        #region Unload

        #region info
        [TestMethod]
        public void CUSPrice_DI_PriceMOQUnLoad_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 1283;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_DI_PriceMOQUnLoad_List(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQUnLoad_Get()
        {
            var id = 20;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_DI_PriceMOQUnLoad_Get(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        [TestMethod]
        public void CUSPrice_DI_PriceMOQUnLoad_Save()
        {
            DTOPriceDIMOQLoad item = Price_DI_PriceMOQLoadConstructor();
            int priceID = 3396;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSPrice_DI_PriceMOQUnLoad_Save(item, priceID);
                bl.CUSPrice_DI_PriceMOQUnLoad_Delete(item.ID);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQUnLoad_Delete()
        {
            DTOPriceDIMOQLoad item = Price_DI_PriceMOQLoadConstructor();
            int priceID = 3396;
            int id = -1;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSPrice_DI_PriceMOQUnLoad_Save(item, priceID);
                bl.CUSPrice_DI_PriceMOQUnLoad_Delete(item.ID);
                id = item.ID;
                item = null;

                item = bl.CUSPrice_DI_PriceMOQUnLoad_Get(id);
            }
            Assert.IsTrue((item == null || item.ID <= 0) && id < 0);
        }

        [TestMethod]
        public void CUSPrice_DI_PriceMOQUnLoad_DeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #endregion
        #endregion

        #region CAT_PriceCOContainer
        [TestMethod]
        public void CUSPrice_CO_COContainer_Data()
        {
            var priceId = 108;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_CO_COContainer_Data(priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_COContainer_SaveList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_COContainer_ContainerList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 108;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_CO_COContainer_ContainerList(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_COContainer_ContainerDelete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_COContainer_ContainerNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var priceId = 108;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSPrice_CO_COContainer_ContainerNotInList(request, priceId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_COContainer_ContainerNotInSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_COContainer_ExcelData()
        {
            var priceId = 108;
            var contractTermId = 1266;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSPrice_CO_COContainer_ExcelData(priceId, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSPrice_CO_COContainer_ExcelImport()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #endregion

        #region GroupOfProduct
        [TestMethod]
        public void CUSContract_GroupOfProduct_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 7;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_GroupOfProduct_List(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_GroupOfProduct_Get()
        {
            var id = 13;
            var contractId = 7;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSContract_GroupOfProduct_Get(id, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_GroupOfProduct_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_GroupOfProduct_Delete()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_GroupOfProduct_Check()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Cuscontract_material
        [TestMethod]
        public void CUSContract_MaterialChange_Data()
        {
            var id = 1349;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSContract_MaterialChange_Data(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_MaterialChange_Save()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region Contract term(phụ lục hợp đồng)
        [TestMethod]
        public void CUSContract_ContractTerm_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_ContractTerm_List(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_ContractTerm_Get()
        {
            var id = 1349;
            int contractID = 6;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSContract_ContractTerm_Get(id, contractID);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        private DTOContractTerm ContractTermConstructor()
        {
            DTOContractTerm item = new DTOContractTerm();
            item.Code = "TestTermCode";
            item.TermName = "TestTermTermName";
            item.DisplayName = "TestTermDisplayName";
            item.DateEffect = DateTime.Now;
            item.DateExpire = DateTime.Now;

            return item;
        }
        [TestMethod]
        public void CUSContract_ContractTerm_Save()
        {
            DTOContractTerm item = ContractTermConstructor();
            int contractID = 6;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSContract_ContractTerm_Save(item, contractID);
                bl.CUSContract_ContractTerm_Delete(item.ID);
            }
            Assert.IsTrue(item.ID > 0);
        }

        [TestMethod]
        public void CUSContract_ContractTerm_Delete()
        {
            DTOContractTerm item = ContractTermConstructor();
            int contractID = 6;
            int id = -1;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSContract_ContractTerm_Save(item, contractID);
                bl.CUSContract_ContractTerm_Delete(item.ID);
                id = item.ID;
                item = null;

                item = bl.CUSContract_ContractTerm_Get(id, contractID);
            }
            Assert.IsTrue((item == null || item.ID <= 0) && id > 0);
        }

        [TestMethod]
        public void CUSContract_ContractTerm_Open()
        {
            DTOContractTerm item = ContractTermConstructor();
            int contractID = 6;
            bool IsClosed = false;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSContract_ContractTerm_Save(item, contractID);
                bl.CUSContract_ContractTerm_Close(item.ID);
                bl.CUSContract_ContractTerm_Open(item.ID);

                item = bl.CUSContract_ContractTerm_Get(item.ID, contractID);
                IsClosed = item.IsClosed;

                bl.CUSContract_ContractTerm_Delete(item.ID);

            }
            Assert.IsTrue(IsClosed == false);
        }

        [TestMethod]
        public void CUSContract_ContractTerm_Close()
        {
            DTOContractTerm item = ContractTermConstructor();
            int contractID = 6;
            bool IsClosed = false;

            using (var bl = new BLCustomer())
            {
                item.ID = bl.CUSContract_ContractTerm_Save(item, contractID);
                bl.CUSContract_ContractTerm_Close(item.ID);

                item = bl.CUSContract_ContractTerm_Get(item.ID, contractID);
                IsClosed = item.IsClosed;

                bl.CUSContract_ContractTerm_Delete(item.ID);

            }
            Assert.IsTrue(IsClosed);
        }

        [TestMethod]
        public void CUSContract_ContractTerm_Price_List()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractTermId = 1349;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_ContractTerm_Price_List(request, contractTermId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSTerm_Change_RemoveWarning()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion

        #region CusContract Setting
        [TestMethod]
        public void CUSContract_Setting_TypeOfRunLevelSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_GOVList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_Setting_GOVList(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_GOVGet()
        {
            var id = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSContract_Setting_GOVGet(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_GOVSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_GOVDeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_GOVNotInList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_Setting_GOVNotInList(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_GOVNotInSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_LevelList()
        {
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var contractId = 68;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_Setting_LevelList(request, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_LevelGet()
        {
            var id = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.CUSContract_Setting_LevelGet(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_LevelSave()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_LevelDeleteList()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void CUSContract_Setting_Level_GOVList()
        {
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var lst = bl.CUSContract_Setting_Level_GOVList(contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
        #endregion

        [TestMethod]
        public void Location_Check()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        #region PriceHistory
        [TestMethod]
        public void PriceHistory_CheckPrice()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PriceHistory_GetDataOneUser()
        {
            var cusId = 4;
            var contractId = 6;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
                var obj = bl.PriceHistory_GetDataOneUser(cusId, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }

        [TestMethod]
        public void PriceHistory_GetDataMulUser()
        {
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLCustomer())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds < 1);
        }
        #endregion
    }
}
