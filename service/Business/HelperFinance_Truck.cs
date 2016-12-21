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
        #region Public
        /// <summary>
        /// Offer doanh thu, chi phí, lợi nhuận
        /// </summary>
        /// <param name="model"></param>
        /// <param name="master"></param>
        /// <param name="lstOrder"></param>
        /// <returns></returns>
        public static DTODIAppointmentRate DITOMaster_Tendering_Offer(DataEntities model, DTODIAppointmentRate master, List<DTODIAppointmentOrder> lstOrder)
        {
            // Constant
            const int iTon = -(int)SYSVarType.PriceOfGOPTon;
            const int iCBM = -(int)SYSVarType.PriceOfGOPCBM;
            const int iTU = -(int)SYSVarType.PriceOfGOPTU;
            const int iFTL = -(int)SYSVarType.TransportModeFTL;
            const int iLTL = -(int)SYSVarType.TransportModeLTL;
            master.CreateDateTime = master.CreateDateTime.Date;
            List<int> lstCustomerID = new List<int>();

            #region Tính doanh thu
            decimal Credit = 0;
            foreach (var item in lstOrder)
            {
                var ops = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                var group = ops.ORD_GroupProduct;
                var locationFromID = group.CUS_Location.LocationID;
                var locationToID = group.CUS_Location1.LocationID;
                var order = model.ORD_Order.FirstOrDefault(c => c.ID == ops.ORD_GroupProduct.OrderID);
                if (order != null)
                {
                    lstCustomerID.Add(order.CustomerID);
                    // Lấy theo giá nhập vào
                    if (order.TypeOfContractID != -(int)SYSVarType.TypeOfContractMain && group.PriceOfGOPID.HasValue)
                    {
                        double quantity = 0;

                        if (group.PriceOfGOPID == iTon)
                            quantity = item.Ton;
                        else
                            if (group.PriceOfGOPID == iCBM)
                                quantity = item.CBM;
                            else
                                if (group.PriceOfGOPID == iTU)
                                    quantity = item.Quantity;

                        Credit += group.Price.Value * (decimal)quantity;
                    }
                    else
                    {
                        // Lấy giá theo hợp đồng
                        if (order.TypeOfContractID == -(int)SYSVarType.TypeOfContractMain && order.ContractID.HasValue && group.PriceOfGOPID.HasValue)
                        {
                            group.DateConfig = group.DateConfig.HasValue ? group.DateConfig.Value.Date : order.DateConfig.Date;
                            var effectDate = group.DateConfig;
                            var cusContract = model.CAT_Contract.FirstOrDefault(c => c.ID == order.ContractID);
                            if (cusContract != null)
                            {
                                // Lấy bảng giá theo ngày hiệu lực
                                var lstPriceID = model.CAT_Price.Where(c => c.CAT_ContractTerm.ContractID == cusContract.ID && c.EffectDate <= effectDate && c.TypeOfOrderID == order.TypeOfOrderID).OrderByDescending(c => c.EffectDate).Select(c => c.ID).ToList();
                                if (lstPriceID.Count > 0)
                                {
                                    #region Tính doanh thu v/c
                                    // Tính theo chuyến
                                    if (order.CAT_TransportMode.TransportModeID == iFTL)
                                    {
                                        var priceVehicle = model.CAT_PriceGroupVehicle.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfVehicleID == order.GroupOfVehicleID && c.CAT_ContractRouting.RoutingID == group.CUS_Routing.RoutingID);
                                        if (priceVehicle == null)
                                            priceVehicle = model.CAT_PriceGroupVehicle.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfVehicleID == order.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == locationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == locationToID);
                                        if (priceVehicle == null)
                                            priceVehicle = model.CAT_PriceGroupVehicle.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfVehicleID == order.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == locationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == locationToID));
                                        if (priceVehicle != null && priceVehicle.Price > Credit)
                                            Credit = priceVehicle.Price;
                                    }
                                    else
                                    {
                                        // Tính theo Tấn, Khối or ĐVVC
                                        if (order.CAT_TransportMode.TransportModeID == iLTL)
                                        {
                                            decimal unitPrice = 0;
                                            double totalTon = group.Ton;
                                            double totalCBM = group.CBM;
                                            double totalTU = group.Quantity;

                                            // Ktra xem có tính theo qui đổi hay ko
                                            double? exchangeQuantity = null;
                                            var exchange = model.CAT_ContractGroupOfProduct.FirstOrDefault(c => c.ContractID == order.ContractID && c.GroupOfProductID == group.GroupOfProductID && !string.IsNullOrEmpty(c.Expression));
                                            if (exchange != null)
                                            {
                                                DTOCATContractGroupOfProduct itemChange = new DTOCATContractGroupOfProduct();
                                                itemChange.Ton = totalTon;
                                                itemChange.CBM = totalCBM;
                                                itemChange.Quantity = totalTU;
                                                itemChange.Expression = exchange.Expression;
                                                exchangeQuantity = GetGroupOfProductTransfer(itemChange);
                                            }

                                            if (exchangeQuantity.HasValue)
                                            {
                                                if (group.PriceOfGOPID == iTon)
                                                    totalTon = exchangeQuantity.Value;
                                                else
                                                    if (group.PriceOfGOPID == iCBM)
                                                        totalCBM = exchangeQuantity.Value;
                                                    else
                                                        if (group.PriceOfGOPID == iTU)
                                                            totalTU = exchangeQuantity.Value;
                                            }

                                            #region Giá bậc thang
                                            if (model.CAT_ContractLevel.Count(c => c.ContractID == order.ContractID && (c.Ton > 0 || c.CBM > 0 || c.Quantity > 0)) > 0)
                                            {
                                                var priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.RoutingID == group.CUS_Routing.RoutingID && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                                                if (priceGOP == null)
                                                    priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.LocationToID && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                                                if (priceGOP == null)
                                                    priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationToID) && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                                                if (priceGOP != null)
                                                    unitPrice = (decimal)priceGOP.Price;
                                            }
                                            #endregion

                                            #region Giá thường
                                            else
                                            {
                                                var priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.RoutingID == group.CUS_Routing.RoutingID);
                                                if (priceGOP == null)
                                                    priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.LocationToID);
                                                if (priceGOP == null)
                                                    priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationToID));
                                                if (priceGOP != null)
                                                    unitPrice = priceGOP.Price;
                                            }
                                            #endregion

                                            double quantity = 0;
                                            if (group.PriceOfGOPID == iTon)
                                                quantity = item.Ton;
                                            else
                                                if (group.PriceOfGOPID == iCBM)
                                                    quantity = item.CBM;
                                                else
                                                    if (group.PriceOfGOPID == iTU)
                                                        quantity = item.Quantity;

                                            if (exchangeQuantity.HasValue)
                                            {
                                                DTOCATContractGroupOfProduct itemChange = new DTOCATContractGroupOfProduct();
                                                itemChange.Ton = item.Ton;
                                                itemChange.CBM = item.CBM;
                                                itemChange.Quantity = item.Quantity;
                                                itemChange.Expression = exchange.Expression;
                                                exchangeQuantity = GetGroupOfProductTransfer(itemChange);

                                                if (exchangeQuantity.HasValue)
                                                    quantity = exchangeQuantity.Value;
                                            }

                                            Credit += unitPrice * (decimal)quantity;
                                        }
                                    }
                                    #endregion

                                    #region Tính doanh thu bốc xếp lên/xuống
                                    // Bốc xếp lên
                                    var priceLoad = model.CAT_PriceDILoadDetail.FirstOrDefault(c => lstPriceID.Contains(c.CAT_PriceDILoad.PriceID) && c.CAT_PriceDILoad.IsLoading && c.GroupOfProductID == group.GroupOfProductID && (c.CAT_PriceDILoad.LocationID == group.CUS_Location.LocationID || c.CAT_PriceDILoad.RoutingID == group.CUS_Routing.RoutingID || (group.CUS_Routing.CAT_Routing.ParentID.HasValue && c.CAT_PriceDILoad.ParentRoutingID == group.CUS_Routing.CAT_Routing.ParentID) || (group.CUS_Location.CAT_Location.GroupOfLocationID.HasValue && c.CAT_PriceDILoad.GroupOfLocationID == group.CUS_Location.CAT_Location.GroupOfLocationID)));
                                    if (priceLoad != null)
                                    {
                                        double quantityLoad = 0;
                                        // Tính theo Tấn
                                        if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTon)
                                            quantityLoad = item.Ton;
                                        else if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPCBM)
                                            quantityLoad = item.CBM;
                                        else if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTU)
                                            quantityLoad = item.Quantity;

                                        Credit += (decimal)quantityLoad * priceLoad.Price;
                                    }

                                    // Bốc xếp xuống
                                    var priceUnLoad = model.CAT_PriceDILoadDetail.FirstOrDefault(c => lstPriceID.Contains(c.CAT_PriceDILoad.PriceID) && !c.CAT_PriceDILoad.IsLoading && c.GroupOfProductID == group.GroupOfProductID && (c.CAT_PriceDILoad.LocationID == group.CUS_Location1.LocationID || c.CAT_PriceDILoad.RoutingID == group.CUS_Routing.RoutingID || (group.CUS_Routing.CAT_Routing.ParentID.HasValue && c.CAT_PriceDILoad.ParentRoutingID == group.CUS_Routing.CAT_Routing.ParentID) || (group.CUS_Location1.CAT_Location.GroupOfLocationID.HasValue && c.CAT_PriceDILoad.GroupOfLocationID == group.CUS_Location1.CAT_Location.GroupOfLocationID)));
                                    if (priceUnLoad != null)
                                    {
                                        double quantityLoad = 0;
                                        // Tính theo Tấn
                                        if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTon)
                                            quantityLoad = item.Ton;
                                        else if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPCBM)
                                            quantityLoad = item.CBM;
                                        else if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTU)
                                            quantityLoad = item.Quantity;

                                        Credit += (decimal)quantityLoad * priceUnLoad.Price;
                                    }
                                    #endregion
                                }
                            }
                        }
                    }
                }
            }
            master.Credit = Credit;

            #endregion

            #region Tính chi phí
            decimal Debit = 0;
            lstCustomerID = lstCustomerID.Distinct().ToList();
            // Tự tìm hợp đồng theo vendor, mode, service
            var contract = model.CAT_Contract.FirstOrDefault(c => c.CustomerID == master.CreateVendorID && c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.EffectDate <= master.CreateDateTime && (c.ExpiredDate == null || c.ExpiredDate >= master.CreateDateTime) && c.TypeOfContractID == -(int)SYSVarType.TypeOfContractMain && (c.CompanyID == null || (c.CompanyID.HasValue && lstCustomerID.Contains(c.CUS_Company.CustomerRelateID))));
            if (contract != null)
            {
                // Danh sách product mapping
                var lsGroupMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == contract.CustomerID && c.SYSCustomerID == contract.SYSCustomerID).Select(c => new { c.GroupOfProductCUSID, c.GroupOfProductVENID }).ToList();
                var effectDate = master.CreateDateTime;
                // Lấy bảng giá theo ngày hiệu lực
                var lstPriceID = model.CAT_Price.Where(c => c.CAT_ContractTerm.ContractID == contract.ID && c.EffectDate <= effectDate).OrderByDescending(c => c.EffectDate).Select(c => c.ID).ToList();
                if (lstPriceID.Count > 0)
                {
                    foreach (var item in lstOrder)
                    {
                        #region Chi phí vận chuyển
                        var ops = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                        var group = ops.ORD_GroupProduct;
                        var locationFromID = group.CUS_Location.LocationID;
                        var locationToID = group.CUS_Location1.LocationID;

                        double totalTon = lstOrder.Where(c => c.OrderGroupProductID == ops.OrderGroupProductID).Sum(c => c.Ton);
                        double totalCBM = lstOrder.Where(c => c.OrderGroupProductID == ops.OrderGroupProductID).Sum(c => c.CBM);
                        double totalTU = lstOrder.Where(c => c.OrderGroupProductID == ops.OrderGroupProductID).Sum(c => c.Quantity);

                        // Nếu hợp đồng vendor tính theo đơn hàng => lấy giá theo tổng đơn hàng của tất cả chuyến đã chở xong
                        if (contract.IsVENLTLLevelOrder)
                        {
                            var lstVENGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID == ops.OrderGroupProductID);
                            if (lstVENGroupProduct != null && lstVENGroupProduct.Count() > 0)
                            {
                                totalTon = lstVENGroupProduct.Sum(c => c.TonTranfer);
                                totalCBM = lstVENGroupProduct.Sum(c => c.CBMTranfer);
                                totalTU = lstVENGroupProduct.Sum(c => c.QuantityTranfer);
                            }
                        }

                        // Ktra xem có tính theo qui đổi hay ko
                        double? exchangeQuantity = null;
                        var lstGroupMappingID = lsGroupMapping.Where(c => c.GroupOfProductCUSID == group.GroupOfProductID).Select(c => c.GroupOfProductVENID).Distinct().ToList();
                        var exchange = model.CAT_ContractGroupOfProduct.FirstOrDefault(c => c.ContractID == contract.ID && lstGroupMappingID.Contains(c.GroupOfProductID) && !string.IsNullOrEmpty(c.Expression));
                        if (exchange != null)
                        {
                            DTOCATContractGroupOfProduct itemChange = new DTOCATContractGroupOfProduct();
                            itemChange.Ton = totalTon;
                            itemChange.CBM = totalCBM;
                            itemChange.Quantity = totalTU;
                            itemChange.Expression = exchange.Expression;
                            exchangeQuantity = GetGroupOfProductTransfer(itemChange);
                        }

                        if (exchangeQuantity.HasValue)
                        {
                            if (group.PriceOfGOPID == iTon)
                                totalTon = exchangeQuantity.Value;
                            else
                                if (group.PriceOfGOPID == iCBM)
                                    totalCBM = exchangeQuantity.Value;
                                else
                                    if (group.PriceOfGOPID == iTU)
                                        totalTU = exchangeQuantity.Value;
                        }

                        decimal unitPrice = 0;
                        double quantity = 0;

                        if (model.CAT_ContractLevel.Count(c => c.ContractID == contract.ID && (c.Ton > 0 || c.CBM > 0 || c.Quantity > 0)) > 0)
                        {
                            var priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.RoutingID == group.CUS_Routing.RoutingID && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID) && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                            if (priceGOP == null)
                                priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.CUS_Location.LocationID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.CUS_Location1.LocationID && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID) && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                            if (priceGOP == null)
                                priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.CUS_Location.LocationID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.CUS_Location1.LocationID) && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID) && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                            if (priceGOP != null)
                                unitPrice = priceGOP.Price;
                        }
                        else
                        {
                            // Lấy bảng giá theo routing order
                            var priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.RoutingID == group.CUS_Routing.RoutingID && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID));
                            if (priceGOP == null)
                                // Lấy bảng giá theo location trùng vs location của order
                                priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.CUS_Location.LocationID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.CUS_Location1.LocationID && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID));
                            if (priceGOP == null)
                                // Lấy bảng giá theo khu vực có location trùng vs location của order
                                priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.CUS_Location.LocationID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.CUS_Location1.LocationID) && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID));
                            if (priceGOP != null)
                                unitPrice = priceGOP.Price;
                        }

                        if (group.PriceOfGOPID == iTon)
                            quantity = item.Ton;
                        else
                            if (group.PriceOfGOPID == iCBM)
                                quantity = item.CBM;
                            else
                                if (group.PriceOfGOPID == iTU)
                                    quantity = item.Quantity;

                        if (exchangeQuantity.HasValue)
                        {
                            DTOCATContractGroupOfProduct itemChange = new DTOCATContractGroupOfProduct();
                            itemChange.Ton = item.Ton;
                            itemChange.CBM = item.CBM;
                            itemChange.Quantity = item.Quantity;
                            itemChange.Expression = exchange.Expression;
                            exchangeQuantity = GetGroupOfProductTransfer(itemChange);

                            if (exchangeQuantity.HasValue)
                                quantity = exchangeQuantity.Value;
                        }

                        Debit += (decimal)quantity * unitPrice;
                        #endregion

                        #region Bốc xếp lên
                        // Bốc xếp lên
                        var priceLoad = model.CAT_PriceDILoadDetail.FirstOrDefault(c => lstPriceID.Contains(c.CAT_PriceDILoad.PriceID) && c.CAT_PriceDILoad.IsLoading && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID) && (c.CAT_PriceDILoad.LocationID == group.CUS_Location.LocationID || c.CAT_PriceDILoad.RoutingID == group.CUS_Routing.RoutingID || (group.CUS_Routing.CAT_Routing.ParentID.HasValue && c.CAT_PriceDILoad.ParentRoutingID == group.CUS_Routing.CAT_Routing.ParentID) || (group.CUS_Location.CAT_Location.GroupOfLocationID.HasValue && c.CAT_PriceDILoad.GroupOfLocationID == group.CUS_Location.CAT_Location.GroupOfLocationID)));
                        if (priceLoad != null)
                        {
                            double quantityLoad = 0;
                            // Tính theo Tấn
                            if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTon)
                                quantityLoad = item.Ton;
                            else if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPCBM)
                                quantityLoad = item.CBM;
                            else if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTU)
                                quantityLoad = item.Quantity;

                            Debit += (decimal)quantityLoad * priceLoad.Price;
                        }
                        #endregion

                        #region Bốc xếp xuống
                        // Bốc xếp xuống
                        var priceUnLoad = model.CAT_PriceDILoadDetail.FirstOrDefault(c => lstPriceID.Contains(c.CAT_PriceDILoad.PriceID) && !c.CAT_PriceDILoad.IsLoading && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID) && (c.CAT_PriceDILoad.LocationID == group.CUS_Location1.LocationID || c.CAT_PriceDILoad.RoutingID == group.CUS_Routing.RoutingID || (group.CUS_Routing.CAT_Routing.ParentID.HasValue && c.CAT_PriceDILoad.ParentRoutingID == group.CUS_Routing.CAT_Routing.ParentID) || (group.CUS_Location1.CAT_Location.GroupOfLocationID.HasValue && c.CAT_PriceDILoad.GroupOfLocationID == group.CUS_Location1.CAT_Location.GroupOfLocationID)));
                        if (priceUnLoad != null)
                        {
                            double quantityLoad = 0;
                            // Tính theo Tấn
                            if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTon)
                                quantityLoad = item.Ton;
                            else if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPCBM)
                                quantityLoad = item.CBM;
                            else if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTU)
                                quantityLoad = item.Quantity;

                            Debit += (decimal)quantityLoad * priceUnLoad.Price;
                        }
                        #endregion
                    }
                }
            }

            master.Debit = Debit;
            #endregion

            master.PL = master.Credit - master.Debit;
            return master;
        }

        /// <summary>
        /// Tính + lưu doanh thu, chi phí kế hoạch cho lệnh
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Account"></param>
        /// <param name="id"></param>
        public static void DITOMaster_Planning(DataEntities model, AccountItem Account, int id, int? vendorid)
        {
            #region Lệnh Xe tải
            var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == id);
            if (master != null && master.VehicleID.HasValue)
            {
                // Xóa hết PL cũ (nếu có)
                var lstOldPL = model.FIN_PL.Where(c => c.IsPlanning && c.DITOMasterID == master.ID);
                foreach (var oldPL in lstOldPL)
                {
                    foreach (var child in model.FIN_PLDetails.Where(c => c.PLID == oldPL.ID))
                    {
                        foreach (var group in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == child.ID))
                            model.FIN_PLGroupOfProduct.Remove(group);

                        model.FIN_PLDetails.Remove(child);
                    }
                    model.FIN_PL.Remove(oldPL);
                }
                // Lấy xe
                var vehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == master.VehicleID);
                // Lấy danh sách đơn hàng của lệnh
                var lstOrder = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID).Select(c => new
                {
                    OrderID = c.ORD_GroupProduct.OrderID,
                }).GroupBy(c => new { c.OrderID }).ToList();
                // Xử lý cho từng đơn hàng => mỗi đơn hàng là 1 PL
                foreach (var groupOrder in lstOrder)
                {
                    #region Doanh thu
                    // Lấy đơn hàng
                    var order = model.ORD_Order.FirstOrDefault(c => c.ID == groupOrder.Key.OrderID);
                    // Ngày áp dụng tính giá
                    DateTime effectDate = order.DateConfig;
                    // Tạo pl doanh thu rỗng
                    FIN_PL plCredit = new FIN_PL();
                    plCredit.IsPlanning = true; // kế hoạch
                    plCredit.Effdate = master.ETD.Value.Date;
                    plCredit.Code = string.Empty;
                    plCredit.CreatedBy = Account.UserName;
                    plCredit.CreatedDate = DateTime.Now;
                    plCredit.SYSCustomerID = Account.SYSCustomerID;
                    plCredit.DITOMasterID = id;
                    plCredit.OrderID = order.ID;
                    plCredit.VehicleID = master.VehicleID;
                    plCredit.Credit = plCredit.Debit = 0;
                    plCredit.CustomerID = order.CustomerID;
                    plCredit.VendorID = master.VendorOfVehicleID;
                    plCredit.FINPLTypeID = -(int)SYSVarType.FINPLTypePlan;

                    model.FIN_PL.Add(plCredit);

                    // Doanh thu cước vận chuyển phân phối
                    bool isFixPrice = false;
                    FIN_PLDetails plCostCredit = new FIN_PLDetails();
                    plCostCredit.CreatedBy = Account.UserName;
                    plCostCredit.CreatedDate = DateTime.Now;
                    plCostCredit.CostID = (int)CATCostType.DITOFreightCredit;
                    plCostCredit.Debit = 0;
                    plCostCredit.Credit = DITOMaster_OfferCreditByOrder(model, Account, master, order, effectDate, plCostCredit, ref isFixPrice);
                    plCredit.FIN_PLDetails.Add(plCostCredit);

                    // Doanh thu bốc xếp
                    decimal creditLoad = DITOMaster_OfferCreditLoadByOrder(model, Account, master, order, effectDate, plCredit);


                    // Tính doanh thu theo đơn hàng
                    plCredit.Credit = plCostCredit.Credit + creditLoad;
                    #endregion
                }

                #region Chi phí
                decimal debit = 0;
                decimal debitLoad = 0;

                // Chi phí
                FIN_PL plDebit = new FIN_PL();
                plDebit.IsPlanning = true;
                plDebit.Effdate = master.ETD.Value.Date;
                plDebit.Code = string.Empty;
                plDebit.CreatedBy = Account.UserName;
                plDebit.CreatedDate = DateTime.Now;
                plDebit.DITOMasterID = id;
                plDebit.Debit = plDebit.Credit = 0;
                plDebit.VehicleID = master.VehicleID;
                plDebit.SYSCustomerID = Account.SYSCustomerID;
                plDebit.DriverID = master.DriverID1;
                plDebit.VendorID = master.VendorOfVehicleID;
                plDebit.FINPLTypeID = -(int)SYSVarType.FINPLTypePlan;

                model.FIN_PL.Add(plDebit);

                // Chi phí cước vận chuyển phân phối
                bool isFixPriceVEN = false;
                FIN_PLDetails plCostDebit = new FIN_PLDetails();
                plCostDebit.CreatedBy = Account.UserName;
                plCostDebit.CreatedDate = DateTime.Now;
                plCostDebit.CostID = (int)CATCostType.DITOFreightDebit;
                plCostDebit.Credit = 0;
                plDebit.FIN_PLDetails.Add(plCostDebit);

                if (vendorid == null || vendorid == Account.SYSCustomerID)
                {
                    debit = 0;
                }
                else
                {
                    // Lấy debit theo rate được accept
                    var rate = model.OPS_DITORate.FirstOrDefault(c => c.IsAccept == true && c.DITOMasterID == master.ID && c.IsManual);
                    if (rate != null)
                        debit = rate.Debit;
                    else
                    {
                        rate = model.OPS_DITORate.Local.FirstOrDefault(c => c.IsAccept == true && c.DITOMasterID == master.ID && c.IsManual);
                        if (rate != null)
                            debit = rate.Debit;
                        else
                        {
                            DateTime EffectDate = master.DateConfig.HasValue ? master.DateConfig.Value.Date : master.ETD.Value.Date;
                            var contract = model.CAT_Contract.FirstOrDefault(c => c.ID == master.ContractID);
                            if (contract == null) // Tự tìm hợp đồng theo vendor, mode, service
                                contract = GetContractByMasterID(model, master.ID, EffectDate);
                            if (contract != null)
                            {
                                master.ContractID = contract.ID;
                                // Chi phí vận chuyển
                                debit = DITOMaster_OfferDebit(model, master, plCostDebit, plDebit, ref isFixPriceVEN);
                                plCostDebit.Debit = debit;
                                // Chi phí bốc xếp
                                debitLoad = DITOMaster_OfferDebitLoadByContract(model, master, contract.ID, plDebit);
                            }
                        }
                    }
                }


                // Tính chi phí
                plDebit.Debit = plCostDebit.Debit + debitLoad;
                #endregion
            }
            #endregion
        }

        /// <summary>
        /// Tính + lưu doanh thu, chi phí thực tế theo lệnh (sau khi hoàn tất chuyến)
        /// </summary>
        /// <param name="model"></param>
        /// <param name="Account"></param>
        /// <param name="id"></param>
        public static void DITOMaster_POD(DataEntities model, AccountItem Account, int id, out bool isFixPriceCUS, out bool isFixPriceVEN)
        {
            var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == id);
            isFixPriceVEN = false;
            isFixPriceCUS = false;
            if (master != null && master.VehicleID.HasValue)
            {
                #region Doanh thu - Chi phí
                // Xóa hết PL cũ (nếu có)
                var lstOldPL = model.FIN_PL.Where(c => !c.IsPlanning && c.DITOMasterID == master.ID);
                foreach (var oldPL in lstOldPL)
                {
                    foreach (var child in model.FIN_PLDetails.Where(c => c.PLID == oldPL.ID))
                    {
                        foreach (var group in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == child.ID))
                            model.FIN_PLGroupOfProduct.Remove(group);

                        model.FIN_PLDetails.Remove(child);
                    }
                    model.FIN_PL.Remove(oldPL);
                }
                // Lấy xe
                var vehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == master.VehicleID);
                // Lấy danh sách đơn hàng của lệnh
                var lstOrder = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID).Select(c => new
                {
                    OrderID = c.ORD_GroupProduct.OrderID,
                }).GroupBy(c => new { c.OrderID }).ToList();
                // Xử lý cho từng đơn hàng => mỗi đơn hàng là 1 PL
                DateTime dtEffectDate = DateTime.Now;
                // Ds khách hàng
                List<int> lstCustomerID = new List<int>();
                foreach (var groupOrder in lstOrder)
                {
                    #region Doanh thu
                    // Lấy đơn hàng
                    var order = model.ORD_Order.FirstOrDefault(c => c.ID == groupOrder.Key.OrderID);
                    if (!lstCustomerID.Contains(order.CustomerID))
                        lstCustomerID.Add(order.CustomerID);
                    // Ngày áp dụng tính giá
                    DateTime effectDate = order.DateConfig;
                    // Tạo pl doanh thu rỗng
                    FIN_PL plCredit = new FIN_PL();
                    plCredit.IsPlanning = false; // Thực tế
                    plCredit.Effdate = order.DateConfig.Date;
                    plCredit.Code = string.Empty;
                    plCredit.CreatedBy = Account.UserName;
                    plCredit.CreatedDate = DateTime.Now;
                    plCredit.SYSCustomerID = Account.SYSCustomerID;
                    plCredit.DITOMasterID = master.ID;
                    plCredit.OrderID = order.ID;
                    plCredit.VehicleID = master.VehicleID;
                    plCredit.Credit = plCredit.Debit = 0;
                    plCredit.CustomerID = order.CustomerID;
                    plCredit.VendorID = master.VendorOfVehicleID;
                    plCredit.ContractID = order.ContractID;
                    plCredit.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                    model.FIN_PL.Add(plCredit);
                    // Doanh thu cước vận chuyển phân phối
                    bool isFixPrice = false;
                    FIN_PLDetails plCostCredit = new FIN_PLDetails();
                    plCostCredit.CreatedBy = Account.UserName;
                    plCostCredit.CreatedDate = DateTime.Now;
                    plCostCredit.CostID = (int)CATCostType.DITOFreightCredit;
                    plCostCredit.Debit = 0;
                    plCostCredit.Credit = DITOMaster_OfferCreditByOrder(model, Account, master, order, effectDate, plCostCredit, ref isFixPrice);
                    plCredit.FIN_PLDetails.Add(plCostCredit);
                    if (isFixPrice)
                        isFixPriceCUS = true;
                    // Doanh thu bốc xếp
                    decimal creditLoad = DITOMaster_OfferCreditLoadByOrder(model, Account, master, order, effectDate, plCredit);

                    // Doanh thu chi hộ theo đơn hàng
                    decimal creditCostingCredit = 0; // Chi hộ
                    var lstCostingCredit = model.FIN_PLCosting.Where(c => c.DITOMasterID == master.ID && c.OrderID == order.ID && c.CostID == (int)CATCostType.PLCostingCredit);
                    if (lstCostingCredit.Count() > 0)
                    {
                        FIN_PLDetails plCostingCredit = new FIN_PLDetails();
                        plCostingCredit.CreatedBy = Account.UserName;
                        plCostingCredit.CreatedDate = DateTime.Now;
                        plCostingCredit.CostID = (int)CATCostType.PLCostingCredit;
                        plCostingCredit.Credit = creditCostingCredit = lstCostingCredit.Sum(c => c.Amount);
                        plCredit.FIN_PLDetails.Add(plCostingCredit);
                    }

                    // Phân bổ chi phí Trouble
                    decimal creditTrouble = 0;
                    var lstCreditTrouble = model.CAT_Trouble.Where(c => c.DITOMasterID == master.ID && c.CostOfCustomer != 0 && c.TroubleCostStatusID == -(int)SYSVarType.TroubleCostStatusApproved);
                    if (lstCreditTrouble.Count() > 0)
                    {
                        FIN_PLDetails plCreditTrouble = new FIN_PLDetails();
                        plCreditTrouble.CreatedBy = Account.UserName;
                        plCreditTrouble.CreatedDate = DateTime.Now;
                        plCreditTrouble.CostID = (int)CATCostType.TroubleCredit;
                        plCreditTrouble.Debit = 0;
                        plCreditTrouble.Credit = creditTrouble = lstCreditTrouble.Sum(c => c.CostOfCustomer);
                        plCredit.FIN_PLDetails.Add(plCreditTrouble);
                    }


                    // Tính doanh thu theo đơn hàng
                    plCredit.Credit = plCostCredit.Credit + creditCostingCredit + creditLoad + creditTrouble;
                    #endregion
                }

                #region Chi phí
                decimal debitDriver = 0; // Chi phí tài xế
                decimal debitTrouble = 0; // trouble
                decimal debitCostingDebit = 0; // thu hộ
                decimal debitTOMasterCost = 0; // hành trình
                decimal debit = 0;
                decimal debitLoad = 0;
                // Chi phí
                FIN_PL plDebit = new FIN_PL();
                plDebit.IsPlanning = false;
                plDebit.Effdate = master.DateConfig.HasValue ? master.DateConfig.Value.Date : master.ETD.Value.Date;
                plDebit.Code = string.Empty;
                plDebit.CreatedBy = Account.UserName;
                plDebit.CreatedDate = DateTime.Now;
                plDebit.DITOMasterID = master.ID;
                plDebit.Debit = plDebit.Credit = 0;
                plDebit.VehicleID = master.VehicleID;
                plDebit.SYSCustomerID = Account.SYSCustomerID;
                plDebit.VendorID = master.VendorOfVehicleID;
                plDebit.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                model.FIN_PL.Add(plDebit);

                // Chi phí cước vận chuyển phân phối
                FIN_PLDetails plCostDebit = new FIN_PLDetails();
                plCostDebit.CreatedBy = Account.UserName;
                plCostDebit.CreatedDate = DateTime.Now;
                plCostDebit.CostID = (int)CATCostType.DITOFreightDebit;
                plCostDebit.Credit = 0;
                plCostDebit.Debit = debit;
                plDebit.FIN_PLDetails.Add(plCostDebit);

                // Nếu chi phí đã được điều chỉnh => giữ nguyên
                var rate = model.OPS_DITORate.FirstOrDefault(c => c.IsAccept == true && c.DITOMasterID == master.ID && c.IsManual);
                if (rate != null)
                {
                    debit = rate.Debit;
                }
                else
                {
                    var contract = model.CAT_Contract.FirstOrDefault(c => c.ID == master.ContractID);
                    if (contract == null) // Tự tìm hợp đồng theo vendor, mode, service
                        contract = GetContractByMasterID(model, master.ID, master.DateConfig.HasValue ? master.DateConfig.Value.Date : master.ETD.Value.Date);
                    if (contract != null)
                    {
                        plDebit.ContractID = contract.ID;
                        master.ContractID = contract.ID;
                        // Tính lại chi phí
                        debit = DITOMaster_OfferDebit(model, master, plCostDebit, plDebit, ref isFixPriceVEN);
                        // Chi phí bốc xếp
                        debitLoad = DITOMaster_OfferDebitLoadByContract(model, master, contract.ID, plDebit);
                        // Chi phí phụ thu khác
                        DateTime masterEffectDate = master.DateConfig.HasValue ? master.DateConfig.Value.Date : master.ETD.Value.Date;
                        debit += DITOMaster_GetPriceExByContract(model, Account, plDebit, master, DITOMaster_GetGroupOfProduct(model, master), master.ContractID, masterEffectDate, master.TypeOfOrderID);
                    }
                }
                plCostDebit.Debit = debit;

                if (master.TransportModeID > 0 && master.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFTL)
                {
                    var lstOrderID = lstOrder.Select(c => c.Key.OrderID).Distinct().ToList();
                    int TotalOrder = lstOrderID.Count();
                    if (TotalOrder == 1)
                        plDebit.OrderID = lstOrderID.FirstOrDefault();
                    else
                    {
                    }
                }

                // Phân bổ chi phí Trouble
                var lstTrouble = model.CAT_Trouble.Where(c => c.DITOMasterID == master.ID && c.CostOfVendor != 0 && c.TroubleCostStatusID == -(int)SYSVarType.TroubleCostStatusApproved);
                if (lstTrouble.Count() > 0)
                {
                    FIN_PLDetails plCostTrouble = new FIN_PLDetails();
                    plCostTrouble.CreatedBy = Account.UserName;
                    plCostTrouble.CreatedDate = DateTime.Now;
                    plCostTrouble.CostID = (int)CATCostType.TroubleDebit;
                    plCostTrouble.Credit = 0;
                    plCostTrouble.Debit = debitTrouble = lstTrouble.Sum(c => c.CostOfVendor);
                    plDebit.FIN_PLDetails.Add(plCostTrouble);
                }

                // Tính chi phí
                plDebit.Debit = debitTrouble + debitCostingDebit + debitTOMasterCost + debitLoad;
                #endregion
                #endregion
            }
        }



        public static void Truck_TenderedSchedule(DataEntities model, AccountItem Account, int tomasterid)
        {
            var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == tomasterid);
            if (master != null && (master.VendorOfVehicleID == null || master.VendorOfVehicleID == Account.SYSCustomerID))
            {

            }
        }

        public static void Truck_CompleteSchedule(DataEntities model, AccountItem Account, int tomasterid, int? digroupproductid = null)
        {
            #region Cập nhật cho group
            if (digroupproductid > 0)
            {
                var group = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == digroupproductid);
                if (group != null && group.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusComplete)
                {
                    // Thay đổi trạng thái lệnh
                    group.ModifiedBy = Account.UserName;
                    group.ModifiedDate = DateTime.Now;
                    group.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                    model.SaveChanges();

                    if (model.OPS_DITOGroupProduct.Count(c => c.DITOMasterID == group.DITOMasterID && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete) == model.OPS_DITOGroupProduct.Count(c => c.DITOMasterID == group.DITOMasterID))
                        tomasterid = group.DITOMasterID.Value;
                }
            }
            #endregion

            #region Cập nhật ATA, ATD cho chuyến
            var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == tomasterid);
            if (master != null)
            {
                if (master.ATD == null)
                {
                    var location = model.OPS_DITOLocation.Where(c => c.DITOMasterID == master.ID && c.DateCome != null && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet).OrderBy(c => c.DateCome).Select(c => new { c.DateCome }).FirstOrDefault();
                    if (location != null)
                        master.ATD = location.DateCome;
                    else
                        master.ATD = master.ETD;
                }

                if (master.ATA == null)
                {
                    //var location = model.OPS_DITOLocation.Where(c => c.DITOMasterID == master.ID && c.DateCome != null && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery).OrderByDescending(c => c.DateCome).Select(c => new { c.DateCome }).FirstOrDefault();
                    //if (location != null)
                    //    master.ATA = location.DateCome;
                    //else
                    master.ATA = DateTime.Now;
                }

                master.DateReceived = master.ATA;
            }
            model.SaveChanges();
            #endregion

            #region Cập nhật ngày tính giá cho đơn hàng
            var lstOrderID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == tomasterid).Select(c => c.ORD_GroupProduct.OrderID).Distinct().ToList();
            foreach (var orderID in lstOrderID)
            {
                var order = model.ORD_Order.FirstOrDefault(c => c.ID == orderID);
                if (order != null)
                {
                    var contract = model.CAT_Contract.FirstOrDefault(c => c.ID == order.ContractID);
                    // Mặc định theo ngày gửi yêu cầu
                    order.DateConfig = order.RequestDate.Date;
                    // Danh sách chuyến chạy đơn hàng này
                    var lstMasterDate = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && c.ORD_GroupProduct.OrderID == order.ID && (c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived || c.DITOMasterID == tomasterid) && c.OPS_DITOMaster.ETD.HasValue && c.OPS_DITOMaster.ETA.HasValue).Select(c => new { c.OPS_DITOMaster.ETD, c.OPS_DITOMaster.ETA, c.OPS_DITOMaster.ATA, c.OPS_DITOMaster.ATD, c.OPS_DITOMaster.DateReceived, c.OrderGroupProductID }).Distinct().ToList();
                    if (lstMasterDate != null && lstMasterDate.Count > 0)
                    {
                        #region Tính cho đơn hàng
                        // Ngày yêu cầu lấy hàng
                        if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETDRequest && order.ETDRequest.HasValue)
                            order.DateConfig = order.ETDRequest.Value.Date;
                        // Ngày yêu cầu giao hàng
                        if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETARequest && order.ETARequest.HasValue)
                            order.DateConfig = order.ETARequest.Value.Date;
                        // Tính theo ngày đến kho sớm nhất
                        if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETD)
                            order.DateConfig = lstMasterDate.OrderBy(c => c.ETD).FirstOrDefault().ETD.Value.Date;
                        // Tính theo ngày giao hàng sớm nhất
                        if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATA)
                        {
                            var masterCheck = lstMasterDate.Where(c => c.ATA.HasValue).OrderBy(c => c.ATA).FirstOrDefault();
                            if (masterCheck != null)
                                order.DateConfig = masterCheck.ATA.Value.Date;
                        }
                        // Tính theo ngày giao hàng sớm nhất
                        if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATD)
                        {
                            var masterCheck = lstMasterDate.Where(c => c.ATD.HasValue).OrderBy(c => c.ATD).FirstOrDefault();
                            if (masterCheck != null)
                                order.DateConfig = masterCheck.ATD.Value.Date;
                        }
                        // Tính theo ngày giao hàng sớm nhất
                        if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateComplete)
                        {
                            var masterCheck = lstMasterDate.Where(c => c.DateReceived.HasValue).OrderBy(c => c.DateReceived).FirstOrDefault();
                            if (masterCheck != null)
                                order.DateConfig = masterCheck.DateReceived.Value.Date;
                        }
                        #endregion

                        #region Tính cho từng chi tiết
                        foreach (var orderGroup in model.ORD_GroupProduct.Where(c => c.OrderID == order.ID))
                        {
                            orderGroup.PriceOfGOPID = orderGroup.CUS_GroupOfProduct.PriceOfGOPID;
                            orderGroup.DateConfig = null;
                            // Ngày yêu cầu lấy hàng
                            if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETDRequest && orderGroup.ETDRequest.HasValue)
                                orderGroup.DateConfig = orderGroup.ETDRequest.Value.Date;
                            // Ngày yêu cầu giao hàng
                            if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETARequest && orderGroup.ETARequest.HasValue)
                                orderGroup.DateConfig = orderGroup.ETARequest.Value.Date;
                            // Tính theo ngày đến kho sớm nhất
                            if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETD)
                            {
                                var masterCheck = lstMasterDate.Where(c => c.OrderGroupProductID == orderGroup.ID && c.ETD.HasValue).OrderBy(c => c.ETD).FirstOrDefault();
                                if (masterCheck != null)
                                    orderGroup.DateConfig = masterCheck.ETD.Value.Date;
                            }
                            // Tính theo ngày giao hàng sớm nhất
                            if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATA)
                            {
                                var masterCheck = lstMasterDate.Where(c => c.OrderGroupProductID == orderGroup.ID && c.ATA.HasValue).OrderBy(c => c.ATA).FirstOrDefault();
                                if (masterCheck != null)
                                    orderGroup.DateConfig = masterCheck.ATA.Value.Date;
                            }
                            // Tính theo ngày giao hàng sớm nhất
                            if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATD)
                            {
                                var masterCheck = lstMasterDate.Where(c => c.OrderGroupProductID == orderGroup.ID && c.ATD.HasValue).OrderBy(c => c.ATD).FirstOrDefault();
                                if (masterCheck != null)
                                    orderGroup.DateConfig = masterCheck.ATD.Value.Date;
                            }
                            // Tính theo ngày giao hàng sớm nhất
                            if (contract != null && contract.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateComplete)
                            {
                                var masterCheck = lstMasterDate.Where(c => c.OrderGroupProductID == orderGroup.ID && c.DateReceived.HasValue).OrderBy(c => c.DateReceived).FirstOrDefault();
                                if (masterCheck != null)
                                    orderGroup.DateConfig = masterCheck.DateReceived.Value.Date;
                            }
                            if (orderGroup.DateConfig == null)
                                orderGroup.DateConfig = order.DateConfig;
                        }
                        #endregion
                    }
                    else
                    {
                        foreach (var orderGroup in model.ORD_GroupProduct.Where(c => c.OrderID == order.ID))
                        {
                            orderGroup.PriceOfGOPID = orderGroup.CUS_GroupOfProduct.PriceOfGOPID;
                            orderGroup.DateConfig = orderGroup.DateConfig;
                        }
                    }
                }
            }
            model.SaveChanges();
            #endregion

            #region Cập nhật thông tin chuyến, hợp đồng, KPI, đơn hàng
            List<HelperFinance_Contract> lstContractVENLoad = new List<HelperFinance_Contract>();
            if (master != null)
            {
                #region Cập nhật thông tin đơn hàng
                var lstGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID).ToList();
                master.TextCustomerCode = string.Empty;
                master.TextCustomerName = string.Empty;
                master.TextGroupLocationCode = string.Empty;

                master.TextCustomerCode = string.Join(", ", lstGroup.Select(c => c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code).Distinct().ToList());
                master.TextCustomerName = string.Join(", ", lstGroup.Select(c => c.ORD_GroupProduct.ORD_Order.CUS_Customer.ShortName).Distinct().ToList());
                master.TextGroupLocationCode = string.Join(", ", lstGroup.Where(c => c.ORD_GroupProduct.LocationToID > 0 && c.ORD_GroupProduct.CUS_Location1.CAT_Location.GroupOfLocationID > 0).Select(c => c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_GroupOfLocation.Code).Distinct().ToList());
                #endregion

                #region Cập nhật loại xe
                if (master.GroupOfVehicleID == null)
                {
                    var lstGOVID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID && c.ORD_GroupProduct.ORD_Order.GroupOfVehicleID > 0).OrderByDescending(c => c.ORD_GroupProduct.ORD_Order.CAT_GroupOfVehicle.Ton).Select(c => c.ORD_GroupProduct.ORD_Order.GroupOfVehicleID.Value).Distinct().ToList();
                    if (lstGOVID.Count > 0)
                        master.GroupOfVehicleID = lstGOVID.FirstOrDefault();
                    else
                    {
                        var vehicle = model.CAT_Vehicle.FirstOrDefault(c => c.ID == master.VehicleID);
                        if (vehicle != null)
                            master.GroupOfVehicleID = vehicle.GroupOfVehicleID;
                    }
                }

                #endregion

                #region Thay đổi trạng thái lệnh
                master.ModifiedBy = Account.UserName;
                master.ModifiedDate = DateTime.Now;
                master.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterReceived;
                master.TypeOfPaymentDITOMasterID = -(int)SYSVarType.TypeOfPaymentDITOMasterOpen;
                master.DateConfig = master.ETD.HasValue ? master.ETD.Value.Date : DateTime.Now.Date;
                master.VendorOfVehicleID = master.VendorOfVehicleID.HasValue ? master.VendorOfVehicleID.Value : Account.SYSCustomerID;
                // TimeSheet
                var timeSheet = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ReferID == tomasterid && (c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetRunning || c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetAccept || c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetOpen) && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster);
                if (timeSheet != null)
                {
                    timeSheet.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                    timeSheet.ModifiedDate = DateTime.Now;
                    timeSheet.ModifiedBy = Account.UserName;
                    timeSheet.DateFromActual = master.ATD.HasValue ? master.ATD.Value : DateTime.Now;
                    timeSheet.DateToActual = master.ATA.HasValue ? master.ATA.Value : DateTime.Now;
                }
                // Ktra nếu ko có location nào có sort real thì cập nhật sort real = sort order
                if (model.OPS_DITOLocation.Count(c => c.DITOMasterID == master.ID && c.SortOrderReal == null) == 0)
                {
                    foreach (var item in model.OPS_DITOLocation.Where(c => c.DITOMasterID == master.ID))
                        item.SortOrderReal = item.SortOrder;
                }
                model.SaveChanges();
                #endregion

                #region Cập nhật status cho group
                foreach (var item in lstGroup)
                {
                    item.ModifiedBy = Account.UserName;
                    item.ModifiedDate = DateTime.Now;
                    item.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                }
                #endregion

                #region Cập nhật DITO
                foreach (var item in model.OPS_DITO.Where(c => c.DITOMasterID == master.ID))
                {
                    foreach (var detail in model.OPS_DITODetail.Where(c => c.DITOID == item.ID))
                        model.OPS_DITODetail.Remove(detail);
                    model.OPS_DITO.Remove(item);
                }

                var lstLocation = model.OPS_DITOLocation.Where(c => c.DITOMasterID == master.ID && c.SortOrderReal != null).OrderBy(c => c.SortOrderReal).Select(c => new
                {
                    c.LocationID,
                    c.DateComeEstimate,
                    DateCome = c.DateCome.HasValue ? c.DateCome : c.DateLeave
                }).ToList();
                if (lstLocation.Count > 1)
                {
                    int sortOrder = 1;
                    for (int i = 0; i < lstLocation.Count - 1; i++)
                    {
                        OPS_DITO objDITO = new OPS_DITO();
                        objDITO.CreatedBy = Account.UserName;
                        objDITO.CreatedDate = DateTime.Now;
                        objDITO.DITOMasterID = master.ID;
                        objDITO.SortOrder = sortOrder;
                        objDITO.StatusOfDITOID = -(int)SYSVarType.DITOLocationStatusLeave;
                        objDITO.ETD = lstLocation[i].DateComeEstimate;
                        objDITO.ETA = lstLocation[i + 1].DateComeEstimate;
                        objDITO.LocationFromID = lstLocation[i].LocationID;
                        objDITO.LocationToID = lstLocation[i + 1].LocationID;
                        objDITO.ATD = lstLocation[i].DateCome;
                        objDITO.ATA = lstLocation[i + 1].DateCome;
                        if (i == 0 && objDITO.ATD == null)
                            objDITO.ATD = master.ATD;
                        model.OPS_DITO.Add(objDITO);

                        // check KM
                        var matrix = model.CAT_LocationMatrix.FirstOrDefault(c => c.LocationFromID == objDITO.LocationFromID && c.LocationToID == objDITO.LocationToID && c.KM > 0);
                        if (matrix != null)
                            objDITO.KM = matrix.KM;

                        // Tạo detail
                        var lstGroupDetail = lstGroup.Where(c => c.ORD_GroupProduct.LocationToID > 0 && c.ORD_GroupProduct.CUS_Location1.LocationID == objDITO.LocationToID);
                        foreach (var item in lstGroupDetail)
                        {
                            OPS_DITODetail objDetail = new OPS_DITODetail();
                            objDetail.CreatedBy = Account.UserName;
                            objDetail.CreatedDate = DateTime.Now;
                            objDetail.OPS_DITO = objDITO;
                            objDetail.DITOGroupProductID = item.ID;
                            model.OPS_DITODetail.Add(objDetail);
                        }

                        sortOrder++;
                    }
                }

                #endregion

                #region Tìm hợp đồng vendor
                bool isHome = master.VendorOfVehicleID == Account.SYSCustomerID; // xe nhà

                var objContractCUS = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == tomasterid).Select(c => new { c.ORD_GroupProduct.ORD_Order.ContractID, c.ORD_GroupProduct.ORD_Order.CAT_TransportMode.TransportModeID, c.OPS_DITOMaster.VendorOfVehicleID, c.ORD_GroupProduct.ORD_Order.CustomerID }).FirstOrDefault();
                var flagNoContract = true;
                if (objContractCUS != null)
                {
                    var lstGroupRouting = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID).Select(c => new HelperFinance_OPSGroupProduct
                    {
                        ID = c.ID,
                        OrderRoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                        LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                        LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                        GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID.Value,
                        CATRoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                    }).ToList();

                    int vendorID = master.VendorOfVehicleID.HasValue ? master.VendorOfVehicleID.Value : Account.SYSCustomerID;
                    // Nhóm hàng Mapping
                    var lstGroupMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == vendorID).ToList();

                    foreach (var itemOPSGroupProduct in lstGroupRouting)
                    {
                        var mapping = lstGroupMapping.FirstOrDefault(c => c.VendorID == vendorID && c.GroupOfProductCUSID == itemOPSGroupProduct.GroupOfProductID);
                        if (mapping != null)
                            itemOPSGroupProduct.GroupOfProductID = mapping.GroupOfProductVENID;
                        else
                            itemOPSGroupProduct.GroupOfProductID = -1;
                    }

                    HelperFinance_Contract objContractVEN = new HelperFinance_Contract();
                    var lstContractVEN = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && ((c.CompanyID > 0 && c.CUS_Company.CustomerRelateID == objContractCUS.CustomerID) || c.CompanyID == null) && (c.CustomerID == master.VendorOfVehicleID || (isHome && c.CustomerID == null)) && c.EffectDate <= master.DateConfig && (c.ExpiredDate == null || c.ExpiredDate > master.DateConfig) &&
                        (c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFTL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLTL)).Select(c => new HelperFinance_Contract
                        {
                            ID = c.ID,
                            TransportModeID = c.TransportModeID,
                            TypeOfContractDateID = c.TypeOfContractDateID,
                            EffectDate = c.EffectDate,
                            CustomerID = c.CustomerID
                        }).OrderByDescending(c => c.EffectDate).ToList();

                    // Vendor bốc xếp
                    var lstVendorLoadID = lstGroup.Where(c => c.VendorLoadID.HasValue).Select(c => c.VendorLoadID.Value).Distinct().ToList();
                    lstVendorLoadID.AddRange(lstGroup.Where(c => c.VendorUnLoadID.HasValue).Select(c => c.VendorUnLoadID.Value).Distinct().ToList());
                    lstVendorLoadID = lstVendorLoadID.Distinct().ToList();
                    lstContractVENLoad = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && ((c.CompanyID > 0 && c.CUS_Company.CustomerRelateID == objContractCUS.CustomerID) || c.CompanyID == null) && lstVendorLoadID.Contains(c.CustomerID.Value) && c.EffectDate <= master.DateConfig && (c.ExpiredDate == null || c.ExpiredDate > master.DateConfig) &&
                       (c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFTL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLTL)).Select(c => new HelperFinance_Contract
                       {
                           ID = c.ID,
                           TransportModeID = c.TransportModeID,
                           TypeOfContractDateID = c.TypeOfContractDateID,
                           EffectDate = c.EffectDate,
                           CustomerID = c.CustomerID
                       }).OrderByDescending(c => c.EffectDate).ToList();

                    #region Giá chính - Lấy hợp đồng có giá
                    // Ưu tiên lấy theo transport mode của khách hàng
                    foreach (var itemContractVEN in lstContractVEN)
                    {
                        objContractVEN = itemContractVEN;
                        bool flagHasPrice = false;
                        // Danh sách bảng giá
                        var lstPrice = model.CAT_Price.Where(c => c.CAT_ContractTerm.ContractID == itemContractVEN.ID && c.EffectDate <= master.DateConfig && (c.CAT_ContractTerm.DateEffect <= master.DateConfig && (c.CAT_ContractTerm.DateExpire == null || c.CAT_ContractTerm.DateExpire >= master.DateConfig)));
                        // Lấy bảng giá gần nhất
                        if (lstPrice != null && lstPrice.Count() > 0)
                        {
                            decimal masterPrice = 0;
                            var lstPriceID = lstPrice.Select(c => c.ID).Distinct().ToList();
                            foreach (var item in lstGroup)
                            {
                                var objGroupRouting = lstGroupRouting.FirstOrDefault(c => c.ID == item.ID);
                                if (objGroupRouting != null)
                                {
                                    // Tìm giá FTL
                                    if (master.TransportModeID > 0 && master.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFTL)
                                    {
                                        var itemPrice = model.CAT_PriceGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_ContractRouting.RoutingID == objGroupRouting.OrderRoutingID && c.GroupOfVehicleID == master.GroupOfVehicleID && c.Price > 0).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_PriceGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.GroupOfVehicleID == master.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == objGroupRouting.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == objGroupRouting.LocationToID).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_PriceGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.GroupOfVehicleID == master.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == objGroupRouting.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == objGroupRouting.LocationToID)).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_PriceGVLevelGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_ContractRouting.RoutingID == objGroupRouting.OrderRoutingID && c.CAT_ContractLevel.GroupOfVehicleID == master.GroupOfVehicleID && c.Price > 0).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_PriceGVLevelGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_ContractLevel.GroupOfVehicleID == master.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == objGroupRouting.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == objGroupRouting.LocationToID).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_PriceGVLevelGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_ContractLevel.GroupOfVehicleID == master.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == objGroupRouting.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == objGroupRouting.LocationToID)).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_ContractRouting.Where(c => c.ContractID == master.ContractID && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.RoutingID == objGroupRouting.OrderRoutingID).Select(c => new { c.RoutingID, Price = (decimal)0 }).FirstOrDefault();
                                        if (itemPrice != null)
                                        {
                                            item.CATRoutingID = itemPrice.RoutingID;
                                            if (masterPrice <= itemPrice.Price)
                                            {
                                                master.CATRoutingID = itemPrice.RoutingID;
                                                masterPrice = itemPrice.Price;
                                            }
                                            flagHasPrice = true;
                                        }
                                        else
                                            item.CATRoutingID = objGroupRouting.OrderRoutingID;
                                    }
                                    // Tìm giá LTL
                                    if (master.TransportModeID > 0 && master.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLTL)
                                    {
                                        var itemPrice = model.CAT_PriceDIGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_ContractRouting.RoutingID == objGroupRouting.OrderRoutingID && c.GroupOfProductID == objGroupRouting.GroupOfProductID && c.Price > 0).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_PriceDIGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.GroupOfProductID == objGroupRouting.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == objGroupRouting.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == objGroupRouting.LocationToID).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_PriceDIGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.GroupOfProductID == objGroupRouting.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == objGroupRouting.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == objGroupRouting.LocationToID)).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.CAT_ContractRouting.RoutingID == objGroupRouting.OrderRoutingID && c.GroupOfProductID == objGroupRouting.GroupOfProductID && c.Price > 0).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.GroupOfProductID == objGroupRouting.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == objGroupRouting.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == objGroupRouting.LocationToID).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.GroupOfProductID == objGroupRouting.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == objGroupRouting.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == objGroupRouting.LocationToID)).OrderByDescending(c => c.Price).Select(c => new { c.CAT_ContractRouting.RoutingID, c.Price }).FirstOrDefault();
                                        if (itemPrice == null)
                                            itemPrice = model.CAT_ContractRouting.Where(c => c.ContractID == master.ContractID && c.ContractRoutingTypeID == -(int)SYSVarType.ContractRoutingTypePrice && c.RoutingID == objGroupRouting.OrderRoutingID).Select(c => new { c.RoutingID, Price = (decimal)0 }).FirstOrDefault();
                                        if (itemPrice != null)
                                        {
                                            item.CATRoutingID = itemPrice.RoutingID;
                                            if (masterPrice <= itemPrice.Price)
                                            {
                                                master.CATRoutingID = itemPrice.RoutingID;
                                                masterPrice = itemPrice.Price;
                                            }
                                            flagHasPrice = true;
                                        }
                                        else
                                            item.CATRoutingID = objGroupRouting.OrderRoutingID;
                                    }
                                }
                                if (master.CATRoutingID == null)
                                    master.CATRoutingID = objGroupRouting.OrderRoutingID;
                            }
                        }

                        if (lstPrice.Count() == 0 && (itemContractVEN.CustomerID == Account.SYSCustomerID || itemContractVEN.CustomerID == null))
                        {
                            foreach (var item in lstGroup)
                            {
                                var opsGroup = lstGroupRouting.FirstOrDefault(c => c.ID == item.ID && c.CATRoutingID > 0);
                                if (opsGroup != null)
                                    item.CATRoutingID = opsGroup.CATRoutingID;
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
                        if (objContractVEN.CustomerID != null && objContractVEN.CustomerID != Account.SYSCustomerID)
                            master.TransportModeID = objContractVEN.TransportModeID;

                        flagNoContract = false;

                        // Cập nhật lại ngày tính giá
                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETD && master.ETD.HasValue)
                            master.DateConfig = master.ETD.Value.Date;

                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATA && master.ATA.HasValue)
                            master.DateConfig = master.ATA.Value.Date;

                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATD && master.ATD.HasValue)
                            master.DateConfig = master.ATD.Value.Date;

                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateComplete && master.DateReceived.HasValue)
                            master.DateConfig = master.DateReceived.Value.Date;

                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateRequest)
                        {
                            var requestDate = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == tomasterid).OrderBy(c => c.ORD_GroupProduct.ORD_Order.RequestDate).Select(c => c.ORD_GroupProduct.ORD_Order.RequestDate).FirstOrDefault();
                            if (requestDate != null)
                                master.DateConfig = requestDate.Date;

                            foreach (var opsGroup in model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID))
                                opsGroup.DateConfig = opsGroup.ORD_GroupProduct.ORD_Order.RequestDate.Date;
                        }

                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETARequest)
                        {
                            foreach (var opsGroup in model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID))
                                opsGroup.DateConfig = opsGroup.ORD_GroupProduct.ETARequest.HasValue ? opsGroup.ORD_GroupProduct.ETARequest.Value.Date : opsGroup.ORD_GroupProduct.ORD_Order.RequestDate.Date;
                        }

                        if (objContractVEN.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETDRequest)
                        {
                            foreach (var opsGroup in model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID))
                                opsGroup.DateConfig = opsGroup.ORD_GroupProduct.ETDRequest.HasValue ? opsGroup.ORD_GroupProduct.ETDRequest.Value.Date : opsGroup.ORD_GroupProduct.ORD_Order.RequestDate.Date;
                        }

                        if (objContractVEN.TypeOfContractDateID != -(int)SYSVarType.TypeOfContractDateRequest && objContractVEN.TypeOfContractDateID != -(int)SYSVarType.TypeOfContractDateETARequest && objContractVEN.TypeOfContractDateID != -(int)SYSVarType.TypeOfContractDateETDRequest)
                        {
                            // Cập nhật theo DateConfig của master
                            foreach (var opsGroup in model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID))
                                opsGroup.DateConfig = master.DateConfig;
                        }
                    }
                }

                if (flagNoContract == true)
                {
                    master.ContractID = null;
                    master.CATRoutingID = null;
                    foreach (var item in lstGroup)
                        item.CATRoutingID = null;
                }

                foreach (var item in lstGroup)
                {
                    if (item.VendorLoadID == master.VendorOfVehicleID)
                        item.VendorLoadContractID = master.ContractID;
                    else
                    {
                        var contract = lstContractVENLoad.FirstOrDefault(c => c.CustomerID == item.VendorLoadID);
                        if (contract != null)
                            item.VendorLoadContractID = contract.ID;
                    }
                    if (item.VendorUnLoadID == master.VendorOfVehicleID)
                        item.VendorUnLoadContractID = master.ContractID;
                    else
                    {
                        var contract = lstContractVENLoad.FirstOrDefault(c => c.CustomerID == item.VendorUnLoadID);
                        if (contract != null)
                            item.VendorUnLoadContractID = contract.ID;
                    }
                }

                model.SaveChanges();
                #endregion

                // Cập nhật KPI
                HelperKPI.KPITime_DIMonitorComplete(model, Account, master.ID);
                model.SaveChanges();
                // Cập nhật status đơn hàng liên quan
                if (lstOrderID.Count > 0)
                    HelperStatus.ORDOrder_Status(model, Account, lstOrderID);
            }
            #endregion
        }

        public static void Truck_TimeChange(DataEntities model, AccountItem Account, int tomasterid)
        {
            // Cập nhật ATA, ATD cho chuyến
            var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == tomasterid);
            if (master != null)
            {
                if (master.ATD == null)
                {
                    var location = model.OPS_DITOLocation.Where(c => c.DITOMasterID == master.ID && c.DateCome != null && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet).OrderBy(c => c.DateCome).Select(c => new { c.DateCome }).FirstOrDefault();
                    if (location != null)
                        master.ATD = location.DateCome;
                    else
                        master.ATD = master.ETD;
                }

                if (master.ATA == null || master.ATA != null)
                {
                    var location = model.OPS_DITOLocation.Where(c => c.DITOMasterID == master.ID && c.DateCome != null && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery).OrderByDescending(c => c.DateLeave).Select(c => new { c.DateLeave }).FirstOrDefault();
                    if (master.ATA != null)
                    {
                        if (location != null && location.DateLeave > master.ATA)
                            master.ATA = location.DateLeave;
                    }
                    else
                        master.ATA = location.DateLeave;
                }

                master.DateReceived = master.ATA;

                // TimeSheet
                if (master.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterReceived)
                {
                    var timeSheet = model.FLM_AssetTimeSheet.FirstOrDefault(c => c.ReferID == tomasterid && (c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetRunning || c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetAccept || c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetOpen || c.TypeOfAssetTimeSheetID == -(int)SYSVarType.TypeOfAssetTimeSheetComplete) && c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster);
                    if (timeSheet != null)
                    {
                        timeSheet.TypeOfAssetTimeSheetID = -(int)SYSVarType.TypeOfAssetTimeSheetComplete;
                        timeSheet.ModifiedDate = DateTime.Now;
                        timeSheet.ModifiedBy = Account.UserName;
                        timeSheet.DateFromActual = master.ATD.HasValue ? master.ATD.Value : DateTime.Now;
                        timeSheet.DateToActual = master.ATA.HasValue ? master.ATA.Value : DateTime.Now;
                    }
                }

                // Ktra nếu ko có location nào có sort real thì cập nhật sort real = sort order
                if (model.OPS_DITOLocation.Count(c => c.DITOMasterID == master.ID && c.SortOrderReal == null) == 0)
                {
                    foreach (var item in model.OPS_DITOLocation.Where(c => c.DITOMasterID == master.ID))
                        item.SortOrderReal = item.SortOrder;
                }
                model.SaveChanges();

                // Cập nhật status cho group
                var lstGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID).ToList();

                // Xóa DITO
                foreach (var item in model.OPS_DITO.Where(c => c.DITOMasterID == master.ID))
                {
                    foreach (var detail in model.OPS_DITODetail.Where(c => c.DITOID == item.ID))
                        model.OPS_DITODetail.Remove(detail);
                    model.OPS_DITO.Remove(item);
                }
                // Cập nhật DITO
                var lstLocation = model.OPS_DITOLocation.Where(c => c.DITOMasterID == master.ID && c.SortOrderReal != null).OrderBy(c => c.SortOrderReal).Select(c => new
                {
                    c.LocationID,
                    c.DateComeEstimate
                }).ToList();
                if (lstLocation.Count > 1)
                {
                    int sortOrder = 1;
                    for (int i = 0; i < lstLocation.Count - 1; i++)
                    {
                        OPS_DITO objDITO = new OPS_DITO();
                        objDITO.CreatedBy = Account.UserName;
                        objDITO.CreatedDate = DateTime.Now;
                        objDITO.DITOMasterID = master.ID;
                        objDITO.SortOrder = sortOrder;
                        objDITO.StatusOfDITOID = -(int)SYSVarType.DITOLocationStatusLeave;
                        objDITO.ETD = lstLocation[i].DateComeEstimate;
                        objDITO.ETA = lstLocation[i + 1].DateComeEstimate;
                        objDITO.LocationFromID = lstLocation[i].LocationID;
                        objDITO.LocationToID = lstLocation[i + 1].LocationID;
                        model.OPS_DITO.Add(objDITO);

                        // check KM
                        var matrix = model.CAT_LocationMatrix.FirstOrDefault(c => c.LocationFromID == objDITO.LocationFromID && c.LocationToID == objDITO.LocationToID && c.KM > 0);
                        if (matrix != null)
                            objDITO.KM = matrix.KM;

                        // Tạo detail
                        var lstGroupDetail = lstGroup.Where(c => c.ORD_GroupProduct.LocationToID > 0 && c.ORD_GroupProduct.CUS_Location1.LocationID == objDITO.LocationToID);
                        foreach (var item in lstGroupDetail)
                        {
                            OPS_DITODetail objDetail = new OPS_DITODetail();
                            objDetail.CreatedBy = Account.UserName;
                            objDetail.CreatedDate = DateTime.Now;
                            objDetail.OPS_DITO = objDITO;
                            objDetail.DITOGroupProductID = item.ID;
                            model.OPS_DITODetail.Add(objDetail);
                        }

                        sortOrder++;
                    }
                }
                model.SaveChanges();
            }
        }

        public static void Truck_CalculateOrder(DataEntities model, AccountItem Account, DateTime DateConfig, List<int> lstCustomerID)
        {
            System.Diagnostics.Debug.WriteLine("Truck_CalculateOrder: " + DateConfig.ToString("dd/MM/yyyy"));
            const int iTon = -(int)SYSVarType.PriceOfGOPTon;
            const int iCBM = -(int)SYSVarType.PriceOfGOPCBM;
            const int iQuantity = -(int)SYSVarType.PriceOfGOPTU;

            DateConfig = DateConfig.Date;
            var DateConfigEnd = DateConfig.Date.AddDays(1);

            // Lấy dữ liệu input
            var ItemInput = DIPrice_GetInput(model, DateConfig, Account, lstCustomerID, true);

            #region Xóa PL cũ
            foreach (var pl in model.FIN_PL.Where(c => DbFunctions.TruncateTime(c.Effdate) == DateConfig && c.SYSCustomerID == Account.SYSCustomerID && !c.IsPlanning && c.ContractID.HasValue && ItemInput.ListContractID.Contains(c.ContractID.Value)))
            {
                foreach (var plDetail in model.FIN_PLDetails.Where(c => c.PLID == pl.ID))
                {
                    foreach (var plGroup in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == plDetail.ID))
                        model.FIN_PLGroupOfProduct.Remove(plGroup);
                    model.FIN_PLDetails.Remove(plDetail);
                }
                model.FIN_PL.Remove(pl);
            }

            // Xóa PL của manual fix
            var lstOPSGroupProductID = ItemInput.ListManualFix.Where(c => c.ContractID == null).Select(c => c.DITOGroupProductID).Distinct().ToList();
            foreach (var item in model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID > 0 && lstOPSGroupProductID.Contains(c.GroupOfProductID) && c.FIN_PLDetails.CostID == (int)CATCostType.ManualFixCredit))
            {
                model.FIN_PLGroupOfProduct.Remove(item);
                model.FIN_PLDetails.Remove(item.FIN_PLDetails);
                model.FIN_PL.Remove(item.FIN_PLDetails.FIN_PL);
            }

            model.SaveChanges();
            #endregion

            List<FIN_PL> lstPlTrouble = new List<FIN_PL>();
            List<FIN_PL> lstPl = new List<FIN_PL>();
            List<FIN_Temp> lstPlTemp = new List<FIN_Temp>();
            List<HelperFinance_ORDGroupProduct> lstOrderGroupUpDate = new List<HelperFinance_ORDGroupProduct>();

            if (ItemInput.ListContract.Count > 0 && ItemInput.ListOrder.Count > 0)
            {
                #region Qui đổi
                var lstGroupProductChange = ItemInput.ListContractGroupProduct;
                //Thực hiện qui đổi
                foreach (var contractGroupChange in lstGroupProductChange.GroupBy(c => new { c.ContractID, c.TypeOfSGroupProductChangeID, c.TypeOfContractQuantityID }))
                {
                    var contractChange = contractGroupChange.FirstOrDefault();
                    foreach (var itemChange in contractGroupChange)
                    {
                        // Qui đổi chi tiết
                        if (contractChange.TypeOfSGroupProductChangeID == -(int)SYSVarType.TypeOfSGroupProductChangeDetail)
                        {
                            #region Qui đổi OPS nhóm, hàng hóa
                            foreach (var item in ItemInput.ListOPSGroupProduct.Where(c => c.GroupOfProductID == itemChange.GroupOfProductID && c.ContractID == itemChange.ContractID && (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue ? c.ProductID == itemChange.ProductID : true)))
                            {
                                // Thay đổi nhóm khác
                                if (itemChange.GroupOfProductIDChange.HasValue)
                                {
                                    item.GroupOfProductID = itemChange.GroupOfProductIDChange.Value;
                                    item.PriceOfGOPID = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChange : item.PriceOfGOPID;
                                    item.PriceOfGOPName = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChangeName : item.PriceOfGOPName;
                                }
                                if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                {
                                    if (itemChange.ProductID == item.ProductID)
                                    {
                                        //Qui đổi transfer
                                        DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                        itemTransfer.Quantity = item.QuantityTranfer;
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemTransfer.Quantity = item.QuantityBBGN;
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemTransfer.Quantity = item.Quantity;
                                        itemTransfer.Expression = itemChange.Expression;
                                        itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                        bool flag = false;
                                        try
                                        {
                                            flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                        }
                                        catch { }
                                        if (flag)
                                        {
                                            double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                            if (quantityTransfer.HasValue)
                                            {
                                                if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                                                {
                                                    var exchangeCBM = itemChange.Weight > 0 && itemChange.CBM.HasValue ? itemChange.CBM.Value / itemChange.Weight.Value : 0;
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                    {
                                                        item.TonTranfer = quantityTransfer.Value;
                                                        item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                    {
                                                        item.Ton = quantityTransfer.Value;
                                                        item.CBM = quantityTransfer.Value * exchangeCBM;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                    {
                                                        item.TonBBGN = quantityTransfer.Value;
                                                        item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                    }
                                                }
                                                if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                                                {
                                                    var exchangeTon = itemChange.Weight.HasValue && itemChange.CBM > 0 ? itemChange.Weight.Value / itemChange.CBM.Value : 0;
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                    {
                                                        item.CBMTranfer = quantityTransfer.Value;
                                                        item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                    {
                                                        item.CBM = quantityTransfer.Value;
                                                        item.Ton = quantityTransfer.Value * exchangeTon;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                    {
                                                        item.CBMBBGN = quantityTransfer.Value;
                                                        item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                    }
                                                }
                                                if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                                                {
                                                    var exchangeTon = itemChange.Weight.HasValue ? itemChange.Weight.Value : 0;
                                                    var exchangeCBM = itemChange.CBM.HasValue ? itemChange.CBM.Value : 0;
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                    {
                                                        item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                        item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                        item.QuantityTranfer = quantityTransfer.Value;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                    {
                                                        item.Ton = quantityTransfer.Value * exchangeTon;
                                                        item.CBM = quantityTransfer.Value * exchangeCBM;
                                                        item.Quantity = quantityTransfer.Value;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                    {
                                                        item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                        item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                        item.QuantityBBGN = quantityTransfer.Value;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //Qui đổi transfer
                                    DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                    itemTransfer.Ton = item.TonTranfer;
                                    itemTransfer.CBM = item.CBMTranfer;
                                    itemTransfer.Quantity = item.QuantityTranfer;
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        itemTransfer.Ton = item.Ton;
                                        itemTransfer.CBM = item.CBM;
                                        itemTransfer.Quantity = item.Quantity;
                                    }
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        itemTransfer.Ton = item.TonBBGN;
                                        itemTransfer.CBM = item.CBMBBGN;
                                        itemTransfer.Quantity = item.QuantityBBGN;
                                    }
                                    itemTransfer.Expression = itemChange.Expression;
                                    itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                    bool flag = false;
                                    try
                                    {
                                        flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                    }
                                    catch { }
                                    if (flag)
                                    {
                                        double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                        if (quantityTransfer.HasValue)
                                        {
                                            if (item.PriceOfGOPID.HasValue)
                                            {
                                                if (item.PriceOfGOPID == iTon)
                                                {
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        item.TonTranfer = quantityTransfer.Value;
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        item.Ton = quantityTransfer.Value;
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        item.TonBBGN = quantityTransfer.Value;
                                                }
                                                else
                                                {
                                                    if (item.PriceOfGOPID == iCBM)
                                                    {
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                            item.CBMTranfer = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                            item.CBM = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                            item.CBMBBGN = quantityTransfer.Value;
                                                    }
                                                    else
                                                    {
                                                        if (item.PriceOfGOPID == iQuantity)
                                                        {
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                                item.QuantityTranfer = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                                item.Quantity = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                                item.QuantityBBGN = quantityTransfer.Value;
                                                        }
                                                    }
                                                }
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
                            }
                            #endregion

                            #region Qui đổi ORD nhóm, hàng hóa
                            foreach (var item in ItemInput.ListORDGroupProduct.Where(c => c.GroupOfProductID == itemChange.GroupOfProductID && c.ContractID == itemChange.ContractID && (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue ? c.ProductID == itemChange.ProductID : true)))
                            {
                                // Thay đổi nhóm khác
                                if (itemChange.GroupOfProductIDChange.HasValue)
                                {
                                    item.GroupOfProductID = itemChange.GroupOfProductIDChange.Value;
                                    item.PriceOfGOPID = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChange : item.PriceOfGOPID;
                                }
                                if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                {
                                    if (itemChange.ProductID == item.ProductID)
                                    {
                                        //Qui đổi transfer
                                        DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                        itemTransfer.Quantity = item.Quantity;
                                        itemTransfer.Expression = itemChange.Expression;
                                        itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                        bool flag = false;
                                        try
                                        {
                                            flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                        }
                                        catch { }
                                        if (flag)
                                        {
                                            double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                            if (quantityTransfer.HasValue)
                                            {
                                                if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                                                {
                                                    var exchangeCBM = itemChange.Weight > 0 && itemChange.CBM.HasValue ? itemChange.CBM.Value / itemChange.Weight.Value : 0;
                                                    item.Ton = quantityTransfer.Value;
                                                    item.CBM = quantityTransfer.Value * exchangeCBM;
                                                }
                                                if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                                                {
                                                    var exchangeTon = itemChange.Weight.HasValue && itemChange.CBM > 0 ? itemChange.Weight.Value / itemChange.CBM.Value : 0;
                                                    item.CBM = quantityTransfer.Value;
                                                    item.Ton = quantityTransfer.Value * exchangeTon;
                                                }
                                                if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                                                {
                                                    var exchangeTon = itemChange.Weight.HasValue ? itemChange.Weight.Value : 0;
                                                    var exchangeCBM = itemChange.CBM.HasValue ? itemChange.CBM.Value : 0;
                                                    item.Ton = quantityTransfer.Value * exchangeTon;
                                                    item.CBM = quantityTransfer.Value * exchangeCBM;
                                                    item.Quantity = quantityTransfer.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //Qui đổi
                                    DTOCATContractGroupOfProduct itemOrder = new DTOCATContractGroupOfProduct();
                                    itemOrder.Ton = item.Ton;
                                    itemOrder.CBM = item.CBM;
                                    itemOrder.Quantity = item.Quantity;
                                    itemOrder.Expression = itemChange.Expression;
                                    itemOrder.ExpressionInput = itemChange.ExpressionInput;
                                    bool flag = false;
                                    try
                                    {
                                        flag = GetGroupOfProductTransfer_Check(itemOrder);
                                    }
                                    catch { }
                                    if (flag)
                                    {
                                        double? quantityTransfer = GetGroupOfProductTransfer(itemOrder);
                                        if (quantityTransfer.HasValue)
                                        {
                                            if (item.PriceOfGOPID.HasValue)
                                            {
                                                if (item.PriceOfGOPID == iTon)
                                                    item.Ton = quantityTransfer.Value;
                                                else
                                                    if (item.PriceOfGOPID == iCBM)
                                                        item.CBM = quantityTransfer.Value;
                                                    else
                                                        if (item.PriceOfGOPID == iQuantity)
                                                            item.Quantity = quantityTransfer.Value;
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Qui đổi theo điểm
                        if (contractChange.TypeOfSGroupProductChangeID == -(int)SYSVarType.TypeOfSGroupProductChangeLocationTo)
                        {
                            #region Qui đổi OPS nhóm, hàng hóa
                            foreach (var itemGroup in ItemInput.ListOPSGroupProduct.Where(c => c.GroupOfProductID == itemChange.GroupOfProductID && c.ContractID == itemChange.ContractID && (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue ? c.ProductID == itemChange.ProductID : true)).GroupBy(c => c.LocationToID))
                            {
                                bool flag = false;
                                // Ktra nếu công thức đúng thì mới qui đổi
                                if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                {
                                    if (itemGroup.Any(d => d.ProductID == itemChange.ProductID))
                                    {
                                        DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityTranfer);
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityBBGN);
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                        itemTransfer.Expression = itemChange.Expression;
                                        itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                        try
                                        {
                                            flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                        }
                                        catch { }
                                    }
                                }
                                else
                                {
                                    //Qui đổi transfer
                                    DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                    itemTransfer.Ton = itemGroup.Sum(c => c.TonTranfer);
                                    itemTransfer.CBM = itemGroup.Sum(c => c.CBMTranfer);
                                    itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityTranfer);
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        itemTransfer.Ton = itemGroup.Sum(c => c.TonBBGN);
                                        itemTransfer.CBM = itemGroup.Sum(c => c.CBMBBGN);
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityBBGN);
                                    }
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        itemTransfer.Ton = itemGroup.Sum(c => c.Ton);
                                        itemTransfer.CBM = itemGroup.Sum(c => c.CBM);
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                    }
                                    itemTransfer.Expression = itemChange.Expression;
                                    itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                    try
                                    {
                                        flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                    }
                                    catch { }
                                }

                                if (flag)
                                {
                                    foreach (var item in itemGroup)
                                    {
                                        // Thay đổi nhóm khác
                                        if (itemChange.GroupOfProductIDChange.HasValue)
                                        {
                                            item.GroupOfProductID = itemChange.GroupOfProductIDChange.Value;
                                            item.PriceOfGOPID = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChange : item.PriceOfGOPID;
                                            item.PriceOfGOPName = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChangeName : item.PriceOfGOPName;
                                        }
                                        if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                        {
                                            if (itemChange.ProductID == item.ProductID)
                                            {
                                                //Qui đổi transfer
                                                DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                                itemTransfer.Quantity = item.QuantityTranfer;
                                                itemTransfer.Expression = itemChange.Expression;
                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                    itemTransfer.Quantity = item.QuantityBBGN;
                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                    itemTransfer.Quantity = item.Quantity;
                                                double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                                if (quantityTransfer.HasValue)
                                                {
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                                                    {
                                                        var exchangeCBM = itemChange.Weight > 0 && itemChange.CBM.HasValue ? itemChange.CBM.Value / itemChange.Weight.Value : 0;

                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.TonTranfer = quantityTransfer.Value;
                                                            item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.Ton = quantityTransfer.Value;
                                                            item.CBM = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.TonBBGN = quantityTransfer.Value;
                                                            item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue && itemChange.CBM > 0 ? itemChange.Weight.Value / itemChange.CBM.Value : 0;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.CBMTranfer = quantityTransfer.Value;
                                                            item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.CBM = quantityTransfer.Value;
                                                            item.Ton = quantityTransfer.Value * exchangeTon;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.CBMBBGN = quantityTransfer.Value;
                                                            item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                        }
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue ? itemChange.Weight.Value : 0;
                                                        var exchangeCBM = itemChange.CBM.HasValue ? itemChange.CBM.Value : 0;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                            item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                            item.QuantityTranfer = quantityTransfer.Value;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.Ton = quantityTransfer.Value * exchangeTon;
                                                            item.CBM = quantityTransfer.Value * exchangeCBM;
                                                            item.Quantity = quantityTransfer.Value;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                            item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                            item.QuantityBBGN = quantityTransfer.Value;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Qui đổi transfer
                                            DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                            itemTransfer.Ton = item.TonTranfer;
                                            itemTransfer.CBM = item.CBMTranfer;
                                            itemTransfer.Quantity = item.QuantityTranfer;
                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            {
                                                item.Ton = item.Ton;
                                                item.CBM = item.CBM;
                                                item.Quantity = item.Quantity;
                                            }
                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            {
                                                item.Ton = item.TonBBGN;
                                                item.CBM = item.CBMBBGN;
                                                item.Quantity = item.QuantityBBGN;
                                            }
                                            itemTransfer.Expression = itemChange.Expression;
                                            itemTransfer.ExpressionInput = itemChange.ExpressionInput;

                                            double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                            if (quantityTransfer.HasValue)
                                            {
                                                if (item.PriceOfGOPID.HasValue)
                                                {
                                                    if (item.PriceOfGOPID == iTon)
                                                    {
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                            item.TonTranfer = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                            item.Ton = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                            item.TonBBGN = quantityTransfer.Value;
                                                    }
                                                    else
                                                    {
                                                        if (item.PriceOfGOPID == iCBM)
                                                        {
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                                item.CBMTranfer = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                                item.CBM = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                                item.CBMBBGN = quantityTransfer.Value;
                                                        }
                                                        else
                                                        {
                                                            if (item.PriceOfGOPID == iQuantity)
                                                            {
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                                    item.QuantityTranfer = quantityTransfer.Value;
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                                    item.Quantity = quantityTransfer.Value;
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                                    item.QuantityBBGN = quantityTransfer.Value;
                                                            }
                                                        }
                                                    }
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
                                }
                            }
                            #endregion

                            #region Qui đổi ORD nhóm, hàng hóa
                            foreach (var itemGroup in ItemInput.ListORDGroupProduct.Where(c => c.GroupOfProductID == itemChange.GroupOfProductID && c.ContractID == itemChange.ContractID && (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue ? c.ProductID == itemChange.ProductID : true)).GroupBy(c => c.LocationToID))
                            {
                                bool flag = false;
                                // Ktra nếu công thức đúng thì mới qui đổi
                                if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                {
                                    if (itemGroup.Any(d => d.ProductID == itemChange.ProductID))
                                    {
                                        DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                        itemTransfer.Expression = itemChange.Expression;
                                        itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                        try
                                        {
                                            flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                        }
                                        catch { }
                                    }
                                }
                                else
                                {
                                    //Qui đổi transfer
                                    DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                    itemTransfer.Ton = itemGroup.Sum(c => c.Ton);
                                    itemTransfer.CBM = itemGroup.Sum(c => c.CBM);
                                    itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                    itemTransfer.Expression = itemChange.Expression;
                                    itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                    try
                                    {
                                        flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                    }
                                    catch { }
                                }

                                if (flag)
                                {
                                    foreach (var item in itemGroup)
                                    {
                                        // Thay đổi nhóm khác
                                        if (itemChange.GroupOfProductIDChange.HasValue)
                                        {
                                            item.GroupOfProductID = itemChange.GroupOfProductIDChange.Value;
                                            item.PriceOfGOPID = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChange : item.PriceOfGOPID;
                                        }
                                        if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                        {
                                            if (itemChange.ProductID == item.ProductID)
                                            {
                                                //Qui đổi transfer
                                                DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                                itemTransfer.Quantity = item.Quantity;
                                                itemTransfer.Expression = itemChange.Expression;

                                                double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                                if (quantityTransfer.HasValue)
                                                {
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                                                    {
                                                        var exchangeCBM = itemChange.Weight > 0 && itemChange.CBM.HasValue ? itemChange.CBM.Value / itemChange.Weight.Value : 0;
                                                        item.Ton = quantityTransfer.Value;
                                                        item.CBM = quantityTransfer.Value * exchangeCBM;
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue && itemChange.CBM > 0 ? itemChange.Weight.Value / itemChange.CBM.Value : 0;
                                                        item.CBM = quantityTransfer.Value;
                                                        item.Ton = quantityTransfer.Value * exchangeTon;
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue ? itemChange.Weight.Value : 0;
                                                        var exchangeCBM = itemChange.CBM.HasValue ? itemChange.CBM.Value : 0;
                                                        item.Ton = quantityTransfer.Value * exchangeTon;
                                                        item.CBM = quantityTransfer.Value * exchangeCBM;
                                                        item.Quantity = quantityTransfer.Value;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Qui đổi transfer
                                            DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                            itemTransfer.Ton = item.Ton;
                                            itemTransfer.CBM = item.CBM;
                                            itemTransfer.Quantity = item.Quantity;
                                            itemTransfer.Expression = itemChange.Expression;
                                            itemTransfer.ExpressionInput = itemChange.ExpressionInput;

                                            double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                            if (quantityTransfer.HasValue)
                                            {
                                                if (item.PriceOfGOPID.HasValue)
                                                {
                                                    if (item.PriceOfGOPID == iTon)
                                                        item.Ton = quantityTransfer.Value;
                                                    else
                                                        if (item.PriceOfGOPID == iCBM)
                                                            item.CBM = quantityTransfer.Value;
                                                        else
                                                            if (item.PriceOfGOPID == iQuantity)
                                                                item.Quantity = quantityTransfer.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }

                        // Qui đổi theo cung đường
                        if (contractChange.TypeOfSGroupProductChangeID == -(int)SYSVarType.TypeOfSGroupProductChangeRouting)
                        {
                            #region Qui đổi OPS nhóm, hàng hóa
                            foreach (var itemGroup in ItemInput.ListOPSGroupProduct.Where(c => c.GroupOfProductID == itemChange.GroupOfProductID && c.ContractID == itemChange.ContractID && (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue ? c.ProductID == itemChange.ProductID : true) && c.CATRoutingID > 0).GroupBy(c => c.CATRoutingID))
                            {
                                bool flag = false;
                                // Ktra nếu công thức đúng thì mới qui đổi
                                if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                {
                                    if (itemGroup.Any(d => d.ProductID == itemChange.ProductID))
                                    {
                                        DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityTranfer);
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityBBGN);
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                        itemTransfer.Expression = itemChange.Expression;
                                        itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                        try
                                        {
                                            flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                        }
                                        catch { }
                                    }
                                }
                                else
                                {
                                    //Qui đổi transfer
                                    DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                    itemTransfer.Ton = itemGroup.Sum(c => c.TonTranfer);
                                    itemTransfer.CBM = itemGroup.Sum(c => c.CBMTranfer);
                                    itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityTranfer);
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        itemTransfer.Ton = itemGroup.Sum(c => c.TonBBGN);
                                        itemTransfer.CBM = itemGroup.Sum(c => c.CBMBBGN);
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityBBGN);
                                    }
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        itemTransfer.Ton = itemGroup.Sum(c => c.Ton);
                                        itemTransfer.CBM = itemGroup.Sum(c => c.CBM);
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                    }
                                    itemTransfer.Expression = itemChange.Expression;
                                    itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                    try
                                    {
                                        flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                    }
                                    catch { }
                                }

                                if (flag)
                                {
                                    foreach (var item in itemGroup)
                                    {
                                        // Thay đổi nhóm khác
                                        if (itemChange.GroupOfProductIDChange.HasValue)
                                        {
                                            item.GroupOfProductID = itemChange.GroupOfProductIDChange.Value;
                                            item.PriceOfGOPID = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChange : item.PriceOfGOPID;
                                            item.PriceOfGOPName = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChangeName : item.PriceOfGOPName;
                                        }
                                        if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                        {
                                            if (itemChange.ProductID == item.ProductID)
                                            {
                                                //Qui đổi transfer
                                                DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                                itemTransfer.Quantity = item.QuantityTranfer;
                                                itemTransfer.Expression = itemChange.Expression;
                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                    itemTransfer.Quantity = item.QuantityBBGN;
                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                    itemTransfer.Quantity = item.Quantity;
                                                double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                                if (quantityTransfer.HasValue)
                                                {
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                                                    {
                                                        var exchangeCBM = itemChange.Weight > 0 && itemChange.CBM.HasValue ? itemChange.CBM.Value / itemChange.Weight.Value : 0;

                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.TonTranfer = quantityTransfer.Value;
                                                            item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.Ton = quantityTransfer.Value;
                                                            item.CBM = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.TonBBGN = quantityTransfer.Value;
                                                            item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue && itemChange.CBM > 0 ? itemChange.Weight.Value / itemChange.CBM.Value : 0;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.CBMTranfer = quantityTransfer.Value;
                                                            item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.CBM = quantityTransfer.Value;
                                                            item.Ton = quantityTransfer.Value * exchangeTon;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.CBMBBGN = quantityTransfer.Value;
                                                            item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                        }
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue ? itemChange.Weight.Value : 0;
                                                        var exchangeCBM = itemChange.CBM.HasValue ? itemChange.CBM.Value : 0;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                            item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                            item.QuantityTranfer = quantityTransfer.Value;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.Ton = quantityTransfer.Value * exchangeTon;
                                                            item.CBM = quantityTransfer.Value * exchangeCBM;
                                                            item.Quantity = quantityTransfer.Value;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                            item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                            item.QuantityBBGN = quantityTransfer.Value;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Qui đổi transfer
                                            DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                            itemTransfer.Ton = item.TonTranfer;
                                            itemTransfer.CBM = item.CBMTranfer;
                                            itemTransfer.Quantity = item.QuantityTranfer;
                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            {
                                                item.Ton = item.Ton;
                                                item.CBM = item.CBM;
                                                item.Quantity = item.Quantity;
                                            }
                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            {
                                                item.Ton = item.TonBBGN;
                                                item.CBM = item.CBMBBGN;
                                                item.Quantity = item.QuantityBBGN;
                                            }
                                            itemTransfer.Expression = itemChange.Expression;
                                            itemTransfer.ExpressionInput = itemChange.ExpressionInput;

                                            double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                            if (quantityTransfer.HasValue)
                                            {
                                                if (item.PriceOfGOPID.HasValue)
                                                {
                                                    if (item.PriceOfGOPID == iTon)
                                                    {
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                            item.TonTranfer = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                            item.Ton = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                            item.TonBBGN = quantityTransfer.Value;
                                                    }
                                                    else
                                                    {
                                                        if (item.PriceOfGOPID == iCBM)
                                                        {
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                                item.CBMTranfer = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                                item.CBM = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                                item.CBMBBGN = quantityTransfer.Value;
                                                        }
                                                        else
                                                        {
                                                            if (item.PriceOfGOPID == iQuantity)
                                                            {
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                                    item.QuantityTranfer = quantityTransfer.Value;
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                                    item.Quantity = quantityTransfer.Value;
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                                    item.QuantityBBGN = quantityTransfer.Value;
                                                            }
                                                        }
                                                    }
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
                                }
                            }
                            #endregion

                            #region Qui đổi ORD nhóm, hàng hóa
                            foreach (var itemGroup in ItemInput.ListORDGroupProduct.Where(c => c.GroupOfProductID == itemChange.GroupOfProductID && c.ContractID == itemChange.ContractID && (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue ? c.ProductID == itemChange.ProductID : true)).GroupBy(c => c.LocationToID))
                            {
                                bool flag = false;
                                // Ktra nếu công thức đúng thì mới qui đổi
                                if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                {
                                    if (itemGroup.Any(d => d.ProductID == itemChange.ProductID))
                                    {
                                        DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                        itemTransfer.Expression = itemChange.Expression;
                                        itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                        try
                                        {
                                            flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                        }
                                        catch { }
                                    }
                                }
                                else
                                {
                                    //Qui đổi transfer
                                    DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                    itemTransfer.Ton = itemGroup.Sum(c => c.Ton);
                                    itemTransfer.CBM = itemGroup.Sum(c => c.CBM);
                                    itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                    itemTransfer.Expression = itemChange.Expression;
                                    itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                    try
                                    {
                                        flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                    }
                                    catch { }
                                }

                                if (flag)
                                {
                                    foreach (var item in itemGroup)
                                    {
                                        // Thay đổi nhóm khác
                                        if (itemChange.GroupOfProductIDChange.HasValue)
                                        {
                                            item.GroupOfProductID = itemChange.GroupOfProductIDChange.Value;
                                            item.PriceOfGOPID = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChange : item.PriceOfGOPID;
                                        }
                                        if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                        {
                                            if (itemChange.ProductID == item.ProductID)
                                            {
                                                //Qui đổi transfer
                                                DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                                itemTransfer.Quantity = item.Quantity;
                                                itemTransfer.Expression = itemChange.Expression;

                                                double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                                if (quantityTransfer.HasValue)
                                                {
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                                                    {
                                                        var exchangeCBM = itemChange.Weight > 0 && itemChange.CBM.HasValue ? itemChange.CBM.Value / itemChange.Weight.Value : 0;
                                                        item.Ton = quantityTransfer.Value;
                                                        item.CBM = quantityTransfer.Value * exchangeCBM;
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue && itemChange.CBM > 0 ? itemChange.Weight.Value / itemChange.CBM.Value : 0;
                                                        item.CBM = quantityTransfer.Value;
                                                        item.Ton = quantityTransfer.Value * exchangeTon;
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue ? itemChange.Weight.Value : 0;
                                                        var exchangeCBM = itemChange.CBM.HasValue ? itemChange.CBM.Value : 0;
                                                        item.Ton = quantityTransfer.Value * exchangeTon;
                                                        item.CBM = quantityTransfer.Value * exchangeCBM;
                                                        item.Quantity = quantityTransfer.Value;
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Qui đổi transfer
                                            DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                            itemTransfer.Ton = item.Ton;
                                            itemTransfer.CBM = item.CBM;
                                            itemTransfer.Quantity = item.Quantity;
                                            itemTransfer.Expression = itemChange.Expression;
                                            itemTransfer.ExpressionInput = itemChange.ExpressionInput;

                                            double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                            if (quantityTransfer.HasValue)
                                            {
                                                if (item.PriceOfGOPID.HasValue)
                                                {
                                                    if (item.PriceOfGOPID == iTon)
                                                        item.Ton = quantityTransfer.Value;
                                                    else
                                                        if (item.PriceOfGOPID == iCBM)
                                                            item.CBM = quantityTransfer.Value;
                                                        else
                                                            if (item.PriceOfGOPID == iQuantity)
                                                                item.Quantity = quantityTransfer.Value;
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion
                        }
                    }
                }
                #endregion

                #region Chạy từng hợp đồng
                foreach (var itemContract in ItemInput.ListContract)
                {
                    var lstPLTempContract = ItemInput.ListFINTemp.Where(c => c.ContractID == itemContract.ID);
                    System.Diagnostics.Debug.WriteLine("Contract start: " + itemContract.ID);

                    var queryOrderContract = ItemInput.ListOrder.Where(c => c.ContractID == itemContract.ID);
                    var queryOPSGroupProductContract = ItemInput.ListOPSGroupProduct.Where(c => c.ContractID == itemContract.ID);
                    var queryORDGroupProductContract = ItemInput.ListORDGroupProduct.Where(c => c.ContractID == itemContract.ID);

                    if (queryOrderContract.Count() > 0)
                    {
                        //Các tham số input của Price
                        decimal totalPrice = 0, totalLoadPrice = 0, totalUnLoadPrice = 0;

                        #region Bảng giá LTL
                        //Bảng giá LTL thường
                        var lstPriceLTLGroup = ItemInput.ListLTL.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig);
                        foreach (var itemOPSGroupProductContract in queryOPSGroupProductContract)
                        {
                            var itemPrice = lstPriceLTLGroup.Where(c => c.RoutingID == itemOPSGroupProductContract.CATRoutingID && c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price).FirstOrDefault();
                            if (itemPrice == null)
                                itemPrice = lstPriceLTLGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.LocationFromID == itemOPSGroupProductContract.LocationFromID && c.LocationToID == itemOPSGroupProductContract.LocationToID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price).FirstOrDefault();
                            if (itemPrice != null)
                            {
                                itemOPSGroupProductContract.Price = itemPrice.Price;
                                itemOPSGroupProductContract.CATRoutingID = itemPrice.RoutingID;
                            }
                            #region Tính tổng tiền
                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                            {
                                if (itemOPSGroupProductContract.PriceOfGOPID == iTon)
                                    totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.TonTranfer;
                                else if (itemOPSGroupProductContract.PriceOfGOPID == iCBM)
                                    totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBMTranfer;
                                else if (itemOPSGroupProductContract.PriceOfGOPID == iQuantity)
                                    totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.QuantityTranfer;
                            }
                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                            {
                                if (itemOPSGroupProductContract.PriceOfGOPID == iTon)
                                    totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.Ton;
                                else if (itemOPSGroupProductContract.PriceOfGOPID == iCBM)
                                    totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBM;
                                else if (itemOPSGroupProductContract.PriceOfGOPID == iQuantity)
                                    totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.Quantity;
                            }
                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                            {
                                if (itemOPSGroupProductContract.PriceOfGOPID == iTon)
                                    totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.TonBBGN;
                                else if (itemOPSGroupProductContract.PriceOfGOPID == iCBM)
                                    totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBMBBGN;
                                else if (itemOPSGroupProductContract.PriceOfGOPID == iQuantity)
                                    totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.QuantityBBGN;
                            }
                            #endregion
                        }

                        //Bảng giá LTL bậc thang
                        var lstPriceLTLLevel = ItemInput.ListLTLLevel.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig);
                        if (lstPriceLTLLevel.Count() > 0)
                        {
                            foreach (var itemOPSGroupProductContract in queryOPSGroupProductContract)
                            {
                                double totalTon = 0, totalCBM = 0, totalQuantity = 0;

                                #region Theo cung đường
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.TonTranfer);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.CBMTranfer);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.QuantityTranfer);
                                }
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.Ton);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.CBM);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.Quantity);
                                }
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.TonBBGN);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.CBMBBGN);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.QuantityBBGN);
                                }
                                #endregion

                                #region Theo điểm
                                if (itemContract.TypeOfRunLevelID == -(int)SYSVarType.TypeOfRunLevelLocation)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.TonTranfer);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBMTranfer);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.QuantityTranfer);
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.Ton);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBM);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.Quantity);
                                    }
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.TonBBGN);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBMBBGN);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.QuantityBBGN);
                                    }
                                }
                                #endregion

                                #region Theo cung đường cha
                                if (itemContract.TypeOfRunLevelID == -(int)SYSVarType.TypeOfRunLevelParentRouting)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.TonTranfer);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.CBMTranfer);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.QuantityTranfer);
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.Ton);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.CBM);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.Quantity);
                                    }
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.TonBBGN);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.CBMBBGN);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.QuantityBBGN);
                                    }
                                }
                                #endregion

                                #region Theo đơn hàng điểm
                                if (itemContract.TypeOfRunLevelID == -(int)SYSVarType.TypeOfRunLevelOrderLocation)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.TonTranfer);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBMTranfer);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.QuantityTranfer);
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.Ton);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBM);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.Quantity);
                                    }
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.TonBBGN);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBMBBGN);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.QuantityBBGN);
                                    }
                                }
                                #endregion

                                var objPriceLTLLevel = lstPriceLTLLevel.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.RoutingID == itemOPSGroupProductContract.CATRoutingID && (c.Ton == 0 || c.Ton >= totalTon) && (c.CBM == 0 || c.CBM >= totalCBM) && (c.Quantity == 0 || c.Quantity >= totalQuantity)).OrderByDescending(c => c.EffectDate).ThenBy(c => c.Ton).ThenBy(c => c.CBM).ThenBy(c => c.Quantity).ThenByDescending(c => c.Price).FirstOrDefault();
                                if (objPriceLTLLevel == null)
                                    objPriceLTLLevel = lstPriceLTLLevel.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.LocationFromID == itemOPSGroupProductContract.LocationFromID && c.LocationToID == itemOPSGroupProductContract.LocationToID && (c.Ton == 0 || c.Ton >= totalTon) && (c.CBM == 0 || c.CBM >= totalCBM) && (c.Quantity == 0 || c.Quantity >= totalQuantity)).OrderByDescending(c => c.EffectDate).ThenBy(c => c.Ton).ThenBy(c => c.CBM).ThenBy(c => c.Quantity).ThenByDescending(c => c.Price).FirstOrDefault();

                                if (objPriceLTLLevel != null)
                                {
                                    itemOPSGroupProductContract.Price = objPriceLTLLevel.Price;
                                    itemOPSGroupProductContract.CATRoutingID = objPriceLTLLevel.RoutingID;
                                }
                                #region Tổng tiền
                                if (itemOPSGroupProductContract.PriceOfGOPID == iTon)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.TonTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.Ton;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.TonBBGN;
                                }
                                else if (itemOPSGroupProductContract.PriceOfGOPID == iCBM)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBMTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBM;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBMBBGN;
                                }
                                else if (itemOPSGroupProductContract.PriceOfGOPID == iQuantity)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.QuantityTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.Quantity;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.QuantityBBGN;
                                }
                                #endregion
                            }
                        }

                        #region Cập nhật giá Order LTL
                        foreach (var itemOrderContract in queryOrderContract.Where(c => c.TransportModeID == -(int)SYSVarType.TransportModeLTL))
                        {
                            var lstGroup = queryOPSGroupProductContract.Where(c => c.OrderID == itemOrderContract.ID);
                            if (lstGroup != null && lstGroup.Count() > 0)
                            {
                                foreach (var item in lstGroup)
                                {
                                    if (item.PriceOfGOPID == iTon)
                                    {
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                            itemOrderContract.Price += item.Price * (decimal)item.TonTranfer;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemOrderContract.Price += item.Price * (decimal)item.Ton;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemOrderContract.Price += item.Price * (decimal)item.TonBBGN;
                                    }
                                    else if (item.PriceOfGOPID == iCBM)
                                    {
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                            itemOrderContract.Price += item.Price * (decimal)item.CBMTranfer;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemOrderContract.Price += item.Price * (decimal)item.CBM;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemOrderContract.Price += item.Price * (decimal)item.CBMBBGN;
                                    }
                                    else if (item.PriceOfGOPID == iQuantity)
                                    {
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                            itemOrderContract.Price += item.Price * (decimal)item.QuantityTranfer;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemOrderContract.Price += item.Price * (decimal)item.Quantity;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemOrderContract.Price += item.Price * (decimal)item.QuantityBBGN;
                                    }
                                }
                            }
                        }
                        #endregion
                        #endregion

                        #region Bảng giá FTL
                        //Bảng giá FTL thường
                        var lstPriceFTLGroup = ItemInput.ListFTL.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig);
                        foreach (var itemOrderContract in queryOrderContract.Where(c => c.GroupOfVehicleID > 0 && c.TransportModeID == -(int)SYSVarType.TransportModeFTL))
                        {
                            decimal tempPrice = itemOrderContract.Price;
                            if (itemOrderContract.CATRoutingID > 0)
                            {
                                var itemPrice = lstPriceFTLGroup.OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price).FirstOrDefault(c => c.RoutingID == itemOrderContract.CATRoutingID && c.GroupOfVehicleID == itemOrderContract.GroupOfVehicleID);
                                if (itemPrice != null)
                                    tempPrice = itemPrice.Price;
                            }
                            else
                            {
                                // Tính giá cung đường cao nhất
                                foreach (var itemOrderGroupProductContract in queryORDGroupProductContract.Where(c => c.OrderID == itemOrderContract.ID))
                                {
                                    var itemPrice = lstPriceFTLGroup.Where(c => c.GroupOfVehicleID == itemOrderGroupProductContract.GroupOfVehicleID && c.RoutingID == itemOrderGroupProductContract.CATRoutingID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price).FirstOrDefault();
                                    if (itemPrice == null)
                                        itemPrice = lstPriceFTLGroup.Where(c => c.GroupOfVehicleID == itemOrderGroupProductContract.GroupOfVehicleID && c.LocationFromID == itemOrderGroupProductContract.LocationFromID && c.LocationToID == itemOrderGroupProductContract.LocationToID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price).FirstOrDefault();

                                    if (itemPrice != null && itemPrice.Price > tempPrice)
                                    {
                                        tempPrice = itemPrice.Price;
                                        itemOrderContract.CATRoutingID = itemPrice.RoutingID;
                                    }
                                }
                            }
                            itemOrderContract.Price = tempPrice;
                            totalPrice += itemOrderContract.Price;
                            foreach (var itemOrderGroupProductContract in queryORDGroupProductContract.Where(c => c.OrderID == itemOrderContract.ID))
                                itemOrderGroupProductContract.Price = tempPrice;

                            foreach (var itemOrderGroupProductContract in queryOPSGroupProductContract.Where(c => c.OrderID == itemOrderContract.ID))
                                itemOrderGroupProductContract.Price = tempPrice;
                        }

                        //Bảng giá FTL bậc thang
                        var lstPriceFTLLevel = ItemInput.ListFTLLevel.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig);
                        if (lstPriceFTLLevel.Count() > 0)
                        {
                            foreach (var itemOrderContract in queryOrderContract.Where(c => c.GroupOfVehicleID > 0 && c.TransportModeID == -(int)SYSVarType.TransportModeFTL))
                            {
                                decimal tempPrice = 0;
                                // Tính theo cung đường đã có
                                if (itemOrderContract.CATRoutingID > 0)
                                {
                                    var lstPriceLevel = lstPriceFTLLevel.Where(c => c.GroupOfVehicleID == itemOrderContract.GroupOfVehicleID && c.RoutingID == itemOrderContract.CATRoutingID).Select(c => new
                                    {
                                        DateStart = c.DateStart,
                                        DateEnd = c.DateEnd,
                                        c.RoutingID,
                                        c.Price
                                    }).ToList();
                                    if (lstPriceLevel != null && lstPriceLevel.Count > 0)
                                    {
                                        foreach (var priceGV in lstPriceLevel)
                                        {
                                            // Ngày
                                            if (priceGV.DateStart <= priceGV.DateEnd)
                                            {
                                                if (priceGV.DateStart.TimeOfDay <= itemOrderContract.DateConfig.TimeOfDay && itemOrderContract.DateConfig.TimeOfDay < priceGV.DateStart.TimeOfDay)
                                                    if (priceGV.Price > tempPrice)
                                                    {
                                                        tempPrice = priceGV.Price;
                                                        itemOrderContract.CATRoutingID = priceGV.RoutingID;
                                                    }
                                            }
                                            else
                                            {
                                                // Đêm
                                                if (priceGV.DateStart.TimeOfDay <= itemOrderContract.DateConfig.TimeOfDay || itemOrderContract.DateConfig.TimeOfDay < priceGV.DateStart.TimeOfDay)
                                                    if (priceGV.Price > tempPrice)
                                                    {
                                                        tempPrice = priceGV.Price;
                                                        itemOrderContract.CATRoutingID = priceGV.RoutingID;
                                                    }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // Tính giá cung đường cao nhất của group
                                    foreach (var itemOrderGroupProductContract in queryORDGroupProductContract.Where(c => c.OrderID == itemOrderContract.ID))
                                    {
                                        var lstPriceLevel = lstPriceFTLLevel.Where(c => c.GroupOfVehicleID == itemOrderContract.GroupOfVehicleID && c.RoutingID == itemOrderGroupProductContract.CATRoutingID).Select(c => new
                                        {
                                            DateStart = c.DateStart,
                                            DateEnd = c.DateEnd,
                                            c.RoutingID,
                                            c.Price
                                        }).ToList();
                                        if (lstPriceLevel.Count == 0)
                                        {
                                            lstPriceLevel = lstPriceFTLLevel.Where(c => c.GroupOfVehicleID == itemOrderContract.GroupOfVehicleID && c.LocationFromID == itemOrderGroupProductContract.LocationFromID && c.LocationToID == itemOrderGroupProductContract.LocationToID).Select(c => new
                                            {
                                                DateStart = c.DateStart,
                                                DateEnd = c.DateEnd,
                                                c.RoutingID,
                                                c.Price
                                            }).ToList();
                                        }
                                        if (lstPriceLevel != null && lstPriceLevel.Count > 0)
                                        {
                                            foreach (var priceGV in lstPriceLevel)
                                            {
                                                // Ngày
                                                if (priceGV.DateStart <= priceGV.DateEnd)
                                                {
                                                    if (priceGV.DateStart.TimeOfDay <= itemOrderContract.DateConfig.TimeOfDay && itemOrderContract.DateConfig.TimeOfDay < priceGV.DateStart.TimeOfDay)
                                                        if (priceGV.Price > tempPrice)
                                                        {
                                                            tempPrice = priceGV.Price;
                                                            itemOrderContract.CATRoutingID = priceGV.RoutingID;
                                                        }

                                                }
                                                else
                                                {
                                                    // Đêm
                                                    if (priceGV.DateStart.TimeOfDay <= itemOrderContract.DateConfig.TimeOfDay || itemOrderContract.DateConfig.TimeOfDay < priceGV.DateStart.TimeOfDay)
                                                        if (priceGV.Price > tempPrice)
                                                        {
                                                            tempPrice = priceGV.Price;
                                                            itemOrderContract.CATRoutingID = priceGV.RoutingID;
                                                        }
                                                }
                                            }
                                        }
                                    }
                                }
                                itemOrderContract.Price = tempPrice;
                                totalPrice += itemOrderContract.Price;
                            }
                        }
                        #endregion

                        #region Bảng giá bốc xếp
                        var lstPriceLoadGroup = ItemInput.ListLoad.Where(c => c.ContractID == itemContract.ID && c.IsLoading && c.EffectDate <= DateConfig);
                        var lstPriceUnLoadGroup = ItemInput.ListLoad.Where(c => c.ContractID == itemContract.ID && !c.IsLoading && c.EffectDate <= DateConfig);
                        foreach (var itemOPSGroupProductContract in queryOPSGroupProductContract)
                        {
                            var priceLoad = lstPriceLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.LocationID == itemOPSGroupProductContract.LocationFromID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceLoad == null)
                                priceLoad = lstPriceLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.PartnerID == itemOPSGroupProductContract.PartnerID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceLoad == null)
                                priceLoad = lstPriceLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.GroupOfLocationID == itemOPSGroupProductContract.GroupOfLocationID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceLoad == null)
                                priceLoad = lstPriceLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.RoutingID == itemOPSGroupProductContract.CATRoutingID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceLoad == null)
                                priceLoad = lstPriceLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceLoad != null)
                            {
                                itemOPSGroupProductContract.IsLoading = true;
                                itemOPSGroupProductContract.UnitPriceLoad = priceLoad.Price;
                                if (priceLoad.PriceOfGOPID == iTon)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.TonTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.Ton;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.TonBBGN;
                                }
                                else if (priceLoad.PriceOfGOPID == iCBM)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.CBMTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.CBM;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.CBMBBGN;
                                }
                                else if (priceLoad.PriceOfGOPID == iQuantity)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.QuantityTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.Quantity;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.QuantityBBGN;
                                }

                                itemOPSGroupProductContract.PriceLoad = itemOPSGroupProductContract.UnitPriceLoad * (decimal)itemOPSGroupProductContract.QuantityLoad;
                                totalLoadPrice += itemOPSGroupProductContract.PriceLoad;
                            }

                            var priceUnLoad = lstPriceUnLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.LocationID == itemOPSGroupProductContract.LocationToID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceUnLoad == null)
                                priceUnLoad = lstPriceUnLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.PartnerID == itemOPSGroupProductContract.PartnerID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceUnLoad == null)
                                priceUnLoad = lstPriceUnLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.GroupOfLocationID == itemOPSGroupProductContract.GroupOfLocationID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceUnLoad == null)
                                priceUnLoad = lstPriceUnLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.RoutingID == itemOPSGroupProductContract.CATRoutingID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceUnLoad == null)
                                priceUnLoad = lstPriceUnLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceUnLoad != null)
                            {
                                itemOPSGroupProductContract.IsUnLoading = true;
                                itemOPSGroupProductContract.UnitPriceUnLoad = priceUnLoad.Price;
                                if (priceUnLoad.PriceOfGOPID == iTon)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.TonTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.Ton;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.TonBBGN;
                                }
                                else if (priceUnLoad.PriceOfGOPID == iCBM)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.CBMTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.CBM;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.CBMBBGN;
                                }
                                else if (priceUnLoad.PriceOfGOPID == iQuantity)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.QuantityTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.Quantity;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.QuantityBBGN;
                                }

                                itemOPSGroupProductContract.PriceUnLoad = itemOPSGroupProductContract.UnitPriceUnLoad * (decimal)itemOPSGroupProductContract.QuantityUnLoad;
                                totalUnLoadPrice += itemOPSGroupProductContract.PriceUnLoad;
                            }
                        }

                        // Cập nhật giá bốc xếp
                        foreach (var itemOrderContract in queryOrderContract)
                        {
                            var lstGroupLoad = queryOPSGroupProductContract.Where(c => c.OrderID == itemOrderContract.ID && c.IsLoading);
                            if (lstGroupLoad != null && lstGroupLoad.Count() > 0)
                            {
                                itemOrderContract.IsLoading = true;
                                itemOrderContract.PriceLoad = lstGroupLoad.Sum(c => c.PriceLoad);
                            }

                            var lstGroupUnLoad = queryOPSGroupProductContract.Where(c => c.OrderID == itemOrderContract.ID && c.IsUnLoading);
                            if (lstGroupUnLoad != null && lstGroupUnLoad.Count() > 0)
                            {
                                itemOrderContract.IsUnLoading = true;
                                itemOrderContract.PriceUnLoad = lstGroupUnLoad.Sum(c => c.PriceUnLoad);
                            }
                        }
                        #endregion

                        System.Diagnostics.Debug.WriteLine("Lấy dữ liệu");

                        #region Vận chuyển và MOQ
                        var lstMOQ = ItemInput.ListMOQ.Where(c => c.ContractID == itemContract.ID && !string.IsNullOrEmpty(c.ExprInput) && c.EffectDate <= DateConfig).ToList();
                        var MOQEffateDate = lstMOQ.Select(c => new { c.EffectDate }).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                        if (MOQEffateDate != null)
                            lstMOQ = lstMOQ.Where(c => c.EffectDate == MOQEffateDate.EffectDate).ToList();
                        //Chạy từng MOQ
                        string strMOQName = string.Empty;
                        foreach (var itemMOQ in lstMOQ)
                        {
                            System.Diagnostics.Debug.WriteLine("MOQ vận chuyển: " + itemMOQ.MOQName);

                            var lstOrderMOQ = new List<HelperFinance_Order>();
                            var lstOPSGroupMOQ = new List<HelperFinance_OPSGroupProduct>();

                            // MOQ
                            var queryOrderMOQ = queryOrderContract.ToList();
                            var queryOPSGroupMOQ = queryOPSGroupProductContract.ToList();
                            var queryORDGroupMOQ = queryORDGroupProductContract.ToList();

                            strMOQName = itemMOQ.MOQName;
                            //Danh sách các điều kiện lọc 
                            var lstMOQParentRouting = itemMOQ.ListParentRouting;
                            var lstMOQRouting = itemMOQ.ListRouting;
                            var lstMOQGroupLocation = itemMOQ.ListGroupOfLocation;
                            var lstMOQPartner = itemMOQ.ListPartnerID;
                            var lstMOQProvince = itemMOQ.ListProvinceID;
                            var lstMOQLocationFrom = itemMOQ.ListLocation.Where(c => c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                                .Select(c => c.LocationID).ToList();
                            var lstMOQLocationTo = itemMOQ.ListLocation.Where(c => (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                                .Select(c => c.LocationID).ToList();
                            var lstMOQGroupProduct = itemMOQ.ListGroupProduct.Select(c => c.GroupOfProductID).Distinct().ToList();

                            if (lstMOQParentRouting.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQParentRouting.Contains(c.ParentRoutingID)).ToList();
                            if (lstMOQRouting.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQRouting.Contains(c.CATRoutingID)).ToList();
                            if (lstMOQGroupLocation.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQGroupLocation.Contains(c.GroupOfLocationID)).ToList();
                            if (lstMOQLocationFrom.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQLocationFrom.Contains(c.LocationFromID)).ToList();
                            if (lstMOQLocationTo.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQLocationTo.Contains(c.LocationToID)).ToList();
                            if (lstMOQGroupProduct.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQGroupProduct.Contains(c.GroupOfProductID)).ToList();
                            if (lstMOQPartner.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQPartner.Contains(c.PartnerID)).ToList();
                            if (lstMOQProvince.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQProvince.Contains(c.LocationToProvinceID)).ToList();

                            var moqOrderID = queryOrderMOQ.Select(c => c.ID).ToArray();
                            var moqGroupID = queryOPSGroupMOQ.Select(c => c.OrderID).Distinct().ToArray();
                            var lstOrderCheckID = moqGroupID.Intersect(moqOrderID).ToList();
                            var lstOrderCheck = queryOrderMOQ.Where(c => lstOrderCheckID.Contains(c.ID)).ToList();
                            var lstOrderGroupCheck = queryOPSGroupMOQ.Where(c => lstOrderCheckID.Contains(c.OrderID)).ToList();

                            if (lstOrderCheck.Count > 0 && lstOrderGroupCheck.Count > 0)
                            {
                                #region Tính theo ngày
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderInDay || itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumScheduleInDay)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceDIExExpr itemExpr = Expr_Generate(lstOrderGroupCheck);
                                    itemExpr.Credit = totalPrice;
                                    itemExpr.UnitPriceMax = lstOrderGroupCheck.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                    itemExpr.UnitPriceMin = lstOrderGroupCheck.OrderBy(c => c.Price).FirstOrDefault().Price;
                                    var lstProvinceID = lstOrderGroupCheck.Select(c => c.LocationToProvinceID).Distinct().ToList();
                                    itemExpr.IsHasPartnerProvince = queryOPSGroupProductContract.Any(c => !lstMOQGroupLocation.Contains(c.GroupOfLocationID) && lstProvinceID.Contains(c.LocationToProvinceID));
                                    try
                                    {
                                        flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                    }
                                    catch { flag = false; }

                                    if (flag == true)
                                    {
                                        lstOrderMOQ = lstOrderCheck;
                                        lstOPSGroupMOQ = lstOrderGroupCheck;
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrder)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstOrderMOQ.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupMOQ.AddRange(itemGroup);
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumSchedule)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }

                                        if (flag == true)
                                        {
                                            var lstTempOrderID = itemGroup.Select(c => c.OrderID).Distinct().ToList();
                                            foreach (var tempOrderID in lstTempOrderID)
                                            {
                                                if (lstOrderMOQ.Count(c => c.ID == tempOrderID) == 0)
                                                    lstOrderMOQ.Add(lstOrderCheck.FirstOrDefault(c => c.ID == tempOrderID));
                                            }
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo location
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderLocation)
                                {
                                    //Thực hiện công thức cho từng Location
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.LocationToID))
                                    {
                                        bool flag = false;
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempOrderID = itemGroup.Select(c => c.OrderID).Distinct().ToList();
                                            foreach (var tempOrderID in lstTempOrderID)
                                            {
                                                if (lstOrderMOQ.Count(c => c.ID == tempOrderID) == 0)
                                                    lstOrderMOQ.Add(lstOrderCheck.FirstOrDefault(c => c.ID == tempOrderID));
                                            }
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToList());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo route
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderRoute)
                                {
                                    //Thực hiện công thức cho từng Location
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.CATRoutingID))
                                    {
                                        bool flag = false;
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempOrderID = itemGroup.Select(c => c.OrderID).Distinct().ToList();
                                            foreach (var tempOrderID in lstTempOrderID)
                                            {
                                                if (lstOrderMOQ.Count(c => c.ID == tempOrderID) == 0)
                                                    lstOrderMOQ.Add(lstOrderCheck.FirstOrDefault(c => c.ID == tempOrderID));
                                            }
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng trả về - chỉ phát sinh price ko change price
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumReturnOrder)
                                {
                                    lstOrderGroupCheck = lstOrderGroupCheck.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstOrderMOQ.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến hàng trả về
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumReturnSchedule)
                                {
                                    lstOrderGroupCheck = lstOrderGroupCheck.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = false;
                                        decimal Credit = 0;
                                        var lstOrderID = itemGroup.Select(c => c.OrderID).Distinct().ToList();
                                        foreach (var itemOrderID in lstOrderID)
                                        {
                                            var order = ItemInput.ListOrder.FirstOrDefault(c => c.ID == itemOrderID);
                                            if (order != null)
                                                Credit += order.Price;
                                        }
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = Credit;
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempOrderID = itemGroup.Select(c => c.OrderID).Distinct().ToList();
                                            foreach (var tempOrderID in lstTempOrderID)
                                            {
                                                if (lstOrderMOQ.Count(c => c.ID == tempOrderID) == 0)
                                                    lstOrderMOQ.Add(lstOrderCheck.FirstOrDefault(c => c.ID == tempOrderID));
                                            }
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion
                            }

                            //Thực hiện lấy output MOQ
                            if (itemMOQ.DIMOQSumID != -(int)SYSVarType.DIMOQSumReturnSchedule && itemMOQ.DIMOQSumID != -(int)SYSVarType.DIMOQSumReturnOrder)
                            {

                                foreach (var itemOrderMOQ in lstOrderMOQ)
                                {
                                    itemOrderMOQ.HasMOQ = true;
                                }
                                foreach (var itemOrderGroupMOQ in lstOPSGroupMOQ)
                                {
                                    itemOrderGroupMOQ.HasMOQ = true;
                                }
                            }

                            if (lstOrderMOQ.Count > 0 && lstOPSGroupMOQ.Count > 0)
                            {
                                //Thực hiện nếu output là fix price
                                #region Tính theo ngày
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderInDay || itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumScheduleInDay)
                                {
                                    //Thực hiện công thức
                                    DTOPriceDIExExpr itemExpr = Expr_Generate(lstOPSGroupMOQ);
                                    itemExpr.Credit = totalPrice;
                                    itemExpr.UnitPriceMax = lstOPSGroupMOQ.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                    itemExpr.UnitPriceMin = lstOPSGroupMOQ.OrderBy(c => c.Price).FirstOrDefault().Price;

                                    decimal? priceFix = null, priceMOQ = null, tonMOQ = null, cbmMOQ = null, quantityMOQ = null;

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                        priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                        tonMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                        cbmMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                        quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                    // PL tạm
                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.ContractID = itemContract.ID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                    {
                                        // set về 0
                                        foreach (var tempOrderGroupLocationCheck in lstOPSGroupMOQ)
                                            tempOrderGroupLocationCheck.Price = 0;

                                        if (priceFix.HasValue)
                                        {
                                            FIN_PLDetails plCost = new FIN_PLDetails();
                                            plCost.CreatedBy = Account.UserName;
                                            plCost.CreatedDate = DateTime.Now;
                                            plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                            plCost.Note = itemMOQ.MOQName;
                                            plCost.Credit = priceFix.Value;
                                            plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                            pl.FIN_PLDetails.Add(plCost);
                                            pl.Credit += plCost.Credit;

                                            var lstOPSGroupID = lstOPSGroupMOQ.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                            DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                            lstPl.Add(pl);
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.Quantity = tonMOQ.HasValue ? (double)tonMOQ.Value : cbmMOQ.HasValue ? (double)cbmMOQ.Value : quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = lstOPSGroupMOQ.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                        if (lstPriceMOQGroupProduct.Count() > 0)
                                        {
                                            var lstPriceMOQGroupProductID = lstPriceMOQGroupProduct.Select(c => c.GroupOfProductID).Distinct().ToList();
                                            var lstOPSGroupCheck = lstOPSGroupMOQ.Where(c => lstPriceMOQGroupProductID.Contains(c.GroupOfProductID));
                                            double totalCMBMOQ = 0, totalTonMOQ = 0;
                                            if (lstOPSGroupCheck.Count() > 0)
                                            {
                                                var lstTonMOQ = lstOPSGroupCheck.Where(c => c.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTon);
                                                if (lstTonMOQ.Count() > 0)
                                                    totalTonMOQ = lstTonMOQ.Sum(c => c.TonTranfer);
                                                var lstCBMMOQ = lstOPSGroupCheck.Where(c => c.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPCBM);
                                                if (lstCBMMOQ.Count() > 0)
                                                    totalCMBMOQ = lstCBMMOQ.Sum(c => c.CBMTranfer);
                                            }
                                            foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                            {
                                                foreach (var tempOrderGroupLocationCheck in lstOPSGroupMOQ.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                {
                                                    DTOPriceDIExExpr itemExprGroup = Expr_GenerateItem(tempOrderGroupLocationCheck);
                                                    itemExprGroup.UnitPrice = tempOrderGroupLocationCheck.Price;
                                                    itemExprGroup.UnitPriceMax = itemExpr.UnitPriceMax;
                                                    itemExprGroup.UnitPriceMin = itemExpr.UnitPriceMin;
                                                    itemExprGroup.TonMOQ = totalTonMOQ;
                                                    itemExprGroup.CBMMOQ = totalCMBMOQ;
                                                    itemExprGroup.TotalTonTransfer = lstOPSGroupMOQ.Sum(c => c.TonTranfer);
                                                    itemExprGroup.TotalCBMTransfer = lstOPSGroupMOQ.Sum(c => c.CBMTranfer);
                                                    itemExprGroup.TotalQuantityTransfer = lstOPSGroupMOQ.Sum(c => c.QuantityTranfer);
                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExprGroup);
                                                    if (priceGroupMOQ.HasValue)
                                                        tempOrderGroupLocationCheck.Price = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExprGroup);
                                                    if (quantityGroupMOQ.HasValue)
                                                        tempOrderGroupLocationCheck.QuantityMOQ = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            foreach (var tempOrderGroupLocationCheck in lstOPSGroupMOQ)
                                                tempOrderGroupLocationCheck.Price = 0;
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrder)
                                {
                                    // Tính MOQ theo từng đơn hàng
                                    foreach (var itemOrderMOQ in lstOrderMOQ)
                                    {
                                        var lstOPSGroup = lstOPSGroupMOQ.Where(c => c.OrderID == itemOrderMOQ.ID).ToList();
                                        #region FTL
                                        if (itemOrderMOQ.TransportModeID == -(int)SYSVarType.TransportModeFTL)
                                        {
                                            itemOrderMOQ.Price = 0;
                                            //Thực hiện công thức
                                            DTOPriceDIExExpr itemExpr = Expr_Generate(lstOPSGroup);
                                            itemExpr.Credit = itemOrderMOQ.Price;

                                            decimal? priceFix = null, priceMOQ = null;

                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                                priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                                priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PL pl = new FIN_PL();
                                                pl.IsPlanning = false;
                                                pl.Effdate = DateConfig.Date;
                                                pl.Code = string.Empty;
                                                pl.CreatedBy = Account.UserName;
                                                pl.CreatedDate = DateTime.Now;
                                                pl.SYSCustomerID = Account.SYSCustomerID;
                                                pl.CustomerID = itemContract.CustomerID;
                                                pl.ContractID = itemContract.ID;
                                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.Note1 = itemOrderMOQ.OrderCode;
                                                plCost.Credit = priceFix.Value;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = lstOPSGroupMOQ.Where(c => c.OrderID == itemOrderMOQ.ID).OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                            else
                                            {
                                                if (priceMOQ.HasValue)
                                                    itemOrderMOQ.Price = priceMOQ.Value;
                                            }
                                        }
                                        #endregion

                                        #region LTL
                                        else
                                        {
                                            FIN_PL pl = new FIN_PL();
                                            pl.IsPlanning = false;
                                            pl.Effdate = DateConfig.Date;
                                            pl.Code = string.Empty;
                                            pl.CreatedBy = Account.UserName;
                                            pl.CreatedDate = DateTime.Now;
                                            pl.SYSCustomerID = Account.SYSCustomerID;
                                            pl.CustomerID = itemContract.CustomerID;
                                            pl.ContractID = itemContract.ID;
                                            pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                            //Thực hiện công thức
                                            DTOPriceDIExExpr itemExpr = Expr_Generate(lstOPSGroup);
                                            itemExpr.Credit = itemOrderMOQ.Price;

                                            decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                                priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                                priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                            if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                                quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                            if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                                quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                            if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                                quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                            // Fix Cost
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            {
                                                // set về 0
                                                foreach (var itemOPSGroupMOQ in lstOPSGroupMOQ.Where(c => c.OrderID == itemOrderMOQ.ID))
                                                    itemOPSGroupMOQ.Price = 0;

                                                if (priceFix.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.Credit = priceFix.Value;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Credit += plCost.Credit;

                                                    var lstOPSGroupID = lstOPSGroupMOQ.Where(c => c.OrderID == itemOrderMOQ.ID).OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }
                                            else
                                            {
                                                if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                                {
                                                    if (priceMOQ.HasValue && quantityMOQ.HasValue)
                                                    {
                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                        plCost.Note = itemMOQ.MOQName;
                                                        plCost.UnitPrice = priceMOQ.Value;
                                                        plCost.Quantity = (double)quantityMOQ.Value;
                                                        plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                        plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        pl.Credit += plCost.Credit;

                                                        var lstOPSGroupID = lstOPSGroupMOQ.Where(c => c.OrderID == itemOrderMOQ.ID).OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                        DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                        lstPl.Add(pl);
                                                    }
                                                }

                                                // Change giá + sản lượng
                                                var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                                if (lstPriceMOQGroupProduct.Count() > 0)
                                                {
                                                    foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                    {
                                                        foreach (var itemOPSGroupMOQ in lstOPSGroupMOQ.Where(c => c.OrderID == itemOrderMOQ.ID && c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                        {
                                                            itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);
                                                            itemExpr.UnitPrice = itemOPSGroupMOQ.Price;

                                                            decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                            if (priceGroupMOQ.HasValue)
                                                                itemOPSGroupMOQ.Price = priceGroupMOQ.Value;

                                                            decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);
                                                            if (quantityGroupMOQ.HasValue)
                                                                itemOPSGroupMOQ.QuantityMOQ = (double)quantityGroupMOQ.Value;
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    // set về 0
                                                    foreach (var itemOPSGroupMOQ in lstOPSGroupMOQ.Where(c => c.OrderID == itemOrderMOQ.ID))
                                                        itemOPSGroupMOQ.Price = 0;
                                                }
                                            }
                                        }
                                        #endregion
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumSchedule)
                                {
                                    foreach (var itemGroup in lstOPSGroupMOQ.GroupBy(c => c.DITOMasterID))
                                    {
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());

                                        var lstOrderID = itemGroup.Select(c => c.OrderID).Distinct().ToList();
                                        var lstPriceOrder = lstOrderMOQ.Where(c => lstOrderID.Contains(c.ID));
                                        if (lstPriceOrder.Count() > 0)
                                            itemExpr.Credit = lstPriceOrder.Sum(c => c.Price);

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            // set về 0
                                            foreach (var itemOPSGroupMOQ in itemGroup)
                                                itemOPSGroupMOQ.Price = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.Credit = priceFix.Value;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Credit += plCost.Credit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            // Change giá + sản lượng
                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        if (priceGroupMOQ.HasValue)
                                                            itemOPSGroupMOQ.Price = priceGroupMOQ.Value;

                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);
                                                        if (quantityGroupMOQ.HasValue)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)quantityGroupMOQ.Value;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // set về 0
                                                foreach (var itemOPSGroupMOQ in itemGroup)
                                                    itemOPSGroupMOQ.Price = 0;
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo Location
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderLocation)
                                {
                                    // Thực hiện công thức cho từng Location
                                    foreach (var itemGroup in lstOPSGroupMOQ.GroupBy(c => c.LocationToID))
                                    {
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        decimal? priceFix = null, priceMOQ = null, tonMOQ = null, cbmMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            tonMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            cbmMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            // set về 0
                                            foreach (var tempOrderGroupLocationCheck in itemGroup)
                                                tempOrderGroupLocationCheck.Price = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.Credit = priceFix.Value;
                                                plCost.Note1 = itemGroup.FirstOrDefault().LocationToName;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.Quantity = tonMOQ.HasValue ? (double)tonMOQ.Value : cbmMOQ.HasValue ? (double)cbmMOQ.Value : quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    plCost.Note1 = itemGroup.FirstOrDefault().LocationToName;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Credit += plCost.Credit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var tempOrderGroupLocationCheck in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        DTOPriceDIExExpr itemExprGroup = Expr_GenerateItem(tempOrderGroupLocationCheck);
                                                        itemExprGroup.UnitPrice = tempOrderGroupLocationCheck.Price;
                                                        itemExprGroup.UnitPriceMax = itemExpr.UnitPriceMax;
                                                        itemExprGroup.UnitPriceMin = itemExpr.UnitPriceMin;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExprGroup);
                                                        if (priceGroupMOQ.HasValue)
                                                            tempOrderGroupLocationCheck.Price = priceGroupMOQ.Value;

                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExprGroup);
                                                        if (quantityGroupMOQ.HasValue)
                                                            tempOrderGroupLocationCheck.QuantityMOQ = (double)quantityGroupMOQ.Value;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                foreach (var tempOrderGroupLocationCheck in itemGroup)
                                                    tempOrderGroupLocationCheck.Price = 0;
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo Routing
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderRoute)
                                {
                                    //Thực hiện công thức cho từng Location
                                    foreach (var itemOrder in lstOPSGroupMOQ.GroupBy(c => c.CATRoutingID))
                                    {
                                        // PL tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemOrder.ToList());
                                        itemExpr.UnitPrice = itemOrder.FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemOrder.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemOrder.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        decimal? priceFix = null, priceMOQ = null, tonMOQ = null, cbmMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            tonMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            cbmMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            // set về 0
                                            foreach (var tempOrderGroupLocationCheck in itemOrder)
                                                tempOrderGroupLocationCheck.Price = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.Credit = priceFix.Value;
                                                plCost.Note1 = itemOrder.FirstOrDefault().CATRoutingName;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemOrder.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.Quantity = tonMOQ.HasValue ? (double)tonMOQ.Value : cbmMOQ.HasValue ? (double)cbmMOQ.Value : quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    plCost.Note1 = itemOrder.FirstOrDefault().CATRoutingName;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Credit += plCost.Credit;

                                                    var lstOPSGroupID = itemOrder.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var tempOrderGroupLocationCheck in itemOrder.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        DTOPriceDIExExpr itemExprGroup = Expr_GenerateItem(tempOrderGroupLocationCheck);
                                                        itemExprGroup.UnitPrice = tempOrderGroupLocationCheck.Price;
                                                        itemExprGroup.UnitPriceMax = itemExpr.UnitPriceMax;
                                                        itemExprGroup.UnitPriceMin = itemExpr.UnitPriceMin;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExprGroup);
                                                        if (priceGroupMOQ.HasValue)
                                                            tempOrderGroupLocationCheck.Price = priceGroupMOQ.Value;

                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExprGroup);
                                                        if (quantityGroupMOQ.HasValue)
                                                            tempOrderGroupLocationCheck.QuantityMOQ = (double)quantityGroupMOQ.Value;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                foreach (var tempOrderGroupLocationCheck in itemOrder)
                                                    tempOrderGroupLocationCheck.Price = 0;
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng trả về - chỉ phát sinh price ko change price
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumReturnOrder)
                                {
                                    lstOPSGroupMOQ = lstOPSGroupMOQ.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOPSGroupMOQ.GroupBy(c => c.OrderID))
                                    {
                                        // Tạo pl temp
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.FirstOrDefault().Price;
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.Credit = priceFix.Value;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Credit += plCost.Credit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);
                                                        itemExpr.UnitPrice = itemOPSGroupMOQ.Price;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);

                                                        if (quantityGroupMOQ == null)
                                                            if (itemOPSGroupMOQ.PriceOfGOPID == iTon)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.TonReturn;
                                                            else if (itemOPSGroupMOQ.PriceOfGOPID == iCBM)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.CBMReturn;
                                                            else if (itemOPSGroupMOQ.PriceOfGOPID == iQuantity)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.QuantityReturn;

                                                        if (itemOPSGroupMOQ.PriceOfGOPID == iTon)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.TonTranfer;
                                                        else if (itemOPSGroupMOQ.PriceOfGOPID == iCBM)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.CBMTranfer;
                                                        else if (itemOPSGroupMOQ.PriceOfGOPID == iQuantity)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.QuantityTranfer;

                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.DITOReturnCredit;
                                                        plCost.Note = itemMOQ.MOQName;
                                                        plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        lstPl.Add(pl);

                                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.GroupOfProductID = itemOPSGroupMOQ.ID;
                                                        plGroup.Quantity = quantityGroupMOQ.HasValue ? (double)quantityGroupMOQ.Value : 0;
                                                        plGroup.UnitPrice = priceGroupMOQ.HasValue ? priceGroupMOQ.Value : 0;
                                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                        plCost.Credit += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                                                        pl.Credit += plCost.Credit;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến trả về
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumReturnSchedule)
                                {
                                    lstOPSGroupMOQ = lstOPSGroupMOQ.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOPSGroupMOQ.GroupBy(c => c.DITOMasterID))
                                    {
                                        // Tạo pl temp
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        decimal Credit = 0;
                                        var lstOrderID = itemGroup.Select(c => c.OrderID).Distinct().ToList();
                                        foreach (var itemOrderID in lstOrderID)
                                        {
                                            var order = ItemInput.ListOrder.FirstOrDefault(c => c.ID == itemOrderID);
                                            if (order != null)
                                                Credit += order.Price;
                                        }

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = Credit;
                                        itemExpr.UnitPrice = Credit;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.Credit = priceFix.Value;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupCredit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Credit += plCost.Credit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);
                                                        itemExpr.UnitPrice = itemOPSGroupMOQ.Price;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);

                                                        if (quantityGroupMOQ == null)
                                                            if (itemOPSGroupMOQ.PriceOfGOPID == iTon)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.TonReturn;
                                                            else if (itemOPSGroupMOQ.PriceOfGOPID == iCBM)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.CBMReturn;
                                                            else if (itemOPSGroupMOQ.PriceOfGOPID == iQuantity)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.QuantityReturn;

                                                        if (itemOPSGroupMOQ.PriceOfGOPID == iTon)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.TonTranfer;
                                                        else if (itemOPSGroupMOQ.PriceOfGOPID == iCBM)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.CBMTranfer;
                                                        else if (itemOPSGroupMOQ.PriceOfGOPID == iQuantity)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.QuantityTranfer;

                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.DITOReturnCredit;
                                                        plCost.Note = itemMOQ.MOQName;
                                                        plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        lstPl.Add(pl);

                                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.GroupOfProductID = itemOPSGroupMOQ.ID;
                                                        plGroup.Quantity = quantityGroupMOQ.HasValue ? (double)quantityGroupMOQ.Value : 0;
                                                        plGroup.UnitPrice = priceGroupMOQ.HasValue ? priceGroupMOQ.Value : 0;
                                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                        plCost.Credit += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                                                        pl.Credit += plCost.Credit;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }

                        #region Ghi dữ liệu giá chính và MOQ vào hệ thống
                        foreach (var itemOrder in ItemInput.ListOrder.Where(c => c.ContractID == itemContract.ID))
                        {
                            var lstOPSGroup = ItemInput.ListOPSGroupProduct.Where(c => c.OrderID == itemOrder.ID).ToList();

                            var lstMasterID = lstOPSGroup.Select(c => c.DITOMasterID).Distinct().ToList();
                            var lstMaster = ItemInput.ListOPSGroupProduct.Where(c => lstMasterID.Contains(c.DITOMasterID)).Select(c => new { c.VendorID, c.DITOMasterID, c.VehicleID }).Distinct().ToList();
                            foreach (var master in lstMaster)
                            {
                                var lstOPSGroupMaster = lstOPSGroup.Where(c => c.DITOMasterID == master.DITOMasterID);

                                #region Giá vận chuyển
                                FIN_PL pl = new FIN_PL();
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.Code = string.Empty;
                                pl.IsPlanning = false;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.Effdate = DateConfig.Date;
                                pl.DITOMasterID = master.DITOMasterID;
                                pl.VendorID = master.VendorID;
                                pl.CustomerID = itemOrder.CustomerID;
                                pl.OrderID = itemOrder.ID;
                                pl.ContractID = itemContract.ID;
                                pl.VehicleID = master.VehicleID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                if (itemOrder.TransportModeID == -(int)SYSVarType.TransportModeFTL)
                                {
                                    FIN_PLDetails plDetail = new FIN_PLDetails();
                                    plDetail.CreatedBy = Account.UserName;
                                    plDetail.CreatedDate = DateTime.Now;
                                    plDetail.Note = itemOrder.HasMOQ ? strMOQName : string.Empty;
                                    plDetail.CostID = (int)CATCostType.DITOFreightCredit;
                                    pl.FIN_PLDetails.Add(plDetail);

                                    // Nếu đơn FTL bị tách nhiều chuyến thì chỉ tính cho 1 chuyến, các chuyến còn lại = 0;
                                    if (lstPl.Count(c => c.OrderID == itemOrder.ID && c.FIN_PLDetails.Any(d => d.CostID == (int)CATCostType.DITOFreightCredit)) == 0)
                                        plDetail.Credit = itemOrder.Price;

                                    pl.Credit += plDetail.Credit;

                                    if (lstOPSGroupMaster.Count() > 0)
                                    {
                                        var opsGroup = lstOPSGroupMaster.OrderByDescending(c => c.TonTranfer).FirstOrDefault();
                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                        plGroup.CreatedBy = Account.UserName;
                                        plGroup.CreatedDate = DateTime.Now;
                                        plGroup.GroupOfProductID = opsGroup.ID;
                                        plGroup.Unit = "";
                                        plGroup.UnitPrice = 0;
                                        plGroup.Quantity = 0;

                                        plDetail.FIN_PLGroupOfProduct.Add(plGroup);

                                        // Cập nhật sort cho order group
                                        string strSort = opsGroup.OrderGroupProductID.ToString();
                                        foreach (var item in lstOPSGroupMaster)
                                        {
                                            if (lstOrderGroupUpDate.Count(c => c.ID == item.OrderGroupProductID) == 0)
                                            {
                                                HelperFinance_ORDGroupProduct itemGroup = new HelperFinance_ORDGroupProduct();
                                                itemGroup.ID = item.OrderGroupProductID;
                                                if (itemGroup.ID == opsGroup.OrderGroupProductID)
                                                    itemGroup.FINSort = strSort;
                                                else
                                                    itemGroup.FINSort = strSort + "A";

                                                lstOrderGroupUpDate.Add(itemGroup);
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    foreach (var itemGroup in lstOPSGroupMaster)
                                    {
                                        FIN_PLDetails plDetail = new FIN_PLDetails();
                                        plDetail.CreatedBy = Account.UserName;
                                        plDetail.CreatedDate = DateTime.Now;
                                        plDetail.Note = itemOrder.HasMOQ ? strMOQName : string.Empty;
                                        plDetail.CostID = (int)CATCostType.DITOFreightCredit;
                                        pl.FIN_PLDetails.Add(plDetail);

                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                        plGroup.CreatedBy = Account.UserName;
                                        plGroup.CreatedDate = DateTime.Now;
                                        plGroup.GroupOfProductID = itemGroup.ID;
                                        plGroup.Unit = itemGroup.PriceOfGOPName;
                                        plGroup.UnitPrice = itemGroup.Price;
                                        if (itemGroup.PriceOfGOPID == iTon)
                                        {
                                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                plGroup.Quantity = itemGroup.TonTranfer;
                                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                plGroup.Quantity = itemGroup.Ton;
                                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                plGroup.Quantity = itemGroup.TonBBGN;
                                        }
                                        else if (itemGroup.PriceOfGOPID == iCBM)
                                        {
                                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                plGroup.Quantity = itemGroup.CBMTranfer;
                                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                plGroup.Quantity = itemGroup.CBM;
                                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                plGroup.Quantity = itemGroup.CBMBBGN;
                                        }
                                        else if (itemGroup.PriceOfGOPID == iQuantity)
                                        {
                                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                plGroup.Quantity = itemGroup.QuantityTranfer;
                                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                plGroup.Quantity = itemGroup.Quantity;
                                            if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                plGroup.Quantity = itemGroup.QuantityBBGN;
                                        }
                                        if (itemGroup.HasMOQ)
                                            plGroup.Quantity = itemGroup.QuantityMOQ;


                                        plDetail.Credit += (decimal)plGroup.Quantity * plGroup.UnitPrice;
                                        plDetail.FIN_PLGroupOfProduct.Add(plGroup);

                                        pl.Credit += plDetail.Credit;
                                    }
                                }

                                lstPl.Add(pl);
                                #endregion

                                #region Trouble
                                if (lstPlTrouble.Count(c => c.FIN_PLDetails.Any(d => d.Note1 == master.DITOMasterID.ToString())) == 0)
                                {
                                    var lstCreditTrouble = ItemInput.ListTrouble.Where(c => c.DITOMasterID == master.DITOMasterID && c.CostOfCustomer != 0 && c.TroubleCostStatusID == -(int)SYSVarType.TroubleCostStatusApproved);
                                    if (lstCreditTrouble.Count() > 0)
                                    {
                                        foreach (var item in lstCreditTrouble)
                                        {
                                            FIN_PL plTrouble = new FIN_PL();
                                            plTrouble.CreatedBy = Account.UserName;
                                            plTrouble.CreatedDate = DateTime.Now;
                                            plTrouble.Code = string.Empty;
                                            plTrouble.IsPlanning = false;
                                            plTrouble.SYSCustomerID = Account.SYSCustomerID;
                                            plTrouble.Effdate = DateConfig.Date;
                                            plTrouble.DITOMasterID = master.DITOMasterID;
                                            plTrouble.VendorID = master.VendorID;
                                            plTrouble.CustomerID = itemOrder.CustomerID;
                                            plTrouble.OrderID = itemOrder.ID;
                                            plTrouble.ContractID = itemContract.ID;
                                            plTrouble.VehicleID = master.VehicleID;
                                            plTrouble.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                            FIN_PLDetails plCreditTrouble = new FIN_PLDetails();
                                            plCreditTrouble.CreatedBy = Account.UserName;
                                            plCreditTrouble.CreatedDate = DateTime.Now;
                                            plCreditTrouble.CostID = (int)CATCostType.TroubleCredit;
                                            plCreditTrouble.Debit = 0;
                                            plCreditTrouble.Credit = item.CostOfCustomer;
                                            plCreditTrouble.Note = item.GroupOfTroubleName;
                                            plCreditTrouble.Note1 = master.DITOMasterID.ToString();
                                            plCreditTrouble.TypeOfPriceDIExCode = item.GroupOfTroubleCode;

                                            if (lstOPSGroupMaster.Count() > 0)
                                            {
                                                FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                plGroup.CreatedBy = Account.UserName;
                                                plGroup.CreatedDate = DateTime.Now;
                                                plGroup.GroupOfProductID = lstOPSGroupMaster.OrderByDescending(c => c.TonTranfer).FirstOrDefault().ID;
                                                plGroup.Unit = "";
                                                plGroup.UnitPrice = 0;
                                                plGroup.Quantity = 0;

                                                plCreditTrouble.FIN_PLGroupOfProduct.Add(plGroup);
                                            }

                                            plTrouble.FIN_PLDetails.Add(plCreditTrouble);
                                            plTrouble.Credit += plCreditTrouble.Credit;

                                            lstPlTrouble.Add(plTrouble);
                                        }
                                    }
                                }
                                #endregion
                            }
                        }
                        #endregion
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
                            var lstOPSGroupEx = new List<HelperFinance_OPSGroupProduct>();

                            var queryOrderEx = queryOrderContract.ToList();
                            var queryOPSGroupEx = queryOPSGroupProductContract.ToList();
                            var queryORDGroupEx = queryORDGroupProductContract.ToList();

                            strExName = itemEx.Note;
                            //Danh sách các điều kiện lọc 
                            var lstExParentRouting = itemEx.ListParentRouting;
                            var lstExRouting = itemEx.ListRouting;
                            var lstExGroupLocation = itemEx.ListGroupOfLocation;
                            var lstExPartner = itemEx.ListPartnerID;
                            var lstExProvince = itemEx.ListProvinceID;
                            var lstExLocationFrom = itemEx.ListLocation.Where(c => c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                                .Select(c => c.LocationID).ToList();
                            var lstExLocationTo = itemEx.ListLocation.Where(c => (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                                .Select(c => c.LocationID).ToList();
                            var lstExGroupProduct = itemEx.ListGroupProduct.Select(c => c.GroupOfProductID).Distinct().ToList();

                            if (lstExParentRouting.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExParentRouting.Contains(c.ParentRoutingID)).ToList();
                            if (lstExRouting.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExRouting.Contains(c.CATRoutingID)).ToList();
                            if (lstExGroupLocation.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExGroupLocation.Contains(c.GroupOfLocationID)).ToList();
                            if (lstExLocationFrom.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExLocationFrom.Contains(c.LocationFromID)).ToList();
                            if (lstExLocationTo.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExLocationTo.Contains(c.LocationToID)).ToList();
                            if (lstExGroupProduct.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExGroupProduct.Contains(c.GroupOfProductID)).ToList();
                            if (lstExPartner.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExPartner.Contains(c.PartnerID)).ToList();
                            if (lstExProvince.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExProvince.Contains(c.LocationToProvinceID)).ToList();


                            var exOrderID = queryOrderEx.Select(c => c.ID).ToArray();
                            var exGroupID = queryOPSGroupEx.Select(c => c.OrderID).Distinct().ToArray();
                            var lstOrderCheckID = exGroupID.Intersect(exOrderID).ToList();
                            var lstOrderCheck = queryOrderEx.Where(c => lstOrderCheckID.Contains(c.ID)).ToList();
                            var lstOrderGroupCheck = queryOPSGroupEx.Where(c => lstOrderCheckID.Contains(c.OrderID)).ToList();

                            foreach (var itemOrderCheck in lstOrderCheck)
                            {
                                itemOrderCheck.GetPointCurrent = lstOrderGroupCheck.Where(c => c.OrderID == itemOrderCheck.ID).Select(c => c.LocationFromID).Distinct().Count();
                                itemOrderCheck.DropPointCurrent = lstOrderGroupCheck.Where(c => c.OrderID == itemOrderCheck.ID).Select(c => c.LocationToID).Distinct().Count();
                            }

                            // Ktra công thức đầu vào
                            if (lstOrderCheck.Count > 0 && lstOrderGroupCheck.Count > 0)
                            {
                                #region Theo đơn hàng or chuyến trong ngày
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderInDay || itemEx.DIExSumID == -(int)SYSVarType.DIExScheduleInDay)
                                {
                                    bool flag = true;
                                    //Thực hiện công thức
                                    DTOPriceDIExExpr itemExpr = Expr_Generate(lstOrderGroupCheck);
                                    itemExpr.Credit = totalPrice;
                                    try
                                    {
                                        flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        lstOrderEx = lstOrderCheck;
                                        lstOPSGroupEx = lstOrderGroupCheck;
                                    }
                                }
                                #endregion

                                #region Theo chuyến
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSchedule)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.HasCashCollect = itemGroup.Any(c => c.HasCashCollect);
                                        itemExpr.SortConfig = itemGroup.FirstOrDefault().SortConfigMaster;
                                        itemExpr.RoutingCode = string.Join(",", itemGroup.Select(c => c.CATRoutingCode).Distinct().ToList());

                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
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
                                            lstOPSGroupEx.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Theo đơn hàng
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrder)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.HasCashCollect = itemGroup.Any(c => c.HasCashCollect);
                                        var order = lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.Key);
                                        itemExpr.DropPointCurrent = order.DropPointCurrent;
                                        itemExpr.GetPointCurrent = order.GetPointCurrent;
                                        itemExpr.SortConfig = order.SortConfig;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
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
                                            lstOPSGroupEx.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Theo điểm
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderLocation)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.LocationToID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.HasCashCollect = itemGroup.Any(c => c.HasCashCollect);
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
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
                                            lstOPSGroupEx.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Theo cung đường
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.OrderRoutingID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.HasCashCollect = itemGroup.Any(c => c.HasCashCollect);
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
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
                                            lstOPSGroupEx.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Theo thu hộ
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumCollect)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.Where(c => c.HasCashCollect == true))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_GenerateItem(itemGroup);

                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstOPSGroupEx.Add(itemGroup);
                                            if (lstOrderEx.Count(c => c.ID == itemGroup.OrderID) == 0)
                                                lstOrderEx.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.OrderID));
                                        }
                                    }
                                }
                                #endregion
                            }

                            // Ghi dữ liệu
                            if (lstOrderEx.Count > 0 && lstOPSGroupEx.Count > 0)
                            {
                                #region Theo đơn hàng or chuyến trong ngày
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderInDay || itemEx.DIExSumID == -(int)SYSVarType.DIExScheduleInDay)
                                {
                                    //Thực hiện công thức
                                    DTOPriceDIExExpr itemExpr = Expr_Generate(lstOPSGroupEx.ToList());
                                    itemExpr.Credit = totalPrice;

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

                                    var lstOPSGroupID = lstOPSGroupEx.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                    DIPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                    // Tính theo từng group 
                                    var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                    if (lstPriceExGroupProduct.Count() > 0)
                                    {
                                        // Tính phụ thu theo từng loại hàng
                                        foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                        {
                                            foreach (var itemOPSPriceExGroupProduct in lstOPSGroupEx.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID))
                                            {
                                                itemExpr = Expr_GenerateItem(itemOPSPriceExGroupProduct);
                                                if (itemOPSPriceExGroupProduct.HasCashCollect)
                                                {
                                                    itemExpr.TonCollect = itemOPSPriceExGroupProduct.TonTranfer;
                                                    itemExpr.CBMCollect = itemOPSPriceExGroupProduct.CBMTranfer;
                                                    itemExpr.QuantityCollect = itemOPSPriceExGroupProduct.QuantityTranfer;
                                                }
                                                DIPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
                                            }
                                        }
                                    }

                                    if (pl.FIN_PLDetails.Count > 0)
                                    {
                                        pl.Credit = pl.FIN_PLDetails.Sum(c => c.Credit);
                                        lstPl.Add(pl);
                                    }
                                }
                                #endregion

                                #region Theo chuyến
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSchedule)
                                {
                                    foreach (var itemGroup in lstOPSGroupEx.GroupBy(c => c.DITOMasterID))
                                    {
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        var lstCollect = itemGroup.Where(c => c.HasCashCollect == true);
                                        if (lstCollect.Count() > 0)
                                        {
                                            itemExpr.TonCollect = lstCollect.Sum(c => c.TonTranfer);
                                            itemExpr.CBMCollect = lstCollect.Sum(c => c.CBMTranfer);
                                            itemExpr.QuantityCollect = lstCollect.Sum(c => c.QuantityTranfer);
                                        }
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

                                        var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                        // Tính theo từng group 
                                        var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                        if (lstPriceExGroupProduct.Count() > 0)
                                        {
                                            // Tính phụ thu theo từng loại hàng
                                            foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                            {
                                                foreach (var itemOPSPriceExGroupProduct in itemGroup.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSPriceExGroupProduct);
                                                    if (itemOPSPriceExGroupProduct.HasCashCollect)
                                                    {
                                                        itemExpr.TonCollect = itemOPSPriceExGroupProduct.TonTranfer;
                                                        itemExpr.CBMCollect = itemOPSPriceExGroupProduct.CBMTranfer;
                                                        itemExpr.QuantityCollect = itemOPSPriceExGroupProduct.QuantityTranfer;
                                                    }
                                                    DIPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
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

                                #region Theo đơn hàng
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrder)
                                {
                                    foreach (var itemGroup in lstOPSGroupEx.GroupBy(c => c.OrderID))
                                    {
                                        var order = lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.Key);
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.DropPointCurrent = order.DropPointCurrent;
                                        itemExpr.GetPointCurrent = order.GetPointCurrent;
                                        itemExpr.SortConfig = order.SortConfig;

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

                                        var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                        // Tính theo từng group 
                                        var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                        if (lstPriceExGroupProduct.Count() > 0)
                                        {
                                            // Tính phụ thu theo từng loại hàng
                                            foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                            {
                                                foreach (var itemOPSPriceExGroupProduct in itemGroup.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSPriceExGroupProduct);
                                                    itemExpr.DropPointCurrent = order.DropPointCurrent;
                                                    itemExpr.GetPointCurrent = order.GetPointCurrent;
                                                    DIPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
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

                                #region Theo điểm
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderLocation)
                                {
                                    foreach (var itemGroup in lstOPSGroupEx.GroupBy(c => c.LocationToID))
                                    {
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());

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

                                        var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                        // Tính theo từng group 
                                        var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                        if (lstPriceExGroupProduct.Count() > 0)
                                        {
                                            // Tính phụ thu theo từng loại hàng
                                            foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                            {
                                                foreach (var itemOPSPriceExGroupProduct in itemGroup.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSPriceExGroupProduct);

                                                    DIPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
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

                                #region Theo cung đường
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstOPSGroupEx.GroupBy(c => c.OrderRoutingID))
                                    {
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());

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

                                        var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                        // Tính theo từng group 
                                        var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                        if (lstPriceExGroupProduct.Count() > 0)
                                        {
                                            // Tính phụ thu theo từng loại hàng
                                            foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                            {
                                                foreach (var itemOPSPriceExGroupProduct in itemGroup.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSPriceExGroupProduct);
                                                    DIPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
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

                                #region Theo thu hộ
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumCollect)
                                {
                                    foreach (var itemGroup in lstOPSGroupEx)
                                    {
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_GenerateItem(itemGroup);

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

                                        var lstOPSGroupID = lstOPSGroupEx.Where(c => c.ID == itemGroup.ID).Select(c => c.ID).Distinct().ToList();
                                        DIPriceEx_CalculatePrice(true, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                        // Tính theo từng group 
                                        var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                        if (lstPriceExGroupProduct.Count() > 0)
                                        {
                                            // Tính phụ thu theo từng loại hàng
                                            foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                            {
                                                foreach (var itemOPSPriceExGroupProduct in lstOPSGroupEx.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID && c.ID == itemGroup.ID))
                                                {
                                                    DIPriceEx_CalculatePriceGOP(true, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
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

                        #region Bốc xếp lên
                        var lstLoad = ItemInput.ListMOQLoad.Where(c => c.ContractID == itemContract.ID && c.IsLoading && !string.IsNullOrEmpty(c.ExprInput) && c.EffectDate <= DateConfig).ToList();
                        var LoadEffateDate = lstLoad.Select(c => new { c.EffectDate }).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                        if (LoadEffateDate != null)
                            lstLoad = lstLoad.Where(c => c.EffectDate == LoadEffateDate.EffectDate).ToList();
                        //Chạy từng phụ thu
                        foreach (var itemLoad in lstLoad)
                        {
                            System.Diagnostics.Debug.WriteLine("Bốc xếp lên: " + itemLoad.MOQName);

                            var lstOrderLoad = new List<HelperFinance_Order>();
                            var lstOPSGroupLoad = new List<HelperFinance_OPSGroupProduct>();

                            var queryOrderLoad = queryOrderContract.ToList();
                            var queryOPSGroupLoad = queryOPSGroupProductContract.ToList();
                            var queryORDGroupLoad = queryORDGroupProductContract.ToList();

                            //Danh sách các điều kiện lọc 
                            var lstLoadParentRouting = itemLoad.ListParentRouting;
                            var lstLoadRouting = itemLoad.ListRouting;
                            var lstLoadGroupLocation = itemLoad.ListGroupOfLocation;
                            var lstLoadPartner = itemLoad.ListPartnerID;
                            var lstLoadProvince = itemLoad.ListProvinceID;
                            var lstLoadLocationFrom = itemLoad.ListLocation.Where(c => c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                                .Select(c => c.LocationID).ToList();
                            var lstLoadLocationTo = itemLoad.ListLocation.Where(c => (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                                .Select(c => c.LocationID).ToList();
                            var lstLoadGroupProduct = itemLoad.ListGroupProduct.Select(c => c.GroupOfProductID).Distinct().ToList();

                            if (lstLoadParentRouting.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadParentRouting.Contains(c.ParentRoutingID)).ToList();
                            if (lstLoadRouting.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadRouting.Contains(c.CATRoutingID)).ToList();
                            if (lstLoadGroupLocation.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadGroupLocation.Contains(c.GroupOfLocationID)).ToList();
                            if (lstLoadLocationFrom.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadLocationFrom.Contains(c.LocationFromID)).ToList();
                            if (lstLoadLocationTo.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadLocationTo.Contains(c.LocationToID)).ToList();
                            if (lstLoadGroupProduct.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadGroupProduct.Contains(c.GroupOfProductID)).ToList();
                            if (lstLoadPartner.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadPartner.Contains(c.PartnerID)).ToList();
                            if (lstLoadProvince.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadProvince.Contains(c.LocationToProvinceID)).ToList();

                            var exOrderID = queryOrderLoad.Select(c => c.ID).ToArray();
                            var exGroupID = queryOPSGroupLoad.Select(c => c.OrderID).Distinct().ToArray();
                            var lstOrderCheckID = exGroupID.Intersect(exOrderID).ToList();
                            var lstOrderCheck = queryOrderLoad.Where(c => lstOrderCheckID.Contains(c.ID)).ToList();
                            var lstOrderGroupCheck = queryOPSGroupLoad.Where(c => lstOrderCheckID.Contains(c.OrderID)).ToList();

                            bool isLoadSumInDay = false;
                            if (lstOrderGroupCheck.Count > 0 && (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderInDay || itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumScheduleInDay))
                            {
                                bool flag = true;
                                //Thực hiện công thức
                                DTOPriceDIExExpr itemExpr = Expr_Generate(lstOrderGroupCheck);
                                itemExpr.Credit = lstOrderGroupCheck.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                isLoadSumInDay = flag = Expression_CheckBool(itemExpr, itemLoad.ExprInput);
                                if (flag == true)
                                {
                                    lstOrderLoad = lstOrderCheck;
                                    lstOPSGroupLoad = lstOrderGroupCheck;
                                }
                            }
                            else
                            {
                                #region Tính theo đơn hàng
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrder)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        flag = Expression_CheckBool(itemExpr, itemLoad.ExprInput);
                                        if (flag == true)
                                        {
                                            lstOrderLoad.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumSchedule)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempOrderID = itemGroup.Select(c => c.OrderID).Distinct().ToList();
                                            foreach (var tempOrderID in lstTempOrderID)
                                            {
                                                lstOrderLoad.Add(lstOrderCheck.FirstOrDefault(c => c.ID == tempOrderID));
                                            }
                                            lstOPSGroupLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng trả về
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumReturnOrder)
                                {
                                    lstOrderGroupCheck = lstOrderGroupCheck.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = false;
                                        var order = lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.Key);
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstOrderLoad.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo cung đường
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.OrderRoutingID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempOrderID = itemGroup.Select(c => c.OrderID).Distinct().ToList();
                                            foreach (var tempOrderID in lstTempOrderID)
                                            {
                                                lstOrderLoad.Add(lstOrderCheck.FirstOrDefault(c => c.ID == tempOrderID));
                                            }
                                            lstOPSGroupLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion
                            }

                            //Thực hiện lấy output MOQ
                            foreach (var itemOrderLoad in lstOrderLoad)
                            {
                                itemOrderLoad.HasMOQLoad = true;
                            }
                            foreach (var itemOrderGroupLoad in lstOPSGroupLoad)
                            {
                                itemOrderGroupLoad.HasMOQLoad = true;
                            }

                            //Ghi dữ liệu phụ thu vào hệ thống
                            if (isLoadSumInDay && !string.IsNullOrEmpty(itemLoad.ExprPriceFix) && lstOPSGroupLoad.Count > 0)
                            {
                                //Thực hiện công thức
                                DTOPriceDIExExpr itemExpr = Expr_Generate(lstOPSGroupLoad.ToList());
                                itemExpr.Credit = lstOPSGroupLoad.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);

                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.CustomerID = itemContract.CustomerID;
                                pl.ContractID = itemContract.ID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                    priceFix = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPriceFix), itemExpr);

                                if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                    priceMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPrice), itemExpr);

                                if (!string.IsNullOrEmpty(itemLoad.ExprTon))
                                    quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprTon), itemExpr);

                                if (!string.IsNullOrEmpty(itemLoad.ExprCBM))
                                    quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprCBM), itemExpr);

                                if (!string.IsNullOrEmpty(itemLoad.ExprQuan))
                                    quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprQuan), itemExpr);

                                if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                {
                                    // set lại đơn giá = 0
                                    foreach (var tempOPSGroupLoad in lstOPSGroupLoad)
                                        tempOPSGroupLoad.UnitPriceLoad = 0;

                                    if (priceFix.HasValue)
                                    {
                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                        plCost.CreatedBy = Account.UserName;
                                        plCost.CreatedDate = DateTime.Now;
                                        plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupCredit;
                                        plCost.Note = itemLoad.MOQName;
                                        plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                        pl.FIN_PLDetails.Add(plCost);
                                        pl.Credit += plCost.Credit;

                                        var lstOPSGroupID = lstOPSGroupLoad.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                        lstPl.Add(pl);
                                    }
                                }

                                if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                {
                                    if (priceMOQ.HasValue)
                                    {
                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                        plCost.CreatedBy = Account.UserName;
                                        plCost.CreatedDate = DateTime.Now;
                                        plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupCredit;
                                        plCost.Note = itemLoad.MOQName;
                                        plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                        plCost.UnitPrice = priceMOQ.Value;
                                        plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                        pl.FIN_PLDetails.Add(plCost);
                                        pl.Credit += plCost.Credit;

                                        var lstOPSGroupID = lstOPSGroupLoad.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                        lstPl.Add(pl);
                                    }
                                }

                                var lstPriceMOQLoadGroupProduct = itemLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                {
                                    foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                    {
                                        foreach (var itemOPSGroupLoad in lstOPSGroupLoad.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                        {
                                            itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                            itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;

                                            decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                            if (priceGroupMOQ.HasValue)
                                                itemOPSGroupLoad.UnitPriceLoad = priceGroupMOQ.Value;

                                            decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                            if (quantityGroupMOQ.HasValue)
                                                itemOPSGroupLoad.QuantityLoad = (double)quantityGroupMOQ.Value;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                #region Theo đơn hàng
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrder)
                                {
                                    foreach (var itemGroup in lstOPSGroupLoad.GroupBy(c => c.OrderID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.OrderID = itemGroup.Key;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupCredit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupCredit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;
                                        }
                                    }
                                }
                                #endregion

                                #region Theo chuyến
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumSchedule)
                                {
                                    foreach (var itemGroup in lstOPSGroupLoad.GroupBy(c => c.DITOMasterID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.DITOMasterID = itemGroup.Key;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupCredit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupCredit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng trả về
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumReturnOrder)
                                {
                                    lstOPSGroupLoad = lstOPSGroupLoad.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOPSGroupLoad.GroupBy(c => c.OrderID))
                                    {
                                        // Tạo pl temp
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                        {
                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupCredit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Credit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupCredit;
                                                    plCost.Note = itemLoad.MOQName;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Credit += plCost.Credit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);
                                                        itemExpr.UnitPrice = itemOPSGroupMOQ.UnitPriceLoad;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);

                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.DITOLoadReturnCredit;
                                                        plCost.Note = itemLoad.MOQName;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        lstPl.Add(pl);

                                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.GroupOfProductID = itemOPSGroupMOQ.ID;
                                                        plGroup.Quantity = quantityGroupMOQ.HasValue ? (double)quantityGroupMOQ.Value : 0;
                                                        plGroup.UnitPrice = priceGroupMOQ.HasValue ? priceGroupMOQ.Value : 0;
                                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                        plCost.Credit += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                                                        pl.Credit += plCost.Credit;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo cung đường
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstOPSGroupLoad.GroupBy(c => c.OrderRoutingID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.GOVCodeSchedule = itemGroup.FirstOrDefault().GroupOfVehicleCode;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupCredit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupCredit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }

                        // Ghi dữ liệu chính + MOQ bốc xếp
                        var lstORDLoading = ItemInput.ListOrder.Where(c => c.ContractID == itemContract.ID && c.IsLoading);
                        foreach (var itemOrderLoading in lstORDLoading)
                        {
                            var lstOPSGroupLoading = ItemInput.ListOPSGroupProduct.Where(c => c.ContractID == itemContract.ID && c.OrderID == itemOrderLoading.ID && c.IsLoading);
                            var lstMasterID = lstOPSGroupLoading.Select(c => c.DITOMasterID).Distinct().ToList();
                            foreach (var masterID in lstMasterID)
                            {
                                var master = ItemInput.ListOPSGroupProduct.FirstOrDefault(c => c.DITOMasterID == masterID);
                                if (master != null)
                                {
                                    var pl = lstPl.FirstOrDefault(c => c.SYSCustomerID == Account.SYSCustomerID && c.DITOMasterID == master.DITOMasterID && c.OrderID == itemOrderLoading.ID);
                                    if (pl == null)
                                    {
                                        pl = new FIN_PL();
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = master.VendorID;
                                        pl.OrderID = itemOrderLoading.ID;
                                        pl.CustomerID = itemOrderLoading.CustomerID;
                                        pl.DITOMasterID = master.DITOMasterID;
                                        pl.VehicleID = master.VehicleID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        lstPl.Add(pl);
                                    }

                                    var lstOPSGroup = lstOPSGroupLoading.Where(c => c.DITOMasterID == master.DITOMasterID);
                                    foreach (var opsGroup in lstOPSGroup)
                                    {
                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                        plCost.CreatedBy = Account.UserName;
                                        plCost.CreatedDate = DateTime.Now;
                                        plCost.CostID = (int)CATCostType.DITOLoadCredit;
                                        pl.FIN_PLDetails.Add(plCost);

                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                        plGroup.CreatedBy = Account.UserName;
                                        plGroup.CreatedDate = DateTime.Now;
                                        plGroup.Unit = opsGroup.PriceOfGOPName;
                                        plGroup.UnitPrice = opsGroup.UnitPriceLoad;
                                        plGroup.Quantity = opsGroup.QuantityLoad;
                                        plGroup.GroupOfProductID = opsGroup.ID;
                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                        plCost.Credit += plGroup.UnitPrice * (decimal)plGroup.Quantity;

                                        pl.Credit += plCost.Credit;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Bốc xếp xuống
                        var lstUnLoad = ItemInput.ListMOQLoad.Where(c => c.ContractID == itemContract.ID && !c.IsLoading && !string.IsNullOrEmpty(c.ExprInput) && c.EffectDate <= DateConfig).ToList();
                        var UnLoadEffateDate = lstUnLoad.Select(c => new { c.EffectDate }).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                        if (UnLoadEffateDate != null)
                            lstUnLoad = lstUnLoad.Where(c => c.EffectDate == UnLoadEffateDate.EffectDate).ToList();
                        //Chạy từng phụ thu
                        foreach (var itemUnLoad in lstUnLoad)
                        {
                            System.Diagnostics.Debug.WriteLine("Bốc xếp xuống: " + itemUnLoad.MOQName);

                            var lstOrderUnLoad = new List<HelperFinance_Order>();
                            var lstOPSGroupUnLoad = new List<HelperFinance_OPSGroupProduct>();

                            var queryOrderUnLoad = queryOrderContract.ToList();
                            var queryOPSGroupUnLoad = queryOPSGroupProductContract.ToList();
                            var queryORDGroupUnLoad = queryORDGroupProductContract.ToList();

                            //Danh sách các điều kiện lọc 
                            var lstLoadParentRouting = itemUnLoad.ListParentRouting;
                            var lstLoadRouting = itemUnLoad.ListRouting;
                            var lstLoadGroupLocation = itemUnLoad.ListGroupOfLocation;
                            var lstLoadPartner = itemUnLoad.ListPartnerID;
                            var lstLoadProvince = itemUnLoad.ListProvinceID;
                            var lstLoadLocationFrom = itemUnLoad.ListLocation.Where(c => c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                                .Select(c => c.LocationID).ToList();
                            var lstLoadLocationTo = itemUnLoad.ListLocation.Where(c => (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                                .Select(c => c.LocationID).ToList();
                            var lstLoadGroupProduct = itemUnLoad.ListGroupProduct.Select(c => c.GroupOfProductID).Distinct().ToList();

                            if (lstLoadParentRouting.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadParentRouting.Contains(c.ParentRoutingID)).ToList();
                            if (lstLoadRouting.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadRouting.Contains(c.CATRoutingID)).ToList();
                            if (lstLoadGroupLocation.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadGroupLocation.Contains(c.GroupOfLocationID)).ToList();
                            if (lstLoadLocationFrom.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadLocationFrom.Contains(c.LocationFromID)).ToList();
                            if (lstLoadLocationTo.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadLocationTo.Contains(c.LocationToID)).ToList();
                            if (lstLoadGroupProduct.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadGroupProduct.Contains(c.GroupOfProductID)).ToList();
                            if (lstLoadPartner.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadPartner.Contains(c.PartnerID)).ToList();
                            if (lstLoadProvince.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadProvince.Contains(c.LocationToProvinceID)).ToList();

                            var exOrderID = queryOrderUnLoad.Select(c => c.ID).ToArray();
                            var exGroupID = queryOPSGroupUnLoad.Select(c => c.OrderID).Distinct().ToArray();
                            var lstOrderCheckID = exGroupID.Intersect(exOrderID).ToList();
                            var lstOrderCheck = queryOrderUnLoad.Where(c => lstOrderCheckID.Contains(c.ID)).ToList();
                            var lstOrderGroupCheck = queryOPSGroupUnLoad.Where(c => lstOrderCheckID.Contains(c.OrderID)).ToList();

                            bool isUnLoadSumInDay = false;
                            if (lstOrderGroupCheck.Count > 0 && (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderInDay || itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumScheduleInDay))
                            {
                                bool flag = true;
                                //Thực hiện công thức
                                DTOPriceDIExExpr itemExpr = Expr_Generate(lstOrderGroupCheck.ToList());
                                itemExpr.Credit = lstOrderGroupCheck.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                isUnLoadSumInDay = flag = Expression_CheckBool(itemExpr, itemUnLoad.ExprInput);
                                if (flag == true)
                                {
                                    lstOrderUnLoad = lstOrderCheck;
                                    lstOPSGroupUnLoad = lstOrderGroupCheck;
                                }
                            }
                            else
                            {
                                #region Theo đơn hàng
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrder)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        flag = Expression_CheckBool(itemExpr, itemUnLoad.ExprInput);
                                        if (flag == true)
                                        {
                                            lstOrderUnLoad.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupUnLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumSchedule)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemUnLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempMasterID = itemGroup.Select(c => c.DITOMasterID).Distinct().ToList();
                                            foreach (var tempMasterID in lstTempMasterID)
                                            {
                                                lstOrderUnLoad.Add(lstOrderCheck.FirstOrDefault(c => c.ID == tempMasterID));
                                                lstOPSGroupUnLoad.AddRange(itemGroup.ToArray());
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng trả về
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumReturnOrder)
                                {
                                    lstOrderGroupCheck = lstOrderGroupCheck.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = false;
                                        var order = lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.Key);
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = new DTOPriceDIExExpr();
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemUnLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstOrderUnLoad.Add(lstOrderCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupUnLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo cung đường
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstOrderGroupCheck.GroupBy(c => c.OrderRoutingID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemUnLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempOrderID = itemGroup.Select(c => c.OrderID).Distinct().ToList();
                                            foreach (var tempOrderID in lstTempOrderID)
                                            {
                                                lstOrderUnLoad.Add(lstOrderCheck.FirstOrDefault(c => c.ID == tempOrderID));
                                            }
                                            lstOPSGroupUnLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion
                            }

                            //Thực hiện lấy output MOQ
                            foreach (var itemOrderUnLoad in lstOrderUnLoad)
                            {
                                itemOrderUnLoad.HasMOQUnLoad = true;
                            }
                            foreach (var itemOrderGroupUnLoad in lstOPSGroupUnLoad)
                            {
                                itemOrderGroupUnLoad.HasMOQUnLoad = true;
                            }

                            //Ghi dữ liệu phụ thu vào hệ thống
                            if (isUnLoadSumInDay && !string.IsNullOrEmpty(itemUnLoad.ExprPriceFix) && lstOPSGroupUnLoad.Count > 0)
                            {
                                //Thực hiện công thức
                                DTOPriceDIExExpr itemExpr = Expr_Generate(lstOPSGroupUnLoad.ToList());
                                itemExpr.Credit = lstOPSGroupUnLoad.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);

                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.CustomerID = itemContract.CustomerID;
                                pl.ContractID = itemContract.ID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                    priceFix = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPriceFix), itemExpr);

                                if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                    priceMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPrice), itemExpr);

                                if (!string.IsNullOrEmpty(itemUnLoad.ExprTon))
                                    quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprTon), itemExpr);

                                if (!string.IsNullOrEmpty(itemUnLoad.ExprCBM))
                                    quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprCBM), itemExpr);

                                if (!string.IsNullOrEmpty(itemUnLoad.ExprQuan))
                                    quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprQuan), itemExpr);

                                if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                {
                                    // set lại đơn giá = 0
                                    foreach (var tempOPSGroupLoad in lstOPSGroupUnLoad)
                                        tempOPSGroupLoad.UnitPriceUnLoad = 0;

                                    if (priceFix.HasValue)
                                    {
                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                        plCost.CreatedBy = Account.UserName;
                                        plCost.CreatedDate = DateTime.Now;
                                        plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupCredit;
                                        plCost.Note = itemUnLoad.MOQName;
                                        plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                        pl.FIN_PLDetails.Add(plCost);
                                        pl.Credit += plCost.Credit;

                                        var lstOPSGroupID = lstOPSGroupUnLoad.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                        lstPl.Add(pl);
                                    }
                                }

                                if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                {
                                    if (priceMOQ.HasValue)
                                    {
                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                        plCost.CreatedBy = Account.UserName;
                                        plCost.CreatedDate = DateTime.Now;
                                        plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupCredit;
                                        plCost.Note = itemUnLoad.MOQName;
                                        plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                        plCost.UnitPrice = priceMOQ.Value;
                                        plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                        pl.FIN_PLDetails.Add(plCost);
                                        pl.Credit += plCost.Credit;

                                        var lstOPSGroupID = lstOPSGroupUnLoad.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                        lstPl.Add(pl);
                                    }
                                }

                                var lstPriceMOQLoadGroupProduct = itemUnLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                {
                                    foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                    {
                                        foreach (var itemOPSGroupLoad in lstOPSGroupUnLoad.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                        {
                                            itemExpr = new DTOPriceDIExExpr();
                                            itemExpr.TonTransfer = itemOPSGroupLoad.TonTranfer;
                                            itemExpr.CBMTransfer = itemOPSGroupLoad.CBMTranfer;
                                            itemExpr.QuantityTransfer = itemOPSGroupLoad.QuantityTranfer;
                                            itemExpr.TonActual = itemOPSGroupLoad.TonActual;
                                            itemExpr.CBMActual = itemOPSGroupLoad.CBMActual;
                                            itemExpr.QuantityActual = itemOPSGroupLoad.QuantityActual;
                                            itemExpr.TonBBGN = itemOPSGroupLoad.TonBBGN;
                                            itemExpr.CBMBBGN = itemOPSGroupLoad.CBMBBGN;
                                            itemExpr.QuantityBBGN = itemOPSGroupLoad.QuantityBBGN;
                                            itemExpr.TonReturn = itemOPSGroupLoad.TonReturn;
                                            itemExpr.CBMReturn = itemOPSGroupLoad.CBMReturn;
                                            itemExpr.QuantityReturn = itemOPSGroupLoad.QuantityReturn;
                                            itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;
                                            itemExpr.GOVCodeOrder = itemOPSGroupLoad.GroupOfVehicleCode;

                                            decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                            if (priceGroupMOQ.HasValue)
                                                itemOPSGroupLoad.UnitPriceUnLoad = priceGroupMOQ.Value;

                                            decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                            if (quantityGroupMOQ.HasValue)
                                                itemOPSGroupLoad.QuantityUnLoad = (double)quantityGroupMOQ.Value;
                                        }
                                    }
                                }
                            }
                            else
                            {
                                #region Theo đơn hàng
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrder)
                                {
                                    foreach (var itemGroup in lstOPSGroupUnLoad.GroupBy(c => c.OrderID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.OrderID = itemGroup.Key;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupCredit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupCredit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemUnLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceUnLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityUnLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;
                                        }
                                    }
                                }
                                #endregion

                                #region Theo chuyến
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumSchedule)
                                {
                                    foreach (var itemGroup in lstOPSGroupUnLoad.GroupBy(c => c.DITOMasterID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.DITOMasterID = itemGroup.Key;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.GOVCodeSchedule = itemGroup.FirstOrDefault().GroupOfVehicleCode;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupCredit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupCredit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemUnLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceUnLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityUnLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng trả về
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumReturnOrder)
                                {
                                    lstOPSGroupUnLoad = lstOPSGroupUnLoad.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOPSGroupUnLoad.GroupBy(c => c.OrderID))
                                    {
                                        // Tạo pl temp
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                        {
                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupCredit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Credit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupCredit;
                                                    plCost.Note = itemUnLoad.MOQName;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Credit += plCost.Credit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemUnLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);
                                                        itemExpr.UnitPrice = itemOPSGroupMOQ.UnitPriceUnLoad;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);

                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.DITOUnLoadReturnCredit;
                                                        plCost.Note = itemUnLoad.MOQName;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        lstPl.Add(pl);

                                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.GroupOfProductID = itemOPSGroupMOQ.ID;
                                                        plGroup.Quantity = quantityGroupMOQ.HasValue ? (double)quantityGroupMOQ.Value : 0;
                                                        plGroup.UnitPrice = priceGroupMOQ.HasValue ? priceGroupMOQ.Value : 0;
                                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                        plCost.Credit += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                                                        pl.Credit += plCost.Credit;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo cung đường
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstOPSGroupUnLoad.GroupBy(c => c.OrderRoutingID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.GOVCodeSchedule = itemGroup.FirstOrDefault().GroupOfVehicleCode;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupCredit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupCredit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemUnLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = new DTOPriceDIExExpr();
                                                    itemExpr.TonTransfer = itemOPSGroupLoad.TonTranfer;
                                                    itemExpr.CBMTransfer = itemOPSGroupLoad.CBMTranfer;
                                                    itemExpr.QuantityTransfer = itemOPSGroupLoad.QuantityTranfer;
                                                    itemExpr.TonActual = itemOPSGroupLoad.TonActual;
                                                    itemExpr.CBMActual = itemOPSGroupLoad.CBMActual;
                                                    itemExpr.QuantityActual = itemOPSGroupLoad.QuantityActual;
                                                    itemExpr.TonBBGN = itemOPSGroupLoad.TonBBGN;
                                                    itemExpr.CBMBBGN = itemOPSGroupLoad.CBMBBGN;
                                                    itemExpr.QuantityBBGN = itemOPSGroupLoad.QuantityBBGN;
                                                    itemExpr.TonReturn = itemOPSGroupLoad.TonReturn;
                                                    itemExpr.CBMReturn = itemOPSGroupLoad.CBMReturn;
                                                    itemExpr.QuantityReturn = itemOPSGroupLoad.QuantityReturn;
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;
                                                    itemExpr.GOVCodeOrder = itemOPSGroupLoad.GroupOfVehicleCode;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceUnLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityUnLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }

                        // Ghi dữ liệu chính + MOQ bốc xếp
                        var lstORDUnLoading = ItemInput.ListOrder.Where(c => c.ContractID == itemContract.ID && c.IsUnLoading);
                        foreach (var itemOrderUnLoading in lstORDUnLoading)
                        {
                            var lstOPSGroupLoading = ItemInput.ListOPSGroupProduct.Where(c => c.ContractID == itemContract.ID && c.OrderID == itemOrderUnLoading.ID && c.IsUnLoading);
                            var lstMasterID = lstOPSGroupLoading.Select(c => c.DITOMasterID).Distinct().ToList();
                            foreach (var masterID in lstMasterID)
                            {
                                var master = ItemInput.ListOPSGroupProduct.FirstOrDefault(c => c.DITOMasterID == masterID);
                                if (master != null)
                                {
                                    var pl = lstPl.FirstOrDefault(c => c.SYSCustomerID == Account.SYSCustomerID && c.DITOMasterID == master.DITOMasterID && c.OrderID == itemOrderUnLoading.ID);
                                    if (pl == null)
                                    {
                                        pl = new FIN_PL();
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = master.VendorID;
                                        pl.OrderID = itemOrderUnLoading.ID;
                                        pl.CustomerID = itemOrderUnLoading.CustomerID;
                                        pl.DITOMasterID = master.DITOMasterID;
                                        pl.VehicleID = master.VehicleID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        lstPl.Add(pl);
                                    }

                                    var lstOPSGroup = lstOPSGroupLoading.Where(c => c.DITOMasterID == master.DITOMasterID);
                                    foreach (var opsGroup in lstOPSGroup)
                                    {
                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                        plCost.CreatedBy = Account.UserName;
                                        plCost.CreatedDate = DateTime.Now;
                                        plCost.CostID = (int)CATCostType.DITOUnLoadCredit;
                                        pl.FIN_PLDetails.Add(plCost);

                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                        plGroup.CreatedBy = Account.UserName;
                                        plGroup.CreatedDate = DateTime.Now;
                                        plGroup.Unit = opsGroup.PriceOfGOPName;
                                        plGroup.UnitPrice = opsGroup.UnitPriceUnLoad;
                                        plGroup.Quantity = opsGroup.QuantityUnLoad;
                                        plGroup.GroupOfProductID = opsGroup.ID;
                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                        plCost.Credit += plGroup.UnitPrice * (decimal)plGroup.Quantity;

                                        pl.Credit += plCost.Credit;
                                    }
                                }
                            }
                        }
                        #endregion
                    }

                    System.Diagnostics.Debug.WriteLine("Contract end: " + itemContract.ID);
                }
                #endregion

                #region Tính phần bổ sung - Manual Fix
                foreach (var itemManualFix in ItemInput.ListManualFix)
                {
                    FIN_PL pl = new FIN_PL();
                    pl.CreatedBy = Account.UserName;
                    pl.CreatedDate = DateTime.Now;
                    pl.Effdate = DateConfig.Date;
                    pl.Code = string.Empty;
                    pl.SYSCustomerID = Account.SYSCustomerID;
                    pl.CustomerID = itemManualFix.CustomerID;
                    pl.VendorID = itemManualFix.VendorOfVehicleID;
                    pl.VehicleID = itemManualFix.VehicleID;
                    pl.ContractID = itemManualFix.ContractID;
                    pl.OrderID = itemManualFix.OrderID;
                    pl.DITOMasterID = itemManualFix.DITOMasterID;
                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                    lstPl.Add(pl);

                    FIN_PLDetails plCost = new FIN_PLDetails();
                    plCost.CreatedBy = Account.UserName;
                    plCost.CreatedDate = DateTime.Now;
                    plCost.CostID = (int)CATCostType.ManualFixCredit;
                    plCost.Credit = itemManualFix.Credit;
                    plCost.Note = itemManualFix.Note;
                    plCost.TypeOfPriceDIExCode = "ManualFix";
                    pl.Credit = plCost.Credit;
                    pl.FIN_PLDetails.Add(plCost);

                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                    plGroup.CreatedBy = Account.UserName;
                    plGroup.CreatedDate = DateTime.Now;
                    plGroup.GroupOfProductID = itemManualFix.DITOGroupProductID;
                    plGroup.UnitPrice = itemManualFix.UnitPrice;
                    plGroup.Unit = itemManualFix.PriceOfGOPName;
                    if (itemManualFix.PriceOfGOPID == iTon)
                        plGroup.Quantity = itemManualFix.Ton;
                    else if (itemManualFix.PriceOfGOPID == iCBM)
                        plGroup.Quantity = itemManualFix.CBM;
                    else if (itemManualFix.PriceOfGOPID == iQuantity)
                        plGroup.Quantity = itemManualFix.Quantity;

                    plCost.FIN_PLGroupOfProduct.Add(plGroup);
                }
                #endregion

                #region Cập nhật dữ liệu
                foreach (var itemORDGroup in lstOrderGroupUpDate)
                {
                    var group = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == itemORDGroup.ID);
                    if (group != null)
                        group.FINSort = itemORDGroup.FINSort;
                }

                var lstORDOrderID = ItemInput.ListOrder.Select(c => c.ID).Distinct().ToList();
                foreach (var itemORDOrderID in lstORDOrderID)
                {
                    var ordOrder = model.ORD_Order.FirstOrDefault(c => c.ID == itemORDOrderID);
                    if (ordOrder != null)
                        ordOrder.TypeOfPaymentORDOrderID = -(int)SYSVarType.TypeOfPaymentORDOrderOpen;
                }

                var lstORDOrderGroupID = ItemInput.ListOPSGroupProduct.Select(c => c.OrderGroupProductID).Distinct().ToList();
                foreach (var itemORDOrderGroupID in lstORDOrderGroupID)
                {
                    var ordOrderGroup = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == itemORDOrderGroupID);
                    if (ordOrderGroup != null)
                        ordOrderGroup.TypeOfPaymentORDGroupProductID = -(int)SYSVarType.TypeOfPaymentORDGroupProductOpen;
                }
                #endregion

                #region Ghi dữ liệu vào model

                foreach (var pl in lstPl)
                    model.FIN_PL.Add(pl);

                foreach (var plTemp in lstPlTemp)
                    model.FIN_Temp.Add(plTemp);

                foreach (var plTrouble in lstPlTrouble)
                    model.FIN_PL.Add(plTrouble);
                #endregion
            }
        }

        public static void Truck_CalculateTO(DataEntities model, AccountItem Account, DateTime DateConfig, List<int> lstCustomerID)
        {
            System.Diagnostics.Debug.WriteLine("Truck_CalculateTO: " + DateConfig.ToString("dd/MM/yyyy"));
            const int iTon = -(int)SYSVarType.PriceOfGOPTon;
            const int iCBM = -(int)SYSVarType.PriceOfGOPCBM;
            const int iQuantity = -(int)SYSVarType.PriceOfGOPTU;

            DateConfig = DateConfig.Date;
            var DateConfigEnd = DateConfig.Date.AddDays(1);

            //Lấy dữ liệu input
            var ItemInput = DIPrice_GetInput(model, DateConfig, Account, lstCustomerID, false);

            #region Xóa PL cũ
            foreach (var pl in model.FIN_PL.Where(c => DbFunctions.TruncateTime(c.Effdate) == DateConfig && c.SYSCustomerID == Account.SYSCustomerID && !c.IsPlanning && c.ContractID.HasValue && ItemInput.ListContractID.Contains(c.ContractID.Value)))
            {
                foreach (var plDetail in model.FIN_PLDetails.Where(c => c.PLID == pl.ID))
                {
                    foreach (var plGroup in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == plDetail.ID))
                        model.FIN_PLGroupOfProduct.Remove(plGroup);
                    model.FIN_PLDetails.Remove(plDetail);
                }

                model.FIN_PL.Remove(pl);
            }

            // Xóa PL của manual fix
            var lstOPSGroupProductID = ItemInput.ListManualFix.Where(c => c.ContractID == null).Select(c => c.DITOGroupProductID).Distinct().ToList();
            foreach (var item in model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID > 0 && lstOPSGroupProductID.Contains(c.GroupOfProductID) && c.FIN_PLDetails.CostID == (int)CATCostType.ManualFixDebit))
            {
                model.FIN_PLGroupOfProduct.Remove(item);
                model.FIN_PLDetails.Remove(item.FIN_PLDetails);
                model.FIN_PL.Remove(item.FIN_PLDetails.FIN_PL);
            }

            model.SaveChanges();
            #endregion

            // Nhóm hàng Mapping
            foreach (var itemOPSGroupProduct in ItemInput.ListOPSGroupProduct)
            {
                var mappingLoad = ItemInput.ListGroupProductMapping.FirstOrDefault(c => c.VendorID == itemOPSGroupProduct.VendorLoadID && c.GroupOfProductCUSID == itemOPSGroupProduct.GroupOfProductID);
                if (mappingLoad != null)
                {
                    itemOPSGroupProduct.GroupOfProductLoadID = mappingLoad.GroupOfProductVENID;
                    itemOPSGroupProduct.PriceOfGOPLoadID = mappingLoad.PriceOfGOPID;
                    itemOPSGroupProduct.PriceOfGOPLoadName = mappingLoad.PriceOfGOPIDName;
                }
                else
                    itemOPSGroupProduct.GroupOfProductLoadID = -1;

                var mappingUnload = ItemInput.ListGroupProductMapping.FirstOrDefault(c => c.VendorID == itemOPSGroupProduct.VendorUnLoadID && c.GroupOfProductCUSID == itemOPSGroupProduct.GroupOfProductID);
                if (mappingUnload != null)
                {
                    itemOPSGroupProduct.GroupOfProductUnLoadID = mappingUnload.GroupOfProductVENID;
                    itemOPSGroupProduct.PriceOfGOPUnLoadID = mappingUnload.PriceOfGOPID;
                    itemOPSGroupProduct.PriceOfGOPUnLoadName = mappingUnload.PriceOfGOPIDName;
                }
                else
                    itemOPSGroupProduct.GroupOfProductUnLoadID = -1;

                var mapping = ItemInput.ListGroupProductMapping.FirstOrDefault(c => c.VendorID == itemOPSGroupProduct.VendorID && c.GroupOfProductCUSID == itemOPSGroupProduct.GroupOfProductID);
                if (mapping != null)
                {
                    itemOPSGroupProduct.GroupOfProductID = mapping.GroupOfProductVENID;
                    itemOPSGroupProduct.PriceOfGOPID = mapping.PriceOfGOPID;
                    itemOPSGroupProduct.PriceOfGOPName = mapping.PriceOfGOPIDName;
                }
                else
                    itemOPSGroupProduct.GroupOfProductID = -1;
            }

            List<FIN_PL> lstPl = new List<FIN_PL>();
            List<FIN_Temp> lstPlTemp = new List<FIN_Temp>();
            List<HelperFinance_ORDGroupProduct> lstOrderGroupUpDate = new List<HelperFinance_ORDGroupProduct>();

            if (ItemInput.ListContractID.Count > 0 && ItemInput.ListTOMaster.Count > 0)
            {
                #region Qui đổi
                var lstGroupProductChange = ItemInput.ListContractGroupProduct;
                //Thực hiện qui đổi
                foreach (var contractGroupChange in lstGroupProductChange.GroupBy(c => new { c.ContractID, c.TypeOfSGroupProductChangeID, c.TypeOfContractQuantityID }))
                {
                    var contractChange = contractGroupChange.FirstOrDefault();
                    foreach (var itemChange in contractGroupChange)
                    {
                        // Qui đổi chi tiết
                        if (contractChange.TypeOfSGroupProductChangeID == -(int)SYSVarType.TypeOfSGroupProductChangeDetail)
                        {
                            #region Qui đổi OPS nhóm, hàng hóa
                            foreach (var item in ItemInput.ListOPSGroupProduct.Where(c => c.GroupOfProductID == itemChange.GroupOfProductID && c.ContractID == itemChange.ContractID && (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue ? c.ProductID == itemChange.ProductID : true)))
                            {
                                // Thay đổi nhóm khác
                                if (itemChange.GroupOfProductIDChange.HasValue)
                                {
                                    item.GroupOfProductID = itemChange.GroupOfProductIDChange.Value;
                                    item.PriceOfGOPID = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChange : item.PriceOfGOPID;
                                    item.PriceOfGOPName = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChangeName : item.PriceOfGOPName;
                                }
                                if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                {
                                    if (itemChange.ProductID == item.ProductID)
                                    {
                                        //Qui đổi transfer
                                        DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                        itemTransfer.Quantity = item.QuantityTranfer;
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemTransfer.Quantity = item.QuantityBBGN;
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemTransfer.Quantity = item.Quantity;
                                        itemTransfer.Expression = itemChange.Expression;
                                        itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                        bool flag = false;
                                        try
                                        {
                                            flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                        }
                                        catch { }
                                        if (flag)
                                        {
                                            double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                            if (quantityTransfer.HasValue)
                                            {
                                                if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                                                {
                                                    var exchangeCBM = itemChange.Weight > 0 && itemChange.CBM.HasValue ? itemChange.CBM.Value / itemChange.Weight.Value : 0;
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                    {
                                                        item.TonTranfer = quantityTransfer.Value;
                                                        item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                    {
                                                        item.Ton = quantityTransfer.Value;
                                                        item.CBM = quantityTransfer.Value * exchangeCBM;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                    {
                                                        item.TonBBGN = quantityTransfer.Value;
                                                        item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                    }
                                                }
                                                if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                                                {
                                                    var exchangeTon = itemChange.Weight.HasValue && itemChange.CBM > 0 ? itemChange.Weight.Value / itemChange.CBM.Value : 0;
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                    {
                                                        item.CBMTranfer = quantityTransfer.Value;
                                                        item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                    {
                                                        item.CBM = quantityTransfer.Value;
                                                        item.Ton = quantityTransfer.Value * exchangeTon;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                    {
                                                        item.CBMBBGN = quantityTransfer.Value;
                                                        item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                    }
                                                }
                                                if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                                                {
                                                    var exchangeTon = itemChange.Weight.HasValue ? itemChange.Weight.Value : 0;
                                                    var exchangeCBM = itemChange.CBM.HasValue ? itemChange.CBM.Value : 0;
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                    {
                                                        item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                        item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                        item.QuantityTranfer = quantityTransfer.Value;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                    {
                                                        item.Ton = quantityTransfer.Value * exchangeTon;
                                                        item.CBM = quantityTransfer.Value * exchangeCBM;
                                                        item.Quantity = quantityTransfer.Value;
                                                    }
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                    {
                                                        item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                        item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                        item.QuantityBBGN = quantityTransfer.Value;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    //Qui đổi transfer
                                    DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                    itemTransfer.Ton = item.TonTranfer;
                                    itemTransfer.CBM = item.CBMTranfer;
                                    itemTransfer.Quantity = item.QuantityTranfer;
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        itemTransfer.Ton = item.Ton;
                                        itemTransfer.CBM = item.CBM;
                                        itemTransfer.Quantity = item.Quantity;
                                    }
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        itemTransfer.Ton = item.TonBBGN;
                                        itemTransfer.CBM = item.CBMBBGN;
                                        itemTransfer.Quantity = item.QuantityBBGN;
                                    }
                                    itemTransfer.Expression = itemChange.Expression;
                                    itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                    bool flag = false;
                                    try
                                    {
                                        flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                    }
                                    catch { }
                                    if (flag)
                                    {
                                        double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                        if (quantityTransfer.HasValue)
                                        {
                                            if (item.PriceOfGOPID.HasValue)
                                            {
                                                if (item.PriceOfGOPID == iTon)
                                                {
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        item.TonTranfer = quantityTransfer.Value;
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        item.Ton = quantityTransfer.Value;
                                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        item.TonBBGN = quantityTransfer.Value;
                                                }
                                                else
                                                {
                                                    if (item.PriceOfGOPID == iCBM)
                                                    {
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                            item.CBMTranfer = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                            item.CBM = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                            item.CBMBBGN = quantityTransfer.Value;
                                                    }
                                                    else
                                                    {
                                                        if (item.PriceOfGOPID == iQuantity)
                                                        {
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                                item.QuantityTranfer = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                                item.Quantity = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                                item.QuantityBBGN = quantityTransfer.Value;
                                                        }
                                                    }
                                                }
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
                            }
                            #endregion
                        }

                        // Qui đổi theo điểm
                        if (contractChange.TypeOfSGroupProductChangeID == -(int)SYSVarType.TypeOfSGroupProductChangeLocationTo)
                        {
                            #region Qui đổi OPS nhóm, hàng hóa
                            foreach (var itemGroup in ItemInput.ListOPSGroupProduct.Where(c => c.GroupOfProductID == itemChange.GroupOfProductID && c.ContractID == itemChange.ContractID && (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue ? c.ProductID == itemChange.ProductID : true)).GroupBy(c => c.LocationToID))
                            {
                                bool flag = false;
                                // Ktra nếu công thức đúng thì mới qui đổi
                                if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                {
                                    if (itemGroup.Any(d => d.ProductID == itemChange.ProductID))
                                    {
                                        DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityTranfer);
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityBBGN);
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                        itemTransfer.Expression = itemChange.Expression;
                                        itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                        try
                                        {
                                            flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                        }
                                        catch { }
                                    }
                                }
                                else
                                {
                                    //Qui đổi transfer
                                    DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                    itemTransfer.Ton = itemGroup.Sum(c => c.TonTranfer);
                                    itemTransfer.CBM = itemGroup.Sum(c => c.CBMTranfer);
                                    itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityTranfer);
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        itemTransfer.Ton = itemGroup.Sum(c => c.TonBBGN);
                                        itemTransfer.CBM = itemGroup.Sum(c => c.CBMBBGN);
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityBBGN);
                                    }
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        itemTransfer.Ton = itemGroup.Sum(c => c.Ton);
                                        itemTransfer.CBM = itemGroup.Sum(c => c.CBM);
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                    }
                                    itemTransfer.Expression = itemChange.Expression;
                                    itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                    try
                                    {
                                        flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                    }
                                    catch { }
                                }

                                if (flag)
                                {
                                    foreach (var item in itemGroup)
                                    {
                                        // Thay đổi nhóm khác
                                        if (itemChange.GroupOfProductIDChange.HasValue)
                                        {
                                            item.GroupOfProductID = itemChange.GroupOfProductIDChange.Value;
                                            item.PriceOfGOPID = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChange : item.PriceOfGOPID;
                                            item.PriceOfGOPName = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChangeName : item.PriceOfGOPName;
                                        }
                                        if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                        {
                                            if (itemChange.ProductID == item.ProductID)
                                            {
                                                //Qui đổi transfer
                                                DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                                itemTransfer.Quantity = item.QuantityTranfer;
                                                itemTransfer.Expression = itemChange.Expression;
                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                    itemTransfer.Quantity = item.QuantityBBGN;
                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                    itemTransfer.Quantity = item.Quantity;
                                                double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                                if (quantityTransfer.HasValue)
                                                {
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                                                    {
                                                        var exchangeCBM = itemChange.Weight > 0 && itemChange.CBM.HasValue ? itemChange.CBM.Value / itemChange.Weight.Value : 0;

                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.TonTranfer = quantityTransfer.Value;
                                                            item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.Ton = quantityTransfer.Value;
                                                            item.CBM = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.TonBBGN = quantityTransfer.Value;
                                                            item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue && itemChange.CBM > 0 ? itemChange.Weight.Value / itemChange.CBM.Value : 0;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.CBMTranfer = quantityTransfer.Value;
                                                            item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.CBM = quantityTransfer.Value;
                                                            item.Ton = quantityTransfer.Value * exchangeTon;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.CBMBBGN = quantityTransfer.Value;
                                                            item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                        }
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue ? itemChange.Weight.Value : 0;
                                                        var exchangeCBM = itemChange.CBM.HasValue ? itemChange.CBM.Value : 0;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                            item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                            item.QuantityTranfer = quantityTransfer.Value;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.Ton = quantityTransfer.Value * exchangeTon;
                                                            item.CBM = quantityTransfer.Value * exchangeCBM;
                                                            item.Quantity = quantityTransfer.Value;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                            item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                            item.QuantityBBGN = quantityTransfer.Value;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Qui đổi transfer
                                            DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                            itemTransfer.Ton = item.TonTranfer;
                                            itemTransfer.CBM = item.CBMTranfer;
                                            itemTransfer.Quantity = item.QuantityTranfer;
                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            {
                                                item.Ton = item.Ton;
                                                item.CBM = item.CBM;
                                                item.Quantity = item.Quantity;
                                            }
                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            {
                                                item.Ton = item.TonBBGN;
                                                item.CBM = item.CBMBBGN;
                                                item.Quantity = item.QuantityBBGN;
                                            }
                                            itemTransfer.Expression = itemChange.Expression;
                                            itemTransfer.ExpressionInput = itemChange.ExpressionInput;

                                            double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                            if (quantityTransfer.HasValue)
                                            {
                                                if (item.PriceOfGOPID.HasValue)
                                                {
                                                    if (item.PriceOfGOPID == iTon)
                                                    {
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                            item.TonTranfer = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                            item.Ton = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                            item.TonBBGN = quantityTransfer.Value;
                                                    }
                                                    else
                                                    {
                                                        if (item.PriceOfGOPID == iCBM)
                                                        {
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                                item.CBMTranfer = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                                item.CBM = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                                item.CBMBBGN = quantityTransfer.Value;
                                                        }
                                                        else
                                                        {
                                                            if (item.PriceOfGOPID == iQuantity)
                                                            {
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                                    item.QuantityTranfer = quantityTransfer.Value;
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                                    item.Quantity = quantityTransfer.Value;
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                                    item.QuantityBBGN = quantityTransfer.Value;
                                                            }
                                                        }
                                                    }
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
                                }
                            }
                            #endregion
                        }

                        // Qui đổi theo cung đường
                        if (contractChange.TypeOfSGroupProductChangeID == -(int)SYSVarType.TypeOfSGroupProductChangeRouting)
                        {
                            #region Qui đổi OPS nhóm, hàng hóa
                            foreach (var itemGroup in ItemInput.ListOPSGroupProduct.Where(c => c.GroupOfProductID == itemChange.GroupOfProductID && c.ContractID == itemChange.ContractID && (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue ? c.ProductID == itemChange.ProductID : true) && c.CATRoutingID > 0).GroupBy(c => c.CATRoutingID))
                            {
                                bool flag = false;
                                // Ktra nếu công thức đúng thì mới qui đổi
                                if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                {
                                    if (itemGroup.Any(d => d.ProductID == itemChange.ProductID))
                                    {
                                        DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityTranfer);
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityBBGN);
                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                        itemTransfer.Expression = itemChange.Expression;
                                        itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                        try
                                        {
                                            flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                        }
                                        catch { }
                                    }
                                }
                                else
                                {
                                    //Qui đổi transfer
                                    DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                    itemTransfer.Ton = itemGroup.Sum(c => c.TonTranfer);
                                    itemTransfer.CBM = itemGroup.Sum(c => c.CBMTranfer);
                                    itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityTranfer);
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        itemTransfer.Ton = itemGroup.Sum(c => c.TonBBGN);
                                        itemTransfer.CBM = itemGroup.Sum(c => c.CBMBBGN);
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.QuantityBBGN);
                                    }
                                    if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        itemTransfer.Ton = itemGroup.Sum(c => c.Ton);
                                        itemTransfer.CBM = itemGroup.Sum(c => c.CBM);
                                        itemTransfer.Quantity = itemGroup.Sum(c => c.Quantity);
                                    }
                                    itemTransfer.Expression = itemChange.Expression;
                                    itemTransfer.ExpressionInput = itemChange.ExpressionInput;
                                    try
                                    {
                                        flag = GetGroupOfProductTransfer_Check(itemTransfer);
                                    }
                                    catch { }
                                }

                                if (flag)
                                {
                                    foreach (var item in itemGroup)
                                    {
                                        // Thay đổi nhóm khác
                                        if (itemChange.GroupOfProductIDChange.HasValue)
                                        {
                                            item.GroupOfProductID = itemChange.GroupOfProductIDChange.Value;
                                            item.PriceOfGOPID = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChange : item.PriceOfGOPID;
                                            item.PriceOfGOPName = itemChange.PriceOfGOPIDChange > 0 ? itemChange.PriceOfGOPIDChangeName : item.PriceOfGOPName;
                                        }
                                        if (itemChange.ProductID.HasValue && itemChange.ProductIDChange.HasValue)
                                        {
                                            if (itemChange.ProductID == item.ProductID)
                                            {
                                                //Qui đổi transfer
                                                DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                                itemTransfer.Quantity = item.QuantityTranfer;
                                                itemTransfer.Expression = itemChange.Expression;
                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                    itemTransfer.Quantity = item.QuantityBBGN;
                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                    itemTransfer.Quantity = item.Quantity;
                                                double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                                if (quantityTransfer.HasValue)
                                                {
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon)
                                                    {
                                                        var exchangeCBM = itemChange.Weight > 0 && itemChange.CBM.HasValue ? itemChange.CBM.Value / itemChange.Weight.Value : 0;

                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.TonTranfer = quantityTransfer.Value;
                                                            item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.Ton = quantityTransfer.Value;
                                                            item.CBM = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.TonBBGN = quantityTransfer.Value;
                                                            item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                        }
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue && itemChange.CBM > 0 ? itemChange.Weight.Value / itemChange.CBM.Value : 0;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.CBMTranfer = quantityTransfer.Value;
                                                            item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.CBM = quantityTransfer.Value;
                                                            item.Ton = quantityTransfer.Value * exchangeTon;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.CBMBBGN = quantityTransfer.Value;
                                                            item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                        }
                                                    }
                                                    if (itemChange.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTU)
                                                    {
                                                        var exchangeTon = itemChange.Weight.HasValue ? itemChange.Weight.Value : 0;
                                                        var exchangeCBM = itemChange.CBM.HasValue ? itemChange.CBM.Value : 0;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                        {
                                                            item.TonTranfer = quantityTransfer.Value * exchangeTon;
                                                            item.CBMTranfer = quantityTransfer.Value * exchangeCBM;
                                                            item.QuantityTranfer = quantityTransfer.Value;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                        {
                                                            item.Ton = quantityTransfer.Value * exchangeTon;
                                                            item.CBM = quantityTransfer.Value * exchangeCBM;
                                                            item.Quantity = quantityTransfer.Value;
                                                        }
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                        {
                                                            item.TonBBGN = quantityTransfer.Value * exchangeTon;
                                                            item.CBMBBGN = quantityTransfer.Value * exchangeCBM;
                                                            item.QuantityBBGN = quantityTransfer.Value;
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                        else
                                        {
                                            //Qui đổi transfer
                                            DTOCATContractGroupOfProduct itemTransfer = new DTOCATContractGroupOfProduct();
                                            itemTransfer.Ton = item.TonTranfer;
                                            itemTransfer.CBM = item.CBMTranfer;
                                            itemTransfer.Quantity = item.QuantityTranfer;
                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            {
                                                item.Ton = item.Ton;
                                                item.CBM = item.CBM;
                                                item.Quantity = item.Quantity;
                                            }
                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            {
                                                item.Ton = item.TonBBGN;
                                                item.CBM = item.CBMBBGN;
                                                item.Quantity = item.QuantityBBGN;
                                            }
                                            itemTransfer.Expression = itemChange.Expression;
                                            itemTransfer.ExpressionInput = itemChange.ExpressionInput;

                                            double? quantityTransfer = GetGroupOfProductTransfer(itemTransfer);
                                            if (quantityTransfer.HasValue)
                                            {
                                                if (item.PriceOfGOPID.HasValue)
                                                {
                                                    if (item.PriceOfGOPID == iTon)
                                                    {
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                            item.TonTranfer = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                            item.Ton = quantityTransfer.Value;
                                                        if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                            item.TonBBGN = quantityTransfer.Value;
                                                    }
                                                    else
                                                    {
                                                        if (item.PriceOfGOPID == iCBM)
                                                        {
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                                item.CBMTranfer = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                                item.CBM = quantityTransfer.Value;
                                                            if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                                item.CBMBBGN = quantityTransfer.Value;
                                                        }
                                                        else
                                                        {
                                                            if (item.PriceOfGOPID == iQuantity)
                                                            {
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                                                    item.QuantityTranfer = quantityTransfer.Value;
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                                                    item.Quantity = quantityTransfer.Value;
                                                                if (contractChange.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                                                    item.QuantityBBGN = quantityTransfer.Value;
                                                            }
                                                        }
                                                    }
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
                                }
                            }
                            #endregion
                        }
                    }
                }
                #endregion

                #region Chạy từng hợp đồng
                foreach (var itemContract in ItemInput.ListContract)
                {
                    if (itemContract.CustomerID < 1) itemContract.CustomerID = null;

                    var lstPLTempContract = ItemInput.ListFINTemp.Where(c => c.ContractID == itemContract.ID);
                    System.Diagnostics.Debug.WriteLine("Contract start: " + itemContract.ID);
                    var queryMasterContract = ItemInput.ListTOMaster.Where(c => c.ContractID == itemContract.ID);
                    var queryMasterContractLoad = new List<HelperFinance_TOMaster>();
                    var queryMasterContractUnLoad = new List<HelperFinance_TOMaster>();
                    var queryOPSGroupProductContract = ItemInput.ListOPSGroupProduct.Where(c => c.ContractID == itemContract.ID);
                    var queryOPSGroupProductContractLoad = ItemInput.ListOPSGroupProduct.Where(c => (c.ContractID == itemContract.ID && (c.VendorLoadID == null || c.VendorLoadID == c.VendorID)) || (c.VendorLoadContractID == itemContract.ID));
                    var queryOPSGroupProductContractUnLoad = ItemInput.ListOPSGroupProduct.Where(c => (c.ContractID == itemContract.ID && (c.VendorUnLoadID == null || c.VendorUnLoadID == c.VendorID)) || (c.VendorUnLoadContractID == itemContract.ID));
                    if (queryOPSGroupProductContractLoad.Count() > 0)
                    {
                        var queryMasterID = queryOPSGroupProductContractLoad.Select(c => c.DITOMasterID).Distinct().ToList();
                        queryMasterContractLoad = ItemInput.ListTOMaster.Where(c => queryMasterID.Contains(c.ID)).ToList();
                    }
                    if (queryOPSGroupProductContractUnLoad.Count() > 0)
                    {
                        var queryMasterID = queryOPSGroupProductContractUnLoad.Select(c => c.DITOMasterID).Distinct().ToList();
                        queryMasterContractUnLoad = ItemInput.ListTOMaster.Where(c => queryMasterID.Contains(c.ID)).ToList();
                    }

                    if (queryMasterContract.Count() > 0 || queryOPSGroupProductContractLoad.Count() > 0 || queryOPSGroupProductContractUnLoad.Count() > 0)
                    {
                        //Các tham số input của Price
                        decimal totalPrice = 0, totalLoadPrice = 0, totalUnLoadPrice = 0;

                        #region Bảng giá LTL
                        //Bảng giá LTL thường
                        var lstPriceLTLGroup = ItemInput.ListLTL.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig);
                        if (lstPriceLTLGroup.Count() > 0)
                        {
                            foreach (var itemOPSGroupProductContract in queryOPSGroupProductContract)
                            {
                                var itemPrice = lstPriceLTLGroup.OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price).FirstOrDefault(c => c.RoutingID == itemOPSGroupProductContract.CATRoutingID && c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID);
                                if (itemPrice == null)
                                    itemPrice = lstPriceLTLGroup.OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price).FirstOrDefault(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.LocationFromID == itemOPSGroupProductContract.LocationFromID && c.LocationToID == itemOPSGroupProductContract.LocationToID);

                                if (itemPrice != null)
                                {
                                    itemOPSGroupProductContract.Price = itemPrice.Price;
                                    itemOPSGroupProductContract.CATRoutingID = itemPrice.RoutingID;
                                }
                                #region Tính tổng tiền
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                {
                                    if (itemOPSGroupProductContract.PriceOfGOPID == iTon)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.TonTranfer;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iCBM)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBMTranfer;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iQuantity)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.QuantityTranfer;
                                }
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                {
                                    if (itemOPSGroupProductContract.PriceOfGOPID == iTon)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.Ton;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iCBM)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBM;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iQuantity)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.Quantity;
                                }
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                {
                                    if (itemOPSGroupProductContract.PriceOfGOPID == iTon)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.TonBBGN;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iCBM)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBMBBGN;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iQuantity)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.QuantityBBGN;
                                }
                                #endregion
                            }
                        }

                        //Bảng giá LTL bậc thang
                        var lstPriceLTLLevel = ItemInput.ListLTLLevel.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig);
                        if (lstPriceLTLLevel.Count() > 0)
                        {
                            foreach (var itemOPSGroupProductContract in queryOPSGroupProductContract)
                            {
                                double totalTon = 0, totalCBM = 0, totalQuantity = 0;

                                #region Theo cung đường
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.TonTranfer);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.CBMTranfer);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.QuantityTranfer);
                                }
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.Ton);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.CBM);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.Quantity);
                                }
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.TonBBGN);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.CBMBBGN);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.CATRoutingID == itemOPSGroupProductContract.CATRoutingID).Sum(c => c.QuantityBBGN);
                                }
                                #endregion

                                #region Theo điểm
                                if (itemContract.TypeOfRunLevelID == -(int)SYSVarType.TypeOfRunLevelLocation)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.TonTranfer);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBMTranfer);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.QuantityTranfer);
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.Ton);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBM);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.Quantity);
                                    }
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.TonBBGN);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBMBBGN);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.QuantityBBGN);
                                    }
                                }
                                #endregion

                                #region Theo cung đường cha
                                if (itemContract.TypeOfRunLevelID == -(int)SYSVarType.TypeOfRunLevelParentRouting)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.TonTranfer);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.CBMTranfer);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.QuantityTranfer);
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.Ton);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.CBM);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.Quantity);
                                    }
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.TonBBGN);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.CBMBBGN);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.ParentRoutingID > 0 && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).Sum(c => c.QuantityBBGN);
                                    }
                                }
                                #endregion

                                #region Theo đơn hàng điểm
                                if (itemContract.TypeOfRunLevelID == -(int)SYSVarType.TypeOfRunLevelOrderLocation)
                                {
                                    totalTon = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.TonTranfer);
                                    totalCBM = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBMTranfer);
                                    totalQuantity = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.QuantityTranfer);
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.Ton);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBM);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.Quantity);
                                    }
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                    {
                                        totalTon = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.TonBBGN);
                                        totalCBM = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.CBMBBGN);
                                        totalQuantity = queryOPSGroupProductContract.Where(c => c.OrderID == itemOPSGroupProductContract.OrderID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Sum(c => c.QuantityBBGN);
                                    }
                                }
                                #endregion

                                var objPriceLTLLevel = lstPriceLTLLevel.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.RoutingID == itemOPSGroupProductContract.CATRoutingID && (c.Ton == 0 || c.Ton >= totalTon) && (c.CBM == 0 || c.CBM >= totalCBM) && (c.Quantity == 0 || c.Quantity >= totalQuantity)).OrderByDescending(c => c.EffectDate).ThenBy(c => c.Ton).ThenBy(c => c.CBM).ThenBy(c => c.Quantity).ThenByDescending(c => c.Price).FirstOrDefault();
                                if (objPriceLTLLevel == null)
                                    objPriceLTLLevel = lstPriceLTLLevel.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductID && c.LocationFromID == itemOPSGroupProductContract.LocationFromID && c.LocationToID == itemOPSGroupProductContract.LocationToID && (c.Ton == 0 || c.Ton >= totalTon) && (c.CBM == 0 || c.CBM >= totalCBM) && (c.Quantity == 0 || c.Quantity >= totalQuantity)).OrderByDescending(c => c.EffectDate).ThenBy(c => c.Ton).ThenBy(c => c.CBM).ThenBy(c => c.Quantity).ThenByDescending(c => c.Price).FirstOrDefault();

                                if (objPriceLTLLevel != null)
                                {
                                    itemOPSGroupProductContract.Price = objPriceLTLLevel.Price;
                                    itemOPSGroupProductContract.CATRoutingID = objPriceLTLLevel.RoutingID;
                                }
                                #region Tính tổng tiền
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                {
                                    if (itemOPSGroupProductContract.PriceOfGOPID == iTon)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.TonTranfer;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iCBM)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBMTranfer;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iQuantity)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.QuantityTranfer;
                                }
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                {
                                    if (itemOPSGroupProductContract.PriceOfGOPID == iTon)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.Ton;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iCBM)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBM;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iQuantity)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.Quantity;
                                }
                                if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                {
                                    if (itemOPSGroupProductContract.PriceOfGOPID == iTon)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.TonBBGN;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iCBM)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.CBMBBGN;
                                    else if (itemOPSGroupProductContract.PriceOfGOPID == iQuantity)
                                        totalPrice += itemOPSGroupProductContract.Price * (decimal)itemOPSGroupProductContract.QuantityBBGN;
                                }
                                #endregion
                            }
                        }

                        // Cập nhật giá chuyến LTL
                        foreach (var itemMasterContract in queryMasterContract.Where(c => c.TransportModeID == -(int)SYSVarType.TransportModeLTL))
                        {
                            var lstGroup = queryOPSGroupProductContract.Where(c => c.DITOMasterID == itemMasterContract.ID);
                            if (lstGroup != null && lstGroup.Count() > 0)
                            {
                                foreach (var item in lstGroup)
                                {
                                    if (item.PriceOfGOPID == iTon)
                                    {
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                            itemMasterContract.Price += item.Price * (decimal)item.TonTranfer;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemMasterContract.Price += item.Price * (decimal)item.Ton;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemMasterContract.Price += item.Price * (decimal)item.TonBBGN;
                                    }
                                    else if (item.PriceOfGOPID == iCBM)
                                    {
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                            itemMasterContract.Price += item.Price * (decimal)item.CBMTranfer;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemMasterContract.Price += item.Price * (decimal)item.CBM;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemMasterContract.Price += item.Price * (decimal)item.CBMBBGN;
                                    }
                                    else if (item.PriceOfGOPID == iQuantity)
                                    {
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                            itemMasterContract.Price += item.Price * (decimal)item.QuantityTranfer;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityPlan)
                                            itemMasterContract.Price += item.Price * (decimal)item.Quantity;
                                        if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                            itemMasterContract.Price += item.Price * (decimal)item.QuantityBBGN;
                                    }
                                }
                            }
                        }
                        #endregion

                        #region Bảng giá FTL
                        //Bảng giá FTL thường
                        var lstPriceFTLGroup = ItemInput.ListFTL.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig);
                        foreach (var itemMasterContract in queryMasterContract.Where(c => c.GroupOfVehicleID > 0 && c.TransportModeID == -(int)SYSVarType.TransportModeFTL))
                        {
                            decimal tempPrice = itemMasterContract.Price;
                            if (itemMasterContract.CATRoutingID > 0)
                            {
                                var itemPrice = lstPriceFTLGroup.OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price).FirstOrDefault(c => c.RoutingID == itemMasterContract.CATRoutingID && c.GroupOfVehicleID == itemMasterContract.GroupOfVehicleID);
                                if (itemPrice != null)
                                    tempPrice = itemPrice.Price;
                            }
                            else
                            {
                                // Tính giá cung đường cao nhất
                                foreach (var itemOPSGroupProductContract in queryOPSGroupProductContract.Where(c => c.DITOMasterID == itemMasterContract.ID))
                                {
                                    var itemPrice = lstPriceFTLGroup.OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price).FirstOrDefault(c => c.GroupOfVehicleID == itemOPSGroupProductContract.GroupOfVehicleID && c.RoutingID == itemOPSGroupProductContract.CATRoutingID);
                                    if (itemPrice == null)
                                        itemPrice = lstPriceFTLGroup.OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price).FirstOrDefault(c => c.GroupOfVehicleID == itemOPSGroupProductContract.GroupOfVehicleID && c.LocationFromID == itemOPSGroupProductContract.LocationFromID && c.LocationToID == itemOPSGroupProductContract.LocationToID);

                                    if (itemPrice != null && itemPrice.Price > tempPrice)
                                    {
                                        tempPrice = itemPrice.Price;
                                        itemMasterContract.CATRoutingID = itemPrice.RoutingID;
                                    }

                                }
                            }
                            itemMasterContract.Price = tempPrice;
                            totalPrice += itemMasterContract.Price;
                            foreach (var itemOrderGroupProductContract in queryOPSGroupProductContract.Where(c => c.DITOMasterID == itemMasterContract.ID))
                                itemOrderGroupProductContract.Price = tempPrice;
                        }

                        //Bảng giá FTL bậc thang
                        var lstPriceFTLLevel = ItemInput.ListFTLLevel.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig);
                        if (lstPriceFTLLevel.Count() > 0)
                        {
                            foreach (var itemMasterContract in queryMasterContract.Where(c => c.GroupOfVehicleID > 0 && c.TransportModeID == -(int)SYSVarType.TransportModeFTL))
                            {
                                decimal tempPrice = 0;
                                // Tính theo cung đường đã có
                                if (itemMasterContract.CATRoutingID > 0)
                                {
                                    var lstPriceLevel = lstPriceFTLLevel.Where(c => c.GroupOfVehicleID == itemMasterContract.GroupOfVehicleID && c.RoutingID == itemMasterContract.CATRoutingID).Select(c => new
                                    {
                                        DateStart = c.DateStart,
                                        DateEnd = c.DateEnd,
                                        c.RoutingID,
                                        c.Price
                                    }).ToList();
                                    if (lstPriceLevel != null && lstPriceLevel.Count > 0)
                                    {
                                        foreach (var priceGV in lstPriceLevel)
                                        {
                                            // Ngày
                                            if (priceGV.DateStart <= priceGV.DateEnd)
                                            {
                                                if (priceGV.DateStart.TimeOfDay <= itemMasterContract.DateConfig.TimeOfDay && itemMasterContract.DateConfig.TimeOfDay < priceGV.DateStart.TimeOfDay)
                                                    if (priceGV.Price > tempPrice)
                                                    {
                                                        tempPrice = priceGV.Price;
                                                        itemMasterContract.CATRoutingID = priceGV.RoutingID;
                                                    }
                                            }
                                            else
                                            {
                                                // Đêm
                                                if (priceGV.DateStart.TimeOfDay <= itemMasterContract.DateConfig.TimeOfDay || itemMasterContract.DateConfig.TimeOfDay < priceGV.DateStart.TimeOfDay)
                                                    if (priceGV.Price > tempPrice)
                                                    {
                                                        tempPrice = priceGV.Price;
                                                        itemMasterContract.CATRoutingID = priceGV.RoutingID;
                                                    }
                                            }
                                        }
                                    }
                                }
                                else
                                {
                                    // Tính giá cung đường cao nhất của group
                                    foreach (var itemOPSGroupProductContract in queryOPSGroupProductContract.Where(c => c.DITOMasterID == itemMasterContract.ID))
                                    {
                                        var lstPriceLevel = lstPriceFTLLevel.Where(c => c.GroupOfVehicleID == itemMasterContract.GroupOfVehicleID && c.RoutingID == itemOPSGroupProductContract.CATRoutingID).Select(c => new
                                        {
                                            DateStart = c.DateStart,
                                            DateEnd = c.DateEnd,
                                            c.RoutingID,
                                            c.Price
                                        }).ToList();
                                        if (lstPriceLevel.Count == 0)
                                        {
                                            lstPriceLevel = lstPriceFTLLevel.Where(c => c.GroupOfVehicleID == itemMasterContract.GroupOfVehicleID && c.LocationFromID == itemOPSGroupProductContract.LocationFromID && c.LocationToID == itemOPSGroupProductContract.LocationToID).Select(c => new
                                            {
                                                DateStart = c.DateStart,
                                                DateEnd = c.DateEnd,
                                                c.RoutingID,
                                                c.Price
                                            }).ToList();
                                        }
                                        if (lstPriceLevel != null && lstPriceLevel.Count > 0)
                                        {
                                            foreach (var priceGV in lstPriceLevel)
                                            {
                                                // Ngày
                                                if (priceGV.DateStart <= priceGV.DateEnd)
                                                {
                                                    if (priceGV.DateStart.TimeOfDay <= itemMasterContract.DateConfig.TimeOfDay && itemMasterContract.DateConfig.TimeOfDay < priceGV.DateStart.TimeOfDay)
                                                        if (priceGV.Price > tempPrice)
                                                        {
                                                            tempPrice = priceGV.Price;
                                                            itemMasterContract.CATRoutingID = priceGV.RoutingID;
                                                        }
                                                }
                                                else
                                                {
                                                    // Đêm
                                                    if (priceGV.DateStart.TimeOfDay <= itemMasterContract.DateConfig.TimeOfDay || itemMasterContract.DateConfig.TimeOfDay < priceGV.DateStart.TimeOfDay)
                                                        if (priceGV.Price > tempPrice)
                                                        {
                                                            tempPrice = priceGV.Price;
                                                            itemMasterContract.CATRoutingID = priceGV.RoutingID;
                                                        }
                                                }
                                            }
                                        }
                                    }
                                }
                                itemMasterContract.Price = tempPrice;
                                totalPrice += itemMasterContract.Price;
                            }
                        }

                        #endregion

                        #region Bảng giá bốc xếp
                        var lstPriceLoadGroup = ItemInput.ListLoad.Where(c => c.ContractID == itemContract.ID && c.IsLoading && c.EffectDate <= DateConfig);
                        var lstPriceUnLoadGroup = ItemInput.ListLoad.Where(c => c.ContractID == itemContract.ID && !c.IsLoading && c.EffectDate <= DateConfig);

                        foreach (var itemOPSGroupProductContract in queryOPSGroupProductContractLoad)
                        {
                            itemOPSGroupProductContract.IsLoading = false;
                            itemOPSGroupProductContract.UnitPriceLoad = 0;
                            itemOPSGroupProductContract.QuantityLoad = 0;
                            itemOPSGroupProductContract.PriceLoad = 0;
                            var priceLoad = lstPriceLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductLoadID && c.LocationID == itemOPSGroupProductContract.LocationFromID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceLoad == null)
                                priceLoad = lstPriceLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductLoadID && c.PartnerID == itemOPSGroupProductContract.PartnerID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceLoad == null)
                                priceLoad = lstPriceLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductLoadID && c.GroupOfLocationID == itemOPSGroupProductContract.GroupOfLocationID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceLoad == null)
                                priceLoad = lstPriceLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductLoadID && c.RoutingID == itemOPSGroupProductContract.CATRoutingID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceLoad == null)
                                priceLoad = lstPriceLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductLoadID && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceLoad != null)
                            {
                                itemOPSGroupProductContract.IsLoading = true;
                                itemOPSGroupProductContract.UnitPriceLoad = priceLoad.Price;
                                if (priceLoad.PriceOfGOPID == iTon)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.TonTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.Ton;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.TonBBGN;
                                }
                                else if (priceLoad.PriceOfGOPID == iCBM)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.CBMTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.CBM;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.CBMBBGN;
                                }
                                else if (priceLoad.PriceOfGOPID == iQuantity)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.QuantityTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.Quantity;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityLoad = itemOPSGroupProductContract.QuantityBBGN;
                                }
                                itemOPSGroupProductContract.PriceLoad = itemOPSGroupProductContract.UnitPriceLoad * (decimal)itemOPSGroupProductContract.QuantityLoad;
                                totalLoadPrice += itemOPSGroupProductContract.PriceLoad;
                            }
                        }

                        foreach (var itemOPSGroupProductContract in queryOPSGroupProductContractUnLoad)
                        {
                            itemOPSGroupProductContract.IsUnLoading = false;
                            itemOPSGroupProductContract.UnitPriceUnLoad = 0;
                            itemOPSGroupProductContract.QuantityUnLoad = 0;
                            itemOPSGroupProductContract.PriceUnLoad = 0;
                            var priceUnLoad = lstPriceUnLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductUnLoadID && c.LocationID == itemOPSGroupProductContract.LocationToID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceUnLoad == null)
                                priceUnLoad = lstPriceUnLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductUnLoadID && c.PartnerID == itemOPSGroupProductContract.PartnerID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceUnLoad == null)
                                priceUnLoad = lstPriceUnLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductUnLoadID && c.GroupOfLocationID == itemOPSGroupProductContract.GroupOfLocationID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceUnLoad == null)
                                priceUnLoad = lstPriceUnLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductUnLoadID && c.RoutingID == itemOPSGroupProductContract.CATRoutingID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceUnLoad == null)
                                priceUnLoad = lstPriceUnLoadGroup.Where(c => c.GroupOfProductID == itemOPSGroupProductContract.GroupOfProductUnLoadID && c.ParentRoutingID == itemOPSGroupProductContract.ParentRoutingID).OrderByDescending(c => c.EffectDate).ThenByDescending(c => c.Price > 0).FirstOrDefault();
                            if (priceUnLoad != null)
                            {
                                itemOPSGroupProductContract.IsUnLoading = true;
                                itemOPSGroupProductContract.UnitPriceUnLoad = priceUnLoad.Price;
                                if (priceUnLoad.PriceOfGOPID == iTon)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.TonTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.Ton;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.TonBBGN;
                                }
                                else if (priceUnLoad.PriceOfGOPID == iCBM)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.CBMTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.CBM;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.CBMBBGN;
                                }
                                else if (priceUnLoad.PriceOfGOPID == iQuantity)
                                {
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityTransfer)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.QuantityTranfer;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantity)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.Quantity;
                                    if (itemContract.TypeOfContractQuantityID == -(int)SYSVarType.TypeOfContractQuantityBBGN)
                                        itemOPSGroupProductContract.QuantityUnLoad = itemOPSGroupProductContract.QuantityBBGN;
                                }
                                itemOPSGroupProductContract.PriceUnLoad = itemOPSGroupProductContract.UnitPriceUnLoad * (decimal)itemOPSGroupProductContract.QuantityUnLoad;
                                totalUnLoadPrice += itemOPSGroupProductContract.PriceUnLoad;
                            }
                        }

                        // Cập nhật giá bốc xếp
                        foreach (var itemMasterContract in queryMasterContractLoad)
                        {
                            var lstGroupLoad = queryOPSGroupProductContractLoad.Where(c => c.DITOMasterID == itemMasterContract.ID && c.IsLoading);
                            if (lstGroupLoad != null && lstGroupLoad.Count() > 0)
                            {
                                itemMasterContract.IsLoading = true;
                                itemMasterContract.PriceLoad = lstGroupLoad.Sum(c => c.PriceLoad);
                            }
                        }
                        foreach (var itemMasterContract in queryMasterContractUnLoad)
                        {
                            var lstGroupUnLoad = queryOPSGroupProductContractUnLoad.Where(c => c.DITOMasterID == itemMasterContract.ID && c.IsUnLoading);
                            if (lstGroupUnLoad != null && lstGroupUnLoad.Count() > 0)
                            {
                                itemMasterContract.IsUnLoading = true;
                                itemMasterContract.PriceUnLoad = lstGroupUnLoad.Sum(c => c.PriceUnLoad);
                            }
                        }

                        #endregion

                        System.Diagnostics.Debug.WriteLine("Chi Lấy dữ liệu");

                        #region Vận chuyển và MOQ
                        var lstMOQ = ItemInput.ListMOQ.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig).ToList();
                        var MOQEffateDate = lstMOQ.Select(c => new { c.EffectDate }).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                        if (MOQEffateDate != null)
                            lstMOQ = lstMOQ.Where(c => c.EffectDate == MOQEffateDate.EffectDate).ToList();
                        //Chạy từng MOQ
                        string strMOQName = string.Empty;
                        foreach (var itemMOQ in lstMOQ)
                        {
                            System.Diagnostics.Debug.WriteLine("Chi MOQ vận chuyển: " + itemMOQ.MOQName + " ID: " + itemMOQ.ID);

                            var lstMasterMOQ = new List<HelperFinance_TOMaster>();
                            var lstOPSGroupMOQ = new List<HelperFinance_OPSGroupProduct>();

                            var queryMasterMOQ = queryMasterContract.ToList();
                            var queryOPSGroupMOQ = queryOPSGroupProductContract.ToList();

                            strMOQName = itemMOQ.MOQName;
                            //Danh sách các điều kiện lọc 
                            var lstMOQParentRouting = itemMOQ.ListParentRouting;
                            var lstMOQRouting = itemMOQ.ListRouting;
                            var lstMOQGroupLocation = itemMOQ.ListGroupOfLocation;
                            var lstMOQPartner = itemMOQ.ListPartnerID;
                            var lstMOQProvince = itemMOQ.ListProvinceID;
                            var lstMOQLocationFrom = itemMOQ.ListLocation.Where(c => c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                                .Select(c => c.LocationID).ToList();
                            var lstMOQLocationTo = itemMOQ.ListLocation.Where(c => (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                                .Select(c => c.LocationID).ToList();
                            var lstMOQGroupProduct = itemMOQ.ListGroupProduct.Select(c => c.GroupOfProductID).ToList();

                            if (lstMOQParentRouting.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQParentRouting.Contains(c.ParentRoutingID)).ToList();
                            if (lstMOQRouting.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQRouting.Contains(c.CATRoutingID)).ToList();
                            if (lstMOQGroupLocation.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQGroupLocation.Contains(c.GroupOfLocationID)).ToList();
                            if (lstMOQLocationFrom.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQLocationFrom.Contains(c.LocationFromID)).ToList();
                            if (lstMOQLocationTo.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQLocationTo.Contains(c.LocationToID)).ToList();
                            if (lstMOQGroupProduct.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQGroupProduct.Contains(c.GroupOfProductID)).ToList();
                            if (lstMOQPartner.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQPartner.Contains(c.PartnerID)).ToList();
                            if (lstMOQProvince.Count > 0)
                                queryOPSGroupMOQ = queryOPSGroupMOQ.Where(c => lstMOQProvince.Contains(c.LocationToProvinceID)).ToList();

                            var moqMasterID = queryMasterMOQ.Select(c => c.ID).ToArray();
                            var moqGroupID = queryOPSGroupMOQ.Select(c => c.DITOMasterID).Distinct().ToArray();
                            var lstMasterCheckID = moqGroupID.Intersect(moqMasterID).ToList();
                            var lstMasterCheck = queryMasterMOQ.Where(c => lstMasterCheckID.Contains(c.ID)).ToList();
                            var lstMasterGroupCheck = queryOPSGroupMOQ.Where(c => lstMasterCheckID.Contains(c.DITOMasterID)).ToList();

                            if (lstMasterCheck.Count > 0 && lstMasterGroupCheck.Count > 0)
                            {
                                //Thực hiện kiểm tra công thức
                                #region Tính theo ngày
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderInDay || itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumScheduleInDay)
                                {
                                    bool flag = false;
                                    //Thực hiện công thức
                                    DTOPriceDIExExpr itemExpr = Expr_Generate(lstMasterGroupCheck.ToList());
                                    itemExpr.UnitPriceMax = lstMasterGroupCheck.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                    itemExpr.UnitPriceMin = lstMasterGroupCheck.OrderBy(c => c.Price).FirstOrDefault().Price;
                                    var lstProvinceID = lstMasterGroupCheck.Select(c => c.LocationToProvinceID).Distinct().ToList();
                                    itemExpr.IsHasPartnerProvince = queryOPSGroupProductContract.Any(c => !lstMOQGroupLocation.Contains(c.GroupOfLocationID) && lstProvinceID.Contains(c.LocationToProvinceID));
                                    try
                                    {
                                        flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                    }
                                    catch { flag = false; }

                                    if (flag == true)
                                    {
                                        lstMasterMOQ = lstMasterCheck;
                                        lstOPSGroupMOQ = lstMasterGroupCheck;
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrder)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }

                                        if (flag == true)
                                        {
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToArray());
                                            var lstMasterID = itemGroup.Select(c => c.DITOMasterID).Distinct().ToList();
                                            foreach (var item in lstMasterID)
                                            {
                                                if (lstMasterMOQ.Count(c => c.ID == item) == 0)
                                                    lstMasterMOQ.Add(lstMasterCheck.FirstOrDefault(c => c.ID == item));
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumSchedule)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }

                                        if (flag == true)
                                        {
                                            lstMasterMOQ.Add(lstMasterCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo location
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderLocation)
                                {
                                    //Thực hiện công thức cho từng Location
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.LocationToID))
                                    {
                                        bool flag = false;
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempMasterID = itemGroup.Select(c => c.DITOMasterID).Distinct().ToList();
                                            foreach (var tempMasterID in lstTempMasterID)
                                            {
                                                if (lstMasterMOQ.Count(c => c.ID == tempMasterID) == 0)
                                                    lstMasterMOQ.Add(lstMasterCheck.FirstOrDefault(c => c.ID == tempMasterID));
                                            }
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToList());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo route
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderRoute)
                                {
                                    //Thực hiện công thức cho từng Location
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.CATRoutingID))
                                    {
                                        bool flag = false;
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempMasterID = itemGroup.Select(c => c.DITOMasterID).Distinct().ToList();
                                            foreach (var tempMasterID in lstTempMasterID)
                                            {
                                                if (lstMasterMOQ.Count(c => c.ID == tempMasterID) == 0)
                                                    lstMasterMOQ.Add(lstMasterCheck.FirstOrDefault(c => c.ID == tempMasterID));
                                            }
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToList());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng trả về - chỉ phát sinh price ko change price
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumReturnOrder)
                                {
                                    lstMasterGroupCheck = lstMasterGroupCheck.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstMasterID = itemGroup.Select(c => c.DITOMasterID).Distinct().ToList();
                                            foreach (var masterID in lstMasterID)
                                            {
                                                if (lstMasterMOQ.Count(c => c.ID == masterID) == 0)
                                                {
                                                    lstMasterMOQ.Add(lstMasterCheck.FirstOrDefault(c => c.ID == masterID));
                                                }
                                            }
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToList());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến hàng trả về - chỉ phát sinh price ko change price
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumReturnSchedule)
                                {
                                    lstMasterGroupCheck = lstMasterGroupCheck.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemMOQ.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstMasterMOQ.Add(lstMasterCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupMOQ.AddRange(itemGroup.ToList());
                                        }
                                    }
                                }
                                #endregion
                            }

                            //Thực hiện lấy output MOQ
                            if (itemMOQ.DIMOQSumID != -(int)SYSVarType.DIMOQSumReturnSchedule && itemMOQ.DIMOQSumID != -(int)SYSVarType.DIMOQSumReturnOrder)
                            {
                                foreach (var itemMasterMOQ in lstMasterMOQ)
                                {
                                    itemMasterMOQ.HasMOQ = true;
                                    itemMasterMOQ.GetPointCurrent = lstOPSGroupMOQ.Where(c => c.DITOMasterID == itemMasterMOQ.ID).Select(c => c.LocationFromID).Distinct().Count();
                                    itemMasterMOQ.DropPointCurrent = lstOPSGroupMOQ.Where(c => c.DITOMasterID == itemMasterMOQ.ID).Select(c => c.LocationToID).Distinct().Count();
                                }
                                foreach (var itemOPSGroupMOQ in lstOPSGroupMOQ)
                                {
                                    itemOPSGroupMOQ.HasMOQ = true;
                                }
                            }
                            if (lstMasterMOQ.Count > 0 && lstOPSGroupMOQ.Count > 0)
                            {
                                //Thực hiện nếu output là fix price
                                #region Tính theo ngày
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderInDay || itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumScheduleInDay)
                                {
                                    //Thực hiện công thức
                                    DTOPriceDIExExpr itemExpr = Expr_Generate(lstMasterGroupCheck.ToList());
                                    itemExpr.UnitPriceMax = lstMasterGroupCheck.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                    itemExpr.UnitPriceMin = lstMasterGroupCheck.OrderBy(c => c.Price).FirstOrDefault().Price;

                                    decimal? priceFix = null, priceMOQ = null, tonMOQ = null, cbmMOQ = null, quantityMOQ = null;

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                        priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                        tonMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                        cbmMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                        quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                    // PL tạm
                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.VendorID = itemContract.VendorID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                    {
                                        // set về 0
                                        foreach (var tempOrderGroupLocationCheck in lstMasterMOQ)
                                            tempOrderGroupLocationCheck.Price = 0;

                                        if (priceFix.HasValue)
                                        {
                                            FIN_PLDetails plCost = new FIN_PLDetails();
                                            plCost.CreatedBy = Account.UserName;
                                            plCost.CreatedDate = DateTime.Now;
                                            plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                            plCost.Note = itemMOQ.MOQName;
                                            plCost.Debit = priceFix.Value;
                                            plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                            pl.FIN_PLDetails.Add(plCost);
                                            pl.Debit += plCost.Debit;

                                            var lstOPSGroupID = lstOPSGroupMOQ.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                            DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                            lstPl.Add(pl);
                                        }
                                    }
                                    else
                                    {
                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.Quantity = tonMOQ.HasValue ? (double)tonMOQ.Value : cbmMOQ.HasValue ? (double)cbmMOQ.Value : quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Debit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                var lstOPSGroupID = lstOPSGroupMOQ.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQGroupProduct.Count() > 0)
                                        {
                                            var lstPriceMOQGroupProductID = lstPriceMOQGroupProduct.Select(c => c.GroupOfProductID).Distinct().ToList();
                                            var lstOPSGroupCheck = lstOPSGroupMOQ.Where(c => lstPriceMOQGroupProductID.Contains(c.GroupOfProductID));
                                            double totalCMBMOQ = 0, totalTonMOQ = 0;
                                            if (lstOPSGroupCheck.Count() > 0)
                                            {
                                                var lstTonMOQ = lstOPSGroupCheck.Where(c => c.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTon);
                                                if (lstTonMOQ.Count() > 0)
                                                    totalTonMOQ = lstTonMOQ.Sum(c => c.TonTranfer);
                                                var lstCBMMOQ = lstOPSGroupCheck.Where(c => c.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPCBM);
                                                if (lstCBMMOQ.Count() > 0)
                                                    totalCMBMOQ = lstCBMMOQ.Sum(c => c.CBMTranfer);
                                            }
                                            foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                            {
                                                foreach (var tempOrderGroupLocationCheck in lstOPSGroupMOQ.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                {
                                                    DTOPriceDIExExpr itemExprGroup = new DTOPriceDIExExpr();
                                                    itemExprGroup.TonOrder = tempOrderGroupLocationCheck.TonOrder;
                                                    itemExprGroup.CBMOrder = tempOrderGroupLocationCheck.CBMOrder;
                                                    itemExprGroup.QuantityOrder = tempOrderGroupLocationCheck.QuantityOrder;
                                                    itemExprGroup.TonTransfer = tempOrderGroupLocationCheck.TonTranfer;
                                                    itemExprGroup.CBMTransfer = tempOrderGroupLocationCheck.CBMTranfer;
                                                    itemExprGroup.QuantityTransfer = tempOrderGroupLocationCheck.QuantityTranfer;
                                                    itemExprGroup.TonActual = tempOrderGroupLocationCheck.TonActual;
                                                    itemExprGroup.CBMActual = tempOrderGroupLocationCheck.CBMActual;
                                                    itemExprGroup.QuantityActual = tempOrderGroupLocationCheck.QuantityActual;
                                                    itemExprGroup.TonReturn = tempOrderGroupLocationCheck.TonReturn;
                                                    itemExprGroup.CBMReturn = tempOrderGroupLocationCheck.CBMReturn;
                                                    itemExprGroup.QuantityReturn = tempOrderGroupLocationCheck.QuantityReturn;
                                                    itemExprGroup.UnitPrice = tempOrderGroupLocationCheck.Price;
                                                    itemExprGroup.UnitPriceMax = itemExpr.UnitPriceMax;
                                                    itemExprGroup.UnitPriceMin = itemExpr.UnitPriceMin;
                                                    itemExprGroup.TonMOQ = totalTonMOQ;
                                                    itemExprGroup.CBMMOQ = totalCMBMOQ;
                                                    itemExprGroup.TotalTonTransfer = lstMasterGroupCheck.Sum(c => c.TonTranfer);
                                                    itemExprGroup.TotalCBMTransfer = lstMasterGroupCheck.Sum(c => c.CBMTranfer);
                                                    itemExprGroup.TotalQuantityTransfer = lstMasterGroupCheck.Sum(c => c.QuantityTranfer);
                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExprGroup);
                                                    if (priceGroupMOQ.HasValue)
                                                        tempOrderGroupLocationCheck.Price = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExprGroup);
                                                    if (quantityGroupMOQ.HasValue)
                                                        tempOrderGroupLocationCheck.QuantityMOQ = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            foreach (var tempOrderGroupLocationCheck in lstOPSGroupMOQ)
                                                tempOrderGroupLocationCheck.Price = 0;
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrder)
                                {
                                    foreach (var itemGroup in lstOPSGroupMOQ.GroupBy(c => c.OrderID))
                                    {
                                        // Tạo pl temp
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        var priceMaster = lstMasterMOQ.FirstOrDefault(c => c.ID == itemGroup.Key);
                                        if (priceMaster != null)
                                            itemExpr.Debit = priceMaster.Price;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            // set về 0
                                            foreach (var itemOPSGroupMOQ in itemGroup)
                                                itemOPSGroupMOQ.Price = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                //plCost.Note1 = string.Join(",", itemOPSGroupMOQTemp.Select(c => c.OrderCode).Distinct().ToList());
                                                plCost.Debit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    //plCost.Note1 = string.Join(",", itemOPSGroupMOQTemp.Select(c => c.OrderCode).Distinct().ToList());
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Debit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            // Change giá + sản lượng
                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);
                                                        itemExpr.UnitPrice = itemOPSGroupMOQ.Price;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        if (priceGroupMOQ.HasValue)
                                                            itemOPSGroupMOQ.Price = priceGroupMOQ.Value;

                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);
                                                        if (quantityGroupMOQ.HasValue)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)quantityGroupMOQ.Value;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // set về 0
                                                foreach (var itemOPSGroupMOQ in itemGroup)
                                                    itemOPSGroupMOQ.Price = 0;
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumSchedule)
                                {
                                    foreach (var itemGroup in lstOPSGroupMOQ.GroupBy(c => c.DITOMasterID))
                                    {
                                        // Tạo pl temp
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        var priceMaster = lstMasterMOQ.FirstOrDefault(c => c.ID == itemGroup.Key);
                                        if (priceMaster != null)
                                            itemExpr.Debit = priceMaster.Price;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            // set về 0
                                            foreach (var itemOPSGroupMOQ in itemGroup)
                                                itemOPSGroupMOQ.Price = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                //plCost.Note1 = string.Join(",", itemOPSGroupMOQTemp.Select(c => c.OrderCode).Distinct().ToList());
                                                plCost.Debit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    //plCost.Note1 = string.Join(",", itemOPSGroupMOQTemp.Select(c => c.OrderCode).Distinct().ToList());
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Debit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            // Change giá + sản lượng
                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);
                                                        itemExpr.UnitPrice = itemOPSGroupMOQ.Price;
                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        if (priceGroupMOQ.HasValue)
                                                            itemOPSGroupMOQ.Price = priceGroupMOQ.Value;

                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);
                                                        if (quantityGroupMOQ.HasValue)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)quantityGroupMOQ.Value;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                // set về 0
                                                foreach (var itemOPSGroupMOQ in itemGroup)
                                                    itemOPSGroupMOQ.Price = 0;
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo Location
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderLocation)
                                {
                                    // Thực hiện công thức cho từng Location
                                    foreach (var itemGroup in lstOPSGroupMOQ.GroupBy(c => c.LocationToID))
                                    {
                                        // PL tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;
                                        decimal? priceFix = null, priceMOQ = null, tonMOQ = null, cbmMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            tonMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            cbmMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            // set về 0
                                            foreach (var tempOrderGroupLocationCheck in itemGroup)
                                                tempOrderGroupLocationCheck.Price = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.Debit = priceFix.Value;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                //plCost.Note1 = string.Join(",", itemOrderGroupLocationCheck.Select(c => c.OrderCode).Distinct().ToList());
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.Quantity = tonMOQ.HasValue ? (double)tonMOQ.Value : cbmMOQ.HasValue ? (double)cbmMOQ.Value : quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Debit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    //plCost.Note1 = string.Join(",", itemOrderGroupLocationCheck.Select(c => c.OrderCode).Distinct().ToList());
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var tempOrderGroupLocationCheck in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        DTOPriceDIExExpr itemExprGroup = Expr_GenerateItem(tempOrderGroupLocationCheck);
                                                        itemExprGroup.UnitPrice = tempOrderGroupLocationCheck.Price;
                                                        itemExprGroup.UnitPriceMax = itemExpr.UnitPriceMax;
                                                        itemExprGroup.UnitPriceMin = itemExpr.UnitPriceMin;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExprGroup);
                                                        if (priceGroupMOQ.HasValue)
                                                            tempOrderGroupLocationCheck.Price = priceGroupMOQ.Value;

                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExprGroup);
                                                        if (quantityGroupMOQ.HasValue)
                                                            tempOrderGroupLocationCheck.QuantityMOQ = (double)quantityGroupMOQ.Value;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                foreach (var tempOrderGroupLocationCheck in itemGroup)
                                                    tempOrderGroupLocationCheck.Price = 0;
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo Routing
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumOrderRoute)
                                {
                                    //Thực hiện công thức cho từng Location
                                    foreach (var itemGroup in lstOPSGroupMOQ.GroupBy(c => c.CATRoutingID))
                                    {
                                        // PL tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;
                                        decimal? priceFix = null, priceMOQ = null, tonMOQ = null, cbmMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            tonMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            cbmMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            // set về 0
                                            foreach (var tempOrderGroupLocationCheck in itemGroup)
                                                tempOrderGroupLocationCheck.Price = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.Debit = priceFix.Value;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                //plCost.Note1 = string.Join(",", itemOrderGroupLocationCheck.Select(c => c.OrderCode).Distinct().ToList());
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.Quantity = tonMOQ.HasValue ? (double)tonMOQ.Value : cbmMOQ.HasValue ? (double)cbmMOQ.Value : quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Debit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    //plCost.Note1 = string.Join(",", itemOrderGroupLocationCheck.Select(c => c.OrderCode).Distinct().ToList());
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var tempOrderGroupLocationCheck in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        DTOPriceDIExExpr itemExprGroup = Expr_GenerateItem(tempOrderGroupLocationCheck);
                                                        itemExprGroup.UnitPrice = tempOrderGroupLocationCheck.Price;
                                                        itemExprGroup.UnitPriceMax = itemExpr.UnitPriceMax;
                                                        itemExprGroup.UnitPriceMin = itemExpr.UnitPriceMin;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExprGroup);
                                                        if (priceGroupMOQ.HasValue)
                                                            tempOrderGroupLocationCheck.Price = priceGroupMOQ.Value;

                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExprGroup);
                                                        if (quantityGroupMOQ.HasValue)
                                                            tempOrderGroupLocationCheck.QuantityMOQ = (double)quantityGroupMOQ.Value;
                                                    }
                                                }
                                            }
                                            else
                                            {
                                                foreach (var tempOrderGroupLocationCheck in itemGroup)
                                                    tempOrderGroupLocationCheck.Price = 0;
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng trả về - chỉ phát sinh price ko change price
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumReturnOrder)
                                {
                                    lstOPSGroupMOQ = lstOPSGroupMOQ.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOPSGroupMOQ.GroupBy(c => c.OrderID))
                                    {
                                        // Tạo pl temp
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                plCost.Debit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Debit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = new DTOPriceDIExExpr();
                                                        itemExpr.TonTransfer = itemOPSGroupMOQ.TonTranfer;
                                                        itemExpr.CBMTransfer = itemOPSGroupMOQ.CBMTranfer;
                                                        itemExpr.QuantityTransfer = itemOPSGroupMOQ.QuantityTranfer;
                                                        itemExpr.TonActual = itemOPSGroupMOQ.TonActual;
                                                        itemExpr.CBMActual = itemOPSGroupMOQ.CBMActual;
                                                        itemExpr.QuantityActual = itemOPSGroupMOQ.QuantityActual;
                                                        itemExpr.TonReturn = itemOPSGroupMOQ.TonReturn;
                                                        itemExpr.CBMReturn = itemOPSGroupMOQ.CBMReturn;
                                                        itemExpr.QuantityReturn = itemOPSGroupMOQ.QuantityReturn;
                                                        itemExpr.UnitPrice = itemOPSGroupMOQ.Price;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);

                                                        if (quantityGroupMOQ == null)
                                                            if (itemOPSGroupMOQ.PriceOfGOPID == iTon)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.TonReturn;
                                                            else if (itemOPSGroupMOQ.PriceOfGOPID == iCBM)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.CBMReturn;
                                                            else if (itemOPSGroupMOQ.PriceOfGOPID == iQuantity)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.QuantityReturn;

                                                        if (itemOPSGroupMOQ.PriceOfGOPID == iTon)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.TonTranfer;
                                                        else if (itemOPSGroupMOQ.PriceOfGOPID == iCBM)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.CBMTranfer;
                                                        else if (itemOPSGroupMOQ.PriceOfGOPID == iQuantity)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.QuantityTranfer;

                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.DITOReturnDebit;
                                                        plCost.Note = itemMOQ.MOQName;
                                                        plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        lstPl.Add(pl);

                                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.GroupOfProductID = itemOPSGroupMOQ.ID;
                                                        plGroup.Quantity = quantityGroupMOQ.HasValue ? (double)quantityGroupMOQ.Value : 0;
                                                        plGroup.UnitPrice = priceGroupMOQ.HasValue ? priceGroupMOQ.Value : 0;
                                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                        plCost.Debit += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                                                        pl.Debit += plCost.Debit;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến hàng trả về - chỉ phát sinh price ko change price
                                if (itemMOQ.DIMOQSumID == -(int)SYSVarType.DIMOQSumReturnSchedule)
                                {
                                    lstOPSGroupMOQ = lstOPSGroupMOQ.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOPSGroupMOQ.GroupBy(c => c.DITOMasterID))
                                    {
                                        // Tạo pl temp
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.Price).FirstOrDefault().Price;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemMOQ.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemMOQ.ExprPriceFix))
                                        {
                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                plCost.Note = itemMOQ.MOQName;
                                                plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                //plCost.Note1 = string.Join(",", itemMasterGroup.Select(c => c.OrderCode).Distinct().ToList());
                                                plCost.Debit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemMOQ.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQNoGroupDebit;
                                                    plCost.Note = itemMOQ.MOQName;
                                                    plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                    //plCost.Note1 = string.Join(",", itemMasterGroup.Select(c => c.OrderCode).Distinct().ToList());
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Debit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Debit += plCost.Debit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceMOQ_FindOrder_Debit(itemMOQ, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupMOQ, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemMOQ.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);
                                                        itemExpr.UnitPrice = itemOPSGroupMOQ.Price;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);

                                                        if (quantityGroupMOQ == null)
                                                            if (itemOPSGroupMOQ.PriceOfGOPID == iTon)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.TonReturn;
                                                            else if (itemOPSGroupMOQ.PriceOfGOPID == iCBM)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.CBMReturn;
                                                            else if (itemOPSGroupMOQ.PriceOfGOPID == iQuantity)
                                                                quantityGroupMOQ = (decimal)itemOPSGroupMOQ.QuantityReturn;

                                                        if (itemOPSGroupMOQ.PriceOfGOPID == iTon)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.TonTranfer;
                                                        else if (itemOPSGroupMOQ.PriceOfGOPID == iCBM)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.CBMTranfer;
                                                        else if (itemOPSGroupMOQ.PriceOfGOPID == iQuantity)
                                                            itemOPSGroupMOQ.QuantityMOQ = (double)itemOPSGroupMOQ.QuantityTranfer;

                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.DITOReturnDebit;
                                                        plCost.Note = itemMOQ.MOQName;
                                                        plCost.TypeOfPriceDIExCode = itemMOQ.TypeOfPriceDIExCode;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        lstPl.Add(pl);

                                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.GroupOfProductID = itemOPSGroupMOQ.ID;
                                                        plGroup.Quantity = quantityGroupMOQ.HasValue ? (double)quantityGroupMOQ.Value : 0;
                                                        plGroup.UnitPrice = priceGroupMOQ.HasValue ? priceGroupMOQ.Value : 0;
                                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                        plCost.Debit += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                                                        pl.Debit += plCost.Debit;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion
                            }
                        }

                        #region Ghi dữ liệu giá chính và MOQ vào hệ thống
                        foreach (var itemMaster in queryMasterContract)
                        {
                            var lstOPSGroup = queryOPSGroupProductContract.Where(c => c.DITOMasterID == itemMaster.ID).ToList();

                            if (lstOPSGroup.Count > 0)
                            {
                                var master = queryOPSGroupProductContract.FirstOrDefault(c => c.DITOMasterID == itemMaster.ID);

                                FIN_PL pl = new FIN_PL();
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.Code = string.Empty;
                                pl.IsPlanning = false;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.Effdate = DateConfig.Date;
                                pl.DITOMasterID = master.DITOMasterID;
                                pl.VendorID = itemMaster.VendorID;
                                pl.VehicleID = master.VehicleID;
                                pl.ContractID = itemContract.ID;
                                pl.CustomerID = itemContract.CustomerID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                #region Chi phí phát sinh
                                var lstCreditTrouble = ItemInput.ListTrouble.Where(c => c.DITOMasterID.HasValue && c.DITOMasterID == master.DITOMasterID && c.CostOfVendor != 0 && c.TroubleCostStatusID == -(int)SYSVarType.TroubleCostStatusApproved);
                                if (lstCreditTrouble.Count() > 0)
                                {
                                    foreach (var item in lstCreditTrouble)
                                    {
                                        FIN_PL plTrouble = new FIN_PL();
                                        plTrouble.CreatedBy = Account.UserName;
                                        plTrouble.CreatedDate = DateTime.Now;
                                        plTrouble.Code = string.Empty;
                                        plTrouble.IsPlanning = false;
                                        plTrouble.SYSCustomerID = Account.SYSCustomerID;
                                        plTrouble.Effdate = DateConfig.Date;
                                        plTrouble.DITOMasterID = master.DITOMasterID;
                                        plTrouble.VendorID = itemMaster.VendorID;
                                        plTrouble.VehicleID = master.VehicleID;
                                        plTrouble.ContractID = itemContract.ID;
                                        plTrouble.CustomerID = itemContract.CustomerID;
                                        plTrouble.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        FIN_PLDetails plCreditTrouble = new FIN_PLDetails();
                                        plCreditTrouble.CreatedBy = Account.UserName;
                                        plCreditTrouble.CreatedDate = DateTime.Now;
                                        plCreditTrouble.CostID = (int)CATCostType.TroubleDebit;
                                        plCreditTrouble.Debit = item.CostOfVendor;
                                        plCreditTrouble.Credit = 0;
                                        plCreditTrouble.TypeOfPriceDIExCode = item.GroupOfTroubleCode;

                                        if (lstOPSGroup.Count > 0)
                                        {
                                            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                            plGroup.CreatedBy = Account.UserName;
                                            plGroup.CreatedDate = DateTime.Now;
                                            plGroup.GroupOfProductID = lstOPSGroup.OrderByDescending(c => c.TonTranfer).FirstOrDefault().ID;
                                            plGroup.Unit = "";
                                            plGroup.UnitPrice = 0;
                                            plGroup.Quantity = 0;

                                            plCreditTrouble.FIN_PLGroupOfProduct.Add(plGroup);
                                        }

                                        plTrouble.FIN_PLDetails.Add(plCreditTrouble);
                                        plTrouble.Debit += plCreditTrouble.Debit;
                                        lstPl.Add(plTrouble);
                                    }
                                }
                                #endregion

                                #region Tính giá FTL
                                if (itemMaster.TransportModeID == -(int)SYSVarType.TransportModeFTL)
                                {
                                    FIN_PLDetails plDetail = new FIN_PLDetails();
                                    plDetail.CreatedBy = Account.UserName;
                                    plDetail.CreatedDate = DateTime.Now;
                                    plDetail.Note = itemMaster.HasMOQ ? strMOQName : string.Empty;
                                    plDetail.CostID = (int)CATCostType.DITOFreightDebit;
                                    pl.FIN_PLDetails.Add(plDetail);

                                    if (lstOPSGroup.Count > 0)
                                    {
                                        var opsGroup = lstOPSGroup.OrderByDescending(c => c.TonTranfer).FirstOrDefault();
                                        plDetail.Debit = lstOPSGroup.OrderByDescending(c => c.Price).FirstOrDefault().Price;
                                        if (itemContract.TypeOfContractID == -(int)SYSVarType.TypeOfContractSpotRate)
                                            plDetail.Debit = itemMaster.Price;

                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                        plGroup.CreatedBy = Account.UserName;
                                        plGroup.CreatedDate = DateTime.Now;
                                        plGroup.GroupOfProductID = opsGroup.ID;
                                        plGroup.Unit = "";
                                        plGroup.UnitPrice = 0;
                                        plGroup.Quantity = 0;

                                        plDetail.FIN_PLGroupOfProduct.Add(plGroup);

                                        // Cập nhật FINSort
                                        string strSort = opsGroup.ID.ToString();
                                        foreach (var item in lstOPSGroup)
                                        {
                                            if (lstOrderGroupUpDate.Count(c => c.ID == item.ID) == 0)
                                            {
                                                HelperFinance_ORDGroupProduct itemGroup = new HelperFinance_ORDGroupProduct();
                                                itemGroup.ID = item.ID;
                                                if (itemGroup.ID == opsGroup.ID)
                                                    itemGroup.FINSort = strSort;
                                                else
                                                    itemGroup.FINSort = strSort + "A";

                                                lstOrderGroupUpDate.Add(itemGroup);
                                            }
                                        }
                                    }
                                    pl.Debit += plDetail.Debit;
                                }
                                #endregion

                                #region Tính giá LTL
                                else
                                {
                                    foreach (var itemGroup in lstOPSGroup)
                                    {
                                        FIN_PLDetails plDetail = new FIN_PLDetails();
                                        plDetail.CreatedBy = Account.UserName;
                                        plDetail.CreatedDate = DateTime.Now;
                                        plDetail.Note = itemMaster.HasMOQ ? strMOQName : string.Empty;
                                        plDetail.CostID = (int)CATCostType.DITOFreightDebit;
                                        pl.FIN_PLDetails.Add(plDetail);

                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                        plGroup.CreatedBy = Account.UserName;
                                        plGroup.CreatedDate = DateTime.Now;
                                        plGroup.GroupOfProductID = itemGroup.ID;
                                        plGroup.Unit = itemGroup.PriceOfGOPName;
                                        plGroup.UnitPrice = itemGroup.Price;
                                        if (itemGroup.PriceOfGOPID == iTon)
                                            plGroup.Quantity = itemGroup.HasMOQ ? itemGroup.QuantityMOQ : itemGroup.TonTranfer;
                                        else if (itemGroup.PriceOfGOPID == iCBM)
                                            plGroup.Quantity = itemGroup.HasMOQ ? itemGroup.QuantityMOQ : itemGroup.CBMTranfer;
                                        else if (itemGroup.PriceOfGOPID == iQuantity)
                                            plGroup.Quantity = itemGroup.HasMOQ ? itemGroup.QuantityMOQ : itemGroup.QuantityTranfer;

                                        plDetail.Debit += (decimal)plGroup.Quantity * plGroup.UnitPrice;
                                        plDetail.FIN_PLGroupOfProduct.Add(plGroup);
                                        pl.Debit += plDetail.Debit;
                                    }
                                }
                                #endregion

                                lstPl.Add(pl);
                            }
                        }
                        #endregion

                        #endregion

                        #region Phụ thu
                        var lstEx = ItemInput.ListEx.Where(c => c.ContractID == itemContract.ID && c.EffectDate <= DateConfig).ToList();
                        var ExEffateDate = lstEx.Select(c => new { c.EffectDate }).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                        if (ExEffateDate != null)
                            lstEx = lstEx.Where(c => c.EffectDate == ExEffateDate.EffectDate).ToList();
                        string strExName = string.Empty;
                        //Chạy từng phụ thu
                        foreach (var itemEx in lstEx)
                        {
                            System.Diagnostics.Debug.WriteLine("Chi Phụ phí: " + itemEx.Note);

                            var lstMasterEx = new List<HelperFinance_TOMaster>();
                            var lstOPSGroupEx = new List<HelperFinance_OPSGroupProduct>();

                            var queryMasterEx = queryMasterContract.ToList();
                            var queryOPSGroupEx = queryOPSGroupProductContract.ToList();

                            strExName = itemEx.Note;
                            //Danh sách các điều kiện lọc 
                            var lstExParentRouting = itemEx.ListParentRouting;
                            var lstExRouting = itemEx.ListRouting;
                            var lstExGroupLocation = itemEx.ListGroupOfLocation;
                            var lstExPartner = itemEx.ListPartnerID;
                            var lstExProvince = itemEx.ListProvinceID;
                            var lstExLocationFrom = itemEx.ListLocation.Where(c => c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                                .Select(c => c.LocationID).ToList();
                            var lstExLocationTo = itemEx.ListLocation.Where(c => (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                                .Select(c => c.LocationID).ToList();
                            var lstExGroupProduct = itemEx.ListGroupProduct.Select(c => c.GroupOfProductID).Distinct().ToList();

                            if (lstExParentRouting.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExParentRouting.Contains(c.ParentRoutingID)).ToList();
                            if (lstExRouting.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExRouting.Contains(c.CATRoutingID)).ToList();
                            if (lstExGroupLocation.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExGroupLocation.Contains(c.GroupOfLocationID)).ToList();
                            if (lstExLocationFrom.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExLocationFrom.Contains(c.LocationFromID)).ToList();
                            if (lstExLocationTo.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExLocationTo.Contains(c.LocationToID)).ToList();
                            if (lstExGroupProduct.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExGroupProduct.Contains(c.GroupOfProductID)).ToList();
                            if (lstExPartner.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExPartner.Contains(c.PartnerID)).ToList();
                            if (lstExProvince.Count > 0)
                                queryOPSGroupEx = queryOPSGroupEx.Where(c => lstExProvince.Contains(c.LocationToProvinceID)).ToList();

                            var exMasterID = queryMasterEx.Select(c => c.ID).ToArray();
                            var exGroupID = queryOPSGroupEx.Select(c => c.DITOMasterID).Distinct().ToArray();
                            var lstMasterCheckID = exGroupID.Intersect(exMasterID).ToList();
                            var lstMasterCheck = queryMasterEx.Where(c => lstMasterCheckID.Contains(c.ID)).ToList();
                            var lstMasterGroupCheck = queryOPSGroupEx.Where(c => lstMasterCheckID.Contains(c.DITOMasterID)).ToList();

                            foreach (var itemMasterCheck in lstMasterCheck)
                            {
                                itemMasterCheck.GetPointCurrent = lstMasterGroupCheck.Where(c => c.DITOMasterID == itemMasterCheck.ID).Select(c => c.LocationFromID).Distinct().Count();
                                itemMasterCheck.DropPointCurrent = lstMasterGroupCheck.Where(c => c.DITOMasterID == itemMasterCheck.ID).Select(c => c.LocationToID).Distinct().Count();
                            }

                            if (lstMasterCheck.Count > 0 && lstMasterGroupCheck.Count > 0)
                            {
                                #region Theo đơn hàng or chuyến trong ngày
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderInDay || itemEx.DIExSumID == -(int)SYSVarType.DIExScheduleInDay)
                                {
                                    bool flag = true;
                                    //Thực hiện công thức
                                    DTOPriceDIExExpr itemExpr = Expr_Generate(lstMasterGroupCheck.ToList());
                                    itemExpr.Credit = totalPrice;

                                    try
                                    {
                                        flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
                                    }
                                    catch { flag = false; }
                                    if (flag == true)
                                    {
                                        lstMasterEx = lstMasterCheck;
                                        lstOPSGroupEx = lstMasterGroupCheck;
                                    }
                                }
                                #endregion

                                #region Theo chuyến
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSchedule)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        var itemMasterCheck = lstMasterCheck.FirstOrDefault(c => c.ID == itemGroup.Key);
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.DropPointCurrent = itemMasterCheck.DropPointCurrent;
                                        itemExpr.GetPointCurrent = itemMasterCheck.GetPointCurrent;
                                        itemExpr.SortConfig = itemMasterCheck.SortConfig;

                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstMasterEx.Add(itemMasterCheck);
                                            lstOPSGroupEx.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Theo đơn hàng
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrder)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.SortConfig = itemGroup.FirstOrDefault().SortConfigOrder;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            foreach (var masterID in itemGroup.Select(c => c.DITOMasterID).Distinct().ToList())
                                            {
                                                if (lstMasterEx.Count(c => c.ID == masterID) == 0)
                                                {
                                                    lstMasterEx.Add(lstMasterCheck.FirstOrDefault(c => c.ID == masterID));
                                                }
                                            }
                                            lstOPSGroupEx.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Theo điểm
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderLocation)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.LocationToID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            foreach (var masterID in itemGroup.Select(c => c.DITOMasterID).Distinct().ToList())
                                            {
                                                if (lstMasterEx.Count(c => c.ID == masterID) == 0)
                                                {
                                                    lstMasterEx.Add(lstMasterCheck.FirstOrDefault(c => c.ID == masterID));
                                                }
                                            }
                                            lstOPSGroupEx.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Theo cung đường
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.OrderRoutingID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            foreach (var masterID in itemGroup.Select(c => c.DITOMasterID).Distinct().ToList())
                                            {
                                                if (lstMasterEx.Count(c => c.ID == masterID) == 0)
                                                {
                                                    lstMasterEx.Add(lstMasterCheck.FirstOrDefault(c => c.ID == masterID));
                                                }
                                            }
                                            lstOPSGroupEx.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Theo thu hộ
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumCollect)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.Where(c => c.HasCashCollect == true))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_GenerateItem(itemGroup);
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemEx.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstOPSGroupEx.Add(itemGroup);
                                            if (lstMasterEx.Count(c => c.ID == itemGroup.DITOMasterID) == 0)
                                                lstMasterEx.Add(lstMasterCheck.FirstOrDefault(c => c.ID == itemGroup.DITOMasterID));
                                        }
                                    }
                                }
                                #endregion
                            }

                            if (lstMasterEx.Count > 0 && lstOPSGroupEx.Count > 0)
                            {
                                #region Theo đơn hàng or chuyến trong ngày
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderInDay || itemEx.DIExSumID == -(int)SYSVarType.DIExScheduleInDay)
                                {
                                    //Thực hiện công thức
                                    DTOPriceDIExExpr itemExpr = Expr_Generate(lstOPSGroupEx.ToList());
                                    itemExpr.Credit = totalPrice;
                                    decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                    if (!string.IsNullOrEmpty(itemEx.ExprPriceFix))
                                        priceFix = Expression_GetValue(Expression_GetPackage(itemEx.ExprPriceFix), itemExpr);
                                    if (!string.IsNullOrEmpty(itemEx.ExprPrice))
                                        priceMOQ = Expression_GetValue(Expression_GetPackage(itemEx.ExprPrice), itemExpr);
                                    if (!string.IsNullOrEmpty(itemEx.ExprTon))
                                        quantityMOQ = Expression_GetValue(Expression_GetPackage(itemEx.ExprTon), itemExpr);
                                    if (!string.IsNullOrEmpty(itemEx.ExprCBM))
                                        quantityMOQ = Expression_GetValue(Expression_GetPackage(itemEx.ExprCBM), itemExpr);
                                    if (!string.IsNullOrEmpty(itemEx.ExprQuan))
                                        quantityMOQ = Expression_GetValue(Expression_GetPackage(itemEx.ExprQuan), itemExpr);

                                    FIN_PL pl = new FIN_PL();
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig.Date;
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.VendorID = itemContract.VendorID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.DITOExNoGroupDebit;
                                    plCost.Note = itemEx.Note;
                                    plCost.TypeOfPriceDIExCode = itemEx.TypeOfPriceDIExCode;
                                    //plCost.Note1 = string.Join(",", lstMasterGroupCheck.Select(c => c.OrderCode).Distinct().ToList());
                                    if (priceFix.HasValue)
                                        plCost.Debit = priceFix.Value;
                                    if (priceMOQ.HasValue)
                                        plCost.UnitPrice = priceMOQ.Value;

                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                    plCost.Debit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                    pl.FIN_PLDetails.Add(plCost);
                                    pl.Debit += plCost.Debit;

                                    var lstOPSGroupID = lstOPSGroupEx.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                    DIPriceEx_FindOrder_Debit(itemEx, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx, lstOrderGroupUpDate);
                                    lstPl.Add(pl);
                                }
                                #endregion

                                #region Theo chuyến
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSchedule)
                                {
                                    foreach (var itemGroup in lstOPSGroupEx.GroupBy(c => c.DITOMasterID))
                                    {
                                        var itemMasterEx = lstMasterEx.FirstOrDefault(c => c.ID == itemGroup.Key);

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.DropPointCurrent = itemMasterEx.DropPointCurrent;
                                        itemExpr.GetPointCurrent = itemMasterEx.GetPointCurrent;
                                        itemExpr.SortConfig = itemMasterEx.SortConfig;

                                        var lstCollect = itemGroup.Where(c => c.HasCashCollect == true);
                                        if (lstCollect.Count() > 0)
                                        {
                                            itemExpr.TonCollect = lstCollect.Sum(c => c.TonTranfer);
                                            itemExpr.CBMCollect = lstCollect.Sum(c => c.CBMTranfer);
                                            itemExpr.QuantityCollect = lstCollect.Sum(c => c.QuantityTranfer);
                                        }
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                        // Tính theo từng group trong chuyến
                                        var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceExGroupProduct.Count() > 0)
                                        {
                                            // Tính phụ thu theo từng loại hàng
                                            foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                            {
                                                foreach (var itemOPSPriceExGroupProduct in itemGroup.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSPriceExGroupProduct);
                                                    itemExpr.DropPointCurrent = itemMasterEx.DropPointCurrent;
                                                    itemExpr.GetPointCurrent = itemMasterEx.GetPointCurrent;
                                                    itemExpr.ID = itemOPSPriceExGroupProduct.ID;
                                                    if (itemOPSPriceExGroupProduct.HasCashCollect)
                                                    {
                                                        itemExpr.TonCollect = itemOPSPriceExGroupProduct.TonTranfer;
                                                        itemExpr.CBMCollect = itemOPSPriceExGroupProduct.CBMTranfer;
                                                        itemExpr.QuantityCollect = itemOPSPriceExGroupProduct.QuantityTranfer;
                                                    }
                                                    DIPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
                                                }
                                            }
                                        }

                                        if (pl.FIN_PLDetails.Count > 0)
                                        {
                                            pl.Debit = pl.FIN_PLDetails.Sum(c => c.Debit);
                                            lstPl.Add(pl);
                                        }
                                    }
                                }
                                #endregion

                                #region Theo đơn hàng
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrder)
                                {
                                    foreach (var itemGroup in lstOPSGroupEx.GroupBy(c => c.OrderID))
                                    {
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.SortConfig = itemGroup.FirstOrDefault().SortConfigOrder;
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                        // Tính theo từng group trong đơn hàng
                                        var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceExGroupProduct.Count() > 0)
                                        {
                                            // Tính phụ thu theo từng loại hàng
                                            foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                            {
                                                foreach (var itemOPSPriceExGroupProduct in itemGroup.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSPriceExGroupProduct);
                                                    DIPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
                                                }
                                            }
                                        }

                                        if (pl.FIN_PLDetails.Count > 0)
                                        {
                                            pl.Debit = pl.FIN_PLDetails.Sum(c => c.Debit);
                                            lstPl.Add(pl);
                                        }
                                    }
                                }
                                #endregion

                                #region Theo điểm
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderLocation)
                                {
                                    foreach (var itemGroup in lstOPSGroupEx.GroupBy(c => c.LocationToID))
                                    {
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                        // Tính theo từng group trong đơn hàng
                                        var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceExGroupProduct.Count() > 0)
                                        {
                                            // Tính phụ thu theo từng loại hàng
                                            foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                            {
                                                foreach (var itemOPSPriceExGroupProduct in itemGroup.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSPriceExGroupProduct);
                                                    DIPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
                                                }
                                            }
                                        }

                                        if (pl.FIN_PLDetails.Count > 0)
                                        {
                                            pl.Debit = pl.FIN_PLDetails.Sum(c => c.Debit);
                                            lstPl.Add(pl);
                                        }
                                    }
                                }
                                #endregion

                                #region Theo cung đường
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstOPSGroupEx.GroupBy(c => c.OrderRoutingID))
                                    {
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                        DIPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                        // Tính theo từng group trong đơn hàng
                                        var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceExGroupProduct.Count() > 0)
                                        {
                                            // Tính phụ thu theo từng loại hàng
                                            foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                            {
                                                foreach (var itemOPSPriceExGroupProduct in itemGroup.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSPriceExGroupProduct);

                                                    DIPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
                                                }
                                            }
                                        }

                                        if (pl.FIN_PLDetails.Count > 0)
                                        {
                                            pl.Debit = pl.FIN_PLDetails.Sum(c => c.Debit);
                                            lstPl.Add(pl);
                                        }
                                    }
                                }
                                #endregion

                                #region Theo thu hộ
                                if (itemEx.DIExSumID == -(int)SYSVarType.DIExSumCollect)
                                {
                                    foreach (var itemGroup in lstOPSGroupEx)
                                    {
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_GenerateItem(itemGroup);
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

                                        var lstOPSGroupID = lstOPSGroupEx.Where(c => c.ID == itemGroup.ID).Select(c => c.ID).Distinct().ToList();
                                        DIPriceEx_CalculatePrice(false, itemExpr, itemEx, pl, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupEx);

                                        // Tính theo từng group 
                                        var lstPriceExGroupProduct = itemEx.ListGroupProduct.Where(c => !string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity));
                                        if (lstPriceExGroupProduct.Count() > 0)
                                        {
                                            // Tính phụ thu theo từng loại hàng
                                            foreach (var itemPriceExGroupProduct in lstPriceExGroupProduct)
                                            {
                                                foreach (var itemOPSPriceExGroupProduct in lstOPSGroupEx.Where(c => c.GroupOfProductID == itemPriceExGroupProduct.GroupOfProductID && c.ID == itemGroup.ID))
                                                {
                                                    DIPriceEx_CalculatePriceGOP(false, itemExpr, itemEx, itemPriceExGroupProduct, pl, lstPlTemp, lstPLTempContract, itemOPSPriceExGroupProduct.ID, itemContract.ID, lstOPSGroupEx);
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

                        #region Bốc xếp lên
                        var lstLoad = ItemInput.ListMOQLoad.Where(c => c.ContractID == itemContract.ID && c.IsLoading && c.EffectDate <= DateConfig).ToList();
                        var LoadEffateDate = lstLoad.Select(c => new { c.EffectDate }).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                        if (LoadEffateDate != null)
                            lstLoad = lstLoad.Where(c => c.EffectDate == LoadEffateDate.EffectDate).ToList();

                        #region Chạy từng Bốc xếp lên
                        foreach (var itemLoad in lstLoad)
                        {
                            System.Diagnostics.Debug.WriteLine("Chi Bốc xếp lên: " + itemLoad.MOQName);

                            var lstMasterLoad = new List<HelperFinance_TOMaster>();
                            var lstOPSGroupLoad = new List<HelperFinance_OPSGroupProduct>();

                            var queryOPSGroupLoad = queryOPSGroupProductContractLoad.ToList();
                            var queryMasterID = queryOPSGroupLoad.Select(c => c.DITOMasterID).Distinct().ToList();
                            var queryMasterLoad = ItemInput.ListTOMaster.Where(c => queryMasterID.Contains(c.ID)).ToList();

                            //Danh sách các điều kiện lọc 
                            var lstLoadParentRouting = itemLoad.ListParentRouting;
                            var lstLoadRouting = itemLoad.ListRouting;
                            var lstLoadGroupLocation = itemLoad.ListGroupOfLocation;
                            var lstLoadPartner = itemLoad.ListPartnerID;
                            var lstLoadProvince = itemLoad.ListProvinceID;
                            var lstLoadLocationFrom = itemLoad.ListLocation.Where(c => c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                               .Select(c => c.LocationID).ToList();
                            var lstLoadLocationTo = itemLoad.ListLocation.Where(c => (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                                .Select(c => c.LocationID).ToList();
                            var lstLoadGroupProduct = itemLoad.ListGroupProduct.Select(c => c.GroupOfProductID).Distinct().ToList();

                            if (lstLoadParentRouting.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadParentRouting.Contains(c.ParentRoutingID)).ToList();
                            if (lstLoadRouting.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadRouting.Contains(c.CATRoutingID)).ToList();
                            if (lstLoadGroupLocation.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadGroupLocation.Contains(c.GroupOfLocationID)).ToList();
                            if (lstLoadLocationFrom.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadLocationFrom.Contains(c.LocationFromID)).ToList();
                            if (lstLoadLocationTo.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadLocationTo.Contains(c.LocationToID)).ToList();
                            if (lstLoadGroupProduct.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => c.GroupOfProductLoadID.HasValue && lstLoadGroupProduct.Contains(c.GroupOfProductLoadID.Value)).ToList();
                            if (lstLoadPartner.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadPartner.Contains(c.PartnerID)).ToList();
                            if (lstLoadProvince.Count > 0)
                                queryOPSGroupLoad = queryOPSGroupLoad.Where(c => lstLoadProvince.Contains(c.LocationToProvinceID)).ToList();

                            var exMasterID = queryMasterLoad.Select(c => c.ID).ToArray();
                            var exGroupID = queryOPSGroupLoad.Select(c => c.DITOMasterID).Distinct().ToArray();
                            var lstMasterCheckID = exGroupID.Intersect(exMasterID).ToList();
                            var lstMasterCheck = queryMasterLoad.Where(c => lstMasterCheckID.Contains(c.ID)).ToList();
                            var lstMasterGroupCheck = queryOPSGroupLoad.Where(c => lstMasterCheckID.Contains(c.DITOMasterID)).ToList();


                            bool isLoadSumInDay = false;
                            if (lstMasterGroupCheck.Count > 0 && (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderInDay || itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumScheduleInDay))
                            {
                                bool flag = true;
                                //Thực hiện công thức
                                DTOPriceDIExExpr itemExpr = Expr_Generate(lstMasterGroupCheck.ToList());
                                itemExpr.Credit = lstMasterGroupCheck.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                isLoadSumInDay = flag = Expression_CheckBool(itemExpr, itemLoad.ExprInput);
                                if (flag == true)
                                {
                                    lstMasterLoad = lstMasterCheck;
                                    lstOPSGroupLoad = lstMasterGroupCheck;
                                }
                            }
                            else
                            {
                                #region Tính theo đơn hàng
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrder)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        flag = Expression_CheckBool(itemExpr, itemLoad.ExprInput);
                                        if (flag == true)
                                        {
                                            var lstMasterID = itemGroup.Select(c => c.DITOMasterID).Distinct().ToList();
                                            foreach (var masterID in lstMasterID)
                                            {
                                                if (lstMasterLoad.Count(c => c.ID == masterID) > 0)
                                                {
                                                    lstMasterLoad.Add(lstMasterCheck.FirstOrDefault(c => c.ID == masterID));
                                                }
                                            }
                                            lstOPSGroupLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumSchedule)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        flag = Expression_CheckBool(itemExpr, itemLoad.ExprInput);
                                        if (flag == true)
                                        {
                                            lstMasterLoad.Add(lstMasterCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupLoad.AddRange(itemGroup);
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến hàng trả về
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumReturnSchedule)
                                {
                                    lstMasterGroupCheck = lstMasterGroupCheck.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstMasterLoad.Add(lstMasterCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo cung đường
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.CATRoutingID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempOrderID = itemGroup.Select(c => c.DITOMasterID).Distinct().ToList();
                                            foreach (var tempOrderID in lstTempOrderID)
                                            {
                                                lstMasterLoad.Add(lstMasterCheck.FirstOrDefault(c => c.ID == tempOrderID));
                                            }
                                            lstOPSGroupLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion
                            }

                            //Thực hiện lấy output MOQ
                            foreach (var itemOrderLoad in lstMasterLoad)
                            {
                                itemOrderLoad.HasMOQLoad = true;
                            }
                            foreach (var itemOrderGroupLoad in lstOPSGroupLoad)
                            {
                                itemOrderGroupLoad.HasMOQLoad = true;
                            }

                            //Ghi dữ liệu phụ thu vào hệ thống
                            if (isLoadSumInDay && !string.IsNullOrEmpty(itemLoad.ExprPriceFix) && lstOPSGroupLoad.Count > 0)
                            {
                                //Thực hiện công thức
                                DTOPriceDIExExpr itemExpr = Expr_Generate(lstOPSGroupLoad.ToList());
                                itemExpr.Credit = lstOPSGroupLoad.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);

                                decimal? fixPrice = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPriceFix), itemExpr);


                                // Set lại đơn giá = 0;
                                foreach (var item in lstMasterLoad)
                                {
                                    item.PriceLoad = 0;
                                }
                                foreach (var item in lstOPSGroupLoad)
                                {
                                    item.UnitPriceLoad = 0;
                                    item.PriceLoad = 0;
                                }


                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = itemContract.VendorID;
                                pl.Debit = fixPrice.HasValue ? fixPrice.Value : 0;
                                pl.ContractID = itemContract.ID;
                                pl.CustomerID = itemContract.CustomerID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupDebit;
                                plCost.Note = itemLoad.MOQName;
                                plCost.Debit = fixPrice.HasValue ? fixPrice.Value : 0;
                                pl.FIN_PLDetails.Add(plCost);

                                var lstOPSGroupID = lstOPSGroupLoad.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                DIPriceLoad_FindOrder_Debit(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                lstPl.Add(pl);
                            }
                            else
                            {
                                #region Theo đơn hàng
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrder)
                                {
                                    foreach (var itemGroup in lstOPSGroupLoad.GroupBy(c => c.OrderID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.VendorID = itemContract.VendorID;
                                        pl.OrderID = itemGroup.Key;
                                        pl.ContractID = itemContract.ID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupDebit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Debit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder_Debit(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupDebit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Debit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Debit += plCost.Debit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder_Debit(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductLoadID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;
                                        }
                                    }
                                }
                                #endregion

                                #region Theo chuyến
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumSchedule)
                                {
                                    foreach (var itemGroup in lstOPSGroupLoad.GroupBy(c => c.DITOMasterID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.DITOMasterID = itemGroup.Key;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupDebit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupDebit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceLoad;
                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến trả về
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumReturnSchedule)
                                {
                                    lstOPSGroupLoad = lstOPSGroupLoad.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOPSGroupLoad.GroupBy(c => c.DITOMasterID))
                                    {
                                        // Tạo pl temp
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                        pl.VendorID = itemContract.VendorID;

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                        {
                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupDebit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Credit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder_Debit(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupDebit;
                                                    plCost.Note = itemLoad.MOQName;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Credit += plCost.Credit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceLoad_FindOrder_Debit(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductLoadID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);
                                                        itemExpr.UnitPrice = itemOPSGroupMOQ.UnitPriceLoad;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);


                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.DITOLoadReturnDebit;
                                                        plCost.Note = itemLoad.MOQName;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        lstPl.Add(pl);


                                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.GroupOfProductID = itemOPSGroupMOQ.ID;
                                                        plGroup.Quantity = quantityGroupMOQ.HasValue ? (double)quantityGroupMOQ.Value : 0;
                                                        plGroup.UnitPrice = priceGroupMOQ.HasValue ? priceGroupMOQ.Value : 0;
                                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                        plCost.Credit += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                                                        pl.Credit += plCost.Credit;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo cung đường
                                if (itemLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstOPSGroupLoad.GroupBy(c => c.CATRoutingID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityLoad * c.UnitPriceLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupDebit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQLoadNoGroupDebit;
                                                plCost.Note = itemLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceLoad;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceLoad = 0;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }

                        #endregion

                        #region Ghi dữ liệu chính + MOQ bốc xếp
                        var lstMasterLoading = queryMasterContractLoad.Where(c => c.IsLoading);
                        foreach (var itemMasterLoading in lstMasterLoading)
                        {
                            var master = ItemInput.ListTOMaster.FirstOrDefault(c => c.ID == itemMasterLoading.ID);
                            if (master != null)
                            {
                                var lstOPSGroupLoading = queryOPSGroupProductContractLoad.Where(c => c.DITOMasterID == itemMasterLoading.ID && c.IsLoading);
                                foreach (var opsGroup in lstOPSGroupLoading)
                                {
                                    var pl = new FIN_PL();
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.DITOMasterID = master.ID;
                                    pl.VehicleID = master.VehicleID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.VendorID = itemContract.VendorID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                    lstPl.Add(pl);

                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.DITOLoadDebit;
                                    pl.FIN_PLDetails.Add(plCost);

                                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                    plGroup.CreatedBy = Account.UserName;
                                    plGroup.CreatedDate = DateTime.Now;
                                    plGroup.Unit = opsGroup.PriceOfGOPLoadName;
                                    plGroup.UnitPrice = opsGroup.UnitPriceLoad;
                                    plGroup.Quantity = opsGroup.QuantityLoad;
                                    plGroup.GroupOfProductID = opsGroup.ID;
                                    plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                    plCost.Debit += plGroup.UnitPrice * (decimal)plGroup.Quantity;

                                    pl.Debit += plCost.Debit;
                                }
                            }
                        }
                        #endregion
                        #endregion

                        #region Bốc xếp xuống
                        var lstUnLoad = ItemInput.ListMOQLoad.Where(c => c.ContractID == itemContract.ID && !c.IsLoading && c.EffectDate <= DateConfig).ToList();
                        var UnLoadEffateDate = lstUnLoad.Select(c => new { c.EffectDate }).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                        if (UnLoadEffateDate != null)
                            lstUnLoad = lstUnLoad.Where(c => c.EffectDate == UnLoadEffateDate.EffectDate).ToList();
                        //Chạy từng phụ thu
                        foreach (var itemUnLoad in lstUnLoad)
                        {
                            var lstMasterUnLoad = new List<HelperFinance_TOMaster>();
                            var lstOPSGroupUnLoad = new List<HelperFinance_OPSGroupProduct>();

                            var queryOPSGroupUnLoad = queryOPSGroupProductContractUnLoad.ToList();
                            var queryMasterID = queryOPSGroupUnLoad.Select(c => c.DITOMasterID).Distinct().ToList();
                            var queryMasterUnLoad = ItemInput.ListTOMaster.Where(c => queryMasterID.Contains(c.ID)).ToList();

                            //Danh sách các điều kiện lọc 
                            var lstLoadParentRouting = itemUnLoad.ListParentRouting;
                            var lstLoadRouting = itemUnLoad.ListRouting;
                            var lstLoadGroupLocation = itemUnLoad.ListGroupOfLocation;
                            var lstLoadPartner = itemUnLoad.ListPartnerID;
                            var lstLoadProvince = itemUnLoad.ListProvinceID;
                            var lstLoadLocationFrom = itemUnLoad.ListLocation.Where(c => c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationStock)
                               .Select(c => c.LocationID).ToList();
                            var lstLoadLocationTo = itemUnLoad.ListLocation.Where(c => (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery))
                                .Select(c => c.LocationID).ToList();
                            var lstLoadGroupProduct = itemUnLoad.ListGroupProduct.Select(c => c.GroupOfProductID).Distinct().ToList();

                            if (lstLoadParentRouting.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadParentRouting.Contains(c.ParentRoutingID)).ToList();
                            if (lstLoadRouting.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadRouting.Contains(c.CATRoutingID)).ToList();
                            if (lstLoadGroupLocation.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadGroupLocation.Contains(c.GroupOfLocationID)).ToList();
                            if (lstLoadLocationFrom.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadLocationFrom.Contains(c.LocationFromID)).ToList();
                            if (lstLoadLocationTo.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadLocationTo.Contains(c.LocationToID)).ToList();
                            if (lstLoadGroupProduct.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => c.GroupOfProductUnLoadID.HasValue && lstLoadGroupProduct.Contains(c.GroupOfProductUnLoadID.Value)).ToList();
                            if (lstLoadPartner.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadPartner.Contains(c.PartnerID)).ToList();
                            if (lstLoadProvince.Count > 0)
                                queryOPSGroupUnLoad = queryOPSGroupUnLoad.Where(c => lstLoadProvince.Contains(c.LocationToProvinceID)).ToList();

                            var exMasterID = queryMasterUnLoad.Select(c => c.ID).ToArray();
                            var exGroupID = queryOPSGroupUnLoad.Select(c => c.DITOMasterID).Distinct().ToArray();
                            var lstMasterCheckID = exGroupID.Intersect(exMasterID).ToList();
                            var lstMasterCheck = queryMasterUnLoad.Where(c => lstMasterCheckID.Contains(c.ID)).ToList();
                            var lstMasterGroupCheck = queryOPSGroupUnLoad.Where(c => lstMasterCheckID.Contains(c.DITOMasterID)).ToList();

                            bool isLoadSumInDay = false;
                            if (lstMasterGroupCheck.Count > 0 && (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderInDay || itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumScheduleInDay))
                            {
                                bool flag = true;
                                //Thực hiện công thức
                                DTOPriceDIExExpr itemExpr = Expr_Generate(lstMasterGroupCheck.ToList());
                                itemExpr.Credit = lstMasterGroupCheck.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                isLoadSumInDay = flag = Expression_CheckBool(itemExpr, itemUnLoad.ExprInput);
                                if (flag == true)
                                {
                                    lstMasterUnLoad = lstMasterCheck;
                                    lstOPSGroupUnLoad = lstMasterGroupCheck;
                                }
                            }
                            else
                            {
                                #region Tính theo đơn hàng
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrder)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.OrderID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        flag = Expression_CheckBool(itemExpr, itemUnLoad.ExprInput);
                                        if (flag == true)
                                        {
                                            var lstMasterID = itemGroup.Select(c => c.DITOMasterID).Distinct().ToList();
                                            foreach (var masterID in lstMasterID)
                                            {
                                                if (lstMasterUnLoad.Count(c => c.ID == masterID) > 0)
                                                {
                                                    lstMasterUnLoad.Add(lstMasterCheck.FirstOrDefault(c => c.ID == masterID));
                                                }
                                            }
                                            lstOPSGroupUnLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumSchedule)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemUnLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstMasterUnLoad.Add(lstMasterCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupUnLoad.AddRange(itemGroup);
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo chuyến hàng trả về
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumReturnSchedule)
                                {
                                    lstMasterGroupCheck = lstMasterGroupCheck.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.DITOMasterID))
                                    {
                                        bool flag = false;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemUnLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            lstMasterUnLoad.Add(lstMasterCheck.FirstOrDefault(c => c.ID == itemGroup.Key));
                                            lstOPSGroupUnLoad.AddRange(itemGroup);
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo cung đường
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstMasterGroupCheck.GroupBy(c => c.CATRoutingID))
                                    {
                                        bool flag = true;
                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        try
                                        {
                                            flag = Expression_CheckBool(itemExpr, itemUnLoad.ExprInput);
                                        }
                                        catch { flag = false; }
                                        if (flag == true)
                                        {
                                            var lstTempOrderID = itemGroup.Select(c => c.DITOMasterID).Distinct().ToList();
                                            foreach (var tempOrderID in lstTempOrderID)
                                            {
                                                lstMasterUnLoad.Add(lstMasterCheck.FirstOrDefault(c => c.ID == tempOrderID));
                                            }
                                            lstOPSGroupUnLoad.AddRange(itemGroup.ToArray());
                                        }
                                    }
                                }
                                #endregion
                            }

                            //Thực hiện lấy output MOQ
                            foreach (var itemOrderLoad in lstMasterUnLoad)
                            {
                                itemOrderLoad.HasMOQUnLoad = true;
                            }
                            foreach (var itemOrderGroupLoad in lstOPSGroupUnLoad)
                            {
                                itemOrderGroupLoad.HasMOQUnLoad = true;
                            }

                            //Ghi dữ liệu phụ thu vào hệ thống
                            if (isLoadSumInDay && !string.IsNullOrEmpty(itemUnLoad.ExprPriceFix) && lstOPSGroupUnLoad.Count > 0)
                            {
                                //Thực hiện công thức
                                DTOPriceDIExExpr itemExpr = Expr_Generate(lstOPSGroupUnLoad.ToList());
                                itemExpr.Credit = lstOPSGroupUnLoad.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);

                                decimal? fixPrice = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPriceFix), itemExpr);

                                // Set lại đơn giá = 0;
                                foreach (var item in lstMasterUnLoad)
                                {
                                    item.PriceUnLoad = 0;
                                }
                                foreach (var item in lstOPSGroupUnLoad)
                                {
                                    item.UnitPriceUnLoad = 0;
                                    item.PriceUnLoad = 0;
                                }

                                FIN_PL pl = new FIN_PL();
                                pl.IsPlanning = false;
                                pl.Effdate = DateConfig.Date;
                                pl.Code = string.Empty;
                                pl.CreatedBy = Account.UserName;
                                pl.CreatedDate = DateTime.Now;
                                pl.SYSCustomerID = Account.SYSCustomerID;
                                pl.VendorID = itemContract.VendorID;
                                pl.Debit = fixPrice.HasValue ? fixPrice.Value : 0;
                                pl.ContractID = itemContract.ID;
                                pl.CustomerID = itemContract.CustomerID;
                                pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                FIN_PLDetails plCost = new FIN_PLDetails();
                                plCost.CreatedBy = Account.UserName;
                                plCost.CreatedDate = DateTime.Now;
                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupDebit;
                                plCost.Note = itemUnLoad.MOQName;
                                plCost.Debit = fixPrice.HasValue ? fixPrice.Value : 0;
                                pl.FIN_PLDetails.Add(plCost);

                                var lstOPSGroupID = lstOPSGroupUnLoad.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                DIPriceLoad_FindOrder_Debit(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                lstPl.Add(pl);
                            }
                            else
                            {
                                #region Theo đơn hàng
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrder)
                                {
                                    foreach (var itemGroup in lstOPSGroupUnLoad.GroupBy(c => c.OrderID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.OrderID = itemGroup.Key;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupDebit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupDebit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemUnLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceUnLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityUnLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;
                                        }
                                    }
                                }
                                #endregion

                                #region Theo chuyến
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumSchedule)
                                {
                                    foreach (var itemGroup in lstOPSGroupUnLoad.GroupBy(c => c.DITOMasterID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.DITOMasterID = itemGroup.Key;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupDebit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupDebit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemUnLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceUnLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityUnLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo đơn hàng trả về
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumReturnOrder)
                                {
                                    lstOPSGroupUnLoad = lstOPSGroupUnLoad.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).ToList();
                                    foreach (var itemGroup in lstOPSGroupUnLoad.GroupBy(c => c.OrderID))
                                    {
                                        // Tạo pl temp
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        //Thực hiện công thức
                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPrice = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceLoad).FirstOrDefault().UnitPriceUnLoad;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                        {
                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupDebit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Credit = priceFix.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }
                                        else
                                        {
                                            if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                            {
                                                if (priceMOQ.HasValue)
                                                {
                                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                                    plCost.CreatedBy = Account.UserName;
                                                    plCost.CreatedDate = DateTime.Now;
                                                    plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupDebit;
                                                    plCost.Note = itemUnLoad.MOQName;
                                                    plCost.UnitPrice = priceMOQ.Value;
                                                    plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                    plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                    pl.FIN_PLDetails.Add(plCost);
                                                    pl.Credit += plCost.Credit;

                                                    var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                    DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                    lstPl.Add(pl);
                                                }
                                            }

                                            var lstPriceMOQGroupProduct = itemUnLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                            if (lstPriceMOQGroupProduct.Count() > 0)
                                            {
                                                foreach (var itemPriceMOQGroupProduct in lstPriceMOQGroupProduct)
                                                {
                                                    foreach (var itemOPSGroupMOQ in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQGroupProduct.GroupOfProductID))
                                                    {
                                                        itemExpr = Expr_GenerateItem(itemOPSGroupMOQ);
                                                        itemExpr.UnitPrice = itemOPSGroupMOQ.UnitPriceUnLoad;

                                                        decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprPrice), itemExpr);
                                                        decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQGroupProduct.ExprQuantity), itemExpr);

                                                        FIN_PLDetails plCost = new FIN_PLDetails();
                                                        plCost.CreatedBy = Account.UserName;
                                                        plCost.CreatedDate = DateTime.Now;
                                                        plCost.CostID = (int)CATCostType.DITOLoadReturnDebit;
                                                        plCost.Note = itemUnLoad.MOQName;
                                                        pl.FIN_PLDetails.Add(plCost);
                                                        lstPl.Add(pl);

                                                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                                        plGroup.CreatedBy = Account.UserName;
                                                        plGroup.CreatedDate = DateTime.Now;
                                                        plGroup.GroupOfProductID = itemOPSGroupMOQ.ID;
                                                        plGroup.Quantity = quantityGroupMOQ.HasValue ? (double)quantityGroupMOQ.Value : 0;
                                                        plGroup.UnitPrice = priceGroupMOQ.HasValue ? priceGroupMOQ.Value : 0;
                                                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                                        plCost.Credit += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                                                        pl.Credit += plCost.Credit;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                #endregion

                                #region Tính theo cung đường
                                if (itemUnLoad.DIMOQLoadSumID == -(int)SYSVarType.DIMOQLoadSumOrderRoute)
                                {
                                    foreach (var itemGroup in lstOPSGroupUnLoad.GroupBy(c => c.CATRoutingID))
                                    {
                                        // pl tạm
                                        FIN_PL pl = new FIN_PL();
                                        pl.IsPlanning = false;
                                        pl.Effdate = DateConfig.Date;
                                        pl.Code = string.Empty;
                                        pl.CreatedBy = Account.UserName;
                                        pl.CreatedDate = DateTime.Now;
                                        pl.SYSCustomerID = Account.SYSCustomerID;
                                        pl.CustomerID = itemContract.CustomerID;
                                        pl.ContractID = itemContract.ID;
                                        pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                                        DTOPriceDIExExpr itemExpr = Expr_Generate(itemGroup.ToList());
                                        itemExpr.Credit = itemGroup.Sum(c => (decimal)c.QuantityUnLoad * c.UnitPriceUnLoad);
                                        itemExpr.UnitPriceMin = itemGroup.OrderBy(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPriceMax = itemGroup.OrderByDescending(c => c.UnitPriceUnLoad).FirstOrDefault().UnitPriceUnLoad;
                                        itemExpr.UnitPrice = itemExpr.UnitPriceMax;

                                        decimal? priceFix = null, priceMOQ = null, quantityMOQ = null;

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                            priceFix = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPriceFix), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                            priceMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprPrice), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprTon))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprTon), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprCBM))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprCBM), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprQuan))
                                            quantityMOQ = Expression_GetValue(Expression_GetPackage(itemUnLoad.ExprQuan), itemExpr);

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPriceFix))
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;

                                            if (priceFix.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupDebit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Credit = priceFix.HasValue ? priceFix.Value : 0;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        if (!string.IsNullOrEmpty(itemUnLoad.ExprPrice))
                                        {
                                            if (priceMOQ.HasValue)
                                            {
                                                FIN_PLDetails plCost = new FIN_PLDetails();
                                                plCost.CreatedBy = Account.UserName;
                                                plCost.CreatedDate = DateTime.Now;
                                                plCost.CostID = (int)CATCostType.DITOMOQUnLoadNoGroupDebit;
                                                plCost.Note = itemUnLoad.MOQName;
                                                plCost.Quantity = quantityMOQ.HasValue ? (double)quantityMOQ.Value : 0;
                                                plCost.UnitPrice = priceMOQ.Value;
                                                plCost.Credit = (decimal)plCost.Quantity.Value * plCost.UnitPrice.Value;
                                                pl.FIN_PLDetails.Add(plCost);
                                                pl.Credit += plCost.Credit;

                                                var lstOPSGroupID = itemGroup.OrderByDescending(c => c.TonTranfer).Select(c => c.ID).Distinct().ToList();
                                                DIPriceLoad_FindOrder(itemUnLoad, pl, plCost, lstPlTemp, lstPLTempContract, lstOPSGroupID, itemContract.ID, lstOPSGroupUnLoad, lstOrderGroupUpDate);
                                                lstPl.Add(pl);
                                            }
                                        }

                                        var lstPriceMOQLoadGroupProduct = itemUnLoad.ListGroupProduct.Where(c => (!string.IsNullOrEmpty(c.ExprPrice) || !string.IsNullOrEmpty(c.ExprQuantity)));
                                        if (lstPriceMOQLoadGroupProduct.Count() > 0)
                                        {
                                            foreach (var itemPriceMOQLoadGroupProduct in lstPriceMOQLoadGroupProduct)
                                            {
                                                foreach (var itemOPSGroupLoad in itemGroup.Where(c => c.GroupOfProductID == itemPriceMOQLoadGroupProduct.GroupOfProductID))
                                                {
                                                    itemExpr = Expr_GenerateItem(itemOPSGroupLoad);
                                                    itemExpr.UnitPrice = itemOPSGroupLoad.UnitPriceUnLoad;

                                                    decimal? priceGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprPrice), itemExpr);
                                                    if (priceGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.UnitPriceUnLoad = priceGroupMOQ.Value;

                                                    decimal? quantityGroupMOQ = Expression_GetValue(Expression_GetPackage(itemPriceMOQLoadGroupProduct.ExprQuantity), itemExpr);
                                                    if (quantityGroupMOQ.HasValue)
                                                        itemOPSGroupLoad.QuantityUnLoad = (double)quantityGroupMOQ.Value;
                                                }
                                            }
                                        }
                                        else
                                        {
                                            // set lại đơn giá = 0
                                            foreach (var tempOPSGroupLoad in itemGroup)
                                                tempOPSGroupLoad.UnitPriceUnLoad = 0;
                                        }
                                    }
                                }
                                #endregion
                            }
                        }

                        // Ghi dữ liệu chính + MOQ bốc xếp
                        var lstMasterUnLoading = queryMasterContractUnLoad.Where(c => c.IsUnLoading);
                        foreach (var itemMasterUnLoading in lstMasterUnLoading)
                        {
                            var master = ItemInput.ListTOMaster.FirstOrDefault(c => c.ID == itemMasterUnLoading.ID);
                            if (master != null)
                            {
                                var lstOPSGroupLoading = queryOPSGroupProductContractUnLoad.Where(c => c.DITOMasterID == itemMasterUnLoading.ID && c.IsUnLoading);
                                foreach (var opsGroup in lstOPSGroupLoading)
                                {
                                    var pl = new FIN_PL();
                                    pl.Code = string.Empty;
                                    pl.CreatedBy = Account.UserName;
                                    pl.CreatedDate = DateTime.Now;
                                    pl.IsPlanning = false;
                                    pl.Effdate = DateConfig;
                                    pl.SYSCustomerID = Account.SYSCustomerID;
                                    pl.DITOMasterID = master.ID;
                                    pl.VehicleID = master.VehicleID;
                                    pl.ContractID = itemContract.ID;
                                    pl.CustomerID = itemContract.CustomerID;
                                    pl.VendorID = itemContract.VendorID;
                                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;
                                    lstPl.Add(pl);

                                    FIN_PLDetails plCost = new FIN_PLDetails();
                                    plCost.CreatedBy = Account.UserName;
                                    plCost.CreatedDate = DateTime.Now;
                                    plCost.CostID = (int)CATCostType.DITOUnLoadDebit;
                                    pl.FIN_PLDetails.Add(plCost);

                                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                    plGroup.CreatedBy = Account.UserName;
                                    plGroup.CreatedDate = DateTime.Now;
                                    plGroup.Unit = opsGroup.PriceOfGOPUnLoadName;
                                    plGroup.UnitPrice = opsGroup.UnitPriceUnLoad;
                                    plGroup.Quantity = opsGroup.QuantityUnLoad;
                                    plGroup.GroupOfProductID = opsGroup.ID;
                                    plCost.FIN_PLGroupOfProduct.Add(plGroup);
                                    plCost.Debit += plGroup.UnitPrice * (decimal)plGroup.Quantity;

                                    pl.Debit += plCost.Debit;
                                }
                            }
                        }
                        #endregion
                    }

                    System.Diagnostics.Debug.WriteLine("Contract end: " + itemContract.ID);
                }
                #endregion

                #region Tính phần bổ sung - Manual Fix
                foreach (var itemManualFix in ItemInput.ListManualFix)
                {
                    FIN_PL pl = new FIN_PL();
                    pl.CreatedBy = Account.UserName;
                    pl.CreatedDate = DateTime.Now;
                    pl.Effdate = DateConfig.Date;
                    pl.Code = string.Empty;
                    pl.SYSCustomerID = Account.SYSCustomerID;
                    pl.CustomerID = itemManualFix.CustomerID;
                    pl.VendorID = itemManualFix.VendorOfVehicleID;
                    pl.VehicleID = itemManualFix.VehicleID;
                    pl.ContractID = itemManualFix.ContractID;
                    pl.OrderID = itemManualFix.OrderID;
                    pl.DITOMasterID = itemManualFix.DITOMasterID;
                    pl.FINPLTypeID = -(int)SYSVarType.FINPLTypePL;

                    lstPl.Add(pl);

                    FIN_PLDetails plCost = new FIN_PLDetails();
                    plCost.CreatedBy = Account.UserName;
                    plCost.CreatedDate = DateTime.Now;
                    plCost.CostID = (int)CATCostType.ManualFixDebit;
                    plCost.Debit = itemManualFix.Debit;
                    plCost.Note = itemManualFix.Note;
                    plCost.TypeOfPriceDIExCode = "ManualFix";
                    pl.Debit = plCost.Debit;
                    pl.FIN_PLDetails.Add(plCost);

                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                    plGroup.CreatedBy = Account.UserName;
                    plGroup.CreatedDate = DateTime.Now;
                    plGroup.GroupOfProductID = itemManualFix.DITOGroupProductID;
                    plGroup.UnitPrice = itemManualFix.UnitPrice;
                    plGroup.Unit = itemManualFix.PriceOfGOPName;
                    if (itemManualFix.PriceOfGOPID == iTon)
                        plGroup.Quantity = itemManualFix.Ton;
                    else if (itemManualFix.PriceOfGOPID == iCBM)
                        plGroup.Quantity = itemManualFix.CBM;
                    else if (itemManualFix.PriceOfGOPID == iQuantity)
                        plGroup.Quantity = itemManualFix.Quantity;

                    plCost.FIN_PLGroupOfProduct.Add(plGroup);
                }
                #endregion

                #region Cập nhật dữ liệu
                foreach (var itemOPSGroup in lstOrderGroupUpDate)
                {
                    var opsGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == itemOPSGroup.ID);
                    if (opsGroup != null)
                        opsGroup.FINSort = itemOPSGroup.FINSort;
                }

                var lstDIMasterID = ItemInput.ListTOMaster.Select(c => c.ID).Distinct().ToList();
                foreach (var itemDIMasterID in lstDIMasterID)
                {
                    var objUpdate = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == itemDIMasterID);
                    if (objUpdate != null)
                        objUpdate.TypeOfPaymentDITOMasterID = -(int)SYSVarType.TypeOfPaymentDITOMasterOpen;
                }
                #endregion

                #region Ghi dữ liệu vào model
                foreach (var pl in lstPl)
                {
                    model.FIN_PL.Add(pl);
                    FIN_PL plFLM = new FIN_PL();
                    CopyFinPL_FLM(pl, plFLM, Account);
                    if (plFLM.FIN_PLDetails.Count > 0 && plFLM.FIN_PLDetails.Count(c => c.CostID == 0) == 0)
                        model.FIN_PL.Add(plFLM);
                }

                foreach (var plTemp in lstPlTemp)
                    model.FIN_Temp.Add(plTemp);
                #endregion
            }
        }

        public static void Truck_UpdateGroup(DataEntities model, AccountItem Account, List<int> lstOPSGroupID)
        {
            foreach (var item in lstOPSGroupID)
            {
                var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusComplete && c.DITOMasterID > 0);
                if (obj != null)
                {
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;

                    var lstContractVENLoad = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && ((c.CompanyID > 0 && c.CUS_Company.CustomerRelateID == obj.ORD_GroupProduct.ORD_Order.CustomerID) || c.CompanyID == null) && (c.CustomerID == obj.VendorLoadID || c.CustomerID == obj.VendorUnLoadID) && c.EffectDate <= obj.OPS_DITOMaster.DateConfig && (c.ExpiredDate == null || c.ExpiredDate > obj.OPS_DITOMaster.DateConfig) &&
                       (c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFTL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLTL)).Select(c => new HelperFinance_Contract
                       {
                           ID = c.ID,
                           TransportModeID = c.TransportModeID,
                           TypeOfContractDateID = c.TypeOfContractDateID,
                           EffectDate = c.EffectDate,
                           CustomerID = c.CustomerID
                       }).OrderByDescending(c => c.EffectDate).ToList();

                    if (obj.VendorLoadID == null)
                        obj.VendorLoadContractID = null;
                    else
                    {
                        var contract = lstContractVENLoad.FirstOrDefault(c => c.CustomerID == obj.VendorLoadID);
                        if (contract != null)
                            obj.VendorLoadContractID = contract.ID;
                        // cập nhật cho CUS_Location của vendor
                        var cusLocation = model.CUS_Location.FirstOrDefault(c => c.CustomerID == obj.VendorLoadID && c.LocationID == obj.ORD_GroupProduct.CUS_Location.LocationID);
                        if (cusLocation == null)
                        {
                            cusLocation = new CUS_Location();
                            cusLocation.CreatedBy = Account.UserName;
                            cusLocation.CreatedDate = DateTime.Now;
                            cusLocation.LocationID = obj.ORD_GroupProduct.CUS_Location.LocationID;
                            cusLocation.CustomerID = obj.VendorLoadID.Value;
                            cusLocation.Code = obj.ORD_GroupProduct.CUS_Location.Code;
                            cusLocation.LocationName = obj.ORD_GroupProduct.CUS_Location.LocationName;
                            model.CUS_Location.Add(cusLocation);
                        }
                        cusLocation.IsVendorLoad = true;
                        model.SaveChanges();
                    }
                    if (obj.VendorUnLoadID == null)
                        obj.VendorUnLoadContractID = null;
                    else
                    {
                        var contract = lstContractVENLoad.FirstOrDefault(c => c.CustomerID == obj.VendorUnLoadID);
                        if (contract != null)
                            obj.VendorUnLoadContractID = contract.ID;
                        // cập nhật cho CUS_Location của vendor
                        var cusLocation = model.CUS_Location.FirstOrDefault(c => c.CustomerID == obj.VendorUnLoadID && c.LocationID == obj.ORD_GroupProduct.CUS_Location1.LocationID);
                        if (cusLocation == null)
                        {
                            cusLocation = new CUS_Location();
                            cusLocation.CreatedBy = Account.UserName;
                            cusLocation.CreatedDate = DateTime.Now;
                            cusLocation.LocationID = obj.ORD_GroupProduct.CUS_Location1.LocationID;
                            cusLocation.CustomerID = obj.VendorUnLoadID.Value;
                            cusLocation.Code = obj.ORD_GroupProduct.CUS_Location1.Code;
                            cusLocation.LocationName = obj.ORD_GroupProduct.CUS_Location1.LocationName;
                            model.CUS_Location.Add(cusLocation);
                        }
                        cusLocation.IsVendorUnLoad = true;
                        model.SaveChanges();
                    }
                }
            }
        }

        public static void Truck_UpdateMaster(DataEntities model, AccountItem Account, List<int> lstMasterID)
        {
            List<int> lstOrderID = new List<int>();
            foreach (var item in model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && lstMasterID.Contains(c.DITOMasterID.Value) && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusComplete))
            {
                lstOrderID.Add(item.ORD_GroupProduct.OrderID);
                item.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
            }
            model.SaveChanges();
            lstOrderID = lstOrderID.Distinct().ToList();
            HelperStatus.ORDOrder_Status(model, Account, lstOrderID);
        }

        #endregion

        #region Common

        public static bool Expression_CheckBool(DTOPriceDIExExpr item, string strExpr)
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
                worksheet.Cells[row, col].Value = item.DropPoint;
                strExp.Replace("[DropPoint]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DropPointReturn;
                strExp.Replace("[DropPointReturn]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GetPoint;
                strExp.Replace("[GetPoint]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonOrder;
                strExp.Replace("[TonOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMOrder;
                strExp.Replace("[CBMOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityOrder;
                strExp.Replace("[QuantityOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonTransfer;
                strExp.Replace("[TonTransfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMTransfer;
                strExp.Replace("[CBMTransfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityTransfer;
                strExp.Replace("[QuantityTransfer]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonActual;
                strExp.Replace("[TonActual]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMActual;
                strExp.Replace("[CBMActual]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityActual;
                strExp.Replace("[QuantityActual]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonBBGN;
                strExp.Replace("[TonBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMBBGN;
                strExp.Replace("[CBMBBGN]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityBBGN;
                strExp.Replace("[QuantityBBGN]", strCol + row);
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

                worksheet.Cells[row, col].Value = item.GOVCodeOrder;
                strExp.Replace("[GOVCodeOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GOVCodeSchedule;
                strExp.Replace("[GOVCodeSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPrice;
                strExp.Replace("[UnitPrice]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Credit;
                strExp.Replace("[Credit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Debit;
                strExp.Replace("[Debit]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalSchedule;
                strExp.Replace("[TotalSchedule]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMax;
                strExp.Replace("[UnitPriceMax]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMin;
                strExp.Replace("[UnitPriceMin]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GetPointCurrent;
                strExp.Replace("[GetPointCurrent]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DropPointCurrent;
                strExp.Replace("[DropPointCurrent]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalOrder;
                strExp.Replace("[TotalOrder]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.HasCashCollect.ToString().ToUpper();
                strExp.Replace("[HasCashCollect]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPacking;
                strExp.Replace("[TotalPacking]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonMOQ;
                strExp.Replace("[TonMOQ]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMMOQ;
                strExp.Replace("[CBMMOQ]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.SortConfig;
                strExp.Replace("[SortConfig]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsHasPartnerProvince;
                strExp.Replace("[IsHasPartnerProvince]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.RoutingCode;
                strExp.Replace("[RoutingCode]", strCol + row);
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

        private static ExcelPackage Expression_GetPackage(string strExpr)
        {
            try
            {
                //ExcelPackage result = new ExcelPackage();

                string file = "/MailTemplate/" + "Jotun_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                ExcelPackage result = new ExcelPackage(exportfile);


                ExcelWorksheet worksheet = result.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                StringBuilder strExp = new StringBuilder(strExpr);

                row++;
                strExp.Replace("[DropPoint]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DropPointReturn]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[GetPoint]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TonOrder]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[CBMOrder]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[QuantityOrder]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TonTransfer]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[CBMTransfer]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[QuantityTransfer]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TonActual]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[CBMActual]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[QuantityActual]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TonBBGN]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[CBMBBGN]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[QuantityBBGN]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TonReturn]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[CBMReturn]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[QuantityReturn]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[GOVCodeOrder]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[GOVCodeSchedule]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[UnitPrice]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Credit]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[Debit]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalSchedule]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[UnitPriceMax]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[UnitPriceMin]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[GetPointCurrent]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[DropPointCurrent]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalOrder]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[HasCashCollect]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TotalPacking]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[TonMOQ]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[CBMMOQ]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[SortConfig]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[IsHasPartnerProvince]", strCol + row);
                strRow = strCol + row; row++;

                strExp.Replace("[RoutingCode]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();

                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private static ExcelPackage Expression_GetPackage(CAT_PriceDIEx item)
        {
            ExcelPackage result = new ExcelPackage();
            try
            {
                if (!string.IsNullOrEmpty(item.ExprTon))
                    result = Expression_GetPackage(item.ExprTon);
                if (!string.IsNullOrEmpty(item.ExprCBM))
                    result = Expression_GetPackage(item.ExprCBM);
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

        private static decimal? Expression_GetValue(ExcelPackage package, DTOPriceDIExExpr item)
        {
            decimal? result = null;
            try
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet 1"];
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                row++;

                worksheet.Cells[row, col].Value = item.DropPoint;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DropPointReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GetPoint;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonTransfer;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMTransfer;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityTransfer;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonActual;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMActual;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityActual;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonBBGN;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMBBGN;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityBBGN;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GOVCodeOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GOVCodeSchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPrice;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Credit;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Debit;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalSchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMax;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMin;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GetPointCurrent;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DropPointCurrent;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.HasCashCollect;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPacking;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonMOQ;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMMOQ;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.SortConfig;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsHasPartnerProvince;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.RoutingCode;
                strRow = strCol + row; row++;

                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                if (item.ID == 84554)
                    package.Save();

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

        private static bool Expression_Run(ExcelPackage package, DTOPriceDIExExpr item)
        {
            bool result = false;
            try
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets["Sheet 1"];
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                row++;

                worksheet.Cells[row, col].Value = item.DropPoint;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DropPointReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GetPoint;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonTransfer;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMTransfer;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityTransfer;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonActual;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMActual;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityActual;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonBBGN;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMBBGN;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityBBGN;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.QuantityReturn;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GOVCodeOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GOVCodeSchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPrice;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Credit;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Debit;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalSchedule;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMax;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.UnitPriceMin;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.GetPointCurrent;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DropPointCurrent;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalOrder;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.HasCashCollect;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TotalPacking;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.TonMOQ;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBMMOQ;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.SortConfig;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.IsHasPartnerProvince;
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.RoutingCode;
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


        private static void DIPriceFLM_FindOrder(FIN_PL pl, FIN_PLDetails plDetail, List<FIN_Temp> lstPlTemp, IEnumerable<FIN_Temp> lstPlTempQuery, List<int> lstOPSGroupID, List<HelperFinance_OPSGroupProduct> lstOPSGroup, int scheduleID, int masterID)
        {
            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
            plGroup.CreatedBy = pl.CreatedBy;
            plGroup.CreatedDate = DateTime.Now;

            //var plTemp = lstPlTempQuery.OrderByDescending(c => c.CreatedDate).FirstOrDefault(c => c.ScheduleID == scheduleID && c.DITOGroupProductID.HasValue && lstOPSGroupID.Contains(c.DITOGroupProductID.Value) && c.DITOMasterID == masterID);
            //if (plTemp != null)
            //    plGroup.GroupOfProductID = plTemp.DITOGroupProductID.Value;
            //else
            //{
            //    plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            //    // Tạo pl Temp
            //    plTemp = new FIN_Temp();
            //    plTemp.CreatedBy = pl.CreatedBy;
            //    plTemp.CreatedDate = DateTime.Now;
            //    plTemp.ScheduleID = scheduleID;
            //    plTemp.DITOMasterID = masterID;
            //    plTemp.DITOGroupProductID = plGroup.GroupOfProductID;
            //    lstPlTemp.Add(plTemp);
            //}

            plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            plDetail.FIN_PLGroupOfProduct.Add(plGroup);

            var opsGroup = lstOPSGroup.FirstOrDefault(c => c.ID == plGroup.GroupOfProductID);
            if (opsGroup != null)
            {
                pl.OrderID = opsGroup.OrderID;
                pl.DITOMasterID = opsGroup.DITOMasterID;
                pl.VehicleID = opsGroup.VehicleID;
                pl.VendorID = opsGroup.VendorID;
                pl.CustomerID = opsGroup.CustomerID;
            }
        }


        private static HelperFinance_PriceInput DIPrice_GetInput(DataEntities model, DateTime DateConfig, AccountItem Account, List<int> lstCustomerID, bool isCredit)
        {
            DateTime DateConfigEnd = DateConfig.AddDays(1);
            HelperFinance_PriceInput result = new HelperFinance_PriceInput();
            result.ListOrder = new List<HelperFinance_Order>();
            result.ListORDGroupProduct = new List<HelperFinance_ORDGroupProduct>();
            result.ListOPSGroupProduct = new List<HelperFinance_OPSGroupProduct>();
            result.ListTOMaster = new List<HelperFinance_TOMaster>();
            result.ListContract = new List<HelperFinance_Contract>();
            result.ListContractTerm = new List<HelperFinance_ContractTerm>();
            result.ListPrice = new List<HelperFinance_Price>();
            result.ListLTL = new List<HelperFinance_PriceLTL>();
            result.ListLTLLevel = new List<HelperFinance_PriceLTLLevel>();
            result.ListFTL = new List<HelperFinance_PriceFTL>();
            result.ListFTLLevel = new List<HelperFinance_PriceFTLLevel>();
            result.ListLoad = new List<HelperFinance_PriceLoad>();
            result.ListMOQ = new List<HelperFinance_PriceMOQ>();
            result.ListMOQLoad = new List<HelperFinance_PriceMOQLoad>();
            result.ListEx = new List<HelperFinance_PriceEx>();
            result.ListContractGroupProduct = new List<HelperFinance_ContractGroupProduct>();
            result.ListManualFix = new List<HelperFinance_ManualFix>();
            result.ListTrouble = new List<HelperFinance_Trouble>();
            result.ListGroupProductMapping = new List<HelperFinance_GroupProductMapping>();
            result.ListOPSContainer = new List<HelperFinance_OPSContainer>();
            result.ListCutomerID = new List<int>();

            result.SYSSetting = new DTOSYSSetting();
            result.SYSSetting = HelperSYSSetting.SYSSettingSystem_GetBySYSCustomerID(model, Account.SYSCustomerID);

            if (isCredit)
                result.ListCutomerID = lstCustomerID;
            else
            {
                // Lấy danh sách vendor của customer
                result.ListCutomerID = model.CUS_Company.Where(c => lstCustomerID.Contains(c.CustomerRelateID) && (c.CUS_Customer.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN || c.CUS_Customer.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH)).Select(c => c.CustomerOwnID).Distinct().ToList();
                result.ListCutomerID.Add(Account.SYSCustomerID);
            }

            result.ListContract = model.CAT_Contract.Where(c => c.CustomerID > 0 && c.SYSCustomerID == Account.SYSCustomerID && result.ListCutomerID.Contains(c.CustomerID.Value) && c.EffectDate <= DateConfig && (c.ExpiredDate == null || c.ExpiredDate >= DateConfig) && (c.TypeOfContractID == -(int)SYSVarType.TypeOfContractMain || c.TypeOfContractID == -(int)SYSVarType.TypeOfContractSpotRate) && (c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeFTL || c.CAT_TransportMode.TransportModeID == -(int)SYSVarType.TransportModeLTL)).Select(c => new HelperFinance_Contract
            {
                ID = c.ID,
                EffectDate = c.EffectDate,
                PriceInDay = c.PriceInDay,
                TransportModeID = c.CAT_TransportMode.TransportModeID,
                TypeOfContractDateID = c.TypeOfContractDateID,
                TypeOfRunLevelID = c.TypeOfRunLevelID,
                TypeOfSGroupProductChangeID = c.TypeOfSGroupProductChangeID,
                CustomerID = isCredit ? c.CustomerID : (c.CompanyID.HasValue ? c.CUS_Company.CustomerRelateID : -1),
                VendorID = !isCredit ? c.CustomerID.Value : -1,
                TypeOfContractID = c.TypeOfContractID > 0 ? c.TypeOfContractID.Value : -1,
                TypeOfContractQuantityID = c.TypeOfContractQuantityID > 0 ? c.TypeOfContractQuantityID.Value : -(int)SYSVarType.TypeOfContractQuantityTransfer,
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

            #region Lấy data
            if (isCredit)
            {
                //Lấy các đơn hàng
                result.ListOrder = model.ORD_Order.Where(c => c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderReceived && c.ContractID > 0 && result.ListContractID.Contains(c.ContractID.Value)).Select(c => new HelperFinance_Order
                {
                    ID = c.ID,
                    GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                    GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    TypeOfContractID = c.ContractID > 0 && c.CAT_Contract.TypeOfContractID > 0 ? c.CAT_Contract.TypeOfContractID.Value : -1,
                    CATRoutingID = c.CUSRoutingID > 0 ? c.CUS_Routing.RoutingID : -1,
                    ParentRoutingID = c.CUSRoutingID > 0 && c.CUS_Routing.CAT_Routing.ParentID > 0 ? c.CUS_Routing.CAT_Routing.ParentID.Value : -1,
                    DateConfig = c.DateConfig,
                    CustomerID = c.CustomerID,
                    TransportModeID = c.CAT_TransportMode.TransportModeID,
                    OrderCode = c.Code,
                    PriceManual = c.RoutePrice.HasValue ? c.RoutePrice.Value : 0,
                    SortConfig = c.SortConfig.HasValue ? c.SortConfig : -1,
                    Price = c.RoutePrice.HasValue ? c.RoutePrice.Value : 0,
                }).ToList();
                //Lấy các ops nhóm hàng
                result.ListOPSGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.DateConfig.HasValue && c.ORD_GroupProduct.DateConfig >= DateConfig && c.ORD_GroupProduct.DateConfig < DateConfigEnd && c.DITOMasterID > 0 && c.OrderGroupProductID > 0 && c.ORD_GroupProduct.ORD_Product.Count > 0 && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterDelivery && c.ORD_GroupProduct.ORD_Order.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderReceived && c.ORD_GroupProduct.ORD_Order.ContractID > 0 && result.ListContractID.Contains(c.ORD_GroupProduct.ORD_Order.ContractID.Value)).Select(c => new HelperFinance_OPSGroupProduct
                {
                    ID = c.ID,
                    OrderID = c.ORD_GroupProduct.OrderID,
                    DITOMasterID = c.DITOMasterID.Value,
                    VehicleID = c.OPS_DITOMaster.VehicleID,
                    VehicleCode = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                    OrderGroupProductID = c.OrderGroupProductID.Value,
                    GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID > 0 ? c.ORD_GroupProduct.GroupOfProductID.Value : -1,
                    ContractID = c.ORD_GroupProduct.ORD_Order.ContractID > 0 ? c.ORD_GroupProduct.ORD_Order.ContractID.Value : -1,
                    TypeOfContractID = c.ORD_GroupProduct.ORD_Order.ContractID > 0 && c.ORD_GroupProduct.ORD_Order.CAT_Contract.TypeOfContractID > 0 ? c.ORD_GroupProduct.ORD_Order.CAT_Contract.TypeOfContractID.Value : -1,
                    CUSRoutingID = c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUSRoutingID.Value : -1,
                    CATRoutingID = c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.RoutingID : -1,
                    CATRoutingCode = c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.Code : string.Empty,
                    CATRoutingName = c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                    ParentRoutingID = c.ORD_GroupProduct.CUSRoutingID > 0 && c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID.Value : -1,
                    LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                    LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                    LocationToName = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location,
                    LocationToProvinceID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.ProvinceID,
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
                    ProductID = c.ORD_GroupProduct.ORD_Product.FirstOrDefault().ProductID,
                    HasCashCollect = c.ORD_GroupProduct.HasCashCollect.HasValue ? c.ORD_GroupProduct.HasCashCollect.Value : false,
                    Price = c.ORD_GroupProduct.Price.HasValue ? c.ORD_GroupProduct.Price.Value : 0,
                    IsReturn = c.ORD_GroupProduct.IsReturn.HasValue ? c.ORD_GroupProduct.IsReturn.Value : false,
                    SortConfigOrder = c.ORD_GroupProduct.ORD_Order.SortConfig.HasValue ? c.ORD_GroupProduct.ORD_Order.SortConfig.Value : -1,
                    SortConfigMaster = c.OPS_DITOMaster.SortConfig.HasValue ? c.OPS_DITOMaster.SortConfig : -1,
                    PartnerID = c.ORD_GroupProduct.PartnerID > 0 ? c.ORD_GroupProduct.CUS_Partner.PartnerID : -1,
                    TonActual = c.TonTranfer,
                    CBMActual = c.CBMTranfer,
                    QuantityActual = c.QuantityTranfer,
                    TonBBGN = c.TonBBGN,
                    CBMBBGN = c.CBMBBGN,
                    QuantityBBGN = c.QuantityBBGN,
                    Ton = c.Ton,
                    CBM = c.CBM,
                    Quantity = c.Quantity,
                    GOVCode = c.ORD_GroupProduct.ORD_Order.GroupOfVehicleID > 0 ? c.ORD_GroupProduct.ORD_Order.CAT_GroupOfVehicle.Code : string.Empty,
                }).ToList();
                //Lấy các ord nhóm hàng
                result.ListORDGroupProduct = model.ORD_GroupProduct.Where(c => c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.ORD_Product.Count > 0 && c.ORD_Order.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderReceived && c.ORD_Order.ContractID > 0 && result.ListContractID.Contains(c.ORD_Order.ContractID.Value)).Select(c => new HelperFinance_ORDGroupProduct
                {
                    ID = c.ID,
                    OrderID = c.OrderID,
                    GroupOfProductID = c.GroupOfProductID > 0 ? c.GroupOfProductID.Value : -1,
                    ContractID = c.ORD_Order.ContractID > 0 ? c.ORD_Order.ContractID.Value : -1,
                    TypeOfContractID = c.ORD_Order.ContractID > 0 && c.ORD_Order.CAT_Contract.TypeOfContractID > 0 ? c.ORD_Order.CAT_Contract.TypeOfContractID.Value : -1,
                    CUSRoutingID = c.CUSRoutingID > 0 ? c.CUSRoutingID.Value : -1,
                    CATRoutingID = c.CUSRoutingID > 0 ? c.CUS_Routing.RoutingID : -1,
                    ParentRoutingID = c.CUSRoutingID > 0 && c.CUS_Routing.CAT_Routing.ParentID > 0 ? c.CUS_Routing.CAT_Routing.ParentID.Value : -1,
                    LocationFromID = c.CUS_Location.LocationID,
                    LocationToID = c.CUS_Location1.LocationID,
                    GroupOfLocationID = c.CUS_Location1.CAT_Location.GroupOfLocationID > 0 ? c.CUS_Location1.CAT_Location.GroupOfLocationID.Value : -1,
                    Ton = c.Ton,
                    CBM = c.CBM,
                    Quantity = c.Quantity,
                    PriceOfGOPID = c.PriceOfGOPID,
                    GroupOfVehicleID = c.ORD_Order.GroupOfVehicleID > 0 ? c.ORD_Order.GroupOfVehicleID.Value : -1,
                    ProductID = c.ORD_Product.FirstOrDefault().ProductID,
                    PartnerID = c.PartnerID > 0 ? c.CUS_Partner.PartnerID : -1,
                }).ToList();
                // Lấy đơn hàng còn thiếu
                var lstOrderID = result.ListOPSGroupProduct.Select(c => c.OrderID).Distinct().ToList();
                var lstOrderCurrentID = result.ListOrder.Select(c => c.ID).Distinct().ToList();
                var lstOrderNotInID = lstOrderID.Where(c => !lstOrderCurrentID.Contains(c)).ToList();
                result.ListOrder.AddRange(model.ORD_Order.Where(c => lstOrderNotInID.Contains(c.ID)).Select(c => new HelperFinance_Order
                {
                    ID = c.ID,
                    GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                    GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    TypeOfContractID = c.ContractID > 0 && c.CAT_Contract.TypeOfContractID > 0 ? c.CAT_Contract.TypeOfContractID.Value : -1,
                    CATRoutingID = c.CUSRoutingID > 0 ? c.CUS_Routing.RoutingID : -1,
                    ParentRoutingID = c.CUSRoutingID > 0 && c.CUS_Routing.CAT_Routing.ParentID > 0 ? c.CUS_Routing.CAT_Routing.ParentID.Value : -1,
                    DateConfig = c.DateConfig,
                    CustomerID = c.CustomerID,
                    TransportModeID = c.CAT_TransportMode.TransportModeID,
                    OrderCode = c.Code,
                    PriceManual = c.RoutePrice.HasValue ? c.RoutePrice.Value : 0,
                    SortConfig = c.SortConfig.HasValue ? c.SortConfig : -1,
                }).ToList());
            }
            else
            {
                //Lấy các chuyến
                result.ListTOMaster = model.OPS_DITOMaster.Where(c => c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.ContractID > 0 && result.ListContractID.Contains(c.ContractID.Value)).Select(c => new HelperFinance_TOMaster
                {
                    ID = c.ID,
                    VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                    GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                    GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    TypeOfContractID = c.ContractID > 0 && c.CAT_Contract.TypeOfContractID > 0 ? c.CAT_Contract.TypeOfContractID.Value : -1,
                    CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                    ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                    DateConfig = c.DateConfig.Value,
                    VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                    TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                    SortConfig = c.SortConfig.HasValue ? c.SortConfig : -1,
                    Price = c.PriceVendor > 0 ? c.PriceVendor.Value : 0,
                }).ToList();
                //Lấy các ops nhóm hàng
                result.ListOPSGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.DITOMasterID > 0 && c.ORD_GroupProduct.ORD_Product.Count > 0 && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.OPS_DITOMaster.ContractID > 0 && result.ListContractID.Contains(c.OPS_DITOMaster.ContractID.Value)).Select(c => new HelperFinance_OPSGroupProduct
                {
                    ID = c.ID,
                    OrderID = c.ORD_GroupProduct.OrderID,
                    DITOMasterID = c.DITOMasterID.Value,
                    VehicleID = c.OPS_DITOMaster.VehicleID,
                    VehicleCode = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                    OrderGroupProductID = c.OrderGroupProductID.Value,
                    GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID > 0 ? c.ORD_GroupProduct.GroupOfProductID.Value : -1,
                    ContractID = c.OPS_DITOMaster.ContractID > 0 ? c.OPS_DITOMaster.ContractID.Value : -1,
                    TypeOfContractID = c.OPS_DITOMaster.ContractID > 0 && c.OPS_DITOMaster.CAT_Contract.TypeOfContractID > 0 ? c.OPS_DITOMaster.CAT_Contract.TypeOfContractID.Value : -1,
                    CUSRoutingID = c.CUSRoutingID > 0 ? c.CUSRoutingID.Value : -1,
                    CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                    CATRoutingCode = c.CATRoutingID > 0 ? c.CAT_Routing.Code : string.Empty,
                    CATRoutingName = c.CATRoutingID > 0 ? c.CAT_Routing.RoutingName : string.Empty,
                    OrderRoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                    ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                    LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                    LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                    LocationToProvinceID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.ProvinceID,
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
                    HasCashCollect = c.ORD_GroupProduct.HasCashCollect.HasValue ? c.ORD_GroupProduct.HasCashCollect.Value : false,
                    ProductID = c.ORD_GroupProduct.ORD_Product.FirstOrDefault().ProductID,
                    VendorLoadID = c.VendorLoadID == null ? c.OPS_DITOMaster.VendorOfVehicleID : c.VendorLoadID,
                    VendorUnLoadID = c.VendorUnLoadID == null ? c.OPS_DITOMaster.VendorOfVehicleID : c.VendorUnLoadID,
                    VendorLoadContractID = c.VendorLoadContractID,
                    VendorUnLoadContractID = c.VendorUnLoadContractID,
                    IsReturn = c.ORD_GroupProduct.IsReturn.HasValue ? c.ORD_GroupProduct.IsReturn.Value : false,
                    SortConfigOrder = c.ORD_GroupProduct.ORD_Order.SortConfig.HasValue ? c.ORD_GroupProduct.ORD_Order.SortConfig.Value : -1,
                    SortConfigMaster = c.OPS_DITOMaster.SortConfig.HasValue ? c.OPS_DITOMaster.SortConfig : -1,
                    PartnerID = c.ORD_GroupProduct.PartnerID > 0 ? c.ORD_GroupProduct.CUS_Partner.PartnerID : -1,
                    TonActual = c.TonTranfer,
                    CBMActual = c.CBMTranfer,
                    QuantityActual = c.QuantityTranfer,
                    TonBBGN = c.TonBBGN,
                    CBMBBGN = c.CBMBBGN,
                    QuantityBBGN = c.QuantityBBGN,
                    Ton = c.Ton,
                    CBM = c.CBM,
                    Quantity = c.Quantity,
                    GOVCode = c.ORD_GroupProduct.ORD_Order.GroupOfVehicleID > 0 ? c.ORD_GroupProduct.ORD_Order.CAT_GroupOfVehicle.Code : string.Empty,
                }).ToList();
                //Lấy các ops nhóm hàng có bốc xếp ko nằm trong lst trên
                var lstOPSGroupID = result.ListOPSGroupProduct.Select(c => c.ID).Distinct().ToList();
                result.ListOPSGroupProduct.AddRange(model.OPS_DITOGroupProduct.Where(c => !lstOPSGroupID.Contains(c.ID) && c.DateConfig.HasValue && c.DateConfig >= DateConfig && c.DateConfig < DateConfigEnd && c.DITOMasterID > 0 && c.ORD_GroupProduct.ORD_Product.Count > 0 && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && ((c.VendorLoadContractID.HasValue && result.ListContractID.Contains(c.VendorLoadContractID.Value)) || (c.VendorUnLoadContractID.HasValue && result.ListContractID.Contains(c.VendorUnLoadContractID.Value)))).Select(c => new HelperFinance_OPSGroupProduct
                {
                    ID = c.ID,
                    OrderID = c.ORD_GroupProduct.OrderID,
                    DITOMasterID = c.DITOMasterID.Value,
                    VehicleID = c.OPS_DITOMaster.VehicleID,
                    VehicleCode = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                    OrderGroupProductID = c.OrderGroupProductID.Value,
                    GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID > 0 ? c.ORD_GroupProduct.GroupOfProductID.Value : -1,
                    ContractID = c.OPS_DITOMaster.ContractID > 0 ? c.OPS_DITOMaster.ContractID.Value : -1,
                    TypeOfContractID = c.OPS_DITOMaster.ContractID > 0 && c.OPS_DITOMaster.CAT_Contract.TypeOfContractID > 0 ? c.OPS_DITOMaster.CAT_Contract.TypeOfContractID.Value : -1,
                    CUSRoutingID = c.CUSRoutingID > 0 ? c.CUSRoutingID.Value : -1,
                    CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                    CATRoutingCode = c.CATRoutingID > 0 ? c.CAT_Routing.Code : string.Empty,
                    CATRoutingName = c.CATRoutingID > 0 ? c.CAT_Routing.RoutingName : string.Empty,
                    OrderRoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                    ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                    LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                    LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                    LocationToName = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location,
                    LocationToProvinceID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.ProvinceID,
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
                    HasCashCollect = c.ORD_GroupProduct.HasCashCollect.HasValue ? c.ORD_GroupProduct.HasCashCollect.Value : false,
                    ProductID = c.ORD_GroupProduct.ORD_Product.FirstOrDefault().ProductID,
                    VendorLoadID = c.VendorLoadID == null ? c.OPS_DITOMaster.VendorOfVehicleID : c.VendorLoadID,
                    VendorUnLoadID = c.VendorUnLoadID == null ? c.OPS_DITOMaster.VendorOfVehicleID : c.VendorUnLoadID,
                    VendorLoadContractID = c.VendorLoadContractID,
                    VendorUnLoadContractID = c.VendorUnLoadContractID,
                    IsReturn = c.ORD_GroupProduct.IsReturn.HasValue ? c.ORD_GroupProduct.IsReturn.Value : false,
                    SortConfigOrder = c.ORD_GroupProduct.ORD_Order.SortConfig.HasValue ? c.ORD_GroupProduct.ORD_Order.SortConfig.Value : -1,
                    SortConfigMaster = c.OPS_DITOMaster.SortConfig.HasValue ? c.OPS_DITOMaster.SortConfig : -1,
                    PartnerID = c.ORD_GroupProduct.PartnerID > 0 ? c.ORD_GroupProduct.CUS_Partner.PartnerID : -1,
                    TonActual = c.TonTranfer,
                    CBMActual = c.CBMTranfer,
                    QuantityActual = c.QuantityTranfer,
                    TonBBGN = c.TonBBGN,
                    CBMBBGN = c.CBMBBGN,
                    QuantityBBGN = c.QuantityBBGN,
                    Ton = c.Ton,
                    CBM = c.CBM,
                    Quantity = c.Quantity,
                    GOVCode = c.ORD_GroupProduct.ORD_Order.GroupOfVehicleID > 0 ? c.ORD_GroupProduct.ORD_Order.CAT_GroupOfVehicle.Code : string.Empty,
                }).ToList());

                // Danh sách chuyến còn thiếu trong list Master
                var lstOPSMasterID = result.ListOPSGroupProduct.Select(c => c.DITOMasterID).Distinct().ToList();
                var lstOPSMasterCurrentID = result.ListTOMaster.Select(c => c.ID).Distinct().ToList();
                var lstOPSMasterNotInID = lstOPSMasterID.Where(c => !lstOPSMasterCurrentID.Contains(c)).ToList();
                result.ListTOMaster.AddRange(model.OPS_DITOMaster.Where(c => lstOPSMasterNotInID.Contains(c.ID)).Select(c => new HelperFinance_TOMaster
                {
                    ID = c.ID,
                    VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                    GroupOfVehicleID = c.GroupOfVehicleID > 0 ? c.GroupOfVehicleID.Value : -1,
                    GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : string.Empty,
                    ContractID = c.ContractID > 0 ? c.ContractID.Value : -1,
                    TypeOfContractID = c.ContractID > 0 && c.CAT_Contract.TypeOfContractID > 0 ? c.CAT_Contract.TypeOfContractID.Value : -1,
                    CATRoutingID = c.CATRoutingID > 0 ? c.CATRoutingID.Value : -1,
                    ParentRoutingID = c.CATRoutingID > 0 && c.CAT_Routing.ParentID > 0 ? c.CAT_Routing.ParentID.Value : -1,
                    DateConfig = c.DateConfig.Value,
                    VendorID = c.VendorOfVehicleID == null ? Account.SYSCustomerID : c.VendorOfVehicleID.Value,
                    TransportModeID = c.TransportModeID.HasValue ? c.CAT_TransportMode.TransportModeID : -(int)SYSVarType.TransportModeFTL,
                    SortConfig = c.SortConfig.HasValue ? c.SortConfig : -1,
                }).ToList());

                result.ListGroupProductMapping = model.CUS_GroupOfProductMapping.Where(c => result.ListCutomerID.Contains(c.VendorID)).Select(c => new HelperFinance_GroupProductMapping
                {
                    VendorID = c.VendorID,
                    GroupOfProductCUSID = c.GroupOfProductCUSID,
                    GroupOfProductVENID = c.GroupOfProductVENID,
                    PriceOfGOPID = c.CUS_GroupOfProduct1.PriceOfGOPID,
                    PriceOfGOPIDName = c.CUS_GroupOfProduct1.SYS_Var.ValueOfVar,
                }).ToList();
            }

            result.ListOPSGroupProduct = result.ListOPSGroupProduct.OrderBy(c => c.ID).ToList();

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
            }).ToList();

            if (isCredit)
            {
                result.ListManualFix = model.FIN_ManualFix.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Credit != 0 && c.OPS_DITOGroupProduct.ORD_GroupProduct.DateConfig >= DateConfig && c.OPS_DITOGroupProduct.ORD_GroupProduct.DateConfig < DateConfigEnd && result.ListCutomerID.Contains(c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID) && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.ContractID.HasValue && result.ListContractID.Contains(c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.ContractID.Value) && c.OPS_DITOGroupProduct.DITOMasterID.HasValue).Select(c => new HelperFinance_ManualFix
                {
                    ID = c.ID,
                    DITOGroupProductID = c.DITOGroupProductID,
                    DITOMasterID = c.OPS_DITOGroupProduct.DITOMasterID,
                    VehicleID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID,
                    VendorOfVehicleID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID,
                    OrderID = c.OPS_DITOGroupProduct.ORD_GroupProduct.OrderID,
                    CustomerID = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID,
                    ContractID = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.ContractID,
                    Ton = c.Ton,
                    CBM = c.CBM,
                    Quantity = c.Quantity,
                    Credit = c.Credit,
                    Debit = c.Debit,
                    Note = c.Note,
                    UnitPrice = c.UnitPrice,
                    PriceOfGOPID = c.OPS_DITOGroupProduct.ORD_GroupProduct.PriceOfGOPID,
                    PriceOfGOPName = c.OPS_DITOGroupProduct.ORD_GroupProduct.SYS_Var.ValueOfVar
                }).ToList();
            }
            else
            {
                result.ListManualFix = model.FIN_ManualFix.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Debit != 0 && c.OPS_DITOGroupProduct.DateConfig >= DateConfig && c.OPS_DITOGroupProduct.DateConfig < DateConfigEnd && c.OPS_DITOGroupProduct.DITOMasterID.HasValue && c.OPS_DITOGroupProduct.OPS_DITOMaster.ContractID > 0 && result.ListContractID.Contains(c.OPS_DITOGroupProduct.OPS_DITOMaster.ContractID.Value)).Select(c => new HelperFinance_ManualFix
                {
                    ID = c.ID,
                    DITOGroupProductID = c.DITOGroupProductID,
                    DITOMasterID = c.OPS_DITOGroupProduct.DITOMasterID,
                    VehicleID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID,
                    VendorOfVehicleID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID,
                    OrderID = c.OPS_DITOGroupProduct.ORD_GroupProduct.OrderID,
                    CustomerID = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID,
                    ContractID = c.OPS_DITOGroupProduct.OPS_DITOMaster.ContractID,
                    Ton = c.Ton,
                    CBM = c.CBM,
                    Quantity = c.Quantity,
                    Credit = c.Credit,
                    Debit = c.Debit,
                    Note = c.Note,
                    UnitPrice = c.UnitPrice,
                    PriceOfGOPID = c.OPS_DITOGroupProduct.ORD_GroupProduct.PriceOfGOPID,
                    PriceOfGOPName = c.OPS_DITOGroupProduct.ORD_GroupProduct.SYS_Var.ValueOfVar
                }).ToList();
            }

            var lstMasterID = result.ListTOMaster.Select(c => c.ID).Distinct().ToList();
            if (isCredit)
                lstMasterID = result.ListOPSGroupProduct.Select(c => c.DITOMasterID).Distinct().ToList();
            result.ListTrouble = model.CAT_Trouble.Where(c => c.DITOMasterID > 0 && lstMasterID.Contains(c.DITOMasterID.Value) && (isCredit ? c.CostOfCustomer != 0 : c.CostOfVendor != 0) && c.TroubleCostStatusID == -(int)SYSVarType.TroubleCostStatusApproved && c.DriverID == null).Select(c => new HelperFinance_Trouble
            {
                ID = c.ID,
                DITOMasterID = c.DITOMasterID,
                DriverID = c.DriverID,
                VendorOfVehicleID = c.OPS_DITOMaster.VendorOfVehicleID,
                VehicleID = c.OPS_DITOMaster.VehicleID,
                ContractID = c.OPS_DITOMaster.ContractID,
                TypeOfContractID = c.OPS_DITOMaster.ContractID > 0 && c.OPS_DITOMaster.CAT_Contract.TypeOfContractID > 0 ? c.OPS_DITOMaster.CAT_Contract.TypeOfContractID.Value : -1,
                TroubleCostStatusID = c.TroubleCostStatusID,
                GroupOfTroubleID = c.GroupOfTroubleID,
                GroupOfTroubleCode = c.CAT_GroupOfTrouble.Code,
                GroupOfTroubleName = c.CAT_GroupOfTrouble.Name,
                CostOfCustomer = c.CostOfCustomer,
                CostOfVendor = c.CostOfVendor,
                Description = c.Description
            }).ToList();

            #endregion

            #region Lấy giá
            if (result.ListOrder.Count > 0 || result.ListOPSGroupProduct.Count > 0 || result.ListOPSContainer.Count > 0)
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
                }).ToList();

                result.ListContractGroupProduct.AddRange(model.CAT_ContractGroupOfProduct.Where(c => result.ListContractID.Contains(c.ContractID)).Select(c => new HelperFinance_ContractGroupProduct
                {
                    ContractID = c.ContractID,
                    Expression = c.Expression,
                    ExpressionInput = c.ExpressionInput,
                    GroupOfProductID = c.GroupOfProductID,
                    PriceOfGOPID = c.CUS_GroupOfProduct.PriceOfGOPID,
                    GroupOfProductIDChange = c.GroupOfProductIDChange,
                    ProductID = c.ProductID,
                    ProductIDChange = c.ProductIDChange,
                    PriceOfGOPIDChange = c.GroupOfProductIDChange.HasValue ? c.CUS_GroupOfProduct1.PriceOfGOPID : -1,
                    PriceOfGOPIDChangeName = c.GroupOfProductIDChange.HasValue ? c.CUS_GroupOfProduct1.SYS_Var.ValueOfVar : string.Empty,
                    TypeOfPackageID = c.ProductIDChange.HasValue && c.CUS_Product1.PackingID.HasValue ? c.CUS_Product1.CAT_Packing.TypeOfPackageID : -1,
                    Weight = c.ProductIDChange.HasValue ? c.CUS_Product1.Weight : null,
                    CBM = c.ProductIDChange.HasValue ? c.CUS_Product1.CBM : null,
                    TypeOfSGroupProductChangeID = c.CAT_Contract.TypeOfSGroupProductChangeID,
                    TypeOfContractQuantityID = c.CAT_Contract.TypeOfContractQuantityID > 0 ? c.CAT_Contract.TypeOfContractQuantityID.Value : -(int)SYSVarType.TypeOfContractQuantityTransfer,
                }).ToList());

                foreach (var itemContractTerm in result.ListContractTerm)
                {
                    result.ListPrice.AddRange(model.CAT_Price.Where(c => c.ContractTermID == itemContractTerm.ContractTermID && c.EffectDate <= DateConfig).Select(c => new HelperFinance_Price
                    {
                        ContractID = c.CAT_ContractTerm.ContractID,
                        ContractTermID = c.ContractTermID,
                        ID = c.ID,
                        EffectDate = c.EffectDate,
                        PriceContract = c.PriceContract,
                        PriceWarning = c.PriceWarning,
                        TypeOfOrderID = c.TypeOfOrderID,
                        TypeOfRunLevelID = c.TypeOfRunLevelID,
                    }).ToList());

                    result.ListLTL.AddRange(model.CAT_PriceDIGroupProduct.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceLTL
                    {
                        ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_Price.ContractTermID,
                        PriceID = c.PriceID,
                        GroupOfProductID = c.GroupOfProductID,
                        Price = c.Price,
                        RoutingID = c.CAT_ContractRouting.RoutingID,
                        LocationFromID = c.CAT_ContractRouting.CAT_Routing.LocationFromID,
                        LocationToID = c.CAT_ContractRouting.CAT_Routing.LocationToID,
                        EffectDate = c.CAT_Price.EffectDate,
                    }).ToList());

                    result.ListLTLLevel.AddRange(model.CAT_PriceDILevelGroupProduct.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceLTLLevel
                    {
                        ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_Price.ContractTermID,
                        PriceID = c.PriceID,
                        GroupOfProductID = c.GroupOfProductID,
                        Price = c.Price,
                        RoutingID = c.CAT_ContractRouting.RoutingID,
                        LocationFromID = c.CAT_ContractRouting.CAT_Routing.LocationFromID,
                        LocationToID = c.CAT_ContractRouting.CAT_Routing.LocationToID,
                        Ton = c.CAT_ContractLevel.Ton.HasValue ? c.CAT_ContractLevel.Ton.Value : 0,
                        CBM = c.CAT_ContractLevel.CBM.HasValue ? c.CAT_ContractLevel.CBM.Value : 0,
                        Quantity = c.CAT_ContractLevel.Quantity.HasValue ? c.CAT_ContractLevel.Quantity.Value : 0,
                        EffectDate = c.CAT_Price.EffectDate,
                    }).ToList());

                    result.ListFTL.AddRange(model.CAT_PriceGroupVehicle.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceFTL
                    {
                        ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_Price.ContractTermID,
                        PriceID = c.PriceID,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        Price = c.Price,
                        RoutingID = c.CAT_ContractRouting.RoutingID,
                        LocationFromID = c.CAT_ContractRouting.CAT_Routing.LocationFromID,
                        LocationToID = c.CAT_ContractRouting.CAT_Routing.LocationToID,
                        EffectDate = c.CAT_Price.EffectDate,
                    }).ToList());

                    result.ListFTLLevel.AddRange(model.CAT_PriceGVLevelGroupVehicle.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig && c.CAT_ContractLevel.DateStart.HasValue && c.CAT_ContractLevel.DateEnd.HasValue && c.CAT_ContractLevel.GroupOfVehicleID.HasValue).Select(c => new HelperFinance_PriceFTLLevel
                    {
                        ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_Price.ContractTermID,
                        PriceID = c.PriceID,
                        GroupOfVehicleID = c.CAT_ContractLevel.GroupOfVehicleID.Value,
                        RoutingID = c.CAT_ContractRouting.RoutingID,
                        LocationFromID = c.CAT_ContractRouting.CAT_Routing.LocationFromID,
                        LocationToID = c.CAT_ContractRouting.CAT_Routing.LocationToID,
                        DateStart = c.CAT_ContractLevel.DateStart.Value,
                        DateEnd = c.CAT_ContractLevel.DateEnd.Value,
                        EffectDate = c.CAT_Price.EffectDate,
                    }).ToList());

                    result.ListLoad.AddRange(model.CAT_PriceDILoadDetail.Where(c => c.CAT_PriceDILoad.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_PriceDILoad.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceLoad
                    {
                        ContractID = c.CAT_PriceDILoad.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_PriceDILoad.CAT_Price.ContractTermID,
                        PriceID = c.CAT_PriceDILoad.PriceID,
                        GroupOfProductID = c.GroupOfProductID,
                        PriceOfGOPID = c.PriceOfGOPID,
                        Price = c.Price,
                        IsLoading = c.CAT_PriceDILoad.IsLoading,
                        RoutingID = c.CAT_PriceDILoad.RoutingID,
                        ParentRoutingID = c.CAT_PriceDILoad.ParentRoutingID,
                        GroupOfLocationID = c.CAT_PriceDILoad.GroupOfLocationID,
                        LocationID = c.CAT_PriceDILoad.LocationID,
                        PartnerID = c.CAT_PriceDILoad.PartnerID,
                        EffectDate = c.CAT_PriceDILoad.CAT_Price.EffectDate,
                    }).ToList());

                    result.ListMOQ.AddRange(model.CAT_PriceDIMOQ.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceMOQ
                    {
                        ID = c.ID,
                        ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_Price.ContractTermID,
                        PriceID = c.PriceID,
                        DIMOQSumID = c.DIMOQSumID,
                        MOQName = c.MOQName,
                        ExprCBM = c.ExprCBM,
                        ExprInput = c.ExprInput,
                        ExprPrice = c.ExprPrice,
                        ExprPriceFix = c.ExprPriceFix,
                        ExprQuan = c.ExprQuan,
                        ExprTon = c.ExprTon,
                        TypeOfPriceDIExID = c.TypeOfPriceDIExID,
                        TypeOfPriceDIExCode = c.CAT_TypeOfPriceDIEx.Code,
                        TypeOfPriceDIExName = c.CAT_TypeOfPriceDIEx.TypeName,
                        EffectDate = c.CAT_Price.EffectDate,
                    }).ToList());

                    foreach (var itemMOQ in result.ListMOQ.Where(c => c.ContractTermID == itemContractTerm.ContractTermID))
                    {
                        itemMOQ.ListGroupOfLocation = new List<int>();
                        itemMOQ.ListGroupProduct = new List<HelperFinance_PriceMOQGroupProduct>();
                        itemMOQ.ListLocation = new List<HelperFinance_PriceMOQLocation>();
                        itemMOQ.ListParentRouting = new List<int>();
                        itemMOQ.ListRouting = new List<int>();
                        itemMOQ.ListPartnerID = new List<int>();
                        itemMOQ.ListProvinceID = new List<int>();

                        itemMOQ.ListGroupOfLocation = model.CAT_PriceDIMOQGroupLocation.Where(c => c.PriceDIMOQID == itemMOQ.ID).Select(c => c.GroupOfLocationID).Distinct().ToList();
                        itemMOQ.ListGroupProduct = model.CAT_PriceDIMOQGroupProduct.Where(c => c.PriceDIMOQID == itemMOQ.ID).Select(c => new HelperFinance_PriceMOQGroupProduct
                        {
                            GroupOfProductID = c.GroupOfProductID,
                            ExprPrice = c.ExprPrice,
                            ExprQuantity = c.ExprQuantity
                        }).ToList();
                        itemMOQ.ListLocation = model.CAT_PriceDIMOQRouting.Where(c => c.PriceDIMOQID == itemMOQ.ID && c.LocationID.HasValue && c.TypeOfTOLocationID.HasValue).Select(c => new HelperFinance_PriceMOQLocation
                        {
                            LocationID = c.LocationID.Value,
                            TypeOfTOLocationID = c.TypeOfTOLocationID.Value
                        }).ToList();
                        itemMOQ.ListParentRouting = model.CAT_PriceDIMOQRouting.Where(c => c.PriceDIMOQID == itemMOQ.ID && c.ParentRoutingID.HasValue).Select(c => c.ParentRoutingID.Value).Distinct().ToList();
                        itemMOQ.ListRouting = model.CAT_PriceDIMOQRouting.Where(c => c.PriceDIMOQID == itemMOQ.ID && c.RoutingID.HasValue).Select(c => c.RoutingID.Value).Distinct().ToList();
                        itemMOQ.ListPartnerID = model.CAT_PriceDIMOQRouting.Where(c => c.PriceDIMOQID == itemMOQ.ID && c.PartnerID.HasValue).Select(c => c.PartnerID.Value).Distinct().ToList();
                        itemMOQ.ListProvinceID = model.CAT_PriceDIMOQRouting.Where(c => c.PriceDIMOQID == itemMOQ.ID && c.ProvinceID.HasValue).Select(c => c.ProvinceID.Value).Distinct().ToList();
                    }

                    result.ListMOQLoad.AddRange(model.CAT_PriceDIMOQLoad.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceMOQLoad
                    {
                        ID = c.ID,
                        ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_Price.ContractTermID,
                        PriceID = c.PriceID,
                        DIMOQLoadSumID = c.DIMOQLoadSumID,
                        IsLoading = c.IsLoading,
                        MOQName = c.MOQName,
                        ExprCBM = c.ExprCBM,
                        ExprInput = c.ExprInput,
                        ExprPrice = c.ExprPrice,
                        ExprPriceFix = c.ExprPriceFix,
                        ExprQuan = c.ExprQuan,
                        ExprTon = c.ExprTon,
                        TypeOfPriceDIExID = c.TypeOfPriceDIExID,
                        TypeOfPriceDIExCode = c.CAT_TypeOfPriceDIEx.Code,
                        TypeOfPriceDIExName = c.CAT_TypeOfPriceDIEx.TypeName,
                        EffectDate = c.CAT_Price.EffectDate,
                    }).ToList());

                    foreach (var itemMOQ in result.ListMOQLoad.Where(c => c.ContractTermID == itemContractTerm.ContractTermID))
                    {
                        itemMOQ.ListGroupOfLocation = new List<int>();
                        itemMOQ.ListGroupProduct = new List<HelperFinance_PriceMOQGroupProduct>();
                        itemMOQ.ListLocation = new List<HelperFinance_PriceMOQLocation>();
                        itemMOQ.ListParentRouting = new List<int>();
                        itemMOQ.ListRouting = new List<int>();
                        itemMOQ.ListPartnerID = new List<int>();
                        itemMOQ.ListProvinceID = new List<int>();

                        itemMOQ.ListGroupOfLocation = model.CAT_PriceDIMOQLoadGroupLocation.Where(c => c.PriceDIMOQLoadID == itemMOQ.ID).Select(c => c.GroupOfLocationID).Distinct().ToList();
                        itemMOQ.ListGroupProduct = model.CAT_PriceDIMOQLoadGroupProduct.Where(c => c.PriceDIMOQLoadID == itemMOQ.ID).Select(c => new HelperFinance_PriceMOQGroupProduct
                        {
                            GroupOfProductID = c.GroupOfProductID,
                            ExprPrice = c.ExprPrice,
                            ExprQuantity = c.ExprQuantity
                        }).ToList();
                        itemMOQ.ListLocation = model.CAT_PriceDIMOQLoadRouting.Where(c => c.PriceDIMOQLoadID == itemMOQ.ID && c.LocationID.HasValue && c.TypeOfTOLocationID.HasValue).Select(c => new HelperFinance_PriceMOQLocation
                        {
                            LocationID = c.LocationID.Value,
                            TypeOfTOLocationID = c.TypeOfTOLocationID.Value,
                        }).ToList();
                        itemMOQ.ListParentRouting = model.CAT_PriceDIMOQLoadRouting.Where(c => c.PriceDIMOQLoadID == itemMOQ.ID && c.ParentRoutingID.HasValue).Select(c => c.ParentRoutingID.Value).Distinct().ToList();
                        itemMOQ.ListRouting = model.CAT_PriceDIMOQLoadRouting.Where(c => c.PriceDIMOQLoadID == itemMOQ.ID && c.RoutingID.HasValue).Select(c => c.RoutingID.Value).Distinct().ToList();
                        //itemMOQ.ListPartnerID = model.CAT_PriceDIMOQLoadRouting.Where(c => c.PriceDIMOQLoadID == itemMOQ.ID && c.PartnerID.HasValue).Select(c => c.PartnerID.Value).Distinct().ToList();
                        itemMOQ.ListProvinceID = model.CAT_PriceDIMOQLoadRouting.Where(c => c.PriceDIMOQLoadID == itemMOQ.ID && c.ProvinceID.HasValue).Select(c => c.ProvinceID.Value).Distinct().ToList();
                    }

                    result.ListEx.AddRange(model.CAT_PriceDIEx.Where(c => c.CAT_Price.ContractTermID == itemContractTerm.ContractTermID && c.CAT_Price.EffectDate <= DateConfig).Select(c => new HelperFinance_PriceEx
                    {
                        ID = c.ID,
                        ContractID = c.CAT_Price.CAT_ContractTerm.ContractID,
                        ContractTermID = c.CAT_Price.ContractTermID,
                        PriceID = c.PriceID,
                        DIExSumID = c.DIExSumID,
                        Note = c.Note,
                        ExprCBM = c.ExprCBM,
                        ExprInput = c.ExprInput,
                        ExprPrice = c.ExprPrice,
                        ExprPriceFix = c.ExprPriceFix,
                        ExprQuan = c.ExprQuan,
                        ExprTon = c.ExprTon,
                        TypeOfPriceDIExID = c.TypeOfPriceDIExID,
                        TypeOfPriceDIExCode = c.CAT_TypeOfPriceDIEx.Code,
                        TypeOfPriceDIExName = c.CAT_TypeOfPriceDIEx.TypeName,
                        EffectDate = c.CAT_Price.EffectDate,
                    }).ToList());

                    foreach (var itemMOQ in result.ListEx.Where(c => c.ContractTermID == itemContractTerm.ContractTermID))
                    {
                        itemMOQ.ListGroupOfLocation = new List<int>();
                        itemMOQ.ListGroupProduct = new List<HelperFinance_PriceMOQGroupProduct>();
                        itemMOQ.ListLocation = new List<HelperFinance_PriceMOQLocation>();
                        itemMOQ.ListParentRouting = new List<int>();
                        itemMOQ.ListRouting = new List<int>();
                        itemMOQ.ListPartnerID = new List<int>();
                        itemMOQ.ListProvinceID = new List<int>();

                        itemMOQ.ListGroupOfLocation = model.CAT_PriceDIExGroupLocation.Where(c => c.PriceDIExID == itemMOQ.ID).Select(c => c.GroupOfLocationID).Distinct().ToList();
                        itemMOQ.ListGroupProduct = model.CAT_PriceDIExGroupProduct.Where(c => c.PriceDIExID == itemMOQ.ID).Select(c => new HelperFinance_PriceMOQGroupProduct
                        {
                            GroupOfProductID = c.GroupOfProductID,
                            ExprPrice = c.ExprPrice,
                            ExprQuantity = c.ExprQuantity
                        }).ToList();
                        itemMOQ.ListLocation = model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == itemMOQ.ID && c.LocationID.HasValue && c.TypeOfTOLocationID.HasValue).Select(c => new HelperFinance_PriceMOQLocation
                        {
                            LocationID = c.LocationID.Value,
                            TypeOfTOLocationID = c.TypeOfTOLocationID.Value
                        }).ToList();
                        itemMOQ.ListParentRouting = model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == itemMOQ.ID && c.ParentRoutingID.HasValue).Select(c => c.ParentRoutingID.Value).Distinct().ToList();
                        itemMOQ.ListRouting = model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == itemMOQ.ID && c.RoutingID.HasValue).Select(c => c.RoutingID.Value).Distinct().ToList();
                        itemMOQ.ListPartnerID = model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == itemMOQ.ID && c.PartnerID.HasValue).Select(c => c.PartnerID.Value).Distinct().ToList();
                        itemMOQ.ListProvinceID = model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == itemMOQ.ID && c.ProvinceID.HasValue).Select(c => c.ProvinceID.Value).Distinct().ToList();
                    }
                }
            }
            #endregion

            return result;
        }


        private static void DIPriceMOQ_FindOrder(HelperFinance_PriceMOQ itemMOQ, FIN_PL pl, FIN_PLDetails plDetail, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, List<int> lstOPSGroupID, int contractID, List<HelperFinance_OPSGroupProduct> lstOPSGroup, List<HelperFinance_ORDGroupProduct> lstOrderGroupUpdate)
        {
            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
            plGroup.CreatedBy = pl.CreatedBy;
            plGroup.CreatedDate = DateTime.Now;
            plGroup.Quantity = 0;
            plGroup.UnitPrice = 0;

            //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceDIMOQID == itemMOQ.ID && c.DITOGroupProductID.HasValue && lstOPSGroupID.Contains(c.DITOGroupProductID.Value));
            //if (plTemp != null)
            //    plGroup.GroupOfProductID = plTemp.DITOGroupProductID.Value;
            //else
            //{
            //    plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            //    // Tạo pl Temp
            //    var objTemp = new FIN_Temp();
            //    objTemp.CreatedBy = pl.CreatedBy;
            //    objTemp.CreatedDate = DateTime.Now;
            //    objTemp.ContractID = contractID;
            //    objTemp.PriceDIMOQID = itemMOQ.ID;
            //    objTemp.DITOGroupProductID = plGroup.GroupOfProductID;
            //    lstPlTemp.Add(objTemp);
            //}

            plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            plDetail.FIN_PLGroupOfProduct.Add(plGroup);

            var opsGroup = lstOPSGroup.FirstOrDefault(c => c.ID == plGroup.GroupOfProductID);
            if (opsGroup != null)
            {
                pl.OrderID = opsGroup.OrderID;
                pl.DITOMasterID = opsGroup.DITOMasterID;
                pl.VehicleID = opsGroup.VehicleID;
                pl.VendorID = opsGroup.VendorID;
                pl.CustomerID = opsGroup.CustomerID;
            }

            // Cập nhật FINSort
            string strSort = opsGroup.OrderGroupProductID.ToString();
            foreach (var item in lstOPSGroup.Where(c => lstOPSGroupID.Contains(c.ID)))
            {
                if (lstOrderGroupUpdate.Count(c => c.ID == item.OrderGroupProductID) == 0)
                {
                    HelperFinance_ORDGroupProduct itemGroup = new HelperFinance_ORDGroupProduct();
                    itemGroup.ID = item.OrderGroupProductID;
                    if (itemGroup.ID == opsGroup.OrderGroupProductID)
                        itemGroup.FINSort = strSort;
                    else
                        itemGroup.FINSort = strSort + "A";

                    lstOrderGroupUpdate.Add(itemGroup);
                }
            }
        }

        private static void DIPriceEx_FindOrder(HelperFinance_PriceEx itemEx, FIN_PL pl, FIN_PLDetails plDetail, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, List<int> lstOPSGroupID, int contractID, List<HelperFinance_OPSGroupProduct> lstOPSGroup, List<HelperFinance_ORDGroupProduct> lstOrderGroupUpdate)
        {
            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
            plGroup.CreatedBy = pl.CreatedBy;
            plGroup.CreatedDate = DateTime.Now;
            plGroup.Quantity = 0;
            plGroup.UnitPrice = 0;

            //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceDIExID == itemEx.ID && c.DITOGroupProductID.HasValue && lstOPSGroupID.Contains(c.DITOGroupProductID.Value));
            //if (plTemp != null)
            //    plGroup.GroupOfProductID = plTemp.DITOGroupProductID.Value;
            //else
            //{
            //    plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();

            //    // Tạo pl Temp
            //    var objTemp = new FIN_Temp();
            //    objTemp.CreatedBy = pl.CreatedBy;
            //    objTemp.CreatedDate = DateTime.Now;
            //    objTemp.ContractID = contractID;
            //    objTemp.PriceDIExID = itemEx.ID;
            //    objTemp.DITOGroupProductID = plGroup.GroupOfProductID;
            //    lstPlTemp.Add(objTemp);
            //}

            plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            plDetail.FIN_PLGroupOfProduct.Add(plGroup);
            var opsGroup = lstOPSGroup.FirstOrDefault(c => c.ID == plGroup.GroupOfProductID);
            if (opsGroup != null)
            {
                pl.OrderID = opsGroup.OrderID;
                pl.DITOMasterID = opsGroup.DITOMasterID;
                pl.VehicleID = opsGroup.VehicleID;
                pl.VendorID = opsGroup.VendorID;
                pl.CustomerID = opsGroup.CustomerID;
            }

            // Cập nhật FINSort
            string strSort = opsGroup.OrderGroupProductID.ToString();
            foreach (var item in lstOPSGroup.Where(c => lstOPSGroupID.Contains(c.ID)))
            {
                if (lstOrderGroupUpdate.Count(c => c.ID == item.OrderGroupProductID) == 0)
                {
                    HelperFinance_ORDGroupProduct itemGroup = new HelperFinance_ORDGroupProduct();
                    itemGroup.ID = item.OrderGroupProductID;
                    if (itemGroup.ID == opsGroup.OrderGroupProductID)
                        itemGroup.FINSort = strSort;
                    else
                        itemGroup.FINSort = strSort + "A";

                    lstOrderGroupUpdate.Add(itemGroup);
                }
            }
        }

        private static void DIPriceLoad_FindOrder(HelperFinance_PriceMOQLoad itemMOQ, FIN_PL pl, FIN_PLDetails plDetail, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, List<int> lstOPSGroupID, int contractID, List<HelperFinance_OPSGroupProduct> lstOPSGroup, List<HelperFinance_ORDGroupProduct> lstOrderGroupUpdate)
        {
            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
            plGroup.CreatedBy = pl.CreatedBy;
            plGroup.CreatedDate = DateTime.Now;
            plGroup.Quantity = 0;
            plGroup.UnitPrice = 0;

            //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceDIMOQLoadID == itemMOQ.ID && c.DITOGroupProductID.HasValue && lstOPSGroupID.Contains(c.DITOGroupProductID.Value));
            //if (plTemp != null)
            //    plGroup.GroupOfProductID = plTemp.DITOGroupProductID.Value;
            //else
            //{
            //    plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            //    // Tạo pl Temp
            //    var objTemp = new FIN_Temp();
            //    objTemp.CreatedBy = pl.CreatedBy;
            //    objTemp.CreatedDate = DateTime.Now;
            //    objTemp.ContractID = contractID;
            //    objTemp.PriceDIMOQLoadID = itemMOQ.ID;
            //    objTemp.DITOGroupProductID = plGroup.GroupOfProductID;
            //    lstPlTemp.Add(objTemp);
            //}

            plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            plDetail.FIN_PLGroupOfProduct.Add(plGroup);

            var opsGroup = lstOPSGroup.FirstOrDefault(c => c.ID == plGroup.GroupOfProductID);
            if (opsGroup != null)
            {
                pl.OrderID = opsGroup.OrderID;
                pl.DITOMasterID = opsGroup.DITOMasterID;
                pl.VehicleID = opsGroup.VehicleID;
                pl.VendorID = opsGroup.VendorID;
                pl.CustomerID = opsGroup.CustomerID;
            }

            // Cập nhật FINSort
            string strSort = opsGroup.OrderGroupProductID.ToString();
            foreach (var item in lstOPSGroup.Where(c => lstOPSGroupID.Contains(c.ID)))
            {
                if (lstOrderGroupUpdate.Count(c => c.ID == item.OrderGroupProductID) == 0)
                {
                    HelperFinance_ORDGroupProduct itemGroup = new HelperFinance_ORDGroupProduct();
                    itemGroup.ID = item.OrderGroupProductID;
                    if (itemGroup.ID == opsGroup.OrderGroupProductID)
                        itemGroup.FINSort = strSort;
                    else
                        itemGroup.FINSort = strSort + "A";

                    lstOrderGroupUpdate.Add(itemGroup);
                }
            }
        }



        private static decimal? DIPriceEx_CalculatePrice(bool isCredit, DTOPriceDIExExpr objExpr, HelperFinance_PriceEx diEx, FIN_PL pl, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, List<int> lstOPSGroupID, int contractID, List<HelperFinance_OPSGroupProduct> lstOPSGroup)
        {
            decimal? priceFix = null, price = null, Quan = null, CBM = null, Ton = null;
            const int iCredit = (int)CATCostType.DITOExCredit;
            const int iDebit = (int)CATCostType.DITOExDebit;

            if (!string.IsNullOrEmpty(diEx.ExprPriceFix))
                priceFix = Expression_GetValue(Expression_GetPackage(diEx.ExprPriceFix), objExpr);

            if (!string.IsNullOrEmpty(diEx.ExprPrice))
                price = Expression_GetValue(Expression_GetPackage(diEx.ExprPrice), objExpr);

            if (!string.IsNullOrEmpty(diEx.ExprQuan))
                Quan = Expression_GetValue(Expression_GetPackage(diEx.ExprQuan), objExpr);

            if (!string.IsNullOrEmpty(diEx.ExprCBM))
                CBM = Expression_GetValue(Expression_GetPackage(diEx.ExprCBM), objExpr);

            if (!string.IsNullOrEmpty(diEx.ExprTon))
                Ton = Expression_GetValue(Expression_GetPackage(diEx.ExprTon), objExpr);

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
                    plEx.Note1 = diEx.TypeOfPriceDIExName;
                    plEx.TypeOfPriceDIExCode = diEx.TypeOfPriceDIExCode;
                    pl.FIN_PLDetails.Add(plEx);
                    if (!isCredit)
                    {
                        plEx.Credit = 0;
                        plEx.CostID = iDebit;
                        plEx.Debit = priceFix.Value;
                    }

                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                    plGroup.CreatedBy = pl.CreatedBy;
                    plGroup.CreatedDate = DateTime.Now;
                    plGroup.Quantity = 0;
                    plGroup.UnitPrice = 0;

                    var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceDIExID == diEx.ID && c.DITOGroupProductID.HasValue && lstOPSGroupID.Contains(c.DITOGroupProductID.Value));
                    if (plTemp != null)
                        plGroup.GroupOfProductID = plTemp.DITOGroupProductID.Value;
                    else
                    {
                        plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
                        // Tạo pl Temp
                        var objTemp = new FIN_Temp();
                        objTemp.CreatedBy = pl.CreatedBy;
                        objTemp.CreatedDate = DateTime.Now;
                        objTemp.ContractID = contractID;
                        objTemp.PriceDIExID = diEx.ID;
                        objTemp.DITOGroupProductID = plGroup.GroupOfProductID;
                        lstPlTemp.Add(objTemp);
                    }
                    plEx.FIN_PLGroupOfProduct.Add(plGroup);

                    var opsGroup = lstOPSGroup.FirstOrDefault(c => c.ID == plGroup.GroupOfProductID);
                    if (opsGroup != null)
                    {
                        pl.OrderID = opsGroup.OrderID;
                        pl.DITOMasterID = opsGroup.DITOMasterID;
                        pl.VehicleID = opsGroup.VehicleID;
                        pl.VendorID = opsGroup.VendorID;
                        pl.CustomerID = opsGroup.CustomerID;
                    }
                }

                if (price.HasValue && (Quan.HasValue || Ton.HasValue || CBM.HasValue))
                {
                    FIN_PLDetails plEx = new FIN_PLDetails();
                    plEx.CreatedBy = pl.CreatedBy;
                    plEx.CreatedDate = DateTime.Now;
                    plEx.CostID = iCredit;
                    plEx.UnitPrice = price;
                    plEx.Quantity = Ton.HasValue ? (double?)Ton.Value : CBM.HasValue ? (double?)CBM.Value : Quan.HasValue ? (double?)Quan.Value : 0;
                    plEx.Note = diEx.Note;
                    plEx.Note1 = diEx.TypeOfPriceDIExName;
                    plEx.TypeOfPriceDIExCode = diEx.TypeOfPriceDIExCode;
                    plEx.Credit = (decimal)plEx.Quantity.Value * plEx.UnitPrice.Value;
                    pl.FIN_PLDetails.Add(plEx);
                    if (!isCredit)
                    {
                        plEx.Credit = 0;
                        plEx.CostID = iDebit;
                        plEx.Debit = (decimal)plEx.Quantity.Value * plEx.UnitPrice.Value;
                    }

                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                    plGroup.CreatedBy = pl.CreatedBy;
                    plGroup.CreatedDate = DateTime.Now;
                    plGroup.Quantity = plEx.Quantity.Value;
                    plGroup.UnitPrice = plEx.UnitPrice.Value;

                    //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceDIExID == diEx.ID && c.DITOGroupProductID.HasValue && lstOPSGroupID.Contains(c.DITOGroupProductID.Value));
                    //if (plTemp != null)
                    //    plGroup.GroupOfProductID = plTemp.DITOGroupProductID.Value;
                    //else
                    //{
                    //    plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
                    //    // Tạo pl Temp
                    //    var objTemp = new FIN_Temp();
                    //    objTemp.CreatedBy = pl.CreatedBy;
                    //    objTemp.CreatedDate = DateTime.Now;
                    //    objTemp.ContractID = contractID;
                    //    objTemp.PriceDIExID = diEx.ID;
                    //    objTemp.DITOGroupProductID = plGroup.GroupOfProductID;
                    //    lstPlTemp.Add(objTemp);
                    //}

                    plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
                    plEx.FIN_PLGroupOfProduct.Add(plGroup);
                    var opsGroup = lstOPSGroup.FirstOrDefault(c => c.ID == plGroup.GroupOfProductID);
                    if (opsGroup != null)
                    {
                        pl.OrderID = opsGroup.OrderID;
                        pl.DITOMasterID = opsGroup.DITOMasterID;
                        pl.VehicleID = opsGroup.VehicleID;
                        pl.VendorID = opsGroup.VendorID;
                        pl.CustomerID = opsGroup.CustomerID;
                    }
                }
            }
            return priceFix;
        }

        private static void DIPriceEx_CalculatePriceGOP(bool isCredit, DTOPriceDIExExpr objExpr, HelperFinance_PriceEx diEx, HelperFinance_PriceMOQGroupProduct diGroupEx, FIN_PL pl, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, int opsGroupID, int contractID, List<HelperFinance_OPSGroupProduct> lstOPSGroup)
        {
            const int iCredit = (int)CATCostType.DITOExCredit;
            const int iDebit = (int)CATCostType.DITOExDebit;

            decimal? price = null;
            if (!string.IsNullOrEmpty(diGroupEx.ExprPrice))
                price = Expression_GetValue(Expression_GetPackage(diGroupEx.ExprPrice), objExpr);

            decimal? Quantity = null;
            if (!string.IsNullOrEmpty(diGroupEx.ExprQuantity))
                Quantity = Expression_GetValue(Expression_GetPackage(diGroupEx.ExprQuantity), objExpr);

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
                plEx.Note1 = diEx.TypeOfPriceDIExName;
                plEx.TypeOfPriceDIExCode = diEx.TypeOfPriceDIExCode;
                plEx.Credit = plEx.UnitPrice.Value * (decimal)plEx.Quantity.Value;
                pl.FIN_PLDetails.Add(plEx);
                if (!isCredit)
                {
                    plEx.Credit = 0;
                    plEx.CostID = iDebit;
                    plEx.Debit = plEx.UnitPrice.Value * (decimal)plEx.Quantity.Value;
                }

                FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                plGroup.CreatedBy = pl.CreatedBy;
                plGroup.CreatedDate = DateTime.Now;
                plGroup.Quantity = plEx.Quantity.Value;
                plGroup.UnitPrice = plEx.UnitPrice.Value;

                //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceDIExID == diEx.ID && c.DITOGroupProductID.HasValue && c.DITOGroupProductID == opsGroupID);
                //if (plTemp != null)
                //    plGroup.GroupOfProductID = plTemp.DITOGroupProductID.Value;
                //else
                //{
                //    plGroup.GroupOfProductID = opsGroupID;

                //    // Tạo pl Temp
                //    var objTemp = new FIN_Temp();
                //    objTemp.CreatedBy = pl.CreatedBy;
                //    objTemp.CreatedDate = DateTime.Now;
                //    objTemp.ContractID = contractID;
                //    objTemp.PriceDIExID = diEx.ID;
                //    objTemp.DITOGroupProductID = plGroup.GroupOfProductID;
                //    lstPlTemp.Add(objTemp);
                //}

                plGroup.GroupOfProductID = opsGroupID;
                plEx.FIN_PLGroupOfProduct.Add(plGroup);

                var opsGroup = lstOPSGroup.FirstOrDefault(c => c.ID == plGroup.GroupOfProductID);
                if (opsGroup != null)
                {
                    pl.OrderID = opsGroup.OrderID;
                    pl.DITOMasterID = opsGroup.DITOMasterID;
                    pl.VehicleID = opsGroup.VehicleID;
                    pl.VendorID = opsGroup.VendorID;
                    pl.CustomerID = opsGroup.CustomerID;
                }
            }
        }



        private static void DIPriceMOQ_FindOrder_Debit(HelperFinance_PriceMOQ itemMOQ, FIN_PL pl, FIN_PLDetails plDetail, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, List<int> lstOPSGroupID, int contractID, List<HelperFinance_OPSGroupProduct> lstOPSGroup, List<HelperFinance_ORDGroupProduct> lstOrderGroupUpdate)
        {
            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
            plGroup.CreatedBy = pl.CreatedBy;
            plGroup.CreatedDate = DateTime.Now;
            plGroup.Quantity = 0;
            plGroup.UnitPrice = 0;

            //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceDIMOQID == itemMOQ.ID && c.DITOGroupProductID.HasValue && lstOPSGroupID.Contains(c.DITOGroupProductID.Value));
            //if (plTemp != null)
            //    plGroup.GroupOfProductID = plTemp.DITOGroupProductID.Value;
            //else
            //{
            //    plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            //    // Tạo pl Temp
            //    var objTemp = new FIN_Temp();
            //    objTemp.CreatedBy = pl.CreatedBy;
            //    objTemp.CreatedDate = DateTime.Now;
            //    objTemp.ContractID = contractID;
            //    objTemp.PriceDIMOQID = itemMOQ.ID;
            //    objTemp.DITOGroupProductID = plGroup.GroupOfProductID;
            //    lstPlTemp.Add(objTemp);
            //}

            plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            plDetail.FIN_PLGroupOfProduct.Add(plGroup);

            var opsGroup = lstOPSGroup.FirstOrDefault(c => c.ID == plGroup.GroupOfProductID);
            if (opsGroup != null)
            {
                pl.OrderID = opsGroup.OrderID;
                pl.DITOMasterID = opsGroup.DITOMasterID;
                pl.VehicleID = opsGroup.VehicleID;
                pl.VendorID = opsGroup.VendorID;
                pl.CustomerID = opsGroup.CustomerID;
            }

            // Cập nhật FINSort
            string strSort = opsGroup.ID.ToString();
            foreach (var item in lstOPSGroup.Where(c => lstOPSGroupID.Contains(c.ID)))
            {
                if (lstOrderGroupUpdate.Count(c => c.ID == item.ID) == 0)
                {
                    HelperFinance_ORDGroupProduct itemGroup = new HelperFinance_ORDGroupProduct();
                    itemGroup.ID = item.ID;
                    if (itemGroup.ID == opsGroup.ID)
                        itemGroup.FINSort = strSort;
                    else
                        itemGroup.FINSort = strSort + "A";

                    lstOrderGroupUpdate.Add(itemGroup);
                }
            }
        }

        private static void DIPriceEx_FindOrder_Debit(HelperFinance_PriceEx itemEx, FIN_PL pl, FIN_PLDetails plDetail, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, List<int> lstOPSGroupID, int contractID, List<HelperFinance_OPSGroupProduct> lstOPSGroup, List<HelperFinance_ORDGroupProduct> lstOrderGroupUpdate)
        {
            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
            plGroup.CreatedBy = pl.CreatedBy;
            plGroup.CreatedDate = DateTime.Now;
            plGroup.Quantity = 0;
            plGroup.UnitPrice = 0;

            //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceDIExID == itemEx.ID && c.DITOGroupProductID.HasValue && lstOPSGroupID.Contains(c.DITOGroupProductID.Value));
            //if (plTemp != null)
            //    plGroup.GroupOfProductID = plTemp.DITOGroupProductID.Value;
            //else
            //{
            //    plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            //    // Tạo pl Temp
            //    var objTemp = new FIN_Temp();
            //    objTemp.CreatedBy = pl.CreatedBy;
            //    objTemp.CreatedDate = DateTime.Now;
            //    objTemp.ContractID = contractID;
            //    objTemp.PriceDIExID = itemEx.ID;
            //    objTemp.DITOGroupProductID = plGroup.GroupOfProductID;
            //    lstPlTemp.Add(objTemp);
            //}

            plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            plDetail.FIN_PLGroupOfProduct.Add(plGroup);
            var opsGroup = lstOPSGroup.FirstOrDefault(c => c.ID == plGroup.GroupOfProductID);
            if (opsGroup != null)
            {
                pl.OrderID = opsGroup.OrderID;
                pl.DITOMasterID = opsGroup.DITOMasterID;
                pl.VehicleID = opsGroup.VehicleID;
                pl.VendorID = opsGroup.VendorID;
                pl.CustomerID = opsGroup.CustomerID;
            }

            // Cập nhật FINSort
            string strSort = opsGroup.ID.ToString();
            foreach (var item in lstOPSGroup.Where(c => lstOPSGroupID.Contains(c.ID)))
            {
                if (lstOrderGroupUpdate.Count(c => c.ID == item.ID) == 0)
                {
                    HelperFinance_ORDGroupProduct itemGroup = new HelperFinance_ORDGroupProduct();
                    itemGroup.ID = item.ID;
                    if (itemGroup.ID == opsGroup.ID)
                        itemGroup.FINSort = strSort;
                    else
                        itemGroup.FINSort = strSort + "A";

                    lstOrderGroupUpdate.Add(itemGroup);
                }
            }
        }

        private static void DIPriceLoad_FindOrder_Debit(HelperFinance_PriceMOQLoad itemMOQ, FIN_PL pl, FIN_PLDetails plDetail, List<FIN_Temp> lstPlTemp, IEnumerable<HelperFinance_FINTemp> lstPLTempContract, List<int> lstOPSGroupID, int contractID, List<HelperFinance_OPSGroupProduct> lstOPSGroup, List<HelperFinance_ORDGroupProduct> lstOrderGroupUpdate)
        {
            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
            plGroup.CreatedBy = pl.CreatedBy;
            plGroup.CreatedDate = DateTime.Now;
            plGroup.Quantity = 0;
            plGroup.UnitPrice = 0;

            //var plTemp = lstPLTempContract.FirstOrDefault(c => c.PriceDIMOQLoadID == itemMOQ.ID && c.DITOGroupProductID.HasValue && lstOPSGroupID.Contains(c.DITOGroupProductID.Value));
            //if (plTemp != null)
            //    plGroup.GroupOfProductID = plTemp.DITOGroupProductID.Value;
            //else
            //{
            //    plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            //    // Tạo pl Temp
            //    var objTemp = new FIN_Temp();
            //    objTemp.CreatedBy = pl.CreatedBy;
            //    objTemp.CreatedDate = DateTime.Now;
            //    objTemp.ContractID = contractID;
            //    objTemp.PriceDIMOQLoadID = itemMOQ.ID;
            //    objTemp.DITOGroupProductID = plGroup.GroupOfProductID;
            //    lstPlTemp.Add(objTemp);
            //}

            plGroup.GroupOfProductID = lstOPSGroupID.FirstOrDefault();
            plDetail.FIN_PLGroupOfProduct.Add(plGroup);

            var opsGroup = lstOPSGroup.FirstOrDefault(c => c.ID == plGroup.GroupOfProductID);
            if (opsGroup != null)
            {
                pl.OrderID = opsGroup.OrderID;
                pl.DITOMasterID = opsGroup.DITOMasterID;
                pl.VehicleID = opsGroup.VehicleID;
                pl.VendorID = opsGroup.VendorID;
                pl.CustomerID = opsGroup.CustomerID;
            }

            // Cập nhật FINSort
            string strSort = opsGroup.ID.ToString();
            foreach (var item in lstOPSGroup.Where(c => lstOPSGroupID.Contains(c.ID)))
            {
                if (lstOrderGroupUpdate.Count(c => c.ID == item.ID) == 0)
                {
                    HelperFinance_ORDGroupProduct itemGroup = new HelperFinance_ORDGroupProduct();
                    itemGroup.ID = item.ID;
                    if (itemGroup.ID == opsGroup.ID)
                        itemGroup.FINSort = strSort;
                    else
                        itemGroup.FINSort = strSort + "A";

                    lstOrderGroupUpdate.Add(itemGroup);
                }
            }
        }

        #endregion
    }
}
