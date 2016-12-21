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
    public class BLKPI : Base, IBase
    {
        #region Common

        public DTOResult Customer_List()
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

        #region KPITime
        public DTOResult KPITime_List(string request, int kpiID, int cusID, DateTime from, DateTime to)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.KPI_KPITime.Where(c => c.KPIID == kpiID && c.OrderID > 0 && c.ORD_Order.CustomerID == cusID && c.DateData >= from && c.DateData <= to).Select(c => new KPIKPITime
                    {
                        ID = c.ID,
                        KPIID = c.KPIID,
                        KPICode = c.KPI_KPI.Code,
                        CustomerID = c.CustomerID,
                        OrderID = c.OrderID,
                        SOCode = c.OrderGroupProductID > 0 ? c.ORD_GroupProduct.SOCode : string.Empty,
                        DNCode = c.DITOGroupProductID > 0 ? c.OPS_DITOGroupProduct.DNCode : string.Empty,
                        DITOMasterID = c.DITOMasterID,
                        COTOMasterID = c.COTOMasterID,
                        OrderGroupProductID = c.OrderGroupProductID,
                        OrderContainerID = c.OrderContainerID,
                        RoutingName = c.OrderGroupProductID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                        CustomerCode = c.CUS_Customer.Code,
                        CustomerName = c.CUS_Customer.CustomerName,
                        OrderCode = c.OrderID.HasValue ? c.ORD_Order.Code : string.Empty,
                        COTOMasterCode = c.COTOMasterID > 0 ? c.OPS_COTOMaster.Code : string.Empty,
                        DITOMasterCode = c.DITOMasterID > 0 ? c.OPS_DITOMaster.Code : string.Empty,

                        DateData = c.DateData,
                        DateFromCome = c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        DateDN = c.DateDN,
                        DateInvoice = c.DateInvoice,
                        ETARequest = c.ETARequest,
                        DateTOMasterETD = c.DateTOMasterETD,
                        DateTOMasterETA = c.DateTOMasterETA,
                        DateTOMasterATD = c.DateTOMasterATD,
                        DateTOMasterATA = c.DateTOMasterATA,
                        DateOrderETD = c.DateOrderETD,
                        DateOrderETA = c.DateOrderETA,
                        DateOrderETDRequest = c.DateOrderETDRequest,
                        DateOrderETARequest = c.DateOrderETARequest,
                        DateOrderCutOfTime = c.DateOrderCutOfTime,
                        KPIDate = c.KPIDate,
                        IsKPI = c.IsKPI,
                        ReasonID = c.ReasonID,
                        ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "",
                        ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "",
                        Note = c.Note,
                        DITOGroupProductStatusPODName = c.OPS_DITOGroupProduct.SYS_Var1.ValueOfVar,
                        ContractDisplayName = c.ORD_Order.ContractID > 0 ? c.ORD_Order.CAT_Contract.DisplayName : string.Empty,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<KPIKPITime>;
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

        public void KPITime_Save(KPIKPITime item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.KPI_KPITime.FirstOrDefault(c => c.ID == item.ID && item.IsKPI.HasValue);
                    if (obj != null && obj.ReasonID != item.ReasonID)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.ReasonID = item.ReasonID;
                        obj.Note = item.Note;
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

        public void KPITime_Generate(DateTime from, DateTime to)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    DateTime? dtNull = null;
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in model.KPI_KPITime.Where(c => c.ReasonID == null && c.OrderID > 0 && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_Order.RequestDate >= from && c.ORD_Order.RequestDate < to))
                        model.KPI_KPITime.Remove(item);
                    model.SaveChanges();

                    var lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.DITOMasterID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 &&
                        c.ORD_GroupProduct.ORD_Order.ContractID > 0 && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_GroupProduct.ORD_Order.RequestDate >= from && c.ORD_GroupProduct.ORD_Order.RequestDate < to).Select(c => new
                        {
                            c.ID,
                            c.ORD_GroupProduct.ORD_Order.Code,
                            c.DNCode,
                            c.ORD_GroupProduct.ORD_Order.CustomerID,
                            c.ORD_GroupProduct.OrderID,
                            c.DITOMasterID,
                            c.OrderGroupProductID,
                            ContractID = c.ORD_GroupProduct.ORD_Order.ContractID.Value,
                            CATRoutingID = c.ORD_GroupProduct.CUS_Routing.RoutingID,
                            c.ORD_GroupProduct.ETARequest,
                            c.DateDN,
                            c.ORD_GroupProduct.ORD_Order.RequestDate,
                            c.DateFromCome,
                            c.DateFromLeave,
                            c.DateFromLoadStart,
                            c.DateFromLoadEnd,
                            c.DateToCome,
                            c.DateToLeave,
                            c.DateToLoadStart,
                            c.DateToLoadEnd,
                            c.InvoiceDate,

                            DateTOMasterETD = c.OPS_DITOMaster.ETD,
                            DateTOMasterETA = c.OPS_DITOMaster.ETA,
                            DateTOMasterATD = c.OPS_DITOMaster.ATD,
                            DateTOMasterATA = c.OPS_DITOMaster.ATA,

                            DateOrderETD = c.ORD_GroupProduct.ORD_Order.ETD,
                            DateOrderETA = c.ORD_GroupProduct.ORD_Order.ETA,
                            DateOrderETDRequest = c.ORD_GroupProduct.ETDRequest,
                            DateOrderETARequest = c.ORD_GroupProduct.ETARequest,
                            DateOrderCutOfTime = dtNull,
                        }).ToList();

                    var lstCustomerID = lstDITOGroupProduct.Select(c => c.CustomerID).Distinct().ToList();
                    var lstKPI = model.CAT_ContractKPITime.Where(c => c.CAT_ContractRouting.CAT_Contract.SYSCustomerID == Account.SYSCustomerID && c.CAT_ContractRouting.CAT_Contract.CustomerID > 0 &&
                        lstCustomerID.Contains(c.CAT_ContractRouting.CAT_Contract.CustomerID.Value)).Select(c => new DTOContractKPITime
                        {
                            CustomerID = c.CAT_ContractRouting.CAT_Contract.CustomerID,
                            ContractID = c.CAT_ContractRouting.ContractID,
                            CATRoutingID = c.CAT_ContractRouting.RoutingID,
                            Expression = c.Expression,
                            CompareField = c.CompareField,
                            KPIID = c.KPIID,
                            KPICode = c.KPI_KPI.Code
                        }).ToList();
                    var lstRouting = model.CAT_ContractRouting.Where(c => c.CAT_Contract.SYSCustomerID == Account.SYSCustomerID && c.CAT_Contract.CustomerID > 0 &&
                        lstCustomerID.Contains(c.CAT_Contract.CustomerID.Value)).Select(c => new
                        {
                            c.ContractID,
                            CATRoutingID = c.RoutingID,
                            c.Zone,
                            c.LeadTime
                        }).ToList();

                    //Danh sach cong thuc
                    Dictionary<string, OfficeOpenXml.ExcelPackage> dicKPICode = new Dictionary<string, OfficeOpenXml.ExcelPackage>();
                    foreach (var item in lstKPI.Distinct())
                    {
                        if (!string.IsNullOrEmpty(item.Expression) && !string.IsNullOrEmpty(item.CompareField))
                        {
                            var lst = lstKPI.Where(c => c.ContractID == item.ContractID && c.CATRoutingID == item.CATRoutingID).ToList();
                            dicKPICode.Add(item.ContractID + "_" + item.CATRoutingID + "_" + item.KPICode, HelperKPI.KPITime_GetPackage(item.Expression, item.CompareField, item.KPICode, lst, item.Zone, item.LeadTime));
                        }
                    }

                    //Tinh KPI
                    foreach (var itemGroupProduct in lstDITOGroupProduct)
                    {
                        var itemRouting = lstRouting.FirstOrDefault(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.CATRoutingID);
                        var lstKPIGroup = lstKPI.Where(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.CATRoutingID).ToList();

                        if (itemRouting != null && lstKPIGroup.Count > 0)
                        {
                            foreach (var itemKPIGroup in lstKPIGroup)
                            {
                                if (!string.IsNullOrEmpty(itemKPIGroup.Expression) && !string.IsNullOrEmpty(itemKPIGroup.CompareField))
                                {
                                    OfficeOpenXml.ExcelPackage kpiCode = default(OfficeOpenXml.ExcelPackage);
                                    if (dicKPICode.ContainsKey(itemGroupProduct.ContractID + "_" + itemGroupProduct.CATRoutingID + "_" + itemKPIGroup.KPICode))
                                        kpiCode = dicKPICode[itemGroupProduct.ContractID + "_" + itemGroupProduct.CATRoutingID + "_" + itemKPIGroup.KPICode];

                                    if (kpiCode != null)
                                    {
                                        var obj = model.KPI_KPITime.FirstOrDefault(c => c.CustomerID == itemGroupProduct.CustomerID && c.OrderID == itemGroupProduct.OrderID &&
                                            c.DITOGroupProductID == itemGroupProduct.ID);
                                        if (obj == null)
                                        {
                                            obj = new KPI_KPITime();
                                            obj.KPIID = itemKPIGroup.KPIID;
                                            obj.CustomerID = itemGroupProduct.CustomerID;
                                            obj.OrderID = itemGroupProduct.OrderID;
                                            obj.OrderGroupProductID = itemGroupProduct.OrderGroupProductID;
                                            obj.DITOGroupProductID = itemGroupProduct.ID;
                                            obj.DITOMasterID = itemGroupProduct.DITOMasterID;

                                            obj.CreatedBy = Account.UserName;
                                            obj.CreatedDate = DateTime.Now;
                                        }
                                        else
                                        {
                                            obj.ModifiedBy = Account.UserName;
                                            obj.ModifiedDate = DateTime.Now;
                                        }
                                        obj.DateData = itemGroupProduct.RequestDate;
                                        obj.DateRequest = itemGroupProduct.RequestDate;
                                        obj.DateDN = itemGroupProduct.DateDN;
                                        if (obj.DateDN == null)
                                            obj.DateDN = itemGroupProduct.RequestDate;
                                        obj.DateFromCome = itemGroupProduct.DateFromCome;
                                        obj.DateFromLeave = itemGroupProduct.DateFromLeave;
                                        obj.DateFromLoadStart = itemGroupProduct.DateFromLoadStart;
                                        obj.DateFromLoadEnd = itemGroupProduct.DateFromLoadEnd;
                                        obj.DateToCome = itemGroupProduct.DateToCome;
                                        obj.DateToLeave = itemGroupProduct.DateToLeave;
                                        obj.DateToLoadStart = itemGroupProduct.DateToLoadStart;
                                        obj.DateToLoadEnd = itemGroupProduct.DateToLoadEnd;
                                        obj.DateInvoice = itemGroupProduct.InvoiceDate;
                                        obj.ETARequest = itemGroupProduct.ETARequest;

                                        obj.DateTOMasterETD = itemGroupProduct.DateTOMasterETD;
                                        obj.DateTOMasterETA = itemGroupProduct.DateTOMasterETA;
                                        obj.DateTOMasterATD = itemGroupProduct.DateTOMasterATD;
                                        obj.DateTOMasterATA = itemGroupProduct.DateTOMasterATA;
                                        obj.DateOrderETD = itemGroupProduct.DateOrderETD;
                                        obj.DateOrderETA = itemGroupProduct.DateOrderETA;
                                        obj.DateOrderETDRequest = itemGroupProduct.DateOrderETDRequest;
                                        obj.DateOrderETARequest = itemGroupProduct.DateOrderETARequest;
                                        obj.DateOrderCutOfTime = itemGroupProduct.DateOrderCutOfTime;


                                        obj.Zone = itemRouting.Zone;
                                        if (obj.Zone == null) obj.Zone = 0;
                                        obj.LeadTime = itemRouting.LeadTime;
                                        if (obj.LeadTime == null) obj.LeadTime = 0;
                                        obj.Note = string.Empty;

                                        try
                                        {
                                            HelperKPI.KPITime_GetDate(kpiCode, itemKPIGroup.KPICode, obj, lstKPIGroup);
                                            if (obj.IsKPI == true && obj.ReasonID > 0)
                                            {
                                                obj.ReasonID = null;
                                                obj.Note = string.Empty;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            obj.Note = ex.Message;
                                        }

                                        if (obj.ID < 1)
                                            model.KPI_KPITime.Add(obj);
                                    }
                                }
                            }
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

        public List<KPIReason> KPITimeReason_List(int kpiID)
        {
            try
            {
                List<KPIReason> result = new List<KPIReason>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_Reason.Where(c => c.KPIID == kpiID).Select(c => new KPIReason
                    {
                        ID = c.ID,
                        Code = c.Code,
                        KPIName = c.KPI_KPI.KPIName,
                        KPICode = c.KPI_KPI.Code,
                        ReasonName = c.ReasonName,
                        KPIID = c.KPIID
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

        public List<KPIKPITime> KPITime_Excel(int kpiID, int cusID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIKPITime> result = new List<KPIKPITime>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_KPITime.Where(c => c.KPIID == kpiID && c.OrderID > 0 && c.ORD_Order.CustomerID == cusID && c.DateData >= from && c.DateData <= to).Select(c => new KPIKPITime
                    {
                        ID = c.ID,
                        KPIID = c.KPIID,
                        KPICode = c.KPI_KPI.Code,
                        CustomerID = c.CustomerID,
                        OrderID = c.OrderID,
                        SOCode = c.OrderGroupProductID > 0 ? c.ORD_GroupProduct.SOCode : string.Empty,
                        DNCode = c.DITOGroupProductID > 0 ? c.OPS_DITOGroupProduct.DNCode : string.Empty,
                        DITOMasterID = c.DITOMasterID,
                        COTOMasterID = c.COTOMasterID,
                        OrderGroupProductID = c.OrderGroupProductID,
                        OrderContainerID = c.OrderContainerID,
                        RoutingName = c.OrderGroupProductID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                        CustomerCode = c.CUS_Customer.Code,
                        CustomerName = c.CUS_Customer.CustomerName,
                        OrderCode = c.OrderID.HasValue ? c.ORD_Order.Code : string.Empty,
                        COTOMasterCode = c.COTOMasterID > 0 ? c.OPS_COTOMaster.Code : string.Empty,
                        DITOMasterCode = c.DITOMasterID > 0 ? c.OPS_DITOMaster.Code : string.Empty,

                        DateData = c.DateData,
                        DateFromCome = c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        DateDN = c.DateDN,
                        DateInvoice = c.DateInvoice,
                        ETARequest = c.ETARequest,
                        DateTOMasterETD = c.DateTOMasterETD,
                        DateTOMasterETA = c.DateTOMasterETA,
                        DateTOMasterATD = c.DateTOMasterATD,
                        DateTOMasterATA = c.DateTOMasterATA,
                        DateOrderETD = c.DateOrderETD,
                        DateOrderETA = c.DateOrderETA,
                        DateOrderETDRequest = c.DateOrderETDRequest,
                        DateOrderETARequest = c.DateOrderETARequest,
                        DateOrderCutOfTime = c.DateOrderCutOfTime,
                        KPIDate = c.KPIDate,
                        IsKPI = c.IsKPI,
                        ReasonID = c.ReasonID,
                        ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "",
                        ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "",
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

        #endregion

        #region KPITimeDate
        public DTOResult KPITimeDate_List(string request, int kpiID, int cusID, DateTime from, DateTime to)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.KPI_TimeDate.Where(c => c.TypeOfKPIID == kpiID && c.OrderID > 0 && c.ORD_Order.CustomerID == cusID && c.DateData >= from && c.DateData <= to).Select(c => new KPITimeDate
                    {
                        ID = c.ID,
                        KPIID = c.TypeOfKPIID,
                        KPICode = c.KPI_TypeOfKPI.Code,
                        KPIName = c.KPI_TypeOfKPI.TypeName,
                        CustomerID = c.ORD_Order.CustomerID,
                        OrderID = c.OrderID,
                        SOCode = c.OrderGroupProductID > 0 ? c.ORD_GroupProduct.SOCode : string.Empty,
                        DNCode = c.DITOGroupProductID > 0 ? c.OPS_DITOGroupProduct.DNCode : string.Empty,
                        DITOMasterID = c.DITOMasterID,
                        COTOMasterID = c.COTOMasterID,
                        OrderGroupProductID = c.OrderGroupProductID,
                        OrderContainerID = c.OrderContainerID,
                        RoutingName = c.OrderGroupProductID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                        CustomerCode = c.ORD_Order.CUS_Customer.Code,
                        CustomerName = c.ORD_Order.CUS_Customer.CustomerName,
                        OrderCode = c.OrderID.HasValue ? c.ORD_Order.Code : string.Empty,
                        COTOMasterCode = c.COTOMasterID > 0 ? c.OPS_COTOMaster.Code : string.Empty,
                        DITOMasterCode = c.DITOMasterID > 0 ? c.OPS_DITOMaster.Code : string.Empty,

                        DateData = c.DateData,
                        DateFromCome = c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        DateDN = c.DateDN,
                        DateInvoice = c.DateInvoice,
                        ETARequest = c.DateOrderETDRequest,
                        DateTOMasterETD = c.DateTOMasterETD,
                        DateTOMasterETA = c.DateTOMasterETA,
                        DateTOMasterATD = c.DateTOMasterATD,
                        DateTOMasterATA = c.DateTOMasterATA,
                        DateOrderETD = c.DateOrderETD,
                        DateOrderETA = c.DateOrderETA,
                        DateOrderETDRequest = c.DateOrderETDRequest,
                        DateOrderETARequest = c.DateOrderETARequest,
                        DateOrderCutOfTime = c.DateOrderCutOfTime,
                        KPIDate = c.KPIDate,
                        IsKPI = c.IsKPI,
                        ReasonID = c.ReasonID,
                        ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "",
                        ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "",
                        Note = c.Note,
                        DITOGroupProductStatusPODName = c.OPS_DITOGroupProduct.SYS_Var1.ValueOfVar,
                        ContractDisplayName = c.ORD_Order.ContractID > 0 ? c.ORD_Order.CAT_Contract.DisplayName : string.Empty,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<KPITimeDate>;
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

        public void KPITimeDate_Save(KPITimeDate item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.KPI_TimeDate.FirstOrDefault(c => c.ID == item.ID && item.IsKPI.HasValue);
                    if (obj != null && obj.ReasonID != item.ReasonID)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.ReasonID = item.ReasonID;
                        obj.Note = item.Note;
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

        public void KPITimeDate_Generate(DateTime from, DateTime to, int cusID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in model.KPI_TimeDate.Where(c => c.ReasonID == null && c.OrderID > 0 && c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.DateData >= from && c.DateData <= to && c.ORD_Order.CustomerID == cusID))
                        model.KPI_TimeDate.Remove(item);
                    model.SaveChanges();

                    var lstOrderID = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.DITOMasterID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 &&
                        c.ORD_GroupProduct.ORD_Order.ContractID > 0 && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_GroupProduct.DateConfig >= from && c.ORD_GroupProduct.DateConfig <= to).Select(c => c.ORD_GroupProduct.OrderID).Distinct().ToList();

                    HelperKPI.KPITime_Generate(model, Account, null, lstOrderID, null, true);
                    HelperKPI.KPITime_Generate(model, Account, null, lstOrderID, null, false);
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

        public List<KPIReason> KPITimeDateReason_List(int kpiID)
        {
            try
            {
                List<KPIReason> result = new List<KPIReason>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_Reason.Where(c => c.KPIID == kpiID).Select(c => new KPIReason
                    {
                        ID = c.ID,
                        Code = c.Code,
                        KPIName = c.KPI_KPI.KPIName,
                        KPICode = c.KPI_KPI.Code,
                        ReasonName = c.ReasonName,
                        KPIID = c.KPIID
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

        public List<KPITimeDate> KPITimeDate_Excel(int kpiID, int cusID, DateTime from, DateTime to)
        {
            try
            {
                List<KPITimeDate> result = new List<KPITimeDate>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_TimeDate.Where(c => c.TypeOfKPIID == kpiID && c.OrderID > 0 && c.ORD_Order.CustomerID == cusID && c.DateData >= from && c.DateData <= to).Select(c => new KPITimeDate
                    {
                        ID = c.ID,
                        KPIID = c.TypeOfKPIID,
                        KPICode = c.KPI_TypeOfKPI.Code,
                        CustomerID = c.ORD_Order.CustomerID,
                        OrderID = c.OrderID,
                        SOCode = c.OrderGroupProductID > 0 ? c.ORD_GroupProduct.SOCode : string.Empty,
                        DNCode = c.DITOGroupProductID > 0 ? c.OPS_DITOGroupProduct.DNCode : string.Empty,
                        DITOMasterID = c.DITOMasterID,
                        COTOMasterID = c.COTOMasterID,
                        OrderGroupProductID = c.OrderGroupProductID,
                        OrderContainerID = c.OrderContainerID,
                        RoutingName = c.OrderGroupProductID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                        CustomerCode = c.ORD_Order.CUS_Customer.Code,
                        CustomerName = c.ORD_Order.CUS_Customer.CustomerName,
                        OrderCode = c.OrderID.HasValue ? c.ORD_Order.Code : string.Empty,
                        COTOMasterCode = c.COTOMasterID > 0 ? c.OPS_COTOMaster.Code : string.Empty,
                        DITOMasterCode = c.DITOMasterID > 0 ? c.OPS_DITOMaster.Code : string.Empty,

                        DateData = c.DateData,
                        DateFromCome = c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        DateDN = c.DateDN,
                        DateInvoice = c.DateInvoice,
                        ETARequest = c.DateOrderETARequest,
                        DateTOMasterETD = c.DateTOMasterETD,
                        DateTOMasterETA = c.DateTOMasterETA,
                        DateTOMasterATD = c.DateTOMasterATD,
                        DateTOMasterATA = c.DateTOMasterATA,
                        DateOrderETD = c.DateOrderETD,
                        DateOrderETA = c.DateOrderETA,
                        DateOrderETDRequest = c.DateOrderETDRequest,
                        DateOrderETARequest = c.DateOrderETARequest,
                        DateOrderCutOfTime = c.DateOrderCutOfTime,
                        KPIDate = c.KPIDate,
                        IsKPI = c.IsKPI,
                        ReasonID = c.ReasonID,
                        ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "",
                        ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "",
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

        public List<DTOTypeOfKPI> KPITimeDate_GetTypeOfKPI()
        {
            try
            {
                List<DTOTypeOfKPI> result = new List<DTOTypeOfKPI>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_TypeOfKPI.Where(c => c.KPITypeOfKPIID == -(int)SYSVarType.KPITypeOfKPITimeDateCustomer || c.KPITypeOfKPIID == -(int)SYSVarType.KPITypeOfKPITimeDateVendor).Select(c => new DTOTypeOfKPI
                    {
                        ID = c.ID,
                        Code = c.Code,
                       TypeName = c.TypeName,
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

        #region KPIReason
        public List<KPIKPI> KPIKPI_List()
        {
            try
            {
                List<KPIKPI> result = new List<KPIKPI>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_KPI.Select(c => new KPIKPI
                    {
                        ID = c.ID,
                        Code = c.Code,
                        KPIName = c.KPIName,
                        TypeOfKPIID = c.KPITypeID,
                        TypeOfKPIName = c.SYS_Var.ValueOfVar
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

        public DTOResult KPIReason_List(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var objRequest = new Kendo.Mvc.UI.DataSourceRequest();
                    if (!string.IsNullOrEmpty(request))
                        objRequest = CreateRequest(request);
                    var query = model.KPI_Reason.Select(c => new KPIReason
                    {
                        ID = c.ID,
                        Code = c.Code,
                        KPIName = c.KPI_KPI.KPIName,
                        KPICode = c.KPI_KPI.Code,
                        ReasonName = c.ReasonName,
                        KPIID = c.KPIID
                    }).ToDataSourceResult(objRequest);
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<KPIReason>;
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

        public int KPIReason_Save(KPIReason item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.KPI_Reason.FirstOrDefault(c => c.ID != item.ID && c.Code == item.Code);
                    if (obj != null)
                        throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng.");
                    obj = model.KPI_Reason.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new KPI_Reason();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;

                        model.KPI_Reason.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.ReasonName = item.ReasonName;
                    obj.KPIID = item.KPIID;

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

        public void KPIReason_Delete(int id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (model.KPI_KPITime.Count(c => c.ReasonID == id) > 0)
                        throw FaultHelper.BusinessFault(null, null, "Lý do đã sử dụng!");

                    var obj = model.KPI_Reason.FirstOrDefault(c => c.ID == id);
                    if (obj != null)
                    {
                        model.KPI_Reason.Remove(obj);
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

        public List<KPIReason> KPIReason_Export()
        {
            try
            {
                List<KPIReason> result = new List<KPIReason>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_Reason.Select(c => new KPIReason
                    {
                        ID = c.ID,
                        Code = c.Code,
                        KPIName = c.KPI_KPI.KPIName,
                        KPICode = c.KPI_KPI.Code,
                        ReasonName = c.ReasonName,
                        KPIID = c.KPIID
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

        public void KPIReason_Import(List<KPIReason_Import> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in data)
                    {
                        var obj = model.KPI_Reason.FirstOrDefault(c => c.Code == item.Code);
                        if (obj == null)
                        {
                            obj = new KPI_Reason();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                            obj.Code = item.Code;

                            model.KPI_Reason.Add(obj);
                        }
                        else
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }
                        obj.ReasonName = item.ReasonName;
                        obj.KPIID = item.KPIID;
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

        #region KPIDashBoard

        public List<KPIDashBoard_DN> Dashboard_DN_Data(int cusID, int kpiID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIDashBoard_DN> result = new List<KPIDashBoard_DN>();
                using (var model = new DataEntities())
                {
                    var lstID = new List<int>();
                    if (cusID > 0)
                        lstID.Add(cusID);
                    else
                        lstID = model.CUS_Customer.Where(c => !c.IsSystem && c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS && Account.ListCustomerID.Contains(c.ID)).Select(c => c.ID).ToList();

                    var kpiData = model.KPI_KPITime.Where(c => c.OrderID > 0 && lstID.Contains(c.ORD_Order.CustomerID) && c.KPIID == kpiID && c.DateData >= from && c.DateData <= to).Select(c => new
                    {
                        DateData = c.DateData,
                        DateDN = c.DateDN,
                        IsKPI = c.IsKPI,
                        CusID = c.ORD_Order.CustomerID
                    }).ToList();
                    for (var date = from.Date; date <= to.Date; date = date.AddDays(1))
                    {
                        KPIDashBoard_DN obj = new KPIDashBoard_DN();
                        obj.Date = date.Date;
                        var data = kpiData.Where(c => c.DateData >= date && c.DateData <= date.AddDays(1)).ToList();
                        obj.Total = data.Count;
                        obj.TotalFail = data.Count(c => c.IsKPI == false);
                        obj.TotalSuccess = data.Count(c => c.IsKPI == true);
                        obj.TotalNull = data.Count(c => c.IsKPI == null);
                        var val = obj.TotalFail + obj.TotalSuccess;
                        if (val > 0)
                            obj.Goal = (double)Math.Round((decimal)obj.TotalSuccess / val, 4) * 100;
                        result.Add(obj);
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

        public List<KPIDashBoard_Reason> Dashboard_Reason_Data(int cusID, int kpiID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIDashBoard_Reason> result = new List<KPIDashBoard_Reason>();
                using (var model = new DataEntities())
                {
                    var lstID = new List<int>();
                    if (cusID > 0)
                        lstID.Add(cusID);
                    else
                        lstID = model.CUS_Customer.Where(c => !c.IsSystem && c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS && Account.ListCustomerID.Contains(c.ID)).Select(c => c.ID).ToList();

                    var kpiTime = model.KPI_KPITime.Count(c => c.OrderID > 0 && lstID.Contains(c.ORD_Order.CustomerID) && c.DateData >= from && c.DateData <= to && c.KPIID == kpiID && c.IsKPI == false && c.ReasonID > 0);
                    if (kpiTime > 0)
                    {
                        var kpiData = model.KPI_KPITime.Where(c => c.OrderID > 0 && lstID.Contains(c.ORD_Order.CustomerID) && c.DateData >= from && c.DateData <= to && c.KPIID == kpiID && c.IsKPI == false).Select(c => new
                        {
                            DateData = c.DateData,
                            ReasonID = c.ReasonID,
                            ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "NoReason",
                            ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "Chưa chọn lý do",
                            CusID = c.ORD_Order.CustomerID
                        }).GroupBy(c => c.ReasonID).ToList();
                        foreach (var kpi in kpiData)
                        {
                            KPIDashBoard_Reason obj = new KPIDashBoard_Reason();
                            var o = kpi.FirstOrDefault();
                            obj.ID = kpi.Key.HasValue ? kpi.Key.Value : -1;
                            obj.ReasonCode = o.ReasonCode;
                            obj.ReasonName = o.ReasonName;
                            obj.Percent = (double)Math.Round((decimal)kpi.Count() / kpiTime, 2);
                            result.Add(obj);
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

        public List<KPIDashBoard_DN> Dashboard_DN_VENData(int venID, int kpiID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIDashBoard_DN> result = new List<KPIDashBoard_DN>();
                using (var model = new DataEntities())
                {
                    var lstID = new List<int>();
                    if (venID > 0)
                        lstID.Add(venID);
                    else
                        lstID = model.CUS_Customer.Where(c => !c.IsSystem && c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN && Account.ListCustomerID.Contains(c.ID)).Select(c => c.ID).ToList();

                    var kpiData = model.KPI_VENTime.Where(c => c.DITOGroupProductID > 0 && lstID.Contains(c.VendorID) && c.KPIID == kpiID && c.DateData >= from && c.DateData <= to).Select(c => new
                    {
                        DateData = c.DateData,
                        DateDN = c.DateDN,
                        IsKPI = c.IsKPI,
                        VenID = c.VendorID
                    }).ToList();
                    for (var date = from.Date; date <= to.Date; date = date.AddDays(1))
                    {
                        KPIDashBoard_DN obj = new KPIDashBoard_DN();
                        obj.Date = date.Date;
                        var data = kpiData.Where(c => c.DateData >= date && c.DateData <= date.AddDays(1)).ToList();
                        obj.Total = data.Count;
                        obj.TotalFail = data.Count(c => c.IsKPI == false);
                        obj.TotalSuccess = data.Count(c => c.IsKPI == true);
                        obj.TotalNull = data.Count(c => c.IsKPI == null);
                        var val = obj.TotalFail + obj.TotalSuccess;
                        if (val > 0)
                            obj.Goal = (double)Math.Round((decimal)obj.TotalSuccess / val, 4) * 100;
                        result.Add(obj);
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

        public List<KPIDashBoard_Reason> Dashboard_Reason_VENData(int venID, int kpiID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIDashBoard_Reason> result = new List<KPIDashBoard_Reason>();
                using (var model = new DataEntities())
                {
                    var lstID = new List<int>();
                    if (venID > 0)
                        lstID.Add(venID);
                    else
                        lstID = model.CUS_Customer.Where(c => !c.IsSystem && c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN && Account.ListCustomerID.Contains(c.ID)).Select(c => c.ID).ToList();

                    var kpiTime = model.KPI_VENTime.Count(c => c.DITOGroupProductID > 0 && lstID.Contains(c.VendorID) && c.DateData >= from && c.DateData <= to && c.KPIID == kpiID && c.IsKPI == false && c.ReasonID > 0);
                    if (kpiTime > 0)
                    {
                        var kpiData = model.KPI_VENTime.Where(c => c.DITOGroupProductID > 0 && lstID.Contains(c.VendorID) && c.DateData >= from && c.DateData <= to && c.KPIID == kpiID && c.IsKPI == false).Select(c => new
                        {
                            DateData = c.DateData,
                            ReasonID = c.ReasonID,
                            ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "NoReason",
                            ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "Chưa chọn lý do",
                            VenID = c.VendorID
                        }).GroupBy(c => c.ReasonID).ToList();
                        foreach (var kpi in kpiData)
                        {
                            KPIDashBoard_Reason obj = new KPIDashBoard_Reason();
                            var o = kpi.FirstOrDefault();
                            obj.ID = kpi.Key.HasValue ? kpi.Key.Value : -1;
                            obj.ReasonCode = o.ReasonCode;
                            obj.ReasonName = o.ReasonName;
                            obj.Percent = (double)Math.Round((decimal)kpi.Count() / kpiTime, 2);
                            result.Add(obj);
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

        #region KPI VEN
        public DTOResult KPIVENTime_List(string request, int kpiID, int venID, DateTime from, DateTime to)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.KPI_VENTime.Where(c => c.KPIID == kpiID && c.VendorID == venID && c.OrderID > 0 && c.DateData >= from && c.DateData <= to && c.DITOGroupProductID > 0).Select(c => new KPIVENTime
                    {
                        ID = c.ID,
                        KPIID = c.KPIID,
                        KPICode = c.KPI_KPI.Code,

                        VendorID = c.VendorID,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.CustomerName,

                        CustomerID = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID,
                        OrderID = c.OrderID,
                        SOCode = c.OrderGroupProductID > 0 ? c.ORD_GroupProduct.SOCode : string.Empty,
                        DNCode = c.DITOGroupProductID > 0 ? c.OPS_DITOGroupProduct.DNCode : string.Empty,
                        DITOMasterID = c.DITOMasterID,
                        COTOMasterID = c.COTOMasterID,
                        OrderGroupProductID = c.OrderGroupProductID,
                        OrderContainerID = c.OrderContainerID,
                        RoutingName = c.OrderGroupProductID > 0 && c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName : string.Empty,
                        CustomerCode = c.CUS_Customer.Code,
                        CustomerName = c.CUS_Customer.CustomerName,
                        OrderCode = c.OrderID.HasValue ? c.ORD_Order.Code : string.Empty,
                        COTOMasterCode = c.COTOMasterID > 0 ? c.OPS_COTOMaster.Code : string.Empty,
                        DITOMasterCode = c.DITOMasterID > 0 ? c.OPS_DITOMaster.Code : string.Empty,

                        DateData = c.DateData,
                        DateFromCome = c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        DateDN = c.DateDN,
                        DateInvoice = c.DateInvoice,
                        ETARequest = c.ETARequest,
                        KPIDate = c.KPIDate,
                        IsKPI = c.IsKPI,
                        ReasonID = c.ReasonID,
                        ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "",
                        ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "",
                        Note = c.Note
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<KPIVENTime>;
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

        public void KPIVENTime_Save(KPIVENTime item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.KPI_VENTime.FirstOrDefault(c => c.ID == item.ID && item.IsKPI.HasValue);
                    if (obj != null && obj.ReasonID != item.ReasonID)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.ReasonID = item.ReasonID;
                        obj.Note = item.Note;
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

        public void KPIVENTime_Generate(DateTime from, DateTime to)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in model.KPI_VENTime.Where(c => c.ReasonID == null && c.DITOGroupProductID > 0 && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOGroupProduct.DateConfig >= from && c.OPS_DITOGroupProduct.DateConfig < to))
                        model.KPI_VENTime.Remove(item);
                    model.SaveChanges();

                    var lstDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.DITOMasterID > 0 && c.CATRoutingID > 0 &&
                        c.OPS_DITOMaster.ContractID > 0 && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.DateConfig >= from && c.DateConfig < to).Select(c => new
                        {
                            c.ID,
                            c.ORD_GroupProduct.ORD_Order.Code,
                            c.DNCode,
                            c.ORD_GroupProduct.ORD_Order.CustomerID,
                            c.ORD_GroupProduct.OrderID,
                            c.DITOMasterID,
                            c.OrderGroupProductID,
                            ContractID = c.OPS_DITOMaster.ContractID.Value,
                            CATRoutingID = c.CATRoutingID,
                            c.ORD_GroupProduct.ETARequest,
                            c.DateDN,
                            c.ORD_GroupProduct.ORD_Order.RequestDate,
                            c.DateFromCome,
                            c.DateFromLeave,
                            c.DateFromLoadStart,
                            c.DateFromLoadEnd,
                            c.DateToCome,
                            c.DateToLeave,
                            c.DateToLoadStart,
                            c.DateToLoadEnd,
                            c.InvoiceDate,
                            VendorID = c.OPS_DITOMaster.VendorOfVehicleID == null ? Account.SYSCustomerID : c.OPS_DITOMaster.VendorOfVehicleID.Value,
                        }).ToList();

                    var lstVendorID = lstDITOGroupProduct.Select(c => c.VendorID).Distinct().ToList();
                    var lstKPI = model.CAT_ContractKPITime.Where(c => c.CAT_ContractRouting.CAT_Contract.SYSCustomerID == Account.SYSCustomerID && c.CAT_ContractRouting.CAT_Contract.CustomerID > 0 &&
                        lstVendorID.Contains(c.CAT_ContractRouting.CAT_Contract.CustomerID.Value)).Select(c => new DTOContractKPITime
                        {
                            CustomerID = c.CAT_ContractRouting.CAT_Contract.CustomerID,
                            ContractID = c.CAT_ContractRouting.ContractID,
                            CATRoutingID = c.CAT_ContractRouting.RoutingID,
                            Expression = c.Expression,
                            CompareField = c.CompareField,
                            KPIID = c.KPIID,
                            KPICode = c.KPI_KPI.Code
                        }).ToList();
                    var lstRouting = model.CAT_ContractRouting.Where(c => c.CAT_Contract.SYSCustomerID == Account.SYSCustomerID && c.CAT_Contract.CustomerID > 0 &&
                        lstVendorID.Contains(c.CAT_Contract.CustomerID.Value)).Select(c => new
                        {
                            c.ContractID,
                            CATRoutingID = c.RoutingID,
                            c.Zone,
                            c.LeadTime
                        }).ToList();

                    //Danh sach cong thuc
                    Dictionary<string, OfficeOpenXml.ExcelPackage> dicKPICode = new Dictionary<string, OfficeOpenXml.ExcelPackage>();
                    foreach (var item in lstKPI.Distinct())
                    {
                        if (!string.IsNullOrEmpty(item.Expression) && !string.IsNullOrEmpty(item.CompareField))
                        {
                            var lst = lstKPI.Where(c => c.ContractID == item.ContractID && c.CATRoutingID == item.CATRoutingID).ToList();
                            dicKPICode.Add(item.ContractID + "_" + item.CATRoutingID + "_" + item.KPICode, HelperKPI.KPITime_GetPackage(item.Expression, item.CompareField, item.KPICode, lst, item.Zone, item.LeadTime));
                        }
                    }

                    //Tinh KPI
                    foreach (var itemGroupProduct in lstDITOGroupProduct)
                    {
                        var itemRouting = lstRouting.FirstOrDefault(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.CATRoutingID);
                        var lstKPIGroup = lstKPI.Where(c => c.ContractID == itemGroupProduct.ContractID && c.CATRoutingID == itemGroupProduct.CATRoutingID).ToList();

                        if (itemRouting != null && lstKPIGroup.Count > 0)
                        {
                            foreach (var itemKPIGroup in lstKPIGroup)
                            {
                                if (!string.IsNullOrEmpty(itemKPIGroup.Expression) && !string.IsNullOrEmpty(itemKPIGroup.CompareField))
                                {
                                    OfficeOpenXml.ExcelPackage kpiCode = default(OfficeOpenXml.ExcelPackage);
                                    if (dicKPICode.ContainsKey(itemGroupProduct.ContractID + "_" + itemGroupProduct.CATRoutingID + "_" + itemKPIGroup.KPICode))
                                        kpiCode = dicKPICode[itemGroupProduct.ContractID + "_" + itemGroupProduct.CATRoutingID + "_" + itemKPIGroup.KPICode];

                                    if (kpiCode != null)
                                    {
                                        var obj = model.KPI_VENTime.FirstOrDefault(c => c.VendorID == itemGroupProduct.VendorID && c.OrderID == itemGroupProduct.OrderID &&
                                            c.DITOGroupProductID == itemGroupProduct.ID);
                                        if (obj == null)
                                        {
                                            obj = new KPI_VENTime();
                                            obj.KPIID = itemKPIGroup.KPIID;
                                            obj.VendorID = itemGroupProduct.VendorID;
                                            obj.OrderID = itemGroupProduct.OrderID;
                                            obj.OrderGroupProductID = itemGroupProduct.OrderGroupProductID;
                                            obj.DITOGroupProductID = itemGroupProduct.ID;
                                            obj.DITOMasterID = itemGroupProduct.DITOMasterID;

                                            obj.CreatedBy = Account.UserName;
                                            obj.CreatedDate = DateTime.Now;
                                        }
                                        else
                                        {
                                            obj.ModifiedBy = Account.UserName;
                                            obj.ModifiedDate = DateTime.Now;
                                        }
                                        obj.DateData = itemGroupProduct.RequestDate;
                                        obj.DateRequest = itemGroupProduct.RequestDate;
                                        obj.DateDN = itemGroupProduct.DateDN;
                                        if (obj.DateDN == null)
                                            obj.DateDN = itemGroupProduct.RequestDate;
                                        obj.DateFromCome = itemGroupProduct.DateFromCome;
                                        obj.DateFromLeave = itemGroupProduct.DateFromLeave;
                                        obj.DateFromLoadStart = itemGroupProduct.DateFromLoadStart;
                                        obj.DateFromLoadEnd = itemGroupProduct.DateFromLoadEnd;
                                        obj.DateToCome = itemGroupProduct.DateToCome;
                                        obj.DateToLeave = itemGroupProduct.DateToLeave;
                                        obj.DateToLoadStart = itemGroupProduct.DateToLoadStart;
                                        obj.DateToLoadEnd = itemGroupProduct.DateToLoadEnd;
                                        obj.DateInvoice = itemGroupProduct.InvoiceDate;
                                        obj.ETARequest = itemGroupProduct.ETARequest;

                                        obj.Zone = itemRouting.Zone;
                                        if (obj.Zone == null) obj.Zone = 0;
                                        obj.LeadTime = itemRouting.LeadTime;
                                        if (obj.LeadTime == null) obj.LeadTime = 0;
                                        obj.Note = string.Empty;

                                        try
                                        {
                                            HelperKPI.KPIVENTime_GetDate(kpiCode, itemKPIGroup.KPICode, obj, lstKPIGroup);
                                            if (obj.IsKPI == true && obj.ReasonID > 0)
                                            {
                                                obj.ReasonID = null;
                                                obj.Note = string.Empty;
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            obj.Note = ex.Message;
                                        }

                                        if (obj.ID < 1)
                                            model.KPI_VENTime.Add(obj);
                                    }
                                }
                            }
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

        public List<KPIVENTime> KPIVENTime_Excel(int kpiID, int venID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIVENTime> result = new List<KPIVENTime>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_VENTime.Where(c => c.KPIID == kpiID && c.VendorID == venID && c.DateData >= from && c.DateData <= to).Select(c => new KPIVENTime
                    {
                        ID = c.ID,
                        KPIID = c.KPIID,
                        KPICode = c.KPI_KPI.Code,
                        CustomerID = c.OrderID.HasValue ? c.ORD_Order.CustomerID : -1,
                        CustomerCode = c.OrderID.HasValue ? c.ORD_Order.CUS_Customer.Code :string.Empty,
                        CustomerName = c.OrderID.HasValue ? c.ORD_Order.CUS_Customer.CustomerName : string.Empty,

                        VendorID = c.VendorID,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.CustomerName,

                        OrderID = c.OrderID,
                        OrderCode = c.OrderID.HasValue ? c.ORD_Order.Code : string.Empty,

                        SOCode = c.OrderGroupProductID > 0 ? c.ORD_GroupProduct.SOCode : string.Empty,
                        DNCode = c.DITOGroupProductID > 0 ? c.OPS_DITOGroupProduct.DNCode : string.Empty,
                        DITOMasterID = c.DITOMasterID,
                        COTOMasterID = c.COTOMasterID,
                        OrderGroupProductID = c.OrderGroupProductID,
                        OrderContainerID = c.OrderContainerID,
                        RoutingName = c.DITOGroupProductID > 0 && c.OPS_DITOGroupProduct.CATRoutingID > 0 ? c.OPS_DITOGroupProduct.CAT_Routing.RoutingName : string.Empty,


                        COTOMasterCode = c.COTOMasterID > 0 ? c.OPS_COTOMaster.Code : string.Empty,
                        DITOMasterCode = c.DITOMasterID > 0 ? c.OPS_DITOMaster.Code : string.Empty,

                        DateData = c.DateData,
                        DateFromCome = c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        DateDN = c.DateDN,
                        DateInvoice = c.DateInvoice,
                        ETARequest = c.ETARequest,
                        KPIDate = c.KPIDate,
                        IsKPI = c.IsKPI,
                        ReasonID = c.ReasonID,
                        ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "",
                        ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "",
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

        #endregion

        #region KPI VEN Tender
        public DTOResult KPIVENTenderFTL_List(string request, int venID, DateTime from, DateTime to)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var lstID = new List<int>();
                    if (venID > 0)
                        lstID.Add(venID);
                    else
                        lstID = model.CUS_Customer.Where(c => !c.IsSystem && c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN && Account.ListCustomerID.Contains(c.ID)).Select(c => c.ID).ToList();

                    var query = model.KPI_VENTenderFTL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.KPIID == (int)KPICode.TenderFTL && lstID.Contains(c.VendorID) && c.DateData >= from && c.DateData <= to).Select(c => new KPIVENTenderFTL
                    {
                        ID = c.ID,
                        KPIID = c.KPIID,
                        KPICode = c.KPI_KPI.Code,

                        VendorID = c.VendorID,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.CustomerName,

                        TotalAccept = c.TotalAccept,
                        TotalReject = c.TotalReject,
                        TotalSchedule = c.TotalSchedule,
                        TotalKPI = c.TotalKPI,

                        DateData = c.DateData,
                        IsKPI = c.IsKPI,
                        ReasonID = c.ReasonID,
                        ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "",
                        ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "",
                        Note = c.Note
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<KPIVENTenderFTL>;
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

        public void KPIVENTenderFTL_Save(KPIVENTenderFTL item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.KPI_VENTenderFTL.FirstOrDefault(c => c.ID == item.ID && item.IsKPI.HasValue);
                    if (obj != null && obj.ReasonID != item.ReasonID)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.ReasonID = item.ReasonID;
                        obj.Note = item.Note;
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

        public void KPIVENTenderFTL_Generate(DateTime from, DateTime to)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in model.KPI_VENTenderFTL.Where(c => c.ReasonID == null && c.DateData >= from && c.DateData < to && c.KPI_VENTenderFTLDetail.Any(d => d.DITOMasterID.HasValue) && c.SYSCustomerID == Account.SYSCustomerID))
                    {
                        foreach (var itemDetail in model.KPI_VENTenderFTLDetail.Where(c => c.VENTenderFTLID == item.ID))
                            model.KPI_VENTenderFTLDetail.Remove(itemDetail);

                        model.KPI_VENTenderFTL.Remove(item);
                    }
                    model.SaveChanges();

                    var lstRate = model.OPS_DITORate.Where(c => c.DITOMasterID > 0 && c.VendorID.HasValue && c.IsSend && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.ETD >= from && c.OPS_DITOMaster.ETD < to && c.IsAccept.HasValue && c.OPS_DITOMaster.ContractID.HasValue).Select(c=> new
                    {
                        c.FirstRateTime,
                        c.LastRateTime,
                        c.DITOMasterID,
                        c.OPS_DITOMaster.ETA,
                        c.OPS_DITOMaster.ETD,
                        c.IsAccept,
                        c.ReasonID,
                        c.VendorID,
                    }).Distinct().ToList();

                    if (lstRate != null && lstRate.Count() > 0)
                    {
                        var lstDate = lstRate.Select(c => c.ETD.Value.Date).Distinct().ToList();
                        foreach (var date in lstDate)
                        {
                            var lstVendor = lstRate.Where(c => c.ETD.Value.Date == date).Select(c => c.VendorID).Distinct().ToList();
                            foreach (var itemVendor in lstVendor)
                            {
                                // Lấy hợp đồng theo vendor, ngày hiệu lực
                                var lstKPI = model.CAT_Contract.Where(c => c.CustomerID == itemVendor && (c.EffectDate <= date || c.EffectDate <= to) && !string.IsNullOrEmpty(c.ExprVENTenderFTL) && !string.IsNullOrEmpty(c.ExprVENTenderFTLCompareField)).Select(c => new
                                {
                                    c.EffectDate,
                                    c.ExprVENTenderFTL,
                                    c.ExprVENTenderFTLCompareField,
                                }).ToList();

                                var kpi = lstKPI.Where(c => c.EffectDate <= date).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                                if (kpi != null)
                                {
                                    KPI_VENTenderFTL itemKPI = new KPI_VENTenderFTL();
                                    itemKPI.CreatedBy = Account.UserName;
                                    itemKPI.CreatedDate = DateTime.Now;
                                    itemKPI.SYSCustomerID = Account.SYSCustomerID;
                                    itemKPI.VendorID = itemVendor.Value;
                                    itemKPI.DateData = date;
                                    itemKPI.KPIID = (int)KPICode.TenderFTL;
                                    model.KPI_VENTenderFTL.Add(itemKPI);

                                    var lstRateVen = lstRate.Where(c => c.ETD.Value.Date == date && c.VendorID == itemVendor);
                                    foreach (var itemRateVen in lstRateVen)
                                    {
                                        KPI_VENTenderFTLDetail itemKPIDetail = new KPI_VENTenderFTLDetail();
                                        itemKPIDetail.CreatedBy = Account.UserName;
                                        itemKPIDetail.CreatedDate = DateTime.Now;
                                        itemKPIDetail.DITOMasterID = itemRateVen.DITOMasterID;
                                        itemKPIDetail.DateData = date;
                                        itemKPIDetail.FirstRateTime = itemRateVen.FirstRateTime;
                                        itemKPIDetail.LastRateTime = itemRateVen.LastRateTime;
                                        itemKPIDetail.ETA = itemRateVen.ETA;
                                        itemKPIDetail.ETD = itemRateVen.ETD;
                                        itemKPIDetail.IsAccept = itemRateVen.IsAccept;
                                        itemKPIDetail.CATReasonID = itemRateVen.ReasonID;
                                        itemKPI.KPI_VENTenderFTLDetail.Add(itemKPIDetail);
                                    }

                                    itemKPI.TotalSchedule = lstRateVen.Select(c => c.DITOMasterID).Distinct().Count();
                                    itemKPI.TotalAccept = lstRateVen.Select(c => c.IsAccept == true).Distinct().Count();
                                    itemKPI.TotalReject = lstRateVen.Select(c => c.IsAccept == false).Distinct().Count();

                                    // Tính KPI
                                    HelperKPI.KPIVENTenderFTL_GetValue(itemKPI, kpi.ExprVENTenderFTL, kpi.ExprVENTenderFTLCompareField);
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

        public List<KPIVENTenderFTL> KPIVENTenderFTL_Excel(int venID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIVENTenderFTL> result = new List<KPIVENTenderFTL>();
                using (var model = new DataEntities())
                {
                    var lstID = new List<int>();
                    if (venID > 0)
                        lstID.Add(venID);
                    else
                        lstID = model.CUS_Customer.Where(c => !c.IsSystem && c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN && Account.ListCustomerID.Contains(c.ID)).Select(c => c.ID).ToList();

                    result = model.KPI_VENTenderFTL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.KPIID == (int)KPICode.TenderFTL && lstID.Contains(c.VendorID) && c.DateData >= from && c.DateData <= to).Select(c => new KPIVENTenderFTL
                    {
                        ID = c.ID,
                        KPIID = c.KPIID,
                        KPICode = c.KPI_KPI.Code,

                        VendorID = c.VendorID,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.CustomerName,

                        TotalAccept = c.TotalAccept,
                        TotalReject = c.TotalReject,
                        TotalSchedule = c.TotalSchedule,
                        TotalKPI = c.TotalKPI,

                        DateData = c.DateData,
                        IsKPI = c.IsKPI,
                        ReasonID = c.ReasonID,
                        ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "",
                        ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "",
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

        public List<KPIDashBoard_DN> KPIVENTenderFTL_Dashboard(int venID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIDashBoard_DN> result = new List<KPIDashBoard_DN>();
                using (var model = new DataEntities())
                {
                    var lstID = new List<int>();
                    if (venID > 0)
                        lstID.Add(venID);
                    else
                        lstID = model.CUS_Customer.Where(c => !c.IsSystem && c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN && Account.ListCustomerID.Contains(c.ID)).Select(c => c.ID).ToList();

                    var kpiData = model.KPI_VENTenderFTL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.KPIID == (int)KPICode.TenderFTL && lstID.Contains(c.VendorID) && c.DateData >= from && c.DateData <= to).Select(c => new
                    {
                        DateData = c.DateData,
                        IsKPI = c.IsKPI,
                        VendorID = c.VendorID,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.CustomerName,
                        VendorShortName = c.CUS_Customer.ShortName
                    }).ToList();
                    for (var date = from.Date; date <= to.Date; date = date.AddDays(1))
                    {
                        KPIDashBoard_DN obj = new KPIDashBoard_DN();
                        obj.Date = date.Date;
                        var data = kpiData.Where(c => c.DateData >= date && c.DateData <= date.AddDays(1)).ToList();
                        obj.Total = data.Count;
                        obj.TotalFail = data.Count(c => c.IsKPI == false);
                        obj.TotalSuccess = data.Count(c => c.IsKPI == true);
                        obj.TotalNull = data.Count(c => c.IsKPI == null);
                        var val = obj.TotalFail + obj.TotalSuccess;
                        if (val > 0)
                            obj.Goal = (double)Math.Round((decimal)obj.TotalSuccess / val, 4) * 100;
                        result.Add(obj);
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

        public List<KPIDashBoard_Reason> KPIVENTenderFTL_Dashboard_Reason(int venID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIDashBoard_Reason> result = new List<KPIDashBoard_Reason>();
                using (var model = new DataEntities())
                {
                    var lstID = new List<int>();
                    if (venID > 0)
                        lstID.Add(venID);
                    else
                        lstID = model.CUS_Customer.Where(c => !c.IsSystem && c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN && Account.ListCustomerID.Contains(c.ID)).Select(c => c.ID).ToList();

                    var kpiTime = model.KPI_VENTenderFTL.Count(c => c.SYSCustomerID == Account.SYSCustomerID && c.KPIID == (int)KPICode.TenderFTL && lstID.Contains(c.VendorID) && c.DateData >= from && c.DateData <= to && c.ReasonID > 0);
                    if (kpiTime > 0)
                    {
                        var kpiData = model.KPI_VENTenderFTL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.KPIID == (int)KPICode.TenderFTL && lstID.Contains(c.VendorID) && c.DateData >= from && c.DateData <= to && c.IsKPI == false).Select(c => new
                        {
                            DateData = c.DateData,
                            ReasonID = c.ReasonID,
                            ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "NoReason",
                            ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "Chưa chọn lý do",
                            VendorID = c.VendorID
                        }).GroupBy(c => c.ReasonID).ToList();
                        foreach (var kpi in kpiData)
                        {
                            KPIDashBoard_Reason obj = new KPIDashBoard_Reason();
                            var o = kpi.FirstOrDefault();
                            obj.ID = kpi.Key.HasValue ? kpi.Key.Value : -1;
                            obj.ReasonCode = o.ReasonCode;
                            obj.ReasonName = o.ReasonName;
                            obj.Percent = (double)Math.Round((decimal)kpi.Count() / kpiTime, 2);
                            result.Add(obj);
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


        public DTOResult KPIVENTenderLTL_List(string request, int venID, DateTime from, DateTime to)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.KPI_VENTenderLTL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.KPIID == (int)KPICode.TenderLTL && c.VendorID == venID && c.DateData >= from && c.DateData <= to).Select(c => new KPIVENTenderLTL
                    {
                        ID = c.ID,
                        KPIID = c.KPIID,
                        KPICode = c.KPI_KPI.Code,

                        VendorID = c.VendorID,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.CustomerName,

                        DateData = c.DateData,
                        IsKPI = c.IsKPI,
                        ReasonID = c.ReasonID,
                        ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "",
                        ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "",
                        Note = c.Note,

                        DIPacketDetailID = c.DIPacketDetailID,
                        TonOrder = c.TonOrder,
                        CBMOrder = c.CBMOrder,
                        QuantityOrder = c.QuantityOrder,

                        TonAccept = c.TonAccept,
                        CBMAccept = c.CBMAccept,
                        QuantityAccept = c.QuantityAccept,
                        ValueKPI = c.ValueKPI
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<KPIVENTenderLTL>;
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

        public void KPIVENTenderLTL_Save(KPIVENTenderLTL item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.KPI_VENTenderLTL.FirstOrDefault(c => c.ID == item.ID && item.IsKPI.HasValue);
                    if (obj != null && obj.ReasonID != item.ReasonID)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.ReasonID = item.ReasonID;
                        obj.Note = item.Note;
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

        public void KPIVENTenderLTL_Generate(DateTime from, DateTime to)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in model.KPI_VENTenderLTL.Where(c => c.ReasonID == null && c.DateData >= from && c.DateData < to && c.SYSCustomerID == Account.SYSCustomerID))
                        model.KPI_VENTenderLTL.Remove(item);
                    model.SaveChanges();

                    var lstPacketDetail = model.OPS_DIPacketDetailRate.Where(c => c.OPS_DIPacketDetail.OPS_DIPacket.SYSCustomerID == Account.SYSCustomerID && c.VendorID.HasValue && c.IsSend && c.IsAccept.HasValue && c.FirstRateTime >= from && c.LastRateTime < to).Select(c => new
                    {
                        c.FirstRateTime,
                        c.LastRateTime,
                        c.IsAccept,
                        c.ReasonID,
                        c.VendorID,
                        c.TonAccept,
                        c.CBMAccept,
                        c.QuantityAccept,
                        c.OPS_DIPacketDetail.TonOrder,
                        c.OPS_DIPacketDetail.CBMOrder,
                        c.OPS_DIPacketDetail.QuantityOrder,
                        c.DIPacketDetailID,
                    }).ToList();
                    if (lstPacketDetail != null && lstPacketDetail.Count() > 0)
                    {
                        foreach (var itemPacketDetailGroup in lstPacketDetail.GroupBy(c => c.VendorID))
                        {
                            // Lấy hợp đồng theo vendor, ngày hiệu lực
                            var lstKPI = model.CAT_Contract.Where(c => c.CustomerID == itemPacketDetailGroup.Key && !string.IsNullOrEmpty(c.ExprVENTenderFTL) && !string.IsNullOrEmpty(c.ExprVENTenderFTLCompareField)).Select(c => new
                            {
                                c.EffectDate,
                                c.ExprVENTenderLTL,
                                c.ExprVENTenderLTLCompareField,
                            }).ToList();

                            foreach (var itemPacketDetail in itemPacketDetailGroup)
                            {
                                var kpi = lstKPI.Where(c => c.EffectDate <= itemPacketDetail.FirstRateTime.Value.Date).OrderByDescending(c => c.EffectDate).FirstOrDefault();
                                if (kpi != null)
                                {
                                    KPI_VENTenderLTL itemKPI = new KPI_VENTenderLTL();
                                    itemKPI.CreatedBy = Account.UserName;
                                    itemKPI.CreatedDate = DateTime.Now;
                                    itemKPI.SYSCustomerID = Account.SYSCustomerID;
                                    itemKPI.VendorID = itemPacketDetail.VendorID.Value;
                                    itemKPI.DateData = itemPacketDetail.FirstRateTime.Value.Date;
                                    itemKPI.KPIID = (int)KPICode.TenderLTL;
                                    itemKPI.DIPacketDetailID = itemPacketDetail.DIPacketDetailID;
                                    itemKPI.TonOrder = itemPacketDetail.TonOrder;
                                    itemKPI.CBMOrder = itemPacketDetail.CBMOrder;
                                    itemKPI.QuantityOrder = itemPacketDetail.QuantityOrder;
                                    itemKPI.TonAccept = itemPacketDetail.TonAccept;
                                    itemKPI.CBMAccept = itemPacketDetail.CBMAccept;
                                    itemKPI.QuantityAccept = itemPacketDetail.QuantityAccept;
                                    model.KPI_VENTenderLTL.Add(itemKPI);

                                    // Tính KPI
                                    HelperKPI.KPIVENTenderLTL_GetValue(itemKPI, kpi.ExprVENTenderLTL, kpi.ExprVENTenderLTLCompareField);
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

        public List<KPIVENTenderLTL> KPIVENTenderLTL_Excel(int venID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIVENTenderLTL> result = new List<KPIVENTenderLTL>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_VENTenderLTL.Where(c => c.KPIID == (int)KPICode.TenderLTL && c.VendorID == venID && c.DateData >= from && c.DateData <= to).Select(c => new KPIVENTenderLTL
                    {
                        ID = c.ID,
                        KPIID = c.KPIID,
                        KPICode = c.KPI_KPI.Code,

                        VendorID = c.VendorID,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.CustomerName,

                        TonOrder = c.TonOrder,
                        CBMOrder = c.CBMOrder,
                        QuantityOrder = c.QuantityOrder,
                        TonAccept = c.TonAccept,
                        CBMAccept = c.CBMAccept,
                        QuantityAccept = c.QuantityAccept,
                        //TonKPI = c.TonKPI,
                        //CBMKPI = c.CBMKPI,
                        //QuantityKPI = c.QuantityKPI,

                        DateData = c.DateData,
                        IsKPI = c.IsKPI,
                        ReasonID = c.ReasonID,
                        ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "",
                        ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "",
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

        public List<KPIDashBoard_DN> KPIVENTenderLTL_Dashboard(int venID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIDashBoard_DN> result = new List<KPIDashBoard_DN>();
                using (var model = new DataEntities())
                {
                    var lstID = new List<int>();
                    if (venID > 0)
                        lstID.Add(venID);
                    else
                        lstID = model.CUS_Customer.Where(c => !c.IsSystem && c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN && Account.ListCustomerID.Contains(c.ID)).Select(c => c.ID).ToList();

                    var kpiData = model.KPI_VENTenderLTL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.KPIID == (int)KPICode.TenderLTL && lstID.Contains(c.VendorID) && c.DateData >= from && c.DateData <= to).Select(c => new
                    {
                        DateData = c.DateData,
                        IsKPI = c.IsKPI,
                        VendorID = c.VendorID,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.CustomerName,
                        VendorShortName = c.CUS_Customer.ShortName
                    }).ToList();
                    for (var date = from.Date; date <= to.Date; date = date.AddDays(1))
                    {
                        KPIDashBoard_DN obj = new KPIDashBoard_DN();
                        obj.Date = date.Date;
                        var data = kpiData.Where(c => c.DateData >= date && c.DateData <= date.AddDays(1)).ToList();
                        obj.Total = data.Count;
                        obj.TotalFail = data.Count(c => c.IsKPI == false);
                        obj.TotalSuccess = data.Count(c => c.IsKPI == true);
                        obj.TotalNull = data.Count(c => c.IsKPI == null);
                        var val = obj.TotalFail + obj.TotalSuccess;
                        if (val > 0)
                            obj.Goal = (double)Math.Round((decimal)obj.TotalSuccess / val, 4) * 100;
                        result.Add(obj);
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

        public List<KPIDashBoard_Reason> KPIVENTenderLTL_Dashboard_Reason(int venID, DateTime from, DateTime to)
        {
            try
            {
                List<KPIDashBoard_Reason> result = new List<KPIDashBoard_Reason>();
                using (var model = new DataEntities())
                {
                    var lstID = new List<int>();
                    if (venID > 0)
                        lstID.Add(venID);
                    else
                        lstID = model.CUS_Customer.Where(c => !c.IsSystem && c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN && Account.ListCustomerID.Contains(c.ID)).Select(c => c.ID).ToList();

                    var kpiTime = model.KPI_VENTenderLTL.Count(c => c.SYSCustomerID == Account.SYSCustomerID && c.KPIID == (int)KPICode.TenderLTL && lstID.Contains(c.VendorID) && c.DateData >= from && c.DateData <= to && c.ReasonID > 0);
                    if (kpiTime > 0)
                    {
                        var kpiData = model.KPI_VENTenderLTL.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.KPIID == (int)KPICode.TenderLTL && lstID.Contains(c.VendorID) && c.DateData >= from && c.DateData <= to && c.IsKPI == false).Select(c => new
                        {
                            DateData = c.DateData,
                            ReasonID = c.ReasonID,
                            ReasonCode = c.ReasonID > 0 ? c.KPI_Reason.Code : "NoReason",
                            ReasonName = c.ReasonID > 0 ? c.KPI_Reason.ReasonName : "Chưa chọn lý do",
                            VendorID = c.VendorID
                        }).GroupBy(c => c.ReasonID).ToList();
                        foreach (var kpi in kpiData)
                        {
                            KPIDashBoard_Reason obj = new KPIDashBoard_Reason();
                            var o = kpi.FirstOrDefault();
                            obj.ID = kpi.Key.HasValue ? kpi.Key.Value : -1;
                            obj.ReasonCode = o.ReasonCode;
                            obj.ReasonName = o.ReasonName;
                            obj.Percent = (double)Math.Round((decimal)kpi.Count() / kpiTime, 2);
                            result.Add(obj);
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

        #region KPIKPI
        public DTOResult KPIKPI_GetList(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.KPI_KPI.Select(c => new KPIKPI
                    {
                        ID = c.ID,
                        Code = c.Code,
                        KPIName = c.KPIName,
                        TypeOfKPIID = c.KPITypeID,
                        TypeOfKPIName = c.SYS_Var.ValueOfVar,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<KPIKPI>;
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

        public KPIKPI KPIKPI_Get(int id)
        {
            try
            {
                KPIKPI result = new KPIKPI();
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.KPI_KPI.Where(c => c.ID == id).Select(c => new KPIKPI
                        {
                            ID = c.ID,
                            Code = c.Code,
                            KPIName = c.KPIName,
                            TypeOfKPIID = c.KPITypeID,
                            TypeOfKPIName = c.SYS_Var.ValueOfVar,
                        }).FirstOrDefault();
                    }
                    else
                    {
                        result.ID = 0;
                        var objC = model.SYS_Var.FirstOrDefault(c => c.TypeOfVar == (int)SYSVarType.KPIType);
                        if (objC != null) result.TypeOfKPIID = objC.ID;
                        else
                            result.TypeOfKPIID = -1;
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

        public int KPIKPI_Save(KPIKPI item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (model.KPI_KPI.Count(c => c.ID != item.ID && c.Code == item.Code) > 0)
                        throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");

                    var obj = model.KPI_KPI.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new KPI_KPI();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.KPI_KPI.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }

                    obj.Code = item.Code;
                    obj.KPIName = item.KPIName;
                    obj.KPITypeID = item.TypeOfKPIID;
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

        public void KPIKPI_Delete(int id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.KPI_KPI.FirstOrDefault(c => c.ID == id);
                    if (obj != null)
                    {
                        foreach (var child in model.KPI_Column.Where(c => c.KPIID == obj.ID))
                        {
                            model.KPI_Column.Remove(child);
                        }

                        model.KPI_KPI.Remove(obj);

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

        public List<KPIKPI> KPIKPI_AllList()
        {
            try
            {
                List<KPIKPI> result = new List<KPIKPI>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_KPI.Select(c => new KPIKPI
                    {
                        ID = c.ID,
                        Code = c.Code,
                        KPIName = c.KPIName,
                        TypeOfKPIID = c.KPITypeID,
                        TypeOfKPIName = c.SYS_Var.ValueOfVar,
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

        public DTOResult KPIColumn_List(string request, int KPIID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.KPI_Column.Where(c => c.KPIID == KPIID).Select(c => new KPIColumn
                    {
                        ID = c.ID,
                        Code = c.Code,
                        ColumnName = c.ColumnName,
                        ExprData = c.ExprData,
                        FieldName = c.KPI_Field.FieldName,
                        KPIColumnTypeName = c.SYS_Var.ValueOfVar,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<KPIColumn>;
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

        public KPIColumn KPIColumn_Get(int id)
        {
            try
            {
                KPIColumn result = new KPIColumn();
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.KPI_Column.Where(c => c.ID == id).Select(c => new KPIColumn
                        {
                            ID = c.ID,
                            Code = c.Code,
                            ColumnName = c.ColumnName,
                            ExprData = c.ExprData,
                            FieldID = c.FieldID.HasValue ? c.FieldID.Value : -1,
                            FieldName = c.KPI_Field.FieldName,
                            KPIColumnTypeID = c.KPIColumnTypeID,
                            KPIColumnTypeName = c.SYS_Var.ValueOfVar,
                        }).FirstOrDefault();
                    }
                    else
                    {
                        result.ID = 0;
                        var objC = model.SYS_Var.FirstOrDefault(c => c.TypeOfVar == (int)SYSVarType.KPIColumnType);
                        if (objC != null) result.KPIColumnTypeID = objC.ID;
                        else
                            result.KPIColumnTypeID = -1;

                        var objField = model.KPI_Field.FirstOrDefault();
                        if (objField != null) result.FieldID = objField.ID;
                        else
                            result.FieldID = -1;
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

        public int KPIColumn_Save(KPIColumn item, int KPIID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var objKPI = model.KPI_KPI.FirstOrDefault(c => c.ID == KPIID);
                    if (objKPI != null)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        if (model.KPI_Column.Count(c => c.ID != item.ID && c.Code == item.Code) > 0)
                            throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");

                        var obj = model.KPI_Column.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                        {
                            obj = new KPI_Column();
                            obj.CreatedBy = Account.UserName;
                            obj.CreatedDate = DateTime.Now;
                            model.KPI_Column.Add(obj);
                        }
                        else
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                        }

                        obj.KPIID = KPIID;
                        obj.Code = item.Code;
                        obj.ColumnName = item.ColumnName;
                        obj.ExprData = item.ExprData;
                        obj.FieldID = item.FieldID;
                        obj.KPIColumnTypeID = item.KPIColumnTypeID;
                        model.SaveChanges();

                        return obj.ID;
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy KPI");
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

        public void KPIColumn_Delete(int id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.KPI_Column.FirstOrDefault(c => c.ID == id);
                    if (obj != null)
                    {
                        model.KPI_Column.Remove(obj);

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

        public List<KPIField> KPIField_List(int typeID)
        {
            try
            {
                List<KPIField> result = new List<KPIField>();
                using (var model = new DataEntities())
                {
                    result = model.KPI_Field.Where(c => c.KPITypeID == typeID).Select(c => new KPIField
                    {
                        ID = c.ID,
                        Code = c.Code,
                        FieldName = c.FieldName,
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

        #region KPI Collection
        /// <summary>
        /// 1:int, 2:long, 3: decimal, 4: double, 5:date, 6: date collect, 7:bool, 8: text
        /// </summary>
        /// <param name="KPIID"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        public void KPICollection_Generate(int KPIID, DateTime from, DateTime to)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    const int KPITypeOrder = -(int)SYSVarType.KPITypeOrder;
                    const int KPITypeOrderGroupProduct = -(int)SYSVarType.KPITypeOrderGroupProduct;
                    const int KPITypeOrderContainer = -(int)SYSVarType.KPITypeOrderContainer;
                    const int KPITypeTOMaster = -(int)SYSVarType.KPITypeTOMaster;
                    const int KPITypeTOGroupProduct = -(int)SYSVarType.KPITypeTOGroupProduct;
                    const int KPITypeTOContainer = -(int)SYSVarType.KPITypeTOContainer;

                    var itemKPI = model.KPI_KPI.FirstOrDefault(c => c.ID == KPIID);
                    if (itemKPI != null)
                    {
                        model.EventAccount = Account; model.EventRunning = false;
                        foreach (var item in model.KPI_Collection.Where(c => c.KPIID == KPIID && c.DateData >= from && c.DateData <= to))
                        {
                            foreach (var itemDetail in model.KPI_CollectionDetail.Where(c => c.CollectionID == item.ID))
                                model.KPI_CollectionDetail.Remove(itemDetail);
                        }
                        model.SaveChanges();

                        to = to.AddDays(1);
                        switch (itemKPI.KPITypeID)
                        {
                            case KPITypeOrder:
                                KPICollection_Generate_Order(model, KPIID, from, to, Account);
                                break;
                            case KPITypeOrderGroupProduct:
                                KPICollection_Generate_OrderGroupProduct(model, KPIID, from, to, Account);
                                break;
                            case KPITypeOrderContainer:
                                KPICollection_Generate_OrderContainer(model, KPIID, from, to, Account);
                                break;
                            case KPITypeTOMaster:
                                KPICollection_Generate_TOMaster(model, KPIID, from, to, Account);
                                break;
                            case KPITypeTOGroupProduct:
                                KPICollection_Generate_TOGroupProduct(model, KPIID, from, to, Account);
                                break;
                            case KPITypeTOContainer:
                                KPICollection_Generate_TOContainer(model, KPIID, from, to, Account);
                                break;
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

        private void KPICollection_Generate_Order(DataEntities model, int KPIID, DateTime from, DateTime to, AccountItem Account)
        {
            try
            {
                DateTime? dtNull = null;
                List<DTOKPICollection_Order> ListData = new List<DTOKPICollection_Order>();
                List<DTOKPICollection_Column> ListColumn = new List<DTOKPICollection_Column>();
                // Lấy danh sách column tính KPI
                ListColumn = model.KPI_Column.Where(c => c.KPIID == KPIID && c.FieldID > 0 && !string.IsNullOrEmpty(c.Code)).Select(c => new DTOKPICollection_Column
                {
                    ID = c.ID,
                    Code = c.Code,
                    KPIColumnTypeID = c.KPIColumnTypeID,
                    ExprData = c.ExprData,
                    FieldType = c.KPI_Field.FieldType,
                    KPITypeID = c.KPI_Field.KPITypeID,
                }).ToList();

                var columnDetail = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDateData && c.FieldType == 6);
                if (columnDetail != null && columnDetail.Code.Split('_').Length > 1)
                {
                    string qField = columnDetail.Code.Split('_')[1];
                    var query = model.ORD_Order.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderPlaning && c.ORD_GroupProduct.Count > 0).Select(c => new DTOKPICollection_Order
                        {
                            Order_ID = c.ID,
                            Order_CustomerID = c.CustomerID,
                            Order_DateConfig = c.DateConfig,
                            Order_DateRequest = c.RequestDate,
                            Order_ExternalDate = c.ExternalDate,
                            Order_ETARequest = c.ETARequest,
                            Order_ETA = c.ETA,
                            Order_ETD = c.ETD,
                        }).ToDataSourceResult(CreateRequest_ByFieldAndDate(from, to, qField));

                    foreach (DTOKPICollection_Order item in query.Data)
                    {
                        item.ListGroup = new List<DTOKPICollection_Group>();
                        item.ListGroupFIN = new List<DTOKPICollection_GroupFIN>();
                        ListData.Add(item);
                        item.ListGroup = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.OrderID == item.Order_ID).Select(c => new DTOKPICollection_Group
                        {
                            ID = c.ID,
                            TonOrder = c.ORD_GroupProduct.Ton,
                            CBMOrder = c.ORD_GroupProduct.CBM,
                            QuantityOrder = c.ORD_GroupProduct.Quantity,
                            Ton = c.Ton,
                            CBM = c.CBM,
                            Quantity = c.Quantity,
                            TonTranfer = c.TonTranfer,
                            CBMTranfer = c.CBMTranfer,
                            QuantityTranfer = c.QuantityTranfer,
                            TonReturn = c.TonReturn,
                            CBMReturn = c.CBMReturn,
                            QuantityReturn = c.QuantityReturn,
                            TonBBGN = c.TonBBGN,
                            CBMBBGN = c.CBMBBGN,
                            QuantityBBGN = c.QuantityBBGN,
                            IsCancel = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel,
                            IsReturn = c.ORD_GroupProduct.IsReturn,
                            ATA = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ATA : dtNull,
                            ATD = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ATD : dtNull,
                            ETA = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ETA : dtNull,
                            ETD = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ETD : dtNull,
                            InvoiceDate = c.InvoiceDate,
                            OrderGroupProductID = c.OrderGroupProductID
                        }).ToList();

                        item.ListContainer = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.OrderID == item.Order_ID).Select(c => new DTOKPICollection_Container
                        {
                            ID = c.ID,
                            TonOrder = c.OPS_Container.ORD_Container.Ton,
                            PackingID = c.OPS_Container.ORD_Container.PackingID,
                            ATA = c.COTOMasterID > 0 ? c.OPS_COTOMaster.ATA : dtNull,
                            ATD = c.COTOMasterID > 0 ? c.OPS_COTOMaster.ATD : dtNull,
                            ETA = c.COTOMasterID > 0 ? c.OPS_COTOMaster.ETA : dtNull,
                            ETD = c.COTOMasterID > 0 ? c.OPS_COTOMaster.ETD : dtNull,
                            InvoiceDate = c.InvoiceDate,
                            OrderContainerID = c.OPS_Container.ContainerID,
                            TypeOfStatusContainerID = c.TypeOfStatusContainerID,
                            StatusOfCOContainerID = c.StatusOfCOContainerID,
                        }).ToList();

                        item.ListGroupFIN = model.FIN_PLGroupOfProduct.Where(c => c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.OPS_DITOGroupProduct.ORD_GroupProduct.OrderID == item.Order_ID).Select(c => new DTOKPICollection_GroupFIN
                        {
                            Credit = c.FIN_PLDetails.Credit,
                            Debit = c.FIN_PLDetails.Debit,
                        }).ToList();

                        item.ListGroupFIN = model.FIN_PLContainer.Where(c => c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.OPS_COTOContainer.OPS_Container.ORD_Container.OrderID == item.Order_ID).Select(c => new DTOKPICollection_GroupFIN
                        {
                            Credit = c.FIN_PLDetails.Credit,
                            Debit = c.FIN_PLDetails.Debit,
                        }).ToList();
                    }

                    HelperKPI.KPICollection_Order_Generate(model, Account, ListData, ListColumn);
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

        private void KPICollection_Generate_OrderGroupProduct(DataEntities model, int KPIID, DateTime from, DateTime to, AccountItem Account)
        {
            try
            {
                DateTime? dtNull = null;
                List<DTOKPICollection_OrderGroupProduct> ListData = new List<DTOKPICollection_OrderGroupProduct>();
                List<DTOKPICollection_Column> ListColumn = new List<DTOKPICollection_Column>();
                // Lấy danh sách column tính KPI
                ListColumn = model.KPI_Column.Where(c => c.KPIID == KPIID && c.FieldID > 0 && !string.IsNullOrEmpty(c.Code)).Select(c => new DTOKPICollection_Column
                {
                    ID = c.ID,
                    Code = c.Code,
                    KPIColumnTypeID = c.KPIColumnTypeID,
                    ExprData = c.ExprData,
                    FieldType = c.KPI_Field.FieldType,
                    KPITypeID = c.KPI_Field.KPITypeID,
                }).ToList();

                var columnDetail = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDateData && c.FieldType == 6);
                if (columnDetail != null && columnDetail.Code.Split('_').Length > 1)
                {
                    string qField = columnDetail.Code.Split('_')[1];
                    var query = model.ORD_GroupProduct.Where(c => c.ORD_Order.SYSCustomerID == Account.SYSCustomerID && c.ORD_Order.StatusOfOrderID >= -(int)SYSVarType.StatusOfOrderPlaning).Select(c => new DTOKPICollection_OrderGroupProduct
                    {
                        OrderGroupProduct_ID = c.ID,
                        OrderGroupProduct_OrderID = c.OrderID,
                        OrderGroupProduct_CustomerID = c.ORD_Order.CustomerID,
                        OrderGroupProduct_DateConfig = c.DateConfig,
                        OrderGroupProduct_DateRequest = c.ORD_Order.RequestDate,
                        OrderGroupProduct_ETARequest = c.ETARequest,
                        OrderGroupProduct_ETA = c.ETA,
                        OrderGroupProduct_ETD = c.ETD,
                        OrderGroupProduct_TonOrder = c.Ton,
                        OrderGroupProduct_CBMOrder = c.CBM,
                        OrderGroupProduct_QuantityOrder = c.Quantity,
                    }).ToDataSourceResult(CreateRequest_ByFieldAndDate(from, to, qField));

                    foreach (DTOKPICollection_OrderGroupProduct item in query.Data)
                    {
                        item.ListGroupFIN = new List<DTOKPICollection_GroupFIN>();
                        ListData.Add(item);

                        item.ListGroup = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID == item.OrderGroupProduct_ID).Select(c => new DTOKPICollection_Group
                        {
                            ID = c.ID,
                            TonOrder = c.ORD_GroupProduct.Ton,
                            CBMOrder = c.ORD_GroupProduct.CBM,
                            QuantityOrder = c.ORD_GroupProduct.Quantity,
                            Ton = c.Ton,
                            CBM = c.CBM,
                            Quantity = c.Quantity,
                            TonTranfer = c.TonTranfer,
                            CBMTranfer = c.CBMTranfer,
                            QuantityTranfer = c.QuantityTranfer,
                            TonReturn = c.TonReturn,
                            CBMReturn = c.CBMReturn,
                            QuantityReturn = c.QuantityReturn,
                            TonBBGN = c.TonBBGN,
                            CBMBBGN = c.CBMBBGN,
                            QuantityBBGN = c.QuantityBBGN,
                            IsCancel = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel,
                            IsReturn = c.ORD_GroupProduct.IsReturn,
                            ATA = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ATA : dtNull,
                            ATD = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ATD : dtNull,
                            ETA = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ETA : dtNull,
                            ETD = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ETD : dtNull,
                            InvoiceDate = c.InvoiceDate,
                            OrderGroupProductID = c.OrderGroupProductID
                        }).ToList();

                        item.ListGroupFIN = model.FIN_PLGroupOfProduct.Where(c => c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.OPS_DITOGroupProduct.OrderGroupProductID == item.OrderGroupProduct_ID).Select(c => new DTOKPICollection_GroupFIN
                        {
                            Credit = c.FIN_PLDetails.Credit,
                            Debit = c.FIN_PLDetails.Debit,
                        }).ToList();
                    }

                    HelperKPI.KPICollection_OrderGroupProduct_Generate(model, Account, ListData, ListColumn);
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

        private void KPICollection_Generate_OrderContainer(DataEntities model, int KPIID, DateTime from, DateTime to, AccountItem Account)
        {
            try
            {
                // Lấy danh sách column tính KPI
                var lstColumn = model.KPI_Column.Where(c => c.KPIID == KPIID && c.FieldID > 0 && !string.IsNullOrEmpty(c.Code)).Select(c => new
                {
                    c.ID,
                    c.Code,
                    c.KPIColumnTypeID,
                    c.ExprData,
                    TableCode = c.KPI_Field.Code,
                    c.KPI_Field.FieldType,
                    c.KPI_Field.KPITypeID
                }).ToList();
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

        private void KPICollection_Generate_TOMaster(DataEntities model, int KPIID, DateTime from, DateTime to, AccountItem Account)
        {
            try
            {
                DateTime? dtNull = null;
                List<DTOKPICollection_TOMaster> ListData = new List<DTOKPICollection_TOMaster>();
                List<DTOKPICollection_Column> ListColumn = new List<DTOKPICollection_Column>();
                // Lấy danh sách column tính KPI
                ListColumn = model.KPI_Column.Where(c => c.KPIID == KPIID && c.FieldID > 0 && !string.IsNullOrEmpty(c.Code)).Select(c => new DTOKPICollection_Column
                {
                    ID = c.ID,
                    Code = c.Code,
                    KPIColumnTypeID = c.KPIColumnTypeID,
                    ExprData = c.ExprData,
                    FieldType = c.KPI_Field.FieldType,
                    KPITypeID = c.KPI_Field.KPITypeID,
                }).ToList();

                var columnDetail = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDateData && c.FieldType == 6);
                if (columnDetail != null && columnDetail.Code.Split('_').Length > 1)
                {
                    string qField = columnDetail.Code.Split('_')[1];
                    var query = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID > 0 && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterPlanning).Select(c => new DTOKPICollection_TOMaster
                    {
                        TOMaster_ID = c.ID,
                        TOMaster_ETD = c.ETD,
                        TOMaster_ETA = c.ETA,
                        TOMaster_ATD = c.ATD,
                        TOMaster_ATA = c.ATA,
                        TOMaster_DateConfig = c.DateConfig,
                        TOMaster_KMStart = c.KMStart.HasValue ? c.KMStart.Value : 0,
                        TOMaster_KMEnd = c.KMEnd.HasValue ? c.KMEnd.Value : 0,
                        TOMaster_KM = c.KM.HasValue ? c.KM.Value : 0,
                        TOMaster_TonAllow = c.VehicleID.HasValue && c.CAT_Vehicle.MaxWeightCal.HasValue ? c.CAT_Vehicle.MaxWeightCal.Value : 0,
                        TOMaster_CBMAllow = c.VehicleID.HasValue && c.CAT_Vehicle.MaxCapacity.HasValue ? c.CAT_Vehicle.MaxCapacity.Value : 0,
                    }).ToDataSourceResult(CreateRequest_ByFieldAndDate(from, to, qField));

                    var queryCO = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.VehicleID > 0 && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterPlanning).Select(c => new DTOKPICollection_TOMaster
                    {
                        TOMaster_ID = c.ID,
                        TOMaster_ETD = c.ETD,
                        TOMaster_ETA = c.ETA,
                        TOMaster_ATD = c.ATD,
                        TOMaster_ATA = c.ATA,
                        TOMaster_DateConfig = c.DateConfig,
                        TOMaster_KMStart = c.KMStart.HasValue ? c.KMStart.Value : 0,
                        TOMaster_KMEnd = c.KMEnd.HasValue ? c.KMEnd.Value : 0,
                        TOMaster_KM = c.KM.HasValue ? c.KM.Value : 0,
                        TOMaster_TonAllow = c.VehicleID.HasValue && c.CAT_Vehicle.MaxWeightCal.HasValue ? c.CAT_Vehicle.MaxWeightCal.Value : 0,
                        TOMaster_CBMAllow = c.VehicleID.HasValue && c.CAT_Vehicle.MaxCapacity.HasValue ? c.CAT_Vehicle.MaxCapacity.Value : 0,
                    }).ToDataSourceResult(CreateRequest_ByFieldAndDate(from, to, qField));

                    foreach (DTOKPICollection_TOMaster item in query.Data)
                    {
                        item.ListGroup = new List<DTOKPICollection_Group>();
                        item.ListContainer = new List<DTOKPICollection_Container>();
                        item.ListGroupFIN = new List<DTOKPICollection_GroupFIN>();
                        ListData.Add(item);

                        item.ListGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == item.TOMaster_ID).Select(c => new DTOKPICollection_Group
                        {
                            ID = c.ID,
                            TonOrder = c.ORD_GroupProduct.Ton,
                            CBMOrder = c.ORD_GroupProduct.CBM,
                            QuantityOrder = c.ORD_GroupProduct.Quantity,
                            Ton = c.Ton,
                            CBM = c.CBM,
                            Quantity = c.Quantity,
                            TonTranfer = c.TonTranfer,
                            CBMTranfer = c.CBMTranfer,
                            QuantityTranfer = c.QuantityTranfer,
                            TonReturn = c.TonReturn,
                            CBMReturn = c.CBMReturn,
                            QuantityReturn = c.QuantityReturn,
                            TonBBGN = c.TonBBGN,
                            CBMBBGN = c.CBMBBGN,
                            QuantityBBGN = c.QuantityBBGN,
                            IsCancel = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel,
                            IsReturn = c.ORD_GroupProduct.IsReturn,
                            ATA = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ATA : dtNull,
                            ATD = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ATD : dtNull,
                            ETA = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ETA : dtNull,
                            ETD = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ETD : dtNull,
                            InvoiceDate = c.InvoiceDate,
                            OrderGroupProductID = c.OrderGroupProductID
                        }).ToList();

                        item.ListGroupFIN = model.FIN_PLGroupOfProduct.Where(c => c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.OPS_DITOGroupProduct.DITOMasterID == item.TOMaster_ID).Select(c => new DTOKPICollection_GroupFIN
                        {
                            Credit = c.FIN_PLDetails.Credit,
                            Debit = c.FIN_PLDetails.Debit,
                        }).ToList();
                    }

                    foreach (DTOKPICollection_TOMaster item in queryCO.Data)
                    {
                        item.ListGroup = new List<DTOKPICollection_Group>();
                        item.ListContainer = new List<DTOKPICollection_Container>();
                        item.ListGroupFIN = new List<DTOKPICollection_GroupFIN>();
                        ListData.Add(item);

                        item.ListContainer = model.OPS_COTOContainer.Where(c => c.COTOMasterID == item.TOMaster_ID).Select(c => new DTOKPICollection_Container
                        {
                            ID = c.ID,
                            TonOrder = c.OPS_Container.ORD_Container.Ton,
                            PackingID = c.OPS_Container.ORD_Container.PackingID,
                            ATA = c.COTOMasterID > 0 ? c.OPS_COTOMaster.ATA : dtNull,
                            ATD = c.COTOMasterID > 0 ? c.OPS_COTOMaster.ATD : dtNull,
                            ETA = c.COTOMasterID > 0 ? c.OPS_COTOMaster.ETA : dtNull,
                            ETD = c.COTOMasterID > 0 ? c.OPS_COTOMaster.ETD : dtNull,
                            InvoiceDate = c.InvoiceDate,
                            OrderContainerID = c.OPS_Container.ContainerID,
                            TypeOfStatusContainerID = c.TypeOfStatusContainerID,
                            StatusOfCOContainerID = c.StatusOfCOContainerID,
                        }).ToList();

                        item.ListGroupFIN = model.FIN_PLContainer.Where(c => c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.OPS_COTOContainer.COTOMasterID == item.TOMaster_ID).Select(c => new DTOKPICollection_GroupFIN
                        {
                            Credit = c.FIN_PLDetails.Credit,
                            Debit = c.FIN_PLDetails.Debit,
                        }).ToList();
                    }

                    HelperKPI.KPICollection_TOMaster_Generate(model, Account, ListData, ListColumn);
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

        private void KPICollection_Generate_TOGroupProduct(DataEntities model, int KPIID, DateTime from, DateTime to, AccountItem Account)
        {
            try
            {
                DateTime? dtNull = null;
                List<DTOKPICollection_TOGroupProduct> ListData = new List<DTOKPICollection_TOGroupProduct>();
                List<DTOKPICollection_Column> ListColumn = new List<DTOKPICollection_Column>();
                // Lấy danh sách column tính KPI
                ListColumn = model.KPI_Column.Where(c => c.KPIID == KPIID && c.FieldID > 0 && !string.IsNullOrEmpty(c.Code)).Select(c => new DTOKPICollection_Column
                {
                    ID = c.ID,
                    Code = c.Code,
                    KPIColumnTypeID = c.KPIColumnTypeID,
                    ExprData = c.ExprData,
                    FieldType = c.KPI_Field.FieldType,
                    KPITypeID = c.KPI_Field.KPITypeID,
                }).ToList();

                var columnDetail = ListColumn.FirstOrDefault(c => c.KPIColumnTypeID == -(int)SYSVarType.KPIColumnTypeDateData && c.FieldType == 6);
                if (columnDetail != null && columnDetail.Code.Split('_').Length > 1)
                {
                    string qField = columnDetail.Code.Split('_')[1];
                    var query = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOKPICollection_TOGroupProduct
                    {
                        TOGroupProduct_ID = c.ID,
                        TOGroupProduct_OrderID = c.ORD_GroupProduct.OrderID,
                        TOGroupProduct_CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                        TOGroupProduct_OrderGroupProductID = c.OrderGroupProductID.Value,
                        TOGroupProduct_DateConfig = c.DateConfig,
                        TOGroupProduct_DateRequest = c.ORD_GroupProduct.ORD_Order.RequestDate,
                        TOGroupProduct_ExternalDate = c.ORD_GroupProduct.ORD_Order.ExternalDate,
                        TOGroupProduct_ETARequest = c.ORD_GroupProduct.ETARequest,
                        TOGroupProduct_ETA = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ETA : dtNull,
                        TOGroupProduct_ETD = c.DITOMasterID > 0 ? c.OPS_DITOMaster.ETD : dtNull,
                        TOGroupProduct_DateFromCome = c.DateFromCome,
                        TOGroupProduct_DateFromLeave = c.DateFromLeave,
                        TOGroupProduct_DateFromLoadStart = c.DateFromLoadStart,
                        TOGroupProduct_DateFromLoadEnd = c.DateFromLoadEnd,
                        TOGroupProduct_DateToCome = c.DateToCome,
                        TOGroupProduct_DateToLeave = c.DateToLeave,
                        TOGroupProduct_DateToLoadStart = c.DateToLoadStart,
                        TOGroupProduct_DateToLoadEnd = c.DateToLoadEnd,
                        TOGroupProduct_InvoiceDate = c.InvoiceDate,
                        TOGroupProduct_InvoiceReturnDate = c.InvoiceReturnDate,
                        TOGroupProduct_IsCancel = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel,
                        TOGroupProduct_IsReturn = c.ORD_GroupProduct.IsReturn,
                        TOGroupProduct_Ton = c.Ton,
                        TOGroupProduct_CBM = c.CBM,
                        TOGroupProduct_Quantity = c.Quantity,
                        TOGroupProduct_TonTranfer = c.TonTranfer,
                        TOGroupProduct_CBMTranfer = c.CBMTranfer,
                        TOGroupProduct_QuantityTranfer = c.QuantityTranfer,
                        TOGroupProduct_TonBBGN = c.TonBBGN,
                        TOGroupProduct_CBMBBGN = c.CBMBBGN,
                        TOGroupProduct_QuantityBBGN = c.QuantityBBGN,
                        TOGroupProduct_TonReturn = c.TonReturn,
                        TOGroupProduct_CBMReturn = c.CBMReturn,
                        TOGroupProduct_QuantityReturn = c.QuantityReturn,
                        TOGroupProduct_TonCancel = c.TonTranfer,
                        TOGroupProduct_CBMCancel = c.CBMTranfer,
                        TOGroupProduct_QuantityCancel = c.QuantityTranfer,
                        TOGroupProduct_TonPlus = c.TonTranfer,
                        TOGroupProduct_CBMPlus = c.CBMTranfer,
                        TOGroupProduct_QuantityPlus = c.QuantityTranfer,
                    }).ToDataSourceResult(CreateRequest_ByFieldAndDate(from, to, qField));

                    foreach (DTOKPICollection_TOGroupProduct item in query.Data)
                    {
                        item.ListGroupFIN = new List<DTOKPICollection_GroupFIN>();
                        ListData.Add(item);

                        if (item.TOGroupProduct_IsReturn == true)
                            item.TOGroupProduct_TonCancel = item.TOGroupProduct_CBMCancel = item.TOGroupProduct_QuantityCancel = 0;

                        if (item.TOGroupProduct_IsCancel == true)
                            item.TOGroupProduct_Ton = item.TOGroupProduct_CBM = item.TOGroupProduct_Quantity = item.TOGroupProduct_TonTranfer = item.TOGroupProduct_CBMTranfer = item.TOGroupProduct_QuantityTranfer = item.TOGroupProduct_TonBBGN = item.TOGroupProduct_CBMBBGN = item.TOGroupProduct_QuantityBBGN = item.TOGroupProduct_TonReturn = item.TOGroupProduct_CBMReturn = item.TOGroupProduct_QuantityReturn = item.TOGroupProduct_TonCancel = item.TOGroupProduct_CBMCancel = item.TOGroupProduct_QuantityCancel = 0;

                        if (item.TOGroupProduct_IsReturn == false && item.TOGroupProduct_IsCancel == false)
                            item.TOGroupProduct_TonReturn = item.TOGroupProduct_CBMReturn = item.TOGroupProduct_QuantityReturn = item.TOGroupProduct_TonCancel = item.TOGroupProduct_CBMCancel = item.TOGroupProduct_QuantityCancel = item.TOGroupProduct_TonPlus = item.TOGroupProduct_CBMPlus = item.TOGroupProduct_QuantityPlus = 0;

                        item.ListGroupFIN = model.FIN_PLGroupOfProduct.Where(c => c.FIN_PLDetails.FIN_PL.FINPLTypeID == -(int)SYSVarType.FINPLTypePL && c.GroupOfProductID == item.TOGroupProduct_ID).Select(c => new DTOKPICollection_GroupFIN
                        {
                            Credit = c.FIN_PLDetails.Credit,
                            Debit = c.FIN_PLDetails.Debit,
                        }).ToList();
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

        private void KPICollection_Generate_TOContainer(DataEntities model, int KPIID, DateTime from, DateTime to, AccountItem Account)
        {
            try
            {
                // Lấy danh sách column tính KPI
                var lstColumn = model.KPI_Column.Where(c => c.KPIID == KPIID && c.FieldID > 0 && !string.IsNullOrEmpty(c.Code)).Select(c => new
                {
                    c.ID,
                    c.Code,
                    c.KPIColumnTypeID,
                    c.ExprData,
                    TableCode = c.KPI_Field.Code,
                    c.KPI_Field.FieldType,
                    c.KPI_Field.KPITypeID
                }).ToList();
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

        public DTOResult KPICollection_GetList(string request, int KPIID, DateTime from, DateTime to)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.KPI_Collection.Where(c => c.DateData >= from && c.DateData <= to && c.KPIID == KPIID).Select(c => new DTOKPICollection()
                    {
                        ID = c.ID,
                        IsKPI = c.IsKPI,
                        Note = c.Note,
                        ReasonName = c.KPI_Reason != null? c.KPI_Reason.ReasonName : string.Empty,
                        CustomerName = c.CUS_Customer != null? c.CUS_Customer.CustomerName : string.Empty,
                        OrderCode = c.ORD_Order != null? c.ORD_Order.Code : string.Empty,
                        DITOMasterCode = c.OPS_DITOMaster != null? c.OPS_DITOMaster.Code : string.Empty,
                        COTOMasterCode = c.OPS_COTOMaster != null? c.OPS_COTOMaster.Code : string.Empty,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOKPICollection>;
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

        #region TypeOfKPI

        public DTOResult KPITypeOfKPI_List(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.KPI_TypeOfKPI.Select(c => new DTOTypeOfKPI
                    {
                        ID = c.ID,
                        Code = c.Code,
                        TypeName = c.TypeName,
                        KPITypeOfKPIName = c.SYS_Var.ValueOfVar,
                        KPITypeOfKPIID = c.KPITypeOfKPIID,
                        Level = c.Level,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOTypeOfKPI>;
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
        public DTOTypeOfKPI KPITypeOfKPI_Get(int id)
        {
            try
            {
                DTOTypeOfKPI result = new DTOTypeOfKPI();
                using (var model = new DataEntities())
                {
                    result = model.KPI_TypeOfKPI.Where(c => c.ID == id).Select(c => new DTOTypeOfKPI
                    {
                        ID = c.ID,
                        Code = c.Code,
                        TypeName = c.TypeName,
                        KPITypeOfKPIName = c.SYS_Var.ValueOfVar,
                        KPITypeOfKPIID = c.KPITypeOfKPIID,
                        Level = c.Level,
                    }).FirstOrDefault();
                }
                if(result == null)
                {
                    result = new DTOTypeOfKPI();
                    result.Code = "";
                    result.TypeName = "";
                    result.KPITypeOfKPIID = 0;
                    result.ID = id;
                    result.Level = 1;
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

        public void KPITypeOfKPI_Save(DTOTypeOfKPI item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.KPI_TypeOfKPI.FirstOrDefault(c => c.ID != item.ID && c.Code == item.Code);
                    if (obj != null)
                        throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng.");
                    obj = model.KPI_TypeOfKPI.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null)
                    {
                        obj = new KPI_TypeOfKPI();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        model.KPI_TypeOfKPI.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.Code = item.Code;
                    obj.TypeName = item.TypeName;
                    obj.KPITypeOfKPIID = item.KPITypeOfKPIID;
                    obj.Level = item.Level;
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

        public void KPITypeOfKPI_Delete(int id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (model.CAT_ContractTermKPIQuantityDate.Count(c => c.TypeOfKPIID == id) > 0 || model.CAT_ContractTermKPITimeDate.Count(c => c.TypeOfKPIID == id) > 0)
                        throw FaultHelper.BusinessFault(null, null, "Lý do đã sử dụng!");

                    var obj = model.KPI_TypeOfKPI.FirstOrDefault(c => c.ID == id);
                    if (obj != null)
                    {
                        model.KPI_TypeOfKPI.Remove(obj);
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
    }
}