using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using System.Linq;
using DTO;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Business_Test
{
    [TestClass]
    public class UT_Operation : BaseTest
    {
        [TestMethod]
        public void Appointment_Route_ListVendor()
        {
            BLOperation bl = new BLOperation();
        }
    
        [TestMethod]
        public void COAppointment_2View_Container_List()
        {
            int[] sCus = { 4, 5, 47 };
            List<int> lstCusID = new List<int>(sCus);
            BLOperation bl = new BLOperation();
            bl.COAppointment_2View_Container_List(string.Empty, lstCusID, new DateTime(2016, 1, 1), new DateTime(2016, 6, 1));
        }

        [TestMethod]
        public void COAppointment_2View_ContainerHasMaster_List()
        {
            int[] sCus = { 4, 5, 47 };
            List<int> lstCusID = new List<int>(sCus);
            BLOperation bl = new BLOperation();
            bl.COAppointment_2View_ContainerHasMaster_List(string.Empty, lstCusID, new DateTime(2016, 1, 1), new DateTime(2016, 6, 1));
        }

        [TestMethod]
        public void COAppointment_2View_Master_List()
        {
            int[] sCus = { 4, 5, 47 };
            List<int> lstCusID = new List<int>(sCus);
            BLOperation bl = new BLOperation();
            bl.COAppointment_2View_Master_List(string.Empty, lstCusID, new DateTime(2016, 1, 1), new DateTime(2016, 6, 1));
        }

        [TestMethod]
        public void COAppointment_2View_Master_Update()
        {
            int[] sCus = { 4, 5, 47 };
            List<int> lstCusID = new List<int>(sCus);

            List<DTOOPSCOTOMaster> dataMaster = new List<DTOOPSCOTOMaster>();
            List<DTOOPSCOTOContainer> dataContainer = new List<DTOOPSCOTOContainer>();
            DTOOPSCOTOMaster objMaster = new DTOOPSCOTOMaster();
            objMaster.CreateSortOrder = 1;
            objMaster.ETD = DateTime.Now.AddDays(-3);

            dataMaster.Add(objMaster);

            BLOperation bl = new BLOperation();

            dataContainer = bl.COAppointment_2View_Container_List(string.Empty, lstCusID, new DateTime(2016, 1, 1), new DateTime(2016, 6, 1));
            dataContainer = dataContainer.Where(c => c.ContainerID < 348).ToList();
            foreach (var o in dataContainer)
            {
                o.CreateSortOrder = 1;
            }

            bl.COAppointment_2View_Master_Update(dataMaster, dataContainer);
        }

        [TestMethod]
        public void COAppointment_2View_Master_Delete()
        {
            BLOperation bl = new BLOperation();
            bl.COAppointment_2View_Master_Delete(5);
        }
        
        [TestMethod]
        public void RomoocByVendorID_List()
        {
            BLOperation bl = new BLOperation();

            bl.RomoocByVendorID_List(1);
        }

        [TestMethod]
        public void TEST()
        {
            BLOperation bl = new BLOperation();
            List<int> lstID = new List<int>();
            lstID.Add(18);

            //bl.OPS_Tendering_Vendor_List();
            
            //bl.DIAppointment_Route_FTL_NoDNOrderList("sort: \"\", page: 0, pageSize: 0, group: \"\", filter: \"TypeID~eq~1\"", lstID, new DateTime(2016, 4, 30), new DateTime(2016, 5, 3));
        }
        
    }
}
