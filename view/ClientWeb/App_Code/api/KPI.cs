using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Security;
using DTO;
using CacheManager.Core;
using System.Web;
using Microsoft.Practices.Unity;
using Newtonsoft.Json;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Presentation;
using IServices;
using System.IO;
using OfficeOpenXml;
using System.ServiceModel;
using OfficeOpenXml.Style;

namespace ClientWeb
{
    public class KPIController : BaseController
    {
        #region Customer
        [HttpPost]
        public DTOResult Customer_List()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.Customer_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult Vendor_List()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.Vendor_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult KPITime_List(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                int kpiID = (int)dynParam.kpiID;
                string request = dynParam.request.ToString();
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPITime_List(request, kpiID, cusID, from.Date, to.Date.AddDays(1).AddSeconds(-1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPITime_Save(dynamic dynParam)
        {
            try
            {
                KPIKPITime item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIKPITime>(dynParam.item.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPITime_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public void KPITime_Generate(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                int cusID = (int)dynParam.cusID;
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPITime_Generate(dtfrom.Date, dtto.Date.AddDays(1).AddSeconds(-1));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string KPITime_Excel(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                int kpiID = (int)dynParam.kpiID;
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());

                List<KPIKPITime> sData = new List<KPIKPITime>();
                ServiceFactory.SVKpi((IServices.ISVKPI sv) =>
                {
                    sData = sv.KPITime_Excel(kpiID, cusID, from, to);
                });

                string file = "/" + FolderUpload.Export + "KPITime_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    int col = 1, row = 1;
                    worksheet.Cells[row, col].Value = "Đơn hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số SO"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số DN"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã khách hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên khách hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày y/c ĐH"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày DN"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đến kho"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Rời kho"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Vào máng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ra máng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đến NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Rời NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "BĐ dỡ hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "KT dỡ hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày hóa đơn"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày ETD chuyến"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày ETA chuyến"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày ATD chuyến"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày ATA chuyến"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày đơn hàng ETD"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày đơn hàng ETA"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày đơn hàng ETD yêu cầu"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày đơn hàng ETA yêu cầu"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "CutOfTime"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đạt KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Lý do"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    foreach (var item in sData)
                    {
                        row += 1; col = 1;
                        worksheet.Cells[row, col].Value = item.OrderCode;
                        col++; worksheet.Cells[row, col].Value = item.SOCode;
                        col++; worksheet.Cells[row, col].Value = item.DNCode;
                        col++; worksheet.Cells[row, col].Value = item.CustomerCode;
                        col++; worksheet.Cells[row, col].Value = item.CustomerName;
                        col++; worksheet.Cells[row, col].Value = item.DateData;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateDN;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromCome;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLeave;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLoadStart;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLoadEnd;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToCome;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLeave;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLoadStart;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLoadEnd;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateInvoice;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.KPIDate;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateTOMasterETD;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateTOMasterETA;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateTOMasterATD;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateTOMasterATA;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateOrderETD;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateOrderETA;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateOrderETDRequest;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateOrderETARequest;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateOrderCutOfTime;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.IsKPI.HasValue ? item.IsKPI == true ? "Đạt" : "Không đạt" : "";
                        col++; worksheet.Cells[row, col].Value = item.ReasonName;
                        col++; worksheet.Cells[row, col].Value = item.Note;
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<KPIReason> KPITimeReason_List(dynamic dynParam)
        {
            try
            {
                int kpiID = (int)dynParam.kpiID;
                var result = default(List<KPIReason>);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPITimeReason_List(kpiID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region KPITimeDate

        [HttpPost]
        public DTOResult KPITimeDate_List(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                int kpiID = (int)dynParam.kpiID;
                string request = dynParam.request.ToString();
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPITimeDate_List(request, kpiID, cusID, from.Date, to.Date.AddDays(1).AddSeconds(-1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPITimeDate_Save(dynamic dynParam)
        {
            try
            {
                KPITimeDate item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPITimeDate>(dynParam.item.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPITimeDate_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPITimeDate_Generate(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                int cusID = (int)dynParam.cusID;
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPITimeDate_Generate(dtfrom.Date, dtto.Date.AddDays(1).AddSeconds(-1), cusID);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string KPITimeDate_Excel(dynamic dynParam)
        {
            try
            {
                int cusID = (int)dynParam.cusID;
                int kpiID = (int)dynParam.kpiID;
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());

                List<KPITimeDate> sData = new List<KPITimeDate>();
                ServiceFactory.SVKpi((IServices.ISVKPI sv) =>
                {
                    sData = sv.KPITimeDate_Excel(kpiID, cusID, from, to);
                });

                string file = "/" + FolderUpload.Export + "KPITime_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    int col = 1, row = 1;
                    worksheet.Cells[row, col].Value = "Đơn hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số SO"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số DN"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã khách hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên khách hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày y/c ĐH"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày DN"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đến kho"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Rời kho"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Vào máng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ra máng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đến NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Rời NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "BĐ dỡ hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "KT dỡ hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày hóa đơn"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày ETD chuyến"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày ETA chuyến"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày ATD chuyến"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày ATA chuyến"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày đơn hàng ETD"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày đơn hàng ETA"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày đơn hàng ETD yêu cầu"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày đơn hàng ETA yêu cầu"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "CutOfTime"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đạt KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Lý do"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    foreach (var item in sData)
                    {
                        row += 1; col = 1;
                        worksheet.Cells[row, col].Value = item.OrderCode;
                        col++; worksheet.Cells[row, col].Value = item.SOCode;
                        col++; worksheet.Cells[row, col].Value = item.DNCode;
                        col++; worksheet.Cells[row, col].Value = item.CustomerCode;
                        col++; worksheet.Cells[row, col].Value = item.CustomerName;
                        col++; worksheet.Cells[row, col].Value = item.DateData;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateDN;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromCome;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLeave;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLoadStart;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLoadEnd;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToCome;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLeave;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLoadStart;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLoadEnd;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateInvoice;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.KPIDate;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateTOMasterETD;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateTOMasterETA;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateTOMasterATD;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateTOMasterATA;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateOrderETD;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateOrderETA;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateOrderETDRequest;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateOrderETARequest;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateOrderCutOfTime;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.IsKPI.HasValue ? item.IsKPI == true ? "Đạt" : "Không đạt" : "";
                        col++; worksheet.Cells[row, col].Value = item.ReasonName;
                        col++; worksheet.Cells[row, col].Value = item.Note;
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<KPIReason> KPITimeDateReason_List(dynamic dynParam)
        {
            try
            {
                int kpiID = (int)dynParam.kpiID;
                var result = default(List<KPIReason>);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPITimeDateReason_List(kpiID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTOTypeOfKPI> KPITimeDate_GetTypeOfKPI()
        {
            try
            {
                var result = default(List<DTOTypeOfKPI>);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPITimeDate_GetTypeOfKPI();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        [HttpPost]
        public List<KPIKPI> KPIKPI_List()
        {
            try
            {
                var result = default(List<KPIKPI>);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIKPI_List();
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public DTOResult KPIReason_List(dynamic dynParam)
        {
            try
            {
                string request = string.Empty;
                try
                {
                    request = dynParam.request.ToString();
                }
                catch
                { }
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIReason_List(request);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int KPIReason_Save(dynamic dynParam)
        {
            try
            {
                KPIReason item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIReason>(dynParam.item.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    item.ID = sv.KPIReason_Save(item);
                });
                return item.ID;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPIReason_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPIReason_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string KPIReason_Export()
        {
            try
            {
                List<KPIReason> sData = new List<KPIReason>();
                ServiceFactory.SVKpi((IServices.ISVKPI sv) =>
                {
                    sData = sv.KPIReason_Export();
                });

                string file = "/" + FolderUpload.Export + "KPIReason_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    int col = 1, row = 1;
                    worksheet.Cells[row, col].Value = "STT";
                    col++; worksheet.Cells[row, col].Value = "Mã"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên"; worksheet.Column(col).Width = 25;
                    col++; worksheet.Cells[row, col].Value = "KPI"; worksheet.Column(col).Width = 20;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    
                    foreach (var item in sData)
                    {
                        row +=1; col = 1;
                        worksheet.Cells[row, col].Value = row -1;
                        col++; worksheet.Cells[row, col].Value = item.Code;
                        col++; worksheet.Cells[row, col].Value = item.ReasonName;
                        col++; worksheet.Cells[row, col].Value = item.KPICode;
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        
        [HttpPost]
        public List<KPIReason_Import> KPIReason_Check(dynamic dynParam)
        {
            try
            {
                string file = dynParam.file.ToString();

                List<KPIKPI> sData = new List<KPIKPI>();
                List<KPIReason_Import> resBody = new List<KPIReason_Import>();
                ServiceFactory.SVKpi((IServices.ISVKPI sv) =>
                {
                    sData = sv.KPIKPI_List();
                });

                using (System.IO.FileStream fs = new System.IO.FileStream(HttpContext.Current.Server.MapPath("/" + file), System.IO.FileMode.Open, System.IO.FileAccess.Read))
                {
                    using (var package = new ExcelPackage(fs))
                    {
                        ExcelWorksheet worksheet = ExcelHelper.GetWorksheetByIndex(package, 1);
                        if (worksheet != null)
                        {
                            int col = 2, row = 2;
                            while (row <= worksheet.Dimension.End.Row)
                            {
                                col = 2;
                                KPIReason_Import obj = new KPIReason_Import();
                                obj.ExcelRow = row;
                                string str = ExcelHelper.GetValue(worksheet, row, col);
                                obj.Code = str;
                                if (string.IsNullOrEmpty(str))
                                    obj.ExcelError = "Thiếu mã lý do. ";
                                else
                                {
                                    var objCheck = resBody.FirstOrDefault(c => c.Code.ToLower().Trim() == str.ToLower().Trim());
                                    if (objCheck != null)
                                    {
                                        obj.ExcelError += "Mã [" + str + "] đã tồn tại.";
                                    }
                                }
                                col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                obj.ReasonName = str;
                                if (string.IsNullOrEmpty(str))
                                    obj.ExcelError += "Thiếu tên lý do. ";
                                col++; str = ExcelHelper.GetValue(worksheet, row, col);
                                obj.KPICode =str;
                                if (string.IsNullOrEmpty(str))
                                    obj.ExcelError += "KPI trống. ";
                                else
                                {
                                    var objKPI = sData.FirstOrDefault(c => c.Code.ToLower().Trim() == str.ToLower().Trim());
                                    if (objKPI == null)
                                    {
                                        obj.ExcelError += "KPI [" + str + "] không tồn tại.";
                                    }
                                    else
                                    {
                                        obj.KPIID = objKPI.ID;
                                    }
                                }
                                if (string.IsNullOrEmpty(obj.ExcelError))
                                    obj.ExcelSuccess = true;
                                resBody.Add(obj);
                                row++;
                            }
                        }
                    }
                }
                return resBody;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPIReason_Import(dynamic dynParam)
        {
            try
            {
                List<KPIReason_Import> data = Newtonsoft.Json.JsonConvert.DeserializeObject<List<KPIReason_Import>>(dynParam.data.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPIReason_Import(data);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Vendor
        [HttpPost]
        public DTOResult KPIVENTime_List(dynamic dynParam)
        {
            try
            {
                int venID = (int)dynParam.venID;
                int kpiID = (int)dynParam.kpiID;
                string request = dynParam.request.ToString();
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIVENTime_List(request, kpiID, venID, from.Date, to.Date.AddDays(1).AddSeconds(-1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPIVENTime_Save(dynamic dynParam)
        {
            try
            {
                KPIVENTime item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIVENTime>(dynParam.item.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPIVENTime_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPIVENTime_Generate(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                int venID = (int)dynParam.venID;
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPIVENTime_Generate(dtfrom.Date, dtto.Date.AddDays(1).AddSeconds(-1));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string KPIVENTime_Excel(dynamic dynParam)
        {
            try
            {
                int venID = (int)dynParam.venID;
                int kpiID = (int)dynParam.kpiID;
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());

                List<KPIVENTime> sData = new List<KPIVENTime>();
                ServiceFactory.SVKpi((IServices.ISVKPI sv) =>
                {
                    sData = sv.KPIVENTime_Excel(kpiID, venID, from, to);
                });

                string file = "/" + FolderUpload.Export + "KPITime_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    int col = 1, row = 1;
                    worksheet.Cells[row, col].Value = "Đơn hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số SO"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số DN"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Mã khách hàng"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên khách hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày y/c ĐH"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày DN"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đến kho"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Rời kho"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Vào máng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ra máng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đến NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Rời NPP"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "BĐ dỡ hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "KT dỡ hàng"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày hóa đơn"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đạt KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Lý do"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    foreach (var item in sData)
                    {
                        row += 1; col = 1;
                        worksheet.Cells[row, col].Value = item.OrderCode;
                        col++; worksheet.Cells[row, col].Value = item.SOCode;
                        col++; worksheet.Cells[row, col].Value = item.DNCode;
                        col++; worksheet.Cells[row, col].Value = item.CustomerCode;
                        col++; worksheet.Cells[row, col].Value = item.CustomerName;
                        col++; worksheet.Cells[row, col].Value = item.DateData;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateDN;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromCome;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLeave;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLoadStart;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateFromLoadEnd;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToCome;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLeave;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLoadStart;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateToLoadEnd;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.DateInvoice;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.KPIDate;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.IsKPI.HasValue ? item.IsKPI == true ? "Đạt" : "Không đạt" : "";
                        col++; worksheet.Cells[row, col].Value = item.ReasonName;
                        col++; worksheet.Cells[row, col].Value = item.Note;
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region Tender
        [HttpPost]
        public DTOResult KPIVENTenderFTL_List(dynamic dynParam)
        {
            try
            {
                int venID = (int)dynParam.venID;
                string request = dynParam.request.ToString();
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIVENTenderFTL_List(request, venID, from.Date, to.Date.AddDays(1).AddSeconds(-1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPIVENTenderFTL_Save(dynamic dynParam)
        {
            try
            {
                KPIVENTenderFTL item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIVENTenderFTL>(dynParam.item.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPIVENTenderFTL_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPIVENTenderFTL_Generate(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPIVENTenderFTL_Generate(dtfrom.Date, dtto.Date.AddDays(1).AddSeconds(-1));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string KPIVENTenderFTL_Excel(dynamic dynParam)
        {
            try
            {
                int venID = (int)dynParam.venID;
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());

                List<KPIVENTenderFTL> sData = new List<KPIVENTenderFTL>();
                ServiceFactory.SVKpi((IServices.ISVKPI sv) =>
                {
                    sData = sv.KPIVENTenderFTL_Excel(venID, from, to);
                });

                string file = "/" + FolderUpload.Export + "KPIVENTender_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    int col = 1, row = 1;
                    worksheet.Cells[row, col].Value = "Mã đối tác"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tên đối tác"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày y/c ĐH"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đạt KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "KPI Code"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số lần chấp nhận"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số lần tính KPI"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Số lần từ chối"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Tổng chuyến"; worksheet.Column(col).Width = 15;
                    col++; worksheet.Cells[row, col].Value = "Lý do"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    foreach (var item in sData)
                    {
                        row += 1; col = 1;
                        col++; worksheet.Cells[row, col].Value = item.VendorCode;
                        col++; worksheet.Cells[row, col].Value = item.VendorName;
                        col++; worksheet.Cells[row, col].Value = item.DateData;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.IsKPI.HasValue ? item.IsKPI == true ? "Đạt" : "Không đạt" : "";
                        col++; worksheet.Cells[row, col].Value = item.KPICode;
                        col++; worksheet.Cells[row, col].Value = item.TotalAccept;
                        col++; worksheet.Cells[row, col].Value = item.TotalKPI;
                        col++; worksheet.Cells[row, col].Value = item.TotalReject;
                        col++; worksheet.Cells[row, col].Value = item.TotalSchedule;
                        col++; worksheet.Cells[row, col].Value = item.ReasonName;
                        col++; worksheet.Cells[row, col].Value = item.Note;
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOResult KPIVENTenderLTL_List(dynamic dynParam)
        {
            try
            {
                int venID = (int)dynParam.venID;
                string request = dynParam.request.ToString();
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIVENTenderLTL_List(request, venID, from.Date, to.Date.AddDays(1).AddSeconds(-1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPIVENTenderLTL_Save(dynamic dynParam)
        {
            try
            {
                KPIVENTenderLTL item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIVENTenderLTL>(dynParam.item.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPIVENTenderLTL_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPIVENTenderLTL_Generate(dynamic dynParam)
        {
            try
            {
                DateTime dtfrom = Convert.ToDateTime(dynParam.dtfrom.ToString());
                DateTime dtto = Convert.ToDateTime(dynParam.dtto.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPIVENTenderLTL_Generate(dtfrom.Date, dtto.Date.AddDays(1).AddSeconds(-1));
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public string KPIVENTenderLTL_Excel(dynamic dynParam)
        {
            try
            {
                int venID = (int)dynParam.venID;
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());

                List<KPIVENTenderLTL> sData = new List<KPIVENTenderLTL>();
                ServiceFactory.SVKpi((IServices.ISVKPI sv) =>
                {
                    sData = sv.KPIVENTenderLTL_Excel(venID, from, to);
                });

                string file = "/" + FolderUpload.Export + "KPIVENTender_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";

                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(file)))
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(file));
                FileInfo exportfile = new FileInfo(HttpContext.Current.Server.MapPath(file));
                using (ExcelPackage package = new ExcelPackage(exportfile))
                {
                    ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Sheet1");
                    int col = 1, row = 1;
                    worksheet.Cells[row, col].Value = "Mã đối tác"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tên đối tác"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ngày y/c ĐH"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Đạt KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "KPI Code"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Tấn yêu cầu"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Khối yêu cầu"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Số lượng yêu cầu"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Số tấn chấp nhận"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Số khối chấp nhận"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Số lượng chấp nhận"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Số KPI"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "KPI Code"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Lý do"; worksheet.Column(col).Width = 20;
                    col++; worksheet.Cells[row, col].Value = "Ghi chú"; worksheet.Column(col).Width = 30;

                    ExcelHelper.CreateCellStyle(worksheet, row, 1, row, col, false, true, ExcelHelper.ColorGreen, ExcelHelper.ColorWhite, 0, "");
                    worksheet.Cells[row, 1, row, col].Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;

                    foreach (var item in sData)
                    {
                        row += 1; col = 1;
                        col++; worksheet.Cells[row, col].Value = item.VendorCode;
                        col++; worksheet.Cells[row, col].Value = item.VendorName;
                        col++; worksheet.Cells[row, col].Value = item.DateData;
                        ExcelHelper.CreateFormat(worksheet, row, col, ExcelHelper.FormatDMYHM);
                        col++; worksheet.Cells[row, col].Value = item.IsKPI.HasValue ? item.IsKPI == true ? "Đạt" : "Không đạt" : "";
                        col++; worksheet.Cells[row, col].Value = item.TonOrder;
                        col++; worksheet.Cells[row, col].Value = item.CBMOrder;
                        col++; worksheet.Cells[row, col].Value = item.QuantityOrder;
                        col++; worksheet.Cells[row, col].Value = item.TonAccept;
                        col++; worksheet.Cells[row, col].Value = item.CBMAccept;
                        col++; worksheet.Cells[row, col].Value = item.QuantityAccept;
                        col++; worksheet.Cells[row, col].Value = item.ValueKPI;
                        col++; worksheet.Cells[row, col].Value = item.KPICode;
                        col++; worksheet.Cells[row, col].Value = item.ReasonName;
                        col++; worksheet.Cells[row, col].Value = item.Note;
                    }
                    package.Save();
                }
                return file;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
        #endregion

        #region DashBoard

        [HttpPost]
        public List<KPIDashBoard_DN> Dashboard_DN_Data(dynamic dynParam)
        {
            try
            {
                int kpiID = (int)dynParam.kpiID;
                int cusID = (int)dynParam.cusID;
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                var result = default(List<KPIDashBoard_DN>);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.Dashboard_DN_Data(cusID, kpiID, from.Date, to.Date.AddDays(1).AddSeconds(-1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<KPIDashBoard_Reason> Dashboard_Reason_Data(dynamic dynParam)
        {
            try
            {
                int kpiID = (int)dynParam.kpiID;
                int cusID = (int)dynParam.cusID;
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                var result = default(List<KPIDashBoard_Reason>);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.Dashboard_Reason_Data(cusID, kpiID, from.Date, to.Date.AddDays(1).AddSeconds(-1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public List<KPIDashBoard_DN> Dashboard_DN_VENData(dynamic dynParam)
        {
            try
            {
                int kpiID = (int)dynParam.kpiID;
                int venID = (int)dynParam.venID;
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                var result = default(List<KPIDashBoard_DN>);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.Dashboard_DN_VENData(venID, kpiID, from.Date, to.Date.AddDays(1).AddSeconds(-1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<KPIDashBoard_Reason> Dashboard_Reason_VENData(dynamic dynParam)
        {
            try
            {
                int kpiID = (int)dynParam.kpiID;
                int venID = (int)dynParam.venID;
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                var result = default(List<KPIDashBoard_Reason>);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.Dashboard_Reason_VENData(venID, kpiID, from.Date, to.Date.AddDays(1).AddSeconds(-1));
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region KPI KPI
        [HttpPost]
        public DTOResult KPIKPI_GetList(dynamic dynParam)
        {
            try
            {
                string request = string.Empty;
                try 
                {
                    request = dynParam.request.ToString();
                }
                catch { }
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) => 
                {
                    result = sv.KPIKPI_GetList(request); 
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public KPIKPI KPIKPI_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(KPIKPI);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIKPI_Get(id);
                });
                return result;
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public int KPIKPI_Update(dynamic dynParam)
        {
            try
            {
                KPIKPI item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIKPI>(dynParam.item.ToString());
                int result = -1;
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIKPI_Save(item);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void KPIKPI_Destroy(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPIKPI_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<KPIKPI> KPIKPI_AllList()
        {
            try
            {
                var result = new List<KPIKPI>();
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIKPI_AllList();
                   
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region KPIKPI Column

        [HttpPost]
        public DTOResult KPIColumn_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int ID = (int)dynParam.ID;
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIColumn_List(request, ID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public KPIColumn KPIColumn_Get(dynamic dynParma)
        {
            try 
            {
                int id = (int)dynParma.ID;
                var result = default(KPIColumn);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIColumn_Get(id);
                });
                return result;
            }
           
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public int KPIColumn_Save(dynamic dynParam)
        {
            try
            {
                KPIColumn item = Newtonsoft.Json.JsonConvert.DeserializeObject<KPIColumn>(dynParam.item.ToString());
                int KPIID = (int)dynParam.KPIID;
                var result = -1;
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIColumn_Save(item, KPIID);
                });
                return result;
            }
           
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public void KPIColumn_Delete(dynamic dynParam)
        {
            try
            {
                int ID = (int)dynParam.ID;
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPIColumn_Delete(ID);
                });
            }
            
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public List<KPIField> KPIField_List(dynamic dynParam)
        {
            try
            {
                int typeID = (int)dynParam.typeID;
                List<KPIField> result = new List<KPIField>();
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPIField_List(typeID);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region KPI Collection
        [HttpPost]
        public DTOResult KPICollection_GetList(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                int KPIID = (int)dynParam.KPIID;               
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());

                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPICollection_GetList(request, KPIID, from.Date, to.Date);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        [HttpPost]
        public void KPICollection_Generate(dynamic dynParam)
        {
            try
            {
                int KPIID = (int)dynParam.KPIID;
                DateTime from = Convert.ToDateTime(dynParam.from.ToString());
                DateTime to = Convert.ToDateTime(dynParam.to.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPICollection_Generate(KPIID, from.Date, to.Date);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region TypeOfKPI
        [HttpPost]
        public DTOResult KPITypeOfKPI_List(dynamic dynParam)
        {
            try
            {
                string request = dynParam.request.ToString();
                var result = default(DTOResult);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPITypeOfKPI_List(request);
                });
                return result;

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        public DTOTypeOfKPI KPITypeOfKPI_Get(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.ID;
                var result = default(DTOTypeOfKPI);
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    result = sv.KPITypeOfKPI_Get(id);
                });
                return result;
            }

            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPost]
        public void KPITypeOfKPI_Save(dynamic dynParam)
        {
            try
            {
                DTOTypeOfKPI item = Newtonsoft.Json.JsonConvert.DeserializeObject<DTOTypeOfKPI>(dynParam.item.ToString());
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPITypeOfKPI_Save(item);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void KPITypeOfKPI_Delete(dynamic dynParam)
        {
            try
            {
                int id = (int)dynParam.id;
                ServiceFactory.SVKpi((ISVKPI sv) =>
                {
                    sv.KPITypeOfKPI_Delete(id);
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public DTOResult KPIGet_KPITypeOfKPIID()
        {
            try
            {
                var result = default(DTOResult);
                ServiceFactory.SVCategory((ISVCategory sv) =>
                {
                    result = sv.ALL_SysVar(SYSVarType.KPITypeOfKPI);
                });
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}