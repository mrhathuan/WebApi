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
using System.IO;

namespace Business
{
    public class BLPOD : Base, IBase
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
                    query.Insert(0, new DTOCustomer { ID = -1, Code = "Xe nhà", CustomerName = "Xe nhà" });
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

        #region Container POD

        public DTOResult PODCOInput_List(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    if (listCustomerID == null || listCustomerID.Count == 0)
                        listCustomerID = Account.ListCustomerID.ToList();

                    List<int> listStt = new List<int>(){
                        -(int)SYSVarType.StatusOfCOContainerEXEmpty,
                        -(int)SYSVarType.StatusOfCOContainerEXLaden,
                        -(int)SYSVarType.StatusOfCOContainerIMEmpty,
                        -(int)SYSVarType.StatusOfCOContainerIMLaden,
                        -(int)SYSVarType.StatusOfCOContainerLOEmpty,
                        -(int)SYSVarType.StatusOfCOContainerLOGetEmpty,
                        -(int)SYSVarType.StatusOfCOContainerLOLaden,
                    };
                    var query = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0 && c.OPSContainerID > 0 && c.ParentID == null
                        && c.OPS_COTOMaster.SYSCustomerID == Account.SYSCustomerID && listStt.Contains(c.StatusOfCOContainerID)
                        && c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived
                        && c.OPS_Container.ORD_Container.ORD_Order.RequestDate >= dtFrom && c.OPS_Container.ORD_Container.ORD_Order.RequestDate < dtTO
                        && listCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID)).OrderBy(c => c.COTOMasterID)
                        .Select(c => new DTOPODCOInput
                        {
                            ID = c.ID,
                            MasterID = c.COTOMasterID > 0 ? c.COTOMasterID.Value : -1,
                            MasterCode = c.OPS_COTOMaster.Code,
                            OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                            ETA = c.ETA,
                            ETD = c.ETD,
                            CustomerID = c.OPS_Container.ORD_Container.ORD_Order.CustomerID,
                            CustomerCode = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.Code,
                            CustomerName = c.OPS_Container.ORD_Container.ORD_Order.CUS_Customer.CustomerName,
                            DateFromCome = c.DateFromCome,
                            DateFromLeave = c.DateFromLeave,
                            DateToCome = c.DateToCome,
                            DateToLeave = c.DateToLeave,
                            InvoiceBy = c.InvoiceBy,
                            InvoiceDate = c.InvoiceDate,
                            InvoiceNo = c.InvoiceNo,
                            IsInvoice = c.TypeOfStatusContainerPODID == -(int)SYSVarType.TypeOfStatusContainerPODComplete,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            TypeOfContainer = c.OPS_Container.ORD_Container.CAT_Packing.Code,
                            ServiceOfOrder = c.OPS_Container.ORD_Container.ORD_Order.CAT_ServiceOfOrder.Name,
                            VehicleNo = c.OPS_COTOMaster.CAT_Vehicle.RegNo,
                            RomoocNo = c.OPS_COTOMaster.CAT_Romooc.RegNo,
                            COTOSort = c.COTOSort,
                            KM = 0,
                            ContainerNo = c.OPS_Container.ContainerNo,
                            SealNo1 = c.OPS_Container.SealNo1,
                            SealNo2 = c.OPS_Container.SealNo2,

                            IsComplete = c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete,
                        }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as List<DTOPODCOInput>;

                    foreach (DTOPODCOInput obj in result.Data)
                    {
                        var opsCoto = model.OPS_COTO.FirstOrDefault(c => c.COTOMasterID == obj.MasterID && c.SortOrder == obj.COTOSort);
                        if (opsCoto != null)
                        {
                            obj.KM = opsCoto.KM;
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

        public void PODCOInput_Save(DTOPODCOInput item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    List<int> lstMasterID = new List<int>();
                    List<int> lstOrderID = new List<int>();
                    var obj = model.OPS_COTOContainer.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        if (item.IsInvoice)
                            obj.TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODComplete;
                        else obj.TypeOfStatusContainerPODID = -(int)SYSVarType.TypeOfStatusContainerPODWait;

                        obj.InvoiceNo = item.InvoiceNo;
                        obj.InvoiceDate = item.InvoiceDate;
                        obj.InvoiceBy = item.InvoiceBy;
                        obj.Note1 = item.Note1;
                        obj.Note2 = item.Note2;
                        lstOrderID.Add(obj.OPS_Container.ORD_Container.OrderID);
                        lstMasterID.Add(obj.COTOMasterID.Value);
                    }

                    model.SaveChanges();

                    // Cập nhật status order
                    HelperStatus.ORDOrder_Status(model, Account, lstOrderID);
                    //using (var statusHelper = new StatusHelper())
                    //{
                    //    statusHelper.ORDStatus_Update(lstOrderID);
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

        #endregion

        #region Distribution POD
        public DTOResult PODDIInput_List(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID, List<int> listVendorID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    List<int?> lstVen = new List<int?>();
                    foreach (var venid in listVendorID)
                    {
                        lstVen.Add(venid);
                    }
                    if (listVendorID.Contains(-1))
                    {
                        lstVen.Add(null); lstVen.Add(Account.SYSCustomerID);
                    }

                    var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OrderGroupProductID > 0 && c.ORD_GroupProduct.IsReturn != true && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID &&
                        c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived &&
                        c.ORD_GroupProduct.ORD_Order.RequestDate >= dtFrom && c.ORD_GroupProduct.ORD_Order.RequestDate < dtTO && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel &&
                        listCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && lstVen.Contains(c.OPS_DITOMaster.VendorOfVehicleID)).OrderBy(c => c.DITOMasterID)
                        .Select(c => new DTOPODDIInput
                        {
                            ID = c.ID,
                            CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                            CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                            CustomerName = c.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                            DNCode = c.DNCode == null ? string.Empty : c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode == null ? string.Empty : c.ORD_GroupProduct.SOCode,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            ETARequest = c.ORD_GroupProduct.ETARequest,
                            ETD = c.ORD_GroupProduct.ETD,

                            LocationFromCode = c.ORD_GroupProduct.CUS_Location.Code,
                            LocationToCode = c.ORD_GroupProduct.CUS_Location1.Code,
                            LocationToName = c.ORD_GroupProduct.CUS_Location1.LocationName,
                            LocationToAddress = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                            LocationToProvince = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                            LocationToDistrict = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName,
                            CreatedDate = c.ORD_GroupProduct.CreatedDate,
                            MasterCode = c.OPS_DITOMaster.Code,
                            MasterID = c.DITOMasterID,
                            RegNo = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                            DriverName = c.OPS_DITOMaster.DriverName1,
                            DriverTel = c.OPS_DITOMaster.DriverTel1,
                            DriverCard = c.OPS_DITOMaster.DriverCard1,
                            RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                            IsComplete = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete ? 1 : 0,
                            IsInvoice = c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete,
                            DateFromCome = c.DateFromCome,
                            DateFromLeave = c.DateFromLeave,
                            DateFromLoadEnd = c.DateFromLoadEnd,
                            DateFromLoadStart = c.DateFromLoadStart,
                            DateToCome = c.DateToCome,
                            DateToLeave = c.DateToLeave,
                            DateToLoadEnd = c.DateToLoadEnd,
                            DateToLoadStart = c.DateToLoadStart,
                            EconomicZone = c.ORD_GroupProduct.CUS_Location1.CAT_Location.EconomicZone,
                            ETDMaster = c.OPS_DITOMaster.ETD,
                            Weight = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.MaxWeight : null,
                            OrderID = c.ORD_GroupProduct.OrderID,
                            DITOGroupProductStatusPODID = c.DITOGroupProductStatusPODID,
                            DITOGroupProductStatusPODName = c.SYS_Var1.ValueOfVar,
                            IsOrigin = c.IsOrigin,
                            DITOGroupProductStatusID = c.DITOGroupProductStatusID,
                            InvoiceBy = c.InvoiceBy,
                            InvoiceDate = c.InvoiceDate,
                            InvoiceNote = c.InvoiceNote,
                            Note = c.Note,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            Description = c.ORD_GroupProduct.Description,
                            VendorName = (c.OPS_DITOMaster.VendorOfVehicleID == null || c.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID) ? "Xe nhà" : c.OPS_DITOMaster.CUS_Customer.CustomerName,
                            VendorCode = (c.OPS_DITOMaster.VendorOfVehicleID == null || c.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID) ? "Xe nhà" : c.OPS_DITOMaster.CUS_Customer.Code,
                            GroupOfProductCode = c.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                            GroupOfProductName = c.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                            DatePODContract = null,
                            //StatusHitPOD = string.Empty,
                            StatusOPSMaster = c.OPS_DITOMaster.SYS_Var.ValueOfVar,
                            StatusSOPOD = string.Empty,
                            DNABA = c.OPS_DITOMaster.Code,
                            ChipNo = c.OPS_DITOMaster.Note1,
                            Temperature = c.OPS_DITOMaster.Note2,
                            //30-3
                            Ton = c.Ton,
                            TonBBGN = c.TonBBGN,
                            TonTransfer = c.TonTranfer,
                            CBM = c.CBM,
                            CBMBBGN = c.CBMBBGN,
                            CBMTransfer = c.CBMTranfer,
                            Quantity = c.Quantity,
                            QuantityBBGN = c.QuantityBBGN,
                            QuantityTransfer = c.QuantityTranfer,
                            StatusOrder = c.ORD_GroupProduct.ORD_Order.SYS_Var.ValueOfVar,

                            OrderGroupProductID = c.OrderGroupProductID.Value,
                            HasReturn = false,
                            TotalReturn = 0,
                            TonReturn = c.TonReturn,
                            CBMReturn = c.CBMReturn,
                            QuantityReturn = c.QuantityReturn,
                            TypeOfDITOGroupProductReturnID = c.TypeOfDITOGroupProductReturnID > 0 ? c.TypeOfDITOGroupProductReturnID.Value : -1,
                            TypeOfDITOGroupProductReturnName = c.TypeOfDITOGroupProductReturnID > 0 ? c.OPS_TypeOfDITOGroupProductReturn.TypeName : string.Empty,
                            InvoiceReturnBy = c.InvoiceReturnBy,
                            InvoiceReturnDate = c.InvoiceReturnDate,
                            InvoiceReturnNote = c.InvoiceReturnNote,

                            IsClosed = c.IsClosed,
                            ClosedBy = c.ClosedBy,
                            ClosedDate = c.ClosedDate,
                            HasUpload = c.HasUpload,

                        }).ToDataSourceResult(CreateRequest(request));

                    var lst = query.Data.Cast<DTOPODDIInput>().ToList();

                    var lstOrderGroupProductID = lst.Select(c => c.OrderGroupProductID).Distinct().ToList();
                    var lstSOCodeCheck = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel &&
                        lstOrderGroupProductID.Contains(c.OrderGroupProductID.Value)).Select(c => new
                    {
                        c.ORD_GroupProduct.SOCode,
                        c.DITOGroupProductStatusPODID
                    }).ToList();
                    var lstSOCode = lst.Where(c => !string.IsNullOrEmpty(c.SOCode)).Select(c => c.SOCode).Distinct().ToList();
                    Dictionary<string, string> dicSOCode = new Dictionary<string, string>();
                    foreach (var socode in lstSOCode)
                    {
                        if (lstSOCodeCheck.Where(c => c.SOCode == socode && c.DITOGroupProductStatusPODID != -(int)SYSVarType.DITOGroupProductStatusPODComplete).Count() == 0)
                            dicSOCode.Add(socode, "Đã nhận");
                        else if (lstSOCodeCheck.Where(c => c.SOCode == socode && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete).Count() > 0)
                            dicSOCode.Add(socode, "Nhận một phần");
                        else
                            dicSOCode.Add(socode, "Chưa nhận");
                    }

                    var lstOrderGroupID = lst.Select(c => c.ID).ToList();
                    var lstKPI = model.KPI_KPITime.Where(c => c.DITOGroupProductID > 0 && lstOrderGroupID.Contains(c.DITOGroupProductID.Value)).Select(c => new
                    {
                        c.DITOGroupProductID,
                        c.KPIID,
                        c.KPIDate,
                        c.IsKPI
                    }).ToList();
                    var lstReturn = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OrderGroupProductID > 0 && c.ORD_GroupProduct.IsReturn == true && c.ORD_GroupProduct.ReturnID > 0 && lstOrderGroupProductID.Contains(c.ORD_GroupProduct.ReturnID.Value))
                        .Select(c => new
                        {
                            DITOMasterID = c.DITOMasterID.Value,
                            ReturnID = c.ORD_GroupProduct.ReturnID.Value,
                            OrderGroupProductID = c.OrderGroupProductID.Value,
                            c.Ton,
                            c.CBM,
                            c.Quantity
                        }).ToList();

                    foreach (var item in lst)
                    {
                        if (dicSOCode.ContainsKey(item.SOCode))
                            item.StatusSOPOD = dicSOCode[item.SOCode];
                        //else
                        //    item.StatusSOPOD = str;

                        var kpi = lstKPI.FirstOrDefault(c => c.DITOGroupProductID == item.ID && c.KPIID == (int)KPICode.OPS);
                        if (kpi != null)
                        {
                            item.KPIOPSDate = kpi.KPIDate;
                            item.IsKPIOPS = kpi.IsKPI;
                        }
                        kpi = lstKPI.FirstOrDefault(c => c.DITOGroupProductID == item.ID && c.KPIID == (int)KPICode.POD);
                        if (kpi != null)
                        {
                            item.KPIPODDate = kpi.KPIDate;
                            item.IsKPIPOD = kpi.IsKPI;
                        }

                        var queryReturn = lstReturn.Where(c => c.DITOMasterID == item.MasterID && c.ReturnID == item.OrderGroupProductID);
                        item.HasReturn = queryReturn.Count() > 0;
                        if (item.HasReturn)
                            item.TotalReturn = queryReturn.Sum(c => c.Quantity);
                    }

                    result.Data = lst.ToArray();
                    result.Total = query.Total;


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

        public void PODDIInput_Save(DTOPODDIInput item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    List<int> lstMasterID = new List<int>();
                    List<int> lstOrderID = new List<int>();
                    var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        if (item.IsInvoice)
                            obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;
                        else obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODWait;
                        obj.Note1 = item.Note1;
                        obj.Note2 = item.Note2;
                        obj.Note = item.Note;
                        obj.InvoiceNote = item.InvoiceNote;
                        obj.InvoiceDate = item.InvoiceDate;
                        obj.InvoiceBy = item.InvoiceBy;
                        obj.DateDN = item.DateDN;
                        obj.DNCode = item.DNCode;
                        //cap nhat ton cbm quantity (3/5/2016)
                        obj.TonBBGN = item.TonBBGN;
                        obj.CBMBBGN = item.CBMBBGN;
                        obj.TonTranfer = item.TonTransfer;
                        obj.CBMTranfer = item.CBMTransfer;
                        obj.QuantityBBGN = item.QuantityBBGN;
                        obj.QuantityTranfer = item.QuantityTransfer;

                        //cap nhat ton cbm quan re turn
                        obj.TonReturn = item.TonReturn;
                        obj.CBMReturn = item.CBMReturn;
                        obj.QuantityReturn = item.QuantityReturn;

                        obj.InvoiceReturnBy = item.InvoiceReturnBy;
                        obj.InvoiceReturnDate = item.InvoiceReturnDate;
                        obj.InvoiceReturnNote = item.InvoiceReturnNote;

                        if (item.TypeOfDITOGroupProductReturnID > 0)
                            obj.TypeOfDITOGroupProductReturnID = item.TypeOfDITOGroupProductReturnID;
                        else obj.TypeOfDITOGroupProductReturnID = null;

                        lstMasterID.Add(obj.DITOMasterID.Value);
                        lstOrderID.Add(obj.ORD_GroupProduct.OrderID);
                        var orderGroup = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == obj.OrderGroupProductID);
                        if (orderGroup != null)
                        {
                            orderGroup.Description = item.Description;
                            orderGroup.SOCode = item.SOCode;
                        }
                    }
                    model.SaveChanges();

                    //Cap nhat KPI
                    HelperKPI.KPITime_DIPODChange(model, Account, new List<int> { item.ID });

                    // Ktra cập nhật status
                    HelperStatus.OPSDIMaster_Status(model, Account, lstMasterID);
                    //using (var status = new StatusHelper())
                    //{
                    //    status.OPSDIMaster_Update(lstMasterID);
                    //    status.ORDStatus_Update(lstOrderID);
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

        public DTOPODBarCode PODBarcodeGroup_List(string Barcode)
        {
            try
            {
                DTOSYSSetting setting = new DTOSYSSetting { CollectDataKM = 1 };
                string sKey = SYSSettingKey.System.ToString();

                DTOPODBarCode result = new DTOPODBarCode();
                result.Barcode = Barcode;
                result.DNCode = string.Empty;
                result.SOCode = string.Empty;
                result.Note1 = string.Empty;
                result.Note2 = string.Empty;
                result.lstGroup = null;
                using (var model = new DataEntities())
                {
                    if (!string.IsNullOrEmpty(Barcode))
                    {
                        var item = model.SYS_Setting.FirstOrDefault(c => c.Key == sKey && c.SYSCustomerID == Account.SYSCustomerID);
                        if (item != null)
                        {
                            if (!string.IsNullOrEmpty(item.Setting))
                            {
                                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<DTOSYSSetting>>(item.Setting);
                                if (data != null && data.Count > 0)
                                    setting = data.FirstOrDefault();
                            }
                        }

                        if (setting.PODBarcode == null)
                            setting.PODBarcode = new DTOSYSSetting_PODBarcode();

                        result.SOCode = Barcode.Substring(setting.PODBarcode.SOIndex, setting.PODBarcode.SOLength).ToUpper();
                        result.DNCode = Barcode.Substring(setting.PODBarcode.DNIndex, setting.PODBarcode.DNLength);

                        var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete && c.DNCode.ToLower() == result.DNCode.ToLower() && c.ORD_GroupProduct.SOCode.ToLower() == result.SOCode.ToLower()).Select(c => new DTOPODBarCodeGroup
                        {
                            ID = c.ID,
                            DriverName = c.OPS_DITOMaster.DriverName1 == null ? string.Empty : c.OPS_DITOMaster.DriverName1,
                            IsInvoice = c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete,
                            RegNo = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                            Kg = c.TonTranfer * 1000,
                            CBM = c.CBMTranfer,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            InvoiceNote = c.InvoiceNote
                        }).ToList();
                        if (query.Count > 0)
                        {
                            result.lstGroup = new List<DTOPODBarCodeGroup>();
                            result.lstGroup.AddRange(query);
                            result.ErrorString = "DN đã nhận chứng từ";
                        }
                        else
                        {
                            query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered && c.DITOGroupProductStatusPODID != -(int)SYSVarType.DITOGroupProductStatusPODComplete && c.DNCode.ToLower() == result.DNCode.ToLower() && c.ORD_GroupProduct.SOCode.ToLower() == result.SOCode.ToLower()).Select(c => new DTOPODBarCodeGroup
                            {
                                ID = c.ID,
                                DriverName = c.OPS_DITOMaster.DriverName1 == null ? string.Empty : c.OPS_DITOMaster.DriverName1,
                                IsInvoice = c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete,
                                RegNo = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                                Kg = c.TonTranfer * 1000,
                                CBM = c.CBMTranfer,
                                Note1 = c.Note1,
                                Note2 = c.Note2,
                                InvoiceNote = c.InvoiceNote
                            }).ToList();
                            if (query.Count > 0)
                            {
                                result.lstGroup = new List<DTOPODBarCodeGroup>();
                                result.lstGroup.AddRange(query);
                            }
                            else
                                result.ErrorString = "DN không tồn tại";
                        }
                    }
                    else
                        result.ErrorString = "Sai định dạng Barcode";
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

        public void PODBarcodeGroup_Save(DTOPODBarCodeGroup item, bool IsNote)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                        obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;
                        obj.InvoiceBy = Account.UserName;
                        obj.InvoiceDate = DateTime.Now;
                        obj.InvoiceNote = item.InvoiceNote;
                        if (IsNote)
                        {
                            obj.Note1 = item.Note1;
                            obj.Note2 = item.Note2;
                        }
                        if (obj.DITOMasterID.HasValue)
                        {
                            obj.OPS_DITOMaster.ModifiedBy = Account.UserName;
                            obj.OPS_DITOMaster.ModifiedDate = DateTime.Now;
                            obj.OPS_DITOMaster.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterDelivery;
                        }
                    }
                    model.SaveChanges();
                    // Ktra cập nhật lệnh v/c
                    if (obj.DITOMasterID.HasValue)
                    {
                        HelperStatus.OPSDIMaster_Status(model, Account, new List<int> { obj.DITOMasterID.Value });
                        //using (var status = new StatusHelper())
                        //{
                        //    status.OPSDIMaster_Update(new List<int> { obj.DITOMasterID.Value });
                        //}
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

        public DTOResult PODDistributionDN_ExcelExport(string request, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                var result = new DTOResult();
                using (var model = new DataEntities())
                {
                    //var count = model.POD_DIGroupProduct.Where(c => c.POD_DIMaster.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOGroupProduct.ORD_GroupProduct.SOCode != null).Count();

                    var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.DITOGroupProductStatusID >= -(int)SYSVarType.DITOGroupProductStatusComplete && c.ORD_GroupProduct.ORD_Order.RequestDate >= dtFrom && c.ORD_GroupProduct.ORD_Order.RequestDate <= dtTO)
                        .Select(c => new DTOPODDistributionDNExcel
                        {
                            ID = c.ID,
                            VehicleCode = c.OPS_DITOMaster.CAT_Vehicle.RegNo,
                            DateFrom = c.OPS_DITOMaster.ETD.Value,
                            DateTo = c.OPS_DITOMaster.ETA.Value,
                            GroupOfProductName = string.Empty,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            RouteCode = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.EconomicZone : string.Empty,
                            RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                            Ton = c.Ton * 1000,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            Description = c.ORD_GroupProduct.Description,
                            DNCode = c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode != null ? c.ORD_GroupProduct.SOCode : string.Empty,

                            IsOrigin = c.IsOrigin,
                            TOMasterCode = c.OPS_DITOMaster.Code,
                            PartnerCode = c.ORD_GroupProduct.CUS_Partner.PartnerCode,
                            PartnerName = c.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName,
                            Address = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                            ProvinceName = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                            DriverName = c.OPS_DITOMaster.DriverID1 > 0 ? c.OPS_DITOMaster.FLM_Driver.CAT_Driver.LastName + " " + c.OPS_DITOMaster.FLM_Driver.CAT_Driver.LastName : c.OPS_DITOMaster.DriverName1,
                            ETD = c.OPS_DITOMaster.ETD.Value,
                            ETARequest = c.ORD_GroupProduct.ETARequest
                        }).ToDataSourceResult(CreateRequest(request));

                    result.Total = query.Total;
                    result.Data = query.Data as List<DTOPODDistributionDNExcel>;
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

        public List<DTOPODDistributionDNExcel> PODDistributionDN_GetDataCheck()
        {
            try
            {
                var result = new List<DTOPODDistributionDNExcel>();
                using (var model = new DataEntities())
                {
                    //var count = model.POD_DIGroupProduct.Where(c => c.POD_DIMaster.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOGroupProduct.ORD_GroupProduct.SOCode != null).Count();

                    result = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.DITOGroupProductStatusID >= -(int)SYSVarType.DITOGroupProductStatusComplete)
                        .Select(c => new DTOPODDistributionDNExcel
                        {
                            ID = c.ID,
                            VehicleCode = c.OPS_DITOMaster.CAT_Vehicle.RegNo,
                            DateFrom = c.OPS_DITOMaster.ETD.Value,
                            DateTo = c.OPS_DITOMaster.ETA.Value,
                            GroupOfProductName = string.Empty,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            RouteCode = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.EconomicZone : string.Empty,
                            RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                            Ton = c.Ton * 1000,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            Description = c.ORD_GroupProduct.Description,
                            DNCode = c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode != null ? c.ORD_GroupProduct.SOCode : string.Empty,

                            IsOrigin = c.IsOrigin,
                            TOMasterCode = c.OPS_DITOMaster.Code,
                            PartnerCode = c.ORD_GroupProduct.CUS_Partner.PartnerCode,
                            PartnerName = c.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName,
                            Address = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                            ProvinceName = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                            DriverName = c.OPS_DITOMaster.DriverID1 > 0 ? c.OPS_DITOMaster.FLM_Driver.CAT_Driver.LastName + " " + c.OPS_DITOMaster.FLM_Driver.CAT_Driver.LastName : c.OPS_DITOMaster.DriverName1,
                            ETD = c.OPS_DITOMaster.ETD.Value,
                            ETARequest = c.ORD_GroupProduct.ETARequest
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

        public void PODDistributionDN_ExcelSave(List<DTOPODDistributionDNExcel> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (lst.Count > 0)
                    {
                        foreach (var item in lst.Where(c => c.ExcelSuccess))
                        {
                            var objGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                            if (objGroup != null)
                            {
                                objGroup.Note1 = item.Note1;
                                objGroup.Note2 = item.Note2;
                                objGroup.ModifiedBy = Account.UserName;
                                objGroup.ModifiedDate = DateTime.Now;
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

        public List<DTOPODDIDNReportExcel> PODDI_DN_ReportExcel()
        {
            try
            {
                List<DTOPODDIDNReportExcel> result = new List<DTOPODDIDNReportExcel>();
                using (var model = new DataEntities())
                {
                    //var count = model.POD_DIGroupProduct.Where(c => c.POD_DIMaster.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOGroupProduct.ORD_GroupProduct.SOCode != null).Count();

                    result = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.DITOGroupProductStatusID >= -(int)SYSVarType.DITOGroupProductStatusComplete).Select(c => new DTOPODDIDNReportExcel
                    {
                        ID = c.ID,
                        CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                        CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                        CustomerName = c.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                        DNCode = c.DNCode == null ? string.Empty : c.DNCode,
                        SOCode = c.ORD_GroupProduct.SOCode == null ? string.Empty : c.ORD_GroupProduct.SOCode,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                        ETARequest = c.ORD_GroupProduct.ETARequest,
                        ETD = c.ORD_GroupProduct.ETD,
                        TonTransfer = c.TonTranfer,
                        CBMTransfer = c.CBMTranfer,
                        QuantityTransfer = c.QuantityTranfer,
                        LocationFromCode = c.ORD_GroupProduct.LocationFromID.HasValue ? c.ORD_GroupProduct.CUS_Location.Code : string.Empty,
                        LocationToCode = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.Code : string.Empty,
                        LocationToName = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.LocationName : string.Empty,
                        LocationToAddress = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : string.Empty,
                        LocationToProvince = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName : string.Empty,
                        LocationToDistrict = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName : string.Empty,
                        CreatedDate = c.ORD_GroupProduct.ORD_Order.CreatedDate,
                        MasterCode = c.OPS_DITOMaster.Code,
                        MasterID = c.DITOMasterID,
                        RegNo = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                        DriverName = c.OPS_DITOMaster.DriverName1,
                        DriverTel = c.OPS_DITOMaster.DriverTel1,
                        DriverCard = c.OPS_DITOMaster.DriverCard1,
                        RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                        IsComplete = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete ? 1 : 0,
                        DateFromCome = c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadEnd = c.DateToLoadEnd,
                        DateToLoadStart = c.DateToLoadStart,
                        EconomicZone = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.EconomicZone : string.Empty,
                        ETDMaster = c.OPS_DITOMaster.ETD,
                        Weight = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.MaxWeight : null,
                        OrderID = c.ORD_GroupProduct.OrderID,
                        //  IsInvoice = c.IsInvoice,
                        IsOrigin = c.IsOrigin,
                        DITOGroupProductStatusID = c.DITOGroupProductStatusID,
                        Status = c.SYS_Var.ValueOfVar,
                        InvoiceBy = c.InvoiceBy,
                        InvoiceDate = c.InvoiceDate,
                        Note = c.Note,
                        Note1 = c.Note1,
                        Note2 = c.Note2,
                        DistibutorCode = c.ORD_GroupProduct.PartnerID.HasValue ? c.ORD_GroupProduct.CUS_Partner.PartnerCode : string.Empty,
                        DistibutorName = c.ORD_GroupProduct.PartnerID.HasValue ? c.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName : string.Empty,
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
        public DTOResult PODDIInput_CloseList(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    if (listCustomerID == null || listCustomerID.Count == 0)
                        listCustomerID = Account.ListCustomerID.ToList();
                    var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OrderGroupProductID > 0 && c.ORD_GroupProduct.IsReturn != true && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID &&
                        c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered &&
                        c.ORD_GroupProduct.ORD_Order.RequestDate >= dtFrom && c.ORD_GroupProduct.ORD_Order.RequestDate < dtTO && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel &&
                        listCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID)).OrderBy(c => c.DITOMasterID)
                        .Select(c => new DTOPODDIInput
                        {
                            ID = c.ID,
                            CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                            CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                            CustomerName = c.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                            DNCode = c.DNCode == null ? string.Empty : c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode == null ? string.Empty : c.ORD_GroupProduct.SOCode,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            ETARequest = c.ORD_GroupProduct.ETARequest,
                            ETD = c.ORD_GroupProduct.ETD,

                            LocationFromCode = c.ORD_GroupProduct.CUS_Location.Code,
                            LocationToCode = c.ORD_GroupProduct.CUS_Location1.Code,
                            LocationToName = c.ORD_GroupProduct.CUS_Location1.LocationName,
                            LocationToAddress = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                            LocationToProvince = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                            LocationToDistrict = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName,
                            CreatedDate = c.ORD_GroupProduct.CreatedDate,
                            MasterCode = c.OPS_DITOMaster.Code,
                            MasterID = c.DITOMasterID,
                            RegNo = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                            DriverName = c.OPS_DITOMaster.DriverName1,
                            DriverTel = c.OPS_DITOMaster.DriverTel1,
                            DriverCard = c.OPS_DITOMaster.DriverCard1,
                            RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                            IsComplete = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete ? 1 : 0,
                            IsInvoice = c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete,
                            DateFromCome = c.DateFromCome,
                            DateFromLeave = c.DateFromLeave,
                            DateFromLoadEnd = c.DateFromLoadEnd,
                            DateFromLoadStart = c.DateFromLoadStart,
                            DateToCome = c.DateToCome,
                            DateToLeave = c.DateToLeave,
                            DateToLoadEnd = c.DateToLoadEnd,
                            DateToLoadStart = c.DateToLoadStart,
                            EconomicZone = c.ORD_GroupProduct.CUS_Location1.CAT_Location.EconomicZone,
                            ETDMaster = c.OPS_DITOMaster.ETD,
                            Weight = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.MaxWeight : null,
                            OrderID = c.ORD_GroupProduct.OrderID,
                            DITOGroupProductStatusPODID = c.DITOGroupProductStatusPODID,
                            DITOGroupProductStatusPODName = c.SYS_Var1.ValueOfVar,
                            IsOrigin = c.IsOrigin,
                            DITOGroupProductStatusID = c.DITOGroupProductStatusID,
                            InvoiceBy = c.InvoiceBy,
                            InvoiceDate = c.InvoiceDate,
                            InvoiceNote = c.InvoiceNote,
                            Note = c.Note,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            Description = c.ORD_GroupProduct.Description,
                            VendorName = (c.OPS_DITOMaster.VendorOfVehicleID == null || c.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID) ? "Xe nhà" : c.OPS_DITOMaster.CUS_Customer.CustomerName,
                            VendorCode = (c.OPS_DITOMaster.VendorOfVehicleID == null || c.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID) ? "Xe nhà" : c.OPS_DITOMaster.CUS_Customer.Code,
                            GroupOfProductCode = c.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                            GroupOfProductName = c.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                            DatePODContract = null,
                            //StatusHitPOD = string.Empty,
                            StatusOPSMaster = c.OPS_DITOMaster.SYS_Var.ValueOfVar,
                            StatusSOPOD = string.Empty,
                            DNABA = c.OPS_DITOMaster.Code,
                            ChipNo = c.OPS_DITOMaster.Note1,
                            Temperature = c.OPS_DITOMaster.Note2,
                            //30-3
                            Ton = c.Ton,
                            TonBBGN = c.TonBBGN,
                            CBM = c.CBM,
                            CBMBBGN = c.CBMBBGN,
                            Quantity = c.Quantity,
                            QuantityBBGN = c.QuantityBBGN,
                            TonTransfer = c.TonTranfer,
                            CBMTransfer = c.CBMTranfer,
                            QuantityTransfer = c.QuantityTranfer,

                            StatusOrder = c.ORD_GroupProduct.ORD_Order.SYS_Var.ValueOfVar,

                            OrderGroupProductID = c.OrderGroupProductID.Value,
                            HasReturn = false,
                            TotalReturn = 0,
                            TonReturn = c.TonReturn,
                            CBMReturn = c.CBMReturn,
                            QuantityReturn = c.QuantityReturn,
                            TypeOfDITOGroupProductReturnID = c.TypeOfDITOGroupProductReturnID > 0 ? c.TypeOfDITOGroupProductReturnID.Value : -1,
                            TypeOfDITOGroupProductReturnName = c.TypeOfDITOGroupProductReturnID > 0 ? c.OPS_TypeOfDITOGroupProductReturn.TypeName : string.Empty,
                            InvoiceReturnBy = c.InvoiceReturnBy,
                            InvoiceReturnDate = c.InvoiceReturnDate,
                            InvoiceReturnNote = c.InvoiceReturnNote,

                            IsClosed = c.IsClosed,
                            ClosedBy = c.ClosedBy,
                            ClosedDate = c.ClosedDate
                        }).ToDataSourceResult(CreateRequest(request));

                    var lst = query.Data.Cast<DTOPODDIInput>().ToList();

                    var lstOrderGroupProductID = lst.Select(c => c.OrderGroupProductID).Distinct().ToList();
                    var lstSOCodeCheck = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel &&
                        lstOrderGroupProductID.Contains(c.OrderGroupProductID.Value)).Select(c => new
                        {
                            c.ORD_GroupProduct.SOCode,
                            c.DITOGroupProductStatusPODID
                        }).ToList();
                    var lstSOCode = lst.Where(c => !string.IsNullOrEmpty(c.SOCode)).Select(c => c.SOCode).Distinct().ToList();
                    Dictionary<string, string> dicSOCode = new Dictionary<string, string>();
                    foreach (var socode in lstSOCode)
                    {
                        if (lstSOCodeCheck.Where(c => c.SOCode == socode && c.DITOGroupProductStatusPODID != -(int)SYSVarType.DITOGroupProductStatusPODComplete).Count() == 0)
                            dicSOCode.Add(socode, "Đã nhận");
                        else if (lstSOCodeCheck.Where(c => c.SOCode == socode && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete).Count() > 0)
                            dicSOCode.Add(socode, "Nhận một phần");
                        else
                            dicSOCode.Add(socode, "Chưa nhận");
                    }

                    var lstOrderGroupID = lst.Select(c => c.ID).ToList();
                    var lstKPI = model.KPI_KPITime.Where(c => c.DITOGroupProductID > 0 && lstOrderGroupID.Contains(c.DITOGroupProductID.Value)).Select(c => new
                    {
                        c.DITOGroupProductID,
                        c.KPIID,
                        c.KPIDate,
                        c.IsKPI
                    }).ToList();
                    var lstReturn = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OrderGroupProductID > 0 && c.ORD_GroupProduct.IsReturn == true && c.ORD_GroupProduct.ReturnID > 0 && lstOrderGroupProductID.Contains(c.ORD_GroupProduct.ReturnID.Value))
                        .Select(c => new
                        {
                            DITOMasterID = c.DITOMasterID.Value,
                            ReturnID = c.ORD_GroupProduct.ReturnID.Value,
                            OrderGroupProductID = c.OrderGroupProductID.Value,
                            c.Ton,
                            c.CBM,
                            c.Quantity
                        }).ToList();

                    foreach (var item in lst)
                    {
                        if (dicSOCode.ContainsKey(item.SOCode))
                            item.StatusSOPOD = dicSOCode[item.SOCode];
                        //else
                        //    item.StatusSOPOD = str;

                        var kpi = lstKPI.FirstOrDefault(c => c.DITOGroupProductID == item.ID && c.KPIID == (int)KPICode.OPS);
                        if (kpi != null)
                        {
                            item.KPIOPSDate = kpi.KPIDate;
                            item.IsKPIOPS = kpi.IsKPI;
                        }
                        kpi = lstKPI.FirstOrDefault(c => c.DITOGroupProductID == item.ID && c.KPIID == (int)KPICode.POD);
                        if (kpi != null)
                        {
                            item.KPIPODDate = kpi.KPIDate;
                            item.IsKPIPOD = kpi.IsKPI;
                        }

                        var queryReturn = lstReturn.Where(c => c.DITOMasterID == item.MasterID && c.ReturnID == item.OrderGroupProductID);
                        item.HasReturn = queryReturn.Count() > 0;
                        if (item.HasReturn)
                            item.TotalReturn = queryReturn.Sum(c => c.Quantity);
                    }

                    result.Data = lst.ToArray();
                    result.Total = query.Total;


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
        public void PODDIInput_Approved(List<int> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var item in lst)
                    {
                        var obj = model.OPS_DITOGroupProduct.Where(c => c.ID == item).FirstOrDefault();
                        if (obj == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu");
                        if (!obj.IsClosed)
                        {
                            obj.IsClosed = true;
                            obj.ClosedDate = DateTime.Now;
                            obj.CreatedBy = Account.UserName;
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
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
        public void PODDIInput_UnApproved(List<int> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var item in lst)
                    {
                        var obj = model.OPS_DITOGroupProduct.Where(c => c.ID == item).FirstOrDefault();
                        if (obj == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu");
                        if (obj.IsClosed)
                        {
                            obj.IsClosed = false;
                            obj.ClosedDate = null;
                            obj.CreatedBy = string.Empty;
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
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
        public void PODDIInput_UpdateHasUpload(int DITOGroupID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var objGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == DITOGroupID);
                    if (objGroup == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu");
                    var listFile = model.CAT_File.Where(c => c.ReferID == objGroup.ID && c.TypeOfFileID == (int)CATTypeOfFileCode.DIPOD).Select(c => c.ID).ToList();
                    if (listFile.Count > 0)
                        objGroup.HasUpload = true;
                    else objGroup.HasUpload = false;

                    objGroup.ModifiedBy = Account.UserName;
                    objGroup.ModifiedDate = DateTime.Now;
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
        public List<CUSCustomer> PODDIInput_VendorList()
        {
            try
            {
                List<CUSCustomer> result = new List<CUSCustomer>();
                using (var model = new DataEntities())
                {
                    result = model.CUS_Customer.Where(c => !c.IsSystem && (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && c.ID != Account.SYSCustomerID).Select(c => new CUSCustomer
                        {
                            ID = c.ID,
                            Code = c.Code,
                            CustomerName = c.CustomerName
                        }).ToList();
                    CUSCustomer itemFirst = new CUSCustomer { ID = -1, Code = "Xe nhà", CustomerName = "Xe nhà" };
                    result.Insert(0, itemFirst);
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
        // bo sung hang hoa chia lai sản lượng
        public List<DTOCUSProduct> PODDIInput_InsertProduct_ProductByGOPList(int DITOGroupProductID)
        {
            try
            {
                List<DTOCUSProduct> result = new List<DTOCUSProduct>();
                using (var model = new DataEntities())
                {
                    var objOPSGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == DITOGroupProductID);
                    if (objOPSGroup == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy OPS_DITOGroupProduct");
                    int GOPID = objOPSGroup.ORD_GroupProduct.GroupOfProductID > 0 ? objOPSGroup.ORD_GroupProduct.GroupOfProductID.Value : -1;
                    var query = model.CUS_Product.Where(c => c.GroupOfProductID == GOPID).Select(c => new DTOCUSProduct
                        {
                            ID = c.ID,
                            Code = c.Code,
                            ProductName = c.ProductName
                        }).ToList();
                    result.AddRange(query);
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
        public void PODDIInput_InsertProduct_SaveList(List<PODInsertProduct> lst, int DITOGroupProductID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var objOPSGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == DITOGroupProductID);
                    if (objOPSGroup == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu OPS_DITOGroupProduct");
                    //lay id
                    int GOPID = objOPSGroup.ORD_GroupProduct.GroupOfProductID > 0 ? objOPSGroup.ORD_GroupProduct.GroupOfProductID.Value : -1;
                    if (!(GOPID > 0))
                        throw FaultHelper.BusinessFault(null, null, "Nhóm hàng trả về không thể thêm sản phẩm");
                    int masterID = objOPSGroup.DITOMasterID > 0 ? objOPSGroup.DITOMasterID.Value : -1;
                    if (!(masterID > 0))
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy chuyến");
                    var objORDGroup = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == objOPSGroup.OrderGroupProductID);
                    if (objORDGroup == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu ORD_GroupProduct");
                    foreach (var item in lst)
                    {
                        #region ORD_GroupProduct
                        ORD_GroupProduct objORDGroupNew = new ORD_GroupProduct();
                        objORDGroupNew.CreatedDate = DateTime.Now;
                        objORDGroupNew.CreatedBy = Account.UserName;
                        model.ORD_GroupProduct.Add(objORDGroupNew);

                        objORDGroupNew.GroupOfProductID = GOPID;
                        objORDGroupNew.Ton = item.Ton;
                        objORDGroupNew.CBM = item.CBM;
                        objORDGroupNew.Quantity = item.Quantity;

                        //copy du lieu tu group cu
                        objORDGroupNew.OrderID = objORDGroup.OrderID;
                        objORDGroupNew.ContainerID = objORDGroup.ContainerID;
                        objORDGroupNew.Description = objORDGroup.Description;
                        objORDGroupNew.SOCode = objORDGroup.SOCode;

                        objORDGroupNew.PriceOfGOPID = objORDGroup.PriceOfGOPID;
                        objORDGroupNew.PackingID = objORDGroup.PackingID;
                        objORDGroupNew.LocationToID = objORDGroup.LocationToID;
                        objORDGroupNew.ETD = objORDGroup.ETD;
                        objORDGroupNew.LocationFromID = objORDGroup.LocationFromID;
                        objORDGroupNew.ETA = objORDGroup.ETA;
                        objORDGroupNew.Price = objORDGroup.Price;
                        objORDGroupNew.CUSRoutingID = objORDGroup.CUSRoutingID;
                        objORDGroupNew.DNCode = objORDGroup.DNCode;
                        objORDGroupNew.ETARequest = objORDGroup.ETARequest;
                        objORDGroupNew.PartnerID = objORDGroup.PartnerID;
                        objORDGroupNew.IsReturn = objORDGroup.IsReturn;
                        objORDGroupNew.ReturnID = objORDGroup.ReturnID;
                        objORDGroupNew.DateConfig = objORDGroup.DateConfig;
                        objORDGroupNew.TypeOfPaymentORDGroupProductID = objORDGroup.TypeOfPaymentORDGroupProductID;
                        objORDGroupNew.PayCustomerModified = objORDGroup.PayCustomerModified;
                        objORDGroupNew.PayCustomerNote = objORDGroup.PayCustomerNote;
                        objORDGroupNew.PayUserModified = objORDGroup.PayUserModified;
                        objORDGroupNew.PayUserNote = objORDGroup.PayUserNote;
                        objORDGroupNew.HasCashCollect = objORDGroup.HasCashCollect;
                        objORDGroupNew.FINSort = objORDGroup.FINSort;
                        objORDGroupNew.LocationToOldID = objORDGroup.LocationToOldID;
                        objORDGroupNew.FINSort = objORDGroup.FINSort;
                        objORDGroupNew.LocationToOldID = objORDGroup.LocationToOldID;
                        objORDGroupNew.PartnerOldID = objORDGroup.PartnerOldID;
                        objORDGroupNew.ETDStart = objORDGroup.ETDStart;
                        objORDGroupNew.ETAStart = objORDGroup.ETAStart;
                        objORDGroupNew.ETDRequest = objORDGroup.ETDRequest;
                        objORDGroupNew.TempMin = objORDGroup.TempMin;
                        objORDGroupNew.TempMax = objORDGroup.TempMax;
                        objORDGroupNew.Note1 = objORDGroup.Note1;
                        objORDGroupNew.Note2 = objORDGroup.Note2;
                        #endregion

                        #region ORD_Product

                        var chechCUSProduct = model.CUS_Product.FirstOrDefault(c => c.ID == item.ProductID);
                        if (chechCUSProduct == null)
                            throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu CUS_Product ID:" + item.ProductID);

                        ORD_Product objORDProductNew = new ORD_Product();
                        objORDProductNew.CreatedBy = Account.UserName;
                        objORDProductNew.CreatedDate = DateTime.Now;
                        objORDProductNew.ORD_GroupProduct = objORDGroupNew;
                        objORDProductNew.ProductID = item.ProductID;

                        switch (chechCUSProduct.CAT_Packing.TypeOfPackageID)
                        {
                            case -(int)SYSVarType.TypeOfPackingGOPTon: objORDProductNew.Quantity = item.Ton; break;
                            case -(int)SYSVarType.TypeOfPackingGOPCBM: objORDProductNew.Quantity = item.CBM; break;
                            default: objORDProductNew.Quantity = item.Quantity; break;
                        }

                        objORDProductNew.PackingID = chechCUSProduct.PackingID;

                        model.ORD_Product.Add(objORDProductNew);

                        #endregion

                        #region OPS_DITOGroupProduct
                        OPS_DITOGroupProduct objOPSGroupNew = new OPS_DITOGroupProduct();
                        objOPSGroupNew.CreatedBy = Account.UserName;
                        objOPSGroupNew.CreatedDate = DateTime.Now;
                        objOPSGroupNew.DITOMasterID = objOPSGroup.DITOMasterID;
                        objOPSGroupNew.ORD_GroupProduct = objORDGroupNew;
                        objOPSGroupNew.Ton = item.Ton;
                        objOPSGroupNew.TonBBGN = item.Ton;
                        objOPSGroupNew.TonTranfer = item.Ton;
                        objOPSGroupNew.CBM = item.CBM;
                        objOPSGroupNew.CBMBBGN = item.CBM;
                        objOPSGroupNew.CBMTranfer = item.CBM;
                        objOPSGroupNew.Quantity = item.Quantity;
                        objOPSGroupNew.QuantityBBGN = item.Quantity;
                        objOPSGroupNew.QuantityTranfer = item.Quantity;
                        objOPSGroupNew.QuantityLoading = item.Quantity;
                        objOPSGroupNew.IsInput = true;
                        objOPSGroupNew.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                        objOPSGroupNew.IsSplit = false;
                        objOPSGroupNew.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODWait;
                        objOPSGroupNew.TonReturn = 0;
                        objOPSGroupNew.CBMReturn = 0;
                        objOPSGroupNew.QuantityReturn = 0;
                        objOPSGroupNew.IsClosed = false;
                        objOPSGroupNew.HasUpload = false;
                        #endregion

                        #region OPS_DITOProduct
                        OPS_DITOProduct objOPSProductNew = new OPS_DITOProduct();
                        objOPSProductNew.CreatedBy = Account.UserName;
                        objOPSProductNew.CreatedDate = DateTime.Now;

                        objOPSProductNew.OPS_DITOGroupProduct = objOPSGroupNew;
                        objOPSProductNew.ORD_Product = objORDProductNew;

                        switch (chechCUSProduct.CAT_Packing.TypeOfPackageID)
                        {
                            case -(int)SYSVarType.TypeOfPackingGOPTon:
                                objOPSProductNew.Quantity = item.Ton;
                                objOPSProductNew.QuantityTranfer = item.Ton;
                                objOPSProductNew.QuantityBBGN = item.Ton;
                                break;
                            case -(int)SYSVarType.TypeOfPackingGOPCBM:
                                objOPSProductNew.Quantity = item.CBM;
                                objOPSProductNew.QuantityTranfer = item.CBM;
                                objOPSProductNew.QuantityBBGN = item.CBM;
                                break;
                            default:
                                objOPSProductNew.Quantity = item.Quantity;
                                objOPSProductNew.QuantityTranfer = item.Quantity;
                                objOPSProductNew.QuantityBBGN = item.Quantity;
                                break;
                        }
                        objOPSProductNew.QuantityReturn = 0;
                        model.OPS_DITOProduct.Add(objOPSProductNew);
                        #endregion
                    }
                    model.SaveChanges();

                    //cập nhật chuyến

                    HelperFinance.Truck_CompleteSchedule(model, Account, masterID);

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

        #region MON Time

        public DTOResult PODMONTime_List(string request)
        {
            try
            {
                var isAdmin = Account.ListActionCode.Contains(SYSViewCode.ViewAdmin.ToString());
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    using (var helper = new CopyHelper())
                    {
                        var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered &&
                            c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == Account.SYSCustomerID &&
                            Account.ListCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID)).Select(c => new DTOPODTime
                        {
                            ID = c.ID,
                            CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                            CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                            CustomerName = c.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                            DNCode = c.DNCode == null ? string.Empty : c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode == null ? string.Empty : c.ORD_GroupProduct.SOCode,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            ETARequest = c.ORD_GroupProduct.ETARequest,
                            ETD = c.ORD_GroupProduct.ETD,
                            TonTransfer = c.TonTranfer,
                            CBMTransfer = c.CBMTranfer,
                            Quantity = c.ORD_GroupProduct.CAT_Packing.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon ? c.Ton : c.ORD_GroupProduct.CAT_Packing.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM ? c.CBM : c.Quantity,
                            QuantityTransfer = c.ORD_GroupProduct.CAT_Packing.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPTon ? c.TonTranfer : c.ORD_GroupProduct.CAT_Packing.TypeOfPackageID == -(int)SYSVarType.TypeOfPackingGOPCBM ? c.CBMTranfer : c.QuantityTranfer,
                            LocationFromCode = c.ORD_GroupProduct.CUS_Location.Code,
                            LocationToCode = c.ORD_GroupProduct.CUS_Location1.Code,
                            LocationToName = c.ORD_GroupProduct.CUS_Location1.LocationName,
                            LocationToAddress = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                            LocationToProvince = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                            LocationToDistrict = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName,
                            CreatedDate = c.ORD_GroupProduct.CreatedDate,
                            MasterCode = c.OPS_DITOMaster.Code,
                            MasterID = c.DITOMasterID,
                            RegNo = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                            DriverName = c.OPS_DITOMaster.DriverName1,
                            DriverTel = c.OPS_DITOMaster.DriverTel1,
                            DriverCard = c.OPS_DITOMaster.DriverCard1,
                            RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                            IsComplete = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete ? 1 : 0,
                            IsInvoice = c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete,
                            DateFromCome = c.DateFromCome,
                            DateFromLeave = c.DateFromLeave,
                            DateFromLoadEnd = c.DateFromLoadEnd,
                            DateFromLoadStart = c.DateFromLoadStart,
                            DateToCome = c.DateToCome,
                            DateToLeave = c.DateToLeave,
                            DateToLoadEnd = c.DateToLoadEnd,
                            DateToLoadStart = c.DateToLoadStart,
                            EconomicZone = c.ORD_GroupProduct.CUS_Location1.CAT_Location.EconomicZone,
                            ETDMaster = c.OPS_DITOMaster.ETD,
                            Weight = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.MaxWeight : null,
                            OrderID = c.ORD_GroupProduct.OrderID,
                            DITOGroupProductStatusPODID = c.DITOGroupProductStatusPODID,
                            DITOGroupProductStatusPODName = c.SYS_Var1.ValueOfVar,
                            IsOrigin = c.IsOrigin,
                            DITOGroupProductStatusID = c.DITOGroupProductStatusID,
                            InvoiceBy = c.InvoiceBy,
                            InvoiceDate = c.InvoiceDate,
                            InvoiceNote = c.InvoiceNote,
                            Note = c.Note,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            Description = c.ORD_GroupProduct.Description,
                            VendorName = (c.OPS_DITOMaster.VendorOfVehicleID == null || c.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID) ? "Xe nhà" : c.OPS_DITOMaster.CUS_Customer.CustomerName,
                            VendorCode = (c.OPS_DITOMaster.VendorOfVehicleID == null || c.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID) ? "Xe nhà" : c.OPS_DITOMaster.CUS_Customer.Code,
                            GroupOfProductCode = c.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                            GroupOfProductName = c.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                            DatePODContract = null,
                            //StatusHitPOD = string.Empty,
                            StatusSOPOD = string.Empty,
                            DNABA = c.OPS_DITOMaster.Code,
                            ChipNo = c.OPS_DITOMaster.Note1,
                            Temperature = c.OPS_DITOMaster.Note2,
                            //30-3
                            Ton = c.Ton,
                            TonBBGN = c.TonBBGN,
                            TonTranfer = c.TonTranfer,
                            CBM = c.CBM,
                            CBMBBGN = c.CBMBBGN,
                            CBMTranfer = c.CBMTranfer,
                            QuantityBBGN = c.QuantityBBGN,
                            StatusOrder = c.ORD_GroupProduct.ORD_Order.SYS_Var.ValueOfVar,

                            OrderGroupProductID = c.OrderGroupProductID.Value
                        }).ToDataSourceResult(CreateRequest(request));

                        var lst = query.Data.Cast<DTOPODTime>().ToList();

                        var lstOrderGroupProductID = lst.Select(c => c.OrderGroupProductID).Distinct().ToList();
                        var lstSOCodeCheck = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel &&
                            lstOrderGroupProductID.Contains(c.OrderGroupProductID.Value)).Select(c => new
                            {
                                c.ORD_GroupProduct.SOCode,
                                c.DITOGroupProductStatusPODID
                            }).ToList();
                        var lstSOCode = lst.Select(c => c.SOCode).Distinct().ToList();
                        Dictionary<string, string> dicSOCode = new Dictionary<string, string>();
                        foreach (var socode in lstSOCode)
                        {
                            if (lstSOCodeCheck.Where(c => c.SOCode == socode && c.DITOGroupProductStatusPODID != -(int)SYSVarType.DITOGroupProductStatusPODComplete).Count() == 0)
                                dicSOCode.Add(socode, "Đã nhận");
                            else if (lstSOCodeCheck.Where(c => c.SOCode == socode && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete).Count() > 0)
                                dicSOCode.Add(socode, "Nhận một phần");
                            else
                                dicSOCode.Add(socode, "Chưa nhận");
                        }

                        var lstOrderGroupID = lst.Select(c => c.ID).ToList();
                        var lstKPI = model.KPI_KPITime.Where(c => c.DITOGroupProductID > 0 && lstOrderGroupID.Contains(c.DITOGroupProductID.Value)).Select(c => new
                        {
                            c.DITOGroupProductID,
                            c.KPIID,
                            c.KPIDate,
                            c.IsKPI
                        }).ToList();

                        foreach (var item in lst)
                        {
                            if (dicSOCode.ContainsKey(item.SOCode))
                                item.StatusSOPOD = dicSOCode[item.SOCode];
                            else
                                item.StatusSOPOD = "Chưa nhận";

                            var kpi = lstKPI.FirstOrDefault(c => c.DITOGroupProductID == item.ID && c.KPIID == (int)KPICode.OPS);
                            if (kpi != null)
                            {
                                item.KPIOPSDate = kpi.KPIDate;
                                item.IsKPIOPS = kpi.IsKPI;
                            }
                            kpi = lstKPI.FirstOrDefault(c => c.DITOGroupProductID == item.ID && c.KPIID == (int)KPICode.POD);
                            if (kpi != null)
                            {
                                item.KPIPODDate = kpi.KPIDate;
                                item.IsKPIPOD = kpi.IsKPI;
                            }
                        }

                        result.Data = lst.ToArray();
                        result.Total = query.Total;
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

        public void PODMONTime_SaveList(List<DTOPODTime> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    List<int> lstMasterID = new List<int>();
                    foreach (var item in lst)
                    {
                        var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                        if (obj == null)
                            throw FaultHelper.BusinessFault(null, null, "Không tồn tại!");
                        if (obj.DITOMasterID > 0 && !lstMasterID.Contains(obj.DITOMasterID.Value))
                            lstMasterID.Add(obj.DITOMasterID.Value);
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.DateFromCome = item.DateFromCome;
                        obj.DateFromLeave = item.DateFromLeave;
                        obj.DateFromLoadEnd = item.DateFromLoadEnd;
                        obj.DateFromLoadStart = item.DateFromLoadStart;
                        obj.DateToCome = item.DateToCome;
                        obj.DateToLeave = item.DateToLeave;
                        obj.DateToLoadEnd = item.DateToLoadEnd;
                        obj.DateToLoadStart = item.DateToLoadStart;

                        if (obj.DateToLeave.HasValue)
                            obj.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                        else
                            obj.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusWaiting;

                        // Điểm đi
                        var opsLocationFrom = model.OPS_DITOLocation.FirstOrDefault(c => c.DITOMasterID == obj.DITOMasterID && c.LocationID == obj.ORD_GroupProduct.CUS_Location.LocationID);
                        if (opsLocationFrom != null)
                        {
                            opsLocationFrom.DateCome = item.DateFromCome;
                            opsLocationFrom.LoadingStart = item.DateFromLoadStart;
                            opsLocationFrom.LoadingEnd = item.DateFromLoadEnd;
                            opsLocationFrom.DateLeave = item.DateFromLeave;
                            // status
                            opsLocationFrom.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusPlan;
                            if (opsLocationFrom.DateCome.HasValue)
                                opsLocationFrom.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusCome;
                            if (opsLocationFrom.LoadingStart.HasValue)
                                opsLocationFrom.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusLoadStart;
                            if (opsLocationFrom.LoadingEnd.HasValue)
                                opsLocationFrom.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusLoadEnd;
                            if (opsLocationFrom.DateLeave.HasValue)
                                opsLocationFrom.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusLeave;
                        }

                        // Điểm đến
                        var opsLocationTo = model.OPS_DITOLocation.FirstOrDefault(c => c.DITOMasterID == obj.DITOMasterID && c.LocationID == obj.ORD_GroupProduct.CUS_Location1.LocationID);
                        if (opsLocationTo != null)
                        {
                            opsLocationTo.DateCome = item.DateToCome;
                            opsLocationTo.LoadingStart = item.DateToLoadStart;
                            opsLocationTo.LoadingEnd = item.DateToLoadEnd;
                            opsLocationTo.DateLeave = item.DateToLeave;
                            // status
                            opsLocationTo.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusPlan;
                            if (opsLocationTo.DateCome.HasValue)
                                opsLocationTo.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusCome;
                            if (opsLocationTo.LoadingStart.HasValue)
                                opsLocationTo.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusLoadStart;
                            if (opsLocationTo.LoadingEnd.HasValue)
                                opsLocationTo.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusLoadEnd;
                            if (opsLocationTo.DateLeave.HasValue)
                                opsLocationTo.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusLeave;
                        }
                    }
                    model.SaveChanges();

                    if (lstMasterID.Count > 0)
                    {
                        foreach (var masterid in lstMasterID)
                        {
                            HelperFinance.Truck_TimeChange(model, Account, masterid);
                        }

                    }
                    //Cap nhat KPI
                    if (lst != null)
                    {
                        HelperKPI.KPITime_DIPODChange(model, Account, lst.Select(c => c.ID).Distinct().ToList());
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

        #region Công nợ trả về
        public DTOResult PODOPSExtReturn_List(string request, DateTime dFrom, DateTime dTo)
        {
            try
            {
                DTOResult result = new DTOResult();
                dFrom = dFrom.Date;
                dTo = dTo.Date;
                using (var model = new DataEntities())
                {
                    var query = model.OPS_ExtReturn.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.InvoiceDate < dTo && c.InvoiceDate >= dFrom).Select(c => new DTOPODOPSExtReturn
                        {
                            ID = c.ID,
                            CustomerID = c.CustomerID,
                            CustomerCode = c.CUS_Customer.Code,
                            CustomerName = c.CUS_Customer.CustomerName,
                            VehicleID = c.VehicleID > 0 ? c.VehicleID.Value : -1,
                            VehicleNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                            VendorID = c.VendorID > 0 ? c.VendorID.Value : -1,
                            VendorCode = c.VendorID > 0 ? c.CUS_Customer1.Code : string.Empty,
                            VendorName = c.VendorID > 0 ? c.CUS_Customer1.CustomerName : string.Empty,
                            DriverID = c.DriverID > 0 ? c.DriverID.Value : -1,
                            DriverName = c.DriverID > 0 ? c.FLM_Driver.CAT_Driver.LastName + " " + c.FLM_Driver.CAT_Driver.FirstName : string.Empty,
                            InvoiceDate = c.InvoiceDate,
                            InvoiceNo = c.InvoiceNo,
                            ProductID = c.ProductID,
                            ProductCode = c.CUS_Product.Code,
                            Quantity = c.Quantity,
                            IsApproved = c.IsApproved,
                            DITOMasterID = c.DITOMasterID > 0 ? c.DITOMasterID.Value : -1,
                            DITOMasterCode = c.DITOMasterID > 0 ? c.OPS_DITOMaster.Code : string.Empty,
                            ExtReturnStatusName = c.SYS_Var.ValueOfVar
                        }).ToDataSourceResult(CreateRequest(request));

                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOPODOPSExtReturn>;
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
        public DTOPODOPSExtReturn PODOPSExtReturn_Get(int id)
        {
            try
            {
                DTOPODOPSExtReturn result = new DTOPODOPSExtReturn();
                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        var query = model.OPS_ExtReturn.Where(c => c.ID == id).FirstOrDefault();
                        if (query != null)
                        {
                            result.ID = query.ID;
                            result.CustomerID = query.CustomerID;
                            result.CustomerCode = query.CUS_Customer.Code;
                            result.CustomerName = query.CUS_Customer.CustomerName;
                            result.VehicleID = query.VehicleID > 0 ? query.VehicleID.Value : -1;
                            result.VehicleNo = query.VehicleID > 0 ? query.CAT_Vehicle.RegNo : string.Empty;
                            result.VendorID = query.VendorID > 0 ? query.VendorID.Value : -1;
                            result.VendorCode = query.VendorID > 0 ? query.CUS_Customer1.Code : string.Empty;
                            result.VendorName = query.VendorID > 0 ? query.CUS_Customer1.CustomerName : string.Empty;
                            result.DriverID = query.DriverID > 0 ? query.DriverID.Value : -1;
                            result.DriverName = query.DriverID > 0 ? query.FLM_Driver.CAT_Driver.LastName + " " + query.FLM_Driver.CAT_Driver.FirstName : string.Empty;
                            result.InvoiceDate = query.InvoiceDate;
                            result.InvoiceNo = query.InvoiceNo;
                            result.ProductID = query.ProductID;
                            result.GroupProductID = query.CUS_Product.GroupOfProductID;
                            result.ProductCode = query.CUS_Product.Code;
                            result.Quantity = query.Quantity;
                            result.IsApproved = query.IsApproved;
                            result.DITOMasterID = query.DITOMasterID > 0 ? query.DITOMasterID.Value : -1;
                            result.DITOMasterCode = query.DITOMasterID > 0 ? query.OPS_DITOMaster.Code : string.Empty;
                            result.ExtReturnStatusID = query.ExtReturnStatusID;
                            result.Note = query.Note;
                            //result.ListDetail = query.OPS_ExtReturnDetail.Select(c => new DTOPODOPSExtReturnDetail
                            //{
                            //    ID = c.ID,
                            //    OrderID = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.ID,
                            //    OrderCode = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.Code,
                            //    LocationToID = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID.Value : -1,
                            //    LocationTo = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.CUS_Location3.CAT_Location.Location : string.Empty,
                            //    GroupProductID = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.ID,
                            //    GroupProductCode = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                            //    ProductID = c.OPS_DITOProduct.ORD_Product.CUS_Product.ID,
                            //    ProductCode = c.OPS_DITOProduct.ORD_Product.CUS_Product.Code,
                            //    DITOProductID = c.DITOProductID,
                            //    Quantity = c.Quantity
                            //}).ToList();
                        }
                    }
                    else
                    {
                        result.ID = 0;
                        result.CustomerID = 0;
                        result.InvoiceDate = DateTime.Now;
                        result.IsApproved = false;
                        var objCus = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem).FirstOrDefault();
                        if (objCus != null) result.CustomerID = objCus.ID;
                        result.GroupProductID = 0;
                        var objG = model.CUS_GroupOfProduct.Where(c => c.CustomerID == result.CustomerID).FirstOrDefault();
                        result.GroupProductID = objG.ID;
                        result.ProductID = 0;
                        var objP = model.CUS_Product.Where(c => c.GroupOfProductID == result.GroupProductID).FirstOrDefault();
                        if (objP != null) result.ProductID = objP.ID;
                        // result.ListDetail = new List<DTOPODOPSExtReturnDetail>();
                        result.ExtReturnStatusID = -(int)SYSVarType.ExtReturnStatusNormal;
                        result.VehicleID = -1;
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
        public int PODOPSExtReturn_Save(DTOPODOPSExtReturn item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_ExtReturn.Where(c => c.ID == item.ID).FirstOrDefault();
                    if (obj == null)
                    {
                        obj = new OPS_ExtReturn();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        model.OPS_ExtReturn.Add(obj);
                    }
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                    }
                    obj.InvoiceNo = item.InvoiceNo;
                    obj.InvoiceDate = item.InvoiceDate;
                    obj.CustomerID = item.CustomerID;
                    if (item.VehicleID > 0)
                        obj.VehicleID = item.VehicleID;
                    else obj.VehicleID = null;
                    if (item.VendorID > 0)
                        obj.VendorID = item.VendorID;
                    else obj.VendorID = null;
                    if (item.DriverID > 0)
                        obj.DriverID = item.DriverID;
                    else obj.DriverID = null;

                    obj.IsApproved = item.IsApproved;
                    // obj.Quantity = item.Quantity;
                    obj.ProductID = item.ProductID;
                    if (item.DITOMasterID > 0)
                        obj.DITOMasterID = item.DITOMasterID;
                    else obj.DITOMasterID = null;

                    obj.Quantity = item.Quantity;
                    obj.ExtReturnStatusID = item.ExtReturnStatusID;
                    obj.Note = item.Note;

                    //kiem tra, set detail
                    if (item.DITOMasterID > 0 && item.ID < 1)
                    {
                        var list = model.OPS_DITOProduct.Where(d => d.OPS_DITOGroupProduct.DITOMasterID == item.DITOMasterID.Value && d.ORD_Product.ORD_GroupProduct.IsReturn == true &&
                             ((d.QuantityExtReturn >= 0 && d.QuantityExtReturn < d.QuantityTranfer) || (d.QuantityExtReturn == null && d.QuantityTranfer > 0))).ToList();
                        if (list.Count() > 0)
                        {
                            if ((list.Sum(c => c.QuantityTranfer) - list.Where(c => c.QuantityExtReturn > 0).Sum(c => c.QuantityExtReturn.Value) == item.Quantity))
                            {
                                foreach (var detail in list)
                                {
                                    var objDetail = new OPS_ExtReturnDetail();
                                    objDetail.CreatedDate = DateTime.Now;
                                    objDetail.CreatedBy = Account.UserName;
                                    objDetail.DITOProductID = detail.ID;
                                    objDetail.Quantity = detail.QuantityTranfer;//ko ro
                                    obj.OPS_ExtReturnDetail.Add(objDetail);

                                    detail.QuantityExtReturn = detail.QuantityTranfer;
                                }
                            }
                        }
                    }

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
        public List<DTOPODOPSExtReturnExport> PODOPSExtReturn_Data(DateTime dFrom, DateTime dTo)
        {
            try
            {
                List<DTOPODOPSExtReturnExport> result = new List<DTOPODOPSExtReturnExport>();
                dFrom = dFrom.Date;
                dTo = dTo.Date;
                using (var model = new DataEntities())
                {
                    result = model.OPS_ExtReturnDetail.Where(c => c.OPS_ExtReturn.SYSCustomerID == Account.SYSCustomerID && c.OPS_ExtReturn.InvoiceDate < dTo && c.OPS_ExtReturn.InvoiceDate > dFrom).Select(c => new DTOPODOPSExtReturnExport
                    {
                        ID = c.ID,
                        CustomerID = c.OPS_ExtReturn.CustomerID,
                        CustomerCode = c.OPS_ExtReturn.CUS_Customer.Code,
                        CustomerName = c.OPS_ExtReturn.CUS_Customer.CustomerName,
                        VehicleID = c.OPS_ExtReturn.VehicleID > 0 ? c.OPS_ExtReturn.VehicleID.Value : -1,
                        VehicleNo = c.OPS_ExtReturn.VehicleID > 0 ? c.OPS_ExtReturn.CAT_Vehicle.RegNo : string.Empty,
                        VendorID = c.OPS_ExtReturn.VendorID > 0 ? c.OPS_ExtReturn.VendorID.Value : -1,
                        VendorCode = c.OPS_ExtReturn.VendorID > 0 ? c.OPS_ExtReturn.CUS_Customer1.Code : string.Empty,
                        VendorName = c.OPS_ExtReturn.VendorID > 0 ? c.OPS_ExtReturn.CUS_Customer1.CustomerName : string.Empty,
                        DriverID = c.OPS_ExtReturn.DriverID > 0 ? c.OPS_ExtReturn.DriverID.Value : -1,
                        DriverName = c.OPS_ExtReturn.DriverID > 0 ? c.OPS_ExtReturn.FLM_Driver.CAT_Driver.LastName + " " + c.OPS_ExtReturn.FLM_Driver.CAT_Driver.FirstName : string.Empty,
                        InvoiceDate = c.OPS_ExtReturn.InvoiceDate,
                        InvoiceNo = c.OPS_ExtReturn.InvoiceNo,
                        ProductID = c.OPS_ExtReturn.ProductID,
                        ProductCode = c.OPS_ExtReturn.CUS_Product.Code,
                        Quantity = c.OPS_ExtReturn.Quantity,
                        IsApproved = c.OPS_ExtReturn.IsApproved,
                        DITOMasterID = c.OPS_ExtReturn.DITOMasterID > 0 ? c.OPS_ExtReturn.DITOMasterID.Value : -1,
                        DITOMasterCode = c.OPS_ExtReturn.DITOMasterID > 0 ? c.OPS_ExtReturn.OPS_DITOMaster.Code : string.Empty,
                        ExtReturnStatusName = c.OPS_ExtReturn.SYS_Var.ValueOfVar,
                        Note = c.OPS_ExtReturn.Note
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

        public DTOResult PODOPSExtReturn_DetailList(string request, int ExtReturnID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_ExtReturnDetail.Where(c => c.ExtReturnID == ExtReturnID).Select(c => new DTOPODOPSExtReturnDetail
                    {
                        ID = c.ID,
                        OrderID = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.ID,
                        OrderCode = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.Code,
                        LocationToID = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID.Value : -1,
                        LocationTo = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.ORD_Order.CUS_Location3.CAT_Location.Location : string.Empty,
                        GroupProductID = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.ID,
                        GroupProductCode = c.OPS_DITOProduct.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                        ProductID = c.OPS_DITOProduct.ORD_Product.CUS_Product.ID,
                        ProductCode = c.OPS_DITOProduct.ORD_Product.CUS_Product.Code,
                        DITOProductID = c.DITOProductID,
                        Quantity = c.Quantity,
                        KMax = c.Quantity,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOPODOPSExtReturnDetail>;
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

        public void PODOPSExtReturn_Delete(List<int> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var exR in lst)
                    {
                        var obj = model.OPS_ExtReturn.Where(c => c.ID == exR).FirstOrDefault();
                        if (obj != null)
                        {
                            foreach (var item in model.OPS_ExtReturnDetail.Where(c => c.ExtReturnID == obj.ID))
                            {
                                var objProduct = model.OPS_DITOProduct.Where(c => c.ID == item.DITOProductID).FirstOrDefault();
                                if (objProduct != null)
                                {
                                    objProduct.ModifiedBy = Account.UserName;
                                    objProduct.ModifiedDate = DateTime.Now;
                                    objProduct.QuantityExtReturn -= item.Quantity;
                                }
                                model.OPS_ExtReturnDetail.Remove(item);
                            }
                            model.OPS_ExtReturn.Remove(obj);
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
        public void PODOPSExtReturn_Approved(List<int> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var exR in lst)
                    {
                        var obj = model.OPS_ExtReturn.Where(c => c.ID == exR).FirstOrDefault();
                        if (obj != null)
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                            obj.IsApproved = true;
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
        public void PODOPSExtReturn_DetailSave(List<DTOPODOPSExtReturnDetail> lst, int ExtReturnID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst)
                    {
                        var objProduct = model.OPS_DITOProduct.FirstOrDefault(c => c.ID == item.DITOProductID);
                        if (objProduct != null)
                        {
                            var obj = model.OPS_ExtReturnDetail.Where(c => c.ID == item.ID).FirstOrDefault();
                            if (obj == null)
                            {
                                obj = new OPS_ExtReturnDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.ExtReturnID = ExtReturnID;
                                obj.DITOProductID = item.DITOProductID;
                                model.OPS_ExtReturnDetail.Add(obj);
                            }
                            else
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                            }
                            obj.Quantity = item.Quantity;

                            model.SaveChanges();
                            objProduct.QuantityExtReturn = objProduct.OPS_ExtReturnDetail.Sum(d => d.Quantity);
                            model.SaveChanges();
                        }
                    }
                    // model.SaveChanges();
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
        public DTOResult PODOPSExtReturn_CustomerList()
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerCUS || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem).Select(c => new DTOCustomer
                    {
                        ID = c.ID,
                        Code = c.Code,
                        CustomerName = c.CustomerName,
                    }).ToList();

                    result.Total = query.Count;
                    result.Data = query.ToArray();
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
        public DTOResult PODOPSExtReturn_GOPByCus(int customerid)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.CUS_GroupOfProduct.Where(c => c.CustomerID == customerid).Select(c => new DTOCUSGroupOfProduct
                    {
                        ID = c.ID,
                        Code = c.Code,
                    }).ToList();

                    result.Total = query.Count;
                    result.Data = query.ToArray();
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
        public DTOResult PODOPSExtReturn_ProductByGOP(int gopID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.CUS_Product.Where(c => c.GroupOfProductID == gopID).Select(c => new DTOCUSProduct
                    {
                        ID = c.ID,
                        Code = c.Code,
                    }).ToList();

                    result.Total = query.Count;
                    result.Data = query.ToArray();
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
        public List<DTOCATVehicle> PODOPSExtReturn_VehicleList(int vendorID)
        {
            try
            {
                List<DTOCATVehicle> result = new List<DTOCATVehicle>();
                using (var model = new DataEntities())
                {
                    if (vendorID > 0 && vendorID != Account.SYSCustomerID)
                    {
                        result = model.CUS_Vehicle.Where(c => c.CustomerID == vendorID).Select(c => new DTOCATVehicle
                            {
                                ID = c.VehicleID,
                                RegNo = c.CAT_Vehicle.RegNo
                            }).ToList();
                    }
                    else
                    {
                        result = model.FLM_Asset.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.TypeOfAssetID == -(int)SYSVarType.TypeOfAssetTruck || c.TypeOfAssetID == -(int)SYSVarType.TypeOfAssetTractor)).Select(c => new DTOCATVehicle
                            {
                                ID = c.VehicleID.Value,
                                RegNo = c.CAT_Vehicle.RegNo
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
        public List<DTOFLMDriver> PODOPSExtReturn_DriverList()
        {
            try
            {
                List<DTOFLMDriver> result = new List<DTOFLMDriver>();
                using (var model = new DataEntities())
                {
                    result = model.FLM_Driver.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.IsUse && !c.IsAssistant).Select(c => new DTOFLMDriver
                        {
                            ID = c.ID,
                            DriverName = c.CAT_Driver.LastName + " " + c.CAT_Driver.FirstName
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
        public DTOResult PODOPSExtReturn_VendorList()
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem).Select(c => new DTOCustomer
                    {
                        ID = c.ID,
                        Code = c.Code,
                        CustomerName = c.CustomerName,
                    }).ToList();

                    result.Total = query.Count;
                    result.Data = query.ToArray();
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

        public DTOResult PODOPSExtReturn_DetailNotIn(string request, int masterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    if (masterID > 0)
                    {
                        var query = model.OPS_DITOProduct.Where(c => c.OPS_DITOGroupProduct.DITOMasterID == masterID && c.ORD_Product.ORD_GroupProduct.IsReturn == true &&
                           ((c.QuantityExtReturn >= 0 && c.QuantityExtReturn < c.QuantityTranfer) || c.QuantityExtReturn == null)).Select(c => new DTOPODOPSExtReturnDetail
                            {
                                ID = c.ID,
                                OrderID = c.ORD_Product.ORD_GroupProduct.OrderID,
                                OrderCode = c.ORD_Product.ORD_GroupProduct.ORD_Order.Code,
                                LocationToID = c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID.Value : -1,
                                LocationTo = c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.ORD_Product.ORD_GroupProduct.ORD_Order.CUS_Location3.CAT_Location.Location : string.Empty,
                                GroupProductCode = c.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                                GroupProductID = c.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.ID,
                                ProductID = c.ORD_Product.ProductID,
                                ProductCode = c.ORD_Product.CUS_Product.Code,
                                Quantity = c.QuantityExtReturn > 0 ? c.QuantityTranfer - (double)c.QuantityExtReturn : c.QuantityTranfer,
                                KMax = c.QuantityExtReturn > 0 ? c.QuantityTranfer - (double)c.QuantityExtReturn : c.QuantityTranfer,
                                DITOProductID = c.ID,
                            }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOPODOPSExtReturnDetail>;
                    }
                    else
                    {
                        var query = model.OPS_DITOProduct.Where(c => c.ORD_Product.ORD_GroupProduct.IsReturn == true && ((c.QuantityExtReturn >= 0 && c.QuantityExtReturn < c.QuantityTranfer) || c.QuantityExtReturn == null)).Select(c => new DTOPODOPSExtReturnDetail
                        {
                            ID = c.ID,
                            OrderID = c.ORD_Product.ORD_GroupProduct.OrderID,
                            OrderCode = c.ORD_Product.ORD_GroupProduct.ORD_Order.Code,
                            LocationToID = c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID.Value : -1,
                            LocationTo = c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.ORD_Product.ORD_GroupProduct.ORD_Order.CUS_Location3.CAT_Location.Location : string.Empty,
                            GroupProductCode = c.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                            GroupProductID = c.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.ID,
                            ProductID = c.ORD_Product.CUS_Product.ID,
                            ProductCode = c.ORD_Product.CUS_Product.Code,
                            Quantity = c.QuantityExtReturn > 0 ? c.QuantityTranfer - (double)c.QuantityExtReturn : c.QuantityTranfer,
                            KMax = c.QuantityExtReturn > 0 ? c.QuantityTranfer - (double)c.QuantityExtReturn : c.QuantityTranfer,
                            DITOProductID = c.ID,
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOPODOPSExtReturnDetail>;
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

        public List<DTOPODTOMaster> PODOPSExtReturn_DITOMasterList(int cusID, int vendorID, int vehicleID)
        {
            try
            {
                List<DTOPODTOMaster> result = new List<DTOPODTOMaster>();
                using (var model = new DataEntities())
                {
                    result = model.OPS_DITOGroupProduct.Where(c => c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID &&
                        c.ORD_GroupProduct.ORD_Order.CustomerID == cusID).Select(c => new DTOPODTOMaster
                    {
                        TOMasterID = c.OPS_DITOMaster.ID,
                        TOMasterCode = c.OPS_DITOMaster.Code,
                        VehicleID = c.OPS_DITOMaster.VehicleID,
                        VendorID = c.OPS_DITOMaster.VendorOfVehicleID
                    }).Distinct().ToList();
                    if (vendorID > 0)
                        result = result.Where(c => c.VendorID == vendorID).ToList();
                    if (vehicleID > 0)
                        result = result.Where(c => c.VehicleID == vehicleID).ToList();
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

        public DTOResult PODOPSExtReturn_FindList(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_DITOProduct.Where(c => c.ORD_Product.ORD_GroupProduct.IsReturn == true &&
                       ((c.QuantityExtReturn >= 0 && c.QuantityExtReturn < c.QuantityTranfer) || c.QuantityExtReturn == null) &&
                       c.OPS_DITOGroupProduct.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived &&
                       c.OPS_DITOGroupProduct.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOPODOPSDITOProduct
                       {
                           ID = -1,
                           DITOProduct = c.ID,
                           CustomerID = c.ORD_Product.ORD_GroupProduct.ORD_Order.CustomerID,
                           CustomerCode = c.ORD_Product.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                           CustomerName = c.ORD_Product.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                           VendorID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID > 0 ? (c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID ? -1 : c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID.Value) : -1,
                           VendorCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID > 0 ? (c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID ? "Xe nhà" : c.OPS_DITOGroupProduct.OPS_DITOMaster.CAT_GroupOfVehicle.Code) : "Xe nhà",
                           OrderID = c.ORD_Product.ORD_GroupProduct.OrderID,
                           OrderCode = c.ORD_Product.ORD_GroupProduct.ORD_Order.Code,
                           LocationToID = c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID.Value : -1,
                           LocationTo = c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.ORD_Product.ORD_GroupProduct.ORD_Order.CUS_Location3.CAT_Location.Location : string.Empty,
                           GroupProductCode = c.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                           GroupProductID = c.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.ID,
                           ProductID = c.ORD_Product.ProductID,
                           ProductCode = c.ORD_Product.CUS_Product.Code,
                           VehicleID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID.Value : -1,
                           VehicleNo = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                           DITOMasterID = c.OPS_DITOGroupProduct.DITOMasterID,
                           DITOMasterCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.Code,
                           Quantity = c.QuantityExtReturn > 0 ? c.QuantityTranfer - (double)c.QuantityExtReturn : c.QuantityTranfer,
                           ETA = c.OPS_DITOGroupProduct.OPS_DITOMaster.ETA,
                           ETD = c.OPS_DITOGroupProduct.OPS_DITOMaster.ETD,

                           ExtReturnStatusID = -(int)SYSVarType.ExtReturnStatusNormal
                       }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOPODOPSDITOProduct>;
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
        public DTOResult PODOPSExtReturn_QuickList(string request)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_DITOProduct.Where(c => c.ORD_Product.ORD_GroupProduct.IsReturn == true &&
                       ((c.QuantityExtReturn >= 0 && c.QuantityExtReturn < c.QuantityTranfer) || c.QuantityExtReturn == null) &&
                       c.OPS_DITOGroupProduct.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived &&
                       c.OPS_DITOGroupProduct.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID).Select(c => new DTOPODOPSDITOProductQuick
                       {
                           ID = c.ID,
                           CustomerID = c.ORD_Product.ORD_GroupProduct.ORD_Order.CustomerID,
                           CustomerCode = c.ORD_Product.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                           CustomerName = c.ORD_Product.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                           VendorID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID > 0 ? (c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID ? -1 : c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID.Value) : -1,
                           VendorCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID > 0 ? (c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID ? "Xe nhà" : c.OPS_DITOGroupProduct.OPS_DITOMaster.CAT_GroupOfVehicle.Code) : "Xe nhà",
                           OrderID = c.ORD_Product.ORD_GroupProduct.OrderID,
                           OrderCode = c.ORD_Product.ORD_GroupProduct.ORD_Order.Code,
                           LocationToID = c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID.Value : -1,
                           LocationTo = c.ORD_Product.ORD_GroupProduct.ORD_Order.LocationToID > 0 ? c.ORD_Product.ORD_GroupProduct.ORD_Order.CUS_Location3.CAT_Location.Location : string.Empty,
                           GroupProductCode = c.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                           GroupProductID = c.ORD_Product.ORD_GroupProduct.CUS_GroupOfProduct.ID,
                           ProductID = c.ORD_Product.ProductID,
                           ProductCode = c.ORD_Product.CUS_Product.Code,
                           VehicleID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID.Value : -1,
                           VehicleNo = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                           DITOMasterID = c.OPS_DITOGroupProduct.DITOMasterID,
                           DITOMasterCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.Code,
                           Quantity = c.QuantityExtReturn > 0 ? c.QuantityTranfer - (double)c.QuantityExtReturn : c.QuantityTranfer,
                           KMax = c.QuantityExtReturn > 0 ? c.QuantityTranfer - (double)c.QuantityExtReturn : c.QuantityTranfer,
                           ETA = c.OPS_DITOGroupProduct.OPS_DITOMaster.ETA,
                           ETD = c.OPS_DITOGroupProduct.OPS_DITOMaster.ETD,

                           ExtReturnStatusID = -(int)SYSVarType.ExtReturnStatusNormal,
                           InvoiceDate = DateTime.Now,
                           InvoiceNo = string.Empty,
                       }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOPODOPSDITOProductQuick>;
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
        public void PODOPSExtReturn_QuickSave(DTOPODOPSDITOProductQuick item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var objProduct = model.OPS_DITOProduct.FirstOrDefault(c => c.ID == item.ID);
                    if (objProduct != null)
                    {
                        OPS_ExtReturn obj = new OPS_ExtReturn();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.SYSCustomerID = Account.SYSCustomerID;
                        obj.InvoiceNo = item.InvoiceNo;
                        obj.InvoiceDate = item.InvoiceDate;
                        obj.CustomerID = item.CustomerID;
                        if (item.VehicleID > 0)
                            obj.VehicleID = item.VehicleID;
                        else obj.VehicleID = null;
                        if (item.VendorID > 0)
                            obj.VendorID = item.VendorID;
                        else obj.VendorID = null;
                        if (item.DriverID > 0)
                            obj.DriverID = item.DriverID;
                        else obj.DriverID = null;

                        obj.IsApproved = item.IsApproved;
                        // obj.Quantity = item.Quantity;
                        obj.ProductID = item.ProductID;
                        if (item.DITOMasterID > 0)
                            obj.DITOMasterID = item.DITOMasterID;
                        else obj.DITOMasterID = null;

                        obj.Quantity = item.Quantity;
                        obj.ExtReturnStatusID = item.ExtReturnStatusID;
                        obj.Note = item.Note;

                        model.OPS_ExtReturn.Add(obj);

                        OPS_ExtReturnDetail objDetail = new OPS_ExtReturnDetail();
                        objDetail.CreatedDate = DateTime.Now;
                        objDetail.CreatedBy = Account.UserName;
                        objDetail.DITOProductID = item.ID;
                        objDetail.Quantity = item.Quantity;
                        objDetail.OPS_ExtReturn = obj;
                        model.OPS_ExtReturnDetail.Add(objDetail);

                        objProduct.QuantityExtReturn = objProduct.QuantityExtReturn == null ? item.Quantity : objProduct.QuantityExtReturn += item.Quantity;

                        model.SaveChanges();
                    }
                    else throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu");
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

        public DTOPODOPSExtReturnData PODOPSExtReturn_QuickData()
        {
            try
            {
                DTOPODOPSExtReturnData result = new DTOPODOPSExtReturnData();
                result.ListVehicle = new List<DTOPODCUSVehicle>();
                result.ListVendor = new List<DTOCustomer>();
                using (var model = new DataEntities())
                {
                    result.ListVendor = model.CUS_Customer.Where(c => (c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerVEN || c.TypeOfCustomerID == -(int)SYSVarType.TypeOfCustomerBOTH) && !c.IsSystem).Select(c => new DTOCustomer
                    {
                        ID = c.ID,
                        Code = c.Code,
                        CustomerName = c.CustomerName,
                    }).ToList();

                    List<int> lst = new List<int>();
                    lst = result.ListVendor.Select(c => c.ID).ToList();

                    result.ListVehicle = model.CUS_Vehicle.Where(c => c.CAT_Vehicle.TypeOfVehicleID == -(int)SYSVarType.TypeOfVehicleTruck && c.CAT_Vehicle.ID > 2).Select(c => new DTOPODCUSVehicle
                        {
                            ID = c.ID,
                            CustomerID = c.CustomerID == Account.SYSCustomerID ? -1 : c.CustomerID,
                            VehicleID = c.VehicleID,
                            RegNo = c.CAT_Vehicle.RegNo
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

        #region POD DI Quick
        public DTOResult PODDIInput_Quick_List(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    if (listCustomerID == null || listCustomerID.Count == 0)
                        listCustomerID = Account.ListCustomerID.ToList();

                    var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OrderGroupProductID > 0 && c.ORD_GroupProduct.IsReturn != true && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID &&
                        c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived &&
                        c.ORD_GroupProduct.ORD_Order.RequestDate >= dtFrom && c.ORD_GroupProduct.ORD_Order.RequestDate < dtTO &&
                        listCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.DITOGroupProductStatusPODID < -(int)SYSVarType.DITOGroupProductStatusPODComplete).OrderBy(c => c.DITOMasterID)
                        .Select(c => new DTOPODDIInputQuick
                        {
                            ID = c.ID,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            TonTranfer = c.TonTranfer,
                            CBMTranfer = c.CBMTranfer,
                            QuantityTranfer = c.QuantityTranfer,
                            MasterCode = c.OPS_DITOMaster.Code,
                            MasterID = c.DITOMasterID,
                            RegNo = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                            IsComplete = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete ? 1 : 0,
                            IsInvoice = c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete,
                            InvoiceBy = c.InvoiceBy,
                            InvoiceNote = c.InvoiceNote,
                            InvoiceDate = DateTime.Now,
                            GroupOfProductCode = c.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                            GroupOfProductName = c.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,

                            ListProductCode = c.ORD_GroupProduct.ORD_Product.Select(d => d.CUS_Product.Code).ToList(),
                            ProductName = string.Empty,
                            ProductCode = string.Empty,
                            TonBBGN = c.TonBBGN,
                            CBMBBGN = c.CBMBBGN,
                            Quantity = c.Quantity,
                            QuantityBBGN = c.QuantityBBGN,
                            TonReturn = c.TonReturn,
                            CBMReturn = c.CBMReturn,
                            QuantityReturn = c.QuantityReturn,
                            TypeOfDITOGroupProductReturnID = c.TypeOfDITOGroupProductReturnID.HasValue ? c.TypeOfDITOGroupProductReturnID.Value : -1,
                            InvoiceReturnBy = c.InvoiceReturnBy,
                            InvoiceReturnNote = c.InvoiceReturnNote,
                            InvoiceReturnDate = DateTime.Now
                        }).ToDataSourceResult(CreateRequest(request));

                    var lst = query.Data.Cast<DTOPODDIInputQuick>().ToList();

                    foreach (var item in lst)
                    {
                        item.ProductCode = string.Join(",", item.ListProductCode);
                        item.InvoiceDateString = DateTime.Now.ToString("dd/MM");
                        item.InvoiceReturnDateString = DateTime.Now.ToString("dd/MM");
                    }

                    result.Data = lst;
                    result.Total = query.Total;


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

        public void PODDIInput_Quick_Save(DTOPODDIInputQuick item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    List<int> lstMasterID = new List<int>();
                    List<int> lstOrderID = new List<int>();
                    var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;
                        obj.InvoiceNote = item.InvoiceNote;
                        obj.InvoiceDate = item.InvoiceDate;
                        obj.InvoiceBy = Account.UserName;
                        //cap nhat ton cbm quantity (3/5/2016)
                        obj.TonBBGN = item.TonBBGN;
                        obj.CBMBBGN = item.CBMBBGN;
                        obj.TonTranfer = item.TonTranfer;
                        obj.CBMTranfer = item.CBMTranfer;
                        obj.QuantityBBGN = item.QuantityBBGN;
                        obj.QuantityTranfer = item.QuantityTranfer;

                        //cap nhat ton cbm quan re turn
                        if (item.QuantityReturn > 0)
                        {
                            obj.TonReturn = item.TonReturn;
                            obj.CBMReturn = item.CBMReturn;
                            obj.QuantityReturn = item.QuantityReturn;

                            obj.InvoiceReturnBy = Account.UserName;
                            obj.InvoiceReturnDate = item.InvoiceReturnDate;
                            obj.InvoiceReturnNote = item.InvoiceReturnNote;

                            if (item.TypeOfDITOGroupProductReturnID > 0)
                                obj.TypeOfDITOGroupProductReturnID = item.TypeOfDITOGroupProductReturnID;
                            else obj.TypeOfDITOGroupProductReturnID = null;
                        }


                        lstMasterID.Add(obj.DITOMasterID.Value);
                        lstOrderID.Add(obj.ORD_GroupProduct.OrderID);
                    }
                    model.SaveChanges();

                    //Cap nhat KPI
                    HelperKPI.KPITime_DIPODChange(model, Account, new List<int> { item.ID });

                    HelperStatus.OPSDIMaster_Status(model, Account, lstMasterID);
                    //// Ktra cập nhật status
                    //using (var status = new StatusHelper())
                    //{
                    //    status.OPSDIMaster_Update(lstMasterID);
                    //    status.ORDStatus_Update(lstOrderID);
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
        public DTOPODDIInputQuickDN PODDIInput_Quick_DNGet(int DITOGroupProductID)
        {
            try
            {
                DTOPODDIInputQuickDN result = new DTOPODDIInputQuickDN();
                using (var model = new DataEntities())
                {
                    var obj = model.OPS_DITOProduct.Where(c => c.DITOGroupProductID == DITOGroupProductID).FirstOrDefault();
                    if (obj != null)
                    {
                        result.ID = obj.ID;
                        result.OrderProductID = obj.OrderProductID;
                        result.DITOGroupProductID = obj.DITOGroupProductID;
                        result.InvoiceDate = DateTime.Now;
                        result.InvoiceDateString = DateTime.Now.ToString("dd/MM");
                        result.InvoiceNote = string.Empty;
                        result.QuantityTranfer = obj.QuantityTranfer;
                        result.KMax = result.QuantityTranfer;
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
        public void PODDIInput_Quick_DNSave(DTOPODDIInputQuickDN item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    double Ton, CBM, Quantity = 0;
                    var objProduct = model.OPS_DITOProduct.Where(c => c.ID == item.ID).FirstOrDefault();
                    if (objProduct == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu");


                    var objGroupProduct = model.OPS_DITOGroupProduct.Where(c => c.ID == item.DITOGroupProductID).FirstOrDefault();
                    if (objGroupProduct == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu Group");

                    if (objProduct.QuantityTranfer == item.QuantityTranfer)
                    {
                        objGroupProduct.InvoiceNote = item.InvoiceNote;
                        objGroupProduct.InvoiceDate = item.InvoiceDate;
                        objGroupProduct.InvoiceBy = Account.UserName;
                        objGroupProduct.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;
                    }
                    else
                    {
                        var exTon = objProduct.ORD_Product.ExchangeTon;
                        var exCBM = objProduct.ORD_Product.ExchangeCBM;
                        var exQuan = objProduct.ORD_Product.ExchangeQuantity;
                        var package = objProduct.ORD_Product.CAT_Packing.TypeOfPackageID;

                        OPS_DITOGroupProduct objGroupProductNew = new OPS_DITOGroupProduct();
                        objGroupProductNew.CreatedBy = Account.UserName;
                        objGroupProductNew.CreatedDate = DateTime.Now;
                        model.OPS_DITOGroupProduct.Add(objGroupProductNew);
                        objGroupProductNew.DITOMasterID = objGroupProduct.DITOMasterID;
                        objGroupProductNew.OrderGroupProductID = objGroupProduct.OrderGroupProductID;
                        objGroupProductNew.LockedBy = objGroupProduct.LockedBy;

                        objGroupProductNew.Ton = item.QuantityTranfer * (double)exTon;
                        objGroupProductNew.CBM = item.QuantityTranfer * (double)exCBM;
                        objGroupProductNew.Quantity = item.QuantityTranfer * (double)exQuan;
                        objGroupProductNew.TonTranfer = item.QuantityTranfer * (double)exTon;
                        objGroupProductNew.CBMTranfer = item.QuantityTranfer * (double)exCBM;
                        objGroupProductNew.QuantityTranfer = item.QuantityTranfer * (double)exQuan;
                        objGroupProductNew.TonBBGN = item.QuantityTranfer * (double)exTon;
                        objGroupProductNew.CBMBBGN = item.QuantityTranfer * (double)exCBM;
                        objGroupProductNew.QuantityBBGN = item.QuantityTranfer * (double)exQuan;
                        objGroupProductNew.QuantityLoading = item.QuantityTranfer * (double)exQuan;

                        objGroupProductNew.Note = objGroupProduct.Note;
                        objGroupProductNew.IsInput = objGroupProduct.IsInput;
                        objGroupProductNew.GroupSort = objGroupProduct.GroupSort;
                        objGroupProductNew.DNCode = objGroupProduct.DNCode;
                        objGroupProductNew.DITOGroupProductStatusID = objGroupProduct.DITOGroupProductStatusID;
                        objGroupProductNew.DateFromCome = objGroupProduct.DateFromCome;
                        objGroupProductNew.DateFromLeave = objGroupProduct.DateFromLeave;
                        objGroupProductNew.DateFromLoadStart = objGroupProduct.DateFromLoadStart;
                        objGroupProductNew.DateFromLoadEnd = objGroupProduct.DateFromLoadEnd;
                        objGroupProductNew.DateToCome = objGroupProduct.DateToCome;
                        objGroupProductNew.DateToLeave = objGroupProduct.DateToLeave;
                        objGroupProductNew.DateToLoadStart = objGroupProduct.DateToLoadStart;
                        objGroupProductNew.DateToLoadEnd = objGroupProduct.DateToLoadEnd;
                        objGroupProductNew.Note1 = objGroupProduct.Note1;
                        objGroupProductNew.Note2 = objGroupProduct.Note2;
                        objGroupProductNew.IsOrigin = objGroupProduct.IsOrigin;

                        objGroupProductNew.InvoiceBy = Account.UserName;
                        objGroupProductNew.InvoiceDate = item.InvoiceDate;
                        objGroupProductNew.InvoiceNote = item.InvoiceNote;

                        objGroupProductNew.DateDN = objGroupProduct.DateDN;
                        objGroupProductNew.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;
                        objGroupProductNew.CUSRoutingID = objGroupProduct.CUSRoutingID;
                        objGroupProductNew.TonReturn = objGroupProduct.TonReturn;
                        objGroupProductNew.CBMReturn = objGroupProduct.CBMReturn;
                        objGroupProductNew.QuantityReturn = objGroupProduct.QuantityReturn;
                        objGroupProductNew.TypeOfDITOGroupProductReturnID = objGroupProduct.TypeOfDITOGroupProductReturnID;
                        objGroupProductNew.DateConfig = objGroupProduct.DateConfig;
                        objGroupProductNew.CATRoutingID = objGroupProduct.CATRoutingID;
                        objGroupProductNew.InvoiceReturnBy = objGroupProduct.InvoiceReturnBy;
                        objGroupProductNew.InvoiceReturnDate = objGroupProduct.InvoiceReturnDate;
                        objGroupProductNew.InvoiceReturnNote = objGroupProduct.InvoiceReturnNote;
                        objGroupProductNew.FINSort = objGroupProduct.FINSort;
                        objGroupProductNew.IsSplit = objGroupProduct.IsSplit;

                        OPS_DITOProduct objProductNew = new OPS_DITOProduct();
                        objProductNew.CreatedBy = Account.UserName;
                        objProductNew.CreatedDate = DateTime.Now;
                        objProductNew.OPS_DITOGroupProduct = objGroupProductNew;
                        objProductNew.OrderProductID = objProduct.OrderProductID;
                        model.OPS_DITOProduct.Add(objProductNew);
                        objProductNew.Quantity = item.QuantityTranfer * (double)exQuan;
                        objProductNew.QuantityTranfer = item.QuantityTranfer * (double)exQuan;
                        objProductNew.QuantityBBGN = item.QuantityTranfer * (double)exQuan;
                        objProductNew.Note = objProduct.Note;
                        objProductNew.QuantityReturn = objProduct.QuantityReturn;
                        objProductNew.QuantityExtReturn = objProduct.QuantityExtReturn;

                        //cap nhat cho item cu
                        objProduct.ModifiedBy = Account.UserName;
                        objProduct.ModifiedDate = DateTime.Now;
                        objProduct.Quantity = objProduct.Quantity - item.QuantityTranfer * (double)exQuan;
                        objProduct.QuantityTranfer = objProduct.QuantityTranfer - item.QuantityTranfer * (double)exQuan;
                        objProduct.QuantityBBGN = objProduct.QuantityBBGN - item.QuantityTranfer * (double)exQuan;

                        objGroupProduct.CreatedBy = Account.UserName;
                        objGroupProduct.CreatedDate = DateTime.Now;
                        objGroupProduct.Ton = objGroupProduct.Ton - item.QuantityTranfer * (double)exTon;
                        objGroupProduct.CBM = objGroupProduct.CBM - item.QuantityTranfer * (double)exCBM;
                        objGroupProduct.Quantity = objGroupProduct.Quantity - item.QuantityTranfer * (double)exQuan;
                        objGroupProduct.TonTranfer = objGroupProduct.TonTranfer - item.QuantityTranfer * (double)exTon;
                        objGroupProduct.CBMTranfer = objGroupProduct.CBMTranfer - item.QuantityTranfer * (double)exCBM;
                        objGroupProduct.QuantityTranfer = objGroupProduct.QuantityTranfer - item.QuantityTranfer * (double)exQuan;
                        objGroupProduct.TonBBGN = objGroupProduct.TonBBGN - item.QuantityTranfer * (double)exTon;
                        objGroupProduct.CBMBBGN = objGroupProduct.CBMBBGN - item.QuantityTranfer * (double)exCBM;
                        objGroupProduct.QuantityBBGN = objGroupProduct.QuantityBBGN - item.QuantityTranfer * (double)exQuan;
                        objGroupProduct.QuantityLoading = objGroupProduct.QuantityLoading - item.QuantityTranfer * (double)exQuan;
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

        #region DTOPODFLMDIInput  (chi phí xe tải nhà)
        public DTOResult PODFLMDIInput_List(string request, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                DTOResult result = new DTOResult();
                dtFrom = dtFrom.Date;
                dtTO = dtTO.Date;
                using (var model = new DataEntities())
                {
                    var query = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) &&
                        c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.ETD >= dtFrom && c.ETD < dtTO).Select(c => new DTOPODFLMDIInput
                        {
                            ID = c.ID,
                            DITOMasterID = c.ID,
                            DITOMasterCode = c.Code,
                            VehicleID = c.VehicleID > 0 ? c.VehicleID.Value : -1,
                            VehicleNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                            ETA = c.ETA,
                            ETD = c.ETD,
                            AETA = c.ATA,
                            AETD = c.ATD,
                            DriverID1 = c.DriverID1 > 0 ? c.DriverID1.Value : -1,
                            TypeOfDriverID1 = c.TypeOfDriverID1 > 0 ? c.TypeOfDriverID1 : -1,
                            DriverID2 = c.DriverID2 > 0 ? c.DriverID2.Value : -1,
                            TypeOfDriverID2 = c.TypeOfDriverID2 > 0 ? c.TypeOfDriverID2 : -1,
                            SortOrder = c.SortOrder,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            TypeOfPaymentDITOMasterID = c.TypeOfPaymentDITOMasterID > 0 ? c.TypeOfPaymentDITOMasterID.Value : -1,
                            IsApproved = c.TypeOfPaymentDITOMasterID == -(int)SYSVarType.TypeOfPaymentDITOMasterApproved,
                            ExIsOverNight = c.ExIsOverNight.HasValue ? c.ExIsOverNight.Value : false,
                            ExIsOverWeight = c.ExIsOverWeight.HasValue ? c.ExIsOverWeight.Value : false,
                            ExTotalDayOut = c.ExTotalDayOut > 0 ? c.ExTotalDayOut.Value : 0,
                            ExTotalJoin = c.ExTotalJoin > 0 ? c.ExTotalJoin.Value : 0,
                            TotalStationCost = c.OPS_DITOStation.Count(d => d.IsMonth == false) > 0 ? c.OPS_DITOStation.Where(d => d.IsMonth == false).Sum(d => d.Price) : 0,
                            TotalTroubleCost = c.CAT_Trouble.Count() > 0 ? c.CAT_Trouble.Sum(d => d.Cost) : 0,
                            KmStart = c.KMStart > 0 ? c.KMStart.Value : 0,
                            KmEnd = c.KMEnd > 0 ? c.KMEnd.Value : 0
                        }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOPODFLMDIInput>;
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

        public List<DTOPODFLMDIInputExcel> PODFLMDIInput_Export(DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                List<DTOPODFLMDIInputExcel> result = new List<DTOPODFLMDIInputExcel>();
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date;
                using (var model = new DataEntities())
                {
                    result = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) &&
                        c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.ETD >= dtFrom && c.ETD < dtTo).Select(c => new DTOPODFLMDIInputExcel
                        {
                            ID = c.ID,
                            DITOMasterID = c.ID,
                            DITOMasterCode = c.Code,
                            VehicleID = c.VehicleID > 0 ? c.VehicleID.Value : -1,
                            VehicleNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                            ETA = c.ETA,
                            ETD = c.ETD,
                            AETA = c.ATA,
                            AETD = c.ATD,
                            Driver1 = c.FLM_Driver != null ? new DTOFLMDriver { ID = c.FLM_Driver.ID, EmployeeCode = c.FLM_Driver.Code, DriverName = c.FLM_Driver.CAT_Driver.FirstName + " " + c.FLM_Driver.CAT_Driver.LastName } : null,
                            Driver2 = c.FLM_Driver1 != null ? new DTOFLMDriver { ID = c.FLM_Driver1.ID, EmployeeCode = c.FLM_Driver1.Code, DriverName = c.FLM_Driver1.CAT_Driver.FirstName + " " + c.FLM_Driver1.CAT_Driver.LastName } : null,
                            Driver3 = c.FLM_Driver2 != null ? new DTOFLMDriver { ID = c.FLM_Driver2.ID, EmployeeCode = c.FLM_Driver2.Code, DriverName = c.FLM_Driver2.CAT_Driver.FirstName + " " + c.FLM_Driver2.CAT_Driver.LastName } : null,
                            Driver4 = c.FLM_Driver3 != null ? new DTOFLMDriver { ID = c.FLM_Driver3.ID, EmployeeCode = c.FLM_Driver3.Code, DriverName = c.FLM_Driver3.CAT_Driver.FirstName + " " + c.FLM_Driver3.CAT_Driver.LastName } : null,
                            Driver5 = c.FLM_Driver4 != null ? new DTOFLMDriver { ID = c.FLM_Driver4.ID, EmployeeCode = c.FLM_Driver4.Code, DriverName = c.FLM_Driver4.CAT_Driver.FirstName + " " + c.FLM_Driver4.CAT_Driver.LastName } : null,
                            SortOrder = c.SortOrder,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            TypeOfPaymentDITOMasterID = c.TypeOfPaymentDITOMasterID > 0 ? c.TypeOfPaymentDITOMasterID.Value : -1,
                            IsApproved = c.TypeOfPaymentDITOMasterID == -(int)SYSVarType.TypeOfPaymentDITOMasterApproved,
                            ExIsOverNight = c.ExIsOverNight.HasValue ? c.ExIsOverNight.Value : false,
                            ExIsOverWeight = c.ExIsOverWeight.HasValue ? c.ExIsOverWeight.Value : false,
                            ExTotalDayOut = c.ExTotalDayOut > 0 ? c.ExTotalDayOut.Value : 0,
                            ExTotalJoin = c.ExTotalJoin > 0 ? c.ExTotalJoin.Value : 0,
                            TotalStationCost = c.OPS_DITOStation.Count(d => d.IsMonth == false) > 0 ? c.OPS_DITOStation.Where(d => d.IsMonth == false).Sum(d => d.Price) : 0,
                            TotalTroubleCost = c.CAT_Trouble.Count() > 0 ? c.CAT_Trouble.Sum(d => d.Cost) : 0,
                            KmStart = c.KMStart > 0 ? c.KMStart.Value : 0,
                            KmEnd = c.KMEnd > 0 ? c.KMEnd.Value : 0,
                            CustomerCode = c.CUS_Customer.Code,
                            IsBidding = c.IsBidding,
                            CountOfStation = c.OPS_DITOStation.Count(),
                            CountOfTrouble = c.CAT_Trouble.Count(),
                            CountOfLocation = c.OPS_DITOLocation.Count(),
                            ListStationCost = c.OPS_DITOStation.Select(d => new DTOPODOPSDITOStation
                            {
                                ID = d.ID,
                                LocationID = d.LocationID,
                                LocationCode = d.CAT_Location.Code,
                                LocationName = d.CAT_Location.Location,
                                LocationAddress = d.CAT_Location.Address,
                                Price = d.Price
                            }).ToList(),
                            ListTroubleCost = c.CAT_Trouble.Select(e => new DTOPODCATTroubleCost
                            {
                                ID = e.ID,
                                GroupOfTroubleID = e.GroupOfTroubleID,
                                GroupOfTroubleCode = e.CAT_GroupOfTrouble.Code,
                                GroupOfTroubleName = e.CAT_GroupOfTrouble.Name,
                                Cost = e.CostOfVendor,
                                Note = e.Description,
                            }).OrderBy(e => e.GroupOfTroubleID).ToList(),
                            ListLocation = c.OPS_DITOLocation.Select(f => new CATLocation
                            {
                                ID = f.ID,
                                Code = f.CAT_Location.Code,
                                GroupOfLocationCode = f.CAT_Location.CAT_GroupOfLocation.Code,
                            }).ToList(),
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

        public void PODFLMDIInput_Import(List<DTOPODFLMDIInputImport> lst)
        {
            try
            {
                int iDriver = -(int)SYSVarType.TypeOfDriverMain;
                int iDriverEx = -(int)SYSVarType.TypeOfDriverEx;
                int iLoader = -(int)SYSVarType.TypeOfDriverLoad;
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (lst != null)
                    {
                        foreach (var item in lst.Where(c => c.ExcelSuccess))
                        {
                            var objMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.Code == item.DITOMasterCode);
                            if (objMaster != null)
                            {
                                objMaster.ModifiedBy = Account.UserName;
                                objMaster.ModifiedDate = DateTime.Now;

                                #region lưu tài xế
                                //tai xe 1
                                if (item.Driver1 != null)
                                {
                                    objMaster.DriverID1 = item.Driver1.ID > 0 ? (int?)item.Driver1.ID : null;
                                    objMaster.TypeOfDriverID1 = iDriver;

                                    var driver1 = model.FLM_Driver.FirstOrDefault(c => c.ID == objMaster.DriverID1);
                                    if (driver1 != null)
                                    {
                                        objMaster.DriverName1 = driver1.CAT_Driver.LastName + " " + driver1.CAT_Driver.FirstName;
                                        objMaster.DriverTel1 = driver1.CAT_Driver.Cellphone;
                                        objMaster.DriverCard1 = driver1.CAT_Driver.CardNumber;
                                    }
                                    else
                                    {
                                        objMaster.DriverName1 = string.Empty;
                                        objMaster.DriverTel1 = string.Empty;
                                        objMaster.DriverCard1 = string.Empty;
                                    }
                                }


                                //tai xe 2
                                if (item.Driver2 != null)
                                {
                                    objMaster.DriverID2 = item.Driver2.ID > 0 ? (int?)item.Driver2.ID : null;
                                    objMaster.TypeOfDriverID2 = iDriverEx;

                                    var driver2 = model.FLM_Driver.FirstOrDefault(c => c.ID == objMaster.DriverID2);
                                    if (driver2 != null)
                                    {
                                        objMaster.DriverName2 = driver2.CAT_Driver.LastName + " " + driver2.CAT_Driver.FirstName;
                                        objMaster.DriverTel2 = driver2.CAT_Driver.Cellphone;
                                        objMaster.DriverCard2 = driver2.CAT_Driver.CardNumber;
                                    }
                                    else
                                    {
                                        objMaster.DriverName2 = string.Empty;
                                        objMaster.DriverTel2 = string.Empty;
                                        objMaster.DriverCard2 = string.Empty;
                                    }
                                }

                                //tai xe 3
                                if (item.Driver3 != null)
                                {
                                    objMaster.DriverID3 = item.Driver3.ID > 0 ? (int?)item.Driver3.ID : null;
                                    objMaster.TypeOfDriverID3 = iLoader;
                                }

                                //tai xe 4
                                if (item.Driver4 != null)
                                {
                                    objMaster.DriverID4 = item.Driver4.ID > 0 ? (int?)item.Driver4.ID : null;
                                    objMaster.TypeOfDriverID4 = iLoader;
                                }

                                //tai xe 5
                                if (item.Driver5 != null)
                                {
                                    objMaster.DriverID5 = item.Driver5.ID > 0 ? (int?)item.Driver5.ID : null;
                                    objMaster.TypeOfDriverID5 = iLoader;
                                }
                                #endregion

                                #region lưu station
                                foreach (var station in item.ListStationCost)
                                {
                                    var objStation = model.OPS_DITOStation.FirstOrDefault(c => c.ID == station.ID);
                                    if (objStation != null)
                                        objStation.Price = station.Price;
                                }
                                #endregion

                                #region lưu trouble
                                foreach (var trouble in item.ListTroubleCost)
                                {
                                    var objTrouble = model.CAT_Trouble.FirstOrDefault(c => c.ID == trouble.ID);
                                    if (objTrouble != null)
                                        objTrouble.Cost = trouble.Cost;
                                    else
                                    {
                                        objTrouble = new CAT_Trouble();
                                        objTrouble.CreatedBy = Account.UserName;
                                        objTrouble.CreatedDate = DateTime.Now;
                                        objTrouble.DITOMasterID = item.DITOMasterID;
                                        objTrouble.GroupOfTroubleID = trouble.GroupOfTroubleID;
                                        objTrouble.Cost = trouble.Cost;
                                        objTrouble.CostOfVendor = trouble.Cost;
                                        objTrouble.TroubleCostStatusID = -(int)SYSVarType.TroubleCostStatusApproved;
                                        objTrouble.Description = trouble.Note;
                                        objTrouble.AttachmentFile = string.Empty;
                                        objTrouble.DITOID = null;
                                        model.CAT_Trouble.Add(objTrouble);
                                    }
                                }
                                #endregion
                            }
                            else
                                throw FaultHelper.BusinessFault(null, null, "Mã chuyến không tồn tại");



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

        public void PODFLMDIInput_Save(DTOPODFLMDIInput item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy chuyến ID" + item.DITOMasterID);
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;

                        obj.Note1 = item.Note1;
                        obj.Note2 = item.Note2;

                        obj.DriverID1 = item.DriverID1 > 0 ? item.DriverID1 : null;
                        obj.TypeOfDriverID1 = item.TypeOfDriverID1 > 0 ? item.TypeOfDriverID1 : null;

                        var driver1 = model.FLM_Driver.FirstOrDefault(c => c.ID == item.DriverID1);
                        if (driver1 != null)
                        {
                            obj.DriverName1 = driver1.CAT_Driver.LastName + " " + driver1.CAT_Driver.FirstName;
                            obj.DriverTel1 = driver1.CAT_Driver.Cellphone;
                            obj.DriverCard1 = driver1.CAT_Driver.CardNumber;
                        }
                        else
                        {
                            obj.DriverName1 = string.Empty;
                            obj.DriverTel1 = string.Empty;
                            obj.DriverCard1 = string.Empty;
                        }

                        obj.DriverID2 = item.DriverID2 > 0 ? item.DriverID2 : null;
                        obj.TypeOfDriverID2 = item.TypeOfDriverID2 > 0 ? item.TypeOfDriverID2 : null;

                        var driver2 = model.FLM_Driver.FirstOrDefault(c => c.ID == item.DriverID2);
                        if (driver2 != null)
                        {
                            obj.DriverName2 = driver2.CAT_Driver.LastName + " " + driver2.CAT_Driver.FirstName;
                            obj.DriverTel2 = driver2.CAT_Driver.Cellphone;
                            obj.DriverCard2 = driver2.CAT_Driver.CardNumber;
                        }
                        else
                        {
                            obj.DriverName2 = string.Empty;
                            obj.DriverTel2 = string.Empty;
                            obj.DriverCard2 = string.Empty;
                        }

                        obj.ExIsOverNight = item.ExIsOverNight;
                        obj.ExIsOverWeight = item.ExIsOverWeight;
                        obj.ExTotalDayOut = item.ExTotalDayOut;
                        obj.ExTotalJoin = item.ExTotalJoin;

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
        public void PODFLMDIInput_Approved(List<int> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var item in lst)
                    {
                        var obj = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item);
                        if (obj != null && obj.TypeOfPaymentDITOMasterID != -(int)SYSVarType.TypeOfPaymentDITOMasterApproved)
                        {
                            obj.ApprovedBy = Account.UserName;
                            obj.ApprovedDate = DateTime.Now;
                            obj.TypeOfPaymentDITOMasterID = -(int)SYSVarType.TypeOfPaymentDITOMasterApproved;
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

        public DTOPODFLMDIInputDriver PODFLMDIInput_GetDrivers(int DITOMasterID)
        {
            try
            {
                DTOPODFLMDIInputDriver result = new DTOPODFLMDIInputDriver();
                using (var model = new DataEntities())
                {
                    var objMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == DITOMasterID);
                    if (objMaster != null)
                    {
                        result.DITOMasterID = objMaster.ID;
                        result.DriverID1 = objMaster.DriverID1 > 0 ? objMaster.DriverID1 : -1;
                        result.DriverID2 = objMaster.DriverID2 > 0 ? objMaster.DriverID2 : -1;
                        result.DriverID3 = objMaster.DriverID3 > 0 ? objMaster.DriverID3 : -1;
                        result.DriverID4 = objMaster.DriverID4 > 0 ? objMaster.DriverID4 : -1;
                        result.DriverID5 = objMaster.DriverID5 > 0 ? objMaster.DriverID5 : -1;

                        result.TypeOfDriverID1 = objMaster.TypeOfDriverID1 > 0 ? objMaster.TypeOfDriverID1 : -1;
                        result.TypeOfDriverID2 = objMaster.TypeOfDriverID2 > 0 ? objMaster.TypeOfDriverID2 : -1;
                        result.TypeOfDriverID3 = objMaster.TypeOfDriverID3 > 0 ? objMaster.TypeOfDriverID3 : -1;
                        result.TypeOfDriverID4 = objMaster.TypeOfDriverID4 > 0 ? objMaster.TypeOfDriverID4 : -1;
                        result.TypeOfDriverID5 = objMaster.TypeOfDriverID5 > 0 ? objMaster.TypeOfDriverID5 : -1;
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
        public void PODFLMDIInput_SaveDrivers(DTOPODFLMDIInputDriver item, int DITOMasterID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == DITOMasterID);
                    if (obj == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy chuyến ID" + DITOMasterID);
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;

                        obj.DriverID1 = item.DriverID1 > 0 ? item.DriverID1 : null;
                        obj.TypeOfDriverID1 = item.TypeOfDriverID1 > 0 ? item.TypeOfDriverID1 : null;

                        var driver1 = model.FLM_Driver.FirstOrDefault(c => c.ID == item.DriverID1);
                        if (driver1 != null)
                        {
                            obj.DriverName1 = driver1.CAT_Driver.LastName + " " + driver1.CAT_Driver.FirstName;
                            obj.DriverTel1 = driver1.CAT_Driver.Cellphone;
                            obj.DriverCard1 = driver1.CAT_Driver.CardNumber;
                        }
                        else
                        {
                            obj.DriverName1 = string.Empty;
                            obj.DriverTel1 = string.Empty;
                            obj.DriverCard1 = string.Empty;
                        }

                        obj.DriverID2 = item.DriverID2 > 0 ? item.DriverID2 : null;
                        obj.TypeOfDriverID2 = item.TypeOfDriverID2 > 0 ? item.TypeOfDriverID2 : null;

                        var driver2 = model.FLM_Driver.FirstOrDefault(c => c.ID == item.DriverID2);
                        if (driver2 != null)
                        {
                            obj.DriverName2 = driver2.CAT_Driver.LastName + " " + driver2.CAT_Driver.FirstName;
                            obj.DriverTel2 = driver2.CAT_Driver.Cellphone;
                            obj.DriverCard2 = driver2.CAT_Driver.CardNumber;
                        }
                        else
                        {
                            obj.DriverName2 = string.Empty;
                            obj.DriverTel2 = string.Empty;
                            obj.DriverCard2 = string.Empty;
                        }

                        obj.DriverID3 = item.DriverID3 > 0 ? item.DriverID3 : null;
                        obj.TypeOfDriverID3 = item.TypeOfDriverID3 > 0 ? item.TypeOfDriverID3 : null;

                        obj.DriverID4 = item.DriverID4 > 0 ? item.DriverID4 : null;
                        obj.DriverID5 = item.DriverID5 > 0 ? item.DriverID5 : null;
                        obj.TypeOfDriverID4 = item.TypeOfDriverID4 > 0 ? item.TypeOfDriverID4 : null;
                        obj.TypeOfDriverID5 = item.TypeOfDriverID5 > 0 ? item.TypeOfDriverID5 : null;

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

        public DTOResult PODFLMDIInput_TroubleCostList(string request, int DITOMasterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.CAT_Trouble.Where(c => c.DITOMasterID > 0 && c.DITOMasterID == DITOMasterID).Select(c => new DTOPODCATTroubleCost
                        {
                            ID = c.ID,
                            GroupOfTroubleID = c.GroupOfTroubleID,
                            GroupOfTroubleCode = c.CAT_GroupOfTrouble.Code,
                            GroupOfTroubleName = c.CAT_GroupOfTrouble.Name,
                            Cost = c.CostOfVendor,
                            Note = c.Description,
                        }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOPODCATTroubleCost>;
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

        public DTOResult PODFLMDIInput_TroubleCostNotIn_List(string request, int DITOMasterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var objMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == DITOMasterID);
                    if (objMaster != null)
                    {
                        var hasList = model.CAT_Trouble.Where(c => c.DITOMasterID > 0 && c.DITOMasterID == DITOMasterID).Select(c => c.GroupOfTroubleID).Distinct().ToList();
                        var query = model.CAT_GroupOfTrouble.Where(c => !hasList.Contains(c.ID)).Select(c => new DTOPODCATTrouble
                        {
                            ID = c.ID,
                            GroupOfTroubleCode = c.Code,
                            GroupOfTroubleName = c.Name,
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOPODCATTrouble>;
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

        public void PODFLMDIInput_TroubleCostNotIn_SaveList(List<int> lst, int DITOMasterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    #region Kiểm tra trùng GroupID
                    foreach (var item in lst)
                    {
                        var obj = model.CAT_Trouble.FirstOrDefault(c => c.GroupOfTroubleID == item && c.DITOMasterID == DITOMasterID);
                        if (obj != null)
                            throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");
                    }

                    foreach (var item in lst)
                    {
                        var obj = new CAT_Trouble();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.DITOMasterID = DITOMasterID;
                        obj.GroupOfTroubleID = item;
                        obj.Cost = 0;
                        obj.CostOfVendor = 0;
                        obj.TroubleCostStatusID = -(int)SYSVarType.TroubleCostStatusApproved;
                        obj.Description = string.Empty;
                        obj.AttachmentFile = string.Empty;
                        obj.DITOID = null;
                        model.CAT_Trouble.Add(obj);
                    }
                    model.SaveChanges();
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

        public void PODFLMDIInput_TroubleCost_DeleteList(List<int> lst)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst)
                    {
                        var obj = model.CAT_Trouble.FirstOrDefault(c => c.ID == item);
                        if (obj != null)
                            model.CAT_Trouble.Remove(obj);
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

        public void PODFLMDIInput_TroubleCostSave(List<DTOPODCATTroubleCost> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst)
                    {
                        var obj = model.CAT_Trouble.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                        {
                            obj.Cost = item.Cost;
                            obj.CostOfVendor = item.Cost;
                            obj.Description = item.Note;
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

        public DTOResult PODFLMDIInput_StationCostList(string request, int DITOMasterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_DITOStation.Where(c => c.DITOMasterID > 0 && c.DITOMasterID == DITOMasterID && c.IsMonth == false).Select(c => new DTOPODOPSDITOStation
                    {
                        ID = c.ID,
                        LocationID = c.LocationID,
                        LocationCode = c.CAT_Location.Code,
                        LocationName = c.CAT_Location.Location,
                        LocationAddress = c.CAT_Location.Address,
                        Price = c.Price
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOPODOPSDITOStation>;
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
        public void PODFLMDIInput_StationCostSave(List<DTOPODOPSDITOStation> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst)
                    {
                        var obj = model.OPS_DITOStation.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                            obj.Price = item.Price;
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

        public List<DTOPODCATTrouble> PODGroupOfTrouble_List()
        {
            try
            {
                List<DTOPODCATTrouble> result = new List<DTOPODCATTrouble>();
                using (var model = new DataEntities())
                {
                    result = model.CAT_GroupOfTrouble.Select(c => new DTOPODCATTrouble
                    {
                        ID = c.ID,
                        GroupOfTroubleID = c.ID,
                        GroupOfTroubleCode = c.Code,
                        GroupOfTroubleName = c.Name,
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
        public DTOResult PODFLMDIInput_DriverList()
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.FLM_Driver.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.IsUse).Select(c => new DTOFLMDriver
                    {
                        ID = c.ID,
                        DriverID = c.DriverID,
                        EmployeeCode = c.Code,
                        DriverName = c.CAT_Driver.LastName + " " + c.CAT_Driver.FirstName,
                        IsAssistant = c.IsAssistant
                    }).ToList();
                    result.Total = query.Count;
                    result.Data = query as IEnumerable<DTOFLMDriver>;
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

        #region DTOPODFLMCOInput  (chi phí xe container nhà)
        public DTOResult PODFLMCOInput_List(string request, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                DTOResult result = new DTOResult();
                dtFrom = dtFrom.Date;
                dtTO = dtTO.Date;
                using (var model = new DataEntities())
                {
                    var query = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) &&
                        c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.ETD >= dtFrom && c.ETD < dtTO).Select(c => new DTOPODFLMCOInput
                        {
                            ID = c.ID,
                            COTOMasterID = c.ID,
                            COTOMasterCode = c.Code,
                            VehicleID = c.VehicleID > 0 ? c.VehicleID.Value : -1,
                            VehicleNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                            ETA = c.ETA,
                            ETD = c.ETD,
                            AETA = c.ATA,
                            AETD = c.ATD,
                            DriverID1 = c.DriverID1 > 0 ? c.DriverID1.Value : -1,
                            TypeOfDriverID1 = c.TypeOfDriverID1 > 0 ? c.TypeOfDriverID1 : -1,
                            DriverID2 = c.DriverID2 > 0 ? c.DriverID2.Value : -1,
                            TypeOfDriverID2 = c.TypeOfDriverID2 > 0 ? c.TypeOfDriverID2 : -1,
                            SortOrder = c.SortOrder,
                            Note1 = string.Empty,
                            Note2 = string.Empty,
                            TypeOfPaymentCOTOMasterID = c.TypeOfPaymentCOTOMasterID > 0 ? c.TypeOfPaymentCOTOMasterID.Value : -1,
                            IsApproved = c.TypeOfPaymentCOTOMasterID == -(int)SYSVarType.TypeOfPaymentCOTOMasterApproved,
                            ExIsOverNight = c.ExIsOverNight.HasValue ? c.ExIsOverNight.Value : false,
                            ExIsOverWeight = c.ExIsOverWeight.HasValue ? c.ExIsOverWeight.Value : false,
                            ExTotalDayOut = c.ExTotalDayOut > 0 ? c.ExTotalDayOut.Value : 0,
                            ExTotalJoin = c.ExTotalJoin > 0 ? c.ExTotalJoin.Value : 0,
                            TotalStationCost = c.OPS_COTOStation.Count(d => d.IsMonth == false) > 0 ? c.OPS_COTOStation.Where(d => d.IsMonth == false).Sum(d => d.Price) : 0,
                            TotalTroubleCost = c.CAT_Trouble.Count() > 0 ? c.CAT_Trouble.Sum(d => d.Cost) : 0,
                            KmStart = c.KMStart > 0 ? c.KMStart.Value : 0,
                            KmEnd = c.KMEnd > 0 ? c.KMEnd.Value : 0
                        }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOPODFLMCOInput>;
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

        public List<DTOPODFLMDIInputExcel> PODFLMCOInput_Export(DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                List<DTOPODFLMDIInputExcel> result = new List<DTOPODFLMDIInputExcel>();
                dtFrom = dtFrom.Date;
                dtTo = dtTo.Date;
                using (var model = new DataEntities())
                {
                    result = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == Account.SYSCustomerID) &&
                        c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived && c.ETD >= dtFrom && c.ETD < dtTo).Select(c => new DTOPODFLMDIInputExcel
                        {
                            ID = c.ID,
                            DITOMasterID = c.ID,
                            DITOMasterCode = c.Code,
                            VehicleID = c.VehicleID > 0 ? c.VehicleID.Value : -1,
                            VehicleNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                            ETA = c.ETA,
                            ETD = c.ETD,
                            AETA = c.ATA,
                            AETD = c.ATD,
                            Driver1 = c.FLM_Driver != null ? new DTOFLMDriver { ID = c.FLM_Driver.ID, EmployeeCode = c.FLM_Driver.Code, DriverName = c.FLM_Driver.CAT_Driver.FirstName + " " + c.FLM_Driver.CAT_Driver.LastName } : null,
                            Driver2 = c.FLM_Driver1 != null ? new DTOFLMDriver { ID = c.FLM_Driver1.ID, EmployeeCode = c.FLM_Driver1.Code, DriverName = c.FLM_Driver1.CAT_Driver.FirstName + " " + c.FLM_Driver1.CAT_Driver.LastName } : null,
                            Driver3 = c.FLM_Driver2 != null ? new DTOFLMDriver { ID = c.FLM_Driver2.ID, EmployeeCode = c.FLM_Driver2.Code, DriverName = c.FLM_Driver2.CAT_Driver.FirstName + " " + c.FLM_Driver2.CAT_Driver.LastName } : null,
                            Driver4 = c.FLM_Driver3 != null ? new DTOFLMDriver { ID = c.FLM_Driver3.ID, EmployeeCode = c.FLM_Driver3.Code, DriverName = c.FLM_Driver3.CAT_Driver.FirstName + " " + c.FLM_Driver3.CAT_Driver.LastName } : null,
                            Driver5 = c.FLM_Driver4 != null ? new DTOFLMDriver { ID = c.FLM_Driver4.ID, EmployeeCode = c.FLM_Driver4.Code, DriverName = c.FLM_Driver4.CAT_Driver.FirstName + " " + c.FLM_Driver4.CAT_Driver.LastName } : null,
                            SortOrder = c.SortOrder,
                            Note1 = c.Note,
                            Note2 = "",
                            TypeOfPaymentDITOMasterID = c.TypeOfPaymentCOTOMasterID > 0 ? c.TypeOfPaymentCOTOMasterID.Value : -1,
                            IsApproved = c.TypeOfPaymentCOTOMasterID == -(int)SYSVarType.TypeOfPaymentDITOMasterApproved,
                            ExIsOverNight = c.ExIsOverNight.HasValue ? c.ExIsOverNight.Value : false,
                            ExIsOverWeight = c.ExIsOverWeight.HasValue ? c.ExIsOverWeight.Value : false,
                            ExTotalDayOut = c.ExTotalDayOut > 0 ? c.ExTotalDayOut.Value : 0,
                            ExTotalJoin = c.ExTotalJoin > 0 ? c.ExTotalJoin.Value : 0,
                            TotalStationCost = c.OPS_COTOStation.Count(d => d.IsMonth == false) > 0 ? c.OPS_COTOStation.Where(d => d.IsMonth == false).Sum(d => d.Price) : 0,
                            TotalTroubleCost = c.CAT_Trouble.Count() > 0 ? c.CAT_Trouble.Sum(d => d.Cost) : 0,
                            KmStart = c.KMStart > 0 ? c.KMStart.Value : 0,
                            KmEnd = c.KMEnd > 0 ? c.KMEnd.Value : 0,
                            CustomerCode = c.CUS_Customer.Code,
                            IsBidding = c.IsBidding,
                            CountOfStation = c.OPS_COTOStation.Count(),
                            CountOfTrouble = c.CAT_Trouble.Count(),
                            CountOfLocation = c.OPS_COTOLocation.Count(),
                            ListStationCost = c.OPS_COTOStation.Select(d => new DTOPODOPSDITOStation
                            {
                                ID = d.ID,
                                LocationID = d.LocationID,
                                LocationCode = d.CAT_Location.Code,
                                LocationName = d.CAT_Location.Location,
                                LocationAddress = d.CAT_Location.Address,
                                Price = d.Price
                            }).ToList(),
                            ListTroubleCost = c.CAT_Trouble.Select(e => new DTOPODCATTroubleCost
                            {
                                ID = e.ID,
                                GroupOfTroubleID = e.GroupOfTroubleID,
                                GroupOfTroubleCode = e.CAT_GroupOfTrouble.Code,
                                GroupOfTroubleName = e.CAT_GroupOfTrouble.Name,
                                Cost = e.CostOfVendor,
                                Note = e.Description,
                            }).OrderBy(e => e.GroupOfTroubleID).ToList(),
                            ListLocation = c.OPS_COTOLocation.Select(f => new CATLocation
                            {
                                ID = f.ID,
                                Code = f.CAT_Location.Code,
                                GroupOfLocationCode = f.CAT_Location.CAT_GroupOfLocation.Code,
                            }).ToList(),
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

        public void PODFLMCOInput_Import(List<DTOPODFLMDIInputImport> lst)
        {
            try
            {
                int iDriver = -(int)SYSVarType.TypeOfDriverMain;
                int iDriverEx = -(int)SYSVarType.TypeOfDriverEx;
                int iLoader = -(int)SYSVarType.TypeOfDriverLoad;
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    if (lst != null)
                    {
                        foreach (var item in lst.Where(c => c.ExcelSuccess))
                        {
                            var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.Code == item.DITOMasterCode);
                            if (objMaster != null)
                            {
                                objMaster.ModifiedBy = Account.UserName;
                                objMaster.ModifiedDate = DateTime.Now;

                                #region lưu tài xế
                                //tai xe 1
                                if (item.Driver1 != null)
                                {
                                    objMaster.DriverID1 = item.Driver1.ID > 0 ? (int?)item.Driver1.ID : null;
                                    objMaster.TypeOfDriverID1 = iDriver;

                                    var driver1 = model.FLM_Driver.FirstOrDefault(c => c.ID == objMaster.DriverID1);
                                    if (driver1 != null)
                                    {
                                        objMaster.DriverName1 = driver1.CAT_Driver.LastName + " " + driver1.CAT_Driver.FirstName;
                                        objMaster.DriverTel1 = driver1.CAT_Driver.Cellphone;
                                        objMaster.DriverCard1 = driver1.CAT_Driver.CardNumber;
                                    }
                                    else
                                    {
                                        objMaster.DriverName1 = string.Empty;
                                        objMaster.DriverTel1 = string.Empty;
                                        objMaster.DriverCard1 = string.Empty;
                                    }
                                }


                                //tai xe 2
                                if (item.Driver2 != null)
                                {
                                    objMaster.DriverID2 = item.Driver2.ID > 0 ? (int?)item.Driver2.ID : null;
                                    objMaster.TypeOfDriverID2 = iDriverEx;

                                    var driver2 = model.FLM_Driver.FirstOrDefault(c => c.ID == objMaster.DriverID2);
                                    if (driver2 != null)
                                    {
                                        objMaster.DriverName2 = driver2.CAT_Driver.LastName + " " + driver2.CAT_Driver.FirstName;
                                        objMaster.DriverTel2 = driver2.CAT_Driver.Cellphone;
                                        objMaster.DriverCard2 = driver2.CAT_Driver.CardNumber;
                                    }
                                    else
                                    {
                                        objMaster.DriverName2 = string.Empty;
                                        objMaster.DriverTel2 = string.Empty;
                                        objMaster.DriverCard2 = string.Empty;
                                    }
                                }

                                //tai xe 3
                                if (item.Driver3 != null)
                                {
                                    objMaster.DriverID3 = item.Driver3.ID > 0 ? (int?)item.Driver3.ID : null;
                                    objMaster.TypeOfDriverID3 = iLoader;
                                }

                                //tai xe 4
                                if (item.Driver4 != null)
                                {
                                    objMaster.DriverID4 = item.Driver4.ID > 0 ? (int?)item.Driver4.ID : null;
                                    objMaster.TypeOfDriverID4 = iLoader;
                                }

                                //tai xe 5
                                if (item.Driver5 != null)
                                {
                                    objMaster.DriverID5 = item.Driver5.ID > 0 ? (int?)item.Driver5.ID : null;
                                    objMaster.TypeOfDriverID5 = iLoader;
                                }
                                #endregion

                                #region lưu station
                                foreach (var station in item.ListStationCost)
                                {
                                    var objStation = model.OPS_COTOStation.FirstOrDefault(c => c.ID == station.ID);
                                    if (objStation != null)
                                        objStation.Price = station.Price;
                                }
                                #endregion

                                #region lưu trouble
                                foreach (var trouble in item.ListTroubleCost)
                                {
                                    var objTrouble = model.CAT_Trouble.FirstOrDefault(c => c.ID == trouble.ID);
                                    if (objTrouble != null)
                                        objTrouble.Cost = trouble.Cost;
                                    else
                                    {
                                        objTrouble = new CAT_Trouble();
                                        objTrouble.CreatedBy = Account.UserName;
                                        objTrouble.CreatedDate = DateTime.Now;
                                        objTrouble.COTOMasterID = item.DITOMasterID;
                                        objTrouble.GroupOfTroubleID = trouble.GroupOfTroubleID;
                                        objTrouble.Cost = trouble.Cost;
                                        objTrouble.CostOfVendor = trouble.Cost;
                                        objTrouble.TroubleCostStatusID = -(int)SYSVarType.TroubleCostStatusApproved;
                                        objTrouble.Description = trouble.Note;
                                        objTrouble.AttachmentFile = string.Empty;
                                        objTrouble.COTOID = null;
                                        model.CAT_Trouble.Add(objTrouble);
                                    }
                                }
                                #endregion
                            }
                            else
                                throw FaultHelper.BusinessFault(null, null, "Mã chuyến không tồn tại");



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
        public void PODFLMCOInput_Save(DTOPODFLMCOInput item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == item.ID);
                    if (obj == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy chuyến ID" + item.COTOMasterID);
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;

                        // obj.Note1 = item.Note1;
                        //obj.Note2 = item.Note2;

                        obj.DriverID1 = item.DriverID1 > 0 ? item.DriverID1 : null;
                        obj.TypeOfDriverID1 = item.TypeOfDriverID1 > 0 ? item.TypeOfDriverID1 : null;

                        var driver1 = model.FLM_Driver.FirstOrDefault(c => c.ID == item.DriverID1);
                        if (driver1 != null)
                        {
                            obj.DriverName1 = driver1.CAT_Driver.LastName + " " + driver1.CAT_Driver.FirstName;
                            obj.DriverTel1 = driver1.CAT_Driver.Cellphone;
                            obj.DriverCard1 = driver1.CAT_Driver.CardNumber;
                        }
                        else
                        {
                            obj.DriverName1 = string.Empty;
                            obj.DriverTel1 = string.Empty;
                            obj.DriverCard1 = string.Empty;
                        }

                        obj.DriverID2 = item.DriverID2 > 0 ? item.DriverID2 : null;
                        obj.TypeOfDriverID2 = item.TypeOfDriverID2 > 0 ? item.TypeOfDriverID2 : null;

                        var driver2 = model.FLM_Driver.FirstOrDefault(c => c.ID == item.DriverID2);
                        if (driver2 != null)
                        {
                            obj.DriverName2 = driver2.CAT_Driver.LastName + " " + driver2.CAT_Driver.FirstName;
                            obj.DriverTel2 = driver2.CAT_Driver.Cellphone;
                            obj.DriverCard2 = driver2.CAT_Driver.CardNumber;
                        }
                        else
                        {
                            obj.DriverName2 = string.Empty;
                            obj.DriverTel2 = string.Empty;
                            obj.DriverCard2 = string.Empty;
                        }

                        obj.ExIsOverNight = item.ExIsOverNight;
                        obj.ExIsOverWeight = item.ExIsOverWeight;
                        obj.ExTotalDayOut = item.ExTotalDayOut;
                        obj.ExTotalJoin = item.ExTotalJoin;

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
        public void PODFLMCOInput_Approved(List<int> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    foreach (var item in lst)
                    {
                        var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == item);
                        if (obj != null && obj.TypeOfPaymentCOTOMasterID != -(int)SYSVarType.TypeOfPaymentCOTOMasterApproved)
                        {
                            obj.ApprovedBy = Account.UserName;
                            obj.ApprovedDate = DateTime.Now;
                            obj.TypeOfPaymentCOTOMasterID = -(int)SYSVarType.TypeOfPaymentCOTOMasterApproved;
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

        public DTOPODFLMCOInputDriver PODFLMCOInput_GetDrivers(int COTOMasterID)
        {
            try
            {
                DTOPODFLMCOInputDriver result = new DTOPODFLMCOInputDriver();
                using (var model = new DataEntities())
                {
                    var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == COTOMasterID);
                    if (objMaster != null)
                    {
                        result.COTOMasterID = objMaster.ID;
                        result.DriverID1 = objMaster.DriverID1 > 0 ? objMaster.DriverID1 : -1;
                        result.DriverID2 = objMaster.DriverID2 > 0 ? objMaster.DriverID2 : -1;
                        result.DriverID3 = objMaster.DriverID3 > 0 ? objMaster.DriverID3 : -1;
                        result.DriverID4 = objMaster.DriverID4 > 0 ? objMaster.DriverID4 : -1;
                        result.DriverID5 = objMaster.DriverID5 > 0 ? objMaster.DriverID5 : -1;

                        result.TypeOfDriverID1 = objMaster.TypeOfDriverID1 > 0 ? objMaster.TypeOfDriverID1 : -1;
                        result.TypeOfDriverID2 = objMaster.TypeOfDriverID2 > 0 ? objMaster.TypeOfDriverID2 : -1;
                        result.TypeOfDriverID3 = objMaster.TypeOfDriverID3 > 0 ? objMaster.TypeOfDriverID3 : -1;
                        result.TypeOfDriverID4 = objMaster.TypeOfDriverID4 > 0 ? objMaster.TypeOfDriverID4 : -1;
                        result.TypeOfDriverID5 = objMaster.TypeOfDriverID5 > 0 ? objMaster.TypeOfDriverID5 : -1;
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
        public void PODFLMCOInput_SaveDrivers(DTOPODFLMCOInputDriver item, int COTOMasterID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == COTOMasterID);
                    if (obj == null) throw FaultHelper.BusinessFault(null, null, "Không tìm thấy chuyến ID" + COTOMasterID);
                    else
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;

                        obj.DriverID1 = item.DriverID1 > 0 ? item.DriverID1 : null;
                        obj.TypeOfDriverID1 = item.TypeOfDriverID1 > 0 ? item.TypeOfDriverID1 : null;

                        var driver1 = model.FLM_Driver.FirstOrDefault(c => c.ID == item.DriverID1);
                        if (driver1 != null)
                        {
                            obj.DriverName1 = driver1.CAT_Driver.LastName + " " + driver1.CAT_Driver.FirstName;
                            obj.DriverTel1 = driver1.CAT_Driver.Cellphone;
                            obj.DriverCard1 = driver1.CAT_Driver.CardNumber;
                        }
                        else
                        {
                            obj.DriverName1 = string.Empty;
                            obj.DriverTel1 = string.Empty;
                            obj.DriverCard1 = string.Empty;
                        }

                        obj.DriverID2 = item.DriverID2 > 0 ? item.DriverID2 : null;
                        obj.TypeOfDriverID2 = item.TypeOfDriverID2 > 0 ? item.TypeOfDriverID2 : null;

                        var driver2 = model.FLM_Driver.FirstOrDefault(c => c.ID == item.DriverID2);
                        if (driver2 != null)
                        {
                            obj.DriverName2 = driver2.CAT_Driver.LastName + " " + driver2.CAT_Driver.FirstName;
                            obj.DriverTel2 = driver2.CAT_Driver.Cellphone;
                            obj.DriverCard2 = driver2.CAT_Driver.CardNumber;
                        }
                        else
                        {
                            obj.DriverName2 = string.Empty;
                            obj.DriverTel2 = string.Empty;
                            obj.DriverCard2 = string.Empty;
                        }

                        obj.DriverID3 = item.DriverID3 > 0 ? item.DriverID3 : null;
                        obj.TypeOfDriverID3 = item.TypeOfDriverID3 > 0 ? item.TypeOfDriverID3 : null;

                        obj.DriverID4 = item.DriverID4 > 0 ? item.DriverID4 : null;
                        obj.DriverID5 = item.DriverID5 > 0 ? item.DriverID5 : null;
                        obj.TypeOfDriverID4 = item.TypeOfDriverID4 > 0 ? item.TypeOfDriverID4 : null;
                        obj.TypeOfDriverID5 = item.TypeOfDriverID5 > 0 ? item.TypeOfDriverID5 : null;

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

        public DTOResult PODFLMCOInput_TroubleCostList(string request, int COTOMasterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.CAT_Trouble.Where(c => c.COTOMasterID > 0 && c.COTOMasterID == COTOMasterID).Select(c => new DTOPODCATTroubleCost
                    {
                        ID = c.ID,
                        GroupOfTroubleID = c.GroupOfTroubleID,
                        GroupOfTroubleCode = c.CAT_GroupOfTrouble.Code,
                        GroupOfTroubleName = c.CAT_GroupOfTrouble.Name,
                        Cost = c.CostOfVendor,
                        Note = c.Description,
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOPODCATTroubleCost>;
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

        public DTOResult PODFLMCOInput_TroubleCostNotIn_List(string request, int COTOMasterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var objMaster = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == COTOMasterID);
                    if (objMaster != null)
                    {
                        var hasList = model.CAT_Trouble.Where(c => c.COTOMasterID > 0 && c.COTOMasterID == COTOMasterID).Select(c => c.GroupOfTroubleID).Distinct().ToList();
                        var query = model.CAT_GroupOfTrouble.Where(c => !hasList.Contains(c.ID)).Select(c => new DTOPODCATTrouble
                        {
                            ID = c.ID,
                            GroupOfTroubleCode = c.Code,
                            GroupOfTroubleName = c.Name,
                        }).ToDataSourceResult(CreateRequest(request));
                        result.Total = query.Total;
                        result.Data = query.Data as IEnumerable<DTOPODCATTrouble>;
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

        public void PODFLMCOInput_TroubleCostNotIn_SaveList(List<int> lst, int COTOMasterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    #region Kiểm tra trùng GroupID
                    foreach (var item in lst)
                    {
                        var obj = model.CAT_Trouble.FirstOrDefault(c => c.GroupOfTroubleID == item && c.COTOMasterID == COTOMasterID);
                        if (obj != null)
                            throw FaultHelper.BusinessFault(null, null, "Mã đã sử dụng!");
                    }

                    foreach (var item in lst)
                    {
                        var obj = new CAT_Trouble();
                        obj.CreatedBy = Account.UserName;
                        obj.CreatedDate = DateTime.Now;
                        obj.COTOMasterID = COTOMasterID;
                        obj.GroupOfTroubleID = item;
                        obj.Cost = 0;
                        obj.CostOfVendor = 0;
                        obj.TroubleCostStatusID = -(int)SYSVarType.TroubleCostStatusApproved;
                        obj.Description = string.Empty;
                        obj.AttachmentFile = string.Empty;
                        obj.COTOID = null;
                        model.CAT_Trouble.Add(obj);
                    }
                    model.SaveChanges();
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

        public void PODFLMCOInput_TroubleCost_DeleteList(List<int> lst)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst)
                    {
                        var obj = model.CAT_Trouble.FirstOrDefault(c => c.ID == item);
                        if (obj != null)
                            model.CAT_Trouble.Remove(obj);
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
        public void PODFLMCOInput_TroubleCostSave(List<DTOPODCATTroubleCost> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst)
                    {
                        var obj = model.CAT_Trouble.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                        {
                            obj.Cost = item.Cost;
                            obj.CostOfVendor = item.Cost;
                            obj.Description = item.Note;
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

        public DTOResult PODFLMCOInput_StationCostList(string request, int COTOMasterID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_COTOStation.Where(c => c.COTOMasterID > 0 && c.COTOMasterID == COTOMasterID && c.IsMonth == false).Select(c => new DTOPODOPSCOTOStation
                    {
                        ID = c.ID,
                        LocationID = c.LocationID,
                        LocationCode = c.CAT_Location.Code,
                        LocationName = c.CAT_Location.Location,
                        LocationAddress = c.CAT_Location.Address,
                        Price = c.Price
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOPODOPSCOTOStation>;
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
        public void PODFLMCOInput_StationCostSave(List<DTOPODOPSCOTOStation> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var item in lst)
                    {
                        var obj = model.OPS_COTOStation.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                            obj.Price = item.Price;
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

        public DTOResult PODFLMCOInput_DriverList()
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.FLM_Driver.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.IsUse).Select(c => new DTOFLMDriver
                    {
                        ID = c.ID,
                        DriverID = c.DriverID,
                        EmployeeCode = c.Code,
                        DriverName = c.CAT_Driver.LastName + " " + c.CAT_Driver.FirstName,
                        IsAssistant = c.IsAssistant
                    }).ToList();
                    result.Total = query.Count;
                    result.Data = query as IEnumerable<DTOFLMDriver>;
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

        #region PODImport
        public List<DTOPODImport> PODImport_Data(DateTime dtFrom, DateTime dtTO, int cusId)
        {
            try
            {
                List<DTOPODImport> result = new List<DTOPODImport>();
                dtFrom = dtFrom.Date;
                dtTO = dtTO.Date.AddDays(1);
                using (var model = new DataEntities())
                {
                    result = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.CustomerID == cusId && c.OPS_DITOMaster.ETD >= dtFrom && c.OPS_DITOMaster.ETD < dtTO
                        && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete
                        && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived).Select(c => new DTOPODImport
                        {
                            ID = c.ID,
                            DNCode = c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            ETARequest = c.ORD_GroupProduct.ETARequest,
                            ETD = c.ORD_GroupProduct.ETD,
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
                            CBM = c.CBM,
                            CBMBBGN = c.CBMBBGN,
                            CBMTranfer = c.CBMTranfer,
                            Quantity = c.Quantity,
                            QuantityBBGN = c.QuantityBBGN,
                            QuantityTranfer = c.QuantityTranfer,
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

        public DTOResult PODImport_Index_Setting_List(string request)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    DTOResult result = new DTOResult();
                    var query = model.CUS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.CustomerID == Account.SYSCustomerID ? true : Account.ListCustomerID.Contains(c.CustomerID)) && c.Key == CUSSettingKey.POD.ToString()).Select(c => new DTOCUSSettingPOD
                    {
                        SettingID = c.ID,
                        Name = c.Name,
                        CreateBy = c.CreatedBy,
                        CreateDate = c.CreatedDate,
                        CustomerID = c.CustomerID,
                        SettingCustomerName = c.CUS_Customer.CustomerName
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOCUSSettingPOD>;
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

        public DTOCUSSettingPOD PODImport_Index_Setting_Get(int id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    DTOCUSSettingPOD result = new DTOCUSSettingPOD();
                    if (id > 0)
                    {
                        var obj = model.CUS_Setting.Where(c => c.ID == id).FirstOrDefault();
                        if (obj != null)
                        {
                            result = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingPOD>(obj.Setting);
                        }
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

        public void PODImport_Excel_Import(int sID, List<DTOPODImport> data)
        {
            try
            {
                DTOCUSSettingPOD objSetting = PODImport_Index_Setting_Get(sID);
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    //Order by CustomerID
                    data = data.OrderBy(c => c.CustomerID).ToList();

                    //Check code
                    foreach (var item in data.Where(c => c.ExcelSuccess))
                    {
                        var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                        {
                            if (objSetting.DNCode > 0)
                                obj.DNCode = item.DNCode;

                            if (objSetting.SOCode > 0)
                                obj.ORD_GroupProduct.SOCode = item.SOCode;

                            if (objSetting.IsInvoice > 0)
                                obj.DITOGroupProductStatusPODID = item.IsInvoice ? (-(int)SYSVarType.DITOGroupProductStatusPODComplete) : (-(int)SYSVarType.DITOGroupProductStatusPODWait);

                            if (objSetting.DateFromCome > 0)
                                obj.DateFromCome = item.DateFromCome;

                            if (objSetting.DateFromLeave > 0)
                                obj.DateFromLeave = item.DateFromLeave;

                            if (objSetting.DateFromLoadEnd > 0)
                                obj.DateFromLoadEnd = item.DateFromLoadEnd;

                            if (objSetting.DateFromLoadStart > 0)
                                obj.DateFromLoadStart = item.DateFromLoadStart;

                            if (objSetting.DateToCome > 0)
                                obj.DateToCome = item.DateToCome;

                            if (objSetting.DateToLeave > 0)
                                obj.DateToLeave = item.DateToLeave;

                            if (objSetting.DateToLoadEnd > 0)
                                obj.DateToLoadEnd = item.DateToLoadEnd;

                            if (objSetting.DateToLoadStart > 0)
                                obj.DateToLoadStart = item.DateToLoadStart;

                            if (objSetting.InvoiceDate > 0)
                                obj.InvoiceDate = item.InvoiceDate;

                            if (objSetting.InvoiceNote > 0)
                                obj.InvoiceNote = item.InvoiceNote;

                            if (objSetting.Note > 0)
                                obj.Note = item.Note;

                            if (objSetting.Note1 > 0)
                                obj.Note1 = item.OPSGroupNote1;

                            if (objSetting.Note2 > 0)
                                obj.Note2 = item.OPSGroupNote1;

                            if (objSetting.ChipNo > 0)
                                obj.OPS_DITOMaster.Note1 = item.ChipNo;

                            if (objSetting.Temperature > 0)
                                obj.OPS_DITOMaster.Note2 = item.Temperature;

                            if (objSetting.TonBBGN > 0)
                                obj.TonBBGN = item.TonBBGN;

                            if (objSetting.TonTranfer > 0)
                                obj.TonTranfer = item.TonTranfer;

                            if (objSetting.CBMBBGN > 0)
                                obj.CBMBBGN = item.CBMBBGN;

                            if (objSetting.CBMTranfer > 0)
                                obj.CBMTranfer = item.CBMTranfer;

                            if (objSetting.QuantityBBGN > 0)
                                obj.QuantityBBGN = item.QuantityBBGN;

                            if (objSetting.QuantityTranfer > 0)
                                obj.QuantityTranfer = item.QuantityTranfer;


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

        #region chung tu chi check nahn
        public DTOResult PODDIInput_Check_List(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID, bool hasIsReturn)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OrderGroupProductID > 0 && (hasIsReturn == true ? true : (c.ORD_GroupProduct.IsReturn != true)) && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID &&
                        c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived &&
                        c.ORD_GroupProduct.ORD_Order.RequestDate >= dtFrom && c.ORD_GroupProduct.ORD_Order.RequestDate < dtTO && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel &&
                        listCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID)).OrderBy(c => c.DITOMasterID)
                        .Select(c => new DTOPODDIInput
                        {
                            ID = c.ID,
                            CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                            CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                            CustomerName = c.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                            DNCode = c.DNCode == null ? string.Empty : c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode == null ? string.Empty : c.ORD_GroupProduct.SOCode,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            ETARequest = c.ORD_GroupProduct.ETARequest,
                            ETD = c.ORD_GroupProduct.ETD,

                            LocationFromCode = c.ORD_GroupProduct.CUS_Location.Code,
                            LocationToCode = c.ORD_GroupProduct.CUS_Location1.Code,
                            LocationToName = c.ORD_GroupProduct.CUS_Location1.LocationName,
                            LocationToAddress = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                            LocationToProvince = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName,
                            LocationToDistrict = c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName,
                            CreatedDate = c.ORD_GroupProduct.CreatedDate,
                            MasterCode = c.OPS_DITOMaster.Code,
                            MasterID = c.DITOMasterID,
                            RegNo = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                            DriverName = c.OPS_DITOMaster.DriverName1,
                            DriverTel = c.OPS_DITOMaster.DriverTel1,
                            DriverCard = c.OPS_DITOMaster.DriverCard1,
                            RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                            IsComplete = c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete ? 1 : 0,
                            IsInvoice = c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete,
                            DateFromCome = c.DateFromCome,
                            DateFromLeave = c.DateFromLeave,
                            DateFromLoadEnd = c.DateFromLoadEnd,
                            DateFromLoadStart = c.DateFromLoadStart,
                            DateToCome = c.DateToCome,
                            DateToLeave = c.DateToLeave,
                            DateToLoadEnd = c.DateToLoadEnd,
                            DateToLoadStart = c.DateToLoadStart,
                            EconomicZone = c.ORD_GroupProduct.CUS_Location1.CAT_Location.EconomicZone,
                            ETDMaster = c.OPS_DITOMaster.ETD,
                            Weight = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.MaxWeight : null,
                            OrderID = c.ORD_GroupProduct.OrderID,
                            DITOGroupProductStatusPODID = c.DITOGroupProductStatusPODID,
                            DITOGroupProductStatusPODName = c.SYS_Var1.ValueOfVar,
                            IsOrigin = c.IsOrigin,
                            DITOGroupProductStatusID = c.DITOGroupProductStatusID,
                            InvoiceBy = c.InvoiceBy,
                            InvoiceDate = c.InvoiceDate,
                            InvoiceNote = c.InvoiceNote,
                            Note = c.Note,
                            Note1 = c.Note1,
                            Note2 = c.Note2,
                            Description = c.ORD_GroupProduct.Description,
                            VendorName = (c.OPS_DITOMaster.VendorOfVehicleID == null || c.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID) ? "Xe nhà" : c.OPS_DITOMaster.CUS_Customer.CustomerName,
                            VendorCode = (c.OPS_DITOMaster.VendorOfVehicleID == null || c.OPS_DITOMaster.VendorOfVehicleID == Account.SYSCustomerID) ? "Xe nhà" : c.OPS_DITOMaster.CUS_Customer.Code,
                            GroupOfProductCode = c.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                            GroupOfProductName = c.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                            DatePODContract = null,
                            //StatusHitPOD = string.Empty,
                            StatusOPSMaster = c.OPS_DITOMaster.SYS_Var.ValueOfVar,
                            StatusSOPOD = string.Empty,
                            DNABA = c.OPS_DITOMaster.Code,
                            ChipNo = c.OPS_DITOMaster.Note1,
                            Temperature = c.OPS_DITOMaster.Note2,
                            //30-3
                            Ton = c.Ton,
                            TonBBGN = c.TonBBGN,
                            TonTransfer = c.TonTranfer,
                            CBM = c.CBM,
                            CBMBBGN = c.CBMBBGN,
                            CBMTransfer = c.CBMTranfer,
                            Quantity = c.Quantity,
                            QuantityBBGN = c.QuantityBBGN,
                            QuantityTransfer = c.QuantityTranfer,
                            StatusOrder = c.ORD_GroupProduct.ORD_Order.SYS_Var.ValueOfVar,

                            OrderGroupProductID = c.OrderGroupProductID.Value,
                            HasReturn = false,
                            TotalReturn = 0,
                            TonReturn = c.TonReturn,
                            CBMReturn = c.CBMReturn,
                            QuantityReturn = c.QuantityReturn,
                            TypeOfDITOGroupProductReturnID = c.TypeOfDITOGroupProductReturnID > 0 ? c.TypeOfDITOGroupProductReturnID.Value : -1,
                            TypeOfDITOGroupProductReturnName = c.TypeOfDITOGroupProductReturnID > 0 ? c.OPS_TypeOfDITOGroupProductReturn.TypeName : string.Empty,
                            InvoiceReturnBy = c.InvoiceReturnBy,
                            InvoiceReturnDate = c.InvoiceReturnDate,
                            InvoiceReturnNote = c.InvoiceReturnNote,

                            IsClosed = c.IsClosed,
                            ClosedBy = c.ClosedBy,
                            ClosedDate = c.ClosedDate,
                            HasUpload = c.HasUpload,
                            DateDN = c.DateDN,
                        }).ToDataSourceResult(CreateRequest(request));

                    var lst = query.Data.Cast<DTOPODDIInput>().ToList();

                    var lstOrderGroupProductID = lst.Select(c => c.OrderGroupProductID).Distinct().ToList();
                    var lstSOCodeCheck = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID > 0 && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel &&
                        lstOrderGroupProductID.Contains(c.OrderGroupProductID.Value)).Select(c => new
                        {
                            c.ORD_GroupProduct.SOCode,
                            c.DITOGroupProductStatusPODID
                        }).ToList();
                    var lstSOCode = lst.Where(c => !string.IsNullOrEmpty(c.SOCode)).Select(c => c.SOCode).Distinct().ToList();
                    Dictionary<string, string> dicSOCode = new Dictionary<string, string>();
                    foreach (var socode in lstSOCode)
                    {
                        if (lstSOCodeCheck.Where(c => c.SOCode == socode && c.DITOGroupProductStatusPODID != -(int)SYSVarType.DITOGroupProductStatusPODComplete).Count() == 0)
                            dicSOCode.Add(socode, "Đã nhận");
                        else if (lstSOCodeCheck.Where(c => c.SOCode == socode && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete).Count() > 0)
                            dicSOCode.Add(socode, "Nhận một phần");
                        else
                            dicSOCode.Add(socode, "Chưa nhận");
                    }

                    var lstOrderGroupID = lst.Select(c => c.ID).ToList();
                    var lstKPI = model.KPI_KPITime.Where(c => c.DITOGroupProductID > 0 && lstOrderGroupID.Contains(c.DITOGroupProductID.Value)).Select(c => new
                    {
                        c.DITOGroupProductID,
                        c.KPIID,
                        c.KPIDate,
                        c.IsKPI
                    }).ToList();
                    var lstReturn = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OrderGroupProductID > 0 && c.ORD_GroupProduct.IsReturn == true && c.ORD_GroupProduct.ReturnID > 0 && lstOrderGroupProductID.Contains(c.ORD_GroupProduct.ReturnID.Value))
                        .Select(c => new
                        {
                            DITOMasterID = c.DITOMasterID.Value,
                            ReturnID = c.ORD_GroupProduct.ReturnID.Value,
                            OrderGroupProductID = c.OrderGroupProductID.Value,
                            c.Ton,
                            c.CBM,
                            c.Quantity
                        }).ToList();

                    foreach (var item in lst)
                    {
                        if (dicSOCode.ContainsKey(item.SOCode))
                            item.StatusSOPOD = dicSOCode[item.SOCode];
                        //else
                        //    item.StatusSOPOD = str;

                        var kpi = lstKPI.FirstOrDefault(c => c.DITOGroupProductID == item.ID && c.KPIID == (int)KPICode.OPS);
                        if (kpi != null)
                        {
                            item.KPIOPSDate = kpi.KPIDate;
                            item.IsKPIOPS = kpi.IsKPI;
                        }
                        kpi = lstKPI.FirstOrDefault(c => c.DITOGroupProductID == item.ID && c.KPIID == (int)KPICode.POD);
                        if (kpi != null)
                        {
                            item.KPIPODDate = kpi.KPIDate;
                            item.IsKPIPOD = kpi.IsKPI;
                        }

                        var queryReturn = lstReturn.Where(c => c.DITOMasterID == item.MasterID && c.ReturnID == item.OrderGroupProductID);
                        item.HasReturn = queryReturn.Count() > 0;
                        if (item.HasReturn)
                            item.TotalReturn = queryReturn.Sum(c => c.Quantity);
                    }

                    result.Data = lst.ToArray();
                    result.Total = query.Total;


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
        public void PODDIInput_Check_Save(DTOPODDIInput item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    List<int> lstMasterID = new List<int>();
                    List<int> lstOrderID = new List<int>();
                    var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                    if (obj != null)
                    {
                        if (item.IsInvoice)
                            obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;
                        else obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODWait;

                        //obj.IsClosed = true;
                        obj.InvoiceNote = item.InvoiceNote;
                        obj.InvoiceDate = item.InvoiceDate;
                        obj.InvoiceBy = item.InvoiceBy;
                        obj.DateDN = item.DateDN;
                        obj.Note = item.Note;
                        obj.Note1 = item.Note1;
                        obj.Note2 = item.Note2;
                        obj.ORD_GroupProduct.Description = item.Description;
                    }
                    model.SaveChanges();

                    //Cap nhat KPI
                    // HelperKPI.KPITime_DIPODChange(model, Account, new List<int> { item.ID });

                    // Ktra cập nhật status
                    HelperStatus.ORDOrder_Status(model, Account, new List<int> { obj.ORD_GroupProduct.OrderID });
                    //using (var status = new StatusHelper())
                    //{
                    //    status.ORDStatus_Update(new List<int> { obj.ORD_GroupProduct.OrderID });
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

        public void PODDIInput_Check_SaveList(List<DTOPODDIInput> list)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    List<int> lstMasterID = new List<int>();
                    List<int> lstOrderID = new List<int>();
                    foreach (var item in list)
                    {
                        var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                        {
                            if (item.IsInvoice)
                                obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;
                            else obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODWait;

                            //obj.IsClosed = true;
                            obj.InvoiceNote = item.InvoiceNote;
                            obj.InvoiceDate = item.InvoiceDate;
                            obj.InvoiceBy = item.InvoiceBy;
                            obj.DateDN = item.DateDN;
                            obj.Note = item.Note;
                            obj.Note1 = item.Note1;
                            obj.Note2 = item.Note2;
                            obj.ORD_GroupProduct.Description = item.Description;
                            lstOrderID.Add(obj.ORD_GroupProduct.OrderID);
                        }
                    }
                    model.SaveChanges();

                    HelperStatus.ORDOrder_Status(model, Account, lstOrderID);
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

        public void PODDIInput_Check_Reset(int DITOGroupID)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    List<int> lstMasterID = new List<int>();
                    List<int> lstOrderID = new List<int>();
                    var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == DITOGroupID);
                    if (obj == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu OPS_DITOGroupProduct");
                    obj.ModifiedBy = Account.UserName;
                    obj.ModifiedDate = DateTime.Now;
                    obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODWait;

                    obj.IsClosed = false;

                    model.SaveChanges();
                    HelperStatus.ORDOrder_Status(model, Account, new List<int> { obj.ORD_GroupProduct.OrderID });
                    //using (var status = new StatusHelper())
                    //{
                    //    status.ORDStatus_Update(new List<int> { obj.ORD_GroupProduct.OrderID });
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
        #endregion

        #region upload đơn hàng
        public DTOResult PODDIInput_UploadOrder_List(string request, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                DTOResult result = new DTOResult();
                dtFrom = dtFrom.Date;
                dtTO = dtTO.AddDays(1).Date;
                int iLTL = -(int)SYSVarType.TransportModeLTL;
                int iFTL = -(int)SYSVarType.TransportModeFTL;
                using (var model = new DataEntities())
                {
                    var query = model.OPS_DITOProduct.Where(c => c.OPS_DITOGroupProduct.DITOMasterID > 0 && c.OPS_DITOGroupProduct.OrderGroupProductID > 0 && c.OPS_DITOGroupProduct.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID &&
                        c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.RequestDate >= dtFrom && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.RequestDate < dtTO
                        && (c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CAT_TransportMode.TransportModeID == iFTL || c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CAT_TransportMode.TransportModeID == iLTL)).Select(c => new PODInputUpLoadOrder
                        {
                            ID = c.ID,
                            OrderID = c.OPS_DITOGroupProduct.ORD_GroupProduct.OrderID,
                            OrderCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code,
                            RequestDate = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.RequestDate,
                            CustomerID = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID,
                            CustomerCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                            CustomerName = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                            TransportModeID = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CAT_TransportMode.TransportModeID,
                            TransportModeName = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CAT_TransportMode.Name,

                            StockID = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationFromID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationFromID.Value : -1,
                            StockCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationFromID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.Code : string.Empty,

                            LocationToID = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID.Value : -1,
                            LocationToCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.Code : string.Empty,
                            LocationToName = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationName : string.Empty,
                            LocationToAddress = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : string.Empty,

                            PartnerID = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID.Value : -1,
                            PartnerCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.PartnerCode : string.Empty,
                            PartnerName = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName : string.Empty,
                            PartnerAddress = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.CAT_Partner.Address : string.Empty,

                            GroupOfProductID = c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID.Value : -1,
                            GroupOfProductCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.Code : string.Empty,
                            GroupOfProductName = c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName : string.Empty,
                            ProductID = c.ORD_Product.ProductID,
                            ProductCode = c.ORD_Product.CUS_Product.Code,
                            ProductName = c.ORD_Product.CUS_Product.ProductName,
                            Ton = c.OPS_DITOGroupProduct.ORD_GroupProduct.Ton,
                            CBM = c.OPS_DITOGroupProduct.ORD_GroupProduct.CBM,
                            Quantity = c.OPS_DITOGroupProduct.ORD_GroupProduct.Quantity,
                            VehicleID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID.Value : -1,
                            VehicleCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                            VendorID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID.Value : -1,
                            VendorCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.CUS_Customer.Code : string.Empty,
                            DriverID1 = c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverID1 > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverID1.Value : -1,
                            DriverName1 = c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverName1,
                            DriverID2 = c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverID2 > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverID2.Value : -1,
                            DriverName2 = c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverName2,
                            ETA = c.OPS_DITOGroupProduct.OPS_DITOMaster.ETA.Value,
                            ETD = c.OPS_DITOGroupProduct.OPS_DITOMaster.ETD.Value,
                            InvoiceNo = c.OPS_DITOGroupProduct.InvoiceNote,
                            Note1 = c.OPS_DITOGroupProduct.Note1,

                            ORDGroupID = c.OPS_DITOGroupProduct.OrderGroupProductID.Value,
                            ORDProductID = c.OrderProductID,
                            OPSGroupID = c.DITOGroupProductID,
                            OPSMasterID = c.OPS_DITOGroupProduct.DITOMasterID.Value,

                        }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<PODInputUpLoadOrder>;
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
        public PODInputUpLoadOrderData PODDIInput_UploadOrder_GetData()
        {
            try
            {
                PODInputUpLoadOrderData result = new PODInputUpLoadOrderData();
                result.ListCustomer = new List<CUSCustomer>();
                result.ListDriver = new List<DTOCUSDriver>();
                result.ListGroupProduct = new List<DTOPODCUSGroupOfProductInStock>();
                result.ListPartner = new List<DTOCUSPartner>();
                result.ListPartnerLocation = new List<CUSLocation>();
                result.ListProduct = new List<CUSProduct>();
                result.ListStock = new List<CUSLocation>();
                result.ListVehicle = new List<DTOCUSVehicle>();
                result.ListVendor = new List<CUSCustomer>();

                int iCus = -(int)SYSVarType.TypeOfCustomerCUS;
                int iVen = -(int)SYSVarType.TypeOfCustomerVEN;
                int iBoth = -(int)SYSVarType.TypeOfCustomerBOTH;
                string ViewAdmin = SYSViewCode.ViewAdmin.ToString();

                using (var model = new DataEntities())
                {
                    result.ListCustomer = model.CUS_Customer.Where(c => !c.IsSystem && (c.TypeOfCustomerID == iCus || c.TypeOfCustomerID == iBoth) && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => new CUSCustomer
                        {
                            ID = c.ID,
                            CustomerName = c.CustomerName,
                            Code = c.Code,
                            ShortName = c.ShortName
                        }).ToList();

                    var lstCusID = result.ListCustomer.Select(c => c.ID).ToList();

                    var lstGroupProduct = model.CUS_GroupOfProductInStock.Where(c => lstCusID.Contains(c.CUS_GroupOfProduct.CustomerID)).Select(c => new DTOPODCUSGroupOfProductInStock
                        {
                            ID = c.ID,
                            CustomerID = c.CUS_GroupOfProduct.CustomerID,
                            GroupOfProductCode = c.CUS_GroupOfProduct.Code,
                            GroupOfProductName = c.CUS_GroupOfProduct.GroupName,
                            GroupOfProductID = c.GroupOfProductID,
                            StockID = c.StockID
                        }).ToList();
                    result.ListGroupProduct.AddRange(lstGroupProduct);
                    var lstGroupProductID = lstGroupProduct.Select(c => c.GroupOfProductID).ToList();

                    var lstProduct = model.CUS_Product.Where(c => lstGroupProductID.Contains(c.GroupOfProductID)).Select(c => new CUSProduct
                        {
                            ID = c.ID,
                            GroupOfProductID = c.GroupOfProductID,
                            Code = c.Code,
                            ProductName = c.ProductName
                        }).ToList();
                    result.ListProduct.AddRange(lstProduct);

                    var lstStock = model.CUS_Location.Where(c => lstCusID.Contains(c.CustomerID) && c.CusPartID == null).Select(c => new CUSLocation
                        {
                            ID = c.ID,
                            Code = c.Code,
                            LocationName = c.LocationName,
                            CustomerID = c.CustomerID
                        }).ToList();
                    result.ListStock.AddRange(lstStock);

                    result.ListPartner = model.CUS_Partner.Where(c => lstCusID.Contains(c.CustomerID) && c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerDistributor).Select(c => new DTOCUSPartner
                         {
                             ID = c.ID,
                             PartnerCode = c.PartnerCode,
                             PartnerName = c.CAT_Partner.PartnerName,
                             CustomerID = c.CustomerID
                         }).ToList();
                    var lsrCusPartID = result.ListPartner.Select(c => c.ID).ToList();
                    result.ListPartnerLocation = model.CUS_Location.Where(c => lstCusID.Contains(c.CustomerID) && c.CusPartID > 0 && lsrCusPartID.Contains(c.CusPartID.Value)).Select(c => new CUSLocation
                       {
                           ID = c.ID,
                           Code = c.Code,
                           CusPartID = c.CusPartID,
                           LocationName = c.LocationName
                       }).ToList();

                    result.ListVendor = model.CUS_Customer.Where(c => !c.IsSystem && (c.TypeOfCustomerID == iVen || c.TypeOfCustomerID == iBoth) && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => new CUSCustomer
                    {
                        ID = c.ID,
                        CustomerName = c.CustomerName,
                        Code = c.Code,
                        ShortName = c.ShortName
                    }).ToList();

                    result.ListVendor.Insert(0, new CUSCustomer { ID = -1, CustomerName = "Xe nhà", Code = "Xe nhà", ShortName = "Xe nhà" });
                    var lstVenID = result.ListVendor.Select(c => c.ID).ToList();

                    result.ListVehicle = model.CUS_Vehicle.Where(c => lstVenID.Contains(c.CustomerID) && c.VehicleID > 2).Select(c => new DTOCUSVehicle
                        {
                            ID = c.ID,
                            VehicleID = c.VehicleID,
                            RegNo = c.CAT_Vehicle.RegNo,
                            CurrentVendorID = c.CustomerID
                        }).ToList();
                    var lstFLMVehicle = model.FLM_Asset.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfAssetID == -(int)SYSVarType.TypeOfAssetTruck && c.IsDisposal == false).Select(c => new DTOCUSVehicle
                        {
                            ID = c.ID,
                            VehicleID = c.VehicleID.Value,
                            RegNo = c.CAT_Vehicle.RegNo,
                            CurrentVendorID = -1
                        }).ToList();
                    result.ListVehicle.AddRange(lstFLMVehicle);

                    result.ListDriver = model.CUS_Driver.Where(c => lstVenID.Contains(c.CustomerID)).Select(c => new DTOCUSDriver
                        {
                            ID = c.ID,
                            DriverName = c.CAT_Driver.LastName + " " + c.CAT_Driver.FirstName,
                            LastName = c.CAT_Driver.LastName,
                            FirstName = c.CAT_Driver.FirstName,
                            CustomerID = c.CustomerID
                        }).ToList();

                    var lstFLMDriver = model.FLM_Driver.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.IsAssistant == false && c.IsUse).Select(c => new DTOCUSDriver
                        {
                            ID = c.ID,
                            DriverName = c.CAT_Driver.LastName + " " + c.CAT_Driver.FirstName,
                            LastName = c.CAT_Driver.LastName,
                            FirstName = c.CAT_Driver.FirstName,
                            CustomerID = -1
                        }).ToList();
                    result.ListDriver.AddRange(lstFLMDriver);

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
        public PODInputUpLoadOrder PODDIInput_UploadOrder_Get(int id)
        {
            try
            {
                PODInputUpLoadOrder result = new PODInputUpLoadOrder();

                using (var model = new DataEntities())
                {
                    if (id > 0)
                    {
                        result = model.OPS_DITOProduct.Where(c => c.ID == id).Select(c => new PODInputUpLoadOrder
                            {
                                ID = c.ID,
                                OrderID = c.OPS_DITOGroupProduct.ORD_GroupProduct.OrderID,
                                OrderCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code,
                                RequestDate = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.RequestDate,
                                CustomerID = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID,
                                CustomerCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                                CustomerName = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                                TransportModeID = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.TransportModeID,
                                TransportModeName = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CAT_TransportMode.Name,
                                StockID = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationFromID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationFromID.Value : -1,
                                StockCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationFromID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.Code : string.Empty,

                                LocationToID = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID.Value : -1,
                                LocationToCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.Code : string.Empty,
                                LocationToName = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationName : string.Empty,
                                LocationToAddress = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : string.Empty,

                                PartnerID = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID.Value : -1,
                                PartnerCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.PartnerCode : string.Empty,
                                PartnerName = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName : string.Empty,
                                PartnerAddress = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.CAT_Partner.Address : string.Empty,
                                GroupOfProductID = c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID.Value : -1,
                                GroupOfProductCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.Code : string.Empty,
                                GroupOfProductName = c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName : string.Empty,
                                ProductID = c.ORD_Product.ProductID,
                                ProductCode = c.ORD_Product.CUS_Product.Code,
                                ProductName = c.ORD_Product.CUS_Product.ProductName,
                                Ton = c.OPS_DITOGroupProduct.ORD_GroupProduct.Ton,
                                CBM = c.OPS_DITOGroupProduct.ORD_GroupProduct.CBM,
                                Quantity = c.OPS_DITOGroupProduct.ORD_GroupProduct.Quantity,
                                VehicleID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID.Value : -1,
                                VehicleCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                                VendorID = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID.Value : -1,
                                VendorCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.CUS_Customer.Code : "Xe nhà",
                                DriverID1 = c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverID1 > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverID1.Value : -1,
                                DriverName1 = c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverName1,
                                DriverID2 = c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverID2 > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverID2.Value : -1,
                                DriverName2 = c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverName2,
                                ETA = c.OPS_DITOGroupProduct.OPS_DITOMaster.ETA.Value,
                                ETD = c.OPS_DITOGroupProduct.OPS_DITOMaster.ETD.Value,
                                InvoiceNo = c.OPS_DITOGroupProduct.InvoiceNote,
                                Note1 = c.OPS_DITOGroupProduct.Note1,
                                ORDGroupID = c.OPS_DITOGroupProduct.OrderGroupProductID.Value,
                                ORDProductID = c.OrderProductID,
                                OPSGroupID = c.DITOGroupProductID,
                                OPSMasterID = c.OPS_DITOGroupProduct.DITOMasterID.Value,
                            }).FirstOrDefault();
                    }
                    else
                    {
                        string ViewAdmin = SYSViewCode.ViewAdmin.ToString();
                        int iCus = -(int)SYSVarType.TypeOfCustomerCUS;
                        int iVen = -(int)SYSVarType.TypeOfCustomerVEN;
                        int iBoth = -(int)SYSVarType.TypeOfCustomerBOTH;
                        result.ID = -1;
                        result.RequestDate = DateTime.Now.Date;
                        result.CustomerID = -1;
                        var objCus = model.CUS_Customer.Where(c => !c.IsSystem && (c.TypeOfCustomerID == iCus || c.TypeOfCustomerID == iBoth) && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID))).Select(c => new CUSCustomer
                        {
                            ID = c.ID,
                            CustomerName = c.CustomerName,
                            Code = c.Code,
                            ShortName = c.ShortName
                        }).FirstOrDefault();
                        if (objCus != null) result.CustomerID = objCus.ID;

                        result.StockID = -1;
                        var objStock = model.CUS_Location.Where(c => c.CustomerID == result.CustomerID && c.CusPartID == null).FirstOrDefault();
                        if (objStock != null) result.StockID = objStock.ID;

                        result.GroupOfProductID = -1;
                        var objGOP = model.CUS_GroupOfProductInStock.Where(c => c.CUS_GroupOfProduct.CustomerID == result.CustomerID && c.StockID == result.StockID).Select(c => new { c.GroupOfProductID, c.StockID, c.ID }).FirstOrDefault();
                        if (objGOP != null) result.GroupOfProductID = objGOP.GroupOfProductID;

                        result.ProductID = -1;
                        var objProduct = model.CUS_Product.Where(c => c.GroupOfProductID == result.GroupOfProductID).FirstOrDefault();
                        if (objProduct != null) result.ProductID = objProduct.ID;



                        result.PartnerID = -1;
                        var objPartner = model.CUS_Partner.Where(c => c.CustomerID == result.CustomerID && c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerDistributor).FirstOrDefault();
                        if (objPartner != null) result.PartnerID = objPartner.ID;

                        result.LocationToID = -1;
                        var objLocationTo = model.CUS_Location.Where(c => c.CustomerID == result.CustomerID && c.CusPartID == result.PartnerID).Select(c => new { c.ID, c.LocationID, c.Code }).FirstOrDefault();

                        if (objLocationTo != null) result.LocationToID = objLocationTo.ID;

                        result.VendorID = -1;
                        result.VehicleID = -1;
                        var objVehicle = model.FLM_Asset.Where(c => c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfAssetID == -(int)SYSVarType.TypeOfAssetTruck && c.IsDisposal == false).Select(c => new
                        {
                            ID = c.ID,
                            VehicleID = c.VehicleID.Value,
                            RegNo = c.CAT_Vehicle.RegNo,
                            CurrentVendorID = -1
                        }).FirstOrDefault();
                        if (objVehicle != null) result.VehicleID = objVehicle.VehicleID;

                        result.TransportModeID = HelperContract.GetTransportMode_First(model, -(int)SYSVarType.TransportModeFTL);

                        result.ORDGroupID = -1;
                        result.ORDProductID = -1;
                        result.OPSGroupID = -1;
                        result.OPSMasterID = -1;
                        result.OrderID = -1;
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
        public void PODDIInput_UploadOrder_Save(PODInputUpLoadOrder item)
        {
            try
            {
                int iLTL = -(int)SYSVarType.TransportModeLTL;
                int iFTL = -(int)SYSVarType.TransportModeFTL;

                using (var model = new DataEntities())
                {
                    #region check error

                    item.OrderCode = item.OrderCode.Trim();
                    item.DriverName1 = item.DriverName1.Trim();
                    item.DriverName2 = item.DriverName2.Trim();
                    item.InvoiceNo = item.InvoiceNo.Trim();

                    int orderIDUse = -1;

                    if (!StringHelper.IsValidCode(item.OrderCode))
                        throw FaultHelper.BusinessFault(null, null, "Mã có kí tự không hợp lệ");
                    if (item.VendorID < 0 && POD_FindFLMDriver(model, item.DriverName1) == null)
                        throw FaultHelper.BusinessFault(null, null, "Tài xế 1 xe nhà không tồn tại hoặc đã nghỉ");
                    if (!string.IsNullOrEmpty(item.DriverName2) && item.VendorID < 0 && POD_FindFLMDriver(model, item.DriverName2) == null)
                        throw FaultHelper.BusinessFault(null, null, "Tài xế 2 xe nhà không tồn tại hoặc đã nghỉ");
                    if (item.ETA == null)
                        item.ETA = item.ETD.AddHours(1);
                    var objCusGroupProduct = model.CUS_GroupOfProduct.Where(c => c.ID == item.GroupOfProductID).Select(c => new { c.ID, c.PriceOfGOPID }).FirstOrDefault();
                    if (objCusGroupProduct == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy nhóm hàng");
                    var objCusProduct = model.CUS_Product.Where(c => c.ID == item.ProductID).Select(c => new { c.ID, c.PackingID, c.CAT_Packing.TypeOfPackageID }).FirstOrDefault();
                    if (objCusProduct == null)
                        throw FaultHelper.BusinessFault(null, null, "Không tìm thấy hàng hóa");

                    var objORDOrderOld = model.ORD_Order.FirstOrDefault(c => c.Code.ToLower() == item.OrderCode.ToLower() && c.CustomerID == item.CustomerID);
                    if (objORDOrderOld != null)
                    {
                        orderIDUse = objORDOrderOld.ID;
                        if (item.OrderID > 0 && objORDOrderOld.ID != item.OrderID)
                        {
                            item.OPSGroupID = -1;
                            item.OPSMasterID = -1;
                            item.ORDGroupID = -1;
                            item.ORDProductID = -1;
                        }
                    }


                    #endregion

                    #region ORD_Order
                    var objORDOrder = model.ORD_Order.FirstOrDefault(c => c.ID == orderIDUse);
                    if (objORDOrder == null)
                    {
                        objORDOrder = new ORD_Order();
                        objORDOrder.CreatedBy = Account.UserName;
                        objORDOrder.CreatedDate = DateTime.Now;
                        objORDOrder.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderReceived;
                        objORDOrder.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanComplete;

                        objORDOrder.CustomerID = item.CustomerID;
                        objORDOrder.SYSCustomerID = Account.SYSCustomerID;
                        objORDOrder.IsOPS = true;
                        model.ORD_Order.Add(objORDOrder);
                    }
                    else
                    {
                        objORDOrder.ModifiedBy = Account.UserName;
                        objORDOrder.ModifiedDate = DateTime.Now;
                    }

                    objORDOrder.TypeOfOrderID = -(int)SYSVarType.TypeOfOrderDirect;// loai don hang thuong
                    objORDOrder.AllowCoLoad = true;
                    objORDOrder.Code = item.OrderCode;
                    objORDOrder.ServiceOfOrderID = HelperContract.GetServiceOfOrder_First(model, -(int)SYSVarType.ServiceOfOrderLocal);// dich vu noi dia
                    objORDOrder.TransportModeID = item.TransportModeID;
                    objORDOrder.TypeOfContractID = -(int)SYSVarType.TypeOfContractMain;// loai hop dong chính thức
                    objORDOrder.ContractID = null;
                    objORDOrder.RequestDate = item.RequestDate;
                    objORDOrder.ETD = item.ETD;
                    objORDOrder.ETA = item.ETA;
                    objORDOrder.DateConfig = item.RequestDate;
                    objORDOrder.IsClosed = false;
                    objORDOrder.IsHot = false;
                    #endregion

                    #region ORD_GroupProduct
                    var objORDGroup = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == item.ORDGroupID);
                    if (objORDGroup == null)
                    {
                        objORDGroup = new ORD_GroupProduct();
                        objORDGroup.CreatedBy = Account.UserName;
                        objORDGroup.CreatedDate = DateTime.Now;
                        objORDGroup.ORD_Order = objORDOrder;
                        model.ORD_GroupProduct.Add(objORDGroup);
                    }
                    else
                    {
                        objORDGroup.ModifiedBy = Account.UserName;
                        objORDGroup.ModifiedDate = DateTime.Now;
                    }
                    objORDGroup.GroupOfProductID = item.GroupOfProductID;
                    objORDGroup.Ton = item.Ton;
                    objORDGroup.CBM = item.CBM;
                    objORDGroup.Quantity = item.Quantity;
                    objORDGroup.PriceOfGOPID = objCusGroupProduct.PriceOfGOPID;
                    objORDGroup.PackingID = objCusProduct.PackingID;
                    objORDGroup.LocationFromID = item.StockID;
                    objORDGroup.LocationToID = item.LocationToID;
                    objORDGroup.PartnerID = item.PartnerID;
                    objORDGroup.ETA = item.ETA;
                    objORDGroup.ETD = item.ETD;

                    #endregion

                    #region ORD_Product
                    var objORDProduct = model.ORD_Product.FirstOrDefault(c => c.ID == item.ORDProductID);
                    if (objORDProduct == null)
                    {
                        objORDProduct = new ORD_Product();
                        objORDProduct.CreatedBy = Account.UserName;
                        objORDProduct.CreatedDate = DateTime.Now;
                        objORDProduct.ORD_GroupProduct = objORDGroup;
                        model.ORD_Product.Add(objORDProduct);
                    }
                    else
                    {
                        objORDProduct.ModifiedBy = Account.UserName;
                        objORDProduct.ModifiedDate = DateTime.Now;
                    }

                    objORDProduct.ProductID = item.ProductID;
                    objORDProduct.PackingID = objCusProduct.PackingID;
                    switch (objCusProduct.TypeOfPackageID)
                    {
                        default:
                            break;
                        case -(int)SYSVarType.TypeOfPackingGOPTon:
                            objORDProduct.ExchangeTon = 1;
                            objORDProduct.ExchangeCBM = 0;
                            objORDProduct.ExchangeQuantity = 0;
                            objORDProduct.Quantity = item.Ton;
                            break;
                        case -(int)SYSVarType.TypeOfPackingGOPCBM:
                            objORDProduct.ExchangeTon = 0;
                            objORDProduct.ExchangeCBM = 1;
                            objORDProduct.ExchangeQuantity = 0;
                            objORDProduct.Quantity = item.CBM;
                            break;
                        case -(int)SYSVarType.TypeOfPackingGOPTU:
                            objORDProduct.ExchangeTon = 0;
                            objORDProduct.ExchangeCBM = 0;
                            objORDProduct.ExchangeQuantity = 1;
                            objORDProduct.Quantity = item.Quantity;
                            break;
                    }
                    #endregion

                    #region OPS_DITOMaster
                    var objOPSMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.OPSMasterID);
                    if (objOPSMaster == null)
                    {
                        objOPSMaster = new OPS_DITOMaster();
                        objOPSMaster.CreatedBy = Account.UserName;
                        objOPSMaster.CreatedDate = DateTime.Now;
                        objOPSMaster.SYSCustomerID = Account.SYSCustomerID;
                        objOPSMaster.Code = string.Empty;
                        objOPSMaster.IsRouteVendor = false;
                        objOPSMaster.IsRouteCustomer = false;
                        objOPSMaster.IsLoading = false;
                        objOPSMaster.IsHot = false;
                        objOPSMaster.IsBidding = false;
                        objOPSMaster.AllowCoLoad = true;
                        model.OPS_DITOMaster.Add(objOPSMaster);
                    }
                    else
                    {
                        objOPSMaster.ModifiedBy = Account.UserName;
                        objOPSMaster.ModifiedDate = DateTime.Now;
                    }

                    objOPSMaster.VehicleID = item.VehicleID;
                    if (item.VendorID > 0)
                    {
                        objOPSMaster.VendorOfVehicleID = item.VendorID;
                        objOPSMaster.DriverName1 = item.DriverName1;
                        objOPSMaster.DriverName2 = item.DriverName2;
                        if (string.IsNullOrEmpty(item.DriverName1)) objOPSMaster.DriverName1 = null;
                        if (string.IsNullOrEmpty(item.DriverName2)) objOPSMaster.DriverName2 = null;
                    }
                    else
                    {
                        objOPSMaster.VendorOfVehicleID = null;
                        objOPSMaster.DriverID1 = POD_FindFLMDriver(model, item.DriverName1);
                        objOPSMaster.DriverID2 = POD_FindFLMDriver(model, item.DriverName2);
                        objOPSMaster.DriverName1 = null;
                        objOPSMaster.DriverName2 = null;
                        if (objOPSMaster.DriverID1 != null) objOPSMaster.DriverName1 = item.DriverName1;
                        if (objOPSMaster.DriverID2 != null) objOPSMaster.DriverName2 = item.DriverName2;
                    }

                    objOPSMaster.SortOrder = 1;
                    objOPSMaster.ETA = item.ETA;
                    objOPSMaster.ETD = item.ETD;
                    objOPSMaster.ATA = item.ETA;
                    objOPSMaster.ATD = item.ETD;
                    objOPSMaster.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterReceived;
                    objOPSMaster.TypeOfDITOMasterID = -(int)SYSVarType.TypeOfDITONormal;
                    objOPSMaster.TransportModeID = item.TransportModeID;
                    objOPSMaster.TypeOfOrderID = -(int)SYSVarType.TypeOfOrderDirect;
                    #endregion

                    #region OPS_DITOGroupProduct
                    var objOPSGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.OPSGroupID);
                    if (objOPSGroup == null)
                    {
                        objOPSGroup = new OPS_DITOGroupProduct();
                        objOPSGroup.CreatedBy = Account.UserName;
                        objOPSGroup.CreatedDate = DateTime.Now;
                        objOPSGroup.OPS_DITOMaster = objOPSMaster;
                        objOPSGroup.ORD_GroupProduct = objORDGroup;

                        objOPSGroup.IsInput = true;
                        objOPSGroup.IsClosed = false;
                        objOPSGroup.HasUpload = false;
                        objOPSGroup.IsSplit = false;

                        model.OPS_DITOGroupProduct.Add(objOPSGroup);
                    }

                    objOPSGroup.Ton = item.Ton;
                    objOPSGroup.CBM = item.CBM;
                    objOPSGroup.Quantity = item.Quantity;

                    objOPSGroup.TonBBGN = item.Ton;
                    objOPSGroup.CBMBBGN = item.CBM;
                    objOPSGroup.QuantityBBGN = item.Quantity;

                    objOPSGroup.TonTranfer = item.Ton;
                    objOPSGroup.CBMTranfer = item.CBM;
                    objOPSGroup.QuantityTranfer = item.Quantity;

                    objOPSGroup.QuantityLoading = item.Quantity;

                    objOPSGroup.TonReturn = 0;
                    objOPSGroup.CBMReturn = 0;
                    objOPSGroup.QuantityReturn = 0;

                    objOPSGroup.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                    objOPSGroup.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;

                    objOPSGroup.InvoiceNote = item.InvoiceNo;
                    objOPSGroup.Note1 = item.Note1;
                    #endregion

                    #region OPS_DITOProduct
                    var objOPSProduct = model.OPS_DITOProduct.FirstOrDefault(c => c.ID == item.ID);
                    if (objOPSProduct == null)
                    {
                        objOPSProduct = new OPS_DITOProduct();
                        objOPSProduct.CreatedBy = Account.UserName;
                        objOPSProduct.CreatedDate = DateTime.Now;

                        objOPSProduct.ORD_Product = objORDProduct;
                        objOPSProduct.OPS_DITOGroupProduct = objOPSGroup;

                        model.OPS_DITOProduct.Add(objOPSProduct);
                    }
                    else
                    {
                        objOPSProduct.ModifiedBy = Account.UserName;
                        objOPSProduct.ModifiedDate = DateTime.Now;
                    }
                    objOPSProduct.Quantity = objORDProduct.Quantity;
                    objOPSProduct.QuantityTranfer = objORDProduct.Quantity;
                    objOPSProduct.QuantityBBGN = objORDProduct.Quantity;
                    objOPSProduct.QuantityReturn = 0;
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
                throw FaultHelper.BusinessFault(ex);
            }
        }

        private int? POD_FindFLMDriver(DataEntities model, string driverName)
        {
            var obj = model.FLM_Driver.FirstOrDefault(c => c.SYSCustomerID == Account.SYSCustomerID && (c.CAT_Driver.LastName + " " + c.CAT_Driver.FirstName).ToLower().Trim() == driverName.ToLower().Trim());
            if (obj != null)
                return obj.ID;
            return null;
        }

        public SYSExcel PODDIInput_UploadOrder_ExcelInit(int functionid, string functionkey, bool isreload, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    int iLTL = -(int)SYSVarType.TransportModeLTL;
                    int iFTL = -(int)SYSVarType.TransportModeFTL;

                    dtFrom = dtFrom.Date;
                    dtTo = dtTo.AddDays(1).Date;

                    var result = default(SYSExcel);
                    var id = HelperExcel.GetLastID(model, functionid, functionkey);
                    if (id < 1 || isreload == true)
                    {
                        #region lấy dữ liệu
                        var data = model.OPS_DITOProduct.Where(c => c.OPS_DITOGroupProduct.DITOMasterID > 0 && c.OPS_DITOGroupProduct.OrderGroupProductID > 0 && c.OPS_DITOGroupProduct.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID &&
                            c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.RequestDate >= dtFrom && c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.RequestDate < dtTo &&
                        (c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.TransportModeID == iFTL || c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.TransportModeID == iLTL)).Select(c => new
                        {
                            OrderID = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.ID,
                            OrderCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.Code,
                            RequestDate = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.RequestDate,
                            CustomerCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                            CustomerName = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,
                            TransportModeName = c.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CAT_TransportMode.Code,
                            StockCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationFromID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location.Code : string.Empty,

                            LocationToCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.Code : string.Empty,
                            LocationToName = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.LocationName : string.Empty,
                            LocationToAddress = c.OPS_DITOGroupProduct.ORD_GroupProduct.LocationToID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : string.Empty,

                            PartnerCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.PartnerCode : string.Empty,
                            PartnerName = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName : string.Empty,
                            PartnerAddress = c.OPS_DITOGroupProduct.ORD_GroupProduct.PartnerID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_Partner.CAT_Partner.Address : string.Empty,

                            GroupOfProductCode = c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.Code : string.Empty,
                            GroupOfProductName = c.OPS_DITOGroupProduct.ORD_GroupProduct.GroupOfProductID > 0 ? c.OPS_DITOGroupProduct.ORD_GroupProduct.CUS_GroupOfProduct.GroupName : string.Empty,

                            ProductCode = c.ORD_Product.CUS_Product.Code,
                            ProductName = c.ORD_Product.CUS_Product.ProductName,
                            Ton = c.OPS_DITOGroupProduct.ORD_GroupProduct.Ton,
                            CBM = c.OPS_DITOGroupProduct.ORD_GroupProduct.CBM,
                            Quantity = c.OPS_DITOGroupProduct.ORD_GroupProduct.Quantity,
                            VehicleCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                            VendorCode = c.OPS_DITOGroupProduct.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOGroupProduct.OPS_DITOMaster.CUS_Customer.Code : "Xe nhà",
                            DriverName1 = c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverName1,
                            DriverName2 = c.OPS_DITOGroupProduct.OPS_DITOMaster.DriverName2,
                            ETA = c.OPS_DITOGroupProduct.OPS_DITOMaster.ATA,
                            ETD = c.OPS_DITOGroupProduct.OPS_DITOMaster.ATD,
                            InvoiceNo = c.OPS_DITOGroupProduct.InvoiceNote,
                            Note1 = c.OPS_DITOGroupProduct.Note1,

                        }).OrderBy(c => c.OrderID).ToList();
                        #endregion

                        List<Worksheet> lstWorkSheet = HelperExcel.GetWorksheetByID(model, id);
                        var ws = lstWorkSheet[0];
                        ws.Rows.Clear();

                        double[] arrColumnWidth = new double[24];
                        arrColumnWidth[0] = 30;
                        for (int i = 1; i < 24; i++)
                        {
                            arrColumnWidth[i] = 100;
                        }
                        ws.Columns = HelperExcel.NewColumns(arrColumnWidth);

                        var cells = new List<Cell>();
                        cells.Add(HelperExcel.NewCell(0, "STT", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(1, "Mã đơn hàng", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(2, "Mã KH", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(3, "Ngày gửi y/c", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(4, "Loại v/c", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(5, "Kho", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(6, "Mã nhóm hàng", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(7, "Nhóm hàng", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(8, "Mã hàng", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(9, "Hàng hóa", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(10, "Nhà phân phối", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(11, "Mã điểm giao", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(12, "Địa chỉ đ.giao", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(13, "ATD", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(14, "ATA", HelperExcel.ColorWhite, HelperExcel.ColorGreen, "@"));
                        cells.Add(HelperExcel.NewCell(15, "Số lượng", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(16, "Tấn", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(17, "CBM", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(18, "Nhà vận tải", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(19, "Số xe", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(20, "Tài xế 1", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(21, "Tài xế 2", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(22, "Số chứng từ", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        cells.Add(HelperExcel.NewCell(23, "Loại chứng từ", HelperExcel.ColorWhite, HelperExcel.ColorGreen));
                        ws.Rows.Add(HelperExcel.NewRow(ws.Rows.Count, cells));
                        ws.MergedCells = new List<string>();
                        int stt = 1;
                        foreach (var item in data)
                        {
                            cells = new List<Cell>();
                            cells.Add(HelperExcel.NewCell(stt));
                            cells.Add(HelperExcel.NewCell(item.OrderCode));
                            cells.Add(HelperExcel.NewCell(item.CustomerCode));
                            cells.Add(HelperExcel.NewCell(-1, item.RequestDate.ToString("dd/MM/yyyy"), HelperExcel.ColorBlack, HelperExcel.ColorWhite, "@"));
                            cells.Add(HelperExcel.NewCell(item.TransportModeName));
                            cells.Add(HelperExcel.NewCell(item.StockCode));
                            cells.Add(HelperExcel.NewCell(item.GroupOfProductCode));
                            cells.Add(HelperExcel.NewCell(item.GroupOfProductName));
                            cells.Add(HelperExcel.NewCell(item.ProductCode));
                            cells.Add(HelperExcel.NewCell(item.ProductName));
                            cells.Add(HelperExcel.NewCell(item.PartnerCode));
                            cells.Add(HelperExcel.NewCell(item.LocationToCode));
                            cells.Add(HelperExcel.NewCell(item.LocationToAddress));
                            cells.Add(HelperExcel.NewCell(-1, item.ETD == null ? string.Empty : item.ETD.Value.ToString("dd/MM/yyyy"), HelperExcel.ColorBlack, HelperExcel.ColorWhite, "@"));
                            cells.Add(HelperExcel.NewCell(-1, item.ETA == null ? string.Empty : item.ETA.Value.ToString("dd/MM/yyyy"), HelperExcel.ColorBlack, HelperExcel.ColorWhite, "@"));
                            cells.Add(HelperExcel.NewCell(-1, item.Quantity, HelperExcel.ColorBlack, HelperExcel.ColorWhite, HelperExcel.FormatNumber6));
                            cells.Add(HelperExcel.NewCell(-1, item.Ton, HelperExcel.ColorBlack, HelperExcel.ColorWhite, HelperExcel.FormatNumber6));
                            cells.Add(HelperExcel.NewCell(-1, item.CBM, HelperExcel.ColorBlack, HelperExcel.ColorWhite, HelperExcel.FormatNumber6));

                            cells.Add(HelperExcel.NewCell(item.VendorCode));
                            cells.Add(HelperExcel.NewCell(item.VehicleCode));
                            cells.Add(HelperExcel.NewCell(item.DriverName1));
                            cells.Add(HelperExcel.NewCell(item.DriverName2));
                            cells.Add(HelperExcel.NewCell(item.InvoiceNo));
                            cells.Add(HelperExcel.NewCell(item.Note1));
                            ws.Rows.Add(HelperExcel.NewRow(ws.Rows.Count, cells));
                            stt++;
                        }

                        result = HelperExcel.GetByKey(model, functionid, functionkey);
                        result.Data = Newtonsoft.Json.JsonConvert.SerializeObject(lstWorkSheet);
                        result = HelperExcel.Save(model, Account, result);
                    }
                    else
                    {
                        result = HelperExcel.GetByID(model, id);
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

        public Row PODDIInput_UploadOrder_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    int rowStart = 1;
                    int colData = 24;
                    int colCheckChange = colData++;
                    int colCheckNote = colData++;
                    int colOrder = colData++;
                    int colCus = colData++;
                    int colTransport = colData++;
                    int colStock = colData++;
                    int colGroupProduct = colData++;
                    int colProduct = colData++;
                    int colPacking = colData++;
                    int colPakage = colData++;
                    int colPriceOfGop = colData++;
                    int colPartner = colData++;
                    int colLocation = colData++;
                    int colVendor = colData++;
                    int colRegNo = colData++;
                    int colDriver1 = colData++;
                    int colDriver2 = colData++;

                    int iLTL = -(int)SYSVarType.TransportModeLTL;
                    int iFTL = -(int)SYSVarType.TransportModeFTL;
                    int iCus = -(int)SYSVarType.TypeOfCustomerCUS;
                    int iVen = -(int)SYSVarType.TypeOfCustomerVEN;
                    int iBoth = -(int)SYSVarType.TypeOfCustomerBOTH;
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();

                    // var lstCustomer = model.CUS_Customer.Where(c => !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID)) && (c.TypeOfCustomerID == iCus || c.TypeOfCustomerID == iBoth)).Select(c => new { c.ID, c.Code }).ToList();
                    var lstCustomer = model.CUS_Customer.Where(c => !c.IsSystem && (c.TypeOfCustomerID == iCus || c.TypeOfCustomerID == iBoth)).Select(c => new { c.ID, c.Code }).ToList();
                    var lstOrder = model.ORD_Order.Where(c => (c.CAT_TransportMode.TransportModeID == iFTL || c.CAT_TransportMode.TransportModeID == iLTL)).Select(c => new { c.ID, c.Code, c.CustomerID }).ToList();
                    var lstTransport = model.CAT_TransportMode.Where(c => (c.TransportModeID == iLTL || c.TransportModeID == iFTL)).Select(c => new { c.ID, c.Code, c.TransportModeID }).ToList();
                    var lstStock = model.CUS_Location.Where(c => c.CusPartID == null).Select(c => new { c.ID, c.Code, c.CustomerID }).ToList();
                    var lstGOP = model.CUS_GroupOfProduct.Select(c => new { c.ID, c.Code, c.CustomerID, c.GroupName, c.PriceOfGOPID }).ToList();
                    var lstGOPInStock = model.CUS_GroupOfProductInStock.Select(c => new { c.ID, c.StockID, c.GroupOfProductID }).ToList();
                    var lstProduct = model.CUS_Product.Select(c => new { c.ID, c.Code, c.GroupOfProductID, c.PackingID, c.CAT_Packing.TypeOfPackageID }).ToList();
                    var lstPartner = model.CUS_Partner.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerDistributor).Select(c => new { c.ID, c.PartnerCode, c.CustomerID }).ToList();
                    var lstLocation = model.CUS_Location.Where(c => c.CusPartID > 0).Select(c => new { c.ID, c.Code, c.CusPartID, c.CustomerID }).ToList();

                    // var lstVendor = model.CUS_Customer.Where(c => !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID)) && (c.TypeOfCustomerID == iVen || c.TypeOfCustomerID == iBoth)).Select(c => new { c.ID, c.Code }).ToList();
                    var lstVendor = model.CUS_Customer.Where(c => !c.IsSystem && (c.TypeOfCustomerID == iVen || c.TypeOfCustomerID == iBoth)).Select(c => new { c.ID, c.Code }).ToList();
                    var lstFLMVehicle = model.FLM_Asset.Where(c => c.VehicleID > 0 && c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfAssetID == -(int)SYSVarType.TypeOfAssetTruck && c.IsDisposal == false).Select(c => new { c.CAT_Vehicle.RegNo, c.VehicleID, c.ID }).ToList();
                    var lstVendorVehicle = model.CUS_Vehicle.Where(c => c.CAT_Vehicle.TypeOfVehicleID == -(int)SYSVarType.TypeOfVehicleTruck).Select(c => new { c.CustomerID, c.VehicleID, c.CAT_Vehicle.RegNo }).ToList();
                    var lstDriver = model.FLM_Driver.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new { c.CAT_Driver.LastName, c.CAT_Driver.FirstName, c.ID }).ToList();

                    var lstWorksheet = HelperExcel.GetWorksheetByID(model, id);
                    var ws = lstWorksheet[0];
                    var result = default(Row);

                    bool flag = true;
                    int OrderID = -1, CustomerID = -1;
                    int TransportID = -1, StockID = -1, PriceOfGOPID = -1;
                    int GroupProductID = -1, ProductID = -1, PartnerID = -1, LocationToID = -1, VehicleID = -1;
                    int? VendorID = null; string vendorHome = "Xe nhà";

                    int failMax = 2;
                    int failCurrent = 0;

                    var checkRow = ws.Rows.FirstOrDefault(c => c.Index == row);
                    if (checkRow == null)
                    {
                        checkRow = HelperExcel.NewRow(row, cells);
                        ws.Rows.Add(checkRow);
                    }

                    failCurrent = 0;
                    colData = 1;
                    int indexError = 0;

                    if (checkRow != null)
                    {
                        flag = true;
                        OrderID = -1;
                        CustomerID = -1;
                        TransportID = -1;
                        StockID = -1;
                        GroupProductID = -1;
                        ProductID = -1;
                        PartnerID = -1;
                        LocationToID = -1;
                        VehicleID = -1;
                        VendorID = null;

                        checkRow.Cells = cells;
                        colData = 1;
                        string dataOrderCode = HelperExcel.GetString(checkRow, colData++);
                        string dataCustomerCode = HelperExcel.GetString(checkRow, colData++);
                        string dataRequestDate = HelperExcel.GetString(checkRow, colData++);
                        string dataTransport = HelperExcel.GetString(checkRow, colData++);
                        string dataStockCode = HelperExcel.GetString(checkRow, colData++);
                        string dataGOPCode = HelperExcel.GetString(checkRow, colData++);
                        colData++;//7
                        string dataProductCode = HelperExcel.GetString(checkRow, colData++);
                        colData++;//9
                        string dataPartnerCode = HelperExcel.GetString(checkRow, colData++);
                        string dataLocationToCode = HelperExcel.GetString(checkRow, colData++);
                        colData++;//12
                        string dataETD = HelperExcel.GetString(checkRow, colData++);
                        string dataETA = HelperExcel.GetString(checkRow, colData++);
                        string dataQuantity = HelperExcel.GetString(checkRow, colData++);
                        string dataTon = HelperExcel.GetString(checkRow, colData++);
                        string dataCBM = HelperExcel.GetString(checkRow, colData++);
                        string dataVendorCode = HelperExcel.GetString(checkRow, colData++);
                        string dataVehicleCode = HelperExcel.GetString(checkRow, colData++);
                        string dataDriverName1 = HelperExcel.GetString(checkRow, colData++);
                        string dataDriverName2 = HelperExcel.GetString(checkRow, colData++);
                        string dataInvoiceNo = HelperExcel.GetString(checkRow, colData++);
                        string dataNote1 = HelperExcel.GetString(checkRow, colData++);

                        indexError = 0;
                        if (flag)
                            flag = HelperExcel.Valid(dataOrderCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true, 50);
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataCustomerCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag && lstCustomer.Count(c => c.Code == dataCustomerCode) == 0)
                        {
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                            flag = false;
                        }
                        if (flag)
                        {
                            CustomerID = -1;
                            OrderID = -1;
                            var check = lstCustomer.Where(c => c.Code == dataCustomerCode).FirstOrDefault();
                            if (check != null)
                                CustomerID = check.ID;
                            var checkOrder = lstOrder.Where(c => c.CustomerID == CustomerID && dataOrderCode.ToLower() == c.Code.ToLower()).FirstOrDefault();
                            if (checkOrder != null) OrderID = checkOrder.ID;

                            HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colOrder, OrderID.ToString());
                            HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colCus, CustomerID.ToString());
                        }
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataRequestDate, HelperExcel.ValidType.DateTime, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataTransport, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag)
                        {
                            var check = lstTransport.Where(c => c.Code.ToLower() == dataTransport.ToLower()).FirstOrDefault();
                            if (check == null || (check != null && check.ID != iLTL && check.ID != iFTL))
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            else
                            {
                                TransportID = check.ID;
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colTransport, TransportID.ToString());
                            }
                        }
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataStockCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag)
                        {
                            var check = lstStock.Where(c => c.CustomerID == CustomerID && c.Code.ToLower() == dataStockCode.ToLower()).FirstOrDefault();
                            if (check == null)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            else
                            {
                                StockID = check.ID;
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colStock, StockID.ToString());
                            }
                        }

                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataGOPCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag)
                        {
                            var check = lstGOP.Where(c => c.CustomerID == CustomerID && c.Code.ToLower() == dataGOPCode.ToLower()).FirstOrDefault();
                            if (check == null)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            else
                            {
                                GroupProductID = check.ID;
                                PriceOfGOPID = check.PriceOfGOPID;
                            }
                        }
                        indexError++;
                        if (flag)
                        {
                            var checkInStock = lstGOPInStock.Where(c => c.StockID == StockID && c.GroupOfProductID == GroupProductID).FirstOrDefault();
                            if (checkInStock == null)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            else
                            {
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colGroupProduct, GroupProductID.ToString());
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colPriceOfGop, PriceOfGOPID.ToString());
                            }
                        }

                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataProductCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag)
                        {
                            var check = lstProduct.Where(c => c.GroupOfProductID == GroupProductID && c.Code.ToLower() == dataProductCode.ToLower()).FirstOrDefault();
                            if (check == null)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            else
                            {
                                ProductID = check.ID;
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colProduct, ProductID.ToString());
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colPacking, check.PackingID.ToString());
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colPakage, check.TypeOfPackageID.ToString());
                            }
                        }

                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataPartnerCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag && lstPartner.Count(c => c.CustomerID == CustomerID && c.PartnerCode.ToLower() == dataPartnerCode.ToLower()) == 0)
                        {
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                            flag = false;
                        }
                        if (flag)
                        {
                            PartnerID = -1;
                            var check = lstPartner.Where(c => c.CustomerID == CustomerID && c.PartnerCode.ToLower() == dataPartnerCode.ToLower()).FirstOrDefault();
                            if (check != null)
                                PartnerID = check.ID;
                            HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colPartner, PartnerID.ToString());
                        }
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataLocationToCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag && lstLocation.Count(c => c.CustomerID == CustomerID && c.CusPartID == PartnerID && c.Code.ToLower() == dataLocationToCode.ToLower()) == 0)
                        {
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                            flag = false;
                        }
                        if (flag)
                        {
                            LocationToID = -1;
                            var check = lstLocation.Where(c => c.CustomerID == CustomerID && c.CusPartID == PartnerID && c.Code.ToLower() == dataLocationToCode.ToLower()).FirstOrDefault();
                            if (check != null)
                                LocationToID = check.ID;
                            HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colLocation, LocationToID.ToString());
                        }
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataETD, HelperExcel.ValidType.DateTime, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataETA, HelperExcel.ValidType.DateTime, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);

                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataQuantity, HelperExcel.ValidType.Double, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true, 0, 0);
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataTon, HelperExcel.ValidType.Double, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true, 0, 0);
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataCBM, HelperExcel.ValidType.Double, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true, 0, 0);
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataVendorCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag)
                        {
                            if (dataVendorCode.ToLower() == vendorHome.ToLower())
                            {
                                VendorID = -1;
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colVendor, VendorID.ToString());
                            }
                            else
                            {

                                var check = lstVendor.Where(c => c.Code == dataVendorCode).FirstOrDefault();
                                if (check == null)
                                {
                                    HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                    flag = false;
                                }
                                else
                                {
                                    VendorID = check.ID;
                                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colVendor, VendorID.ToString());
                                }
                            }
                        }

                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataVehicleCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag && VendorID == -1 && lstFLMVehicle.Where(c => c.RegNo.ToUpper() == dataVehicleCode.ToUpper()).Count() == 0)
                        {
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                            flag = false;
                        }
                        indexError++;
                        if (flag && VendorID > 0 && lstVendorVehicle.Where(c => c.RegNo.ToUpper() == dataVehicleCode.ToUpper()).Count() == 0)
                        {
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                            flag = false;
                        }
                        if (flag)
                        {
                            VehicleID = -1;
                            if (VendorID == -1)
                            {
                                var check = lstFLMVehicle.FirstOrDefault(c => c.RegNo.ToUpper() == dataVehicleCode.ToUpper());
                                if (check != null) VehicleID = check.VehicleID.Value;
                            }
                            else
                            {
                                var check = lstVendorVehicle.FirstOrDefault(c => c.RegNo.ToUpper() == dataVehicleCode.ToUpper());
                                if (check != null) VehicleID = check.VehicleID;
                            }
                            HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colRegNo, VehicleID.ToString());
                        }
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataDriverName1, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                        indexError++;
                        if (flag && VendorID == -1 && lstDriver.Count(c => (c.LastName + " " + c.FirstName).Trim().ToLower() == dataDriverName1.ToLower()) == 0)
                        {
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                            flag = false;
                        }
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataDriverName2, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, false);
                        indexError++;
                        if (flag && VendorID == -1 && !string.IsNullOrEmpty(dataDriverName2) && lstDriver.Count(c => (c.LastName + " " + c.FirstName).Trim().ToLower() == dataDriverName2.ToLower()) == 0)
                        {
                            HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                            flag = false;
                        }
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataInvoiceNo, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, false, 1000);
                        indexError++;
                        if (flag)
                            flag = HelperExcel.Valid(dataNote1, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, false, 500);

                        HelperExcel.SaveData(model, id, lstWorksheet);
                        HelperExcel.ClearData(checkRow, colCheckChange);
                        result = checkRow;
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

        public SYSExcel PODDIInput_UploadOrder_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var model = new DataEntities())
                {

                    model.EventAccount = Account; model.EventRunning = true;

                    int rowStart = 1;
                    int colData = 24;
                    int colCheckChange = colData++;
                    int colCheckNote = colData++;
                    int colOrder = colData++;
                    int colCus = colData++;
                    int colTransport = colData++;
                    int colStock = colData++;
                    int colGroupProduct = colData++;
                    int colProduct = colData++;
                    int colPacking = colData++;
                    int colPakage = colData++;
                    int colPriceOfGop = colData++;
                    int colPartner = colData++;
                    int colLocation = colData++;
                    int colVendor = colData++;
                    int colRegNo = colData++;
                    int colDriver1 = colData++;
                    int colDriver2 = colData++;

                    int iLTL = -(int)SYSVarType.TransportModeLTL;
                    int iFTL = -(int)SYSVarType.TransportModeFTL;
                    int iCus = -(int)SYSVarType.TypeOfCustomerCUS;
                    int iVen = -(int)SYSVarType.TypeOfCustomerVEN;
                    int iBoth = -(int)SYSVarType.TypeOfCustomerBOTH;
                    string ViewAdmin = SYSViewCode.ViewAdmin.ToString();

                    #region lấy dữ liệu check
                    //var lstCustomer = model.CUS_Customer.Where(c => !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID)) && (c.TypeOfCustomerID == iCus || c.TypeOfCustomerID == iBoth)).Select(c => new { c.ID, c.Code }).ToList();
                    var lstCustomer = model.CUS_Customer.Where(c => !c.IsSystem && (c.TypeOfCustomerID == iCus || c.TypeOfCustomerID == iBoth)).Select(c => new { c.ID, c.Code }).ToList();
                    var lstOrder = model.ORD_Order.Where(c => (c.CAT_TransportMode.TransportModeID == iFTL || c.CAT_TransportMode.TransportModeID == iLTL)).Select(c => new { c.ID, c.Code, c.CustomerID }).ToList();
                    var lstTransport = model.CAT_TransportMode.Where(c => (c.TransportModeID == iLTL || c.TransportModeID == iFTL)).Select(c => new { c.ID, c.Code, c.TransportModeID }).ToList();
                    var lstStock = model.CUS_Location.Where(c => c.CusPartID == null).Select(c => new { c.ID, c.Code, c.CustomerID }).ToList();
                    var lstGOP = model.CUS_GroupOfProduct.Select(c => new { c.ID, c.Code, c.CustomerID, c.GroupName, c.PriceOfGOPID }).ToList();
                    var lstGOPInStock = model.CUS_GroupOfProductInStock.Select(c => new { c.ID, c.StockID, c.GroupOfProductID }).ToList();
                    var lstProduct = model.CUS_Product.Select(c => new { c.ID, c.Code, c.GroupOfProductID, c.PackingID, c.CAT_Packing.TypeOfPackageID }).ToList();
                    var lstPartner = model.CUS_Partner.Where(c => c.CAT_Partner.TypeOfPartnerID == -(int)SYSVarType.TypeOfPartnerDistributor).Select(c => new { c.ID, c.PartnerCode, c.CustomerID }).ToList();
                    var lstLocation = model.CUS_Location.Where(c => c.CusPartID > 0).Select(c => new { c.ID, c.Code, c.CusPartID, c.CustomerID }).ToList();

                    //var lstVendor = model.CUS_Customer.Where(c => !c.IsSystem && (Account.ListActionCode.Contains(ViewAdmin) || Account.ListCustomerID.Contains(c.ID)) && (c.TypeOfCustomerID == iVen || c.TypeOfCustomerID == iBoth)).Select(c => new { c.ID, c.Code }).ToList();
                    var lstVendor = model.CUS_Customer.Where(c => !c.IsSystem && (c.TypeOfCustomerID == iVen || c.TypeOfCustomerID == iBoth)).Select(c => new { c.ID, c.Code }).ToList();
                    var lstFLMVehicle = model.FLM_Asset.Where(c => c.VehicleID > 0 && c.SYSCustomerID == Account.SYSCustomerID && c.TypeOfAssetID == -(int)SYSVarType.TypeOfAssetTruck && c.IsDisposal == false).Select(c => new { c.CAT_Vehicle.RegNo, c.VehicleID, c.ID }).ToList();
                    var lstVendorVehicle = model.CUS_Vehicle.Where(c => c.CAT_Vehicle.TypeOfVehicleID == -(int)SYSVarType.TypeOfVehicleTruck).Select(c => new { c.CustomerID, c.VehicleID, c.CAT_Vehicle.RegNo }).ToList();
                    var lstDriver = model.FLM_Driver.Where(c => c.SYSCustomerID == Account.SYSCustomerID).Select(c => new { c.CAT_Driver.LastName, c.CAT_Driver.FirstName, c.ID }).ToList();
                    #endregion

                    var lstWorksheet = HelperExcel.GetWorksheetByID(model, id);

                    double[] arrColumnWidth = new double[24];
                    arrColumnWidth[0] = 30;
                    for (int i = 1; i < 24; i++)
                    {
                        arrColumnWidth[i] = 100;
                    }

                    var ws = lstWorksheet[0];
                    ws.Rows = lst;
                    ws.Columns = HelperExcel.NewColumns(arrColumnWidth);

                    bool flag = true;
                    int OrderID = -1, CustomerID = -1, indexError = 0, PriceOfGOPID = -1;
                    int TransportID = -1, StockID = -1;
                    int GroupProductID = -1, ProductID = -1, PartnerID = -1, LocationToID = -1, VehicleID = -1;
                    int? VendorID = null;

                    string vendorHome = "Xe nhà";
                    foreach (var checkRow in lst)
                    {
                        if (checkRow.Index >= rowStart)
                        {
                            flag = true;
                            OrderID = -1;
                            CustomerID = -1;
                            TransportID = -1;
                            StockID = -1;
                            GroupProductID = -1;
                            ProductID = -1;
                            PartnerID = -1;
                            LocationToID = -1;
                            VehicleID = -1;
                            VendorID = null;

                            #region read data
                            colData = 1;
                            string dataOrderCode = HelperExcel.GetString(checkRow, colData++);
                            string dataCustomerCode = HelperExcel.GetString(checkRow, colData++);
                            string dataRequestDate = HelperExcel.GetString(checkRow, colData++);
                            string dataTransport = HelperExcel.GetString(checkRow, colData++);
                            string dataStockCode = HelperExcel.GetString(checkRow, colData++);
                            string dataGOPCode = HelperExcel.GetString(checkRow, colData++);
                            colData++;//7
                            string dataProductCode = HelperExcel.GetString(checkRow, colData++);
                            colData++;//9
                            string dataPartnerCode = HelperExcel.GetString(checkRow, colData++);
                            string dataLocationToCode = HelperExcel.GetString(checkRow, colData++);
                            colData++;//12
                            string dataETD = HelperExcel.GetString(checkRow, colData++);
                            string dataETA = HelperExcel.GetString(checkRow, colData++);
                            string dataQuantity = HelperExcel.GetString(checkRow, colData++);
                            string dataTon = HelperExcel.GetString(checkRow, colData++);
                            string dataCBM = HelperExcel.GetString(checkRow, colData++);
                            string dataVendorCode = HelperExcel.GetString(checkRow, colData++);
                            string dataVehicleCode = HelperExcel.GetString(checkRow, colData++);
                            string dataDriverName1 = HelperExcel.GetString(checkRow, colData++);
                            string dataDriverName2 = HelperExcel.GetString(checkRow, colData++);
                            string dataInvoiceNo = HelperExcel.GetString(checkRow, colData++);
                            string dataNote1 = HelperExcel.GetString(checkRow, colData++);
                            #endregion

                            #region check data
                            indexError = 0;
                            if (flag)
                                flag = HelperExcel.Valid(dataOrderCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true, 50);
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataCustomerCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag && lstCustomer.Count(c => c.Code == dataCustomerCode) == 0)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            if (flag)
                            {
                                CustomerID = -1;
                                OrderID = -1;
                                var check = lstCustomer.Where(c => c.Code == dataCustomerCode).FirstOrDefault();
                                if (check != null)
                                    CustomerID = check.ID;
                                var checkOrder = lstOrder.Where(c => c.CustomerID == CustomerID && dataOrderCode.ToLower() == c.Code.ToLower()).FirstOrDefault();
                                if (checkOrder != null) OrderID = checkOrder.ID;

                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colOrder, OrderID.ToString());
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colCus, CustomerID.ToString());
                            }
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataRequestDate, HelperExcel.ValidType.DateTime, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataTransport, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag)
                            {
                                var check = lstTransport.Where(c => c.Code.ToLower() == dataTransport.ToLower()).FirstOrDefault();
                                if (check.ID != iLTL && check.ID != iFTL)
                                {
                                    HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                    flag = false;
                                }
                                else
                                {
                                    TransportID = check.ID;
                                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colTransport, TransportID.ToString());
                                }
                            }
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataStockCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag)
                            {
                                var check = lstStock.Where(c => c.CustomerID == CustomerID && c.Code.ToLower() == dataStockCode.ToLower()).FirstOrDefault();
                                if (check == null)
                                {
                                    HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                    flag = false;
                                }
                                else
                                {
                                    StockID = check.ID;
                                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colStock, StockID.ToString());
                                }
                            }

                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataGOPCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag)
                            {
                                var check = lstGOP.Where(c => c.CustomerID == CustomerID && c.Code.ToLower() == dataGOPCode.ToLower()).FirstOrDefault();
                                if (check == null)
                                {
                                    HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                    flag = false;
                                }
                                else
                                {
                                    GroupProductID = check.ID;
                                    PriceOfGOPID = check.PriceOfGOPID;
                                }
                            }
                            indexError++;
                            if (flag)
                            {
                                var checkInStock = lstGOPInStock.Where(c => c.StockID == StockID && c.GroupOfProductID == GroupProductID).FirstOrDefault();
                                if (checkInStock == null)
                                {
                                    HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                    flag = false;
                                }
                                else
                                {
                                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colGroupProduct, GroupProductID.ToString());
                                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colPriceOfGop, PriceOfGOPID.ToString());
                                }
                            }

                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataProductCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag)
                            {
                                var check = lstProduct.Where(c => c.GroupOfProductID == GroupProductID && c.Code.ToLower() == dataProductCode.ToLower()).FirstOrDefault();
                                if (check == null)
                                {
                                    HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                    flag = false;
                                }
                                else
                                {
                                    ProductID = check.ID;
                                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colProduct, ProductID.ToString());
                                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colPacking, check.PackingID.ToString());
                                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colPakage, check.TypeOfPackageID.ToString());
                                }
                            }

                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataPartnerCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag && lstPartner.Count(c => c.CustomerID == CustomerID && c.PartnerCode.ToLower() == dataPartnerCode.ToLower()) == 0)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            if (flag)
                            {
                                PartnerID = -1;
                                var check = lstPartner.Where(c => c.CustomerID == CustomerID && c.PartnerCode.ToLower() == dataPartnerCode.ToLower()).FirstOrDefault();
                                if (check != null)
                                    PartnerID = check.ID;
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colPartner, PartnerID.ToString());
                            }
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataLocationToCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag && lstLocation.Count(c => c.CustomerID == CustomerID && c.CusPartID == PartnerID && c.Code.ToLower() == dataLocationToCode.ToLower()) == 0)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            if (flag)
                            {
                                LocationToID = -1;
                                var check = lstLocation.Where(c => c.CustomerID == CustomerID && c.CusPartID == PartnerID && c.Code.ToLower() == dataLocationToCode.ToLower()).FirstOrDefault();
                                if (check != null)
                                    LocationToID = check.ID;
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colLocation, LocationToID.ToString());
                            }
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataETD, HelperExcel.ValidType.DateTime, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataETA, HelperExcel.ValidType.DateTime, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);

                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataQuantity, HelperExcel.ValidType.Double, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true, 0, 0);
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataTon, HelperExcel.ValidType.Double, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true, 0, 0);
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataCBM, HelperExcel.ValidType.Double, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true, 0, 0);
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataVendorCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag)
                            {
                                if (dataVendorCode.ToLower() == vendorHome.ToLower())
                                {
                                    VendorID = -1;
                                    HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colVendor, VendorID.ToString());
                                }
                                else
                                {

                                    var check = lstVendor.Where(c => c.Code == dataVendorCode).FirstOrDefault();
                                    if (check == null)
                                    {
                                        HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                        flag = false;
                                    }
                                    else
                                    {
                                        VendorID = check.ID;
                                        HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colVendor, VendorID.ToString());
                                    }
                                }
                            }

                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataVehicleCode, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag && VendorID == -1 && lstFLMVehicle.Where(c => c.RegNo.ToUpper() == dataVehicleCode.ToUpper()).Count() == 0)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            indexError++;
                            if (flag && VendorID > 0 && lstVendorVehicle.Where(c => c.RegNo.ToUpper() == dataVehicleCode.ToUpper()).Count() == 0)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            if (flag)
                            {
                                VehicleID = -1;
                                if (VendorID == -1)
                                {
                                    var check = lstFLMVehicle.FirstOrDefault(c => c.RegNo.ToUpper() == dataVehicleCode.ToUpper());
                                    if (check != null) VehicleID = check.VehicleID.Value;
                                }
                                else
                                {
                                    var check = lstVendorVehicle.FirstOrDefault(c => c.RegNo.ToUpper() == dataVehicleCode.ToUpper());
                                    if (check != null) VehicleID = check.VehicleID;
                                }
                                HelperExcel.CheckErrorSuccess(checkRow, colCheckChange, colCheckNote, colRegNo, VehicleID.ToString());
                            }
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataDriverName1, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, true);
                            indexError++;
                            if (flag && VendorID == -1 && lstDriver.Count(c => (c.LastName + " " + c.FirstName).Trim().ToLower() == dataDriverName1.ToLower()) == 0)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataDriverName2, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, false);
                            indexError++;
                            if (flag && VendorID == -1 && !string.IsNullOrEmpty(dataDriverName2) && lstDriver.Count(c => (c.LastName + " " + c.FirstName).Trim().ToLower() == dataDriverName2.ToLower()) == 0)
                            {
                                HelperExcel.CheckErrorFail(checkRow, colCheckChange, colCheckNote, HelperExcel.MessageError(indexError, lstMessageError));
                                flag = false;
                            }
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataInvoiceNo, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, false, 1000);
                            indexError++;
                            if (flag)
                                flag = HelperExcel.Valid(dataNote1, HelperExcel.ValidType.String, checkRow, colCheckChange, colCheckNote, indexError, lstMessageError, false, 500);

                            #endregion
                        }
                    }
                    HelperExcel.SaveData(model, id, lstWorksheet);

                    return HelperExcel.GetByID(model, id); ;
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

        public bool PODDIInput_UploadOrder_ExcelApprove(long id)
        {
            try
            {
                using (var model = new DataEntities())
                {

                    model.EventAccount = Account; model.EventRunning = true;

                    int rowStart = 1;
                    int colData = 24;
                    int colCheckChange = colData++;
                    int colCheckNote = colData++;
                    int colOrder = colData++;
                    int colCus = colData++;
                    int colTransport = colData++;
                    int colStock = colData++;
                    int colGroupProduct = colData++;
                    int colProduct = colData++;
                    int colPacking = colData++;
                    int colPakage = colData++;
                    int colPriceOfGop = colData++;
                    int colPartner = colData++;
                    int colLocation = colData++;
                    int colVendor = colData++;
                    int colRegNo = colData++;
                    int colDriver1 = colData++;
                    int colDriver2 = colData++;

                    var lstRow = HelperExcel.GetSuccess(model, id, rowStart, colCheckChange, colCheckNote);
                    if (lstRow.Count > 0)
                    {
                        List<ORD_Order> lstOrderUse = new List<ORD_Order>();
                        foreach (var eRow in lstRow)
                        {
                            #region read data
                            string dataOrderCode = HelperExcel.GetString(eRow, 1);
                            string dataRequestDate = HelperExcel.GetString(eRow, 3);
                            string dataETD = HelperExcel.GetString(eRow, 13);
                            string dataETA = HelperExcel.GetString(eRow, 14);
                            string dataQuantity = HelperExcel.GetString(eRow, 15);
                            string dataTon = HelperExcel.GetString(eRow, 16);
                            string dataCBM = HelperExcel.GetString(eRow, 17);
                            string dataDriverName1 = HelperExcel.GetString(eRow, 20);
                            string dataDriverName2 = HelperExcel.GetString(eRow, 21);
                            string dataInvoiceNo = HelperExcel.GetString(eRow, 22);
                            string dataNote1 = HelperExcel.GetString(eRow, 23);

                            string dataOrderID = HelperExcel.GetString(eRow, colOrder);
                            string dataCutomerID = HelperExcel.GetString(eRow, colCus);
                            string dataTransportID = HelperExcel.GetString(eRow, colTransport);
                            string dataStockID = HelperExcel.GetString(eRow, colStock);

                            string dataGroupProductID = HelperExcel.GetString(eRow, colGroupProduct);
                            string dataProductID = HelperExcel.GetString(eRow, colProduct);
                            string dataPriceOfGopID = HelperExcel.GetString(eRow, colPriceOfGop);
                            string dataPackingID = HelperExcel.GetString(eRow, colPacking);
                            string dataPackageID = HelperExcel.GetString(eRow, colPakage);
                            string dataPartnerID = HelperExcel.GetString(eRow, colPartner);
                            string dataLocationID = HelperExcel.GetString(eRow, colLocation);
                            string dataVendorID = HelperExcel.GetString(eRow, colVendor);
                            string dataVehicleID = HelperExcel.GetString(eRow, colRegNo);
                            string dataDriverID1 = HelperExcel.GetString(eRow, colDriver1);
                            string dataDriverID2 = HelperExcel.GetString(eRow, colDriver2);
                            #endregion

                            #region convert data
                            int OrderID = Convert.ToInt32(dataOrderID);
                            int CustomerID = Convert.ToInt32(dataCutomerID);
                            int TransportModeID = Convert.ToInt32(dataTransportID);
                            int StockID = Convert.ToInt32(dataStockID);
                            int GroupProductID = Convert.ToInt32(dataGroupProductID);
                            int ProductID = Convert.ToInt32(dataProductID);
                            int packingID = Convert.ToInt32(dataPackingID);
                            int priceOfGOPID = Convert.ToInt32(dataPriceOfGopID);
                            int packageID = Convert.ToInt32(dataPackageID);
                            int PartnerID = Convert.ToInt32(dataPartnerID);
                            int LocationID = Convert.ToInt32(dataLocationID);
                            int VendorID = Convert.ToInt32(dataVendorID);
                            int VehicleID = Convert.ToInt32(dataVehicleID);
                            int? DriverID1 = null;
                            int? DriverID2 = null;
                            if (!string.IsNullOrEmpty(dataDriverID1)) DriverID1 = Convert.ToInt32(dataDriverID1);
                            if (!string.IsNullOrEmpty(dataDriverID2)) DriverID2 = Convert.ToInt32(dataDriverID2);

                            DateTime RequestDate = HelperExcel.ValueToDateVN(dataRequestDate);
                            DateTime ETA = HelperExcel.ValueToDateVN(dataETA);
                            DateTime? ETD = null;
                            if (string.IsNullOrEmpty(dataETD)) ETD = ETA.AddHours(1);
                            else ETD = HelperExcel.ValueToDateVN(dataETD);
                            double ton = Convert.ToDouble(dataTon);
                            double cbm = Convert.ToDouble(dataCBM);
                            double quantity = Convert.ToDouble(dataQuantity);
                            #endregion

                            #region ORD_Order
                            var objORDOrder = model.ORD_Order.FirstOrDefault(c => c.ID == OrderID);
                            if (objORDOrder == null)
                            {
                                objORDOrder = lstOrderUse.FirstOrDefault(c => c.CustomerID == CustomerID && c.Code.ToLower() == dataOrderCode.ToLower());
                                if (objORDOrder == null)
                                {
                                    objORDOrder = new ORD_Order();
                                    objORDOrder.CreatedBy = Account.UserName;
                                    objORDOrder.CreatedDate = DateTime.Now;
                                    objORDOrder.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderReceived;
                                    objORDOrder.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanComplete;

                                    objORDOrder.CustomerID = CustomerID;
                                    objORDOrder.SYSCustomerID = Account.SYSCustomerID;
                                    objORDOrder.Code = dataOrderCode;
                                    objORDOrder.IsOPS = true;
                                    objORDOrder.TransportModeID = TransportModeID;
                                    objORDOrder.RequestDate = RequestDate;
                                    objORDOrder.ETD = ETD;
                                    objORDOrder.ETA = ETA;
                                    objORDOrder.DateConfig = RequestDate;
                                    model.ORD_Order.Add(objORDOrder);
                                    lstOrderUse.Add(objORDOrder);
                                }
                            }
                            else
                            {
                                objORDOrder.ModifiedBy = Account.UserName;
                                objORDOrder.ModifiedDate = DateTime.Now;
                            }

                            objORDOrder.TypeOfOrderID = -(int)SYSVarType.TypeOfOrderDirect;// loai don hang thuong
                            objORDOrder.AllowCoLoad = true;

                            objORDOrder.ServiceOfOrderID = HelperContract.GetServiceOfOrder_First(model, -(int)SYSVarType.ServiceOfOrderLocal);// dich vu noi dia

                            objORDOrder.TypeOfContractID = -(int)SYSVarType.TypeOfContractMain;// loai hop dong chính thức
                            objORDOrder.ContractID = null;

                            objORDOrder.IsClosed = false;
                            objORDOrder.IsHot = false;
                            #endregion

                            #region ORD_GroupProduct
                            var objORDGroup = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == -1);
                            if (objORDGroup == null)
                            {
                                objORDGroup = new ORD_GroupProduct();
                                objORDGroup.CreatedBy = Account.UserName;
                                objORDGroup.CreatedDate = DateTime.Now;
                                objORDGroup.ORD_Order = objORDOrder;
                                model.ORD_GroupProduct.Add(objORDGroup);
                            }
                            else
                            {
                                objORDGroup.ModifiedBy = Account.UserName;
                                objORDGroup.ModifiedDate = DateTime.Now;
                            }
                            objORDGroup.GroupOfProductID = GroupProductID;
                            objORDGroup.Ton = ton;
                            objORDGroup.CBM = cbm;
                            objORDGroup.Quantity = quantity;
                            objORDGroup.PriceOfGOPID = priceOfGOPID;
                            objORDGroup.PackingID = packingID;
                            objORDGroup.LocationFromID = StockID;
                            objORDGroup.LocationToID = LocationID;
                            objORDGroup.PartnerID = PartnerID;
                            objORDGroup.ETA = ETA;
                            objORDGroup.ETD = ETD;
                            #endregion

                            #region ORD_Product
                            var objORDProduct = model.ORD_Product.FirstOrDefault(c => c.ID == -1);
                            if (objORDProduct == null)
                            {
                                objORDProduct = new ORD_Product();
                                objORDProduct.CreatedBy = Account.UserName;
                                objORDProduct.CreatedDate = DateTime.Now;
                                objORDProduct.ORD_GroupProduct = objORDGroup;
                                model.ORD_Product.Add(objORDProduct);
                            }
                            else
                            {
                                objORDProduct.ModifiedBy = Account.UserName;
                                objORDProduct.ModifiedDate = DateTime.Now;
                            }

                            objORDProduct.ProductID = ProductID;
                            objORDProduct.PackingID = packingID;
                            switch (packageID)
                            {
                                default:
                                    break;
                                case -(int)SYSVarType.TypeOfPackingGOPTon:
                                    objORDProduct.ExchangeTon = 1;
                                    objORDProduct.ExchangeCBM = 0;
                                    objORDProduct.ExchangeQuantity = 0;
                                    objORDProduct.Quantity = ton;
                                    break;
                                case -(int)SYSVarType.TypeOfPackingGOPCBM:
                                    objORDProduct.ExchangeTon = 0;
                                    objORDProduct.ExchangeCBM = 1;
                                    objORDProduct.ExchangeQuantity = 0;
                                    objORDProduct.Quantity = cbm;
                                    break;
                                case -(int)SYSVarType.TypeOfPackingGOPTU:
                                    objORDProduct.ExchangeTon = 0;
                                    objORDProduct.ExchangeCBM = 0;
                                    objORDProduct.ExchangeQuantity = 1;
                                    objORDProduct.Quantity = quantity;
                                    break;
                            }
                            #endregion

                            #region OPS_DITOMaster
                            var objOPSMaster = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == -1);
                            if (objOPSMaster == null)
                            {
                                objOPSMaster = new OPS_DITOMaster();
                                objOPSMaster.CreatedBy = Account.UserName;
                                objOPSMaster.CreatedDate = DateTime.Now;
                                objOPSMaster.SYSCustomerID = Account.SYSCustomerID;
                                objOPSMaster.Code = string.Empty;
                                objOPSMaster.IsRouteVendor = false;
                                objOPSMaster.IsRouteCustomer = false;
                                objOPSMaster.IsLoading = false;
                                objOPSMaster.IsHot = false;
                                objOPSMaster.IsBidding = false;
                                objOPSMaster.AllowCoLoad = true;
                                model.OPS_DITOMaster.Add(objOPSMaster);
                            }
                            else
                            {
                                objOPSMaster.ModifiedBy = Account.UserName;
                                objOPSMaster.ModifiedDate = DateTime.Now;
                            }

                            objOPSMaster.VehicleID = VehicleID;
                            if (VendorID > 0)
                            {
                                objOPSMaster.VendorOfVehicleID = VendorID;
                                objOPSMaster.DriverName1 = dataDriverName1;
                                objOPSMaster.DriverName2 = dataDriverName2;
                                if (string.IsNullOrEmpty(dataDriverName1)) objOPSMaster.DriverName1 = null;
                                if (string.IsNullOrEmpty(dataDriverName2)) objOPSMaster.DriverName2 = null;
                            }
                            else
                            {
                                objOPSMaster.VendorOfVehicleID = null;
                                objOPSMaster.DriverID1 = DriverID1;
                                objOPSMaster.DriverID2 = DriverID1;
                                objOPSMaster.DriverName1 = null;
                                objOPSMaster.DriverName2 = null;
                                if (objOPSMaster.DriverID1 != null) objOPSMaster.DriverName1 = dataDriverName1;
                                if (objOPSMaster.DriverID2 != null) objOPSMaster.DriverName2 = dataDriverName2;
                            }

                            objOPSMaster.SortOrder = 1;
                            objOPSMaster.ETA = ETA;
                            objOPSMaster.ETD = ETD;
                            objOPSMaster.ATA = ETA;
                            objOPSMaster.ATD = ETD;
                            objOPSMaster.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterReceived;
                            objOPSMaster.TypeOfDITOMasterID = -(int)SYSVarType.TypeOfDITONormal;
                            objOPSMaster.TransportModeID = TransportModeID;
                            objOPSMaster.TypeOfOrderID = -(int)SYSVarType.TypeOfOrderDirect;
                            #endregion

                            #region OPS_DITOGroupProduct
                            var objOPSGroup = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == -1);
                            if (objOPSGroup == null)
                            {
                                objOPSGroup = new OPS_DITOGroupProduct();
                                objOPSGroup.CreatedBy = Account.UserName;
                                objOPSGroup.CreatedDate = DateTime.Now;
                                objOPSGroup.OPS_DITOMaster = objOPSMaster;
                                objOPSGroup.ORD_GroupProduct = objORDGroup;

                                objOPSGroup.IsInput = true;
                                objOPSGroup.IsClosed = false;
                                objOPSGroup.HasUpload = false;
                                objOPSGroup.IsSplit = false;

                                model.OPS_DITOGroupProduct.Add(objOPSGroup);
                            }

                            objOPSGroup.Ton = ton;
                            objOPSGroup.CBM = cbm;
                            objOPSGroup.Quantity = quantity;

                            objOPSGroup.TonBBGN = ton;
                            objOPSGroup.CBMBBGN = cbm;
                            objOPSGroup.QuantityBBGN = quantity;

                            objOPSGroup.TonTranfer = ton;
                            objOPSGroup.CBMTranfer = cbm;
                            objOPSGroup.QuantityTranfer = quantity;

                            objOPSGroup.QuantityLoading = quantity;

                            objOPSGroup.TonReturn = 0;
                            objOPSGroup.CBMReturn = 0;
                            objOPSGroup.QuantityReturn = 0;

                            objOPSGroup.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                            objOPSGroup.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;

                            objOPSGroup.InvoiceNote = dataInvoiceNo;
                            objOPSGroup.Note1 = dataNote1;
                            #endregion

                            #region OPS_DITOProduct
                            var objOPSProduct = model.OPS_DITOProduct.FirstOrDefault(c => c.ID == -1);
                            if (objOPSProduct == null)
                            {
                                objOPSProduct = new OPS_DITOProduct();
                                objOPSProduct.CreatedBy = Account.UserName;
                                objOPSProduct.CreatedDate = DateTime.Now;

                                objOPSProduct.ORD_Product = objORDProduct;
                                objOPSProduct.OPS_DITOGroupProduct = objOPSGroup;

                                model.OPS_DITOProduct.Add(objOPSProduct);
                            }
                            else
                            {
                                objOPSProduct.ModifiedBy = Account.UserName;
                                objOPSProduct.ModifiedDate = DateTime.Now;
                            }
                            objOPSProduct.Quantity = objORDProduct.Quantity;
                            objOPSProduct.QuantityTranfer = objORDProduct.Quantity;
                            objOPSProduct.QuantityBBGN = objORDProduct.Quantity;
                            objOPSProduct.QuantityReturn = 0;
                            #endregion
                        }
                        model.SaveChanges();
                        return true;
                    }
                    else return false;
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

        #region PODMap
        public List<DTOPODImport> PODMapImport_Data(DateTime dtFrom, DateTime dtTO, int cusId)
        {
            try
            {
                List<DTOPODImport> result = new List<DTOPODImport>();
                dtFrom = dtFrom.Date;
                dtTO = dtTO.Date.AddDays(1);
                using (var model = new DataEntities())
                {
                    List<DTOPODImport> temp = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.CustomerID == cusId && c.OPS_DITOMaster.ETD >= dtFrom && c.OPS_DITOMaster.ETD < dtTO
                        && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered).Select(c => new DTOPODImport
                        {
                            ID = c.ID,
                            DNCode = c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            ETARequest = c.ORD_GroupProduct.ETARequest,
                            ETD = c.ORD_GroupProduct.ETD,
                            MasterETDDate = c.ORD_GroupProduct.ETD,
                            MasterETDDatetime = c.ORD_GroupProduct.ETD,
                            OrderGroupETDDate = c.ORD_GroupProduct.ETD,
                            OrderGroupETDDatetime = c.ORD_GroupProduct.ETD,
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
                            InvoiceDate = null,
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

                        }).OrderBy(c => c.ETD).ThenBy(c => c.MasterCode).ThenBy(c => c.OrderCode).ToList();

                    var listPOD = model.OPS_DITOProductPOD.Where(c => c.OPS_DITOProduct.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID == cusId && c.OPS_DITOProduct.OPS_DITOGroupProduct.OPS_DITOMaster.ETD >= dtFrom && c.OPS_DITOProduct.OPS_DITOGroupProduct.OPS_DITOMaster.ETD < dtTO
                        && c.OPS_DITOProduct.OPS_DITOGroupProduct.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOProduct.OPS_DITOGroupProduct.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered).Select(c => new
                        {
                            ShipmentNo = c.ShipmentNo,
                            InvoiceNo = c.InvoiceNo,
                            BillingNo = c.BillingNo,
                            ID = c.OPS_DITOProduct.DITOGroupProductID,
                            InvoiceDate = c.InvoiceDate,
                        }).ToList();

                    foreach (var item in temp)
                    {
                        var lstDetail = listPOD.Where(c => c.ID == item.ID).ToList();
                        if (lstDetail != null && lstDetail.Count > 0)
                        {
                            foreach (var detail in lstDetail)
                            {
                                var objOPS = new DTOPODImport
                                {
                                    ID = item.ID,
                                    DNCode = item.DNCode,
                                    SOCode = item.SOCode,
                                    OrderCode = item.OrderCode,
                                    ETARequest = item.ETARequest,
                                    ETD = item.ETD,
                                    MasterETDDate = item.MasterETDDate,
                                    MasterETDDatetime = item.MasterETDDatetime,
                                    OrderGroupETDDate = item.OrderGroupETDDate,
                                    OrderGroupETDDatetime = item.OrderGroupETDDatetime,
                                    CustomerCode = item.CustomerCode,
                                    CustomerName = item.CustomerName,
                                    CreatedDate = item.CreatedDate,
                                    MasterCode = item.MasterCode,
                                    DriverName = item.DriverName,
                                    DriverTel = item.DriverTel,
                                    DriverCard = item.DriverCard,
                                    RegNo = item.RegNo,
                                    RequestDate = item.RequestDate,
                                    LocationFromCode = item.LocationFromCode,
                                    LocationToCode = item.LocationToCode,
                                    LocationToName = item.LocationToName,
                                    LocationToAddress = item.LocationToAddress,
                                    LocationToProvince = item.LocationToProvince,
                                    LocationToDistrict = item.LocationToDistrict,
                                    DistributorCode = item.DistributorCode,
                                    DistributorName = item.DistributorName,
                                    DistributorCodeName = item.DistributorCodeName,
                                    Packing = item.Packing,
                                    IsInvoice = item.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete ? true : false,
                                    DateFromCome = item.DateFromCome,
                                    DateFromLeave = item.DateFromLeave,
                                    DateFromLoadEnd = item.DateFromLoadEnd,
                                    DateFromLoadStart = item.DateFromLoadStart,
                                    DateToCome = item.DateToCome,
                                    DateToLeave = item.DateToLeave,
                                    DateToLoadEnd = item.DateToLoadEnd,
                                    DateToLoadStart = item.DateToLoadStart,
                                    EconomicZone = item.EconomicZone,
                                    //DITOGroupProductStatusPODName = item.SYS_Var1.ValueOfVar,
                                    IsOrigin = item.IsOrigin,
                                    InvoiceBy = item.InvoiceBy,
                                    InvoiceNote = item.InvoiceNote,
                                    InvoiceDate = null,
                                    Note = item.Note,
                                    OPSGroupNote1 = item.OPSGroupNote1,
                                    OPSGroupNote2 = item.OPSGroupNote2,
                                    ORDGroupNote1 = item.ORDGroupNote1,
                                    ORDGroupNote2 = item.ORDGroupNote2,
                                    TOMasterNote1 = item.TOMasterNote1,
                                    TOMasterNote2 = item.TOMasterNote2,
                                    VendorName = item.VendorName,
                                    VendorCode = item.VendorCode,
                                    Description = item.Description,
                                    GroupOfProductCode = item.GroupOfProductCode,
                                    GroupOfProductName = item.GroupOfProductName,
                                    ChipNo = item.ChipNo,
                                    Temperature = item.Temperature,
                                    Ton = item.Ton,
                                    TonBBGN = item.TonBBGN,
                                    TonTranfer = item.TonTranfer,
                                    CBM = item.CBM,
                                    CBMBBGN = item.CBMBBGN,
                                    CBMTranfer = item.CBMTranfer,
                                    Quantity = item.Quantity,
                                    QuantityBBGN = item.QuantityBBGN,
                                    QuantityTranfer = item.QuantityTranfer,
                                    VENLoadCodeID = item.VENLoadCodeID,
                                    VENLoadCode = item.VENLoadCode,
                                    VENUnLoadCodeID = item.VENUnLoadCodeID,
                                    VENUnLoadCode = item.VENUnLoadCode,
                                };

                                objOPS.ShipmentNo = detail.ShipmentNo;
                                objOPS.InvoiceNo = detail.InvoiceNo;
                                objOPS.BillingNo = detail.BillingNo;
                                objOPS.InvoiceDate = detail.InvoiceDate;
                                result.Add(objOPS);
                            }
                        }
                        else
                        {
                            result.Add(item);
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

        public DTOResult PODMapImport_Index_Setting_List(string request)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    DTOResult result = new DTOResult();
                    var query = model.CUS_Setting.Where(c => c.SYSCustomerID == Account.SYSCustomerID && (c.CustomerID == Account.SYSCustomerID ? true : Account.ListCustomerID.Contains(c.CustomerID)) && c.Key == CUSSettingKey.PODMap.ToString()).Select(c => new DTOCUSSettingPODMap
                    {
                        SettingID = c.ID,
                        Name = c.Name,
                        CreateBy = c.CreatedBy,
                        CreateDate = c.CreatedDate,
                        CustomerID = c.CustomerID,
                        SettingCustomerName = c.CUS_Customer.CustomerName
                    }).ToDataSourceResult(CreateRequest(request));
                    result.Total = query.Total;
                    result.Data = query.Data as IEnumerable<DTOCUSSettingPODMap>;
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

        public DTOCUSSettingPODMap PODMapImport_Index_Setting_Get(int id)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    DTOCUSSettingPODMap result = new DTOCUSSettingPODMap();
                    if (id > 0)
                    {
                        var obj = model.CUS_Setting.Where(c => c.ID == id).FirstOrDefault();
                        if (obj != null)
                        {
                            result = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOCUSSettingPODMap>(obj.Setting);
                        }
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

        public void PODMapImport_Excel_Import(int sID, List<DTOPODImport> data)
        {
            try
            {
                DTOCUSSettingPODMap objSetting = PODMapImport_Index_Setting_Get(sID);
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    //Order by CustomerID
                    data = data.OrderBy(c => c.CustomerID).ToList();

                    //Check code
                    foreach (var item in data.Where(c => c.ExcelSuccess))
                    {
                        var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                        {
                            if (!string.IsNullOrEmpty(item.ShipmentNo) || !string.IsNullOrEmpty(item.ShipmentNo) || !string.IsNullOrEmpty(item.ShipmentNo))
                            {
                                var pod = model.OPS_DITOProductPOD.FirstOrDefault(c => c.OPS_DITOProduct.DITOGroupProductID == obj.ID
                                    && c.ShipmentNo == item.ShipmentNo && c.InvoiceNo == item.InvoiceNo && c.BillingNo == item.BillingNo);
                                if (pod == null)
                                {
                                    pod = new OPS_DITOProductPOD();
                                    pod.CreatedBy = Account.UserName;
                                    pod.CreatedDate = DateTime.Now;
                                    pod.DITOProductID = obj.OPS_DITOProduct.FirstOrDefault().ID;
                                    if (objSetting.ShipmentNo > 0)
                                        pod.ShipmentNo = item.ShipmentNo;
                                    if (objSetting.InvoiceNo > 0)
                                        pod.InvoiceNo = item.InvoiceNo;
                                    if (objSetting.BillingNo > 0)
                                        pod.BillingNo = item.BillingNo;
                                    if (objSetting.InvoiceDate > 0)
                                        pod.InvoiceDate = item.InvoiceDate;
                                    model.OPS_DITOProductPOD.Add(pod);
                                }
                            }
                        }
                        else
                        {
                            throw FaultHelper.BusinessFault(null, null, "Số thứ tự không hợp lệ.");
                        }
                    }

                    model.SaveChanges();

                    List<int> lstOrderID = new List<int>();
                    foreach (var item in data.Select(c => c.ID).Distinct().ToList())
                    {
                        var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item);
                        if (obj != null)
                        {
                            if (model.OPS_DITOProductPOD.Count(c => c.OPS_DITOProduct.DITOGroupProductID == item) == model.OPS_DITOProductPOD.Count(c => c.OPS_DITOProduct.DITOGroupProductID == item && c.InvoiceDate != null))
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                                obj.DITOGroupProductStatusPODID = (-(int)SYSVarType.DITOGroupProductStatusPODComplete);
                            }

                            lstOrderID.Add(obj.ORD_GroupProduct.ORD_Order.ID);
                        }
                    }

                    model.SaveChanges();


                    if (lstOrderID != null && lstOrderID.Count > 0)
                        HelperStatus.ORDOrder_Status(model, Account, lstOrderID.Distinct().ToList());
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

        public List<DTOPODInvoice> PODInvoice_Data(DateTime dtFrom, DateTime dtTO, int cusId)
        {
            try
            {
                List<DTOPODInvoice> result = new List<DTOPODInvoice>();
                dtFrom = dtFrom.Date;
                dtTO = dtTO.Date.AddDays(1);
                using (var model = new DataEntities())
                {
                    List<DTOPODInvoice> temp = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.CustomerID == cusId && c.OPS_DITOMaster.ETD >= dtFrom && c.OPS_DITOMaster.ETD < dtTO
                        && c.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered).Select(c => new DTOPODInvoice
                        {
                            ID = c.ID,
                            DNCode = c.DNCode,
                            SOCode = c.ORD_GroupProduct.SOCode,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            ETARequest = c.ORD_GroupProduct.ETARequest,
                            ETD = c.ORD_GroupProduct.ETD,
                            MasterETDDate = c.ORD_GroupProduct.ETD,
                            MasterETDDatetime = c.ORD_GroupProduct.ETD,
                            OrderGroupETDDate = c.ORD_GroupProduct.ETD,
                            OrderGroupETDDatetime = c.ORD_GroupProduct.ETD,
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
                            InvoiceDate = null,
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
                            ProductPODID = 0,
                        }).OrderBy(c => c.ETD).ThenBy(c => c.MasterCode).ThenBy(c => c.OrderCode).ToList();

                    var listPOD = model.OPS_DITOProductPOD.Where(c => c.OPS_DITOProduct.OPS_DITOGroupProduct.ORD_GroupProduct.ORD_Order.CustomerID == cusId && c.OPS_DITOProduct.OPS_DITOGroupProduct.OPS_DITOMaster.ETD >= dtFrom && c.OPS_DITOProduct.OPS_DITOGroupProduct.OPS_DITOMaster.ETD < dtTO
                        && c.OPS_DITOProduct.OPS_DITOGroupProduct.OPS_DITOMaster.SYSCustomerID == Account.SYSCustomerID && c.OPS_DITOProduct.OPS_DITOGroupProduct.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered).Select(c => new
                        {
                            ShipmentNo = c.ShipmentNo,
                            InvoiceNo = c.InvoiceNo,
                            BillingNo = c.BillingNo,
                            ID = c.OPS_DITOProduct.DITOGroupProductID,
                            ProductPODID = c.ID,
                            InvoiceDate = c.InvoiceDate,
                        }).ToList();

                    foreach (var item in temp)
                    {
                        var lstDetail = listPOD.Where(c => c.ID == item.ID).ToList();
                        if (lstDetail != null && lstDetail.Count > 0)
                        {
                            foreach (var detail in lstDetail)
                            {
                                var objOPS = new DTOPODInvoice
                                {
                                    ID = item.ID,
                                    DNCode = item.DNCode,
                                    SOCode = item.SOCode,
                                    OrderCode = item.OrderCode,
                                    ETARequest = item.ETARequest,
                                    ETD = item.ETD,
                                    MasterETDDate = item.MasterETDDate,
                                    MasterETDDatetime = item.MasterETDDatetime,
                                    OrderGroupETDDate = item.OrderGroupETDDate,
                                    OrderGroupETDDatetime = item.OrderGroupETDDatetime,
                                    CustomerCode = item.CustomerCode,
                                    CustomerName = item.CustomerName,
                                    CreatedDate = item.CreatedDate,
                                    MasterCode = item.MasterCode,
                                    DriverName = item.DriverName,
                                    DriverTel = item.DriverTel,
                                    DriverCard = item.DriverCard,
                                    RegNo = item.RegNo,
                                    RequestDate = item.RequestDate,
                                    LocationFromCode = item.LocationFromCode,
                                    LocationToCode = item.LocationToCode,
                                    LocationToName = item.LocationToName,
                                    LocationToAddress = item.LocationToAddress,
                                    LocationToProvince = item.LocationToProvince,
                                    LocationToDistrict = item.LocationToDistrict,
                                    DistributorCode = item.DistributorCode,
                                    DistributorName = item.DistributorName,
                                    DistributorCodeName = item.DistributorCodeName,
                                    Packing = item.Packing,
                                    IsInvoice = item.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete ? true : false,
                                    DateFromCome = item.DateFromCome,
                                    DateFromLeave = item.DateFromLeave,
                                    DateFromLoadEnd = item.DateFromLoadEnd,
                                    DateFromLoadStart = item.DateFromLoadStart,
                                    DateToCome = item.DateToCome,
                                    DateToLeave = item.DateToLeave,
                                    DateToLoadEnd = item.DateToLoadEnd,
                                    DateToLoadStart = item.DateToLoadStart,
                                    EconomicZone = item.EconomicZone,
                                    //DITOGroupProductStatusPODName = item.SYS_Var1.ValueOfVar,
                                    IsOrigin = item.IsOrigin,
                                    InvoiceBy = item.InvoiceBy,
                                    InvoiceNote = item.InvoiceNote,
                                    InvoiceDate = null,
                                    Note = item.Note,
                                    OPSGroupNote1 = item.OPSGroupNote1,
                                    OPSGroupNote2 = item.OPSGroupNote2,
                                    ORDGroupNote1 = item.ORDGroupNote1,
                                    ORDGroupNote2 = item.ORDGroupNote2,
                                    TOMasterNote1 = item.TOMasterNote1,
                                    TOMasterNote2 = item.TOMasterNote2,
                                    VendorName = item.VendorName,
                                    VendorCode = item.VendorCode,
                                    Description = item.Description,
                                    GroupOfProductCode = item.GroupOfProductCode,
                                    GroupOfProductName = item.GroupOfProductName,
                                    ChipNo = item.ChipNo,
                                    Temperature = item.Temperature,
                                    Ton = item.Ton,
                                    TonBBGN = item.TonBBGN,
                                    TonTranfer = item.TonTranfer,
                                    CBM = item.CBM,
                                    CBMBBGN = item.CBMBBGN,
                                    CBMTranfer = item.CBMTranfer,
                                    Quantity = item.Quantity,
                                    QuantityBBGN = item.QuantityBBGN,
                                    QuantityTranfer = item.QuantityTranfer,
                                    VENLoadCodeID = item.VENLoadCodeID,
                                    VENLoadCode = item.VENLoadCode,
                                    VENUnLoadCodeID = item.VENUnLoadCodeID,
                                    VENUnLoadCode = item.VENUnLoadCode,
                                };

                                objOPS.ShipmentNo = detail.ShipmentNo;
                                objOPS.InvoiceNo = detail.InvoiceNo;
                                objOPS.BillingNo = detail.BillingNo;
                                objOPS.ProductPODID = detail.ProductPODID;
                                objOPS.InvoiceDate = detail.InvoiceDate;
                                result.Add(objOPS);
                            }
                        }
                        else
                        {
                            result.Add(item);
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

        public void PODInvoice_SaveList(List<DTOPODInvoice> data)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    //Check code
                    foreach (var item in data)
                    {
                        var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.ID);
                        if (obj != null)
                        {
                            if (!string.IsNullOrEmpty(item.ShipmentNo) || !string.IsNullOrEmpty(item.ShipmentNo) || !string.IsNullOrEmpty(item.ShipmentNo))
                            {
                                var pod = model.OPS_DITOProductPOD.FirstOrDefault(c => c.ID == item.ProductPODID);
                                if (pod == null)
                                {
                                    pod = new OPS_DITOProductPOD();
                                    pod.CreatedBy = Account.UserName;
                                    pod.CreatedDate = DateTime.Now;
                                    pod.DITOProductID = obj.OPS_DITOProduct.FirstOrDefault().ID;
                                    pod.ShipmentNo = item.ShipmentNo;
                                    pod.InvoiceNo = item.InvoiceNo;
                                    pod.BillingNo = item.BillingNo;
                                    pod.InvoiceDate = item.InvoiceDate;
                                    model.OPS_DITOProductPOD.Add(pod);
                                }
                                else
                                {
                                    pod.ModifiedBy = Account.UserName;
                                    pod.ModifiedDate = DateTime.Now;
                                    pod.ShipmentNo = item.ShipmentNo;
                                    pod.InvoiceNo = item.InvoiceNo;
                                    pod.BillingNo = item.BillingNo;
                                    pod.InvoiceDate = item.InvoiceDate;
                                }
                            }
                        }
                        else
                        {
                            throw FaultHelper.BusinessFault(null, null, "Không tìm thấy dữ liệu.");
                        }
                    }
                    model.SaveChanges();

                    List<int> lstOrderID = new List<int>();
                    foreach (var item in data.Select(c => c.ID).Distinct().ToList())
                    {
                        var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item);
                        if (obj != null)
                        {
                            if (model.OPS_DITOProductPOD.Count(c => c.OPS_DITOProduct.DITOGroupProductID == item) == model.OPS_DITOProductPOD.Count(c => c.OPS_DITOProduct.DITOGroupProductID == item && c.InvoiceDate != null))
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                                obj.DITOGroupProductStatusPODID = (-(int)SYSVarType.DITOGroupProductStatusPODComplete);
                            }
                            lstOrderID.Add(obj.ORD_GroupProduct.ORD_Order.ID);
                        }
                    }

                    model.SaveChanges();

                    if (lstOrderID != null && lstOrderID.Count > 0)
                        HelperStatus.ORDOrder_Status(model, Account, lstOrderID.Distinct().ToList());
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

        #region Close Data

        public DTOResult PODDI_CloseDataList(string request, DateTime dfrom, DateTime dto, List<int> listCustomerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    List<DTOPODDateData> list = new List<DTOPODDateData>();

                    dto = dto.AddDays(1);
                    dto = new DateTime(dto.Year, dto.Month, dto.Day, 0, 0, 0);
                    dfrom = new DateTime(dfrom.Year, dfrom.Month, dfrom.Day, 0, 0, 0);

                    for (var from = dfrom; from < dto; from = from.AddDays(1))
                    {
                        var to = from.AddDays(1);
                        var qr = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel
                                    && listCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.ORD_GroupProduct.ORD_Order.RequestDate >= from && c.ORD_GroupProduct.ORD_Order.RequestDate < to).Select(c => new
                                    {
                                        c.IsClosed
                                    }).ToList();

                        list.Add(new DTOPODDateData
                        {
                            Date = from,
                            NumberOfClosed = qr.Count(c => c.IsClosed),
                            NumberOfNonClosed = qr.Count(c => !c.IsClosed),
                        });
                    }

                    var data = list.ToDataSourceResult(CreateRequest(request));
                    result.Total = data.Total;
                    result.Data = data.Data as IEnumerable<DTOPODDateData>;
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

        public void PODDI_CloseDataByDate(List<int> listCustomerID, List<DateTime> lst, bool isOpen)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var date in lst)
                    {
                        DateTime dfrom = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                        DateTime dto = date.AddDays(1);
                        dto = new DateTime(dto.Year, dto.Month, dto.Day, 0, 0, 0);
                        var qr = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived
                            && listCustomerID.Contains(c.ORD_GroupProduct.ORD_Order.CustomerID) && c.ORD_GroupProduct.ORD_Order.RequestDate >= dfrom && c.ORD_GroupProduct.ORD_Order.RequestDate <= dto).ToList();
                        foreach (var obj in qr)
                        {
                            if (!isOpen)
                            {
                                if (!obj.IsClosed)
                                {
                                    obj.IsClosed = true;
                                    obj.ClosedDate = DateTime.Now;
                                    obj.ModifiedBy = Account.UserName;
                                    obj.ModifiedDate = DateTime.Now;
                                }
                            }
                            else
                            {
                                if (obj.IsClosed)
                                {
                                    obj.IsClosed = false;
                                    obj.ClosedDate = null;
                                    obj.ModifiedBy = Account.UserName;
                                    obj.ModifiedDate = DateTime.Now;
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

        //container ----

        public DTOResult PODCO_CloseDataList(string request, DateTime dfrom, DateTime dto, List<int> listCustomerID)
        {
            try
            {
                DTOResult result = new DTOResult();
                using (var model = new DataEntities())
                {
                    List<DTOPODDateData> list = new List<DTOPODDateData>();

                    dto = dto.AddDays(1);
                    dto = new DateTime(dto.Year, dto.Month, dto.Day, 0, 0, 0);
                    dfrom = new DateTime(dfrom.Year, dfrom.Month, dfrom.Day, 0, 0, 0);

                    for (var from = dfrom; from < dto; from = from.AddDays(1))
                    {
                        var to = from.AddDays(1);
                        var qr = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0
                            && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived
                            && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel
                            && listCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID)
                            && c.OPS_Container.ORD_Container.ORD_Order.RequestDate >= from && c.OPS_Container.ORD_Container.ORD_Order.RequestDate <= to).Select(c => new
                                    {
                                        c.IsClosed
                                    }).ToList();

                        list.Add(new DTOPODDateData
                        {
                            Date = from,
                            NumberOfClosed = qr.Count(c => c.IsClosed),
                            NumberOfNonClosed = qr.Count(c => !c.IsClosed),
                        });
                    }

                    var data = list.ToDataSourceResult(CreateRequest(request));
                    result.Total = data.Total;
                    result.Data = data.Data as IEnumerable<DTOPODDateData>;
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

        public void PODCO_CloseDataByDate(List<int> listCustomerID, List<DateTime> lst, bool isOpen)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;
                    foreach (var date in lst)
                    {
                        DateTime dfrom = new DateTime(date.Year, date.Month, date.Day, 0, 0, 0);
                        DateTime dto = date.AddDays(1);
                        dto = new DateTime(dto.Year, dto.Month, dto.Day, 0, 0, 0);
                        var qr = model.OPS_COTOContainer.Where(c => c.COTOMasterID > 0
                            && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived
                            && c.TypeOfStatusContainerID != -(int)SYSVarType.TypeOfStatusContainerCancel
                            && listCustomerID.Contains(c.OPS_Container.ORD_Container.ORD_Order.CustomerID)
                            && c.OPS_Container.ORD_Container.ORD_Order.RequestDate >= dfrom && c.OPS_Container.ORD_Container.ORD_Order.RequestDate <= dto).ToList();
                        foreach (var obj in qr)
                        {
                            if (!isOpen)
                            {
                                if (!obj.IsClosed)
                                {
                                    obj.IsClosed = true;
                                    obj.ClosedDate = DateTime.Now;
                                    obj.ModifiedBy = Account.UserName;
                                    obj.ModifiedDate = DateTime.Now;
                                }
                            }
                            else
                            {
                                if (obj.IsClosed)
                                {
                                    obj.IsClosed = false;
                                    obj.ClosedDate = null;
                                    obj.ModifiedBy = Account.UserName;
                                    obj.ModifiedDate = DateTime.Now;
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

        #endregion
    }
}

