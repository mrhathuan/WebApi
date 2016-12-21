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
    public partial class SVCustomer : SVBase, ISVCustomer
    {
        #region Common
        public DTOResult Customer_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Customer_AllList();
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

        #region Customer
        public DTOResult Customer_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Customer_List(request);
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

        public void CUSCustomer_ApprovedList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSCustomer_ApprovedList(lst);
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

        public void CUSCustomer_UnApprovedList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSCustomer_UnApprovedList(lst);
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

        public CUSCustomer CustomerGetByID(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CustomerGetByID(customerid);
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

        public int Customer_Save(CUSCustomer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Customer_Save(item);
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

        public void Customer_Delete(CUSCustomer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Customer_Delete(item);
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

        public CUSSettingsCutOffTimeSuggest Customer_Setting_Get(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Customer_Setting_Get(customerid);
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

        public void Customer_Setting_Save(int customerid, CUSCustomer item, CUSSettingsCutOffTimeSuggest setting)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Customer_Setting_Save(customerid, item, setting);
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

        public void Customer_Generate_LocationArea(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Customer_Generate_LocationArea(customerid);
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

        #region Contact
        public CUSContact Contact_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Contact_Get(id);
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
        public DTOResult Contact_List(string request, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Contact_List(request, customerid);
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

        public int Contact_Save(CUSContact item, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Contact_Save(item, customerid);
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

        public void Contact_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Contact_Delete(id);
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

        #region GroupOfProduct
        public DTOResult GroupOfProductAll_List(DTORequest request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.GroupOfProductAll_List(request.ToKendo());
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
        public DTOCUSGroupOfProduct GroupOfProduct_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.GroupOfProduct_Get(id);
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

        public DTOResult GroupOfProduct_List(string request, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.GroupOfProduct_List(request, customerid);
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

        public int GroupOfProduct_Save(DTOCUSGroupOfProduct item, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.GroupOfProduct_Save(item, customerid);
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

        public void GroupOfProduct_Delete(DTOCUSGroupOfProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.GroupOfProduct_Delete(item);
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

        public CUSGroupOfProduct GroupOfProduct_GetByCode(string Code, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.GroupOfProduct_GetByCode(Code, customerid);
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

        public void GroupOfProduct_ResetPrice(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.GroupOfProduct_ResetPrice(customerid);
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

        #region GroupOfProduct In Stock
        public DTOResult GroupOfProductInStock_List(string request, int stockid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.GroupOfProductInStock_List(request, stockid);
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

        public DTOResult GroupOfProductNotInStock_List(string request, int stockid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.GroupOfProductNotInStock_List(request, stockid);
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

        public void GroupOfProductNotInStock_SaveList(List<int> data, int stockid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.GroupOfProductNotInStock_SaveList(data, stockid);
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

        public void GroupOfProductInStock_DeleteList(List<int> lstGroupID, int stockid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.GroupOfProductInStock_DeleteList(lstGroupID, stockid);
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

        #region Product
        public DTOCUSProduct Product_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Product_Get(id);
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
        public DTOResult Product_List(string request, int groupofprodutid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Product_List(request, groupofprodutid);
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

        public int Product_Save(DTOCUSProduct item, int groupofprodutid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Product_Save(item, groupofprodutid);
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

        public void Product_Delete(DTOCUSProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Product_Delete(item);
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

        public List<DTOCUSProduct_Export> CUS_Product_Export(int groupofprodutid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Product_Export(groupofprodutid);
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

        public DTOCUSProduct_Data CUS_Product_Check(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Product_Check(customerid);
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

        public void CUS_Product_Import(List<DTOCUSGroupProduct_Import> data, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUS_Product_Import(data, customerid);
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

        public SYSExcel CUS_Product_ExcelInit(int functionid, string functionkey,bool isreload, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Product_ExcelInit(functionid, functionkey,isreload, customerid);
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

        public  Row  CUS_Product_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Product_ExcelChange(id, row, cells, lstMessageError, customerID);
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

        public SYSExcel CUS_Product_ExcelImport(long id, List<Row> lst, List<string> lstMessageError, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Product_ExcelImport(id, lst,  lstMessageError, customerid);
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

        public bool CUS_Product_ExcelApprove(long id, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Product_ExcelApprove(id, customerid);
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

        #region Location

        #endregion

        #region Setting info
        public DTOCUSSettingInfo CUSSettingInfo_Get(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingInfo_Get(customerid);
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
        public void CUSSettingInfo_Save(DTOCUSSettingInfo item, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingInfo_Save(item, customerid);
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
        public DTOResult CUSSettingInfo_LocationList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingInfo_LocationList(request);
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

        #region Stock
        public DTOResult Stock_List(string request, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Stock_List(request, customerid);
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

        public DTOResult StockNotIn_List(string request, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.StockNotIn_List(request, customerid);
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

        public List<int> Stock_SaveList(List<DTOCUSLocation> data, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Stock_SaveList(data, customerid);
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

        public DTOCUSStock Stock_Save(DTOCUSStock item, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Stock_Save(item, customerid);
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

        public DTOCUSStock Stock_GetByID(int stockid, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Stock_GetByID(stockid, customerid);
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

        public void Stock_Delete(DTOCUSStock item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Stock_Delete(item);
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
                using (var bl = CreateBusiness<BLCustomer>())
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

        public void Stock_DeleteList(List<DTOCUSStock> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Stock_DeleteList(data);
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

        #region routing area (kho+ đối tác)
        public List<CATRoutingArea> Customer_Location_RoutingAreaList(int locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Customer_Location_RoutingAreaList(locationID);
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
        public DTOResult Customer_Location_RoutingAreaNotInList(int locationID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Customer_Location_RoutingAreaNotInList(locationID, request);
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
        public void Customer_Location_RoutingAreaNotInSave(List<int> lstAreaID, int locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Customer_Location_RoutingAreaNotInSave(lstAreaID, locationID);
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
        public void Customer_Location_RoutingAreaNotInDeleteList(List<int> lstAreaID, int locationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Customer_Location_RoutingAreaNotInDeleteList(lstAreaID, locationID);
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

        #region Routing
        public DTOResult Routing_List(string request, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Routing_List(request, customerID);
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

        public void Routing_Delete(DTOCUSRouting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Routing_Delete(item);
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

        public DTOResult RoutingNotIn_List(string request, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.RoutingNotIn_List(request, customerID);
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

        public void RoutingNotIn_SaveList(List<DTOCATRouting> data, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.RoutingNotIn_SaveList(data, customerID);
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

        public void Routing_Reset(int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Routing_Reset(customerID);
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

        #region Đối tác
        public DTOResult Partner_List(string request, int customerid, int typeofpartnerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Partner_List(request, customerid, typeofpartnerID);
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
        public DTOResult PartnerNotIn_List(string request, int customerid, int typePartner)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PartnerNotIn_List(request, customerid, typePartner);
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
        public List<int> Partner_SaveList(List<DTOCUSPartnerAll> data, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Partner_SaveList(data, customerid);
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
        public void Partner_Delete(int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Partner_Delete(cuspartnerid);
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
        public int Partner_Save(DTOCUSPartnerAllCustom item, int customerid, int typeOfPartner)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.Partner_Save(item, customerid, typeOfPartner);
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
        public List<DTOPartnerLocation_Excel> PartnerLocation_Export(int customerid, bool isCarrier, bool isSeaport, bool isDistributor)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PartnerLocation_Export(customerid, isCarrier, isSeaport, isDistributor);
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
        public DTOPartnerLocation_Check PartnerLocation_Check(int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PartnerLocation_Check(customerID);
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
        public List<AddressSearchItem> PartnerLocation_Import(List<DTOPartnerImport> lst, int customerid, bool isCarrier, bool isSeaport, bool isDistributor)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PartnerLocation_Import(lst, customerid, isCarrier, isSeaport, isDistributor);
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
        public DTOResult PartnerLocation_List(string request, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PartnerLocation_List(request, cuspartnerid);
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
        public List<DTOCUSLocation> PartnerLocation_SaveList(List<DTOCUSLocation> data, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PartnerLocation_SaveList(data, cuspartnerid);
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

        public DTOResult PartnerLocation_NotInList(string request, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PartnerLocation_NotInList(request, cuspartnerid);
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
        public List<int> PartnerLocation_SaveNotinList(List<int> lst, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PartnerLocation_SaveNotinList(lst, cuspartnerid);
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
        public DTOCUSPartnerLocation PartnerLocation_Save(DTOCUSPartnerLocation item, int cuspartnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PartnerLocation_Save(item, cuspartnerid);
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
        public DTOCUSPartnerLocation PartnerLocation_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PartnerLocation_Get(id);
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
        public void PartnerLocation_Delete(DTOCUSLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.PartnerLocation_Delete(item);
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
        public void PartnerLocation_DeleteList(List<DTOCUSLocation> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.PartnerLocation_DeleteList(data);
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

        #region excel onl mới
        public SYSExcel CUS_Partner_ExcelInit(int functionid, string functionkey, bool isreload, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_ExcelInit(functionid, functionkey, isreload, customerid);
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

        public Row CUS_Partner_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_ExcelChange(id, row, cells, lstMessageError, customerid);
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

        public SYSExcel CUS_Partner_ExcelImport(long id, List<Row> lst, List<string> lstMessageError, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_ExcelImport(id, lst, lstMessageError, customerid);
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

        public bool CUS_Partner_ExcelApprove(long id, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_ExcelApprove(id, customerid);
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

        public List<DTOCUSPartnerLocationAll> CUS_Partner_List(int customerid, List<int> lstPartner, List<int> lstLocation, bool isUseLocation)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_List(customerid, lstPartner, lstLocation, isUseLocation);
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
        public DTOCUSPartnerAllCustom CUS_Partner_Get(int id,int typeid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_Get(id,typeid);
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
        public int CUS_Partner_CUSLocationSaveCode(DTOCUSPartnerLocationAll item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_CUSLocationSaveCode(item);
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

        #region filter 
        public DTOResult CUS_Partner_FilterByPartner_List(string request, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_FilterByPartner_List(request, customerid);
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
        public DTOResult CUS_Partner_FilterByLocation_List(string request, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_FilterByLocation_List(request, customerid);
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
        public List<int> CUS_Partner_FilterByPartner_GetNumOfCusLocation(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_FilterByPartner_GetNumOfCusLocation(customerid);
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

        #region routing contract
        public DTOResult CUS_Partner_RoutingContract_List(string request, int customerid,int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_RoutingContract_List(request, customerid,locationid);
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
        public void CUS_Partner_RoutingContract_SaveList(List<int> lstAreaClear, List<int> lstAreaAdd, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                     bl.CUS_Partner_RoutingContract_SaveList(lstAreaClear, lstAreaAdd, locationid);
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
        public  void CUS_Partner_RoutingContract_NewRoutingSave(DTOCUSPartnerNewRouting item, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                     bl.CUS_Partner_RoutingContract_NewRoutingSave(item, customerid);
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
        public DTOCUSPartnerNewRouting CUS_Partner_RoutingContract_NewRoutingGet(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                  return  bl.CUS_Partner_RoutingContract_NewRoutingGet(customerid);
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
        public List<DTOCATContract> CUS_Partner_RoutingContract_ContractData(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_RoutingContract_ContractData(customerid);
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
        public void CUS_Partner_RoutingContract_NewAreaSave(CATRoutingArea item, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUS_Partner_RoutingContract_NewAreaSave(item, locationid);
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
        public DTOResult CUS_Partner_RoutingContract_AreaList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_RoutingContract_AreaList(request);
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

        #region Location
        public DTOResult CUS_Partner_RoutingLocationContract_List(string request, int customerid, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_RoutingLocationContract_List(request, customerid, locationid);
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
        public void CUS_Partner_RoutingLocationContract_NewRoutingSave(DTOCUSPartnerNewRouting item, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUS_Partner_RoutingLocationContract_NewRoutingSave(item, customerid);
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
        public DTOCUSPartnerNewRouting CUS_Partner_RoutingLocationContract_NewRoutingGet(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_RoutingLocationContract_NewRoutingGet(customerid);
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
        public DTOResult CUS_Partner_RoutingContract_LocationList(string request, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_RoutingContract_LocationList(request, customerid);
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

        #region CATDock
        public DTOResult CUS_Partner_StockDock_List(string request, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_StockDock_List(request, locationid);
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
        public void CUS_Partner_StockDock_Save(DTOCATDock item, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                     bl.CUS_Partner_StockDock_Save(item, locationid);
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
        public void CUS_Partner_StockDock_Delete(DTOCATDock item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUS_Partner_StockDock_Delete(item);
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
        public DTOCATDock CUS_Partner_StockDock_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUS_Partner_StockDock_Get(id);
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
        #endregion

        #endregion

        //Done
        #region CUSSettingORD

        public DTOResult CUSSettingOrder_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingOrder_List(request);
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
        public DTOCUSSettingOrder CUSSettingOrder_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingOrder_Get(id);
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
        public void CUSSettingOrder_Save(DTOCUSSettingOrder item, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingOrder_Save(item, id);
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
        public void CUSSettingOrder_Delete(DTOCUSSettingOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingOrder_Delete(item);
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
        public DTOCUSSettingStockData CUSSettingORD_StockByCus_List(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingORD_StockByCus_List(cusID);
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

        #region CUSSettingPOD

        public DTOResult CUSSettingPOD_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingPOD_List(request);
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
        public DTOCUSSettingPOD CUSSettingPOD_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingPOD_Get(id);
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
        public void CUSSettingPOD_Save(DTOCUSSettingPOD item, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingPOD_Save(item, id);
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
        public void CUSSettingPOD_Delete(DTOCUSSettingPOD item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingPOD_Delete(item);
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


        public DTOResult CUSSettingMON_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingMON_List(request);
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
        public DTOCUSSettingMON CUSSettingMON_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingMON_Get(id);
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
        public void CUSSettingMON_Save(DTOCUSSettingMON item, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingMON_Save(item, id);
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
        public void CUSSettingMON_Delete(DTOCUSSettingMON item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingMON_Delete(item);
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

        #region CUSSettingPlan

        public DTOResult CUSSettingPlan_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingPlan_List(request);
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
        public DTOCUSSettingPlan CUSSettingPlan_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingPlan_Get(id);
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
        public void CUSSettingPlan_Save(DTOCUSSettingPlan item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingPlan_Save(item);
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
        public void CUSSettingPlan_Delete(DTOCUSSettingPlan item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingPlan_Delete(item);
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
        public DTOResult CUSSettingPlan_SettingOrderList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingPlan_SettingOrderList(request);
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

        #region CUSSettingOderCode

        public DTOResult CUSSettingOrderCode_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingOrderCode_List(request);
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
        public DTOCUSSettingOrderCode CUSSettingOrderCode_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingOrderCode_Get(id);
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
        public void CUSSettingOrderCode_Save(DTOCUSSettingOrderCode item, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingOrderCode_Save(item, id);
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
        public void CUSSettingOrderCode_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingOrderCode_Delete(id);
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

        #region CUSSettingMONImport

        public DTOResult CUSSettingMONImport_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingMONImport_List(request);
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
        public DTOCUSSettingMONImport CUSSettingMONImport_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingMONImport_Get(id);
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
        public void CUSSettingMONImport_Save(DTOCUSSettingMONImport item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingMONImport_Save(item);
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
        public void CUSSettingMONImport_Delete(DTOCUSSettingMONImport item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingMONImport_Delete(item);
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
        public DTOResult CUSSettingMONImport_SettingOrderList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingMONImport_SettingOrderList(request);
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

        //MONExt
        public DTOResult CUSSettingMONExt_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingMONExt_List(request);
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
        public DTOCUSSettingMONExt CUSSettingMONExt_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingMONExt_Get(id);
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
        public void CUSSettingMONExt_Save(DTOCUSSettingMONExt item, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingMONExt_Save(item, id);
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
        public void CUSSettingMONExt_Delete(DTOCUSSettingMONExt item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingMONExt_Delete(item);
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

        #region CUSSettingPODMap

        public DTOResult CUSSettingPODMap_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingPODMap_List(request);
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
        public DTOCUSSettingPODMap CUSSettingPODMap_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingPODMap_Get(id);
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
        public void CUSSettingPODMap_Save(DTOCUSSettingPODMap item, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingPODMap_Save(item, id);
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
        public void CUSSettingPODMap_Delete(DTOCUSSettingPODMap item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingPODMap_Delete(item);
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

        #region CUSSettingORDPlan

        public DTOResult CUSSettingORDPlan_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingORDPlan_List(request);
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
        public DTOCUSSettingORDPlan CUSSettingORDPlan_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingORDPlan_Get(id);
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
        public void CUSSettingORDPlan_Save(DTOCUSSettingORDPlan item, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingORDPlan_Save(item, id);
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
        public void CUSSettingORDPlan_Delete(DTOCUSSettingORDPlan item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingORDPlan_Delete(item);
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
        //Done
        #region CUSContract

        #region Common
        public DTOResult CUSContract_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_List(request);
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

        public DTOCATContract CUSContract_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Get(id);
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

        public int CUSContract_Save(DTOCATContract item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Save(item);
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

        public void CUSContract_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Delete(id);
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

        public DTOCUSContract_Data CUSContract_Data(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Data(id);
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
        public DTOResult CUSContract_ByCustomerList(string request, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_ByCustomerList(request, customerID);
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
        public DTOResult CUSContract_ByCompanyList(string request, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_ByCompanyList(request, customerID);
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

        #region CODefault
        public DTOResult CUSContract_CODefault_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_CODefault_List(request, contractID);
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

        public DTOResult CUSContract_CODefault_NotInList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_CODefault_NotInList(request, contractID);
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

        public void CUSContract_CODefault_NotIn_SaveList(List<DTOCATPacking> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_CODefault_NotIn_SaveList(data, contractID);
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

        public void CUSContract_CODefault_Delete(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_CODefault_Delete(lst);
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

        public void CUSContract_CODefault_Update(List<DTOCATContractCODefault> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_CODefault_Update(data, contractID);
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

        #region Routing

        public DTOResult CUSContract_Routing_List(int contractID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Routing_List(contractID, request);
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

        public void CUSContract_Routing_Save(DTOCATContractRouting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Routing_Save(item);
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

        public string CUSContract_Routing_LeadTime_Check(string sExpression, DateTime requestDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Routing_LeadTime_Check(sExpression, requestDate);
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

        public void CUSContract_Routing_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Routing_Delete(id);
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
        public void CUSContract_Routing_NotIn_Delete(int id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Routing_NotIn_Delete(id, contractID);
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
        public DTOResult CUSContract_Routing_CATNotIn_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Routing_CATNotIn_List(request, contractID);
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
        public void CUSContract_Routing_CATNotIn_Save(List<DTOCATRouting> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Routing_CATNotIn_Save(data, contractID);
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
        public DTOResult CUSContract_Routing_NotIn_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Routing_NotIn_List(request, contractID);
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

        public void CUSContract_Routing_Insert(List<DTOCATRouting> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Routing_Insert(data, contractID);
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

        public List<DTOCUSContractRouting_Import> CUSContract_Routing_Export(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Routing_Export(contractID);
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

        public void CUSContract_Routing_Import(List<DTOCUSContractRouting_Import> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Routing_Import(data, contractID);
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

        public DTOCUSContractRoutingData CUSContract_RoutingByCus_List(int customerID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_RoutingByCus_List(customerID, contractID);
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

        public void CUSContract_KPI_Save(List<DTOContractKPITime> data, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_KPI_Save(data, routingID);
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

        public DTOResult CUSContract_KPI_Routing_List(string request, int contractID, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_KPI_Routing_List(request, contractID, routingID);
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

        public DateTime? CUSContract_KPI_Check_Expression(string sExpression, KPIKPITime item, double zone, double leadTime, List<DTOContractKPITime> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_KPI_Check_Expression(sExpression, item, zone, leadTime, lst);
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

        public bool? CUSContract_KPI_Check_Hit(string sExpression, string sField, KPIKPITime item, double zone, double leadTime, List<DTOContractKPITime> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_KPI_Check_Hit(sExpression, sField, item, zone, leadTime, lst);
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

        public void CUSContract_KPI_Routing_Apply(List<DTOCATContractRouting> data, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_KPI_Routing_Apply(data, routingID);
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

        public List<DTOContractTerm> CUSContract_Routing_ContractTermList(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Routing_ContractTermList(contractID);
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

        public SYSExcel CUSContract_Routing_ExcelOnline_Init(int contractID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Routing_ExcelOnline_Init(contractID, functionid, functionkey, isreload);
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

        public Row CUSContract_Routing_ExcelOnline_Change(int contractID, int customerID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Routing_ExcelOnline_Change(contractID, customerID, id, row, cells, lstMessageError);
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

        public SYSExcel CUSContract_Routing_ExcelOnline_Import(int contractID, int customerID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Routing_ExcelOnline_Import(contractID, customerID, id, lst, lstMessageError);
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

        public bool CUSContract_Routing_ExcelOnline_Approve(long id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Routing_ExcelOnline_Approve(id, contractID);
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

        #region tao routing moi
        public DTOCATRouting CUSContract_NewRouting_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_Get(ID);
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
        public int CUSContract_NewRouting_Save(DTOCATRouting item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_Save(item, contractID);
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
        //route by location
        public DTOResult CUSContract_NewRouting_LocationList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_LocationList(request);
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
        //route by area
        public DTOResult CUSContract_NewRouting_AreaList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_AreaList(request);
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
        public CATRoutingArea CUSContract_NewRouting_AreaGet(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_AreaGet(ID);
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
        public int CUSContract_NewRouting_AreaSave(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_AreaSave(item);
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
        public void CUSContract_NewRouting_AreaDelete(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_NewRouting_AreaDelete(item);
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
        public void CUSContract_NewRouting_AreaRefresh(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_NewRouting_AreaRefresh(item);
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

        public DTOResult CUSContract_NewRouting_AreaDetailList(string request, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_AreaDetailList(request, areaID);
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
        public DTOCATRoutingAreaDetail CUSContract_NewRouting_AreaDetailGet(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_AreaDetailGet(ID);
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
        public int CUSContract_NewRouting_AreaDetailSave(DTOCATRoutingAreaDetail item, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_AreaDetailSave(item, areaID);
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
        public void CUSContract_NewRouting_AreaDetailDelete(DTOCATRoutingAreaDetail item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_NewRouting_AreaDetailDelete(item);
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

        public DTOResult CUSContract_NewRouting_AreaLocation_List(string request, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_AreaLocation_List(request, areaID);
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
        public DTOResult CUSContract_NewRouting_AreaLocationNotIn_List(string request, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_NewRouting_AreaLocationNotIn_List(request, areaID);
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
        public void CUSContract_NewRouting_AreaLocationNotIn_Save(int areaID, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_NewRouting_AreaLocationNotIn_Save(areaID, lstID);
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
        public void CUSContract_NewRouting_AreaLocation_Delete(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_NewRouting_AreaLocation_Delete(lstID);
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
        public void CUSContract_NewRouting_AreaLocation_Copy(int areaID, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_NewRouting_AreaLocation_Copy(areaID, lstID);
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

        #endregion

        #region Price

        #region Common

        public DTOResult CUSContract_Price_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Price_List(request, contractID);
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

        public DTOPrice CUSContract_Price_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Price_Get(id);
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

        public DTOCUSPrice_Data CUSContract_Price_Data(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Price_Data(contractID);
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

        public int CUSContract_Price_Save(DTOPrice item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Price_Save(item, contractID);
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

        public void CUSContract_Price_Copy(List<DTOPriceCopyItem> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Price_Copy(data);
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

        public void CUSContract_Price_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Price_Delete(id);
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
        public void CUSContract_Price_DeletePriceNormal(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Price_DeletePriceNormal(priceID);
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
        public void CUSContract_Price_DeletePriceLevel(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Price_DeletePriceLevel(priceID);
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

        public DTOCUSPriceCO_Data CUSContract_PriceCO_Data(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_PriceCO_Data(contractID);
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
        public DTOPriceCOContainerExcelData CUSPrice_CO_COContainer_ExcelData(int priceid, int contractid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_COContainer_ExcelData(priceid, contractid);
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
        public void CUSPrice_CO_COContainer_ExcelImport(List<DTOPriceCOContainerImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_COContainer_ExcelImport(lst, priceID);
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

        public DTOCUSPrice_ExcelData CUSContract_Price_ExcelData(int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Price_ExcelData(contractTermID);
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

        public SYSExcel CUSPrice_CO_GroupContainer_ExcelInit(bool isFrame, int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
               using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_GroupContainer_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row CUSPrice_CO_GroupContainer_ExcelChange(bool isFrame, int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
               using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_GroupContainer_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
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

        public SYSExcel CUSPrice_CO_GroupContainer_ExcelOnImport(bool isFrame, int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
               using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_GroupContainer_ExcelOnImport(isFrame, priceID, contractTermID, id, lst, lstMessageError);
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

        public bool CUSPrice_CO_GroupContainer_ExcelApprove(bool isFrame, int priceID, int contractTermID, long id)
        {
            try
            {
               using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_GroupContainer_ExcelApprove(isFrame, priceID, contractTermID, id);
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

        #region DI_GroupVehicle

        #region old
        public List<DTOPriceGroupVehicle> CUSPrice_DI_GroupVehicle_GetData(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupVehicle_GetData(priceID);
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

        public void CUSPrice_DI_GroupVehicle_SaveList(List<DTOPriceGroupVehicle> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_GroupVehicle_SaveList(data, priceID);
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
        public DTOPriceGroupVehicleData CUSPrice_DI_GroupVehicle_ExcelData(int priceID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupVehicle_ExcelData(priceID, contractID);
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
        public void CUSPrice_DI_GroupVehicle_ExcelImport(List<DTOPriceGroupVehicleImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_GroupVehicle_ExcelImport(lst, priceID);
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

        public SYSExcel CUSPrice_DI_GroupVehicle_ExcelInit(bool isFrame, int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupVehicle_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row CUSPrice_DI_GroupVehicle_ExcelChange(bool isFrame, int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupVehicle_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
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

        public SYSExcel CUSPrice_DI_GroupVehicle_ExcelOnImport(bool isFrame, int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupVehicle_ExcelOnImport(isFrame, priceID, contractTermID, id, lst, lstMessageError);
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

        public bool CUSPrice_DI_GroupVehicle_ExcelApprove(bool isFrame, int priceID, int contractTermID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupVehicle_ExcelApprove(isFrame, priceID, contractTermID, id);
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

        #region new
        public List<DTOPriceGVLevelGroupVehicle> CUSPrice_DI_PriceGVLevel_DetailData(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceGVLevel_DetailData(priceID);
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
        public void CUSPrice_DI_PriceGVLevel_Save(List<DTOPriceGVLevelGroupVehicle> lst, int priceid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceGVLevel_Save(lst, priceid);
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

        public DTOPriceGVLevelData CUSPrice_DI_PriceGVLevel_ExcelData(int priceid, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceGVLevel_ExcelData(priceid, contractTermID);
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
        public void CUSPrice_DI_PriceGVLevel_ExcelImport(List<DTOPriceGVLevelImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceGVLevel_ExcelImport(lst, priceID);
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
        #endregion

        #region GroupProduct
        public List<DTOPriceDIGroupOfProduct> CUSPrice_DI_GroupProduct_Data(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupProduct_Data(priceID);
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

        public void CUSPrice_DI_GroupProduct_SaveList(List<DTOPriceDIGroupOfProduct> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_GroupProduct_SaveList(data, priceID);
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

        public DTOPriceDIGroupOfProductData CUSPrice_DI_GroupProduct_Export(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupProduct_Export(priceID);
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

        public void CUSPrice_DI_GroupProduct_Import(List<DTOPriceDIGroupOfProductImport> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_GroupProduct_Import(data, priceID);
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

        public SYSExcel CUSPrice_DI_GroupProduct_ExcelInit(bool isFrame, int priceID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupProduct_ExcelInit(isFrame, priceID, functionid, functionkey, isreload);
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
        public Row CUSPrice_DI_GroupProduct_ExcelChange(bool isFrame, int priceID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupProduct_ExcelChange(isFrame, priceID, id, row, cells, lstMessageError);
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

        public SYSExcel CUSPrice_DI_GroupProduct_ExcelOnImport(bool isFrame, int priceID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupProduct_ExcelOnImport(isFrame, priceID, id, lst, lstMessageError);
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

        public bool CUSPrice_DI_GroupProduct_ExcelApprove(bool isFrame, int priceID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_GroupProduct_ExcelApprove(isFrame, priceID, id);
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

        #region loading new

        #region comon
        public void CUSPrice_DI_Load_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Load_Delete(id);
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
        public void CUSPrice_DI_Load_DeleteList(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Load_DeleteList(data);
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
        public void CUSPrice_DI_Load_DeleteAllList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Load_DeleteAllList(priceID);
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

        #region Location
        public List<DTOPriceTruckDILoad> CUSPrice_DI_LoadLocation_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadLocation_List(priceID);
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

        public DTOResult CUSPrice_DI_LoadLocation_LocationNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadLocation_LocationNotIn_List(request, priceID);
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

        public void CUSPrice_DI_LoadLocation_LocationNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadLocation_LocationNotIn_SaveList(data, priceID);
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

        public void CUSPrice_DI_LoadLocation_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadLocation_SaveList(data);
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
        public void CUSPrice_DI_LoadLocation_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadLocation_DeleteList(priceID);
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

        //Excel
        public DTOPriceTruckDILoad_Export CUSPrice_DI_LoadLocation_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadLocation_Export(contractTermID, priceID);
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
        public void CUSPrice_DI_LoadLocation_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadLocation_Import(data, priceID);
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

        #region Route
        public List<DTOPriceTruckDILoad> CUSPrice_DI_LoadRoute_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadRoute_List(priceID);
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

        public DTOResult CUSPrice_DI_LoadRoute_RouteNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadRoute_RouteNotIn_List(request, priceID);
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

        public void CUSPrice_DI_LoadRoute_RouteNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadRoute_RouteNotIn_SaveList(data, priceID);
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

        public void CUSPrice_DI_LoadRoute_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadRoute_SaveList(data);
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
        public void CUSPrice_DI_LoadRoute_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadRoute_DeleteList(priceID);
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
        //Excel
        public DTOPriceTruckDILoad_Export CUSPrice_DI_LoadRoute_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadRoute_Export(contractTermID, priceID);
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

        public void CUSPrice_DI_LoadRoute_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadRoute_Import(data, priceID);
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

        #region TypeOfPartner
        public List<DTOPriceTruckDILoad> CUSPrice_DI_LoadPartner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadPartner_List(priceID);
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

        public DTOResult CUSPrice_DI_LoadPartner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadPartner_PartnerNotIn_List(request, priceID);
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

        public void CUSPrice_DI_LoadPartner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadPartner_PartnerNotIn_SaveList(data, priceID);
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

        public void CUSPrice_DI_LoadPartner_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadPartner_SaveList(data);
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
        public void CUSPrice_DI_LoadPartner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadPartner_DeleteList(priceID);
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
        //Excel
        public DTOPriceTruckDILoad_Export CUSPrice_DI_LoadPartner_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadPartner_Export(contractTermID, priceID);
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

        public void CUSPrice_DI_LoadPartner_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadPartner_Import(data, priceID);
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

        #region Partner
        public List<DTOPriceDILoadPartner> CUSPrice_DI_LoadPartner_Partner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadPartner_Partner_List(priceID);
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

        public DTOResult CUSPrice_DI_LoadPartner_Partner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_LoadPartner_Partner_PartnerNotIn_List(request, priceID);
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

        public void CUSPrice_DI_LoadPartner_Partner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadPartner_Partner_PartnerNotIn_SaveList(data, priceID);
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

        public void CUSPrice_DI_LoadPartner_Partner_SaveList(List<DTOPriceDILoadPartner> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadPartner_Partner_SaveList(data);
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

        public void CUSPrice_DI_LoadPartner_Partner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_LoadPartner_Partner_DeleteList(priceID);
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

        #region MOQ

        #region info
        public DTOResult CUSPrice_DI_PriceMOQLoad_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_List(request, priceID);
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
        public int CUSPrice_DI_PriceMOQLoad_Save(DTOPriceDIMOQLoad item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_Save(item, priceID);
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
        public void CUSPrice_DI_PriceMOQLoad_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_Delete(priceID);
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
        public DTOPriceDIMOQLoad CUSPrice_DI_PriceMOQLoad_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_Get(priceID);
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
        public void CUSPrice_DI_PriceMOQLoad_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_DeleteList(priceID);
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

        #region group location
        public DTOResult CUSPrice_DI_PriceMOQLoad_GroupLocation_List(string request, int priceMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_GroupLocation_List(request, priceMOQLoadID);
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
        public void CUSPrice_DI_PriceMOQLoad_GroupLocation_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_GroupLocation_DeleteList(lst);
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
        public void CUSPrice_DI_PriceMOQLoad_GroupLocation_SaveList(List<int> lst, int priceMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_GroupLocation_SaveList(lst, priceMOQLoadID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(string request, int priceMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(request, priceMOQLoadID);
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

        #region groupproduct
        public DTOResult CUSPrice_DI_PriceMOQLoad_GroupProduct_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_GroupProduct_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQLoad_GroupProduct_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_GroupProduct_DeleteList(lst);
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
        public DTOPriceDIMOQLoadGroupProduct CUSPrice_DI_PriceMOQLoad_GroupProduct_Get(int id, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_GroupProduct_Get(id, cusID);
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
        public void CUSPrice_DI_PriceMOQLoad_GroupProduct_Save(DTOPriceDIMOQLoadGroupProduct item, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_GroupProduct_Save(item, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_GroupProduct_GOPList(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_GroupProduct_GOPList(cusID);
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

        #region  location
        public DTOResult CUSPrice_DI_PriceMOQLoad_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_Location_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQLoad_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_Location_DeleteList(lst);
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
        public void CUSPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_Location_LocationNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_Location_LocationNotInList(request, priceExID, contractID);
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

        public DTOPriceDIMOQLoadLocation CUSPrice_DI_PriceMOQLoad_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_Location_Get(id);
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

        public void CUSPrice_DI_PriceMOQLoad_Location_Save(DTOPriceDIMOQLoadLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_Location_Save(item);
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

        #region route
        public DTOResult CUSPrice_DI_PriceMOQLoad_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_Route_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQLoad_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_Route_DeleteList(lst);
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
        public void CUSPrice_DI_PriceMOQLoad_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_Route_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_Route_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_Route_RouteNotInList(request, priceExID, contractID);
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

        #region parent route
        public DTOResult CUSPrice_DI_PriceMOQLoad_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_ParentRoute_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQLoad_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_ParentRoute_DeleteList(lst);
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
        public void CUSPrice_DI_PriceMOQLoad_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_ParentRoute_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(request, priceExID, contractID);
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

        #region province

        public DTOResult CUSPrice_DI_PriceMOQLoad_Province_List(string request, int PriceDIMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_Province_List(request, PriceDIMOQLoadID);
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
        public void CUSPrice_DI_PriceMOQLoad_Province_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_Province_DeleteList(lst);
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
        public void CUSPrice_DI_PriceMOQLoad_Province_SaveList(List<int> lst, int PriceDIMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQLoad_Province_SaveList(lst, PriceDIMOQLoadID);
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
        public DTOResult CUSPrice_DI_PriceMOQLoad_Province_NotInList(string request, int PriceDIMOQLoadID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQLoad_Province_NotInList(request, PriceDIMOQLoadID, contractID, CustomerID);
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

        #endregion

        #endregion

        #region price di Ex

        #region info
        public DTOResult CUSPrice_DI_Ex_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_List(request, priceID);
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
        public int CUSPrice_DI_Ex_Save(DTOPriceDIEx item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Save(item, priceID);
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

        public void CUSPrice_DI_Ex_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_Delete(priceID);
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

        public DTOPriceDIEx CUSPrice_DI_Ex_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Get(priceID);
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

        #region group location
        public DTOResult CUSPrice_DI_Ex_GroupLocation_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_GroupLocation_List(request, priceExID);
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
        public void CUSPrice_DI_Ex_GroupLocation_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_GroupLocation_DeleteList(lst);
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
        public void CUSPrice_DI_Ex_GroupLocation_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_GroupLocation_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_Ex_GroupLocation_GroupNotInList(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_GroupLocation_GroupNotInList(request, priceExID);
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

        #region groupproduct
        public DTOResult CUSPrice_DI_Ex_GroupProduct_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_GroupProduct_List(request, priceExID);
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
        public void CUSPrice_DI_Ex_GroupProduct_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_GroupProduct_DeleteList(lst);
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
        public DTOPriceDIExGroupProduct CUSPrice_DI_Ex_GroupProduct_Get(int id, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_GroupProduct_Get(id, cusID);
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
        public void CUSPrice_DI_Ex_GroupProduct_Save(DTOPriceDIExGroupProduct item, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_GroupProduct_Save(item, priceExID);
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
        public DTOResult CUSPrice_DI_Ex_GroupProduct_GOPList(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_GroupProduct_GOPList(cusID);
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

        #region  location
        public DTOResult CUSPrice_DI_Ex_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Location_List(request, priceExID);
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
        public void CUSPrice_DI_Ex_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_Location_DeleteList(lst);
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
        public DTOPriceDIExLocation CUSPrice_DI_Ex_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Location_Get(id);
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
        public void CUSPrice_DI_Ex_Location_Save(DTOPriceDIExLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_Location_Save(item);
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
        public void CUSPrice_DI_Ex_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_Location_LocationNotInSaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_Ex_Location_LocationNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Location_LocationNotInList(request, priceExID, contractID);
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

        #region route
        public DTOResult CUSPrice_DI_Ex_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Route_List(request, priceExID);
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
        public void CUSPrice_DI_Ex_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_Route_DeleteList(lst);
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
        public void CUSPrice_DI_Ex_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_Route_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_Ex_Route_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Route_RouteNotInList(request, priceExID, contractID);
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

        #region parent route
        public DTOResult CUSPrice_DI_Ex_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_ParentRoute_List(request, priceExID);
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
        public void CUSPrice_DI_Ex_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_ParentRoute_DeleteList(lst);
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
        public void CUSPrice_DI_Ex_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_ParentRoute_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_Ex_ParentRoute_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_ParentRoute_RouteNotInList(request, priceExID, contractID);
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

        #region parent
        public DTOResult CUSPrice_DI_Ex_Partner_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Partner_List(request, priceExID);
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
        public void CUSPrice_DI_Ex_Partner_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_Partner_DeleteList(lst);
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
        public void CUSPrice_DI_Ex_Partner_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_Partner_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_Ex_Partner_PartnerNotInList(string request, int priceExID, int contractID,int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Partner_PartnerNotInList(request, priceExID, contractID, CustomerID);
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

        #region province

        public DTOResult CUSPrice_DI_Ex_Province_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Province_List(request, priceExID);
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
        public void CUSPrice_DI_Ex_Province_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_Province_DeleteList(lst);
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
        public void CUSPrice_DI_Ex_Province_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_Ex_Province_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_Ex_Province_NotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_Ex_Province_NotInList(request, priceExID, contractID, CustomerID);
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

        #endregion

        #region moq new

        #region info
        public DTOResult CUSPrice_DI_PriceMOQ_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_List(request, priceID);
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
        public int CUSPrice_DI_PriceMOQ_Save(DTOCATPriceDIMOQ item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Save(item, priceID);
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

        public void CUSPrice_DI_PriceMOQ_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_Delete(id);
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
        public DTOCATPriceDIMOQ CUSPrice_DI_PriceMOQ_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Get(priceID);
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
        public void CUSPrice_DI_PriceMOQ_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_DeleteList(priceID);
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

        #region group location
        public DTOResult CUSPrice_DI_PriceMOQ_GroupLocation_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_GroupLocation_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQ_GroupLocation_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_GroupLocation_DeleteList(lst);
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
        public void CUSPrice_DI_PriceMOQ_GroupLocation_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_GroupLocation_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(request, priceExID);
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

        #region groupproduct
        public DTOResult CUSPrice_DI_PriceMOQ_GroupProduct_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_GroupProduct_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQ_GroupProduct_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_GroupProduct_DeleteList(lst);
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
        public DTOPriceDIMOQGroupProduct CUSPrice_DI_PriceMOQ_GroupProduct_Get(int id, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_GroupProduct_Get(id, cusID);
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
        public void CUSPrice_DI_PriceMOQ_GroupProduct_Save(DTOPriceDIMOQGroupProduct item, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_GroupProduct_Save(item, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_GroupProduct_GOPList(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_GroupProduct_GOPList(cusID);
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

        #region  location
        public DTOResult CUSPrice_DI_PriceMOQ_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Location_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQ_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_Location_DeleteList(lst);
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
        public DTOPriceDIMOQLocation CUSPrice_DI_PriceMOQ_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Location_Get(id);
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
        public void CUSPrice_DI_PriceMOQ_Location_Save(DTOPriceDIMOQLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_Location_Save(item);
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
        public void CUSPrice_DI_PriceMOQ_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_Location_LocationNotInSaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_Location_LocationNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Location_LocationNotInList(request, priceExID, contractID);
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

        #region route
        public DTOResult CUSPrice_DI_PriceMOQ_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Route_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQ_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_Route_DeleteList(lst);
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
        public void CUSPrice_DI_PriceMOQ_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_Route_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_Route_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Route_RouteNotInList(request, priceExID, contractID);
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

        #region parent route
        public DTOResult CUSPrice_DI_PriceMOQ_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_ParentRoute_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQ_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_ParentRoute_DeleteList(lst);
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
        public void CUSPrice_DI_PriceMOQ_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_ParentRoute_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(request, priceExID, contractID);
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

        #region level

        #endregion

        #region parent
        public DTOResult CUSPrice_DI_PriceMOQ_Partner_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Partner_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQ_Partner_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_Partner_DeleteList(lst);
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
        public void CUSPrice_DI_PriceMOQ_Partner_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_Partner_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_Partner_PartnerNotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Partner_PartnerNotInList(request, priceExID, contractID, CustomerID);
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

        #region province

        public DTOResult CUSPrice_DI_PriceMOQ_Province_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Province_List(request, priceExID);
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
        public void CUSPrice_DI_PriceMOQ_Province_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_Province_DeleteList(lst);
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
        public void CUSPrice_DI_PriceMOQ_Province_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQ_Province_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_DI_PriceMOQ_Province_NotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQ_Province_NotInList(request, priceExID, contractID, CustomerID);
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

        #endregion

        #region Unload

        #region comon
        public void CUSPrice_DI_UnLoad_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoad_Delete(id);
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
        public void CUSPrice_DI_UnLoad_DeleteList(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoad_DeleteList(data);
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
        public void CUSPrice_DI_UnLoad_DeleteAllList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoad_DeleteAllList(priceID);
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

        #region Location
        public List<DTOPriceTruckDILoad> CUSPrice_DI_UnLoadLocation_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadLocation_List(priceID);
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

        public DTOResult CUSPrice_DI_UnLoadLocation_LocationNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadLocation_LocationNotIn_List(request, priceID);
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

        public void CUSPrice_DI_UnLoadLocation_LocationNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadLocation_LocationNotIn_SaveList(data, priceID);
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

        public void CUSPrice_DI_UnLoadLocation_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadLocation_SaveList(data);
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
        public void CUSPrice_DI_UnLoadLocation_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadLocation_DeleteList(priceID);
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

        public DTOPriceTruckDILoad_Export CUSPrice_DI_UnLoadLocation_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadLocation_Export(contractTermID, priceID);
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

        public void CUSPrice_DI_UnLoadLocation_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadLocation_Import(data, priceID);
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

        #region Route
        public List<DTOPriceTruckDILoad> CUSPrice_DI_UnLoadRoute_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadRoute_List(priceID);
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

        public DTOResult CUSPrice_DI_UnLoadRoute_RouteNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadRoute_RouteNotIn_List(request, priceID);
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

        public void CUSPrice_DI_UnLoadRoute_RouteNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadRoute_RouteNotIn_SaveList(data, priceID);
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

        public void CUSPrice_DI_UnLoadRoute_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadRoute_SaveList(data);
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
        public void CUSPrice_DI_UnLoadRoute_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadRoute_DeleteList(priceID);
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

        public DTOPriceTruckDILoad_Export CUSPrice_DI_UnLoadRoute_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadRoute_Export(contractTermID, priceID);
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

        public void CUSPrice_DI_UnLoadRoute_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadRoute_Import(data, priceID);
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

        #region TypeOfPartner
        public List<DTOPriceTruckDILoad> CUSPrice_DI_UnLoadPartner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadPartner_List(priceID);
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

        public DTOResult CUSPrice_DI_UnLoadPartner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadPartner_PartnerNotIn_List(request, priceID);
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

        public void CUSPrice_DI_UnLoadPartner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadPartner_PartnerNotIn_SaveList(data, priceID);
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

        public void CUSPrice_DI_UnLoadPartner_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadPartner_SaveList(data);
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
        public void CUSPrice_DI_UnLoadPartner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadPartner_DeleteList(priceID);
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

        public DTOPriceTruckDILoad_Export CUSPrice_DI_UnLoadPartner_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadPartner_Export(contractTermID, priceID);
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

        public void CUSPrice_DI_UnLoadPartner_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadPartner_Import(data, priceID);
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

        #region Partner
        public List<DTOPriceDILoadPartner> CUSPrice_DI_UnLoadPartner_Partner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadPartner_Partner_List(priceID);
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

        public DTOResult CUSPrice_DI_UnLoadPartner_Partner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_UnLoadPartner_Partner_PartnerNotIn_List(request, priceID);
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

        public void CUSPrice_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(data, priceID);
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

        public void CUSPrice_DI_UnLoadPartner_Partner_SaveList(List<DTOPriceDILoadPartner> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadPartner_Partner_SaveList(data);
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
        public void CUSPrice_DI_UnLoadPartner_Partner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_UnLoadPartner_Partner_DeleteList(priceID);
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

        #region MOQ
        #region info
        public DTOResult CUSPrice_DI_PriceMOQUnLoad_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQUnLoad_List(request, priceID);
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
        public int CUSPrice_DI_PriceMOQUnLoad_Save(DTOPriceDIMOQLoad item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQUnLoad_Save(item, priceID);
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

        public void CUSPrice_DI_PriceMOQUnLoad_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQUnLoad_Delete(priceID);
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

        public DTOPriceDIMOQLoad CUSPrice_DI_PriceMOQUnLoad_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceMOQUnLoad_Get(priceID);
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
        public void CUSPrice_DI_PriceMOQUnLoad_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceMOQUnLoad_DeleteList(priceID);
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
        #endregion

        #region Area

        #endregion

        #endregion

        #region CO_PackingPrice

        public List<DTOPriceRouting> CUSPrice_CO_COPackingPrice_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_COPackingPrice_List(priceID);
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

        public void CUSPrice_CO_COPackingPrice_SaveList(List<DTOPriceRouting> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_COPackingPrice_SaveList(data, priceID);
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

        public DTOPriceContainer_Export CUSPrice_CO_COPackingPrice_Export(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_COPackingPrice_Export(priceID);
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

        public void CUSPrice_CO_COPackingPrice_Import(List<DTOPrice_COPackingPrice_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_COPackingPrice_Import(data, priceID);
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

        #region CO_Service

        public DTOResult CUSPrice_CO_Service_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Service_List(request, priceID);
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

        public DTOResult CUSPrice_CO_ServicePacking_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_ServicePacking_List(request, priceID);
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

        public DTOCATPriceCOService CUSPrice_CO_Service_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Service_Get(id);
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

        public DTOCATPriceCOService CUSPrice_CO_ServicePacking_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_ServicePacking_Get(id);
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

        public int CUSPrice_CO_Service_Save(DTOCATPriceCOService item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Service_Save(item, priceID);
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

        public void CUSPrice_CO_Service_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Service_Delete(id);
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

        public DTOResult CUSPrice_CO_CATService_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_CATService_List();
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

        public DTOResult CUSPrice_CO_CATServicePacking_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_CATServicePacking_List();
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

        public DTOResult CUSPrice_CO_CATCODefault_List(int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_CATCODefault_List(contractTermID);
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

        #region PriceDI level

        public List<DTOPriceDILevelGroupProduct> CUSPrice_DI_PriceLevel_DetailData(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceLevel_DetailData(priceID);
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
        public void CUSPrice_DI_PriceLevel_Save(List<DTOPriceDILevelGroupProduct> lst, int priceid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceLevel_Save(lst, priceid);
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

        public DTOPriceDILevelData CUSPrice_DI_PriceLevel_ExcelData(int priceid, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceLevel_ExcelData(priceid, contractTermID);
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
        public void CUSPrice_DI_PriceLevel_ExcelImport(List<DTOPriceDILevelImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_DI_PriceLevel_ExcelImport(lst, priceID);
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

        public SYSExcel CUSPrice_DI_PriceGVLevel_ExcelInit(bool isFrame, int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceGVLevel_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row CUSPrice_DI_PriceGVLevel_ExcelChange(bool isFrame, int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceGVLevel_ExcelChange(isFrame,priceID, contractTermID, id, row, cells, lstMessageError);
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

        public SYSExcel CUSPrice_DI_PriceGVLevel_ExcelOnImport(bool isFrame, int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceGVLevel_ExcelOnImport(isFrame,priceID, contractTermID, id, lst, lstMessageError);
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

        public bool CUSPrice_DI_PriceGVLevel_ExcelApprove(bool isFrame, int priceID, int contractTermID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceGVLevel_ExcelApprove(isFrame,priceID, contractTermID, id);
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

        public SYSExcel CUSPrice_DI_PriceLevel_ExcelInit(int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceLevel_ExcelInit(priceID, contractTermID, functionid, functionkey, isreload);
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
        public Row CUSPrice_DI_PriceLevel_ExcelChange(int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceLevel_ExcelChange(priceID, contractTermID,id, row, cells, lstMessageError);
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

        public SYSExcel CUSPrice_DI_PriceLevel_OnExcelImport(int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceLevel_OnExcelImport(priceID, contractTermID,id, lst, lstMessageError);
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

        public bool CUSPrice_DI_PriceLevel_ExcelApprove(int priceID, int contractTermID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_DI_PriceLevel_ExcelApprove(priceID, contractTermID, id);
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

        #region price CO container
        public DTOPriceCOContainerData CUSPrice_CO_COContainer_Data(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_COContainer_Data(priceID);
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

        public void CUSPrice_CO_COContainer_SaveList(List<DTOPriceCOContainer> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_COContainer_SaveList(data, priceID);
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
        //public DTOPriceGroupVehicleData CUSPrice_DI_GroupVehicle_ExcelData(int priceID, int contractID)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLCustomer>())
        //        {
        //            return bl.CUSPrice_DI_GroupVehicle_ExcelData(priceID, contractID);
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}
        //public void CUSPrice_DI_GroupVehicle_ExcelImport(List<DTOPriceGroupVehicleImport> lst, int priceID)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLCustomer>())
        //        {
        //            bl.CUSPrice_DI_GroupVehicle_ExcelImport(lst, priceID);
        //        }
        //    }
        //    catch (FaultException<DTOError> ex)
        //    {
        //        throw ex;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw FaultHelper.ServiceFault(ex);
        //    }
        //}
        public DTOResult CUSPrice_CO_COContainer_ContainerList(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_COContainer_ContainerList(request, priceID);
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
        public void CUSPrice_CO_COContainer_ContainerNotInSave(List<int> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_COContainer_ContainerNotInSave(lst, priceID);
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
        public DTOResult CUSPrice_CO_COContainer_ContainerNotInList(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_COContainer_ContainerNotInList(request, priceID);
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
        public void CUSPrice_CO_COContainer_ContainerDelete(List<int> lstPacking, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_COContainer_ContainerDelete(lstPacking, priceID);
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

        #region price co EX
        #region info
        public DTOResult CUSPrice_CO_Ex_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_List(request, priceID);
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
        public int CUSPrice_CO_Ex_Save(DTOPriceCOEx item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_Save(item, priceID);
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

        public void CUSPrice_CO_Ex_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Ex_Delete(priceID);
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

        public DTOPriceCOEx CUSPrice_CO_Ex_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_Get(priceID);
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

        #region  location
        public DTOResult CUSPrice_CO_Ex_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_Location_List(request, priceExID);
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
        public void CUSPrice_CO_Ex_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Ex_Location_DeleteList(lst);
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
        public DTOPriceCOExLocation CUSPrice_CO_Ex_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_Location_Get(id);
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
        public void CUSPrice_CO_Ex_Location_Save(DTOPriceCOExLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Ex_Location_Save(item);
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
        public void CUSPrice_CO_Ex_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Ex_Location_LocationNotInSaveList(lst, priceExID);
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
        public DTOResult CUSPrice_CO_Ex_Location_LocationNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_Location_LocationNotInList(request, priceExID, contractID);
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

        #region route
        public DTOResult CUSPrice_CO_Ex_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_Route_List(request, priceExID);
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
        public void CUSPrice_CO_Ex_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Ex_Route_DeleteList(lst);
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
        public void CUSPrice_CO_Ex_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Ex_Route_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_CO_Ex_Route_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_Route_RouteNotInList(request, priceExID, contractID);
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

        #region parent route
        public DTOResult CUSPrice_CO_Ex_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_ParentRoute_List(request, priceExID);
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
        public void CUSPrice_CO_Ex_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Ex_ParentRoute_DeleteList(lst);
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
        public void CUSPrice_CO_Ex_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Ex_ParentRoute_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_CO_Ex_ParentRoute_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_ParentRoute_RouteNotInList(request, priceExID, contractID);
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

        #region partner
        public DTOResult CUSPrice_CO_Ex_Partner_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_Partner_List(request, priceExID);
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
        public void CUSPrice_CO_Ex_Partner_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Ex_Partner_DeleteList(lst);
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
        public void CUSPrice_CO_Ex_Partner_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSPrice_CO_Ex_Partner_SaveList(lst, priceExID);
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
        public DTOResult CUSPrice_CO_Ex_Partner_PartnerNotInList(string request, int priceExID, int contractID,int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSPrice_CO_Ex_Partner_PartnerNotInList(request, priceExID, contractID, CustomerID);
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

        #endregion

        #endregion

        #region GroupOfProduct

        public DTOResult CUSContract_GroupOfProduct_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_GroupOfProduct_List(request, contractID);
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

        public DTOCATContractGroupOfProduct CUSContract_GroupOfProduct_Get(int id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_GroupOfProduct_Get(id, contractID);
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

        public void CUSContract_GroupOfProduct_Save(DTOCATContractGroupOfProduct item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_GroupOfProduct_Save(item, contractID);
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

        public void CUSContract_GroupOfProduct_Delete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_GroupOfProduct_Delete(lstid);
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

        public double? CUSContract_GroupOfProduct_Check(DTOCATContractGroupOfProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_GroupOfProduct_Check(item);
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

        public SYSExcel CUSContract_GroupOfProduct_ExcelInit(int contractID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_GroupOfProduct_ExcelInit(contractID, functionid, functionkey, isreload);
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
        public Row CUSContract_GroupOfProduct_ExcelChange(int contractID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_GroupOfProduct_ExcelChange(contractID, id, row, cells, lstMessageError);
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

        public SYSExcel CUSContract_GroupOfProduct_ExcelImport(int contractID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_GroupOfProduct_ExcelImport(contractID, id, lst, lstMessageError);
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

        public bool CUSContract_GroupOfProduct_ExcelApprove(int contractID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_GroupOfProduct_ExcelApprove(contractID, id);
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

        #region cus contract material
        public DTOCUSPrice_MaterialData CUSContract_MaterialChange_Data(int contractMaterialID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_MaterialChange_Data(contractMaterialID);
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
        public void CUSContract_MaterialChange_Save(DTOCUSPrice_MaterialData item, int contractMaterialID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_MaterialChange_Save(item, contractMaterialID);
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

        #region Contract term(phụ lục hợp đồng)
        public DTOResult CUSContract_ContractTerm_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_ContractTerm_List(request, contractID);
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
        public DTOContractTerm CUSContract_ContractTerm_Get(int id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_ContractTerm_Get(id, contractID);
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
        public int CUSContract_ContractTerm_Save(DTOContractTerm item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_ContractTerm_Save(item, contractID);
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
        public void CUSContract_ContractTerm_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_ContractTerm_Delete(id);
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
        public void CUSContract_ContractTerm_Open(int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_ContractTerm_Open(termID);
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
        public void CUSContract_ContractTerm_Close(int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_ContractTerm_Close(termID);
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
        public DTOResult CUSContract_ContractTerm_Price_List(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_ContractTerm_Price_List(request, contractTermID);
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

        public void CUSTerm_Change_RemoveWarning(int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSTerm_Change_RemoveWarning(termID);
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

        #region KPI Term
        public DTOResult CUSContract_ContractTerm_KPITime_List(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_ContractTerm_KPITime_List(request, contractTermID);
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

        public void CUSContract_ContractTerm_KPITime_SaveExpr(DTOContractTerm_KPITime item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_ContractTerm_KPITime_SaveExpr(item);
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

        public DTOResult CUSContract_ContractTerm_KPITime_NotInList(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                   return bl.CUSContract_ContractTerm_KPITime_NotInList(request, contractTermID);
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

        public DateTime? CUSContract_KPITime_Check_Expression(KPITimeDate item, List<KPITimeDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_KPITime_Check_Expression(item, lst);
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

        public bool? CUSContract_KPITime_Check_Hit(KPITimeDate item, List<KPITimeDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_KPITime_Check_Hit(item, lst);
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

        public void CUSContract_ContractTerm_KPITime_SaveNotInList(List<DTOContractTerm_TypeOfKPI> lst, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_ContractTerm_KPITime_SaveNotInList(lst, contractTermID);
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

        public DTOResult CUSContract_ContractTerm_KPIQuantity_List(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_ContractTerm_KPIQuantity_List(request, contractTermID);
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

        public void CUSContract_ContractTerm_KPIQuantity_SaveExpr(DTOContractTerm_KPIQuantity item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_ContractTerm_KPIQuantity_SaveExpr(item);
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

        public DTOResult CUSContract_ContractTerm_KPIQuantity_NotInList(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_ContractTerm_KPIQuantity_NotInList(request, contractTermID);
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

        public void CUSContract_ContractTerm_KPIQuantity_SaveNotInList(List<DTOContractTerm_TypeOfKPI> lst, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_ContractTerm_KPIQuantity_SaveNotInList(lst, contractTermID);
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
        public List<KPIQuantityDate> CUSContract_KPIQuantity_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_KPIQuantity_Get(id);
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
        
        public KPIQuantityDate CUSContract_KPIQuantity_Check_Expression(KPIQuantityDate item, List<KPIQuantityDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_KPIQuantity_Check_Expression(item, lst);
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

        public bool? CUSContract_KPIQuantity_Check_Hit(KPIQuantityDate item, List<KPIQuantityDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_KPIQuantity_Check_Hit(item, lst);
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

        #endregion

        #region CusContract Setting
        public void CUSContract_Setting_TypeOfRunLevelSave(int typeID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Setting_TypeOfRunLevelSave(typeID, contractID);
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

        public void CUSContract_Setting_TypeOfSGroupProductChangeSave(int typeID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Setting_TypeOfSGroupProductChangeSave(typeID, contractID);
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

        public void CATContractSetting_Save(string setting, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CATContractSetting_Save(setting, contractID);
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

        public DTOResult CUSContract_Setting_GOVList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Setting_GOVList(request, contractID);
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
        public DTOCATContractGroupVehicle CUSContract_Setting_GOVGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Setting_GOVGet(id);
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
        public void CUSContract_Setting_GOVSave(DTOCATContractGroupVehicle item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Setting_GOVSave(item);
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
        public void CUSContract_Setting_GOVDeleteList(List<int> lst, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Setting_GOVDeleteList(lst, contractID);
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
        public DTOResult CUSContract_Setting_GOVNotInList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Setting_GOVNotInList(request, contractID);
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
        public void CUSContract_Setting_GOVNotInSave(List<int> lst, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Setting_GOVNotInSave(lst, contractID);
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

        public DTOResult CUSContract_Setting_LevelList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Setting_LevelList(request, contractID);
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
        public DTOCATContractLevel CUSContract_Setting_LevelGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Setting_LevelGet(id);
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
        public void CUSContract_Setting_LevelSave(DTOCATContractLevel item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Setting_LevelSave(item, contractID);
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
        public void CUSContract_Setting_LevelDeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSContract_Setting_LevelDeleteList(lst);
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

        public List<CATGroupOfVehicle> CUSContract_Setting_Level_GOVList(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSContract_Setting_Level_GOVList(contractID);
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

        #endregion

        public void Location_Check(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.Location_Check(customerid);
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

        #region Price History
        public DTOCUSPrice_HistoryData PriceHistory_CheckPrice(List<int> lstCusId, int transportModeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PriceHistory_CheckPrice(lstCusId, transportModeID);
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
        public DTOCUSPrice_HistoryData PriceHistory_GetDataOneUser(int cusID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PriceHistory_GetDataOneUser(cusID, contractID);
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

        public DTOCUSPrice_HistoryData PriceHistory_GetDataMulUser(List<int> listCusId, int transportModeID, int typePrice)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.PriceHistory_GetDataMulUser(listCusId, transportModeID, typePrice);
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

        #region common
        public List<AddressSearchItem> AddressSearch_ByCustomerList(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.AddressSearch_ByCustomerList(customerid);
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

        #region CUSSettingExtReturn
        public DTOResult CUSSettingExtReturn_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingExtReturn_List(request);
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
        public DTOCUSSettingExtReturn CUSSettingExtReturn_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    return bl.CUSSettingExtReturn_Get(id);
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
        public void CUSSettingExtReturn_Save(DTOCUSSettingExtReturn item,int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingExtReturn_Save(item, id);
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
        public void CUSSettingExtReturn_Delete(DTOCUSSettingExtReturn item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCustomer>())
                {
                    bl.CUSSettingExtReturn_Delete(item);
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

        #region CUSSettingManual
        #endregion
    }
}