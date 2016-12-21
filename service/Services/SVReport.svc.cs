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
    public partial class SVReport : SVBase, ISVReport
    {
        #region Common

        public DTOResult Customer_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
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

        public DTOResult GroupOfProduct_List(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.GroupOfProduct_List(lstid);
                }
            }
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
                using (var bl = CreateBusiness<BLReport>())
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

        public DTOResult Vendor_Read()
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.Vendor_Read();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FLMDriver_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.FLMDriver_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult GroupOfLocation_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.GroupOfLocation_List();
                }
            }
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
                using (var bl = CreateBusiness<BLReport>())
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
        #endregion

        #region REPDIPivotOrder
        public List<REPDIPivotOrder> REPDIPivotOrder_List(List<int> lstid, DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPivotOrder_List(lstid, dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOREPTRPL_Detail> REPDIPivotOrder_Read(List<int> lstid, DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPivotOrder_Read(lstid, dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public REPDIPLPivot_Template REPDIPivotOrder_GetTemplate(int functionID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPivotOrder_GetTemplate(functionID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void REPDIPivotOrder_SaveTemplate(REPDIPLPivot_TemplateDetail item, int functionID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    bl.REPDIPivotOrder_SaveTemplate(item, functionID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void REPDIPivotOrder_DeleteTemplate(int functionID, List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    bl.REPDIPivotOrder_DeleteTemplate(functionID, lstid);
                }
            }
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

        #region REPCOPivotOrder
        public REPCOPLPivot_Template REPCOPivotOrder_GetTemplate(int functionID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPCOPivotOrder_GetTemplate(functionID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void REPCOPivotOrder_SaveTemplate(REPCOPLPivot_TemplateDetail item, int functionID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    bl.REPCOPivotOrder_SaveTemplate(item, functionID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void REPCOPivotOrder_DeleteTemplate(int functionID, List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    bl.REPCOPivotOrder_DeleteTemplate(functionID, lstid);
                }
            }
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

        #region REPDIPivotPOD
        public List<REPDIPivotPOD> REPDIPivotPOD_List(List<int> lstid, DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPivotPOD_List(lstid, dtfrom, dtto);
                }
            }
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

        #region REPDISchedulePivot
        public List<REPDISchedulePivot> REPDISchedulePivot_Data(List<int> lstid, DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDISchedulePivot_Data(lstid, dtfrom, dtto);
                }
            }
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

        #region REPDIOPSPlan
        public List<DTOREPOPSPlan_Detail> REPDIOPSPlan_DetailData(List<int> lstid, List<int> lstgroupid, DateTime dtfrom, DateTime dtto, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIOPSPlan_DetailData(lstid, lstgroupid, dtfrom, dtto, statusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPOPSPlan_ColumnDetail REPDIOPSPlan_ColumnDetailData(List<int> lstid, List<int> lstgroupid, DateTime dtfrom, DateTime dtto, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIOPSPlan_ColumnDetailData(lstid, lstgroupid, dtfrom, dtto, statusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPOPSPlan_ColumnDetail REPDIOPSPlan_ColumnDetailGroupStockData(List<int> lstid, List<int> lstgroupid, DateTime dtfrom, DateTime dtto, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIOPSPlan_ColumnDetailGroupStockData(lstid, lstgroupid, dtfrom, dtto, statusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTOREPOPSPlan_Order> REPDIOPSPlan_OrderData(List<int> lstid, List<int> lstgroupid, DateTime dtfrom, DateTime dtto, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIOPSPlan_OrderData(lstid, lstgroupid, dtfrom, dtto, statusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPOPSPlan_ColumnOrder REPDIOPSPlan_ColumnOrderData(List<int> lstid, List<int> lstgroupid, DateTime dtfrom, DateTime dtto, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIOPSPlan_ColumnOrderData(lstid, lstgroupid, dtfrom, dtto, statusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPOPSPlan_ColumnOrder REPDIOPSPlan_ColumnOrderGroupStockData(List<int> lstid, List<int> lstgroupid, DateTime dtfrom, DateTime dtto, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIOPSPlan_ColumnOrderGroupStockData(lstid, lstgroupid, dtfrom, dtto, statusID);
                }
            }
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

        #region REPDIPL
        public List<DTOREPTRPL_Detail> REPDIPL_DetailData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_DetailData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPTRPL_ColumnDetail REPDIPL_ColumnDetailData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_ColumnDetailData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPTRPL_ColumnDetail REPDIPL_ColumnDetailGroupStockData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_ColumnDetailGroupStockData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPTRPL_ColumnDetail REPDIPL_ColumnDetailMOQData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_ColumnDetailMOQData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPTRPL_ColumnDetail REPDIPL_ColumnDetailGroupProductData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_ColumnDetailGroupProductData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTOREPTRPL_Order> REPDIPL_OrderData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_OrderData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPTRPL_ColumnOrder REPDIPL_ColumnOrderData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_ColumnOrderData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPTRPL_ColumnOrder REPDIPL_ColumnOrderGroupStockData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_ColumnOrderGroupStockData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPTRPL_ColumnOrder REPDIPL_ColumnOrderMOQData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_ColumnOrderMOQData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPTRPL_ColumnOrder REPDIPL_ColumnOrderGroupProductData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_ColumnOrderGroupProductData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOREPTRPL_Spotrate> REPDIPL_SpotrateData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_SpotrateData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCUSStock> REPDIPL_StockList(List<int> lstCusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPL_StockList(lstCusID);
                }
            }
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

        #region REPDIPOD
        
        public List<DTOREPDIPOD_Detail> REPDIPOD_DetailData(List<int> lstid, List<int> lstgroupid, DateTime dtfrom, DateTime dtto, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPOD_DetailData(lstid, lstgroupid, dtfrom, dtto, statusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPDIPOD_ColumnDetail REPDIPOD_ColumnDetailData(List<int> lstid, List<int> lstgroupid, DateTime dtfrom, DateTime dtto, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPOD_ColumnDetailData(lstid, lstgroupid, dtfrom, dtto, statusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOREPDIPOD_Detail> REPDIPOD_DetailPODData(List<int> lstid, List<int> lstgroupid, DateTime dtfrom, DateTime dtto, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIPOD_DetailPODData(lstid, lstgroupid, dtfrom, dtto, statusID);
                }
            }
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

        #region REPDIReturn (Công nợ trả về)
        public List<DTOREPDIReturn_Detail> REPDIReturn_DetailData(List<int> lstid, DateTime dtfrom, DateTime dtto, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIReturn_DetailData(lstid, dtfrom, dtto, request);
                }
            }
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

        #region REPDIKPI
        public DTOREPDIKPI_Time REPDIKPI_TimeData(List<int> lstid, List<int> lstgroupid, List<int> lstStockID, DateTime dtfrom, DateTime dtto, bool isKPI, int? typeOfFilter, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIKPI_TimeData(lstid, lstgroupid, lstStockID, dtfrom, dtto, isKPI, typeOfFilter, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPDIKPI_Time REPDIKPI_Time_OrderData(List<int> lstid, List<int> lstgroupid, List<int> lstStockID, DateTime dtfrom, DateTime dtto, bool isKPI, int? typeOfFilter, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIKPI_Time_OrderData(lstid, lstgroupid, lstStockID, dtfrom, dtto, isKPI, typeOfFilter, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPDIKPI_Time REPDIKPI_TimeDate_Data(List<int> lstid, List<int> lstgroupid, List<int> lstStockID, DateTime dtfrom, DateTime dtto, bool isKPI, int? typeOfFilter, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIKPI_TimeDate_Data(lstid, lstgroupid, lstStockID, dtfrom, dtto, isKPI, typeOfFilter, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPDIKPI_Time REPDIKPI_TimeDate_OrderData(List<int> lstid, List<int> lstgroupid, List<int> lstStockID, DateTime dtfrom, DateTime dtto, bool isKPI, int? typeOfFilter, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIKPI_TimeDate_OrderData(lstid, lstgroupid, lstStockID, dtfrom, dtto, isKPI, typeOfFilter, request);
                }
            }
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

        #region REPCOOPSPlan
        public List<DTOREPCOOPSPlan_Detail> REPCOOPSPlan_DetailData(List<int> lstid, DateTime dtfrom, DateTime dtto, int statusID, List<int> listServiceOfOrderID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPCOOPSPlan_DetailData(lstid, dtfrom, dtto, statusID, listServiceOfOrderID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPCOOPSPlan_ColumnDetail REPCOOPSPlan_ColumnDetailData(List<int> lstid, DateTime dtfrom, DateTime dtto, int statusID, List<int> listServiceOfOrderID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPCOOPSPlan_ColumnDetailData(lstid, dtfrom, dtto, statusID, listServiceOfOrderID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOREPCOOPSPlan_Order> REPCOOPSPlan_OrderData(List<int> lstid, DateTime dtfrom, DateTime dtto, int statusID, List<int> listServiceOfOrderID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPCOOPSPlan_OrderData(lstid, dtfrom, dtto, statusID, listServiceOfOrderID);
                }
            }
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

        #region REPCOPL
        public List<DTOREPCOPL_Detail> REPCOPL_DetailData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPCOPL_DetailData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPCOPL_ColumnDetail REPCOPL_ColumnDetailData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPCOPL_ColumnDetailData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTOREPCOPL_Order> REPCOPL_OrderData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPCOPL_OrderData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPCOPL_ColumnOrder REPCOPL_ColumnOrderData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPCOPL_ColumnOrderData(filter);
                }
            }
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


        #region MAP
        public DTOCartoDB CartoDB_List(string request, List<int> dataCusID, DateTime DateFrom, DateTime DateTo, int provinceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CartoDB_List(request, dataCusID, DateFrom, DateTo, provinceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<string> CartoDB_Vehicle_List(DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CartoDB_Vehicle_List(DateFrom, DateTo);
                }
            }
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

        #region Đội xe
        public List<DTOREPOwner_DriverFee> REPOwner_DriverFee(int scheduleID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_DriverFee(scheduleID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPOwner_DriverFee_ColumnDetail REPOwner_DriverFee_ColumnDetailData(int scheduleID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_DriverFee_ColumnDetailData(scheduleID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTOREPOwner_Vehicle> REPOwner_VehicleFee(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_VehicleFee(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPOwner_VehicleFee_PL_ColumnDetail REPOwner_VehicleFee_ColumnDetailData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_VehicleFee_ColumnDetailData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOREPOwner_Vehicle> REPOwner_VehiclePriceData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_VehiclePriceData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPOwner_VehicleFee_PL_ColumnDetail REPOwner_VehiclePrice_ColumnDetailData(CUSSettingsReport_Filter filter)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_VehiclePrice_ColumnDetailData(filter);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOREPOwner_Vehicle> REPOwner_VehicleFee_Cost(DateTime dtfrom, DateTime dtto, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_VehicleFee_Cost(dtfrom, dtto, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOREPOwner_VehicleFee_ColumnDetail REPOwner_VehicleFee_Cost_ColumnDetailData(DateTime dtfrom, DateTime dtto, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_VehicleFee_Cost_ColumnDetailData(dtfrom, dtto, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }



        public DTOREPOwner_TotalSchedule REPOwner_TotalSchedule(DateTime dtfrom, DateTime dtto, List<int?> lstVendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_TotalSchedule(dtfrom, dtto, lstVendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public DTOResult REPOwner_Schedule()
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_Schedule();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        //vai trò tài xế
        public List<DTOREPFLMVehiclePlan> REPFLMDriverRole_PlanData(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPFLMDriverRole_PlanData(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOREPFLMVehicleActual> REPFLMDriverRole_ActualData(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPFLMDriverRole_ActualData(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        // Phiếu
        public DTOREPOwner_Receipt REPOwner_Receipt(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_Receipt(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOREPOwner_ReceiptMaterial> REPOwner_Receipt_Detail(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_Receipt_Detail(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOREPOwner_Repair> REPOwner_Repair(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_Repair(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        // Thiết bị lịch sử
        public List<DTOREPOwner_Equipment> REPOwner_Equipment(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_Equipment(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        // chi tiết phân lệnh và từ chối lệnh (REPOwnerDriverRole)
        public List<DTOREPOwnerDriverRole> REPOwner_DriverRole(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_DriverRole(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        // Thiết bị
        public List<DTOREPOwner_Asset> REPOwner_Asset(int typeOfAssetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_Asset(typeOfAssetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        // Định mức xe
        public DTOREPOwner_Quota REPOwner_AssetQuota(int typeOfAssetID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_AssetQuota(typeOfAssetID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        // Định  mức xe cont
        public List<DTOREPOwner_Vehicle_Container> REPOwner_VehicleFee_Cost_Container(DateTime dtfrom, DateTime dtto, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_VehicleFee_Cost_Container(dtfrom, dtto, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        // Định mức xăng dầu hàng tháng
        public List<DTOREPOwner_MaterialQuota> REPOwner_MaterialQuota(DateTime dtfrom, DateTime dtto, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_MaterialQuota(dtfrom, dtto, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        #region REPOwnerMaintenance
        public List<DTOREPOwnerMaintenance> REPOwner_Maintenance_Detail(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_Maintenance_Detail(dtfrom, dtto);
                }
            }
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

        #region CUSSettingsReport
        public List<CUSSettingsReport> CUSSettingsReport_List(int referid)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingsReport_List(referid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CUSSettingsReport_Save(CUSSettingsReport item)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    bl.CUSSettingsReport_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CUSSettingsReport_Delete(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    bl.CUSSettingsReport_Delete(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingReport_CustomerNotInList(List<CUSSettingsReport_Customer> lstCus, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_CustomerNotInList(lstCus, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingReport_CustomerNotInSave(CUSSettingsReport item, List<int> lstCusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_CustomerNotInSave(item, lstCusId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingReport_CustomerDeleteList(CUSSettingsReport item, List<int> lstCusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_CustomerDeleteList(item, lstCusId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingReport_GroupOfProductNotInList(List<CUSSettingsReport_Customer> lstCus, List<CUSSettingsReport_GroupProduct> lstGOP, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_GroupOfProductNotInList(lstCus, lstGOP, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingReport_GroupOfProductNotInSave(CUSSettingsReport item, List<int> lstGOPId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_GroupOfProductNotInSave(item, lstGOPId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingReport_GroupOfProductDeleteList(CUSSettingsReport item, List<int> lstGOPId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_GroupOfProductDeleteList(item, lstGOPId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingReport_StockNotInList(List<CUSSettingsReport_Customer> lstCus, List<CUSSettingsReport_Stock> lstStock, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_StockNotInList(lstCus, lstStock, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingReport_StockNotInSave(CUSSettingsReport item, List<int> lstStockId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_StockNotInSave(item, lstStockId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingReport_StockDeleteList(CUSSettingsReport item, List<int> lstStockId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_StockDeleteList(item, lstStockId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingReport_ProvinceNotInList(List<CUSSettingsReport_Province> lstProvince, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_ProvinceNotInList(lstProvince, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_ProvinceNotInSave(CUSSettingsReport item, List<int> lstProvinceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_ProvinceNotInSave(item, lstProvinceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingReport_ProvinceDeleteList(CUSSettingsReport item, List<int> lstProvinceID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_ProvinceDeleteList(item, lstProvinceID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingReport_GroupOfLocationNotInList(List<CUSSettingsReport_GroupOfLocation> lstGroupOfLocation, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_GroupOfLocationNotInList(lstGroupOfLocation, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_GroupOfLocationNotInSave(CUSSettingsReport item, List<int> lstGroupOfLocationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_GroupOfLocationNotInSave(item, lstGroupOfLocationID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingReport_GroupOfLocationDeleteList(CUSSettingsReport item, List<int> lstGroupOfLocationID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_GroupOfLocationDeleteList(item, lstGroupOfLocationID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingReport_GroupOfPartnerNotInList(List<CUSSettingsReport_GroupOfPartner> lstGroupOfPartner, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_GroupOfPartnerNotInList(lstGroupOfPartner, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_GroupOfPartnerNotInSave(CUSSettingsReport item, List<int> lstGroupOfPartnerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_GroupOfPartnerNotInSave(item, lstGroupOfPartnerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_GroupOfPartnerDeleteList(CUSSettingsReport item, List<int> lstGroupOfPartnerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_GroupOfPartnerDeleteList(item, lstGroupOfPartnerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingReport_ServiceOfOrderNotInList(List<CUSSettingsReport_ServiceOfOrder> lstServiceOfOrder, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_ServiceOfOrderNotInList(lstServiceOfOrder, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_ServiceOfOrderNotInSave(CUSSettingsReport item, List<int> lstServiceOfOrderID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_ServiceOfOrderNotInSave(item, lstServiceOfOrderID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_ServiceOfOrderDeleteList(CUSSettingsReport item, List<int> lstServiceOfOrderID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_ServiceOfOrderDeleteList(item, lstServiceOfOrderID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingReport_OrderRoutingNotInList(List<CUSSettingsReport_Customer> lstCus, List<CUSSettingsReport_Routing> lstOrderRouting, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_OrderRoutingNotInList(lstCus, lstOrderRouting, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_OrderRoutingNotInSave(CUSSettingsReport item, List<int> lstOrderRoutingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_OrderRoutingNotInSave(item, lstOrderRoutingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_OrderRoutingDeleteList(CUSSettingsReport item, List<int> lstOrderRoutingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_OrderRoutingDeleteList(item, lstOrderRoutingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingReport_OPSRoutingNotInList(List<CUSSettingsReport_Customer> lstCus, List<CUSSettingsReport_Routing> lstOPSRouting, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_OPSRoutingNotInList(lstCus, lstOPSRouting, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_OPSRoutingNotInSave(CUSSettingsReport item, List<int> lstOPSRoutingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_OPSRoutingNotInSave(item, lstOPSRoutingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_OPSRoutingDeleteList(CUSSettingsReport item, List<int> lstOPSRoutingID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_OPSRoutingDeleteList(item, lstOPSRoutingID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingReport_PartnerNotInList(List<CUSSettingsReport_Customer> lstCus, List<CUSSettingsReport_Partner> lstPartner, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_PartnerNotInList(lstCus, lstPartner, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_PartnerNotInSave(CUSSettingsReport item, List<int> lstPartnerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_PartnerNotInSave(item, lstPartnerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public CUSSettingsReport CUSSettingReport_PartnerDeleteList(CUSSettingsReport item, List<int> lstPartnerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingReport_PartnerDeleteList(item, lstPartnerID);
                }
            }
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

        #region CUSSettingsPlan
        public List<CUSSettingsReport> CUSSettingsPlan_List(int referid)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingsPlan_List(referid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void CUSSettingsPlan_Save(CUSSettingsReport item)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    bl.CUSSettingsPlan_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void CUSSettingsPlan_Delete(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    bl.CUSSettingsPlan_Delete(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingPlan_CustomerNotInList(List<CUSSettingsReport_Customer> lstCus, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingPlan_CustomerNotInList(lstCus, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingPlan_CustomerNotInSave(CUSSettingsReport item, List<int> lstCusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingPlan_CustomerNotInSave(item, lstCusId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingPlan_CustomerDeleteList(CUSSettingsReport item, List<int> lstCusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingPlan_CustomerDeleteList(item, lstCusId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult CUSSettingPlan_GroupOfProductNotInList(List<CUSSettingsReport_Customer> lstCus, List<CUSSettingsReport_GroupProduct> lstGOP, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingPlan_GroupOfProductNotInList(lstCus, lstGOP, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingPlan_GroupOfProductNotInSave(CUSSettingsReport item, List<int> lstGOPId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingPlan_GroupOfProductNotInSave(item, lstGOPId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsReport CUSSettingPlan_GroupOfProductDeleteList(CUSSettingsReport item, List<int> lstGOPId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.CUSSettingPlan_GroupOfProductDeleteList(item, lstGOPId);
                }
            }
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

        #region telerik reporting

        #region rpDriverScheduleFee(luong tài xế)
        public List<DTOReportDriverSchedule> REPDriverScheduleFee_Data(List<int> lstDriverID, int month, int year)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDriverScheduleFee_Data(lstDriverID, month,year);
                }
            }
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

        #region rpWorkOrder_Fuel
        public List<DTOReportWorkOrderFuel> REPWorkOrderFuel_Data(List<int> lstReceiptID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPWorkOrderFuel_Data(lstReceiptID);
                }
            }
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

        #region rp ops sotrans
        public List<DTOReportOPSSotrans> REPOPSSotrans_Data(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOPSSotrans_Data(masterID);
                }
            }
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


        #region Báo cáo khấu hao
        public List<DTOREPOwner_FixedCost> REPOwner_FixedCost_Detail(DateTime dt)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_FixedCost_Detail(dt);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOREPOwner_FixedCost> REPOwner_FixedCost_Vehicle(DateTime dt)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_FixedCost_Vehicle(dt);
                }
            }
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

        #region Báo cáo khấu hao
        public List<DTOREPOwner_GPS> REPOwner_GPS_Detail(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPOwner_GPS_Detail(dtfrom, dtto);
                }
            }
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

        #region REPTotalPriceVendor
        public List<REPTotalPriceVendor> REPTotalPriceVendor_Data(int cusId, List<int> lstVenId, int transportModeID, int typePrice, DateTime effectDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPTotalPriceVendor_Data(cusId, lstVenId, transportModeID, typePrice, effectDate);
                }
            }
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

        #region Báo cáo tổng hợp
        public List<DTOREP_TotalPL> REPTotalPL_List(DateTime dtfrom, DateTime dtto, int typeOfView)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPTotalPL_List(dtfrom, dtto, typeOfView);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<int> REPTotalPriceVendor_ListVendor(int cusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPTotalPriceVendor_ListVendor(cusId);
                }
            }
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

        #region Báo cáo tờ khai (REPCOInspection)
        public List<DTOREPCOInspection> REPCOInspection_Detail(List<int> lstid, DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPCOInspection_Detail(lstid, dtfrom, dtto);
                }
            }
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

        #region Báo cáo trạng thái đơn hàng
        public List<DTOREPStatus_Order> REPDIStatus_OrderData(List<int> lstid, DateTime dtfrom, DateTime dtto, int typeOfFilter, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLReport>())
                {
                    return bl.REPDIStatus_OrderData(lstid, dtfrom, dtto, typeOfFilter, statusID);
                }
            }
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

