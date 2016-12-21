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
    public partial class SVCategory : SVBase, ISVCategory
    {
        #region ALL Data
        public DTOResult ALL_Country()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_Country();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_District()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_District();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_Province()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_Province();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_Ward()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_Ward();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_SysVar(SYSVarType typeOfVar)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_SysVar(typeOfVar);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_Customer()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_Customer();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CustomerInUser()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CustomerInUser();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_Vendor()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_Vendor();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_VendorInUser()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_VendorInUser();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATTypeOfPriceDIEx()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATTypeOfPriceDIEx();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATTypeOfPriceCOEx()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATTypeOfPriceCOEx();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_GroupOfVehicle()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_GroupOfVehicle();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_Service()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_Service();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATPacking(SYSVarType TypeOfVar)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATPacking(TypeOfVar);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATPackingCO()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATPackingCO();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATPackingGOP()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATPackingGOP();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_TroubleCostStatus()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_TroubleCostStatus();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATGroupOfRomooc()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATGroupOfRomooc();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATGroupOfEquipment()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATGroupOfEquipment();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult All_CATGroupOfPartner()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.All_CATGroupOfPartner();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATGroupOfMaterial()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATGroupOfMaterial();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATGroupOfCost()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATGroupOfCost();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_Material()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_Material();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATGroupOfLocation()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATGroupOfLocation();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult ALL_OPSTypeOfDITOGroupProductReturn()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_OPSTypeOfDITOGroupProductReturn();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult ALL_CATTypeOfDriverFee()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATTypeOfDriverFee();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult ALL_CATDrivingLicence()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATDrivingLicence();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult ALL_CATShift()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATShift();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult ALL_FLMTypeOfScheduleFee()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_FLMTypeOfScheduleFee();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult ALL_SYSVarFLMTypeWarning()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_SYSVarFLMTypeWarning();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult ALL_SYSVarREPOwnerAsset()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_SYSVarREPOwnerAsset();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult ALL_CATTransportMode()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATTransportMode();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ALL_CATServiceOfOrder()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ALL_CATServiceOfOrder();
                }
            }
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

        #region CATCountry
        public DTOResult CATCountry_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCountry_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATCountry_Save(CATCountry item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCountry_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATCountry CATCountry_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCountry_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATCountry_Delete(CATCountry item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATCountry_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATCountry_ExcelInit(int functionid, string functionkey)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCountry_ExcelInit(functionid, functionkey);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<Row> CATCountry_ExcelChange(long id, int row, int col, string val)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCountry_ExcelChange(id, row, col, val);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATCountry_ExcelImport(long id, List<Row> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCountry_ExcelImport(id, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATCountry_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCountry_ExcelApprove(id);
                }
            }
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

        #region CATDistrict
        public DTOResult CATDistrict_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATDistrict_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATDistrict_Save(CATDistrict item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATDistrict_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATDistrict_Delete(CATDistrict item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATDistrict_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATDistrict CATDistrict_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATDistrict_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATDistrict_ExcelInit(int functionid, string functionkey)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATDistrict_ExcelInit(functionid, functionkey);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<Row> CATDistrict_ExcelChange(long id, int row, int col, string val)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATDistrict_ExcelChange(id, row, col, val);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel CATDistrict_ExcelImport(long id, List<Row> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATDistrict_ExcelImport(id, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel CATDistrict_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATDistrict_ExcelApprove(id);
                }
            }
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

        #region CATProvince
        public DTOResult CATProvince_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATProvince_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATProvince_Save(CATProvince item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATProvince_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATProvince_Delete(CATProvince item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATProvince_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATProvince CATProvince_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATProvince_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATProvince_ExcelInit(int functionid, string functionkey)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATProvince_ExcelInit(functionid, functionkey);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<Row> CATProvince_ExcelChange(long id, int row, int col, string val)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATProvince_ExcelChange(id, row, col, val);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel CATProvince_ExcelImport(long id, List<Row> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATProvince_ExcelImport(id, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel CATProvince_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATProvince_ExcelApprove(id);
                }
            }
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

        #region SeaPort
        public DTOResult SeaPortCustom_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SeaPortCustom_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPartnerLocationResult SeaPortCustomer_List(List<int> lstCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SeaPortCustomer_List(lstCustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATPartner SeaPort_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SeaPort_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int SeaPort_Save(CATPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SeaPort_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SeaPortCustom_Save(DTOCATSeaPortCustom item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.SeaPortCustom_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SeaPort_Delete(CATPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.SeaPort_Delete(item);
                }
            }
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

        #region Distributor
        public DTOResult Customer_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
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

        public DTOResult DistributorCustom_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.DistributorCustom_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPartnerLocationResult DistributorCustomer_List(List<int> lstCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.DistributorCustomer_List(lstCustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATDistributor Distributor_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Distributor_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Distributor_Save(DTOCATDistributor item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Distributor_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DistributorCustom_Save(DTOCATDistributorCustom item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.DistributorCustom_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Distributor_Delete(DTOCATDistributor item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.Distributor_Delete(item);
                }
            }
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

        #region Carrier
        public DTOResult CarrierCustom_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CarrierCustom_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOPartnerLocationResult CarrierCustomer_List(List<int> lstCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CarrierCustomer_List(lstCustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATCarrier Carrier_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Carrier_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Carrier_Save(CATPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Carrier_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CarrierCustom_Save( DTOCATCarrierCustom  lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CarrierCustom_Save(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Carrier_Delete(DTOCATCarrier item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.Carrier_Delete(item);
                }
            }
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

        #region LocationInDistributor
        public DTOResult LocationInDistributor_List(string request, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInDistributor_List(request, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATLocationInPartner LocationInDistributor_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInDistributor_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATLocationInPartner LocationInDistributor_Save(DTOCATLocationInPartner item, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInDistributor_Save(item, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void LocationInDistributor_Delete(DTOCATLocationInPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.LocationInDistributor_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult LocationNotInDistributor_List(string request, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationNotInDistributor_List(request, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void LocationNotInDistributor_SaveList(List<DTOCATLocationInPartner> lst, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.LocationNotInDistributor_SaveList(lst, partnerid);
                }
            }
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

        #region LocationInSeaport
        public DTOResult LocationInSeaport_List(string request, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInSeaport_List(request, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATLocationInPartner LocationInSeaport_Save(DTOCATLocationInPartner item, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInSeaport_Save(item, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATLocationInPartner LocationInSeaport_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInSeaport_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public void LocationInSeaport_Delete(DTOCATLocationInPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.LocationInSeaport_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult LocationNotInSeaport_List(string request, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationNotInSeaport_List(request, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void LocationNotInSeaport_SaveList(List<DTOCATLocationInPartner> lst, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.LocationNotInSeaport_SaveList(lst, partnerid);
                }
            }
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

        #region LocationInCarrier
        public DTOResult LocationInCarrier_List(string request, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInCarrier_List(request, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATLocationInPartner LocationInCarrier_Save(DTOCATLocationInPartner item, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInCarrier_Save(item, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void LocationInCarrier_Delete(DTOCATLocationInPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.LocationInCarrier_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult LocationNotInCarrier_List(string request, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationNotInCarrier_List(request, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void LocationNotInCarrier_SaveList(List<DTOCATLocationInPartner> lst, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.LocationNotInCarrier_SaveList(lst, partnerid);
                }
            }
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

        #region CATStock
        public DTOResult FLMStock_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMStock_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMStock_Save(FLMStock item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMStock_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public FLMStock FLMStock_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMStock_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMStock_Delete(FLMStock item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.FLMStock_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMStock_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMStock_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMStock_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMStock_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMStock_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMStock_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMStock_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMStock_ExcelApprove(id);
                }
            }
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
        public DTOResult Location_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Location_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Location_Save(CATLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Location_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATLocation Location_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Location_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATLocationDetail Location_GetDetail(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Location_GetDetail(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Location_Delete(CATLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.Location_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public AddressSearchItem CATAddressSearch_List(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATAddressSearch_List(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATLocation> ExcelLocation_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ExcelLocation_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void Location_UpdateLatLng(List<CATLocation> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                     bl.Location_UpdateLatLng(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void ExcelLocation_Save(List<CATLocationImport> lstLocation)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.ExcelLocation_Save(lstLocation);
                }
            }
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

        #region CATTransportMode
        public DTOResult TransportMode_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.TransportMode_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int TransportMode_Save(CATTransportMode item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.TransportMode_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATTransportMode TransportMode_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.TransportMode_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void TransportMode_Delete(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.TransportMode_Delete(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel TransportMode_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.TransportMode_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row TransportMode_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.TransportMode_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel TransportMode_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.TransportMode_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public bool TransportMode_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.TransportMode_ExcelApprove(id);
                }
            }
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

        #region CATServiceOfOrder
        public DTOResult ServiceOfOrder_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ServiceOfOrder_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int ServiceOfOrder_Save(CATServiceOfOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ServiceOfOrder_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATServiceOfOrder ServiceOfOrder_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ServiceOfOrder_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void ServiceOfOrder_Delete(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.ServiceOfOrder_Delete(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel ServiceOfOrder_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ServiceOfOrder_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row ServiceOfOrder_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ServiceOfOrder_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel ServiceOfOrder_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ServiceOfOrder_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public bool ServiceOfOrder_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ServiceOfOrder_ExcelApprove(id);
                }
            }
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

        #region CATDriver
        public DTOResult Driver_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Driver_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Driver_Save(CATDriver item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Driver_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATDriver Driver_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Driver_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void Driver_Delete(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.Driver_Delete(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel Driver_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Driver_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row Driver_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Driver_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel Driver_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Driver_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool Driver_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Driver_ExcelApprove(id);
                }
            }
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
       
        #region DrivingLicence
        public DTOResult DrivingLicence_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.DrivingLicence_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATDrivingLicence DrivingLicence_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.DrivingLicence_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int DrivingLicence_Save(CATDrivingLicence item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.DrivingLicence_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DrivingLicence_Delete(CATDrivingLicence item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.DrivingLicence_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel DrivingLicence_ExcelInit(int functionid, string functionkey ,bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.DrivingLicence_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row DrivingLicence_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.DrivingLicence_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel DrivingLicence_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.DrivingLicence_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool DrivingLicence_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.DrivingLicence_ExcelApprove(id);
                }
            }
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

        #region Service
        public DTOResult GroupOfService_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.GroupOfService_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Cost_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Cost_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CostRevenue_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CostRevenue_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Packing_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Packing_List();
                }
            }
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

        #region CategoryFactory
        public List<CATVehicle> Customer_Vehicle(int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Customer_Vehicle(customerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATRomooc> Customer_Romooc(int? customerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Customer_Romooc(customerid);
                }
            }
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

        #region Container Packing
        public DTOResult ContainerPacking_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ContainerPacking_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATPackingCO_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATPackingCO_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATPacking ContainerPacking_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ContainerPacking_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int ContainerPacking_Save(DTOCATPacking item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ContainerPacking_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void ContainerPacking_Delete(DTOCATPacking item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.ContainerPacking_Delete(item);
                }
            }
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

        #region Thiết lập cung đường
        public DTOResult RoutingAll_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingAll_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Routing_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Routing_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int Routing_Save(DTOCATRouting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Routing_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATRouting Routing_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Routing_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Routing_Delete(DTOCATRouting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
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

        public void Routing_UpdateLocationForAllRouting()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.Routing_UpdateLocationForAllRouting();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Routing_SaveAllCustomer(List<int> lstRoutingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.Routing_SaveAllCustomer(lstRoutingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult RoutingLocationNotIn_List(string request, int? fromID, int? toID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingLocationNotIn_List(request, fromID, toID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult RoutingAreaNotIn_List(string request, int? areafromID, int? areatoID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingAreaNotIn_List(request, areafromID, areatoID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int RoutingArea_Save(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingArea_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATRoutingArea RoutingArea_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingArea_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void RoutingArea_Delete(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.RoutingArea_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult RoutingAreaDetail_List(int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingAreaDetail_List(areaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int RoutingAreaDetail_Save(DTOCATRoutingAreaDetail item, int areaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingAreaDetail_Save(item, areaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATRoutingAreaDetail RoutingAreaDetail_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingAreaDetail_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void RoutingAreaDetail_Delete(DTOCATRoutingAreaDetail item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.RoutingAreaDetail_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void RoutingAreaDetail_Refresh(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.RoutingAreaDetail_Refresh(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult RoutingCost_List(string request, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingCost_List(request, routingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATRoutingCost RoutingCost_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingCost_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int RoutingCost_Save(DTOCATRoutingCost item, int routingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.RoutingCost_Save(item, routingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void RoutingCost_Delete(DTOCATRoutingCost item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.RoutingCost_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ExcelRoutingCost_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ExcelRoutingCost_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ExcelRoutingCost_HeaderList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ExcelRoutingCost_HeaderList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult ExcelRoutingArea_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ExcelRoutingArea_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATCost> ExcelCost_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ExcelCost_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATRoutingArea> ExcelArea_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ExcelArea_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void ExcelRoutingCost_Save(List<DTOExcelRoute> lstRoute)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.ExcelRoutingCost_Save(lstRoute);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void ExcelArea_Save(List<DTOExcelRouteArea> lstArea)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.ExcelArea_Save(lstArea);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATProvince> ExcelProvince_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ExcelProvince_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATDistrict> ExcelDistrict_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ExcelDistrict_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<AddressSearchItem> AddressSearch_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.AddressSearch_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOExcelRouteAreaLocation> ExcelRouteAreaLocation_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ExcelRouteAreaLocation_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATRoutingExcelData CATRouting_ExcelData()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRouting_ExcelData();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATRouting_AllCustomerList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRouting_AllCustomerList();
                }
            }
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

        #region CAT_Vehicle

        public DTOResult CATVehicle_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATVehicle_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATVehicle CATVehicle_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATVehicle_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATVehicle_Save(DTOCATVehicle item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATVehicle_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATVehicle_Delete(DTOCATVehicle item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATVehicle_Delete(item);
                }
            }
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

        #region CAT_GroupOfCost, CAT_Cost
        public DTOResult CATCost_GroupList(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCost_GroupList(id);
                }
            }
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

        #region Import Hãng tàu, Cảng biển, NPP
        public DTOResult Distributor_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Distributor_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Carrier_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Carrier_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult SeaPort_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SeaPort_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Station_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Station_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult DistributorLocation_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.DistributorLocation_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CarrierLocation_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CarrierLocation_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult SeaPortLocation_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SeaPortLocation_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult StationLocation_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.StationLocation_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult GroupOfPartner_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.GroupOfPartner_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Distributor_Import(List<DTOCATPartnerImport> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.Distributor_Import(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Carrier_Import(List<DTOCATPartnerImport> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.Carrier_Import(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SeaPort_Import(List<DTOCATPartnerImport> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.SeaPort_Import(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Station_Import(List<DTOCATPartnerImport> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.Station_Import(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DistributorLocation_Import(DTOCUSPartnerLocationImport item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.DistributorLocation_Import(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CarrierLocation_Import(DTOCUSPartnerLocationImport item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CarrierLocation_Import(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SeaPortLocation_Import(DTOCUSPartnerLocationImport item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.SeaPortLocation_Import(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void StationLocation_Import(DTOCUSPartnerLocationImport item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.StationLocation_Import(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATPartnerImport> Distributor_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Distributor_Export();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATPartnerImport> SeaPort_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SeaPort_Export();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATPartnerImport> Station_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Station_Export();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATPartnerImport> Carrier_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.Carrier_Export();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSPartnerLocationExport PartnerLocation_Distributor_Export(List<int> lstCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.PartnerLocation_Distributor_Export(lstCustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSPartnerLocationExport PartnerLocation_Carrier_Export(List<int> lstCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.PartnerLocation_Carrier_Export(lstCustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSPartnerLocationExport PartnerLocation_SeaPort_Export(List<int> lstCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.PartnerLocation_SeaPort_Export(lstCustomerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSPartnerLocationExport PartnerLocation_Station_Export(List<int> lstCustomerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.PartnerLocation_Station_Export(lstCustomerID);
                }
            }
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

        #region CATConstraint

        public DTOResult CATConstraint_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATConstraint CATConstraint_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATConstraint_Save(DTOCATConstraint item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_Delete(DTOCATConstraint item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_UpdateConstraint(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_UpdateConstraint(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATConstraint_Route_List(string request, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_Route_List(request, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_RouteNotIn_Save(List<int> lstid, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_RouteNotIn_Save(lstid, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_Route_Delete(DTOCATConstraintAllocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_Route_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATConstraint_RouteNotIn_List(string request, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_RouteNotIn_List(request, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATConstraint_Location_List(string request, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_Location_List(request, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_LocationNotIn_Save(List<int> lstid, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_LocationNotIn_Save(lstid, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_Location_Delete(DTOCATConstraintAllocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_Location_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATConstraint_LocationNotIn_List(string request, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_LocationNotIn_List(request, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATConstraint_Vehicle_List(string request, int ID, SYSVarType typeAssetVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_Vehicle_List(request, ID, typeAssetVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_VehicleNotIn_Save(List<int> lstid, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_VehicleNotIn_Save(lstid, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_Vehicle_Delete(DTOCATConstraintAllocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_Vehicle_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATConstraint_VehicleNotIn_List(string request, int ID, SYSVarType typeAssetVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_VehicleNotIn_List(request, ID, typeAssetVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATConstraint_OpenHour_List(string request, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_OpenHour_List(request, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATConstraintRequire CATConstraint_OpenHour_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_OpenHour_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_OpenHour_Save(DTOCATConstraintRequire item, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_OpenHour_Save(item, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_OpenHour_Delete(DTOCATConstraintRequire item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_OpenHour_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATConstraint_Weight_List(string request, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_Weight_List(request, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATConstraintRequire CATConstraint_Weight_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_Weight_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_Weight_Save(DTOCATConstraintRequire item, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_Weight_Save(item, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_Weight_Delete(DTOCATConstraintRequire item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_Weight_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATConstraint_KM_List(string request, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_KM_List(request, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATConstraintRequire CATConstraint_KM_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATConstraint_KM_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_KM_Save(DTOCATConstraintRequire item, int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_KM_Save(item, ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATConstraint_KM_Delete(DTOCATConstraintRequire item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATConstraint_KM_Delete(item);
                }
            }
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

        #region CATCurrency

        public DTOResult CATCurrency_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCurrency_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATCurrency_Save(CATCurrency item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCurrency_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATCurrency CATCurrency_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCurrency_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATCurrency_Delete(CATCurrency item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATCurrency_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATCurrency> CATCurrency_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCurrency_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATCurrency_ExcelInit(int functionid, string functionkey)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCurrency_ExcelInit(functionid, functionkey);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<Row> CATCurrency_ExcelChange(long id, int row, int col, string val)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCurrency_ExcelChange(id, row, col, val);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATCurrency_ExcelImport(long id, List<Row> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCurrency_ExcelImport(id, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATCurrency_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCurrency_ExcelApprove(id);
                }
            }
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

        #region CATReason

        public DTOResult CATReason_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATReason_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATReason_Save(DTOCATReason item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATReason_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATReason CATReason_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATReason_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATReason_Delete(DTOCATReason item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATReason_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATReason_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATReason_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row CATReason_ExcelChange(long id, int row, List<Cell> cells)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATReason_ExcelChange(id, row, cells);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATReason_ExcelImport(long id, List<Row> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATReason_ExcelImport(id, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATReason_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATReason_ExcelApprove(id);
                }
            }
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

        #region CATGroupOfVehicle
        public DTOResult CATGroupOfVehicle_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfVehicle_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATGroupOfVehicle_Save(CATGroupOfVehicle item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfVehicle_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATGroupOfVehicle CATGroupOfVehicle_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfVehicle_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATGroupOfVehicle_Delete(CATGroupOfVehicle item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfVehicle_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfVehicle_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfVehicle_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATGroupOfVehicle_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfVehicle_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfVehicle_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfVehicle_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATGroupOfVehicle_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfVehicle_ExcelApprove(id);
                }
            }
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

        #region CATGroupOfRomooc
        public DTOResult CATGroupOfRomooc_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfRomooc_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATGroupOfRomooc_Save(CATGroupOfRomooc item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfRomooc_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATGroupOfRomooc CATGroupOfRomooc_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfRomooc_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATGroupOfRomooc_Delete(CATGroupOfRomooc item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfRomooc_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfRomooc_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfRomooc_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATGroupOfRomooc_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfRomooc_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfRomooc_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfRomooc_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATGroupOfRomooc_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfRomooc_ExcelApprove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATGroupOfRomoocPacking_List(string request, int GroupOfRomoocID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfRomoocPacking_List(request, GroupOfRomoocID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATGroupOfRomoocPacking_NotInList(string request, int GroupOfRomoocID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfRomoocPacking_NotInList(request, GroupOfRomoocID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATGroupOfRomoocPacking_Save(List<CATGroupOfRomoocPacking> lst, int GroupOfRomoocID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfRomoocPacking_Save(lst, GroupOfRomoocID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATGroupOfRomoocPacking_Delete(CATGroupOfRomoocPacking item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfRomoocPacking_Delete(item);
                }
            }
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

        #region CATGroupOfEquipment
        public DTOResult CATGroupOfEquipment_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfEquipment_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATGroupOfEquipment_Save(CATGroupOfEquipment item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfEquipment_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATGroupOfEquipment CATGroupOfEquipment_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfEquipment_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATGroupOfEquipment_Delete(CATGroupOfEquipment item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfEquipment_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfEquipment_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfEquipment_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATGroupOfEquipment_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfEquipment_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfEquipment_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfEquipment_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATGroupOfEquipment_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfEquipment_ExcelApprove(id);
                }
            }
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

        #region CATGroupOfMaterial
        public DTOResult CATGroupOfMaterial_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfMaterial_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATGroupOfMaterial_Save(CATGroupOfMaterial item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfMaterial_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATGroupOfMaterial CATGroupOfMaterial_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfMaterial_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATGroupOfMaterial_Delete(CATGroupOfMaterial item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfMaterial_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public SYSExcel CATGroupOfMaterial_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfMaterial_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATGroupOfMaterial_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfMaterial_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfMaterial_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfMaterial_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATGroupOfMaterial_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfMaterial_ExcelApprove(id);
                }
            }
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

        #region CATMaterial
        public DTOResult FLMMaterial_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMMaterial_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMMaterial_Save(FLMMaterial item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMMaterial_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public FLMMaterial FLMMaterial_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMMaterial_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMMaterial_Delete(FLMMaterial item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.FLMMaterial_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public SYSExcel CATMaterial_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATMaterial_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATMaterial_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATMaterial_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATMaterial_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATMaterial_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATMaterial_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATMaterial_ExcelApprove(id);
                }
            }
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

        #region CATRomooc
        public DTOResult CATRomooc_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRomooc_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATRomooc_Save(CATRomooc item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRomooc_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATRomooc CATRomooc_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRomooc_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATRomooc_Delete(CATRomooc item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATRomooc_Delete(item);
                }
            }
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

        #region CATGroupOfService
        public DTOResult CATGroupOfService_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfService_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATGroupOfService_Save(CATGroupOfService item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfService_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATGroupOfService CATGroupOfService_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfService_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATGroupOfService_Delete(CATGroupOfService item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfService_Delete(item);
                }
            }
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

        #region CATService
        public DTOResult CATService_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATService_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATService_Save(CATService item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATService_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATService CATService_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATService_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATService_Delete(CATService item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATService_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATService_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATService_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATService_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATService_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATService_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATService_ExcelImport(id, lst,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATService_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATService_ExcelApprove(id);
                }
            }
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

        #region CATGroupOfPartner
        public DTOResult CATGroupOfPartner_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfPartner_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATGroupOfPartner_Save(CATGroupOfPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfPartner_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATGroupOfPartner CATGroupOfPartner_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfPartner_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATGroupOfPartner_Delete(CATGroupOfPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfPartner_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfPartner_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfPartner_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATGroupOfPartner_ExcelChange(long id, int row, List<Cell> cells,List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfPartner_ExcelChange(id, row, cells,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfPartner_ExcelImport(long id, List<Row> lst,List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfPartner_ExcelImport(id, lst,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATGroupOfPartner_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfPartner_ExcelApprove(id);
                }
            }
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

        #region CATShift
        public DTOResult CATShift_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATShift_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATShift_Save(CATShift item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATShift_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATShift CATShift_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATShift_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATShift_Delete(CATShift item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATShift_Delete(item);
                }
            }
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

        #region CATGroupOfTrouble
        public DTOResult CATGroupOfTrouble_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfTrouble_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATGroupOfTrouble_Save(CATGroupOfTrouble item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfTrouble_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATGroupOfTrouble CATGroupOfTrouble_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfTrouble_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATGroupOfTrouble_Delete(CATGroupOfTrouble item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfTrouble_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfTrouble_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfTrouble_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATGroupOfTrouble_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfTrouble_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfTrouble_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfTrouble_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATGroupOfTrouble_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfTrouble_ExcelApprove(id);
                }
            }
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

        #region CATTypeBusiness
        public DTOResult CATTypeBusiness_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeBusiness_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATTypeBusiness_Save(CATTypeBusiness item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeBusiness_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATTypeBusiness CATTypeBusiness_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeBusiness_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATTypeBusiness_Delete(CATTypeBusiness item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATTypeBusiness_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATTypeBusiness_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeBusiness_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATTypeBusiness_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeBusiness_ExcelChange(id, row, cells,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATTypeBusiness_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeBusiness_ExcelImport(id, lst,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATTypeBusiness_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeBusiness_ExcelApprove(id);
                }
            }
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

        #region CATField
        public DTOResult CATField_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATField_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATField_Save(CATField item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATField_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATField CATField_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATField_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATField_Delete(CATField item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATField_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }



        public SYSExcel CATField_ExcelInit(int functionid, string functionkey)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATField_ExcelInit(functionid, functionkey);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<Row> CATField_ExcelChange(long id, int row, int col, string val)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATField_ExcelChange(id, row, col, val);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public SYSExcel CATField_ExcelImport(long id, List<Row> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATField_ExcelImport(id, lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATField_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATField_ExcelApprove(id);
                }
            }
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

        #region CATScale
        public DTOResult CATScale_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATScale_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATScale_Save(CATScale item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATScale_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATScale CATScale_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATScale_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATScale_Delete(CATScale item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATScale_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public SYSExcel CATScale_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATScale_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATScale_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATScale_ExcelChange(id, row, cells,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATScale_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATScale_ExcelImport(id, lst,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATScale_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATScale_ExcelApprove(id);
                }
            }
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

        #region CATGroupOfCost
        public DTOResult CATGroupOfCost_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfCost_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATGroupOfCost_Save(CATGroupOfCost item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfCost_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATGroupOfCost CATGroupOfCost_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfCost_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATGroupOfCost_Delete(CATGroupOfCost item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfCost_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATGroupOfCost_GroupList(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfCost_GroupList(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfCost_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfCost_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATGroupOfCost_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfCost_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfCost_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfCost_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATGroupOfCost_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfCost_ExcelApprove(id);
                }
            }
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

        #region CATCost
        public DTOResult CATCost_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCost_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATCost_Save(DTOCATCost item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCost_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATCost CATCost_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCost_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATCost_Delete(DTOCATCost item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATCost_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel CATCost_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCost_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row CATCost_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCost_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATCost_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCost_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATCost_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATCost_ExcelApprove(id);
                }
            }
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

        #region SYSCustomer

        public DTOResult SYSCustomer_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SYSCustomer_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int SYSCustomer_Save(CUSCustomer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SYSCustomer_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SYSCustomer_Delete(CUSCustomer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.SYSCustomer_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult SYSCustomer_Goal_List(string request, int branchID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SYSCustomer_Goal_List(request, branchID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int SYSCustomer_Goal_Save(CUSGoal item, int branchID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SYSCustomer_Goal_Save(item, branchID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SYSCustomer_Goal_Delete(CUSGoal item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.SYSCustomer_Goal_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SYSCustomer_Goal_Reset(int goalID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.SYSCustomer_Goal_Reset(goalID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult SYSCustomer_GoalDetail_List(string request, int goalID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SYSCustomer_GoalDetail_List(request, goalID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int SYSCustomer_GoalDetail_Save(CUSGoalDetail item, int goalID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SYSCustomer_GoalDetail_Save(item, goalID);
                }
            }
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

        #region SYSSetting

        public DTOSYSSetting SYSSetting_Get(int? syscusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SYSSetting_Get(syscusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SYSSetting_Save(DTOSYSSetting item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.SYSSetting_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SYSSetting_CheckApplySeaportCarrier()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.SYSSetting_CheckApplySeaportCarrier();
                }
            }
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

        #region SYSMaterial

        public DTOTriggerMaterial SYSMaterial_Get(int? syscusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.SYSMaterial_Get(syscusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void SYSMaterial_Save(DTOTriggerMaterial item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.SYSMaterial_Save(item);
                }
            }
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

        #region CATCost
        public DTOResult CATSYSCustomerTrouble_SysCustomer_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATSYSCustomerTrouble_SysCustomer_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATSYSCustomerTrouble_Trouble_List(string request, int SysCus)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATSYSCustomerTrouble_Trouble_List(request, SysCus);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATSYSCustomerTrouble_Trouble_Save(List<DTOCATSYSCustomerTrouble> lst, int SysCus)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATSYSCustomerTrouble_Trouble_Save(lst, SysCus);
                }
            }
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

        #region CATPackingGOPTU
        public DTOResult CATPackingGOP_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATPackingGOP_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATPackingGOPTU_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATPackingGOPTU_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATPacking CATPackingGOPTU_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATPackingGOPTU_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATPackingGOPTU_Save(DTOCATPacking item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATPackingGOPTU_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATPackingGOPTU_Delete(DTOCATPacking item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATPackingGOPTU_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATPackingGOPTU_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATPackingGOPTU_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATPackingGOPTU_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATPackingGOPTU_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel CATPackingGOPTU_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATPackingGOPTU_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public bool CATPackingGOPT_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATPackingGOPT_ExcelApprove(id);
                }
            }
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

        #region CATTypeOfRouteProblem
        public DTOResult CATTypeOfRouteProblem_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfRouteProblem_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATTypeOfRouteProblem_Save(DTOCATTypeOfRouteProblem item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfRouteProblem_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATTypeOfRouteProblem CATTypeOfRouteProblem_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfRouteProblem_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATTypeOfRouteProblem_Delete(DTOCATTypeOfRouteProblem item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATTypeOfRouteProblem_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATTypeOfRouteProblem_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfRouteProblem_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATTypeOfRouteProblem_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfRouteProblem_ExcelChange(id, row, cells,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATTypeOfRouteProblem_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfRouteProblem_ExcelImport(id, lst,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATTypeOfRouteProblem_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfRouteProblem_ExcelApprove(id);
                }
            }
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

        #region CATTypeOfPriceDIEx
        public DTOResult CATTypeOfPriceDIEx_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfPriceDIEx_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATTypeOfPriceDIEx_Save(DTOCATTypeOfPriceDIEx item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfPriceDIEx_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATTypeOfPriceDIEx CATTypeOfPriceDIEx_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfPriceDIEx_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATTypeOfPriceDIEx_Delete(DTOCATTypeOfPriceDIEx item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATTypeOfPriceDIEx_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATTypeOfPriceDIEx_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfPriceDIEx_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATTypeOfPriceDIEx_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfPriceDIEx_ExcelChange(id, row, cells,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATTypeOfPriceDIEx_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfPriceDIEx_ExcelImport(id, lst,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATTypeOfPriceDIEx_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfPriceDIEx_ExcelApprove(id);
                }
            }
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

        #region CATTypeOfPriceCOEx
        public DTOResult CATTypeOfPriceCOEx_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfPriceCOEx_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATTypeOfPriceCOEx_Save(DTOCATTypeOfPriceCOEx item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfPriceCOEx_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATTypeOfPriceCOEx CATTypeOfPriceCOEx_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfPriceCOEx_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATTypeOfPriceCOEx_Delete(DTOCATTypeOfPriceCOEx item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATTypeOfPriceCOEx_Delete(item);
                }
            }
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

        #region CATGroupOfLocation
        public DTOResult CATGroupOfLocation_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfLocation_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATGroupOfLocation_Save(DTOCATGroupOfLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfLocation_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATGroupOfLocation CATGroupOfLocation_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfLocation_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATGroupOfLocation_Delete(DTOCATGroupOfLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATGroupOfLocation_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfLocation_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfLocation_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row CATGroupOfLocation_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfLocation_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATGroupOfLocation_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfLocation_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATGroupOfLocation_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATGroupOfLocation_ExcelApprove(id);
                }
            }
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

        #region OPSTypeOfDITOGroupProductReturn
        public DTOResult OPSTypeOfDITOGroupProductReturn_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.OPSTypeOfDITOGroupProductReturn_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int OPSTypeOfDITOGroupProductReturn_Save(DTOOPSTypeOfDITOGroupProductReturn item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.OPSTypeOfDITOGroupProductReturn_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSTypeOfDITOGroupProductReturn OPSTypeOfDITOGroupProductReturn_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.OPSTypeOfDITOGroupProductReturn_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSTypeOfDITOGroupProductReturn_Delete(DTOOPSTypeOfDITOGroupProductReturn item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.OPSTypeOfDITOGroupProductReturn_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public SYSExcel OPSTypeOfDITOGroupProductReturn_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.OPSTypeOfDITOGroupProductReturn_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row OPSTypeOfDITOGroupProductReturn_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.OPSTypeOfDITOGroupProductReturn_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel OPSTypeOfDITOGroupProductReturn_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.OPSTypeOfDITOGroupProductReturn_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool OPSTypeOfDITOGroupProductReturn_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.OPSTypeOfDITOGroupProductReturn_ExcelApprove(id);
                }
            }
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

        #region CATTypeOfDriverFee
        public DTOResult CATTypeOfDriverFee_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfDriverFee_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATTypeOfDriverFee_Save(DTOCATTypeOfDriverFee item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfDriverFee_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATTypeOfDriverFee CATTypeOfDriverFee_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfDriverFee_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATTypeOfDriverFee_Delete(DTOCATTypeOfDriverFee item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATTypeOfDriverFee_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATTypeOfDriverFee_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfDriverFee_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row CATTypeOfDriverFee_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfDriverFee_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATTypeOfDriverFee_ExcelImport(long id, List<Row> lst,List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfDriverFee_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATTypeOfDriverFee_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATTypeOfDriverFee_ExcelApprove(id);
                }
            }
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

        #region ORD_TypeOfDoc
        public DTOResult ORDTypeOfDoc_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ORDTypeOfDoc_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int ORDTypeOfDoc_Save(DTOORDTypeOfDoc item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ORDTypeOfDoc_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOORDTypeOfDoc ORDTypeOfDoc_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.ORDTypeOfDoc_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void ORDTypeOfDoc_Delete(DTOORDTypeOfDoc item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.ORDTypeOfDoc_Delete(item);
                }
            }
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

        #region FLM_TypeOfScheduleFee
        public DTOResult FLMTypeOfScheduleFee_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMTypeOfScheduleFee_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMTypeOfScheduleFee_Save(DTOFLMTypeOfScheduleFee item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMTypeOfScheduleFee_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMTypeOfScheduleFee FLMTypeOfScheduleFee_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMTypeOfScheduleFee_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMTypeOfScheduleFee_Delete(DTOFLMTypeOfScheduleFee item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.FLMTypeOfScheduleFee_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel FLMTypeOfScheduleFee_ExcelInit(int functionid, string functionkey,bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMTypeOfScheduleFee_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public Row FLMTypeOfScheduleFee_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMTypeOfScheduleFee_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public SYSExcel FLMTypeOfScheduleFee_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMTypeOfScheduleFee_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public bool FLMTypeOfScheduleFee_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMTypeOfScheduleFee_ExcelApprove(id);
                }
            }
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

        #region CAT_LocationMatrix
        public DTOResult CATLocationMatrix_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrix_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATLocationMatrix_Save(CATLocationMatrix item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrix_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATLocationMatrix CATLocationMatrix_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrix_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATLocationMatrix_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                     bl.CATLocationMatrix_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATLocationMatrix_CreateFromOPS(bool isDI)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrix_CreateFromOPS(isDI);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region CATLocationMatrixDetail
        public DTOResult CATLocationMatrixDetail_List(string request, long LocationMatrixID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrixDetail_List(request, LocationMatrixID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public long CATLocationMatrixDetail_Save(CATLocationMatrixDetail item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrixDetail_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CATLocationMatrixDetail CATLocationMatrixDetail_Get(long ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrixDetail_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATLocationMatrixDetail_Detele(long ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrixDetail_Detele(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region CAT_LocationMatrixDetailStation
        public DTOResult CATLocationMatrixDetailStation_List(string request, long LocationMatrixDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrixDetailStation_List(request, LocationMatrixDetailID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATLocationMatrixDetailStationNotIn_SaveList(List<int> lstStaionID, long LocationMatrixDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrixDetailStationNotIn_SaveList(lstStaionID, LocationMatrixDetailID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATLocationMatrixDetailStation_SaveList(List<DTOCATLocationMatrixStation> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrixDetailStation_SaveList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATLocationMatrixDetailStation_DeleteList(List<int> lstId)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrixDetailStation_DeleteList(lstId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATLocationMatrixDetailStation_LocationList(string request, long LocationMatrixDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrixDetailStation_LocationList(request, LocationMatrixDetailID);
                }
            }
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

        public void CATLocationMatrix_Generate()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrix_Generate();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATLocationMatrix_SaveList(List<CATLocationMatrix> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrix_SaveList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATLocationMatrix_GenerateByList(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrix_GenerateByList(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATLocationMatrix_GenerateLimit(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrix_GenerateLimit(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATLocationMatrix_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrix_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row CATLocationMatrix_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrix_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATLocationMatrix_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrix_ExcelImport(id, lst,lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATLocationMatrix_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrix_ExcelApprove(id);
                }
            }
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

        #region CATStation
        public List<CATPartner> CATStation_Data()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStation_Data();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATPartner CATStation_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStation_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int CATStation_Save(CATPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStation_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATStation_Delete(CATPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATStation_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult LocationInStation_List(string request, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInStation_List(request, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATLocationInPartner LocationInStation_Save(DTOCATLocationInPartner item, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInStation_Save(item, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATLocationInPartner LocationInStation_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationInStation_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void LocationInStation_Delete(DTOCATLocationInPartner item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.LocationInStation_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult LocationNotInStation_List(string request, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.LocationNotInStation_List(request, partnerid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void LocationNotInStation_SaveList(List<DTOCATLocationInPartner> lst, int partnerid)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.LocationNotInStation_SaveList(lst, partnerid);
                }
            }
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

        #region CATSupplier
        public DTOResult FLMSupplier_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMSupplier_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public FLMSupplier FLMSupplier_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMSupplier_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMSupplier_Save(FLMSupplier item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMSupplier_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMSupplier_Delete(FLMSupplier item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.FLMSupplier_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMSupplier_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMSupplier_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row FLMSupplier_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMSupplier_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel FLMSupplier_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMSupplier_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool FLMSupplier_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMSupplier_ExcelApprove(id);
                }
            }
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

        #region CATMaterialPrice
        public DTOResult FLMMaterialPrice_List(int supplierID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMMaterialPrice_List(supplierID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOFLMMaterialPrice FLMMaterialPrice_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMMaterialPrice_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int FLMMaterialPrice_Save(DTOFLMMaterialPrice item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMMaterialPrice_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FLMaterialPrice_Delete(DTOFLMMaterialPrice item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.FLMaterialPrice_Delete(item);
                }
            }
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

        #region CATStationPrice
        public DTOResult CATStationPrice_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationPrice_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATStationPrice CATStationPrice_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationPrice_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATStationPrice_Save(DTOCATStationPrice item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATStationPrice_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATStationPrice_Delete(DTOCATStationPrice item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATStationPrice_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOCATLocation> CATStationPrice_LocationList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationPrice_LocationList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOFLMVehicle> CATStationPrice_AssetList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationPrice_AssetList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATStationPriceImport> CATStationPriceExport()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationPriceExport();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATStationPriceData CATStationPrice_Data()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationPrice_Data();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATStationPriceImport(List<DTOCATStationPriceImport> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATStationPriceImport(lst);
                }
            }
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

        #region CATStationMonth
        public DTOResult CATStationMonth_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationMonth_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATStationMonth CATStationMonth_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationMonth_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATStationMonth_Save(DTOCATStationMonth item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATStationMonth_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATStationMonth_Delete(DTOCATStationMonth item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATStationMonth_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOCATLocation> CATStationMonth_LocationList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationMonth_LocationList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOFLMVehicle> CATStationMonth_AssetList()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationMonth_AssetList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATStationMonthExcel> CATStationMonthExport()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationMonthExport();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATStationMonthData CATStationMonth_Data()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATStationMonth_Data();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATStationMonthImport(List<DTOCATStationMonthExcel> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATStationMonthImport(lst);
                }
            }
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

        #region CATStationMonth
        public DTOResult CATLocationMatrixStation_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrixStation_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCATLocationMatrixStation CATLocationMatrixStation_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrixStation_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATLocationMatrixStation_Save(DTOCATLocationMatrixStation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrixStation_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATLocationMatrixStation_Delete(DTOCATLocationMatrixStation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATLocationMatrixStation_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATLocationMatrixStation_LocationMatrixList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrixStation_LocationMatrixList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult CATLocationMatrixStation_LocationList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationMatrixStation_LocationList(request);
                }
            }
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

        #region CATRoutingArea
        public DTOResult CATRoutingArea_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingArea_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATRoutingArea_Save(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingArea_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CATRoutingArea CATRoutingArea_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingArea_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CATRoutingArea_Delete(CATRoutingArea item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATRoutingArea_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATRoutingAreaDetail_List(string request, int routingAreaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingAreaDetail_List(request, routingAreaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int CATRoutingAreaDetail_Save(DTOCATRoutingAreaDetail item, int routingAreaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingAreaDetail_Save(item, routingAreaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATRoutingAreaDetail CATRoutingAreaDetail_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingAreaDetail_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATRoutingAreaDetail_Delete(DTOCATRoutingAreaDetail item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATRoutingAreaDetail_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATRoutingArea_Location_List(string request, int routingAreaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingArea_Location_List(request, routingAreaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATRoutingArea_LocationNotIn_List(string request, int routingAreaID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingArea_LocationNotIn_List(request, routingAreaID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATRoutingArea_LocationNotIn_Save(int routingAreaID, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATRoutingArea_LocationNotIn_Save(routingAreaID, lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATRoutingArea_Location_Delete(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATRoutingArea_Location_Delete(lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATRoutingAreaLocation_Refresh(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATRoutingAreaLocation_Refresh(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATRoutingArea_AreaNotIn_AreaList(string request, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingArea_AreaNotIn_AreaList(request, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATRoutingArea_AreaLocation_Copy(int areaID, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATRoutingArea_AreaLocation_Copy(areaID, lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CATRoutingAreaExcel_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingAreaExcel_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATRoutingAreaExcel_Save(List<DTOExcelRouteArea> lstArea)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATRoutingAreaExcel_Save(lstArea);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOExcelRouteAreaLocation> CATRouteAreaLocationExcel_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRouteAreaLocationExcel_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATRoutingArea_ExcelInit(int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingArea_ExcelInit(functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public Row CATRoutingArea_ExcelChange(long id, int row, List<Cell> cells, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingArea_ExcelChange(id, row, cells, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public SYSExcel CATRoutingArea_ExcelImport(long id, List<Row> lst, List<string> lstMessageError)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingArea_ExcelImport(id, lst, lstMessageError);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool CATRoutingArea_ExcelApprove(long id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATRoutingArea_ExcelApprove(id);
                }
            }
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

        #region DTOFLMTypeWarning
        public DTOResult FLMTypeWarning_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMTypeWarning_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FLMTypeWarning_Save(DTOFLMTypeWarning item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMTypeWarning_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFLMTypeWarning FLMTypeWarning_Get(int ID)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.FLMTypeWarning_Get(ID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FLMTypeWarning_Delete(DTOFLMTypeWarning item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.FLMTypeWarning_Delete(item);
                }
            }
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

        #region CAT Vessel

        public DTOResult CATVessel_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATVessel_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATVessel_Save(DTOCATVessel item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATVessel_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOCATVessel CATVessel_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATVessel_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATPartner> CboCATPartner_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CboCATPartner_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CATVessel_Delete(DTOCATVessel item)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    bl.CATVessel_Delete(item);
                }
            }
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

        #region catlocation  test
        public List<DTOOPSDI_Map_Schedule_Event> CATLocationTest_Read(DateTime dtStart,DateTime dtEnd)
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationTest_Read(dtStart,dtEnd);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public List<DTOOPSDI_Map_Schedule_Group> CATLocationTest_Resource()
        {
            try
            {
                using (var bl = CreateBusiness<BLCategory>())
                {
                    return bl.CATLocationTest_Resource();
                }
            }
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