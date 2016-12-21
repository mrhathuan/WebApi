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
    public partial class SVFinance : SVBase, ISVFinance
    {
        public void Connect()
        {

        }

        #region Chạy lại PL
        public List<DTOCATContract> FINRefresh_Contract_List(int cusID, int? serID, int transID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_Contract_List(cusID, serID, transID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOContractTerm> FINRefresh_ContractTerm_List(int contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_ContractTerm_List(contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATContract> FINRefresh_Contract_Master_List(int vendorID, int? serID, int transID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_Contract_Master_List(vendorID, serID, transID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCUSRouting> FINRefresh_Routing_List(int cusID, int? contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_Routing_List(cusID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCUSRouting> FINRefresh_Routing_Master_List(int vendorID, int? contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_Routing_Master_List(vendorID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FINRefresh_ORD_Group_List(string request, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_ORD_Group_List(request, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINRefresh_ORD_Group_Save(FINRefresh_ORDGroup item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINRefresh_ORD_Group_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FINRefresh_OPS_Group_List(string request, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_OPS_Group_List(request, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINRefresh_OPS_Group_Save(FINRefresh_OPSGroup item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINRefresh_OPS_Group_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FINRefresh_OPS_Master_List(string request, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_OPS_Master_List(request, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINRefresh_OPS_Master_Save(FINRefresh_OPSMaster item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINRefresh_OPS_Master_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATRouting> FINRefresh_OPSRouting_List(int? routingID, int cusID, int? contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_OPSRouting_List(routingID, cusID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public DTOResult FINRefresh_ORD_Container_List(string request, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_ORD_Container_List(request, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINRefresh_ORD_Container_Save(FINRefresh_ORDContainer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINRefresh_ORD_Container_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FINRefresh_OPS_Container_List(string request, DateTime from, DateTime to)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_OPS_Container_List(request, from, to);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINRefresh_OPS_Container_Save(FINRefresh_OPSContainer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINRefresh_OPS_Container_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region Common

        public DTOResult Customer_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
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

        public DTOResult Vendor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
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

        public void FINFreightAuditCus_ImportReject(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                     bl.FINFreightAuditCus_ImportReject(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
         public void FINFreightAuditCus_ImportAccept(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                     bl.FINFreightAuditCus_ImportAccept(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region FINRefresh
        public FINRefresh FINRefresh_List(DateTime dtfrom, DateTime dtto)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_List(dtfrom, dtto);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public string FINRefresh_Refresh(DateTime date, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_Refresh(date, lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINRefresh_RefreshRoute_Order(DateTime dtfrom, DateTime dtto, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINRefresh_RefreshRoute_Order(dtfrom, dtto, lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINRefresh_RefreshRoute_TO(DateTime dtfrom, DateTime dtto, List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINRefresh_RefreshRoute_TO(dtfrom, dtto, lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCATRouting> FINRefresh_OPSGroupRouting_List(int? routingID, int venID, int? contractID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINRefresh_OPSGroupRouting_List(routingID, venID, contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region FIN FreightAudit
        #region Truct
        public DTOResult FINFreightAudit_List(DateTime pDateFrom, DateTime pDateTo, int statusID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAudit_List(pDateFrom, pDateTo, statusID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public FINFreightAudit_Detail FINFreightAudit_DetailList(int masterid)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAudit_DetailList(masterid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<SYSVar> FINFreightAudit_StatusList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAudit_StatusList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAudit_Reject(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAudit_Reject(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAudit_Accept(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAudit_Accept(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAudit_Waiting(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAudit_Waiting(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAudit_Approved(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAudit_Approved(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<FINFreightAudit_Export> FINFreightAudit_Export(DateTime pDateFrom, DateTime pDateTo, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAudit_Export(pDateFrom, pDateTo, statusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Container
        public List<SYSVar> FINFreightAuditCON_StatusList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCON_StatusList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FINFreightAuditCON_List(DateTime pDateFrom, DateTime pDateTo, int statusID, bool IsOwner, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCON_List(pDateFrom, pDateTo, statusID, IsOwner, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public FINFreightAuditCON_Detail FINFreightAudit_CONDetail_List(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAudit_CONDetail_List(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAudit_CONDetail_Save(FINFreightAuditCON_Detail item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAudit_CONDetail_Save(item);
                }
            }
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
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCON_Reject(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCON_Accept(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCON_Accept(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCON_Waiting(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCON_Waiting(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCON_Approved(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCON_Approved(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
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

        #region FIN FreightAudit Cus
        #region Truct
        public List<SYSVar> FINFreightAuditCus_Order_StatusList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_Order_StatusList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<SYSVar> FINFreightAuditCus_Group_StatusList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_Group_StatusList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FINFreightAuditCus_Order_List(DateTime pDateFrom, DateTime pDateTo, int statusID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_Order_List(pDateFrom, pDateTo, statusID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public FINFreightAuditCus_Order_Detail FINFreightAuditCus_Order_DetailList(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_Order_DetailList(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public FINFreightAuditCus_Order_Detail FINFreightAuditCus_OrderDetail_List(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_OrderDetail_List(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCus_Order_Reject(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCus_Order_Reject(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCus_Order_Accept(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCus_Order_Accept(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCus_Order_Approved(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCus_Order_Approved(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult FINFreightAuditCus_Group_List(DateTime pDateFrom, DateTime pDateTo, int statusID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_Group_List(pDateFrom, pDateTo, statusID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public FINFreightAuditCus_Group_Detail FINFreightAuditCus_Group_DetailList(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_Group_DetailList(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCus_Group_Reject(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCus_Group_Reject(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCus_Group_Accept(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCus_Group_Accept(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCus_Group_Approved(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCus_Group_Approved(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<FINFreightAuditCus_Import> FINFreightAuditCus_ImportCheck(List<FINFreightAuditCus_Import> lstData, int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_ImportCheck(lstData, customerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public FINFreightAuditCus_ImportData FINFreightAuditCus_ImportData(int customerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_ImportData(customerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Container
        public List<SYSVar> FINFreightAuditCus_OrderCON_StatusList()
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_OrderCON_StatusList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult FINFreightAuditCus_OrderCON_List(DateTime pDateFrom, DateTime pDateTo, int statusID, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_OrderCON_List(pDateFrom, pDateTo, statusID, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public FINFreightAuditCus_OrderCON_Detail FINFreightAuditCus_OrderCONDetail_List(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAuditCus_OrderCONDetail_List(id);
                }
            }
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
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAudit_ContractTerm_List(contractID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCUSRouting> FINFreightAudit_Routing_List(int customerID, int contractID, int termID)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINFreightAudit_Routing_List(customerID, contractID, termID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCus_OrderCONDetail_Save(FINFreightAuditCus_OrderCON_Detail item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCus_OrderCONDetail_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FINFreightAuditCus_OrderCON_Reject(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCus_OrderCON_Reject(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCus_OrderCON_Accept(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCus_OrderCON_Accept(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINFreightAuditCus_OrderCON_Approved(List<int> lst, string Note)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINFreightAuditCus_OrderCON_Approved(lst, Note);
                }
            }
            catch (FaultException<DTOError> ex)
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

        #region FIN ManualFix
        public DTOResult FINManualFix_List(DateTime pDateFrom, DateTime pDateTo, string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINManualFix_List(pDateFrom, pDateTo, request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public int FINManualFix_Save(DTOFINManualFix item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINManualFix_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FINManualFix_Delete(DTOFINManualFix item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINManualFix_Delete(item);
                }
            }
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
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINManualFix_ChooseList(request, pDateFrom, pDateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FINManualFix_SaveList(List<DTOFINManualFix> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                     bl.FINManualFix_SaveList(lst);
                }
            }
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
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINSettingManual_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOFINSettingManual FINSettingManual_Get(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINSettingManual_Get(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FINSettingManual_Save(DTOFINSettingManual item, int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINSettingManual_Save(item, id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void FINSettingManual_Delete(DTOFINSettingManual item)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINSettingManual_Delete(item);
                }
            }
            catch (FaultException<DTOError> ex)
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
        public List<DTOFINManualFixImport> FINManualFix_ExcelData(DateTime dtFrom, DateTime dtTO, int cusId)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    return bl.FINManualFix_ExcelData(dtFrom, dtTO, cusId);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void FINManualFix_ExcelImport(int sID, List<DTOFINManualFixImport> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLFinance>())
                {
                    bl.FINManualFix_ExcelImport(sID, data);
                }
            }
            catch (FaultException<DTOError> ex)
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

