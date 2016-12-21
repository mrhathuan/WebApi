using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Data;
using DTO;
using System.ServiceModel;

namespace Business
{
    public class BLMobile : Base, IBase
    {
        const string ReceiptCodeRrefix = "FLM";
        const string ReceiptCodeNum = "000000000";
        const string DIVehicleCode = "[Chờ nhập xe]";

        #region Mobile Driver
        private void DITO_UpdateFinanceReference(DataEntities model, int id)
        {
            // Nếu hợp đồng vendor tính theo IsVENLTLLevelOrder => tính lại các chuyến liên quan
            var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == id);
            if (master.ContractID.HasValue && master.CAT_Contract.IsVENLTLLevelOrder)
            {
                List<int> lstRerunID = new List<int>();
                var lstOrderGroupID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == id).Select(c => c.OrderGroupProductID.Value).Distinct().ToList();
                foreach (var orderGroupID in lstOrderGroupID)
                {
                    var lstMasterID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID != id && c.OrderGroupProductID == orderGroupID && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.DITOMasterID.HasValue && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived).Select(c => c.DITOMasterID.Value).Distinct().ToList();
                    lstRerunID.AddRange(lstMasterID);
                }
                lstRerunID = lstRerunID.Distinct().ToList();

                foreach (var item in lstRerunID)
                {
                    bool isFixPriceCUS = false;
                    bool isFixPriceVEN = false;
                    HelperFinance.DITOMaster_POD(model, Account, item, out isFixPriceCUS, out isFixPriceVEN);
                }
            }
        }

        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleRunning_List()
        {
            try
            {
                int irunning = -(int)SYSVarType.TypeOfAssetTimeSheetRunning;
                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                using (var model = new DataEntities())
                {
                    var qr = model.FLM_AssetTimeSheetDriver.Where(c => c.DriverID == Account.DriverID && c.IsMain && c.IsReject == false && c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == irunning).Select(c => new DTOFLMDriverScheduleMobile
                    {
                        DateFrom = c.FLM_AssetTimeSheet.DateFrom,
                        DateTo = c.FLM_AssetTimeSheet.DateTo,
                        AssetID = c.FLM_AssetTimeSheet.AssetID,
                        TimeSheetDriverID = c.ID,
                        TimeSheetID = c.AssetTimeSheetID,
                        StatusOfAssetTimeSheetID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        TOMasterID = c.FLM_AssetTimeSheet.ReferID,
                        StatusID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        StatusName = c.FLM_AssetTimeSheet.SYS_Var.ValueOfVar,

                        TypeOfTimeSheetID = c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID,
                        TypeOfTimeSheet = c.FLM_AssetTimeSheet.SYS_Var1.ValueOfVar,
                        RegNo = c.FLM_AssetTimeSheet.FLM_Asset.VehicleID.HasValue ? c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.RegNo : string.Empty,
                        GPSCode = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.GPSCode,
                        VehicleID = c.FLM_AssetTimeSheet.FLM_Asset.VehicleID.HasValue ? c.FLM_AssetTimeSheet.FLM_Asset.VehicleID.Value : 0,
                    }).ToList();
                    foreach (var item in qr)
                    {
                        result.Add(TimerSheet_GetTOMaster(model, item));
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleAccept_List()
        {
            try
            {
                int iAccept = -(int)SYSVarType.TypeOfAssetTimeSheetAccept;
                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                using (var model = new DataEntities())
                {
                    var qr = model.FLM_AssetTimeSheetDriver.Where(c => c.DriverID == Account.DriverID && c.IsMain && c.IsReject == false && c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == iAccept).Select(c => new DTOFLMDriverScheduleMobile
                    {
                        DateFrom = c.FLM_AssetTimeSheet.DateFrom,
                        DateTo = c.FLM_AssetTimeSheet.DateTo,
                        AssetID = c.FLM_AssetTimeSheet.AssetID,
                        TimeSheetDriverID = c.ID,
                        TimeSheetID = c.AssetTimeSheetID,
                        StatusOfAssetTimeSheetID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        TOMasterID = c.FLM_AssetTimeSheet.ReferID,
                        StatusID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        StatusName = c.FLM_AssetTimeSheet.SYS_Var.ValueOfVar,
                        IsContainer = false,
                        TypeOfTimeSheetID = c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID,
                        TypeOfTimeSheet = c.FLM_AssetTimeSheet.SYS_Var1.ValueOfVar,
                        GPSCode = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.GPSCode,
                        RegNo = c.FLM_AssetTimeSheet.FLM_Asset.VehicleID.HasValue ? c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.RegNo : string.Empty,
                    }).ToList();
                    foreach (var item in qr)
                    {
                        result.Add(TimerSheet_GetTOMaster(model, item));
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleOpen_List()
        {
            try
            {
                int iOpen = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                using (var model = new DataEntities())
                {
                    result = model.FLM_AssetTimeSheetDriver.Where(c => c.DriverID == Account.DriverID && c.IsMain && c.IsReject == false && c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == iOpen).Select(c => new DTOFLMDriverScheduleMobile
                    {
                        DateFrom = c.FLM_AssetTimeSheet.DateFrom,
                        DateTo = c.FLM_AssetTimeSheet.DateTo,
                        AssetID = c.FLM_AssetTimeSheet.AssetID,
                        TimeSheetDriverID = c.ID,
                        TimeSheetID = c.AssetTimeSheetID,
                        StatusOfAssetTimeSheetID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        TOMasterID = c.FLM_AssetTimeSheet.ReferID,
                        StatusID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        StatusName = c.FLM_AssetTimeSheet.SYS_Var.ValueOfVar,
                        IsContainer = false,
                        TypeOfTimeSheetID = c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID,
                        TypeOfTimeSheet = c.FLM_AssetTimeSheet.SYS_Var1.ValueOfVar,
                        GPSCode = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.GPSCode,
                        RegNo = c.FLM_AssetTimeSheet.FLM_Asset.VehicleID.HasValue ? c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.RegNo : string.Empty,
                    }).OrderByDescending(x=>x.TOMasterID).ToList();
                    foreach (var item in result)
                    {
                        TimerSheet_GetTOMaster(model, item);
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOFLMDriverScheduleMobile> FLMMobileScheduleGet_List()
        {
            try
            {
                int iGet = -(int)SYSVarType.TypeOfAssetTimeSheetGet;
                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                using (var model = new DataEntities())
                {
                    result = model.FLM_AssetTimeSheetDriver.Where(c => c.DriverID == Account.DriverID && c.IsMain && c.IsReject == false && c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == iGet).Select(c => new DTOFLMDriverScheduleMobile
                    {
                        DateFrom = c.FLM_AssetTimeSheet.DateFrom,
                        DateTo = c.FLM_AssetTimeSheet.DateTo,
                        AssetID = c.FLM_AssetTimeSheet.AssetID,
                        TimeSheetDriverID = c.ID,
                        TimeSheetID = c.AssetTimeSheetID,
                        StatusOfAssetTimeSheetID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        TOMasterID = c.FLM_AssetTimeSheet.ReferID,
                        StatusID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        StatusName = c.FLM_AssetTimeSheet.SYS_Var.ValueOfVar,

                        TypeOfTimeSheetID = c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID,
                        TypeOfTimeSheet = c.FLM_AssetTimeSheet.SYS_Var1.ValueOfVar,
                        GPSCode = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.GPSCode,
                        RegNo = c.FLM_AssetTimeSheet.FLM_Asset.VehicleID.HasValue ? c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.RegNo : string.Empty,
                    }).ToList();
                    foreach (var item in result)
                    {
                        if (item.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster)
                        {
                            item.MasterCode = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.TOMasterID).Code;
                        }
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOCATReason> FLMMobileReason_List()
        {
            try
            {
                List<DTOCATReason> result = new List<DTOCATReason>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_Reason.Where(c => c.TypeOfReasonID == -(int)SYSVarType.TypeOfReasonDriverReject).Select(c => new DTOCATReason
                    {

                        ID = c.ID,
                        ReasonName = c.ReasonName,
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOFLMDriverScheduleMobile> FLMMobileReject_List()
        {
            try
            {
                int iReject = -(int)SYSVarType.TypeOfAssetTimeSheetReject;
                int iget = -(int)SYSVarType.TypeOfAssetTimeSheetGet;
                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                using (var model = new DataEntities())
                {
                    //list chuyen ma tai xe nay da tu choi
                    var lstTimeSheet = model.FLM_AssetTimeSheetDriver.Where(c => c.DriverID == Account.DriverID && (c.IsReject == false || (c.IsReject == true && (c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == iReject || c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == iget)))).Select(c => c.AssetTimeSheetID).ToList();

                    result = model.FLM_AssetTimeSheet.Where(c => !lstTimeSheet.Contains(c.ID) && c.TypeOfAssetTimeSheetID == iReject ).Select(c => new DTOFLMDriverScheduleMobile
                    {
                        DateFrom = c.DateFrom,
                        DateTo = c.DateTo,
                        AssetID = c.AssetID,
                        TimeSheetID = c.ID,
                        StatusOfAssetTimeSheetID = c.StatusOfAssetTimeSheetID,
                        TOMasterID = c.ReferID,
                        StatusID = c.StatusOfAssetTimeSheetID,
                        StatusName = c.SYS_Var.ValueOfVar,
                        TypeOfTimeSheetID = c.TypeOfAssetTimeSheetID,
                        TypeOfTimeSheet = c.SYS_Var1.ValueOfVar,
                        GPSCode = c.FLM_Asset.CAT_Vehicle.GPSCode,
                        RegNo = c.FLM_Asset.VehicleID.HasValue ? c.FLM_Asset.CAT_Vehicle.RegNo : string.Empty,
                    }).OrderByDescending(x=>x.TimeSheetID).ToList();
                    foreach (var item in result)
                    {
                        if (item.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster)
                        {
                            //ds diem nhan
                            List<int> lstFrom = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location.LocationID).Distinct().ToList();
                            //ds diem giao
                            List<int> lstTo = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location1.LocationID).Distinct().ToList();
                            //lst ton cbm
                            var lsttc = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => new { c.Ton, c.CBM }).ToList();

                            item.Ton = lsttc.Sum(c => c.Ton);
                            item.CBM = lsttc.Sum(c => c.CBM);

                            item.MasterCode = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.TOMasterID).Code;
                            item.lstLocationFrom = new List<DTOMobileOPSDITOLocation>();


                            item.lstLocationFrom.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == item.TOMasterID && c.LocationID.HasValue && lstFrom.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                            {
                                ID = c.ID,
                                LocationID = c.LocationID,
                                LocationName = c.CAT_Location.Location,
                                LocationAddress = c.CAT_Location.Address,
                                LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                                LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                                TOLocationStatusID = c.DITOLocationStatusID,
                                TOLocationStatusName = c.SYS_Var.ValueOfVar,
                                DateCome = c.DateCome,
                                DateLeave = c.DateLeave,
                                LoadingStart = c.LoadingStart,
                                LoadingEnd = c.LoadingEnd,
                                SortOrder = c.SortOrder,
                                SortOrderReal = c.SortOrderReal,
                            }).ToList());

                            item.lstLocationTo = new List<DTOMobileOPSDITOLocation>();
                            item.lstLocationTo.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == item.TOMasterID && c.LocationID.HasValue && lstTo.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                            {
                                ID = c.ID,
                                LocationID = c.LocationID,
                                LocationName = c.CAT_Location.Location,
                                LocationAddress = c.CAT_Location.Address,
                                LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                                LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                                TOLocationStatusID = c.DITOLocationStatusID,
                                TOLocationStatusName = c.SYS_Var.ValueOfVar,
                                DateCome = c.DateCome,
                                DateLeave = c.DateLeave,
                                LoadingStart = c.LoadingStart,
                                LoadingEnd = c.LoadingEnd,
                                SortOrder = c.SortOrder,
                                SortOrderReal = c.SortOrderReal
                            }).ToList());
                        }
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobileGroupOfProduct> FLMMobileTOMaster_Get(int masterID)
        {
            try
            {
                List<DTOMobileGroupOfProduct> result = default(List<DTOMobileGroupOfProduct>);
                using (var model = new DataEntities())
                {
                    result = model.OPS_DITOGroupProduct.Where(c => c.ID == masterID).Select(c => new DTOMobileGroupOfProduct
                    {
                        GroupOfPrductName = c.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                        Ton = c.Ton,
                        CBM = c.CBM,
                        Quantity = c.Quantity,
                        LocationFrom = c.ORD_GroupProduct.CUS_Location.LocationName,
                        LocationTo = c.ORD_GroupProduct.CUS_Location1.LocationName,
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOMobileFLMAsset FLMMobileCurrentAsset_Get()
        {
            try
            {
                int iStatusRunning = -(int)SYSVarType.TypeOfAssetTimeSheetRunning;
                DTOMobileFLMAsset result = new DTOMobileFLMAsset();
                using (var model = new DataEntities())
                {
                    result = model.FLM_AssetTimeSheetDriver.Where(c => c.DriverID == Account.DriverID && c.IsMain && c.IsReject == false && c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == iStatusRunning).Select(c => new DTOMobileFLMAsset
                    {
                        ID = c.FLM_AssetTimeSheet.FLM_Asset.ID,
                        VehicleID = c.FLM_AssetTimeSheet.FLM_Asset.VehicleID,
                        GroupOfVehicleID = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.GroupOfVehicleID,
                        GroupOfVehicleName = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.GroupOfVehicleID.HasValue ? c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.CAT_GroupOfVehicle.GroupName : string.Empty,
                        YearOfProduction = c.FLM_AssetTimeSheet.FLM_Asset.YearOfProduction,
                        Manufactor = c.FLM_AssetTimeSheet.FLM_Asset.Manufactor,
                        IsRent = c.FLM_AssetTimeSheet.FLM_Asset.IsRent,
                        RentID = c.FLM_AssetTimeSheet.FLM_Asset.RentID,
                        RegNo = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.RegNo,
                        Note = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.Note,
                        IsOwn = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.IsOwn,
                        TypeOfVehicleID = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.TypeOfVehicleID,
                        MaxWeight = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.MaxWeight,
                        MaxCapacity = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.MaxCapacity,
                        RegWeight = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.RegWeight,
                        RegCapacity = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.RegCapacity,
                        Lat = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.Lat,
                        Lng = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.Lng,
                        AssistantID = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.FLM_Driver1.ID,
                        TimeSheetDriverID = c.ID,
                        MasterID = c.FLM_AssetTimeSheet.ReferID,
                        StatusOfAssetTimeSheetID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID
                    }).FirstOrDefault();
                    if (result != null)
                    {
                        if (result.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster)
                        {
                            var qmaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == result.MasterID);
                            if (qmaster != null)
                                result.MasterCode = qmaster.Code;
                        }
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public bool FLMMobileMaster_Accept(int TimeSheetDriverID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var a = model.FLM_AssetTimeSheetDriver.FirstOrDefault(c => c.ID == TimeSheetDriverID);
                    if (!CheckMaterExist(model, a.AssetTimeSheetID))
                        throw FaultHelper.BusinessFault(null, null, "Chuyến không tồn tại");

                    if (a.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetOpen)
                    {
                        a.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetAccept;
                        a.IsReject = false;
                        a.ModifiedDate = DateTime.Now;
                        a.ModifiedBy = Account.UserName;
                        model.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public bool FLMMobileMaster_ReAccept(int TimeSheetID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var a = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ID == TimeSheetID);
                    if (!CheckMaterExist(model, TimeSheetID))
                        throw FaultHelper.BusinessFault(null, null, "Chuyến không tồn tại");
                    if (a != null)
                    {
                        if (a.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetReject)
                        {
                            a.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetGet;
                            var o = new FLM_AssetTimeSheetDriver();
                            o.CreatedBy = Account.UserName;
                            o.CreatedDate = DateTime.Now;
                            o.DriverID = Account.DriverID.Value;
                            o.IsMain = true;
                            o.IsReject = false;
                            a.FLM_AssetTimeSheetDriver.Add(o);
                            a.ModifiedDate = DateTime.Now;
                            a.ModifiedBy = Account.UserName;

                            model.SaveChanges();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public string FLMMobileMaster_Run(int TimeSheetDriverID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var b = model.FLM_AssetTimeSheetDriver.Where(c => c.DriverID == Account.DriverID && c.IsReject == false && c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetRunning).ToList();
                    if (b.Count == 0)
                    {
                        var a = model.FLM_AssetTimeSheetDriver.FirstOrDefault(c => c.ID == TimeSheetDriverID);
                        if (a.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID < -(int)SYSVarType.TypeOfAssetTimeSheetRunning)
                        {
                            a.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetRunning;
                            a.ModifiedDate = DateTime.Now;
                            a.ModifiedBy = Account.UserName;
                            a.FLM_AssetTimeSheet.DateFromActual = DateTime.Now;
                            //cap nhat ATD
                            if (a.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster)
                            {
                                var qrmaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == a.FLM_AssetTimeSheet.ReferID);
                                if (qrmaster != null)
                                {
                                    qrmaster.ATD = DateTime.Now;
                                    qrmaster.ModifiedBy = Account.UserName;
                                    qrmaster.ModifiedDate = DateTime.Now;
                                    qrmaster.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterDelivery;
                                    var qrmasterLocation = model.OPS_DITOLocation.Where(c => c.DITOMasterID == a.FLM_AssetTimeSheet.ReferID && c.SortOrder == 1 && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationEmpty).FirstOrDefault();
                                    if (qrmasterLocation == null)
                                        throw FaultHelper.BusinessFault(null, null, "Chuyến chưa thiết lập điểm bắt đầu");
                                    qrmasterLocation.SortOrderReal = 1;
                                }
                                else
                                {
                                    var flm = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ID == a.AssetTimeSheetID);
                                    if (flm != null)
                                    {
                                        model.FLM_AssetTimeSheetDriver.RemoveRange(flm.FLM_AssetTimeSheetDriver);
                                        model.SaveChanges();
                                        model.FLM_AssetTimeSheet.Remove(flm);
                                        model.SaveChanges();
                                    }
                                    throw FaultHelper.BusinessFault(null, null, "Chuyến không còn tồn tại");
                                }
                            }
                            if (a.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster)
                            {
                                var qrmaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == a.FLM_AssetTimeSheet.ReferID);
                                if (qrmaster != null)
                                {
                                    qrmaster.ATD = DateTime.Now;
                                    qrmaster.ModifiedBy = Account.UserName;
                                    qrmaster.ModifiedDate = DateTime.Now;
                                    qrmaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterDelivery;

                                    var qrmasterLocation = model.OPS_COTOLocation.Where(c => c.COTOMasterID == a.FLM_AssetTimeSheet.ReferID && c.SortOrder == 1 && (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationEmpty || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetRomooc)).FirstOrDefault();
                                    if (qrmasterLocation == null)
                                        throw FaultHelper.BusinessFault(null, null, "Chuyến chưa thiết lập điểm bắt đầu");
                                    qrmasterLocation.SortOrderReal = 1;
                                }
                                else
                                {
                                    var flm = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ID == a.AssetTimeSheetID);
                                    if (flm != null)
                                    {
                                        model.FLM_AssetTimeSheetDriver.RemoveRange(flm.FLM_AssetTimeSheetDriver);
                                        model.SaveChanges();
                                        model.FLM_AssetTimeSheet.Remove(flm);
                                        model.SaveChanges();
                                    }
                                    throw FaultHelper.BusinessFault(null, null, "Chuyến không còn tồn tại");
                                }
                            }
                            model.SaveChanges();
                            return string.Empty;
                        }
                        else
                        {
                            CheckMaterExist(model, a.AssetTimeSheetID);
                            return "Chuyến đã được chạy hoặc hoàn thành";
                        }
                    }
                    else
                    {
                        var lstTimeSheetID = b.Select(c => c.AssetTimeSheetID).Distinct().ToList();
                        foreach (var timesheetID in lstTimeSheetID)
                        {
                            CheckMaterExist(model, timesheetID);
                        }
                        return "Hoàn thành chuyến hiện tại trước khi nhận chuyến mới";

                    }

                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public bool FLMMobileMaster_Reject(int TimeSheetDriverID, int reasonID, string reasonNote)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var a = model.FLM_AssetTimeSheetDriver.FirstOrDefault(c => c.ID == TimeSheetDriverID);
                    if (a.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID < -(int)SYSVarType.TypeOfAssetTimeSheetRunning)
                    {
                        a.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetReject;
                        a.ReasonID = reasonID;
                        a.Note = reasonNote;
                        a.ModifiedDate = DateTime.Now;
                        a.ModifiedBy = Account.UserName;
                        a.IsReject = true;
                        model.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public bool FLMMobileMaster_Complete(int TimeSheetDriverID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var a = model.FLM_AssetTimeSheetDriver.FirstOrDefault(c => c.ID == TimeSheetDriverID);
                    if (a.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetRunning)
                    {
                        a.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                        a.IsReject = false;
                        a.ModifiedDate = DateTime.Now;
                        a.ModifiedBy = Account.UserName;
                        a.FLM_AssetTimeSheet.DateToActual = DateTime.Now;
                        model.SaveChanges();
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTO_MONDITO> FLMMobileStatus_Save(int timeid, int masterID, int locationID, double? temp,double? Lat, double? Lng)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    List<DTO_MONDITO> result = new List<DTO_MONDITO>();
                    var location = model.OPS_DITOLocation.FirstOrDefault(c => c.DITOMasterID == masterID && c.LocationID == locationID && c.TypeOfTOLocationID != -(int)SYSVarType.TypeOfTOLocationEmpty);
                    if (location != null)
                    {
                        //tim sortorder
                        int sort = 1;
                        var qrsort = model.OPS_DITOLocation.Where(c=>c.DITOMasterID == masterID).OrderByDescending(c => c.SortOrderReal).Select(c => c.SortOrderReal).FirstOrDefault();
                        if (qrsort != null)
                            sort = qrsort.Value + 1;

                        const int StatusPlan = -(int)SYSVarType.DITOLocationStatusPlan;
                        const int StatusCome = -(int)SYSVarType.DITOLocationStatusCome;
                        const int StatusLeave = -(int)SYSVarType.DITOLocationStatusLeave;
                        const int StatusStart = -(int)SYSVarType.DITOLocationStatusLoadStart;
                        const int StatusEnd = -(int)SYSVarType.DITOLocationStatusLoadEnd;
                        location.ModifiedBy = Account.UserName;
                        location.ModifiedDate = DateTime.Now;                        
                        location.Temp = temp; 
                        if(location.CAT_Location.Lat == null || location.CAT_Location.Lng == null)
                        {
                            location.CAT_Location.Lat = Lat;
                            location.CAT_Location.Lng = Lng;
                        }                      
                        if (location.SortOrderReal == null)
                            location.SortOrderReal = sort;

                        var qrOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == masterID && (c.ORD_GroupProduct.CUS_Location.LocationID == locationID || c.ORD_GroupProduct.CUS_Location1.LocationID == locationID)).Distinct().ToList();
                        
                        switch (location.DITOLocationStatusID)
                        {
                            case StatusPlan:
                                location.DITOLocationStatusID = StatusCome;
                                location.DateCome = DateTime.Now;
                                foreach (var opsGroup in qrOPSGroup)
                                {
                                    if (location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery)
                                    {
                                        opsGroup.DateFromCome = DateTime.Now;
                                    }
                                    if (location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery)
                                    {
                                        opsGroup.DateToCome = DateTime.Now;
                                    }
                                }
                                break;
                            case StatusCome:
                                location.DITOLocationStatusID = StatusStart;
                                location.LoadingStart = DateTime.Now;
                                foreach (var opsGroup in qrOPSGroup)
                                {
                                    if (location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery)
                                    {
                                        opsGroup.DateFromLoadStart = DateTime.Now;
                                    }
                                    if (location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery)
                                    {
                                        opsGroup.DateToLoadStart = DateTime.Now;
                                    }
                                }
                                break;
                            case StatusStart:
                                location.DITOLocationStatusID = StatusEnd;
                                location.LoadingEnd = DateTime.Now;
                                foreach (var opsGroup in qrOPSGroup)
                                {
                                    if (location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery)
                                    {
                                        opsGroup.DateFromLoadEnd = DateTime.Now;
                                    }
                                    if (location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery)
                                    {
                                        opsGroup.DateToLoadEnd = DateTime.Now;
                                    }
                                }
                                break;
                            case StatusEnd:
                                location.DITOLocationStatusID = StatusLeave;
                                location.DateLeave = DateTime.Now;

                                foreach (var opsGroup in qrOPSGroup)
                                {
                                    if (location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery)
                                    {
                                        opsGroup.DateFromLeave = DateTime.Now;
                                    }
                                    if (location.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery)
                                    {
                                        opsGroup.DateToLeave = DateTime.Now;
                                        if(StatusLeave != location.DITOLocationStatusID)
                                        {
                                            HelperFinance.Truck_CompleteSchedule(model, Account, -1, opsGroup.ID);
                                        }                                        
                                        var qrmaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == masterID);
                                        if (qrmaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived)
                                        {
                                            result = ListDITO(model, Account, masterID, qrmaster.CAT_Vehicle.RegNo);
                                        }
                                    }
                                }
                                break;
                        }

                        model.SaveChanges();
                    }
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void FLMMobileStatus_Complete(int masterID)
        {
            try
            {
                using(var model = new DataEntities())
                {
                    model.EventAccount = Account;                                     
                    var qrOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == masterID ).Distinct().ToList();
                    foreach (var opsGroup in qrOPSGroup)
                    {
                        opsGroup.DateToLeave = DateTime.Now;
                        HelperFinance.Truck_CompleteSchedule(model, Account, -1, opsGroup.ID);
                    }
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void Mobile_UpdateDITO(int masterID,List<DTO_MONDITO> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var lstDITO = model.OPS_DITO.Where(c => c.DITOMasterID == masterID);
                    foreach (var obj in lst)
                    {
                        var qr = lstDITO.FirstOrDefault(c => c.ATA == obj.ATA && c.ATD == obj.ATD && c.LocationToID == obj.LocationToID && c.LocationFromID == obj.LocationFromID);
                        if (qr != null)
                        {
                            qr.ModifiedBy = Account.UserName;
                            qr.ModifiedDate = DateTime.Now;
                            qr.KM = obj.KM;
                        }
                    }
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        private List<DTO_MONDITO> ListDITO(DataEntities model, AccountItem account, int masterID, string regno)
        {
            List<DTO_MONDITO> lst = new List<DTO_MONDITO>();
            lst = model.OPS_DITO.Where(c => c.DITOMasterID == masterID).Select(c => new DTO_MONDITO
            {
                MasterID = masterID,
                ATA = c.ATA,
                ATD = c.ATD,
                KM = c.KM,
                LocationFromID = c.LocationFromID,
                LocationToID = c.LocationToID,
                RegNo = regno,
            }).ToList();

            return lst;
        }

        //test

        public Dictionary<object, object> Mobile_Test()
        {
            var result = new Dictionary<object, object>();


            return result;
        }
        //

        public bool FLMMobileStatus_COSave(int masterID, int locationID, int? romoocID,double?lat, double?lng)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    bool IsComplete = false;

                    var location = model.OPS_COTOLocation.FirstOrDefault(c => c.COTOMasterID == masterID && c.LocationID == locationID);
                    if (location != null)
                    {
                        //tim sortorder
                        int sort = 1;
                        var qrsort = model.OPS_COTOLocation.OrderByDescending(c => c.SortOrderReal).Select(c => c.SortOrderReal).FirstOrDefault();
                        if (qrsort != null)
                            sort = qrsort.Value + 1;

                        const int StatusPlan = -(int)SYSVarType.COTOLocationStatusPlan;
                        const int StatusCome = -(int)SYSVarType.COTOLocationStatusCome;
                        const int StatusLeave = -(int)SYSVarType.COTOLocationStatusLeave;
                        const int StatusStart = -(int)SYSVarType.COTOLocationStatusLoadStart;
                        const int StatusEnd = -(int)SYSVarType.COTOLocationStatusLoadEnd;
                        location.ModifiedBy = Account.UserName;
                        location.ModifiedDate = DateTime.Now;
                        if (location.SortOrderReal == null)
                            location.SortOrderReal = sort;
                        if(location.CAT_Location.Lat == null || location.CAT_Location.Lng == null)
                        {
                            location.CAT_Location.Lat = lat;
                            location.CAT_Location.Lng = lng;
                        }
                        switch (location.COTOLocationStatusID)
                        {
                            case StatusPlan:
                                location.COTOLocationStatusID = StatusCome;
                                location.DateCome = DateTime.Now;
                                break;
                            default:
                                location.COTOLocationStatusID = StatusLeave;
                                location.DateLeave = DateTime.Now;
                                break;
                        }

                        model.SaveChanges();

                        if (location.COTOLocationStatusID == StatusLeave)
                        {
                            //cap nhat romooc

                            if (romoocID >= 0)
                            {
                                var qrVehicleMaster = model.OPS_COTOMaster.Where(c => c.ID == masterID && c.VehicleID > 0).Select(c => c.VehicleID).FirstOrDefault();
                                if (qrVehicleMaster == null)
                                    FaultHelper.BusinessFault(null, null, "Chuyến không tồn tại");
                                var qrVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == qrVehicleMaster);
                                if (qrVehicle == null)
                                    FaultHelper.BusinessFault(null, null, "Xe không tồn tại");
                                if (romoocID == 0)
                                    qrVehicle.CurrentRomoocID = null;
                                else
                                    qrVehicle.CurrentRomoocID = romoocID;
                            }

                            //tim ops_group // chi tim diem giao hang
                            var qrOPSGroup = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterID
                                && c.LocationToID == locationID).Select(c => c.ID).Distinct().ToList();
                            foreach (var opsGroupID in qrOPSGroup)
                            {
                                HelperFinance.Container_CompleteSchedule(model, Account, -1, opsGroupID);
                            }
                            return true;
                        }
                    }
                    return IsComplete;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobileSODetail> FLMMobile_SOList(int masterID, int locationID)
        {
            try
            {
                List<DTOMobileSODetail> result = new List<DTOMobileSODetail>();
                using (var model = new DataEntities())
                {
                    result = model.OPS_DITOProduct.Where(c => c.OPS_DITOGroupProduct.DITOMasterID == masterID && c.OPS_DITOGroupProduct.ORD_GroupProduct.IsReturn != true && (c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationID == locationID || c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationID == locationID)).Select(c => new DTOMobileSODetail
                    {

                        ID = c.ID,
                        TOProductID = c.ID,
                        TOGroupProductID = c.DITOGroupProductID,
                        SOCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.SOCode,
                        Ton = c.OPS_DITOGroupProduct.ORD_GroupProduct.Ton,
                        CBM = c.OPS_DITOGroupProduct.ORD_GroupProduct.CBM,
                        GroupProductName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                        ProductName = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Product.FirstOrDefault().CUS_Product.ProductName,
                        QuantityReturn = c.QuantityReturn,
                        QuantityTranfer = c.QuantityTranfer,
                        Note = c.Note
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobileSODetail> FLMMobile_SummarySOList(int timesheetID)
        {
            try
            {
                List<DTOMobileSODetail> result = new List<DTOMobileSODetail>();
                using (var model = new DataEntities())
                {
                    var timesheet = model.FLM_AssetTimeSheet.Where(c => c.ID == timesheetID).Select(c => new { c.ReferID, c.StatusOfAssetTimeSheetID }).FirstOrDefault();
                    if (timesheet == null)
                        throw FaultHelper.BusinessFault(null, null, "Chuyến không tồn tại");
                    if (timesheet.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster)
                    {
                        if (model.OPS_DITOMaster.Where(c => c.ID == timesheet.ReferID).FirstOrDefault() == null)
                            throw FaultHelper.BusinessFault(null, null, "Chuyến không tồn tại");
                        result = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == timesheet.ReferID).Select(c => new DTOMobileSODetail
                        {
                            ID = c.ID,
                            SOCode = c.ORD_GroupProduct.SOCode,
                            Ton = c.ORD_GroupProduct.Ton,
                            CBM = c.ORD_GroupProduct.CBM,
                        }).ToList();
                    }
                    if (timesheet.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster)
                    {
                        if (model.OPS_COTOMaster.Where(c => c.ID == timesheet.ReferID).FirstOrDefault() == null)
                            throw FaultHelper.BusinessFault(null, null, "Chuyến không tồn tại");
                        result = model.OPS_COTOContainer.Where(c => c.COTOMasterID == timesheet.ReferID).Select(c => new DTOMobileSODetail
                        {
                            ID = c.ID,
                            SOCode = c.OPS_Container.ContainerNo,
                        }).ToList();
                    }
                    
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobileSODetail> FLMMobile_COList(int masterID)
        {
            try
            {
                List<DTOMobileSODetail> result = new List<DTOMobileSODetail>();
                using (var model = new DataEntities())
                {
                    result = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterID ).Select(c => new DTOMobileSODetail
                    {

                        ID = c.ID,
                        SOCode = c.OPS_Container.ContainerNo,
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOMobileLocationMaster FLMMobile_ListLocationOfCurrentMaster()
        {
            try
            {
                int irunning = -(int)SYSVarType.TypeOfAssetTimeSheetRunning;

                int comestt = -(int)SYSVarType.DITOLocationStatusCome;
                int leavestt = -(int)SYSVarType.DITOLocationStatusLeave;
                DTOMobileLocationMaster result = new DTOMobileLocationMaster();
                using (var model = new DataEntities())
                {
                    var qr = model.FLM_AssetTimeSheetDriver.Where(c => c.DriverID == Account.DriverID && c.IsMain && c.IsReject == false && c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == irunning).Select(c => new DTOFLMDriverScheduleMobile
                    {
                        StatusOfAssetTimeSheetID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        TOMasterID = c.FLM_AssetTimeSheet.ReferID,
                    }).FirstOrDefault();
                    if (qr != null)
                    {
                        if (qr.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster)
                        {
                            result.lstLocationFrom = new List<DTOMobileLocation>();
                            result.lstLocationTo = new List<DTOMobileLocation>();

                            //ds diem nhan
                            List<int> lstFrom = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == qr.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location.LocationID).Distinct().ToList();
                            //ds diem giao
                            List<int> lstTo = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == qr.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location1.LocationID).Distinct().ToList();

                            result.lstLocationFrom.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == qr.TOMasterID && c.LocationID.HasValue && lstFrom.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileLocation
                            {
                                LocationID = c.LocationID.Value,
                                Lat = c.CAT_Location.Lat,
                                Lng = c.CAT_Location.Lng,
                                LocationName = c.CAT_Location.Location,
                                Status = c.DITOLocationStatusID < comestt ? 0 : c.DITOLocationStatusID == comestt ? 1 : c.DITOLocationStatusID == leavestt ? 4 : 0,
                                DITOLocationStatusID = c.DITOLocationStatusID,
                                SortOrderReal = c.SortOrderReal,
                                SortOrder = c.SortOrder,
                                DistanceRemain = -1,
                            }).ToList());

                            result.lstLocationTo.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == qr.TOMasterID && c.LocationID.HasValue && lstTo.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileLocation
                            {
                                LocationID = c.LocationID.Value,
                                Lat = c.CAT_Location.Lat,
                                Lng = c.CAT_Location.Lng,
                                LocationName = c.CAT_Location.Location + "(" + c.CAT_Location.Address + ")",
                                Status = c.DITOLocationStatusID < comestt ? 0 : c.DITOLocationStatusID == comestt ? 1 : c.DITOLocationStatusID == leavestt ? 4 : 0,
                                DITOLocationStatusID = c.DITOLocationStatusID,
                                SortOrderReal = c.SortOrderReal,
                                SortOrder = c.SortOrder,
                                DistanceRemain = -1,
                            }).ToList());
                        }
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOMobileLocationMaster FLMMobile_ListLocationOfMaster(int masterID)
        {
            try
            {

                int comestt = -(int)SYSVarType.DITOLocationStatusCome;
                int leavestt = -(int)SYSVarType.DITOLocationStatusLeave;
                DTOMobileLocationMaster result = new DTOMobileLocationMaster();
                using (var model = new DataEntities())
                {
                    result.lstLocationFrom = new List<DTOMobileLocation>();
                    result.lstLocationTo = new List<DTOMobileLocation>();

                    //ds diem nhan
                    List<int> lstFrom = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == masterID).Select(c => c.ORD_GroupProduct.CUS_Location.LocationID).Distinct().ToList();
                    //ds diem giao
                    List<int> lstTo = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == masterID).Select(c => c.ORD_GroupProduct.CUS_Location1.LocationID).Distinct().ToList();

                    result.lstLocationFrom.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == masterID && c.LocationID.HasValue && lstFrom.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileLocation
                    {
                        LocationID = c.LocationID.Value,
                        Lat = c.CAT_Location.Lat,
                        Lng = c.CAT_Location.Lng,
                        Status = c.DITOLocationStatusID < comestt ? 0 : c.DITOLocationStatusID == comestt ? 1 : c.DITOLocationStatusID == leavestt ? 4 : 0,
                        DITOLocationStatusID = c.DITOLocationStatusID,
                        SortOrderReal = c.SortOrderReal,
                        SortOrder = c.SortOrder,
                        DistanceRemain = -1,
                    }).ToList());

                    result.lstLocationTo.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == masterID && c.LocationID.HasValue && lstTo.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileLocation
                    {
                        LocationID = c.LocationID.Value,
                        Lat = c.CAT_Location.Lat,
                        Lng = c.CAT_Location.Lng,
                        Status = c.DITOLocationStatusID < comestt ? 0 : c.DITOLocationStatusID == comestt ? 1 : c.DITOLocationStatusID == leavestt ? 4 : 0,
                        DITOLocationStatusID = c.DITOLocationStatusID,
                        SortOrderReal = c.SortOrderReal,
                        SortOrder = c.SortOrder,
                        DistanceRemain = -1,
                    }).ToList());
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOMobileLocationMaster FLMMobile_ListCOLocationOfMaster(int masterID)
        {
            try
            {

                int comestt = -(int)SYSVarType.COTOLocationStatusCome;
                int leavestt = -(int)SYSVarType.COTOLocationStatusLeave;
                DTOMobileLocationMaster result = new DTOMobileLocationMaster();
                using (var model = new DataEntities())
                {

                    var qrMaster = model.OPS_COTOMaster.Where(c => c.ID == masterID).Select(c => new { c.VehicleID, c.OPS_COTOLocation, c.Code, c.ID }).FirstOrDefault();
                    if (qrMaster != null)
                    {
                        result.lstLocationFrom = new List<DTOMobileLocation>();
                        result.lstLocationFrom.AddRange(qrMaster.OPS_COTOLocation.Where(c => c.LocationID.HasValue && (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileLocation
                        {
                            LocationID = c.LocationID.Value,
                            Lat = c.CAT_Location.Lat,
                            Lng = c.CAT_Location.Lng,
                            Status = c.COTOLocationStatusID < comestt ? 0 : c.COTOLocationStatusID == comestt ? 1 : c.COTOLocationStatusID == leavestt ? 4 : 0,
                            DITOLocationStatusID = c.COTOLocationStatusID,
                            SortOrderReal = c.SortOrderReal,
                            SortOrder = c.SortOrder,
                            DistanceRemain = -1,
                        }).ToList());

                        result.lstLocationTo = new List<DTOMobileLocation>();
                        result.lstLocationTo.AddRange(qrMaster.OPS_COTOLocation.Where(c => c.LocationID.HasValue && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery).OrderBy(c => c.SortOrder).Select(c => new DTOMobileLocation
                        {
                            LocationID = c.LocationID.Value,
                            Lat = c.CAT_Location.Lat,
                            Lng = c.CAT_Location.Lng,
                            Status = c.COTOLocationStatusID < comestt ? 0 : c.COTOLocationStatusID == comestt ? 1 : c.COTOLocationStatusID == leavestt ? 4 : 0,
                            DITOLocationStatusID = c.COTOLocationStatusID,
                            SortOrderReal = c.SortOrderReal,
                            SortOrder = c.SortOrder,
                            DistanceRemain = -1,
                        }).ToList());
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOFLMDriverScheduleMobile> FLMMobileDriverHistory_List(DateTime dfrom, DateTime dto)
        {
            try
            {
                int iReject = -(int)SYSVarType.TypeOfAssetTimeSheetReject;
                int iComplete = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                List<DTOFLMDriverScheduleMobile> result = new List<DTOFLMDriverScheduleMobile>();
                dfrom = dfrom.Date;
                dto = dto.Date.AddDays(1);

                using (var model = new DataEntities())
                {
                    result = model.FLM_AssetTimeSheetDriver.Where(c => c.DriverID == Account.DriverID && c.IsMain &&
                        ((c.FLM_AssetTimeSheet.DateFromActual >= dfrom && c.FLM_AssetTimeSheet.DateFromActual < dto) || (c.FLM_AssetTimeSheet.DateToActual >= dfrom && c.FLM_AssetTimeSheet.DateToActual < dto)) &&
                        (c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == iReject || c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == iComplete)).Select(c => new DTOFLMDriverScheduleMobile
                    {
                        DateFrom = c.FLM_AssetTimeSheet.DateFrom,
                        DateTo = c.FLM_AssetTimeSheet.DateTo,
                        AssetID = c.FLM_AssetTimeSheet.AssetID,
                        TimeSheetDriverID = c.ID,
                        TimeSheetID = c.AssetTimeSheetID,
                        StatusOfAssetTimeSheetID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        TOMasterID = c.FLM_AssetTimeSheet.ReferID,
                        StatusID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        StatusName = c.FLM_AssetTimeSheet.SYS_Var.ValueOfVar,
                        IsReject = c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID == iReject ? true : false,
                        TypeOfTimeSheetID = c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID,
                        TypeOfTimeSheet = c.FLM_AssetTimeSheet.SYS_Var1.ValueOfVar,
                        GPSCode = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.GPSCode,
                        RegNo = c.FLM_AssetTimeSheet.FLM_Asset.VehicleID.HasValue ? c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.RegNo : string.Empty,
                    }).ToList();

                    foreach (var item in result)
                    {
                        if (item.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster)
                        {
                            //lst ton cbm
                            var lsttc = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => new { c.Ton, c.CBM }).ToList();

                            item.Ton = lsttc.Sum(c => c.Ton);
                            item.CBM = lsttc.Sum(c => c.CBM);
                            
                            var qrMaster=model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.TOMasterID);
                            item.MasterCode = qrMaster.Code;
                            if (qrMaster.ATD.HasValue)
                                item.DateFrom = qrMaster.ATD.Value;

                        }
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<CATFile> FLMMobileDriver_FileList(CATTypeOfFileCode code, int id)
        {
            try
            {
                List<CATFile> result = new List<CATFile>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_File.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfFileID == (int)code && c.ReferID == id && !c.IsDelete).Select(c => new CATFile
                    {
                        ID = c.ID,
                        FileName = c.FileName,
                        FileExt = c.FileExt,
                        FilePath = c.FilePath,
                        ReferID = c.ReferID,
                        TypeOfFileID = c.TypeOfFileID
                    }).OrderByDescending(c=>c.ID).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void FLMMobileDriver_UpdateGPS(string vehicleCode, float lat, float lng)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    string str = vehicleCode.ToLower().Trim();
                    var qr = model.CAT_Vehicle.FirstOrDefault(c => c.RegNo.ToLower().Trim() == str);
                    if (qr != null)
                    {
                        qr.Lat = lat;
                        qr.Lng = lng;
                        qr.ModifiedBy = Account.UserName;
                        qr.ModifiedDate = DateTime.Now;
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOMobile_TOMaster FLMMobile_MasterGetActualTime(int masterID)
        {
            try
            {
                DTOMobile_TOMaster result = new DTOMobile_TOMaster();
                using (var model = new DataEntities())
                {
                    result = model.OPS_DITOMaster.Where(c => c.ID == masterID).Select(c => new DTOMobile_TOMaster
                    {
                        ATA = c.ATA == null ? DateTime.Now : c.ATA.Value,
                        ATD = c.ATD != null ? c.ATD.Value : c.ETD != null ? c.ETD.Value : DateTime.Now,
                    }).FirstOrDefault();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOFLMDriverScheduleMobile FLMMobileSchedule_Get(int timeSheetDriverID)
        {
            try
            {
                DTOFLMDriverScheduleMobile result = new DTOFLMDriverScheduleMobile();
                using (var model = new DataEntities())
                {
                    var qr = model.FLM_AssetTimeSheetDriver.Where(c => c.ID == timeSheetDriverID).Select(c => new DTOFLMDriverScheduleMobile
                    {
                        DateFrom = c.FLM_AssetTimeSheet.DateFrom,
                        DateTo = c.FLM_AssetTimeSheet.DateTo,
                        AssetID = c.FLM_AssetTimeSheet.AssetID,
                        TimeSheetDriverID = c.ID,
                        TimeSheetID = c.AssetTimeSheetID,
                        StatusOfAssetTimeSheetID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        TOMasterID = c.FLM_AssetTimeSheet.ReferID,
                        StatusID = c.FLM_AssetTimeSheet.StatusOfAssetTimeSheetID,
                        StatusName = c.FLM_AssetTimeSheet.SYS_Var.ValueOfVar,

                        TypeOfTimeSheetID = c.FLM_AssetTimeSheet.TypeOfAssetTimeSheetID,
                        TypeOfTimeSheet = c.FLM_AssetTimeSheet.SYS_Var1.ValueOfVar,
                        GPSCode = c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.GPSCode,
                        RegNo = c.FLM_AssetTimeSheet.FLM_Asset.VehicleID.HasValue ? c.FLM_AssetTimeSheet.FLM_Asset.CAT_Vehicle.RegNo : string.Empty,
                        VehicleID = c.FLM_AssetTimeSheet.FLM_Asset.VehicleID.HasValue ? c.FLM_AssetTimeSheet.FLM_Asset.VehicleID.Value : 0,
                    }).FirstOrDefault();
                    if (qr != null)
                    {
                        result = TimerSheet_GetTOMaster(model, qr);
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #region temp

        public void FLMMobileDriver_UpdateTemp(int timeSheetID, int locationID,double temp)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var qrTimeSheet = model.FLM_AssetTimeSheet.Where(c => c.ID == timeSheetID).Select(c =>new { c.ReferID,c.StatusOfAssetTimeSheetID}).FirstOrDefault();
                    if (qrTimeSheet.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster)
                    {
                        var qrMasterLocation = model.OPS_DITOLocation.Where(c => c.DITOMasterID == qrTimeSheet.ReferID && c.LocationID == locationID).FirstOrDefault();
                        if (qrMasterLocation != null)
                        {
                            qrMasterLocation.Temp = temp;
                            model.SaveChanges();
                        }
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion

        #region Trouble
        public int FLMMobile_TroubleSave(DTOCATTrouble item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.CAT_Trouble.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new CAT_Trouble();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.TroubleCostStatusID = -(int)SYSVarType.TroubleCostStatusOpen;
                        model.CAT_Trouble.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Lat = item.Lat;
                    obj.Lng = item.Lng;
                    obj.GroupOfTroubleID = item.GroupOfTroubleID;
                    obj.DITOID = item.DITOID > 0 ? item.DITOID : null;
                    obj.DITOMasterID = item.DITOMasterID;
                    obj.COTOID = item.COTOID > 0 ? item.COTOID : null;
                    obj.COTOMasterID = item.COTOMasterID;
                    obj.Description = item.Description;
                    obj.AttachmentFile = item.AttachmentFile == null ? string.Empty : item.AttachmentFile;
                    obj.Cost = item.Cost;
                    obj.CostOfCustomer = item.CostOfCustomer;
                    obj.CostOfVendor = item.Cost;
                    model.SaveChanges();
                    item.ID = obj.ID;

                    obj.CreatedDate = DateTime.Now.AddSeconds(5);
                    model.SaveChanges();
                }
                return item.ID;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<CATGroupOfTrouble> FLMMobile_GroupTroubleList(bool isCO)
        {
            try
            {
                List<CATGroupOfTrouble> result = new List<CATGroupOfTrouble>();
                using (var model = new DataEntities())
                {
                    int typeID = -(int)SYSVarType.TypeOfGroupTroubleDI;
                    if (isCO)
                        typeID = -(int)SYSVarType.TypeOfGroupTroubleCO;
                    result = model.CAT_GroupOfTrouble.Where(c => c.TypeOfGroupTroubleID == typeID).Select(c => new CATGroupOfTrouble
                    {
                        ID = c.ID,
                        Code = c.Code,
                        Name = c.Name
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOCATTrouble> FLMMobileDriver_TroubleList(int masterID, bool isCo)
        {
            try
            {
                List<DTOCATTrouble> result = new List<DTOCATTrouble>();
                var typeofTrouble = -(int)SYSVarType.TypeOfGroupTroubleDI;
                if (isCo)
                    typeofTrouble = -(int)SYSVarType.TypeOfGroupTroubleCO;
                using (var model = new DataEntities())
                {
                    result = model.CAT_Trouble.Where(c => c.CAT_GroupOfTrouble.TypeOfGroupTroubleID == typeofTrouble && ((c.DITOMasterID == masterID && !isCo) || (c.COTOMasterID == masterID && isCo))).Select(c => new DTOCATTrouble
                    {
                        ID = c.ID,
                        GroupOfTroubleID = c.GroupOfTroubleID,
                        GroupOfTroubleName = c.CAT_GroupOfTrouble.Name,
                        DITOID = c.DITOID,
                        DITOMasterID = c.DITOMasterID,
                        COTOID = c.COTOID,
                        COTOMasterID = c.COTOMasterID,
                        Description = c.Description,
                        AttachmentFile = c.AttachmentFile,
                        Cost = c.Cost,
                        CostOfCustomer = c.CostOfCustomer,
                        CostOfVendor = c.CostOfVendor,
                        TroubleCostStatusID = c.TroubleCostStatusID,
                        TroubleCostStatusName = c.SYS_Var.ValueOfVar == null ? string.Empty : c.SYS_Var.ValueOfVar,
                        RoutingName = c.DITOID > 0 ? c.OPS_DITO.CAT_Routing.CAT_Location.Location + " - " + c.OPS_DITO.CAT_Routing.CAT_Location1.Location : string.Empty,
                    }).ToList();

                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region Problem
        public List<DTOMobileRouteProblem> FLMMobileDriver_ProblemList()
        {
            try
            {
                List<DTOMobileRouteProblem> result = new List<DTOMobileRouteProblem>();
                using (var model = new DataEntities())
                {
                    result = model.OPS_RouteProblem.Where(c => c.DateEnd > DateTime.Now).Select(c => new DTOMobileRouteProblem
                    {
                        ID = c.ID,
                        DriverID = c.DriverID,
                        Lat = c.Lat,
                        Lng = c.Lng,
                        VehicleID = c.VehicleID
                    }).ToList();

                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void FLMMobileDriver_ProblemSave(DTOMobileRouteProblem item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var qr = model.OPS_RouteProblem.FirstOrDefault(c => c.ID == item.ID);

                    if (qr == null)
                    {
                        qr = new OPS_RouteProblem();
                        qr.CreatedBy = Account.UserName;
                        qr.CreatedDate = DateTime.Now;
                        qr.DriverID = Account.DriverID;
                        qr.DateStart = item.DateStart;
                        qr.DateEnd = item.DateEnd;
                        qr.TypeOfRouteProblemID = item.TypeOfRouteProblemID;
                        qr.Lat = item.Lat;
                        qr.Lng = item.Lng;
                        qr.VehicleID = item.VehicleID;
                        model.OPS_RouteProblem.Add(qr);
                        model.SaveChanges();
                        qr.ModifiedBy = Account.UserName;
                        qr.ModifiedDate = DateTime.Now;
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobileProblemType> FLMMobileDriver_ProblemTypeList()
        {
            try
            {
                List<DTOMobileProblemType> result = new List<DTOMobileProblemType>();
                using (var model = new DataEntities())
                {
                    result = model.OPS_TypeOfRouteProblem.Select(c => new DTOMobileProblemType
                    {
                        ID = c.ID,
                        Code = c.Code,
                        TypeName = c.TypeName
                    }).ToList();

                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion

        #region Salary
        public List<DTOMobileSalary> FLMMobileDriverSalary_List(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                dtfrom = dtfrom.Date;
                dtto = dtto.Date;
                List<DTOMobileSalary> result = new List<DTOMobileSalary>();
                using (var model = new DataEntities())
                {
                    //result = model.FIN_PLDetails.Where(c => !c.FIN_PL.IsPlanning && c.FIN_PL.DITOMasterID.HasValue && c.FIN_PL.DriverID == Account.DriverID && c.FIN_PL.Effdate >= dtfrom && c.FIN_PL.Effdate <= dtto && c.FIN_PL.SYSCustomerID == Account.SYSCustomerID && (c.CostID == (int)CATCostType.PLDriverFeeBase || c.CostID == (int)CATCostType.PLDriverFeeIncome || c.CostID == (int)CATCostType.PLDriverFeePlus || c.CostID == (int)CATCostType.PLDriverFeeRoute)).Select(c => new DTOMobileSalary
                    //    {
                    //        TOMasterID = c.FIN_PL.DITOMasterID.Value,
                    //        TOMasterCode = c.FIN_PL.OPS_DITOMaster.Code,
                    //        DateConfig = c.FIN_PL.Effdate,
                    //        CostName = c.CAT_Cost.CostName,
                    //        Price = c.Debit,
                    //    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion

        #region Return

        public List<DTOMobileOrderReturn> Mobile_GOPReturnList(int masterID, int locationID)
        {
            try
            {
                var result = new List<DTOMobileOrderReturn>();
                using (var model = new DataEntities())
                {

                    result = model.OPS_DITOProduct.Where(c => c.OPS_DITOGroupProduct.DITOMasterID == masterID && c.OPS_DITOGroupProduct.ORD_GroupProduct.IsReturn == true && c.ORD_Product.ORD_GroupProduct.CUS_Location1.LocationID == locationID).Select(c => new DTOMobileOrderReturn
                    {
                        ID = c.ID,
                        OrderCode = c.ORD_Product.ORD_GroupProduct.ORD_Order.Code,
                        GroupProductID = c.ORD_Product.ORD_GroupProduct.GroupOfProductID.Value,
                        GroupProductName = c.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                        ProductID = c.ORD_Product.ProductID,
                        ProductName = c.ORD_Product.CUS_Product.ProductName,
                        Quantity = c.Quantity,
                        CustomerID = c.ORD_Product.ORD_GroupProduct.ORD_Order.CustomerID,
                        Note = c.OPS_DITOGroupProduct.Note1
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<CUSGroupOfProduct> Mobiler_CUSGOPListNotin(int masterID, int locationID)
        {
            try
            {
                var result = new List<CUSGroupOfProduct>();
                using (var model = new DataEntities())
                {
                    var lstGOPInUse = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == masterID && c.ORD_GroupProduct.IsReturn == true).Select(c => c.ORD_GroupProduct.GroupOfProductID.Value).ToList();
                    var lstCusID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == masterID && c.ORD_GroupProduct.CUS_Location1.LocationID == locationID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).ToList();
                    result = model.CUS_GroupOfProduct.Where(c => lstCusID.Contains(c.CustomerID) && !lstGOPInUse.Contains(c.ID) && c.HasReturn == true).Select(c => new CUSGroupOfProduct
                    {
                        ID = c.ID,
                        GroupName = c.GroupName,
                        Code = c.Code,
                    }).ToList();

                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void Mobile_GOPReturnSave(DTOMONProductReturn item)
        {
            try
            {
                var result = new DTOResult();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var qrOrdGP = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == item.OrderGroupID);//qr SO cua order
                    var qrMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.MasterID);// qr master
                    var CusGP = model.CUS_GroupOfProduct.FirstOrDefault(c => c.ID == item.GroupProductID);
                    if (qrOrdGP != null && qrMaster != null)
                    {

                        var qrOPSProductReturn = model.OPS_DITOProduct.FirstOrDefault(c => c.ORD_Product.ORD_GroupProduct.ReturnID == item.OrderGroupID && c.ORD_Product.ProductID == item.ProductID && c.OPS_DITOGroupProduct.DITOMasterID == item.MasterID);
                        if (qrOPSProductReturn == null)
                        {
                            #region tao moi ORD_GroupProduct
                            var qrORDGroupProductReturn = new ORD_GroupProduct();
                            qrORDGroupProductReturn.OrderID = qrOrdGP.OrderID;
                            qrORDGroupProductReturn.ContainerID = qrOrdGP.ContainerID;
                            qrORDGroupProductReturn.GroupOfProductID = item.GroupProductID;
                            qrORDGroupProductReturn.Description = qrOrdGP.Description;
                            qrORDGroupProductReturn.SOCode = string.Empty;
                            qrORDGroupProductReturn.PriceOfGOPID = CusGP.PriceOfGOPID;
                            qrORDGroupProductReturn.PackingID = qrOrdGP.PackingID;
                            qrORDGroupProductReturn.LocationFromID = qrOrdGP.LocationFromID;
                            qrORDGroupProductReturn.LocationToID = qrOrdGP.LocationToID;
                            qrORDGroupProductReturn.DateConfig = qrOrdGP.DateConfig;
                            qrORDGroupProductReturn.ETD = qrOrdGP.ETD;
                            qrORDGroupProductReturn.ETA = qrOrdGP.ETA;
                            qrORDGroupProductReturn.Price = qrOrdGP.Price;
                            qrORDGroupProductReturn.CUSRoutingID = qrOrdGP.CUSRoutingID;
                            qrORDGroupProductReturn.CreatedDate = DateTime.Now;
                            qrORDGroupProductReturn.CreatedBy = Account.UserName;
                            qrORDGroupProductReturn.DNCode = qrOrdGP.DNCode;
                            qrORDGroupProductReturn.ETARequest = qrOrdGP.ETARequest;
                            qrORDGroupProductReturn.PartnerID = qrOrdGP.PartnerID;
                            qrORDGroupProductReturn.IsReturn = true;
                            qrORDGroupProductReturn.ReturnID = item.OrderGroupID;
                            qrORDGroupProductReturn.Ton = 0;
                            qrORDGroupProductReturn.CBM = 0;
                            qrORDGroupProductReturn.Quantity = 0;
                            model.ORD_GroupProduct.Add(qrORDGroupProductReturn);
                            #endregion

                            #region them ORD_Product
                            var qrORDProductReturn = new ORD_Product();
                            qrORDProductReturn.ORD_GroupProduct = qrORDGroupProductReturn;
                            qrORDProductReturn.CreatedBy = Account.UserName;
                            qrORDProductReturn.CreatedDate = DateTime.Now;
                            qrORDProductReturn.ProductID = item.ProductID;
                            model.ORD_Product.Add(qrORDProductReturn);
                            #endregion

                            qrORDProductReturn.Quantity = item.Quantity;

                            QuyDoiTanKhoi(model, Account, qrORDProductReturn);

                            //ops
                            var qrOpsGP = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.DITOMasterID == item.MasterID && c.OrderGroupProductID == item.OrderGroupID);
                            if (qrOpsGP != null)
                            {

                                #region tao moi OPS_DITOGroupProduct
                                var qrOPSGroupProductReturn = new OPS_DITOGroupProduct();
                                qrOPSGroupProductReturn.ORD_GroupProduct = qrORDGroupProductReturn;
                                qrOPSGroupProductReturn.DITOMasterID = item.MasterID;
                                qrOPSGroupProductReturn.CBM = qrORDGroupProductReturn.CBM;
                                qrOPSGroupProductReturn.Ton = qrORDGroupProductReturn.Ton;
                                qrOPSGroupProductReturn.Quantity = qrORDGroupProductReturn.Quantity;
                                qrOPSGroupProductReturn.CBMTranfer = qrORDGroupProductReturn.CBM;
                                qrOPSGroupProductReturn.TonTranfer = qrORDGroupProductReturn.Ton;
                                qrOPSGroupProductReturn.QuantityTranfer = qrORDGroupProductReturn.Quantity;
                                qrOPSGroupProductReturn.CBMBBGN = qrORDGroupProductReturn.CBM;
                                qrOPSGroupProductReturn.QuantityBBGN = qrORDGroupProductReturn.Quantity;
                                qrOPSGroupProductReturn.TonBBGN = qrORDGroupProductReturn.Ton;
                                qrOPSGroupProductReturn.QuantityLoading = qrORDGroupProductReturn.Quantity;
                                qrOPSGroupProductReturn.DNCode = string.Empty;
                                qrOPSGroupProductReturn.QuantityLoading = 0;
                                qrOPSGroupProductReturn.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                                qrOPSGroupProductReturn.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;
                                qrOPSGroupProductReturn.CreatedDate = DateTime.Now;
                                qrOPSGroupProductReturn.CreatedBy = Account.UserName;
                                qrOPSGroupProductReturn.IsOrigin = qrOpsGP.IsOrigin;
                                qrOPSGroupProductReturn.IsInput = qrOpsGP.IsInput;
                                qrOPSGroupProductReturn.GroupSort = qrOpsGP.GroupSort;
                                qrOPSGroupProductReturn.DateFromCome = qrOpsGP.DateFromCome;
                                qrOPSGroupProductReturn.DateFromLeave = qrOpsGP.DateFromLeave;
                                qrOPSGroupProductReturn.DateFromLoadEnd = qrOpsGP.DateFromLoadEnd;
                                qrOPSGroupProductReturn.DateFromLoadStart = qrOpsGP.DateFromLoadStart;
                                qrOPSGroupProductReturn.DateToCome = qrOpsGP.DateToCome;
                                qrOPSGroupProductReturn.DateToLeave = qrOpsGP.DateToLeave;
                                qrOPSGroupProductReturn.DateToLoadEnd = qrOpsGP.DateToLoadEnd;
                                qrOPSGroupProductReturn.DateToLoadStart = qrOpsGP.DateToLoadStart;
                                qrOPSGroupProductReturn.Note = string.Empty;
                                qrOPSGroupProductReturn.Note1 = item.Note;
                                qrOPSGroupProductReturn.Note2 = string.Empty;
                                qrOPSGroupProductReturn.InvoiceBy = qrOpsGP.InvoiceBy;
                                qrOPSGroupProductReturn.InvoiceDate = qrOpsGP.InvoiceDate;
                                qrOPSGroupProductReturn.InvoiceNote = qrOpsGP.InvoiceNote;
                                qrOPSGroupProductReturn.DateDN = qrOpsGP.DateDN;

                                model.OPS_DITOGroupProduct.Add(qrOPSGroupProductReturn);


                                #endregion

                                #region tao moi OPS_Product
                                qrOPSProductReturn = new OPS_DITOProduct();
                                qrOPSProductReturn.CreatedDate = DateTime.Now;
                                qrOPSProductReturn.CreatedBy = Account.UserName;
                                qrOPSProductReturn.OPS_DITOGroupProduct = qrOPSGroupProductReturn;
                                qrOPSProductReturn.ORD_Product = qrORDProductReturn;
                                qrOPSProductReturn.Note = item.Note;
                                model.OPS_DITOProduct.Add(qrOPSProductReturn);
                                #endregion

                                qrOPSProductReturn.Quantity = qrORDProductReturn.Quantity;
                                qrOPSProductReturn.QuantityTranfer = qrORDProductReturn.Quantity;
                                qrOPSProductReturn.QuantityBBGN = qrORDProductReturn.Quantity;
                                qrOPSProductReturn.QuantityReturn = qrORDProductReturn.Quantity;
                            }
                        }
                        else
                        {
                            throw FaultHelper.BusinessFault(null, null, "Hàng trả về này đã có ,chọn hàng trả về khác");
                        }
                    }
                    else
                    {
                        throw FaultHelper.BusinessFault(null, null, "SO không tồn tại");
                    }
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void Mobile_GOPReturnEdit(int id, double quantity)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var qr = model.OPS_DITOProduct.FirstOrDefault(c => c.ID == id);
                    if (qr != null)
                    {
                        qr.ModifiedBy = Account.UserName;
                        qr.ModifiedDate = DateTime.Now;
                        qr.Quantity = quantity;
                        qr.QuantityTranfer = quantity;
                        qr.QuantityBBGN = quantity;
                        qr.QuantityReturn = quantity;
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOCombobox> Mobile_DITOGroupProductList(int masterID, int locationID)
        {
            try
            {
                var result = new List<DTOCombobox>();
                using (var model = new DataEntities())
                {
                    var qr = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == masterID && c.ORD_GroupProduct.IsReturn != true && c.ORD_GroupProduct.CUS_Location1.LocationID == locationID).Select(c => new
                    {
                        Text = "(" + c.ORD_GroupProduct.ORD_Order.Code + ")" + c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                        ValueInt = c.OrderGroupProductID.Value
                    }).Distinct().ToList();
                    result = qr.Select(c => new DTOCombobox
                    {
                        Text = c.Text,
                        ValueInt = c.ValueInt
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOCUSProduct> Mobile_CUSProductList(int masterID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    List<DTOCUSProduct> result = new List<DTOCUSProduct>();
                    //list customer
                    var qrmaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == masterID);
                    List<int> lstCus = new List<int>();
                    if (qrmaster != null)
                    {
                        lstCus = qrmaster.OPS_DITOGroupProduct.Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                    }
                    //list gop return from list customer
                    List<CUSGroupOfProduct> lstReturnGOP = new List<CUSGroupOfProduct>();
                    foreach (var o in lstCus)
                    {
                        result.AddRange(model.CUS_Product.Where(c => c.CUS_GroupOfProduct.CustomerID == o && c.CUS_GroupOfProduct.HasReturn == true).Select(c => new DTOCUSProduct
                        {
                            ID = c.ID,
                            ProductName = c.ProductName,
                            Code = c.Code,
                            GroupOfProductID = c.GroupOfProductID,
                        }).ToList());
                    }
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOCombobox> Mobile_CUSGOPList(int masterID)
        {
            try
            {
                var result = new List<DTOCombobox>();
                using (var model = new DataEntities())
                {
                    //list customer
                    var qrmaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == masterID);
                    List<int> lstCus = new List<int>();
                    if (qrmaster != null)
                    {
                        lstCus = qrmaster.OPS_DITOGroupProduct.Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                    }
                    result = model.CUS_GroupOfProduct.Where(c => lstCus.Contains(c.CustomerID) && c.HasReturn == true && c.CUS_Product.Count > 0).Select(c => new DTOCombobox
                    {
                        ValueInt = c.ID,
                        Text = c.GroupName,
                    }).ToList();

                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion

        #region SL SO

        public void Mobile_SL_Save(DTOMobileSODetail item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var qrOPSProduct = model.OPS_DITOProduct.FirstOrDefault(c => c.ID == item.ID);
                    var qrOPSGroupProduct = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == qrOPSProduct.DITOGroupProductID);
                    if (qrOPSProduct != null)
                    {
                        //cap nhat GoP
                        qrOPSGroupProduct.ModifiedBy = Account.UserName;
                        qrOPSGroupProduct.ModifiedDate = DateTime.Now;
                        qrOPSGroupProduct.Note = item.Note;
                        //cap nhat product
                        qrOPSProduct.ModifiedBy = Account.UserName;
                        qrOPSProduct.ModifiedDate = DateTime.Now;
                        qrOPSProduct.QuantityReturn = item.QuantityReturn;
                        qrOPSProduct.QuantityTranfer = item.QuantityTranfer;
                        qrOPSProduct.Note = item.Note;

                        model.SaveChanges();

                        //tinh lai cho OPS_DITOGroupProduct
                        var lstProduct = model.OPS_DITOProduct.Where(c => c.DITOGroupProductID == qrOPSProduct.DITOGroupProductID);

                        //tinh lai cho OPS_DITOGroupProduct transfer

                        if (lstProduct.Where(c => c.ORD_Product.ExchangeTon.HasValue).Count() > 0)
                        {
                            qrOPSProduct.OPS_DITOGroupProduct.TonTranfer = lstProduct.Sum(c => c.QuantityTranfer * c.ORD_Product.ExchangeTon.Value);
                        }
                        if (lstProduct.Where(c => c.ORD_Product.ExchangeCBM.HasValue).Count() > 0)
                        {
                            qrOPSProduct.OPS_DITOGroupProduct.CBMTranfer = lstProduct.Sum(c => c.QuantityTranfer * c.ORD_Product.ExchangeCBM.Value);
                        }
                        if (lstProduct.Where(c => c.ORD_Product.ExchangeQuantity.HasValue).Count() > 0)
                        {
                            qrOPSProduct.OPS_DITOGroupProduct.QuantityTranfer = lstProduct.Sum(c => c.QuantityTranfer * c.ORD_Product.ExchangeQuantity.Value);
                        }

                        //tinh lai cho OPS_DITOGroupProduct Return

                        if (lstProduct.Where(c => c.ORD_Product.ExchangeTon.HasValue).Count() > 0)
                        {
                            qrOPSProduct.OPS_DITOGroupProduct.TonReturn = lstProduct.Sum(c => c.QuantityReturn * c.ORD_Product.ExchangeTon.Value);
                        }
                        if (lstProduct.Where(c => c.ORD_Product.ExchangeCBM.HasValue).Count() > 0)
                        {
                            qrOPSProduct.OPS_DITOGroupProduct.CBMReturn = lstProduct.Sum(c => c.QuantityReturn * c.ORD_Product.ExchangeCBM.Value);
                        }
                        if (lstProduct.Where(c => c.ORD_Product.ExchangeQuantity.HasValue).Count() > 0)
                        {
                            qrOPSProduct.OPS_DITOGroupProduct.QuantityReturn = lstProduct.Sum(c => c.QuantityReturn * c.ORD_Product.ExchangeQuantity.Value);
                        }
                    }
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMON_CUSProduct> Mobile_GroupProductOfTOGroup(int TOGroupID)
        {
            try
            {
                var result = new List<DTOMON_CUSProduct>();
                using (var model = new DataEntities())
                {
                    var qrTOGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == TOGroupID);
                    if (qrTOGroup == null)
                        throw FaultHelper.BusinessFault(null, null, "DN không tồn tại");
                    var qrCusGP = model.CUS_GroupOfProductInStock.Where(c => c.StockID == qrTOGroup.ORD_GroupProduct.LocationFromID).Select(c => c.GroupOfProductID).Distinct().ToList();
                    var lstProductID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == qrTOGroup.DITOMasterID && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel).Select(c => c.ORD_GroupProduct.ORD_Product.FirstOrDefault().ProductID);
                    result = model.CUS_Product.Where(c => !lstProductID.Contains(c.ID) && qrCusGP.Contains(c.GroupOfProductID)).Select(c => new DTOMON_CUSProduct
                    {
                        ID = c.ID,
                        ProductCode = c.Code,
                        ProductName = c.ProductName,
                        GroupProductID = c.GroupOfProductID,
                        GroupProductName = c.CUS_GroupOfProduct.GroupName,
                        GroupProductCode = c.CUS_GroupOfProduct.Code,
                    }).ToList();

                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void Mobile_AddGroupProductFromDN(int opsGroupID, DTOMON_CUSProduct item)
        {
            try
            {
                using (var model = new DataEntities())
                {

                    model.EventAccount = Account; model.EventRunning = false;
                    var qrTOGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == opsGroupID);
                    if (qrTOGroup == null)
                        throw FaultHelper.BusinessFault(null, null, "DN không tồn tại");
                    var qrProduct = model.CUS_Product.FirstOrDefault(c => c.ID == item.ID);
                    if (qrProduct == null)
                        throw FaultHelper.BusinessFault(null, null, "Sản phẩm không tồn tại");

                    #region tao moi ORD

                    var oldORDGroup = qrTOGroup.ORD_GroupProduct;

                    //tao moi ORD_GroupProduct
                    var ordGP = new ORD_GroupProduct();
                    ordGP.OrderID = oldORDGroup.OrderID;
                    ordGP.ContainerID = oldORDGroup.ContainerID;
                    ordGP.GroupOfProductID = item.GroupProductID;
                    ordGP.Description = oldORDGroup.Description;
                    ordGP.SOCode = string.Empty;
                    ordGP.PriceOfGOPID = qrProduct.CUS_GroupOfProduct.PriceOfGOPID;
                    ordGP.PackingID = qrProduct.PackingID;
                    ordGP.LocationFromID = oldORDGroup.LocationFromID;
                    ordGP.LocationToID = oldORDGroup.LocationToID;
                    ordGP.DateConfig = oldORDGroup.DateConfig;
                    ordGP.ETD = oldORDGroup.ETD;
                    ordGP.ETA = oldORDGroup.ETA;
                    ordGP.Price = item.Price;
                    ordGP.CUSRoutingID = oldORDGroup.CUSRoutingID;
                    ordGP.CreatedDate = DateTime.Now;
                    ordGP.CreatedBy = Account.UserName;
                    ordGP.DNCode = oldORDGroup.DNCode;
                    ordGP.ETARequest = oldORDGroup.ETARequest;
                    ordGP.PartnerID = oldORDGroup.PartnerID;
                    ordGP.IsReturn = false;
                    ordGP.Ton = 0;
                    ordGP.CBM = 0;
                    ordGP.Quantity = 0;
                    model.ORD_GroupProduct.Add(ordGP);


                    // them ORD_Product
                    var ordP = new ORD_Product();
                    ordP.ORD_GroupProduct = ordGP;
                    ordP.CreatedBy = Account.UserName;
                    ordP.CreatedDate = DateTime.Now;
                    ordP.ProductID = item.ID;
                    ordP.Quantity = item.Quantity;
                    model.ORD_Product.Add(ordP);


                    #region qui doi tan khoi
                    var cusGroup = model.CUS_GroupOfProduct.FirstOrDefault(c => c.ID == item.GroupProductID);
                    if (cusGroup != null)
                    {
                        var cusproduct = qrProduct;
                        if (cusproduct != null)
                        {
                            if (cusproduct.CAT_Packing.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                            {
                                ordP.ExchangeTon = 1;
                                ordP.ExchangeCBM = 0;
                                if (cusproduct.Weight.HasValue && cusproduct.Weight.Value > 0 && cusproduct.CBM.HasValue)
                                    ordP.ExchangeCBM = cusproduct.CBM.Value / cusproduct.Weight.Value;
                                ordP.ExchangeQuantity = 0;
                                ordGP.Ton += item.Quantity;
                                ordGP.CBM += item.Quantity * ordP.ExchangeCBM.Value;
                            }
                            if (cusproduct.CAT_Packing.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                            {
                                ordP.ExchangeCBM = 1;
                                ordP.ExchangeTon = 0;
                                if (cusproduct.Weight.HasValue && cusproduct.CBM.Value > 0 && cusproduct.CBM.HasValue)
                                    ordP.ExchangeTon = cusproduct.Weight.Value / cusproduct.CBM.Value;
                                ordP.ExchangeQuantity = 0;
                                ordGP.CBM += item.Quantity;
                                ordGP.Ton += item.Quantity * ordP.ExchangeTon.Value;
                            }
                            if (cusproduct.CAT_Packing.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                            {
                                ordP.ExchangeTon = cusproduct.Weight.HasValue ? cusproduct.Weight.Value : 0;
                                ordP.ExchangeCBM = cusproduct.CBM.HasValue ? cusproduct.CBM.Value : 0;
                                ordP.ExchangeQuantity = 1;
                                ordGP.CBM += item.Quantity * ordP.ExchangeCBM.Value;
                                ordGP.Ton += item.Quantity * ordP.ExchangeTon.Value;
                                ordGP.Quantity += item.Quantity;
                            }
                        }
                    }

                    #endregion

                    #endregion

                    var qrOpsGP = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == opsGroupID);
                    if (qrOpsGP != null)
                    {
                        #region tao moi OPS_DITOGroupProduct
                        var opsGP = new OPS_DITOGroupProduct();
                        opsGP.ORD_GroupProduct = ordGP;
                        opsGP.DITOMasterID = qrOpsGP.DITOMasterID;
                        opsGP.CBM = ordGP.CBM;
                        opsGP.Ton = ordGP.Ton;
                        opsGP.Quantity = ordGP.Quantity;
                        opsGP.CBMTranfer = ordGP.CBM;
                        opsGP.TonTranfer = ordGP.Ton;
                        opsGP.QuantityTranfer = ordGP.Quantity;
                        opsGP.CBMBBGN = ordGP.CBM;
                        opsGP.QuantityBBGN = ordGP.Quantity;
                        opsGP.TonBBGN = ordGP.Ton;
                        opsGP.QuantityLoading = ordGP.Quantity;
                        opsGP.DNCode = string.Empty;
                        opsGP.QuantityLoading = 0;
                        opsGP.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusWaiting;
                        opsGP.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODWait;
                        opsGP.CreatedDate = DateTime.Now;
                        opsGP.CreatedBy = Account.UserName;
                        opsGP.IsOrigin = qrOpsGP.IsOrigin;
                        opsGP.IsInput = qrOpsGP.IsInput;
                        opsGP.GroupSort = qrOpsGP.GroupSort;
                        opsGP.DateFromCome = qrOpsGP.DateFromCome;
                        opsGP.DateFromLeave = qrOpsGP.DateFromLeave;
                        opsGP.DateFromLoadEnd = qrOpsGP.DateFromLoadEnd;
                        opsGP.DateFromLoadStart = qrOpsGP.DateFromLoadStart;
                        opsGP.DateToCome = qrOpsGP.DateToCome;
                        opsGP.DateToLeave = qrOpsGP.DateToLeave;
                        opsGP.DateToLoadEnd = qrOpsGP.DateToLoadEnd;
                        opsGP.DateToLoadStart = qrOpsGP.DateToLoadStart;
                        opsGP.Note = string.Empty;
                        opsGP.Note1 = string.Empty;
                        opsGP.Note2 = item.Note;
                        opsGP.InvoiceBy = qrOpsGP.InvoiceBy;
                        opsGP.InvoiceDate = qrOpsGP.InvoiceDate;
                        opsGP.InvoiceNote = qrOpsGP.InvoiceNote;
                        opsGP.DateDN = qrOpsGP.DateDN;

                        model.OPS_DITOGroupProduct.Add(opsGP);


                        #endregion

                        #region tao moi OPS_Product

                        var opsProduct = new OPS_DITOProduct();
                        opsProduct.CreatedDate = DateTime.Now;
                        opsProduct.CreatedBy = Account.UserName;
                        opsProduct.OPS_DITOGroupProduct = opsGP;
                        opsProduct.ORD_Product = ordP;
                        opsProduct.Quantity = ordP.Quantity;
                        opsProduct.QuantityTranfer = ordP.Quantity;
                        opsProduct.QuantityBBGN = 0;
                        opsProduct.QuantityReturn = 0;
                        model.OPS_DITOProduct.Add(opsProduct);
                        #endregion
                    }
                    model.SaveChanges();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        private void QuyDoiTanKhoi(DataEntities model, AccountItem Account, ORD_Product qrORDProduct)
        {
            var qrORDGroupProduct = qrORDProduct.ORD_GroupProduct;
            var cusproduct = model.CUS_Product.FirstOrDefault(c => c.ID == qrORDProduct.ProductID);
            if (cusproduct != null)
            {
                qrORDProduct.PackingID = cusproduct.PackingID;
                if (cusproduct.CAT_Packing.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                {
                    qrORDProduct.ExchangeTon = 1;
                    qrORDProduct.ExchangeCBM = 0;
                    qrORDProduct.ExchangeQuantity = 0;
                    if (cusproduct.Weight.HasValue && cusproduct.Weight.Value > 0 && cusproduct.CBM.HasValue)
                    {
                        qrORDProduct.ExchangeCBM = cusproduct.CBM.Value / cusproduct.Weight.Value;
                        qrORDProduct.ExchangeQuantity = 1 / cusproduct.Weight.Value;
                    }
                    qrORDGroupProduct.Ton = qrORDGroupProduct.ORD_Product.Sum(c=>c.Quantity);
                    qrORDGroupProduct.CBM = qrORDGroupProduct.ORD_Product.Sum(c => c.Quantity * qrORDProduct.ExchangeCBM.Value);
                    qrORDGroupProduct.Quantity = qrORDGroupProduct.ORD_Product.Sum(c => c.Quantity * qrORDProduct.ExchangeQuantity.Value);
                }
                if (cusproduct.CAT_Packing.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                {
                    qrORDProduct.ExchangeCBM = 1;
                    qrORDProduct.ExchangeTon = 0;
                    qrORDProduct.ExchangeQuantity = 0;
                    if (cusproduct.Weight.HasValue && cusproduct.CBM.Value > 0 && cusproduct.CBM.HasValue)
                    {
                        qrORDProduct.ExchangeTon = cusproduct.Weight.Value / cusproduct.CBM.Value;
                        qrORDProduct.ExchangeQuantity = 1 / cusproduct.CBM.Value;
                    }
                    qrORDGroupProduct.CBM = qrORDGroupProduct.ORD_Product.Sum(c => c.Quantity);
                    qrORDGroupProduct.Ton = qrORDGroupProduct.ORD_Product.Sum(c => c.Quantity * qrORDProduct.ExchangeTon.Value);
                    qrORDGroupProduct.Quantity = qrORDGroupProduct.ORD_Product.Sum(c => c.Quantity * qrORDProduct.ExchangeQuantity.Value);
                }
                if (cusproduct.CAT_Packing.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                {
                    qrORDProduct.ExchangeTon = cusproduct.Weight.HasValue ? cusproduct.Weight.Value : 0;
                    qrORDProduct.ExchangeCBM = cusproduct.CBM.HasValue ? cusproduct.CBM.Value : 0;
                    qrORDProduct.ExchangeQuantity = 1;
                    qrORDGroupProduct.CBM = qrORDGroupProduct.ORD_Product.Sum(c => c.Quantity * qrORDProduct.ExchangeCBM.Value);
                    qrORDGroupProduct.Ton = qrORDGroupProduct.ORD_Product.Sum(c => c.Quantity * qrORDProduct.ExchangeTon.Value);
                    qrORDGroupProduct.Quantity = qrORDGroupProduct.ORD_Product.Sum(c => c.Quantity);
                }
                model.SaveChanges();
            }

        }

        #endregion

        #region check in location

        public List<DTOMobilePartnerLocation> FLMMobileDriver_PartnerList(int type)
        {
            try
            {
                var typeofPartner = -(int)SYSVarType.TypeOfPartnerStation;
                switch (type)
                {
                    case 1:
                        typeofPartner = -(int)SYSVarType.TypeOfPartnerDistributor;
                        break;
                    default:
                        typeofPartner = -(int)SYSVarType.TypeOfPartnerStation;
                        break;
                }
                List<DTOMobilePartnerLocation> result = new List<DTOMobilePartnerLocation>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_PartnerLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == typeofPartner && !c.CAT_Location.IsChecked).Select(c => new DTOMobilePartnerLocation
                    {
                        ID = c.ID,
                        LocationID = c.LocationID,
                        LocationName = c.CAT_Location.Code,
                        PartnerID = c.PartnerID,
                        PartnerCode = c.CAT_Partner.Code,
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobilePartnerLocation> FLMMobileDriver_PartnerLocationList(int partnerID)
        {
            try
            {
                List<DTOMobilePartnerLocation> result = new List<DTOMobilePartnerLocation>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_PartnerLocation.Where(c => !c.CAT_Location.IsChecked && c.PartnerID == partnerID).Select(c => new DTOMobilePartnerLocation
                    {
                        ID = c.ID,
                        LocationID = c.LocationID,
                        PartnerID = c.PartnerID,
                        PartnerCode = c.CAT_Partner.Code,
                        Lat = c.CAT_Location.Lat,
                        Lng = c.CAT_Location.Lng
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void FLMMobileDriver_CheckInLocation(DTOMobilePartnerLocation item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var qrPartnerLocation = model.CAT_PartnerLocation.FirstOrDefault(c => c.ID == item.ID);
                    if (qrPartnerLocation != null)
                    {
                        qrPartnerLocation.ModifiedBy = Account.UserName;
                        qrPartnerLocation.ModifiedDate = DateTime.Now;
                        qrPartnerLocation.CAT_Location.Lat = item.Lat;
                        qrPartnerLocation.CAT_Location.Lng = item.Lng;
                        qrPartnerLocation.CAT_Location.IsChecked = true;
                        model.SaveChanges();
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion

        #region Station truck

        public List<DTOMobile_Station> FLMMobileDriver_Stationlist(int masterID)
        {
            try
            {
                var result = new List<DTOMobile_Station>();
                using (var model = new DataEntities())
                {
                    var qrDITOLocation = model.OPS_DITOLocation.Where(c => c.DITOMasterID == masterID).ToList();
                    var qrDITOStation = model.OPS_DITOStation.Where(c => c.DITOMasterID == masterID).Select(c => c.LocationID).ToList();
                    //lay diem dau tien
                    var fisrtLocation = qrDITOLocation.Where(c => c.SortOrderReal.HasValue).OrderByDescending(c => c.SortOrderReal).FirstOrDefault();
                    if (fisrtLocation == null)
                    {
                        fisrtLocation = qrDITOLocation.OrderBy(c => c.SortOrder).FirstOrDefault();
                    }
                    // lst location to
                    List<int> lstLocationNext = qrDITOLocation.Where(c => c.SortOrderReal == null && c.LocationID.HasValue).Select(c => c.LocationID.Value).ToList();
                    var qrDefaultMatrix = model.CAT_LocationMatrixDetail.FirstOrDefault(c => c.IsDefault == true && c.CAT_LocationMatrix.LocationFromID == fisrtLocation.ID && lstLocationNext.Contains(c.CAT_LocationMatrix.LocationToID));
                    List<int> lstStation = new List<int>();
                    if (qrDefaultMatrix != null)
                        lstStation = qrDefaultMatrix.CAT_LocationMatrixStation.Select(c => c.LocationID).Distinct().ToList();
                    if (lstStation.Count == 0)
                    {
                        lstStation = model.CAT_PartnerLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerStation).Select(c => c.LocationID).Distinct().ToList();
                    }
                    result = model.CAT_Location.Where(c => c.Lat > 0 && c.Lng > 0 && lstStation.Contains(c.ID) && !qrDITOStation.Contains(c.ID)).Select(c => new DTOMobile_Station
                    {
                        LocationID = c.ID,
                        LocationName = c.Location,
                        Lat = c.Lat,
                        Lng = c.Lng
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobile_Station> FLMMobileDriver_StationPassed(int masterID)
        {
            try
            {
                var result = new List<DTOMobile_Station>();
                using (var model = new DataEntities())
                {
                    result = model.OPS_DITOStation.Where(c => c.DITOMasterID == masterID).Select(c => new DTOMobile_Station
                    {
                        LocationID = c.LocationID,
                        LocationName = c.CAT_Location.Location,
                        CreatedDate = c.CreatedDate
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public int FLMMobileDriver_StationPass(int masterID, int stationID)
        {
            try
            {
                var dnow = DateTime.Now;
                using (var model = new DataEntities())
                {
                    //tim ditolocation
                    int ditoLocationID = 1;
                    var qrDITOLocation = model.OPS_DITOLocation.Where(c => c.DITOMasterID == masterID).ToList();
                    var lastLocation = qrDITOLocation.Where(c => c.SortOrderReal.HasValue).OrderByDescending(c => c.SortOrderReal).FirstOrDefault();
                    if (lastLocation == null)
                    {
                        lastLocation = qrDITOLocation.OrderBy(c => c.SortOrder).FirstOrDefault();
                    }
                    if (lastLocation.LocationID.Value > 0)
                        ditoLocationID = lastLocation.ID;

                    //luu station cho master
                    var query = model.OPS_DITOStation.FirstOrDefault(c => c.DITOMasterID == masterID && c.LocationID == stationID && c.DITOLocationID == ditoLocationID);
                    if (query == null)
                    {
                        query = new OPS_DITOStation();
                        query.DITOMasterID = masterID;
                        query.LocationID = stationID;
                        query.DITOLocationID = ditoLocationID;
                        query.Price = 0;
                        query.IsMonth = false;
                        query.IsApproved = false;
                        query.CreatedBy = Account.UserName;
                        query.CreatedDate = DateTime.Now;
                        query.DateCome = DateTime.Now;

                        model.OPS_DITOStation.Add(query);
                        model.SaveChanges();

                        query.ModifiedBy = Account.UserName;
                        query.ModifiedDate = DateTime.Now;
                        //chi phi

                        var vehicleID = model.OPS_DITOMaster.Where(c => c.ID == masterID).Select(c => c.VehicleID).FirstOrDefault();
                        var weight = model.OPS_DITOMaster.Where(c => c.ID == masterID).Select(c => c.CAT_Vehicle.RegWeight).FirstOrDefault();
                        var qrPriceMonth = model.CAT_StationMonth.FirstOrDefault(c => c.LocationID == stationID && c.DateFrom <= dnow && c.DateTo >= dnow && c.FLM_Asset.VehicleID == vehicleID);
                        if (qrPriceMonth != null)
                        {
                            query.Price = qrPriceMonth.Price;
                            query.IsMonth = true;
                        }
                        else
                        {
                            var qrPrice = model.CAT_StationPrice.Where(c => c.LocationID == stationID && (c.Ton >= weight|| c.Ton==0)).OrderBy(c => c.Ton).FirstOrDefault();
                            if (qrPrice != null)
                                query.Price = qrPrice.Price;
                        }

                        model.SaveChanges();
                        //end chi phi

                        return -1 * stationID;
                    }
                    return stationID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion

        #region Station Container

        public List<DTOMobile_Station> FLMMobileDriver_COStationlist(int masterID)
        {
            try
            {
                var result = new List<DTOMobile_Station>();
                using (var model = new DataEntities())
                {
                    var qrTOLocation = model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterID).ToList();
                    var qrTOStation = model.OPS_COTOStation.Where(c => c.COTOMasterID == masterID).Select(c => c.LocationID).ToList();
                    //lay diem dau tien
                    var fisrtLocation = qrTOLocation.Where(c => c.SortOrderReal.HasValue).OrderByDescending(c => c.SortOrderReal).FirstOrDefault();
                    if (fisrtLocation == null)
                    {
                        fisrtLocation = qrTOLocation.OrderBy(c => c.SortOrder).FirstOrDefault();
                    }
                    // lst location to
                    List<int> lstLocationNext = qrTOLocation.Where(c => c.SortOrderReal == null && c.LocationID.HasValue).Select(c => c.LocationID.Value).ToList();
                    List<int> lstStation = new List<int>();
                    var qrDefaultMatrix = model.CAT_LocationMatrixDetail.FirstOrDefault(c => c.IsDefault == true && c.CAT_LocationMatrix.LocationFromID == fisrtLocation.ID && lstLocationNext.Contains(c.CAT_LocationMatrix.LocationToID));
                    if (qrDefaultMatrix != null)
                        lstStation = qrDefaultMatrix.CAT_LocationMatrixStation.Select(c => c.LocationID).Distinct().ToList();
                    if (lstStation.Count == 0)
                    {
                        lstStation = model.CAT_PartnerLocation.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerStation).Select(c => c.LocationID).Distinct().ToList();
                    }
                    result = model.CAT_Location.Where(c => c.Lat > 0 && c.Lng > 0 && lstStation.Contains(c.ID) && !qrTOStation.Contains(c.ID)).Select(c => new DTOMobile_Station
                    {
                        LocationID = c.ID,
                        LocationName = c.Location,
                        Lat = c.Lat,
                        Lng = c.Lng
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobile_Station> FLMMobileDriver_COStationPassed(int masterID)
        {
            try
            {
                var result = new List<DTOMobile_Station>();
                using (var model = new DataEntities())
                {
                    result = model.OPS_COTOStation.Where(c => c.COTOMasterID == masterID).Select(c => new DTOMobile_Station
                    {
                        LocationID = c.LocationID,
                        LocationName = c.CAT_Location.Location,
                        CreatedDate = c.CreatedDate
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public int FLMMobileDriver_COStationPass(int masterID, int stationID)
        {
            try
            {
                var dnow = DateTime.Now;
                using (var model = new DataEntities())
                {
                    //tim ditolocation
                    int toLocationID = 1;
                    var qrTOLocation = model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterID).ToList();
                    var fisrtLocation = qrTOLocation.Where(c => c.SortOrderReal.HasValue).OrderByDescending(c => c.SortOrderReal).FirstOrDefault();
                    if (fisrtLocation == null)
                    {
                        fisrtLocation = qrTOLocation.OrderBy(c => c.SortOrder).FirstOrDefault();
                    }
                    if (fisrtLocation.LocationID.Value > 0)
                        toLocationID = fisrtLocation.ID;

                    //luu station cho master
                    var query = model.OPS_COTOStation.FirstOrDefault(c => c.COTOMasterID == masterID && c.LocationID == stationID && c.COTOLocationID == toLocationID);
                    if (query == null)
                    {
                        query = new OPS_COTOStation();
                        query.COTOMasterID = masterID;
                        query.LocationID = stationID;
                        query.COTOLocationID = toLocationID;
                        query.Price = 0;
                        query.IsMonth = false;
                        query.IsApproved = false;
                        query.CreatedBy = Account.UserName;
                        query.CreatedDate = DateTime.Now;
                        query.DateCome = DateTime.Now;
                        model.OPS_COTOStation.Add(query);
                        model.SaveChanges();

                        //chi phi
                        var qrPriceMonth = model.CAT_StationMonth.FirstOrDefault(c => c.LocationID == stationID && c.DateFrom <= dnow && c.DateTo >= dnow && c.FLM_Asset.VehicleID == fisrtLocation.OPS_COTOMaster.VehicleID);
                        if (qrPriceMonth != null)
                        {
                            query.Price = qrPriceMonth.Price;
                            query.IsMonth = true;
                        }
                        else
                        {
                            //var qrPrice = model.CAT_StationPrice.FirstOrDefault(c => c.LocationID == stationID && (c.AssetID == null || c.FLM_Asset.VehicleID == fisrtLocation.OPS_COTOMaster.VehicleID));
                            //if (qrPrice != null)
                            //    query.Price = qrPrice.Price;
                        }
                        model.SaveChanges();
                        //end chi phi

                        return -1 * stationID;
                    }
                    return stationID;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion

        private void DITOLocation_Refresh(DataEntities model, int id)
        {
            var obj = model.OPS_DITOLocation.FirstOrDefault(c => c.ID == id);
            if (obj != null)
            {
                obj.ModifiedBy = Account.UserName;
                obj.ModifiedDate = DateTime.Now;

                // Cập nhật cho OPSDITOGroupProduct
                var lstGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.DITOMasterID && (c.ORD_GroupProduct.CUS_Location.LocationID == obj.LocationID || c.ORD_GroupProduct.CUS_Location1.LocationID == obj.LocationID)).ToList();
                foreach (var group in lstGroup)
                {
                    // Lấy hàng
                    if (group.ORD_GroupProduct.CUS_Location.LocationID == obj.LocationID)
                    {
                        group.ModifiedBy = Account.UserName;
                        group.ModifiedDate = DateTime.Now;
                        group.DateFromCome = obj.DateCome;
                        group.DateFromLeave = obj.DateLeave;
                        if (obj.DateCome.HasValue && obj.LoadingStart.HasValue)
                            group.DateFromLoadStart = obj.DateCome.Value.Date.AddHours(obj.LoadingStart.Value.Hour);
                        if (obj.DateCome.HasValue && obj.LoadingEnd.HasValue)
                        {
                            if (obj.LoadingStart.HasValue && obj.LoadingStart.Value.Hour > obj.LoadingEnd.Value.Hour)
                                group.DateFromLoadEnd = obj.DateCome.Value.Date.AddDays(1).AddHours(obj.LoadingEnd.Value.Hour);
                            else
                                group.DateFromLoadEnd = obj.DateCome.Value.Date.AddHours(obj.LoadingEnd.Value.Hour);
                        }
                    }
                    // Giao hàng
                    if (group.ORD_GroupProduct.CUS_Location1.LocationID == obj.LocationID)
                    {
                        group.ModifiedBy = Account.UserName;
                        group.ModifiedDate = DateTime.Now;
                        group.DateToCome = obj.DateCome;
                        group.DateToLeave = obj.DateLeave;
                        if (obj.DateCome.HasValue && obj.LoadingStart.HasValue)
                            group.DateToLoadStart = obj.DateCome.Value.Date.AddHours(obj.LoadingStart.Value.Hour);
                        if (obj.DateCome.HasValue && obj.LoadingEnd.HasValue)
                        {
                            if (obj.LoadingStart.HasValue && obj.LoadingStart.Value.Hour > obj.LoadingEnd.Value.Hour)
                                group.DateToLoadEnd = obj.DateCome.Value.Date.AddDays(1).AddHours(obj.LoadingEnd.Value.Hour);
                            else
                                group.DateToLoadEnd = obj.DateCome.Value.Date.AddHours(obj.LoadingEnd.Value.Hour);
                        }
                        if (group.DateToLeave.HasValue)
                            group.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                    }
                }
            }
        }

        private DTOFLMDriverScheduleMobile TimerSheet_GetTOMaster(DataEntities model, DTOFLMDriverScheduleMobile item)
        {
            int comestt = -(int)SYSVarType.DITOLocationStatusCome;
            int starttt = -(int)SYSVarType.DITOLocationStatusLoadStart;
            int endtt = -(int)SYSVarType.DITOLocationStatusLoadEnd;
            int leavestt = -(int)SYSVarType.DITOLocationStatusLeave;

            int COcomestt = -(int)SYSVarType.COTOLocationStatusCome;
            int COstartstt = -(int)SYSVarType.COTOLocationStatusLoadStart;
            int COendstt = -(int)SYSVarType.COTOLocationStatusLoadEnd;
            int COleavestt = -(int)SYSVarType.COTOLocationStatusLeave;

            if (item.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster)
            {
                #region truck
                //ds diem nhan
                List<int> lstFrom = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location.LocationID).Distinct().ToList();
                //ds diem giao
                List<int> lstTo = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location1.LocationID).Distinct().ToList();
                //lst ton cbm
                var lsttc = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => new { c.Ton, c.CBM }).ToList();

                item.Ton = lsttc.Sum(c => c.Ton);
                item.CBM = lsttc.Sum(c => c.CBM);

                var qrDImaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.TOMasterID);
                if (qrDImaster == null)
                {
                    return item;
                }
                item.MasterCode = qrDImaster.Code;
                item.ETA = qrDImaster.ETA;
                item.ETD = qrDImaster.ETD;
                item.TempMax = qrDImaster.CAT_Vehicle.TempMax;
                item.TempMin = qrDImaster.CAT_Vehicle.TempMin;
                item.lstLocationFrom = new List<DTOMobileOPSDITOLocation>();

                item.lstLocationFrom.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == item.TOMasterID && c.TypeOfTOLocationID != -(int)SYSVarType.TypeOfTOLocationEmpty && c.LocationID.HasValue && lstFrom.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                {
                    ID = c.ID,
                    LocationID = c.LocationID,
                    LocationName = c.CAT_Location.Location,
                    LocationAddress = c.CAT_Location.Address,
                    LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                    LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                    TOLocationStatusID = c.DITOLocationStatusID,
                    TOLocationStatusName = c.SYS_Var.ValueOfVar,
                    DateCome = c.DateCome,
                    DateLeave = c.DateLeave,
                    LoadingStart = c.LoadingStart,
                    LoadingEnd = c.LoadingEnd,
                    SortOrder = c.SortOrder,
                    Lat = c.CAT_Location.Lat,
                    Lng = c.CAT_Location.Lng,
                    DateComeEstimate = c.DateComeEstimate,
                    SortOrderReal = c.SortOrderReal,
                    Status = c.DITOLocationStatusID == comestt ? 1 : c.DITOLocationStatusID == starttt ? 2 : c.DITOLocationStatusID == endtt ? 3 : c.DITOLocationStatusID == leavestt ? 4 : 0,
                    IsLeave = c.DITOLocationStatusID == -(int)SYSVarType.DITOLocationStatusLeave ? true : false,
                }).ToList());

                item.lstLocationTo = new List<DTOMobileOPSDITOLocation>();
                item.lstLocationTo.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == item.TOMasterID && c.TypeOfTOLocationID != -(int)SYSVarType.TypeOfTOLocationEmpty && c.LocationID.HasValue && lstTo.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                {
                    ID = c.ID,
                    LocationID = c.LocationID,
                    LocationName = c.CAT_Location.Location,
                    LocationAddress = c.CAT_Location.Address,
                    LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                    LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                    TOLocationStatusID = c.DITOLocationStatusID,
                    TOLocationStatusName = c.SYS_Var.ValueOfVar,
                    DateCome = c.DateCome,
                    DateLeave = c.DateLeave,
                    LoadingStart = c.LoadingStart,
                    LoadingEnd = c.LoadingEnd,
                    SortOrder = c.SortOrder,
                    Lat = c.CAT_Location.Lat,
                    Lng = c.CAT_Location.Lng,
                    DateComeEstimate = c.DateComeEstimate,
                    SortOrderReal = c.SortOrderReal,
                    Status = c.DITOLocationStatusID == comestt ? 1 : c.DITOLocationStatusID == starttt ? 2 : c.DITOLocationStatusID == endtt ? 3 : c.DITOLocationStatusID == leavestt ? 4 : 0,
                    IsLeave = c.DITOLocationStatusID == -(int)SYSVarType.DITOLocationStatusLeave ? true : false,
                }).ToList());
                #endregion
            }

            if (item.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster)
            {
                #region container

                item.IsContainer = true;
                var qrMaster = model.OPS_COTOMaster.Where(c => c.ID == item.TOMasterID).Select(c => new { c.VehicleID, c.OPS_COTOLocation, c.Code, c.ID, c.OPS_COTOContainer, c.RomoocID, RomoocNo = c.CAT_Romooc.RegNo }).FirstOrDefault();
                if (qrMaster != null)
                {
                    item.MasterCode = qrMaster.Code;
                    item.RomoocID = qrMaster.RomoocID;
                    item.RomoocNo = qrMaster.RomoocNo;

                    item.GroupOfVehicle = string.Join(", ", qrMaster.OPS_COTOContainer.Select(c => c.OPS_Container.ORD_Container.CAT_Packing.PackingName).ToList());

                    item.lstLocationFrom = new List<DTOMobileOPSDITOLocation>();
                    item.lstLocationFrom.AddRange(qrMaster.OPS_COTOLocation.Where(c => c.LocationID.HasValue && (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                    {
                        ID = c.ID,
                        LocationID = c.LocationID,
                        LocationName = c.CAT_Location.Location,
                        LocationAddress = c.CAT_Location.Address,
                        LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                        LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                        TOLocationStatusID = c.COTOLocationStatusID,
                        TOLocationStatusName = c.SYS_Var.ValueOfVar,
                        DateCome = c.DateCome,
                        DateLeave = c.DateLeave,
                        LoadingStart = c.LoadingStart,
                        LoadingEnd = c.LoadingEnd,
                        SortOrder = c.SortOrder,
                        SortOrderReal = c.SortOrderReal,
                        IsLeave = c.COTOLocationStatusID == -(int)SYSVarType.COTOLocationStatusLeave ? true : false,
                        Lat = c.CAT_Location.Lat,
                        Lng = c.CAT_Location.Lng,
                        DateComeEstimate = c.DateComeEstimate,
                        Status = c.COTOLocationStatusID == COcomestt ? 1 : c.COTOLocationStatusID == COstartstt ? 2 : c.COTOLocationStatusID == COendstt ? 3 : c.COTOLocationStatusID == COleavestt ? 4 : 0,
                        DistanceRemain = -1,

                    }).ToList());

                    item.lstLocationTo = new List<DTOMobileOPSDITOLocation>();
                    item.lstLocationTo.AddRange(qrMaster.OPS_COTOLocation.Where(c => c.LocationID.HasValue && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                    {
                        ID = c.ID,
                        LocationID = c.LocationID,
                        LocationName = c.CAT_Location.Location,
                        LocationAddress = c.CAT_Location.Address,
                        LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                        LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                        TOLocationStatusID = c.COTOLocationStatusID,
                        TOLocationStatusName = c.SYS_Var.ValueOfVar,
                        DateCome = c.DateCome,
                        DateLeave = c.DateLeave,
                        LoadingStart = c.LoadingStart,
                        LoadingEnd = c.LoadingEnd,
                        SortOrder = c.SortOrder,
                        SortOrderReal = c.SortOrderReal,
                        IsLeave = c.COTOLocationStatusID == -(int)SYSVarType.COTOLocationStatusLeave ? true : false,
                        Lat = c.CAT_Location.Lat,
                        Lng = c.CAT_Location.Lng,
                        DateComeEstimate = c.DateComeEstimate,
                        Status = c.COTOLocationStatusID == COcomestt ? 1 : c.COTOLocationStatusID == COstartstt ? 2 : c.COTOLocationStatusID == COendstt ? 3 : c.COTOLocationStatusID == COleavestt ? 4 : 0,
                        DistanceRemain = -1,
                    }).ToList());

                    item.lstLocationGetRomooc = new List<DTOMobileOPSDITOLocation>();
                    item.lstLocationGetRomooc.AddRange(qrMaster.OPS_COTOLocation.Where(c => c.LocationID.HasValue && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetRomooc).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                    {
                        ID = c.ID,
                        LocationID = c.LocationID,
                        LocationName = c.CAT_Location.Location,
                        LocationAddress = c.CAT_Location.Address,
                        LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                        LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                        TOLocationStatusID = c.COTOLocationStatusID,
                        TOLocationStatusName = c.SYS_Var.ValueOfVar,
                        DateCome = c.DateCome,
                        DateLeave = c.DateLeave,
                        LoadingStart = c.LoadingStart,
                        LoadingEnd = c.LoadingEnd,
                        SortOrder = c.SortOrder,
                        SortOrderReal = c.SortOrderReal,
                        IsLeave = c.COTOLocationStatusID == -(int)SYSVarType.COTOLocationStatusLeave ? true : false,
                        Lat = c.CAT_Location.Lat,
                        Lng = c.CAT_Location.Lng,
                        Status = c.COTOLocationStatusID == COcomestt ? 1 : c.COTOLocationStatusID == COstartstt ? 2 : c.COTOLocationStatusID == COendstt ? 3 : c.COTOLocationStatusID == COleavestt ? 4 : 0,
                        DistanceRemain = -1,
                    }).ToList());

                    item.lstLocationReturnRomooc = new List<DTOMobileOPSDITOLocation>();
                    item.lstLocationReturnRomooc.AddRange(qrMaster.OPS_COTOLocation.Where(c => c.LocationID.HasValue && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationReturnRomooc).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                    {
                        ID = c.ID,
                        LocationID = c.LocationID,
                        LocationName = c.CAT_Location.Location,
                        LocationAddress = c.CAT_Location.Address,
                        LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                        LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                        TOLocationStatusID = c.COTOLocationStatusID,
                        TOLocationStatusName = c.SYS_Var.ValueOfVar,
                        DateCome = c.DateCome,
                        DateLeave = c.DateLeave,
                        LoadingStart = c.LoadingStart,
                        LoadingEnd = c.LoadingEnd,
                        SortOrder = c.SortOrder,
                        SortOrderReal = c.SortOrderReal,
                        IsLeave = c.COTOLocationStatusID == -(int)SYSVarType.COTOLocationStatusLeave ? true : false,
                        Lat = c.CAT_Location.Lat,
                        Lng = c.CAT_Location.Lng,
                        Status = c.COTOLocationStatusID == COcomestt ? 1 : c.COTOLocationStatusID == COstartstt ? 2 : c.COTOLocationStatusID == COendstt ? 3 : c.COTOLocationStatusID == COleavestt ? 4 : 0,
                        DistanceRemain = -1,
                    }).ToList());
                }
                #endregion
            }

            return item;
        }

        private bool CheckMaterExist(DataEntities model, int timesheetID)
        {
            var qr = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ID == timesheetID);
            if (qr.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster)
            {
                var qrmaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == qr.ReferID);
                if (qrmaster != null)
                {
                    if (qrmaster.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered)
                    {
                        model.FLM_AssetTimeSheetDriver.RemoveRange(qr.FLM_AssetTimeSheetDriver);
                        model.FLM_AssetTimeSheet.Remove(qr);
                        model.SaveChanges();
                        return false;
                    }
                    if (qrmaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived)
                    {
                        qr.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                        model.SaveChanges();
                        return false;
                    }
                }
            }
            if (qr.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster)
            {
                var qrmaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == qr.ReferID && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterTendered);
                if (qrmaster != null)
                {
                    if (qrmaster.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterTendered)
                    {
                        model.FLM_AssetTimeSheetDriver.RemoveRange(qr.FLM_AssetTimeSheetDriver);
                        model.FLM_AssetTimeSheet.Remove(qr);
                        model.SaveChanges();
                        return false;
                    }
                    if (qrmaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived)
                    {
                        qr.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                        model.SaveChanges();
                        return false;
                    }
                }
            }

            return true;
        }

        public List<DTOOPSVehicle> FLMMobile_RomoocList()
        {
            try
            {
                List<DTOOPSVehicle> result = new List<DTOOPSVehicle>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_Romooc.Select(c => new DTOOPSVehicle
                    {
                        ID = c.ID,
                        RegNo = c.RegNo
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region Mobile Vendor
        public DTOVendor FLMMobileVendor_Get(int vendorID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = model.CUS_Customer.Where(c => c.ID == vendorID).Select(c => new DTOVendor
                    {
                        ID = c.ID,
                        Code = c.Code,
                        Address = c.Address,
                        ProvinceID = c.ProvinceID,
                        ProvinceName = c.CAT_Province.ProvinceName,
                        TelNo = c.TelNo,
                        Fax = c.Fax,
                        Email = c.Email,
                        TaxCode = c.TaxCode,
                        Image = c.Image,
                        VendorName = c.CustomerName,
                    }).FirstOrDefault();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOMobileDIAppointmentRate FLMMobileVendor_MasterGet(int id)
        {
            try
            {
                var result = new DTOMobileDIAppointmentRate();

                using (var model = new DataEntities())
                {
                    var objUser = model.SYS_User.Where(c => c.ID == Account.UserID).Select(c => new { c.ListCustomerID }).FirstOrDefault();
                    if (objUser != null && objUser.ListCustomerID != null && objUser.ListCustomerID.Trim() != string.Empty)
                    {
                        var lstid = objUser.ListCustomerID.Split(',').Select(c => Convert.ToInt32(c)).ToList();

                        result = model.OPS_DITOMaster.Where(c => c.ID == id).Select(c => new DTOMobileDIAppointmentRate
                        {
                            TOMasterID = c.ID,
                            MasterCode = c.Code,
                            CreateVendorID = c.OPS_DITORate.FirstOrDefault(d => lstid.Contains(d.VendorID.Value) && d.IsSend && d.IsAccept != false).VendorID.Value,
                            CreateVendorName = c.OPS_DITORate.FirstOrDefault(d => lstid.Contains(d.VendorID.Value) && d.IsSend && d.IsAccept != false).CUS_Customer.CustomerName,
                            CreateVehicleCode = c.VehicleID.HasValue ? c.CAT_Vehicle.RegNo : DIVehicleCode,
                            CreateDateTime = c.ETD.Value,
                            CreateDriverName = c.DriverName1,
                            CreateTelephone = c.DriverTel1,
                        }).FirstOrDefault();
                        if (result != null)
                        {
                            //ds diem nhan
                            List<int> lstFrom = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == result.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location.LocationID).Distinct().ToList();
                            //ds diem giao
                            List<int> lstTo = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == result.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location1.LocationID).Distinct().ToList();
                            //lst ton cbm
                            var lsttc = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == result.TOMasterID).Select(c => new { c.Ton, c.CBM }).ToList();

                            result.Ton = lsttc.Sum(c => c.Ton);
                            result.CBM = lsttc.Sum(c => c.CBM);

                            result.MasterCode = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == result.TOMasterID).Code;
                            result.lstLocationFrom = new List<DTOMobileOPSDITOLocation>();


                            result.lstLocationFrom.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == result.TOMasterID && c.LocationID.HasValue && lstFrom.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                            {
                                ID = c.ID,
                                LocationID = c.LocationID,
                                LocationName = c.CAT_Location.Location,
                                LocationAddress = c.CAT_Location.Address,
                                LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                                LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                                TOLocationStatusID = c.DITOLocationStatusID,
                                TOLocationStatusName = c.SYS_Var.ValueOfVar,
                                IsLeave = c.DITOLocationStatusID == -(int)SYSVarType.DITOLocationStatusLeave ? true : false,
                                DateCome = c.DateCome,
                                DateLeave = c.DateLeave,
                                LoadingStart = c.LoadingStart,
                                LoadingEnd = c.LoadingEnd,
                                SortOrder = c.SortOrder,
                                SortOrderReal = c.SortOrderReal,
                            }).ToList());

                            result.lstLocationTo = new List<DTOMobileOPSDITOLocation>();
                            result.lstLocationTo.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == result.TOMasterID && c.LocationID.HasValue && lstTo.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                            {
                                ID = c.ID,
                                LocationID = c.LocationID,
                                LocationName = c.CAT_Location.Location,
                                LocationAddress = c.CAT_Location.Address,
                                LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                                LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                                TOLocationStatusID = c.DITOLocationStatusID,
                                IsLeave = c.DITOLocationStatusID == -(int)SYSVarType.DITOLocationStatusLeave ? true : false,
                                TOLocationStatusName = c.SYS_Var.ValueOfVar,
                                DateCome = c.DateCome,
                                DateLeave = c.DateLeave,
                                LoadingStart = c.LoadingStart,
                                LoadingEnd = c.LoadingEnd,
                                SortOrder = c.SortOrder,
                                SortOrderReal = c.SortOrderReal
                            }).ToList());
                        }
                    }
                }

                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobileDIAppointmentRate> FLMMobileVendor_TenderRequestList(int vendorID, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                var result = new List<DTOMobileDIAppointmentRate>();

                using (var model = new DataEntities())
                {
                    DateFrom = DateFrom.Date;
                    DateTo = DateTo.AddDays(1).Date;

                    result = model.OPS_DITORate.Where(c => c.FirstRateTime != null && c.FirstRateTime >= DateFrom && c.LastRateTime < DateTo && c.OPS_DITOMaster.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterSendTender
                        && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.OPS_DITOGroupProduct.Count > 0
                        && c.VendorID == vendorID && c.IsSend && c.IsAccept == null).OrderBy(c => c.CreatedDate).Select(c => new DTOMobileDIAppointmentRate
                    {
                        TOMasterID = c.DITOMasterID,
                        MasterCode = c.OPS_DITOMaster.Code,
                        CreateVendorID = c.VendorID.Value,
                        CreateVendorName = c.CUS_Customer.CustomerName,
                        CreateVehicleCode = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : DIVehicleCode,
                        CreateDateTime = c.OPS_DITOMaster.ETD.Value,
                        CreateDriverName = c.OPS_DITOMaster.DriverName1,
                        CreateTelephone = c.OPS_DITOMaster.DriverTel1,
                    }).ToList();

                    foreach (var item in result)
                    {
                        //ds diem nhan
                        List<int> lstFrom = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location.LocationID).Distinct().ToList();
                        //ds diem giao
                        List<int> lstTo = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location1.LocationID).Distinct().ToList();
                        //lst ton cbm
                        var lsttc = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => new { c.Ton, c.CBM }).ToList();

                        item.Ton = lsttc.Sum(c => c.Ton);
                        item.CBM = lsttc.Sum(c => c.CBM);

                        item.MasterCode = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.TOMasterID).Code;
                        item.lstLocationFrom = new List<DTOMobileOPSDITOLocation>();


                        item.lstLocationFrom.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == item.TOMasterID && c.LocationID.HasValue && lstFrom.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                        {
                            ID = c.ID,
                            LocationID = c.LocationID,
                            LocationName = c.CAT_Location.Location,
                            LocationAddress = c.CAT_Location.Address,
                            LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                            LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                            TOLocationStatusID = c.DITOLocationStatusID,
                            TOLocationStatusName = c.SYS_Var.ValueOfVar,
                            IsLeave = c.DITOLocationStatusID == -(int)SYSVarType.DITOLocationStatusLeave ? true : false,
                            DateCome = c.DateCome,
                            DateLeave = c.DateLeave,
                            LoadingStart = c.LoadingStart,
                            LoadingEnd = c.LoadingEnd,
                            SortOrder = c.SortOrder,
                            SortOrderReal = c.SortOrderReal,
                        }).ToList());

                        item.lstLocationTo = new List<DTOMobileOPSDITOLocation>();
                        item.lstLocationTo.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == item.TOMasterID && c.LocationID.HasValue && lstTo.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                        {
                            ID = c.ID,
                            LocationID = c.LocationID,
                            LocationName = c.CAT_Location.Location,
                            LocationAddress = c.CAT_Location.Address,
                            LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                            LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                            TOLocationStatusID = c.DITOLocationStatusID,
                            IsLeave = c.DITOLocationStatusID == -(int)SYSVarType.DITOLocationStatusLeave ? true : false,
                            TOLocationStatusName = c.SYS_Var.ValueOfVar,
                            DateCome = c.DateCome,
                            DateLeave = c.DateLeave,
                            LoadingStart = c.LoadingStart,
                            LoadingEnd = c.LoadingEnd,
                            SortOrder = c.SortOrder,
                            SortOrderReal = c.SortOrderReal
                        }).ToList());
                    }
                }

                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobileDIAppointmentRate> FLMMobileVendor_TenderAcceptList(int vendorID, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                var result = new List<DTOMobileDIAppointmentRate>();
                int itender = -(int)SYSVarType.StatusOfDITOMasterTendered;
                int idelevery = -(int)SYSVarType.StatusOfDITOMasterDelivery;
                using (var model = new DataEntities())
                {
                    DateFrom = DateFrom.Date;
                    DateTo = DateTo.AddDays(1).Date;

                    result = model.OPS_DITORate.Where(c => c.FirstRateTime != null && c.FirstRateTime >= DateFrom && c.LastRateTime < DateTo && (c.OPS_DITOMaster.StatusOfDITOMasterID == itender || c.OPS_DITOMaster.StatusOfDITOMasterID == idelevery)
                        && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.OPS_DITOGroupProduct.Count > 0 && c.OPS_DITOMaster.OPS_DITORate.Count > 0
                        && c.VendorID == vendorID && c.IsSend && c.IsAccept == true).OrderBy(c => c.CreatedDate).Select(c => new DTOMobileDIAppointmentRate
                    {
                        TOMasterID = c.DITOMasterID,
                        MasterCode = c.OPS_DITOMaster.Code,
                        CreateVendorID = c.VendorID.Value,
                        CreateVendorName = c.CUS_Customer.CustomerName,
                        CreateVehicleCode = c.OPS_DITOMaster.CAT_Vehicle.RegNo,
                        CreateDateTime = c.OPS_DITOMaster.ETD.Value,
                        CreateDriverName = c.OPS_DITOMaster.DriverName1,
                        CreateTelephone = c.OPS_DITOMaster.DriverTel1,
                    }).ToList();

                    foreach (var item in result)
                    {
                        //ds diem nhan
                        List<int> lstFrom = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location.LocationID).Distinct().ToList();
                        //ds diem giao
                        List<int> lstTo = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => c.ORD_GroupProduct.CUS_Location1.LocationID).Distinct().ToList();
                        //lst ton cbm
                        var lsttc = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMasterID).Select(c => new { c.Ton, c.CBM }).ToList();

                        item.Ton = lsttc.Sum(c => c.Ton);
                        item.CBM = lsttc.Sum(c => c.CBM);

                        item.MasterCode = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.TOMasterID).Code;
                        item.lstLocationFrom = new List<DTOMobileOPSDITOLocation>();


                        item.lstLocationFrom.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == item.TOMasterID && c.LocationID.HasValue && lstFrom.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                        {
                            ID = c.ID,
                            LocationID = c.LocationID,
                            LocationName = c.CAT_Location.Location,
                            LocationAddress = c.CAT_Location.Address,
                            LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                            LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                            TOLocationStatusID = c.DITOLocationStatusID,
                            TOLocationStatusName = c.SYS_Var.ValueOfVar,
                            IsLeave = c.DITOLocationStatusID == -(int)SYSVarType.DITOLocationStatusLeave ? true : false,
                            DateCome = c.DateCome,
                            DateLeave = c.DateLeave,
                            LoadingStart = c.LoadingStart,
                            LoadingEnd = c.LoadingEnd,
                            SortOrder = c.SortOrder,
                            SortOrderReal = c.SortOrderReal,
                        }).ToList());

                        item.lstLocationTo = new List<DTOMobileOPSDITOLocation>();
                        item.lstLocationTo.AddRange(model.OPS_DITOLocation.Where(c => c.DITOMasterID == item.TOMasterID && c.LocationID.HasValue && lstTo.Contains(c.LocationID.Value)).OrderBy(c => c.SortOrder).Select(c => new DTOMobileOPSDITOLocation
                        {
                            ID = c.ID,
                            LocationID = c.LocationID,
                            LocationName = c.CAT_Location.Location,
                            LocationAddress = c.CAT_Location.Address,
                            LocationDistrict = c.CAT_Location.CAT_District.DistrictName,
                            LocationProvince = c.CAT_Location.CAT_Province.ProvinceName,
                            TOLocationStatusID = c.DITOLocationStatusID,
                            IsLeave = c.DITOLocationStatusID == -(int)SYSVarType.DITOLocationStatusLeave ? true : false,
                            TOLocationStatusName = c.SYS_Var.ValueOfVar,
                            DateCome = c.DateCome,
                            DateLeave = c.DateLeave,
                            LoadingStart = c.LoadingStart,
                            LoadingEnd = c.LoadingEnd,
                            SortOrder = c.SortOrder,
                            SortOrderReal = c.SortOrderReal
                        }).ToList());
                    }
                }

                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobileDIAppointmentRate> FLMMobileVendor_TenderRejectList(DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                var result = new List<DTOMobileDIAppointmentRate>();

                using (var model = new DataEntities())
                {
                    var objUser = model.SYS_User.Where(c => c.ID == Account.UserID).Select(c => new { c.ListCustomerID }).FirstOrDefault();
                    if (objUser != null && objUser.ListCustomerID != null && objUser.ListCustomerID.Trim() != string.Empty)
                    {
                        var lstid = objUser.ListCustomerID.Split(',').Select(c => Convert.ToInt32(c)).ToList();
                        DateFrom = DateFrom.Date;
                        DateTo = DateTo.AddDays(1).Date;

                        result = model.OPS_DITOMaster.Where(c => c.ETD != null && c.ETD >= DateFrom && c.ETD < DateTo && c.StatusOfDITOMasterID!=-(int)SYSVarType.StatusOfDITOMasterPHT && c.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOGroupProduct.Count > 0 && c.OPS_DITORate.Count > 0 && c.OPS_DITORate.Any(d => d.VendorID.HasValue && lstid.Contains(d.VendorID.Value) && d.IsSend && d.IsAccept == false)).OrderBy(c => c.CreatedDate).Select(c => new DTOMobileDIAppointmentRate
                        {
                            TOMasterID = c.ID,
                            MasterCode = c.Code,
                            CreateVendorID = 0,
                            CreateVendorName = string.Empty,
                            CreateVehicleCode = string.Empty,
                            CreateDateTime = c.ETD.Value,
                            CreateDriverName = string.Empty,
                            CreateTelephone = string.Empty,
                        }).ToList();
                    }
                }

                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobileDITOGroupProduct> FLMMobileVendor_MasterDetail(int id)
        {
            try
            {
                var result = new List<DTOMobileDITOGroupProduct>();

                using (var model = new DataEntities())
                {
                    result = model.OPS_DITOGroupProduct.Where(c => c.OPS_DITOMaster.ID == id).Select(c => new DTOMobileDITOGroupProduct
                    {
                        Ton = c.Ton,
                        CBM = c.CBM,
                        Quantity = c.Quantity,
                        DNCode = c.DNCode,
                        IsOrigin = c.IsOrigin,
                        SOCode = c.ORD_GroupProduct.SOCode,
                        PartnerName = c.ORD_GroupProduct.PartnerID > 0 ? c.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName : "",
                        RoutingCode = c.ORD_GroupProduct.CUS_Routing.CAT_Routing.Code,
                        TranferItem = c.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                    }).ToList();
                }

                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void FLMMobileVendor_TenderApproved(int TOMasterID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (TOMasterID > 0)
                    {
                        var objMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == TOMasterID);
                        var objRate = model.OPS_DITORate.Where(c => c.DITOMasterID == TOMasterID && c.VendorID == objMaster.VendorOfVehicleID && c.IsSend && c.IsAccept == null).OrderBy(c => c.SortOrder).FirstOrDefault();
                        if (objMaster != null && objRate != null)
                        {
                            objMaster.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterApproved;

                            objRate.IsAccept = true;
                            objRate.ModifiedBy = Account.UserName;
                            objRate.ModifiedDate = DateTime.Now;

                            // Cập nhật thời gian rời kho
                            var obj = model.OPS_DITOLocation.FirstOrDefault(c => c.DITOMasterID == TOMasterID && c.SortOrder == 2);
                            if (obj != null)
                                obj.DateLeave = objMaster.ETD;
                            var lstOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == TOMasterID);
                            foreach (var opsGroup in lstOPSGroup)
                            {
                                opsGroup.DateFromLeave = objMaster.ETD;
                                opsGroup.DateFromLoadEnd = objMaster.ETD;
                            }
                            model.SaveChanges();
                        }

                        var lst = model.OPS_DITOMaster.Where(c => c.ID == TOMasterID).Select(c => new DTOOPSMasterTenderRate
                        {
                            ID = c.ID,
                            VehicleID = c.VehicleID,
                            VendorOfVehicleID = c.VendorOfVehicleID,
                            IsAccept = true
                        }).ToList();
                        var first = lst.FirstOrDefault();
                        if (first != null && first.VehicleID > 0)
                        {
                            if (first.VendorOfVehicleID == Account.SYSCustomerID || first.VendorOfVehicleID == null)
                            {
                                DIAppointment_Route_Tender_Update(lst);
                            }
                            else
                            {
                                var lstVEN = model.OPS_DITORate.Where(c => c.ID == objRate.ID).Select(c => new DTOOPSTenderDITOMaster
                                {
                                    ID = c.ID,
                                    DITOMasterID = c.DITOMasterID,
                                    VehicleID = c.OPS_DITOMaster.VehicleID,
                                    VendorOfVehicleID = c.OPS_DITOMaster.VendorOfVehicleID,
                                    DriverName1 = c.OPS_DITOMaster.DriverName1,
                                    DriverTel1 = c.OPS_DITOMaster.DriverTel1,
                                    DriverName2 = c.OPS_DITOMaster.DriverName2,
                                    DriverTel2 = c.OPS_DITOMaster.DriverTel2,
                                    DriverCard1 = c.OPS_DITOMaster.DriverCard1,
                                    DriverCard2 = c.OPS_DITOMaster.DriverCard2,
                                    DriverID1 = c.OPS_DITOMaster.DriverID1,
                                    DriverID2 = c.OPS_DITOMaster.DriverID2,
                                    //AssistantName1 = c.OPS_DITOMaster.AssistantName1,
                                    //AssistantName2 = c.OPS_DITOMaster.AssistantName2,
                                    //AssistantTel1 = c.OPS_DITOMaster.AssistantTel1,
                                    //AssistantTel2 = c.OPS_DITOMaster.AssistantTel2,
                                    //AssistantCard1 = c.OPS_DITOMaster.AssistantCard1,
                                    //AssistantCard2 = c.OPS_DITOMaster.AssistantCard2,
                                    //AssistantID1 = c.OPS_DITOMaster.AssistantID1,
                                    //AssistantID2 = c.OPS_DITOMaster.AssistantID2,
                                }).ToList();
                                DIAppointment_Route_TenderVendor_Update(lstVEN, true, first.VendorOfVehicleID.Value);
                            }
                        }
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void FLMMobileVendor_TenderSave(DTOMobileDIAppointmentRate Vehicle)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (Vehicle != null)
                    {
                        if (true)
                        {
                            string regNo = Vehicle.CreateVehicleCode.Trim();
                            string drivername = string.Empty;
                            string cellphone = string.Empty;
                            double? maxWeight = null;
                            double? maxCBM = null;
                            drivername = !string.IsNullOrEmpty(Vehicle.CreateDriverName) ? Vehicle.CreateDriverName : "";
                            cellphone = Vehicle.CreateTelephone;
                            maxWeight = Vehicle.MaxWeight;
                            maxCBM = Vehicle.MaxCBM;

                            var obj = model.CAT_Vehicle.FirstOrDefault(c => c.RegNo == regNo);
                            if (obj == null)
                            {
                                obj = new CAT_Vehicle();
                                obj.TypeOfVehicleID = -(int)SYSVarType.TypeOfVehicleTruck;
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.MaxWeight = maxWeight;
                                obj.MaxCapacity = maxCBM;
                                obj.RegNo = regNo;
                                obj.IsOwn = false;
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            if (!string.IsNullOrEmpty(drivername))
                                obj.DriverName = drivername;
                            if (!string.IsNullOrEmpty(cellphone))
                                obj.Cellphone = cellphone;

                            if (obj.ID < 1)
                                model.CAT_Vehicle.Add(obj);

                            if (Vehicle.CreateVendorID > 0)
                            {
                                var objCUS = model.CUS_Vehicle.FirstOrDefault(c => c.CustomerID == Vehicle.CreateVendorID && c.VehicleID == obj.ID);
                                if (objCUS == null)
                                {
                                    objCUS = new CUS_Vehicle();
                                    objCUS.VehicleID = obj.ID;
                                    objCUS.CustomerID = Vehicle.CreateVendorID;
                                    objCUS.CreatedBy = Account.UserName;
                                    objCUS.CreatedDate = DateTime.Now;

                                    model.CUS_Vehicle.Add(objCUS);
                                }
                            }


                            foreach (var item in model.OPS_DITOLocation.Where(c => c.DITOMasterID == Vehicle.TOMasterID))
                                model.OPS_DITOLocation.Remove(item);


                            model.SaveChanges();

                            if (true)
                            {
                                var objMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == Vehicle.TOMasterID);
                                if (objMaster != null)
                                {
                                    objMaster.ModifiedBy = Account.UserName;
                                    objMaster.ModifiedDate = DateTime.Now;

                                    objMaster.VendorOfVehicleID = Vehicle.CreateVendorID;
                                    objMaster.VehicleID = obj.ID;

                                    objMaster.DriverName1 = Vehicle.CreateDriverName;
                                    objMaster.DriverTel1 = Vehicle.CreateTelephone;

                                    objMaster.IsHot = false;
                                    objMaster.SortOrder = Vehicle.CreateSortOrder;
                                    objMaster.ETD = Vehicle.CreateDateTime;
                                    objMaster.ETA = Vehicle.CreateDateTime.AddHours(2);
                                    objMaster.DateConfig = Vehicle.CreateDateTime.AddHours(2);
                                    objMaster.IsRouteVendor = false;
                                    objMaster.IsRouteCustomer = false;
                                    objMaster.IsLoading = false;
                                    objMaster.IsBidding = false;
                                    objMaster.Note = "";
                                    objMaster.KM = 0;

                                    // Tao location
                                    var lstLocation = new List<int>();
                                    lstLocation.Add(LocationDefaultID);

                                    var lstLocationFrom = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == objMaster.ID).Select(c => c.ORD_GroupProduct.CUS_Location.LocationID).Distinct().ToList();
                                    var lstLocationTo = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == objMaster.ID).Select(c => c.ORD_GroupProduct.CUS_Location1.LocationID).Distinct().ToList();

                                    for (int sort = 0; sort < lstLocationFrom.Count; sort++)
                                    {
                                        var objLocation = new OPS_DITOLocation();
                                        objLocation.CreatedBy = Account.UserName;
                                        objLocation.CreatedDate = DateTime.Now;
                                        objLocation.OPS_DITOMaster = objMaster;
                                        objLocation.LocationID = lstLocationFrom[sort];
                                        objLocation.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusPlan;
                                        objLocation.SortOrder = sort + 1;
                                        objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationGet;
                                        model.OPS_DITOLocation.Add(objLocation);
                                    }

                                    for (int sort = 0; sort < lstLocationTo.Count; sort++)
                                    {
                                        var objLocation = new OPS_DITOLocation();
                                        objLocation.CreatedBy = Account.UserName;
                                        objLocation.CreatedDate = DateTime.Now;
                                        objLocation.OPS_DITOMaster = objMaster;
                                        objLocation.LocationID = lstLocationTo[sort];
                                        objLocation.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusPlan;
                                        objLocation.SortOrder = sort + 1;
                                        objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationDelivery;
                                        model.OPS_DITOLocation.Add(objLocation);
                                    }

                                    objMaster.TotalLocation = lstLocation.Distinct().Count() - 1;
                                    model.SaveChanges();

                                    HelperTimeSheet.Remove(model, Account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetDITOMaster);
                                    HelperTimeSheet.Create(model, Account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetDITOMaster);
                                    model.SaveChanges();
                                }
                            }
                        }
                    }
                }

                FLMMobileVendor_TenderApproved(Vehicle.TOMasterID);
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public void FLMMobileVendor_LeaveLocation(int masterID, int locationID, int statusID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == masterID);
                    if (master != null)
                    {
                        var location = model.OPS_DITOLocation.FirstOrDefault(c => c.DITOMasterID == masterID && c.LocationID == locationID);
                        if (location != null)
                        {

                            const int StatusCome = -(int)SYSVarType.DITOLocationStatusCome;
                            const int StatusLeave = -(int)SYSVarType.DITOLocationStatusLeave;
                            const int StatusStart = -(int)SYSVarType.DITOLocationStatusLoadStart;
                            const int StatusEnd = -(int)SYSVarType.DITOLocationStatusLoadEnd;
                            location.ModifiedBy = Account.UserName;
                            location.ModifiedDate = DateTime.Now;
                            switch (statusID)
                            {
                                case 1: location.DITOLocationStatusID = StatusCome;
                                    location.DateCome = DateTime.Now;
                                    break;
                                case 2: location.DITOLocationStatusID = StatusStart;
                                    location.LoadingStart = DateTime.Now;
                                    break;
                                case 3: location.DITOLocationStatusID = StatusEnd;
                                    location.LoadingStart = DateTime.Now;
                                    break;
                                case 4: location.DITOLocationStatusID = StatusLeave;
                                    location.DateLeave = DateTime.Now;
                                    break;
                            }

                            model.SaveChanges();
                            master.ModifiedBy = Account.UserName;
                            master.ModifiedDate = DateTime.Now;
                            master.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterDelivery;
                            // Cập nhật cho OPSDITOGroup
                            DITOLocation_Refresh(model, location.ID);
                            model.SaveChanges();
                            // Nếu chuyến hoàn tất => phát sinh/cập nhật PL thực tế
                            bool IsComplete = false;
                            var lstGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID);
                            if (lstGroup.Count() == lstGroup.Count(c => c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete))
                                IsComplete = true;
                            if (IsComplete)
                            {
                                master.ModifiedBy = Account.UserName;
                                master.ModifiedDate = DateTime.Now;
                                master.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterReceived;
                                bool isFixPriceCUS = false;
                                bool isFixPriceVEN = false;
                                HelperFinance.DITOMaster_POD(model, Account, master.ID, out isFixPriceCUS, out isFixPriceVEN);
                                model.SaveChanges();

                                DITO_UpdateFinanceReference(model, master.ID);
                                model.SaveChanges();
                            }
                            model.SaveChanges();
                            // Cập nhật status cho order
                            List<int> lstDIOrderID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID && c.OrderGroupProductID.HasValue && (c.ORD_GroupProduct.CUS_Location.LocationID == locationID || c.ORD_GroupProduct.CUS_Location1.LocationID == locationID)).Select(c => c.ORD_GroupProduct.OrderID).Distinct().ToList();
                            HelperStatus.ORDOrder_Status(model, Account, lstDIOrderID);
                            //using (var statusHelper = new StatusHelper())
                            //{
                            //    statusHelper.ORDStatus_Update(lstDIOrderID);
                            //}
                        }
                    }
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMailVendor> FLMMobileVendor_TenderReject(int id, DTODIAppointmentRouteTenderReject item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    List<DTOMailVendor> result = new List<DTOMailVendor>();
                    var objMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == id);
                    var objRate = model.OPS_DITORate.Where(c => c.DITOMasterID == id && c.IsSend && c.IsAccept == null).OrderBy(c => c.SortOrder).FirstOrDefault();
                    if (objMaster != null && objRate != null)
                    {
                        objMaster.ModifiedBy = Account.UserName;
                        objMaster.ModifiedDate = DateTime.Now;
                        objMaster.VehicleID = null;
                        objMaster.VendorOfVehicleID = null;
                        objMaster.DriverName1 = string.Empty;
                        objMaster.DriverID1 = null;

                        objRate.IsAccept = false;
                        objRate.ModifiedBy = Account.UserName;
                        objRate.ModifiedDate = DateTime.Now;
                        objRate.ReasonID = item.ReasonID;
                        objRate.Reason = item.Reason;

                        var nextRate = model.OPS_DITORate.Where(c => c.DITOMasterID == id && !c.IsSend && c.IsAccept == null && c.SortOrder == objRate.SortOrder + 1).FirstOrDefault();
                        if (nextRate != null)
                        {
                            nextRate.ModifiedBy = Account.UserName;
                            nextRate.ModifiedDate = DateTime.Now;
                            nextRate.IsAccept = null;
                            nextRate.FirstRateTime = DateTime.Now.AddMinutes(1);
                            nextRate.LastRateTime = DateTime.Now.AddHours(2);

                            var mail = result.FirstOrDefault(c => c.VendorID == nextRate.VendorID && c.SysCustomerID == objMaster.SYSCustomerID);
                            if (mail == null)
                            {
                                mail = new DTOMailVendor();
                                mail.VendorID = nextRate.VendorID.Value;
                                mail.SysCustomerID = objMaster.SYSCustomerID;
                                mail.ListRateID = new List<int>();
                                result.Add(mail);
                            }
                            mail.ListRateID.Add(nextRate.ID);
                        }
                        else
                            objMaster.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterApproved;
                    }
                    model.SaveChanges();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<CUSCustomer> FLMMobileVendor_ListVendor()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<CUSCustomer>();
                    result = model.CUS_Customer.Where(c => !c.IsSystem && Account.ListCustomerID.Contains(c.ID) && (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH)).Select(c => new CUSCustomer
                    {
                        ID = c.ID,
                        Code = c.Code,
                        CustomerName = c.CustomerName
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOOPSVehicle> FLMMobileVendor_VehicleListVehicle(int vendorID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = model.CUS_Vehicle.Where(c => c.CustomerID == vendorID && c.CAT_Vehicle.TypeOfVehicleID == -(int)SYSVarType.TypeOfVehicleTruck).Select(c => new DTOOPSVehicle
                    {
                        ID = c.ID,
                        RegNo = c.CAT_Vehicle.RegNo,
                        DriverID = c.CAT_Vehicle.DriverID,
                        DriverName = c.CAT_Vehicle.DriverName != null ? c.CAT_Vehicle.DriverName : "",
                        Cellphone = c.CAT_Vehicle.Cellphone != null ? c.CAT_Vehicle.Cellphone : "",
                        MaxWeight = c.CAT_Vehicle.MaxWeight,
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<FLMDriver> FLMMobileVendor_ListDriver()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    return model.FLM_Driver.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new FLMDriver
                    {
                        ID = c.ID,
                        EmployeeCode = c.CAT_Driver.Code,
                        LastName = c.CAT_Driver.LastName,
                        FirstName = c.CAT_Driver.FirstName,
                        Cellphone = c.CAT_Driver.Cellphone
                    }).ToList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOCATReason> FLMMobileVendor_ReasonList()
        {
            try
            {
                List<DTOCATReason> result = new List<DTOCATReason>();
                using (var model = new DataEntities())
                {
                    var iRejectReason = -(int)SYSVarType.TypeOfReasonTenderReject;
                    result = model.CAT_Reason.Where(c => c.TypeOfReasonID == iRejectReason).OrderBy(c => c.OrderBy).Select(c => new DTOCATReason
                    {
                        ID = c.ID,
                        ReasonName = c.ReasonName
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #region private

        private void DIAppointment_Route_TenderVendor_Update(List<DTOOPSTenderDITOMaster> lst, bool isAccept, int customerid)
        {
            try
            {
                //var isAllowed = Account.ListActionCode.Contains(SYSActionCode.ActApproved.ToString());
                //if (!isAllowed)
                //    throw FaultHelper.BusinessFault(null, null, "Tài khoản không có quyền thực hiện chức năng này.");

                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    //bool IsAutoUpdate = false;
                    foreach (var item in lst)
                    {
                        var objDITORate = model.OPS_DITORate.FirstOrDefault(c => c.ID == item.ID);
                        var objMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.DITOMasterID);
                        if (objDITORate == null || objMaster == null)
                            throw FaultHelper.BusinessFault(DTOErrorString.DAT_NoExists, "");
                        if (isAccept)
                        {
                            //Cập nhật Master
                            objMaster.VendorOfVehicleID = customerid;
                            objMaster.VehicleID = item.VehicleID;
                            objMaster.DriverName1 = item.DriverName1;
                            objMaster.DriverName2 = item.DriverName2;
                            objMaster.DriverTel1 = item.DriverTel1;
                            objMaster.DriverTel2 = item.DriverTel2;
                            objMaster.DriverCard1 = item.DriverCard1;
                            objMaster.DriverCard2 = item.DriverCard2;
                            objMaster.DriverID1 = item.DriverID1;
                            objMaster.DriverID2 = item.DriverID2;
                            //objMaster.AssistantName1 = item.AssistantName1;
                            //objMaster.AssistantName2 = item.AssistantName2;
                            //objMaster.AssistantTel1 = item.AssistantTel1;
                            //objMaster.AssistantTel2 = item.AssistantTel2;
                            //objMaster.AssistantCard1 = item.AssistantCard1;
                            //objMaster.AssistantCard2 = item.AssistantCard2;
                            //objMaster.AssistantID1 = item.AssistantID1;
                            //objMaster.AssistantID2 = item.AssistantID2;
                            objMaster.ModifiedBy = Account.UserName;
                            objMaster.ModifiedDate = DateTime.Now;
                            objMaster.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterTendered;

                            //Cập nhật Rate
                            objDITORate.IsAccept = true;
                            objDITORate.ModifiedBy = Account.UserName;
                            objDITORate.ModifiedDate = DateTime.Now;

                            // Tính PL
                            HelperFinance.DITOMaster_Planning(model, Account, objMaster.ID, customerid);

                            // Tạo sẫn POD
                            POD_CreateByMasterID(model, null, objMaster.ID);
                        }
                        else
                        {
                            //Cập nhật Rate
                            objDITORate.IsAccept = false;
                            objDITORate.ReasonID = item.ReasonID;
                            objDITORate.Reason = item.ReasonNote;

                            //Kiểm tra Rate, nếu là Last => ChangeStatus
                            var lastItem = model.OPS_DITORate.Where(c => c.DITOMasterID == item.DITOMasterID).OrderByDescending(c => c.SortOrder).FirstOrDefault();
                            if (lastItem.ID == item.ID)
                            {
                                objMaster.ModifiedBy = Account.UserName;
                                objMaster.ModifiedDate = DateTime.Now;
                                objMaster.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterApproveAgain; //Gởi phê duyệt lại
                            }
                            else
                            {
                                // Gửi cho vendor tiếp theo
                                //IsAutoUpdate = true;
                                // Cập nhật lại thời gian cho các rate sau
                                var lstNextRate = objMaster.OPS_DITORate.Where(c => !c.IsSend && c.IsAccept == null && c.SortOrder > objDITORate.SortOrder).OrderBy(c => c.SortOrder).ToList();
                                DateTime startTime = DateTime.Now;
                                DateTime endTime = startTime.AddHours(objMaster.RateTime.Value);
                                foreach (var nextRate in lstNextRate)
                                {
                                    nextRate.FirstRateTime = startTime;
                                    nextRate.LastRateTime = endTime;

                                    startTime = endTime.AddMinutes(1);
                                    endTime = startTime.AddHours(objMaster.RateTime.Value);
                                }
                            }
                        }
                        objDITORate.ModifiedBy = Account.UserName;
                        objDITORate.ModifiedDate = DateTime.Now;
                    }
                    model.SaveChanges();

                    //if (IsAutoUpdate)
                    //    OPSMasterTendered_AutoSendMail();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        private void DIAppointment_Route_Tender_Update(List<DTOOPSMasterTenderRate> lst)
        {
            try
            {
                //var isAllowed = Account.ListActionCode.Contains(SYSActionCode.ActApproved.ToString());
                //if (!isAllowed)
                //    throw FaultHelper.BusinessFault(null, null, "Tài khoản không có quyền thực hiện chức năng này.");

                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    //bool IsAutoUpdate = false;
                    DateTime dtNow = DateTime.Now;
                    foreach (var item in lst)
                    {
                        var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.ID);
                        if (master != null)
                        {
                            // Duyệt
                            if (item.IsAccept)
                            {
                                if (master.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterApproved) // Chờ phân xe
                                {
                                    if (item.VehicleID == null)
                                        throw FaultHelper.BusinessFault(null, null, "Vui lòng chọn xe cho lệnh " + master.Code + "!");

                                    master.ModifiedBy = Account.UserName;
                                    master.ModifiedDate = DateTime.Now;
                                    master.VehicleID = item.VehicleID;
                                    master.VendorOfVehicleID = null;
                                    master.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterTendered;

                                    #region Chấp nhận rate
                                    var rate = master.OPS_DITORate.FirstOrDefault(c => (c.VendorID == null || c.VendorID == Account.SYSCustomerID) && c.IsSend && c.IsAccept == null);
                                    if (rate != null)
                                    {
                                        // Nếu lệnh đã chọn sẵn xe hoặc còn trong thời gian duyệt
                                        if (master.VehicleID.HasValue || (rate.FirstRateTime <= dtNow && rate.LastRateTime >= dtNow))
                                        {
                                            rate.IsAccept = true;
                                            rate.ModifiedBy = Account.UserName;
                                            rate.ModifiedDate = DateTime.Now;
                                        }
                                        else
                                            throw FaultHelper.BusinessFault(null, null, "Lệnh " + master.Code + " đã hết hạn duyệt!");
                                    }
                                    else
                                        throw FaultHelper.BusinessFault(null, null, "Lệnh " + master.Code + " đã hết hạn duyệt!");
                                    #endregion

                                    #region Tự động lấy tài xế gán vào Trip
                                    var DateStart = master.ETD.Value.Date;
                                    if (DateStart != null)
                                    {
                                        //var lstFleetPlanning = model.FLM_FleetPlanning.Where(c => c.VehicleID == item.VehicleID && c.PlanningDate == DateStart).Select(c => new
                                        //{
                                        //    c.DriverMainID1,
                                        //    c.DriverMainID2,
                                        //    c.DriverAssistantID1,
                                        //    c.DriverAssistantID2,
                                        //    DriverName1 = c.DriverMainID1 == null ? string.Empty : (c.FLM_Driver2.CAT_Driver.LastName + " " + c.FLM_Driver2.CAT_Driver.FirstName),
                                        //    DriverName2 = c.DriverMainID2 == null ? string.Empty : (c.FLM_Driver3.CAT_Driver.LastName + " " + c.FLM_Driver3.CAT_Driver.FirstName),
                                        //    AssistantName1 = c.DriverAssistantID1 == null ? string.Empty : (c.FLM_Driver.CAT_Driver.LastName + " " + c.FLM_Driver.CAT_Driver.FirstName),
                                        //    AssistantName2 = c.DriverAssistantID2 == null ? string.Empty : (c.FLM_Driver1.CAT_Driver.LastName + " " + c.FLM_Driver1.CAT_Driver.FirstName),
                                        //    DriverTel1 = c.FLM_Driver2.CAT_Driver.Cellphone,
                                        //    DriverTel2 = c.FLM_Driver3.CAT_Driver.Cellphone,
                                        //    AssistantTel1 = c.FLM_Driver.CAT_Driver.Cellphone,
                                        //    AssistantTel2 = c.FLM_Driver1.CAT_Driver.Cellphone,
                                        //    DriverCard1 = c.FLM_Driver2.CAT_Driver.CardNumber,
                                        //    DriverCard2 = c.FLM_Driver3.CAT_Driver.CardNumber,
                                        //    AssistantCard1 = c.FLM_Driver.CAT_Driver.CardNumber,
                                        //    AssistantCard2 = c.FLM_Driver1.CAT_Driver.CardNumber,
                                        //}).ToList();
                                        //foreach (var plan in lstFleetPlanning)
                                        //{
                                        //    if (plan.DriverMainID1.HasValue)
                                        //    {
                                        //        master.DriverID1 = plan.DriverMainID1;
                                        //        master.DriverName1 = plan.DriverName1;
                                        //        master.DriverTel1 = plan.DriverTel1;
                                        //        master.DriverCard1 = plan.DriverCard1;
                                        //    }
                                        //    if (plan.DriverMainID2.HasValue)
                                        //    {
                                        //        master.DriverID2 = plan.DriverMainID2;
                                        //        master.DriverName2 = plan.DriverName2;
                                        //        master.DriverTel2 = plan.DriverTel2;
                                        //        master.DriverCard2 = plan.DriverCard2;
                                        //    }
                                        //    //if (plan.DriverAssistantID1.HasValue)
                                        //    //{
                                        //    //    master.AssistantID1 = plan.DriverAssistantID1;
                                        //    //    master.AssistantName1 = plan.AssistantName1;
                                        //    //    master.AssistantTel1 = plan.AssistantTel1;
                                        //    //    master.AssistantCard1 = plan.AssistantCard1;
                                        //    //}
                                        //    //if (plan.DriverAssistantID2.HasValue)
                                        //    //{
                                        //    //    master.AssistantID2 = plan.DriverAssistantID2;
                                        //    //    master.AssistantName2 = plan.AssistantName2;
                                        //    //    master.AssistantTel2 = plan.AssistantTel2;
                                        //    //    master.AssistantCard2 = plan.AssistantCard2;
                                        //    //}
                                        //}
                                    }
                                    #endregion

                                    // Tính P/L
                                    HelperFinance.DITOMaster_Planning(model, Account, master.ID, null);

                                    // Tạo sẵn POD
                                    POD_CreateByMasterID(model, null, master.ID);
                                    model.SaveChanges();
                                    //Tạo TimeSheet
                                    HelperTimeSheet.Remove(model, Account, master.ID, SYSVarType.StatusOfAssetTimeSheetDITOMaster);
                                    HelperTimeSheet.Create(model, Account, master.ID, SYSVarType.StatusOfAssetTimeSheetDITOMaster);
                                }
                                else
                                    throw FaultHelper.BusinessFault(null, null, "Lệnh " + master.Code + " đã được Phân xe không thể chấp nhận!");
                            }
                            else
                            {
                                // Từ chối lệnh
                                if (master.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterApproved) // Chờ phân xe
                                {
                                    if (item.ReasonID == null)
                                        throw FaultHelper.BusinessFault(null, null, "Lệnh " + master.Code + " chưa chọn lý do từ chối!");

                                    #region Từ chối rate + Chuyển qua cho vendor tiếp theo (nếu có) or Chuyển sang trạng thái gởi phê duyệt lại
                                    var rate = master.OPS_DITORate.FirstOrDefault(c => (c.VendorID == null || c.VendorID == Account.SYSCustomerID) && c.IsSend && c.IsAccept == null);
                                    if (rate != null)
                                    {
                                        // Nếu lệnh đã chọn sẵn xe hoặc còn trong thời gian duyệt
                                        if (master.VehicleID.HasValue || (rate.FirstRateTime <= dtNow && rate.LastRateTime >= dtNow))
                                        {
                                            rate.IsAccept = false;
                                            rate.ModifiedBy = Account.UserName;
                                            rate.ModifiedDate = DateTime.Now;
                                            rate.ReasonID = item.ReasonID;
                                            var reason = model.CAT_Reason.FirstOrDefault(c => c.ID == item.ReasonID);
                                            rate.Reason = reason.ReasonName;
                                            //Kiểm tra Rate, nếu là Last => ChangeStatus
                                            var lastItem = master.OPS_DITORate.Where(c => c.DITOMasterID == item.ID).OrderByDescending(c => c.SortOrder).FirstOrDefault();
                                            if (lastItem.ID == rate.ID)
                                            {
                                                master.ModifiedBy = Account.UserName;
                                                master.ModifiedDate = DateTime.Now;
                                                master.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterApproveAgain; //Gởi phê duyệt lại
                                            }
                                            else
                                            {
                                                //IsAutoUpdate = true;
                                                // Cập nhật lại thời gian cho các rate sau
                                                var lstNextRate = master.OPS_DITORate.Where(c => !c.IsSend && c.IsAccept == null && c.SortOrder > rate.SortOrder).OrderBy(c => c.SortOrder).ToList();
                                                DateTime startTime = DateTime.Now;
                                                DateTime endTime = startTime.AddHours(master.RateTime.Value);
                                                foreach (var nextRate in lstNextRate)
                                                {
                                                    nextRate.FirstRateTime = startTime;
                                                    nextRate.LastRateTime = endTime;
                                                    nextRate.ModifiedBy = Account.UserName;
                                                    nextRate.ModifiedDate = DateTime.Now;

                                                    startTime = endTime.AddMinutes(1);
                                                    endTime = startTime.AddHours(master.RateTime.Value);
                                                }
                                            }
                                        }
                                        else
                                            throw FaultHelper.BusinessFault(null, null, "Lệnh " + master.Code + " đã hết hạn duyệt!");
                                    }
                                    else
                                        throw FaultHelper.BusinessFault(null, null, "Lệnh " + master.Code + " đã hết hạn duyệt!");
                                    #endregion
                                }
                                else
                                    throw FaultHelper.BusinessFault(null, null, "Lệnh " + master.Code + " đã được Phân xe không thể từ chối!");
                            }
                        }
                        else
                            throw FaultHelper.BusinessFault(null, null, "Lệnh không tồn tại!");

                    }
                    model.SaveChanges();

                    //if (IsAutoUpdate)
                    //    OPSMasterTendered_AutoSendMail();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        private void POD_CreateByMasterID(DataEntities model, int? cotomasterID, int? ditomasterID)
        {
            try
            {
                #region Tạo POD cho Container
                if (cotomasterID.HasValue)
                {
                    //var lstContainer = model.OPS_COTODetail.Where(c => c.OPS_COTO.COTOMasterID == cotomasterID).Select(c => c.ContainerID).Distinct().ToList();
                    //foreach (var container in lstContainer)
                    //{
                    //    // Tạo POD
                    //    //var pod = model.POD_COMaster.FirstOrDefault(c => c.COTOMasterID == cotomasterID && c.OrderContainerID == container);
                    //    //if (pod == null)
                    //    //{
                    //    //    pod = new POD_COMaster();
                    //    //    pod.OrderContainerID = container;
                    //    //    pod.COTOMasterID = cotomasterID.Value;
                    //    //    pod.CreatedBy = Account.UserName;
                    //    //    pod.CreatedDate = DateTime.Now;
                    //    //    model.POD_COMaster.Add(pod);
                    //    //}
                    //}
                }
                #endregion

                #region Tạo POD cho Truck
                else
                {
                    if (ditomasterID.HasValue)
                    {
                        var lstGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == ditomasterID);
                        foreach (var group in lstGroupProduct)
                        {
                            var pod = model.POD_DIMaster.FirstOrDefault(c => c.DITOMasterID == ditomasterID && c.OrderID == group.ORD_GroupProduct.OrderID && c.StockID == group.ORD_GroupProduct.LocationFromID.Value);
                            if (pod == null)
                                pod = model.POD_DIMaster.Local.FirstOrDefault(c => c.DITOMasterID == ditomasterID && c.OrderID == group.ORD_GroupProduct.OrderID && c.StockID == group.ORD_GroupProduct.LocationFromID.Value);

                            if (pod == null)
                            {
                                pod = new POD_DIMaster();
                                pod.CreatedBy = Account.UserName;
                                pod.CreatedDate = DateTime.Now;
                                pod.DITOMasterID = ditomasterID.Value;
                                pod.OrderID = group.ORD_GroupProduct.OrderID;
                                pod.StockID = group.ORD_GroupProduct.LocationFromID.Value;
                                model.POD_DIMaster.Add(pod);
                            }
                            POD_DIGroupProduct podGroup = new POD_DIGroupProduct();
                            podGroup.CreatedBy = Account.UserName;
                            podGroup.CreatedDate = DateTime.Now;
                            podGroup.DITOGroupProductID = group.ID;
                            podGroup.TonBBGN = group.TonBBGN;
                            podGroup.CBMBBGN = group.CBMBBGN;
                            podGroup.QuantityBBGN = group.QuantityBBGN;
                            if (!string.IsNullOrEmpty(group.ORD_GroupProduct.DNCode))
                                podGroup.BBGNNo = group.ORD_GroupProduct.DNCode;
                            pod.POD_DIGroupProduct.Add(podGroup);
                            // POD Product
                            foreach (var product in group.OPS_DITOProduct.ToList())
                            {
                                POD_DIProduct podProduct = new POD_DIProduct();
                                podProduct.CreatedBy = Account.UserName;
                                podProduct.CreatedDate = DateTime.Now;
                                podProduct.DITOProductID = product.ID;
                                podProduct.QuantityBBGN = product.QuantityBBGN;
                                podGroup.POD_DIProduct.Add(podProduct);
                            }
                        }
                    }
                }
                #endregion
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        private List<DTOOPSVehicle> Appointment_Route_ListVehicleVendor(DataEntities model, SYSVarType typeid)
        {
            var result = model.CAT_Vehicle.Where(c => c.TypeOfVehicleID == -(int)typeid).Select(c => new DTOOPSVehicle
            {
                ID = c.ID,
                RegNo = c.RegNo,
                DriverID = c.DriverID,
                DriverName = c.DriverName != null ? c.DriverName : "",
                Cellphone = c.Cellphone != null ? c.Cellphone : "",
                ListVendorID = c.CUS_Vehicle.Select(d => d.CustomerID).ToList(),
                IsOwn = c.CUS_Vehicle.Count(d => d.CustomerID == Account.SYSCustomerID) > 0,
                MaxWeight = c.MaxWeight,
            }).ToList();
            return result;
        }

        #endregion

        #endregion

        #region Management

        public List<int> MobiManage_OrderList(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<int>();
                    for (var i = dFrom; dFrom < dTo; dFrom = dFrom.AddDays(1))
                    {
                        var from = dFrom.Date;
                        var to = dFrom.Date.AddDays(1);
                        result.Add(model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.RequestDate >= from && c.ORD_GroupProduct.ORD_Order.RequestDate < to).Select(c => c.ORD_GroupProduct.OrderID).ToList().Distinct().Count());
                    }
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<int> MobiManage_NumOfTOMaster(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<int>();
                    for (var i = dFrom; dFrom < dTo; dFrom = dFrom.AddDays(1))
                    {
                        var from = dFrom.Date;
                        var to = dFrom.Date.AddDays(1);
                        result.Add(model.OPS_DITOGroupProduct.Where(c => Account.ListCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.OPS_DITOMaster.ETD >= from && c.OPS_DITOMaster.ETD < to).Select(c => c.DITOMasterID).ToList().Distinct().Count());
                    }
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<double> MobiManage_SumOfTonCBM(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = new List<double>();
                    for (var i = dFrom; dFrom < dTo; dFrom = dFrom.AddDays(1))
                    {
                        var from = dFrom.Date;
                        var to = dFrom.Date.AddDays(1);
                        var qr = model.ORD_GroupProduct.Where(c => c.ORD_Order.RequestDate >= dFrom && c.ORD_Order.RequestDate < dTo && Account.ListCustomerID.Contains(c.ORD_Order.CustomerID)).Select(c => new { c.Ton, c.CBM }).ToList();
                        result.Add(qr.Sum(c => c.Ton));
                        result.Add(qr.Sum(c => c.CBM));
                    }
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<int> MobiManage_SumOfVehicle()
        {
            try
            {
                int irun = -(int)SYSVarType.StatusOfDITOMasterDelivery;
                using (var model = new DataEntities())
                {
                    var result = new List<int>();
                    var lstXe = model.CUS_Vehicle.Where(c => c.CustomerID == Account.SYSCustomerID).Select(c => c.VehicleID).Distinct().ToList();
                    int free = 0;
                    int notfree = 0;
                    var qrMaster = model.OPS_DITOMaster.Where(c => c.StatusOfDITOMasterID == irun ).Select(c => c.VehicleID).ToList();
                    foreach (var xe in lstXe)
                    {
                        if (qrMaster.Contains(xe))
                        {
                            notfree++;
                        }
                        else
                        {
                            free++;
                        }
                    }
                    result.Add(free);
                    result.Add(notfree);
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOCATVehicle> MobiManage_VehicleList()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var result = model.CUS_Vehicle.Where(c => c.CAT_Vehicle.Lat.HasValue && c.CAT_Vehicle.Lat != 0 && c.CAT_Vehicle.Lng.HasValue && c.CAT_Vehicle.Lng != 0 && c.CustomerID == Account.SYSCustomerID && c.CAT_Vehicle.TypeOfVehicleID == -(int)SYSVarType.TypeOfVehicleTruck).Select(c => new DTOCATVehicle
                    {
                        ID = c.ID,
                        RegNo = c.CAT_Vehicle.RegNo,
                        DriverID = c.CAT_Vehicle.DriverID,
                        DriverName = c.CAT_Vehicle.DriverName != null ? c.CAT_Vehicle.DriverName : "",
                        Cellphone = c.CAT_Vehicle.Cellphone != null ? c.CAT_Vehicle.Cellphone : "",
                        MaxWeight = c.CAT_Vehicle.MaxWeight,
                        Lat = c.CAT_Vehicle.Lat,
                        Lng = c.CAT_Vehicle.Lng
                    }).ToList();
                    return result;
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMON_WFL_Message> MobiManage_GetAllNotification()
        {
            try
            {
                var result = new List<DTOMON_WFL_Message>();

                using (var model = new DataEntities())
                {
                    result = model.WFL_Message.Where(c => c.WFL_Action.TypeOfActionID == 3 && c.WFL_Action.WFL_Event.Code.Substring(0, 3) == "OPS").Select(c => new DTOMON_WFL_Message
                    {
                        Message = c.Message + c.ActionID,
                        CreatedDate = c.CreatedDate,
                        ActionCode = c.WFL_Action.WFL_Event.Code
                    }).OrderByDescending(c => c.CreatedDate).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobile_Order> MobiManage_OrderSummary(DateTime dFrom, DateTime dTo)
        {
            try
            {
                var result = new List<DTOMobile_Order>();
                int iNew = -(int)SYSVarType.StatusOfOrderPlaning;
                int iTrans = -(int)SYSVarType.StatusOfOrderTranfer;
                int iRece = -(int)SYSVarType.StatusOfOrderReceived;
                int iInvo = -(int)SYSVarType.StatusOfOrderInvoiceComplete;

                using (var model = new DataEntities())
                {
                    bool isAdmin = Account.ListActionCode.Contains(SYSViewCode.ViewAdmin.ToString());
                    result = model.ORD_Order.Where(c => c.RequestDate >= dFrom && c.RequestDate < dTo && (isAdmin || Account.ListCustomerID.Contains(c.CustomerID))).GroupBy(c => new { c.StatusOfOrderID, StatusOfOrderName = c.SYS_Var.ValueOfVar }).Select(c => new DTOMobile_Order
                    {
                        StatusOfOrderID = c.Key.StatusOfOrderID,
                        StatusOfOrderName = c.Key.StatusOfOrderName,
                        StatusOfOrderCode = c.Key.StatusOfOrderID < iTrans ? "new" : c.Key.StatusOfOrderID == iTrans ? "trans" : c.Key.StatusOfOrderID == iRece ? "receive" : c.Key.StatusOfOrderID == iInvo ? "invoice" : string.Empty,
                        Sum = c.Count(),
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobile_Order> MobiManage_Order(int skip, int pageSize, DateTime dFrom, DateTime dTo, int orderStatus)
        {
            try
            {
                var result = new List<DTOMobile_Order>();

                using (var model = new DataEntities())
                {
                    bool isAdmin = Account.ListActionCode.Contains(SYSViewCode.ViewAdmin.ToString());
                    result = model.ORD_Order.Where(c => c.StatusOfOrderID == orderStatus && c.RequestDate >= dFrom && c.RequestDate < dTo && (isAdmin || Account.ListCustomerID.Contains(c.CustomerID))).Select(c => new DTOMobile_Order
                    {
                        OrderID = c.ID,
                        OrderCode = c.Code,
                        CreateDate = c.RequestDate,

                    }).OrderByDescending(c => c.CreateDate).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOMobile_OrderDetail MobiManage_OrderDetail(int orderID)
        {
            try
            {
                var result = new DTOMobile_OrderDetail();

                using (var model = new DataEntities())
                {
                    var qr = model.ORD_Order.FirstOrDefault(c => c.ID == orderID);
                    if (qr != null)
                    {
                        result.OrderID = qr.ID;
                        result.OrderCode = qr.Code;
                        result.Ton = qr.ORD_GroupProduct.Sum(c => c.Ton);
                        result.ListProduct = qr.ORD_GroupProduct.Select(c => new DTOMobile_OrderProduct
                        {
                            Ton = c.Ton,
                            Quantity = c.Quantity,
                            GroupProductName = c.CUS_GroupOfProduct.GroupName,
                            ListProductCode = string.Join(",", c.ORD_Product.Select(d => d.CUS_Product.Code).ToList()),
                        }).ToList();

                        result.ListLocation = qr.ORD_GroupProduct.Select(c => new DTOMobile_OrderLocation
                        {
                            Address = c.CUS_Location1.CAT_Location.Address
                        }).ToList();

                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobile_TOMaster> MobiManage_TOMasterList(int skip, int pageSize, DateTime dFrom, DateTime dTo, int masterStatus)
        {
            try
            {
                var result = new List<DTOMobile_TOMaster>();
                bool isAdmin = Account.ListActionCode.Contains(SYSViewCode.ViewAdmin.ToString());
                using (var model = new DataEntities())
                {
                    var lstMasterID = model.OPS_DITOGroupProduct.Where(c => c.OPS_DITOMaster.StatusOfDITOMasterID == masterStatus && c.OPS_DITOMaster.ETD >= dFrom && c.OPS_DITOMaster.ETD < dTo && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.VehicleID > 1 && (isAdmin || Account.ListCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID))).Select(c => c.DITOMasterID).Distinct().ToList();
                    result = model.OPS_DITOMaster.Where(c => lstMasterID.Contains(c.ID)).Select(c => new DTOMobile_TOMaster
                    {
                        ID = c.ID,
                        MasterCode = c.Code,
                        RegNo = c.CAT_Vehicle.RegNo,
                        ListCustomerCode = c.OPS_DITOGroupProduct.FirstOrDefault().ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        DriverName = c.DriverName1,
                        StatusOfTOMaster = c.SYS_Var.ValueOfVar,
                        StatusCode = c.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterDelivery ? "plan" : c.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterDelivery ? "delivery" : c.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterReceived ? "received" : "invoice"
                    }).OrderByDescending(c => c.ID).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobile_TOMaster> MobiManage_TOMasterSummary(DateTime dFrom, DateTime dTo)
        {
            try
            {
                var result = new List<DTOMobile_TOMaster>();
                bool isAdmin = Account.ListActionCode.Contains(SYSViewCode.ViewAdmin.ToString());
                using (var model = new DataEntities())
                {
                    var lstMasterID = model.OPS_DITOGroupProduct.Where(c => c.OPS_DITOMaster.ETD >= dFrom && c.OPS_DITOMaster.ETD < dTo && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.VehicleID > 1 && (isAdmin || Account.ListCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID))).Select(c => c.DITOMasterID).Distinct().ToList();
                    result = model.OPS_DITOMaster.Where(c => lstMasterID.Contains(c.ID)).GroupBy(c => new { c.StatusOfDITOMasterID, StatusOfTOMasterName = c.SYS_Var.ValueOfVar }).Select(c => new DTOMobile_TOMaster
                    {
                        StatusOfTOMasterID = c.Key.StatusOfDITOMasterID,
                        StatusOfTOMaster = c.Key.StatusOfTOMasterName,
                        StatusCode = c.Key.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterDelivery ? "plan" : c.Key.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterDelivery ? "delivery" : c.Key.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterReceived ? "received" : "invoice",
                        Sum = c.Count(),
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobile_Trouble> MobiManage_DITroubleSummary(DateTime dFrom, DateTime dTo)
        {
            try
            {
                var result = new List<DTOMobile_Trouble>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_Trouble.Where(c => c.CreatedDate >= dFrom && c.CreatedDate < dTo && c.DITOMasterID > 0 && c.CAT_GroupOfTrouble.TypeOfGroupTroubleID == -(int)SYSVarType.TypeOfGroupTroubleDI).GroupBy(c => new { c.GroupOfTroubleID, GroupOfTroubleName = c.CAT_GroupOfTrouble.Name }).Select(c => new DTOMobile_Trouble
                    {
                        TroubleGroupID = c.Key.GroupOfTroubleID,
                        TroubleGroup = c.Key.GroupOfTroubleName,
                        Cost = c.Sum(d => d.Cost),
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobile_Trouble> MobiManage_DITroubleList(int skip, int pageSize, DateTime dFrom, DateTime dTo, int GroupID)
        {
            try
            {
                var result = new List<DTOMobile_Trouble>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_Trouble.Where(c => c.GroupOfTroubleID == GroupID && c.CreatedDate >= dFrom && c.CreatedDate < dTo && c.DITOMasterID > 0 && c.DITOMasterID > 0 && c.CAT_GroupOfTrouble.TypeOfGroupTroubleID == -(int)SYSVarType.TypeOfGroupTroubleDI).Select(c => new DTOMobile_Trouble
                    {
                        ID = c.ID,
                        MasterID = c.DITOMasterID.Value,
                        MasterCode = c.OPS_DITOMaster.Code,
                        TroubleGroup = c.CAT_GroupOfTrouble.Name,
                        Date = c.CreatedDate,
                        Cost = c.Cost,
                    }).OrderByDescending(c => c.Date).Skip(skip).Take(pageSize).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOMobile_Finance> MobiManage_TruckProfit(int skip, int pageSize)
        {
            try
            {
                var result = new List<DTOMobile_Finance>();
                using (var model = new DataEntities())
                {
                    result = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ETD.HasValue && c.StatusOfDITOMasterID != -(int)SYSVarType.StatusOfDITOMasterPHT && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived).Select(c => new DTOMobile_Finance
                    {
                        TOMasterID = c.ID,
                        TOMasterCode = c.Code,
                        ETD = c.ETD.Value,
                        Credit = 0,
                        Debit = 0,
                    }).OrderByDescending(c => c.ETD).Skip(skip).Take(pageSize).ToList();
                    foreach (var obj in HelperFinance.Truck_GetProfit(model, result.Select(c => c.TOMasterID).ToList()))
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            if (obj.DITOMasterID == result[i].TOMasterID)
                            {
                                result[i].Credit = obj.Credit;
                                result[i].Debit = obj.Debit;
                            }
                        }
                    }

                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
        #endregion

        #region Route Problem

        public List<DTOTriggerMessage> MessageCall()
        {
            try
            {
                var result = new List<DTOTriggerMessage>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_Message.Where(c => c.CreatedBy!=Account.UserName && c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_Action.UserID == Account.UserID).Select(c => new DTOTriggerMessage
                    {
                        ID = c.ID,
                        Message = c.Message,
                        CreatedDate = c.CreatedDate,
                    }).ToList();

                    if (result.Count > 0)
                    {
                        foreach (var item in result)
                        {
                            var obj = model.WFL_Message.FirstOrDefault(c => c.ID == item.ID);
                            if (obj != null)
                                obj.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageSended;
                        }
                        model.SaveChanges();
                    }
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public List<DTOOPSRouteProblem> ProblemList()
        {
            try
            {
                var result = new List<DTOOPSRouteProblem>();
                using (var model = new DataEntities())
                {
                    var time = DateTime.Now.AddHours(-12);
                    result = model.OPS_RouteProblem.Where(c => c.DateStart > time).OrderByDescending(c=>c.DateStart).Select(c => new DTOOPSRouteProblem
                    {
                        ID = c.ID,
                        TypeOfRouteProblemName = c.OPS_TypeOfRouteProblem.TypeName,
                        DateStart = c.DateStart
                    }).ToList();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public DTOOPSRouteProblem ProblemGet(int id)
        {
            try
            {
                var result = new DTOOPSRouteProblem();
                using (var model = new DataEntities())
                {
                    result = model.OPS_RouteProblem.Where(c => c.ID == id).Select(c => new DTOOPSRouteProblem
                    {
                        ID = c.ID,
                        TypeOfRouteProblemName = c.OPS_TypeOfRouteProblem.TypeName,
                        Lat = c.Lat,
                        Lng = c.Lng
                    }).FirstOrDefault();
                }
                return result;
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        #endregion
    }
}