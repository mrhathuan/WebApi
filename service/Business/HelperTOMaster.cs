using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data;
using DTO;
using System.ServiceModel;

namespace Business
{
    public class HelperTOMaster
    {
        //(check) 

        const string DICodePrefix = "DI";
        const string DICodeNum = "0000000";
        const string COCodePrefix = "CO";
        const string COCodeNum = "0000000";
        const int DefaultTruck = 1;
        const int DefaultTractor = 2;
        const int DefaultRomooc = 1;
        const double HourMatrixDefault = 1;
        const double HourInStockDefault = 1;
        const double HourMin = 0.1;
        const double HourEmpty = 0.01;

        const double MaxHourSamePoint = 0.5;
        const double Hour2Point = 0.3;

        #region Container

        #region OPS
        private static string OPSCO_GetLastCode(DataEntities model)
        {
            long idx = 1;
            var last = model.OPS_COTOMaster.OrderByDescending(c => c.ID).Select(c => new { c.ID }).FirstOrDefault();
            if (last != null)
                idx = Convert.ToInt64(last.ID) + 1;
            else
                idx = 1;
            return COCodePrefix + idx.ToString(COCodeNum);
        }

        private static void OPSCO_CheckingTime(DataEntities model, AccountItem account, int mID, int? vehicleID, int? romoocID, DateTime? ETD, DateTime? ETA, bool isContainer)
        {
            try
            {
                var objSetting = OPSCO_GetSetting(model, account);

                if (objSetting != null && ETA.HasValue && ETD.HasValue)
                {
                    var strETD = String.Format("{0:d/M HH:mm}", ETD.Value);
                    var strETA = String.Format("{0:d/M HH:mm}", ETA.Value);

                    if (objSetting.HasConstraintTimeOPS)
                    {
                        if (isContainer)
                        {
                            var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == vehicleID);
                            if (vehicleID > 0 && objVehicle == null)
                                throw FaultHelper.BusinessFault(null, null, "Đầu kéo không tồn tại.");
                            var objRomooc = model.CAT_Vehicle.FirstOrDefault(c => c.ID == romoocID);
                            if (romoocID > 0 && objVehicle == null)
                                throw FaultHelper.BusinessFault(null, null, "Romooc không tồn tại.");

                            var objV = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == account.SYSCustomerID && c.FLM_Asset.VehicleID == vehicleID
                                && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.ReferID != mID
                              && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                            if (objV != null)
                            {
                                var objTo = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objV.ReferID);
                                if (objTo != null)
                                    throw FaultHelper.BusinessFault(null, null, "Đầu kéo " + objVehicle.RegNo + " đã phân chuyến. Số: " + objTo.Code + ", từ: " + String.Format("{0:d/M HH:mm}", objV.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objV.DateToActual));
                            }
                            else
                            {
                                objV = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == account.SYSCustomerID
                                    && c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.FLM_Asset.VehicleID == vehicleID
                                    && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                                if (objV != null)
                                {
                                    var strCode = string.Empty;
                                    switch (objV.StatusOfAssetTimeSheetID)
                                    {
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetMaintence:
                                            strCode = "bảo trì";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRegistry:
                                            strCode = "đăng kiểm";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRepair:
                                            strCode = "sửa chữa";
                                            break;
                                    }
                                    throw FaultHelper.BusinessFault(null, null, "Đầu kéo " + objVehicle.RegNo + " bận " + strCode + ", từ: " + String.Format("{0:d/M HH:mm}", objV.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objV.DateToActual));
                                }
                            }

                            var objR = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == account.SYSCustomerID && c.FLM_Asset.RomoocID == romoocID
                                && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.ReferID != mID
                                && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                            if (objR != null)
                            {
                                var objTo = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objV.ReferID);
                                if (objTo != null)
                                    throw FaultHelper.BusinessFault(null, null, "Romooc " + objRomooc.RegNo + "đã phân chuyến. Số: " + objTo.Code + ", từ: " + String.Format("{0:d/M HH:mm}", objR.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objR.DateToActual));
                            }
                            else
                            {
                                objR = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == account.SYSCustomerID
                                    && c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.FLM_Asset.RomoocID == romoocID
                                    && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                                if (objR != null)
                                {
                                    var strCode = string.Empty;
                                    switch (objR.StatusOfAssetTimeSheetID)
                                    {
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetMaintence:
                                            strCode = "bảo trì";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRegistry:
                                            strCode = "đăng kiểm";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRepair:
                                            strCode = "sửa chữa";
                                            break;
                                    }
                                    throw FaultHelper.BusinessFault(null, null, "Romooc " + objRomooc.RegNo + " bận " + strCode + ", từ: " + String.Format("{0:d/M HH:mm}", objR.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objR.DateToActual));
                                }
                            }
                        }
                        else
                        {
                            var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == vehicleID);
                            if (vehicleID >= 0 && objVehicle == null)
                                throw FaultHelper.BusinessFault(null, null, "Xe không tồn tại.");
                            var objV = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == account.SYSCustomerID && c.FLM_Asset.VehicleID == vehicleID
                                && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster && c.ReferID != mID
                                && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                            if (objV != null)
                            {
                                var objTo = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == objV.ReferID);
                                if (objTo != null)
                                    throw FaultHelper.BusinessFault(null, null, "Xe " + objVehicle.RegNo + " đã phân chuyến. Số: " + objTo.Code + ", từ: " + String.Format("{0:d/M HH:mm}", objV.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objV.DateToActual));
                            }
                            else
                            {
                                objV = model.FLM_AssetTimeSheet.Where(c => c.FLM_Asset.SYSCustomerID == account.SYSCustomerID
                                    && c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster && c.FLM_Asset.VehicleID == vehicleID
                                    && !((c.DateFromActual <= ETD && c.DateToActual <= ETD) || (c.DateFromActual >= ETA && c.DateToActual >= ETA))).OrderBy(c => c.DateFromActual).FirstOrDefault();
                                if (objV != null)
                                {
                                    var strCode = string.Empty;
                                    switch (objV.StatusOfAssetTimeSheetID)
                                    {
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetMaintence:
                                            strCode = "bảo trì";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRegistry:
                                            strCode = "đăng kiểm";
                                            break;
                                        case -(int)SYSVarType.StatusOfAssetTimeSheetRepair:
                                            strCode = "sửa chữa";
                                            break;
                                    }
                                    throw FaultHelper.BusinessFault(null, null, "Xe " + objVehicle.RegNo + " bận " + strCode + ", từ: " + String.Format("{0:d/M HH:mm}", objV.DateFromActual) + " đến: " + String.Format("{0:d/M HH:mm}", objV.DateToActual));
                                }
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

        //get setting   
        private static DTOSYSSetting OPSCO_GetSetting(DataEntities model, AccountItem account)
        {
            DTOSYSSetting objSetting = new DTOSYSSetting();
            var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
            if (!string.IsNullOrEmpty(sSet))
            {
                objSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
                if (objSetting != null)
                {
                    var objCheck = model.CAT_Location.FirstOrDefault(c => c.ID == objSetting.LocationFromID);
                    if (objCheck == null)
                        throw FaultHelper.BusinessFault(null, null, "setting can't find location from");
                    objCheck = model.CAT_Location.FirstOrDefault(c => c.ID == objSetting.LocationToID);
                    if (objCheck == null)
                        throw FaultHelper.BusinessFault(null, null, "setting can't find location to");
                    objCheck = model.CAT_Location.FirstOrDefault(c => c.ID == objSetting.LocationRomoocReturnID);
                    if (objCheck == null)
                        throw FaultHelper.BusinessFault(null, null, "setting can't find romooc location");
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "setting fail");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "setting fail");

            return objSetting;
        }

        //reset sort, etd, eta
        private static void OPSCO_TOContainerResetSort(DataEntities model, AccountItem account, List<int> lstopsconid)
        {
            if (lstopsconid != null && lstopsconid.Count > 0)
            {
                if (model.OPS_COTOContainer.Where(c => lstopsconid.Contains(c.OPSContainerID) && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerReturnEmptyFail).Count() > 0)
                    throw FaultHelper.BusinessFault(null, null, "list has return empty fail");

                var lsTOContainer = model.OPS_COTOContainer.Where(c => lstopsconid.Contains(c.OPSContainerID)).ToList();
                foreach (var opsconid in lstopsconid)
                {
                    var time = OPSCO_TOContainerResetSort_Child(model, account, lsTOContainer, opsconid, null, 1);
                    var objOPSContainer = model.OPS_Container.Where(c => c.ID == opsconid).Select(c => new { c.ContainerID }).FirstOrDefault();
                    if (objOPSContainer != null)
                    {
                        var objORDContainer = model.ORD_Container.FirstOrDefault(c => c.ID == objOPSContainer.ContainerID);
                        if (objORDContainer != null)
                        {
                            objORDContainer.ETD = time.ETD;
                            objORDContainer.ETA = time.ETA;
                            objORDContainer.ModifiedBy = account.UserName;
                            objORDContainer.ModifiedDate = DateTime.Now;
                        }
                    }
                }
                model.SaveChanges();
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list null");
        }

        private static HelperTOMaster_Time OPSCO_TOContainerResetSort_Child(DataEntities model, AccountItem account, List<OPS_COTOContainer> lst, int opsconid, int? parentid, int parentsort)
        {
            var result = default(HelperTOMaster_Time);
            var query = lst.Where(c => c.OPSContainerID == opsconid && c.ParentID == null);
            if (parentid > 0)
                query = lst.Where(c => c.OPSContainerID == opsconid && c.ParentID == parentid.Value);
            int sort = parentsort;
            foreach (var item in query)
            {
                item.SortOrder = sort;
                var dtChild = OPSCO_TOContainerResetSort_Child(model, account, lst, opsconid, item.ID, sort);

                if (parentid > 0)
                    sort++;
                else
                    sort += 20;

                if (dtChild != null)
                {
                    result = dtChild;
                    item.ETD = dtChild.ETD;
                    item.ETDStart = dtChild.ETD;
                    item.ETA = dtChild.ETA;
                    item.ETAStart = dtChild.ETA;
                }
                else if (result == null)
                {
                    result = new HelperTOMaster_Time();
                    result.ETD = item.ETD.Value;
                    result.ETA = item.ETA.Value;
                }
                if (result.ETD.CompareTo(item.ETD.Value) > 0)
                    result.ETD = item.ETD.Value;
                if (result.ETA.CompareTo(item.ETA.Value) < 0)
                    result.ETA = item.ETA.Value;
            }
            return result;
        }

        //save data container 
        private static void OPSCO_CreateMaster_CON(DataEntities model, AccountItem account, int masterid, int? parentdataid, int? parentid, List<HelperTOMaster_TOContainer> lstOPSCON)
        {
            if (parentid == null)
            {
                var lstCON = new List<HelperTOMaster_TOContainer>();
                var lstCONData = new List<OPS_COTOContainer>();
                foreach (var tocon in lstOPSCON.Where(c => c.ParentID == null))
                {
                    var obj = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == tocon.ID);
                    if (obj == null)
                    {
                        obj = new OPS_COTOContainer();
                        obj.CreatedBy = account.UserName;
                        obj.CreatedDate = DateTime.Now;

                        obj.LocationFromID = tocon.LocationFromID;
                        obj.LocationToID = tocon.LocationToID;
                        obj.OPSContainerID = tocon.OPSContainerID;
                        obj.IsSwap = false;
                        obj.IsInput = false;
                        obj.TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait;
                        obj.CreateByMasterID = masterid;
                    }
                    else
                    {
                        obj.ModifiedBy = account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.COTOSort = tocon.SortLocation;
                    obj.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                    obj.StatusOfCOContainerID = tocon.StatusOfCOContainerID;
                    obj.COTOMasterID = masterid;
                    obj.ETD = tocon.ETD;
                    obj.ETA = tocon.ETA;
                    obj.ETDStart = tocon.ETDStart;
                    obj.ETAStart = tocon.ETAStart;
                    obj.SortOrder = tocon.SortOrder;
                    obj.IsSplit = tocon.IsSplit;

                    if (obj.ID < 1)
                        model.OPS_COTOContainer.Add(obj);

                    lstCON.Add(tocon);
                    lstCONData.Add(obj);
                }
                model.SaveChanges();

                for (int i = 0; i < lstCON.Count; i++)
                {
                    var itemCON = lstCON[i];
                    var itemCONData = lstCONData[i];

                    OPSCO_CreateMaster_CON(model, account, masterid, itemCONData.ID, itemCON.ID, lstOPSCON);
                }
            }
            else
            {
                var lstCON = new List<HelperTOMaster_TOContainer>();
                var lstCONData = new List<OPS_COTOContainer>();
                foreach (var tocon in lstOPSCON.Where(c => c.ParentID == parentid))
                {
                    var obj = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == tocon.ID);
                    if (obj == null)
                    {
                        obj = new OPS_COTOContainer();
                        obj.CreatedBy = account.UserName;
                        obj.CreatedDate = DateTime.Now;

                        if (parentdataid > 0)
                            obj.ParentID = parentdataid;
                        obj.LocationFromID = tocon.LocationFromID;
                        obj.LocationToID = tocon.LocationToID;
                        obj.OPSContainerID = tocon.OPSContainerID;
                        obj.IsSwap = false;
                        obj.IsInput = false;
                        obj.TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait;
                        obj.CreateByMasterID = masterid;
                    }
                    else
                    {
                        obj.ModifiedBy = account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.COTOSort = tocon.SortLocation;
                    obj.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                    obj.StatusOfCOContainerID = tocon.StatusOfCOContainerID;
                    obj.COTOMasterID = masterid;
                    obj.ETD = tocon.ETD;
                    obj.ETA = tocon.ETA;
                    obj.ETDStart = tocon.ETDStart;
                    obj.ETAStart = tocon.ETAStart;
                    obj.SortOrder = tocon.SortOrder;
                    obj.IsSplit = tocon.IsSplit;

                    if (obj.ID < 1)
                        model.OPS_COTOContainer.Add(obj);

                    lstCON.Add(tocon);
                    lstCONData.Add(obj);
                }

                model.SaveChanges();

                for (int i = 0; i < lstCON.Count; i++)
                {
                    var itemCON = lstCON[i];
                    var itemCONData = lstCONData[i];

                    OPSCO_CreateMaster_CON(model, account, masterid, itemCONData.ID, itemCON.ID, lstOPSCON);
                }
            }
        }


        //gen data 1 change container time 
        private static void OPSCO_CreateItem_ContainerTime(DataEntities model, AccountItem account, DTOOPSCOTOMaster master, HelperTOMaster_COParam param)
        {
            var lstCONID = master.ListCOContainer.Select(c => c.ID).Distinct().ToList();
            if (lstCONID.Count > 0)
            {
                foreach (var item in master.ListCOContainer)
                {
                    var objCON = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item.ID);
                    if (objCON != null)
                    {
                        objCON.ETD = item.ETD;
                        objCON.ETA = item.ETA;
                    }
                }
                model.SaveChanges();

                //var lstOPSContainerID = model.OPS_COTOContainer.Where(c => lstCONID.Contains(c.ID)).Select(c => c.OPSContainerID).Distinct().ToList();
                //foreach (var opsContainerID in lstOPSContainerID)
                //{
                //    var lstTOContainer = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opsContainerID && lstCONID.Contains(c.ID)).OrderBy(c => c.SortOrder).ToList();
                //    if (lstTOContainer.Count > 0)
                //    {
                //        var first = lstTOContainer[0];
                //        DateTime dtCheck = first.ETD.Value;
                //        foreach (var item in lstTOContainer)
                //        {
                //            if (dtCheck.CompareTo(item.ETD.Value) > 0)
                //                item.ETD = dtCheck;
                //            if (item.ETD.Value.CompareTo(item.ETA.Value) == 0)
                //            {
                //                item.ETA = item.ETA.Value.AddHours(param.Hour2PointCON);
                //            }
                //            else if (item.ETD.Value.CompareTo(item.ETA.Value) > 0)
                //            {
                //                item.ETA = item.ETD.Value.AddHours(param.Hour2PointCON);
                //            }
                //            item.ETAStart = item.ETA;
                //            dtCheck = item.ETA.Value;
                //        }

                //        var last = lstTOContainer[lstTOContainer.Count - 1];
                //        //if etd, eta different
                //        if (first.ETD.Value.CompareTo(param.DateStart.Value) < 0 && last.ETA.Value.CompareTo(param.DateEnd.Value) < 0)
                //        {
                //            var dtCurrent = param.DateStart.Value.AddHours(param.Hour2Point);
                //            foreach (var item in lstTOContainer)
                //            {
                //                item.ETD = dtCurrent;
                //                dtCurrent = dtCurrent.AddHours(param.Hour2PointCON);
                //                item.ETA = dtCurrent;
                //            }
                //            last.ETA = param.DateEnd.Value;
                //        }
                //        //if etd different
                //        else if (first.ETD.Value.CompareTo(param.DateStart.Value) < 0)
                //        {
                //            var sub = param.DateStart.Value - first.ETD.Value;
                //            var houradd = Math.Round(sub.TotalHours, 2, MidpointRounding.AwayFromZero);
                //            foreach (var item in lstTOContainer)
                //            {
                //                item.ETD = item.ETD.Value.AddHours(houradd);
                //                item.ETA = item.ETA.Value.AddHours(houradd);
                //            }
                //            first.ETD = param.DateStart.Value.AddHours(param.Hour2Point);
                //            last.ETA = param.DateEnd.Value;
                //        }
                //        //if eta different
                //        else if (last.ETA.Value.CompareTo(param.DateEnd.Value) < 0)
                //        {
                //            var sub = param.DateEnd.Value - last.ETA.Value;
                //            var hoursub = -Math.Round(sub.TotalHours, 2, MidpointRounding.AwayFromZero);
                //            foreach (var item in lstTOContainer)
                //            {
                //                item.ETD = item.ETD.Value.AddHours(hoursub);
                //                item.ETA = item.ETA.Value.AddHours(hoursub);
                //            }
                //            first.ETD = param.DateStart.Value.AddHours(param.Hour2Point);
                //            last.ETA = param.DateEnd.Value;
                //        }
                //    }
                //    else
                //        throw FaultHelper.BusinessFault(null, null, "list container fail");
                //}
                //model.SaveChanges();
            }
        }

        //gen data 2 first TO for container
        private static List<HelperTOMaster_TOContainer> OPSCO_CreateItem_FirstTO(DataEntities model, List<int> lstCONID, HelperTOMaster_COParam param)
        {
            //Create first to
            List<HelperTOMaster_TOContainer> result = model.OPS_COTOContainer.Where(c => lstCONID.Contains(c.ID)).Select(c => new HelperTOMaster_TOContainer
            {
                ID = c.ID,
                ParentID = c.ParentID,
                OPSContainerID = c.OPSContainerID,
                LocationFromID = c.LocationFromID,
                LocationToID = c.LocationToID,
                ETD = c.ETD.Value,
                ETA = c.ETA.Value,
                ETDStart = c.ETDStart,
                ETAStart = c.ETAStart,
                SortOrder = c.SortOrder,
                StatusOfCOContainerID = c.StatusOfCOContainerID,
                ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID > 0 ? c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID : -1,
                CATTransportModeID = c.OPS_Container.ORD_Container.ORD_Order.TransportModeID,
                IsSplit = c.IsSplit,
                IsCreate = false,
                HasCOTO = false
            }).ToList();

            //Create start
            var lstCheckStatusID = new List<int>() { -(int)SYSVarType.StatusOfCOContainerIMLaden, -(int)SYSVarType.StatusOfCOContainerEXEmpty, -(int)SYSVarType.StatusOfCOContainerLOGetEmpty, -(int)SYSVarType.StatusOfCOContainerLOEmpty, -(int)SYSVarType.StatusOfCOContainerLOLaden };
            var lstParentNULL = result.Where(c => c.ParentID == null && lstCheckStatusID.Contains(c.StatusOfCOContainerID) && c.IsSplit == false).ToList();
            foreach (var parentNULL in lstParentNULL)
            {
                parentNULL.IsSplit = true;
                var dtStart = param.DateStart.Value;
                var dtEnd = param.DateStart.Value;
                switch (parentNULL.StatusOfCOContainerID)
                {
                    case -(int)SYSVarType.StatusOfCOContainerIMLaden:
                        if (param.LocationStartID == param.LocationGetRomoocID && param.LocationFromID != param.LocationGetRomoocID)
                        {
                            result.Add(new HelperTOMaster_TOContainer
                            {
                                ID = -result.Count,
                                ParentID = parentNULL.ID,
                                OPSContainerID = parentNULL.OPSContainerID,
                                LocationFromID = param.LocationFromID.Value,
                                LocationToID = param.LocationStartID.Value,
                                ETD = dtStart,
                                ETA = param.DateGetRomooc.Value,
                                SortOrder = parentNULL.SortOrder - 4,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetRomooc,
                                TypeOfTOLocation = SYSVarType.TypeOfTOLocationGetRomooc,
                                IsSplit = false,
                                IsCreate = true,
                                HasCOTO = false
                            });
                            dtStart = param.DateGetRomooc.Value;
                        }
                        dtEnd = parentNULL.ETD;
                        if (dtStart.CompareTo(dtEnd) >= 0)
                        {
                            dtEnd = dtStart.AddHours(param.Hour2Point);
                            if (dtEnd.CompareTo(parentNULL.ETA) >= 0)
                            {
                                parentNULL.ETA = dtEnd.AddHours(param.Hour2Point);
                                //throw FaultHelper.BusinessFault(null, null, "container " + parentNULL.SortOrder + " etd,eta fail");
                            }
                        }
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = param.LocationStartID.Value,
                            LocationToID = parentNULL.LocationFromID,
                            ETD = dtStart,
                            ETA = dtEnd,
                            SortOrder = parentNULL.SortOrder - 3,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetLaden,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = parentNULL.LocationFromID,
                            LocationToID = parentNULL.LocationToID,
                            ETD = dtEnd,
                            ETA = parentNULL.ETA,
                            SortOrder = parentNULL.SortOrder - 2,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipLaden,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationStock,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        break;
                    case -(int)SYSVarType.StatusOfCOContainerEXEmpty:
                    case -(int)SYSVarType.StatusOfCOContainerLOGetEmpty:
                        if (param.LocationStartID == param.LocationGetRomoocID && param.LocationFromID != param.LocationGetRomoocID)
                        {
                            result.Add(new HelperTOMaster_TOContainer
                            {
                                ID = -result.Count,
                                ParentID = parentNULL.ID,
                                OPSContainerID = parentNULL.OPSContainerID,
                                LocationFromID = param.LocationFromID.Value,
                                LocationToID = param.LocationStartID.Value,
                                ETD = dtStart,
                                ETA = param.DateGetRomooc.Value,
                                SortOrder = parentNULL.SortOrder - 3,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetRomooc,
                                TypeOfTOLocation = SYSVarType.TypeOfTOLocationGetRomooc,
                                IsSplit = false,
                                IsCreate = true,
                                HasCOTO = false
                            });
                            dtStart = param.DateGetRomooc.Value;
                        }
                        dtEnd = parentNULL.ETD;
                        if (dtStart.CompareTo(dtEnd) >= 0)
                        {
                            dtEnd = dtStart.AddHours(param.Hour2Point);
                            if (dtEnd.CompareTo(parentNULL.ETA) >= 0)
                            {
                                parentNULL.ETA = dtEnd.AddHours(param.Hour2Point);
                                //throw FaultHelper.BusinessFault(null, null, "container " + parentNULL.SortOrder + " etd,eta fail");
                            }
                        }
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = param.LocationStartID.Value,
                            LocationToID = parentNULL.LocationFromID,
                            ETD = dtStart,
                            ETA = dtEnd,
                            SortOrder = parentNULL.SortOrder - 2,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetEmpty,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationDepot,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = parentNULL.LocationFromID,
                            LocationToID = parentNULL.LocationToID,
                            ETD = dtEnd,
                            ETA = parentNULL.ETA,
                            SortOrder = parentNULL.SortOrder - 1,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationStock,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        break;
                    case -(int)SYSVarType.StatusOfCOContainerLOEmpty:
                        if (param.LocationStartID == param.LocationGetRomoocID && param.LocationFromID != param.LocationGetRomoocID)
                        {
                            result.Add(new HelperTOMaster_TOContainer
                            {
                                ID = -result.Count,
                                ParentID = parentNULL.ID,
                                OPSContainerID = parentNULL.OPSContainerID,
                                LocationFromID = param.LocationFromID.Value,
                                LocationToID = param.LocationStartID.Value,
                                ETD = dtStart,
                                ETA = param.DateGetRomooc.Value,
                                SortOrder = parentNULL.SortOrder - 3,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetRomooc,
                                TypeOfTOLocation = SYSVarType.TypeOfTOLocationGetRomooc,
                                IsSplit = false,
                                IsCreate = true,
                                HasCOTO = false
                            });
                            dtStart = param.DateGetRomooc.Value;
                        }
                        dtEnd = parentNULL.ETD;
                        if (dtStart.CompareTo(dtEnd) >= 0)
                        {
                            dtEnd = dtStart.AddHours(param.Hour2Point);
                            if (dtEnd.CompareTo(parentNULL.ETA) >= 0)
                            {
                                parentNULL.ETA = dtEnd.AddHours(param.Hour2Point);
                                //throw FaultHelper.BusinessFault(null, null, "container " + parentNULL.SortOrder + " etd,eta fail");
                            }
                        }
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = param.LocationStartID.Value,
                            LocationToID = parentNULL.LocationFromID,
                            ETD = dtStart,
                            ETA = dtEnd,
                            SortOrder = parentNULL.SortOrder - 2,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetEmpty,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = parentNULL.LocationFromID,
                            LocationToID = parentNULL.LocationToID,
                            ETD = dtStart,
                            ETA = dtEnd,
                            SortOrder = parentNULL.SortOrder - 2,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        break;
                    case -(int)SYSVarType.StatusOfCOContainerLOLaden:
                        if (parentNULL.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocalLaden)
                        {
                            if (param.LocationStartID == param.LocationGetRomoocID && param.LocationFromID != param.LocationGetRomoocID)
                            {
                                result.Add(new HelperTOMaster_TOContainer
                                {
                                    ID = -result.Count,
                                    ParentID = parentNULL.ID,
                                    OPSContainerID = parentNULL.OPSContainerID,
                                    LocationFromID = param.LocationFromID.Value,
                                    LocationToID = param.LocationStartID.Value,
                                    ETD = dtStart,
                                    ETA = param.DateGetRomooc.Value,
                                    SortOrder = parentNULL.SortOrder - 3,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetRomooc,
                                    TypeOfTOLocation = SYSVarType.TypeOfTOLocationGetRomooc,
                                    IsSplit = false,
                                    IsCreate = true,
                                    HasCOTO = false
                                });
                                dtStart = param.DateGetRomooc.Value;
                            }
                            dtEnd = parentNULL.ETD;
                            if (dtStart.CompareTo(dtEnd) >= 0)
                            {
                                dtEnd = dtStart.AddHours(param.Hour2Point);
                                if (dtEnd.CompareTo(parentNULL.ETA) >= 0)
                                {
                                    parentNULL.ETA = dtEnd.AddHours(param.Hour2Point);
                                    //throw FaultHelper.BusinessFault(null, null, "container " + parentNULL.SortOrder + " etd,eta fail");
                                }
                            }
                            result.Add(new HelperTOMaster_TOContainer
                            {
                                ID = -result.Count,
                                ParentID = parentNULL.ID,
                                OPSContainerID = parentNULL.OPSContainerID,
                                LocationFromID = param.LocationStartID.Value,
                                LocationToID = parentNULL.LocationFromID,
                                ETD = dtStart,
                                ETA = dtEnd,
                                SortOrder = parentNULL.SortOrder - 2,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetLaden,
                                TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
                                IsSplit = false,
                                IsCreate = true,
                                HasCOTO = false
                            });
                            result.Add(new HelperTOMaster_TOContainer
                            {
                                ID = -result.Count,
                                ParentID = parentNULL.ID,
                                OPSContainerID = parentNULL.OPSContainerID,
                                LocationFromID = parentNULL.LocationFromID,
                                LocationToID = parentNULL.LocationToID,
                                ETD = dtStart,
                                ETA = dtEnd,
                                SortOrder = parentNULL.SortOrder - 2,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipLaden,
                                TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
                                IsSplit = false,
                                IsCreate = true,
                                HasCOTO = false
                            });
                        }
                        else
                            parentNULL.IsSplit = false;
                        break;
                }
            }

            //Create end
            lstCheckStatusID = new List<int>() { -(int)SYSVarType.StatusOfCOContainerIMEmpty, -(int)SYSVarType.StatusOfCOContainerEXLaden, -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty };
            lstParentNULL = result.Where(c => c.ParentID == null && lstCheckStatusID.Contains(c.StatusOfCOContainerID) && c.IsSplit == false).ToList();
            foreach (var parentNULL in lstParentNULL)
            {
                var locationfromid = parentNULL.LocationToID;
                var dtStart = parentNULL.ETA;
                if (param.LocationReturnRomoocID > 0 && param.DateReturnRomooc != null && param.HasBreakRomooc == true)
                {
                    //Add hour if same time
                    var dateReturn = param.DateReturnRomooc.Value;
                    if (dateReturn.CompareTo(dtStart) <= 0)
                        dateReturn = dtStart.AddHours(param.Hour2Point);
                    result.Add(new HelperTOMaster_TOContainer
                    {
                        ID = -result.Count,
                        ParentID = null,
                        OPSContainerID = parentNULL.OPSContainerID,
                        LocationFromID = locationfromid,
                        LocationToID = param.LocationReturnRomoocID.Value,
                        ETD = dtStart,
                        ETA = dateReturn,
                        SortOrder = parentNULL.SortOrder + 1,
                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerReturnRomooc,
                        TypeOfTOLocation = SYSVarType.TypeOfTOLocationReturnRomooc,
                        IsSplit = false,
                        IsCreate = true,
                        HasCOTO = false
                    });
                    dtStart = dateReturn;
                    locationfromid = param.LocationReturnRomoocID.Value;
                }
                if (locationfromid != param.LocationToID)
                {
                    //Add hour if same time
                    var dtEnd = param.DateEnd.Value;
                    if (dtEnd.CompareTo(dtStart) <= 0)
                        dtEnd = dtStart.AddHours(param.Hour2Point);
                    result.Add(new HelperTOMaster_TOContainer
                    {
                        ID = -result.Count,
                        ParentID = null,
                        OPSContainerID = parentNULL.OPSContainerID,
                        LocationFromID = locationfromid,
                        LocationToID = param.LocationToID.Value,
                        ETD = dtStart,
                        ETA = dtEnd,
                        SortOrder = parentNULL.SortOrder + 2,
                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                        TypeOfTOLocation = SYSVarType.TypeOfTOLocationEmpty,
                        IsSplit = false,
                        IsCreate = true,
                        HasCOTO = false
                    });
                }
            }

            var lstOPSContainerID = result.Select(c => c.OPSContainerID).Distinct().ToList();
            foreach (var opsContainerID in lstOPSContainerID)
            {
                DateTime? dt = null;
                foreach (var item in result.Where(c => c.OPSContainerID == opsContainerID && c.IsSplit == false).OrderBy(c => c.SortOrder))
                {
                    if (dt != null && dt.Value.CompareTo(item.ETD) > 0)
                        item.ETD = dt.Value;
                    if (item.ETD.CompareTo(item.ETA) >= 0)
                        item.ETA = item.ETD.AddHours(param.Hour2Point);
                    dt = item.ETA;
                }
            }
            //Check time 
            foreach (var opsContainerID in lstOPSContainerID)
            {
                DateTime? dtCheck = null;
                var toid = -1;
                foreach (var item in result.Where(c => c.OPSContainerID == opsContainerID && c.IsSplit == false).OrderBy(c => c.SortOrder))
                {
                    if (item.LocationFromID == item.LocationToID)
                        throw FaultHelper.BusinessFault(null, null, "container " + item.SortOrder + " to,from fail");
                    if (item.LocationToID == toid)
                        throw FaultHelper.BusinessFault(null, null, "container " + item.SortOrder + " to same fail");
                    if (dtCheck != null)
                    {
                        if (item.ETD.CompareTo(dtCheck.Value) <= 0)
                            throw FaultHelper.BusinessFault(null, null, "container " + item.SortOrder + " time fail");
                    }
                    toid = item.LocationToID;
                    dtCheck = item.ETD;
                }
                if (result.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerGetRomooc).Count() > 2)
                    throw FaultHelper.BusinessFault(null, null, "container more get mooc fail");
                if (result.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerReturnRomooc).Count() > 2)
                    throw FaultHelper.BusinessFault(null, null, "container more return mooc fail");
            }

            return result;
        }

        //gen data change container sort 
        private static void OPSCO_CreateItem_Sort(List<HelperTOMaster_TOContainer> lst)
        {
            //Change sort
            var lstOPSContainerID = lst.Select(c => c.OPSContainerID).Distinct().ToList();
            foreach (var opscontainerid in lstOPSContainerID)
            {
                int sort = 1;
                foreach (var tocontainer in lst.Where(c => c.IsSplit == false && c.OPSContainerID == opscontainerid).OrderBy(c => c.SortOrder))
                {
                    tocontainer.SortOrder = sort;
                    sort++;
                }
            }
        }

        //gen data 3 location 
        private static List<HelperTOMaster_TOLocation> OPSCO_CreateLocation(List<HelperTOMaster_TOContainer> lst)
        {
            //Change sort 
            var result = new List<HelperTOMaster_TOLocation>();

            var lstOPSContainerID = lst.Where(c => c.IsSplit == false).Select(c => c.OPSContainerID).Distinct().ToList();
            var dicOPSContainer = new Dictionary<int, int>();
            var getmoocSort = -1;
            var returnmoocSort = -1;
            foreach (var opscontainerid in lstOPSContainerID)
            {
                var sort = 1;
                var lstChange = lst.Where(c => c.IsSplit == false && c.OPSContainerID == opscontainerid).OrderBy(c => c.SortOrder).ToList();
                foreach (var tocontainer in lstChange)
                {
                    tocontainer.SortLocation = sort++;
                    if (!dicOPSContainer.ContainsKey(tocontainer.OPSContainerID))
                        dicOPSContainer.Add(tocontainer.OPSContainerID, tocontainer.SortLocation);
                    else
                        dicOPSContainer[tocontainer.OPSContainerID] = tocontainer.SortLocation;
                }
                var con = lst.FirstOrDefault(c => c.OPSContainerID == opscontainerid && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerGetRomooc);
                if (con != null)
                {
                    if (getmoocSort == -1)
                        getmoocSort = con.SortLocation;
                    else if (getmoocSort != con.SortLocation)
                        throw FaultHelper.BusinessFault(null, null, "location get mooc different");
                }
                con = lst.FirstOrDefault(c => c.OPSContainerID == opscontainerid && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerReturnRomooc);
                if (con != null)
                {
                    if (returnmoocSort == -1)
                        returnmoocSort = con.SortLocation;
                    else if (returnmoocSort != con.SortLocation)
                        throw FaultHelper.BusinessFault(null, null, "location return mooc different");
                }
            }

            var nextCurrent = default(HelperTOMaster_TOLocation);
            var typeoftolocationid = -1;
            int i = 1;
            for (i = 1; i < 12; i++)
            {
                //Get first location in sort
                var first = lst.Where(c => c.SortLocation == i).OrderBy(c => c.ETD).FirstOrDefault();
                if (first != null)
                {
                    var itemFirst = new HelperTOMaster_TOLocation()
                    {
                        LocationID = first.LocationFromID,
                        DateComeEstimate = first.ETD,
                        DateLeaveEstimate = first.ETD,
                        TypeOfTOLocationID = typeoftolocationid
                    };
                    if (nextCurrent != null && itemFirst.LocationID == nextCurrent.LocationID)
                    {
                        throw FaultHelper.BusinessFault(null, null, "location fail");
                    }
                    typeoftolocationid = -(int)first.TypeOfTOLocation;
                    result.Add(itemFirst);
                    if (lst.Where(c => c.SortLocation == i).Count() > 1)
                    {
                        DateTime datecomeStart = itemFirst.DateComeEstimate.AddHours(-MaxHourSamePoint);
                        DateTime datecomeEnd = itemFirst.DateComeEstimate.AddHours(MaxHourSamePoint);
                        if (lst.Where(c => c.LocationFromID == first.LocationFromID &&
                            c.ETD < datecomeStart && c.ETD > datecomeEnd && c.SortLocation == i && c.ID != first.ID).Count() > 0)
                            throw FaultHelper.BusinessFault(null, null, "location time fail");
                        else
                        {
                            //Add first location map
                            itemFirst.ListTOContainer = new List<HelperTOMaster_TOContainer>();
                            if (nextCurrent != null)
                            {
                                foreach (var itemAdd in nextCurrent.ListTOContainer.Where(c => c.LocationToID == -1))
                                {
                                    itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                                    {
                                        OPSContainerID = itemAdd.OPSContainerID,
                                        ParentID = itemAdd.ID,
                                        LocationFromID = itemAdd.LocationFromID,
                                        LocationToID = first.LocationFromID,
                                        SortOrder = i,
                                        SortLocation = itemAdd.SortLocation,
                                        StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
                                        IsSplit = false,
                                        IsCreate = true
                                    });
                                    if (dicOPSContainer[itemAdd.OPSContainerID] >= itemAdd.SortLocation)
                                    {
                                        itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                                        {
                                            OPSContainerID = itemAdd.OPSContainerID,
                                            ParentID = itemAdd.ID,
                                            LocationFromID = first.LocationFromID,
                                            LocationToID = -1,
                                            SortOrder = i,
                                            SortLocation = itemAdd.SortLocation,
                                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                                            IsSplit = false,
                                            IsCreate = true
                                        });
                                    }
                                }
                            }
                            foreach (var itemAdd in lst.Where(c => c.LocationFromID == first.LocationFromID &&
                                c.ETD < datecomeStart && c.ETD > datecomeEnd && c.SortLocation == i))
                            {
                                itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                                {
                                    OPSContainerID = itemAdd.OPSContainerID,
                                    ParentID = itemAdd.ID,
                                    LocationFromID = itemAdd.LocationFromID,
                                    LocationToID = -1,
                                    SortOrder = i,
                                    SortLocation = itemAdd.SortLocation,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                                    IsSplit = false,
                                    IsCreate = true
                                });
                            }
                            //Defind current
                            nextCurrent = itemFirst;

                            if (lst.Where(c => c.LocationFromID != first.LocationFromID && c.ETD >= first.ETD && c.ETD <= first.ETA &&
                                c.SortLocation == i).Count() > 0)
                            {

                                //Get next location
                                foreach (var locationid in lst.Where(c => c.LocationFromID != first.LocationFromID && c.ETD >= first.ETD && c.ETD <= first.ETA &&
                                    c.SortLocation == i).OrderBy(c => c.ETD).Select(c => c.LocationFromID).Distinct())
                                {
                                    var next = lst.Where(c => c.LocationFromID == locationid && c.SortLocation == i).OrderBy(c => c.ETD).FirstOrDefault();
                                    if (next != null)
                                    {
                                        var itemNext = new HelperTOMaster_TOLocation()
                                        {
                                            LocationID = next.LocationFromID,
                                            DateComeEstimate = next.ETD,
                                            DateLeaveEstimate = next.ETD,
                                            TypeOfTOLocationID = typeoftolocationid
                                        };
                                        typeoftolocationid = -(int)next.TypeOfTOLocation;
                                        result.Add(itemNext);
                                        datecomeStart = itemNext.DateComeEstimate.AddHours(-MaxHourSamePoint);
                                        datecomeEnd = itemNext.DateComeEstimate.AddHours(MaxHourSamePoint);
                                        if (lst.Where(c => c.LocationFromID == next.LocationFromID &&
                                            c.ETD < datecomeStart && c.ETD > datecomeEnd && c.SortLocation == i && c.ID != next.ID).Count() > 0)
                                            throw FaultHelper.BusinessFault(null, null, "location time fail");
                                        else
                                        {
                                            //Add next old location map
                                            itemNext.ListTOContainer = new List<HelperTOMaster_TOContainer>();
                                            foreach (var itemAdd in nextCurrent.ListTOContainer.Where(c => c.LocationToID == -1))
                                            {
                                                itemNext.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                                                {
                                                    OPSContainerID = itemAdd.OPSContainerID,
                                                    ParentID = itemAdd.ID,
                                                    LocationFromID = itemAdd.LocationFromID,
                                                    LocationToID = next.LocationFromID,
                                                    SortOrder = i,
                                                    SortLocation = itemAdd.SortLocation,
                                                    StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
                                                    IsSplit = false,
                                                    IsCreate = true
                                                });
                                                if (dicOPSContainer[itemAdd.OPSContainerID] >= itemAdd.SortLocation)
                                                {
                                                    itemNext.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                                                    {
                                                        OPSContainerID = itemAdd.OPSContainerID,
                                                        ParentID = itemAdd.ID,
                                                        LocationFromID = next.LocationFromID,
                                                        LocationToID = -1,
                                                        SortOrder = i,
                                                        SortLocation = itemAdd.SortLocation,
                                                        StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
                                                        IsSplit = false,
                                                        IsCreate = true
                                                    });
                                                }
                                            }
                                            //Add next new location map
                                            foreach (var itemAdd in lst.Where(c => c.LocationFromID == next.LocationFromID &&
                                                c.ETD < datecomeStart && c.ETD > datecomeEnd && c.SortLocation == i))
                                            {
                                                itemNext.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                                                {
                                                    OPSContainerID = itemAdd.OPSContainerID,
                                                    ParentID = itemAdd.ID,
                                                    LocationFromID = itemAdd.LocationFromID,
                                                    LocationToID = -1,
                                                    SortOrder = i,
                                                    SortLocation = itemAdd.SortLocation,
                                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                                                    IsSplit = false,
                                                    IsCreate = true
                                                });
                                            }
                                            //Defind current
                                            nextCurrent = itemNext;
                                        }
                                    }
                                }
                            }

                            //Remove item not in this sort
                            foreach (var itemnotin in lst.Where(c => c.LocationFromID != first.LocationFromID && c.ETD > first.ETA &&
                                c.SortLocation == i).OrderBy(c => c.ETD))
                            {
                                itemnotin.SortLocation++;
                            }
                        }
                    }
                }
                else
                    break;
            }
            i--;
            if (i > 1)
            {
                //Get first location in sort
                var first = lst.Where(c => c.SortLocation == i).OrderBy(c => c.ETA).FirstOrDefault();
                if (first != null)
                {
                    var itemFirst = new HelperTOMaster_TOLocation()
                    {
                        LocationID = first.LocationToID,
                        DateComeEstimate = first.ETA,
                        DateLeaveEstimate = first.ETA,
                        TypeOfTOLocationID = typeoftolocationid
                    };
                    typeoftolocationid = -(int)first.TypeOfTOLocation;
                    result.Add(itemFirst);
                    if (lst.Where(c => c.SortLocation == i).Count() > 1)
                    {
                        DateTime datecomeStart = itemFirst.DateComeEstimate.AddHours(-MaxHourSamePoint);
                        DateTime datecomeEnd = itemFirst.DateComeEstimate.AddHours(MaxHourSamePoint);
                        if (lst.Where(c => c.LocationToID == first.LocationToID &&
                            c.ETA < datecomeStart && c.ETA > datecomeEnd && c.SortLocation == i && c.ID != first.ID).Count() > 0)
                            throw FaultHelper.BusinessFault(null, null, "location time fail");
                        else
                        {
                            if (nextCurrent != null)
                            {
                                foreach (var itemAdd in nextCurrent.ListTOContainer.Where(c => c.LocationToID == -1))
                                {
                                    itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                                    {
                                        OPSContainerID = itemAdd.OPSContainerID,
                                        ParentID = itemAdd.ID,
                                        LocationFromID = itemAdd.LocationFromID,
                                        LocationToID = first.LocationToID,
                                        SortOrder = i,
                                        SortLocation = itemAdd.SortLocation,
                                        StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
                                        IsSplit = false,
                                        IsCreate = true
                                    });

                                    if (itemAdd.LocationToID != first.LocationToID)
                                    {
                                        itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                                        {
                                            OPSContainerID = itemAdd.OPSContainerID,
                                            ParentID = itemAdd.ID,
                                            LocationFromID = first.LocationToID,
                                            LocationToID = -1,
                                            SortOrder = i,
                                            SortLocation = itemAdd.SortLocation,
                                            StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
                                            IsSplit = false,
                                            IsCreate = true
                                        });
                                    }
                                }
                                //Defind current
                                nextCurrent = itemFirst;

                                if (lst.Where(c => c.LocationToID != first.LocationToID && c.SortLocation == i).Count() > 0)
                                {
                                    //Get next location
                                    foreach (var locationid in lst.Where(c => c.LocationToID != first.LocationToID && c.SortLocation == i).OrderBy(c => c.ETA).Select(c => c.LocationToID).Distinct())
                                    {
                                        var next = lst.Where(c => c.LocationToID == locationid && c.SortLocation == i).OrderBy(c => c.ETA).FirstOrDefault();
                                        if (next != null)
                                        {
                                            var itemNext = new HelperTOMaster_TOLocation()
                                            {
                                                LocationID = next.LocationToID,
                                                DateComeEstimate = next.ETA,
                                                DateLeaveEstimate = next.ETA,
                                                TypeOfTOLocationID = typeoftolocationid
                                            };
                                            typeoftolocationid = -(int)first.TypeOfTOLocation;
                                            result.Add(itemNext);
                                            datecomeStart = itemNext.DateComeEstimate.AddHours(-MaxHourSamePoint);
                                            datecomeEnd = itemNext.DateComeEstimate.AddHours(MaxHourSamePoint);
                                            if (lst.Where(c => c.LocationToID == next.LocationToID &&
                                                c.ETA < datecomeStart && c.ETA > datecomeEnd && c.SortLocation == i && c.ID != next.ID).Count() > 0)
                                                throw FaultHelper.BusinessFault(null, null, "location time fail");
                                            else
                                            {
                                                //Add next old location map
                                                itemNext.ListTOContainer = new List<HelperTOMaster_TOContainer>();
                                                foreach (var itemAdd in nextCurrent.ListTOContainer.Where(c => c.LocationToID == -1))
                                                {
                                                    itemNext.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                                                    {
                                                        OPSContainerID = itemAdd.OPSContainerID,
                                                        ParentID = itemAdd.ID,
                                                        LocationFromID = itemAdd.LocationFromID,
                                                        LocationToID = next.LocationToID,
                                                        SortOrder = i,
                                                        SortLocation = itemAdd.SortLocation,
                                                        StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
                                                        IsSplit = false,
                                                        IsCreate = true
                                                    });
                                                    if (itemAdd.LocationToID != first.LocationToID)
                                                    {
                                                        itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                                                        {
                                                            OPSContainerID = itemAdd.OPSContainerID,
                                                            ParentID = itemAdd.ID,
                                                            LocationFromID = first.LocationToID,
                                                            LocationToID = -1,
                                                            SortOrder = i,
                                                            SortLocation = itemAdd.SortLocation,
                                                            StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
                                                            IsSplit = false,
                                                            IsCreate = true
                                                        });
                                                    }
                                                }
                                                //Defind current
                                                nextCurrent = itemNext;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            //Check time location
            DateTime? dtCheck = null;
            for (i = 0; i < result.Count; i++)
            {
                var itemLocation = result[i];
                if (dtCheck != null)
                {
                    if (itemLocation.DateComeEstimate.CompareTo(dtCheck.Value) <= 0)
                        throw FaultHelper.BusinessFault(null, null, "create location " + (i + 1) + " time fail");
                }
                dtCheck = itemLocation.DateComeEstimate;
                if (i > 0)
                {
                    if (itemLocation.LocationID == result[i - 1].LocationID)
                        throw FaultHelper.BusinessFault(null, null, "create location " + (i + 1) + " same fail");
                }
            }

            return result;
        }

        private static List<HelperTOMaster_TOContainer> OPSCO_CreateTOContainer(DataEntities model, AccountItem account, int masterid, HelperTOMaster_COParam param)
        {
            double percentSubRoute = 0.2;

            var result = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false).Select(c => new HelperTOMaster_TOContainer
            {
                ID = c.ID,
                ParentID = c.ParentID,
                OPSContainerID = c.OPSContainerID,
                LocationFromID = c.LocationFromID,
                LocationToID = c.LocationToID,
                ETD = c.ETD.Value,
                ETA = c.ETA.Value,
                ETDStart = c.ETDStart,
                ETAStart = c.ETAStart,
                SortOrder = c.SortOrder,
                StatusOfCOContainerID = c.StatusOfCOContainerID,
                ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID > 0 ? c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID : -1,
                CATTransportModeID = c.OPS_Container.ORD_Container.ORD_Order.TransportModeID,
                IsSplit = c.IsSplit,
                IsCreate = false,
                HasCOTO = false
            }).ToList();

            //Create start
            var lstCheckStatusID = new List<int>() { -(int)SYSVarType.StatusOfCOContainerIMLaden, -(int)SYSVarType.StatusOfCOContainerEXEmpty, -(int)SYSVarType.StatusOfCOContainerLOGetEmpty, -(int)SYSVarType.StatusOfCOContainerLOEmpty, -(int)SYSVarType.StatusOfCOContainerLOLaden };
            var lstParentNULL = result.Where(c => c.ParentID == null && lstCheckStatusID.Contains(c.StatusOfCOContainerID) && c.IsSplit == false).ToList();
            foreach (var parentNULL in lstParentNULL)
            {
                parentNULL.IsSplit = true;
                var totalHour = (parentNULL.ETA - parentNULL.ETD).TotalHours;
                var subHour = totalHour * percentSubRoute;
                var mainHour = totalHour - subHour;
                var start = parentNULL.ETD;
                switch (parentNULL.StatusOfCOContainerID)
                {
                    case -(int)SYSVarType.StatusOfCOContainerIMLaden:
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = param.LocationFromID.Value,
                            LocationToID = parentNULL.LocationFromID,
                            ETD = start,
                            ETA = start.AddHours(subHour),
                            SortOrder = 1,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetLaden,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        start = start.AddHours(subHour);
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = parentNULL.LocationFromID,
                            LocationToID = parentNULL.LocationToID,
                            ETD = start,
                            ETA = start.AddHours(mainHour),
                            SortOrder = 2,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipLaden,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationStock,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        break;
                    case -(int)SYSVarType.StatusOfCOContainerEXEmpty:
                    case -(int)SYSVarType.StatusOfCOContainerLOGetEmpty:
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = param.LocationFromID.Value,
                            LocationToID = parentNULL.LocationFromID,
                            ETD = start,
                            ETA = start.AddHours(subHour),
                            SortOrder = 1,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetEmpty,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationDepot,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        start = start.AddHours(subHour);
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = parentNULL.LocationFromID,
                            LocationToID = parentNULL.LocationToID,
                            ETD = start,
                            ETA = start.AddHours(mainHour),
                            SortOrder = 2,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationStock,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        break;
                    case -(int)SYSVarType.StatusOfCOContainerLOEmpty:
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = param.LocationFromID.Value,
                            LocationToID = parentNULL.LocationFromID,
                            ETD = start,
                            ETA = start.AddHours(subHour),
                            SortOrder = 1,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetEmpty,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        start = start.AddHours(subHour);
                        result.Add(new HelperTOMaster_TOContainer
                        {
                            ID = -result.Count,
                            ParentID = parentNULL.ID,
                            OPSContainerID = parentNULL.OPSContainerID,
                            LocationFromID = parentNULL.LocationFromID,
                            LocationToID = parentNULL.LocationToID,
                            ETD = start,
                            ETA = start.AddHours(mainHour),
                            SortOrder = 2,
                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                            TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
                            IsSplit = false,
                            IsCreate = true,
                            HasCOTO = false
                        });
                        break;
                    case -(int)SYSVarType.StatusOfCOContainerLOLaden:
                        if (parentNULL.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocalLaden)
                        {
                            result.Add(new HelperTOMaster_TOContainer
                            {
                                ID = -result.Count,
                                ParentID = parentNULL.ID,
                                OPSContainerID = parentNULL.OPSContainerID,
                                LocationFromID = param.LocationFromID.Value,
                                LocationToID = parentNULL.LocationFromID,
                                ETD = start,
                                ETA = start.AddHours(subHour),
                                SortOrder = 1,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetLaden,
                                TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
                                IsSplit = false,
                                IsCreate = true,
                                HasCOTO = false
                            });
                            start = start.AddHours(subHour);
                            result.Add(new HelperTOMaster_TOContainer
                            {
                                ID = -result.Count,
                                ParentID = parentNULL.ID,
                                OPSContainerID = parentNULL.OPSContainerID,
                                LocationFromID = parentNULL.LocationFromID,
                                LocationToID = parentNULL.LocationToID,
                                ETD = start,
                                ETA = start.AddHours(mainHour),
                                SortOrder = 2,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipLaden,
                                TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
                                IsSplit = false,
                                IsCreate = true,
                                HasCOTO = false
                            });
                        }
                        else
                            parentNULL.IsSplit = false;
                        break;
                }
            }

            //Create end
            lstCheckStatusID = new List<int>() { -(int)SYSVarType.StatusOfCOContainerIMEmpty, -(int)SYSVarType.StatusOfCOContainerEXLaden, -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty };
            lstParentNULL = result.Where(c => c.ParentID == null && lstCheckStatusID.Contains(c.StatusOfCOContainerID) && c.IsSplit == false).ToList();
            foreach (var parentNULL in lstParentNULL)
            {
                var totalHour = (parentNULL.ETA - parentNULL.ETD).TotalHours;
                var subHour = totalHour * percentSubRoute;
                var mainHour = totalHour - subHour;
                var start = parentNULL.ETA;

                if (parentNULL.LocationToID != param.LocationToID)
                {
                    result.Add(new HelperTOMaster_TOContainer
                    {
                        ID = -result.Count,
                        ParentID = null,
                        OPSContainerID = parentNULL.OPSContainerID,
                        LocationFromID = parentNULL.LocationToID,
                        LocationToID = param.LocationToID.Value,
                        ETD = start,
                        ETA = start.AddHours(subHour),
                        SortOrder = parentNULL.SortOrder + 1,
                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerReturnStation,
                        TypeOfTOLocation = SYSVarType.TypeOfTOLocationEmpty,
                        IsSplit = false,
                        IsCreate = true,
                        HasCOTO = false
                    });
                }
            }

            ////Create end
            //lstCheckStatusID = new List<int>() { -(int)SYSVarType.StatusOfCOContainerIMEmpty, -(int)SYSVarType.StatusOfCOContainerEXLaden, -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty };
            //lstParentNULL = result.Where(c => c.ParentID == null && lstCheckStatusID.Contains(c.StatusOfCOContainerID) && c.IsSplit == false).ToList();
            //foreach (var parentNULL in lstParentNULL)
            //{
            //    var totalHour = (parentNULL.ETA - parentNULL.ETD).TotalHours;
            //    var subHour = totalHour * percentSubRoute;
            //    var mainHour = totalHour - subHour;
            //    var start = parentNULL.ETD;


            //    var locationfromid = parentNULL.LocationToID;
            //    var dtStart = parentNULL.ETA;
            //    if (param.LocationReturnRomoocID > 0 && param.DateReturnRomooc != null && param.HasBreakRomooc == true)
            //    {
            //        //Add hour if same time
            //        var dateReturn = param.DateReturnRomooc.Value;
            //        if (dateReturn.CompareTo(dtStart) <= 0)
            //            dateReturn = dtStart.AddHours(param.Hour2Point);
            //        result.Add(new HelperTOMaster_TOContainer
            //        {
            //            ID = -result.Count,
            //            ParentID = null,
            //            OPSContainerID = parentNULL.OPSContainerID,
            //            LocationFromID = locationfromid,
            //            LocationToID = param.LocationReturnRomoocID.Value,
            //            ETD = dtStart,
            //            ETA = dateReturn,
            //            SortOrder = parentNULL.SortOrder + 1,
            //            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerReturnRomooc,
            //            TypeOfTOLocation = SYSVarType.TypeOfTOLocationReturnRomooc,
            //            IsSplit = false,
            //            IsCreate = true,
            //            HasCOTO = false
            //        });
            //        dtStart = dateReturn;
            //        locationfromid = param.LocationReturnRomoocID.Value;
            //    }
            //    if (locationfromid != param.LocationToID)
            //    {
            //        //Add hour if same time
            //        var dtEnd = param.DateEnd.Value;
            //        if (dtEnd.CompareTo(dtStart) <= 0)
            //            dtEnd = dtStart.AddHours(param.Hour2Point);
            //        result.Add(new HelperTOMaster_TOContainer
            //        {
            //            ID = -result.Count,
            //            ParentID = null,
            //            OPSContainerID = parentNULL.OPSContainerID,
            //            LocationFromID = locationfromid,
            //            LocationToID = param.LocationToID.Value,
            //            ETD = dtStart,
            //            ETA = dtEnd,
            //            SortOrder = parentNULL.SortOrder + 2,
            //            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
            //            TypeOfTOLocation = SYSVarType.TypeOfTOLocationEmpty,
            //            IsSplit = false,
            //            IsCreate = true,
            //            HasCOTO = false
            //        });
            //    }
            //}

            //var lstOPSContainerID = result.Select(c => c.OPSContainerID).Distinct().ToList();
            //foreach (var opsContainerID in lstOPSContainerID)
            //{
            //    DateTime? dt = null;
            //    foreach (var item in result.Where(c => c.OPSContainerID == opsContainerID && c.IsSplit == false).OrderBy(c => c.SortOrder))
            //    {
            //        if (dt != null && dt.Value.CompareTo(item.ETD) > 0)
            //            item.ETD = dt.Value;
            //        if (item.ETD.CompareTo(item.ETA) >= 0)
            //            item.ETA = item.ETD.AddHours(param.Hour2Point);
            //        dt = item.ETA;
            //    }
            //}
            ////Check time 
            //foreach (var opsContainerID in lstOPSContainerID)
            //{
            //    DateTime? dtCheck = null;
            //    var toid = -1;
            //    foreach (var item in result.Where(c => c.OPSContainerID == opsContainerID && c.IsSplit == false).OrderBy(c => c.SortOrder))
            //    {
            //        if (item.LocationFromID == item.LocationToID)
            //            throw FaultHelper.BusinessFault(null, null, "container " + item.SortOrder + " to,from fail");
            //        if (item.LocationToID == toid)
            //            throw FaultHelper.BusinessFault(null, null, "container " + item.SortOrder + " to same fail");
            //        if (dtCheck != null)
            //        {
            //            if (item.ETD.CompareTo(dtCheck.Value) <= 0)
            //                throw FaultHelper.BusinessFault(null, null, "container " + item.SortOrder + " time fail");
            //        }
            //        toid = item.LocationToID;
            //        dtCheck = item.ETD;
            //    }
            //    if (result.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerGetRomooc).Count() > 2)
            //        throw FaultHelper.BusinessFault(null, null, "container more get mooc fail");
            //    if (result.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerReturnRomooc).Count() > 2)
            //        throw FaultHelper.BusinessFault(null, null, "container more return mooc fail");
            //}

            return result;
        }

        private void OPSCO_CheckLocationRequired(DataEntities model, DTOSYSSetting objSetting, DTOOPSCOTOMaster obj)
        {
            try
            {
                double cWeight = 0;
                double eWeight = 0;
                if (obj.VehicleID > 0)
                {
                    var objV = model.CAT_Vehicle.FirstOrDefault(c => c.ID == obj.VehicleID);
                    if (objV == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy đầu kéo");
                    eWeight = Math.Min(objV.MaxWeight ?? 0, eWeight);
                }
                if (obj.RomoocID > 0)
                {
                    var objR = model.CAT_Romooc.FirstOrDefault(c => c.ID == obj.RomoocID);
                    if (objR == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy romooc");
                    eWeight = Math.Min(objR.MaxWeight ?? 0, eWeight);
                }

                for (var i = 0; i < obj.ListCOLocation.Count; i++)
                {
                    var item = obj.ListCOLocation[i];
                    var catObj = model.CAT_Location.FirstOrDefault(c => c.ID == item.LocationID);
                    if (catObj != null && item.DateComeEstimate.HasValue)
                    {
                        var dataRequired = OPS_GetLocationRequired(model, item.LocationID.Value);
                        if (objSetting.HasConstraintTimeOpenLocation)
                        {
                            if (dataRequired.Count(c => c.IsOpen == true && c.IsSize == false) > 0)
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));

                                var objCheck = dataRequired.FirstOrDefault(c => c.IsOpen == true && c.IsSize == false && ((c.DayOfWeek == dayOfWeek && esDate >= c.TimeFrom && esDate <= c.TimeTo) || (c.DayOfWeek == -1 && item.DateComeEstimate >= c.TimeFrom && item.DateComeEstimate <= c.TimeTo)));
                                if (objCheck == null)
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không mở cửa vào tgian: " + String.Format("{0:d/M HH:mm}", item.DateComeEstimate.Value));
                            }
                            foreach (var o in dataRequired.Where(c => c.IsOpen == false && c.IsSize == false))
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));

                                if ((o.DayOfWeek == dayOfWeek && esDate > o.TimeFrom && esDate < o.TimeTo) || (o.DayOfWeek == -1 && item.DateComeEstimate > o.TimeFrom && item.DateComeEstimate < o.TimeTo))
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không mở cửa vào tgian: từ " + String.Format("{0:d/M HH:mm}", o.TimeFrom) + " đến " + String.Format("{0:d/M HH:mm}", o.TimeTo));
                            }
                        }
                        //Check trọng tải trước khi vào kho/cảng/bãi
                        if (objSetting.HasConstraintWeight)
                        {
                            foreach (var o in dataRequired.Where(c => c.IsOpen == true && c.IsSize == true))
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));
                                if (((o.DayOfWeek == dayOfWeek && esDate > o.TimeFrom && esDate < o.TimeTo) || (o.DayOfWeek == -1 && item.DateComeEstimate > o.TimeFrom && item.DateComeEstimate < o.TimeTo)) && o.Weight < cWeight)
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không đáp ứng trọng tải. Tgian:" + String.Format("{0:d/M HH:mm}", item.DateComeEstimate.Value));
                            }
                        }
                        foreach (var o in obj.ListCOContainer.Where(c => c.LocationFromID == item.LocationID).ToList())
                        {
                            cWeight = cWeight + o.Ton;
                        }
                        foreach (var o in obj.ListCOContainer.Where(c => c.LocationFromID == item.LocationID).ToList())
                        {
                            cWeight = cWeight - o.Ton;
                        }
                        //Check trọng tải sau khi Load/UnLoad hàng
                        if (objSetting.HasConstraintTransport)
                        {
                            if (eWeight > 0 && eWeight < cWeight)
                            {
                                throw FaultHelper.BusinessFault(null, null, "Phương tiện không đáp ứng trọng tải tại điểm " + catObj.Location);
                            }
                        }
                        if (objSetting.HasConstraintWeight)
                        {
                            foreach (var o in dataRequired.Where(c => c.IsOpen == true && c.IsSize == true))
                            {
                                var dayOfWeek = (int)item.DateComeEstimate.Value.DayOfWeek;
                                var esDate = new DateTime(2000, 1, 1).Add(item.DateComeEstimate.Value.Subtract(item.DateComeEstimate.Value.Date));
                                if (((o.DayOfWeek == dayOfWeek && esDate > o.TimeFrom && esDate < o.TimeTo) || (o.DayOfWeek == -1 && item.DateComeEstimate > o.TimeFrom && item.DateComeEstimate < o.TimeTo)) && o.Weight < cWeight)
                                    throw FaultHelper.BusinessFault(null, null, "Điểm " + catObj.Location + " không đáp ứng trọng tải. Tgian:" + String.Format("{0:d/M HH:mm}", item.DateComeEstimate.Value));
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

        private List<DTOOPSLocationRequired> OPS_GetLocationRequired(DataEntities model, int cID)
        {
            List<DTOOPSLocationRequired> result = new List<DTOOPSLocationRequired>();

            foreach (var item in model.CAT_LocationRequire.Where(c => c.LocationID == cID).Select(c => new
            {
                c.ID,
                c.ConstraintRequireTypeID,
                c.Weight,
                c.TimeFrom,
                c.TimeTo
            }).ToList())
            {
                switch (item.ConstraintRequireTypeID)
                {
                    case -(int)SYSVarType.ConstraintRequireTypeSizeDay:
                        if (item.TimeFrom.HasValue && item.TimeTo.HasValue && item.Weight.HasValue)
                        {
                            var obj = new DTOOPSLocationRequired(true, true, item.Weight.Value, -1, item.TimeFrom.Value, item.TimeTo.Value);
                            result.Add(obj);
                        }
                        break;
                    case -(int)SYSVarType.ConstraintRequireTypeSizeWeek:
                        if (item.TimeFrom.HasValue && item.TimeTo.HasValue && item.Weight.HasValue)
                        {
                            var fDate = (int)item.TimeFrom.Value.Date.DayOfWeek;
                            var tDate = (int)item.TimeTo.Value.Date.DayOfWeek;
                            if (fDate <= tDate)
                            {
                                for (var i = fDate; i <= tDate; i++)
                                {
                                    var obj = new DTOOPSLocationRequired(true, true, item.Weight.Value, i, new DateTime(2000, 1, 1).Add(item.TimeFrom.Value.Subtract(item.TimeFrom.Value.Date)), new DateTime(2000, 1, 1).Add(item.TimeTo.Value.Subtract(item.TimeTo.Value.Date)));
                                    result.Add(obj);
                                }
                            }
                        }
                        break;
                    case -(int)SYSVarType.ConstraintRequireTypeOpenWeek:
                        if (item.TimeFrom.HasValue && item.TimeTo.HasValue)
                        {
                            var fDate = (int)item.TimeFrom.Value.Date.DayOfWeek;
                            var tDate = (int)item.TimeTo.Value.Date.DayOfWeek;
                            if (fDate <= tDate)
                            {
                                for (var i = fDate; i <= tDate; i++)
                                {
                                    var obj = new DTOOPSLocationRequired(false, true, 0, i, new DateTime(2000, 1, 1).Add(item.TimeFrom.Value.Subtract(item.TimeFrom.Value.Date)), new DateTime(2000, 1, 1).Add(item.TimeTo.Value.Subtract(item.TimeTo.Value.Date)));
                                    result.Add(obj);
                                }
                            }
                        }
                        break;
                    case -(int)SYSVarType.ConstraintRequireTypeOpenDay:
                        if (item.TimeFrom.HasValue && item.TimeTo.HasValue)
                        {
                            var obj = new DTOOPSLocationRequired(false, true, 0, -1, item.TimeFrom.Value, item.TimeTo.Value);
                            result.Add(obj);
                        }
                        break;
                    case -(int)SYSVarType.ConstraintRequireTypeCloseDay:
                        if (item.TimeFrom.HasValue && item.TimeTo.HasValue)
                        {
                            var obj = new DTOOPSLocationRequired(false, false, 0, -1, item.TimeFrom.Value, item.TimeTo.Value);
                            result.Add(obj);
                        }
                        break;
                }
            }

            return result;
        }



        //save data 
        private static int OPSCO_CreateItem_Create(DataEntities model, AccountItem account, DTOOPSCOTOMaster master)
        {
            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == master.ID);
            if (objMaster == null)
            {
                objMaster = new OPS_COTOMaster();
                objMaster.CreatedBy = account.UserName;
                objMaster.CreatedDate = DateTime.Now;
                objMaster.SYSCustomerID = account.SYSCustomerID;

                objMaster.Code = OPSCO_GetLastCode(model);
                objMaster.IsHot = false;
                objMaster.RateTime = 0;
                objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproved;
                objMaster.TransportModeID = master.TransportModeID;

                model.OPS_COTOMaster.Add(objMaster);
            }
            else
            {
                objMaster.ModifiedBy = account.UserName;
                objMaster.ModifiedDate = DateTime.Now;
            }

            objMaster.SortOrder = master.SortOrder;
            objMaster.VehicleID = master.VehicleID;
            if (objMaster.VehicleID == null || objMaster.VehicleID < 1)
                objMaster.VehicleID = DefaultTractor;
            objMaster.VendorOfVehicleID = master.VendorOfVehicleID;
            objMaster.RomoocID = master.RomoocID;
            if (objMaster.RomoocID == null || objMaster.RomoocID < 1)
                objMaster.RomoocID = DefaultRomooc;
            objMaster.VendorOfRomoocID = master.VendorOfRomoocID;
            objMaster.DriverID1 = master.DriverID1;
            objMaster.DriverID2 = master.DriverID2;
            objMaster.DriverName1 = master.DriverName1;
            if ((objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID) && objMaster.DriverID1 == null)
                objMaster.DriverName1 = "";
            objMaster.DriverName2 = master.DriverName2;
            if ((objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID) && objMaster.DriverID2 == null)
                objMaster.DriverName2 = "";
            objMaster.DriverCard1 = master.DriverCard1;
            objMaster.DriverCard2 = master.DriverCard2;
            objMaster.DriverTel1 = master.DriverTel1;
            objMaster.DriverTel2 = master.DriverTel2;
            objMaster.ApprovedBy = master.ApprovedBy;
            objMaster.ApprovedDate = master.ApprovedDate;
            objMaster.GroupOfVehicleID = master.GroupOfVehicleID;
            objMaster.RateTime = master.RateTime;
            objMaster.ETD = master.ETD;
            objMaster.ETA = master.ETA;
            objMaster.ATD = master.ETD;
            objMaster.ATA = master.ETA;
            objMaster.TypeOfDriverID1 = -(int)SYSVarType.TypeOfDriverMain;

            //OPSCO_CheckingTime(model, account, objMaster.ID, objMaster.VehicleID, objMaster.RomoocID, objMaster.ETD, objMaster.ETA, true); 

            objMaster.DateConfig = master.ETD;
            objMaster.Note = master.Note;
            objMaster.IsBidding = master.IsBidding;
            objMaster.BiddingID = master.BiddingID;
            objMaster.KM = master.KM;
            objMaster.TransportModeID = master.TransportModeID;
            objMaster.TypeOfOrderID = master.TypeOfOrderID;
            objMaster.ContractID = master.ContractID;
            objMaster.IsCreateContainer = master.IsCreateContainer;

            model.SaveChanges();
            objMaster.Code = COCodePrefix + objMaster.ID.ToString(COCodeNum);

            //try
            //{
            //    OPSCO_CreateMaster_CON(model, account, objMaster.ID, null, null, lstOPSCON);
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //} 

            foreach (var tocon in master.ListCOContainer)
            {
                var objCON = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == tocon.ID);
                if (objCON != null)
                {
                    objCON.ETD = tocon.ETD;
                    objCON.ETA = tocon.ETA;
                    objCON.DateFromCome = tocon.ETD;
                    objCON.DateToCome = tocon.ETA;
                    objCON.IsOwnerPlanning = true;
                    objCON.COTOMasterID = objMaster.ID;
                    objCON.ModifiedBy = account.UserName;
                    objCON.ModifiedDate = DateTime.Now;
                }
            }

            foreach (var coto in master.ListCO)
            {
                var objTO = new OPS_COTO();
                objTO.CreatedBy = account.UserName;
                objTO.CreatedDate = DateTime.Now;
                objTO.COTOMasterID = objMaster.ID;
                objTO.IsOPS = true;
                objTO.SortOrder = coto.SortOrder;
                objTO.LocationFromID = coto.LocationFromID;
                objTO.LocationToID = coto.LocationToID;
                objTO.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;

                model.OPS_COTO.Add(objTO);
            }

            int sort = 1;
            foreach (var toloc in master.ListCOLocation)
            {
                var objLocation = new OPS_COTOLocation();
                objLocation.LocationID = toloc.LocationID;
                objLocation.SortOrder = sort++;
                objLocation.COTOMasterID = objMaster.ID;
                objLocation.CreatedBy = account.UserName;
                objLocation.CreatedDate = DateTime.Now;
                objLocation.DateComeEstimate = toloc.DateComeEstimate;
                objLocation.DateLeaveEstimate = toloc.DateLeaveEstimate;
                objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                //objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                objLocation.TypeOfTOLocationID = toloc.TypeOfTOLocationID;

                model.OPS_COTOLocation.Add(objLocation);
            }
            model.SaveChanges();

            var lstOPSContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objMaster.ID).Select(c => c.OPSContainerID).Distinct().ToList();
            OPSCO_TOContainerResetSort(model, account, lstOPSContainerID);

            return objMaster.ID;
        }

        private static string CO_CheckAssetTimeSheet(DataEntities model, AccountItem account, int? vehicleid, int? romoocid, int? cotomasterid, DateTime etd, DateTime eta)
        {
            var objAssetVehicle = model.FLM_Asset.Where(c => c.VehicleID == vehicleid).Select(c => new { c.ID }).FirstOrDefault();
            var objAssetRomooc = model.FLM_Asset.Where(c => c.RomoocID == romoocid).Select(c => new { c.ID }).FirstOrDefault();
            var result = string.Empty;

            if (vehicleid > 0 && objAssetVehicle != null)
            {
                if (cotomasterid > 0)
                {
                    if (model.FLM_AssetTimeSheet.Where(c => c.ReferID != cotomasterid && c.AssetID == objAssetVehicle.ID && ((c.DateFromActual <= eta && c.DateToActual >= eta) || (c.DateToActual >= etd && c.DateToActual <= eta))).Count() > 0)
                        result = "Đầu kéo bận";
                }
                else if (model.FLM_AssetTimeSheet.Where(c => c.AssetID == objAssetVehicle.ID && ((c.DateFromActual <= eta && c.DateToActual >= eta) || (c.DateToActual >= etd && c.DateToActual <= eta))).Count() > 0)
                    result = "Đầu kéo bận";
                else
                {
                    var next = model.FLM_AssetTimeSheet.Where(c => c.AssetID == objAssetVehicle.ID && c.ReferID != cotomasterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.DateFromActual > eta).Select(c => new { c.ReferID }).FirstOrDefault();
                    if (next != null && next.ReferID > 0)
                    {
                        if (model.OPS_COTOMaster.Where(c => c.ID == next.ReferID && c.StatusOfCOTOMasterID > -(int)SYSVarType.StatusOfCOTOMasterTendered).Count() > 0)
                            result = "Không lập kế hoạch trước chuyến đã đang chạy";
                    }
                }
                //if (result == string.Empty && model.FLM_AssetTimeSheet.Where(c => c.TypeOfAssetTimeSheetID >= -(int)SYSVarType.TypeOfAssetTimeSheetRunning && c.AssetID == objAssetVehicle.ID && c.DateFromActual >= etd).Count() > 0)
                //    result = "Xe đã chạy chuyến sau thời gian này";
            }
            if (result == string.Empty && romoocid > 0 && objAssetRomooc != null)
            {
                if (cotomasterid > 0)
                {
                    if (model.FLM_AssetTimeSheet.Where(c => c.ReferID != cotomasterid && c.AssetID == objAssetRomooc.ID && (c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterInStock && c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterEmpty && c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterLaden) && ((c.DateFromActual <= eta && c.DateToActual >= eta) || (c.DateToActual >= etd && c.DateToActual <= eta))).Count() > 0)
                        result = "Romooc bận";
                }
                else if (model.FLM_AssetTimeSheet.Where(c => c.AssetID == objAssetRomooc.ID && (c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterEmpty && c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterLaden) && ((c.DateFromActual <= eta && c.DateToActual >= eta) || (c.DateToActual >= etd && c.DateToActual <= eta))).Count() > 0)
                    result = "Romooc bận";
                //if (result == string.Empty && model.FLM_AssetTimeSheet.Where(c => c.AssetID == objAssetRomooc.ID && c.DateFromActual >= etd).Count() > 0)
                //    result = "Romooc đã lập chuyến sau thời gian này";
            }

            return result;
        }

        private static string CO_CheckRomoocAvailable(DataEntities model, AccountItem account, int? romoocid, List<int> lstCOTOContainerID)
        {
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };

            var objAssetRomooc = model.FLM_Asset.Where(c => c.RomoocID == romoocid).Select(c => new { c.ID }).FirstOrDefault();
            var result = string.Empty;

            var lstOPSContainerID = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID) &&
                (c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport ||
                c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)).Select(c => c.OPSContainerID).Distinct().ToList();
            int totalCOTOContainerID = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID) &&
                (c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport ||
                c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)).Count();
            if (lstCOTOContainerID.Count == 0)
            {
                model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => c.OPSContainerID).Distinct().ToList();
                totalCOTOContainerID = lstCOTOContainerID.Count;
            }
            //if (romoocid > 0 && objAssetRomooc != null)
            //{
            //    if (model.OPS_Container.Where(c => !lstOPSContainerID.Contains(c.ID) && c.RomoocID == romoocid && c.OPSContainerTypeID == -(int)SYSVarType.OPSContainerTypeRunning).Count() > 0)
            //        result = "Đang lập kế hoạch chặng";
            //}
            if (string.IsNullOrEmpty(result))
            {
                bool isorder = true;
                foreach (var opscontainerid in lstOPSContainerID)
                {
                    int total = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && lstCOTOContainerID.Contains(c.ID)).Count();
                    var lstTOContainerNULL = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.COTOMasterID == null && !statusload.Contains(c.StatusOfCOContainerID)).Select(c => new { c.ID, c.SortOrder }).OrderBy(c => c.SortOrder).ToList();
                    if (isorder)
                        isorder = total == lstTOContainerNULL.Count;
                    int idcheck = 0;
                    foreach (var containernull in lstTOContainerNULL)
                    {
                        if (!lstCOTOContainerID.Contains(containernull.ID))
                            result = "Phải lập kế hoạch chặng trước";
                        else
                            idcheck++;
                        if (!string.IsNullOrEmpty(result) || idcheck >= totalCOTOContainerID)
                            break;
                    }
                    if (!string.IsNullOrEmpty(result) || idcheck >= totalCOTOContainerID)
                        break;
                }
                if (string.IsNullOrEmpty(result) && isorder == false && (romoocid == null || romoocid == DefaultRomooc))
                    result = "Không phân chặng cho romooc chờ nhập";
            }
            //else if (romoocid == null && romoocid == DefaultRomooc)
            //{

            //}

            return result;
        }

        private static DTOOPSCOTOMaster CO_CheckConstraint(DataEntities model, AccountItem account, DTOOPSCOTOMaster item, List<int> lstCOTOContainerID)
        {
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };

            var objSetting = OPSCO_GetSetting(model, account);
            //objSetting.HasConstraintWeight
            var lstCOTOContainer = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new
            {
                c.ID,
                c.StatusOfCOContainerID,
                c.OPSContainerID,
                c.OPS_Container.ORD_Container.CAT_Packing.TypeOfPackageID,
                c.OPS_Container.ORD_Container.Ton,
                c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID
            }).ToList();

            var lstPackageLocalID = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Select(c => c.TypeOfPackageID).Distinct().ToList();
            bool haspackagelocal = true;
            bool haspackageswap = true;
            foreach (var packagelocalid in lstPackageLocalID)
            {
                if (lstCOTOContainer.Where(c => c.TypeOfPackageID == packagelocalid && (c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport || c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport)).Count() == 0)
                {
                    haspackagelocal = false;
                }
            }

            var lstPackageImportID = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Select(c => c.TypeOfPackageID).Distinct().ToList();
            var lstPackageExportID = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Select(c => c.TypeOfPackageID).Distinct().ToList();
            if (lstPackageImportID.Count > 0 && lstPackageExportID.Count > 0)
            {
                foreach (var packageexportid in lstPackageExportID)
                {
                    if (!lstPackageImportID.Contains(packageexportid))
                    {
                        haspackageswap = false;
                    }
                }
            }

            if (!haspackagelocal)
                item.OfferTimeError = "Loại container chuyển kho không đúng với chuyến đang chạy";
            else if (!haspackageswap)
                item.OfferTimeError = "Loại container để xuất khẩu không có";
            else
            {
                var lstPackageID = lstCOTOContainer.Select(c => c.TypeOfPackageID).Distinct().ToList();
                if (lstPackageID.Count == 1)
                {
                    int packageid = lstPackageID[0];
                    double calTon = 0;

                    int quantity = 0;
                    if (item.RomoocID > 0 && item.RomoocID != DefaultRomooc)
                    {
                        var catRomooc = model.CAT_Romooc.Where(c => c.ID == item.RomoocID).Select(c => new { c.MaxWeight, c.GroupOfRomoocID }).FirstOrDefault();
                        if (catRomooc != null)
                        {
                            calTon = catRomooc.MaxWeight.Value;
                            if (catRomooc.GroupOfRomoocID > 0)
                            {
                                var lstTypeOfPackage = model.CAT_GroupOfRomoocPacking.Where(c => c.GroupOfRomoocID == catRomooc.GroupOfRomoocID).Select(c => new { c.TypeOfPackageID, c.Quantity }).ToList();

                                quantity = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count();
                                if (quantity == 0)
                                    quantity = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count();
                                quantity += lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocalLaden).Count();
                                quantity += lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocalEmpty).Count();

                                if (lstTypeOfPackage.Where(c => c.TypeOfPackageID == packageid && c.Quantity >= quantity).Count() == 0)
                                    item.OfferTimeError = "Loại mooc không đủ khả năng chở " + quantity + " loại container này";
                            }
                        }
                    }

                    if (item.VehicleID > 2)
                    {
                        var catVehicle = model.CAT_Vehicle.Where(c => c.ID == item.VehicleID).Select(c => new { c.MaxWeight }).FirstOrDefault();
                        if (catVehicle != null && catVehicle.MaxWeight < calTon)
                            calTon = catVehicle.MaxWeight.Value;
                    }

                    double totalLocal = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Count();
                    if (totalLocal > 0)
                    {
                        double tonLocal = 0;
                        tonLocal = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Sum(c => c.Ton);
                        tonLocal = tonLocal / totalLocal;
                        tonLocal = tonLocal * quantity;

                        if (calTon > 0 && tonLocal > calTon)
                        {
                            if (objSetting.HasConstraintWeight == true)
                                item.OfferTimeError = "Quá trọng tải của đầu và mooc";
                            else
                                item.OfferTimeWarning = "Quá trọng tải của đầu và mooc";
                        }
                    }

                    List<int> statusLaden = new List<int>()
                    {
                        -(int)SYSVarType.StatusOfCOContainerEXLaden,
                        -(int)SYSVarType.StatusOfCOContainerIMLaden,
                        -(int)SYSVarType.StatusOfCOContainerLOLaden
                    };
                    double tonLaden = 0;
                    if (lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() > 0)
                        tonLaden = lstCOTOContainer.Where(c => statusLaden.Contains(c.StatusOfCOContainerID) && c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Sum(c => c.Ton);
                    if (lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() > 0)
                        tonLaden = lstCOTOContainer.Where(c => statusLaden.Contains(c.StatusOfCOContainerID) && c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Sum(c => c.Ton);
                    if (lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocalLaden).Count() > 0)
                        tonLaden = lstCOTOContainer.Where(c => statusLaden.Contains(c.StatusOfCOContainerID) && c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocalLaden).Sum(c => c.Ton);

                    if (calTon > 0 && tonLaden > calTon)
                    {
                        if (objSetting.HasConstraintWeight == true)
                            item.OfferTimeError = "Quá trọng tải của đầu và mooc";
                        else
                            item.OfferTimeWarning = "Quá trọng tải của đầu và mooc";
                    }
                }
                else
                    item.OfferTimeError = "Chỉ được sử dụng 1 loại container";
            }

            return item;
        }

        private static void OPSCO_ResetOPSContainer(DataEntities model, AccountItem account, List<int> lstCOTOContainerID)
        {
            //check mooc in opscontainer
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };
            var lstTOContainer = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID) && c.COTOMasterID > 0).Select(c => new { c.OPSContainerID, c.OPS_COTOMaster.RomoocID }).Distinct().ToList();
            if (lstTOContainer.Count > 0)
            {
                var lstOPSContainerPlanningID = new List<int>();
                var lstOPSContainerApprovedID = new List<int>();
                foreach (var tocontainer in lstTOContainer)
                {
                    var container = model.OPS_Container.FirstOrDefault(c => c.ID == tocontainer.OPSContainerID);
                    if (container != null)
                    {
                        if (tocontainer.RomoocID > 1)
                        {
                            container.RomoocID = tocontainer.RomoocID;
                            if (model.OPS_COTOContainer.Where(c => c.OPSContainerID == container.ID && c.COTOMasterID == null && !statusload.Contains(c.StatusOfCOContainerID)).Count() == 0)
                            {
                                container.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeApproved;
                                if (!lstOPSContainerApprovedID.Contains(tocontainer.OPSContainerID))
                                    lstOPSContainerApprovedID.Add(tocontainer.OPSContainerID);
                            }
                            else
                            {
                                container.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypePlanning;
                                if (!lstOPSContainerPlanningID.Contains(tocontainer.OPSContainerID))
                                    lstOPSContainerPlanningID.Add(tocontainer.OPSContainerID);
                            }
                        }
                        else
                        {
                            container.RomoocID = null;
                            container.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeOpen;
                        }
                    }
                }
                model.SaveChanges();

                foreach (var opscontainerplanningid in lstOPSContainerPlanningID)
                {
                    var firstMaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerplanningid && c.COTOMasterID > 0).Select(c => new { COTOMasterID = c.COTOMasterID.Value, c.OPS_Container.RomoocID, c.LocationToID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID, c.OPS_COTOMaster.ETA, c.OPS_COTOMaster.ATA }).FirstOrDefault();
                    if (firstMaster != null && firstMaster.RomoocID > 0)
                    {
                        var eta = firstMaster.ETA;
                        if (firstMaster.ATA != null)
                            eta = firstMaster.ATA.Value;

                        if (firstMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport || firstMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport)
                        {
                            var dtfrom = eta.AddHours(HourEmpty);
                            var toload = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerplanningid && statusload.Contains(c.StatusOfCOContainerID)).FirstOrDefault();
                            if (toload != null)
                            {
                                var timeinstock = HourInStockDefault;
                                var loc = model.CAT_Location.Where(c => c.ID == toload.LocationWaitID).Select(c => new { c.UnLoadTimeCO, c.LoadTimeCO }).FirstOrDefault();
                                if (loc != null)
                                {
                                    if (firstMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport && loc.UnLoadTimeCO > 0)
                                        timeinstock = loc.UnLoadTimeCO.Value;
                                    if (firstMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport && loc.LoadTimeCO > 0)
                                        timeinstock = loc.LoadTimeCO.Value;
                                }
                                if (timeinstock > 0)
                                {
                                    var dtto = dtfrom.AddHours(timeinstock);
                                    toload.ETD = dtfrom;
                                    toload.ETA = dtto;
                                    toload.DateFromCome = dtfrom;
                                    toload.DateToCome = dtto;
                                    toload.IsSplit = false;
                                    dtfrom = dtto.AddHours(HourEmpty);

                                    HelperTimeSheet.COTOMaster_RomoocInStock(model, account, firstMaster.COTOMasterID, timeinstock);
                                }
                            }

                            var totemp = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerplanningid && !statusload.Contains(c.StatusOfCOContainerID) && c.COTOMasterID == null).FirstOrDefault();
                            if (totemp != null)
                            {
                                var timetemp = (totemp.DateToCome.Value - totemp.DateFromCome.Value).TotalHours;
                                if (timetemp > 0)
                                {
                                    var dtto = dtfrom.AddHours(timetemp);
                                    totemp.ETD = dtfrom;
                                    totemp.ETA = dtto;
                                    totemp.DateFromCome = dtfrom;
                                    totemp.DateToCome = dtto;
                                    dtfrom = dtto.AddHours(HourEmpty);

                                    if (firstMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)
                                        HelperTimeSheet.COTOMaster_RomoocEmpty(model, account, firstMaster.COTOMasterID, timetemp);
                                    else
                                        HelperTimeSheet.COTOMaster_RomoocLaden(model, account, firstMaster.COTOMasterID, timetemp);
                                }
                            }
                        }
                    }
                }

                bool flag = false;
                foreach (var opscontainerapprovedid in lstOPSContainerApprovedID)
                {
                    var lstMasterID = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerapprovedid && c.COTOMasterID > 0).Select(c => c.COTOMasterID.Value).Distinct().ToList();
                    foreach (var masterid in lstMasterID)
                    {
                        foreach (var timesheet in model.FLM_AssetTimeSheet.Where(c => c.ReferID == masterid && (c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterEmpty || c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterLaden || c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMasterInStock)))
                        {
                            model.FLM_AssetTimeSheet.Remove(timesheet);
                            flag = true;
                        }
                    }
                }
                if (flag == true)
                    model.SaveChanges();

                flag = false;
                foreach (var opscontainerapprovedid in lstOPSContainerApprovedID)
                {
                    var lstMaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerapprovedid && c.COTOMasterID > 0 && c.ParentID == null).Select(c => new { COTOMasterID = c.COTOMasterID.Value, c.OPS_COTOMaster.ETD, c.OPS_COTOMaster.ETA, c.OPS_COTOMaster.ATD, c.OPS_COTOMaster.ATA, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).Distinct().OrderBy(c => c.ETD).ToList();
                    if (lstMaster.Count > 0)
                    {
                        var firstMaster = lstMaster[0];

                        if (firstMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || firstMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)
                        {
                            var lstMasterID = lstMaster.Select(c => c.COTOMasterID).Distinct().ToList();
                            if (lstMasterID.Count > 1)
                            {
                                var nextMaster = lstMaster[1];
                                var toload = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerapprovedid && statusload.Contains(c.StatusOfCOContainerID)).FirstOrDefault();
                                if (toload != null)
                                {
                                    var dtfrom = firstMaster.ETA;
                                    if (firstMaster.ATA != null)
                                        dtfrom = firstMaster.ATA.Value;
                                    var dtto = nextMaster.ETD;
                                    if (nextMaster.ATD != null)
                                        dtto = nextMaster.ATD.Value;
                                    double timeinstock = (dtto - dtfrom).TotalHours - (HourEmpty * 2);
                                    if (timeinstock > 0)
                                    {
                                        dtfrom = dtfrom.AddHours(HourEmpty);
                                        dtto = dtto.AddHours(-HourEmpty);
                                        toload.ETD = dtfrom;
                                        toload.ETA = dtto;
                                        toload.DateFromCome = dtfrom;
                                        toload.DateToCome = dtto;
                                        toload.IsSplit = false;

                                        HelperTimeSheet.COTOMaster_RomoocInStock(model, account, firstMaster.COTOMasterID, timeinstock);
                                    }
                                }
                            }
                        }
                        else if (firstMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal)
                        {
                            //var toload = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerplanningid && statusload.Contains(c.StatusOfCOContainerID)).FirstOrDefault();
                            //if (toload != null)
                            //{

                            //}
                        }
                    }
                }
            }
            else
            {
                var lstOPSContainerID = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => c.OPSContainerID).Distinct().ToList();
                if (lstOPSContainerID.Count > 0)
                {
                    foreach (var opscontainerid in lstOPSContainerID)
                    {
                        var opscontainer = model.OPS_Container.FirstOrDefault(c => c.ID == opscontainerid);
                        if (opscontainer != null)
                        {
                            if (model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.COTOMasterID > 0).Count() > 0)
                            {
                                opscontainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypePlanning;
                            }
                            else
                            {
                                opscontainer.RomoocID = null;
                                opscontainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeOpen;
                            }
                            opscontainer.ModifiedBy = account.UserName;
                            opscontainer.ModifiedDate = DateTime.Now;
                        }
                    }
                    model.SaveChanges();
                }
            }

            CO_ResetCOTOContainer(model, account, lstCOTOContainerID);

            //status order 
            var lstOrderID = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
            HelperStatus.ORDOrder_Status(model, account, lstOrderID);
        }

        private static void CO_ResetCOTOContainer(DataEntities model, AccountItem account, List<int> lstCOTOContainerID)
        {
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };

            var lstService = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID) && c.COTOMasterID > 0).Select(c => new { c.OPSContainerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).Distinct().ToList();
            if ((lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 2 && lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 2) ||
                (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 1 && lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 1))
            {
                bool haslocal = lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Count() > 0;
                var lstSwapID = new List<int>();
                foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport))
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                    {
                        if (tocontainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty)
                        {
                            tocontainer.IsDuplicateHidden = true;
                            lstSwapID.Add(tocontainer.ID);
                        }
                        else if (haslocal)
                            tocontainer.IsDuplicateHidden = false;
                        else
                            tocontainer.IsDuplicateHidden = null;
                    }
                }

                foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport))
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                    {
                        if (tocontainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty && lstSwapID.Count > 0)
                        {
                            tocontainer.IsSwap = true;
                            tocontainer.SwapID = lstSwapID[0];
                            lstSwapID.Remove(0);
                        }
                        tocontainer.IsDuplicateHidden = null;
                    }
                }
            }
            else if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 2 || lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 2)
            {
                var lstIMEX = lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).ToList();
                var first = lstIMEX[0];
                var second = lstIMEX[1];

                foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == first.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                {
                    tocontainer.IsDuplicateHidden = false;
                }

                foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == second.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                {
                    tocontainer.IsDuplicateHidden = true;
                }

                model.SaveChanges();
            }
            else if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 1 || lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 1)
            {
                bool haslocal = lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Count() > 0;
                foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport))
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                    {
                        if (haslocal)
                            tocontainer.IsDuplicateHidden = false;
                        else
                            tocontainer.IsDuplicateHidden = null;
                    }
                }
                model.SaveChanges();
            }

            if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Count() > 0 && lstService.Where(c => c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderLocal).Count() > 0)
            {
                foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal))
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                    {
                        tocontainer.IsDuplicateHidden = true;
                    }
                }
                model.SaveChanges();
            }
            else if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Count() > 0)
            {
                foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal))
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                    {
                        tocontainer.IsDuplicateHidden = null;
                    }
                }
                model.SaveChanges();
            }
        }

        private static void CO_ResetRomoocByOPSContainer(DataEntities model, AccountItem account, List<int> lstOPSContainerID)
        {
            if (lstOPSContainerID != null && lstOPSContainerID.Count > 0)
            {
                foreach (var opscontainerid in lstOPSContainerID)
                {
                    var objContainer = model.OPS_Container.FirstOrDefault(c => c.ID == opscontainerid);
                    if (objContainer != null)
                    {
                        var lstMaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.COTOMasterID > 0).Select(c => new { c.OPS_COTOMaster.ID, c.OPS_COTOMaster.RomoocID, c.OPS_COTOMaster.ETD, c.OPS_COTOMaster.ETA }).Distinct().OrderBy(c => c.ETD).ToList();
                        if (lstMaster.Count == 1)
                        {
                            objContainer.RomoocID = lstMaster[0].RomoocID;
                            if (model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.ParentID == null && c.COTOMasterID == null).Count() > 0)
                                objContainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypePlanning;
                            else
                                objContainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeApproved;
                            objContainer.ModifiedBy = account.UserName;
                            objContainer.ModifiedDate = DateTime.Now;
                            model.SaveChanges();

                            var assetRomooc = model.FLM_Asset.Where(c => c.RomoocID == objContainer.RomoocID).Select(c => new { c.ID }).FirstOrDefault();
                            if (assetRomooc != null)
                            {
                                var tomasterid = lstMaster[0].ID;
                                var timeSheet = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.AssetID == assetRomooc.ID && c.ReferID == tomasterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                                if (timeSheet == null)
                                {
                                    HelperTimeSheet.Remove(model, account, lstMaster[0].ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                                    HelperTimeSheet.Create(model, account, lstMaster[0].ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                                }
                            }
                        }
                        else if (lstMaster.Count > 1)
                        {
                            if (objContainer.RomoocID > 0)
                            {
                                var assetRomooc = model.FLM_Asset.Where(c => c.RomoocID == objContainer.RomoocID).Select(c => new { c.ID }).FirstOrDefault();
                                if (assetRomooc != null)
                                {
                                    foreach (var itemMaster in lstMaster)
                                    {
                                        var timeSheet = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.AssetID == assetRomooc.ID && c.ReferID == itemMaster.ID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                                        if (timeSheet == null)
                                        {
                                            HelperTimeSheet.Remove(model, account, lstMaster[0].ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                                            HelperTimeSheet.Create(model, account, lstMaster[0].ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                                        }
                                    }

                                    //var etd = lstMaster[0].ETD;
                                    //foreach (var itemMaster in lstMaster)
                                    //{
                                    //    var timeSheet = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.AssetID == assetRomooc.ID && c.ReferID == itemMaster.ID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                                    //    if (timeSheet != null)
                                    //    {
                                    //        if (etd.CompareTo(itemMaster.ETA) > 0)
                                    //            throw FaultHelper.BusinessFault(null, null, "timesheet fail etd");
                                    //        else
                                    //        {
                                    //            timeSheet.DateFrom = etd;
                                    //            timeSheet.DateFromActual = etd;
                                    //            timeSheet.DateTo = itemMaster.ETA;
                                    //            timeSheet.DateToActual = itemMaster.ETA;
                                    //            timeSheet.ModifiedBy = account.UserName;
                                    //            timeSheet.ModifiedDate = DateTime.Now;
                                    //        }
                                    //        etd = itemMaster.ETA.AddHours(0.1);
                                    //    }
                                    //    else
                                    //        throw FaultHelper.BusinessFault(null, null, "can't find timesheet");
                                    //}
                                    //model.SaveChanges();
                                }
                            }
                        }
                        else
                        {
                            objContainer.RomoocID = null;
                            objContainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeOpen;
                            objContainer.ModifiedBy = account.UserName;
                            objContainer.ModifiedDate = DateTime.Now;
                            model.SaveChanges();
                        }
                    }
                }

            }
        }




        public static DTOOPSCOTOMaster OPSCO_CreateItemOfferLeadtime(DataEntities model, AccountItem account, List<int> lstCOTOContainerID)
        {
            var result = new DTOOPSCOTOMaster();
            result.HourETAOffer = 1;
            if (lstCOTOContainerID != null && lstCOTOContainerID.Count > 0)
            {
                lstCOTOContainerID = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID) && c.COTOMasterID == null).Select(c => c.ID).ToList();
                if (lstCOTOContainerID.Count > 0)
                {
                    var lstOPSCON = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new { c.OPSContainerID, c.StatusOfCOContainerID, c.OPS_Container.ORD_Container.ETDOld, c.OPS_Container.ORD_Container.ETAOld }).ToList();
                    var lstOPSContainerID = lstOPSCON.Select(c => c.OPSContainerID).Distinct().ToList();
                    var isOrder = false;
                    foreach (var opscontainerid in lstOPSContainerID)
                    {
                        var queryCheck = lstOPSCON.Where(c => c.OPSContainerID == opscontainerid);
                        if (queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).Count() > 0 &&
                            queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Count() > 0)
                        {
                            isOrder = true;
                        }
                        if (queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty).Count() > 0 &&
                            queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).Count() > 0)
                        {
                            isOrder = true;
                        }
                        if (queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).Count() > 0 &&
                            queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty).Count() == 0 &&
                            queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).Count() == 0)
                        {
                            isOrder = true;
                        }
                    }
                    if (isOrder)
                    {
                        foreach (var opscontainerid in lstOPSContainerID)
                        {
                            var first = lstOPSCON.Where(c => c.OPSContainerID == opscontainerid && c.ETDOld != null).OrderBy(c => c.ETDOld).FirstOrDefault();
                            if (first != null)
                            {
                                if (result.DateMin == null)
                                    result.DateMin = first.ETDOld;
                                else if (result.DateMin.Value.CompareTo(first.ETDOld.Value) > 0)
                                    result.DateMin = first.ETDOld;
                            }
                            first = lstOPSCON.Where(c => c.OPSContainerID == opscontainerid && c.ETAOld != null).OrderBy(c => c.ETAOld).FirstOrDefault();
                            if (first != null)
                            {
                                if (result.DateMax == null)
                                    result.DateMax = first.ETAOld;
                                else if (result.DateMax.Value.CompareTo(first.ETAOld.Value) > 0)
                                    result.DateMax = first.ETAOld;
                            }
                        }
                        result.HourETAOffer = 2;
                    }
                    else
                    {
                        if (lstOPSContainerID.Count > 0)
                        {
                            var lstOPSContainer = model.OPS_Container.Where(c => lstOPSContainerID.Contains(c.ID)).Select(c => new { c.ID, c.ORD_Container.ETDOld, c.ORD_Container.ETAOld, c.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).ToList();
                            foreach (var opscontainer in lstOPSContainer)
                            {
                                var tomaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainer.ID && c.COTOMasterID > 0).Select(c => new { c.COTOMasterID, c.OPS_COTOMaster.ETD, c.OPS_COTOMaster.ETA, c.OPS_COTOMaster.ATD, c.OPS_COTOMaster.ATA }).OrderByDescending(c => c.ETA).FirstOrDefault();
                                if (tomaster != null)
                                {
                                    var eta = tomaster.ETA;
                                    if (tomaster.ATA != null)
                                        eta = tomaster.ATA.Value;
                                    result.DateMin = eta.AddHours(0.1);
                                    result.DateMax = eta;
                                    if (opscontainer.ETAOld != null && (opscontainer.ETAOld.Value - result.DateMin.Value).TotalHours > 0)
                                        result.DateMax = opscontainer.ETAOld.Value;
                                }
                                else
                                {
                                    result.DateMin = opscontainer.ETDOld;
                                    result.DateMax = opscontainer.ETAOld;
                                }
                                result.HourETAOffer = 0;
                                foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainer.ID && c.COTOMasterID == null).Select(c => new { c.LocationFromID, c.LocationToID }))
                                {
                                    var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocontainer.LocationFromID && c.LocationToID == tocontainer.LocationToID).Select(c => new { c.Hour }).FirstOrDefault();
                                    if (matrix != null && matrix.Hour > 0)
                                        result.HourETAOffer += matrix.Hour;
                                    else
                                        result.HourETAOffer += 1;
                                }
                            }

                            if (result.DateMin != null && result.HourETAOffer > 0)
                            {
                                if (result.DateMax == null || (result.DateMax != null && (result.DateMax.Value - result.DateMin.Value).TotalHours < result.HourETAOffer))
                                    result.DateMax = result.DateMin.Value.AddHours(result.HourETAOffer);
                            }
                            if (result.HourETAOffer < 1)
                                result.HourETAOffer = 1;
                        }

                        //foreach (var opscontainerid in lstOPSContainerID)
                        //{

                        //    //if (model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.COTOMasterID > 0).Count())
                        //    //{

                        //    //}

                        //    //if(model.OPS_COTOContainer.Where(c=))
                        //    var queryCheck = lstOPSCON.Where(c => c.OPSContainerID == opscontainerid);

                        //    if (queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).Count() > 0 &&
                        //        queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Count() == 0)
                        //    {
                        //        var first = queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty && c.ETDOld != null).FirstOrDefault();
                        //        if (first != null)
                        //        {
                        //            result.DateMin = first.ETDOld.Value;
                        //            result.DateMax = first.ETDOld.Value.AddHours(0.5);
                        //        }
                        //    }
                        //    if (queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).Count() == 0 &&
                        //        queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Count() > 0)
                        //    {
                        //        var first = queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden && c.ETDOld != null).FirstOrDefault();
                        //        if (first != null)
                        //        {
                        //            result.DateMin = first.ETDOld.Value.AddHours(-0.5);
                        //            result.DateMax = first.ETAOld.Value;
                        //        }
                        //    }
                        //    if (queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty).Count() > 0 &&
                        //        queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).Count() == 0)
                        //    {
                        //        var first = queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty && c.ETAOld != null).FirstOrDefault();
                        //        if (first != null)
                        //        {
                        //            result.DateMin = first.ETAOld.Value;
                        //            result.DateMax = first.ETAOld.Value.AddHours(0.5);
                        //        }
                        //    }
                        //    if (queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty).Count() == 0 &&
                        //        queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).Count() > 0)
                        //    {
                        //        var first = queryCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden && c.ETDOld != null).FirstOrDefault();
                        //        if (first != null)
                        //        {
                        //            result.DateMin = first.ETDOld.Value;
                        //            result.DateMax = first.ETAOld.Value;
                        //        }
                        //    }
                        //}
                        //if (result.DateMin != null && result.DateMax != null)
                        //{
                        //    if ((result.DateMax.Value - result.DateMin.Value).TotalHours < 1)
                        //        result.DateMin = result.DateMax.Value.AddHours(-1);
                        //}
                        //result.HourETAOffer = 1;
                    }
                }
            }
            return result;
        }

        public static DTOOPSCOTOMaster OPSCO_CreateItemOfferTime(DataEntities model, AccountItem account, List<int> lstCOTOContainerID, DateTime etd, DateTime eta, int? vehicleid, int? romoocid)
        {
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad, 
                -(int)SYSVarType.StatusOfCOContainerLoad
            };
            if (lstCOTOContainerID != null && lstCOTOContainerID.Count > 0)
            {
                lstCOTOContainerID = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID) && c.COTOMasterID == null && !statusload.Contains(c.StatusOfCOContainerID)).Select(c => c.ID).ToList();
                //var objSetting = OPSCO_GetSetting(model, account);
                if (lstCOTOContainerID.Count > 0)
                {
                    var result = new DTOOPSCOTOMaster();
                    result.ETD = etd;
                    result.ETA = eta;
                    result.DateMin = etd;
                    result.DateMax = eta;
                    result.HourETAOffer = HourMatrixDefault;
                    //result.RomoocID = -1;
                    result.RomoocID = romoocid;
                    if (result.RomoocID == null || result.RomoocID < 1)
                        result.RomoocID = DefaultRomooc;
                    result.RomoocNo = "";
                    if (result.RomoocID > 0)
                    {
                        var objRomooc = model.CAT_Romooc.Where(c => c.ID == result.RomoocID.Value).Select(c => new { c.RegNo }).FirstOrDefault();
                        if (objRomooc != null)
                            result.RomoocNo = objRomooc.RegNo;
                    }
                    result.AllowChangeRomooc = true;
                    result.ListCOContainer = new List<DTOOPSCOTOContainer>();
                    result.OfferTimeError = string.Empty;
                    result.OfferTimeWarning = string.Empty;

                    if (etd.CompareTo(eta) < 0 && etd.CompareTo(DateTime.MinValue) > 0)
                    {
                        //bool flag = false;  
                        var lstServiceID = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID).Distinct().ToList();
                        if (lstServiceID.Count == 1)
                        {
                            //1 service
                            var lstCOTOContainer = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new
                            {
                                c.ID,
                                c.ETD,
                                c.ETA,
                                c.OPS_Container.ORD_Container.ETDOld,
                                c.OPS_Container.ORD_Container.ETAOld,
                                c.OPS_Container.ORD_Container.ORD_Order.RequestDate,
                                c.SortOrder,
                                c.OPSContainerID,
                                c.COTOMasterID,
                                c.ParentID,
                                c.StatusOfCOContainerID,
                                c.LocationFromID,
                                c.LocationToID,
                                c.OPS_Container.RomoocID,
                                RomoocNo = c.OPS_Container.RomoocID > 0 ? c.OPS_Container.CAT_Romooc.RegNo : "",
                                c.OPS_Container.OPSContainerTypeID,
                                c.IsDuplicateHidden,
                                c.IsSwap
                            }).ToList();
                            var lstOPSContainerID = lstCOTOContainer.Select(c => c.OPSContainerID).Distinct().ToList();
                            if ((lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).Count() > 0 && lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Count() == 0) ||
                                (lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Count() > 0 && lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).Count() == 0) ||
                                (lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).Count() > 0 && lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty).Count() == 0) ||
                                (lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty).Count() > 0 && lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).Count() == 0))
                            {
                                //state

                                if (lstCOTOContainer.Count == 1)
                                {
                                    //has 1 state
                                    var tocontainer = lstCOTOContainer.FirstOrDefault();
                                    double totalHour = (eta - etd).TotalHours;
                                    double totalMatrix = 0;

                                    var firstMaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == tocontainer.OPSContainerID && c.COTOMasterID > 0).Select(c => new { c.COTOMasterID }).FirstOrDefault();
                                    if (firstMaster != null)
                                    {
                                        //state 2
                                        var lstFirstOPSContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == firstMaster.COTOMasterID).Select(c => new { c.OPSContainerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).Distinct().ToList();
                                        if (lstFirstOPSContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 2 || lstFirstOPSContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 2)
                                        {
                                            //state 2 - 2 con
                                            var otherOPSContainer = lstFirstOPSContainer.FirstOrDefault(c => c.OPSContainerID != tocontainer.OPSContainerID);
                                            if (otherOPSContainer != null)
                                            {
                                                var tocontainerother = model.OPS_COTOContainer.Where(c => c.OPSContainerID == otherOPSContainer.OPSContainerID && c.COTOMasterID == null && !statusload.Contains(c.StatusOfCOContainerID)).Select(c => new
                                                {
                                                    c.ID,
                                                    c.ETD,
                                                    c.ETA,
                                                    c.OPS_Container.ORD_Container.ETDOld,
                                                    c.OPS_Container.ORD_Container.ETAOld,
                                                    c.OPS_Container.ORD_Container.ORD_Order.RequestDate,
                                                    c.OPS_Container.ORD_Container.CutOffTime,
                                                    c.SortOrder,
                                                    c.OPSContainerID,
                                                    c.COTOMasterID,
                                                    c.ParentID,
                                                    c.StatusOfCOContainerID,
                                                    c.LocationFromID,
                                                    c.LocationToID,
                                                    c.OPS_Container.RomoocID,
                                                    RomoocNo = c.OPS_Container.RomoocID > 0 ? c.OPS_Container.CAT_Romooc.RegNo : "",
                                                    c.OPS_Container.OPSContainerTypeID,
                                                    c.IsDuplicateHidden
                                                }).FirstOrDefault();
                                                if (tocontainerother != null)
                                                {
                                                    var etdchange = etd;
                                                    var etachange = eta;
                                                    if (tocontainerother.CutOffTime != null)
                                                        result.DateMax = tocontainerother.CutOffTime.Value.AddHours(3);
                                                    else if (tocontainerother.ETAOld != null)
                                                        result.DateMax = tocontainerother.ETAOld.Value.AddHours(3);
                                                    else
                                                        result.DateMax = eta.AddHours(3);

                                                    if (tocontainer.LocationFromID == tocontainerother.LocationFromID && tocontainerother.LocationToID == tocontainerother.LocationToID)
                                                    {
                                                        var matrixhour = HourMatrixDefault;
                                                        var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocontainer.LocationFromID && c.LocationToID == tocontainerother.LocationFromID).Select(c => new { c.Hour }).FirstOrDefault();
                                                        if (matrix != null && matrix.Hour > 0)
                                                            matrixhour = matrix.Hour;
                                                        totalMatrix += matrixhour;

                                                        result.ETD = etdchange;
                                                        result.ETA = etachange;

                                                        result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                        {
                                                            ID = tocontainer.ID,
                                                            LocationFromID = tocontainer.LocationFromID,
                                                            LocationToID = tocontainer.LocationToID,
                                                            ETD = etdchange,
                                                            ETA = etachange,
                                                            SortOrder = tocontainer.SortOrder
                                                        });

                                                        result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                        {
                                                            ID = tocontainerother.ID,
                                                            LocationFromID = tocontainerother.LocationFromID,
                                                            LocationToID = tocontainerother.LocationToID,
                                                            ETD = etdchange,
                                                            ETA = etachange,
                                                            SortOrder = tocontainerother.SortOrder
                                                        });
                                                    }
                                                    else if (tocontainer.LocationFromID != tocontainerother.LocationFromID && tocontainer.LocationToID != tocontainerother.LocationToID)
                                                    {
                                                        var matrixhour = HourMatrixDefault;
                                                        var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocontainer.LocationFromID && c.LocationToID == tocontainerother.LocationFromID).Select(c => new { c.Hour }).FirstOrDefault();
                                                        if (matrix != null && matrix.Hour > 0)
                                                            matrixhour = matrix.Hour;
                                                        totalMatrix += matrixhour;
                                                        etachange = etachange.AddHours(matrixhour);

                                                        matrixhour = HourMatrixDefault;
                                                        matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocontainer.LocationFromID && c.LocationToID == tocontainerother.LocationFromID).Select(c => new { c.Hour }).FirstOrDefault();
                                                        if (matrix != null && matrix.Hour > 0)
                                                            matrixhour = matrix.Hour;
                                                        totalMatrix += matrixhour;
                                                        etachange = etachange.AddHours(matrixhour);

                                                        result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                        {
                                                            ID = tocontainer.ID,
                                                            LocationFromID = tocontainer.LocationFromID,
                                                            LocationToID = tocontainer.LocationToID,
                                                            ETD = etdchange,
                                                            ETA = etachange,
                                                            SortOrder = tocontainer.SortOrder
                                                        });

                                                        result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                        {
                                                            ID = tocontainerother.ID,
                                                            LocationFromID = tocontainerother.LocationFromID,
                                                            LocationToID = tocontainerother.LocationToID,
                                                            ETD = etdchange,
                                                            ETA = etachange,
                                                            SortOrder = tocontainerother.SortOrder
                                                        });
                                                    }
                                                    else
                                                    {
                                                        var matrixhour = HourMatrixDefault;
                                                        var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocontainer.LocationToID && c.LocationToID == tocontainerother.LocationToID).Select(c => new { c.Hour }).FirstOrDefault();
                                                        if (matrix != null && matrix.Hour > 0)
                                                            matrixhour = matrix.Hour;
                                                        totalMatrix += matrixhour;
                                                        etachange = etachange.AddHours(matrixhour);

                                                        result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                        {
                                                            ID = tocontainer.ID,
                                                            LocationFromID = tocontainer.LocationFromID,
                                                            LocationToID = tocontainer.LocationToID,
                                                            ETD = etdchange,
                                                            ETA = etachange,
                                                            SortOrder = tocontainer.SortOrder
                                                        });

                                                        result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                        {
                                                            ID = tocontainerother.ID,
                                                            LocationFromID = tocontainerother.LocationFromID,
                                                            LocationToID = tocontainerother.LocationToID,
                                                            ETD = etdchange,
                                                            ETA = etachange,
                                                            SortOrder = tocontainerother.SortOrder
                                                        });
                                                    }
                                                }
                                            }
                                            //end state 2 - 2 con
                                        }
                                        else if (lstFirstOPSContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 1 && lstFirstOPSContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 1)
                                        {
                                            //state 2 - con swap

                                            var dtFrom = etd;
                                            var dtTo = eta;

                                            var matrixhour = HourMatrixDefault;
                                            var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocontainer.LocationFromID && c.LocationToID == tocontainer.LocationToID).Select(c => new { c.Hour }).FirstOrDefault();
                                            if (matrix != null && matrix.Hour > 0)
                                                matrixhour = matrix.Hour;
                                            totalMatrix += matrixhour;

                                            result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                            {
                                                ID = tocontainer.ID,
                                                LocationFromID = tocontainer.LocationFromID,
                                                LocationToID = tocontainer.LocationToID,
                                                ETD = dtFrom,
                                                ETA = dtTo,
                                                SortOrder = tocontainer.SortOrder
                                            });

                                            //end state 2 - con swap
                                        }
                                        else if (lstFirstOPSContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 1 || lstFirstOPSContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 1)
                                        {
                                            //state 2 - 1 con

                                            var dtFrom = etd;
                                            var dtTo = eta;

                                            var matrixhour = HourMatrixDefault;
                                            var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocontainer.LocationFromID && c.LocationToID == tocontainer.LocationToID).Select(c => new { c.Hour }).FirstOrDefault();
                                            if (matrix != null && matrix.Hour > 0)
                                                matrixhour = matrix.Hour;
                                            totalMatrix += matrixhour;

                                            result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                            {
                                                ID = tocontainer.ID,
                                                LocationFromID = tocontainer.LocationFromID,
                                                LocationToID = tocontainer.LocationToID,
                                                ETD = dtFrom,
                                                ETA = dtTo,
                                                SortOrder = tocontainer.SortOrder
                                            });

                                            //end state 2 - 1 con
                                        }

                                        result.HourETAOffer = totalMatrix;

                                        var opsPlanning = lstCOTOContainer.FirstOrDefault(c => c.RomoocID > 0);
                                        if (opsPlanning != null)
                                        {
                                            result.RomoocID = opsPlanning.RomoocID;
                                            result.RomoocNo = opsPlanning.RomoocNo;
                                            result.AllowChangeRomooc = false;

                                            if (firstMaster != null && firstMaster.COTOMasterID > 0)
                                            {
                                                var prev = model.FLM_AssetTimeSheet.Where(c => c.ReferID == firstMaster.COTOMasterID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster).Select(c => new { c.DateToActual }).OrderByDescending(c => c.DateToActual).FirstOrDefault();
                                                if (prev != null)
                                                    result.DateMin = prev.DateToActual.AddHours(HourEmpty);
                                            }
                                        }

                                        if (result.RomoocID > 0 && result.RomoocID != DefaultRomooc)
                                        {
                                            result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, result.RomoocID, null, etd, eta);
                                            if (string.IsNullOrEmpty(result.OfferTimeError))
                                                result.OfferTimeError = CO_CheckRomoocAvailable(model, account, result.RomoocID, lstCOTOContainerID);
                                        }
                                        else
                                        {
                                            if (romoocid > 0)
                                                result.RomoocID = romoocid;
                                            var objRomooc = model.CAT_Romooc.Where(c => c.ID == result.RomoocID).Select(c => new { c.RegNo }).FirstOrDefault();
                                            if (objRomooc != null)
                                                result.RomoocNo = objRomooc.RegNo;
                                            result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, result.RomoocID, null, etd, eta);
                                            if (string.IsNullOrEmpty(result.OfferTimeError))
                                                result.OfferTimeError = CO_CheckRomoocAvailable(model, account, result.RomoocID, lstCOTOContainerID);
                                        }

                                        //end state 2
                                    }
                                    else
                                    {
                                        //state 1

                                        var dtFrom = etd;
                                        var dtTo = eta;

                                        var matrixhour = HourMatrixDefault;
                                        var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocontainer.LocationFromID && c.LocationToID == tocontainer.LocationToID).Select(c => new { c.Hour }).FirstOrDefault();
                                        if (matrix != null && matrix.Hour > 0)
                                            matrixhour = matrix.Hour;
                                        totalMatrix += matrixhour;

                                        result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                        {
                                            ID = tocontainer.ID,
                                            LocationFromID = tocontainer.LocationFromID,
                                            LocationToID = tocontainer.LocationToID,
                                            ETD = dtFrom,
                                            ETA = dtTo,
                                            SortOrder = tocontainer.SortOrder
                                        });

                                        result.HourETAOffer = totalMatrix;
                                        result.DateMin = tocontainer.RequestDate;
                                        result.DateMax = tocontainer.ETAOld.Value.AddHours(1);

                                        if (result.RomoocID > 0 && result.RomoocID != DefaultRomooc)
                                        {
                                            result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, result.RomoocID, null, etd, eta);
                                            if (string.IsNullOrEmpty(result.OfferTimeError))
                                                result.OfferTimeError = CO_CheckRomoocAvailable(model, account, result.RomoocID, lstCOTOContainerID);
                                        }
                                        else
                                        {
                                            if (romoocid > 0)
                                                result.RomoocID = romoocid;
                                            var objRomooc = model.CAT_Romooc.Where(c => c.ID == result.RomoocID).Select(c => new { c.RegNo }).FirstOrDefault();
                                            if (objRomooc != null)
                                                result.RomoocNo = objRomooc.RegNo;
                                            result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, result.RomoocID, null, etd, eta);
                                            if (string.IsNullOrEmpty(result.OfferTimeError))
                                                result.OfferTimeError = CO_CheckRomoocAvailable(model, account, result.RomoocID, lstCOTOContainerID);
                                        }
                                        //end state 1
                                    }
                                    //end has 1 state 
                                }
                                else
                                {
                                    //has more state

                                    double totalHour = (eta - etd).TotalHours;
                                    double totalMatrix = 0;
                                    var lstMasterPrevID = new List<int>();
                                    foreach (var opsContainerID in lstOPSContainerID)
                                    {
                                        var queryContainer = lstCOTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.ParentID == null).OrderBy(c => c.SortOrder);
                                        var divHour = totalHour / queryContainer.Count();
                                        var dtFrom = etd;
                                        var dtTo = dtFrom.AddHours(divHour);
                                        foreach (var item in queryContainer)
                                        {
                                            var matrixhour = HourMatrixDefault;
                                            var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == item.LocationFromID && c.LocationToID == item.LocationToID).Select(c => new { c.Hour }).FirstOrDefault();
                                            if (matrix != null && matrix.Hour > 0)
                                                matrixhour = matrix.Hour;
                                            result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                            {
                                                ID = item.ID,
                                                LocationFromID = item.LocationFromID,
                                                LocationToID = item.LocationToID,
                                                ETD = dtFrom,
                                                ETA = dtTo,
                                                SortOrder = item.SortOrder
                                            });

                                            totalMatrix += matrixhour;
                                            dtFrom = dtTo.AddHours(HourEmpty);
                                            dtTo = dtFrom.AddHours(divHour);
                                        }

                                        var lstid = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.COTOMasterID > 0).Select(c => c.COTOMasterID.Value).Distinct().ToList();
                                        if (lstid.Count > 0)
                                            lstMasterPrevID.AddRange(lstid);
                                    }

                                    result.HourETAOffer = totalMatrix;

                                    var opsPlanning = lstCOTOContainer.FirstOrDefault(c => c.RomoocID > 0 && c.OPSContainerTypeID == -(int)SYSVarType.OPSContainerTypePlanning);
                                    if (opsPlanning != null)
                                    {
                                        result.RomoocID = opsPlanning.RomoocID;
                                        result.RomoocNo = opsPlanning.RomoocNo;
                                        result.AllowChangeRomooc = false;

                                        if (lstMasterPrevID.Count > 0)
                                        {
                                            var prev = model.FLM_AssetTimeSheet.Where(c => lstMasterPrevID.Contains(c.ReferID) && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster).Select(c => new { c.DateToActual }).OrderByDescending(c => c.DateToActual).FirstOrDefault();
                                            if (prev != null)
                                                result.DateMin = prev.DateToActual.AddHours(HourEmpty);
                                        }
                                    }

                                    if (result.RomoocID > 0 && result.RomoocID != DefaultRomooc)
                                    {
                                        result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, result.RomoocID, null, etd, eta);
                                        if (string.IsNullOrEmpty(result.OfferTimeError))
                                            result.OfferTimeError = CO_CheckRomoocAvailable(model, account, result.RomoocID, lstCOTOContainerID);
                                    }
                                    else
                                    {
                                        if (romoocid > 0)
                                            result.RomoocID = romoocid;
                                        var objRomooc = model.CAT_Romooc.Where(c => c.ID == result.RomoocID).Select(c => new { c.RegNo }).FirstOrDefault();
                                        if (objRomooc != null)
                                            result.RomoocNo = objRomooc.RegNo;
                                        result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, result.RomoocID, null, etd, eta);
                                        if (string.IsNullOrEmpty(result.OfferTimeError))
                                            result.OfferTimeError = CO_CheckRomoocAvailable(model, account, result.RomoocID, lstCOTOContainerID);
                                    }
                                    //end has more state
                                }
                            }
                            else
                            {
                                //order
                                //result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, romoocid, null, etd, eta);
                                //if (string.IsNullOrEmpty(result.OfferTimeError))
                                //    result.OfferTimeError = CO_CheckRomoocAvailable(model, account, romoocid, lstCOTOContainerID);

                                foreach (var opsContainerID in lstOPSContainerID)
                                {
                                    if (lstCOTOContainer.Where(c => c.OPSContainerID == opsContainerID && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden)).Count() > 0)
                                    {
                                        double timeinstock = HourInStockDefault;
                                        var first = lstCOTOContainer.FirstOrDefault(c => c.OPSContainerID == opsContainerID && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden);
                                        if (first != null)
                                        {
                                            var loc = model.CAT_Location.Where(c => c.ID == first.LocationFromID).Select(c => new { c.UnLoadTimeCO, c.LoadTimeCO }).FirstOrDefault();
                                            if (loc != null && loc.LoadTimeCO > 0)
                                            {
                                                timeinstock = loc.LoadTimeCO.Value;
                                            }
                                        }
                                        else if (first == null)
                                        {
                                            first = lstCOTOContainer.FirstOrDefault(c => c.OPSContainerID == opsContainerID && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden);
                                            if (first != null)
                                            {
                                                var loc = model.CAT_Location.Where(c => c.ID == first.LocationToID).Select(c => new { c.UnLoadTimeCO, c.LoadTimeCO }).FirstOrDefault();
                                                if (loc != null && loc.UnLoadTimeCO > 0)
                                                {
                                                    timeinstock = loc.UnLoadTimeCO.Value;
                                                }
                                            }
                                        }
                                        if (first != null)
                                        {
                                            result.DateMin = first.RequestDate;
                                            result.DateMax = first.ETAOld.Value.AddHours(1);
                                            result.HourETAOffer = 0;
                                            foreach (var tocontainer in lstCOTOContainer.Where(c => c.OPSContainerID == opsContainerID))
                                            {
                                                var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocontainer.LocationFromID && c.LocationToID == tocontainer.LocationToID).Select(c => new { c.KM, c.Hour }).FirstOrDefault();
                                                if (matrix != null && matrix.Hour > 0)
                                                    result.HourETAOffer += matrix.Hour;
                                                else
                                                    result.HourETAOffer += HourMatrixDefault;
                                            }
                                        }

                                        //if ((eta - etd).TotalHours < (result.HourETAOffer + timeinstock))
                                        //{
                                        //    eta = etd.AddHours(result.HourETAOffer + timeinstock);
                                        //}
                                        var etaChange = etd.AddHours(result.HourETAOffer + timeinstock);

                                        var lstChange = lstCOTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.ParentID == null).OrderBy(c => c.SortOrder).ToList();
                                        if (lstChange.Count == 2)
                                        {
                                            double totalHour = (etaChange - etd).TotalHours - timeinstock;
                                            double divHour = totalHour / 2;

                                            var item = lstChange[0];
                                            result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                            {
                                                ID = item.ID,
                                                LocationFromID = item.LocationFromID,
                                                LocationToID = item.LocationToID,
                                                ETD = etd,
                                                ETA = etd.AddHours(divHour),
                                                SortOrder = item.SortOrder
                                            });

                                            item = lstChange[1];
                                            result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                            {
                                                ID = item.ID,
                                                LocationFromID = item.LocationFromID,
                                                LocationToID = item.LocationToID,
                                                ETD = etaChange.AddHours(-divHour),
                                                ETA = etaChange,
                                                SortOrder = item.SortOrder
                                            });
                                        }
                                    }
                                    else
                                    {
                                        var queryContainer = lstCOTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.ParentID == null).OrderBy(c => c.SortOrder);
                                        int total = queryContainer.Count();
                                        double totalHour = (eta - etd).TotalHours;
                                        double divHour = totalHour / total;
                                        var startDate = etd;
                                        foreach (var item in queryContainer)
                                        {
                                            result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                            {
                                                ID = item.ID,
                                                LocationFromID = item.LocationFromID,
                                                LocationToID = item.LocationToID,
                                                ETD = startDate,
                                                ETA = startDate.AddHours(divHour),
                                                SortOrder = item.SortOrder
                                            });
                                            startDate = startDate.AddHours(divHour);
                                        }
                                    }
                                }

                                result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, romoocid, null, etd, eta);
                                if (string.IsNullOrEmpty(result.OfferTimeError))
                                    result.OfferTimeError = CO_CheckRomoocAvailable(model, account, romoocid, lstCOTOContainerID);
                            }
                        }
                        else if (lstServiceID.Count == 2)
                        {
                            //2 service        
                            var lstCOTOContainer = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new
                            {
                                c.ID,
                                c.ETD,
                                c.ETA,
                                c.OPS_Container.ORD_Container.ETDOld,
                                c.OPS_Container.ORD_Container.ETAOld,
                                c.OPS_Container.ORD_Container.ORD_Order.RequestDate,
                                c.SortOrder,
                                c.OPSContainerID,
                                c.COTOMasterID,
                                c.ParentID,
                                c.StatusOfCOContainerID,
                                c.LocationFromID,
                                c.LocationToID,
                                c.OPS_Container.RomoocID,
                                RomoocNo = c.OPS_Container.RomoocID > 0 ? c.OPS_Container.CAT_Romooc.RegNo : "",
                                c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID,
                                c.OPS_Container.OPSContainerTypeID,
                            }).ToList();

                            double totalHour = (eta - etd).TotalHours;
                            //double divHour = totalHour / lstCOTOContainer.Count;
                            var startDate = etd;

                            if ((lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderExport) && lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderLocal)) ||
                                lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderImport) && lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderLocal))
                            {
                                if ((lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).Count() > 0 && lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Count() == 0) ||
                                    (lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Count() > 0 && lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).Count() == 0) ||
                                    (lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).Count() > 0 && lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty).Count() == 0) ||
                                    (lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty).Count() > 0 && lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).Count() == 0))
                                {
                                    //state
                                    var lstOPSContainerID = lstCOTOContainer.Select(c => c.OPSContainerID).Distinct().ToList();
                                    double totalMatrix = 0;
                                    var lstMasterPrevID = new List<int>();
                                    totalHour = totalHour - (HourEmpty * (lstCOTOContainer.Count + 1));
                                    var divHour = totalHour / (lstCOTOContainer.Count + 1);
                                    var dtFrom = etd;
                                    var dtTo = eta;
                                    //var lstOPSContainerID = lstCOTOContainer.Select(c => c.OPSContainerID).Distinct().ToList();
                                    var lstOPSContainerEmptyID = lstCOTOContainer.Where(c => c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderLocal).Select(c => c.OPSContainerID).Distinct().ToList();
                                    //var lstMasterPrevID = new List<int>();
                                    //double totalMatrix = 0;
                                    foreach (var opsContainerID in lstOPSContainerEmptyID)
                                    {
                                        var queryContainer = lstCOTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.ParentID == null).OrderBy(c => c.SortOrder);
                                        //var startDate = etd;
                                        //var divHour = totalHour / queryContainer.Count();
                                        //var dtFrom = etd;
                                        //var dtTo = eta;

                                        foreach (var item in queryContainer)
                                        {
                                            var matrixhour = HourMatrixDefault;
                                            var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == item.LocationFromID && c.LocationToID == item.LocationToID).Select(c => new { c.Hour }).FirstOrDefault();
                                            if (matrix != null && matrix.Hour > 0)
                                                matrixhour = matrix.Hour;
                                            result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                            {
                                                ID = item.ID,
                                                LocationFromID = item.LocationFromID,
                                                LocationToID = item.LocationToID,
                                                ETD = dtFrom,
                                                ETA = dtTo,
                                                SortOrder = item.SortOrder
                                            });
                                            totalMatrix += matrixhour;
                                            dtFrom = dtTo.AddHours(HourEmpty);
                                            dtTo = dtFrom.AddHours(divHour);
                                        }

                                        var lstid = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.COTOMasterID > 0).Select(c => c.COTOMasterID.Value).Distinct().ToList();
                                        if (lstid.Count > 0)
                                            lstMasterPrevID.AddRange(lstid);
                                    }

                                    dtFrom = etd.AddHours(divHour + HourEmpty);
                                    dtTo = dtFrom.AddHours(divHour);
                                    foreach (var item in lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal))
                                    {
                                        var matrixhour = HourMatrixDefault;
                                        var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == item.LocationFromID && c.LocationToID == item.LocationToID).Select(c => new { c.Hour }).FirstOrDefault();
                                        if (matrix != null && matrix.Hour > 0)
                                            matrixhour = matrix.Hour;
                                        //var instockhour = HourInStockDefault;
                                        //var stockLoad = model.CAT_Location.Where(c => c.ID == item.LocationFromID).Select(c => new { c.LoadTimeCO, c.UnLoadTimeCO }).FirstOrDefault();
                                        //if (stockLoad != null && stockLoad.LoadTimeCO > 0)
                                        //    instockhour = stockLoad.LoadTimeCO.Value;

                                        result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                        {
                                            ID = item.ID,
                                            LocationFromID = item.LocationFromID,
                                            LocationToID = item.LocationToID,
                                            ETD = dtFrom,
                                            ETA = dtTo,
                                            SortOrder = item.SortOrder
                                        });

                                        //stockLoad = model.CAT_Location.Where(c => c.ID == item.LocationToID).Select(c => new { c.LoadTimeCO, c.UnLoadTimeCO }).FirstOrDefault();
                                        //if (stockLoad != null && stockLoad.UnLoadTimeCO > 0)
                                        //    instockhour += stockLoad.UnLoadTimeCO.Value;
                                        //else
                                        //    instockhour += HourInStockDefault;
                                        //instockhour += 0.4;

                                        totalMatrix += matrixhour;
                                        dtFrom = dtTo.AddHours(HourEmpty);
                                        dtTo = dtFrom.AddHours(divHour);
                                    }

                                    result.HourETAOffer = totalMatrix;

                                    var opsPlanning = lstCOTOContainer.FirstOrDefault(c => c.RomoocID > 0 && c.OPSContainerTypeID == -(int)SYSVarType.OPSContainerTypePlanning);
                                    if (opsPlanning != null)
                                    {
                                        result.RomoocID = opsPlanning.RomoocID;
                                        result.RomoocNo = opsPlanning.RomoocNo;
                                        result.AllowChangeRomooc = false;

                                        if (lstMasterPrevID.Count > 0)
                                        {
                                            var prev = model.FLM_AssetTimeSheet.Where(c => lstMasterPrevID.Contains(c.ReferID) && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster).Select(c => new { c.DateToActual }).OrderByDescending(c => c.DateToActual).FirstOrDefault();
                                            if (prev != null)
                                                result.DateMin = prev.DateToActual.AddHours(HourEmpty);
                                        }
                                    }

                                    if (result.RomoocID > 0 && result.RomoocID != DefaultRomooc)
                                    {
                                        result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, result.RomoocID, null, etd, eta);
                                        if (string.IsNullOrEmpty(result.OfferTimeError))
                                            result.OfferTimeError = CO_CheckRomoocAvailable(model, account, result.RomoocID, lstCOTOContainerID);
                                    }
                                    else
                                    {
                                        if (romoocid > 0)
                                            result.RomoocID = romoocid;
                                        var objRomooc = model.CAT_Romooc.Where(c => c.ID == result.RomoocID).Select(c => new { c.RegNo }).FirstOrDefault();
                                        if (objRomooc != null)
                                            result.RomoocNo = objRomooc.RegNo;
                                        result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, result.RomoocID, null, etd, eta);
                                        if (string.IsNullOrEmpty(result.OfferTimeError))
                                            result.OfferTimeError = CO_CheckRomoocAvailable(model, account, result.RomoocID, lstCOTOContainerID);
                                    }
                                }
                                else
                                {
                                    //order

                                }


                            }

                            //result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, vehicleid, romoocid, null, etd, eta);
                            //if (string.IsNullOrEmpty(result.OfferTimeError))
                            //    result.OfferTimeError = CO_CheckRomoocAvailable(model, account, romoocid, lstCOTOContainerID);

                            ////var lstCOTOContainer = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new { c.ID, c.ETD, c.ETA, c.SortOrder, c.OPSContainerID, c.COTOMasterID, c.ParentID, c.StatusOfCOContainerID, c.LocationFromID, c.LocationToID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).ToList();
                            ////var lstOPSContainer = lstCOTOContainer.Select(c => new { c.OPSContainerID, c.ServiceOfOrderID }).Distinct().ToList();

                            //double totalHour = (eta - etd).TotalHours;
                            //double divHour = totalHour / lstCOTOContainer.Count;
                            //var startDate = etd;

                            //if ((lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderExport) && lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderLocal)) ||
                            //    lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderImport) && lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderLocal))
                            //{
                            //    if ((lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).Count() > 0 && lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Count() == 0) ||
                            //        (lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty).Count() > 0 && lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).Count() == 0))
                            //    {
                            //        //state

                            //    }
                            //    else
                            //    {
                            //        //order

                            //    }

                            //    ////export local or import local
                            //    //var endLocal = startDate;
                            //    //foreach (var opscontainer in lstOPSContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal))
                            //    //{
                            //    //    var startLocal = startDate.AddHours(divHour);
                            //    //    if (lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderImport))
                            //    //        startLocal = startDate.AddHours(divHour * 2);
                            //    //    foreach (var item in lstCOTOContainer.Where(c => c.OPSContainerID == opscontainer.OPSContainerID).OrderBy(c => c.SortOrder))
                            //    //    {
                            //    //        result.ListCOContainer.Add(new DTOOPSCOTOContainer
                            //    //        {
                            //    //            ID = item.ID,
                            //    //            LocationFromID = item.LocationFromID,
                            //    //            LocationToID = item.LocationToID,
                            //    //            ETD = startLocal,
                            //    //            ETA = startLocal.AddHours(divHour),
                            //    //            SortOrder = item.SortOrder
                            //    //        });
                            //    //        startLocal = startLocal.AddHours(divHour);
                            //    //        if (startLocal.CompareTo(endLocal) > 0)
                            //    //            endLocal = startLocal;
                            //    //    }
                            //    //}
                            //    //foreach (var opscontainer in lstOPSContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport ||
                            //    //    c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport))
                            //    //{
                            //    //    var startMain = startDate;
                            //    //    foreach (var item in lstCOTOContainer.Where(c => c.OPSContainerID == opscontainer.OPSContainerID).OrderBy(c => c.SortOrder))
                            //    //    {
                            //    //        if (item.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || item.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty)
                            //    //        {
                            //    //            result.ListCOContainer.Add(new DTOOPSCOTOContainer
                            //    //            {
                            //    //                ID = item.ID,
                            //    //                LocationFromID = item.LocationFromID,
                            //    //                LocationToID = item.LocationToID,
                            //    //                ETD = startMain,
                            //    //                ETA = endLocal.AddHours(divHour),
                            //    //                SortOrder = item.SortOrder
                            //    //            });
                            //    //            startMain = endLocal.AddHours(divHour);
                            //    //        }
                            //    //        else
                            //    //        {
                            //    //            result.ListCOContainer.Add(new DTOOPSCOTOContainer
                            //    //            {
                            //    //                ID = item.ID,
                            //    //                LocationFromID = item.LocationFromID,
                            //    //                LocationToID = item.LocationToID,
                            //    //                ETD = startMain,
                            //    //                ETA = startMain.AddHours(divHour),
                            //    //                SortOrder = item.SortOrder
                            //    //            });
                            //    //            startMain = startMain.AddHours(divHour);
                            //    //        }
                            //    //    }
                            //    //}
                            //}
                            //else if (lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderImport) && lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderLocal))
                            //{
                            //    //import local   
                            //}
                            else if (lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderImport) && lstServiceID.Contains(-(int)SYSVarType.ServiceOfOrderExport))
                            {
                                //import export
                                double divHour = totalHour / lstCOTOContainer.Count;
                                var lstOPSContainer = lstCOTOContainer.Select(c => new { c.OPSContainerID, c.ServiceOfOrderID }).Distinct().ToList();
                                var emptyim = lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty).FirstOrDefault();
                                var ladenim = lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).FirstOrDefault();
                                var emptyex = lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).FirstOrDefault();
                                var ladenex = lstCOTOContainer.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).FirstOrDefault();
                                if (emptyim != null && ladenim != null && emptyex != null && ladenex != null)
                                {
                                    if (emptyim.LocationFromID == emptyex.LocationToID)
                                    {
                                        divHour = totalHour / (lstCOTOContainer.Count - 2);

                                        foreach (var opscontainer in lstOPSContainer)
                                        {
                                            var startMain = startDate;
                                            foreach (var item in lstCOTOContainer.Where(c => c.OPSContainerID == opscontainer.OPSContainerID).OrderBy(c => c.SortOrder))
                                            {
                                                if (item.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport && item.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty)
                                                {
                                                    result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                    {
                                                        ID = item.ID,
                                                        LocationFromID = item.LocationFromID,
                                                        LocationToID = ladenex.LocationToID,
                                                        ETD = startMain,
                                                        ETA = startMain.AddHours(divHour),
                                                        SortOrder = item.SortOrder
                                                    });
                                                }
                                                else if (item.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport && item.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty)
                                                {
                                                    result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                    {
                                                        ID = item.ID,
                                                        LocationFromID = ladenim.LocationFromID,
                                                        LocationToID = item.LocationToID,
                                                        ETD = startMain,
                                                        ETA = startMain.AddHours(divHour),
                                                        SortOrder = item.SortOrder
                                                    });
                                                }
                                                else
                                                {
                                                    result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                    {
                                                        ID = item.ID,
                                                        LocationFromID = item.LocationFromID,
                                                        LocationToID = item.LocationToID,
                                                        ETD = startMain,
                                                        ETA = startMain.AddHours(divHour),
                                                        SortOrder = item.SortOrder
                                                    });
                                                }
                                                startMain = startMain.AddHours(divHour);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        divHour = totalHour / (lstCOTOContainer.Count - 1);

                                        foreach (var opscontainer in lstOPSContainer)
                                        {
                                            var startMain = startDate;
                                            foreach (var item in lstCOTOContainer.Where(c => c.OPSContainerID == opscontainer.OPSContainerID).OrderBy(c => c.SortOrder))
                                            {
                                                if (item.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport && item.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty)
                                                {
                                                    result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                    {
                                                        ID = item.ID,
                                                        LocationFromID = item.LocationFromID,
                                                        LocationToID = emptyim.LocationToID,
                                                        ETD = startMain,
                                                        ETA = startMain.AddHours(divHour),
                                                        SortOrder = item.SortOrder
                                                    });
                                                }
                                                else if (item.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport && item.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty)
                                                {
                                                    result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                    {
                                                        ID = item.ID,
                                                        LocationFromID = emptyim.LocationFromID,
                                                        LocationToID = item.LocationToID,
                                                        ETD = startMain,
                                                        ETA = startMain.AddHours(divHour),
                                                        SortOrder = item.SortOrder
                                                    });
                                                }
                                                else
                                                {
                                                    result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                                    {
                                                        ID = item.ID,
                                                        LocationFromID = item.LocationFromID,
                                                        LocationToID = item.LocationToID,
                                                        ETD = startMain,
                                                        ETA = startMain.AddHours(divHour),
                                                        SortOrder = item.SortOrder
                                                    });
                                                }
                                                startMain = startMain.AddHours(divHour);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        if (result.DateMin != null)
                        {
                            if (result.DateMin.Value.CompareTo(etd) > 0)
                                result.OfferTimeError = "Thời gian ETD lố thời gian cho phép";
                        }
                        if (result.DateMax != null)
                        {
                            if (result.DateMax.Value.CompareTo(eta) < 0)
                                result.OfferTimeError = "Thời gian ETA lố thời gian cho phép";
                        }
                    }
                    if (result.HourETAOffer <= 0)
                        result.HourETAOffer = 0.5;
                    if (result.ListCOContainer.Count == 0)
                        result.OfferTimeError = "Không tìm được các chặng đã gửi";
                    if ((eta - etd).TotalHours < result.HourETAOffer)
                        result.OfferTimeError = "Thời gian không đủ thực hiện";
                    if (result.ListCOContainer.Count == 0)
                        result.OfferTimeError = "Không tìm thấy danh sách chặng";

                    if (string.IsNullOrEmpty(result.OfferTimeError))
                        result = CO_CheckConstraint(model, account, result, result.ListCOContainer.Select(c => c.ID).ToList());

                    return result;
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "list fail");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list fail");
        }

        public static DTOOPSCOTOMaster OPSCO_CreateItem(DataEntities model, AccountItem account, DTOOPSCOTOMaster item, HelperTOMaster_COParam param = default(HelperTOMaster_COParam))
        {
            if (item != null)
            {
                //check time 
                string strCheck = CO_CheckAssetTimeSheet(model, account, item.VehicleID, item.RomoocID, null, item.ETD, item.ETA);
                if (string.IsNullOrEmpty(strCheck))
                {
                    strCheck = CO_CheckRomoocAvailable(model, account, item.RomoocID, item.ListCOContainer.Select(c => c.ID).ToList());
                    if (string.IsNullOrEmpty(strCheck))
                    {
                        //Check param and setting
                        var setting = OPSCO_GetSetting(model, account);
                        if (param == null)
                        {
                            param = new HelperTOMaster_COParam();
                            param.LocationFromID = setting.LocationFromID;
                            param.LocationToID = setting.LocationToID;
                            param.LocationGetRomoocID = setting.LocationRomoocReturnID;
                        }
                        else
                        {
                            param.LocationFromID = param.LocationFromID > 0 ? param.LocationFromID.Value : setting.LocationFromID;
                            param.LocationToID = param.LocationToID > 0 ? param.LocationToID.Value : setting.LocationToID;
                            param.LocationGetRomoocID = param.LocationGetRomoocID > 0 ? param.LocationGetRomoocID.Value : setting.LocationRomoocReturnID;
                        }

                        var lstCOContainerID = item.ListCOContainer.Select(c => c.ID).Distinct().ToList();
                        var lstOPSContainerID = model.OPS_COTOContainer.Where(c => lstCOContainerID.Contains(c.ID)).Select(c => c.OPSContainerID).Distinct().ToList();
                        OPSCO_TOContainerResetSort(model, account, lstOPSContainerID);

                        var lstContainerID = item.ListCOContainer.Select(c => c.ID).Distinct().ToList();
                        var first = model.OPS_COTOContainer.Where(c => lstContainerID.Contains(c.ID)).Select(c => new { c.OPS_Container.ORD_Container.ORD_Order.TransportModeID }).FirstOrDefault();
                        if (first != null)
                        {
                            //item.TransportModeID = first.TransportModeID;
                            //item.ID = OPSCO_CreateItem_Create(model, account, item);

                            //HelperTimeSheet.Create(model, account, item.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, null);
                            //OPSCO_CreateItemResetStatus(model, account, item.ListCOContainer.Select(c => c.ID).ToList());
                            ////var SDATA = model.OPS_COTOContainer.Where(c => c.COTOMasterID == item.ID).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
                            ////HelperStatus.ORDOrder_Status(model, account, SDATA); 

                            item.TransportModeID = first.TransportModeID;
                            if (lstOPSContainerID.Count == 1)
                            {
                                if (item.ListCOContainer.Count > 1)
                                {
                                    var tocontainer1 = item.ListCOContainer[0];
                                    var tocontainer2 = item.ListCOContainer[1];
                                    int id = item.ID;

                                    item.ETD = tocontainer1.ETD.Value;
                                    item.ETA = tocontainer1.ETA.Value;
                                    item.ListCOContainer = new List<DTOOPSCOTOContainer>() { tocontainer1 };
                                    id = OPSCO_CreateItem_Create(model, account, item);
                                    HelperTimeSheet.Create(model, account, id, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, null);

                                    item.ETD = tocontainer2.ETD.Value;
                                    item.ETA = tocontainer2.ETA.Value;
                                    item.ListCOContainer = new List<DTOOPSCOTOContainer>() { tocontainer2 };
                                    id = OPSCO_CreateItem_Create(model, account, item);
                                    HelperTimeSheet.Create(model, account, id, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, null);

                                    item.ID = id;
                                    OPSCO_ResetOPSContainer(model, account, lstCOContainerID);
                                    var SDATA = model.OPS_COTOContainer.Where(c => c.COTOMasterID == item.ID).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
                                    HelperStatus.ORDOrder_Status(model, account, SDATA);
                                }
                                else if (item.ListCOContainer.Count == 1)
                                {
                                    item.ID = OPSCO_CreateItem_Create(model, account, item);
                                    HelperTimeSheet.Create(model, account, item.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, null);

                                    OPSCO_ResetOPSContainer(model, account, lstCOContainerID);
                                    var SDATA = model.OPS_COTOContainer.Where(c => c.COTOMasterID == item.ID).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
                                    HelperStatus.ORDOrder_Status(model, account, SDATA);
                                }
                            }
                            else
                            {
                                item.ID = OPSCO_CreateItem_Create(model, account, item);
                                HelperTimeSheet.Create(model, account, item.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, null);

                                OPSCO_ResetOPSContainer(model, account, lstCOContainerID);
                                var SDATA = model.OPS_COTOContainer.Where(c => c.COTOMasterID == item.ID).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
                                HelperStatus.ORDOrder_Status(model, account, SDATA);
                            }
                        }
                    }
                    else
                        throw FaultHelper.BusinessFault(null, null, strCheck);
                }
                else
                    throw FaultHelper.BusinessFault(null, null, strCheck);
            }
            else
                throw FaultHelper.BusinessFault(null, null, "item null");

            return item;
        }




        public static HelperTOMaster_Error OPSCO_CreateItemCheck(DataEntities model, AccountItem account, DTOOPSCOTOMaster item, HelperTOMaster_COParam param = default(HelperTOMaster_COParam))
        {
            return HelperTOMaster_Error.None;
        }




        public static DTOOPSCOTOMaster OPSCO_ChangeScheduleTimeOfferTime(DataEntities model, AccountItem account, int masterid, DateTime etd, DateTime eta, DTOOPSCOTOContainer tocontainerchange = default(DTOOPSCOTOContainer))
        {
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };
            var result = new DTOOPSCOTOMaster();
            result.DateMin = etd;
            result.DateMax = eta;
            result.ETD = etd;
            result.ETA = eta;
            result.HourETAOffer = HourMatrixDefault;
            result.RomoocID = DefaultRomooc;
            result.AllowChangeRomooc = true;
            result.AllowAddLocal = true;
            result.ListCOContainer = new List<DTOOPSCOTOContainer>();
            result.OfferTimeError = string.Empty;
            result.OfferTimeWarning = string.Empty;

            if (etd.CompareTo(eta) < 0 && etd.CompareTo(DateTime.MinValue) > 0)
            {
                result.AllowAddLocal = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty)).Count() > 0;
                if (tocontainerchange != null)
                {
                    var objTOChange = model.OPS_COTOContainer.Where(c => c.ID == tocontainerchange.ID).Select(c => new { c.ETD, c.ETA }).FirstOrDefault();
                    if (objTOChange != null && tocontainerchange.ETD != null && tocontainerchange.ETA != null && tocontainerchange.ETA.Value.CompareTo(tocontainerchange.ETD.Value) > 0)
                    {
                        var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid).Select(c => new { c.ETD, c.ETA, c.VehicleID, c.RomoocID, c.ID }).FirstOrDefault();
                        //var tocontainer = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == tocontainerchange.ID);
                        if (objMaster != null)
                        {
                            etd = objMaster.ETD;
                            if (etd.CompareTo(tocontainerchange.ETD.Value) > 0)
                                etd = tocontainerchange.ETD.Value;
                            eta = objMaster.ETA;
                            if (eta.CompareTo(tocontainerchange.ETA.Value) < 0)
                                eta = tocontainerchange.ETA.Value;

                            var lstTOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false && !statusload.Contains(c.StatusOfCOContainerID)).Select(c => new
                            {
                                c.ID,
                                c.OPS_COTOMaster.RomoocID,
                                RomoocNo = c.OPS_COTOMaster.RomoocID > 0 ? c.OPS_COTOMaster.CAT_Romooc.RegNo : "",
                                c.OPSContainerID,
                                c.SortOrder,
                                c.COTOSort,
                                c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID,
                                c.StatusOfCOContainerID,
                                c.OPS_Container.OPSContainerTypeID,
                                c.LocationFromID,
                                c.LocationToID,
                                c.ETD,
                                c.ETA,
                                c.OPS_Container.ORD_Container.ETDOld,
                                c.OPS_Container.ORD_Container.ETAOld,
                                c.OPS_Container.ORD_Container.CutOffTime,
                                c.OPS_Container.ORD_Container.ORD_Order.RequestDate
                            }).ToList();
                            result.ETD = tocontainerchange.ETD.Value;
                            result.ETA = tocontainerchange.ETD.Value;
                            foreach (var item in lstTOContainer)
                            {
                                var dtfrom = item.ETD;
                                var dtto = item.ETA;
                                if (item.ID == tocontainerchange.ID)
                                {
                                    dtfrom = tocontainerchange.ETD;
                                    dtto = tocontainerchange.ETA;
                                }
                                result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                {
                                    ID = item.ID,
                                    ETD = dtfrom,
                                    ETA = dtto,
                                    LocationFromID = item.LocationFromID,
                                    LocationToID = item.LocationToID,
                                    SortOrder = item.SortOrder
                                });
                                if (result.ETD.CompareTo(dtfrom.Value) > 0)
                                    result.ETD = dtfrom.Value;
                                if (result.ETA.CompareTo(dtto.Value) < 0)
                                    result.ETA = dtto.Value;
                            }

                            var etdold = objTOChange.ETD.Value.AddHours(-0.2);
                            var etaold = objTOChange.ETA.Value.AddHours(0.2);
                            var firstLoad = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && statusload.Contains(c.StatusOfCOContainerID) && c.DateToCome >= etdold && c.DateFromCome < etdold).Select(c => new { c.ID, c.ETD, c.ETA, c.SortOrder }).FirstOrDefault();
                            if (firstLoad != null)
                            {
                                result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                {
                                    ID = firstLoad.ID,
                                    ETD = firstLoad.ETD,
                                    ETA = tocontainerchange.ETD.Value.AddHours(-HourEmpty),
                                    SortOrder = firstLoad.SortOrder
                                });
                            }
                            var lastLoad = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && statusload.Contains(c.StatusOfCOContainerID) && c.DateFromCome <= etaold && c.DateToCome > etaold).Select(c => new { c.ID, c.ETD, c.ETA, c.SortOrder }).FirstOrDefault();
                            if (lastLoad != null)
                            {
                                result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                {
                                    ID = lastLoad.ID,
                                    ETD = tocontainerchange.ETA.Value.AddHours(HourEmpty),
                                    ETA = lastLoad.ETA,
                                    SortOrder = lastLoad.SortOrder
                                });
                            }

                            result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, objMaster.VehicleID, objMaster.RomoocID, objMaster.ID, result.ETD, result.ETA);
                        }
                    }
                }
                else
                {
                    var lstTOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false && !statusload.Contains(c.StatusOfCOContainerID)).Select(c => new
                    {
                        c.ID,
                        c.OPS_COTOMaster.RomoocID,
                        RomoocNo = c.OPS_COTOMaster.RomoocID > 0 ? c.OPS_COTOMaster.CAT_Romooc.RegNo : "",
                        c.OPSContainerID,
                        c.SortOrder,
                        c.COTOSort,
                        c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID,
                        c.StatusOfCOContainerID,
                        c.OPS_Container.OPSContainerTypeID,
                        c.LocationFromID,
                        c.LocationToID,
                        c.ETD,
                        c.ETA,
                        c.OPS_Container.ORD_Container.ETDOld,
                        c.OPS_Container.ORD_Container.ETAOld,
                        c.OPS_Container.ORD_Container.CutOffTime,
                        c.OPS_Container.ORD_Container.ORD_Order.RequestDate
                    }).ToList();
                    var lstOPSContainerID = lstTOContainer.Select(c => c.OPSContainerID).Distinct().ToList();

                    var first = lstTOContainer.FirstOrDefault();
                    if (first != null)
                    {
                        result.RomoocID = first.RomoocID;
                        result.RomoocNo = first.RomoocNo;
                    }

                    bool isorder = true;
                    bool isfirst = false;
                    var totalHours = (eta - etd).TotalHours;
                    double houretaoffer = 0;

                    foreach (var opscontainerid in lstOPSContainerID)
                    {
                        var query = lstTOContainer.Where(c => c.OPSContainerID == opscontainerid).OrderBy(c => c.SortOrder);
                        if (query.Count() > 0)
                        {
                            var divHour = totalHours / query.Count();
                            var dtFrom = etd;
                            var dtTo = dtFrom.AddHours(divHour);
                            foreach (var item in query)
                            {
                                result.ListCOContainer.Add(new DTOOPSCOTOContainer
                                {
                                    ID = item.ID,
                                    ETD = dtFrom,
                                    ETA = dtTo,
                                    SortOrder = item.SortOrder,
                                    COTOSort = item.COTOSort
                                });
                                dtFrom = dtTo.AddHours(HourEmpty);
                                dtTo = dtFrom.AddHours(divHour);

                                var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == item.LocationFromID && c.LocationToID == item.LocationToID).Select(c => new { c.Hour, c.KM }).FirstOrDefault();
                                if (matrix != null && matrix.Hour > 0)
                                    houretaoffer += matrix.Hour;
                                else
                                    houretaoffer += HourMatrixDefault;
                            }
                        }
                        if (lstTOContainer.Where(c => c.OPSContainerTypeID == -(int)SYSVarType.OPSContainerTypePlanning).Count() > 0)
                        {
                            isorder = false;
                            isfirst = true;
                        }
                        else
                        {
                            var lstRun = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.COTOMasterID > 0).Select(c => new { COTOMasterID = c.COTOMasterID.Value, c.OPS_COTOMaster.ETD, c.OPS_COTOMaster.ETA }).Distinct().ToList();
                            if (lstRun.Count > 1)
                            {
                                //has 2 master
                                isorder = false;
                                if (lstTOContainer.Where(c => c.SortOrder < 10 && (c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)).Count() > 0)
                                {
                                    isfirst = true;
                                    result.DateMax = null;
                                    var next = lstRun.Where(c => c.COTOMasterID != masterid).OrderBy(c => c.ETD).FirstOrDefault();
                                    if (next != null && (result.DateMax == null || result.DateMax.Value.CompareTo(next.ETD) > 0))
                                        result.DateMax = next.ETD.AddHours(-HourEmpty);
                                }
                                else
                                {
                                    isfirst = false;
                                    var prev = lstRun.Where(c => c.COTOMasterID != masterid).OrderBy(c => c.ETD).FirstOrDefault();
                                    if (prev != null && (result.DateMin == null || result.DateMin.Value.CompareTo(prev.ETA) < 0))
                                        result.DateMin = prev.ETA.AddHours(HourEmpty);
                                }
                            }
                            else if (lstRun.Count == 1)
                            {
                                isfirst = true;
                                result.DateMax = null;
                            }
                        }
                    }
                    result.HourETAOffer = houretaoffer;

                    var tofirst = lstTOContainer.Where(c => c.ETDOld != null).OrderBy(c => c.ETDOld).FirstOrDefault();
                    var tolast = lstTOContainer.Where(c => c.ETAOld != null).OrderByDescending(c => c.ETAOld).FirstOrDefault();
                    if (isorder)
                    {
                        if (tofirst != null)
                            result.DateMin = tofirst.RequestDate;
                        if (tolast != null)
                            result.DateMax = tolast.ETAOld.Value;
                    }
                    else if (isfirst)
                    {
                        if (tofirst != null)
                            result.DateMin = tofirst.RequestDate;
                        if (result.DateMax == null)
                        {
                            if (tolast != null)
                                result.DateMax = tolast.ETAOld.Value;
                            else
                                result.DateMax = eta;
                        }
                    }
                    else
                    {
                        if (tolast != null)
                            result.DateMax = tolast.ETAOld.Value;
                    }
                    if (isorder || !isfirst)
                    {
                        var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid).Select(c => new { c.VehicleID, c.RomoocID }).FirstOrDefault();
                        if (objMaster != null)
                        {
                            DateTime? etanext = null;
                            var assetVehicle = model.FLM_Asset.Where(c => c.VehicleID == objMaster.VehicleID).Select(c => new { c.ID }).FirstOrDefault();
                            if (assetVehicle != null)
                            {
                                var timeVehicle = model.FLM_AssetTimeSheet.Where(c => c.AssetID == assetVehicle.ID && ((c.ReferID != masterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster) || c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster) &&
                                    c.DateFromActual > result.DateMax).Select(c => new { c.DateFromActual }).OrderBy(c => c.DateFromActual).FirstOrDefault();
                                if (timeVehicle != null)
                                    etanext = timeVehicle.DateFromActual;
                            }
                            var assetRomooc = model.FLM_Asset.Where(c => c.RomoocID == objMaster.RomoocID).Select(c => new { c.ID }).FirstOrDefault();
                            if (assetRomooc != null)
                            {
                                var timeRomooc = model.FLM_AssetTimeSheet.Where(c => c.AssetID == assetRomooc.ID && ((c.ReferID != masterid && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster) || c.StatusOfAssetTimeSheetID != -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster) &&
                                    c.DateFromActual > result.DateMax).Select(c => new { c.DateFromActual }).OrderBy(c => c.DateFromActual).FirstOrDefault();
                                if (timeRomooc != null && (etanext == null || etanext.Value.CompareTo(timeRomooc.DateFromActual) < 0))
                                    etanext = timeRomooc.DateFromActual;
                            }
                            if (etanext != null)
                                result.DateMax = etanext;
                            else
                            {
                                var tocut = lstTOContainer.Where(c => c.CutOffTime != null).OrderBy(c => c.CutOffTime).FirstOrDefault();
                                if (tocut != null && (result.DateMax == null || (result.DateMax != null && tocut.CutOffTime.Value.CompareTo(result.DateMax.Value) > 0)))
                                    result.DateMax = tocut.CutOffTime.Value.AddHours(24);
                                else if (result.DateMax != null)
                                    result.DateMax = result.DateMax.Value.AddHours(24);
                            }
                        }
                    }

                    if (!isorder && !isfirst)
                    {
                        result.AllowChangeRomooc = false;
                    }

                    var firstTO = result.ListCOContainer.OrderBy(c => c.COTOSort).FirstOrDefault();
                    if (firstTO != null && firstTO.ETD.Value.CompareTo(etd) > 0)
                    {
                        foreach (var to in result.ListCOContainer.Where(c => c.COTOSort == firstTO.COTOSort))
                        {
                            to.ETD = etd;
                        }
                    }
                    var lastTO = result.ListCOContainer.OrderByDescending(c => c.COTOSort).FirstOrDefault();
                    if (lastTO != null && lastTO.ETA.Value.CompareTo(eta) < 0)
                    {
                        foreach (var to in result.ListCOContainer.Where(c => c.COTOSort == lastTO.COTOSort))
                        {
                            to.ETA = eta;
                        }
                    }

                    //if (result.DateMin != null && result.DateMin.Value.CompareTo(etd) > 0)
                    //    result.OfferTimeError = "Thời gian lấy sớm hơn kế hoạch";
                    //if (result.DateMax != null && result.DateMax.Value.CompareTo(eta) < 0)
                    //    result.OfferTimeError = "Thời gian đến trễ đã lố kế hoạch";
                }
            }
            if (result.ListCOContainer.Count == 0)
                result.OfferTimeError = "Không tìm được các chặng đã gửi";
            return result;
        }

        public static HelperTOMaster_Error OPSCO_ChangeScheduleTime(DataEntities model, AccountItem account, int masterid, DateTime etd, DateTime eta, List<DTOOPSCOTOContainer> lstTOContainer)
        {
            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
            if (objMaster != null && (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID))
            {
                if (etd.CompareTo(eta) < 0 && etd.CompareTo(DateTime.MinValue) > 0)
                {
                    objMaster.ETD = etd;
                    objMaster.ETA = eta;
                    objMaster.ATD = etd;
                    objMaster.ATA = eta;
                    objMaster.ModifiedBy = account.UserName;
                    objMaster.ModifiedDate = DateTime.Now;

                    var lstTOLocation = model.OPS_COTOLocation.Where(c => c.COTOMasterID == objMaster.ID).ToList();
                    var lstCOTO = model.OPS_COTO.Where(c => c.COTOMasterID == objMaster.ID).ToList();
                    var lstOPSContainerID = new List<int>();

                    //foreach (var coto in lstCOTO)
                    //{
                    //    var objTO = lstTOContainer.FirstOrDefault(c => c.COTOSort == coto.SortOrder);
                    //    var locETD = lstTOLocation.FirstOrDefault(c => c.SortOrder == coto.SortOrder);
                    //    var locETA = lstTOLocation.FirstOrDefault(c => c.SortOrder == coto.SortOrder + 1);
                    //    if (objTO != null)
                    //    {
                    //        coto.ETD = objTO.ETD;
                    //        coto.ETA = objTO.ETA;

                    //        if (locETD != null)
                    //        {
                    //            locETD.DateComeEstimate = coto.ETD;
                    //            locETD.DateLeaveEstimate = coto.ETD;
                    //        }
                    //        if (locETA != null)
                    //        {
                    //            locETA.DateComeEstimate = coto.ETA;
                    //            locETA.DateLeaveEstimate = coto.ETA;
                    //        }
                    //    }
                    //}

                    var lstCON = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objMaster.ID).ToList();
                    foreach (var tocontainer in lstTOContainer)
                    {
                        var objCON = lstCON.FirstOrDefault(c => c.ID == tocontainer.ID);
                        if (objCON != null)
                        {
                            //objCON.ETD = tocontainer.ETD;
                            //objCON.ETA = tocontainer.ETA;
                            //objCON.DateFromCome = tocontainer.ETD;
                            //objCON.DateToCome = tocontainer.ETA;

                            objCON.ETD = etd;
                            objCON.ETA = eta;
                            objCON.DateFromCome = etd;
                            objCON.DateToCome = eta;
                        }
                    }
                    lstOPSContainerID = lstCON.Select(c => c.OPSContainerID).Distinct().ToList();
                    model.SaveChanges();

                    HelperTimeSheet.ChangePlan(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, etd, eta);
                    CO_ResetRomoocByOPSContainer(model, account, lstOPSContainerID);
                    OPSCO_ResetOPSContainer(model, account, lstTOContainer.Select(c => c.ID).ToList());
                    return HelperTOMaster_Error.None;
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "etd, eta fail");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "master null");
        }





        public static DTOOPSCOTOMaster OPSCO_ChangeScheduleVehicleOffer(DataEntities model, AccountItem account, int masterid, int? vehicleid, int? vendorid)
        {
            var result = new DTOOPSCOTOMaster();
            result.OfferTimeError = "";
            result.OfferTimeWarning = "";

            var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid).Select(c => new { c.VehicleID, c.RomoocID, c.VendorOfVehicleID, c.ETD, c.ETA, c.ATD, c.ATA }).FirstOrDefault();
            if (objMaster != null)
            {
                if (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID)
                {
                    var etd = objMaster.ETD;
                    var eta = objMaster.ETA;

                    result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, objMaster.VehicleID, null, masterid, etd, eta);
                }
            }

            if (string.IsNullOrEmpty(result.OfferTimeError))
                result = CO_CheckConstraint(model, account, result, model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => c.ID).ToList());

            return result;
            //var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
            //if (objMaster != null)
            //{
            //    if (vendorid > 0 || vehicleid > 0)
            //    {
            //        //change vendor 
            //        if (vendorid > 0)
            //        {
            //            objMaster.VendorOfVehicleID = vendorid.Value;
            //            objMaster.VendorOfRomoocID = vendorid.Value;
            //        }
            //        //change vehicle 
            //        if (vehicleid > 0)
            //        {
            //            objMaster.VehicleID = vehicleid;
            //        }

            //        HelperTimeSheet.Remove(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
            //        if (objMaster.VehicleID > 2 && (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID))
            //            HelperTimeSheet.Create(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
            //        //else
            //        //    HelperTimeSheet.Remove(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
            //        return HelperTOMaster_Error.None;
            //    }
            //    else
            //        throw FaultHelper.BusinessFault(null, null, "no change");
            //}
            //else
            //    throw FaultHelper.BusinessFault(null, null, "master null");

            //return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error OPSCO_ChangeScheduleVehicle(DataEntities model, AccountItem account, int masterid, int? vehicleid, int? vendorid)
        {
            var objCheck = OPSCO_ChangeScheduleVehicleOffer(model, account, masterid, vehicleid, vendorid);
            if (objCheck != null)
            {
                if (string.IsNullOrEmpty(objCheck.OfferTimeError))
                {
                    var lstCOTOContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => c.ID).ToList();
                    var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
                    if (objMaster != null)
                    {
                        if (vendorid > 0 || vehicleid > 0)
                        {
                            //change vendor 
                            if (vendorid > 0)
                            {
                                objMaster.VendorOfVehicleID = vendorid.Value;
                                objMaster.VendorOfRomoocID = vendorid.Value;
                            }
                            //change vehicle 
                            if (vehicleid > 0)
                            {
                                objMaster.VehicleID = vehicleid;
                            }

                            HelperTimeSheet.Remove(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                            if (objMaster.VehicleID > 2 && (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID))
                                HelperTimeSheet.Create(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                            //else
                            //    HelperTimeSheet.Remove(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                            OPSCO_ResetOPSContainer(model, account, lstCOTOContainerID);

                            return HelperTOMaster_Error.None;
                        }
                        else
                            throw FaultHelper.BusinessFault(null, null, "no change");
                    }
                    else
                        throw FaultHelper.BusinessFault(null, null, "master null");
                }
                else
                    throw FaultHelper.BusinessFault(null, null, objCheck.OfferTimeError);
            }
            else
                return HelperTOMaster_Error.None;
        }

        public static DTOOPSCOTOMaster OPSCO_ChangeScheduleRomoocOffer(DataEntities model, AccountItem account, int masterid, int? romoocid)
        {
            var result = new DTOOPSCOTOMaster();
            result.OfferTimeError = "";
            result.OfferTimeWarning = "";

            var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid).Select(c => new { c.RomoocID, c.VendorOfVehicleID, c.ETD, c.ETA, c.ATD, c.ATA }).FirstOrDefault();
            if (objMaster != null)
            {
                if (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID)
                {
                    var isorder = true;
                    var isfirst = false;
                    var etd = objMaster.ETD;
                    var eta = objMaster.ETA;

                    var lstOPSContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => c.OPSContainerID).Distinct().ToList();
                    if (lstOPSContainerID.Count > 0)
                    {
                        foreach (var opscontainerid in lstOPSContainerID)
                        {
                            var lstRun = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.COTOMasterID > 0).Select(c => new { c.OPS_COTOMaster.ID, c.OPS_COTOMaster.ETD, c.OPS_COTOMaster.ETA }).Distinct().ToList();
                            if (lstRun.Count == 1)
                            {
                                isorder = true;
                                isfirst = true;
                            }
                            else if (lstRun.Count > 1)
                            {
                                isorder = false;
                                var first = lstRun.OrderBy(c => c.ETD).FirstOrDefault();
                                if (first != null && first.ID == masterid)
                                    isfirst = true;
                                else
                                    isfirst = false;
                            }
                        }
                        if (isorder == false && isfirst == false)
                            result.OfferTimeError = "Chỉ cho điều chỉnh mooc ở chặng đầu";
                        else if (objMaster.RomoocID != romoocid && romoocid > 1)
                        {
                            if (model.OPS_Container.Where(c => c.RomoocID == romoocid && c.OPSContainerTypeID == -(int)SYSVarType.OPSContainerTypePlanning).Count() > 0)
                                result.OfferTimeError = "Đang chạy chặng khác";
                            else
                            {
                                var assetRomooc = model.FLM_Asset.Where(c => c.RomoocID == romoocid).Select(c => new { c.ID }).FirstOrDefault();
                                if (assetRomooc != null)
                                {
                                    if (model.FLM_AssetTimeSheet.Where(c => c.AssetID == assetRomooc.ID && c.DateFromActual <= objMaster.ETD && c.DateToActual >= objMaster.ETD).Count() > 0)
                                        result.OfferTimeError = "Romooc đang bận không chạy được";
                                }
                                else
                                    throw FaultHelper.BusinessFault(null, null, "no found romooc");
                            }
                        }

                        if (string.IsNullOrEmpty(result.OfferTimeError))
                            result.OfferTimeError = CO_CheckAssetTimeSheet(model, account, null, romoocid, masterid, etd, eta);
                    }
                    else
                        throw FaultHelper.BusinessFault(null, null, "no found data");
                }
            }

            if (string.IsNullOrEmpty(result.OfferTimeError))
                result = CO_CheckConstraint(model, account, result, model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => c.ID).ToList());

            return result;
        }

        public static HelperTOMaster_Error OPSCO_ChangeScheduleRomooc(DataEntities model, AccountItem account, int masterid, int? romoocid)
        {
            var objCheck = OPSCO_ChangeScheduleRomoocOffer(model, account, masterid, romoocid);
            if (objCheck != null)
            {
                if (string.IsNullOrEmpty(objCheck.OfferTimeError))
                {
                    var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
                    if (objMaster != null)
                    {
                        if (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID)
                        {
                            var lstOPSContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objMaster.ID).Select(c => c.OPSContainerID).Distinct().ToList();

                            if (romoocid > 0)
                            {
                                objMaster.RomoocID = romoocid;
                                objMaster.ModifiedBy = account.UserName;
                                objMaster.ModifiedDate = DateTime.Now;

                                var lstCOTOContainerID = new List<int>();

                                foreach (var opscontainerid in lstOPSContainerID)
                                {
                                    var objCON = model.OPS_Container.FirstOrDefault(c => c.ID == opscontainerid);
                                    if (objCON != null)
                                    {
                                        objCON.RomoocID = romoocid;
                                        objCON.ModifiedBy = account.UserName;
                                        objCON.ModifiedDate = DateTime.Now;
                                    }

                                    var lstOtherID = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.COTOMasterID != objMaster.ID && c.COTOMasterID > 0).Select(c => c.OPS_COTOMaster.ID).Distinct().ToList();
                                    foreach (var otherid in lstOtherID)
                                    {
                                        var objOther = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == otherid);
                                        if (objOther != null)
                                        {
                                            objOther.RomoocID = romoocid;
                                            objOther.ModifiedBy = account.UserName;
                                            objOther.ModifiedDate = DateTime.Now;
                                        }
                                    }

                                    lstCOTOContainerID.AddRange(model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.COTOMasterID > 0).Select(c => c.ID).ToList());
                                }
                                model.SaveChanges();

                                CO_ResetRomoocByOPSContainer(model, account, lstOPSContainerID);
                                OPSCO_ResetOPSContainer(model, account, lstCOTOContainerID);

                                //HelperTimeSheet.Remove(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                                //HelperTimeSheet.Create(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                                //CO_ResetRomoocByOPSContainer(model, account, lstOPSContainerID);
                            }
                        }
                        else
                        {
                            if (romoocid != null)
                                objMaster.RomoocID = romoocid;
                            else
                                objMaster.RomoocID = DefaultRomooc;
                            objMaster.ModifiedBy = account.UserName;
                            objMaster.ModifiedDate = DateTime.Now;
                            model.SaveChanges();
                        }

                        //HelperTimeSheet.Remove(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                        //if (objMaster.VehicleID > 2 && (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID))
                        //    HelperTimeSheet.Create(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);

                        return HelperTOMaster_Error.None;
                    }
                    else
                        throw FaultHelper.BusinessFault(null, null, "master null");
                }
                else
                    throw FaultHelper.BusinessFault(null, null, objCheck.OfferTimeError);
            }
            else
                return HelperTOMaster_Error.None;
        }


        public static HelperTOMaster_Error OPSCO_ChangeScheduleLocationRomooc(DataEntities model, AccountItem account, int masterid, int? locationromoocid)
        {
            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
            if (objMaster != null)
            {
                if (locationromoocid > 0)
                    objMaster.LocationRomoocID = locationromoocid;
                else
                    objMaster.LocationRomoocID = null;
                objMaster.ModifiedBy = account.UserName;
                objMaster.ModifiedDate = DateTime.Now;
                model.SaveChanges();
            }
            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error OPSCO_ChangeScheduleLocationStand(DataEntities model, AccountItem account, int masterid, int? locationstandid)
        {
            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
            if (objMaster != null)
            {
                if (locationstandid > 0)
                    objMaster.LocationStandID = locationstandid;
                objMaster.ModifiedBy = account.UserName;
                objMaster.ModifiedDate = DateTime.Now;
                model.SaveChanges();
            }
            return HelperTOMaster_Error.None;
        }



        public static DTOOPSCOTOMaster OPSCO_MasterAddTOContainerOffer(DataEntities model, AccountItem account, int masterid, List<int> lstCOTOContainerID)
        {
            var result = new DTOOPSCOTOMaster();
            result.HourETAOffer = HourMatrixDefault;
            result.RomoocID = DefaultRomooc;
            result.AllowChangeRomooc = true;
            result.ListCOContainer = new List<DTOOPSCOTOContainer>();
            result.OfferTimeError = string.Empty;
            result.OfferTimeWarning = string.Empty;

            if (lstCOTOContainerID != null && lstCOTOContainerID.Count > 0)
            {
                lstCOTOContainerID = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID) && c.COTOMasterID == null).Select(c => c.ID).ToList();
                if (lstCOTOContainerID.Count > 0)
                {
                    var lstServiceCurrent = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && (c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)).Select(c => new
                    {
                        c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID,
                        c.OPS_Container.RomoocID,
                        c.OPSContainerID,
                        c.OPS_COTOMaster.StatusOfCOTOMasterID
                    }).Distinct().ToList();
                    var lstServiceAdd = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new
                    {
                        c.ID,
                        c.ETD,
                        c.ETA,
                        c.OPS_Container.ORD_Container.ETDOld,
                        c.OPS_Container.ORD_Container.ETAOld,
                        c.OPS_Container.ORD_Container.ORD_Order.RequestDate,
                        c.SortOrder,
                        c.OPSContainerID,
                        c.COTOMasterID,
                        c.ParentID,
                        c.StatusOfCOContainerID,
                        c.LocationFromID,
                        c.LocationToID,
                        c.OPS_Container.RomoocID,
                        RomoocNo = c.OPS_Container.RomoocID > 0 ? c.OPS_Container.CAT_Romooc.RegNo : "",
                        c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID,
                        c.OPS_Container.OPSContainerTypeID
                    }).Distinct().ToList();
                    if (lstServiceCurrent.Count == 1 && lstServiceAdd.Count == 1)
                    {
                        var serviceAdd = lstServiceAdd[0];
                        var serviceCurrent = lstServiceCurrent[0];

                        result.RomoocID = serviceCurrent.RomoocID;

                        List<int> statusempty = new List<int>()
                        {
                            -(int)SYSVarType.StatusOfCOContainerEXEmpty,
                            -(int)SYSVarType.StatusOfCOContainerIMEmpty
                        };
                        List<int> statusladen = new List<int>()
                        {
                            -(int)SYSVarType.StatusOfCOContainerEXLaden,
                            -(int)SYSVarType.StatusOfCOContainerIMLaden
                        };

                        var tocontainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && statusempty.Contains(c.StatusOfCOContainerID)).Select(c => new { c.ID, c.ETD, c.ETA, c.StatusOfCOContainerID }).FirstOrDefault();
                        if (tocontainer == null)
                            tocontainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && statusladen.Contains(c.StatusOfCOContainerID)).Select(c => new { c.ID, c.ETD, c.ETA, c.StatusOfCOContainerID }).FirstOrDefault();
                        if (tocontainer == null)
                            tocontainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.ParentID == null).Select(c => new { c.ID, c.ETD, c.ETA, c.StatusOfCOContainerID }).FirstOrDefault();

                        if (tocontainer != null)
                        {
                            if (serviceAdd.ServiceOfOrderID == serviceCurrent.ServiceOfOrderID)
                            {
                                //2 con
                                var tocurrent = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.OPSContainerID == serviceCurrent.OPSContainerID).Select(c => new DTOOPSCOTOContainer
                                {
                                    ID = c.ID,
                                    StatusOfCOContainerID = c.StatusOfCOContainerID,
                                    LocationFromID = c.LocationFromID,
                                    LocationToID = c.LocationToID,
                                    ETD = c.ETD,
                                    ETA = c.ETA,
                                    SortOrder = c.SortOrder
                                }).FirstOrDefault();
                                var toadd = model.OPS_COTOContainer.Where(c => c.OPSContainerID == serviceAdd.OPSContainerID && c.ParentID == null).Select(c => new DTOOPSCOTOContainer
                                {
                                    ID = c.ID,
                                    StatusOfCOContainerID = c.StatusOfCOContainerID,
                                    LocationFromID = c.LocationFromID,
                                    LocationToID = c.LocationToID,
                                    ETD = c.ETD,
                                    ETA = c.ETA,
                                    SortOrder = c.SortOrder
                                }).FirstOrDefault();
                                if (tocurrent != null && toadd != null)
                                {
                                    var etd = tocurrent.ETD.Value;
                                    var eta = tocurrent.ETA.Value;

                                    if (tocurrent.LocationFromID == toadd.LocationFromID && tocurrent.LocationToID == toadd.LocationToID)
                                    {
                                        result.ETD = etd;
                                        result.ETA = eta;
                                        toadd.ETD = result.ETD;
                                        toadd.ETA = result.ETA;
                                        tocurrent.ETD = result.ETD;
                                        tocurrent.ETA = result.ETA;
                                        result.ListCOContainer.Add(tocurrent);
                                        result.ListCOContainer.Add(toadd);
                                    }
                                    else if (tocurrent.LocationFromID != toadd.LocationFromID && tocurrent.LocationToID != toadd.LocationToID)
                                    {
                                        var matrixhour = HourMatrixDefault;
                                        var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocurrent.LocationFromID && c.LocationToID == toadd.LocationFromID).Select(c => new { c.Hour }).FirstOrDefault();
                                        if (matrix != null && matrix.Hour > 0)
                                            matrixhour = matrix.Hour;
                                        eta = eta.AddHours(matrixhour);

                                        matrixhour = HourMatrixDefault;
                                        matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocurrent.LocationFromID && c.LocationToID == toadd.LocationFromID).Select(c => new { c.Hour }).FirstOrDefault();
                                        if (matrix != null && matrix.Hour > 0)
                                            matrixhour = matrix.Hour;
                                        eta = eta.AddHours(matrixhour);

                                        result.ETD = etd;
                                        result.ETA = eta;
                                        toadd.ETD = result.ETD;
                                        toadd.ETA = result.ETA;
                                        tocurrent.ETD = result.ETD;
                                        tocurrent.ETA = result.ETA;
                                        result.ListCOContainer.Add(tocurrent);
                                        result.ListCOContainer.Add(toadd);
                                    }
                                    else
                                    {
                                        var matrixhour = HourMatrixDefault;
                                        var matrix = model.CAT_LocationMatrix.Where(c => c.LocationFromID == tocurrent.LocationToID && c.LocationToID == toadd.LocationToID).Select(c => new { c.Hour }).FirstOrDefault();
                                        if (matrix != null && matrix.Hour > 0)
                                            matrixhour = matrix.Hour;
                                        eta = eta.AddHours(matrixhour);

                                        result.ETD = etd;
                                        result.ETA = eta;
                                        toadd.ETD = result.ETD;
                                        toadd.ETA = result.ETA;
                                        tocurrent.ETD = result.ETD;
                                        tocurrent.ETA = result.ETA;
                                        result.ListCOContainer.Add(tocurrent);
                                        result.ListCOContainer.Add(toadd);
                                    }
                                }
                                //end 2 con
                            }
                            else if (serviceAdd.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal && statusempty.Contains(tocontainer.StatusOfCOContainerID))
                            {
                                //local
                                double matrixhour = HourMatrixDefault;

                                var etd = tocontainer.ETD.Value;
                                var eta = tocontainer.ETA.Value;
                                eta = eta.AddHours(matrixhour);
                                result.ETD = etd;
                                result.ETA = eta;

                                result.ListCOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => new DTOOPSCOTOContainer
                                {
                                    ID = c.ID,
                                    StatusOfCOContainerID = c.StatusOfCOContainerID,
                                    LocationFromID = c.LocationFromID,
                                    LocationToID = c.LocationToID,
                                    ETD = etd,
                                    ETA = eta
                                }).ToList();

                                result.ListCOContainer.AddRange(model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new DTOOPSCOTOContainer
                                {
                                    ID = c.ID,
                                    StatusOfCOContainerID = c.StatusOfCOContainerID,
                                    LocationFromID = c.LocationFromID,
                                    LocationToID = c.LocationToID,
                                    ETD = etd,
                                    ETA = eta
                                }).ToList());
                                //end local
                            }
                            else if (serviceAdd.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport && tocontainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty)
                            {
                                //swap
                                double matrixhour = HourMatrixDefault;

                                var etd = tocontainer.ETD.Value;
                                var eta = tocontainer.ETA.Value;
                                eta = eta.AddHours(matrixhour);
                                result.ETD = etd;
                                result.ETA = eta;

                                result.ListCOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => new DTOOPSCOTOContainer
                                {
                                    ID = c.ID,
                                    StatusOfCOContainerID = c.StatusOfCOContainerID,
                                    LocationFromID = c.LocationFromID,
                                    LocationToID = c.LocationToID,
                                    ETD = etd,
                                    ETA = eta
                                }).ToList();

                                result.ListCOContainer.AddRange(model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new DTOOPSCOTOContainer
                                {
                                    ID = c.ID,
                                    StatusOfCOContainerID = c.StatusOfCOContainerID,
                                    LocationFromID = c.LocationFromID,
                                    LocationToID = c.LocationToID,
                                    ETD = etd,
                                    ETA = eta
                                }).ToList());
                                //end swap
                            }
                        }
                    }
                    else if (lstServiceCurrent.Count > 1)
                    {
                        List<int> statusempty = new List<int>()
                        {
                            -(int)SYSVarType.StatusOfCOContainerEXEmpty,
                            -(int)SYSVarType.StatusOfCOContainerIMEmpty
                        };
                        List<int> statusladen = new List<int>()
                        {
                            -(int)SYSVarType.StatusOfCOContainerEXLaden,
                            -(int)SYSVarType.StatusOfCOContainerIMLaden
                        };

                        var tocontainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && statusempty.Contains(c.StatusOfCOContainerID)).Select(c => new { c.ID, c.ETD, c.ETA, c.StatusOfCOContainerID }).FirstOrDefault();
                        if (tocontainer == null)
                            tocontainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && statusladen.Contains(c.StatusOfCOContainerID)).Select(c => new { c.ID, c.ETD, c.ETA, c.StatusOfCOContainerID }).FirstOrDefault();
                        if (tocontainer == null)
                            tocontainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.ParentID == null).Select(c => new { c.ID, c.ETD, c.ETA, c.StatusOfCOContainerID }).FirstOrDefault();

                        if (tocontainer != null)
                        {
                            if (lstServiceCurrent.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 2 || lstServiceCurrent.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 2)
                            {
                                if (statusempty.Contains(tocontainer.StatusOfCOContainerID))
                                {
                                    int local = lstServiceAdd.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Count();
                                    if (local > 0 && local % 2 == 0)
                                    {
                                        //2 con + local
                                        double hourlocal = local / 1;
                                        double matrixhour = HourMatrixDefault * hourlocal;

                                        var etd = tocontainer.ETD.Value;
                                        var eta = tocontainer.ETA.Value;
                                        eta = eta.AddHours(matrixhour);
                                        result.ETD = etd;
                                        result.ETA = eta;

                                        result.ListCOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => new DTOOPSCOTOContainer
                                        {
                                            ID = c.ID,
                                            StatusOfCOContainerID = c.StatusOfCOContainerID,
                                            LocationFromID = c.LocationFromID,
                                            LocationToID = c.LocationToID,
                                            ETD = etd,
                                            ETA = eta
                                        }).ToList();

                                        result.ListCOContainer.AddRange(model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new DTOOPSCOTOContainer
                                        {
                                            ID = c.ID,
                                            StatusOfCOContainerID = c.StatusOfCOContainerID,
                                            LocationFromID = c.LocationFromID,
                                            LocationToID = c.LocationToID,
                                            ETD = etd,
                                            ETA = eta
                                        }).ToList());
                                        //end 2 con + local
                                    }

                                    if (tocontainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty &&
                                        lstServiceAdd.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 2)
                                    {
                                        //2 con swap
                                        double matrixhour = HourMatrixDefault;
                                        var etd = tocontainer.ETD.Value;
                                        var eta = tocontainer.ETA.Value;
                                        eta = eta.AddHours(matrixhour);
                                        result.ETD = etd;
                                        result.ETA = eta;

                                        result.ListCOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => new DTOOPSCOTOContainer
                                        {
                                            ID = c.ID,
                                            StatusOfCOContainerID = c.StatusOfCOContainerID,
                                            LocationFromID = c.LocationFromID,
                                            LocationToID = c.LocationToID,
                                            ETD = etd,
                                            ETA = eta
                                        }).ToList();

                                        result.ListCOContainer.AddRange(model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new DTOOPSCOTOContainer
                                        {
                                            ID = c.ID,
                                            StatusOfCOContainerID = c.StatusOfCOContainerID,
                                            LocationFromID = c.LocationFromID,
                                            LocationToID = c.LocationToID,
                                            ETD = etd,
                                            ETA = eta
                                        }).ToList());
                                        //end 2 con swap
                                    }
                                }

                                if (result.ListCOContainer.Count == 0)
                                    result.OfferTimeError = "Không thực hiện được";
                            }
                            else
                            {

                            }
                        }
                    }
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "list fail");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list fail");

            if (string.IsNullOrEmpty(result.OfferTimeError))
            {
                var lstID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => c.ID).Distinct().ToList();
                lstID.AddRange(lstCOTOContainerID);
                result = CO_CheckConstraint(model, account, result, lstID);
            }

            return result;
        }

        public static HelperTOMaster_Error OPSCO_MasterAddTOContainer(DataEntities model, AccountItem account, int masterid, DateTime etd, DateTime eta, List<DTOOPSCOTOContainer> lstTOContainer)
        {
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };

            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
            if (objMaster != null && (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID))
            {
                var lstCOTOContainerID = lstTOContainer.Select(c => c.ID).ToList();
                //var tolast = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.ParentID > 0).Select(c => new { c.ID, c.OPSContainerID, c.ParentID, c.LocationFromID, c.LocationToID, c.SortOrder }).OrderByDescending(c => c.SortOrder).FirstOrDefault();
                //var tolocal = model.OPS_COTOContainer.Where(c => c.COTOMasterID == null && lstCOTOContainerID.Contains(c.ID) && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).Select(c => new { c.ID, c.LocationFromID, c.LocationToID }).FirstOrDefault();
                if (etd.CompareTo(eta) < 0 && etd.CompareTo(DateTime.MinValue) > 0)
                {
                    objMaster.ETD = etd;
                    objMaster.ETA = eta;
                    objMaster.ATD = etd;
                    objMaster.ATA = eta;
                    objMaster.ModifiedBy = account.UserName;
                    objMaster.ModifiedDate = DateTime.Now;

                    foreach (var tocontainer in lstTOContainer)
                    {
                        var objCON = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == tocontainer.ID);
                        if (objCON != null)
                        {
                            objCON.COTOMasterID = objMaster.ID;
                            objCON.ETD = tocontainer.ETD;
                            objCON.ETA = tocontainer.ETA;
                            objCON.DateFromCome = tocontainer.ETD;
                            objCON.DateToCome = tocontainer.ETA;



                            //if (objCON.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty)
                            //if (!statusload.Contains(objCON.StatusOfCOContainerID))




                            //if (tolast != null && tolocal != null && objCON.ID == tolast.ID)
                            //{
                            //    if (tolocal.LocationFromID == objCON.LocationFromID)
                            //        objCON.SortOrder = objCON.SortOrder + 1;
                            //    else
                            //        objCON.SortOrder = objCON.SortOrder + 2;
                            //    objCON.LocationFromID = tolocal.LocationToID;
                            //}

                            //if (objCON.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden)
                            //{
                            //    var load = model.OPS_COTOContainer.FirstOrDefault(c => c.OPSContainerID == objCON.OPSContainerID && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLoad);
                            //    if (load != null)
                            //    {
                            //        load.IsSplit = false;
                            //        var loadhour = (load.ETA.Value - load.ETD.Value).TotalHours;
                            //        load.ETA = objCON.ETD.Value.AddHours(-HourEmpty);
                            //        load.ETD = load.ETA.Value.AddHours(-loadhour);
                            //        load.DateFromCome = load.ETD;
                            //        load.DateToCome = load.ETA;
                            //        load.COTOMasterID = objMaster.ID;
                            //    }

                            //    var unload = model.OPS_COTOContainer.FirstOrDefault(c => c.OPSContainerID == objCON.OPSContainerID && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerUnLoad);
                            //    if (unload != null)
                            //    {
                            //        unload.IsSplit = false;
                            //        var loadhour = (unload.ETA.Value - unload.ETD.Value).TotalHours;
                            //        unload.ETD = objCON.ETA.Value.AddHours(HourEmpty);
                            //        unload.ETA = unload.ETD.Value.AddHours(loadhour);
                            //        unload.DateFromCome = unload.ETD;
                            //        unload.DateToCome = unload.ETA;
                            //        unload.COTOMasterID = objMaster.ID;
                            //    }
                            //}

                            //if (!lstOPSContainerID.Contains(objCON.OPSContainerID))
                            //    lstOPSContainerID.Add(objCON.OPSContainerID);
                        }
                        ////lstOPSContainerID = lstCON.Select(c => c.OPSContainerID).Distinct().ToList();
                        //model.SaveChanges();

                        //HelperTimeSheet.ChangePlan(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, etd, eta);
                        ////CO_ResetRomoocByOPSContainer(model, account, lstOPSContainerID);
                        //OPSCO_ResetOPSContainer(model, account, lstTOContainer.Select(c => c.ID).ToList());
                    }

                    model.SaveChanges();
                    OPSCO_ResetOPSContainer(model, account, lstTOContainer.Select(c => c.ID).ToList());

                    //var lstService = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => new { c.OPSContainerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).Distinct().ToList();
                    //if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 2 || lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 2)
                    //{
                    //    var lstIMEX = lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).ToList();
                    //    var first = lstIMEX[0];
                    //    var second = lstIMEX[1];

                    //    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == first.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                    //    {
                    //        tocontainer.IsDuplicateHidden = false;
                    //    }

                    //    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == second.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                    //    {
                    //        tocontainer.IsDuplicateHidden = true;
                    //    }

                    //    model.SaveChanges();
                    //}
                    //else if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 1 && lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 1)
                    //{
                    //    var lstIMEX = lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).ToList();
                    //    var first = lstIMEX[0];
                    //    var second = lstIMEX[1];


                    //}
                    //else if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 1 || lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 1)
                    //{

                    //}

                    //if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Count() > 0 && lstService.Where(c => c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderLocal).Count() > 0)
                    //{
                    //    foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal))
                    //    {
                    //        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                    //        {
                    //            tocontainer.IsDuplicateHidden = true;
                    //        }
                    //    }
                    //    model.SaveChanges();
                    //}
                    //else if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Count() > 0)
                    //{
                    //    foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal))
                    //    {
                    //        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)))
                    //        {
                    //            tocontainer.IsDuplicateHidden = null;
                    //        }
                    //    }
                    //    model.SaveChanges();
                    //}


                    //HelperTimeSheet.ChangePlan(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, etd, eta);
                    ////CO_ResetRomoocByOPSContainer(model, account, lstOPSContainerID);
                    //OPSCO_ResetOPSContainer(model, account, lstTOContainer.Select(c => c.ID).ToList());



                    //if (tolocal != null)
                    //{
                    //    objMaster.ETD = etd;
                    //    objMaster.ETA = eta;
                    //    objMaster.ATD = etd;
                    //    objMaster.ATA = eta;
                    //    objMaster.ModifiedBy = account.UserName;
                    //    objMaster.ModifiedDate = DateTime.Now;

                    //    var lstOPSContainerID = new List<int>();
                    //    //var lstCON = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objMaster.ID).ToList();
                    //    foreach (var tocontainer in lstTOContainer)
                    //    {
                    //        var objCON = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == tocontainer.ID);
                    //        if (objCON != null)
                    //        {
                    //            //if (objCON.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty)
                    //            if (!statusload.Contains(objCON.StatusOfCOContainerID))
                    //                objCON.COTOMasterID = objMaster.ID;
                    //            objCON.ETD = tocontainer.ETD;
                    //            objCON.ETA = tocontainer.ETA;
                    //            objCON.DateFromCome = objCON.ETD;
                    //            objCON.DateToCome = objCON.ETA;

                    //            if (tolast != null && tolocal != null && objCON.ID == tolast.ID)
                    //            {
                    //                if (tolocal.LocationFromID == objCON.LocationFromID)
                    //                    objCON.SortOrder = objCON.SortOrder + 1;
                    //                else
                    //                    objCON.SortOrder = objCON.SortOrder + 2;
                    //                objCON.LocationFromID = tolocal.LocationToID;
                    //            }

                    //            if (objCON.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden)
                    //            {
                    //                var load = model.OPS_COTOContainer.FirstOrDefault(c => c.OPSContainerID == objCON.OPSContainerID && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLoad);
                    //                if (load != null)
                    //                {
                    //                    load.IsSplit = false;
                    //                    var loadhour = (load.ETA.Value - load.ETD.Value).TotalHours;
                    //                    load.ETA = objCON.ETD.Value.AddHours(-HourEmpty);
                    //                    load.ETD = load.ETA.Value.AddHours(-loadhour);
                    //                    load.DateFromCome = load.ETD;
                    //                    load.DateToCome = load.ETA;
                    //                    load.COTOMasterID = objMaster.ID;
                    //                }

                    //                var unload = model.OPS_COTOContainer.FirstOrDefault(c => c.OPSContainerID == objCON.OPSContainerID && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerUnLoad);
                    //                if (unload != null)
                    //                {
                    //                    unload.IsSplit = false;
                    //                    var loadhour = (unload.ETA.Value - unload.ETD.Value).TotalHours;
                    //                    unload.ETD = objCON.ETA.Value.AddHours(HourEmpty);
                    //                    unload.ETA = unload.ETD.Value.AddHours(loadhour);
                    //                    unload.DateFromCome = unload.ETD;
                    //                    unload.DateToCome = unload.ETA;
                    //                    unload.COTOMasterID = objMaster.ID;
                    //                }
                    //            }

                    //            if (!lstOPSContainerID.Contains(objCON.OPSContainerID))
                    //                lstOPSContainerID.Add(objCON.OPSContainerID);
                    //        }
                    //        else if (tolast != null && tolocal != null)
                    //        {
                    //            var obj = new OPS_COTOContainer();
                    //            obj.ParentID = tolast.ParentID;
                    //            obj.COTOMasterID = objMaster.ID;
                    //            obj.CreatedBy = account.UserName;
                    //            obj.CreatedDate = DateTime.Now;
                    //            obj.OPSContainerID = tolast.OPSContainerID;
                    //            obj.IsClosed = false;
                    //            obj.HasUpload = false;
                    //            obj.IsSplit = false;
                    //            obj.ETD = tocontainer.ETD;
                    //            obj.ETDStart = tocontainer.ETD.Value.AddHours(-HourEmpty);
                    //            obj.ETA = tocontainer.ETA;
                    //            obj.ETAStart = tocontainer.ETA.Value.AddHours(-HourEmpty);
                    //            obj.DateFromCome = tocontainer.ETD;
                    //            obj.DateToCome = tocontainer.ETA;
                    //            if (tocontainer.ID == -2)
                    //            {
                    //                obj.StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty;
                    //                obj.IsDuplicateHidden = false;
                    //                obj.SortOrder = tolast.SortOrder + 1;
                    //                obj.LocationFromID = tolast.LocationFromID;
                    //                obj.LocationToID = tolocal.LocationFromID;
                    //            }
                    //            else if (tocontainer.ID == -1)
                    //            {
                    //                obj.StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop;
                    //                obj.IsDuplicateHidden = true;
                    //                obj.SortOrder = tolast.SortOrder;
                    //                obj.LocationFromID = tolocal.LocationFromID;
                    //                obj.LocationToID = tolocal.LocationToID;
                    //            }
                    //            obj.IsInput = true;
                    //            obj.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                    //            obj.TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait;
                    //            model.OPS_COTOContainer.Add(obj);
                    //        }
                    //    }
                    //    //lstOPSContainerID = lstCON.Select(c => c.OPSContainerID).Distinct().ToList();
                    //    model.SaveChanges();

                    //    HelperTimeSheet.ChangePlan(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, etd, eta);
                    //    CO_ResetRomoocByOPSContainer(model, account, lstOPSContainerID);
                    //    OPSCO_ResetOPSContainer(model, account, lstTOContainer.Select(c => c.ID).ToList());
                    //}
                    //else
                    //{
                    //    var opscontainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && (c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)).Select(c => new { c.OPSContainerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID, c.OPS_Container.OPSContainerTypeID, c.OPS_Container.RomoocID }).FirstOrDefault();
                    //    var opscontaineradd = model.OPS_COTOContainer.Where(c => c.COTOMasterID == null && lstCOTOContainerID.Contains(c.ID) && (c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)).Select(c => new { c.OPSContainerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).FirstOrDefault();
                    //    if (opscontainer != null && opscontaineradd != null && opscontainer.ServiceOfOrderID == opscontaineradd.ServiceOfOrderID)
                    //    {
                    //        var lstCOTOContainer = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainer.OPSContainerID).ToList();
                    //        var lstCOTOContainerAdd = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontaineradd.OPSContainerID);
                    //        foreach (var tocontaineradd in lstCOTOContainerAdd)
                    //        {
                    //            var tocontainer = lstCOTOContainer.FirstOrDefault(c => c.StatusOfCOContainerID == tocontaineradd.StatusOfCOContainerID);
                    //            if (tocontainer != null && tocontainer.ParentID == null)
                    //            {
                    //                tocontaineradd.COTOMasterID = tocontainer.COTOMasterID;
                    //                tocontaineradd.ETD = tocontainer.ETD;
                    //                tocontaineradd.ETDStart = tocontainer.ETD.Value.AddHours(-HourMin);
                    //                tocontaineradd.ETA = tocontainer.ETA;
                    //                tocontaineradd.ETAStart = tocontainer.ETA.Value.AddHours(-HourMin);
                    //                tocontaineradd.DateFromCome = tocontainer.ETD;
                    //                tocontaineradd.DateToCome = tocontainer.ETA;
                    //                tocontaineradd.IsDuplicateHidden = true;

                    //                if (tocontainer.IsDuplicateHidden == null)
                    //                    tocontainer.IsDuplicateHidden = false;
                    //            }
                    //        }
                    //        var objContainerAdd = model.OPS_Container.FirstOrDefault(c => c.ID == opscontaineradd.OPSContainerID);
                    //        if (objContainerAdd != null)
                    //        {
                    //            objContainerAdd.RomoocID = opscontainer.RomoocID;
                    //            objContainerAdd.OPSContainerTypeID = opscontainer.OPSContainerTypeID;
                    //            objContainerAdd.ModifiedBy = account.UserName;
                    //            objContainerAdd.ModifiedDate = DateTime.Now;
                    //        }
                    //        model.SaveChanges();

                    //        OPSCO_ResetOPSContainer(model, account, new List<int>() { opscontainer.OPSContainerID, opscontaineradd.OPSContainerID });
                    //    }
                    //}
                }
            }

            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error OPSCO_MasterRemoveTOContainer(DataEntities model, AccountItem account, int masterid, int tocontainerid)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID == masterid).Select(c => new { c.ID, c.ParentID, c.OPSContainerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).FirstOrDefault();
            if (objCON != null)
            {
                if (objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal)
                {
                    var totalLocal = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).Count();
                    var lstCOTOContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.ParentID == null).Select(c => c.ID).ToList();
                    foreach (var item in model.OPS_COTOContainer.Where(c => c.OPSContainerID == objCON.OPSContainerID))
                    {
                        if (item.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden)
                        {
                            item.COTOMasterID = null;
                            item.ModifiedBy = account.UserName;
                            item.ModifiedDate = DateTime.Now;
                        }
                        else
                        {
                            item.COTOMasterID = null;
                            item.IsSplit = true;
                        }
                    }
                    var objOPSContainer = model.OPS_Container.FirstOrDefault(c => c.ID == objCON.OPSContainerID);
                    if (objOPSContainer != null)
                    {
                        objOPSContainer.RomoocID = null;
                        objOPSContainer.ModifiedBy = account.UserName;
                        objOPSContainer.ModifiedDate = DateTime.Now;
                    }

                    if (totalLocal == 1)
                    {
                        foreach (var item in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid))
                        {
                            if (item.ParentID > 0)
                                model.OPS_COTOContainer.Remove(item);
                            else
                            {
                                item.IsSplit = false;
                            }
                        }
                    }
                    model.SaveChanges();

                    OPSCO_ResetOPSContainer(model, account, lstCOTOContainerID);
                }
                else
                {
                    var lstCOTOContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.ParentID == null).Select(c => c.ID).ToList();
                    foreach (var item in model.OPS_COTOContainer.Where(c => c.OPSContainerID == objCON.OPSContainerID))
                    {
                        if (item.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLoad || item.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerUnLoad)
                        {
                            item.COTOMasterID = null;
                            item.IsSplit = true;
                        }
                        else if (item.COTOMasterID == masterid)
                        {
                            item.COTOMasterID = null;
                            item.IsDuplicateHidden = null;
                            item.IsSwap = false;
                            item.SwapID = null;
                            item.ModifiedBy = account.UserName;
                            item.ModifiedDate = DateTime.Now;
                        }
                    }
                    model.SaveChanges();

                    OPSCO_ResetOPSContainer(model, account, lstCOTOContainerID);
                }
            }
            return HelperTOMaster_Error.None;
        }



        public static List<HelperTOMaster_Error> OPSCO_TenderAccept(DataEntities model, AccountItem account, List<DTOOPSCO_VEN_TOMaster> lstMaster)
        {
            if (lstMaster != null && lstMaster.Count > 0)
            {
                var result = new List<HelperTOMaster_Error>();

                foreach (var item in lstMaster)
                {
                    var objRate = model.OPS_COTORate.Where(c => c.COTOMasterID == item.ID && c.IsAccept == null).OrderBy(c => c.SortOrder).FirstOrDefault();
                    if (objRate == null)
                        result.Add(HelperTOMaster_Error.Fail);
                    else
                    {
                        var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == item.ID);
                        if (objMaster != null)
                        {
                            if (item.VehicleID <= 2)
                                objMaster.VehicleID = DefaultTruck;
                            else
                                objMaster.VehicleID = item.VehicleID;
                            if (item.RomoocID > 1)
                                objMaster.RomoocID = item.RomoocID;
                            else
                                objMaster.RomoocID = DefaultRomooc;
                            objMaster.DriverName1 = item.DriverName;
                            objMaster.DriverTel1 = item.DriverTel;
                            objMaster.VendorOfVehicleID = objRate.VendorID;
                            objMaster.VendorOfRomoocID = objRate.VendorID;
                            objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterTendered;
                            objMaster.ModifiedBy = account.UserName;
                            objMaster.ModifiedDate = DateTime.Now;

                            objRate.IsAccept = true;

                            model.SaveChanges();

                            result.Add(HelperTOMaster_Error.None);
                        }
                    }
                }

                return result;
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list fail");
        }

        public static List<HelperTOMaster_Error> OPSCO_TenderReject(DataEntities model, AccountItem account, List<DTOOPSCO_VEN_TOMaster> lstMaster)
        {
            if (lstMaster != null && lstMaster.Count > 0)
            {
                var result = new List<HelperTOMaster_Error>();

                foreach (var item in lstMaster)
                {
                    var objRate = model.OPS_COTORate.Where(c => c.COTOMasterID == item.ID && c.IsAccept == null).OrderBy(c => c.SortOrder).FirstOrDefault();
                    if (objRate == null)
                        result.Add(HelperTOMaster_Error.Fail);
                    else
                    {
                        var objNext = model.OPS_COTORate.Where(c => c.COTOMasterID == item.ID && c.SortOrder > objRate.SortOrder).OrderBy(c => c.SortOrder).FirstOrDefault();
                        var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == item.ID);
                        if (objMaster != null)
                        {
                            objRate.IsAccept = false;
                            if (item.ReasonID > 0)
                            {
                                objRate.ReasonID = item.ReasonID;
                                objRate.Reason = item.Reason;
                            }
                            if (objNext != null)
                            {
                                objMaster.VendorOfVehicleID = objNext.VendorID;
                                objMaster.VendorOfRomoocID = objNext.VendorID;
                                objNext.FirstRateTime = DateTime.Now;
                                objNext.LastRateTime = DateTime.Now.AddHours(objMaster.RateTime.Value);
                            }
                            else
                            {
                                objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproved;
                                objMaster.VendorOfVehicleID = objMaster.SYSCustomerID;
                                objMaster.VendorOfRomoocID = objMaster.SYSCustomerID;
                                objMaster.ModifiedBy = account.UserName;
                                objMaster.ModifiedDate = DateTime.Now;
                            }
                            model.SaveChanges();

                            result.Add(HelperTOMaster_Error.None);
                        }
                    }
                }

                return result;
            }
            else
                return new List<HelperTOMaster_Error>();
            //else
            //    throw FaultHelper.BusinessFault(null, null, "list fail");
        }



        public static List<DTOOPSCOTOContainer> OPSCO_AddTOContainerMasterOfferTime(DataEntities model, AccountItem account, int masterid, List<int> lstCOTOContainerID)
        {
            if (lstCOTOContainerID != null && lstCOTOContainerID.Count > 0)
            {
                var result = new List<DTOOPSCOTOContainer>();

                var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid).Select(c => new { c.ID, c.ETD, c.ETA }).FirstOrDefault();
                if (objMaster != null)
                {
                    double totalHour = (objMaster.ETA - objMaster.ETD).TotalHours;

                    var lstCOTOContainerMaster = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => new
                    {
                        c.ID,
                        c.ETD,
                        c.ETA,
                        c.SortOrder,
                        c.OPSContainerID,
                        c.COTOMasterID,
                        c.ParentID,
                        c.OPS_Container.ORD_Container.CAT_Packing.TypeOfPackageID,
                        ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID > 0 ? c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID.Value : -1
                    }).ToList();
                    var lstCOTOContainerAdd = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new
                    {
                        c.ID,
                        c.ETD,
                        c.ETA,
                        c.SortOrder,
                        c.OPSContainerID,
                        c.COTOMasterID,
                        c.ParentID,
                        c.OPS_Container.ORD_Container.CAT_Packing.TypeOfPackageID,
                        ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID > 0 ? c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID.Value : -1
                    }).ToList();

                    var lstOPSContainerMaster = lstCOTOContainerMaster.Select(c => new { c.OPSContainerID, c.TypeOfPackageID, c.ServiceOfOrderID }).Distinct().ToList();
                    var lstOPSContainerAdd = lstCOTOContainerAdd.Select(c => new { c.OPSContainerID, c.COTOMasterID, c.TypeOfPackageID, c.ServiceOfOrderID }).Distinct().ToList();

                    if (lstOPSContainerAdd.Where(c => c.COTOMasterID > 0).Count() > 0)
                    {
                        //add master to master



                    }
                    else
                    {
                        //add order to master

                        var lstTypeOfPacking20 = new List<int>() { -(int)SYSVarType.TypeOfPackingCO20, -(int)SYSVarType.TypeOfPackingCO20R, -(int)SYSVarType.TypeOfPackingCO20F };

                        if (lstOPSContainerMaster.Count == 1 && lstOPSContainerAdd.Count == 1)
                        {
                            //1 con add 1 con

                            var conMaster = lstOPSContainerMaster[0];
                            var conAdd = lstOPSContainerAdd[0];

                            if (conMaster.ServiceOfOrderID == conAdd.ServiceOfOrderID && conMaster.TypeOfPackageID == conAdd.TypeOfPackageID && lstTypeOfPacking20.Contains(conMaster.TypeOfPackageID))
                            {
                                //2 x 20'
                                result.AddRange(model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false).Select(c => new DTOOPSCOTOContainer
                                {
                                    ID = c.ID,
                                    COTOMasterID = c.COTOMasterID,
                                    ETD = c.ETD,
                                    ETA = c.ETA,
                                    SortOrder = c.SortOrder
                                }).OrderBy(c => c.SortOrder).ToList());

                                var queryContainerAdd = model.OPS_COTOContainer.Where(c => c.OPSContainerID == conAdd.OPSContainerID && c.ParentID == null).OrderBy(c => c.SortOrder);
                                var total = queryContainerAdd.Count();
                                double divHour = totalHour / total;
                                var startDate = objMaster.ETD;
                                foreach (var item in queryContainerAdd)
                                {
                                    result.Add(new DTOOPSCOTOContainer
                                    {
                                        ID = item.ID,
                                        ETD = startDate,
                                        ETA = startDate.AddHours(divHour),
                                        SortOrder = item.SortOrder
                                    });
                                    startDate = startDate.AddHours(divHour);
                                }
                            }
                            else if (conMaster.ServiceOfOrderID != conAdd.ServiceOfOrderID && conMaster.TypeOfPackageID == conAdd.TypeOfPackageID)
                            {
                                //difference service
                                bool flagEX = conMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || conAdd.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport;
                                bool flagIM = conMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport || conAdd.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport;
                                bool flagLO = conMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal || conAdd.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal;

                                var lstStatusEmpty = new List<int>() { -(int)SYSVarType.StatusOfCOContainerEXEmpty, -(int)SYSVarType.StatusOfCOContainerIMEmpty, -(int)SYSVarType.StatusOfCOContainerShipEmpty, -(int)SYSVarType.StatusOfCOContainerReturnEmpty };

                                if (flagEX && flagIM)
                                {
                                    //swap 

                                    if (conMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)
                                    {
                                        //im add ex

                                        var emptyMaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == conMaster.OPSContainerID && c.IsSplit == false && lstStatusEmpty.Contains(c.StatusOfCOContainerID)).Select(c => new { c.ID, c.ETD, c.ETA }).FirstOrDefault();
                                        if (emptyMaster != null && emptyMaster.ETD != null && emptyMaster.ETA != null && emptyMaster.ETA.Value.CompareTo(emptyMaster.ETD.Value) > 0)
                                        {
                                            totalHour = (emptyMaster.ETA.Value - emptyMaster.ETD.Value).TotalHours;
                                            var queryContainerAdd = model.OPS_COTOContainer.Where(c => c.OPSContainerID == conAdd.OPSContainerID && c.ParentID == null).OrderBy(c => c.SortOrder);
                                            var total = queryContainerAdd.Count();
                                            double divHour = totalHour / total;
                                            var startDate = emptyMaster.ETD.Value;

                                            var queryContainerMaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == conMaster.OPSContainerID && c.IsSplit == false).Select(c => new { c.ID, c.OPSContainerID, c.ETD, c.ETA, c.SortOrder });
                                            foreach (var item in queryContainerMaster)
                                            {
                                                if (item.ID != emptyMaster.ID)
                                                {
                                                    result.Add(new DTOOPSCOTOContainer
                                                    {
                                                        ID = item.ID,
                                                        ETD = item.ETD,
                                                        ETA = item.ETA,
                                                        SortOrder = item.SortOrder
                                                    });
                                                }
                                                else
                                                {
                                                    result.Add(new DTOOPSCOTOContainer
                                                    {
                                                        ID = item.ID,
                                                        ETD = item.ETD,
                                                        ETA = item.ETD.Value.AddHours(divHour),
                                                        SortOrder = item.SortOrder
                                                    });
                                                    break;
                                                }
                                            }

                                            foreach (var item in queryContainerAdd)
                                            {
                                                result.Add(new DTOOPSCOTOContainer
                                                {
                                                    ID = item.ID,
                                                    ETD = startDate,
                                                    ETA = startDate.AddHours(divHour),
                                                    SortOrder = item.SortOrder
                                                });
                                                startDate = startDate.AddHours(divHour);
                                            }
                                        }
                                    }
                                    else if (conMaster.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport)
                                    {
                                        //ex add im 

                                        var emptyMaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == conMaster.OPSContainerID && c.IsSplit == false && lstStatusEmpty.Contains(c.StatusOfCOContainerID)).Select(c => new { c.ID, c.ETD, c.ETA }).FirstOrDefault();
                                        if (emptyMaster != null && emptyMaster.ETD != null && emptyMaster.ETA != null && emptyMaster.ETA.Value.CompareTo(emptyMaster.ETD.Value) > 0)
                                        {
                                            totalHour = (emptyMaster.ETA.Value - emptyMaster.ETD.Value).TotalHours;
                                            var queryContainerAdd = model.OPS_COTOContainer.Where(c => c.OPSContainerID == conAdd.OPSContainerID && c.ParentID == null).OrderBy(c => c.SortOrder);
                                            var total = queryContainerAdd.Count();
                                            double divHour = totalHour / total;
                                            var startDate = emptyMaster.ETD.Value;

                                            var queryContainerMaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == conMaster.OPSContainerID && c.IsSplit == false).Select(c => new { c.ID, c.OPSContainerID, c.ETD, c.ETA, c.SortOrder });
                                            foreach (var item in queryContainerMaster)
                                            {
                                                if (item.ID != emptyMaster.ID)
                                                {
                                                    result.Add(new DTOOPSCOTOContainer
                                                    {
                                                        ID = item.ID,
                                                        ETD = item.ETD,
                                                        ETA = item.ETA,
                                                        SortOrder = item.SortOrder
                                                    });
                                                }
                                                else
                                                {
                                                    result.Add(new DTOOPSCOTOContainer
                                                    {
                                                        ID = item.ID,
                                                        ETD = item.ETD,
                                                        ETA = item.ETD.Value.AddHours(divHour),
                                                        SortOrder = item.SortOrder
                                                    });
                                                }
                                            }

                                            foreach (var item in queryContainerAdd)
                                            {
                                                result.Add(new DTOOPSCOTOContainer
                                                {
                                                    ID = item.ID,
                                                    ETD = startDate,
                                                    ETA = startDate.AddHours(divHour),
                                                    SortOrder = item.SortOrder
                                                });
                                                startDate = startDate.AddHours(divHour);
                                            }
                                        }
                                    }

                                }
                                else if (flagLO && conAdd.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal)
                                {
                                    //join

                                    DateTime? etd = null;
                                    DateTime? eta = null;

                                    if (flagEX || flagIM)
                                    {
                                        var emptyMaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == conMaster.OPSContainerID && c.IsSplit == false && lstStatusEmpty.Contains(c.StatusOfCOContainerID)).Select(c => new { c.ETD, c.ETA }).FirstOrDefault();
                                        if (emptyMaster != null && emptyMaster.ETD != null && emptyMaster.ETA != null && emptyMaster.ETA.Value.CompareTo(emptyMaster.ETD.Value) > 0)
                                        {
                                            etd = emptyMaster.ETD;
                                            eta = emptyMaster.ETA;
                                        }
                                    }

                                    if (etd != null && eta != null)
                                    {
                                        result.AddRange(model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false).Select(c => new DTOOPSCOTOContainer
                                        {
                                            ID = c.ID,
                                            COTOMasterID = c.COTOMasterID,
                                            ETD = c.ETD,
                                            ETA = c.ETA,
                                            SortOrder = c.SortOrder
                                        }).OrderBy(c => c.SortOrder).ToList());

                                        totalHour = (eta.Value - etd.Value).TotalHours;
                                        var queryContainerAdd = model.OPS_COTOContainer.Where(c => c.OPSContainerID == conAdd.OPSContainerID && c.ParentID == null && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).OrderBy(c => c.SortOrder);
                                        var total = queryContainerAdd.Count();
                                        double divHour = totalHour / total;
                                        var startDate = objMaster.ETD;
                                        foreach (var item in queryContainerAdd)
                                        {
                                            result.Add(new DTOOPSCOTOContainer
                                            {
                                                ID = item.ID,
                                                ETD = startDate,
                                                ETA = startDate.AddHours(divHour),
                                                SortOrder = item.SortOrder
                                            });
                                            startDate = startDate.AddHours(divHour);
                                        }
                                    }
                                }
                            }
                        }
                        else if (lstOPSContainerMaster.Count == 2)
                        {
                            //2 con master

                            if (lstOPSContainerMaster.Where(c => c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderExport).Count() == 0 &&
                                lstOPSContainerAdd.Where(c => !lstTypeOfPacking20.Contains(c.TypeOfPackageID) || c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderLocal).Count() == 0)
                            {
                                //ex join


                            }
                            else if (lstOPSContainerMaster.Where(c => c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderImport).Count() == 0 &&
                                lstOPSContainerAdd.Where(c => !lstTypeOfPacking20.Contains(c.TypeOfPackageID) || c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderLocal).Count() == 0)
                            {
                                //im join


                            }
                            else if (lstOPSContainerMaster.Where(c => c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderExport).Count() == 0 &&
                                lstOPSContainerAdd.Where(c => !lstTypeOfPacking20.Contains(c.TypeOfPackageID) || c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderImport).Count() == 0)
                            {
                                //ex swap


                            }
                            else if (lstOPSContainerMaster.Where(c => c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderImport).Count() == 0 &&
                                lstOPSContainerAdd.Where(c => !lstTypeOfPacking20.Contains(c.TypeOfPackageID) || c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderExport).Count() == 0)
                            {
                                //im swap


                            }
                        }

                    }
                }


                return result;
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list fail");
        }

        //public create list
        public static List<DTOOPSCOTOMaster> OPSCO_CreateList(DataEntities model, AccountItem account, List<DTOOPSCOTOMaster> lst, HelperTOMaster_COParam param = default(HelperTOMaster_COParam))
        {
            var result = new List<DTOOPSCOTOMaster>();
            if (lst != null && lst.Count > 0)
            {
                for (int i = 0; i < lst.Count; i++)
                {
                    var item = lst[i];
                    if (item.ListCOContainer == null || item.ListCOContainer.Count == 0)
                        throw FaultHelper.BusinessFault(null, null, "item [" + (i + 1) + "] list container null");
                    if (item.ListCOContainer.Where(c => c.LocationFromID < 1 || c.LocationToID < 1).Count() > 0)
                        throw FaultHelper.BusinessFault(null, null, "item [" + (i + 1) + "] location fail");
                    if (item.ListCOContainer.Where(c => c.LocationFromID == c.LocationToID).Count() > 0)
                        throw FaultHelper.BusinessFault(null, null, "location fail");
                    if (item.ListCOContainer.Where(c => c.ETD == null || c.ETA == null).Count() > 0)
                        throw FaultHelper.BusinessFault(null, null, "time fail");
                }

                foreach (var item in lst)
                {
                    result.Add(OPSCO_CreateItem(model, account, item, param));
                }
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list null");

            return result;
        }

        //public create optimize 
        public static List<HelperTOMaster_Error> OPSCO_CreateOptimize(DataEntities model, AccountItem account, int optimizerid, List<int> lstmasterid)
        {
            var result = new List<HelperTOMaster_Error>();

            //Check tocontainer
            foreach (var masterid in lstmasterid)
            {
                var lstTOContainerOPS = model.OPS_OPTCOTOContainer.Where(c => c.OPTCOTOMasterID == masterid && c.OPS_OPTOPSContainer.OPS_COTOContainer.COTOMasterID > 0).Select(c => c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ContainerNo + "_(" + c.OPS_OPTOPSContainer.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.Code + ")").ToList();
                if (lstTOContainerOPS.Count > 0)
                    throw FaultHelper.BusinessFault(null, null, "Có dữ liệu ops. Container: " + string.Join(", ", lstTOContainerOPS));
            }

            var lstMaster = model.OPS_OPTCOTOMaster.Where(c => lstmasterid.Contains(c.ID)).Select(c => new
            {
                c.ID,
                c.VendorOfVehicleID,
                c.VendorOfRomoocID,
                c.VehicleID,
                c.RomoocID,
                c.DriverID1,
                c.DriverID2,
                c.DriverName1,
                c.DriverName2,
                c.DriverTel1,
                c.DriverTel2,
                c.DriverCard1,
                c.DriverCard2,
                c.CAT_Vehicle.GroupOfVehicleID,
                c.KM,
                c.ETA,
                c.ETD,
                c.RoutingID,
                c.DateConfig,
                c.TransportModeID,
                c.ContractID,
                c.TypeOfOrderID
            }).ToList();

            foreach (var objMaster in lstMaster)
            {
                OPS_COTOMaster obj = new OPS_COTOMaster();
                obj.CreatedBy = account.UserName;
                obj.CreatedDate = DateTime.Now;

                obj.SYSCustomerID = account.SYSCustomerID;
                obj.Code = OPSCO_GetLastCode(model);
                obj.VendorOfVehicleID = objMaster.VendorOfVehicleID;
                obj.VendorOfRomoocID = objMaster.VendorOfRomoocID;
                obj.VehicleID = objMaster.VehicleID;
                obj.RomoocID = objMaster.RomoocID;
                obj.DriverID1 = objMaster.DriverID1;
                obj.DriverID2 = objMaster.DriverID2;
                obj.DriverName1 = objMaster.DriverName1;
                obj.DriverName2 = objMaster.DriverName2;
                obj.DriverTel1 = objMaster.DriverTel1;
                obj.DriverTel2 = objMaster.DriverTel2;
                obj.DriverCard1 = objMaster.DriverCard1;
                obj.DriverCard2 = objMaster.DriverCard2;
                obj.ApprovedBy = account.UserName;
                obj.ApprovedDate = DateTime.Now;

                obj.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproved;
                obj.GroupOfVehicleID = objMaster.GroupOfVehicleID;
                obj.SortOrder = 1;
                obj.Note = "Create via optimizer";
                obj.KM = objMaster.KM;
                obj.ETA = objMaster.ETA;
                obj.ETD = objMaster.ETD;
                if (obj.ETD.CompareTo(obj.ETA) >= 0)
                    obj.ETA = obj.ETD.AddHours(0.5);
                obj.RoutingID = objMaster.RoutingID;
                obj.DateConfig = objMaster.DateConfig;
                obj.TransportModeID = objMaster.TransportModeID;
                obj.ContractID = objMaster.ContractID;
                obj.TypeOfOrderID = objMaster.TypeOfOrderID;
                model.OPS_COTOMaster.Add(obj);

                //model.SaveChanges(); 
                var dataLocation = model.OPS_OPTCOTOLocation.Where(c => c.OPTCOTOMasterID == objMaster.ID).OrderBy(c => c.SortOrder).Select(c => new
                {
                    c.ID,
                    c.LocationID,
                    c.DateCome,
                    c.DateLeave,
                    c.SortOrder
                }).ToList();
                //var dataContainer = objMaster.OPS_OPTCOTOContainer.Select(c => new
                //{
                //    c.LocationFromID,
                //    c.LocationToID
                //}).ToList();
                var lstCOTOContainerID = model.OPS_OPTCOTOContainer.Where(c => c.OPTCOTOMasterID == objMaster.ID).Select(c => c.OPS_OPTOPSContainer.COTOContainerID).ToList();
                int locationfromid = -1;
                DateTime? dtFrom = null;
                foreach (var o in dataLocation)
                {
                    if (o.LocationID == null)
                        throw FaultHelper.BusinessFault(null, null, "Dữ liệu location null");

                    OPS_COTOLocation objLocation = new OPS_COTOLocation();
                    objLocation.CreatedBy = account.UserName;
                    objLocation.CreatedDate = DateTime.Now;
                    objLocation.LocationID = o.LocationID;
                    //objLocation.COTOMasterID = obj.ID;
                    objLocation.OPS_COTOMaster = obj;
                    objLocation.SortOrder = o.SortOrder;
                    objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                    objLocation.DateCome = o.DateCome;
                    objLocation.DateLeave = o.DateLeave;
                    objLocation.DateComeEstimate = o.DateCome;
                    objLocation.DateLeaveEstimate = o.DateLeave;
                    //if (dataContainer.Count(c => c.LocationFromID == o.LocationID) > 0 && dataContainer.Count(c => c.LocationToID == o.LocationID) > 0)
                    //    objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationGetDelivery;
                    //else if (dataContainer.Count(c => c.LocationFromID == o.LocationID) > 0)
                    //    objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationGet;
                    //else if (dataContainer.Count(c => c.LocationToID == o.LocationID) > 0)
                    //    objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationDelivery;
                    //else
                    objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                    obj.OPS_COTOLocation.Add(objLocation);

                    if (locationfromid == -1)
                    {
                        locationfromid = objLocation.LocationID.Value;
                        dtFrom = o.DateCome;
                    }
                    else
                    {
                        var objCOTO = new OPS_COTO();
                        objCOTO.CreatedBy = account.UserName;
                        objCOTO.CreatedDate = DateTime.Now;
                        //objCOTO.COTOMasterID = obj.ID;
                        objCOTO.OPS_COTOMaster = obj;
                        objCOTO.IsOPS = true;
                        objCOTO.SortOrder = o.SortOrder - 1;
                        objCOTO.LocationFromID = locationfromid;
                        objCOTO.LocationToID = o.LocationID;
                        objCOTO.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                        objCOTO.ETD = dtFrom;
                        objCOTO.ETA = o.DateCome;
                        model.OPS_COTO.Add(objCOTO);

                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID) && c.LocationFromID == locationfromid && c.LocationToID == o.LocationID.Value))
                        {
                            tocontainer.COTOSort = o.SortOrder - 1;
                            //tocontainer.COTOMasterID = obj.ID;
                            tocontainer.OPS_COTOMaster = obj;
                        }
                        locationfromid = o.LocationID.Value;
                        dtFrom = o.DateLeave;
                    }
                }
                model.SaveChanges();
                //foreach (var o in objMaster.OPS_OPTCOTOContainer.Select(c => c.OPS_OPTOPSContainer.COTOContainerID).ToList())
                //{
                //    var objCo = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == o);
                //    if (objCo != null)
                //    {
                //        objCo.ModifiedBy = account.UserName;
                //        objCo.ModifiedDate = DateTime.Now;
                //        objCo.COTOMasterID = obj.ID;
                //        objCo.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                //        objCo.COTOSort = 1;
                //    }
                //}

                //OPS_CheckingTime(model, objSetting, obj.ID, obj.VehicleID, obj.RomoocID, obj.ETD, obj.ETA, true);
                HelperTimeSheet.Create(model, account, obj.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster, null);
                model.SaveChanges();

                //model.SaveChanges();
            }

            var objOpt = model.OPS_Optimizer.FirstOrDefault(c => c.ID == optimizerid);
            if (objOpt != null)
            {
                //Chuyển trạng thái optimizer: Đã lưu.
                objOpt.ModifiedBy = account.UserName;
                objOpt.ModifiedDate = DateTime.Now;
                objOpt.IsSave = true;
                model.SaveChanges();
            }

            return result;
        }




        //private static void 

        //private static DTOSYSSetting OPSCO_CreateOptimize_SystemSetting(DataEntities model)
        //{
        //    DTOSYSSetting objSetting = new DTOSYSSetting();
        //    var sSet = model.SYS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == SYSSettingKey.System.ToString()).Select(c => c.Setting).FirstOrDefault();
        //    if (!string.IsNullOrEmpty(sSet))
        //    {
        //        objSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(sSet).FirstOrDefault();
        //        if (objSetting != null)
        //        {
        //            var objCheck = model.CAT_Location.FirstOrDefault(c => c.ID == objSetting.LocationFromID);
        //            if (objCheck == null)
        //                objSetting.LocationFromID = model.CAT_Location.FirstOrDefault().ID;
        //            objCheck = model.CAT_Location.FirstOrDefault(c => c.ID == objSetting.LocationToID);
        //            if (objCheck != null)
        //                objSetting.LocationFromID = model.CAT_Location.FirstOrDefault().ID;
        //        }
        //    }
        //    else
        //    {
        //        objSetting.LocationFromID = model.CAT_Location.FirstOrDefault().ID;
        //        objSetting.LocationFromID = model.CAT_Location.FirstOrDefault().ID;
        //    }
        //    return objSetting;
        //}

        //public tendered
        public static List<HelperTOMaster_Error> OPSCO_Tendered(DataEntities model, AccountItem account, List<int> lstmasterid)
        {
            var result = new List<HelperTOMaster_Error>();
            foreach (var masterid in lstmasterid)
            {
                var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
                if (obj != null)
                {
                    if (obj.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterApproved)
                    {
                        if (obj.ETD == null || obj.ETA == null)
                            throw FaultHelper.BusinessFault(null, null, "Thiếu thông tin thời gian ETD hoặc ETA. Không thể duyệt. Chuyến " + obj.Code);
                        if (obj.ETD >= obj.ETA)
                            throw FaultHelper.BusinessFault(null, null, "Sai ràng buộc thời gian ETD - ETA. Không thể duyệt. Chuyến " + obj.Code);
                        if (obj.VendorOfVehicleID == null || obj.VendorOfVehicleID == account.SYSCustomerID)
                        {
                            if (obj.DriverID1 == null || string.IsNullOrEmpty(obj.DriverName1))
                                throw FaultHelper.BusinessFault(null, null, "Thiếu thông tin tài xế. Không thể duyệt. Chuyến " + obj.Code);
                            if (obj.RomoocID == null || obj.RomoocID < 2)
                                throw FaultHelper.BusinessFault(null, null, "Chưa nhập rờ mooc. Không thể duyệt. Chuyến " + obj.Code);
                            if (obj.VehicleID == null || obj.VehicleID <= 2)
                                throw FaultHelper.BusinessFault(null, null, "Chưa nhập xe. Không thể duyệt. Chuyến " + obj.Code);
                        }

                        if (obj.VendorOfVehicleID == null || obj.VendorOfVehicleID == account.SYSCustomerID)
                        {
                            var SDATA = model.OPS_COTOContainer.Where(c => c.COTOMasterID == obj.ID).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
                            OPSCO_Tendered_CheckInspection(model, SDATA);

                            var lstService = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => new { c.OPSContainerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).Distinct().ToList();
                            if (lstService.Count > 0)
                            {
                                int sort = 1;
                                var lstTOContainer = new List<HelperTOMaster_TOContainer>();
                                if ((lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 2 && lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 2) ||
                                    (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 1 && lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 1))
                                {
                                    foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport))
                                    {
                                        lstTOContainer.AddRange(model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID == masterid).Select(c => new HelperTOMaster_TOContainer
                                        {
                                            ID = c.ID,
                                            LocationFromID = c.LocationFromID,
                                            LocationToID = c.LocationToID,
                                            OPSContainerID = c.OPSContainerID,
                                            ETD = c.ETD.Value,
                                            ETA = c.ETA.Value,
                                            SortOrder = sort
                                        }).ToList());
                                    }
                                    sort++;
                                }
                                else if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 2 || lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 2)
                                {
                                    foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport))
                                    {
                                        lstTOContainer.AddRange(model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID == masterid).Select(c => new HelperTOMaster_TOContainer
                                        {
                                            ID = c.ID,
                                            LocationFromID = c.LocationFromID,
                                            LocationToID = c.LocationToID,
                                            OPSContainerID = c.OPSContainerID,
                                            ETD = c.ETD.Value,
                                            ETA = c.ETA.Value,
                                            SortOrder = sort
                                        }).ToList());
                                    }
                                    sort++;
                                }
                                else if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 1 || lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 1)
                                {
                                    foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport))
                                    {
                                        lstTOContainer.AddRange(model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID == masterid).Select(c => new HelperTOMaster_TOContainer
                                        {
                                            ID = c.ID,
                                            LocationFromID = c.LocationFromID,
                                            LocationToID = c.LocationToID,
                                            OPSContainerID = c.OPSContainerID,
                                            ETD = c.ETD.Value,
                                            ETA = c.ETA.Value,
                                            SortOrder = sort
                                        }).ToList());
                                    }
                                    sort++;
                                }

                                if (lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Count() > 0)
                                {
                                    foreach (var service in lstService.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal))
                                    {
                                        lstTOContainer.AddRange(model.OPS_COTOContainer.Where(c => c.OPSContainerID == service.OPSContainerID && c.COTOMasterID == masterid).Select(c => new HelperTOMaster_TOContainer
                                        {
                                            ID = c.ID,
                                            LocationFromID = c.LocationFromID,
                                            LocationToID = c.LocationToID,
                                            OPSContainerID = c.OPSContainerID,
                                            ETD = c.ETD.Value,
                                            ETA = c.ETA.Value,
                                            SortOrder = sort
                                        }).ToList());
                                    }
                                    sort++;
                                }

                                for (int i = 1; i <= sort; i++)
                                {
                                    var first = lstTOContainer.FirstOrDefault(c => c.SortOrder == i);
                                    if (first != null)
                                    {
                                        var objLocation = new OPS_COTOLocation();
                                        objLocation.LocationID = first.LocationToID;
                                        objLocation.SortOrder = i + 1;
                                        objLocation.COTOMasterID = obj.ID;
                                        objLocation.CreatedBy = account.UserName;
                                        objLocation.CreatedDate = DateTime.Now;
                                        objLocation.DateComeEstimate = first.ETD;
                                        objLocation.DateLeaveEstimate = first.ETD;
                                        objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                                        objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                        model.OPS_COTOLocation.Add(objLocation);

                                        if (i == 1)
                                        {
                                            objLocation = new OPS_COTOLocation();
                                            objLocation.LocationID = first.LocationFromID;
                                            objLocation.SortOrder = i;
                                            objLocation.COTOMasterID = obj.ID;
                                            objLocation.CreatedBy = account.UserName;
                                            objLocation.CreatedDate = DateTime.Now;
                                            objLocation.DateComeEstimate = first.ETD;
                                            objLocation.DateLeaveEstimate = first.ETD;
                                            objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                                            objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                            model.OPS_COTOLocation.Add(objLocation);
                                        }

                                        var objTO = new OPS_COTO();
                                        objTO.CreatedBy = account.UserName;
                                        objTO.CreatedDate = DateTime.Now;
                                        objTO.COTOMasterID = obj.ID;
                                        objTO.IsOPS = true;
                                        objTO.SortOrder = i;
                                        objTO.ETD = first.ETD;
                                        objTO.ETA = first.ETA;
                                        objTO.LocationFromID = first.LocationFromID;
                                        objTO.LocationToID = first.LocationToID;
                                        objTO.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                                        model.OPS_COTO.Add(objTO);
                                    }
                                }

                                foreach (var item in lstTOContainer)
                                {
                                    var tocontainer = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item.ID);
                                    if (tocontainer != null)
                                    {
                                        tocontainer.COTOSort = item.SortOrder;
                                        tocontainer.ModifiedBy = account.UserName;
                                        tocontainer.ModifiedDate = DateTime.Now;
                                    }
                                }
                            }
                            //var lstOPSContainerID = lstTOContainer.Select(c => c.OPSContainerID).Distinct().ToList();
                            //OPSCO_TOContainerResetSort(model, account, lstOPSContainerID);

                            //Update COTOMaster  
                            obj.ModifiedBy = account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                            obj.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterTendered;
                            model.SaveChanges();

                            //Update Rate
                            var sOrder = model.OPS_COTORate.Where(c => c.COTOMasterID == obj.ID).OrderByDescending(c => c.SortOrder).Select(c => c.SortOrder).FirstOrDefault();
                            var objRate = model.OPS_COTORate.FirstOrDefault(c => c.COTOMasterID == obj.ID && c.VendorID == obj.VendorOfVehicleID && c.IsSend == true);
                            if (objRate == null)
                            {
                                objRate = new OPS_COTORate();
                                objRate.CreatedBy = account.UserName;
                                objRate.CreatedDate = DateTime.Now;
                                objRate.COTOMasterID = masterid;
                                objRate.VendorID = obj.VendorOfVehicleID;
                                objRate.SortOrder = sOrder > 0 ? sOrder + 1 : 1;
                                objRate.IsSend = true;
                                objRate.Debit = 0;
                                objRate.IsManual = false;
                                objRate.FirstRateTime = DateTime.Now;
                                objRate.LastRateTime = DateTime.Now.Add(TimeSpan.FromHours(obj.RateTime ?? 2));

                                model.OPS_COTORate.Add(objRate);
                            }
                            else
                            {
                                objRate.ModifiedBy = account.UserName;
                                objRate.ModifiedDate = DateTime.Now;
                            }
                            objRate.IsAccept = true;
                            model.SaveChanges();

                            //HelperTimeSheet.Create(model, account, obj.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                            HelperStatus.ORDOrder_Status(model, account, SDATA);
                        }
                        else
                        {
                            var SDATA = model.OPS_COTOContainer.Where(c => c.COTOMasterID == obj.ID).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
                            OPSCO_Tendered_CheckInspection(model, SDATA);

                            //Update COTOMaster  
                            obj.ModifiedBy = account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                            obj.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterTendered;
                            model.SaveChanges();

                            //Update Rate
                            var sOrder = model.OPS_COTORate.Where(c => c.COTOMasterID == obj.ID).OrderByDescending(c => c.SortOrder).Select(c => c.SortOrder).FirstOrDefault();
                            var objRate = model.OPS_COTORate.FirstOrDefault(c => c.COTOMasterID == obj.ID && c.VendorID == obj.VendorOfVehicleID && c.IsSend == true);
                            if (objRate == null)
                            {
                                objRate = new OPS_COTORate();
                                objRate.CreatedBy = account.UserName;
                                objRate.CreatedDate = DateTime.Now;
                                objRate.COTOMasterID = masterid;
                                objRate.VendorID = obj.VendorOfVehicleID;
                                objRate.SortOrder = sOrder > 0 ? sOrder + 1 : 1;
                                objRate.IsSend = true;
                                objRate.Debit = 0;
                                objRate.IsManual = false;
                                objRate.FirstRateTime = DateTime.Now;
                                objRate.LastRateTime = DateTime.Now.Add(TimeSpan.FromHours(obj.RateTime ?? 2));

                                model.OPS_COTORate.Add(objRate);
                            }
                            else
                            {
                                objRate.ModifiedBy = account.UserName;
                                objRate.ModifiedDate = DateTime.Now;
                            }
                            objRate.IsAccept = true;
                            model.SaveChanges();


                            HelperTimeSheet.Create(model, account, obj.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);

                            HelperStatus.ORDOrder_Status(model, account, SDATA);
                        }


                        //    if (obj.ETD == null || obj.ETA == null)
                        //        throw FaultHelper.BusinessFault(null, null, "Thiếu thông tin thời gian ETD hoặc ETA. Không thể duyệt. Chuyến " + obj.Code);
                        //    if (obj.ETD >= obj.ETA)
                        //        throw FaultHelper.BusinessFault(null, null, "Sai ràng buộc thời gian ETD - ETA. Không thể duyệt. Chuyến " + obj.Code);
                        //    if (obj.VendorOfVehicleID == null || obj.VendorOfVehicleID == account.SYSCustomerID)
                        //    {
                        //        if (obj.VehicleID == null || obj.RomoocID == null || obj.VehicleID <= 2 || obj.RomoocID < 2)
                        //            throw FaultHelper.BusinessFault(null, null, "Không thể duyệt! Nhập thông tin đầu kéo hoặc romooc. Chuyến " + obj.Code);
                        //        if ((obj.VendorOfVehicleID == null || obj.VendorOfVehicleID == account.SYSCustomerID) && string.IsNullOrEmpty(obj.DriverName1))
                        //            throw FaultHelper.BusinessFault(null, null, "Thiếu thông tin tài xế. Không thể duyệt. Chuyến " + obj.Code);
                        //        if (obj.VehicleID == null || obj.VehicleID <= 2 || obj.RomoocID < 2)
                        //            throw FaultHelper.BusinessFault(null, null, "Chưa nhập xe. Không thể duyệt. Chuyến " + obj.Code);

                        //    }
                        //    var SDATA = model.OPS_COTOContainer.Where(c => c.COTOMasterID == obj.ID).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
                        //    OPSCO_Tendered_CheckInspection(model, SDATA);

                        //    if (obj.VendorOfVehicleID == null || obj.VendorOfVehicleID == account.SYSCustomerID)
                        //    {
                        //        var setting = OPSCO_GetSetting(model, account);
                        //        var param = new HelperTOMaster_COParam()
                        //        {
                        //            LocationFromID = setting.LocationFromID,
                        //            LocationToID = setting.LocationToID
                        //        };
                        //        var lstTOContainer = OPSCO_CreateTOContainer(model, account, masterid, param);
                        //        var lstLocation = OPSCO_CreateLocation(lstTOContainer);
                        //        var lstCONAdd = new List<HelperTOMaster_TOContainer>();
                        //        for (int i = 0; i < lstLocation.Count; i++)
                        //        {
                        //            var itemLocation = lstLocation[i];

                        //            var objLocation = new OPS_COTOLocation();
                        //            objLocation.LocationID = itemLocation.LocationID;
                        //            objLocation.SortOrder = i + 1;
                        //            objLocation.COTOMasterID = obj.ID;
                        //            objLocation.CreatedBy = account.UserName;
                        //            objLocation.CreatedDate = DateTime.Now;
                        //            objLocation.DateComeEstimate = itemLocation.DateComeEstimate;
                        //            objLocation.DateLeaveEstimate = itemLocation.DateLeaveEstimate;
                        //            objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                        //            objLocation.TypeOfTOLocationID = itemLocation.TypeOfTOLocationID > 0 ? itemLocation.TypeOfTOLocationID : -(int)SYSVarType.TypeOfTOLocationEmpty;
                        //            model.OPS_COTOLocation.Add(objLocation);

                        //            if (i > 0)
                        //            {
                        //                var objTO = new OPS_COTO();
                        //                objTO.CreatedBy = account.UserName;
                        //                objTO.CreatedDate = DateTime.Now;
                        //                objTO.COTOMasterID = obj.ID;
                        //                objTO.IsOPS = true;
                        //                objTO.SortOrder = i;
                        //                objTO.ETD = lstLocation[i - 1].DateComeEstimate;
                        //                objTO.ETA = lstLocation[i].DateComeEstimate;
                        //                objTO.LocationFromID = lstLocation[i - 1].LocationID;
                        //                objTO.LocationToID = lstLocation[i].LocationID;
                        //                objTO.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                        //                model.OPS_COTO.Add(objTO);

                        //                if (itemLocation.ListTOContainer != null && itemLocation.ListTOContainer.Where(c => c.LocationToID > 0).Count() > 0)
                        //                    lstCONAdd.AddRange(itemLocation.ListTOContainer.Where(c => c.LocationToID > 0).ToList());
                        //            }
                        //        }
                        //        var lstTOContainerID = lstTOContainer.Select(c => c.ID).ToList();
                        //        foreach (var itemCONID in lstTOContainerID)
                        //        {
                        //            var lst = lstCONAdd.Where(c => c.ParentID == itemCONID).ToList();
                        //            if (lst.Count > 1)
                        //            {
                        //                var itemCON = lstTOContainer.FirstOrDefault(c => c.ID == itemCONID);
                        //                if (itemCON != null)
                        //                {
                        //                    itemCON.IsSplit = true;
                        //                    lst[lst.Count - 1].StatusOfCOContainerID = itemCON.StatusOfCOContainerID;
                        //                    lstTOContainer.AddRange(lst);
                        //                }
                        //            }
                        //        }
                        //        OPSCO_CreateMaster_CON(model, account, masterid, null, null, lstTOContainer);

                        //        var lstOPSContainerID = lstTOContainer.Select(c => c.OPSContainerID).Distinct().ToList();
                        //        OPSCO_TOContainerResetSort(model, account, lstOPSContainerID);
                        //    }

                        //    //Update COTOMaster  
                        //    obj.ModifiedBy = account.UserName;
                        //    obj.ModifiedDate = DateTime.Now;
                        //    obj.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterTendered;
                        //    model.SaveChanges();



                        //    //Update Rate
                        //    var sOrder = model.OPS_COTORate.Where(c => c.COTOMasterID == obj.ID).OrderByDescending(c => c.SortOrder).Select(c => c.SortOrder).FirstOrDefault();
                        //    var objRate = model.OPS_COTORate.FirstOrDefault(c => c.COTOMasterID == obj.ID && c.VendorID == obj.VendorOfVehicleID && c.IsSend == true);
                        //    if (objRate == null)
                        //    {
                        //        objRate = new OPS_COTORate();
                        //        objRate.CreatedBy = account.UserName;
                        //        objRate.CreatedDate = DateTime.Now;
                        //        objRate.COTOMasterID = masterid;
                        //        objRate.VendorID = obj.VendorOfVehicleID;
                        //        objRate.SortOrder = sOrder > 0 ? sOrder + 1 : 1;
                        //        objRate.IsSend = true;
                        //        objRate.Debit = 0;
                        //        objRate.IsManual = false;
                        //        objRate.FirstRateTime = DateTime.Now;
                        //        objRate.LastRateTime = DateTime.Now.Add(TimeSpan.FromHours(obj.RateTime ?? 2));

                        //        model.OPS_COTORate.Add(objRate);
                        //    }
                        //    else
                        //    {
                        //        objRate.ModifiedBy = account.UserName;
                        //        objRate.ModifiedDate = DateTime.Now;
                        //    }
                        //    objRate.IsAccept = true;
                        //    model.SaveChanges();


                        //    HelperTimeSheet.Create(model, account, obj.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);

                        //    HelperStatus.ORDOrder_Status(model, account, SDATA);
                    }
                    else
                    {
                        throw FaultHelper.BusinessFault(null, null, "Không thể duyệt! Chỉ được duyệt các chuyến đang lập kế hoạch.");
                    }
                }
            }
            return result;
        }

        private static List<HelperTOMaster_TOContainer> OPSCO_Tendered_CreateTOContainer(DataEntities model, AccountItem account, int masterid)
        {
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };

            var result = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false && !statusload.Contains(c.StatusOfCOContainerID)).Select(c => new HelperTOMaster_TOContainer
            {
                ID = c.ID,
                ParentID = c.ParentID,
                OPSContainerID = c.OPSContainerID,
                LocationFromID = c.LocationFromID,
                LocationToID = c.LocationToID,
                ETD = c.ETD.Value,
                ETA = c.ETA.Value,
                ETDStart = c.ETDStart,
                ETAStart = c.ETAStart,
                SortOrder = c.SortOrder,
                StatusOfCOContainerID = c.StatusOfCOContainerID,
                ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID > 0 ? c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID : -1,
                CATTransportModeID = c.OPS_Container.ORD_Container.ORD_Order.TransportModeID,
                IsSplit = c.IsSplit,
                IsCreate = false,
                HasCOTO = false
            }).ToList();

            return result;

            ////Create start 
            //var lstCheckStatusID = new List<int>() { -(int)SYSVarType.StatusOfCOContainerIMLaden, -(int)SYSVarType.StatusOfCOContainerEXEmpty, -(int)SYSVarType.StatusOfCOContainerLOGetEmpty, -(int)SYSVarType.StatusOfCOContainerLOEmpty, -(int)SYSVarType.StatusOfCOContainerLOLaden };
            //var lstParentNULL = result.Where(c => c.ParentID == null && lstCheckStatusID.Contains(c.StatusOfCOContainerID) && c.IsSplit == false).ToList();
            //foreach (var parentNULL in lstParentNULL)
            //{
            //    parentNULL.IsSplit = true;
            //    var totalHour = (parentNULL.ETA - parentNULL.ETD).TotalHours;
            //    var subHour = totalHour * percentSubRoute;
            //    var mainHour = totalHour - subHour;
            //    var start = parentNULL.ETD;
            //    switch (parentNULL.StatusOfCOContainerID)
            //    {
            //        case -(int)SYSVarType.StatusOfCOContainerIMLaden:
            //            result.Add(new HelperTOMaster_TOContainer
            //            {
            //                ID = -result.Count,
            //                ParentID = parentNULL.ID,
            //                OPSContainerID = parentNULL.OPSContainerID,
            //                LocationFromID = param.LocationFromID.Value,
            //                LocationToID = parentNULL.LocationFromID,
            //                ETD = start,
            //                ETA = start.AddHours(subHour),
            //                SortOrder = 1,
            //                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetLaden,
            //                TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
            //                IsSplit = false,
            //                IsCreate = true,
            //                HasCOTO = false
            //            });
            //            start = start.AddHours(subHour);
            //            result.Add(new HelperTOMaster_TOContainer
            //            {
            //                ID = -result.Count,
            //                ParentID = parentNULL.ID,
            //                OPSContainerID = parentNULL.OPSContainerID,
            //                LocationFromID = parentNULL.LocationFromID,
            //                LocationToID = parentNULL.LocationToID,
            //                ETD = start,
            //                ETA = start.AddHours(mainHour),
            //                SortOrder = 2,
            //                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipLaden,
            //                TypeOfTOLocation = SYSVarType.TypeOfTOLocationStock,
            //                IsSplit = false,
            //                IsCreate = true,
            //                HasCOTO = false
            //            });
            //            break;
            //        case -(int)SYSVarType.StatusOfCOContainerEXEmpty:
            //        case -(int)SYSVarType.StatusOfCOContainerLOGetEmpty:
            //            result.Add(new HelperTOMaster_TOContainer
            //            {
            //                ID = -result.Count,
            //                ParentID = parentNULL.ID,
            //                OPSContainerID = parentNULL.OPSContainerID,
            //                LocationFromID = param.LocationFromID.Value,
            //                LocationToID = parentNULL.LocationFromID,
            //                ETD = start,
            //                ETA = start.AddHours(subHour),
            //                SortOrder = 1,
            //                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetEmpty,
            //                TypeOfTOLocation = SYSVarType.TypeOfTOLocationDepot,
            //                IsSplit = false,
            //                IsCreate = true,
            //                HasCOTO = false
            //            });
            //            start = start.AddHours(subHour);
            //            result.Add(new HelperTOMaster_TOContainer
            //            {
            //                ID = -result.Count,
            //                ParentID = parentNULL.ID,
            //                OPSContainerID = parentNULL.OPSContainerID,
            //                LocationFromID = parentNULL.LocationFromID,
            //                LocationToID = parentNULL.LocationToID,
            //                ETD = start,
            //                ETA = start.AddHours(mainHour),
            //                SortOrder = 2,
            //                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
            //                TypeOfTOLocation = SYSVarType.TypeOfTOLocationStock,
            //                IsSplit = false,
            //                IsCreate = true,
            //                HasCOTO = false
            //            });
            //            break;
            //        case -(int)SYSVarType.StatusOfCOContainerLOEmpty:
            //            result.Add(new HelperTOMaster_TOContainer
            //            {
            //                ID = -result.Count,
            //                ParentID = parentNULL.ID,
            //                OPSContainerID = parentNULL.OPSContainerID,
            //                LocationFromID = param.LocationFromID.Value,
            //                LocationToID = parentNULL.LocationFromID,
            //                ETD = start,
            //                ETA = start.AddHours(subHour),
            //                SortOrder = 1,
            //                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetEmpty,
            //                TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
            //                IsSplit = false,
            //                IsCreate = true,
            //                HasCOTO = false
            //            });
            //            start = start.AddHours(subHour);
            //            result.Add(new HelperTOMaster_TOContainer
            //            {
            //                ID = -result.Count,
            //                ParentID = parentNULL.ID,
            //                OPSContainerID = parentNULL.OPSContainerID,
            //                LocationFromID = parentNULL.LocationFromID,
            //                LocationToID = parentNULL.LocationToID,
            //                ETD = start,
            //                ETA = start.AddHours(mainHour),
            //                SortOrder = 2,
            //                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
            //                TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
            //                IsSplit = false,
            //                IsCreate = true,
            //                HasCOTO = false
            //            });
            //            break;
            //        case -(int)SYSVarType.StatusOfCOContainerLOLaden:
            //            if (parentNULL.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocalLaden)
            //            {
            //                result.Add(new HelperTOMaster_TOContainer
            //                {
            //                    ID = -result.Count,
            //                    ParentID = parentNULL.ID,
            //                    OPSContainerID = parentNULL.OPSContainerID,
            //                    LocationFromID = param.LocationFromID.Value,
            //                    LocationToID = parentNULL.LocationFromID,
            //                    ETD = start,
            //                    ETA = start.AddHours(subHour),
            //                    SortOrder = 1,
            //                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetLaden,
            //                    TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
            //                    IsSplit = false,
            //                    IsCreate = true,
            //                    HasCOTO = false
            //                });
            //                start = start.AddHours(subHour);
            //                result.Add(new HelperTOMaster_TOContainer
            //                {
            //                    ID = -result.Count,
            //                    ParentID = parentNULL.ID,
            //                    OPSContainerID = parentNULL.OPSContainerID,
            //                    LocationFromID = parentNULL.LocationFromID,
            //                    LocationToID = parentNULL.LocationToID,
            //                    ETD = start,
            //                    ETA = start.AddHours(mainHour),
            //                    SortOrder = 2,
            //                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipLaden,
            //                    TypeOfTOLocation = SYSVarType.TypeOfTOLocationPort,
            //                    IsSplit = false,
            //                    IsCreate = true,
            //                    HasCOTO = false
            //                });
            //            }
            //            else
            //                parentNULL.IsSplit = false;
            //            break;
            //    }
            //}

            ////Create end
            //lstCheckStatusID = new List<int>() { -(int)SYSVarType.StatusOfCOContainerIMEmpty, -(int)SYSVarType.StatusOfCOContainerEXLaden, -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty };
            //lstParentNULL = result.Where(c => c.ParentID == null && lstCheckStatusID.Contains(c.StatusOfCOContainerID) && c.IsSplit == false).ToList();
            //foreach (var parentNULL in lstParentNULL)
            //{
            //    var totalHour = (parentNULL.ETA - parentNULL.ETD).TotalHours;
            //    var subHour = totalHour * percentSubRoute;
            //    var mainHour = totalHour - subHour;
            //    var start = parentNULL.ETA;

            //    if (parentNULL.LocationToID != param.LocationToID)
            //    {
            //        result.Add(new HelperTOMaster_TOContainer
            //        {
            //            ID = -result.Count,
            //            ParentID = null,
            //            OPSContainerID = parentNULL.OPSContainerID,
            //            LocationFromID = parentNULL.LocationToID,
            //            LocationToID = param.LocationToID.Value,
            //            ETD = start,
            //            ETA = start.AddHours(subHour),
            //            SortOrder = parentNULL.SortOrder + 1,
            //            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerReturnStation,
            //            TypeOfTOLocation = SYSVarType.TypeOfTOLocationEmpty,
            //            IsSplit = false,
            //            IsCreate = true,
            //            HasCOTO = false
            //        });
            //    }
            //}
        }

        private static List<HelperTOMaster_TOLocation> OPSCO_Tendered_CreateLocation(List<HelperTOMaster_TOContainer> lst)
        {
            //Change sort 
            var result = new List<HelperTOMaster_TOLocation>();

            //var lstOPSContainerID = lst.Where(c => c.IsSplit == false).Select(c => c.OPSContainerID).Distinct().ToList();
            //var dicOPSContainer = new Dictionary<int, int>();
            //var getmoocSort = -1;
            //var returnmoocSort = -1;
            //foreach (var opscontainerid in lstOPSContainerID)
            //{
            //    var sort = 1;
            //    var lstChange = lst.Where(c => c.IsSplit == false && c.OPSContainerID == opscontainerid).OrderBy(c => c.SortOrder).ToList();
            //    foreach (var tocontainer in lstChange)
            //    {
            //        tocontainer.SortLocation = sort++;
            //        if (!dicOPSContainer.ContainsKey(tocontainer.OPSContainerID))
            //            dicOPSContainer.Add(tocontainer.OPSContainerID, tocontainer.SortLocation);
            //        else
            //            dicOPSContainer[tocontainer.OPSContainerID] = tocontainer.SortLocation;
            //    }
            //    var con = lst.FirstOrDefault(c => c.OPSContainerID == opscontainerid && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerGetRomooc);
            //    if (con != null)
            //    {
            //        if (getmoocSort == -1)
            //            getmoocSort = con.SortLocation;
            //        else if (getmoocSort != con.SortLocation)
            //            throw FaultHelper.BusinessFault(null, null, "location get mooc different");
            //    }
            //    con = lst.FirstOrDefault(c => c.OPSContainerID == opscontainerid && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerReturnRomooc);
            //    if (con != null)
            //    {
            //        if (returnmoocSort == -1)
            //            returnmoocSort = con.SortLocation;
            //        else if (returnmoocSort != con.SortLocation)
            //            throw FaultHelper.BusinessFault(null, null, "location return mooc different");
            //    }
            //}

            //var nextCurrent = default(HelperTOMaster_TOLocation);
            //var typeoftolocationid = -1;
            //int i = 1;
            //for (i = 1; i < 12; i++)
            //{
            //    //Get first location in sort
            //    var first = lst.Where(c => c.SortLocation == i).OrderBy(c => c.ETD).FirstOrDefault();
            //    if (first != null)
            //    {
            //        var itemFirst = new HelperTOMaster_TOLocation()
            //        {
            //            LocationID = first.LocationFromID,
            //            DateComeEstimate = first.ETD,
            //            DateLeaveEstimate = first.ETD,
            //            TypeOfTOLocationID = typeoftolocationid
            //        };
            //        if (nextCurrent != null && itemFirst.LocationID == nextCurrent.LocationID)
            //        {
            //            throw FaultHelper.BusinessFault(null, null, "location fail");
            //        }
            //        typeoftolocationid = -(int)first.TypeOfTOLocation;
            //        result.Add(itemFirst);
            //        if (lst.Where(c => c.SortLocation == i).Count() > 1)
            //        {
            //            DateTime datecomeStart = itemFirst.DateComeEstimate.AddHours(-MaxHourSamePoint);
            //            DateTime datecomeEnd = itemFirst.DateComeEstimate.AddHours(MaxHourSamePoint);
            //            if (lst.Where(c => c.LocationFromID == first.LocationFromID &&
            //                c.ETD < datecomeStart && c.ETD > datecomeEnd && c.SortLocation == i && c.ID != first.ID).Count() > 0)
            //                throw FaultHelper.BusinessFault(null, null, "location time fail");
            //            else
            //            {
            //                //Add first location map
            //                itemFirst.ListTOContainer = new List<HelperTOMaster_TOContainer>();
            //                if (nextCurrent != null)
            //                {
            //                    foreach (var itemAdd in nextCurrent.ListTOContainer.Where(c => c.LocationToID == -1))
            //                    {
            //                        itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                        {
            //                            OPSContainerID = itemAdd.OPSContainerID,
            //                            ParentID = itemAdd.ID,
            //                            LocationFromID = itemAdd.LocationFromID,
            //                            LocationToID = first.LocationFromID,
            //                            SortOrder = i,
            //                            SortLocation = itemAdd.SortLocation,
            //                            StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
            //                            IsSplit = false,
            //                            IsCreate = true
            //                        });
            //                        if (dicOPSContainer[itemAdd.OPSContainerID] >= itemAdd.SortLocation)
            //                        {
            //                            itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                            {
            //                                OPSContainerID = itemAdd.OPSContainerID,
            //                                ParentID = itemAdd.ID,
            //                                LocationFromID = first.LocationFromID,
            //                                LocationToID = -1,
            //                                SortOrder = i,
            //                                SortLocation = itemAdd.SortLocation,
            //                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
            //                                IsSplit = false,
            //                                IsCreate = true
            //                            });
            //                        }
            //                    }
            //                }
            //                foreach (var itemAdd in lst.Where(c => c.LocationFromID == first.LocationFromID &&
            //                    c.ETD < datecomeStart && c.ETD > datecomeEnd && c.SortLocation == i))
            //                {
            //                    itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                    {
            //                        OPSContainerID = itemAdd.OPSContainerID,
            //                        ParentID = itemAdd.ID,
            //                        LocationFromID = itemAdd.LocationFromID,
            //                        LocationToID = -1,
            //                        SortOrder = i,
            //                        SortLocation = itemAdd.SortLocation,
            //                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
            //                        IsSplit = false,
            //                        IsCreate = true
            //                    });
            //                }
            //                //Defind current
            //                nextCurrent = itemFirst;

            //                if (lst.Where(c => c.LocationFromID != first.LocationFromID && c.ETD >= first.ETD && c.ETD <= first.ETA &&
            //                    c.SortLocation == i).Count() > 0)
            //                {

            //                    //Get next location
            //                    foreach (var locationid in lst.Where(c => c.LocationFromID != first.LocationFromID && c.ETD >= first.ETD && c.ETD <= first.ETA &&
            //                        c.SortLocation == i).OrderBy(c => c.ETD).Select(c => c.LocationFromID).Distinct())
            //                    {
            //                        var next = lst.Where(c => c.LocationFromID == locationid && c.SortLocation == i).OrderBy(c => c.ETD).FirstOrDefault();
            //                        if (next != null)
            //                        {
            //                            var itemNext = new HelperTOMaster_TOLocation()
            //                            {
            //                                LocationID = next.LocationFromID,
            //                                DateComeEstimate = next.ETD,
            //                                DateLeaveEstimate = next.ETD,
            //                                TypeOfTOLocationID = typeoftolocationid
            //                            };
            //                            typeoftolocationid = -(int)next.TypeOfTOLocation;
            //                            result.Add(itemNext);
            //                            datecomeStart = itemNext.DateComeEstimate.AddHours(-MaxHourSamePoint);
            //                            datecomeEnd = itemNext.DateComeEstimate.AddHours(MaxHourSamePoint);
            //                            if (lst.Where(c => c.LocationFromID == next.LocationFromID &&
            //                                c.ETD < datecomeStart && c.ETD > datecomeEnd && c.SortLocation == i && c.ID != next.ID).Count() > 0)
            //                                throw FaultHelper.BusinessFault(null, null, "location time fail");
            //                            else
            //                            {
            //                                //Add next old location map
            //                                itemNext.ListTOContainer = new List<HelperTOMaster_TOContainer>();
            //                                foreach (var itemAdd in nextCurrent.ListTOContainer.Where(c => c.LocationToID == -1))
            //                                {
            //                                    itemNext.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                                    {
            //                                        OPSContainerID = itemAdd.OPSContainerID,
            //                                        ParentID = itemAdd.ID,
            //                                        LocationFromID = itemAdd.LocationFromID,
            //                                        LocationToID = next.LocationFromID,
            //                                        SortOrder = i,
            //                                        SortLocation = itemAdd.SortLocation,
            //                                        StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
            //                                        IsSplit = false,
            //                                        IsCreate = true
            //                                    });
            //                                    if (dicOPSContainer[itemAdd.OPSContainerID] >= itemAdd.SortLocation)
            //                                    {
            //                                        itemNext.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                                        {
            //                                            OPSContainerID = itemAdd.OPSContainerID,
            //                                            ParentID = itemAdd.ID,
            //                                            LocationFromID = next.LocationFromID,
            //                                            LocationToID = -1,
            //                                            SortOrder = i,
            //                                            SortLocation = itemAdd.SortLocation,
            //                                            StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
            //                                            IsSplit = false,
            //                                            IsCreate = true
            //                                        });
            //                                    }
            //                                }
            //                                //Add next new location map
            //                                foreach (var itemAdd in lst.Where(c => c.LocationFromID == next.LocationFromID &&
            //                                    c.ETD < datecomeStart && c.ETD > datecomeEnd && c.SortLocation == i))
            //                                {
            //                                    itemNext.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                                    {
            //                                        OPSContainerID = itemAdd.OPSContainerID,
            //                                        ParentID = itemAdd.ID,
            //                                        LocationFromID = itemAdd.LocationFromID,
            //                                        LocationToID = -1,
            //                                        SortOrder = i,
            //                                        SortLocation = itemAdd.SortLocation,
            //                                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
            //                                        IsSplit = false,
            //                                        IsCreate = true
            //                                    });
            //                                }
            //                                //Defind current
            //                                nextCurrent = itemNext;
            //                            }
            //                        }
            //                    }
            //                }

            //                //Remove item not in this sort
            //                foreach (var itemnotin in lst.Where(c => c.LocationFromID != first.LocationFromID && c.ETD > first.ETA &&
            //                    c.SortLocation == i).OrderBy(c => c.ETD))
            //                {
            //                    itemnotin.SortLocation++;
            //                }
            //            }
            //        }
            //        else
            //        {
            //            itemFirst.ListTOContainer = new List<HelperTOMaster_TOContainer>();
            //            foreach (var itemAdd in lst.Where(c => c.LocationFromID == first.LocationFromID && c.SortLocation == i))
            //            {
            //                itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                {
            //                    OPSContainerID = itemAdd.OPSContainerID,
            //                    ParentID = itemAdd.ID,
            //                    LocationFromID = itemAdd.LocationFromID,
            //                    LocationToID = first.LocationFromID,
            //                    SortOrder = i,
            //                    SortLocation = itemAdd.SortLocation,
            //                    StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
            //                    IsSplit = false,
            //                    IsCreate = true
            //                });
            //            }
            //        }
            //    }
            //    else
            //        break;
            //}
            //i--;
            //if (i > 1)
            //{
            //    //Get first location in sort
            //    var first = lst.Where(c => c.SortLocation == i).OrderBy(c => c.ETA).FirstOrDefault();
            //    if (first != null)
            //    {
            //        var itemFirst = new HelperTOMaster_TOLocation()
            //        {
            //            LocationID = first.LocationToID,
            //            DateComeEstimate = first.ETA,
            //            DateLeaveEstimate = first.ETA,
            //            TypeOfTOLocationID = typeoftolocationid
            //        };
            //        typeoftolocationid = -(int)first.TypeOfTOLocation;
            //        result.Add(itemFirst);
            //        if (lst.Where(c => c.SortLocation == i).Count() > 1)
            //        {
            //            DateTime datecomeStart = itemFirst.DateComeEstimate.AddHours(-MaxHourSamePoint);
            //            DateTime datecomeEnd = itemFirst.DateComeEstimate.AddHours(MaxHourSamePoint);
            //            if (lst.Where(c => c.LocationToID == first.LocationToID &&
            //                c.ETA < datecomeStart && c.ETA > datecomeEnd && c.SortLocation == i && c.ID != first.ID).Count() > 0)
            //                throw FaultHelper.BusinessFault(null, null, "location time fail");
            //            else
            //            {
            //                if (nextCurrent != null)
            //                {
            //                    foreach (var itemAdd in nextCurrent.ListTOContainer.Where(c => c.LocationToID == -1))
            //                    {
            //                        itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                        {
            //                            OPSContainerID = itemAdd.OPSContainerID,
            //                            ParentID = itemAdd.ID,
            //                            LocationFromID = itemAdd.LocationFromID,
            //                            LocationToID = first.LocationToID,
            //                            SortOrder = i,
            //                            SortLocation = itemAdd.SortLocation,
            //                            StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
            //                            IsSplit = false,
            //                            IsCreate = true
            //                        });

            //                        if (itemAdd.LocationToID != first.LocationToID)
            //                        {
            //                            itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                            {
            //                                OPSContainerID = itemAdd.OPSContainerID,
            //                                ParentID = itemAdd.ID,
            //                                LocationFromID = first.LocationToID,
            //                                LocationToID = -1,
            //                                SortOrder = i,
            //                                SortLocation = itemAdd.SortLocation,
            //                                StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
            //                                IsSplit = false,
            //                                IsCreate = true
            //                            });
            //                        }
            //                    }
            //                    //Defind current
            //                    nextCurrent = itemFirst;

            //                    if (lst.Where(c => c.LocationToID != first.LocationToID && c.SortLocation == i).Count() > 0)
            //                    {
            //                        //Get next location
            //                        foreach (var locationid in lst.Where(c => c.LocationToID != first.LocationToID && c.SortLocation == i).OrderBy(c => c.ETA).Select(c => c.LocationToID).Distinct())
            //                        {
            //                            var next = lst.Where(c => c.LocationToID == locationid && c.SortLocation == i).OrderBy(c => c.ETA).FirstOrDefault();
            //                            if (next != null)
            //                            {
            //                                var itemNext = new HelperTOMaster_TOLocation()
            //                                {
            //                                    LocationID = next.LocationToID,
            //                                    DateComeEstimate = next.ETA,
            //                                    DateLeaveEstimate = next.ETA,
            //                                    TypeOfTOLocationID = typeoftolocationid
            //                                };
            //                                typeoftolocationid = -(int)first.TypeOfTOLocation;
            //                                result.Add(itemNext);
            //                                datecomeStart = itemNext.DateComeEstimate.AddHours(-MaxHourSamePoint);
            //                                datecomeEnd = itemNext.DateComeEstimate.AddHours(MaxHourSamePoint);
            //                                if (lst.Where(c => c.LocationToID == next.LocationToID &&
            //                                    c.ETA < datecomeStart && c.ETA > datecomeEnd && c.SortLocation == i && c.ID != next.ID).Count() > 0)
            //                                    throw FaultHelper.BusinessFault(null, null, "location time fail");
            //                                else
            //                                {
            //                                    //Add next old location map
            //                                    itemNext.ListTOContainer = new List<HelperTOMaster_TOContainer>();
            //                                    foreach (var itemAdd in nextCurrent.ListTOContainer.Where(c => c.LocationToID == -1))
            //                                    {
            //                                        itemNext.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                                        {
            //                                            OPSContainerID = itemAdd.OPSContainerID,
            //                                            ParentID = itemAdd.ID,
            //                                            LocationFromID = itemAdd.LocationFromID,
            //                                            LocationToID = next.LocationToID,
            //                                            SortOrder = i,
            //                                            SortLocation = itemAdd.SortLocation,
            //                                            StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
            //                                            IsSplit = false,
            //                                            IsCreate = true
            //                                        });
            //                                        if (itemAdd.LocationToID != first.LocationToID)
            //                                        {
            //                                            itemFirst.ListTOContainer.Add(new HelperTOMaster_TOContainer()
            //                                            {
            //                                                OPSContainerID = itemAdd.OPSContainerID,
            //                                                ParentID = itemAdd.ID,
            //                                                LocationFromID = first.LocationToID,
            //                                                LocationToID = -1,
            //                                                SortOrder = i,
            //                                                SortLocation = itemAdd.SortLocation,
            //                                                StatusOfCOContainerID = itemAdd.StatusOfCOContainerID,
            //                                                IsSplit = false,
            //                                                IsCreate = true
            //                                            });
            //                                        }
            //                                    }
            //                                    //Defind current
            //                                    nextCurrent = itemNext;
            //                                }
            //                            }
            //                        }
            //                    }
            //                }
            //            }
            //        }
            //    }
            //}

            ////Check time location
            //DateTime? dtCheck = null;
            //for (i = 0; i < result.Count; i++)
            //{
            //    var itemLocation = result[i];
            //    if (dtCheck != null)
            //    {
            //        if (itemLocation.DateComeEstimate.CompareTo(dtCheck.Value) <= 0)
            //            throw FaultHelper.BusinessFault(null, null, "create location " + (i + 1) + " time fail");
            //    }
            //    dtCheck = itemLocation.DateComeEstimate;
            //    if (i > 0)
            //    {
            //        if (itemLocation.LocationID == result[i - 1].LocationID)
            //            throw FaultHelper.BusinessFault(null, null, "create location " + (i + 1) + " same fail");
            //    }
            //}

            int sort = 1;
            var tocontainerfirst = lst.OrderBy(c => c.ETD).FirstOrDefault();
            if (tocontainerfirst != null)
            {
                DateTime etd = tocontainerfirst.ETD;
                var dtStart = etd.AddHours(-HourMin);
                var dtEnd = etd.AddHours(HourMin);
                var typeoftolocationid = -(int)SYSVarType.TypeOfTOLocationEmpty;

                var itemLocation = new HelperTOMaster_TOLocation()
                {
                    LocationID = tocontainerfirst.LocationFromID,
                    DateComeEstimate = tocontainerfirst.ETD,
                    DateLeaveEstimate = tocontainerfirst.ETD,
                    TypeOfTOLocationID = typeoftolocationid,
                };
                result.Add(itemLocation);
                sort++;

                foreach (var tocontainer in lst.OrderBy(c => c.ETD))
                {
                    if (tocontainer.LocationToID == itemLocation.LocationID)
                    {
                        itemLocation.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                        {
                            ID = tocontainer.ID,
                            OPSContainerID = tocontainer.OPSContainerID,
                            ParentID = tocontainer.ID,
                            LocationFromID = tocontainer.LocationFromID,
                            LocationToID = tocontainer.LocationToID,
                            SortOrder = tocontainer.SortOrder,
                            SortLocation = sort,
                            StatusOfCOContainerID = tocontainer.StatusOfCOContainerID,
                            IsSplit = false,
                            IsCreate = true
                        });
                    }
                    else
                    {
                        itemLocation = new HelperTOMaster_TOLocation()
                        {
                            LocationID = tocontainer.LocationToID,
                            DateComeEstimate = tocontainer.ETD,
                            DateLeaveEstimate = tocontainer.ETD,
                            TypeOfTOLocationID = typeoftolocationid,
                        };
                        result.Add(itemLocation);
                        itemLocation.ListTOContainer = new List<HelperTOMaster_TOContainer>();
                        itemLocation.ListTOContainer.Add(new HelperTOMaster_TOContainer()
                        {
                            ID = tocontainer.ID,
                            OPSContainerID = tocontainer.OPSContainerID,
                            ParentID = tocontainer.ID,
                            LocationFromID = tocontainer.LocationFromID,
                            LocationToID = tocontainer.LocationToID,
                            SortOrder = tocontainer.SortOrder,
                            SortLocation = sort,
                            StatusOfCOContainerID = tocontainer.StatusOfCOContainerID,
                            IsSplit = false,
                            IsCreate = true
                        });
                        sort++;
                    }
                }

            }
            return result;
        }

        public static List<HelperTOMaster_Error> OPSCO_UnTendered(DataEntities model, AccountItem account, List<int> lstmasterid)
        {
            var result = new List<HelperTOMaster_Error>();

            foreach (var masterid in lstmasterid)
            {
                var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
                if (obj != null)
                {
                    if (obj.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterTendered)
                    {
                        obj.ModifiedBy = account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproved;

                        foreach (var item in model.OPS_COTORate.Where(c => c.COTOMasterID == obj.ID))
                            model.OPS_COTORate.Remove(item);
                        foreach (var item in model.CAT_Trouble.Where(c => c.COTOMasterID == obj.ID))
                            model.CAT_Trouble.Remove(item);
                        foreach (var item in model.OPS_COTOStation.Where(c => c.COTOMasterID == obj.ID))
                            model.OPS_COTOStation.Remove(item);
                        foreach (var item in model.OPS_COTOLocation.Where(c => c.COTOMasterID == obj.ID))
                            model.OPS_COTOLocation.Remove(item);
                        foreach (var item in model.OPS_COTO.Where(c => c.COTOMasterID == obj.ID))
                        {
                            foreach (var detail in model.OPS_COTODetail.Where(c => c.COTOID == item.ID))
                                model.OPS_COTODetail.Remove(detail);
                            model.OPS_COTO.Remove(item);
                        }
                        foreach (var item in model.OPS_COTOContainer.Where(c => c.CreateByMasterID == obj.ID && c.ParentID > 0))
                            model.OPS_COTOContainer.Remove(item);
                        foreach (var item in model.OPS_COTOContainer.Where(c => c.CreateByMasterID == obj.ID && c.ParentID == null))
                            model.OPS_COTOContainer.Remove(item);

                        //foreach (var o in model.OPS_COTORate.Where(c => c.COTOMasterID == obj.ID && c.IsSend == true).ToList())
                        //{
                        //    o.ModifiedDate = DateTime.Now;
                        //    o.ModifiedBy = account.UserName;
                        //    o.IsAccept = false;
                        //}
                        //model.OPS_COTORate.RemoveRange(model.OPS_COTORate.Where(c => c.COTOMasterID == obj.ID && c.IsSend == false).ToList());
                        //foreach (var o in model.CAT_Trouble.Where(c => c.COTOMasterID == obj.ID).ToList())
                        //{
                        //    model.CAT_Trouble.Remove(o);
                        //}
                        //foreach (var o in model.OPS_COTOStation.Where(c => c.COTOMasterID == obj.ID).ToList())
                        //{
                        //    model.OPS_COTOStation.Remove(o);
                        //}
                        OPS_FIN_Delete(model, obj.ID, -1, -1, -1);
                        model.SaveChanges();

                        foreach (var item in model.OPS_COTOContainer.Where(c => c.COTOMasterID == obj.ID && c.IsSplit == true))
                        {
                            if (model.OPS_COTOContainer.Where(c => c.ParentID == item.ID).Count() == 0)
                            {
                                item.IsSplit = false;
                                item.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                            }
                                
                        }
                        model.SaveChanges();

                        if (model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Count() == 1)
                        {
                            foreach (var item in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid))
                                item.IsSplit = false;
                            model.SaveChanges();
                        }

                        var SDATA = model.OPS_COTOContainer.Where(c => c.COTOMasterID == obj.ID).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
                        HelperStatus.ORDOrder_Status(model, account, SDATA);
                    }
                    else
                    {
                        throw FaultHelper.BusinessFault(null, null, "Không thể trả về! Chỉ được trả về chuyến đã duyệt.");
                    }
                }
            }
            return result;
        }

        private static void OPSCO_Tendered_CheckInspection(DataEntities model, List<int> data)
        {
            try
            {
                List<string> errors = new List<string>();
                var dataOrd = model.ORD_Order.Where(c => data.Contains(c.ID)).Select(c => new
                {
                    c.ID,
                    c.Code
                }).ToList();
                foreach (var item in dataOrd)
                {
                    if (model.ORD_Document.Count(c => c.OrderID == item.ID) > 0)
                    {
                        if (model.ORD_Document.Count(c => c.OrderID == item.ID) != model.ORD_Document.Count(c => c.OrderID == item.ID && c.DocumentStatusID == -(int)SYSVarType.DocumentStatusComplete))
                            errors.Add(item.Code);
                    }
                }
                if (errors.Count > 0)
                    throw FaultHelper.BusinessFault(null, null, "Các đơn " + string.Join(", ", errors) + " chưa được kiểm hóa xong!");
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

        //public remove tendered 
        public static List<HelperTOMaster_Error> OPSCO_RemoveApproved(DataEntities model, AccountItem account, List<int> lstmasterid)
        {
            var result = new List<HelperTOMaster_Error>();
            foreach (var masterid in lstmasterid)
            {
                var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
                if (obj != null)
                {
                    if (obj.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterApproved)
                    {
                        var lstOPSContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => c.OPSContainerID).Distinct().ToList();
                        bool isorder = true;
                        bool isfirst = false;
                        bool hassecond = false;
                        foreach (var opscontainerid in lstOPSContainerID)
                        {
                            int total = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid).Count();
                            int totalMaster = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.COTOMasterID == masterid).Count();
                            if (isorder)
                                isorder = total == totalMaster;
                            if (model.OPS_COTOContainer.Where(c => c.SortOrder < 10 && c.COTOMasterID == masterid && c.OPSContainerID == opscontainerid).Count() > 0)
                                isfirst = true;
                            if (model.OPS_COTOContainer.Where(c => c.COTOMasterID != masterid && c.COTOMasterID > 0 && c.OPSContainerID == opscontainerid).Count() > 0)
                                hassecond = true;
                        }
                        bool flag = false;
                        if (isorder)
                        {
                            //order
                            flag = true;
                        }
                        else
                        {
                            //state
                            if (isfirst && hassecond)
                                throw FaultHelper.BusinessFault(null, null, "Không thể xóa! Phải xóa chuyến chặng 2 trước.");
                            else if (hassecond == false)
                            {
                                //one master
                                flag = true;
                            }
                            else if (isfirst == false && hassecond == true)
                            {
                                //state 2
                                var timeCurrent = model.FLM_AssetTimeSheet.Where(c => c.ReferID == obj.ID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster).Select(c => new { c.ID, c.AssetID, c.DateToActual }).FirstOrDefault();
                                if (timeCurrent != null)
                                {
                                    //var timeNext = model.FLM_AssetTimeSheet.Where(c => c.ID != timeCurrent.ID && c.DateFromActual >= timeCurrent.DateToActual).Select(c => new { c.ID, c.AssetID, c.DateToActual }).FirstOrDefault();
                                    //if (timeNext != null)
                                    //    throw FaultHelper.BusinessFault(null, null, "Không thể xóa! Phải xóa chuyến đã lập sau chuyến này.");
                                    //else
                                    flag = true;
                                }
                            }
                        }

                        if (flag)
                        {
                            var SDATA = model.OPS_COTOContainer.Where(c => c.COTOMasterID == obj.ID).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
                            List<int> dataP = new List<int>();
                            var lstTOContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid).Select(c => c.ID).ToList();
                            foreach (var o in model.OPS_COTOContainer.Where(c => c.CreateByMasterID == obj.ID).ToList())
                            {
                                if (o.ParentID > 0)
                                    dataP.Add(o.ParentID.Value);
                                model.OPS_COTOContainer.Remove(o);
                            }
                            foreach (var o in model.OPS_COTOContainer.Where(c => dataP.Contains(c.ID)).ToList())
                            {
                                o.IsSplit = false;
                            }
                            foreach (var o in model.OPS_COTOLocation.Where(c => c.COTOMasterID == obj.ID).ToList())
                            {
                                model.OPS_COTOLocation.Remove(o);
                            }
                            foreach (var o in model.OPS_COTOStation.Where(c => c.COTOMasterID == obj.ID).ToList())
                            {
                                model.OPS_COTOStation.Remove(o);
                            }
                            foreach (var o in model.OPS_COTORate.Where(c => c.COTOMasterID == obj.ID).ToList())
                            {
                                model.OPS_COTORate.Remove(o);
                            }
                            foreach (var o in model.OPS_COTO.Where(c => c.COTOMasterID == obj.ID).ToList())
                            {
                                foreach (var e in model.OPS_COTODetail.Where(c => c.COTOID == o.ID).ToList())
                                {
                                    model.OPS_COTODetail.Remove(e);
                                }
                                model.OPS_COTO.Remove(o);
                            }
                            OPS_FIN_Delete(model, obj.ID, -1, -1, -1);
                            model.OPS_COTOMaster.Remove(obj);
                            model.SaveChanges();

                            HelperTimeSheet.COTOMaster_Remove(model, account, masterid);
                            OPSCO_ResetOPSContainer(model, account, lstTOContainerID);
                            HelperStatus.ORDOrder_Status(model, account, SDATA);
                        }
                    }
                    else
                    {
                        throw FaultHelper.BusinessFault(null, null, "Không thể xóa! Chỉ được xóa các chuyến đang lập kế hoạch.");
                    }
                }
            }
            return result;
        }

        private static void OPS_FIN_Delete(DataEntities model, int cotoMasterID, int ditoMasterID, int gopID, int conID)
        {
            foreach (var item in model.FIN_Temp.Where(c => c.DITOMasterID == ditoMasterID))
            {
                model.FIN_Temp.Remove(item);
            }
            foreach (var item in model.FIN_PL.Where(c => c.DITOMasterID == ditoMasterID))
            {
                foreach (var o in model.FIN_PLDetails.Where(c => c.PLID == item.ID))
                {
                    foreach (var e in model.FIN_PLContainer.Where(c => c.PLDetailID == o.ID))
                    {
                        model.FIN_PLContainer.Remove(e);
                    }
                    foreach (var e in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == o.ID))
                    {
                        model.FIN_PLGroupOfProduct.Remove(e);
                    }
                    model.FIN_PLDetails.Remove(o);
                }
                model.FIN_PL.Remove(item);
            }
            foreach (var item in model.FIN_PL.Where(c => c.COTOMasterID == cotoMasterID))
            {
                foreach (var o in model.FIN_PLDetails.Where(c => c.PLID == item.ID))
                {
                    foreach (var e in model.FIN_PLContainer.Where(c => c.PLDetailID == o.ID))
                    {
                        model.FIN_PLContainer.Remove(e);
                    }
                    foreach (var e in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == o.ID))
                    {
                        model.FIN_PLGroupOfProduct.Remove(e);
                    }
                    model.FIN_PLDetails.Remove(o);
                }
                model.FIN_PL.Remove(item);
            }
            foreach (var item in model.KPI_KPITime.Where(c => c.COTOMasterID == cotoMasterID))
            {
                model.KPI_KPITime.Remove(item);
            }
            foreach (var item in model.KPI_VENTime.Where(c => c.COTOMasterID == cotoMasterID))
            {
                model.KPI_VENTime.Remove(item);
            }
            foreach (var item in model.KPI_VENTime.Where(c => c.DITOMasterID == ditoMasterID))
            {
                model.KPI_VENTime.Remove(item);
            }
            foreach (var item in model.KPI_KPITime.Where(c => c.DITOMasterID == ditoMasterID))
            {
                model.KPI_KPITime.Remove(item);
            }
            foreach (var item in model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == ditoMasterID))
            {
                foreach (var o in model.FIN_Temp.Where(c => c.DITOGroupProductID == item.ID))
                {
                    model.FIN_Temp.Remove(o);
                }
                foreach (var o in model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID == item.ID))
                {
                    foreach (var e in model.FIN_PLDetails.Where(c => c.PLID == o.PLDetailID))
                        model.FIN_PLDetails.Remove(e);

                    model.FIN_PLGroupOfProduct.Remove(o);
                }
                foreach (var o in model.FIN_ManualFix.Where(c => c.DITOGroupProductID == item.ID))
                {
                    model.FIN_ManualFix.Remove(o);
                }
                foreach (var o in model.KPI_KPITime.Where(c => c.DITOGroupProductID == item.ID))
                {
                    model.KPI_KPITime.Remove(o);
                }
                foreach (var o in model.KPI_VENTime.Where(c => c.DITOGroupProductID == item.ID))
                {
                    model.KPI_VENTime.Remove(o);
                }
            }
            foreach (var item in model.OPS_COTOContainer.Where(c => c.COTOMasterID == cotoMasterID))
            {
                foreach (var o in model.FIN_Temp.Where(c => c.COTOContainerID == item.ID))
                {
                    model.FIN_Temp.Remove(o);
                }
                foreach (var o in model.FIN_PLContainer.Where(c => c.COTOContainerID == item.ID))
                {
                    foreach (var e in model.FIN_PLDetails.Where(c => c.PLID == o.PLDetailID))
                        model.FIN_PLDetails.Remove(e);

                    model.FIN_PLContainer.Remove(o);
                }
            }
            foreach (var item in model.WFL_PacketDetail.Where(c => c.DITOMasterID == ditoMasterID))
            {
                model.WFL_PacketDetail.Remove(item);
            }
            foreach (var item in model.WFL_PacketDetail.Where(c => c.COTOMasterID == cotoMasterID))
            {
                model.WFL_PacketDetail.Remove(item);
            }
        }

        public static HelperTOMaster_Error OPSCO_AddTOContainerMasterCheck(DataEntities model, AccountItem account, int masterid, List<DTOOPSCO_Map_Schedule_Event> lstCOTOContainerID, HelperTOMaster_COParam param = default(HelperTOMaster_COParam))
        {

            return HelperTOMaster_Error.None;
        }

        //public add container(check)  
        public static HelperTOMaster_Error OPSCO_AddTOContainerMaster(DataEntities model, AccountItem account, int masterid, List<DTOOPSCO_Map_Schedule_Event> lstCOTOContainerAdd, HelperTOMaster_COParam param = default(HelperTOMaster_COParam))
        {
            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
            if (objMaster != null)
            {
                //if (lstCOTOContainerID != null && lstCOTOContainerID.Count > 0)
                //{
                //    if (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID)
                //    {
                //        //owner
                //        foreach (var tocon in model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)))
                //        {
                //            tocon.COTOMasterID = objMaster.ID;
                //        }
                //        model.SaveChanges();
                //    }
                //    else
                //    {
                //        //vendor
                //        foreach (var tocon in model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)))
                //        {
                //            tocon.COTOMasterID = objMaster.ID;
                //        }
                //        model.SaveChanges();
                //    }
                return HelperTOMaster_Error.None;
                //}
                //else
                //    throw FaultHelper.BusinessFault(null, null, "list null");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "master null");
        }

        //public delete container(check) 
        public static HelperTOMaster_Error OPSCO_DeleteTOContainerMaster(DataEntities model, AccountItem account, int masterid, List<int> lstCOTOContainerID, HelperTOMaster_COParam param = default(HelperTOMaster_COParam))
        {
            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
            if (objMaster != null)
            {
                if (lstCOTOContainerID != null && lstCOTOContainerID.Count > 0)
                {
                    if (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID)
                    {
                        //owner
                        foreach (var tocon in model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)))
                        {
                            tocon.COTOMasterID = null;
                        }
                        model.SaveChanges();
                    }
                    else
                    {
                        //vendor
                        foreach (var tocon in model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)))
                        {
                            tocon.COTOMasterID = null;
                        }
                        model.SaveChanges();
                    }
                    return HelperTOMaster_Error.None;
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "list null");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "master null");
        }



        //Choose vendor 
        public static DTOOPSCOTOMaster OPSCO_CreateItemChooseVendor(DataEntities model, AccountItem account, int vendorid, DateTime? etd, DateTime? eta, List<int> lstCOTOContainerID)
        {
            if (lstCOTOContainerID != null && lstCOTOContainerID.Count > 0)
            {
                if (vendorid > 0 && vendorid != account.SYSCustomerID)
                {
                    var lstOPSContainerID = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => c.OPSContainerID).Distinct().ToList();

                    foreach (var opscontainerid in lstOPSContainerID)
                    {
                        var item = new DTOOPSCOTOMaster();
                        item.VendorOfVehicleID = vendorid;
                        item.VehicleID = DefaultTractor;
                        item.VendorOfRomoocID = vendorid;
                        item.RomoocID = DefaultRomooc;
                        if (etd == null)
                        {
                            var first = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid).Select(c => new { c.SortOrder, c.ETD }).OrderBy(c => c.SortOrder).FirstOrDefault();
                            if (first != null)
                                etd = first.ETD;
                            else
                                etd = DateTime.Now;
                        }
                        if (eta == null)
                        {
                            var last = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid).Select(c => new { c.SortOrder, c.ETA }).OrderByDescending(c => c.SortOrder).FirstOrDefault();
                            if (last != null)
                                eta = last.ETA;
                            else
                                eta = etd.Value.AddHours(1);
                        }
                        item.ETD = etd.Value;
                        item.ETA = eta.Value;
                        item.ListCOContainer = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid).Select(c => new DTOOPSCOTOContainer
                        {
                            ID = c.ID
                        }).ToList();
                        item.ID = OPSCO_CreateMasterVendor(model, account, item);
                    }

                    HelperStatus.ORDOrder_Status(model, account, model.OPS_COTOContainer.Where(c => lstOPSContainerID.Contains(c.OPSContainerID)).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList());

                    return new DTOOPSCOTOMaster();
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "vendor fail");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list fail");
        }

        //Send vendor 
        public static void OPSCO_CreateItemSendVendor(DataEntities model, AccountItem account, List<OPSCOTORate> lstTender, List<int> lstCOTOContainerID, double rateTime = 0.5)
        {
            if (lstCOTOContainerID != null && lstCOTOContainerID.Count > 0 && lstTender != null && lstTender.Count > 0)
            {
                var lstCOTOContainer = model.OPS_COTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new { c.ID, c.ETD, c.ETA, c.SortOrder, c.OPSContainerID, c.COTOMasterID, c.ParentID }).ToList();
                var lstOPSContainerID = lstCOTOContainer.Select(c => c.OPSContainerID).Distinct().ToList();
                foreach (var opsContainerID in lstOPSContainerID)
                {
                    var item = new DTOOPSCOTOMaster();
                    item.VendorOfVehicleID = null;
                    item.VehicleID = DefaultTractor;
                    item.VendorOfRomoocID = null;
                    item.RomoocID = DefaultRomooc;
                    item.RateTime = rateTime;

                    DateTime etd = DateTime.Now;
                    var first = lstCOTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new { c.SortOrder, c.ETD }).OrderBy(c => c.SortOrder).FirstOrDefault();
                    if (first != null)
                        etd = first.ETD.Value;
                    DateTime eta = DateTime.Now;
                    var last = lstCOTOContainer.Where(c => lstCOTOContainerID.Contains(c.ID)).Select(c => new { c.SortOrder, c.ETA }).OrderByDescending(c => c.SortOrder).FirstOrDefault();
                    if (last != null)
                        eta = last.ETA.Value;
                    item.ETD = etd;
                    item.ETA = eta;
                    item.ListCOContainer = lstCOTOContainer.Where(c => c.OPSContainerID == opsContainerID).Select(c => new DTOOPSCOTOContainer
                    {
                        ID = c.ID
                    }).ToList();
                    item.ID = OPSCO_CreateMasterVendor(model, account, item);
                    var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == item.ID);
                    if (objMaster != null)
                    {
                        objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterSendTender;

                        var firstTender = default(OPS_COTORate);
                        foreach (var itemTender in lstTender)
                        {
                            var objTender = new OPS_COTORate();
                            objTender.COTOMasterID = item.ID;
                            objTender.VendorID = itemTender.VendorID;
                            objTender.SortOrder = itemTender.SortOrder;
                            objTender.Debit = itemTender.Debit;
                            objTender.IsManual = itemTender.IsManual;
                            objTender.IsAccept = null;
                            objTender.IsSend = false;
                            objTender.Note = itemTender.Note;
                            objTender.FirstRateTime = null;
                            objTender.LastRateTime = null;
                            objTender.CreatedBy = account.UserName;
                            objTender.CreatedDate = DateTime.Now;
                            model.OPS_COTORate.Add(objTender);
                            if (objTender.SortOrder == 1)
                            {
                                firstTender = objTender;
                                objMaster.VendorOfVehicleID = itemTender.VendorID;
                                objMaster.VendorOfRomoocID = itemTender.VendorID;
                            }
                        }
                        model.SaveChanges();
                        if (firstTender != null)
                        {
                            firstTender.FirstRateTime = DateTime.Now;
                            firstTender.LastRateTime = DateTime.Now.AddHours(rateTime);
                        }
                        model.SaveChanges();
                    }
                }
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list fail");
        }

        //Choose vendor 
        public static DTOOPSCOTOMaster OPSCO_CreateItemVendor(DataEntities model, AccountItem account, DTOOPSCOTOMaster master)
        {
            if (master.VendorOfVehicleID > 0 && master.VendorOfVehicleID != account.SYSCustomerID)
                master.ID = OPSCO_CreateMasterVendor(model, account, master);
            else
                throw FaultHelper.BusinessFault(null, null, "vendor fail");
            return master;
        }

        private static int OPSCO_CreateMasterVendor(DataEntities model, AccountItem account, DTOOPSCOTOMaster master)
        {
            //Reset sort 
            var lstTOContainerID = master.ListCOContainer.Select(c => c.ID).ToList();

            int cattransportmodeid = -1;
            var ord = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.ORD_Order.TransportModeID > 0 && lstTOContainerID.Contains(c.ID)).Select(c => new { c.OPS_Container.ORD_Container.ORD_Order.TransportModeID }).FirstOrDefault();
            if (ord != null)
                cattransportmodeid = ord.TransportModeID;
            else
                throw FaultHelper.BusinessFault(null, null, "transport mode fail");

            var lstOPSContainerID = model.OPS_COTOContainer.Where(c => lstTOContainerID.Contains(c.ID)).Select(c => c.OPSContainerID).Distinct().ToList();
            var lstTOContainer = model.OPS_COTOContainer.Where(c => lstOPSContainerID.Contains(c.OPSContainerID)).ToList();
            if (lstTOContainer.Count == 0)
                throw FaultHelper.BusinessFault(null, null, "list fail");
            foreach (var opscontainerid in lstOPSContainerID)
            {
                OPSCO_CreateMasterVendor_SortContainer(opscontainerid, null, lstTOContainer, 1);
            }
            model.SaveChanges();

            var lstLocationID = lstTOContainer.OrderBy(c => c.SortOrder).Select(c => c.LocationFromID).Distinct().ToList();
            lstLocationID.AddRange(lstTOContainer.OrderBy(c => c.SortOrder).Select(c => c.LocationToID).Distinct().ToList());
            lstLocationID = lstLocationID.Distinct().ToList();
            master.ListCOLocation = new List<DTOOPSCOTOLocation>();
            foreach (var location in lstTOContainer.OrderBy(c => c.ETD))
            {
                if (master.ListCOLocation.Where(c => c.LocationID == location.LocationFromID).Count() == 0)
                {
                    master.ListCOLocation.Add(new DTOOPSCOTOLocation
                    {
                        LocationID = location.LocationFromID,
                        DateComeEstimate = location.ETD,
                        DateLeaveEstimate = location.ETD
                    });
                }
                if (master.ListCOLocation.Where(c => c.LocationID == location.LocationToID).Count() == 0)
                {
                    master.ListCOLocation.Add(new DTOOPSCOTOLocation
                    {
                        LocationID = location.LocationFromID,
                        DateComeEstimate = location.ETA,
                        DateLeaveEstimate = location.ETA
                    });
                }
            }

            var objMaster = new OPS_COTOMaster();
            objMaster.CreatedBy = account.UserName;
            objMaster.CreatedDate = DateTime.Now;
            objMaster.SYSCustomerID = account.SYSCustomerID;
            objMaster.TransportModeID = cattransportmodeid;

            objMaster.Code = "";
            objMaster.IsHot = false;
            objMaster.RateTime = 0;
            objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproved;

            model.OPS_COTOMaster.Add(objMaster);

            objMaster.SortOrder = 1;
            objMaster.VehicleID = master.VehicleID;
            if (objMaster.VehicleID == null || objMaster.VehicleID < 1)
                objMaster.VehicleID = DefaultTractor;
            objMaster.VendorOfVehicleID = master.VendorOfVehicleID;
            objMaster.RomoocID = master.RomoocID;
            if (objMaster.RomoocID == null || objMaster.RomoocID < 1)
                objMaster.RomoocID = DefaultRomooc;
            objMaster.VendorOfRomoocID = master.VendorOfVehicleID; //same vehicle
            objMaster.DriverID1 = master.DriverID1;
            objMaster.DriverID2 = master.DriverID2;
            objMaster.DriverName1 = master.DriverName1;
            objMaster.DriverName2 = master.DriverName2;
            objMaster.DriverCard1 = master.DriverCard1;
            objMaster.DriverCard2 = master.DriverCard2;
            objMaster.DriverTel1 = master.DriverTel1;
            objMaster.DriverTel2 = master.DriverTel2;
            objMaster.ApprovedBy = master.ApprovedBy;
            objMaster.ApprovedDate = master.ApprovedDate;
            objMaster.GroupOfVehicleID = master.GroupOfVehicleID;
            objMaster.RateTime = master.RateTime;
            objMaster.ETD = master.ETD;
            objMaster.ETA = master.ETA;
            objMaster.TypeOfDriverID1 = -(int)SYSVarType.TypeOfDriverMain;

            //OPSCO_CheckingTime(model, account, objMaster.ID, objMaster.VehicleID, objMaster.RomoocID, objMaster.ETD, objMaster.ETA, true);

            objMaster.DateConfig = master.ETD;
            objMaster.Note = master.Note;
            objMaster.IsBidding = master.IsBidding;
            objMaster.BiddingID = master.BiddingID;
            objMaster.KM = master.KM;
            objMaster.TransportModeID = master.TransportModeID;
            objMaster.TypeOfOrderID = master.TypeOfOrderID;
            objMaster.ContractID = master.ContractID;
            objMaster.IsCreateContainer = master.IsCreateContainer;

            model.SaveChanges();
            objMaster.Code = COCodePrefix + objMaster.ID.ToString(COCodeNum);

            int sort = 1;
            var fromlocation = default(OPS_COTOLocation);
            master.ListCO = new List<DTOOPSCOTO>();
            foreach (var toloc in master.ListCOLocation)
            {
                var objLocation = new OPS_COTOLocation();
                objLocation.LocationID = toloc.LocationID;
                objLocation.SortOrder = sort++;
                objLocation.COTOMasterID = objMaster.ID;
                objLocation.CreatedBy = account.UserName;
                objLocation.CreatedDate = DateTime.Now;
                objLocation.DateComeEstimate = toloc.DateComeEstimate;
                objLocation.DateLeaveEstimate = toloc.DateLeaveEstimate;
                objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;

                model.OPS_COTOLocation.Add(objLocation);

                if (fromlocation != null)
                {
                    var objTO = new OPS_COTO();
                    objTO.CreatedBy = account.UserName;
                    objTO.CreatedDate = DateTime.Now;
                    objTO.COTOMasterID = objMaster.ID;
                    objTO.IsOPS = true;
                    objTO.SortOrder = sort - 1;
                    objTO.LocationFromID = fromlocation.LocationID;
                    objTO.LocationToID = toloc.LocationID;
                    objTO.ETD = fromlocation.DateComeEstimate;
                    objTO.ETA = toloc.DateComeEstimate;
                    objTO.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;

                    model.OPS_COTO.Add(objTO);

                    foreach (var tocon in lstTOContainer.Where(c => c.LocationFromID == objTO.LocationFromID))
                    {
                        tocon.COTOSort = objTO.SortOrder;
                        tocon.COTOMasterID = objMaster.ID;
                    }
                }
                fromlocation = objLocation;
            }
            foreach (var tocon in lstTOContainer)
            {
                tocon.COTOMasterID = objMaster.ID;
            }
            model.SaveChanges();

            return objMaster.ID;
        }

        //Reset sort
        private static void OPSCO_CreateMasterVendor_SortContainer(int opscontainerid, int? parentid, List<OPS_COTOContainer> lst, int parentsort)
        {
            var query = lst.Where(c => c.ParentID == null && c.OPSContainerID == opscontainerid);
            if (parentid > 0)
                query = lst.Where(c => c.ParentID == parentid.Value && c.OPSContainerID == opscontainerid);
            int sort = parentsort;
            foreach (var item in query)
            {
                item.SortOrder = sort;
                OPSCO_CreateMasterVendor_SortContainer(opscontainerid, item.ID, lst, sort);

                if (parentid > 0)
                    sort++;
                else
                    sort += 20;
            }
        }

        public static HelperTOMaster_Error OPSCO_VendorChangeScheduleTime(DataEntities model, AccountItem account, int masterid, DateTime etd, DateTime eta)
        {
            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
            if (objMaster != null)
            {
                if (etd.CompareTo(eta) < 0 && etd.CompareTo(DateTime.MinValue) > 0)
                {
                    string strCheck = string.Empty;
                    if (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID)
                        strCheck = CO_CheckAssetTimeSheet(model, account, objMaster.VehicleID, objMaster.RomoocID, objMaster.ID, etd, eta);
                    if (strCheck == string.Empty)
                    {
                        objMaster.ETD = etd;
                        objMaster.ETA = eta;
                        model.SaveChanges();

                        HelperTimeSheet.Remove(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                        if (objMaster.VehicleID > 2 && (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID))
                            HelperTimeSheet.Create(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);

                        return HelperTOMaster_Error.None;
                    }
                    else
                        throw FaultHelper.BusinessFault(null, null, strCheck);
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "etd, eta fail");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "master null");
        }



        private static void CO_DockScheduleAdd(DataEntities model, AccountItem account, List<int> lstMasterID)
        {
            double houradd = 2;
            bool flag = false;

            if (lstMasterID != null && lstMasterID.Count > 0)
            {
                foreach (var masterid in lstMasterID)
                {
                    var lstStock = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.OPS_COTOMaster.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterTendered).Select(c => new
                    {
                        c.StatusOfCOContainerID,
                        c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID,
                        c.OPS_Container.ORD_Container.LocationFromID,
                        c.OPS_Container.ORD_Container.LocationToID,
                        c.ETD,
                        c.ETA
                    }).Distinct().ToList();
                    if (lstStock.Count > 0 && model.OPS_TOMasterDockSchedule.Where(c => c.COTOMasterID == masterid).Count() == 0)
                    {
                        Dictionary<int, DateTime> dicStock = new Dictionary<int, DateTime>();
                        //var lstStockID = new List<int>();
                        foreach (var stock in lstStock)
                        {
                            if (stock.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport)
                            {
                                if (stock.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty && !dicStock.ContainsKey(stock.LocationFromID.Value))
                                    dicStock.Add(stock.LocationFromID.Value, stock.ETD.Value);
                            }
                            else if (stock.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)
                            {
                                if (stock.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden && !dicStock.ContainsKey(stock.LocationToID.Value))
                                    dicStock.Add(stock.LocationToID.Value, stock.ETA.Value);
                            }
                            else if (stock.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal)
                            {
                                if (!dicStock.ContainsKey(stock.LocationFromID.Value))
                                    dicStock.Add(stock.LocationFromID.Value, stock.ETD.Value);
                                if (!dicStock.ContainsKey(stock.LocationToID.Value))
                                    dicStock.Add(stock.LocationToID.Value, stock.ETA.Value);
                            }
                        }
                        foreach (var stock in dicStock)
                        {
                            var catloc = model.CUS_Location.Where(c => c.ID == stock.Key).Select(c => new { c.LocationID }).FirstOrDefault();
                            if (catloc != null)
                            {
                                if (model.CAT_Dock.Where(c => c.LocationID == catloc.LocationID).Count() > 0)
                                {
                                    var obj = new OPS_TOMasterDockSchedule();
                                    obj.DITOMasterID = masterid;
                                    obj.LocationID = catloc.LocationID;
                                    obj.TOMasterDockScheduleStatusID = -(int)SYSVarType.TOMasterDockScheduleStatusOpen;
                                    obj.CreatedBy = account.UserName;
                                    obj.CreatedDate = DateTime.Now;

                                    obj.DateCome = stock.Value;
                                    obj.DateComeEnd = stock.Value.AddHours(houradd);
                                    obj.DateComeModified = obj.DateCome;
                                    obj.DateComeModifiedEnd = obj.DateComeEnd;
                                    obj.DateComeApproved = obj.DateCome;
                                    obj.DateComeApprovedEnd = obj.DateComeEnd;

                                    obj.Ton = 0;
                                    obj.CBM = 0;
                                    obj.Quantity = 0;

                                    model.OPS_TOMasterDockSchedule.Add(obj);
                                    flag = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void CO_DockScheduleRemove(DataEntities model, AccountItem account, List<int> lstMasterID)
        {
            if (lstMasterID != null && lstMasterID.Count > 0)
            {
                foreach (var masterid in lstMasterID)
                {
                    if (model.OPS_TOMasterDockSchedule.Where(c => c.COTOMasterID == masterid).Count() > 0 && model.OPS_COTOMaster.Where(c => c.ID == masterid && c.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterTendered).Count() == 0)
                    {
                        foreach (var item in model.OPS_TOMasterDockSchedule.Where(c => c.COTOMasterID == masterid))
                            model.OPS_TOMasterDockSchedule.Remove(item);
                        model.SaveChanges();
                    }
                }
            }
        }
        #endregion

        #region MON
        private static void MONCO_CompleteParent(DataEntities model, AccountItem account, List<int> lstmasterid)
        {
            var lstOPSContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && lstmasterid.Contains(c.COTOMasterID.Value)).Select(c => c.OPSContainerID).Distinct().ToList();
            var lstOPSContainerIDFail = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && lstmasterid.Contains(c.COTOMasterID.Value) && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerReturnEmptyFail).Select(c => c.OPSContainerID).Distinct().ToList();
            foreach (var opsContainerID in lstOPSContainerID)
            {
                if (lstOPSContainerIDFail.Count == 0 || !lstOPSContainerIDFail.Contains(opsContainerID))
                {
                    var flag = MONCO_CompleteParent_Child(model, account, opsContainerID, null, null);
                    var objOPSContainer = model.OPS_Container.FirstOrDefault(c => c.ID == opsContainerID);
                    if (objOPSContainer != null)
                    {
                        var objORDContainer = model.ORD_Container.FirstOrDefault(c => c.ID == objOPSContainer.ContainerID);
                        if (objORDContainer != null)
                        {
                            var last = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opsContainerID && (c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerTranfer || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete) && c.COTOMasterID > 0 && c.OPS_COTOMaster.RomoocID > 0)
                                    .Select(c => new { c.OPS_COTOMaster.RomoocID, c.SortOrder }).OrderByDescending(c => c.SortOrder).FirstOrDefault();
                            if (last != null)
                            {
                                objOPSContainer.RomoocID = last.RomoocID;
                                objORDContainer.RomoocID = last.RomoocID;
                            }

                            if (flag == true)
                                objOPSContainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeComplete;
                            else if (last != null)
                                objOPSContainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeRunning;
                            else
                                objOPSContainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeOpen;
                        }
                    }
                }
            }
            model.SaveChanges();

            foreach (var masterid in lstmasterid)
            {
                MONCO_CompleteParent_LastData(model, account, masterid);
            }
            model.SaveChanges();
        }

        private static bool MONCO_CompleteParent_Child(DataEntities model, AccountItem account, int opsContainerID, int? parentid, OPS_COTOContainer parentnull)
        {
            var query = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.ParentID == null);
            if (parentid > 0)
                query = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.ParentID == parentid.Value);
            int total = query.Count();
            if (total > 0)
            {
                int totalComplete = 0;
                var last = query.Select(c => new { c.ID, c.COTOMasterID, c.SortOrder }).OrderByDescending(c => c.SortOrder).FirstOrDefault();
                foreach (var toContainer in query)
                {
                    var flag = false;
                    int totalchild = model.OPS_COTOContainer.Where(c => c.ParentID == toContainer.ID).Count();
                    if (totalchild > 0)
                    {
                        if (parentid == null)
                            flag = MONCO_CompleteParent_Child(model, account, opsContainerID, toContainer.ID, toContainer);
                        else
                            flag = MONCO_CompleteParent_Child(model, account, opsContainerID, toContainer.ID, parentnull);

                        if (flag == true)
                        {
                            toContainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                            totalComplete++;
                        }
                        else
                        {
                            toContainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                        }
                    }
                    else
                    {
                        flag = toContainer.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete;
                        if (flag == true)
                        {
                            totalComplete++;
                            if (last.ID == toContainer.ID && parentnull != null)
                            {
                                parentnull.COTOMasterID = toContainer.COTOMasterID;
                            }
                        }
                    }
                }
                if (total == totalComplete)
                {
                    if (parentnull != null)
                    {

                        parentnull.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                    }
                    return true;
                }
                else
                    return false;
            }
            else if (parentid > 0)
                return model.OPS_COTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.ID == parentid.Value && c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete).Count() > 0;
            else
                return model.OPS_COTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.ParentID == null && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerComplete).Count() == 0;
        }

        private static void MONCO_CompleteParent_LastData(DataEntities model, AccountItem account, int masterid)
        {
            var objMaster = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false &&
                c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerReturnRomooc && c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete)
                .Select(c => new { c.OPS_COTOMaster.VehicleID, c.OPS_COTOMaster.RomoocID, c.LocationToID }).FirstOrDefault();
            if (objMaster != null)
            {
                var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == objMaster.VehicleID);
                if (objVehicle != null)
                {
                    objVehicle.LocationID = objMaster.LocationToID;
                    if (objVehicle.CurrentRomoocID > 0)
                    {
                        var objRomooc = model.CAT_Romooc.FirstOrDefault(c => c.ID == objVehicle.CurrentRomoocID);
                        if (objRomooc != null)
                        {
                            objRomooc.LocationID = objMaster.LocationToID;
                            objRomooc.Lat = objVehicle.Lat;
                            objRomooc.Lng = objVehicle.Lng;
                        }
                    }
                    objVehicle.CurrentRomoocID = null;
                }
            }
            else
            {
                objMaster = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false &&
                    c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerGetRomooc &&
                    c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete).Select(c => new { c.OPS_COTOMaster.VehicleID, c.OPS_COTOMaster.RomoocID, c.LocationToID }).FirstOrDefault();
                if (objMaster != null)
                {
                    var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == objMaster.VehicleID);
                    if (objVehicle != null)
                    {
                        objVehicle.LocationID = objMaster.LocationToID;
                        objVehicle.CurrentRomoocID = objMaster.RomoocID;
                    }
                }
                else
                {
                    var objLocation = model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid && c.COTOLocationStatusID >= -(int)SYSVarType.COTOLocationStatusCome)
                        .OrderByDescending(c => c.SortOrder).Select(c => new { c.LocationID, c.OPS_COTOMaster.VehicleID }).FirstOrDefault();
                    if (objLocation != null)
                    {
                        var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == objLocation.VehicleID);
                        if (objVehicle != null)
                        {
                            objVehicle.LocationID = objLocation.LocationID;
                            if (objVehicle.CurrentRomoocID > 0)
                            {
                                var objRomooc = model.CAT_Romooc.FirstOrDefault(c => c.ID == objVehicle.CurrentRomoocID);
                                if (objRomooc != null)
                                {
                                    objRomooc.LocationID = objLocation.LocationID;
                                    objRomooc.Lat = objVehicle.Lat;
                                    objRomooc.Lng = objVehicle.Lng;
                                    objRomooc.HasContainer = true;

                                    var lstStatusLast = new List<int>() 
                                    { 
                                        -(int)SYSVarType.StatusOfCOContainerEXEmpty, 
                                        -(int)SYSVarType.StatusOfCOContainerEXLaden, 
                                        -(int)SYSVarType.StatusOfCOContainerIMEmpty, 
                                        -(int)SYSVarType.StatusOfCOContainerIMLaden, 
                                        -(int)SYSVarType.StatusOfCOContainerLOEmpty, 
                                        -(int)SYSVarType.StatusOfCOContainerLOLaden,
                                        -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                        -(int)SYSVarType.StatusOfCOContainerShipLaden,
                                        -(int)SYSVarType.StatusOfCOContainerReturnEmpty,
                                        -(int)SYSVarType.StatusOfCOContainerReturnEmptyFail
                                    };
                                    var lstStatusNext = new List<int>()
                                    {
                                        -(int)SYSVarType.StatusOfCOContainerEXEmpty, 
                                        -(int)SYSVarType.StatusOfCOContainerEXLaden, 
                                        -(int)SYSVarType.StatusOfCOContainerIMEmpty, 
                                        -(int)SYSVarType.StatusOfCOContainerIMLaden, 
                                        -(int)SYSVarType.StatusOfCOContainerLOEmpty, 
                                        -(int)SYSVarType.StatusOfCOContainerLOLaden,
                                        -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                        -(int)SYSVarType.StatusOfCOContainerShipLaden,
                                        -(int)SYSVarType.StatusOfCOContainerReturnEmpty,
                                        -(int)SYSVarType.StatusOfCOContainerReturnEmptyFail,
                                        -(int)SYSVarType.StatusOfCOContainerStop,
                                    };

                                    var last = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete)
                                        .OrderByDescending(c => c.COTOSort).Select(c => new { c.StatusOfCOContainerID, c.COTOSort, c.OPSContainerID, c.SortOrder }).FirstOrDefault();
                                    if (last != null)
                                    {
                                        var next = model.OPS_COTOContainer.Where(c => c.OPSContainerID == last.OPSContainerID && c.SortOrder > last.SortOrder)
                                            .Select(c => new { c.StatusOfCOContainerID }).FirstOrDefault();

                                        if (next != null)
                                        {
                                            if (lstStatusLast.Contains(last.StatusOfCOContainerID) && !lstStatusNext.Contains(next.StatusOfCOContainerID))
                                                objRomooc.HasContainer = false;
                                        }
                                        else
                                        {
                                            if (lstStatusLast.Contains(last.StatusOfCOContainerID))
                                                objRomooc.HasContainer = false;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        private static void MONCO_CreateTO(DataEntities model, AccountItem account, int id, int locationid, int reasionid, string reasionnote, double houradd)
        {

        }

        //public break mooc
        public static HelperTOMaster_Error MONCO_BreakRommoc(DataEntities model, AccountItem account, int id, int reasionid, string reasionnote, int? vehicleid, double houradd)
        {
            //add create new master, add new location, resort
            if (id > 0 && model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0).Count() > 0)
            {
                var objCON = model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0).Select(c => new { c.ID, COTOMasterID = c.COTOMasterID.Value, c.COTOSort, c.LocationToID }).FirstOrDefault();
                if (objCON == null)
                    throw FaultHelper.BusinessFault(null, null, "obj null");
                if (model.CAT_Reason.Where(c => c.ID == reasionid).Count() == 0)
                    return HelperTOMaster_Error.ReasionFail;

                var masterNew = MONCO_BreakRommoc_Create(model, account, objCON.COTOMasterID);
                var masterCurrent = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objCON.COTOMasterID);

                foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                {
                    if (coto.SortOrder <= objCON.COTOSort)
                    {
                        coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                    }
                    else
                    {
                        coto.COTOMasterID = masterNew.ID;
                    }
                }
                foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                {
                    if (toloc.SortOrder <= objCON.COTOSort + 1)
                    {
                        if (toloc.SortOrder == objCON.COTOSort + 1)
                        {
                            var newLocation = new OPS_COTOLocation();
                            newLocation.LocationID = toloc.LocationID;
                            newLocation.SortOrder = toloc.SortOrder;
                            newLocation.COTOMasterID = masterNew.ID;
                            newLocation.CreatedBy = account.UserName;
                            newLocation.CreatedDate = DateTime.Now;
                            newLocation.DateComeEstimate = toloc.DateLeaveEstimate.Value.AddHours(houradd);
                            newLocation.DateLeaveEstimate = toloc.DateLeaveEstimate.Value.AddHours(houradd);
                            newLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                            newLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                            model.OPS_COTOLocation.Add(newLocation);

                            masterCurrent.ETA = newLocation.DateComeEstimate.Value;
                            masterNew.ETD = newLocation.DateComeEstimate.Value;
                        }
                        toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                    }
                    else
                    {
                        toloc.COTOMasterID = masterNew.ID;
                    }
                }
                foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                {
                    if (tocontainer.COTOSort <= objCON.COTOSort)
                    {
                        tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                        if (tocontainer.COTOSort == objCON.COTOSort)
                        {
                            tocontainer.IsBreakRomooc = true;
                            tocontainer.ReasonChangeID = reasionid;
                            tocontainer.ReasonChangeNote = reasionnote;
                            tocontainer.ModifiedBy = account.UserName;
                            tocontainer.ModifiedDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        tocontainer.COTOMasterID = masterNew.ID;
                    }
                }
                masterCurrent.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterReceived;
                var vehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == masterCurrent.VehicleID);
                if (vehicle != null)
                {
                    vehicle.CurrentRomoocID = null;
                    vehicle.LocationID = objCON.LocationToID;
                }
                var romooc = model.CAT_Romooc.FirstOrDefault(c => c.ID == masterCurrent.RomoocID);
                if (romooc != null)
                {
                    romooc.HasContainer = true;
                    romooc.LocationID = objCON.LocationToID;
                }
                model.SaveChanges();

                if (model.OPS_COTOMaster.Where(c => c.ID == objCON.COTOMasterID && c.VehicleID == vehicleid).Count() > 0)
                {
                    masterNew.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterTendered;
                }
                else
                {
                    masterNew.VehicleID = DefaultTractor;
                    masterNew.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproved;
                }
                int sort = 1;
                foreach (var item in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterNew.ID).OrderBy(c => c.SortOrder))
                {
                    item.SortOrder = sort++;
                }
                sort = 1;
                foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterNew.ID).OrderBy(c => c.SortOrder))
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterNew.ID && c.COTOSort == coto.SortOrder))
                    {
                        tocontainer.COTOSort = sort;
                    }
                    coto.SortOrder = sort++;
                }

                masterCurrent.ATD = masterCurrent.ETD;
                masterCurrent.ATA = masterCurrent.ETA;
                model.SaveChanges();

                MONCO_CompleteParent(model, account, new List<int>() { objCON.COTOMasterID });
                HelperTimeSheet.Create(model, account, masterCurrent.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                HelperTimeSheet.Create(model, account, masterNew.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                HelperStatus.OPSCOMaster_Status(model, account, new List<int> { objCON.COTOMasterID });

                return HelperTOMaster_Error.None;
            }
            else
                throw FaultHelper.BusinessFault(null, null, "id null");
        }

        private static OPS_COTOMaster MONCO_BreakRommoc_Create(DataEntities model, AccountItem account, int id)
        {
            var master = model.OPS_COTOMaster.Where(c => c.ID == id).Select(c => new DTOOPSCOTOMaster
            {
                SortOrder = c.SortOrder,
                VehicleID = c.VehicleID,
                VendorOfVehicleID = c.VendorOfVehicleID,
                RomoocID = c.RomoocID,
                VendorOfRomoocID = c.VendorOfRomoocID,
                DriverID1 = c.DriverID1,
                DriverID2 = c.DriverID2,
                DriverName1 = c.DriverName1,
                DriverName2 = c.DriverName2,
                DriverCard1 = c.DriverCard1,
                DriverCard2 = c.DriverCard2,
                DriverTel1 = c.DriverTel1,
                DriverTel2 = c.DriverTel2,
                GroupOfVehicleID = c.GroupOfVehicleID,
                RateTime = c.RateTime,
                ETD = c.ETD,
                ETA = c.ETA,
                DateConfig = c.ETD,
                Note = c.Note,
                IsBidding = c.IsBidding,
                BiddingID = c.BiddingID,
                KM = c.KM,
                TransportModeID = c.TransportModeID,
                TypeOfOrderID = c.TypeOfOrderID,
                ContractID = c.ContractID
            }).FirstOrDefault();

            var objMaster = new OPS_COTOMaster();
            objMaster.CreatedBy = account.UserName;
            objMaster.CreatedDate = DateTime.Now;
            objMaster.SYSCustomerID = account.SYSCustomerID;

            objMaster.Code = OPSCO_GetLastCode(model);
            objMaster.IsHot = false;
            objMaster.RateTime = 0;
            objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproved;

            objMaster.SortOrder = master.SortOrder;
            objMaster.VehicleID = master.VehicleID;
            objMaster.VendorOfVehicleID = master.VendorOfVehicleID;
            objMaster.RomoocID = master.RomoocID;
            objMaster.VendorOfRomoocID = master.VendorOfRomoocID;
            objMaster.DriverID1 = master.DriverID1;
            objMaster.DriverID2 = master.DriverID2;
            objMaster.DriverName1 = master.DriverName1;
            objMaster.DriverName2 = master.DriverName2;
            objMaster.DriverCard1 = master.DriverCard1;
            objMaster.DriverCard2 = master.DriverCard2;
            objMaster.DriverTel1 = master.DriverTel1;
            objMaster.DriverTel2 = master.DriverTel2;
            objMaster.ApprovedBy = master.ApprovedBy;
            objMaster.ApprovedDate = master.ApprovedDate;
            objMaster.GroupOfVehicleID = master.GroupOfVehicleID;
            objMaster.RateTime = master.RateTime;
            objMaster.ETD = master.ETD;
            objMaster.ETA = master.ETA;
            objMaster.TypeOfDriverID1 = -(int)SYSVarType.TypeOfDriverMain;

            objMaster.DateConfig = master.ETD;
            objMaster.Note = master.Note;
            objMaster.IsBidding = master.IsBidding;
            objMaster.BiddingID = master.BiddingID;
            objMaster.KM = master.KM;
            objMaster.TransportModeID = master.TransportModeID;
            objMaster.TypeOfOrderID = master.TypeOfOrderID;
            objMaster.ContractID = master.ContractID;

            model.OPS_COTOMaster.Add(objMaster);
            model.SaveChanges();

            return objMaster;
        }

        private static void MONCO_ChangeLocationReload(DataEntities model, AccountItem account, int masterid)
        {
            DateTime? dt = null;
            var houradd = Hour2Point;
            var firstloc = model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid && c.SortOrder == 1).Select(c => new { c.DateComeEstimate, c.OPS_COTOMaster.ETD, c.OPS_COTOMaster.ETA }).FirstOrDefault();
            if (firstloc != null)
            {
                dt = firstloc.ETD;
                if (firstloc.DateComeEstimate != null)
                    dt = firstloc.DateComeEstimate.Value;
                if (firstloc.ETD.CompareTo(firstloc.ETA) > 0)
                {
                    int totalLocation = model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid).Count();
                    houradd = Math.Round(firstloc.ETA.Subtract(firstloc.ETD).TotalHours / totalLocation, 2, MidpointRounding.AwayFromZero);
                }

                foreach (var item in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid).OrderBy(c => c.SortOrder))
                {
                    if (item.DateComeEstimate == null)
                    {
                        item.DateComeEstimate = dt;
                        item.DateLeaveEstimate = dt;
                        dt = dt.Value.AddHours(houradd);
                    }
                    else if (item.DateComeEstimate != null && dt.Value.CompareTo(item.DateComeEstimate.Value) > 0)
                    {
                        item.DateComeEstimate = dt;
                        item.DateLeaveEstimate = dt;
                        dt = dt.Value.AddHours(houradd);
                    }
                    else if (dt.Value.CompareTo(item.DateComeEstimate.Value) == 0)
                    {
                        dt = dt.Value.AddHours(houradd);
                    }
                    else
                    {
                        dt = item.DateComeEstimate.Value.AddHours(houradd);
                    }
                }

                model.SaveChanges();
            }
        }


        public static HelperTOMaster_Error MONCO_RepairEmpty(DataEntities model, AccountItem account, int id, int locationid, int reasionid, string reasionnote, double houradd)
        {
            if (id > 0 && model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0 && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerShipEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty)).Count() > 0)
            {
                var objCON = model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0).Select(c => new { c.ID, c.ETA, c.COTOSort, COTOMasterID = c.COTOMasterID.Value, c.LocationToID }).FirstOrDefault();
                if (objCON == null)
                    throw FaultHelper.BusinessFault(null, null, "obj null");
                if (model.CAT_Reason.Where(c => c.ID == reasionid).Count() == 0)
                    return HelperTOMaster_Error.ReasionFail;
                if (objCON.LocationToID == locationid)
                    return HelperTOMaster_Error.VehicleLocation;

                //Default 
                if (houradd <= 0)
                    houradd = Hour2Point;
                var hourSplit = Math.Round(houradd / 2, 2, MidpointRounding.AwayFromZero);

                //var objTOLocationStart = model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOLocationStatusID >= -(int)SYSVarType.COTOLocationStatusCome && c.COTOLocationStatusID < -(int)SYSVarType.COTOLocationStatusLeave).OrderByDescending(c => c.SortOrder).FirstOrDefault();
                if (objCON.LocationToID != locationid)
                {
                    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                    {
                        if (toloc.SortOrder <= objCON.COTOSort + 1)
                        {
                            toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                        }
                        else
                        {
                            toloc.SortOrder += 2;
                        }
                    }
                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                    {
                        if (coto.SortOrder <= objCON.COTOSort)
                        {
                            coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                        }
                        else
                        {
                            coto.SortOrder += 2;
                        }
                    }
                    var lstAdd = new List<OPS_COTOContainer>();
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                    {
                        if (tocontainer.COTOSort <= objCON.COTOSort)
                        {
                            if (tocontainer.COTOSort == objCON.COTOSort)
                            {
                                //add new stop
                                lstAdd.Add(new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = tocontainer.ParentID,
                                    OPSContainerID = tocontainer.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerRepairEmpty,
                                    CreateByMasterID = tocontainer.COTOMasterID,

                                    COTOMasterID = tocontainer.COTOMasterID,
                                    LocationFromID = tocontainer.LocationToID,
                                    LocationToID = locationid,
                                    ETD = tocontainer.ETA,
                                    ETA = tocontainer.ETA.Value.AddHours(houradd),
                                    ETDStart = tocontainer.ETA,
                                    ETAStart = tocontainer.ETA.Value.AddHours(houradd),
                                    SortOrder = tocontainer.SortOrder + 1,
                                    COTOSort = tocontainer.COTOSort + 1,
                                    IsSplit = false,
                                    ReasonChangeID = reasionid,
                                    ReasonChangeNote = reasionnote
                                });
                                //add new stop
                                lstAdd.Add(new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = tocontainer.ParentID,
                                    OPSContainerID = tocontainer.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                    CreateByMasterID = tocontainer.COTOMasterID,

                                    COTOMasterID = tocontainer.COTOMasterID,
                                    LocationFromID = locationid,
                                    LocationToID = tocontainer.LocationToID,
                                    ETD = tocontainer.ETA.Value.AddHours(houradd),
                                    ETA = tocontainer.ETA.Value.AddHours(houradd * 2),
                                    ETDStart = tocontainer.ETA.Value.AddHours(houradd),
                                    ETAStart = tocontainer.ETA.Value.AddHours(houradd * 2),
                                    SortOrder = tocontainer.SortOrder + 2,
                                    COTOSort = tocontainer.COTOSort + 2,
                                    IsSplit = false,
                                    ReasonChangeID = reasionid,
                                    ReasonChangeNote = reasionnote
                                });
                            }
                            tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                        }
                        else
                        {
                            tocontainer.SortOrder += 2;
                            tocontainer.COTOSort += 2;
                        }
                    }

                    var objTONew1 = new OPS_COTO();
                    objTONew1.CreatedBy = account.UserName;
                    objTONew1.CreatedDate = DateTime.Now;
                    objTONew1.COTOMasterID = objCON.COTOMasterID;
                    objTONew1.IsOPS = true;
                    objTONew1.SortOrder = objCON.COTOSort + 1;
                    objTONew1.LocationFromID = objCON.LocationToID;
                    objTONew1.LocationToID = locationid;
                    objTONew1.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                    model.OPS_COTO.Add(objTONew1);

                    var objTONew2 = new OPS_COTO();
                    objTONew2.CreatedBy = account.UserName;
                    objTONew2.CreatedDate = DateTime.Now;
                    objTONew2.COTOMasterID = objCON.COTOMasterID;
                    objTONew2.IsOPS = true;
                    objTONew2.SortOrder = objCON.COTOSort + 2;
                    objTONew2.LocationFromID = locationid;
                    objTONew2.LocationToID = objCON.LocationToID;
                    objTONew2.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                    model.OPS_COTO.Add(objTONew2);

                    var objTOLocationNew1 = new OPS_COTOLocation();
                    objTOLocationNew1.LocationID = locationid;
                    objTOLocationNew1.SortOrder = objCON.COTOSort + 2;
                    objTOLocationNew1.COTOMasterID = objCON.COTOMasterID;
                    objTOLocationNew1.CreatedBy = account.UserName;
                    objTOLocationNew1.CreatedDate = DateTime.Now;
                    objTOLocationNew1.DateComeEstimate = objCON.ETA.Value.AddHours(houradd);
                    objTOLocationNew1.DateLeaveEstimate = objCON.ETA.Value.AddHours(houradd);
                    objTOLocationNew1.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                    objTOLocationNew1.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                    model.OPS_COTOLocation.Add(objTOLocationNew1);

                    var objTOLocationNew2 = new OPS_COTOLocation();
                    objTOLocationNew2.LocationID = objCON.LocationToID;
                    objTOLocationNew2.SortOrder = objCON.COTOSort + 3;
                    objTOLocationNew2.COTOMasterID = objCON.COTOMasterID;
                    objTOLocationNew2.CreatedBy = account.UserName;
                    objTOLocationNew2.CreatedDate = DateTime.Now;
                    objTOLocationNew2.DateComeEstimate = objCON.ETA.Value.AddHours(houradd * 2);
                    objTOLocationNew2.DateLeaveEstimate = objCON.ETA.Value.AddHours(houradd * 2);
                    objTOLocationNew2.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                    objTOLocationNew2.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                    model.OPS_COTOLocation.Add(objTOLocationNew2);

                    foreach (var item in lstAdd)
                    {
                        model.OPS_COTOContainer.Add(item);
                    }
                    model.SaveChanges();

                    MONCO_ChangeLocationReload(model, account, objCON.COTOMasterID);

                    return HelperTOMaster_Error.None;
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "same location");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "id null");
        }

        //check 2 container 
        public static HelperTOMaster_Error MONCO_DestroyEmpty(DataEntities model, AccountItem account, int id, int locationid, bool fail2container, int reasionid, string reasionnote, DateTime datereturnempty, DateTime dateshipempty)
        {
            if (id > 0 && model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0).Count() > 0)
            {
                var objCON = model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0).Select(c => new { c.ID, c.ParentID, c.ETA, c.LocationToID, c.COTOSort, c.SortOrder, c.OPS_Container.ContainerNo, c.OPSContainerID, ORDContainerID = c.OPS_Container.ContainerID, COTOMasterID = c.COTOMasterID.Value }).FirstOrDefault();
                if (objCON == null)
                    throw FaultHelper.BusinessFault(null, null, "obj null");
                if (model.CAT_Reason.Where(c => c.ID == reasionid).Count() == 0)
                    return HelperTOMaster_Error.ReasionFail;
                if (objCON.LocationToID == locationid)
                    return HelperTOMaster_Error.VehicleLocation;

                var lstStatusEmptyID = new List<int>() { -(int)SYSVarType.StatusOfCOContainerEXEmpty, -(int)SYSVarType.StatusOfCOContainerLOGetEmpty };
                var lstStatusLadenID = new List<int>() { -(int)SYSVarType.StatusOfCOContainerEXLaden, -(int)SYSVarType.StatusOfCOContainerShipLaden };
                var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID).Select(c => new { c.OPSContainerID, c.OPS_Container.ContainerID, c.OPS_Container.ContainerNo, c.OPS_Container.SealNo1, c.OPS_Container.SealNo2 }).Distinct().ToList();
                var lstOPSContainerID = lstOPSContainer.Select(c => c.OPSContainerID).Distinct().ToList();
                var lstParent = model.OPS_COTOContainer.Where(c => lstOPSContainerID.Contains(c.OPSContainerID) && lstStatusEmptyID.Contains(c.StatusOfCOContainerID)).Select(c => new { c.ID, c.OPSContainerID }).ToList();
                var lstCOTOContainer = model.OPS_COTOContainer.Where(c => lstOPSContainerID.Contains(c.OPSContainerID)).ToList();
                if (lstParent.Count == 0)
                    throw FaultHelper.BusinessFault(null, null, "list parent null");

                var lstCOTOContainerAdd = new List<OPS_COTOContainer>();
                foreach (var opsContainer in lstOPSContainer)
                {
                    if (lstCOTOContainer.Where(c => c.OPSContainerID == opsContainer.OPSContainerID && lstStatusLadenID.Contains(c.StatusOfCOContainerID)).Count() > 0)
                    {
                        //create return empty fail  
                        var opsContainerNew = new OPS_Container();
                        opsContainerNew.ContainerID = opsContainer.ContainerID;
                        opsContainerNew.ContainerNo = opsContainer.ContainerNo;
                        opsContainerNew.SealNo1 = opsContainer.SealNo1;
                        opsContainerNew.SealNo2 = opsContainer.SealNo2;
                        opsContainerNew.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeRunning;
                        opsContainerNew.CreatedBy = account.UserName;
                        opsContainerNew.CreatedDate = DateTime.Now;
                        model.OPS_Container.Add(opsContainerNew);

                        //change and add new data 
                        var lstParentID = lstParent.Where(c => c.OPSContainerID == opsContainer.OPSContainerID).Select(c => c.ID).Distinct().ToList();
                        foreach (var parent in lstCOTOContainer.Where(c => lstParentID.Contains(c.ID)))
                        {
                            parent.OPS_Container = opsContainerNew;
                            //add new parent for old container
                            lstCOTOContainerAdd.Add(new OPS_COTOContainer
                            {
                                CreatedBy = account.UserName,
                                CreatedDate = DateTime.Now,
                                LocationFromID = locationid,
                                LocationToID = objCON.LocationToID,
                                OPSContainerID = objCON.OPSContainerID,
                                COTOMasterID = objCON.COTOMasterID,
                                CreateByMasterID = objCON.COTOMasterID,
                                IsSwap = false,
                                IsInput = false,
                                IsSplit = true,
                                StatusOfCOContainerID = parent.StatusOfCOContainerID,
                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                ETD = parent.ETD,
                                ETA = parent.ETA,
                                ETDStart = parent.ETD,
                                ETAStart = parent.ETA,
                                SortOrder = parent.SortOrder,
                                COTOSort = parent.COTOSort
                            });
                            //add new child for old container
                            lstCOTOContainerAdd.Add(new OPS_COTOContainer
                            {
                                OPS_COTOContainer2 = lstCOTOContainer[lstCOTOContainer.Count - 1],
                                CreatedBy = account.UserName,
                                CreatedDate = DateTime.Now,
                                LocationFromID = locationid,
                                LocationToID = objCON.LocationToID,
                                OPSContainerID = objCON.OPSContainerID,
                                COTOMasterID = objCON.COTOMasterID,
                                CreateByMasterID = objCON.COTOMasterID,
                                IsSwap = false,
                                IsInput = false,
                                IsSplit = false,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                ETD = datereturnempty,
                                ETA = dateshipempty,
                                ETDStart = datereturnempty,
                                ETAStart = dateshipempty,
                                SortOrder = objCON.SortOrder,
                                COTOSort = objCON.COTOSort + 2
                            });
                            //add new child for new container
                            lstCOTOContainerAdd.Add(new OPS_COTOContainer
                            {
                                ParentID = objCON.ParentID,
                                CreatedBy = account.UserName,
                                CreatedDate = DateTime.Now,
                                LocationFromID = objCON.LocationToID,
                                LocationToID = locationid,
                                OPS_Container = opsContainerNew,
                                COTOMasterID = objCON.COTOMasterID,
                                CreateByMasterID = objCON.COTOMasterID,
                                IsSwap = false,
                                IsInput = false,
                                IsSplit = false,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerReturnEmptyFail,
                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                ETD = objCON.ETA,
                                ETA = datereturnempty,
                                ETDStart = objCON.ETA,
                                ETAStart = datereturnempty,
                                SortOrder = objCON.SortOrder,
                                COTOSort = objCON.COTOSort + 1
                            });

                            MONCO_DestroyEmpty_DivEmpty(model, account, opsContainerNew, lstCOTOContainer, parent.ID, objCON.COTOMasterID);
                        }
                    }
                }

                //Add sort for container
                foreach (var tocontainer in lstCOTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                {
                    if (tocontainer.COTOSort <= objCON.COTOSort)
                    {
                        tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                    }
                    else
                    {
                        tocontainer.SortOrder += 2;
                        tocontainer.COTOSort += 2;
                    }
                }
                //add new container
                foreach (var tocontainer in lstCOTOContainerAdd)
                {
                    model.OPS_COTOContainer.Add(tocontainer);
                }

                foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                {
                    if (toloc.SortOrder <= objCON.COTOSort + 1)
                    {
                        toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                    }
                    else
                        toloc.SortOrder += 2;
                }
                foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                {
                    if (coto.SortOrder <= objCON.COTOSort)
                    {
                        coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                    }
                    else
                        coto.SortOrder += 2;
                }

                //add co new 1
                var objCOTONew1 = new OPS_COTO();
                objCOTONew1.CreatedBy = account.UserName;
                objCOTONew1.CreatedDate = DateTime.Now;
                objCOTONew1.COTOMasterID = objCON.COTOMasterID;
                objCOTONew1.IsOPS = true;
                objCOTONew1.SortOrder = objCON.COTOSort + 1;
                objCOTONew1.LocationFromID = objCON.LocationToID;
                objCOTONew1.LocationToID = locationid;
                objCOTONew1.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                model.OPS_COTO.Add(objCOTONew1);

                //add co new 2
                var objCOTONew2 = new OPS_COTO();
                objCOTONew2.CreatedBy = account.UserName;
                objCOTONew2.CreatedDate = DateTime.Now;
                objCOTONew2.COTOMasterID = objCON.COTOMasterID;
                objCOTONew2.IsOPS = true;
                objCOTONew2.SortOrder = objCON.COTOSort + 2;
                objCOTONew2.LocationFromID = objCON.LocationToID;
                objCOTONew2.LocationToID = locationid;
                objCOTONew2.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                model.OPS_COTO.Add(objCOTONew2);

                //add location new 1
                var objLocationNew1 = new OPS_COTOLocation();
                objLocationNew1.LocationID = locationid;
                objLocationNew1.SortOrder = objCON.COTOSort + 2;
                objLocationNew1.COTOMasterID = objCON.COTOMasterID;
                objLocationNew1.CreatedBy = account.UserName;
                objLocationNew1.CreatedDate = DateTime.Now;
                objLocationNew1.DateComeEstimate = datereturnempty;
                objLocationNew1.DateLeaveEstimate = datereturnempty;
                objLocationNew1.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                objLocationNew1.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                model.OPS_COTOLocation.Add(objLocationNew1);

                //add location new 2
                var objLocationNew2 = new OPS_COTOLocation();
                objLocationNew2.LocationID = objCON.LocationToID;
                objLocationNew2.SortOrder = objCON.COTOSort + 3;
                objLocationNew2.COTOMasterID = objCON.COTOMasterID;
                objLocationNew2.CreatedBy = account.UserName;
                objLocationNew2.CreatedDate = DateTime.Now;
                objLocationNew2.DateComeEstimate = dateshipempty;
                objLocationNew2.DateLeaveEstimate = dateshipempty;
                objLocationNew2.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                objLocationNew2.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                model.OPS_COTOLocation.Add(objLocationNew2);

                model.SaveChanges();

                MONCO_ChangeLocationReload(model, account, objCON.COTOMasterID);

                return HelperTOMaster_Error.None;
            }
            else
                throw FaultHelper.BusinessFault(null, null, "id null");
        }

        private static void MONCO_DestroyEmpty_DivEmpty(DataEntities model, AccountItem account, OPS_Container opsContainerNew, List<OPS_COTOContainer> lstCOTOContainer, int? parentid, int cotomasterid)
        {
            foreach (var tocontainer in lstCOTOContainer.Where(c => c.ParentID == parentid))
            {
                if (tocontainer.COTOMasterID == cotomasterid)
                    tocontainer.OPS_Container = opsContainerNew;
                MONCO_DestroyEmpty_DivEmpty(model, account, opsContainerNew, lstCOTOContainer, tocontainer.ID, cotomasterid);
            }
        }

        private static void MONCO_DestroyEmpty_ChangeContainer(DataEntities model, AccountItem account, OPS_Container container, OPS_COTOContainer parent, List<OPS_COTOContainer> lstCOTOContainer,
            int? parentid, int opsContainerID, List<int> lstParentID, int locationid, DateTime datereturnempty, DateTime dateshipempty, int masteroldid, int lastsort)
        {
            if (parentid == null)
            {
                foreach (var tocontainer in lstCOTOContainer.Where(c => c.OPSContainerID == opsContainerID && lstParentID.Contains(c.ID)))
                {
                    var objNewCopy = new OPS_COTOContainer();
                    objNewCopy.CreatedBy = account.UserName;
                    objNewCopy.CreatedDate = DateTime.Now;
                    objNewCopy.LocationFromID = tocontainer.LocationFromID;
                    objNewCopy.LocationToID = tocontainer.LocationToID;
                    objNewCopy.OPS_Container = container;
                    objNewCopy.COTOMasterID = tocontainer.COTOMasterID;
                    objNewCopy.CreateByMasterID = tocontainer.COTOMasterID;
                    objNewCopy.IsSwap = false;
                    objNewCopy.IsInput = false;
                    objNewCopy.IsSplit = tocontainer.IsSplit;
                    objNewCopy.StatusOfCOContainerID = tocontainer.StatusOfCOContainerID;
                    objNewCopy.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                    objNewCopy.TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait;
                    objNewCopy.ETD = tocontainer.ETD;
                    objNewCopy.ETA = tocontainer.ETA;
                    objNewCopy.ETDStart = tocontainer.ETD;
                    objNewCopy.ETAStart = tocontainer.ETA;
                    objNewCopy.SortOrder = tocontainer.SortOrder + 1;
                    objNewCopy.COTOSort = lastsort;
                    model.OPS_COTOContainer.Add(objNewCopy);

                    var objOldCopy2 = new OPS_COTOContainer();
                    objOldCopy2.CreatedBy = account.UserName;
                    objOldCopy2.CreatedDate = DateTime.Now;
                    objOldCopy2.OPS_COTOContainer2 = tocontainer;
                    objOldCopy2.LocationFromID = locationid;
                    objOldCopy2.LocationToID = tocontainer.LocationToID;
                    objOldCopy2.OPSContainerID = tocontainer.OPSContainerID;
                    objOldCopy2.COTOMasterID = tocontainer.COTOMasterID;
                    objOldCopy2.CreateByMasterID = tocontainer.COTOMasterID;
                    objOldCopy2.IsSwap = false;
                    objOldCopy2.IsInput = false;
                    objOldCopy2.IsSplit = false;
                    objOldCopy2.StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty;
                    objOldCopy2.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                    objOldCopy2.TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait;
                    objOldCopy2.ETD = datereturnempty;
                    objOldCopy2.ETA = dateshipempty;
                    objOldCopy2.ETDStart = datereturnempty;
                    objOldCopy2.ETAStart = dateshipempty;
                    objOldCopy2.SortOrder = tocontainer.SortOrder;
                    objOldCopy2.COTOSort = lastsort + 2;
                    model.OPS_COTOContainer.Add(objOldCopy2);

                    MONCO_DestroyEmpty_ChangeContainer(model, account, container, objNewCopy, lstCOTOContainer, tocontainer.ID, opsContainerID, lstParentID,
                        locationid, datereturnempty, dateshipempty, masteroldid, lastsort);
                }
            }
            else
            {
                foreach (var tocontainer in lstCOTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.ParentID == parentid.Value))
                {
                    tocontainer.OPS_Container = container;
                    tocontainer.SortOrder += 2;
                    if (parent != null)
                        tocontainer.OPS_COTOContainer2 = parent;
                    MONCO_DestroyEmpty_ChangeContainer(model, account, container, null, lstCOTOContainer, tocontainer.ID, opsContainerID, lstParentID,
                         locationid, datereturnempty, dateshipempty, masteroldid, lastsort);

                    if (tocontainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerShipEmpty && tocontainer.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete)
                    {
                        var objOldCopy = new OPS_COTOContainer();
                        objOldCopy.CreatedBy = account.UserName;
                        objOldCopy.CreatedDate = DateTime.Now;
                        objOldCopy.LocationFromID = tocontainer.LocationToID;
                        objOldCopy.LocationToID = locationid;
                        objOldCopy.OPS_Container = container;
                        objOldCopy.OPS_COTOContainer2 = parent;
                        objOldCopy.COTOMasterID = tocontainer.COTOMasterID;
                        objOldCopy.CreateByMasterID = tocontainer.COTOMasterID;
                        objOldCopy.IsSwap = false;
                        objOldCopy.IsInput = false;
                        objOldCopy.IsSplit = tocontainer.IsSplit;
                        objOldCopy.StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerReturnEmptyFail;
                        objOldCopy.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                        objOldCopy.TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait;
                        objOldCopy.ETD = tocontainer.ETA;
                        objOldCopy.ETA = datereturnempty;
                        objOldCopy.ETDStart = tocontainer.ETA;
                        objOldCopy.ETAStart = datereturnempty;
                        objOldCopy.SortOrder = tocontainer.SortOrder + 1;
                        objOldCopy.COTOSort = lastsort + 1;
                        model.OPS_COTOContainer.Add(objOldCopy);
                    }
                }
            }
        }

        private static void MONCO_DestroyEmpty_SortContainer(List<OPS_COTOContainer> lstCOTOContainer, int opsContainerID, int? parentid, int parentsort)
        {
            var query = lstCOTOContainer.Where(c => c.ParentID == null && c.OPSContainerID == opsContainerID);
            if (parentid > 0)
                query = lstCOTOContainer.Where(c => c.ParentID == parentid.Value && c.OPSContainerID == opsContainerID);
            int sort = parentsort;
            foreach (var item in query)
            {
                item.SortOrder = sort;
                MONCO_DestroyEmpty_SortContainer(lstCOTOContainer, opsContainerID, item.ID, sort);
                if (parentid > 0)
                    sort++;
                else
                    sort += 20;
            }
        }

        public static HelperTOMaster_Error MONCO_TOContainerRun(DataEntities model, AccountItem account, int id)
        {
            if (id > 0 && model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0 && c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerWait).Count() > 0)
            {
                var objContainer = model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0).Select(c => new { c.ID, COTOMasterID = c.COTOMasterID.Value, c.OPS_COTOMaster.VehicleID, c.StatusOfCOContainerID, c.OPSContainerID, c.SortOrder, c.OPS_COTOMaster.ETD }).FirstOrDefault();
                if (objContainer != null)
                {
                    if (model.OPS_COTOContainer.Where(c => c.OPSContainerID == objContainer.OPSContainerID && c.IsSplit == false &&
                        c.SortOrder < objContainer.SortOrder && c.TypeOfStatusContainerID < -(int)SYSVarType.TypeOfStatusContainerComplete).Count() > 0)
                        throw FaultHelper.BusinessFault(null, null, "ops con prev fail");
                    if (model.CAT_Vehicle.Where(c => c.ID == objContainer.VehicleID).Count() == 0)
                        throw FaultHelper.BusinessFault(null, null, "vehicle id fail");
                    if (model.OPS_COTOLocation.Where(c => c.COTOMasterID == objContainer.COTOMasterID && c.SortOrder == 1 && c.COTOLocationStatusID < -(int)SYSVarType.COTOLocationStatusLeave).Count() > 0)
                    {
                        var lstStatusGetEmpty = new List<int>(){
                            -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                            -(int)SYSVarType.StatusOfCOContainerRepairEmpty,
                            -(int)SYSVarType.StatusOfCOContainerReturnEmpty,
                            -(int)SYSVarType.StatusOfCOContainerEXLaden,
                            -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty,
                        };
                        var lstStatusGetLaden = new List<int>(){
                            -(int)SYSVarType.StatusOfCOContainerShipLaden,
                            -(int)SYSVarType.StatusOfCOContainerEXLaden,
                            -(int)SYSVarType.StatusOfCOContainerLOLaden,
                        };
                        //Add location first
                        var objLoc = model.OPS_COTOLocation.Where(c => c.COTOMasterID == objContainer.COTOMasterID && c.SortOrder == 1).Select(c => new { c.LocationID }).FirstOrDefault();
                        if (objLoc != null)
                        {
                            var vehiclelocation = model.CAT_Vehicle.Where(c => c.ID == objContainer.VehicleID).Select(c => new { c.LocationID }).FirstOrDefault();
                            if (vehiclelocation == null || vehiclelocation.LocationID == null || vehiclelocation.LocationID < 1)
                                return HelperTOMaster_Error.VehicleLocation;
                            if (objLoc.LocationID != vehiclelocation.LocationID)
                            {
                                foreach (var item in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objContainer.COTOMasterID))
                                {
                                    item.SortOrder++;
                                }
                                foreach (var item in model.OPS_COTO.Where(c => c.COTOMasterID == objContainer.COTOMasterID))
                                {
                                    item.SortOrder++;
                                }
                                foreach (var item in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objContainer.COTOMasterID))
                                {
                                    item.SortOrder++;
                                }

                                var objTONew = new OPS_COTO();
                                objTONew.CreatedBy = account.UserName;
                                objTONew.CreatedDate = DateTime.Now;
                                objTONew.COTOMasterID = objContainer.COTOMasterID;
                                objTONew.IsOPS = true;
                                objTONew.SortOrder = 1;
                                objTONew.LocationFromID = vehiclelocation.LocationID;
                                objTONew.LocationToID = objLoc.LocationID;
                                objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                                model.OPS_COTO.Add(objTONew);

                                var objTOLocationNew = new OPS_COTOLocation();
                                objTOLocationNew.LocationID = vehiclelocation.LocationID;
                                objTOLocationNew.SortOrder = 1;
                                objTOLocationNew.COTOMasterID = objContainer.COTOMasterID;
                                objTOLocationNew.CreatedBy = account.UserName;
                                objTOLocationNew.CreatedDate = DateTime.Now;
                                objTOLocationNew.DateComeEstimate = objContainer.ETD;
                                objTOLocationNew.DateLeaveEstimate = objContainer.ETD;
                                objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                model.OPS_COTOLocation.Add(objTOLocationNew);

                                model.SaveChanges();

                                var lstOPSContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objContainer.COTOMasterID && c.IsSplit == false && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel).Select(c => c.OPSContainerID).Distinct().ToList();
                                foreach (var opsContainerID in lstOPSContainerID)
                                {
                                    var objCONFirst = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opsContainerID && c.COTOMasterID == objContainer.COTOMasterID &&
                                        c.IsSplit == false && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel).OrderBy(c => c.SortOrder).FirstOrDefault();
                                    if (objCONFirst != null)
                                    {
                                        var statusid = -(int)SYSVarType.StatusOfCOContainerStop;
                                        if (lstStatusGetEmpty.Contains(objCONFirst.StatusOfCOContainerID))
                                            statusid = -(int)SYSVarType.StatusOfCOContainerGetEmpty;
                                        else if (lstStatusGetLaden.Contains(objCONFirst.StatusOfCOContainerID))
                                            statusid = -(int)SYSVarType.StatusOfCOContainerGetLaden;

                                        var obj = new OPS_COTOContainer()
                                        {
                                            CreatedBy = account.UserName,
                                            CreatedDate = DateTime.Now,

                                            ParentID = objCONFirst.ParentID,
                                            OPSContainerID = objCONFirst.OPSContainerID,
                                            IsSwap = false,
                                            IsInput = false,
                                            TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                            TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                            StatusOfCOContainerID = statusid,
                                            CreateByMasterID = objContainer.COTOMasterID,

                                            COTOMasterID = objContainer.COTOMasterID,
                                            LocationFromID = vehiclelocation.LocationID.Value,
                                            LocationToID = objCONFirst.LocationFromID,
                                            ETD = objContainer.ETD,
                                            ETA = objCONFirst.ETD,
                                            ETDStart = objContainer.ETD,
                                            ETAStart = objCONFirst.ETD,
                                            SortOrder = objCONFirst.SortOrder,
                                            IsSplit = false
                                        };
                                        model.OPS_COTOContainer.Add(obj);
                                    }
                                }
                                model.SaveChanges();

                                return HelperTOMaster_Error.None;
                            }
                        }
                        else
                            throw FaultHelper.BusinessFault(null, null, "loc id fail");
                    }

                    //Change first
                    var objLocationFirst = model.OPS_COTOLocation.Where(c => c.COTOMasterID == objContainer.COTOMasterID && c.SortOrder == 1 && c.COTOLocationStatusID == -(int)SYSVarType.COTOLocationStatusPlan).OrderBy(c => c.SortOrder).FirstOrDefault();
                    if (objLocationFirst != null)
                    {
                        objLocationFirst.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusCome;
                        objLocationFirst.ModifiedBy = account.UserName;
                        objLocationFirst.ModifiedDate = DateTime.Now;
                        model.SaveChanges();
                    }

                    //Change location, co
                    var objCOTO = model.OPS_COTO.Where(c => c.COTOMasterID == objContainer.COTOMasterID && c.COTOStatusID == -(int)SYSVarType.COTOStatusOpen).OrderBy(c => c.SortOrder).FirstOrDefault();
                    var objLocation = model.OPS_COTOLocation.Where(c => c.COTOMasterID == objContainer.COTOMasterID &&
                        (c.COTOLocationStatusID == -(int)SYSVarType.COTOLocationStatusCome || c.COTOLocationStatusID == -(int)SYSVarType.COTOLocationStatusLoadStart || c.COTOLocationStatusID == -(int)SYSVarType.COTOLocationStatusLoadEnd)).OrderBy(c => c.SortOrder).FirstOrDefault();

                    if (objCOTO != null && objLocation != null)
                    {
                        if (objCOTO.LocationFromID == objLocation.LocationID)
                        {
                            var lstOPSContainerID = model.OPS_COTOContainer.Where(c => c.IsSplit == false && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel && c.COTOMasterID == objContainer.COTOMasterID).Select(c => c.OPSContainerID).Distinct().ToList();
                            foreach (var opsContainerID in lstOPSContainerID)
                            {
                                var obj = model.OPS_COTOContainer.Where(c => c.IsSplit == false && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel && c.COTOMasterID == objContainer.COTOMasterID && c.OPSContainerID == opsContainerID && c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerWait).OrderBy(c => c.SortOrder).FirstOrDefault();
                                if (obj != null)
                                {
                                    obj.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                                    obj.ModifiedBy = account.UserName;
                                    obj.ModifiedDate = DateTime.Now;
                                }
                            }

                            objCOTO.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                            objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;

                            model.SaveChanges();

                            return HelperTOMaster_Error.None;
                        }
                        else
                            throw FaultHelper.BusinessFault(null, null, "co,location different");
                    }
                    else
                        throw FaultHelper.BusinessFault(null, null, "co,location null");
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "id null");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "id null");
        }

        private static int MONCO_TOContainerComplete_Run(DataEntities model, AccountItem account, int id, int? resionid = null, string resionnote = "", double? houradd = null)
        {
            if (id > 0 && model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0 && c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerTranfer).Count() > 0)
            {
                var objContainer = model.OPS_COTOContainer.Where(c => c.ID == id).Select(c => new { c.ID, COTOMasterID = c.COTOMasterID.Value }).FirstOrDefault();
                if (objContainer != null)
                {
                    var objCOTO = model.OPS_COTO.Where(c => c.COTOMasterID == objContainer.COTOMasterID && c.COTOStatusID == -(int)SYSVarType.COTOStatusRunning).OrderBy(c => c.SortOrder).FirstOrDefault();
                    var objLocation = model.OPS_COTOLocation.Where(c => c.COTOMasterID == objContainer.COTOMasterID && c.COTOLocationStatusID == -(int)SYSVarType.COTOLocationStatusPlan).OrderBy(c => c.SortOrder).FirstOrDefault();

                    if (objCOTO != null && objLocation != null)
                    {
                        var lstNext = model.OPS_COTOContainer.Where(c => c.IsSplit == false && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel &&
                                c.COTOMasterID == objContainer.COTOMasterID &&
                                c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerWait).OrderBy(c => c.SortOrder)
                                .Select(c => new { c.ID, c.OPSContainerID, c.SortOrder }).ToList();
                        int opsContainerNextID = -1;
                        var lstCOTOContainer = model.OPS_COTOContainer.Where(c => c.IsSplit == false && c.LocationFromID == objCOTO.LocationFromID && c.LocationToID == objCOTO.LocationToID &&
                            c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel &&
                            c.COTOMasterID == objContainer.COTOMasterID && c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerTranfer).ToList();
                        if (lstCOTOContainer.Count > 0)
                        {
                            foreach (var itemContainer in lstCOTOContainer)
                            {
                                itemContainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                                itemContainer.ModifiedBy = account.UserName;
                                itemContainer.ModifiedDate = DateTime.Now;

                                if (resionid > 0)
                                {
                                    itemContainer.ReasonChangeID = resionid;
                                    itemContainer.ReasonChangeNote = resionnote;
                                }

                                var objNext = lstNext.Where(c => c.OPSContainerID == itemContainer.OPSContainerID && c.SortOrder > itemContainer.SortOrder).FirstOrDefault();
                                if (objNext != null)
                                    opsContainerNextID = objNext.ID;
                            }

                            objCOTO.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                            objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusCome;

                            model.SaveChanges();

                            MONCO_CompleteParent(model, account, new List<int>() { objContainer.COTOMasterID });
                            return opsContainerNextID;
                        }
                        else
                            throw FaultHelper.BusinessFault(null, null, "list con null");
                    }
                    else
                        throw FaultHelper.BusinessFault(null, null, "id null");
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "id null");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "id null");
        }

        public static HelperTOMaster_Error MONCO_TOContainerComplete_Next(DataEntities model, AccountItem account, int id)
        {
            var nextid = MONCO_TOContainerComplete_Run(model, account, id);
            return MONCO_TOContainerRun(model, account, nextid);
        }



        private static void MONCO_TOContainer_ResetDetail(DataEntities model, AccountItem account, int tomasterid)
        {
            var lstCOTO = model.OPS_COTO.Where(c => c.COTOMasterID == tomasterid).Select(c => new { c.ID, c.SortOrder }).ToList();
            bool flag = false;
            foreach (var coto in lstCOTO)
            {
                if (model.OPS_COTODetail.Where(c => c.COTOID == coto.ID).Count() == 0)
                {
                    var first = model.OPS_COTOContainer.Where(c => c.COTOMasterID == tomasterid && c.COTOSort == coto.SortOrder && c.IsSplit == false && c.IsDuplicateHidden != true).Select(c => new { c.ID, c.ParentID }).FirstOrDefault();
                    if (first != null)
                    {
                        int id = first.ID;
                        if (first.ParentID > 0)
                        {
                            id = first.ParentID.Value;
                            first = model.OPS_COTOContainer.Where(c => c.COTOMasterID == tomasterid && c.ID == id).Select(c => new { c.ID, c.ParentID }).FirstOrDefault();
                            if (first != null)
                            {
                                id = first.ID;
                                if (first.ParentID > 0)
                                {
                                    id = first.ParentID.Value;
                                    first = model.OPS_COTOContainer.Where(c => c.COTOMasterID == tomasterid && c.ID == id).Select(c => new { c.ID, c.ParentID }).FirstOrDefault();
                                    if (first != null)
                                    {
                                        id = first.ID;
                                        if (first.ParentID > 0)
                                        {
                                            id = first.ParentID.Value;
                                        }
                                    }
                                }
                            }
                        }

                        if (id > 0)
                        {
                            var obj = new OPS_COTODetail();
                            obj.COTOID = coto.ID;
                            obj.COTOContainerID = id;
                            obj.CreatedBy = account.UserName;
                            obj.CreatedDate = DateTime.Now;
                            model.OPS_COTODetail.Add(obj);

                            flag = true;
                        }
                    }
                }
            }
            if (flag)
                model.SaveChanges();
        }

        public static HelperTOMaster_Error MONCO_BreakTO(DataEntities model, AccountItem account, int id, int locationid, int reasionid, string reasionnote)
        {
            if (id > 0 && model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0).Count() > 0)
            {
                var objCON = model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0).Select(c => new { c.ID, COTOMasterID = c.COTOMasterID.Value, c.LocationFromID, c.LocationToID, c.StatusOfCOContainerID, c.ETD, c.ETA, c.COTOSort }).FirstOrDefault();
                if (objCON == null)
                    throw FaultHelper.BusinessFault(null, null, "obj null");
                if (model.CAT_Reason.Where(c => c.ID == reasionid).Count() == 0)
                    return HelperTOMaster_Error.ReasionFail;
                if (objCON.LocationToID == locationid || model.CAT_Location.Where(c => c.ID == locationid).Count() == 0)
                    return HelperTOMaster_Error.VehicleLocation;

                foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                {
                    if (toloc.SortOrder < objCON.COTOSort + 1)
                    {
                        toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                    }
                    else
                    {
                        toloc.SortOrder++;
                    }
                }
                foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                {
                    if (coto.SortOrder <= objCON.COTOSort)
                    {
                        if (coto.SortOrder == objCON.COTOSort)
                        {
                            coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                            coto.LocationToID = locationid;
                        }
                        else
                            coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                    }
                    else
                    {
                        coto.SortOrder++;
                    }
                }
                var lstAdd = new List<OPS_COTOContainer>();
                foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.IsSplit == false))
                {
                    if (tocontainer.COTOSort <= objCON.COTOSort)
                    {
                        if (tocontainer.COTOSort == objCON.COTOSort)
                        {
                            tocontainer.IsSplit = true;

                            //add new stop
                            lstAdd.Add(new OPS_COTOContainer()
                            {
                                CreatedBy = account.UserName,
                                CreatedDate = DateTime.Now,

                                ParentID = tocontainer.ID,
                                OPSContainerID = tocontainer.OPSContainerID,
                                IsSwap = false,
                                IsInput = false,
                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                StatusOfCOContainerID = tocontainer.StatusOfCOContainerID,
                                CreateByMasterID = tocontainer.COTOMasterID,

                                COTOMasterID = tocontainer.COTOMasterID,
                                LocationFromID = tocontainer.LocationFromID,
                                LocationToID = locationid,
                                ETD = tocontainer.ETD,
                                ETA = tocontainer.ETD,
                                ETDStart = tocontainer.ETD,
                                ETAStart = tocontainer.ETD,
                                DateFromCome = tocontainer.ETD,
                                DateToCome = tocontainer.ETD,
                                SortOrder = tocontainer.SortOrder,
                                COTOSort = tocontainer.COTOSort,
                                IsSplit = false,
                                IsDuplicateHidden = tocontainer.IsDuplicateHidden,
                                ReasonChangeID = reasionid,
                                ReasonChangeNote = reasionnote
                            });

                            lstAdd.Add(new OPS_COTOContainer()
                            {
                                CreatedBy = account.UserName,
                                CreatedDate = DateTime.Now,

                                ParentID = tocontainer.ID,
                                OPSContainerID = tocontainer.OPSContainerID,
                                IsSwap = false,
                                IsInput = false,
                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                StatusOfCOContainerID = tocontainer.StatusOfCOContainerID,
                                CreateByMasterID = tocontainer.COTOMasterID,

                                COTOMasterID = tocontainer.COTOMasterID,
                                LocationFromID = locationid,
                                LocationToID = tocontainer.LocationToID,
                                ETD = tocontainer.ETD,
                                ETA = tocontainer.ETA,
                                ETDStart = tocontainer.ETD,
                                ETAStart = tocontainer.ETA,
                                DateFromCome = tocontainer.ETD,
                                DateToCome = tocontainer.ETA,
                                SortOrder = tocontainer.SortOrder + 1,
                                COTOSort = tocontainer.COTOSort + 1,
                                IsSplit = false,
                                IsDuplicateHidden = tocontainer.IsDuplicateHidden,
                                ReasonChangeID = reasionid,
                                ReasonChangeNote = reasionnote
                            });
                        }
                        else
                            tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                    }
                    else
                    {
                        tocontainer.SortOrder++;
                        tocontainer.COTOSort++;
                    }
                }

                //new coto
                var objTONew = new OPS_COTO();
                objTONew.CreatedBy = account.UserName;
                objTONew.CreatedDate = DateTime.Now;
                objTONew.COTOMasterID = objCON.COTOMasterID;
                objTONew.IsOPS = true;
                objTONew.SortOrder = objCON.COTOSort + 1;
                objTONew.LocationFromID = locationid;
                objTONew.LocationToID = objCON.LocationToID;
                objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                model.OPS_COTO.Add(objTONew);

                //new location
                var objTOLocationNew = new OPS_COTOLocation();
                objTOLocationNew.LocationID = locationid;
                objTOLocationNew.SortOrder = objCON.COTOSort + 1;
                objTOLocationNew.COTOMasterID = objCON.COTOMasterID;
                objTOLocationNew.CreatedBy = account.UserName;
                objTOLocationNew.CreatedDate = DateTime.Now;
                objTOLocationNew.DateComeEstimate = objCON.ETD;
                objTOLocationNew.DateLeaveEstimate = objCON.ETD;
                objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                model.OPS_COTOLocation.Add(objTOLocationNew);

                foreach (var item in lstAdd)
                {
                    model.OPS_COTOContainer.Add(item);
                }
                model.SaveChanges();

                MONCO_TOContainer_ResetDetail(model, account, objCON.COTOMasterID);

                return HelperTOMaster_Error.None;

                //double houradd = 0;
                //var objTOLocationStart = model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort).OrderByDescending(c => c.SortOrder).FirstOrDefault();
                //var objTOLocationEnd = model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort + 1).OrderBy(c => c.SortOrder).FirstOrDefault();
                //if (objTOLocationStart != null && objTOLocationEnd != null)
                //{
                //    if (objTOLocationStart.DateLeaveEstimate != null && objTOLocationEnd.DateComeEstimate != null && objTOLocationStart.DateLeaveEstimate.Value.CompareTo(objTOLocationEnd.DateComeEstimate.Value) < 0)
                //    {
                //        var sub = objTOLocationEnd.DateComeEstimate.Value - objTOLocationStart.DateLeaveEstimate.Value;
                //        houradd = Math.Round(sub.TotalHours / 2, 2, MidpointRounding.AwayFromZero);
                //    }
                //    else
                //        throw FaultHelper.BusinessFault(null, null, "time fail");

                //    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                //    {
                //        if (toloc.SortOrder <= objCON.COTOSort + 1)
                //        {
                //            toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                //        }
                //        else
                //        {
                //            toloc.SortOrder++;
                //        }
                //    }
                //    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                //    {
                //        if (coto.SortOrder <= objCON.COTOSort)
                //        {
                //            if (coto.SortOrder == objCON.COTOSort)
                //            {
                //                coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                //                coto.LocationToID = locationid;
                //            }
                //            else
                //                coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                //        }
                //        else
                //        {
                //            coto.SortOrder++;
                //        }
                //    }
                //    var lstAdd = new List<OPS_COTOContainer>();
                //    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                //    {
                //        if (tocontainer.COTOSort <= objCON.COTOSort)
                //        {
                //            if (tocontainer.COTOSort == objCON.COTOSort)
                //            {
                //                //add new stop
                //                lstAdd.Add(new OPS_COTOContainer()
                //                {
                //                    CreatedBy = account.UserName,
                //                    CreatedDate = DateTime.Now,

                //                    ParentID = tocontainer.ParentID,
                //                    OPSContainerID = tocontainer.OPSContainerID,
                //                    IsSwap = false,
                //                    IsInput = false,
                //                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                //                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                //                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                //                    CreateByMasterID = tocontainer.COTOMasterID,

                //                    COTOMasterID = tocontainer.COTOMasterID,
                //                    LocationFromID = tocontainer.LocationFromID,
                //                    LocationToID = locationid,
                //                    ETD = objTOLocationStart.DateLeaveEstimate.Value,
                //                    ETA = objTOLocationStart.DateLeaveEstimate.Value.AddHours(houradd),
                //                    ETDStart = objTOLocationStart.DateLeaveEstimate.Value,
                //                    ETAStart = objTOLocationStart.DateLeaveEstimate.Value.AddHours(houradd),
                //                    SortOrder = tocontainer.SortOrder,
                //                    COTOSort = tocontainer.COTOSort,
                //                    IsSplit = false,
                //                    ReasonChangeID = reasionid,
                //                    ReasonChangeNote = reasionnote
                //                });

                //                tocontainer.LocationFromID = locationid;
                //                tocontainer.SortOrder++;
                //                tocontainer.COTOSort++;
                //                tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                //                tocontainer.ReasonChangeID = reasionid;
                //                tocontainer.ReasonChangeNote = reasionnote;
                //                tocontainer.ModifiedDate = DateTime.Now;
                //                tocontainer.ModifiedBy = account.UserName;
                //            }
                //            else
                //                tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                //        }
                //        else
                //        {
                //            tocontainer.SortOrder++;
                //            tocontainer.COTOSort++;
                //        }
                //    }

                //    //new coto
                //    var objTONew = new OPS_COTO();
                //    objTONew.CreatedBy = account.UserName;
                //    objTONew.CreatedDate = DateTime.Now;
                //    objTONew.COTOMasterID = objCON.COTOMasterID;
                //    objTONew.IsOPS = true;
                //    objTONew.SortOrder = objCON.COTOSort + 1;
                //    objTONew.LocationFromID = objCON.LocationToID;
                //    objTONew.LocationToID = locationid;
                //    objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                //    model.OPS_COTO.Add(objTONew);

                //    //new location
                //    var objTOLocationNew = new OPS_COTOLocation();
                //    objTOLocationNew.LocationID = locationid;
                //    objTOLocationNew.SortOrder = objCON.COTOSort + 2;
                //    objTOLocationNew.COTOMasterID = objCON.COTOMasterID;
                //    objTOLocationNew.CreatedBy = account.UserName;
                //    objTOLocationNew.CreatedDate = DateTime.Now;
                //    objTOLocationNew.DateComeEstimate = objTOLocationStart.DateLeaveEstimate.Value.AddHours(houradd);
                //    objTOLocationNew.DateLeaveEstimate = objTOLocationStart.DateLeaveEstimate.Value.AddHours(houradd);
                //    objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                //    objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                //    model.OPS_COTOLocation.Add(objTOLocationNew);

                //    foreach (var item in lstAdd)
                //    {
                //        model.OPS_COTOContainer.Add(item);
                //    }
                //    model.SaveChanges();

                //    MONCO_ChangeLocationReload(model, account, objCON.COTOMasterID);

                //    return HelperTOMaster_Error.None;
                //}
                //else
                //    throw FaultHelper.BusinessFault(null, null, "location start, end different");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "id null");
        }

        public static HelperTOMaster_Error MONCO_TOContainer_StartOffer(DataEntities model, AccountItem account, int tocontainerid, int? cotoid = null)
        {
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0 && c.COTOSort == 1 && !statusload.Contains(c.StatusOfCOContainerID)).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID > 0 ? c.COTOMasterID.Value : -1,
                c.ParentID,
                c.SortOrder,
                c.ETD,
                c.ETA,
                c.StatusOfCOContainerID,
                c.LocationFromID,
                c.LocationToID,
                c.OPSContainerID,
                c.OPS_COTOMaster.VehicleID,
                LocationVehicleID = c.OPS_COTOMaster.VehicleID > 0 && c.OPS_COTOMaster.CAT_Vehicle.LocationID > 0 ? c.OPS_COTOMaster.CAT_Vehicle.LocationID.Value : -1,
                c.OPS_COTOMaster.RomoocID,
                LocationMoocID = c.OPS_COTOMaster.RomoocID > 0 && c.OPS_COTOMaster.CAT_Romooc.LocationID > 0 ? c.OPS_COTOMaster.CAT_Romooc.LocationID.Value : -1,
                c.OPS_COTOMaster.VendorOfVehicleID,
                c.OPS_COTOMaster.StatusOfCOTOMasterID,
                c.COTOSort
            }).FirstOrDefault();
            if (objCON != null && objCON.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterTendered)
            {
                if (objCON.VendorOfVehicleID == null || objCON.VendorOfVehicleID == account.SYSCustomerID)
                {
                    //int locationvehicleid = objCON.LocationVehicleID;
                    //int locationmoocid = objCON.LocationMoocID;
                    //if (locationvehicleid < 1)
                    //{
                    //    var locationstand = model.CAT_StandDetail.Where(c => c.VehicleID == objCON.VehicleID && c.IsDefault).Select(c => new { c.CAT_Stand.LocationID }).FirstOrDefault();
                    //    if (locationstand != null)
                    //        locationvehicleid = locationstand.LocationID;
                    //}
                    //if (locationmoocid < 1)
                    //{
                    //    var locationstand = model.CAT_StandDetail.Where(c => c.RomoocID == objCON.RomoocID && c.IsDefault).Select(c => new { c.CAT_Stand.LocationID }).FirstOrDefault();
                    //    if (locationstand != null)
                    //        locationmoocid = locationstand.LocationID;
                    //}

                    ////Check setting
                    //var standVehicle = model.CAT_StandDetail.Where(c => c.VehicleID == objCON.VehicleID && c.IsDefault).Select(c => new { c.CAT_Stand.LocationID }).FirstOrDefault();
                    //var standRomooc = model.CAT_StandDetail.Where(c => c.RomoocID == objCON.RomoocID && c.IsDefault).Select(c => new { c.CAT_Stand.LocationID }).FirstOrDefault();
                    //if (standVehicle == null)
                    //    return HelperTOMaster_Error.TOContainerComplete_StartVehicleStand;
                    //if (standRomooc == null)
                    //    return HelperTOMaster_Error.TOContainerComplete_StartRomoocStand;

                    //var timeMaster = model.FLM_AssetTimeSheet.Where(c => c.ReferID == objCON.COTOMasterID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster).Select(c => new { c.DateFromActual }).OrderBy(c => c.DateFromActual).FirstOrDefault();
                    //var assetVehicle = model.FLM_Asset.Where(c => c.VehicleID == objCON.VehicleID).Select(c => new { c.ID }).FirstOrDefault();
                    //var assetRomooc = model.FLM_Asset.Where(c => c.RomoocID == objCON.RomoocID).Select(c => new { c.ID }).FirstOrDefault();
                    //if (timeMaster != null && assetVehicle != null && assetRomooc != null)
                    //{
                    //    //check time
                    //    var timePrev = model.FLM_AssetTimeSheet.Where(c => c.AssetID == assetVehicle.ID && c.ReferID != objCON.COTOMasterID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.DateToActual <= timeMaster.DateFromActual).Select(c => new { c.ReferID }).FirstOrDefault();
                    //    if (timePrev != null && timePrev.ReferID > 0)
                    //    {
                    //        var masterprev = model.OPS_COTOMaster.Where(c => c.ID == timePrev.ReferID).Select(c => new { c.ID, c.RomoocID, c.StatusOfCOTOMasterID }).FirstOrDefault();
                    //        if (masterprev != null)
                    //        {
                    //            if (masterprev.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterReceived)
                    //                return HelperTOMaster_Error.TOContainerComplete_StartScheduleWait;
                    //            else
                    //            {
                    //                //if (masterprev.RomoocID != objCON.RomoocID)
                    //                //{
                    //                //    var assetPrevMooc = model.FLM_Asset.Where(c => c.RomoocID == masterprev.RomoocID).Select(c => new { c.ID }).FirstOrDefault();
                    //                //    if (assetPrevMooc != null)
                    //                //    {
                    //                //        var timePrevMooc = model.FLM_AssetTimeSheet.Where(c => c.AssetID == assetPrevMooc.ID && c.DateFromActual > timeMaster.DateFromActual).Select(c => new { c.ReferID, c.DateFromActual }).OrderBy(c => c.DateFromActual).FirstOrDefault();
                    //                //        if (timePrevMooc == null)
                    //                //            return HelperTOMaster_Error.TOContainerComplete_StartHasRomooc;
                    //                //    }
                    //                //}
                    //            }
                    //        }
                    //        if (model.OPS_COTOMaster.Where(c => c.ID == timePrev.ReferID && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterReceived).Count() > 0)
                    //            return HelperTOMaster_Error.TOContainerComplete_StartScheduleWait;
                    //    }
                    //    timePrev = model.FLM_AssetTimeSheet.Where(c => c.AssetID == assetRomooc.ID && c.ReferID != objCON.COTOMasterID && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster && c.DateToActual <= timeMaster.DateFromActual).Select(c => new { c.ReferID }).FirstOrDefault();
                    //    if (timePrev != null && timePrev.ReferID > 0)
                    //    {
                    //        if (model.OPS_COTOMaster.Where(c => c.ID == timePrev.ReferID && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterReceived).Count() > 0)
                    //            return HelperTOMaster_Error.TOContainerComplete_StartScheduleWait;
                    //    }
                    //}

                    var lstTOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID).Select(c => new
                    {
                        c.ID,
                        c.ParentID,
                        c.IsSplit,
                        c.LocationFromID,
                        c.LocationToID,
                        c.ETD,
                        c.ETA,
                        c.SortOrder,
                        c.OPSContainerID,
                        c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID
                    }).ToList();
                    if (lstTOContainer.Where(c => c.ParentID == null && c.IsSplit == true).Count() == 0)
                    {
                        int totalLocal = lstTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Count();

                        if (lstTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 2 && lstTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 2)
                        {
                            //swap 2 con

                            //end swap 2 con
                        }
                        else if (lstTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 2 || lstTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 2)
                        {
                            //2 con
                            if (totalLocal > 2)
                                return HelperTOMaster_Error.TOContainerComplete_StartMoreLocal;
                            else
                            {

                            }
                            //end 2 con
                        }
                        else if (lstTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 1 && lstTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 1)
                        {
                            //swap 1 con
                            var itemExport = lstTOContainer.FirstOrDefault(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport);
                            var itemImport = lstTOContainer.FirstOrDefault(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport);
                            if (itemExport != null && itemImport != null)
                            {
                                var dataExport = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == itemExport.ID);
                                var dataImport = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == itemImport.ID);
                                if (dataExport != null && dataImport != null)
                                {
                                    if (totalLocal > 1)
                                        return HelperTOMaster_Error.TOContainerComplete_StartMoreLocal;
                                    else if (totalLocal == 1)
                                    {
                                        var itemLocal = lstTOContainer.FirstOrDefault(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal);
                                        if (itemLocal != null)
                                        {
                                            var datalocal = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == itemLocal.ID);
                                            if (datalocal != null)
                                            {
                                                dataImport.IsSplit = true;
                                                datalocal.IsSplit = true;
                                                dataExport.IsSplit = true;
                                                int sort = 1;
                                                var tocontainernew = new OPS_COTOContainer();
                                                if (itemLocal.LocationFromID != itemImport.LocationFromID)
                                                {
                                                    tocontainernew = new OPS_COTOContainer()
                                                    {
                                                        CreatedBy = account.UserName,
                                                        CreatedDate = DateTime.Now,

                                                        ParentID = itemLocal.ID,
                                                        OPSContainerID = itemLocal.OPSContainerID,
                                                        IsSwap = false,
                                                        IsInput = false,
                                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                                        CreateByMasterID = objCON.COTOMasterID,

                                                        COTOMasterID = objCON.COTOMasterID,
                                                        LocationFromID = itemImport.LocationFromID,
                                                        LocationToID = itemLocal.LocationFromID,
                                                        ETD = itemLocal.ETD,
                                                        ETA = itemLocal.ETD,
                                                        ETDStart = itemLocal.ETD,
                                                        ETAStart = itemLocal.ETD,
                                                        DateFromCome = itemLocal.ETD,
                                                        DateToCome = itemLocal.ETD,
                                                        SortOrder = itemLocal.SortOrder + (sort - 1),
                                                        COTOSort = sort,
                                                        IsSplit = false
                                                    };
                                                    model.OPS_COTOContainer.Add(tocontainernew);
                                                    sort++;
                                                }

                                                tocontainernew = new OPS_COTOContainer()
                                                {
                                                    CreatedBy = account.UserName,
                                                    CreatedDate = DateTime.Now,

                                                    ParentID = itemLocal.ID,
                                                    OPSContainerID = itemLocal.OPSContainerID,
                                                    IsSwap = false,
                                                    IsInput = false,
                                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerLOLaden,
                                                    CreateByMasterID = objCON.COTOMasterID,

                                                    COTOMasterID = objCON.COTOMasterID,
                                                    LocationFromID = itemLocal.LocationFromID,
                                                    LocationToID = itemLocal.LocationToID,
                                                    ETD = itemLocal.ETD,
                                                    ETA = itemLocal.ETD,
                                                    ETDStart = itemLocal.ETD,
                                                    ETAStart = itemLocal.ETD,
                                                    DateFromCome = itemLocal.ETD,
                                                    DateToCome = itemLocal.ETD,
                                                    SortOrder = itemLocal.SortOrder + (sort - 1),
                                                    COTOSort = sort,
                                                    IsSplit = false
                                                };
                                                model.OPS_COTOContainer.Add(tocontainernew);

                                                if (itemLocal.LocationToID != itemExport.LocationToID)
                                                {
                                                    sort++;

                                                    tocontainernew = new OPS_COTOContainer()
                                                    {
                                                        CreatedBy = account.UserName,
                                                        CreatedDate = DateTime.Now,

                                                        ParentID = itemLocal.ID,
                                                        OPSContainerID = itemLocal.OPSContainerID,
                                                        IsSwap = true,
                                                        IsInput = false,
                                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerEXEmpty,
                                                        CreateByMasterID = objCON.COTOMasterID,

                                                        COTOMasterID = objCON.COTOMasterID,
                                                        LocationFromID = itemLocal.LocationToID,
                                                        LocationToID = itemExport.LocationToID,
                                                        ETD = itemExport.ETD,
                                                        ETA = itemExport.ETA,
                                                        ETDStart = itemExport.ETD,
                                                        ETAStart = itemExport.ETA,
                                                        DateFromCome = itemExport.ETD,
                                                        DateToCome = itemExport.ETA,
                                                        SortOrder = itemLocal.SortOrder + (sort - 1),
                                                        COTOSort = sort,
                                                        IsSplit = false
                                                    };
                                                    model.OPS_COTOContainer.Add(tocontainernew);
                                                }
                                                else
                                                {
                                                    tocontainernew = new OPS_COTOContainer()
                                                    {
                                                        CreatedBy = account.UserName,
                                                        CreatedDate = DateTime.Now,

                                                        ParentID = itemLocal.ID,
                                                        OPSContainerID = itemLocal.OPSContainerID,
                                                        IsSwap = true,
                                                        IsInput = false,
                                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerEXEmpty,
                                                        CreateByMasterID = objCON.COTOMasterID,

                                                        COTOMasterID = objCON.COTOMasterID,
                                                        LocationFromID = itemLocal.LocationFromID,
                                                        LocationToID = itemLocal.LocationToID,
                                                        ETD = itemExport.ETD,
                                                        ETA = itemExport.ETA,
                                                        ETDStart = itemExport.ETD,
                                                        ETAStart = itemExport.ETA,
                                                        DateFromCome = itemExport.ETD,
                                                        DateToCome = itemExport.ETA,
                                                        SortOrder = itemLocal.SortOrder + (sort - 1),
                                                        COTOSort = sort,
                                                        IsSplit = false
                                                    };
                                                    model.OPS_COTOContainer.Add(tocontainernew);
                                                }
                                                model.SaveChanges();
                                                MONCO_TOContainer_StartOffer_GenData(model, account, objCON.COTOMasterID);
                                            }
                                        }
                                    }
                                    else
                                    {
                                        dataImport.IsSplit = true;
                                        dataExport.IsSplit = true;
                                        int sort = 1;
                                        var tocontainernew = new OPS_COTOContainer()
                                        {
                                            CreatedBy = account.UserName,
                                            CreatedDate = DateTime.Now,

                                            ParentID = dataExport.ID,
                                            OPSContainerID = dataExport.OPSContainerID,
                                            IsSwap = true,
                                            IsInput = false,
                                            TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                            TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerEXEmpty,
                                            CreateByMasterID = objCON.COTOMasterID,

                                            COTOMasterID = objCON.COTOMasterID,
                                            LocationFromID = dataImport.LocationFromID,
                                            LocationToID = dataExport.LocationToID,
                                            ETD = dataExport.ETD,
                                            ETA = dataExport.ETA,
                                            ETDStart = dataExport.ETD,
                                            ETAStart = dataExport.ETA,
                                            DateFromCome = dataExport.ETD,
                                            DateToCome = dataExport.ETA,
                                            SortOrder = dataExport.SortOrder,
                                            COTOSort = sort,
                                            IsSplit = false
                                        };
                                        model.OPS_COTOContainer.Add(tocontainernew);
                                        model.SaveChanges();
                                        MONCO_TOContainer_StartOffer_GenData(model, account, objCON.COTOMasterID);
                                    }
                                }
                            }
                            //end swap 1 con
                        }
                        else if (lstTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).Count() == 1 || lstTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() == 1)
                        {
                            //1 con
                            var item = lstTOContainer.FirstOrDefault(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport);
                            if (item != null)
                            {
                                var data = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item.ID);
                                if (data != null)
                                {
                                    if (totalLocal > 1)
                                        return HelperTOMaster_Error.TOContainerComplete_StartMoreLocal;
                                    else if (totalLocal == 1)
                                    {
                                        var itemLocal = lstTOContainer.FirstOrDefault(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal);
                                        if (itemLocal != null)
                                        {
                                            var datalocal = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == itemLocal.ID);
                                            if (datalocal != null)
                                            {
                                                data.IsSplit = true;
                                                datalocal.IsSplit = true;

                                                int sort = 1;
                                                var tocontainernew = new OPS_COTOContainer();
                                                if (itemLocal.LocationFromID != item.LocationFromID)
                                                {
                                                    tocontainernew = new OPS_COTOContainer()
                                                    {
                                                        CreatedBy = account.UserName,
                                                        CreatedDate = DateTime.Now,

                                                        ParentID = itemLocal.ID,
                                                        OPSContainerID = itemLocal.OPSContainerID,
                                                        IsSwap = false,
                                                        IsInput = false,
                                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                                        CreateByMasterID = objCON.COTOMasterID,

                                                        COTOMasterID = objCON.COTOMasterID,
                                                        LocationFromID = item.LocationFromID,
                                                        LocationToID = itemLocal.LocationFromID,
                                                        ETD = itemLocal.ETD,
                                                        ETA = itemLocal.ETD,
                                                        ETDStart = itemLocal.ETD,
                                                        ETAStart = itemLocal.ETD,
                                                        DateFromCome = itemLocal.ETD,
                                                        DateToCome = itemLocal.ETD,
                                                        SortOrder = itemLocal.SortOrder + (sort - 1),
                                                        COTOSort = sort,
                                                        IsSplit = false
                                                    };
                                                    model.OPS_COTOContainer.Add(tocontainernew);
                                                    sort++;
                                                }

                                                tocontainernew = new OPS_COTOContainer()
                                                {
                                                    CreatedBy = account.UserName,
                                                    CreatedDate = DateTime.Now,

                                                    ParentID = itemLocal.ID,
                                                    OPSContainerID = itemLocal.OPSContainerID,
                                                    IsSwap = false,
                                                    IsInput = false,
                                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerLOLaden,
                                                    CreateByMasterID = objCON.COTOMasterID,

                                                    COTOMasterID = objCON.COTOMasterID,
                                                    LocationFromID = itemLocal.LocationFromID,
                                                    LocationToID = itemLocal.LocationToID,
                                                    ETD = itemLocal.ETD,
                                                    ETA = itemLocal.ETD,
                                                    ETDStart = itemLocal.ETD,
                                                    ETAStart = itemLocal.ETD,
                                                    DateFromCome = itemLocal.ETD,
                                                    DateToCome = itemLocal.ETD,
                                                    SortOrder = itemLocal.SortOrder + (sort - 1),
                                                    COTOSort = sort,
                                                    IsSplit = false
                                                };
                                                model.OPS_COTOContainer.Add(tocontainernew);

                                                if (itemLocal.LocationToID != item.LocationToID)
                                                {
                                                    sort++;

                                                    tocontainernew = new OPS_COTOContainer()
                                                    {
                                                        CreatedBy = account.UserName,
                                                        CreatedDate = DateTime.Now,

                                                        ParentID = itemLocal.ID,
                                                        OPSContainerID = itemLocal.OPSContainerID,
                                                        IsSwap = true,
                                                        IsInput = false,
                                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                        StatusOfCOContainerID = data.StatusOfCOContainerID,
                                                        CreateByMasterID = objCON.COTOMasterID,

                                                        COTOMasterID = objCON.COTOMasterID,
                                                        LocationFromID = itemLocal.LocationToID,
                                                        LocationToID = item.LocationToID,
                                                        ETD = item.ETD,
                                                        ETA = item.ETA,
                                                        ETDStart = item.ETD,
                                                        ETAStart = item.ETA,
                                                        DateFromCome = item.ETD,
                                                        DateToCome = item.ETA,
                                                        SortOrder = itemLocal.SortOrder + (sort - 1),
                                                        COTOSort = sort,
                                                        IsSplit = false
                                                    };
                                                    model.OPS_COTOContainer.Add(tocontainernew);
                                                }
                                                else
                                                {
                                                    tocontainernew = new OPS_COTOContainer()
                                                    {
                                                        CreatedBy = account.UserName,
                                                        CreatedDate = DateTime.Now,

                                                        ParentID = itemLocal.ID,
                                                        OPSContainerID = itemLocal.OPSContainerID,
                                                        IsSwap = true,
                                                        IsInput = false,
                                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                        StatusOfCOContainerID = data.StatusOfCOContainerID,
                                                        CreateByMasterID = objCON.COTOMasterID,

                                                        COTOMasterID = objCON.COTOMasterID,
                                                        LocationFromID = itemLocal.LocationFromID,
                                                        LocationToID = itemLocal.LocationToID,
                                                        ETD = item.ETD,
                                                        ETA = item.ETA,
                                                        ETDStart = item.ETD,
                                                        ETAStart = item.ETA,
                                                        DateFromCome = item.ETD,
                                                        DateToCome = item.ETA,
                                                        SortOrder = itemLocal.SortOrder + (sort - 1),
                                                        COTOSort = sort,
                                                        IsSplit = false
                                                    };
                                                    model.OPS_COTOContainer.Add(tocontainernew);
                                                }
                                                model.SaveChanges();
                                                MONCO_TOContainer_StartOffer_GenData(model, account, objCON.COTOMasterID);
                                            }
                                        }
                                    }
                                }
                            }
                            //end 1 con
                        }
                        else
                        {
                            //other

                            //end other
                        }

                    }
                }
            }

            return HelperTOMaster_Error.None;
        }

        private static void MONCO_TOContainer_StartOffer_GenData(DataEntities model, AccountItem account, int masterid)
        {
            if (masterid > 0)
            {
                foreach (var item in model.OPS_COTO.Where(c => c.COTOMasterID == masterid))
                {
                    model.OPS_COTO.Remove(item);
                }
                foreach (var item in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid))
                {
                    model.OPS_COTOLocation.Remove(item);
                }
                model.SaveChanges();

                var lstTOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false && c.COTOSort > 0).Select(c => new
                {
                    c.ID,
                    c.COTOMasterID,
                    c.LocationFromID,
                    c.LocationToID,
                    c.ETD,
                    c.ETA,
                    c.COTOSort
                }).ToList();

                for (int i = 1; i <= 10; i++)
                {
                    var first = lstTOContainer.FirstOrDefault(c => c.COTOSort == i);
                    if (first != null)
                    {
                        var objLocation = new OPS_COTOLocation();
                        objLocation.LocationID = first.LocationToID;
                        objLocation.SortOrder = i + 1;
                        objLocation.COTOMasterID = masterid;
                        objLocation.CreatedBy = account.UserName;
                        objLocation.CreatedDate = DateTime.Now;
                        objLocation.DateComeEstimate = first.ETD;
                        objLocation.DateLeaveEstimate = first.ETD;
                        objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                        objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                        model.OPS_COTOLocation.Add(objLocation);

                        if (i == 1)
                        {
                            objLocation = new OPS_COTOLocation();
                            objLocation.LocationID = first.LocationFromID;
                            objLocation.SortOrder = i;
                            objLocation.COTOMasterID = masterid;
                            objLocation.CreatedBy = account.UserName;
                            objLocation.CreatedDate = DateTime.Now;
                            objLocation.DateComeEstimate = first.ETD;
                            objLocation.DateLeaveEstimate = first.ETD;
                            objLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                            objLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                            model.OPS_COTOLocation.Add(objLocation);
                        }

                        var objTO = new OPS_COTO();
                        objTO.CreatedBy = account.UserName;
                        objTO.CreatedDate = DateTime.Now;
                        objTO.COTOMasterID = masterid;
                        objTO.IsOPS = true;
                        objTO.SortOrder = i;
                        objTO.ETD = first.ETD;
                        objTO.ETA = first.ETA;
                        objTO.LocationFromID = first.LocationFromID;
                        objTO.LocationToID = first.LocationToID;
                        objTO.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                        model.OPS_COTO.Add(objTO);
                    }
                }

                model.SaveChanges();
            }
        }

        public static void MONCO_TOContainer_StartOfferData(DataEntities model, AccountItem account, int tocontainerid, HelperTOMaster_Start itemStart)
        {
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0 && !statusload.Contains(c.StatusOfCOContainerID)).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID > 0 ? c.COTOMasterID.Value : -1,
                c.ParentID,
                c.SortOrder,
                c.ETD,
                c.ETA,
                c.StatusOfCOContainerID,
                c.LocationFromID,
                c.LocationToID,
                c.OPSContainerID,
                c.OPS_COTOMaster.VehicleID,
                LocationVehicleID = c.OPS_COTOMaster.VehicleID > 0 && c.OPS_COTOMaster.CAT_Vehicle.LocationID > 0 ? c.OPS_COTOMaster.CAT_Vehicle.LocationID.Value : -1,
                c.OPS_COTOMaster.RomoocID,
                LocationMoocID = c.OPS_COTOMaster.RomoocID > 0 && c.OPS_COTOMaster.CAT_Romooc.LocationID > 0 ? c.OPS_COTOMaster.CAT_Romooc.LocationID.Value : -1,
                c.OPS_COTOMaster.VendorOfVehicleID,
                c.OPS_COTOMaster.StatusOfCOTOMasterID,
                c.COTOSort
            }).FirstOrDefault();

            if (objCON != null && itemStart != null)
            {
                var lstCOTOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.ParentID == null).Select(c => new
                {
                    c.ID,
                    c.OPSContainerID,
                    c.LocationFromID,
                    c.LocationToID,
                    c.ETD,
                    c.ETA,
                    c.StatusOfCOContainerID,
                    c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID,
                    c.SortOrder,
                    c.IsDuplicateHidden
                }).ToList();

                if (itemStart.ListDuplicate == null || itemStart.ListDuplicate.Count == 0)
                {
                    var lstData = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).ToList();
                    if (lstData.Count == 0)
                        lstData = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).ToList();
                    if (lstData.Count == 0)
                        lstData = lstCOTOContainer.Where(c => c.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderLocal).ToList();
                    int i = 1;
                    itemStart.ListDuplicate = new List<HelperTOMaster_Start_Duplicate>();
                    foreach (var item in lstData)
                    {
                        itemStart.ListDuplicate.Add(new HelperTOMaster_Start_Duplicate
                        {
                            ID = item.ID,
                            SortFrom = i
                        });
                        i++;
                    }
                    foreach (var item in itemStart.ListDuplicate)
                    {
                        item.SortTo = i;
                        i++;
                    }
                }

                var lstEmpty = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).ToList();
                if (lstEmpty.Count == 0)
                    lstEmpty = lstCOTOContainer.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport).ToList();
                if (lstEmpty.Count > 0)
                {
                    if (itemStart.ListLocal != null && itemStart.ListLocal.Count > 0)
                    {
                        //local

                        int sort = 0;
                        int locationid = -1;
                        var lstAdd = new List<OPS_COTOContainer>();
                        for (; sort < 30; sort++)
                        {
                            var _empty = itemStart.ListDuplicate.FirstOrDefault(c => c.SortFrom == sort + 1);
                            if (_empty != null)
                            {
                                var itemEmpty = lstEmpty.FirstOrDefault(c => c.ID == _empty.ID);
                                if (itemEmpty != null)
                                {
                                    if (locationid > 0)
                                    {
                                        var tocontainernew = new OPS_COTOContainer()
                                        {
                                            CreatedBy = account.UserName,
                                            CreatedDate = DateTime.Now,

                                            ParentID = itemEmpty.ID,
                                            OPSContainerID = itemEmpty.OPSContainerID,
                                            IsSwap = true,
                                            IsInput = false,
                                            TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                            TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                            StatusOfCOContainerID = itemEmpty.StatusOfCOContainerID,
                                            CreateByMasterID = objCON.COTOMasterID,

                                            COTOMasterID = objCON.COTOMasterID,
                                            LocationFromID = locationid,
                                            LocationToID = itemEmpty.LocationFromID,
                                            ETD = itemEmpty.ETD,
                                            ETA = itemEmpty.ETD,
                                            ETDStart = itemEmpty.ETD,
                                            ETAStart = itemEmpty.ETD,
                                            DateFromCome = itemEmpty.ETD,
                                            DateToCome = itemEmpty.ETD,
                                            SortOrder = itemEmpty.SortOrder + (sort - 1),
                                            COTOSort = sort,
                                            IsSplit = false,
                                            IsDuplicateHidden = true
                                        };
                                        lstAdd.Add(tocontainernew);
                                    }

                                    locationid = itemEmpty.LocationFromID;
                                }
                            }
                            else
                                break;
                        }

                        foreach (var local in itemStart.ListLocal.OrderBy(c => c.SortOrder))
                        {
                            var itemLocal = lstCOTOContainer.FirstOrDefault(c => c.ID == local.ID);
                            if (itemLocal != null)
                            {
                                if (itemLocal.LocationFromID != locationid)
                                {
                                    var tocontainerlocaladd = new OPS_COTOContainer()
                                    {
                                        CreatedBy = account.UserName,
                                        CreatedDate = DateTime.Now,

                                        ParentID = itemLocal.ID,
                                        OPSContainerID = itemLocal.OPSContainerID,
                                        IsSwap = true,
                                        IsInput = false,
                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                        CreateByMasterID = objCON.COTOMasterID,

                                        COTOMasterID = objCON.COTOMasterID,
                                        LocationFromID = locationid,
                                        LocationToID = itemLocal.LocationFromID,
                                        ETD = itemLocal.ETD,
                                        ETA = itemLocal.ETD,
                                        ETDStart = itemLocal.ETD,
                                        ETAStart = itemLocal.ETD,
                                        DateFromCome = itemLocal.ETD,
                                        DateToCome = itemLocal.ETD,
                                        SortOrder = itemLocal.SortOrder + (sort - 1),
                                        COTOSort = sort,
                                        IsSplit = false
                                    };
                                    lstAdd.Add(tocontainerlocaladd);

                                    foreach (var itemEmpty in lstEmpty)
                                    {
                                        tocontainerlocaladd = new OPS_COTOContainer()
                                        {
                                            CreatedBy = account.UserName,
                                            CreatedDate = DateTime.Now,

                                            ParentID = itemEmpty.ID,
                                            OPSContainerID = itemEmpty.OPSContainerID,
                                            IsSwap = true,
                                            IsInput = false,
                                            TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                            TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                            CreateByMasterID = objCON.COTOMasterID,

                                            COTOMasterID = objCON.COTOMasterID,
                                            LocationFromID = locationid,
                                            LocationToID = itemLocal.LocationFromID,
                                            ETD = itemEmpty.ETD,
                                            ETA = itemEmpty.ETD,
                                            ETDStart = itemEmpty.ETD,
                                            ETAStart = itemEmpty.ETD,
                                            DateFromCome = itemEmpty.ETD,
                                            DateToCome = itemEmpty.ETD,
                                            SortOrder = itemEmpty.SortOrder + (sort - 1),
                                            COTOSort = sort,
                                            IsSplit = false,
                                            IsDuplicateHidden = true
                                        };
                                        lstAdd.Add(tocontainerlocaladd);
                                    }

                                    locationid = itemLocal.LocationFromID;
                                    sort++;
                                }

                                var tocontainernew = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = itemLocal.ID,
                                    OPSContainerID = itemLocal.OPSContainerID,
                                    IsSwap = true,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = itemLocal.StatusOfCOContainerID,
                                    CreateByMasterID = objCON.COTOMasterID,

                                    COTOMasterID = objCON.COTOMasterID,
                                    LocationFromID = itemLocal.LocationFromID,
                                    LocationToID = itemLocal.LocationToID,
                                    ETD = itemLocal.ETD,
                                    ETA = itemLocal.ETD,
                                    ETDStart = itemLocal.ETD,
                                    ETAStart = itemLocal.ETD,
                                    DateFromCome = itemLocal.ETD,
                                    DateToCome = itemLocal.ETD,
                                    SortOrder = itemLocal.SortOrder + (sort - 1),
                                    COTOSort = sort,
                                    IsSplit = false
                                };
                                lstAdd.Add(tocontainernew);

                                foreach (var itemEmpty in lstEmpty)
                                {
                                    tocontainernew = new OPS_COTOContainer()
                                    {
                                        CreatedBy = account.UserName,
                                        CreatedDate = DateTime.Now,

                                        ParentID = itemEmpty.ID,
                                        OPSContainerID = itemEmpty.OPSContainerID,
                                        IsSwap = true,
                                        IsInput = false,
                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                        CreateByMasterID = objCON.COTOMasterID,

                                        COTOMasterID = objCON.COTOMasterID,
                                        LocationFromID = itemLocal.LocationFromID,
                                        LocationToID = itemLocal.LocationToID,
                                        ETD = itemEmpty.ETD,
                                        ETA = itemEmpty.ETD,
                                        ETDStart = itemEmpty.ETD,
                                        ETAStart = itemEmpty.ETD,
                                        DateFromCome = itemEmpty.ETD,
                                        DateToCome = itemEmpty.ETD,
                                        SortOrder = itemEmpty.SortOrder + (sort - 1),
                                        COTOSort = sort,
                                        IsSplit = false,
                                        IsDuplicateHidden = true
                                    };
                                    lstAdd.Add(tocontainernew);
                                }

                                locationid = itemLocal.LocationToID;
                                sort++;
                            }
                        }

                        foreach (var item in itemStart.ListDuplicate.OrderBy(c => c.SortTo))
                        {
                            var itemEmpty = lstEmpty.FirstOrDefault(c => c.ID == item.ID);
                            if (itemEmpty != null)
                            {
                                if (itemEmpty.LocationToID != locationid)
                                {
                                    var tocontainernew = new OPS_COTOContainer()
                                    {
                                        CreatedBy = account.UserName,
                                        CreatedDate = DateTime.Now,

                                        ParentID = itemEmpty.ID,
                                        OPSContainerID = itemEmpty.OPSContainerID,
                                        IsSwap = true,
                                        IsInput = false,
                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                        StatusOfCOContainerID = itemEmpty.StatusOfCOContainerID,
                                        CreateByMasterID = objCON.COTOMasterID,

                                        COTOMasterID = objCON.COTOMasterID,
                                        LocationFromID = locationid,
                                        LocationToID = itemEmpty.LocationToID,
                                        ETD = itemEmpty.ETD,
                                        ETA = itemEmpty.ETA,
                                        ETDStart = itemEmpty.ETD,
                                        ETAStart = itemEmpty.ETA,
                                        DateFromCome = itemEmpty.ETD,
                                        DateToCome = itemEmpty.ETA,
                                        SortOrder = itemEmpty.SortOrder + (sort - 1),
                                        COTOSort = sort,
                                        IsSplit = false,
                                        IsDuplicateHidden = itemEmpty.IsDuplicateHidden
                                    };
                                    lstAdd.Add(tocontainernew);

                                    locationid = itemEmpty.LocationToID;
                                    sort++;
                                }
                            }
                        }

                        //for (; sort < 30; sort++)
                        //{
                        //    var _empty = itemStart.ListDuplicate.OrderBy()
                        //    if (_empty != null)
                        //    {

                        //    }
                        //    else
                        //        break;
                        //}

                        foreach (var item in lstCOTOContainer)
                        {
                            var tocontainer = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item.ID);
                            if (tocontainer != null)
                            {
                                tocontainer.IsSplit = true;
                            }
                        }
                        foreach (var item in lstAdd)
                        {
                            model.OPS_COTOContainer.Add(item);
                        }
                        model.SaveChanges();
                        MONCO_TOContainer_StartOffer_GenData(model, account, objCON.COTOMasterID);

                        //end local
                    }
                    else
                    {

                    }
                }
                else
                {

                }
            }
        }

        public static HelperTOMaster_Error MONCO_TOContainer_Start(DataEntities model, AccountItem account, int tocontainerid, bool haschangemooc)
        {
            double hourmatrixtemp = 0;
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0 && c.COTOSort == 1 && c.IsSplit == false && !statusload.Contains(c.StatusOfCOContainerID)).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID > 0 ? c.COTOMasterID.Value : -1,
                c.ParentID,
                c.SortOrder,
                c.ETD,
                c.ETA,
                c.StatusOfCOContainerID,
                c.LocationFromID,
                c.LocationToID,
                c.OPSContainerID,
                c.OPS_COTOMaster.VehicleID,
                LocationVehicleID = c.OPS_COTOMaster.VehicleID > 0 && c.OPS_COTOMaster.CAT_Vehicle.LocationID > 0 ? c.OPS_COTOMaster.CAT_Vehicle.LocationID.Value : -1,
                c.OPS_COTOMaster.RomoocID,
                LocationMoocID = c.OPS_COTOMaster.RomoocID > 0 && c.OPS_COTOMaster.CAT_Romooc.LocationID > 0 ? c.OPS_COTOMaster.CAT_Romooc.LocationID.Value : -1,
                c.OPS_COTOMaster.VendorOfVehicleID,
                c.OPS_COTOMaster.StatusOfCOTOMasterID,
                c.COTOSort
            }).FirstOrDefault();
            if (objCON == null)
            {
                var objMaster = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0 && c.IsSplit == true).Select(c => new { c.COTOMasterID }).FirstOrDefault();
                if (objMaster != null)
                {
                    objCON = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objMaster.COTOMasterID && c.COTOSort == 1 && c.IsSplit == false && !statusload.Contains(c.StatusOfCOContainerID)).Select(c => new
                    {
                        c.ID,
                        COTOMasterID = c.COTOMasterID > 0 ? c.COTOMasterID.Value : -1,
                        c.ParentID,
                        c.SortOrder,
                        c.ETD,
                        c.ETA,
                        c.StatusOfCOContainerID,
                        c.LocationFromID,
                        c.LocationToID,
                        c.OPSContainerID,
                        c.OPS_COTOMaster.VehicleID,
                        LocationVehicleID = c.OPS_COTOMaster.VehicleID > 0 && c.OPS_COTOMaster.CAT_Vehicle.LocationID > 0 ? c.OPS_COTOMaster.CAT_Vehicle.LocationID.Value : -1,
                        c.OPS_COTOMaster.RomoocID,
                        LocationMoocID = c.OPS_COTOMaster.RomoocID > 0 && c.OPS_COTOMaster.CAT_Romooc.LocationID > 0 ? c.OPS_COTOMaster.CAT_Romooc.LocationID.Value : -1,
                        c.OPS_COTOMaster.VendorOfVehicleID,
                        c.OPS_COTOMaster.StatusOfCOTOMasterID,
                        c.COTOSort
                    }).FirstOrDefault();
                }
            }

            if (objCON != null && objCON.COTOSort == 1 && objCON.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterTendered && (objCON.VendorOfVehicleID == null || objCON.VendorOfVehicleID == account.SYSCustomerID))
            {
                var objCheck = model.OPS_COTOMaster.Where(c => c.ID != objCON.COTOMasterID && c.VehicleID == objCON.VehicleID && (c.SYSCustomerID == null || c.SYSCustomerID == account.SYSCustomerID) && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterReceived && c.ETA < objCON.ETD).Select(c => new { c.Code }).FirstOrDefault();
                if (objCheck == null)
                {
                    objCheck = model.OPS_COTOMaster.Where(c => c.ID != objCON.COTOMasterID && c.RomoocID == objCON.RomoocID && (c.SYSCustomerID == null || c.SYSCustomerID == account.SYSCustomerID) && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterReceived && c.ETA < objCON.ETD).Select(c => new { c.Code }).FirstOrDefault();
                    if (objCheck == null)
                    {
                        bool failState2 = false;
                        List<int> statemain = new List<int>()
                        {
                            -(int)SYSVarType.StatusOfCOContainerIMEmpty,
                            -(int)SYSVarType.StatusOfCOContainerIMLaden,
                            -(int)SYSVarType.StatusOfCOContainerEXEmpty,
                            -(int)SYSVarType.StatusOfCOContainerEXLaden,
                            -(int)SYSVarType.StatusOfCOContainerLOLaden,
                            -(int)SYSVarType.StatusOfCOContainerLOEmpty
                        };
                        var firstRomooc = model.OPS_COTOContainer.Where(c => c.OPS_Container.RomoocID == objCON.RomoocID && c.COTOMasterID > 0 && c.OPS_COTOMaster.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterReceived).Select(c => new { c.OPSContainerID, c.OPS_COTOMaster.ATD, c.COTOMasterID }).OrderByDescending(c => c.ATD).FirstOrDefault();
                        if (firstRomooc != null)
                        {
                            if (model.OPS_COTOContainer.Where(c => c.OPSContainerID == firstRomooc.OPSContainerID && c.COTOMasterID > 0 && c.COTOMasterID != firstRomooc.COTOMasterID && c.COTOMasterID != objCON.COTOMasterID && c.OPS_COTOMaster.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterReceived).Count() > 0 ||
                                model.OPS_COTOContainer.Where(c => c.OPSContainerID == firstRomooc.OPSContainerID && c.ParentID == null && statemain.Contains(c.StatusOfCOContainerID) && c.COTOMasterID == null).Count() > 0)
                            {
                                failState2 = true;
                            }
                        }
                        if (!failState2)
                        {
                            var lstOPSContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID).Select(c => c.OPSContainerID).Distinct().ToList();

                            if (model.OPS_COTOContainer.Where(c => lstOPSContainerID.Contains(c.OPSContainerID) && c.COTOMasterID > 0 && c.COTOMasterID != objCON.COTOMasterID && c.OPS_COTOMaster.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterReceived).Count() > 0)
                            {
                                int locationvehicleid = objCON.LocationVehicleID;
                                if (locationvehicleid < 1)
                                {
                                    var locationstand = model.CAT_StandDetail.Where(c => c.VehicleID == objCON.VehicleID && c.IsDefault).Select(c => new { c.CAT_Stand.LocationID }).FirstOrDefault();
                                    if (locationstand != null)
                                        locationvehicleid = locationstand.LocationID;
                                }

                                if (locationvehicleid == objCON.LocationMoocID && objCON.LocationMoocID == objCON.LocationFromID)
                                {
                                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.IsSplit == false && !statusload.Contains(c.StatusOfCOContainerID) && c.COTOSort == 1))
                                    {
                                        tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                                    }
                                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                                    {
                                        coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                                    }

                                    foreach (var tolocation in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                                    {
                                        tolocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                    }

                                    var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objCON.COTOMasterID);
                                    if (objMaster != null)
                                    {
                                        objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterDelivery;
                                        objMaster.ModifiedBy = account.UserName;
                                        objMaster.ModifiedDate = DateTime.Now;
                                    }
                                    model.SaveChanges();
                                }
                                else
                                {
                                    double totalhour = hourmatrixtemp + HourEmpty;

                                    var dtFrom = objCON.ETD.Value.AddHours(-hourmatrixtemp).AddHours(-HourEmpty);
                                    var dtTo = dtFrom.AddHours(hourmatrixtemp);

                                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.IsSplit == false && !statusload.Contains(c.StatusOfCOContainerID)))
                                    {
                                        if (tocontainer.COTOSort == objCON.COTOSort)
                                        {
                                            var tocontainernew = new OPS_COTOContainer()
                                            {
                                                CreatedBy = account.UserName,
                                                CreatedDate = DateTime.Now,

                                                ParentID = tocontainer.ParentID,
                                                OPSContainerID = tocontainer.OPSContainerID,
                                                IsSwap = false,
                                                IsInput = false,
                                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetRomooc,
                                                CreateByMasterID = objCON.COTOMasterID,

                                                COTOMasterID = objCON.COTOMasterID,
                                                LocationFromID = locationvehicleid,
                                                LocationToID = objCON.LocationFromID,
                                                ETD = dtFrom,
                                                ETA = dtTo,
                                                ETDStart = dtFrom,
                                                ETAStart = dtTo,
                                                DateFromCome = dtFrom,
                                                DateToCome = dtTo,
                                                SortOrder = tocontainer.SortOrder - 1,
                                                COTOSort = tocontainer.COTOSort,
                                                IsSplit = false,
                                                IsDuplicateHidden = tocontainer.IsDuplicateHidden
                                            };
                                            model.OPS_COTOContainer.Add(tocontainernew);
                                        }
                                        tocontainer.COTOSort++;
                                    }

                                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                                    {
                                        coto.SortOrder++;
                                    }

                                    foreach (var tolocation in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                                    {
                                        tolocation.SortOrder++;
                                    }

                                    //new coto
                                    var objCOTO = new OPS_COTO();
                                    objCOTO.CreatedBy = account.UserName;
                                    objCOTO.CreatedDate = DateTime.Now;
                                    objCOTO.COTOMasterID = objCON.COTOMasterID;
                                    objCOTO.IsOPS = true;
                                    objCOTO.SortOrder = objCON.COTOSort;
                                    objCOTO.LocationFromID = locationvehicleid;
                                    objCOTO.LocationToID = objCON.LocationFromID;
                                    objCOTO.ETD = dtFrom;
                                    objCOTO.ETA = dtTo;
                                    objCOTO.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                                    model.OPS_COTO.Add(objCOTO);

                                    //new location 
                                    var objTOLocation = new OPS_COTOLocation();
                                    objTOLocation.LocationID = locationvehicleid;
                                    objTOLocation.SortOrder = objCON.COTOSort;
                                    objTOLocation.COTOMasterID = objCON.COTOMasterID;
                                    objTOLocation.CreatedBy = account.UserName;
                                    objTOLocation.CreatedDate = DateTime.Now;
                                    objTOLocation.DateComeEstimate = dtFrom;
                                    objTOLocation.DateLeaveEstimate = dtFrom;
                                    objTOLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                    objTOLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                    model.OPS_COTOLocation.Add(objTOLocation);

                                    var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objCON.COTOMasterID);
                                    if (objMaster != null)
                                    {
                                        objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterDelivery;
                                        objMaster.ModifiedBy = account.UserName;
                                        objMaster.ModifiedDate = DateTime.Now;
                                    }

                                    model.SaveChanges();
                                }
                            }
                            else
                            {
                                int locationvehicleid = objCON.LocationVehicleID;
                                int locationmoocid = objCON.LocationMoocID;
                                if (locationvehicleid < 1)
                                {
                                    var locationstand = model.CAT_StandDetail.Where(c => c.VehicleID == objCON.VehicleID && c.IsDefault).Select(c => new { c.CAT_Stand.LocationID }).FirstOrDefault();
                                    if (locationstand != null)
                                        locationvehicleid = locationstand.LocationID;
                                }
                                if (locationmoocid < 1)
                                {
                                    var locationstand = model.CAT_StandDetail.Where(c => c.RomoocID == objCON.RomoocID && c.IsDefault).Select(c => new { c.CAT_Stand.LocationID }).FirstOrDefault();
                                    if (locationstand != null)
                                        locationmoocid = locationstand.LocationID;
                                }

                                bool returnmooc = false;
                                bool returnbase = false;
                                if (locationmoocid > 0 && locationvehicleid > 0)
                                {
                                    if (locationmoocid == locationvehicleid)
                                    {
                                        returnmooc = true;
                                        returnbase = false;
                                    }
                                    else
                                    {
                                        returnmooc = true;
                                        returnbase = true;
                                    }
                                }
                                else if (locationvehicleid > 0)
                                {
                                    returnmooc = false;
                                    returnbase = true;
                                }
                                else if (locationmoocid > 0)
                                {
                                    returnmooc = true;
                                    returnbase = false;
                                }
                                double totalhour = 0;
                                if (returnmooc)
                                    totalhour += hourmatrixtemp + HourEmpty;
                                if (returnbase)
                                    totalhour += hourmatrixtemp + HourEmpty;
                                //DateTime dtStart= 

                                //var lasttime=model.FLM_AssetTimeSheet.Where(c=>c.FLM_Asset.VehicleID==objCON.VehicleID && c.DateToActual>)


                                if (locationvehicleid > 1)
                                {
                                    bool hasgetmooc = false;
                                    if (locationvehicleid != locationmoocid && objCON.LocationFromID != locationmoocid)
                                        hasgetmooc = true;

                                    if (hasgetmooc || locationvehicleid != objCON.LocationFromID)
                                    {
                                        var locationid = locationvehicleid;
                                        if (hasgetmooc)
                                            locationid = locationmoocid;
                                        int status1 = -(int)SYSVarType.StatusOfCOContainerStop;
                                        int status2 = objCON.StatusOfCOContainerID;
                                        if (status2 == -(int)SYSVarType.StatusOfCOContainerEXEmpty || status2 == -(int)SYSVarType.StatusOfCOContainerLOEmpty || status2 == -(int)SYSVarType.StatusOfCOContainerShipEmpty)
                                            status1 = -(int)SYSVarType.StatusOfCOContainerGetEmpty;
                                        if (status2 == -(int)SYSVarType.StatusOfCOContainerIMLaden || status2 == -(int)SYSVarType.StatusOfCOContainerLOLaden || status2 == -(int)SYSVarType.StatusOfCOContainerShipLaden)
                                            status1 = -(int)SYSVarType.StatusOfCOContainerGetLaden;

                                        var dtFrom = objCON.ETD.Value.AddHours(-hourmatrixtemp);
                                        var dtTo = objCON.ETD.Value.AddHours(-HourEmpty);

                                        foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                                        {
                                            if (hasgetmooc)
                                                toloc.SortOrder += 2;
                                            else
                                                toloc.SortOrder++;
                                        }
                                        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                                        {
                                            if (hasgetmooc)
                                                coto.SortOrder += 2;
                                            else
                                                coto.SortOrder++;
                                        }
                                        var lstAdd = new List<OPS_COTOContainer>();
                                        var sort = 0;
                                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.IsSplit == false && !statusload.Contains(c.StatusOfCOContainerID)))
                                        {
                                            if (tocontainer.COTOSort > 0)
                                            {
                                                if (tocontainer.COTOSort == objCON.COTOSort)
                                                {
                                                    if (tocontainer.ParentID == null)
                                                    {
                                                        tocontainer.IsSplit = true;

                                                        sort = 0;
                                                        dtFrom = objCON.ETD.Value.AddHours(-hourmatrixtemp).AddHours(-HourEmpty);
                                                        dtTo = dtFrom.AddHours(hourmatrixtemp);
                                                        if (hasgetmooc)
                                                        {
                                                            dtFrom = objCON.ETD.Value.AddHours(-hourmatrixtemp * 2).AddHours(-HourEmpty * 2);
                                                            dtTo = dtFrom.AddHours(hourmatrixtemp);

                                                            lstAdd.Add(new OPS_COTOContainer()
                                                            {
                                                                CreatedBy = account.UserName,
                                                                CreatedDate = DateTime.Now,

                                                                ParentID = tocontainer.ID,
                                                                OPSContainerID = tocontainer.OPSContainerID,
                                                                IsSwap = false,
                                                                IsInput = false,
                                                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete,
                                                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetRomooc,
                                                                CreateByMasterID = tocontainer.COTOMasterID,

                                                                COTOMasterID = tocontainer.COTOMasterID,
                                                                LocationFromID = locationvehicleid,
                                                                LocationToID = locationid,
                                                                ETD = dtFrom,
                                                                ETA = dtTo,
                                                                ETDStart = dtFrom,
                                                                ETAStart = dtTo,
                                                                DateFromCome = dtFrom,
                                                                DateToCome = dtTo,
                                                                SortOrder = tocontainer.SortOrder + sort,
                                                                COTOSort = tocontainer.COTOSort + sort,
                                                                IsSplit = false,
                                                                IsDuplicateHidden = tocontainer.IsDuplicateHidden
                                                            });

                                                            dtFrom = dtTo.AddHours(HourEmpty);
                                                            dtTo = dtFrom.AddHours(hourmatrixtemp);
                                                            sort++;
                                                        }

                                                        lstAdd.Add(new OPS_COTOContainer()
                                                        {
                                                            CreatedBy = account.UserName,
                                                            CreatedDate = DateTime.Now,

                                                            ParentID = tocontainer.ID,
                                                            OPSContainerID = tocontainer.OPSContainerID,
                                                            IsSwap = false,
                                                            IsInput = false,
                                                            TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                                            TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                            StatusOfCOContainerID = status1,
                                                            CreateByMasterID = tocontainer.COTOMasterID,

                                                            COTOMasterID = tocontainer.COTOMasterID,
                                                            LocationFromID = locationid,
                                                            LocationToID = tocontainer.LocationFromID,
                                                            ETD = dtFrom,
                                                            ETA = dtTo,
                                                            ETDStart = dtFrom,
                                                            ETAStart = dtTo,
                                                            DateFromCome = dtFrom,
                                                            DateToCome = dtTo,
                                                            SortOrder = tocontainer.SortOrder + sort,
                                                            COTOSort = tocontainer.COTOSort + sort,
                                                            IsSplit = false,
                                                            IsDuplicateHidden = tocontainer.IsDuplicateHidden
                                                        });
                                                        sort++;
                                                        lstAdd.Add(new OPS_COTOContainer()
                                                        {
                                                            CreatedBy = account.UserName,
                                                            CreatedDate = DateTime.Now,

                                                            ParentID = tocontainer.ID,
                                                            OPSContainerID = tocontainer.OPSContainerID,
                                                            IsSwap = false,
                                                            IsInput = false,
                                                            TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                                            TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                            StatusOfCOContainerID = status2,
                                                            CreateByMasterID = tocontainer.COTOMasterID,

                                                            COTOMasterID = tocontainer.COTOMasterID,
                                                            LocationFromID = tocontainer.LocationFromID,
                                                            LocationToID = tocontainer.LocationToID,
                                                            ETD = tocontainer.ETD,
                                                            ETA = tocontainer.ETA,
                                                            ETDStart = tocontainer.ETD,
                                                            ETAStart = tocontainer.ETA,
                                                            DateFromCome = tocontainer.ETD,
                                                            DateToCome = tocontainer.ETA,
                                                            SortOrder = tocontainer.SortOrder + sort,
                                                            COTOSort = tocontainer.COTOSort + sort,
                                                            IsSplit = false,
                                                            IsDuplicateHidden = tocontainer.IsDuplicateHidden
                                                        });
                                                    }
                                                    else
                                                    {
                                                        sort = 0;
                                                        dtFrom = objCON.ETD.Value.AddHours(-hourmatrixtemp).AddHours(-HourEmpty);
                                                        dtTo = dtFrom.AddHours(hourmatrixtemp);
                                                        if (hasgetmooc)
                                                        {
                                                            dtFrom = objCON.ETD.Value.AddHours(-hourmatrixtemp * 2).AddHours(-HourEmpty * 2);
                                                            dtTo = dtFrom.AddHours(hourmatrixtemp);

                                                            lstAdd.Add(new OPS_COTOContainer()
                                                            {
                                                                CreatedBy = account.UserName,
                                                                CreatedDate = DateTime.Now,

                                                                ParentID = tocontainer.ParentID,
                                                                OPSContainerID = tocontainer.OPSContainerID,
                                                                IsSwap = false,
                                                                IsInput = false,
                                                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete,
                                                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetRomooc,
                                                                CreateByMasterID = tocontainer.COTOMasterID,

                                                                COTOMasterID = tocontainer.COTOMasterID,
                                                                LocationFromID = locationvehicleid,
                                                                LocationToID = locationid,
                                                                ETD = dtFrom,
                                                                ETA = dtTo,
                                                                ETDStart = dtFrom,
                                                                ETAStart = dtTo,
                                                                DateFromCome = dtFrom,
                                                                DateToCome = dtTo,
                                                                SortOrder = tocontainer.SortOrder + sort,
                                                                COTOSort = tocontainer.COTOSort + sort,
                                                                IsSplit = false,
                                                                IsDuplicateHidden = tocontainer.IsDuplicateHidden
                                                            });

                                                            dtFrom = dtTo.AddHours(HourEmpty);
                                                            dtTo = dtFrom.AddHours(hourmatrixtemp);
                                                            sort++;
                                                        }

                                                        lstAdd.Add(new OPS_COTOContainer()
                                                        {
                                                            CreatedBy = account.UserName,
                                                            CreatedDate = DateTime.Now,

                                                            ParentID = tocontainer.ParentID,
                                                            OPSContainerID = tocontainer.OPSContainerID,
                                                            IsSwap = false,
                                                            IsInput = false,
                                                            TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                                            TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                                            StatusOfCOContainerID = status1,
                                                            CreateByMasterID = tocontainer.COTOMasterID,

                                                            COTOMasterID = tocontainer.COTOMasterID,
                                                            LocationFromID = locationid,
                                                            LocationToID = tocontainer.LocationFromID,
                                                            ETD = dtFrom,
                                                            ETA = dtTo,
                                                            ETDStart = dtFrom,
                                                            ETAStart = dtTo,
                                                            DateFromCome = dtFrom,
                                                            DateToCome = dtTo,
                                                            SortOrder = tocontainer.SortOrder + sort,
                                                            COTOSort = tocontainer.COTOSort + sort,
                                                            IsSplit = false,
                                                            IsDuplicateHidden = tocontainer.IsDuplicateHidden
                                                        });
                                                        sort++;
                                                        tocontainer.SortOrder = tocontainer.SortOrder + sort;
                                                        tocontainer.COTOSort = tocontainer.COTOSort + sort;
                                                    }
                                                }
                                                else if (hasgetmooc)
                                                    tocontainer.COTOSort += 2;
                                                else
                                                    tocontainer.COTOSort++;
                                            }
                                        }

                                        sort = 0;
                                        if (hasgetmooc)
                                        {
                                            //new coto
                                            var objTONewMooc = new OPS_COTO();
                                            objTONewMooc.CreatedBy = account.UserName;
                                            objTONewMooc.CreatedDate = DateTime.Now;
                                            objTONewMooc.COTOMasterID = objCON.COTOMasterID;
                                            objTONewMooc.IsOPS = true;
                                            objTONewMooc.SortOrder = objCON.COTOSort + sort;
                                            objTONewMooc.LocationFromID = locationvehicleid;
                                            objTONewMooc.LocationToID = locationid;
                                            objTONewMooc.ETD = dtFrom;
                                            objTONewMooc.ETA = dtTo;
                                            objTONewMooc.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                                            model.OPS_COTO.Add(objTONewMooc);

                                            //new location
                                            var objTOLocationNewMooc = new OPS_COTOLocation();
                                            objTOLocationNewMooc.LocationID = locationvehicleid;
                                            objTOLocationNewMooc.SortOrder = objCON.COTOSort + sort;
                                            objTOLocationNewMooc.COTOMasterID = objCON.COTOMasterID;
                                            objTOLocationNewMooc.CreatedBy = account.UserName;
                                            objTOLocationNewMooc.CreatedDate = DateTime.Now;
                                            objTOLocationNewMooc.DateComeEstimate = dtFrom;
                                            objTOLocationNewMooc.DateLeaveEstimate = dtFrom;
                                            objTOLocationNewMooc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                                            objTOLocationNewMooc.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                            model.OPS_COTOLocation.Add(objTOLocationNewMooc);

                                            sort++;
                                        }

                                        //new coto
                                        var objTONew = new OPS_COTO();
                                        objTONew.CreatedBy = account.UserName;
                                        objTONew.CreatedDate = DateTime.Now;
                                        objTONew.COTOMasterID = objCON.COTOMasterID;
                                        objTONew.IsOPS = true;
                                        objTONew.SortOrder = objCON.COTOSort + sort;
                                        objTONew.LocationFromID = locationid;
                                        objTONew.LocationToID = objCON.LocationFromID;
                                        objTONew.ETD = dtFrom;
                                        objTONew.ETA = dtTo;
                                        objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                                        model.OPS_COTO.Add(objTONew);

                                        //new location
                                        var objTOLocationNew = new OPS_COTOLocation();
                                        objTOLocationNew.LocationID = locationid;
                                        objTOLocationNew.SortOrder = objCON.COTOSort + sort;
                                        objTOLocationNew.COTOMasterID = objCON.COTOMasterID;
                                        objTOLocationNew.CreatedBy = account.UserName;
                                        objTOLocationNew.CreatedDate = DateTime.Now;
                                        objTOLocationNew.DateComeEstimate = dtFrom;
                                        objTOLocationNew.DateLeaveEstimate = dtFrom;
                                        objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                                        objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                        model.OPS_COTOLocation.Add(objTOLocationNew);

                                        var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objCON.COTOMasterID);
                                        if (objMaster != null)
                                        {
                                            objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterDelivery;
                                            objMaster.ModifiedBy = account.UserName;
                                            objMaster.ModifiedDate = DateTime.Now;
                                        }

                                        foreach (var item in lstAdd)
                                        {
                                            model.OPS_COTOContainer.Add(item);
                                        }
                                        model.SaveChanges();

                                        //model.SaveChanges();



                                        //foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                                        //{

                                        //}
                                        //var lstOPSContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID).Select(c => c.OPSContainerID).Distinct().ToList();
                                        //foreach (var opscontainerid in lstOPSContainerID)
                                        //{

                                        //}  
                                    }
                                    else
                                    {
                                        var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objCON.COTOMasterID);
                                        if (objMaster != null)
                                        {
                                            objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterDelivery;
                                            objMaster.ModifiedBy = account.UserName;
                                            objMaster.ModifiedDate = DateTime.Now;

                                            model.SaveChanges();
                                        }
                                    }
                                }

                            }

                            MONCO_TOContainer_ResetDetail(model, account, objCON.COTOMasterID);
                        }
                        else
                            throw FaultHelper.BusinessFault(null, null, "Rờ mooc phải hoàn thành chặng còn lại");
                    }
                    else
                        throw FaultHelper.BusinessFault(null, null, "Rờ mooc có chuyến trước chưa hoàn tất" + objCheck.Code);
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "Đầu kéo có chuyến trước chưa hoàn tất " + objCheck.Code);
            }

            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_Complete(DataEntities model, AccountItem account, int tocontainerid)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID.Value,
                c.LocationFromID,
                c.LocationToID,
                c.OPSContainerID,
                c.OPS_COTOMaster.VehicleID,
                c.OPS_COTOMaster.RomoocID,
                c.OPS_COTOMaster.VendorOfVehicleID,
                c.TypeOfStatusContainerID,
                c.SortOrder,
                c.COTOSort,
                c.OPS_COTOMaster.StatusOfCOTOMasterID
            }).FirstOrDefault();
            if (objCON != null)
            {
                var lstOPSContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID).Select(c => c.OPSContainerID).Distinct().ToList();
                if (objCON.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterReceived)
                {
                    //container change  
                    foreach (var tocon in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOSort <= objCON.COTOSort))
                    {
                        tocon.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                    }
                    //coto change 
                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder <= objCON.COTOSort))
                    {
                        coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                    }
                    //location change
                    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder <= objCON.COTOSort + 1))
                    {
                        if (toloc.SortOrder == objCON.COTOSort + 1)
                            toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusCome;
                        else
                            toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                    }
                    model.SaveChanges();

                    foreach (var opscontainerid in lstOPSContainerID)
                    {
                        var opscontainer = model.OPS_Container.FirstOrDefault(c => c.ID == opscontainerid);
                        if (opscontainer != null)
                        {
                            if (model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.StatusOfCOContainerID != -(int)SYSVarType.StatusOfCOContainerLoad && c.StatusOfCOContainerID != -(int)SYSVarType.StatusOfCOContainerUnLoad && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerComplete).Count() == 0)
                            {
                                opscontainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeComplete;
                            }
                            else
                            {
                                opscontainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeRunning;
                            }
                        }
                    }
                    model.SaveChanges();

                    bool iscomplete = false;
                    if (model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.StatusOfCOContainerID != -(int)SYSVarType.StatusOfCOContainerLoad && c.StatusOfCOContainerID != -(int)SYSVarType.StatusOfCOContainerUnLoad && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerComplete).Count() == 0)
                    {
                        var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objCON.COTOMasterID);
                        if (objMaster != null)
                        {
                            objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterReceived;
                            objMaster.ATD = objMaster.ETD;
                            objMaster.ATA = objMaster.ETA;
                            iscomplete = true;
                        }
                        HelperTimeSheet.Create(model, account, objCON.COTOMasterID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                    }
                    HelperStatus.OPSCOMaster_Status(model, account, new List<int> { objCON.COTOMasterID });
                    if (iscomplete)
                        HelperFinance.Container_CompleteSchedule(model, account, objCON.COTOMasterID, null);

                    var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == objCON.VehicleID);
                    var objMooc = model.CAT_Romooc.FirstOrDefault(c => c.ID == objCON.RomoocID);
                    if (objVehicle != null)
                    {
                        objVehicle.LocationID = objCON.LocationToID;
                        objVehicle.CurrentRomoocID = objCON.RomoocID;
                    }
                    if (objMooc != null)
                    {
                        objMooc.LocationID = objCON.LocationToID;
                    }
                    model.SaveChanges();

                    return HelperTOMaster_Error.None;
                }
                else
                {
                    foreach (var opscontainerid in lstOPSContainerID)
                    {
                        var opscontainer = model.OPS_Container.FirstOrDefault(c => c.ID == opscontainerid);
                        if (opscontainer != null)
                        {
                            if (model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.StatusOfCOContainerID != -(int)SYSVarType.StatusOfCOContainerLoad && c.StatusOfCOContainerID != -(int)SYSVarType.StatusOfCOContainerUnLoad && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerComplete).Count() == 0)
                            {
                                opscontainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeComplete;
                            }
                            else
                            {
                                opscontainer.OPSContainerTypeID = -(int)SYSVarType.OPSContainerTypeRunning;
                            }
                        }
                    }
                    model.SaveChanges();
                    HelperStatus.OPSCOMaster_Status(model, account, new List<int> { objCON.COTOMasterID });
                }
            }
            return HelperTOMaster_Error.None;
        }

        public static int MONCO_TOContainer_Continuous(DataEntities model, AccountItem account, int tocontainerid)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                c.COTOMasterID,
                c.COTOSort,
                c.OPS_COTOMaster.VehicleID,
                c.OPS_COTOMaster.ETA,
                c.OPS_COTOMaster.ATA
            }).FirstOrDefault();
            if (objCON != null)
            {
                var lastTOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID).Select(c => new { c.ID, c.TypeOfStatusContainerID, c.COTOSort, c.OPS_COTOMaster.ETD }).OrderByDescending(c => c.COTOSort).FirstOrDefault();
                if (lastTOContainer != null)
                {
                    if (lastTOContainer.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerComplete)
                    {
                        MONCO_TOContainer_Complete(model, account, lastTOContainer.ID);
                    }

                    var next = model.OPS_COTOMaster.Where(c => c.VehicleID == objCON.VehicleID && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == account.SYSCustomerID) && c.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterTendered && c.ETD > lastTOContainer.ETD).Select(c => new { c.ID, c.ETD }).OrderBy(c => c.ETD).FirstOrDefault();
                    if (next != null)
                    {
                        var objCONNext = model.OPS_COTOContainer.Where(c => c.COTOMasterID == next.ID && c.COTOSort == 1 && c.IsSplit == false).Select(c => new { c.ID }).FirstOrDefault();
                        if (objCONNext != null)
                        {
                            return next.ID; 
                            //var offerError = MONCO_TOContainer_StartOffer(model, account, objCONNext.ID);
                            //if (offerError == HelperTOMaster_Error.None)
                            //    MONCO_TOContainer_Start(model, account, objCONNext.ID, false);
                        }
                    }
                }
            }
            return -1;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_EndStation(DataEntities model, AccountItem account, int tocontainerid, int? locationmoocid = null, int? locationvehicleid = null)
        {
            double hourmatrixtemp = 0; 
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                c.ParentID,
                COTOMasterID = c.COTOMasterID.Value,
                c.LocationFromID,
                c.LocationToID,
                c.OPSContainerID,
                c.OPS_COTOMaster.VehicleID,
                c.OPS_COTOMaster.RomoocID,
                c.OPS_COTOMaster.VendorOfVehicleID,
                c.TypeOfStatusContainerID,
                c.StatusOfCOContainerID,
                c.OPS_COTOMaster.StatusOfCOTOMasterID,
                c.SortOrder,
                c.ETA,
                c.DateToCome,
                c.COTOSort
            }).FirstOrDefault();
            if (objCON != null && model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOSort > objCON.COTOSort).Count() > 0)
            {
                objCON = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOSort > objCON.COTOSort).Select(c => new
                {
                    c.ID,
                    c.ParentID,
                    COTOMasterID = c.COTOMasterID.Value,
                    c.LocationFromID,
                    c.LocationToID,
                    c.OPSContainerID,
                    c.OPS_COTOMaster.VehicleID,
                    c.OPS_COTOMaster.RomoocID,
                    c.OPS_COTOMaster.VendorOfVehicleID,
                    c.TypeOfStatusContainerID,
                    c.StatusOfCOContainerID,
                    c.OPS_COTOMaster.StatusOfCOTOMasterID,
                    c.SortOrder,
                    c.ETA,
                    c.DateToCome,
                    c.COTOSort
                }).OrderByDescending(c => c.COTOSort).FirstOrDefault();
            }
            if (objCON != null)
            {
                bool hasMasterWait = model.OPS_COTOContainer.Where(c => c.OPSContainerID == objCON.OPSContainerID && c.COTOMasterID > 0 && c.COTOMasterID != objCON.COTOMasterID && c.OPS_COTOMaster.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterReceived).Count() > 0;
                bool hasPlanWait = model.OPS_COTOContainer.Where(c => c.OPSContainerID == objCON.OPSContainerID && c.COTOMasterID == null && c.ParentID == null && !statusload.Contains(c.StatusOfCOContainerID)).Count() > 0;
                //bool hasother=model.OPS_COTOContainer.Where(c=>c.OPSContainerID==objCON.OPSContainerID && c.COTOMasterID>0 && c.COTOMasterID!=objCON.COTOMasterID &&)
                if (!hasMasterWait && !hasPlanWait && objCON.StatusOfCOContainerID != -(int)SYSVarType.StatusOfCOContainerReturnRomooc && objCON.StatusOfCOContainerID != -(int)SYSVarType.StatusOfCOContainerReturnStation)
                {
                    bool returnmooc = false;
                    bool returnbase = false;
                    if (locationmoocid > 0 && locationvehicleid > 0)
                    {
                        if (locationmoocid == locationvehicleid)
                        {
                            returnmooc = true;
                            returnbase = false;
                        }
                        else
                        {
                            returnmooc = true;
                            returnbase = true;
                        }
                    }
                    else if (locationvehicleid > 0)
                    {
                        returnmooc = false;
                        returnbase = true;
                    }
                    else if (locationmoocid > 0)
                    {
                        returnmooc = true;
                        returnbase = false;
                    }

                    if (returnmooc == true || returnbase == true)
                    {
                        int? parentid = objCON.ParentID;
                        var locationid = objCON.LocationToID;
                        int sort = 1;
                        var lstAdd = new List<OPS_COTOContainer>();
                        var dtFrom = objCON.DateToCome.Value.AddHours(HourEmpty);
                        var dtTo = dtFrom.AddHours(hourmatrixtemp);
                        if (returnmooc == true)
                        {
                            dtTo = dtFrom.AddHours(hourmatrixtemp);

                            var tocontainer = new OPS_COTOContainer()
                            {
                                CreatedBy = account.UserName,
                                CreatedDate = DateTime.Now,

                                ParentID = parentid,
                                OPSContainerID = objCON.OPSContainerID,
                                IsSwap = false,
                                IsInput = false,
                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete,
                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerReturnRomooc,
                                CreateByMasterID = objCON.COTOMasterID,

                                COTOMasterID = objCON.COTOMasterID,
                                LocationFromID = locationid,
                                LocationToID = locationmoocid.Value,
                                ETD = dtFrom,
                                ETA = dtTo,
                                ETDStart = dtFrom,
                                ETAStart = dtTo,
                                DateFromCome = dtFrom,
                                DateToCome = dtTo,
                                SortOrder = objCON.SortOrder + sort,
                                COTOSort = objCON.COTOSort + sort,
                                IsSplit = false
                            };
                            model.OPS_COTOContainer.Add(tocontainer);
                            dtFrom = dtTo.AddHours(HourEmpty);

                            //new coto
                            var objCOTO = new OPS_COTO();
                            objCOTO.CreatedBy = account.UserName;
                            objCOTO.CreatedDate = DateTime.Now;
                            objCOTO.COTOMasterID = objCON.COTOMasterID;
                            objCOTO.IsOPS = true;
                            objCOTO.SortOrder = objCON.COTOSort + sort;
                            objCOTO.LocationFromID = locationid;
                            objCOTO.LocationToID = locationmoocid.Value;
                            objCOTO.ETD = dtFrom;
                            objCOTO.ETA = dtTo;
                            objCOTO.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                            model.OPS_COTO.Add(objCOTO);

                            //new location
                            var objTOLocation = new OPS_COTOLocation();
                            objTOLocation.LocationID = locationmoocid.Value;
                            objTOLocation.SortOrder = objCON.COTOSort + sort + 1;
                            objTOLocation.COTOMasterID = objCON.COTOMasterID;
                            objTOLocation.CreatedBy = account.UserName;
                            objTOLocation.CreatedDate = DateTime.Now;
                            objTOLocation.DateComeEstimate = dtFrom;
                            objTOLocation.DateLeaveEstimate = dtFrom;
                            objTOLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusCome;
                            objTOLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                            model.OPS_COTOLocation.Add(objTOLocation);

                            locationid = locationmoocid.Value;
                            sort++;
                        }
                        if (returnbase == true)
                        {
                            dtTo = dtFrom.AddHours(hourmatrixtemp);

                            var tocontainer = new OPS_COTOContainer()
                            {
                                CreatedBy = account.UserName,
                                CreatedDate = DateTime.Now,

                                ParentID = parentid,
                                OPSContainerID = objCON.OPSContainerID,
                                IsSwap = false,
                                IsInput = false,
                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete,
                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerReturnStation,
                                CreateByMasterID = objCON.COTOMasterID,

                                COTOMasterID = objCON.COTOMasterID,
                                LocationFromID = locationid,
                                LocationToID = locationvehicleid.Value,
                                ETD = dtFrom,
                                ETA = dtTo,
                                ETDStart = dtFrom,
                                ETAStart = dtTo,
                                DateFromCome = dtFrom,
                                DateToCome = dtTo,
                                SortOrder = objCON.SortOrder + sort,
                                COTOSort = objCON.COTOSort + sort,
                                IsSplit = false
                            };
                            model.OPS_COTOContainer.Add(tocontainer);
                            dtFrom = dtTo.AddHours(HourEmpty);

                            //new coto
                            var objCOTO = new OPS_COTO();
                            objCOTO.CreatedBy = account.UserName;
                            objCOTO.CreatedDate = DateTime.Now;
                            objCOTO.COTOMasterID = objCON.COTOMasterID;
                            objCOTO.IsOPS = true;
                            objCOTO.SortOrder = objCON.COTOSort + sort;
                            objCOTO.LocationFromID = locationid;
                            objCOTO.LocationToID = locationvehicleid.Value;
                            objCOTO.ETD = dtFrom;
                            objCOTO.ETA = dtTo;
                            objCOTO.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                            model.OPS_COTO.Add(objCOTO);

                            //new location
                            var objTOLocation = new OPS_COTOLocation();
                            objTOLocation.LocationID = locationvehicleid.Value;
                            objTOLocation.SortOrder = objCON.COTOSort + sort + 1;
                            objTOLocation.COTOMasterID = objCON.COTOMasterID;
                            objTOLocation.CreatedBy = account.UserName;
                            objTOLocation.CreatedDate = DateTime.Now;
                            objTOLocation.DateComeEstimate = dtFrom;
                            objTOLocation.DateLeaveEstimate = dtFrom;
                            objTOLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusCome;
                            objTOLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                            model.OPS_COTOLocation.Add(objTOLocation);

                            sort++;
                        }
                        model.SaveChanges();

                        var cotolast = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.IsSplit == false).Select(c => new { c.ID, c.COTOSort }).OrderByDescending(c => c.COTOSort).FirstOrDefault();
                        if (cotolast != null)
                        {
                            MONCO_TOContainer_Complete(model, account, cotolast.ID);
                            var objRomooc = model.CAT_Romooc.FirstOrDefault(c => c.ID == objCON.RomoocID);
                            var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == objCON.VehicleID);
                            if (objRomooc != null && objVehicle != null)
                            {
                                if (locationmoocid > 0)
                                {
                                    objRomooc.LocationID = locationmoocid.Value;
                                    objRomooc.ModifiedBy = account.UserName;
                                    objRomooc.ModifiedDate = DateTime.Now;

                                    objVehicle.LocationID = locationmoocid.Value;
                                    objVehicle.CurrentRomoocID = null;
                                    objVehicle.ModifiedBy = account.UserName;
                                    objVehicle.ModifiedDate = DateTime.Now;
                                }
                                else
                                {
                                    objRomooc.LocationID = objCON.LocationToID;
                                    objRomooc.ModifiedBy = account.UserName;
                                    objRomooc.ModifiedDate = DateTime.Now;
                                }
                                if (locationvehicleid > 0)
                                {
                                    objVehicle.LocationID = locationvehicleid.Value;
                                    objVehicle.CurrentRomoocID = null;
                                    objVehicle.ModifiedBy = account.UserName;
                                    objVehicle.ModifiedDate = DateTime.Now;
                                }
                                model.SaveChanges();
                            }
                        }
                    }
                }
                else
                {
                    throw FaultHelper.BusinessFault(null, null, "Đã kết thúc chuyến");
                }
            }
            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_Others(DataEntities model, AccountItem account, int tocontainerid, int masterotherid)
        {
            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_UnComplete(DataEntities model, AccountItem account, int tocontainerid)
        {
            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_ChangeDepotGet(DataEntities model, AccountItem account, int tocontainerid, int locationid, int reasionid, string reasionnote)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID.Value,
                c.COTOSort,
                c.LocationFromID,
                c.LocationToID,
                c.OPS_Container.ORD_Container.LocationDepotID,
                c.OPS_Container.ORD_Container.LocationDepotReturnID,
            }).FirstOrDefault();
            if (objCON != null && objCON.LocationFromID != locationid && objCON.LocationToID != locationid)
            {
                if (objCON.LocationDepotID > 0)
                {
                    if (model.CUS_Location.Where(c => c.ID == objCON.LocationDepotID && c.LocationID == objCON.LocationToID).Count() > 0)
                    {
                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && (c.COTOSort == objCON.COTOSort || c.COTOSort == objCON.COTOSort + 1) && c.IsSplit == false))
                        {
                            if (tocontainer.COTOSort == objCON.COTOSort)
                                tocontainer.LocationToID = locationid;
                            else if (tocontainer.COTOSort == objCON.COTOSort + 1)
                                tocontainer.LocationFromID = locationid;
                            tocontainer.ReasonChangeID = reasionid;
                            tocontainer.ReasonChangeNote = reasionnote;
                        }

                        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort))
                        {
                            coto.LocationToID = locationid;
                        }

                        foreach (var tolocation in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort + 1))
                        {
                            tolocation.LocationID = locationid;
                        }
                        model.SaveChanges();
                    }
                    else if (model.CUS_Location.Where(c => c.ID == objCON.LocationDepotID && c.LocationID == objCON.LocationFromID).Count() > 0)
                    {
                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && (c.COTOSort == objCON.COTOSort || c.COTOSort == objCON.COTOSort - 1) && c.IsSplit == false))
                        {
                            if (tocontainer.COTOSort == objCON.COTOSort)
                                tocontainer.LocationFromID = locationid;
                            else if (tocontainer.COTOSort == objCON.COTOSort - 1)
                                tocontainer.LocationToID = locationid;
                            tocontainer.ReasonChangeID = reasionid;
                            tocontainer.ReasonChangeNote = reasionnote;
                        }

                        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort))
                        {
                            coto.LocationFromID = locationid;
                        }

                        foreach (var tolocation in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort - 1))
                        {
                            tolocation.LocationID = locationid;
                        }
                        model.SaveChanges();

                        var cotoPrev = model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort - 1).Select(c => new { c.LocationFromID, c.LocationToID, c.SortOrder }).FirstOrDefault();
                        if (cotoPrev != null && cotoPrev.LocationFromID == cotoPrev.LocationToID)
                        {
                            foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOSort >= cotoPrev.SortOrder && c.IsSplit == false))
                            {
                                if (tocontainer.COTOSort == cotoPrev.SortOrder)
                                    model.OPS_COTOContainer.Remove(tocontainer);
                                else
                                    tocontainer.COTOSort--;
                            }

                            foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder >= cotoPrev.SortOrder))
                            {
                                if (coto.SortOrder == cotoPrev.SortOrder)
                                    model.OPS_COTO.Remove(coto);
                                else
                                    coto.SortOrder--;
                            }

                            foreach (var tolocation in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder >= cotoPrev.SortOrder))
                            {
                                if (tolocation.SortOrder == cotoPrev.SortOrder)
                                    model.OPS_COTOLocation.Remove(tolocation);
                                else
                                    tolocation.SortOrder--;
                            }
                            model.SaveChanges();
                        }

                        return HelperTOMaster_Error.None;
                    }
                }
            }
            return HelperTOMaster_Error.Fail;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_AddDepotGet(DataEntities model, AccountItem account, int tocontainerid, int locationid, int reasionid, string reasionnote)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID.Value,
                c.COTOSort,
                c.LocationFromID,
                c.LocationToID,
                c.OPS_Container.ORD_Container.LocationDepotID,
                c.OPS_Container.ORD_Container.LocationDepotReturnID,
            }).FirstOrDefault();
            if (objCON != null && objCON.LocationFromID != locationid && objCON.LocationToID != locationid)
            {
                if (model.CUS_Location.Where(c => c.ID == objCON.LocationDepotID && c.LocationID == objCON.LocationFromID).Count() > 0)
                {
                    MONCO_BreakTO(model, account, objCON.ID, locationid, reasionid, reasionnote);

                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOSort == objCON.COTOSort && c.IsSplit == false))
                    {
                        tocontainer.StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetEmpty;
                    }
                    model.SaveChanges();
                }

                return HelperTOMaster_Error.None;
            }
            return HelperTOMaster_Error.Fail;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_ChangeDepotReturn(DataEntities model, AccountItem account, int tocontainerid, int locationid, int reasionid, string reasionnote)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID.Value,
                c.COTOSort,
                c.LocationFromID,
                c.LocationToID,
                c.OPS_Container.ORD_Container.LocationDepotID,
                c.OPS_Container.ORD_Container.LocationDepotReturnID,
            }).FirstOrDefault();
            if (objCON != null && objCON.LocationFromID != locationid && objCON.LocationToID != locationid)
            {
                if (objCON.LocationDepotReturnID > 0)
                {
                    if (model.CUS_Location.Where(c => c.ID == objCON.LocationDepotReturnID && c.LocationID == objCON.LocationToID).Count() > 0)
                    {
                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && (c.COTOSort == objCON.COTOSort || c.COTOSort == objCON.COTOSort + 1) && c.IsSplit == false))
                        {
                            if (tocontainer.COTOSort == objCON.COTOSort)
                                tocontainer.LocationToID = locationid;
                            else if (tocontainer.COTOSort == objCON.COTOSort + 1)
                                tocontainer.LocationFromID = locationid;
                            tocontainer.ReasonChangeID = reasionid;
                            tocontainer.ReasonChangeNote = reasionnote;
                        }

                        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort))
                        {
                            coto.LocationToID = locationid;
                        }

                        foreach (var tolocation in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort + 1))
                        {
                            tolocation.LocationID = locationid;
                        }
                        model.SaveChanges();
                    }
                    else if (model.CUS_Location.Where(c => c.ID == objCON.LocationDepotReturnID && c.LocationID == objCON.LocationFromID).Count() > 0)
                    {
                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && (c.COTOSort == objCON.COTOSort || c.COTOSort == objCON.COTOSort - 1) && c.IsSplit == false))
                        {
                            if (tocontainer.COTOSort == objCON.COTOSort)
                                tocontainer.LocationFromID = locationid;
                            else if (tocontainer.COTOSort == objCON.COTOSort - 1)
                                tocontainer.LocationToID = locationid;
                            tocontainer.ReasonChangeID = reasionid;
                            tocontainer.ReasonChangeNote = reasionnote;
                        }

                        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort))
                        {
                            coto.LocationFromID = locationid;
                        }

                        foreach (var tolocation in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort - 1))
                        {
                            tolocation.LocationID = locationid;
                        }
                        model.SaveChanges();

                        var cotoPrev = model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder == objCON.COTOSort - 1).Select(c => new { c.LocationFromID, c.LocationToID, c.SortOrder }).FirstOrDefault();
                        if (cotoPrev != null && cotoPrev.LocationFromID == cotoPrev.LocationToID)
                        {
                            foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOSort >= cotoPrev.SortOrder && c.IsSplit == false))
                            {
                                if (tocontainer.COTOSort == cotoPrev.SortOrder)
                                    model.OPS_COTOContainer.Remove(tocontainer);
                                else
                                    tocontainer.COTOSort--;
                            }

                            foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder >= cotoPrev.SortOrder))
                            {
                                if (coto.SortOrder == cotoPrev.SortOrder)
                                    model.OPS_COTO.Remove(coto);
                                else
                                    coto.SortOrder--;
                            }

                            foreach (var tolocation in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.SortOrder >= cotoPrev.SortOrder))
                            {
                                if (tolocation.SortOrder == cotoPrev.SortOrder)
                                    model.OPS_COTOLocation.Remove(tolocation);
                                else
                                    tolocation.SortOrder--;
                            }
                            model.SaveChanges();
                        }

                        return HelperTOMaster_Error.None;
                    }
                }
            }
            return HelperTOMaster_Error.Fail;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_AddDepotReturn(DataEntities model, AccountItem account, int tocontainerid, int locationid, int reasionid, string reasionnote)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID.Value,
                c.COTOSort,
                c.LocationFromID,
                c.LocationToID,
                c.OPS_Container.ORD_Container.LocationDepotID,
                c.OPS_Container.ORD_Container.LocationDepotReturnID,
            }).FirstOrDefault();
            if (objCON != null && objCON.LocationFromID != locationid && objCON.LocationToID != locationid)
            {
                if (objCON.LocationFromID == objCON.LocationDepotReturnID)
                {
                    MONCO_BreakTO(model, account, objCON.ID, locationid, reasionid, reasionnote);

                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOSort == objCON.COTOSort && c.IsSplit == false))
                    {
                        tocontainer.StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerReturnEmpty;
                    }
                    model.SaveChanges();
                }

                return HelperTOMaster_Error.None;
            }
            return HelperTOMaster_Error.Fail;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_AddStock(DataEntities model, AccountItem account, int tocontainerid, int locationid, int reasionid, string reasionnote)
        {
            List<int> statusload = new List<int>()
            {
                -(int)SYSVarType.StatusOfCOContainerUnLoad,
                -(int)SYSVarType.StatusOfCOContainerLoad,
            };
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID.Value,
                c.COTOSort,
                c.LocationFromID,
                c.LocationToID,
                c.ETD,
                c.ETA,
                c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID,
                CUSLocationFromID = c.OPS_Container.ORD_Container.LocationFromID,
                CUSLocationToID = c.OPS_Container.ORD_Container.LocationToID,
                c.OPS_Container.ORD_Container.LocationDepotID,
                c.OPS_Container.ORD_Container.LocationDepotReturnID,
            }).FirstOrDefault();
            if (objCON != null)
            {
                int stockid = -1;
                if (objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal)
                {
                    if (model.CUS_Location.Where(c => c.ID == objCON.CUSLocationFromID && c.LocationID == objCON.LocationToID).Count() > 0)
                    {
                        stockid = objCON.LocationToID;
                    }
                }
                if (objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport || objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal)
                {
                    if (model.CUS_Location.Where(c => c.ID == objCON.CUSLocationToID && c.LocationID == objCON.LocationToID).Count() > 0)
                    {
                        stockid = objCON.LocationToID;
                    }
                }

                if (stockid > 0 && stockid != locationid)
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOSort >= objCON.COTOSort && c.IsSplit == false && !statusload.Contains(c.StatusOfCOContainerID)))
                    {
                        if (tocontainer.COTOSort == objCON.COTOSort)
                        {
                            tocontainer.IsSplit = true;

                            var tocontainernew = new OPS_COTOContainer()
                            {
                                CreatedBy = account.UserName,
                                CreatedDate = DateTime.Now,

                                ParentID = tocontainer.ParentID,
                                OPSContainerID = tocontainer.OPSContainerID,
                                IsSwap = false,
                                IsInput = false,
                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                StatusOfCOContainerID = tocontainer.StatusOfCOContainerID,
                                CreateByMasterID = objCON.COTOMasterID,

                                COTOMasterID = objCON.COTOMasterID,
                                LocationFromID = objCON.LocationFromID,
                                LocationToID = objCON.LocationToID,
                                ETD = tocontainer.ETD,
                                ETA = tocontainer.ETA,
                                ETDStart = tocontainer.ETD,
                                ETAStart = tocontainer.ETA,
                                DateFromCome = tocontainer.ETD,
                                DateToCome = tocontainer.ETA,
                                SortOrder = tocontainer.SortOrder,
                                COTOSort = tocontainer.COTOSort,
                                IsSplit = false,
                                IsDuplicateHidden = tocontainer.IsDuplicateHidden
                            };
                            model.OPS_COTOContainer.Add(tocontainernew);

                            tocontainernew = new OPS_COTOContainer()
                            {
                                CreatedBy = account.UserName,
                                CreatedDate = DateTime.Now,

                                ParentID = tocontainer.ParentID,
                                OPSContainerID = tocontainer.OPSContainerID,
                                IsSwap = false,
                                IsInput = false,
                                TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                StatusOfCOContainerID = tocontainer.StatusOfCOContainerID,
                                CreateByMasterID = objCON.COTOMasterID,

                                COTOMasterID = objCON.COTOMasterID,
                                LocationFromID = objCON.LocationToID,
                                LocationToID = locationid,
                                ETD = tocontainer.ETA,
                                ETA = tocontainer.ETA,
                                ETDStart = tocontainer.ETA,
                                ETAStart = tocontainer.ETA,
                                DateFromCome = tocontainer.ETA,
                                DateToCome = tocontainer.ETA,
                                SortOrder = tocontainer.SortOrder + 1,
                                COTOSort = tocontainer.COTOSort + 1,
                                IsSplit = false
                            };
                            model.OPS_COTOContainer.Add(tocontainernew);
                        }
                        else
                        {
                            tocontainer.SortOrder++;
                            tocontainer.COTOSort++;
                        }
                    }

                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                    {
                        coto.SortOrder++;
                    }

                    foreach (var tolocation in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                    {
                        tolocation.SortOrder++;
                    }

                    //new coto
                    var objCOTO = new OPS_COTO();
                    objCOTO.CreatedBy = account.UserName;
                    objCOTO.CreatedDate = DateTime.Now;
                    objCOTO.COTOMasterID = objCON.COTOMasterID;
                    objCOTO.IsOPS = true;
                    objCOTO.SortOrder = objCON.COTOSort + 1;
                    objCOTO.LocationFromID = objCON.LocationToID;
                    objCOTO.LocationToID = locationid;
                    objCOTO.ETD = objCON.ETA;
                    objCOTO.ETA = objCON.ETA;
                    objCOTO.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                    model.OPS_COTO.Add(objCOTO);

                    //new location 
                    var objTOLocation = new OPS_COTOLocation();
                    objTOLocation.LocationID = locationid;
                    objTOLocation.SortOrder = objCON.COTOSort + 2;
                    objTOLocation.COTOMasterID = objCON.COTOMasterID;
                    objTOLocation.CreatedBy = account.UserName;
                    objTOLocation.CreatedDate = DateTime.Now;
                    objTOLocation.DateComeEstimate = objCON.ETA;
                    objTOLocation.DateLeaveEstimate = objCON.ETA;
                    objTOLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                    objTOLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                    model.OPS_COTOLocation.Add(objTOLocation);

                    model.SaveChanges();
                }


                //if(model.CUS_Location.Where(c=>c.ID==objCON.CUSLocationFromID && c.LocationID==))


                //if(objCON.LocationFromID)
                //MONCO_BreakTO(model, account, objCON.ID, locationid, reasionid, reasionnote);

                return HelperTOMaster_Error.None;
            }

            return HelperTOMaster_Error.Fail;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_BreakMooc(DataEntities model, AccountItem account, int tocontainerid, int reasionid, string reasionnote)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID.Value,
                c.COTOSort,
                c.LocationFromID,
                c.LocationToID
            }).FirstOrDefault();
            if (objCON != null)
            {
                MONCO_TOContainer_Complete(model, account, tocontainerid);
                
                var master = model.OPS_COTOMaster.Where(c => c.ID == objCON.COTOMasterID).Select(c => new DTOOPSCOTOMaster
                {
                    SortOrder = c.SortOrder,
                    VehicleID = c.VehicleID,
                    VendorOfVehicleID = c.VendorOfVehicleID,
                    RomoocID = c.RomoocID,
                    VendorOfRomoocID = c.VendorOfRomoocID,
                    DriverID1 = c.DriverID1,
                    DriverID2 = c.DriverID2,
                    DriverName1 = c.DriverName1,
                    DriverName2 = c.DriverName2,
                    DriverCard1 = c.DriverCard1,
                    DriverCard2 = c.DriverCard2,
                    DriverTel1 = c.DriverTel1,
                    DriverTel2 = c.DriverTel2,
                    GroupOfVehicleID = c.GroupOfVehicleID,
                    RateTime = c.RateTime,
                    ETD = c.ETD,
                    ETA = c.ETA,
                    DateConfig = c.ETD,
                    Note = c.Note,
                    IsBidding = c.IsBidding,
                    BiddingID = c.BiddingID,
                    KM = c.KM,
                    TransportModeID = c.TransportModeID,
                    TypeOfOrderID = c.TypeOfOrderID,
                    ContractID = c.ContractID
                }).FirstOrDefault();

                var objMaster = new OPS_COTOMaster();
                objMaster.CreatedBy = account.UserName;
                objMaster.CreatedDate = DateTime.Now;
                objMaster.SYSCustomerID = account.SYSCustomerID;

                objMaster.Code = OPSCO_GetLastCode(model);
                objMaster.IsHot = false;
                objMaster.RateTime = 0;
                objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproved;

                objMaster.SortOrder = master.SortOrder;
                objMaster.VehicleID = master.VehicleID;
                objMaster.VendorOfVehicleID = master.VendorOfVehicleID;
                objMaster.RomoocID = master.RomoocID;
                objMaster.VendorOfRomoocID = master.VendorOfRomoocID;
                objMaster.DriverID1 = master.DriverID1;
                objMaster.DriverID2 = master.DriverID2;
                objMaster.DriverName1 = master.DriverName1;
                objMaster.DriverName2 = master.DriverName2;
                objMaster.DriverCard1 = master.DriverCard1;
                objMaster.DriverCard2 = master.DriverCard2;
                objMaster.DriverTel1 = master.DriverTel1;
                objMaster.DriverTel2 = master.DriverTel2;
                objMaster.ApprovedBy = master.ApprovedBy;
                objMaster.ApprovedDate = master.ApprovedDate;
                objMaster.GroupOfVehicleID = master.GroupOfVehicleID;
                objMaster.RateTime = master.RateTime;
                objMaster.ETD = master.ETD;
                objMaster.ETA = master.ETA;
                objMaster.ATD = master.ETD;
                objMaster.ATA = master.ETA; 
                objMaster.TypeOfDriverID1 = -(int)SYSVarType.TypeOfDriverMain;

                objMaster.DateConfig = master.ETD;
                objMaster.Note = master.Note;
                objMaster.IsBidding = master.IsBidding;
                objMaster.BiddingID = master.BiddingID;
                objMaster.KM = master.KM;
                objMaster.TransportModeID = master.TransportModeID;
                objMaster.TypeOfOrderID = master.TypeOfOrderID;
                objMaster.ContractID = master.ContractID;

                model.OPS_COTOMaster.Add(objMaster);
                model.SaveChanges();

                foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                {
                    if (coto.SortOrder <= objCON.COTOSort)
                    {
                        coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                    }
                    else
                    {
                        coto.COTOMasterID = objMaster.ID;
                    }
                }
                foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                {
                    if (toloc.SortOrder <= objCON.COTOSort + 1)
                    {
                        if (toloc.SortOrder == objCON.COTOSort + 1)
                        {
                            var newLocation = new OPS_COTOLocation();
                            newLocation.LocationID = toloc.LocationID;
                            newLocation.SortOrder = toloc.SortOrder;
                            newLocation.COTOMasterID = objMaster.ID;
                            newLocation.CreatedBy = account.UserName;
                            newLocation.CreatedDate = DateTime.Now;
                            newLocation.DateComeEstimate = toloc.DateLeaveEstimate;
                            newLocation.DateLeaveEstimate = toloc.DateLeaveEstimate;
                            newLocation.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                            newLocation.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                            model.OPS_COTOLocation.Add(newLocation);
                        }
                        toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                    }
                    else
                    {
                        toloc.COTOMasterID = objMaster.ID;
                    }
                }
                foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.IsSplit == false))
                {
                    if (tocontainer.COTOSort <= objCON.COTOSort)
                    {
                        tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                        if (tocontainer.COTOSort == objCON.COTOSort)
                        {
                            tocontainer.IsBreakRomooc = true;
                            tocontainer.ReasonChangeID = reasionid;
                            tocontainer.ReasonChangeNote = reasionnote;
                            tocontainer.ModifiedBy = account.UserName;
                            tocontainer.ModifiedDate = DateTime.Now;
                        }
                    }
                    else
                    {
                        tocontainer.COTOMasterID = objMaster.ID;
                    }
                }
                var masterCurrent = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objCON.COTOMasterID);
                if (masterCurrent != null)
                {
                    masterCurrent.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterReceived;
                }
                var vehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == objMaster.VehicleID);
                if (vehicle != null)
                {
                    vehicle.CurrentRomoocID = null;
                    vehicle.LocationID = objCON.LocationToID;
                }
                var romooc = model.CAT_Romooc.FirstOrDefault(c => c.ID == objMaster.RomoocID);
                if (romooc != null)
                {
                    romooc.HasContainer = true;
                    romooc.LocationID = objCON.LocationToID;
                }
                model.SaveChanges();

                objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterTendered;
                int sort = 1;
                foreach (var item in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objMaster.ID).OrderBy(c => c.SortOrder))
                {
                    item.SortOrder = sort++;
                }
                sort = 1;
                foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objMaster.ID).OrderBy(c => c.SortOrder))
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objMaster.ID && c.COTOSort == coto.SortOrder))
                    {
                        tocontainer.COTOSort = sort;
                    }
                    coto.SortOrder = sort++;
                }

                return HelperTOMaster_Error.None;
            }

            return HelperTOMaster_Error.Fail;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_RepairEmpty(DataEntities model, AccountItem account, int tocontainerid, int locationid, int reasionid, string reasionnote)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID.Value,
                c.COTOSort,
                c.LocationFromID,
                c.LocationToID
            }).FirstOrDefault();
            if (objCON != null)
            {


                return HelperTOMaster_Error.None;
            }

            return HelperTOMaster_Error.Fail;
        }

        public static HelperTOMaster_Error MONCO_TOContainer_ChangeEmpty(DataEntities model, AccountItem account, int tocontainerid, int locationid, int reasionid, string reasionnote)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == tocontainerid && c.COTOMasterID > 0).Select(c => new
            {
                c.ID,
                COTOMasterID = c.COTOMasterID.Value,
                c.COTOSort,
                c.LocationFromID,
                c.LocationToID
            }).FirstOrDefault();
            if (objCON != null)
            {


                return HelperTOMaster_Error.None;
            }

            return HelperTOMaster_Error.Fail;
        }

        public static HelperTOMaster_MONReturn MONCO_TOContainer_ChangeRomoocOffer(DataEntities model, AccountItem account, int masterid, int romoocid)
        {
            var result = new HelperTOMaster_MONReturn();

            return result;
        }

        public static void MONCO_TOContainer_ChangeRomooc(DataEntities model, AccountItem account, int masterid, int romoocid)
        {

        }

        public static HelperTOMaster_MONReturn MONCO_TOContainer_ChangeVehicleOffer(DataEntities model, AccountItem account, int masterid, int vehicleid)
        {
            var result = new HelperTOMaster_MONReturn();

            return result;
        }

        public static void MONCO_TOContainer_ChangeVehicle(DataEntities model, AccountItem account, int masterid, int vehicleid)
        {

        }

        public static void MONCO_Fleet_VehicleLocation(DataEntities model, AccountItem account, int vehicleid, int locationid)
        {

        }

        public static void MONCO_Fleet_RomoocLocation(DataEntities model, AccountItem account, int romoocid, int vehicleid, int locationid)
        {

        }

        public static void MONCO_Fleet_ReturnStand(DataEntities model, AccountItem account, List<HelperTOMaster_Fleet> lstReturn)
        {

        }


        public static List<HelperTOMaster_Error> MONCO_TOContainerComplete_PrevListOffer(DataEntities model, AccountItem account, List<int> lstconid)
        {
            var result = new List<HelperTOMaster_Error>();
            if (lstconid != null && lstconid.Count > 0)
            {
                var lstContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && lstconid.Contains(c.ID) && c.IsSplit == false && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel)
                    .Select(c => new { c.COTOMasterID, c.OPSContainerID, c.SortOrder, c.COTOSort, c.TypeOfStatusContainerID }).ToList();
                var lstMasterID = lstContainer.Select(c => c.COTOMasterID).Distinct().ToList();
                if (lstMasterID.Count == 1)
                {

                }

            }
            return result;
        }

        public static List<HelperTOMaster_Error> MONCO_TOContainerComplete_PrevList(DataEntities model, AccountItem account, List<int> lstconid)
        {
            var result = new List<HelperTOMaster_Error>();
            if (lstconid != null && lstconid.Count > 0)
            {
                var lstContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && lstconid.Contains(c.ID) && c.IsSplit == false && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel)
                    .Select(c => new { c.COTOMasterID, c.OPSContainerID, c.SortOrder, c.COTOSort, c.TypeOfStatusContainerID }).ToList();
                var lstMasterID = lstContainer.Select(c => c.COTOMasterID).Distinct().ToList();
                if (lstMasterID.Count == 1)
                {

                }

                //var lstmasterid = new List<int>();

                //foreach (var itemContainer in lstContainer)
                //{
                //    //Check other master 
                //    if (itemContainer.TypeOfStatusContainerID < -(int)SYSVarType.TypeOfStatusContainerComplete &&
                //        model.OPS_COTOContainer.Where(c => c.COTOMasterID != itemContainer.COTOMasterID && c.OPSContainerID == itemContainer.OPSContainerID && c.SortOrder < itemContainer.SortOrder && c.IsSplit == false &&
                //        c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel && c.TypeOfStatusContainerID < -(int)SYSVarType.TypeOfStatusContainerComplete).Count() == 0)
                //    {
                //        //container change
                //        foreach (var tocon in model.OPS_COTOContainer.Where(c => c.COTOMasterID == itemContainer.COTOMasterID && c.COTOSort <= itemContainer.COTOSort))
                //        {
                //            tocon.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                //        }
                //        //coto change 
                //        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == itemContainer.COTOMasterID && c.SortOrder <= itemContainer.COTOSort))
                //        {
                //            coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                //        }
                //        //location change
                //        foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == itemContainer.COTOMasterID && c.SortOrder <= itemContainer.COTOSort + 1))
                //        {
                //            if (toloc.SortOrder == itemContainer.COTOSort + 1)
                //                toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusCome;
                //            else
                //                toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                //        }
                //        model.SaveChanges();
                //        if (!lstmasterid.Contains(itemContainer.COTOMasterID.Value))
                //            lstmasterid.Add(itemContainer.COTOMasterID.Value);
                //        result.Add(HelperTOMaster_Error.None);
                //    }
                //    else
                //        result.Add(HelperTOMaster_Error.Fail);
                //}
                //MONCO_CompleteParent(model, account, lstmasterid);

                //foreach (var masterid in lstmasterid)
                //{
                //    bool iscomplete = false;
                //    if (model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerComplete).Count() == 0)
                //    {
                //        var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
                //        if (objMaster != null)
                //        {
                //            objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterReceived;
                //            objMaster.ATD = objMaster.ETD;
                //            objMaster.ATA = objMaster.ETA;
                //            iscomplete = true;
                //        }
                //        HelperTimeSheet.Create(model, account, masterid, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                //        //HelperFinance.Container_CompleteSchedule(model, account, masterid, null);
                //        //HelperFinance.Container_CompleteSchedule(model, account, masterid, null);
                //    }
                //    HelperStatus.OPSCOMaster_Status(model, account, new List<int> { masterid });
                //    if (iscomplete)
                //        HelperFinance.Container_CompleteSchedule(model, account, masterid, null);
                //}

            }
            else
                throw FaultHelper.BusinessFault(null, null, "list null");
            return result;
        }

        public static List<HelperTOMaster_Error> MONCO_TOContainerComplete_AllList(DataEntities model, AccountItem account, List<int> lstmasterid)
        {
            var result = new List<HelperTOMaster_Error>();
            if (lstmasterid != null && lstmasterid.Count > 0)
            {
                var lstMasterCheck = new List<int>();
                foreach (var masterid in lstmasterid)
                {
                    if (model.OPS_COTO.Where(c => c.COTOMasterID == masterid && c.COTOStatusID != -(int)SYSVarType.COTOStatusComplete).Count() > 0)
                    {
                        //location change
                        foreach (var loc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid))
                        {
                            loc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                        }
                        //coto change 
                        foreach (var to in model.OPS_COTO.Where(c => c.COTOMasterID == masterid))
                        {
                            to.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                        }
                        //container change
                        foreach (var co in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel))
                        {
                            co.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                        }
                        lstMasterCheck.Add(masterid);
                        result.Add(HelperTOMaster_Error.None);
                    }
                    else
                        result.Add(HelperTOMaster_Error.Fail);
                }
                model.SaveChanges();
                MONCO_CompleteParent(model, account, lstMasterCheck);

                foreach (var masterid in lstmasterid)
                {
                    if (model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerComplete).Count() == 0)
                    {
                        var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
                        if (objMaster != null)
                        {
                            objMaster.ATD = objMaster.ETD;
                            objMaster.ATA = objMaster.ETA;
                        }
                        HelperTimeSheet.Create(model, account, masterid, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                        //HelperFinance.Container_CompleteSchedule(model, account, masterid, null);

                        HelperFinance.Container_CompleteSchedule(model, account, masterid, null);
                    }
                }

                //foreach (var masterid in lstmasterid)
                //{
                //    var count = model.OPS_COTOContainer.Count(c => c.COTOMasterID == masterid && c.TypeOfStatusContainerID < -(int)SYSVarType.TypeOfStatusContainerComplete);
                //    if (count == 0)
                //    {
                //        HelperFinance.Container_CompleteSchedule(model, account, masterid, null);
                //        var qr = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
                //        if (qr != null)
                //        {
                //            //qr.ATA = master.ATA;
                //            //qr.ATD = master.ATD;
                //            qr.ATD = qr.ETD;
                //            qr.ATA = qr.ETA;
                //            HelperFinance.Container_TimeChange(model, account, masterid);
                //            model.SaveChanges();
                //        }
                //    }
                //    else
                //        throw FaultHelper.BusinessFault(null, null, "Dữ liệu chặng container bị lỗi");

                //}
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list null");
            return result;
        }

        public static List<HelperTOMaster_Error> MONCO_TOContainerUnComplete_AllList(DataEntities model, AccountItem account, List<int> lstmasterid)
        {
            var result = new List<HelperTOMaster_Error>();
            if (lstmasterid != null && lstmasterid.Count > 0)
            {
                var lstMasterCheck = new List<int>();
                foreach (var masterid in lstmasterid)
                {
                    var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
                    if (objMaster != null)
                    {
                        //location change 
                        foreach (var loc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid))
                        {
                            loc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                        }
                        //coto change
                        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterid))
                        {
                            coto.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                        }
                        //container change
                        foreach (var co in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel))
                        {
                            co.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait;
                        }
                        objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterTendered;
                        result.Add(HelperTOMaster_Error.None);
                    }
                }
                model.SaveChanges();
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list null");
            return result;
        }

        public static HelperTOMaster_Error MONCO_TOContainerComplete_Wait(DataEntities model, AccountItem account, int id, int reasionid, string reasionnote, double houradd)
        {
            if (model.CAT_Reason.Where(c => c.ID == reasionid).Count() == 0)
                return HelperTOMaster_Error.ReasionFail;
            var nextid = MONCO_TOContainerComplete_Run(model, account, id, reasionid, reasionnote, houradd);
            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error MONCO_TOContainerComplete_Break(DataEntities model, AccountItem account, int id, int reasionid, string reasionnote, int? vehicleid, double houradd)
        {
            if (model.CAT_Reason.Where(c => c.ID == reasionid).Count() == 0)
                return HelperTOMaster_Error.ReasionFail;
            //var nextid = MONCO_TOContainerComplete_Run(model, account, id, reasionid, reasionnote, houradd);
            MONCO_TOContainer_Complete(model, account, id);
            MONCO_BreakRommoc(model, account, id, reasionid, reasionnote, vehicleid, houradd);
            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error MONCO_AddHourEmpty(DataEntities model, AccountItem account, int opscontainerid, double houradd)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.ID == opscontainerid && c.COTOMasterID > 0).Select(c => new { c.ID, COTOMasterID = c.COTOMasterID.Value, c.OPS_COTOMaster.ETA, c.OPS_COTOMaster.ATA }).FirstOrDefault();
            if (objCON != null)
            {
                var ata = objCON.ETA;
                if (objCON.ATA != null)
                    ata = objCON.ATA.Value;
                var query = model.OPS_COTOMaster.Where(c => c.ETD > ata);
                if (query.Count() > 0)
                {
                    if (query.Where(c => c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived).Count() > 0)
                        return HelperTOMaster_Error.AddHourReceived;
                    else
                    {
                        var lstMasterID = new List<int>();
                        foreach (var objMaster in query)
                        {
                            lstMasterID.Add(objMaster.ID);
                            objMaster.ETD = objMaster.ETD.AddHours(houradd);
                            objMaster.ETA = objMaster.ETA.AddHours(houradd);
                            objMaster.ModifiedBy = account.UserName;
                            objMaster.ModifiedDate = DateTime.Now;
                            foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objMaster.ID))
                            {
                                tocontainer.ETD = tocontainer.ETD.Value.AddHours(houradd);
                                tocontainer.ETDStart = tocontainer.ETD.Value.AddHours(houradd);
                                tocontainer.ETA = tocontainer.ETA.Value.AddHours(houradd);
                                tocontainer.ETAStart = tocontainer.ETA.Value.AddHours(houradd);
                                tocontainer.ModifiedBy = account.UserName;
                                tocontainer.ModifiedDate = DateTime.Now;
                            }
                            foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objMaster.ID))
                            {
                                toloc.DateComeEstimate = toloc.DateComeEstimate.Value.AddHours(houradd);
                                toloc.ModifiedBy = account.UserName;
                                toloc.ModifiedDate = DateTime.Now;
                            }
                        }
                        model.SaveChanges();

                        foreach (var masterid in lstMasterID)
                        {
                            HelperTimeSheet.Create(model, account, masterid, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                        }
                    }
                }
            }
            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error MONCO_MasterChangeETD(DataEntities model, AccountItem account, int id, DateTime etd)
        {
            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == id);
            if (objMaster != null)
            {
                var totalHours = (objMaster.ETA - objMaster.ETD).TotalHours;
                objMaster.ETD = etd;
                objMaster.ETA = etd.AddHours(totalHours);
                objMaster.ModifiedBy = account.UserName;
                objMaster.ModifiedDate = DateTime.Now;
                model.SaveChanges();

                HelperTimeSheet.Remove(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                if (objMaster.VehicleID > 0 && (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID))
                    HelperTimeSheet.Create(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
            }

            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error MONCO_MasterChangeRomooc(DataEntities model, AccountItem account, int id, int romoocid)
        {
            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == id);
            if (objMaster != null)
            {
                if (objMaster.SYSCustomerID > 0 && objMaster.SYSCustomerID == account.SYSCustomerID)
                {
                    objMaster.RomoocID = romoocid;
                    objMaster.ModifiedBy = account.UserName;
                    objMaster.ModifiedDate = DateTime.Now;
                    model.SaveChanges();
                }

                //HelperTimeSheet.Remove(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                //if (objMaster.VehicleID > 0 && (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID))
                //    HelperTimeSheet.Create(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
            }
            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error MONCO_MasterChangeVehicle(DataEntities model, AccountItem account, int id, int vehicleid)
        {
            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == id);
            if (objMaster != null)
            {
                if (objMaster.SYSCustomerID > 0 && objMaster.SYSCustomerID == account.SYSCustomerID)
                {
                    objMaster.VehicleID = vehicleid;
                    objMaster.ModifiedBy = account.UserName;
                    objMaster.ModifiedDate = DateTime.Now;
                    model.SaveChanges();
                }

                //HelperTimeSheet.Remove(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                //if (objMaster.VehicleID > 0 && (objMaster.VendorOfVehicleID == null || objMaster.VendorOfVehicleID == account.SYSCustomerID))
                //    HelperTimeSheet.Create(model, account, objMaster.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
            }
            return HelperTOMaster_Error.None;
        }

        public static HelperTOMaster_Error MONCO_AddHour(DataEntities model, AccountItem account, int id, int reasionid, string reasionnote, double houradd)
        {
            if (id > 0 && model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0).Count() > 0)
            {
                var objCON = model.OPS_COTOContainer.Where(c => c.ID == id && c.COTOMasterID > 0).Select(c => new { c.ID, COTOMasterID = c.COTOMasterID.Value }).FirstOrDefault();
                if (objCON == null)
                    throw FaultHelper.BusinessFault(null, null, "obj null");
                if (model.CAT_Reason.Where(c => c.ID == reasionid).Count() == 0)
                    return HelperTOMaster_Error.ReasionFail;

                return HelperTOMaster_Error.None;
            }
            else
                throw FaultHelper.BusinessFault(null, null, "id null");
        }

        public static HelperTOMaster_Error MONCO_ChangeDepot(DataEntities model, AccountItem account, int masterid, int opscontainerid, int cuslocationid, int reasionid, string reasionnote)
        {
            if (opscontainerid > 0 && model.OPS_Container.Where(c => c.ID == opscontainerid).Count() > 0)
            {
                var objCON = model.OPS_Container.Where(c => c.ID == opscontainerid).Select(c => new { c.ContainerID, c.LocationDepotID, c.LocationDepotReturnID, c.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).FirstOrDefault();
                var catLoc = model.CUS_Location.Where(c => c.ID == cuslocationid).Select(c => new { c.LocationID }).FirstOrDefault();
                if (objCON != null && catLoc != null && (objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport || objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport))
                {
                    if (objCON.LocationDepotID > 0)
                    {
                        if (model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.LocationFromID == objCON.LocationDepotID.Value && c.LocationToID == catLoc.LocationID).Count() > 0)
                            throw FaultHelper.BusinessFault(null, null, "locationfrom, locationto different");
                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.LocationFromID == objCON.LocationDepotID.Value))
                        {
                            tocontainer.LocationFromID = catLoc.LocationID;

                            var coto = model.OPS_COTO.FirstOrDefault(c => c.COTOMasterID == masterid && c.SortOrder == tocontainer.COTOSort);
                            if (coto != null)
                            {
                                coto.LocationFromID = catLoc.LocationID;
                            }
                            var coloc = model.OPS_COTOLocation.FirstOrDefault(c => c.COTOMasterID == masterid && c.SortOrder == tocontainer.COTOSort);
                            if (coloc != null)
                            {
                                coloc.LocationID = catLoc.LocationID;
                            }
                        }
                    }
                    if (objCON.LocationDepotReturnID > 0)
                    {
                        if (model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.LocationFromID == catLoc.LocationID && c.LocationToID == objCON.LocationDepotReturnID.Value).Count() > 0)
                            throw FaultHelper.BusinessFault(null, null, "locationfrom, locationto different");
                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.LocationToID == objCON.LocationDepotReturnID.Value))
                        {
                            tocontainer.LocationToID = catLoc.LocationID;

                            var coto = model.OPS_COTO.FirstOrDefault(c => c.COTOMasterID == masterid && c.SortOrder == tocontainer.COTOSort);
                            if (coto != null)
                            {
                                coto.LocationToID = catLoc.LocationID;
                            }
                            var coloc = model.OPS_COTOLocation.FirstOrDefault(c => c.COTOMasterID == masterid && c.SortOrder == tocontainer.COTOSort + 1);
                            if (coloc != null)
                            {
                                coloc.LocationID = catLoc.LocationID;
                            }
                        }
                    }

                    var ordContainer = model.ORD_Container.FirstOrDefault(c => c.ID == objCON.ContainerID);
                    if (ordContainer != null)
                    {
                        if (objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport)
                        {
                            ordContainer.LocationDepotID = cuslocationid;
                            ordContainer.LocationDepotReturnID = null;
                        }
                        else if (objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)
                        {
                            ordContainer.LocationDepotID = null;
                            ordContainer.LocationDepotReturnID = cuslocationid;
                        }
                        else if (objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal)
                        {
                            ordContainer.LocationDepotID = cuslocationid;
                            ordContainer.LocationDepotReturnID = cuslocationid;
                        }
                    }
                    model.SaveChanges();

                    //Change routing
                    var ordCONTemp = model.ORD_Container.Where(c => c.ID == objCON.ContainerID).Select(c => new { c.ORD_Order.CustomerID, c.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID, c.ORD_Order.ContractID, c.ORD_Order.ContractTermID, c.LocationFromID, c.LocationToID, c.LocationDepotID, c.LocationDepotReturnID }).FirstOrDefault();
                    if (ordCONTemp != null)
                    {
                        int? getemptyid = null;
                        int? returnemptyid = null;
                        if (ordCONTemp.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport)
                            getemptyid = ordCONTemp.LocationDepotID;
                        else if (ordCONTemp.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)
                            returnemptyid = ordCONTemp.LocationDepotReturnID;
                        else if (objCON.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal)
                        {
                            getemptyid = ordCONTemp.LocationDepotID;
                            returnemptyid = ordCONTemp.LocationDepotReturnID;
                        }
                        int routingid = HelperRouting.ORDOrder_CUSRouting_FindCO(model, account, ordCONTemp.CustomerID, ordCONTemp.ContractID, ordCONTemp.ContractTermID, ordCONTemp.LocationFromID.Value, ordCONTemp.LocationToID.Value, getemptyid, returnemptyid);

                        if (routingid > 0 && ordContainer != null)
                        {
                            ordContainer.CUSRoutingID = routingid;
                            model.SaveChanges();
                        }
                    }

                    return HelperTOMaster_Error.None;
                }
                else
                    throw FaultHelper.BusinessFault(null, null, "location or con null");
            }
            else
                throw FaultHelper.BusinessFault(null, null, "id null");
        }

        public static List<HelperTOMaster_Error> MONCO_ChangeLocationTime(DataEntities model, AccountItem account, int masterid, List<DTOMON_OPSTOLocation> lstChange)
        {
            var result = new List<HelperTOMaster_Error>();
            if (masterid > 0 && lstChange != null && lstChange.Count > 0)
            {
                //check 
                bool flag = true;
                DateTime? dt = null;
                DateTime? dtEstimate = null;
                var lstCheckID = model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid).Select(c => c.ID).ToList();
                foreach (var item in lstChange.OrderBy(c => c.SortOrder))
                {
                    if (lstCheckID.Contains(item.ID))
                    {
                        if (dt != null && item.DateCome != null && dt.Value.CompareTo(item.DateCome.Value) > 0)
                        {
                            flag = false;
                            dt = item.DateCome.Value;
                        }
                        else if (dt == null && item.DateCome != null)
                        {
                            dt = item.DateCome.Value;
                        }
                        else if (item.DateCome != null && item.DateLeave != null && item.DateCome.Value.CompareTo(item.DateLeave.Value) > 0)
                        {
                            flag = false;
                        }

                        if (dtEstimate != null && dtEstimate.Value.CompareTo(item.DateComeEstimate.Value) > 0)
                        {
                            flag = false;
                            dtEstimate = item.DateComeEstimate.Value;
                            result.Add(HelperTOMaster_Error.Fail);
                        }
                        else if (item.DateComeEstimate != null && item.DateLeaveEstimate != null && item.DateComeEstimate.Value.CompareTo(item.DateLeaveEstimate.Value) > 0)
                        {
                            flag = false;
                            result.Add(HelperTOMaster_Error.Fail);
                        }
                        else if (dt == null)
                        {
                            dtEstimate = item.DateComeEstimate.Value;
                        }
                    }
                    else
                    {
                        flag = false;
                        result.Add(HelperTOMaster_Error.Fail);
                    }
                }

                if (flag)
                {
                    foreach (var item in lstChange.OrderBy(c => c.SortOrder))
                    {
                        var toloc = model.OPS_COTOLocation.FirstOrDefault(c => c.ID == item.ID);
                        if (toloc != null)
                        {
                            if (item.DateCome != null && item.DateCome.Value.CompareTo(DateTime.MinValue) > 0)
                                toloc.DateCome = item.DateCome.Value;
                            else
                                toloc.DateCome = null;
                            if (item.DateLeave != null && item.DateLeave.Value.CompareTo(DateTime.MinValue) > 0)
                                toloc.DateLeave = item.DateLeave.Value;
                            else
                                toloc.DateLeave = null;
                            if (item.DateComeEstimate != null && item.DateComeEstimate.Value.CompareTo(DateTime.MinValue) > 0)
                                toloc.DateComeEstimate = item.DateComeEstimate.Value;
                            else
                                toloc.DateComeEstimate = null;
                            if (item.DateLeaveEstimate != null && item.DateLeaveEstimate.Value.CompareTo(DateTime.MinValue) > 0)
                                toloc.DateLeaveEstimate = item.DateLeaveEstimate.Value;
                            else
                                toloc.DateLeaveEstimate = null;
                        }
                    }
                    model.SaveChanges();
                }
            }
            return result;
        }

        public static HelperTOMaster_Status MONCO_ChangePlanCheck(DataEntities model, AccountItem account, int masterid, int masterchangeid, DateTime? etd = null)
        {
            if (model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerComplete && c.StatusOfCOContainerID != -(int)SYSVarType.StatusOfCOContainerReturnRomooc && c.StatusOfCOContainerID != -(int)SYSVarType.StatusOfCOContainerReturnStation).Count() == 0)
            {
                var itemCurrent = model.OPS_COTOMaster.Where(c => c.ID == masterid).Select(c => new { c.RomoocID, c.CAT_Vehicle.CurrentRomoocID }).FirstOrDefault();
                var itemChange = model.OPS_COTOMaster.Where(c => c.ID == masterchangeid).Select(c => new { c.RomoocID, c.CAT_Romooc.HasContainer }).FirstOrDefault();
                if (itemCurrent != null && itemChange != null && itemCurrent.RomoocID != itemChange.RomoocID && itemCurrent.CurrentRomoocID != null && itemChange.HasContainer == false)
                {
                    return HelperTOMaster_Status.MONCO_ChangePlanHasRomooc;
                }
            }

            return HelperTOMaster_Status.None;
        }

        public static HelperTOMaster_Error MONCO_ChangePlan(DataEntities model, AccountItem account, int masterid, int masterchangeid, bool changemooc = true, DateTime? etd = null)
        {
            var objMaster = model.OPS_COTOMaster.Where(c => c.ID == masterid && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived).Select(c => new { c.ID, c.VehicleID, c.VendorOfVehicleID, c.RomoocID, c.CAT_Vehicle.LocationID, c.CAT_Romooc.HasContainer }).FirstOrDefault();
            var objMasterChange = model.OPS_COTOMaster.Where(c => c.ID == masterchangeid && c.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterTendered).Select(c => new { c.ID, c.VehicleID, c.VendorOfVehicleID, c.ETD, c.ETA }).FirstOrDefault();
            var setting = OPSCO_GetSetting(model, account);
            if (objMaster != null && objMasterChange != null)
            {
                DateTime? eta = null;
                var objMasterNext = model.OPS_COTOMaster.Where(c => c.ID != objMaster.ID && c.ETD >= etd && c.VehicleID == objMaster.VehicleID &&
                    c.VendorOfVehicleID == objMaster.VendorOfVehicleID && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterReceived)
                    .Select(c => new
                    {
                        c.ETD
                    }).FirstOrDefault();
                if (objMasterNext != null)
                {
                    eta = objMasterNext.ETD;
                    eta = eta.Value.AddHours(-0.1);
                }
                else if (objMasterChange.ETA.CompareTo(etd.Value) > 0)
                {
                    eta = objMasterChange.ETA;
                    eta = eta.Value.AddHours(-0.1);
                }
                else
                {
                    eta = etd.Value.AddHours((objMasterChange.ETA - objMasterChange.ETD).TotalHours);
                }
                if (etd.Value.CompareTo(eta.Value) < 0)
                {
                    bool flag = false;
                    //Remove data time 
                    if (objMasterChange.VendorOfVehicleID == null || objMasterChange.VendorOfVehicleID == account.SYSCustomerID)
                    {
                        if (objMasterChange.VehicleID > 2)
                            HelperTimeSheet.Remove(model, account, objMasterChange.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                        flag = true;
                    }
                    else if (objMasterChange.VehicleID <= 2)
                        flag = true;
                    if (flag)
                    {
                        var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objMasterChange.ID);
                        if (obj != null)
                        {
                            obj.VehicleID = objMaster.VehicleID;
                            if (objMaster.HasContainer == false)
                            {
                                if (changemooc == false)
                                    obj.RomoocID = objMaster.RomoocID;
                            }
                            obj.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterDelivery;
                            obj.VendorOfVehicleID = account.SYSCustomerID;
                            obj.ETD = etd.Value;
                            obj.ETA = eta.Value;
                            model.SaveChanges();
                            HelperTimeSheet.Create(model, account, objMasterChange.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                        }

                        //check time change
                        var lastChange = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort > 0).Select(c => new { c.ID, c.ParentID, c.COTOSort, c.SortOrder, c.LocationFromID, c.LocationToID, c.OPS_COTOMaster.RomoocID, c.ETD, c.ETA, c.StatusOfCOContainerID, c.OPSContainerID, c.COTOMasterID }).OrderBy(c => c.COTOSort).FirstOrDefault();
                        if (lastChange != null)
                        {
                            var startChange = false;
                            if (objMaster.HasContainer == false)
                            {
                                //has mooc
                                if (changemooc == true)
                                {
                                    if (lastChange.LocationFromID != objMaster.LocationID)
                                    {
                                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort >= lastChange.COTOSort))
                                        {
                                            tocontainer.COTOSort++;
                                        }
                                        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                                        {
                                            coto.SortOrder++;
                                        }
                                        foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                                        {
                                            toloc.SortOrder++;
                                        }

                                        //add get mooc
                                        var objTOContainer = new OPS_COTOContainer()
                                        {
                                            CreatedBy = account.UserName,
                                            CreatedDate = DateTime.Now,

                                            ParentID = lastChange.ParentID,
                                            OPSContainerID = lastChange.OPSContainerID,
                                            IsSwap = false,
                                            IsInput = false,
                                            TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                            TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                            StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetRomooc,
                                            CreateByMasterID = lastChange.COTOMasterID,

                                            COTOMasterID = lastChange.COTOMasterID,
                                            LocationFromID = objMaster.LocationID.Value,
                                            LocationToID = lastChange.LocationFromID,
                                            ETD = lastChange.ETD.Value.AddHours(-0.1),
                                            ETA = lastChange.ETD.Value,
                                            ETDStart = lastChange.ETD.Value.AddHours(-0.1),
                                            ETAStart = lastChange.ETD.Value,
                                            SortOrder = lastChange.SortOrder - 1,
                                            COTOSort = lastChange.COTOSort,
                                            IsSplit = false,
                                            //ReasonChangeID = reasionid,
                                            //ReasonChangeNote = reasionnote
                                        };
                                        //detail.LocationFromID = setting.LocationRomoocReturnID;
                                        model.OPS_COTOContainer.Add(objTOContainer);

                                        //new coto
                                        var objTONew = new OPS_COTO();
                                        objTONew.CreatedBy = account.UserName;
                                        objTONew.CreatedDate = DateTime.Now;
                                        objTONew.COTOMasterID = masterchangeid;
                                        objTONew.IsOPS = true;
                                        objTONew.SortOrder = lastChange.COTOSort;
                                        objTONew.LocationFromID = objMaster.LocationID;
                                        objTONew.LocationToID = lastChange.LocationFromID;
                                        objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                                        model.OPS_COTO.Add(objTONew);

                                        //new location
                                        var objTOLocationNew = new OPS_COTOLocation();
                                        objTOLocationNew.LocationID = objMaster.LocationID;
                                        objTOLocationNew.SortOrder = lastChange.COTOSort;
                                        objTOLocationNew.COTOMasterID = masterchangeid;
                                        objTOLocationNew.CreatedBy = account.UserName;
                                        objTOLocationNew.CreatedDate = DateTime.Now;
                                        objTOLocationNew.DateComeEstimate = lastChange.ETD.Value.AddHours(-0.1);
                                        objTOLocationNew.DateLeaveEstimate = lastChange.ETD.Value.AddHours(-0.1);
                                        objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                                        objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                        model.OPS_COTOLocation.Add(objTOLocationNew);

                                        model.SaveChanges();
                                    }
                                    else
                                    {
                                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort == 1))
                                        {
                                            tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                                        }
                                        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder == 1))
                                        {
                                            coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                                        }
                                        foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder == 1))
                                        {
                                            toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                        }
                                        model.SaveChanges();
                                    }
                                }
                                else
                                {
                                    if (lastChange.LocationToID == objMaster.LocationID)
                                    {
                                        //remove ship empty
                                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort >= lastChange.COTOSort))
                                        {
                                            if (tocontainer.COTOSort == lastChange.COTOSort)
                                                model.OPS_COTOContainer.Remove(tocontainer);
                                            else
                                            {
                                                if (tocontainer.COTOSort == lastChange.COTOSort + 1)
                                                    tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                                                tocontainer.COTOSort--;
                                            }
                                        }
                                        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                                        {
                                            if (coto.SortOrder == lastChange.COTOSort)
                                                model.OPS_COTO.Remove(coto);
                                            else
                                            {
                                                if (coto.SortOrder == lastChange.COTOSort + 1)
                                                    coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                                                coto.SortOrder--;
                                            }
                                        }
                                        foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                                        {
                                            if (toloc.SortOrder == lastChange.COTOSort)
                                                model.OPS_COTOLocation.Remove(toloc);
                                            else
                                            {
                                                if (toloc.SortOrder == lastChange.COTOSort + 1)
                                                    toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                                toloc.SortOrder--;
                                            }
                                        }
                                        model.SaveChanges();
                                    }
                                    else
                                        startChange = true;
                                }
                            }
                            else
                                startChange = true;

                            if (startChange)
                            {
                                if (lastChange.LocationFromID != objMaster.LocationID)
                                {
                                    //add get empty
                                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort >= lastChange.COTOSort))
                                    {
                                        tocontainer.COTOSort++;
                                    }
                                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                                    {
                                        coto.SortOrder++;
                                    }
                                    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                                    {
                                        toloc.SortOrder++;
                                    }

                                    //add get mooc
                                    var objTOContainer = new OPS_COTOContainer()
                                    {
                                        CreatedBy = account.UserName,
                                        CreatedDate = DateTime.Now,

                                        ParentID = lastChange.ParentID,
                                        OPSContainerID = lastChange.OPSContainerID,
                                        IsSwap = false,
                                        IsInput = false,
                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                                        CreateByMasterID = lastChange.COTOMasterID,

                                        COTOMasterID = lastChange.COTOMasterID,
                                        LocationFromID = objMaster.LocationID.Value,
                                        LocationToID = lastChange.LocationFromID,
                                        ETD = lastChange.ETD.Value.AddHours(-0.1),
                                        ETA = lastChange.ETD.Value,
                                        ETDStart = lastChange.ETD.Value.AddHours(-0.1),
                                        ETAStart = lastChange.ETD.Value,
                                        SortOrder = lastChange.SortOrder - 1,
                                        COTOSort = lastChange.COTOSort,
                                        IsSplit = false,
                                        //ReasonChangeID = reasionid,
                                        //ReasonChangeNote = reasionnote 
                                    };
                                    //detail.LocationFromID = setting.LocationRomoocReturnID;
                                    model.OPS_COTOContainer.Add(objTOContainer);

                                    //new coto
                                    var objTONew = new OPS_COTO();
                                    objTONew.CreatedBy = account.UserName;
                                    objTONew.CreatedDate = DateTime.Now;
                                    objTONew.COTOMasterID = masterchangeid;
                                    objTONew.IsOPS = true;
                                    objTONew.SortOrder = lastChange.COTOSort;
                                    objTONew.LocationFromID = objMaster.LocationID;
                                    objTONew.LocationToID = lastChange.LocationFromID;
                                    objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                                    model.OPS_COTO.Add(objTONew);

                                    //new location
                                    var objTOLocationNew = new OPS_COTOLocation();
                                    objTOLocationNew.LocationID = objMaster.LocationID;
                                    objTOLocationNew.SortOrder = lastChange.COTOSort;
                                    objTOLocationNew.COTOMasterID = masterchangeid;
                                    objTOLocationNew.CreatedBy = account.UserName;
                                    objTOLocationNew.CreatedDate = DateTime.Now;
                                    objTOLocationNew.DateComeEstimate = lastChange.ETD.Value.AddHours(-0.1);
                                    objTOLocationNew.DateLeaveEstimate = lastChange.ETD.Value.AddHours(-0.1);
                                    objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                                    objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                    model.OPS_COTOLocation.Add(objTOLocationNew);

                                    model.SaveChanges();
                                }
                                else
                                {
                                    //change state
                                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort == 1))
                                    {
                                        tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                                    }
                                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder == 1))
                                    {
                                        coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                                    }
                                    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder == 1))
                                    {
                                        toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                    }
                                    model.SaveChanges();
                                }
                            }
                        }
                    }
                }

                //bool flag = false;
                ////Remove data time
                //if (objMasterChange.VendorOfVehicleID == null || objMasterChange.VendorOfVehicleID == account.SYSCustomerID)
                //{
                //    if (objMasterChange.VehicleID > 2)
                //        HelperTimeSheet.Remove(model, account, objMasterChange.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                //    flag = true;
                //}
                //else if (objMasterChange.VehicleID <= 2)
                //    flag = true;
                //if (flag)
                //{
                //    var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objMasterChange.ID);
                //    if (obj != null)
                //    {
                //        obj.VehicleID = objMaster.VehicleID;
                //        if (objMaster.HasContainer == false)
                //        {
                //            if (changemooc == false)
                //                obj.RomoocID = objMaster.RomoocID;
                //        }
                //        obj.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterDelivery;
                //        obj.VendorOfVehicleID = account.SYSCustomerID;
                //        model.SaveChanges();
                //        HelperTimeSheet.Create(model, account, objMasterChange.ID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                //    }

                //    var lastChange = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort > 0).Select(c => new { c.ID, c.ParentID, c.COTOSort, c.SortOrder, c.LocationFromID, c.LocationToID, c.OPS_COTOMaster.RomoocID, c.ETD, c.ETA, c.StatusOfCOContainerID, c.OPSContainerID, c.COTOMasterID }).OrderBy(c => c.COTOSort).FirstOrDefault();
                //    if (lastChange != null)
                //    {
                //        var startChange = false;
                //        if (objMaster.HasContainer == false)
                //        {
                //            //has mooc
                //            if (changemooc == true)
                //            {
                //                if (lastChange.LocationFromID != objMaster.LocationID)
                //                {
                //                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort >= lastChange.COTOSort))
                //                    {
                //                        tocontainer.COTOSort++;
                //                    }
                //                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                //                    {
                //                        coto.SortOrder++;
                //                    }
                //                    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                //                    {
                //                        toloc.SortOrder++;
                //                    }

                //                    //add get mooc
                //                    var objTOContainer = new OPS_COTOContainer()
                //                    {
                //                        CreatedBy = account.UserName,
                //                        CreatedDate = DateTime.Now,

                //                        ParentID = lastChange.ParentID,
                //                        OPSContainerID = lastChange.OPSContainerID,
                //                        IsSwap = false,
                //                        IsInput = false,
                //                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                //                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                //                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerGetRomooc,
                //                        CreateByMasterID = lastChange.COTOMasterID,

                //                        COTOMasterID = lastChange.COTOMasterID,
                //                        LocationFromID = objMaster.LocationID.Value,
                //                        LocationToID = lastChange.LocationFromID,
                //                        ETD = lastChange.ETD.Value.AddHours(-0.1),
                //                        ETA = lastChange.ETD.Value,
                //                        ETDStart = lastChange.ETD.Value.AddHours(-0.1),
                //                        ETAStart = lastChange.ETD.Value,
                //                        SortOrder = lastChange.SortOrder - 1,
                //                        COTOSort = lastChange.COTOSort,
                //                        IsSplit = false,
                //                        //ReasonChangeID = reasionid,
                //                        //ReasonChangeNote = reasionnote
                //                    };
                //                    //detail.LocationFromID = setting.LocationRomoocReturnID;
                //                    model.OPS_COTOContainer.Add(objTOContainer);

                //                    //new coto
                //                    var objTONew = new OPS_COTO();
                //                    objTONew.CreatedBy = account.UserName;
                //                    objTONew.CreatedDate = DateTime.Now;
                //                    objTONew.COTOMasterID = masterchangeid;
                //                    objTONew.IsOPS = true;
                //                    objTONew.SortOrder = lastChange.COTOSort;
                //                    objTONew.LocationFromID = objMaster.LocationID;
                //                    objTONew.LocationToID = lastChange.LocationFromID;
                //                    objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                //                    model.OPS_COTO.Add(objTONew);

                //                    //new location
                //                    var objTOLocationNew = new OPS_COTOLocation();
                //                    objTOLocationNew.LocationID = objMaster.LocationID;
                //                    objTOLocationNew.SortOrder = lastChange.COTOSort;
                //                    objTOLocationNew.COTOMasterID = masterchangeid;
                //                    objTOLocationNew.CreatedBy = account.UserName;
                //                    objTOLocationNew.CreatedDate = DateTime.Now;
                //                    objTOLocationNew.DateComeEstimate = lastChange.ETD.Value.AddHours(-0.1);
                //                    objTOLocationNew.DateLeaveEstimate = lastChange.ETD.Value.AddHours(-0.1);
                //                    objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                //                    objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                //                    model.OPS_COTOLocation.Add(objTOLocationNew);

                //                    model.SaveChanges();
                //                }
                //                else
                //                {
                //                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort == 1))
                //                    {
                //                        tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                //                    }
                //                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder == 1))
                //                    {
                //                        coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                //                    }
                //                    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder == 1))
                //                    {
                //                        toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                //                    }
                //                    model.SaveChanges();
                //                }
                //            }
                //            else
                //            {
                //                if (lastChange.LocationToID == objMaster.LocationID)
                //                {
                //                    //remove ship empty
                //                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort >= lastChange.COTOSort))
                //                    {
                //                        if (tocontainer.COTOSort == lastChange.COTOSort)
                //                            model.OPS_COTOContainer.Remove(tocontainer);
                //                        else
                //                        {
                //                            if (tocontainer.COTOSort == lastChange.COTOSort + 1)
                //                                tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                //                            tocontainer.COTOSort--;
                //                        }
                //                    }
                //                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                //                    {
                //                        if (coto.SortOrder == lastChange.COTOSort)
                //                            model.OPS_COTO.Remove(coto);
                //                        else
                //                        {
                //                            if (coto.SortOrder == lastChange.COTOSort + 1)
                //                                coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                //                            coto.SortOrder--;
                //                        }
                //                    }
                //                    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                //                    {
                //                        if (toloc.SortOrder == lastChange.COTOSort)
                //                            model.OPS_COTOLocation.Remove(toloc);
                //                        else
                //                        {
                //                            if (toloc.SortOrder == lastChange.COTOSort + 1)
                //                                toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                //                            toloc.SortOrder--;
                //                        }
                //                    }
                //                    model.SaveChanges();
                //                }
                //                else
                //                    startChange = true;
                //            }
                //        }
                //        else
                //            startChange = true;

                //        if (startChange)
                //        {
                //            if (lastChange.LocationFromID != objMaster.LocationID)
                //            {
                //                //add get empty
                //                foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort >= lastChange.COTOSort))
                //                {
                //                    tocontainer.COTOSort++;
                //                }
                //                foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                //                {
                //                    coto.SortOrder++;
                //                }
                //                foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder >= lastChange.COTOSort))
                //                {
                //                    toloc.SortOrder++;
                //                }

                //                //add get mooc
                //                var objTOContainer = new OPS_COTOContainer()
                //                {
                //                    CreatedBy = account.UserName,
                //                    CreatedDate = DateTime.Now,

                //                    ParentID = lastChange.ParentID,
                //                    OPSContainerID = lastChange.OPSContainerID,
                //                    IsSwap = false,
                //                    IsInput = false,
                //                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                //                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                //                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                //                    CreateByMasterID = lastChange.COTOMasterID,

                //                    COTOMasterID = lastChange.COTOMasterID,
                //                    LocationFromID = objMaster.LocationID.Value,
                //                    LocationToID = lastChange.LocationFromID,
                //                    ETD = lastChange.ETD.Value.AddHours(-0.1),
                //                    ETA = lastChange.ETD.Value,
                //                    ETDStart = lastChange.ETD.Value.AddHours(-0.1),
                //                    ETAStart = lastChange.ETD.Value,
                //                    SortOrder = lastChange.SortOrder - 1,
                //                    COTOSort = lastChange.COTOSort,
                //                    IsSplit = false,
                //                    //ReasonChangeID = reasionid,
                //                    //ReasonChangeNote = reasionnote 
                //                };
                //                //detail.LocationFromID = setting.LocationRomoocReturnID;
                //                model.OPS_COTOContainer.Add(objTOContainer);

                //                //new coto
                //                var objTONew = new OPS_COTO();
                //                objTONew.CreatedBy = account.UserName;
                //                objTONew.CreatedDate = DateTime.Now;
                //                objTONew.COTOMasterID = masterchangeid;
                //                objTONew.IsOPS = true;
                //                objTONew.SortOrder = lastChange.COTOSort;
                //                objTONew.LocationFromID = objMaster.LocationID;
                //                objTONew.LocationToID = lastChange.LocationFromID;
                //                objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                //                model.OPS_COTO.Add(objTONew);

                //                //new location
                //                var objTOLocationNew = new OPS_COTOLocation();
                //                objTOLocationNew.LocationID = objMaster.LocationID;
                //                objTOLocationNew.SortOrder = lastChange.COTOSort;
                //                objTOLocationNew.COTOMasterID = masterchangeid;
                //                objTOLocationNew.CreatedBy = account.UserName;
                //                objTOLocationNew.CreatedDate = DateTime.Now;
                //                objTOLocationNew.DateComeEstimate = lastChange.ETD.Value.AddHours(-0.1);
                //                objTOLocationNew.DateLeaveEstimate = lastChange.ETD.Value.AddHours(-0.1);
                //                objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                //                objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                //                model.OPS_COTOLocation.Add(objTOLocationNew);

                //                model.SaveChanges();
                //            }
                //            else
                //            {
                //                //change state
                //                foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterchangeid && c.IsSplit == false && c.COTOSort == 1))
                //                {
                //                    tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                //                }
                //                foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder == 1))
                //                {
                //                    coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                //                }
                //                foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterchangeid && c.SortOrder == 1))
                //                {
                //                    toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                //                }
                //                model.SaveChanges();
                //            }
                //        }
                //    }
                //}

                return HelperTOMaster_Error.None;
            }
            else
                throw FaultHelper.BusinessFault(null, null, "id null");
        }

        public static int MONCO_Continue(DataEntities model, AccountItem account, int opscontainerid, int masterid)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.ID == opscontainerid && c.IsSplit == false)
                    .Select(c => new { c.ID, c.OPS_COTOMaster.VehicleID, c.COTOMasterID, c.OPSContainerID, c.LocationToID, c.SortOrder, c.COTOSort, c.TypeOfStatusContainerID }).FirstOrDefault();
            if (objCON != null)
            {
                var lastReason = model.OPS_COTOContainer.Where(c => c.OPS_COTOMaster.VehicleID == objCON.VehicleID && (c.OPS_COTOMaster.VendorOfVehicleID == null || c.OPS_COTOMaster.VendorOfVehicleID == account.SYSCustomerID) && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.ReasonChangeID > 0 && c.ModifiedDate != null).Select(c => new { c.ReasonChangeID, c.ModifiedDate }).OrderByDescending(c => c.ModifiedDate).FirstOrDefault();
                if (lastReason != null)
                {
                    var locContinue = model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid).Select(c => new { c.ID, c.COTOMasterID, c.OPS_COTOMaster.RomoocID, c.DateComeEstimate, c.LocationID, c.SortOrder }).OrderBy(c => c.SortOrder).FirstOrDefault();
                    if (locContinue != null)
                    {
                        var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == objCON.VehicleID);
                        if (objVehicle != null)
                        {
                            objVehicle.CurrentRomoocID = locContinue.RomoocID;
                            objVehicle.ModifiedBy = account.UserName;
                            objVehicle.ModifiedDate = DateTime.Now;
                        }
                        var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == masterid);
                        if (objMaster != null)
                        {
                            objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterDelivery;
                            objMaster.ModifiedBy = account.UserName;
                            objMaster.ModifiedDate = DateTime.Now;
                        }

                        if (locContinue.LocationID != objCON.LocationToID)
                        {
                            foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false))
                            {
                                if (tocontainer.COTOSort == locContinue.SortOrder)
                                {
                                    var objTOContainer = new OPS_COTOContainer()
                                    {
                                        CreatedBy = account.UserName,
                                        CreatedDate = DateTime.Now,

                                        ParentID = tocontainer.ParentID,
                                        OPSContainerID = tocontainer.OPSContainerID,
                                        IsSwap = false,
                                        IsInput = false,
                                        TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                        TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                        StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                                        CreateByMasterID = tocontainer.COTOMasterID,

                                        COTOMasterID = tocontainer.COTOMasterID,
                                        LocationFromID = objCON.LocationToID,
                                        LocationToID = tocontainer.LocationFromID,
                                        ETD = tocontainer.ETD.Value.AddHours(-0.1),
                                        ETA = tocontainer.ETD.Value,
                                        ETDStart = tocontainer.ETD.Value.AddHours(-0.1),
                                        ETAStart = tocontainer.ETD.Value,
                                        SortOrder = tocontainer.SortOrder - 1,
                                        COTOSort = tocontainer.COTOSort,
                                        IsSplit = false,
                                        //ReasonChangeID = reasionid, 
                                        //ReasonChangeNote = reasionnote 
                                    };
                                    model.OPS_COTOContainer.Add(objTOContainer);
                                }
                                tocontainer.COTOSort++;
                            }

                            foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterid))
                            {
                                coto.SortOrder++;
                                coto.ModifiedBy = account.UserName;
                                coto.ModifiedDate = DateTime.Now;
                            }
                            foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid))
                            {
                                toloc.SortOrder++;
                                toloc.ModifiedBy = account.UserName;
                                toloc.ModifiedDate = DateTime.Now;
                            }

                            //new coto
                            var objTONew = new OPS_COTO();
                            objTONew.CreatedBy = account.UserName;
                            objTONew.CreatedDate = DateTime.Now;
                            objTONew.COTOMasterID = locContinue.COTOMasterID;
                            objTONew.IsOPS = true;
                            objTONew.SortOrder = locContinue.SortOrder;
                            objTONew.LocationFromID = objCON.LocationToID;
                            objTONew.LocationToID = locContinue.LocationID;
                            objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                            model.OPS_COTO.Add(objTONew);

                            //new location
                            var objTOLocationNew = new OPS_COTOLocation();
                            objTOLocationNew.LocationID = objCON.LocationToID;
                            objTOLocationNew.SortOrder = locContinue.SortOrder;
                            objTOLocationNew.COTOMasterID = locContinue.COTOMasterID;
                            objTOLocationNew.CreatedBy = account.UserName;
                            objTOLocationNew.CreatedDate = DateTime.Now;
                            objTOLocationNew.DateComeEstimate = locContinue.DateComeEstimate.Value.AddHours(-0.1);
                            objTOLocationNew.DateLeaveEstimate = locContinue.DateComeEstimate.Value.AddHours(-0.1);
                            objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                            objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                            model.OPS_COTOLocation.Add(objTOLocationNew);

                            model.SaveChanges();
                        }
                        else
                        {
                            foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.IsSplit == false))
                            {
                                if (tocontainer.COTOSort == locContinue.SortOrder)
                                {
                                    tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                                    tocontainer.ModifiedBy = account.UserName;
                                    tocontainer.ModifiedDate = DateTime.Now;
                                }
                            }
                            foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterid))
                            {
                                if (coto.SortOrder == locContinue.SortOrder)
                                {
                                    coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                                    coto.ModifiedBy = account.UserName;
                                    coto.ModifiedDate = DateTime.Now;
                                }
                            }
                            foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == masterid))
                            {
                                if (toloc.SortOrder == locContinue.SortOrder)
                                {
                                    toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                    toloc.ModifiedBy = account.UserName;
                                    toloc.ModifiedDate = DateTime.Now;
                                }
                            }

                            model.SaveChanges();
                        }

                        return masterid;
                    }
                }
            }
            return -1;
        }

        public static HelperTOMaster_Error MONCO_End(DataEntities model, AccountItem account, int opscontainerid, int? locationid = null)
        {
            var objCON = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.ID == opscontainerid && c.IsSplit == false)
                    .Select(c => new { c.OPS_COTOMaster.VehicleID, c.OPS_COTOMaster.RomoocID, COTOMasterID = c.COTOMasterID.Value, c.OPSContainerID, c.LocationToID, c.SortOrder, c.COTOSort, c.TypeOfStatusContainerID }).FirstOrDefault();
            if (objCON != null)
            {
                DateTime? eta = null;
                if (locationid > 0)
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                    {
                        if (tocontainer.COTOSort == objCON.COTOSort + 1 && tocontainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerReturnStation)
                        {
                            if (tocontainer.LocationToID != locationid)
                                tocontainer.LocationToID = locationid.Value;
                        }
                        tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                        tocontainer.ModifiedBy = account.UserName;
                        tocontainer.ModifiedDate = DateTime.Now;

                        eta = eta == null || tocontainer.ETA > eta ? tocontainer.ETA : null;
                    }
                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                    {
                        if (coto.SortOrder == objCON.COTOSort + 1)
                        {
                            if (coto.LocationToID != locationid)
                                coto.LocationToID = locationid.Value;
                        }
                        coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                        coto.ModifiedBy = account.UserName;
                        coto.ModifiedDate = DateTime.Now;
                    }
                    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                    {
                        if (toloc.SortOrder == objCON.COTOSort + 1)
                        {
                            if (toloc.LocationID != locationid)
                                toloc.LocationID = locationid.Value;
                        }
                        toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                        toloc.ModifiedBy = account.UserName;
                        toloc.ModifiedDate = DateTime.Now;
                    }
                }
                else
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.IsSplit == false))
                    {
                        if (tocontainer.COTOSort > objCON.COTOSort)
                            model.OPS_COTOContainer.Remove(tocontainer);
                        else
                            tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                        eta = eta == null || tocontainer.ETA > eta ? tocontainer.ETA : null;
                    }
                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                    {
                        if (coto.SortOrder > objCON.COTOSort)
                            model.OPS_COTO.Remove(coto);
                        else
                            coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                    }
                    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCON.COTOMasterID))
                    {
                        if (toloc.SortOrder > objCON.COTOSort + 1)
                            model.OPS_COTOLocation.Remove(toloc);
                        else
                            toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                    }
                }

                var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objCON.COTOMasterID);
                if (objMaster != null)
                {
                    objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterReceived;
                    objMaster.ModifiedBy = account.UserName;
                    objMaster.ModifiedDate = DateTime.Now;

                    objMaster.ATD = objMaster.ETD;
                    objMaster.ATA = objMaster.ATA;
                }
                var objVehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == objCON.VehicleID);
                if (objVehicle != null)
                {
                    objVehicle.LocationID = objCON.LocationToID;
                    objVehicle.CurrentRomoocID = objCON.RomoocID;
                    objVehicle.ModifiedBy = account.UserName;
                    objVehicle.ModifiedDate = DateTime.Now;
                }
                var objRomooc = model.CAT_Romooc.FirstOrDefault(c => c.ID == objCON.RomoocID);
                if (objRomooc != null)
                {
                    objRomooc.LocationID = objCON.LocationToID;
                    objRomooc.HasContainer = false;
                    objRomooc.ModifiedBy = account.UserName;
                    objRomooc.ModifiedDate = DateTime.Now;
                }

                model.SaveChanges();

                MONCO_CompleteParent(model, account, new List<int>() { objCON.COTOMasterID });
                HelperTimeSheet.Create(model, account, objCON.COTOMasterID, SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                HelperStatus.OPSCOMaster_Status(model, account, new List<int> { objCON.COTOMasterID });

                return HelperTOMaster_Error.None;
            }
            return HelperTOMaster_Error.Fail;
        }

        public static List<HelperTOMaster_Error> MONCO_OrderLocal(DataEntities model, AccountItem account, int opscontainerid, List<int> lstORDContainerID)
        {
            var result = new List<HelperTOMaster_Error>();
            var objCON = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.ID == opscontainerid && c.IsSplit == false)
                    .Select(c => new { c.COTOMasterID, c.StatusOfCOContainerID, c.COTOSort }).FirstOrDefault();
            if (objCON != null && lstORDContainerID.Count > 0)
            {
                var lstStatusID = new List<int>()
                {
                    -(int)SYSVarType.StatusOfCOContainerEXEmpty,
                    -(int)SYSVarType.StatusOfCOContainerIMEmpty,
                    -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                };

                var lstServiceOfOrder = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOSort >= objCON.COTOSort && c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderLocal).Select(c => new { c.OPSContainerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).Distinct().ToList();
                Dictionary<int, int> dic = new Dictionary<int, int>();
                foreach (var item in lstServiceOfOrder)
                {
                    var objStatus = model.OPS_COTOContainer.Where(c => c.OPSContainerID == item.OPSContainerID && c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerWait && lstStatusID.Contains(c.StatusOfCOContainerID) && c.IsSplit == false).OrderByDescending(c => c.COTOSort).Select(c => new { c.ID }).FirstOrDefault();
                    if (objStatus != null && lstORDContainerID.Count > dic.Count)
                        dic.Add(objStatus.ID, lstORDContainerID[dic.Count]);
                }
                int statusEmpty = -(int)SYSVarType.StatusOfCOContainerShipEmpty;
                if (lstServiceOfOrder.Where(c => c.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport).Count() > 0)
                    statusEmpty = -(int)SYSVarType.StatusOfCOContainerReturnEmpty;
                if (lstORDContainerID.Count == 1)
                {
                    foreach (var val in dic)
                    {
                        var objCurrent = model.OPS_COTOContainer.Where(c => c.ID == val.Key).FirstOrDefault();
                        var objLocal = model.OPS_COTOContainer.Where(c => c.OPS_Container.ContainerID == val.Value && c.ParentID == null && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).FirstOrDefault();
                        if (objCurrent != null && objLocal != null && objCurrent.ETD.Value.CompareTo(objCurrent.ETA.Value) < 0)
                        {
                            foreach (var item in model.OPS_COTOContainer.Where(c => c.OPSContainerID == objLocal.OPSContainerID && c.ParentID == null && c.ID != objLocal.ID))
                                model.OPS_COTOContainer.Remove(item);
                            model.SaveChanges();

                            double totalHours = (objCurrent.ETA.Value - objCurrent.ETD.Value).TotalHours;
                            double divHour = totalHours;
                            if (objCurrent.LocationFromID != objLocal.LocationFromID && objCurrent.LocationToID != objLocal.LocationToID)
                                divHour = totalHours / 3;
                            else if (objCurrent.LocationFromID != objLocal.LocationFromID || objCurrent.LocationToID != objLocal.LocationToID)
                                divHour = totalHours / 2;
                            int cotoSort = objCurrent.COTOSort;
                            int add = 0;
                            int fromid = objLocal.LocationFromID;
                            var lstAdd = new List<OPS_COTOContainer>();
                            if (objCurrent.LocationFromID != fromid)
                            {
                                var objTOContainerCurrent = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objCurrent.ID,
                                    OPSContainerID = objCurrent.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = objCurrent.LocationFromID,
                                    LocationToID = fromid,
                                    ETD = objCurrent.ETD.Value,
                                    ETA = objCurrent.ETD.Value.AddHours(divHour),
                                    ETDStart = objCurrent.ETD.Value,
                                    ETAStart = objCurrent.ETD.Value.AddHours(divHour),
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                lstAdd.Add(objTOContainerCurrent);
                                model.OPS_COTOContainer.Add(objTOContainerCurrent);

                                var objTOContainerLocal = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objLocal.ID,
                                    OPSContainerID = objLocal.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = statusEmpty,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = objCurrent.LocationFromID,
                                    LocationToID = fromid,
                                    ETD = objCurrent.ETD.Value,
                                    ETA = objCurrent.ETD.Value.AddHours(divHour),
                                    ETDStart = objCurrent.ETD.Value,
                                    ETAStart = objCurrent.ETD.Value.AddHours(divHour),
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                model.OPS_COTOContainer.Add(objTOContainerLocal);

                                add++;
                            }

                            if (objCurrent.LocationToID != objLocal.LocationToID)
                            {
                                var objTOContainerCurrent = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objCurrent.ID,
                                    OPSContainerID = objCurrent.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = fromid,
                                    LocationToID = objLocal.LocationToID,
                                    ETD = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETA = objCurrent.ETD.Value.AddHours((add + 1) * divHour),
                                    ETDStart = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETAStart = objCurrent.ETD.Value.AddHours((add + 1) * divHour),
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                lstAdd.Add(objTOContainerCurrent);
                                model.OPS_COTOContainer.Add(objTOContainerCurrent);

                                var objTOContainerLocal = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objLocal.ID,
                                    OPSContainerID = objLocal.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipLaden,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = fromid,
                                    LocationToID = objLocal.LocationToID,
                                    ETD = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETA = objCurrent.ETD.Value.AddHours((add + 1) * divHour),
                                    ETDStart = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETAStart = objCurrent.ETD.Value.AddHours((add + 1) * divHour),
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                model.OPS_COTOContainer.Add(objTOContainerLocal);

                                add++;

                                objTOContainerCurrent = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objCurrent.ID,
                                    OPSContainerID = objCurrent.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = objLocal.LocationToID,
                                    LocationToID = objCurrent.LocationToID,
                                    ETD = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETA = objCurrent.ETA.Value,
                                    ETDStart = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETAStart = objCurrent.ETA.Value,
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                lstAdd.Add(objTOContainerCurrent);
                                model.OPS_COTOContainer.Add(objTOContainerCurrent);

                                add++;
                            }
                            else
                            {
                                var objTOContainerCurrent = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objCurrent.ID,
                                    OPSContainerID = objCurrent.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = statusEmpty,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = fromid,
                                    LocationToID = objLocal.LocationToID,
                                    ETD = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETA = objCurrent.ETA.Value,
                                    ETDStart = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETAStart = objCurrent.ETA.Value,
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                lstAdd.Add(objTOContainerCurrent);
                                model.OPS_COTOContainer.Add(objTOContainerCurrent);

                                var objTOContainerLocal = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objLocal.ID,
                                    OPSContainerID = objLocal.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipLaden,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = fromid,
                                    LocationToID = objLocal.LocationToID,
                                    ETD = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETA = objCurrent.ETA.Value,
                                    ETDStart = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETAStart = objCurrent.ETA.Value,
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                model.OPS_COTOContainer.Add(objTOContainerLocal);

                                add++;
                            }

                            objCurrent.IsSplit = true;
                            objLocal.IsSplit = true;

                            if (add > 1)
                            {
                                var first = lstAdd[0];
                                var locationid = first.LocationToID;
                                foreach (var tocon in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                {
                                    if (tocon.COTOSort < objCurrent.COTOSort)
                                        tocon.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                                    if (tocon.COTOSort == objCurrent.COTOSort)
                                    {
                                        tocon.LocationToID = first.LocationToID;
                                        tocon.ETD = first.ETD;
                                        tocon.ETA = first.ETA;
                                        tocon.ETDStart = first.ETD;
                                        tocon.ETAStart = first.ETA;
                                    }
                                    if (tocon.COTOSort > objCurrent.COTOSort)
                                        tocon.COTOSort += (add - 1);
                                }
                                foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                {
                                    if (coto.SortOrder < objCurrent.COTOSort)
                                        coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                                    if (coto.SortOrder == objCurrent.COTOSort)
                                        coto.LocationToID = locationid;
                                    if (coto.SortOrder > objCurrent.COTOSort)
                                        coto.SortOrder += (add - 1);
                                }
                                foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                {
                                    if (toloc.SortOrder <= objCurrent.COTOSort)
                                        toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                    if (toloc.SortOrder > objCurrent.COTOSort)
                                        toloc.SortOrder += (add - 1);
                                }

                                for (int i = 1; i < lstAdd.Count; i++)
                                {
                                    var item = lstAdd[i];
                                    locationid = item.LocationFromID;

                                    //new location 
                                    var objTOLocationNew = new OPS_COTOLocation();
                                    objTOLocationNew.LocationID = locationid;
                                    objTOLocationNew.SortOrder = item.COTOSort;
                                    objTOLocationNew.COTOMasterID = item.COTOMasterID.Value;
                                    objTOLocationNew.CreatedBy = account.UserName;
                                    objTOLocationNew.CreatedDate = DateTime.Now;
                                    objTOLocationNew.DateComeEstimate = item.ETD.Value;
                                    objTOLocationNew.DateLeaveEstimate = item.ETD.Value;
                                    objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                                    objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                    model.OPS_COTOLocation.Add(objTOLocationNew);

                                    //new coto
                                    var objTONew = new OPS_COTO();
                                    objTONew.CreatedBy = account.UserName;
                                    objTONew.CreatedDate = DateTime.Now;
                                    objTONew.COTOMasterID = item.COTOMasterID.Value;
                                    objTONew.IsOPS = true;
                                    objTONew.SortOrder = item.COTOSort;
                                    objTONew.LocationFromID = item.LocationFromID;
                                    objTONew.LocationToID = item.LocationToID;
                                    objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                                    model.OPS_COTO.Add(objTONew);
                                }
                            }

                            model.SaveChanges();

                        }
                    }

                    var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objCON.COTOMasterID);
                    if (objMaster != null)
                    {
                        objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterDelivery;
                        objMaster.ModifiedBy = account.UserName;
                        objMaster.ModifiedDate = DateTime.Now;
                        model.SaveChanges();
                    }
                }
            }

            return result;
        }

        public static List<HelperTOMaster_Error> MONCO_OrderLocalByMaster(DataEntities model, AccountItem account, int opscontainerid, List<int> lstMasterID)
        {
            var result = new List<HelperTOMaster_Error>();
            var objCON = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.ID == opscontainerid && c.IsSplit == false)
                    .Select(c => new { c.COTOMasterID, c.StatusOfCOContainerID, c.COTOSort }).FirstOrDefault();
            var lstORDContainerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && lstMasterID.Contains(c.COTOMasterID.Value) && c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal).Select(c => c.OPS_Container.ContainerID).Distinct().ToList();
            if (objCON != null && lstORDContainerID.Count > 0)
            {
                var lstStatusID = new List<int>()
                {
                    -(int)SYSVarType.StatusOfCOContainerEXEmpty,
                    -(int)SYSVarType.StatusOfCOContainerIMEmpty,
                    -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                };

                var lstServiceOfOrder = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID && c.COTOSort >= objCON.COTOSort && c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID != -(int)SYSVarType.ServiceOfOrderLocal).Select(c => new { c.OPSContainerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).Distinct().ToList();
                Dictionary<int, int> dic = new Dictionary<int, int>();
                foreach (var item in lstServiceOfOrder)
                {
                    var objStatus = model.OPS_COTOContainer.Where(c => c.OPSContainerID == item.OPSContainerID && lstStatusID.Contains(c.StatusOfCOContainerID) && c.IsSplit == false).OrderByDescending(c => c.COTOSort).Select(c => new { c.ID }).FirstOrDefault();
                    if (objStatus != null && lstORDContainerID.Count > dic.Count)
                        dic.Add(objStatus.ID, lstORDContainerID[dic.Count]);
                }
                if (lstORDContainerID.Count == 1)
                {
                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && lstMasterID.Contains(c.COTOMasterID.Value)))
                    {
                        if (tocontainer.ParentID > 0)
                            model.OPS_COTOContainer.Remove(tocontainer);
                        else
                            tocontainer.COTOMasterID = null;
                    }
                    foreach (var toloc in model.OPS_COTOLocation.Where(c => lstMasterID.Contains(c.COTOMasterID)))
                    {
                        model.OPS_COTOLocation.Remove(toloc);
                    }
                    foreach (var coto in model.OPS_COTO.Where(c => lstMasterID.Contains(c.COTOMasterID)))
                    {
                        model.OPS_COTO.Remove(coto);
                    }
                    foreach (var torate in model.OPS_COTORate.Where(c => lstMasterID.Contains(c.COTOMasterID)))
                    {
                        model.OPS_COTORate.Remove(torate);
                    }
                    foreach (var tomaster in model.OPS_COTOMaster.Where(c => lstMasterID.Contains(c.ID)))
                    {
                        model.OPS_COTOMaster.Remove(tomaster);
                    }
                    model.SaveChanges();

                    foreach (var val in dic)
                    {
                        var objCurrent = model.OPS_COTOContainer.Where(c => c.ID == val.Key).FirstOrDefault();
                        var objLocal = model.OPS_COTOContainer.Where(c => c.OPS_Container.ContainerID == val.Value && c.ParentID == null && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).FirstOrDefault();
                        if (objCurrent != null && objLocal != null && objCurrent.ETD.Value.CompareTo(objCurrent.ETA.Value) < 0)
                        {
                            foreach (var item in model.OPS_COTOContainer.Where(c => c.OPSContainerID == objLocal.OPSContainerID && c.ParentID == null && c.ID != objLocal.ID))
                                model.OPS_COTOContainer.Remove(item);
                            model.SaveChanges();

                            double totalHours = (objCurrent.ETA.Value - objCurrent.ETD.Value).TotalHours;
                            double divHour = totalHours;
                            if (objCurrent.LocationFromID != objLocal.LocationFromID && objCurrent.LocationToID != objLocal.LocationToID)
                                divHour = totalHours / 3;
                            else if (objCurrent.LocationFromID != objLocal.LocationFromID || objCurrent.LocationToID != objLocal.LocationToID)
                                divHour = totalHours / 2;
                            int cotoSort = objCurrent.COTOSort;
                            int add = 0;
                            int fromid = objLocal.LocationFromID;
                            var lstAdd = new List<OPS_COTOContainer>();
                            if (objCurrent.LocationFromID != fromid)
                            {
                                var objTOContainerCurrent = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objCurrent.ID,
                                    OPSContainerID = objCurrent.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = objCurrent.LocationFromID,
                                    LocationToID = fromid,
                                    ETD = objCurrent.ETD.Value,
                                    ETA = objCurrent.ETD.Value.AddHours(divHour),
                                    ETDStart = objCurrent.ETD.Value,
                                    ETAStart = objCurrent.ETD.Value.AddHours(divHour),
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                lstAdd.Add(objTOContainerCurrent);
                                model.OPS_COTOContainer.Add(objTOContainerCurrent);

                                var objTOContainerLocal = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objLocal.ID,
                                    OPSContainerID = objLocal.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = objCurrent.LocationFromID,
                                    LocationToID = fromid,
                                    ETD = objCurrent.ETD.Value,
                                    ETA = objCurrent.ETD.Value.AddHours(divHour),
                                    ETDStart = objCurrent.ETD.Value,
                                    ETAStart = objCurrent.ETD.Value.AddHours(divHour),
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                model.OPS_COTOContainer.Add(objTOContainerLocal);

                                add++;
                            }

                            if (objCurrent.LocationToID != objLocal.LocationToID)
                            {
                                var objTOContainerCurrent = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objCurrent.ID,
                                    OPSContainerID = objCurrent.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerStop,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = fromid,
                                    LocationToID = objLocal.LocationToID,
                                    ETD = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETA = objCurrent.ETD.Value.AddHours((add + 1) * divHour),
                                    ETDStart = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETAStart = objCurrent.ETD.Value.AddHours((add + 1) * divHour),
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                lstAdd.Add(objTOContainerCurrent);
                                model.OPS_COTOContainer.Add(objTOContainerCurrent);

                                var objTOContainerLocal = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objLocal.ID,
                                    OPSContainerID = objLocal.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipLaden,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = fromid,
                                    LocationToID = objLocal.LocationToID,
                                    ETD = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETA = objCurrent.ETD.Value.AddHours((add + 1) * divHour),
                                    ETDStart = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETAStart = objCurrent.ETD.Value.AddHours((add + 1) * divHour),
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                model.OPS_COTOContainer.Add(objTOContainerLocal);

                                add++;

                                objTOContainerCurrent = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objCurrent.ID,
                                    OPSContainerID = objCurrent.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = objLocal.LocationToID,
                                    LocationToID = objCurrent.LocationToID,
                                    ETD = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETA = objCurrent.ETA.Value,
                                    ETDStart = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETAStart = objCurrent.ETA.Value,
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                lstAdd.Add(objTOContainerCurrent);
                                model.OPS_COTOContainer.Add(objTOContainerCurrent);

                                add++;
                            }
                            else
                            {
                                var objTOContainerCurrent = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objCurrent.ID,
                                    OPSContainerID = objCurrent.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = fromid,
                                    LocationToID = objLocal.LocationToID,
                                    ETD = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETA = objCurrent.ETA.Value,
                                    ETDStart = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETAStart = objCurrent.ETA.Value,
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                lstAdd.Add(objTOContainerCurrent);
                                model.OPS_COTOContainer.Add(objTOContainerCurrent);

                                var objTOContainerLocal = new OPS_COTOContainer()
                                {
                                    CreatedBy = account.UserName,
                                    CreatedDate = DateTime.Now,

                                    ParentID = objLocal.ID,
                                    OPSContainerID = objLocal.OPSContainerID,
                                    IsSwap = false,
                                    IsInput = false,
                                    TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerWait,
                                    TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait,
                                    StatusOfCOContainerID = -(int)SYSVarType.StatusOfCOContainerShipLaden,
                                    CreateByMasterID = objCurrent.COTOMasterID,

                                    COTOMasterID = objCurrent.COTOMasterID,
                                    LocationFromID = fromid,
                                    LocationToID = objLocal.LocationToID,
                                    ETD = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETA = objCurrent.ETA.Value,
                                    ETDStart = objCurrent.ETD.Value.AddHours(add * divHour),
                                    ETAStart = objCurrent.ETA.Value,
                                    SortOrder = objCurrent.SortOrder + add,
                                    COTOSort = cotoSort + add,
                                    IsSplit = false
                                };
                                model.OPS_COTOContainer.Add(objTOContainerLocal);

                                add++;
                            }

                            objCurrent.IsSplit = true;
                            objLocal.IsSplit = true;

                            if (add > 1)
                            {
                                var first = lstAdd[0];
                                var locationid = first.LocationToID;
                                foreach (var tocon in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                {
                                    if (tocon.COTOSort < objCurrent.COTOSort)
                                        tocon.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                                    if (tocon.COTOSort == objCurrent.COTOSort)
                                    {
                                        tocon.LocationToID = first.LocationToID;
                                        tocon.ETD = first.ETD;
                                        tocon.ETA = first.ETA;
                                        tocon.ETDStart = first.ETD;
                                        tocon.ETAStart = first.ETA;
                                    }
                                    if (tocon.COTOSort > objCurrent.COTOSort)
                                        tocon.COTOSort += (add - 1);
                                }
                                foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                {
                                    if (coto.SortOrder < objCurrent.COTOSort)
                                        coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                                    if (coto.SortOrder == objCurrent.COTOSort)
                                        coto.LocationToID = locationid;
                                    if (coto.SortOrder > objCurrent.COTOSort)
                                        coto.SortOrder += (add - 1);
                                }
                                foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                {
                                    if (toloc.SortOrder <= objCurrent.COTOSort)
                                        toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                    if (toloc.SortOrder > objCurrent.COTOSort)
                                        toloc.SortOrder += (add - 1);
                                }

                                for (int i = 1; i < lstAdd.Count; i++)
                                {
                                    var item = lstAdd[i];
                                    locationid = item.LocationFromID;

                                    //new location 
                                    var objTOLocationNew = new OPS_COTOLocation();
                                    objTOLocationNew.LocationID = locationid;
                                    objTOLocationNew.SortOrder = item.COTOSort;
                                    objTOLocationNew.COTOMasterID = item.COTOMasterID.Value;
                                    objTOLocationNew.CreatedBy = account.UserName;
                                    objTOLocationNew.CreatedDate = DateTime.Now;
                                    objTOLocationNew.DateComeEstimate = item.ETD.Value;
                                    objTOLocationNew.DateLeaveEstimate = item.ETD.Value;
                                    objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                                    objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                    model.OPS_COTOLocation.Add(objTOLocationNew);

                                    //new coto
                                    var objTONew = new OPS_COTO();
                                    objTONew.CreatedBy = account.UserName;
                                    objTONew.CreatedDate = DateTime.Now;
                                    objTONew.COTOMasterID = item.COTOMasterID.Value;
                                    objTONew.IsOPS = true;
                                    objTONew.SortOrder = item.COTOSort;
                                    objTONew.LocationFromID = item.LocationFromID;
                                    objTONew.LocationToID = item.LocationToID;
                                    objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                                    model.OPS_COTO.Add(objTONew);
                                }
                            }

                            model.SaveChanges();

                        }
                    }

                    var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objCON.COTOMasterID);
                    if (objMaster != null)
                    {
                        objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterDelivery;
                        objMaster.ModifiedBy = account.UserName;
                        objMaster.ModifiedDate = DateTime.Now;
                        model.SaveChanges();
                    }
                }
            }

            return result;
        }

        public static List<HelperTOMaster_Error> MONCO_OrderExport(DataEntities model, AccountItem account, int opscontainerid, List<int> lstORDContainerID)
        {
            var result = new List<HelperTOMaster_Error>();
            var objCON = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.ID == opscontainerid && c.IsSplit == false)
                    .Select(c => new { c.COTOMasterID, c.OPSContainerID, c.SortOrder, c.COTOSort, c.TypeOfStatusContainerID }).FirstOrDefault();
            if (objCON != null)
            {
                var lstStatusID = new List<int>()
                {
                    -(int)SYSVarType.StatusOfCOContainerIMEmpty,
                    -(int)SYSVarType.StatusOfCOContainerShipEmpty,
                };

                var lstServiceOfOrder = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCON.COTOMasterID).Select(c => new { c.OPSContainerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).Distinct().ToList();
                Dictionary<int, int> dic = new Dictionary<int, int>();
                foreach (var item in lstServiceOfOrder)
                {
                    var objStatus = model.OPS_COTOContainer.Where(c => c.OPSContainerID == item.OPSContainerID && lstStatusID.Contains(c.StatusOfCOContainerID) && c.IsSplit == false).OrderByDescending(c => c.COTOSort).Select(c => new { c.ID }).FirstOrDefault();
                    if (objStatus != null && lstORDContainerID.Count > dic.Count)
                        dic.Add(objStatus.ID, lstORDContainerID[dic.Count]);
                }
                if (lstORDContainerID.Count == 1)
                {
                    foreach (var val in dic)
                    {
                        var objCurrent = model.OPS_COTOContainer.Where(c => c.ID == val.Key).FirstOrDefault();
                        var objExportEmpty = model.OPS_COTOContainer.Where(c => c.OPS_Container.ContainerID == val.Value && c.ParentID == null && c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty).FirstOrDefault();
                        if (objCurrent != null && objExportEmpty != null && objCurrent.ETD.Value.CompareTo(objCurrent.ETA.Value) < 0)
                        {
                            var objExportLaden = model.OPS_COTOContainer.Where(c => c.OPSContainerID == objExportEmpty.OPSContainerID && c.ParentID == null && c.SortOrder > objExportEmpty.SortOrder).OrderBy(c => c.SortOrder).FirstOrDefault();
                            if (objExportLaden != null)
                            {
                                foreach (var item in model.OPS_COTOContainer.Where(c => c.OPSContainerID == objExportEmpty.OPSContainerID && c.ParentID != null && c.ID != objExportEmpty.ID && c.ID != objExportLaden.ID))
                                    model.OPS_COTOContainer.Remove(item);
                                foreach (var item in model.OPS_COTOContainer.Where(c => c.OPSContainerID == objExportEmpty.OPSContainerID && c.ParentID == null && c.ID != objExportEmpty.ID && c.ID != objExportLaden.ID))
                                    model.OPS_COTOContainer.Remove(item);
                                model.SaveChanges();

                                double totalHours = (objCurrent.ETA.Value - objCurrent.ETD.Value).TotalHours;
                                double divHour = totalHours / 2;

                                if (objCurrent.LocationFromID == objExportEmpty.LocationToID)
                                {
                                    //laden = get empty
                                    var objBefore = model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCurrent.COTOMasterID && c.COTOSort == objCurrent.COTOSort - 1).Select(c => new { c.LocationFromID, c.ETD, c.ETA, c.COTOSort }).FirstOrDefault();
                                    if (objBefore != null)
                                    {
                                        objCurrent.LocationToID = objExportLaden.LocationToID;
                                        objCurrent.ModifiedBy = account.UserName;
                                        objCurrent.ModifiedDate = DateTime.Now;
                                        objCurrent.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;

                                        objExportEmpty.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                                        objExportEmpty.IsSplit = false;
                                        objExportEmpty.LocationFromID = objBefore.LocationFromID;
                                        objExportEmpty.ETD = objBefore.ETD.Value;
                                        objExportEmpty.ETA = objBefore.ETA.Value;
                                        objExportEmpty.ETDStart = objBefore.ETD.Value;
                                        objExportEmpty.ETAStart = objBefore.ETA.Value;
                                        objExportEmpty.COTOSort = objBefore.COTOSort;
                                        objExportEmpty.COTOMasterID = objCurrent.COTOMasterID;

                                        objExportEmpty.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                                        objExportLaden.IsSplit = false;
                                        objExportLaden.ETD = objCurrent.ETD.Value;
                                        objExportLaden.ETA = objCurrent.ETA.Value;
                                        objExportLaden.ETDStart = objCurrent.ETD.Value;
                                        objExportLaden.ETAStart = objCurrent.ETA.Value;
                                        objExportLaden.COTOSort = objCurrent.COTOSort;
                                        objExportLaden.COTOMasterID = objCurrent.COTOMasterID;

                                        foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                        {
                                            if (tocontainer.COTOSort > objCurrent.COTOSort)
                                            {
                                                if (tocontainer.COTOSort == objCurrent.COTOSort + 1)
                                                    tocontainer.LocationFromID = objExportLaden.LocationToID;
                                                tocontainer.ModifiedBy = account.UserName;
                                                tocontainer.ModifiedDate = DateTime.Now;

                                                tocontainer.ParentID = null;
                                                tocontainer.OPSContainerID = objExportLaden.OPSContainerID;
                                            }
                                            else
                                            {
                                                if (tocontainer.COTOSort == objCurrent.COTOSort)
                                                    tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                                                else
                                                    tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                                                tocontainer.ModifiedBy = account.UserName;
                                                tocontainer.ModifiedDate = DateTime.Now;
                                            }
                                        }

                                        foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                        {
                                            if (coto.SortOrder == objCurrent.COTOSort)
                                            {
                                                coto.LocationToID = objExportLaden.LocationToID;
                                                coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                                                coto.ModifiedBy = account.UserName;
                                                coto.ModifiedDate = DateTime.Now;
                                            }
                                            else if (coto.SortOrder == objCurrent.COTOSort + 1)
                                            {
                                                coto.LocationToID = objExportLaden.LocationToID;
                                                coto.ModifiedBy = account.UserName;
                                                coto.ModifiedDate = DateTime.Now;
                                            }
                                            else if (coto.SortOrder < objCurrent.COTOSort)
                                            {
                                                coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                                                coto.ModifiedBy = account.UserName;
                                                coto.ModifiedDate = DateTime.Now;
                                            }
                                        }

                                        foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                        {
                                            if (toloc.SortOrder == objCurrent.COTOSort + 1)
                                            {
                                                toloc.LocationID = objExportLaden.LocationToID;
                                                toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                                toloc.ModifiedBy = account.UserName;
                                                toloc.ModifiedDate = DateTime.Now;
                                            }
                                            else if (toloc.SortOrder <= objCurrent.COTOSort)
                                            {
                                                toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                                toloc.ModifiedBy = account.UserName;
                                                toloc.ModifiedDate = DateTime.Now;
                                            }
                                        }

                                        model.SaveChanges();
                                    }
                                }
                                else
                                {
                                    //return empty = get empty
                                    objCurrent.LocationToID = objExportEmpty.LocationToID;
                                    objCurrent.ETD = objCurrent.ETD.Value;
                                    objCurrent.ETA = objCurrent.ETD.Value.AddHours(divHour);
                                    objCurrent.ETDStart = objCurrent.ETD.Value;
                                    objCurrent.ETAStart = objCurrent.ETD.Value.AddHours(divHour);
                                    objCurrent.ModifiedBy = account.UserName;
                                    objCurrent.ModifiedDate = DateTime.Now;
                                    objCurrent.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;

                                    objExportEmpty.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                                    objExportEmpty.IsSplit = false;
                                    objExportEmpty.LocationFromID = objCurrent.LocationFromID;
                                    objExportEmpty.ETD = objCurrent.ETD.Value;
                                    objExportEmpty.ETA = objCurrent.ETD.Value.AddHours(divHour);
                                    objExportEmpty.ETDStart = objCurrent.ETD.Value;
                                    objExportEmpty.ETAStart = objCurrent.ETD.Value.AddHours(divHour);
                                    objExportEmpty.COTOSort = objCurrent.COTOSort;
                                    objExportEmpty.COTOMasterID = objCurrent.COTOMasterID;

                                    objExportLaden.IsSplit = false;
                                    objExportLaden.ETD = objCurrent.ETD.Value.AddHours(divHour);
                                    objExportLaden.ETA = objCurrent.ETD.Value.AddHours(divHour * 2);
                                    objExportLaden.ETDStart = objCurrent.ETD.Value.AddHours(divHour);
                                    objExportLaden.ETAStart = objCurrent.ETD.Value.AddHours(divHour * 2);
                                    objExportLaden.COTOSort = objCurrent.COTOSort + 1;
                                    objExportLaden.COTOMasterID = objCurrent.COTOMasterID;

                                    foreach (var tocontainer in model.OPS_COTOContainer.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                    {
                                        if (tocontainer.COTOSort > objCurrent.COTOSort)
                                        {
                                            if (tocontainer.COTOSort == objCurrent.COTOSort + 1)
                                                tocontainer.LocationFromID = objExportLaden.LocationToID;
                                            tocontainer.ModifiedBy = account.UserName;
                                            tocontainer.ModifiedDate = DateTime.Now;
                                            tocontainer.COTOSort++;
                                            tocontainer.SortOrder++;

                                            tocontainer.ParentID = null;
                                            tocontainer.OPSContainerID = objExportLaden.OPSContainerID;
                                        }
                                        else
                                        {
                                            if (tocontainer.COTOSort == objCurrent.COTOSort)
                                                tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerTranfer;
                                            else
                                                tocontainer.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                                            tocontainer.ModifiedBy = account.UserName;
                                            tocontainer.ModifiedDate = DateTime.Now;
                                        }
                                    }

                                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                    {
                                        if (coto.SortOrder == objCurrent.COTOSort)
                                        {
                                            coto.LocationToID = objExportEmpty.LocationToID;
                                            coto.COTOStatusID = -(int)SYSVarType.COTOStatusRunning;
                                            coto.ModifiedBy = account.UserName;
                                            coto.ModifiedDate = DateTime.Now;
                                        }
                                        else if (coto.SortOrder > objCurrent.COTOSort)
                                        {
                                            if (coto.SortOrder == objCurrent.COTOSort + 1)
                                                coto.LocationToID = objExportLaden.LocationToID;
                                            coto.SortOrder++;
                                            coto.ModifiedBy = account.UserName;
                                            coto.ModifiedDate = DateTime.Now;
                                        }
                                        else
                                        {
                                            coto.COTOStatusID = -(int)SYSVarType.COTOStatusComplete;
                                            coto.ModifiedBy = account.UserName;
                                            coto.ModifiedDate = DateTime.Now;
                                        }
                                    }

                                    foreach (var toloc in model.OPS_COTOLocation.Where(c => c.COTOMasterID == objCurrent.COTOMasterID))
                                    {
                                        if (toloc.SortOrder > objCurrent.COTOSort)
                                        {
                                            if (toloc.SortOrder == objCurrent.COTOSort + 1)
                                                toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                            else
                                                toloc.SortOrder++;
                                            toloc.ModifiedBy = account.UserName;
                                            toloc.ModifiedDate = DateTime.Now;
                                        }
                                        else
                                        {
                                            toloc.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusLeave;
                                            toloc.ModifiedBy = account.UserName;
                                            toloc.ModifiedDate = DateTime.Now;
                                        }
                                    }

                                    //new location 
                                    var objTOLocationNew = new OPS_COTOLocation();
                                    objTOLocationNew.LocationID = objExportLaden.LocationToID;
                                    objTOLocationNew.SortOrder = objCurrent.COTOSort + 2;
                                    objTOLocationNew.COTOMasterID = objCurrent.COTOMasterID.Value;
                                    objTOLocationNew.CreatedBy = account.UserName;
                                    objTOLocationNew.CreatedDate = DateTime.Now;
                                    objTOLocationNew.DateComeEstimate = objCurrent.ETD.Value;
                                    objTOLocationNew.DateLeaveEstimate = objCurrent.ETD.Value;
                                    objTOLocationNew.COTOLocationStatusID = -(int)SYSVarType.COTOLocationStatusPlan;
                                    objTOLocationNew.TypeOfTOLocationID = -(int)SYSVarType.TypeOfTOLocationEmpty;
                                    model.OPS_COTOLocation.Add(objTOLocationNew);

                                    //new coto
                                    var objTONew = new OPS_COTO();
                                    objTONew.CreatedBy = account.UserName;
                                    objTONew.CreatedDate = DateTime.Now;
                                    objTONew.COTOMasterID = objCurrent.COTOMasterID.Value;
                                    objTONew.IsOPS = true;
                                    objTONew.SortOrder = objCurrent.COTOSort + 1;
                                    objTONew.LocationFromID = objExportLaden.LocationFromID;
                                    objTONew.LocationToID = objExportLaden.LocationToID;
                                    objTONew.COTOStatusID = -(int)SYSVarType.COTOStatusOpen;
                                    model.OPS_COTO.Add(objTONew);

                                    model.SaveChanges();
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        public static List<HelperTOMaster_Error> MONCO_OrderExportByMaster(DataEntities model, AccountItem account, int opscontainerid, List<int> lstORDContainerID)
        {
            var result = new List<HelperTOMaster_Error>();
            var objContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.ID == opscontainerid && c.IsSplit == false)
                    .Select(c => new { c.COTOMasterID, c.OPSContainerID, c.SortOrder, c.COTOSort, c.TypeOfStatusContainerID }).FirstOrDefault();
            if (objContainer != null)
            {

            }

            return result;
        }

        public static HelperTOMaster_Error MONCO_ChangeOPSContainer(DataEntities model, AccountItem account, int opscontainerid)
        {
            var lstMasteID = model.OPS_COTOContainer.Where(c => c.OPSContainerID == opscontainerid && c.COTOMasterID > 0).Select(c => c.COTOMasterID).Distinct().ToList();
            if (lstMasteID.Count > 0)
            {
                //var lstLaden=new List<int>
                foreach (var masterid in lstMasteID)
                {
                    //var ton=
                    var lstCOTOContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == masterid && c.OPS_Container.Ton > 0).Select(c => new { c.COTOSort, c.StatusOfCOContainerID, c.OPS_Container.Ton }).ToList();
                    //if (lstCOTOContainer.Count > 0)
                    //{

                    //}
                    //var to=model.OPS_COTOContainer.Where(c=>c.COTOMasterID==masterid)

                    foreach (var coto in model.OPS_COTO.Where(c => c.COTOMasterID == masterid))
                    {
                        double ton = 0;
                        foreach (var tocontainer in lstCOTOContainer.Where(c => c.COTOSort == coto.SortOrder))
                        {
                            //if(tocontainer.StatusOfCOContainerID==-())
                        }
                    }
                }
            }
            return HelperTOMaster_Error.None;
        }
        #endregion

        #endregion

        #region Distribution
        private string DI_GetLastCode(DataEntities model)
        {
            long idx = 1;
            var last = model.OPS_DITOMaster.OrderByDescending(c => c.ID).Select(c => new { c.ID }).FirstOrDefault();
            if (last != null)
                idx = Convert.ToInt64(last.ID) + 1;
            else
                idx = 1;
            return DICodePrefix + idx.ToString(DICodeNum);
        }

        public static List<HelperTOMaster_Error> OPSDI_TenderAccept(DataEntities model, AccountItem account, List<DTOOPSCO_VEN_TOMaster> lstMaster)
        {
            if (lstMaster != null && lstMaster.Count > 0)
            {
                var result = new List<HelperTOMaster_Error>();

                foreach (var item in lstMaster)
                {
                    var objRate = model.OPS_COTORate.Where(c => c.COTOMasterID == item.ID && c.IsAccept == null).OrderBy(c => c.SortOrder).FirstOrDefault();
                    if (objRate == null)
                        result.Add(HelperTOMaster_Error.Fail);
                    else
                    {
                        var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == item.ID);
                        if (objMaster != null)
                        {
                            if (item.VehicleID <= 2)
                                objMaster.VehicleID = DefaultTruck;
                            else
                                objMaster.VehicleID = item.VehicleID;
                            if (item.RomoocID > 1)
                                objMaster.RomoocID = item.RomoocID;
                            else
                                objMaster.RomoocID = DefaultRomooc;
                            objMaster.VendorOfVehicleID = objRate.VendorID;
                            objMaster.VendorOfRomoocID = objRate.VendorID;
                            objMaster.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterTendered;
                            objMaster.ModifiedBy = account.UserName;
                            objMaster.ModifiedDate = DateTime.Now;

                            objRate.IsAccept = true;

                            model.SaveChanges();

                            result.Add(HelperTOMaster_Error.None);
                        }
                    }
                }

                return result;
            }
            else
                throw FaultHelper.BusinessFault(null, null, "list fail");
        }

        public static List<HelperTOMaster_Error> OPSDI_TenderReject(DataEntities model, AccountItem account, List<DTOOPSCO_VEN_TOMaster> lstMaster)
        {
            if (lstMaster != null && lstMaster.Count > 0)
            {
                var result = new List<HelperTOMaster_Error>();

                foreach (var item in lstMaster)
                {
                    var objRate = model.OPS_DITORate.Where(c => c.DITOMasterID == item.ID && c.IsAccept == null).OrderBy(c => c.SortOrder).FirstOrDefault();
                    if (objRate == null)
                        result.Add(HelperTOMaster_Error.Fail);
                    else
                    {
                        var objNext = model.OPS_DITORate.Where(c => c.DITOMasterID == item.ID && c.SortOrder > objRate.SortOrder).OrderBy(c => c.SortOrder).FirstOrDefault();
                        var objMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.ID);
                        if (objMaster != null)
                        {
                            objRate.IsAccept = false;
                            if (item.ReasonID > 0)
                            {
                                objRate.ReasonID = item.ReasonID;
                                objRate.Reason = item.Reason;
                            }
                            if (objNext != null)
                            {
                                objMaster.VendorOfVehicleID = objNext.VendorID;
                                //objMaster.VendorOfRomoocID = objNext.VendorID;
                                objNext.FirstRateTime = DateTime.Now;
                                objNext.LastRateTime = DateTime.Now.AddHours(objMaster.RateTime.Value);
                            }
                            else
                            {
                                objMaster.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterApproved;
                                objMaster.VendorOfVehicleID = objMaster.SYSCustomerID;
                                //objMaster.VendorOfRomoocID = account.SYSCustomerID;
                                objMaster.ModifiedBy = account.UserName;
                                objMaster.ModifiedDate = DateTime.Now;
                            }
                            model.SaveChanges();

                            result.Add(HelperTOMaster_Error.None);
                        }
                    }
                }

                return result;
            }
            else
                return new List<HelperTOMaster_Error>();
            //else
            //    throw FaultHelper.BusinessFault(null, null, "list fail");
        }

        public static void OPS_DI_ToMON(DataEntities model, List<int> data, AccountItem account)
        {
            foreach (var item in data)
            {
                var obj = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item);
                if (obj != null)
                {
                    if (obj.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterApproved)
                    {
                        if (model.OPS_DITOGroupProduct.Count(c => c.DITOMasterID == obj.ID && c.OrderGroupProductID > 0 && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel) == 0)
                            throw FaultHelper.BusinessFault(null, null, "Không có chi tiết hàng hóa. Không thể duyệt. Chuyến " + obj.Code);
                        if (obj.ETA == null || obj.ETD == null)
                            throw FaultHelper.BusinessFault(null, null, "Thiếu thông tin thời gian ETD hoặc ETA. Không thể duyệt. Chuyến " + obj.Code);
                        if (obj.VehicleID == null && obj.VehicleID <= 2)
                            throw FaultHelper.BusinessFault(null, null, "Không thể duyệt! Chưa chọn xe. Chuyến " + obj.Code);
                        if (obj.ETD >= obj.ETA)
                            throw FaultHelper.BusinessFault(null, null, "Sai ràng buộc thời gian ETD - ETA. Không thể duyệt. Chuyến " + obj.Code);
                        if ((obj.VendorOfVehicleID == null || obj.VendorOfVehicleID == account.SYSCustomerID) && string.IsNullOrEmpty(obj.DriverName1))
                            throw FaultHelper.BusinessFault(null, null, "Thiếu thông tin tài xế. Không thể duyệt. Chuyến " + obj.Code);

                        obj.ModifiedBy = account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterTendered;

                        var objFLM = model.FLM_VehiclePlan.Where(c => c.VehicleID == obj.VehicleID && c.SYSCustomerID == account.SYSCustomerID && c.TypeOfDriverID == -(int)SYSVarType.TypeOfDriverEx && c.DateFrom < obj.ETA && c.DateTo > obj.ETD).OrderBy(c => c.SortOrder).FirstOrDefault();
                        if (objFLM != null)
                        {
                            obj.DriverID2 = objFLM.DriverID;
                            obj.DriverName2 = objFLM.FLM_Driver.CAT_Driver.LastName + " " + objFLM.FLM_Driver.CAT_Driver.FirstName;
                            obj.DriverTel2 = objFLM.FLM_Driver.CAT_Driver.Cellphone;
                            obj.TypeOfDriverID2 = -(int)SYSVarType.TypeOfDriverEx;
                        }
                        else if (obj.CAT_Vehicle.AssistantID > 0)
                        {
                            obj.DriverID2 = obj.CAT_Vehicle.AssistantID.Value;
                            obj.DriverName2 = obj.CAT_Vehicle.FLM_Driver1.CAT_Driver.LastName + " " + obj.CAT_Vehicle.FLM_Driver1.CAT_Driver.FirstName;
                            obj.DriverTel2 = obj.CAT_Vehicle.FLM_Driver1.CAT_Driver.Cellphone;
                            obj.TypeOfDriverID2 = -(int)SYSVarType.TypeOfDriverEx;
                        }

                        //Gom các nhóm hàng cùng chi tiết.
                        List<int> dataGop = new List<int>();
                        while (model.OPS_DITOGroupProduct.FirstOrDefault(c => !dataGop.Contains(c.ID) && c.OrderGroupProductID > 0 && c.DITOMasterID == obj.ID && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel) != null)
                        {
                            var objGop = model.OPS_DITOGroupProduct.FirstOrDefault(c => !dataGop.Contains(c.ID) && c.OrderGroupProductID > 0 && c.DITOMasterID == obj.ID && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel);
                            dataGop.Add(objGop.ID);
                            if (model.OPS_DITOGroupProduct.Count(c => c.DITOMasterID == obj.ID && c.ID != objGop.ID && c.OrderGroupProductID == objGop.OrderGroupProductID && c.DNCode == objGop.DNCode && c.Note == objGop.Note && c.Note1 == objGop.Note1 && c.Note2 == objGop.Note2) > 0)
                            {
                                objGop.ModifiedBy = account.UserName;
                                objGop.ModifiedDate = DateTime.Now;
                                var dataDel = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.ID && c.ID != objGop.ID && c.OrderGroupProductID == objGop.OrderGroupProductID && c.DNCode == objGop.DNCode && c.Note == objGop.Note && c.Note1 == objGop.Note1 && c.Note2 == objGop.Note2).ToList();
                                foreach (var o in dataDel)
                                {
                                    objGop.TonBBGN = objGop.TonTranfer = objGop.Ton = Math.Round(objGop.Ton + o.Ton, 5, MidpointRounding.AwayFromZero);
                                    objGop.CBMBBGN = objGop.CBMTranfer = objGop.CBM = Math.Round(objGop.CBM + o.CBM, 5, MidpointRounding.AwayFromZero);
                                    objGop.QuantityBBGN = objGop.QuantityLoading = objGop.QuantityTranfer = objGop.Quantity = Math.Round(objGop.Quantity + o.Quantity, 5, MidpointRounding.AwayFromZero);
                                    foreach (var i in model.OPS_DITOProduct.Where(c => c.DITOGroupProductID == o.ID))
                                    {
                                        model.OPS_DITOProduct.Remove(i);
                                    }
                                    model.OPS_DITOGroupProduct.Remove(o);
                                }
                                var objProduct = model.OPS_DITOProduct.FirstOrDefault(c => c.DITOGroupProductID == objGop.ID);
                                if (objProduct != null)
                                {
                                    objProduct.ModifiedBy = account.UserName;
                                    objProduct.ModifiedDate = DateTime.Now;
                                    objProduct.Quantity = objProduct.QuantityBBGN = objProduct.QuantityTranfer = objGop.Quantity;
                                    switch (objProduct.ORD_Product.CUS_Product.CAT_Packing.TypeOfPackageID)
                                    {
                                        case -(int)SYSVarType.TypeOfPackingGOPTon:
                                            objProduct.Quantity = objProduct.QuantityBBGN = objProduct.QuantityTranfer = objGop.Ton;
                                            break;
                                        case -(int)SYSVarType.TypeOfPackingGOPCBM:
                                            objProduct.Quantity = objProduct.QuantityBBGN = objProduct.QuantityTranfer = objGop.CBM;
                                            break;
                                    }
                                }
                                model.SaveChanges();
                            }
                        }

                        var sOrder = model.OPS_DITORate.Where(c => c.DITOMasterID == obj.ID).OrderByDescending(c => c.SortOrder).Select(c => c.SortOrder).FirstOrDefault();
                        var objRate = model.OPS_DITORate.FirstOrDefault(c => c.DITOMasterID == obj.ID && c.VendorID == obj.VendorOfVehicleID && c.IsSend == true);
                        if (objRate == null)
                        {
                            objRate = new OPS_DITORate();
                            objRate.CreatedBy = account.UserName;
                            objRate.CreatedDate = DateTime.Now;
                            objRate.DITOMasterID = obj.ID;
                            objRate.VendorID = obj.VendorOfVehicleID;
                            objRate.SortOrder = sOrder > 0 ? sOrder + 1 : 1;
                            objRate.IsSend = true;
                            objRate.Debit = 0;
                            objRate.IsManual = false;
                            objRate.FirstRateTime = DateTime.Now;
                            objRate.LastRateTime = DateTime.Now.Add(TimeSpan.FromHours(obj.RateTime ?? 2));

                            model.OPS_DITORate.Add(objRate);
                        }
                        else
                        {
                            objRate.ModifiedBy = account.UserName;
                            objRate.ModifiedDate = DateTime.Now;
                        }
                        objRate.IsAccept = true;
                        model.SaveChanges();

                        var dataLocation = model.OPS_DITOLocation.Where(c => c.DITOMasterID == obj.ID).Select(c => new
                        {
                            c.ID,
                            c.LocationID,
                            c.SortOrder
                        }).OrderBy(c => c.SortOrder).ToList();
                        for (var i = 1; i < dataLocation.Count; i++)
                        {
                            OPS_DITO objTO = new OPS_DITO();
                            objTO.CreatedDate = DateTime.Now;
                            objTO.CreatedBy = account.UserName;
                            objTO.IsOPS = true;
                            objTO.StatusOfDITOID = -(int)SYSVarType.StatusOfDITOStockPlan;
                            objTO.SortOrder = i;
                            objTO.LocationToID = dataLocation[i].LocationID.Value;
                            objTO.LocationFromID = dataLocation[i - 1].LocationID.Value;
                            objTO.DITOMasterID = obj.ID;
                            model.OPS_DITO.Add(objTO);
                        }

                        //Update VendorLoad & Unload
                        foreach (var gop in model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.ID))
                        {
                            gop.ModifiedBy = account.UserName;
                            gop.ModifiedDate = DateTime.Now;
                            gop.VendorLoadID = gop.VendorUnLoadID = obj.VendorOfVehicleID;
                            // Tìm xem có thiết lập vendor bốc xếp cho điểm này
                            var venLocation = model.CUS_Location.FirstOrDefault(c => c.CustomerID != obj.VendorOfVehicleID && c.CustomerID != gop.ORD_GroupProduct.ORD_Order.CustomerID && c.LocationID == gop.ORD_GroupProduct.CUS_Location.LocationID && c.IsVendorLoad == true);
                            if (venLocation != null)
                                gop.VendorLoadID = venLocation.CustomerID;
                            venLocation = model.CUS_Location.FirstOrDefault(c => c.CustomerID != obj.VendorOfVehicleID && c.CustomerID != gop.ORD_GroupProduct.ORD_Order.CustomerID && c.LocationID == gop.ORD_GroupProduct.CUS_Location1.LocationID && c.IsVendorUnLoad == true);
                            if (venLocation != null)
                                gop.VendorUnLoadID = venLocation.CustomerID;
                        }
                        model.SaveChanges();

                        HelperTimeSheet.Remove(model, account, obj.ID, SYSVarType.StatusOfAssetTimeSheetDITOMaster);
                        HelperTimeSheet.Create(model, account, obj.ID, SYSVarType.StatusOfAssetTimeSheetDITOMaster);

                        var SDATA = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.ID).Select(c => c.ORD_GroupProduct.OrderID).Distinct().ToList();
                        HelperStatus.ORDOrder_Status(model, account, SDATA);
                    }
                    else
                    {
                        throw FaultHelper.BusinessFault(null, null, "Không thể duyệt! Chỉ được duyệt các chuyến đang lập kế hoạch.");
                    }
                }
            }
        }

        private static void DI_DockScheduleAdd(DataEntities model, AccountItem account, List<int> lstMasterID)
        {
            double houradd = 2;
            bool flag = false;

            if (lstMasterID != null && lstMasterID.Count > 0)
            {
                foreach (var masterid in lstMasterID)
                {
                    var lstStock = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OrderGroupProductID > 0 && c.ORD_GroupProduct.LocationFromID > 0 && c.DITOMasterID == masterid && c.OPS_DITOMaster.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterTendered).Select(c => new { c.OPS_DITOMaster.ETD, c.ORD_GroupProduct.LocationFromID }).Distinct().ToList();
                    if (lstStock.Count > 0 && model.OPS_TOMasterDockSchedule.Where(c => c.DITOMasterID == masterid).Count() == 0)
                    {
                        foreach (var stock in lstStock)
                        {
                            double ton = 0;
                            double cbm = 0;
                            double quantity = 0;
                            var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == masterid && c.ORD_GroupProduct.LocationFromID == stock.LocationFromID);
                            if (query.Count() > 0)
                            {
                                ton = query.Sum(c => c.Ton);
                                cbm = query.Sum(c => c.CBM);
                                quantity = query.Sum(c => c.Quantity);
                            }

                            var catloc = model.CUS_Location.Where(c => c.ID == stock.LocationFromID).Select(c => new { c.LocationID }).FirstOrDefault();
                            if (catloc != null)
                            {
                                if (model.CAT_Dock.Where(c => c.LocationID == catloc.LocationID).Count() > 0)
                                {
                                    var obj = new OPS_TOMasterDockSchedule();
                                    obj.DITOMasterID = masterid;
                                    obj.LocationID = catloc.LocationID;
                                    obj.TOMasterDockScheduleStatusID = -(int)SYSVarType.TOMasterDockScheduleStatusOpen;
                                    obj.CreatedBy = account.UserName;
                                    obj.CreatedDate = DateTime.Now;

                                    obj.DateCome = stock.ETD.Value;
                                    obj.DateComeEnd = stock.ETD.Value.AddHours(houradd);
                                    obj.DateComeModified = obj.DateCome;
                                    obj.DateComeModifiedEnd = obj.DateComeEnd;
                                    obj.DateComeApproved = obj.DateCome;
                                    obj.DateComeApprovedEnd = obj.DateComeEnd;

                                    obj.Ton = ton;
                                    obj.CBM = cbm;
                                    obj.Quantity = quantity;

                                    model.OPS_TOMasterDockSchedule.Add(obj);
                                    flag = true;
                                }
                            }
                        }
                    }
                }

                if (flag)
                    model.SaveChanges();
            }
        }

        private static void DI_DockScheduleRemove(DataEntities model, AccountItem account, List<int> lstMasterID)
        {
            if (lstMasterID != null && lstMasterID.Count > 0)
            {
                foreach (var masterid in lstMasterID)
                {
                    if (model.OPS_TOMasterDockSchedule.Where(c => c.DITOMasterID == masterid).Count() > 0 && model.OPS_DITOMaster.Where(c => c.ID == masterid && c.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterTendered).Count() == 0)
                    {
                        foreach (var item in model.OPS_TOMasterDockSchedule.Where(c => c.DITOMasterID == masterid))
                            model.OPS_TOMasterDockSchedule.Remove(item);
                        model.SaveChanges();
                    }
                }
            }
        }
        #endregion
    }

    public enum HelperTOMaster_Error
    {
        None = 0,
        Fail = 1,
        VehicleLocation = 2,
        VehicleNoFound = 3,
        SameFromTo = 4,
        ReasionFail = 5,
        AddHourReceived = 6,

        TOContainerComplete_StartVehicleStand = 7,
        TOContainerComplete_StartRomoocStand = 8,
        TOContainerComplete_StartScheduleWait = 9,
        TOContainerComplete_StartHasRomooc = 10,
        TOContainerComplete_HasScheduleWait = 11,

        TOContainerComplete_StartDuplicate = 12,
        TOContainerComplete_StartMoreLocal = 13,
        TOContainerComplete_StartDuplicateMoreLocal = 14,
    }

    public class HelperTOMaster_MONReturn
    {
        public string OfferError { get; set; }
        public string OfferWarning { get; set; }
    }

    public enum HelperTOMaster_Status
    {
        None = 0,
        MONCO_ChangePlanHasRomooc = 1,

    }
    
    public class HelperTOMaster_Start
    {
        public List<HelperTOMaster_Start_Duplicate> ListDuplicate { get; set; }
        public List<HelperTOMaster_Start_Local> ListLocal { get; set; }
    }

    public class HelperTOMaster_Fleet
    {
        public int VehicleID { get; set; }
        public int RomoocID { get; set; }
        public int LocationID { get; set; }
    }

    public class HelperTOMaster_Start_Duplicate
    {
        public int SortFrom { get; set; }
        public int SortTo { get; set; }
        public int ID { get; set; }
    }

    public class HelperTOMaster_Start_Local
    {
        public int SortOrder { get; set; }
        public int ID { get; set; }
    }

    public class HelperTOMaster_TOContainer
    {
        public SYSVarType TypeOfTOLocation { get; set; }
        public int ID { get; set; }
        public int? ParentID { get; set; }
        public int OPSContainerID { get; set; }
        public int LocationFromID { get; set; }
        public int LocationToID { get; set; }
        public DateTime ETD { get; set; }
        public DateTime ETA { get; set; }
        public DateTime? ETDStart { get; set; }
        public DateTime? ETAStart { get; set; }
        public int SortOrder { get; set; }
        public bool IsSplit { get; set; }
        public bool HasCOTO { get; set; }
        public int StatusOfCOContainerID { get; set; }
        public int CATTransportModeID { get; set; }
        public int ServiceOfOrderID { get; set; }
        public bool IsCreate { get; set; }

        public int SortLocation { get; set; }
    }

    public class HelperTOMaster_TOLocation
    {
        public int LocationID { get; set; }
        public int TypeOfTOLocationID { get; set; }
        public DateTime DateComeEstimate { get; set; }
        public DateTime DateLeaveEstimate { get; set; }
        public List<HelperTOMaster_TOContainer> ListTOContainer { get; set; }
    }

    public class HelperTOMaster_COParam
    {
        public int? LocationFromID { get; set; }
        public int? LocationToID { get; set; }
        public int? LocationGetRomoocID { get; set; }
        public int? LocationReturnRomoocID { get; set; }
        public DateTime? DateGetRomooc { get; set; }
        public DateTime? DateReturnRomooc { get; set; }
        public bool? HasBreakRomooc { get; set; }

        public int? LocationStartID { get; set; }
        public DateTime? DateStart { get; set; }
        public DateTime? DateEnd { get; set; }
        public double Hour2Point { get; set; }
        public double Hour2PointCON { get; set; }
    }

    public class HelperTOMaster_Time
    {
        public DateTime ETD { get; set; }
        public DateTime ETA { get; set; }
    }
}
