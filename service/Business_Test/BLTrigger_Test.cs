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
    public class BLTrigger_Test : BaseTest
    {
        [TestMethod]
        public void GPS_Refresh()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void OPSMasterTendered_AutoSendMail()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void SMSTOMaster_List()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void SMSTOMaster_Update()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void BarcodeSO_List()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void BarcodeSO_Update()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PODBarcodeGroup_List()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PODBarcodeGroup_Save()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void MessageCall()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void MessageCall_User()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void MessageCall_Sended()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PriceMaterial_ListMaterial()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void PriceMaterial_Save()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Location_List()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Location_Save()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void LocationMatrix_List()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void LocationMatrix_Save()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TriggerEmail_List()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void TriggerEmail_Save()
        {
            Assert.Fail();
        }

        [TestMethod]
        public void Packet_Check()
        {
            using (var bl = new BLTrigger())
            {
                bl.Packet_Check();
            }
        }
    }
}
