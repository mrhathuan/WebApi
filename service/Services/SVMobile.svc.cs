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
    public partial class SVMobile : SVBase, ISVMobile
    {
        public void Connect()
        {
            throw new NotImplementedException();
        }

        #region Driver
        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleRunning_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileScheduleRunning_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleAccept_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileScheduleAccept_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleOpen_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileScheduleOpen_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleGet_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileScheduleGet_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMDriverScheduleMobile> FLMMobileReject_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileReject_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOMobileFLMAsset FLMMobileCurrentAsset_Get()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileCurrentAsset_Get();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileGroupOfProduct> FLMMobileTOMaster_Get(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileTOMaster_Get(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMMobileMaster_Accept(int timeid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileMaster_Accept(timeid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMMobileMaster_ReAccept(int timeid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileMaster_ReAccept(timeid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public string FLMMobileMaster_Run(int timeid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileMaster_Run(timeid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMMobileMaster_Reject(int timeid, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileMaster_Reject(timeid, reasonID, reasonNote);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMMobileMaster_Complete(int timeid)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileMaster_Complete(timeid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTO_MONDITO> FLMMobileStatus_Save(int timeid, int masterID, int locationID, double? temp, double? Lat, double? Lng)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileStatus_Save(timeid, masterID, locationID,temp, Lat, Lng);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMobileStatus_Complete(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.FLMMobileStatus_Complete(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Mobile_UpdateDITO(int masterID, List<DTO_MONDITO> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.Mobile_UpdateDITO(masterID,lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMMobileStatus_COSave(int masterID, int locationID, int? romoocID, double? lat, double? lng)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileStatus_COSave(masterID, locationID,romoocID,lat,lng);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATReason> FLMMobileReason_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileReason_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileSODetail> FLMMobile_SOList(int masterID, int locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobile_SOList(masterID, locationID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileSODetail> FLMMobile_COList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobile_COList(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileSODetail> FLMMobile_SummarySOList(int timesheetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobile_SummarySOList(timesheetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOMobileLocationMaster FLMMobile_ListLocationOfCurrentMaster()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobile_ListLocationOfCurrentMaster();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOMobileLocationMaster FLMMobile_ListLocationOfMaster(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobile_ListLocationOfMaster(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOMobileLocationMaster FLMMobile_ListCOLocationOfMaster(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobile_ListCOLocationOfMaster(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOMobile_TOMaster FLMMobile_MasterGetActualTime(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobile_MasterGetActualTime(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMDriverScheduleMobile FLMMobileSchedule_Get(int timeSheetDriverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileSchedule_Get(timeSheetDriverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMobileDriver_UpdateTemp(int timeSheetID, int locationID, double temp)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.FLMMobileDriver_UpdateTemp(timeSheetID, locationID, temp);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMMobile_TroubleSave(DTOCATTrouble item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobile_TroubleSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATGroupOfTrouble> FLMMobile_GroupTroubleList(bool isCO)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobile_GroupTroubleList(isCO);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMDriverScheduleMobile> FLMMobileDriverHistory_List(DateTime dfrom, DateTime dto)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriverHistory_List(dfrom, dto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATTrouble> FLMMobileDriver_TroubleList(int masterID,bool isCo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_TroubleList(masterID, isCo);
                }
                
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATFile> FLMMobileDriver_FileList(CATTypeOfFileCode code, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_FileList(code, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMobileDriver_UpdateGPS(string vehicleCode, float lat, float lng)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.FLMMobileDriver_UpdateGPS(vehicleCode, lat, lng);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileRouteProblem> FLMMobileDriver_ProblemList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_ProblemList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileProblemType> FLMMobileDriver_ProblemTypeList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_ProblemTypeList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMobileDriver_ProblemSave(DTOMobileRouteProblem item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.FLMMobileDriver_ProblemSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileSalary> FLMMobileDriverSalary_List(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriverSalary_List(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTOMobileOrderReturn> Mobile_GOPReturnList(int masterID, int locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.Mobile_GOPReturnList(masterID, locationID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CUSGroupOfProduct> Mobiler_CUSGOPListNotin(int masterID, int locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.Mobiler_CUSGOPListNotin(masterID, locationID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Mobile_GOPReturnSave(DTOMONProductReturn item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.Mobile_GOPReturnSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Mobile_GOPReturnEdit(int id, double quantity)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.Mobile_GOPReturnEdit(id, quantity);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCombobox> Mobile_DITOGroupProductList(int masterID, int locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.Mobile_DITOGroupProductList(masterID, locationID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCUSProduct> Mobile_CUSProductList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.Mobile_CUSProductList(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCombobox> Mobile_CUSGOPList(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.Mobile_CUSGOPList(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobilePartnerLocation> FLMMobileDriver_PartnerList(int type)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_PartnerList(type);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobilePartnerLocation> FLMMobileDriver_PartnerLocationList(int partnerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_PartnerLocationList(partnerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMobileDriver_CheckInLocation(DTOMobilePartnerLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.FLMMobileDriver_CheckInLocation(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_Station> FLMMobileDriver_Stationlist(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_Stationlist(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_Station> FLMMobileDriver_StationPassed(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_StationPassed(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMMobileDriver_StationPass(int masterID, int stationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_StationPass(masterID, stationID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_Station> FLMMobileDriver_COStationlist(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_COStationlist(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_Station> FLMMobileDriver_COStationPassed(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_COStationPassed(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSVehicle> FLMMobile_RomoocList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobile_RomoocList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMMobileDriver_COStationPass(int masterID, int stationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileDriver_COStationPass(masterID, stationID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Mobile_SL_Save(DTOMobileSODetail item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.Mobile_SL_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMON_CUSProduct> Mobile_GroupProductOfTOGroup(int TOGroupID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.Mobile_GroupProductOfTOGroup(TOGroupID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Mobile_AddGroupProductFromDN(int opsGroupID, DTOMON_CUSProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.Mobile_AddGroupProductFromDN(opsGroupID, item);
                }
            }
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

        #region Vendor

        //vendor

        public DTOVendor FLMMobileVendor_Get(int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_Get(vendorID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOMobileDIAppointmentRate FLMMobileVendor_MasterGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_MasterGet(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileDIAppointmentRate> FLMMobileVendor_TenderRequestList(int vendorID, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_TenderRequestList(vendorID, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileDIAppointmentRate> FLMMobileVendor_TenderAcceptList(int vendorID, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_TenderAcceptList(vendorID, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileDIAppointmentRate> FLMMobileVendor_TenderRejectList(DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_TenderRejectList(DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMobileVendor_TenderApproved(int TOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.FLMMobileVendor_TenderApproved(TOMasterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMailVendor> FLMMobileVendor_TenderReject(int id, DTODIAppointmentRouteTenderReject item)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_TenderReject(id, item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATReason> FLMMobileVendor_ReasonList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_ReasonList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobileDITOGroupProduct> FLMMobileVendor_MasterDetail(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_MasterDetail(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMobileVendor_TenderSave(DTOMobileDIAppointmentRate Vehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.FLMMobileVendor_TenderSave(Vehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMobileVendor_LeaveLocation(int masterID, int locationID, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    bl.FLMMobileVendor_LeaveLocation(masterID, locationID, statusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CUSCustomer> FLMMobileVendor_ListVendor()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_ListVendor();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSVehicle> FLMMobileVendor_VehicleListVehicle(int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_VehicleListVehicle(vendorID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<FLMDriver> FLMMobileVendor_ListDriver()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.FLMMobileVendor_ListDriver();
                }
            }
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

        #region manager

        public List<int> MobiManage_OrderList(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_OrderList(dFrom, dTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<int> MobiManage_NumOfTOMaster(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_NumOfTOMaster(dFrom, dTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<double> MobiManage_SumOfTonCBM(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_SumOfTonCBM(dFrom, dTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<int> MobiManage_SumOfVehicle()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_SumOfVehicle();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATVehicle> MobiManage_VehicleList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_VehicleList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMON_WFL_Message> MobiManage_GetAllNotification()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_GetAllNotification();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_Order> MobiManage_OrderSummary(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_OrderSummary(dFrom, dTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_Order> MobiManage_Order(int skip, int pageSize, DateTime dFrom, DateTime dTo, int orderStatus)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_Order(skip, pageSize, dFrom, dTo, orderStatus);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOMobile_OrderDetail MobiManage_OrderDetail(int orderID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_OrderDetail(orderID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_TOMaster> MobiManage_TOMasterList(int skip, int pageSize, DateTime dFrom, DateTime dTo, int masterStatus)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_TOMasterList(skip, pageSize, dFrom, dTo, masterStatus);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_TOMaster> MobiManage_TOMasterSummary(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_TOMasterSummary(dFrom, dTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_Trouble> MobiManage_DITroubleList(int skip, int pageSize, DateTime dFrom, DateTime dTo, int GroupID)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_DITroubleList(skip, pageSize, dFrom, dTo, GroupID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_Trouble> MobiManage_DITroubleSummary(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_DITroubleSummary(dFrom, dTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMobile_Finance> MobiManage_TruckProfit(int skip, int pageSize)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.MobiManage_TruckProfit(skip, pageSize);
                }
            }
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

        #region RouteProblem

        public List<DTOTriggerMessage> MessageCall()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
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

        public List<DTOOPSRouteProblem> ProblemList()
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.ProblemList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSRouteProblem ProblemGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLMobile>())
                {
                    return bl.ProblemGet(id);
                }
            }
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
    }
}