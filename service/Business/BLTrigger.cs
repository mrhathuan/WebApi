using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Kendo.Mvc.Extensions;
using Data;
using DTO;
using System.ServiceModel;
using OfficeOpenXml;
using System.Globalization;
using System.IO;
using System.Web;
using System.Reflection;
using Kendo.Mvc.UI;
using ExpressionEvaluator;

namespace Business
{
    public class BLTrigger : Base, IBase
    {
        #region GPS
        public void GPS_Refresh()
        {
            try
            {
                //using (var model = new DataEntities())
                //{
                //    string key = System.Configuration.ConfigurationManager.AppSettings["GPSKey"];
                //    switch (key)
                //    {
                //        case "vinafco": GPS_Refresh_vinafco(model);
                //            break;
                //    }
                //}
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

        private void GPS_Refresh_vinafco(DataEntities model)
        {
            //var lst = GPSHelper.VinafcoHelper.GetList();
            //foreach (var item in lst.Where(c => c.Lat > 0 && c.Lon > 0 && c.sDateTime != null))
            //{
            //    var obj = model.CAT_Vehicle.FirstOrDefault(c => c.GPSCode == item.LicenseCard);
            //    if (obj != null)
            //    {
            //        obj.Lat = item.Lat;
            //        obj.Lng = item.Lon;

            //        obj.ModifiedDate = DateTime.Now;
            //    }
            //}
            //model.SaveChanges();
        }
        #endregion

        #region Tự động cập nhật lệnh gửi Vendor
        public void OPSMasterTendered_AutoSendMail()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var reasonOutOfDate = model.CAT_Reason.FirstOrDefault(c => c.TypeOfReasonID == -(int)SYSVarType.TypeOfReasonTenderSystem);
                    var dtNow = DateTime.Now;

                    #region Container
                    var lstCOTOMaster = model.OPS_COTOMaster.Where(c => c.StatusOfCOTOMasterID == -(int)SYSVarType.StatusOfCOTOMasterApproved && !c.OPS_COTORate.Any(d => d.IsAccept == true) && c.VehicleID == null).ToList();
                    List<DTOMailVendor> lstMailContainer = new List<DTOMailVendor>();
                    foreach (var master in lstCOTOMaster)
                    {
                        // Hệ thống tự động từ chối các rate quá hạn
                        int SortOrder = 0;
                        var latestReject = master.OPS_COTORate.OrderByDescending(c => c.SortOrder).FirstOrDefault(c => c.IsAccept == false);
                        if (latestReject != null)
                            SortOrder = latestReject.SortOrder;
                        var lstOutOfDate = master.OPS_COTORate.Where(c => c.LastRateTime < dtNow && c.IsAccept == null).ToList();
                        foreach (var rate in lstOutOfDate)
                        {
                            rate.ModifiedBy = Account.UserName;
                            rate.ModifiedDate = DateTime.Now;
                            rate.IsAccept = false;
                            rate.ReasonID = reasonOutOfDate.ID;
                            rate.Reason = reasonOutOfDate.ReasonName;
                            if (rate.SortOrder > SortOrder)
                                SortOrder = rate.SortOrder;
                        }
                        SortOrder++;
                        // rate hiện tại
                        var currentRate = master.OPS_COTORate.FirstOrDefault(c => c.SortOrder == SortOrder && !c.IsSend);
                        if (currentRate != null)
                        {
                            int vendorID = 0;
                            if (currentRate.VendorID.HasValue)
                                vendorID = currentRate.VendorID.Value;

                            var vendor = lstMailContainer.FirstOrDefault(c => c.VendorID == vendorID && c.SysCustomerID == master.SYSCustomerID);
                            if (vendor != null)
                                vendor.ListRateID.Add(currentRate.ID);
                            else
                            {
                                vendor = new DTOMailVendor();
                                vendor.VendorID = vendorID;
                                vendor.ListRateID = new List<int>();
                                vendor.ListRateID.Add(currentRate.ID);
                                vendor.SysCustomerID = master.SYSCustomerID;
                                lstMailContainer.Add(vendor);
                            }
                        }
                        // Ktra nếu tất cả rate đều bị từ chối => chuyển state gửi phê duyệt lại
                        if (master.OPS_COTORate.Count(c => c.IsAccept == false) == master.OPS_COTORate.Count())
                        {
                            master.ModifiedBy = Account.UserName;
                            master.ModifiedDate = DateTime.Now;
                            master.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterApproveAgain;
                        }
                    }
                    #endregion

                    #region Truck
                    List<DTOMailVendor> lstMailTruck = new List<DTOMailVendor>();
                    var lstDITOMaster = model.OPS_DITOMaster.Where(c => c.StatusOfDITOMasterID == -(int)SYSVarType.StatusOfDITOMasterApproved && !c.OPS_DITORate.Any(d => d.IsAccept == true) && c.VehicleID == null).ToList();
                    foreach (var master in lstDITOMaster)
                    {
                        // Hệ thống tự động từ chối cái rate quá hạn
                        int SortOrder = 0;
                        var latestReject = master.OPS_DITORate.OrderByDescending(c => c.SortOrder).FirstOrDefault(c => c.IsAccept == false);
                        if (latestReject != null)
                            SortOrder = latestReject.SortOrder;
                        var lstOutOfDate = master.OPS_DITORate.Where(c => c.LastRateTime < dtNow && c.IsAccept == null).ToList();
                        foreach (var rate in lstOutOfDate)
                        {
                            rate.ModifiedBy = Account.UserName;
                            rate.ModifiedDate = DateTime.Now;
                            rate.IsAccept = false;
                            rate.ReasonID = reasonOutOfDate.ID;
                            rate.Reason = reasonOutOfDate.ReasonName;
                        }
                        // rate hiện tại
                        SortOrder++;
                        var currentRate = master.OPS_DITORate.FirstOrDefault(c => c.SortOrder == SortOrder && !c.IsSend);
                        if (currentRate != null)
                        {
                            int vendorID = 0;
                            if (currentRate.VendorID.HasValue)
                                vendorID = currentRate.VendorID.Value;

                            var vendor = lstMailTruck.FirstOrDefault(c => c.VendorID == vendorID && c.SysCustomerID == master.SYSCustomerID);
                            if (vendor != null)
                                vendor.ListRateID.Add(currentRate.ID);
                            else
                            {
                                vendor = new DTOMailVendor();
                                vendor.VendorID = vendorID;
                                vendor.ListRateID = new List<int>();
                                vendor.ListRateID.Add(currentRate.ID);
                                vendor.SysCustomerID = master.SYSCustomerID;
                                lstMailTruck.Add(vendor);
                            }
                        }

                        // Ktra nếu tất cả rate đều bị từ chối => chuyển state gửi phê duyệt lại
                        if (master.OPS_DITORate.Count(c => c.IsAccept == false) == master.OPS_DITORate.Count())
                        {
                            master.ModifiedBy = Account.UserName;
                            master.ModifiedDate = DateTime.Now;
                            master.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterApproveAgain;
                        }
                    }
                    #endregion

                    model.SaveChanges();

                    #region Gửi mail theo vendor
                    foreach (var mail in lstMailContainer)
                        Container_SendMailToVendor(mail.ListRateID, mail.VendorID, mail.SysCustomerID);

                    foreach (var mail in lstMailTruck)
                        Truck_SendMailToVendor(mail.ListRateID, mail.VendorID, mail.SysCustomerID);
                    #endregion
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

        private void Container_SendMailToVendor(List<int> lstRateID, int vendorID, int syscustomerid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var customer = model.CUS_Customer.FirstOrDefault(c => c.ID == syscustomerid);
                    var vendor = model.CUS_Customer.FirstOrDefault(c => c.ID == vendorID);
                    var lstRate = model.OPS_COTORate.Where(c => lstRateID.Contains(c.ID)).ToList();
                    var mailTemplate = HelperSYSSetting.MailTemplate_GetBySYSCustomerID(model, MailTemplateCode.OPSTenderingContainer, syscustomerid);
                    var mailSetting = HelperSYSSetting.SYSSettingSystem_GetBySYSCustomerID(model, syscustomerid);
                    if (vendor != null && !string.IsNullOrEmpty(vendor.Email) && customer != null && mailTemplate != null && mailSetting != null)
                    {
                        string strContent = mailTemplate.Content;
                        string strDetail = mailTemplate.Details;
                        string strSubject = MailHelper.StringHTML(mailTemplate.Subject, delegate(MailTemplate obj)
                        {
                            switch (obj.Token)
                            {
                                default: break;
                            }
                        });

                        string strMail = MailHelper.StringHTML(strContent, delegate(MailTemplate obj)
                        {
                            switch (obj.Token)
                            {
                                case "VendorName": obj.HTML = vendor.CustomerName; break;
                                case "ExpiredTime": obj.HTML = lstRate.Where(c => c.LastRateTime.HasValue).OrderBy(c => c.LastRateTime).FirstOrDefault().LastRateTime.Value.ToString("dd/MM/yyyy HH:mm:ss"); break;
                                case "URLToTenderPage": obj.HTML = mailSetting.Website; break;
                                case "CustomerName": obj.HTML = customer.CustomerName; break;
                                case "Address": obj.HTML = customer.Address; break;
                                case "TelNo": obj.HTML = customer.TelNo; break;
                                case "Fax": obj.HTML = customer.Fax; break;
                                case "Email": obj.HTML = customer.Email; break;
                                case "Details":
                                    string htmlDetail = string.Empty;
                                    foreach (var rate in lstRate)
                                    {
                                        if (rate != null && !rate.IsSend && rate.VendorID.HasValue)
                                        {
                                            rate.IsSend = true;
                                            var objMaster = rate.OPS_COTOMaster;
                                            var firstRoute = objMaster.OPS_COTO.OrderBy(c => c.SortOrder).FirstOrDefault(c => c.IsOPS);
                                            var lastRoute = objMaster.OPS_COTO.OrderByDescending(c => c.SortOrder).FirstOrDefault(c => c.IsOPS);

                                            htmlDetail += obj.HTML = MailHelper.StringHTML(strDetail, delegate(MailTemplate detail)
                                            {
                                                switch (detail.Token)
                                                {
                                                    case "TripNo": detail.HTML = objMaster.Code; break;
                                                    case "TypeOfContainer":
                                                        //var lstContainer = model.OPS_COTODetail.Where(c => c.OPS_COTO.COTOMasterID == objMaster.ID).GroupBy(c => c.ContainerID).Select(c => new
                                                        //{
                                                        //    TypeOfContainerID = c.FirstOrDefault().ORD_Container.PackingID,
                                                        //    TypeOfContainerName = c.FirstOrDefault().ORD_Container.CAT_Packing.PackingName
                                                        //});
                                                        //var lstTypeOfContainer = lstContainer.OrderBy(c => c.TypeOfContainerName).GroupBy(c => new { c.TypeOfContainerID, c.TypeOfContainerName }).Select(c => new
                                                        //{
                                                        //    TypeOfContainerName = c.Key.TypeOfContainerName,
                                                        //    NumberOfContainer = c.Count()
                                                        //}).ToList();
                                                        //detail.HTML = string.Empty;
                                                        //foreach (var container in lstTypeOfContainer)
                                                        //    detail.HTML += container.TypeOfContainerName + "x" + container.NumberOfContainer + " ";
                                                        break;
                                                    case "LocationFrom": detail.HTML = firstRoute.CAT_Routing.CAT_Location.Location; break;
                                                    case "LocationTo": detail.HTML = lastRoute.CAT_Routing.CAT_Location1.Location; break;
                                                    //case "ETD": detail.HTML = firstRoute.ETD.ToString("dd/MM/yyyy HH:mm:ss"); break;
                                                    //case "ETA": detail.HTML = lastRoute.ETA.ToString("dd/MM/yyyy HH:mm:ss"); break;
                                                }
                                            });
                                        }
                                    }
                                    obj.HTML = htmlDetail;
                                    break;
                            }
                        });

                        MailHelper.SendMail(mailSetting, vendor.Email, vendor.CustomerName, mailTemplate.CC, strSubject, strMail);

                    }
                    else
                    {
                        foreach (var rate in lstRate)
                            rate.IsSend = true;
                    }
                    model.SaveChanges();
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

        private void Container_SendMailToVendor(int id)
        {

        }

        private void Truck_SendMailToVendor(int opsrateID)
        {

        }

        private void Truck_SendMailToVendor(List<int> lstRateID, int vendorID, int syscustomerid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    var customer = model.CUS_Customer.FirstOrDefault(c => c.ID == syscustomerid);
                    var vendor = model.CUS_Customer.FirstOrDefault(c => c.ID == vendorID);
                    var lstRate = model.OPS_DITORate.Where(c => lstRateID.Contains(c.ID)).ToList();
                    var mailTemplate = HelperSYSSetting.MailTemplate_GetBySYSCustomerID(model, MailTemplateCode.OPSTenderingDistributor, syscustomerid);
                    var mailSetting = HelperSYSSetting.SYSSettingSystem_GetBySYSCustomerID(model, syscustomerid);
                    if (vendor != null && !string.IsNullOrEmpty(vendor.Email) && customer != null)
                    {
                        string strContent = mailTemplate.Content;
                        string strDetail = mailTemplate.Details;
                        string strSubject = MailHelper.StringHTML(mailTemplate.Subject, delegate(MailTemplate obj)
                        {
                            switch (obj.Token)
                            {
                                default: break;
                            }
                        });

                        string strMail = MailHelper.StringHTML(strContent, delegate(MailTemplate obj)
                        {
                            switch (obj.Token)
                            {
                                case "VendorName": obj.HTML = vendor.CustomerName; break;
                                case "ExpiredTime": obj.HTML = lstRate.Where(c => c.LastRateTime.HasValue).OrderBy(c => c.LastRateTime).FirstOrDefault().LastRateTime.Value.ToString("dd/MM/yyyy HH:mm:ss"); break;
                                case "URLToTenderPage": obj.HTML = mailSetting.Website; break;
                                case "CustomerName": obj.HTML = customer.CustomerName; break;
                                case "Address": obj.HTML = customer.Address; break;
                                case "TelNo": obj.HTML = customer.TelNo; break;
                                case "Fax": obj.HTML = customer.Fax; break;
                                case "Email": obj.HTML = customer.Email; break;
                                case "Details":
                                    string htmlDetail = string.Empty;
                                    foreach (var rate in lstRate)
                                    {
                                        if (rate != null && !rate.IsSend && rate.VendorID.HasValue)
                                        {
                                            rate.IsSend = true;
                                            var objMaster = rate.OPS_DITOMaster;
                                            var firstRoute = objMaster.OPS_DITO.OrderBy(c => c.SortOrder).FirstOrDefault(d => d.IsOPS);
                                            var lastRoute = objMaster.OPS_DITO.OrderByDescending(c => c.SortOrder).FirstOrDefault(d => d.IsOPS);
                                            if (firstRoute != null && lastRoute != null)
                                            {
                                                htmlDetail += obj.HTML = MailHelper.StringHTML(strDetail, delegate(MailTemplate detail)
                                                {
                                                    switch (detail.Token)
                                                    {
                                                        case "TripNo": detail.HTML = objMaster.Code; break;
                                                        case "TotalTon": detail.HTML = objMaster.OPS_DITOGroupProduct.Sum(c => c.Ton).ToString(); break;
                                                        case "TotalCBM": detail.HTML = objMaster.OPS_DITOGroupProduct.Sum(c => c.CBM).ToString(); break;
                                                        case "LocationFrom": detail.HTML = firstRoute.CAT_Routing.CAT_Location.Location; break;
                                                        case "LocationTo": detail.HTML = lastRoute.CAT_Routing.CAT_Location1.Location; break;
                                                        case "ETD": detail.HTML = firstRoute.ETD.Value.ToString("dd/MM/yyyy HH:mm:ss"); break;
                                                        case "ETA": detail.HTML = lastRoute.ETA.Value.ToString("dd/MM/yyyy HH:mm:ss"); break;
                                                    }
                                                });
                                            }
                                        }
                                    }
                                    obj.HTML = htmlDetail;
                                    break;
                            }
                        });

                        MailHelper.SendMail(mailSetting, vendor.Email, vendor.CustomerName, mailTemplate.CC, strSubject, strMail);
                    }
                    else
                    {
                        foreach (var rate in lstRate)
                            rate.IsSend = true;
                    }
                    model.SaveChanges();
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

        #region Lấy ds lệnh Master có status >= Tendered
        public List<DTOTriggerTOMaster> SMSTOMaster_List()
        {
            try
            {
                List<DTOTriggerTOMaster> result = new List<DTOTriggerTOMaster>();
                using (var model = new DataEntities())
                {
                    var lstCOTO = model.OPS_COTOMaster.Where(c => c.VehicleID > 0 && c.DriverName1 != null && c.DriverTel1 != null && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterTendered && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterInvoice).Select(c => new DTOTriggerTOMaster
                        {
                            DomainName = string.Empty,
                            COTOMasterID = c.ID,
                            COTOMasterCode = c.Code,
                            DITOMasterID = -1,
                            DITOMasterCode = string.Empty,
                            DNCode = string.Empty,
                            SOCode = string.Empty,
                            DriverName = c.DriverName1,
                            ETA = c.ETA,
                            ETD = c.ETD,
                            VehicleID = c.VehicleID,
                            VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                            TelNo = c.DriverTel1,
                            OrderID = 0,
                            OrderCode = string.Empty
                        }).ToList();
                    result.AddRange(lstCOTO);
                    var lstDITO = model.OPS_DITOGroupProduct.Where(c => c.OPS_DITOMaster.VehicleID > 0 && c.OPS_DITOMaster.DriverTel1 != null && c.DITOMasterID.HasValue && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered && c.OPS_DITOMaster.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterInvoice).Select(c => new DTOTriggerTOMaster
                    {
                        DomainName = string.Empty,
                        COTOMasterID = -1,
                        COTOMasterCode = string.Empty,
                        DITOMasterID = c.DITOMasterID,
                        DITOMasterCode = c.OPS_DITOMaster.Code,
                        DNCode = c.DNCode,
                        SOCode = c.ORD_GroupProduct.SOCode != null ? c.ORD_GroupProduct.SOCode : string.Empty,
                        DriverName = c.OPS_DITOMaster.DriverName1 != null ? c.OPS_DITOMaster.DriverName1 : "",
                        ETA = c.OPS_DITOMaster.ETA,
                        ETD = c.OPS_DITOMaster.ETD,
                        VehicleID = c.OPS_DITOMaster.VehicleID,
                        VehicleCode = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                        TelNo = c.OPS_DITOMaster.DriverTel1,
                        OrderID = c.ORD_GroupProduct.OrderID,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code
                    }).ToList();
                    result.AddRange(lstDITO);
                }
                return result;
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

        public void SMSTOMaster_Update(DTOTriggerTOMaster item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    if (item.COTOMasterID > 0)
                    {
                        var obj = model.OPS_COTOMaster.FirstOrDefault(c => c.ID == item.COTOMasterID.Value);
                        if (obj != null)
                        {
                            obj.StatusOfCOTOMasterID = -(int)SYSVarType.StatusOfCOTOMasterReceived;
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                            model.SaveChanges();
                        }
                    }
                    if (item.DITOMasterID > 0)
                    {
                        var obj = model.OPS_DITOMaster.FirstOrDefault(c => c.ID == item.DITOMasterID.Value);
                        if (obj != null)
                        {
                            // Lấy ds Order trùng số DN và status chưa hoàn tất
                            var lstGroup = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.ID && c.DNCode.ToLower() == item.DNCode.ToLower() && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusComplete);
                            // Ds location cần cập nhật status
                            var lstLocationID = new List<int>();
                            DateTime dtNow = DateTime.Now;
                            // Cập nhật cho OPSDITOGroupProduct
                            foreach (var objGroup in lstGroup)
                            {
                                objGroup.ModifiedBy = Account.UserName;
                                objGroup.ModifiedDate = DateTime.Now;
                                objGroup.DateToLeave = dtNow;
                                objGroup.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                                objGroup.Note = item.Note;
                                lstLocationID.Add(objGroup.ORD_GroupProduct.CUS_Location1.LocationID);
                            }
                            // Cập nhật cho OPSDITOLocation
                            lstLocationID = lstLocationID.Distinct().ToList();
                            var lstLocation = model.OPS_DITOLocation.Where(c => c.DITOMasterID == obj.ID && c.LocationID.HasValue && lstLocationID.Contains(c.LocationID.Value));
                            foreach (var location in lstLocation)
                            {
                                location.ModifiedBy = Account.UserName;
                                location.ModifiedDate = DateTime.Now;
                                location.DateLeave = dtNow;
                                location.DITOLocationStatusID = -(int)SYSVarType.DITOLocationStatusLeave;
                            }
                            model.SaveChanges();
                            // Ktra hoàn tất lệnh hay ko
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                            var lstDN = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.ID);
                            if (lstDN.Count() == lstDN.Count(c => c.DITOGroupProductStatusID == -(int)SYSVarType.DITOGroupProductStatusComplete))
                                obj.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterReceived;
                            else
                                obj.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterDelivery;
                            //var lstLocationCheck = model.OPS_DITOLocation.Where(c => c.DITOMasterID == obj.ID && c.SortOrder != 1);
                            //if (lstLocationCheck.Count() == lstLocationCheck.Count(c => c.DateLeave.HasValue))
                            //    obj.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterReceived;
                            //else
                            //    obj.StatusOfDITOMasterID = -(int)SYSVarType.StatusOfDITOMasterDelivery;
                            model.SaveChanges();
                        }
                    }
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

        #region Barcode reader
        public List<DTOTriggerORDGroup> BarcodeSO_List(string SOCode, string DNCode, int? SysCustomerID)
        {
            try
            {
                List<DTOTriggerORDGroup> result = new List<DTOTriggerORDGroup>();
                using (var model = new DataEntities())
                {
                    if (SysCustomerID == null)
                        SysCustomerID = 3;
                    var query = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == SysCustomerID && c.ORD_GroupProduct.SOCode.ToLower() == SOCode.ToLower() && c.DNCode.ToLower() == DNCode.ToLower()).Select(c => new DTOTriggerORDGroup
                        {
                            DITOGroupProductID = c.ID,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            SOCode = c.ORD_GroupProduct.SOCode,
                            Address = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                            Ton = c.Ton,
                            DNCode = c.DNCode,
                            IsNew = false
                        }).ToList();
                    if (query.Count > 0)
                        result.AddRange(query);
                    else
                    {
                        query = model.OPS_DITOGroupProduct.Where(c => c.ORD_GroupProduct.ORD_Order.SYSCustomerID == SysCustomerID && c.ORD_GroupProduct.SOCode.ToLower() == SOCode.ToLower() && string.IsNullOrEmpty(c.DNCode)).Select(c => new DTOTriggerORDGroup
                        {
                            DITOGroupProductID = c.ID,
                            OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                            SOCode = c.ORD_GroupProduct.SOCode,
                            Address = c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address,
                            Ton = c.Ton,
                            DNCode = string.Empty,
                            IsNew = false
                        }).ToList();
                        if (query.Count > 0)
                            result.AddRange(query);
                        else
                            return null;
                    }
                }
                return result;
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

        public void BarcodeSO_Update(List<DTOTriggerORDGroup> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var group in lst.GroupBy(c => c.DITOGroupProductID))
                    {
                        // 1 SO chỉ có 1 DN
                        if (group.Count() == 1)
                        {
                            var item = group.FirstOrDefault();
                            var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.DITOGroupProductID);
                            if (obj != null)
                            {
                                obj.ModifiedBy = Account.UserName;
                                obj.ModifiedDate = DateTime.Now;
                                obj.DNCode = item.DNCode;
                                obj.Ton = obj.TonTranfer = obj.TonBBGN = item.Ton;
                            }
                        }
                        else
                        {
                            foreach (var item in group)
                            {
                                if (!item.IsNew)
                                {
                                    var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.DITOGroupProductID);
                                    if (obj != null)
                                    {
                                        obj.ModifiedBy = Account.UserName;
                                        obj.ModifiedDate = DateTime.Now;
                                        obj.DNCode = item.DNCode;
                                        obj.Ton = obj.TonTranfer = obj.TonBBGN = item.Ton;
                                        var exchange = model.ORD_Product.FirstOrDefault(c => c.GroupProductID == obj.OrderGroupProductID);
                                        if (exchange != null)
                                        {
                                            obj.CBM = obj.CBMTranfer = obj.CBMBBGN = exchange.ExchangeTon > 0 && exchange.ExchangeCBM.HasValue ? exchange.ExchangeCBM.Value * item.Ton / exchange.ExchangeTon.Value : 0;
                                            obj.Quantity = obj.QuantityTranfer = obj.QuantityBBGN = exchange.ExchangeTon > 0 && exchange.ExchangeQuantity.HasValue ? exchange.ExchangeQuantity.Value * item.Ton / exchange.ExchangeTon.Value : 0;
                                        }
                                    }
                                }
                                else
                                {
                                    var parent = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == item.DITOGroupProductID);
                                    if (parent != null)
                                    {
                                        var exchange = model.ORD_Product.FirstOrDefault(c => c.GroupProductID == parent.OrderGroupProductID);
                                        if (exchange != null)
                                        {
                                            OPS_DITOGroupProduct obj = new OPS_DITOGroupProduct();
                                            obj.CreatedBy = Account.UserName;
                                            obj.CreatedDate = DateTime.Now;
                                            obj.DNCode = item.DNCode;
                                            obj.Ton = obj.TonTranfer = obj.TonBBGN = item.Ton;
                                            obj.CBM = obj.CBMTranfer = obj.CBMBBGN = exchange.ExchangeTon > 0 && exchange.ExchangeCBM.HasValue ? exchange.ExchangeCBM.Value * item.Ton / exchange.ExchangeTon.Value : 0;
                                            obj.Quantity = obj.QuantityTranfer = obj.QuantityBBGN = exchange.ExchangeTon > 0 && exchange.ExchangeQuantity.HasValue ? exchange.ExchangeQuantity.Value * item.Ton / exchange.ExchangeTon.Value : 0;
                                            obj.DateFromCome = parent.DateFromCome;
                                            obj.DateFromLeave = parent.DateFromLeave;
                                            obj.DateFromLoadEnd = parent.DateFromLoadEnd;
                                            obj.DateFromLoadStart = parent.DateFromLoadStart;
                                            obj.DateToCome = parent.DateToCome;
                                            obj.DateToLeave = parent.DateToLeave;
                                            obj.DateToLoadEnd = parent.DateToLoadEnd;
                                            obj.DateToLoadStart = parent.DateToLoadStart;
                                            obj.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusWaiting;
                                            obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODWait;
                                            obj.GroupSort = parent.ORD_GroupProduct.OrderID + "_" + parent.ORD_GroupProduct.CUSRoutingID.Value + "_" + parent.OrderGroupProductID;
                                            obj.Note = parent.Note1;
                                            obj.OrderGroupProductID = parent.OrderGroupProductID;
                                            model.OPS_DITOGroupProduct.Add(obj);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    model.SaveChanges();
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

        public List<DTOBarCodeGroup> PODBarcodeGroup_List(string DNCode, int? SysCustomerID)
        {
            try
            {
                List<DTOBarCodeGroup> result = new List<DTOBarCodeGroup>();
                using (var model = new DataEntities())
                {
                    if (SysCustomerID == null)
                        SysCustomerID = 3;

                    var query = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID.HasValue && c.OPS_DITOMaster.SYSCustomerID == SysCustomerID && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterTendered && c.DNCode.ToLower() == DNCode.ToLower()).Select(c => new DTOBarCodeGroup
                    {
                        ID = c.ID,
                        DriverName = c.OPS_DITOMaster.DriverName1 == null ? string.Empty : c.OPS_DITOMaster.DriverName1,
                        IsInvoice = c.DITOGroupProductStatusPODID == -(int)SYSVarType.DITOGroupProductStatusPODComplete,
                        RegNo = c.OPS_DITOMaster.VehicleID.HasValue ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : string.Empty,
                    }).ToList();
                    result.AddRange(query);
                }
                return result;
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

        public DTOBarCodeGroup PODBarcodeGroup_Save(int ID, string lit, string kg)
        {
            try
            {
                DTOBarCodeGroup item = new DTOBarCodeGroup();
                item.ID = ID;
                using (var model = new DataEntities())
                {
                    var obj = model.OPS_DITOGroupProduct.FirstOrDefault(c => c.ID == ID);
                    if (obj != null)
                    {
                        obj.ModifiedBy = Account.UserName;
                        obj.ModifiedDate = DateTime.Now;
                        obj.DITOGroupProductStatusID = -(int)SYSVarType.DITOGroupProductStatusComplete;
                        obj.DITOGroupProductStatusPODID = -(int)SYSVarType.DITOGroupProductStatusPODComplete;
                        obj.InvoiceBy = Account.UserName;
                        obj.InvoiceDate = DateTime.Now;
                        obj.Note1 = kg;
                        obj.Note2 = lit;
                    }
                    model.SaveChanges();
                    item.IsInvoice = true;
                }
                return item;
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

        #region Work Flow
        public List<DTOTriggerMessage> MessageCall()
        {
            try
            {
                var result = new List<DTOTriggerMessage>();
                using (var model = new DataEntities())
                {
                    var lstmsg = model.WFL_Message.Where(c => c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_Action.UserID.HasValue).Select(c => new { c.WFL_Action.UserID, c.ID }).ToList();
                    var lstid = model.WFL_Message.Where(c => c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_Action.UserID > 0).Select(c => c.WFL_Action.UserID.Value).Distinct().ToArray();
                    foreach (var id in lstid)
                    {
                        var obj = new DTOTriggerMessage();
                        obj.UserID = id;
                        obj.Total = lstmsg.Where(c => c.UserID == id).Count();
                        result.Add(obj);
                    }
                }
                return result;
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

        public List<DTOTriggerMessage> MessageCall_User()
        {
            try
            {
                var result = new List<DTOTriggerMessage>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_Message.Where(c => c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_Action.UserID == Account.UserID).OrderByDescending(c => c.CreatedDate).Select(c => new DTOTriggerMessage
                    {
                        ID = c.ID,
                        EventCode = c.WFL_Action.WFL_Event.Code,
                        Message = c.Message,
                        UserID = c.WFL_Action.UserID.Value
                    }).ToList();
                }
                return result;
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

        public void MessageCall_Sended(List<long> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var item in lstid)
                    {
                        var message = model.WFL_Message.FirstOrDefault(c => c.ID == item);
                        if (message != null)
                            message.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageSended;
                    }
                    model.SaveChanges();
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

        public DTONotificationUser MessageCall_LoadMore(int currentPage, int pageSize, string typeOfMessage)
        {
            try
            {
                var result = new DTONotificationUser();
                result.ListMessage = new List<DTONotification>();
                result.CurrentPage = currentPage;
                using (var model = new DataEntities())
                {
                    if (result.CurrentPage <= 0 || model.WFL_Message.Count(c => c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.WFL_Action.UserID == Account.UserID && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait) > 0)
                        result.CurrentPage = 1;
                    bool IsAll = typeOfMessage == "All";
                    result.ListMessage = model.WFL_Message.Where(c => c.StatusOfMessageID >= -(int)SYSVarType.StatusOfMessageWait && c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.WFL_Action.UserID == Account.UserID && (IsAll ? true : c.WFL_Action.WFL_Event.Code.StartsWith(typeOfMessage))).OrderByDescending(c => c.CreatedDate).Select(c => new DTONotification
                    {
                        ID = c.ID,
                        EventCode = c.WFL_Action.WFL_Event.Code,
                        Message = c.Message,
                        UserID = c.WFL_Action.UserID.Value,
                        IsUnRead = c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageNotified,
                        CreatedDate = c.CreatedDate
                    }).Skip((result.CurrentPage - 1) * pageSize).Take(pageSize).ToList();
                }
                return result;
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

        public void MessageCall_Read()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var item in model.WFL_Message.Where(c => c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.WFL_Action.UserID == Account.UserID && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait))
                    {
                        item.ModifiedBy = Account.UserName;
                        item.ModifiedDate = DateTime.Now;
                        item.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageSended;
                    }
                    model.SaveChanges();
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

        #region Work Flow New
        public List<DTOTriggerMessage> WFL_MessageCall()
        {
            try
            {
                var result = new List<DTOTriggerMessage>();
                using (var model = new DataEntities())
                {
                    var lstmsg = model.WFL_DefineWFMessage.Where(c => c.WFL_DefineWFAction.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_DefineWFAction.UserID.HasValue).Select(c => new { c.WFL_DefineWFAction.UserID, c.ID }).ToList();
                    var lstid = model.WFL_DefineWFMessage.Where(c => c.WFL_DefineWFAction.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_DefineWFAction.UserID > 0).Select(c => c.WFL_DefineWFAction.UserID.Value).Distinct().ToArray();
                    foreach (var id in lstid)
                    {
                        var obj = new DTOTriggerMessage();
                        obj.UserID = id;
                        obj.Total = lstmsg.Where(c => c.UserID == id).Count();
                        result.Add(obj);
                    }
                }
                return result;
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

        public List<DTOTriggerMessage> WFL_MessageCall_User()
        {
            try
            {
                var result = new List<DTOTriggerMessage>();
                using (var model = new DataEntities())
                {
                    result = model.WFL_DefineWFMessage.Where(c => c.WFL_DefineWFAction.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_DefineWFAction.UserID == Account.UserID).OrderByDescending(c => c.CreatedDate).Select(c => new DTOTriggerMessage
                    {
                        ID = c.ID,
                        EventCode = c.WFL_DefineWFAction.WFL_DefineWFEvent.WFL_WFEvent.Code,
                        Message = c.Message,
                        UserID = c.WFL_DefineWFAction.UserID.Value
                    }).ToList();
                }
                return result;
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

        public void WFL_MessageCall_Sended(List<long> lstid)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var item in lstid)
                    {
                        var message = model.WFL_DefineWFMessage.FirstOrDefault(c => c.ID == item);
                        if (message != null)
                            message.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageSended;
                    }
                    model.SaveChanges();
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

        public DTONotificationUser WFL_MessageCall_LoadMore(int currentPage, int pageSize, string typeOfMessage)
        {
            try
            {
                var result = new DTONotificationUser();
                result.ListMessage = new List<DTONotification>();
                result.CurrentPage = currentPage;
                using (var model = new DataEntities())
                {
                    if (result.CurrentPage <= 0 || model.WFL_Message.Count(c => c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.WFL_Action.UserID == Account.UserID && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait) > 0)
                        result.CurrentPage = 1;
                    bool IsAll = typeOfMessage == "All";
                    result.ListMessage = model.WFL_DefineWFMessage.Where(c => c.StatusOfMessageID >= -(int)SYSVarType.StatusOfMessageWait && c.WFL_DefineWFAction.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.WFL_DefineWFAction.UserID == Account.UserID && (IsAll ? true : c.WFL_DefineWFAction.WFL_DefineWFEvent.WFL_WFEvent.Code.StartsWith(typeOfMessage))).OrderByDescending(c => c.CreatedDate).Select(c => new DTONotification
                    {
                        ID = c.ID,
                        EventCode = c.WFL_DefineWFAction.WFL_DefineWFEvent.WFL_WFEvent.Code,
                        Message = c.Message,
                        UserID = c.WFL_DefineWFAction.UserID.Value,
                        IsUnRead = c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageNotified,
                        CreatedDate = c.CreatedDate
                    }).Skip((result.CurrentPage - 1) * pageSize).Take(pageSize).ToList();
                }
                return result;
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

        public void WFL_MessageCall_Read()
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var item in model.WFL_DefineWFMessage.Where(c => c.WFL_DefineWFAction.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS && c.WFL_DefineWFAction.UserID == Account.UserID && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait))
                    {
                        item.ModifiedBy = Account.UserName;
                        item.ModifiedDate = DateTime.Now;
                        item.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageSended;
                    }
                    model.SaveChanges();
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

        #region Packet
        public DTOTriggerWFLPacket Packet_Check()
        {
            try
            {
                DTOTriggerWFLPacket result = new DTOTriggerWFLPacket();
                result.ListPacket = new List<DTOTriggerPacket>();
                result.ListPacketAction = new List<DTOTriggerPacketAction>();
                result.ListPacketDriver = new List<DTOTriggerPacketDriver>();
                result.ListOPSGroup = new List<DTOTriggerOPSGroup>();
                result.ListMaster = new List<DTOTriggerMaster>();
                using (var model = new DataEntities())
                {
                    #region Tạo Packet tự động
                    DateTime dtFrom = DateTime.Now.Date;
                    DateTime dtTo = DateTime.Now.Date.AddDays(1);
                    DateTime dtNow = DateTime.Now;
                    var lstPacket = model.WFL_Packet.Where(c => c.PacketDate >= dtFrom && c.PacketDate < dtTo).Select(c => new
                    {
                        c.PacketSettingID,
                        c.PacketDate,
                        c.PacketProcessID
                    }).ToList();

                    List<WFL_Packet> lstPacketTemp = new List<WFL_Packet>();

                    foreach (var item in model.WFL_PacketSetting.Where(c => c.IsAutoCollect && c.IsApproved && (c.TimeSend1.HasValue || c.TimeSend2.HasValue)))
                    {
                        bool flag = false;
                        if (item.TimeSend1.HasValue)
                        {
                            // KTra xem packet đã đc tạo chưa
                            DateTime PacketDate = dtNow.Date.AddHours(item.TimeSend1.Value.Hour).AddMinutes(item.TimeSend1.Value.Minute);
                            var packet = lstPacket.FirstOrDefault(c => c.PacketSettingID == item.ID && c.PacketDate == PacketDate);
                            if (packet == null)
                            {
                                // Tạo packet
                                WFL_Packet obj = new WFL_Packet();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PacketSettingID = item.ID;
                                obj.PacketDate = PacketDate;
                                obj.TimeSendOffer = PacketDate;
                                obj.PacketProcessID = -(int)SYSVarType.PacketProcessOpen;
                                model.WFL_Packet.Add(obj);
                                lstPacketTemp.Add(obj);
                            }
                            else
                                flag = packet.PacketProcessID == -(int)SYSVarType.PacketProcessSend;
                        }

                        if (flag && item.TimeSend2.HasValue)
                        {
                            DateTime PacketDate = dtNow.Date.AddHours(item.TimeSend2.Value.Hour).AddMinutes(item.TimeSend2.Value.Minute);
                            var packet = lstPacket.FirstOrDefault(c => c.PacketSettingID == item.ID && c.PacketDate == PacketDate);
                            if (packet == null)
                            {
                                WFL_Packet obj = new WFL_Packet();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.PacketSettingID = item.ID;
                                obj.PacketDate = PacketDate;
                                obj.TimeSendOffer = PacketDate;
                                obj.PacketProcessID = -(int)SYSVarType.PacketProcessOpen;
                                model.WFL_Packet.Add(obj);
                                lstPacketTemp.Add(obj);
                            }
                        }
                    }

                    model.SaveChanges();

                    #endregion

                    #region Tạo detail cho Packet
                    var lstPacketAutoCollect = model.WFL_Packet.Where(c => c.PacketProcessID == -(int)SYSVarType.PacketProcessOpen && c.WFL_PacketSetting.IsAutoCollect && c.WFL_PacketSetting.IsApproved);
                    // Xóa detail của các packet này
                    foreach (var item in lstPacketAutoCollect)
                    {
                        foreach (var detail in model.WFL_PacketDetail.Where(c => c.PacketID == item.ID))
                            model.WFL_PacketDetail.Remove(detail);

                        foreach (var detail in model.WFL_PacketAction.Where(c => c.PacketID == item.ID))
                            model.WFL_PacketAction.Remove(detail);

                        foreach (var detail in model.WFL_PacketDriver.Where(c => c.PacketID == item.ID))
                            model.WFL_PacketDriver.Remove(detail);
                    }
                    model.SaveChanges();

                    foreach (var item in lstPacketAutoCollect)
                    {
                        int PacketSettingTypeID = item.WFL_PacketSetting.PacketSettingTypeID;
                        int? CustomerID = item.WFL_PacketSetting.CustomerID;
                        int SYSCustomerID = item.WFL_PacketSetting.SYSCustomerID;
                        // Lấy tất cả đơn
                        if (PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeOrderConfirmAll)
                        {
                            // Lấy đơn đã lập kế hoạch
                            var lstOrderGroupID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterApproved && c.OPS_DITOMaster.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered
                                && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == SYSCustomerID && (CustomerID.HasValue ? c.ORD_GroupProduct.ORD_Order.CustomerID == CustomerID : true)).Select(c => c.OrderGroupProductID).Distinct().ToList();
                            // Lấy đơn chưa lập kế hoạch và ko bị hủy
                            lstOrderGroupID.AddRange(model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == null && c.DITOGroupProductStatusID != -(int)SYSVarType.DITOGroupProductStatusCancel
                                && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == SYSCustomerID && (CustomerID.HasValue ? c.ORD_GroupProduct.ORD_Order.CustomerID == CustomerID : true)).Select(c => c.OrderGroupProductID).Distinct().ToList());
                            // Distinct
                            lstOrderGroupID = lstOrderGroupID.Distinct().ToList();
                            foreach (var orderGroupID in lstOrderGroupID)
                            {
                                WFL_PacketDetail obj = new WFL_PacketDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.ORDGroupProductID = orderGroupID;
                                obj.PacketID = item.ID;
                                model.WFL_PacketDetail.Add(obj);
                            }
                        }

                        // Lấy đơn hàng đã được tạo chuyến
                        if (PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeOrderConfirmPlan)
                        {
                            var lstOrderGroupID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID > 0 && c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterApproved && c.OPS_DITOMaster.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered && c.OPS_DITOMaster.VehicleID > 0
                                && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == SYSCustomerID && (CustomerID.HasValue ? c.ORD_GroupProduct.ORD_Order.CustomerID == CustomerID : true)).Select(c => c.OrderGroupProductID).Distinct().ToList();
                            foreach (var orderGroupID in lstOrderGroupID)
                            {
                                WFL_PacketDetail obj = new WFL_PacketDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.ORDGroupProductID = orderGroupID;
                                obj.PacketID = item.ID;
                                model.WFL_PacketDetail.Add(obj);
                            }
                        }

                        // Đổi chuyến vận chuyển
                        if (PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeTOMasterChange)
                        {

                        }

                        // Xác nhận chuyến tài xế
                        if (PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeTOMasterOwner)
                        {
                            var lstMasterID = model.OPS_DITOMaster.Where(c => c.SYSCustomerID == SYSCustomerID && c.DriverID1 > 0 && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == SYSCustomerID) && c.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterApproved && c.StatusOfDITOMasterID < -(int)SYSVarType.StatusOfDITOMasterTendered).Select(c => c.ID).ToList();
                            foreach (var masterID in lstMasterID)
                            {
                                WFL_PacketDetail obj = new WFL_PacketDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.DITOMasterID = masterID;
                                obj.PacketID = item.ID;
                                model.WFL_PacketDetail.Add(obj);
                            }

                            var lstMasterCOID = model.OPS_COTOMaster.Where(c => c.SYSCustomerID == SYSCustomerID && c.DriverID1 > 0 && (c.VendorOfVehicleID == null || c.VendorOfVehicleID == SYSCustomerID) && c.StatusOfCOTOMasterID >= -(int)SYSVarType.StatusOfCOTOMasterApproved && c.StatusOfCOTOMasterID < -(int)SYSVarType.StatusOfCOTOMasterTendered).Select(c => c.ID).ToList();
                            foreach (var masterID in lstMasterCOID)
                            {
                                WFL_PacketDetail obj = new WFL_PacketDetail();
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                obj.COTOMasterID = masterID;
                                obj.PacketID = item.ID;
                                model.WFL_PacketDetail.Add(obj);
                            }
                        }

                        // Sự kiện
                        if (PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeEvent)
                        {

                        }
                    }
                    model.SaveChanges();
                    #endregion

                    #region Cập nhật status Close cho các packet thiết lập AutoSend
                    var lstPacketAutoSend = model.WFL_Packet.Where(c => c.PacketProcessID == -(int)SYSVarType.PacketProcessOpen && c.WFL_PacketSetting.IsAutoSend && c.TimeSendOffer < dtNow && c.WFL_PacketSetting.IsApproved);
                    foreach (var item in lstPacketAutoSend)
                    {
                        item.PacketProcessID = -(int)SYSVarType.PacketProcessClose;
                        item.ModifiedBy = Account.UserName;
                        item.ModifiedDate = DateTime.Now;
                    }
                    model.SaveChanges();
                    #endregion

                    #region Tạo action cho các packet chưa send và có PacketDate <= Ngày giờ hiện tại
                    var lstPacketActive = model.WFL_Packet.Where(c => c.PacketProcessID == -(int)SYSVarType.PacketProcessClose && c.TimeSendOffer <= dtNow && c.WFL_PacketSetting.PacketSettingTypeID != -(int)SYSVarType.PacketSettingTypeEvent && c.WFL_PacketAction.Count == 0 && c.WFL_PacketDriver.Count == 0 && c.WFL_PacketSetting.IsApproved).Select(c => new DTOTriggerPacket
                    {
                        ID = c.ID,
                        PacketSettingID = c.PacketSettingID,
                        PacketDate = c.PacketDate,
                        PacketSettingTypeID = c.WFL_PacketSetting.PacketSettingTypeID,
                        IsDriverMail = c.WFL_PacketSetting.IsDriverMail,
                        IsDriverSMS = c.WFL_PacketSetting.IsDriverSMS,
                        DriverMailTemplateID = c.WFL_PacketSetting.DriverMailTemplateID,
                        DriverMailTemplate = c.WFL_PacketSetting.DriverMailTemplateID > 0 ? c.WFL_PacketSetting.WFL_Template.Template : "",
                        DriverMailFileID = c.WFL_PacketSetting.DriverMailTemplateID > 0 ? c.WFL_PacketSetting.WFL_Template.FileID : -1,
                        DriverMailSubject = c.WFL_PacketSetting.DriverMailTemplateID > 0 ? c.WFL_PacketSetting.WFL_Template.Subject : "",
                        DriverMailFileName = c.WFL_PacketSetting.DriverMailTemplateID > 0 && c.WFL_PacketSetting.WFL_Template.FileID > 0 ? c.WFL_PacketSetting.WFL_Template.CAT_File.FileName : "",
                        DriverMailFileExt = c.WFL_PacketSetting.DriverMailTemplateID > 0 && c.WFL_PacketSetting.WFL_Template.FileID > 0 ? c.WFL_PacketSetting.WFL_Template.CAT_File.FileExt : "",
                        DriverMailFilePath = c.WFL_PacketSetting.DriverMailTemplateID > 0 && c.WFL_PacketSetting.WFL_Template.FileID > 0 ? c.WFL_PacketSetting.WFL_Template.CAT_File.FilePath : "",

                        DriverSMSTemplateID = c.WFL_PacketSetting.DriverSMSTemplateID,
                        DriverSMSTemplate = c.WFL_PacketSetting.DriverSMSTemplateID > 0 ? c.WFL_PacketSetting.WFL_Template1.Template : "",
                        DriverSMSFileID = c.WFL_PacketSetting.DriverSMSTemplateID > 0 ? c.WFL_PacketSetting.WFL_Template1.FileID : -1,
                        DriverSMSSubject = c.WFL_PacketSetting.DriverSMSTemplateID > 0 ? c.WFL_PacketSetting.WFL_Template1.Subject : "",
                        DriverSMSFileName = c.WFL_PacketSetting.DriverSMSTemplateID > 0 && c.WFL_PacketSetting.WFL_Template1.FileID > 0 ? c.WFL_PacketSetting.WFL_Template1.CAT_File.FileName : "",
                        DriverSMSFileExt = c.WFL_PacketSetting.DriverSMSTemplateID > 0 && c.WFL_PacketSetting.WFL_Template1.FileID > 0 ? c.WFL_PacketSetting.WFL_Template1.CAT_File.FileExt : "",
                        DriverSMSFilePath = c.WFL_PacketSetting.DriverSMSTemplateID > 0 && c.WFL_PacketSetting.WFL_Template1.FileID > 0 ? c.WFL_PacketSetting.WFL_Template1.CAT_File.FilePath : "",

                        Setting = c.WFL_PacketSetting.CUSSettingID > 0 ? c.WFL_PacketSetting.CUS_Setting.Setting : "",
                        CustomerID = c.WFL_PacketSetting.CustomerID,
                        CustomerName = c.WFL_PacketSetting.CustomerID > 0 ? c.WFL_PacketSetting.CUS_Customer.CustomerName : "",
                    });

                    var lstPacketSettingID = lstPacketActive.Select(c => c.PacketSettingID).Distinct().ToList();

                    var lstPacketSettingAction = model.WFL_PacketSettingAction.Where(c => lstPacketSettingID.Contains(c.PacketSettingID) && c.IsUse).Select(c => new
                    {
                        c.ID,
                        c.UserID,
                        c.TypeOfActionID,
                        c.SYS_User.Email,
                        c.PacketSettingID
                    }).ToList();

                    var lstPacketSettingTemplate = model.WFL_PacketSettingTemplate.Where(c => lstPacketSettingID.Contains(c.PacketSettingID)).Select(c => new
                    {
                        c.PacketSettingID,
                        c.TypeOfActionID,
                        c.TemplateID,
                        c.WFL_Template.Subject,
                        c.WFL_Template.Template,
                        c.WFL_Template.FileID,
                        FilePath = c.WFL_Template.FileID > 0 ? c.WFL_Template.CAT_File.FilePath : "",
                        FileName = c.WFL_Template.FileID > 0 ? c.WFL_Template.CAT_File.FileName : "",
                        FileExt = c.WFL_Template.FileID > 0 ? c.WFL_Template.CAT_File.FileExt : "",
                    }).ToList();

                    var lstPacketActiveID = lstPacketActive.Select(c => c.ID).ToList();
                    var lstOrderGroup = model.WFL_PacketDetail.Where(c => lstPacketActiveID.Contains(c.PacketID) && c.ORDGroupProductID > 0).Select(c => new { c.PacketID, c.ORDGroupProductID }).Distinct().ToList();
                    var lstDIMaster = model.WFL_PacketDetail.Where(c => lstPacketActiveID.Contains(c.PacketID) && c.DITOMasterID > 0).Select(c => new { c.PacketID, c.DITOMasterID }).Distinct().ToList();
                    var lstCOMaster = model.WFL_PacketDetail.Where(c => lstPacketActiveID.Contains(c.PacketID) && c.COTOMasterID > 0).Select(c => new { c.PacketID, c.COTOMasterID }).Distinct().ToList();

                    foreach (var item in lstPacketActive)
                    {
                        result.ListPacket.Add(item);

                        var lstSettingAction = lstPacketSettingAction.Where(c => c.PacketSettingID == item.PacketSettingID).Select(c => new
                        {
                            c.ID,
                            c.UserID,
                            c.TypeOfActionID,
                            c.Email,
                        }).ToList();

                        var lstSettingTemplate = lstPacketSettingTemplate.Where(c => c.PacketSettingID == item.PacketSettingID).Select(c => new
                        {
                            c.TypeOfActionID,
                            c.TemplateID,
                            c.Subject,
                            c.Template,
                            c.FileID,
                            c.FileName,
                            c.FilePath,
                            c.FileExt
                        }).ToList();

                        // Lấy data detail khi có thiết lập gửi
                        if (lstSettingAction.Count > 0 || (item.IsDriverMail && item.DriverMailTemplateID.HasValue) || (item.IsDriverSMS && item.DriverSMSTemplateID.HasValue))
                        {
                            List<DTOTriggerOPSGroup> lstOPSGroup = new List<DTOTriggerOPSGroup>();
                            List<DTOTriggerMaster> lstMaster = new List<DTOTriggerMaster>();

                            #region Đơn hàng
                            if (item.PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeOrderConfirmAll || item.PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeOrderConfirmPlan)
                            {
                                var lstOrderGroupID = lstOrderGroup.Where(c => c.PacketID == item.ID).Select(c => c.ORDGroupProductID.Value).ToList();
                                lstOPSGroup = model.OPS_DITOGroupProduct.Where(c => c.OrderGroupProductID.HasValue && lstOrderGroupID.Contains(c.OrderGroupProductID.Value)).Select(c => new DTOTriggerOPSGroup
                                {
                                    PacketID = item.ID,
                                    TOMasterID = c.DITOMasterID,
                                    TOMasterCode = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.Code : "",
                                    TotalLocation = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.TotalLocation : 1,
                                    DITOGroupProductID = c.ID,
                                    OrderID = c.ORD_GroupProduct.OrderID,
                                    OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                                    DNCode = c.DNCode != null ? c.DNCode : "",
                                    SOCode = c.ORD_GroupProduct.SOCode == null ? c.ORD_GroupProduct.SOCode : "",
                                    DateConfig = c.ORD_GroupProduct.DateConfig,
                                    RequestDate = c.ORD_GroupProduct.ORD_Order.RequestDate,
                                    StockID = c.ORD_GroupProduct.LocationFromID,
                                    StockCode = c.ORD_GroupProduct.CUS_Location.Code,
                                    StockName = c.ORD_GroupProduct.CUS_Location.LocationName,
                                    StockAddress = c.ORD_GroupProduct.CUS_Location.CAT_Location.Address,
                                    PartnerID = c.ORD_GroupProduct.PartnerID,
                                    PartnerCode = c.ORD_GroupProduct.PartnerID > 0 ? c.ORD_GroupProduct.CUS_Partner.PartnerCode : "",
                                    PartnerName = c.ORD_GroupProduct.PartnerID > 0 ? c.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName : "",
                                    PartnerCodeName = c.ORD_GroupProduct.PartnerID > 0 ? c.ORD_GroupProduct.CUS_Partner.PartnerCode + "-" + c.ORD_GroupProduct.CUS_Partner.CAT_Partner.PartnerName : string.Empty,
                                    Address = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : "",
                                    LocationToProvince = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_Province.ProvinceName : "",
                                    LocationToDistrict = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.CAT_District.DistrictName : "",

                                    CUSRoutingID = c.ORD_GroupProduct.CUSRoutingID,
                                    CUSRoutingCode = c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.Code : "",
                                    CUSRoutingName = c.ORD_GroupProduct.CUSRoutingID > 0 ? c.ORD_GroupProduct.CUS_Routing.CAT_Routing.RoutingName : "",
                                    OrderGroupProductID = c.OrderGroupProductID.Value,
                                    GroupOfProductID = c.ORD_GroupProduct.GroupOfProductID,
                                    GroupOfProductCode = c.ORD_GroupProduct.CUS_GroupOfProduct.Code,
                                    GroupOfProductName = c.ORD_GroupProduct.CUS_GroupOfProduct.GroupName,
                                    Description = c.ORD_GroupProduct.Description,

                                    DriverName = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverName1 : string.Empty,
                                    TelNo = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.DriverTel1 : string.Empty,
                                    DrivingLicense = string.Empty,

                                    VehicleID = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.VehicleID : null,
                                    VehicleCode = c.DITOMasterID.HasValue && c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                                    VendorID = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.VendorOfVehicleID : null,
                                    VendorCode = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOMaster.CUS_Customer.Code : "Xe nhà" : "",
                                    VendorName = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.VendorOfVehicleID > 0 ? c.OPS_DITOMaster.CUS_Customer.CustomerName : "Xe nhà" : "",
                                    CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                                    CustomerCode = c.ORD_GroupProduct.ORD_Order.CUS_Customer.Code,
                                    CustomerName = c.ORD_GroupProduct.ORD_Order.CUS_Customer.CustomerName,

                                    TonTranfer = c.TonTranfer,
                                    CBMTranfer = c.CBMTranfer,
                                    QuantityTranfer = c.QuantityTranfer,
                                    TonBBGN = c.TonBBGN,
                                    CBMBBGN = c.CBMBBGN,
                                    QuantityBBGN = c.QuantityBBGN,

                                    KM = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.KM : null,
                                    KMStart = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.KMStart : null,
                                    KMEnd = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.KMEnd : null,
                                    ETD = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ETD : null,
                                    ETA = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.ETA : null,

                                    TonReturn = c.TonReturn,
                                    CBMReturn = c.CBMReturn,
                                    QuantityReturn = c.QuantityReturn,

                                    KgTranfer = c.TonTranfer * 1000,
                                    KgBBGN = c.TonBBGN * 1000,
                                    KgReturn = c.TonReturn * 1000,

                                    Note1 = c.Note1,
                                    Note2 = c.Note2,

                                    ProductCode = c.ORD_GroupProduct.ORD_Product.Count > 0 ? c.ORD_GroupProduct.ORD_Product.FirstOrDefault().CUS_Product.Code : "",
                                    ProductName = c.ORD_GroupProduct.ORD_Product.Count > 0 ? c.ORD_GroupProduct.ORD_Product.FirstOrDefault().CUS_Product.ProductName : "",
                                    ProductDescription = c.ORD_GroupProduct.ORD_Product.Count > 0 ? c.ORD_GroupProduct.ORD_Product.FirstOrDefault().CUS_Product.Description : "",

                                }).OrderBy(c => c.TOMasterID > 0).ThenBy(c => c.OrderCode).ToList();
                            }
                            #endregion

                            #region Chuyến
                            if (item.PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeTOMasterChange || item.PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeTOMasterOwner)
                            {
                                var lstDIMasterID = lstDIMaster.Where(c => c.PacketID == item.ID).Select(c => c.DITOMasterID.Value).Distinct().ToList();
                                lstMaster = model.OPS_DITOMaster.Where(c => lstDIMasterID.Contains(c.ID)).Select(c => new DTOTriggerMaster
                                {
                                    PacketID = item.ID,
                                    MasterID = c.ID,
                                    MasterCode = c.Code,
                                    VehicleID = c.VehicleID,
                                    VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                                    VendorID = c.VendorOfVehicleID,
                                    VendorCode = c.VendorOfVehicleID > 0 ? c.CUS_Customer.Code : "",
                                    VendorName = c.VendorOfVehicleID > 0 ? c.CUS_Customer.CustomerName : "",
                                    VendorShortName = c.VendorOfVehicleID > 0 ? c.CUS_Customer.ShortName : "",
                                    DriverID = c.DriverID1,
                                    DriverName = c.DriverName1,
                                    DrivingLicense = "",
                                    TelNo = c.DriverTel1,
                                    ETA = c.ETA,
                                    ETD = c.ETD,
                                    GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : "",
                                    GroupOfVehicleName = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.GroupName : "",
                                    KM = c.KM,
                                    KMStart = c.KMStart,
                                    KMEnd = c.KMEnd,
                                    TonTranfer = c.OPS_DITOGroupProduct.Count > 0 ? c.OPS_DITOGroupProduct.Sum(d => d.TonTranfer) : 0,
                                    CBMTranfer = c.OPS_DITOGroupProduct.Count > 0 ? c.OPS_DITOGroupProduct.Sum(d => d.CBMTranfer) : 0,
                                    QuantityTranfer = c.OPS_DITOGroupProduct.Count > 0 ? c.OPS_DITOGroupProduct.Sum(d => d.QuantityTranfer) : 0,
                                    TotalLocation = c.TotalLocation
                                }).ToList();

                                var lstCOMasterID = lstCOMaster.Where(c => c.PacketID == item.ID).Select(c => c.COTOMasterID.Value).Distinct().ToList();
                                lstMaster.AddRange(model.OPS_COTOMaster.Where(c => lstCOMasterID.Contains(c.ID)).Select(c => new DTOTriggerMaster
                                {
                                    MasterID = c.ID,
                                    MasterCode = c.Code,
                                    VehicleID = c.VehicleID,
                                    VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                                    RomoocCode = c.RomoocID > 0 ? c.CAT_Romooc.RegNo : "",
                                    VendorID = c.VendorOfVehicleID,
                                    VendorCode = c.VendorOfVehicleID > 0 ? c.CUS_Customer.Code : "",
                                    VendorName = c.VendorOfVehicleID > 0 ? c.CUS_Customer.CustomerName : "",
                                    VendorShortName = c.VendorOfVehicleID > 0 ? c.CUS_Customer.ShortName : "",
                                    DriverID = c.DriverID1,
                                    DriverName = c.DriverName1,
                                    DrivingLicense = "",
                                    TelNo = c.DriverTel1,
                                    ETA = c.ETA,
                                    ETD = c.ETD,
                                    GroupOfVehicleCode = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.Code : "",
                                    GroupOfVehicleName = c.GroupOfVehicleID > 0 ? c.CAT_GroupOfVehicle.GroupName : "",
                                    KM = c.KM,
                                    KMStart = c.KMStart,
                                    KMEnd = c.KMEnd,
                                    TotalLocation = c.TotalLocation,
                                }).ToList());
                            }
                            #endregion

                            #region Tạo PacketAction cho User
                            var company = model.CUS_Customer.FirstOrDefault(c => c.ID == Account.SYSCustomerID);
                            var lstCustomerName = lstOPSGroup.Select(c => c.CustomerName).Distinct().ToList();
                            DTOTriggerMailData itemData = new DTOTriggerMailData();
                            itemData.CustomerName = string.Join(", ", lstCustomerName);
                            itemData.Date = item.PacketDate.Date.ToString("dd/MM/yyyy");
                            if (company != null)
                            {
                                itemData.CompanyName = company.CustomerName;
                                itemData.CompanyAddress = company.Address;
                                itemData.CompanyTel = company.TelNo;
                                itemData.CompanyEmail = company.Email;
                            }
                            foreach (var itemSettingAction in lstSettingAction)
                            {
                                string strSubject = string.Empty;
                                string strTemplate = string.Empty;
                                var template = lstPacketSettingTemplate.FirstOrDefault(c => c.PacketSettingID == item.PacketSettingID && c.TypeOfActionID == itemSettingAction.TypeOfActionID);
                                if (template != null)
                                {
                                    if (!string.IsNullOrEmpty(template.Subject))
                                        strSubject = template.Subject;
                                    if (!string.IsNullOrEmpty(template.Template))
                                        strTemplate = template.Template;

                                    strSubject = MailHelper.StringHTML(strSubject, delegate(MailTemplate objHTML)
                                    {
                                        objHTML.HTML = objHTML.Token;
                                        try { objHTML.HTML = itemData.GetType().GetProperty(objHTML.Token).GetValue(itemData).ToString(); }
                                        catch { }
                                    });

                                    strTemplate = MailHelper.StringHTML(strTemplate, delegate(MailTemplate objHTML)
                                    {
                                        objHTML.HTML = objHTML.Token;
                                        try { objHTML.HTML = itemData.GetType().GetProperty(objHTML.Token).GetValue(itemData).ToString(); }
                                        catch { }
                                    });
                                }

                                WFL_PacketAction objAction = new WFL_PacketAction();
                                objAction.CreatedBy = Account.UserName;
                                objAction.CreatedDate = DateTime.Now;
                                objAction.PacketID = item.ID;
                                objAction.PacketSettingActionID = itemSettingAction.ID;
                                objAction.Subject = strSubject;
                                objAction.Body = strTemplate;
                                model.WFL_PacketAction.Add(objAction);

                                var settingTemplate = lstSettingTemplate.FirstOrDefault(c => c.TypeOfActionID == itemSettingAction.TypeOfActionID);
                                if (settingTemplate != null && settingTemplate.FileID > 0)
                                {
                                    DTOTriggerPacketAction packetAction = new DTOTriggerPacketAction();
                                    packetAction.PacketID = item.ID;
                                    packetAction.PacketSettingActionID = itemSettingAction.ID;
                                    packetAction.FileID = settingTemplate.FileID.Value;
                                    packetAction.FileName = settingTemplate.FileName;
                                    packetAction.FilePath = settingTemplate.FilePath;
                                    packetAction.FileExt = settingTemplate.FileExt;
                                    packetAction.FilePathResult = string.Empty;
                                    result.ListPacketAction.Add(packetAction);
                                }
                            }
                            #endregion

                            #region Tạo PacketDriver cho Driver
                            if ((item.IsDriverMail && item.DriverMailTemplateID.HasValue) || (item.IsDriverSMS && item.DriverSMSTemplateID.HasValue))
                            {
                                // Lấy danh sách tài xế
                                var lstDriver = lstMaster.Where(c => c.DriverID > 0).ToList();
                                foreach (var itemDriver in lstDriver.GroupBy(c => new { c.DriverID, c.DriverName, c.TelNo }))
                                {
                                    foreach (var itemMaster in lstMaster.Where(c => c.DriverID == itemDriver.Key.DriverID))
                                    {
                                        itemData = new DTOTriggerMailData();
                                        itemData.CustomerName = string.Join(", ", lstCustomerName);
                                        itemData.Date = item.PacketDate.Date.ToString("dd/MM/yyyy");
                                        itemData.DriverName = itemDriver.Key.DriverName;
                                        itemData.TOMasterCode = itemMaster.MasterCode;
                                        itemData.VehicleCode = itemMaster.VehicleCode;
                                        itemData.ETD = itemMaster.ETD.HasValue ? itemMaster.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                                        if (company != null)
                                        {
                                            itemData.CompanyName = company.CustomerName;
                                            itemData.CompanyAddress = company.Address;
                                            itemData.CompanyTel = company.TelNo;
                                            itemData.CompanyEmail = company.Email;
                                        }
                                        if ((item.IsDriverMail && item.DriverMailTemplateID.HasValue))
                                        {
                                            string strSubject = string.Empty;
                                            string strTemplate = string.Empty;

                                            if (!string.IsNullOrEmpty(item.DriverMailSubject))
                                                strSubject = item.DriverMailSubject;
                                            if (!string.IsNullOrEmpty(item.DriverMailTemplate))
                                                strTemplate = item.DriverMailTemplate;

                                            strSubject = MailHelper.StringHTML(strSubject, delegate(MailTemplate objHTML)
                                            {
                                                objHTML.HTML = objHTML.Token;
                                                try { objHTML.HTML = itemData.GetType().GetProperty(objHTML.Token).GetValue(itemData).ToString(); }
                                                catch { }
                                            });

                                            strTemplate = MailHelper.StringHTML(strTemplate, delegate(MailTemplate objHTML)
                                            {
                                                objHTML.HTML = objHTML.Token;
                                                try { objHTML.HTML = itemData.GetType().GetProperty(objHTML.Token).GetValue(itemData).ToString(); }
                                                catch { }
                                            });

                                            WFL_PacketDriver objAction = new WFL_PacketDriver();
                                            objAction.CreatedBy = Account.UserName;
                                            objAction.CreatedDate = DateTime.Now;
                                            objAction.PacketID = item.ID;
                                            objAction.DriverID = itemDriver.Key.DriverID.Value;
                                            objAction.TypeOfActionID = (int)WFLTypeOfAction.SendMail;
                                            objAction.Subject = strSubject;
                                            objAction.Body = strTemplate;
                                            model.WFL_PacketDriver.Add(objAction);

                                            if (item.DriverMailFileID > 0)
                                            {
                                                DTOTriggerPacketDriver packetDriver = new DTOTriggerPacketDriver();
                                                packetDriver.PacketID = item.ID;
                                                packetDriver.DriverID = itemDriver.Key.DriverID.Value;
                                                packetDriver.TypeOfActionID = objAction.TypeOfActionID;
                                                result.ListPacketDriver.Add(packetDriver);
                                            }
                                        }

                                        if ((item.IsDriverSMS && item.DriverSMSTemplateID.HasValue))
                                        {
                                            string strSubject = string.Empty;
                                            string strTemplate = string.Empty;

                                            if (!string.IsNullOrEmpty(item.DriverSMSSubject))
                                                strSubject = item.DriverSMSSubject;
                                            if (!string.IsNullOrEmpty(item.DriverSMSTemplate))
                                                strTemplate = item.DriverSMSTemplate;

                                            strSubject = MailHelper.StringHTML(strSubject, delegate(MailTemplate objHTML)
                                            {
                                                objHTML.HTML = objHTML.Token;
                                                try { objHTML.HTML = itemData.GetType().GetProperty(objHTML.Token).GetValue(itemData).ToString(); }
                                                catch { }
                                            });

                                            strTemplate = MailHelper.StringHTML(strTemplate, delegate(MailTemplate objHTML)
                                            {
                                                objHTML.HTML = objHTML.Token;
                                                try { objHTML.HTML = itemData.GetType().GetProperty(objHTML.Token).GetValue(itemData).ToString(); }
                                                catch { }
                                            });

                                            WFL_PacketDriver objAction = new WFL_PacketDriver();
                                            objAction.CreatedBy = Account.UserName;
                                            objAction.CreatedDate = DateTime.Now;
                                            objAction.PacketID = item.ID;
                                            objAction.DriverID = itemDriver.Key.DriverID.Value;
                                            objAction.TypeOfActionID = (int)WFLTypeOfAction.SMS;
                                            objAction.Subject = strSubject;
                                            objAction.Body = strTemplate;
                                            model.WFL_PacketDriver.Add(objAction);

                                            if (item.DriverSMSFileID > 0)
                                            {
                                                DTOTriggerPacketDriver packetDriver = new DTOTriggerPacketDriver();
                                                packetDriver.PacketID = item.ID;
                                                packetDriver.DriverID = itemDriver.Key.DriverID.Value;
                                                packetDriver.TypeOfActionID = objAction.TypeOfActionID;
                                                result.ListPacketDriver.Add(packetDriver);
                                            }
                                        }
                                    }
                                }
                            }
                            #endregion

                            result.ListOPSGroup.AddRange(lstOPSGroup);
                            result.ListMaster.AddRange(lstMaster);

                        }
                    }
                    #endregion

                    #region Tạo message cho các packet chưa send của Sự kiện và có PacketDate <= Ngày giờ hiện tại
                    List<DTOWFLMessageQueue> lstMessage = new List<DTOWFLMessageQueue>();
                    var lstPacketEvent = model.WFL_Packet.Where(c => c.PacketProcessID == -(int)SYSVarType.PacketProcessClose && c.TimeSendOffer <= dtNow && c.WFL_PacketSetting.PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeEvent && c.WFL_PacketSetting.IsApproved).Select(c => new
                    {
                        ID = c.ID,
                        PacketSettingID = c.PacketSettingID,
                        PacketDate = c.PacketDate,
                        PacketSettingTypeID = c.WFL_PacketSetting.PacketSettingTypeID,
                        Setting = c.WFL_PacketSetting.CUSSettingID > 0 ? c.WFL_PacketSetting.CUS_Setting.Setting : "",
                        CustomerID = c.WFL_PacketSetting.CustomerID,
                        CustomerName = c.WFL_PacketSetting.CustomerID > 0 ? c.WFL_PacketSetting.CUS_Customer.CustomerName : "",
                        c.TimeSendOffer,
                        c.WFL_PacketSetting.SYSCustomerID
                    });

                    foreach (var packetEvent in lstPacketEvent)
                    {
                        DateTime dtfrom = DateTime.Now.AddHours(-12);
                        // Lấy thời gian của gói trước
                        var prevPacket = model.WFL_Packet.Where(c => c.TimeSendOffer.HasValue && c.WFL_PacketSetting.PacketSettingTypeID == -(int)SYSVarType.PacketSettingTypeEvent && c.PacketSettingID == packetEvent.PacketSettingID && c.TimeSendOffer < packetEvent.TimeSendOffer && c.ID != packetEvent.ID).OrderByDescending(c => c.TimeSendOffer).FirstOrDefault();
                        if (prevPacket != null)
                            dtfrom = prevPacket.TimeSendOffer.Value;
                        var lstTable = model.WFL_EventField.Where(c => c.WFL_Event.SYSCustomerID == packetEvent.SYSCustomerID && (c.WFL_Event.IsApproved || c.WFL_Event.IsSystem) && c.WFL_Event.PacketSettingID == packetEvent.PacketSettingID).Select(c => new
                            {
                                c.WFL_Field.TableName,
                                c.WFL_Field.ColumnName,
                                c.WFL_Event.Expression,
                                EventID = c.WFL_Event.ID,
                                c.WFL_Event.EventName,
                                c.WFL_Field.ColumnType,
                                c.OperatorCode,
                                c.OperatorValue,
                                c.CompareValue
                            }).ToList();
                        var lstEventTemplate = model.WFL_EventTemplate.Where(c => c.WFL_Event.SYSCustomerID == packetEvent.SYSCustomerID && (c.WFL_Event.IsApproved || c.WFL_Event.IsSystem) && c.WFL_Event.PacketSettingID == packetEvent.PacketSettingID).Select(c => new
                        {
                            c.EventID,
                            c.TypeOfActionID,
                            c.WFL_Template.Name,
                            c.WFL_Template.Template,
                            c.ID,
                        }).ToList();

                        foreach (var itemEvent in lstTable.GroupBy(c => new { c.EventID, c.EventName, c.TableName, c.Expression }))
                        {
                            DTOTriggerPacket_Event evt = new DTOTriggerPacket_Event();
                            evt.SYSCustomerID = packetEvent.SYSCustomerID;
                            evt.TableName = itemEvent.Key.TableName;
                            evt.Expression = itemEvent.Key.Expression;
                            evt.ListProperty = new List<DTOTriggerPacket_EventField>();
                            foreach (var itemProperty in itemEvent)
                            {
                                DTOTriggerPacket_EventField pro = new DTOTriggerPacket_EventField();
                                pro.ColumnName = itemProperty.ColumnName;
                                pro.ColumnType = itemProperty.ColumnType;
                                evt.ListProperty.Add(pro);
                            }

                            var ListObject = GetListObjectByTableName(model, dtfrom, dtNow, evt);
                            if (ListObject.Count > 0)
                            {
                                var lstAction = model.WFL_Action.Where(c => c.EventID == itemEvent.Key.EventID && c.UserID.HasValue && c.IsUse && c.SYS_User.IsApproved).Select(c => new
                                {
                                    c.UserID,
                                    c.TypeOfActionID,
                                    c.ID,
                                    c.SYS_User.Email,
                                    c.SYS_User.ListCustomerID
                                }).ToList();

                                foreach (var act in lstAction)
                                {
                                    var ListCustomerID = new List<int>();
                                    if (!string.IsNullOrEmpty(act.ListCustomerID))
                                    {
                                        try
                                        {
                                            ListCustomerID = act.ListCustomerID.Split(',').Select(Int32.Parse).ToList();
                                        }
                                        catch { }
                                    }
                                    foreach (var obj in ListObject)
                                    {
                                        List<int> lstCustomerID = new List<int>();
                                        try
                                        {
                                            var objList = obj.GetType().GetProperty("ListCustomerID").GetValue(obj);
                                            lstCustomerID = objList as List<int>;
                                        }
                                        catch { }
                                        List<int> lstVendorID = new List<int>();
                                        try
                                        {
                                            var objList = obj.GetType().GetProperty("lstVendorID").GetValue(obj);
                                            lstVendorID = objList as List<int>;
                                        }
                                        catch { }
                                        if (lstCustomerID.Count == 0 || lstCustomerID.Any(c => ListCustomerID.Contains(c)) || lstVendorID.Any(c => ListCustomerID.Contains(c)))
                                        {
                                            // Chỉ thêm message khi event message cho user ko trùng
                                            if (lstMessage.Count(c => c.EventID == itemEvent.Key.EventID && c.UserID == act.UserID.Value && c.TypeOfActionID == act.TypeOfActionID) == 0)
                                            {
                                                lstMessage.Add(new DTOWFLMessageQueue { EventID = itemEvent.Key.EventID, UserID = act.UserID.Value, TypeOfActionID = act.TypeOfActionID });
                                                // Nội dung message
                                                string strContent = string.Empty;
                                                var template = lstEventTemplate.FirstOrDefault(c => c.TypeOfActionID == act.TypeOfActionID);
                                                if (template != null && !string.IsNullOrEmpty(template.Template))
                                                    strContent = template.Template;
                                                string strMessage = MailHelper.StringHTML(strContent, delegate(MailTemplate objMessage)
                                                {
                                                    objMessage.HTML = objMessage.Token;
                                                    try { objMessage.HTML = obj.GetType().GetProperty(objMessage.Token).GetValue(obj).ToString(); }
                                                    catch { }
                                                });
                                                if (string.IsNullOrEmpty(strMessage))
                                                {
                                                    var actionName = "";
                                                    if (act.TypeOfActionID == (int)WFLTypeOfAction.SendMail)
                                                        actionName = WFLTypeOfAction.SendMail.ToString();
                                                    if (act.TypeOfActionID == (int)WFLTypeOfAction.SMS)
                                                        actionName = WFLTypeOfAction.SMS.ToString();
                                                    if (act.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS)
                                                        actionName = WFLTypeOfAction.MessageTMS.ToString();
                                                    strMessage = "Event: " + itemEvent.Key.EventName + " hành động " + actionName + " không có template";
                                                }

                                                WFL_Message message = new WFL_Message();
                                                message.CreatedBy = Account.UserName;
                                                message.CreatedDate = DateTime.Now;
                                                message.ActionID = act.ID;
                                                message.Message = strMessage;
                                                message.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageWait;
                                                // Gửi mail
                                                if (act.TypeOfActionID == (int)WFLTypeOfAction.SendMail)
                                                {
                                                    if (!string.IsNullOrEmpty(act.Email))
                                                        model.WFL_Message.Add(message);
                                                }
                                                // Gửi SMS || Gửi thông báo TMS
                                                if (act.TypeOfActionID == (int)WFLTypeOfAction.SMS || act.TypeOfActionID == (int)WFLTypeOfAction.MessageTMS)
                                                    model.WFL_Message.Add(message);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    model.SaveChanges();

                    #region Check DITORate và COTORate
                    var lstCOTORate = model.OPS_COTORate.Where(c => c.IsAccept == null && c.LastRateTime <= dtNow).Select(c => c.COTOMasterID).Distinct().ToList();
                    var lstMasterCO = lstCOTORate.Select(c => new DTOOPSCO_VEN_TOMaster
                    { 
                        ID = c,
                    }).ToList();
                    HelperTOMaster.OPSCO_TenderReject(model, Account, lstMasterCO);

                    var lstDITORate = model.OPS_DITORate.Where(c => c.IsAccept == null && c.LastRateTime <= dtNow).Select(c => c.DITOMasterID).Distinct().ToList();
                    var lstMasterDI = lstDITORate.Select(c => new DTOOPSCO_VEN_TOMaster
                    {
                        ID = c,
                    }).ToList();
                    HelperTOMaster.OPSDI_TenderReject(model, Account, lstMasterDI);
                    #endregion
                }

                return result;
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

        public void Packet_Update(List<DTOTriggerPacketAction> ListPacketAction, List<DTOTriggerPacketDriver> ListPacketDriver)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    foreach (var item in ListPacketAction.Where(c => !string.IsNullOrEmpty(c.FilePathResult)))
                    {
                        var obj = model.WFL_PacketAction.FirstOrDefault(c => c.PacketID == item.PacketID && c.PacketSettingActionID == item.PacketSettingActionID);
                        if (obj != null)
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                            obj.FilePath = item.FilePathResult;
                        }
                    }

                    foreach (var item in ListPacketDriver.Where(c => !string.IsNullOrEmpty(c.FilePathResult)))
                    {
                        var obj = model.WFL_PacketDriver.FirstOrDefault(c => c.PacketID == item.PacketID && c.TypeOfActionID == item.TypeOfActionID && c.DriverID == item.DriverID);
                        if (obj != null)
                        {
                            obj.ModifiedBy = Account.UserName;
                            obj.ModifiedDate = DateTime.Now;
                            obj.FilePath = item.FilePathResult;
                        }
                    }

                    model.SaveChanges();
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

        private Type GetTypeByTypeName(string typeName)
        {
            switch (typeName)
            {
                case "int": return typeof(int);
                case "string": return typeof(string);
                case "datetime": return typeof(DateTime);
                case "bool": return typeof(bool);
                case "double": return typeof(double);
                case "decimal": return typeof(decimal);
                default: return typeof(string);
            }
        }

        private List<object> GetListObjectByTableName(DataEntities model, DateTime dtfrom, DateTime dtto, DTOTriggerPacket_Event evt)
        {
            List<object> result = new List<object>();
            DateTime dtNow = DateTime.Now;
            switch (evt.TableName)
            {
                #region KPI_KPITime
                case "KPI_KPITime":
                    var ListKPITime = model.KPI_KPITime.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.OrderID > 0 && c.ORD_Order.SYSCustomerID == evt.SYSCustomerID).Select(c => new DTOMessageQueueHelper_KPITime
                    {
                        ID = c.ID,
                        CustomerID = c.CustomerID,
                        OrderID = c.OrderID,
                        DITOMasterID = c.DITOMasterID,
                        COTOMasterID = c.COTOMasterID,
                        DateData = c.DateData,
                        OrderGroupProductID = c.OrderGroupProductID,
                        OrderContainerID = c.OrderContainerID,
                        DITOGroupProductID = c.DITOGroupProductID,
                        DateFromCome = c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        DateDN = c.DateDN,
                        DateInvoice = c.DateInvoice,
                        KPIDate = c.KPIDate,
                        KPIID = c.KPIID,
                        IsKPI = c.IsKPI,
                        Zone = c.Zone,
                        LeadTime = c.LeadTime,
                        ReasonID = c.ReasonID,
                        Note = c.Note,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        DateRequest = c.DateRequest,
                        CustomerName = c.CUS_Customer.CustomerName,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                        LocationFrom = c.ORD_GroupProduct.LocationFromID.HasValue ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Address : string.Empty,
                        LocationTo = c.ORD_GroupProduct.LocationToID.HasValue ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : string.Empty,
                        Ton = c.OPS_DITOGroupProduct.Ton,
                        CBM = c.OPS_DITOGroupProduct.CBM,
                        Quantity = c.OPS_DITOGroupProduct.Quantity,
                    }).ToList();

                    foreach (var obj in ListKPITime)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateFromComeTemp = obj.DateFromCome.HasValue ? obj.DateFromCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateFromLoadStartTemp = obj.DateFromLoadStart.HasValue ? obj.DateFromLoadStart.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateFromLoadEndTemp = obj.DateFromLoadEnd.HasValue ? obj.DateFromLoadEnd.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateFromLeaveTemp = obj.DateFromLeave.HasValue ? obj.DateFromLeave.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateToComeTemp = obj.DateToCome.HasValue ? obj.DateToCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateToLoadStartTemp = obj.DateToLoadStart.HasValue ? obj.DateToLoadStart.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateToLoadEndTemp = obj.DateToLoadEnd.HasValue ? obj.DateToLoadEnd.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateToLeaveTemp = obj.DateToLeave.HasValue ? obj.DateToLeave.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            obj.ListCustomerID.Add(obj.CustomerID);
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion

                #region ORD_Order
                case "ORD_Order":
                    var ListOrder = model.ORD_Order.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.SYSCustomerID == evt.SYSCustomerID).Select(c => new DTOMessageQueueHelper_Order
                    {
                        ID = c.ID,
                        ParentID = c.ParentID,
                        OrderRefID = c.OrderRefID,
                        SYSCustomerID = c.SYSCustomerID,
                        Code = c.Code,
                        CustomerID = c.CustomerID,
                        ServiceOfOrderID = c.ServiceOfOrderID,
                        TransportModeID = c.TransportModeID,
                        TypeOfContractID = c.TypeOfContractID,
                        ContractID = c.ContractID,
                        TypeOfOrderID = c.TypeOfOrderID,
                        StatusOfOrderID = c.StatusOfOrderID,
                        StatusOfPlanID = c.StatusOfPlanID,
                        RequestDate = c.RequestDate,
                        LocationFromID = c.LocationFromID,
                        ETD = c.ETD,
                        LocationToID = c.LocationToID,
                        ETA = c.ETA,
                        DateConfig = c.DateConfig,
                        CutOffTime = c.CutOffTime,
                        LoadingTime = c.LoadingTime,
                        PartnerID = c.PartnerID,
                        LocationDepotID = c.LocationDepotID,
                        LocationDepotReturnID = c.LocationDepotReturnID,
                        VesselNo = c.VesselNo,
                        VesselName = c.VesselName,
                        TripNo = c.TripNo,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        IsOPS = c.IsOPS,
                        IsClosed = c.IsClosed,
                        IsHot = c.IsHot,
                        BiddingNo = c.BiddingNo,
                        AllowCoLoad = c.AllowCoLoad,
                        LeadTime = c.LeadTime,
                        Note = c.Note,
                        ExternalCode = c.ExternalCode,
                        ExternalDate = c.ExternalDate,
                        UserDefine1 = c.UserDefine1,
                        UserDefine2 = c.UserDefine2,
                        UserDefine3 = c.UserDefine3,
                        UserDefine4 = c.UserDefine4,
                        UserDefine5 = c.UserDefine5,
                        UserDefine6 = c.UserDefine6,
                        UserDefine7 = c.UserDefine7,
                        UserDefine8 = c.UserDefine8,
                        UserDefine9 = c.UserDefine9,
                        DateShipCome = c.DateShipCome,
                        DateDocument = c.DateDocument,
                        DateInspect = c.DateInspect,
                        DateGetEmpty = c.DateGetEmpty,
                        DateReturnEmpty = c.DateReturnEmpty,
                        DateLoading = c.DateLoading,
                        DateUnloading = c.DateUnloading,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        ETARequest = c.ETARequest,
                        TextFrom = c.TextFrom,
                        TextTo = c.TextTo,
                        ETDEnd = c.ETDEnd,
                        ETAEnd = c.ETAEnd,
                        OrderCode = c.Code,
                        StatusOfOrderName = c.SYS_Var.ValueOfVar,
                        StatusOfPlanName = c.SYS_Var1.ValueOfVar,
                        TotalTon = model.OPS_DITOGroupProduct.Count(d => d.ORD_GroupProduct.OrderID == c.ID) > 0 ? model.OPS_DITOGroupProduct.Where(d => d.ORD_GroupProduct.OrderID == c.ID).Sum(d => d.Ton) : c.ORD_GroupProduct.Sum(d => d.Ton),
                        TotalCBM = model.OPS_DITOGroupProduct.Count(d => d.ORD_GroupProduct.OrderID == c.ID) > 0 ? model.OPS_DITOGroupProduct.Where(d => d.ORD_GroupProduct.OrderID == c.ID).Sum(d => d.CBM) : c.ORD_GroupProduct.Sum(d => d.CBM),
                    }).ToList();

                    foreach (var obj in ListOrder)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ETDTemp = obj.ETD.HasValue ? obj.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ETATemp = obj.ETA.HasValue ? obj.ETA.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateConfigTemp = obj.DateConfig.HasValue ? obj.DateConfig.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            obj.ListCustomerID.Add(obj.CustomerID);
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion

                #region OPS_DITOMaster
                case "OPS_DITOMaster":
                    var ListDITOMaster = model.OPS_DITOMaster.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.SYSCustomerID == evt.SYSCustomerID).Select(c => new DTOMessageQueueHelper_DITOMaster
                    {
                        ID = c.ID,
                        SYSCustomerID = c.SYSCustomerID,
                        Code = c.Code,
                        VehicleID = c.VehicleID,
                        VendorOfVehicleID = c.VendorOfVehicleID,
                        DriverID1 = c.DriverID1,
                        DriverID2 = c.DriverID2,
                        ApprovedBy = c.ApprovedBy,
                        ApprovedDate = c.ApprovedDate,
                        StatusOfDITOMasterID = c.StatusOfDITOMasterID,
                        TypeOfDITOMasterID = c.TypeOfDITOMasterID,
                        GroupOfVehicleID = c.GroupOfVehicleID,
                        SortOrder = c.SortOrder,
                        ETD = c.ETD,
                        ETA = c.ETA,
                        IsRouteVendor = c.IsRouteVendor,
                        IsRouteCustomer = c.IsRouteCustomer,
                        IsLoading = c.IsLoading,
                        IsHot = c.IsHot,
                        RateTime = c.RateTime,
                        IsBidding = c.IsBidding,
                        Note = c.Note,
                        DriverName1 = c.DriverName1,
                        DriverName2 = c.DriverName2,
                        DriverTel1 = c.DriverTel1,
                        DriverTel2 = c.DriverTel2,
                        DriverCard1 = c.DriverCard1,
                        DriverCard2 = c.DriverCard2,
                        KM = c.KM,
                        TransportModeID = c.TransportModeID,
                        TypeOfOrderID = c.TypeOfOrderID,
                        ContractID = c.ContractID,
                        AllowCoLoad = c.AllowCoLoad,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        CATRoutingID = c.CATRoutingID,
                        CUSRoutingID = c.CUSRoutingID,
                        DateConfig = c.DateConfig,
                        KMStart = c.KMStart,
                        KMEnd = c.KMEnd,
                        Note1 = c.Note1,
                        Note2 = c.Note2,
                        TotalLocation = c.TotalLocation,
                        TypeOfPaymentDITOMasterID = c.TypeOfPaymentDITOMasterID,
                        PayVendorModified = c.PayVendorModified,
                        PayVendorNote = c.PayVendorNote,
                        PayUserModified = c.PayUserModified,
                        PayUserNote = c.PayUserNote,
                        StatusOfDITOMasterName = c.SYS_Var.ValueOfVar,
                        TOMasterCode = c.Code,
                        VehicleCode = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : string.Empty,
                    }).ToList();
                    foreach (var obj in ListDITOMaster)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ETDTemp = obj.ETD.HasValue ? obj.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ETATemp = obj.ETA.HasValue ? obj.ETA.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateConfigTemp = obj.DateConfig.HasValue ? obj.DateConfig.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            obj.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.ID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                            result.Add(obj);
                        }
                    }

                    break;
                #endregion

                #region FLM_AssetTimeSheet
                case "FLM_AssetTimeSheet":
                    var ListAssetTimeSheet = model.FLM_AssetTimeSheet.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.FLM_Asset.SYSCustomerID == evt.SYSCustomerID).Select(c => new DTOMessageQueueHelper_AssetTimeSheet
                    {
                        ID = c.ID,
                        AssetID = c.AssetID,
                        StatusOfAssetTimeSheetID = c.StatusOfAssetTimeSheetID,
                        ReferID = c.ReferID,
                        DateFrom = c.DateFrom,
                        DateTo = c.DateTo,
                        Note = c.Note,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        DateFromActual = c.DateFromActual,
                        DateToActual = c.DateToActual,
                        TypeOfAssetTimeSheetID = c.TypeOfAssetTimeSheetID,
                        StatusOfAssetTimeSheetName = c.SYS_Var.ValueOfVar,
                        TypeOfAssetTimeSheetName = c.SYS_Var1.ValueOfVar,
                        VehicleCode = c.FLM_Asset.CAT_Vehicle.RegNo,
                        DriverName = c.FLM_AssetTimeSheetDriver.Count > 0 ? c.FLM_AssetTimeSheetDriver.FirstOrDefault().FLM_Driver.CAT_Driver.LastName + " " + c.FLM_AssetTimeSheetDriver.FirstOrDefault().FLM_Driver.CAT_Driver.FirstName : "",
                        DITOMasterID = c.ReferID,
                        TOMasterCode = c.StatusOfAssetTimeSheetID == -(int)SYSVarType.StatusOfAssetTimeSheetDITOMaster && c.ReferID > 0 && model.OPS_DITOMaster.Count(d => d.ID == c.ReferID) > 0 ? model.OPS_DITOMaster.FirstOrDefault(d => d.ID == c.ReferID).Code : "",
                    }).ToList();
                    foreach (var obj in ListAssetTimeSheet)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateFromTemp = obj.DateFrom.HasValue ? obj.DateFrom.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateToTemp = obj.DateTo.HasValue ? obj.DateTo.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateFromActualTemp = obj.DateFromActual.HasValue ? obj.DateFromActual.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateToActualTemp = obj.DateToActual.HasValue ? obj.DateToActual.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            obj.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.ReferID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                            result.Add(obj);
                        }
                    }

                    break;
                #endregion

                #region OPS_DITOGroupProduct
                case "OPS_DITOGroupProduct":
                    var ListDITOGroupProduct = model.OPS_DITOGroupProduct.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.OrderGroupProductID > 0 && c.ORD_GroupProduct.ORD_Order.SYSCustomerID == evt.SYSCustomerID).Select(c => new DTOMessageQueueHelper_DITOGroupProduct
                    {
                        ID = c.ID,
                        DITOMasterID = c.DITOMasterID,
                        CustomerID = c.ORD_GroupProduct.ORD_Order.CustomerID,
                        OrderGroupProductID = c.OrderGroupProductID,
                        LockedBy = c.LockedBy,
                        Ton = c.Ton,
                        CBM = c.CBM,
                        Quantity = c.Quantity,
                        TonTranfer = c.TonTranfer,
                        CBMTranfer = c.CBMTranfer,
                        QuantityTranfer = c.QuantityTranfer,
                        TonBBGN = c.TonBBGN,
                        CBMBBGN = c.CBMBBGN,
                        QuantityBBGN = c.QuantityBBGN,
                        QuantityLoading = c.QuantityLoading,
                        Note = c.Note == null ? string.Empty : c.Note,
                        IsInput = c.IsInput,
                        GroupSort = c.GroupSort,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        DNCode = c.DNCode == null ? string.Empty : c.DNCode,
                        DITOGroupProductStatusID = c.DITOGroupProductStatusID,
                        DateFromCome = c.DateFromCome,
                        DateFromLeave = c.DateFromLeave,
                        DateFromLoadStart = c.DateFromLoadStart,
                        DateFromLoadEnd = c.DateFromLoadEnd,
                        DateToCome = c.DateToCome,
                        DateToLeave = c.DateToLeave,
                        DateToLoadStart = c.DateToLoadStart,
                        DateToLoadEnd = c.DateToLoadEnd,
                        IsOrigin = c.IsOrigin,
                        Note1 = c.Note1 == null ? string.Empty : c.Note1,
                        Note2 = c.Note2 == null ? string.Empty : c.Note2,
                        DITOGroupProductStatusPODID = c.DITOGroupProductStatusPODID,
                        InvoiceNote = c.InvoiceNote == null ? string.Empty : c.InvoiceNote,
                        InvoiceBy = c.InvoiceBy == null ? string.Empty : c.InvoiceBy,
                        InvoiceDate = c.InvoiceDate,
                        DateDN = c.DateDN,
                        CUSRoutingID = c.CUSRoutingID,
                        StatusOfDITOMasterID = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.StatusOfDITOMasterID : -1,
                        TOMasterCode = c.DITOMasterID.HasValue ? c.OPS_DITOMaster.Code : string.Empty,
                        OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                        SOCode = c.ORD_GroupProduct.SOCode == null ? string.Empty : c.ORD_GroupProduct.SOCode,
                        LocationFromCode = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Code : "",
                        LocationFromName = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Location : "",
                        LocationFromAddress = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Address : "",
                        LocationToCode = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Code : "",
                        LocationToName = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location : "",
                        LocationToAddress = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : "",
                    }).ToList();

                    foreach (var obj in ListDITOGroupProduct)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateFromComeTemp = obj.DateFromCome.HasValue ? obj.DateFromCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateFromLoadStartTemp = obj.DateFromLoadStart.HasValue ? obj.DateFromLoadStart.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateFromLoadEndTemp = obj.DateFromLoadEnd.HasValue ? obj.DateFromLoadEnd.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateFromLeaveTemp = obj.DateFromLeave.HasValue ? obj.DateFromLeave.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateToComeTemp = obj.DateToCome.HasValue ? obj.DateToCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateToLoadStartTemp = obj.DateToLoadStart.HasValue ? obj.DateToLoadStart.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateToLoadEndTemp = obj.DateToLoadEnd.HasValue ? obj.DateToLoadEnd.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateToLeaveTemp = obj.DateToLeave.HasValue ? obj.DateToLeave.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.InvoiceDateTemp = obj.InvoiceDate.HasValue ? obj.InvoiceDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateDNTemp = obj.DateDN.HasValue ? obj.DateDN.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            obj.ListCustomerID.Add(obj.CustomerID);
                            result.Add(obj);
                        }
                    }

                    break;
                #endregion

                #region OPS_DITORate
                case "OPS_DITORate":
                    var ListDITORate = model.OPS_DITORate.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.VendorID.HasValue && c.VendorID != evt.SYSCustomerID && c.OPS_DITOMaster.SYSCustomerID == evt.SYSCustomerID).Select(c => new DTOMessageQueueHelper_DITORate
                    {
                        ID = c.ID,
                        DITOMasterID = c.DITOMasterID,
                        VendorID = c.VendorID,
                        SortOrder = c.SortOrder,
                        IsAccept = c.IsAccept,
                        IsSend = c.IsSend,
                        Debit = c.Debit,
                        ReasonID = c.ReasonID,
                        Reason = c.Reason,
                        IsManual = c.IsManual,
                        FirstRateTime = c.FirstRateTime,
                        LastRateTime = c.LastRateTime,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        StatusOfDITOMasterID = c.OPS_DITOMaster.StatusOfDITOMasterID,
                        TOMasterCode = c.OPS_DITOMaster.Code,
                        VendorCode = c.CUS_Customer.Code,
                    }).ToList();

                    foreach (var obj in ListDITORate)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.FirstRateTimeTemp = obj.FirstRateTime.HasValue ? obj.FirstRateTime.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.LastRateTimeTemp = obj.LastRateTime.HasValue ? obj.LastRateTime.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            obj.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.DITOMasterID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                            obj.ListVendorID = new List<int>();
                            obj.ListVendorID.Add(obj.VendorID.Value);
                            obj.ListDetail = new List<DTOMessageQueueHelper_DITOGroupProduct>();
                            obj.ListDetail = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.DITOMasterID).Select(c => new DTOMessageQueueHelper_DITOGroupProduct
                            {
                                SOCode = c.ORD_GroupProduct.SOCode,
                                DNCode = c.DNCode,
                                Note = c.Note,
                                Note1 = c.Note1,
                                Note2 = c.Note2,
                                Ton = c.Ton,
                                CBM = c.CBM,
                                Quantity = c.Quantity,
                                TonTranfer = c.TonTranfer,
                                CBMTranfer = c.CBMTranfer,
                                QuantityTranfer = c.QuantityTranfer,
                                OrderCode = c.ORD_GroupProduct.ORD_Order.Code,
                                LocationFromCode = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Code : "",
                                LocationFromName = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Location : "",
                                LocationFromAddress = c.ORD_GroupProduct.LocationFromID > 0 ? c.ORD_GroupProduct.CUS_Location.CAT_Location.Address : "",
                                LocationToCode = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Code : "",
                                LocationToName = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Location : "",
                                LocationToAddress = c.ORD_GroupProduct.LocationToID > 0 ? c.ORD_GroupProduct.CUS_Location1.CAT_Location.Address : "",
                            }).ToList();
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion

                #region OPS_COTORate
                case "OPS_COTORate":
                    var ListCOTORate = model.OPS_COTORate.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.VendorID.HasValue && c.VendorID != Account.SYSCustomerID).Select(c => new DTOMessageQueueHelper_COTORate
                    {
                        ID = c.ID,
                        COTOMasterID = c.COTOMasterID,
                        VendorID = c.VendorID,
                        SortOrder = c.SortOrder,
                        IsAccept = c.IsAccept,
                        IsSend = c.IsSend,
                        Debit = c.Debit,
                        ReasonID = c.ReasonID,
                        Reason = c.Reason,
                        IsManual = c.IsManual,
                        FirstRateTime = c.FirstRateTime,
                        LastRateTime = c.LastRateTime,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        StatusOfCOTOMasterID = c.OPS_COTOMaster.StatusOfCOTOMasterID,
                        TOMasterCode = c.OPS_COTOMaster.Code,
                        VendorCode = c.CUS_Customer.Code,
                        VendorName = c.CUS_Customer.ShortName,
                        ETD = c.OPS_COTOMaster.ETD,
                        ETA = c.OPS_COTOMaster.ETA,
                    }).ToList();

                    foreach (var obj in ListCOTORate)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.FirstRateTimeTemp = obj.FirstRateTime.HasValue ? obj.FirstRateTime.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.LastRateTimeTemp = obj.LastRateTime.HasValue ? obj.LastRateTime.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ETDTemp = obj.ETD.HasValue ? obj.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ETATemp = obj.ETA.HasValue ? obj.ETA.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            obj.ListCustomerID = model.OPS_COTOContainer.Where(c => c.COTOMasterID == obj.COTOMasterID).Select(c => c.OPS_Container.ORD_Container.ORD_Order.CustomerID).Distinct().ToList();
                            obj.ListVendorID = new List<int>();
                            obj.ListVendorID.Add(obj.VendorID.Value);
                            obj.ListDetail = new List<DTOMessageQueueHelper_COTOContainer>();
                            obj.ListDetail = model.OPS_COTOContainer.Where(c => c.COTOMasterID == obj.COTOMasterID).Select(c => new DTOMessageQueueHelper_COTOContainer
                            {
                                OrderCode = c.OPS_Container.ORD_Container.ORD_Order.Code,
                                ContainerNo = c.OPS_Container.ContainerNo,
                                SealNo1 = c.OPS_Container.SealNo1,
                                SealNo2 = c.OPS_Container.SealNo2,
                                UserDefine1 = c.OPS_Container.ORD_Container.ORD_Order.UserDefine1,
                                UserDefine2 = c.OPS_Container.ORD_Container.ORD_Order.UserDefine2,
                                UserDefine3 = c.OPS_Container.ORD_Container.ORD_Order.UserDefine3,
                                LocationFromCode = c.OPS_Container.CAT_Location.Code,
                                LocationFromName = c.OPS_Container.CAT_Location.Location,
                                LocationFromAddress = c.OPS_Container.CAT_Location.Address,
                                LocationToCode = c.OPS_Container.CAT_Location1.Code,
                                LocationToName = c.OPS_Container.CAT_Location1.Location,
                                LocationToAddress = c.OPS_Container.CAT_Location1.Address,
                                ETA = c.ETA,
                                ETD = c.ETD,
                            }).ToList();

                            foreach (var item in obj.ListDetail)
                            {
                                item.ETDTemp = item.ETD.HasValue ? item.ETD.Value.ToString("dd/MM/yyyy HH:mm") : "";
                                item.ETATemp = item.ETA.HasValue ? item.ETA.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            }
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion

                #region CAT_ContractTerm
                case "CAT_ContractTerm":
                    var ListContractTerm = model.CAT_ContractTerm.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.CAT_Contract.SYSCustomerID == evt.SYSCustomerID).Select(c => new DTOMessageQueueHelper_ContractTerm
                    {
                        ID = c.ID,
                        ContractID = c.ContractID,
                        MaterialID = c.MaterialID,
                        PriceContract = c.PriceContract.HasValue ? c.PriceContract.Value : 0,
                        PriceCurrent = c.PriceCurrent.HasValue ? c.PriceCurrent.Value : 0,
                        Note = c.Note,
                        ExprInput = c.ExprInput,
                        IsWarning = c.IsWarning,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        MaterialCode = c.FLM_Material.Code,
                        ContractNo = c.CAT_Contract.ContractNo,
                        CustomerID = c.CAT_Contract.CustomerID.HasValue ? c.CAT_Contract.CustomerID.Value : -1,
                        CustomerName = c.CAT_Contract.CustomerID.HasValue ? c.CAT_Contract.CUS_Customer.CustomerName : string.Empty,
                    }).ToList();

                    foreach (var obj in ListContractTerm)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.PriceContractTemp = obj.PriceContract.ToString("0");
                            obj.PriceCurrentTemp = obj.PriceCurrent.ToString("0");
                            obj.ListCustomerID = new List<int>();
                            obj.ListCustomerID.Add(obj.CustomerID);
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion

                #region OPS_RouteProblem
                case "OPS_RouteProblem":
                    var ListRouteProblem = model.OPS_RouteProblem.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto))).Select(c => new DTOMessageQueueHelper_RouteProblem
                    {
                        ID = c.ID,
                        RegNo = c.VehicleID > 0 ? c.CAT_Vehicle.RegNo : "",
                        LastName = c.DriverID > 0 ? c.FLM_Driver.CAT_Driver.LastName : "",
                        FirstName = c.DriverID > 0 ? c.FLM_Driver.CAT_Driver.FirstName : "",
                        TypeOfRouteProblemCode = c.OPS_TypeOfRouteProblem.Code,
                        TypeOfRouteProblemName = c.OPS_TypeOfRouteProblem.TypeName,
                        Lat = c.Lat,
                        Lng = c.Lng,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        DateStart = c.DateStart,
                        DateEnd = c.DateEnd
                    }).ToList();

                    foreach (var obj in ListRouteProblem)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.DateStartTemp = obj.DateStart.ToString("dd/MM/yyyy HH:mm");
                            obj.DateEndTemp = obj.DateEnd.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion

                #region CAT_Trouble
                case "CAT_Trouble":
                    var ListTrouble = model.CAT_Trouble.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && ((c.DITOMasterID > 0 && c.OPS_DITOMaster.SYSCustomerID == evt.SYSCustomerID) || (c.COTOMasterID > 0 && c.OPS_COTOMaster.SYSCustomerID == evt.SYSCustomerID))).Select(c => new DTOMessageQueueHelper_Trouble
                    {
                        ID = c.ID,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        Description = c.Description,
                        Cost = c.Cost,
                        TOMasterCode = c.DITOMasterID > 0 ? c.OPS_DITOMaster.Code : c.COTOMasterID > 0 ? c.OPS_COTOMaster.Code : "",
                        DITOMasterID = c.DITOMasterID,
                        COTOMasterID = c.COTOMasterID,
                        TroubleCostStatusID = c.TroubleCostStatusID,
                        TroubleName = c.CAT_GroupOfTrouble.Name,
                    }).ToList();

                    foreach (var obj in ListTrouble)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CostTemp = obj.Cost.ToString("0");
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            if (obj.DITOMasterID > 0)
                                obj.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.DITOMasterID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion

                #region CAT_Comment
                case "CAT_Comment":
                    var ListComment = model.CAT_Comment.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto))).Select(c => new DTOMessageQueueHelper_Comment
                    {
                        ID = c.ID,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        OrderCode = model.ORD_Order.Count(d => d.ID == c.ReferID) > 0 ? model.ORD_Order.FirstOrDefault(d => d.ID == c.ReferID).Code : "",
                        SYSCustomerID = model.ORD_Order.Count(d => d.ID == c.ReferID) > 0 ? model.ORD_Order.FirstOrDefault(d => d.ID == c.ReferID).SYSCustomerID : -1,
                        CustomerID = model.ORD_Order.Count(d => d.ID == c.ReferID) > 0 ? model.ORD_Order.FirstOrDefault(d => d.ID == c.ReferID).CustomerID : -1,
                        ReferID = c.ReferID,
                    }).ToList();

                    foreach (var obj in ListComment.Where(c => c.SYSCustomerID == evt.SYSCustomerID))
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            obj.ListCustomerID.Add(obj.CustomerID);
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion

                #region OPS_DITOLocation
                case "OPS_DITOLocation":
                    var ListDITOLocation = model.OPS_DITOLocation.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.OPS_DITOMaster.SYSCustomerID == evt.SYSCustomerID).Select(c => new DTOMessageQueueHelper_DITOLocation
                    {
                        ID = c.ID,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        TOMasterCode = c.OPS_DITOMaster.Code,
                        VehicleCode = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                        DriverName = c.OPS_DITOMaster.DriverID1 > 0 ? c.OPS_DITOMaster.FLM_Driver.CAT_Driver.LastName + " " + c.OPS_DITOMaster.FLM_Driver.CAT_Driver.FirstName : "",
                        DateCome = c.DateCome,
                        DateLeave = c.DateLeave,
                        LocationAddress = c.CAT_Location.Address,
                        LocationName = c.CAT_Location.Location,
                        DITOLocationStatusID = c.DITOLocationStatusID,
                        DITOMasterID = c.DITOMasterID,
                        TypeOfTOLocationID = c.TypeOfTOLocationID,
                    }).ToList();

                    foreach (var obj in ListDITOLocation)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateComeTemp = obj.DateCome.HasValue ? obj.DateCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateLeaveTemp = obj.DateLeave.HasValue ? obj.DateLeave.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            if (obj.DITOMasterID > 0)
                                obj.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.DITOMasterID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion

                #region OPS_DITOStation
                case "OPS_DITOStation":
                    var ListDITOStation = model.OPS_DITOStation.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.OPS_DITOMaster.SYSCustomerID == evt.SYSCustomerID).Select(c => new DTOMessageQueueHelper_DITOStation
                    {
                        ID = c.ID,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        TOMasterCode = c.OPS_DITOMaster.Code,
                        VehicleCode = c.OPS_DITOMaster.VehicleID > 0 ? c.OPS_DITOMaster.CAT_Vehicle.RegNo : "",
                        DriverName = c.OPS_DITOMaster.DriverID1 > 0 ? c.OPS_DITOMaster.FLM_Driver.CAT_Driver.LastName + " " + c.OPS_DITOMaster.FLM_Driver.CAT_Driver.FirstName : "",
                        DateCome = c.DateCome,
                        LocationAddress = c.CAT_Location.Address,
                        LocationName = c.CAT_Location.Location,
                        LocationID = c.LocationID,
                        DITOLocationID = c.DITOLocationID,
                        DITOMasterID = c.DITOMasterID,
                    }).ToList();

                    foreach (var obj in ListDITOStation)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateComeTemp = obj.DateCome.HasValue ? obj.DateCome.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            if (obj.DITOMasterID > 0)
                                obj.ListCustomerID = model.OPS_DITOGroupProduct.Where(c => c.DITOMasterID == obj.DITOMasterID).Select(c => c.ORD_GroupProduct.ORD_Order.CustomerID).Distinct().ToList();
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion

                #region FLM_AssetWarning
                case "FLM_AssetWarning":
                    var ListAssetWarning = model.FLM_AssetWarning.Where(c => ((c.CreatedDate >= dtfrom && c.CreatedDate <= dtto) || (c.ModifiedDate >= dtfrom && c.ModifiedDate <= dtto)) && c.FLM_Asset.SYSCustomerID == evt.SYSCustomerID).Select(c => new DTOMessageQueueHelper_AssetWarning
                    {
                        ID = c.ID,
                        CreatedDate = c.CreatedDate,
                        CreatedBy = c.CreatedBy,
                        ModifiedDate = c.ModifiedDate,
                        ModifiedBy = c.ModifiedBy,
                        AssetID = c.AssetID,
                        AssetNo = c.FLM_Asset.Code != null ? c.FLM_Asset.Code : c.FLM_Asset.VehicleID > 0 ? c.FLM_Asset.CAT_Vehicle.RegNo : c.FLM_Asset.RomoocID > 0 ? c.FLM_Asset.CAT_Romooc.RegNo : c.FLM_Asset.ContainerID > 0 ? c.FLM_Asset.CAT_Container.ContainerNo : "",
                        TypeWarningID = c.TypeWarningID,
                        TypeWarningName = c.FLM_TypeWarning.WarningName,
                        DateData = c.DateData,
                        DateCompare = c.DateCompare,
                        NumberData = c.NumberData,
                        NumberCompare = c.NumberCompare,
                    }).ToList();

                    foreach (var obj in ListAssetWarning)
                    {
                        obj.DateNow = dtNow.Date;
                        obj.DateTimeNow = dtNow;
                        // Ktra công thức event có matching
                        var registry = new TypeRegistry();
                        foreach (var property in evt.ListProperty)
                            registry.RegisterSymbol(property.ColumnName, obj.GetType().GetProperty(property.ColumnName).GetValue(obj), GetTypeByTypeName(property.ColumnType));
                        var compiler = new CompiledExpression(evt.Expression);
                        compiler.TypeRegistry = registry;
                        bool res = false;
                        try
                        {
                            res = (bool)compiler.Eval();
                        }
                        catch
                        {
                            res = false;
                        }
                        if (res)
                        {
                            obj.CreatedDateTemp = obj.CreatedDate.ToString("dd/MM/yyyy HH:mm");
                            obj.ModifiedDateTemp = obj.ModifiedDate.HasValue ? obj.ModifiedDate.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateCompareTemp = obj.DateCompare.HasValue ? obj.DateCompare.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.DateDataTemp = obj.DateData.HasValue ? obj.DateData.Value.ToString("dd/MM/yyyy HH:mm") : "";
                            obj.ListCustomerID = new List<int>();
                            result.Add(obj);
                        }
                    }
                    break;
                #endregion
            }
            return result;
        }

        #endregion

        #region Trigger Material
        public List<DTOTriggerMaterial> PriceMaterial_ListMaterial()
        {
            try
            {
                var result = new List<DTOTriggerMaterial>();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    string sKey = SYSSettingKey.Material.ToString();
                    foreach (var item in model.SYS_Setting.Where(c => c.Key == sKey))
                    {
                        if (!string.IsNullOrEmpty(item.Setting))
                        {
                            var data = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOTriggerMaterial>(item.Setting);
                            if (data != null)
                                result.Add(data);
                        }
                    }
                }
                return result;
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

        public void PriceMaterial_Save(DTOTriggerMaterial item)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    if (item.DieselArea1_MaterialID > 0 && item.DieselArea1 > 0)
                        PriceMaterial_CheckContract(model, item.DieselArea1_MaterialID.Value, item.DieselArea1);
                    if (item.DieselArea2_MaterialID > 0 && item.DieselArea2 > 0)
                        PriceMaterial_CheckContract(model, item.DieselArea2_MaterialID.Value, item.DieselArea2);
                    if (item.DO05Area1_MaterialID > 0 && item.DO05Area1 > 0)
                        PriceMaterial_CheckContract(model, item.DO05Area1_MaterialID.Value, item.DO05Area1);
                    if (item.DO05Area2_MaterialID > 0 && item.DO05Area2 > 0)
                        PriceMaterial_CheckContract(model, item.DO05Area2_MaterialID.Value, item.DO05Area2);
                    if (item.DO25Area1_MaterialID > 0 && item.DO25Area1 > 0)
                        PriceMaterial_CheckContract(model, item.DO25Area1_MaterialID.Value, item.DO25Area1);
                    if (item.DO25Area2_MaterialID > 0 && item.DO25Area2 > 0)
                        PriceMaterial_CheckContract(model, item.DO25Area2_MaterialID.Value, item.DO25Area2);
                    if (item.E5RON92Area1_MaterialID > 0 && item.E5RON92Area1 > 0)
                        PriceMaterial_CheckContract(model, item.E5RON92Area1_MaterialID.Value, item.E5RON92Area1);
                    if (item.E5RON92Area2_MaterialID > 0 && item.E5RON92Area2 > 0)
                        PriceMaterial_CheckContract(model, item.E5RON92Area2_MaterialID.Value, item.E5RON92Area2);

                    model.SaveChanges();

                    string sKey = SYSSettingKey.Material.ToString();
                    foreach (var objSetting in model.SYS_Setting.Where(c => c.Key == sKey))
                    {
                        if (!string.IsNullOrEmpty(objSetting.Setting))
                        {
                            var material = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOTriggerMaterial>(objSetting.Setting);
                            if (material != null)
                            {
                                material.DieselArea1 = item.DieselArea1;
                                material.DieselArea2 = item.DieselArea2;
                                material.DO05Area1 = item.DO05Area1;
                                material.DO05Area2 = item.DO05Area2;
                                material.DO25Area1 = item.DO25Area1;
                                material.DO25Area2 = item.DO25Area2;
                                material.E5RON92Area1 = item.E5RON92Area1;
                                material.E5RON92Area2 = item.E5RON92Area2;
                                objSetting.Setting = Newtonsoft.Json.JsonConvert.SerializeObject(material);
                            }
                        }
                    }
                    model.SaveChanges();
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

        private void PriceMaterial_CheckContract(DataEntities model, int id, decimal price)
        {
            foreach (var item in model.CAT_ContractTerm.Where(c => c.MaterialID == id && c.PriceContract > 0 && c.IsClosed == false))
            {
                item.PriceCurrent = price;
                item.ModifiedBy = Account.UserName;
                item.ModifiedDate = DateTime.Now;
                if (!string.IsNullOrEmpty(item.ExprInput) && !string.IsNullOrEmpty(item.ExprDatePrice) && !item.IsWarning)
                {
                    DTOMaterialChecking itemCheck = new DTOMaterialChecking();
                    itemCheck.PriceContract = item.PriceContract.Value;
                    itemCheck.PriceCurrent = item.PriceCurrent > 0 ? item.PriceCurrent.Value : 0;
                    itemCheck.DateWarning = DateTime.Now;
                    bool flag = false;
                    DateTime? datePrice = null;
                    try
                    {
                        flag = PriceMaterial_CheckBool(itemCheck, item.ExprInput);
                        datePrice = PriceMaterial_CheckDate(itemCheck, item.ExprDatePrice);
                    }
                    catch { flag = false; }
                    if (flag && datePrice != null)
                    {
                        item.PriceWarning = price;
                        item.IsWarning = true;
                        item.DatePrice = datePrice;
                        item.DateWarning = DateTime.Now;
                        item.ModifiedBy = "trigger";
                        item.ModifiedDate = DateTime.Now;
                    }
                }
            }
        }

        private bool PriceMaterial_CheckBool(DTOMaterialChecking item, string strExp)
        {
            try
            {
                bool result = false;
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";

                row++;
                worksheet.Cells[row, col].Value = item.PriceContract;
                strExp = strExp.Replace("[PriceContract]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PriceCurrent;
                strExp = strExp.Replace("[PriceCurrent]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp;

                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                if (val == "True") result = true;
                else if (val == "False") result = false;

                return result;
            }
            catch
            {
                return false;
            }
        }

        private DateTime? PriceMaterial_CheckDate(DTOMaterialChecking item, string strExp)
        {
            try
            {
                DateTime? result = null;
                ExcelPackage package = new ExcelPackage();
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet 1");
                int row = 0, col = 1;
                string strCol = "A", strRow = "";

                row++;
                worksheet.Cells[row, col].Value = item.PriceContract;
                strExp = strExp.Replace("[PriceContract]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.PriceCurrent;
                strExp = strExp.Replace("[PriceCurrent]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = DateTime.Now;
                strExp = strExp.Replace("[Date]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Value = item.DateWarning;
                strExp = strExp.Replace("[DateWarning]", strCol + row);
                strRow = strCol + row; row++;

                worksheet.Cells[row, col].Formula = strExp;

                package.Workbook.Calculate();
                var val = worksheet.Cells[row, col].Value.ToString().Trim();

                try
                { result = DateTime.FromOADate(Convert.ToDouble(val)); }
                catch { }
                if (result == null)
                {
                    try
                    { result = Convert.ToDateTime(val, new CultureInfo("en-US")).Date; }
                    catch { }
                }

                return result;
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region Trigger Location
        public List<DTOCATLocation> Location_List()
        {
            try
            {
                var result = new List<DTOCATLocation>();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    result = model.CAT_Location.Where(c => !(c.Lat > 0 && c.Lng > 0)).Select(c => new DTOCATLocation
                    {
                        ID = c.ID,
                        Code = c.Code,
                        Address = c.Address,
                        ProvinceName = c.CAT_Province.ProvinceName,
                        DistrictName = c.CAT_District.DistrictName
                    }).Take(10).ToList();
                }
                return result;
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

        public void Location_Save(List<DTOCATLocation> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    if (lst.Count > 0)
                    {
                        foreach (var item in lst.Where(c => c.Lat > 0 && c.Lng > 0))
                        {
                            var obj = model.CAT_Location.FirstOrDefault(c => c.ID == item.ID);
                            if (obj != null)
                            {
                                obj.Lat = item.Lat;
                                obj.Lng = item.Lng;
                            }
                        }
                        model.SaveChanges();
                    }
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

        public List<CATLocationMatrix> LocationMatrix_List()
        {
            try
            {
                var result = new List<CATLocationMatrix>();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    var lst = model.OPS_DITO.Where(c => c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterDelivery &&
                        c.LocationFromID > 0 && c.LocationToID > 0 && (c.KM == null || c.KM <= 0)).Select(c => new { LocationFromID = c.LocationFromID.Value, LocationToID = c.LocationToID.Value }).Distinct().Take(10).ToList();
                    if (lst.Count > 0)
                    {
                        foreach (var item in lst)
                        {
                            var obj = model.CAT_LocationMatrix.FirstOrDefault(c => c.LocationFromID == item.LocationFromID && c.LocationToID == item.LocationToID);
                            if (obj == null)
                            {
                                obj = new CAT_LocationMatrix();
                                obj.LocationFromID = item.LocationFromID;
                                obj.LocationToID = item.LocationToID;
                                obj.IsChecked = false;
                                obj.KM = 0;
                                obj.Hour = 0;
                                obj.CreatedBy = Account.UserName;
                                obj.CreatedDate = DateTime.Now;
                                model.CAT_LocationMatrix.Add(obj);
                            }
                        }
                        model.SaveChanges();
                    }

                    result = model.CAT_LocationMatrix.Where(c => c.IsChecked == false).Select(c => new CATLocationMatrix
                    {
                        ID = c.ID,
                        LocationFromID = c.LocationFromID,
                        LocationFromLat = c.CAT_Location.Lat,
                        LocationFromLng = c.CAT_Location.Lng,
                        LocationToID = c.LocationToID,
                        LocationToLat = c.CAT_Location1.Lat,
                        LocationToLng = c.CAT_Location1.Lng,
                        KM = c.KM,
                        Hour = c.Hour
                    }).Take(10).ToList();
                }
                return result;
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

        public void LocationMatrix_Save(List<CATLocationMatrix> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = true;

                    if (lst.Count > 0)
                    {
                        foreach (var item in lst.Where(c => c.Hour > 0 && c.KM > 0))
                        {
                            var obj = model.CAT_LocationMatrix.FirstOrDefault(c => c.ID == item.ID);
                            if (obj != null)
                            {
                                obj.IsChecked = true;
                                obj.Hour = item.Hour;
                                obj.KM = item.KM;
                                obj.Instructions = item.Instructions;
                            }

                            foreach (var detail in model.OPS_DITO.Where(c => c.OPS_DITOMaster.StatusOfDITOMasterID >= -(int)SYSVarType.StatusOfDITOMasterDelivery &&
                                c.LocationFromID == item.LocationFromID && c.LocationToID == item.LocationToID && (c.KM == null || c.KM <= 0)))
                            {
                                detail.KM = item.KM;
                            }
                        }
                        model.SaveChanges();
                    }
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

        #region Trigger Email
        public List<DTOTriggerEmail> TriggerEmail_List()
        {
            try
            {
                var result = new List<DTOTriggerEmail>();
                using (var model = new DataEntities())
                {
                    DateTime dtNow = DateTime.Now;

                    var lstMessage = model.WFL_Message.Where(c => c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.SendMail && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_Action.UserID > 0).Select(c => new
                    {
                        c.WFL_Action.WFL_Event.SYSCustomerID,
                        c.ID,
                        c.Message,
                        c.WFL_Action.SYS_User.Email,
                        FirstName = c.WFL_Action.SYS_User.FirstName,
                        LastName = c.WFL_Action.SYS_User.LastName,
                        Subject = c.WFL_Action.WFL_Event.EventName,
                        UserName = c.WFL_Action.SYS_User.UserName,
                    }).ToList();

                    var lstPacketAction = model.WFL_PacketAction.Where(c => !c.IsSend && c.WFL_Packet.PacketProcessID == -(int)SYSVarType.PacketProcessClose && c.WFL_PacketSettingAction.TypeOfActionID == (int)WFLTypeOfAction.SendMail && c.WFL_Packet.TimeSendOffer < dtNow).Select(c => new
                    {
                        c.ID,
                        c.Subject,
                        c.Body,
                        c.FilePath,
                        c.WFL_PacketSettingAction.WFL_PacketSetting.SYSCustomerID,
                        c.WFL_PacketSettingAction.SYS_User.Email,
                        c.WFL_PacketSettingAction.SYS_User.UserName,
                        c.WFL_PacketSettingAction.SYS_User.LastName,
                        c.WFL_PacketSettingAction.SYS_User.FirstName,
                    }).ToList();

                    var lstPacketDriver = model.WFL_PacketDriver.Where(c => !c.IsSend && c.WFL_Packet.PacketProcessID == -(int)SYSVarType.PacketProcessClose && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail && c.WFL_Packet.TimeSendOffer < dtNow).Select(c => new
                    {
                        c.ID,
                        c.Subject,
                        c.Body,
                        c.FilePath,
                        c.WFL_Packet.WFL_PacketSetting.SYSCustomerID,
                        c.FLM_Driver.CAT_Driver.LastName,
                        c.FLM_Driver.CAT_Driver.FirstName,
                        c.FLM_Driver.CAT_Driver.Cellphone,
                        Email = model.SYS_User.Count(d => d.ID == c.DriverID) > 0 ? model.SYS_User.FirstOrDefault(d => d.DriverID == c.DriverID).Email : "",
                        UserName = model.SYS_User.Count(d => d.ID == c.DriverID) > 0 ? model.SYS_User.FirstOrDefault(d => d.DriverID == c.DriverID).UserName : "",
                    }).ToList();

                    var lstSYSCustomerID = model.CUS_Customer.Where(c => c.IsSystem).Select(c => c.ID).Distinct().ToList();
                    foreach (var itemSYSCustomerID in lstSYSCustomerID)
                    {
                        // Check mail setting
                        var mailSetting = HelperSYSSetting.SYSSettingSystem_GetBySYSCustomerID(model, itemSYSCustomerID);
                        if (mailSetting != null && !string.IsNullOrEmpty(mailSetting.MailUSER))
                        {
                            DTOTriggerEmail item = new DTOTriggerEmail();
                            item.SYSSetting = mailSetting;
                            item.Items = new List<DTOTriggerEmail_Item>();

                            item.Items = lstMessage.Where(c => c.SYSCustomerID == itemSYSCustomerID).Select(c => new DTOTriggerEmail_Item
                            {
                                ServiceID = c.ID,
                                ServiceType = "WFL_Message",
                                Body = c.Message,
                                Email = c.Email,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Subject = c.Subject,
                                UserName = c.UserName,
                            }).ToList();

                            item.Items.AddRange(lstPacketAction.Where(c => c.SYSCustomerID == itemSYSCustomerID && !string.IsNullOrEmpty(c.Email)).Select(c => new DTOTriggerEmail_Item
                            {
                                ServiceID = c.ID,
                                ServiceType = "WFL_PacketAction",
                                Body = c.Body,
                                Email = c.Email,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Subject = c.Subject,
                                UserName = c.UserName,
                                FilePath = c.FilePath,
                            }).ToList());

                            item.Items.AddRange(lstPacketDriver.Where(c => c.SYSCustomerID == itemSYSCustomerID && !string.IsNullOrEmpty(c.Email)).Select(c => new DTOTriggerEmail_Item
                            {
                                ServiceID = c.ID,
                                ServiceType = "WFL_PacketDriver",
                                Body = c.Body,
                                Email = c.Email,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Subject = c.Subject,
                                UserName = c.UserName,
                                FilePath = c.FilePath,
                            }).ToList());

                            result.Add(item);
                        }
                    }
                }
                return result;
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

        public void TriggerEmail_Send(List<DTOTriggerEmail> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    //model.EventAccount = Account; model.EventRunning = true;
                    List<long> lstPacketID = new List<long>();

                    foreach (var itemSetting in lst)
                    {
                        foreach (var item in itemSetting.Items.Where(c => c.ServiceType == "WFL_Message"))
                        {
                            var obj = model.WFL_Message.FirstOrDefault(c => c.ID == item.ServiceID);
                            if (obj != null)
                            {
                                obj.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageSended;
                                obj.ModifiedDate = DateTime.Now;
                                obj.ModifiedBy = Account.UserName;
                            }
                        }

                        foreach (var item in itemSetting.Items.Where(c => c.ServiceType == "WFL_PacketAction"))
                        {
                            var obj = model.WFL_PacketAction.FirstOrDefault(c => c.ID == item.ServiceID);
                            if (obj != null)
                            {
                                obj.IsSend = true;
                                obj.ModifiedDate = DateTime.Now;
                                obj.ModifiedBy = Account.UserName;
                                lstPacketID.Add(obj.PacketID);
                            }
                        }

                        foreach (var item in itemSetting.Items.Where(c => c.ServiceType == "WFL_PacketDriver"))
                        {
                            var obj = model.WFL_PacketDriver.FirstOrDefault(c => c.ID == item.ServiceID);
                            if (obj != null)
                            {
                                obj.IsSend = true;
                                obj.ModifiedDate = DateTime.Now;
                                obj.ModifiedBy = Account.UserName;
                                lstPacketID.Add(obj.PacketID);
                            }
                        }
                    }
                    model.SaveChanges();

                    foreach (var item in lstPacketID.Distinct().ToList())
                    {
                        var packet = model.WFL_Packet.FirstOrDefault(c => c.ID == item);
                        if (packet != null)
                        {
                            if (model.WFL_PacketAction.Where(c => c.PacketID == item).Count() == model.WFL_PacketAction.Where(c => c.PacketID == item && c.IsSend).Count() && model.WFL_PacketDriver.Where(c => c.PacketID == item).Count() == model.WFL_PacketDriver.Where(c => c.PacketID == item && c.IsSend).Count())
                            {
                                packet.PacketProcessID = -(int)SYSVarType.PacketProcessSend;
                                packet.ModifiedDate = DateTime.Now;
                                packet.ModifiedBy = Account.UserName;
                            }
                        }
                    }

                    model.SaveChanges();
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


        public List<DTOTriggerEmail> WFL_TriggerEmail_List()
        {
            try
            {
                var result = new List<DTOTriggerEmail>();
                using (var model = new DataEntities())
                {
                    DateTime dtNow = DateTime.Now;

                    var lstMessage = model.WFL_DefineWFMessage.Where(c => c.WFL_DefineWFAction.TypeOfActionID == (int)WFLTypeOfAction.SendMail && c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_DefineWFAction.UserID > 0).Select(c => new
                    {
                        c.WFL_DefineWFAction.WFL_DefineWFEvent.WFL_DefineWF.WFL_Define.SYSCustomerID,
                        c.ID,
                        c.Message,
                        c.WFL_DefineWFAction.SYS_User.Email,
                        FirstName = c.WFL_DefineWFAction.SYS_User.FirstName,
                        LastName = c.WFL_DefineWFAction.SYS_User.LastName,
                        Subject = c.WFL_DefineWFAction.WFL_DefineWFEvent.WFL_WFEvent.EventName,
                        UserName = c.WFL_DefineWFAction.SYS_User.UserName,
                    }).ToList();

                    var lstPacketAction = model.WFL_PacketAction.Where(c => !c.IsSend && c.WFL_Packet.PacketProcessID == -(int)SYSVarType.PacketProcessClose && c.WFL_PacketSettingAction.TypeOfActionID == (int)WFLTypeOfAction.SendMail && c.WFL_Packet.TimeSendOffer < dtNow).Select(c => new
                    {
                        c.ID,
                        c.Subject,
                        c.Body,
                        c.FilePath,
                        c.WFL_PacketSettingAction.WFL_PacketSetting.SYSCustomerID,
                        c.WFL_PacketSettingAction.SYS_User.Email,
                        c.WFL_PacketSettingAction.SYS_User.UserName,
                        c.WFL_PacketSettingAction.SYS_User.LastName,
                        c.WFL_PacketSettingAction.SYS_User.FirstName,
                    }).ToList();

                    var lstPacketDriver = model.WFL_PacketDriver.Where(c => !c.IsSend && c.WFL_Packet.PacketProcessID == -(int)SYSVarType.PacketProcessClose && c.TypeOfActionID == (int)WFLTypeOfAction.SendMail && c.WFL_Packet.TimeSendOffer < dtNow).Select(c => new
                    {
                        c.ID,
                        c.Subject,
                        c.Body,
                        c.FilePath,
                        c.WFL_Packet.WFL_PacketSetting.SYSCustomerID,
                        c.FLM_Driver.CAT_Driver.LastName,
                        c.FLM_Driver.CAT_Driver.FirstName,
                        c.FLM_Driver.CAT_Driver.Cellphone,
                        Email = model.SYS_User.Count(d => d.ID == c.DriverID) > 0 ? model.SYS_User.FirstOrDefault(d => d.DriverID == c.DriverID).Email : "",
                        UserName = model.SYS_User.Count(d => d.ID == c.DriverID) > 0 ? model.SYS_User.FirstOrDefault(d => d.DriverID == c.DriverID).UserName : "",
                    }).ToList();

                    var lstSYSCustomerID = model.CUS_Customer.Where(c => c.IsSystem).Select(c => c.ID).Distinct().ToList();
                    foreach (var itemSYSCustomerID in lstSYSCustomerID)
                    {
                        // Check mail setting
                        var mailSetting = HelperSYSSetting.SYSSettingSystem_GetBySYSCustomerID(model, itemSYSCustomerID);
                        if (mailSetting != null && !string.IsNullOrEmpty(mailSetting.MailUSER))
                        {
                            DTOTriggerEmail item = new DTOTriggerEmail();
                            item.SYSSetting = mailSetting;
                            item.Items = new List<DTOTriggerEmail_Item>();

                            item.Items = lstMessage.Where(c => c.SYSCustomerID == itemSYSCustomerID).Select(c => new DTOTriggerEmail_Item
                            {
                                ServiceID = c.ID,
                                ServiceType = "WFL_DefineWFMessage",
                                Body = c.Message,
                                Email = c.Email,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Subject = c.Subject,
                                UserName = c.UserName,
                            }).ToList();

                            item.Items.AddRange(lstPacketAction.Where(c => c.SYSCustomerID == itemSYSCustomerID && !string.IsNullOrEmpty(c.Email)).Select(c => new DTOTriggerEmail_Item
                            {
                                ServiceID = c.ID,
                                ServiceType = "WFL_PacketAction",
                                Body = c.Body,
                                Email = c.Email,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Subject = c.Subject,
                                UserName = c.UserName,
                                FilePath = c.FilePath,
                            }).ToList());

                            item.Items.AddRange(lstPacketDriver.Where(c => c.SYSCustomerID == itemSYSCustomerID && !string.IsNullOrEmpty(c.Email)).Select(c => new DTOTriggerEmail_Item
                            {
                                ServiceID = c.ID,
                                ServiceType = "WFL_PacketDriver",
                                Body = c.Body,
                                Email = c.Email,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Subject = c.Subject,
                                UserName = c.UserName,
                                FilePath = c.FilePath,
                            }).ToList());

                            result.Add(item);
                        }
                    }
                }
                return result;
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

        public void WFL_TriggerEmail_Send(List<DTOTriggerEmail> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    //model.EventAccount = Account; model.EventRunning = true;
                    List<long> lstPacketID = new List<long>();

                    foreach (var itemSetting in lst)
                    {
                        foreach (var item in itemSetting.Items.Where(c => c.ServiceType == "WFL_DefineWFMessage"))
                        {
                            var obj = model.WFL_DefineWFMessage.FirstOrDefault(c => c.ID == item.ServiceID);
                            if (obj != null)
                            {
                                obj.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageSended;
                                obj.ModifiedDate = DateTime.Now;
                                obj.ModifiedBy = Account.UserName;
                            }
                        }

                        foreach (var item in itemSetting.Items.Where(c => c.ServiceType == "WFL_PacketAction"))
                        {
                            var obj = model.WFL_PacketAction.FirstOrDefault(c => c.ID == item.ServiceID);
                            if (obj != null)
                            {
                                obj.IsSend = true;
                                obj.ModifiedDate = DateTime.Now;
                                obj.ModifiedBy = Account.UserName;
                                lstPacketID.Add(obj.PacketID);
                            }
                        }

                        foreach (var item in itemSetting.Items.Where(c => c.ServiceType == "WFL_PacketDriver"))
                        {
                            var obj = model.WFL_PacketDriver.FirstOrDefault(c => c.ID == item.ServiceID);
                            if (obj != null)
                            {
                                obj.IsSend = true;
                                obj.ModifiedDate = DateTime.Now;
                                obj.ModifiedBy = Account.UserName;
                                lstPacketID.Add(obj.PacketID);
                            }
                        }
                    }
                    model.SaveChanges();

                    foreach (var item in lstPacketID.Distinct().ToList())
                    {
                        var packet = model.WFL_Packet.FirstOrDefault(c => c.ID == item);
                        if (packet != null)
                        {
                            if (model.WFL_PacketAction.Where(c => c.PacketID == item).Count() == model.WFL_PacketAction.Where(c => c.PacketID == item && c.IsSend).Count() && model.WFL_PacketDriver.Where(c => c.PacketID == item).Count() == model.WFL_PacketDriver.Where(c => c.PacketID == item && c.IsSend).Count())
                            {
                                packet.PacketProcessID = -(int)SYSVarType.PacketProcessSend;
                                packet.ModifiedDate = DateTime.Now;
                                packet.ModifiedBy = Account.UserName;
                            }
                        }
                    }

                    model.SaveChanges();
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

        #region Trigger SMS
        public List<DTOTriggerSMS> TriggerSMS_List()
        {
            try
            {
                var result = new List<DTOTriggerSMS>();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    result.AddRange(model.WFL_Message.Where(c => c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_Action.TypeOfActionID == (int)WFLTypeOfAction.SMS && !string.IsNullOrEmpty(c.WFL_Action.SYS_User.TelNo)).Select(c => new DTOTriggerSMS
                    {
                        ServiceID = c.ID,
                        ServiceType = "WFL_Message",
                        TelNo = c.WFL_Action.SYS_User.TelNo,
                        Message = c.Message
                    }).ToList());

                    DateTime dtNow = DateTime.Now;
                    var lstPacketAction = model.WFL_PacketAction.Where(c => !c.IsSend && c.WFL_Packet.PacketProcessID == -(int)SYSVarType.PacketProcessClose && c.WFL_PacketSettingAction.TypeOfActionID == (int)WFLTypeOfAction.SMS && c.WFL_Packet.TimeSendOffer < dtNow).Select(c => new
                    {
                        c.ID,
                        c.Body,
                        TelNo = c.WFL_PacketSettingAction.SYS_User.TelNo,
                        ServiceType = "WFL_PacketAction",
                    }).ToList();

                    lstPacketAction.AddRange(model.WFL_PacketDriver.Where(c => !c.IsSend && c.WFL_Packet.PacketProcessID == -(int)SYSVarType.PacketProcessClose && c.TypeOfActionID == (int)WFLTypeOfAction.SMS && c.WFL_Packet.TimeSendOffer < dtNow).Select(c => new
                    {
                        c.ID,
                        c.Body,
                        TelNo = model.SYS_User.Count(d => d.DriverID == c.DriverID && d.TelNo != null) > 0 ? model.SYS_User.FirstOrDefault(d => d.DriverID == c.DriverID).TelNo : "",
                        ServiceType = "WFL_PacketDriver",
                    }).ToList());
                    foreach (var action in lstPacketAction.Where(c => !string.IsNullOrEmpty(c.TelNo)))
                    {
                        DTOTriggerSMS item = new DTOTriggerSMS();
                        item.ServiceID = action.ID;
                        item.ServiceType = action.ServiceType;
                        item.TelNo = action.TelNo;
                        item.Message = action.Body;
                        result.Add(item);
                    }
                }
                return result;
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

        public void TriggerSMS_Send(List<DTOTriggerSMS> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    //model.EventAccount = Account; model.EventRunning = false;
                    List<long> lstPacketID = new List<long>();

                    foreach (var item in lst.Where(c => c.ServiceType == "WFL_Message"))
                    {
                        var obj = model.WFL_Message.FirstOrDefault(c => c.ID == item.ServiceID);
                        if (obj != null)
                        {
                            obj.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageSended;
                            obj.ModifiedDate = DateTime.Now;
                            obj.ModifiedBy = Account.UserName;
                        }
                    }
                    model.SaveChanges();

                    foreach (var item in lst.Where(c => c.ServiceType == "WFL_PacketAction"))
                    {
                        var obj = model.WFL_PacketAction.FirstOrDefault(c => c.ID == item.ServiceID);
                        if (obj != null)
                        {
                            obj.IsSend = true;
                            obj.ModifiedDate = DateTime.Now;
                            obj.ModifiedBy = Account.UserName;
                            lstPacketID.Add(obj.PacketID);
                        }
                    }
                    model.SaveChanges();

                    foreach (var item in lst.Where(c => c.ServiceType == "WFL_PacketDriver"))
                    {
                        var obj = model.WFL_PacketDriver.FirstOrDefault(c => c.ID == item.ServiceID);
                        if (obj != null)
                        {
                            obj.IsSend = true;
                            obj.ModifiedDate = DateTime.Now;
                            obj.ModifiedBy = Account.UserName;
                            lstPacketID.Add(obj.PacketID);
                        }
                    }
                    model.SaveChanges();

                    foreach (var item in lstPacketID.Distinct().ToList())
                    {
                        var packet = model.WFL_Packet.FirstOrDefault(c => c.ID == item);
                        if (packet != null)
                        {
                            if (model.WFL_PacketAction.Where(c => c.PacketID == item).Count() == model.WFL_PacketAction.Where(c => c.PacketID == item).Count(c => c.IsSend) && model.WFL_PacketDriver.Where(c => c.PacketID == item).Count() == model.WFL_PacketDriver.Where(c => c.PacketID == item).Count(c => c.IsSend))
                            {
                                packet.PacketProcessID = -(int)SYSVarType.PacketProcessSend;
                                packet.ModifiedDate = DateTime.Now;
                                packet.ModifiedBy = Account.UserName;
                            }
                        }
                    }
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


        public List<DTOTriggerSMS> WFL_TriggerSMS_List()
        {
            try
            {
                var result = new List<DTOTriggerSMS>();
                using (var model = new DataEntities())
                {
                    model.EventAccount = Account; model.EventRunning = false;

                    result.AddRange(model.WFL_DefineWFMessage.Where(c => c.StatusOfMessageID == -(int)SYSVarType.StatusOfMessageWait && c.WFL_DefineWFAction.TypeOfActionID == (int)WFLTypeOfAction.SMS && !string.IsNullOrEmpty(c.WFL_DefineWFAction.SYS_User.TelNo)).Select(c => new DTOTriggerSMS
                    {
                        ServiceID = c.ID,
                        ServiceType = "WFL_DefineWFMessage",
                        TelNo = c.WFL_DefineWFAction.SYS_User.TelNo,
                        Message = c.Message
                    }).ToList());

                    DateTime dtNow = DateTime.Now;
                    var lstPacketAction = model.WFL_PacketAction.Where(c => !c.IsSend && c.WFL_Packet.PacketProcessID == -(int)SYSVarType.PacketProcessClose && c.WFL_PacketSettingAction.TypeOfActionID == (int)WFLTypeOfAction.SMS && c.WFL_Packet.TimeSendOffer < dtNow).Select(c => new
                    {
                        c.ID,
                        c.Body,
                        TelNo = c.WFL_PacketSettingAction.SYS_User.TelNo,
                        ServiceType = "WFL_PacketAction",
                    }).ToList();

                    lstPacketAction.AddRange(model.WFL_PacketDriver.Where(c => !c.IsSend && c.WFL_Packet.PacketProcessID == -(int)SYSVarType.PacketProcessClose && c.TypeOfActionID == (int)WFLTypeOfAction.SMS && c.WFL_Packet.TimeSendOffer < dtNow).Select(c => new
                    {
                        c.ID,
                        c.Body,
                        TelNo = model.SYS_User.Count(d => d.DriverID == c.DriverID && d.TelNo != null) > 0 ? model.SYS_User.FirstOrDefault(d => d.DriverID == c.DriverID).TelNo : "",
                        ServiceType = "WFL_PacketDriver",
                    }).ToList());
                    foreach (var action in lstPacketAction.Where(c => !string.IsNullOrEmpty(c.TelNo)))
                    {
                        DTOTriggerSMS item = new DTOTriggerSMS();
                        item.ServiceID = action.ID;
                        item.ServiceType = action.ServiceType;
                        item.TelNo = action.TelNo;
                        item.Message = action.Body;
                        result.Add(item);
                    }
                }
                return result;
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

        public void WFL_TriggerSMS_Send(List<DTOTriggerSMS> lst)
        {
            try
            {
                using (var model = new DataEntities())
                {
                    //model.EventAccount = Account; model.EventRunning = false;
                    List<long> lstPacketID = new List<long>();

                    foreach (var item in lst.Where(c => c.ServiceType == "WFL_DefineWFMessage"))
                    {
                        var obj = model.WFL_DefineWFMessage.FirstOrDefault(c => c.ID == item.ServiceID);
                        if (obj != null)
                        {
                            obj.StatusOfMessageID = -(int)SYSVarType.StatusOfMessageSended;
                            obj.ModifiedDate = DateTime.Now;
                            obj.ModifiedBy = Account.UserName;
                        }
                    }
                    model.SaveChanges();

                    foreach (var item in lst.Where(c => c.ServiceType == "WFL_PacketAction"))
                    {
                        var obj = model.WFL_PacketAction.FirstOrDefault(c => c.ID == item.ServiceID);
                        if (obj != null)
                        {
                            obj.IsSend = true;
                            obj.ModifiedDate = DateTime.Now;
                            obj.ModifiedBy = Account.UserName;
                            lstPacketID.Add(obj.PacketID);
                        }
                    }
                    model.SaveChanges();

                    foreach (var item in lst.Where(c => c.ServiceType == "WFL_PacketDriver"))
                    {
                        var obj = model.WFL_PacketDriver.FirstOrDefault(c => c.ID == item.ServiceID);
                        if (obj != null)
                        {
                            obj.IsSend = true;
                            obj.ModifiedDate = DateTime.Now;
                            obj.ModifiedBy = Account.UserName;
                            lstPacketID.Add(obj.PacketID);
                        }
                    }
                    model.SaveChanges();

                    foreach (var item in lstPacketID.Distinct().ToList())
                    {
                        var packet = model.WFL_Packet.FirstOrDefault(c => c.ID == item);
                        if (packet != null)
                        {
                            if (model.WFL_PacketAction.Where(c => c.PacketID == item).Count() == model.WFL_PacketAction.Where(c => c.PacketID == item).Count(c => c.IsSend) && model.WFL_PacketDriver.Where(c => c.PacketID == item).Count() == model.WFL_PacketDriver.Where(c => c.PacketID == item).Count(c => c.IsSend))
                            {
                                packet.PacketProcessID = -(int)SYSVarType.PacketProcessSend;
                                packet.ModifiedDate = DateTime.Now;
                                packet.ModifiedBy = Account.UserName;
                            }
                        }
                    }
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
    }
}

