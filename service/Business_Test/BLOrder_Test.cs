using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DTO;

namespace Business_Test
{
    [TestClass]
    public class BLOrder_Test : BaseTest
    {
        [TestMethod]
        public void ORDOrder_CustomerList()
        {
            DateTime fDate = DateTime.Now;
            using (var bl = new BLOrder())
            {
                bl.ORDOrder_CustomerList();
            }
            Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_VendorList()
        {
            DateTime fDate = DateTime.Now;
            using (var bl = new BLOrder())
            {
                bl.ORDOrder_VendorList();
            }
            Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Contract_List()
        {
            DateTime fDate = DateTime.Now;
            using (var bl = new BLOrder())
            {
                bl.ORDOrder_Contract_List(6, -(int)SYSVarType.ServiceOfOrderLocal, -(int)SYSVarType.TransportModeFTL, false);
            }
            Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_GroupOfProduct_List()
        {
            DateTime fDate = DateTime.Now;
            using (var bl = new BLOrder())
            {
                bl.ORDOrder_GroupOfProduct_List(6);
            }
            Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Stock_List()
        {
            DateTime fDate = DateTime.Now;
            using (var bl = new BLOrder())
            {
                bl.ORDOrder_Stock_List(6);
            }
            Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_CusLocation_List()
        {
            DateTime fDate = DateTime.Now;
            using (var bl = new BLOrder())
            {
                bl.ORDOrder_CusLocation_List(6);
            }
            Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_ContainerService_List()
        {
            DateTime fDate = DateTime.Now;
            using (var bl = new BLOrder())
            {
                bl.ORDOrder_ContainerService_List(6);
            }
            Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_ContractCODefault_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_ContainerService_List(6); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_TruckLocal_Data()
        {
            DateTime fDate = DateTime.Now;
            using (var bl = new BLOrder())
            {
                bl.ORDOrder_TruckLocal_Data(6, 1);
            }
            Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_IMEX_Data()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_IMEX_Data(6); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_GetItem()
        {
            //DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_GetItem(1, 6, -(int)SYSVarType.ServiceOfOrderLocal, -(int)SYSVarType.TransportModeFTL, -1); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_FTLLO_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_LTLLO_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_FCLLO_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_FCLIMEX_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_LTLIMEX_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_FTLIMEX_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_GetView()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_GetView(-(int)SYSVarType.ServiceOfOrderLocal, -(int)SYSVarType.TransportModeFTL); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Contract_Change()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_PriceGroupVehicle()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_PriceGroupProduct()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_PriceContainer()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Delete()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_Delete(1); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

       

        [TestMethod]
        public void ORDOrder_Copy()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DeleteList()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_ToOPSCheck()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_RoutingArea_Refresh()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_ToOPS()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DN_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_DN_List(string.Empty, 6); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DN_SORest_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_DN_SORest_List(6, true, DateTime.Now, DateTime.Now); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DN_DNRest_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_DN_DNRest_List(6, true, DateTime.Now, DateTime.Now); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DN_AllRest_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_DN_AllRest_List(6, true, DateTime.Now, DateTime.Now); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Excel_Location_Create()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Excel_Import_Data()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_Excel_Import_Data(6); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Excel_Import()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Excel_Setting_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_Excel_Setting_List(string.Empty); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Excel_Setting_Get()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_Excel_Setting_Get(6); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Excel_Setting_Code_Get()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORDOrder_Excel_Setting_Code_Get(); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_TypeOfDocument_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_TypeOfDocument_Get()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_TypeOfDocument_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_TypeOfDocument_DeleteList()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_TypeOfDocument_Read()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Document_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Document_Get()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Document_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_Document_DeleteList()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentService_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentService_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentService_NotInList()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentService_NotInList_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentService_DeleteList()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentContainer_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentContainer_Complete()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentContainer_NotInList()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentContainer_NotInList_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentContainer_DeleteList()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_ContainerInService_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_ContainerInService_NotInList_Save()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORDOrder_DocumentContainer_Read()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORD_Tracking_Order_List(List<int> dataCus)
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORD_Tracking_Order_List(new List<int>(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11})); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORD_Tracking_TripByOrder_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORD_Tracking_TripByOrder_List(1); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }

        [TestMethod]
        public void ORD_Tracking_LocationByTrip_List()
        {
            DateTime fDate = DateTime.Now; using (var bl = new BLOrder()) { bl.ORD_Tracking_LocationByTrip_List(1, 1000); } Assert.IsTrue((DateTime.Now - fDate).Seconds < MaxSec);
        }
    }
}
