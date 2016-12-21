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
        #region Truck

        /// <summary>
        /// Tính chi phí của lệnh Master
        /// </summary>
        /// <param name="model"></param>
        /// <param name="master">Lệnh master cần tính</param>
        /// <returns></returns>
        public static decimal DITOMaster_OfferDebit(DataEntities model, OPS_DITOMaster master, FIN_PLDetails plCostDebit, FIN_PL pl, ref bool isFixPrice)
        {
            decimal Price = 0;
            var contract = model.CAT_Contract.FirstOrDefault(c => c.ID == master.ContractID);
            if (contract != null)
            {
                // Lấy ds Nhóm SP thuộc lệnh master
                var lstOrder = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID && c.OrderGroupProductID.HasValue).Select(c => new
                {
                    ID = c.ID,
                    OrderID = c.ORD_GroupProduct.OrderID,
                    GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID,
                    LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                    LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                    PackingID = c.ORD_GroupProduct.PackingID,
                    PriceOfGOPID = c.ORD_GroupProduct.PriceOfGOPID,
                    RoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                    Ton = c.TonTranfer,
                    CBM = c.CBMTranfer,
                    Quantity = c.QuantityTranfer,
                    PriceOfGOPName = c.ORD_GroupProduct.CUS_GroupOfProduct.SYS_Var.ValueOfVar,
                    DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.DateConfig,
                    OrderGroupProductID = c.OrderGroupProductID,
                }).ToList();
                // Ds Nhóm SP
                var lstGroupProduct = lstOrder.Select(c => new DTOFINGroupProduct
                {
                    ID = c.ID,
                    OrderID = c.OrderID,
                    OrderGroupProductID = c.OrderGroupProductID.Value,
                    GroupOfProductID = c.GroupOfProductID.Value,
                    LocationFromID = c.LocationFromID,
                    LocationToID = c.LocationToID,
                    PackingID = c.PackingID,
                    PriceOfGOPID = c.PriceOfGOPID,
                    RoutingID = c.RoutingID,
                    Ton = c.Ton,
                    CBM = c.CBM,
                    Quantity = c.Quantity,
                    PriceOfGOPName = c.PriceOfGOPName,
                    DateConfig = c.DateConfig,
                }).Distinct().ToList();
                // Lấy ngày hiệu lực
                DateTime effectDate = master.DateConfig.HasValue ? master.DateConfig.Value.Date : master.ETD.Value.Date;
                // Tính chi phí
                Price = DITOMaster_GetPriceByContract(model, master, lstGroupProduct, contract.ID, effectDate, master.TypeOfOrderID, plCostDebit, pl, ref isFixPrice);
            }
            return Price;
        }

        /// <summary>
        /// Tính chi phí theo hợp đồng đc chọn
        /// </summary>
        /// <param name="model"></param>
        /// <param name="master"></param>
        /// <param name="contractID">Hợp đồng của vendor</param>
        /// <returns></returns>
        public static decimal DITOMaster_OfferDebitByContract(DataEntities model, OPS_DITOMaster master, int contractID, FIN_PLDetails plCostDebit, FIN_PL pl, ref bool isFixPrice)
        {
            decimal Price = 0;
            var contract = model.CAT_Contract.FirstOrDefault(c => c.ID == contractID);
            if (contract != null)
            {
                // Lấy ds Nhóm SP thuộc lệnh master
                var lstOrder = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID).Select(c => new
                {
                    ID = c.ID,
                    OrderID = c.ORD_GroupProduct.OrderID,
                    GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID,
                    LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                    LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                    PackingID = c.ORD_GroupProduct.PackingID,
                    PriceOfGOPID = c.ORD_GroupProduct.PriceOfGOPID,
                    RoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                    Ton = c.TonTranfer,
                    CBM = c.CBMTranfer,
                    Quantity = c.QuantityTranfer,
                    PriceOfGOPName = c.ORD_GroupProduct.CUS_GroupOfProduct.SYS_Var.ValueOfVar,
                    DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.DateConfig,
                }).ToList();
                // Ds Nhóm SP
                var lstGroupProduct = lstOrder.Select(c => new DTOFINGroupProduct
                {
                    ID = c.ID,
                    OrderID = c.OrderID,
                    GroupOfProductID = c.GroupOfProductID.Value,
                    LocationFromID = c.LocationFromID,
                    LocationToID = c.LocationToID,
                    PackingID = c.PackingID,
                    PriceOfGOPID = c.PriceOfGOPID,
                    RoutingID = c.RoutingID,
                    Ton = c.Ton,
                    CBM = c.CBM,
                    Quantity = c.Quantity,
                    PriceOfGOPName = c.PriceOfGOPName,
                    DateConfig = c.DateConfig,
                }).Distinct().ToList();
                // Lấy ngày hiệu lực
                DateTime effectDate = master.DateConfig.HasValue ? master.DateConfig.Value.Date : master.ETD.Value.Date;
                // Tính chi phí v/c
                Price = DITOMaster_GetPriceByContract(model, master, lstGroupProduct, contract.ID, effectDate, master.TypeOfOrderID, plCostDebit, pl, ref isFixPrice);
                // Chi phí khác
                Price += DITOMaster_GetPriceExByContract(model, new AccountItem(), new FIN_PL(), master, lstGroupProduct, master.ContractID, effectDate, master.TypeOfOrderID);
            }
            return Price;
        }

        /// <summary>
        /// Tính chi phí bốc xếp theo hợp đồng
        /// </summary>
        /// <param name="model"></param>
        /// <param name="master"></param>
        /// <param name="contractID"></param>
        /// <returns></returns>
        public static decimal DITOMaster_OfferDebitLoadByContract(DataEntities model, OPS_DITOMaster master, int contractID, FIN_PL pl)
        {
            decimal Price = 0;
            var contract = model.CAT_Contract.FirstOrDefault(c => c.ID == contractID);
            if (contract != null)
            {
                // Lấy ds Nhóm SP thuộc lệnh master
                var lstOrder = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID).Select(c => new
                {
                    ID = c.ID,
                    OrderID = c.ORD_GroupProduct.OrderID,
                    GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID,
                    LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                    LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                    PackingID = c.ORD_GroupProduct.PackingID,
                    PriceOfGOPID = c.ORD_GroupProduct.PriceOfGOPID,
                    RoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                    Ton = c.TonTranfer,
                    CBM = c.CBMTranfer,
                    Quantity = c.QuantityTranfer,
                    PriceOfGOPName = c.ORD_GroupProduct.CUS_GroupOfProduct.SYS_Var.ValueOfVar,
                    DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.DateConfig,
                    GroupOfLocationFromID = c.ORD_GroupProduct.CUS_Location.CAT_Location.GroupOfLocationID,
                    GroupOfLocationToID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.GroupOfLocationID,
                    ParentRoutingID = c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID,
                }).ToList();
                // Ds Nhóm SP
                var lstGroupProduct = lstOrder.Select(c => new DTOFINGroupProduct
                {
                    ID = c.ID,
                    GroupOfProductID = c.GroupOfProductID.Value,
                    LocationFromID = c.LocationFromID,
                    LocationToID = c.LocationToID,
                    PackingID = c.PackingID,
                    PriceOfGOPID = c.PriceOfGOPID,
                    RoutingID = c.RoutingID,
                    Ton = c.Ton,
                    CBM = c.CBM,
                    Quantity = c.Quantity,
                    PriceOfGOPName = c.PriceOfGOPName,
                    DateConfig = c.DateConfig,
                    GroupOfLocationFromID = c.GroupOfLocationFromID,
                    GroupOfLocationToID = c.GroupOfLocationToID,
                    ParentRoutingID = c.ParentRoutingID,
                }).Distinct().ToList();
                // Lấy ngày hiệu lực
                DateTime effectDate = master.DateConfig.HasValue ? master.DateConfig.Value.Date : master.ETD.Value.Date;
                Price = DITOMaster_GetPriceLoadByContract(model, master, lstGroupProduct, contract.ID, effectDate, master.TypeOfOrderID, pl);
            }
            return Price;
        }

        /// <summary>
        /// Doanh thu của lệnh theo từng đơn hàng
        /// </summary>
        /// <param name="model"></param>
        /// <param name="master">Lệnh v/c</param>
        /// <param name="order">Đơn hàng</param>
        /// <param name="effectDate">Ngày hiệu lực bảng giá</param>
        /// <returns></returns>
        private static decimal DITOMaster_OfferCreditByOrder(DataEntities model, AccountItem Account, OPS_DITOMaster master, ORD_Order order, DateTime effectDate, FIN_PLDetails plCostCredit, ref bool isFixPrice)
        {
            decimal Price = 0;
            const int iFTL = -(int)SYSVarType.TransportModeFTL;
            const int iLTL = -(int)SYSVarType.TransportModeLTL;
            const int iTon = -(int)SYSVarType.PriceOfGOPTon;
            const int iCBM = -(int)SYSVarType.PriceOfGOPCBM;
            const int iTU = -(int)SYSVarType.PriceOfGOPTU;
            // Ds nhóm SP
            var lstGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID && c.ORD_GroupProduct.OrderID == order.ID && c.ORD_GroupProduct.CUSRoutingID.HasValue).Select(c => new DTOFINGroupProduct
            {
                ID = c.ID,
                OrderID = c.ORD_GroupProduct.OrderID,
                OrderGroupProductID = c.OrderGroupProductID.Value,
                PackingID = c.ORD_GroupProduct.PackingID,
                Ton = c.TonTranfer,
                CBM = c.CBMTranfer,
                Quantity = c.QuantityTranfer,
                PriceOfGOPID = c.ORD_GroupProduct.PriceOfGOPID,
                LocationFromID = c.ORD_GroupProduct.LocationFromID.Value,
                LocationToID = c.ORD_GroupProduct.LocationToID.Value,
                Price = c.ORD_GroupProduct.Price,
                GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID.Value,
                RoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                PriceOfGOPName = c.ORD_GroupProduct.PriceOfGOPID.HasValue ? c.ORD_GroupProduct.SYS_Var.ValueOfVar : string.Empty,
                DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.DateConfig,
            });
            // Nếu order là loại HD Nhanh or Khung => Lấy theo giá nhập vào
            if (order.TypeOfContractID == -(int)SYSVarType.TypeOfContractSpotRate || order.TypeOfContractID == -(int)SYSVarType.TypeOfContractFrame)
            {
                // Tính theo chuyến
                if (order.TransportModeID == iFTL)
                {
                    if (order.RoutePrice.HasValue)
                        Price = order.RoutePrice.Value;
                }
                else
                {
                    // Tính theo Tấn, Khối or ĐVVC
                    if (order.TransportModeID == iLTL)
                    {
                        foreach (var group in lstGroup)
                        {
                            if (group.PriceOfGOPID.HasValue)
                            {
                                FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                plGroup.CreatedBy = Account.UserName;
                                plGroup.CreatedDate = DateTime.Now;
                                plGroup.GroupOfProductID = group.ID;
                                plGroup.Unit = group.PriceOfGOPName;
                                plGroup.UnitPrice = group.Price.HasValue ? group.Price.Value : 0;
                                plCostCredit.FIN_PLGroupOfProduct.Add(plGroup);
                                if (group.PriceOfGOPID == iTon)
                                    plGroup.Quantity = group.Ton;
                                else
                                    if (group.PriceOfGOPID == iCBM)
                                        plGroup.Quantity = group.CBM;
                                    else
                                        if (group.PriceOfGOPID == iTU)
                                            plGroup.Quantity = group.Quantity;

                                Price += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                            }
                        }
                    }
                }
            }
            else // Lấy giá theo HD chính thức
            {
                #region FTL
                if (order.TransportModeID == iFTL)
                {
                    // Lấy bảng giá theo ngày hiệu lực
                    var lstPriceID = model.CAT_Price.Where(c => c.CAT_ContractTerm.ContractID == order.ContractID && c.EffectDate <= effectDate && c.TypeOfOrderID == order.TypeOfOrderID && (c.CAT_ContractTerm.DateExpire == null || c.CAT_ContractTerm.DateExpire >= effectDate)).OrderByDescending(c => c.EffectDate).Select(c => c.ID).ToList();
                    if (lstPriceID.Count > 0)
                    {
                        // Ktra xem có tính giá theo ngày/đêm hay ko
                        if (model.CAT_ContractLevel.Count(c => c.ContractID == order.ContractID && c.DateEnd.HasValue && c.DateStart.HasValue && c.GroupOfVehicleID > 0) > 0)
                        {
                            // Tính theo chuyến bình thường => lấy giá cao nhất
                            foreach (var group in lstGroup)
                            {
                                var priceVehicle = model.CAT_PriceGVLevelGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractLevel.GroupOfVehicleID == order.GroupOfVehicleID && c.CAT_ContractRouting.RoutingID == group.RoutingID && c.CAT_ContractLevel.DateStart.HasValue && c.CAT_ContractLevel.DateEnd.HasValue).Select(c => new
                                    {
                                        Price = c.Price,
                                        c.CAT_ContractLevel.DateStart,
                                        c.CAT_ContractLevel.DateEnd,
                                        c.CAT_ContractRouting.RoutingID
                                    }).ToList();
                                if (priceVehicle == null || priceVehicle.Count == 0)
                                    priceVehicle = model.CAT_PriceGVLevelGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractLevel.GroupOfVehicleID == order.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.LocationToID).Select(c => new
                                    {
                                        Price = c.Price,
                                        c.CAT_ContractLevel.DateStart,
                                        c.CAT_ContractLevel.DateEnd,
                                        c.CAT_ContractRouting.RoutingID
                                    }).ToList();
                                if (priceVehicle == null || priceVehicle.Count == 0)
                                    priceVehicle = model.CAT_PriceGVLevelGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractLevel.GroupOfVehicleID == order.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationToID)).Select(c => new
                                    {
                                        Price = c.Price,
                                        c.CAT_ContractLevel.DateStart,
                                        c.CAT_ContractLevel.DateEnd,
                                        c.CAT_ContractRouting.RoutingID
                                    }).ToList();
                                if (priceVehicle != null && priceVehicle.Count > 0)
                                {
                                    foreach (var priceGV in priceVehicle)
                                    {
                                        // Ngày
                                        if (priceGV.DateStart <= priceGV.DateEnd)
                                        {
                                            if (priceGV.DateStart.Value.TimeOfDay <= group.DateConfig.TimeOfDay && group.DateConfig.TimeOfDay < priceGV.DateStart.Value.TimeOfDay)
                                                if (priceGV.Price > Price)
                                                {
                                                    Price = priceGV.Price;
                                                    var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.RoutingID == priceGV.RoutingID && c.CustomerID == order.CustomerID);
                                                    if (cusRouting != null)
                                                        order.CUSRoutingID = cusRouting.ID;
                                                }
                                        }
                                        else
                                        {
                                            // Đêm
                                            if (priceGV.DateStart.Value.TimeOfDay <= group.DateConfig.TimeOfDay || group.DateConfig.TimeOfDay < priceGV.DateStart.Value.TimeOfDay)
                                                if (priceGV.Price > Price)
                                                {
                                                    Price = priceGV.Price;
                                                    var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.RoutingID == priceGV.RoutingID && c.CustomerID == order.CustomerID);
                                                    if (cusRouting != null)
                                                        order.CUSRoutingID = cusRouting.ID;
                                                }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Tính theo chuyến bình thường => lấy giá cao nhất
                            foreach (var group in lstGroup)
                            {
                                var priceGV = model.CAT_PriceGroupVehicle.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfVehicleID == order.GroupOfVehicleID && c.CAT_ContractRouting.RoutingID == group.RoutingID);
                                if (priceGV == null)
                                    priceGV = model.CAT_PriceGroupVehicle.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfVehicleID == order.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.LocationToID);
                                if (priceGV == null)
                                    priceGV = model.CAT_PriceGroupVehicle.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfVehicleID == order.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationToID));
                                if (priceGV != null && priceGV.Price > Price)
                                {
                                    Price = priceGV.Price;
                                    var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.RoutingID == priceGV.CAT_ContractRouting.RoutingID && c.CustomerID == order.CustomerID);
                                    if (cusRouting != null)
                                        order.CUSRoutingID = cusRouting.ID;
                                }
                            }
                        }
                    }
                }
                #endregion

                #region LTL
                else
                {
                    // Tính theo Tấn, Khối or ĐVVC
                    if (order.TransportModeID == iLTL)
                    {
                        // Lấy bảng giá theo ngày hiệu lực
                        foreach (var group in lstGroup)
                        {
                            if (group.PriceOfGOPID.HasValue)
                            {
                                #region Lấy giá + MOQ nếu có
                                double totalTon = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == group.OrderGroupProductID).Ton;
                                double totalCBM = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == group.OrderGroupProductID).CBM;
                                double totalTU = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == group.OrderGroupProductID).Quantity;

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

                                decimal unitPrice = 0;
                                string strMOQName = string.Empty;
                                var lstPriceID = model.CAT_Price.Where(c => c.CAT_ContractTerm.ContractID == order.ContractID && c.EffectDate <= group.DateConfig && c.TypeOfOrderID == order.TypeOfOrderID && (c.CAT_ContractTerm.DateExpire == null || c.CAT_ContractTerm.DateExpire >= group.DateConfig)).OrderByDescending(c => c.EffectDate).Select(c => c.ID).ToList();
                                if (lstPriceID != null)
                                {
                                    if (model.CAT_ContractLevel.Count(c => c.ContractID == order.ContractID && (c.Ton > 0 || c.CBM > 0 || c.Quantity > 0)) > 0)
                                    {
                                        var priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.RoutingID == group.RoutingID && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                                        if (priceGOP == null)
                                            priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.LocationToID && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                                        if (priceGOP == null)
                                            priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationToID) && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                                        if (priceGOP != null)
                                            unitPrice = priceGOP.Price;
                                    }
                                    else
                                    {
                                        var priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.RoutingID == group.RoutingID);
                                        if (priceGOP == null)
                                            priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.LocationToID);
                                        if (priceGOP == null)
                                            priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfProductID == group.GroupOfProductID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationToID));
                                        if (priceGOP != null)
                                            unitPrice = priceGOP.Price;
                                    }
                                }
                                #endregion

                                if (!isFixPrice)
                                {
                                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                    plGroup.CreatedBy = Account.UserName;
                                    plGroup.CreatedDate = DateTime.Now;
                                    plGroup.GroupOfProductID = group.ID;
                                    plGroup.Unit = group.PriceOfGOPName;
                                    plGroup.UnitPrice = unitPrice;
                                    plCostCredit.FIN_PLGroupOfProduct.Add(plGroup);
                                    plCostCredit.Note = strMOQName;
                                    if (group.PriceOfGOPID == iTon)
                                        plGroup.Quantity = group.Ton;
                                    else
                                        if (group.PriceOfGOPID == iCBM)
                                            plGroup.Quantity = group.CBM;
                                        else
                                            if (group.PriceOfGOPID == iTU)
                                                plGroup.Quantity = group.Quantity;

                                    if (exchangeQuantity.HasValue)
                                    {
                                        DTOCATContractGroupOfProduct itemChange = new DTOCATContractGroupOfProduct();
                                        itemChange.Ton = group.Ton;
                                        itemChange.CBM = group.CBM;
                                        itemChange.Quantity = group.Quantity;
                                        itemChange.Expression = exchange.Expression;
                                        exchangeQuantity = GetGroupOfProductTransfer(itemChange);

                                        if (exchangeQuantity.HasValue)
                                            plGroup.Quantity = exchangeQuantity.Value;
                                    }

                                    Price += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                                }
                            }
                        }
                    }
                }
                #endregion
            }
            return Price;
        }

        /// <summary>
        /// Doanh thu bốc xếp của lệnh theo từng đơn hàng
        /// </summary>
        /// <param name="model"></param>
        /// <param name="master">Lệnh v/c</param>
        /// <param name="order">Đơn hàng</param>
        /// <param name="effectDate">Ngày hiệu lực bảng giá</param>
        /// <returns></returns>
        private static decimal DITOMaster_OfferCreditLoadByOrder(DataEntities model, AccountItem Account, OPS_DITOMaster master, ORD_Order order, DateTime effectDate, FIN_PL pl)
        {
            decimal Price = 0;
            // Tính doanh thu bốc xếp khi có HD và HD chính thức
            if (order.TypeOfContractID == -(int)SYSVarType.TypeOfContractMain && order.ContractID.HasValue)
            {
                // Ds nhóm SP
                var lstGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID && c.ORD_GroupProduct.OrderID == order.ID && c.ORD_GroupProduct.CUSRoutingID.HasValue).Select(c => new DTOFINGroupProduct
                {
                    ID = c.ID,
                    OrderGroupProductID = c.OrderGroupProductID.Value,
                    PackingID = c.ORD_GroupProduct.PackingID,
                    Ton = c.TonTranfer,
                    CBM = c.CBMTranfer,
                    Quantity = c.QuantityTranfer,
                    PriceOfGOPID = c.ORD_GroupProduct.PriceOfGOPID,
                    LocationFromID = c.ORD_GroupProduct.LocationFromID.Value,
                    LocationToID = c.ORD_GroupProduct.LocationToID.Value,
                    Price = c.ORD_GroupProduct.Price,
                    GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID.Value,
                    RoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                    PriceOfGOPName = c.ORD_GroupProduct.PriceOfGOPID.HasValue ? c.ORD_GroupProduct.SYS_Var.ValueOfVar : string.Empty,
                    DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.DateConfig,
                    ParentRoutingID = c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID,
                    GroupOfLocationFromID = c.ORD_GroupProduct.CUS_Location.CAT_Location.GroupOfLocationID,
                    GroupOfLocationToID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.GroupOfLocationID,
                });
                // Bốc xếp lên
                FIN_PLDetails plLoad = new FIN_PLDetails();
                plLoad.CreatedBy = Account.UserName;
                plLoad.CreatedDate = DateTime.Now;
                plLoad.CostID = (int)CATCostType.DITOLoadCredit;
                // Bốc xếp xuống
                FIN_PLDetails plUnLoad = new FIN_PLDetails();
                plUnLoad.CreatedBy = Account.UserName;
                plUnLoad.CreatedDate = DateTime.Now;
                plUnLoad.CostID = (int)CATCostType.DITOUnLoadCredit;
                // Tính doanh thu bốc xếp cho từng nhóm SP
                foreach (var group in lstGroup)
                {
                    group.DateConfig = group.DateConfig.Date;
                    // Lấy bảng giá theo ngày hiệu lực
                    var price = model.CAT_Price.Where(c => c.CAT_ContractTerm.ContractID == order.ContractID && c.EffectDate <= group.DateConfig && c.TypeOfOrderID == order.TypeOfOrderID).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                    if (price != null)
                    {
                        // Bốc xếp lên
                        var priceLoad = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.CAT_PriceDILoad.PriceID == price.ID && c.CAT_PriceDILoad.IsLoading && c.GroupOfProductID == group.GroupOfProductID && (c.CAT_PriceDILoad.LocationID == group.LocationFromID || c.CAT_PriceDILoad.RoutingID == group.RoutingID || (group.ParentRoutingID.HasValue && c.CAT_PriceDILoad.ParentRoutingID == group.ParentRoutingID) || (group.GroupOfLocationFromID.HasValue && c.CAT_PriceDILoad.GroupOfLocationID == group.GroupOfLocationFromID)));
                        if (priceLoad != null)
                        {
                            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                            plGroup.CreatedBy = Account.UserName;
                            plGroup.CreatedDate = DateTime.Now;
                            plGroup.GroupOfProductID = group.ID;
                            plGroup.Unit = priceLoad.SYS_Var.ValueOfVar;
                            plGroup.UnitPrice = priceLoad.Price;
                            plLoad.FIN_PLGroupOfProduct.Add(plGroup);
                            // Tính theo Tấn
                            if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTon)
                                plGroup.Quantity = group.Ton;
                            else if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPCBM)
                                plGroup.Quantity = group.CBM;
                            else if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTU)
                                plGroup.Quantity = group.Quantity;

                            Price += (decimal)plGroup.Quantity * priceLoad.Price;

                            plLoad.Credit = Price;
                            if (plLoad.FIN_PLGroupOfProduct.Count > 0)
                                pl.FIN_PLDetails.Add(plLoad);
                        }

                        // Bốc xếp xuống
                        var priceUnLoad = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.CAT_PriceDILoad.PriceID == price.ID && !c.CAT_PriceDILoad.IsLoading && c.GroupOfProductID == group.GroupOfProductID && (c.CAT_PriceDILoad.LocationID == group.LocationToID || c.CAT_PriceDILoad.RoutingID == group.RoutingID || (group.ParentRoutingID.HasValue && c.CAT_PriceDILoad.ParentRoutingID == group.ParentRoutingID) || (group.GroupOfLocationToID.HasValue && c.CAT_PriceDILoad.GroupOfLocationID == group.GroupOfLocationToID)));
                        if (priceUnLoad != null)
                        {
                            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                            plGroup.CreatedBy = Account.UserName;
                            plGroup.CreatedDate = DateTime.Now;
                            plGroup.GroupOfProductID = group.ID;
                            plGroup.Unit = priceUnLoad.SYS_Var.ValueOfVar;
                            plGroup.UnitPrice = priceUnLoad.Price;
                            plUnLoad.FIN_PLGroupOfProduct.Add(plGroup);
                            // Tính theo Tấn
                            if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTon)
                                plGroup.Quantity = group.Ton;
                            else if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPCBM)
                                plGroup.Quantity = group.CBM;
                            else if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTU)
                                plGroup.Quantity = group.Quantity;

                            Price += (decimal)plGroup.Quantity * priceUnLoad.Price;

                            plUnLoad.Credit = Price;
                            if (plUnLoad.FIN_PLGroupOfProduct.Count > 0)
                                pl.FIN_PLDetails.Add(plUnLoad);
                        }
                    }

                }
            }
            return Price;
        }

        /// <summary>
        /// Tính giá theo hợp đồng
        /// </summary>
        /// <param name="model"></param>
        /// <param name="master"></param>
        /// <param name="lstGroupProduct"></param>
        /// <param name="contractID"></param>
        /// <param name="effectDate"></param>
        /// <param name="TypeOfOrderID"></param>
        /// <returns></returns>
        private static decimal DITOMaster_GetPriceByContract(DataEntities model, OPS_DITOMaster master, List<DTOFINGroupProduct> lstGroupProduct, int? contractID, DateTime effectDate, int? TypeOfOrderID, FIN_PLDetails plCostDebit, FIN_PL pl, ref bool isFixPrice)
        {
            decimal Price = 0;
            const int iFTL = -(int)SYSVarType.TransportModeFTL;
            const int iLTL = -(int)SYSVarType.TransportModeLTL;
            const int iTon = -(int)SYSVarType.PriceOfGOPTon;
            const int iCBM = -(int)SYSVarType.PriceOfGOPCBM;
            const int iTU = -(int)SYSVarType.PriceOfGOPTU;

            // lấy hợp đồng
            var contract = model.CAT_Contract.FirstOrDefault(c => c.ID == contractID);
            if (contract != null)
            {
                // Danh sách product mapping
                var lsGroupMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == contract.CustomerID && c.SYSCustomerID == contract.SYSCustomerID).Select(c => new { c.GroupOfProductCUSID, c.GroupOfProductVENID }).ToList();
                // Lấy bảng giá
                var lstPriceID = model.CAT_Price.Where(c => c.CAT_ContractTerm.ContractID == contractID && c.EffectDate <= effectDate && c.TypeOfOrderID == TypeOfOrderID && (c.CAT_ContractTerm.DateExpire == null || c.CAT_ContractTerm.DateExpire >= effectDate)).OrderByDescending(c => c.EffectDate).Select(c => c.ID).ToList();
                if (lstPriceID != null)
                {
                    #region Tính cước v/c theo chuyến
                    if (contract.TransportModeID == iFTL)
                    {
                        // Ktra xem có tính giá theo ngày/đêm hay ko
                        if (model.CAT_ContractLevel.Count(c => c.ContractID == contract.ID && c.DateEnd.HasValue && c.DateStart.HasValue && c.GroupOfVehicleID > 0) > 0)
                        {
                            // lấy giá cao nhất
                            foreach (var group in lstGroupProduct)
                            {
                                var priceVehicle = model.CAT_PriceGVLevelGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractLevel.GroupOfVehicleID == master.GroupOfVehicleID && c.CAT_ContractRouting.RoutingID == group.RoutingID && c.CAT_ContractLevel.DateStart.HasValue && c.CAT_ContractLevel.DateEnd.HasValue).Select(c => new
                                {
                                    Price = c.Price,
                                    c.CAT_ContractLevel.DateStart,
                                    c.CAT_ContractLevel.DateEnd,
                                    c.CAT_ContractRouting.RoutingID,
                                    c.ContractRoutingID
                                }).ToList();
                                if (priceVehicle == null || priceVehicle.Count == 0)
                                    priceVehicle = model.CAT_PriceGVLevelGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractLevel.GroupOfVehicleID == master.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.LocationToID).Select(c => new
                                    {
                                        Price = c.Price,
                                        c.CAT_ContractLevel.DateStart,
                                        c.CAT_ContractLevel.DateEnd,
                                        c.CAT_ContractRouting.RoutingID,
                                        c.ContractRoutingID
                                    }).ToList();
                                if (priceVehicle == null || priceVehicle.Count == 0)
                                    priceVehicle = model.CAT_PriceGVLevelGroupVehicle.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractLevel.GroupOfVehicleID == master.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationToID)).Select(c => new
                                    {
                                        Price = c.Price,
                                        c.CAT_ContractLevel.DateStart,
                                        c.CAT_ContractLevel.DateEnd,
                                        c.CAT_ContractRouting.RoutingID,
                                        c.ContractRoutingID
                                    }).ToList();
                                if (priceVehicle != null && priceVehicle.Count > 0)
                                {
                                    foreach (var priceGV in priceVehicle)
                                    {
                                        // Ngày
                                        if (priceGV.DateStart <= priceGV.DateEnd)
                                        {
                                            if (priceGV.DateStart.Value.TimeOfDay <= group.DateConfig.TimeOfDay && group.DateConfig.TimeOfDay < priceGV.DateStart.Value.TimeOfDay)
                                                if (priceGV.Price > Price)
                                                {
                                                    Price = priceGV.Price;
                                                    var opsGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == group.ID);
                                                    if (opsGroup != null)
                                                    {
                                                        var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.RoutingID == priceGV.RoutingID && c.CustomerID == master.VendorOfVehicleID);
                                                        if (cusRouting != null)
                                                            opsGroup.CUSRoutingID = cusRouting.ID;
                                                    }
                                                }
                                        }
                                        else
                                        {
                                            // Đêm
                                            if (priceGV.DateStart.Value.TimeOfDay <= group.DateConfig.TimeOfDay || group.DateConfig.TimeOfDay < priceGV.DateStart.Value.TimeOfDay)
                                                if (priceGV.Price > Price)
                                                {
                                                    Price = priceGV.Price;
                                                    var opsGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == group.ID);
                                                    if (opsGroup != null)
                                                    {
                                                        var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.RoutingID == priceGV.RoutingID && c.CustomerID == master.VendorOfVehicleID);
                                                        if (cusRouting != null)
                                                            opsGroup.CUSRoutingID = cusRouting.ID;
                                                    }
                                                }
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            // Tính theo chuyến bình thường => lấy giá cao nhất
                            foreach (var group in lstGroupProduct)
                            {
                                var priceGV = model.CAT_PriceGroupVehicle.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfVehicleID == master.GroupOfVehicleID && c.CAT_ContractRouting.RoutingID == group.RoutingID);
                                if (priceGV == null)
                                    priceGV = model.CAT_PriceGroupVehicle.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfVehicleID == master.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.LocationToID);
                                if (priceGV == null)
                                    priceGV = model.CAT_PriceGroupVehicle.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.GroupOfVehicleID == master.GroupOfVehicleID && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationToID));
                                if (priceGV != null && priceGV.Price > Price)
                                {
                                    Price = priceGV.Price;
                                    var opsGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == group.ID);
                                    if (opsGroup != null)
                                    {
                                        var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.RoutingID == priceGV.CAT_ContractRouting.RoutingID && c.CustomerID == master.VendorOfVehicleID);
                                        if (cusRouting != null)
                                            opsGroup.CUSRoutingID = cusRouting.ID;
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region Tính cước v/c theo Tấn, Khối or ĐVVC
                    else
                    {
                        if (contract.TransportModeID == iLTL)
                        {
                            foreach (var group in lstGroupProduct)
                            {
                                double totalTon = lstGroupProduct.Where(c => c.OrderGroupProductID == group.OrderGroupProductID).Sum(c => c.Ton);
                                double totalCBM = lstGroupProduct.Where(c => c.OrderGroupProductID == group.OrderGroupProductID).Sum(c => c.CBM);
                                double totalTU = lstGroupProduct.Where(c => c.OrderGroupProductID == group.OrderGroupProductID).Sum(c => c.Quantity);

                                // Nếu hợp đồng vendor tính theo đơn hàng => lấy giá theo tổng đơn hàng của tất cả chuyến đã chở xong
                                if (contract.IsVENLTLLevelOrder)
                                {
                                    var lstVENGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.OPS_DITOMaster.VendorOfVehicleID == master.VendorOfVehicleID && (c.ORD_GroupProduct.IsReturn == null || c.ORD_GroupProduct.IsReturn == false) && c.OrderGroupProductID == group.OrderGroupProductID);
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
                                var exchange = model.CAT_ContractGroupOfProduct.FirstOrDefault(c => c.ContractID == contractID && lstGroupMappingID.Contains(c.GroupOfProductID) && !string.IsNullOrEmpty(c.Expression));
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

                                FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                                plGroup.CreatedBy = plCostDebit.CreatedBy;
                                plGroup.CreatedDate = DateTime.Now;
                                plGroup.GroupOfProductID = group.ID;
                                plGroup.Unit = group.PriceOfGOPName;
                                plGroup.UnitPrice = group.Price.HasValue ? group.Price.Value : 0;

                                decimal unitPrice = plGroup.UnitPrice;

                                #region Lấy bảng giá
                                if (model.CAT_ContractLevel.Count(c => c.ContractID == contract.ID && (c.Ton > 0 || c.CBM > 0 || c.Quantity > 0)) > 0)
                                {
                                    var priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.RoutingID == group.RoutingID && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID) && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                                    if (priceGOP == null)
                                        priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.LocationToID && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID) && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                                    if (priceGOP == null)
                                        priceGOP = model.CAT_PriceDILevelGroupProduct.Where(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationToID) && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID) && (c.CAT_ContractLevel.Ton == 0 || c.CAT_ContractLevel.Ton >= totalTon) && (c.CAT_ContractLevel.CBM == 0 || c.CAT_ContractLevel.CBM >= totalCBM) && (c.CAT_ContractLevel.Quantity == 0 || c.CAT_ContractLevel.Quantity >= totalTU)).OrderBy(c => c.CAT_ContractLevel.Ton).ThenBy(c => c.CAT_ContractLevel.CBM).ThenBy(c => c.CAT_ContractLevel.Quantity).FirstOrDefault();
                                    if (priceGOP != null)
                                    {
                                        plGroup.UnitPrice = priceGOP.Price;
                                        var opsGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == group.ID);
                                        if (opsGroup != null)
                                        {
                                            var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.RoutingID == priceGOP.CAT_ContractRouting.RoutingID && c.CustomerID == master.VendorOfVehicleID);
                                            if (cusRouting != null)
                                                opsGroup.CUSRoutingID = cusRouting.ID;
                                        }
                                    }
                                }
                                else
                                {
                                    // Lấy bảng giá theo routing order
                                    var priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.RoutingID == group.RoutingID && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID));
                                    if (priceGOP == null)
                                        // Lấy bảng giá theo location trùng vs location của order
                                        priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.CAT_Routing.LocationFromID == group.LocationFromID && c.CAT_ContractRouting.CAT_Routing.LocationToID == group.LocationToID && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID));
                                    if (priceGOP == null)
                                        // Lấy bảng giá theo khu vực có location trùng vs location của order
                                        priceGOP = model.CAT_PriceDIGroupProduct.FirstOrDefault(c => lstPriceID.Contains(c.PriceID) && c.CAT_ContractRouting.CAT_Routing.RoutingAreaFromID.HasValue && c.CAT_ContractRouting.CAT_Routing.RoutingAreaToID.HasValue && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationFromID) && c.CAT_ContractRouting.CAT_Routing.CAT_RoutingArea1.CAT_RoutingAreaLocation.Any(d => d.LocationID == group.LocationToID) && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID));
                                    if (priceGOP != null)
                                    {
                                        plGroup.UnitPrice = priceGOP.Price;
                                        var opsGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == group.ID);
                                        if (opsGroup != null)
                                        {
                                            var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.RoutingID == priceGOP.CAT_ContractRouting.RoutingID && c.CustomerID == master.VendorOfVehicleID);
                                            if (cusRouting != null)
                                                opsGroup.CUSRoutingID = cusRouting.ID;
                                        }
                                    }
                                }
                                #endregion

                                if (!isFixPrice)
                                {
                                    plCostDebit.FIN_PLGroupOfProduct.Add(plGroup);

                                    if (group.PriceOfGOPID == iTon)
                                        plGroup.Quantity = group.Ton;
                                    else
                                        if (group.PriceOfGOPID == iCBM)
                                            plGroup.Quantity = group.CBM;
                                        else
                                            if (group.PriceOfGOPID == iTU)
                                                plGroup.Quantity = group.Quantity;

                                    if (exchangeQuantity.HasValue)
                                    {
                                        DTOCATContractGroupOfProduct itemChange = new DTOCATContractGroupOfProduct();
                                        itemChange.Ton = group.Ton;
                                        itemChange.CBM = group.CBM;
                                        itemChange.Quantity = group.Quantity;
                                        itemChange.Expression = exchange.Expression;
                                        exchangeQuantity = GetGroupOfProductTransfer(itemChange);

                                        if (exchangeQuantity.HasValue)
                                            plGroup.Quantity = exchangeQuantity.Value;
                                    }

                                    Price += plGroup.UnitPrice * (decimal)plGroup.Quantity;
                                }
                            }
                        }
                    }
                    #endregion
                }
            }

            return Price;
        }

        /// <summary>
        /// Tính chi phí bốc xếp cho vendor
        /// </summary>
        /// <param name="model"></param>
        /// <param name="master"></param>
        /// <param name="lstGroupProduct"></param>
        /// <param name="contractID"></param>
        /// <param name="effectDate"></param>
        /// <param name="TypeOfOrderID"></param>
        /// <returns></returns>
        private static decimal DITOMaster_GetPriceLoadByContract(DataEntities model, OPS_DITOMaster master, List<DTOFINGroupProduct> lstGroupProduct, int? contractID, DateTime effectDate, int? TypeOfOrderID, FIN_PL pl)
        {
            decimal Price = 0;
            // lấy hợp đồng
            var contract = model.CAT_Contract.FirstOrDefault(c => c.ID == contractID);
            if (contract != null)
            {
                // Lấy bảng giá
                var price = model.CAT_Price.Where(c => c.CAT_ContractTerm.ContractID == contractID && c.EffectDate <= effectDate && c.TypeOfOrderID == TypeOfOrderID).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                if (price != null)
                {
                    // Bốc xếp lên
                    FIN_PLDetails plLoad = new FIN_PLDetails();
                    plLoad.CreatedBy = pl.CreatedBy;
                    plLoad.CreatedDate = DateTime.Now;
                    plLoad.CostID = (int)CATCostType.DITOLoadDebit;
                    // Bốc xếp xuống
                    FIN_PLDetails plUnLoad = new FIN_PLDetails();
                    plUnLoad.CreatedBy = pl.CreatedBy;
                    plUnLoad.CreatedDate = DateTime.Now;
                    plUnLoad.CostID = (int)CATCostType.DITOUnLoadDebit;

                    // Tính doanh thu bốc xếp cho từng nhóm SP
                    foreach (var group in lstGroupProduct)
                    {
                        // Bốc xếp lên
                        var priceLoad = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.CAT_PriceDILoad.PriceID == price.ID && c.CAT_PriceDILoad.IsLoading && c.CAT_PriceDILoad.LocationID == group.LocationFromID && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID) && (c.CAT_PriceDILoad.LocationID == group.LocationFromID || c.CAT_PriceDILoad.RoutingID == group.RoutingID || (group.ParentRoutingID.HasValue && c.CAT_PriceDILoad.ParentRoutingID == group.ParentRoutingID) || (group.GroupOfLocationFromID.HasValue && c.CAT_PriceDILoad.GroupOfLocationID == group.GroupOfLocationFromID)));
                        if (priceLoad != null)
                        {
                            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                            plGroup.CreatedBy = pl.CreatedBy;
                            plGroup.CreatedDate = DateTime.Now;
                            plGroup.GroupOfProductID = group.ID;
                            plGroup.Unit = priceLoad.SYS_Var.ValueOfVar;
                            plGroup.UnitPrice = priceLoad.Price;
                            plLoad.FIN_PLGroupOfProduct.Add(plGroup);
                            // Tính theo Tấn
                            if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTon)
                                plGroup.Quantity = group.Ton;
                            else if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPCBM)
                                plGroup.Quantity = group.CBM;
                            else if (priceLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTU)
                                plGroup.Quantity = group.Quantity;

                            Price += (decimal)plGroup.Quantity * priceLoad.Price;
                            plLoad.Debit = Price;
                            if (plLoad.FIN_PLGroupOfProduct.Count > 0)
                                pl.FIN_PLDetails.Add(plLoad);
                        }

                        // Bốc xếp xuống
                        var priceUnLoad = model.CAT_PriceDILoadDetail.FirstOrDefault(c => c.CAT_PriceDILoad.PriceID == price.ID && !c.CAT_PriceDILoad.IsLoading && c.CUS_GroupOfProduct.CUS_GroupOfProductMapping1.Any(d => d.GroupOfProductCUSID == group.GroupOfProductID) && (c.CAT_PriceDILoad.LocationID == group.LocationToID || c.CAT_PriceDILoad.RoutingID == group.RoutingID || (group.ParentRoutingID.HasValue && c.CAT_PriceDILoad.ParentRoutingID == group.ParentRoutingID) || (group.GroupOfLocationToID.HasValue && c.CAT_PriceDILoad.GroupOfLocationID == group.GroupOfLocationToID)));
                        if (priceUnLoad != null)
                        {
                            FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                            plGroup.CreatedBy = pl.CreatedBy;
                            plGroup.CreatedDate = DateTime.Now;
                            plGroup.GroupOfProductID = group.ID;
                            plGroup.Unit = priceUnLoad.SYS_Var.ValueOfVar;
                            plGroup.UnitPrice = priceUnLoad.Price;
                            plUnLoad.FIN_PLGroupOfProduct.Add(plGroup);
                            // Tính theo Tấn
                            if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTon)
                                plGroup.Quantity = group.Ton;
                            else if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPCBM)
                                plGroup.Quantity = group.CBM;
                            else if (priceUnLoad.PriceOfGOPID == -(int)SYSVarType.PriceOfGOPTU)
                                plGroup.Quantity = group.Quantity;

                            Price += (decimal)plGroup.Quantity * priceUnLoad.Price;
                            plUnLoad.Debit = Price;
                            if (plUnLoad.FIN_PLGroupOfProduct.Count > 0)
                                pl.FIN_PLDetails.Add(plUnLoad);
                        }
                    }
                }
            }

            return Price;
        }

        private static decimal DITOMaster_GetPriceExByContract(DataEntities model, AccountItem Account, FIN_PL pl, OPS_DITOMaster master, List<DTOFINGroupProduct> lstGroupProduct, int? contractID, DateTime effectDate, int? TypeOfOrderID)
        {
            decimal Price = 0;
            //const int iTon = -(int)SYSVarType.PriceOfGOPTon;
            //const int iCBM = -(int)SYSVarType.PriceOfGOPCBM;
            //const int iQuantity = -(int)SYSVarType.PriceOfGOPTU;
            //const int iGroupComplete = -(int)SYSVarType.DITOGroupProductStatusComplete;
            //var priceOfGOPTonName = model.SYS_Var.FirstOrDefault(c => c.ID == iTon).ValueOfVar;
            //var priceOfGOPCBMName = model.SYS_Var.FirstOrDefault(c => c.ID == iCBM).ValueOfVar;
            //var priceOfGOPQuantityName = model.SYS_Var.FirstOrDefault(c => c.ID == iQuantity).ValueOfVar;
            //List<int> lstMasterID = new List<int>();
            //// Đối tượng dùng để tính trong công thức
            //DTOPriceDIExExpr objExpr = new DTOPriceDIExExpr();

            //objExpr.GOVCodeOrder = master.GroupOfVehicleID.HasValue ? master.CAT_GroupOfVehicle.Code : string.Empty;
            //int priceTotalID = -1;
            //bool isTotal = false;
            //var price = model.CAT_Price.Where(c => c.CAT_ContractTerm.ContractID == contractID && c.EffectDate <= effectDate && c.TypeOfOrderID == master.TypeOfOrderID).OrderByDescending(c => c.EffectDate).FirstOrDefault();
            //if (price != null)
            //{
            //    priceTotalID = price.ID;
            //    if (model.CAT_PriceDIEx.Count(c => c.PriceID == price.ID && (c.DIExSumID == -(int)SYSVarType.DIExScheduleInDay || c.DIExSumID == -(int)SYSVarType.DIExSumOrderInDay)) > 0)
            //    {
            //        DateTime dtConfig = effectDate.Date;
            //        isTotal = true;
            //        var lstCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
            //        lstMasterID = model.OPS_DITOGroupProduct.Where(c => lstCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.DITOMasterID.HasValue && c.OPS_DITOMaster.VendorOfVehicleID == master.VendorOfVehicleID && c.OPS_DITOMaster.TransportModeID == master.TransportModeID && c.OPS_DITOMaster.ContractID == master.ContractID && DbFunctions.TruncateTime(c.OPS_DITOMaster.DateConfig) == dtConfig && c.OPS_DITOMaster.TypeOfOrderID == master.TypeOfOrderID && c.DITOGroupProductStatusID >= iGroupComplete).Select(c => c.DITOMasterID.Value).Distinct().ToList();
            //    }
            //    else
            //        lstMasterID.Add(master.ID);
            //}

            //if (priceTotalID > 0)
            //{
            //    var objPrice = model.CAT_Price.FirstOrDefault(c => c.ID == priceTotalID);
            //    // Lấy thiết lập phụ thu
            //    var lstDIEx = model.CAT_PriceDIEx.Where(c => c.PriceID == priceTotalID && !string.IsNullOrEmpty(c.ExprInput));
            //    List<DTOFINGroupProduct> lstGroupOPS = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && lstMasterID.Contains(c.DITOMasterID.Value) && c.DITOGroupProductStatusID == iGroupComplete && c.ORD_GroupProduct.CUSRoutingID.HasValue).Select(c => new DTOFINGroupProduct
            //        {
            //            ID = c.ID,
            //            OrderID = c.ORD_GroupProduct.OrderID,
            //            GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID.HasValue ? c.ORD_GroupProduct.GroupOfProductID.Value : -1,
            //            LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
            //            LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
            //            GroupOfLocationFromID = c.ORD_GroupProduct.CUS_Location.CAT_Location.GroupOfLocationID,
            //            GroupOfLocationToID = c.ORD_GroupProduct.CUS_Location1.CAT_Location.GroupOfLocationID,
            //            PackingID = c.ORD_GroupProduct.PackingID,
            //            PriceOfGOPID = c.ORD_GroupProduct.PriceOfGOPID,
            //            RoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
            //            ParentRoutingID = c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID.HasValue ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.ParentID.Value : -1,
            //            Ton = c.ORD_GroupProduct.Ton,
            //            CBM = c.ORD_GroupProduct.CBM,
            //            Quantity = c.ORD_GroupProduct.Quantity,
            //            TonTranfer = c.TonTranfer,
            //            CBMTranfer = c.CBMTranfer,
            //            QuantityTranfer = c.QuantityTranfer,
            //            PriceOfGOPName = c.ORD_GroupProduct.CUS_GroupOfProduct.SYS_Var.ValueOfVar,
            //            DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.DateConfig,
            //            OrderGroupProductID = c.OrderGroupProductID.Value,
            //            DITOMasterID = c.DITOMasterID
            //        }).ToList();

            //    foreach (var diEx in lstDIEx)
            //    {
            //        // Tính theo Parent Routing
            //        var lstParentRoutingID = model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == diEx.ID && c.ParentRoutingID.HasValue).Select(c => c.ParentRoutingID.Value).Distinct().ToList();
            //        if (lstParentRoutingID.Count > 0)
            //            lstGroupOPS = lstGroupOPS.Where(c => c.ParentRoutingID.HasValue && lstParentRoutingID.Contains(c.ParentRoutingID.Value)).ToList();

            //        // Tính theo Routing
            //        var lstRoutingID = model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == diEx.ID && c.RoutingID.HasValue).Select(c => c.RoutingID.Value).Distinct().ToList();
            //        if (lstRoutingID.Count > 0)
            //            lstGroupOPS = lstGroupOPS.Where(c => lstRoutingID.Contains(c.RoutingID)).ToList();

            //        // Tính theo Group Location
            //        var lstGroupLocationID = model.CAT_PriceDIExGroupLocation.Where(c => c.PriceDIExID == diEx.ID).Select(c => c.GroupOfLocationID).Distinct().ToList();
            //        if (lstGroupLocationID.Count > 0)
            //            lstGroupOPS = lstGroupOPS.Where(c => ((c.GroupOfLocationFromID.HasValue && lstGroupLocationID.Contains(c.GroupOfLocationFromID.Value)) || (c.GroupOfLocationToID.HasValue && lstGroupLocationID.Contains(c.GroupOfLocationToID.Value)))).ToList();

            //        // Tính theo LocationFrom
            //        var lstLocationFromID = model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == diEx.ID && c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGet).Select(c => c.LocationID.Value).Distinct().ToList();
            //        if (lstLocationFromID.Count > 0)
            //            lstGroupOPS = lstGroupOPS.Where(c => lstLocationFromID.Contains(c.LocationFromID)).ToList();

            //        // Tính theo LocationTo
            //        var lstLocationToID = model.CAT_PriceDIExRouting.Where(c => c.PriceDIExID == diEx.ID && (c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationDelivery || c.TypeOfTOLocationID == -(int)SYSVarType.TypeOfTOLocationGetDelivery)).Select(c => c.LocationID.Value).Distinct().ToList();
            //        if (lstLocationToID.Count > 0)
            //            lstGroupOPS = lstGroupOPS.Where(c => lstLocationToID.Contains(c.LocationToID)).ToList();

            //        #region Tính theo GroupOfProduct
            //        var lstGroupProductEx = model.CAT_PriceDIExGroupProduct.Where(c => c.PriceDIExID == diEx.ID);
            //        if (lstGroupProductEx.Count() > 0)
            //        {
            //            foreach (var group in lstGroupProductEx)
            //            {
            //                var lstMappingID = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == objPrice.CAT_Contract.CustomerID && c.GroupOfProductVENID == group.GroupOfProductID).Select(c => c.GroupOfProductCUSID).Distinct().ToList();
            //                // Lấy dữ liệu đầu vào để tính công thức
            //                var lstGroupOPSTemp = lstGroupOPS.Where(c => lstMappingID.Contains(c.GroupOfProductID)).ToList();
            //                if (lstGroupOPSTemp.Count > 0)
            //                {
            //                    // Qui đổi nếu có
            //                    objExpr.GetPoint = lstGroupOPSTemp.Select(c => c.LocationFromID).Distinct().ToList().Count;
            //                    objExpr.DropPoint = lstGroupOPSTemp.Select(c => c.LocationToID).Distinct().ToList().Count;
            //                    objExpr = DITOMaster_MasterExchangeQuantity(model, objPrice.ContractID, lstGroupOPSTemp, objExpr);
            //                    // Tính giá phụ thu
            //                    DIPriceEx_GetPriceGOP(model, false, objExpr, diEx, group, pl);
            //                }
            //            }
            //        }
            //        else
            //        {
            //            if (lstGroupOPS.Count > 0)
            //            {
            //                // Qui đổi nếu có
            //                objExpr.GetPoint = lstGroupOPS.Select(c => c.LocationFromID).Distinct().ToList().Count;
            //                objExpr.DropPoint = lstGroupOPS.Select(c => c.LocationToID).Distinct().ToList().Count;
            //                objExpr = DITOMaster_MasterExchangeQuantity(model, objPrice.ContractID, lstGroupOPS, objExpr);
            //                // Tính giá phụ thu
            //                DIPriceEx_GetPrice(model, false, objExpr, diEx, pl, isTotal);
            //            }
            //        }
            //        #endregion
            //    }
            //}

            return Price;
        }

        #endregion

        #region Mobile
        public static List<DTOFINMobile_Profit> Truck_GetProfit(DataEntities model, List<int> lstMasterID)
        {
            List<DTOFINMobile_Profit> lstProfit = new List<DTOFINMobile_Profit>();

            foreach (var itemMasterID in lstMasterID)
            {
                var lstFIN = model.FIN_PL.Where(c => c.DITOMasterID == itemMasterID && c.FINPLTypeID == -(int)SYSVarType.FINPLTypePL).Select(c => new { c.Credit, c.Debit }).ToList();
                DTOFINMobile_Profit itemProfit = new DTOFINMobile_Profit();
                itemProfit.DITOMasterID = itemMasterID;
                itemProfit.Credit = lstFIN.Sum(c => c.Credit);
                itemProfit.Debit = lstFIN.Sum(c => c.Debit);
                lstProfit.Add(itemProfit);
            }

            return lstProfit;
        }
        #endregion

        #region Common
        public static List<DTOFINGroupProduct> DITOMaster_GetGroupOfProduct(DataEntities model, OPS_DITOMaster master)
        {
            // Lấy ds Nhóm SP thuộc lệnh master
            var lstOrder = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == master.ID).Select(c => new
            {
                ID = c.ID,
                OrderID = c.ORD_GroupProduct.OrderID,
                GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID,
                LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                PackingID = c.ORD_GroupProduct.PackingID,
                PriceOfGOPID = c.ORD_GroupProduct.PriceOfGOPID,
                RoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                Ton = c.TonTranfer,
                CBM = c.CBMTranfer,
                Quantity = c.QuantityTranfer,
                DateConfig = c.ORD_GroupProduct.DateConfig.HasValue ? c.ORD_GroupProduct.DateConfig.Value : c.ORD_GroupProduct.ORD_Order.DateConfig,
            }).ToList();
            // Ds Nhóm SP
            var lstGroupProduct = lstOrder.Select(c => new DTOFINGroupProduct
            {
                ID = c.ID,
                GroupOfProductID = c.GroupOfProductID.Value,
                LocationFromID = c.LocationFromID,
                LocationToID = c.LocationToID,
                PackingID = c.PackingID,
                PriceOfGOPID = c.PriceOfGOPID,
                RoutingID = c.RoutingID,
                Ton = c.Ton,
                CBM = c.CBM,
                Quantity = c.Quantity,
                DateConfig = c.DateConfig,
            }).Distinct().ToList();
            return lstGroupProduct;
        }

        private static double? GetGroupOfProductTransfer(DTOCATContractGroupOfProduct item)
        {
            try
            {
                double? result = null;

                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                Dictionary<string, string> dicEx = new Dictionary<string, string>();

                StringBuilder strExp = new StringBuilder(item.Expression);
                if (string.IsNullOrEmpty(item.Expression))
                    return 0;

                row++;
                worksheet.Cells[row, col].Value = item.OrderTon;
                strExp.Replace("[OrderTon]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderCBM;
                strExp.Replace("[OrderCBM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderQuantity;
                strExp.Replace("[OrderQuantity]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Ton;
                strExp.Replace("[Ton]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBM;
                strExp.Replace("[CBM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Quantity;
                strExp.Replace("[Quantity]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();
                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                {
                    result = Convert.ToDouble(val);
                }
                catch
                {
                    result = null;
                }

                return result;
            }
            catch
            {
                throw null;
            }
        }

        private static bool GetGroupOfProductTransfer_Check(DTOCATContractGroupOfProduct item)
        {
            try
            {
                bool result = false;

                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";
                Dictionary<string, string> dicEx = new Dictionary<string, string>();

                StringBuilder strExp = new StringBuilder(item.ExpressionInput);
                if (string.IsNullOrEmpty(item.ExpressionInput))
                    return false;

                row++;
                worksheet.Cells[row, col].Value = item.OrderTon;
                strExp.Replace("[OrderTon]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderCBM;
                strExp.Replace("[OrderCBM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.OrderQuantity;
                strExp.Replace("[OrderQuantity]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Ton;
                strExp.Replace("[Ton]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.CBM;
                strExp.Replace("[CBM]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.Quantity;
                strExp.Replace("[Quantity]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp.ToString();
                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                {
                    result = Convert.ToBoolean(val);
                }
                catch
                {
                    result = false;
                }

                return result;
            }
            catch
            {
                throw null;
            }
        }

        private static CAT_Contract GetContractByMasterID(DataEntities model, int masterID, DateTime EffectDate)
        {
            try
            {
                CAT_Contract result = null;

                var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == masterID);
                if (master != null)
                {
                    if (master.ContractID.HasValue)
                        result = master.CAT_Contract;
                    else
                    {
                        var lstCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == masterID && c.OrderGroupProductID.HasValue).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                        var contract = model.CAT_Contract.Where(c => c.CustomerID == master.VendorOfVehicleID && c.TransportModeID == master.TransportModeID && c.TypeOfContractID == -(int)SYSVarType.TypeOfContractMain && c.EffectDate <= EffectDate && (c.ExpiredDate == null || c.ExpiredDate >= EffectDate) && (c.CompanyID == null || (c.CompanyID.HasValue && lstCustomerID.Contains(c.CUS_Company.CustomerRelateID)))).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                        result = contract;
                    }
                }
                return result;
            }
            catch
            {
                throw null;
            }
        }


        private static void CopyFinPL(FIN_PL plSource, FIN_PL plDes)
        {
            plDes.CreatedBy = plSource.CreatedBy;
            plDes.CreatedDate = plSource.CreatedDate;
            plDes.Code = plSource.Code;
            plDes.ContractID = plSource.ContractID;
            plDes.COTOMasterID = plSource.COTOMasterID;
            plDes.Credit = plSource.Credit;
            plDes.CustomerID = plSource.CustomerID;
            plDes.Debit = plSource.Debit;
            plDes.DITOMasterID = plSource.DITOMasterID;
            plDes.DriverID = plSource.DriverID;
            plDes.Effdate = plSource.Effdate;
            plDes.FINPLTypeID = plSource.FINPLTypeID;
            plDes.OrderID = plSource.OrderID;
            plDes.ScheduleID = plSource.ScheduleID;
            plDes.SYSCustomerID = plSource.SYSCustomerID;
            plDes.VehicleID = plSource.VehicleID;
            plDes.VendorID = plSource.VendorID;

            foreach (var itemDetail in plSource.FIN_PLDetails)
            {
                FIN_PLDetails plCost = new FIN_PLDetails();
                plCost.CreatedBy = itemDetail.CreatedBy;
                plCost.CreatedDate = itemDetail.CreatedDate;
                plCost.CostID = itemDetail.CostID;
                plCost.Credit = itemDetail.Credit;
                plCost.Debit = itemDetail.Debit;
                plCost.Note = itemDetail.Note;
                plCost.Note1 = itemDetail.Note1;
                plCost.Note2 = itemDetail.Note2;
                plCost.Quantity = itemDetail.Quantity;
                plCost.UnitPrice = itemDetail.UnitPrice;
                plCost.Unit = itemDetail.Unit;
                plCost.TypeOfPriceDIExCode = itemDetail.TypeOfPriceDIExCode;
                plDes.FIN_PLDetails.Add(plCost);

                foreach (var itemGroup in itemDetail.FIN_PLGroupOfProduct)
                {
                    FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                    plGroup.CreatedBy = itemGroup.CreatedBy;
                    plGroup.CreatedDate = itemGroup.CreatedDate;
                    plGroup.GroupOfProductID = itemGroup.GroupOfProductID;
                    plGroup.Quantity = itemGroup.Quantity;
                    plGroup.QuantityMOQ = itemGroup.QuantityMOQ;
                    plGroup.UnitPrice = itemGroup.UnitPrice;
                    plGroup.Unit = itemGroup.Unit;
                    plCost.FIN_PLGroupOfProduct.Add(plGroup);
                }

                foreach (var itemContainer in itemDetail.FIN_PLContainer)
                {
                    FIN_PLContainer plContainer = new FIN_PLContainer();
                    plContainer.CreatedBy = itemContainer.CreatedBy;
                    plContainer.CreatedDate = itemContainer.CreatedDate;
                    plContainer.COTOContainerID = itemContainer.COTOContainerID;
                    plContainer.Quantity = itemContainer.Quantity;
                    plContainer.UnitPrice = itemContainer.UnitPrice;
                    plContainer.Unit = itemContainer.Unit;
                    plCost.FIN_PLContainer.Add(plContainer);
                }
            }
        }

        private static void CopyFinPL_FLM(FIN_PL plSource, FIN_PL plDes, AccountItem Account)
        {
            if (plSource.VendorID == Account.SYSCustomerID)
            {
                plDes.CreatedBy = plSource.CreatedBy;
                plDes.CreatedDate = plSource.CreatedDate;
                plDes.Code = plSource.Code;
                plDes.ContractID = plSource.ContractID;
                plDes.COTOMasterID = plSource.COTOMasterID;
                plDes.Credit = plSource.Credit;
                plDes.CustomerID = plSource.CustomerID;
                plDes.Debit = plSource.Debit;
                plDes.DITOMasterID = plSource.DITOMasterID;
                plDes.DriverID = plSource.DriverID;
                plDes.Effdate = plSource.Effdate;
                plDes.FINPLTypeID = plSource.FINPLTypeID;
                plDes.OrderID = plSource.OrderID;
                plDes.ScheduleID = plSource.ScheduleID;
                plDes.SYSCustomerID = plSource.SYSCustomerID;
                plDes.VehicleID = plSource.VehicleID;
                plDes.VendorID = plSource.VendorID;

                foreach (var itemDetail in plSource.FIN_PLDetails)
                {
                    FIN_PLDetails plCost = new FIN_PLDetails();
                    plCost.CreatedBy = itemDetail.CreatedBy;
                    plCost.CreatedDate = itemDetail.CreatedDate;
                    plCost.Credit = itemDetail.Credit;
                    plCost.Debit = itemDetail.Debit;
                    plCost.Note = itemDetail.Note;
                    plCost.Note1 = itemDetail.Note1;
                    plCost.Note2 = itemDetail.Note2;
                    plCost.Quantity = itemDetail.Quantity;
                    plCost.UnitPrice = itemDetail.UnitPrice;
                    plCost.Unit = itemDetail.Unit;
                    plCost.TypeOfPriceDIExCode = itemDetail.TypeOfPriceDIExCode;
                    plDes.FIN_PLDetails.Add(plCost);

                    switch (itemDetail.CostID)
                    {
                        case (int)CATCostType.ORDContainerServiceDebit:
                            plCost.CostID = (int)CATCostType.FLMORDContainerServiceCredit;
                            break;
                        case (int)CATCostType.ORDDocumentDebit:
                            plCost.CostID = (int)CATCostType.FLMORDDocumentCredit;
                            break;
                        case (int)CATCostType.COTOFreightDebit:
                            plCost.CostID = (int)CATCostType.FLMCOFreightCredit;
                            break;
                        case (int)CATCostType.ManualFixDebit:
                            plCost.CostID = (int)CATCostType.FLMManualFixCredit;
                            break;
                        case (int)CATCostType.DITOUnLoadReturnDebit:
                            plCost.CostID = (int)CATCostType.FLMDIUnLoadReturnCredit;
                            break;
                        case (int)CATCostType.DITOLoadReturnDebit:
                            plCost.CostID = (int)CATCostType.FLMDILoadReturnCredit;
                            break;
                        case (int)CATCostType.DITOExNoGroupDebit:
                            plCost.CostID = (int)CATCostType.FLMDIExNoGroupCredit;
                            break;
                        case (int)CATCostType.DITOExDebit:
                            plCost.CostID = (int)CATCostType.FLMDIExCredit;
                            break;
                        case (int)CATCostType.DITOMOQUnLoadNoGroupDebit:
                            plCost.CostID = (int)CATCostType.FLMDIMOQUnLoadNoGroupCredit;
                            break;
                        case (int)CATCostType.DITOMOQLoadNoGroupDebit:
                            plCost.CostID = (int)CATCostType.FLMDIMOQLoadNoGroupCredit;
                            break;
                        case (int)CATCostType.DITOMOQNoGroupDebit:
                            plCost.CostID = (int)CATCostType.FLMDIMOQNoGroupCredit;
                            break;
                        case (int)CATCostType.DITOFreightNoGroupDebit:
                            plCost.CostID = (int)CATCostType.FLMDIFreightNoGroupCredit;
                            break;
                        case (int)CATCostType.DITOReturnDebit:
                            plCost.CostID = (int)CATCostType.FLMDIReturnCredit;
                            break;
                        case (int)CATCostType.DITOUnLoadDebit:
                            plCost.CostID = (int)CATCostType.FLMDIUnLoadCredit;
                            break;
                        case (int)CATCostType.DITOLoadDebit:
                            plCost.CostID = (int)CATCostType.FLMDILoadCredit;
                            break;
                        case (int)CATCostType.DITOFreightDebit:
                            plCost.CostID = (int)CATCostType.FLMDIFreightCredit;
                            break;
                        case (int)CATCostType.TroubleDebit:
                            plCost.CostID = (int)CATCostType.FLMTroubleCredit;
                            break;
                        case (int)CATCostType.COTOExDebit:
                            plCost.CostID = (int)CATCostType.FLMCOExCredit;
                            break;
                    }

                    foreach (var itemGroup in itemDetail.FIN_PLGroupOfProduct)
                    {
                        FIN_PLGroupOfProduct plGroup = new FIN_PLGroupOfProduct();
                        plGroup.CreatedBy = itemGroup.CreatedBy;
                        plGroup.CreatedDate = itemGroup.CreatedDate;
                        plGroup.GroupOfProductID = itemGroup.GroupOfProductID;
                        plGroup.Quantity = itemGroup.Quantity;
                        plGroup.QuantityMOQ = itemGroup.QuantityMOQ;
                        plGroup.UnitPrice = itemGroup.UnitPrice;
                        plGroup.Unit = itemGroup.Unit;
                        plCost.FIN_PLGroupOfProduct.Add(plGroup);
                    }

                    foreach (var itemContainer in itemDetail.FIN_PLContainer)
                    {
                        FIN_PLContainer plContainer = new FIN_PLContainer();
                        plContainer.CreatedBy = itemContainer.CreatedBy;
                        plContainer.CreatedDate = itemContainer.CreatedDate;
                        plContainer.COTOContainerID = itemContainer.COTOContainerID;
                        plContainer.Quantity = itemContainer.Quantity;
                        plContainer.UnitPrice = itemContainer.UnitPrice;
                        plContainer.Unit = itemContainer.Unit;
                        plCost.FIN_PLContainer.Add(plContainer);
                    }
                }
            }
        }

        private static DTOPriceDIExExpr Expr_Generate(List<HelperFinance_OPSGroupProduct> lstOPSGroup)
        {
            DTOPriceDIExExpr itemExpr = new DTOPriceDIExExpr();
            itemExpr.TonOrder = lstOPSGroup.Sum(c => c.TonOrder);
            itemExpr.CBMOrder = lstOPSGroup.Sum(c => c.CBMOrder);
            itemExpr.QuantityOrder = lstOPSGroup.Sum(c => c.QuantityOrder);
            itemExpr.TonTransfer = lstOPSGroup.Sum(c => c.TonTranfer);
            itemExpr.CBMTransfer = lstOPSGroup.Sum(c => c.CBMTranfer);
            itemExpr.QuantityTransfer = lstOPSGroup.Sum(c => c.QuantityTranfer);
            itemExpr.TonActual = lstOPSGroup.Sum(c => c.TonActual);
            itemExpr.CBMActual = lstOPSGroup.Sum(c => c.CBMActual);
            itemExpr.QuantityActual = lstOPSGroup.Sum(c => c.QuantityActual);
            itemExpr.TonBBGN = lstOPSGroup.Sum(c => c.TonBBGN);
            itemExpr.CBMBBGN = lstOPSGroup.Sum(c => c.CBMBBGN);
            itemExpr.QuantityBBGN = lstOPSGroup.Sum(c => c.QuantityBBGN);
            itemExpr.TonReturn = lstOPSGroup.Sum(c => c.TonReturn);
            itemExpr.CBMReturn = lstOPSGroup.Sum(c => c.CBMReturn);
            itemExpr.QuantityReturn = lstOPSGroup.Sum(c => c.QuantityReturn);
            itemExpr.TotalSchedule = lstOPSGroup.Select(c => c.DITOMasterID).Distinct().Count();
            itemExpr.GetPoint = lstOPSGroup.Select(c => c.LocationFromID).Distinct().Count();
            itemExpr.DropPoint = lstOPSGroup.Select(c => c.LocationToID).Distinct().Count();
            itemExpr.GOVCodeOrder = lstOPSGroup.FirstOrDefault().GOVCode;
            itemExpr.GOVCodeSchedule = lstOPSGroup.FirstOrDefault().GroupOfVehicleCode;
            itemExpr.VehicleCode = lstOPSGroup.FirstOrDefault().VehicleCode;
            itemExpr.DropPointReturn = lstOPSGroup.Where(c => (c.TonReturn > 0 || c.CBMReturn > 0 || c.QuantityReturn > 0)).Select(c => c.LocationToID).Distinct().Count();
            itemExpr.TotalOrder = lstOPSGroup.Select(c => c.OrderID).Distinct().Count();
            itemExpr.RoutingCode = string.Join(",", lstOPSGroup.Select(c => c.CATRoutingCode).Distinct().ToList());
            itemExpr.HasCashCollect = lstOPSGroup.Any(c => c.HasCashCollect);
            itemExpr.SortConfig = lstOPSGroup.FirstOrDefault().SortConfigMaster;
            return itemExpr;
        }

        private static DTOPriceDIExExpr Expr_GenerateItem(HelperFinance_OPSGroupProduct item)
        {
            DTOPriceDIExExpr itemExpr = new DTOPriceDIExExpr();
            itemExpr.TonOrder = item.TonOrder;
            itemExpr.CBMOrder = item.CBMOrder;
            itemExpr.QuantityOrder = item.QuantityOrder;
            itemExpr.TonTransfer = item.TonTranfer;
            itemExpr.CBMTransfer = item.CBMTranfer;
            itemExpr.QuantityTransfer = item.QuantityTranfer;
            itemExpr.TonActual = item.TonActual;
            itemExpr.CBMActual = item.CBMActual;
            itemExpr.QuantityActual = item.QuantityActual;
            itemExpr.TonBBGN = item.TonBBGN;
            itemExpr.CBMBBGN = item.CBMBBGN;
            itemExpr.QuantityBBGN = item.QuantityBBGN;
            itemExpr.TonReturn = item.TonReturn;
            itemExpr.CBMReturn = item.CBMReturn;
            itemExpr.QuantityReturn = item.QuantityReturn;
            itemExpr.TotalSchedule = 1;
            itemExpr.DropPoint = 1;
            itemExpr.GetPoint = 1;
            itemExpr.GOVCodeOrder = item.GOVCode;
            itemExpr.GOVCodeSchedule = item.GroupOfVehicleCode;
            itemExpr.VehicleCode = item.VehicleCode;
            itemExpr.DropPointReturn = (item.TonReturn > 0 || item.CBMReturn > 0 || item.QuantityReturn > 0) ? 1 : 0;
            itemExpr.TotalOrder = 1;
            itemExpr.RoutingCode = item.CATRoutingCode;
            itemExpr.HasCashCollect = item.HasCashCollect;
            return itemExpr;
        }
        #endregion
    }

    #region DTO

    public class HelperFinance_Order
    {
        public int ID { get; set; }
        public int CustomerID { get; set; }
        public int GroupOfVehicleID { get; set; }
        public int ContractID { get; set; }
        public int TypeOfContractID { get; set; }
        public int CATRoutingID { get; set; }
        public int ParentRoutingID { get; set; }
        public int GetPoint { get; set; }
        public int DropPoint { get; set; }
        public double TonOrder { get; set; }
        public double CBMOrder { get; set; }
        public double QuantityOrder { get; set; }
        public double TonTranfer { get; set; }
        public double CBMTranfer { get; set; }
        public double QuantityTranfer { get; set; }
        public double TonActual { get; set; }
        public double CBMActual { get; set; }
        public double QuantityActual { get; set; }
        public double TonBBGN { get; set; }
        public double CBMBBGN { get; set; }
        public double QuantityBBGN { get; set; }
        public double TonReturn { get; set; }
        public double CBMReturn { get; set; }
        public double QuantityReturn { get; set; }
        public decimal Price { get; set; }
        public decimal PriceMOQ { get; set; }
        public decimal PriceEx { get; set; }
        public decimal PriceLoad { get; set; }
        public decimal PriceUnLoad { get; set; }
        public bool HasMOQ { get; set; }
        public bool HasMOQLoad { get; set; }
        public bool HasMOQUnLoad { get; set; }
        public int TransportModeID { get; set; }
        public DateTime DateConfig { get; set; }
        public bool IsLoading { get; set; }
        public bool IsUnLoading { get; set; }
        public string MOQLoadingName { get; set; }
        public string MOQUnLoadingName { get; set; }
        public string GroupOfVehicleCode { get; set; }
        public int DropPointCurrent { get; set; }
        public int GetPointCurrent { get; set; }
        public string OrderCode { get; set; }
        public decimal PriceManual { get; set; }
        public int? ContractTermID { get; set; }
        public int? SortConfig { get; set; }
        public int? ServiceOfOrderID { get; set; }
    }

    public class HelperFinance_TOMaster
    {
        public double MaxWeightCal { get; set; }
        public int ID { get; set; }
        public int VendorID { get; set; }
        public int? VehicleID { get; set; }
        public string VehicleCode { get; set; }
        public string GOVCode { get; set; }
        public int? AssetID { get; set; }
        public int GroupOfVehicleID { get; set; }
        public string GroupOfVehicleCode { get; set; }
        public int ContractID { get; set; }
        public int TypeOfContractID { get; set; }
        public int CATRoutingID { get; set; }
        public int ParentRoutingID { get; set; }
        public int GetPoint { get; set; }
        public int DropPoint { get; set; }
        public double TonOrder { get; set; }
        public double CBMOrder { get; set; }
        public double QuantityOrder { get; set; }
        public double TonTranfer { get; set; }
        public double CBMTranfer { get; set; }
        public double QuantityTranfer { get; set; }
        public double TonActual { get; set; }
        public double CBMActual { get; set; }
        public double QuantityActual { get; set; }
        public double TonReturn { get; set; }
        public double CBMReturn { get; set; }
        public double QuantityReturn { get; set; }
        public decimal Price { get; set; }
        public decimal PriceMOQ { get; set; }
        public decimal PriceEx { get; set; }
        public decimal PriceLoad { get; set; }
        public decimal PriceUnLoad { get; set; }
        public bool HasMOQ { get; set; }
        public bool HasMOQLoad { get; set; }
        public bool HasMOQUnLoad { get; set; }
        public int TransportModeID { get; set; }
        public DateTime DateConfig { get; set; }
        public bool IsLoading { get; set; }
        public bool IsUnLoading { get; set; }
        public string MOQLoadingName { get; set; }
        public string MOQUnLoadingName { get; set; }
        public int DropPointCurrent { get; set; }
        public int GetPointCurrent { get; set; }
        public int? DriverID1 { get; set; }
        public int? DriverID2 { get; set; }
        public int? DriverID3 { get; set; }
        public int? DriverID4 { get; set; }
        public int? DriverID5 { get; set; }
        public int? TypeOfDriverID1 { get; set; }
        public int? TypeOfDriverID2 { get; set; }
        public int? TypeOfDriverID3 { get; set; }
        public int? TypeOfDriverID4 { get; set; }
        public int? TypeOfDriverID5 { get; set; }
        public bool? ExIsOverNight { get; set; }
        public bool? ExIsOverWeight { get; set; }
        public double? ExTotalDayOut { get; set; }
        public double? ExTotalJoin { get; set; }
        public double? KM { get; set; }
        public DateTime? ETD { get; set; }
        public int SortOrder { get; set; }
        public int OPSGroupProductID { get; set; }
        public int OPSContainerID { get; set; }

        public int? PHTPackingID { get; set; }
        public string PackingCode { get; set; }
        public bool PHTLoading { get; set; }
        public int? PHTCustomerID { get; set; }
        public int? PHTGroupOfLocationID { get; set; }
        public int? SortConfig { get; set; }
        public int? ServiceOfOrderID { get; set; }
    }

    public class HelperFinance_COMaster
    {
        public double MaxWeightCal { get; set; }
        public int ID { get; set; }
        public int VendorID { get; set; }
        public int? VehicleID { get; set; }
        public string VehicleCode { get; set; }
        public string GOVCode { get; set; }
        public int? AssetID { get; set; }
        public int GroupOfVehicleID { get; set; }
        public string GroupOfVehicleCode { get; set; }
        public int ContractID { get; set; }
        public int TypeOfContractID { get; set; }
        public int CATRoutingID { get; set; }
        public int ParentRoutingID { get; set; }
        public int GetPoint { get; set; }
        public int DropPoint { get; set; }
        public double TonOrder { get; set; }
        public double CBMOrder { get; set; }
        public double QuantityOrder { get; set; }
        public double TonTranfer { get; set; }
        public double CBMTranfer { get; set; }
        public double QuantityTranfer { get; set; }
        public double TonReturn { get; set; }
        public double CBMReturn { get; set; }
        public double QuantityReturn { get; set; }
        public decimal Price { get; set; }
        public decimal PriceMOQ { get; set; }
        public decimal PriceEx { get; set; }
        public decimal PriceLoad { get; set; }
        public decimal PriceUnLoad { get; set; }
        public bool HasMOQ { get; set; }
        public bool HasMOQLoad { get; set; }
        public bool HasMOQUnLoad { get; set; }
        public int TransportModeID { get; set; }
        public DateTime DateConfig { get; set; }
        public bool IsLoading { get; set; }
        public bool IsUnLoading { get; set; }
        public string MOQLoadingName { get; set; }
        public string MOQUnLoadingName { get; set; }
        public int DropPointCurrent { get; set; }
        public int GetPointCurrent { get; set; }
        public int? DriverID1 { get; set; }
        public int? DriverID2 { get; set; }
        public int? DriverID3 { get; set; }
        public int? DriverID4 { get; set; }
        public int? DriverID5 { get; set; }
        public int? TypeOfDriverID1 { get; set; }
        public int? TypeOfDriverID2 { get; set; }
        public int? TypeOfDriverID3 { get; set; }
        public int? TypeOfDriverID4 { get; set; }
        public int? TypeOfDriverID5 { get; set; }
        public bool? ExIsOverNight { get; set; }
        public bool? ExIsOverWeight { get; set; }
        public double? ExTotalDayOut { get; set; }
        public double? ExTotalJoin { get; set; }
        public double? KM { get; set; }
        public DateTime? ETD { get; set; }
        public int SortOrder { get; set; }
        public int OPSContainerID { get; set; }

        public int? PHTPackingID { get; set; }
        public bool PHTLoading { get; set; }
        public int? PHTCustomerID { get; set; }
        public int? PHTGroupOfLocationID { get; set; }
        public int? ServiceOfOrderID { get; set; }
    }

    public class HelperFinance_OPSGroupProduct
    {
        public double MaxWeightCal { get; set; }
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int OrderGroupProductID { get; set; }
        public int DITOMasterID { get; set; }
        public int? VehicleID { get; set; }
        public string VehicleCode { get; set; }
        public string GOVCode { get; set; }
        public int GroupOfProductID { get; set; }
        public int ContractID { get; set; }
        public int TypeOfContractID { get; set; }
        public int CUSRoutingID { get; set; }
        public int CATRoutingID { get; set; }
        public int ParentRoutingID { get; set; }
        public int PartnerID { get; set; }
        public int LocationFromID { get; set; }
        public int LocationToID { get; set; }
        public int LocationToProvinceID { get; set; }
        public int GroupOfLocationID { get; set; }
        public double TonOrder { get; set; }
        public double CBMOrder { get; set; }
        public double QuantityOrder { get; set; }
        public double TonTranfer { get; set; }
        public double CBMTranfer { get; set; }
        public double QuantityTranfer { get; set; }
        public double TonReturn { get; set; }
        public double CBMReturn { get; set; }
        public double QuantityReturn { get; set; }
        public double TonActual { get; set; }
        public double CBMActual { get; set; }
        public double QuantityActual { get; set; }
        public double TonBBGN { get; set; }
        public double CBMBBGN { get; set; }
        public double QuantityBBGN { get; set; }
        public double Ton { get; set; }
        public double CBM { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal PriceMOQ { get; set; }
        public double QuantityMOQ { get; set; }
        public decimal UnitPriceLoad { get; set; }
        public decimal UnitPriceUnLoad { get; set; }
        public decimal PriceLoad { get; set; }
        public decimal PriceUnLoad { get; set; }
        public double QuantityLoad { get; set; }
        public double QuantityUnLoad { get; set; }
        public bool HasMOQ { get; set; }
        public bool HasMOQLoad { get; set; }
        public bool HasMOQUnLoad { get; set; }
        public bool IsLoading { get; set; }
        public bool IsUnLoading { get; set; }
        public string MOQLoadingName { get; set; }
        public string MOQUnLoadingName { get; set; }
        public int? PriceOfGOPID { get; set; }
        public string PriceOfGOPName { get; set; }
        public int GroupOfVehicleID { get; set; }
        public int CustomerID { get; set; }
        public int VendorID { get; set; }
        public int OrderRoutingID { get; set; }
        public string LocationToName { get; set; }
        public string CATRoutingName { get; set; }
        public string CATRoutingCode { get; set; }
        public string GroupOfVehicleCode { get; set; }
        public string OrderCode { get; set; }

        public int? DriverID1 { get; set; }
        public int? DriverID2 { get; set; }
        public int? DriverID3 { get; set; }
        public int? TypeOfDriverID1 { get; set; }
        public int? TypeOfDriverID2 { get; set; }
        public int? TypeOfDriverID3 { get; set; }
        public int ProductID { get; set; }
        public bool HasCashCollect { get; set; }

        public DateTime? DateConfig { get; set; }
        public DateTime? ETD { get; set; }
        public int SortOrder { get; set; }

        public int? VendorLoadID { get; set; }
        public int? VendorUnLoadID { get; set; }

        public int? VendorLoadContractID { get; set; }
        public int? VendorUnLoadContractID { get; set; }
        public int? GroupOfProductLoadID { get; set; }
        public int? GroupOfProductUnLoadID { get; set; }
        public int? PriceOfGOPLoadID { get; set; }
        public int? PriceOfGOPUnLoadID { get; set; }
        public string PriceOfGOPLoadName { get; set; }
        public string PriceOfGOPUnLoadName { get; set; }
        public decimal PriceManual { get; set; }
        public bool IsReturn { get; set; }
        public int? SortConfigOrder { get; set; }
        public int? SortConfigMaster { get; set; }
    }

    public class HelperFinance_Container
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int OrderContainerID { get; set; }
        public int COTOMasterID { get; set; }
        public int PackingID { get; set; }
        public string PackingCode { get; set; }
        public int? VehicleID { get; set; }
        public string VehicleCode { get; set; }
        public string GOVCode { get; set; }
        public int GroupOfProductID { get; set; }
        public int ContractID { get; set; }
        public int TypeOfContractID { get; set; }
        public int CUSRoutingID { get; set; }
        public int CATRoutingID { get; set; }
        public int ParentRoutingID { get; set; }
        public int LocationFromID { get; set; }
        public int LocationToID { get; set; }
        public int GroupOfLocationID { get; set; }
        public int OrderRoutingID { get; set; }
        public string LocationToName { get; set; }
        public string CATRoutingName { get; set; }
        public string GroupOfVehicleCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int StatusOfCOContainerID { get; set; }
        public int COTOSort { get; set; }
        public int? DriverID1 { get; set; }
        public int? DriverID2 { get; set; }
        public int? DriverID3 { get; set; }
        public int? TypeOfDriverID1 { get; set; }
        public int? TypeOfDriverID2 { get; set; }
        public int? TypeOfDriverID3 { get; set; }
        public int CustomerID { get; set; }
        public int VendorID { get; set; }
        public DateTime? ETD { get; set; }
        public int SortOrder { get; set; }
        public int ServiceOfOrderID { get; set; }

        public string COTOLocationFromAddress { get; set; }
        public string COTOLocationToAddress { get; set; }

        public double MaxWeightCal { get; set; }
    }

    public class HelperFinance_COTO
    {
        public int ID { get; set; }
        public int COTOMasterID { get; set; }
        public int SortOrder { get; set; }
        public int ContractID { get; set; }
        public double KM { get; set; }
        public double Ton { get; set; }
        public int? DriverID1 { get; set; }
        public int? DriverID2 { get; set; }
        public int? DriverID3 { get; set; }
        public int? TypeOfDriverID1 { get; set; }
        public int? TypeOfDriverID2 { get; set; }
        public int? TypeOfDriverID3 { get; set; }
        public int? VehicleID { get; set; }
    }


    public class HelperFinance_ORDGroupProduct
    {
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int GroupOfProductID { get; set; }
        public int ContractID { get; set; }
        public int TypeOfContractID { get; set; }
        public int CUSRoutingID { get; set; }
        public int CATRoutingID { get; set; }
        public int ParentRoutingID { get; set; }
        public int PartnerID { get; set; }
        public int LocationFromID { get; set; }
        public int LocationToID { get; set; }
        public int GroupOfLocationID { get; set; }
        public double Ton { get; set; }
        public double CBM { get; set; }
        public double Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal LoadPrice { get; set; }
        public decimal UnLoadPrice { get; set; }
        public int? PriceOfGOPID { get; set; }
        public int GroupOfVehicleID { get; set; }
        public int ProductID { get; set; }
        public string FINSort { get; set; }
    }

    public class HelperFinance_Contract
    {
        public int ID { get; set; }
        public int VendorID { get; set; }
        public string VendorCode { get; set; }
        public string VendorName { get; set; }
        public int? TransportModeID { get; set; }
        public bool PriceInDay { get; set; }
        public int? CustomerID { get; set; }
        public int TypeOfContractDateID { get; set; }
        public int TypeOfContractID { get; set; }
        public DateTime EffectDate { get; set; }
        public int? TypeOfRunLevelID { get; set; }
        public int? TypeOfSGroupProductChangeID { get; set; }
        public string ExprFCLAllocationPrice { get; set; }
        public string ExprMaterialQuota { get; set; }
        public List<DTOCATContract_Setting> ListContractSetting { get; set; }
        public int? CompanyID { get; set; }
        public int? TypeOfContractQuantityID { get; set; }
    }

    public class HelperFinance_OPSContainer
    {
        public double MaxWeightCal { get; set; }
        public int? ContractTermID { get; set; }
        public int ID { get; set; }
        public int OrderID { get; set; }
        public int OrderContainerID { get; set; }
        public int? COTOMasterID { get; set; }
        public int? VehicleID { get; set; }
        public int PackingID { get; set; }
        public string PackingName { get; set; }
        public int ContractID { get; set; }
        public int TypeOfContractID { get; set; }
        public int OrderRoutingID { get; set; }
        public int CUSRoutingID { get; set; }
        public int CATRoutingID { get; set; }
        public int ParentRoutingID { get; set; }
        public int PartnerID { get; set; }
        public int LocationFromID { get; set; }
        public int LocationToID { get; set; }
        public int LocationDepotID { get; set; }
        public int LocationDepotReturnID { get; set; }
        public int CustomerID { get; set; }
        public int? VendorID { get; set; }
        public string OrderCode { get; set; }
        public decimal UnitPrice { get; set; }
        public int OPSContainerID { get; set; }
        public int? DriverID1 { get; set; }
        public int? DriverID2 { get; set; }
        public int? DriverID3 { get; set; }
        public int? TypeOfDriverID1 { get; set; }
        public int? TypeOfDriverID2 { get; set; }
        public int? TypeOfDriverID3 { get; set; }
        public DateTime? DateConfig { get; set; }
        public int SortOrder { get; set; }
        public int StatusOfCOContainerID { get; set; }
        public int ServiceOfOrderID { get; set; }
        public string FINSort { get; set; }
    }

    public class HelperFinance_FLMCheck
    {
        public string StatusOfAssetTimeSheetCode { get; set; }
        public decimal FeeBase { get; set; }
        public bool? ExIsOverNight { get; set; }
        public bool? ExIsOverWeight { get; set; }
        public double? ExTotalDayOut { get; set; }
        public double? ExTotalJoin { get; set; }
        public int TotalDriverMain { get; set; }
        public int TotalDriverEx { get; set; }
        public int TotalDriverLoad { get; set; }
        public int TotalSchedule { get; set; }
        public bool IsAssistant { get; set; }

        public double TotalKM { get; set; }
        public double TotalDay { get; set; }
        public double TotalDaySchedule { get; set; }
        public double TotalDayActual { get; set; }
        public double TotalDayOn { get; set; }
        public double TotalDayOff { get; set; }
        public double TotalDayHoliday { get; set; }
        public decimal Value { get; set; }
        public decimal Price { get; set; }

        public double TotalDayOnDriver { get; set; }
        public double TotalDayOffDriver { get; set; }
        public double TotalDayHolidayDriver { get; set; }
        public double TotalDayAllowOffDriver { get; set; }
        public double TotalDayAllowOffRemainDriver { get; set; }

        public double TotalTon { get; set; }
        public double TotalCBM { get; set; }

        public double TotalTonInDay { get; set; }
        public double TotalCBMInDay { get; set; }
        public int TotalScheduleInDay { get; set; }


        public double TotalTonInSchedule { get; set; }
        public double TotalCBMInSchedule { get; set; }

        public double QuantityOrder { get; set; }
        public double TonTransfer { get; set; }
        public double CBMTransfer { get; set; }
        public double QuantityTransfer { get; set; }
        public double TonReturn { get; set; }
        public double CBMReturn { get; set; }
        public double QuantityReturn { get; set; }

        public int DropPoint { get; set; }
        public int GetPoint { get; set; }
        public int SortInDay { get; set; }

        public bool IsDayOff { get; set; }
        public bool IsDayOn { get; set; }
        public bool IsDayHoliday { get; set; }
        public bool IsWorking { get; set; }
        public bool IsDaySchedule { get; set; }

        public bool? PHTLoading { get; set; }
        public string PackingCode { get; set; }
        public int TotalPacking { get; set; }
        public int TotalPackingInDay { get; set; }

        public double KM { get; set; }
        public double Ton { get; set; }
        public double Quota { get; set; }

        public int Con20DCLaden { get; set; }
        public int Con40DCLaden { get; set; }
        public int Con40HCLaden { get; set; }
        public int Con20DCEmpty { get; set; }
        public int Con40DCEmpty { get; set; }
        public int Con40HCEmpty { get; set; }
        public int TotalCOContainer { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalTOMaster { get; set; }
        public string VehicleCode { get; set; }
        public string GOVCode { get; set; }

        public double MaxWeightCal { get; set; }

        public List<HelperFinance_FLMCheckDetail> lstCheckDetail { get; set; }
    }

    public class HelperFinance_FLMCheckDetail
    {
        public int DriverID { get; set; }
        public int TypeOfScheduleFeeID { get; set; }
        public string TypeOfScheduleFeeCode { get; set; }
        public double TypeOfScheduleFeeDay { get; set; }
        public bool IsAssistant { get; set; }
    }

    public class HelperFinance_TOMasterSort
    {
        public int ID { get; set; }
        public DateTime? ETD { get; set; }
        public int VehicleID { get; set; }
        public int SortOrder { get; set; }
    }


    public class HelperFinance_PriceInput
    {
        public List<int> ListCutomerID { get; set; }

        public List<HelperFinance_Order> ListOrder { get; set; }
        public List<HelperFinance_TOMaster> ListTOMaster { get; set; }
        public List<HelperFinance_ORDGroupProduct> ListORDGroupProduct { get; set; }
        public List<HelperFinance_OPSGroupProduct> ListOPSGroupProduct { get; set; }

        public List<HelperFinance_COMaster> ListCOMaster { get; set; }
        public List<HelperFinance_OPSContainer> ListOPSContainer { get; set; }
        public List<HelperFinance_OPSContainer> ListContainer { get; set; }

        public List<int> ListContractID { get; set; }
        public List<HelperFinance_Contract> ListContract { get; set; }
        public List<HelperFinance_ContractTerm> ListContractTerm { get; set; }
        public List<HelperFinance_Price> ListPrice { get; set; }
        public List<HelperFinance_ContractGroupProduct> ListContractGroupProduct { get; set; }
        public List<HelperFinance_GroupProductMapping> ListGroupProductMapping { get; set; }

        public List<HelperFinance_PriceLTL> ListLTL { get; set; }
        public List<HelperFinance_PriceLTLLevel> ListLTLLevel { get; set; }
        public List<HelperFinance_PriceFTL> ListFTL { get; set; }
        public List<HelperFinance_PriceFTLLevel> ListFTLLevel { get; set; }
        public List<HelperFinance_PriceLoad> ListLoad { get; set; }
        public List<HelperFinance_PriceMOQ> ListMOQ { get; set; }
        public List<HelperFinance_PriceMOQLoad> ListMOQLoad { get; set; }
        public List<HelperFinance_PriceEx> ListEx { get; set; }
        public List<HelperFinance_PriceContainer> ListContainerPrice { get; set; }
        public List<HelperFinance_PriceService> ListContainerService { get; set; }
        public List<HelperFinance_DocumentContainer> ListDocumentContainer { get; set; }
        public List<HelperFinance_DocumentService> ListDocumentService { get; set; }

        public List<HelperFinance_ManualFix> ListManualFix { get; set; }
        public List<HelperFinance_Trouble> ListTrouble { get; set; }
        public List<HelperFinance_FINTemp> ListFINTemp { get; set; }
        public List<CATServiceOfOrder> ListServiceOfOrder { get; set; }

        public DTOSYSSetting SYSSetting { get; set; }

    }

    public class HelperFinance_Price
    {
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int ID { get; set; }
        public int TypeOfOrderID { get; set; }
        public int? TypeOfRunLevelID { get; set; }
        public DateTime EffectDate { get; set; }
        public decimal? PriceContract { get; set; }
        public decimal? PriceWarning { get; set; }
    }

    public class HelperFinance_ContractTerm
    {
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int? MaterialID { get; set; }
        public int? ServiceOfOrderID { get; set; }
        public string ExprInput { get; set; }
        public string ExprDatePrice { get; set; }
        public DateTime? DatePrice { get; set; }
        public string ExprPrice { get; set; }
        public DateTime DateEffect { get; set; }
        public DateTime? DateExpire { get; set; }
        public bool IsAllRouting { get; set; }
    }

    public class HelperFinance_PriceLTL
    {
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int PriceID { get; set; }
        public int GroupOfProductID { get; set; }
        public decimal Price { get; set; }
        public int RoutingID { get; set; }
        public int? LocationFromID { get; set; }
        public int? LocationToID { get; set; }
        public DateTime EffectDate { get; set; }
    }

    public class HelperFinance_PriceLTLLevel
    {
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int PriceID { get; set; }
        public int GroupOfProductID { get; set; }
        public decimal Price { get; set; }
        public int RoutingID { get; set; }
        public int? LocationFromID { get; set; }
        public int? LocationToID { get; set; }
        public double Ton { get; set; }
        public double CBM { get; set; }
        public double Quantity { get; set; }
        public DateTime EffectDate { get; set; }
    }

    public class HelperFinance_PriceFTL
    {
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int PriceID { get; set; }
        public int GroupOfVehicleID { get; set; }
        public decimal Price { get; set; }
        public int RoutingID { get; set; }
        public int? LocationFromID { get; set; }
        public int? LocationToID { get; set; }
        public DateTime EffectDate { get; set; }
    }

    public class HelperFinance_PriceFTLLevel
    {
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int PriceID { get; set; }
        public int GroupOfVehicleID { get; set; }
        public decimal Price { get; set; }
        public int RoutingID { get; set; }
        public int? LocationFromID { get; set; }
        public int? LocationToID { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateEnd { get; set; }
        public DateTime EffectDate { get; set; }
    }

    public class HelperFinance_PriceLoad
    {
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int PriceID { get; set; }
        public int GroupOfProductID { get; set; }
        public int PriceOfGOPID { get; set; }
        public bool IsLoading { get; set; }
        public decimal Price { get; set; }
        public int? RoutingID { get; set; }
        public int? ParentRoutingID { get; set; }
        public int? GroupOfLocationID { get; set; }
        public int? LocationID { get; set; }
        public int? PartnerID { get; set; }
        public DateTime EffectDate { get; set; }
    }

    public class HelperFinance_PriceMOQ
    {
        public int ID { get; set; }
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int PriceID { get; set; }
        public string MOQName { get; set; }
        public int DIMOQSumID { get; set; }
        public string ExprInput { get; set; }
        public string ExprTon { get; set; }
        public string ExprCBM { get; set; }
        public string ExprQuan { get; set; }
        public string ExprPrice { get; set; }
        public string ExprPriceFix { get; set; }
        public int TypeOfPriceDIExID { get; set; }
        public string TypeOfPriceDIExCode { get; set; }
        public string TypeOfPriceDIExName { get; set; }
        public DateTime EffectDate { get; set; }

        public List<int> ListGroupOfLocation { get; set; }
        public List<HelperFinance_PriceMOQGroupProduct> ListGroupProduct { get; set; }
        public List<int> ListRouting { get; set; }
        public List<int> ListParentRouting { get; set; }
        public List<int> ListPartnerID { get; set; }
        public List<int> ListProvinceID { get; set; }
        public List<HelperFinance_PriceMOQLocation> ListLocation { get; set; }
    }

    public class HelperFinance_PriceMOQGroupProduct
    {
        public int GroupOfProductID { get; set; }
        public string ExprQuantity { get; set; }
        public string ExprPrice { get; set; }
    }

    public class HelperFinance_PriceMOQContainer
    {
        public int PackingID { get; set; }
        public string ExprQuantity { get; set; }
        public string ExprPrice { get; set; }
    }

    public class HelperFinance_PriceMOQLocation
    {
        public int LocationID { get; set; }
        public int TypeOfTOLocationID { get; set; }
    }

    public class HelperFinance_PriceMOQLoad
    {
        public int ID { get; set; }
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int PriceID { get; set; }
        public bool IsLoading { get; set; }
        public string MOQName { get; set; }
        public int DIMOQLoadSumID { get; set; }
        public int? ParentRoutingID { get; set; }
        public string ExprInput { get; set; }
        public string ExprTon { get; set; }
        public string ExprCBM { get; set; }
        public string ExprQuan { get; set; }
        public string ExprPrice { get; set; }
        public string ExprPriceFix { get; set; }
        public int TypeOfPriceDIExID { get; set; }
        public string TypeOfPriceDIExCode { get; set; }
        public string TypeOfPriceDIExName { get; set; }
        public DateTime EffectDate { get; set; }

        public List<int> ListGroupOfLocation { get; set; }
        public List<HelperFinance_PriceMOQGroupProduct> ListGroupProduct { get; set; }
        public List<int> ListRouting { get; set; }
        public List<int> ListParentRouting { get; set; }
        public List<int> ListPartnerID { get; set; }
        public List<int> ListProvinceID { get; set; }
        public List<HelperFinance_PriceMOQLocation> ListLocation { get; set; }
    }

    public class HelperFinance_PriceEx
    {
        public int ID { get; set; }
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int? ServiceOfOrderID { get; set; }
        public int PriceID { get; set; }
        public string Note { get; set; }
        public int DIExSumID { get; set; }
        public int COExSumID { get; set; }
        public string ExprInput { get; set; }
        public string ExprTon { get; set; }
        public string ExprCBM { get; set; }
        public string ExprQuan { get; set; }
        public string ExprPrice { get; set; }
        public string ExprPriceFix { get; set; }
        public int TypeOfPriceDIExID { get; set; }
        public string TypeOfPriceDIExCode { get; set; }
        public string TypeOfPriceDIExName { get; set; }
        public int TypeOfPriceCOExID { get; set; }
        public string TypeOfPriceCOExCode { get; set; }
        public string TypeOfPriceCOExName { get; set; }
        public DateTime EffectDate { get; set; }

        public List<int> ListGroupOfLocation { get; set; }
        public List<HelperFinance_PriceMOQGroupProduct> ListGroupProduct { get; set; }
        public List<HelperFinance_PriceMOQContainer> ListContainer { get; set; }
        public List<int> ListRouting { get; set; }
        public List<int> ListParentRouting { get; set; }
        public List<int> ListPartnerID { get; set; }
        public List<int> ListProvinceID { get; set; }
        public List<HelperFinance_PriceMOQLocation> ListLocation { get; set; }
    }

    public class HelperFinance_ContractGroupProduct
    {
        public int ContractID { get; set; }
        public int? TypeOfContractQuantityID { get; set; }
        public int GroupOfProductID { get; set; }
        public int? PriceOfGOPID { get; set; }
        public int? GroupOfProductIDChange { get; set; }
        public int? ProductID { get; set; }
        public int? ProductIDChange { get; set; }
        public string Expression { get; set; }
        public string ExpressionInput { get; set; }
        public int? PriceOfGOPIDChange { get; set; }
        public string PriceOfGOPIDChangeName { get; set; }
        public int? TypeOfSGroupProductChangeID { get; set; }
        public int TypeOfPackageID { get; set; }
        public double? CBM { get; set; }
        public double? Weight { get; set; }
    }

    public class HelperFinance_GroupProductMapping
    {
        public int VendorID { get; set; }
        public int GroupOfProductVENID { get; set; }
        public int GroupOfProductCUSID { get; set; }
        public int? PriceOfGOPID { get; set; }
        public string PriceOfGOPIDName { get; set; }
    }

    public class HelperFinance_PriceContainer
    {
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int ServiceOfOrderID { get; set; }
        public int PackingID { get; set; }
        public int PriceID { get; set; }
        public int RoutingID { get; set; }
        public int? LocationFromID { get; set; }
        public int? LocationToID { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectDate { get; set; }
        public int? AreaFromID { get; set; }
        public int? AreaToID { get; set; }
        public List<int> ListAreaFromID { get; set; }
        public List<int> ListAreaToID { get; set; }
    }

    public class HelperFinance_PriceService
    {
        public int ContractID { get; set; }
        public int ContractTermID { get; set; }
        public int? PackingID { get; set; }
        public int PriceID { get; set; }
        public int ServiceID { get; set; }
        public int CurrencyID { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectDate { get; set; }
    }

    public class HelperFinance_DocumentService
    {
        public DateTime DateConfigCustomer { get; set; }
        public int CustomerID { get; set; }
        public int? ContractCustomerID { get; set; }
        public int? VendorID { get; set; }
        public int? ContractVendorID { get; set; }
        public int ServiceID { get; set; }
        public string ServiceCode { get; set; }
        public decimal? PriceCustomer { get; set; }
        public decimal? PriceVendor { get; set; }
        public int OrderID { get; set; }
        public int DocumentID { get; set; }
    }

    public class HelperFinance_DocumentContainer
    {
        public int DocumentID { get; set; }
        public DateTime DateConfigCustomer { get; set; }
        public int CustomerID { get; set; }
        public int? ContractCustomerID { get; set; }
        public int? VendorID { get; set; }
        public int? ContractVendorID { get; set; }
        public int ContainerID { get; set; }
        public int OPSContainerID { get; set; }
        public int OrderID { get; set; }
        public int PackingID { get; set; }
        public List<HelperFinance_ContainerService> ListService { get; set; }
    }

    public class HelperFinance_ContainerService
    {
        public int ServiceID { get; set; }
        public string ServiceCode { get; set; }
        public decimal? PriceCustomer { get; set; }
        public decimal? PriceVendor { get; set; }
    }

    public class HelperFinance_ManualFix
    {
        public int ID { get; set; }
        public int DITOGroupProductID { get; set; }
        public int? DITOMasterID { get; set; }
        public int? VehicleID { get; set; }
        public int? VendorOfVehicleID { get; set; }
        public int OrderID { get; set; }
        public int CustomerID { get; set; }
        public int? ContractID { get; set; }
        public double Ton { get; set; }
        public double CBM { get; set; }
        public double Quantity { get; set; }
        public decimal Credit { get; set; }
        public decimal Debit { get; set; }
        public string Note { get; set; }
        public decimal UnitPrice { get; set; }
        public int? PriceOfGOPID { get; set; }
        public string PriceOfGOPName { get; set; }
    }

    public class HelperFinance_Trouble
    {
        public int ID { get; set; }
        public int? DITOMasterID { get; set; }
        public int? COTOMasterID { get; set; }
        public int? VehicleID { get; set; }
        public int? VendorOfVehicleID { get; set; }
        public int? ContractID { get; set; }
        public int TypeOfContractID { get; set; }
        public decimal CostOfCustomer { get; set; }
        public decimal CostOfVendor { get; set; }
        public int TroubleCostStatusID { get; set; }
        public int? DriverID { get; set; }
        public int GroupOfTroubleID { get; set; }
        public string GroupOfTroubleCode { get; set; }
        public string GroupOfTroubleName { get; set; }
        public string Description { get; set; }
    }

    public class HelperFinance_FINTemp
    {
        public int? ContractID { get; set; }
        public int? PriceDIMOQID { get; set; }
        public int? PriceDIMOQLoadID { get; set; }
        public int? PriceDIExID { get; set; }
        public int? DITOGroupProductID { get; set; }
        public int? ScheduleID { get; set; }
        public int? COTOContainerID { get; set; }
        public int? DITOMasterID { get; set; }
        public int? PriceCOExID { get; set; }
    }

    public class HelperFinance_OPSVendorPrice
    {
        public int ID { get; set; }
        public int RoutingID { get; set; }
        public int? ContractTermID { get; set; }
        public decimal Price { get; set; }
        public string Code { get; set; }
        public string RoutingName { get; set; }
        public List<int> ListFrom { get; set; }
        public List<int> ListTo { get; set; }
    }

    #endregion
}