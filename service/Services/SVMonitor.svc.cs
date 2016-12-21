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
    public partial class SVMonitor : SVBase, ISVMonitor
    {
        #region Common
        public List<DTOMONTractor> TractorOwner_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.TractorOwner_List();
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

        public List<DTOMONTruck> TruckOwner_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.TruckOwner_List();
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

        public List<DTOMONRomooc> RomoocOwner_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.RomoocOwner_List();
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

        public void TractorOwner_UpdateFromGPS(List<DTOMONVehicle> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.TractorOwner_UpdateFromGPS(lst);
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

        public void TruckOwner_UpdateFromGPS(List<DTOMONVehicle> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.TruckOwner_UpdateFromGPS(lst);
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




        public DTOResult Tractor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.Tractor_List();
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

        public DTOResult Romooc_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.Romooc_List();
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

        public DTOResult Truck_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.Truck_List();
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

        public DTOResult TractorByVendorID_List(int? vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.TractorByVendorID_List(vendorID);
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

        public DTOResult RomoocByVendorID_List(int? vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.RomoocByVendorID_List(vendorID);
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

        public DTOResult TruckByVendorID_List(int? vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.TruckByVendorID_List(vendorID);
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

        public DTOResult Driver_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.Driver_List();
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

        public DTOResult DITOMasterRouting_List(int masterID, int? locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DITOMasterRouting_List(masterID, locationID);
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

        public DTOResult COTOMasterRouting_List(int masterID, int? locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.COTOMasterRouting_List(masterID, locationID);
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

        public DTOResult CATTrouble_List(bool isCO)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.CATTrouble_List(isCO);
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

        public DTOResult Vendor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.Vendor_List();
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

        public DTOResult Customer_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.Customer_List();
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

        public DTOResult CATLocation_List(string request, List<int> ignore)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.CATLocation_List(request, ignore);
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

        public DTOResult CATStock_List(string request, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.CATStock_List(request, cusID);
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

        public CATVehicle Vehicle_Create(string vehicleNo, int? vendorID, int vehicleType, double maxWeight)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.Vehicle_Create(vehicleNo, vendorID, vehicleType, maxWeight);
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

        public CATVehicle Romooc_Create(string vehicleNo, int? vendorID, int vehicleType, double maxWeight)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.Romooc_Create(vehicleNo, vendorID, vehicleType, maxWeight);
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

        public DTOResult CATLocation_SeaPortList(string request, int opsTOContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.CATLocation_SeaPortList(request, opsTOContainer);
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

        public DTOResult MONControlTowerCO_COTOStockRead(string request, int opsTOContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_COTOStockRead(request, opsTOContainer);
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

        public DTOResult MONControlTowerCO_StandDetailList(string request, int opsTOContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_StandDetailList(request, opsTOContainer);
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

        public DTOResult MONControlTowerCO_OrderEXIM(string request, int opsTOContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_OrderEXIM(request, opsTOContainer);
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

        public DTOResult MONControlTowerCO_OrderLocalOffer(string request, int opsTOContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_OrderLocalOffer(request, opsTOContainer);
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

        public List<DTOCATLocation> MONControlTowerCO_VehicleLocationDefault(int opsTOContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_VehicleLocationDefault(opsTOContainer);
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

        #region Truck-New


        public DTOMONOPSTOMaster DIMonitor_VehicleTimeGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitor_VehicleTimeGet(id);
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

        public DTOResult DIMonitorTrouble_List(int masterID, int? locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorTrouble_List(masterID, locationID);
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

        public DTOResult DIMonitorTrouble_NotinList(int masterID, int? locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorTrouble_NotinList(masterID, locationID);
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

        public DTOCATTrouble DIMonitorTrouble_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorTrouble_Get(id);
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

        public int DIMonitorTrouble_Save(DTOCATTrouble item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorTrouble_Save(item);
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

        public void DIMonitorTrouble_SaveAll(List<DTOCATTrouble> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorTrouble_SaveAll(data);
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

        public void DIMonitorTrouble_SaveList(List<DTOCATSYSCustomerTrouble> lst, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorTrouble_SaveList(lst, masterID);
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

        public void DIMonitorTrouble_Delete(DTOCATTrouble item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorTrouble_Delete(item);
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




        public List<DTO_MONDITO> DIMonitorMaster_Complete(List<DTO_TOMasterActualTime> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorMaster_Complete(lst);
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

        public void DIMonitorMaster_Revert(List<int> lstMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_Revert(lstMasterID);
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


        public DTOResult DIMonitorMaster_SOPartnerLocation(string request, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorMaster_SOPartnerLocation(request, customerID);
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

        public List<DTOCombobox> DIMonitorMaster_SOPartnerList(int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorMaster_SOPartnerList(customerID);
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

        public void DIMonitorMaster_ChangeSOLocation(int cuslocationID, int OrderGroupProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_ChangeSOLocation(cuslocationID, OrderGroupProductID);
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

        public int DIMonitorMaster_PartnerLocationSave(DTOCUSPartnerLocation item, int OrderGroupProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorMaster_PartnerLocationSave(item, OrderGroupProductID);
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

        public void DIMonitorMaster_Update(DTOMONOPSTOMaster item, List<DTOMONOPSTODetail> lstDetail)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_Update(item, lstDetail);
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

        public DTOResult DIMonitorMaster_DITOLocationList(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorMaster_DITOLocationList(request, masterID);
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

        public List<CUSGroupOfProduct> DIMonitorMaster_CUSGOPList(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorMaster_CUSGOPList(customerid);
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

        public DTOResult DIMonitorMaster_SL_List(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorMaster_SL_List(request, masterID);
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

        public DTOResult DIMonitorMaster_GOPReturnList(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorMaster_GOPReturnList(request, masterID);
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

        public void DIMonitorMaster_GOPReturnSave(List<DTOMONOPSTODetail> data, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_GOPReturnSave(data, masterID);
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

        public void DIMonitorMaster_GOPReturnDeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_GOPReturnDeleteList(lst);
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

        public void DIMonitorMaster_SL_Save(List<DTOMONOPSTODetail> item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_SL_Save(item);
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
        public List<DTOMON_TOProductOfGroup> DIMonitor_DITOProductOfGroup(int TOGroupProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitor_DITOProductOfGroup(TOGroupProductID);
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

        public List<DTOCombobox> DIMonitor_TOGroupProductCancelReason()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitor_TOGroupProductCancelReason();
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

        public void DIMonitorMaster_CancelTOGroup(int opsGroupID, int reasonID, string note)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_CancelTOGroup(opsGroupID, reasonID, note);
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





        public List<DTOCombobox> DIMonitor_ListTypeOfDriver()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitor_ListTypeOfDriver();
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

        public void DIMonitorMaster_UpdateCashCollection(int ordGroupID, bool value)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_UpdateCashCollection(ordGroupID, value);
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

        #region Truck _ DN

        public void DIMonitorMaster_CompleteDN(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_CompleteDN(lstID);
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

        public void DIMonitorMaster_RevertDN(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_RevertDN(lstID);
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

        public void DIMonitorMaster_UpdateDITO(List<DTO_MONDITO> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_UpdateDITO(lst);
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



        public void DIMonitorMaster_GOPReturnAdd(DTOMONProductReturn item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_GOPReturnAdd(item);
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

        public List<DTOCombobox> DIMonitor_DITOGroupProductList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitor_DITOGroupProductList(masterID);
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

        public List<DTOCUSProduct> DIMonitor_CUSProductList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitor_CUSProductList(masterID);
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

        #region Tower DI

        public DTOMONMonitor_OrderLog MONControlTower_OrderLogList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_OrderLogList(masterID);
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

        public void MONControlTower_ApprovedTrouble(int troubleId)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_ApprovedTrouble(troubleId);
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

        public void MONControlTower_RejectTrouble(int troubleId)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_RejectTrouble(troubleId);
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

        public DTOMAPVehicle MONControlTower_VehicleGPS(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_VehicleGPS(masterID);
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

        public DTOResult MONControlTower_OrderList(string request, DateTime dfrom, DateTime dto, bool isRunning, bool isComplete)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_OrderList(request, dfrom, dto, isRunning, isComplete);
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

        public DTOMONMonitor_OPSSummary MONControlTower_FilterByRoute(int routeID, DateTime dfrom, DateTime dto)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_FilterByRoute(routeID, dfrom, dto);
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

        public List<DTOMONMonitor_OPSSummary> MONControlTower_FilterByArea(int areaID, DateTime dfrom, DateTime dto)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_FilterByArea(areaID, dfrom, dto);
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

        public DTOMON_MainFilter MONControlTower_MainVehicleFilter(DTOMONControlTower_ObjectFilter item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_MainVehicleFilter(item);
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

        public DTOMON_Schedule_Data MONControlTower_Schedule(DTOMONControlTower_ObjectFilter item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_Schedule(item);
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

        public List<DTOOPSDITOLocation> MONControlTower_GetLocationByMaster(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_GetLocationByMaster(masterID);
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

        public List<DTOOPSRouteProblem> MONControlTower_ProblemList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_ProblemList();
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

        public DTOMAPVehicle MONControlTower_GetTroubleLocation(int troubleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_GetTroubleLocation(troubleID);
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

        public void MONControlTower_SchedulerSaveChance(DTOOPSDITOMaster item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_SchedulerSaveChance(item);
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

        public DTOResult MONControlTower_DIStationList(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_DIStationList(request, masterID);
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

        public DTOResult MONControlTower_DIStationNotinList(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_DIStationNotinList(request, masterID);
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

        public DTOResult MONControlTower_DILocation(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_DILocation(request, masterID);
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

        public void MONControlTower_DIStationAdd(int masterID, List<int> ListStationID, DTOMON_OPSLocation opsLocation)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_DIStationAdd(masterID, ListStationID, opsLocation);
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

        public void MONControlTower_DIStationRemove(List<int> ListStationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_DIStationRemove(ListStationID);
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

        public void MONControlTower_DIStationApprove(int id, bool value)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_DIStationApprove(id, value);
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

        public void MONControlTower_DIStationSaveChanges(List<DTOMON_Station> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_DIStationSaveChanges(lst);
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

        public DTOResult MONControlTower_COStationList(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_COStationList(request, masterID);
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

        public DTOResult MONControlTower_COStationNotinList(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_COStationNotinList(request, masterID);
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

        public void MONControlTower_COStationAdd(int masterID, List<int> ListStationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_COStationAdd(masterID, ListStationID);
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

        public void MONControlTower_COStationRemove(List<int> ListStationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_COStationRemove(ListStationID);
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

        public void MONControlTower_COStationApprove(int id, bool value)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_COStationApprove(id, value);
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

        public void MONControlTower_COStationSaveChanges(List<DTOMON_Station> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_COStationSaveChanges(lst);
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


        public void MONControlTower_ChangeTractor(int masterID, int vehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_ChangeTractor(masterID, vehicleID);
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

        public void MONControlTower_ChangeRomooc(int masterID, int vehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_ChangeRomooc(masterID, vehicleID);
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

        public DTOResult MONControlTower_TractorRead(string request, int? vendorID, DateTime? dfrom, DateTime? dto)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_TractorRead(request, vendorID, dfrom, dto);
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

        public DTOResult MONControlTower_RomoocRead(string request, int? vendorID, DateTime? dfrom, DateTime? dto, int vehicleid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_RomoocRead(request, vendorID, dfrom, dto, vehicleid);
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


        public List<DTOMON_WFL_Message> MONControlTower_GetAllNotification()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_GetAllNotification();
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

        public DTOResult MONControlTower_TruckRead(string request, int? vendorID, DateTime? dfrom, DateTime? dto)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_TruckRead(request, vendorID, dfrom, dto);
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

        public DTOResult MONControlTower_DriverRead(string request, DateTime? dfrom, DateTime? dto)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_DriverRead(request, dfrom, dto);
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

        public void MONControlTower_ChangeVehicle(int masterID, int vehicleID, int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_ChangeVehicle(masterID, vehicleID, vendorID);
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

        public void MONControlTower_TOMaster_Split(int toID, int? vendorID, int vehicleID, DateTime ETD, DateTime ETA, string driverName, string driverTel, int fLocation, int tLocation)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_TOMaster_Split(toID, vendorID, vehicleID, ETD, ETA, driverName, driverTel, fLocation, tLocation);
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

        public void MONControlTower_ChangeDriver(int masterID, int driverID, int typeOfDriver, string reason)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_ChangeDriver(masterID, driverID, typeOfDriver, reason);
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

        public List<DTOMONDriver> MONControlTower_SupDriverRead(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_SupDriverRead(masterID);
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

        public void MONControlTower_ChangeSupDriver(int masterID, List<DTOMONDriver> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_ChangeSupDriver(masterID, lst);
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

        public void MONControlTower_TimeSheet_ApproveDriver(int timeDriverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_TimeSheet_ApproveDriver(timeDriverID);
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

        public void MONControlTower_TimeSheet_RejectDriver(int timeDriverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_TimeSheet_RejectDriver(timeDriverID);
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

        public void MONControlTower_ChangeTimeSheetDriver(int timeID, int flmDriverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_ChangeTimeSheetDriver(timeID, flmDriverID);
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

        public List<DTOFLMVehicle> MONControlTower_TimeSheet_VehicleList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_TimeSheet_VehicleList();
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

        public DTOMON_TimeSheetDriver MONControlTower_TimeSheet_VehicleTimeList(DateTime dateFrom, DateTime dateTo, bool isOpen, bool isAccept, bool isReject, bool isGet, bool isComplete, bool isRunning)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_TimeSheet_VehicleTimeList(dateFrom, dateTo, isOpen, isAccept, isReject, isGet, isComplete, isRunning);
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

        public MON_DTOFLMDriverTimeSheet MONControlTower_TimeSheet_VehicleTimeGet(int timeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_TimeSheet_VehicleTimeGet(timeID);
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

        public void MONControlTower_TimeSheetDriverComplete(int timeID, DTOFLMAssetTimeSheetCheck item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTower_TimeSheetDriverComplete(timeID, item);
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

        public DTOMON_TimeSheetInfo MONControlTower_TimeSheetDriverQuickInfo(int timeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_TimeSheetDriverQuickInfo(timeID);
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

        public DTOFLMAssetTimeSheetCheck MONControlTower_TimeSheetDriver_CheckComplete(int timeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_TimeSheetDriver_CheckComplete(timeID);
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



        public List<DTOMON_CUSProduct> DIMonitor_GroupProductOfTOGroup(int TOGroupID, int productID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitor_GroupProductOfTOGroup(TOGroupID, productID);
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

        public void DIMonitorMaster_AddGroupProductFromDN(int opsGroupID, DTOMON_CUSProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_AddGroupProductFromDN(opsGroupID, item);
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


        public DTOResult DIMonitorMaster_NonTenderList(string request, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.DIMonitorMaster_NonTenderList(request, fDate, tDate);
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

        public void DIMonitorMaster_AddGroupProductFromNonTender(int masterID, List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_AddGroupProductFromNonTender(masterID, lst);
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

        public void DIMonitorMaster_GroupProduct_Split(int gopID, double value, int packingType)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_GroupProduct_Split(gopID, value, packingType);
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

        public void DIMonitorMaster_CancelMissingProduct(int opsGroupID, double quantity, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_CancelMissingProduct(opsGroupID, quantity, reasonID, reasonNote);
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

        public void DIMonitorMaster_ReturnMissingProductToOPS(int opsGroupID, double quantity)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_ReturnMissingProductToOPS(opsGroupID, quantity);
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

        public void DIMonitorMaster_SplitDN(int opsGroupID, double quantity)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.DIMonitorMaster_SplitDN(opsGroupID, quantity);
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

        #region Tower CO

        public DTOResult MONControlTowerCO_OrderList(string request, DateTime dfrom, DateTime dto, bool isRunning, bool isComplete, bool isLoadAll)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_OrderList(request, dfrom, dto, isRunning, isComplete, isLoadAll);
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

        public DTOResult MONControlTowerCO_TOContainerList(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_TOContainerList(request, masterID);
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

        public DTOResult MONControlTowerCO_OPSCOTORead(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_OPSCOTORead(request, masterID);
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

        public DTOResult MONControlTowerCO_OPSCOTOContainerRead(string request, int masterID, int cotoid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_OPSCOTOContainerRead(request, masterID, cotoid);
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

        public DTOResult MONControlTowerCO_OPSCOTODetailRead(string request, int cotoid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_OPSCOTODetailRead(request, cotoid);
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

        public void MONControlTowerCO_OPSCOTOSave(List<DTOMONCOTO> lst, int type)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_OPSCOTOSave(lst, type);
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

        public void MONControlTowerCO_COTODeatailAddOPSTOContainer(int id, List<int> lstTOContainerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_COTODeatailAddOPSTOContainer(id, lstTOContainerID);
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

        public void MONControlTowerCO_Complete(List<DTO_TOMasterActualTime> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_Complete(lst);
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

        public void MONControlTowerCO_CompleteContainer(List<int> ListContainerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_CompleteContainer(ListContainerID);
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

        public void MONControlTowerCO_RevertMaster(List<int> ListMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_RevertMaster(ListMasterID);
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

        public void MONControlTowerCO_RevertContainer(List<int> ListContainerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_RevertContainer(ListContainerID);
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

        public DTOMONOPSTOMaster MONControlTowerCO_MasterGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_MasterGet(id);
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

        public DTOMONMonitor_OrderLog MONControlTowerCO_OrderLogList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_OrderLogList(masterID);
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

        public List<DTOOPSDITOLocation> MONControlTowerCO_GetLocationByMaster(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_GetLocationByMaster(masterID);
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

        public DTOResult MONControlTowerCO_COList(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_COList(request, masterID);
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

        public void MONControlTowerCO_COEdit(DTOMONCO_Container item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_COEdit(item);
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

        public DTOResult MONControlTowerCO_MasterLocation(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_MasterLocation(request, masterID);
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

        public void MONControlTowerCO_MasterLocationUpdate(int masterID, List<DTOMON_OPSTOLocation> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_MasterLocationUpdate(masterID, lst);
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

        public void MONControlTowerCO_MasterUpdate(DTOMONOPSTOMaster item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_MasterUpdate(item);
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

        public DTOResult MONControlTowerCO_RomoocList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_RomoocList(request);
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

        public void MONControlTowerCO_ReturnRomooc(List<int> lstRomoocID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ReturnRomooc(lstRomoocID);
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

        public DTOResult MONControlTowerCO_TroubleList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_TroubleList(masterID);
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

        public DTOResult MONControlTowerCO_RoutingList(int masterID, int? locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_RoutingList(masterID, locationID);
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

        public DTOResult MONControlTowerCO_TroubleNotinList(int masterID, int? locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_TroubleNotinList(masterID, locationID);
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

        public DTOResult MONControlTowerCO_MasterDriverList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_MasterDriverList(masterID);
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

        public void MONControlTowerCO_TroubleSaveAll(List<DTOCATTrouble> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_TroubleSaveAll(data);
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

        public int MONControlTowerCO_TroubleSave(DTOCATTrouble item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_TroubleSave(item);
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

        public void MONControlTowerCO_TroubleSaveList(List<DTOCATSYSCustomerTrouble> lst, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_TroubleSaveList(lst, masterID);
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

        public void MONControlTowerCO_TroubleDelete(DTOCATTrouble item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_TroubleDelete(item);
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

        public DTOMON_Schedule_Data MONControlTowerCO_Schedule(DTOMONControlTower_ObjectFilter item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_Schedule(item);
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

        public List<DTOMON_Schedule_Resource> MONControlTowerCO_TractorShedulerResource(DTOMONTimeLineFilter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_TractorShedulerResource(filter);
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

        public List<DTOMON_Schedule_Task> MONControlTowerCO_TractorShedulerTask(DTOMONTimeLineFilter filter, List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_TractorShedulerTask(filter, lst);
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

        public List<DTOMON_Schedule_Resource> MONControlTowerCO_RomoocShedulerResource(DTOMONTimeLineFilter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_RomoocShedulerResource(filter);
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

        public List<DTOMON_Schedule_Task> MONControlTowerCO_RomoocShedulerTask(DTOMONTimeLineFilter filter, List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_RomoocShedulerTask(filter, lst);
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

        public void MONControlTowerCO_SchedulerSaveChance(DTOOPSDITOMaster item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_SchedulerSaveChance(item);
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

        public void MONControlTowerCO_MasterActualChange(DTOMON_OPSMaster item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_MasterActualChange(item);
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

        public void MONControlTowerCO_RepairContainer(int masterID, int ordContainerID, int locationID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_RepairContainer(masterID, ordContainerID, locationID, reasonID, reasonNote);
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

        public void MONControlTowerCO_CutContainer(int masterID, int ordContainerID, int locationID, string containerNo, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_CutContainer(masterID, ordContainerID, locationID, containerNo, reasonID, reasonNote);
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

        public int MONCO_TOContainer_StartOffer(int opsTOContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONCO_TOContainer_StartOffer(opsTOContainer);
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

        public void MONCO_TOContainer_StartOfferData(int opsTOContainer, DTOHelperTOMaster_Start item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONCO_TOContainer_StartOfferData(opsTOContainer, item);
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

        public void MONCO_TOContainer_Start(int opsTOContainer, bool isChangeRomooc)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONCO_TOContainer_Start(opsTOContainer, isChangeRomooc);
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

        public void MONControlTowerCO_ContainerStop(int opsTOContainer, int locationID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ContainerStop(opsTOContainer, locationID, reasonID, reasonNote);
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

        public void MONControlTowerCO_ChangeDepotGet(int opsTOContainer, int locationID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ChangeDepotGet(opsTOContainer, locationID, reasonID, reasonNote);
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

        public void MONControlTowerCO_AddDepotGet(int opsTOContainer, int locationID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_AddDepotGet(opsTOContainer, locationID, reasonID, reasonNote);
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

        public void MONControlTowerCO_ChangeDepotReturn(int opsTOContainer, int locationID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ChangeDepotReturn(opsTOContainer, locationID, reasonID, reasonNote);
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

        public void MONControlTowerCO_AddDepotReturn(int opsTOContainer, int locationID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_AddDepotReturn(opsTOContainer, locationID, reasonID, reasonNote);
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

        public void MONControlTowerCO_AddStock(int opsTOContainer, int locationID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_AddStock(opsTOContainer, locationID, reasonID, reasonNote);
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

        public void MONControlTowerCO_ContainerRepair(int opsTOContainer, int locationID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ContainerRepair(opsTOContainer, locationID, reasonID, reasonNote);
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

        public void MONControlTowerCO_ContainerStatusChange(int opsTOContainer, bool isCutRomooc, bool isPause, double stopHour, int reasonID, string reasonNote, int? vehicleID, string containerNo, string sealNo1, string sealNo2)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ContainerStatusChange(opsTOContainer, isCutRomooc, isPause, stopHour, reasonID, reasonNote, vehicleID, containerNo, sealNo1, sealNo2);
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

        public void MONControlTowerCO_ContainerCompleteCOList(int opstocoid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ContainerCompleteCOList(opstocoid);
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

        public int MONControlTowerCO_Continuous(int opstocoid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_Continuous(opstocoid);
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

        public void MONControlTowerCO_EndStation(int opstocoid, int? locationromoocid, int? locationvehicleid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_EndStation(opstocoid, locationromoocid, locationvehicleid);
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

        public void MONControlTowerCO_COTOKMUpdate(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_COTOKMUpdate(masterID);
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

        public void MONControlTowerCO_UnComplete(int opsTOContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_UnComplete(opsTOContainer);
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

        public OPSCOTOMaster MONControlTowerCO_Check_VehicleMaster(int id, int? romoocID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_Check_VehicleMaster(id, romoocID);
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

        public void MONControlTowerCO_ContainerCorrupt(int opsTOContainer, int locationID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ContainerCorrupt(opsTOContainer, locationID, reasonID, reasonNote);
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

        public void MONControlTowerCO_ContainerCutRomooc(int opsTOContainer, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ContainerCutRomooc(opsTOContainer, reasonID, reasonNote);
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

        public List<DTOCombobox> MONControlTower_ReasonChange()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTower_ReasonChange();
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

        public DTOMON_MainFilter MONControlTowerCO_MainFilter(DTOMONControlTower_ObjectFilter item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_MainFilter(item);
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

        public List<DTOMON_VehicleIconData> MONControlTowerCO_AllVehicle(bool showTractor, bool showRomooc)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_AllVehicle(showTractor, showRomooc);
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

        public DTOMON_OPSMaster MONControlTowerCO_MasterActual(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_MasterActual(masterID);
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

        public DTOMON_VehicleData MONControlTowerCO_GetVehicleStatus(string vehicleNo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_GetVehicleStatus(vehicleNo);
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

        public DTOResult MONControlTowerCO_DepotList(string request, int opsConID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_DepotList(request, opsConID);
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

        public void MONControlTowerCO_ChangeDepot(int masterID, int opscontainerID, int cuslocationID, int reasionID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ChangeDepot(masterID, opscontainerID, cuslocationID, reasionID, reasonNote);
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

        public List<DTOMONContainerScheduler> MONControlTowerCO_ContainerShedulerTask(DTOMONTimeLineFilter filter, List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_ContainerShedulerTask(filter, lst);
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

        public List<DTOMONOPSTO> MONControlTowerCO_ContainerShedulerResource(DTOMONTimeLineFilter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_ContainerShedulerResource(filter);
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


        public List<DTOOPSCO_Map_Schedule_Group> MONCTCO_TimelineChangePlan_TractorResource(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONCTCO_TimelineChangePlan_TractorResource(masterID);
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

        public List<DTOOPSCO_Map_Schedule_Event> MONCTCO_TimelineChangePlan_TractorEvent(List<int> lst, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONCTCO_TimelineChangePlan_TractorEvent(lst, masterID);
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

        public List<DTOMONOPSTO> MONControlTowerCO_ChangePlanByORDCont_Resource(DTOMONTimeLineFilter filter, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_ChangePlanByORDCont_Resource(filter, masterID);
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

        public List<DTOMONContainerScheduler> MONControlTowerCO_ChangePlanByORDCont_Task(DTOMONTimeLineFilter filter, List<int> lst, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_ChangePlanByORDCont_Task(filter, lst, masterID);
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

        public void MONControlTowerCO_ChangeMasterPlan(int masterID, int planMasterID, bool isChangeMooc, DateTime? etd)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_ChangeMasterPlan(masterID, planMasterID, isChangeMooc, etd);
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

        public void MONControlTowerCO_MasterChangeETD(int masterID, DateTime etd)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_MasterChangeETD(masterID, etd);
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

        public int MONControlTowerCO_MONCO_Continue(int masterID, int opscontainerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_MONCO_Continue(masterID, opscontainerid);
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

        public DTOResult MONControlTowerCO_ListMasterByTractor(string request, int tractorID, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_ListMasterByTractor(request, tractorID, masterID);
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

        public void MONControlTowerCO_MONCO_End(int opscontainerid, int? locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_MONCO_End(opscontainerid, locationID);
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

        public void MONControlTowerCO_AddHourEmpty(int opsContainerID, double hour)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_AddHourEmpty(opsContainerID, hour);
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

        public DTOResult MONControlTowerCO_COTONonMasterList(string request, int packingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_COTONonMasterList(request, packingID);
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

        public DTOResult MONControlTowerCO_UnCompleteMasterList(string request, int packingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_UnCompleteMasterList(request, packingID);
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

        public void MONControlTowerCO_OrderLocal(int opsContainerID, List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_OrderLocal(opsContainerID, lst);
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

        public void MONControlTowerCO_OrderLocalByMaster(int opscontainerid, List<int> lstMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_OrderLocalByMaster(opscontainerid, lstMasterID);
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

        public int MONControlTowerCO_CheckMasterRomooc(int masterID, int planMasterID, DateTime? etd)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_CheckMasterRomooc(masterID, planMasterID, etd);
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

        public DTOResult MONControlTowerCO_SwapCOTONonMasterList(string request, int packingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_SwapCOTONonMasterList(request, packingID);
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

        public DTOResult MONControlTowerCO_SwapUnCompleteMasterList(string request, int packingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONControlTowerCO_SwapUnCompleteMasterList(request, packingID);
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

        public void MONControlTowerCO_OrderExport(int opsContainerID, List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_OrderExport(opsContainerID, lst);
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

        public void MONControlTowerCO_OrderExportByMaster(int opscontainerid, List<int> lstMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONControlTowerCO_OrderExportByMaster(opscontainerid, lstMasterID);
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

        #region Container Timeline

        public DTOResult MONCO_TimeLine_Vehicle_List(string request, DateTime fDate, DateTime tDate, int typeOfView, int vendorID, DTOMONCO_FilterTimeline filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONCO_TimeLine_Vehicle_List(request, fDate, tDate, typeOfView, vendorID, filter);
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

        public DTOMONCO_Schedule_Data MONCO_TimeLine_Schedule_Data(DateTime fDate, DateTime tDate, List<string> dataRes, DTOMONCO_FilterTimeline filter, int typeOfView)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONCO_TimeLine_Schedule_Data(fDate, tDate, dataRes, filter, typeOfView);
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

        #region Tender COntainer

        public DTOResult MONCO_TenderList(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONCO_TenderList(request, dtFrom, dtTO, listCustomerID);
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

        public void MONCO_TenderSave(DTOPODCOInput item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONCO_TenderSave(item);
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

        public void MONCO_TenderUpdateStatus(List<int> lst, int type)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONCO_TenderUpdateStatus(lst, type);
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

        #region Container Operation

        public DTOResult MONCO_COTORead(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONCO_COTORead(request);
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

        public DTOResult MONCO_COTOByMaster(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONCO_COTOByMaster(request, masterID);
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

        public void MONCO_COTOUpdateList(List<DTOMONCOTO> lst, int type)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONCO_COTOUpdateList(lst, type);
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

        public void MONCO_COTOUpdateCOTOContainer(int masterID, DTOMONOPSTO item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONCO_COTOUpdateCOTOContainer(masterID, item);
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

        public void MONCO_COTOUpdateKMGPS(List<DTOMONCOTO> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONCO_COTOUpdateKMGPS(lst);
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

        public void MONCO_COTOofMasterAdd(DTOMONCOTO item, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONCO_COTOofMasterAdd(item,masterID);
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

        public void MONCO_COTOofMasterRomove(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONCO_COTOofMasterRomove(id);
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

        #region Dock

        public DTOResult MONDock_VehicleList(string request, int locationID, DateTime dStart, DateTime dEnd)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONDock_VehicleList(request, locationID, dStart, dEnd);
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

        public List<DTOMONDockTimeline> MONDock_RegisterTimeline(List<int?> lstVehicleID, int vendorID, DateTime dStart, DateTime dEnd)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONDock_RegisterTimeline(lstVehicleID, vendorID, dStart, dEnd);
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

        public DTOMONOPSTODock MONDock_GetInfo(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONDock_GetInfo(id);
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

        public void MONDock_DockTimelineAccept(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONDock_DockTimelineAccept(id);
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

        public void MONDock_DockUpdateTime(DTOMONOPSTODock item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONDock_DockUpdateTime(item);
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

        #region Approve Cost

        public DTOResult MONCO_TroubleList(string request, DateTime dfrom, DateTime dto)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONCO_TroubleList(request, dfrom, dto);
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

        public DTOResult MONDI_TroubleList(string request, DateTime dfrom, DateTime dto)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONDI_TroubleList(request, dfrom, dto);
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

        public void MON_ApprovedTrouble(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MON_ApprovedTrouble(lst);
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

        public void MON_RevertApprovedTrouble(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MON_RevertApprovedTrouble(lst);
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


        public DTOResult MONDI_TroubleRead(string request, DateTime dfrom, DateTime dto)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONDI_TroubleRead(request, dfrom, dto);
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

        public void MONDI_TroubleApproved(List<DTOCATTrouble> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONDI_TroubleApproved(data);
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

        public void MONDI_TroubleReject(List<DTOCATTrouble> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONDI_TroubleReject(data);
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

        #region Import Data
        public List<DTOMONImport> MONImport_Data(DateTime dtFrom, DateTime dtTO, int cusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONImport_Data(dtFrom, dtTO, cusId);
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

        public DTOResult MONImport_Index_Setting_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONImport_Index_Setting_List(request);
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

        public DTOCUSSettingMON MONImport_Index_Setting_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONImport_Index_Setting_Get(id);
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

        public DTOCUSSettingMONImport MONImportInput_Index_Setting_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONImportInput_Index_Setting_Get(id);
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

        public void MONImport_Excel_Import(int sID, List<DTOMONImport> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONImport_Excel_Import(sID, data);
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

        public DTOResult MONImportInput_Index_Setting_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONImportInput_Index_Setting_List(request);
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

        public List<DTOMONInputExport> MONImportInput_Excel_Export(int sID, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONImportInput_Excel_Export(sID, dtFrom, dtTO);
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

        public void MONImportInput_Excel_Import(int sID, List<DTOMONInputImport> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONImportInput_Excel_Import(sID, data);
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

        //Import Ext
        public DTOMONExtImportExcel MONExtImport_Data(DateTime dtFrom, DateTime dtTO, int cusId, int sID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONExtImport_Data(dtFrom, dtTO, cusId, sID);
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

        public DTOResult MONExtImport_Index_Setting_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONExtImport_Index_Setting_List(request);
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

        public DTOCUSSettingMONExt MONExtImport_Index_Setting_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONExtImport_Index_Setting_Get(id);
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

        public void MONExtImport_Excel_Import(int sID, List<DTOMONExtImport> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONExtImport_Excel_Import(sID, data);
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

        #region Input

        #region DI FLM fee(chi phí xe tải nhà)
        public DTOResult MONInput_DIFLMFee_List(string request, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_DIFLMFee_List(request, dtFrom, dtTO);
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
        //public List<DTOMONDIFLMFeeExcel> MONInput_DIFLMFee_Export(DateTime dtFrom, DateTime dtTo)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLMonitor>())
        //        {
        //            return bl.MONInput_DIFLMFee_Export(dtFrom, dtTo);
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}

        //public void MONInput_DIFLMFee_Import(List<DTOMONDIFLMFeeImport> lst)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLMonitor>())
        //        {
        //            bl.MONInput_DIFLMFee_Import(lst);
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}
        public void MONInput_DIFLMFee_Save(DTOMONDIFLMFee item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_DIFLMFee_Save(item);
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
        public void MONInput_DIFLMFee_Approved(List<int> lst, int type)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_DIFLMFee_Approved(lst, type);
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

        public DTOMONFLMDIInputDriver MONInput_DIFLMFee_GetDrivers(int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_DIFLMFee_GetDrivers(DITOMasterID);
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
        public void MONInput_DIFLMFee_SaveDrivers(DTOMONFLMDIInputDriver item, int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_DIFLMFee_SaveDrivers(item, DITOMasterID);
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

        public DTOResult MONInput_DIFLMFee_TroubleCostList(string request, int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_DIFLMFee_TroubleCostList(request, DITOMasterID);
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

        public DTOResult MONInput_DIFLMFee_TroubleCostNotIn_List(string request, int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_DIFLMFee_TroubleCostNotIn_List(request, DITOMasterID);
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

        public void MONInput_DIFLMFee_TroubleCostNotIn_SaveList(List<int> lst, int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_DIFLMFee_TroubleCostNotIn_SaveList(lst, DITOMasterID);
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

        public void MONInput_DIFLMFee_TroubleCost_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_DIFLMFee_TroubleCost_DeleteList(lst);
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

        public void MONInput_DIFLMFee_TroubleCostSave(List<DTOMONCATTroubleCost> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_DIFLMFee_TroubleCostSave(lst);
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

        public DTOResult MONInput_DIFLMFee_StationCostList(string request, int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_DIFLMFee_StationCostList(request, DITOMasterID);
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
        public void MONInput_DIFLMFee_StationCostSave(List<DTOMONOPSDITOStation> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_DIFLMFee_StationCostSave(lst);
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

        public DTOResult MONInput_DIFLMFee_DriverList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_DIFLMFee_DriverList();
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

        public List<DTOMONCATTrouble> PODGroupOfTrouble_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.PODGroupOfTrouble_List();
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

        #region CO FLM fee(chi phí xe công nhà)
        public DTOResult MONInput_COFLMFee_List(string request, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_COFLMFee_List(request, dtFrom, dtTO);
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

        public void MONInput_COFLMFee_Save(DTOMONCOFLMFee item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_COFLMFee_Save(item);
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
        public void MONInput_COFLMFee_Approved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_COFLMFee_Approved(lst);
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

        public DTOMONFLMCOInputDriver MONInput_COFLMFee_GetDrivers(int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_COFLMFee_GetDrivers(COTOMasterID);
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
        public void MONInput_COFLMFee_SaveDrivers(DTOMONFLMCOInputDriver item, int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_COFLMFee_SaveDrivers(item, COTOMasterID);
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

        public DTOResult MONInput_COFLMFee_TroubleCostList(string request, int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_COFLMFee_TroubleCostList(request, COTOMasterID);
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

        public DTOResult MONInput_COFLMFee_TroubleCostNotIn_List(string request, int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_COFLMFee_TroubleCostNotIn_List(request, COTOMasterID);
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

        public void MONInput_COFLMFee_TroubleCostNotIn_SaveList(List<int> lst, int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_COFLMFee_TroubleCostNotIn_SaveList(lst, COTOMasterID);
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

        public void MONInput_COFLMFee_TroubleCost_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_COFLMFee_TroubleCost_DeleteList(lst);
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
        public void MONInput_COFLMFee_TroubleCostSave(List<DTOMONCATTroubleCost> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_COFLMFee_TroubleCostSave(lst);
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

        public DTOResult MONInput_COFLMFee_StationCostList(string request, int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_COFLMFee_StationCostList(request, COTOMasterID);
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
        public void MONInput_COFLMFee_StationCostSave(List<DTOMONOPSCOTOStation> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_COFLMFee_StationCostSave(lst);
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

        public DTOResult MONInput_COFLMFee_DriverList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_COFLMFee_DriverList();
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

        #region input production (nhap san luong nho)
        public DTOResult MONInput_InputProduction_List(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID, bool hasIsReturn)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_InputProduction_List(request, dtFrom, dtTO, listCustomerID, hasIsReturn);
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
        public void MONInput_InputProduction_Save(DTOMONInputProduction item, bool ispod)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_InputProduction_Save(item, ispod);
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
        public DTOResult MONInput_InputProduction_Vendor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_InputProduction_Vendor_List();
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
        public void MONInput_InputProduction_ChangeComplete(List<int> lstDITOGroupID, bool isComplete)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_InputProduction_ChangeComplete(lstDITOGroupID, isComplete);
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
        public DTOMONInputProductionSplitDN MONInput_InputProduction_SplitDNGet(int DITOGroupProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_InputProduction_SplitDNGet(DITOGroupProductID);
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
        public void MONInput_InputProduction_SplitDNSave(DTOMONInputProductionSplitDN item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_InputProduction_SplitDNSave(item);
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
        public DTOMONInputProductionAddReturnData MONInput_InputProduction_AddReturnGet(int DITOGroupProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_InputProduction_AddReturnGet(DITOGroupProductID);
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
        public void MONInput_InputProduction_AddReturnSave(DTOMONInputProductionAddReturn item, int DITOGroupProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_InputProduction_AddReturnSave(item, DITOGroupProductID);
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
        public DTOMONInputProductionAddReturnData MONInput_InputProduction_AddReturn_EditGet(int DITOProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONInput_InputProduction_AddReturn_EditGet(DITOProductID);
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
        public void MONInput_InputProduction_AddReturnEditDelete(int DITOProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONInput_InputProduction_AddReturnEditDelete(DITOProductID);
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

        public DTOResult MONCOInput_List(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID, int type)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONCOInput_List(request, dtFrom, dtTO, listCustomerID, type);
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

        public void MONCOInput_Save(DTOPODCOInput item, bool ispod)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONCOInput_Save(item, ispod);
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

        #region cong no tra ve
        public DTOResult MONOPSExtReturn_List(string request, DateTime dFrom, DateTime dTo, List<int> lstCustomer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_List(request, dFrom, dTo, lstCustomer);
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
        public List<DTOMONOPSExtReturnExport> MONOPSExtReturn_Data(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_Data(dFrom, dTo);
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
        public DTOMONOPSExtReturn MONOPSExtReturn_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_Get(id);
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
        public void MONOPSExtReturn_Delete(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONOPSExtReturn_Delete(lst);
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
        public void MONOPSExtReturn_Approved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONOPSExtReturn_Approved(lst);
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
        public void MONOPSExtReturn_UnApproved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONOPSExtReturn_UnApproved(lst);
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
        public int MONOPSExtReturn_Save(DTOMONOPSExtReturn item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_Save(item);
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
        public DTOResult MONOPSExtReturn_DetailList(string request, int ExtReturnID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_DetailList(request, ExtReturnID);
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
        public void MONOPSExtReturn_DetailSave(List<DTOMONOPSExtReturnDetail> lst, int ExtReturnID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONOPSExtReturn_DetailSave(lst, ExtReturnID);
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
        public DTOResult MONOPSExtReturn_CustomerList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_CustomerList();
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
        public DTOResult MONOPSExtReturn_GOPByCus(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_GOPByCus(cusID);
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
        public DTOResult MONOPSExtReturn_ProductByGOP(int gopID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_ProductByGOP(gopID);
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
        public List<DTOCATVehicle> MONOPSExtReturn_VehicleList(int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_VehicleList(vendorID);
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
        public List<DTOFLMDriver> MONOPSExtReturn_DriverList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_DriverList();
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
        public DTOResult MONOPSExtReturn_VendorList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_VendorList();
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
        public DTOResult MONOPSExtReturn_DetailNotIn(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_DetailNotIn(request, masterID);
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
        public List<DTOMONTOMaster> MONOPSExtReturn_DITOMasterList(int cusID, int vendorID, int vehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_DITOMasterList(cusID, vendorID, vehicleID);
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

        public DTOResult MONOPSExtReturn_FindList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_FindList(request);
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
        public DTOResult MONOPSExtReturn_QuickList(string request, DateTime dFrom, DateTime dTo, List<int> lstCustomer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_QuickList(request, dFrom, dTo, lstCustomer);
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
        public void MONOPSExtReturn_QuickSave(DTOMONOPSDITOProductQuick item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    bl.MONOPSExtReturn_QuickSave(item);
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
        public DTOMONOPSExtReturnData MONOPSExtReturn_QuickData()
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_QuickData();
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

        public DTOResult MONOPSExtReturn_Setting_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_Setting_List(request);
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

        public DTOCUSSettingExtReturn MONOPSExtReturn_Setting_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_Setting_Get(id);
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

        public SYSExcel MONOPSExtReturn_ExcelOnline_Init(int templateID, DateTime dFrom, DateTime dTo, List<int> lstCustomer, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_ExcelOnline_Init(templateID, dFrom, dTo, lstCustomer, functionid, functionkey, isreload);
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

        public Row MONOPSExtReturn_ExcelOnline_Change(int templateID, DateTime dFrom, DateTime dTo, List<int> lstCustomer, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_ExcelOnline_Change(templateID, dFrom, dTo, lstCustomer, id, row, cells, lstMessageError);
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

        public SYSExcel MONOPSExtReturn_ExcelOnline_Import(int templateID, DateTime dFrom, DateTime dTo, List<int> lstCustomer, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_ExcelOnline_Import(templateID, dFrom, dTo, lstCustomer, id, lst, lstMessageError);
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

        public bool MONOPSExtReturn_ExcelOnline_Approve(long id, int templateID, DateTime dFrom, DateTime dTo, List<int> lstCustomer)
        {
            try
            {
                using (var bl = CreateBusiness<BLMonitor>())
                {
                    return bl.MONOPSExtReturn_ExcelOnline_Approve(id, templateID, dFrom, dTo, lstCustomer);
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

        #endregion
    }
}

