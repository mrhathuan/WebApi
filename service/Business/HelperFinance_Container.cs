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
        /// <summary>
        /// Tính + lưu doanh thu, chi phí kế hoạch cho lệnh
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Account"></param>
        /// <param name="id"></param>
        public static void COTOMaster_Planning(DataEntities model, AccountItem Account, int id, int? vendorid)
        {
            #region Lệnh Container
            var master = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == id);
            if (master != null)
            {
            }
            #endregion
        }

        /// <summary>
        /// Tính + lưu doanh thu, chi phí thực tế theo lệnh (sau khi hoàn tất chuyến)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Account"></param>
        /// <param name="id"></param>
        public static void COTOMaster_POD(DataEntities model, AccountItem Account, int id)
        {
        }

        public static DTOOPTPriceContainer Container_OptimizePrice(int optimizeID)
        {
            var result = new DTOOPTPriceContainer();
            result.ListOrderContainer = new List<DTOOPTPriceOrderContainer>();
            result.ListOwner = new List<DTOOPTPriceOwnerContainer>();
            result.ListVendor = new List<DTOOPTPriceVendorContainer>();
            return result;
        }

        #region Container
        public static void Container_TenderedSchedule(DataEntities model, AccountItem Account, int tomasterid)
        {
            var master = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == tomasterid);
            if (master != null && (master.VendorOfVehicleID == null || master.VendorOfVehicleID == Account.SYSCustomerID))
            {

            }
        }

        public static void Container_CompleteSchedule(DataEntities model, AccountItem Account, int comasterid, int? cotocontainerid)
        {
            if (cotocontainerid > 0)
            {
                var container = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == cotocontainerid);
                if (container != null && container.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerComplete)
                {
                    // Thay đổi trạng thái lệnh
                    container.ModifiedBy = Account.UserName;
                    container.ModifiedDate = DateTime.Now;
                    container.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                    model.SaveChanges();

                    if (model.OPS_COTOContainer.Count(c => c.COTOMasterID == container.COTOMasterID && c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete) == model.OPS_COTOContainer.Count(c => c.COTOMasterID == container.COTOMasterID))
                        comasterid = container.COTOMasterID.Value;
                }
            }

            // Cập nhật ATA, ATD cho chuyến
            var master = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == comasterid);
            if (master != null)
            {
                if (master.ATD == null && master.ETD != null)
                    master.ATD = master.ETD;

                var location = model.OPS_COTOLocation.Where(c => c.COTOMasterID == master.ID && c.DateCome != null && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet).OrderBy(c => c.DateCome).Select(c => new { c.DateCome }).FirstOrDefault();
                if (location != null)
                    master.ATD = location.DateCome;

                location = model.OPS_COTOLocation.Where(c => c.COTOMasterID == master.ID && c.DateCome != null && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery).OrderByDescending(c => c.DateCome).Select(c => new { c.DateCome }).FirstOrDefault();
                if (location != null)
                    master.ATA = location.DateCome;

                if (master.ATA == null)
                    master.ATA = DateTime.Now;

                master.DateReceived = DateTime.Now;
            }
            model.SaveChanges();

            // Cập nhật ngày tính giá cho đơn hàng
            var lstOrderID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == comasterid).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
            foreach (var orderID in lstOrderID)
            {
                var order = model.ORD_Order.FirstOrDefault(c => c.ID == orderID);
                if (order != null)
                {
                    var contract = model.CAT_Contract.FirstOrDefault(c => c.ID == order.ContractID);
                    // Mặc định theo ngày gửi yêu cầu
                    order.DateConfig = order.RequestDate.Date;
                    // Danh sách chuyến chạy đơn hàng này
                    var lstMasterDate = model.OPS_COTOContainer.Where(c => c.COTOMasterID.HasValue && c.OPS_Container.ORD_Container.OrderID == order.ID && (c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived || c.COTOMasterID == comasterid)).Select(c => new { c.OPS_COTOMaster.ETD, c.OPS_COTOMaster.ETA, c.OPS_COTOMaster.ATA, c.OPS_COTOMaster.ATD, c.OPS_COTOMaster.DateReceived }).Distinct().ToList();
                    if (lstMasterDate != null && lstMasterDate.Count > 0)
                    {
                        // Tính theo ngày đến kho sớm nhất
                        if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETD)
                            order.DateConfig = lstMasterDate.OrderBy(c => c.ETD).FirstOrDefault().ETD.Date;
                        // Tính theo ngày giao hàng sớm nhất
                        if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATA)
                        {
                            var masterCheck = lstMasterDate.Where(c => c.ATA.HasValue).OrderBy(c => c.ATA).FirstOrDefault();
                            if (masterCheck != null)
                                order.DateConfig = masterCheck.ATA.Value;
                        }
                        // Tính theo ngày giao hàng sớm nhất
                        if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATD)
                        {
                            var masterCheck = lstMasterDate.Where(c => c.ATD.HasValue).OrderBy(c => c.ATD).FirstOrDefault();
                            if (masterCheck != null)
                                order.DateConfig = masterCheck.ATD.Value;
                        }
                        // Tính theo ngày giao hàng sớm nhất
                        if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateComplete)
                        {
                            var masterCheck = lstMasterDate.Where(c => c.DateReceived.HasValue).OrderBy(c => c.DateReceived).FirstOrDefault();
                            if (masterCheck != null)
                                order.DateConfig = masterCheck.DateReceived.Value;
                        }
                    }

                    foreach (var orderGroup in model.ORD_Container.Where(c => c.OrderID == order.ID))
                    {
                        orderGroup.DateConfig = order.DateConfig;
                    }
                }
            }
            model.SaveChanges();

            if (master != null)
            {
                #region Cập nhật thông tin đơn hàng
                var lstGroup = model.OPS_COTOContainer.Where(c => c.COTOMasterID == master.ID).ToList();
                master.TextCustomerCode = string.Empty;
                master.TextCustomerName = string.Empty;
                master.TextGroupLocationCode = string.Empty;

                master.TextCustomerCode = string.Join(", ", lstGroup.Select(c => c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code).Distinct().ToList());
                master.TextCustomerName = string.Join(", ", lstGroup.Select(c => c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.ShortName).Distinct().ToList());
                master.TextGroupLocationCode = string.Join(", ", lstGroup.Where(c => c.OPS_Container.ORD_Container.LocationToID > 0 && c.OPS_Container.ORD_Container.CUS_Location2.CAT_Location.GroupOfLocationID > 0).Select(c => c.OPS_Container.ORD_Container.CUS_Location2.CAT_Location.CAT_GroupOfLocation.Code).Distinct().ToList());
                #endregion

                // Thay đổi trạng thái lệnh
                master.ModifiedBy = Account.UserName;
                master.ModifiedDate = DateTime.Now;
                master.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterReceived;
                //master.TypeOfPaymentDITOMasterID = -(int)SYSVarType.TypeOfPaymentDITOMasterOpen;
                master.DateConfig = master.ETD.Date;
                master.VendorOfVehicleID = master.VendorOfVehicleID.HasValue ? master.VendorOfVehicleID.Value : Account.SYSCustomerID;
                // TimeSheet
                var timeSheet = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ReferID == comasterid && (c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetRunning || c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetAccept || c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetOpen) && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                if (timeSheet != null)
                {
                    timeSheet.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                    timeSheet.ModifiedDate = DateTime.Now;
                    timeSheet.ModifiedBy = Account.UserName;
                    timeSheet.DateFromActual = master.ATD.HasValue ? master.ATD.Value : DateTime.Now;
                    timeSheet.DateToActual = master.ATA.HasValue ? master.ATA.Value : DateTime.Now;
                }
                // Ktra nếu ko có location nào có sort real thì cập nhật sort real = sort order
                if (model.OPS_COTOLocation.Count(c => c.COTOMasterID == master.ID && c.SortOrderReal == null) == 0)
                {
                    foreach (var item in model.OPS_COTOLocation.Where(c => c.COTOMasterID == master.ID))
                        item.SortOrderReal = item.SortOrder;
                }
                model.SaveChanges();


                bool isHome = master.VendorOfVehicleID == Account.SYSCustomerID; // xe nhà
                // Cập nhật status cho group
                foreach (var item in lstGroup)
                {
                    item.ModifiedBy = Account.UserName;
                    item.ModifiedDate = DateTime.Now;
                    item.TypeOfStatusContainerID = -(int)SYSVarType.TypeOfStatusContainerComplete;
                }

                var objContractCUS = model.OPS_COTOContainer.Where(c => c.COTOMasterID == comasterid).Select(c => new { c.OPS_Container.ORD_Container.ORD_Order.ContractID, c.OPS_Container.ORD_Container.ORD_Order.CAT_TransportMode.TransportModeID, c.OPS_COTOMaster.VendorOfVehicleID, c.OPS_Container.ORD_Container.ORD_Order.CustomerID, c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID }).FirstOrDefault();
                var flagNoContract = true;
                var priceid = -1;

                #region Tìm hợp đồng vendor
                if (objContractCUS != null)
                {
                    var lstGroupRouting = model.OPS_COTOContainer.Where(c => c.COTOMasterID == master.ID).Select(c => new HelperFinance_OPSContainer
                    {
                        ID = c.ID,
                        OrderRoutingID = c.OPS_Container.ORD_Container.CUS_Routing.RoutingID,
                        LocationFromID = c.OPS_Container.ORD_Container.LocationFromID.HasValue ? c.OPS_Container.ORD_Container.CUS_Location2.LocationID : -1,
                        LocationToID = c.OPS_Container.ORD_Container.LocationToID.HasValue ? c.OPS_Container.ORD_Container.CUS_Location3.LocationID : -1,
                        LocationDepotID = c.OPS_Container.ORD_Container.LocationDepotID.HasValue ? c.OPS_Container.ORD_Container.CUS_Location.LocationID : -1,
                        LocationDepotReturnID = c.OPS_Container.ORD_Container.LocationDepotReturnID.HasValue ? c.OPS_Container.ORD_Container.CUS_Location1.LocationID : -1,
                        PackingID = c.OPS_Container.ORD_Container.PackingID,
                        ContractTermID = c.ContractTermID,
                        ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID > 0 ? c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID : -1,
                    }).ToList();

                    int vendorID = master.VendorOfVehicleID.HasValue ? master.VendorOfVehicleID.Value : Account.SYSCustomerID;

                    HelperFinance_Contract objContractVEN = new HelperFinance_Contract();
                    var lstContractVEN = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && ((c.CompanyID > 0 && c.CUS_Company.CustomerRelateID == objContractCUS.CustomerID) || c.CompanyID == null) && (c.CustomerID == master.VendorOfVehicleID || (isHome && c.CustomerID == null)) && c.EffectDate <= master.DateConfig && (c.ExpiredDate == null || c.ExpiredDate > master.DateConfig) &&
                        ((vendorID == Account.SYSCustomerID && (c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFCL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLCL)) || (vendorID != Account.SYSCustomerID && c.CAT_TransportMode.TransportModeID == objContractCUS.TransportModeID))).Select(c => new HelperFinance_Contract
                        {
                            ID = c.ID,
                            TransportModeID = c.TransportModeID,
                            TypeOfContractDateID = c.TypeOfContractDateID,
                            EffectDate = c.EffectDate
                        }).OrderByDescending(c => c.EffectDate).ToList();

                    #region Lấy hợp đồng có giá
                    foreach (var itemContractVEN in lstContractVEN.OrderBy(c => c.CompanyID > 0))
                    {
                        objContractVEN = itemContractVEN;
                        bool flagHasPrice = false;
                        // Lấy bảng giá gần nhất
                        var lstPriceID = model.CAT_Price.Where(c => ((c.CAT_ContractTerm.ServiceOfOrderID > 0 && c.CAT_ContractTerm.CAT_ServiceOfOrder.ServiceOfOrderID == objContractCUS.ServiceOfOrderID) || c.CAT_ContractTerm.ServiceOfOrderID == null ? true : false) && c.CAT_ContractTerm.ContractID == itemContractVEN.ID && c.EffectDate <= master.DateConfig && (c.CAT_ContractTerm.DateExpire == null || c.CAT_ContractTerm.DateExpire >= master.DateConfig)).OrderByDescending(c => c.EffectDate).Select(c => c.ID).ToList();
                        if (lstPriceID.Count > 0)
                        {
                            decimal masterPrice = 0;
                            foreach (var item in lstGroup)
                            {
                                var objCon = lstGroupRouting.FirstOrDefault(c => c.ID == item.ID);
                                if (objCon != null)
                                {
                                    List<HelperFinance_OPSVendorPrice> lstPrice = new List<HelperFinance_OPSVendorPrice>();
                                    // Check có giá của cung đường này hay ko
                                    var objPrice = model.CAT_PriceCOContainer.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.PackingID == objCon.PackingID && c.CAT_ContractRouting.RoutingID == objCon.OrderRoutingID && c.Price > 0).OrderByDescending(c => c.Price).Select(c => new HelperFinance_OPSVendorPrice
                                    {
                                        ID = c.ID,
                                        Price = c.Price,
                                        Code = c.CAT_ContractRouting.CAT_Routing.Code,
                                        RoutingName = c.CAT_ContractRouting.CAT_Routing.RoutingName,
                                        ContractTermID = c.CAT_Price.ContractTermID,
                                        RoutingID = c.CAT_ContractRouting.RoutingID
                                    }).FirstOrDefault();
                                    if (objPrice == null)
                                        objPrice = model.CAT_PriceCOContainer.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.PackingID == objCon.PackingID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == objCon.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == objCon.LocationToID && c.Price > 0).OrderByDescending(c => c.Price).Select(c => new HelperFinance_OPSVendorPrice
                                        {
                                            ID = c.ID,
                                            Price = c.Price,
                                            Code = c.CAT_ContractRouting.CAT_Routing.Code,
                                            RoutingName = c.CAT_ContractRouting.CAT_Routing.RoutingName,
                                            ContractTermID = c.CAT_Price.ContractTermID,
                                            RoutingID = c.CAT_ContractRouting.RoutingID
                                        }).FirstOrDefault();
                                    if (objPrice == null)
                                    {
                                        lstPrice = model.CAT_PriceCOContainer.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.PackingID == objCon.PackingID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID > 0 && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID > 0).Select(c => new HelperFinance_OPSVendorPrice
                                        {
                                            ID = c.ID,
                                            Price = c.Price,
                                            Code = c.CAT_ContractRouting.CAT_Routing.Code,
                                            RoutingName = c.CAT_ContractRouting.CAT_Routing.RoutingName,
                                            ContractTermID = c.CAT_Price.ContractTermID,
                                            RoutingID = c.CAT_ContractRouting.RoutingID,
                                            ListFrom = c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Select(d => d.LocationID).ToList(),
                                            ListTo = c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Select(d => d.LocationID).ToList(),
                                        }).ToList();
                                    }

                                    if (objCon.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport)
                                    {
                                        if (objPrice == null)
                                            objPrice = lstPrice.Where(c => c.ListFrom.Any(d => d == objCon.LocationFromID) && c.ListFrom.Any(d => d == objCon.LocationDepotID) && c.ListTo.Any(d => d == objCon.LocationToID) && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                                    }
                                    if (objCon.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)
                                    {
                                        if (objPrice == null)
                                            objPrice = lstPrice.Where(c => c.ListFrom.Any(d => d == objCon.LocationFromID) && c.ListTo.Any(d => d == objCon.LocationToID) && c.ListTo.Any(d => d == objCon.LocationDepotReturnID) && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                                    }
                                    if (objCon.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal)
                                    {
                                        if (objPrice == null)
                                            objPrice = lstPrice.Where(c => c.ListFrom.Any(d => d == objCon.LocationFromID) && c.ListFrom.Any(d => d == objCon.LocationDepotID) && c.ListTo.Any(d => d == objCon.LocationToID) && c.ListTo.Any(d => d == objCon.LocationDepotReturnID) && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                                    }
                                    if (objPrice == null)
                                        objPrice = lstPrice.Where(c => c.ListFrom.Any(d => d == objCon.LocationFromID || d == objCon.LocationDepotID) && c.ListTo.Any(d => d == objCon.LocationToID || d == objCon.LocationDepotReturnID) && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                                    if (objPrice != null)
                                    {
                                        item.CATRoutingID = objPrice.RoutingID;
                                        item.ContractTermID = objPrice.ContractTermID;
                                        if (masterPrice <= objPrice.Price)
                                        {
                                            master.RoutingID = objPrice.RoutingID;
                                            masterPrice = objPrice.Price;
                                        }
                                        flagHasPrice = true;
                                    }
                                    else
                                    {
                                        item.CATRoutingID = null;
                                        item.ContractTermID = null;
                                    }

                                }
                                if (master.RoutingID == null)
                                    master.RoutingID = objCon.OrderRoutingID;
                            }
                        }

                        if (lstPriceID.Count == 0 && (itemContractVEN.CustomerID == Account.SYSCustomerID || itemContractVEN.CustomerID == null))
                        {
                            foreach (var item in lstGroup)
                            {
                                var opsGroup = lstGroupRouting.FirstOrDefault(c => c.ID == item.ID && c.CATRoutingID > 0);
                                if (opsGroup != null)
                                {
                                    item.CATRoutingID = opsGroup.CATRoutingID;
                                    item.ContractTermID = opsGroup.ContractTermID;
                                }
                            }
                        }

                        // Nếu hợp đồng này có giá thì ko cần xét hợp đồng sau
                        if (flagHasPrice)
                            break;
                    }
                    #endregion

                    if (objContractVEN != null && objContractVEN.ID > 0)
                    {
                        master.ContractID = objContractVEN.ID;
                        master.TransportModeID = objContractVEN.TransportModeID;
                        var objPrice = model.CAT_Price.Where(c => c.CAT_ContractTerm.ContractID == objContractVEN.ID && c.EffectDate <= master.DateConfig).Select(c => new { c.ID }).FirstOrDefault();
                        if (objPrice != null)
                            priceid = objPrice.ID;

                        flagNoContract = false;

                        // Cập nhật lại ngày tính giá
                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETD)
                            master.DateConfig = master.ETD.Date;

                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATA && master.ATA.HasValue)
                            master.DateConfig = master.ATA.Value.Date;

                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATD && master.ATD.HasValue)
                            master.DateConfig = master.ATD.Value.Date;

                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateComplete && master.DateReceived.HasValue)
                            master.DateConfig = master.DateReceived.Value.Date;

                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateRequest)
                        {
                            var requestDate = model.OPS_COTOContainer.Where(c => c.COTOMasterID == comasterid).OrderBy(c => c.OPS_Container.ORD_Container.ORD_Order.RequestDate).Select(c => c.OPS_Container.ORD_Container.ORD_Order.RequestDate).FirstOrDefault();
                            if (requestDate != null)
                                master.DateConfig = requestDate.Date;
                        }
                    }
                }
                #endregion

                if (flagNoContract == true)
                {
                    master.ContractID = null;
                    master.RoutingID = null;
                    foreach (var item in lstGroup)
                    {
                        item.CATRoutingID = null;
                        item.ContractTermID = null;
                    }
                }
                model.SaveChanges();

                //// Cập nhật KPI
                //HelperKPI.KPITime_DIMonitorComplete(model, Account, master.ID);
                //model.SaveChanges();
                // Cập nhật status đơn hàng liên quan
                if (lstOrderID.Count > 0)
                    HelperStatus.ORDOrder_Status(model, Account, lstOrderID);
            }
        }

        public static void Container_TimeChange(DataEntities model, AccountItem Account, int comasterid)
        {
            // Cập nhật ATA, ATD cho chuyến
            var master = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == comasterid);
            if (master != null)
            {
                if (master.ATD == null)
                {
                    var location = model.OPS_COTOLocation.Where(c => c.COTOMasterID == master.ID && c.DateCome != null && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet).OrderBy(c => c.DateCome).Select(c => new { c.DateCome }).FirstOrDefault();
                    if (location != null)
                        master.ATD = location.DateCome;
                    else
                        master.ATD = master.ETD;
                }

                if (master.ATA == null || master.ATA != null)
                {
                    var location = model.OPS_COTOLocation.Where(c => c.COTOMasterID == master.ID && c.DateCome != null && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery).OrderByDescending(c => c.DateCome).Select(c => new { c.DateCome }).FirstOrDefault();
                    if (master.ATA != null)
                    {
                        if (location != null && location.DateCome > master.ATA)
                            master.ATA = location.DateCome;
                    }
                    else
                        master.ATA = location != null ? location.DateCome : DateTime.Now;
                }

                master.DateReceived = master.ATA;

                if (master.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterReceived)
                {
                    // TimeSheet
                    var timeSheet = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ReferID == comasterid && (c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetRunning || c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetAccept || c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetOpen) && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetCOTOMaster);
                    if (timeSheet != null)
                    {
                        timeSheet.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                        timeSheet.ModifiedDate = DateTime.Now;
                        timeSheet.ModifiedBy = Account.UserName;
                        timeSheet.DateToActual = DateTime.Now;
                    }
                }

                // Ktra nếu ko có location nào có sort real thì cập nhật sort real = sort order
                if (model.OPS_COTOLocation.Count(c => c.COTOMasterID == master.ID && c.SortOrderReal == null) == 0)
                {
                    foreach (var item in model.OPS_COTOLocation.Where(c => c.COTOMasterID == master.ID))
                        item.SortOrderReal = item.SortOrder;
                }
                model.SaveChanges();
            }
            model.SaveChanges();
        }

        public static void Container_CalculateOrder(DataEntities model, AccountItem Account, DateTime DateConfig, List<int> lstCustomerID)
        {
            System.Diagnostics.Debug.WriteLine("Container_CalculateOrder: " + DateConfig.ToString("dd/MM/yyyy"));

            DateConfig = DateConfig.Date;
            var DateConfigEnd = DateConfig.Date.AddDays(1);
            // Lấy dữ liệu input
            var ItemInput = COPrice_GetInput(model, DateConfig, Account, lstCustomerID, true);

            #region Xóa PL cũ
            foreach (var pl in model.FIN_PL.Where(c => DbFunctions.TruncateTime(c.Effdate) == DateConfig && c.SYSCustomerID == Account.SYSCustomerID && !c.IsPlanning && c.ContractID.HasValue && ItemInput.ListContractID.Contains(c.ContractID.Value)))
            {
                foreach (var plDetail in model.FIN_PLDetails.Where(c => c.PLID == pl.ID))
                {
                    foreach (var plGroup in model.FIN_PLContainer.Where(c => c.PLDetailID == plDetail.ID))
                        model.FIN_PLContainer.Remove(plGroup);
                    model.FIN_PLDetails.Remove(plDetail);
                }
                model.FIN_PL.Remove(pl);
            }
            model.SaveChanges();
            #endregion

            List<FIN_PL> lstPl = new List<FIN_PL>();
            List<FIN_Temp> lstPlTemp = new List<FIN_Temp>();
            List<HelperFinance_OPSContainer> lstOrderContainerUpDate = new List<HelperFinance_OPSContainer>();

            if (ItemInput.ListContract.Count > 0 && ItemInput.ListOrder.Count > 0)
            {
                #region Chạy từng hợp đồng
                foreach (var itemContract in ItemInput.ListContract)
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
                        foreach (var item in ItemInput.ListServiceOfOrder)
                        {
                            DTOCATContract_Setting objConSet = new DTOCATContract_Setting();
                            objConSet.GetEmpty = 0;
                            objConSet.Laden = 100;
                            objConSet.ReturnEmpty = 0;
                            objConSet.ServiceOfOrderID = item.ID;
                            itemContract.ListContractSetting.Add(objConSet);
                        }
                    }

                    System.Diagnostics.Debug.WriteLine("Contract start: " + itemContract.ID);
                    var lstPLTempContract = ItemInput.ListFINTemp.Where(c => c.ContractID == itemContract.ID);
                    var objPrice = ItemInput.ListContainerPrice.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                    var queryOrderContract = ItemInput.ListOrder.Where(c => c.ContractID == itemContract.ID);
                    var queryORDContainer = ItemInput.ListOPSContainer.Where(c => c.ContractID == itemContract.ID);
                    var queryOPSContainer = ItemInput.ListContainer.Where(c => c.ContractID == itemContract.ID);
                    // Giá chính
                    if (objPrice != null && queryORDContainer.Count() > 0)
                    {
                        foreach (var itemOPSContainer in queryORDContainer)
                        {
                            var priceCO = ItemInput.ListContainerPrice.Where(c => (itemOPSContainer.ContractTermID == null ? true : c.ContractTermID == itemOPSContainer.ContractTermID) && c.ServiceOfOrderID == itemOPSContainer.ServiceOfOrderID && c.PackingID == itemOPSContainer.PackingID && (c.RoutingID == itemOPSContainer.CATRoutingID || (c.LocationFromID == itemOPSContainer.LocationFromID && c.LocationToID == itemOPSContainer.LocationToID)) && c.Price > 0).OrderByDescending(c => c.EffectDate).OrderByDescending(c => c.Price).FirstOrDefault();
                            if (priceCO == null)
                                priceCO = ItemInput.ListContainerPrice.Where(c => (itemOPSContainer.ContractTermID == null ? true : c.ContractTermID == itemOPSContainer.ContractTermID) && c.ServiceOfOrderID == null && c.PackingID == itemOPSContainer.PackingID && (c.RoutingID == itemOPSContainer.CATRoutingID || (c.LocationFromID == itemOPSContainer.LocationFromID && c.LocationToID == itemOPSContainer.LocationToID)) && c.Price > 0).OrderByDescending(c => c.EffectDate).OrderByDescending(c => c.Price).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                            if (priceCO != null)
                                itemOPSContainer.UnitPrice = priceCO.Price;
                        }
                    }

                    #region Doanh thu chính
                    foreach (var itemContainer in queryORDContainer)
                    {
                        decimal rate = 0;
                        var contractSetting = itemContract.ListContractSetting.FirstOrDefault(c => c.ServiceOfOrderID == itemContainer.ServiceOfOrderID);
                        if (contractSetting != null)
                        {
                            // Tính doanh thu cont theo chặng
                            if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOGetEmpty)
                                rate = (decimal)contractSetting.GetEmpty;
                            if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden)
                                rate = (decimal)contractSetting.Laden;
                            if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty)
                                rate = (decimal)contractSetting.ReturnEmpty;
                        }

                        FIN_PL pl = new FIN_PL();
                        pl.CreatedBy = Account.UserName;
                        pl.CreatedDate = DateTime.Now;
                        pl.Code = string.Empty;
                        pl.IsPlanning = false;
                        pl.SYSCustomerID = Account.SYSCustomerID;
                        pl.Effdate = DateConfig.Date;
                        pl.OrderID = itemContainer.OrderID;
                        pl.COTOMasterID = itemContainer.COTOMasterID;
                        pl.VendorID = itemContainer.VendorID.HasValue ? itemContainer.VendorID : Account.SYSCustomerID;
                        pl.CustomerID = itemContract.CustomerID;
                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                        pl.ContractID = itemContract.ID;
                        lstPl.Add(pl);

                        FIN_PLDetails plDetail = new FIN_PLDetails();
                        plDetail.CreatedBy = Account.UserName;
                        plDetail.CreatedDate = DateTime.Now;
                        plDetail.CostID = (int)CATCostType.COTOFreightCredit;
                        pl.FIN_PLDetails.Add(plDetail);

                        FIN_PLContainer plCon = new FIN_PLContainer();
                        plCon.CreatedBy = Account.UserName;
                        plCon.CreatedDate = DateTime.Now;
                        plCon.COTOContainerID = itemContainer.OPSContainerID;
                        plCon.Unit = itemContainer.PackingName;
                        plCon.UnitPrice = itemContainer.UnitPrice * rate / 100;
                        plCon.Quantity = 1;
                        plDetail.Credit += (decimal)plCon.Quantity * plCon.UnitPrice;
                        plDetail.FIN_PLContainer.Add(plCon);

                        pl.Credit += plDetail.Credit;

                    }
                    #endregion

                    #region Trouble
                    foreach (var itemORDContainerGroup in queryORDContainer.GroupBy(c => new { c.COTOMasterID, c.VendorID }))
                    {
                        var lstCreditTrouble = ItemInput.ListTrouble.Where(c => c.COTOMasterID == itemORDContainerGroup.Key.COTOMasterID && c.CostOfCustomer != 0 && c.TroubleCostStatusID == -(int)SYSVarType.TroubleCostStatusApproved);
                        if (lstCreditTrouble.Count() > 0)
                        {
                            foreach (var item in lstCreditTrouble)
                            {
                                FIN_PL pl = new FIN_PL();
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.Code = string.Empty;
                                pl.IsPlanning = false;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.Effdate = DateConfig.Date;
                                pl.COTOMasterID = itemORDContainerGroup.Key.COTOMasterID;
                                pl.VendorID = itemORDContainerGroup.Key.VendorID.HasValue ? itemORDContainerGroup.Key.VendorID : Account.SYSCustomerID;
                                pl.CustomerID = itemContract.CustomerID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                pl.ContractID = itemContract.ID;
                                lstPl.Add(pl);

                                FIN_PLDetails plCreditTrouble = new FIN_PLDetails();
                                plCreditTrouble.CreatedBy = Account.UserName;
                                plCreditTrouble.CreatedDate = DateTime.Now;
                                plCreditTrouble.CostID = (int)CATCostType.TroubleCredit;
                                plCreditTrouble.Debit = 0;
                                plCreditTrouble.Credit = item.CostOfCustomer;
                                plCreditTrouble.Note = item.Description;
                                plCreditTrouble.TypeOfPriceDIExCode = item.GroupOfTroubleCode;

                                FIN_PLContainer plCon = new FIN_PLContainer();
                                plCon.CreatedBy = Account.UserName;
                                plCon.CreatedDate = DateTime.Now;
                                plCon.COTOContainerID = itemORDContainerGroup.FirstOrDefault().OPSContainerID;
                                plCon.UnitPrice = 0;
                                plCon.Quantity = 0;
                                pl.OrderID = itemORDContainerGroup.FirstOrDefault().OrderID;
                                plCreditTrouble.FIN_PLContainer.Add(plCon);

                                pl.FIN_PLDetails.Add(plCreditTrouble);
                                pl.Credit += plCreditTrouble.Credit;
                            }
                        }
                    }
                    #endregion

                    #region Tờ khai
                    foreach (var item in ItemInput.ListDocumentService)
                    {
                        var lstContainer = ItemInput.ListDocumentContainer.Where(c => c.DocumentID == item.DocumentID);
                        if (lstContainer.Count() > 0)
                        {
                            // Giá tờ khai chi đều cho các container
                            var price = ItemInput.ListContainerService.Where(c => c.ServiceID == item.ServiceID && c.PackingID == null && c.ContractID == itemContract.ID && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                            if (price != null)
                            {
                                decimal unitPrice = price.Price / lstContainer.Count();
                                foreach (var itemContainer in lstContainer)
                                {
                                    FIN_PL pl = new FIN_PL();
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.Code = string.Empty;
                                    pl.IsPlanning = false;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.Effdate = DateConfig.Date;
                                    pl.OrderID = itemContainer.OrderID;
                                    pl.CustomerID = itemContainer.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                    pl.ContractID = itemContract.ID;
                                    lstPl.Add(pl);

                                    FIN_PLDetails plCredit = new FIN_PLDetails();
                                    plCredit.CreatedBy = Account.UserName;
                                    plCredit.CreatedDate = DateTime.Now;
                                    plCredit.CostID = (int)CATCostType.ORDDocumentCredit;
                                    plCredit.Debit = 0;
                                    plCredit.Credit = unitPrice;
                                    plCredit.TypeOfPriceDIExCode = item.ServiceCode;

                                    FIN_PLContainer plCon = new FIN_PLContainer();
                                    plCon.CreatedBy = Account.UserName;
                                    plCon.CreatedDate = DateTime.Now;
                                    plCon.COTOContainerID = itemContainer.OPSContainerID;
                                    plCredit.FIN_PLContainer.Add(plCon);

                                    pl.FIN_PLDetails.Add(plCredit);
                                }
                            }
                        }
                    }
                    #endregion

                    #region Giá service container
                    foreach (var itemContainer in ItemInput.ListDocumentContainer)
                    {
                        foreach (var itemService in itemContainer.ListService)
                        {
                            var price = ItemInput.ListContainerService.Where(c => c.ServiceID == itemService.ServiceID && c.PackingID == itemContainer.PackingID && c.ContractID == itemContract.ID && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                            if (price != null)
                            {
                                FIN_PL pl = new FIN_PL();
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.Code = string.Empty;
                                pl.IsPlanning = false;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.Effdate = DateConfig.Date;
                                pl.OrderID = itemContainer.OrderID;
                                pl.CustomerID = itemContainer.CustomerID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                pl.ContractID = itemContract.ID;
                                lstPl.Add(pl);

                                FIN_PLDetails plCredit = new FIN_PLDetails();
                                plCredit.CreatedBy = Account.UserName;
                                plCredit.CreatedDate = DateTime.Now;
                                plCredit.CostID = (int)CATCostType.ORDContainerServiceCredit;
                                plCredit.Debit = 0;
                                plCredit.Credit = price.Price;
                                plCredit.TypeOfPriceDIExCode = itemService.ServiceCode;

                                FIN_PLContainer plCon = new FIN_PLContainer();
                                plCon.CreatedBy = Account.UserName;
                                plCon.CreatedDate = DateTime.Now;
                                plCon.COTOContainerID = itemContainer.OPSContainerID;
                                plCredit.FIN_PLContainer.Add(plCon);

                                pl.FIN_PLDetails.Add(plCredit);
                            }
                        }
                    }
                    #endregion

                    #region Phụ thu
                    var lstEx = ItemInput.ListEx.Where(c => c.ContractID == itemContract.ID && !string.IsNullOrEmpty(c.ExprInput) && c.EffectDate <= DateConfig).ToList();
                    var ExEffateDate = lstEx.Select(c => new { c.EffectDate }).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                    if (ExEffateDate != null)
                        lstEx = lstEx.Where(c => c.EffectDate == ExEffateDate.EffectDate).ToList();
                    string strExName = string.Empty;
                    //Chạy từng phụ thu
                    foreach (var itemEx in lstEx)
                    {
                        System.Diagnostics.Debug.WriteLine("Phụ phí: " + itemEx.Note);

                        var lstOrderEx = new List<HelperFinance_Order>();
                        var lstCOTOContainerEx = new List<HelperFinance_OPSContainer>();
                        var lstContainerEx = new List<HelperFinance_OPSContainer>();

                        var queryOrderEx = queryOrderContract.Where(c => itemEx.ServiceOfOrderID == null ? true : c.ServiceOfOrderID == itemEx.ServiceOfOrderID).ToList();
                        var queryCOTOContainerEx = queryORDContainer.Where(c => itemEx.ServiceOfOrderID == null ? true : c.ServiceOfOrderID == itemEx.ServiceOfOrderID).ToList();
                        var queryContainerEx = queryOPSContainer.Where(c => itemEx.ServiceOfOrderID == null ? true : c.ServiceOfOrderID == itemEx.ServiceOfOrderID).ToList();

                        strExName = itemEx.Note;
                        //Danh sách các điều kiện lọc 
                        var lstExParentRouting = itemEx.ListParentRouting;
                        var lstExRouting = itemEx.ListRouting;
                        var lstExGroupLocation = itemEx.ListGroupOfLocation;
                        var lstExPartner = itemEx.ListPartnerID;
                        var lstExLocationFrom = itemEx.ListLocation.Where(c => c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                            .Select(c => c.LocationID).ToList();
                        var lstExLocationTo = itemEx.ListLocation.Where(c => (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                            .Select(c => c.LocationID).ToList();
                        var lstExContainer = itemEx.ListContainer.Select(c => c.PackingID).Distinct().ToList();

                        if (lstExParentRouting.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExParentRouting.Contains(c.ParentRoutingID)).ToList();
                        if (lstExRouting.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExRouting.Contains(c.CATRoutingID)).ToList();
                        if (lstExLocationFrom.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExLocationFrom.Contains(c.LocationFromID)).ToList();
                        if (lstExLocationTo.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExLocationTo.Contains(c.LocationToID)).ToList();
                        if (lstExContainer.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExContainer.Contains(c.PackingID)).ToList();
                        if (lstExPartner.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExPartner.Contains(c.PartnerID)).ToList();


                        var exOrderID = queryOrderEx.Select(c => c.ID).ToArray();
                        var exGroupID = queryCOTOContainerEx.Select(c => c.OrderID).Distinct().ToArray();
                        var lstOrderCheckID = exGroupID.Intersect(exOrderID).ToList();
                        var lstOrderCheck = queryOrderEx.Where(c => lstOrderCheckID.Contains(c.ID)).ToList();
                        var lstCOTOContainerCheck = queryCOTOContainerEx.Where(c => lstOrderCheckID.Contains(c.OrderID)).ToList();
                        var lstOPSContainerID = lstCOTOContainerCheck.Select(c => c.OPSContainerID).Distinct().ToList();
                        var lstContainerCheck = ItemInput.ListContainer.Where(c => lstOPSContainerID.Contains(c.ID)).ToList();

                        // Ktra công thức đầu vào
                        if (lstOrderCheck.Count > 0 && lstCOTOContainerCheck.Count > 0)
                        {
                            #region Theo đơn hàng or chuyến trong ngày
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerInDay || itemEx.COExSumID == -(int)SYSVarType.COExScheduleInDay)
                            {
                                bool flag = true;
                                //Thực hiện công thức
                                DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                itemExpr.TotalSchedule = lstCOTOContainerCheck.Where(c => lstOrderCheckID.Contains(c.OrderID)).Select(c => c.COTOMasterID).Distinct().Count();
                                itemExpr.TotalOrder = lstOrderCheck.Count;
                                itemExpr.TotalPacking = lstCOTOContainerCheck.Select(c => c.OPSContainerID).Distinct().Count();
                                itemExpr.Credit = 0;
                                try
                                {
                                    flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                }
                                catch { flag = false; }
                                if (flag == true)
                                {
                                    lstOrderEx = lstOrderCheck;
                                    lstCOTOContainerEx = lstCOTOContainerCheck;
                                    lstContainerEx = lstContainerCheck;
                                }
                            }
                            #endregion

                            #region Container
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainer)
                            {
                                foreach (var item in lstContainerCheck)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.Credit = 0;
                                    itemExpr.PackingCode = item.PackingName;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        if (lstOrderEx.Count(c => c.ID == item.OrderID) == 0)
                                        {
                                            lstOrderEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == item.OrderID));
                                        }
                                        lstContainerEx.Add(item);
                                        lstCOTOContainerEx.AddRange(lstCOTOContainerCheck.Where(c => c.OPSContainerID == item.OPSContainerID));
                                    }
                                }
                            }
                            #endregion

                            #region Container theo cung đường
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerRoute)
                            {
                                var lstGroup = lstContainerCheck.GroupBy(c => c.OrderRoutingID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = itemGroup.Select(c => c.COTOMasterID).Distinct().Count();
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        foreach (var orderID in itemGroup.Select(c => c.OrderID).Distinct().ToList())
                                        {
                                            if (lstOrderEx.Count(c => c.ID == orderID) == 0)
                                            {
                                                lstOrderEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == orderID));
                                            }
                                        }
                                        lstContainerEx.AddRange(itemGroup.ToArray());
                                        var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                        lstCOTOContainerEx.AddRange(lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)));
                                    }
                                }
                            }
                            #endregion

                            #region Container theo chặng rỗng
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerEmpty)
                            {
                                var lstGroup = lstCOTOContainerCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOEmpty);
                                foreach (var itemGroup in lstGroup)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.PackingCode = itemGroup.PackingName;
                                    itemExpr.Credit = 0;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        if (lstOrderEx.Count(c => c.ID == itemGroup.OrderID) == 0)
                                        {
                                            lstOrderEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.OrderID));
                                        }
                                        lstCOTOContainerEx.Add(itemGroup);
                                        if (lstContainerEx.Count(c => c.OPSContainerID == itemGroup.OPSContainerID) == 0)
                                        {
                                            lstContainerEx.Add(lstContainerCheck.FirstOrDefault(c => c.OPSContainerID == itemGroup.OPSContainerID));
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Container theo chặng đầy
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerLaden)
                            {
                                var lstGroup = lstCOTOContainerCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden);
                                foreach (var itemGroup in lstGroup)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.PackingCode = itemGroup.PackingName;
                                    itemExpr.Credit = 0;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        if (lstOrderEx.Count(c => c.ID == itemGroup.OrderID) == 0)
                                        {
                                            lstOrderEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.OrderID));
                                        }
                                        lstCOTOContainerEx.Add(itemGroup);
                                        if (lstContainerEx.Count(c => c.OPSContainerID == itemGroup.OPSContainerID) == 0)
                                        {
                                            lstContainerEx.Add(lstContainerCheck.FirstOrDefault(c => c.OPSContainerID == itemGroup.OPSContainerID));
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Chuyến
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSchedule)
                            {
                                var lstGroup = lstCOTOContainerCheck.GroupBy(c => c.COTOMasterID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        foreach (var orderID in itemGroup.Select(c => c.OrderID).Distinct().ToList())
                                        {
                                            if (lstOrderEx.Count(c => c.ID == orderID) == 0)
                                            {
                                                lstOrderEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == orderID));
                                            }
                                        }
                                        lstCOTOContainerEx.AddRange(itemGroup.ToArray());
                                        var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                        foreach (var itemContainerID in lstContainerID)
                                        {
                                            if (lstContainerEx.Count(c => c.OPSContainerID == itemContainerID) == 0)
                                            {
                                                lstContainerEx.Add(lstContainerCheck.FirstOrDefault(c => c.OPSContainerID == itemContainerID));
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Chuyến theo cung đường
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerRoute)
                            {
                                var lstGroup = lstCOTOContainerCheck.GroupBy(c => c.OrderRoutingID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = itemGroup.Select(c => c.COTOMasterID).Distinct().Count();
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        foreach (var orderID in itemGroup.Select(c => c.OrderID).Distinct().ToList())
                                        {
                                            if (lstOrderEx.Count(c => c.ID == orderID) == 0)
                                            {
                                                lstOrderEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == orderID));
                                            }
                                        }
                                        lstCOTOContainerEx.AddRange(itemGroup.ToArray());
                                        var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                        foreach (var itemContainerID in lstContainerID)
                                        {
                                            if (lstContainerEx.Count(c => c.OPSContainerID == itemContainerID) == 0)
                                                lstContainerEx.Add(lstContainerCheck.FirstOrDefault(c => c.OPSContainerID == itemContainerID));
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Ghi dữ liệu
                        if (lstOrderEx.Count > 0 && (lstCOTOContainerEx.Count > 0 || lstContainerEx.Count > 0))
                        {
                            #region Theo đơn hàng or chuyến trong ngày
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerInDay || itemEx.COExSumID == -(int)SYSVarType.COExScheduleInDay)
                            {
                                //Thực hiện công thức
                                DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                itemExpr.TotalSchedule = lstCOTOContainerEx.Select(c => c.COTOMasterID).Distinct().Count();
                                itemExpr.TotalOrder = lstOrderEx.Count;
                                itemExpr.TotalPacking = lstCOTOContainerEx.Select(c => c.OPSContainerID).Distinct().Count();
                                itemExpr.Credit = 0;

                                decimal? fixPrice = Expression_COGetValue(Expression_GetPackage(itemEx.ExprPriceFix), itemExpr);

                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.Credit = fixPrice.HasValue ? fixPrice.Value : 0;
                                pl.ContractID = itemContract.ID;
                                pl.CustomerID = itemContract.CustomerID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.COTOExCredit;
                                plCost.Note = itemEx.Note;
                                plCost.Credit = fixPrice.HasValue ? fixPrice.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemEx.TypeOfPriceCOExCode;
                                pl.FIN_PLDetails.Add(plCost);

                                var lstCOTOContaienrID = lstCOTOContainerCheck.OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                COPriceEx_FindOrder(itemEx, pl, plCost, lstPlTemp, lstPLTempContract, lstCOTOContaienrID, itemContract.ID, lstCOTOContainerCheck, lstOrderContainerUpDate);
                                lstPl.Add(pl);
                            }
                            #endregion

                            #region Container
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainer)
                            {
                                foreach (var item in lstContainerEx)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.Credit = 0;
                                    itemExpr.PackingCode = item.PackingName;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => c.OPSContainerID == item.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => c.OPSContainerID == item.OPSContainerID).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity) && c.PackingID == item.PackingID);
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            itemExpr = new DTOPriceCOExExpr();
                                            itemExpr.TotalSchedule = 1;
                                            itemExpr.TotalOrder = 1;
                                            itemExpr.TotalPacking = 1;
                                            itemExpr.Credit = 0;
                                            itemExpr.PackingCode = item.PackingName;
                                            COPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID.FirstOrDefault(), itemContract.ID, lstCOTOContainerCheck.Where(c => c.OPSContainerID == item.OPSContainerID).ToList());
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion

                            #region Container theo cung đường
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerRoute)
                            {
                                var lstGroup = lstContainerEx.GroupBy(c => c.OrderRoutingID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = itemGroup.Select(c => c.COTOMasterID).Distinct().Count();
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            foreach (var itemOPSPriceExContainer in itemGroup.Where(c => c.PackingID == itemPriceExContainer.PackingID))
                                            {
                                                itemExpr = new DTOPriceCOExExpr();
                                                itemExpr.TotalSchedule = 1;
                                                itemExpr.TotalOrder = 1;
                                                itemExpr.TotalPacking = 1;
                                                itemExpr.Credit = 0;
                                                itemExpr.PackingCode = itemOPSPriceExContainer.PackingName;
                                                var lstID = lstCOTOContainerEx.Where(c => c.OPSContainerID == itemOPSPriceExContainer.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                                COPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstID.FirstOrDefault(), itemContract.ID, lstCOTOContainerEx);
                                            }
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion

                            #region Container theo chặng rỗng
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerEmpty)
                            {
                                var lstGroup = lstCOTOContainerCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOEmpty);
                                foreach (var itemGroup in lstGroup)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.PackingCode = itemGroup.PackingName;
                                    itemExpr.Credit = 0;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstContainerID = new List<int> { itemGroup.OPSContainerID };
                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)) && c.PackingID == itemGroup.PackingID);
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            itemExpr = new DTOPriceCOExExpr();
                                            itemExpr.TotalSchedule = 1;
                                            itemExpr.TotalOrder = 1;
                                            itemExpr.TotalPacking = 1;
                                            itemExpr.Credit = 0;
                                            itemExpr.PackingCode = itemGroup.PackingName;
                                            var lstID = lstCOTOContainerEx.Where(c => c.OPSContainerID == itemGroup.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                            COPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstID.FirstOrDefault(), itemContract.ID, lstCOTOContainerEx);
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion

                            #region Container theo chặng đầy
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerLaden)
                            {
                                var lstGroup = lstCOTOContainerCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden);
                                foreach (var itemGroup in lstGroup)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.PackingCode = itemGroup.PackingName;
                                    itemExpr.Credit = 0;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstContainerID = new List<int> { itemGroup.OPSContainerID };
                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)) && c.PackingID == itemGroup.PackingID);
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            itemExpr = new DTOPriceCOExExpr();
                                            itemExpr.TotalSchedule = 1;
                                            itemExpr.TotalOrder = 1;
                                            itemExpr.TotalPacking = 1;
                                            itemExpr.Credit = 0;
                                            itemExpr.PackingCode = itemGroup.PackingName;
                                            var lstID = lstCOTOContainerEx.Where(c => c.OPSContainerID == itemGroup.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                            COPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstID.FirstOrDefault(), itemContract.ID, lstCOTOContainerEx);
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion

                            #region Chuyến
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSchedule)
                            {
                                var lstGroup = lstCOTOContainerCheck.GroupBy(c => c.COTOMasterID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            foreach (var itemOPSPriceExContainer in itemGroup.Where(c => c.PackingID == itemPriceExContainer.PackingID))
                                            {
                                                itemExpr = new DTOPriceCOExExpr();
                                                itemExpr.TotalSchedule = 1;
                                                itemExpr.TotalOrder = 1;
                                                itemExpr.TotalPacking = 1;
                                                itemExpr.Credit = 0;
                                                itemExpr.PackingCode = itemOPSPriceExContainer.PackingName;
                                                var lstID = lstCOTOContainerEx.Where(c => c.OPSContainerID == itemOPSPriceExContainer.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                                COPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstID.FirstOrDefault(), itemContract.ID, lstCOTOContainerEx);
                                            }
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion

                            #region Chuyến theo cung đường
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerRoute)
                            {
                                var lstGroup = lstCOTOContainerCheck.GroupBy(c => c.OrderRoutingID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = itemGroup.Select(c => c.COTOMasterID).Distinct().Count();
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            foreach (var itemOPSPriceExContainer in itemGroup.Where(c => c.PackingID == itemPriceExContainer.PackingID))
                                            {
                                                itemExpr = new DTOPriceCOExExpr();
                                                itemExpr.TotalSchedule = 1;
                                                itemExpr.TotalOrder = 1;
                                                itemExpr.TotalPacking = 1;
                                                itemExpr.Credit = 0;
                                                itemExpr.PackingCode = itemOPSPriceExContainer.PackingName;
                                                var lstID = lstCOTOContainerEx.Where(c => c.OPSContainerID == itemOPSPriceExContainer.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                                COPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstID.FirstOrDefault(), itemContract.ID, lstCOTOContainerEx);
                                            }
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion

                    System.Diagnostics.Debug.WriteLine("Contract end: " + itemContract.ID);
                }

                #endregion

                foreach (var pl in lstPl)
                    model.FIN_PL.Add(pl);

                #region Cập nhật dữ liệu
                //foreach (var itemORDGroup in lstOrderContainerUpDate)
                //{
                //    var group = model.ORD_Container.FirstOrDefault(c => c.ID == itemORDGroup.ID);
                //    if (group != null)
                //        group.FINSort = itemORDGroup.FINSort;
                //}

                //var lstORDOrderID = ItemInput.ListOrder.Select(c => c.ID).Distinct().ToList();
                //foreach (var itemORDOrderID in lstORDOrderID)
                //{
                //    var ordOrder = model.ORD_Order.FirstOrDefault(c => c.ID == itemORDOrderID);
                //    if (ordOrder != null)
                //        ordOrder.TypeOfPaymentORDOrderID = -(int)SYSVarType.TypeOfPaymentORDOrderOpen;
                //}

                //var lstORDOrderGroupID = ItemInput.ListOPSGroupProduct.Select(c => c.OrderGroupProductID).Distinct().ToList();
                //foreach (var itemORDOrderGroupID in lstORDOrderGroupID)
                //{
                //    var ordOrderGroup = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == itemORDOrderGroupID);
                //    if (ordOrderGroup != null)
                //        ordOrderGroup.TypeOfPaymentORDGroupProductID = -(int)SYSVarType.TypeOfPaymentORDGroupProductOpen;
                //}
                #endregion
            }
        }

        public static void Container_CalculateCO(DataEntities model, AccountItem Account, DateTime DateConfig, List<int> lstCustomerID)
        {
            System.Diagnostics.Debug.WriteLine("Container_CalculateCO: " + DateConfig.ToString("dd/MM/yyyy"));
            DateConfig = DateConfig.Date;
            var DateConfigEnd = DateConfig.Date.AddDays(1);
            // Lấy dữ liệu input
            var ItemInput = COPrice_GetInput(model, DateConfig, Account, lstCustomerID, false);

            #region Xóa PL cũ
            foreach (var pl in model.FIN_PL.Where(c => DbFunctions.TruncateTime(c.Effdate) == DateConfig && c.SYSCustomerID == Account.SYSCustomerID && !c.IsPlanning && c.ContractID.HasValue && ItemInput.ListContractID.Contains(c.ContractID.Value)))
            {
                foreach (var plDetail in model.FIN_PLDetails.Where(c => c.PLID == pl.ID))
                {
                    foreach (var plGroup in model.FIN_PLContainer.Where(c => c.PLDetailID == plDetail.ID))
                        model.FIN_PLContainer.Remove(plGroup);
                    model.FIN_PLDetails.Remove(plDetail);
                }
                model.FIN_PL.Remove(pl);
            }
            model.SaveChanges();
            #endregion

            List<FIN_PL> lstPl = new List<FIN_PL>();
            List<FIN_PL> lstPlTrouble = new List<FIN_PL>();
            List<FIN_Temp> lstPlTemp = new List<FIN_Temp>();
            List<HelperFinance_OPSContainer> lstOrderContainerUpDate = new List<HelperFinance_OPSContainer>();

            if (ItemInput.ListContract.Count > 0 && ItemInput.ListCOMaster.Count > 0)
            {
                #region Chạy từng hợp đồng
                foreach (var itemContract in ItemInput.ListContract)
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
                        foreach (var item in ItemInput.ListServiceOfOrder)
                        {
                            DTOCATContract_Setting objConSet = new DTOCATContract_Setting();
                            objConSet.GetEmpty = 0;
                            objConSet.Laden = 100;
                            objConSet.ReturnEmpty = 0;
                            objConSet.ServiceOfOrderID = item.ID;
                            itemContract.ListContractSetting.Add(objConSet);
                        }
                    }

                    System.Diagnostics.Debug.WriteLine("Contract start: " + itemContract.ID);
                    var lstPLTempContract = ItemInput.ListFINTemp.Where(c => c.ContractID == itemContract.ID);
                    var objPrice = ItemInput.ListContainerPrice.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                    var queryOrderContract = ItemInput.ListCOMaster.Where(c => c.ContractID == itemContract.ID);
                    var queryORDContainer = ItemInput.ListOPSContainer.Where(c => c.ContractID == itemContract.ID);
                    var queryOPSContainer = ItemInput.ListContainer.Where(c => c.ContractID == itemContract.ID);

                    // Giá chính
                    if (objPrice != null && queryORDContainer.Count() > 0)
                    {
                        foreach (var itemOPSContainer in queryORDContainer)
                        {
                            var priceCO = ItemInput.ListContainerPrice.Where(c => (itemOPSContainer.ContractTermID == null ? true : c.ContractTermID == itemOPSContainer.ContractTermID) && c.ServiceOfOrderID == itemOPSContainer.ServiceOfOrderID && c.PackingID == itemOPSContainer.PackingID && (c.RoutingID == itemOPSContainer.CATRoutingID || (c.LocationFromID == itemOPSContainer.LocationFromID && c.LocationToID == itemOPSContainer.LocationToID)) && c.Price > 0).OrderByDescending(c => c.EffectDate).OrderByDescending(c => c.Price).FirstOrDefault();
                            if (priceCO == null)
                                priceCO = ItemInput.ListContainerPrice.Where(c => (itemOPSContainer.ContractTermID == null ? true : c.ContractTermID == itemOPSContainer.ContractTermID) && c.ServiceOfOrderID == null && c.PackingID == itemOPSContainer.PackingID && (c.RoutingID == itemOPSContainer.CATRoutingID || (c.LocationFromID == itemOPSContainer.LocationFromID && c.LocationToID == itemOPSContainer.LocationToID)) && c.Price > 0).OrderByDescending(c => c.EffectDate).OrderByDescending(c => c.Price).FirstOrDefault();
                            if (priceCO != null)
                                itemOPSContainer.UnitPrice = priceCO.Price;
                        }
                    }

                    #region Doanh thu chính
                    foreach (var itemContainer in queryORDContainer)
                    {
                        decimal rate = 0;
                        var contractSetting = itemContract.ListContractSetting.FirstOrDefault(c => c.ServiceOfOrderID == itemContainer.ServiceOfOrderID);
                        if (contractSetting != null)
                        {
                            // Tính doanh thu cont theo chặng
                            if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOGetEmpty)
                                rate = (decimal)contractSetting.GetEmpty;
                            if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden)
                                rate = (decimal)contractSetting.Laden;
                            if (itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || itemContainer.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOReturnEmpty)
                                rate = (decimal)contractSetting.ReturnEmpty;
                        }

                        FIN_PL pl = new FIN_PL();
                        pl.CreatedBy = Account.UserName;
                        pl.CreatedDate = DateTime.Now;
                        pl.Code = string.Empty;
                        pl.IsPlanning = false;
                        pl.SYSCustomerID = Account.SYSCustomerID;
                        pl.Effdate = DateConfig.Date;
                        pl.OrderID = itemContainer.OrderID;
                        pl.COTOMasterID = itemContainer.COTOMasterID;
                        pl.VendorID = itemContainer.VendorID.HasValue ? itemContainer.VendorID : Account.SYSCustomerID;
                        pl.CustomerID = itemContract.CustomerID;
                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                        pl.ContractID = itemContract.ID;
                        lstPl.Add(pl);

                        FIN_PLDetails plDetail = new FIN_PLDetails();
                        plDetail.CreatedBy = Account.UserName;
                        plDetail.CreatedDate = DateTime.Now;
                        plDetail.CostID = (int)CATCostType.COTOFreightDebit;
                        pl.FIN_PLDetails.Add(plDetail);

                        FIN_PLContainer plCon = new FIN_PLContainer();
                        plCon.CreatedBy = Account.UserName;
                        plCon.CreatedDate = DateTime.Now;
                        plCon.COTOContainerID = itemContainer.OPSContainerID;
                        plCon.Unit = itemContainer.PackingName;
                        plCon.UnitPrice = itemContainer.UnitPrice * rate / 100;
                        plCon.Quantity = 1;
                        plDetail.Debit += (decimal)plCon.Quantity * plCon.UnitPrice;
                        plDetail.FIN_PLContainer.Add(plCon);

                        pl.Debit += plDetail.Debit;

                    }
                    #endregion

                    #region Trouble
                    foreach (var itemORDContainerGroup in queryORDContainer.GroupBy(c => new { c.COTOMasterID, c.VendorID }))
                    {
                        var lstCreditTrouble = ItemInput.ListTrouble.Where(c => c.COTOMasterID == itemORDContainerGroup.Key.COTOMasterID && c.CostOfVendor != 0 && c.TroubleCostStatusID == -(int)SYSVarType.TroubleCostStatusApproved);
                        if (lstCreditTrouble.Count() > 0)
                        {
                            foreach (var item in lstCreditTrouble)
                            {
                                FIN_PL pl = new FIN_PL();
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.Code = string.Empty;
                                pl.IsPlanning = false;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.Effdate = DateConfig.Date;
                                pl.COTOMasterID = itemORDContainerGroup.Key.COTOMasterID;
                                pl.VendorID = itemORDContainerGroup.Key.VendorID.HasValue ? itemORDContainerGroup.Key.VendorID : Account.SYSCustomerID;
                                pl.CustomerID = itemContract.CustomerID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                pl.ContractID = itemContract.ID;
                                lstPl.Add(pl);

                                FIN_PLDetails plCreditTrouble = new FIN_PLDetails();
                                plCreditTrouble.CreatedBy = Account.UserName;
                                plCreditTrouble.CreatedDate = DateTime.Now;
                                plCreditTrouble.CostID = (int)CATCostType.TroubleDebit;
                                plCreditTrouble.Debit = item.CostOfVendor;
                                plCreditTrouble.Note = item.Description;
                                plCreditTrouble.TypeOfPriceDIExCode = item.GroupOfTroubleCode;

                                FIN_PLContainer plCon = new FIN_PLContainer();
                                plCon.CreatedBy = Account.UserName;
                                plCon.CreatedDate = DateTime.Now;
                                plCon.COTOContainerID = itemORDContainerGroup.FirstOrDefault().OPSContainerID;
                                pl.OrderID = itemORDContainerGroup.FirstOrDefault().OrderID;
                                plCreditTrouble.FIN_PLContainer.Add(plCon);

                                pl.FIN_PLDetails.Add(plCreditTrouble);
                                pl.Debit += plCreditTrouble.Debit;
                            }
                        }
                    }
                    #endregion

                    #region Tờ khai
                    foreach (var item in ItemInput.ListDocumentService)
                    {
                        var lstContainer = ItemInput.ListDocumentContainer.Where(c => c.DocumentID == item.DocumentID);
                        if (lstContainer.Count() > 0)
                        {
                            // Giá tờ khai chi đều cho các container
                            var price = ItemInput.ListContainerService.Where(c => c.ServiceID == item.ServiceID && c.PackingID == null && c.ContractID == itemContract.ID && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                            if (price != null)
                            {
                                decimal unitPrice = price.Price / lstContainer.Count();
                                foreach (var itemContainer in lstContainer)
                                {
                                    FIN_PL pl = new FIN_PL();
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.Code = string.Empty;
                                    pl.IsPlanning = false;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.Effdate = DateConfig.Date;
                                    pl.OrderID = itemContainer.OrderID;
                                    pl.CustomerID = itemContainer.CustomerID;
                                    pl.VendorID = itemContainer.VendorID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                    pl.ContractID = itemContract.ID;
                                    lstPl.Add(pl);

                                    FIN_PLDetails plCredit = new FIN_PLDetails();
                                    plCredit.CreatedBy = Account.UserName;
                                    plCredit.CreatedDate = DateTime.Now;
                                    plCredit.CostID = (int)CATCostType.ORDDocumentDebit;
                                    plCredit.Debit = unitPrice;
                                    plCredit.TypeOfPriceDIExCode = item.ServiceCode;

                                    FIN_PLContainer plCon = new FIN_PLContainer();
                                    plCon.CreatedBy = Account.UserName;
                                    plCon.CreatedDate = DateTime.Now;
                                    plCon.COTOContainerID = itemContainer.OPSContainerID;
                                    plCredit.FIN_PLContainer.Add(plCon);

                                    pl.FIN_PLDetails.Add(plCredit);
                                }
                            }
                        }
                    }
                    #endregion

                    #region Giá service container
                    foreach (var itemContainer in ItemInput.ListDocumentContainer)
                    {
                        foreach (var itemService in itemContainer.ListService)
                        {
                            var price = ItemInput.ListContainerService.Where(c => c.ServiceID == itemService.ServiceID && c.PackingID == itemContainer.PackingID && c.ContractID == itemContract.ID && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                            if (price != null)
                            {
                                FIN_PL pl = new FIN_PL();
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.Code = string.Empty;
                                pl.IsPlanning = false;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.Effdate = DateConfig.Date;
                                pl.OrderID = itemContainer.OrderID;
                                pl.CustomerID = itemContainer.CustomerID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                pl.ContractID = itemContract.ID;
                                lstPl.Add(pl);

                                FIN_PLDetails plCredit = new FIN_PLDetails();
                                plCredit.CreatedBy = Account.UserName;
                                plCredit.CreatedDate = DateTime.Now;
                                plCredit.CostID = (int)CATCostType.ORDContainerServiceDebit;
                                plCredit.Debit = price.Price;
                                plCredit.TypeOfPriceDIExCode = itemService.ServiceCode;

                                FIN_PLContainer plCon = new FIN_PLContainer();
                                plCon.CreatedBy = Account.UserName;
                                plCon.CreatedDate = DateTime.Now;
                                plCon.COTOContainerID = itemContainer.OPSContainerID;
                                plCredit.FIN_PLContainer.Add(plCon);

                                pl.FIN_PLDetails.Add(plCredit);
                            }
                        }
                    }
                    #endregion

                    #region Phụ thu
                    var lstEx = ItemInput.ListEx.Where(c => c.ContractID == itemContract.ID && !string.IsNullOrEmpty(c.ExprInput) && c.EffectDate <= DateConfig).ToList();
                    var ExEffateDate = lstEx.Select(c => new { c.EffectDate }).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                    if (ExEffateDate != null)
                        lstEx = lstEx.Where(c => c.EffectDate == ExEffateDate.EffectDate).ToList();
                    string strExName = string.Empty;
                    //Chạy từng phụ thu
                    foreach (var itemEx in lstEx)
                    {
                        System.Diagnostics.Debug.WriteLine("Phụ phí: " + itemEx.Note);

                        var lstMasterEx = new List<HelperFinance_COMaster>();
                        var lstCOTOContainerEx = new List<HelperFinance_OPSContainer>();
                        var lstContainerEx = new List<HelperFinance_OPSContainer>();

                        var queryCOTOContainerEx = queryORDContainer.Where(c => itemEx.ServiceOfOrderID == null ? true : c.ServiceOfOrderID == itemEx.ServiceOfOrderID).ToList();
                        var queryContainerEx = queryOPSContainer.Where(c => itemEx.ServiceOfOrderID == null ? true : c.ServiceOfOrderID == itemEx.ServiceOfOrderID).ToList();
                        var lstCOTOMasterID = queryCOTOContainerEx.Select(c => c.COTOMasterID).Distinct().ToList();
                        var queryMasterEx = queryOrderContract.Where(c => lstCOTOMasterID.Contains(c.ID)).ToList();

                        strExName = itemEx.Note;
                        //Danh sách các điều kiện lọc 
                        var lstExParentRouting = itemEx.ListParentRouting;
                        var lstExRouting = itemEx.ListRouting;
                        var lstExGroupLocation = itemEx.ListGroupOfLocation;
                        var lstExPartner = itemEx.ListPartnerID;
                        var lstExLocationFrom = itemEx.ListLocation.Where(c => c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                            .Select(c => c.LocationID).ToList();
                        var lstExLocationTo = itemEx.ListLocation.Where(c => (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                            .Select(c => c.LocationID).ToList();
                        var lstExContainer = itemEx.ListContainer.Select(c => c.PackingID).Distinct().ToList();

                        if (lstExParentRouting.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExParentRouting.Contains(c.ParentRoutingID)).ToList();
                        if (lstExRouting.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExRouting.Contains(c.CATRoutingID)).ToList();
                        if (lstExLocationFrom.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExLocationFrom.Contains(c.LocationFromID)).ToList();
                        if (lstExLocationTo.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExLocationTo.Contains(c.LocationToID)).ToList();
                        if (lstExContainer.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExContainer.Contains(c.PackingID)).ToList();
                        if (lstExPartner.Count > 0)
                            queryCOTOContainerEx = queryCOTOContainerEx.Where(c => lstExPartner.Contains(c.PartnerID)).ToList();


                        var exOrderID = lstMasterEx.Select(c => c.ID).ToArray();
                        var exGroupID = queryCOTOContainerEx.Where(c => c.COTOMasterID.HasValue).Select(c => c.COTOMasterID.Value).Distinct().ToArray();
                        var lstOrderCheckID = exGroupID.Intersect(exOrderID).ToList();
                        var lstOrderCheck = lstMasterEx.Where(c => lstOrderCheckID.Contains(c.ID)).ToList();
                        var lstCOTOContainerCheck = queryCOTOContainerEx.Where(c => lstOrderCheckID.Contains(c.COTOMasterID.Value)).ToList();
                        var lstOPSContainerID = lstCOTOContainerCheck.Select(c => c.OPSContainerID).Distinct().ToList();
                        var lstContainerCheck = ItemInput.ListContainer.Where(c => lstOPSContainerID.Contains(c.ID)).ToList();

                        // Ktra công thức đầu vào
                        if (lstOrderCheck.Count > 0 && lstCOTOContainerCheck.Count > 0)
                        {
                            #region Theo đơn hàng or chuyến trong ngày
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerInDay || itemEx.COExSumID == -(int)SYSVarType.COExScheduleInDay)
                            {
                                bool flag = true;
                                //Thực hiện công thức
                                DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                itemExpr.TotalSchedule = lstCOTOContainerCheck.Where(c => lstOrderCheckID.Contains(c.OrderID)).Select(c => c.COTOMasterID).Distinct().Count();
                                itemExpr.TotalOrder = lstOrderCheck.Count;
                                itemExpr.TotalPacking = lstCOTOContainerCheck.Select(c => c.OPSContainerID).Distinct().Count();
                                itemExpr.Credit = 0;
                                try
                                {
                                    flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                }
                                catch { flag = false; }
                                if (flag == true)
                                {
                                    lstMasterEx = lstOrderCheck;
                                    lstCOTOContainerEx = lstCOTOContainerCheck;
                                    lstContainerEx = lstContainerCheck;
                                }
                            }
                            #endregion

                            #region Container
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainer)
                            {
                                foreach (var item in lstContainerCheck)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.Credit = 0;
                                    itemExpr.PackingCode = item.PackingName;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        if (lstMasterEx.Count(c => c.ID == item.COTOMasterID) == 0)
                                        {
                                            lstMasterEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == item.COTOMasterID));
                                        }
                                        lstContainerEx.Add(item);
                                        lstCOTOContainerEx.AddRange(lstCOTOContainerCheck.Where(c => c.OPSContainerID == item.OPSContainerID));
                                    }
                                }
                            }
                            #endregion

                            #region Container theo cung đường
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerRoute)
                            {
                                var lstGroup = lstContainerCheck.GroupBy(c => c.OrderRoutingID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = itemGroup.Select(c => c.COTOMasterID).Distinct().Count();
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        foreach (var orderID in itemGroup.Select(c => c.COTOMasterID).Distinct().ToList())
                                        {
                                            if (lstMasterEx.Count(c => c.ID == orderID) == 0)
                                            {
                                                lstMasterEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == orderID));
                                            }
                                        }
                                        lstContainerEx.AddRange(itemGroup.ToArray());
                                        var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                        lstCOTOContainerEx.AddRange(lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)));
                                    }
                                }
                            }
                            #endregion

                            #region Container theo chặng rỗng
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerEmpty)
                            {
                                var lstGroup = lstCOTOContainerCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOEmpty);
                                foreach (var itemGroup in lstGroup)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.PackingCode = itemGroup.PackingName;
                                    itemExpr.Credit = 0;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        if (lstMasterEx.Count(c => c.ID == itemGroup.COTOMasterID) == 0)
                                        {
                                            lstMasterEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.COTOMasterID));
                                        }
                                        lstCOTOContainerEx.Add(itemGroup);
                                        if (lstContainerEx.Count(c => c.OPSContainerID == itemGroup.OPSContainerID) == 0)
                                        {
                                            lstContainerEx.Add(lstContainerCheck.FirstOrDefault(c => c.OPSContainerID == itemGroup.OPSContainerID));
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Container theo chặng đầy
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerLaden)
                            {
                                var lstGroup = lstCOTOContainerCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden);
                                foreach (var itemGroup in lstGroup)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.PackingCode = itemGroup.PackingName;
                                    itemExpr.Credit = 0;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        if (lstMasterEx.Count(c => c.ID == itemGroup.COTOMasterID) == 0)
                                        {
                                            lstMasterEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.COTOMasterID));
                                        }
                                        lstCOTOContainerEx.Add(itemGroup);
                                        if (lstContainerEx.Count(c => c.OPSContainerID == itemGroup.OPSContainerID) == 0)
                                        {
                                            lstContainerEx.Add(lstContainerCheck.FirstOrDefault(c => c.OPSContainerID == itemGroup.OPSContainerID));
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Chuyến
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSchedule)
                            {
                                var lstGroup = lstCOTOContainerCheck.GroupBy(c => c.COTOMasterID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        foreach (var orderID in itemGroup.Select(c => c.COTOMasterID).Distinct().ToList())
                                        {
                                            if (lstMasterEx.Count(c => c.ID == orderID) == 0)
                                            {
                                                lstMasterEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == orderID));
                                            }
                                        }
                                        lstCOTOContainerEx.AddRange(itemGroup.ToArray());
                                        var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                        foreach (var itemContainerID in lstContainerID)
                                        {
                                            if (lstContainerEx.Count(c => c.OPSContainerID == itemContainerID) == 0)
                                            {
                                                lstContainerEx.Add(lstContainerCheck.FirstOrDefault(c => c.OPSContainerID == itemContainerID));
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            #region Chuyến theo cung đường
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerRoute)
                            {
                                var lstGroup = lstCOTOContainerCheck.GroupBy(c => c.OrderRoutingID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = itemGroup.Select(c => c.COTOMasterID).Distinct().Count();
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;
                                    try
                                    {
                                        flag = Expression_COCheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        foreach (var orderID in itemGroup.Select(c => c.COTOMasterID).Distinct().ToList())
                                        {
                                            if (lstMasterEx.Count(c => c.ID == orderID) == 0)
                                            {
                                                lstMasterEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == orderID));
                                            }
                                        }
                                        lstCOTOContainerEx.AddRange(itemGroup.ToArray());
                                        var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                        foreach (var itemContainerID in lstContainerID)
                                        {
                                            if (lstContainerEx.Count(c => c.OPSContainerID == itemContainerID) == 0)
                                                lstContainerEx.Add(lstContainerCheck.FirstOrDefault(c => c.OPSContainerID == itemContainerID));
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Ghi dữ liệu
                        if (lstMasterEx.Count > 0 && (lstCOTOContainerEx.Count > 0 || lstContainerEx.Count > 0))
                        {
                            #region Theo đơn hàng or chuyến trong ngày
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerInDay || itemEx.COExSumID == -(int)SYSVarType.COExScheduleInDay)
                            {
                                //Thực hiện công thức
                                DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                itemExpr.TotalOrder = lstCOTOContainerEx.Select(c => c.OrderID).Distinct().Count();
                                itemExpr.TotalSchedule = lstMasterEx.Count;
                                itemExpr.TotalPacking = lstCOTOContainerEx.Select(c => c.OPSContainerID).Distinct().Count();
                                itemExpr.Credit = 0;

                                decimal? fixPrice = Expression_COGetValue(Expression_GetPackage(itemEx.ExprPriceFix), itemExpr);

                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.Credit = fixPrice.HasValue ? fixPrice.Value : 0;
                                pl.ContractID = itemContract.ID;
                                pl.CustomerID = itemContract.CustomerID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.COTOExDebit;
                                plCost.Note = itemEx.Note;
                                plCost.Credit = fixPrice.HasValue ? fixPrice.Value : 0;
                                plCost.TypeOfPriceDIExCode = itemEx.TypeOfPriceCOExCode;
                                pl.FIN_PLDetails.Add(plCost);

                                var lstCOTOContaienrID = lstCOTOContainerCheck.OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                COPriceEx_FindOrder(itemEx, pl, plCost, lstPlTemp, lstPLTempContract, lstCOTOContaienrID, itemContract.ID, lstCOTOContainerCheck, lstOrderContainerUpDate);
                                lstPl.Add(pl);
                            }
                            #endregion

                            #region Container
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainer)
                            {
                                foreach (var item in lstContainerEx)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.Credit = 0;
                                    itemExpr.PackingCode = item.PackingName;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => c.OPSContainerID == item.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => c.OPSContainerID == item.OPSContainerID).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity) && c.PackingID == item.PackingID);
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            itemExpr = new DTOPriceCOExExpr();
                                            itemExpr.TotalSchedule = 1;
                                            itemExpr.TotalOrder = 1;
                                            itemExpr.TotalPacking = 1;
                                            itemExpr.Credit = 0;
                                            itemExpr.PackingCode = item.PackingName;
                                            COPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID.FirstOrDefault(), itemContract.ID, lstCOTOContainerCheck.Where(c => c.OPSContainerID == item.OPSContainerID).ToList());
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion

                            #region Container theo cung đường
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerRoute)
                            {
                                var lstGroup = lstContainerEx.GroupBy(c => c.OrderRoutingID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = itemGroup.Select(c => c.COTOMasterID).Distinct().Count();
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            foreach (var itemOPSPriceExContainer in itemGroup.Where(c => c.PackingID == itemPriceExContainer.PackingID))
                                            {
                                                itemExpr = new DTOPriceCOExExpr();
                                                itemExpr.TotalSchedule = 1;
                                                itemExpr.TotalOrder = 1;
                                                itemExpr.TotalPacking = 1;
                                                itemExpr.Credit = 0;
                                                itemExpr.PackingCode = itemOPSPriceExContainer.PackingName;
                                                var lstID = lstCOTOContainerEx.Where(c => c.OPSContainerID == itemOPSPriceExContainer.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                                COPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstID.FirstOrDefault(), itemContract.ID, lstCOTOContainerEx);
                                            }
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion

                            #region Container theo chặng rỗng
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerEmpty)
                            {
                                var lstGroup = lstCOTOContainerCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMEmpty || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOEmpty);
                                foreach (var itemGroup in lstGroup)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.PackingCode = itemGroup.PackingName;
                                    itemExpr.Credit = 0;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstContainerID = new List<int> { itemGroup.OPSContainerID };
                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)) && c.PackingID == itemGroup.PackingID);
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            itemExpr = new DTOPriceCOExExpr();
                                            itemExpr.TotalSchedule = 1;
                                            itemExpr.TotalOrder = 1;
                                            itemExpr.TotalPacking = 1;
                                            itemExpr.Credit = 0;
                                            itemExpr.PackingCode = itemGroup.PackingName;
                                            var lstID = lstCOTOContainerEx.Where(c => c.OPSContainerID == itemGroup.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                            COPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstID.FirstOrDefault(), itemContract.ID, lstCOTOContainerEx);
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion

                            #region Container theo chặng đầy
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerLaden)
                            {
                                var lstGroup = lstCOTOContainerCheck.Where(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden);
                                foreach (var itemGroup in lstGroup)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = 1;
                                    itemExpr.TotalPacking = 1;
                                    itemExpr.PackingCode = itemGroup.PackingName;
                                    itemExpr.Credit = 0;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstContainerID = new List<int> { itemGroup.OPSContainerID };
                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)) && c.PackingID == itemGroup.PackingID);
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            itemExpr = new DTOPriceCOExExpr();
                                            itemExpr.TotalSchedule = 1;
                                            itemExpr.TotalOrder = 1;
                                            itemExpr.TotalPacking = 1;
                                            itemExpr.Credit = 0;
                                            itemExpr.PackingCode = itemGroup.PackingName;
                                            var lstID = lstCOTOContainerEx.Where(c => c.OPSContainerID == itemGroup.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                            COPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstID.FirstOrDefault(), itemContract.ID, lstCOTOContainerEx);
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion

                            #region Chuyến
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSchedule)
                            {
                                var lstGroup = lstCOTOContainerCheck.GroupBy(c => c.COTOMasterID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = 1;
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            foreach (var itemOPSPriceExContainer in itemGroup.Where(c => c.PackingID == itemPriceExContainer.PackingID))
                                            {
                                                itemExpr = new DTOPriceCOExExpr();
                                                itemExpr.TotalSchedule = 1;
                                                itemExpr.TotalOrder = 1;
                                                itemExpr.TotalPacking = 1;
                                                itemExpr.Credit = 0;
                                                itemExpr.PackingCode = itemOPSPriceExContainer.PackingName;
                                                var lstID = lstCOTOContainerEx.Where(c => c.OPSContainerID == itemOPSPriceExContainer.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                                COPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstID.FirstOrDefault(), itemContract.ID, lstCOTOContainerEx);
                                            }
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion

                            #region Chuyến theo cung đường
                            if (itemEx.COExSumID == -(int)SYSVarType.COExSumContainerRoute)
                            {
                                var lstGroup = lstCOTOContainerCheck.GroupBy(c => c.OrderRoutingID);
                                foreach (var itemGroup in lstGroup)
                                {
                                    //Thực hiện công thức
                                    DTOPriceCOExExpr itemExpr = new DTOPriceCOExExpr();
                                    itemExpr.TotalSchedule = itemGroup.Select(c => c.COTOMasterID).Distinct().Count();
                                    itemExpr.TotalOrder = itemGroup.Select(c => c.OrderID).Distinct().Count();
                                    itemExpr.TotalPacking = itemGroup.Select(c => c.OPSContainerID).Distinct().Count();
                                    itemExpr.Credit = 0;

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    var lstContainerID = itemGroup.Select(c => c.OPSContainerID).Distinct().ToList();
                                    var lstCOTOContainerID = lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                    COPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstCOTOContainerID, itemContract.ID, lstCOTOContainerCheck.Where(c => lstContainerID.Contains(c.OPSContainerID)).ToList());

                                    // Tính theo từng cont 
                                    var lstPriceExContainer = itemEx.ListContainer.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                    if (lstPriceExContainer.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExContainer in lstPriceExContainer)
                                        {
                                            foreach (var itemOPSPriceExContainer in itemGroup.Where(c => c.PackingID == itemPriceExContainer.PackingID))
                                            {
                                                itemExpr = new DTOPriceCOExExpr();
                                                itemExpr.TotalSchedule = 1;
                                                itemExpr.TotalOrder = 1;
                                                itemExpr.TotalPacking = 1;
                                                itemExpr.Credit = 0;
                                                itemExpr.PackingCode = itemOPSPriceExContainer.PackingName;
                                                var lstID = lstCOTOContainerEx.Where(c => c.OPSContainerID == itemOPSPriceExContainer.OPSContainerID).OrderBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden).ThenBy(c => c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden).Select(c => c.ID).Distinct().ToList();
                                                COPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExContainer, pl, lstPlTemp, lstPLTempContract, lstID.FirstOrDefault(), itemContract.ID, lstCOTOContainerEx);
                                            }
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                    #endregion

                    System.Diagnostics.Debug.WriteLine("Contract end: " + itemContract.ID);
                }

                #endregion

                foreach (var pl in lstPl)
                    model.FIN_PL.Add(pl);
            }
        }

        public static List<DTOOPSContainerPrice> Container_TenderPrice(DataEntities model, AccountItem Account, List<int> lstOpsContainerID)
        {
            List<DTOOPSContainerPrice> result = new List<DTOOPSContainerPrice>();
            foreach (var opsConID in lstOpsContainerID)
            {
                DTOOPSContainerPrice item = new DTOOPSContainerPrice();
                item.OPSContainerID = opsConID;
                item.ListVendorPrice = new List<DTOOPSContainerPrice_Vendor>();
                var objCon = model.OPS_Container.Where(c => c.ID == opsConID).Select(c => new
                {
                    c.ID,
                    DateConfig = c.ORD_Container.DateConfig.HasValue ? c.ORD_Container.DateConfig.Value : c.ORD_Container.ORD_Order.DateConfig,
                    LocationFromID = c.ORD_Container.LocationFromID.HasValue ? c.ORD_Container.CUS_Location2.LocationID : -1,
                    LocationToID = c.ORD_Container.LocationToID.HasValue ? c.ORD_Container.CUS_Location3.LocationID : -1,
                    LocationDepotID = c.ORD_Container.LocationDepotID.HasValue ? c.ORD_Container.CUS_Location.LocationID : -1,
                    LocationDepotReturnID = c.ORD_Container.LocationDepotReturnID.HasValue ? c.ORD_Container.CUS_Location1.LocationID : -1,
                    RoutingID = c.ORD_Container.CUSRoutingID > 0 ? c.ORD_Container.CUS_Routing.RoutingID : -1,
                    ParentRoutingID = c.ORD_Container.CUSRoutingID > 0 ? c.ORD_Container.CUS_Routing.CAT_Routing.ParentID : -1,
                    TransportModeID = c.ORD_Container.ORD_Order.CAT_TransportMode.TransportModeID,
                    ServiceOfOrderID = c.ORD_Container.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID,
                    CustomerID = c.ORD_Container.ORD_Order.CustomerID,
                    PackingID = c.ORD_Container.PackingID,
                }).FirstOrDefault();
                if (objCon != null)
                {
                    var DateConfig = objCon.DateConfig.Date;
                    var lstContract = model.CAT_ContractTerm.Where(c => c.CAT_Contract.SYSCustomerID == Account.SYSCustomerID && c.CAT_Contract.CUS_Company.CustomerRelateID == objCon.CustomerID && c.DateEffect <= DateConfig && (c.CAT_Contract.ExpiredDate == null || c.CAT_Contract.ExpiredDate > DateConfig) &&
                        c.CAT_Contract.CAT_TransportMode.TransportModeID == objCon.TransportModeID && c.ServiceOfOrderID == objCon.ServiceOfOrderID).Select(c => new HelperFinance_Contract
                        {
                            ID = c.ID,
                            TransportModeID = c.CAT_Contract.TransportModeID,
                            TypeOfContractDateID = c.CAT_Contract.TypeOfContractDateID,
                            EffectDate = c.DateEffect,
                            VendorID = c.CAT_Contract.CustomerID.Value,
                            VendorCode = c.CAT_Contract.CUS_Customer.Code,
                            VendorName = c.CAT_Contract.CUS_Customer.CustomerName,
                        }).OrderByDescending(c => c.EffectDate).ToList();

                    foreach (var itemContract in lstContract)
                    {
                        if (item.ListVendorPrice.Count(c => c.VendorID == itemContract.VendorID) == 0)
                        {
                            List<HelperFinance_OPSVendorPrice> lstPrice = new List<HelperFinance_OPSVendorPrice>();
                            // Check có giá của cung đường này hay ko
                            var objPrice = model.CAT_PriceCOContainer.Where(c => c.CAT_Price.ContractTermID == itemContract.ID && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.PackingID == objCon.PackingID && c.CAT_ContractRouting.RoutingID == objCon.RoutingID && c.Price > 0).OrderByDescending(c => c.Price).Select(c => new HelperFinance_OPSVendorPrice
                            {
                                ID = c.ID,
                                Price = c.Price,
                                Code = c.CAT_ContractRouting.CAT_Routing.Code,
                                RoutingName = c.CAT_ContractRouting.CAT_Routing.RoutingName,
                            }).FirstOrDefault();
                            if (objPrice == null)
                                objPrice = model.CAT_PriceCOContainer.Where(c => c.CAT_Price.ContractTermID == itemContract.ID && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.PackingID == objCon.PackingID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == objCon.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == objCon.LocationToID && c.Price > 0).OrderByDescending(c => c.Price).Select(c => new HelperFinance_OPSVendorPrice
                                {
                                    ID = c.ID,
                                    Price = c.Price,
                                    Code = c.CAT_ContractRouting.CAT_Routing.Code,
                                    RoutingName = c.CAT_ContractRouting.CAT_Routing.RoutingName,
                                }).FirstOrDefault();
                            if (objPrice == null)
                            {
                                lstPrice = model.CAT_PriceCOContainer.Where(c => c.CAT_Price.ContractTermID == itemContract.ID && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.PackingID == objCon.PackingID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID > 0 && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID > 0).Select(c => new HelperFinance_OPSVendorPrice
                                {
                                    ID = c.ID,
                                    Price = c.Price,
                                    Code = c.CAT_ContractRouting.CAT_Routing.Code,
                                    RoutingName = c.CAT_ContractRouting.CAT_Routing.RoutingName,
                                    ListFrom = c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Select(d => d.LocationID).ToList(),
                                    ListTo = c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Select(d => d.LocationID).ToList(),
                                }).ToList();
                            }

                            if (objCon.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport)
                            {
                                if (objPrice == null)
                                    objPrice = lstPrice.Where(c => c.ListFrom.Any(d => d == objCon.LocationFromID) && c.ListFrom.Any(d => d == objCon.LocationDepotID) && c.ListTo.Any(d => d == objCon.LocationToID) && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                            }
                            if (objCon.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)
                            {
                                if (objPrice == null)
                                    objPrice = lstPrice.Where(c => c.ListFrom.Any(d => d == objCon.LocationFromID) && c.ListTo.Any(d => d == objCon.LocationToID) && c.ListTo.Any(d => d == objCon.LocationDepotReturnID) && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                            }
                            if (objCon.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal)
                            {
                                if (objPrice == null)
                                    objPrice = lstPrice.Where(c => c.ListFrom.Any(d => d == objCon.LocationFromID) && c.ListFrom.Any(d => d == objCon.LocationDepotID) && c.ListTo.Any(d => d == objCon.LocationToID) && c.ListTo.Any(d => d == objCon.LocationDepotReturnID) && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                            }
                            if (objPrice == null)
                                objPrice = lstPrice.Where(c => c.ListFrom.Any(d => d == objCon.LocationFromID || d == objCon.LocationDepotID) && c.ListTo.Any(d => d == objCon.LocationToID || d == objCon.LocationDepotReturnID) && c.Price > 0).OrderByDescending(c => c.Price).FirstOrDefault();
                            if (objPrice != null)
                            {
                                DTOOPSContainerPrice_Vendor itemVendor = new DTOOPSContainerPrice_Vendor();
                                itemVendor.VendorID = itemContract.VendorID;
                                itemVendor.VendorCode = itemContract.VendorCode;
                                itemVendor.VendorName = itemContract.VendorName;
                                itemVendor.RoutingCode = objPrice.Code;
                                itemVendor.RoutingName = objPrice.RoutingName;
                                itemVendor.Price = objPrice.Price;
                                item.ListVendorPrice.Add(itemVendor);
                            }
                        }
                    }
                }
                item.ListVendorPrice = item.ListVendorPrice.OrderBy(c => c.Price).ToList();
                if (item.ListVendorPrice.Count > 0)
                    result.Add(item);
            }

            return result;
        }

        #endregion

        #region Common
        private static HelperFinance_PriceInput COPrice_GetInput(DataEntities model, DateTime DateConfig, AccountItem Account, List<int> lstCustomerID, bool isCredit)
        {
            DateTime DateConfigEnd = DateConfig.AddDays(1);
            HelperFinance_PriceInput result = new HelperFinance_PriceInput();
            result.ListOrder = new List<HelperFinance_Order>();
            result.ListContract = new List<HelperFinance_Contract>();
            result.ListCOMaster = new List<HelperFinance_COMaster>();
            result.ListOPSContainer = new List<HelperFinance_OPSContainer>();
            result.ListContainer = new List<HelperFinance_OPSContainer>();
            result.ListContainerPrice = new List<HelperFinance_PriceContainer>();
            result.ListContainerService = new List<HelperFinance_PriceService>();
            result.ListDocumentContainer = new List<HelperFinance_DocumentContainer>();
            result.ListServiceOfOrder = new List<CATServiceOfOrder>();
            result.ListEx = new List<HelperFinance_PriceEx>();
            if (isCredit)
                result.ListCutomerID = lstCustomerID;
            else
            {
                // Lấy danh sách vendor của customer
                result.ListCutomerID = model.CUS_Company.Where(c => lstCustomerID.Contains(c.CustomerRelateID) && (c.CUS_Customer.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN || c.CUS_Customer.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH)).Select(c => c.CustomerOwnID).Distinct().ToList();
                result.ListCutomerID.Add(Account.SYSCustomerID);
            }

            result.ListContract = model.CAT_Contract.Where(c => c.CustomerID > 0 && c.SYSCustomerID == Account.SYSCustomerID && result.ListCutomerID.Contains(c.CustomerID.Value) && c.EffectDate <= DateConfig && (c.ExpiredDate == null || c.ExpiredDate >= DateConfig) && c.TypeOfContractID == -(int)SYSVarType.TypeOfContractMain && (c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFCL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLCL)).Select(c => new HelperFinance_Contract
            {
                ID = c.ID,
                EffectDate = c.EffectDate,
                PriceInDay = c.PriceInDay,
                TransportModeID = c.CAT_TransportMode.TransportModeID,
                TypeOfContractDateID = c.TypeOfContractDateID,
                TypeOfRunLevelID = c.TypeOfRunLevelID,
                CustomerID = isCredit ? c.CustomerID : (c.CompanyID.HasValue ? c.CUS_Company.CustomerRelateID : -1),
                VendorID = !isCredit ? c.CustomerID.Value : -1,
                ExprFCLAllocationPrice = c.ExprFCLAllocationPrice
            }).ToList();

            if (!isCredit)
            {
                foreach (var itemContract in result.ListContract)
                {
                    if (itemContract.CustomerID == -1)
                        itemContract.CustomerID = null;
                }
            }

            result.ListContractID = result.ListContract.Select(c => c.ID).ToList();

            var lstStatusCon = new List<int>();
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerEXEmpty);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerEXLaden);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerIMLaden);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerIMEmpty);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerLOGetEmpty);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerLOLaden);
            lstStatusCon.Add(-(int)SYSVarType.StatusOfCOContainerLOReturnEmpty);

            #region Lấy data
            if (isCredit)
            {
                //Lấy các đơn hàng
                result.ListOrder = model.ORD_Order.Where(c => c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderPlaning && c.ContractID > 0 && result.ListContractID.Contains(c.ContractID.Value)).Select(c => new HelperFinance_Order
                {
                    ID = c.ID,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    CATRoutingID = c.CUSRoutingID > 0 ? c.CUS_Routing.RoutingID : -1,
                    ParentRoutingID = c.CUSRoutingID > 0 && c.CUS_Routing.CAT_Routing.ParentID > 0 ? c.CUS_Routing.CAT_Routing.ParentID.Value : -1,
                    DateConfig = c.DateConfig,
                    CustomerID = c.CustomerID,
                    TransportModeID = c.CAT_TransportMode.TransportModeID,
                    OrderCode = c.Code,
                    ContractTermID = c.ContractTermID,
                    ServiceOfOrderID = c.ServiceOfOrderID,
                }).ToList();

                result.ListOPSContainer = model.OPS_COTOContainer.Where(c => c.ParentID == null && c.COTOMasterID > 0 && c.OPS_Container.ORD_Container.DateConfig.HasValue && c.OPS_Container.ORD_Container.DateConfig >= DateConfig && c.OPS_Container.ORD_Container.DateConfig < DateConfigEnd && c.OPS_Container.ORD_Container.ORD_Order.ContractID > 0 && result.ListContractID.Contains(c.OPS_Container.ORD_Container.ORD_Order.ContractID.Value) && c.OPS_Container.OPSContainerTypeID == -(int)SYSVarType.OPSContainerTypeComplete && (c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel) && lstStatusCon.Contains(c.StatusOfCOContainerID)).Select(c => new HelperFinance_OPSContainer
                {
                    ID = c.ID,
                    OrderContainerID = c.OPS_Container.ContainerID,
                    OrderID = c.OPS_Container.ORD_Container.OrderID,
                    COTOMasterID = c.COTOMasterID,
                    VehicleID = c.OPS_COTOMaster.VehicleID,
                    VendorID = c.OPS_COTOMaster.VendorOfVehicleID,
                    OPSContainerID = c.ID,
                    PackingID = c.OPS_Container.ORD_Container.PackingID,
                    PackingName = c.OPS_Container.ORD_Container.CAT_Packing.Code,
                    ContractID = c.OPS_Container.ORD_Container.ORD_Order.ContractID > 0 ? c.OPS_Container.ORD_Container.ORD_Order.ContractID.Value : -1,
                    CUSRoutingID = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUSRoutingID.Value : -1,
                    CATRoutingID = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUS_Routing.RoutingID : -1,
                    ParentRoutingID = c.OPS_Container.ORD_Container.CUSRoutingID > 0 && c.OPS_Container.ORD_Container.CUS_Routing.CAT_Routing.ParentID > 0 ? c.OPS_Container.ORD_Container.CUS_Routing.CAT_Routing.ParentID.Value : -1,
                    LocationFromID = c.LocationFromID,
                    LocationToID = c.LocationToID,
                    LocationDepotID = c.OPS_Container.ORD_Container.LocationDepotID > 0 ? c.OPS_Container.ORD_Container.LocationDepotID.Value : -1,
                    LocationDepotReturnID = c.OPS_Container.ORD_Container.LocationDepotReturnID > 0 ? c.OPS_Container.ORD_Container.LocationDepotReturnID.Value : -1,
                    CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                    OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                    StatusOfCOContainerID = c.StatusOfCOContainerID,
                    ContractTermID = c.OPS_Container.ORD_Container.ORD_Order.ContractTermID,
                    ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID.HasValue ? c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID.Value : -1,
                    PartnerID = c.OPS_Container.ORD_Container.PartnerID > 0 ? c.OPS_Container.ORD_Container.CUS_Partner.PartnerID : -1,
                }).ToList();

                var lstContainerID = result.ListOPSContainer.Select(c => c.OPSContainerID).Distinct().ToList();
                result.ListContainer = model.OPS_Container.Where(c => lstContainerID.Contains(c.ID)).Select(c => new HelperFinance_OPSContainer
                {
                    ID = c.ID,
                    OrderContainerID = c.ContainerID,
                    OrderID = c.ORD_Container.OrderID,
                    OPSContainerID = c.ID,
                    PackingID = c.ORD_Container.PackingID,
                    PackingName = c.ORD_Container.CAT_Packing.Code,
                    ContractID = c.ORD_Container.ORD_Order.ContractID > 0 ? c.ORD_Container.ORD_Order.ContractID.Value : -1,
                    CUSRoutingID = c.ORD_Container.CUSRoutingID > 0 ? c.ORD_Container.CUSRoutingID.Value : -1,
                    CATRoutingID = c.ORD_Container.CUSRoutingID > 0 ? c.ORD_Container.CUS_Routing.RoutingID : -1,
                    ParentRoutingID = c.ORD_Container.CUSRoutingID > 0 && c.ORD_Container.CUS_Routing.CAT_Routing.ParentID > 0 ? c.ORD_Container.CUS_Routing.CAT_Routing.ParentID.Value : -1,
                    LocationFromID = c.ORD_Container.LocationFromID > 0 ? c.ORD_Container.CUS_Location2.LocationID : -1,
                    LocationToID = c.ORD_Container.LocationToID > 0 ? c.ORD_Container.CUS_Location3.LocationID : -1,
                    LocationDepotID = c.ORD_Container.LocationDepotID > 0 ? c.ORD_Container.LocationDepotID.Value : -1,
                    LocationDepotReturnID = c.ORD_Container.LocationDepotReturnID > 0 ? c.ORD_Container.LocationDepotReturnID.Value : -1,
                    CustomerID = c.ORD_Container.ORD_Order.CustomerID,
                    OrderCode = c.ORD_Container.ORD_Order.Code,
                    ContractTermID = c.ORD_Container.ORD_Order.ContractTermID,
                    ServiceOfOrderID = c.ORD_Container.ORD_Order.ServiceOfOrderID.HasValue ? c.ORD_Container.ORD_Order.ServiceOfOrderID.Value : -1,
                    PartnerID = c.ORD_Container.PartnerID > 0 ? c.ORD_Container.CUS_Partner.PartnerID : -1,
                }).ToList();

                var ListDocumentContainer = model.ORD_DocumentContainer.Where(c => c.ORD_Document.DocumentStatusID == -(int)SYSVarType.DocumentStatusComplete && c.ORD_Document.ContractCustomerID > 0 && result.ListContractID.Contains(c.ORD_Document.ContractCustomerID.Value) && c.ORD_Document.DateConfigCustomer == DateConfig && c.ORD_Container.ORD_ContainerService.Count > 0).Select(c => new HelperFinance_DocumentContainer
                {
                    DocumentID = c.DocumentID,
                    ContainerID = c.ContainerID,
                    ContractCustomerID = c.ORD_Document.ContractCustomerID,
                    ContractVendorID = c.ORD_Document.ContractVendorID,
                    CustomerID = c.ORD_Document.ORD_Order.CustomerID,
                    DateConfigCustomer = c.ORD_Document.DateConfigCustomer.Value,
                    VendorID = c.ORD_Document.VendorID,
                    OrderID = c.ORD_Container.OrderID,
                    PackingID = c.ORD_Container.PackingID
                }).ToList();

                foreach (var item in ListDocumentContainer)
                {
                    var opsCon = model.OPS_COTOContainer.FirstOrDefault(c => c.OPS_Container.ContainerID == item.ContainerID && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden));
                    if (opsCon != null)
                    {
                        item.ListService = new List<HelperFinance_ContainerService>();
                        item.ListService = model.ORD_ContainerService.Where(c => c.ContainerID == item.ContainerID).Select(c => new HelperFinance_ContainerService
                        {
                            ServiceID = c.ServiceID,
                            ServiceCode = c.CAT_Service.Code,
                            PriceCustomer = c.PriceCustomer,
                            PriceVendor = c.PriceVendor
                        }).ToList();

                        item.OPSContainerID = opsCon.ID;
                        result.ListDocumentContainer.Add(item);
                    }

                }
                result.ListDocumentService = model.ORD_DocumentService.Where(c => c.ORD_Document.DocumentStatusID == -(int)SYSVarType.DocumentStatusComplete && c.ORD_Document.ContractCustomerID > 0 && result.ListContractID.Contains(c.ORD_Document.ContractCustomerID.Value) && c.ORD_Document.DateConfigCustomer == DateConfig).Select(c => new HelperFinance_DocumentService
                {
                    DocumentID = c.DocumentID,
                    ServiceID = c.ServiceID,
                    ServiceCode = c.CAT_Service.Code,
                    ContractCustomerID = c.ORD_Document.ContractCustomerID,
                    ContractVendorID = c.ORD_Document.ContractVendorID,
                    CustomerID = c.ORD_Document.ORD_Order.CustomerID,
                    DateConfigCustomer = c.ORD_Document.DateConfigCustomer.Value,
                    VendorID = c.ORD_Document.VendorID,
                    PriceCustomer = c.PriceCustomer,
                    PriceVendor = c.PriceVendor
                }).ToList();
            }
            else
            {
                //Lấy các chuyến
                result.ListCOMaster = model.OPS_COTOMaster.Where(c => c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.ContractID > 0 && result.ListContractID.Contains(c.ContractID.Value) && c.TransportModeID > 0).Select(c => new HelperFinance_COMaster
                {
                    ID = c.ID,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    CATRoutingID = c.RoutingID > 0 ? c.RoutingID.Value : -1,
                    ParentRoutingID = c.RoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                    DateConfig = c.DateConfig.Value,
                    VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                    TransportModeID = c.CAT_TransportMode.TransportModeID,
                    VehicleID = c.VehicleID,
                }).ToList();
                //Lấy các ops con
                result.ListOPSContainer = model.OPS_COTOContainer.Where(c => c.ParentID == null && c.COTOMasterID > 0 && c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_COTOMaster.DateConfig.HasValue && c.OPS_COTOMaster.DateConfig >= DateConfig && c.OPS_COTOMaster.DateConfig < DateConfigEnd && c.OPS_COTOMaster.ContractID > 0 && result.ListContractID.Contains(c.OPS_COTOMaster.ContractID.Value) && (c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel) && lstStatusCon.Contains(c.StatusOfCOContainerID)).Select(c => new HelperFinance_OPSContainer
                {
                    ID = c.ID,
                    OrderID = c.OPS_Container.ORD_Container.OrderID,
                    OrderContainerID = c.OPS_Container.ContainerID,
                    COTOMasterID = c.COTOMasterID,
                    VehicleID = c.OPS_COTOMaster.VehicleID,
                    PackingID = c.OPS_Container.ORD_Container.PackingID,
                    PackingName = c.OPS_Container.ORD_Container.CAT_Packing.Code,
                    ContractID = c.OPS_COTOMaster.ContractID > 0 ? c.OPS_COTOMaster.ContractID.Value : -1,
                    CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                    ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                    LocationFromID = c.LocationFromID,
                    LocationToID = c.LocationToID,
                    CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                    VendorID = c.OPS_COTOMaster.VendorOfVehicleID,
                    OPSContainerID = c.ID,
                    StatusOfCOContainerID = c.StatusOfCOContainerID,
                    ContractTermID = c.ContractTermID,
                    OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                    ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID.HasValue ? c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID.Value : -1,
                    PartnerID = c.OPS_Container.ORD_Container.PartnerID > 0 ? c.OPS_Container.ORD_Container.CUS_Partner.PartnerID : -1,
                }).ToList();

                var lstContainerID = result.ListOPSContainer.Select(c => c.OPSContainerID).Distinct().ToList();
                result.ListContainer = model.OPS_Container.Where(c => lstContainerID.Contains(c.ID)).Select(c => new HelperFinance_OPSContainer
                {
                    ID = c.ID,
                    OrderContainerID = c.ContainerID,
                    OrderID = c.ORD_Container.OrderID,
                    OPSContainerID = c.ID,
                    PackingID = c.ORD_Container.PackingID,
                    PackingName = c.ORD_Container.CAT_Packing.Code,
                    ContractID = c.ORD_Container.ORD_Order.ContractID > 0 ? c.ORD_Container.ORD_Order.ContractID.Value : -1,
                    CUSRoutingID = c.ORD_Container.CUSRoutingID > 0 ? c.ORD_Container.CUSRoutingID.Value : -1,
                    CATRoutingID = c.ORD_Container.CUSRoutingID > 0 ? c.ORD_Container.CUS_Routing.RoutingID : -1,
                    ParentRoutingID = c.ORD_Container.CUSRoutingID > 0 && c.ORD_Container.CUS_Routing.CAT_Routing.ParentID > 0 ? c.ORD_Container.CUS_Routing.CAT_Routing.ParentID.Value : -1,
                    LocationFromID = c.ORD_Container.LocationFromID > 0 ? c.ORD_Container.CUS_Location2.LocationID : -1,
                    LocationToID = c.ORD_Container.LocationToID > 0 ? c.ORD_Container.CUS_Location3.LocationID : -1,
                    LocationDepotID = c.ORD_Container.LocationDepotID > 0 ? c.ORD_Container.LocationDepotID.Value : -1,
                    LocationDepotReturnID = c.ORD_Container.LocationDepotReturnID > 0 ? c.ORD_Container.LocationDepotReturnID.Value : -1,
                    CustomerID = c.ORD_Container.ORD_Order.CustomerID,
                    OrderCode = c.ORD_Container.ORD_Order.Code,
                    ContractTermID = c.ORD_Container.ORD_Order.ContractTermID,
                    ServiceOfOrderID = c.ORD_Container.ORD_Order.ServiceOfOrderID.HasValue ? c.ORD_Container.ORD_Order.ServiceOfOrderID.Value : -1,
                    PartnerID = c.ORD_Container.PartnerID > 0 ? c.ORD_Container.CUS_Partner.PartnerID : -1,
                }).ToList();

                var ListDocumentContainer = model.ORD_DocumentContainer.Where(c => c.ORD_Document.DocumentStatusID == -(int)SYSVarType.DocumentStatusComplete && c.ORD_Document.ContractVendorID > 0 && result.ListContractID.Contains(c.ORD_Document.ContractVendorID.Value) && c.ORD_Document.DateConfigVendor == DateConfig && c.ORD_Container.ORD_ContainerService.Count > 0).Select(c => new HelperFinance_DocumentContainer
                {
                    DocumentID = c.DocumentID,
                    ContainerID = c.ContainerID,
                    ContractCustomerID = c.ORD_Document.ContractCustomerID,
                    ContractVendorID = c.ORD_Document.ContractVendorID,
                    CustomerID = c.ORD_Document.ORD_Order.CustomerID,
                    DateConfigCustomer = c.ORD_Document.DateConfigCustomer.Value,
                    VendorID = c.ORD_Document.VendorID,
                    OrderID = c.ORD_Container.OrderID,
                    PackingID = c.ORD_Container.PackingID
                }).ToList();

                foreach (var item in ListDocumentContainer)
                {
                    var opsCon = model.OPS_COTOContainer.FirstOrDefault(c => c.OPS_Container.ContainerID == item.ContainerID && (c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerEXLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerIMLaden || c.StatusOfCOContainerID == -(int)SYSVarType.StatusOfCOContainerLOLaden));
                    if (opsCon != null)
                    {
                        item.ListService = new List<HelperFinance_ContainerService>();
                        item.ListService = model.ORD_ContainerService.Where(c => c.ContainerID == item.ContainerID).Select(c => new HelperFinance_ContainerService
                        {
                            ServiceID = c.ServiceID,
                            ServiceCode = c.CAT_Service.Code,
                            PriceCustomer = c.PriceCustomer,
                            PriceVendor = c.PriceVendor
                        }).ToList();

                        item.OPSContainerID = opsCon.ID;
                        result.ListDocumentContainer.Add(item);
                    }
                }
                result.ListDocumentService = model.ORD_DocumentService.Where(c => c.ORD_Document.DocumentStatusID == -(int)SYSVarType.DocumentStatusComplete && c.ORD_Document.ContractVendorID > 0 && result.ListContractID.Contains(c.ORD_Document.ContractVendorID.Value) && c.ORD_Document.DateConfigVendor == DateConfig).Select(c => new HelperFinance_DocumentService
                {
                    DocumentID = c.DocumentID,
                    ServiceID = c.ServiceID,
                    ServiceCode = c.CAT_Service.Code,
                    ContractCustomerID = c.ORD_Document.ContractCustomerID,
                    ContractVendorID = c.ORD_Document.ContractVendorID,
                    CustomerID = c.ORD_Document.ORD_Order.CustomerID,
                    DateConfigCustomer = c.ORD_Document.DateConfigCustomer.Value,
                    VendorID = c.ORD_Document.VendorID,
                    PriceCustomer = c.PriceCustomer,
                    PriceVendor = c.PriceVendor
                }).ToList();
            }

            result.ListFINTemp = model.FIN_Temp.Where(c => c.ContractID.HasValue && result.ListContractID.Contains(c.ContractID.Value)).Select(c => new HelperFinance_FINTemp
            {
                ContractID = c.ContractID,
                COTOContainerID = c.COTOContainerID,
                DITOGroupProductID = c.DITOGroupProductID,
                DITOMasterID = c.DITOMasterID,
                PriceDIExID = c.PriceDIExID,
                PriceDIMOQID = c.PriceDIMOQID,
                PriceDIMOQLoadID = c.PriceDIMOQLoadID,
                ScheduleID = c.ScheduleID,
                PriceCOExID = c.PriceCOExID
            }).ToList();

            var lstMasterID = result.ListOPSContainer.Select(c => c.COTOMasterID).Distinct().ToList();
            result.ListTrouble = model.CAT_Trouble.Where(c => c.COTOMasterID > 0 && lstMasterID.Contains(c.COTOMasterID.Value) && (isCredit ? c.CostOfCustomer != 0 : c.CostOfVendor != 0) && c.TroubleCostStatusID == -(int)SYSVarType.TroubleCostStatusApproved && c.DriverID == null).Select(c => new HelperFinance_Trouble
            {
                ID = c.ID,
                COTOMasterID = c.COTOMasterID,
                DriverID = c.DriverID,
                VendorOfVehicleID = c.OPS_COTOMaster.VendorOfVehicleID,
                VehicleID = c.OPS_COTOMaster.VehicleID,
                ContractID = c.OPS_COTOMaster.ContractID,
                TroubleCostStatusID = c.TroubleCostStatusID,
                GroupOfTroubleID = c.GroupOfTroubleID,
                GroupOfTroubleCode = c.CAT_GroupOfTrouble.Code,
                GroupOfTroubleName = c.CAT_GroupOfTrouble.Name,
                CostOfCustomer = c.CostOfCustomer,
                CostOfVendor = c.CostOfVendor,
                Description = c.Description
            }).ToList();

            result.ListServiceOfOrder = model.CAT_ServiceOfOrder.Select(c => new CATServiceOfOrder
            {
                ID = c.ID,
                Code = c.Code,
                ServiceOfOrderID = c.ServiceOfOrderID
            }).ToList();
            #endregion

            #region Lấy giá
            if (result.ListOPSContainer.Count > 0)
            {
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
                    ServiceOfOrderID = c.CAT_ServiceOfOrder.ServiceOfOrderID
                }).ToList();

                foreach (var itemContractTerm in result.ListContractTerm)
                {
                    result.ListContainerPrice.AddRange(model.CAT_PriceCOContainer.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceContainer
                    {
                        ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_Price.ContractTermID,
                        EffectDate = c.CAT_Price.EffectDate,
                        PackingID = c.PackingID,
                        Price = c.Price,
                        PriceID = c.PriceID,
                        RoutingID = c.CAT_ContractRouting.RoutingID,
                        LocationFromID = c.CAT_ContractRouting.CAT_Routing.LocationFromID,
                        LocationToID = c.CAT_ContractRouting.CAT_Routing.LocationToID,
                        AreaFromID = c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID,
                        AreaToID = c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID,
                        ServiceOfOrderID = c.CAT_Price.CAT_ContractTerm.ServiceOfOrderID > 0 ? c.CAT_Price.CAT_ContractTerm.ServiceOfOrderID.Value : -1,
                    }).ToList());

                    result.ListContainerService.AddRange(model.CAT_PriceCOService.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceService
                    {
                        ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_Price.ContractTermID,
                        EffectDate = c.CAT_Price.EffectDate,
                        PriceID = c.PriceID,
                        PackingID = c.PackingID,
                        Price = c.Price,
                        ServiceID = c.ServiceID,
                        CurrencyID = c.CurrencyID
                    }).ToList());

                    result.ListEx.AddRange(model.CAT_PriceCOEx.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceEx
                    {
                        ID = c.ID,
                        ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_Price.ContractTermID,
                        ServiceOfOrderID = c.CAT_Price.CAT_ContractTerm.ServiceOfOrderID,
                        PriceID = c.PriceID,
                        COExSumID = c.COExSumID,
                        Note = c.Note,
                        ExprInput = c.ExprInput,
                        ExprPrice = c.ExprPrice,
                        ExprPriceFix = c.ExprPriceFix,
                        ExprQuan = c.ExprQuan,
                        TypeOfPriceCOExID = c.TypeOfPriceCOExID,
                        TypeOfPriceCOExCode = c.CAT_TypeOfPriceCOEx.Code,
                        TypeOfPriceCOExName = c.CAT_TypeOfPriceCOEx.TypeName,
                        EffectDate = c.CAT_Price.EffectDate,
                    }).ToList());

                    foreach (var itemMOQ in result.ListEx.Where(c => c.ContractTermID == itemContractTerm.ContractTermID))
                    {
                        itemMOQ.ListGroupOfLocation = new List<int>();
                        itemMOQ.ListContainer = new List<HelperFinance_PriceMOQContainer>();
                        itemMOQ.ListLocation = new List<HelperFinance_PriceMOQLocation>();
                        itemMOQ.ListParentRouting = new List<int>();
                        itemMOQ.ListRouting = new List<int>();
                        itemMOQ.ListPartnerID = new List<int>();

                        itemMOQ.ListContainer = model.CAT_PriceCOExContainer.Where(c => c.PriceCOExID == itemMOQ.ID).Select(c => new HelperFinance_PriceMOQContainer
                        {
                            PackingID = c.PackingID,
                            ExprPrice = c.ExprPrice,
                            ExprQuantity = c.ExprQuantity
                        }).ToList();
                        itemMOQ.ListLocation = model.CAT_PriceCOExRouting.Where(c => c.PriceCOExID == itemMOQ.ID && c.LocationID.HasValue && c.TypeOfTOLocationID.HasValue).Select(c => new HelperFinance_PriceMOQLocation
                        {
                            LocationID = c.LocationID.Value,
                            TypeOfTOLocationID = c.TypeOfTOLocationID.Value
                        }).ToList();
                        itemMOQ.ListParentRouting = model.CAT_PriceCOExRouting.Where(c => c.PriceCOExID == itemMOQ.ID && c.ParentRoutingID.HasValue).Select(c => c.ParentRoutingID.Value).Distinct().ToList();
                        itemMOQ.ListRouting = model.CAT_PriceCOExRouting.Where(c => c.PriceCOExID == itemMOQ.ID && c.RoutingID.HasValue).Select(c => c.RoutingID.Value).Distinct().ToList();
                        itemMOQ.ListPartnerID = model.CAT_PriceCOExRouting.Where(c => c.PriceCOExID == itemMOQ.ID && c.PartnerID.HasValue).Select(c => c.PartnerID.Value).Distinct().ToList();
                    }
                }

            }
            #endregion

            return result;
        }


        private static void DIPriceFLM_FindOrderCO(FIN_PL pl, FIN_PLDetails plDetail, List<FIN_Temp> lstPlTemp, IEnumerable<FIN_Temp> lstPlTempQuery, List<int> lstOPSContainerID, List<HelperFinance_Container> lstOPSContainer, int scheduleID, int masterID)
        {
            FIN_PLContainer plContainer = new FIN_PLContainer();
            plContainer.CreatedBy = pl.CreatedBy;
            plContainer.CreatedDate = DateTime.Now;

            //var plTemp = lstPlTempQuery.OrderByDescending(c => c.CreatedDate).FirstOrDefault(c => c.ScheduleID == scheduleID && c.COTOContainerID.HasValue && lstOPSContainerID.Contains(c.COTOContainerID.Value));
            //if (plTemp != null)
            //    plContainer.COTOContainerID = plTemp.COTOContainerID.Value;
            //else
            //{
            //    plContainer.COTOContainerID = lstOPSContainerID.FirstOrDefault();
            //    // Tạo pl Temp
            //    plTemp = new FIN_Temp();
            //    plTemp.CreatedBy = pl.CreatedBy;
            //    plTemp.CreatedDate = DateTime.Now;
            //    plTemp.ScheduleID = scheduleID;
            //    plTemp.COTOContainerID = plContainer.COTOContainerID;
            //    lstPlTemp.Add(plTemp);
            //}

            plContainer.COTOContainerID = lstOPSContainerID.FirstOrDefault();
            plDetail.FIN_PLContainer.Add(plContainer);

            var opsGroup = lstOPSContainer.FirstOrDefault(c => c.ID == plContainer.COTOContainerID);
            if (opsGroup != null)
            {
                pl.OrderID = opsGroup.OrderID;
                pl.COTOMasterID = opsGroup.COTOMasterID;
                pl.VehicleID = opsGroup.VehicleID;
                pl.VendorID = opsGroup.VendorID;
                pl.CustomerID = opsGroup.CustomerID;
            }
        }


        public static bool Expression_COCheckBool(DTOPriceCOExExpr item, string strExpr)
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
                worksheet.Cells[row, col].Value = item.Credit;
                strExp.Replace("[Credit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Debit;
                strExp.Replace("[Debit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PackingCode;
                strExp.Replace("[PackingCode]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.SortConfig;
                strExp.Replace("[SortConfig]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalOrder;
                strExp.Replace("[TotalOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPacking;
                strExp.Replace("[TotalPacking]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalSchedule;
                strExp.Replace("[TotalSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPrice;
                strExp.Replace("[UnitPrice]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMax;
                strExp.Replace("[UnitPriceMax]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMin;
                strExp.Replace("[UnitPriceMin]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.VehicleCode;
                strExp.Replace("[VehicleCode]", strCol + row);
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
                System.Diagnostics.Debug.WriteLine("Thiết lập sai công thức: " + strExpr);
                return result;
            }
        }

        private static ExcelPackage Expression_COGetPackage(string strExpr)
        {
            try
            {
                ExcelPackage result = new ExcelPackage();

                ExcelWorksheet worksheet = result.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);

                row++;
                strExp.Replace("[Credit]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Debit]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[PackingCode]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[SortConfig]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalOrder]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalPacking]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalSchedule]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[UnitPrice]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[UnitPriceMax]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[UnitPriceMin]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[VehicleCode]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static ExcelPackage Expression_COGetPackage(CAT_PriceCOEx item)
        {
            ExcelPackage result = new ExcelPackage();
            try
            {
                if (!string.IsNullOrEmpty(item.ExprQuan))
                    result = Expression_GetPackage(item.ExprQuan);
                if (!string.IsNullOrEmpty(item.ExprPrice))
                    result = Expression_GetPackage(item.ExprPrice);
                if (!string.IsNullOrEmpty(item.ExprPriceFix))
                    result = Expression_GetPackage(item.ExprPriceFix);

                return result;
            }
            catch
            {
                return result;
            }
        }

        private static decimal? Expression_COGetValue(ExcelPackage package, DTOPriceCOExExpr item)
        {
            decimal? result = null;
            try
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet 1"];
                int row = 0, col = 1;
                string strCol = "A", strRow = "";

                row++;
                worksheet.Cells[row, col].Value = item.Credit;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Debit;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PackingCode;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.SortConfig;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPacking;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalSchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPrice;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMax;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMin;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.VehicleCode;
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

        private static bool Expression_CORun(ExcelPackage package, DTOPriceCOExExpr item)
        {
            bool result = false;
            try
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet 1"];
                int row = 0, col = 1;
                string strCol = "A", strRow = "";

                row++;
                worksheet.Cells[row, col].Value = item.Credit;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Debit;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PackingCode;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.SortConfig;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPacking;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalSchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPrice;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMax;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMin;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.VehicleCode;
                strRow = strCol + row; row++;

                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                if (val == "True") result = true;
                else if (val == "False") result = false;

                return result;
            }
            catch
            {
                return false;
            }
        }

        private static decimal? COPriceEx_CalculatePrice(bool isCredit, DTOPriceCOExExpr objExpr, HelperFinance_PriceEx diEx, FIN_PL pl, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, List<int> lstCOTOContainerID, int contractID, List<HelperFinance_OPSContainer> lstOPSContainer)
        {
            decimal? priceFix = null, price = null, Quan = null;
            const int iCredit = (int)CATCostType.COTOExCredit;
            const int iDebit = (int)CATCostType.COTOExDebit;

            if (!string.IsNullOrEmpty(diEx.ExprPriceFix))
                priceFix = Expression_COGetValue(Expression_GetPackage(diEx.ExprPriceFix), objExpr);

            if (!string.IsNullOrEmpty(diEx.ExprPrice))
                price = Expression_COGetValue(Expression_GetPackage(diEx.ExprPrice), objExpr);

            if (!string.IsNullOrEmpty(diEx.ExprQuan))
                Quan = Expression_COGetValue(Expression_GetPackage(diEx.ExprQuan), objExpr);

            if (priceFix.HasValue || price.HasValue)
            {
                if (priceFix.HasValue)
                {
                    FIN_PLDetails plEx = new FIN_PLDetails();
                    plEx.CreatedBy = pl.CreatedBy;
                    plEx.CreatedDate = DateTime.Now;
                    plEx.CostID = iCredit;
                    plEx.Credit = priceFix.Value;
                    plEx.Unit = string.Empty;
                    plEx.Note = diEx.Note;
                    plEx.Note1 = diEx.TypeOfPriceCOExName;
                    plEx.TypeOfPriceDIExCode = diEx.TypeOfPriceCOExCode;
                    pl.FIN_PLDetails.Add(plEx);
                    if (!isCredit)
                    {
                        plEx.Credit = 0;
                        plEx.CostID = iDebit;
                        plEx.Debit = priceFix.Value;
                    }

                    FIN_PLContainer plGroup = new FIN_PLContainer();
                    plGroup.CreatedBy = pl.CreatedBy;
                    plGroup.CreatedDate = DateTime.Now;
                    plGroup.Quantity = 0;
                    plGroup.UnitPrice = 0;

                    //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceCOExID == diEx.ID && c.COTOContainerID.HasValue && lstCOTOContainerID.Contains(c.COTOContainerID.Value));
                    //if (plTemp != null)
                    //    plGroup.COTOContainerID = plTemp.COTOContainerID.Value;
                    //else
                    //{
                    //    plGroup.COTOContainerID = lstCOTOContainerID.FirstOrDefault();
                    //    // Tạo pl Temp
                    //    var objTemp = new FIN_Temp();
                    //    objTemp.CreatedBy = pl.CreatedBy;
                    //    objTemp.CreatedDate = DateTime.Now;
                    //    objTemp.ContractID = contractID;
                    //    objTemp.PriceCOExID = diEx.ID;
                    //    objTemp.COTOContainerID = plGroup.COTOContainerID;
                    //    lstPlTemp.Add(objTemp);
                    //}

                    plGroup.COTOContainerID = lstCOTOContainerID.FirstOrDefault();
                    plEx.FIN_PLContainer.Add(plGroup);

                    var opsGroup = lstOPSContainer.FirstOrDefault(c => c.ID == plGroup.COTOContainerID);
                    if (opsGroup != null)
                    {
                        pl.OrderID = opsGroup.OrderID;
                        pl.COTOMasterID = opsGroup.COTOMasterID;
                        pl.VehicleID = opsGroup.VehicleID;
                        pl.VendorID = opsGroup.VendorID;
                        pl.CustomerID = opsGroup.CustomerID;
                    }
                }

                if (price.HasValue && Quan.HasValue)
                {
                    FIN_PLDetails plEx = new FIN_PLDetails();
                    plEx.CreatedBy = pl.CreatedBy;
                    plEx.CreatedDate = DateTime.Now;
                    plEx.CostID = iCredit;
                    plEx.UnitPrice = price;
                    plEx.Quantity = (double)Quan.Value;
                    plEx.Note = diEx.Note;
                    plEx.Note1 = diEx.TypeOfPriceCOExName;
                    plEx.TypeOfPriceDIExCode = diEx.TypeOfPriceCOExCode;
                    plEx.Credit = (decimal)plEx.Quantity.Value * plEx.UnitPrice.Value;
                    pl.FIN_PLDetails.Add(plEx);
                    if (!isCredit)
                    {
                        plEx.Credit = 0;
                        plEx.CostID = iDebit;
                        plEx.Debit = (decimal)plEx.Quantity.Value * plEx.UnitPrice.Value;
                    }

                    FIN_PLContainer plGroup = new FIN_PLContainer();
                    plGroup.CreatedBy = pl.CreatedBy;
                    plGroup.CreatedDate = DateTime.Now;
                    plGroup.Quantity = 0;
                    plGroup.UnitPrice = 0;

                    //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceDIExID == diEx.ID && c.COTOContainerID.HasValue && lstCOTOContainerID.Contains(c.COTOContainerID.Value));
                    //if (plTemp != null)
                    //    plGroup.COTOContainerID = plTemp.COTOContainerID.Value;
                    //else
                    //{
                    //    plGroup.COTOContainerID = lstCOTOContainerID.FirstOrDefault();
                    //    // Tạo pl Temp
                    //    var objTemp = new FIN_Temp();
                    //    objTemp.CreatedBy = pl.CreatedBy;
                    //    objTemp.CreatedDate = DateTime.Now;
                    //    objTemp.ContractID = contractID;
                    //    objTemp.PriceCOExID = diEx.ID;
                    //    objTemp.COTOContainerID = plGroup.COTOContainerID;
                    //    lstPlTemp.Add(objTemp);
                    //}

                    plGroup.COTOContainerID = lstCOTOContainerID.FirstOrDefault();
                    plEx.FIN_PLContainer.Add(plGroup);
                    var opsGroup = lstOPSContainer.FirstOrDefault(c => c.ID == plGroup.COTOContainerID);
                    if (opsGroup != null)
                    {
                        pl.OrderID = opsGroup.OrderID;
                        pl.COTOMasterID = opsGroup.COTOMasterID;
                        pl.VehicleID = opsGroup.VehicleID;
                        pl.VendorID = opsGroup.VendorID;
                        pl.CustomerID = opsGroup.CustomerID;
                    }
                }
            }
            return priceFix;
        }

        private static void COPriceEx_CalculatePriceGOP(bool isCredit, DTOPriceCOExExpr objExpr, HelperFinance_PriceEx diEx, HelperFinance_PriceMOQContainer diGroupEx, FIN_PL pl, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, int opsGroupID, int contractID, List<HelperFinance_OPSContainer> lstOPSContainer)
        {
            const int iCredit = (int)CATCostType.COTOExCredit;
            const int iDebit = (int)CATCostType.COTOExDebit;

            decimal? price = null;
            if (!string.IsNullOrEmpty(diGroupEx.ExprPrice))
                price = Expression_COGetValue(Expression_GetPackage(diGroupEx.ExprPrice), objExpr);

            decimal? Quantity = null;
            if (!string.IsNullOrEmpty(diGroupEx.ExprQuantity))
                Quantity = Expression_COGetValue(Expression_GetPackage(diGroupEx.ExprQuantity), objExpr);

            if (price.HasValue && Quantity.HasValue)
            {
                FIN_PLDetails plEx = new FIN_PLDetails();
                plEx.CreatedBy = pl.CreatedBy;
                plEx.CreatedDate = DateTime.Now;
                plEx.CostID = iCredit;
                plEx.UnitPrice = price.Value;
                plEx.Quantity = (double?)Quantity.Value;
                plEx.Unit = string.Empty;
                plEx.Note = diEx.Note;
                plEx.Note1 = diEx.TypeOfPriceCOExName;
                plEx.TypeOfPriceDIExCode = diEx.TypeOfPriceCOExCode;
                plEx.Credit = plEx.UnitPrice.Value * (decimal)plEx.Quantity.Value;
                pl.FIN_PLDetails.Add(plEx);
                if (!isCredit)
                {
                    plEx.Credit = 0;
                    plEx.CostID = iDebit;
                    plEx.Debit = plEx.UnitPrice.Value * (decimal)plEx.Quantity.Value;
                }

                FIN_PLContainer plGroup = new FIN_PLContainer();
                plGroup.CreatedBy = pl.CreatedBy;
                plGroup.CreatedDate = DateTime.Now;
                plGroup.Quantity = 0;
                plGroup.UnitPrice = 0;

                //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceCOExID == diEx.ID && c.COTOContainerID.HasValue && c.COTOContainerID == opsGroupID);
                //if (plTemp != null)
                //    plGroup.COTOContainerID = plTemp.COTOContainerID.Value;
                //else
                //{
                //    plGroup.COTOContainerID = opsGroupID;

                //    // Tạo pl Temp
                //    var objTemp = new FIN_Temp();
                //    objTemp.CreatedBy = pl.CreatedBy;
                //    objTemp.CreatedDate = DateTime.Now;
                //    objTemp.ContractID = contractID;
                //    objTemp.PriceCOExID = diEx.ID;
                //    objTemp.COTOContainerID = plGroup.COTOContainerID;
                //    lstPlTemp.Add(objTemp);
                //}

                plGroup.COTOContainerID = opsGroupID;
                plEx.FIN_PLContainer.Add(plGroup);

                var opsGroup = lstOPSContainer.FirstOrDefault(c => c.ID == plGroup.COTOContainerID);
                if (opsGroup != null)
                {
                    pl.OrderID = opsGroup.OrderID;
                    pl.COTOMasterID = opsGroup.COTOMasterID;
                    pl.VehicleID = opsGroup.VehicleID;
                    pl.VendorID = opsGroup.VendorID;
                    pl.CustomerID = opsGroup.CustomerID;
                }
            }
        }

        private static void COPriceEx_FindOrder(HelperFinance_PriceEx itemEx, FIN_PL pl, FIN_PLDetails plDetail, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, List<int> lstCOTOContainerID, int contractID, List<HelperFinance_OPSContainer> lstOPSContainer, List<HelperFinance_OPSContainer> lstOrderContainerUpdate)
        {
            FIN_PLContainer plGroup = new FIN_PLContainer();
            plGroup.CreatedBy = pl.CreatedBy;
            plGroup.CreatedDate = DateTime.Now;
            plGroup.Quantity = 0;
            plGroup.UnitPrice = 0;

            //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceCOExID == itemEx.ID && c.COTOContainerID.HasValue && lstCOTOContainerID.Contains(c.COTOContainerID.Value));
            //if (plTemp != null)
            //    plGroup.COTOContainerID = plTemp.COTOContainerID.Value;
            //else
            //{
            //    plGroup.COTOContainerID = lstCOTOContainerID.FirstOrDefault();

            //    // Tạo pl Temp
            //    var objTemp = new FIN_Temp();
            //    objTemp.CreatedBy = pl.CreatedBy;
            //    objTemp.CreatedDate = DateTime.Now;
            //    objTemp.ContractID = contractID;
            //    objTemp.PriceCOExID = itemEx.ID;
            //    objTemp.COTOContainerID = plGroup.COTOContainerID;
            //    lstPlTemp.Add(objTemp);
            //}

            plGroup.COTOContainerID = lstCOTOContainerID.FirstOrDefault();
            plDetail.FIN_PLContainer.Add(plGroup);
            var opsGroup = lstOPSContainer.FirstOrDefault(c => c.ID == plGroup.COTOContainerID);
            if (opsGroup != null)
            {
                pl.OrderID = opsGroup.OrderID;
                pl.COTOMasterID = opsGroup.COTOMasterID;
                pl.VehicleID = opsGroup.VehicleID;
                pl.VendorID = opsGroup.VendorID;
                pl.CustomerID = opsGroup.CustomerID;
            }

            // Cập nhật FINSort
            string strSort = opsGroup.OrderContainerID.ToString();
            foreach (var item in lstOPSContainer.Where(c => lstCOTOContainerID.Contains(c.ID)))
            {
                if (lstOrderContainerUpdate.Count(c => c.ID == item.OrderContainerID) == 0)
                {
                    HelperFinance_OPSContainer itemGroup = new HelperFinance_OPSContainer();
                    itemGroup.ID = item.OrderContainerID;
                    if (itemGroup.ID == opsGroup.OrderContainerID)
                        itemGroup.FINSort = strSort;
                    else
                        itemGroup.FINSort = strSort + "A";

                    lstOrderContainerUpdate.Add(itemGroup);
                }
            }
        }


        #endregion
    }
}
