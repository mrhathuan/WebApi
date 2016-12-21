using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Kendo.Mvc.Extensions;
using DTO;
using IServices;
using Business;

namespace Services
{
    public partial class SVTrigger : SVBase, ISVTrigger
    {
        public void GPS_Refresh()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.GPS_Refresh();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSMasterTendered_AutoSendMail()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.OPSMasterTendered_AutoSendMail();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOTriggerTOMaster> SMSTOMaster_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.SMSTOMaster_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SMSTOMaster_Update(DTOTriggerTOMaster item)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.SMSTOMaster_Update(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOTriggerORDGroup> BarcodeSO_List(string SOCode, string DNCode, int? SysCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.BarcodeSO_List(SOCode, DNCode, SysCustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void BarcodeSO_Update(List<DTOTriggerORDGroup> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.BarcodeSO_Update(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOBarCodeGroup> PODBarcodeGroup_List(string DNCode, int? SysCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.PODBarcodeGroup_List(DNCode, SysCustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOBarCodeGroup PODBarcodeGroup_Save(int ID, string lit, string kg)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.PODBarcodeGroup_Save(ID, lit, kg);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        #region Workflow
        public List<DTOTriggerMessage> MessageCall()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.MessageCall();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOTriggerMessage> MessageCall_User()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.MessageCall_User();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void MessageCall_Sended(List<long> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.MessageCall_Sended(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTONotificationUser MessageCall_LoadMore(int currentPage, int pageSize, string typeOfMessage)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.MessageCall_LoadMore(currentPage, pageSize, typeOfMessage);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void MessageCall_Read()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.MessageCall_Read();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Workflow new
        public List<DTOTriggerMessage> WFL_MessageCall()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.MessageCall();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOTriggerMessage> WFL_MessageCall_User()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.MessageCall_User();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void WFL_MessageCall_Sended(List<long> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.WFL_MessageCall_Sended(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTONotificationUser WFL_MessageCall_LoadMore(int currentPage, int pageSize, string typeOfMessage)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.WFL_MessageCall_LoadMore(currentPage, pageSize, typeOfMessage);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void WFL_MessageCall_Read()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.WFL_MessageCall_Read();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTOTriggerEmail> WFL_TriggerEmail_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.WFL_TriggerEmail_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void WFL_TriggerEmail_Send(List<DTOTriggerEmail> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.WFL_TriggerEmail_Send(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOTriggerSMS> WFL_TriggerSMS_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.WFL_TriggerSMS_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void WFL_TriggerSMS_Send(List<DTOTriggerSMS> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.WFL_TriggerSMS_Send(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Packet
        public DTOTriggerWFLPacket Packet_Check()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.Packet_Check();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Packet_Update(List<DTOTriggerPacketAction> ListPacketAction, List<DTOTriggerPacketDriver> ListPacketDriver)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.Packet_Update(ListPacketAction, ListPacketDriver);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion


        public List<DTOTriggerMaterial> PriceMaterial_ListMaterial()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.PriceMaterial_ListMaterial();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void PriceMaterial_Save(DTOTriggerMaterial item)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.PriceMaterial_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATLocation> Location_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.Location_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Location_Save(List<DTOCATLocation> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.Location_Save(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATLocationMatrix> LocationMatrix_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.LocationMatrix_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void LocationMatrix_Save(List<CATLocationMatrix> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.LocationMatrix_Save(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOTriggerEmail> TriggerEmail_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.TriggerEmail_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void TriggerEmail_Send(List<DTOTriggerEmail> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.TriggerEmail_Send(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOTriggerSMS> TriggerSMS_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    return bl.TriggerSMS_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void TriggerSMS_Send(List<DTOTriggerSMS> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLTrigger>())
                {
                    bl.TriggerSMS_Send(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
    }
}

