using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Data;
using DTO;
using System.ServiceModel;
using ExpressionEvaluator;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using OfficeOpenXml;
using System.Web;
using System.IO;

namespace Business
{
    public partial class HelperFinance
    {
        #region Tính lương thường
        // Xóa lương cũ, cập nhật lại dữ liệu chuyến liên quan đến schedule
        public static void Truck_CalculateDriverRerun(DataEntities model, AccountItem Account, int scheduleID)
        {
            #region Xóa PL cũ
            foreach (var pl in model.FIN_PL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ScheduleID == scheduleID))
            {
                foreach (var plDetail in model.FIN_PLDetails.Where(c => c.PLID == pl.ID))
                {
                    foreach (var plGroup in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == plDetail.ID))
                        model.FIN_PLGroupOfProduct.Remove(plGroup);
                    foreach (var plGroup in model.FIN_PLContainer.Where(c => c.PLDetailID == plDetail.ID))
                        model.FIN_PLContainer.Remove(plGroup);
                    model.FIN_PLDetails.Remove(plDetail);
                }
                model.FIN_PL.Remove(pl);
            }
            foreach (var pl in model.FLM_ScheduleAssetTemp.Where(c => c.FLM_ScheduleAsset.ScheduleID == scheduleID))
                model.FLM_ScheduleAssetTemp.Remove(pl);
            foreach (var pl in model.FLM_ScheduleDriverTemp.Where(c => c.FLM_ScheduleDriver.ScheduleID == scheduleID))
                model.FLM_ScheduleDriverTemp.Remove(pl);
            #endregion

            #region Cập nhật dữ liệu StationMonth
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);
            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);

            foreach (var item in model.CAT_StationMonth.Where(c => c.ScheduleID == scheduleID || (c.DateFrom >= schedule.DateFrom && c.DateTo <= schedule.DateTo)))
            {
                item.ModifiedBy = Account.UserName;
                item.ModifiedDate = DateTime.Now;
                if (item.DateFrom >= schedule.DateFrom && item.DateTo <= schedule.DateTo)
                    item.ScheduleID = schedule.ID;
                else
                    item.ScheduleID = null;
            }
            #endregion

            #region Cập nhật ScheduleID và SortOrder cho các chuyến DIMaster
            var lstMaster = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID > 0 && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID));
            foreach (var item in lstMaster)
            {
                item.ModifiedBy = Account.UserName;
                item.ModifiedDate = DateTime.Now;
                item.ScheduleID = schedule.ID;
            }

            var lstMasterSort = lstMaster.Select(c => new HelperFinance_TOMasterSort
            {
                ID = c.ID,
                ETD = c.ETD,
                VehicleID = c.VehicleID.Value,
                SortOrder = c.SortOrder
            }).ToList();

            foreach (var itemGroup in lstMasterSort.OrderBy(c => c.ETD).GroupBy(c => new { c.VehicleID, c.ETD.Value.Date }))
            {
                int sort = 1;
                foreach (var item in itemGroup.OrderBy(c => c.ETD))
                    item.SortOrder = sort++;
            }

            foreach (var item in lstMasterSort)
            {
                var master = lstMaster.FirstOrDefault(c => c.ID == item.ID);
                if (master != null)
                    master.SortOrder = item.SortOrder;
            }
            #endregion

            #region Cập nhật ScheduleID và SortOrder cho các chuyến COMaster
            var lstMasterCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID > 0 && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID));
            foreach (var item in lstMaster)
            {
                item.ModifiedBy = Account.UserName;
                item.ModifiedDate = DateTime.Now;
                item.ScheduleID = schedule.ID;
            }

            var lstMasterCOSort = lstMaster.Select(c => new HelperFinance_TOMasterSort
            {
                ID = c.ID,
                ETD = c.ETD,
                VehicleID = c.VehicleID.Value,
                SortOrder = c.SortOrder
            }).ToList();

            foreach (var itemGroup in lstMasterCOSort.OrderBy(c => c.ETD).GroupBy(c => new { c.VehicleID, c.ETD.Value.Date }))
            {
                int sort = 1;
                foreach (var item in itemGroup.OrderBy(c => c.ETD))
                    item.SortOrder = sort++;
            }

            foreach (var item in lstMasterCOSort)
            {
                var master = lstMaster.FirstOrDefault(c => c.ID == item.ID);
                if (master != null)
                    master.SortOrder = item.SortOrder;
            }
            #endregion

            #region Tính cho Driver
            var lstScheduleDate = model.FLM_ScheduleDate.Where(c => c.ScheduleID == scheduleID);
            // Lấy param
            double totalDay = schedule.TotalDays;
            double totalDayActual = (schedule.DateTo - schedule.DateFrom).TotalDays + 1;
            double totalDayOff = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
            double totalDayOn = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
            double totalDayHoliday = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
            // Tính lại các ngày cho driver
            var lstScheduleDriver = model.FLM_ScheduleDriver.Where(c => c.ScheduleID == schedule.ID);
            foreach (var itemScheduleDriver in lstScheduleDriver)
            {
                itemScheduleDriver.ModifiedBy = Account.UserName;
                itemScheduleDriver.ModifiedDate = DateTime.Now;

                var lstScheduleDateDetail = model.FLM_ScheduleDateDetail.Where(c => c.DriverID == itemScheduleDriver.DriverID).Select(c => new
                {
                    c.FLM_ScheduleDate.Date,
                    c.FLM_ScheduleDate.TypeScheduleDateID
                }).ToList();

                HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                itemCheck.FeeBase = itemScheduleDriver.FeeBase;
                itemCheck.TotalDay = totalDay;
                itemCheck.TotalDayActual = totalDayActual;
                itemCheck.TotalDayOff = totalDayOff;
                itemCheck.TotalDayOn = totalDayOn;
                itemCheck.TotalDayHoliday = totalDayHoliday;
                itemCheck.TotalDayAllowOffDriver = itemScheduleDriver.DaysAllowOff;
                itemCheck.TotalDayAllowOffRemainDriver = itemScheduleDriver.DaysAllowOffRemain.HasValue ? itemScheduleDriver.DaysAllowOffRemain.Value : 0;
                itemCheck.TotalDayOffDriver = lstScheduleDateDetail.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                itemCheck.TotalDayOnDriver = lstScheduleDateDetail.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                itemCheck.TotalDayHolidayDriver = lstScheduleDateDetail.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);

                // ExprDaysAllowOff - Tính ngày DaysAllowOff của từng tài xế: DaysAllowOffRemain, như trên + của riêng tài xế (FLM_ScheduleDateDetail)
                if (!string.IsNullOrEmpty(schedule.ExprDaysAllowOff))
                {
                    decimal? total = Expression_FLMGetValue(itemCheck, schedule.ExprDaysAllowOff);
                    if (total.HasValue)
                        itemScheduleDriver.DaysAllowOff = (double)total.Value;
                }

                // ExprDaysWorkInOn - Tính ngày DaysWorkInOn của từng tài xế: DaysAllowOffRemain, như trên + của riêng tài xế (FLM_ScheduleDateDetail)
                if (!string.IsNullOrEmpty(schedule.ExprDaysWorkInOn))
                {
                    decimal? total = Expression_FLMGetValue(itemCheck, schedule.ExprDaysWorkInOn);
                    if (total.HasValue)
                        itemScheduleDriver.DaysWorkInOn = (double)total.Value;
                }

                // ExprDaysWorkInOff - Tính ngày DaysWorkInOff của từng tài xế: DaysAllowOffRemain, như trên + của riêng tài xế (FLM_ScheduleDateDetail)
                if (!string.IsNullOrEmpty(schedule.ExprDaysWorkInOff))
                {
                    decimal? total = Expression_FLMGetValue(itemCheck, schedule.ExprDaysWorkInOff);
                    if (total.HasValue)
                        itemScheduleDriver.DaysWorkInOff = (double)total.Value;
                }

                // ExprDaysWorkInHoliday - Tính ngày DaysWorkInHoliday của từng tài xế: DaysAllowOffRemain, như trên + của riêng tài xế (FLM_ScheduleDateDetail)
                if (!string.IsNullOrEmpty(schedule.ExprDaysWorkInHoliday))
                {
                    decimal? total = Expression_FLMGetValue(itemCheck, schedule.ExprDaysWorkInHoliday);
                    if (total.HasValue)
                        itemScheduleDriver.DaysWorkInHoliday = (double)total.Value;
                }

                // Cập nhật Tổng chuyến, KM cho driver
                var lstMasterDriver = lstMaster.Where(c => c.DriverID1 == itemScheduleDriver.DriverID || c.DriverID2 == itemScheduleDriver.DriverID || c.DriverID3 == itemScheduleDriver.DriverID || c.DriverID4 == itemScheduleDriver.DriverID || c.DriverID5 == itemScheduleDriver.DriverID);
                var lstMasterCODriver = lstMasterCO.Where(c => c.DriverID1 == itemScheduleDriver.DriverID || c.DriverID2 == itemScheduleDriver.DriverID || c.DriverID3 == itemScheduleDriver.DriverID || c.DriverID4 == itemScheduleDriver.DriverID || c.DriverID5 == itemScheduleDriver.DriverID);
                if (lstMasterDriver.Count() > 0)
                {
                    itemScheduleDriver.TotalTO = lstMaster.Count();
                    itemScheduleDriver.TotalKM = lstMasterDriver.Count(c => c.KM > 0) > 0 ? lstMasterDriver.Where(c => c.KM > 0).Sum(c => c.KM.Value) : 0;
                }
                if (lstMasterCODriver.Count() > 0)
                {
                    itemScheduleDriver.TotalTO += lstMasterCODriver.Count();
                    itemScheduleDriver.TotalKM += lstMasterCODriver.Count(c => c.KM > 0) > 0 ? lstMasterCODriver.Where(c => c.KM > 0).Sum(c => c.KM.Value) : 0;
                }
            }

            // Tính cho các ngày tự định nghĩa khác của driver
            var lstScheduleFeeDriverOther = model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.DriverID.HasValue && !string.IsNullOrEmpty(c.ExprDay));
            if (lstScheduleFeeDriverOther != null && lstScheduleFeeDriverOther.Count() > 0)
            {
                foreach (var itemScheduleFeeDriverOther in lstScheduleFeeDriverOther)
                {
                    // Item check công thức
                    HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                    itemCheck.TotalDay = schedule.TotalDays;
                    itemCheck.TotalDayActual = (schedule.DateTo.Date - schedule.DateFrom.Date).TotalDays + 1;
                    // Param bổ sung
                    var scheduleDriver = model.FLM_ScheduleDriver.FirstOrDefault(c => c.ScheduleID == schedule.ID && c.DriverID == itemScheduleFeeDriverOther.DriverID);
                    if (scheduleDriver != null)
                    {
                        itemCheck.TotalDayOffDriver = scheduleDriver.DaysWorkInOff.HasValue ? scheduleDriver.DaysWorkInOff.Value : 0;
                        itemCheck.TotalDayOnDriver = scheduleDriver.DaysWorkInOn.HasValue ? scheduleDriver.DaysWorkInOn.Value : 0;
                        itemCheck.TotalDayHolidayDriver = scheduleDriver.DaysWorkInHoliday.HasValue ? scheduleDriver.DaysWorkInHoliday.Value : 0;
                        itemCheck.TotalDayAllowOffDriver = scheduleDriver.DaysAllowOff;
                        itemCheck.TotalDayAllowOffRemainDriver = scheduleDriver.DaysAllowOffRemain.HasValue ? scheduleDriver.DaysAllowOffRemain.Value : 0;
                    }
                    decimal? day = Expression_FLMGetValue(itemCheck, itemScheduleFeeDriverOther.ExprDay);
                    if (day.HasValue)
                        itemScheduleFeeDriverOther.Day = (double)day.Value;
                    else
                        itemScheduleFeeDriverOther.Day = 0;
                }
            }
            #endregion

            #region Cập nhật Tổng chuyến và KM cho Asset
            var lstScheduleAsset = model.FLM_ScheduleAsset.Where(c => c.ScheduleID == schedule.ID);
            foreach (var itemScheduleAsset in lstScheduleAsset)
            {
                var lstMasterAsset = lstMaster.Where(c => c.VehicleID == itemScheduleAsset.FLM_Asset.VehicleID);
                if (lstMasterAsset.Count() > 0)
                {
                    itemScheduleAsset.TotalTO = lstMasterAsset.Count();
                    itemScheduleAsset.TotalKM = lstMasterAsset.Count(c => c.KM > 0) > 0 ? lstMasterAsset.Where(c => c.KM > 0).Sum(c => c.KM.Value) : 0;
                }
                var lstMasterCOAsset = lstMasterCO.Where(c => c.VehicleID == itemScheduleAsset.FLM_Asset.VehicleID);
                if (lstMasterCOAsset.Count() > 0)
                {
                    itemScheduleAsset.TotalTO = lstMasterCOAsset.Count();
                    itemScheduleAsset.TotalKM = lstMasterCOAsset.Count(c => c.KM > 0) > 0 ? lstMasterCOAsset.Where(c => c.KM > 0).Sum(c => c.KM.Value) : 0;
                }
            }
            #endregion
        }

        // Tính lại cho schedule
        public static void Truck_CalculateSchedule(DataEntities model, AccountItem Account, int scheduleID)
        {
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);
            if (schedule != null)
            {
                var DateConfig = schedule.DateFrom.Date;
                var DateConfigEnd = schedule.DateTo.Date.AddDays(1);
                var lstScheduleDate = model.FLM_ScheduleDate.Where(c => c.ScheduleID == scheduleID);
                // Lấy param
                double totalDay = schedule.TotalDays;
                double totalDayActual = (schedule.DateTo - schedule.DateFrom).TotalDays + 1;
                double totalDayOff = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                double totalDayOn = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                double totalDayHoliday = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);

                // Tính lại TotalDay cho schedule
                if (!string.IsNullOrEmpty(schedule.ExprTotalDays))
                {
                    HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                    itemCheck.TotalDay = totalDay;
                    itemCheck.TotalDayActual = totalDayActual;
                    itemCheck.TotalDayOff = totalDayOff;
                    itemCheck.TotalDayOn = totalDayOn;
                    itemCheck.TotalDayHoliday = totalDayHoliday;
                    decimal? total = Expression_FLMGetValue(itemCheck, schedule.ExprTotalDays);
                    if (total.HasValue)
                        schedule.TotalDays = (double)total.Value;
                }


                var lstScheduleFee = model.FLM_ScheduleFee.Where(c => c.ScheduleID == scheduleID && !string.IsNullOrEmpty(c.ExprPrice) && (c.AssetID > 0 || c.DriverID > 0 || c.IsAssistant.HasValue)).ToList();
                var lstAsset = model.FLM_ScheduleFee.Where(c => c.ScheduleID == scheduleID && !string.IsNullOrEmpty(c.ExprPrice) && c.AssetID > 0).Select(c => new { c.AssetID, c.FLM_Asset.VehicleID }).Distinct().ToList();
                if (lstScheduleFee.Count() > 0)
                {
                    var lstMaster = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID > 0 && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new HelperFinance_TOMaster
                    {
                        ID = c.ID,
                        ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                        DateConfig = c.DateConfig.Value,
                        VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                        VehicleID = c.VehicleID,
                        KM = c.KM > 0 ? c.KM : 0,
                        DriverID1 = c.DriverID1,
                        DriverID2 = c.DriverID2,
                        DriverID3 = c.DriverID3,
                        DriverID4 = c.DriverID4,
                        DriverID5 = c.DriverID5
                    }).ToList();

                    var lstMasterCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID > 0 && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new HelperFinance_TOMaster
                    {
                        ID = c.ID,
                        ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                        DateConfig = c.DateConfig.Value,
                        VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                        VehicleID = c.VehicleID,
                        KM = c.KM > 0 ? c.KM : 0,
                        DriverID1 = c.DriverID1,
                        DriverID2 = c.DriverID2,
                        DriverID3 = c.DriverID3,
                        DriverID4 = c.DriverID4,
                        DriverID5 = c.DriverID5
                    }).ToList();

                    var lstDriverFee = model.FLM_ScheduleDriver.Where(c => c.ScheduleID == scheduleID).Select(c => new
                    {
                        c.FeeBase,
                        c.DriverID,
                        c.IsAssistant,
                        c.DaysAllowOff
                    }).ToList();

                    var lstFixCost = model.FLM_FixedCost.Where(c => c.Month == schedule.Month && c.Year == schedule.Year && c.IsClosed && c.Value > 0 && c.ReceiptID == null).Select(c => new
                    {
                        c.FLM_Asset.VehicleID,
                        c.Value,
                    }).ToList();

                    foreach (var itemScheduleFee in lstScheduleFee)
                    {
                        if (itemScheduleFee.AssetID > 0)
                        {
                            HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                            itemExpr.TotalDay = schedule.TotalDays;
                            itemExpr.TotalDayOn = totalDayOn;
                            itemExpr.TotalDayOff = totalDayOff;
                            itemExpr.TotalDayHoliday = totalDayHoliday;
                            var asset = lstAsset.FirstOrDefault(c => c.AssetID == itemScheduleFee.AssetID);
                            if (asset != null)
                            {
                                itemExpr.TotalSchedule = lstMaster.Count(c => c.VehicleID == asset.VehicleID);
                                itemExpr.TotalSchedule += lstMasterCO.Count(c => c.VehicleID == asset.VehicleID);
                                var lstFixCostAsset = lstFixCost.Where(c => c.VehicleID == asset.VehicleID);
                                if (lstFixCostAsset.Count() > 0)
                                    itemExpr.Price = lstFixCostAsset.Sum(c => c.Value);

                                itemScheduleFee.Price = Expression_FLMGetValue(itemExpr, itemScheduleFee.ExprPrice);
                            }
                        }

                        if (itemScheduleFee.DriverID.HasValue)
                        {
                            HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                            itemExpr.TotalDay = schedule.TotalDays;
                            itemExpr.TotalDayOn = totalDayOn;
                            itemExpr.TotalDayOff = totalDayOff;
                            itemExpr.TotalDayHoliday = totalDayHoliday;
                            itemExpr.TotalSchedule = lstMaster.Count(c => (c.DriverID1 == itemScheduleFee.DriverID || c.DriverID2 == itemScheduleFee.DriverID || c.DriverID3 == itemScheduleFee.DriverID || c.DriverID4 == itemScheduleFee.DriverID || c.DriverID5 == itemScheduleFee.DriverID));
                            itemExpr.TotalSchedule += lstMasterCO.Count(c => (c.DriverID1 == itemScheduleFee.DriverID || c.DriverID2 == itemScheduleFee.DriverID || c.DriverID3 == itemScheduleFee.DriverID || c.DriverID4 == itemScheduleFee.DriverID || c.DriverID5 == itemScheduleFee.DriverID));
                            var driverFee = lstDriverFee.FirstOrDefault(c => c.DriverID == itemScheduleFee.DriverID);
                            if (driverFee != null)
                            {
                                itemExpr.FeeBase = driverFee.FeeBase;
                                itemExpr.TotalDayOffDriver = driverFee.DaysAllowOff;
                            }
                            itemScheduleFee.Price = Expression_FLMGetValue(itemExpr, itemScheduleFee.ExprPrice);
                        }

                        if (itemScheduleFee.IsAssistant.HasValue)
                        {
                            var defDriver = lstDriverFee.FirstOrDefault(c => c.IsAssistant == itemScheduleFee.IsAssistant.Value);
                            if (defDriver != null)
                            {
                                HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                itemExpr.TotalDay = schedule.TotalDays;
                                itemExpr.TotalDayOn = totalDayOn;
                                itemExpr.TotalDayOff = totalDayOff;
                                itemExpr.TotalDayHoliday = totalDayHoliday;
                                itemExpr.TotalSchedule = lstMaster.Count(c => (c.DriverID1 == defDriver.DriverID || c.DriverID2 == defDriver.DriverID || c.DriverID3 == defDriver.DriverID || c.DriverID4 == defDriver.DriverID || c.DriverID5 == defDriver.DriverID));
                                itemExpr.TotalSchedule += lstMasterCO.Count(c => (c.DriverID1 == defDriver.DriverID || c.DriverID2 == defDriver.DriverID || c.DriverID3 == defDriver.DriverID || c.DriverID4 == defDriver.DriverID || c.DriverID5 == defDriver.DriverID));
                                itemExpr.FeeBase = defDriver.FeeBase;

                                itemScheduleFee.Price = Expression_FLMGetValue(itemExpr, itemScheduleFee.ExprPrice);
                            }
                        }
                    }
                }
            }
        }

        // Tính lương chuyến cho tài xế
        public static void Truck_CalculateDriver(DataEntities model, AccountItem Account, int scheduleID)
        {
            const int iTon = -(int)SYSVarType.PriceOfGOPTon;
            const int iCBM = -(int)SYSVarType.PriceOfGOPCBM;
            const int iQuantity = -(int)SYSVarType.PriceOfGOPTU;
            var lstStatusCon = new List<int>();
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerEXEmpty);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerEXLaden);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerIMLaden);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerIMEmpty);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerLOGetEmpty);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerLOLaden);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerLOReturnEmpty);

            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);

            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);
            // Lấy ds Vendor
            List<int> lstVendorID = new List<int>();
            lstVendorID.Add(Account.SYSCustomerID);

            //Lấy danh sách ID hợp đồng chính của LTL, FTL
            var lstContractID = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (lstVendorID.Contains(c.CustomerID.Value) || c.CustomerID == null) &&
                c.EffectDate <= DateConfig && (c.ExpiredDate == null || c.ExpiredDate > DateConfig) && (c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFTL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLTL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFCL))
                .Select(c => c.ID).ToArray();

            #region Lấy giá
            HelperFinance_PriceInput result = new HelperFinance_PriceInput();
            result.ListContract = new List<HelperFinance_Contract>();
            result.ListContractTerm = new List<HelperFinance_ContractTerm>();
            result.ListContainerPrice = new List<HelperFinance_PriceContainer>();
            result.ListContainerService = new List<HelperFinance_PriceService>();
            result.ListServiceOfOrder = new List<CATServiceOfOrder>();
            result.ListContractID = new List<int>();
            result.ListContractID.AddRange(lstContractID);
            result.ListContractTerm = model.CAT_ContractTerm.Where(c => result.ListContractID.Contains(c.ContractID) && c.DateEffect <= DateConfig && (c.DateExpire == null || c.DateExpire >= DateConfig)).Select(c => new HelperFinance_ContractTerm
            {
                ContractID = c.ContractID,
                ContractTermID = c.ID,
                DateEffect = c.DateEffect,
                DateExpire = c.DateExpire,
                DatePrice = c.DatePrice,
                ExprDatePrice = c.ExprDatePrice,
                ExprInput = c.ExprInput,
                ExprPrice = c.ExprPrice,
                IsAllRouting = c.IsAllRouting,
                MaterialID = c.MaterialID,
            }).ToList();

            result.ListServiceOfOrder = model.CAT_ServiceOfOrder.Select(c => new CATServiceOfOrder
            {
                ID = c.ID,
                ServiceOfOrderID = c.ServiceOfOrderID
            }).ToList();

            foreach (var itemContractTerm in result.ListContractTerm)
            {
                result.ListContainerPrice.AddRange(model.CAT_PriceCOContainer.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceContainer
                {
                    ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                    EffectDate = c.CAT_Price.EffectDate,
                    PackingID = c.PackingID,
                    Price = c.Price,
                    PriceID = c.PriceID,
                    RoutingID = c.CAT_ContractRouting.RoutingID,
                    LocationFromID = c.CAT_ContractRouting.CAT_Routing.LocationFromID,
                    LocationToID = c.CAT_ContractRouting.CAT_Routing.LocationToID,
                    AreaFromID = c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID,
                    AreaToID = c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID,
                }).ToList());

                foreach (var item in result.ListContainerPrice)
                {
                    item.ListAreaFromID = new List<int>();
                    item.ListAreaToID = new List<int>();
                    if (item.AreaFromID.HasValue && item.AreaToID.HasValue)
                    {
                        item.ListAreaFromID = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == item.AreaFromID).Select(c => c.LocationID).Distinct().ToList();
                        item.ListAreaToID = model.CAT_RoutingAreaLocation.Where(c => c.RoutingAreaID == item.AreaToID).Select(c => c.LocationID).Distinct().ToList();
                    }
                }

                result.ListContainerService.AddRange(model.CAT_PriceCOService.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceService
                {
                    ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                    EffectDate = c.CAT_Price.EffectDate,
                    PriceID = c.PriceID,
                    PackingID = c.PackingID,
                    Price = c.Price,
                    ServiceID = c.ServiceID,
                    CurrencyID = c.CurrencyID
                }).ToList());
            }

            #endregion

            #region Tính theo chuyến
            //Lấy các chuyến
            var lstMaster = model.OPS_DITOMaster.Where(c => c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.ContractID > 0 && lstContractID.Contains(c.ContractID.Value)).Select(c => new HelperFinance_TOMaster
            {
                ID = c.ID,
                GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                DateConfig = c.DateConfig.Value,
                VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                VehicleID = c.VehicleID,
                DriverID1 = c.DriverID1,
                DriverID2 = c.DriverID2,
                DriverID3 = c.DriverID3,
                TypeOfDriverID1 = c.TypeOfDriverID1,
                TypeOfDriverID2 = c.TypeOfDriverID2,
                TypeOfDriverID3 = c.TypeOfDriverID3,
                ExIsOverNight = c.ExIsOverNight,
                ExIsOverWeight = c.ExIsOverWeight,
                ExTotalDayOut = c.ExTotalDayOut,
                ExTotalJoin = c.ExTotalJoin,
                ETD = c.ETD,
                SortOrder = c.SortOrder,
                VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                GOVCode = c.VehicleID > 0 && c.CAT_Vehicle.GroupOfVehicleID > 0 ? c.CAT_Vehicle.CAT_GroupOfVehicle.Code : "",
                MaxWeightCal = c.VehicleID > 0 && c.CAT_Vehicle.MaxWeight > 0 ? c.CAT_Vehicle.MaxWeight.Value : 0,
            }).ToList();
            //Lấy các ops nhóm hàng
            var lstOPSGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.DITOMasterID > 0 && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.OPS_DITOMaster.ContractID > 0 && lstContractID.Contains(c.OPS_DITOMaster.ContractID.Value)).Select(c => new HelperFinance_OPSGroupProduct
            {
                ID = c.ID,
                OrderID = c.ORD_GroupProduct.OrderID,
                DITOMasterID = c.DITOMasterID.Value,
                VehicleID = c.OPS_DITOMaster.VehicleID,
                OrderGroupProductID = c.OrderGroupProductID.Value,
                GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID > 0 ? c.ORD_GroupProduct.GroupOfProductID.Value : -1,
                ContractID = c.OPS_DITOMaster.ContractID > 0 ? c.OPS_DITOMaster.ContractID.Value : -1,
                CUSRoutingID = c.CUSRoutingID > 0 ? c.CUSRoutingID.Value : -1,
                CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                CATRoutingName = c.CUSRoutingID > 0 ? c.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                OrderRoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                LocationToName = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location,
                GroupOfLocationID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.GroupOfLocationID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.GroupOfLocationID.Value : -1,
                TonOrder = c.ORD_GroupProduct.Ton,
                CBMOrder = c.ORD_GroupProduct.CBM,
                QuantityOrder = c.ORD_GroupProduct.Quantity,
                TonTranfer = c.TonTranfer,
                CBMTranfer = c.CBMTranfer,
                QuantityTranfer = c.QuantityTranfer,
                TonReturn = c.TonReturn,
                CBMReturn = c.CBMReturn,
                QuantityReturn = c.QuantityReturn,
                PriceOfGOPID = c.ORD_GroupProduct.PriceOfGOPID,
                PriceOfGOPName = c.ORD_GroupProduct.PriceOfGOPID.HasValue ? c.ORD_GroupProduct.SYS_Var.ValueOfVar : string.Empty,
                GroupOfVehicleID = c.OPS_DITOMaster.GroupOfVehicleID > 0 ? c.OPS_DITOMaster.GroupOfVehicleID.Value : -1,
                GroupOfVehicleCode = c.OPS_DITOMaster.GroupOfVehicleID > 0 ? c.OPS_DITOMaster.CAT_GroupOfVehicle.Code : string.Empty,
                CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                VendorID = c.OPS_DITOMaster.VendorOfVehicleID.HasValue ? c.OPS_DITOMaster.VendorOfVehicleID.Value : Account.SYSCustomerID,
                OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                DriverID1 = c.OPS_DITOMaster.DriverID1,
                DriverID2 = c.OPS_DITOMaster.DriverID2,
                DriverID3 = c.OPS_DITOMaster.DriverID3,
                TypeOfDriverID1 = c.OPS_DITOMaster.TypeOfDriverID1,
                TypeOfDriverID2 = c.OPS_DITOMaster.TypeOfDriverID2,
                TypeOfDriverID3 = c.OPS_DITOMaster.TypeOfDriverID3,
                ETD = c.OPS_DITOMaster.ETD,
                SortOrder = c.OPS_DITOMaster.SortOrder,
                VehicleCode = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                GOVCode = c.DITOMasterID > 0 && c.OPS_DITOMaster.VehicleID > 0 && c.OPS_DITOMaster.CAT_Vehicle.GroupOfVehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.CAT_GroupOfVehicle.Code : "",
                MaxWeightCal = c.DITOMasterID > 0 && c.OPS_DITOMaster.VehicleID > 0 && c.OPS_DITOMaster.CAT_Vehicle.MaxWeight > 0 ? c.OPS_DITOMaster.CAT_Vehicle.MaxWeight.Value : 0,
            }).ToList();

            // Container
            var lstMasterCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.ContractID > 0).Select(c => new HelperFinance_COMaster
            {
                ID = c.ID,
                GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                CATRoutingID = c.RoutingID > 0 ? c.RoutingID.Value : -1,
                ParentRoutingID = c.RoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                DateConfig = c.DateConfig.Value,
                VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                VehicleID = c.VehicleID,
                DriverID1 = c.DriverID1,
                DriverID2 = c.DriverID2,
                DriverID3 = c.DriverID3,
                TypeOfDriverID1 = c.TypeOfDriverID1,
                TypeOfDriverID2 = c.TypeOfDriverID2,
                TypeOfDriverID3 = c.TypeOfDriverID3,
                ExIsOverNight = c.ExIsOverNight,
                ExIsOverWeight = c.ExIsOverWeight,
                ExTotalDayOut = c.ExTotalDayOut,
                ExTotalJoin = c.ExTotalJoin,
                ETD = c.ETD,
                SortOrder = c.SortOrder,
                KM = c.KM,
                VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                GOVCode = c.VehicleID > 0 && c.CAT_Vehicle.GroupOfVehicleID > 0 ? c.CAT_Vehicle.CAT_GroupOfVehicle.Code : "",
                MaxWeightCal = c.VehicleID > 0 && c.CAT_Vehicle.MaxWeight > 0 ? c.CAT_Vehicle.MaxWeight.Value : 0,
            }).ToList();

            var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && (c.OPS_COTOMaster.VendorOfVehicleID == null || c.OPS_COTOMaster.VendorOfVehicleID == Account.SYSCustomerID) && c.OPS_COTOMaster.DateConfig.HasValue && c.OPS_COTOMaster.DateConfig >= DateConfig && c.OPS_COTOMaster.DateConfig < DateConfigEnd && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.OPS_COTOMaster.ContractID > 0 && c.ParentID == null && lstStatusCon.Contains(c.StatusOfCOContainerID)).Select(c => new HelperFinance_Container
            {
                ID = c.ID,
                OrderContainerID = c.OPS_Container.ContainerID,
                PackingID = c.OPS_Container.ORD_Container.PackingID,
                PackingCode = c.OPS_Container.ORD_Container.CAT_Packing.Code,
                COTOMasterID = c.COTOMasterID.Value,
                CUSRoutingID = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUSRoutingID.Value : -1,
                CATRoutingID = c.OPS_COTOMaster.RoutingID > 0 ? c.OPS_COTOMaster.RoutingID.Value : -1,
                CATRoutingName = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                OrderRoutingID = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUSRoutingID.Value : -1,
                ParentRoutingID = c.OPS_COTOMaster.RoutingID > 0 && c.OPS_COTOMaster.CAT_Routing.ParentID > 0 ? c.OPS_COTOMaster.CAT_Routing.ParentID.Value : -1,
                LocationFromID = c.OPS_Container.ORD_Container.CUS_Location2.LocationID,
                LocationToID = c.OPS_Container.ORD_Container.CUS_Location3.LocationID,
                LocationToName = c.CAT_Location.Location,
                GroupOfLocationID = c.CAT_Location.GroupOfLocationID > 0 ? c.CAT_Location.GroupOfLocationID.Value : -1,
                ContractID = c.OPS_COTOMaster.ContractID.Value,
                DriverID1 = c.OPS_COTOMaster.DriverID1,
                DriverID2 = c.OPS_COTOMaster.DriverID2,
                DriverID3 = c.OPS_COTOMaster.DriverID3,
                TypeOfDriverID1 = c.OPS_COTOMaster.TypeOfDriverID1,
                TypeOfDriverID2 = c.OPS_COTOMaster.TypeOfDriverID2,
                TypeOfDriverID3 = c.OPS_COTOMaster.TypeOfDriverID3,
                CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                ETD = c.OPS_COTOMaster.ETD,
                VendorID = c.OPS_COTOMaster.VendorOfVehicleID == null ? Account.SYSCustomerID : c.OPS_COTOMaster.VendorOfVehicleID.Value,
                SortOrder = c.OPS_COTOMaster.SortOrder,
                StatusOfCOContainerID = c.StatusOfCOContainerID,
                ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID.HasValue ? c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID.Value : -1,
                VehicleCode = c.COTOMasterID > 0 && c.OPS_COTOMaster.VehicleID > 0 ? c.OPS_COTOMaster.CAT_Vehicle.RegNo : "",
                GOVCode = c.COTOMasterID > 0 && c.OPS_COTOMaster.VehicleID > 0 && c.OPS_COTOMaster.CAT_Vehicle.GroupOfVehicleID > 0 ? c.OPS_COTOMaster.CAT_Vehicle.CAT_GroupOfVehicle.Code : "",
                MaxWeightCal = c.COTOMasterID > 0 && c.OPS_COTOMaster.VehicleID > 0 && c.OPS_COTOMaster.CAT_Vehicle.MaxWeight > 0 ? c.OPS_COTOMaster.CAT_Vehicle.MaxWeight.Value : 0,
            }).ToList();

            var lstOPSCOTO = model.OPS_COTO.Where(c => c.COTOMasterID > 0 && c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && (c.OPS_COTOMaster.VendorOfVehicleID == null || c.OPS_COTOMaster.VendorOfVehicleID == Account.SYSCustomerID) && c.OPS_COTOMaster.DateConfig.HasValue && c.OPS_COTOMaster.DateConfig >= DateConfig && c.OPS_COTOMaster.DateConfig < DateConfig && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.OPS_COTOMaster.ContractID > 0).Select(c => new HelperFinance_COTO
            {
                ID = c.ID,
                COTOMasterID = c.COTOMasterID,
                Ton = c.Ton > 0 ? c.Ton.Value : 0
            }).ToList();

            // Danh sách chuyến còn thiếu trong list Master
            var lstOPSMasterID = lstOPSGroupProduct.Select(c => c.DITOMasterID).Distinct().ToList();
            var lstOPSMasterCurrentID = lstMaster.Select(c => c.ID).Distinct().ToList();
            var lstOPSMasterNotInID = lstOPSMasterID.Where(c => !lstOPSMasterCurrentID.Contains(c)).ToList();
            lstMaster.AddRange(model.OPS_DITOMaster.Where(c => lstOPSMasterNotInID.Contains(c.ID)).Select(c => new HelperFinance_TOMaster
            {
                ID = c.ID,
                GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                DateConfig = c.DateConfig.Value,
                VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                VehicleID = c.VehicleID,
                DriverID1 = c.DriverID1,
                DriverID2 = c.DriverID2,
                DriverID3 = c.DriverID3,
                TypeOfDriverID1 = c.TypeOfDriverID1,
                TypeOfDriverID2 = c.TypeOfDriverID2,
                TypeOfDriverID3 = c.TypeOfDriverID3,
                ExIsOverNight = c.ExIsOverNight,
                ExIsOverWeight = c.ExIsOverWeight,
                ExTotalDayOut = c.ExTotalDayOut,
                ExTotalJoin = c.ExTotalJoin,
                ETD = c.ETD,
                SortOrder = c.SortOrder,
                VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                GOVCode = c.VehicleID > 0 && c.CAT_Vehicle.GroupOfVehicleID > 0 ? c.CAT_Vehicle.CAT_GroupOfVehicle.Code : "",
                MaxWeightCal = c.VehicleID > 0 && c.CAT_Vehicle.MaxWeight > 0 ? c.CAT_Vehicle.MaxWeight.Value : 0,
            }).ToList());

            var lstContractMasterID = lstMaster.Select(c => c.ContractID).Distinct().ToList();
            lstContractMasterID.AddRange(lstMasterCO.Select(c => c.ContractID).Distinct().ToList());
            if (lstContractMasterID.Count > 0 && lstContractMasterID.Count > 0)
            {
                lstContractID = lstContractID.Intersect(lstContractMasterID).ToArray();
                var lstContract = model.CAT_Contract.Where(c => lstContractID.Contains(c.ID)).Select(c => new HelperFinance_Contract
                {
                    ID = c.ID,
                    VendorID = c.CustomerID.HasValue ? c.CustomerID.Value : Account.SYSCustomerID,
                    TransportModeID = c.CAT_TransportMode.TransportModeID,
                    PriceInDay = c.PriceInDay,
                    CustomerID = c.CompanyID > 0 ? c.CUS_Company.CustomerRelateID : -1,
                    ExprFCLAllocationPrice = c.ExprFCLAllocationPrice
                }).ToList();
                if (lstContract.Count > 0)
                {
                    List<FIN_PL> lstPl = new List<FIN_PL>();
                    List<FIN_Temp> lstPlTemp = new List<FIN_Temp>();
                    var lstPLTempQuery = model.FIN_Temp.Where(c => c.ScheduleID == scheduleID && c.DITOGroupProductID.HasValue);
                    var lstPLTempQueryCO = model.FIN_Temp.Where(c => c.ScheduleID == scheduleID && c.COTOContainerID.HasValue);

                    #region Qui đổi
                    var lstGroupProductChange = model.CAT_ContractGroupOfProduct.Where(c => lstContractID.Contains(c.ContractID)).Select(c => new { c.GroupOfProductID, c.CUS_GroupOfProduct.PriceOfGOPID, c.Expression, c.ContractID, VendorID = c.CAT_Contract.CustomerID }).ToList();
                    //Thực hiện qui đổi
                    foreach (var itemChange in lstGroupProductChange)
                    {
                        foreach (var item in lstOPSGroupProduct.Where(c => c.GroupOfProductID == itemChange.GroupOfProductID && c.ContractID == itemChange.ContractID))
                        {
                            //Qui đổi transfer
                            DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                            itemTransfer.Ton = item.TonTranfer;
                            itemTransfer.CBM = item.CBMTranfer;
                            itemTransfer.Quantity = item.QuantityTranfer;
                            itemTransfer.Expression = itemChange.Expression;
                            double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                            if (quantityTransfer.HasValue)
                            {
                                if (item.PriceOfGOPID.HasValue)
                                {
                                    if (item.PriceOfGOPID == iTon)
                                        item.TonTranfer = quantityTransfer.Value;
                                    else
                                        if (item.PriceOfGOPID == iCBM)
                                            item.CBMTranfer = quantityTransfer.Value;
                                        else
                                            if (item.PriceOfGOPID == iQuantity)
                                                item.QuantityTranfer = quantityTransfer.Value;
                                }
                            }
                            //Qui đổi return
                            DTOCATContractGroupOfProduct itemReturn = new DTOCATContractGroupOfProduct();
                            itemReturn.Ton = item.TonReturn;
                            itemReturn.CBM = item.CBMReturn;
                            itemReturn.Quantity = item.QuantityReturn;
                            itemReturn.Expression = itemChange.Expression;
                            double? quantityReturn = GetGroupOfProductTransfer(itemReturn);
                            if (quantityReturn.HasValue)
                            {
                                if (item.PriceOfGOPID.HasValue)
                                {
                                    if (item.PriceOfGOPID == iTon)
                                        item.TonReturn = quantityReturn.Value;
                                    else
                                        if (item.PriceOfGOPID == iCBM)
                                            item.CBMReturn = quantityReturn.Value;
                                        else
                                            if (item.PriceOfGOPID == iQuantity)
                                                item.QuantityReturn = quantityReturn.Value;
                                }
                            }
                        }
                    }
                    #endregion

                    #region Tổng chuyến
                    foreach (var item in lstMaster)
                    {
                        var queryOPSGroupProduct = lstOPSGroupProduct.Where(c => c.DITOMasterID == item.ID);
                        if (queryOPSGroupProduct.Count() > 0)
                        {
                            item.GetPoint = queryOPSGroupProduct.Select(c => c.LocationFromID).Distinct().Count();
                            item.DropPoint = queryOPSGroupProduct.Select(c => c.LocationToID).Distinct().Count();
                            item.TonTranfer = queryOPSGroupProduct.Sum(c => c.TonTranfer);
                            item.CBMTranfer = queryOPSGroupProduct.Sum(c => c.CBMTranfer);
                            item.QuantityTranfer = queryOPSGroupProduct.Sum(c => c.QuantityTranfer);
                            item.TonReturn = queryOPSGroupProduct.Sum(c => c.TonReturn);
                            item.CBMReturn = queryOPSGroupProduct.Sum(c => c.CBMReturn);
                            item.QuantityReturn = queryOPSGroupProduct.Sum(c => c.QuantityReturn);
                            var queryOPSGroupProductTemp = queryOPSGroupProduct.Select(c => new { c.OrderGroupProductID, c.TonOrder, c.CBMOrder, c.QuantityOrder }).Distinct().ToList();
                            item.TonOrder = queryOPSGroupProductTemp.Sum(c => c.TonOrder);
                            item.CBMOrder = queryOPSGroupProductTemp.Sum(c => c.CBMOrder);
                            item.QuantityOrder = queryOPSGroupProductTemp.Sum(c => c.QuantityOrder);
                        }
                    }
                    #endregion

                    //Chạy từng hợp đồng
                    foreach (var itemContract in lstContract)
                    {
                        itemContract.ListContractSetting = new List<DTOCATContract_Setting>();
                        if (!string.IsNullOrEmpty(itemContract.ExprFCLAllocationPrice))
                        {
                            try
                            {
                                itemContract.ListContractSetting = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOCATContract_Setting>>(itemContract.ExprFCLAllocationPrice);
                            }
                            catch { itemContract.ListContractSetting = new List<DTOCATContract_Setting>(); }
                        }
                        if (itemContract.ListContractSetting.Count == 0)
                        {
                            foreach (var item in result.ListServiceOfOrder)
                            {
                                DTOCATContract_Setting objConSet = new DTOCATContract_Setting();
                                objConSet.GetEmpty = 0;
                                objConSet.Laden = 100;
                                objConSet.ReturnEmpty = 0;
                                objConSet.ServiceOfOrderID = item.ID;
                                itemContract.ListContractSetting.Add(objConSet);
                            }
                        }

                        if (itemContract.CustomerID < 1) itemContract.CustomerID = null;

                        System.Diagnostics.Debug.WriteLine("Contract start: " + itemContract.ID);
                        var queryMasterContract = lstMaster.Where(c => c.ContractID == itemContract.ID);
                        var queryOPSGroupProductContract = lstOPSGroupProduct.Where(c => c.ContractID == itemContract.ID);

                        var queryMasterContractCO = lstMasterCO.Where(c => c.ContractID == itemContract.ID);
                        var queryOPSContainerContract = lstOPSContainer.Where(c => c.ContractID == itemContract.ID);

                        if (queryMasterContract.Count() > 0 || queryMasterContractCO.Count() > 0)
                        {
                            #region Tính giá từng chặng cho Cont
                            foreach (var itemOPSContainer in queryOPSContainerContract)
                            {
                                var priceCO = result.ListContainerPrice.Where(c => c.ContractID == itemContract.ID && c.PackingID == itemOPSContainer.PackingID && (c.RoutingID == itemOPSContainer.CATRoutingID || (c.LocationFromID == itemOPSContainer.LocationFromID && c.LocationToID == itemOPSContainer.LocationToID)) && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                                if (priceCO == null)
                                    priceCO = result.ListContainerPrice.Where(c => c.ContractID == itemContract.ID && c.PackingID == itemOPSContainer.PackingID && c.LocationFromID.HasValue && c.LocationToID.HasValue && c.ListAreaFromID.Contains(c.LocationFromID.Value) && c.ListAreaToID.Contains(c.LocationToID.Value) && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                                if (priceCO != null)
                                    itemOPSContainer.UnitPrice = priceCO.Price;
                            }
                            #endregion

                            #region Tính lương chuyến
                            var lstDriverFee = model.CAT_DriverFee.Where(c => c.ContractID == itemContract.ID && (c.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumSchedule || c.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumTotal)).Select(c => new
                            {
                                c.ID,
                                c.TypeOfDriverFeeID,
                                c.TypeOfDriverID,
                                c.FeeName,
                                c.ExprInput,
                                c.ExprPriceFix,
                                c.ExprQuantity,
                                c.ExprUnitPrice,
                                c.DriverFeeSumID,
                                TypeOfDriverFeeCode = c.CAT_TypeOfDriverFee.Code,
                                TypeOfDriverFeeName = c.CAT_TypeOfDriverFee.TypeName,
                                c.SortOrder
                            }).ToList();

                            foreach (var driverFee in lstDriverFee)
                            {
                                System.Diagnostics.Debug.WriteLine("Lương tài xế: " + driverFee.FeeName + " ID: " + driverFee.ID);

                                var lstMasterDriverFee = new List<HelperFinance_TOMaster>();
                                var lstOPSGroupDriverFee = new List<HelperFinance_OPSGroupProduct>();
                                var queryMasterDriverFee = queryMasterContract == null ? new List<HelperFinance_TOMaster>() : queryMasterContract.Where(c => (c.DriverID1.HasValue && c.TypeOfDriverID1 == driverFee.TypeOfDriverID) || (c.DriverID2.HasValue && c.TypeOfDriverID2 == driverFee.TypeOfDriverID) || (c.DriverID3.HasValue && c.TypeOfDriverID3 == driverFee.TypeOfDriverID) || (driverFee.TypeOfDriverID == null && (c.DriverID1.HasValue || c.DriverID2.HasValue || c.DriverID3.HasValue))).ToList();
                                var queryOPSGroupDriverFee = queryOPSGroupProductContract == null ? new List<HelperFinance_OPSGroupProduct>() : queryOPSGroupProductContract.Where(c => (c.DriverID1.HasValue && c.TypeOfDriverID1 == driverFee.TypeOfDriverID) || (c.DriverID2.HasValue && c.TypeOfDriverID2 == driverFee.TypeOfDriverID) || (c.DriverID3.HasValue && c.TypeOfDriverID3 == driverFee.TypeOfDriverID) || (driverFee.TypeOfDriverID == null && (c.DriverID1.HasValue || c.DriverID2.HasValue || c.DriverID3.HasValue))).ToList();

                                var lstMasterCODriverFee = new List<HelperFinance_COMaster>();
                                var lstOPSContainerDriverFee = new List<HelperFinance_Container>();
                                var queryMasterCODriverFee = queryMasterContractCO == null ? new List<HelperFinance_COMaster>() : queryMasterContractCO.Where(c => (c.DriverID1.HasValue && c.TypeOfDriverID1 == driverFee.TypeOfDriverID) || (c.DriverID2.HasValue && c.TypeOfDriverID2 == driverFee.TypeOfDriverID) || (c.DriverID3.HasValue && c.TypeOfDriverID3 == driverFee.TypeOfDriverID) || (driverFee.TypeOfDriverID == null && (c.DriverID1.HasValue || c.DriverID2.HasValue || c.DriverID3.HasValue))).ToList();
                                var queryOPSContainerDriverFee = queryOPSContainerContract == null ? new List<HelperFinance_Container>() : queryOPSContainerContract.Where(c => (c.DriverID1.HasValue && c.TypeOfDriverID1 == driverFee.TypeOfDriverID) || (c.DriverID2.HasValue && c.TypeOfDriverID2 == driverFee.TypeOfDriverID) || (c.DriverID3.HasValue && c.TypeOfDriverID3 == driverFee.TypeOfDriverID) || (driverFee.TypeOfDriverID == null && (c.DriverID1.HasValue || c.DriverID2.HasValue || c.DriverID3.HasValue))).ToList();

                                if (driverFee.SortOrder > 0)
                                {
                                    queryOPSGroupDriverFee = queryOPSGroupDriverFee.Where(c => c.SortOrder == driverFee.SortOrder).ToList();
                                    queryOPSContainerDriverFee = queryOPSContainerDriverFee.Where(c => c.SortOrder == driverFee.SortOrder).ToList();
                                }

                                //Danh sách các điều kiện lọc 
                                var lstDriverFeeCustomer = model.CAT_DriverFeeCustomer.Where(c => c.DriverFeeID == driverFee.ID && c.CustomerID > 0)
                                   .Select(c => c.CustomerID).ToList();
                                var lstDriverFeeParentRouting = model.CAT_DriverFeeRouting.Where(c => c.DriverFeeID == driverFee.ID && c.ParentRoutingID > 0)
                                    .Select(c => c.ParentRoutingID.Value).ToList();
                                var lstDriverFeeRouting = model.CAT_DriverFeeRouting.Where(c => c.DriverFeeID == driverFee.ID && c.RoutingID > 0)
                                    .Select(c => c.RoutingID.Value).ToList();
                                var lstDriverFeeGroupLocation = model.CAT_DriverFeeGroupLocation.Where(c => c.DriverFeeID == driverFee.ID)
                                    .Select(c => c.GroupOfLocationID).ToList();
                                var lstDriverFeeLocationFrom = model.CAT_DriverFeeRouting.Where(c => c.DriverFeeID == driverFee.ID && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                                    .Select(c => c.LocationID.Value).ToList();
                                var lstDriverFeeLocationTo = model.CAT_DriverFeeRouting.Where(c => c.DriverFeeID == driverFee.ID && (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                                    .Select(c => c.LocationID.Value).ToList();
                                var lstDriverFeeGroupProduct = model.CAT_DriverFeeGroupProduct.Where(c => c.DriverFeeID == driverFee.ID && c.GroupOfProductID > 0)
                                   .Select(c => c.GroupOfProductID).ToList();
                                var lstDriverFeePacking = model.CAT_DriverFeePacking.Where(c => c.DriverFeeID == driverFee.ID && c.PackingID > 0)
                                    .Select(c => c.PackingID).ToList();


                                if (lstDriverFeeCustomer.Count > 0)
                                {
                                    queryOPSGroupDriverFee = queryOPSGroupDriverFee.Where(c => lstDriverFeeCustomer.Contains(c.CustomerID)).ToList();
                                    queryOPSContainerDriverFee = queryOPSContainerDriverFee.Where(c => lstDriverFeeCustomer.Contains(c.CustomerID)).ToList();
                                }
                                if (lstDriverFeeParentRouting.Count > 0)
                                {
                                    queryOPSGroupDriverFee = queryOPSGroupDriverFee.Where(c => lstDriverFeeParentRouting.Contains(c.ParentRoutingID)).ToList();
                                    queryOPSContainerDriverFee = queryOPSContainerDriverFee.Where(c => lstDriverFeeParentRouting.Contains(c.ParentRoutingID)).ToList();
                                }
                                if (lstDriverFeeRouting.Count > 0)
                                {
                                    queryOPSGroupDriverFee = queryOPSGroupDriverFee.Where(c => lstDriverFeeRouting.Contains(c.CATRoutingID)).ToList();
                                    queryOPSContainerDriverFee = queryOPSContainerDriverFee.Where(c => lstDriverFeeRouting.Contains(c.CATRoutingID)).ToList();
                                }
                                if (lstDriverFeeGroupLocation.Count > 0)
                                {
                                    queryOPSGroupDriverFee = queryOPSGroupDriverFee.Where(c => lstDriverFeeGroupLocation.Contains(c.GroupOfLocationID)).ToList();
                                    queryOPSContainerDriverFee = queryOPSContainerDriverFee.Where(c => lstDriverFeeGroupLocation.Contains(c.GroupOfLocationID)).ToList();
                                }
                                if (lstDriverFeeLocationFrom.Count > 0)
                                {
                                    queryOPSGroupDriverFee = queryOPSGroupDriverFee.Where(c => lstDriverFeeLocationFrom.Contains(c.LocationFromID)).ToList();
                                    queryOPSContainerDriverFee = queryOPSContainerDriverFee.Where(c => lstDriverFeeLocationFrom.Contains(c.LocationFromID)).ToList();
                                }
                                if (lstDriverFeeLocationTo.Count > 0)
                                {
                                    queryOPSGroupDriverFee = queryOPSGroupDriverFee.Where(c => lstDriverFeeLocationTo.Contains(c.LocationToID)).ToList();
                                    queryOPSContainerDriverFee = queryOPSContainerDriverFee.Where(c => lstDriverFeeLocationTo.Contains(c.LocationToID)).ToList();
                                }
                                if (lstDriverFeeGroupProduct.Count > 0)
                                {
                                    queryOPSGroupDriverFee = queryOPSGroupDriverFee.Where(c => lstDriverFeeGroupProduct.Contains(c.GroupOfProductID)).ToList();
                                }
                                if (lstDriverFeePacking.Count > 0)
                                {
                                    queryOPSContainerDriverFee = queryOPSContainerDriverFee.Where(c => lstDriverFeePacking.Contains(c.PackingID)).ToList();
                                }

                                // Xe tải
                                var lstMasterCheckID = queryOPSGroupDriverFee.Select(c => c.DITOMasterID).Distinct().ToArray();
                                var lstMasterCheck = queryMasterDriverFee.Where(c => lstMasterCheckID.Contains(c.ID)).ToList();
                                var lstMasterGroupCheck = queryOPSGroupDriverFee.Where(c => lstMasterCheckID.Contains(c.DITOMasterID)).ToList();
                                var lstDriverID = lstMasterCheck.Where(c => c.DriverID1.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID1))).Select(c => c.DriverID1).Distinct().ToList();
                                lstDriverID.AddRange(lstMasterCheck.Where(c => c.DriverID2.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID2))).Select(c => c.DriverID2).Distinct().ToList());
                                lstDriverID.AddRange(lstMasterCheck.Where(c => c.DriverID3.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID3))).Select(c => c.DriverID3).Distinct().ToList());
                                lstDriverID = lstDriverID.Distinct().ToList();

                                // Container
                                var lstMasterCOCheckID = queryOPSContainerDriverFee.Select(c => c.COTOMasterID).Distinct().ToArray();
                                var lstMasterCOCheck = queryMasterCODriverFee.Where(c => lstMasterCOCheckID.Contains(c.ID)).ToList();
                                var lstMasterContainerCheck = queryOPSContainerDriverFee.Where(c => lstMasterCOCheckID.Contains(c.COTOMasterID)).ToList();
                                var lstDriverCOID = lstMasterCOCheck.Where(c => c.DriverID1.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID1))).Select(c => c.DriverID1).Distinct().ToList();
                                lstDriverCOID.AddRange(lstMasterCOCheck.Where(c => c.DriverID2.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID2))).Select(c => c.DriverID2).Distinct().ToList());
                                lstDriverCOID.AddRange(lstMasterCOCheck.Where(c => c.DriverID3.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID3))).Select(c => c.DriverID3).Distinct().ToList());
                                lstDriverCOID = lstDriverCOID.Distinct().ToList();

                                #region Tính theo tổng chuyến
                                if (driverFee.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumTotal)
                                {
                                    #region Xe tải
                                    foreach (var driverID in lstDriverID)
                                    {
                                        var lstTemp = lstMasterGroupCheck.Where(c => (c.DriverID1 == driverID || c.DriverID2 == driverID || c.DriverID3 == driverID));
                                        DTOPriceDIExExpr itemExpr = new DTOPriceDIExExpr();
                                        itemExpr.TonTransfer = lstTemp.Sum(c => c.TonTranfer);
                                        itemExpr.CBMTransfer = lstTemp.Sum(c => c.CBMTranfer);
                                        itemExpr.QuantityTransfer = lstTemp.Sum(c => c.QuantityTranfer);
                                        itemExpr.TonReturn = lstTemp.Sum(c => c.TonReturn);
                                        itemExpr.CBMReturn = lstTemp.Sum(c => c.CBMReturn);
                                        itemExpr.QuantityReturn = lstTemp.Sum(c => c.QuantityReturn);
                                        itemExpr.TotalSchedule = lstTemp.Select(c => c.DITOMasterID).Distinct().Count();
                                        itemExpr.GetPoint = lstTemp.Select(c => c.LocationFromID).Distinct().Count();
                                        itemExpr.DropPoint = lstTemp.Select(c => c.LocationToID).Distinct().Count();
                                        bool flag = false;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, driverFee.ExprInput);
                                        }
                                        catch { flag = false; }

                                        if (flag == true)
                                        {
                                            FIN_PL pl = new FIN_PL();
                                            pl.IsPlanning = false;
                                            pl.Effdate = schedule.DateTo.Date;
                                            pl.Code = string.Empty;
                                            pl.CreatedBy = Account.UserName;
                                            pl.CreatedDate = DateTime.Now;
                                            pl.SYSCustomerID = Account.SYSCustomerID;
                                            pl.VendorID = itemContract.VendorID;
                                            pl.ContractID = itemContract.ID;
                                            pl.CustomerID = itemContract.CustomerID;
                                            pl.DriverID = driverID;
                                            pl.ScheduleID = schedule.ID;
                                            pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                            decimal? priceFix = null, priceUnit = null, quantity = null;
                                            if (!string.IsNullOrEmpty(driverFee.ExprPriceFix))
                                                priceFix = Expression_GetValue(Expression_GetPackage(driverFee.ExprPriceFix), itemExpr);
                                            if (!string.IsNullOrEmpty(driverFee.ExprUnitPrice))
                                                priceUnit = Expression_GetValue(Expression_GetPackage(driverFee.ExprUnitPrice), itemExpr);
                                            if (!string.IsNullOrEmpty(driverFee.ExprQuantity))
                                                quantity = Expression_GetValue(Expression_GetPackage(driverFee.ExprQuantity), itemExpr);

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                                plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                plCost.Note = driverFee.FeeName;
                                                plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                plCost.Debit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;
                                            }

                                            if (priceUnit.HasValue && quantity.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                                plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                plCost.Note = driverFee.FeeName;
                                                plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                plCost.Quantity = (double)quantity.Value;
                                                plCost.UnitPrice = priceUnit.Value;
                                                plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;
                                            }

                                            var lstGroupProduct = model.CAT_DriverFeeGroupProduct.Where(c => c.DriverFeeID == driverFee.ID && !string.IsNullOrEmpty(c.ExprQuantity) && !string.IsNullOrEmpty(c.ExprPrice));
                                            foreach (var itemGroupProduct in lstGroupProduct)
                                            {
                                                var lstTempGroup = lstTemp.Where(c => c.GroupOfProductID == itemGroupProduct.GroupOfProductID);
                                                foreach (var itemGroup in lstTempGroup)
                                                {
                                                    DTOPriceDIExExpr itemExprGroup = new DTOPriceDIExExpr();
                                                    itemExprGroup.TonOrder = itemGroup.TonOrder;
                                                    itemExprGroup.CBMOrder = itemGroup.CBMOrder;
                                                    itemExprGroup.QuantityOrder = itemGroup.QuantityOrder;
                                                    itemExprGroup.TonTransfer = itemGroup.TonTranfer;
                                                    itemExprGroup.CBMTransfer = itemGroup.CBMTranfer;
                                                    itemExprGroup.QuantityTransfer = itemGroup.QuantityTranfer;
                                                    itemExprGroup.TonReturn = itemGroup.TonReturn;
                                                    itemExprGroup.CBMReturn = itemGroup.CBMReturn;
                                                    itemExprGroup.QuantityReturn = itemGroup.QuantityReturn;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprPrice), itemExprGroup);
                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprQuantity), itemExprGroup);

                                                    if (priceGroupMOQ.HasValue && quantityGroupMOQ.HasValue)
                                                    {
                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                        plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                        plCost.Note = driverFee.FeeName;
                                                        plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                        plCost.Quantity = (double)quantityGroupMOQ.Value;
                                                        plCost.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        pl.Debit += plCost.Debit;

                                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.GroupOfProductID = itemGroup.ID;
                                                        plGroup.Quantity = (double)quantityGroupMOQ.Value;
                                                        plGroup.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                    }
                                                }
                                            }

                                            if (pl.FIN_PLDetails.Count > 0)
                                                lstPl.Add(pl);
                                        }
                                    }
                                    #endregion

                                    #region Container
                                    foreach (var driverID in lstDriverCOID)
                                    {
                                        var lstTemp = lstMasterContainerCheck.Where(c => (c.DriverID1 == driverID || c.DriverID2 == driverID || c.DriverID3 == driverID));
                                        DTOPriceDIExExpr itemExpr = new DTOPriceDIExExpr();
                                        itemExpr.TotalSchedule = lstTemp.Select(c => c.COTOMasterID).Distinct().Count();
                                        itemExpr.GetPoint = lstTemp.Select(c => c.LocationFromID).Distinct().Count();
                                        itemExpr.DropPoint = lstTemp.Select(c => c.LocationToID).Distinct().Count();
                                        itemExpr.TotalPacking = lstTemp.Select(c => c.OrderContainerID).Count();
                                        bool flag = false;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, driverFee.ExprInput);
                                        }
                                        catch { flag = false; }

                                        if (flag == true)
                                        {
                                            FIN_PL pl = new FIN_PL();
                                            pl.IsPlanning = false;
                                            pl.Effdate = schedule.DateTo.Date;
                                            pl.Code = string.Empty;
                                            pl.CreatedBy = Account.UserName;
                                            pl.CreatedDate = DateTime.Now;
                                            pl.SYSCustomerID = Account.SYSCustomerID;
                                            pl.VendorID = itemContract.VendorID;
                                            pl.ContractID = itemContract.ID;
                                            pl.CustomerID = itemContract.CustomerID;
                                            pl.DriverID = driverID;
                                            pl.ScheduleID = schedule.ID;
                                            pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                            decimal? priceFix = null, priceUnit = null, quantity = null;
                                            if (!string.IsNullOrEmpty(driverFee.ExprPriceFix))
                                                priceFix = Expression_GetValue(Expression_GetPackage(driverFee.ExprPriceFix), itemExpr);
                                            if (!string.IsNullOrEmpty(driverFee.ExprUnitPrice))
                                                priceUnit = Expression_GetValue(Expression_GetPackage(driverFee.ExprUnitPrice), itemExpr);
                                            if (!string.IsNullOrEmpty(driverFee.ExprQuantity))
                                                quantity = Expression_GetValue(Expression_GetPackage(driverFee.ExprQuantity), itemExpr);

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                                plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                plCost.Note = driverFee.FeeName;
                                                plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                plCost.Debit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;
                                            }

                                            if (priceUnit.HasValue && quantity.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                                plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                plCost.Note = driverFee.FeeName;
                                                plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                plCost.Quantity = (double)quantity.Value;
                                                plCost.UnitPrice = priceUnit.Value;
                                                plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;
                                            }

                                            var lstGroupProduct = model.CAT_DriverFeePacking.Where(c => c.DriverFeeID == driverFee.ID && !string.IsNullOrEmpty(c.ExprQuantity) && !string.IsNullOrEmpty(c.ExprPrice));
                                            foreach (var itemGroupProduct in lstGroupProduct)
                                            {
                                                var lstTempGroup = lstTemp.Where(c => c.PackingID == itemGroupProduct.PackingID);
                                                DTOPriceDIExExpr itemExprGroup = new DTOPriceDIExExpr();
                                                itemExprGroup.TotalPacking = lstTempGroup.Count();

                                                decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprPrice), itemExprGroup);
                                                decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprQuantity), itemExprGroup);
                                                if (priceGroupMOQ.HasValue && quantityGroupMOQ.HasValue)
                                                {
                                                    foreach (var itemCotainer in lstTempGroup)
                                                    {
                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                                        plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                        plCost.Note = driverFee.FeeName;
                                                        plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                        plCost.Quantity = (double)quantityGroupMOQ.Value;
                                                        plCost.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        pl.Debit += plCost.Debit;

                                                        FIN_PLContainer plGroup = new FIN_PLContainer();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.COTOContainerID = itemCotainer.ID;
                                                        plGroup.Quantity = (double)quantityGroupMOQ.Value;
                                                        plGroup.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.FIN_PLContainer.Add(plGroup);
                                                    }
                                                }
                                            }

                                            if (pl.FIN_PLDetails.Count > 0)
                                                lstPl.Add(pl);
                                        }
                                    }
                                    #endregion
                                }
                                #endregion

                                #region Tính theo từng chuyến
                                if (driverFee.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumSchedule)
                                {
                                    #region Xe tải
                                    foreach (var driverID in lstDriverID)
                                    {
                                        var lstMasterGroup = lstMasterGroupCheck.Where(c => (c.DriverID1 == driverID || c.DriverID2 == driverID || c.DriverID3 == driverID)).OrderBy(c => c.ETD).GroupBy(c => c.DITOMasterID);
                                        int sort = 1;
                                        foreach (var itemMasterGroup in lstMasterGroup)
                                        {
                                            HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                            itemExpr.TonTransfer = itemMasterGroup.Sum(c => c.TonTranfer);
                                            itemExpr.CBMTransfer = itemMasterGroup.Sum(c => c.CBMTranfer);
                                            itemExpr.QuantityTransfer = itemMasterGroup.Sum(c => c.QuantityTranfer);
                                            itemExpr.TonReturn = itemMasterGroup.Sum(c => c.TonReturn);
                                            itemExpr.CBMReturn = itemMasterGroup.Sum(c => c.CBMReturn);
                                            itemExpr.QuantityReturn = itemMasterGroup.Sum(c => c.QuantityReturn);
                                            itemExpr.TotalSchedule = lstMasterGroup.Count();
                                            itemExpr.GetPoint = itemMasterGroup.Select(c => c.LocationFromID).Distinct().Count();
                                            itemExpr.DropPoint = itemMasterGroup.Select(c => c.LocationToID).Distinct().Count();
                                            var master = lstMasterCheck.FirstOrDefault(c => c.ID == itemMasterGroup.Key);
                                            itemExpr.VehicleCode = master.VehicleCode;
                                            itemExpr.GOVCode = master.GOVCode;
                                            itemExpr.MaxWeightCal = master.MaxWeightCal;
                                            itemExpr.ExIsOverNight = master.ExIsOverNight;
                                            itemExpr.ExIsOverWeight = master.ExIsOverWeight;
                                            itemExpr.ExTotalDayOut = master.ExTotalDayOut;
                                            itemExpr.ExTotalJoin = master.ExTotalJoin;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.SortInDay = sort;
                                            bool flag = false;
                                            try
                                            {
                                                flag = Expression_FLMCheckBool(itemExpr, driverFee.ExprInput);
                                            }
                                            catch { flag = false; }

                                            if (flag == true && lstMasterCheck.Where(c => c.ID == itemMasterGroup.Key).Count() > 0)
                                            {
                                                FIN_PL pl = new FIN_PL();
                                                pl.IsPlanning = false;
                                                pl.Effdate = lstMasterCheck.FirstOrDefault(c => c.ID == itemMasterGroup.Key).DateConfig.Date;
                                                pl.Code = string.Empty;
                                                pl.CreatedBy = Account.UserName;
                                                pl.CreatedDate = DateTime.Now;
                                                pl.SYSCustomerID = Account.SYSCustomerID;
                                                pl.VendorID = itemContract.VendorID;
                                                pl.ContractID = itemContract.ID;
                                                pl.CustomerID = itemContract.CustomerID;
                                                pl.DriverID = driverID;
                                                pl.DITOMasterID = itemMasterGroup.Key;
                                                pl.VehicleID = master.VehicleID;
                                                pl.ScheduleID = schedule.ID;
                                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                                decimal? priceFix = null, priceUnit = null, quantity = null;
                                                if (!string.IsNullOrEmpty(driverFee.ExprPriceFix))
                                                    priceFix = Expression_FLMGetValue(itemExpr, driverFee.ExprPriceFix);
                                                if (!string.IsNullOrEmpty(driverFee.ExprUnitPrice))
                                                    priceUnit = Expression_FLMGetValue(itemExpr, driverFee.ExprUnitPrice);
                                                if (!string.IsNullOrEmpty(driverFee.ExprQuantity))
                                                    quantity = Expression_FLMGetValue(itemExpr, driverFee.ExprQuantity);

                                                if (priceFix.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Debit = priceFix.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    // Phân bổ cho 1 group
                                                    var lstOPSGroupID = itemMasterGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceFLM_FindOrder(pl, plCost, lstPlTemp, lstPLTempQuery, lstOPSGroupID, lstMasterGroupCheck, scheduleID, itemMasterGroup.Key);
                                                }

                                                if (priceUnit.HasValue && quantity.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Quantity = (double)quantity.Value;
                                                    plCost.UnitPrice = priceUnit.Value;
                                                    plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    // Phân bổ cho 1 group
                                                    var lstOPSGroupID = itemMasterGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceFLM_FindOrder(pl, plCost, lstPlTemp, lstPLTempQuery, lstOPSGroupID, lstMasterGroupCheck, scheduleID, itemMasterGroup.Key);
                                                }

                                                var lstGroupProduct = model.CAT_DriverFeeGroupProduct.Where(c => c.DriverFeeID == driverFee.ID && !string.IsNullOrEmpty(c.ExprQuantity) && !string.IsNullOrEmpty(c.ExprPrice));
                                                foreach (var itemGroupProduct in lstGroupProduct)
                                                {
                                                    var lstTempGroup = lstMasterGroupCheck.Where(c => c.DITOMasterID == itemMasterGroup.Key && c.GroupOfProductID == itemGroupProduct.GroupOfProductID);
                                                    foreach (var itemGroup in lstTempGroup)
                                                    {
                                                        DTOPriceDIExExpr itemExprGroup = new DTOPriceDIExExpr();
                                                        itemExprGroup.TonOrder = itemGroup.TonOrder;
                                                        itemExprGroup.CBMOrder = itemGroup.CBMOrder;
                                                        itemExprGroup.QuantityOrder = itemGroup.QuantityOrder;
                                                        itemExprGroup.TonTransfer = itemGroup.TonTranfer;
                                                        itemExprGroup.CBMTransfer = itemGroup.CBMTranfer;
                                                        itemExprGroup.QuantityTransfer = itemGroup.QuantityTranfer;
                                                        itemExprGroup.TonReturn = itemGroup.TonReturn;
                                                        itemExprGroup.CBMReturn = itemGroup.CBMReturn;
                                                        itemExprGroup.QuantityReturn = itemGroup.QuantityReturn;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprPrice), itemExprGroup);
                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprQuantity), itemExprGroup);

                                                        if (priceGroupMOQ.HasValue && quantityGroupMOQ.HasValue)
                                                        {
                                                            FIN_PLDetails plCost = new FIN_PLDetails();
                                                            plCost.CreatedBy = Account.UserName;
                                                            plCost.CreatedDate = DateTime.Now;
                                                            plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                            plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                            plCost.Note = driverFee.FeeName;
                                                            plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                            plCost.Quantity = (double)quantityGroupMOQ.Value;
                                                            plCost.UnitPrice = priceGroupMOQ.Value;
                                                            plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                            pl.FIN_PLDetails.Add(plCost);
                                                            pl.Debit += plCost.Debit;

                                                            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                            plGroup.CreatedBy = Account.UserName;
                                                            plGroup.CreatedDate = DateTime.Now;
                                                            plGroup.GroupOfProductID = itemGroup.ID;
                                                            plGroup.Quantity = (double)quantityGroupMOQ.Value;
                                                            plGroup.UnitPrice = priceGroupMOQ.Value;
                                                            plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                        }
                                                    }
                                                }

                                                if (pl.FIN_PLDetails.Count > 0)
                                                {
                                                    FIN_PL plPL = new FIN_PL();
                                                    CopyFinPL(pl, plPL);
                                                    plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                                    lstPl.Add(pl);
                                                    lstPl.Add(plPL);
                                                }
                                            }

                                            sort++;
                                        }
                                    }
                                    #endregion

                                    #region Container
                                    foreach (var driverID in lstDriverCOID)
                                    {
                                        var lstMasterGroup = lstMasterContainerCheck.Where(c => (c.DriverID1 == driverID || c.DriverID2 == driverID || c.DriverID3 == driverID)).OrderBy(c => c.ETD).GroupBy(c => c.COTOMasterID);
                                        int sort = 1;
                                        foreach (var itemMasterGroup in lstMasterGroup)
                                        {
                                            HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                            itemExpr.TotalSchedule = lstMasterGroup.Count();
                                            itemExpr.GetPoint = itemMasterGroup.Select(c => c.LocationFromID).Distinct().Count();
                                            itemExpr.DropPoint = itemMasterGroup.Select(c => c.LocationToID).Distinct().Count();
                                            var master = lstMasterCOCheck.FirstOrDefault(c => c.ID == itemMasterGroup.Key);
                                            itemExpr.VehicleCode = master.VehicleCode;
                                            itemExpr.GOVCode = master.GOVCode;
                                            itemExpr.MaxWeightCal = master.MaxWeightCal;
                                            itemExpr.ExIsOverNight = master.ExIsOverNight;
                                            itemExpr.ExIsOverWeight = master.ExIsOverWeight;
                                            itemExpr.ExTotalDayOut = master.ExTotalDayOut;
                                            itemExpr.ExTotalJoin = master.ExTotalJoin;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.SortInDay = sort;
                                            itemExpr.TotalPacking = itemMasterGroup.Select(c => c.OrderContainerID).Count();
                                            itemExpr.Con20DCEmpty = itemMasterGroup.Count(c => c.PackingCode == CATPackingCOCode.CO20.ToString() && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOGetEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty));
                                            itemExpr.Con40DCEmpty = itemMasterGroup.Count(c => c.PackingCode == CATPackingCOCode.CO40.ToString() && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOGetEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty));
                                            itemExpr.Con40HCEmpty = itemMasterGroup.Count(c => c.PackingCode == CATPackingCOCode.CO40H.ToString() && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOGetEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty));
                                            itemExpr.Con20DCLaden = itemMasterGroup.Count(c => c.PackingCode == CATPackingCOCode.CO20.ToString() && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden));
                                            itemExpr.Con40DCLaden = itemMasterGroup.Count(c => c.PackingCode == CATPackingCOCode.CO40.ToString() && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden));
                                            itemExpr.Con40HCLaden = itemMasterGroup.Count(c => c.PackingCode == CATPackingCOCode.CO40H.ToString() && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden));
                                            itemExpr.KM = master.KM > 0 ? master.KM.Value : 0;
                                            itemExpr.TotalKM = master.KM > 0 ? master.KM.Value : 0;
                                            itemExpr.TotalTon = 0;
                                            var coto = lstOPSCOTO.Where(c => c.COTOMasterID == master.ID).OrderByDescending(c => c.Ton).FirstOrDefault();
                                            if (coto != null)
                                                itemExpr.TotalTon = coto.Ton;
                                            bool flag = false;
                                            try
                                            {
                                                flag = Expression_FLMCheckBool(itemExpr, driverFee.ExprInput);
                                            }
                                            catch { flag = false; }

                                            if (flag == true && lstMasterCheck.Where(c => c.ID == itemMasterGroup.Key).Count() > 0)
                                            {
                                                FIN_PL pl = new FIN_PL();
                                                pl.IsPlanning = false;
                                                pl.Effdate = lstMasterCheck.FirstOrDefault(c => c.ID == itemMasterGroup.Key).DateConfig.Date;
                                                pl.Code = string.Empty;
                                                pl.CreatedBy = Account.UserName;
                                                pl.CreatedDate = DateTime.Now;
                                                pl.SYSCustomerID = Account.SYSCustomerID;
                                                pl.VendorID = itemContract.VendorID;
                                                pl.ContractID = itemContract.ID;
                                                pl.CustomerID = itemContract.CustomerID;
                                                pl.DriverID = driverID;
                                                pl.COTOMasterID = itemMasterGroup.Key;
                                                pl.VehicleID = master.VehicleID;
                                                pl.ScheduleID = schedule.ID;
                                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                                decimal? priceFix = null, priceUnit = null, quantity = null;
                                                if (!string.IsNullOrEmpty(driverFee.ExprPriceFix))
                                                    priceFix = Expression_FLMGetValue(itemExpr, driverFee.ExprPriceFix);
                                                if (!string.IsNullOrEmpty(driverFee.ExprUnitPrice))
                                                    priceUnit = Expression_FLMGetValue(itemExpr, driverFee.ExprUnitPrice);
                                                if (!string.IsNullOrEmpty(driverFee.ExprQuantity))
                                                    quantity = Expression_FLMGetValue(itemExpr, driverFee.ExprQuantity);

                                                if (priceFix.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Debit = priceFix.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    // Phân bổ cho 1 group
                                                    var lstOPSContainerID = itemMasterGroup.Select(c => c.ID).Distinct().ToList();
                                                    DIPriceFLM_FindOrderCO(pl, plCost, lstPlTemp, lstPLTempQueryCO, lstOPSContainerID, lstMasterContainerCheck, scheduleID, itemMasterGroup.Key);
                                                }

                                                if (priceUnit.HasValue && quantity.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Quantity = (double)quantity.Value;
                                                    plCost.UnitPrice = priceUnit.Value;
                                                    plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    // Phân bổ cho 1 group
                                                    var lstOPSContainerID = itemMasterGroup.Select(c => c.ID).Distinct().ToList();
                                                    DIPriceFLM_FindOrderCO(pl, plCost, lstPlTemp, lstPLTempQueryCO, lstOPSContainerID, lstMasterContainerCheck, scheduleID, itemMasterGroup.Key);
                                                }

                                                var lstGroupProduct = model.CAT_DriverFeePacking.Where(c => c.DriverFeeID == driverFee.ID && !string.IsNullOrEmpty(c.ExprQuantity) && !string.IsNullOrEmpty(c.ExprPrice));
                                                foreach (var itemGroupProduct in lstGroupProduct)
                                                {
                                                    var lstTempGroup = lstMasterContainerCheck.Where(c => c.COTOMasterID == itemMasterGroup.Key && c.PackingID == itemGroupProduct.PackingID);
                                                    DTOPriceDIExExpr itemExprGroup = new DTOPriceDIExExpr();
                                                    itemExprGroup.TotalPacking = lstTempGroup.Count();

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprPrice), itemExprGroup);
                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprQuantity), itemExprGroup);

                                                    if (priceGroupMOQ.HasValue && quantityGroupMOQ.HasValue)
                                                    {
                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                        plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                        plCost.Note = driverFee.FeeName;
                                                        plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                        plCost.Quantity = (double)quantityGroupMOQ.Value;
                                                        plCost.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        pl.Debit += plCost.Debit;

                                                        FIN_PLContainer plContainer = new FIN_PLContainer();
                                                        plContainer.CreatedBy = Account.UserName;
                                                        plContainer.CreatedDate = DateTime.Now;
                                                        plContainer.COTOContainerID = lstTempGroup.FirstOrDefault().ID;
                                                        plContainer.Quantity = (double)quantityGroupMOQ.Value;
                                                        plContainer.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.FIN_PLContainer.Add(plContainer);
                                                    }
                                                }

                                                if (pl.FIN_PLDetails.Count > 0)
                                                {
                                                    FIN_PL plPL = new FIN_PL();
                                                    CopyFinPL(pl, plPL);
                                                    plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                                    lstPl.Add(pl);
                                                    lstPl.Add(plPL);
                                                }
                                            }

                                            sort++;
                                        }
                                    }
                                    #endregion
                                }
                                #endregion

                                #region Tính theo lương Cont
                                if (driverFee.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumCOOwnerPrice)
                                {
                                    #region Container
                                    foreach (var driverID in lstDriverCOID)
                                    {
                                        var lstContainer = lstMasterContainerCheck.Where(c => (c.DriverID1 == driverID || c.DriverID2 == driverID || c.DriverID3 == driverID));
                                        foreach (var itemContainer in lstContainer)
                                        {
                                            HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                            var master = lstMasterCOCheck.FirstOrDefault(c => c.ID == itemContainer.COTOMasterID);
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalPacking = 1;
                                            itemExpr.Price = itemContainer.UnitPrice;
                                            itemExpr.KM = master.KM > 0 ? master.KM.Value : 0;
                                            itemExpr.TotalKM = master.KM > 0 ? master.KM.Value : 0;
                                            itemExpr.TotalTon = 0;
                                            var coto = lstOPSCOTO.Where(c => c.COTOMasterID == master.ID).OrderByDescending(c => c.Ton).FirstOrDefault();
                                            if (coto != null)
                                                itemExpr.TotalTon = coto.Ton;
                                            // Tính doanh thu cont theo chặng
                                            decimal rate = 0;
                                            var contractSetting = itemContract.ListContractSetting.FirstOrDefault(c => c.ServiceOfOrderID == itemContainer.ServiceOfOrderID);
                                            if (contractSetting != null)
                                            {
                                                // Tính doanh thu cont theo chặng
                                                if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOGetEmpty)
                                                    itemExpr.Price = (decimal)contractSetting.GetEmpty * itemContainer.UnitPrice / 100; ;
                                                if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden)
                                                    itemExpr.Price = (decimal)contractSetting.Laden * itemContainer.UnitPrice / 100; ;
                                                if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty)
                                                    itemExpr.Price = (decimal)contractSetting.ReturnEmpty * itemContainer.UnitPrice / 100; ;
                                            }

                                            bool flag = false;
                                            try
                                            {
                                                flag = Expression_FLMCheckBool(itemExpr, driverFee.ExprInput);
                                            }
                                            catch { flag = false; }

                                            if (flag == true)
                                            {
                                                FIN_PL pl = new FIN_PL();
                                                pl.IsPlanning = false;
                                                pl.Effdate = master.DateConfig.Date;
                                                pl.Code = string.Empty;
                                                pl.CreatedBy = Account.UserName;
                                                pl.CreatedDate = DateTime.Now;
                                                pl.SYSCustomerID = Account.SYSCustomerID;
                                                pl.VendorID = itemContract.VendorID;
                                                pl.ContractID = itemContract.ID;
                                                pl.CustomerID = itemContract.CustomerID;
                                                pl.DriverID = driverID;
                                                pl.COTOMasterID = master.ID;
                                                pl.VehicleID = master.VehicleID;
                                                pl.ScheduleID = schedule.ID;
                                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                                decimal? priceFix = null, priceUnit = null, quantity = null;
                                                if (!string.IsNullOrEmpty(driverFee.ExprPriceFix))
                                                    priceFix = Expression_FLMGetValue(itemExpr, driverFee.ExprPriceFix);
                                                if (!string.IsNullOrEmpty(driverFee.ExprUnitPrice))
                                                    priceUnit = Expression_FLMGetValue(itemExpr, driverFee.ExprUnitPrice);
                                                if (!string.IsNullOrEmpty(driverFee.ExprQuantity))
                                                    quantity = Expression_FLMGetValue(itemExpr, driverFee.ExprQuantity);

                                                if (priceFix.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Debit = priceFix.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    // Phân bổ cho 1 group
                                                    var lstOPSContainerID = new List<int> { itemContainer.ID };
                                                    DIPriceFLM_FindOrderCO(pl, plCost, lstPlTemp, lstPLTempQueryCO, lstOPSContainerID, lstMasterContainerCheck, scheduleID, master.ID);
                                                }

                                                if (priceUnit.HasValue && quantity.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Quantity = (double)quantity.Value;
                                                    plCost.UnitPrice = priceUnit.Value;
                                                    plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    // Phân bổ cho 1 group
                                                    var lstOPSContainerID = new List<int> { itemContainer.ID };
                                                    DIPriceFLM_FindOrderCO(pl, plCost, lstPlTemp, lstPLTempQueryCO, lstOPSContainerID, lstMasterContainerCheck, scheduleID, master.ID);
                                                }

                                                var lstGroupProduct = model.CAT_DriverFeePacking.Where(c => c.DriverFeeID == driverFee.ID && !string.IsNullOrEmpty(c.ExprQuantity) && !string.IsNullOrEmpty(c.ExprPrice) && c.PackingID == itemContainer.PackingID);
                                                foreach (var itemGroupProduct in lstGroupProduct)
                                                {
                                                    DTOPriceDIExExpr itemExprGroup = new DTOPriceDIExExpr();
                                                    itemExprGroup.TotalPacking = 1;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprPrice), itemExprGroup);
                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprQuantity), itemExprGroup);

                                                    if (priceGroupMOQ.HasValue && quantityGroupMOQ.HasValue)
                                                    {
                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                        plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                        plCost.Note = driverFee.FeeName;
                                                        plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                        plCost.Quantity = (double)quantityGroupMOQ.Value;
                                                        plCost.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        pl.Debit += plCost.Debit;

                                                        FIN_PLContainer plContainer = new FIN_PLContainer();
                                                        plContainer.CreatedBy = Account.UserName;
                                                        plContainer.CreatedDate = DateTime.Now;
                                                        plContainer.COTOContainerID = itemContainer.ID;
                                                        plContainer.Quantity = (double)quantityGroupMOQ.Value;
                                                        plContainer.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.FIN_PLContainer.Add(plContainer);
                                                    }
                                                }

                                                if (pl.FIN_PLDetails.Count > 0)
                                                {
                                                    FIN_PL plPL = new FIN_PL();
                                                    CopyFinPL(pl, plPL);
                                                    plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                                    lstPl.Add(pl);
                                                    lstPl.Add(plPL);
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                #endregion

                                #region Tính theo tổng doanh thu
                                if (driverFee.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumCOOwnerPriceTotal)
                                {
                                    #region Container
                                    foreach (var driverID in lstDriverCOID)
                                    {
                                        var lstContainer = lstMasterContainerCheck.Where(c => (c.DriverID1 == driverID || c.DriverID2 == driverID || c.DriverID3 == driverID));
                                        HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                        itemExpr.TotalTOMaster = lstContainer.Select(c => c.COTOMasterID).Distinct().Count();
                                        foreach (var itemContainer in lstContainer)
                                        {
                                            var contractSetting = itemContract.ListContractSetting.FirstOrDefault(c => c.ServiceOfOrderID == itemContainer.ServiceOfOrderID);
                                            if (contractSetting != null)
                                            {
                                                // Tính doanh thu cont theo chặng
                                                if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOGetEmpty)
                                                    itemExpr.TotalPrice += (decimal)contractSetting.GetEmpty * itemContainer.UnitPrice / 100; ;
                                                if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden)
                                                    itemExpr.TotalPrice += (decimal)contractSetting.Laden * itemContainer.UnitPrice / 100; ;
                                                if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty)
                                                    itemExpr.TotalPrice += (decimal)contractSetting.ReturnEmpty * itemContainer.UnitPrice / 100; ;
                                            }
                                        }


                                        bool flag = false;
                                        try
                                        {
                                            flag = Expression_FLMCheckBool(itemExpr, driverFee.ExprInput);
                                        }
                                        catch { flag = false; }

                                        if (flag == true)
                                        {
                                            var masterID = lstContainer.OrderByDescending(c => c.ETD).FirstOrDefault().COTOMasterID;
                                            var master = lstMasterCOCheck.FirstOrDefault(c => c.ID == masterID);
                                            var itemContainer = lstContainer.Where(c => c.COTOMasterID == masterID && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden)).FirstOrDefault();
                                            if (itemContainer == null)
                                                itemContainer = lstContainer.Where(c => c.COTOMasterID == masterID).FirstOrDefault();
                                            FIN_PL pl = new FIN_PL();
                                            pl.IsPlanning = false;
                                            pl.Effdate = master.DateConfig.Date;
                                            pl.Code = string.Empty;
                                            pl.CreatedBy = Account.UserName;
                                            pl.CreatedDate = DateTime.Now;
                                            pl.SYSCustomerID = Account.SYSCustomerID;
                                            pl.VendorID = itemContract.VendorID;
                                            pl.ContractID = itemContract.ID;
                                            pl.CustomerID = itemContract.CustomerID;
                                            pl.DriverID = driverID;
                                            pl.COTOMasterID = master.ID;
                                            pl.VehicleID = master.VehicleID;
                                            pl.ScheduleID = schedule.ID;
                                            pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                            decimal? priceFix = null, priceUnit = null, quantity = null;
                                            if (!string.IsNullOrEmpty(driverFee.ExprPriceFix))
                                                priceFix = Expression_FLMGetValue(itemExpr, driverFee.ExprPriceFix);
                                            if (!string.IsNullOrEmpty(driverFee.ExprUnitPrice))
                                                priceUnit = Expression_FLMGetValue(itemExpr, driverFee.ExprUnitPrice);
                                            if (!string.IsNullOrEmpty(driverFee.ExprQuantity))
                                                quantity = Expression_FLMGetValue(itemExpr, driverFee.ExprQuantity);

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                plCost.Note = driverFee.FeeName;
                                                plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                plCost.Debit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                // Phân bổ cho 1 group
                                                var lstOPSContainerID = new List<int> { itemContainer.ID };
                                                DIPriceFLM_FindOrderCO(pl, plCost, lstPlTemp, lstPLTempQueryCO, lstOPSContainerID, lstMasterContainerCheck, scheduleID, master.ID);
                                            }

                                            if (priceUnit.HasValue && quantity.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                plCost.Note = driverFee.FeeName;
                                                plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                plCost.Quantity = (double)quantity.Value;
                                                plCost.UnitPrice = priceUnit.Value;
                                                plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                // Phân bổ cho 1 group
                                                var lstOPSContainerID = new List<int> { itemContainer.ID };
                                                DIPriceFLM_FindOrderCO(pl, plCost, lstPlTemp, lstPLTempQueryCO, lstOPSContainerID, lstMasterContainerCheck, scheduleID, master.ID);
                                            }

                                            var lstGroupProduct = model.CAT_DriverFeePacking.Where(c => c.DriverFeeID == driverFee.ID && !string.IsNullOrEmpty(c.ExprQuantity) && !string.IsNullOrEmpty(c.ExprPrice) && c.PackingID == itemContainer.PackingID);
                                            foreach (var itemGroupProduct in lstGroupProduct)
                                            {
                                                DTOPriceDIExExpr itemExprGroup = new DTOPriceDIExExpr();
                                                itemExprGroup.TotalPacking = 1;

                                                decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprPrice), itemExprGroup);
                                                decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprQuantity), itemExprGroup);

                                                if (priceGroupMOQ.HasValue && quantityGroupMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Quantity = (double)quantityGroupMOQ.Value;
                                                    plCost.UnitPrice = priceGroupMOQ.Value;
                                                    plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    FIN_PLContainer plContainer = new FIN_PLContainer();
                                                    plContainer.CreatedBy = Account.UserName;
                                                    plContainer.CreatedDate = DateTime.Now;
                                                    plContainer.COTOContainerID = itemContainer.ID;
                                                    plContainer.Quantity = (double)quantityGroupMOQ.Value;
                                                    plContainer.UnitPrice = priceGroupMOQ.Value;
                                                    plCost.FIN_PLContainer.Add(plContainer);
                                                }
                                            }

                                            if (pl.FIN_PLDetails.Count > 0)
                                            {
                                                FIN_PL plPL = new FIN_PL();
                                                CopyFinPL(pl, plPL);
                                                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                                lstPl.Add(pl);
                                                lstPl.Add(plPL);
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion

                            #region Tính trouble
                            var lstMasterID = queryMasterContract.Select(c => c.ID).Distinct().ToList();
                            var lstMasterCOID = queryMasterContractCO.Select(c => c.ID).Distinct().ToList();
                            var lstTrouble = model.CAT_Trouble.Where(c => ((c.DITOMasterID.HasValue && lstMasterID.Contains(c.DITOMasterID.Value)) || (c.COTOMasterID.HasValue && lstMasterCOID.Contains(c.COTOMasterID.Value))) && c.TroubleCostStatusID == -(int)SYSVarType.TroubleCostStatusApproved && c.CostOfVendor != 0 && c.DriverID > 0);
                            if (lstTrouble != null && lstTrouble.Count() > 0)
                            {
                                foreach (var itemTrouble in lstTrouble)
                                {
                                    var master = queryMasterContract.FirstOrDefault(c => c.ID == itemTrouble.DITOMasterID);
                                    var masterCO = queryMasterContractCO.FirstOrDefault(c => c.ID == itemTrouble.COTOMasterID);
                                    if (master != null || masterCO != null)
                                    {
                                        var driver = model.FLM_Driver.FirstOrDefault(c => c.DriverID == itemTrouble.DriverID);
                                        if (driver != null)
                                        {
                                            FIN_PL pl = new FIN_PL();
                                            pl.IsPlanning = false;
                                            pl.Code = string.Empty;
                                            pl.CreatedBy = Account.UserName;
                                            pl.CreatedDate = DateTime.Now;
                                            pl.SYSCustomerID = Account.SYSCustomerID;
                                            pl.VendorID = itemContract.VendorID;
                                            pl.ContractID = itemContract.ID;
                                            pl.CustomerID = itemContract.CustomerID;
                                            pl.DriverID = driver.ID;
                                            pl.ScheduleID = schedule.ID;
                                            pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;
                                            if (itemTrouble.DITOMasterID > 0)
                                            {
                                                pl.DITOMasterID = itemTrouble.DITOMasterID;
                                                pl.VehicleID = master.VehicleID;
                                                pl.Effdate = master.DateConfig.Date;
                                            }
                                            if (itemTrouble.COTOMasterID > 0)
                                            {
                                                pl.COTOMasterID = itemTrouble.COTOMasterID;
                                                pl.VehicleID = masterCO.VehicleID;
                                                pl.Effdate = masterCO.DateConfig.Date;
                                            }

                                            FIN_PLDetails plCost = new FIN_PLDetails();
                                            plCost.CreatedBy = Account.UserName;
                                            plCost.CreatedDate = DateTime.Now;
                                            plCost.CostID = (int)CATCostType.TroubleDebit;
                                            plCost.Debit = itemTrouble.CostOfVendor;
                                            plCost.Credit = 0;
                                            plCost.TypeOfPriceDIExCode = itemTrouble.CAT_GroupOfTrouble.Code;
                                            pl.Debit = plCost.Debit;
                                            pl.FIN_PLDetails.Add(plCost);

                                            FIN_PL plPL = new FIN_PL();
                                            CopyFinPL(pl, plPL);
                                            plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                            lstPl.Add(pl);
                                            lstPl.Add(plPL);
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        System.Diagnostics.Debug.WriteLine("Contract end: " + itemContract.ID);
                    }

                    foreach (var pl in lstPl)
                        model.FIN_PL.Add(pl);

                    foreach (var pl in lstPlTemp)
                        model.FIN_Temp.Add(pl);

                }
            }
            #endregion

            #region Tính theo TimeSheet
            List<FIN_PL> lstPlTimeSheet = new List<FIN_PL>();

            // Lấy dữ liệu
            var contract = model.CAT_Contract.FirstOrDefault(c => c.ID == lstContractID.FirstOrDefault());
            if (contract != null)
            {
                var lstDriverFee = model.CAT_DriverFee.Where(c => c.ContractID == contract.ID && c.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumTimeSheet && !string.IsNullOrEmpty(c.ExprInput)).Select(c => new
                {
                    c.ID,
                    c.TypeOfDriverFeeID,
                    c.TypeOfDriverID,
                    c.FeeName,
                    c.ExprInput,
                    c.ExprPriceFix,
                    c.ExprQuantity,
                    c.ExprUnitPrice,
                    c.DriverFeeSumID,
                    TypeOfDriverFeeCode = c.CAT_TypeOfDriverFee.Code
                }).ToList();

                var lstTimeSheet = model.FLM_AssetTimeSheet.Where(c => DbFunctions.TruncateTime(c.DateFromActual) == DateConfig);

                foreach (var itemDriverFee in lstDriverFee)
                {
                    foreach (var itemTimeSheet in lstTimeSheet)
                    {
                        var itemTimeSheetDriver = model.FLM_AssetTimeSheetDriver.FirstOrDefault(c => c.AssetTimeSheetID == itemTimeSheet.ID && !c.IsReject);
                        if (itemTimeSheetDriver != null)
                        {
                            HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                            itemExpr.StatusOfAssetTimeSheetCode = itemTimeSheet.SYS_Var.Code;

                            bool flag = false;
                            try
                            {
                                flag = Expression_FLMCheckBool(itemExpr, itemDriverFee.ExprInput);
                            }
                            catch { flag = false; }

                            if (flag)
                            {
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = contract.CustomerID.HasValue ? contract.CustomerID.Value : Account.SYSCustomerID;
                                pl.ContractID = contract.ID;
                                pl.DriverID = itemTimeSheetDriver.DriverID;
                                pl.VehicleID = itemTimeSheet.FLM_Asset.VehicleID;
                                pl.ScheduleID = schedule.ID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                decimal? priceFix = null, priceUnit = null, quantity = null;
                                if (!string.IsNullOrEmpty(itemDriverFee.ExprPriceFix))
                                    priceFix = Expression_FLMGetValue(itemExpr, itemDriverFee.ExprPriceFix);
                                if (!string.IsNullOrEmpty(itemDriverFee.ExprUnitPrice))
                                    priceUnit = Expression_FLMGetValue(itemExpr, itemDriverFee.ExprUnitPrice);
                                if (!string.IsNullOrEmpty(itemDriverFee.ExprQuantity))
                                    quantity = Expression_FLMGetValue(itemExpr, itemDriverFee.ExprQuantity);

                                if (priceFix.HasValue)
                                {
                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                    plCost.Debit = priceFix.Value;
                                    plCost.Credit = 0;
                                    plCost.TypeOfPriceDIExCode = itemTimeSheet.SYS_Var1.Code;
                                    pl.Debit = plCost.Debit;
                                    pl.FIN_PLDetails.Add(plCost);
                                    lstPlTimeSheet.Add(pl);
                                }

                                if (priceUnit.HasValue && quantity.HasValue)
                                {
                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                    plCost.UnitPrice = priceUnit.Value;
                                    plCost.Quantity = (double)quantity.Value;
                                    plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                    plCost.TypeOfPriceDIExCode = itemTimeSheet.SYS_Var1.Code;
                                    pl.Debit = plCost.Debit;
                                    pl.FIN_PLDetails.Add(plCost);
                                    lstPlTimeSheet.Add(pl);
                                }

                                if (pl.FIN_PLDetails.Count > 0)
                                    lstPlTimeSheet.Add(pl);
                            }
                        }
                    }
                }
            }

            foreach (var plTimeSheet in lstPlTimeSheet)
                model.FIN_PL.Add(plTimeSheet);
            #endregion
        }

        // Tính định mức dầu
        public static void Truck_CalculateMaterialQuota(DataEntities model, AccountItem Account, int scheduleID)
        {
            string strKey = SYSSettingKey.Material.ToString();
            var lstStatusCon = new List<int>();
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerEXEmpty);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerEXLaden);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerIMLaden);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerIMEmpty);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerLOGetEmpty);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerLOLaden);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerLOReturnEmpty);

            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);

            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);
            // Lấy ds Vendor
            List<int> lstVendorID = new List<int>();
            lstVendorID.Add(Account.SYSCustomerID);

            //Lấy danh sách ID hợp đồng chính của LTL, FTL
            var lstContractID = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (lstVendorID.Contains(c.CustomerID.Value) || c.CustomerID == null) &&
                c.EffectDate <= DateConfig && (c.ExpiredDate == null || c.ExpiredDate > DateConfig) && (c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFCL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLCL))
                .Select(c => c.ID).ToArray();

            #region Lấy thông tin chuyến
            // Container
            var lstMasterCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.ContractID > 0).Select(c => new HelperFinance_COMaster
            {
                ID = c.ID,
                GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                CATRoutingID = c.RoutingID > 0 ? c.RoutingID.Value : -1,
                ParentRoutingID = c.RoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                DateConfig = c.DateConfig.Value,
                VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                VehicleID = c.VehicleID,
                DriverID1 = c.DriverID1,
                DriverID2 = c.DriverID2,
                DriverID3 = c.DriverID3,
                TypeOfDriverID1 = c.TypeOfDriverID1,
                TypeOfDriverID2 = c.TypeOfDriverID2,
                TypeOfDriverID3 = c.TypeOfDriverID3,
                ExIsOverNight = c.ExIsOverNight,
                ExIsOverWeight = c.ExIsOverWeight,
                ExTotalDayOut = c.ExTotalDayOut,
                ExTotalJoin = c.ExTotalJoin,
                ETD = c.ETD,
                SortOrder = c.SortOrder
            }).ToList();

            var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && (c.OPS_COTOMaster.VendorOfVehicleID == null || c.OPS_COTOMaster.VendorOfVehicleID == Account.SYSCustomerID) && c.OPS_COTOMaster.DateConfig.HasValue && c.OPS_COTOMaster.DateConfig >= DateConfig && c.OPS_COTOMaster.DateConfig < DateConfigEnd && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.OPS_COTOMaster.ContractID > 0 && c.ParentID == null && lstStatusCon.Contains(c.StatusOfCOContainerID)).Select(c => new HelperFinance_Container
            {
                ID = c.ID,
                OrderContainerID = c.OPS_Container.ContainerID,
                PackingID = c.OPS_Container.ORD_Container.PackingID,
                PackingCode = c.OPS_Container.ORD_Container.CAT_Packing.Code,
                COTOMasterID = c.COTOMasterID.Value,
                CUSRoutingID = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUSRoutingID.Value : -1,
                CATRoutingID = c.OPS_COTOMaster.RoutingID > 0 ? c.OPS_COTOMaster.RoutingID.Value : -1,
                CATRoutingName = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                OrderRoutingID = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUSRoutingID.Value : -1,
                ParentRoutingID = c.OPS_COTOMaster.RoutingID > 0 && c.OPS_COTOMaster.CAT_Routing.ParentID > 0 ? c.OPS_COTOMaster.CAT_Routing.ParentID.Value : -1,
                LocationFromID = c.OPS_Container.ORD_Container.CUS_Location2.LocationID,
                LocationToID = c.OPS_Container.ORD_Container.CUS_Location3.LocationID,
                LocationToName = c.CAT_Location.Location,
                GroupOfLocationID = c.CAT_Location.GroupOfLocationID > 0 ? c.CAT_Location.GroupOfLocationID.Value : -1,
                ContractID = c.OPS_COTOMaster.ContractID.Value,
                DriverID1 = c.OPS_COTOMaster.DriverID1,
                DriverID2 = c.OPS_COTOMaster.DriverID2,
                DriverID3 = c.OPS_COTOMaster.DriverID3,
                TypeOfDriverID1 = c.OPS_COTOMaster.TypeOfDriverID1,
                TypeOfDriverID2 = c.OPS_COTOMaster.TypeOfDriverID2,
                TypeOfDriverID3 = c.OPS_COTOMaster.TypeOfDriverID3,
                CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                ETD = c.OPS_COTOMaster.ETD,
                VendorID = c.OPS_COTOMaster.VendorOfVehicleID == null ? Account.SYSCustomerID : c.OPS_COTOMaster.VendorOfVehicleID.Value,
                SortOrder = c.SortOrder,
                COTOSort = c.COTOSort,
                StatusOfCOContainerID = c.StatusOfCOContainerID,
                ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID.HasValue ? c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID.Value : -1,

                COTOLocationFromAddress = c.CAT_Location.Address,
                COTOLocationToAddress = c.CAT_Location1.Address,
            }).ToList();

            var lstOPSCOTO = model.OPS_COTO.Where(c => c.COTOMasterID > 0 && c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && (c.OPS_COTOMaster.VendorOfVehicleID == null || c.OPS_COTOMaster.VendorOfVehicleID == Account.SYSCustomerID) && c.OPS_COTOMaster.DateConfig.HasValue && c.OPS_COTOMaster.DateConfig >= DateConfig && c.OPS_COTOMaster.DateConfig < DateConfigEnd && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.OPS_COTOMaster.ContractID > 0 && c.OPS_COTOMaster.VehicleID > 0).Select(c => new HelperFinance_COTO
            {
                ID = c.ID,
                COTOMasterID = c.COTOMasterID,
                KM = c.KM > 0 ? c.KM.Value : 0,
                Ton = c.Ton > 0 ? c.Ton.Value : 0,
                SortOrder = c.SortOrder,
                ContractID = c.OPS_COTOMaster.ContractID > 0 ? c.OPS_COTOMaster.ContractID.Value : -1,
                DriverID1 = c.OPS_COTOMaster.DriverID1,
                DriverID2 = c.OPS_COTOMaster.DriverID2,
                DriverID3 = c.OPS_COTOMaster.DriverID3,
                TypeOfDriverID1 = c.OPS_COTOMaster.TypeOfDriverID1,
                TypeOfDriverID2 = c.OPS_COTOMaster.TypeOfDriverID2,
                TypeOfDriverID3 = c.OPS_COTOMaster.TypeOfDriverID3,
                VehicleID = c.OPS_COTOMaster.VehicleID,
            }).ToList();

            var lstCOTODetail = model.OPS_COTODetail.Where(c => c.OPS_COTO.COTOMasterID > 0 && c.OPS_COTO.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && (c.OPS_COTO.OPS_COTOMaster.VendorOfVehicleID == null || c.OPS_COTO.OPS_COTOMaster.VendorOfVehicleID == Account.SYSCustomerID) && c.OPS_COTO.OPS_COTOMaster.DateConfig.HasValue && c.OPS_COTO.OPS_COTOMaster.DateConfig >= DateConfig && c.OPS_COTO.OPS_COTOMaster.DateConfig < DateConfigEnd && c.OPS_COTO.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.OPS_COTO.OPS_COTOMaster.ContractID > 0 && c.OPS_COTO.OPS_COTOMaster.VehicleID > 0).Select(c => new
            {
                c.COTOID,
                c.COTOContainerID
            }).ToList();

            // Lấy giá dầu hiện tại
            DTOTriggerMaterial materialQuota = new DTOTriggerMaterial();
            var sysSetting = model.SYS_Setting.FirstOrDefault(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == strKey);
            if (sysSetting != null)
            {
                try
                {
                    materialQuota = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOTriggerMaterial>(sysSetting.Setting);
                }
                catch { }
            }
            #endregion

            #region Tính định mức
            var lstContractMasterID = lstMasterCO.Select(c => c.ContractID).Distinct().ToList();
            if (lstContractMasterID.Count > 0 && lstContractMasterID.Count > 0)
            {
                lstContractID = lstContractID.Intersect(lstContractMasterID).ToArray();
                var lstContract = model.CAT_Contract.Where(c => lstContractID.Contains(c.ID) && !string.IsNullOrEmpty(c.ExprMaterialQuota)).Select(c => new HelperFinance_Contract
                {
                    ID = c.ID,
                    VendorID = c.CustomerID.HasValue ? c.CustomerID.Value : Account.SYSCustomerID,
                    TransportModeID = c.CAT_TransportMode.TransportModeID,
                    PriceInDay = c.PriceInDay,
                    CustomerID = c.CompanyID > 0 ? c.CUS_Company.CustomerRelateID : -1,
                    ExprFCLAllocationPrice = c.ExprFCLAllocationPrice,
                    ExprMaterialQuota = c.ExprMaterialQuota
                }).ToList();
                if (lstContract.Count > 0)
                {
                    List<FIN_PL> lstPl = new List<FIN_PL>();

                    //Chạy từng hợp đồng
                    foreach (var itemContract in lstContract)
                    {
                        if (itemContract.CustomerID < 1) itemContract.CustomerID = null;

                        System.Diagnostics.Debug.WriteLine("Contract start: " + itemContract.ID);
                        var queryMasterContractCO = lstMasterCO.Where(c => c.ContractID == itemContract.ID);
                        var queryOPSContainerContract = lstOPSContainer.Where(c => c.ContractID == itemContract.ID);
                        var queryOPSCOTOContract = lstOPSCOTO.Where(c => c.ContractID == itemContract.ID);
                        var lstVehicleID = queryOPSCOTOContract.Select(c => c.VehicleID.Value).Distinct().ToList();
                        var lstAssetID = model.FLM_Asset.Where(c => c.VehicleID.HasValue && lstVehicleID.Contains(c.VehicleID.Value)).Select(c => c.ID).Distinct().ToList();
                        var lstVehicleQuota = model.FLM_MaterialQuota.Where(c => lstAssetID.Contains(c.VehicleID)).Select(c => new
                        {
                            VehicleID = c.FLM_Asset.VehicleID.Value,
                            MaterialID = c.MaterialID,
                            MaterialCode = c.FLM_Material.Code,
                            c.QuantityPerKM,
                        }).ToList();
                        if (queryMasterContractCO.Count() > 0 && queryOPSCOTOContract.Count() > 0 && queryOPSContainerContract.Count() > 0)
                        {
                            foreach (var itemCOTO in queryOPSCOTOContract)
                            {
                                var lstDriverCOID = new List<int>();
                                if (itemCOTO.DriverID1 > 0 && itemCOTO.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverMain)
                                    lstDriverCOID.Add(itemCOTO.DriverID1.Value);
                                if (itemCOTO.DriverID2 > 0 && itemCOTO.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverMain)
                                    lstDriverCOID.Add(itemCOTO.DriverID2.Value);
                                if (itemCOTO.DriverID3 > 0 && itemCOTO.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverMain)
                                    lstDriverCOID.Add(itemCOTO.DriverID3.Value);
                                lstDriverCOID = lstDriverCOID.Distinct().ToList();
                                var lstQuota = lstVehicleQuota.Where(c => c.VehicleID == itemCOTO.VehicleID);
                                var lstContainerID = lstCOTODetail.Where(c => c.COTOID == itemCOTO.ID).Select(c => c.COTOContainerID).Distinct().ToList();
                                var lstContainer = queryOPSContainerContract.Where(c => lstContainerID.Contains(c.ID));
                                if (lstContainer.Count() > 0 && lstDriverCOID.Count > 0)
                                {
                                    foreach (var itemQuota in lstQuota)
                                    {
                                        HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                        itemExpr.KM = itemCOTO.KM;
                                        itemExpr.Ton = itemCOTO.Ton;
                                        itemExpr.Quota = itemQuota.QuantityPerKM;
                                        itemExpr.TotalCOContainer = lstContainer.Count();
                                        decimal? Value = 0;
                                        try
                                        {
                                            Value = Expression_FLMGetValue(itemExpr, itemContract.ExprMaterialQuota);
                                        }
                                        catch { }
                                        if (Value > 0)
                                            Value = Value / lstDriverCOID.Count();
                                        else
                                            Value = 0;
                                        // Phân bổ cho tài xế
                                        foreach (var driverID in lstDriverCOID)
                                        {
                                            foreach (var itemContainer in lstContainer)
                                            {
                                                #region Chuyến và đơn giá vật tư
                                                var master = lstMasterCO.FirstOrDefault(c => c.ID == itemCOTO.COTOMasterID);
                                                decimal unitPrice = 0;
                                                if (itemQuota.MaterialID == materialQuota.DieselArea1_MaterialID)
                                                    unitPrice = materialQuota.DieselArea1;
                                                if (itemQuota.MaterialID == materialQuota.DieselArea2_MaterialID)
                                                    unitPrice = materialQuota.DieselArea2;
                                                if (itemQuota.MaterialID == materialQuota.DO05Area1_MaterialID)
                                                    unitPrice = materialQuota.DO05Area1;
                                                if (itemQuota.MaterialID == materialQuota.DO05Area2_MaterialID)
                                                    unitPrice = materialQuota.DO05Area2;
                                                if (itemQuota.MaterialID == materialQuota.DO25Area1_MaterialID)
                                                    unitPrice = materialQuota.DO25Area1;
                                                if (itemQuota.MaterialID == materialQuota.DO25Area2_MaterialID)
                                                    unitPrice = materialQuota.DO25Area2;
                                                if (itemQuota.MaterialID == materialQuota.E5RON92Area1_MaterialID)
                                                    unitPrice = materialQuota.E5RON92Area1;
                                                if (itemQuota.MaterialID == materialQuota.E5RON92Area2_MaterialID)
                                                    unitPrice = materialQuota.E5RON92Area2;
                                                #endregion

                                                FIN_PL pl = new FIN_PL();
                                                pl.IsPlanning = false;
                                                pl.Effdate = master.DateConfig.Date;
                                                pl.Code = string.Empty;
                                                pl.CreatedBy = Account.UserName;
                                                pl.CreatedDate = DateTime.Now;
                                                pl.SYSCustomerID = Account.SYSCustomerID;
                                                pl.VendorID = itemContract.VendorID;
                                                pl.ContractID = itemContract.ID;
                                                pl.CustomerID = itemContract.CustomerID;
                                                pl.DriverID = driverID;
                                                pl.COTOMasterID = master.ID;
                                                pl.VehicleID = master.VehicleID;
                                                pl.ScheduleID = schedule.ID;
                                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;
                                                pl.OrderID = itemContainer.OrderID;

                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.COTOMaterialQuota;
                                                plCost.TypeOfPriceDIExCode = itemQuota.MaterialCode;
                                                plCost.Note = itemExpr.KM.ToString();
                                                plCost.Note1 = itemExpr.Quota.ToString();
                                                plCost.Note2 = itemExpr.Ton.ToString();
                                                plCost.Note3 = itemContainer.COTOLocationFromAddress;
                                                plCost.Note4 = itemContainer.COTOLocationToAddress;
                                                plCost.Note5 = string.Empty;
                                                plCost.Quantity = (double)Value;
                                                plCost.UnitPrice = unitPrice;
                                                plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                FIN_PLContainer plContainer = new FIN_PLContainer();
                                                plContainer.CreatedBy = Account.UserName;
                                                plContainer.CreatedDate = DateTime.Now;
                                                plContainer.COTOContainerID = itemContainer.ID;
                                                plContainer.Quantity = (double)Value;
                                                plContainer.UnitPrice = unitPrice;
                                                plCost.FIN_PLContainer.Add(plContainer);

                                                lstPl.Add(pl);
                                            }
                                        }
                                    }
                                }
                            }
                        }

                        System.Diagnostics.Debug.WriteLine("Contract end: " + itemContract.ID);
                    }

                    foreach (var pl in lstPl)
                        model.FIN_PL.Add(pl);
                }
            }
            #endregion
        }

        // Phân bổ khấu hao xe và phiếu
        public static void Truck_CalculateFixCost(DataEntities model, AccountItem Account, int scheduleID)
        {
            List<FIN_PL> lstPL = new List<FIN_PL>();
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);
            double totalDayActual = (schedule.DateTo - schedule.DateFrom).TotalDays + 1;
            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);

            // Danh sách loại ngày
            var lstScheduleDate = model.FLM_ScheduleDate.Where(c => c.ScheduleID == schedule.ID).Select(c => new
            {
                c.Date,
                c.TypeScheduleDateID
            }).ToList();

            #region Khấu hao xe
            var lstFixCost = model.FLM_FixedCost.Where(c => c.Month == schedule.Month && c.Year == schedule.Year && c.IsClosed && c.ReceiptID == null && ((!string.IsNullOrEmpty(c.FLM_Asset.ExprDay) && !string.IsNullOrEmpty(c.FLM_Asset.ExprInputDay)) || (!string.IsNullOrEmpty(c.FLM_Asset.ExprTOMaster) && !string.IsNullOrEmpty(c.FLM_Asset.ExprInputTOMaster))));

            if (lstFixCost != null && lstFixCost.Count() > 0)
            {
                #region Danh sách chuyến
                //Lấy các chuyến
                var lstMaster = model.OPS_DITOMaster.Where(c => c.DateConfig.HasValue && c.VehicleID > 0 && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.OPS_DITOGroupProduct.Count > 0 && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new HelperFinance_TOMaster
                {
                    ID = c.ID,
                    GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                    GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                    ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                    DateConfig = c.DateConfig.Value,
                    VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                    TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                    VehicleID = c.VehicleID,
                    DriverID1 = c.DriverID1,
                    DriverID2 = c.DriverID2,
                    DriverID3 = c.DriverID3,
                    TypeOfDriverID1 = c.TypeOfDriverID1,
                    TypeOfDriverID2 = c.TypeOfDriverID2,
                    TypeOfDriverID3 = c.TypeOfDriverID3,
                    ExIsOverNight = c.ExIsOverNight,
                    ExIsOverWeight = c.ExIsOverWeight,
                    ExTotalDayOut = c.ExTotalDayOut,
                    ExTotalJoin = c.ExTotalJoin,
                    KM = c.KM > 0 ? c.KM : 0,
                    OPSGroupProductID = c.OPS_DITOGroupProduct.OrderByDescending(d => d.TonTranfer).FirstOrDefault().ID
                }).ToList();

                //Lấy các chuyến
                var lstMasterCO = model.OPS_COTOMaster.Where(c => c.DateConfig.HasValue && c.VehicleID > 0 && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.OPS_COTOContainer.Count > 0 && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new HelperFinance_TOMaster
                {
                    ID = c.ID,
                    GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                    GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    DateConfig = c.DateConfig.Value,
                    VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                    TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                    VehicleID = c.VehicleID,
                    DriverID1 = c.DriverID1,
                    DriverID2 = c.DriverID2,
                    DriverID3 = c.DriverID3,
                    TypeOfDriverID1 = c.TypeOfDriverID1,
                    TypeOfDriverID2 = c.TypeOfDriverID2,
                    TypeOfDriverID3 = c.TypeOfDriverID3,
                    ExIsOverNight = c.ExIsOverNight,
                    ExIsOverWeight = c.ExIsOverWeight,
                    ExTotalDayOut = c.ExTotalDayOut,
                    ExTotalJoin = c.ExTotalJoin,
                    KM = c.KM > 0 ? c.KM : 0,
                    OPSContainerID = c.OPS_COTOContainer.OrderBy(d => d.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).ThenBy(d => d.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(d => d.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).FirstOrDefault().ID,
                }).ToList();

                foreach (var fixCost in lstFixCost)
                {
                    var asset = model.FLM_Asset.FirstOrDefault(c => c.ID == fixCost.AssetID);
                    HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                    itemExpr.Value = fixCost.Value;
                    itemExpr.TotalDay = schedule.TotalDays;
                    itemExpr.TotalDayActual = totalDayActual;
                    var lstMasterAsset = lstMaster.Where(c => c.VehicleID == asset.VehicleID).ToList();
                    if (lstMasterAsset != null && lstMasterAsset.Count > 0)
                    {
                        itemExpr.TotalSchedule = lstMasterAsset.Count;
                        itemExpr.TotalKM = lstMasterAsset.Sum(c => c.KM.Value);
                        itemExpr.TotalDaySchedule = lstMasterAsset.Select(c => c.DateConfig.Date).Distinct().Count();
                    }
                    var lstMasterCOAsset = lstMasterCO.Where(c => c.VehicleID == asset.VehicleID).ToList();
                    if (lstMasterCOAsset != null && lstMasterCOAsset.Count > 0)
                    {
                        itemExpr.TotalSchedule += lstMasterCOAsset.Count;
                        itemExpr.TotalKM += lstMasterCOAsset.Sum(c => c.KM.Value);
                        itemExpr.TotalDaySchedule += lstMasterCOAsset.Select(c => c.DateConfig.Date).Distinct().Count();
                    }
                    itemExpr.TotalDayOn = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                    itemExpr.TotalDayOff = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                    itemExpr.TotalDayHoliday = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);

                    #region Phân bổ theo ngày
                    if (!string.IsNullOrEmpty(asset.ExprDay))
                    {
                        decimal? priceFix = null;
                        priceFix = Expression_FLMGetValue(itemExpr, asset.ExprDay);
                        if (priceFix.HasValue)
                        {
                            var DateFrom = schedule.DateFrom.Date;
                            while (DateFrom <= schedule.DateTo.Date)
                            {
                                bool flag = false;
                                itemExpr.IsWorking = lstMaster.Any(c => c.VehicleID == asset.VehicleID) || lstMasterCO.Any(c => c.VehicleID == asset.VehicleID);
                                itemExpr.IsDayOn = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                                itemExpr.IsDayOff = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                                itemExpr.IsDayHoliday = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
                                try
                                {
                                    flag = Expression_FLMCheckBool(itemExpr, asset.ExprInputDay);
                                }
                                catch { }

                                if (flag)
                                {
                                    FIN_PL pl = new FIN_PL();
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.Code = string.Empty;
                                    pl.VehicleID = asset.VehicleID;
                                    pl.Effdate = DateFrom;
                                    pl.VendorID = Account.SYSCustomerID;
                                    pl.ScheduleID = schedule.ID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.FLMDepreciationNoGroup;
                                    plCost.TypeOfPriceDIExCode = CATCostType.FLMDepreciationNoGroup.ToString();
                                    plCost.Debit = priceFix.HasValue ? priceFix.Value : 0;
                                    pl.Debit = plCost.Debit;
                                    pl.FIN_PLDetails.Add(plCost);

                                    lstPL.Add(pl);
                                }

                                DateFrom = DateFrom.AddDays(1);
                            }
                        }
                    }
                    #endregion

                    #region Phân bổ cho từng chuyến
                    if (!string.IsNullOrEmpty(asset.ExprTOMaster) && lstMasterAsset != null && lstMasterAsset.Count > 0)
                    {
                        decimal? priceFix = null;
                        priceFix = Expression_FLMGetValue(itemExpr, asset.ExprTOMaster);
                        if (priceFix.HasValue)
                        {
                            #region Chuyến TOMaster
                            foreach (var itemMasterAsset in lstMasterAsset)
                            {
                                bool flag = false;
                                try
                                {
                                    flag = Expression_FLMCheckBool(itemExpr, asset.ExprInputTOMaster);
                                }
                                catch { }

                                if (flag)
                                {
                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.Code = string.Empty;
                                    pl.VehicleID = asset.VehicleID;
                                    pl.Effdate = itemMasterAsset.DateConfig.Date;
                                    pl.VendorID = Account.SYSCustomerID;
                                    pl.ScheduleID = schedule.ID;
                                    pl.DITOMasterID = itemMasterAsset.ID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.FLMDepreciationNoGroup;
                                    plCost.TypeOfPriceDIExCode = CATCostType.FLMDepreciationNoGroup.ToString();
                                    plCost.Debit = priceFix.HasValue ? priceFix.Value : 0;
                                    pl.Debit = plCost.Debit;
                                    pl.FIN_PLDetails.Add(plCost);

                                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                    plGroup.CreatedBy = Account.UserName;
                                    plGroup.CreatedDate = DateTime.Now;
                                    plGroup.GroupOfProductID = itemMasterAsset.OPSGroupProductID;
                                    plCost.FIN_PLGroupOfProduct.Add(plGroup);

                                    FIN_PL plPL = new FIN_PL();
                                    CopyFinPL(pl, plPL);
                                    plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    lstPL.Add(pl);
                                    lstPL.Add(plPL);
                                }
                            }
                            #endregion

                            #region Chuyến COMaster
                            foreach (var itemMasterAsset in lstMasterCOAsset)
                            {
                                bool flag = false;
                                try
                                {
                                    flag = Expression_FLMCheckBool(itemExpr, asset.ExprInputTOMaster);
                                }
                                catch { }

                                if (flag)
                                {
                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.Code = string.Empty;
                                    pl.VehicleID = asset.VehicleID;
                                    pl.Effdate = itemMasterAsset.DateConfig.Date;
                                    pl.VendorID = Account.SYSCustomerID;
                                    pl.ScheduleID = schedule.ID;
                                    pl.COTOMasterID = itemMasterAsset.ID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.FLMDepreciationNoGroup;
                                    plCost.TypeOfPriceDIExCode = CATCostType.FLMDepreciationNoGroup.ToString();
                                    plCost.Debit = priceFix.HasValue ? priceFix.Value : 0;
                                    pl.Debit = plCost.Debit;
                                    pl.FIN_PLDetails.Add(plCost);

                                    FIN_PLContainer plContainer = new FIN_PLContainer();
                                    plContainer.CreatedBy = Account.UserName;
                                    plContainer.CreatedDate = DateTime.Now;
                                    plContainer.COTOContainerID = itemMasterAsset.OPSContainerID;
                                    plCost.FIN_PLContainer.Add(plContainer);

                                    FIN_PL plPL = new FIN_PL();
                                    CopyFinPL(pl, plPL);
                                    plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    lstPL.Add(pl);
                                    lstPL.Add(plPL);
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

            #region Khấu hao phiếu
            var lstFixCostReceipt = model.FLM_FixedCost.Where(c => c.Month == schedule.Month && c.Year == schedule.Year && c.IsClosed && c.ReceiptID > 0 && !string.IsNullOrEmpty(c.FLM_Receipt.ExprDay) && !string.IsNullOrEmpty(c.FLM_Receipt.ExprInputDay));

            if (lstFixCostReceipt != null && lstFixCostReceipt.Count() > 0)
            {
                #region Danh sách chuyến
                //Lấy các chuyến
                var lstMaster = model.OPS_DITOMaster.Where(c => c.DateConfig.HasValue && c.VehicleID > 0 && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new HelperFinance_TOMaster
                {
                    ID = c.ID,
                    GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                    GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                    ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                    DateConfig = c.DateConfig.Value,
                    VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                    TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                    VehicleID = c.VehicleID,
                    DriverID1 = c.DriverID1,
                    DriverID2 = c.DriverID2,
                    DriverID3 = c.DriverID3,
                    TypeOfDriverID1 = c.TypeOfDriverID1,
                    TypeOfDriverID2 = c.TypeOfDriverID2,
                    TypeOfDriverID3 = c.TypeOfDriverID3,
                    ExIsOverNight = c.ExIsOverNight,
                    ExIsOverWeight = c.ExIsOverWeight,
                    ExTotalDayOut = c.ExTotalDayOut,
                    ExTotalJoin = c.ExTotalJoin,
                    KM = c.KM > 0 ? c.KM : 0,
                }).ToList();

                var lstMasterCO = model.OPS_COTOMaster.Where(c => c.DateConfig.HasValue && c.VehicleID > 0 && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new HelperFinance_TOMaster
                {
                    ID = c.ID,
                    GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                    GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    DateConfig = c.DateConfig.Value,
                    VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                    TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                    VehicleID = c.VehicleID,
                    DriverID1 = c.DriverID1,
                    DriverID2 = c.DriverID2,
                    DriverID3 = c.DriverID3,
                    TypeOfDriverID1 = c.TypeOfDriverID1,
                    TypeOfDriverID2 = c.TypeOfDriverID2,
                    TypeOfDriverID3 = c.TypeOfDriverID3,
                    ExIsOverNight = c.ExIsOverNight,
                    ExIsOverWeight = c.ExIsOverWeight,
                    ExTotalDayOut = c.ExTotalDayOut,
                    ExTotalJoin = c.ExTotalJoin,
                    KM = c.KM > 0 ? c.KM : 0,
                }).ToList();

                foreach (var fixCost in lstFixCostReceipt)
                {
                    var asset = model.FLM_Asset.FirstOrDefault(c => c.ID == fixCost.AssetID);
                    HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                    itemExpr.Value = fixCost.Value;
                    itemExpr.TotalDay = schedule.TotalDays;
                    itemExpr.TotalDayActual = totalDayActual;
                    var lstMasterAsset = lstMaster.Where(c => c.VehicleID == asset.VehicleID).ToList();
                    if (lstMasterAsset != null && lstMasterAsset.Count > 0)
                    {
                        itemExpr.TotalSchedule = lstMasterAsset.Count;
                        itemExpr.TotalKM = lstMasterAsset.Sum(c => c.KM.Value);
                        itemExpr.TotalDaySchedule = lstMasterAsset.Select(c => c.DateConfig.Date).Distinct().Count();
                    }
                    var lstMasterCOAsset = lstMasterCO.Where(c => c.VehicleID == asset.VehicleID).ToList();
                    if (lstMasterCOAsset != null && lstMasterCOAsset.Count > 0)
                    {
                        itemExpr.TotalSchedule += lstMasterCOAsset.Count;
                        itemExpr.TotalKM += lstMasterCOAsset.Sum(c => c.KM.Value);
                        itemExpr.TotalDaySchedule += lstMasterCOAsset.Select(c => c.DateConfig.Date).Distinct().Count();
                    }

                    itemExpr.TotalDayOn = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                    itemExpr.TotalDayOff = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                    itemExpr.TotalDayHoliday = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);

                    #region Phân bổ theo ngày
                    if (!string.IsNullOrEmpty(fixCost.FLM_Receipt.ExprDay))
                    {
                        decimal? priceFix = null;
                        priceFix = Expression_FLMGetValue(itemExpr, fixCost.FLM_Receipt.ExprDay);
                        if (priceFix.HasValue)
                        {
                            var DateFrom = schedule.DateFrom.Date;
                            while (DateFrom <= schedule.DateTo.Date)
                            {
                                bool flag = false;
                                itemExpr.IsWorking = lstMaster.Any(c => c.VehicleID == asset.VehicleID && c.DateConfig == DateFrom) || lstMasterCO.Any(c => c.VehicleID == asset.VehicleID && c.DateConfig == DateFrom);
                                itemExpr.IsDayOn = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                                itemExpr.IsDayOff = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                                itemExpr.IsDayHoliday = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
                                try
                                {
                                    flag = Expression_FLMCheckBool(itemExpr, fixCost.FLM_Receipt.ExprInputDay);
                                }
                                catch { }

                                if (flag)
                                {
                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.Code = string.Empty;
                                    pl.VehicleID = asset.VehicleID;
                                    pl.Effdate = DateFrom;
                                    pl.VendorID = Account.SYSCustomerID;
                                    pl.ScheduleID = schedule.ID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.FLMDepreciationReceiptNoGroup;
                                    plCost.TypeOfPriceDIExCode = CATCostType.FLMDepreciationReceiptNoGroup.ToString();
                                    plCost.Debit = priceFix.HasValue ? priceFix.Value : 0;
                                    pl.Debit = plCost.Debit;
                                    pl.FIN_PLDetails.Add(plCost);

                                    lstPL.Add(pl);
                                }

                                DateFrom = DateFrom.AddDays(1);
                            }
                        }
                    }
                    #endregion
                }
                #endregion
            }
            #endregion

            foreach (var pl in lstPL)
                model.FIN_PL.Add(pl);
        }

        // Phân bổ chi phí cố định cho xe và tài xế (lương căn bản, chi phí xe hàng tháng...)
        public static void Truck_CalculateFee(DataEntities model, AccountItem Account, int scheduleID)
        {
            List<FIN_PL> lstPL = new List<FIN_PL>();
            List<FIN_Temp> lstFinTemp = new List<FIN_Temp>();
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);
            var lstFINTemp = model.FIN_Temp.Where(c => c.ScheduleID == scheduleID);

            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);

            // Danh sách loại ngày
            var lstScheduleDate = model.FLM_ScheduleDate.Where(c => c.ScheduleID == schedule.ID);
            var lstScheduleDateDriver = model.FLM_ScheduleDateDetail.Where(c => c.FLM_ScheduleDate.ScheduleID == schedule.ID);
            var lstScheduleDriver = model.FLM_ScheduleDriver.Where(c => c.ScheduleID == schedule.ID);

            // Dữ liệu chuyến
            // Lấy danh sách opsgroup chạy trong schedule
            var lstOPSGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OPS_DITOMaster.ScheduleID == scheduleID && c.OPS_DITOMaster.VehicleID > 0 && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete).Select(c => new HelperFinance_OPSGroupProduct
            {
                ID = c.ID,
                OrderID = c.ORD_GroupProduct.OrderID,
                DITOMasterID = c.DITOMasterID.Value,
                VehicleID = c.OPS_DITOMaster.VehicleID,
                OrderGroupProductID = c.OrderGroupProductID.Value,
                GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID > 0 ? c.ORD_GroupProduct.GroupOfProductID.Value : -1,
                ContractID = c.OPS_DITOMaster.ContractID > 0 ? c.OPS_DITOMaster.ContractID.Value : -1,
                TonTranfer = c.TonTranfer,
                CBMTranfer = c.CBMTranfer,
                QuantityTranfer = c.QuantityTranfer,
                TonReturn = c.TonReturn,
                CBMReturn = c.CBMReturn,
                QuantityReturn = c.QuantityReturn,
                CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                VendorID = c.OPS_DITOMaster.VendorOfVehicleID.HasValue ? c.OPS_DITOMaster.VendorOfVehicleID.Value : Account.SYSCustomerID,
                DriverID1 = c.OPS_DITOMaster.DriverID1,
                DriverID2 = c.OPS_DITOMaster.DriverID2,
                DriverID3 = c.OPS_DITOMaster.DriverID3,
                TypeOfDriverID1 = c.OPS_DITOMaster.TypeOfDriverID1,
                TypeOfDriverID2 = c.OPS_DITOMaster.TypeOfDriverID2,
                TypeOfDriverID3 = c.OPS_DITOMaster.TypeOfDriverID3,
                DateConfig = c.DateConfig == null ? c.OPS_DITOMaster.DateConfig == null ? c.OPS_DITOMaster.ETD : c.OPS_DITOMaster.DateConfig : c.DateConfig,
                SortOrder = c.OPS_DITOMaster.SortOrder
            }).ToList();

            // Lấy danh sách opscontainer chạy trong schedule
            var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.OPS_COTOMaster.ScheduleID == scheduleID && c.OPS_COTOMaster.VehicleID > 0 && (c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel) && c.OPS_COTOMaster.DateConfig.HasValue && c.ParentID == null).Select(c => new HelperFinance_OPSContainer
            {
                ID = c.ID,
                OrderID = c.OPS_Container.ORD_Container.OrderID,
                COTOMasterID = c.COTOMasterID.Value,
                VehicleID = c.OPS_COTOMaster.VehicleID,
                OrderContainerID = c.OPS_Container.ContainerID,
                PackingID = c.OPS_Container.ORD_Container.PackingID,
                ContractID = c.OPS_COTOMaster.ContractID > 0 ? c.OPS_COTOMaster.ContractID.Value : -1,
                CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                VendorID = c.OPS_COTOMaster.VendorOfVehicleID.HasValue ? c.OPS_COTOMaster.VendorOfVehicleID.Value : Account.SYSCustomerID,
                DriverID1 = c.OPS_COTOMaster.DriverID1,
                DriverID2 = c.OPS_COTOMaster.DriverID2,
                DriverID3 = c.OPS_COTOMaster.DriverID3,
                TypeOfDriverID1 = c.OPS_COTOMaster.TypeOfDriverID1,
                TypeOfDriverID2 = c.OPS_COTOMaster.TypeOfDriverID2,
                TypeOfDriverID3 = c.OPS_COTOMaster.TypeOfDriverID3,
                DateConfig = c.OPS_COTOMaster.DateConfig,
                SortOrder = c.OPS_COTOMaster.SortOrder,
                StatusOfCOContainerID = c.StatusOfCOContainerID,
            }).ToList();

            #region Tính cho Asset => ko lưu driverID
            var lstScheduleFeeAsset = model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.AssetID.HasValue && ((!string.IsNullOrEmpty(c.ExprPriceDay) && !string.IsNullOrEmpty(c.ExprInputDay)) || (!string.IsNullOrEmpty(c.ExprPriceTOMaster) && !string.IsNullOrEmpty(c.ExprInputTOMaster))));
            if (lstScheduleFeeAsset != null && lstScheduleFeeAsset.Count() > 0)
            {
                foreach (var itemScheduleFeeAsset in lstScheduleFeeAsset)
                {
                    var lstOPSGroupProductAsset = lstOPSGroupProduct.Where(c => c.VehicleID == itemScheduleFeeAsset.FLM_Asset.VehicleID);
                    var lstOPSContainerAsset = lstOPSContainer.Where(c => c.VehicleID == itemScheduleFeeAsset.FLM_Asset.VehicleID);
                    // Item check công thức
                    HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                    itemCheck.Price = itemScheduleFeeAsset.Price.HasValue ? itemScheduleFeeAsset.Price.Value : 0;
                    itemCheck.TotalDay = schedule.TotalDays;
                    itemCheck.TotalDayActual = (schedule.DateTo.Date - schedule.DateFrom.Date).TotalDays + 1;
                    itemCheck.TotalDayHoliday = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
                    itemCheck.TotalDayOff = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                    itemCheck.TotalDayOn = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                    if (lstOPSGroupProductAsset.Count() > 0)
                    {
                        itemCheck.TotalTon = lstOPSGroupProductAsset.Sum(c => c.TonTranfer);
                        itemCheck.TotalCBM = lstOPSGroupProductAsset.Sum(c => c.CBMTranfer);
                        itemCheck.TotalSchedule = lstOPSGroupProductAsset.Select(c => c.DITOMasterID).Distinct().Count();
                        itemCheck.TotalDaySchedule = lstOPSGroupProductAsset.Select(c => c.DateConfig.Value.Date).Distinct().Count();
                    }
                    if (lstOPSContainerAsset.Count() > 0)
                    {
                        itemCheck.TotalPacking = lstOPSContainerAsset.Select(c => c.OrderContainerID).Distinct().Count();
                        itemCheck.TotalSchedule = lstOPSContainerAsset.Select(c => c.COTOMasterID).Distinct().Count();
                        itemCheck.TotalDaySchedule = lstOPSContainerAsset.Select(c => c.DateConfig.Value.Date).Distinct().Count();
                    }

                    #region Phân bổ cho từng ngày của schedule
                    if (!string.IsNullOrEmpty(itemScheduleFeeAsset.ExprPriceDay))
                    {
                        DateTime DateFrom = schedule.DateFrom.Date;
                        while (DateFrom <= schedule.DateTo.Date)
                        {
                            itemCheck.TotalTonInDay = 0;
                            itemCheck.TotalCBMInDay = 0;
                            itemCheck.TotalScheduleInDay = 0;
                            itemCheck.TotalPackingInDay = 0;

                            var lstGroupInDay = lstOPSGroupProductAsset.Where(c => c.DateConfig == DateFrom);
                            if (lstGroupInDay != null && lstGroupInDay.Count() > 0)
                            {
                                itemCheck.TotalTonInDay = lstGroupInDay.Sum(c => c.TonTranfer);
                                itemCheck.TotalCBMInDay = lstGroupInDay.Sum(c => c.CBMTranfer);
                                itemCheck.TotalScheduleInDay = lstGroupInDay.Select(c => c.DITOMasterID).Distinct().Count();
                            }
                            var lstContainerInDay = lstOPSContainerAsset.Where(c => c.DateConfig == DateFrom);
                            if (lstContainerInDay != null && lstContainerInDay.Count() > 0)
                            {
                                itemCheck.TotalPackingInDay = lstContainerInDay.Select(c => c.OrderContainerID).Distinct().Count();
                                itemCheck.TotalScheduleInDay = lstContainerInDay.Select(c => c.COTOMasterID).Distinct().Count();
                            }

                            bool flag = false;
                            itemCheck.IsWorking = lstOPSGroupProductAsset.Any(c => c.DateConfig == DateFrom) || lstContainerInDay.Any(c => c.DateConfig == DateFrom);
                            itemCheck.IsDayOn = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                            itemCheck.IsDayOff = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                            itemCheck.IsDayHoliday = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeAsset.ExprInputDay);
                            }
                            catch { }

                            if (flag)
                            {
                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeAsset.ExprPriceDay);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeAsset.ExprPriceDay);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateFrom.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.VehicleID = itemScheduleFeeAsset.FLM_Asset.VehicleID;
                                pl.ScheduleID = schedule.ID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                lstPL.Add(pl);

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFeeNoGroup;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.Code;
                                plCost.Note = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.TypeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                            }
                            DateFrom = DateFrom.AddDays(1);
                        }
                    }
                    #endregion

                    #region Phân bổ cho từng chuyến xe tải
                    if (!string.IsNullOrEmpty(itemScheduleFeeAsset.ExprPriceTOMaster))
                    {
                        var lstMasterID = lstOPSGroupProductAsset.Select(c => c.DITOMasterID).Distinct().ToList();
                        foreach (var itemMasterID in lstMasterID)
                        {
                            var lstGroup = lstOPSGroupProductAsset.Where(c => c.DITOMasterID == itemMasterID).OrderByDescending(c => c.TonTranfer);
                            var master = lstOPSGroupProductAsset.FirstOrDefault(c => c.DITOMasterID == itemMasterID);
                            itemCheck.TotalTonInSchedule = lstGroup.Sum(c => c.TonTranfer);
                            itemCheck.TotalCBMInSchedule = lstGroup.Sum(c => c.CBMTranfer);
                            itemCheck.SortInDay = master.SortOrder;

                            bool flag = false;

                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeAsset.ExprInputTOMaster);
                            }
                            catch { }

                            if (flag)
                            {
                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeAsset.ExprPriceTOMaster);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeAsset.ExprPriceTOMaster);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = lstGroup.FirstOrDefault().DateConfig.Value.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.VehicleID = itemScheduleFeeAsset.FLM_Asset.VehicleID;
                                pl.ScheduleID = schedule.ID;
                                pl.DITOMasterID = itemMasterID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.Code;
                                plCost.Note = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.TypeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                                // Group
                                FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                plGroup.CreatedBy = Account.UserName;
                                plGroup.CreatedDate = DateTime.Now;
                                plGroup.GroupOfProductID = lstGroup.FirstOrDefault().ID;
                                plCost.FIN_PLGroupOfProduct.Add(plGroup);

                                var lstGroupID = lstGroup.Select(c => c.ID).Distinct().ToList();
                                var temp = lstFINTemp.FirstOrDefault(c => c.DITOGroupProductID.HasValue && lstGroupID.Contains(c.DITOGroupProductID.Value) && c.DITOMasterID == itemMasterID);
                                if (temp != null)
                                    plGroup.GroupOfProductID = temp.DITOGroupProductID.Value;
                                else
                                {
                                    FIN_Temp finTemp = new FIN_Temp();
                                    finTemp.CreatedBy = Account.UserName;
                                    finTemp.CreatedDate = DateTime.Now;
                                    finTemp.DITOMasterID = itemMasterID;
                                    finTemp.DITOGroupProductID = plGroup.GroupOfProductID;
                                    finTemp.ScheduleID = schedule.ID;
                                    lstFinTemp.Add(finTemp);
                                }

                                FIN_PL plPL = new FIN_PL();
                                CopyFinPL(pl, plPL);
                                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                lstPL.Add(pl);
                                lstPL.Add(plPL);
                            }
                        }
                    }
                    #endregion

                    #region Phân bổ cho từng chuyến xe Con
                    if (!string.IsNullOrEmpty(itemScheduleFeeAsset.ExprPriceTOMaster))
                    {
                        var lstMasterID = lstOPSContainer.Select(c => c.COTOMasterID).Distinct().ToList();
                        foreach (var itemMasterID in lstMasterID)
                        {
                            var lstGroup = lstOPSContainerAsset.Where(c => c.COTOMasterID == itemMasterID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden);
                            var master = lstOPSContainerAsset.FirstOrDefault(c => c.COTOMasterID == itemMasterID);
                            itemCheck.TotalPackingInDay = lstGroup.Select(c => c.OrderContainerID).Distinct().Count();
                            itemCheck.SortInDay = master.SortOrder;

                            bool flag = false;

                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeAsset.ExprInputTOMaster);
                            }
                            catch { }

                            if (flag)
                            {
                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeAsset.ExprPriceTOMaster);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeAsset.ExprPriceTOMaster);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = lstGroup.FirstOrDefault().DateConfig.Value.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.VehicleID = itemScheduleFeeAsset.FLM_Asset.VehicleID;
                                pl.ScheduleID = schedule.ID;
                                pl.COTOMasterID = itemMasterID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.Code;
                                plCost.Note = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.TypeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                                // Group
                                FIN_PLContainer plContainer = new FIN_PLContainer();
                                plContainer.CreatedBy = Account.UserName;
                                plContainer.CreatedDate = DateTime.Now;
                                plContainer.COTOContainerID = lstGroup.FirstOrDefault().ID;
                                plCost.FIN_PLContainer.Add(plContainer);

                                var lstGroupID = lstGroup.Select(c => c.ID).Distinct().ToList();
                                var temp = lstFINTemp.FirstOrDefault(c => c.COTOContainerID.HasValue && lstGroupID.Contains(c.COTOContainerID.Value));
                                if (temp != null)
                                    plContainer.COTOContainerID = temp.COTOContainerID.Value;
                                else
                                {
                                    FIN_Temp finTemp = new FIN_Temp();
                                    finTemp.CreatedBy = Account.UserName;
                                    finTemp.CreatedDate = DateTime.Now;
                                    finTemp.COTOContainerID = plContainer.COTOContainerID;
                                    finTemp.ScheduleID = schedule.ID;
                                    lstFinTemp.Add(finTemp);
                                }

                                FIN_PL plPL = new FIN_PL();
                                CopyFinPL(pl, plPL);
                                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                lstPL.Add(pl);
                                lstPL.Add(plPL);
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Tính cho Driver
            var lstScheduleFeeDriverOther = model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.DriverID.HasValue).Select(c => new HelperFinance_FLMCheckDetail
            {
                DriverID = c.DriverID.Value,
                TypeOfScheduleFeeID = c.TypeOfScheduleFeeID,
                TypeOfScheduleFeeCode = c.FLM_TypeOfScheduleFee.Code,
                TypeOfScheduleFeeDay = c.Day.HasValue ? c.Day.Value : 0,
            }).ToList();

            var lstScheduleFeeDriver = model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.DriverID.HasValue && ((!string.IsNullOrEmpty(c.ExprPriceDay) && !string.IsNullOrEmpty(c.ExprInputDay)) || (!string.IsNullOrEmpty(c.ExprPriceTOMaster) && !string.IsNullOrEmpty(c.ExprInputTOMaster)))).Select(c => new
            {
                DriverID = c.DriverID,
                Price = c.Price,
                ExprPriceDay = c.ExprPriceDay,
                ExprInputDay = c.ExprInputDay,
                AssetID = c.AssetID,
                VehicleID = c.AssetID > 0 ? c.FLM_Asset.VehicleID : null,
                TypeOfScheduleFeeCode = c.FLM_TypeOfScheduleFee.Code,
                TypeOfScheduleFeeName = c.FLM_TypeOfScheduleFee.TypeName,
                ExprPriceTOMaster = c.ExprPriceTOMaster,
                ExprInputTOMaster = c.ExprInputTOMaster,

            }).ToList();
            if (lstScheduleFeeDriver != null && lstScheduleFeeDriver.Count() > 0)
            {
                foreach (var itemScheduleFeeDriver in lstScheduleFeeDriver)
                {
                    var lstOPSGroupProductDriver = lstOPSGroupProduct.Where(c => (c.DriverID1 == itemScheduleFeeDriver.DriverID || c.DriverID2 == itemScheduleFeeDriver.DriverID || c.DriverID3 == itemScheduleFeeDriver.DriverID));
                    var lstOPSContainerDriver = lstOPSContainer.Where(c => (c.DriverID1 == itemScheduleFeeDriver.DriverID || c.DriverID2 == itemScheduleFeeDriver.DriverID || c.DriverID3 == itemScheduleFeeDriver.DriverID));
                    // Item check công thức
                    HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                    itemCheck.Price = itemScheduleFeeDriver.Price.HasValue ? itemScheduleFeeDriver.Price.Value : 0;
                    itemCheck.TotalDay = schedule.TotalDays;
                    itemCheck.TotalDayActual = (schedule.DateTo.Date - schedule.DateFrom.Date).TotalDays + 1;
                    if (lstOPSGroupProductDriver.Count() > 0)
                    {
                        itemCheck.TotalTon = lstOPSGroupProductDriver.Sum(c => c.TonTranfer);
                        itemCheck.TotalCBM = lstOPSGroupProductDriver.Sum(c => c.CBMTranfer);
                        itemCheck.TotalSchedule = lstOPSGroupProductDriver.Select(c => c.DITOMasterID).Distinct().Count();
                        itemCheck.TotalDaySchedule = lstOPSGroupProductDriver.Select(c => c.DateConfig.Value.Date).Distinct().Count();
                    }
                    if (lstOPSContainerDriver.Count() > 0)
                    {
                        itemCheck.TotalPacking = lstOPSContainerDriver.Select(c => c.OrderContainerID).Distinct().Count();
                        itemCheck.TotalSchedule += lstOPSContainerDriver.Select(c => c.COTOMasterID).Distinct().Count();
                        itemCheck.TotalDaySchedule = lstOPSContainerDriver.Select(c => c.DateConfig.Value.Date).Distinct().Count();
                    }
                    // Param bổ sung
                    var scheduleDriver = model.FLM_ScheduleDriver.FirstOrDefault(c => c.ScheduleID == schedule.ID && c.DriverID == itemScheduleFeeDriver.DriverID);
                    if (scheduleDriver != null)
                    {
                        itemCheck.FeeBase = scheduleDriver.FeeBase;
                        itemCheck.TotalDayOffDriver = scheduleDriver.DaysWorkInOff.HasValue ? scheduleDriver.DaysWorkInOff.Value : 0;
                        itemCheck.TotalDayOnDriver = scheduleDriver.DaysWorkInOn.HasValue ? scheduleDriver.DaysWorkInOn.Value : 0;
                        itemCheck.TotalDayHoliday = scheduleDriver.DaysWorkInHoliday.HasValue ? scheduleDriver.DaysWorkInHoliday.Value : 0;
                        itemCheck.TotalDayAllowOffDriver = scheduleDriver.DaysAllowOff;
                        itemCheck.TotalDayAllowOffRemainDriver = scheduleDriver.DaysAllowOffRemain.HasValue ? scheduleDriver.DaysAllowOffRemain.Value : 0;
                        itemCheck.IsAssistant = scheduleDriver.IsAssistant;
                    }

                    // Param ngày tự định nghĩa
                    itemCheck.lstCheckDetail = new List<HelperFinance_FLMCheckDetail>();
                    var lstGroupCheckDetail = lstScheduleFeeDriverOther.Where(c => c.DriverID == itemScheduleFeeDriver.DriverID).GroupBy(c => new { c.TypeOfScheduleFeeID, c.TypeOfScheduleFeeCode }).ToList();
                    foreach (var itemGroupCheckDetail in lstGroupCheckDetail)
                    {
                        HelperFinance_FLMCheckDetail itemCheckDetail = new HelperFinance_FLMCheckDetail();
                        itemCheckDetail.TypeOfScheduleFeeCode = itemGroupCheckDetail.Key.TypeOfScheduleFeeCode;
                        itemCheckDetail.TypeOfScheduleFeeDay = itemGroupCheckDetail.Sum(c => c.TypeOfScheduleFeeDay);
                        itemCheck.lstCheckDetail.Add(itemCheckDetail);
                    }

                    #region Phân bổ cho từng ngày của schedule
                    if (!string.IsNullOrEmpty(itemScheduleFeeDriver.ExprPriceDay))
                    {
                        DateTime DateFrom = schedule.DateFrom.Date;
                        while (DateFrom <= schedule.DateTo.Date)
                        {
                            bool flag = false;
                            itemCheck.IsWorking = lstScheduleDateDriver.Any(c => c.DriverID == itemScheduleFeeDriver.DriverID && c.FLM_ScheduleDate.Date == DateFrom);
                            itemCheck.IsDayOn = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                            itemCheck.IsDayOff = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                            itemCheck.IsDayHoliday = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
                            itemCheck.IsDaySchedule = lstOPSGroupProductDriver.Any(c => c.DateConfig.Value.Date == DateFrom) || lstOPSContainerDriver.Any(c => c.DateConfig.Value.Date == DateFrom);
                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeDriver.ExprInputDay);
                            }
                            catch { }
                            if (flag)
                            {
                                itemCheck.TotalTonInDay = 0;
                                itemCheck.TotalCBMInDay = 0;
                                itemCheck.TotalScheduleInDay = 0;
                                itemCheck.TotalPackingInDay = 0;

                                var lstGroupInDay = lstOPSGroupProductDriver.Where(c => c.DateConfig == DateFrom);
                                if (lstGroupInDay != null && lstGroupInDay.Count() > 0)
                                {
                                    itemCheck.TotalTonInDay = lstGroupInDay.Sum(c => c.TonTranfer);
                                    itemCheck.TotalCBMInDay = lstGroupInDay.Sum(c => c.CBMTranfer);
                                    itemCheck.TotalScheduleInDay = lstGroupInDay.Select(c => c.DITOMasterID).Distinct().Count();
                                }
                                var lstContainerInDay = lstOPSContainerDriver.Where(c => c.DateConfig == DateFrom);
                                if (lstContainerInDay != null && lstContainerInDay.Count() > 0)
                                {
                                    itemCheck.TotalPackingInDay = lstContainerInDay.Select(c => c.OrderContainerID).Distinct().Count();
                                    itemCheck.TotalScheduleInDay = lstContainerInDay.Select(c => c.COTOMasterID).Distinct().Count();
                                }

                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeDriver.ExprPriceDay);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeDriver.ExprPriceDay);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateFrom.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.DriverID = itemScheduleFeeDriver.DriverID;
                                pl.ScheduleID = schedule.ID;
                                pl.VehicleID = itemScheduleFeeDriver.VehicleID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                lstPL.Add(pl);

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFeeNoGroup;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeDriver.TypeOfScheduleFeeCode;
                                plCost.Note = itemScheduleFeeDriver.TypeOfScheduleFeeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                            }

                            DateFrom = DateFrom.AddDays(1);
                        }
                    }
                    #endregion

                    #region Phân bổ cho từng chuyến xe tải
                    if (!string.IsNullOrEmpty(itemScheduleFeeDriver.ExprPriceTOMaster))
                    {
                        var lstMasterID = lstOPSGroupProductDriver.Select(c => c.DITOMasterID).Distinct().ToList();
                        foreach (var itemMasterID in lstMasterID)
                        {
                            var lstGroup = lstOPSGroupProductDriver.Where(c => c.DITOMasterID == itemMasterID).OrderByDescending(c => c.TonTranfer);
                            var master = lstOPSGroupProductDriver.FirstOrDefault(c => c.DITOMasterID == itemMasterID);
                            itemCheck.TotalTonInSchedule = lstGroup.Sum(c => c.TonTranfer);
                            itemCheck.TotalCBMInSchedule = lstGroup.Sum(c => c.CBMTranfer);
                            itemCheck.SortInDay = master.SortOrder;

                            bool flag = false;

                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeDriver.ExprInputTOMaster);
                            }
                            catch { }

                            if (flag)
                            {
                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeDriver.ExprPriceTOMaster);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeDriver.ExprPriceTOMaster);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = lstGroup.FirstOrDefault().DateConfig.Value.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.DriverID = itemScheduleFeeDriver.DriverID;
                                pl.ScheduleID = schedule.ID;
                                pl.VehicleID = master.VehicleID;
                                pl.DITOMasterID = master.DITOMasterID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeDriver.TypeOfScheduleFeeCode;
                                plCost.Note = itemScheduleFeeDriver.TypeOfScheduleFeeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                                // Group
                                FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                plGroup.CreatedBy = Account.UserName;
                                plGroup.CreatedDate = DateTime.Now;
                                plGroup.GroupOfProductID = lstGroup.FirstOrDefault().ID;
                                plCost.FIN_PLGroupOfProduct.Add(plGroup);

                                var lstGroupID = lstGroup.Select(c => c.ID).Distinct().ToList();
                                var temp = lstFINTemp.FirstOrDefault(c => c.DITOGroupProductID.HasValue && lstGroupID.Contains(c.DITOGroupProductID.Value) && c.DITOMasterID == itemMasterID);
                                if (temp != null)
                                    plGroup.GroupOfProductID = temp.DITOGroupProductID.Value;
                                else
                                {
                                    FIN_Temp finTemp = new FIN_Temp();
                                    finTemp.CreatedBy = Account.UserName;
                                    finTemp.CreatedDate = DateTime.Now;
                                    finTemp.DITOMasterID = itemMasterID;
                                    finTemp.DITOGroupProductID = plGroup.GroupOfProductID;
                                    finTemp.ScheduleID = schedule.ID;
                                    lstFinTemp.Add(finTemp);
                                }

                                FIN_PL plPL = new FIN_PL();
                                CopyFinPL(pl, plPL);
                                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                lstPL.Add(pl);
                                lstPL.Add(plPL);
                            }
                        }
                    }
                    #endregion

                    #region Phân bổ cho từng chuyến xe Con
                    if (!string.IsNullOrEmpty(itemScheduleFeeDriver.ExprPriceTOMaster))
                    {
                        var lstMasterID = lstOPSContainerDriver.Select(c => c.COTOMasterID).Distinct().ToList();
                        foreach (var itemMasterID in lstMasterID)
                        {
                            var lstGroup = lstOPSContainerDriver.Where(c => c.COTOMasterID == itemMasterID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden);
                            var master = lstOPSContainerDriver.FirstOrDefault(c => c.COTOMasterID == itemMasterID);
                            itemCheck.TotalPackingInDay = lstGroup.Select(c => c.OrderContainerID).Distinct().Count();
                            itemCheck.SortInDay = master.SortOrder;

                            bool flag = false;

                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeDriver.ExprInputTOMaster);
                            }
                            catch { }

                            if (flag)
                            {
                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeDriver.ExprPriceTOMaster);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeDriver.ExprPriceTOMaster);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = lstGroup.FirstOrDefault().DateConfig.Value.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.DriverID = itemScheduleFeeDriver.DriverID;
                                pl.ScheduleID = schedule.ID;
                                pl.VehicleID = master.VehicleID;
                                pl.COTOMasterID = master.COTOMasterID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeDriver.TypeOfScheduleFeeCode;
                                plCost.Note = itemScheduleFeeDriver.TypeOfScheduleFeeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                                // Group
                                FIN_PLContainer plContainer = new FIN_PLContainer();
                                plContainer.CreatedBy = Account.UserName;
                                plContainer.CreatedDate = DateTime.Now;
                                plContainer.COTOContainerID = lstGroup.FirstOrDefault().ID;
                                plCost.FIN_PLContainer.Add(plContainer);

                                var lstGroupID = lstGroup.Select(c => c.ID).Distinct().ToList();
                                var temp = lstFINTemp.FirstOrDefault(c => c.COTOContainerID.HasValue && lstGroupID.Contains(c.COTOContainerID.Value));
                                if (temp != null)
                                    plContainer.COTOContainerID = temp.COTOContainerID.Value;
                                else
                                {
                                    FIN_Temp finTemp = new FIN_Temp();
                                    finTemp.CreatedBy = Account.UserName;
                                    finTemp.CreatedDate = DateTime.Now;
                                    finTemp.COTOContainerID = plContainer.COTOContainerID;
                                    finTemp.ScheduleID = schedule.ID;
                                    lstFinTemp.Add(finTemp);
                                }

                                FIN_PL plPL = new FIN_PL();
                                CopyFinPL(pl, plPL);
                                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                lstPL.Add(pl);
                                lstPL.Add(plPL);
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            foreach (var pl in lstPL)
                model.FIN_PL.Add(pl);
            foreach (var pl in lstFinTemp)
                model.FIN_Temp.Add(pl);
        }

        // Tính các phiếu ko khấu hao
        public static void Truck_CalculateReceipt(DataEntities model, AccountItem Account, int scheduleID)
        {
            const int iMaintence = -(int)SYSVarType.TypeOfReceiptMaintence;
            const int iRegistry = -(int)SYSVarType.TypeOfReceiptRegistry;
            const int iRepairLarge = -(int)SYSVarType.TypeOfReceiptRepairLarge;
            const int iRepairSmall = -(int)SYSVarType.TypeOfReceiptRepairSmall;
            const int iMaterial = -(int)SYSVarType.TypeOfReceiptMaterial;

            List<FIN_PL> lstPL = new List<FIN_PL>();
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);

            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);

            #region Receipt ko khấu hao
            var lstReceiptCost = model.FLM_ReceiptCost.Where(c => c.ReceiptID > 0 && c.FLM_Receipt.DateReceipt >= DateConfig && c.FLM_Receipt.DateReceipt < DateConfigEnd && c.FLM_Receipt.SYSCustomerID == Account.SYSCustomerID && !c.FLM_Receipt.IsDepreciation).Select(c => new
            {
                c.Amount,
                c.FLM_Receipt.Code,
                //c.FLM_Receipt.DITOMasterID,
                c.FLM_Receipt.TypeOfReceiptID,
                TypeOfReceiptCode = c.FLM_Receipt.SYS_Var.Code,
                TypeOfReceiptName = c.FLM_Receipt.SYS_Var.ValueOfVar,
                c.FLM_Receipt.DateReceipt,
                VehicleID = c.FLM_Asset.VehicleID,
            }).ToList();

            foreach (var itemReceiptCost in lstReceiptCost)
            {
                // Tạo pl
                FIN_PL pl = new FIN_PL();
                pl.IsPlanning = false;
                pl.Effdate = itemReceiptCost.DateReceipt.Date;
                pl.Code = string.Empty;
                pl.CreatedBy = Account.UserName;
                pl.CreatedDate = DateTime.Now;
                pl.SYSCustomerID = Account.SYSCustomerID;
                pl.VendorID = Account.SYSCustomerID;
                pl.VehicleID = itemReceiptCost.VehicleID;
                pl.ScheduleID = schedule.ID;
                //pl.DITOMasterID = itemReceiptCost.DITOMasterID;
                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                lstPL.Add(pl);

                // Detail
                FIN_PLDetails plCost = new FIN_PLDetails();
                plCost.CreatedBy = Account.UserName;
                plCost.CreatedDate = DateTime.Now;
                plCost.Debit = itemReceiptCost.Amount;
                plCost.TypeOfPriceDIExCode = itemReceiptCost.TypeOfReceiptCode;
                plCost.Note1 = itemReceiptCost.TypeOfReceiptName;
                pl.Debit = plCost.Debit;
                switch (itemReceiptCost.TypeOfReceiptID)
                {
                    case iMaintence:
                        plCost.CostID = (int)CATCostType.FLMReceiptMaintence;
                        break;
                    case iRegistry:
                        plCost.CostID = (int)CATCostType.FLMReceiptRegistry;
                        break;
                    case iRepairLarge:
                        plCost.CostID = (int)CATCostType.FLMReceiptRepairLarge;
                        break;
                    case iRepairSmall:
                        plCost.CostID = (int)CATCostType.FLMReceiptRepairSmall;
                        break;
                    case iMaterial:
                        plCost.CostID = (int)CATCostType.FLMReceiptMaterial;
                        break;
                }

                if (plCost.CostID > 0)
                    pl.FIN_PLDetails.Add(plCost);
            }
            #endregion

            foreach (var pl in lstPL)
                model.FIN_PL.Add(pl);
        }

        // Chi phí trạm
        public static void Truck_CalculateStation(DataEntities model, AccountItem Account, int scheduleID)
        {
            List<FIN_PL> lstPL = new List<FIN_PL>();
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);

            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);

            #region Phân bổ theo tháng
            var lstStationMonth = model.CAT_StationMonth.Where(c => c.ScheduleID == scheduleID && c.Price > 0 && !string.IsNullOrEmpty(c.ExprInputDay) && !string.IsNullOrEmpty(c.ExprDay)).Select(c => new
            {
                c.LocationID,
                c.FLM_Asset.VehicleID,
                c.DateFrom,
                c.DateTo,
                c.Price,
                Location = c.CAT_Location.Location,
                LocationAddress = c.CAT_Location.Address,
                c.ExprDay,
                c.ExprInputDay,
            }).ToList();

            var lstMaster = model.OPS_DITOMaster.Where(c => c.ScheduleID == scheduleID && c.DateConfig.HasValue && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.VehicleID > 0 && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new
            {
                c.VehicleID,
                c.DateConfig
            }).ToList();

            var lstScheduleDate = model.FLM_ScheduleDate.Where(c => c.ScheduleID == schedule.ID);
            int totalDayOn = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
            int totalDayOff = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
            int totalDayHoliday = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
            HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
            itemCheck.TotalDay = schedule.TotalDays;
            itemCheck.TotalDayOn = totalDayOn;
            itemCheck.TotalDayOff = totalDayOff;
            itemCheck.TotalDayHoliday = totalDayHoliday;

            foreach (var item in lstStationMonth)
            {
                itemCheck.Price = item.Price;
                itemCheck.Value = item.Price;
                decimal? price = null;
                try
                {
                    price = Expression_FLMGetValue(itemCheck, item.ExprDay);
                }
                catch { }
                if (price.HasValue)
                {
                    var DateFrom = item.DateFrom.Date;
                    while (DateFrom <= item.DateTo.Date)
                    {
                        bool flag = false;
                        itemCheck.IsWorking = lstMaster.Any(c => c.VehicleID == item.VehicleID && c.DateConfig.Value.Date == DateFrom);
                        itemCheck.IsDayOn = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                        itemCheck.IsDayOff = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                        itemCheck.IsDayHoliday = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
                        try
                        {
                            flag = Expression_FLMCheckBool(itemCheck, item.ExprInputDay);
                        }
                        catch { }
                        if (flag)
                        {
                            // Tạo pl
                            FIN_PL pl = new FIN_PL();
                            pl.IsPlanning = false;
                            pl.Effdate = DateFrom.Date;
                            pl.Code = string.Empty;
                            pl.CreatedBy = Account.UserName;
                            pl.CreatedDate = DateTime.Now;
                            pl.SYSCustomerID = Account.SYSCustomerID;
                            pl.VendorID = Account.SYSCustomerID;
                            pl.VehicleID = item.VehicleID;
                            pl.ScheduleID = schedule.ID;
                            pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                            // Detail
                            FIN_PLDetails plCost = new FIN_PLDetails();
                            plCost.CreatedBy = Account.UserName;
                            plCost.CreatedDate = DateTime.Now;
                            plCost.CostID = (int)CATCostType.StationDebit;
                            plCost.TypeOfPriceDIExCode = CATCostType.StationDebit.ToString();
                            plCost.Debit = price.Value;
                            plCost.Note = item.Location;
                            plCost.Note1 = item.LocationAddress;
                            pl.Debit = plCost.Debit;
                            pl.FIN_PLDetails.Add(plCost);
                            lstPL.Add(pl);
                        }
                        DateFrom = DateFrom.AddDays(1);
                    }
                }
            }

            #endregion

            #region Phân bổ theo lượt
            var lstOPSDIStation = model.OPS_DITOStation.Where(c => c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.VehicleID > 0 && c.OPS_DITOMaster.ScheduleID == scheduleID && c.IsApproved && !c.IsMonth && c.OPS_DITOMaster.OPS_DITOGroupProduct.Count > 0 && (c.OPS_DITOMaster.DateConfig.HasValue || c.OPS_DITOMaster.ETD.HasValue)).Select(c => new
            {
                c.ID,
                c.DITOMasterID,
                VehicleID = c.OPS_DITOMaster.VehicleID.Value,
                c.DITOLocationID,
                c.LocationID,
                Location = c.CAT_Location.Location,
                LocationAddress = c.CAT_Location.Address,
                c.Price,
                DateConfig = c.OPS_DITOMaster.DateConfig.HasValue ? c.OPS_DITOMaster.DateConfig.Value : c.OPS_DITOMaster.ETD.Value,
                OPSGroupProductID = c.OPS_DITOMaster.OPS_DITOGroupProduct.OrderByDescending(d => d.TonTranfer).FirstOrDefault().ID,
            }).ToList();

            var lstOPSCOStation = model.OPS_COTOStation.Where(c => c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_COTOMaster.VehicleID > 0 && c.OPS_COTOMaster.ScheduleID == scheduleID && c.IsApproved && !c.IsMonth && c.OPS_COTOMaster.OPS_COTOContainer.Count > 0).Select(c => new
            {
                c.ID,
                c.COTOMasterID,
                VehicleID = c.OPS_COTOMaster.VehicleID.Value,
                c.COTOLocationID,
                c.LocationID,
                Location = c.CAT_Location.Location,
                LocationAddress = c.CAT_Location.Address,
                c.Price,
                DateConfig = c.OPS_COTOMaster.DateConfig.HasValue ? c.OPS_COTOMaster.DateConfig.Value : c.OPS_COTOMaster.ETD,
                OPSContainerID = c.OPS_COTOMaster.OPS_COTOContainer.OrderBy(d => d.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || d.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || d.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).FirstOrDefault().ID,
            }).ToList();

            foreach (var item in lstOPSDIStation)
            {
                // Tạo pl
                FIN_PL pl = new FIN_PL();
                pl.IsPlanning = false;
                pl.Effdate = item.DateConfig.Date;
                pl.Code = string.Empty;
                pl.CreatedBy = Account.UserName;
                pl.CreatedDate = DateTime.Now;
                pl.SYSCustomerID = Account.SYSCustomerID;
                pl.VendorID = Account.SYSCustomerID;
                pl.VehicleID = item.VehicleID;
                pl.ScheduleID = schedule.ID;
                pl.DITOMasterID = item.DITOMasterID;
                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                // Detail
                FIN_PLDetails plCost = new FIN_PLDetails();
                plCost.CreatedBy = Account.UserName;
                plCost.CreatedDate = DateTime.Now;
                plCost.CostID = (int)CATCostType.StationDebit;
                plCost.TypeOfPriceDIExCode = CATCostType.StationDebit.ToString();
                plCost.Debit = item.Price;
                plCost.Note = item.Location;
                plCost.Note1 = item.LocationAddress;
                pl.Debit = plCost.Debit;
                pl.FIN_PLDetails.Add(plCost);

                // Group
                FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                plGroup.CreatedBy = Account.UserName;
                plGroup.CreatedDate = DateTime.Now;
                plGroup.GroupOfProductID = item.OPSGroupProductID;
                plCost.FIN_PLGroupOfProduct.Add(plGroup);

                FIN_PL plPL = new FIN_PL();
                CopyFinPL(pl, plPL);
                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                lstPL.Add(pl);
                lstPL.Add(plPL);
            }

            foreach (var item in lstOPSCOStation)
            {
                // Tạo pl
                FIN_PL pl = new FIN_PL();
                pl.IsPlanning = false;
                pl.Effdate = item.DateConfig.Date;
                pl.Code = string.Empty;
                pl.CreatedBy = Account.UserName;
                pl.CreatedDate = DateTime.Now;
                pl.SYSCustomerID = Account.SYSCustomerID;
                pl.VendorID = Account.SYSCustomerID;
                pl.VehicleID = item.VehicleID;
                pl.ScheduleID = schedule.ID;
                pl.COTOMasterID = item.COTOMasterID;
                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                // Detail
                FIN_PLDetails plCost = new FIN_PLDetails();
                plCost.CreatedBy = Account.UserName;
                plCost.CreatedDate = DateTime.Now;
                plCost.CostID = (int)CATCostType.StationDebit;
                plCost.TypeOfPriceDIExCode = CATCostType.StationDebit.ToString();
                plCost.Debit = item.Price;
                plCost.Note = item.Location;
                plCost.Note1 = item.LocationAddress;
                pl.Debit = plCost.Debit;
                pl.FIN_PLDetails.Add(plCost);

                // Container
                FIN_PLContainer plContainer = new FIN_PLContainer();
                plContainer.CreatedBy = Account.UserName;
                plContainer.CreatedDate = DateTime.Now;
                plContainer.COTOContainerID = item.OPSContainerID;
                plCost.FIN_PLContainer.Add(plContainer);

                FIN_PL plPL = new FIN_PL();
                CopyFinPL(pl, plPL);
                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                lstPL.Add(pl);
                lstPL.Add(plPL);
            }

            #endregion

            foreach (var pl in lstPL)
                model.FIN_PL.Add(pl);
        }

        // Phân bổ chi phí xe theo từng chuyến or KM
        public static void Truck_CalculateTemp(DataEntities model, AccountItem Account, int scheduleID, bool IsAllSchedule)
        {
            List<FIN_PL> lstPL = new List<FIN_PL>();
            List<FLM_ScheduleAssetTemp> lstAssetTemp = new List<FLM_ScheduleAssetTemp>();
            List<FLM_ScheduleDriverTemp> lstDriverTemp = new List<FLM_ScheduleDriverTemp>();
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);

            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);

            // Danh sách loại ngày
            var lstScheduleDate = model.FLM_ScheduleDate.Where(c => c.ScheduleID == schedule.ID).Select(c => new
            {
                c.ID,
                c.Date,
                c.TypeScheduleDateID
            }).ToList();

            if (IsAllSchedule)
            {
                var lstMaster = model.OPS_DITOMaster.Where(c => c.DateConfig.HasValue && c.VehicleID > 0 && c.OPS_DITOGroupProduct.Count > 0 && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new
                {
                    ID = c.ID,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    DateConfig = c.DateConfig.Value,
                    VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                    VehicleID = c.VehicleID,
                    KM = c.KM > 0 ? c.KM.Value : 0,
                    c.DriverID1,
                    c.DriverID2,
                    c.DriverID3,
                    c.DriverID4,
                    c.DriverID5,
                    OPSGroupProductID = c.OPS_DITOGroupProduct.OrderByDescending(d => d.TonTranfer).FirstOrDefault().ID
                }).ToList();

                var lstMasterCO = model.OPS_COTOMaster.Where(c => c.DateConfig.HasValue && c.VehicleID > 0 && c.OPS_COTOContainer.Count > 0 && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new
                {
                    ID = c.ID,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    DateConfig = c.DateConfig.Value,
                    VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                    VehicleID = c.VehicleID,
                    KM = c.KM > 0 ? c.KM.Value : 0,
                    c.DriverID1,
                    c.DriverID2,
                    c.DriverID3,
                    c.DriverID4,
                    c.DriverID5,
                    OPSContainerID = c.OPS_COTOContainer.OrderBy(d => d.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).ThenBy(d => d.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(d => d.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).FirstOrDefault().ID
                }).ToList();

                #region Tính cho Asset
                var lstScheduleAsset = model.FLM_ScheduleAsset.Where(c => c.ScheduleID == scheduleID && c.FLM_Asset.VehicleID > 0);
                // Lấy các chuyến
                if (lstScheduleAsset.Count() > 0)
                {
                    var lstFee = model.FLM_FixedCost.Where(c => c.Month == schedule.Month && c.Year == schedule.Year && c.IsClosed && c.Value > 0 && c.ReceiptID == null && !string.IsNullOrEmpty(c.FLM_Asset.ExprInputDay)).Select(c => new
                    {
                        c.ID,
                        c.Value,
                        VehicleID = c.FLM_Asset.VehicleID,
                        c.FLM_Asset.ExprInputDay,
                        IsFixCost = true,
                        TypeOfPriceDIExCode = string.Empty
                    }).ToList();

                    lstFee.AddRange(model.FLM_FixedCost.Where(c => c.Month == schedule.Month && c.Year == schedule.Year && c.IsClosed && c.Value > 0 && c.ReceiptID > 0 && !string.IsNullOrEmpty(c.FLM_Receipt.ExprInputDay)).Select(c => new
                    {
                        c.ID,
                        c.Value,
                        VehicleID = c.FLM_Asset.VehicleID,
                        c.FLM_Receipt.ExprInputDay,
                        IsFixCost = true,
                        TypeOfPriceDIExCode = string.Empty
                    }).ToList());

                    lstFee.AddRange(model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.Price > 0 && !string.IsNullOrEmpty(c.ExprInputDay) && c.AssetID.HasValue).Select(c => new
                    {
                        c.ID,
                        c.Price.Value,
                        VehicleID = c.FLM_Asset.VehicleID,
                        c.ExprInputDay,
                        IsFixCost = false,
                        TypeOfPriceDIExCode = c.FLM_TypeOfScheduleFee.Code
                    }).ToList());

                    foreach (var itemScheduleAsset in lstScheduleAsset)
                    {
                        var lstMasterAsset = lstMaster.Where(c => c.VehicleID == itemScheduleAsset.FLM_Asset.VehicleID);
                        var lstMasterCOAsset = lstMasterCO.Where(c => c.VehicleID == itemScheduleAsset.FLM_Asset.VehicleID);
                        if (lstMasterAsset.Count() > 0 || lstMasterCOAsset.Count() > 0)
                        {
                            foreach (var itemFee in lstFee.Where(c => c.VehicleID == itemScheduleAsset.FLM_Asset.VehicleID))
                            {
                                // Ktra các ngày được phân bổ khẩu hao
                                List<DateTime> lstDate = new List<DateTime>();
                                var DateFrom = DateConfig;
                                while (DateFrom < DateConfigEnd)
                                {
                                    if (lstScheduleDate.Any(c => c.Date == DateFrom))
                                    {
                                        bool flag = false;
                                        HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                        itemExpr.IsDaySchedule = lstMaster.Any(c => c.DateConfig.Date == DateFrom) || lstMasterCO.Any(c => c.DateConfig.Date == DateFrom);
                                        itemExpr.IsWorking = lstMasterAsset.Any(c => c.DateConfig.Date == DateFrom) || lstMasterCOAsset.Any(c => c.DateConfig.Date == DateFrom);
                                        itemExpr.IsDayOn = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                                        itemExpr.IsDayOff = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                                        itemExpr.IsDayHoliday = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
                                        try
                                        {
                                            flag = Expression_FLMCheckBool(itemExpr, itemFee.ExprInputDay);
                                        }
                                        catch { }

                                        if (flag)
                                            lstDate.Add(DateFrom);
                                    }
                                    DateFrom = DateFrom.AddDays(1);
                                }

                                // Chỉ phân bổ khi số ngày > 0
                                if (lstDate.Count > 0)
                                {
                                    int totalTO = lstMasterAsset.Count(c => lstDate.Contains(c.DateConfig));
                                    totalTO = lstMasterCOAsset.Count(c => lstDate.Contains(c.DateConfig));
                                    if (totalTO > 0)
                                    {
                                        decimal priceUnit = itemFee.Value / totalTO;
                                        foreach (var itemDate in lstDate)
                                        {
                                            var lstMasterDate = lstMasterAsset.Where(c => c.DateConfig == itemDate);
                                            var lstMasterCODate = lstMasterCOAsset.Where(c => c.DateConfig == itemDate);
                                            if (lstMasterDate.Count() > 0 || lstMasterCODate.Count() > 0)
                                            {
                                                FLM_ScheduleAssetTemp objTemp = new FLM_ScheduleAssetTemp();
                                                objTemp.CreatedBy = Account.UserName;
                                                objTemp.CreatedDate = DateTime.Now;
                                                objTemp.ScheduleAssetID = itemScheduleAsset.ID;
                                                objTemp.ScheduleDateID = lstScheduleDate.FirstOrDefault(c => c.Date == itemDate).ID;
                                                objTemp.TotalTO = lstMasterDate.Count() > 0 ? lstMasterDate.Count() : lstMasterCODate.Count();
                                                objTemp.KM = lstMasterDate.Count() > 0 ? lstMasterDate.Sum(c => c.KM) : lstMasterCODate.Sum(c => c.KM);
                                                objTemp.Value = priceUnit * objTemp.TotalTO;
                                                if (itemFee.IsFixCost)
                                                    objTemp.FixedCostID = itemFee.ID;
                                                else
                                                    objTemp.ScheduleFeeID = itemFee.ID;
                                                lstAssetTemp.Add(objTemp);

                                                #region Tạo pl cho từng chuyến xe tải
                                                foreach (var itemMasterDate in lstMasterDate)
                                                {
                                                    // Tạo pl
                                                    FIN_PL pl = new FIN_PL();
                                                    pl.IsPlanning = false;
                                                    pl.Effdate = itemMasterDate.DateConfig;
                                                    pl.Code = string.Empty;
                                                    pl.CreatedBy = Account.UserName;
                                                    pl.CreatedDate = DateTime.Now;
                                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                                    pl.VendorID = Account.SYSCustomerID;
                                                    pl.VehicleID = itemMasterDate.VehicleID;
                                                    pl.ScheduleID = schedule.ID;
                                                    pl.DITOMasterID = itemMasterDate.ID;
                                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                                    lstPL.Add(pl);

                                                    // Detail
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.Debit = priceUnit;
                                                    pl.Debit = plCost.Debit;
                                                    pl.FIN_PLDetails.Add(plCost);

                                                    if (itemFee.IsFixCost)
                                                    {
                                                        plCost.CostID = (int)CATCostType.FLMDepreciation;
                                                        plCost.TypeOfPriceDIExCode = CATCostType.FLMDepreciation.ToString();
                                                    }
                                                    else
                                                    {
                                                        plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                                        plCost.TypeOfPriceDIExCode = itemFee.TypeOfPriceDIExCode;
                                                    }

                                                    //Group
                                                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                    plGroup.CreatedBy = Account.UserName;
                                                    plGroup.CreatedDate = DateTime.Now;
                                                    plGroup.GroupOfProductID = itemMasterDate.OPSGroupProductID;
                                                    plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                }
                                                #endregion

                                                #region Tạo pl cho từng chuyến xe con
                                                foreach (var itemMasterDate in lstMasterCODate)
                                                {
                                                    // Tạo pl
                                                    FIN_PL pl = new FIN_PL();
                                                    pl.IsPlanning = false;
                                                    pl.Effdate = itemMasterDate.DateConfig;
                                                    pl.Code = string.Empty;
                                                    pl.CreatedBy = Account.UserName;
                                                    pl.CreatedDate = DateTime.Now;
                                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                                    pl.VendorID = Account.SYSCustomerID;
                                                    pl.VehicleID = itemMasterDate.VehicleID;
                                                    pl.ScheduleID = schedule.ID;
                                                    pl.COTOMasterID = itemMasterDate.ID;
                                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                                    lstPL.Add(pl);

                                                    // Detail
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.Debit = priceUnit;
                                                    pl.Debit = plCost.Debit;
                                                    pl.FIN_PLDetails.Add(plCost);

                                                    if (itemFee.IsFixCost)
                                                    {
                                                        plCost.CostID = (int)CATCostType.FLMDepreciation;
                                                        plCost.TypeOfPriceDIExCode = CATCostType.FLMDepreciation.ToString();
                                                    }
                                                    else
                                                    {
                                                        plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                                        plCost.TypeOfPriceDIExCode = itemFee.TypeOfPriceDIExCode;
                                                    }

                                                    //Group
                                                    FIN_PLContainer plContainer = new FIN_PLContainer();
                                                    plContainer.CreatedBy = Account.UserName;
                                                    plContainer.CreatedDate = DateTime.Now;
                                                    plContainer.COTOContainerID = itemMasterDate.OPSContainerID;
                                                    plCost.FIN_PLContainer.Add(plContainer);
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion

                #region Tính cho Driver
                var lstScheduleDriver = model.FLM_ScheduleDriver.Where(c => c.ScheduleID == scheduleID);
                // Lấy các chuyến
                if (lstScheduleDriver.Count() > 0)
                {
                    var lstFee = model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.Price > 0 && !string.IsNullOrEmpty(c.ExprInputDay) && (c.DriverID.HasValue || c.IsAssistant.HasValue)).Select(c => new
                    {
                        c.ID,
                        c.Price.Value,
                        c.DriverID,
                        c.ExprInputDay,
                        IsAssistant = c.IsAssistant.Value,
                        TypeOfPriceDIExCode = c.FLM_TypeOfScheduleFee.Code
                    }).ToList();

                    foreach (var itemScheduleDriver in lstScheduleDriver)
                    {
                        var lstMasterDriver = lstMaster.Where(c => (c.DriverID1 == itemScheduleDriver.DriverID || c.DriverID2 == itemScheduleDriver.DriverID || c.DriverID3 == itemScheduleDriver.DriverID));
                        var lstMasterCODriver = lstMasterCO.Where(c => (c.DriverID1 == itemScheduleDriver.DriverID || c.DriverID2 == itemScheduleDriver.DriverID || c.DriverID3 == itemScheduleDriver.DriverID));
                        if (lstMasterDriver.Count() > 0 || lstMasterCODriver.Count() > 0)
                        {
                            foreach (var itemFee in lstFee.Where(c => c.DriverID == itemScheduleDriver.DriverID || c.IsAssistant == itemScheduleDriver.IsAssistant))
                            {
                                // Ktra các ngày được phân bổ khẩu hao
                                List<DateTime> lstDate = new List<DateTime>();
                                var DateFrom = DateConfig;
                                while (DateFrom < DateConfigEnd)
                                {
                                    if (lstScheduleDate.Any(c => c.Date == DateFrom))
                                    {
                                        bool flag = false;
                                        HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                        itemExpr.IsWorking = lstMasterDriver.Any(c => c.DateConfig.Date == DateFrom);
                                        itemExpr.IsDayOn = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                                        itemExpr.IsDayOff = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                                        itemExpr.IsDayHoliday = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
                                        try
                                        {
                                            flag = Expression_FLMCheckBool(itemExpr, itemFee.ExprInputDay);
                                        }
                                        catch { }

                                        if (flag)
                                            lstDate.Add(DateFrom);
                                    }
                                    DateFrom = DateFrom.AddDays(1);
                                }

                                // Chỉ phân bổ khi số ngày > 0
                                if (lstDate.Count > 0)
                                {
                                    int totalTO = lstMasterDriver.Count(c => lstDate.Contains(c.DateConfig));
                                    totalTO = lstMasterCODriver.Count(c => lstDate.Contains(c.DateConfig));
                                    if (totalTO > 0)
                                    {
                                        decimal priceUnit = itemFee.Value / totalTO;
                                        foreach (var itemDate in lstDate)
                                        {
                                            var lstMasterDate = lstMasterDriver.Where(c => c.DateConfig == itemDate);
                                            var lstMasterCODate = lstMasterCODriver.Where(c => c.DateConfig == itemDate);
                                            if (lstMasterDate.Count() > 0 || lstMasterCODate.Count() > 0)
                                            {
                                                FLM_ScheduleDriverTemp objTemp = new FLM_ScheduleDriverTemp();
                                                objTemp.CreatedBy = Account.UserName;
                                                objTemp.CreatedDate = DateTime.Now;
                                                objTemp.ScheduleDriverID = itemScheduleDriver.ID;
                                                objTemp.ScheduleDateID = lstScheduleDate.FirstOrDefault(c => c.Date == itemDate).ID;
                                                objTemp.TotalTO = lstMasterDate.Count() > 0 ? lstMasterDate.Count() : lstMasterCODate.Count();
                                                objTemp.KM = lstMasterDate.Count() > 0 ? lstMasterDate.Sum(c => c.KM) : lstMasterCODate.Sum(c => c.KM);
                                                objTemp.Value = priceUnit * objTemp.TotalTO;
                                                objTemp.ScheduleFeeID = itemFee.ID;
                                                lstDriverTemp.Add(objTemp);

                                                #region Tạo pl cho từng chuyến xe tải
                                                foreach (var itemMasterDate in lstMasterDate)
                                                {
                                                    // Tạo pl
                                                    FIN_PL pl = new FIN_PL();
                                                    pl.IsPlanning = false;
                                                    pl.Effdate = itemMasterDate.DateConfig;
                                                    pl.Code = string.Empty;
                                                    pl.CreatedBy = Account.UserName;
                                                    pl.CreatedDate = DateTime.Now;
                                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                                    pl.VendorID = Account.SYSCustomerID;
                                                    pl.VehicleID = itemMasterDate.VehicleID;
                                                    pl.ScheduleID = schedule.ID;
                                                    pl.DITOMasterID = itemMasterDate.ID;
                                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                                    lstPL.Add(pl);

                                                    // Detail
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.Debit = priceUnit;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = itemFee.TypeOfPriceDIExCode;
                                                    pl.Debit = plCost.Debit;
                                                    pl.FIN_PLDetails.Add(plCost);

                                                    //Group
                                                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                    plGroup.CreatedBy = Account.UserName;
                                                    plGroup.CreatedDate = DateTime.Now;
                                                    plGroup.GroupOfProductID = itemMasterDate.OPSGroupProductID;
                                                    plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                }
                                                #endregion

                                                #region Tạo pl cho từng chuyến xe con
                                                foreach (var itemMasterDate in lstMasterCODate)
                                                {
                                                    // Tạo pl
                                                    FIN_PL pl = new FIN_PL();
                                                    pl.IsPlanning = false;
                                                    pl.Effdate = itemMasterDate.DateConfig;
                                                    pl.Code = string.Empty;
                                                    pl.CreatedBy = Account.UserName;
                                                    pl.CreatedDate = DateTime.Now;
                                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                                    pl.VendorID = Account.SYSCustomerID;
                                                    pl.VehicleID = itemMasterDate.VehicleID;
                                                    pl.ScheduleID = schedule.ID;
                                                    pl.COTOMasterID = itemMasterDate.ID;
                                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                                    lstPL.Add(pl);

                                                    // Detail
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.Debit = priceUnit;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = itemFee.TypeOfPriceDIExCode;
                                                    pl.Debit = plCost.Debit;
                                                    pl.FIN_PLDetails.Add(plCost);

                                                    //Group
                                                    FIN_PLContainer plContainer = new FIN_PLContainer();
                                                    plContainer.CreatedBy = Account.UserName;
                                                    plContainer.CreatedDate = DateTime.Now;
                                                    plContainer.COTOContainerID = itemMasterDate.OPSContainerID;
                                                    plCost.FIN_PLContainer.Add(plContainer);
                                                }
                                                #endregion
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                #endregion
            }

            foreach (var pl in lstPL)
                model.FIN_PL.Add(pl);
            foreach (var pl in lstAssetTemp)
                model.FLM_ScheduleAssetTemp.Add(pl);
            foreach (var pl in lstDriverTemp)
                model.FLM_ScheduleDriverTemp.Add(pl);
        }
        #endregion

        #region Tính lương theo PHT
        // Tính lại cho schedule
        public static void Truck_CalculateSchedule_Temp(DataEntities model, AccountItem Account, int scheduleID)
        {
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);
            if (schedule != null)
            {
                var DateConfig = schedule.DateFrom.Date;
                var DateConfigEnd = schedule.DateTo.Date.AddDays(1);
                var DateConfigEndDate = schedule.DateTo.Date;
                var lstScheduleDate = model.FLM_ScheduleDate.Where(c => c.ScheduleID == scheduleID);
                // Lấy param
                double totalDay = schedule.TotalDays;
                double totalDayActual = (schedule.DateTo - schedule.DateFrom).TotalDays + 1;
                double totalDayOff = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                double totalDayOn = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                double totalDayHoliday = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);

                // Tính lại TotalDay cho schedule
                if (!string.IsNullOrEmpty(schedule.ExprTotalDays))
                {
                    HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                    itemCheck.TotalDay = totalDay;
                    itemCheck.TotalDayActual = totalDayActual;
                    itemCheck.TotalDayOff = totalDayOff;
                    itemCheck.TotalDayOn = totalDayOn;
                    itemCheck.TotalDayHoliday = totalDayHoliday;
                    decimal? total = Expression_FLMGetValue(itemCheck, schedule.ExprTotalDays);
                    if (total.HasValue)
                        schedule.TotalDays = (double)total.Value;
                }


                var lstScheduleFee = model.FLM_ScheduleFee.Where(c => c.ScheduleID == scheduleID && !string.IsNullOrEmpty(c.ExprPrice) && (c.AssetID > 0 || c.DriverID > 0 || c.IsAssistant.HasValue)).ToList();
                var lstAsset = model.FLM_ScheduleFee.Where(c => c.ScheduleID == scheduleID && !string.IsNullOrEmpty(c.ExprPrice) && c.AssetID > 0).Select(c => new { c.AssetID, c.FLM_Asset.VehicleID }).Distinct().ToList();
                if (lstScheduleFee.Count() > 0)
                {
                    var lstMaster = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID > 0 && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig <= DateConfigEndDate && c.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterPHT && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new HelperFinance_TOMaster
                    {
                        ID = c.ID,
                        ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                        DateConfig = c.DateConfig.Value,
                        VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                        VehicleID = c.VehicleID,
                        KM = c.KM > 0 ? c.KM : 0,
                        DriverID1 = c.DriverID1,
                        DriverID2 = c.DriverID2,
                        DriverID3 = c.DriverID3,
                        DriverID4 = c.DriverID4,
                        DriverID5 = c.DriverID5
                    }).ToList();

                    var lstMasterCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID > 0 && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig <= DateConfigEndDate && c.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterPHT && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID)).Select(c => new HelperFinance_TOMaster
                    {
                        ID = c.ID,
                        ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                        DateConfig = c.DateConfig.Value,
                        VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                        VehicleID = c.VehicleID,
                        KM = c.KM > 0 ? c.KM : 0,
                        DriverID1 = c.DriverID1,
                        DriverID2 = c.DriverID2,
                        DriverID3 = c.DriverID3,
                        DriverID4 = c.DriverID4,
                        DriverID5 = c.DriverID5
                    }).ToList();

                    var lstDriverFee = model.FLM_ScheduleDriver.Where(c => c.ScheduleID == scheduleID).Select(c => new
                    {
                        c.FeeBase,
                        c.DriverID,
                        c.IsAssistant
                    }).ToList();

                    var lstFixCost = model.FLM_FixedCost.Where(c => c.Month == schedule.Month && c.Year == schedule.Year && c.IsClosed && c.Value > 0 && c.ReceiptID == null).Select(c => new
                    {
                        c.FLM_Asset.VehicleID,
                        c.Value,
                    }).ToList();

                    foreach (var itemScheduleFee in lstScheduleFee)
                    {
                        if (itemScheduleFee.AssetID > 0)
                        {
                            HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                            itemExpr.TotalDay = schedule.TotalDays;
                            itemExpr.TotalDayOn = totalDayOn;
                            itemExpr.TotalDayOff = totalDayOff;
                            itemExpr.TotalDayHoliday = totalDayHoliday;
                            var asset = lstAsset.FirstOrDefault(c => c.AssetID == itemScheduleFee.AssetID);
                            if (asset != null)
                            {
                                itemExpr.TotalSchedule = lstMaster.Count(c => c.VehicleID == asset.VehicleID);
                                itemExpr.TotalSchedule += lstMasterCO.Count(c => c.VehicleID == asset.VehicleID);
                                var lstFixCostAsset = lstFixCost.Where(c => c.VehicleID == asset.VehicleID);
                                if (lstFixCostAsset.Count() > 0)
                                    itemExpr.Price = lstFixCostAsset.Sum(c => c.Value);

                                itemScheduleFee.Price = Expression_FLMGetValue(itemExpr, itemScheduleFee.ExprPrice);
                            }
                        }

                        if (itemScheduleFee.DriverID.HasValue)
                        {
                            HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                            itemExpr.TotalDay = schedule.TotalDays;
                            itemExpr.TotalDayOn = totalDayOn;
                            itemExpr.TotalDayOff = totalDayOff;
                            itemExpr.TotalDayHoliday = totalDayHoliday;
                            itemExpr.TotalSchedule = lstMaster.Count(c => (c.DriverID1 == itemScheduleFee.DriverID || c.DriverID2 == itemScheduleFee.DriverID || c.DriverID3 == itemScheduleFee.DriverID || c.DriverID4 == itemScheduleFee.DriverID || c.DriverID5 == itemScheduleFee.DriverID));
                            itemExpr.TotalSchedule += lstMasterCO.Count(c => (c.DriverID1 == itemScheduleFee.DriverID || c.DriverID2 == itemScheduleFee.DriverID || c.DriverID3 == itemScheduleFee.DriverID || c.DriverID4 == itemScheduleFee.DriverID || c.DriverID5 == itemScheduleFee.DriverID));
                            var driverFee = lstDriverFee.FirstOrDefault(c => c.DriverID == itemScheduleFee.DriverID);
                            if (driverFee != null)
                                itemExpr.FeeBase = driverFee.FeeBase;

                            itemScheduleFee.Price = Expression_FLMGetValue(itemExpr, itemScheduleFee.ExprPrice);
                        }

                        if (itemScheduleFee.IsAssistant.HasValue)
                        {
                            var defDriver = lstDriverFee.FirstOrDefault(c => c.IsAssistant == itemScheduleFee.IsAssistant.Value);
                            if (defDriver != null)
                            {
                                HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                itemExpr.TotalDay = schedule.TotalDays;
                                itemExpr.TotalDayOn = totalDayOn;
                                itemExpr.TotalDayOff = totalDayOff;
                                itemExpr.TotalDayHoliday = totalDayHoliday;
                                itemExpr.TotalSchedule = lstMaster.Count(c => (c.DriverID1 == defDriver.DriverID || c.DriverID2 == defDriver.DriverID || c.DriverID3 == defDriver.DriverID || c.DriverID4 == defDriver.DriverID || c.DriverID5 == defDriver.DriverID));
                                itemExpr.TotalSchedule += lstMasterCO.Count(c => (c.DriverID1 == defDriver.DriverID || c.DriverID2 == defDriver.DriverID || c.DriverID3 == defDriver.DriverID || c.DriverID4 == defDriver.DriverID || c.DriverID5 == defDriver.DriverID));
                                itemExpr.FeeBase = defDriver.FeeBase;

                                itemScheduleFee.Price = Expression_FLMGetValue(itemExpr, itemScheduleFee.ExprPrice);
                            }
                        }
                    }
                }
            }
        }

        // Xóa lương cũ, cập nhật lại dữ liệu chuyến liên quan đến schedule
        public static void Truck_CalculateDriverRerun_Temp(DataEntities model, AccountItem Account, int scheduleID)
        {
            #region Xóa PL cũ
            foreach (var pl in model.FIN_PL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ScheduleID == scheduleID))
            {
                foreach (var plDetail in model.FIN_PLDetails.Where(c => c.PLID == pl.ID))
                {
                    foreach (var plGroup in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == plDetail.ID))
                        model.FIN_PLGroupOfProduct.Remove(plGroup);
                    model.FIN_PLDetails.Remove(plDetail);
                }
                model.FIN_PL.Remove(pl);
            }
            foreach (var pl in model.FLM_ScheduleAssetTemp.Where(c => c.FLM_ScheduleAsset.ScheduleID == scheduleID))
                model.FLM_ScheduleAssetTemp.Remove(pl);
            foreach (var pl in model.FLM_ScheduleDriverTemp.Where(c => c.FLM_ScheduleDriver.ScheduleID == scheduleID))
                model.FLM_ScheduleDriverTemp.Remove(pl);
            #endregion

            #region Cập nhật dữ liệu StationMonth
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);
            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);
            var DateConfigEndDate = schedule.DateTo.Date;

            foreach (var item in model.CAT_StationMonth.Where(c => c.ScheduleID == scheduleID || (c.DateFrom >= schedule.DateFrom && c.DateTo <= schedule.DateTo)))
            {
                item.ModifiedBy = Account.UserName;
                item.ModifiedDate = DateTime.Now;
                if (item.DateFrom >= schedule.DateFrom && item.DateTo <= schedule.DateTo)
                    item.ScheduleID = schedule.ID;
                else
                    item.ScheduleID = null;
            }
            #endregion

            #region Hợp đồng xe nhà
            List<int> lstVendorID = new List<int>();
            lstVendorID.Add(Account.SYSCustomerID);

            //Lấy danh sách ID hợp đồng chính của LTL, FTL
            var lstContractID = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (lstVendorID.Contains(c.CustomerID.Value) || c.CustomerID == null) &&
                c.EffectDate <= DateConfig && (c.ExpiredDate == null || c.ExpiredDate > DateConfig) && (c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFTL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLTL))
                .Select(c => c.ID).ToArray();

            int contractID = 0;
            if (lstContractID.Count() > 0)
                contractID = lstContractID.FirstOrDefault();
            #endregion

            #region Cập nhật ScheduleID và SortOrder cho các chuyến DIMaster
            var lstMaster = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID > 0 && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig <= DateConfigEndDate && c.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterPHT && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID));
            foreach (var item in lstMaster)
            {
                item.ModifiedBy = Account.UserName;
                item.ModifiedDate = DateTime.Now;
                item.ScheduleID = schedule.ID;
                if (contractID > 0)
                    item.ContractID = contractID;
            }
            #endregion

            #region Cập nhật ScheduleID và SortOrder cho các chuyến COMaster
            var lstMasterCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID > 0 && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig <= DateConfigEndDate && c.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterPHT && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID));
            foreach (var item in lstMaster)
            {
                item.ModifiedBy = Account.UserName;
                item.ModifiedDate = DateTime.Now;
                item.ScheduleID = schedule.ID;
                if (contractID > 0)
                    item.ContractID = contractID;
            }
            #endregion

            #region Tính cho Driver
            var lstScheduleDate = model.FLM_ScheduleDate.Where(c => c.ScheduleID == scheduleID);
            // Lấy param
            double totalDay = schedule.TotalDays;
            double totalDayActual = (schedule.DateTo - schedule.DateFrom).TotalDays + 1;
            double totalDayOff = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
            double totalDayOn = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
            double totalDayHoliday = lstScheduleDate.Count(c => c.ScheduleID == schedule.ID && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
            // Tính lại các ngày cho driver
            var lstScheduleDriver = model.FLM_ScheduleDriver.Where(c => c.ScheduleID == schedule.ID);
            foreach (var itemScheduleDriver in lstScheduleDriver)
            {
                itemScheduleDriver.ModifiedBy = Account.UserName;
                itemScheduleDriver.ModifiedDate = DateTime.Now;

                var lstScheduleDateDetail = model.FLM_ScheduleDateDetail.Where(c => c.DriverID == itemScheduleDriver.DriverID).Select(c => new
                {
                    c.FLM_ScheduleDate.Date,
                    c.FLM_ScheduleDate.TypeScheduleDateID
                }).ToList();

                HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                itemCheck.FeeBase = itemScheduleDriver.FeeBase;
                itemCheck.TotalDay = totalDay;
                itemCheck.TotalDayActual = totalDayActual;
                itemCheck.TotalDayOff = totalDayOff;
                itemCheck.TotalDayOn = totalDayOn;
                itemCheck.TotalDayHoliday = totalDayHoliday;
                itemCheck.TotalDayAllowOffDriver = itemScheduleDriver.DaysAllowOff;
                itemCheck.TotalDayAllowOffRemainDriver = itemScheduleDriver.DaysAllowOffRemain.HasValue ? itemScheduleDriver.DaysAllowOffRemain.Value : 0;
                itemCheck.TotalDayOffDriver = lstScheduleDateDetail.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                itemCheck.TotalDayOnDriver = lstScheduleDateDetail.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                itemCheck.TotalDayHolidayDriver = lstScheduleDateDetail.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);

                // ExprDaysAllowOff - Tính ngày DaysAllowOff của từng tài xế: DaysAllowOffRemain, như trên + của riêng tài xế (FLM_ScheduleDateDetail)
                if (!string.IsNullOrEmpty(schedule.ExprDaysAllowOff))
                {
                    decimal? total = Expression_FLMGetValue(itemCheck, schedule.ExprDaysAllowOff);
                    if (total.HasValue)
                        itemScheduleDriver.DaysAllowOff = (double)total.Value;
                }

                // ExprDaysWorkInOn - Tính ngày DaysWorkInOn của từng tài xế: DaysAllowOffRemain, như trên + của riêng tài xế (FLM_ScheduleDateDetail)
                if (!string.IsNullOrEmpty(schedule.ExprDaysWorkInOn))
                {
                    decimal? total = Expression_FLMGetValue(itemCheck, schedule.ExprDaysWorkInOn);
                    if (total.HasValue)
                        itemScheduleDriver.DaysWorkInOn = (double)total.Value;
                }

                // ExprDaysWorkInOff - Tính ngày DaysWorkInOff của từng tài xế: DaysAllowOffRemain, như trên + của riêng tài xế (FLM_ScheduleDateDetail)
                if (!string.IsNullOrEmpty(schedule.ExprDaysWorkInOff))
                {
                    decimal? total = Expression_FLMGetValue(itemCheck, schedule.ExprDaysWorkInOff);
                    if (total.HasValue)
                        itemScheduleDriver.DaysWorkInOff = (double)total.Value;
                }

                // ExprDaysWorkInHoliday - Tính ngày DaysWorkInHoliday của từng tài xế: DaysAllowOffRemain, như trên + của riêng tài xế (FLM_ScheduleDateDetail)
                if (!string.IsNullOrEmpty(schedule.ExprDaysWorkInHoliday))
                {
                    decimal? total = Expression_FLMGetValue(itemCheck, schedule.ExprDaysWorkInHoliday);
                    if (total.HasValue)
                        itemScheduleDriver.DaysWorkInHoliday = (double)total.Value;
                }

                // Cập nhật Tổng chuyến, KM cho driver
                var lstMasterDriver = lstMaster.Where(c => c.DriverID1 == itemScheduleDriver.DriverID || c.DriverID2 == itemScheduleDriver.DriverID || c.DriverID3 == itemScheduleDriver.DriverID || c.DriverID4 == itemScheduleDriver.DriverID || c.DriverID5 == itemScheduleDriver.DriverID);
                var lstMasterCODriver = lstMasterCO.Where(c => c.DriverID1 == itemScheduleDriver.DriverID || c.DriverID2 == itemScheduleDriver.DriverID || c.DriverID3 == itemScheduleDriver.DriverID || c.DriverID4 == itemScheduleDriver.DriverID || c.DriverID5 == itemScheduleDriver.DriverID);
                if (lstMasterDriver.Count() > 0)
                {
                    itemScheduleDriver.TotalTO = lstMaster.Count();
                    itemScheduleDriver.TotalKM = lstMasterDriver.Count(c => c.KM > 0) > 0 ? lstMasterDriver.Where(c => c.KM > 0).Sum(c => c.KM.Value) : 0;
                }
                if (lstMasterCODriver.Count() > 0)
                {
                    itemScheduleDriver.TotalTO += lstMasterCODriver.Count();
                    itemScheduleDriver.TotalKM += lstMasterCODriver.Count(c => c.KM > 0) > 0 ? lstMasterCODriver.Where(c => c.KM > 0).Sum(c => c.KM.Value) : 0;
                }
            }

            // Tính cho các ngày tự định nghĩa khác của driver
            var lstScheduleFeeDriverOther = model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.DriverID.HasValue && !string.IsNullOrEmpty(c.ExprDay));
            if (lstScheduleFeeDriverOther != null && lstScheduleFeeDriverOther.Count() > 0)
            {
                foreach (var itemScheduleFeeDriverOther in lstScheduleFeeDriverOther)
                {
                    // Item check công thức
                    HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                    itemCheck.TotalDay = schedule.TotalDays;
                    itemCheck.TotalDayActual = (schedule.DateTo.Date - schedule.DateFrom.Date).TotalDays + 1;
                    // Param bổ sung
                    var scheduleDriver = model.FLM_ScheduleDriver.FirstOrDefault(c => c.ScheduleID == schedule.ID && c.DriverID == itemScheduleFeeDriverOther.DriverID);
                    if (scheduleDriver != null)
                    {
                        itemCheck.TotalDayOffDriver = scheduleDriver.DaysWorkInOff.HasValue ? scheduleDriver.DaysWorkInOff.Value : 0;
                        itemCheck.TotalDayOnDriver = scheduleDriver.DaysWorkInOn.HasValue ? scheduleDriver.DaysWorkInOn.Value : 0;
                        itemCheck.TotalDayHolidayDriver = scheduleDriver.DaysWorkInHoliday.HasValue ? scheduleDriver.DaysWorkInHoliday.Value : 0;
                        itemCheck.TotalDayAllowOffDriver = scheduleDriver.DaysAllowOff;
                        itemCheck.TotalDayAllowOffRemainDriver = scheduleDriver.DaysAllowOffRemain.HasValue ? scheduleDriver.DaysAllowOffRemain.Value : 0;
                    }
                    decimal? day = Expression_FLMGetValue(itemCheck, itemScheduleFeeDriverOther.ExprDay);
                    if (day.HasValue)
                        itemScheduleFeeDriverOther.Day = (double)day.Value;
                }
            }
            #endregion

            #region Cập nhật Tổng chuyến và KM cho Asset
            var lstScheduleAsset = model.FLM_ScheduleAsset.Where(c => c.ScheduleID == schedule.ID);
            foreach (var itemScheduleAsset in lstScheduleAsset)
            {
                var lstMasterAsset = lstMaster.Where(c => c.VehicleID == itemScheduleAsset.FLM_Asset.VehicleID);
                if (lstMasterAsset.Count() > 0)
                {
                    itemScheduleAsset.TotalTO = lstMasterAsset.Count();
                    itemScheduleAsset.TotalKM = lstMasterAsset.Count(c => c.KM > 0) > 0 ? lstMasterAsset.Where(c => c.KM > 0).Sum(c => c.KM.Value) : 0;
                }
                var lstMasterCOAsset = lstMasterCO.Where(c => c.VehicleID == itemScheduleAsset.FLM_Asset.VehicleID);
                if (lstMasterCOAsset.Count() > 0)
                {
                    itemScheduleAsset.TotalTO = lstMasterCOAsset.Count();
                    itemScheduleAsset.TotalKM = lstMasterCOAsset.Count(c => c.KM > 0) > 0 ? lstMasterCOAsset.Where(c => c.KM > 0).Sum(c => c.KM.Value) : 0;
                }
            }
            #endregion
        }

        // Tính lương chuyến cho tài xế
        public static void Truck_CalculateDriver_Temp(DataEntities model, AccountItem Account, int scheduleID)
        {
            const int iTon = -(int)SYSVarType.PriceOfGOPTon;
            const int iCBM = -(int)SYSVarType.PriceOfGOPCBM;
            const int iQuantity = -(int)SYSVarType.PriceOfGOPTU;
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);

            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);
            var DateConfigEndDate = schedule.DateTo.Date;

            // Lấy ds Vendor
            List<int> lstVendorID = new List<int>();
            lstVendorID.Add(Account.SYSCustomerID);

            //Lấy danh sách ID hợp đồng chính của LTL, FTL
            var lstContractID = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (lstVendorID.Contains(c.CustomerID.Value) || c.CustomerID == null) &&
                c.EffectDate <= DateConfig && (c.ExpiredDate == null || c.ExpiredDate > DateConfig) && (c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFTL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLTL))
                .Select(c => c.ID).ToArray();

            #region Tính theo chuyến
            //Lấy các chuyến
            var lstMaster = model.OPS_DITOMaster.Where(c => c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig <= DateConfigEndDate && c.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterPHT && c.ScheduleID == scheduleID && c.ContractID > 0 && lstContractID.Contains(c.ContractID.Value) && c.PHTCustomerID > 0).Select(c => new HelperFinance_TOMaster
            {
                ID = c.ID,
                GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                DateConfig = c.DateConfig.Value,
                VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                VehicleID = c.VehicleID,
                DriverID1 = c.DriverID1,
                DriverID2 = c.DriverID2,
                DriverID3 = c.DriverID3,
                DriverID4 = c.DriverID4,
                DriverID5 = c.DriverID5,
                TypeOfDriverID1 = c.TypeOfDriverID1,
                TypeOfDriverID2 = c.TypeOfDriverID2,
                TypeOfDriverID3 = c.TypeOfDriverID3,
                TypeOfDriverID4 = c.TypeOfDriverID4,
                TypeOfDriverID5 = c.TypeOfDriverID5,
                ExIsOverNight = c.ExIsOverNight,
                ExIsOverWeight = c.ExIsOverWeight,
                ExTotalDayOut = c.ExTotalDayOut,
                ExTotalJoin = c.ExTotalJoin,
                ETD = c.ETD,
                SortOrder = c.SortOrder,
                PHTCustomerID = c.PHTCustomerID,
                PHTGroupOfLocationID = c.PHTGroupOfLocationID.HasValue ? c.PHTGroupOfLocationID.Value : -1,
                PHTLoading = c.PHTLoading.HasValue ? c.PHTLoading.Value : false,
                PHTPackingID = -1,
            }).ToList();
            // Container
            var lstMasterCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig <= DateConfigEndDate && c.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterPHT && c.ScheduleID == scheduleID && c.ContractID > 0 && lstContractID.Contains(c.ContractID.Value) && c.PHTCustomerID > 0).Select(c => new HelperFinance_COMaster
            {
                ID = c.ID,
                GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                CATRoutingID = c.RoutingID > 0 ? c.RoutingID.Value : -1,
                ParentRoutingID = c.RoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                DateConfig = c.DateConfig.Value,
                VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                VehicleID = c.VehicleID,
                DriverID1 = c.DriverID1,
                DriverID2 = c.DriverID2,
                DriverID3 = c.DriverID3,
                DriverID4 = c.DriverID4,
                DriverID5 = c.DriverID5,
                TypeOfDriverID1 = c.TypeOfDriverID1,
                TypeOfDriverID2 = c.TypeOfDriverID2,
                TypeOfDriverID3 = c.TypeOfDriverID3,
                TypeOfDriverID4 = c.TypeOfDriverID4,
                TypeOfDriverID5 = c.TypeOfDriverID5,
                ExIsOverNight = c.ExIsOverNight,
                ExIsOverWeight = c.ExIsOverWeight,
                ExTotalDayOut = c.ExTotalDayOut,
                ExTotalJoin = c.ExTotalJoin,
                ETD = c.ETD,
                SortOrder = c.SortOrder,
                PHTCustomerID = c.PHTCustomerID,
                PHTGroupOfLocationID = c.PHTGroupOfLocationID.HasValue ? c.PHTGroupOfLocationID.Value : -1,
                PHTLoading = c.PHTLoading.HasValue ? c.PHTLoading.Value : false,
                PHTPackingID = c.PHTPackingID > 0 ? c.PHTPackingID.Value : -1,
            }).Distinct().ToList();
            // Danh sách chuyến còn thiếu trong list Master

            var lstContractMasterID = lstMaster.Select(c => c.ContractID).Distinct().ToList();
            lstContractMasterID.AddRange(lstMasterCO.Select(c => c.ContractID).Distinct().ToList());
            if (lstContractMasterID.Count > 0 && lstContractMasterID.Count > 0)
            {
                lstContractID = lstContractID.Intersect(lstContractMasterID).ToArray();
                var lstContract = model.CAT_Contract.Where(c => lstContractID.Contains(c.ID)).Select(c => new HelperFinance_Contract
                {
                    ID = c.ID,
                    VendorID = c.CustomerID.HasValue ? c.CustomerID.Value : Account.SYSCustomerID,
                    TransportModeID = c.CAT_TransportMode.TransportModeID,
                    PriceInDay = c.PriceInDay,
                    CustomerID = c.CompanyID > 0 ? c.CUS_Company.CustomerRelateID : -1,
                }).ToList();
                if (lstContract.Count > 0)
                {
                    List<FIN_PL> lstPl = new List<FIN_PL>();

                    //Chạy từng hợp đồng
                    foreach (var itemContract in lstContract)
                    {
                        if (itemContract.CustomerID < 1) itemContract.CustomerID = null;

                        System.Diagnostics.Debug.WriteLine("Contract start: " + itemContract.ID);
                        var queryMasterContract = lstMaster.Where(c => c.ContractID == itemContract.ID);

                        var queryMasterContractCO = lstMasterCO.Where(c => c.ContractID == itemContract.ID);

                        if (queryMasterContract.Count() > 0 || queryMasterContractCO.Count() > 0)
                        {
                            #region Tính lương chuyến
                            var lstDriverFee = model.CAT_DriverFee.Where(c => c.ContractID == itemContract.ID && (c.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumSchedule || c.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumTotal)).Select(c => new
                            {
                                c.ID,
                                c.TypeOfDriverFeeID,
                                c.TypeOfDriverID,
                                c.FeeName,
                                c.ExprInput,
                                c.ExprPriceFix,
                                c.ExprQuantity,
                                c.ExprUnitPrice,
                                c.DriverFeeSumID,
                                TypeOfDriverFeeCode = c.CAT_TypeOfDriverFee.Code,
                                TypeOfDriverFeeName = c.CAT_TypeOfDriverFee.TypeName,
                                c.SortOrder
                            }).ToList();

                            foreach (var driverFee in lstDriverFee)
                            {
                                System.Diagnostics.Debug.WriteLine(driverFee.FeeName + " ID: " + driverFee.ID);

                                var lstMasterDriverFee = new List<HelperFinance_TOMaster>();
                                var queryMasterDriverFee = queryMasterContract == null ? new List<HelperFinance_TOMaster>() : queryMasterContract.Where(c => (c.DriverID1.HasValue && c.TypeOfDriverID1 == driverFee.TypeOfDriverID) || (c.DriverID2.HasValue && c.TypeOfDriverID2 == driverFee.TypeOfDriverID) || (c.DriverID3.HasValue && c.TypeOfDriverID3 == driverFee.TypeOfDriverID) || (driverFee.TypeOfDriverID == null && (c.DriverID1.HasValue || c.DriverID2.HasValue || c.DriverID3.HasValue))).ToList();

                                var lstMasterCODriverFee = new List<HelperFinance_COMaster>();
                                var queryMasterCODriverFee = queryMasterContractCO == null ? new List<HelperFinance_COMaster>() : queryMasterContractCO.Where(c => (c.DriverID1.HasValue && c.TypeOfDriverID1 == driverFee.TypeOfDriverID) || (c.DriverID2.HasValue && c.TypeOfDriverID2 == driverFee.TypeOfDriverID) || (c.DriverID3.HasValue && c.TypeOfDriverID3 == driverFee.TypeOfDriverID) || (driverFee.TypeOfDriverID == null && (c.DriverID1.HasValue || c.DriverID2.HasValue || c.DriverID3.HasValue))).ToList();

                                if (driverFee.SortOrder > 0)
                                {
                                    queryMasterDriverFee = queryMasterDriverFee.Where(c => c.SortOrder == driverFee.SortOrder).ToList();
                                    queryMasterCODriverFee = queryMasterCODriverFee.Where(c => c.SortOrder == driverFee.SortOrder).ToList();
                                }

                                //Danh sách các điều kiện lọc 
                                var lstDriverFeeCustomer = model.CAT_DriverFeeCustomer.Where(c => c.DriverFeeID == driverFee.ID && c.CustomerID > 0)
                                   .Select(c => c.CustomerID).ToList();
                                var lstDriverFeeParentRouting = model.CAT_DriverFeeRouting.Where(c => c.DriverFeeID == driverFee.ID && c.ParentRoutingID > 0)
                                    .Select(c => c.ParentRoutingID.Value).ToList();
                                var lstDriverFeeRouting = model.CAT_DriverFeeRouting.Where(c => c.DriverFeeID == driverFee.ID && c.RoutingID > 0)
                                    .Select(c => c.RoutingID.Value).ToList();
                                var lstDriverFeeGroupLocation = model.CAT_DriverFeeGroupLocation.Where(c => c.DriverFeeID == driverFee.ID)
                                    .Select(c => c.GroupOfLocationID).ToList();
                                var lstDriverFeeLocationFrom = model.CAT_DriverFeeRouting.Where(c => c.DriverFeeID == driverFee.ID && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                                    .Select(c => c.LocationID.Value).ToList();
                                var lstDriverFeeLocationTo = model.CAT_DriverFeeRouting.Where(c => c.DriverFeeID == driverFee.ID && (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                                    .Select(c => c.LocationID.Value).ToList();
                                var lstDriverFeeGroupProduct = model.CAT_DriverFeeGroupProduct.Where(c => c.DriverFeeID == driverFee.ID && c.GroupOfProductID > 0)
                                   .Select(c => c.GroupOfProductID).ToList();
                                var lstDriverFeePacking = model.CAT_DriverFeePacking.Where(c => c.DriverFeeID == driverFee.ID && c.PackingID > 0)
                                    .Select(c => c.PackingID).ToList();

                                if (lstDriverFeeCustomer.Count > 0)
                                {
                                    queryMasterDriverFee = queryMasterDriverFee.Where(c => lstDriverFeeCustomer.Contains(c.PHTCustomerID.Value)).ToList();
                                    queryMasterCODriverFee = queryMasterCODriverFee.Where(c => lstDriverFeeCustomer.Contains(c.PHTCustomerID.Value)).ToList();
                                }
                                if (lstDriverFeeParentRouting.Count > 0)
                                {
                                    queryMasterDriverFee = queryMasterDriverFee.Where(c => lstDriverFeeParentRouting.Contains(c.ParentRoutingID)).ToList();
                                    queryMasterCODriverFee = queryMasterCODriverFee.Where(c => lstDriverFeeParentRouting.Contains(c.ParentRoutingID)).ToList();
                                }
                                if (lstDriverFeeRouting.Count > 0)
                                {
                                    queryMasterDriverFee = queryMasterDriverFee.Where(c => lstDriverFeeRouting.Contains(c.CATRoutingID)).ToList();
                                    queryMasterCODriverFee = queryMasterCODriverFee.Where(c => lstDriverFeeRouting.Contains(c.CATRoutingID)).ToList();
                                }
                                if (lstDriverFeeGroupLocation.Count > 0)
                                {
                                    queryMasterDriverFee = queryMasterDriverFee.Where(c => lstDriverFeeGroupLocation.Contains(c.PHTGroupOfLocationID.Value)).ToList();
                                    queryMasterCODriverFee = queryMasterCODriverFee.Where(c => lstDriverFeeGroupLocation.Contains(c.PHTGroupOfLocationID.Value)).ToList();
                                }
                                if (lstDriverFeePacking.Count > 0)
                                {
                                    queryMasterCODriverFee = queryMasterCODriverFee.Where(c => lstDriverFeePacking.Contains(c.PHTPackingID.Value)).ToList();
                                }

                                // Xe tải
                                var lstMasterCheck = queryMasterDriverFee.ToList();
                                var lstDriverID = lstMasterCheck.Where(c => c.DriverID1.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID1))).Select(c => c.DriverID1).Distinct().ToList();
                                lstDriverID.AddRange(lstMasterCheck.Where(c => c.DriverID2.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID2))).Select(c => c.DriverID2).Distinct().ToList());
                                lstDriverID.AddRange(lstMasterCheck.Where(c => c.DriverID3.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID3))).Select(c => c.DriverID3).Distinct().ToList());
                                lstDriverID.AddRange(lstMasterCheck.Where(c => c.DriverID4.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID4))).Select(c => c.DriverID4).Distinct().ToList());
                                lstDriverID.AddRange(lstMasterCheck.Where(c => c.DriverID5.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID5))).Select(c => c.DriverID5).Distinct().ToList());
                                lstDriverID = lstDriverID.Distinct().ToList();

                                // Container
                                var lstMasterCOCheck = queryMasterCODriverFee.ToList();
                                var lstDriverCOID = lstMasterCOCheck.Where(c => c.DriverID1.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID1))).Select(c => c.DriverID1).Distinct().ToList();
                                lstDriverCOID.AddRange(lstMasterCOCheck.Where(c => c.DriverID2.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID2))).Select(c => c.DriverID2).Distinct().ToList());
                                lstDriverCOID.AddRange(lstMasterCOCheck.Where(c => c.DriverID3.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID3))).Select(c => c.DriverID3).Distinct().ToList());
                                lstDriverCOID.AddRange(lstMasterCOCheck.Where(c => c.DriverID4.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID4))).Select(c => c.DriverID4).Distinct().ToList());
                                lstDriverCOID.AddRange(lstMasterCOCheck.Where(c => c.DriverID5.HasValue && (driverFee.TypeOfDriverID == null || (driverFee.TypeOfDriverID == c.TypeOfDriverID5))).Select(c => c.DriverID5).Distinct().ToList());
                                lstDriverCOID = lstDriverCOID.Distinct().ToList();

                                #region Tính theo tổng chuyến
                                if (driverFee.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumTotal)
                                {
                                    #region Xe tải
                                    foreach (var driverID in lstDriverID)
                                    {
                                        var lstTemp = lstMasterCheck.Where(c => (c.DriverID1 == driverID || c.DriverID2 == driverID || c.DriverID3 == driverID || c.DriverID4 == driverID || c.DriverID5 == driverID));
                                        DTOPriceDIExExpr itemExpr = new DTOPriceDIExExpr();
                                        itemExpr.TonTransfer = lstTemp.Sum(c => c.TonTranfer);
                                        itemExpr.CBMTransfer = lstTemp.Sum(c => c.CBMTranfer);
                                        itemExpr.QuantityTransfer = lstTemp.Sum(c => c.QuantityTranfer);
                                        itemExpr.TonReturn = lstTemp.Sum(c => c.TonReturn);
                                        itemExpr.CBMReturn = lstTemp.Sum(c => c.CBMReturn);
                                        itemExpr.QuantityReturn = lstTemp.Sum(c => c.QuantityReturn);
                                        itemExpr.TotalSchedule = lstTemp.Select(c => c.ID).Distinct().Count();
                                        bool flag = false;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, driverFee.ExprInput);
                                        }
                                        catch { flag = false; }

                                        if (flag == true)
                                        {
                                            FIN_PL pl = new FIN_PL();
                                            pl.IsPlanning = false;
                                            pl.Effdate = schedule.DateTo.Date;
                                            pl.Code = string.Empty;
                                            pl.CreatedBy = Account.UserName;
                                            pl.CreatedDate = DateTime.Now;
                                            pl.SYSCustomerID = Account.SYSCustomerID;
                                            pl.VendorID = itemContract.VendorID;
                                            pl.ContractID = itemContract.ID;
                                            pl.CustomerID = itemContract.CustomerID;
                                            pl.DriverID = driverID;
                                            pl.ScheduleID = schedule.ID;
                                            pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                            decimal? priceFix = null, priceUnit = null, quantity = null;
                                            if (!string.IsNullOrEmpty(driverFee.ExprPriceFix))
                                                priceFix = Expression_GetValue(Expression_GetPackage(driverFee.ExprPriceFix), itemExpr);
                                            if (!string.IsNullOrEmpty(driverFee.ExprUnitPrice))
                                                priceUnit = Expression_GetValue(Expression_GetPackage(driverFee.ExprUnitPrice), itemExpr);
                                            if (!string.IsNullOrEmpty(driverFee.ExprQuantity))
                                                quantity = Expression_GetValue(Expression_GetPackage(driverFee.ExprQuantity), itemExpr);

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                                plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                plCost.Note = driverFee.FeeName;
                                                plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                plCost.Debit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;
                                            }

                                            if (priceUnit.HasValue && quantity.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                                plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                plCost.Note = driverFee.FeeName;
                                                plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                plCost.Quantity = (double)quantity.Value;
                                                plCost.UnitPrice = priceUnit.Value;
                                                plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;
                                            }

                                            if (pl.FIN_PLDetails.Count > 0)
                                                lstPl.Add(pl);
                                        }
                                    }
                                    #endregion

                                    #region Container
                                    foreach (var driverID in lstDriverCOID)
                                    {
                                        var lstTemp = lstMasterCOCheck.Where(c => (c.DriverID1 == driverID || c.DriverID2 == driverID || c.DriverID3 == driverID || c.DriverID4 == driverID || c.DriverID5 == driverID));
                                        DTOPriceDIExExpr itemExpr = new DTOPriceDIExExpr();
                                        itemExpr.TotalSchedule = lstTemp.Select(c => c.ID).Distinct().Count();
                                        itemExpr.TotalPacking = lstTemp.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Count();
                                        bool flag = false;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, driverFee.ExprInput);
                                        }
                                        catch { flag = false; }

                                        if (flag == true)
                                        {
                                            FIN_PL pl = new FIN_PL();
                                            pl.IsPlanning = false;
                                            pl.Effdate = schedule.DateTo.Date;
                                            pl.Code = string.Empty;
                                            pl.CreatedBy = Account.UserName;
                                            pl.CreatedDate = DateTime.Now;
                                            pl.SYSCustomerID = Account.SYSCustomerID;
                                            pl.VendorID = itemContract.VendorID;
                                            pl.ContractID = itemContract.ID;
                                            pl.CustomerID = itemContract.CustomerID;
                                            pl.DriverID = driverID;
                                            pl.ScheduleID = schedule.ID;
                                            pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                            decimal? priceFix = null, priceUnit = null, quantity = null;
                                            if (!string.IsNullOrEmpty(driverFee.ExprPriceFix))
                                                priceFix = Expression_GetValue(Expression_GetPackage(driverFee.ExprPriceFix), itemExpr);
                                            if (!string.IsNullOrEmpty(driverFee.ExprUnitPrice))
                                                priceUnit = Expression_GetValue(Expression_GetPackage(driverFee.ExprUnitPrice), itemExpr);
                                            if (!string.IsNullOrEmpty(driverFee.ExprQuantity))
                                                quantity = Expression_GetValue(Expression_GetPackage(driverFee.ExprQuantity), itemExpr);

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                                plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                plCost.Note = driverFee.FeeName;
                                                plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                plCost.Debit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;
                                            }

                                            if (priceUnit.HasValue && quantity.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                                plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                plCost.Note = driverFee.FeeName;
                                                plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                plCost.Quantity = (double)quantity.Value;
                                                plCost.UnitPrice = priceUnit.Value;
                                                plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;
                                            }

                                            var lstGroupProduct = model.CAT_DriverFeePacking.Where(c => c.DriverFeeID == driverFee.ID && !string.IsNullOrEmpty(c.ExprQuantity) && !string.IsNullOrEmpty(c.ExprPrice));
                                            foreach (var itemGroupProduct in lstGroupProduct)
                                            {
                                                var lstTempGroup = lstTemp.Where(c => c.PHTPackingID == itemGroupProduct.PackingID);
                                                DTOPriceDIExExpr itemExprGroup = new DTOPriceDIExExpr();
                                                itemExprGroup.TotalPacking = lstTempGroup.Count();

                                                decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprPrice), itemExprGroup);
                                                decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprQuantity), itemExprGroup);
                                                if (priceGroupMOQ.HasValue && quantityGroupMOQ.HasValue)
                                                {
                                                    foreach (var itemCotainer in lstTempGroup)
                                                    {
                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.FLMDriverFeeNoGroup;
                                                        plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                        plCost.Note = driverFee.FeeName;
                                                        plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                        plCost.Quantity = (double)quantityGroupMOQ.Value;
                                                        plCost.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        pl.Debit += plCost.Debit;

                                                        FIN_PLContainer plGroup = new FIN_PLContainer();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.COTOContainerID = itemCotainer.ID;
                                                        plGroup.Quantity = (double)quantityGroupMOQ.Value;
                                                        plGroup.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.FIN_PLContainer.Add(plGroup);
                                                    }
                                                }
                                            }

                                            if (pl.FIN_PLDetails.Count > 0)
                                                lstPl.Add(pl);
                                        }
                                    }
                                    #endregion
                                }
                                #endregion

                                #region Tính theo từng chuyến
                                if (driverFee.DriverFeeSumID == -(int)SYSVarType.DriverFeeSumSchedule)
                                {
                                    #region Xe tải
                                    foreach (var driverID in lstDriverID)
                                    {
                                        var lstMasterGroup = lstMasterCheck.Where(c => (c.DriverID1 == driverID || c.DriverID2 == driverID || c.DriverID3 == driverID || c.DriverID4 == driverID || c.DriverID5 == driverID)).GroupBy(c => c.ID);
                                        foreach (var itemMasterGroup in lstMasterGroup)
                                        {
                                            HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                            itemExpr.TonTransfer = itemMasterGroup.Sum(c => c.TonTranfer);
                                            itemExpr.CBMTransfer = itemMasterGroup.Sum(c => c.CBMTranfer);
                                            itemExpr.QuantityTransfer = itemMasterGroup.Sum(c => c.QuantityTranfer);
                                            itemExpr.TonReturn = itemMasterGroup.Sum(c => c.TonReturn);
                                            itemExpr.CBMReturn = itemMasterGroup.Sum(c => c.CBMReturn);
                                            itemExpr.QuantityReturn = itemMasterGroup.Sum(c => c.QuantityReturn);
                                            itemExpr.TotalSchedule = lstMasterGroup.Count();
                                            var master = lstMasterCheck.FirstOrDefault(c => c.ID == itemMasterGroup.Key);
                                            itemExpr.ExIsOverNight = master.ExIsOverNight;
                                            itemExpr.ExIsOverWeight = master.ExIsOverWeight;
                                            itemExpr.ExTotalDayOut = master.ExTotalDayOut;
                                            itemExpr.ExTotalJoin = master.ExTotalJoin;
                                            itemExpr.PHTLoading = master.PHTLoading;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID4.HasValue && master.TypeOfDriverID4 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID5.HasValue && master.TypeOfDriverID5 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;

                                            itemExpr.TotalDriverEx += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID4.HasValue && master.TypeOfDriverID4 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID5.HasValue && master.TypeOfDriverID5 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;

                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID4.HasValue && master.TypeOfDriverID4 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID5.HasValue && master.TypeOfDriverID5 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;

                                            itemExpr.SortInDay = master.SortOrder;
                                            bool flag = false;
                                            try
                                            {
                                                flag = Expression_FLMCheckBool(itemExpr, driverFee.ExprInput);
                                            }
                                            catch { flag = false; }

                                            if (flag == true)
                                            {
                                                FIN_PL pl = new FIN_PL();
                                                pl.IsPlanning = false;
                                                pl.Effdate = lstMasterCheck.FirstOrDefault(c => c.ID == itemMasterGroup.Key).DateConfig.Date;
                                                pl.Code = string.Empty;
                                                pl.CreatedBy = Account.UserName;
                                                pl.CreatedDate = DateTime.Now;
                                                pl.SYSCustomerID = Account.SYSCustomerID;
                                                pl.VendorID = itemContract.VendorID;
                                                pl.ContractID = itemContract.ID;
                                                pl.CustomerID = itemContract.CustomerID;
                                                pl.DriverID = driverID;
                                                pl.DITOMasterID = itemMasterGroup.Key;
                                                pl.VehicleID = master.VehicleID;
                                                pl.ScheduleID = schedule.ID;
                                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                                decimal? priceFix = null, priceUnit = null, quantity = null;
                                                if (!string.IsNullOrEmpty(driverFee.ExprPriceFix))
                                                    priceFix = Expression_FLMGetValue(itemExpr, driverFee.ExprPriceFix);
                                                if (!string.IsNullOrEmpty(driverFee.ExprUnitPrice))
                                                    priceUnit = Expression_FLMGetValue(itemExpr, driverFee.ExprUnitPrice);
                                                if (!string.IsNullOrEmpty(driverFee.ExprQuantity))
                                                    quantity = Expression_FLMGetValue(itemExpr, driverFee.ExprQuantity);

                                                if (priceFix.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Debit = priceFix.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;
                                                }

                                                if (priceUnit.HasValue && quantity.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Quantity = (double)quantity.Value;
                                                    plCost.UnitPrice = priceUnit.Value;
                                                    plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;
                                                }

                                                if (pl.FIN_PLDetails.Count > 0)
                                                {
                                                    FIN_PL plPL = new FIN_PL();
                                                    CopyFinPL(pl, plPL);
                                                    plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                                    lstPl.Add(pl);
                                                    lstPl.Add(plPL);
                                                }
                                            }
                                        }
                                    }
                                    #endregion

                                    #region Container
                                    foreach (var driverID in lstDriverCOID)
                                    {
                                        var lstMasterGroup = lstMasterCOCheck.Where(c => (c.DriverID1 == driverID || c.DriverID2 == driverID || c.DriverID3 == driverID || c.DriverID4 == driverID || c.DriverID5 == driverID)).GroupBy(c => c.ID);
                                        foreach (var itemMasterGroup in lstMasterGroup)
                                        {
                                            HelperFinance_FLMCheck itemExpr = new HelperFinance_FLMCheck();
                                            itemExpr.TotalSchedule = lstMasterGroup.Count();
                                            var master = lstMasterCheck.FirstOrDefault(c => c.ID == itemMasterGroup.Key);
                                            itemExpr.ExIsOverNight = master.ExIsOverNight;
                                            itemExpr.ExIsOverWeight = master.ExIsOverWeight;
                                            itemExpr.ExTotalDayOut = master.ExTotalDayOut;
                                            itemExpr.ExTotalJoin = master.ExTotalJoin;
                                            itemExpr.PHTLoading = master.PHTLoading;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID4.HasValue && master.TypeOfDriverID4 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;
                                            itemExpr.TotalDriverMain += master.TypeOfDriverID5.HasValue && master.TypeOfDriverID5 == -(int)SYSVarType.TypeOfDriverMain ? 1 : 0;

                                            itemExpr.TotalDriverEx += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID4.HasValue && master.TypeOfDriverID4 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;
                                            itemExpr.TotalDriverEx += master.TypeOfDriverID5.HasValue && master.TypeOfDriverID5 == -(int)SYSVarType.TypeOfDriverEx ? 1 : 0;

                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID1.HasValue && master.TypeOfDriverID1 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID2.HasValue && master.TypeOfDriverID2 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID3.HasValue && master.TypeOfDriverID3 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID4.HasValue && master.TypeOfDriverID4 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;
                                            itemExpr.TotalDriverLoad += master.TypeOfDriverID5.HasValue && master.TypeOfDriverID5 == -(int)SYSVarType.TypeOfDriverLoad ? 1 : 0;

                                            itemExpr.SortInDay = master.SortOrder;
                                            itemExpr.PackingCode = master.PackingCode;
                                            itemExpr.TotalPacking = itemMasterGroup.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Count();
                                            bool flag = false;
                                            try
                                            {
                                                flag = Expression_FLMCheckBool(itemExpr, driverFee.ExprInput);
                                            }
                                            catch { flag = false; }

                                            if (flag == true)
                                            {
                                                FIN_PL pl = new FIN_PL();
                                                pl.IsPlanning = false;
                                                pl.Effdate = lstMasterCheck.FirstOrDefault(c => c.ID == itemMasterGroup.Key).DateConfig.Date;
                                                pl.Code = string.Empty;
                                                pl.CreatedBy = Account.UserName;
                                                pl.CreatedDate = DateTime.Now;
                                                pl.SYSCustomerID = Account.SYSCustomerID;
                                                pl.VendorID = itemContract.VendorID;
                                                pl.ContractID = itemContract.ID;
                                                pl.CustomerID = itemContract.CustomerID;
                                                pl.DriverID = driverID;
                                                pl.COTOMasterID = itemMasterGroup.Key;
                                                pl.VehicleID = master.VehicleID;
                                                pl.ScheduleID = schedule.ID;
                                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                                decimal? priceFix = null, priceUnit = null, quantity = null;
                                                if (!string.IsNullOrEmpty(driverFee.ExprPriceFix))
                                                    priceFix = Expression_FLMGetValue(itemExpr, driverFee.ExprPriceFix);
                                                if (!string.IsNullOrEmpty(driverFee.ExprUnitPrice))
                                                    priceUnit = Expression_FLMGetValue(itemExpr, driverFee.ExprUnitPrice);
                                                if (!string.IsNullOrEmpty(driverFee.ExprQuantity))
                                                    quantity = Expression_FLMGetValue(itemExpr, driverFee.ExprQuantity);

                                                if (priceFix.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Debit = priceFix.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;
                                                }

                                                if (priceUnit.HasValue && quantity.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                    plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                    plCost.Note = driverFee.FeeName;
                                                    plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                    plCost.Quantity = (double)quantity.Value;
                                                    plCost.UnitPrice = priceUnit.Value;
                                                    plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;
                                                }

                                                var lstGroupProduct = model.CAT_DriverFeePacking.Where(c => c.DriverFeeID == driverFee.ID && !string.IsNullOrEmpty(c.ExprQuantity) && !string.IsNullOrEmpty(c.ExprPrice));
                                                foreach (var itemGroupProduct in lstGroupProduct)
                                                {
                                                    var lstTempGroup = itemMasterGroup.Where(c => c.PHTPackingID == itemGroupProduct.PackingID);
                                                    DTOPriceDIExExpr itemExprGroup = new DTOPriceDIExExpr();
                                                    itemExprGroup.TotalPacking = lstTempGroup.Count();

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprPrice), itemExprGroup);
                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemGroupProduct.ExprQuantity), itemExprGroup);

                                                    if (priceGroupMOQ.HasValue && quantityGroupMOQ.HasValue)
                                                    {
                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.FLMDriverFee;
                                                        plCost.TypeOfPriceDIExCode = driverFee.TypeOfDriverFeeCode;
                                                        plCost.Note = driverFee.FeeName;
                                                        plCost.Note1 = driverFee.TypeOfDriverFeeName;
                                                        plCost.Quantity = (double)quantityGroupMOQ.Value;
                                                        plCost.UnitPrice = priceGroupMOQ.Value;
                                                        plCost.Debit = plCost.UnitPrice.Value * (decimal)plCost.Quantity.Value;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        pl.Debit += plCost.Debit;
                                                    }
                                                }

                                                if (pl.FIN_PLDetails.Count > 0)
                                                {
                                                    FIN_PL plPL = new FIN_PL();
                                                    CopyFinPL(pl, plPL);
                                                    plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                                    lstPl.Add(pl);
                                                    lstPl.Add(plPL);
                                                }
                                            }
                                        }
                                    }
                                    #endregion
                                }
                                #endregion
                            }
                            #endregion

                            #region Tính trouble
                            var lstMasterID = queryMasterContract.Select(c => c.ID).Distinct().ToList();
                            var lstMasterCOID = queryMasterContractCO.Select(c => c.ID).Distinct().ToList();
                            var lstTrouble = model.CAT_Trouble.Where(c => ((c.DITOMasterID.HasValue && lstMasterID.Contains(c.DITOMasterID.Value)) || (c.COTOMasterID.HasValue && lstMasterCOID.Contains(c.COTOMasterID.Value))) && c.TroubleCostStatusID == -(int)SYSVarType.TroubleCostStatusApproved && c.CostOfVendor != 0);
                            if (lstTrouble != null && lstTrouble.Count() > 0)
                            {
                                foreach (var itemTrouble in lstTrouble)
                                {
                                    var master = queryMasterContract.FirstOrDefault(c => c.ID == itemTrouble.DITOMasterID);
                                    var masterCO = queryMasterContractCO.FirstOrDefault(c => c.ID == itemTrouble.COTOMasterID);
                                    if (master != null || masterCO != null)
                                    {
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = master.DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.DriverID = itemTrouble.DriverID;
                                        pl.VehicleID = master.VehicleID;
                                        pl.ScheduleID = schedule.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;
                                        if (itemTrouble.DITOMasterID > 0)
                                            pl.DITOMasterID = itemTrouble.DITOMasterID;
                                        if (itemTrouble.COTOMasterID > 0)
                                            pl.COTOMasterID = itemTrouble.COTOMasterID;

                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                        plCost.CreatedBy = Account.UserName;
                                        plCost.CreatedDate = DateTime.Now;
                                        plCost.CostID = (int)CATCostType.TroubleDebit;
                                        plCost.Debit = itemTrouble.CostOfVendor;
                                        plCost.TypeOfPriceDIExCode = itemTrouble.CAT_GroupOfTrouble.Code;
                                        pl.Debit = plCost.Debit;
                                        pl.FIN_PLDetails.Add(plCost);

                                        FIN_PL plPL = new FIN_PL();
                                        CopyFinPL(pl, plPL);
                                        plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        lstPl.Add(pl);
                                        lstPl.Add(plPL);
                                    }
                                }
                            }
                            #endregion
                        }

                        System.Diagnostics.Debug.WriteLine("Contract end: " + itemContract.ID);
                    }

                    foreach (var pl in lstPl)
                        model.FIN_PL.Add(pl);
                }
            }
            #endregion
        }

        // Phân bổ chi phí cố định cho xe và tài xế
        public static void Truck_CalculateFee_Temp(DataEntities model, AccountItem Account, int scheduleID)
        {
            List<FIN_PL> lstPL = new List<FIN_PL>();
            List<FIN_Temp> lstFinTemp = new List<FIN_Temp>();
            var schedule = model.FLM_Schedule.FirstOrDefault(c => c.ID == scheduleID);
            var lstFINTemp = model.FIN_Temp.Where(c => c.ScheduleID == scheduleID);

            var DateConfig = schedule.DateFrom.Date;
            var DateConfigEnd = schedule.DateTo.Date.AddDays(1);
            var DateConfigEndDate = schedule.DateTo.Date.AddDays(1);
            // Danh sách loại ngày
            var lstScheduleDate = model.FLM_ScheduleDate.Where(c => c.ScheduleID == schedule.ID);
            var lstScheduleDateDriver = model.FLM_ScheduleDateDetail.Where(c => c.FLM_ScheduleDate.ScheduleID == schedule.ID);
            var lstScheduleDriver = model.FLM_ScheduleDriver.Where(c => c.ScheduleID == schedule.ID);

            // Dữ liệu chuyến
            // Lấy phiếu hành trình xe tải
            var lstMaster = model.OPS_DITOMaster.Where(c => c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig <= DateConfigEndDate && c.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterPHT && c.ScheduleID == scheduleID && c.PHTCustomerID > 0).Select(c => new HelperFinance_TOMaster
            {
                ID = c.ID,
                GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                DateConfig = c.DateConfig.Value,
                VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                VehicleID = c.VehicleID,
                DriverID1 = c.DriverID1,
                DriverID2 = c.DriverID2,
                DriverID3 = c.DriverID3,
                DriverID4 = c.DriverID4,
                DriverID5 = c.DriverID5,
                TypeOfDriverID1 = c.TypeOfDriverID1,
                TypeOfDriverID2 = c.TypeOfDriverID2,
                TypeOfDriverID3 = c.TypeOfDriverID3,
                TypeOfDriverID4 = c.TypeOfDriverID4,
                TypeOfDriverID5 = c.TypeOfDriverID5,
                ExIsOverNight = c.ExIsOverNight,
                ExIsOverWeight = c.ExIsOverWeight,
                ExTotalDayOut = c.ExTotalDayOut,
                ExTotalJoin = c.ExTotalJoin,
                ETD = c.ETD,
                SortOrder = c.SortOrder,
                PHTCustomerID = c.PHTCustomerID,
                PHTGroupOfLocationID = c.PHTGroupOfLocationID.HasValue ? c.PHTGroupOfLocationID.Value : -1,
                PHTLoading = c.PHTLoading.HasValue ? c.PHTLoading.Value : false,
                PHTPackingID = -1,
            }).ToList();
            // Container
            var lstMasterCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig <= DateConfigEndDate && c.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterPHT && c.ScheduleID == scheduleID && c.PHTCustomerID > 0).Select(c => new HelperFinance_COMaster
            {
                ID = c.ID,
                GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                CATRoutingID = c.RoutingID > 0 ? c.RoutingID.Value : -1,
                ParentRoutingID = c.RoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                DateConfig = c.DateConfig.Value,
                VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                VehicleID = c.VehicleID,
                DriverID1 = c.DriverID1,
                DriverID2 = c.DriverID2,
                DriverID3 = c.DriverID3,
                DriverID4 = c.DriverID4,
                DriverID5 = c.DriverID5,
                TypeOfDriverID1 = c.TypeOfDriverID1,
                TypeOfDriverID2 = c.TypeOfDriverID2,
                TypeOfDriverID3 = c.TypeOfDriverID3,
                TypeOfDriverID4 = c.TypeOfDriverID4,
                TypeOfDriverID5 = c.TypeOfDriverID5,
                ExIsOverNight = c.ExIsOverNight,
                ExIsOverWeight = c.ExIsOverWeight,
                ExTotalDayOut = c.ExTotalDayOut,
                ExTotalJoin = c.ExTotalJoin,
                ETD = c.ETD,
                SortOrder = c.SortOrder,
                PHTCustomerID = c.PHTCustomerID,
                PHTGroupOfLocationID = c.PHTGroupOfLocationID.HasValue ? c.PHTGroupOfLocationID.Value : -1,
                PHTLoading = c.PHTLoading.HasValue ? c.PHTLoading.Value : false,
                PHTPackingID = c.PHTPackingID > 0 ? c.PHTPackingID.Value : -1,
            }).Distinct().ToList();

            #region Tính cho Asset => ko lưu driverID
            var lstScheduleFeeAsset = model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.AssetID.HasValue && ((!string.IsNullOrEmpty(c.ExprPriceDay) && !string.IsNullOrEmpty(c.ExprInputDay)) || (!string.IsNullOrEmpty(c.ExprPriceTOMaster) && !string.IsNullOrEmpty(c.ExprInputTOMaster))));
            if (lstScheduleFeeAsset != null && lstScheduleFeeAsset.Count() > 0)
            {
                foreach (var itemScheduleFeeAsset in lstScheduleFeeAsset)
                {
                    var lstOPSGroupProductAsset = lstMaster.Where(c => c.VehicleID == itemScheduleFeeAsset.FLM_Asset.VehicleID);
                    var lstOPSContainerAsset = lstMasterCO.Where(c => c.VehicleID == itemScheduleFeeAsset.FLM_Asset.VehicleID);
                    // Item check công thức
                    HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                    itemCheck.Price = itemScheduleFeeAsset.Price.HasValue ? itemScheduleFeeAsset.Price.Value : 0;
                    itemCheck.TotalDay = schedule.TotalDays;
                    itemCheck.TotalDayActual = (schedule.DateTo.Date - schedule.DateFrom.Date).TotalDays + 1;
                    itemCheck.TotalDayHoliday = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
                    itemCheck.TotalDayOff = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                    itemCheck.TotalDayOn = lstScheduleDate.Count(c => c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                    if (lstOPSGroupProductAsset.Count() > 0)
                    {
                        itemCheck.TotalTon = lstOPSGroupProductAsset.Sum(c => c.TonTranfer);
                        itemCheck.TotalCBM = lstOPSGroupProductAsset.Sum(c => c.CBMTranfer);
                        itemCheck.TotalSchedule = lstOPSGroupProductAsset.Select(c => c.ID).Distinct().Count();
                        itemCheck.TotalDaySchedule = lstOPSGroupProductAsset.Select(c => c.DateConfig).Distinct().Count();
                    }
                    if (lstOPSContainerAsset.Count() > 0)
                    {
                        itemCheck.TotalPacking = lstOPSContainerAsset.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Distinct().Count();
                        itemCheck.TotalSchedule = lstOPSContainerAsset.Select(c => c.ID).Distinct().Count();
                        itemCheck.TotalDaySchedule = lstOPSContainerAsset.Select(c => c.DateConfig).Distinct().Count();
                    }

                    #region Phân bổ cho từng ngày của schedule
                    if (!string.IsNullOrEmpty(itemScheduleFeeAsset.ExprPriceDay))
                    {
                        DateTime DateFrom = schedule.DateFrom.Date;
                        while (DateFrom <= schedule.DateTo.Date)
                        {
                            itemCheck.TotalTonInDay = 0;
                            itemCheck.TotalCBMInDay = 0;
                            itemCheck.TotalScheduleInDay = 0;
                            itemCheck.TotalPackingInDay = 0;

                            var lstGroupInDay = lstOPSGroupProductAsset.Where(c => c.DateConfig == DateFrom);
                            if (lstGroupInDay != null && lstGroupInDay.Count() > 0)
                            {
                                itemCheck.TotalTonInDay = lstGroupInDay.Sum(c => c.TonTranfer);
                                itemCheck.TotalCBMInDay = lstGroupInDay.Sum(c => c.CBMTranfer);
                                itemCheck.TotalScheduleInDay = lstGroupInDay.Select(c => c.ID).Distinct().Count();
                            }
                            var lstContainerInDay = lstOPSContainerAsset.Where(c => c.DateConfig == DateFrom);
                            if (lstContainerInDay != null && lstContainerInDay.Count() > 0)
                            {
                                itemCheck.TotalPackingInDay = lstContainerInDay.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Distinct().Count();
                                itemCheck.TotalScheduleInDay = lstContainerInDay.Select(c => c.ID).Distinct().Count();
                            }

                            bool flag = false;
                            itemCheck.IsWorking = lstOPSGroupProductAsset.Any(c => c.DateConfig == DateFrom) || lstContainerInDay.Any(c => c.DateConfig == DateFrom);
                            itemCheck.IsDayOn = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                            itemCheck.IsDayOff = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                            itemCheck.IsDayHoliday = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);
                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeAsset.ExprInputDay);
                            }
                            catch { }

                            if (flag)
                            {
                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeAsset.ExprPriceDay);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeAsset.ExprPriceDay);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateFrom.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.VehicleID = itemScheduleFeeAsset.FLM_Asset.VehicleID;
                                pl.ScheduleID = schedule.ID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                lstPL.Add(pl);

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFeeNoGroup;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.Code;
                                plCost.Note = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.TypeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                            }
                            DateFrom = DateFrom.AddDays(1);
                        }
                    }
                    #endregion

                    #region Phân bổ cho từng chuyến xe tải
                    if (!string.IsNullOrEmpty(itemScheduleFeeAsset.ExprPriceTOMaster))
                    {
                        var lstMasterID = lstOPSGroupProductAsset.Select(c => c.ID).Distinct().ToList();
                        foreach (var itemMasterID in lstMasterID)
                        {
                            var lstGroup = lstOPSGroupProductAsset.Where(c => c.ID == itemMasterID).OrderByDescending(c => c.TonTranfer);
                            var master = lstOPSGroupProductAsset.FirstOrDefault(c => c.ID == itemMasterID);
                            itemCheck.TotalTonInSchedule = lstGroup.Sum(c => c.TonTranfer);
                            itemCheck.TotalCBMInSchedule = lstGroup.Sum(c => c.CBMTranfer);
                            itemCheck.SortInDay = master.SortOrder;

                            bool flag = false;

                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeAsset.ExprInputTOMaster);
                            }
                            catch { }

                            if (flag)
                            {
                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeAsset.ExprPriceTOMaster);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeAsset.ExprPriceTOMaster);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = lstGroup.FirstOrDefault().DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.VehicleID = itemScheduleFeeAsset.FLM_Asset.VehicleID;
                                pl.ScheduleID = schedule.ID;
                                pl.DITOMasterID = itemMasterID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.Code;
                                plCost.Note = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.TypeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                                FIN_PL plPL = new FIN_PL();
                                CopyFinPL(pl, plPL);
                                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                lstPL.Add(pl);
                                lstPL.Add(plPL);
                            }
                        }
                    }
                    #endregion

                    #region Phân bổ cho từng chuyến xe Con
                    if (!string.IsNullOrEmpty(itemScheduleFeeAsset.ExprPriceTOMaster))
                    {
                        var lstMasterID = lstMasterCO.Select(c => c.ID).Distinct().ToList();
                        foreach (var itemMasterID in lstMasterID)
                        {
                            var lstGroup = lstOPSContainerAsset.Where(c => c.ID == itemMasterID);
                            var master = lstOPSContainerAsset.FirstOrDefault(c => c.ID == itemMasterID);
                            itemCheck.TotalPackingInDay = lstGroup.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Distinct().Count();
                            itemCheck.SortInDay = master.SortOrder;

                            bool flag = false;

                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeAsset.ExprInputTOMaster);
                            }
                            catch { }

                            if (flag)
                            {
                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeAsset.ExprPriceTOMaster);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeAsset.ExprPriceTOMaster);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = lstGroup.FirstOrDefault().DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.VehicleID = itemScheduleFeeAsset.FLM_Asset.VehicleID;
                                pl.ScheduleID = schedule.ID;
                                pl.COTOMasterID = itemMasterID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.Code;
                                plCost.Note = itemScheduleFeeAsset.FLM_TypeOfScheduleFee.TypeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                                FIN_PL plPL = new FIN_PL();
                                CopyFinPL(pl, plPL);
                                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                lstPL.Add(pl);
                                lstPL.Add(plPL);
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Tính cho Driver
            var lstScheduleFeeDriverOther = model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.DriverID.HasValue && c.Day.HasValue).Select(c => new HelperFinance_FLMCheckDetail
            {
                DriverID = c.DriverID.Value,
                TypeOfScheduleFeeID = c.TypeOfScheduleFeeID,
                TypeOfScheduleFeeCode = c.FLM_TypeOfScheduleFee.Code,
                TypeOfScheduleFeeDay = c.Day.HasValue ? c.Day.Value : 0,
            }).ToList();

            var lstScheduleFeeDriver = model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.DriverID.HasValue && ((!string.IsNullOrEmpty(c.ExprPriceDay) && !string.IsNullOrEmpty(c.ExprInputDay)) || (!string.IsNullOrEmpty(c.ExprPriceTOMaster) && !string.IsNullOrEmpty(c.ExprInputTOMaster))));
            if (lstScheduleFeeDriver != null && lstScheduleFeeDriver.Count() > 0)
            {
                foreach (var itemScheduleFeeDriver in lstScheduleFeeDriver)
                {
                    var lstOPSGroupProductDriver = lstMaster.Where(c => (c.DriverID1 == itemScheduleFeeDriver.DriverID || c.DriverID2 == itemScheduleFeeDriver.DriverID || c.DriverID3 == itemScheduleFeeDriver.DriverID || c.DriverID4 == itemScheduleFeeDriver.DriverID || c.DriverID5 == itemScheduleFeeDriver.DriverID));
                    var lstOPSContainerDriver = lstMasterCO.Where(c => (c.DriverID1 == itemScheduleFeeDriver.DriverID || c.DriverID2 == itemScheduleFeeDriver.DriverID || c.DriverID3 == itemScheduleFeeDriver.DriverID || c.DriverID4 == itemScheduleFeeDriver.DriverID || c.DriverID5 == itemScheduleFeeDriver.DriverID));
                    // Item check công thức
                    HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                    itemCheck.Price = itemScheduleFeeDriver.Price.HasValue ? itemScheduleFeeDriver.Price.Value : 0;
                    itemCheck.TotalDay = schedule.TotalDays;
                    itemCheck.TotalDayActual = (schedule.DateTo.Date - schedule.DateFrom.Date).TotalDays + 1;
                    if (lstOPSGroupProductDriver.Count() > 0)
                    {
                        itemCheck.TotalTon = lstOPSGroupProductDriver.Sum(c => c.TonTranfer);
                        itemCheck.TotalCBM = lstOPSGroupProductDriver.Sum(c => c.CBMTranfer);
                        itemCheck.TotalSchedule = lstOPSGroupProductDriver.Select(c => c.ID).Distinct().Count();
                        itemCheck.TotalDaySchedule = lstOPSGroupProductDriver.Select(c => c.DateConfig).Distinct().Count();
                    }
                    if (lstOPSContainerDriver.Count() > 0)
                    {
                        itemCheck.TotalPacking = lstOPSContainerDriver.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Distinct().Count();
                        itemCheck.TotalSchedule = lstOPSContainerDriver.Select(c => c.ID).Distinct().Count();
                        itemCheck.TotalDaySchedule = lstOPSContainerDriver.Select(c => c.DateConfig.Date).Distinct().Count();
                    }
                    // Param bổ sung
                    var scheduleDriver = model.FLM_ScheduleDriver.FirstOrDefault(c => c.ScheduleID == schedule.ID && c.DriverID == itemScheduleFeeDriver.DriverID);
                    if (scheduleDriver != null)
                    {
                        itemCheck.FeeBase = scheduleDriver.FeeBase;
                        itemCheck.TotalDayOffDriver = scheduleDriver.DaysWorkInOff.HasValue ? scheduleDriver.DaysWorkInOff.Value : 0;
                        itemCheck.TotalDayOnDriver = scheduleDriver.DaysWorkInOn.HasValue ? scheduleDriver.DaysWorkInOn.Value : 0;
                        itemCheck.TotalDayHoliday = scheduleDriver.DaysWorkInHoliday.HasValue ? scheduleDriver.DaysWorkInHoliday.Value : 0;
                        itemCheck.TotalDayAllowOffDriver = scheduleDriver.DaysAllowOff;
                        itemCheck.TotalDayAllowOffRemainDriver = scheduleDriver.DaysAllowOffRemain.HasValue ? scheduleDriver.DaysAllowOffRemain.Value : 0;
                        itemCheck.IsAssistant = scheduleDriver.IsAssistant;
                    }

                    // Param ngày tự định nghĩa
                    itemCheck.lstCheckDetail = new List<HelperFinance_FLMCheckDetail>();
                    var lstGroupCheckDetail = lstScheduleFeeDriverOther.Where(c => c.DriverID == itemScheduleFeeDriver.DriverID).GroupBy(c => new { c.TypeOfScheduleFeeID, c.TypeOfScheduleFeeCode }).ToList();
                    foreach (var itemGroupCheckDetail in lstGroupCheckDetail)
                    {
                        HelperFinance_FLMCheckDetail itemCheckDetail = new HelperFinance_FLMCheckDetail();
                        itemCheckDetail.TypeOfScheduleFeeCode = itemGroupCheckDetail.Key.TypeOfScheduleFeeCode;
                        itemCheckDetail.TypeOfScheduleFeeDay = itemGroupCheckDetail.Sum(c => c.TypeOfScheduleFeeDay);
                        itemCheck.lstCheckDetail.Add(itemCheckDetail);
                    }

                    #region Phân bổ cho từng ngày của schedule
                    if (!string.IsNullOrEmpty(itemScheduleFeeDriver.ExprPriceDay))
                    {
                        DateTime DateFrom = schedule.DateFrom.Date;
                        while (DateFrom <= schedule.DateTo.Date)
                        {
                            bool flag = false;
                            itemCheck.IsWorking = lstScheduleDateDriver.Any(c => c.DriverID == itemScheduleFeeDriver.DriverID && c.FLM_ScheduleDate.Date == DateFrom);
                            itemCheck.IsDayOn = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                            itemCheck.IsDayOff = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                            itemCheck.IsDayHoliday = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);

                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeDriver.ExprInputDay);
                            }
                            catch { }
                            if (flag)
                            {
                                itemCheck.TotalTonInDay = 0;
                                itemCheck.TotalCBMInDay = 0;
                                itemCheck.TotalScheduleInDay = 0;
                                itemCheck.TotalPackingInDay = 0;

                                var lstGroupInDay = lstOPSGroupProductDriver.Where(c => c.DateConfig == DateFrom);
                                if (lstGroupInDay != null && lstGroupInDay.Count() > 0)
                                {
                                    itemCheck.TotalTonInDay = lstGroupInDay.Sum(c => c.TonTranfer);
                                    itemCheck.TotalCBMInDay = lstGroupInDay.Sum(c => c.CBMTranfer);
                                    itemCheck.TotalScheduleInDay = lstGroupInDay.Select(c => c.ID).Distinct().Count();
                                }
                                var lstContainerInDay = lstOPSContainerDriver.Where(c => c.DateConfig == DateFrom);
                                if (lstContainerInDay != null && lstContainerInDay.Count() > 0)
                                {
                                    itemCheck.TotalPackingInDay = lstContainerInDay.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Distinct().Count();
                                    itemCheck.TotalScheduleInDay = lstContainerInDay.Select(c => c.ID).Distinct().Count();
                                }

                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeDriver.ExprPriceDay);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeDriver.ExprPriceDay);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateFrom.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.DriverID = itemScheduleFeeDriver.DriverID;
                                pl.ScheduleID = schedule.ID;
                                pl.VehicleID = itemScheduleFeeDriver.AssetID > 0 ? itemScheduleFeeDriver.FLM_Asset.VehicleID : null;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                lstPL.Add(pl);

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFeeNoGroup;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeDriver.FLM_TypeOfScheduleFee.Code;
                                plCost.Note = itemScheduleFeeDriver.FLM_TypeOfScheduleFee.TypeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                            }

                            DateFrom = DateFrom.AddDays(1);
                        }
                    }
                    #endregion

                    #region Phân bổ cho từng chuyến xe tải
                    if (!string.IsNullOrEmpty(itemScheduleFeeDriver.ExprPriceTOMaster))
                    {
                        var lstMasterID = lstMaster.Select(c => c.ID).Distinct().ToList();
                        foreach (var itemMasterID in lstMasterID)
                        {
                            var lstGroup = lstOPSGroupProductDriver.Where(c => c.ID == itemMasterID).OrderByDescending(c => c.TonTranfer);
                            var master = lstOPSGroupProductDriver.FirstOrDefault(c => c.ID == itemMasterID);
                            itemCheck.TotalTonInSchedule = lstGroup.Sum(c => c.TonTranfer);
                            itemCheck.TotalCBMInSchedule = lstGroup.Sum(c => c.CBMTranfer);
                            itemCheck.SortInDay = master.SortOrder;

                            bool flag = false;

                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeDriver.ExprInputTOMaster);
                            }
                            catch { }

                            if (flag)
                            {
                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeDriver.ExprPriceTOMaster);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeDriver.ExprPriceTOMaster);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = lstGroup.FirstOrDefault().DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.DriverID = itemScheduleFeeDriver.DriverID;
                                pl.ScheduleID = schedule.ID;
                                pl.VehicleID = master.VehicleID;
                                pl.DITOMasterID = master.ID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeDriver.FLM_TypeOfScheduleFee.Code;
                                plCost.Note = itemScheduleFeeDriver.FLM_TypeOfScheduleFee.TypeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                                FIN_PL plPL = new FIN_PL();
                                CopyFinPL(pl, plPL);
                                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                lstPL.Add(pl);
                                lstPL.Add(plPL);
                            }
                        }
                    }
                    #endregion

                    #region Phân bổ cho từng chuyến xe Con
                    if (!string.IsNullOrEmpty(itemScheduleFeeDriver.ExprPriceTOMaster))
                    {
                        var lstMasterID = lstMasterCO.Select(c => c.ID).Distinct().ToList();
                        foreach (var itemMasterID in lstMasterID)
                        {
                            var lstGroup = lstOPSContainerDriver.Where(c => c.ID == itemMasterID);
                            var master = lstOPSContainerDriver.FirstOrDefault(c => c.ID == itemMasterID);
                            itemCheck.TotalPackingInDay = lstGroup.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Distinct().Count();
                            itemCheck.SortInDay = master.SortOrder;

                            bool flag = false;

                            try
                            {
                                flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeDriver.ExprInputTOMaster);
                            }
                            catch { }

                            if (flag)
                            {
                                decimal? price = null;
                                try
                                {
                                    price = Expression_FLMGetValue(itemCheck, itemScheduleFeeDriver.ExprPriceTOMaster);
                                }
                                catch
                                {
                                    System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeDriver.ExprPriceTOMaster);
                                }

                                // Tạo pl
                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = lstGroup.FirstOrDefault().DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = Account.SYSCustomerID;
                                pl.DriverID = itemScheduleFeeDriver.DriverID;
                                pl.ScheduleID = schedule.ID;
                                pl.VehicleID = master.VehicleID;
                                pl.COTOMasterID = master.ID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                // Detail
                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                plCost.Debit = price.HasValue ? price.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemScheduleFeeDriver.FLM_TypeOfScheduleFee.Code;
                                plCost.Note = itemScheduleFeeDriver.FLM_TypeOfScheduleFee.TypeName;
                                pl.Debit = plCost.Debit;
                                pl.FIN_PLDetails.Add(plCost);

                                FIN_PL plPL = new FIN_PL();
                                CopyFinPL(pl, plPL);
                                plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                lstPL.Add(pl);
                                lstPL.Add(plPL);
                            }
                        }
                    }
                    #endregion
                }
            }
            #endregion

            #region Tính cho Assistant
            var lstScheduleFeeAssistant = model.FLM_ScheduleFee.Where(c => c.ScheduleID == schedule.ID && c.IsAssistant.HasValue && ((!string.IsNullOrEmpty(c.ExprPriceDay) && !string.IsNullOrEmpty(c.ExprInputDay)) || (!string.IsNullOrEmpty(c.ExprPriceTOMaster) && !string.IsNullOrEmpty(c.ExprInputTOMaster)))).Select(c => new
            {
                TypeOfScheduleFeeID = c.TypeOfScheduleFeeID,
                TypeOfScheduleFeeCode = c.FLM_TypeOfScheduleFee.Code,
                TypeOfScheduleFeeName = c.FLM_TypeOfScheduleFee.TypeName,
                TypeOfScheduleFeeDay = c.Day.HasValue ? c.Day.Value : 0,
                IsAssistant = c.IsAssistant.Value,
                c.ExprDay,
                c.ExprInputDay,
                c.ExprInputTOMaster,
                c.ExprPriceDay,
                c.ExprPriceTOMaster,
                c.Price
            }).ToList();

            if (lstScheduleFeeAssistant != null && lstScheduleFeeAssistant.Count() > 0 && lstScheduleDriver.Count() > 0)
            {
                foreach (var itemScheduleFeeAssistant in lstScheduleFeeAssistant)
                {
                    // Lấy danh sách opsgroup chạy trong schedule
                    var lstDriverID = lstScheduleDriver.Where(c => c.IsAssistant == itemScheduleFeeAssistant.IsAssistant).Select(c => c.DriverID).Distinct().ToList();
                    foreach (var itemDriverID in lstDriverID)
                    {
                        var lstOPSGroupProductDriver = lstMaster.Where(c => c.DriverID1 == itemDriverID || c.DriverID2 == itemDriverID || c.DriverID3 == itemDriverID || c.DriverID4 == itemDriverID || c.DriverID5 == itemDriverID).ToList();
                        var lstOPSContainerDriver = lstMasterCO.Where(c => c.DriverID1 == itemDriverID || c.DriverID2 == itemDriverID || c.DriverID3 == itemDriverID || c.DriverID4 == itemDriverID || c.DriverID5 == itemDriverID).ToList();
                        // Item check công thức
                        HelperFinance_FLMCheck itemCheck = new HelperFinance_FLMCheck();
                        itemCheck.Price = itemScheduleFeeAssistant.Price.HasValue ? itemScheduleFeeAssistant.Price.Value : 0;
                        itemCheck.TotalDay = schedule.TotalDays;
                        itemCheck.TotalDayActual = (schedule.DateTo.Date - schedule.DateFrom.Date).TotalDays + 1;
                        if (lstOPSGroupProductDriver.Count() > 0)
                        {
                            itemCheck.TotalTon = lstOPSGroupProductDriver.Sum(c => c.TonTranfer);
                            itemCheck.TotalCBM = lstOPSGroupProductDriver.Sum(c => c.CBMTranfer);
                            itemCheck.TotalSchedule = lstOPSGroupProductDriver.Select(c => c.ID).Distinct().Count();
                            itemCheck.TotalDaySchedule = lstOPSGroupProductDriver.Select(c => c.DateConfig.Date).Distinct().Count();
                        }
                        if (lstOPSContainerDriver.Count() > 0)
                        {
                            itemCheck.TotalPacking = lstOPSContainerDriver.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Distinct().Count();
                            itemCheck.TotalSchedule = lstOPSContainerDriver.Select(c => c.ID).Distinct().Count();
                            itemCheck.TotalDaySchedule = lstOPSContainerDriver.Select(c => c.DateConfig.Date).Distinct().Count();
                        }

                        // Param bổ sung
                        var scheduleDriver = model.FLM_ScheduleDriver.FirstOrDefault(c => c.ScheduleID == schedule.ID && c.DriverID == itemDriverID);
                        if (scheduleDriver != null)
                        {
                            itemCheck.FeeBase = scheduleDriver.FeeBase;
                            itemCheck.TotalDayOffDriver = scheduleDriver.DaysWorkInOff.HasValue ? scheduleDriver.DaysWorkInOff.Value : 0;
                            itemCheck.TotalDayOnDriver = scheduleDriver.DaysWorkInOn.HasValue ? scheduleDriver.DaysWorkInOn.Value : 0;
                            itemCheck.TotalDayHoliday = scheduleDriver.DaysWorkInHoliday.HasValue ? scheduleDriver.DaysWorkInHoliday.Value : 0;
                            itemCheck.TotalDayAllowOffDriver = scheduleDriver.DaysAllowOff;
                            itemCheck.TotalDayAllowOffRemainDriver = scheduleDriver.DaysAllowOffRemain.HasValue ? scheduleDriver.DaysAllowOffRemain.Value : 0;
                            itemCheck.IsAssistant = scheduleDriver.IsAssistant;
                        }

                        // Param ngày tự định nghĩa
                        itemCheck.lstCheckDetail = new List<HelperFinance_FLMCheckDetail>();
                        var lstGroupCheckDetail = lstScheduleFeeDriverOther.Where(c => c.DriverID == itemDriverID).GroupBy(c => new { c.TypeOfScheduleFeeID, c.TypeOfScheduleFeeCode }).ToList();
                        foreach (var itemGroupCheckDetail in lstGroupCheckDetail)
                        {
                            HelperFinance_FLMCheckDetail itemCheckDetail = new HelperFinance_FLMCheckDetail();
                            itemCheckDetail.TypeOfScheduleFeeCode = itemGroupCheckDetail.Key.TypeOfScheduleFeeCode;
                            itemCheckDetail.TypeOfScheduleFeeDay = itemGroupCheckDetail.Sum(c => c.TypeOfScheduleFeeDay);
                            itemCheck.lstCheckDetail.Add(itemCheckDetail);
                        }

                        #region Phân bổ cho từng ngày của schedule
                        if (!string.IsNullOrEmpty(itemScheduleFeeAssistant.ExprPriceDay))
                        {
                            DateTime DateFrom = schedule.DateFrom.Date;
                            while (DateFrom <= schedule.DateTo.Date)
                            {
                                bool flag = false;
                                itemCheck.IsWorking = lstScheduleDateDriver.Any(c => c.DriverID == itemDriverID && c.FLM_ScheduleDate.Date == DateFrom);
                                itemCheck.IsDayOn = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOn);
                                itemCheck.IsDayOff = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateOff);
                                itemCheck.IsDayHoliday = lstScheduleDate.Any(c => c.Date == DateFrom && c.TypeScheduleDateID == -(int)SYSVarType.TypeScheduleDateHoliday);

                                try
                                {
                                    flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeAssistant.ExprInputDay);
                                }
                                catch { }
                                if (flag)
                                {
                                    itemCheck.TotalTonInDay = 0;
                                    itemCheck.TotalCBMInDay = 0;
                                    itemCheck.TotalScheduleInDay = 0;
                                    itemCheck.TotalPackingInDay = 0;

                                    var lstGroupInDay = lstOPSGroupProductDriver.Where(c => c.DateConfig == DateFrom);
                                    if (lstGroupInDay != null && lstGroupInDay.Count() > 0)
                                    {
                                        itemCheck.TotalTonInDay = lstGroupInDay.Sum(c => c.TonTranfer);
                                        itemCheck.TotalCBMInDay = lstGroupInDay.Sum(c => c.CBMTranfer);
                                        itemCheck.TotalScheduleInDay = lstGroupInDay.Select(c => c.ID).Distinct().Count();
                                    }
                                    var lstContainerInDay = lstOPSContainerDriver.Where(c => c.DateConfig == DateFrom);
                                    if (lstContainerInDay != null && lstContainerInDay.Count() > 0)
                                    {
                                        itemCheck.TotalPackingInDay = lstContainerInDay.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Distinct().Count();
                                        itemCheck.TotalScheduleInDay = lstContainerInDay.Select(c => c.ID).Distinct().Count();
                                    }

                                    decimal? price = null;
                                    try
                                    {
                                        price = Expression_FLMGetValue(itemCheck, itemScheduleFeeAssistant.ExprPriceDay);
                                    }
                                    catch
                                    {
                                        System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeAssistant.ExprPriceDay);
                                    }

                                    // Tạo pl
                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateFrom.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.VendorID = Account.SYSCustomerID;
                                    pl.DriverID = itemDriverID;
                                    pl.ScheduleID = schedule.ID;
                                    pl.VehicleID = scheduleDriver.AssetID > 0 ? scheduleDriver.FLM_Asset.VehicleID : null;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                    lstPL.Add(pl);

                                    // Detail
                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.FLMScheduleFeeNoGroup;
                                    plCost.Debit = price.HasValue ? price.Value : 0;
                                    plCost.TypeOfPriceDIExCode = itemScheduleFeeAssistant.TypeOfScheduleFeeCode;
                                    plCost.Note = itemScheduleFeeAssistant.TypeOfScheduleFeeName;
                                    pl.Debit = plCost.Debit;
                                    pl.FIN_PLDetails.Add(plCost);

                                }

                                DateFrom = DateFrom.AddDays(1);
                            }
                        }
                        #endregion

                        #region Phân bổ cho từng chuyến
                        if (!string.IsNullOrEmpty(itemScheduleFeeAssistant.ExprPriceTOMaster))
                        {
                            var lstMasterID = lstOPSGroupProductDriver.Select(c => c.ID).Distinct().ToList();
                            foreach (var itemMasterID in lstMasterID)
                            {
                                var lstGroup = lstOPSGroupProductDriver.Where(c => c.ID == itemMasterID).OrderByDescending(c => c.TonTranfer);
                                var master = lstOPSGroupProductDriver.FirstOrDefault(c => c.ID == itemMasterID);
                                itemCheck.TotalTonInSchedule = lstGroup.Sum(c => c.TonTranfer);
                                itemCheck.TotalCBMInSchedule = lstGroup.Sum(c => c.CBMTranfer);
                                itemCheck.SortInDay = master.SortOrder;

                                bool flag = false;

                                try
                                {
                                    flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeAssistant.ExprInputTOMaster);
                                }
                                catch { }

                                if (flag)
                                {
                                    decimal? price = null;
                                    try
                                    {
                                        price = Expression_FLMGetValue(itemCheck, itemScheduleFeeAssistant.ExprPriceTOMaster);
                                    }
                                    catch
                                    {
                                        System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeAssistant.ExprPriceTOMaster);
                                    }

                                    // Tạo pl
                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = lstGroup.FirstOrDefault().DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.VendorID = Account.SYSCustomerID;
                                    pl.DriverID = itemDriverID;
                                    pl.ScheduleID = schedule.ID;
                                    pl.VehicleID = master.VehicleID;
                                    pl.DITOMasterID = master.ID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                    // Detail
                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                    plCost.Debit = price.HasValue ? price.Value : 0;
                                    plCost.TypeOfPriceDIExCode = itemScheduleFeeAssistant.TypeOfScheduleFeeCode;
                                    plCost.Note = itemScheduleFeeAssistant.TypeOfScheduleFeeName;
                                    pl.Debit = plCost.Debit;
                                    pl.FIN_PLDetails.Add(plCost);

                                    FIN_PL plPL = new FIN_PL();
                                    CopyFinPL(pl, plPL);
                                    plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    lstPL.Add(pl);
                                    lstPL.Add(plPL);
                                }
                            }
                        }
                        #endregion

                        #region Phân bổ cho từng chuyến xe Con
                        if (!string.IsNullOrEmpty(itemScheduleFeeAssistant.ExprPriceTOMaster))
                        {
                            var lstMasterID = lstOPSContainerDriver.Select(c => c.ID).Distinct().ToList();
                            foreach (var itemMasterID in lstMasterID)
                            {
                                var lstGroup = lstOPSContainerDriver.Where(c => c.ID == itemMasterID);
                                var master = lstOPSContainerDriver.FirstOrDefault(c => c.ID == itemMasterID);
                                itemCheck.TotalPackingInDay = lstGroup.Where(c => c.PHTPackingID > 0).Select(c => c.PHTPackingID).Distinct().Count();
                                itemCheck.SortInDay = master.SortOrder;

                                bool flag = false;

                                try
                                {
                                    flag = Expression_FLMCheckBool(itemCheck, itemScheduleFeeAssistant.ExprInputTOMaster);
                                }
                                catch { }

                                if (flag)
                                {
                                    decimal? price = null;
                                    try
                                    {
                                        price = Expression_FLMGetValue(itemCheck, itemScheduleFeeAssistant.ExprPriceTOMaster);
                                    }
                                    catch
                                    {
                                        System.Diagnostics.Debug.WriteLine("ScheduleFee - Expr error: " + itemScheduleFeeAssistant.ExprPriceTOMaster);
                                    }

                                    // Tạo pl
                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = lstGroup.FirstOrDefault().DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.VendorID = Account.SYSCustomerID;
                                    pl.DriverID = itemDriverID;
                                    pl.ScheduleID = schedule.ID;
                                    pl.VehicleID = master.VehicleID;
                                    pl.COTOMasterID = master.ID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypeDriver;

                                    // Detail
                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.FLMScheduleFee;
                                    plCost.Debit = price.HasValue ? price.Value : 0;
                                    plCost.TypeOfPriceDIExCode = itemScheduleFeeAssistant.TypeOfScheduleFeeCode;
                                    plCost.Note = itemScheduleFeeAssistant.TypeOfScheduleFeeName;
                                    pl.Debit = plCost.Debit;
                                    pl.FIN_PLDetails.Add(plCost);

                                    FIN_PL plPL = new FIN_PL();
                                    CopyFinPL(pl, plPL);
                                    plPL.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    lstPL.Add(pl);
                                    lstPL.Add(plPL);
                                }
                            }
                        }
                        #endregion
                    }
                }
            }
            #endregion

            foreach (var pl in lstPL)
                model.FIN_PL.Add(pl);
            foreach (var pl in lstFinTemp)
                model.FIN_Temp.Add(pl);
        }
        #endregion

        #region Common
        public static bool Expression_FLMCheckBool(HelperFinance_FLMCheck item, string strExpr)
        {
            bool result = false;
            try
            {
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);

                row++;
                worksheet.Cells[row, col].Value = item.TonTransfer;
                strExp.Replace("[TonTransfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMTransfer;
                strExp.Replace("[CBMTransfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityTransfer;
                strExp.Replace("[QuantityTransfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonReturn;
                strExp.Replace("[TonReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMReturn;
                strExp.Replace("[CBMReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityReturn;
                strExp.Replace("[QuantityReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GetPoint;
                strExp.Replace("[GetPoint]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DropPoint;
                strExp.Replace("[DropPoint]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.StatusOfAssetTimeSheetCode;
                strExp.Replace("[StatusOfAssetTimeSheetCode]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExIsOverNight;
                strExp.Replace("[ExIsOverNight]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExIsOverWeight;
                strExp.Replace("[ExIsOverWeight]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExTotalDayOut;
                strExp.Replace("[ExTotalDayOut]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExTotalJoin;
                strExp.Replace("[ExTotalJoin]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDriverMain;
                strExp.Replace("[TotalDriverMain]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDriverEx;
                strExp.Replace("[TotalDriverEx]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDriverLoad;
                strExp.Replace("[TotalDriverLoad]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalSchedule;
                strExp.Replace("[TotalSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalKM;
                strExp.Replace("[TotalKM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDay;
                strExp.Replace("[TotalDay]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDaySchedule;
                strExp.Replace("[TotalDaySchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayActual;
                strExp.Replace("[TotalDayActual]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOn;
                strExp.Replace("[TotalDayOn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOff;
                strExp.Replace("[TotalDayOff]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayHoliday;
                strExp.Replace("[TotalDayHoliday]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayAllowOffDriver; // Tổng ngày allow off driver
                strExp.Replace("[TotalDayAllowOffDriver]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayAllowOffRemainDriver; // Tổng ngày allow off remain driver
                strExp.Replace("[TotalDayAllowOffRemainDriver]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOffDriver; // Tổng ngày off driver
                strExp.Replace("[TotalDayOffDriver]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOnDriver; // Tổng ngày on driver
                strExp.Replace("[TotalDayOnDriver]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayHolidayDriver; // Tổng ngày holiday driver
                strExp.Replace("[TotalDayHolidayDriver]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.FeeBase;
                strExp.Replace("[FeeBase]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Value;
                strExp.Replace("[Value]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Price;
                strExp.Replace("[Price]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTonInDay;
                strExp.Replace("[TotalTonInDay]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalCBMInDay;
                strExp.Replace("[TotalCBMInDay]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalScheduleInDay;
                strExp.Replace("[TotalScheduleInDay]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTonInSchedule;
                strExp.Replace("[TotalTonInSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalCBMInSchedule;
                strExp.Replace("[TotalCBMInSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPacking;
                strExp.Replace("[TotalPacking]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsAssistant;
                strExp.Replace("[IsAssistant]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDayOn;
                strExp.Replace("[IsDayOn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDayOff;
                strExp.Replace("[IsDayOff]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDayHoliday;
                strExp.Replace("[IsDayHoliday]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsWorking;
                strExp.Replace("[IsWorking]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDaySchedule;
                strExp.Replace("[IsDaySchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.SortInDay;
                strExp.Replace("[SortInDay]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PHTLoading;
                strExp.Replace("[PHTLoading]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PackingCode;
                strExp.Replace("[PackingCode]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.KM;
                strExp.Replace("[KM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Ton;
                strExp.Replace("[Ton]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Quota;
                strExp.Replace("[Quota]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con20DCLaden;
                strExp.Replace("[Con20DCLaden]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40DCLaden;
                strExp.Replace("[Con40DCLaden]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40HCLaden;
                strExp.Replace("[Con40HCLaden]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con20DCEmpty;
                strExp.Replace("[Con20DCEmpty]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40DCEmpty;
                strExp.Replace("[Con40DCEmpty]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40HCEmpty;
                strExp.Replace("[Con40HCEmpty]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTon;
                strExp.Replace("[TotalTon]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPrice;
                strExp.Replace("[TotalPrice]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTOMaster;
                strExp.Replace("[TotalTOMaster]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();

                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                if (val == "True") result = true;
                else if (val == "False") result = false;

                return result;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("Thiết lập sai công thức");
                return result;
            }
        }

        private static ExcelPackage Expression_FLMGetPackage(string strExpr)
        {
            try
            {
                ExcelPackage result = new ExcelPackage();

                ExcelWorksheet worksheet = result.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);

                row++;
                strExp.Replace("[TonTransfer]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[CBMTransfer]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[QuantityTransfer]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TonReturn]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[CBMReturn]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[QuantityReturn]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[GetPoint]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DropPoint]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[StatusOfAssetTimeSheetCode]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[ExIsOverNight]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[ExIsOverWeight]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[ExTotalDayOut]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[ExTotalJoin]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDriverMain]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDriverEx]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDriverLoad]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalSchedule]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalKM]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDay]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDaySchedule]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDayActual]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDayOn]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDayOff]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDayHoliday]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDayAllowOffDriver]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDayAllowOffRemainDriver]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDayOffDriver]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDayOnDriver]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalDayHolidayDriver]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[FeeBase]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Value]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Price]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalTonInDay]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalCBMInDay]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalScheduleInDay]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalTonInSchedule]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalCBMInSchedule]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalPacking]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[IsAssistant]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[IsDayOn]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[IsDayOff]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[IsDayHoliday]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[IsWorking]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[IsDaySchedule]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[SortInDay]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[PHTLoading]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[PackingCode]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[KM]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Ton]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Quota]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Con20DCLaden]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Con40DCLaden]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Con40HCLaden]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Con20DCEmpty]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Con40DCEmpty]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Con40HCEmpty]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalTon]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalPrice]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalTOMaster]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static decimal? Expression_FLMGetValue(ExcelPackage package, HelperFinance_FLMCheck item)
        {
            decimal? result = null;
            try
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet 1"];
                int row = 0, col = 1;
                string strCol = "A", strRow = "";

                row++;

                worksheet.Cells[row, col].Value = item.TonTransfer;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMTransfer;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityTransfer;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GetPoint;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DropPoint;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.StatusOfAssetTimeSheetCode;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExIsOverNight;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExIsOverWeight;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExTotalDayOut;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExTotalJoin;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDriverMain;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDriverEx;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDriverLoad;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalSchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalKM;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDay;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDaySchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayActual;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOff;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayHoliday;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayAllowOffDriver; // Tổng ngày allow off driver
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayAllowOffRemainDriver; // Tổng ngày allow off remain driver
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOffDriver; // Tổng ngày off driver
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOnDriver; // Tổng ngày on driver
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayHolidayDriver; // Tổng ngày holiday driver
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.FeeBase;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Value;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Price;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTonInDay;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalCBMInDay;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalScheduleInDay;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTonInSchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalCBMInSchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPacking;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsAssistant;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDayOn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDayOff;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDayHoliday;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsWorking;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDaySchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.SortInDay;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PHTLoading;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PackingCode;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.KM;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Ton;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Quota;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con20DCLaden;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40DCLaden;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40HCLaden;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con20DCEmpty;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40DCEmpty;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40HCEmpty;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTon;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPrice;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTOMaster;
                strRow = strCol + row; row++;

                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                {
                    result = Convert.ToDecimal(val);
                }
                catch
                {
                    return null;
                }

                return result;
            }
            catch
            {
                return null;
            }
        }

        private static decimal? Expression_FLMGetValue(HelperFinance_FLMCheck item, string strExpr)
        {
            decimal? result = null;
            try
            {
                ExcelPackage excel = new ExcelPackage();

                ExcelWorksheet worksheet = excel.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);

                row++;
                worksheet.Cells[row, col].Value = item.TonTransfer;
                strExp.Replace("[TonTransfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMTransfer;
                strExp.Replace("[CBMTransfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityTransfer;
                strExp.Replace("[QuantityTransfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonReturn;
                strExp.Replace("[TonReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMReturn;
                strExp.Replace("[CBMReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityReturn;
                strExp.Replace("[QuantityReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GetPoint;
                strExp.Replace("[GetPoint]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DropPoint;
                strExp.Replace("[DropPoint]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.StatusOfAssetTimeSheetCode;
                strExp.Replace("[StatusOfAssetTimeSheetCode]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExIsOverNight;
                strExp.Replace("[ExIsOverNight]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExIsOverWeight;
                strExp.Replace("[ExIsOverWeight]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExTotalDayOut;
                strExp.Replace("[ExTotalDayOut]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.ExTotalJoin;
                strExp.Replace("[ExTotalJoin]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDriverMain;
                strExp.Replace("[TotalDriverMain]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDriverEx;
                strExp.Replace("[TotalDriverEx]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDriverLoad;
                strExp.Replace("[TotalDriverLoad]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalSchedule;
                strExp.Replace("[TotalSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalKM;
                strExp.Replace("[TotalKM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDay; // Tổng ngày của schedule
                strExp.Replace("[TotalDay]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDaySchedule; // Tổng ngày chạy
                strExp.Replace("[TotalDaySchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayActual; // Tổng ngày theo datefrom dateto schedule
                strExp.Replace("[TotalDayActual]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOn; // Tổng ngày on
                strExp.Replace("[TotalDayOn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOff; // Tổng ngày off
                strExp.Replace("[TotalDayOff]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayHoliday; // Tổng ngày holiday
                strExp.Replace("[TotalDayHoliday]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayAllowOffDriver; // Tổng ngày allow off driver
                strExp.Replace("[TotalDayAllowOffDriver]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayAllowOffRemainDriver; // Tổng ngày allow off remain driver
                strExp.Replace("[TotalDayAllowOffRemainDriver]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOffDriver; // Tổng ngày off driver
                strExp.Replace("[TotalDayOffDriver]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayOnDriver; // Tổng ngày on driver
                strExp.Replace("[TotalDayOnDriver]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalDayHolidayDriver; // Tổng ngày holiday driver
                strExp.Replace("[TotalDayHolidayDriver]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.FeeBase;
                strExp.Replace("[FeeBase]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Value;
                strExp.Replace("[Value]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Price;
                strExp.Replace("[Price]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTonInDay;
                strExp.Replace("[TotalTonInDay]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalCBMInDay;
                strExp.Replace("[TotalCBMInDay]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalScheduleInDay;
                strExp.Replace("[TotalScheduleInDay]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTonInSchedule;
                strExp.Replace("[TotalTonInSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalCBMInSchedule;
                strExp.Replace("[TotalCBMInSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPacking;
                strExp.Replace("[TotalPacking]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsAssistant;
                strExp.Replace("[IsAssistant]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDayOn;
                strExp.Replace("[IsDayOn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDayOff;
                strExp.Replace("[IsDayOff]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDayHoliday;
                strExp.Replace("[IsDayHoliday]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsWorking;
                strExp.Replace("[IsWorking]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsDaySchedule;
                strExp.Replace("[IsDaySchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.SortInDay;
                strExp.Replace("[SortInDay]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PHTLoading;
                strExp.Replace("[PHTLoading]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PackingCode;
                strExp.Replace("[PackingCode]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.KM;
                strExp.Replace("[KM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Ton;
                strExp.Replace("[Ton]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Quota;
                strExp.Replace("[Quota]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con20DCLaden;
                strExp.Replace("[Con20DCLaden]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40DCLaden;
                strExp.Replace("[Con40DCLaden]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40HCLaden;
                strExp.Replace("[Con40HCLaden]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con20DCEmpty;
                strExp.Replace("[Con20DCEmpty]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40DCEmpty;
                strExp.Replace("[Con40DCEmpty]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Con40HCEmpty;
                strExp.Replace("[Con40HCEmpty]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTon;
                strExp.Replace("[TotalTon]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPrice;
                strExp.Replace("[TotalPrice]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalTOMaster;
                strExp.Replace("[TotalTOMaster]", strCol + row);
                strRow = strCol + row; row++;

                if (item.lstCheckDetail != null && item.lstCheckDetail.Count > 0)
                {
                    foreach (var itemCheckDetail in item.lstCheckDetail)
                    {
                        worksheet.Cells[row, col].Value = itemCheckDetail.TypeOfScheduleFeeDay;
                        strExp.Replace("[" + itemCheckDetail.TypeOfScheduleFeeCode + "]", strCol + row);
                        strRow = strCol + row; row++;
                    }
                }

                worksheet.Cells[row, col].Formula = strExp.ToString();

                excel.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                {
                    result = Convert.ToDecimal(val);
                }
                catch
                {
                    return null;
                }

                return result;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
