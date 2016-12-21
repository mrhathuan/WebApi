using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using DTO;
using CacheManager.Core;
using System.Web;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Presentation;
using Microsoft.SqlServer;
using System.IO;
using OfficeOpenXml;
using IServices;
using ServicesExtend;
using System.Globalization;
using System.Reflection;
namespace ClientWeb
{
    public class MONController : BaseController
    {
        #region Const

        const int iLO = -(int)SYSVarType.ServiceOfOrderLocal;
        const int iIM = -(int)SYSVarType.ServiceOfOrderImport;
        const int iEx = -(int)SYSVarType.ServiceOfOrderExport;
        const int iFCL = -(int)SYSVarType.TransportModeFCL;
        const int iFTL = -(int)SYSVarType.TransportModeFTL;
        const int iLTL = -(int)SYSVarType.TransportModeLTL;

        #endregion

        #region Common
        [HttpPost]
        public List<DTOMONTractor> TractorOwner_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOMONTractor>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.TractorOwner_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMONTruck> TruckOwner_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOMONTruck>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.TruckOwner_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMONRomooc> RomoocOwner_List(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOMONRomooc>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.RomoocOwner_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public void TractorOwner_UpdateFromGPS(dynamic dynParam)
        {
            try
            {
                List<DTOMONVehicle> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONVehicle>>(dynParam.lst.ToString());
                ServiceFactory.SVOther((ISVOther sv) =>
                {
                    foreach (var item in lst.Where(c => c.GPSCode != null && c.GPSCode != string.Empty))
                    {
                        var gps = sv.VehiclePosition_GetLast(item.GPSCode, DateTime.Now);
                        if (gps != null)
                        {
                            item.Lat = gps.Lat;
                            item.Lng = gps.Lng;
                        }
                    }
                });
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.TractorOwner_UpdateFromGPS(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void TruckOwner_UpdateFromGPS(dynamic dynParam)
        {
            try
            {
                List<DTOMONVehicle> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONVehicle>>(dynParam.lst.ToString());
                ServiceFactory.SVOther((ISVOther sv) =>
                {
                    foreach (var item in lst.Where(c => c.GPSCode != null && c.GPSCode != string.Empty))
                    {
                        var gps = sv.VehiclePosition_GetLast(item.GPSCode, DateTime.Now);
                        item.Lat = gps.Lat;
                        item.Lng = gps.Lng;
                    }
                });
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.TractorOwner_UpdateFromGPS(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion


        #region Extend

        [HttpPost]
        public List<DTOOtherVehiclePosition> Extend_VehiclePosition_Get(dynamic dynParam)
        {
            try
            {
                string vehicleCode = dynParam.vehicleCode.ToString();
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<DTOOtherVehiclePosition>();
                ServiceFactory.SVOther((ISVOther sv) =>
                {
                    result = sv.VehiclePosition_Get(vehicleCode, dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOtherVehiclePosition Extend_VehiclePosition_GetLast(dynamic dynParam)
        {
            try
            {
                string vehicleCode = dynParam.vehicleCode.ToString();
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                var result = new DTOOtherVehiclePosition();
                ServiceFactory.SVOther((ISVOther sv) =>
                {
                    result = sv.VehiclePosition_GetLast(vehicleCode, dtfrom);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Common
        public DTOResult Monitor_DriverList()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.Driver_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult Monitor_TruckList()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.Truck_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult Tractor_List()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.Tractor_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult Romooc_List()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.Romooc_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //public ActionResult Monitor_TractorList()
        //{
        //    try
        //    {
        //        var result = new DTOResult();
        //        ServiceFactory.SVMonitor((ISVMonitor sv) =>
        //        {
        //            result = sv.Tractor_List();
        //        });
        //        return result;
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        //public ActionResult Monitor_RomoocList()
        //{
        //    try
        //    {
        //        var result = new DTOResult();
        //        ServiceFactory.SVMonitor((ISVMonitor sv) =>
        //        {
        //            result = sv.Romooc_List();
        //        });
        //        return result;
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }
        //}

        public DTOResult Monitor_VendorList()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.Vendor_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult Customer_List()
        {
            try
            {
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.Customer_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATLocation_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                List<int> ignore = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.ignore.ToString());
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.CATLocation_List(request, ignore);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATStock_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int cusid = d.customerID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.CATStock_List(request, cusid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CATVehicle Vehicle_Create(dynamic d)
        {
            try
            {
                string vehicleNo = d.vehicleNo;
                int? vendorID = d.vendorID;
                int vehicleType = d.vehicleType;
                double maxWeight = d.maxWeight;
                var result = new CATVehicle();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.Vehicle_Create(vehicleNo, vendorID, vehicleType, maxWeight);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public CATVehicle Romooc_Create(dynamic d)
        {
            try
            {
                string vehicleNo = d.vehicleNo;
                int? vendorID = d.vendorID;
                int vehicleType = d.vehicleType;
                double maxWeight = d.maxWeight;
                var result = new CATVehicle();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.Romooc_Create(vehicleNo, vendorID, vehicleType, maxWeight);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult CATLocation_SeaPortList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int opsTOContainer = d.opsTOContainer;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.CATLocation_SeaPortList(request, opsTOContainer);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_COTOStockRead(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int opscontainerID = d.opscontainerID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_COTOStockRead(request, opscontainerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_StandDetailList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int opsTOContainer = d.opsTOContainer;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_StandDetailList(request, opsTOContainer);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_OrderEXIM(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int opsTOContainer = d.opsTOContainer;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_OrderEXIM(request, opsTOContainer);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_OrderLocalOffer(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int opsTOContainer = d.opsTOContainer;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_OrderLocalOffer(request, opsTOContainer);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public List<DTOCATLocation> MONControlTowerCO_VehicleLocationDefault(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                var result = new List<DTOCATLocation>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_VehicleLocationDefault(opsTOContainer);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Truck-New


        public DTOMONOPSTOMaster DIMonitor_VehicleTimeGet(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = new DTOMONOPSTOMaster();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitor_VehicleTimeGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTO_MONDITO> DIMonitor_MsComplete(dynamic d)
        {
            try
            {
                List<DTO_TOMasterActualTime> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTO_TOMasterActualTime>>(d.lst.ToString());
                var result = new List<DTO_MONDITO>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorMaster_Complete(lst);

                    //ServiceFactory.SVOther((ISVOther oth) =>
                    //{
                    //    foreach (var obj in result)
                    //    {
                    //        if (obj.ATA.HasValue && obj.ATD.HasValue)
                    //        {
                    //            var lstGPS = oth.VehiclePosition_Get(obj.RegNo, obj.ATD.Value, obj.ATA.Value);
                    //            double km = 0;
                    //            for (int i = 1; i < lstGPS.Count; i++)
                    //            {
                    //                km += GetDistance(lstGPS[i - 1].Lat, lstGPS[i - 1].Lng, lstGPS[i].Lat, lstGPS[i].Lng);
                    //            }
                    //            obj.KM = km;
                    //        }
                    //    }
                    //});
                    //sv.DIMonitorMaster_UpdateDITO(result);
                });


                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private double GetDistance(double lat1, double lon1, double lat2, double lon2)
        {
            var R = 6371;
            var dLat = ToRad(lat2 - lat1);
            var dLon = ToRad(lon2 - lon1);
            lat1 = ToRad(lat1);
            lat2 = ToRad(lat2);
            var a = Math.Pow(Math.Sin(dLat / 2), 2) + (Math.Pow(Math.Sin(dLon / 2), 2) * Math.Cos(lat1) * Math.Cos(lat2));
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c;
            return distance;
        }
        private double ToRad(double degs)
        {
            return degs * (Math.PI / 180.0);
        }
        [HttpPost]
        public bool DIMonitor_MsCompleteDN(dynamic d)
        {
            try
            {
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lstID.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_CompleteDN(lstID);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool DIMonitor_MsRevert(dynamic d)
        {
            try
            {
                List<int> lstMasterID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lstMasterID.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_Revert(lstMasterID);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool DIMonitor_MsRevertDN(dynamic d)
        {
            try
            {
                List<int> lstID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lstID.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_RevertDN(lstID);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult DIMonitor_MsTroubleList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int? locationID = d.locationID != null ? (int?)d.locationID : null;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorTrouble_List(masterID, locationID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOCATTrouble DIMonitorTrouble_Get(dynamic d)
        {
            try
            {
                int troubleID = d.troubleID;
                var result = default(DTOCATTrouble);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorTrouble_Get(troubleID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCATTrouble DIMonitorTrouble_Save(dynamic d)
        {
            try
            {
                DTOCATTrouble item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTrouble>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    item.ID = sv.DIMonitorTrouble_Save(item);
                });
                return item;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool DIMonitorTrouble_SaveAll(dynamic d)
        {
            try
            {
                List<DTOCATTrouble> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATTrouble>>(d.data.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorTrouble_SaveAll(data);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool DIMonitorTrouble_Delete(dynamic d)
        {
            try
            {
                DTOCATTrouble item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTrouble>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorTrouble_Delete(item);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult DIMonitor_CATTroubleList(dynamic d)
        {
            try
            {
                bool isCO = false;
                if (d.isCo != null)
                    isCO = d.isCo;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.CATTrouble_List(isCO);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult DIMonitor_MsRoutingList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int? locationID = d.locationID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DITOMasterRouting_List(masterID, locationID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public bool DIMonitor_MsUpdate(dynamic d)
        {
            try
            {
                DTOMONOPSTOMaster item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONOPSTOMaster>(d.item.ToString());
                List<DTOMONOPSTODetail> lstDetail = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONOPSTODetail>>(d.lstDetail.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_Update(item, lstDetail);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult DIMonitor_TruckList(dynamic d)
        {
            try
            {
                int? vendorID = d.vendorID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.TruckByVendorID_List(vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DTOResult DIMonitor_MsDITOLocationList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorMaster_DITOLocationList(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        [HttpPost]
        public DTOResult DIMonitorMaster_GOPReturnList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorMaster_GOPReturnList(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool DIMonitorMaster_GOPReturnSave(dynamic d)
        {
            try
            {
                List<DTOMONOPSTODetail> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONOPSTODetail>>(d.data.ToString());
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_GOPReturnSave(data, masterID);
                });
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSGroupOfProduct> DIMonitorMaster_CUSGOPList(dynamic d)
        {
            try
            {
                int customerid = d.customerid;
                var result = new List<CUSGroupOfProduct>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorMaster_CUSGOPList(customerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult DIMonitorMaster_SOPartnerLocation(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int cuspartID = d.cuspartID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorMaster_SOPartnerLocation(request, cuspartID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> DIMonitorMaster_SOPartnerList(dynamic d)
        {
            try
            {
                int customerID = d.customerID;
                var result = new List<DTOCombobox>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorMaster_SOPartnerList(customerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void DIMonitorMaster_ChangeSOLocation(dynamic d)
        {
            try
            {
                int opsGroupID = d.opsGroupID;
                int cuslocationID = d.cuslocationID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_ChangeSOLocation(cuslocationID, opsGroupID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int DIMonitorMaster_PartnerLocationSave(dynamic d)
        {
            try
            {
                int cuspartID = d.cuspartID;
                DTOCUSPartnerLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSPartnerLocation>(d.item.ToString());
                int result = -1;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorMaster_PartnerLocationSave(item, cuspartID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Return

        public void DIMonitorMaster_GOPReturnAdd(dynamic d)
        {
            try
            {
                DTOMONProductReturn item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONProductReturn>(d.item.ToString());
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_GOPReturnAdd(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorMaster_GOPReturnDeleteList(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_GOPReturnDeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOCombobox> DIMonitor_DITOGroupProductList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                var result = new List<DTOCombobox>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitor_DITOGroupProductList(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOCUSProduct> DIMonitor_CUSProductList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                var result = new List<DTOCUSProduct>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitor_CUSProductList(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorMaster_SL_Save(dynamic d)
        {
            try
            {
                List<DTOMONOPSTODetail> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONOPSTODetail>>(d.lst.ToString());
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_SL_Save(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult DIMonitorMaster_SL_List(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorMaster_SL_List(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOCombobox> DIMonitor_TOGroupProductCancelReason(dynamic d)
        {
            try
            {
                var result = new List<DTOCombobox>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitor_TOGroupProductCancelReason();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorMaster_CancelTOGroup(dynamic d)
        {
            try
            {
                int opsGroupID = d.opsGroupID;
                int reasonID = d.reasonID;
                string note = d.note;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_CancelTOGroup(opsGroupID, reasonID, note);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMON_TOProductOfGroup> DIMonitor_DITOProductOfGroup(dynamic d)
        {
            try
            {
                int TOGroupProductID = d.TOGroupProductID;
                var result = new List<DTOMON_TOProductOfGroup>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitor_DITOProductOfGroup(TOGroupProductID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Control tower

        public DTOResult MONControlTower_OrderList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                DateTime pFrom = Convert.ToDateTime(d.DateFrom.ToString());
                DateTime pTo = Convert.ToDateTime(d.DateTo.ToString());
                bool isRunning = d.isRunning;
                bool isComplete = d.isComplete;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_OrderList(request, pFrom, pTo, isRunning, isComplete);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMONMonitor_OrderLog MONControlTower_OrderLogList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                var result = new DTOMONMonitor_OrderLog();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_OrderLogList(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_ApprovedTrouble(dynamic d)
        {
            try
            {
                int troubleID = d.troubleID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_ApprovedTrouble(troubleID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_RejectTrouble(dynamic d)
        {
            try
            {
                int troubleID = d.troubleID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_RejectTrouble(troubleID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMAPVehicle MONControlTower_VehicleGPS(dynamic d)
        {
            try
            {
                string vehicleCode = d.vehicleCode.ToString();
                DateTime dtfrom = DateTime.Now.AddMinutes(-5);
                DateTime dtto = DateTime.Now;
                var lst = new List<DTOOtherVehiclePosition>();
                ServiceFactory.SVOther((ISVOther sv) =>
                {
                    lst = sv.VehiclePosition_Get(vehicleCode, dtfrom, dtto);
                });

                DTOMAPVehicle result = new DTOMAPVehicle();
                if (lst.Count > 0)
                {
                    var obj = lst[lst.Count - 1];
                    result.Lat = obj.Lat;
                    result.Lng = obj.Lng;
                    result.ID = 0;
                    result.RegNo = vehicleCode;
                }

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMONMonitor_OPSSummary MONControlTower_FilterByRoute(dynamic d)
        {
            try
            {
                int routeID = d.routeID;
                DateTime dfrom = d.dfrom;
                DateTime dto = d.dto;
                DTOMONMonitor_OPSSummary result = new DTOMONMonitor_OPSSummary();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_FilterByRoute(routeID, dfrom, dto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMON_MainFilter MONControlTower_MainVehicleFilter(dynamic d)
        {
            try
            {
                DTOMON_MainFilter result = new DTOMON_MainFilter();
                DTOMONControlTower_ObjectFilter item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONControlTower_ObjectFilter>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_MainVehicleFilter(item);
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public DTOMON_Schedule_Data MONControlTower_Schedule(dynamic d)
        {
            try
            {
                DTOMON_Schedule_Data result = new DTOMON_Schedule_Data();
                DTOMONControlTower_ObjectFilter item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONControlTower_ObjectFilter>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_Schedule(item);
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public List<DTOOPSDITOLocation> MONControlTower_GetLocationByMaster(dynamic d)
        {
            try
            {
                var result = new List<DTOOPSDITOLocation>();
                int masterID = d.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_GetLocationByMaster(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMON_WFL_Message> MONControlTower_GetAllNotification(dynamic d)
        {
            try
            {
                var result = new List<DTOMON_WFL_Message>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_GetAllNotification();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOOPSRouteProblem> MONControlTower_ProblemList(dynamic d)
        {
            try
            {
                var result = new List<DTOOPSRouteProblem>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_ProblemList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_SchedulerSaveChance(dynamic d)
        {
            try
            {
                DTOOPSDITOMaster item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDITOMaster>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_SchedulerSaveChance(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTower_DIStationList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_DIStationList(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTower_TruckRead(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                DateTime? dfrom = Convert.ToDateTime(d.DateFrom.ToString());
                DateTime? dto = d.DateTo;
                int? vendorID = d.vendorID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_TruckRead(request, vendorID, dfrom, dto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTower_DriverRead(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                DateTime? dfrom = d.DateFrom == null ? null : Convert.ToDateTime(d.DateFrom.ToString());
                DateTime? dto = d.DateTo == null ? null : Convert.ToDateTime(d.DateTo.ToString());
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_DriverRead(request, dfrom, dto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTower_DIStationNotinList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_DIStationNotinList(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTower_DILocation(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_DILocation(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_DIStationAdd(dynamic d)
        {
            try
            {
                List<int> ListStationID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.ListStationID.ToString());
                DTOMON_OPSLocation opsLocation = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMON_OPSLocation>(d.opsLocation.ToString());
                int masterID = d.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_DIStationAdd(masterID, ListStationID, opsLocation);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_DIStationRemove(dynamic d)
        {
            try
            {
                List<int> ListID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.ListID.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_DIStationRemove(ListID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_DIStationApprove(dynamic d)
        {
            try
            {
                int id = d.id;
                bool value = d.value;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_DIStationApprove(id, value);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_DIStationSaveChanges(dynamic d)
        {
            try
            {
                List<DTOMON_Station> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMON_Station>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_DIStationSaveChanges(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_ChangeVehicle(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int vehicleID = d.vehicleID;
                int vendorID = d.vendorID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_ChangeVehicle(masterID, vehicleID, vendorID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_ChangeDriver(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int driverID = d.driverID;
                int typeOfDriver = d.typeOfDriver;
                string reason = d.reason;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_ChangeDriver(masterID, driverID, typeOfDriver, reason);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_TOMaster_Split(dynamic d)
        {
            try
            {
                int toID = d.masterID;
                int? vendorID = d.vendorID;
                int vehicleID = d.vehicleID;
                DateTime ETD = Convert.ToDateTime(d.ETD.ToString());
                DateTime ETA = Convert.ToDateTime(d.ETA.ToString());
                string driverName = d.driverName;
                string driverTel = d.driverTel;
                int fLocation = d.fLocation;
                int tLocation = d.tLocation;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_TOMaster_Split(toID, vendorID, vehicleID, ETD, ETA, driverName, driverTel, fLocation, tLocation);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMAPVehicle MONControlTower_GetTroubleLocation(dynamic d)
        {
            try
            {
                int troubleID = d.troubleID;
                var result = new DTOMAPVehicle();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_GetTroubleLocation(troubleID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMONDriver> MONControlTower_SupDriverRead(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                var result = new List<DTOMONDriver>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_SupDriverRead(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_ChangeSupDriver(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                List<DTOMONDriver> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONDriver>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_ChangeSupDriver(masterID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DTOMON_CUSProduct> DIMonitor_GroupProductOfTOGroup(dynamic d)
        {
            try
            {
                var result = new List<DTOMON_CUSProduct>();
                int TOGroupID = d.TOGroupID;
                int productID = d.productID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitor_GroupProductOfTOGroup(TOGroupID, productID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorMaster_AddGroupProductFromDN(dynamic d)
        {
            try
            {
                int opsGroupID = d.opsGroupID;
                DTOMON_CUSProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMON_CUSProduct>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_AddGroupProductFromDN(opsGroupID, item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult DIMonitorMaster_NonTenderList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                DateTime? fDate = null;
                if (d.fDate != null)
                    fDate = Convert.ToDateTime(d.fDate);
                DateTime? tDate = null;
                if (d.tDate != null)
                    tDate = Convert.ToDateTime(d.tDate);

                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorMaster_NonTenderList(request, fDate, tDate);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorMaster_AddGroupProductFromNonTender(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_AddGroupProductFromNonTender(masterID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorMaster_GroupProduct_Split(dynamic d)
        {
            try
            {
                int gopID = d.gopID;
                int value = d.value;
                int packingType = d.packingType;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_GroupProduct_Split(gopID, value, packingType);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorMaster_CancelMissingProduct(dynamic d)
        {
            try
            {
                int opsGroupID = d.opsGroupID;
                double quantity = d.quantity;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_CancelMissingProduct(opsGroupID, quantity, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorMaster_ReturnMissingProductToOPS(dynamic d)
        {
            try
            {
                int opsGroupID = d.opsGroupID;
                double quantity = d.quantity;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_ReturnMissingProductToOPS(opsGroupID, quantity);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorMaster_SplitDN(dynamic d)
        {
            try
            {
                int opsGroupID = d.opsGroupID;
                double quantity = d.quantity;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_SplitDN(opsGroupID, quantity);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region FLMDriverTimeSheet
        [HttpPost]
        public List<DTOFLMVehicle> MONControlTower_TimeSheet_VehicleList()
        {
            try
            {
                var result = new List<DTOFLMVehicle>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_TimeSheet_VehicleList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOMON_TimeSheetDriver MONControlTower_TimeSheet_VehicleTimeList(dynamic dynParam)
        {
            try
            {
                var result = new DTOMON_TimeSheetDriver();
                DateTime dateFrom = (DateTime)dynParam.dateFrom;
                DateTime dateTo = (DateTime)dynParam.dateTo;
                bool isOpen = dynParam.isOpen;
                bool isAccept = dynParam.isAccept;
                bool isReject = dynParam.isReject;
                bool isGet = dynParam.isGet;
                bool isComplete = dynParam.isComplete;
                bool isRunning = dynParam.isRunning;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_TimeSheet_VehicleTimeList(dateFrom, dateTo, isOpen, isAccept, isReject, isGet, isComplete, isRunning);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public MON_DTOFLMDriverTimeSheet MONControlTower_TimeSheet_VehicleTimeGet(dynamic dynParam)
        {
            try
            {
                var result = new MON_DTOFLMDriverTimeSheet();
                int timeID = (int)dynParam.timeID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_TimeSheet_VehicleTimeGet(timeID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOMON_TimeSheetInfo MONControlTower_TimeSheetDriverQuickInfo(dynamic dynParam)
        {
            try
            {
                var result = new DTOMON_TimeSheetInfo();
                int timeID = (int)dynParam.timeID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_TimeSheetDriverQuickInfo(timeID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONControlTower_TimeSheet_ApproveDriver(dynamic dynParam)
        {
            try
            {
                int timeDriverID = (int)dynParam.timeDriverID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_TimeSheet_ApproveDriver(timeDriverID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONControlTower_TimeSheet_RejectDriver(dynamic dynParam)
        {
            try
            {
                int timeDriverID = (int)dynParam.timeDriverID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_TimeSheet_RejectDriver(timeDriverID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONControlTower_ChangeTimeSheetDriver(dynamic dynParam)
        {
            try
            {
                int timeID = (int)dynParam.timeID;
                int flmDriverID = (int)dynParam.flmDriverID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_ChangeTimeSheetDriver(timeID, flmDriverID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONControlTower_TimeSheetDriverComplete(dynamic dynParam)
        {
            try
            {
                int timeID = (int)dynParam.timeID;
                DTOFLMAssetTimeSheetCheck item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFLMAssetTimeSheetCheck>(dynParam.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_TimeSheetDriverComplete(timeID, item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOFLMAssetTimeSheetCheck MONControlTower_TimeSheetDriver_CheckComplete(dynamic dynParam)
        {
            try
            {
                var result = new DTOFLMAssetTimeSheetCheck();
                int timeID = (int)dynParam.timeID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_TimeSheetDriver_CheckComplete(timeID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #endregion

        #region Container Timeline

        [HttpPost]
        public DTOResult MONCO_TimeLine_Vehicle_List(dynamic dynParam)
        {
            try
            {
                var result = new DTOResult();
                string request = dynParam.request.ToString();
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                int typeOfView = (int)dynParam.typeOfView;
                int vendorID = (int)dynParam.vendorID;
                DTOMONCO_FilterTimeline filter = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONCO_FilterTimeline>(dynParam.filter.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONCO_TimeLine_Vehicle_List(request, fDate, tDate, typeOfView, vendorID, filter);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOMONCO_Schedule_Data MONCO_TimeLine_Schedule_Data(dynamic dynParam)
        {
            try
            {
                var result = new DTOMONCO_Schedule_Data();
                int type = dynParam.typeOfView;
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                List<string> dataRes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.dataRes.ToString());
                DTOMONCO_FilterTimeline filter = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONCO_FilterTimeline>(dynParam.filter.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONCO_TimeLine_Schedule_Data(fDate, tDate, dataRes, filter, type);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Tower CO

        public DTOResult MONControlTowerCO_OrderList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                DateTime pFrom = Convert.ToDateTime(d.DateFrom.ToString());
                DateTime pTo = Convert.ToDateTime(d.DateTo.ToString());
                pTo = pTo.AddDays(1);
                bool isRunning = d.isRunning;
                bool isComplete = d.isComplete;
                bool isLoadAll = d.isLoadAll;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_OrderList(request, pFrom, pTo, isRunning, isComplete, isLoadAll);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_TOContainerList(dynamic d)
        {
            try
            {
                var result = new DTOResult();
                string request = d.request.ToString();
                int masterID = d.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_TOContainerList(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_OPSCOTORead(dynamic d)
        {
            try
            {
                var result = new DTOResult();
                string request = d.request.ToString();
                int masterID = d.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_OPSCOTORead(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_OPSCOTOContainerRead(dynamic d)
        {
            try
            {
                var result = new DTOResult();
                string request = d.request.ToString();
                int masterID = d.masterID;
                int cotoid = d.cotoid;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_OPSCOTOContainerRead(request, masterID, cotoid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_OPSCOTODetailRead(dynamic d)
        {
            try
            {
                var result = new DTOResult();
                string request = d.request.ToString();
                int cotoid = d.cotoid;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_OPSCOTODetailRead(request, cotoid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DTOMONContainerScheduler> MONControlTowerCO_ContainerShedulerTask(dynamic d)
        {
            try
            {
                var result = new List<DTOMONContainerScheduler>();
                DTOMONTimeLineFilter filter = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONTimeLineFilter>(d.filter.ToString());
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_ContainerShedulerTask(filter, lst);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMONOPSTO> MONControlTowerCO_ContainerShedulerResource(dynamic d)
        {
            try
            {
                var result = new List<DTOMONOPSTO>();
                DTOMONTimeLineFilter filter = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONTimeLineFilter>(d.filter.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_ContainerShedulerResource(filter);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOOPSCO_Map_Schedule_Event> MONCTCO_TimelineChangePlan_TractorEvent(dynamic d)
        {
            try
            {
                var result = new List<DTOOPSCO_Map_Schedule_Event>();
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                int masterID = d.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONCTCO_TimelineChangePlan_TractorEvent(lst, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMONOPSTO> MONControlTowerCO_ChangePlanByORDCont_Resource(dynamic d)
        {
            try
            {
                var result = new List<DTOMONOPSTO>();
                int masterID = d.masterID;
                DTOMONTimeLineFilter filter = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONTimeLineFilter>(d.filter.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_ChangePlanByORDCont_Resource(filter, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMONContainerScheduler> MONControlTowerCO_ChangePlanByORDCont_Task(dynamic d)
        {
            try
            {
                var result = new List<DTOMONContainerScheduler>();
                int masterID = d.masterID;
                DTOMONTimeLineFilter filter = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONTimeLineFilter>(d.filter.ToString());
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_ChangePlanByORDCont_Task(filter, lst, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ChangeMasterPlan(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int planMasterID = d.planMasterID;
                bool isChangeMooc = d.isChangeMooc;
                DateTime? etd = null;
                if (d.etd != null)
                    etd = Convert.ToDateTime(d.etd.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ChangeMasterPlan(masterID, planMasterID, isChangeMooc, etd);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_MasterChangeETD(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                DateTime etd = Convert.ToDateTime(d.etd.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_MasterChangeETD(masterID, etd);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int MONControlTowerCO_MONCO_Continue(dynamic d)
        {
            try
            {
                int opscontainerid = (int)d.opscontainerid;
                int masterID = d.masterID;
                var result = -1;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_MONCO_Continue(masterID, opscontainerid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_ListMasterByTractor(dynamic d)
        {
            try
            {
                var result = new DTOResult();
                string request = d.request.ToString();
                int masterID = d.masterID;
                int? tractorID = d.tractorID;
                if (!tractorID.HasValue)
                    tractorID = 0;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_ListMasterByTractor(request, tractorID.Value, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_MONCO_End(dynamic d)
        {
            try
            {
                int opscontainerid = (int)d.opscontainerid;
                int? locationID = d.locationID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_MONCO_End(opscontainerid, locationID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_AddHourEmpty(dynamic d)
        {
            try
            {
                int opscontainerid = (int)d.opscontainerid;
                double hour = d.hour;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_AddHourEmpty(opscontainerid, hour);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int MONControlTowerCO_CheckMasterRomooc(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int planMasterID = d.planMasterID;
                DateTime? etd = null;
                if (d.etd != null)
                    etd = Convert.ToDateTime(d.etd.ToString());
                int rs = 0;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    rs = sv.MONControlTowerCO_CheckMasterRomooc(masterID, planMasterID, etd);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOOPSCO_Map_Schedule_Group> MONCTCO_TimelineChangePlan_TractorResource(dynamic d)
        {
            try
            {
                var result = new List<DTOOPSCO_Map_Schedule_Group>();
                int masterID = d.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONCTCO_TimelineChangePlan_TractorResource(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DTOResult MONControlTowerCO_MasterLocation(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_MasterLocation(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_MasterLocationUpdate(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                List<DTOMON_OPSTOLocation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMON_OPSTOLocation>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_MasterLocationUpdate(masterID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_RomoocList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_RomoocList(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_OPSCOTOSave(dynamic d)
        {
            try
            {
                List<DTOMONCOTO> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONCOTO>>(d.lst.ToString());
                int type = d.type;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_OPSCOTOSave(lst, type);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_COTODeatailAddOPSTOContainer(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                int id = d.id;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_COTODeatailAddOPSTOContainer(id, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MONControlTowerCO_Complete(dynamic d)
        {
            try
            {
                List<DTO_TOMasterActualTime> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTO_TOMasterActualTime>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_Complete(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_CompleteContainer(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_CompleteContainer(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_RevertMaster(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_RevertMaster(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_RevertContainer(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_RevertContainer(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ReturnRomooc(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ReturnRomooc(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMONOPSTOMaster MONControlTowerCO_MasterGet(dynamic d)
        {
            try
            {
                int masterID = d.id;
                var result = new DTOMONOPSTOMaster();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_MasterGet(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMONMonitor_OrderLog MONControlTowerCO_OrderLogList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                var result = new DTOMONMonitor_OrderLog();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_OrderLogList(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOOPSDITOLocation> MONControlTowerCO_GetLocationByMaster(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                var result = new List<DTOOPSDITOLocation>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_GetLocationByMaster(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_COList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_COList(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_COEdit(dynamic d)
        {
            try
            {
                DTOMONCO_Container item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONCO_Container>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_COEdit(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_TroubleList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_TroubleList(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_RoutingList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int? locationID = d.locationID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_RoutingList(masterID, locationID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_TroubleNotinList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int? locationID = d.locationID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_TroubleNotinList(masterID, locationID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult MONControlTowerCO_MasterDriverList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_MasterDriverList(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_TroubleSaveAll(dynamic d)
        {
            try
            {
                List<DTOCATTrouble> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATTrouble>>(d.data.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_TroubleSaveAll(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_TroubleSave(dynamic d)
        {
            try
            {
                DTOCATTrouble item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTrouble>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_TroubleSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_TroubleSaveList(dynamic d)
        {
            try
            {
                int masterID = d.masterID; ;
                List<DTOCATSYSCustomerTrouble> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATSYSCustomerTrouble>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_TroubleSaveList(data, masterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_TroubleDelete(dynamic d)
        {
            try
            {
                DTOCATTrouble item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTrouble>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_TroubleDelete(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTower_COStationList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_COStationList(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTower_COStationNotinList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int masterID = d.masterID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_COStationNotinList(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_COStationAdd(dynamic d)
        {
            try
            {
                List<int> ListStationID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.ListStationID.ToString());
                int masterID = d.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_COStationAdd(masterID, ListStationID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_COStationRemove(dynamic d)
        {
            try
            {
                List<int> ListID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.ListID.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_COStationRemove(ListID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_COStationApprove(dynamic d)
        {
            try
            {
                int id = d.id;
                bool value = d.value;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_COStationApprove(id, value);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_COStationSaveChanges(dynamic d)
        {
            try
            {
                List<DTOMON_Station> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMON_Station>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_COStationSaveChanges(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMON_Schedule_Data MONControlTowerCO_Schedule(dynamic d)
        {
            try
            {
                DTOMON_Schedule_Data result = new DTOMON_Schedule_Data();
                DTOMONControlTower_ObjectFilter item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONControlTower_ObjectFilter>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_Schedule(item);
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public void MONControlTowerCO_SchedulerSaveChance(dynamic d)
        {
            try
            {
                DTOOPSDITOMaster item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOOPSDITOMaster>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_SchedulerSaveChance(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMON_Schedule_Resource> MONControlTowerCO_TractorShedulerResource(dynamic d)
        {
            try
            {
                List<DTOMON_Schedule_Resource> result = new List<DTOMON_Schedule_Resource>();
                DTOMONTimeLineFilter filter = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONTimeLineFilter>(d.filter.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_TractorShedulerResource(filter);
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public List<DTOMON_Schedule_Task> MONControlTowerCO_TractorShedulerTask(dynamic d)
        {
            try
            {
                List<DTOMON_Schedule_Task> result = new List<DTOMON_Schedule_Task>();
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                DTOMONTimeLineFilter filter = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONTimeLineFilter>(d.filter.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_TractorShedulerTask(filter, lst);
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public List<DTOMON_Schedule_Resource> MONControlTowerCO_RomoocShedulerResource(dynamic d)
        {
            try
            {
                List<DTOMON_Schedule_Resource> result = new List<DTOMON_Schedule_Resource>();
                DTOMONTimeLineFilter filter = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONTimeLineFilter>(d.filter.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_RomoocShedulerResource(filter);
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public List<DTOMON_Schedule_Task> MONControlTowerCO_RomoocShedulerTask(dynamic d)
        {
            try
            {
                List<DTOMON_Schedule_Task> result = new List<DTOMON_Schedule_Task>();
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                DTOMONTimeLineFilter filter = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONTimeLineFilter>(d.filter.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_RomoocShedulerTask(filter, lst);
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public void MONControlTowerCO_MasterUpdate(dynamic d)
        {
            try
            {
                DTOMONOPSTOMaster item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONOPSTOMaster>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_MasterUpdate(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_MasterActualChange(dynamic d)
        {
            try
            {
                DTOMON_OPSMaster item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMON_OPSMaster>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_MasterActualChange(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMON_OPSMaster MONControlTowerCO_MasterActual(dynamic d)
        {
            try
            {
                DTOMON_OPSMaster result = new DTOMON_OPSMaster();
                int masterID = d.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_MasterActual(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public void MONControlTower_ChangeTractor(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int vehicleID = d.vehicleID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_ChangeTractor(masterID, vehicleID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTower_ChangeRomooc(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int vehicleID = d.vehicleID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTower_ChangeRomooc(masterID, vehicleID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTower_TractorRead(dynamic d)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = d.request.ToString();
                int? vendorID = d.vendorID;
                DateTime? dfrom = null;
                DateTime? dto = null;
                if (d.DateFrom != null)
                    dfrom = Convert.ToDateTime(d.DateFrom.ToString());
                if (d.DateTo != null)
                    dto = Convert.ToDateTime(d.DateTo.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_TractorRead(request, vendorID, dfrom, dto);
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public DTOResult MONControlTower_RomoocRead(dynamic d)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = d.request.ToString();
                int? vendorID = d.vendorID;
                DateTime? dfrom = null;
                DateTime? dto = null;
                if (d.DateFrom != null)
                    dfrom = Convert.ToDateTime(d.DateFrom.ToString());
                if (d.DateTo != null)
                    dto = Convert.ToDateTime(d.DateTo.ToString());
                int vehicleid = d.vehicleid;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_RomoocRead(request, vendorID, dfrom, dto, vehicleid);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMON_MainFilter MONControlTowerCO_MainFilter(dynamic d)
        {
            try
            {
                DTOMON_MainFilter result = new DTOMON_MainFilter();
                DTOMONControlTower_ObjectFilter item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONControlTower_ObjectFilter>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_MainFilter(item);
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public void MONControlTowerCO_RepairContainer(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                int locationID = d.locationID;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    foreach (int ordContainerID in lst)
                        sv.MONControlTowerCO_RepairContainer(masterID, ordContainerID, locationID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void MONControlTowerCO_CutContainer(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int locationID = d.locationID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                string containerNo = d.containerNo;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    foreach (int ordContainerID in lst)
                        sv.MONControlTowerCO_CutContainer(masterID, ordContainerID, locationID, containerNo, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int MONCO_TOContainer_StartOffer(dynamic d)
        {
            try
            {
                int result = 0;
                int opsTOContainer = d.opsTOContainer;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONCO_TOContainer_StartOffer(opsTOContainer);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONCO_TOContainer_StartOfferData(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                DTOHelperTOMaster_Start item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOHelperTOMaster_Start>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONCO_TOContainer_StartOfferData(opsTOContainer, item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONCO_TOContainer_Start(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                bool isChangeRomooc = d.isChangeRomooc;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONCO_TOContainer_Start(opsTOContainer, isChangeRomooc);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ContainerStop(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                int locationID = d.locationID;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ContainerStop(opsTOContainer, locationID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ChangeDepotGet(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                int locationID = d.locationID;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ChangeDepotGet(opsTOContainer, locationID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_AddDepotGet(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                int locationID = d.locationID;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_AddDepotGet(opsTOContainer, locationID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ChangeDepotReturn(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                int locationID = d.locationID;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ChangeDepotReturn(opsTOContainer, locationID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_AddDepotReturn(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                int locationID = d.locationID;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_AddDepotReturn(opsTOContainer, locationID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_AddStock(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                int locationID = d.locationID;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_AddStock(opsTOContainer, locationID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ContainerRepair(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                int locationID = d.locationID;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ContainerRepair(opsTOContainer, locationID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_COTOKMUpdate(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_COTOKMUpdate(masterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public OPSCOTOMaster MONControlTowerCO_Check_VehicleMaster(dynamic d)
        {
            try
            {
                int id = d.id;
                int? romoocID = d.romoocID;
                var rs = new OPSCOTOMaster();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    rs = sv.MONControlTowerCO_Check_VehicleMaster(id, romoocID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ContainerCorrupt(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                int locationID = d.locationID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ContainerCorrupt(opsTOContainer, locationID, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ContainerCutRomooc(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ContainerCutRomooc(opsTOContainer, reasonID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ContainerStatusChange(dynamic d)
        {
            try
            {
                int opsTOContainer = d.opsTOContainer;
                int reasonID = d.reasonID;
                string reasonNote = d.reasonNote;
                string containerNo = d.containerNo;
                string sealNo1 = d.sealNo1;
                string sealNo2 = d.sealNo2;
                bool isCutRomooc = d.isCutRomooc;
                int? vehicleID = d.vehicleID;
                bool isPause = d.isPause;
                double stopHour = d.stopHour;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ContainerStatusChange(opsTOContainer, isCutRomooc, isPause, stopHour, reasonID, reasonNote, vehicleID, containerNo, sealNo1, sealNo2);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ContainerCompleteCOList(dynamic d)
        {
            try
            {
                int id = d.id;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ContainerCompleteCOList(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_UnComplete(dynamic d)
        {
            try
            {
                int id = d.id;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_UnComplete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int MONControlTowerCO_Continuous(dynamic d)
        {
            try
            {
                int id = d.id;
                int rs = 0;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    rs = sv.MONControlTowerCO_Continuous(id);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_EndStation(dynamic d)
        {
            try
            {
                int id = d.id;
                int? locationromoocid = d.locationRomoocID;
                int? locationvehicleid = d.locationVehicleID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_EndStation(id, locationromoocid, locationvehicleid);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOCombobox> MONControlTower_ReasonChange(dynamic d)
        {
            try
            {
                List<DTOCombobox> result = new List<DTOCombobox>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTower_ReasonChange();
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public List<DTOMON_VehicleIconData> MONControlTowerCO_AllVehicle(dynamic d)
        {
            try
            {
                List<DTOMON_VehicleIconData> result = new List<DTOMON_VehicleIconData>();
                bool showTractor = d.showTractor;
                bool showRomooc = d.showRomooc;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_AllVehicle(showTractor, showRomooc);
                    foreach (var obj in result)
                    {
                        if (obj.ID > 0 && obj.GPSCode.HasValue())
                        {
                            ServiceFactory.SVOther((ISVOther svoth) =>
                            {
                                var gps = svoth.VehiclePosition_GetLast(obj.GPSCode, DateTime.Now);
                                if (gps != null)
                                {
                                    obj.Lat = gps.Lat;
                                    obj.Lng = gps.Lng;
                                }
                            });
                        }
                    }
                });
                return result;
            }
            catch (Exception ex)
            {


                throw ex;
            }
        }

        public DTOMON_VehicleData MONControlTowerCO_GetVehicleStatus(dynamic d)
        {
            try
            {
                DTOMON_VehicleData result = new DTOMON_VehicleData();
                string vehicleNo = d.vehicleNo;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_GetVehicleStatus(vehicleNo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_DepotList(dynamic d)
        {
            try
            {
                DTOResult result = new DTOResult();
                string request = d.request.ToString();
                int opscontainerID = d.opscontainerID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_DepotList(request, opscontainerID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_ChangeDepot(dynamic d)
        {
            try
            {
                int opscontainerID = d.opscontainerID;
                int cuslocationID = d.cuslocationID;
                int reasionID = d.reasionID;
                int masterID = d.masterID;
                string reasonNote = d.reasonNote;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_ChangeDepot(masterID, opscontainerID, cuslocationID, reasionID, reasonNote);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public DTOResult DIMonitorTrouble_NotinList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                int? locationID = d.locationID != null ? (int?)d.locationID : null;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitorTrouble_NotinList(masterID, locationID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorTrouble_SaveList(dynamic d)
        {
            try
            {
                int masterID = d.masterID;
                List<DTOCATSYSCustomerTrouble> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATSYSCustomerTrouble>>(d.lst.ToString());
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorTrouble_SaveList(lst, masterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOCombobox> DIMonitor_ListTypeOfDriver(dynamic d)
        {
            try
            {
                var result = new List<DTOCombobox>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.DIMonitor_ListTypeOfDriver();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DIMonitorMaster_UpdateCashCollection(dynamic d)
        {
            try
            {
                int ordGroupID = d.ordGroupID;
                bool value = d.value;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.DIMonitorMaster_UpdateCashCollection(ordGroupID, value);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_COTONonMasterList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int packingID = d.packingID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_COTONonMasterList(request, packingID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_UnCompleteMasterList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int packingID = d.packingID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_UnCompleteMasterList(request, packingID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_OrderLocal(dynamic d)
        {
            try
            {
                int opsContainerID = d.opsContainerID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_OrderLocal(opsContainerID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_OrderLocalByMaster(dynamic d)
        {
            try
            {
                int opsContainerID = d.opsContainerID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_OrderLocalByMaster(opsContainerID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DTOResult MONControlTowerCO_SwapCOTONonMasterList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int packingID = d.packingID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_SwapCOTONonMasterList(request, packingID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONControlTowerCO_SwapUnCompleteMasterList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                int packingID = d.packingID;
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONControlTowerCO_SwapUnCompleteMasterList(request, packingID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_OrderExport(dynamic d)
        {
            try
            {
                int opsContainerID = d.opsContainerID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_OrderExport(opsContainerID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONControlTowerCO_OrderExportByMaster(dynamic d)
        {
            try
            {
                int opsContainerID = d.opsContainerID;
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONControlTowerCO_OrderExportByMaster(opsContainerID, lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region input container
        [HttpPost]
        public DTOResult MONCO_TenderList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONCO_TenderList(request, dtFrom, dtTo, listCustomerID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONCO_TenderSave(dynamic dynParam)
        {
            try
            {
                DTOPODCOInput item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODCOInput>(dynParam.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONCO_TenderSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONCO_TenderUpdateStatus(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int type = dynParam.type;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONCO_TenderUpdateStatus(lst, type);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Container Operation

        [HttpPost]
        public DTOResult MONCO_COTORead(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONCO_COTORead(request);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult MONCO_COTOByMaster(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int id = dynParam.id;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONCO_COTOByMaster(request, id);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONCO_COTOUpdateList(dynamic dynParam)
        {
            try
            {
                List<DTOMONCOTO> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONCOTO>>(dynParam.lst.ToString());
                int type = dynParam.type;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONCO_COTOUpdateList(lst, type);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONCO_COTOUpdateCOTOContainer(dynamic dynParam)
        {
            try
            {
                DTOMONOPSTO item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONOPSTO>(dynParam.item.ToString());
                int masterID = dynParam.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONCO_COTOUpdateCOTOContainer(masterID, item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONCO_COTOofMasterAdd(dynamic dynParam)
        {
            try
            {
                DTOMONCOTO item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONCOTO>(dynParam.item.ToString());
                int masterID = dynParam.masterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONCO_COTOofMasterAdd(item, masterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONCO_COTOofMasterRomove(dynamic dynParam)
        {
            try
            {
                int id = dynParam.id;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONCO_COTOofMasterRomove(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Dock

        [HttpPost]
        public DTOResult MONDock_VehicleList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                int locationID = dynParam.locationID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONDock_VehicleList(request, locationID, fDate, tDate);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMONDockTimeline> MONDock_RegisterTimeline(dynamic dynParam)
        {
            try
            {
                List<int?> lstVehicleID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int?>>(dynParam.lst.ToString());
                int vendorID = dynParam.vendorID;
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                var result = default(List<DTOMONDockTimeline>);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONDock_RegisterTimeline(lstVehicleID, vendorID, fDate, tDate);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public DTOMONOPSTODock MONDock_GetInfo(dynamic dynParam)
        {
            try
            {
                int id = dynParam.id;
                var result = default(DTOMONOPSTODock);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONDock_GetInfo(id);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONDock_DockTimelineAccept(dynamic d)
        {
            try
            {
                int id = d.id;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONDock_DockTimelineAccept(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONDock_DockUpdateTime(dynamic d)
        {
            try
            {
                DTOMONOPSTODock item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONOPSTODock>(d.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONDock_DockUpdateTime(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region view 2

        [HttpPost]
        public DTOResult MONDock_Manage_VehicleList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                int locationID = dynParam.locationID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONDock_VehicleList(request, locationID, fDate, tDate);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public List<DTOMONDockTimeline> MONDock_Manage_Timeline(dynamic dynParam)
        {
            try
            {
                List<int?> lstVehicleID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int?>>(dynParam.lst.ToString());
                int vendorID = dynParam.vendorID;
                DateTime fDate = Convert.ToDateTime(dynParam.fDate);
                DateTime tDate = Convert.ToDateTime(dynParam.tDate);
                var result = default(List<DTOMONDockTimeline>);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONDock_RegisterTimeline(lstVehicleID, vendorID, fDate, tDate);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #endregion

        #endregion

        #region Approve Cost

        public DTOResult MONCO_TroubleList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                DateTime pFrom = Convert.ToDateTime(d.DateFrom.ToString());
                DateTime pTo = Convert.ToDateTime(d.DateTo.ToString());
                pTo = pTo.AddDays(1);
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONCO_TroubleList(request, pFrom, pTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONDI_TroubleList(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                DateTime pFrom = Convert.ToDateTime(d.DateFrom.ToString());
                DateTime pTo = Convert.ToDateTime(d.DateTo.ToString());
                pTo = pTo.AddDays(1);
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONDI_TroubleList(request, pFrom, pTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MON_ApprovedTrouble(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MON_ApprovedTrouble(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MON_RevertApprovedTrouble(dynamic d)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MON_RevertApprovedTrouble(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public DTOResult MONDI_TroubleRead(dynamic d)
        {
            try
            {
                string request = d.request.ToString();
                DateTime pFrom = Convert.ToDateTime(d.DateFrom.ToString());
                DateTime pTo = Convert.ToDateTime(d.DateTo.ToString());
                pTo = pTo.AddDays(1);
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONDI_TroubleRead(request, pFrom, pTo);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONDI_TroubleApproved(dynamic d)
        {
            try
            {
                List<DTOCATTrouble> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATTrouble>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONDI_TroubleApproved(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONDI_TroubleReject(dynamic d)
        {
            try
            {
                List<DTOCATTrouble> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATTrouble>>(d.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONDI_TroubleReject(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Import Data
        const int digitsOfRound = 6;

        [HttpPost]
        public DTOResult MONImport_Index_Setting_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONImport_Index_Setting_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string MONImport_Index_Setting_Download(dynamic dynParam)
        {
            try
            {
                string file = "";
                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                DTOCUSSettingMON objSetting = new DTOCUSSettingMON();
                List<DTOMONImport> data = new List<DTOMONImport>();

                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    data = sv.MONImport_Data(dtfrom, dtto, customerID);
                    objSetting = sv.MONImport_Index_Setting_Get(templateID);
                });

                string[] aValue = { "CustomerID", "SYSCustomerID", "SettingID", "CreateBy", "CreateDate", "Name", "RowStart",
                                          "SettingCustomerCode", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "DITOGroupProductStatusPODName", "DITOGroupProductStatusPODID", "VehicleID", "IsNew" };
                List<string> sValue = new List<string>(aValue);

                Dictionary<string, string> dicName = GetDataName();

                if (objSetting != null)
                {
                    file = "/Uploads/temp/" + objSetting.Name.Replace(' ', '-') + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";
                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                    FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                    using (ExcelPackage package = new ExcelPackage(exportfile))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(objSetting.Name);
                        if (objSetting.RowStart > 1)
                        {
                            int row = 1;
                            foreach (var prop in objSetting.GetType().GetProperties())
                            {
                                try
                                {
                                    var p = prop.Name;
                                    if (!sValue.Contains(p))
                                    {
                                        var v = (int)prop.GetValue(objSetting, null);
                                        if (v > 0)
                                        {
                                            if (dicName.ContainsKey(p))
                                                worksheet.Cells[row, v].Value = dicName[p];
                                            else
                                                worksheet.Cells[row, v].Value = p;

                                            worksheet.Column(v).Width = 20;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            row = 2;
                            foreach (var item in data)
                            {
                                foreach (var prop in objSetting.GetType().GetProperties())
                                {
                                    try
                                    {
                                        var p = prop.Name;
                                        if (!sValue.Contains(p))
                                        {
                                            var v = (int)prop.GetValue(objSetting, null);
                                            if (v > 0)
                                            {
                                                foreach (var propItem in item.GetType().GetProperties())
                                                {
                                                    var pItem = propItem.Name;
                                                    if (p == pItem)
                                                    {
                                                        var valueItem = propItem.GetValue(item, null);
                                                        Type t = valueItem.GetType();

                                                        if (t.Equals(typeof(bool)))
                                                        {
                                                            bool value = (bool)valueItem;
                                                            if (value == true)
                                                            {
                                                                worksheet.Cells[row, v].Value = "x";
                                                            }
                                                        }
                                                        else if (t.Equals(typeof(DateTime)))
                                                        {
                                                            worksheet.Cells[row, v].Value = valueItem;
                                                            if (pItem == "DateFromLoadStart" || pItem == "DateFromLoadEnd" || pItem == "DateToLoadStart" || pItem == "DateToLoadEnd")
                                                            {
                                                                ExcelHelper.CreateFormat(worksheet, row, v, ExcelHelper.FormatHHMM);
                                                            }
                                                            else
                                                            {
                                                                ExcelHelper.CreateFormat(worksheet, row, v, ExcelHelper.FormatDDMMYYYY);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            worksheet.Cells[row, v].Value = valueItem;
                                                        }

                                                        break;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {
                                    }
                                }
                                row++;
                            }
                        }

                        package.Save();
                    }
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONImport_Excel_Import(dynamic dynParam)
        {
            try
            {
                int templateID = (int)dynParam.TemplateID;

                List<DTOMONImport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONImport>>(dynParam.Data.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONImport_Excel_Import(templateID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMONImport> MONImport_Excel_Check(dynamic dynParam)
        {
            try
            {
                string file = "/" + dynParam.file.ToString();

                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                DTOCUSSettingMON objSetting = new DTOCUSSettingMON();
                List<DTOMONImport> data = new List<DTOMONImport>();

                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    data = sv.MONImport_Data(dtfrom, dtto, customerID);
                    objSetting = sv.MONImport_Index_Setting_Get(templateID);
                });

                List<CATVehicle> lstVehicle = new List<CATVehicle>();
                ServiceFactory.SVFleetManage((IServices.ISVFleetManage sv) =>
                {
                    lstVehicle = sv.CATVehicle_AllList().Data.Cast<CATVehicle>().ToList();
                });

                List<DTOCustomer> lstVendor = new List<DTOCustomer>();
                ServiceFactory.SVReport((IServices.ISVReport sv) =>
                {
                    lstVendor = sv.Vendor_List().Data.Cast<DTOCustomer>().ToList();
                });

                var dataRes = new List<DTOMONImport>();


                if (objSetting != null)
                {
                    //Check các required.

                    string[] aValue = { "CustomerID", "SYSCustomerID", "SettingID", "CreateBy", "CreateDate", "Name", "RowStart",
                                          "SettingCustomerCode", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "VehicleID", "IsNew", "VENLoadCodeID", "VENUnLoadCodeID" };

                    var sValue = new List<string>(aValue);

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int row = 0;
                                for (row = objSetting.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var obj = new DTOMONImport();
                                    var objOrigin = new DTOMONImport();
                                    var lstError = new List<string>();

                                    var excelInput = GetDataValue(worksheet, objSetting, row, sValue);
                                    var ID = excelInput["ID"];
                                    var RegNo = excelInput["RegNo"];
                                    if (string.IsNullOrEmpty(ID))
                                    {
                                        throw new Exception("Số thứ tự không được trống.");
                                    }
                                    else
                                    {
                                        try
                                        {
                                            var id = Convert.ToInt32(ID);

                                            obj.ID = -1;

                                            objOrigin = data.FirstOrDefault(c => c.ID == id);
                                            if (objOrigin == null)
                                            {
                                                lstError.Add("Số thứ tự [" + ID + "] không được thiết lập");
                                            }
                                            else
                                            {
                                                obj.ID = id;
                                                obj.RequestDate = objOrigin.RequestDate;
                                                var checkInFile = dataRes.FirstOrDefault(c => c.ID == id);

                                                if (checkInFile != null)
                                                {
                                                    obj.IsNew = true;
                                                }
                                                else
                                                {
                                                    obj.IsNew = false;
                                                }
                                                //obj = objOrigin;
                                            }

                                        }
                                        catch
                                        {
                                            lstError.Add("Số thứ tự [" + ID + "] không chính xác");
                                        }
                                    }

                                    if (string.IsNullOrEmpty(RegNo))
                                    {
                                        obj.VehicleID = -1;
                                    }
                                    else
                                    {
                                        if (obj.ID > 0)
                                        {
                                            var checkRegNo = lstVehicle.FirstOrDefault(c => c.RegNo.ToLower() == RegNo.ToLower());
                                            if (checkRegNo == null)// xe moi
                                            {
                                                lstError.Add("Xe [" + RegNo + "] không tồn tại");
                                            }
                                            else//da ton tai so xe
                                            {
                                                if (checkRegNo.RegNo != objOrigin.RegNo)
                                                {
                                                    lstError.Add("Xe [" + RegNo + "] không khớp gốc");
                                                }
                                                else
                                                {
                                                    obj.VehicleID = checkRegNo.ID;
                                                    obj.RegNo = checkRegNo.RegNo;
                                                }
                                            }
                                        }
                                    }

                                    obj.DNCode = excelInput["DNCode"];
                                    obj.SOCode = excelInput["SOCode"];

                                    var IsInvoiceStr = excelInput["IsInvoice"];
                                    if (IsInvoiceStr.ToLower().Trim() == "x")
                                    {
                                        obj.IsInvoice = true;
                                    }
                                    else
                                    {
                                        obj.IsInvoice = false;
                                    }

                                    var DateFromCome = excelInput["DateFromCome"];
                                    if (!string.IsNullOrEmpty(DateFromCome))
                                    {
                                        try
                                        {
                                            obj.DateFromCome = ExcelHelper.ValueToDate(DateFromCome);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.DateFromCome = Convert.ToDateTime(DateFromCome, new CultureInfo("vi-VN"));
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày đến kho [" + DateFromCome + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.DateFromCome = null;

                                    var DateFromLoadStart = excelInput["DateFromLoadStart"];
                                    if (!string.IsNullOrEmpty(DateFromLoadStart) && obj.DateFromCome != null)
                                    {
                                        DateTime temp = new DateTime();
                                        TimeSpan time;
                                        if (!TimeSpan.TryParse("07:35", out time))
                                        {
                                            lstError.Add("Thời gian vào máng [" + DateFromLoadStart + "] không chính xác");
                                        }
                                        else
                                        {
                                            temp = obj.DateFromCome.Value.Date + time;
                                            obj.DateFromLoadStart = temp;
                                        }
                                    }
                                    else obj.DateFromLoadStart = null;

                                    var DateFromLeave = excelInput["DateFromLeave"];

                                    if (!string.IsNullOrEmpty(DateFromLeave))
                                    {
                                        try
                                        {
                                            obj.DateFromLeave = ExcelHelper.ValueToDate(DateFromLeave);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.DateFromLeave = Convert.ToDateTime(DateFromLeave, new CultureInfo("vi-VN"));
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày rời kho [" + DateFromLeave + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.DateFromLeave = null;

                                    var DateFromLoadEnd = excelInput["DateFromLoadEnd"];
                                    if (!string.IsNullOrEmpty(DateFromLoadEnd) && obj.DateFromLeave != null)
                                    {
                                        DateTime temp = new DateTime();
                                        TimeSpan time;
                                        if (!TimeSpan.TryParse("07:35", out time))
                                        {
                                            lstError.Add("Thời gian ra máng [" + DateFromLoadEnd + "] không chính xác");
                                        }
                                        else
                                        {
                                            temp = obj.DateFromLeave.Value.Date + time;
                                            obj.DateFromLoadEnd = temp;
                                        }
                                    }
                                    else obj.DateFromLoadEnd = null;

                                    var DateToCome = excelInput["DateToCome"];
                                    if (!string.IsNullOrEmpty(DateToCome))
                                    {
                                        try
                                        {
                                            obj.DateToCome = ExcelHelper.ValueToDate(DateToCome);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.DateToCome = Convert.ToDateTime(DateToCome, new CultureInfo("vi-VN"));
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày đến NPP [" + DateToCome + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.DateToCome = null;

                                    var DateToLoadStart = excelInput["DateToLoadStart"];
                                    if (!string.IsNullOrEmpty(DateToLoadStart) && obj.DateToCome != null)
                                    {
                                        DateTime temp = new DateTime();
                                        TimeSpan time;
                                        if (!TimeSpan.TryParse("07:35", out time))
                                        {
                                            lstError.Add("Thời gian ra máng [" + DateToLoadStart + "] không chính xác");
                                        }
                                        else
                                        {
                                            temp = obj.DateToCome.Value.Date + time;
                                            obj.DateToLoadStart = temp;
                                        }
                                    }
                                    else obj.DateToLoadStart = null;

                                    var DateToLeave = excelInput["DateToLeave"];
                                    if (!string.IsNullOrEmpty(DateToLeave))
                                    {
                                        try
                                        {
                                            obj.DateToLeave = ExcelHelper.ValueToDate(DateToLeave);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.DateToLeave = Convert.ToDateTime(DateToLeave, new CultureInfo("vi-VN"));
                                            }
                                            catch
                                            {
                                                lstError.Add("Ngày rời NPP [" + DateToLeave + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.DateToLeave = null;

                                    var DateToLoadEnd = excelInput["DateToLoadEnd"];
                                    if (!string.IsNullOrEmpty(DateToLoadEnd) && obj.DateToLeave != null)
                                    {
                                        DateTime temp = new DateTime();
                                        TimeSpan time;
                                        if (!TimeSpan.TryParse("07:35", out time))
                                        {
                                            lstError.Add("Thời gian ra máng [" + DateToLoadEnd + "] không chính xác");
                                        }
                                        else
                                        {
                                            temp = obj.DateToLeave.Value.Date + time;
                                            obj.DateToLoadEnd = temp;
                                        }
                                    }
                                    else obj.DateToLoadEnd = null;

                                    //obj.InvoiceBy = excelInput["InvoiceBy"];

                                    var InvoiceDate = excelInput["InvoiceDate"];
                                    if (!string.IsNullOrEmpty(InvoiceDate))
                                    {
                                        try
                                        {
                                            obj.InvoiceDate = ExcelHelper.ValueToDate(InvoiceDate);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                obj.InvoiceDate = Convert.ToDateTime(InvoiceDate, new CultureInfo("vi-VN"));
                                            }
                                            catch
                                            {
                                                lstError.Add("InvoiceDate [" + InvoiceDate + "] không chính xác");
                                            }

                                        }
                                    }
                                    else obj.InvoiceDate = null;

                                    obj.InvoiceNote = excelInput["InvoiceNote"];
                                    obj.Note = excelInput["Note"];
                                    obj.Note1 = excelInput["Note1"];
                                    obj.Note2 = excelInput["Note2"];
                                    obj.ChipNo = excelInput["ChipNo"];
                                    obj.Temperature = excelInput["Temperature"];

                                    var Ton = excelInput["Ton"];
                                    var CBM = excelInput["CBM"];
                                    var Quantity = excelInput["Quantity"];

                                    if (string.IsNullOrEmpty(Ton) && string.IsNullOrEmpty(CBM) && string.IsNullOrEmpty(Quantity))
                                    {
                                        lstError.Add("Tấn, Khối, Số lượng phải có ít nhất 1 cột không được trống");
                                    }

                                    if (!string.IsNullOrEmpty(Ton))
                                    {
                                        try
                                        {
                                            obj.Ton = Convert.ToDouble(Ton);
                                        }
                                        catch
                                        {
                                            lstError.Add("Tấn [" + Ton + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.Ton > 0)
                                            obj.Ton = 0;
                                    }


                                    if (!string.IsNullOrEmpty(CBM))
                                    {
                                        try
                                        {
                                            obj.CBM = Convert.ToDouble(CBM);
                                        }
                                        catch
                                        {
                                            lstError.Add("Khối [" + CBM + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.CBM > 0)
                                            obj.CBM = 0;
                                    }

                                    if (!string.IsNullOrEmpty(Quantity))
                                    {
                                        try
                                        {
                                            obj.Quantity = Convert.ToDouble(Quantity);
                                        }
                                        catch
                                        {
                                            lstError.Add("Số lượng [" + Quantity + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.Quantity > 0)
                                            obj.Quantity = 0;
                                    }

                                    var TonTranfer = excelInput["TonTranfer"];
                                    var CBMTranfer = excelInput["CBMTranfer"];
                                    var QuantityTranfer = excelInput["QuantityTranfer"];

                                    if (string.IsNullOrEmpty(TonTranfer) && string.IsNullOrEmpty(CBMTranfer) && string.IsNullOrEmpty(QuantityTranfer))
                                    {
                                        lstError.Add("Tấn lấy, Khối lấy, Số lượng lấy phải có ít nhất 1 cột không được trống");
                                    }

                                    if (!string.IsNullOrEmpty(TonTranfer))
                                    {
                                        try
                                        {
                                            obj.TonTranfer = Convert.ToDouble(TonTranfer);
                                        }
                                        catch
                                        {
                                            lstError.Add("Tấn lấy [" + TonTranfer + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.TonTranfer > 0)
                                            obj.TonTranfer = 0;
                                    }


                                    if (!string.IsNullOrEmpty(CBMTranfer))
                                    {
                                        try
                                        {
                                            obj.CBMTranfer = Convert.ToDouble(CBMTranfer);
                                        }
                                        catch
                                        {
                                            lstError.Add("Khối lấy [" + CBMTranfer + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.CBMTranfer > 0)
                                            obj.CBMTranfer = 0;
                                    }

                                    if (!string.IsNullOrEmpty(QuantityTranfer))
                                    {
                                        try
                                        {
                                            obj.QuantityTranfer = Convert.ToDouble(QuantityTranfer);
                                        }
                                        catch
                                        {
                                            lstError.Add("Số lượng lấy [" + QuantityTranfer + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.QuantityTranfer > 0)
                                            obj.QuantityTranfer = 0;
                                    }

                                    if (obj.TonTranfer < 0 || obj.CBMTranfer < 0 || obj.QuantityTranfer < 0)
                                    {
                                        lstError.Add("Tấn lấy, Khối lấy, Số lượng lấy phải lớn hơn 0");
                                    }

                                    var TonBBGN = excelInput["TonBBGN"];
                                    var CBMBBGN = excelInput["CBMBBGN"];
                                    var QuantityBBGN = excelInput["QuantityBBGN"];

                                    if (!string.IsNullOrEmpty(TonBBGN))
                                    {
                                        try
                                        {
                                            obj.TonBBGN = Convert.ToDouble(TonBBGN);
                                        }
                                        catch
                                        {
                                            lstError.Add("Tấn giao [" + TonBBGN + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.TonBBGN > 0)
                                            obj.TonBBGN = 0;
                                    }
                                    if (obj.TonBBGN > obj.TonTranfer)
                                    {
                                        lstError.Add("Tấn giao phải bé hơn hoặc bằng Tấn lấy");
                                    }

                                    if (!string.IsNullOrEmpty(CBMBBGN))
                                    {
                                        try
                                        {
                                            obj.CBMBBGN = Convert.ToDouble(CBMBBGN);
                                        }
                                        catch
                                        {
                                            lstError.Add("Khối giao [" + CBMBBGN + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.CBMBBGN > 0)
                                            obj.CBMBBGN = 0;
                                    }
                                    if (obj.CBMBBGN > obj.CBMTranfer)
                                    {
                                        lstError.Add("Khối giao phải bé hơn hoặc bằng Khối lấy");
                                    }

                                    if (!string.IsNullOrEmpty(QuantityBBGN))
                                    {
                                        try
                                        {
                                            obj.QuantityBBGN = Convert.ToDouble(QuantityBBGN);
                                        }
                                        catch
                                        {
                                            lstError.Add("Số lượng giao [" + QuantityBBGN + "] không chính xác");
                                        }
                                    }
                                    else
                                    {
                                        if (objSetting.QuantityBBGN > 0)
                                            obj.QuantityBBGN = 0;
                                    }
                                    if (obj.QuantityBBGN > obj.QuantityTranfer)
                                    {
                                        lstError.Add("Số lượng giao phải bé hơn hoặc bằng Số lượng lấy");
                                    }

                                    var VENCodeLoad = excelInput["VENLoadCode"];
                                    if (!string.IsNullOrEmpty(VENCodeLoad))
                                    {
                                        var ven = lstVendor.FirstOrDefault(c => c.Code == VENCodeLoad);
                                        if (ven != null)
                                        {
                                            obj.VENLoadCode = VENCodeLoad;
                                            obj.VENLoadCodeID = ven.ID;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        obj.VENLoadCodeID = objOrigin.VENLoadCodeID;
                                    }

                                    var VENCodeUnLoad = excelInput["VENUnLoadCode"];
                                    if (!string.IsNullOrEmpty(VENCodeUnLoad))
                                    {
                                        var ven = lstVendor.FirstOrDefault(c => c.Code == VENCodeUnLoad);
                                        if (ven != null)
                                        {
                                            obj.VENUnLoadCode = VENCodeUnLoad;
                                            obj.VENUnLoadCodeID = ven.ID;
                                        }
                                        else
                                        {

                                        }
                                    }
                                    else
                                    {
                                        obj.VENUnLoadCodeID = objOrigin.VENUnLoadCodeID;
                                    }

                                    lstError.Distinct();
                                    obj.ExcelError = string.Join(", ", lstError);
                                    if (!string.IsNullOrEmpty(obj.ExcelError))
                                        obj.ExcelSuccess = false;
                                    else
                                    {
                                        obj.ExcelSuccess = true;
                                    }
                                    dataRes.Add(obj);
                                }
                            }
                        }
                    }
                }

                foreach (var item in dataRes)
                {
                    if (item.ExcelSuccess && !item.IsNew)
                    {
                        var parent = data.FirstOrDefault(c => c.ID == item.ID);
                        var lstChild = dataRes.Where(c => c.ID == item.ID);
                        if (lstChild != null && lstChild.Count() > 1)
                        {
                            if (parent.Ton > 0)
                            {
                                if (lstChild.Count(c => c.Ton == 0) > 0)
                                {
                                    foreach (var itemRes in dataRes)
                                    {
                                        if (itemRes.ID == item.ID)
                                        {
                                            itemRes.ExcelError += !string.IsNullOrEmpty(itemRes.ExcelError) ? ", " : "";
                                            itemRes.ExcelError += "Tồn tại số tấn bằng 0";
                                            itemRes.ExcelSuccess = false;
                                        }
                                    }
                                }
                                else
                                {
                                    var countTon = lstChild.Sum(c => c.Ton);
                                    if (parent.Ton < countTon)
                                    {
                                        foreach (var itemRes in dataRes)
                                        {
                                            if (itemRes.ID == item.ID)
                                            {
                                                itemRes.ExcelError += !string.IsNullOrEmpty(itemRes.ExcelError) ? ", " : "";
                                                itemRes.ExcelError += "Tổng số tấn lớn hơn số tấn gốc";
                                                itemRes.ExcelSuccess = false;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (lstChild.Count(c => c.Ton > 0 && c.IsNew) > 0)
                                {
                                    foreach (var itemRes in dataRes)
                                    {
                                        if (itemRes.ID == item.ID)
                                        {
                                            itemRes.ExcelError += !string.IsNullOrEmpty(itemRes.ExcelError) ? ", " : "";
                                            itemRes.ExcelError += "Số tấn gốc bằng 0";
                                            itemRes.ExcelSuccess = false;
                                        }
                                    }
                                }
                            }

                            if (parent.CBM > 0)
                            {
                                if (lstChild.Count(c => c.CBM == 0) > 0)
                                {
                                    foreach (var itemRes in dataRes)
                                    {
                                        if (itemRes.ID == item.ID)
                                        {
                                            itemRes.ExcelError += !string.IsNullOrEmpty(itemRes.ExcelError) ? ", " : "";
                                            itemRes.ExcelError += "Tồn tại số khối bằng 0";
                                            itemRes.ExcelSuccess = false;
                                        }
                                    }
                                }
                                else
                                {
                                    var countCBM = lstChild.Sum(c => c.CBM);
                                    if (parent.CBM < countCBM)
                                    {
                                        foreach (var itemRes in dataRes)
                                        {
                                            if (itemRes.ID == item.ID)
                                            {
                                                itemRes.ExcelError += !string.IsNullOrEmpty(itemRes.ExcelError) ? ", " : "";
                                                itemRes.ExcelError += "Tổng số khối lớn hơn số khối gốc";
                                                itemRes.ExcelSuccess = false;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (lstChild.Count(c => c.CBM > 0 && c.IsNew) > 0)
                                {
                                    foreach (var itemRes in dataRes)
                                    {
                                        if (itemRes.ID == item.ID)
                                        {
                                            itemRes.ExcelError += !string.IsNullOrEmpty(itemRes.ExcelError) ? ", " : "";
                                            itemRes.ExcelError += "Số khối gốc bằng 0";
                                            itemRes.ExcelSuccess = false;
                                        }
                                    }
                                }
                            }

                            if (parent.Quantity > 0)
                            {
                                if (lstChild.Count(c => c.Quantity == 0) > 0)
                                {
                                    foreach (var itemRes in dataRes)
                                    {
                                        if (itemRes.ID == item.ID)
                                        {
                                            itemRes.ExcelError += !string.IsNullOrEmpty(itemRes.ExcelError) ? ", " : "";
                                            itemRes.ExcelError += "Tồn tại số lượng bằng 0";
                                            itemRes.ExcelSuccess = false;
                                        }
                                    }
                                }
                                else
                                {
                                    var countQuantity = lstChild.Sum(c => c.Quantity);
                                    if (parent.Quantity < countQuantity)
                                    {
                                        foreach (var itemRes in dataRes)
                                        {
                                            if (itemRes.ID == item.ID)
                                            {
                                                itemRes.ExcelError += !string.IsNullOrEmpty(itemRes.ExcelError) ? ", " : "";
                                                itemRes.ExcelError += "Tổng số lượng hơn số lượng gốc";
                                                itemRes.ExcelSuccess = false;
                                            }
                                        }
                                    }
                                }
                            }
                            else
                            {
                                if (lstChild.Count(c => c.Quantity > 0 && c.IsNew) > 0)
                                {
                                    foreach (var itemRes in dataRes)
                                    {
                                        if (itemRes.ID == item.ID)
                                        {
                                            itemRes.ExcelError += !string.IsNullOrEmpty(itemRes.ExcelError) ? ", " : "";
                                            itemRes.ExcelError += "Số lượng gốc bằng 0";
                                            itemRes.ExcelSuccess = false;
                                        }
                                    }
                                }
                            }

                        }
                    }
                }
                return dataRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private Dictionary<string, string> GetDataName()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            result.Add("ID", "Số thứ tự");
            result.Add("DNCode", "Số DN");
            result.Add("SOCode", "Số SO");
            result.Add("OrderCode", "Mã đơn hàng");
            result.Add("ETARequest", "Ngày y/c giao hàng");
            result.Add("ETD", "ETD");
            result.Add("CustomerCode", "Mã Khách hàng");
            result.Add("CustomerName", "Tên khách hàng");
            result.Add("CreatedDate", "Ngày tạo");
            result.Add("MasterCode", "Mã chuyến");
            result.Add("DriverName", "Tài xế");
            result.Add("DriverTel", "SĐT Tài xế");
            result.Add("DriverCard", "CMND");
            result.Add("RegNo", "Xe");
            result.Add("RequestDate", "Ngày gửi y/c");
            result.Add("LocationFromCode", "Mã kho");
            result.Add("LocationToCode", "Mã NPP");
            result.Add("LocationToName", "NPP");
            result.Add("LocationToAddress", "Địa chỉ");
            result.Add("LocationToProvince", "Tỉnh");
            result.Add("LocationToDistrict", "Quận huyện");
            result.Add("IsInvoice", "Nhận chứng từ");
            result.Add("DateFromCome", "Ngày đến kho");
            result.Add("DateFromLeave", "Ngày rời kho");
            result.Add("DateFromLoadStart", "Thời gian vào máng");
            result.Add("DateFromLoadEnd", "Thời gian ra máng");
            result.Add("DateToCome", "Ngày đến NPP");
            result.Add("DateToLeave", "Ngày rời NPP");
            result.Add("DateToLoadStart", "Thời gian b.đầu dỡ hàng");
            result.Add("DateToLoadEnd", "Thời gian k.thúc dỡ hàng");
            result.Add("InvoiceBy", "Người tạo chứng từ");
            result.Add("InvoiceDate", "Ngày tạo c/t");
            result.Add("InvoiceNote", "Ghi chú c/t");
            result.Add("Note", "Ghi chú");
            result.Add("Note1", "Ghi chú 1");
            result.Add("Note2", "Ghi chú 2");
            result.Add("VendorName", "Nhà vận tải");
            result.Add("VendorCode", "Mã nhà vận tải");
            result.Add("Description", "Description");
            result.Add("GroupOfProductCode", "Mã nhóm sản phẩm");
            result.Add("GroupOfProductName", "Nhóm sản phẩm");
            result.Add("ChipNo", "ChipNo");
            result.Add("Temperature", "Temperature");
            result.Add("Ton", "Ton");
            result.Add("CBM", "CBM");
            result.Add("Quantity", "Số lượng");
            result.Add("TonTranfer", "Tấn lấy");
            result.Add("CBMTranfer", "Khối lấy");
            result.Add("QuantityTranfer", "Số lượng lấy");
            result.Add("TonBBGN", "Tấn giao");
            result.Add("CBMBBGN", "Khối giao");
            result.Add("QuantityBBGN", "Số lượng giao");
            result.Add("VENLoadCode", "Vendor bốc xếp lên");
            result.Add("VENUnLoadCode", "Vendor bốc xếp xuống");

            return result;
        }

        private Dictionary<string, string> GetDataValue(ExcelWorksheet ws, object obj, int row, List<string> sValue)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                try
                {
                    var p = prop.Name;
                    if (!sValue.Contains(p))
                    {
                        var v = (int)prop.GetValue(obj, null);
                        result.Add(p, v > 0 ? ExcelHelper.GetValue(ws, row, v) : string.Empty);
                    }
                }
                catch (Exception)
                {
                }
            }
            return result;
        }


        // Import hàng trả về
        [HttpPost]
        public DTOResult MONImportInput_Index_Setting_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONImportInput_Index_Setting_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public string MONImportInput_Index_Setting_Download(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                var result = string.Empty;
                var sID = (int)dynParam.sID;
                var objSetting = new DTOCUSSettingMONImport();
                var objSettingOrder = new DTOCUSSettingOrder();
                var dataExport = new List<DTOMONInputExport>();
                var dataOrder = new DTOORDOrder_ImportCheck();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    objSetting = sv.MONImportInput_Index_Setting_Get(sID);
                    dataExport = sv.MONImportInput_Excel_Export(sID, dtfrom, dtto);
                });

                if (objSetting != null && objSetting.CUSSettingOrderID > 0)
                {
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        objSettingOrder = sv.ORDOrder_Excel_Setting_Get(objSetting.CUSSettingOrderID);
                    });
                    if (objSettingOrder == null)
                        throw new Exception("Không tìm thấy thiết lập đơn hàng.");
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        dataOrder = sv.ORDOrder_Excel_Import_Data(objSettingOrder.CustomerID);
                    });

                    if (objSetting.FileID > 0)
                    {
                        string[] name = objSetting.FileName.Split('.').Reverse().Skip(1).Reverse().ToArray();
                        result = "/" + FolderUpload.Export + string.Join(".", name) + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                        result = result.Replace("+", "");
                        if (System.IO.File.Exists(HttpContext.Current.Server.MapPath("/" + objSetting.FilePath)))
                        {
                            System.IO.File.Copy(HttpContext.Current.Server.MapPath("/" + objSetting.FilePath), HttpContext.Current.Server.MapPath(result), true);
                        }
                        else
                        {
                            throw new Exception("Không tìm thấy file mẫu!");
                        }
                    }
                    else
                    {
                        throw new Exception("Chưa thiết lập file mẫu!");
                    }

                    FileInfo exportFile = new FileInfo(HttpContext.Current.Server.MapPath(result));
                    using (var package = new ExcelPackage(exportFile))
                    {
                        ExcelWorksheet ws = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (ws != null)
                        {
                            var sValue = new List<string>(new string[]{ "CustomerID", "SYSCustomerID", "ID", "CreateBy", "CreateDate", "HasStock", "ListStock", "Name", "ContractID", "RowStart",
                                                  "ServiceOfOrderName", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "ServiceOfOrderID" });

                            //Empty WS
                            var iRow = ws.Dimension.End.Row;
                            if (iRow > objSettingOrder.RowStart)
                            {
                                for (var row = iRow; row >= objSettingOrder.RowStart; row--)
                                {
                                    ws.DeleteRow(row);
                                }
                            }

                            var cRow = objSettingOrder.RowStart;
                            List<string> timeProps = new List<string>(new string[] { "RequestTime", "ETARequestTime", "TimeGetEmpty", "TimeReturnEmpty" });

                            Dictionary<string, int> dicPos = new Dictionary<string, int>();
                            #region GetPositionColumn
                            foreach (var prop in objSettingOrder.GetType().GetProperties())
                            {
                                try
                                {
                                    var p = prop.Name;
                                    if (!sValue.Contains(p))
                                    {
                                        var v = (int)prop.GetValue(objSettingOrder, null);
                                        if (v > 0)
                                        {
                                            dicPos.Add(p, v);
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            foreach (var prop in objSetting.GetType().GetProperties())
                            {
                                try
                                {
                                    var p = prop.Name;
                                    if (!sValue.Contains(p))
                                    {
                                        var v = (int)prop.GetValue(objSetting, null);
                                        if (v > 0)
                                        {
                                            dicPos.Add(p, v);
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            #endregion

                            #region Group theo kho
                            if (objSettingOrder.HasStock)
                            {
                                var dataGop = dataExport.GroupBy(c => new { c.DITOMasterID, c.MasterCode, c.OrderID, c.OrderCode, c.GroupProductID, c.GroupProductCode, c.Packing, c.DistributorCode, c.LocationToCode, c.ETD, c.ETA }).ToList();
                                foreach (var gop in dataGop)
                                {
                                    int max = 1;
                                    var item = gop.FirstOrDefault();
                                    foreach (var sto in objSettingOrder.ListStock)
                                    {
                                        var o = gop.Count(c => c.StockID == sto.StockID);
                                        if (o > max)
                                            max = o;
                                    }
                                    var dataContains = new List<int>();
                                    for (var i = 0; i < max; i++)
                                    {
                                        int type = 0;
                                        foreach (var pos in dicPos)
                                        {
                                            try
                                            {
                                                var property = pos.Key;
                                                var value = GetObjProperty(item, property, ref type);
                                                if (type == 1) //Loại datetime
                                                {
                                                    if (timeProps.Contains(property))
                                                    {
                                                        value = String.Format("{0:HH:mm}", value);
                                                    }
                                                    else
                                                    {
                                                        value = String.Format("{0:dd/MM/yyyy HH:mm}", value);
                                                    }
                                                }

                                                ws.Cells[cRow, pos.Value].Value = value;
                                            }
                                            catch (Exception)
                                            {

                                            }
                                        }

                                        foreach (var stock in objSettingOrder.ListStock)
                                        {
                                            var objGopInStock = gop.FirstOrDefault(c => c.StockID == stock.StockID && !dataContains.Contains(c.ID));
                                            if (objGopInStock != null)
                                            {
                                                dataContains.Add(objGopInStock.ID);
                                                foreach (var prop in stock.GetType().GetProperties())
                                                {
                                                    try
                                                    {
                                                        var p = prop.Name;
                                                        if (p != "StockID")
                                                        {
                                                            var v = (int)prop.GetValue(stock, null);
                                                            var val = objGopInStock.GetType().GetProperty(p).GetValue(objGopInStock, null);
                                                            if (val != null)
                                                            {
                                                                ws.Cells[cRow, v].Value = val.ToString();
                                                            }
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                    }
                                                }
                                            }
                                        }
                                        cRow++;
                                    }
                                }
                            }
                            #endregion

                            #region Group theo Kho-Hàng
                            else if (objSettingOrder.HasStockProduct)
                            {
                                var dataGop = dataExport.GroupBy(c => new { c.DITOMasterID, c.MasterCode, c.OrderID, c.OrderCode, c.DistributorCode, c.LocationToCode, c.ETD, c.ETA }).ToList();
                                foreach (var gop in dataGop)
                                {
                                    int max = 1;
                                    var item = gop.FirstOrDefault();
                                    foreach (var sto in objSettingOrder.ListStockWithProduct)
                                    {
                                        var o = gop.Count(c => c.StockID == sto.StockID && c.GroupProductID == sto.GroupOfProductID && c.PackingID == sto.ProductID);
                                        if (o > max)
                                            max = o;
                                    }
                                    var dataContains = new List<int>();
                                    for (var i = 0; i < max; i++)
                                    {
                                        int type = 0;
                                        foreach (var pos in dicPos)
                                        {
                                            try
                                            {
                                                var property = pos.Key;
                                                var value = GetObjProperty(item, property, ref type);
                                                if (type == 1) //Loại datetime
                                                {
                                                    if (timeProps.Contains(property))
                                                    {
                                                        value = String.Format("{0:HH:mm}", value);
                                                    }
                                                    else
                                                    {
                                                        value = String.Format("{0:dd/MM/yyyy HH:mm}", value);
                                                    }
                                                }

                                                ws.Cells[cRow, pos.Value].Value = value;
                                            }
                                            catch (Exception)
                                            {

                                            }
                                        }
                                        foreach (var stock in objSettingOrder.ListStockWithProduct)
                                        {
                                            var objGopInStock = gop.FirstOrDefault(c => c.StockID == stock.StockID && c.GroupProductID == stock.GroupOfProductID && c.PackingID == stock.ProductID && !dataContains.Contains(c.ID));
                                            if (objGopInStock != null)
                                            {
                                                dataContains.Add(objGopInStock.ID);
                                                foreach (var prop in stock.GetType().GetProperties())
                                                {
                                                    try
                                                    {
                                                        var p = prop.Name;
                                                        if (p != "StockID" && p != "GroupOfProductID" && p != "ProductID")
                                                        {
                                                            var v = (int)prop.GetValue(stock, null);
                                                            var val = objGopInStock.GetType().GetProperty(p).GetValue(objGopInStock, null);
                                                            if (val != null)
                                                            {
                                                                ws.Cells[cRow, v].Value = val.ToString();
                                                            }
                                                        }
                                                    }
                                                    catch (Exception)
                                                    {
                                                    }
                                                }
                                            }
                                        }
                                        cRow++;
                                    }
                                }
                            }
                            #endregion

                            #region Không group
                            else
                            {
                                foreach (var item in dataExport)
                                {
                                    item.Quantity_SKU = item.Quantity;
                                    item.Ton_SKU = item.Ton;
                                    item.CBM_SKU = item.CBM;
                                    int type = 0;
                                    foreach (var pos in dicPos)
                                    {
                                        try
                                        {
                                            var property = pos.Key;
                                            var value = GetObjProperty(item, property, ref type);
                                            if (type == 1) //Loại datetime
                                            {
                                                if (timeProps.Contains(property))
                                                {
                                                    value = String.Format("{0:HH:mm}", value);
                                                }
                                                else
                                                {
                                                    value = String.Format("{0:dd/MM/yyyy HH:mm}", value);
                                                }
                                            }

                                            ws.Cells[cRow, pos.Value].Value = value;
                                        }
                                        catch (Exception)
                                        {

                                        }
                                    }
                                    cRow++;
                                }
                            }
                            #endregion

                            package.Save();
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMONInputImport> MONImportInput_Index_Setting_Check(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var settingID = (int)dynParam.sID;
                string file = "/" + dynParam.file.ToString();
                var result = new List<DTOMONInputImport>();
                DTOCUSSettingMONImport objSetting = new DTOCUSSettingMONImport();
                DTOCUSSettingOrder objSettingOrder = new DTOCUSSettingOrder();
                DTOORDOrder_ImportCheck dataOrder = new DTOORDOrder_ImportCheck();
                var dataExport = new List<DTOMONInputExport>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    objSetting = sv.MONImportInput_Index_Setting_Get(settingID);
                    dataExport = sv.MONImportInput_Excel_Export(settingID, dtfrom, dtto);
                });

                if (objSetting != null)
                {
                    string[] aValue = { "CustomerID", "SYSCustomerID", "ID", "CreateBy", "CreateDate", "HasStock", "ListStock", "Name", "ContractID", "RowStart", "HasStockProduct",
                                        "StockID", "GroupOfProductID", "ProductID", "ListStockWithProduct", "ServiceOfOrderName", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "ServiceOfOrderID" };
                    var sValue = new List<string>(aValue);

                    int cusID = 0;
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        objSettingOrder = sv.ORDOrder_Excel_Setting_Get(objSetting.CUSSettingOrderID);
                    });
                    if (objSettingOrder == null)
                        throw new Exception("Không tìm thấy thiết lập đơn hàng.");
                    else
                        cusID = objSettingOrder.CustomerID;
                    ServiceFactory.SVOrder((ISVOrder sv) =>
                    {
                        dataOrder = sv.ORDOrder_Excel_Import_Data(cusID);
                    });

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int row = 0;
                                List<string> dataOrders = new List<string>();
                                for (row = objSettingOrder.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var excelInput = GetDataValue(worksheet, objSettingOrder, row, sValue);
                                    if (!string.IsNullOrEmpty(excelInput["OrderCode"]))
                                        dataOrders.Add(excelInput["OrderCode"].Trim().ToLower());
                                }
                                if (dataOrders.Count == 0)
                                    throw new Exception("Không có thông tin đơn hàng.");

                                Dictionary<int, int> dicTOMaster = new Dictionary<int, int>();
                                for (row = objSettingOrder.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var excelError = new List<string>();
                                    var excelInput = GetDataValue(worksheet, objSettingOrder, row, sValue);
                                    var excelPlanInput = GetDataValue(worksheet, objSetting, row, sValue);
                                    int masterID = 0;
                                    bool IsReturn = false;

                                    #region Kiểm tra chuyến
                                    if (objSetting.MasterCode > 0)
                                    {
                                        if (!string.IsNullOrEmpty(excelPlanInput["MasterCode"]))
                                        {
                                            var objCheck = dataExport.FirstOrDefault(c => c.MasterCode.ToLower() == excelPlanInput["MasterCode"].ToLower());
                                            if (objCheck != null)
                                                masterID = objCheck.DITOMasterID;
                                            else
                                                excelError.Add("Chuyến " + excelPlanInput["MasterCode"] + " không tồn tại");
                                        }
                                        else
                                            excelError.Add("Không có mã chuyến");
                                    }
                                    else
                                    {
                                        int vehicleID = 0;
                                        if (!string.IsNullOrEmpty(excelPlanInput["VehicleNo"]))
                                        {
                                            var objCheck = dataExport.FirstOrDefault(c => c.VehicleNo.ToLower() == excelPlanInput["VehicleNo"].ToLower());
                                            if (objCheck != null)
                                                vehicleID = objCheck.VehicleID;
                                            else
                                                excelError.Add("Xe " + excelPlanInput["VehicleNo"] + " không tồn tại");
                                        }
                                        else
                                            excelError.Add("Số xe không được trống");

                                        int masterSortOrder = 0;
                                        if (!string.IsNullOrEmpty(excelPlanInput["MasterSortOrder"]))
                                        {
                                            try
                                            {
                                                masterSortOrder = Convert.ToInt32(excelPlanInput["MasterSortOrder"]);
                                            }
                                            catch { }
                                        }
                                        else
                                            excelError.Add("Số chuyến không được trống");

                                        var objCheck1 = dataExport.FirstOrDefault(c => c.VehicleID == vehicleID && c.MasterSortOrder == masterSortOrder);
                                        if (objCheck1 != null)
                                            masterID = objCheck1.DITOMasterID;
                                        else
                                            excelError.Add("Chuyến không tồn tại");
                                    }

                                    if (masterID == 0)
                                        masterID = -row;

                                    var obj = result.FirstOrDefault(c => c.DITOMasterID == masterID);
                                    if (obj == null)
                                    {
                                        obj = new DTOMONInputImport();
                                        obj.Error = new List<string>();
                                        obj.Error.AddRange(excelError);
                                        obj.DITOMasterID = masterID;
                                        obj.ListGroup = new List<DTOMONInputImport_Group>();
                                        result.Add(obj);
                                    }
                                    #endregion

                                    #region Kiểm tra cột IsReturn
                                    if (objSetting.IsReturn > 0)
                                    {
                                        if (!string.IsNullOrEmpty(excelPlanInput["IsReturn"]))
                                        {
                                            if (excelPlanInput["IsReturn"].ToLower() == "x")
                                                IsReturn = true;
                                        }
                                    }
                                    #endregion Kiểm tra cột InvoiceReturnDate

                                    #region MyRegion
                                    var InvoiceReturnDate = excelPlanInput["InvoiceReturnDate"];
                                    DateTime? InvoiceRDate = null;
                                    if (objSetting.InvoiceReturnDate > 0 && !string.IsNullOrEmpty(InvoiceReturnDate))
                                    {
                                        try
                                        {
                                            InvoiceRDate = ExcelHelper.ValueToDate(InvoiceReturnDate);
                                        }
                                        catch
                                        {
                                            try
                                            {
                                                InvoiceRDate = Convert.ToDateTime(InvoiceReturnDate, new CultureInfo("vi-VN"));
                                            }
                                            catch
                                            {
                                                obj.Error.Add("Ngày chứng từ trả về [" + InvoiceReturnDate + "] không chính xác");
                                            }

                                        }
                                    }
                                    #endregion

                                    #region Kiểm tra nhóm sản phẩm
                                    var oData = GetDataGroupProduct(worksheet, row, excelInput, dataOrder, objSettingOrder, sValue, objSetting.IsSKU);
                                    var isSettingETD = objSettingOrder.ETD > 0 || objSettingOrder.ETDTime_RequestDate > 0;
                                    foreach (var o in oData)
                                    {
                                        o.IsReturn = IsReturn;
                                        var objDITO = dataExport.FirstOrDefault(c => c.DITOMasterID == obj.DITOMasterID && c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()
                                            && c.LocationFromID == o.LocationFromID && c.LocationToID == o.LocationToID && c.DNCode == o.DNCode && c.SOCode == o.SOCode
                                            && c.GroupProductID == o.GroupID && c.ProductID == o.ProductID
                                            && (c.TypeOfPacking == 1 ? c.Ton == o.Ton : c.TypeOfPacking == 2 ? c.CBM == o.CBM : c.Quantity == o.Quantity));
                                        if (objDITO == null)
                                            objDITO = dataExport.FirstOrDefault(c => c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()
                                                && c.LocationFromID == o.LocationFromID && c.LocationToID == o.LocationToID && c.DNCode == o.DNCode && c.SOCode == o.SOCode
                                                && c.GroupProductID == o.GroupID && c.ProductID == o.ProductID);

                                        // Hàng mới
                                        if (objDITO == null)
                                        {
                                            // Tìm nhóm hàng có trùng điểm nhận, giao
                                            objDITO = dataExport.FirstOrDefault(c => c.DITOMasterID == obj.DITOMasterID && c.OrderCode.Trim().ToLower() == o.OrderCode.Trim().ToLower()
                                                && c.LocationFromID == o.LocationFromID && c.LocationToID == o.LocationToID);

                                            if (objDITO == null)
                                                obj.Error.Add("Không tìm thấy sản phẩm [" + o.GroupCode + "] ĐH [" + o.OrderCode + "]. Dòng[" + row + "].");
                                            else
                                            {
                                                if (o.GroupID <= 0)
                                                {
                                                    obj.Error.Add("Không tìm thấy nhóm sản phẩm [" + o.GroupCode + "] ĐH [" + o.OrderCode + "]. Dòng[" + row + "].");
                                                }
                                                else
                                                {
                                                    if (o.ProductID <= 0)
                                                    {
                                                        obj.Error.Add("Không tìm thấy sản phẩm [" + o.ProductCode + "] ĐH [" + o.OrderCode + "]. Dòng[" + row + "].");
                                                    }
                                                    else
                                                    {
                                                        o.IsNew = true;
                                                        o.OPSGroupID = objDITO.ID;
                                                        obj.HasReturn = objDITO.HasReturn;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            o.IsNew = false;
                                            // Cập nhật hàng cũ
                                            o.OPSGroupID = objDITO.ID;
                                        }
                                        o.InvoiceReturnNote = excelPlanInput["InvoiceReturnNote"];
                                        o.InvoiceReturnDate = InvoiceRDate;
                                        o.ReasonCancelNote = excelPlanInput["ReasonCancelNote"];
                                        obj.ListGroup.Add(o);
                                    }
                                    #endregion
                                }
                            }
                        }
                    }

                    foreach (var item in result)
                    {
                        item.ExcelSuccess = true;
                        if (item.Error.Count > 0)
                        {
                            item.ExcelSuccess = false;
                            item.ExcelError = string.Join(", ", item.Error.Distinct().ToList());
                        }
                    }
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONImportInput_Excel_Import(dynamic dynParam)
        {
            try
            {
                int sID = (int)dynParam.sID;

                List<DTOMONInputImport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONInputImport>>(dynParam.Data.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONImportInput_Excel_Import(sID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DTOMONInputImport_Group> GetDataGroupProduct(ExcelWorksheet worksheet, int row, Dictionary<string, string> excelInput, DTOORDOrder_ImportCheck data, DTOCUSSettingOrder objSetting, List<string> sValue, bool IsSKU)
        {
            var dataRes = new List<DTOMONInputImport_Group>();

            int svID = -1, serviceID = -1, tmID = -1, transportID = -1;

            if (objSetting.TypeOfTransportModeID > 0)
            {
                tmID = objSetting.TypeOfTransportModeID;
                var objTM = data.ListTransportMode.FirstOrDefault(c => c.ID == tmID);
                if (objTM != null)
                    transportID = objTM.TransportModeID;
            }
            else
            {
                var str = excelInput["TypeOfTransportMode"];
                if (!string.IsNullOrEmpty(str))
                {
                    var objTM = data.ListTransportMode.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                    if (objTM != null)
                    {
                        tmID = objTM.ID;
                        transportID = objTM.TransportModeID;
                    }
                }
            }

            if (objSetting.ServiceOfOrderID > 0)
            {
                svID = objSetting.ServiceOfOrderID;
                var objSV = data.ListServiceOfOrder.FirstOrDefault(c => c.ID == svID);
                if (objSV != null)
                    serviceID = objSV.ServiceOfOrderID;
            }
            else
            {
                var str = excelInput["ServiceOfOrder"].Trim().ToLower();
                if (!string.IsNullOrEmpty(str))
                {
                    var objSV = data.ListServiceOfOrder.FirstOrDefault(c => c.Code.ToLower() == str.Trim().ToLower());
                    if (objSV != null)
                    {
                        svID = objSV.ID;
                        serviceID = objSV.ServiceOfOrderID;
                    }
                }
            }

            //Xe tải
            if ((transportID == iFTL || transportID == iLTL) && serviceID == iLO)
            {
                #region ĐH xe tải
                var eItem = new DTOOPSDIImportPacket_GroupProduct();
                var cusID = -1;

                #region Check tgian

                if (objSetting.RequestDate > 0)
                {
                    try
                    {
                        eItem.RequestDate = ExcelHelper.ValueToDate(excelInput["RequestDate"]);
                    }
                    catch
                    {
                        try
                        {
                            eItem.RequestDate = Convert.ToDateTime(excelInput["RequestDate"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                    if (objSetting.RequestTime > 0 && eItem.RequestDate != null)
                    {
                        if (!string.IsNullOrEmpty(excelInput["RequestTime"]))
                        {
                            try
                            {
                                eItem.RequestDate = eItem.RequestDate.Date.Add(TimeSpan.Parse(excelInput["RequestTime"]));
                            }
                            catch
                            {
                            }
                        }
                    }
                }
                if (objSetting.RequestDate_Time > 0)
                {
                    try
                    {
                        eItem.RequestDate = ExcelHelper.ValueToDate(excelInput["RequestDate_Time"]);
                    }
                    catch
                    {
                        try
                        {
                            eItem.RequestDate = Convert.ToDateTime(excelInput["RequestDate_Time"], new CultureInfo("vi-VN"));
                        }
                        catch { }
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["ETD"]))
                {
                    try
                    {
                        eItem.ETD = ExcelHelper.ValueToDate(excelInput["ETD"]);
                    }
                    catch
                    {
                        try
                        {
                            eItem.ETD = Convert.ToDateTime(excelInput["ETD"], new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                        }
                    }
                }
                else if (objSetting.ETDTime_RequestDate > 0 && eItem.RequestDate != null)
                {
                    if (!string.IsNullOrEmpty(excelInput["ETDTime_RequestDate"]))
                    {
                        try
                        {
                            eItem.ETD = eItem.RequestDate.Date.Add(TimeSpan.Parse(excelInput["ETDTime_RequestDate"]));
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        eItem.ETD = eItem.RequestDate;
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["ETA"]))
                {
                    try
                    {
                        eItem.ETA = ExcelHelper.ValueToDate(excelInput["ETA"]);
                    }
                    catch
                    {
                        try
                        {
                            eItem.ETA = Convert.ToDateTime(excelInput["ETA"], new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                        }
                    }
                }
                else if (objSetting.ETATime_RequestDate > 0 && eItem.RequestDate != null)
                {
                    if (!string.IsNullOrEmpty(excelInput["ETATime_RequestDate"]))
                    {
                        try
                        {
                            eItem.ETA = eItem.RequestDate.Date.Add(TimeSpan.Parse(excelInput["ETATime_RequestDate"]));
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        eItem.ETA = eItem.RequestDate;
                    }
                }

                if (!string.IsNullOrEmpty(excelInput["ETARequest"]))
                {
                    try
                    {
                        eItem.ETARequest = ExcelHelper.ValueToDate(excelInput["ETARequest"]);
                    }
                    catch
                    {
                        try
                        {
                            eItem.ETARequest = Convert.ToDateTime(excelInput["ETARequest"], new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                        }
                    }
                    if (eItem.ETARequest != null && objSetting.ETARequestTime > 0)
                    {
                        if (!string.IsNullOrEmpty(excelInput["ETARequestTime"]))
                        {
                            try
                            {
                                eItem.ETARequest = eItem.ETARequest.Value.Date.Add(TimeSpan.Parse(excelInput["ETARequestTime"]));
                            }
                            catch
                            {
                            }
                        }
                    }
                }

                #endregion

                #region Check Customer, Contract và Code

                if (objSetting.CustomerID == objSetting.SYSCustomerID)
                {
                    if (!string.IsNullOrEmpty(excelInput["CustomerCode"]))
                    {
                        var objCheck = data.ListCustomer.FirstOrDefault(c => c.Code.Trim().ToLower() == excelInput["CustomerCode"].Trim().ToLower());
                        if (objCheck != null)
                        {
                            cusID = objCheck.ID;
                        }
                    }
                }
                else
                {
                    cusID = objSetting.CustomerID;
                }

                eItem.OrderCode = excelInput["OrderCode"];

                #endregion

                #region Check nhà phân phối

                var pID = -1;
                string toCode = string.Empty;
                string toName = string.Empty;
                string dName = excelInput["DistributorName"];
                string dCode = excelInput["DistributorCode"];

                if (!string.IsNullOrEmpty(excelInput["DistributorCodeName"]))
                {
                    string[] s = excelInput["DistributorCodeName"].Split('-');
                    dCode = s[0];
                    if (s.Length > 1)
                    {
                        dName = excelInput["DistributorCodeName"].Substring(dCode.Length + 1);
                    }
                }

                if (!string.IsNullOrEmpty(dCode))
                {
                    var objCheck = data.ListDistributor.FirstOrDefault(c => c.PartnerCode.Trim().ToLower() == dCode.Trim().ToLower() && c.CustomerID == cusID);
                    if (objCheck != null)
                    {
                        pID = objCheck.CUSPartnerID;
                        toCode = excelInput["LocationToCode"];
                        toName = excelInput["LocationToName"];
                        if (objSetting.LocationToCodeName > 0)
                        {
                            if (!string.IsNullOrEmpty(excelInput["LocationToCodeName"]))
                            {
                                toCode = excelInput["LocationToCodeName"].Split('-').FirstOrDefault();
                                toName = excelInput["LocationToCodeName"].Split('-').Skip(1).FirstOrDefault();
                            }
                            else
                            {
                                toCode = string.Empty;
                                toName = string.Empty;
                            }
                        }

                        //Tìm theo code
                        var objTo = data.ListDistributorLocation.FirstOrDefault(c => c.CusPartID == pID && c.LocationCode.Trim().ToLower() == toCode.Trim().ToLower());
                        if (objTo != null)
                        {
                            eItem.LocationToID = objTo.CUSLocationID;
                        }
                        else
                        {
                            objTo = data.ListDistributorLocation.FirstOrDefault(c => c.CusPartID == pID && c.Address.Trim().ToLower() == excelInput["LocationToAddress"].Trim().ToLower());
                            if (objTo != null)
                            {
                                eItem.LocationToID = objTo.CUSLocationID;
                            }
                        }
                    }
                }

                #endregion

                #region Check sản lượng, kho, nhóm sản phẩm và đơn vị tính

                //Dictionary quantity theo kho. [Q = Quantity]
                Dictionary<int, Dictionary<int, double>> dicQ = new Dictionary<int, Dictionary<int, double>>();
                //Dictionary chi tiết kho. [L = Location]
                Dictionary<int, DTOORDData_Location> dicL = new Dictionary<int, DTOORDData_Location>();
                //Dictionary chi tiết nhóm sản phẩm đầu tiên/chỉ định trong kho. [GS = GroupProductInStock]
                Dictionary<int, DTOORDData_GroupProduct> dicGS = new Dictionary<int, DTOORDData_GroupProduct>();

                //Dictionary quantity theo kho-nhóm hàng-hàng hóa. [QP = QuantityProduct]
                Dictionary<string, Dictionary<int, double>> dicQP = new Dictionary<string, Dictionary<int, double>>();

                //Nếu thiết lập kho theo cột, check kho, lấy sản lượng theo excel.
                if (objSetting.HasStock && objSetting.ListStock != null)
                {
                    foreach (var stock in objSetting.ListStock)
                    {
                        int sID = stock.StockID;
                        var objCheck = data.ListStock.FirstOrDefault(c => c.CUSLocationID == sID && c.CustomerID == cusID);
                        if (objCheck != null)
                        {
                            dicL.Add(sID, objCheck);
                        }

                        Dictionary<int, double> dicV = new Dictionary<int, double>();
                        var dicS = GetDataValue(worksheet, stock, row, sValue);
                        if (!dicS.Values.All(c => string.IsNullOrEmpty(c)))
                        {
                            try
                            {
                                if (!string.IsNullOrEmpty(dicS["Ton"]))
                                    dicV.Add(1, Convert.ToDouble(dicS["Ton"]));
                                else
                                    dicV.Add(1, 0);
                            }
                            catch
                            {
                                dicV.Add(1, 0);
                            }
                            try
                            {
                                if (!string.IsNullOrEmpty(dicS["CBM"]))
                                    dicV.Add(2, Convert.ToDouble(dicS["CBM"]));
                                else
                                    dicV.Add(2, 0);
                            }
                            catch
                            {
                                dicV.Add(2, 0);
                            }
                            try
                            {
                                if (!string.IsNullOrEmpty(dicS["Quantity"]))
                                    dicV.Add(3, Convert.ToDouble(dicS["Quantity"]));
                                else
                                    dicV.Add(3, 0);
                            }
                            catch
                            {
                                dicV.Add(3, 0);
                            }
                            dicQ.Add(sID, dicV);
                        }
                    }
                }
                else if (objSetting.HasStockProduct)
                {
                    foreach (var stock in objSetting.ListStockWithProduct)
                    {
                        var cusStock = data.ListStock.FirstOrDefault(c => c.CUSLocationID == stock.StockID && c.CustomerID == cusID);
                        var cusGroup = data.ListGroupOfProduct.FirstOrDefault(c => c.CUSStockID == stock.StockID && c.ID == stock.GroupOfProductID);
                        var cusProduct = data.ListProduct.FirstOrDefault(c => c.GroupOfProductID == stock.GroupOfProductID && stock.ProductID == c.ID);
                        if (cusStock != null && cusGroup != null && cusProduct != null)
                        {
                            Dictionary<int, double> dicV = new Dictionary<int, double>();
                            var dicS = GetDataValue(worksheet, stock, row, sValue);
                            if (!dicS.Values.All(c => string.IsNullOrEmpty(c)))
                            {
                                try
                                {
                                    if (!string.IsNullOrEmpty(dicS["Ton"]))
                                        dicV.Add(1, Convert.ToDouble(dicS["Ton"]));
                                    else
                                        dicV.Add(1, 0);
                                }
                                catch
                                {
                                    dicV.Add(1, 0);
                                }
                                try
                                {
                                    if (!string.IsNullOrEmpty(dicS["CBM"]))
                                        dicV.Add(2, Convert.ToDouble(dicS["CBM"]));
                                    else
                                        dicV.Add(2, 0);
                                }
                                catch
                                {
                                    dicV.Add(2, 0);
                                }
                                try
                                {
                                    if (!string.IsNullOrEmpty(dicS["Quantity"]))
                                        dicV.Add(3, Convert.ToDouble(dicS["Quantity"]));
                                    else
                                        dicV.Add(3, 0);
                                }
                                catch
                                {
                                    dicV.Add(3, 0);
                                }
                                var key = stock.StockID + "-" + stock.GroupOfProductID + "-" + stock.ProductID;
                                dicQP.Add(key, dicV);
                            }
                        }
                    }
                }
                //Mỗi dòng 1 kho, check kho, lấy sản lượng theo excel.
                else
                {
                    int sID = -1;
                    if (objSetting.LocationFromCode < 1 && objSetting.LocationFromCodeName < 1)
                    {
                        if (data.ListStock.Count(c => c.CustomerID == cusID) == 1)
                        {
                            var objCheck = data.ListStock.FirstOrDefault(c => c.CustomerID == cusID);

                            sID = objCheck.CUSLocationID;
                            dicL.Add(sID, objCheck);
                        }
                    }
                    else
                    {
                        var sCode = excelInput["LocationFromCode"];
                        if (objSetting.LocationFromCodeName > 0)
                        {
                            if (!string.IsNullOrEmpty(excelInput["LocationFromCodeName"]))
                            {
                                sCode = excelInput["LocationFromCodeName"].Split('-').FirstOrDefault();
                            }
                            else
                            {
                                sCode = string.Empty;
                            }
                        }
                        var objCheck = data.ListStock.FirstOrDefault(c => c.CustomerID == cusID && c.LocationCode.ToLower().Trim() == sCode.ToLower().Trim());
                        if (objCheck != null)
                        {
                            sID = objCheck.CUSLocationID;
                            dicL.Add(sID, objCheck);
                        }
                        else
                        {
                            dicL.Add(-1, new DTOORDData_Location());
                        }
                    }


                    if (!string.IsNullOrEmpty(excelInput["Ton_SKU"]) || !string.IsNullOrEmpty(excelInput["CBM_SKU"]) || !string.IsNullOrEmpty(excelInput["Quantity_SKU"]))
                    {
                        Dictionary<int, double> dicV = new Dictionary<int, double>();
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["Ton_SKU"]))
                                dicV.Add(1, Convert.ToDouble(excelInput["Ton_SKU"]));
                            else
                                dicV.Add(1, 0);
                        }
                        catch
                        {
                            dicV.Add(1, 0);
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["CBM_SKU"]))
                                dicV.Add(2, Convert.ToDouble(excelInput["CBM_SKU"]));
                            else
                                dicV.Add(2, 0);
                        }
                        catch
                        {
                            dicV.Add(2, 0);
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["Quantity_SKU"]))
                                dicV.Add(3, Convert.ToDouble(excelInput["Quantity_SKU"]));
                            else
                                dicV.Add(3, 0);
                        }
                        catch
                        {
                            dicV.Add(3, 0);
                        }
                        dicQ.Add(sID, dicV);
                    }
                    else
                    {
                        Dictionary<int, double> dicV = new Dictionary<int, double>();
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["Ton"]))
                                dicV.Add(1, Convert.ToDouble(excelInput["Ton"]));
                            else
                                dicV.Add(1, 0);
                        }
                        catch
                        {
                            dicV.Add(1, 0);
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["CBM"]))
                                dicV.Add(2, Convert.ToDouble(excelInput["CBM"]));
                            else
                                dicV.Add(2, 0);
                        }
                        catch
                        {
                            dicV.Add(2, 0);
                        }
                        try
                        {
                            if (!string.IsNullOrEmpty(excelInput["Quantity"]))
                                dicV.Add(3, Convert.ToDouble(excelInput["Quantity"]));
                            else
                                dicV.Add(3, 0);
                        }
                        catch
                        {
                            dicV.Add(3, 0);
                        }
                        dicQ.Add(sID, dicV);
                    }
                }

                string strGopCode = string.Empty;
                //Dictionary Product theo GroupProduct. [P = Product] - Key: GroupOfProductID
                Dictionary<int, int> dicP = new Dictionary<int, int>();
                //Dictionary ProductCode theo GroupProduct. [PCode = ProductCode] - Key: GroupOfProductID
                Dictionary<int, string> dicPCode = new Dictionary<int, string>();
                Dictionary<int, bool> dicPIsKG = new Dictionary<int, bool>();

                //Nếu không có cột nhóm SP, check sản phẩm ko nhóm (ProductCodeWithoutGroup)
                //Nếu không có cột nhóm SP, kiểm tra kho có duy nhất nhóm SP => Lấy
                if (objSetting.GroupProductCode == 0 && objSetting.GroupProductCodeNotUnicode == 0)
                {
                    if (objSetting.ProductCodeWithoutGroup > 0)
                    {
                        if (!string.IsNullOrEmpty(excelInput["ProductCodeWithoutGroup"]))
                        {
                            var objP = data.ListProduct.FirstOrDefault(c => c.Code == excelInput["ProductCodeWithoutGroup"] && c.CustomerID == cusID);
                            if (objP != null)
                            {
                                foreach (var st in dicQ)
                                {
                                    var objGS = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == objP.GroupOfProductID && c.CUSStockID == st.Key);
                                    if (objGS != null)
                                    {
                                        strGopCode = objGS.Code;
                                        dicGS.Add(st.Key, objGS);
                                        dicP.Add(objGS.ID, objP.ID);
                                        dicPCode.Add(objGS.ID, objP.Code);
                                        dicPIsKG.Add(objGS.ID, objP.IsKg);
                                    }
                                    else
                                    {
                                        dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var st in dicQ)
                        {
                            var dataGS = data.ListGroupOfProduct.Where(c => c.CustomerID == cusID && c.CUSStockID == st.Key).ToList();
                            if (dataGS.Count == 0)
                            {
                                dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                            }
                            else if (dataGS.Count == 1)
                            {
                                var objCheck = dataGS.FirstOrDefault();
                                strGopCode = objCheck.Code;
                                dicGS.Add(st.Key, objCheck);
                            }
                            else
                            {
                                var objCheck = dataGS.FirstOrDefault(c => c.IsDefault == true);
                                if (objCheck != null)
                                {
                                    strGopCode = objCheck.Code;
                                    dicGS.Add(st.Key, objCheck);
                                }
                                else
                                {
                                    dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                }
                            }
                        }
                    }
                }
                //Kiểm tra nhóm SP có tồn tại + có trong kho.
                else if (objSetting.ProductCodeWithoutGroup == 0)
                {
                    if (objSetting.GroupProductCode > 0)
                        strGopCode = excelInput["GroupProductCode"];
                    else
                        strGopCode = StringHelper.RemoveSign4VietnameseString(excelInput["GroupProductCodeNotUnicode"]);

                    if (!string.IsNullOrEmpty(strGopCode))
                    {
                        var objGop = data.ListGroupOfProduct.FirstOrDefault(c => c.CustomerID == cusID && c.Code.Trim().ToLower() == strGopCode.Trim().ToLower());
                        if (objGop != null)
                        {
                            foreach (var st in dicQ)
                            {
                                if (data.ListGroupOfProduct.Count(c => c.CustomerID == cusID && c.ID == objGop.ID && c.CUSStockID == st.Key) == 0)
                                {
                                    dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                }
                                else
                                {
                                    var objCheck = data.ListGroupOfProduct.FirstOrDefault(c => c.CustomerID == cusID && c.ID == objGop.ID && c.CUSStockID == st.Key);
                                    dicGS.Add(st.Key, objCheck);
                                }
                            }
                        }
                        else
                        {
                            dicGS.Add(-1, new DTOORDData_GroupProduct());
                        }
                    }
                    else
                    {
                        foreach (var st in dicQ)
                        {
                            var dataGS = data.ListGroupOfProduct.Where(c => c.CustomerID == cusID && c.CUSStockID == st.Key).ToList();
                            if (dataGS.Count == 0)
                            {
                                dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                            }
                            else if (dataGS.Count == 1)
                            {
                                var objCheck = dataGS.FirstOrDefault();
                                strGopCode = objCheck.Code;
                                dicGS.Add(st.Key, objCheck);
                            }
                            else
                            {
                                var objCheck = dataGS.FirstOrDefault(c => c.IsDefault == true);
                                if (objCheck != null)
                                {
                                    strGopCode = objCheck.Code;
                                    dicGS.Add(st.Key, objCheck);
                                }
                                else
                                {
                                    dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                }
                            }
                        }
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(excelInput["ProductCodeWithoutGroup"]))
                    {
                        var objP = data.ListProduct.FirstOrDefault(c => c.Code == excelInput["ProductCodeWithoutGroup"] && c.CustomerID == cusID);
                        if (objP != null)
                        {
                            foreach (var st in dicQ)
                            {
                                var objGS = data.ListGroupOfProduct.FirstOrDefault(c => c.ID == objP.GroupOfProductID && c.CUSStockID == st.Key);
                                if (objGS != null)
                                {
                                    strGopCode = objGS.Code;
                                    dicGS.Add(st.Key, objGS);
                                    dicP.Add(objGS.ID, objP.ID);
                                    dicPCode.Add(objGS.ID, objP.Code);
                                    dicPIsKG.Add(objGS.ID, objP.IsKg);
                                }
                                else
                                {
                                    dicGS.Add(st.Key, new DTOORDData_GroupProduct());
                                }
                            }
                        }
                    }
                }

                if (objSetting.ProductCodeWithoutGroup == 0)
                {
                    if (objSetting.Packing == 0 && objSetting.PackingNotUnicode == 0)
                    {
                        foreach (var gop in dicGS)
                        {
                            if (!dicP.ContainsKey(gop.Value.ID))
                            {
                                var dataProduct = data.ListProduct.Where(c => c.GroupOfProductID == gop.Value.ID && c.CustomerID == cusID).ToList();
                                if (dataProduct.Count == 1)
                                {
                                    dicP.Add(gop.Value.ID, dataProduct[0].ID);
                                    dicPCode.Add(gop.Value.ID, dataProduct[0].Code);
                                    dicPIsKG.Add(gop.Value.ID, dataProduct[0].IsKg);
                                }
                                else
                                {
                                    var objDefault = dataProduct.FirstOrDefault(c => c.IsDefault == true);
                                    if (objDefault != null)
                                    {
                                        dicP.Add(gop.Value.ID, objDefault.ID);
                                        dicPCode.Add(gop.Value.ID, objDefault.Code);
                                        dicPIsKG.Add(gop.Value.ID, objDefault.IsKg);
                                    }
                                    else
                                    {
                                        dicP.Add(gop.Value.ID, -1);
                                        dicPCode.Add(gop.Value.ID, "");
                                        dicPIsKG.Add(gop.Value.ID, false);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (var gop in dicGS)
                        {
                            if (!dicP.ContainsKey(gop.Value.ID))
                            {
                                var dataProduct = data.ListProduct.Where(c => c.GroupOfProductID == gop.Value.ID && c.CustomerID == cusID).ToList();
                                if (dataProduct.Count > 0)
                                {
                                    var str = string.Empty;
                                    if (objSetting.Packing > 0)
                                        str = excelInput["Packing"];
                                    else if (objSetting.PackingNotUnicode > 0)
                                        str = StringHelper.RemoveSign4VietnameseString(excelInput["PackingNotUnicode"]);

                                    if (string.IsNullOrEmpty(str))
                                    {
                                        var objDefault = dataProduct.FirstOrDefault(c => c.IsDefault == true);
                                        if (objDefault != null)
                                        {
                                            dicP.Add(gop.Value.ID, objDefault.ID);
                                            dicPCode.Add(gop.Value.ID, objDefault.Code);
                                            dicPIsKG.Add(gop.Value.ID, objDefault.IsKg);
                                        }
                                        else
                                        {
                                            dicP.Add(gop.Value.ID, -1);
                                            dicPCode.Add(gop.Value.ID, "");
                                            dicPIsKG.Add(gop.Value.ID, false);
                                        }
                                    }
                                    else
                                    {
                                        var product = dataProduct.FirstOrDefault(c => c.Code.ToLower().Trim() == str.ToLower().Trim());
                                        if (product != null)
                                        {
                                            dicP.Add(gop.Value.ID, product.ID);
                                            dicPCode.Add(gop.Value.ID, product.Code);
                                            dicPIsKG.Add(gop.Value.ID, product.IsKg);
                                        }
                                        else
                                        {
                                            dicP.Add(gop.Value.ID, -1);
                                            dicPCode.Add(gop.Value.ID, "");
                                            dicPIsKG.Add(gop.Value.ID, false);
                                        }
                                    }
                                }
                                else
                                {
                                    dicP.Add(gop.Value.ID, -1);
                                    dicPCode.Add(gop.Value.ID, "");
                                    dicPIsKG.Add(gop.Value.ID, false);
                                }
                            }
                        }
                    }
                }

                #endregion

                #region Lưu dữ liệu

                foreach (var dic in dicQ)
                {
                    var gop = new DTOORDData_GroupProduct();
                    try
                    {
                        gop = dicGS[dic.Key];
                    }
                    catch
                    {
                        gop.Code = excelInput["GroupProductCode"];
                    }

                    var item = new DTOMONInputImport_Group();
                    item.IsFTL = transportID == iFTL;
                    item.SOCode = excelInput["SOCode"];
                    item.DNCode = excelInput["DNCode"];
                    item.OrderCode = excelInput["OrderCode"];
                    item.GroupID = gop.ID;
                    item.GroupCode = gop.Code;
                    item.GroupName = gop.GroupName;
                    item.Note = excelInput["Note"];
                    item.Note1 = excelInput["Note1"];
                    item.Note2 = excelInput["Note2"];
                    try
                    {
                        item.ProductID = dicP[gop.ID];
                        item.ProductCode = dicPCode[gop.ID];
                    }
                    catch
                    { }
                    item.LocationToID = eItem.LocationToID;
                    item.LocationFromID = dic.Key;

                    item.ETD = eItem.ETD;
                    item.ETA = eItem.ETA;
                    item.ETARequest = eItem.ETARequest;

                    var objProduct = data.ListProduct.FirstOrDefault(c => c.ID == item.ProductID && c.CustomerID == cusID);
                    if (objProduct != null)
                    {
                        if (!IsSKU)
                        {
                            item.Ton = Math.Round(dic.Value[1], digitsOfRound, MidpointRounding.AwayFromZero);
                            item.CBM = Math.Round(dic.Value[2], digitsOfRound, MidpointRounding.AwayFromZero);
                            item.Quantity = Math.Round(dic.Value[3], digitsOfRound, MidpointRounding.AwayFromZero);
                            if (objProduct.IsKg)
                                item.Ton = item.Ton / 1000;
                        }
                        else
                        {
                            switch (objProduct.PackingTypeGOP)
                            {
                                case 1:
                                    item.Ton = dic.Value[1];

                                    if (objProduct.IsKg)
                                        item.Ton = item.Ton / 1000;
                                    if (objProduct.Weight.HasValue && objProduct.Weight != 0)
                                        item.Quantity = item.Ton / objProduct.Weight.Value;
                                    if (objProduct.CBM.HasValue)
                                        item.CBM = item.Quantity * objProduct.CBM.Value;
                                    break;
                                case 2:
                                    item.CBM = dic.Value[2];
                                    if (objProduct.CBM.HasValue && objProduct.CBM != 0)
                                        item.Quantity = item.CBM / objProduct.CBM.Value;
                                    if (objProduct.Weight.HasValue)
                                        item.Ton = item.Quantity * objProduct.Weight.Value;
                                    break;
                                case 3:
                                    item.Quantity = dic.Value[3];
                                    if (objProduct.CBM.HasValue)
                                        item.CBM = item.Quantity * objProduct.CBM.Value;
                                    if (objProduct.Weight.HasValue)
                                        item.Ton = item.Quantity * objProduct.Weight.Value;
                                    break;
                                default:
                                    break;
                            }
                            item.Ton = Math.Round(item.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                            item.CBM = Math.Round(item.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                            item.Quantity = Math.Round(item.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                        }
                    }
                    dataRes.Add(item);
                }
                foreach (var dic in dicQP)
                {
                    var tmp = dic.Key.ToString().Split('-').ToList();
                    var item = new DTOMONInputImport_Group();
                    item.IsFTL = transportID == iFTL;
                    item.SOCode = excelInput["SOCode"];
                    item.DNCode = excelInput["DNCode"];
                    item.OrderCode = excelInput["OrderCode"];
                    item.Note = excelInput["Note"];
                    item.Note1 = excelInput["Note1"];
                    item.Note2 = excelInput["Note2"];
                    var cusStock = data.ListStock.FirstOrDefault(c => c.CUSLocationID.ToString() == tmp[0] && c.CustomerID == cusID);
                    var cusGroup = data.ListGroupOfProduct.FirstOrDefault(c => c.CUSStockID.ToString() == tmp[0] && c.ID.ToString() == tmp[1]);
                    var cusProduct = data.ListProduct.FirstOrDefault(c => c.GroupOfProductID.ToString() == tmp[1] && c.ID.ToString() == tmp[2]);
                    if (cusStock != null && cusGroup != null && cusProduct != null)
                    {
                        item.GroupID = cusGroup.ID;
                        item.GroupCode = cusGroup.Code;
                        item.GroupName = cusGroup.GroupName;
                        item.ProductID = cusProduct.ID;
                        item.ProductCode = cusProduct.Code;
                        item.LocationToID = eItem.LocationToID;
                        item.LocationFromID = cusStock.CUSLocationID;
                        item.ETD = eItem.ETD;
                        item.ETA = eItem.ETA;
                        item.ETARequest = eItem.ETARequest;
                        if (!IsSKU)
                        {
                            item.Ton = Math.Round(dic.Value[1], digitsOfRound, MidpointRounding.AwayFromZero);
                            item.CBM = Math.Round(dic.Value[2], digitsOfRound, MidpointRounding.AwayFromZero);
                            item.Quantity = Math.Round(dic.Value[3], digitsOfRound, MidpointRounding.AwayFromZero);
                            if (cusProduct.IsKg)
                                item.Ton = item.Ton / 1000;
                        }
                        else
                        {
                            switch (cusProduct.PackingTypeGOP)
                            {
                                case 1:
                                    item.Ton = dic.Value[1];

                                    if (cusProduct.IsKg)
                                        item.Ton = item.Ton / 1000;
                                    if (cusProduct.Weight.HasValue && cusProduct.Weight != 0)
                                        item.Quantity = item.Ton / cusProduct.Weight.Value;
                                    if (cusProduct.CBM.HasValue)
                                        item.CBM = item.Quantity * cusProduct.CBM.Value;
                                    break;
                                case 2:
                                    item.CBM = dic.Value[2];
                                    if (cusProduct.CBM.HasValue && cusProduct.CBM != 0)
                                        item.Quantity = item.CBM / cusProduct.CBM.Value;
                                    if (cusProduct.Weight.HasValue)
                                        item.Ton = item.Quantity * cusProduct.Weight.Value;
                                    break;
                                case 3:
                                    item.Quantity = dic.Value[3];
                                    if (cusProduct.CBM.HasValue)
                                        item.CBM = item.Quantity * cusProduct.CBM.Value;
                                    if (cusProduct.Weight.HasValue)
                                        item.Ton = item.Quantity * cusProduct.Weight.Value;
                                    break;
                                default:
                                    break;
                            }
                            item.Ton = Math.Round(item.Ton, digitsOfRound, MidpointRounding.AwayFromZero);
                            item.CBM = Math.Round(item.CBM, digitsOfRound, MidpointRounding.AwayFromZero);
                            item.Quantity = Math.Round(item.Quantity, digitsOfRound, MidpointRounding.AwayFromZero);
                        }
                        dataRes.Add(item);
                    }
                }

                #endregion

                #endregion
            }

            return dataRes;
        }
        //Import Ext
        [HttpPost]
        public DTOResult MONExtImport_Index_Setting_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONExtImport_Index_Setting_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string MONExtImport_Index_Setting_Download(dynamic dynParam)
        {
            try
            {
                string file = "";
                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                DTOCUSSettingMONExt objSetting = new DTOCUSSettingMONExt();
                List<DTOMONExtImport> data = new List<DTOMONExtImport>();
                DTOMONExtImportExcel objImport = new DTOMONExtImportExcel();

                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    objImport = sv.MONExtImport_Data(dtfrom, dtto, customerID, templateID);
                    objSetting = sv.MONExtImport_Index_Setting_Get(templateID);
                });

                if (objImport != null)
                {
                    data = objImport.data;
                }
                string[] aValue = { "CustomerID", "SYSCustomerID", "SettingID", "CreateBy", "CreateDate", "Name", "RowStart",
                                          "SettingCustomerCode", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "DITOGroupProductStatusPODName", "DITOGroupProductStatusPODID", "VehicleID", "IsNew" };
                List<string> sValue = new List<string>(aValue);

                Dictionary<string, string> dicName = MONExtGetDataName();

                if (objSetting != null)
                {
                    file = "/Uploads/temp/" + objSetting.Name.Replace(' ', '-') + DateTime.Now.ToString("ddMMyyyyHHmmss") + ".xlsx";

                    file = file.Replace("+", "");

                    if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                        System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                    FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                    using (ExcelPackage package = new ExcelPackage(exportfile))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets.Add(objSetting.Name);
                        if (objSetting.RowStart > 1)
                        {
                            Dictionary<string, int> dicPos = new Dictionary<string, int>();

                            int row = 1;
                            foreach (var prop in objSetting.GetType().GetProperties())
                            {
                                try
                                {
                                    var p = prop.Name;
                                    if (!sValue.Contains(p))
                                    {
                                        var v = (int)prop.GetValue(objSetting, null);
                                        if (v > 0)
                                        {
                                            if (dicName.ContainsKey(p))
                                                worksheet.Cells[row, v].Value = dicName[p];
                                            else
                                                worksheet.Cells[row, v].Value = p;

                                            dicPos.Add(p, v);
                                            worksheet.Column(v).Width = 20;
                                        }
                                    }
                                }
                                catch (Exception)
                                {
                                }
                            }
                            row = 2;
                            foreach (var item in data)
                            {
                                int type = 0;
                                foreach (var pos in dicPos)
                                {
                                    try
                                    {
                                        var property = pos.Key;
                                        var value = GetObjProperty(item, property, ref type);
                                        worksheet.Cells[row, pos.Value].Value = value;
                                        if (type == 1) //Loại datetime
                                        {
                                            if (property == "DateFromLoadStart" || property == "DateFromLoadEnd" || property == "DateToLoadStart" || property == "DateToLoadEnd")
                                            {
                                                ExcelHelper.CreateFormat(worksheet, row, pos.Value, ExcelHelper.FormatHHMM);
                                            }
                                            else if (property == "MasterETDDatetime" || property == "OrderGroupETDDatetime")
                                            {
                                                ExcelHelper.CreateFormat(worksheet, row, pos.Value, ExcelHelper.FormatDMYHM);
                                            }
                                            else
                                            {
                                                ExcelHelper.CreateFormat(worksheet, row, pos.Value, ExcelHelper.FormatDDMMYYYY);
                                            }
                                        }
                                    }
                                    catch (Exception)
                                    {

                                    }
                                }

                                row++;
                            }
                        }

                        package.Save();
                    }
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public object GetObjProperty(object obj, string property, ref int type)
        {
            Type t = obj.GetType();
            PropertyInfo p = t.GetProperty(property);
            var value = p.GetValue(obj, null);
            if (t.Equals(typeof(bool)))
            {
                bool flag = (bool)value;
                if (flag == true)
                {
                    value = "x";
                }
            }

            type = 0;
            if (value != null)
            {
                Type t1 = value.GetType();
                if (t1.Equals(typeof(DateTime)))
                {
                    type = 1;
                }
                else if (t1.Equals(typeof(bool)))
                {
                    bool flag = (bool)value;
                    if (flag == true)
                    {
                        value = "x";
                    }
                    else
                    {
                        value = "";
                    }
                }
            }

            return value;
        }

        [HttpPost]
        public void MONExtImport_Excel_Import(dynamic dynParam)
        {
            try
            {
                int templateID = (int)dynParam.TemplateID;

                List<DTOMONExtImport> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONExtImport>>(dynParam.Data.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONExtImport_Excel_Import(templateID, data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMONExtImport> MONExtImport_Excel_Check(dynamic dynParam)
        {
            try
            {
                string file = "/" + dynParam.file.ToString();

                int templateID = (int)dynParam.templateID;
                int customerID = (int)dynParam.customerID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());

                DTOCUSSettingMONExt objSetting = new DTOCUSSettingMONExt();
                DTOMONExtImportExcel objImport = new DTOMONExtImportExcel();

                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    objImport = sv.MONExtImport_Data(dtfrom, dtto, customerID, templateID);
                    objSetting = sv.MONExtImport_Index_Setting_Get(templateID);
                });


                List<DTOMONExtImport> data = new List<DTOMONExtImport>();
                List<CATVehicle> lstVehicle = new List<CATVehicle>();
                List<DTOCustomer> lstVendor = new List<DTOCustomer>();

                if (objImport != null)
                {
                    data = objImport.data;
                    lstVehicle = objImport.lstVehicle;
                    lstVendor = objImport.lstVendor;
                }

                var dataRes = new List<DTOMONExtImport>();


                if (objSetting != null)
                {
                    //Check các required.

                    string[] aValue = { "CustomerID", "SYSCustomerID", "SettingID", "CreateBy", "CreateDate", "Name", "RowStart",
                                          "SettingCustomerCode", "SettingCustomerName", "TypeOfTransportModeName", "TypeOfTransportModeID", "VehicleID", "IsNew", "VENLoadCodeID", "VENUnLoadCodeID" };

                    var sValue = new List<string>(aValue);

                    using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath(file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                    {
                        using (var package = new ExcelPackage(fs))
                        {
                            ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                            if (worksheet != null)
                            {
                                int row = 0;
                                int failMax = 2;
                                int failCurrent = 0;
                                for (row = objSetting.RowStart; row <= worksheet.Dimension.End.Row; row++)
                                {
                                    var obj = new DTOMONExtImport();
                                    var lstError = new List<string>();

                                    var excelInput = MONExtGetDataValue(worksheet, objSetting, row, sValue);
                                    if (excelInput.Count(c => !string.IsNullOrEmpty(c.Value)) > 0)
                                    {
                                        #region Check dữ liệu
                                        var ID = excelInput["ID"];
                                        var RegNo = excelInput["RegNo"];

                                        if (string.IsNullOrEmpty(RegNo))
                                        {
                                            obj.VehicleID = -1;
                                        }
                                        else
                                        {
                                            var checkRegNo = lstVehicle.FirstOrDefault(c => c.RegNo.ToLower() == RegNo.ToLower());
                                            if (checkRegNo == null)// xe moi
                                            {
                                                lstError.Add("Xe [" + RegNo + "] không tồn tại");
                                            }
                                            else//da ton tai so xe
                                            {
                                                obj.VehicleID = checkRegNo.ID;
                                                obj.RegNo = checkRegNo.RegNo;
                                            }
                                        }

                                        obj.CustomerCode = excelInput["CustomerCode"];
                                        obj.OrderCode = excelInput["OrderCode"];
                                        obj.DNCode = excelInput["DNCode"];
                                        obj.SOCode = excelInput["SOCode"];
                                        obj.LocationFromCode = excelInput["LocationFromCode"];
                                        obj.LocationToCode = excelInput["LocationToCode"];
                                        var RequestDate = excelInput["RequestDate"];
                                        if (!string.IsNullOrEmpty(RequestDate))
                                        {
                                            try
                                            {
                                                obj.RequestDate = ExcelHelper.ValueToDate(RequestDate);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.RequestDate = Convert.ToDateTime(RequestDate, new CultureInfo("vi-VN"));
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày gửi y/c [" + RequestDate + "] không chính xác");
                                                }

                                            }
                                        }
                                        else
                                            obj.RequestDate = null;

                                        var IsInvoiceStr = excelInput["IsInvoice"];
                                        if (IsInvoiceStr.ToLower().Trim() == "x")
                                        {
                                            obj.IsInvoice = true;
                                        }
                                        else
                                        {
                                            obj.IsInvoice = false;
                                        }

                                        var DateFromCome = excelInput["DateFromCome"];
                                        if (!string.IsNullOrEmpty(DateFromCome))
                                        {
                                            try
                                            {
                                                obj.DateFromCome = ExcelHelper.ValueToDate(DateFromCome);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.DateFromCome = Convert.ToDateTime(DateFromCome, new CultureInfo("vi-VN"));
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày đến kho [" + DateFromCome + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.DateFromCome = null;

                                        var DateFromLoadStart = excelInput["DateFromLoadStart"];
                                        if (!string.IsNullOrEmpty(DateFromLoadStart) && obj.DateFromCome != null)
                                        {
                                            DateTime temp = new DateTime();
                                            TimeSpan time;
                                            if (!TimeSpan.TryParse(DateFromLoadStart, out time))
                                            {
                                                lstError.Add("Thời gian vào máng [" + DateFromLoadStart + "] không chính xác");
                                            }
                                            else
                                            {
                                                temp = obj.DateFromCome.Value.Date + time;
                                                obj.DateFromLoadStart = temp;
                                            }
                                        }
                                        else obj.DateFromLoadStart = null;

                                        var DateFromLeave = excelInput["DateFromLeave"];

                                        if (!string.IsNullOrEmpty(DateFromLeave))
                                        {
                                            try
                                            {
                                                obj.DateFromLeave = ExcelHelper.ValueToDate(DateFromLeave);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.DateFromLeave = Convert.ToDateTime(DateFromLeave, new CultureInfo("vi-VN"));
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày rời kho [" + DateFromLeave + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.DateFromLeave = null;

                                        var DateFromLoadEnd = excelInput["DateFromLoadEnd"];
                                        if (!string.IsNullOrEmpty(DateFromLoadEnd) && obj.DateFromLeave != null)
                                        {
                                            DateTime temp = new DateTime();
                                            TimeSpan time;
                                            if (!TimeSpan.TryParse(DateFromLoadEnd, out time))
                                            {
                                                lstError.Add("Thời gian ra máng [" + DateFromLoadEnd + "] không chính xác");
                                            }
                                            else
                                            {
                                                temp = obj.DateFromLeave.Value.Date + time;
                                                obj.DateFromLoadEnd = temp;
                                            }
                                        }
                                        else obj.DateFromLoadEnd = null;

                                        var DateToCome = excelInput["DateToCome"];
                                        if (!string.IsNullOrEmpty(DateToCome))
                                        {
                                            try
                                            {
                                                obj.DateToCome = ExcelHelper.ValueToDate(DateToCome);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.DateToCome = Convert.ToDateTime(DateToCome, new CultureInfo("vi-VN"));
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày đến NPP [" + DateToCome + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.DateToCome = null;

                                        var DateToLoadStart = excelInput["DateToLoadStart"];
                                        if (!string.IsNullOrEmpty(DateToLoadStart) && obj.DateToCome != null)
                                        {
                                            DateTime temp = new DateTime();
                                            TimeSpan time;
                                            if (!TimeSpan.TryParse(DateToLoadStart, out time))
                                            {
                                                lstError.Add("Thời gian ra máng [" + DateToLoadStart + "] không chính xác");
                                            }
                                            else
                                            {
                                                temp = obj.DateToCome.Value.Date + time;
                                                obj.DateToLoadStart = temp;
                                            }
                                        }
                                        else obj.DateToLoadStart = null;

                                        var DateToLeave = excelInput["DateToLeave"];
                                        if (!string.IsNullOrEmpty(DateToLeave))
                                        {
                                            try
                                            {
                                                obj.DateToLeave = ExcelHelper.ValueToDate(DateToLeave);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.DateToLeave = Convert.ToDateTime(DateToLeave, new CultureInfo("vi-VN"));
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày rời NPP [" + DateToLeave + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.DateToLeave = null;

                                        var DateToLoadEnd = excelInput["DateToLoadEnd"];
                                        if (!string.IsNullOrEmpty(DateToLoadEnd) && obj.DateToLeave != null)
                                        {
                                            DateTime temp = new DateTime();
                                            TimeSpan time;
                                            if (!TimeSpan.TryParse(DateToLoadEnd, out time))
                                            {
                                                lstError.Add("Thời gian ra máng [" + DateToLoadEnd + "] không chính xác");
                                            }
                                            else
                                            {
                                                temp = obj.DateToLeave.Value.Date + time;
                                                obj.DateToLoadEnd = temp;
                                            }
                                        }
                                        else obj.DateToLoadEnd = null;

                                        //obj.InvoiceBy = excelInput["InvoiceBy"];

                                        var InvoiceDate = excelInput["InvoiceDate"];
                                        if (!string.IsNullOrEmpty(InvoiceDate))
                                        {
                                            try
                                            {
                                                obj.InvoiceDate = ExcelHelper.ValueToDate(InvoiceDate);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.InvoiceDate = Convert.ToDateTime(InvoiceDate, new CultureInfo("vi-VN"));
                                                }
                                                catch
                                                {
                                                    lstError.Add("InvoiceDate [" + InvoiceDate + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.InvoiceDate = null;

                                        var InvoiceReturnDate = excelInput["InvoiceReturnDate"];
                                        if (!string.IsNullOrEmpty(InvoiceReturnDate))
                                        {
                                            try
                                            {
                                                obj.InvoiceReturnDate = ExcelHelper.ValueToDate(InvoiceReturnDate);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.InvoiceReturnDate = Convert.ToDateTime(InvoiceReturnDate, new CultureInfo("vi-VN"));
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày chứng từ trả về [" + InvoiceReturnDate + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.InvoiceReturnDate = null;

                                        obj.InvoiceNote = excelInput["InvoiceNote"];
                                        obj.Note = excelInput["Note"];
                                        obj.OPSGroupNote1 = excelInput["OPSGroupNote1"];
                                        obj.OPSGroupNote2 = excelInput["OPSGroupNote2"];
                                        obj.ORDGroupNote1 = excelInput["ORDGroupNote1"];
                                        obj.ORDGroupNote2 = excelInput["ORDGroupNote2"];
                                        obj.TOMasterNote1 = excelInput["TOMasterNote1"];
                                        obj.TOMasterNote2 = excelInput["TOMasterNote2"];
                                        obj.ChipNo = excelInput["ChipNo"];
                                        obj.Temperature = excelInput["Temperature"];

                                        var Ton = excelInput["Ton"];
                                        var CBM = excelInput["CBM"];
                                        var Quantity = excelInput["Quantity"];

                                        //if (string.IsNullOrEmpty(Ton) && string.IsNullOrEmpty(CBM) && string.IsNullOrEmpty(Quantity))
                                        //{
                                        //    lstError.Add("Tấn, Khối, Số lượng phải có ít nhất 1 cột không được trống");
                                        //}

                                        if (!string.IsNullOrEmpty(Ton))
                                        {
                                            try
                                            {
                                                obj.Ton = Convert.ToDouble(Ton);
                                            }
                                            catch
                                            {
                                                lstError.Add("Tấn [" + Ton + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.Ton > 0)
                                                obj.Ton = 0;
                                        }


                                        if (!string.IsNullOrEmpty(CBM))
                                        {
                                            try
                                            {
                                                obj.CBM = Convert.ToDouble(CBM);
                                            }
                                            catch
                                            {
                                                lstError.Add("Khối [" + CBM + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.CBM > 0)
                                                obj.CBM = 0;
                                        }

                                        if (!string.IsNullOrEmpty(Quantity))
                                        {
                                            try
                                            {
                                                obj.Quantity = Convert.ToDouble(Quantity);
                                            }
                                            catch
                                            {
                                                lstError.Add("Số lượng [" + Quantity + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.Quantity > 0)
                                                obj.Quantity = 0;
                                        }

                                        var TonTranfer = excelInput["TonTranfer"];
                                        var CBMTranfer = excelInput["CBMTranfer"];
                                        var QuantityTranfer = excelInput["QuantityTranfer"];

                                        //if (string.IsNullOrEmpty(TonTranfer) && string.IsNullOrEmpty(CBMTranfer) && string.IsNullOrEmpty(QuantityTranfer))
                                        //{
                                        //    lstError.Add("Tấn lấy, Khối lấy, Số lượng lấy phải có ít nhất 1 cột không được trống");
                                        //}

                                        if (!string.IsNullOrEmpty(TonTranfer))
                                        {
                                            try
                                            {
                                                obj.TonTranfer = Convert.ToDouble(TonTranfer);
                                            }
                                            catch
                                            {
                                                lstError.Add("Tấn lấy [" + TonTranfer + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.TonTranfer > 0)
                                                obj.TonTranfer = 0;
                                        }


                                        if (!string.IsNullOrEmpty(CBMTranfer))
                                        {
                                            try
                                            {
                                                obj.CBMTranfer = Convert.ToDouble(CBMTranfer);
                                            }
                                            catch
                                            {
                                                lstError.Add("Khối lấy [" + CBMTranfer + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.CBMTranfer > 0)
                                                obj.CBMTranfer = 0;
                                        }

                                        if (!string.IsNullOrEmpty(QuantityTranfer))
                                        {
                                            try
                                            {
                                                obj.QuantityTranfer = Convert.ToDouble(QuantityTranfer);
                                            }
                                            catch
                                            {
                                                lstError.Add("Số lượng lấy [" + QuantityTranfer + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.QuantityTranfer > 0)
                                                obj.QuantityTranfer = 0;
                                        }

                                        if (obj.TonTranfer < 0 || obj.CBMTranfer < 0 || obj.QuantityTranfer < 0)
                                        {
                                            lstError.Add("Tấn lấy, Khối lấy, Số lượng lấy phải lớn hơn 0");
                                        }


                                        var TonReturn = excelInput["TonReturn"];
                                        var CBMReturn = excelInput["CBMReturn"];
                                        var QuantityReturn = excelInput["QuantityReturn"];
                                        if (!string.IsNullOrEmpty(TonReturn))
                                        {
                                            try
                                            {
                                                obj.TonReturn = Convert.ToDouble(TonReturn);
                                            }
                                            catch
                                            {
                                                lstError.Add("Tấn trả về [" + TonReturn + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.TonReturn > 0)
                                                obj.TonReturn = 0;
                                        }


                                        if (!string.IsNullOrEmpty(CBMReturn))
                                        {
                                            try
                                            {
                                                obj.CBMReturn = Convert.ToDouble(CBMReturn);
                                            }
                                            catch
                                            {
                                                lstError.Add("Khối trả về [" + CBMReturn + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.CBMReturn > 0)
                                                obj.CBMReturn = 0;
                                        }

                                        if (!string.IsNullOrEmpty(QuantityReturn))
                                        {
                                            try
                                            {
                                                obj.QuantityReturn = Convert.ToDouble(QuantityReturn);
                                            }
                                            catch
                                            {
                                                lstError.Add("Số lượng trả về [" + QuantityReturn + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.QuantityReturn > 0)
                                                obj.QuantityReturn = 0;
                                        }

                                        var TonBBGN = excelInput["TonBBGN"];
                                        var CBMBBGN = excelInput["CBMBBGN"];
                                        var QuantityBBGN = excelInput["QuantityBBGN"];

                                        if (!string.IsNullOrEmpty(TonBBGN))
                                        {
                                            try
                                            {
                                                obj.TonBBGN = Convert.ToDouble(TonBBGN);
                                            }
                                            catch
                                            {
                                                lstError.Add("Tấn giao [" + TonBBGN + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.TonBBGN > 0)
                                                obj.TonBBGN = 0;
                                        }

                                        if (objSetting.TonBBGN > 0 && objSetting.TonTranfer > 0 && obj.TonBBGN > obj.TonTranfer)
                                        {
                                            lstError.Add("Tấn giao phải bé hơn hoặc bằng Tấn lấy");
                                        }

                                        if (!string.IsNullOrEmpty(CBMBBGN))
                                        {
                                            try
                                            {
                                                obj.CBMBBGN = Convert.ToDouble(CBMBBGN);
                                            }
                                            catch
                                            {
                                                lstError.Add("Khối giao [" + CBMBBGN + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.CBMBBGN > 0)
                                                obj.CBMBBGN = 0;
                                        }

                                        if (objSetting.CBMBBGN > 0 && objSetting.CBMTranfer > 0 && obj.CBMBBGN > obj.CBMTranfer)
                                        {
                                            lstError.Add("Khối giao phải bé hơn hoặc bằng Khối lấy");
                                        }

                                        if (!string.IsNullOrEmpty(QuantityBBGN))
                                        {
                                            try
                                            {
                                                obj.QuantityBBGN = Convert.ToDouble(QuantityBBGN);
                                            }
                                            catch
                                            {
                                                lstError.Add("Số lượng giao [" + QuantityBBGN + "] không chính xác");
                                            }
                                        }
                                        else
                                        {
                                            if (objSetting.QuantityBBGN > 0)
                                                obj.QuantityBBGN = 0;
                                        }

                                        if (objSetting.QuantityBBGN > 0 && objSetting.QuantityTranfer > 0 && obj.QuantityBBGN > obj.QuantityTranfer)
                                        {
                                            lstError.Add("Số lượng giao phải bé hơn hoặc bằng Số lượng lấy");
                                        }

                                        var VENCodeLoad = excelInput["VENLoadCode"];
                                        if (!string.IsNullOrEmpty(VENCodeLoad))
                                        {
                                            var ven = lstVendor.FirstOrDefault(c => c.Code == VENCodeLoad);
                                            if (ven != null)
                                            {
                                                obj.VENLoadCode = VENCodeLoad;
                                                obj.VENLoadCodeID = ven.ID;
                                            }
                                            else
                                            {
                                                obj.VENLoadCodeID = -1;
                                                lstError.Add("Vendor bốc xếp lên không tồn tại.");
                                            }
                                        }
                                        else
                                        {
                                            obj.VENLoadCodeID = -1;
                                        }

                                        var VENCodeUnLoad = excelInput["VENUnLoadCode"];
                                        if (!string.IsNullOrEmpty(VENCodeUnLoad))
                                        {
                                            var ven = lstVendor.FirstOrDefault(c => c.Code == VENCodeUnLoad);
                                            if (ven != null)
                                            {
                                                obj.VENUnLoadCode = VENCodeUnLoad;
                                                obj.VENUnLoadCodeID = ven.ID;
                                            }
                                            else
                                            {
                                                obj.VENUnLoadCodeID = -1;
                                                lstError.Add("Vendor bốc xếp xuống không tồn tại.");
                                            }
                                        }
                                        else
                                        {
                                            obj.VENUnLoadCodeID = -1;
                                        }

                                        var DateDN = excelInput["DateDN"];
                                        if (!string.IsNullOrEmpty(DateDN))
                                        {
                                            try
                                            {
                                                obj.DateDN = ExcelHelper.ValueToDate(DateDN);
                                            }
                                            catch
                                            {
                                                try
                                                {
                                                    obj.DateDN = Convert.ToDateTime(DateDN, new CultureInfo("vi-VN"));
                                                }
                                                catch
                                                {
                                                    lstError.Add("Ngày DN [" + DateDN + "] không chính xác");
                                                }

                                            }
                                        }
                                        else obj.DateDN = null;
                                        #endregion

                                        List<DTOMONExtImport> lstDetail = null;
                                        lstDetail = MONExtGetDetail_List(excelInput, data, objSetting, ref lstError, obj, ref failCurrent);
                                        if (failCurrent >= failMax)
                                        {
                                            break;
                                        }

                                        if (lstDetail == null || lstDetail.Count == 0)
                                        {
                                            lstError.Add("Không tìm thấy dữ liệu");
                                        }
                                        else
                                        {
                                            var lstID = lstDetail.Select(c => new DTOMONExtImportID
                                            {
                                                ID = c.ID,
                                                Value = c.ID.ToString(),
                                            }).ToList();
                                            obj.ListID = lstID;
                                            obj.ID = lstDetail[0].ID;
                                            obj.ListDetail = lstDetail;

                                            obj.IsDuplicate = false;
                                            if (dataRes != null && dataRes.Count > 0)
                                            {
                                                if (obj.ListDetail.Count == 1 && dataRes.Count(c => c.ID == obj.ID && c.ListDetail.Count == 1) > 0)
                                                {
                                                    obj.IsDuplicate = true;
                                                    var lst = dataRes.Where(c => c.ID == obj.ID && c.ListDetail.Count == 1).ToList();
                                                    foreach (var item in lst)
                                                    {
                                                        item.IsDuplicate = true;
                                                    }
                                                }
                                            }
                                        }

                                        lstError.Distinct();
                                        obj.ExcelError = string.Join(", ", lstError);
                                        if (!string.IsNullOrEmpty(obj.ExcelError))
                                            obj.ExcelSuccess = false;
                                        else
                                        {
                                            obj.ExcelSuccess = true;
                                        }
                                        dataRes.Add(obj);
                                    }
                                }
                            }
                        }
                    }
                }
                return dataRes;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private List<DTOMONExtImport> MONExtGetDetail_List(Dictionary<string, string> excelInput, List<DTOMONExtImport> data, DTOCUSSettingMONExt objSetting, ref List<string> lstError, DTOMONExtImport obj, ref int failCurrent)
        {
            bool hasKey = false;
            #region Data
            var ID = excelInput["ID"];
            var DNCode = excelInput["DNCode"];
            var SOCode = excelInput["SOCode"];
            var DateDN = excelInput["DateDN"];
            var OrderCode = excelInput["OrderCode"];
            var ETARequest = excelInput["ETARequest"];
            var MasterETDDate = excelInput["MasterETDDate"];
            var MasterETDDatetime = excelInput["MasterETDDatetime"];
            var OrderGroupETDDate = excelInput["OrderGroupETDDate"];
            var OrderGroupETDDatetime = excelInput["OrderGroupETDDatetime"];
            var CustomerCode = excelInput["CustomerCode"];
            var CustomerName = excelInput["CustomerName"];
            var CreatedDate = excelInput["CreatedDate"];
            var MasterCode = excelInput["MasterCode"];
            var DriverName = excelInput["DriverName"];
            var DriverTel = excelInput["DriverTel"];
            var DriverCard = excelInput["DriverCard"];
            var RegNo = excelInput["RegNo"];
            var RequestDate = excelInput["RequestDate"];
            var LocationFromCode = excelInput["LocationFromCode"];
            var LocationToCode = excelInput["LocationToCode"];
            var LocationToName = excelInput["LocationToName"];
            var LocationToAddress = excelInput["LocationToAddress"];
            var LocationToProvince = excelInput["LocationToProvince"];
            var LocationToDistrict = excelInput["LocationToDistrict"];
            var DistributorCode = excelInput["DistributorCode"];
            var DistributorName = excelInput["DistributorName"];
            var DistributorCodeName = excelInput["DistributorCodeName"];
            var IsInvoice = excelInput["IsInvoice"];
            var DateFromCome = excelInput["DateFromCome"];
            var DateFromLeave = excelInput["DateFromLeave"];
            var DateFromLoadStart = excelInput["DateFromLoadStart"];
            var DateFromLoadEnd = excelInput["DateFromLoadEnd"];
            var DateToCome = excelInput["DateToCome"];
            var DateToLeave = excelInput["DateToLeave"];
            var DateToLoadStart = excelInput["DateToLoadStart"];
            var DateToLoadEnd = excelInput["DateToLoadEnd"];
            var EconomicZone = excelInput["EconomicZone"];
            var DITOGroupProductStatusPODName = excelInput["DITOGroupProductStatusPODName"];
            var IsOrigin = excelInput["IsOrigin"];
            var InvoiceBy = excelInput["InvoiceBy"];
            var InvoiceDate = excelInput["InvoiceDate"];
            var InvoiceNote = excelInput["InvoiceNote"];
            var Note = excelInput["Note"];
            var OPSGroupNote1 = excelInput["OPSGroupNote1"];
            var OPSGroupNote2 = excelInput["OPSGroupNote2"];
            var ORDGroupNote1 = excelInput["ORDGroupNote1"];
            var ORDGroupNote2 = excelInput["ORDGroupNote2"];
            var TOMasterNote1 = excelInput["TOMasterNote1"];
            var TOMasterNote2 = excelInput["TOMasterNote2"];
            var VendorName = excelInput["VendorName"];
            var VendorCode = excelInput["VendorCode"];
            var Description = excelInput["Description"];
            var GroupOfProductCode = excelInput["GroupOfProductCode"];
            var GroupOfProductName = excelInput["GroupOfProductName"];
            var ChipNo = excelInput["ChipNo"];
            var Temperature = excelInput["Temperature"];
            var Ton = excelInput["Ton"];
            var CBM = excelInput["CBM"];
            var Quantity = excelInput["Quantity"];
            var TonTranfer = excelInput["TonTranfer"];
            var CBMTranfer = excelInput["CBMTranfer"];
            var QuantityTranfer = excelInput["QuantityTranfer"];
            var TonBBGN = excelInput["TonBBGN"];
            var CBMBBGN = excelInput["CBMBBGN"];
            var QuantityBBGN = excelInput["QuantityBBGN"];
            var Packing = excelInput["Packing"];
            var PackingName = excelInput["PackingName"];
            var VENLoadCode = excelInput["VENLoadCode"];
            var VENUnLoadCode = excelInput["VENUnLoadCode"];
            var TonReturn = excelInput["TonReturn"];
            var CBMReturn = excelInput["CBMReturn"];
            var QuantityReturn = excelInput["QuantityReturn"];
            var InvoiceReturnNote = excelInput["InvoiceReturnNote"];
            var InvoiceReturnDate = excelInput["InvoiceReturnDate"];
            var ReasonCancelNote = excelInput["ReasonCancelNote"];
            #endregion
            DateTime _date = new DateTime();
            List<DTOMONExtImport> temp = null;
            temp = data;

            if (objSetting.RegNoKey && objSetting.RegNo > 0)
            {
                if (string.IsNullOrEmpty(RegNo))
                {
                    lstError.Add("Số xe không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.RegNo == RegNo).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.MasterETDDateKey && objSetting.MasterETDDate > 0)
            {
                if (string.IsNullOrEmpty(MasterETDDate))
                {
                    lstError.Add("Ngày xuất kho không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(MasterETDDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(MasterETDDate, new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                            lstError.Add("Ngày xuất kho [" + MasterETDDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.MasterETDDate.HasValue && c.MasterETDDate.Value.Date == _date.Date).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.MasterETDDatetimeKey && objSetting.MasterETDDatetime > 0)
            {
                if (string.IsNullOrEmpty(MasterETDDatetime))
                {
                    lstError.Add("Ngày giờ xuất kho không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(MasterETDDatetime);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(MasterETDDatetime, new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                            lstError.Add("Ngày giờ xuất kho [" + MasterETDDatetime + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.MasterETDDatetime.HasValue && c.MasterETDDatetime.Value == _date).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.OrderGroupETDDateKey && objSetting.OrderGroupETDDate > 0)
            {
                if (string.IsNullOrEmpty(OrderGroupETDDate))
                {
                    lstError.Add("Ngày ETD chi tiết đơn không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(OrderGroupETDDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(OrderGroupETDDate, new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                            lstError.Add("Ngày ETD chi tiết đơn [" + OrderGroupETDDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.OrderGroupETDDate.HasValue && c.OrderGroupETDDate.Value.Date == _date.Date).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.OrderGroupETDDatetimeKey && objSetting.OrderGroupETDDatetime > 0)
            {
                if (string.IsNullOrEmpty(OrderGroupETDDatetime))
                {
                    lstError.Add("Ngày giờ ETD chi tiết đơn không được trống");
                }
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(OrderGroupETDDatetime);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(OrderGroupETDDatetime, new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                            lstError.Add("Ngày giờ ETD chi tiết đơn [" + OrderGroupETDDatetime + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.OrderGroupETDDatetime.HasValue && c.OrderGroupETDDatetime.Value == _date).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.OrderCodeKey && objSetting.OrderCode > 0)
            {
                if (string.IsNullOrEmpty(OrderCode))
                {
                    lstError.Add("Mã đơn hàng không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.OrderCode == OrderCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.LocationToCodeKey && objSetting.LocationToCode > 0)
            {
                if (string.IsNullOrEmpty(LocationToCode))
                {
                    lstError.Add("Mã Điểm giao không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.LocationToCode == LocationToCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DistributorCodeKey && objSetting.DistributorCode > 0)
            {
                if (string.IsNullOrEmpty(DistributorCode))
                {
                    lstError.Add("Mã NPP không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.DistributorCode == DistributorCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DistributorNameKey && objSetting.DistributorName > 0)
            {
                if (string.IsNullOrEmpty(DistributorName))
                {
                    lstError.Add("NPP không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.DistributorName == DistributorName).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DistributorCodeNameKey && objSetting.DistributorCodeName > 0)
            {
                if (string.IsNullOrEmpty(DistributorCodeName))
                {
                    lstError.Add("Mã tên NPP không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.DistributorCodeName == DistributorCodeName).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.PackingKey && objSetting.Packing > 0)
            {
                if (string.IsNullOrEmpty(Packing))
                {
                    lstError.Add("Hàng hóa/Đơn vị tính không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.Packing == Packing).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.PackingNameKey && objSetting.PackingName > 0)
            {
                if (string.IsNullOrEmpty(PackingName))
                {
                    lstError.Add("Tên hàng hóa/Đơn vị tính không được trống");
                }
                else
                {
                    temp = temp.Where(c => c.PackingName == PackingName).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DNCodeKey && objSetting.DNCode > 0)
            {
                if (string.IsNullOrEmpty(DNCode))
                    lstError.Add("Số DN không được trống");
                else
                {
                    temp = temp.Where(c => c.DNCode == DNCode).ToList();
                    hasKey = true;
                }
            }


            if (objSetting.SOCodeKey && objSetting.SOCode > 0)
            {
                if (string.IsNullOrEmpty(SOCode))
                    lstError.Add("Số SO không được trống");
                else
                {
                    temp = temp.Where(c => c.SOCode == SOCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.ETARequestKey && objSetting.ETARequest > 0)
            {
                if (string.IsNullOrEmpty(ETARequest))
                    lstError.Add("Ngày y/c giao hàng không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(ETARequest);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(ETARequest, new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                            lstError.Add("Ngày y/c giao hàng [" + ETARequest + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.ETARequest == _date).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.CustomerCodeKey && objSetting.CustomerCode > 0)
            {
                if (string.IsNullOrEmpty(CustomerCode))
                    lstError.Add("Mã Khách hàng không được trống");
                else
                {
                    temp = temp.Where(c => c.CustomerCode == CustomerCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.CustomerNameKey && objSetting.CustomerName > 0)
            {
                if (string.IsNullOrEmpty(CustomerName))
                    lstError.Add("Tên khách hàng không được trống");
                else
                {
                    temp = temp.Where(c => c.CustomerName == CustomerName).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.CreatedDateKey && objSetting.CreatedDate > 0)
            {
                if (string.IsNullOrEmpty(CreatedDate))
                    lstError.Add("Ngày tạo không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(CreatedDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(CreatedDate, new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                            lstError.Add("Ngày tạo [" + CreatedDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.CreatedDate == _date).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.MasterCodeKey && objSetting.MasterCode > 0)
            {
                if (string.IsNullOrEmpty(MasterCode))
                    lstError.Add("Mã chuyến không được trống");
                else
                {
                    temp = temp.Where(c => c.MasterCode == MasterCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DriverNameKey && objSetting.DriverName > 0)
            {
                if (string.IsNullOrEmpty(DriverName))
                    lstError.Add("Tài xế không được trống");
                else
                {
                    temp = temp.Where(c => c.DriverName == DriverName).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DriverTelKey && objSetting.DriverTel > 0)
            {
                if (string.IsNullOrEmpty(DriverTel))
                    lstError.Add("SĐT Tài xế không được trống");
                else
                {
                    temp = temp.Where(c => c.DriverTel == DriverTel).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DriverCardKey && objSetting.DriverCard > 0)
            {
                if (string.IsNullOrEmpty(DriverCard))
                    lstError.Add("CMND không được trống");
                else
                {
                    temp = temp.Where(c => c.DriverCard == DriverCard).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.RequestDateKey && objSetting.RequestDate > 0)
            {
                if (string.IsNullOrEmpty(RequestDate))
                    lstError.Add("Ngày gửi y/c không được trống");
                else
                {
                    _date = new DateTime();
                    try
                    {
                        _date = ExcelHelper.ValueToDate(RequestDate);
                    }
                    catch
                    {
                        try
                        {
                            _date = Convert.ToDateTime(RequestDate, new CultureInfo("vi-VN"));
                        }
                        catch
                        {
                            lstError.Add("ETD [" + RequestDate + "] không chính xác");
                        }

                    }
                    temp = temp.Where(c => c.RequestDate == _date).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.LocationFromCodeKey && objSetting.LocationFromCode > 0)
            {
                if (string.IsNullOrEmpty(LocationFromCode))
                    lstError.Add("Mã kho không được trống");
                else
                {
                    temp = temp.Where(c => c.LocationFromCode == LocationFromCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.LocationToNameKey && objSetting.LocationToName > 0)
            {
                if (string.IsNullOrEmpty(LocationToName))
                    lstError.Add("NPP không được trống");
                else
                {
                    temp = temp.Where(c => c.LocationToName == LocationToName).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.LocationToAddressKey && objSetting.LocationToAddress > 0)
            {
                if (string.IsNullOrEmpty(LocationToAddress))
                    lstError.Add("Địa chỉ không được trống");
                else
                {
                    temp = temp.Where(c => c.LocationToAddress == LocationToAddress).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.LocationToProvinceKey && objSetting.LocationToProvince > 0)
            {
                if (string.IsNullOrEmpty(LocationToProvince))
                    lstError.Add("Tỉnh không được trống");
                else
                {
                    temp = temp.Where(c => c.LocationToProvince == LocationToProvince).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.LocationToDistrictKey && objSetting.LocationToDistrict > 0)
            {
                if (string.IsNullOrEmpty(LocationToDistrict))
                    lstError.Add("Quận huyện không được trống");
                else
                {
                    temp = temp.Where(c => c.LocationToDistrict == LocationToDistrict).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.IsInvoiceKey && objSetting.IsInvoice > 0)
            {
                temp = temp.Where(c => c.IsInvoice == obj.IsInvoice).ToList();
                hasKey = true;
            }

            if (objSetting.DateFromComeKey && objSetting.DateFromCome > 0)
            {
                if (string.IsNullOrEmpty(DateFromCome))
                    lstError.Add("Ngày đến kho không được trống");
                else
                {
                    temp = temp.Where(c => c.DateFromCome == obj.DateFromCome).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DateFromLeaveKey && objSetting.DateFromLeave > 0)
            {
                if (string.IsNullOrEmpty(DateFromLeave))
                    lstError.Add("Ngày rời kho không được trống");
                else
                {
                    temp = temp.Where(c => c.DateFromLeave == obj.DateFromLeave).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DateFromLoadStartKey && objSetting.DateFromLoadStart > 0)
            {
                if (string.IsNullOrEmpty(DateFromLoadStart))
                    lstError.Add("Thời gian vào máng không được trống");
                else
                {
                    temp = temp.Where(c => c.DateFromLoadStart == obj.DateFromLoadStart).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DateFromLoadEndKey && objSetting.DateFromLoadEnd > 0)
            {
                if (string.IsNullOrEmpty(DateFromLoadEnd))
                    lstError.Add("Thời gian ra máng không được trống");
                else
                {
                    temp = temp.Where(c => c.DateFromLoadEnd == obj.DateFromLoadEnd).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DateToComeKey && objSetting.DateToCome > 0)
            {
                if (string.IsNullOrEmpty(DateToCome))
                    lstError.Add("Ngày đến NPP không được trống");
                else
                {
                    temp = temp.Where(c => c.DateToCome == c.DateToCome).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DateToLeaveKey && objSetting.DateToLeave > 0)
            {
                if (string.IsNullOrEmpty(DateToLeave))
                    lstError.Add("Ngày rời NPP không được trống");
                else
                {
                    temp = temp.Where(c => c.DateToLeave == obj.DateToLeave).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DateToLoadStartKey && objSetting.DateToLoadStart > 0)
            {
                if (string.IsNullOrEmpty(DateToLoadStart))
                    lstError.Add("Thời gian b.đầu dỡ hàng không được trống");
                else
                {
                    temp = temp.Where(c => c.DateToLoadStart == obj.DateToLoadStart).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DateToLoadEndKey && objSetting.DateToLoadEnd > 0)
            {
                if (string.IsNullOrEmpty(DateToLoadEnd))
                    lstError.Add("Thời gian k.thúc dỡ hàng không được trống");
                else
                {
                    temp = temp.Where(c => c.DateToLoadEnd == obj.DateToLoadEnd).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.InvoiceByKey && objSetting.InvoiceBy > 0)
            {
                if (string.IsNullOrEmpty(InvoiceBy))
                    lstError.Add("Người tạo chứng từ không được trống");
                else
                {
                    temp = temp.Where(c => c.InvoiceBy == InvoiceBy).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.InvoiceDateKey && objSetting.InvoiceDate > 0)
            {
                if (string.IsNullOrEmpty(InvoiceDate))
                    lstError.Add("Ngày tạo c/t không được trống");
                else
                {
                    temp = temp.Where(c => c.InvoiceDate == obj.InvoiceDate).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.InvoiceNoteKey && objSetting.InvoiceNote > 0)
            {
                if (string.IsNullOrEmpty(InvoiceNote))
                    lstError.Add("Ghi chú c/t không được trống");
                else
                {
                    temp = temp.Where(c => c.InvoiceNote == InvoiceNote).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.NoteKey && objSetting.Note > 0)
            {
                if (string.IsNullOrEmpty(Note))
                    lstError.Add("Ghi chú không được trống");
                else
                {
                    temp = temp.Where(c => c.Note == Note).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.OPSGroupNote1Key && objSetting.OPSGroupNote1 > 0)
            {
                if (string.IsNullOrEmpty(OPSGroupNote1))
                    lstError.Add("Ghi chú 1 không được trống");
                else
                {
                    temp = temp.Where(c => c.OPSGroupNote1 == OPSGroupNote1).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.OPSGroupNote2Key && objSetting.OPSGroupNote2 > 0)
            {
                if (string.IsNullOrEmpty(OPSGroupNote2))
                    lstError.Add("Ghi chú 2 không được trống");
                else
                {
                    temp = temp.Where(c => c.OPSGroupNote2 == OPSGroupNote2).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.ORDGroupNote1Key && objSetting.ORDGroupNote1 > 0)
            {
                if (string.IsNullOrEmpty(ORDGroupNote1))
                    lstError.Add("Ghi chú Đ/h 1 không được trống");
                else
                {
                    temp = temp.Where(c => c.ORDGroupNote1 == ORDGroupNote1).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.ORDGroupNote2Key && objSetting.ORDGroupNote2 > 0)
            {
                if (string.IsNullOrEmpty(ORDGroupNote2))
                    lstError.Add("Ghi chú Đ/h 2 không được trống");
                else
                {
                    temp = temp.Where(c => c.ORDGroupNote2 == ORDGroupNote2).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.TOMasterNote1Key && objSetting.TOMasterNote1 > 0)
            {
                if (string.IsNullOrEmpty(TOMasterNote1))
                    lstError.Add("Ghi chú chuyến 1 không được trống");
                else
                {
                    temp = temp.Where(c => c.TOMasterNote1 == TOMasterNote1).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.TOMasterNote2Key && objSetting.TOMasterNote2 > 0)
            {
                if (string.IsNullOrEmpty(TOMasterNote2))
                    lstError.Add("Ghi chú chuyến 2 không được trống");
                else
                {
                    temp = temp.Where(c => c.TOMasterNote2 == TOMasterNote2).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.VendorNameKey && objSetting.VendorName > 0)
            {
                if (string.IsNullOrEmpty(VendorName))
                    lstError.Add("Nhà vận tải không được trống");
                else
                {
                    temp = temp.Where(c => c.VendorName == VendorName).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.VendorCodeKey && objSetting.VendorCode > 0)
            {
                if (string.IsNullOrEmpty(VendorCode))
                    lstError.Add("Mã nhà vận tải không được trống");
                else
                {
                    temp = temp.Where(c => c.VendorCode == VendorCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DescriptionKey && objSetting.Description > 0)
            {
                if (string.IsNullOrEmpty(Description))
                    lstError.Add("Mô tả không được trống");
                else
                {
                    temp = temp.Where(c => c.Description == Description).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.GroupOfProductCodeKey && objSetting.GroupOfProductCode > 0)
            {
                if (string.IsNullOrEmpty(GroupOfProductCode))
                    lstError.Add("Mã nhóm sản phẩm không được trống");
                else
                {
                    temp = temp.Where(c => c.GroupOfProductCode == GroupOfProductCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.GroupOfProductNameKey && objSetting.GroupOfProductName > 0)
            {
                if (string.IsNullOrEmpty(GroupOfProductName))
                    lstError.Add("Nhóm sản phẩm không được trống");
                else
                {
                    temp = temp.Where(c => c.GroupOfProductName == GroupOfProductName).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.ChipNoKey && objSetting.ChipNo > 0)
            {
                if (string.IsNullOrEmpty(ChipNo))
                    lstError.Add("ChipNo không được trống");
                else
                {
                    temp = temp.Where(c => c.ChipNo == ChipNo).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.TemperatureKey && objSetting.Temperature > 0)
            {
                if (string.IsNullOrEmpty(Temperature))
                    lstError.Add("Temperature không được trống");
                else
                {
                    temp = temp.Where(c => c.Temperature == Temperature).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.TonKey && objSetting.Ton > 0)
            {
                if (string.IsNullOrEmpty(Ton))
                    lstError.Add("Số tấn kế hoạch không được trống");
                else
                {
                    temp = temp.Where(c => c.Ton == obj.Ton).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.CBMKey && objSetting.CBM > 0)
            {
                if (string.IsNullOrEmpty(CBM))
                    lstError.Add("Số khối kế hoạch không được trống");
                else
                {
                    temp = temp.Where(c => c.CBM == obj.CBM).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.QuantityKey && objSetting.Quantity > 0)
            {
                if (string.IsNullOrEmpty(Quantity))
                    lstError.Add("Số lượng kế hoạch không được trống");
                else
                {
                    temp = temp.Where(c => c.Quantity == obj.Quantity).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.TonTranferKey && objSetting.TonTranfer > 0)
            {
                if (string.IsNullOrEmpty(TonTranfer))
                    lstError.Add("Tấn lấy không được trống");
                else
                {
                    temp = temp.Where(c => c.TonTranfer == obj.TonTranfer).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.CBMTranferKey && objSetting.CBMTranfer > 0)
            {
                if (string.IsNullOrEmpty(CBMTranfer))
                    lstError.Add("Khối lấy không được trống");
                else
                {
                    temp = temp.Where(c => c.CBMTranfer == obj.CBMTranfer).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.QuantityTranferKey && objSetting.QuantityTranfer > 0)
            {
                if (string.IsNullOrEmpty(QuantityTranfer))
                    lstError.Add("Số lượng lấy không được trống");
                else
                {
                    temp = temp.Where(c => c.QuantityTranfer == obj.QuantityTranfer).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.TonBBGNKey && objSetting.TonBBGN > 0)
            {
                if (string.IsNullOrEmpty(TonBBGN))
                    lstError.Add("Tấn giao không được trống");
                else
                {
                    temp = temp.Where(c => c.TonBBGN == obj.TonBBGN).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.CBMBBGNKey && objSetting.CBMBBGN > 0)
            {
                if (string.IsNullOrEmpty(CBMBBGN))
                    lstError.Add("Khối giao không được trống");
                else
                {
                    temp = temp.Where(c => c.CBMBBGN == obj.CBMBBGN).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.QuantityBBGNKey && objSetting.QuantityBBGN > 0)
            {
                if (string.IsNullOrEmpty(QuantityBBGN))
                    lstError.Add("Số lượng giao không được trống");
                else
                {
                    temp = temp.Where(c => c.QuantityBBGN == obj.QuantityBBGN).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.VENLoadCodeKey && objSetting.VENLoadCode > 0)
            {
                if (string.IsNullOrEmpty(VENLoadCode))
                    lstError.Add("Vendor bốc xếp lên không được trống");
                else
                {
                    temp = temp.Where(c => c.VENLoadCode == VENLoadCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.VENUnLoadCodeKey && objSetting.VENUnLoadCode > 0)
            {
                if (string.IsNullOrEmpty(VENUnLoadCode))
                    lstError.Add("Vendor bốc xếp xuống không được trống");
                else
                {
                    temp = temp.Where(c => c.VENUnLoadCode == VENUnLoadCode).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.TonReturnKey && objSetting.TonReturn > 0)
            {
                if (string.IsNullOrEmpty(TonReturn))
                    lstError.Add("Tấn trả về không được trống");
                else
                {
                    temp = temp.Where(c => c.TonReturn == obj.TonReturn).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.CBMReturnKey && objSetting.CBMReturn > 0)
            {
                if (string.IsNullOrEmpty(CBMReturn))
                    lstError.Add("Khối trả về không được trống");
                else
                {
                    temp = temp.Where(c => c.CBMReturn == obj.CBMReturn).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.QuantityReturnKey && objSetting.QuantityReturn > 0)
            {
                if (string.IsNullOrEmpty(QuantityReturn))
                    lstError.Add("Số lượng trả về không được trống");
                else
                {
                    temp = temp.Where(c => c.QuantityReturn == obj.QuantityReturn).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.InvoiceReturnNoteKey && objSetting.InvoiceReturnNote > 0)
            {
                if (string.IsNullOrEmpty(InvoiceReturnNote))
                    lstError.Add("Ghi chú chứng từ không được trống");
                else
                {
                    temp = temp.Where(c => c.InvoiceReturnNote == InvoiceReturnNote).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.InvoiceReturnDateKey && objSetting.InvoiceReturnDate > 0)
            {
                if (string.IsNullOrEmpty(InvoiceReturnDate))
                    lstError.Add("Ngày chứng từ trả về không được trống");
                else
                {
                    temp = temp.Where(c => c.InvoiceReturnDate == obj.InvoiceReturnDate).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.ReasonCancelNoteKey && objSetting.ReasonCancelNote > 0)
            {
                if (string.IsNullOrEmpty(ReasonCancelNote))
                    lstError.Add("Ghi chú lí do không được trống");
                else
                {
                    temp = temp.Where(c => c.ReasonCancelNote == ReasonCancelNote).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.DateDNKey && objSetting.DateDN > 0)
            {
                if (string.IsNullOrEmpty(DateDN))
                    lstError.Add("Ngày DN không được trống");
                else
                {
                    temp = temp.Where(c => c.DateDN == obj.DateDN).ToList();
                    hasKey = true;
                }
            }

            if (objSetting.IDKey && objSetting.ID > 0)
            {
                var _id = -1;
                if (string.IsNullOrEmpty(ID))
                    lstError.Add("ID không được trống");
                else
                {
                    try
                    {
                        _id = Convert.ToInt32(ID);
                    }
                    catch
                    {
                        lstError.Add("ID [" + _id + "] không chính xác");
                    }
                    temp = temp.Where(c => c.ID == _id).ToList();
                    hasKey = true;
                }
            }

            if (hasKey == false)
            {
                failCurrent++;
            }
            return temp;
        }
        private Dictionary<string, string> MONExtGetDataName()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            result.Add("ID", "Số thứ tự");
            result.Add("DNCode", "Số DN");
            result.Add("SOCode", "Số SO");
            result.Add("DateDN", "Ngày DN");
            result.Add("OrderCode", "Mã đơn hàng");
            result.Add("ETARequest", "Ngày y/c giao hàng");
            result.Add("MasterETDDate", "Ngày xuất kho");
            result.Add("MasterETDDatetime", "Ngày giờ xuất kho");
            result.Add("OrderGroupETDDate", "Ngày ETD chi tiết đơn");
            result.Add("OrderGroupETDDatetime", "Ngày giờ ETD chi tiết đơn");
            result.Add("CustomerCode", "Mã Khách hàng");
            result.Add("CustomerName", "Tên khách hàng");
            result.Add("CreatedDate", "Ngày tạo");
            result.Add("MasterCode", "Mã chuyến");
            result.Add("DriverName", "Tài xế");
            result.Add("DriverTel", "SĐT Tài xế");
            result.Add("DriverCard", "CMND");
            result.Add("RegNo", "Xe");
            result.Add("RequestDate", "Ngày gửi y/c");
            result.Add("LocationFromCode", "Mã kho");
            result.Add("LocationToCode", "Mã điểm giao");
            result.Add("LocationToName", "Điểm giao");
            result.Add("LocationToAddress", "Địa chỉ");
            result.Add("LocationToProvince", "Tỉnh");
            result.Add("LocationToDistrict", "Quận huyện");
            result.Add("DistributorCode", "Mã NPP");
            result.Add("DistributorName", "NPP");
            result.Add("DistributorCodeName", "Mã - Tên NPP");
            result.Add("IsInvoice", "Nhận chứng từ");
            result.Add("DateFromCome", "Ngày đến kho");
            result.Add("DateFromLeave", "Ngày rời kho");
            result.Add("DateFromLoadStart", "Thời gian vào máng");
            result.Add("DateFromLoadEnd", "Thời gian ra máng");
            result.Add("DateToCome", "Ngày đến NPP");
            result.Add("DateToLeave", "Ngày rời NPP");
            result.Add("DateToLoadStart", "Thời gian b.đầu dỡ hàng");
            result.Add("DateToLoadEnd", "Thời gian k.thúc dỡ hàng");
            result.Add("InvoiceBy", "Người nhận chứng từ");
            result.Add("InvoiceDate", "Ngày nhận c/t");
            result.Add("InvoiceNote", "Ghi chú c/t");
            result.Add("Note", "Ghi chú");
            result.Add("OPSGroupNote1", "Ghi chú 1");
            result.Add("OPSGroupNote2", "Ghi chú 2");
            result.Add("ORDGroupNote1", "Ghi chú Đ/h 1");
            result.Add("ORDGroupNote2", "Ghi chú Đ/h 2");
            result.Add("TOMasterNote1", "Ghi chú chuyến 1");
            result.Add("TOMasterNote2", "Ghi chú chuyến 2");
            result.Add("VendorName", "Nhà vận tải");
            result.Add("VendorCode", "Mã nhà vận tải");
            result.Add("Description", "Description");
            result.Add("GroupOfProductCode", "Mã nhóm sản phẩm");
            result.Add("GroupOfProductName", "Nhóm sản phẩm");
            result.Add("ChipNo", "ChipNo");
            result.Add("Temperature", "Temperature");
            result.Add("Ton", "Số tấn/kg kế hoạch");
            result.Add("CBM", "Số khối kế hoạch");
            result.Add("Quantity", "Số lượng");
            result.Add("TonTranfer", "Tấn/kg lấy");
            result.Add("CBMTranfer", "Khối lấy");
            result.Add("QuantityTranfer", "Số lượng lấy");
            result.Add("TonBBGN", "Tấn/kg giao");
            result.Add("CBMBBGN", "Khối giao");
            result.Add("QuantityBBGN", "Số lượng giao");
            result.Add("Packing", "Mã hàng hóa/Đơn vị tính");
            result.Add("PackingName", "Tên hàng hóa/Đơn vị tính");
            result.Add("VENLoadCode", "Vendor bốc xếp lên");
            result.Add("VENUnLoadCode", "Vendor bốc xếp xuống");
            result.Add("TonReturn", "Tấn/kg trả về");
            result.Add("CBMReturn", "Khối trả về");
            result.Add("QuantityReturn", "Số lượng trả về");
            result.Add("InvoiceReturnNote", "Số chứng từ trả về");
            result.Add("InvoiceReturnDate", "Ngày chứng từ trả về");
            result.Add("ReasonCancelNote", "Ghi chú lí do");

            return result;
        }

        private Dictionary<string, string> MONExtGetDataValue(ExcelWorksheet ws, object obj, int row, List<string> sValue)
        {
            Dictionary<string, string> result = new Dictionary<string, string>();
            foreach (var prop in obj.GetType().GetProperties())
            {
                try
                {
                    var p = prop.Name;
                    if (!sValue.Contains(p))
                    {
                        var v = (int)prop.GetValue(obj, null);
                        result.Add(p, v > 0 ? ExcelHelper.GetValue(ws, row, v) : string.Empty);
                    }
                }
                catch (Exception)
                {
                }
            }
            return result;
        }
        #endregion

        #region Input

        #region DI FLM fee(chi phí xe tải nhà)
        public DTOResult MONInput_DIFLMFee_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_DIFLMFee_List(request, dtFrom, dtTo);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMONFLMDIInputDriver MONInput_DIFLMFee_GetDrivers(dynamic dynParam)
        {
            try
            {
                int DITOMasterID = (int)dynParam.DITOMasterID;
                DTOMONFLMDIInputDriver result = default(DTOMONFLMDIInputDriver);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_DIFLMFee_GetDrivers(DITOMasterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_DIFLMFee_SaveDrivers(dynamic dynParam)
        {
            try
            {
                int DITOMasterID = (int)dynParam.DITOMasterID;
                DTOMONFLMDIInputDriver item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONFLMDIInputDriver>(dynParam.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_DIFLMFee_SaveDrivers(item, DITOMasterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult MONInput_DIFLMFee_TroubleCostList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int DITOMasterID = (int)dynParam.DITOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_DIFLMFee_TroubleCostList(request, DITOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONInput_DIFLMFee_TroubleCostNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int DITOMasterID = (int)dynParam.DITOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_DIFLMFee_TroubleCostNotIn_List(request, DITOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_DIFLMFee_TroubleCostNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int DITOMasterID = (int)dynParam.DITOMasterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_DIFLMFee_TroubleCostNotIn_SaveList(lst, DITOMasterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_DIFLMFee_TroubleCost_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_DIFLMFee_TroubleCost_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONInput_DIFLMFee_StationCostList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int DITOMasterID = (int)dynParam.DITOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_DIFLMFee_StationCostList(request, DITOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_DIFLMFee_Save(dynamic dynParam)
        {
            try
            {
                DTOMONDIFLMFee item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONDIFLMFee>(dynParam.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_DIFLMFee_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_DIFLMFee_StationCostSave(dynamic dynParam)
        {
            try
            {
                List<DTOMONOPSDITOStation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONOPSDITOStation>>(dynParam.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_DIFLMFee_StationCostSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_DIFLMFee_TroubleCostSave(dynamic dynParam)
        {
            try
            {
                List<DTOMONCATTroubleCost> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONCATTroubleCost>>(dynParam.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_DIFLMFee_TroubleCostSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONInput_DIFLMFee_DriverList()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_DIFLMFee_DriverList();
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MONInput_DIFLMFee_Approved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int type = dynParam.type;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_DIFLMFee_Approved(lst, type);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region CO FLM fee(chi phí xe công nhà)
        public DTOResult MONInput_COFLMFee_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_COFLMFee_List(request, dtFrom, dtTo);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMONFLMCOInputDriver MONInput_COFLMFee_GetDrivers(dynamic dynParam)
        {
            try
            {
                int COTOMasterID = (int)dynParam.COTOMasterID;
                DTOMONFLMCOInputDriver result = default(DTOMONFLMCOInputDriver);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_COFLMFee_GetDrivers(COTOMasterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_COFLMFee_SaveDrivers(dynamic dynParam)
        {
            try
            {
                int COTOMasterID = (int)dynParam.COTOMasterID;
                DTOMONFLMCOInputDriver item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONFLMCOInputDriver>(dynParam.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_COFLMFee_SaveDrivers(item, COTOMasterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult MONInput_COFLMFee_TroubleCostList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int COTOMasterID = (int)dynParam.COTOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_COFLMFee_TroubleCostList(request, COTOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONInput_COFLMFee_TroubleCostNotIn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int COTOMasterID = (int)dynParam.COTOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_COFLMFee_TroubleCostNotIn_List(request, COTOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_COFLMFee_TroubleCostNotIn_SaveList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                int COTOMasterID = (int)dynParam.COTOMasterID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_COFLMFee_TroubleCostNotIn_SaveList(lst, COTOMasterID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_COFLMFee_TroubleCost_DeleteList(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_COFLMFee_TroubleCost_DeleteList(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONInput_COFLMFee_StationCostList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int COTOMasterID = (int)dynParam.COTOMasterID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_COFLMFee_StationCostList(request, COTOMasterID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_COFLMFee_Save(dynamic dynParam)
        {
            try
            {
                DTOMONCOFLMFee item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONCOFLMFee>(dynParam.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_COFLMFee_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_COFLMFee_StationCostSave(dynamic dynParam)
        {
            try
            {
                List<DTOMONOPSCOTOStation> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONOPSCOTOStation>>(dynParam.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_COFLMFee_StationCostSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MONInput_COFLMFee_TroubleCostSave(dynamic dynParam)
        {
            try
            {
                List<DTOMONCATTroubleCost> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONCATTroubleCost>>(dynParam.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_COFLMFee_TroubleCostSave(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOResult MONInput_COFLMFee_DriverList()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_COFLMFee_DriverList();
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void MONInput_COFLMFee_Approved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_COFLMFee_Approved(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region input production (nhap san luong nho)
        [HttpPost]
        public DTOResult MONInput_InputProduction_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                bool hasIsReturn = Convert.ToBoolean(dynParam.hasIsReturn);
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_InputProduction_List(request, dtFrom, dtTo, listCustomerID, hasIsReturn);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void MONInput_InputProduction_Save(dynamic dynParam)
        {
            try
            {
                DTOMONInputProduction item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONInputProduction>(dynParam.item.ToString());
                bool ispod = dynParam.ispod;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_InputProduction_Save(item, ispod);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult MONInput_InputProduction_Vendor_List(dynamic dynParam)
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_InputProduction_Vendor_List();
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void MONInput_InputProduction_ChangeComplete(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                bool isComplete = Convert.ToBoolean(dynParam.isComplete.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_InputProduction_ChangeComplete(lst, isComplete);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOMONInputProductionSplitDN MONInput_InputProduction_SplitDNGet(dynamic dynParam)
        {
            try
            {
                DTOMONInputProductionSplitDN result = new DTOMONInputProductionSplitDN();
                int DITOGroupProductID = (int)dynParam.DITOGroupProductID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_InputProduction_SplitDNGet(DITOGroupProductID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void MONInput_InputProduction_SplitDNSave(dynamic dynParam)
        {
            try
            {
                DTOMONInputProductionSplitDN item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONInputProductionSplitDN>(dynParam.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_InputProduction_SplitDNSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOMONInputProductionAddReturnData MONInput_InputProduction_AddReturnGet(dynamic dynParam)
        {
            try
            {
                DTOMONInputProductionAddReturnData result = new DTOMONInputProductionAddReturnData();
                int DITOGroupProductID = (int)dynParam.DITOGroupProductID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_InputProduction_AddReturnGet(DITOGroupProductID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void MONInput_InputProduction_AddReturnSave(dynamic dynParam)
        {
            try
            {
                DTOMONInputProductionAddReturn item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONInputProductionAddReturn>(dynParam.item.ToString());
                int DITOGroupProductID = (int)dynParam.DITOGroupProductID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_InputProduction_AddReturnSave(item, DITOGroupProductID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOMONInputProductionAddReturnData MONInput_InputProduction_AddReturn_EditGet(dynamic dynParam)
        {
            try
            {
                DTOMONInputProductionAddReturnData result = new DTOMONInputProductionAddReturnData();
                int DITOProductID = (int)dynParam.DITOProductID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONInput_InputProduction_AddReturn_EditGet(DITOProductID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void MONInput_InputProduction_AddReturnEditDelete(dynamic dynParam)
        {
            try
            {
                int DITOProductID = (int)dynParam.DITOProductID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONInput_InputProduction_AddReturnEditDelete(DITOProductID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region input container
        [HttpPost]
        public DTOResult MONCOInput_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                int type = dynParam.type;
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date.AddDays(1);
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONCOInput_List(request, dtFrom, dtTo, listCustomerID, type);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONCOInput_Save(dynamic dynParam)
        {
            try
            {
                DTOPODCOInput item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOPODCOInput>(dynParam.item.ToString());
                bool ispod = dynParam.ispod;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONCOInput_Save(item, ispod);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ExtReturn
        public DTOResult MONOPSExtReturn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_List(request, dtFrom, dtTo, listCustomerID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOMONOPSExtReturn MONOPSExtReturn_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOMONOPSExtReturn);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_Get(id);
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void MONOPSExtReturn_Delete(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONOPSExtReturn_Delete(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void MONOPSExtReturn_Approved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONOPSExtReturn_Approved(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void MONOPSExtReturn_UnApproved(dynamic dynParam)
        {
            try
            {
                List<int> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.lst.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONOPSExtReturn_UnApproved(lst);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult MONOPSExtReturn_CustomerList()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_CustomerList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATVehicle> MONOPSExtReturn_VehicleList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCATVehicle>();
                int vendorID = (int)dynParam.vendorID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_VehicleList(vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMDriver> MONOPSExtReturn_DriverList()
        {
            try
            {
                var result = new List<DTOFLMDriver>();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_DriverList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult MONOPSExtReturn_VendorList()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_VendorList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMONTOMaster> MONOPSExtReturn_DITOMasterList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOMONTOMaster>();
                int cusID = (int)dynParam.cusID;
                int vendorID = (int)dynParam.vendorID;
                int vehicleID = (int)dynParam.vehicleID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_DITOMasterList(cusID, vendorID, vehicleID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult MONOPSExtReturn_GOPByCus(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_GOPByCus(cusID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult MONOPSExtReturn_ProductByGOP(dynamic dynParam)
        {
            try
            {
                int gopID = (int)dynParam.gopID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_ProductByGOP(gopID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult MONOPSExtReturn_DetailList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int extReturnID = (int)dynParam.ExtReturnID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_DetailList(request, extReturnID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult MONOPSExtReturn_DetailNotIn(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int masterID = (int)dynParam.masterID;
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_DetailNotIn(request, masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int MONOPSExtReturn_Save(dynamic dynParam)
        {
            try
            {
                int result = -1;
                DTOMONOPSExtReturn item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONOPSExtReturn>(dynParam.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void MONOPSExtReturn_DetailSave(dynamic dynParam)
        {
            try
            {
                List<DTOMONOPSExtReturnDetail> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOMONOPSExtReturnDetail>>(dynParam.lst.ToString());
                int ExtReturnID = (int)dynParam.ExtReturnID;
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONOPSExtReturn_DetailSave(lst, ExtReturnID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult MONOPSExtReturn_FindList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_FindList(request);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOResult MONOPSExtReturn_QuickList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                var result = default(DTOResult);
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_QuickList(request, dtFrom, dtTo, listCustomerID);
                });

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void MONOPSExtReturn_QuickSave(dynamic dynParam)
        {
            try
            {
                DTOMONOPSDITOProductQuick item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONOPSDITOProductQuick>(dynParam.item.ToString());
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    sv.MONOPSExtReturn_QuickSave(item);
                });

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public DTOMONOPSExtReturnData MONOPSExtReturn_QuickData()
        {
            try
            {
                DTOMONOPSExtReturnData result = new DTOMONOPSExtReturnData();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_QuickData();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult MONOPSExtReturn_Setting_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = new DTOResult();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_Setting_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOCUSSettingExtReturn MONOPSExtReturn_Setting_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.templateID;
                var result = new DTOCUSSettingExtReturn();
                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_Setting_Get(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel MONOPSExtReturn_ExcelOnline_Init(dynamic dynParam)
        {
            try
            {
                var functionid = (int)dynParam.functionid;
                var functionkey = dynParam.functionkey.ToString();
                var templateID = (int)dynParam.templateID;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                var isreload = (bool)dynParam.isreload;

                var result = default(SYSExcel);

                ServiceFactory.SVMonitor((ISVMonitor sv) =>
                {
                    result = sv.MONOPSExtReturn_ExcelOnline_Init(templateID, dtFrom, dtTo, listCustomerID, functionid, functionkey, isreload);
                });
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                result.Data = "";
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public Row MONOPSExtReturn_ExcelOnline_Change(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var row = (int)dynParam.row;
                var templateID = (int)dynParam.templateID;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());
                List<Cell> cells = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Cell>>(dynParam.cells.ToString());

                var result = default(Row);
                if (id > 0 && cells.Count > 0 && row > 0)
                {
                    ServiceFactory.SVMonitor((ISVMonitor sv) =>
                    {
                        result = sv.MONOPSExtReturn_ExcelOnline_Change(templateID, dtFrom, dtTo, listCustomerID, id, row, cells, lstMessageError);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public SYSExcel MONOPSExtReturn_ExcelOnline_Import(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var templateID = (int)dynParam.templateID;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());
                List<Worksheet> lst = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(dynParam.worksheets.ToString());
                List<string> lstMessageError = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(dynParam.lstMessageError.ToString());

                var result = default(SYSExcel);
                if (id > 0 && lst.Count > 0 && lst[0].Rows.Count > 1)
                {
                    ServiceFactory.SVMonitor((ISVMonitor sv) =>
                    {
                        result = sv.MONOPSExtReturn_ExcelOnline_Import(templateID, dtFrom, dtTo, listCustomerID, id, lst[0].Rows, lstMessageError);
                    });
                }
                if (result != null && !string.IsNullOrEmpty(result.Data))
                {
                    result.Worksheets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Worksheet>>(result.Data);
                }
                else
                {
                    result = new SYSExcel();
                    result.Worksheets = new List<Worksheet>();
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool MONOPSExtReturn_ExcelOnline_Approve(dynamic dynParam)
        {
            try
            {
                var id = (long)dynParam.id;
                var templateID = (int)dynParam.templateID;
                DateTime dtFrom = Convert.ToDateTime(dynParam.dtFrom.ToString());
                DateTime dtTo = Convert.ToDateTime(dynParam.dtTo.ToString());
                List<int> listCustomerID = Newtonsoft.Json.JsonConvert.DeserializeObject<List<int>>(dynParam.listCustomerID.ToString());

                var result = false;
                if (id > 0)
                {
                    ServiceFactory.SVMonitor((ISVMonitor sv) =>
                    {
                        result = sv.MONOPSExtReturn_ExcelOnline_Approve(id, templateID, dtFrom, dtTo, listCustomerID);
                    });
                }
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion
    }
}