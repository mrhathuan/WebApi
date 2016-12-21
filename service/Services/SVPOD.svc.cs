using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using Kendo.Mvc.Extensions;
using DTO;
using IServices;
using Business;

namespace Services
{
    public partial class SVPOD : SVBase, ISVPOD
    {
        public void Connect()
        {

        }
        #region Common

        public DTOResult Customer_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.Customer_List();
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

        public DTOResult Vendor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.Vendor_List();
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
        #endregion

        #region Container POD

        public DTOResult PODCOInput_List(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODCOInput_List(request, dtFrom, dtTO, listCustomerID);
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
        public void PODCOInput_Save(DTOPODCOInput item)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODCOInput_Save(item);
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
        #endregion

        #region Distribution POD

        public DTOResult PODDIInput_List(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID, List<int> listVendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_List(request, dtFrom, dtTO, listCustomerID, listVendorID);
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
        public void PODDIInput_Save(DTOPODDIInput item)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_Save(item);
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
        public DTOPODBarCode PODBarcodeGroup_List(string Barcode)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODBarcodeGroup_List(Barcode);
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

        public void PODBarcodeGroup_Save(DTOPODBarCodeGroup item, bool IsNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODBarcodeGroup_Save(item, IsNote);
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


        public DTOResult PODDistributionDN_ExcelExport(string request, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDistributionDN_ExcelExport(request, dtFrom, dtTO);
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

        public List<DTOPODDistributionDNExcel> PODDistributionDN_GetDataCheck()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDistributionDN_GetDataCheck();
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

        public void PODDistributionDN_ExcelSave(List<DTOPODDistributionDNExcel> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDistributionDN_ExcelSave(lst);
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

        public List<DTOPODDIDNReportExcel> PODDI_DN_ReportExcel()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDI_DN_ReportExcel();
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

        public DTOResult PODDIInput_CloseList(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_CloseList(request, dtFrom, dtTO, listCustomerID);
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
        public void PODDIInput_Approved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_Approved(lst);
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
        public void PODDIInput_UnApproved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_UnApproved(lst);
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
        public void PODDIInput_UpdateHasUpload(int DITOGroupID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_UpdateHasUpload(DITOGroupID);
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
        public List<DTOCUSProduct> PODDIInput_InsertProduct_ProductByGOPList(int DITOGroupProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_InsertProduct_ProductByGOPList(DITOGroupProductID);
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
        public void PODDIInput_InsertProduct_SaveList(List<PODInsertProduct> lst, int DITOGroupProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_InsertProduct_SaveList(lst, DITOGroupProductID);
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
        public List<CUSCustomer> PODDIInput_VendorList()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_VendorList();
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

        #endregion

        #region mon time

        public DTOResult PODMONTime_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODMONTime_List(request);
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

        public void PODMONTime_SaveList(List<DTOPODTime> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODMONTime_SaveList(lst);
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

        #endregion

        #region cong no tra ve
        public DTOResult PODOPSExtReturn_List(string request, DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_List(request, dFrom, dTo);
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
        public List<DTOPODOPSExtReturnExport> PODOPSExtReturn_Data(DateTime dFrom, DateTime dTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_Data(dFrom, dTo);
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
        public DTOPODOPSExtReturn PODOPSExtReturn_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_Get(id);
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
        public void PODOPSExtReturn_Delete(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODOPSExtReturn_Delete(lst);
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
        public void PODOPSExtReturn_Approved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODOPSExtReturn_Approved(lst);
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
        public int PODOPSExtReturn_Save(DTOPODOPSExtReturn item)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_Save(item);
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
        public DTOResult PODOPSExtReturn_DetailList(string request, int ExtReturnID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_DetailList(request, ExtReturnID);
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
        public void PODOPSExtReturn_DetailSave(List<DTOPODOPSExtReturnDetail> lst, int ExtReturnID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODOPSExtReturn_DetailSave(lst, ExtReturnID);
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
        public DTOResult PODOPSExtReturn_CustomerList()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_CustomerList();
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
        public DTOResult PODOPSExtReturn_GOPByCus(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_GOPByCus(cusID);
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
        public DTOResult PODOPSExtReturn_ProductByGOP(int gopID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_ProductByGOP(gopID);
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
        public List<DTOCATVehicle> PODOPSExtReturn_VehicleList(int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_VehicleList(vendorID);
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
        public List<DTOFLMDriver> PODOPSExtReturn_DriverList()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_DriverList();
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
        public DTOResult PODOPSExtReturn_VendorList()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_VendorList();
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
        public DTOResult PODOPSExtReturn_DetailNotIn(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_DetailNotIn(request, masterID);
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
        public List<DTOPODTOMaster> PODOPSExtReturn_DITOMasterList(int cusID, int vendorID, int vehicleID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_DITOMasterList(cusID, vendorID, vehicleID);
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

        public DTOResult PODOPSExtReturn_FindList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_FindList(request);
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
        public DTOResult PODOPSExtReturn_QuickList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_QuickList(request);
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
        public void PODOPSExtReturn_QuickSave(DTOPODOPSDITOProductQuick item)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODOPSExtReturn_QuickSave(item);
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
        public DTOPODOPSExtReturnData PODOPSExtReturn_QuickData()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODOPSExtReturn_QuickData();
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
        #endregion

        #region pod quick
        public DTOResult PODDIInput_Quick_List(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_Quick_List(request, dtFrom, dtTO, listCustomerID);
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
        public void PODDIInput_Quick_Save(DTOPODDIInputQuick item)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_Quick_Save(item);
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
        public DTOPODDIInputQuickDN PODDIInput_Quick_DNGet(int DITOGroupProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_Quick_DNGet(DITOGroupProductID);
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
        public void PODDIInput_Quick_DNSave(DTOPODDIInputQuickDN item)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_Quick_DNSave(item);
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
        #endregion

        #region DTOPODFLMDIInput  (chi phí xe nhà)
        public DTOResult PODFLMDIInput_List(string request, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMDIInput_List(request, dtFrom, dtTO);
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
        public List<DTOPODFLMDIInputExcel> PODFLMDIInput_Export(DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMDIInput_Export(dtFrom, dtTo);
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

        public void PODFLMDIInput_Import(List<DTOPODFLMDIInputImport> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMDIInput_Import(lst);
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
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMDIInput_Save(item);
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
        public void PODFLMDIInput_Approved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMDIInput_Approved(lst);
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

        public DTOPODFLMDIInputDriver PODFLMDIInput_GetDrivers(int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMDIInput_GetDrivers(DITOMasterID);
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
        public void PODFLMDIInput_SaveDrivers(DTOPODFLMDIInputDriver item, int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMDIInput_SaveDrivers(item, DITOMasterID);
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

        public DTOResult PODFLMDIInput_TroubleCostList(string request, int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMDIInput_TroubleCostList(request, DITOMasterID);
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

        public DTOResult PODFLMDIInput_TroubleCostNotIn_List(string request, int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMDIInput_TroubleCostNotIn_List(request, DITOMasterID);
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

        public void PODFLMDIInput_TroubleCostNotIn_SaveList(List<int> lst, int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMDIInput_TroubleCostNotIn_SaveList(lst, DITOMasterID);
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

        public void PODFLMDIInput_TroubleCost_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMDIInput_TroubleCost_DeleteList(lst);
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

        public void PODFLMDIInput_TroubleCostSave(List<DTOPODCATTroubleCost> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMDIInput_TroubleCostSave(lst);
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

        public DTOResult PODFLMDIInput_StationCostList(string request, int DITOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMDIInput_StationCostList(request, DITOMasterID);
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
        public void PODFLMDIInput_StationCostSave(List<DTOPODOPSDITOStation> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMDIInput_StationCostSave(lst);
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

        public DTOResult PODFLMDIInput_DriverList()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMDIInput_DriverList();
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

        public List<DTOPODCATTrouble> PODGroupOfTrouble_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODGroupOfTrouble_List();
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
        #endregion

        #region DTOPODFLMCOInput  (chi phí xe container nhà)
        public DTOResult PODFLMCOInput_List(string request, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMCOInput_List(request, dtFrom, dtTO);
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

        public List<DTOPODFLMDIInputExcel> PODFLMCOInput_Export(DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMCOInput_Export(dtFrom, dtTo);
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

        public void PODFLMCOInput_Import(List<DTOPODFLMDIInputImport> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMCOInput_Import(lst);
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
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMCOInput_Save(item);
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
        public void PODFLMCOInput_Approved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMCOInput_Approved(lst);
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

        public DTOPODFLMCOInputDriver PODFLMCOInput_GetDrivers(int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMCOInput_GetDrivers(COTOMasterID);
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
        public void PODFLMCOInput_SaveDrivers(DTOPODFLMCOInputDriver item, int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMCOInput_SaveDrivers(item, COTOMasterID);
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

        public DTOResult PODFLMCOInput_TroubleCostList(string request, int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMCOInput_TroubleCostList(request, COTOMasterID);
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

        public DTOResult PODFLMCOInput_TroubleCostNotIn_List(string request, int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMCOInput_TroubleCostNotIn_List(request, COTOMasterID);
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

        public void PODFLMCOInput_TroubleCostNotIn_SaveList(List<int> lst, int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMCOInput_TroubleCostNotIn_SaveList(lst, COTOMasterID);
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

        public void PODFLMCOInput_TroubleCost_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMCOInput_TroubleCost_DeleteList(lst);
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
        public void PODFLMCOInput_TroubleCostSave(List<DTOPODCATTroubleCost> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMCOInput_TroubleCostSave(lst);
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

        public DTOResult PODFLMCOInput_StationCostList(string request, int COTOMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMCOInput_StationCostList(request, COTOMasterID);
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
        public void PODFLMCOInput_StationCostSave(List<DTOPODOPSCOTOStation> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODFLMCOInput_StationCostSave(lst);
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

        public DTOResult PODFLMCOInput_DriverList()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODFLMCOInput_DriverList();
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
        #endregion

        #region PODImport
        public List<DTOPODImport> PODImport_Data(DateTime dtFrom, DateTime dtTO, int cusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODImport_Data(dtFrom, dtTO, cusId);
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

        public DTOResult PODImport_Index_Setting_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODImport_Index_Setting_List(request);
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

        public DTOCUSSettingPOD PODImport_Index_Setting_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODImport_Index_Setting_Get(id);
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

        public void PODImport_Excel_Import(int sID, List<DTOPODImport> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODImport_Excel_Import(sID, data);
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
        #endregion

        #region POD check
        public DTOResult PODDIInput_Check_List(string request, DateTime dtFrom, DateTime dtTO, List<int> listCustomerID, bool hasIsReturn)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_Check_List(request, dtFrom, dtTO, listCustomerID, hasIsReturn);
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
        public void PODDIInput_Check_Save(DTOPODDIInput item)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_Check_Save(item);
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

        public void PODDIInput_Check_SaveList(List<DTOPODDIInput> list)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_Check_SaveList(list);
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
        public void PODDIInput_Check_Reset(int DITOGroupID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_Check_Reset(DITOGroupID);
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
        #endregion

        #region PODMap
        public List<DTOPODImport> PODMapImport_Data(DateTime dtFrom, DateTime dtTO, int cusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODMapImport_Data(dtFrom, dtTO, cusId);
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

        public DTOResult PODMapImport_Index_Setting_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODMapImport_Index_Setting_List(request);
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

        public DTOCUSSettingPODMap PODMapImport_Index_Setting_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODMapImport_Index_Setting_Get(id);
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

        public void PODMapImport_Excel_Import(int sID, List<DTOPODImport> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODMapImport_Excel_Import(sID, data);
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

        public List<DTOPODInvoice> PODInvoice_Data(DateTime dtFrom, DateTime dtTO, int cusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODInvoice_Data(dtFrom, dtTO, cusId);
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

        public void PODInvoice_SaveList(List<DTOPODInvoice> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODInvoice_SaveList(data);
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
        #endregion

        #region POD UploadOrder
        public DTOResult PODDIInput_UploadOrder_List(string request, DateTime dtFrom, DateTime dtTO)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_UploadOrder_List(request, dtFrom, dtTO);
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
        public PODInputUpLoadOrderData PODDIInput_UploadOrder_GetData()
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_UploadOrder_GetData();
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
        public PODInputUpLoadOrder PODDIInput_UploadOrder_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_UploadOrder_Get(id);
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
        public void PODDIInput_UploadOrder_Save(PODInputUpLoadOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDIInput_UploadOrder_Save(item);
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
        public SYSExcel PODDIInput_UploadOrder_ExcelInit(int functionid, string functionkey, bool isreload, DateTime dtFrom, DateTime dtTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_UploadOrder_ExcelInit(functionid, functionkey, isreload, dtFrom, dtTo);
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

        public Row PODDIInput_UploadOrder_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_UploadOrder_ExcelChange(id, row, cells, lstMessageError);
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

        public SYSExcel PODDIInput_UploadOrder_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_UploadOrder_ExcelImport(id, lst, lstMessageError);
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

        public bool PODDIInput_UploadOrder_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDIInput_UploadOrder_ExcelApprove(id);
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
        #endregion

        #region Close data

        public DTOResult PODDI_CloseDataList(string request, DateTime dfrom, DateTime dto, List<int> listCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODDI_CloseDataList(request, dfrom, dto, listCustomerID);
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

        public void PODDI_CloseDataByDate(List<int> listCustomerID, List<DateTime> lst, bool isOpen)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODDI_CloseDataByDate(listCustomerID, lst, isOpen);
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

        public DTOResult PODCO_CloseDataList(string request, DateTime dfrom, DateTime dto, List<int> listCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    return bl.PODCO_CloseDataList(request, dfrom, dto, listCustomerID);
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

        public void PODCO_CloseDataByDate(List<int> listCustomerID, List<DateTime> lst, bool isOpen)
        {
            try
            {
                using (var bl = CreateBusiness<BLPOD>())
                {
                    bl.PODCO_CloseDataByDate(listCustomerID, lst, isOpen);
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
        #endregion
    }
}

