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
    public partial class SVOperation : SVBase, ISVOperation
    {
        #region Common

        public DTOResult Vendor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
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

        #region Combobox
        public DTOResult TractorByVendorID_List(int? vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.TractorByVendorID_List(vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult RomoocByVendorID_List(int? vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.RomoocByVendorID_List(vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult TruckByVendorID_List(int? vendorid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.TruckByVendorID_List(vendorid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult RejectReason_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.RejectReason_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult TenderCustomer_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.TenderCustomer_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Tự động cập nhật lệnh gửi Vendor sau mỗi 5p
        public void OPSMasterTendered_AutoSendMail()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSMasterTendered_AutoSendMail();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Appointment_Route

        #region Common
        public List<CUSCustomer> Appointment_Route_ListVendor()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.Appointment_Route_ListVendor();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<FLMDriver> Appointment_Route_ListDriver()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.Appointment_Route_ListDriver();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CUSCustomer> Appointment_Route_ListCustomer()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.Appointment_Route_ListCustomer();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOAppointmentRouteActivity Appointment_Route_ActivityGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.Appointment_Route_ActivityGet(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Appointment_Route_ActivitySave(DTOAppointmentRouteActivity item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.Appointment_Route_ActivitySave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Appointment_Route_ActivityDel(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.Appointment_Route_ActivityDel(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<FLMMaterial> Appointment_Route_MaterialList()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.Appointment_Route_MaterialList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOAppointmentRouteMaterialQuota> Appointment_Route_MaterialDetail(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.Appointment_Route_MaterialDetail(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Appointment_Route_MaterialSave(List<DTOAppointmentRouteMaterialQuota> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.Appointment_Route_MaterialSave(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public List<DTOOPS_FLMPlaning> Appointment_Route_FLMPlaning()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.Appointment_Route_FLMPlaning();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        #endregion

        #region DIAppointment

        public DTOResult DIAppointment_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Cancel(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Cancel(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region DIAppointment_Route
        public List<DTODIAppointmentOrder> DIAppointment_Route_OrderList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_OrderList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentGroupProduct> DIAppointment_Route_OrderDetail(List<DTODIAppointmentOrder> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_OrderDetail(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_OrderDiv(DTODIAppointmentOrder item, int div)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_OrderDiv(item, div);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_OrderGroup(List<DTODIAppointmentOrder> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_OrderGroup(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentGroupProduct> DIAppointment_Route_OrderDivCustomGet(DTODIAppointmentOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_OrderDivCustomGet(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_OrderDivCustomSave(List<DTODIAppointmentGroupProduct> lstMain, List<DTODIAppointmentGroupProduct> lstSub)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_OrderDivCustomSave(lstMain, lstSub);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_OrderDNCodeChange(DTODIAppointmentOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_OrderDNCodeChange(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTOFLMVehicle> DIAppointment_Route_VehicleList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMAssetTimeSheet> DIAppointment_Route_VehicleTimeList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleTimeList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSDIShiptmentRoute DIAppointment_Route_VehicleDetail(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleDetail(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_VehicleAdd(DTODIAppointmentOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_VehicleAdd(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentGroupProduct> DIAppointment_Route_VehicleGet(DTODIAppointmentOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleGet(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTODIAppointmentOrder DIAppointment_Route_VehicleTimeGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleTimeGet(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_VehicleRemove(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_VehicleRemove(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_VehicleSave(DTODIAppointmentOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_VehicleSave(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_VehicleMonitor(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_VehicleMonitor(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public string DIAppointment_Route_VehicleRemoveMonitor(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleRemoveMonitor(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTODIAppointmentOrder DIAppointment_Route_VehicleTimelineChange(DTOFLMAssetTimeSheet source, DTODIAppointmentOrder target)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleTimelineChange(source, target);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<CUSCustomer> DIAppointment_Route_VehicleListVendor()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.Appointment_Route_ListVendor();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSVehicle> DIAppointment_Route_VehicleListVehicle()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleListVehicle();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATGroupOfVehicle> DIAppointment_Route_VehicleListGroupVehicle()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleListGroupVehicle();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_VehicleAddRate(DTODIAppointmentOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_VehicleAddRate(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOFLMAssetTimeSheet> DIAppointment_Route_VehicleTOVEN(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleTOVEN(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentVENVehicle> DIAppointment_Route_VehicleTOVENList()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleTOVENList();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentOrder> DIAppointment_Route_VehicleTOVENInDate(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleTOVENInDate(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTODIAppointmentOrder DIAppointment_Route_VehicleTOVENGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_VehicleTOVENGet(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_VehicleSend(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_VehicleSend(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_WinVehicleSave(List<DTOFLMVehicle> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_WinVehicleSave(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentPOD> DIAppointment_Route_PODList(DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_PODList(fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_PODExcelSave(List<DTODIAppointmentPOD> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_PODExcelSave(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }



        public void DIAppointment_Route_PODDiv(List<DTODIAppointmentPOD> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_PODDiv(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentPOD> DIAppointment_Route_QuickSearch(DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_QuickSearch(fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTODIAppointmentOrder DIAppointment_Route_QuickSearchGet(int id)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_QuickSearchGet(id);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_QuickSearchApproved(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_QuickSearchApproved(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_QuickSearchUnApproved(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_QuickSearchUnApproved(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTODIAppointmentRate> DIAppointment_Route_HasDNList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_HasDNList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentOrder> DIAppointment_Route_HasDNOrderList(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_HasDNOrderList(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentOrder> DIAppointment_Route_HasDNOrderListDN()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_HasDNOrderListDN();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentRate> DIAppointment_Route_HasDNListGroupID(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_HasDNListGroupID(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentOrder> DIAppointment_Route_HasDNOrderListByGroupID(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_HasDNOrderListByGroupID(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_HasDNSave(List<DTODIAppointmentOrder> lstOrder, List<DTODIAppointmentRate> lstVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_HasDNSave(lstOrder, lstVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_HasDNDelete(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_HasDNDelete(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_HasDNApproved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_HasDNApproved(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_HasDNUnApproved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_HasDNUnApproved(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }



        public List<DTODIAppointmentRate> DIAppointment_Route_NoDNList(string request, List<int> dataCusID, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_NoDNList(request, dataCusID, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentOrder> DIAppointment_Route_NoDNOrderList(string request, List<int> dataCusID, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_NoDNOrderList(request, dataCusID, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_NoDNSave(List<DTODIAppointmentOrder> lstOrder, List<DTODIAppointmentRate> lstVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_NoDNSave(lstOrder, lstVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public List<DTODIAppointmentRate> DIAppointment_Route_FTL_NoDNList(string request, List<int> dataCusID, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_FTL_NoDNList(request, dataCusID, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentOrder> DIAppointment_Route_FTL_NoDNOrderList(string request, List<int> dataCusID, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_FTL_NoDNOrderList(request, dataCusID, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_FTL_NoDNSave(List<DTODIAppointmentRate> lstVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_FTL_NoDNSave(lstVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_FTL_NoDNCancel(List<DTODIAppointmentRate> lstVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_FTL_NoDNCancel(lstVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_FTL_NoDNSplit(int toMasterID, List<DTODIAppointmentOrder> dataGop, List<DTODIAppointmentRate> dataVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_FTL_NoDNSplit(toMasterID, dataGop, dataVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult DIAppointment_RouteDN_OrderList(string request, DateTime DateFrom, DateTime DateTo, List<int> dataCusID, int statusID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_RouteDN_OrderList(request, DateFrom, DateTo, dataCusID, statusID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_RouteDN_OrderDNCodeChange(DTODIAppointmentOrder item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_RouteDN_OrderDNCodeChange(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_RouteDN_Delete(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_RouteDN_Delete(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_RouteDN_Revert(List<int> lstid)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_RouteDN_Revert(lstid);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOORDImportABA> DIAppointment_Route_ExcelConfirm(DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_ExcelConfirm(fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMailVendor> DIAppointment_Route_SendToTender(List<int> lst, List<DTODIAppointmentRouteTender> lstTender, double RateTime)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_SendToTender(lst, lstTender, RateTime);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMailVendor> DIAppointment_Route_TenderReject(List<int> lst, DTODIAppointmentRouteTenderReject item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_TenderReject(lst, item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_TenderApproved(List<int> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_TenderApproved(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_TenderSave(List<DTODIAppointmentRate> lstVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_TenderSave(lstVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentRate> DIAppointment_Route_TenderRequestList(string request, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_TenderRequestList(request, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentRate> DIAppointment_Route_TenderAcceptList(string request, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_TenderAcceptList(request, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentRate> DIAppointment_Route_TenderRejectList(string request, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_TenderRejectList(request, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentOrder> DIAppointment_Route_TenderRequestOrderList(string request, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_TenderRequestOrderList(request, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentOrder> DIAppointment_Route_TenderAcceptOrderList(string request, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_TenderAcceptOrderList(request, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentOrder> DIAppointment_Route_TenderRejectOrderList(string request, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_TenderRejectOrderList(request, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_SendMailToTender(List<DTOMailVendor> lst)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_SendMailToTender(lst);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTODIAppointmentRate> DIAppointment_Route_MasterList(string request, List<int> dataCusID, DateTime DateFrom, DateTime DateTo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_MasterList(request, dataCusID, DateFrom, DateTo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTODIAppointmentRate DIAppointment_Route_Master_OfferPL(DTODIAppointmentRate master, List<DTODIAppointmentOrder> lstOrder)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_Master_OfferPL(master, lstOrder);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_Route_Master_ChangeMode(List<int> data, bool fromFTL)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_Route_Master_ChangeMode(data, fromFTL);
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

        public bool DIAppointment_Route_Master_CheckDriver(int vehicleID, int driverID, DateTime? etd, DateTime? eta)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_Master_CheckDriver(vehicleID, driverID, etd, eta);
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

        public List<DTOOPSBookingConfirm> DIAppointment_Route_BookingConfirmation(List<int> lstCustomerID, DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_BookingConfirmation(lstCustomerID, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult DIAppointment_Route_BookingConfirmation_Read(string request, int customerID, DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_Route_BookingConfirmation_Read(request, customerID, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region COAppointment

        public DTOResult COAppointment_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.COAppointment_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void COAppointment_Cancel(List<int> lstID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.COAppointment_Cancel(lstID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region COAppointment_Route

        #region 2View

        public List<DTOOPSCOTOContainer> COAppointment_2View_Container_List(string request, List<int> dataCusID, DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.COAppointment_2View_Container_List(request, dataCusID, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCOTOContainer> COAppointment_2View_ContainerHasMaster_List(string request, List<int> dataCusID, DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.COAppointment_2View_ContainerHasMaster_List(request, dataCusID, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCOTOMaster> COAppointment_2View_Master_List(string request, List<int> dataCusID, DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.COAppointment_2View_Master_List(request, dataCusID, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCOTOMaster COAppointment_2View_Master_PL(DTOOPSCOTOMaster objMaster, List<DTOOPSCOTOContainer> dataDetail)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.COAppointment_2View_Master_PL(objMaster, dataDetail);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void COAppointment_2View_Master_Update(List<DTOOPSCOTOMaster> dataMaster, List<DTOOPSCOTOContainer> dataContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.COAppointment_2View_Master_Update(dataMaster, dataContainer);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void COAppointment_2View_Master_Delete(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.COAppointment_2View_Master_Delete(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void COAppointment_2View_Master_ToMON(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.COAppointment_2View_Master_ToMON(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void COAppointment_2View_Master_ToOPS(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.COAppointment_2View_Master_ToOPS(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOMailVendor> COAppointment_2View_Master_ToVendor(List<int> dataMaster, List<DTODIAppointmentRouteTender> dataRate, double rTime)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.COAppointment_2View_Master_ToVendor(dataMaster, dataRate, rTime);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void COAppointment_2View_Master_ToVendor_Email(List<DTOMailVendor> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.COAppointment_2View_Master_ToVendor_Email(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool COAppointment_2View_Master_CheckDriver(int vehicleID, int driverID, DateTime? etd, DateTime? eta)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.COAppointment_2View_Master_CheckDriver(vehicleID, driverID, etd, eta);
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

        #endregion

        #endregion

        #region Tendering

        public DTOResult OPS_Tendering_Vendor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_Tendering_Vendor_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region COTendering
        public DTOResult OPS_CO_Tendering_Setting_Service_List(string request, List<int> dataExist)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Tendering_Setting_Service_List(request, dataExist);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult OPS_CO_Tendering_Setting_Routing_List(string request, List<int> dataExist)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Tendering_Setting_Routing_List(request, dataExist);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult OPS_CO_Tendering_Setting_Customer_List(string request, List<int> dataExist)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Tendering_Setting_Customer_List(request, dataExist);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public DTOResult OPS_CO_Tendering_Setting_Vendor_List(string request, List<int> dataExist)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Tendering_Setting_Customer_List(request, dataExist);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        public void OPS_CO_Tendering_Setting_FCL_Packet_Save(int fID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Tendering_Setting_FCL_Packet_Save(fID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CUSSettingsTenderFCL> OPS_CO_Tendering_Setting_FCL_List(int fID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Tendering_Setting_FCL_List(fID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsTenderFCL OPS_CO_Tendering_Setting_FCL_Get(int sID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Tendering_Setting_FCL_Get(sID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_CO_Tendering_Setting_FCL_Save(CUSSettingsTenderFCL item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Tendering_Setting_FCL_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public void OPS_CO_Tendering_Setting_FCL_Delete(int sID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Tendering_Setting_FCL_Delete(sID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region DITendering

        public DTOResult OPS_DI_Tendering_Packet_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Packet_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Tendering_PacketGroupProduct_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_PacketGroupProduct_List(request, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Tendering_PacketGroupProduct_NotIn_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_PacketGroupProduct_NotIn_List(request, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Tendering_PacketRate_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_PacketRate_List(request, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Tendering_PacketDetail_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_PacketDetail_List(request, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSDIPacket OPS_DI_Tendering_Packet_Get(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Packet_Get(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CUSSettingsTenderLTL> OPS_DI_Tendering_Setting_List(int fID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Setting_List(fID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public CUSSettingsTenderLTL OPS_DI_Tendering_Setting_Get(int sID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Setting_Get(sID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CUSSettingsTenderLTL> OPS_DI_Tendering_Setting_Packet_List(int fID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Setting_Packet_List(fID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        public DTOResult OPS_DI_Tendering_Setting_Location_List(string request, List<int> dataExist)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Setting_Location_List(request, dataExist);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Tendering_Setting_Routing_List(string request, List<int> dataExist)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Setting_Routing_List(request, dataExist);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Tendering_Setting_GroupLocation_List(string request, List<int> dataExist)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Setting_GroupLocation_List(request, dataExist);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Tendering_Setting_Customer_List(string request, List<int> dataExist)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Setting_Customer_List(request, dataExist);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Tendering_Setting_Vendor_List(string request, List<int> dataExist)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Setting_Vendor_List(request, dataExist);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public DTOResult OPS_DI_Tendering_Setting_Packet_Order_List(string request, int sID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Tendering_Setting_Packet_Order_List(request, sID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Tendering_Packet_Save(DTOOPSDIPacket item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_Packet_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public void OPS_DI_Tendering_Packet_CreateViaSetting(int sID, string name, List<int> dataGop)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_Packet_CreateViaSetting(sID, name, dataGop);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Tendering_PacketGroupProduct_Save(int pID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_PacketGroupProduct_Save(pID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Tendering_PacketGroupProduct_Remove(int pID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_PacketGroupProduct_Remove(pID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Tendering_PacketRate_Save(int pID, DTOOPSDIPacketRate item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_PacketRate_Save(pID, item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Tendering_PacketRate_Remove(int pID, DTOOPSDIPacketRate item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_PacketRate_Remove(pID, item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Tendering_Packet_Send(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_Packet_Send(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Tendering_Packet_Delete(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_Packet_Delete(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Tendering_Setting_Save(CUSSettingsTenderLTL item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_Setting_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Tendering_Setting_Delete(int sID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_Setting_Delete(sID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Tendering_Setting_Packet_Save(int fID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Tendering_Setting_Packet_Save(fID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #region VendorView

        public DTOResult OPS_DI_VEN_Tendering_Rate_List(string request, bool? isAccept)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_VEN_Tendering_Rate_List(request, isAccept);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_VEN_Tendering_GroupProduct_List(string request, int packetDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_VEN_Tendering_GroupProduct_List(request, packetDetailID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_VEN_Tendering_Rate_Reject(DTOOPSDIPacketDetailRate item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_VEN_Tendering_Rate_Reject(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_VEN_Tendering_Rate_Accept(DTOOPSDIPacketDetailRate item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_VEN_Tendering_Rate_Accept(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_VEN_Tendering_Rate_AcceptPart(int packetDetailRateID, List<int> dataGop)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_VEN_Tendering_Rate_AcceptPart(packetDetailRateID, dataGop);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region Vendor2View

        public DTOResult OPS_DI_VEN_2View_GroupProduct_List(string request, int packetDetailID, bool hasMaster)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_VEN_2View_GroupProduct_List(request, packetDetailID, hasMaster);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_VEN_2View_TOMaster_List(string request, int packetDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_VEN_2View_TOMaster_List(request, packetDetailID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSDIPacketDetailRate OPS_DI_VEN_2View_Get(int packetDetailRateID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_VEN_2View_Get(packetDetailRateID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_VEN_2View_TOMaster_Save(int packetDetailID, List<DTOOPSDIPacketTOMaster> dataMaster, List<DTOOPSDIPacketTOGroupProduct> dataGop)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_VEN_2View_TOMaster_Save(packetDetailID, dataMaster, dataGop);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_VEN_2View_TOMaster_Delete(int packetDetailID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_VEN_2View_TOMaster_Delete(packetDetailID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_VEN_Tendering_Packet_Confirm(int packetDetailID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_VEN_Tendering_Packet_Confirm(packetDetailID);
                }
            }
            catch (FaultException<DTOError> ex)
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

        #region Barcode
        public DTOBarcode DIAppointment_RouteBarcode_SOList(string Barcode)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.DIAppointment_RouteBarcode_SOList(Barcode);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void DIAppointment_RouteBarcode_SOSave(List<DTOBarcodeGroup> lst, bool IsNote)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.DIAppointment_RouteBarcode_SOSave(lst, IsNote);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region Optimizer

        public DTOResult Opt_Optimizer_List(string request, bool isCo)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Optimizer_List(request, isCo);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Container_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Container_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Container_NotIn_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Container_NotIn_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_GroupOfProduct_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_GroupOfProduct_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_GroupOfProduct_NotIn_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_GroupOfProduct_NotIn_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Vehicle_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Vehicle_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Vehicle_NotIn_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Vehicle_NotIn_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Romooc_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Romooc_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Romooc_NotIn_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Romooc_NotIn_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Location_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Location_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Location_Require_List(int optLocationID, bool isSize)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Location_Require_List(optLocationID, isSize);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Routing_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Routing_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Routing_Require_List(int optRoutingID, bool isSize)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Routing_Require_List(optRoutingID, isSize);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_LocationMatrix_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_LocationMatrix_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_COTOMaster_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_COTOMaster_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_COTOMaster_Container_List(string request, int optMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_COTOMaster_Container_List(request, optMasterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_COTOContainer_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_COTOContainer_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_COTOLocation_List(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_COTOLocation_List(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_Optimizer_VehicleSchedule(string request, int optimizerID)
        {

            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Optimizer_VehicleSchedule(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_DITOMaster_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_DITOMaster_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_DITOMaster_GroupProduct_List(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_DITOMaster_GroupProduct_List(request, masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_DITOGroupProduct_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_DITOGroupProduct_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_DITOLocation_List(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_DITOLocation_List(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        //2View
        public DTOResult Opt_2ViewCO_Master_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_2ViewCO_Master_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_2ViewCO_Container_List(string request, int optimizerID, bool? hasMaster)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_2ViewCO_Container_List(request, optimizerID, hasMaster);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_2ViewCO_SaveList(int optimizerID, List<DTOOPTCOTOMaster> dataMaster, List<DTOOPTCOTOContainer> dataContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_2ViewCO_SaveList(optimizerID, dataMaster, dataContainer);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_2ViewCO_Delete(int optimizerID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_2ViewCO_Delete(optimizerID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_2ViewDI_Master_List(string request, int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_2ViewDI_Master_List(request, optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult Opt_2ViewDI_GroupProduct_List(string request, int optimizerID, bool? hasMaster)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_2ViewDI_GroupProduct_List(request, optimizerID, hasMaster);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_2ViewDI_SaveList(int optimizerID, List<DTOOPTDITOMaster> dataMaster, List<DTOOPTDITOGroupOfProduct> dataGroupProduct)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_2ViewDI_SaveList(optimizerID, dataMaster, dataGroupProduct);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_2ViewDI_Delete(int optimizerID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_2ViewDI_Delete(optimizerID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public string Opt_Optimizer_Run_Check_Setting()
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Optimizer_Run_Check_Setting();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public string Opt_Optimizer_Run_Check_Vehicle(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Optimizer_Run_Check_Vehicle(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public string Opt_Optimizer_Run_Check_Location(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Optimizer_Run_Check_Location(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPTLocationToLocation> Opt_Optimizer_Run_Get_LocationMatrix(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Optimizer_Run_Get_LocationMatrix(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Optimizer_Run_Update_LocationMatrix(DTOOPTLocationToLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Optimizer_Run_Update_LocationMatrix(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Optimizer_Run(int optimizerID, int typeOfOptimizer)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Optimizer_Run(optimizerID, typeOfOptimizer);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Optimizer_Cal(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Optimizer_Cal(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Optimizer_Out(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Optimizer_Out(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPTOptimizer Opt_Optimizer_Get(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Optimizer_Get(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Optimizer_Save(DTOOPTOptimizer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Optimizer_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Optimizer_Delete(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Optimizer_Delete(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Vehicle_SaveList(int optimizerID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Vehicle_SaveList(optimizerID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Romooc_SaveList(int optimizerID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Romooc_SaveList(optimizerID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Vehicle_Update(int optVehicleID, int? romoocID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Vehicle_Update(optVehicleID, romoocID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Vehicle_UpdateWeight(int optVehicleID, double maxWeight)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Vehicle_UpdateWeight(optVehicleID, maxWeight);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Container_SaveList(int optimizerID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Container_SaveList(optimizerID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Container_Remove(int optimizerID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Container_Remove(optimizerID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Container_Update(DTOOPSCOTOContainer item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Container_Update(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_GroupOfProduct_SaveList(int optimizerID, List<int> dataGop)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_GroupOfProduct_SaveList(optimizerID, dataGop);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_GroupOfProduct_Remove(int optimizerID, List<int> dataGop)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_GroupOfProduct_Remove(optimizerID, dataGop);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_GroupOfProduct_Update(DTOOPTOPSGroupProduct item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_GroupOfProduct_Update(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Romooc_Remove(int optimizerID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Romooc_Remove(optimizerID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Vehicle_Remove(int optimizerID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Vehicle_Remove(optimizerID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Routing_Save(int optRoutingID, double time, double distance)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Routing_Save(optRoutingID, time, distance);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Location_Require_Save(int optLocationID, DTOOPTLocationRequire item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Location_Require_Save(optLocationID, item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Location_Require_Reset(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Location_Require_Reset(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Routing_Require_Save(int optRoutingID, DTOOPTLocationRequire item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Routing_Require_Save(optRoutingID, item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Routing_Require_Reset(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Routing_Require_Reset(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public void Opt_Location_Require_Remove(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Location_Require_Remove(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_Routing_Require_Remove(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_Routing_Require_Remove(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_LocationMatrix_Refresh(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_LocationMatrix_Refresh(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_LocationMatrix_Update(DTOOPTLocationToLocation item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_LocationMatrix_Update(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_COTOMaster_Change(DTOOPTCOTOMaster item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_COTOMaster_Change(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_COTOMaster_Delete(int optMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_COTOMaster_Delete(optMasterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_DITOMaster_Change(DTOOPTDITOMaster item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_DITOMaster_Change(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_DITOMaster_Delete(int optMasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_DITOMaster_Delete(optMasterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public void Opt_COTOMaster_Save(int optimizerID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_COTOMaster_Save(optimizerID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void Opt_DITOMaster_Save(int optimizerID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    bl.Opt_DITOMaster_Save(optimizerID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPTJsonData Opt_Optimizer_GetDataRun(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Optimizer_GetDataRun(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool Opt_Optmizer_HasRun(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Optmizer_HasRun(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool Opt_Optmizer_HasSave(int optimizerID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOptimize>())
                {
                    return bl.Opt_Optmizer_HasSave(optimizerID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region NewCO - Map

        public DTOResult OPSCO_MAP_Order_List(string request, int typeOfOrder, DateTime? fDate, DateTime? tDate, List<int> dataCus, List<int> dataService, List<int> dataCarrier, List<int> dataSeaport)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Order_List(request, typeOfOrder, fDate, tDate, dataCus, dataService, dataCarrier, dataSeaport);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_MAP_Tractor_List(string request, DateTime now)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Tractor_List(request, now);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_MAP_Romooc_List(string request, DateTime now)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Romooc_List(request, now);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_MAP_VehicleVendor_List(string request, int vendorID, int typeofvehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_VehicleVendor_List(request, vendorID, typeofvehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_MAP_TOMaster_List(string request, bool isApproved, bool isTendered, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_TOMaster_List(request, isApproved, isTendered, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_MAP_Location_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Location_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CUSCustomer> OPSCO_MAP_Vendor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Vendor_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CUSCustomer> OPSCO_MAP_Customer_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Customer_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<CATServiceOfOrder> OPSCO_MAP_ServiceOfOrder_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_ServiceOfOrder_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCO_MAP_Seaport> OPSCO_MAP_Seaport_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Seaport_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCO_MAP_Carrier> OPSCO_MAP_Carrier_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Carrier_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<FLMDriver> OPSCO_MAP_Driver_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Driver_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<FLMDriver> OPSCO_MAP_DriverVendor_List(int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_DriverVendor_List(vendorID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Setting OPSCO_MAP_Setting()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Setting();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Trip OPSCO_MAP_TripByID(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_TripByID(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCO_MAP_Trip> OPSCO_MAP_TripByVehicle_List(DateTime now, int vehicleID, int romoocID, int total)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_TripByVehicle_List(now, vehicleID, romoocID, total);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_VehicleAvailable OPSCO_MAP_CheckVehicleAvailable(int masterID, int vehicleID, int romoocID, DateTime ETD, DateTime ETA, double Ton, List<int> dataORDCon, List<int> dataOPSCon, List<int> dataCon)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_CheckVehicleAvailable(masterID, vehicleID, romoocID, ETD, ETA, Ton, dataORDCon, dataOPSCon, dataCon);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Schedule_Data OPSCO_MAP_Schedule_Data(List<int> dataVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Schedule_Data(dataVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public void OPSCO_MAP_Save(DTOOPSCO_MAP_Trip item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_ToMON(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_ToMON(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public void OPSCO_MAP_ToOPS(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_ToOPS(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_Cancel(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_Cancel(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_UpdateAndToMON(int mID, string tDriverName, string tDriverTel)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_UpdateAndToMON(mID, tDriverName, tDriverTel);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_ToVendor(DTOOPSCO_MAP_Trip item, List<DTODIAppointmentRouteTender> dataRate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_ToVendor(item, dataRate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_ToVendorKPI(List<int> dataOPSCon, List<DTOOPSCO_MAP_Vendor_KPI> data, double rateTime)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_ToVendorKPI(dataOPSCon, data, rateTime);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_Delete(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_Delete(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_COTOContainer_Split(List<int> data, int hubID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_COTOContainer_Split(data, hubID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public void OPSCO_MAP_COTOContainer_Split_Cancel(int conID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_COTOContainer_Split_Cancel(conID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        //View xem chuyến mới.
        public DTOResult OPSCO_MAP_COTOContainer_List(string request, bool isApproved, bool isTendered, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_COTOContainer_List(request, isApproved, isTendered, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_MAP_COTOContainer_ByTrip_List(string request, int mID, int opsConID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_COTOContainer_ByTrip_List(request, mID, opsConID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_MAP_2View_Container_List(string request, List<int> data, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_2View_Container_List(request, data, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool OPSCO_MAP_2View_Master_Update_Check4Delete(int mID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_2View_Master_Update_Check4Delete(mID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_2View_Master_Update_TimeLine(int mID, int vendorID, int vehicleID, bool isTractor, DateTime ETD, DateTime ETA, List<DTOOPSCOTOContainer> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_2View_Master_Update_TimeLine(mID, vendorID, vehicleID, isTractor, ETD, ETA, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_2View_Master_Update_Container(int mID, List<int> dataCon, bool isRemove)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_2View_Master_Update_Container(mID, dataCon, isRemove);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_2View_Master_Update(DTOOPSCO_MAP_Trip item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_2View_Master_Update(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_2View_Master_ChangeVehicle(int mID, int vID, int type)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_2View_Master_ChangeVehicle(mID, vID, type);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_VehicleAvailable OPSCO_MAP_2View_Master_ChangeDriver(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_2View_Master_ChangeDriver(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_TimeLine_Update_Container(int mID, List<int> dataOPSCon, List<int> dataCon, bool isRemove)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_TimeLine_Update_Container(mID, dataOPSCon, dataCon, isRemove);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_TimeLine_Create_Master(DTOOPSCO_MAP_Trip item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_TimeLine_Create_Master(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Tractor OPSCO_MAP_TimeLine_Vehicle_Info(int venID, int vehID, int romID, DateTime now)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_TimeLine_Vehicle_Info(venID, vehID, romID, now);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_MAP_TimeLine_Master_Container_List(string request, int mID, int typeOfOrder)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_TimeLine_Master_Container_List(request, mID, typeOfOrder);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCO_MAP_Vendor_KPI> OPSCO_MAP_Vendor_KPI_List(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Vendor_KPI_List(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_Vendor_KPI_Save(int vendorID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_Vendor_KPI_Save(vendorID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCO_MAP_Vendor_KPI> OPSCO_MAP_Vendor_With_KPI_List(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Vendor_With_KPI_List(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Schedule_Data OPSCO_MAP_New_Schedule_Data(bool isShowVehicle, string strVehicle, int typeOfResource, List<int> dataCus, List<int> dataService, List<int> dataCarrier, List<int> dataSeaport, List<int> dataStatus)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_New_Schedule_Data(isShowVehicle, strVehicle, typeOfResource, dataCus, dataService, dataCarrier, dataSeaport, dataStatus);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_MAP_New_Schedule_COTOContainer_List(string request, int vendorID, DateTime fDate, DateTime tDate, List<int> dataCus, List<int> dataService, List<int> dataCarrier, List<int> dataSeaport, List<int> dataStatus)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_New_Schedule_COTOContainer_List(request, vendorID, fDate, tDate, dataCus, dataService, dataCarrier, dataSeaport, dataStatus);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_Vehicle_New(int vendorID, string regNo, double maxWeight, int typeofvehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_Vehicle_New(vendorID, regNo, maxWeight, typeofvehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_TimeLine_Create_Item(DTOOPSCO_MAP_Trip item, List<DTOOPSCOTOContainer> dataOffer)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_TimeLine_Create_Item(item, dataOffer);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Schedule_Data OPSCO_MAP_Info_Schedule_Data(int m1ID, int m2ID, int venID, int vehID, int romID, int typeOfResource, DateTime ETD, DateTime ETA, List<int> dataOPSCon, List<int> dataCon)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Info_Schedule_Data(m1ID, m2ID, venID, vehID, romID, typeOfResource, ETD, ETA, dataOPSCon, dataCon);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public string OPSCO_MAP_Info_Schedule_DragDrop_Save_Check(int mID, List<DTOOPSCO_Map_Schedule_Event> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Info_Schedule_DragDrop_Save_Check(mID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_Info_Schedule_DragDrop_Save(int mID, List<DTOOPSCO_Map_Schedule_Event> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_Info_Schedule_DragDrop_Save(mID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Schedule_Data OPSCO_MAP_Vehicle_Schedule_Data(int mID, int typeOfResource, int vehID, int romID, DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Vehicle_Schedule_Data(mID, typeOfResource, vehID, romID, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public string OPSCO_MAP_Schedule_Check(int vehicleID, int romoocID, List<int> dataContainer, List<int> dataOPSContainer)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Schedule_Check(vehicleID, romoocID, dataContainer, dataOPSContainer);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCOTOMaster OPSCO_MAP_Schedule_NewTime_Offer(DTOOPSCO_MAP_Trip item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Schedule_NewTime_Offer(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCOTOMaster OPSCO_MAP_Schedule_TOMaster_Vehicle_Offer(int mID, int vehID, int? venID, bool istractor)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Schedule_TOMaster_Vehicle_Offer(mID, vehID, venID, istractor);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public void OPSCO_MAP_Schedule_TOMaster_Change_Vehicle(int mID, int vehID, int? venID, bool istractor)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_Schedule_TOMaster_Change_Vehicle(mID, vehID, venID, istractor);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_Schedule_TOMaster_Change_Time(int mID, DateTime ETD, DateTime ETA, List<DTOOPSCOTOContainer> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_Schedule_TOMaster_Change_Time(mID, ETD, ETA, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCOTOMaster OPSCO_MAP_Schedule_LeadTime_Offer(List<int> dataOPSCon, List<int> dataCon)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Schedule_LeadTime_Offer(dataOPSCon, dataCon);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCOTOMaster OPSCO_MAP_Schedule_AddTOContainer_Offer(int mID, List<int> data, int typeOfData)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_MAP_Schedule_AddTOContainer_Offer(mID, data, typeOfData);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_MAP_Schedule_AddTOContainer(int mID, DateTime ETD, DateTime ETA, List<DTOOPSCO_Map_Schedule_Event> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_MAP_Schedule_AddTOContainer(mID, ETD, ETA, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }


        //TimeLine        
        public DTOResult OPSCO_TimeLine_Order_List(string request, int typeOfOrder, bool isOwnerPlanning, DateTime fDate, DateTime tDate, List<int> dataCus, List<int> dataService, List<int> dataCarrier, List<int> dataSeaport)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_Order_List(request, typeOfOrder, isOwnerPlanning, fDate, tDate, dataCus, dataService, dataCarrier, dataSeaport);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_TimeLine_Vehicle_List(string request, DateTime fDate, DateTime tDate, int typeOfView)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_Vehicle_List(request, fDate, tDate, typeOfView);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_TimeLine_RomoocWait_List(string request, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_RomoocWait_List(request, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Schedule_Data OPSCO_TimeLine_Schedule_Data(DateTime fDate, DateTime tDate, List<string> dataRes, int typeOfView)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_Schedule_Data(fDate, tDate, dataRes, typeOfView);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public DTOResult OPSCO_TimeLine_COTOContainer_ByTrip_List(string request, int mID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_COTOContainer_ByTrip_List(request, mID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_TimeLine_COTOContainer_Remove(int mID, int conID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_TimeLine_COTOContainer_Remove(mID, conID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCOTOMaster OPSCO_TimeLine_Event_ChangeTime_Offer(int mID, int conID, DateTime ETD, DateTime ETA)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_Event_ChangeTime_Offer(mID, conID, ETD, ETA);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Trip OPSCO_TimeLine_TOMaster_ByID(int mID, int conID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_TOMaster_ByID(mID, conID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Trip OPSCO_TimeLine_DataContainerLocal_ByTOMasterID(int mID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_DataContainerLocal_ByTOMasterID(mID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
         
        public DTOResult OPSCO_TimeLine_OrderToTOMaster_List(string request, int typeofserviceorder)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_OrderToTOMaster_List(request, typeofserviceorder);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_TimeLine_Order_OwnerPlanning_Update(List<int> data, bool value)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_TimeLine_Order_OwnerPlanning_Update(data, value);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCO_TimeLine_Vehicle> OPSCO_TimeLine_Vehicle_OnMap_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_Vehicle_OnMap_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSCO_TimeLine_OrderFilter_List(string request, DateTime fDate, DateTime tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_OrderFilter_List(request, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCUSLocation> OPSCO_TimeLine_ORDLocation_OnMap_List(int conID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_ORDLocation_OnMap_List(conID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_Schedule_Data OPSCO_TimeLine_Vehicle_Schedule_Data(int typeofvehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_TimeLine_Vehicle_Schedule_Data(typeofvehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion

        #region NewDI - Map

        public DTOResult OPSDI_MAP_Order_List(string request, int typeOfOrder, DateTime? fDate, DateTime? tDate, List<int> dataCus)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_Order_List(request, typeOfOrder, fDate, tDate, dataCus);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSDI_MAP_Vehicle_List(string request, DateTime now)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_Vehicle_List(request, now);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSDI_MAP_VehicleVendor_List(string request, int vendorID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_VehicleVendor_List(request, vendorID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public DTOResult OPSDI_MAP_TOMaster_List(string request, bool isApproved, bool isTendered, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_TOMaster_List(request, isApproved, isTendered, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSDI_MAP_TOMaster_GroupProduct_List(string request, int mID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_TOMaster_GroupProduct_List(request, mID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSDI_MAP_Trip OPSDI_MAP_TripByID(int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_TripByID(masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSDI_MAP_Trip> OPSDI_MAP_TripByVehicle_List(DateTime now, int vehicleID, int total)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_TripByVehicle_List(now, vehicleID, total);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCO_MAP_VehicleAvailable OPSDI_MAP_CheckVehicleAvailable(int masterID, int vehicleID, DateTime ETD, DateTime ETA, double Ton)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_CheckVehicleAvailable(masterID, vehicleID, ETD, ETA, Ton);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSDI_MAP_Schedule_Data OPSDI_MAP_Schedule_Data(List<int> dataVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_Schedule_Data(dataVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSDI_MAP_GroupProduct> OPSDI_MAP_GroupByTrip_List(int tID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_GroupByTrip_List(tID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int OPSDI_MAP_Save(DTOOPSDI_MAP_Trip item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_Map_Update(DTOOPSDI_MAP_Trip item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_Map_Update(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public void OPSDI_MAP_ToMON(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_ToMON(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_ToOPS(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_ToOPS(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_Cancel(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_Cancel(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_UpdateAndToMON(int mID, string tDriverName, string tDriverTel)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_UpdateAndToMON(mID, tDriverName, tDriverTel);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_ToVendor(DTOOPSDI_MAP_Trip item, List<DTODIAppointmentRouteTender> dataRate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_ToVendor(item, dataRate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_Delete(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_Delete(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_GroupProduct_Split(int gopID, int total, double value, int packingType)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_GroupProduct_Split(gopID, total, value, packingType);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_GroupProduct_Split_Cancel(int orderGopID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_GroupProduct_Split_Cancel(orderGopID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public bool OPSDI_MAP_FTL_Split_Check(int tomasterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_FTL_Split_Check(tomasterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_FTL_Split(int toMasterID, List<DTOOPSDI_MAP_GroupProduct> dataGop)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_FTL_Split(toMasterID, dataGop);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public void OPSDI_MAP_FTL_Merge(List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_FTL_Merge(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public void OPSDI_MAP_Vehicle_New(int vendorID, string regNo, double maxWeight)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_Vehicle_New(vendorID, regNo, maxWeight);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        //View xem chuyến mới
        public DTOResult OPSDI_MAP_DITOGroupProduct_List(string request, bool isApproved, bool isTendered, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_DITOGroupProduct_List(request, isApproved, isTendered, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSDI_MAP_2View_GroupProduct_List(string request, List<int> data, DateTime? fDate, DateTime? tDate)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_2View_GroupProduct_List(request, data, fDate, tDate);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool OPSDI_MAP_2View_Master_Update_Check4Delete(int mID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_2View_Master_Update_Check4Delete(mID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool OPSDI_MAP_2View_Master_Update_Check4Update(int mID, int gopID, double value, int packingType)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_2View_Master_Update_Check4Update(mID, gopID, value, packingType);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public bool OPSDI_MAP_2View_Master_Update_Check4Consolidate(int mID, int gopID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_2View_Master_Update_Check4Consolidate(mID, gopID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_2View_Master_Update_TimeLine(int mID, int vehicleID, DateTime ETD, DateTime ETA)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_2View_Master_Update_TimeLine(mID, vehicleID, ETD, ETA);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_2View_Master_Update_Group(int mID, int gopID, bool isRemove)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_2View_Master_Update_Group(mID, gopID, isRemove);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_2View_Master_Update_Group_Quantity(int mID, int gopID, double value, int packingType)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_2View_Master_Update_Group_Quantity(mID, gopID, value, packingType);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_2View_Master_Update(DTOOPSDI_MAP_Trip item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_2View_Master_Update(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSDI_MAP_Schedule_Data OPSDI_MAP_New_Schedule_Data(bool isShowVehicle, string strVehicle, int typeOfResource, List<int> dataCus, List<int> dataStatus)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_New_Schedule_Data(isShowVehicle, strVehicle, typeOfResource, dataCus, dataStatus);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPSDI_MAP_New_Schedule_DITOGroupProduct_List(string request, int vendorID, DateTime fDate, DateTime tDate, List<int> dataCus, List<int> dataStatus)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_New_Schedule_DITOGroupProduct_List(request, vendorID, fDate, tDate, dataCus, dataStatus);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSDI_MAP_Vehicle OPSDI_MAP_TimeLine_Vehicle_Info(int venID, int vehID, DateTime now)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_MAP_TimeLine_Vehicle_Info(venID, vehID, now);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSDI_MAP_TimeLine_Master_Update_Group(int mID, List<int> dataGroupProduct)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSDI_MAP_TimeLine_Master_Update_Group(mID, dataGroupProduct);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion
        
        #region NewDI - Import

        public DTOResult OPS_DI_Import_Packet_List(string request, bool isCreated)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_List(request, isCreated);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Import_Packet_Setting_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_Setting_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Import_Packet_TOMaster_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_TOMaster_List(request, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Import_Packet_GroupProduct_ByMaster_List(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_GroupProduct_ByMaster_List(request, masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Import_Packet_GroupProduct_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_GroupProduct_List(request, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Import_Packet_ORDGroupProduct_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_ORDGroupProduct_List(request, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_DI_Import_Packet_ORDGroupProduct_NotIn_List(string request, int sID, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_ORDGroupProduct_NotIn_List(request, sID, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSDIImportPacket_GroupProductExport> OPS_DI_Import_Packet_ORDGroupProductExport_List(int pID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_ORDGroupProductExport_List(pID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSSettingPlan OPS_DI_Import_Packet_Setting_Get(int sID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_Setting_Get(sID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCUSSettingPlan> OPS_DI_Import_Packet_SettingPlan()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_SettingPlan();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSDIImportPacket OPS_DI_Import_Packet_Get(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_Get(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSDIImportPacket_Data OPS_DI_Import_Packet_Data(int pID, List<string> dataOrders)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_Data(pID, dataOrders);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int OPS_DI_Import_Packet_Save(DTOOPSDIImportPacket item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_Packet_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Import_Packet_Import(int pID, int sID, List<DTOOPSDIImportPacketTOMaster_Import> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Import_Packet_Import(pID, sID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Import_Packet_2View_Save(List<DTOOPSDIImportPacketTOMaster> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Import_Packet_2View_Save(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Import_Packet_ToOPS(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Import_Packet_ToOPS(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Import_Packet_ToMON(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Import_Packet_ToMON(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Import_Packet_Delete(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Import_Packet_Delete(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Import_Packet_Reset(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Import_Packet_Reset(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Import_Packet_Vehicle_Update(List<DTOOPSDIImportPacket_Vehicle> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Import_Packet_Vehicle_Update(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Import_Packet_ORDGroupProduct_SaveList(int pID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Import_Packet_ORDGroupProduct_SaveList(pID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_DI_Import_Packet_ORDGroupProduct_DeleteList(int pID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_DI_Import_Packet_ORDGroupProduct_DeleteList(pID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion        
                
        #region NewCO - Import

        public DTOResult OPS_CO_Import_Packet_List(string request, bool isCreated)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_List(request, isCreated);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_CO_Import_Packet_Setting_List(string request)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_Setting_List(request);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_CO_Import_Packet_TOMaster_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_TOMaster_List(request, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_CO_Import_Packet_Container_ByMaster_List(string request, int masterID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_Container_ByMaster_List(request, masterID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_CO_Import_Packet_Container_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_Container_List(request, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_CO_Import_Packet_ORDContainer_List(string request, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_ORDContainer_List(request, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOResult OPS_CO_Import_Packet_ORDContainer_NotIn_List(string request, int sID, int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_ORDContainer_NotIn_List(request, sID, pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOCUSSettingPlan OPS_CO_Import_Packet_Setting_Get(int sID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_Setting_Get(sID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOCUSSettingPlan> OPS_CO_Import_Packet_SettingPlan()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_SettingPlan();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSDIImportPacket OPS_CO_Import_Packet_Get(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_Get(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public DTOOPSCOImportPacket_Data OPS_CO_Import_Packet_Data(int pID, List<string> dataOrders)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_Data(pID, dataOrders);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCOImportPacket_ContainerExport> OPS_CO_Import_Packet_ORDContainerExport_List(int pID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_ORDContainerExport_List(pID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public int OPS_CO_Import_Packet_Save(DTOOPSCOImportPacket item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_Save(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_CO_Import_Packet_Delete(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Import_Packet_Delete(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_CO_Import_Packet_Reset(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Import_Packet_Reset(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_CO_Import_Packet_Import(int pID, int sID, List<DTOOPSCOImportPacketTOMaster_Import> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Import_Packet_Import(pID, sID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_CO_Import_Packet_2View_Save(List<DTOOPSCOImportPacketTOMaster> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Import_Packet_2View_Save(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_CO_Import_Packet_ToOPS(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Import_Packet_ToOPS(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_CO_Import_Packet_ToMON(int pID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Import_Packet_ToMON(pID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }        
        }

        public void OPS_CO_Import_Packet_Vehicle_Update(List<DTOOPSDIImportPacket_Vehicle> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Import_Packet_Vehicle_Update(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_CO_Import_Packet_ORDContainer_SaveList(int pID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Import_Packet_ORDContainer_SaveList(pID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPS_CO_Import_Packet_ORDContainer_DeleteList(int pID, List<int> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPS_CO_Import_Packet_ORDContainer_DeleteList(pID, data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public string OPS_CO_Import_Packet_CheckLocation()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_CO_Import_Packet_CheckLocation();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        #endregion        

        #region DI Import ExcelOnline
        public SYSExcel OPS_DI_Import_ExcelOnline_Init(int templateID, int pID, int functionid, string functionkey, bool isreload)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPS_DI_Import_ExcelOnline_Init(templateID, pID, functionid, functionkey, isreload);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        #endregion

        #region NewCO - Vendor

        public DTOResult OPSCO_VEN_COTOContainer_List(string request, int typeOfView)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_VEN_COTOContainer_List(request, typeOfView);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_VEN_COTOContainer_Add_No(DTOOPSCO_VEN_Container item)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_VEN_COTOContainer_Add_No(item);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCO_VEN_Driver> OPSCO_VEN_Driver_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_VEN_Driver_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCO_VEN_Vehicle> OPSCO_VEN_Tractor_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_VEN_Tractor_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCO_VEN_Vehicle> OPSCO_VEN_Romooc_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_VEN_Romooc_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public List<DTOOPSCO_VEN_Reason> OPSCO_VEN_Reason_List()
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_VEN_Reason_List();
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_VEN_Reject(List<DTOOPSCO_VEN_TOMaster> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_VEN_Reject(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_VEN_Accept(List<DTOOPSCO_VEN_TOMaster> data)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_VEN_Accept(data);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        public DTOResult OPSCO_VEN_Vehicle_List(string request, int venID, int typeOfVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSCO_VEN_Vehicle_List(request, venID, typeOfVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_VEN_Change_Time(int mID, DateTime ETD, DateTime ETA)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_VEN_Change_Time(mID, ETD, ETA);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_VEN_Change_Vehicle(int mID, int vehID, int romID)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_VEN_Change_Vehicle(mID, vehID, romID);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_VEN_Change_Driver(int mID, string name, string tel)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_VEN_Change_Driver(mID, name, tel);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }

        public void OPSCO_VEN_Vehicle_New(int vendorID, string regNo, double? maxWeight, int typeOfVehicle)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    bl.OPSCO_VEN_Vehicle_New(vendorID, regNo, maxWeight, typeOfVehicle);
                }
            }
            catch (FaultException<DTOError> ex)
            {
                throw ex;
            }
            catch (Exception ex)
            {
                throw FaultHelper.ServiceFault(ex);
            }
        }
        
        #endregion

        #region NewDI - Vendor

        public DTOResult OPSDI_VEN_DITOGroupProduct_List(string request, int typeOfView)
        {
            try
            {
                using (var bl = CreateBusiness<BLOperation>())
                {
                    return bl.OPSDI_VEN_DITOGroupProduct_List(request, typeOfView);
                }
            }
            catch (FaultException<DTOError> ex)
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