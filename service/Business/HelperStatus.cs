using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Data;
using DTO;
using System.ServiceModel;

namespace Business
{
    public static class HelperStatus
    {
        public static void ORDOrder_Status(DataEntities model, AccountItem Account, List<int> lstOrderID)
        {
            try
            {
                model.EventAccount = Account; model.EventRunning = false;
                int iFCL = -(int)SYSVarType.TransportModeFCL;
                int iLCL = -(int)SYSVarType.TransportModeLCL;
                int iFTL = -(int)SYSVarType.TransportModeFTL;
                int iLTL = -(int)SYSVarType.TransportModeLTL;

                int iStatusOrderNew = -(int)SYSVarType.StatusOfOrderNew;
                int iStatusOrderPlaning = -(int)SYSVarType.StatusOfOrderPlaning;
                int iStatusOrderTranfer = -(int)SYSVarType.StatusOfOrderTranfer;
                int iStatusOrderReceived = -(int)SYSVarType.StatusOfOrderReceived;
                int iStatusOrderInvoicePart = -(int)SYSVarType.StatusOfOrderInvoicePart;
                int iStatusOrderInvoiceComplete = -(int)SYSVarType.StatusOfOrderInvoiceComplete;
                int iStatusOrderClosed = -(int)SYSVarType.StatusOfOrderClosed;
                int iStatusOrderClosedPart = -(int)SYSVarType.StatusOfOrderClosedPart;

                int iStatusPlanWaiting = -(int)SYSVarType.StatusOfPlanWaiting;
                int iStatusPlanPlaning = -(int)SYSVarType.StatusOfPlanPlaning;
                int iStatusPlanPart = -(int)SYSVarType.StatusOfPlanPart;
                int iStatusPlanComplete = -(int)SYSVarType.StatusOfPlanComplete;

                lstOrderID = lstOrderID.Distinct().ToList();
                var lstRouteDetailRunningID = new List<int>();
                var lstRouteDetailCompleteID = new List<int>();
                foreach (var orderID in lstOrderID)
                {
                    var order = model.ORD_Order.FirstOrDefault(c => c.ID == orderID);
                    if (order != null)
                    {
                        order.ModifiedBy = Account.UserName;
                        order.ModifiedDate = DateTime.Now;
                        var tranport = model.CAT_TransportMode.FirstOrDefault(c => c.ID == order.TransportModeID);

                        List<ORD_OrderStatus> lstOrderStatus = new List<ORD_OrderStatus>();
                        // Xóa status cũ
                        foreach (var item in model.ORD_OrderStatus.Where(c => c.OrderID == orderID))
                            model.ORD_OrderStatus.Remove(item);

                        #region Đơn hàng Cont
                        if (tranport.TransportModeID == iFCL || tranport.TransportModeID == iLCL)
                        {
                            #region Cập nhật cho ORD_OrderStatus
                            // Dữ liệu kiểm tra
                            var lstContainerID = model.ORD_Container.Where(c => c.OrderID == orderID).Select(c => c.ID).ToList();
                            var lstOPSContainer = model.OPS_Container.Where(c => c.ORD_Container.OrderID == orderID).Select(c => new
                            {
                                c.ContainerID,
                                c.OPSContainerTypeID
                            }).ToList();
                            var lstOPSCOTOContainer = model.OPS_COTOContainer.Where(c => c.OPS_Container.ORD_Container.OrderID == orderID).Select(c => new
                            {
                                c.OPS_Container.ContainerID,
                                c.COTOMasterID
                            }).ToList();
                            // Ktra từng cont
                            foreach (var containerID in lstContainerID)
                            {
                                int iStatusOrder = -1;
                                int iStatusPlan = -1;

                                // Đơn hàng chưa gửi điều phối
                                if (lstOPSContainer.Count(c => c.ContainerID == containerID) == 0)
                                {
                                    iStatusOrder = iStatusOrderNew;
                                    iStatusPlan = iStatusPlanWaiting;
                                }

                                // Đơn hàng gửi điều phối
                                if (lstOPSContainer.Count(c => c.ContainerID == containerID) > 0)
                                {
                                    // Hoàn tất vận chuyển
                                    if (lstOPSContainer.Count(c => c.ContainerID == containerID && c.OPSContainerTypeID == -(int)SYSVarType.OPSContainerTypeComplete) > 0)
                                    {
                                        iStatusOrder = iStatusOrderReceived;
                                        iStatusPlan = iStatusPlanComplete;
                                    }
                                    else
                                    {
                                        // Chưa được lập kế hoạch
                                        if (lstOPSCOTOContainer.Count(c => c.ContainerID == containerID && c.COTOMasterID > 0) == 0)
                                        {
                                            iStatusOrder = iStatusOrderPlaning;
                                            iStatusPlan = iStatusPlanPlaning;
                                        }
                                        else
                                        {
                                            // Hoàn tất lập kế hoạch
                                            if (lstOPSCOTOContainer.Count(c => c.ContainerID == containerID && c.COTOMasterID > 0) == lstOPSCOTOContainer.Count(c => c.ContainerID == containerID))
                                            {
                                                iStatusOrder = iStatusOrderPlaning;
                                                iStatusPlan = iStatusPlanComplete;
                                                // Ktra có đang được vận chuyển
                                                if (lstOPSContainer.Count(c => c.ContainerID == containerID && c.OPSContainerTypeID == -(int)SYSVarType.OPSContainerTypeRunning) > 0)
                                                    iStatusOrder = iStatusOrderTranfer;
                                            }
                                            else
                                            {
                                                // Lập kế hoạch 1 phần
                                                iStatusOrder = iStatusOrderPlaning;
                                                iStatusPlan = iStatusPlanPart;
                                            }
                                        }
                                    }
                                }

                                if (iStatusOrder > 0 && iStatusPlan > 0)
                                {
                                    var status = lstOrderStatus.FirstOrDefault(c => c.OrderID == orderID && c.StatusOfOrderID == iStatusOrder && c.StatusOfPlanID == iStatusPlanPart);
                                    if (status == null)
                                    {
                                        status = new ORD_OrderStatus();
                                        status.CreatedBy = Account.UserName;
                                        status.CreatedDate = DateTime.Now;
                                        status.OrderID = orderID;
                                        status.StatusOfOrderID = iStatusOrder;
                                        status.StatusOfPlanID = iStatusPlanPart;
                                        lstOrderStatus.Add(status);
                                    }
                                    status.TotalDetail += 1;

                                    var cont = model.ORD_Container.FirstOrDefault(c => c.ID == containerID);
                                    if (cont != null)
                                    {
                                        cont.WORDStatusOfOrderID = iStatusOrder;
                                        cont.WORDStatusOfPlanID = iStatusPlan;
                                    }
                                }
                            }
                            #endregion

                            #region Cập nhật riêng cho ORD_Order
                            int totalOPS = model.OPS_COTOContainer.Count(c => c.OPS_Container.ORD_Container.OrderID == orderID);
                            int totalTOMaster = model.OPS_COTOContainer.Count(c => c.OPS_Container.ORD_Container.OrderID == orderID && c.COTOMasterID.HasValue);
                            int totalTender = model.OPS_COTOContainer.Count(c => c.OPS_Container.ORD_Container.OrderID == orderID && c.COTOMasterID.HasValue && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterTendered);
                            int totalTOGroupComplete = model.OPS_COTOContainer.Count(c => c.OPS_Container.ORD_Container.OrderID == order.ID && (c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel));
                            int totalTOGroupPODComplete = model.OPS_COTOContainer.Count(c => c.OPS_Container.ORD_Container.OrderID == order.ID && c.TypeOfStatusContainerPODID == -(int)SYSVarType.TypeOfStatusContainerPODComplete);

                            if (order.RouteDetailID > 0 && totalTOMaster > 0 && !lstRouteDetailRunningID.Contains(order.RouteDetailID.Value))
                                lstRouteDetailRunningID.Add(order.RouteDetailID.Value);

                            order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanWaiting;
                            order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderNew;
                            if (totalOPS > 0)
                            {
                                order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanPlaning;
                                order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderPlaning;
                            }
                            if (totalTOMaster > 0)
                                order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanPart;// Hoàn tất lập 1 phần KH
                            if (totalTOMaster > 0 && totalOPS == totalTOMaster)
                                order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanComplete;// Hoàn tất lập kế hoạch
                            if (totalTender > 0)
                                order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderTranfer; // Đang vận chuyển
                            if (totalTOGroupComplete > 0 && totalTOGroupComplete == totalOPS)
                            {
                                order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderReceived; // Đã giao hàng
                                if (order.RouteDetailID > 0 && !lstRouteDetailCompleteID.Contains(order.RouteDetailID.Value))
                                    lstRouteDetailCompleteID.Add(order.RouteDetailID.Value);
                                if (totalTOGroupPODComplete > 0)
                                {
                                    order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderInvoicePart; // Nhận 1 phần chứng từ
                                    if (totalTOGroupPODComplete == totalOPS)
                                        order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderInvoiceComplete; // Đã nhận chứng từ
                                }
                            }
                            #endregion
                        }
                        #endregion

                        #region Đơn hàng phân phối
                        else
                        {
                            #region Cập nhật cho ORD_OrderStatus
                            // Dữ liệu kiểm tra
                            var lstGroupID = model.ORD_GroupProduct.Where(c => c.OrderID == orderID).Select(c => c.ID).ToList();
                            var lstOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.OrderID == orderID).Select(c => new
                            {
                                GroupID = c.OrderGroupProductID,
                                c.DITOGroupProductStatusID,
                                c.DITOGroupProductStatusPODID,
                                c.DITOMasterID,
                                StatusOfDITOMasterID = c.DITOMasterID > 0 ? c.OPS_DITOMaster.StatusOfDITOMasterID : -1,
                            }).ToList();

                            // Ktra từng OPSGroup
                            foreach (var groupID in lstGroupID)
                            {
                                int iStatusOrder = iStatusOrderPlaning;
                                int iStatusPlan = -1;
                                var lstOPSGroupQuery = lstOPSGroup.Where(c => c.GroupID == groupID).ToList();
                                // Chưa gửi điều phối
                                if (lstOPSGroupQuery.Count == 0)
                                {
                                    iStatusOrder = iStatusOrderNew;
                                    iStatusPlan = iStatusPlanWaiting;
                                }

                                // Đơn hàng gửi điều phối
                                if (lstOPSGroupQuery.Count > 0)
                                {
                                    // Bị hủy
                                    if (lstOPSGroupQuery.Count(c => c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel) > 0)
                                    {
                                        iStatusPlan = iStatusPlanWaiting;
                                        iStatusOrder = iStatusOrderClosedPart;
                                        if (lstOPSGroupQuery.Count(c => c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel) == lstOPSGroupQuery.Count)
                                            iStatusOrder = iStatusOrderClosed;
                                    }
                                    else
                                    {
                                        // Đang lập kế hoạch
                                        if (lstOPSGroupQuery.Count(c => c.DITOMasterID == null) == lstOPSGroupQuery.Count)
                                            iStatusPlan = iStatusPlanPlaning;
                                        else
                                        {
                                            // Lập kế hoạch 1 phần
                                            iStatusPlan = iStatusPlanPart;
                                            // Hoàn tất kế hoạch
                                            if (lstOPSGroupQuery.Count(c => c.DITOMasterID > 0 && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterPlanning) == lstOPSGroupQuery.Count)
                                            {
                                                iStatusPlan = iStatusPlanComplete;
                                                // Đang vận chuyển
                                                if (lstOPSGroupQuery.Count(c => c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered) > 0)
                                                    iStatusOrder = iStatusOrderTranfer;
                                                // Hoàn tất vận chuyển
                                                if (lstOPSGroupQuery.Count(c => c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete || c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel) == lstOPSGroupQuery.Count)
                                                    iStatusOrder = iStatusOrderReceived;
                                                // Nhận 1 phần chứng từ
                                                if (lstOPSGroupQuery.Count(c => c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete) > 0)
                                                    iStatusOrder = iStatusOrderInvoicePart;
                                                if (lstOPSGroupQuery.Count(c => c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete || c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel) == lstOPSGroupQuery.Count)
                                                    iStatusOrder = iStatusOrderInvoiceComplete;
                                            }
                                        }
                                    }
                                }

                                if (iStatusOrder > 0 && iStatusPlan > 0)
                                {
                                    var status = lstOrderStatus.FirstOrDefault(c => c.OrderID == orderID && c.StatusOfOrderID == iStatusOrder && c.StatusOfPlanID == iStatusPlanPart);
                                    if (status == null)
                                    {
                                        status = new ORD_OrderStatus();
                                        status.CreatedBy = Account.UserName;
                                        status.CreatedDate = DateTime.Now;
                                        status.OrderID = orderID;
                                        status.StatusOfOrderID = iStatusOrder;
                                        status.StatusOfPlanID = iStatusPlanPart;
                                        lstOrderStatus.Add(status);
                                    }
                                    status.TotalDetail += 1;

                                    var group = model.ORD_GroupProduct.FirstOrDefault(c => c.ID == groupID);
                                    if (group != null)
                                    {
                                        group.WORDStatusOfOrderID = iStatusOrder;
                                        group.WORDStatusOfPlanID = iStatusPlan;
                                    }
                                }
                            }
                            #endregion

                            #region Cập nhật riêng cho ORD_Order
                            if (order.CAT_TransportMode.TransportModeID == iFTL || order.CAT_TransportMode.TransportModeID == iLTL)
                            {
                                int totalOPS = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == orderID && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel);
                                int totalTOMaster = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == orderID && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && c.DITOMasterID.HasValue);
                                int totalTender = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == orderID && c.DITOMasterID.HasValue && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered);
                                int totalTOGroupComplete = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == order.ID && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete);
                                int totalTOGroupPODComplete = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == order.ID && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete);
                                int totalCancel = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == orderID && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel);

                                if (order.RouteDetailID > 0 && totalTOMaster > 0 && !lstRouteDetailRunningID.Contains(order.RouteDetailID.Value))
                                    lstRouteDetailRunningID.Add(order.RouteDetailID.Value);

                                order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanWaiting;
                                order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderNew;
                                if (totalOPS > 0 || totalCancel > 0)
                                {
                                    order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanPlaning;
                                    order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderPlaning;
                                }

                                if (totalTOMaster > 0)
                                    order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanPart;// Hoàn tất lập 1 phần KH
                                if (totalTOMaster > 0 && totalOPS == totalTOMaster)
                                    order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanComplete;// Hoàn tất lập kế hoạch
                                if (totalTender > 0)
                                    order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderTranfer; // Đang vận chuyển
                                if (totalTOGroupComplete > 0)
                                {
                                    order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderReceived; // Đã giao hàng
                                    if (order.RouteDetailID > 0 &&  !lstRouteDetailCompleteID.Contains(order.RouteDetailID.Value))
                                        lstRouteDetailCompleteID.Add(order.RouteDetailID.Value);
                                    if (totalTOGroupPODComplete > 0)
                                    {
                                        order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderInvoicePart; // Nhận 1 phần chứng từ
                                        if (totalTOGroupPODComplete == totalOPS)
                                            order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderInvoiceComplete; // Đã nhận chứng từ
                                    }
                                }
                            }
                            #endregion
                        }
                        #endregion

                        foreach (var item in lstOrderStatus)
                            model.ORD_OrderStatus.Add(item);
                    }
                }

                if (lstRouteDetailCompleteID.Count > 0)
                {
                    var lstRouteID = new List<int>(); 
                }

                if (lstRouteDetailRunningID.Count > 0)
                {
                    var lstRouteID = new List<int>();
                    foreach (var routedetailid in lstRouteDetailRunningID)
                    {
                        var obj = model.ORD_RouteDetail.FirstOrDefault(c => c.ID == routedetailid);
                        if (obj != null)
                        {
                            obj.RouteDetailStatusID = -(int)SYSVarType.RouteDetailStatusOPSRun;
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;

                            if (!lstRouteID.Contains(obj.RouteID))
                                lstOrderID.Add(obj.RouteID);
                        }
                    }
                    if (lstRouteID.Count > 0)
                    {
                        var lstORDOrderID = model.ORD_RouteOrder.Where(c => lstRouteID.Contains(c.RouteID)).Select(c => c.OrderID).ToList();
                        foreach (var order in model.ORD_Order.Where(c => lstORDOrderID.Contains(c.ID) && c.StatusOfOrderID == -(int)SYSVarType.StatusOfOrderNew))
                        {
                            order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderTranfer;
                            order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanComplete;
                        }
                        foreach (var container in model.ORD_Container.Where(c => lstORDOrderID.Contains(c.OrderID) && c.WORDStatusOfOrderID == -(int)SYSVarType.StatusOfOrderNew))
                        {
                            container.WORDStatusOfOrderID = -(int)SYSVarType.StatusOfOrderTranfer;
                            container.WORDStatusOfPlanID = -(int)SYSVarType.StatusOfPlanComplete;
                        }
                    }
                }

                model.SaveChanges();
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }

        public static void OPSDIMaster_Status(DataEntities model, AccountItem Account, List<int> lstMasterID)
        {
            var lstOrderID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && lstMasterID.Contains(c.DITOMasterID.Value)).Select(c => c.ORD_GroupProduct.OrderID).Distinct().ToList();
            ORDOrder_Status(model, Account, lstOrderID);
            OPSMaster_Status(model, Account, lstMasterID, null);
        }

        public static void OPSCOMaster_Status(DataEntities model, AccountItem Account, List<int> lstMasterID)
        {
            var lstOrderID = model.OPS_COTOContainer.Where(c => c.COTOMasterID.HasValue && lstMasterID.Contains(c.COTOMasterID.Value)).Select(c => c.OPS_Container.ORD_Container.OrderID).Distinct().ToList();
            ORDOrder_Status(model, Account, lstOrderID);
            OPSMaster_Status(model, Account, null, lstMasterID);
        }

        private static void OPSMaster_Status(DataEntities model, AccountItem Account, List<int> lstDIMasterID, List<int> lstCOMasterID)
        {
            try
            {
                if (lstDIMasterID != null && lstDIMasterID.Count > 0)
                {
                    foreach (var itemMasterID in lstDIMasterID)
                    {
                        var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == itemMasterID);
                        if (master != null && master.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterReceived)
                        {
                            if (model.OPS_DITOGroupProduct.Count(c => c.DITOMasterID == master.ID) == model.OPS_DITOGroupProduct.Count(c => c.DITOMasterID == master.ID && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete))
                                master.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterInvoice;
                        }
                    }
                    model.SaveChanges();
                }

                if (lstCOMasterID != null && lstCOMasterID.Count > 0)
                {
                    foreach (var itemMasterID in lstCOMasterID)
                    {
                        var master = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == itemMasterID);
                        if (master != null && master.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterReceived)
                        {
                            if (model.OPS_COTOContainer.Count(c => c.COTOMasterID == master.ID) == model.OPS_COTOContainer.Count(c => c.COTOMasterID == master.ID && c.TypeOfStatusContainerPODID == -(int)SYSVarType.TypeOfStatusContainerPODComplete))
                                master.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterInvoice;
                        }
                    }
                    model.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw FaultHelper.BusinessFault(ex);
            }
        }
    }

    //public class StatusHelper : Base, IBase
    //{
    //    /// <summary>
    //    /// Cập nhật trạng thái Đơn hàng sau khi tạo, chỉnh sửa, xóa lệnh OPS or cập nhật POD
    //    /// </summary>
    //    /// <param name="lstOrderID">Ds đơn hàng cần cập nhật</param>
    //    public void ORDStatus_Update(List<int> lstOrderID)
    //    {
    //        try
    //        {
    //            using (var model = new DataEntities())
    //            {
    //                model.EventAccount = Account; model.EventRunning = false;
    //                int iFCL = -(int)SYSVarType.TransportModeFCL;
    //                int iLCL = -(int)SYSVarType.TransportModeLCL;
    //                int iFTL = -(int)SYSVarType.TransportModeFTL;
    //                int iLTL = -(int)SYSVarType.TransportModeLTL;
    //                lstOrderID = lstOrderID.Distinct().ToList();
    //                foreach (var orderID in lstOrderID)
    //                {
    //                    var order = model.ORD_Order.FirstOrDefault(c => c.ID == orderID);
    //                    if (order != null)
    //                    {
    //                        order.ModifiedBy = Account.UserName;
    //                        order.ModifiedDate = DateTime.Now;
    //                        // Ktra đơn hàng container
    //                        if (order.CAT_TransportMode.TransportModeID == iFCL || order.CAT_TransportMode.TransportModeID == iLCL)
    //                        {
    //                            int totalOPS = model.OPS_COTOContainer.Count(c => c.OPS_Container.ORD_Container.OrderID == orderID);
    //                            int totalTOMaster = model.OPS_COTOContainer.Count(c => c.OPS_Container.ORD_Container.OrderID == orderID && c.COTOMasterID.HasValue);
    //                            int totalTender = model.OPS_COTOContainer.Count(c => c.OPS_Container.ORD_Container.OrderID == orderID && c.COTOMasterID.HasValue && c.OPS_COTOMaster.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterTendered);
    //                            int totalTOGroupComplete = model.OPS_COTOContainer.Count(c => c.OPS_Container.ORD_Container.OrderID == order.ID && (c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerComplete || c.TypeOfStatusContainerID == -(int)SYSVarType.TypeOfStatusContainerCancel));
    //                            int totalTOGroupPODComplete = model.OPS_COTOContainer.Count(c => c.OPS_Container.ORD_Container.OrderID == order.ID && c.TypeOfStatusContainerPODID == -(int)SYSVarType.TypeOfStatusContainerPODComplete);

    //                            order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanWaiting;
    //                            order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderNew;
    //                            if (totalOPS > 0)
    //                            {
    //                                order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanPlaning;
    //                                order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderPlaning;
    //                            }
    //                            if (totalTOMaster > 0)
    //                                order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanPart;// Hoàn tất lập 1 phần KH
    //                            if (totalTOMaster > 0 && totalOPS == totalTOMaster)
    //                                order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanComplete;// Hoàn tất lập kế hoạch
    //                            if (totalTender > 0)
    //                                order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderTranfer; // Đang vận chuyển
    //                            if (totalTOGroupComplete > 0 && totalTOGroupComplete == totalOPS)
    //                            {
    //                                order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderReceived; // Đã giao hàng
    //                                if (totalTOGroupPODComplete > 0)
    //                                {
    //                                    order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderInvoicePart; // Nhận 1 phần chứng từ
    //                                    if (totalTOGroupPODComplete == totalOPS)
    //                                        order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderInvoiceComplete; // Đã nhận chứng từ
    //                                }
    //                            }
    //                        }
    //                        else
    //                        {
    //                            // Ktra đơn hàng xe tải
    //                            if (order.CAT_TransportMode.TransportModeID == iFTL || order.CAT_TransportMode.TransportModeID == iLTL)
    //                            {
    //                                int totalOPS = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == orderID && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel);
    //                                int totalTOMaster = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == orderID && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel && c.DITOMasterID.HasValue);
    //                                int totalTender = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == orderID && c.DITOMasterID.HasValue && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered);
    //                                int totalTOGroupComplete = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == order.ID && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete);
    //                                int totalTOGroupPODComplete = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == order.ID && c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete);
    //                                int totalCancel = model.OPS_DITOGroupProduct.Count(c => c.ORD_GroupProduct.OrderID == orderID && c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusCancel);

    //                                order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanWaiting;
    //                                order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderNew;
    //                                if (totalOPS > 0 || totalCancel > 0)
    //                                {
    //                                    order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanPlaning;
    //                                    order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderPlaning;
    //                                }

    //                                if (totalTOMaster > 0)
    //                                    order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanPart;// Hoàn tất lập 1 phần KH
    //                                if (totalTOMaster > 0 && totalOPS == totalTOMaster)
    //                                    order.StatusOfPlanID = -(int)SYSVarType.StatusOfPlanComplete;// Hoàn tất lập kế hoạch
    //                                if (totalTender > 0)
    //                                    order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderTranfer; // Đang vận chuyển
    //                                if (totalTOGroupComplete > 0)
    //                                {
    //                                    order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderReceived; // Đã giao hàng
    //                                    if (totalTOGroupPODComplete > 0)
    //                                    {
    //                                        order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderInvoicePart; // Nhận 1 phần chứng từ
    //                                        if (totalTOGroupPODComplete == totalOPS)
    //                                            order.StatusOfOrderID = -(int)SYSVarType.StatusOfOrderInvoiceComplete; // Đã nhận chứng từ
    //                                    }
    //                                }
    //                            }
    //                        }
    //                    }
    //                }

    //                model.SaveChanges();
    //            }
    //        }
    //        catch (FaultException<DTOError> ex)
    //        {
    //            throw ex;
    //        }
    //        catch (Exception ex)
    //        {
    //            throw FaultHelper.BusinessFault(ex);
    //        }
    //    }

    //    public void OPSDIMaster_Update(List<int> lstMasterID)
    //    {
    //        try
    //        {
    //            using (var model = new DataEntities())
    //            {
    //                lstMasterID = lstMasterID.Distinct().ToList();
    //                model.EventAccount = Account; model.EventRunning = false;
    //                foreach (var item in lstMasterID)
    //                {
    //                    var master = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item);
    //                    if (master != null)
    //                    {
    //                        if (master.OPS_DITOGroupProduct.Count(c => c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel) == master.OPS_DITOGroupProduct.Count(c => c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete))
    //                        {
    //                            master.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterReceived;
    //                            model.SaveChanges();
    //                        }
    //                    }
    //                }
    //                model.SaveChanges();
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            throw ex;
    //        }
    //    }
    //} 
}
