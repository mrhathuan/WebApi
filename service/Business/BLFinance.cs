using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Data;
using DTO;
using System.ServiceModel;
using System.Data.Entity;

namespace Business
{
    public class BLFinance : Base, IBase
    {
        #region Business Test
        public void Account_Setting()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var lstId = model.CUS_Customer.Select(c => c.ID);
                    Account.ListCustomerID = lstId.ToArray();
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

        #region Common

        public DTOResult Customer_List(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var query = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => new DTOCustomer
                    {
                        ID = c.ID,
                        Code = c.Code,
                        CustomerName = c.ShortName
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOCustomer>;
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

        public DTOResult Vendor_List()
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var query = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => new DTOCustomer
                    {
                        ID = c.ID,
                        Code = c.Code,
                        CustomerName = c.ShortName
                    }).ToList();
                    result.Total = query.Count;
                    result.Data = query;
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

        #region FINRefresh
        public FINRefresh FINRefresh_List(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                var result = new FINRefresh();
                dtfrom = dtfrom.Date;
                dtto = dtto.Date.AddDays(1);

                using (var model = new DataEntities())
                {
                    result.ListPL = model.FIN_PL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Effdate >= dtfrom && c.Effdate < dtto && c.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.CustomerID.HasValue && Account.ListCustomerID.Contains(c.CustomerID.Value)).Select(c => new FINRefresh_PL
                    {
                        ID = c.ID,
                        VehicleID = c.VehicleID,
                        VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                        OrderID = c.OrderID,
                        OrderCode = c.OrderID > 0 ? c.ORD_Order.Code : "",
                        VendorID = c.VendorID > 0 ? c.VendorID.Value : -1,
                        VendorCode = c.VendorID > 0 ? c.CUS_Customer1.Code : "",
                        VendorName = c.VendorID > 0 ? c.CUS_Customer1.CustomerName : c.DITOMasterID > 0 || c.COTOMasterID > 0 ? "Xe nhà" : "",
                        CustomerID = c.CustomerID > 0 ? c.CustomerID : -1,
                        CustomerCode = c.CustomerID > 0 ? c.CUS_Customer.Code : "",
                        CustomerName = c.CustomerID > 0 ? c.CUS_Customer.CustomerName : "",
                        DriverID = c.DriverID,
                        DriverCode = c.DriverID > 0 ? c.FLM_Driver.CAT_Driver.Code : "",
                        DriverName = c.DriverID > 0 ? c.FLM_Driver.CAT_Driver.LastName + " " + c.FLM_Driver.CAT_Driver.FirstName : "",
                        Credit = c.Credit,
                        Debit = c.Debit,
                        Effdate = c.Effdate
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

        public string FINRefresh_Refresh(DateTime date, List<int> lstID)
        {
            try
            {
                var result = string.Empty;
                date = date.Date;
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    if (result == string.Empty)
                    {
                        try
                        {
                            HelperFinance.Truck_CalculateTO(model, Account, date.Date, lstID);
                            model.SaveChanges();
                        }
                        catch
                        {
                            return "TO";
                        }

                        try
                        {
                            HelperFinance.Truck_CalculateOrder(model, Account, date.Date, lstID);
                            model.SaveChanges();
                        }
                        catch
                        {
                            return "TO-Order";
                        }

                        try
                        {
                            HelperFinance.Container_CalculateCO(model, Account, date.Date, lstID);
                            model.SaveChanges();
                        }
                        catch
                        {
                            return "CO";
                        }
                        try
                        {
                            HelperFinance.Container_CalculateOrder(model, Account, date.Date, lstID);
                            model.SaveChanges();
                        }
                        catch
                        {
                            return "CO-Order";
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

        public void FINRefresh_RefreshRoute_Order(DateTime dtfrom, DateTime dtto, List<int> lstID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date;

                    #region Cập nhật hợp đồng + tìm cung đường
                    var lstOrder = model.ORD_Order.Where(c => lstID.Contains(c.CustomerID) && c.RequestDate >= dtfrom && c.RequestDate <= dtto && c.StatusOfOrderID > -(int)SYSVarType.StatusOfOrderNew && c.SYSCustomerID == Account.SYSCustomerID).Select(c => new { c.ID, c.CustomerID, c.ContractID, c.ContractTermID, c.TransportModeID, c.ServiceOfOrderID, c.RequestDate }).ToList();
                    var lstContractID = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.CustomerID > 0 && lstID.Contains(c.CustomerID.Value) && c.EffectDate <= dtfrom && c.EffectDate <= dtto && (c.ExpiredDate == null || c.ExpiredDate >= dtfrom) && c.SYSCustomerID == Account.SYSCustomerID && c.CUS_Customer.IsSystem == false && c.CUS_Customer.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS).Select(c => c.ID).ToList();
                    if (lstContractID.Count > 0)
                    {
                        var lstContractTemp = model.CAT_ContractTerm.Where(c => lstContractID.Contains(c.ContractID) && c.DateEffect <= dtfrom && c.DateEffect <= dtto && (c.DateExpire == null || c.DateExpire >= dtfrom)).Select(c => new { c.ID, c.ContractID, c.CAT_Contract.CustomerID, c.CAT_Contract.TransportModeID, c.ServiceOfOrderID, c.DateEffect, c.DateExpire }).ToList();
                        var lstOrderID = new List<int>();
                        //check order
                        foreach (var item in lstOrder)
                        {
                            if (item.ContractTermID == null)
                            {
                                var itemTemp = lstContractTemp.Where(c => c.CustomerID == item.CustomerID && c.TransportModeID == item.TransportModeID && c.ServiceOfOrderID == item.ServiceOfOrderID && c.DateEffect <= item.RequestDate && (c.DateExpire == null || c.DateExpire >= item.RequestDate)).OrderBy(c => c.DateEffect).FirstOrDefault();
                                if (itemTemp != null)
                                {
                                    var objOrder = model.ORD_Order.FirstOrDefault(c => c.ID == item.ID);
                                    if (objOrder != null)
                                    {
                                        objOrder.ContractID = itemTemp.ContractID;
                                        objOrder.ContractTermID = itemTemp.ID;
                                        lstOrderID.Add(item.ID);
                                    }
                                }
                                else
                                {
                                    var itemContract = lstContractTemp.Where(c => c.CustomerID == item.CustomerID && c.TransportModeID == item.TransportModeID && c.DateEffect <= item.RequestDate && (c.DateExpire == null || c.DateExpire >= item.RequestDate)).OrderBy(c => c.DateEffect).FirstOrDefault();
                                    if (itemContract != null)
                                    {
                                        var objOrder = model.ORD_Order.FirstOrDefault(c => c.ID == item.ID);
                                        if (objOrder != null)
                                        {
                                            objOrder.ContractID = itemContract.ContractID;
                                            lstOrderID.Add(item.ID);
                                        }
                                    }
                                }
                            }
                        }
                        model.SaveChanges();
                    }

                    //check route group
                    var lstOrderGroup = model.ORD_GroupProduct.Where(c => lstID.Contains(c.ORD_Order.CustomerID) && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_Order.RequestDate >= dtfrom && c.ORD_Order.RequestDate <= dtto && (c.ORD_Order.TransportModeID == -(int)SYSVarType.TransportModeFTL || c.ORD_Order.TransportModeID == -(int)SYSVarType.TransportModeLTL) && c.LocationFromID > 0 && c.LocationToID > 0).Select(c => new { c.ID, c.ORD_Order.CustomerID, LocationFromID = c.LocationFromID.Value, LocationToID = c.LocationToID.Value, c.ORD_Order.ContractID }).ToList();
                    foreach (var itemGroup in lstOrderGroup)
                    {
                        var id = HelperRouting.ORDOrder_CUSRouting_FindDI(model, Account, itemGroup.CustomerID, itemGroup.ContractID, itemGroup.LocationFromID, itemGroup.LocationToID);
                        if (id > 0)
                        {
                            var objGroup = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == itemGroup.ID);
                            objGroup.CUSRoutingID = id;
                        }
                    }

                    //check route container
                    var lstOrderContainer = model.ORD_Container.Where(c => lstID.Contains(c.ORD_Order.CustomerID) && c.ORD_Order.RequestDate >= dtfrom && c.ORD_Order.RequestDate <= dtto && c.ORD_Order.ContractTermID > 0).Select(c => new { c.ID, c.ORD_Order.CustomerID, c.ORD_Order.RequestDate, c.ORD_Order.ContractID, c.ORD_Order.ContractTermID, c.ORD_Order.CAT_TransportMode.TransportModeID, c.ORD_Order.CAT_ServiceOfOrder.ServiceOfOrderID, c.LocationFromID, c.LocationToID, c.LocationDepotID, c.LocationDepotReturnID }).ToList();
                    foreach (var itemContainer in lstOrderContainer)
                    {
                        int? getid = itemContainer.LocationDepotID;
                        int? returnid = itemContainer.LocationDepotReturnID;
                        if (itemContainer.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderExport)
                            returnid = null;
                        else if (itemContainer.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderImport)
                            getid = null;
                        else
                        {
                            getid = null;
                            returnid = null;
                        }
                        var id = HelperRouting.ORDOrder_CUSRouting_FindCO(model, Account, itemContainer.CustomerID, itemContainer.ContractID, itemContainer.ContractTermID, itemContainer.LocationFromID.Value, itemContainer.LocationToID.Value, getid, returnid);
                        if (id > 0)
                        {
                            var objContainer = model.ORD_Container.FirstOrDefault(c => c.ID == itemContainer.ID);
                            objContainer.CUSRoutingID = id;
                        }
                    }
                    model.SaveChanges();

                    #endregion

                    #region Cập nhật SortConfig cho đơn hàng
                    if (lstContractID.Count > 0)
                    {
                        // Ktra phụ lục có tính FTL bậc thang
                        var lstTerm = model.CAT_ContractTerm.Where(c => lstContractID.Contains(c.ContractID) && c.SortConfigDateStart.HasValue && (c.DateEffect <= dtfrom || c.DateEffect <= dtto) && !c.IsClosed).Select(c => new { c.ContractID, c.SortConfigDateStart, c.CAT_Contract.TypeOfContractDateID }).ToList();
                        foreach (var itemTerm in lstTerm)
                        {
                            DateTime dtStart = new DateTime(dtfrom.Year, dtfrom.Month, itemTerm.SortConfigDateStart.Value.Day, itemTerm.SortConfigDateStart.Value.Hour, itemTerm.SortConfigDateStart.Value.Minute, itemTerm.SortConfigDateStart.Value.Second);
                            DateTime dtEnd = dtStart.AddMonths(1);
                            List<DTOFINRefresh_Order> lstOrderConfig = new List<DTOFINRefresh_Order>();
                            List<DTOFINRefresh_Order> lstConfig = new List<DTOFINRefresh_Order>();

                            #region Lấy dữ liệu theo ngày đc thiết lập
                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateRequest)
                            {
                                var lstTemp = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.DITOMasterID > 0 && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.ORD_GroupProduct.ORD_Order.ContractID == itemTerm.ContractID && c.ORD_GroupProduct.ORD_Order.RequestDate >= dtStart && c.ORD_GroupProduct.ORD_Order.RequestDate < dtEnd).Select(c => new
                                {
                                    OrderID = c.ORD_GroupProduct.OrderID,
                                    RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                                    ETD = c.OPS_DITOMaster.ETD,
                                    ETA = c.OPS_DITOMaster.ETA,
                                    ATD = c.OPS_DITOMaster.ATD,
                                    ATA = c.OPS_DITOMaster.ATA,
                                    DateReceived = c.OPS_DITOMaster.DateReceived,
                                    ETARequest = c.ORD_GroupProduct.ORD_Order.ETARequest,
                                    ETDRequest = c.ORD_GroupProduct.ORD_Order.ETDRequest,
                                }).Distinct().ToList();

                                lstOrderConfig = lstTemp.Select(c => new DTOFINRefresh_Order
                                {
                                    OrderID = c.OrderID,
                                    RequestDate = c.RequestDate,
                                    ETD = c.ETD,
                                    ETA = c.ETA,
                                    ATD = c.ATD,
                                    ATA = c.ATA,
                                    DateReceived = c.DateReceived,
                                    ETARequest = c.ETARequest,
                                    ETDRequest = c.ETDRequest,
                                }).ToList();
                            }

                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETD)
                            {
                                var lstTemp = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.DITOMasterID > 0 && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.ORD_GroupProduct.ORD_Order.ContractID == itemTerm.ContractID && c.OPS_DITOMaster.ETD >= dtStart && c.OPS_DITOMaster.ETD < dtEnd).Select(c => new
                                {
                                    OrderID = c.ORD_GroupProduct.OrderID,
                                    RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                                    ETD = c.OPS_DITOMaster.ETD,
                                    ETA = c.OPS_DITOMaster.ETA,
                                    ATD = c.OPS_DITOMaster.ATD,
                                    ATA = c.OPS_DITOMaster.ATA,
                                    DateReceived = c.OPS_DITOMaster.DateReceived,
                                    ETARequest = c.ORD_GroupProduct.ORD_Order.ETARequest,
                                    ETDRequest = c.ORD_GroupProduct.ORD_Order.ETDRequest,
                                }).Distinct().ToList();

                                lstOrderConfig = lstTemp.Select(c => new DTOFINRefresh_Order
                                {
                                    OrderID = c.OrderID,
                                    RequestDate = c.RequestDate,
                                    ETD = c.ETD,
                                    ETA = c.ETA,
                                    ATD = c.ATD,
                                    ATA = c.ATA,
                                    DateReceived = c.DateReceived,
                                    ETARequest = c.ETARequest,
                                    ETDRequest = c.ETDRequest,
                                }).ToList();
                            }

                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATD)
                            {
                                var lstTemp = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.DITOMasterID > 0 && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.ORD_GroupProduct.ORD_Order.ContractID == itemTerm.ContractID && c.OPS_DITOMaster.ATD >= dtStart && c.OPS_DITOMaster.ATD < dtEnd).Select(c => new
                                {
                                    OrderID = c.ORD_GroupProduct.OrderID,
                                    RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                                    ETD = c.OPS_DITOMaster.ETD,
                                    ETA = c.OPS_DITOMaster.ETA,
                                    ATD = c.OPS_DITOMaster.ATD,
                                    ATA = c.OPS_DITOMaster.ATA,
                                    DateReceived = c.OPS_DITOMaster.DateReceived,
                                    ETARequest = c.ORD_GroupProduct.ORD_Order.ETARequest,
                                    ETDRequest = c.ORD_GroupProduct.ORD_Order.ETDRequest,
                                }).Distinct().ToList();

                                lstOrderConfig = lstTemp.Select(c => new DTOFINRefresh_Order
                                {
                                    OrderID = c.OrderID,
                                    RequestDate = c.RequestDate,
                                    ETD = c.ETD,
                                    ETA = c.ETA,
                                    ATD = c.ATD,
                                    ATA = c.ATA,
                                    DateReceived = c.DateReceived,
                                    ETARequest = c.ETARequest,
                                    ETDRequest = c.ETDRequest,
                                }).ToList();
                            }

                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATA)
                            {
                                var lstTemp = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.DITOMasterID > 0 && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.ORD_GroupProduct.ORD_Order.ContractID == itemTerm.ContractID && c.OPS_DITOMaster.ATA >= dtStart && c.OPS_DITOMaster.ATA < dtEnd).Select(c => new
                                {
                                    OrderID = c.ORD_GroupProduct.OrderID,
                                    RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                                    ETD = c.OPS_DITOMaster.ETD,
                                    ETA = c.OPS_DITOMaster.ETA,
                                    ATD = c.OPS_DITOMaster.ATD,
                                    ATA = c.OPS_DITOMaster.ATA,
                                    DateReceived = c.OPS_DITOMaster.DateReceived,
                                    ETARequest = c.ORD_GroupProduct.ORD_Order.ETARequest,
                                    ETDRequest = c.ORD_GroupProduct.ORD_Order.ETDRequest,
                                }).Distinct().ToList();

                                lstOrderConfig = lstTemp.Select(c => new DTOFINRefresh_Order
                                {
                                    OrderID = c.OrderID,
                                    RequestDate = c.RequestDate,
                                    ETD = c.ETD,
                                    ETA = c.ETA,
                                    ATD = c.ATD,
                                    ATA = c.ATA,
                                    DateReceived = c.DateReceived,
                                    ETARequest = c.ETARequest,
                                    ETDRequest = c.ETDRequest,
                                }).ToList();
                            }

                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateComplete)
                            {
                                var lstTemp = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.DITOMasterID > 0 && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.ORD_GroupProduct.ORD_Order.ContractID == itemTerm.ContractID && c.OPS_DITOMaster.DateReceived >= dtStart && c.OPS_DITOMaster.DateReceived < dtEnd).Select(c => new
                                {
                                    OrderID = c.ORD_GroupProduct.OrderID,
                                    RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                                    ETD = c.OPS_DITOMaster.ETD,
                                    ETA = c.OPS_DITOMaster.ETA,
                                    ATD = c.OPS_DITOMaster.ATD,
                                    ATA = c.OPS_DITOMaster.ATA,
                                    DateReceived = c.OPS_DITOMaster.DateReceived,
                                    ETARequest = c.ORD_GroupProduct.ORD_Order.ETARequest,
                                    ETDRequest = c.ORD_GroupProduct.ORD_Order.ETDRequest,
                                }).Distinct().ToList();

                                lstOrderConfig = lstTemp.Select(c => new DTOFINRefresh_Order
                                {
                                    OrderID = c.OrderID,
                                    RequestDate = c.RequestDate,
                                    ETD = c.ETD,
                                    ETA = c.ETA,
                                    ATD = c.ATD,
                                    ATA = c.ATA,
                                    DateReceived = c.DateReceived,
                                    ETARequest = c.ETARequest,
                                    ETDRequest = c.ETDRequest,
                                }).ToList();
                            }

                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETARequest)
                            {
                                var lstTemp = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.DITOMasterID > 0 && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.ORD_GroupProduct.ORD_Order.ContractID == itemTerm.ContractID && c.ORD_GroupProduct.ORD_Order.ETARequest >= dtStart && c.ORD_GroupProduct.ORD_Order.ETARequest < dtEnd).Select(c => new
                                {
                                    OrderID = c.ORD_GroupProduct.OrderID,
                                    RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                                    ETD = c.OPS_DITOMaster.ETD,
                                    ETA = c.OPS_DITOMaster.ETA,
                                    ATD = c.OPS_DITOMaster.ATD,
                                    ATA = c.OPS_DITOMaster.ATA,
                                    DateReceived = c.OPS_DITOMaster.DateReceived,
                                    ETARequest = c.ORD_GroupProduct.ORD_Order.ETARequest,
                                    ETDRequest = c.ORD_GroupProduct.ORD_Order.ETDRequest,
                                }).Distinct().ToList();

                                lstOrderConfig = lstTemp.Select(c => new DTOFINRefresh_Order
                                {
                                    OrderID = c.OrderID,
                                    RequestDate = c.RequestDate,
                                    ETD = c.ETD,
                                    ETA = c.ETA,
                                    ATD = c.ATD,
                                    ATA = c.ATA,
                                    DateReceived = c.DateReceived,
                                    ETARequest = c.ETARequest,
                                    ETDRequest = c.ETDRequest,
                                }).ToList();
                            }

                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETDRequest)
                            {
                                var lstTemp = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.DITOMasterID > 0 && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete && c.ORD_GroupProduct.ORD_Order.ContractID == itemTerm.ContractID && c.ORD_GroupProduct.ORD_Order.ETDRequest >= dtStart && c.ORD_GroupProduct.ORD_Order.ETDRequest < dtEnd).Select(c => new
                                {
                                    OrderID = c.ORD_GroupProduct.OrderID,
                                    RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                                    ETD = c.OPS_DITOMaster.ETD,
                                    ETA = c.OPS_DITOMaster.ETA,
                                    ATD = c.OPS_DITOMaster.ATD,
                                    ATA = c.OPS_DITOMaster.ATA,
                                    DateReceived = c.OPS_DITOMaster.DateReceived,
                                    ETARequest = c.ORD_GroupProduct.ORD_Order.ETARequest,
                                    ETDRequest = c.ORD_GroupProduct.ORD_Order.ETDRequest,
                                }).Distinct().ToList();

                                lstOrderConfig = lstTemp.Select(c => new DTOFINRefresh_Order
                                {
                                    OrderID = c.OrderID,
                                    RequestDate = c.RequestDate,
                                    ETD = c.ETD,
                                    ETA = c.ETA,
                                    ATD = c.ATD,
                                    ATA = c.ATA,
                                    DateReceived = c.DateReceived,
                                    ETARequest = c.ETARequest,
                                    ETDRequest = c.ETDRequest,
                                }).ToList();
                            }
                            #endregion

                            foreach (var itemOrder in lstOrderConfig.GroupBy(c => c.OrderID))
                            {
                                DTOFINRefresh_Order obj = new DTOFINRefresh_Order();
                                obj.OrderID = itemOrder.Key;
                                obj.RequestDate = itemOrder.OrderBy(c => c.RequestDate).FirstOrDefault().RequestDate;
                                obj.ETD = itemOrder.OrderBy(c => c.ETD).FirstOrDefault().ETD;
                                obj.ATD = itemOrder.OrderBy(c => c.ATD).FirstOrDefault().ATD;
                                obj.ATA = itemOrder.OrderBy(c => c.ATA).FirstOrDefault().ATA;
                                obj.DateReceived = itemOrder.OrderBy(c => c.DateReceived).FirstOrDefault().DateReceived;
                                obj.ETARequest = itemOrder.OrderBy(c => c.ETARequest).FirstOrDefault().ETARequest;
                                obj.ETDRequest = itemOrder.OrderBy(c => c.ETDRequest).FirstOrDefault().ETDRequest;
                                lstConfig.Add(obj);
                            }

                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateRequest)
                                lstConfig = lstConfig.OrderBy(c => c.RequestDate).ToList();
                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETD)
                                lstConfig = lstConfig.OrderBy(c => c.ETD).ToList();
                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATD)
                                lstConfig = lstConfig.OrderBy(c => c.ATD).ToList();
                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateATA)
                                lstConfig = lstConfig.OrderBy(c => c.ATA).ToList();
                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateComplete)
                                lstConfig = lstConfig.OrderBy(c => c.DateReceived).ToList();
                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETDRequest)
                                lstConfig = lstConfig.OrderBy(c => c.ETDRequest).ToList();
                            if (itemTerm.TypeOfContractDateID == -(int)SYSVarType.TypeOfContractDateETARequest)
                                lstConfig = lstConfig.OrderBy(c => c.ETARequest).ToList();
                            int sort = 1;
                            foreach (var itemOrder in lstConfig)
                            {
                                var obj = model.ORD_Order.FirstOrDefault(c => c.ID == itemOrder.OrderID);
                                if (obj != null)
                                {
                                    obj.ModifiedBy = Account.UserName;
                                    obj.ModifiedDate = DateTime.Now;
                                    obj.SortConfig = sort;
                                }
                                sort++;
                            }
                        }
                        model.SaveChanges();
                    }
                    #endregion
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

        public void FINRefresh_RefreshRoute_TO(DateTime dtfrom, DateTime dtto, List<int> lstID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;
                    dtfrom = dtfrom.Date;
                    dtto = dtto.Date;

                    #region Container
                    //check tranport, contract
                    var lstMasterID = new List<int>();
                    var lstContractID = new List<int>();
                    foreach (var objMaster in model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ETD >= dtfrom && c.ETD <= dtto))
                    {
                        var first = model.OPS_COTOContainer.Where(c => lstID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID) && c.COTOMasterID == objMaster.ID && c.ParentID == null && c.OPS_Container.ORD_Container.ORD_Order.TransportModeID > 0 && c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID > 0).Select(c => new { c.OPS_Container.ORD_Container.ORD_Order.TransportModeID, c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID, c.OPS_Container.ORD_Container.ORD_Order.CustomerID }).FirstOrDefault();
                        if (first != null)
                        {
                            objMaster.TransportModeID = first.TransportModeID;
                            var query = model.CAT_ContractTerm.Where(c => c.CAT_Contract.TransportModeID == first.TransportModeID && c.ServiceOfOrderID == first.ServiceOfOrderID && c.CAT_Contract.SYSCustomerID == Account.SYSCustomerID && c.CAT_Contract.CustomerID == Account.SYSCustomerID && c.DateEffect <= objMaster.ETD && (c.DateExpire == null || c.DateExpire >= objMaster.ETD));
                            var queryCompany = model.CAT_ContractTerm.Where(c => c.CAT_Contract.TransportModeID == first.TransportModeID && c.ServiceOfOrderID == first.ServiceOfOrderID && c.CAT_Contract.CUS_Company.CustomerRelateID == first.CustomerID && c.CAT_Contract.SYSCustomerID == Account.SYSCustomerID && c.CAT_Contract.CustomerID == Account.SYSCustomerID && c.DateEffect <= objMaster.ETD && (c.DateExpire == null || c.DateExpire >= objMaster.ETD));
                            if (objMaster.VendorOfVehicleID != null && objMaster.VendorOfVehicleID != Account.SYSCustomerID)
                            {
                                query = model.CAT_ContractTerm.Where(c => c.CAT_Contract.TransportModeID == first.TransportModeID && c.ServiceOfOrderID == first.ServiceOfOrderID && c.CAT_Contract.SYSCustomerID == Account.SYSCustomerID && c.CAT_Contract.CustomerID == objMaster.VendorOfVehicleID && c.DateEffect <= objMaster.ETD && (c.DateExpire == null || c.DateExpire >= objMaster.ETD));
                                queryCompany = model.CAT_ContractTerm.Where(c => c.CAT_Contract.TransportModeID == first.TransportModeID && c.ServiceOfOrderID == first.ServiceOfOrderID && c.CAT_Contract.CUS_Company.CustomerRelateID == first.CustomerID && c.CAT_Contract.SYSCustomerID == Account.SYSCustomerID && c.CAT_Contract.CustomerID == objMaster.VendorOfVehicleID && c.DateEffect <= objMaster.ETD && (c.DateExpire == null || c.DateExpire >= objMaster.ETD));
                            }
                            if (queryCompany.Count() > 0)
                            {
                                var item = queryCompany.Select(c => new { c.ContractID }).FirstOrDefault();
                                objMaster.ContractID = item.ContractID;
                                lstMasterID.Add(objMaster.ID);
                                lstContractID.Add(item.ContractID);
                            }
                            else if (query.Count() > 0)
                            {
                                var item = query.Select(c => new { c.ContractID }).FirstOrDefault();
                                objMaster.ContractID = item.ContractID;
                                lstMasterID.Add(objMaster.ID);
                                lstContractID.Add(item.ContractID);
                            }
                        }
                    }
                    model.SaveChanges();

                    //check temp
                    var lstContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && lstMasterID.Contains(c.COTOMasterID.Value)).Select(c => new { c.ID, VendorOfVehicleID = c.OPS_COTOMaster.VendorOfVehicleID == null ? c.OPS_COTOMaster.SYSCustomerID : c.OPS_COTOMaster.VendorOfVehicleID.Value, c.OPS_COTOMaster.ContractID, c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID, c.OPS_COTOMaster.ETD }).ToList();
                    var lstContractTemp = model.CAT_ContractTerm.Where(c => lstContractID.Contains(c.ContractID) && c.DateEffect <= dtfrom && c.DateEffect <= dtto && (c.DateExpire == null || c.DateExpire >= dtfrom)).Select(c => new { c.ID, c.ContractID, c.CAT_Contract.CustomerID, c.CAT_Contract.TransportModeID, c.ServiceOfOrderID, c.DateEffect, c.DateExpire }).ToList();
                    foreach (var itemContainer in lstContainer)
                    {
                        var itemTemp = lstContractTemp.FirstOrDefault(c => c.CustomerID == itemContainer.VendorOfVehicleID && c.ContractID == itemContainer.ContractID && c.ServiceOfOrderID == itemContainer.ServiceOfOrderID && c.DateEffect <= itemContainer.ETD && (c.DateExpire == null || c.DateExpire >= itemContainer.ETD));
                        if (itemTemp != null)
                        {
                            var obj = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == itemContainer.ID);
                            if (obj != null)
                            {
                                obj.ContractTermID = itemTemp.ID;
                            }
                        }
                    }
                    model.SaveChanges();

                    //check route container 
                    var lstTOContainer = model.OPS_COTOContainer.Where(c => lstID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID) && c.COTOMasterID > 0 && c.OPS_COTOMaster.ETD >= dtfrom && c.OPS_COTOMaster.ETD <= dtto && c.ParentID == null && c.OPS_COTOMaster.ContractID > 0 && c.ContractTermID > 0).Select(c => new
                    {
                        c.ID,
                        c.OPS_COTOMaster.VendorOfVehicleID,
                        c.OPS_COTOMaster.ETD,
                        c.OPS_COTOMaster.ContractID,
                        c.ContractTermID,
                        LocationFromID = c.OPS_Container.ORD_Container.CUS_Location2.LocationID,
                        LocationToID = c.OPS_Container.ORD_Container.CUS_Location3.LocationID,
                        OrderID = c.OPS_Container.ORD_Container.OrderID,
                        OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                    }).ToList();
                    foreach (var itemTOContainer in lstTOContainer)
                    {
                        int customerid = itemTOContainer.VendorOfVehicleID == null || itemTOContainer.VendorOfVehicleID == Account.SYSCustomerID ? Account.SYSCustomerID : itemTOContainer.VendorOfVehicleID.Value;
                        int routeid = HelperRouting.OPSMaster_Routing_FindCO(model, Account, customerid, itemTOContainer.ContractID, itemTOContainer.ContractTermID, itemTOContainer.LocationFromID, itemTOContainer.LocationToID, null, null);
                        if (routeid > 0)
                        {
                            var obj = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == itemTOContainer.ID);
                            if (obj != null)
                            {
                                obj.CATRoutingID = routeid;
                            }
                        }
                    }
                    model.SaveChanges();
                    #endregion

                    #region Phân phối
                    var lstDIMasterID = new List<int>();
                    lstDIMasterID = model.OPS_DITOMaster.Where(c => c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOGroupProduct.Any(d => lstID.Contains(d.ORD_GroupProduct.ORD_Order.CustomerID)) && c.DateConfig >= dtfrom && c.DateConfig < dtto).Select(c => c.ID).Distinct().ToList();
                    foreach (var dimaterid in lstDIMasterID)
                        HelperFinance.Truck_CompleteSchedule(model, Account, dimaterid);
                    #endregion
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

        #region Điều Chỉnh ORD_OPS Truck

        public List<DTOCATContract> FINRefresh_Contract_List(int cusID, int? serID, int transID)
        {
            try
            {
                List<DTOCATContract> result = new List<DTOCATContract>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.CustomerID == cusID && c.TransportModeID == transID).Select(c => new DTOCATContract
                    {
                        ID = c.ID,
                        ContractNo = c.ContractNo,
                        DisplayName = c.DisplayName
                    }).ToList();

                    var objNull = new DTOCATContract();
                    objNull.DisplayName = string.Empty;
                    objNull.ID = -1;
                    result.Insert(0, objNull);
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

        public List<DTOContractTerm> FINRefresh_ContractTerm_List(int contractID)
        {
            try
            {
                List<DTOContractTerm> result = new List<DTOContractTerm>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_ContractTerm.Where(c => c.ContractID == contractID).Select(c => new DTOContractTerm
                    {
                        ID = c.ID,
                        DisplayName = c.DisplayName,
                        ServiceOfOrderName = c.ServiceOfOrderID > 0 ? c.CAT_ServiceOfOrder.Name : "",
                    }).ToList();

                    var objNull = new DTOContractTerm();
                    objNull.DisplayName = string.Empty;
                    objNull.ID = -1;
                    result.Insert(0, objNull);
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

        public List<DTOCATContract> FINRefresh_Contract_Master_List(int vendorID, int? serID, int transID)
        {
            try
            {
                List<DTOCATContract> result = new List<DTOCATContract>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_Contract.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.CustomerID == vendorID && (vendorID == Account.SYSCustomerID ? true : c.TransportModeID == transID)).OrderByDescending(c => c.EffectDate).Select(c => new DTOCATContract
                    {
                        ID = c.ID,
                        ContractNo = c.ContractNo,
                        DisplayName = c.DisplayName
                    }).ToList();

                    var objNull = new DTOCATContract();
                    objNull.DisplayName = string.Empty;
                    objNull.ID = -1;
                    result.Insert(0, objNull);
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

        public List<DTOCUSRouting> FINRefresh_Routing_List(int cusID, int? contractID)
        {
            try
            {
                List<DTOCUSRouting> result = new List<DTOCUSRouting>();
                using (var model = new DataEntities())
                {
                    if (contractID > 0)
                    {
                        var ListRoute = model.CAT_ContractRouting.Where(c => c.ContractID == contractID).Select(c => c.RoutingID).ToList();

                        result = model.CUS_Routing.Where(c => c.CustomerID == cusID && ListRoute.Contains(c.RoutingID)).Select(c => new DTOCUSRouting
                        {
                            ID = c.ID,
                            RoutingID = c.RoutingID,
                            Code = c.CAT_Routing.Code,
                            RoutingName = c.CAT_Routing.RoutingName
                        }).ToList();
                    }
                    else
                    {
                        result = model.CUS_Routing.Where(c => c.CustomerID == cusID && c.CAT_Routing.Code != string.Empty).Select(c => new DTOCUSRouting
                        {
                            ID = c.ID,
                            RoutingID = c.RoutingID,
                            Code = c.CAT_Routing.Code,
                            RoutingName = c.CAT_Routing.RoutingName
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

        public List<DTOCUSRouting> FINRefresh_Routing_Master_List(int vendorID, int? contractID)
        {
            try
            {
                List<DTOCUSRouting> result = new List<DTOCUSRouting>();
                using (var model = new DataEntities())
                {
                    if (contractID > 0)
                    {
                        var ListRoute = model.CAT_ContractRouting.Where(c => c.ContractID == contractID).Select(c => c.RoutingID).ToList();

                        result = model.CUS_Routing.Where(c => c.CustomerID == vendorID && ListRoute.Contains(c.RoutingID)).Select(c => new DTOCUSRouting
                        {
                            ID = c.ID,
                            RoutingID = c.RoutingID,
                            Code = c.CAT_Routing.Code,
                            RoutingName = c.CAT_Routing.RoutingName
                        }).ToList();
                    }
                    else
                    {
                        if (vendorID != Account.SYSCustomerID)
                        {
                            result = model.CUS_Routing.Where(c => c.CustomerID == vendorID && c.CAT_Routing.Code != string.Empty).Select(c => new DTOCUSRouting
                            {
                                ID = c.ID,
                                RoutingID = c.RoutingID,
                                Code = c.CAT_Routing.Code,
                                RoutingName = c.CAT_Routing.RoutingName
                            }).ToList();
                        }
                        else
                        {
                            result = model.CAT_Routing.Where(c => c.Code != string.Empty).Select(c => new DTOCUSRouting
                            {
                                ID = c.ID,
                                RoutingID = c.ID,
                                Code = c.Code,
                                RoutingName = c.RoutingName
                            }).ToList();
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

        public DTOResult FINRefresh_ORD_Group_List(string request, DateTime from, DateTime to)
        {
            try
            {
                var result = new DTOResult();
                using (var model = new DataEntities())
                {
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();

                    var query = model.ORD_GroupProduct.Where(c => (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ORD_Order.CustomerID)) && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_Order.DateConfig >= from && c.ORD_Order.DateConfig <= to && c.ORD_Order.StatusOfOrderID > (int)SYSVarType.StatusOfOrderNew).Select(c => new FINRefresh_ORDGroup
                    {
                        ID = c.ID,
                        OrderID = c.OrderID,
                        OrderCode = c.ORD_Order.Code,
                        CustomerID = c.ORD_Order.CustomerID,
                        CustomerCode = c.ORD_Order.CUS_Customer.Code,
                        CustomerName = c.ORD_Order.CUS_Customer.CustomerName,
                        ServiceOfOrderID = c.ORD_Order.ServiceOfOrderID,
                        TransportModeID = c.ORD_Order.TransportModeID,
                        ContractID = c.ORD_Order.ContractID > 0 ? c.ORD_Order.ContractID.Value : -1,
                        ETD = c.ORD_Order.ETD,
                        ETA = c.ORD_Order.ETA,
                        RequestDate = c.ORD_Order.RequestDate,
                        SOCode = c.SOCode,
                        DNCode = c.DNCode,
                        DateConfig = c.DateConfig,
                        OrderDateConfig = c.ORD_Order.DateConfig,
                        GroupID = c.GroupOfProductID,
                        GroupCode = c.GroupOfProductID > 0 ? c.CUS_GroupOfProduct.Code : string.Empty,
                        GroupName = c.GroupOfProductID > 0 ? c.CUS_GroupOfProduct.GroupName : string.Empty,
                        GroupETA = c.ETA,
                        GroupETD = c.ETD,
                        Ton = c.Ton,
                        RoutingID = c.CUSRoutingID > 0 ? c.CUSRoutingID.Value : -1,
                        RoutingName = c.CUSRoutingID > 0 ? c.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                        LocationFromAddress = c.CUS_Location.CAT_Location.Address,
                        LocationToAddress = c.CUS_Location1.CAT_Location.Address,
                        GroupOfVehicleID = c.ORD_Order.GroupOfVehicleID.HasValue ? c.ORD_Order.GroupOfVehicleID.Value : -1,
                        GroupOfVehicleName = c.ORD_Order.GroupOfVehicleID.HasValue ? c.ORD_Order.CAT_GroupOfVehicle.GroupName : string.Empty,
                        ORDRoutingID = c.ORD_Order.CUSRoutingID > 0 ? c.ORD_Order.CUSRoutingID.Value : -1,
                        ORDRoutingName = c.ORD_Order.CUSRoutingID > 0 ? c.ORD_Order.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<FINRefresh_ORDGroup>;
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

        public void FINRefresh_ORD_Group_Save(FINRefresh_ORDGroup item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        var objORD = model.ORD_Order.FirstOrDefault(c => c.ID == obj.OrderID);
                        objORD.ETD = item.ETD;
                        objORD.ETA = item.ETA;
                        objORD.DateConfig = item.OrderDateConfig;
                        objORD.RequestDate = item.RequestDate;
                        if (item.ContractID > 0)
                            objORD.ContractID = item.ContractID;
                        else
                            objORD.ContractID = null;

                        if (item.RoutingID > 0)
                            obj.CUSRoutingID = item.RoutingID;
                        else
                            obj.CUSRoutingID = null;
                        if (item.ORDRoutingID > 0)
                            objORD.CUSRoutingID = item.ORDRoutingID;
                        else
                            objORD.CUSRoutingID = null;
                        if (item.GroupOfVehicleID > 0) objORD.GroupOfVehicleID = item.GroupOfVehicleID;
                        else objORD.GroupOfVehicleID = null;
                        obj.ETD = item.GroupETD;
                        obj.ETA = item.GroupETA;
                        obj.DateConfig = item.DateConfig;

                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        objORD.ModifiedBy = Account.UserName;
                        objORD.ModifiedDate = DateTime.Now;

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

        public DTOResult FINRefresh_OPS_Group_List(string request, DateTime from, DateTime to)
        {
            try
            {
                var result = new DTOResult();
                using (var model = new DataEntities())
                {
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();

                    var query = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID)) && c.DITOMasterID > 0 && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.DateConfig >= from && c.DateConfig <= to && c.ORD_GroupProduct.ORD_Order.StatusOfOrderID > (int)SYSVarType.StatusOfOrderNew).Select(c => new FINRefresh_OPSGroup
                    {
                        ID = c.ID,
                        ToMasterID = c.DITOMasterID.Value,
                        ToMasterCode = c.OPS_DITOMaster.Code,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                        VendorID = c.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOMaster.VendorOfVehicleID : Account.SYSCustomerID,
                        VehicleNo = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                        VendorName = c.OPS_DITOMaster.VendorOfVehicleID > 0 && c.OPS_DITOMaster.VendorOfVehicleID != Account.SYSCustomerID ? c.OPS_DITOMaster.CUS_Customer.CustomerName : "Xe nhà",
                        ServiceOfOrderID = c.ORD_GroupProduct.ORD_Order.ServiceOfOrderID,
                        TransportModeID = c.OPS_DITOMaster.TransportModeID,
                        TransportModeName = c.OPS_DITOMaster.TransportModeID > 0 ? c.OPS_DITOMaster.CAT_TransportMode.Name : string.Empty,
                        ContractID = c.OPS_DITOMaster.ContractID,
                        ContractNo = c.OPS_DITOMaster.ContractID > 0 ? c.OPS_DITOMaster.CAT_Contract.DisplayName : string.Empty,
                        TypeOfOrderID = c.OPS_DITOMaster.TypeOfOrderID,
                        ETD = c.OPS_DITOMaster.ETD,
                        ETA = c.OPS_DITOMaster.ETA,
                        SOCode = c.ORD_GroupProduct.SOCode,
                        DNCode = c.DNCode,
                        OrderGroupCode = c.ORD_GroupProduct.GroupOfProductID > 0 ? c.ORD_GroupProduct.CUS_GroupOfProduct.Code : string.Empty,
                        OrderGroupName = c.ORD_GroupProduct.GroupOfProductID > 0 ? c.ORD_GroupProduct.CUS_GroupOfProduct.GroupName : string.Empty,
                        RoutingID = c.OPS_DITOMaster.CATRoutingID,
                        //TypeOfDriverRouteFeeID = c.OPS_DITOMaster.TypeOfDriverRouteFeeID,
                        RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                        CustormerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        CustormerName = c.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                        DateConfig = c.DateConfig,
                        GroupOfVehicleID = c.OPS_DITOMaster.GroupOfVehicleID > 0 ? c.OPS_DITOMaster.GroupOfVehicleID.Value : -1,
                        GroupOfVehicleName = c.OPS_DITOMaster.GroupOfVehicleID > 0 ? c.OPS_DITOMaster.CAT_GroupOfVehicle.GroupName : string.Empty,
                        DateConfigMaster = c.OPS_DITOMaster.DateConfig,
                        RoutingIDGroup = c.CATRoutingID,
                        OPSGroupRoutingCode = c.CATRoutingID > 0 ? c.CAT_Routing.Code : "",
                        ORDGroupRoutingID = c.ORD_GroupProduct.CUSRoutingID,
                        ORDGroupRoutingCode = c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.Code : "",
                        ORDGroupRoutingName = c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<FINRefresh_OPSGroup>;
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

        public DTOResult FINRefresh_OPS_Master_List(string request, DateTime from, DateTime to)
        {
            try
            {
                var result = new DTOResult();
                using (var model = new DataEntities())
                {
                    from = from.Date;
                    to = to.Date.AddDays(1);
                    var query = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.ETD >= from && c.ETA < to && c.StatusOfDITOMasterID >= (int)SYSVarType.StatusOfDITOMasterReceived).Select(c => new FINRefresh_OPSMaster
                    {
                        ID = c.ID,
                        ToMasterID = c.ID,
                        ToMasterCode = c.Code,
                        VendorID = c.VendorOfVehicleID > 0 ? c.VendorOfVehicleID : Account.SYSCustomerID,
                        VehicleNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                        VendorName = c.VendorOfVehicleID > 0 && c.VendorOfVehicleID != Account.SYSCustomerID ? c.CUS_Customer.CustomerName : "Xe nhà",
                        TransportModeID = c.TransportModeID,
                        TransportModeName = c.TransportModeID > 0 ? c.CAT_TransportMode.Name : string.Empty,
                        ContractID = c.ContractID,
                        ContractNo = c.ContractID > 0 ? c.CAT_Contract.DisplayName : string.Empty,
                        ETD = c.ETD,
                        ETA = c.ETA,
                        RoutingID = c.CATRoutingID,
                        //TypeOfDriverRouteFeeID = c.TypeOfDriverRouteFeeID,
                        DateConfig = c.DateConfig,
                        //ServiceOfOrderID = c.ContractID.HasValue ? c.CAT_Contract.ServiceOfOrderID : -1,
                        GroupOfVehicleID = c.GroupOfVehicleID.HasValue ? c.GroupOfVehicleID : -1,
                        GroupOfVehicleName = c.GroupOfVehicleID.HasValue ? c.CAT_GroupOfVehicle.GroupName : string.Empty,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<FINRefresh_OPSMaster>;
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

        public void FINRefresh_OPS_Group_Save(FINRefresh_OPSGroup item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var objGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                    if (objGroup != null)
                    {
                        var obj = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == objGroup.DITOMasterID);

                        obj.DateConfig = item.DateConfigMaster;
                        objGroup.DateConfig = item.DateConfig;
                        if (item.TransportModeID > 0)
                            obj.TransportModeID = item.TransportModeID;
                        else
                            obj.TransportModeID = null;
                        if (item.GroupOfVehicleID > 0)
                            obj.GroupOfVehicleID = item.GroupOfVehicleID;
                        else
                            obj.GroupOfVehicleID = null;
                        if (item.ContractID > 0)
                            obj.ContractID = item.ContractID;
                        else
                            obj.ContractID = null;

                        //luu route master ->cus,cat
                        if (item.RoutingID > 0)
                            obj.CATRoutingID = item.RoutingID;
                        else
                            obj.CATRoutingID = null;
                        if (item.RoutingIDGroup > 0)
                            objGroup.CATRoutingID = item.RoutingIDGroup;
                        else
                            objGroup.CATRoutingID = null;

                        objGroup.ModifiedBy = Account.UserName;
                        objGroup.ModifiedDate = DateTime.Now;
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;

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

        public void FINRefresh_OPS_Master_Save(FINRefresh_OPSMaster item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    //var obj = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.ToMasterID);
                    //if (obj != null)
                    //{
                    //    if (item.TransportModeID > 0)
                    //        obj.TransportModeID = item.TransportModeID;
                    //    else
                    //        obj.TransportModeID = null;

                    //    if (item.ContractID > 0)
                    //        obj.ContractID = item.ContractID;
                    //    else
                    //        obj.ContractID = null;

                    //    if (item.RoutingID > 0)
                    //        obj.CATRoutingID = item.RoutingID;
                    //    else
                    //        obj.CATRoutingID = null;

                    //    if (item.TypeOfDriverRouteFeeID > 0)
                    //        obj.TypeOfDriverRouteFeeID = item.TypeOfDriverRouteFeeID;
                    //    else
                    //        obj.TypeOfDriverRouteFeeID = null;

                    //    if (obj.GroupOfVehicleID > 0) obj.GroupOfVehicleID = item.GroupOfVehicleID;
                    //    else obj.GroupOfVehicleID = null;

                    //    obj.DateConfig = item.DateConfig;

                    //    obj.ModifiedBy = Account.UserName;
                    //    obj.ModifiedDate = DateTime.Now;

                    //    model.SaveChanges();
                    //}
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

        public List<DTOCATRouting> FINRefresh_OPSRouting_List(int? routingID, int venID, int? contractID)
        {
            try
            {
                List<DTOCATRouting> result = new List<DTOCATRouting>();
                using (var model = new DataEntities())
                {
                    if (venID == Account.SYSCustomerID)
                    {
                        result = model.CAT_Routing.Where(c => c.ID == routingID).Select(c => new DTOCATRouting
                        {
                            ID = c.ID,
                            Code = c.Code,
                            RoutingName = c.RoutingName
                        }).ToList();
                    }
                    else
                    {
                        if (contractID > 0)
                        {
                            result = model.CAT_ContractRouting.Where(c => c.ContractID == contractID).Select(c => new DTOCATRouting
                            {
                                ID = c.RoutingID,
                                Code = c.CAT_Routing.Code,
                                RoutingName = c.CAT_Routing.RoutingName
                            }).ToList();
                        }
                        else
                        {
                            result = model.CUS_Routing.Where(c => c.CustomerID == venID && c.CAT_Routing.Code != string.Empty).Select(c => new DTOCATRouting
                            {
                                ID = c.RoutingID,
                                Code = c.CAT_Routing.Code,
                                RoutingName = c.CAT_Routing.RoutingName
                            }).ToList();
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

        public List<DTOCATRouting> FINRefresh_OPSGroupRouting_List(int? routingID, int venID, int? contractID)
        {
            try
            {
                List<DTOCATRouting> result = new List<DTOCATRouting>();
                using (var model = new DataEntities())
                {
                    if (venID == Account.SYSCustomerID)
                    {
                        result = model.CAT_Routing.Where(c => c.ID == routingID).Select(c => new DTOCATRouting
                        {
                            ID = c.ID,
                            Code = c.Code,
                            RoutingName = c.RoutingName
                        }).ToList();
                    }
                    else
                    {
                        if (contractID > 0)
                        {
                            result = model.CAT_ContractRouting.Where(c => c.ContractID == contractID).Select(c => new DTOCATRouting
                                {
                                    ID = c.RoutingID,
                                    RoutingName = c.CAT_Routing.RoutingName,
                                }).ToList();
                        }
                        else
                        {
                            result = model.CAT_Routing.Where(c => !string.IsNullOrEmpty(c.Code)).Select(c => new DTOCATRouting
                                {
                                    ID = c.ID,
                                    RoutingName = c.RoutingName
                                }).ToList();
                        }
                    }
                    DTOCATRouting objNull = new DTOCATRouting { ID = -1, RoutingName = string.Empty, Code = string.Empty };
                    result.Insert(0, objNull);
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

        #region Điều chỉnh Container
        public DTOResult FINRefresh_ORD_Container_List(string request, DateTime from, DateTime to)
        {
            try
            {
                var result = new DTOResult();
                using (var model = new DataEntities())
                {
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();

                    var query = model.ORD_Container.Where(c => (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ORD_Order.CustomerID)) && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_Order.DateConfig >= from && c.ORD_Order.DateConfig <= to && c.ORD_Order.StatusOfOrderID > (int)SYSVarType.StatusOfOrderNew).Select(c => new FINRefresh_ORDContainer
                    {
                        ID = c.ID,
                        OrderID = c.OrderID,
                        OrderCode = c.ORD_Order.Code,
                        CustomerID = c.ORD_Order.CustomerID,
                        CustomerCode = c.ORD_Order.CUS_Customer.Code,
                        CustomerName = c.ORD_Order.CUS_Customer.CustomerName,
                        ServiceOfOrderID = c.ORD_Order.ServiceOfOrderID,
                        TransportModeID = c.ORD_Order.TransportModeID,
                        ContractID = c.ORD_Order.ContractID > 0 ? c.ORD_Order.ContractID.Value : -1,
                        ContractTermID = c.ORD_Order.ContractTermID > 0 ? c.ORD_Order.ContractTermID.Value : -1,
                        ContractTermNo = c.ORD_Order.ContractTermID > 0 ? c.ORD_Order.CAT_ContractTerm.DisplayName : "",
                        ETD = c.ORD_Order.ETD,
                        ETA = c.ORD_Order.ETA,
                        RequestDate = c.ORD_Order.RequestDate,
                        ContainerNo = c.ContainerNo,
                        SealNo1 = c.SealNo1,
                        SealNo2 = c.SealNo2,
                        DateConfig = c.DateConfig,
                        OrderDateConfig = c.ORD_Order.DateConfig,
                        PackingCode = c.CAT_Packing.Code,
                        ContainerETA = c.ETA,
                        ContainerETD = c.ETD,
                        Ton = c.Ton,
                        RoutingID = c.CUSRoutingID > 0 ? c.CUSRoutingID.Value : -1,
                        RoutingName = c.CUSRoutingID > 0 ? c.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                        LocationFromAddress = c.CUS_Location2.CAT_Location.Address,
                        LocationToAddress = c.CUS_Location3.CAT_Location.Address,
                        LocationDepotAddress = c.LocationDepotID > 0 ? c.CUS_Location.CAT_Location.Address : "",
                        LocationDepotReturnAddress = c.LocationDepotReturnID > 0 ? c.CUS_Location1.CAT_Location.Address : "",
                        ORDRoutingID = c.ORD_Order.CUSRoutingID > 0 ? c.ORD_Order.CUSRoutingID.Value : -1,
                        ORDRoutingName = c.ORD_Order.CUSRoutingID > 0 ? c.ORD_Order.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<FINRefresh_ORDContainer>;
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

        public void FINRefresh_ORD_Container_Save(FINRefresh_ORDContainer item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.ORD_Container.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        var objORD = model.ORD_Order.FirstOrDefault(c => c.ID == obj.OrderID);
                        objORD.ETD = item.ETD;
                        objORD.ETA = item.ETA;
                        objORD.DateConfig = item.OrderDateConfig;
                        objORD.RequestDate = item.RequestDate;
                        if (item.ContractID > 0)
                            objORD.ContractID = item.ContractID;
                        else
                            objORD.ContractID = null;
                        if (item.ContractTermID > 0)
                            objORD.ContractTermID = item.ContractTermID;
                        else
                            objORD.ContractTermID = null;

                        if (item.RoutingID > 0)
                            obj.CUSRoutingID = item.RoutingID;
                        else
                            obj.CUSRoutingID = null;
                        if (item.ORDRoutingID > 0)
                            objORD.CUSRoutingID = item.ORDRoutingID;
                        else
                            objORD.CUSRoutingID = null;
                        obj.ETD = item.ContainerETD;
                        obj.ETA = item.ContainerETA;
                        obj.DateConfig = item.DateConfig;

                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        objORD.ModifiedBy = Account.UserName;
                        objORD.ModifiedDate = DateTime.Now;

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

        public DTOResult FINRefresh_OPS_Container_List(string request, DateTime from, DateTime to)
        {
            try
            {
                var result = new DTOResult();
                using (var model = new DataEntities())
                {
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();

                    var query = model.OPS_COTOContainer.Where(c => (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID)) && c.COTOMasterID > 0 && c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_COTOMaster.DateConfig >= from && c.OPS_COTOMaster.DateConfig <= to && c.OPS_Container.ORD_Container.ORD_Order.StatusOfOrderID > (int)SYSVarType.StatusOfOrderNew).Select(c => new FINRefresh_OPSContainer
                    {
                        ID = c.ID,
                        COMasterID = c.COTOMasterID.Value,
                        COMasterCode = c.OPS_COTOMaster.Code,
                        OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                        VendorID = c.OPS_COTOMaster.VendorOfVehicleID > 0 ? c.OPS_COTOMaster.VendorOfVehicleID : Account.SYSCustomerID,
                        VehicleNo = c.OPS_COTOMaster.VehicleID > 0 ? c.OPS_COTOMaster.CAT_Vehicle.RegNo : "",
                        VendorName = c.OPS_COTOMaster.VendorOfVehicleID > 0 && c.OPS_COTOMaster.VendorOfVehicleID != Account.SYSCustomerID ? c.OPS_COTOMaster.CUS_Customer.CustomerName : "Xe nhà",
                        ServiceOfOrderID = c.OPS_Container.ORD_Container.ORD_Order.ServiceOfOrderID,
                        TransportModeID = c.OPS_COTOMaster.TransportModeID,
                        TransportModeName = c.OPS_COTOMaster.TransportModeID > 0 ? c.OPS_COTOMaster.CAT_TransportMode.Name : string.Empty,
                        ContractID = c.OPS_COTOMaster.ContractID,
                        ContractTermID = c.ContractTermID > 0 ? c.ContractTermID.Value : -1,
                        ContractTermNo = c.ContractTermID > 0 ? c.CAT_ContractTerm.DisplayName : "",
                        ContractNo = c.OPS_COTOMaster.ContractID > 0 ? c.OPS_COTOMaster.CAT_Contract.DisplayName : string.Empty,
                        TypeOfOrderID = c.OPS_COTOMaster.TypeOfOrderID,
                        ETD = c.OPS_COTOMaster.ETD,
                        ETA = c.OPS_COTOMaster.ETA,
                        ContainerNo = c.OPS_Container.ContainerNo,
                        SealNo1 = c.OPS_Container.SealNo1,
                        SealNo2 = c.OPS_Container.SealNo2,
                        PackingCode = c.OPS_Container.ORD_Container.CAT_Packing.Code,
                        RoutingID = c.OPS_COTOMaster.RoutingID,
                        RequestDate = c.OPS_Container.ORD_Container.ORD_Order.RequestDate,
                        CustormerCode = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                        CustormerName = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.CustomerName,
                        DateConfig = c.OPS_COTOMaster.DateConfig,
                        DateConfigMaster = c.OPS_COTOMaster.DateConfig,
                        RoutingIDGroup = c.CATRoutingID,
                        ORDContainerRoutingID = c.OPS_Container.ORD_Container.CUSRoutingID,
                        ORDContainerRoutingName = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                        LocationFromAddress = c.CAT_Location.Address,
                        LocationToAddress = c.CAT_Location1.Address,
                        LocationDepotAddress = c.OPS_Container.LocationDepotID > 0 ? c.OPS_Container.CAT_Location.Address : "",
                        LocationDepotReturnAddress = c.OPS_Container.LocationDepotReturnID > 0 ? c.OPS_Container.CAT_Location.Address : "",
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<FINRefresh_OPSContainer>;
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

        public void FINRefresh_OPS_Container_Save(FINRefresh_OPSContainer item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var objGroup = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item.ID);
                    if (objGroup != null)
                    {
                        var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == objGroup.COTOMasterID);

                        obj.DateConfig = item.DateConfigMaster;

                        if (item.TransportModeID > 0)
                            obj.TransportModeID = item.TransportModeID;
                        else
                            obj.TransportModeID = null;
                        if (item.ContractID > 0)
                            obj.ContractID = item.ContractID;
                        else
                            obj.ContractID = null;
                        if (item.ContractTermID > 0)
                            objGroup.ContractTermID = item.ContractTermID;
                        else
                            objGroup.ContractTermID = null;

                        //luu route master ->cus,cat
                        if (item.RoutingID > 0)
                            obj.RoutingID = item.RoutingID;
                        else
                            obj.RoutingID = null;
                        if (item.RoutingIDGroup > 0)
                            objGroup.CATRoutingID = item.RoutingIDGroup;
                        else
                            objGroup.CATRoutingID = null;

                        objGroup.ModifiedBy = Account.UserName;
                        objGroup.ModifiedDate = DateTime.Now;
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;

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
        #endregion

        #region Freight Audit
        #region Truck
        public DTOResult FINFreightAudit_List(DateTime pDateFrom, DateTime pDateTo, int statusID, string request)
        {
            try
            {
                List<int> lstNoGroupDebit = new List<int>();
                lstNoGroupDebit.Add((int)CATCostType.DITOFreightNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQLoadNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQUnLoadNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOUnLoadDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOLoadDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOReturnDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOUnLoadReturnDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOLoadReturnDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOExDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOExNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.ManualFixDebit);
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    pDateFrom = pDateFrom.Date;
                    pDateTo = pDateTo.AddDays(1).Date;

                    var query = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfPaymentDITOMasterID == statusID && c.DateConfig.HasValue && c.DateConfig >= pDateFrom && c.DateConfig < pDateTo && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.TypeOfPaymentDITOMasterID.HasValue && c.VendorOfVehicleID.HasValue && (c.VendorOfVehicleID != null && c.VendorOfVehicleID != Account.SYSCustomerID) && Account.ListCustomerID.Contains(c.VendorOfVehicleID.Value) && c.FIN_PL.Count(d => d.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && d.Credit == 0) > 0).Select(c => new FINFreightAudit
                    {
                        TOMasterID = c.ID,
                        TOMasterCode = c.Code,
                        DriverName = c.DriverName1 == null ? string.Empty : c.DriverName1,
                        RegNo = c.VehicleID.HasValue ? c.CAT_Vehicle.RegNo : string.Empty,
                        DateConfig = c.DateConfig.Value,
                        Debit = 0,
                        StatusID = c.TypeOfPaymentDITOMasterID.Value,
                        StatusName = c.SYS_Var3.ValueOfVar,
                        Credit = 0,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.CustomerName,
                        IsChoose = false,
                        PayUserModified = c.PayUserModified,
                        PayUserNote = c.PayUserNote,
                        PayVendorModified = c.PayVendorModified,
                        PayVendorNote = c.PayVendorNote
                    }).OrderBy(c => c.DateConfig).ThenBy(c => c.TOMasterCode).ToList();

                    foreach (FINFreightAudit item in query)
                    {
                        var lstCostFTL = model.FIN_PLDetails.Where(c => c.FIN_PL.DITOMasterID == item.TOMasterID && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.CostID == (int)CATCostType.DITOFreightDebit).Select(c => new
                        {
                            Cost = c.Debit
                        }).ToList();

                        var lstOther = model.FIN_PLDetails.Where(c => c.FIN_PL.DITOMasterID == item.TOMasterID && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && lstNoGroupDebit.Contains(c.CostID)).Select(c => new
                        {
                            Cost = c.Debit
                        }).ToList();

                        var lstTroubleCredit = model.FIN_PLDetails.Where(c => c.FIN_PL.DITOMasterID == item.TOMasterID && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.CostID == (int)CATCostType.TroubleDebit && c.Debit > 0).Select(c => new
                        {
                            Cost = c.Debit
                        }).ToList();

                        var lstTroubleDebit = model.FIN_PLDetails.Where(c => c.FIN_PL.DITOMasterID == item.TOMasterID && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.CostID == (int)CATCostType.TroubleDebit && c.Debit < 0).Select(c => new
                        {
                            Cost = c.Debit
                        }).ToList();

                        item.Credit = lstCostFTL.Sum(c => c.Cost);
                        item.Credit += lstOther.Sum(c => c.Cost);
                        item.Credit += lstTroubleCredit.Sum(c => c.Cost);
                        item.Debit = lstTroubleDebit.Sum(c => c.Cost);
                    }

                    result.Total = query.ToDataSourceResult(CreateRequest(request)).Total;
                    result.Data = query.ToDataSourceResult(CreateRequest(request)).Data as IEnumerable<FINFreightAudit>;
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

        public FINFreightAudit_Detail FINFreightAudit_DetailList(int masterid)
        {
            try
            {
                List<int> lstNoGroupDebit = new List<int>();
                lstNoGroupDebit.Add((int)CATCostType.DITOFreightDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOFreightNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQLoadNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQUnLoadNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOUnLoadDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOLoadDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOReturnDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOUnLoadReturnDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOLoadReturnDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOExDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOExNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.ManualFixDebit);
                FINFreightAudit_Detail result = new FINFreightAudit_Detail();
                result.lstCredit = new List<FINFreightAudit_Credit>();
                result.lstTrouble = new List<FINFreightAudit_Other>();
                using (var model = new DataEntities())
                {
                    var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == masterid);
                    int vendorid = master.VendorOfVehicleID.HasValue ? master.VendorOfVehicleID.Value : Account.SYSCustomerID;
                    var lstMapping = model.CUS_GroupOfProductMapping.Where(c => c.VendorID == vendorid).Select(c => new
                        {
                            c.GroupOfProductCUSID,
                            c.GroupOfProductVENID,
                            GroupOfProductNameCUS = c.CUS_GroupOfProduct.GroupName,
                            GroupOfProductNameVEN = c.CUS_GroupOfProduct1.GroupName,
                        }).ToList();

                    result.lstCredit = model.FIN_PLGroupOfProduct.Where(c => c.FIN_PLDetails.FIN_PL.DITOMasterID == masterid && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && lstNoGroupDebit.Contains(c.FIN_PLDetails.CostID)).Select(c => new FINFreightAudit_Credit
                        {
                            ID = c.ID,
                            PLDetailID = c.PLDetailID,
                            CostID = c.FIN_PLDetails.CostID,
                            CostName = c.FIN_PLDetails.CAT_Cost.CostName,
                            LocationFromID = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationID,
                            LocationFromName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.Location,
                            LocationToID = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationID,
                            LocationToName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Location,
                            OPSGroupID = c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID,
                            GroupOfProductNameCUS = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                            Unit = !string.IsNullOrEmpty(c.Unit) ? c.Unit : c.FIN_PLDetails.Unit,
                            UnitPrice = c.FIN_PLDetails.UnitPrice.HasValue ? c.FIN_PLDetails.UnitPrice.Value : c.UnitPrice,
                            Quantity = c.FIN_PLDetails.Quantity.HasValue ? c.FIN_PLDetails.Quantity.Value : c.Quantity,
                            Cost = c.FIN_PLDetails.Debit,
                            Note = c.FIN_PLDetails.Note,
                            Note1 = c.FIN_PLDetails.Note1,
                            RoutingCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Routing.CAT_Routing.Code,
                            RoutingName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName,
                            DateFromCome = c.OPS_DITOGroupProduct.DateFromCome,
                            DateFromLoadStart = c.OPS_DITOGroupProduct.DateFromLoadStart,
                            DateFromLoadEnd = c.OPS_DITOGroupProduct.DateFromLoadEnd,
                            DateFromLeave = c.OPS_DITOGroupProduct.DateFromLeave,
                            DateToCome = c.OPS_DITOGroupProduct.DateToCome,
                            DateToLoadStart = c.OPS_DITOGroupProduct.DateToLoadStart,
                            DateToLoadEnd = c.OPS_DITOGroupProduct.DateToLoadEnd,
                            DateToLeave = c.OPS_DITOGroupProduct.DateToLeave,

                        }).ToList();

                    foreach (var item in result.lstCredit)
                    {
                        if (item.UnitPrice > 0 || item.Quantity > 0)
                            item.Cost = item.UnitPrice * (decimal)item.Quantity;

                        var venGroup = lstMapping.FirstOrDefault(c => c.GroupOfProductCUSID == item.OPSGroupID);
                        if (venGroup != null)
                            item.GroupOfProductNameVEN = venGroup.GroupOfProductNameVEN;
                    }

                    result.lstTrouble = model.FIN_PLDetails.Where(c => c.FIN_PL.DITOMasterID == masterid && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.CostID == (int)CATCostType.TroubleDebit).Select(c => new FINFreightAudit_Other
                    {
                        CostID = c.CostID,
                        CostName = c.CAT_Cost.CostName,
                        Cost = c.Debit,
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

        public List<SYSVar> FINFreightAudit_StatusList()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TypeOfPaymentDITOMaster).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        ValueOfVar = c.ValueOfVar
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

        public void FINFreightAudit_Reject(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item);
                            if (master != null)
                            {
                                master.PayVendorModified = Account.UserName;
                                master.PayVendorNote = Note;
                                master.TypeOfPaymentDITOMasterID = -(int)SYSVarType.TypeOfPaymentDITOMasterReject;
                            }
                        }
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

        public void FINFreightAudit_Accept(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item);
                            if (master != null)
                            {
                                master.PayVendorModified = Account.UserName;
                                master.PayVendorNote = Note;
                                master.TypeOfPaymentDITOMasterID = -(int)SYSVarType.TypeOfPaymentDITOMasterWait;
                            }
                        }
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

        public void FINFreightAudit_Waiting(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item);
                            if (master != null)
                            {
                                master.PayUserModified = Account.UserName;
                                master.PayUserNote = Note;
                                master.TypeOfPaymentDITOMasterID = -(int)SYSVarType.TypeOfPaymentDITOMasterOpen;
                            }
                        }
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

        public void FINFreightAudit_Approved(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item);
                            if (master != null)
                            {
                                master.PayUserModified = Account.UserName;
                                master.PayUserNote = Note;
                                master.TypeOfPaymentDITOMasterID = -(int)SYSVarType.TypeOfPaymentDITOMasterApproved;
                            }
                        }
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

        public List<FINFreightAudit_Export> FINFreightAudit_Export(DateTime pDateFrom, DateTime pDateTo, int statusID)
        {
            try
            {
                List<int> lstNoGroupDebit = new List<int>();
                lstNoGroupDebit.Add((int)CATCostType.DITOFreightDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOFreightNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQLoadNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOMOQUnLoadNoGroupDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOUnLoadDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOLoadDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOReturnDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOUnLoadReturnDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOLoadReturnDebit);
                lstNoGroupDebit.Add((int)CATCostType.DITOExDebit);
                List<FINFreightAudit_Export> result = new List<FINFreightAudit_Export>();
                using (var model = new DataEntities())
                {
                    pDateFrom = pDateFrom.Date;
                    pDateTo = pDateTo.AddDays(1).Date;

                    result = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfPaymentDITOMasterID == statusID && c.DateConfig.HasValue && c.DateConfig >= pDateFrom && c.DateConfig < pDateTo && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.TypeOfPaymentDITOMasterID.HasValue && c.VendorOfVehicleID.HasValue && (c.VendorOfVehicleID != null && c.VendorOfVehicleID != Account.SYSCustomerID) && Account.ListCustomerID.Contains(c.VendorOfVehicleID.Value)).Select(c => new FINFreightAudit_Export
                    {
                        TOMasterID = c.ID,
                        TOMasterCode = c.Code,
                        DriverName = c.DriverName1 == null ? string.Empty : c.DriverName1,
                        RegNo = c.VehicleID.HasValue ? c.CAT_Vehicle.RegNo : string.Empty,
                        DateConfig = c.DateConfig.Value,
                        Debit = 0,
                        StatusID = c.TypeOfPaymentDITOMasterID.Value,
                        StatusName = c.SYS_Var3.ValueOfVar,
                        Credit = 0,
                        VendorID = c.VendorOfVehicleID.HasValue ? c.VendorOfVehicleID.Value : Account.SYSCustomerID,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.CustomerName,
                        IsChoose = false,
                        PayUserModified = c.PayUserModified,
                        PayUserNote = c.PayUserNote,
                        PayVendorModified = c.PayVendorModified,
                        PayVendorNote = c.PayVendorNote
                    }).ToList();

                    var lstVendorID = result.Select(c => c.TOMasterID).Distinct().ToList();
                    // Nhóm hàng Mapping
                    var lstMapping = model.CUS_GroupOfProductMapping.Where(c => lstVendorID.Contains(c.VendorID)).Select(c => new
                    {
                        c.VendorID,
                        c.GroupOfProductCUSID,
                        c.GroupOfProductVENID,
                        GroupOfProductNameCUS = c.CUS_GroupOfProduct.GroupName,
                        GroupOfProductNameVEN = c.CUS_GroupOfProduct1.GroupName,
                    }).ToList();

                    foreach (var item in result)
                    {
                        item.lstCredit = new List<FINFreightAudit_Credit>();
                        item.lstTrouble = new List<FINFreightAudit_Other>();

                        item.lstCredit = model.FIN_PLGroupOfProduct.Where(c => c.FIN_PLDetails.FIN_PL.DITOMasterID == item.TOMasterID && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && lstNoGroupDebit.Contains(c.FIN_PLDetails.CostID)).Select(c => new FINFreightAudit_Credit
                        {
                            ID = c.ID,
                            CostID = c.FIN_PLDetails.CostID,
                            CostName = c.FIN_PLDetails.CAT_Cost.CostName,
                            LocationFromID = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationID,
                            LocationFromName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.Location,
                            LocationToID = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationID,
                            LocationToName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Location,
                            OPSGroupID = c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID,
                            GroupOfProductNameCUS = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                            Unit = !string.IsNullOrEmpty(c.Unit) ? c.Unit : c.FIN_PLDetails.Unit,
                            UnitPrice = c.FIN_PLDetails.UnitPrice.HasValue ? c.FIN_PLDetails.UnitPrice.Value : c.UnitPrice,
                            Quantity = c.FIN_PLDetails.Quantity.HasValue ? c.FIN_PLDetails.Quantity.Value : c.Quantity,
                            Cost = c.FIN_PLDetails.Debit,
                            Note = c.FIN_PLDetails.Note,
                            Note1 = c.FIN_PLDetails.Note1,
                            RoutingCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Routing.CAT_Routing.Code,
                            RoutingName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName,
                            DateFromCome = c.OPS_DITOGroupProduct.DateFromCome,
                            DateFromLoadStart = c.OPS_DITOGroupProduct.DateFromLoadStart,
                            DateFromLoadEnd = c.OPS_DITOGroupProduct.DateFromLoadEnd,
                            DateFromLeave = c.OPS_DITOGroupProduct.DateFromLeave,
                            DateToCome = c.OPS_DITOGroupProduct.DateToCome,
                            DateToLoadStart = c.OPS_DITOGroupProduct.DateToLoadStart,
                            DateToLoadEnd = c.OPS_DITOGroupProduct.DateToLoadEnd,
                            DateToLeave = c.OPS_DITOGroupProduct.DateToLeave,
                        }).ToList();

                        foreach (var itemCredit in item.lstCredit)
                        {
                            if (itemCredit.UnitPrice > 0 || itemCredit.Quantity > 0)
                                itemCredit.Cost = itemCredit.UnitPrice * (decimal)itemCredit.Quantity;

                            var venGroup = lstMapping.FirstOrDefault(c => c.VendorID == item.VendorID && c.GroupOfProductCUSID == itemCredit.OPSGroupID);
                            if (venGroup != null)
                                itemCredit.GroupOfProductNameVEN = venGroup.GroupOfProductNameVEN;
                        }

                        item.lstTrouble = model.FIN_PLDetails.Where(c => c.FIN_PL.DITOMasterID == item.TOMasterID && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.CostID == -(int)CATCostType.TroubleDebit).Select(c => new FINFreightAudit_Other
                        {
                            CostID = c.CostID,
                            CostName = c.CAT_Cost.CostName,
                            Cost = c.Credit,
                            Note = c.Note
                        }).ToList();

                        item.Credit = item.lstCredit.Sum(c => c.Cost);
                        item.Debit = item.lstTrouble.Sum(c => c.Cost);
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

        #region Container
        public List<SYSVar> FINFreightAuditCON_StatusList()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TypeOfPaymentCOTOMaster).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        ValueOfVar = c.ValueOfVar
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
        public DTOResult FINFreightAuditCON_List(DateTime pDateFrom, DateTime pDateTo, int statusID, bool IsOwner, string request)
        {
            try
            {
                List<int> lstNoGroupDebit = new List<int>();
                lstNoGroupDebit.Add((int)CATCostType.COTOFreightDebit);
                lstNoGroupDebit.Add((int)CATCostType.ManualFixDebit);
                lstNoGroupDebit.Add((int)CATCostType.ORDContainerServiceDebit);
                lstNoGroupDebit.Add((int)CATCostType.ORDDocumentDebit);

                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    pDateFrom = pDateFrom.Date;
                    pDateTo = pDateTo.AddDays(1).Date;

                    var query = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID
                        && c.OPS_COTOMaster.TypeOfPaymentCOTOMasterID.HasValue && c.ParentID == null
                        && c.OPS_COTOMaster.TypeOfPaymentCOTOMasterID == statusID && c.OPS_COTOMaster.DateConfig >= pDateFrom && c.OPS_COTOMaster.DateConfig < pDateTo
                        && (IsOwner ? (c.OPS_COTOMaster.VendorOfVehicleID == null || c.OPS_COTOMaster.VendorOfVehicleID == Account.SYSCustomerID) : (c.OPS_COTOMaster.VendorOfVehicleID > 0 && c.OPS_COTOMaster.VendorOfVehicleID != Account.SYSCustomerID && Account.ListCustomerID.Contains(c.OPS_COTOMaster.VendorOfVehicleID.Value)))).Select(c => new FINFreightAuditCON
                    {
                        ID = c.ID,
                        OrderID = c.OPS_Container.ORD_Container.OrderID,
                        OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                        TOMasterID = c.COTOMasterID,
                        TOMasterCode = c.COTOMasterID > 0 ? c.OPS_COTOMaster.Code : "",
                        DateConfig = c.OPS_COTOMaster.DateConfig.HasValue ? c.OPS_COTOMaster.DateConfig.Value : c.OPS_Container.ORD_Container.ORD_Order.DateConfig,
                        StatusID = c.OPS_COTOMaster.TypeOfPaymentCOTOMasterID.Value,
                        StatusName = c.OPS_COTOMaster.SYS_Var3.ValueOfVar,
                        Debit = 0,
                        IsChoose = false,
                        PayUserModified = c.OPS_COTOMaster.PayUserModified,
                        PayUserNote = c.OPS_COTOMaster.PayUserNote,
                        PayVendorModified = c.OPS_COTOMaster.PayVendorModified,
                        PayVendorNote = c.OPS_COTOMaster.PayVendorNote,
                        VendorCode = c.OPS_COTOMaster.CUS_Customer.Code,
                        VendorName = c.OPS_COTOMaster.CUS_Customer.CustomerName,
                    }).ToList();

                    //var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.ORD_Order.SYSCustomerID == Account.SYSCustomerID 
                    //    && c.OPS_Container.ORD_Container.TypeOfWAPaymentORDContainerID.HasValue 
                    //    && c.OPS_Container.ORD_Container.TypeOfWAPaymentORDContainerID == statusID 
                    //    && c.OPS_Container.ORD_Container.DateConfig >= pDateFrom && c.OPS_Container.ORD_Container.DateConfig < pDateTo 
                    //    && Account.ListCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID) 
                    //    && c.OPS_Container.OPSContainerTypeID == -(int)SYSVarType.OPSContainerTypeComplete).Select(c => new
                    //{
                    //    OrderContainerID = c.OPS_Container.ContainerID,
                    //    ID = c.ID,
                    //}).ToList();

                    foreach (FINFreightAuditCON item in query)
                    {
                        //var lstOPSContainerID = lstOPSContainer.Where(c => c.OrderContainerID == item.ID).Select(c => c.ID).ToList();
                        var lstDebit = model.FIN_PLContainer.Where(c => c.COTOContainerID == item.ID && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && lstNoGroupDebit.Contains(c.FIN_PLDetails.CostID)).Select(c => new FINFreightAudit_Credit
                        {
                            Cost = c.FIN_PLDetails.Debit
                        }).ToList();

                        var lstTrouble = model.FIN_PLContainer.Where(c => c.COTOContainerID == item.ID && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.FIN_PLDetails.CostID == (int)CATCostType.TroubleDebit).Select(c => new { c.FIN_PLDetails.Debit }).ToList();

                        item.Debit += lstDebit.Sum(c => c.Cost);
                        item.Debit += lstTrouble.Sum(c => c.Debit);
                    }

                    result.Total = query.ToDataSourceResult(CreateRequest(request)).Total;
                    result.Data = query.ToDataSourceResult(CreateRequest(request)).Data;
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

        public FINFreightAuditCON_Detail FINFreightAudit_CONDetail_List(int id)
        {
            try
            {
                List<int> lstNoGroupDebit = new List<int>();
                lstNoGroupDebit.Add((int)CATCostType.COTOFreightDebit);
                lstNoGroupDebit.Add((int)CATCostType.ManualFixDebit);
                lstNoGroupDebit.Add((int)CATCostType.ORDContainerServiceDebit);
                lstNoGroupDebit.Add((int)CATCostType.ORDDocumentDebit);

                FINFreightAuditCON_Detail result = new FINFreightAuditCON_Detail();
                result.lstCredit = new List<FINFreightAudit_Credit>();
                result.lstTrouble = new List<FINFreightAudit_Other>();
                using (var model = new DataEntities())
                {
                    result = model.OPS_COTOContainer.Where(c => c.ID == id).Select(c => new FINFreightAuditCON_Detail
                    {
                        ID = c.ID,
                        DateConfig = c.OPS_COTOMaster.DateConfig.HasValue ? c.OPS_COTOMaster.DateConfig.Value : c.OPS_COTOMaster.ETD,
                        OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                        ContractID = c.OPS_COTOMaster.ContractID,
                        ContractName = c.OPS_COTOMaster.ContractID > 0 ? c.OPS_COTOMaster.CAT_Contract.DisplayName : "",
                        ContractTermID = c.ContractTermID,
                        ContractTermName = c.ContractTermID > 0 ? c.CAT_ContractTerm.TermName : "",
                        CATRoutingID = c.CATRoutingID,
                        CATRoutingName = c.CATRoutingID > 0 ? c.CAT_Routing.RoutingName : "",
                        CustomerID = c.OPS_COTOMaster.VendorOfVehicleID > 0 ? c.OPS_COTOMaster.VendorOfVehicleID.Value : -1,
                    }).FirstOrDefault();

                    result.lstCredit = model.FIN_PLContainer.Where(c => c.COTOContainerID == id && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && lstNoGroupDebit.Contains(c.FIN_PLDetails.CostID)).Select(c => new FINFreightAudit_Credit
                    {
                        ID = c.ID,
                        CostID = c.FIN_PLDetails.CostID,
                        CostName = c.FIN_PLDetails.CAT_Cost.CostName,
                        LocationFromID = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Location.LocationID,
                        LocationFromName = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Location.CAT_Location.Address,
                        LocationToID = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Location1.LocationID,
                        LocationToName = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Location1.CAT_Location.Address,
                        OPSGroupID = c.COTOContainerID,
                        Unit = !string.IsNullOrEmpty(c.Unit) ? c.Unit : c.FIN_PLDetails.Unit,
                        UnitPrice = c.FIN_PLDetails.UnitPrice.HasValue ? c.FIN_PLDetails.UnitPrice.Value : c.UnitPrice,
                        Quantity = c.FIN_PLDetails.Quantity.HasValue ? c.FIN_PLDetails.Quantity.Value : c.Quantity,
                        Cost = c.FIN_PLDetails.Debit,
                        Note = c.FIN_PLDetails.Note,
                        Note1 = c.FIN_PLDetails.Note1,
                        RoutingCode = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Routing.CAT_Routing.Code,
                        RoutingName = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Routing.CAT_Routing.RoutingName,
                    }).ToList();

                    foreach (var item in result.lstCredit)
                    {
                        if (item.UnitPrice > 0 || item.Quantity > 0)
                            item.Cost = item.UnitPrice * (decimal)item.Quantity;
                    }

                    result.lstTrouble = model.FIN_PLContainer.Where(c => c.COTOContainerID == id && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.FIN_PLDetails.CostID == (int)CATCostType.TroubleDebit).Select(c => new FINFreightAudit_Other
                    {
                        CostID = c.FIN_PLDetails.CostID,
                        CostName = c.FIN_PLDetails.CAT_Cost.CostName,
                        Cost = c.FIN_PLDetails.Debit,
                        Note = c.FIN_PLDetails.Note
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

        public void FINFreightAudit_CONDetail_Save(FINFreightAuditCON_Detail item)
        {
            try
            {
                DTOError result = new DTOError();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu");
                    }
                    else
                    {
                        obj.OPS_COTOMaster.DateConfig = item.DateConfig;
                        obj.ContractTermID = item.ContractTermID > 0 ? item.ContractTermID : null;
                        obj.CATRoutingID = item.CATRoutingID > 0 ? item.CATRoutingID : null;
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
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCON_Reject(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.OPS_COTOMaster.PayVendorModified = Account.UserName;
                                obj.OPS_COTOMaster.PayVendorNote = Note;
                                obj.OPS_COTOMaster.TypeOfPaymentCOTOMasterID = -(int)SYSVarType.TypeOfPaymentCOTOMasterReject;
                            }
                        }
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

        public void FINFreightAuditCON_Accept(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.OPS_COTOMaster.PayVendorModified = Account.UserName;
                                obj.OPS_COTOMaster.PayVendorNote = Note;
                                obj.OPS_COTOMaster.TypeOfPaymentCOTOMasterID = -(int)SYSVarType.TypeOfPaymentCOTOMasterWait;
                            }
                        }
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

        public void FINFreightAuditCON_Waiting(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.OPS_COTOMaster.PayUserModified = Account.UserName;
                                obj.OPS_COTOMaster.PayUserNote = Note;
                                obj.OPS_COTOMaster.TypeOfPaymentCOTOMasterID = -(int)SYSVarType.TypeOfPaymentCOTOMasterOpen;
                            }
                        }
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

        public void FINFreightAuditCON_Approved(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.OPS_COTOMaster.PayUserModified = Account.UserName;
                                obj.OPS_COTOMaster.PayUserNote = Note;
                                obj.OPS_COTOMaster.TypeOfPaymentCOTOMasterID = -(int)SYSVarType.TypeOfPaymentCOTOMasterApproved;
                            }
                        }
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
        #endregion

        #region Freight Audit Cus

        #region Truck
        public List<SYSVar> FINFreightAuditCus_Order_StatusList()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TypeOfPaymentORDOrder).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        ValueOfVar = c.ValueOfVar
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

        public List<SYSVar> FINFreightAuditCus_Group_StatusList()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TypeOfPaymentORDGroupProduct).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        ValueOfVar = c.ValueOfVar
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

        public DTOResult FINFreightAuditCus_Order_List(DateTime pDateFrom, DateTime pDateTo, int statusID, string request)
        {
            try
            {
                List<int> lstNoGroupCredit = new List<int>();
                lstNoGroupCredit.Add((int)CATCostType.DITOFreightCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOFreightNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOMOQNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOMOQLoadNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOMOQUnLoadNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOUnLoadCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOLoadCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOReturnCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOUnLoadReturnCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOLoadReturnCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOExCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOExNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.ManualFixCredit);
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    pDateFrom = pDateFrom.Date;
                    pDateTo = pDateTo.AddDays(1).Date;

                    var query = model.ORD_Order.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfPaymentORDOrderID.HasValue && c.TypeOfPaymentORDOrderID == statusID && c.DateConfig >= pDateFrom && c.DateConfig < pDateTo && c.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderReceived && Account.ListCustomerID.Contains(c.CustomerID)).Select(c => new FINFreightAuditCus_Order
                    {
                        OrderID = c.ID,
                        OrderCode = c.Code,
                        DateConfig = c.DateConfig,
                        StatusID = c.TypeOfPaymentORDOrderID.Value,
                        StatusName = c.SYS_Var4.ValueOfVar,
                        Credit = 0,
                        IsChoose = false,
                        PayUserModified = c.PayUserModified,
                        PayUserNote = c.PayUserNote,
                        PayCustomerModified = c.PayCustomerModified,
                        PayCustomerNote = c.PayCustomerNote,
                        CustomerCode = c.CUS_Customer.Code,
                        CustomerName = c.CUS_Customer.CustomerName,
                    }).ToList();

                    foreach (FINFreightAuditCus_Order item in query)
                    {
                        var lstCredit = model.FIN_PLGroupOfProduct.Where(c => c.FIN_PLDetails.FIN_PL.OrderID == item.OrderID && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && lstNoGroupCredit.Contains(c.FIN_PLDetails.CostID)).Select(c => new FINFreightAudit_Credit
                        {
                            Cost = c.FIN_PLDetails.Credit
                        }).ToList();

                        var lstTrouble = model.FIN_PLDetails.Where(c => c.FIN_PL.OrderID == item.OrderID && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.CostID == (int)CATCostType.TroubleCredit).Select(c => new { c.Credit }).ToList();

                        item.Credit += lstCredit.Sum(c => c.Cost);
                        item.Credit += lstTrouble.Sum(c => c.Credit);
                    }

                    result.Total = query.ToDataSourceResult(CreateRequest(request)).Total;
                    result.Data = query.ToDataSourceResult(CreateRequest(request)).Data;
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

        public FINFreightAuditCus_Order_Detail FINFreightAuditCus_OrderDetail_List(int id)
        {
            try
            {
                List<int> lstNoGroupCredit = new List<int>();
                lstNoGroupCredit.Add((int)CATCostType.DITOFreightCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOFreightNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOMOQNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOMOQLoadNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOMOQUnLoadNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOUnLoadCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOLoadCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOReturnCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOUnLoadReturnCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOLoadReturnCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOExCredit);
                lstNoGroupCredit.Add((int)CATCostType.DITOExNoGroupCredit);
                lstNoGroupCredit.Add((int)CATCostType.ManualFixCredit);
                FINFreightAuditCus_Order_Detail result = new FINFreightAuditCus_Order_Detail();
                result.lstCredit = new List<FINFreightAudit_Credit>();
                result.lstTrouble = new List<FINFreightAudit_Other>();
                using (var model = new DataEntities())
                {
                    result.lstCredit = model.FIN_PLGroupOfProduct.Where(c => c.FIN_PLDetails.FIN_PL.OrderID == id && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && lstNoGroupCredit.Contains(c.FIN_PLDetails.CostID)).Select(c => new FINFreightAudit_Credit
                        {
                            ID = c.ID,
                            CostID = c.FIN_PLDetails.CostID,
                            CostName = c.FIN_PLDetails.CAT_Cost.CostName,
                            LocationFromID = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationID,
                            LocationFromName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.Address,
                            LocationToID = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationID,
                            LocationToName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                            OPSGroupID = c.GroupOfProductID,
                            GroupOfProductNameCUS = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                            Unit = !string.IsNullOrEmpty(c.Unit) ? c.Unit : c.FIN_PLDetails.Unit,
                            UnitPrice = c.FIN_PLDetails.UnitPrice.HasValue ? c.FIN_PLDetails.UnitPrice.Value : c.UnitPrice,
                            Quantity = c.FIN_PLDetails.Quantity.HasValue ? c.FIN_PLDetails.Quantity.Value : c.Quantity,
                            Cost = c.FIN_PLDetails.Credit,
                            Note = c.FIN_PLDetails.Note,
                            Note1 = c.FIN_PLDetails.Note1,
                            RoutingCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Routing.CAT_Routing.Code,
                            RoutingName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName,
                            DateFromCome = c.OPS_DITOGroupProduct.DateFromCome,
                            DateFromLoadStart = c.OPS_DITOGroupProduct.DateFromLoadStart,
                            DateFromLoadEnd = c.OPS_DITOGroupProduct.DateFromLoadEnd,
                            DateFromLeave = c.OPS_DITOGroupProduct.DateFromLeave,
                            DateToCome = c.OPS_DITOGroupProduct.DateToCome,
                            DateToLoadStart = c.OPS_DITOGroupProduct.DateToLoadStart,
                            DateToLoadEnd = c.OPS_DITOGroupProduct.DateToLoadEnd,
                            DateToLeave = c.OPS_DITOGroupProduct.DateToLeave,
                        }).ToList();

                    foreach (var item in result.lstCredit)
                    {
                        if (item.UnitPrice > 0 || item.Quantity > 0)
                            item.Cost = item.UnitPrice * (decimal)item.Quantity;
                    }

                    result.lstTrouble = model.FIN_PLDetails.Where(c => c.FIN_PL.OrderID == id && c.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.CostID == (int)CATCostType.TroubleCredit).Select(c => new FINFreightAudit_Other
                    {
                        CostID = c.CostID,
                        CostName = c.CAT_Cost.CostName,
                        Cost = c.Credit,
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

        public FINFreightAuditCus_Order_Detail FINFreightAuditCus_Order_DetailList(int id)
        {
            try
            {
                FINFreightAuditCus_Order_Detail result = new FINFreightAuditCus_Order_Detail();
                //result.lstDebit = new List<FINFreightAudit_Other>();
                using (var model = new DataEntities())
                {
                    //result.lstDebit = model.FIN_PLDetails.Where(c => c.FIN_PL.OrderID == id && !c.FIN_PL.IsPlanning && (c.CostID == (int)CATCostType.DITOFreightCredit || c.CostID == (int)CATCostType.DITOLoadCredit || c.CostID == (int)CATCostType.DITOUnLoadCredit || c.CostID == (int)CATCostType.MOQ || c.CostID == (int)CATCostType.TypeOfPriceExDebit)).Select(c => new FINFreightAudit_Other
                    //{
                    //    CostID = c.CostID,
                    //    CostName = !string.IsNullOrEmpty(c.Note) ? c.Note : c.CAT_Cost.CostName,
                    //    Cost = c.Debit
                    //}).ToList();
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

        public void FINFreightAuditCus_Order_Reject(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_Order.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfPaymentORDOrderID = -(int)SYSVarType.TypeOfPaymentORDOrderReject;
                            }
                        }
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

        public void FINFreightAuditCus_Order_Accept(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_Order.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfPaymentORDOrderID = -(int)SYSVarType.TypeOfPaymentORDOrderWait;
                            }
                        }
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

        public void FINFreightAuditCus_Order_Approved(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_Order.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfPaymentORDOrderID = -(int)SYSVarType.TypeOfPaymentORDOrderApproved;
                            }
                        }
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

        public DTOResult FINFreightAuditCus_Group_List(DateTime pDateFrom, DateTime pDateTo, int statusID, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    pDateFrom = pDateFrom.Date;
                    pDateTo = pDateTo.AddDays(1).Date;

                    var query = model.ORD_GroupProduct.Where(c => c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.TypeOfPaymentORDGroupProductID == statusID && c.DateConfig.HasValue && c.DateConfig >= pDateFrom && c.DateConfig < pDateTo && c.ORD_Order.ServiceOfOrderID == -(int)SYSVarType.ServiceOfOrderLocal && c.ORD_Order.TransportModeID == -(int)SYSVarType.TransportModeLTL && c.TypeOfPaymentORDGroupProductID.HasValue && Account.ListCustomerID.Contains(c.ORD_Order.CustomerID)).Select(c => new FINFreightAuditCus_Group
                    {
                        OrderID = c.OrderID,
                        OrderCode = c.ORD_Order.Code,
                        ORDGroupID = c.ID,
                        SOCode = c.SOCode,
                        StatusID = c.TypeOfPaymentORDGroupProductID.Value,
                        StatusName = c.SYS_Var1.ValueOfVar,
                        DateConfig = c.DateConfig.Value,
                        LocationFromID = c.CUS_Location.LocationID,
                        LocationFromName = c.CUS_Location.CAT_Location.Address,
                        LocationToID = c.CUS_Location1.LocationID,
                        LocationToName = c.CUS_Location1.CAT_Location.Address,
                        Debit = 0,
                        IsChoose = false,
                        PayUserModified = c.PayUserModified,
                        PayUserNote = c.PayUserNote,
                        PayCustomerModified = c.PayCustomerModified,
                        PayCustomerNote = c.PayCustomerNote
                    }).ToDataSourceResult(CreateRequest(request));

                    foreach (FINFreightAuditCus_Group item in query.Data)
                    {
                        //var lstDebit = model.FIN_PLGroupOfProduct.Where(c => c.OPS_DITOGroupProduct.OrderGroupProductID == item.ORDGroupID && !c.FIN_PLDetails.FIN_PL.IsPlanning && (c.FIN_PLDetails.CostID == (int)CATCostType.DITOFreightCredit || c.FIN_PLDetails.CostID == (int)CATCostType.DITOLoadCredit || c.FIN_PLDetails.CostID == (int)CATCostType.DITOUnLoadCredit || c.FIN_PLDetails.CostID == (int)CATCostType.MOQ || c.FIN_PLDetails.CostID == (int)CATCostType.TypeOfPriceExCredit)).Select(c => new
                        //{
                        //    Cost = c.FIN_PLDetails.Credit
                        //}).ToList();

                        //item.Debit += lstDebit.Sum(c => c.Cost);
                    }

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<FINFreightAuditCus_Group>;
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

        public FINFreightAuditCus_Group_Detail FINFreightAuditCus_Group_DetailList(int id)
        {
            try
            {
                FINFreightAuditCus_Group_Detail result = new FINFreightAuditCus_Group_Detail();
                result.lstDebit = new List<FINFreightAuditCus_Group_Debit>();
                using (var model = new DataEntities())
                {
                    //var lstDebit = model.FIN_PLGroupOfProduct.Where(c => c.OPS_DITOGroupProduct.OrderGroupProductID == id && !c.FIN_PLDetails.FIN_PL.IsPlanning && (c.FIN_PLDetails.CostID == (int)CATCostType.DITOFreightCredit || c.FIN_PLDetails.CostID == (int)CATCostType.DITOLoadCredit || c.FIN_PLDetails.CostID == (int)CATCostType.DITOUnLoadCredit || c.FIN_PLDetails.CostID == (int)CATCostType.MOQ)).Select(c => new FINFreightAuditCus_Group_Debit
                    //{
                    //    CostID = c.FIN_PLDetails.CostID,
                    //    CostName = c.FIN_PLDetails.CAT_Cost.CostName,
                    //    Quantity = c.Quantity,
                    //    Unit = c.Unit,
                    //    UnitPrice = c.UnitPrice,
                    //    Cost = c.FIN_PLDetails.Credit
                    //}).ToList();
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

        public void FINFreightAuditCus_Group_Reject(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfPaymentORDGroupProductID = -(int)SYSVarType.TypeOfPaymentORDGroupProductReject;
                            }
                        }
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

        public void FINFreightAuditCus_Group_Accept(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfPaymentORDGroupProductID = -(int)SYSVarType.TypeOfPaymentORDGroupProductWait;
                            }
                        }
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

        public void FINFreightAuditCus_Group_Approved(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfPaymentORDGroupProductID = -(int)SYSVarType.TypeOfPaymentORDGroupProductApproved;
                            }
                        }
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

        // Check Import
        public List<FINFreightAuditCus_Import> FINFreightAuditCus_ImportCheck(List<FINFreightAuditCus_Import> lstData, int customerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var lstOrderID = lstData.Where(c => c.ExcelSuccess).Select(c => c.OrderID).Distinct().ToList();
                    var lstCredit = model.FIN_PL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.CustomerID == customerID && c.Debit == 0 && c.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.OrderID > 0 && lstOrderID.Contains(c.OrderID.Value)).Select(c => new
                        {
                            OrderID = c.OrderID.Value,
                            c.Credit,
                        }).ToList();

                    foreach (var item in lstData.Where(c => c.ExcelSuccess))
                    {
                        item.SysCredit = 0;
                        var lstCreditCus = lstCredit.Where(c => c.OrderID == item.OrderID);
                        if (lstCreditCus.Count() > 0)
                            item.SysCredit = lstCreditCus.Sum(c => c.Credit);
                        item.Result = decimal.Round(item.Credit - item.SysCredit, 0);
                        if (item.Result != 0)
                            item.ExcelError = "Số tiền không khớp";
                    }

                    return lstData;
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

        public FINFreightAuditCus_ImportData FINFreightAuditCus_ImportData(int customerID)
        {
            try
            {
                FINFreightAuditCus_ImportData result = new FINFreightAuditCus_ImportData();
                result.ListOrder = new List<FINFreightAuditCus_ImportData_Order>();
                using (var model = new DataEntities())
                {
                    result.ListOrder = model.ORD_Order.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.CustomerID == customerID).Select(c => new FINFreightAuditCus_ImportData_Order
                        {
                            OrderID = c.ID,
                            OrderCode = c.Code
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

        public void FINFreightAuditCus_ImportReject(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_Order.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfPaymentORDOrderID = -(int)SYSVarType.TypeOfPaymentORDOrderReject;

                                foreach (var objDetail in model.ORD_GroupProduct.Where(c => c.OrderID == item))
                                {
                                    objDetail.PayCustomerModified = Account.UserName;
                                    objDetail.PayCustomerNote = Note;
                                    objDetail.TypeOfPaymentORDGroupProductID = -(int)SYSVarType.TypeOfPaymentORDGroupProductReject;
                                }
                            }
                        }
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

        public void FINFreightAuditCus_ImportAccept(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_Order.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfPaymentORDOrderID = -(int)SYSVarType.TypeOfPaymentORDOrderWait;

                                foreach (var objDetail in model.ORD_GroupProduct.Where(c => c.OrderID == item))
                                {
                                    objDetail.PayCustomerModified = Account.UserName;
                                    objDetail.PayCustomerNote = Note;
                                    objDetail.TypeOfPaymentORDGroupProductID = -(int)SYSVarType.TypeOfPaymentORDGroupProductWait;
                                }
                            }
                        }
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

        #region Container
        public List<SYSVar> FINFreightAuditCus_OrderCON_StatusList()
        {
            try
            {
                List<SYSVar> result = new List<SYSVar>();
                using (var model = new DataEntities())
                {
                    result = model.SYS_Var.Where(c => c.TypeOfVar == (int)SYSVarType.TypeOfWAPaymentORDContainer).Select(c => new SYSVar
                    {
                        ID = c.ID,
                        ValueOfVar = c.ValueOfVar
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
        public DTOResult FINFreightAuditCus_OrderCON_List(DateTime pDateFrom, DateTime pDateTo, int statusID, string request)
        {
            try
            {
                List<int> lstNoGroupCredit = new List<int>();
                lstNoGroupCredit.Add((int)CATCostType.COTOFreightCredit);
                lstNoGroupCredit.Add((int)CATCostType.ManualFixCredit);
                lstNoGroupCredit.Add((int)CATCostType.ORDContainerServiceCredit);
                lstNoGroupCredit.Add((int)CATCostType.ORDDocumentCredit);

                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    pDateFrom = pDateFrom.Date;
                    pDateTo = pDateTo.AddDays(1).Date;

                    var query = model.ORD_Container.Where(c => c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.TypeOfWAPaymentORDContainerID.HasValue && c.TypeOfWAPaymentORDContainerID == statusID && c.DateConfig >= pDateFrom && c.DateConfig < pDateTo && Account.ListCustomerID.Contains(c.ORD_Order.CustomerID)).Select(c => new FINFreightAuditCusCON_Order
                    {
                        ID = c.ID,
                        OrderID = c.OrderID,
                        OrderCode = c.ORD_Order.Code,
                        DateConfig = c.DateConfig.HasValue ? c.DateConfig.Value : c.ORD_Order.DateConfig,
                        StatusID = c.TypeOfWAPaymentORDContainerID.Value,
                        StatusName = c.SYS_Var4.ValueOfVar,
                        Credit = 0,
                        IsChoose = false,
                        PayUserModified = c.PayUserModified,
                        PayUserNote = c.PayUserNote,
                        PayCustomerModified = c.PayCustomerModified,
                        PayCustomerNote = c.PayCustomerNote,
                        CustomerCode = c.ORD_Order.CUS_Customer.Code,
                        CustomerName = c.ORD_Order.CUS_Customer.CustomerName,
                    }).ToList();

                    var lstOPSContainer = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.OPS_Container.ORD_Container.TypeOfWAPaymentORDContainerID.HasValue && c.OPS_Container.ORD_Container.TypeOfWAPaymentORDContainerID == statusID && c.OPS_Container.ORD_Container.DateConfig >= pDateFrom && c.OPS_Container.ORD_Container.DateConfig < pDateTo && Account.ListCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID) && c.OPS_Container.OPSContainerTypeID == -(int)SYSVarType.OPSContainerTypeComplete).Select(c => new
                        {
                            OrderContainerID = c.OPS_Container.ContainerID,
                            ID = c.ID,
                        }).ToList();

                    foreach (FINFreightAuditCusCON_Order item in query)
                    {
                        var lstOPSContainerID = lstOPSContainer.Where(c => c.OrderContainerID == item.ID).Select(c => c.ID).ToList();
                        var lstCredit = model.FIN_PLContainer.Where(c => c.FIN_PLDetails.FIN_PL.OrderID == item.OrderID && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && lstNoGroupCredit.Contains(c.FIN_PLDetails.CostID) && lstOPSContainerID.Contains(c.COTOContainerID)).Select(c => new FINFreightAudit_Credit
                        {
                            Cost = c.FIN_PLDetails.Credit
                        }).ToList();

                        var lstTrouble = model.FIN_PLContainer.Where(c => c.FIN_PLDetails.FIN_PL.OrderID == item.OrderID && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.FIN_PLDetails.CostID == (int)CATCostType.TroubleCredit && lstOPSContainerID.Contains(c.COTOContainerID)).Select(c => new { c.FIN_PLDetails.Credit }).ToList();

                        item.Credit += lstCredit.Sum(c => c.Cost);
                        item.Credit += lstTrouble.Sum(c => c.Credit);
                    }

                    result.Total = query.ToDataSourceResult(CreateRequest(request)).Total;
                    result.Data = query.ToDataSourceResult(CreateRequest(request)).Data;
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

        public FINFreightAuditCus_OrderCON_Detail FINFreightAuditCus_OrderCONDetail_List(int id)
        {
            try
            {
                List<int> lstNoGroupCredit = new List<int>();
                lstNoGroupCredit.Add((int)CATCostType.COTOFreightCredit);
                lstNoGroupCredit.Add((int)CATCostType.ManualFixCredit);
                lstNoGroupCredit.Add((int)CATCostType.ORDContainerServiceCredit);
                lstNoGroupCredit.Add((int)CATCostType.ORDDocumentCredit);

                FINFreightAuditCus_OrderCON_Detail result = new FINFreightAuditCus_OrderCON_Detail();
                result.lstCredit = new List<FINFreightAudit_Credit>();
                result.lstTrouble = new List<FINFreightAudit_Other>();
                using (var model = new DataEntities())
                {
                    result = model.OPS_COTOContainer.Where(c => c.OPS_Container.ContainerID == id).Select(c => new FINFreightAuditCus_OrderCON_Detail
                    {
                        ID = c.OPS_Container.ContainerID,
                        DateConfig = c.OPS_Container.ORD_Container.DateConfig.HasValue ? c.OPS_Container.ORD_Container.DateConfig.Value : c.OPS_Container.ORD_Container.ORD_Order.DateConfig,
                        OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                        ContractID = c.OPS_Container.ORD_Container.ORD_Order.ContractID,
                        ContractName = c.OPS_Container.ORD_Container.ORD_Order.ContractID > 0 ? c.OPS_Container.ORD_Container.ORD_Order.CAT_Contract.DisplayName : "",
                        ContractTermID = c.ContractTermID,
                        ContractTermName = c.ContractTermID > 0 ? c.CAT_ContractTerm.TermName : "",
                        CATRoutingID = c.CATRoutingID,
                        CATRoutingName = c.CATRoutingID > 0 ? c.CAT_Routing.RoutingName : "",
                        CUSRoutingID = c.OPS_Container.ORD_Container.CUSRoutingID,
                        CUSRoutingName = c.OPS_Container.ORD_Container.CUSRoutingID > 0 ? c.OPS_Container.ORD_Container.CUS_Routing.RoutingName : "",
                        CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                    }).FirstOrDefault();

                    result.lstCredit = model.FIN_PLContainer.Where(c => c.OPS_COTOContainer.OPS_Container.ContainerID == id && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && lstNoGroupCredit.Contains(c.FIN_PLDetails.CostID)).Select(c => new FINFreightAudit_Credit
                    {
                        ID = c.ID,
                        CostID = c.FIN_PLDetails.CostID,
                        CostName = c.FIN_PLDetails.CAT_Cost.CostName,
                        LocationFromID = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Location.LocationID,
                        LocationFromName = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Location.CAT_Location.Address,
                        LocationToID = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Location1.LocationID,
                        LocationToName = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Location1.CAT_Location.Address,
                        OPSGroupID = c.COTOContainerID,
                        Unit = !string.IsNullOrEmpty(c.Unit) ? c.Unit : c.FIN_PLDetails.Unit,
                        UnitPrice = c.FIN_PLDetails.UnitPrice.HasValue ? c.FIN_PLDetails.UnitPrice.Value : c.UnitPrice,
                        Quantity = c.FIN_PLDetails.Quantity.HasValue ? c.FIN_PLDetails.Quantity.Value : c.Quantity,
                        Cost = c.FIN_PLDetails.Credit,
                        Note = c.FIN_PLDetails.Note,
                        Note1 = c.FIN_PLDetails.Note1,
                        RoutingCode = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Routing.CAT_Routing.Code,
                        RoutingName = c.OPS_COTOContainer.OPS_Container.ORD_Container.CUS_Routing.CAT_Routing.RoutingName,
                    }).ToList();

                    foreach (var item in result.lstCredit)
                    {
                        if (item.UnitPrice > 0 || item.Quantity > 0)
                            item.Cost = item.UnitPrice * (decimal)item.Quantity;
                    }

                    result.lstTrouble = model.FIN_PLContainer.Where(c => c.OPS_COTOContainer.OPS_Container.ContainerID == id && c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.FIN_PLDetails.CostID == (int)CATCostType.TroubleCredit).Select(c => new FINFreightAudit_Other
                    {
                        CostID = c.FIN_PLDetails.CostID,
                        CostName = c.FIN_PLDetails.CAT_Cost.CostName,
                        Cost = c.FIN_PLDetails.Credit,
                        Note = c.FIN_PLDetails.Note
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

        public void FINFreightAuditCus_OrderCONDetail_Save(FINFreightAuditCus_OrderCON_Detail item)
        {
            try
            {
                DTOError result = new DTOError();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    var obj = model.ORD_Container.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu");
                    }
                    else
                    {
                        var objOrder = model.ORD_Order.FirstOrDefault(c => c.ID == obj.OrderID);
                        if (objOrder != null)
                        {
                            obj.DateConfig = item.DateConfig;
                            if (item.CUSRoutingID < 1)
                                throw FaultHelper.BusinessFault(null, null, "Vui lòng chọn cung đường");
                            obj.CUSRoutingID = item.CUSRoutingID;
                            objOrder.ContractTermID = item.ContractTermID > 0 ? item.ContractTermID : null;
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
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOContractTerm> FINFreightAudit_ContractTerm_List(int contractID)
        {
            try
            {
                List<DTOContractTerm> result = new List<DTOContractTerm>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_ContractTerm.Where(c => c.ContractID == contractID).Select(c => new DTOContractTerm
                    {
                        ID = c.ID,
                        ContractID = c.ContractID,
                        Code = c.Code,
                        TermName = c.TermName,
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

        public List<DTOCUSRouting> FINFreightAudit_Routing_List(int customerID, int contractID, int termID)
        {
            try
            {
                List<DTOCUSRouting> result = new List<DTOCUSRouting>();
                using (var model = new DataEntities())
                {
                    if (contractID > 0)
                    {
                        var ListContractRoute = model.CAT_ContractRouting.Where(c => c.ContractID == contractID).Select(c => new { RoutingID = c.RoutingID, TermID = c.ContractTermID }).ToList();

                        if (termID > 0)
                        {
                            ListContractRoute = ListContractRoute.Where(c => c.TermID == termID).ToList();
                        }

                        var ListRoute = ListContractRoute.Select(c => c.RoutingID).ToList();

                        result = model.CUS_Routing.Where(c => c.CustomerID == customerID && ListRoute.Contains(c.RoutingID)).Select(c => new DTOCUSRouting
                        {
                            ID = c.ID,
                            RoutingID = c.RoutingID,
                            Code = c.CAT_Routing.Code,
                            RoutingName = c.CAT_Routing.RoutingName
                        }).ToList();
                    }
                    else
                    {
                        result = model.CUS_Routing.Where(c => c.CustomerID == customerID && c.CAT_Routing.Code != string.Empty).Select(c => new DTOCUSRouting
                        {
                            ID = c.ID,
                            RoutingID = c.RoutingID,
                            Code = c.CAT_Routing.Code,
                            RoutingName = c.CAT_Routing.RoutingName
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


        public void FINFreightAuditCus_OrderCON_Reject(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_Container.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfWAPaymentORDContainerID = -(int)SYSVarType.TypeOfWAPaymentORDContainerReject;
                            }
                        }
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

        public void FINFreightAuditCus_OrderCON_Accept(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_Container.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfWAPaymentORDContainerID = -(int)SYSVarType.TypeOfWAPaymentORDContainerWait;
                            }
                        }
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

        public void FINFreightAuditCus_OrderCON_Approved(List<int> lst, string Note)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (lst != null && lst.Count > 0)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in lst)
                        {
                            var obj = model.ORD_Container.FirstOrDefault(c => c.ID == item);
                            if (obj != null)
                            {
                                obj.PayCustomerModified = Account.UserName;
                                obj.PayCustomerNote = Note;
                                obj.TypeOfWAPaymentORDContainerID = -(int)SYSVarType.TypeOfWAPaymentORDContainerApproved;
                            }
                        }
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
        #endregion

        #region FIN ManualFix
        public DTOResult FINManualFix_List(DateTime pDateFrom, DateTime pDateTo, string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    pDateFrom = pDateFrom.Date;
                    pDateTo = pDateTo.AddDays(1).Date;
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var query = model.FIN_ManualFix.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.RequestDate >= pDateFrom &&
                        c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.RequestDate < pDateTo && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID))).Select(c => new DTOFINManualFix
                    {
                        ID = c.ID,
                        DITOGroupProductID = c.DITOGroupProductID,
                        CBM = c.CBM,
                        Ton = c.Ton,
                        UnitPrice = c.UnitPrice,
                        Credit = c.Credit,
                        Debit = c.Debit,
                        Note = c.Note,
                        MasterCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.Code,
                        VehicleNo = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                        VendorCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.CUS_Customer.Code : string.Empty,
                        OrderCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code,
                        GroupProductCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                        SOCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.SOCode,
                        DNCode = c.OPS_DITOGroupProduct.DNCode,
                        LocationToID = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationID,
                        LocationToCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Code,
                        LocationToName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Location,
                        LocationFromID = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.LocationID,
                        LocationFromCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.Code,
                        LocationFromName = c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.CAT_Location.Location,
                        CustomerCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        ETA = c.OPS_DITOGroupProduct.OPS_DITOMaster.ETA,
                        ETD = c.OPS_DITOGroupProduct.OPS_DITOMaster.ETD,
                        ATA = c.OPS_DITOGroupProduct.OPS_DITOMaster.ATA,
                        ATD = c.OPS_DITOGroupProduct.OPS_DITOMaster.ATD,
                    }).ToDataSourceResult(CreateRequest(request));

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOFINManualFix>;
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
        public int FINManualFix_Save(DTOFINManualFix item)
        {
            try
            {

                using (var model = new DataEntities())
                {
                    var obj = model.FIN_ManualFix.FirstOrDefault(c => c.SYSCustomerID == Account.SYSCustomerID && c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new FIN_ManualFix();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        obj.DITOGroupProductID = item.DITOGroupProductID;
                        model.FIN_ManualFix.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                    }
                    obj.Ton = item.Ton;
                    obj.CBM = item.CBM;
                    obj.Quantity = item.Quantity;
                    obj.Credit = item.Credit;
                    obj.Debit = item.Debit;
                    obj.UnitPrice = item.UnitPrice;
                    obj.Note = item.Note;

                    model.SaveChanges();
                    return obj.ID;
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

        public void FINManualFix_Delete(DTOFINManualFix item)
        {
            try
            {
                DTOFINManualFix result = new DTOFINManualFix();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.FIN_ManualFix.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        model.FIN_ManualFix.Remove(obj);
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
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FINManualFix_ChooseList(string request, DateTime pDateFrom, DateTime pDateTo)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                    var query = model.OPS_DITOGroupProduct.Where(c => c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.ORD_GroupProduct.ORD_Order.RequestDate >= pDateFrom &&
                        c.ORD_GroupProduct.ORD_Order.RequestDate < pDateTo && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID))).Select(c => new DTOFINManualFix
                    {
                        ID = 0,
                        DITOGroupProductID = c.ID,
                        MasterCode = c.OPS_DITOMaster.Code,
                        VehicleNo = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                        VendorCode = c.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOMaster.CUS_Customer.Code : string.Empty,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                        GroupProductCode = c.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                        Ton = c.TonTranfer,
                        CBM = c.CBMTranfer,
                        Quantity = c.QuantityTranfer,
                        UnitPrice = 0,
                        Debit = 0,
                        Credit = 0,
                        Note = string.Empty,
                        SOCode = c.ORD_GroupProduct.SOCode,
                        DNCode = c.DNCode,
                        LocationToID = c.ORD_GroupProduct.CUS_Location1.LocationID,
                        LocationToCode = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Code,
                        LocationToName = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location,
                        LocationFromID = c.ORD_GroupProduct.CUS_Location.LocationID,
                        LocationFromCode = c.ORD_GroupProduct.CUS_Location.CAT_Location.Code,
                        LocationFromName = c.ORD_GroupProduct.CUS_Location.CAT_Location.Location,
                        CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        ETA = c.OPS_DITOMaster.ETA,
                        ETD = c.OPS_DITOMaster.ETD,
                        ATA = c.OPS_DITOMaster.ATA,
                        ATD = c.OPS_DITOMaster.ATD,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOFINManualFix>;
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
        public void FINManualFix_SaveList(List<DTOFINManualFix> lst)
        {
            try
            {

                using (var model = new DataEntities())
                {
                    foreach (var item in lst)
                    {
                        FIN_ManualFix obj = new FIN_ManualFix();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        obj.DITOGroupProductID = item.DITOGroupProductID;
                        obj.Ton = item.Ton;
                        obj.CBM = item.CBM;
                        obj.Quantity = item.Quantity;
                        obj.Credit = item.Credit;
                        obj.Debit = item.Debit;
                        obj.UnitPrice = item.UnitPrice;
                        obj.Note = item.Note;
                        model.FIN_ManualFix.Add(obj);
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

        // Xóa hết dữ liệu ORD của khách hàng
        public void DeleteORD(int cusID, int sysCustomerID)
        {
            try
            {
                using (var model = new DataEntities())
                {

                    //#region Xóa PL
                    //foreach (var item in model.FIN_PL.Where(c => c.CustomerID == cusID && c.SYSCustomerID == sysCustomerID))
                    //{
                    //    foreach (var detail in model.FIN_PLDetails.Where(c => c.PLID == item.ID))
                    //    {
                    //        foreach (var detail1 in model.FIN_PLContainer.Where(c => c.PLDetailID == detail.ID))
                    //        {
                    //            model.FIN_PLContainer.Remove(detail1);
                    //        }
                    //        foreach (var detail1 in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == detail.ID))
                    //        {
                    //            model.FIN_PLGroupOfProduct.Remove(detail1);
                    //        }
                    //        model.FIN_PLDetails.Remove(detail);
                    //    }

                    //    model.FIN_PL.Remove(item);
                    //}
                    //foreach (var item in model.FIN_PLCosting.Where(c => c.CustomerID == cusID && c.SYSCustomerID == sysCustomerID))
                    //{
                    //    model.FIN_PLCosting.Remove(item);
                    //}

                    //foreach (var item in model.FIN_Temp.Where(c => c.DITOGroupProductID > 0 && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID == cusID && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.SYSCustomerID == sysCustomerID))
                    //{
                    //    model.FIN_Temp.Remove(item);
                    //}
                    //foreach (var item in model.FIN_Temp.Where(c => c.COTOContainerID > 0 && c.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.CustomerID == cusID && c.OPS_COTOContainer.OPS_Container.ORD_Container.ORD_Order.SYSCustomerID == sysCustomerID))
                    //{
                    //    model.FIN_Temp.Remove(item);
                    //}
                    //#endregion

                    //model.SaveChanges();


                    //#region Xóa chuyến
                    //var lstDITOMasterID = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.CustomerID == cusID && c.DITOMasterID > 0 && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == sysCustomerID).Select(c => c.DITOMasterID.Value).Distinct().ToList();
                    //var totalAll = lstDITOMasterID.Count;
                    //lstDITOMasterID = lstDITOMasterID.Take(100).ToList();
                    //var total = lstDITOMasterID.Count;
                    //int count = 1;
                    //foreach (var masterID in lstDITOMasterID)
                    //{
                    //    System.Diagnostics.Debug.WriteLine(count + " of " + total + " totalAll: " + totalAll);

                    //    foreach (var item in model.OPS_DITOMaster.Where(c => c.ID == masterID))
                    //    {
                    //        foreach (var detail1 in model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            foreach (var detail2 in model.OPS_DITOProduct.Where(c => c.DITOGroupProductID == detail1.ID))
                    //            {
                    //                foreach (var detail3 in model.OPS_DITOProductPOD.Where(c => c.DITOProductID == detail2.ID))
                    //                {
                    //                    model.OPS_DITOProductPOD.Remove(detail3);
                    //                }
                    //                foreach (var detail3 in model.OPS_ExtReturnDetail.Where(c => c.DITOProductID == detail2.ID))
                    //                {
                    //                    model.OPS_ExtReturnDetail.Remove(detail3);
                    //                }
                    //                model.OPS_DITOProduct.Remove(detail2);
                    //            }
                    //            foreach (var detail2 in model.FIN_ManualFix.Where(c => c.DITOGroupProductID == detail1.ID))
                    //            {
                    //                model.FIN_ManualFix.Remove(detail2);
                    //            }
                    //            model.OPS_DITOGroupProduct.Remove(detail1);
                    //        }
                    //        foreach (var detail in model.FIN_PL.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            foreach (var detail1 in model.FIN_PLDetails.Where(c => c.PLID == detail.ID))
                    //            {
                    //                foreach (var detail2 in model.FIN_PLContainer.Where(c => c.PLDetailID == detail1.ID))
                    //                {
                    //                    model.FIN_PLContainer.Remove(detail2);
                    //                }
                    //                foreach (var detail2 in model.FIN_PLGroupOfProduct.Where(c => c.PLDetailID == detail1.ID))
                    //                {
                    //                    model.FIN_PLGroupOfProduct.Remove(detail2);
                    //                }
                    //                model.FIN_PLDetails.Remove(detail1);
                    //            }
                    //            model.FIN_PL.Remove(detail);
                    //        }
                    //        foreach (var detail in model.OPS_DITOLocation.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            model.OPS_DITOLocation.Remove(detail);
                    //        }
                    //        foreach (var detail in model.OPS_DITORate.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            model.OPS_DITORate.Remove(detail);
                    //        }
                    //        foreach (var detail in model.OPS_ExtReturn.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            model.OPS_ExtReturn.Remove(detail);
                    //        }
                    //        foreach (var detail in model.OPS_DITOStation.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            model.OPS_DITOStation.Remove(detail);
                    //        }
                    //        foreach (var detail in model.POD_DIMaster.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            foreach (var detail1 in model.POD_DIGroupProduct.Where(c => c.DIMasterID == detail.ID))
                    //            {
                    //                model.POD_DIGroupProduct.Remove(detail1);
                    //            }
                    //            model.POD_DIMaster.Remove(detail);
                    //        }
                    //        foreach (var detail in model.OPS_DITO.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            foreach (var detail1 in model.OPS_DITODetail.Where(c => c.DITOID == detail.ID))
                    //            {
                    //                model.OPS_DITODetail.Remove(detail1);
                    //            }
                    //            model.OPS_DITO.Remove(detail);
                    //        }
                    //        foreach (var detail in model.FLM_AssetTimeSheet.Where(c => c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster && c.ReferID == item.ID))
                    //        {
                    //            foreach (var detail1 in model.FLM_AssetTimeSheetDriver.Where(c => c.AssetTimeSheetID == detail.ID))
                    //            {
                    //                model.FLM_AssetTimeSheetDriver.Remove(detail1);
                    //            }
                    //            model.FLM_AssetTimeSheet.Remove(detail);
                    //        }
                    //        foreach (var detail in model.FLM_Receipt.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            foreach (var detail1 in model.FLM_FixedCost.Where(c => c.ReceiptID == detail.ID))
                    //            {
                    //                model.FLM_FixedCost.Remove(detail1);
                    //            }
                    //            foreach (var detail1 in model.FLM_ReceiptCost.Where(c => c.ReceiptID == detail.ID))
                    //            {
                    //                model.FLM_ReceiptCost.Remove(detail1);
                    //            }
                    //            foreach (var detail1 in model.FLM_ReceiptTOMaster.Where(c => c.ReceiptID == detail.ID))
                    //            {
                    //                model.FLM_ReceiptTOMaster.Remove(detail1);
                    //            }
                    //            model.FLM_Receipt.Remove(detail);
                    //        }
                    //        foreach (var detail in model.FLM_TOMasterCost.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            model.FLM_TOMasterCost.Remove(detail);
                    //        }
                    //        foreach (var detail in model.FLM_TOMasterCost.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            model.FLM_TOMasterCost.Remove(detail);
                    //        }
                    //        foreach (var detail in model.KPI_KPITime.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            model.KPI_KPITime.Remove(detail);
                    //        }
                    //        foreach (var detail in model.WFL_PacketDetail.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            model.WFL_PacketDetail.Remove(detail);
                    //        }
                    //        foreach (var detail in model.CAT_Trouble.Where(c => c.DITOMasterID == item.ID))
                    //        {
                    //            model.CAT_Trouble.Remove(detail);
                    //        }
                    //        model.OPS_DITOMaster.Remove(item);
                    //    }
                    //    count++;
                    //}
                    //#endregion

                    //model.SaveChanges();

                    #region Xóa đơn hàng

                    var lstOrderID = model.ORD_Order.Where(c => c.CustomerID == cusID && c.SYSCustomerID == sysCustomerID).Select(c => c.ID).Distinct().ToList();
                    var totalAll = lstOrderID.Count();
                    lstOrderID = lstOrderID.Take(800).ToList();
                    var total = lstOrderID.Count;
                    int count = 1;
                    foreach (var itemOrder in lstOrderID)
                    {
                        System.Diagnostics.Debug.WriteLine(count + " of " + total + " totalAll: " + totalAll);
                        var item = model.ORD_Order.FirstOrDefault(c => c.ID == itemOrder);
                        #region Xóa ORD_Container
                        foreach (var detail in model.ORD_Container.Where(c => c.OrderID == item.ID))
                        {
                            foreach (var detail1 in model.OPS_COImportORDContainer.Where(c => c.ORDContainerID == detail.ID))
                            {
                                model.OPS_COImportORDContainer.Remove(detail1);
                            }

                            #region Xóa OPS_Container
                            foreach (var detail1 in model.OPS_Container.Where(c => c.ContainerID == detail.ID))
                            {
                                foreach (var detail2 in model.OPS_COTOContainer.Where(c => c.OPSContainerID == detail1.ID))
                                {
                                    foreach (var detail3 in model.FIN_PLContainer.Where(c => c.COTOContainerID == detail2.ID))
                                    {
                                        model.FIN_PLContainer.Remove(detail3);
                                    }
                                    foreach (var detail3 in model.FIN_Temp.Where(c => c.COTOContainerID == detail2.ID))
                                    {
                                        model.FIN_Temp.Remove(detail3);
                                    }
                                    foreach (var detail3 in model.KPI_Collection.Where(c => c.COTOContainerID == detail2.ID))
                                    {
                                        foreach (var detail4 in model.KPI_CollectionDetail.Where(c => c.CollectionID == detail3.ID))
                                        {
                                            model.KPI_CollectionDetail.Remove(detail4);
                                        }
                                        model.KPI_Collection.Remove(detail3);
                                    }
                                    foreach (var detail3 in model.OPS_COImportContainer.Where(c => c.COTOContainerID == detail2.ID))
                                    {
                                        model.OPS_COImportContainer.Remove(detail3);
                                    }
                                    foreach (var detail3 in model.OPS_COTODetail.Where(c => c.COTOContainerID == detail2.ID))
                                    {
                                        model.OPS_COTODetail.Remove(detail3);
                                    }
                                    foreach (var detail3 in model.OPS_OPTOPSContainer.Where(c => c.COTOContainerID == detail2.ID))
                                    {
                                        foreach (var detail4 in model.OPS_OPTCOTOContainer.Where(c => c.OPTOPSContainerID == detail3.ID))
                                        {
                                            foreach (var detail5 in model.OPS_OPTCOTODetail.Where(c => c.OPTCOTOContainerID == detail4.ID))
                                            {
                                                model.OPS_OPTCOTODetail.Remove(detail5);
                                            }
                                            model.OPS_OPTCOTOContainer.Remove(detail4);
                                        }
                                        model.OPS_OPTOPSContainer.Remove(detail3);
                                    }
                                    model.OPS_COTOContainer.Remove(detail2);
                                }
                                model.OPS_Container.Remove(detail1);
                            }
                            #endregion

                            foreach (var detail1 in model.OPS_TOContainer.Where(c => c.ORDContainerID == detail.ID))
                            {
                                model.OPS_TOContainer.Remove(detail1);
                            }

                            foreach (var detail1 in model.ORD_ContainerService.Where(c => c.ContainerID == detail.ID))
                            {
                                model.ORD_ContainerService.Remove(detail1);
                            }
                            foreach (var detail1 in model.ORD_DocumentContainer.Where(c => c.ContainerID == detail.ID))
                            {
                                model.ORD_DocumentContainer.Remove(detail1);
                            }

                            foreach (var detail1 in model.POD_COMaster.Where(c => c.OrderContainerID == detail.ID))
                            {
                                model.POD_COMaster.Remove(detail1);
                            }

                            foreach (var detail1 in model.ORD_GroupProduct.Where(c => c.ContainerID == detail.ID))
                            {
                                model.ORD_GroupProduct.Remove(detail1);
                            }

                            model.ORD_Container.Remove(detail);
                        }
                        #endregion

                        #region Xóa ORD_GroupProduct
                        foreach (var detail in model.ORD_GroupProduct.Where(c => c.OrderID == item.ID))
                        {
                            foreach (var detail1 in model.FIN_PLDetails.Where(c => c.OrderGroupProductID == detail.ID))
                            {
                                foreach (var detail2 in model.FIN_PLContainer.Where(c => c.PLDetailID == detail1.ID))
                                {
                                    model.FIN_PLContainer.Remove(detail2);
                                }
                                model.FIN_PLDetails.Remove(detail1);
                            }
                            foreach (var detail1 in model.OPS_DIImportORDGroupProduct.Where(c => c.ORDGroupProductID == detail.ID))
                            {
                                model.OPS_DIImportORDGroupProduct.Remove(detail1);
                            }
                            foreach (var detail1 in model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID == detail.ID))
                            {
                                foreach (var detail2 in model.FIN_ManualFix.Where(c => c.DITOGroupProductID == detail1.ID))
                                {
                                    model.FIN_ManualFix.Remove(detail2);
                                }
                                foreach (var detail2 in model.FIN_Temp.Where(c => c.DITOGroupProductID == detail1.ID))
                                {
                                    model.FIN_Temp.Remove(detail2);
                                }
                                foreach (var detail2 in model.KPI_Collection.Where(c => c.COTOContainerID == detail1.ID))
                                {
                                    foreach (var detail3 in model.KPI_CollectionDetail.Where(c => c.CollectionID == detail2.ID))
                                    {
                                        model.KPI_CollectionDetail.Remove(detail3);
                                    }
                                    model.KPI_Collection.Remove(detail2);
                                }
                                foreach (var detail2 in model.KPI_KPITime.Where(c => c.DITOGroupProductID == detail1.ID))
                                {
                                    model.KPI_KPITime.Remove(detail2);
                                }
                                foreach (var detail2 in model.KPI_VENTime.Where(c => c.DITOGroupProductID == detail1.ID))
                                {
                                    model.KPI_VENTime.Remove(detail2);
                                }
                                foreach (var detail2 in model.OPS_DIImportGroupProduct.Where(c => c.DITOGroupProductID == detail1.ID))
                                {
                                    model.OPS_DIImportGroupProduct.Remove(detail2);
                                }
                                foreach (var detail2 in model.OPS_DIPacketGroupProduct.Where(c => c.DITOGroupProductID == detail1.ID))
                                {
                                    foreach (var detail3 in model.OPS_DIPacketGroupProductVEN.Where(c => c.DIPacketGroupProductID == detail2.ID))
                                    {
                                        foreach (var detail4 in model.OPS_DIPacketTOGroupProduct.Where(c => c.DIPacketGroupProductVENID == detail3.ID))
                                        {
                                            model.OPS_DIPacketTOGroupProduct.Remove(detail4);
                                        }
                                        model.OPS_DIPacketGroupProductVEN.Remove(detail3);
                                    }
                                    model.OPS_DIPacketGroupProduct.Remove(detail2);
                                }
                                foreach (var detail2 in model.OPS_DITODetail.Where(c => c.DITOGroupProductID == detail1.ID))
                                {
                                    model.OPS_DITODetail.Remove(detail2);
                                }
                                foreach (var detail2 in model.OPS_DITOProduct.Where(c => c.DITOGroupProductID == detail1.ID))
                                {
                                    foreach (var detail3 in model.OPS_DITOProductPOD.Where(c => c.DITOProductID == detail2.ID))
                                    {
                                        model.OPS_DITOProductPOD.Remove(detail3);
                                    }
                                    foreach (var detail3 in model.OPS_ExtReturnDetail.Where(c => c.DITOProductID == detail2.ID))
                                    {
                                        model.OPS_ExtReturnDetail.Remove(detail3);
                                    }
                                    model.OPS_DITOProduct.Remove(detail2);
                                }
                                foreach (var detail2 in model.FIN_PLGroupOfProduct.Where(c => c.GroupOfProductID == detail1.ID))
                                {
                                    model.FIN_PLGroupOfProduct.Remove(detail2);
                                }
                                model.OPS_DITOGroupProduct.Remove(detail1);
                            }

                            foreach (var detail1 in model.OPS_TOGroupProduct.Where(c => c.ORDGroupProductID == detail.ID))
                            {
                                model.OPS_TOGroupProduct.Remove(detail1);
                            }
                            foreach (var detail1 in model.ORD_Product.Where(c => c.GroupProductID == detail.ID))
                            {
                                model.ORD_Product.Remove(detail1);
                            }
                            foreach (var detail1 in model.WFL_PacketDetail.Where(c => c.ORDGroupProductID == detail.ID))
                            {
                                model.WFL_PacketDetail.Remove(detail1);
                            }
                            model.ORD_GroupProduct.Remove(detail);
                        }
                        #endregion


                        foreach (var detail in model.ORD_ContainerPrice.Where(c => c.OrderID == item.ID))
                        {
                            model.ORD_ContainerPrice.Remove(detail);
                        }
                        foreach (var detail in model.ORD_Service.Where(c => c.OrderID == item.ID))
                        {
                            model.ORD_Service.Remove(detail);
                        }
                        foreach (var detail in model.ORD_OrderStatus.Where(c => c.OrderID == item.ID))
                        {
                            model.ORD_OrderStatus.Remove(detail);
                        }
                        foreach (var detail in model.ORD_Document.Where(c => c.OrderID == item.ID))
                        {
                            foreach (var detail1 in model.ORD_DocumentContainer.Where(c => c.DocumentID == detail.ID))
                            {
                                model.ORD_DocumentContainer.Remove(detail1);
                            }
                            foreach (var detail1 in model.ORD_DocumentService.Where(c => c.DocumentID == detail.ID))
                            {
                                foreach (var detail2 in model.ORD_DocumentDetail.Where(c => c.DocumentServiceID == detail1.ID))
                                {
                                    model.ORD_DocumentDetail.Remove(detail2);
                                }
                                model.ORD_DocumentService.Remove(detail1);
                            }
                            model.ORD_Document.Remove(detail);
                        }
                        count++;
                        model.ORD_Order.Remove(item);
                    }
                    #endregion

                    model.SaveChanges();
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

        public void DeletePacket(int cusID, int sysCustomerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var item in model.WFL_PacketSetting)
                    {
                        foreach (var detail in model.WFL_PacketSettingAction.Where(c => c.PacketSettingID == item.ID))
                        {
                            foreach (var detail1 in model.WFL_PacketAction.Where(c => c.PacketSettingActionID == detail.ID))
                            {
                                model.WFL_PacketAction.Remove(detail1);
                            }
                            model.WFL_PacketSettingAction.Remove(detail);
                        }
                        foreach (var detail in model.WFL_PacketSettingTemplate.Where(c => c.PacketSettingID == item.ID))
                        {
                            model.WFL_PacketSettingTemplate.Remove(detail);
                        }
                        foreach (var detail in model.WFL_Packet.Where(c => c.PacketSettingID == item.ID))
                        {
                            foreach (var detail1 in model.WFL_PacketDetail.Where(c => c.PacketID == detail.ID))
                            {
                                model.WFL_PacketDetail.Remove(detail1);
                            }
                            foreach (var detail1 in model.WFL_PacketDriver.Where(c => c.PacketID == detail.ID))
                            {
                                model.WFL_PacketDriver.Remove(detail1);
                            }
                            model.WFL_Packet.Remove(detail);
                        }

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
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void UpdateRouting(int cusID, int sysCustomerID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var item in model.ORD_GroupProduct.Where(c => c.ORD_Order.CustomerID == cusID && c.ORD_Order.SYSCustomerID == sysCustomerID && c.CUSRoutingID == 22264))
                    {
                        var ops = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.OrderGroupProductID == item.ID && c.CATRoutingID > 0);
                        if (ops != null)
                        {
                            var cusRouting = model.CUS_Routing.FirstOrDefault(c => c.CustomerID == cusID && c.RoutingID == ops.CATRoutingID);
                            if (cusRouting != null)
                                item.CUSRoutingID = cusRouting.ID;
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
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region FINSettingManual
        public DTOResult FINSettingManual_List(string request)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    DTOResult result = new DTOResult();
                    var query = model.CUS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.Key == CUSSettingKey.FINManual.ToString()).Select(c => new DTOFINSettingManual
                    {
                        SettingID = c.ID,
                        Name = c.Name,
                        SettingCustomerCode = c.CUS_Customer.Code,
                        SettingCustomerName = c.CUS_Customer.CustomerName,
                        CustomerID = c.CustomerID,
                        CreateBy = c.CreatedBy,
                        CreateDate = c.CreatedDate
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOFINSettingManual>;
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
        public DTOFINSettingManual FINSettingManual_Get(int id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    DTOFINSettingManual result = new DTOFINSettingManual();
                    if (id > 0)
                    {
                        var obj = model.CUS_Setting.Where(c => c.ID == id).FirstOrDefault();
                        if (obj != null)
                        {
                            result = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOFINSettingManual>(obj.Setting);

                            result.Name = obj.Name;
                        }
                    }
                    else
                    {
                        result.RowStart = 2;
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
        public void FINSettingManual_Save(DTOFINSettingManual item, int id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (string.IsNullOrEmpty(item.Name))
                        throw FaultHelper.BusinessFault(null, null, "Tên không được trống");
                    string sKey = CUSSettingKey.FINManual.ToString();
                    var obj = model.CUS_Setting.FirstOrDefault(c => c.Key == sKey && c.ID == id && c.SYSCustomerID == Account.SYSCustomerID);
                    string sSetting = Newtonsoft.Json.JsonConvert.SerializeObject(item);

                    if (obj == null)
                    {
                        obj = new CUS_Setting();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.Key = sKey;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        model.CUS_Setting.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.CustomerID = item.CustomerID == 0 ? Account.SYSCustomerID : item.CustomerID;
                    obj.Setting = sSetting;
                    obj.Name = item.Name;
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
        public void FINSettingManual_Delete(DTOFINSettingManual item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.CUS_Setting.Where(c => c.ID == item.SettingID).FirstOrDefault();
                    if (obj != null)
                    {
                        model.CUS_Setting.Remove(obj);
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

        #region Excel
        public List<DTOFINManualFixImport> FINManualFix_ExcelData(DateTime dtFrom, DateTime dtTO, int cusId)
        {
            try
            {
                List<DTOFINManualFixImport> result = new List<DTOFINManualFixImport>();

                dtFrom = dtFrom.Date;
                dtTO = dtTO.Date.AddDays(1);
                using (var model = new DataEntities())
                {
                    result = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.CustomerID == cusId && c.OPS_DITOMaster.ETD >= dtFrom && c.OPS_DITOMaster.ETD < dtTO
                        && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered).Select(c => new DTOFINManualFixImport
                        {
                            ID = c.ID,
                            DNCode = c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            ETARequest = c.ORD_GroupProduct.ETARequest,
                            MasterETDDate = c.OPS_DITOMaster.ETD,
                            MasterETDDatetime = c.OPS_DITOMaster.ETD,
                            OrderGroupETDDate = c.ORD_GroupProduct.ETD,
                            OrderGroupETDDatetime = c.ORD_GroupProduct.ETD,
                            CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                            CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                            CustomerName = c.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                            CreatedDate = c.ORD_GroupProduct.ORD_Order.CreatedDate,
                            MasterCode = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.Code : string.Empty,
                            DriverName = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverName1 : string.Empty,
                            DriverTel = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverTel1 : string.Empty,
                            DriverCard = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverCard1 : string.Empty,
                            RegNo = c.DITOMasterID.HasValue && c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                            RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                            LocationFromCode = c.ORD_GroupProduct.LocationFromID.HasValue ? c.ORD_GroupProduct.CUS_Location.Code : string.Empty,
                            LocationToCode = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.Code : string.Empty,
                            LocationToName = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.LocationName : string.Empty,
                            LocationToAddress = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : string.Empty,
                            LocationToProvince = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName : string.Empty,
                            LocationToDistrict = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName : string.Empty,
                            DistributorCode = c.ORD_GroupProduct.PartnerID > 0 ? c.ORD_GroupProduct.CUS_Partner.PartnerCode : string.Empty,
                            DistributorName = c.ORD_GroupProduct.PartnerID > 0 ? c.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName : string.Empty,
                            DistributorCodeName = c.ORD_GroupProduct.PartnerID > 0 ? c.ORD_GroupProduct.CUS_Partner.PartnerCode + "-" + c.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName : string.Empty,
                            Packing = c.ORD_GroupProduct.ORD_Product.FirstOrDefault().CUS_Product.Code,
                            IsInvoice = c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete ? true : false,
                            DateFromCome = c.DateFromCome,
                            DateFromLeave = c.DateFromLeave,
                            DateFromLoadEnd = c.DateFromLoadEnd,
                            DateFromLoadStart = c.DateFromLoadStart,
                            DateToCome = c.DateToCome,
                            DateToLeave = c.DateToLeave,
                            DateToLoadEnd = c.DateToLoadEnd,
                            DateToLoadStart = c.DateToLoadStart,
                            EconomicZone = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.EconomicZone : string.Empty,
                            //DITOGroupProductStatusPODName = c.SYS_Var1.ValueOfVar,
                            IsOrigin = c.IsOrigin,
                            InvoiceBy = c.InvoiceBy,
                            InvoiceDate = c.InvoiceDate,
                            InvoiceNote = c.InvoiceNote,
                            Note = c.Note,
                            OPSGroupNote1 = c.Note1,
                            OPSGroupNote2 = c.Note2,
                            ORDGroupNote1 = c.ORD_GroupProduct.Note1,
                            ORDGroupNote2 = c.ORD_GroupProduct.Note2,
                            TOMasterNote1 = c.OPS_DITOMaster.Note1,
                            TOMasterNote2 = c.OPS_DITOMaster.Note2,
                            VendorName = c.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOMaster.CUS_Customer.CustomerName : string.Empty,
                            VendorCode = c.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOMaster.CUS_Customer.Code : string.Empty,
                            Description = c.ORD_GroupProduct.Description,
                            GroupOfProductCode = c.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                            GroupOfProductName = c.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                            ChipNo = c.OPS_DITOMaster.Note1,
                            Temperature = c.OPS_DITOMaster.Note2,
                            Ton = c.Ton,
                            TonBBGN = c.TonBBGN,
                            TonTranfer = c.TonTranfer,
                            Kg = c.Ton * 1000,
                            KgTranfer = c.TonTranfer * 1000,
                            KgBBGN = c.TonBBGN * 1000,
                            KgReturn = c.TonReturn * 1000,
                            CBM = c.CBM,
                            CBMBBGN = c.CBMBBGN,
                            CBMTranfer = c.CBMTranfer,
                            Quantity = c.Quantity,
                            QuantityBBGN = c.QuantityBBGN,
                            QuantityTranfer = c.QuantityTranfer,
                            VENLoadCodeID = c.VendorLoadID > 0 ? c.VendorLoadID.Value : 0,
                            VENLoadCode = c.VendorLoadID > 0 ? c.CUS_Customer.Code : string.Empty,
                            VENUnLoadCodeID = c.VendorUnLoadID > 0 ? c.VendorUnLoadID.Value : 0,
                            VENUnLoadCode = c.VendorUnLoadID > 0 ? c.CUS_Customer1.Code : string.Empty,
                            TonReturn = c.ORD_GroupProduct.ORD_Product.FirstOrDefault().CUS_Product.IsKg ? c.TonReturn * 1000 : c.TonReturn,
                            CBMReturn = c.CBMReturn,
                            QuantityReturn = c.QuantityReturn,
                            InvoiceReturnNote = c.InvoiceReturnNote,
                            InvoiceReturnDate = c.InvoiceReturnDate,
                            ReasonCancelNote = c.ReasonCancelNote,
                            DateDN = c.DateDN,
                            ProductID = c.ORD_GroupProduct.ORD_Product.FirstOrDefault().ProductID,
                            RoutingCode = c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.Code : string.Empty,
                        }).ToList();

                    foreach (var item in result)
                    {
                        var lst = model.FIN_ManualFix.Where(c => c.DITOGroupProductID == item.ID).Select(c => new DTOFINManualFixImportDetail
                        {
                            ID = c.ID,
                            Ton = c.Ton,
                            CBM = c.CBM,
                            Quantity = c.Quantity,
                            UnitPrice = c.UnitPrice,
                            Credit = c.Credit,
                            Debit = c.Debit,
                            Note = c.Note,
                        }).ToList();

                        item.ListManualFix = new List<DTOFINManualFixImportDetail>();
                        item.ListManualFix.AddRange(lst);
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

        public void FINManualFix_ExcelImport(int sID, List<DTOFINManualFixImport> data)
        {
            try
            {
                DTOFINSettingManual objSetting = FINSettingManual_Get(sID);
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var item in data.Where(c => c.ExcelSuccess))
                    {
                        var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                        {
                            if (item.Credit > 0 || item.Debit > 0)
                            {
                                FIN_ManualFix objManualFix = new FIN_ManualFix();
                                objManualFix.CreatedBy = Account.UserName;
                                objManualFix.CreatedDate = DateTime.Now;
                                objManualFix.SYSCustomerID = Account.SYSCustomerID;
                                objManualFix.DITOGroupProductID = obj.ID;

                                objManualFix.Ton = item.TonManual;
                                objManualFix.CBM = item.CBMManual;
                                objManualFix.Quantity = item.QuantityManual;
                                objManualFix.Credit = item.Credit;
                                objManualFix.Debit = item.Debit;
                                objManualFix.UnitPrice = item.UnitPrice;
                                objManualFix.Note = item.ManualNote;

                                model.FIN_ManualFix.Add(objManualFix);
                            }
                        }
                        else
                        {
                            throw FaultHelper.BusinessFault(null, null, "Số thứ tự không hợp lệ.");
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
        #endregion
        #endregion
    }
}

