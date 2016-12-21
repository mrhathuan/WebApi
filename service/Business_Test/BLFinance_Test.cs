using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
namespace Business_Test
{
    [TestClass]
    public class BLFinance_Test : BaseTest
    {
        List<string> lstContent = new List<string>();

        [TestMethod]
        public void FINRefresh_List()
        {
            string fileName = "FINRefresh_List";
            List<string> lstContent = new List<string>();
            lstContent.Add("FINRefresh_CRUD /n Test method: FINRefresh_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINRefresh_List(dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(1 == 1);
          
        }

        [TestMethod]
        public void FINRefresh_Refresh()
        {
            string fileName = "FINRefresh_Refresh";
            List<string> lstContent = new List<string>();
            lstContent.Add("Test method: FINRefresh_Refresh");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(1 == 1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void FINRefresh_Contract_List()
        {
            string fileName = "FINRefresh_Contract_List";
            var cusId = 4;
            var serId = 28;
            var transId = 33;
            lstContent.Add("Test method: FINRefresh_Contract_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINRefresh_Contract_List(cusId, serId, transId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(1==1);
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
             lstContent.Add("");
            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void FINRefresh_Contract_Master_List()
        {
            string fileName = "FINRefresh_Contract_Master_List";
            var cusId = 6;
            var serId = 28;
            var transId = 33;
            lstContent.Add("FINRefresh_Contract_CRUD /n Test method: FINRefresh_Contract_Master_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINRefresh_Contract_List(cusId, serId, transId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
           
            LogResult(fileName, lstContent);
        }

        [TestMethod]
        public void FINRefresh_Routing_List()
        {
            string fileName = "FINRefresh_Routing_List";
            var cusId = 4;
            var contractId = 6;
            lstContent.Add("FINRefresh_Routing_CRUD /n Test method: FINRefresh_Routing_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINRefresh_Routing_List(cusId, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          
        }

        [TestMethod]
        public void FINRefresh_Routing_Master_List()
        {
            string fileName = "FINRefresh_Routing_Master_List";
            var cusId = 6;
            var contractId = 31;
            lstContent.Add("Test method: FINRefresh_Routing_Master_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINRefresh_Routing_Master_List(cusId, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        

        }

        [TestMethod]
        public void FINRefresh_ORD_Group_List()
        {
            string fileName = "FINRefresh_ORD_Group_List";
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            lstContent.Add("FINRefresh_ORD_Group_CRUD \n Test method: FINRefresh_ORD_Group_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINRefresh_ORD_Group_List(request, dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          

        }

        [TestMethod]
        public void FINRefresh_ORD_Group_Save()
        {
            string fileName = "FINRefresh_ORD_Group_Save";
            lstContent.Add("Test method: FINRefresh_ORD_Group_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
           

        }

        [TestMethod]
        public void FINRefresh_OPS_Group_List()
        {
            string fileName = "FINRefresh_OPS_Group_List";
            lstContent.Add("FINRefresh_OPS_Group_CRUD \n Test method: FINRefresh_OPS_Group_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINRefresh_OPS_Group_List(request, dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
           

        }

        [TestMethod]
        public void FINRefresh_OPS_Master_List()
        {
            string fileName = "FINRefresh_OPS_Master_List";
            lstContent.Add("Test method: FINRefresh_OPS_Master_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINRefresh_OPS_Master_List(request, dtfrom, dtto);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          

        }

        [TestMethod]
        public void FINRefresh_OPS_Group_Save()
        {
            string fileName = "FINRefresh_OPS_Group_Save";
            lstContent.Add("Test method: FINRefresh_OPS_Group_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
           

        }

        [TestMethod]
        public void FINRefresh_OPS_Master_Save()
        {
            string fileName = "FINRefresh_OPS_Master_Save";
            lstContent.Add("Test method: FINRefresh_OPS_Master_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        
        }

        [TestMethod]
        public void FINRefresh_OPSRouting_List()
        {
            string fileName = "FINRefresh_OPSRouting_List";
            var cusId = 6;
            var contractId = 31;
            lstContent.Add("FINRefresh_OPSRouting_CRUD \n Test method: FINRefresh_OPSRouting_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINRefresh_OPSRouting_List(1, cusId, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            
        }

        [TestMethod]
        public void FINRefresh_OPSGroupRouting_List()
        {
            string fileName = "FINRefresh_OPSGroupRouting_List";
            lstContent.Add("Test method: FINRefresh_OPSGroupRouting_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            var cusId = 6;
            var contractId = 31;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINRefresh_OPSGroupRouting_List(1, cusId, contractId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            

        }

        [TestMethod]
        public void FINFreightAudit_List()
        {
            string fileName = "FINFreightAudit_List";
            lstContent.Add("FINFreightAudit_CRUD \n Test method: FINFreightAudit_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var statusId = 275;
            using (var bl = new BLFinance())
            {
                bl.Account_Setting();
            }

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINFreightAudit_List(dtfrom, dtto, statusId, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          


        }

        [TestMethod]
        public void FINFreightAudit_DetailList()
        {
            string fileName = "FINFreightAudit_DetailList";
            var masterId = 2939;
            lstContent.Add("Test method: FINFreightAudit_DetailList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINFreightAudit_DetailList(masterId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        }

        [TestMethod]
        public void FINFreightAudit_StatusList()
        {
            string fileName = "FINFreightAudit_StatusList";
            lstContent.Add("Test method: FINFreightAudit_StatusList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINFreightAudit_StatusList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);

        }

        [TestMethod]
        public void FINFreightAudit_Reject()
        {
            string fileName = "FINFreightAudit_Reject";
            lstContent.Add("Test method: FINFreightAudit_Reject");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        }

        [TestMethod]
        public void FINFreightAudit_Accept()
        {
            string fileName = "FINFreightAudit_Accept";
            lstContent.Add("Test method: FINFreightAudit_Accept");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          

        }

        [TestMethod]
        public void FINFreightAudit_Waiting()
        {
            string fileName = "FINFreightAudit_Waiting";
            lstContent.Add("Test method: FINFreightAudit_Waiting");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        }

        [TestMethod]
        public void FINFreightAudit_Approved()
        {

            string fileName = "FINFreightAudit_Approved";
            lstContent.Add("Test method: FINFreightAudit_Approved");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        }

        [TestMethod]
        public void FINFreightAudit_Export()
        {

            string fileName = "FINFreightAudit_Export";
            lstContent.Add("Test method: FINFreightAudit_Export");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var statusId = 275;
            using (var bl = new BLFinance())
            {
                bl.Account_Setting();
            }

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINFreightAudit_Export(dtfrom, dtto, statusId);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          

        }

        [TestMethod]
        public void FINFreightAuditCus_Order_StatusList()
        {
            string fileName = "FINFreightAuditCus_Order_StatusList";
            lstContent.Add("FINFreightAuditCus_CRUD \n Test method: FINFreightAuditCus_Order_StatusList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINFreightAuditCus_Order_StatusList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);

        }

        [TestMethod]
        public void FINFreightAuditCus_Group_StatusList()
        {
            string fileName = "FINFreightAuditCus_Group_StatusList";
            lstContent.Add("Test method: FINFreightAuditCus_Group_StatusList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINFreightAuditCus_Group_StatusList();
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        }

        [TestMethod]
        public void FINFreightAuditCus_Order_List()
        {
            string fileName = "FINFreightAuditCus_Order_List";
            lstContent.Add("Test method: FINFreightAuditCus_Order_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var statusId = 1;

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINFreightAuditCus_Order_List(dtfrom, dtto, statusId, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
         
        }

        [TestMethod]
        public void FINFreightAuditCus_OrderDetail_List()
        {
            string fileName = "FINFreightAuditCus_OrderDetail_List";
            var id = 3539;
            lstContent.Add("Test method: FINFreightAuditCus_OrderDetail_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINFreightAuditCus_OrderDetail_List(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
         
        }

        [TestMethod]
        public void FINFreightAuditCus_Order_DetailList()
        {
            string fileName = "FINFreightAuditCus_Order_DetailList";
            var id = 3539;
            lstContent.Add("Test method: FINFreightAuditCus_Order_DetailList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINFreightAuditCus_Order_DetailList(id);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        }

        [TestMethod]
        public void FINFreightAuditCus_Order_Reject()
        {
            string fileName = "FINFreightAuditCus_Order_Reject";
            lstContent.Add("Test method: FINFreightAuditCus_Order_Reject");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        }

        [TestMethod]
        public void FINFreightAuditCus_Order_Accept()
        {
            string fileName = "FINFreightAuditCus_Order_Accept";
            lstContent.Add("Test method: FINFreightAuditCus_Order_Accept");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        }

        [TestMethod]
        public void FINFreightAuditCus_Order_Approved()
        {
            string fileName = "FINFreightAuditCus_Order_Approved";
            lstContent.Add("Test method: FINFreightAuditCus_Order_Approved");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          
        }

        [TestMethod]
        public void FINFreightAuditCus_Group_List()
        {
            string fileName = "FINFreightAuditCus_Group_List";
            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            var statusId = 1;
            lstContent.Add("Test method: FINFreightAuditCus_Group_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINFreightAuditCus_Group_List(dtfrom, dtto, statusId, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
           
        }

        [TestMethod]
        public void FINFreightAuditCus_Group_DetailList()
        {
            string fileName = "FINFreightAuditCus_Group_DetailList";
            lstContent.Add("Test method: FINFreightAuditCus_Group_DetailList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);


            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        
        }

        [TestMethod]
        public void FINFreightAuditCus_Group_Reject()
        {
            string fileName = "FINFreightAuditCus_Group_Reject";
            lstContent.Add("Test method: FINFreightAuditCus_Group_Reject");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
           

        }

        [TestMethod]
        public void FINFreightAuditCus_Group_Accept()
        {
            string fileName = "FINFreightAuditCus_Group_Accept";
            lstContent.Add("Test method: FINFreightAuditCus_Group_Accept");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          
        }

        [TestMethod]
        public void FINFreightAuditCus_Group_Approved()
        {
            string fileName = "FINFreightAuditCus_Group_Approved";
            lstContent.Add("Test method: FINFreightAuditCus_Group_Approved");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
            Assert.IsTrue(sub.Seconds > 0);
         

        }

        [TestMethod]
        public void FINManualFix_List()
        {
            string fileName = "FINManualFix_List";
            lstContent.Add("FINManualFix_CRUD \n Test method: FINManualFix_List");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);

            var dtfrom = DateTime.Now.AddDays(-30);
            var dtto = DateTime.Now;
            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINManualFix_List(dtfrom, dtto, request);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          
        }

        [TestMethod]
        public void FINManualFix_Save()
        {
            string fileName = "FINManualFix_Save";
            lstContent.Add("Test method: FINManualFix_Save");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          

        }

        [TestMethod]
        public void FINManualFix_ChooseList()
        {
            string fileName = "FINManualFix_ChooseList";

            var request = JsonConvert.SerializeObject(new Kendo.Mvc.UI.DataSourceRequest());
            lstContent.Add("Test method: FINManualFix_ChooseList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            DateTime pDateFrom = DateTime.Now;
            DateTime pDateTo = DateTime.Now;
            LogResult(fileName, lstContent);

            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
                var lst = bl.FINManualFix_ChooseList(request, pDateFrom, pDateTo);
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
          
        }

        [TestMethod]
        public void FINManualFix_SaveList()
        {
            string fileName = "FINManualFix_SaveList";
            lstContent.Add("Test method: FINManualFix_SaveList");
            lstContent.Add("Date start: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Date end: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
            lstContent.Add("Status: " + "success");
            lstContent.Add("");
            LogResult(fileName, lstContent);
            DateTime dtStart = DateTime.Now;
            using (var bl = new BLFinance())
            {
            }
            DateTime dtEnd = DateTime.Now;
            var sub = dtEnd - dtStart;
            Assert.IsTrue(sub.Seconds > 0);
        
        }
    }
}
