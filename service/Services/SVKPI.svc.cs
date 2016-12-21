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
    public partial class SVKPI : SVBase, ISVKPI
    {
        public string Connect()
        {
            try
            {
                return "";
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region Common

        public DTOResult Customer_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
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
                using (var bl = CreateBusiness<BLKPI>())
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

        #region KPITime

        public DTOResult KPITime_List(string request, int kpiID, int cusID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPITime_List(request, kpiID, cusID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void KPITime_Save(KPIKPITime item)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPITime_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPITime_Generate(DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPITime_Generate(from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIReason> KPITimeReason_List(int kpiID)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPITimeReason_List(kpiID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIKPITime> KPITime_Excel(int kpiID, int cusID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPITime_Excel(kpiID, cusID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion        
        
        #region KPITimeDate

        public DTOResult KPITimeDate_List(string request, int kpiID, int cusID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPITimeDate_List(request, kpiID, cusID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void KPITimeDate_Save(KPITimeDate item)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPITimeDate_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPITimeDate_Generate(DateTime from, DateTime to, int cusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPITimeDate_Generate(from, to, cusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIReason> KPITimeDateReason_List(int kpiID)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPITimeDateReason_List(kpiID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPITimeDate> KPITimeDate_Excel(int kpiID, int cusID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPITimeDate_Excel(kpiID, cusID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOTypeOfKPI> KPITimeDate_GetTypeOfKPI()
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPITimeDate_GetTypeOfKPI();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion        
        
        #region KPIReason
        public List<KPIKPI> KPIKPI_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIKPI_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult KPIReason_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIReason_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int KPIReason_Save(KPIReason item)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIReason_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPIReason_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPIReason_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIReason> KPIReason_Export()
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIReason_Export();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPIReason_Import(List<KPIReason_Import> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPIReason_Import(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion   
        
        #region KPIDashBoard

        public List<KPIDashBoard_DN> Dashboard_DN_Data(int cusID, int kpiID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.Dashboard_DN_Data(cusID, kpiID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIDashBoard_Reason> Dashboard_Reason_Data(int cusID, int kpiID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.Dashboard_Reason_Data(cusID, kpiID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIDashBoard_DN> Dashboard_DN_VENData(int venID, int kpiID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.Dashboard_DN_VENData(venID, kpiID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIDashBoard_Reason> Dashboard_Reason_VENData(int venID, int kpiID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.Dashboard_Reason_VENData(venID, kpiID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region KPI Vendor
        public DTOResult KPIVENTime_List(string request, int kpiID, int venID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIVENTime_List(request, kpiID, venID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPIVENTime_Save(KPIVENTime item)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPIVENTime_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPIVENTime_Generate(DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPIVENTime_Generate(from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIVENTime> KPIVENTime_Excel(int kpiID, int venID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIVENTime_Excel(kpiID, venID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region KPI Vendor Tender
        public DTOResult KPIVENTenderFTL_List(string request, int venID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIVENTenderFTL_List(request, venID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPIVENTenderFTL_Save(KPIVENTenderFTL item)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPIVENTenderFTL_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPIVENTenderFTL_Generate(DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPIVENTenderFTL_Generate(from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIVENTenderFTL> KPIVENTenderFTL_Excel(int venID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIVENTenderFTL_Excel(venID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIDashBoard_DN> KPIVENTenderFTL_Dashboard(int venID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIVENTenderFTL_Dashboard(venID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIDashBoard_Reason> KPIVENTenderFTL_Dashboard_Reason(int venID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIVENTenderFTL_Dashboard_Reason(venID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }



        public DTOResult KPIVENTenderLTL_List(string request, int venID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIVENTenderLTL_List(request, venID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPIVENTenderLTL_Save(KPIVENTenderLTL item)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPIVENTenderLTL_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPIVENTenderLTL_Generate(DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPIVENTenderLTL_Generate(from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIVENTenderLTL> KPIVENTenderLTL_Excel(int venID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIVENTenderLTL_Excel(venID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIDashBoard_DN> KPIVENTenderLTL_Dashboard(int venID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIVENTenderLTL_Dashboard(venID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIDashBoard_Reason> KPIVENTenderLTL_Dashboard_Reason(int venID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIVENTenderLTL_Dashboard_Reason(venID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region KPI KPI
        public DTOResult KPIKPI_GetList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIKPI_GetList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public KPIKPI KPIKPI_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIKPI_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int KPIKPI_Save(KPIKPI item)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIKPI_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPIKPI_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPIKPI_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIKPI> KPIKPI_AllList()
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIKPI_AllList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult KPIColumn_List(string request, int KPIID)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIColumn_List(request, KPIID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public KPIColumn KPIColumn_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIColumn_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int KPIColumn_Save(KPIColumn item, int KPIID)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIColumn_Save(item, KPIID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPIColumn_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPIColumn_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<KPIField> KPIField_List(int typeID)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPIField_List(typeID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region KPI Collection
        public DTOResult KPICollection_GetList(string request, int KPIID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPICollection_GetList(request, KPIID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPICollection_Generate(int KPIID, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPICollection_Generate(KPIID, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region TypeOfKPI
        public DTOResult KPITypeOfKPI_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPITypeOfKPI_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOTypeOfKPI KPITypeOfKPI_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    return bl.KPITypeOfKPI_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPITypeOfKPI_Save(DTOTypeOfKPI item)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPITypeOfKPI_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void KPITypeOfKPI_Delete(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLKPI>())
                {
                    bl.KPITypeOfKPI_Delete(id);
                }
            }
            catch (FaultException<DTOError> ex)
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

