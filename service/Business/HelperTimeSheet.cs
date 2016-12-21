using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using System.ServiceModel;

namespace Business
{
    public class HelperTimeSheet
    {
        public static bool Check(DataEntities model, int referID, SYSVarType status, DateTime fDate, DateTime tDate)
        {
            return true;
        }

        public static void Create(DataEntities model, AccountItem account, int referID, SYSVarType status)
        {
            try
            {
                int assetID = -1;
                DateTime? etd = null, eta = null, atd = null, ata = null;
                int typeAsset = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
                Dictionary<int, bool> dicDriver = new Dictionary<int, bool>();
                switch (status)
                {
                    case SYSVarType.StatusOfAssetTimeSheetCOTOMaster:
                        var objCO = model.OPS_COTOMaster.Where(c => c.ID == referID).Select(c => new { c.StatusOfCOTOMasterID, c.RomoocID, c.ETA, c.ETD, c.ATD, c.ATA, c.VehicleID, c.DriverID1, c.DriverID2, c.DriverID3 }).FirstOrDefault();
                        if (objCO != null)
                        {
                            if (objCO.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived)
                                typeAsset = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                            etd = objCO.ETD;
                            eta = objCO.ETA;
                            atd = objCO.ETD;
                            ata = objCO.ETA;
                            if (objCO.ATD != null && objCO.ATA != null)
                            {
                                atd = objCO.ATD;
                                ata = objCO.ATA;
                            }
                            if (objCO.VehicleID > 2)
                            {
                                var objAsset = model.FLM_Asset.Where(c => c.VehicleID == objCO.VehicleID && c.SYSCustomerID == account.SYSCustomerID).Select(c => new { c.ID }).FirstOrDefault();
                                if (objAsset != null)
                                    assetID = objAsset.ID;
                                if (objCO.DriverID1 > 0 && objCO.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterTendered) dicDriver.Add(objCO.DriverID1.Value, true);
                                if (objCO.DriverID2 > 0 && objCO.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterTendered) dicDriver.Add(objCO.DriverID2.Value, true);
                            }

                            if (objCO.RomoocID > 1)
                            {
                                var romooc = model.FLM_Asset.Where(c => c.RomoocID == objCO.RomoocID).Select(c => new { c.ID }).FirstOrDefault();
                                if (romooc != null && etd != null && eta != null)
                                {
                                    var obj = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.AssetID == romooc.ID && c.ReferID == referID && c.StatusOfAssetTimeSheetID == -(int)status);
                                    if (obj == null)
                                    {
                                        obj = new FLM_AssetTimeSheet();
                                        obj.AssetID = romooc.ID;
                                        obj.ReferID = referID;
                                        obj.StatusOfAssetTimeSheetID = -(int)status;
                                        obj.CreatedBy = account.UserName;
                                        obj.CreatedDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        obj.ModifiedBy = account.UserName;
                                        obj.ModifiedDate = DateTime.Now;
                                    }
                                    obj.TypeOfAssetTimeSheetID = typeAsset;
                                    obj.DateFrom = etd.Value;
                                    obj.DateTo = eta.Value;
                                    obj.DateFromActual = atd.Value;
                                    obj.DateToActual = ata.Value;
                                    if (obj.ID < 1)
                                        model.FLM_AssetTimeSheet.Add(obj);
                                }
                            }
                        }
                        break;
                    case SYSVarType.StatusOfAssetTimeSheetDITOMaster:
                        var objDI = model.OPS_DITOMaster.Where(c => c.ID == referID).Select(c => new { c.StatusOfDITOMasterID, c.ETA, c.ETD, c.ATD, c.ATA, c.VehicleID, c.DriverID1, c.DriverID2, c.DriverID3 }).FirstOrDefault();
                        if (objDI != null)
                        {
                            if (objDI.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived)
                                typeAsset = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                            etd = objDI.ETD;
                            eta = objDI.ETA;
                            atd = objDI.ETD;
                            ata = objDI.ETA;
                            if (objDI.ATD != null && objDI.ATA != null)
                            {
                                atd = objDI.ATD;
                                ata = objDI.ATA;
                            }
                            if (objDI.VehicleID > 2)
                            {
                                var objAsset = model.FLM_Asset.Where(c => c.VehicleID == objDI.VehicleID && c.SYSCustomerID == account.SYSCustomerID).Select(c => new { c.ID }).FirstOrDefault();
                                if (objAsset != null)
                                    assetID = objAsset.ID;
                                if (objDI.DriverID1 > 0 && objDI.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered) dicDriver.Add(objDI.DriverID1.Value, true);
                                if (objDI.DriverID2 > 0 && objDI.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered) dicDriver.Add(objDI.DriverID2.Value, true);
                                if (objDI.DriverID3 > 0 && objDI.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered) dicDriver.Add(objDI.DriverID3.Value, false);
                            }
                        }
                        break;
                    case SYSVarType.StatusOfAssetTimeSheetRepair:
                    case SYSVarType.StatusOfAssetTimeSheetMaintence:
                    case SYSVarType.StatusOfAssetTimeSheetRegistry:
                        var objActivity = model.FLM_Activity.Where(c => c.ID == referID).Select(c => new { c.AssetID, c.EffectiveDateFrom, c.EffectiveDateTo }).FirstOrDefault();
                        if (objActivity != null)
                        {
                            assetID = objActivity.AssetID;
                            etd = objActivity.EffectiveDateFrom;
                            eta = objActivity.EffectiveDateTo;
                            atd = objActivity.EffectiveDateFrom;
                            ata = objActivity.EffectiveDateTo;
                        }
                        break;
                }
                if (etd != null && eta != null && assetID > 0)
                {
                    var obj = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.AssetID == assetID && c.ReferID == referID && c.StatusOfAssetTimeSheetID == -(int)status);
                    if (obj == null)
                    {
                        obj = new FLM_AssetTimeSheet();
                        obj.AssetID = assetID;
                        obj.ReferID = referID;
                        obj.StatusOfAssetTimeSheetID = -(int)status;
                        obj.CreatedBy = account.UserName;
                        obj.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        obj.ModifiedBy = account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.TypeOfAssetTimeSheetID = typeAsset;
                    obj.DateFrom = etd.Value;
                    obj.DateTo = eta.Value;
                    obj.DateFromActual = atd.Value;
                    obj.DateToActual = ata.Value;
                    if (obj.ID < 1)
                        model.FLM_AssetTimeSheet.Add(obj);

                    if (dicDriver.Count > 0)
                    {
                        foreach (var item in dicDriver)
                        {
                            var driver = new FLM_AssetTimeSheetDriver();
                            driver.FLM_AssetTimeSheet = obj;
                            driver.DriverID = item.Key;
                            driver.IsMain = item.Value;
                            driver.IsReject = false;
                            driver.CreatedBy = account.UserName;
                            driver.CreatedDate = DateTime.Now;
                            model.FLM_AssetTimeSheetDriver.Add(driver);
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

        public static void Create(DataEntities model, AccountItem account, int referID, SYSVarType status, int? mainDriverID)
        {
            try
            {
                int assetID = -1;
                DateTime? etd = null, eta = null, atd = null, ata = null;
                int typeAsset = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
                Dictionary<int, bool> dicDriver = new Dictionary<int, bool>();
                switch (status)
                {
                    case SYSVarType.StatusOfAssetTimeSheetCOTOMaster:
                        var objCO = model.OPS_COTOMaster.Where(c => c.ID == referID).Select(c => new { c.StatusOfCOTOMasterID, c.RomoocID, c.ETA, c.ETD, c.ATD, c.ATA, c.VehicleID, c.DriverID1, c.DriverID2 }).FirstOrDefault();
                        if (objCO != null)
                        {
                            if (objCO.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived)
                                typeAsset = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                            etd = objCO.ETD;
                            eta = objCO.ETA;
                            atd = objCO.ETD;
                            ata = objCO.ETA;
                            if (objCO.ATD != null && objCO.ATA != null)
                            {
                                atd = objCO.ATD;
                                ata = objCO.ATA;
                            }
                            if (objCO.VehicleID > 0)
                            {
                                var objAsset = model.FLM_Asset.Where(c => c.VehicleID == objCO.VehicleID && c.SYSCustomerID == account.SYSCustomerID).Select(c => new { c.ID }).FirstOrDefault();
                                if (objAsset != null)
                                    assetID = objAsset.ID;
                                if (objCO.DriverID1 > 0 && objCO.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterTendered) dicDriver.Add(objCO.DriverID1.Value, true);
                                if (objCO.DriverID2 > 0 && objCO.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterTendered) dicDriver.Add(objCO.DriverID2.Value, true);
                            }
                            if (objCO.RomoocID > 1)
                            {
                                var romooc = model.FLM_Asset.Where(c => c.RomoocID == objCO.RomoocID).Select(c => new { c.ID }).FirstOrDefault();
                                if (romooc != null && etd != null && eta != null)
                                {
                                    var obj = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.AssetID == romooc.ID && c.ReferID == referID && c.StatusOfAssetTimeSheetID == -(int)status);
                                    if (obj == null)
                                    {
                                        obj = new FLM_AssetTimeSheet();
                                        obj.AssetID = romooc.ID;
                                        obj.ReferID = referID;
                                        obj.StatusOfAssetTimeSheetID = -(int)status;
                                        obj.CreatedBy = account.UserName;
                                        obj.CreatedDate = DateTime.Now;
                                    }
                                    else
                                    {
                                        obj.ModifiedBy = account.UserName;
                                        obj.ModifiedDate = DateTime.Now;
                                    }
                                    obj.TypeOfAssetTimeSheetID = typeAsset;
                                    obj.DateFrom = etd.Value;
                                    obj.DateTo = eta.Value;
                                    obj.DateFromActual = atd.Value;
                                    obj.DateToActual = ata.Value;
                                    if (obj.ID < 1)
                                        model.FLM_AssetTimeSheet.Add(obj);
                                }
                            }
                        }
                        break;
                    case SYSVarType.StatusOfAssetTimeSheetDITOMaster:
                        var objDI = model.OPS_DITOMaster.Where(c => c.ID == referID).Select(c => new { c.StatusOfDITOMasterID, c.ETA, c.ETD, c.ATD, c.ATA, c.VehicleID, c.DriverID1, c.DriverID2, c.DriverID3 }).FirstOrDefault();
                        if (objDI != null)
                        {
                            if (objDI.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived)
                                typeAsset = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                            etd = objDI.ETD;
                            eta = objDI.ETA;
                            atd = objDI.ETD;
                            ata = objDI.ETA;
                            if (objDI.ATD != null && objDI.ATA != null)
                            {
                                atd = objDI.ATD;
                                ata = objDI.ATA;
                            }
                            if (objDI.VehicleID > 0)
                            {
                                var objAsset = model.FLM_Asset.Where(c => c.VehicleID == objDI.VehicleID && c.SYSCustomerID == account.SYSCustomerID).Select(c => new { c.ID }).FirstOrDefault();
                                if (objAsset != null)
                                    assetID = objAsset.ID;
                                if (objDI.DriverID1 > 0 && objDI.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered) dicDriver.Add(objDI.DriverID1.Value, true);
                                if (objDI.DriverID2 > 0 && objDI.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered) dicDriver.Add(objDI.DriverID2.Value, true);
                                if (objDI.DriverID3 > 0 && objDI.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered) dicDriver.Add(objDI.DriverID3.Value, false);
                            }
                        }
                        break;
                    case SYSVarType.StatusOfAssetTimeSheetRepair:
                    case SYSVarType.StatusOfAssetTimeSheetMaintence:
                    case SYSVarType.StatusOfAssetTimeSheetRegistry:
                        var objActivity = model.FLM_Activity.Where(c => c.ID == referID).Select(c => new { c.AssetID, c.EffectiveDateFrom, c.EffectiveDateTo }).FirstOrDefault();
                        if (objActivity != null)
                        {
                            assetID = objActivity.AssetID;
                            etd = objActivity.EffectiveDateFrom;
                            eta = objActivity.EffectiveDateTo;
                            atd = objActivity.EffectiveDateFrom;
                            ata = objActivity.EffectiveDateTo;
                        }
                        break;
                }
                if (etd != null && eta != null && assetID > 0)
                {
                    var obj = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.AssetID == assetID && c.ReferID == referID && c.StatusOfAssetTimeSheetID == -(int)status);
                    if (obj == null)
                    {
                        obj = new FLM_AssetTimeSheet();
                        obj.AssetID = assetID;
                        obj.ReferID = referID;
                        obj.StatusOfAssetTimeSheetID = -(int)status;
                        obj.CreatedBy = account.UserName;
                        obj.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        obj.ModifiedBy = account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.TypeOfAssetTimeSheetID = typeAsset;
                    obj.DateFrom = etd.Value;
                    obj.DateTo = eta.Value;
                    obj.DateFromActual = atd.Value;
                    obj.DateToActual = ata.Value;
                    if (obj.ID < 1)
                        model.FLM_AssetTimeSheet.Add(obj);

                    if (mainDriverID > 0)
                    {
                        var objFLMDriver = new FLM_AssetTimeSheetDriver();
                        objFLMDriver.FLM_AssetTimeSheet = obj;
                        objFLMDriver.DriverID = mainDriverID.Value;
                        objFLMDriver.IsMain = true;
                        objFLMDriver.IsReject = false;
                        objFLMDriver.CreatedBy = account.UserName;
                        objFLMDriver.CreatedDate = DateTime.Now;
                        model.FLM_AssetTimeSheetDriver.Add(objFLMDriver);
                    }

                    if (dicDriver.Count > 0)
                    {
                        foreach (var item in dicDriver)
                        {
                            var objFLMDriver = new FLM_AssetTimeSheetDriver();
                            objFLMDriver.FLM_AssetTimeSheet = obj;
                            objFLMDriver.DriverID = item.Key;
                            objFLMDriver.IsMain = item.Value;
                            objFLMDriver.IsReject = false;
                            objFLMDriver.CreatedBy = account.UserName;
                            objFLMDriver.CreatedDate = DateTime.Now;
                            model.FLM_AssetTimeSheetDriver.Add(objFLMDriver);
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

        /// <summary>
        /// Dùng trong trường hợp tạo chuyến FLM, có bổ sung điểm kết thúc.
        /// </summary>
        /// <param name="model">DataEntities</param>
        /// <param name="account">Tài khoản hiện tại</param>
        /// <param name="referID">Refer ID</param>
        /// <param name="status">StatusOfAssetTimeSheet</param>
        /// <param name="endLocationID"></param>
        public static void FLMCreate(DataEntities model, AccountItem account, int referID, SYSVarType status, int? endLocationID)
        {
            try
            {
                int assetID = -1;
                DateTime? fDate = null; DateTime? tDate = null;
                switch (status)
                {
                    case SYSVarType.StatusOfAssetTimeSheetCOTOMaster:
                        break;
                    case SYSVarType.StatusOfAssetTimeSheetDITOMaster:
                        break;
                    case SYSVarType.StatusOfAssetTimeSheetRepair:
                    case SYSVarType.StatusOfAssetTimeSheetMaintence:
                    case SYSVarType.StatusOfAssetTimeSheetRegistry:
                        var objActivity = model.FLM_Activity.Where(c => c.ID == referID).Select(c => new { c.AssetID, c.EffectiveDateFrom, c.EffectiveDateTo }).FirstOrDefault();
                        if (objActivity != null)
                        {
                            assetID = objActivity.AssetID;
                            fDate = objActivity.EffectiveDateFrom;
                            tDate = objActivity.EffectiveDateTo;
                        }
                        break;
                }
                if (fDate != null && tDate != null && assetID > 0)
                {
                    var obj = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.AssetID == assetID && c.ReferID == referID && c.StatusOfAssetTimeSheetID == -(int)status);
                    if (obj == null)
                    {
                        obj = new FLM_AssetTimeSheet();
                        obj.AssetID = assetID;
                        obj.ReferID = referID;
                        obj.StatusOfAssetTimeSheetID = -(int)status;
                        obj.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
                        obj.CreatedBy = account.UserName;
                        obj.CreatedDate = DateTime.Now;
                    }
                    else
                    {
                        obj.ModifiedBy = account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.DateFrom = fDate.Value;
                    obj.DateTo = tDate.Value;
                    obj.DateFromActual = fDate.Value;
                    obj.DateToActual = tDate.Value;
                    if (endLocationID > 0)
                    {
                        obj.LocationEndPlanID = endLocationID;
                        obj.LocationEndRealID = endLocationID;
                    }

                    if (obj.ID < 1)
                        model.FLM_AssetTimeSheet.Add(obj);
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

        public static void Remove(DataEntities model, AccountItem account, int referID, SYSVarType status)
        {
            try
            {
                foreach (var item in model.FLM_AssetTimeSheet.Where(c => c.ReferID == referID && c.StatusOfAssetTimeSheetID == -(int)status))
                {
                    foreach (var detail in model.FLM_AssetTimeSheetDriver.Where(c => c.AssetTimeSheetID == item.ID))
                        model.FLM_AssetTimeSheetDriver.Remove(detail);
                    model.FLM_AssetTimeSheet.Remove(item);
                }
                model.SaveChanges();
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

        public static void ChangePlan(DataEntities model, AccountItem account, int referID, SYSVarType status, DateTime etd, DateTime eta)
        {
            foreach (var item in model.FLM_AssetTimeSheet.Where(c => c.ReferID == referID && c.StatusOfAssetTimeSheetID == -(int)status))
            {
                item.DateFrom = etd;
                item.DateFromActual = etd;
                item.DateTo = eta;
                item.DateToActual = eta;
            }
            model.SaveChanges();
        }

        public static DTOFLMAssetTimeSheetCheck CheckComplete(DataEntities model, AccountItem acc, int timeSheetID)
        {
            try
            {
                var result = new DTOFLMAssetTimeSheetCheck();
                result.DateFrom = DateTime.Now;
                result.DateTo = DateTime.Now;
                result.ListTimeSheetConflict = new List<DTOFLMAssetTimeSheet>();
                var objTime = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ID == timeSheetID);
                if (objTime == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy timesheetID: " + timeSheetID);

                var objActivity = model.FLM_Activity.FirstOrDefault(c => c.ID == objTime.ReferID);
                if (objActivity == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy activity : " + objTime.ReferID);

                bool IsNewTimeSheet = true;

                result.DateFrom = objActivity.PlanDateFrom;
                result.DateTo = objActivity.PlanDateTo;

                if (objActivity.ActivityRepeatID > -(int)SYSVarType.ActivityRepeatNone)
                {
                    var totalRepeat = model.FLM_AssetTimeSheet.Where(c => c.ReferID == objActivity.ID && c.StatusOfAssetTimeSheetID == objTime.StatusOfAssetTimeSheetID && c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetComplete).Count();
                    if (objActivity.TotalRecall > 0 && totalRepeat >= objActivity.TotalRecall)//lap lai chinh xac n lan
                    {
                        IsNewTimeSheet = false;
                    }

                    if (IsNewTimeSheet)
                    {
                        switch (objActivity.ActivityRepeatID)
                        {
                            default:
                                break;
                            case -(int)SYSVarType.ActivityRepeatDay:
                                result.DateFrom = result.DateFrom.AddDays((totalRepeat + 1) * objActivity.TotalRepeat);
                                result.DateTo = result.DateTo.AddDays((totalRepeat + 1) * objActivity.TotalRepeat);
                                break;
                            case -(int)SYSVarType.ActivityRepeatWeek:
                                result.DateFrom = result.DateFrom.AddDays((totalRepeat + 1) * objActivity.TotalRepeat * 7);
                                result.DateTo = result.DateTo.AddDays((totalRepeat + 1) * objActivity.TotalRepeat * 7);
                                break;
                            case -(int)SYSVarType.ActivityRepeatMonth:
                                result.DateFrom = result.DateFrom.AddMonths((totalRepeat + 1) * objActivity.TotalRepeat);
                                result.DateTo = result.DateTo.AddMonths((totalRepeat + 1) * objActivity.TotalRepeat);
                                break;
                            case -(int)SYSVarType.ActivityRepeatYear:
                                result.DateFrom = result.DateFrom.AddYears((totalRepeat + 1) * objActivity.TotalRepeat);
                                result.DateTo = result.DateTo.AddYears((totalRepeat + 1) * objActivity.TotalRepeat);
                                break;
                        }

                        result.ListTimeSheetConflict.AddRange(model.FLM_AssetTimeSheet.Where(c => c.AssetID == objActivity.AssetID && c.DateFrom <= result.DateFrom && result.DateTo <= c.DateTo).Select(c => new DTOFLMAssetTimeSheet
                        {
                            ID = c.ID,
                            DateFrom = c.DateFrom,
                            DateTo = c.DateTo,
                            Note = c.Note,
                            StatusOfAssetTimeSheetID = c.StatusOfAssetTimeSheetID,
                            StatusOfAssetTimeSheetName = c.SYS_Var.ValueOfVar,
                        }).ToList());
                    }
                }

                result.IsConflict = result.ListTimeSheetConflict.Count() > 0;

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

        public static void Complete(DataEntities model, AccountItem acc, int timeSheetID, DTOFLMAssetTimeSheetCheck item)
        {
            try
            {
                var obj = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ID == timeSheetID);
                if (obj != null)
                {
                    var objTime = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ID == timeSheetID);
                    if (objTime == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy timesheetID: " + timeSheetID);

                    var objActivity = model.FLM_Activity.FirstOrDefault(c => c.ID == objTime.ReferID);
                    if (objActivity == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy activity : " + objTime.ReferID);

                    if (item.IsConflict)
                    {
                        if (model.FLM_AssetTimeSheet.Count(c => c.AssetID == objActivity.AssetID && c.DateFrom <= item.DateFrom && item.DateTo <= c.DateTo) > 0)
                            throw FaultHelper.BusinessFault(null, null, "Thời gian bị trùng");
                    }

                    if (objActivity.ActivityRepeatID == -(int)SYSVarType.ActivityRepeatNone)
                    {
                        objTime.ModifiedBy = acc.UserName;
                        objTime.ModifiedDate = DateTime.Now;
                        objTime.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;

                        objActivity.ModifiedBy = acc.UserName;
                        objActivity.ModifiedDate = DateTime.Now;
                        objActivity.IsClosed = true;
                    }
                    else if (objActivity.ActivityRepeatID > -(int)SYSVarType.ActivityRepeatNone)
                    {
                        bool IsNewTimeSheet = true;
                        var totalRepeat = model.FLM_AssetTimeSheet.Where(c => c.ReferID == objActivity.ID && c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetComplete).Count();
                        if (objActivity.TotalRecall > 0 && totalRepeat >= objActivity.TotalRecall - 1)//lap lai chinh xac n lan
                        {
                            IsNewTimeSheet = false;

                            objActivity.ModifiedBy = acc.UserName;
                            objActivity.ModifiedDate = DateTime.Now;
                            objActivity.IsClosed = true;
                        }

                        if (IsNewTimeSheet)
                        {
                            var status = -1;
                            switch (objActivity.TypeOfActivityID)
                            {
                                default:
                                    break;
                                case -(int)SYSVarType.TypeOfActivityMaintence: status = (int)SYSVarType.StatusOfAssetTimeSheetMaintence; break;
                                case -(int)SYSVarType.TypeOfActivityRegistry: status = (int)SYSVarType.StatusOfAssetTimeSheetRegistry; break;
                                case -(int)SYSVarType.TypeOfActivityRepairLarge:
                                case -(int)SYSVarType.TypeOfActivityRepairSmall: status = (int)SYSVarType.StatusOfAssetTimeSheetRepair; break;
                            }
                            if (status == -1) throw FaultHelper.BusinessFault(null, null, "Status không chính xác");

                            FLM_AssetTimeSheet objNew = new FLM_AssetTimeSheet();
                            objNew.CreatedBy = acc.UserName;
                            objNew.CreatedDate = DateTime.Now;
                            objNew.ReferID = objActivity.ID;
                            objNew.StatusOfAssetTimeSheetID = -(int)status;
                            objNew.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
                            objNew.AssetID = objActivity.AssetID;
                            objNew.DateFrom = item.DateFrom;
                            objNew.DateTo = item.DateTo;
                            objNew.DateFromActual = item.DateFrom;
                            objNew.DateToActual = item.DateTo;
                            objNew.LocationEndPlanID = objTime.LocationEndPlanID;
                            objNew.LocationEndRealID = objTime.LocationEndPlanID;
                            model.FLM_AssetTimeSheet.Add(objNew);
                        }
                        objTime.ModifiedBy = acc.UserName;
                        objTime.ModifiedDate = DateTime.Now;
                        objTime.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
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

        public static void COTOMaster_RomoocInStock(DataEntities model, AccountItem account, int masterid, double timeinstock)
        {
            var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid && c.RomoocID > 0).Select(c => new { c.RomoocID }).FirstOrDefault();
            if (objMaster != null)
            {
                var assetRomooc = model.FLM_Asset.Where(c => c.RomoocID == objMaster.RomoocID.Value).Select(c => new { c.ID }).FirstOrDefault();
                if (assetRomooc != null)
                {
                    var last = model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster).Select(c => new { c.DateToActual }).OrderByDescending(c => c.DateToActual).FirstOrDefault();
                    if (last != null)
                    {
                        var dtFrom = last.DateToActual.AddHours(0.1);
                        var dtTo = dtFrom.AddHours(timeinstock);

                        var obj = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ReferID == masterid && c.AssetID == assetRomooc.ID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterInStock);
                        if (obj == null)
                        {
                            obj = new FLM_AssetTimeSheet();
                            obj.ReferID = masterid;
                            obj.AssetID = assetRomooc.ID;
                            obj.StatusOfAssetTimeSheetID = -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterInStock;
                            obj.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
                            obj.CreatedBy = account.UserName;
                            obj.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            obj.ModifiedBy = account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        obj.DateFrom = dtFrom;
                        obj.DateFromActual = dtFrom;
                        obj.DateTo = dtTo;
                        obj.DateToActual = dtTo;
                        if (obj.ID < 1)
                            model.FLM_AssetTimeSheet.Add(obj);
                        model.SaveChanges();
                    }
                }
            }
        }

        public static void COTOMaster_RomoocEmpty(DataEntities model, AccountItem account, int masterid, double timeempty)
        {
            var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid && c.RomoocID > 0).Select(c => new { c.RomoocID }).FirstOrDefault();
            if (objMaster != null)
            {
                var assetRomooc = model.FLM_Asset.Where(c => c.RomoocID == objMaster.RomoocID.Value).Select(c => new { c.ID }).FirstOrDefault();
                if (assetRomooc != null)
                {
                    var last = model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterInStock).Select(c => new { c.DateToActual }).OrderByDescending(c => c.DateToActual).FirstOrDefault();
                    if (last == null)
                        last = model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster).Select(c => new { c.DateToActual }).OrderByDescending(c => c.DateToActual).FirstOrDefault();
                    if (last != null)
                    {
                        var dtFrom = last.DateToActual.AddHours(0.1);
                        var dtTo = dtFrom.AddHours(timeempty);

                        var obj = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ReferID == masterid && c.AssetID == assetRomooc.ID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterEmpty);
                        if (obj == null)
                        {
                            obj = new FLM_AssetTimeSheet();
                            obj.ReferID = masterid;
                            obj.AssetID = assetRomooc.ID;
                            obj.StatusOfAssetTimeSheetID = -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterEmpty;
                            obj.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
                            obj.CreatedBy = account.UserName;
                            obj.CreatedDate = DateTime.Now; 
                        }
                        else
                        {
                            obj.ModifiedBy = account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        obj.DateFrom = dtFrom;
                        obj.DateFromActual = dtFrom;
                        obj.DateTo = dtTo;
                        obj.DateToActual = dtTo;
                        if (obj.ID < 1)
                            model.FLM_AssetTimeSheet.Add(obj);
                        model.SaveChanges();
                    }
                }
            }
        }

        public static void COTOMaster_RomoocLaden(DataEntities model, AccountItem account, int masterid, double timeladen)
        {
            var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid && c.RomoocID > 0).Select(c => new { c.RomoocID }).FirstOrDefault();
            if (objMaster != null)
            {
                var assetRomooc = model.FLM_Asset.Where(c => c.RomoocID == objMaster.RomoocID.Value).Select(c => new { c.ID }).FirstOrDefault();
                if (assetRomooc != null)
                {
                    var last = model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterInStock).Select(c => new { c.DateToActual }).OrderByDescending(c => c.DateToActual).FirstOrDefault();
                    if (last == null)
                        last = model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster).Select(c => new { c.DateToActual }).OrderByDescending(c => c.DateToActual).FirstOrDefault();
                    if (last != null)
                    {
                        var dtFrom = last.DateToActual.AddHours(0.1);
                        var dtTo = dtFrom.AddHours(timeladen);

                        var obj = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ReferID == masterid && c.AssetID == assetRomooc.ID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterEmpty);
                        if (obj == null)
                        {
                            obj = new FLM_AssetTimeSheet();
                            obj.ReferID = masterid;
                            obj.AssetID = assetRomooc.ID;
                            obj.StatusOfAssetTimeSheetID = -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterLaden;
                            obj.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
                            obj.CreatedBy = account.UserName;
                            obj.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            obj.ModifiedBy = account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        obj.DateFrom = dtFrom;
                        obj.DateFromActual = dtFrom;
                        obj.DateTo = dtTo;
                        obj.DateToActual = dtTo;
                        if (obj.ID < 1)
                            model.FLM_AssetTimeSheet.Add(obj);
                        model.SaveChanges();
                    }
                }
            }
        }

        public static void COTOMaster_RomoocWaitByMasterID(DataEntities model, AccountItem account, int masterid, int nextid)
        {
            var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid && c.RomoocID > 0).Select(c => new { c.RomoocID }).FirstOrDefault();
            if (objMaster != null)
            {
                var assetRomooc = model.FLM_Asset.Where(c => c.RomoocID == objMaster.RomoocID.Value).Select(c => new { c.ID }).FirstOrDefault();
                if (assetRomooc != null)
                {
                    var last = model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterInStock).Select(c => new { c.DateToActual }).OrderByDescending(c => c.DateToActual).FirstOrDefault();
                    if (last == null)
                        last = model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster).Select(c => new { c.DateToActual }).OrderByDescending(c => c.DateToActual).FirstOrDefault();
                    var next = model.FLM_AssetTimeSheet.Where(c => c.ReferID == nextid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster).Select(c => new { c.DateFromActual }).OrderBy(c => c.DateFromActual).FirstOrDefault();
                    if (last != null && next != null && (next.DateFromActual - last.DateToActual).TotalHours > 0.3)
                    {
                        var dtFrom = last.DateToActual.AddHours(0.1);
                        var dtTo = next.DateFromActual.AddHours(-0.1);

                        var obj = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ReferID == masterid && c.AssetID == assetRomooc.ID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterEmpty);
                        if (obj == null)
                        {
                            obj = new FLM_AssetTimeSheet();
                            obj.ReferID = masterid;
                            obj.AssetID = assetRomooc.ID;
                            obj.StatusOfAssetTimeSheetID = -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterLaden;
                            obj.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
                            obj.CreatedBy = account.UserName;
                            obj.CreatedDate = DateTime.Now;
                        }
                        else
                        {
                            obj.ModifiedBy = account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        obj.DateFrom = dtFrom;
                        obj.DateFromActual = dtFrom;
                        obj.DateTo = dtTo;
                        obj.DateToActual = dtTo;
                        if (obj.ID < 1)
                            model.FLM_AssetTimeSheet.Add(obj);
                        model.SaveChanges();
                    }
                }
            }
        }

        //public static void COTOMaster_RomoocSameMasterHasInStock(DataEntities model, AccountItem account, int masterid, DateTime etd1, DateTime eta1, DateTime etd2, DateTime eta2)
        //{
        //    var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid && c.RomoocID > 0).Select(c => new { c.RomoocID }).FirstOrDefault();
        //    if (objMaster != null)
        //    {
        //        double totalHours = (etd2 - eta1).TotalHours - 0.2;

        //        var assetRomooc = model.FLM_Asset.Where(c => c.RomoocID == objMaster.RomoocID.Value).Select(c => new { c.ID }).FirstOrDefault();
        //        if (assetRomooc != null && totalHours > 0)
        //        {
        //            foreach (var item in model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && c.AssetID == assetRomooc.ID))
        //                model.FLM_AssetTimeSheet.Remove(item);
        //            model.SaveChanges();

        //            var dtFrom = etd1;
        //            var dtTo = eta1;

        //            var obj = new FLM_AssetTimeSheet();
        //            obj.ReferID = masterid;
        //            obj.AssetID = assetRomooc.ID;
        //            obj.StatusOfAssetTimeSheetID = -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster;
        //            obj.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
        //            obj.CreatedBy = account.UserName;
        //            obj.CreatedDate = DateTime.Now;
        //            obj.DateFrom = dtFrom;
        //            obj.DateFromActual = dtFrom;
        //            obj.DateTo = dtTo;
        //            obj.DateToActual = dtTo;
        //            model.FLM_AssetTimeSheet.Add(obj);

        //            dtFrom = dtTo.AddHours(0.1);
        //            dtTo = etd2.AddHours(-0.1);

        //            if ((dtTo - dtFrom).TotalHours > 0.2)
        //            {
        //                obj = new FLM_AssetTimeSheet();
        //                obj.ReferID = masterid;
        //                obj.AssetID = assetRomooc.ID;
        //                obj.StatusOfAssetTimeSheetID = -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterInStock;
        //                obj.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
        //                obj.CreatedBy = account.UserName;
        //                obj.CreatedDate = DateTime.Now;
        //                obj.DateFrom = dtFrom;
        //                obj.DateFromActual = dtFrom;
        //                obj.DateTo = dtTo;
        //                obj.DateToActual = dtTo;
        //                model.FLM_AssetTimeSheet.Add(obj);
        //            }

        //            dtFrom = etd2;
        //            dtTo = eta2;

        //            obj = new FLM_AssetTimeSheet();
        //            obj.ReferID = masterid;
        //            obj.AssetID = assetRomooc.ID;
        //            obj.StatusOfAssetTimeSheetID = -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster;
        //            obj.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetOpen;
        //            obj.CreatedBy = account.UserName;
        //            obj.CreatedDate = DateTime.Now;
        //            obj.DateFrom = dtFrom;
        //            obj.DateFromActual = dtFrom;
        //            obj.DateTo = dtTo;
        //            obj.DateToActual = dtTo;
        //            model.FLM_AssetTimeSheet.Add(obj);

        //            model.SaveChanges();
        //        }
        //    }
        //}

        public static void COTOMaster_Remove(DataEntities model, AccountItem account, int masterid)
        {
            var lstStatus = new List<int>(){
                -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster,
                -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterEmpty,
                -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterInStock,
                -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterLaden,
            };
            if (model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && lstStatus.Contains(c.StatusOfAssetTimeSheetID)).Count() > 0)
            {
                foreach (var item in model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && lstStatus.Contains(c.StatusOfAssetTimeSheetID)))
                {
                    foreach (var detail in model.FLM_AssetTimeSheetDriver.Where(c => c.AssetTimeSheetID == item.ID))
                        model.FLM_AssetTimeSheetDriver.Remove(detail);
                    model.FLM_AssetTimeSheet.Remove(item);
                }
                model.SaveChanges();
            }
        }
    }
}
