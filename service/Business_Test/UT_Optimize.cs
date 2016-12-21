using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Business;
using DTO;
using System.Data;
using System.Collections;
using System.Collections.Generic;

namespace Business_Test
{
    [TestClass]
    public class UT_Optimize : BaseTest
    {
        [TestMethod]
        public void CO_Optimize()
        {
            BLOptimize bl = new BLOptimize();

            //bl.Opt_Optimizer_Run(20, 0);
            
            //bl.Opt_Optimizer_Delete(2);
            
            //bl.Opt_COTOLocation_List(2);

            //bl.Opt_Optimizer_VehicleSchedule(string.Empty, 2);

            //bl.Opt_Optimizer_ToDataBase(2);

            //bl.Opt_COTOMaster_Save(2);

            //bl.Opt_Vehicle_List(string.Empty, 2);

            //bl.Opt_Optimizer_List(string.Empty);

            //bl.Opt_Optimizer_Reset(5);

            //bl.Opt_2View_Container_List(string.Empty, )

            BLOperation BLO = new BLOperation();
            BLO.OPSDI_MAP_Vehicle_New(6, "TESTXE", 0);
        }
    }
}
