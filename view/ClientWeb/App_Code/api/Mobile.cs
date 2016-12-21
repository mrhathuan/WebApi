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
using System.ServiceModel;
using ServicesExtend;

namespace ClientWeb
{
    public class MobileController : BaseController
    {
        
        [HttpPost]
        public CATFile SaveImage()
        {
            try
            {
                HttpContext context = HttpContext.Current;

                var folderPath = "Uploads/pod/";
                var a = HttpContext.Current.Request.Params.Get("model");

                var result = new CATFile();
                HttpPostedFile file = context.Request.Files[0];
                string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                string filePath = folderPath + "_" + DateTime.Now.ToString("ddMMyyhhmmss") + fileExt;
                file.SaveAs(context.Server.MapPath("/" + filePath));
                result.FilePath = filePath;
                result.FileName = file.FileName;
                result.FileExt = fileExt;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public CATFile SaveImage1()
        {
            try
            {
                HttpContext context = HttpContext.Current;

                var folderPath = "Uploads/pod/";
                var a = HttpContext.Current.Request.Params.Get("model");

                var result = new CATFile();
                HttpPostedFile file = context.Request.Files["file"];
                string fileExt = file.FileName.Substring(file.FileName.LastIndexOf("."));
                string filePath = folderPath + "_" + DateTime.Now.ToString("ddMMyyhhmmss") + fileExt;
                file.SaveAs(context.Server.MapPath("/" + filePath));
                result.FilePath = filePath;
                result.FileName = file.FileName;
                result.FileExt = fileExt;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #region Extend
        [HttpPost]
        public void Extend_VehiclePosition_Add(dynamic dynParam)
        {
            try
            {
                string vehicleCode = dynParam.vehicleCode.ToString();
                float lat = (float)dynParam.lat;
                float lng = (float)dynParam.lng;
                DateTime deviceDate = Convert.ToDateTime(dynParam.deviceDate.ToString());

                ServiceFactory.SVOther((ISVOther sv) =>
                {
                    sv.VehiclePosition_Add(vehicleCode, lat, lng, "", deviceDate);
                });
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.FLMMobileDriver_UpdateGPS(vehicleCode, lat, lng);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

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

        
        #endregion

        #region time sheet
        [HttpPost]
        public DTOMobileFLMAsset GetAssetInfo(dynamic dynParam)
        {
            try
            {
                DTOMobileFLMAsset result = new DTOMobileFLMAsset();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileCurrentAsset_Get();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleRunning_List(dynamic dynParam)
        {
            try
            {

                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileScheduleRunning_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleOpen_List(dynamic dynParam)
        {
            try
            {

                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileScheduleOpen_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleAccept_List(dynamic dynParam)
        {
            try
            {

                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileScheduleAccept_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleGet_List(dynamic dynParam)
        {
            try
            {

                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileScheduleGet_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATReason> FLMMobileReason_List(dynamic dynParam)
        {
            try
            {
                List<DTOCATReason> result = new List<DTOCATReason>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileReason_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMDriverScheduleMobile> FLMMobileReject_List(dynamic dynParam)
        {
            try
            {
                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileReject_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobileGroupOfProduct> FLMMobileTOMaster_Get(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                List<DTOMobileGroupOfProduct> result = new List<DTOMobileGroupOfProduct>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileTOMaster_Get(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FLMMobileMaster_Reject(dynamic dynParam)
        {
            try
            {
                bool result = true;
                int timedriverID = (int)dynParam.timedriverID;
                int timesheetID = (int)dynParam.timesheetID;
                int reasonID = (int)dynParam.reasonID;
                string reasonNote = dynParam.reasonNote;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileMaster_Reject(timedriverID, reasonID, reasonNote);
                });
                new ClientHub().DITOMasterChangeStatus(timesheetID, 2, "");

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string FLMMobileMaster_Run(dynamic dynParam)
        {
            try
            {
                string result = "";
                int timeID = (int)dynParam.timeID;

                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileMaster_Run(timeID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FLMMobileMaster_Accept(dynamic dynParam)
        {
            try
            {
                bool result = true;
                int timedriverID = (int)dynParam.timedriverID;
                int timesheetID = (int)dynParam.timesheetID;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileMaster_Accept(timedriverID);
                });
                new ClientHub().DITOMasterChangeStatus(timesheetID, 4, "");

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FLMMobileMaster_ReAccept(dynamic dynParam)
        {
            try
            {
                bool result = true;
                int timesheetID = (int)dynParam.timesheetID;

                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileMaster_ReAccept(timesheetID);
                });
                string drivername = "";
                if (Account != null)
                    drivername = Account.LastName + " " + Account.FirstName;
                new ClientHub().DITOMasterChangeStatus(timesheetID, 3, drivername);

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public bool FLMMobileMaster_Complete(dynamic dynParam)
        {
            try
            {
                bool result = true;
                int timeID = (int)dynParam.timeID;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileMaster_Complete(timeID);
                });
                new ClientHub().DITOMasterChangeStatus(timeID, 5, "");

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMMobileStatus_Save(dynamic dynParam)
        {
            try
            {
                int timesheetID = (int)dynParam.timesheetID;
                int timedriverID = (int)dynParam.timedriverID;
                int masterID = (int)dynParam.masterID;
                int locationID = (int)dynParam.locationID;
                double? temp = dynParam.temp;
                double? lat = dynParam.Lat;
                double? lng = dynParam.Lng;
                List<DTO_MONDITO> result = new List<DTO_MONDITO>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileStatus_Save(timedriverID, masterID, locationID, temp,lat,lng);
                    if (result.Count > 0)
                    {
                        new ClientHub().DITOMasterChangeStatus(timesheetID, 5, "");

                        ServiceFactory.SVOther((ISVOther oth) =>
                        {
                            foreach (var obj in result)
                            {
                                if (obj.ATA.HasValue && obj.ATD.HasValue)
                                {
                                    var lstGPS = oth.VehiclePosition_Get(obj.RegNo, obj.ATD.Value, obj.ATA.Value);
                                    double km = 0;
                                    for (int i = 1; i < lstGPS.Count; i++)
                                    {
                                        km += GetDistance(lstGPS[i - 1].Lat, lstGPS[i - 1].Lng, lstGPS[i].Lat, lstGPS[i].Lng);
                                    }
                                    obj.KM = km;
                                }
                            }
                        });
                        sv.Mobile_UpdateDITO(masterID,result);
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMMobileStatus_Complete(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;                
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.FLMMobileStatus_Complete(masterID);
                });
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
        public void FLMMobileStatus_COSave(dynamic dynParam)
        {
            try
            {
                int timesheetID = (int)dynParam.timesheetID;
                int masterID = (int)dynParam.masterID;
                int locationID = (int)dynParam.locationID;
                int? romoocID = dynParam.romoocID;
                double? lat = dynParam.Lat;
                double? lng = dynParam.Lng;
                bool isComplete = false;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    isComplete = sv.FLMMobileStatus_COSave(masterID, locationID, romoocID,lat,lng);
                    if (isComplete)
                    {
                        new ClientHub().DITOMasterChangeStatus(timesheetID, 5, "");
                    }
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobileSODetail> FLMMobile_SOList(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                int locationID = (int)dynParam.locationID;
                List<DTOMobileSODetail> rs = new List<DTOMobileSODetail>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobile_SOList(masterID, locationID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobileSODetail> FLMMobile_COList(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                List<DTOMobileSODetail> rs = new List<DTOMobileSODetail>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobile_COList(masterID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobileSODetail> FLMMobile_SummarySOList(dynamic dynParam)
        {
            try
            {
                int timesheetID = (int)dynParam.timesheetID;
                List<DTOMobileSODetail> rs = new List<DTOMobileSODetail>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobile_SummarySOList(timesheetID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSVehicle> FLMMobile_RomoocList(dynamic dynParam)
        {
            try
            {
                List<DTOOPSVehicle> rs = new List<DTOOPSVehicle>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobile_RomoocList();
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOMobileLocationMaster FLMMobile_ListLocationOfCurrentMaster(dynamic dynParam)
        {
            try
            {

                DTOMobileLocationMaster rs = new DTOMobileLocationMaster();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobile_ListLocationOfCurrentMaster();
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOMobileLocationMaster FLMMobile_ListLocationOfMaster(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                DTOMobileLocationMaster rs = new DTOMobileLocationMaster();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobile_ListLocationOfMaster(masterID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOMobileLocationMaster FLMMobile_ListCOLocationOfMaster(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                DTOMobileLocationMaster rs = new DTOMobileLocationMaster();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobile_ListCOLocationOfMaster(masterID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CATFile> FLMMobileDriver_FileList(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                string code = dynParam.code;
                CATTypeOfFileCode catcode = CATTypeOfFileCode.Trouble;
                if (code == "dipod")
                    catcode = CATTypeOfFileCode.DIPOD;
                if (code == "copod")
                    catcode = CATTypeOfFileCode.COPOD;
                if (code == "TroubleCO")
                    catcode = CATTypeOfFileCode.TroubleCO;
                List<CATFile> result = new List<CATFile>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileDriver_FileList(catcode, id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOFLMDriverScheduleMobile> FLMMobileDriverHistory_List(dynamic dynParam)
        {
            try
            {

                DateTime dfrom = dynParam.dtfrom;
                DateTime dto = dynParam.dtto;
                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileDriverHistory_List(dfrom, dto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMMobileDriver_UpdateTemp(dynamic dynParam)
        {
            try
            {

                int timeSheetID = dynParam.timeSheetID;
                int locationID = dynParam.locationID;
                double temp = dynParam.temp;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.FLMMobileDriver_UpdateTemp(timeSheetID, locationID, temp);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOFLMDriverScheduleMobile FLMMobileSchedule_Get(dynamic dynParam)
        {
            try
            {

                int timeSheetDriverID = dynParam.timeSheetDriverID;
                var result = new DTOFLMDriverScheduleMobile();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileSchedule_Get(timeSheetDriverID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region trouble cost

        [HttpPost]
        public void FLMMobile_TroubleSave(dynamic dynParam)
        {
            try
            {
                DTOCATTrouble item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCATTrouble>(dynParam.item.ToString());
                DTOMobileLocationMaster rs = new DTOMobileLocationMaster();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.FLMMobile_TroubleSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CATGroupOfTrouble> FLMMobile_GroupTroubleList(dynamic dynParam)
        {
            try
            {
                bool isCo = dynParam.isCo;
                List<CATGroupOfTrouble> rs = new List<CATGroupOfTrouble>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobile_GroupTroubleList(isCo);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATTrouble> FLMMobileDriver_TroubleList(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                bool isCO = dynParam.isCO;
                List<DTOCATTrouble> result = new List<DTOCATTrouble>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileDriver_TroubleList(masterID,isCO);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region RouteProblem

        [HttpPost]
        public List<DTOTriggerMessage> MessageCall()
        {
            try
            {
                var result = new List<DTOTriggerMessage>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MessageCall();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSRouteProblem> ProblemList()
        {
            try
            {
                var result = new List<DTOOPSRouteProblem>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.ProblemList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOOPSRouteProblem ProblemGet(dynamic d)
        {
            try
            {
                int id = d.id;
                var result = new DTOOPSRouteProblem();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.ProblemGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region problem
        [HttpPost]
        public List<DTOMobileRouteProblem> FLMMobileDriver_ProblemList(dynamic dynParam)
        {
            try
            {
                List<DTOMobileRouteProblem> result = new List<DTOMobileRouteProblem>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileDriver_ProblemList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobileProblemType> FLMMobileDriver_ProblemTypeList(dynamic dynParam)
        {
            try
            {
                List<DTOMobileProblemType> result = new List<DTOMobileProblemType>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileDriver_ProblemTypeList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMMobileDriver_ProblemSave(dynamic dynParam)
        {
            try
            {
                DTOMobileRouteProblem item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMobileRouteProblem>(dynParam.item.ToString());
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.FLMMobileDriver_ProblemSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region salary

        [HttpPost]
        public List<DTOMobileSalary> FLMMobileDriverSalary_List(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = dynParam.dtfrom;
                DateTime dtto = dynParam.dtto;
                List<DTOMobileSalary> result = new List<DTOMobileSalary>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileDriverSalary_List(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region return

        [HttpPost]
        public List<DTOMobileOrderReturn> Mobile_GOPReturnList(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                int locationID = (int)dynParam.locationID;
                List<DTOMobileOrderReturn> result = new List<DTOMobileOrderReturn>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.Mobile_GOPReturnList(masterID, locationID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSGroupOfProduct> Mobiler_CUSGOPListNotin(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                int locationID = (int)dynParam.locationID;
                List<CUSGroupOfProduct> result = new List<CUSGroupOfProduct>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.Mobiler_CUSGOPListNotin(masterID, locationID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void Mobile_GOPReturnSave(dynamic dynParam)
        {
            try
            {
                DTOMONProductReturn item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMONProductReturn>(dynParam.item.ToString());
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.Mobile_GOPReturnSave(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void Mobile_GOPReturnEdit(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                int quantity = (int)dynParam.quantity;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.Mobile_GOPReturnEdit(id, quantity);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> Mobile_DITOGroupProductList(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                int locationID = (int)dynParam.locationID;
                List<DTOCombobox> result = new List<DTOCombobox>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.Mobile_DITOGroupProductList(masterID, locationID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCUSProduct> Mobile_CUSProductList(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                List<DTOCUSProduct> result = new List<DTOCUSProduct>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.Mobile_CUSProductList(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCombobox> Mobile_CUSGOPList(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                List<DTOCombobox> result = new List<DTOCombobox>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.Mobile_CUSGOPList(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region check in 

        [HttpPost]
        public List<DTOMobilePartnerLocation> FLMMobileDriver_PartnerList(dynamic dynParam)
        {
            try
            {
                int type = (int)dynParam.type;
                List<DTOMobilePartnerLocation> rs = new List<DTOMobilePartnerLocation>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobileDriver_PartnerList(type);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMMobileDriver_CheckInLocation(dynamic dynParam)
        {
            try
            {
                DTOMobilePartnerLocation item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMobilePartnerLocation>(dynParam.item.ToString());
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.FLMMobileDriver_CheckInLocation(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region station truck

        [HttpPost]
        public List<DTOMobile_Station> FLMMobileDriver_Stationlist(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                List<DTOMobile_Station> rs = new List<DTOMobile_Station>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobileDriver_Stationlist(masterID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobile_Station> FLMMobileDriver_StationPassed(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                List<DTOMobile_Station> rs = new List<DTOMobile_Station>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobileDriver_StationPassed(masterID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int FLMMobileDriver_StationPass(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                int stationID = (int)dynParam.stationID;
                int rs = 0;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs= sv.FLMMobileDriver_StationPass(masterID, stationID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region station container

        [HttpPost]
        public List<DTOMobile_Station> FLMMobileDriver_COStationlist(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                List<DTOMobile_Station> rs = new List<DTOMobile_Station>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobileDriver_COStationlist(masterID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobile_Station> FLMMobileDriver_COStationPassed(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                List<DTOMobile_Station> rs = new List<DTOMobile_Station>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobileDriver_COStationPassed(masterID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int FLMMobileDriver_COStationPass(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                int stationID = (int)dynParam.stationID;
                int rs = 0;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    rs = sv.FLMMobileDriver_COStationPass(masterID, stationID);
                });
                return rs;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #endregion

        #region SL SO
        public void Mobile_SL_Save(dynamic dynParam)
        {
            try
            {
                DTOMobileSODetail item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMobileSODetail>(dynParam.item.ToString());
                double? QuantityReturn = item.QuantityReturn;
                double? QuantityTranfer = item.QuantityTranfer;
                item.QuantityReturn = (double)QuantityReturn;
                item.QuantityTranfer = (double)QuantityTranfer;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.Mobile_SL_Save(item);
                });

            }
            catch(Exception e)
            {
                throw e;
            }
        }

        public void Mobile_AddGroupProductFromDN(dynamic dynParam)
        {
            try
            {
                int opsGroupID = (int)dynParam.opsGroupID;
                DTOMON_CUSProduct item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMON_CUSProduct>(dynParam.item.ToString());
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.Mobile_AddGroupProductFromDN(opsGroupID,item);
                });
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<DTOMON_CUSProduct> Mobile_GroupProductOfTOGroup(dynamic dynParam)
        {
            try
            {
                int TOGroupID = (int)dynParam.TOGroupID;
                List<DTOMON_CUSProduct> result = new List<DTOMON_CUSProduct>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.Mobile_GroupProductOfTOGroup(TOGroupID);
                });
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        #endregion SL SO

        #region Vendor
        [HttpPost]
        public DTOVendor FLMMobileVendor_Get(dynamic dynParam)
        {
            try
            {
                var result = new DTOVendor();
                //int vendorID = dynParam.vendorID;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_Get(1);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobileDIAppointmentRate> FLMMobileVendor_TenderRequestList(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                int vendorID = dynParam.vendorID;
                var result = new List<DTOMobileDIAppointmentRate>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_TenderRequestList(vendorID, dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobileDIAppointmentRate> FLMMobileVendor_TenderAcceptList(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                int vendorID = dynParam.vendorID;
                var result = new List<DTOMobileDIAppointmentRate>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_TenderAcceptList(vendorID, dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobileDIAppointmentRate> FLMMobileVendor_TenderRejectList(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<DTOMobileDIAppointmentRate>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_TenderRejectList(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMMobileVendor_TenderApproved(dynamic dynParam)
        {
            try
            {
                int id = dynParam.id;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.FLMMobileVendor_TenderApproved(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMailVendor> FLMMobileVendor_TenderReject(dynamic dynParam)
        {
            try
            {
                int id = dynParam.id;
                DTODIAppointmentRouteTenderReject item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTODIAppointmentRouteTenderReject>(dynParam.item.ToString());
                var result = new List<DTOMailVendor>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_TenderReject(id, item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOCATReason> FLMMobileVendor_ReasonList(dynamic dynParam)
        {
            try
            {
                List<DTOCATReason> result = new List<DTOCATReason>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_ReasonList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOMobileDITOGroupProduct> FLMMobileVendor_MasterDetail(dynamic dynParam)
        {
            try
            {
                int id = dynParam.id;
                List<DTOMobileDITOGroupProduct> result = new List<DTOMobileDITOGroupProduct>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_MasterDetail(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMMobileVendor_TenderSave(dynamic dynParam)
        {
            try
            {
                DTOMobileDIAppointmentRate Vehicle = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOMobileDIAppointmentRate>(dynParam.item.ToString());
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.FLMMobileVendor_TenderSave(Vehicle);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<CUSCustomer> FLMMobileVendor_ListVendor(dynamic dynParam)
        {
            try
            {
                List<CUSCustomer> result = new List<CUSCustomer>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_ListVendor();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<DTOOPSVehicle> FLMMobileVendor_VehicleListVehicle(dynamic dynParam)
        {
            try
            {
                List<DTOOPSVehicle> result = new List<DTOOPSVehicle>();
                int vendorID = dynParam.vendorID;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_VehicleListVehicle(vendorID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<FLMDriver> FLMMobileVendor_ListDriver(dynamic dynParam)
        {
            try
            {
                List<FLMDriver> result = new List<FLMDriver>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_ListDriver();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOMobileDIAppointmentRate FLMMobileVendor_MasterGet(dynamic dynParam)
        {
            try
            {
                int id = dynParam.id;
                DTOMobileDIAppointmentRate result = new DTOMobileDIAppointmentRate();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobileVendor_MasterGet(id);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void FLMMobileVendor_LeaveLocation(dynamic dynParam)
        {
            try
            {
                int masterID = (int)dynParam.masterID;
                int locationID = (int)dynParam.locationID;
                int statusID = (int)dynParam.statusID;
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    sv.FLMMobileVendor_LeaveLocation(masterID, locationID, statusID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region manager

        [HttpPost]
        public List<int> MobiManage_OrderList(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<int>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_OrderList(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOCATVehicle> MobiManage_VehicleList(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOCATVehicle>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_VehicleList();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMON_WFL_Message> MobiManage_GetAllNotification(dynamic dynParam)
        {
            try
            {
                var result = new List<DTOMON_WFL_Message>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_GetAllNotification();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMobile_Order> MobiManage_Order(dynamic dynParam)
        {
            try
            {
                int skip = dynParam.skip;
                int pageSize = dynParam.pageSize;
                int orderStatus = dynParam.orderStatus;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<DTOMobile_Order>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_Order(skip, pageSize, dtfrom, dtto, orderStatus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMobile_Order> MobiManage_OrderSummary(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<DTOMobile_Order>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_OrderSummary(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTOMobile_OrderDetail MobiManage_OrderDetail(dynamic dynParam)
        {
            try
            {
                int orderID = dynParam.orderID;
                var result = new DTOMobile_OrderDetail();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_OrderDetail(orderID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMobile_TOMaster> MobiManage_TOMasterList(dynamic dynParam)
        {
            try
            {

                int skip = dynParam.skip;
                int pageSize = dynParam.pageSize;
                int masterStatus = dynParam.masterStatus;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<DTOMobile_TOMaster>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_TOMasterList(skip, pageSize, dtfrom, dtto, masterStatus);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMobile_TOMaster> MobiManage_TOMasterSummary(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<DTOMobile_TOMaster>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_TOMasterSummary(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOMobile_TOMaster FLMMobile_MasterGetActualTime(dynamic dynParam)
        {
            try
            {
                int masterID = dynParam.masterID;
                var result = new DTOMobile_TOMaster();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.FLMMobile_MasterGetActualTime(masterID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMobile_Trouble> MobiManage_DITroubleList(dynamic dynParam)
        {
            try
            {

                int skip = dynParam.skip;
                int pageSize = dynParam.pageSize;
                int groupID = dynParam.groupID;
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<DTOMobile_Trouble>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_DITroubleList(skip, pageSize,dtfrom,dtto, groupID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMobile_Trouble> MobiManage_DITroubleSummary(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<DTOMobile_Trouble>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_DITroubleSummary(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOMobile_Finance> MobiManage_TruckProfit(dynamic dynParam)
        {
            try
            {

                int skip = dynParam.skip;
                int pageSize = dynParam.pageSize;
                var result = new List<DTOMobile_Finance>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_TruckProfit(skip, pageSize);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<int> MobiManage_NumOfTOMaster(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<int>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_NumOfTOMaster(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<double> MobiManage_SumOfTonCBM(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                var result = new List<double>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_SumOfTonCBM(dtfrom, dtto);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<int> MobiManage_SumOfVehicle(dynamic dynParam)
        {
            try
            {
                var result = new List<int>();
                ServiceFactory.SVMobile((ISVMobile sv) =>
                {
                    result = sv.MobiManage_SumOfVehicle();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        
    }
}