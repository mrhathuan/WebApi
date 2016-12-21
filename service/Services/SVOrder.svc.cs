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
    public partial class SVOrder : SVBase, ISVOrder
    {
        public void Connect()
        {
            //return "";
        }

        #region Common
        public DTOResult ORDOrder_CustomerList()
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_CustomerList();
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

        public DTOResult ORDOrder_VendorList()
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_VendorList();
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

        public DTOResult ORDOrder_Contract_List(int cusID, int serID, int transID, bool isExpired)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Contract_List(cusID, serID, transID, isExpired);
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

        public DTOResult ORDOrder_ContractTemp_List(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_ContractTemp_List(contractID);
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

        public DTOResult ORDOrder_GroupOfProduct_List(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_GroupOfProduct_List(cusID);
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

        public DTOResult ORDOrder_Stock_List(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Stock_List(cusID);
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

        public DTOResult ORDOrder_CusLocation_List(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_CusLocation_List(cusID);
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

        public DTOResult ORDOrder_CusStock_List(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_CusStock_List(cusID);
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

        public DTOResult ORDOrder_ContainerService_List(int? contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_ContainerService_List(contractID);
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

        public DTOResult ORDOrder_ContractCODefault_List(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_ContractCODefault_List(contractID);
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

        public List<DTOORDCATTransportMode> ORDOrder_TransportMode_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_TransportMode_List();
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
        //Common
        public void ORDOrder_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_Delete(id);
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

        public int ORDOrder_GetView(int serID, int transID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_GetView(serID, transID);
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

        public int ORDOrder_GetViewFromCAT(int serID, int transID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_GetViewFromCAT(serID, transID);
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

        public decimal ORDOrder_PriceGroupVehicle(int contractID, int locationFromID, int locationToID, int GroupOfVehicleID, int typeOfOrderID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_PriceGroupVehicle(contractID, locationFromID, locationToID, GroupOfVehicleID, typeOfOrderID);
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

        public decimal ORDOrder_PriceGroupProduct(int contractID, int locationFromID, int locationToID, int groupOfProductID, int typeOfOrderID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_PriceGroupProduct(contractID, locationFromID, locationToID, groupOfProductID, typeOfOrderID);
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

        public decimal ORDOrder_PriceContainer(int contractID, int locationFromID, int locationToID, int typeOfContainerID, int typeOfOrderID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_PriceContainer(contractID, locationFromID, locationToID, typeOfContainerID, typeOfOrderID);
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

        public int ORDOrder_Contract_Change(int orderID, int? contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Contract_Change(orderID, contractID);
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

        public DTOORDOrder ORDOrder_GetDate(DTOORDOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_GetDate(item);
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

        public DTOORDData_Location ORDOrder_NewLocation_Save(DTOORDOrderNewLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_NewLocation_Save(item);
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
        public AddressSearchItem AddressSearch_List(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.AddressSearch_List(id);
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

        #region ORDOrder
        public DTOResult ORDOrder_List(string request, List<int> dataStatus, DateTime fDate, DateTime tDate, bool aDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderNew_List(request, dataStatus, fDate, tDate, aDate);
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

        public void ORDOrder_Copy(List<DTOORDOrder_Copy> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_Copy(lst);
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

        public void ORDOrder_DeleteList(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DeleteList(lstID);
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

        public List<DTOORDContainer_ToTender> ORDOrder_ToOPS(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                  return  bl.ORDOrder_ToOPS(lst);
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

        public List<DTOORDRoutingRefresh> ORDOrder_ToOPSCheck(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_ToOPSCheck(lst);
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

        public void ORDOrder_RoutingArea_Refresh(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_RoutingArea_Refresh(lst);
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

        public List<DTOORDContainer_ToOPS> ORDOrderContainer_ToOPSCheck(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderContainer_ToOPSCheck(data);
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

        public void ORDOrderContainer_ToOPSUpdate(List<DTOORDContainer_ToOPS> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderContainer_ToOPSUpdate(data);
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

        public void ORDOrder_ToTender(List<DTOORDContainer_ToTender> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_ToTender(lst);
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
        public void ORDOrder_UpdateWarning()
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_UpdateWarning();
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

        #region ORDOrder_DN
        public DTOResult ORDOrder_DN_List(string request, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DN_List(request, cusID);
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

        public List<DTOORDOrderDN> ORDOrder_DN_SORest_List(int customerid, bool IsAll, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DN_SORest_List(customerid, IsAll, fromDate, toDate);
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

        public List<DTOORDOrderDN> ORDOrder_DN_DNRest_List(int customerid, bool IsAll, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DN_DNRest_List(customerid, IsAll, fromDate, toDate);
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

        public List<DTOORDOrderDN> ORDOrder_DN_AllRest_List(int customerid, bool IsAll, DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DN_AllRest_List(customerid, IsAll, fromDate, toDate);
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

        #region Common Detail
        public DTOORDTruckLocalData ORDOrder_TruckLocal_Data(int cusID, int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_TruckLocal_Data(cusID, termID);
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

        public DTOORDIMEXData ORDOrder_IMEX_Data(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_IMEX_Data(cusID);
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

        public DTOORDLadenEmptyData ORDOrder_LadenEmpty_Data(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_LadenEmpty_Data(cusID);
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

        public DTOORDOrder ORDOrder_GetItem(int id, int cusID, int serID, int transID, int? contractID, int? contractTempID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_GetItem(id, cusID, serID, transID, contractID, contractTempID);
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

        public int ORDOrder_FTLLO_Save(DTOORDOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_FTLLO_Save(item);
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

        public int ORDOrder_LTLLO_Save(DTOORDOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_LTLLO_Save(item);
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

        public int ORDOrder_FCLLO_Save(DTOORDOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_FCLLO_Save(item);
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

        public int ORDOrder_FCLIMEX_Save(DTOORDOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_FCLIMEX_Save(item);
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

        public int ORDOrder_FCLLOEmpty_Save(DTOORDOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_FCLLOEmpty_Save(item);
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

        public int ORDOrder_FCLLOLaden_Save(DTOORDOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_FCLLOLaden_Save(item);
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

        #region Excel

        public DTOResult ORDOrder_Excel_Setting_List(string request)
        {
            {
                try
                {
                    using (var bl = CreateBusiness<BLOrder>())
                    {
                        return bl.ORDOrder_Excel_Setting_List(request);
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
        }

        public DTOCUSSettingOrder ORDOrder_Excel_Setting_Get(int id)
        {
            {
                try
                {
                    using (var bl = CreateBusiness<BLOrder>())
                    {
                        return bl.ORDOrder_Excel_Setting_Get(id);
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
        }

        public List<DTOCUSSettingOrderCode> ORDOrder_Excel_Setting_Code_Get()
        {
            {
                try
                {
                    using (var bl = CreateBusiness<BLOrder>())
                    {
                        return bl.ORDOrder_Excel_Setting_Code_Get();
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
        }

        public List<AddressSearchItem> ORDOrder_Excel_Location_Create(List<DTOORDOrder_Import_PartnerLocation> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Excel_Location_Create(data);
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

        public void ORDOrder_Excel_Product_Create(List<DTOORDOrder_Import_ProductNew> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_Excel_Product_Create(data);
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

        public DTOORDOrder_ImportCheck ORDOrder_Excel_Import_Data(int cusID)
        {
            {
                try
                {
                    using (var bl = CreateBusiness<BLOrder>())
                    {
                        return bl.ORDOrder_Excel_Import_Data(cusID);
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
        }

        public int ORDOrder_Excel_Import(int sID, CATFile file, List<DTOORDOrder_Import> data, bool isMon)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Excel_Import(sID, file, data, isMon);
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

        public List<DTOORDOrder_Export> ORDOrder_Excel_Export(int sID, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Excel_Export(sID, pID);
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

        public List<DTOCUSGroupOfProduct> GroupOfProduct_List(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.GroupOfProduct_List(customerid);
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

        public DTOORDOrder_ImportCheck ORDOrder_ExcelOnline_Import_Data(int cusID)
        {
            {
                try
                {
                    using (var bl = CreateBusiness<BLOrder>())
                    {
                        return bl.ORDOrder_ExcelOnline_Import_Data(cusID);
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
        }

        public SYSExcel ORDOrder_ExcelOnline_Init(int templateID, int customerID, int pID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_ExcelOnline_Init(templateID, customerID, pID, functionid, functionkey, isreload);
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

        public DTOORDOrder_ImportRowResult ORDOrder_ExcelOnline_Change(int templateID, int customerID, DTOORDOrder_ImportOnline objImport, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_ExcelOnline_Change(templateID, customerID, objImport, id, row, cells, lstMessageError);
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

        public DTOORDOrder_ImportResult ORDOrder_ExcelOnline_Import(int templateID, int customerID, long id, List<Row> lst, List<DTOORDOrder_ImportOnline> lstDetail, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_ExcelOnline_Import(templateID, customerID, id, lst, lstDetail, lstMessageError);
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

        public DTOORDOrder_ExcelOnline_ApproveResult ORDOrder_ExcelOnline_Approve(long id, int templateID, CATFile file)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_ExcelOnline_Approve(id, templateID, file);
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

        public void ORDOrder_ExcelOnline_LocationToSave(long id, int templateID, List<DTOORDOrder_ImportRowResult> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_ExcelOnline_LocationToSave(id, templateID, lst);
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

        #region ORDPlan
        public DTOResult ORDOrder_Plan_Excel_Setting_List(string request)
        {
            {
                try
                {
                    using (var bl = CreateBusiness<BLOrder>())
                    {
                        return bl.ORDOrder_Plan_Excel_Setting_List(request);
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
        }

        public DTOCUSSettingORDPlan ORDOrder_Plan_Excel_Setting_Get(int id)
        {
            {
                try
                {
                    using (var bl = CreateBusiness<BLOrder>())
                    {
                        return bl.ORDOrder_Plan_Excel_Setting_Get(id);
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
        }
        public SYSExcel ORDOrder_Plan_ExcelOnline_Init(int templateID, int customerID, int pID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Plan_ExcelOnline_Init(templateID, customerID, pID, functionid, functionkey, isreload);
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

        public DTOORDOrder_Plan_ImportRowResult ORDOrder_Plan_ExcelOnline_Change(int templateID, int customerID, DTOORDOrder_ImportOnline objImport, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Plan_ExcelOnline_Change(templateID, customerID, objImport, id, row, cells, lstMessageError);
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

        public DTOORDOrder_Plan_ImportResult ORDOrder_Plan_ExcelOnline_Import(int templateID, int customerID, long id, List<Row> lst, List<DTOORDOrder_ImportOnline> lstDetail, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Plan_ExcelOnline_Import(templateID, customerID, id, lst, lstDetail, lstMessageError);
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

        public DTOORDOrder_Plan_ExcelOnline_ApproveResult ORDOrder_Plan_ExcelOnline_Approve(long id, int templateID, CATFile file)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Plan_ExcelOnline_Approve(id, templateID, file);
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

        public void ORDOrder_Plan_ExcelOnline_LocationToSave(long id, int templateID, List<DTOORDOrder_Plan_ImportRowResult> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_Plan_ExcelOnline_LocationToSave(id, templateID, lst);
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

        #region Inspection
        public DTOResult ORDOrder_TypeOfDocument_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_TypeOfDocument_List(request);
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

        public DTOORDTypeOfDocument ORDOrder_TypeOfDocument_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_TypeOfDocument_Get(id);
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

        public void ORDOrder_TypeOfDocument_Save(DTOORDTypeOfDocument item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_TypeOfDocument_Save(item);
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

        public void ORDOrder_TypeOfDocument_DeleteList(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_TypeOfDocument_DeleteList(lstID);
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


        public List<DTOORDTypeOfDocument> ORDOrder_TypeOfDocument_Read()
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_TypeOfDocument_Read();
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

        public DTOResult ORDOrder_Document_List(string request, DateTime dtfrom, DateTime dtto, List<int> lstCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Document_List(request, dtfrom, dtto, lstCustomerID);
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

        public DTOORDDocument ORDOrder_Document_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Document_Get(id);
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

        public void ORDOrder_Document_Save(DTOORDDocument item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_Document_Save(item);
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

        public void ORDOrder_Document_DeleteList(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_Document_DeleteList(lstID);
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

        public DTOResult ORDOrder_Document_OrderList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_Document_OrderList(request);
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


        public List<DTOORDDocumentService> ORDOrder_DocumentService_List(int documentID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DocumentService_List(documentID);
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

        public void ORDOrder_DocumentService_Save(int documentID, List<DTOORDDocumentService> lstService)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DocumentService_Save(documentID, lstService);
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

        public List<DTOORDDocumentService> ORDOrder_DocumentService_NotInList(int documentID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DocumentService_NotInList(documentID);
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

        public void ORDOrder_DocumentService_NotInList_Save(int documentID, List<int> lstServiceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DocumentService_NotInList_Save(documentID, lstServiceID);
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

        public void ORDOrder_DocumentService_DeleteList(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DocumentService_DeleteList(lstID);
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


        public List<DTOORDDocumentDetail> ORDOrder_DocumentDetail_List(int documentServiceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DocumentDetail_List(documentServiceID);
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

        public void ORDOrder_DocumentDetail_Save(int documentServiceID, DTOORDDocumentDetail item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DocumentDetail_Save(documentServiceID, item);
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

        public void ORDOrder_DocumentDetail_SaveList(int documentServiceID, List<DTOORDDocumentDetail> lstDetail)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DocumentDetail_SaveList(documentServiceID, lstDetail);
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

        public void ORDOrder_DocumentDetail_DeleteList(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DocumentDetail_DeleteList(lstID);
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

        public List<DTOORDDocumentDetail> ORDOrder_DocumentDetail_Export(int documentServiceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DocumentDetail_Export(documentServiceID);
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

        public void ORDOrder_DocumentDetail_Import(int documentServiceID, List<DTOORDDocumentDetail> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DocumentDetail_Import(documentServiceID, lst);
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


        public List<DTOORDDocumentContainer> ORDOrder_DocumentContainer_List(int documentID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DocumentContainer_List(documentID);
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

        public void ORDOrder_DocumentContainer_Complete(DTOORDDocumentContainer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DocumentContainer_Complete(item);
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

        public DTOResult ORDOrder_DocumentContainer_NotInList(int documentID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DocumentContainer_NotInList(documentID, request);
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

        public void ORDOrder_DocumentContainer_NotInList_Save(int documentID, List<int> lstContainerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DocumentContainer_NotInList_Save(documentID, lstContainerID);
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

        public void ORDOrder_DocumentContainer_DeleteList(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_DocumentContainer_DeleteList(lstID);
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


        public DTOORDContainerService_Data ORDOrder_ContainerInService_List(int documentID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_ContainerInService_List(documentID);
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

        public void ORDOrder_ContainerInService_NotInList_Save(int documentID, List<DTOORDContainerService_Detail> lstContainerService)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrder_ContainerInService_NotInList_Save(documentID, lstContainerService);
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


        public DTOResult ORDOrder_DocumentContainer_Read(string request, DateTime dtfrom, DateTime dtto, List<int> lstCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrder_DocumentContainer_Read(request, dtfrom, dtto, lstCustomerID);
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

        #region Tracking

        public List<DTOORDTracking_Order> ORD_Tracking_Order_List(List<int> dataCus)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_Tracking_Order_List(dataCus);
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

        public List<DTOORDTracking_TOMaster> ORD_Tracking_TripByOrder_List(int orderID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_Tracking_TripByOrder_List(orderID);
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

        public List<DTOORDTracking_Location> ORD_Tracking_LocationByTrip_List(int orderID, int tripID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_Tracking_LocationByTrip_List(orderID, tripID);
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

        #region Cancel
        public DTOResult ORD_DI_Cancel_List(string request, List<int> dataCus, DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_DI_Cancel_List(request, dataCus, fDate, tDate);
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

        public DTOResult ORD_CO_Cancel_List(string request, List<int> dataCus, DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_CO_Cancel_List(request, dataCus, fDate, tDate);
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

        public List<DTOCATReason> ORD_DI_Cancel_Reason_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_DI_Cancel_Reason_List();
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

        public void ORD_DI_Cancel_Change(int gopID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORD_DI_Cancel_Change(gopID, reasonID, reasonNote);
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

        public void ORD_CO_Cancel_Change(int conID, int reasonID, string reasonNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORD_CO_Cancel_Change(conID, reasonID, reasonNote);
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

        #region COTemp
        public DTOResult ORD_COTemp_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_COTemp_List(request);
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

        public DTOResult ORD_COTemp_Location_List(string request, int cusID, int carID, int serID, int transID, int nLocation)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_COTemp_Location_List(request, cusID, carID, serID, transID, nLocation);
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

        public DTOORDContainer_TempData ORD_COTemp_Data()
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_COTemp_Data();
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

        public List<DTOORDData_Partner> ORD_COTemp_Carrier_List(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_COTemp_Carrier_List(cusID);
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

        public void ORD_COTemp_SaveList(int total, DTOORDContainer_Temp item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORD_COTemp_SaveList(total, item);
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

        public void ORD_COTemp_Update(DTOORDContainer_Temp item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORD_COTemp_Update(item);
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

        public void ORD_COTemp_DeleteList(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORD_COTemp_DeleteList(data);
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

        public void ORD_COTemp_ToORD(List<int> data,bool isGenCode )
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORD_COTemp_ToORD(data, isGenCode);
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

        #region ORDPacket

        public List<DTOORDPAK> ORD_PAK_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_PAK_List();
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

        public DTOResult ORD_PAK_Order_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORD_PAK_Order_List(request, pID);
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

        #region ORDOrder_route
        public DTOResult ORDOrderRoute_List(string request, bool isClosed)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_List(request, isClosed);
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
        public DTOORDRoute ORDOrderRoute_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_Get(id);
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
        public int ORDOrderRoute_Save(DTOORDRoute item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_Save(item);
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
        public void ORDOrderRoute_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_Delete(id);
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

        public DTOResult ORDOrderRoute_OrderList(string request, int ordRouteId)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_OrderList(request, ordRouteId);
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
        public void ORDOrderRoute_OrderSaveList(List<int> lst, int ordRouteId)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_OrderSaveList(lst, ordRouteId);
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
        public void ORDOrderRoute_OrderDelete(int item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_OrderDelete(item);
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
        public DTOResult ORDOrderRoute_OrderNotInList(string request, int ordRouteId)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_OrderNotInList(request, ordRouteId);
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

        public DTOResult ORDOrderRoute_RouteDetailList(string request, int ordRouteId)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_RouteDetailList(request, ordRouteId);
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
        public DTOORDRouteDetail ORDOrderRoute_RouteDetailGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_RouteDetailGet(id);
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
        public int ORDOrderRoute_RouteDetailSave(DTOORDRouteDetail item, int ordRouteId)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_RouteDetailSave(item, ordRouteId);
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
        public void ORDOrderRoute_RouteDetailDelete(int item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_RouteDetailDelete(item);
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
        public void ORDOrderRoute_RouteDetailComplete(int item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_RouteDetailComplete(item);
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
        public void ORDOrderRoute_RouteDetailRun(int item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_RouteDetailRun(item);
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
        public DTOCATVessel ORDOrderRoute_RouteDetail_AddVessel(DTOCATVessel item, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_RouteDetail_AddVessel(item, cuspartnerid);
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

        public void ORDOrderRoute_OrderApproved(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_OrderApproved(id);
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
        public void ORDOrderRoute_OrderUnApproved(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_OrderUnApproved(id);
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

        public DTOORDRouteData ORDOrderRoute_LocationData()
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_LocationData();
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

        //STMS-2404
        public void ORDOrderRoute_CreateOrderChilds(int item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_CreateOrderChilds(item);
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
        public void ORDOrderRoute_ClearOrderChilds(int item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_ClearOrderChilds(item);
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


        public DTOResult ORDOrderRoute_RouteDetail_RouteContainerList(string request, int routeDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_RouteDetail_RouteContainerList(request, routeDetailID);
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
        public DTOORDRouteContainer ORDOrderRoute_RouteDetail_RouteContainerGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_RouteDetail_RouteContainerGet(id);
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
        public long ORDOrderRoute_RouteDetail_RouteContainerSave(DTOORDRouteContainer item, int routeDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_RouteDetail_RouteContainerSave(item, routeDetailID);
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
        public void ORDOrderRoute_RouteDetail_RouteContainerDetele(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_RouteDetail_RouteContainerDetele(id);
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

        public DTOResult ORDOrderRoute_RouteDetail_RouteProductList(string request, int routeDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_RouteDetail_RouteProductList(request, routeDetailID);
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
        public DTOResult ORDOrderRoute_RouteDetail_RouteProductNotInList(string request, int routeID, int routeDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_RouteDetail_RouteProductNotInList(request, routeID, routeDetailID);
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
        public void ORDOrderRoute_RouteDetail_RouteProductNotInSaveList(List<int> lst, int routeDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                     bl.ORDOrderRoute_RouteDetail_RouteProductNotInSaveList(lst, routeDetailID);
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

        public DTOResult ORDOrderRoute_RouteDetail_RouteProduct_ContainerList(string request, int routeDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    return bl.ORDOrderRoute_RouteDetail_RouteProduct_ContainerList(request, routeDetailID);
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

        public void  ORDOrderRoute_RouteDetail_RouteProduct_UpdateContainer(int ordRouteGroup, int ordRouteCont)
        {
            try
            {
                using (var bl = CreateBusiness<BLOrder>())
                {
                    bl.ORDOrderRoute_RouteDetail_RouteProduct_UpdateContainer(ordRouteGroup, ordRouteCont);
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