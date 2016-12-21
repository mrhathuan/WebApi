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
    public partial class SVVendor : SVBase, ISVVendor
    {
        #region Vendor
        public DTOResult Vendor_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Vendor_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENVendor_ApprovedList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENVendor_ApprovedList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENVendor_UnApprovedList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENVendor_UnApprovedList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOVendor Vendor_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Vendor_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Vendor_Save(DTOVendor item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Vendor_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Vendor_Delete(DTOVendor item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.Vendor_Delete(item);
                }
            }
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

        #region Company
        public DTOResult Company_List(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Company_List(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CompanyNotIn_List(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.CompanyNotIn_List(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CompanyNotIn_SaveList(List<DTOCUSCompany> lst, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.CompanyNotIn_SaveList(lst, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Company_DeleteList(List<DTOCUSCompany> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.Company_DeleteList(lst);
                }
            }
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

        #region Truck

        public List<CATGroupOfVehicle> VENGroupOfVehicleList()
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENGroupOfVehicleList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATTruck_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.CATTruck_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSVehicle Truck_Get(int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Truck_Get(vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Truck_List(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Truck_List(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Truck_Save(DTOCUSVehicle item, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Truck_Save(item, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Truck_Delete(DTOCUSVehicle item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.Truck_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCUSVehicle_Excel> Truck_Export(int customerID, string type)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Truck_Export(customerID, type);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSVehicle_Check Truck_Check(int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Truck_Check(customerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Truck_Import(List<DTOCUSVehicle_Excel> lst, int customerid, string type)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.Truck_Import(lst, customerid, type);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Truck_NotInList(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Truck_NotInList(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Truck_NotInSave(List<int> lst, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.Truck_NotInSave(lst, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VEN_Truck_ExcelInit(int customerID, string type, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Truck_ExcelInit(customerID, type,functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row VEN_Truck_ExcelChange(int customerID,long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Truck_ExcelChange(customerID,id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VEN_Truck_ExcelImport(int customerID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Truck_ExcelImport(customerID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VEN_Truck_ExcelApprove(int customerid, string type, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Truck_ExcelApprove(customerid,type,id);
                }
            }
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

        #region Tractor
        public DTOResult CATTractor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.CATTractor_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSVehicle Tractor_Get(int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Tractor_Get(vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Tractor_List(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Tractor_List(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Tractor_Save(DTOCUSVehicle item, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Tractor_Save(item, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Tractor_Delete(DTOCUSVehicle item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.Tractor_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult Tractor_NotInList(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Tractor_NotInList(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Tractor_NotInSave(List<int> lst, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.Tractor_NotInSave(lst, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VEN_Tractor_ExcelInit(int customerID, string type, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Tractor_ExcelInit(customerID, type, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row VEN_Tractor_ExcelChange(int customerID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Tractor_ExcelChange(customerID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VEN_Tractor_ExcelImport(int customerID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Tractor_ExcelImport(customerID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VEN_Tractor_ExcelApprove(int customerid, string type, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Tractor_ExcelApprove(customerid, type, id);
                }
            }
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

        #region Romooc
        public DTOResult CATRomooc_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.CATRomooc_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSRomooc Romooc_Get(int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Romooc_Get(vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Romooc_List(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Romooc_List(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Romooc_Save(DTOCUSRomooc item, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Romooc_Save(item, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Romooc_Delete(DTOCUSRomooc item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.Romooc_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult Romooc_NotInList(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Romooc_NotInList(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Romooc_NotInSave(List<int> lst, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.Romooc_NotInSave(lst, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public SYSExcel Romooc_ExcelInit(int vendorid, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Romooc_ExcelInit(vendorid,functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row Romooc_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Romooc_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel Romooc_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Romooc_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool Romooc_ExcelApprove(int vendorid, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.Romooc_ExcelApprove(vendorid,id);
                }
            }
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
                using (var bl = CreateBusiness<BLVendor>())
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
                using (var bl = CreateBusiness<BLVendor>())
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

        public DTOResult RoutingCusNotIn_List(string request, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.RoutingCusNotIn_List(request, customerID);
                }
            }
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
                using (var bl = CreateBusiness<BLVendor>())
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

        public void RoutingNotIn_SaveList(List<DTOCATRouting> lst, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.RoutingNotIn_SaveList(lst, customerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Routing_Update(int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.Routing_Update(vendorid);
                }
            }
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

        #region driver
        public DTOResult VENDriver_List(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENDriver_List(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOVENDriver VENDriver_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENDriver_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int VENDriver_Save(DTOVENDriver item, int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENDriver_Save(item, vendorID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENDriver_Delete(DTOVENDriver item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENDriver_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOVENDriverData VENDriver_Data(int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENDriver_Data(vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENDriver_Import(List<DTOVENDriverImport> lst, int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENDriver_Import(lst, vendorID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENDriver_NotInList(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENDriver_NotInList(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENDriver_NotInSave(List<DTOVENDriver> lst, int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENDriver_NotInSave(lst, vendorID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOVENDriverImport> VENDriver_ExportByVendor(int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENDriver_ExportByVendor(vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENDriver_DrivingLicence_List(string request, int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENDriver_DrivingLicence_List(request, driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATDriverLicence VENDriver_DrivingLicence_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENDriver_DrivingLicence_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENDriver_DrivingLicence_Save(DTOCATDriverLicence item, int driverID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENDriver_DrivingLicence_Save(item, driverID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENDriver_DrivingLicence_Delete(DTOCATDriverLicence item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENDriver_DrivingLicence_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VEN_Driver_ExcelInit(int vendorid, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Driver_ExcelInit(vendorid, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row VEN_Driver_ExcelChange(int vendorid, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Driver_ExcelChange(vendorid, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VEN_Driver_ExcelImport(int vendorid, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Driver_ExcelImport(vendorid, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VEN_Driver_ExcelApprove(int vendorid, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VEN_Driver_ExcelApprove(vendorid, id);
                }
            }
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
        public DTOResult GroupOfProductAll_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.GroupOfProductAll_List(request);
                }
            }
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
                using (var bl = CreateBusiness<BLVendor>())
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
                using (var bl = CreateBusiness<BLVendor>())
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
                using (var bl = CreateBusiness<BLVendor>())
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
                using (var bl = CreateBusiness<BLVendor>())
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
                using (var bl = CreateBusiness<BLVendor>())
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

        public DTOResult GroupOfProductMapping_List(string request, int groupOfProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.GroupOfProductMapping_List(request, groupOfProductID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult GroupOfProductMappingNotIn_List(string request, int groupOfProductID, int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.GroupOfProductMappingNotIn_List(request, groupOfProductID, vendorID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void GroupOfProductMapping_SaveList(List<DTOCUSGroupOfProductMapping> lst, int groupOfProductID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.GroupOfProductMapping_SaveList(lst, groupOfProductID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void GroupOfProductMapping_Delete(DTOCUSGroupOfProductMapping item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.GroupOfProductMapping_Delete(item);
                }
            }
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
        #region VENContract

        #region Common
        public DTOResult VENContract_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATContract VENContract_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int VENContract_Save(DTOCATContract item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOVENContract_Data VENContract_Data(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Data(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENContract_ByCustomerList(string request, int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_ByCustomerList(request, vendorID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOVENPriceCO_Data VENContract_PriceCO_Data(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_PriceCO_Data(contractID);
                }
            }
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
        public DTOResult VENContract_CODefault_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_CODefault_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_CODefault_NotInList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_CODefault_NotInList(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_CODefault_NotIn_SaveList(List<DTOCATPacking> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_CODefault_NotIn_SaveList(data, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_CODefault_Delete(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_CODefault_Delete(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_CODefault_Update(List<DTOCATContractCODefault> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_CODefault_Update(data, contractID);
                }
            }
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
        public DTOResult VENContract_Routing_List(int contractID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Routing_List(contractID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_Routing_Save(DTOCATContractRouting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Routing_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_Routing_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Routing_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_Routing_NotIn_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Routing_NotIn_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Routing_NotIn_Delete(int id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Routing_NotIn_Delete(id, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Routing_Insert(List<DTOCATRouting> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Routing_Insert(data, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOVENContractRouting_Import> VENContract_Routing_Export(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Routing_Export(contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_Routing_Import(List<DTOVENContractRouting_Import> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Routing_Import(data, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOVENContractRoutingData VENContract_RoutingByCus_List(int customerID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_RoutingByCus_List(customerID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_KPI_Save(List<DTOContractKPITime> data, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_KPI_Save(data, routingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_KPI_Routing_List(string request, int contractID, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_KPI_Routing_List(request, contractID, routingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DateTime? VENContract_KPI_Check_Expression(string sExpression, KPIKPITime item, double zone, double leadTime, List<DTOContractKPITime> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_KPI_Check_Expression(sExpression, item, zone, leadTime, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool? VENContract_KPI_Check_Hit(string sExpression, string sField, KPIKPITime item, double zone, double leadTime, List<DTOContractKPITime> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_KPI_Check_Hit(sExpression, sField, item, zone, leadTime, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_KPI_Routing_Apply(List<DTOCATContractRouting> data, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_KPI_Routing_Apply(data, routingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENContract_Routing_CATNotIn_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Routing_CATNotIn_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Routing_CATNotIn_Save(List<DTOCATRouting> data, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Routing_CATNotIn_Save(data, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOContractTerm> VENContract_Routing_ContractTermList(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Routing_ContractTermList(contractID);
                }
            }
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
        public DTOCATRouting VENContract_NewRouting_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int VENContract_NewRouting_Save(DTOCATRouting item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_Save(item, contractID);
                }
            }
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
        public DTOResult VENContract_NewRouting_LocationList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_LocationList(request);
                }
            }
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
        public DTOResult VENContract_NewRouting_AreaList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_AreaList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATRoutingArea VENContract_NewRouting_AreaGet(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_AreaGet(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int VENContract_NewRouting_AreaSave(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_AreaSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_NewRouting_AreaDelete(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_NewRouting_AreaDelete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_NewRouting_AreaRefresh(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_NewRouting_AreaRefresh(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_NewRouting_AreaDetailList(string request, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_AreaDetailList(request, areaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATRoutingAreaDetail VENContract_NewRouting_AreaDetailGet(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_AreaDetailGet(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int VENContract_NewRouting_AreaDetailSave(DTOCATRoutingAreaDetail item, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_AreaDetailSave(item, areaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_NewRouting_AreaDetailDelete(DTOCATRoutingAreaDetail item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_NewRouting_AreaDetailDelete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_NewRouting_AreaLocation_List(string request, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_AreaLocation_List(request, areaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENContract_NewRouting_AreaLocationNotIn_List(string request, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_NewRouting_AreaLocationNotIn_List(request, areaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_NewRouting_AreaLocationNotIn_Save(int areaID, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_NewRouting_AreaLocationNotIn_Save(areaID, lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_NewRouting_AreaLocation_Delete(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_NewRouting_AreaLocation_Delete(lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_NewRouting_AreaLocation_Copy(int areaID, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_NewRouting_AreaLocation_Copy(areaID, lstID);
                }
            }
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

        public SYSExcel VENContract_Routing_ExcelOnline_Init(int contractID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Routing_ExcelOnline_Init(contractID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row VENContract_Routing_ExcelOnline_Change(int contractID, int customerID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Routing_ExcelOnline_Change(contractID, customerID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENContract_Routing_ExcelOnline_Import(int contractID, int customerID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Routing_ExcelOnline_Import(contractID, customerID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VENContract_Routing_ExcelOnline_Approve(long id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Routing_ExcelOnline_Approve(id, contractID);
                }
            }
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

        #region Price

        #region Common

        public DTOResult VENContract_Price_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Price_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPrice VENContract_Price_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Price_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOVENPrice_Data VENContract_Price_Data(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Price_Data(contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int VENContract_Price_Save(DTOPrice item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Price_Save(item, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_Price_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Price_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Price_Copy(List<DTOVENPrice_ItemCopy> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Price_Copy(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Price_DeletePriceNormal(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Price_DeletePriceNormal(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Price_DeletePriceLevel(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Price_DeletePriceLevel(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOVENPrice_ExcelData VENContract_Price_ExcelData(int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Price_ExcelData(contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceCOContainerExcelData VENPrice_CO_COContainer_ExcelData(int priceid, int contractid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_COContainer_ExcelData(priceid, contractid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_COContainer_ExcelImport(List<DTOPriceCOContainerImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_COContainer_ExcelImport(lst, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENPrice_CO_GroupContainer_ExcelInit(bool isFrame, int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_GroupContainer_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row VENPrice_CO_GroupContainer_ExcelChange(bool isFrame, int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_GroupContainer_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENPrice_CO_GroupContainer_ExcelOnImport(bool isFrame, int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_GroupContainer_ExcelOnImport(isFrame, priceID, contractTermID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VENPrice_CO_GroupContainer_ExcelApprove(bool isFrame, int priceID, int contractTermID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_GroupContainer_ExcelApprove(isFrame, priceID, contractTermID, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region price co EX
        #region info
        public DTOResult VENPrice_CO_Ex_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int VENPrice_CO_Ex_Save(DTOPriceCOEx item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_CO_Ex_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Ex_Delete(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceCOEx VENPrice_CO_Ex_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_Get(priceID);
                }
            }
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
        public DTOResult VENPrice_CO_Ex_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_Location_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_Ex_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Ex_Location_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceCOExLocation VENPrice_CO_Ex_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_Location_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_Ex_Location_Save(DTOPriceCOExLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Ex_Location_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_Ex_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Ex_Location_LocationNotInSaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_CO_Ex_Location_LocationNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_Location_LocationNotInList(request, priceExID, contractID);
                }
            }
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
        public DTOResult VENPrice_CO_Ex_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_Route_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_Ex_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Ex_Route_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_Ex_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Ex_Route_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_CO_Ex_Route_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_Route_RouteNotInList(request, priceExID, contractID);
                }
            }
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
        public DTOResult VENPrice_CO_Ex_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_ParentRoute_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_Ex_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Ex_ParentRoute_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_Ex_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Ex_ParentRoute_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_CO_Ex_ParentRoute_RouteNotInList(string request, int priceExID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_ParentRoute_RouteNotInList(request, priceExID, contractID);
                }
            }
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
        public DTOResult VENPrice_CO_Ex_Partner_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_Partner_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_Ex_Partner_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Ex_Partner_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_Ex_Partner_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Ex_Partner_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_CO_Ex_Partner_PartnerNotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Ex_Partner_PartnerNotInList(request, priceExID, contractID, CustomerID);
                }
            }
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

        #region DI_GroupVehicle

        #region old
        public List<DTOPriceGroupVehicle> VENPrice_DI_GroupVehicle_GetData(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupVehicle_GetData(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_GroupVehicle_SaveList(List<DTOPriceGroupVehicle> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_GroupVehicle_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceGroupVehicleData VENPrice_DI_GroupVehicle_ExcelData(int priceID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupVehicle_ExcelData(priceID, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_GroupVehicle_ExcelImport(List<DTOPriceGroupVehicleImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_GroupVehicle_ExcelImport(lst, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENPrice_DI_GroupVehicle_ExcelInit(bool isFrame, int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupVehicle_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row VENPrice_DI_GroupVehicle_ExcelChange(bool isFrame, int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupVehicle_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENPrice_DI_GroupVehicle_ExcelOnImport(bool isFrame, int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupVehicle_ExcelOnImport(isFrame, priceID, contractTermID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VENPrice_DI_GroupVehicle_ExcelApprove(bool isFrame, int priceID, int contractTermID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupVehicle_ExcelApprove(isFrame, priceID, contractTermID, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_GroupVehicle_GOVNotInList(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupVehicle_GOVNotInList(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_GroupVehicle_GOVNotInSave(List<int> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_GroupVehicle_GOVNotInSave(lst, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_GroupVehicle_GOVList(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupVehicle_GOVList(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_GroupVehicle_GOVDelete(List<int> lstGov, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_GroupVehicle_GOVDelete(lstGov, priceID);
                }
            }
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

        public List<DTOPriceGVLevelGroupVehicle> VENPrice_DI_PriceGVLevel_DetailData(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceGVLevel_DetailData(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_PriceGVLevel_Save(List<DTOPriceGVLevelGroupVehicle> lst, int priceid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceGVLevel_Save(lst, priceid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceGVLevelData VENPrice_DI_PriceGVLevel_ExcelData(int priceid, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceGVLevel_ExcelData(priceid, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceGVLevel_ExcelImport(List<DTOPriceGVLevelImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceGVLevel_ExcelImport(lst, priceID);
                }
            }
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
        public List<DTOPriceDIGroupOfProduct> VENPrice_DI_GroupProduct_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupProduct_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_GroupProduct_SaveList(List<DTOPriceDIGroupOfProduct> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_GroupProduct_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceDIGroupOfProductData VENPrice_DI_GroupProduct_Export(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupProduct_Export(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_GroupProduct_Import(List<DTOPriceDIGroupOfProductImport> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_GroupProduct_Import(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENPrice_DI_GroupProduct_ExcelInit(bool isFrame, int priceID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupProduct_ExcelInit(isFrame, priceID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row VENPrice_DI_GroupProduct_ExcelChange(bool isFrame, int priceID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupProduct_ExcelChange(isFrame, priceID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENPrice_DI_GroupProduct_ExcelOnImport(bool isFrame, int priceID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupProduct_ExcelOnImport(isFrame, priceID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VENPrice_DI_GroupProduct_ExcelApprove(bool isFrame, int priceID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_GroupProduct_ExcelApprove(isFrame, priceID, id);
                }
            }
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

        #region Loading

        public List<DTOPriceTruckDILoad> VENPrice_DI_Loading_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Loading_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_Loading_Location_NotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Loading_Location_NotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_Loading_Location_NotIn_SaveList(List<DTOPriceLocation> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Loading_Location_NotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_Loading_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Loading_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_Loading_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Loading_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_Loading_DeleteList(List<int> data)
        {

            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Loading_DeleteList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceTruckDILoad_Export VENPrice_DI_Loading_Export(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Loading_Export(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_Loading_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Loading_Import(data, priceID);
                }
            }
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

        #region DI_PriceEx
        public List<DTOCATRouting> VENPrice_DI_PriceEx_RoutingParentList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceEx_RoutingParentList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATRouting> VENPrice_DI_PriceEx_RoutingList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceEx_RoutingList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_PriceEx_List(int priceID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceEx_List(priceID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_PriceEx_Detail(int typeid, int priceID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceEx_Detail(typeid, priceID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_PriceEx_SaveList(List<DTOCATPriceDIEx> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceEx_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATPriceDIEx VENPrice_DI_PriceEx_Get(int id, int typeid, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceEx_Get(id, typeid, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int VENPrice_DI_PriceEx_Save(DTOCATPriceDIEx item, int typeid, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceEx_Save(item, typeid, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_PriceEx_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceEx_Delete(id);
                }
            }
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

        #region CO_PackingPrice

        public List<DTOPriceRouting> VENPrice_CO_COPackingPrice_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_COPackingPrice_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_CO_COPackingPrice_SaveList(List<DTOPriceRouting> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_COPackingPrice_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceContainer_Export VENPrice_CO_COPackingPrice_Export(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_COPackingPrice_Export(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_CO_COPackingPrice_Import(List<DTOPrice_COPackingPrice_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_COPackingPrice_Import(data, priceID);
                }
            }
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

        #region CO_Service old

        //public DTOResult VENPrice_CO_Service_List(string request, int priceID)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLVendor>())
        //        {
        //            return bl.VENPrice_CO_Service_List(request, priceID);
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

        //public int VENPrice_CO_Service_Save(DTOCATPriceCOService item, int priceID)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLVendor>())
        //        {
        //            return bl.VENPrice_CO_Service_Save(item, priceID);
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

        //public void VENPrice_CO_Service_Delete(int id)
        //{
        //    try
        //    {
        //        using (var bl = CreateBusiness<BLVendor>())
        //        {
        //            bl.VENPrice_CO_Service_Delete(id);
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

        #endregion

        #region CO_Service

        public DTOResult VENPrice_CO_Service_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Service_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_CO_ServicePacking_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_ServicePacking_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATPriceCOService VENPrice_CO_Service_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Service_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATPriceCOService VENPrice_CO_ServicePacking_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_ServicePacking_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int VENPrice_CO_Service_Save(DTOCATPriceCOService item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_Service_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_CO_Service_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_Service_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_CO_CATService_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_CATService_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_CO_CATServicePacking_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_CATServicePacking_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_CO_CATCODefault_List(int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_CATCODefault_List(contractTermID);
                }
            }
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
        public List<DTOPriceDILevelGroupProduct> VENPrice_DI_PriceLevel_DetailData(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceLevel_DetailData(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceLevel_Save(List<DTOPriceDILevelGroupProduct> lst, int priceid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceLevel_Save(lst, priceid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDILevelData VENPrice_DI_PriceLevel_ExcelData(int priceid, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceLevel_ExcelData(priceid, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceLevel_ExcelImport(List<DTOPriceDILevelImport> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceLevel_ExcelImport(lst, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENPrice_DI_PriceGVLevel_ExcelInit(bool isFrame, int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceGVLevel_ExcelInit(isFrame, priceID, contractTermID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row VENPrice_DI_PriceGVLevel_ExcelChange(bool isFrame, int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceGVLevel_ExcelChange(isFrame, priceID, contractTermID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENPrice_DI_PriceGVLevel_ExcelOnImport(bool isFrame, int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceGVLevel_ExcelOnImport(isFrame, priceID, contractTermID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VENPrice_DI_PriceGVLevel_ExcelApprove(bool isFrame, int priceID, int contractTermID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceGVLevel_ExcelApprove(isFrame, priceID, contractTermID, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENPrice_DI_PriceLevel_ExcelInit(int priceID, int contractTermID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceLevel_ExcelInit(priceID, contractTermID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row VENPrice_DI_PriceLevel_ExcelChange(int priceID, int contractTermID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceLevel_ExcelChange(priceID, contractTermID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENPrice_DI_PriceLevel_OnExcelImport(int priceID, int contractTermID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceLevel_OnExcelImport(priceID, contractTermID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VENPrice_DI_PriceLevel_ExcelApprove(int priceID, int contractTermID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceLevel_ExcelApprove(priceID, contractTermID, id);
                }
            }
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
        public void VENPrice_DI_Load_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Load_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Load_DeleteList(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Load_DeleteList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Load_DeleteAllList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Load_DeleteAllList(priceID);
                }
            }
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
        public List<DTOPriceTruckDILoad> VENPrice_DI_LoadLocation_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadLocation_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_LoadLocation_LocationNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadLocation_LocationNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadLocation_LocationNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadLocation_LocationNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadLocation_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadLocation_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_LoadLocation_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadLocation_DeleteList(priceID);
                }
            }
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
        public DTOPriceTruckDILoad_Export VENPrice_DI_LoadLocation_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadLocation_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_LoadLocation_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadLocation_Import(data, priceID);
                }
            }
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
        public List<DTOPriceTruckDILoad> VENPrice_DI_LoadRoute_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadRoute_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_LoadRoute_RouteNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadRoute_RouteNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadRoute_RouteNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadRoute_RouteNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadRoute_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadRoute_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_LoadRoute_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadRoute_DeleteList(priceID);
                }
            }
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
        public DTOPriceTruckDILoad_Export VENPrice_DI_LoadRoute_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadRoute_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadRoute_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadRoute_Import(data, priceID);
                }
            }
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
        public List<DTOPriceTruckDILoad> VENPrice_DI_LoadPartner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadPartner_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_LoadPartner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadPartner_PartnerNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadPartner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadPartner_PartnerNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadPartner_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadPartner_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadPartner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadPartner_DeleteList(priceID);
                }
            }
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
        public DTOPriceTruckDILoad_Export VENPrice_DI_LoadPartner_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadPartner_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadPartner_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadPartner_Import(data, priceID);
                }
            }
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
        public List<DTOPriceDILoadPartner> VENPrice_DI_LoadPartner_Partner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadPartner_Partner_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_LoadPartner_Partner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_LoadPartner_Partner_PartnerNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadPartner_Partner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadPartner_Partner_PartnerNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadPartner_Partner_SaveList(List<DTOPriceDILoadPartner> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadPartner_Partner_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_LoadPartner_Partner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_LoadPartner_Partner_DeleteList(priceID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQLoad_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int VENPrice_DI_PriceMOQLoad_Save(DTOPriceDIMOQLoad item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_Delete(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIMOQLoad VENPrice_DI_PriceMOQLoad_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_Get(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_DeleteList(priceID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQLoad_GroupLocation_List(string request, int priceMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_GroupLocation_List(request, priceMOQLoadID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_GroupLocation_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_GroupLocation_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_GroupLocation_SaveList(List<int> lst, int priceMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_GroupLocation_SaveList(lst, priceMOQLoadID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(string request, int priceMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_GroupLocation_GroupNotInList(request, priceMOQLoadID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQLoad_GroupProduct_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_GroupProduct_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_GroupProduct_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_GroupProduct_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIMOQLoadGroupProduct VENPrice_DI_PriceMOQLoad_GroupProduct_Get(int id, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_GroupProduct_Get(id, cusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_GroupProduct_Save(DTOPriceDIMOQLoadGroupProduct item, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_GroupProduct_Save(item, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQLoad_GroupProduct_GOPList(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_GroupProduct_GOPList(cusID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQLoad_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_Location_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_Location_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_Location_LocationNotInSaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQLoad_Location_LocationNotInList(string request, int PriceMOQLoadID, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_Location_LocationNotInList(request, PriceMOQLoadID, customerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceDIMOQLoadLocation VENPrice_DI_PriceMOQLoad_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_Location_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_PriceMOQLoad_Location_Save(DTOPriceDIMOQLoadLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_Location_Save(item);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQLoad_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_Route_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_Route_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_Route_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQLoad_Route_RouteNotInList(string request, int PriceMOQLoadID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_Route_RouteNotInList(request, PriceMOQLoadID, contractTermID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQLoad_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_ParentRoute_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_ParentRoute_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_ParentRoute_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(string request, int PriceMOQLoadID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_ParentRoute_RouteNotInList(request, PriceMOQLoadID, contractTermID);
                }
            }
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

        public DTOResult VENPrice_DI_PriceMOQLoad_Province_List(string request, int PriceDIMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_Province_List(request, PriceDIMOQLoadID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_Province_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_Province_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQLoad_Province_SaveList(List<int> lst, int PriceDIMOQLoadID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQLoad_Province_SaveList(lst, PriceDIMOQLoadID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQLoad_Province_NotInList(string request, int PriceDIMOQLoadID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQLoad_Province_NotInList(request, PriceDIMOQLoadID, contractID, CustomerID);
                }
            }
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

        #region price di Ex

        #region info
        public DTOResult VENPrice_DI_Ex_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int VENPrice_DI_Ex_Save(DTOPriceDIEx item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_Ex_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_Delete(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceDIEx VENPrice_DI_Ex_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Get(priceID);
                }
            }
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
        public DTOResult VENPrice_DI_Ex_GroupLocation_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_GroupLocation_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_GroupLocation_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_GroupLocation_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_GroupLocation_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_GroupLocation_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_Ex_GroupLocation_GroupNotInList(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_GroupLocation_GroupNotInList(request, priceExID);
                }
            }
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
        public DTOResult VENPrice_DI_Ex_GroupProduct_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_GroupProduct_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_GroupProduct_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_GroupProduct_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIExGroupProduct VENPrice_DI_Ex_GroupProduct_Get(int id, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_GroupProduct_Get(id, cusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_GroupProduct_Save(DTOPriceDIExGroupProduct item, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_GroupProduct_Save(item, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_Ex_GroupProduct_GOPList(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_GroupProduct_GOPList(cusID);
                }
            }
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
        public DTOResult VENPrice_DI_Ex_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Location_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_Location_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIExLocation VENPrice_DI_Ex_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Location_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_Location_Save(DTOPriceDIExLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_Location_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_Location_LocationNotInSaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_Ex_Location_LocationNotInList(string request, int priceExID, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Location_LocationNotInList(request, priceExID, customerID);
                }
            }
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
        public DTOResult VENPrice_DI_Ex_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Route_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_Route_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_Route_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_Ex_Route_RouteNotInList(string request, int priceExID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Route_RouteNotInList(request, priceExID, contractTermID);
                }
            }
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
        public DTOResult VENPrice_DI_Ex_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_ParentRoute_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_ParentRoute_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_ParentRoute_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_Ex_ParentRoute_RouteNotInList(string request, int priceExID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_ParentRoute_RouteNotInList(request, priceExID, contractTermID);
                }
            }
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
        public DTOResult VENPrice_DI_Ex_Partner_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Partner_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_Partner_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_Partner_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_Partner_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_Partner_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_Ex_Partner_PartnerNotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Partner_PartnerNotInList(request, priceExID, contractID, CustomerID);
                }
            }
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

        public DTOResult VENPrice_DI_Ex_Province_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Province_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_Province_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_Province_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_Ex_Province_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_Ex_Province_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_Ex_Province_NotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_Ex_Province_NotInList(request, priceExID, contractID, CustomerID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQ_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int VENPrice_DI_PriceMOQ_Save(DTOCATPriceDIMOQ item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_PriceMOQ_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_Delete(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATPriceDIMOQ VENPrice_DI_PriceMOQ_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Get(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_DeleteList(priceID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQ_GroupLocation_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_GroupLocation_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_GroupLocation_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_GroupLocation_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_GroupLocation_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_GroupLocation_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_GroupLocation_GroupNotInList(request, priceExID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQ_GroupProduct_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_GroupProduct_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_GroupProduct_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_GroupProduct_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIMOQGroupProduct VENPrice_DI_PriceMOQ_GroupProduct_Get(int id, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_GroupProduct_Get(id, cusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_GroupProduct_Save(DTOPriceDIMOQGroupProduct item, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_GroupProduct_Save(item, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQ_GroupProduct_GOPList(int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_GroupProduct_GOPList(cusID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQ_Location_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Location_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_Location_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_Location_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOPriceDIMOQLocation VENPrice_DI_PriceMOQ_Location_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Location_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_Location_Save(DTOPriceDIMOQLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_Location_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_Location_LocationNotInSaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_Location_LocationNotInSaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQ_Location_LocationNotInList(string request, int priceMOQID, int custormerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Location_LocationNotInList(request, priceMOQID, custormerID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQ_Route_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Route_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_Route_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_Route_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_Route_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_Route_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQ_Route_RouteNotInList(string request, int priceMOQID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Route_RouteNotInList(request, priceMOQID, contractTermID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQ_ParentRoute_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_ParentRoute_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_ParentRoute_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_ParentRoute_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_ParentRoute_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_ParentRoute_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(string request, int priceMOQID, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_ParentRoute_RouteNotInList(request, priceMOQID, contractTermID);
                }
            }
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

        #region partner
        public DTOResult VENPrice_DI_PriceMOQ_Partner_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Partner_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_Partner_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_Partner_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_Partner_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_Partner_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQ_Partner_PartnerNotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Partner_PartnerNotInList(request, priceExID, contractID, CustomerID);
                }
            }
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

        public DTOResult VENPrice_DI_PriceMOQ_Province_List(string request, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Province_List(request, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_Province_DeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_Province_DeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQ_Province_SaveList(List<int> lst, int priceExID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQ_Province_SaveList(lst, priceExID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_DI_PriceMOQ_Province_NotInList(string request, int priceExID, int contractID, int CustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQ_Province_NotInList(request, priceExID, contractID, CustomerID);
                }
            }
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
        public void VENPrice_DI_UnLoad_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoad_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_UnLoad_DeleteList(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoad_DeleteList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_UnLoad_DeleteAllList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoad_DeleteAllList(priceID);
                }
            }
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
        public List<DTOPriceTruckDILoad> VENPrice_DI_UnLoadLocation_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadLocation_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_UnLoadLocation_LocationNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadLocation_LocationNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadLocation_LocationNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadLocation_LocationNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadLocation_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadLocation_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_UnLoadLocation_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadLocation_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceTruckDILoad_Export VENPrice_DI_UnLoadLocation_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadLocation_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadLocation_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadLocation_Import(data, priceID);
                }
            }
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
        public List<DTOPriceTruckDILoad> VENPrice_DI_UnLoadRoute_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadRoute_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_UnLoadRoute_RouteNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadRoute_RouteNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadRoute_RouteNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadRoute_RouteNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadRoute_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadRoute_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_UnLoadRoute_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadRoute_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceTruckDILoad_Export VENPrice_DI_UnLoadRoute_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadRoute_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadRoute_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadRoute_Import(data, priceID);
                }
            }
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
        public List<DTOPriceTruckDILoad> VENPrice_DI_UnLoadPartner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadPartner_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_UnLoadPartner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadPartner_PartnerNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadPartner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadPartner_PartnerNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadPartner_SaveList(List<DTOPriceTruckDILoad> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadPartner_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_UnLoadPartner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadPartner_DeleteList(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceTruckDILoad_Export VENPrice_DI_UnLoadPartner_Export(int contractTermID, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadPartner_Export(contractTermID, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadPartner_Import(List<DTOPriceTruckDILoad_Import> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadPartner_Import(data, priceID);
                }
            }
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
        public List<DTOPriceDILoadPartner> VENPrice_DI_UnLoadPartner_Partner_List(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadPartner_Partner_List(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_DI_UnLoadPartner_Partner_PartnerNotIn_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_UnLoadPartner_Partner_PartnerNotIn_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(List<int> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadPartner_Partner_PartnerNotIn_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_UnLoadPartner_Partner_SaveList(List<DTOPriceDILoadPartner> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadPartner_Partner_SaveList(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_UnLoadPartner_Partner_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_UnLoadPartner_Partner_DeleteList(priceID);
                }
            }
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
        public DTOResult VENPrice_DI_PriceMOQUnLoad_List(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQUnLoad_List(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int VENPrice_DI_PriceMOQUnLoad_Save(DTOPriceDIMOQLoad item, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQUnLoad_Save(item, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_DI_PriceMOQUnLoad_Delete(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQUnLoad_Delete(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPriceDIMOQLoad VENPrice_DI_PriceMOQUnLoad_Get(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_DI_PriceMOQUnLoad_Get(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_DI_PriceMOQUnLoad_DeleteList(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_DI_PriceMOQUnLoad_DeleteList(priceID);
                }
            }
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

        #region price CO container
        public DTOPriceCOContainerData VENPrice_CO_COContainer_Data(int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_COContainer_Data(priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENPrice_CO_COContainer_SaveList(List<DTOPriceCOContainer> data, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_COContainer_SaveList(data, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENPrice_CO_COContainer_ContainerList(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_COContainer_ContainerList(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_COContainer_ContainerNotInSave(List<int> lst, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_COContainer_ContainerNotInSave(lst, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENPrice_CO_COContainer_ContainerNotInList(string request, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENPrice_CO_COContainer_ContainerNotInList(request, priceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENPrice_CO_COContainer_ContainerDelete(List<int> lstPacking, int priceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENPrice_CO_COContainer_ContainerDelete(lstPacking, priceID);
                }
            }
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

        #region GroupOfProduct

        public DTOResult VENContract_GroupOfProduct_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_GroupOfProduct_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATContractGroupOfProduct VENContract_GroupOfProduct_Get(int id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_GroupOfProduct_Get(id, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_GroupOfProduct_Save(DTOCATContractGroupOfProduct item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_GroupOfProduct_Save(item, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_GroupOfProduct_Delete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_GroupOfProduct_Delete(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public double? VENContract_GroupOfProduct_Check(DTOCATContractGroupOfProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_GroupOfProduct_Check(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENContract_GroupOfProduct_ExcelInit(int contractID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_GroupOfProduct_ExcelInit(contractID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row VENContract_GroupOfProduct_ExcelChange(int contractID, long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_GroupOfProduct_ExcelChange(contractID, id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENContract_GroupOfProduct_ExcelImport(int contractID, long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_GroupOfProduct_ExcelImport(contractID, id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VENContract_GroupOfProduct_ExcelApprove(int contractID, long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_GroupOfProduct_ExcelApprove(contractID, id);
                }
            }
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
        public DTOCUSPrice_MaterialData VENContract_MaterialChange_Data(int contractMaterialID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_MaterialChange_Data(contractMaterialID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_MaterialChange_Save(DTOCUSPrice_MaterialData item, int contractMaterialID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_MaterialChange_Save(item, contractMaterialID);
                }
            }
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
        public DTOResult VENContract_ContractTerm_List(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_ContractTerm_List(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOContractTerm VENContract_ContractTerm_Get(int id, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_ContractTerm_Get(id, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int VENContract_ContractTerm_Save(DTOContractTerm item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_ContractTerm_Save(item, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_ContractTerm_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_ContractTerm_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_ContractTerm_Price_List(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_ContractTerm_Price_List(request, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_ContractTerm_Open(int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_ContractTerm_Open(termID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_ContractTerm_Close(int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_ContractTerm_Close(termID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENTerm_Change_RemoveWarning(int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENTerm_Change_RemoveWarning(termID);
                }
            }
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
        public DTOResult VENContract_ContractTerm_KPITime_List(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_ContractTerm_KPITime_List(request, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_ContractTerm_KPITime_SaveExpr(DTOContractTerm_KPITime item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_ContractTerm_KPITime_SaveExpr(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_ContractTerm_KPITime_NotInList(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_ContractTerm_KPITime_NotInList(request, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DateTime? VENContract_KPITime_Check_Expression(KPITimeDate item, List<KPITimeDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_KPITime_Check_Expression(item, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool? VENContract_KPITime_Check_Hit(KPITimeDate item, List<KPITimeDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_KPITime_Check_Hit(item, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_ContractTerm_KPITime_SaveNotInList(List<DTOContractTerm_TypeOfKPI> lst, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_ContractTerm_KPITime_SaveNotInList(lst, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_ContractTerm_KPIQuantity_List(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_ContractTerm_KPIQuantity_List(request, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_ContractTerm_KPIQuantity_SaveExpr(DTOContractTerm_KPIQuantity item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_ContractTerm_KPIQuantity_SaveExpr(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_ContractTerm_KPIQuantity_NotInList(string request, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_ContractTerm_KPIQuantity_NotInList(request, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_ContractTerm_KPIQuantity_SaveNotInList(List<DTOContractTerm_TypeOfKPI> lst, int contractTermID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_ContractTerm_KPIQuantity_SaveNotInList(lst, contractTermID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIQuantityDate> VENContract_KPIQuantity_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_KPIQuantity_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public KPIQuantityDate VENContract_KPIQuantity_Check_Expression(KPIQuantityDate item, List<KPIQuantityDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_KPIQuantity_Check_Expression(item, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool? VENContract_KPIQuantity_Check_Hit(KPIQuantityDate item, List<KPIQuantityDate> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_KPIQuantity_Check_Hit(item, lst);
                }
            }
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
        public void VENContract_Setting_TypeOfRunLevelSave(int typeID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Setting_TypeOfRunLevelSave(typeID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VENContract_Setting_TypeOfSGroupProductChangeSave(int typeID, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Setting_TypeOfSGroupProductChangeSave(typeID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void VenContractSetting_Save(string setting, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VenContractSetting_Save(setting, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_Setting_GOVList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Setting_GOVList(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATContractGroupVehicle VENContract_Setting_GOVGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Setting_GOVGet(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Setting_GOVSave(DTOCATContractGroupVehicle item)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Setting_GOVSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Setting_GOVDeleteList(List<int> lst, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Setting_GOVDeleteList(lst, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENContract_Setting_GOVNotInList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Setting_GOVNotInList(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Setting_GOVNotInSave(List<int> lst, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Setting_GOVNotInSave(lst, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENContract_Setting_LevelList(string request, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Setting_LevelList(request, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATContractLevel VENContract_Setting_LevelGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Setting_LevelGet(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Setting_LevelSave(DTOCATContractLevel item, int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Setting_LevelSave(item, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENContract_Setting_LevelDeleteList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENContract_Setting_LevelDeleteList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATGroupOfVehicle> VENContract_Setting_Level_GOVList(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENContract_Setting_Level_GOVList(contractID);
                }
            }
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

        #region Price History
        public DTOCUSPrice_HistoryData PriceHistory_CheckPrice(List<int> lstVenId, int transportModeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.PriceHistory_CheckPrice(lstVenId, transportModeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCUSPrice_HistoryData PriceHistory_GetDataOneUser(int cusId, int venId, int transportModeID, int typePrice)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.PriceHistory_GetDataOneUser(cusId, venId, transportModeID, typePrice);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSPrice_HistoryData PriceHistory_GetDataMulUser(int cusId, List<int> lstVenId, int transportModeID, int typePrice)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.PriceHistory_GetDataMulUser(cusId, lstVenId, transportModeID, typePrice);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<int> PriceHistory_GetListVendor(int cusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.PriceHistory_GetListVendor(cusId);
                }
            }
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

        #region danh sách địa chỉ
        public DTOResult VENLocation_List(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_List(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENLocationSaveLoad_List(List<DTOCUSLocationInVEN> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENLocationSaveLoad_List(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENLocation_SaveList(int vendorid, List<CATLocation> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENLocation_SaveList(vendorid, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENLocation_Delete(int cuslocationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENLocation_Delete(cuslocationid);
                }
            }
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
                using (var bl = CreateBusiness<BLVendor>())
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

        public List<AddressSearchItem> AddressSearch_ByCustomerList(int Vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.AddressSearch_ByCustomerList(Vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult VENLocation_NotInList(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_NotInList(request, vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENLocation_HasRun(string request, int vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_HasRun(request,vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel VENLocation_ExcelInit(int functionid, string functionkey, bool isreload, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_ExcelInit(functionid, functionkey, isreload, customerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row VENLocation_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_ExcelChange(id, row, cells, lstMessageError, customerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel VENLocation_ExcelImport(long id, List<Row> lst, List<string> lstMessageError, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_ExcelImport(id, lst, lstMessageError, customerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool VENLocation_ExcelApprove(long id, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_ExcelApprove(id, customerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region routing contract
        public DTOResult VENLocation_RoutingContract_List(string request, int customerid, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_RoutingContract_List(request, customerid, locationid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENLocation_RoutingContract_SaveList(List<int> lstAreaClear, List<int> lstAreaAdd, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENLocation_RoutingContract_SaveList(lstAreaClear, lstAreaAdd, locationid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENLocation_RoutingContract_NewRoutingSave(DTOCUSPartnerNewRouting item, int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENLocation_RoutingContract_NewRoutingSave(item, customerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCUSPartnerNewRouting VENLocation_RoutingContract_NewRoutingGet(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_RoutingContract_NewRoutingGet(customerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOCATContract> VENLocation_RoutingContract_ContractData(int customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_RoutingContract_ContractData(customerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void VENLocation_RoutingContract_NewAreaSave(CATRoutingArea item, int locationid)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    bl.VENLocation_RoutingContract_NewAreaSave(item, locationid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult VENLocation_RoutingContract_AreaList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLVendor>())
                {
                    return bl.VENLocation_RoutingContract_AreaList(request);
                }
            }
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
    }
}

